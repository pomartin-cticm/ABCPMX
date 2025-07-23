<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_SectionSFB
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_SectionSFB))
        Me.img_Section = New System.Windows.Forms.PictureBox()
        Me.pan_Lamine = New System.Windows.Forms.Panel()
        Me.lbl_Profiles = New System.Windows.Forms.Label()
        Me.Grid_ProfilesSup = New System.Windows.Forms.DataGridView()
        Me.Col_ListeSup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_HISTARSup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbl_Gamme = New System.Windows.Forms.Label()
        Me.lst_GammeS = New System.Windows.Forms.ListBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Acier = New System.Windows.Forms.Panel()
        Me.btn_FyFu = New System.Windows.Forms.Button()
        Me.img_ReductionCurve = New System.Windows.Forms.PictureBox()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Pan_DimPRS = New System.Windows.Forms.Panel()
        Me.cmb_NuancePlat = New System.Windows.Forms.ComboBox()
        Me.lbl_InfoFyWP = New System.Windows.Forms.Label()
        Me.cmb_ReductionCurveWP = New System.Windows.Forms.ComboBox()
        Me.cmb_GradeWP = New System.Windows.Forms.ComboBox()
        Me.etq_UnitDim2SFB = New System.Windows.Forms.Label()
        Me.etq_UnitDim1SFB = New System.Windows.Forms.Label()
        Me.txt_tpSFB = New System.Windows.Forms.TextBox()
        Me.txt_bpSFB = New System.Windows.Forms.TextBox()
        Me.img_tpSFB = New System.Windows.Forms.PictureBox()
        Me.img_bpSFB = New System.Windows.Forms.PictureBox()
        Me.lbl_WPSteel = New System.Windows.Forms.Label()
        Me.lbl_ThicknessSFB = New System.Windows.Forms.Label()
        Me.lbl_WidthSFB = New System.Windows.Forms.Label()
        Me.lbl_ReductionCurve = New System.Windows.Forms.Label()
        Me.lbl_Qualite = New System.Windows.Forms.Label()
        Me.lbl_Grade = New System.Windows.Forms.Label()
        Me.GridAciers = New System.Windows.Forms.DataGridView()
        Me.Col_Grade = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Qualite = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_ReductionCurve = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbl_Acier = New System.Windows.Forms.Label()
        Me.pan_DefinitionAcier = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Droite = New System.Windows.Forms.Panel()
        Me.TLpan_Saisie = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_WeldedPlate = New System.Windows.Forms.Label()
        Me.lbl_ParentProfile = New System.Windows.Forms.Label()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Provi = New System.Windows.Forms.Panel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.imgList_UY = New System.Windows.Forms.ImageList(Me.components)
        CType(Me.img_Section, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Lamine.SuspendLayout()
        CType(Me.Grid_ProfilesSup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.pan_Acier.SuspendLayout()
        CType(Me.img_ReductionCurve, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Pan_DimPRS.SuspendLayout()
        CType(Me.img_tpSFB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_bpSFB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridAciers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_DefinitionAcier.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pan_Droite.SuspendLayout()
        Me.TLpan_Saisie.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLpan_Gauche.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.pan_General.SuspendLayout()
        Me.SuspendLayout()
        '
        'img_Section
        '
        Me.img_Section.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Section.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Section.Location = New System.Drawing.Point(1, 0)
        Me.img_Section.Margin = New System.Windows.Forms.Padding(1, 0, 1, 1)
        Me.img_Section.Name = "img_Section"
        Me.img_Section.Size = New System.Drawing.Size(100, 50)
        Me.img_Section.TabIndex = 0
        Me.img_Section.TabStop = False
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
        Me.pan_Lamine.Location = New System.Drawing.Point(0, 30)
        Me.pan_Lamine.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Lamine.Name = "pan_Lamine"
        Me.pan_Lamine.Size = New System.Drawing.Size(353, 290)
        Me.pan_Lamine.TabIndex = 3
        '
        'lbl_Profiles
        '
        Me.lbl_Profiles.AutoSize = True
        Me.lbl_Profiles.Location = New System.Drawing.Point(132, 6)
        Me.lbl_Profiles.Name = "lbl_Profiles"
        Me.lbl_Profiles.Size = New System.Drawing.Size(57, 13)
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
        Me.Grid_ProfilesSup.Location = New System.Drawing.Point(129, 25)
        Me.Grid_ProfilesSup.MultiSelect = False
        Me.Grid_ProfilesSup.Name = "Grid_ProfilesSup"
        Me.Grid_ProfilesSup.RowHeadersVisible = False
        Me.Grid_ProfilesSup.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Grid_ProfilesSup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid_ProfilesSup.ShowCellToolTips = False
        Me.Grid_ProfilesSup.Size = New System.Drawing.Size(215, 238)
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
        Me.lbl_Gamme.Location = New System.Drawing.Point(7, 6)
        Me.lbl_Gamme.Name = "lbl_Gamme"
        Me.lbl_Gamme.Size = New System.Drawing.Size(59, 13)
        Me.lbl_Gamme.TabIndex = 16
        Me.lbl_Gamme.Text = "lbl_Gamme"
        Me.lbl_Gamme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lst_GammeS
        '
        Me.lst_GammeS.FormattingEnabled = True
        Me.lst_GammeS.Location = New System.Drawing.Point(6, 25)
        Me.lst_GammeS.Name = "lst_GammeS"
        Me.lst_GammeS.Size = New System.Drawing.Size(119, 238)
        Me.lst_GammeS.TabIndex = 17
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.pan_Acier, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.img_Section, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(353, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(300, 498)
        Me.TableLayoutPanel2.TabIndex = 2
        '
        'pan_Acier
        '
        Me.pan_Acier.Controls.Add(Me.btn_FyFu)
        Me.pan_Acier.Controls.Add(Me.img_ReductionCurve)
        Me.pan_Acier.Location = New System.Drawing.Point(1, 249)
        Me.pan_Acier.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.pan_Acier.Name = "pan_Acier"
        Me.pan_Acier.Size = New System.Drawing.Size(245, 141)
        Me.pan_Acier.TabIndex = 2
        '
        'btn_FyFu
        '
        Me.btn_FyFu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_FyFu.Image = CType(resources.GetObject("btn_FyFu.Image"), System.Drawing.Image)
        Me.btn_FyFu.Location = New System.Drawing.Point(214, 3)
        Me.btn_FyFu.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_FyFu.Name = "btn_FyFu"
        Me.btn_FyFu.Size = New System.Drawing.Size(28, 28)
        Me.btn_FyFu.TabIndex = 2
        Me.btn_FyFu.UseVisualStyleBackColor = True
        '
        'img_ReductionCurve
        '
        Me.img_ReductionCurve.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_ReductionCurve.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_ReductionCurve.Location = New System.Drawing.Point(34, 23)
        Me.img_ReductionCurve.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.img_ReductionCurve.Name = "img_ReductionCurve"
        Me.img_ReductionCurve.Size = New System.Drawing.Size(100, 34)
        Me.img_ReductionCurve.TabIndex = 1
        Me.img_ReductionCurve.TabStop = False
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Pan_DimPRS
        '
        Me.Pan_DimPRS.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Pan_DimPRS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pan_DimPRS.Controls.Add(Me.cmb_NuancePlat)
        Me.Pan_DimPRS.Controls.Add(Me.lbl_InfoFyWP)
        Me.Pan_DimPRS.Controls.Add(Me.cmb_ReductionCurveWP)
        Me.Pan_DimPRS.Controls.Add(Me.cmb_GradeWP)
        Me.Pan_DimPRS.Controls.Add(Me.etq_UnitDim2SFB)
        Me.Pan_DimPRS.Controls.Add(Me.etq_UnitDim1SFB)
        Me.Pan_DimPRS.Controls.Add(Me.txt_tpSFB)
        Me.Pan_DimPRS.Controls.Add(Me.txt_bpSFB)
        Me.Pan_DimPRS.Controls.Add(Me.img_tpSFB)
        Me.Pan_DimPRS.Controls.Add(Me.img_bpSFB)
        Me.Pan_DimPRS.Controls.Add(Me.lbl_WPSteel)
        Me.Pan_DimPRS.Controls.Add(Me.lbl_ThicknessSFB)
        Me.Pan_DimPRS.Controls.Add(Me.lbl_WidthSFB)
        Me.Pan_DimPRS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_DimPRS.Location = New System.Drawing.Point(0, 350)
        Me.Pan_DimPRS.Margin = New System.Windows.Forms.Padding(0)
        Me.Pan_DimPRS.Name = "Pan_DimPRS"
        Me.Pan_DimPRS.Size = New System.Drawing.Size(353, 148)
        Me.Pan_DimPRS.TabIndex = 4
        '
        'cmb_NuancePlat
        '
        Me.cmb_NuancePlat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_NuancePlat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NuancePlat.FormattingEnabled = True
        Me.cmb_NuancePlat.Location = New System.Drawing.Point(195, 79)
        Me.cmb_NuancePlat.Name = "cmb_NuancePlat"
        Me.cmb_NuancePlat.Size = New System.Drawing.Size(113, 21)
        Me.cmb_NuancePlat.TabIndex = 58
        '
        'lbl_InfoFyWP
        '
        Me.lbl_InfoFyWP.AutoSize = True
        Me.lbl_InfoFyWP.Location = New System.Drawing.Point(136, 120)
        Me.lbl_InfoFyWP.Name = "lbl_InfoFyWP"
        Me.lbl_InfoFyWP.Size = New System.Drawing.Size(70, 13)
        Me.lbl_InfoFyWP.TabIndex = 5
        Me.lbl_InfoFyWP.Text = "lbl_InfoFyWP"
        Me.lbl_InfoFyWP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmb_ReductionCurveWP
        '
        Me.cmb_ReductionCurveWP.FormattingEnabled = True
        Me.cmb_ReductionCurveWP.Location = New System.Drawing.Point(208, 79)
        Me.cmb_ReductionCurveWP.Name = "cmb_ReductionCurveWP"
        Me.cmb_ReductionCurveWP.Size = New System.Drawing.Size(75, 21)
        Me.cmb_ReductionCurveWP.TabIndex = 4
        Me.cmb_ReductionCurveWP.Visible = False
        '
        'cmb_GradeWP
        '
        Me.cmb_GradeWP.FormattingEnabled = True
        Me.cmb_GradeWP.Location = New System.Drawing.Point(139, 79)
        Me.cmb_GradeWP.Name = "cmb_GradeWP"
        Me.cmb_GradeWP.Size = New System.Drawing.Size(50, 21)
        Me.cmb_GradeWP.TabIndex = 4
        Me.cmb_GradeWP.Visible = False
        '
        'etq_UnitDim2SFB
        '
        Me.etq_UnitDim2SFB.AutoSize = True
        Me.etq_UnitDim2SFB.Location = New System.Drawing.Point(289, 51)
        Me.etq_UnitDim2SFB.Name = "etq_UnitDim2SFB"
        Me.etq_UnitDim2SFB.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim2SFB.TabIndex = 3
        Me.etq_UnitDim2SFB.Text = "mm"
        '
        'etq_UnitDim1SFB
        '
        Me.etq_UnitDim1SFB.AutoSize = True
        Me.etq_UnitDim1SFB.Location = New System.Drawing.Point(289, 22)
        Me.etq_UnitDim1SFB.Name = "etq_UnitDim1SFB"
        Me.etq_UnitDim1SFB.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim1SFB.TabIndex = 3
        Me.etq_UnitDim1SFB.Text = "mm"
        '
        'txt_tpSFB
        '
        Me.txt_tpSFB.Location = New System.Drawing.Point(225, 48)
        Me.txt_tpSFB.Name = "txt_tpSFB"
        Me.txt_tpSFB.Size = New System.Drawing.Size(58, 20)
        Me.txt_tpSFB.TabIndex = 2
        '
        'txt_bpSFB
        '
        Me.txt_bpSFB.Location = New System.Drawing.Point(225, 19)
        Me.txt_bpSFB.Name = "txt_bpSFB"
        Me.txt_bpSFB.Size = New System.Drawing.Size(58, 20)
        Me.txt_bpSFB.TabIndex = 2
        '
        'img_tpSFB
        '
        Me.img_tpSFB.Location = New System.Drawing.Point(189, 48)
        Me.img_tpSFB.Name = "img_tpSFB"
        Me.img_tpSFB.Size = New System.Drawing.Size(37, 20)
        Me.img_tpSFB.TabIndex = 1
        Me.img_tpSFB.TabStop = False
        '
        'img_bpSFB
        '
        Me.img_bpSFB.Location = New System.Drawing.Point(189, 19)
        Me.img_bpSFB.Name = "img_bpSFB"
        Me.img_bpSFB.Size = New System.Drawing.Size(37, 20)
        Me.img_bpSFB.TabIndex = 1
        Me.img_bpSFB.TabStop = False
        '
        'lbl_WPSteel
        '
        Me.lbl_WPSteel.AutoSize = True
        Me.lbl_WPSteel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline)
        Me.lbl_WPSteel.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_WPSteel.Location = New System.Drawing.Point(11, 82)
        Me.lbl_WPSteel.Name = "lbl_WPSteel"
        Me.lbl_WPSteel.Size = New System.Drawing.Size(65, 13)
        Me.lbl_WPSteel.TabIndex = 0
        Me.lbl_WPSteel.Text = "lbl_WPSteel"
        Me.lbl_WPSteel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_ThicknessSFB
        '
        Me.lbl_ThicknessSFB.AutoSize = True
        Me.lbl_ThicknessSFB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline)
        Me.lbl_ThicknessSFB.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_ThicknessSFB.Location = New System.Drawing.Point(11, 48)
        Me.lbl_ThicknessSFB.Name = "lbl_ThicknessSFB"
        Me.lbl_ThicknessSFB.Size = New System.Drawing.Size(72, 13)
        Me.lbl_ThicknessSFB.TabIndex = 0
        Me.lbl_ThicknessSFB.Text = "lbl_Thickness"
        Me.lbl_ThicknessSFB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_WidthSFB
        '
        Me.lbl_WidthSFB.AutoSize = True
        Me.lbl_WidthSFB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline)
        Me.lbl_WidthSFB.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_WidthSFB.Location = New System.Drawing.Point(11, 19)
        Me.lbl_WidthSFB.Name = "lbl_WidthSFB"
        Me.lbl_WidthSFB.Size = New System.Drawing.Size(51, 13)
        Me.lbl_WidthSFB.TabIndex = 0
        Me.lbl_WidthSFB.Text = "lbl_Width"
        Me.lbl_WidthSFB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_ReductionCurve
        '
        Me.lbl_ReductionCurve.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_ReductionCurve.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ReductionCurve.Location = New System.Drawing.Point(169, 11)
        Me.lbl_ReductionCurve.Name = "lbl_ReductionCurve"
        Me.lbl_ReductionCurve.Size = New System.Drawing.Size(176, 17)
        Me.lbl_ReductionCurve.TabIndex = 24
        Me.lbl_ReductionCurve.Text = "lbl_ReductionCurve"
        Me.lbl_ReductionCurve.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Qualite
        '
        Me.lbl_Qualite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Qualite.Location = New System.Drawing.Point(90, 11)
        Me.lbl_Qualite.Name = "lbl_Qualite"
        Me.lbl_Qualite.Size = New System.Drawing.Size(80, 17)
        Me.lbl_Qualite.TabIndex = 23
        Me.lbl_Qualite.Text = "lbl_Qualite"
        Me.lbl_Qualite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Grade
        '
        Me.lbl_Grade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Grade.Location = New System.Drawing.Point(11, 11)
        Me.lbl_Grade.Name = "lbl_Grade"
        Me.lbl_Grade.Size = New System.Drawing.Size(80, 17)
        Me.lbl_Grade.TabIndex = 22
        Me.lbl_Grade.Text = "lbl_Grade"
        Me.lbl_Grade.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.GridAciers.Size = New System.Drawing.Size(335, 422)
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
        'lbl_Acier
        '
        Me.lbl_Acier.AutoSize = True
        Me.lbl_Acier.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Acier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Acier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Acier.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Acier.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Acier.Name = "lbl_Acier"
        Me.lbl_Acier.Size = New System.Drawing.Size(354, 30)
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
        Me.pan_DefinitionAcier.Location = New System.Drawing.Point(0, 30)
        Me.pan_DefinitionAcier.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_DefinitionAcier.Name = "pan_DefinitionAcier"
        Me.pan_DefinitionAcier.Size = New System.Drawing.Size(354, 468)
        Me.pan_DefinitionAcier.TabIndex = 2
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(354, 498)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'pan_Droite
        '
        Me.pan_Droite.AutoScroll = True
        Me.pan_Droite.Controls.Add(Me.TableLayoutPanel1)
        Me.pan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Droite.Location = New System.Drawing.Point(653, 0)
        Me.pan_Droite.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Droite.Name = "pan_Droite"
        Me.pan_Droite.Size = New System.Drawing.Size(354, 498)
        Me.pan_Droite.TabIndex = 1
        '
        'TLpan_Saisie
        '
        Me.TLpan_Saisie.ColumnCount = 3
        Me.TLpan_Saisie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_Saisie.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
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
        Me.TLpan_Saisie.Size = New System.Drawing.Size(1007, 498)
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
        Me.pan_Gauche.Size = New System.Drawing.Size(353, 498)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.lbl_WeldedPlate, 0, 2)
        Me.TLpan_Gauche.Controls.Add(Me.Pan_DimPRS, 0, 3)
        Me.TLpan_Gauche.Controls.Add(Me.pan_Lamine, 0, 1)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_ParentProfile, 0, 0)
        Me.TLpan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLpan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Gauche.Name = "TLpan_Gauche"
        Me.TLpan_Gauche.RowCount = 4
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 290.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TLpan_Gauche.Size = New System.Drawing.Size(353, 498)
        Me.TLpan_Gauche.TabIndex = 0
        '
        'lbl_WeldedPlate
        '
        Me.lbl_WeldedPlate.AutoSize = True
        Me.lbl_WeldedPlate.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_WeldedPlate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_WeldedPlate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_WeldedPlate.Location = New System.Drawing.Point(0, 321)
        Me.lbl_WeldedPlate.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.lbl_WeldedPlate.Name = "lbl_WeldedPlate"
        Me.lbl_WeldedPlate.Size = New System.Drawing.Size(353, 29)
        Me.lbl_WeldedPlate.TabIndex = 5
        Me.lbl_WeldedPlate.Text = "lbl_WeldedPlate"
        Me.lbl_WeldedPlate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.lbl_ParentProfile.Size = New System.Drawing.Size(353, 30)
        Me.lbl_ParentProfile.TabIndex = 1
        Me.lbl_ParentProfile.Text = "lbl_ParentProfile"
        Me.lbl_ParentProfile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLpan_Saisie)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(1007, 498)
        Me.pan_Main.TabIndex = 1
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
        'pan_Provi
        '
        Me.pan_Provi.Location = New System.Drawing.Point(636, 3)
        Me.pan_Provi.Name = "pan_Provi"
        Me.pan_Provi.Size = New System.Drawing.Size(261, 28)
        Me.pan_Provi.TabIndex = 2
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
        Me.TLPan_PartieBasse.Controls.Add(Me.pan_Provi, 4, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 507)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1007, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
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
        Me.TLpan_Main.Size = New System.Drawing.Size(1013, 544)
        Me.TLpan_Main.TabIndex = 0
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(1013, 544)
        Me.pan_General.TabIndex = 4
        '
        'imgList_UY
        '
        Me.imgList_UY.ImageStream = CType(resources.GetObject("imgList_UY.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList_UY.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList_UY.Images.SetKeyName(0, "Fu")
        Me.imgList_UY.Images.SetKeyName(1, "Fy")
        '
        'Frm_SectionSFB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(1013, 544)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_SectionSFB"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_SectionSFB"
        CType(Me.img_Section, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Lamine.ResumeLayout(False)
        Me.pan_Lamine.PerformLayout()
        CType(Me.Grid_ProfilesSup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.pan_Acier.ResumeLayout(False)
        CType(Me.img_ReductionCurve, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Pan_DimPRS.ResumeLayout(False)
        Me.Pan_DimPRS.PerformLayout()
        CType(Me.img_tpSFB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_bpSFB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridAciers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_DefinitionAcier.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.pan_Droite.ResumeLayout(False)
        Me.TLpan_Saisie.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.PerformLayout()
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.pan_General.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents img_Section As PictureBox
    Friend WithEvents pan_Lamine As Panel
    Friend WithEvents lbl_Profiles As Label
    Friend WithEvents Grid_ProfilesSup As DataGridView
    Friend WithEvents Col_ListeSup As DataGridViewTextBoxColumn
    Friend WithEvents Col_HISTARSup As DataGridViewTextBoxColumn
    Friend WithEvents lbl_Gamme As Label
    Friend WithEvents lst_GammeS As ListBox
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Provi As Panel
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Saisie As TableLayoutPanel
    Friend WithEvents pan_Droite As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lbl_Acier As Label
    Friend WithEvents pan_DefinitionAcier As Panel
    Friend WithEvents lbl_ReductionCurve As Label
    Friend WithEvents lbl_Qualite As Label
    Friend WithEvents lbl_Grade As Label
    Friend WithEvents GridAciers As DataGridView
    Friend WithEvents Col_Grade As DataGridViewTextBoxColumn
    Friend WithEvents Col_Qualite As DataGridViewTextBoxColumn
    Friend WithEvents Col_ReductionCurve As DataGridViewTextBoxColumn
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents Pan_DimPRS As Panel
    Friend WithEvents lbl_ParentProfile As Label
    Friend WithEvents lbl_WeldedPlate As Label
    Friend WithEvents lbl_WidthSFB As Label
    Friend WithEvents img_bpSFB As PictureBox
    Friend WithEvents txt_bpSFB As TextBox
    Friend WithEvents etq_UnitDim1SFB As Label
    Friend WithEvents etq_UnitDim2SFB As Label
    Friend WithEvents txt_tpSFB As TextBox
    Friend WithEvents img_tpSFB As PictureBox
    Friend WithEvents lbl_ThicknessSFB As Label
    Friend WithEvents lbl_WPSteel As Label
    Friend WithEvents cmb_ReductionCurveWP As ComboBox
    Friend WithEvents cmb_GradeWP As ComboBox
    Friend WithEvents lbl_InfoFyWP As Label
    Friend WithEvents cmb_NuancePlat As ComboBox
    Friend WithEvents pan_Acier As Panel
    Friend WithEvents btn_FyFu As Button
    Friend WithEvents img_ReductionCurve As PictureBox
    Friend WithEvents imgList_UY As ImageList
End Class
