<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MaintienBacN_Tole
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_MaintienBacN_Tole))
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_ToleK = New System.Windows.Forms.Label()
        Me.lbl_Panneau = New System.Windows.Forms.Label()
        Me.pan_Panneau = New System.Windows.Forms.Panel()
        Me.lbl_Fu = New System.Windows.Forms.Label()
        Me.img_Fup = New System.Windows.Forms.PictureBox()
        Me.etq_UnitF1 = New System.Windows.Forms.Label()
        Me.txt_Fup = New System.Windows.Forms.TextBox()
        Me.lbl_NbSpans = New System.Windows.Forms.Label()
        Me.lbl_Tpr = New System.Windows.Forms.Label()
        Me.img_Tpr = New System.Windows.Forms.PictureBox()
        Me.etq_UnitD1 = New System.Windows.Forms.Label()
        Me.txt_Tpr = New System.Windows.Forms.TextBox()
        Me.img_m = New System.Windows.Forms.PictureBox()
        Me.cmb_NbSpan = New System.Windows.Forms.ComboBox()
        Me.pan_BacIndividuel = New System.Windows.Forms.Panel()
        Me.img_bp = New System.Windows.Forms.PictureBox()
        Me.img_ap = New System.Windows.Forms.PictureBox()
        Me.lbl_SheetWidth = New System.Windows.Forms.Label()
        Me.lbl_SheetLength = New System.Windows.Forms.Label()
        Me.etq_UnitL4 = New System.Windows.Forms.Label()
        Me.txt_SheetWidth = New System.Windows.Forms.TextBox()
        Me.etq_UnitL3 = New System.Windows.Forms.Label()
        Me.txt_SheetLength = New System.Windows.Forms.TextBox()
        Me.lbl_IndSheetDimensions = New System.Windows.Forms.Label()
        Me.pan_ToleK = New System.Windows.Forms.Panel()
        Me.img_info = New System.Windows.Forms.PictureBox()
        Me.lbl_Explication_2 = New System.Windows.Forms.Label()
        Me.lbl_Explication_1 = New System.Windows.Forms.Label()
        Me.img_K = New System.Windows.Forms.PictureBox()
        Me.txt_ToleK = New System.Windows.Forms.TextBox()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.etq_UnitK2 = New System.Windows.Forms.Label()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_Panneau.SuspendLayout()
        CType(Me.img_Fup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Tpr, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_m, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_BacIndividuel.SuspendLayout()
        CType(Me.img_bp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_ToleK.SuspendLayout()
        CType(Me.img_info, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_K, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.AutoScroll = True
        Me.pan_Main.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Main.Location = New System.Drawing.Point(250, 12)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(300, 386)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.AutoScroll = True
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_ToleK, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Panneau, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Panneau, 0, 1)
        Me.TLPan_Gauche.Controls.Add(Me.pan_ToleK, 0, 3)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 4
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 152.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(300, 371)
        Me.TLPan_Gauche.TabIndex = 1
        '
        'lbl_ToleK
        '
        Me.lbl_ToleK.AutoSize = True
        Me.lbl_ToleK.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_ToleK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ToleK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ToleK.Location = New System.Drawing.Point(0, 182)
        Me.lbl_ToleK.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ToleK.Name = "lbl_ToleK"
        Me.lbl_ToleK.Size = New System.Drawing.Size(300, 30)
        Me.lbl_ToleK.TabIndex = 4
        Me.lbl_ToleK.Text = "lbl_ToleK"
        Me.lbl_ToleK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Panneau
        '
        Me.lbl_Panneau.AutoSize = True
        Me.lbl_Panneau.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Panneau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Panneau.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Panneau.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Panneau.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Panneau.Name = "lbl_Panneau"
        Me.lbl_Panneau.Size = New System.Drawing.Size(300, 30)
        Me.lbl_Panneau.TabIndex = 2
        Me.lbl_Panneau.Text = "lbl_Panneau"
        Me.lbl_Panneau.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Panneau
        '
        Me.pan_Panneau.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Panneau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Panneau.Controls.Add(Me.lbl_Fu)
        Me.pan_Panneau.Controls.Add(Me.img_Fup)
        Me.pan_Panneau.Controls.Add(Me.etq_UnitF1)
        Me.pan_Panneau.Controls.Add(Me.txt_Fup)
        Me.pan_Panneau.Controls.Add(Me.lbl_NbSpans)
        Me.pan_Panneau.Controls.Add(Me.lbl_Tpr)
        Me.pan_Panneau.Controls.Add(Me.img_Tpr)
        Me.pan_Panneau.Controls.Add(Me.etq_UnitD1)
        Me.pan_Panneau.Controls.Add(Me.txt_Tpr)
        Me.pan_Panneau.Controls.Add(Me.img_m)
        Me.pan_Panneau.Controls.Add(Me.cmb_NbSpan)
        Me.pan_Panneau.Controls.Add(Me.pan_BacIndividuel)
        Me.pan_Panneau.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Panneau.Location = New System.Drawing.Point(0, 30)
        Me.pan_Panneau.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Panneau.Name = "pan_Panneau"
        Me.pan_Panneau.Size = New System.Drawing.Size(300, 151)
        Me.pan_Panneau.TabIndex = 3
        '
        'lbl_Fu
        '
        Me.lbl_Fu.AutoSize = True
        Me.lbl_Fu.Location = New System.Drawing.Point(8, 128)
        Me.lbl_Fu.Name = "lbl_Fu"
        Me.lbl_Fu.Size = New System.Drawing.Size(39, 13)
        Me.lbl_Fu.TabIndex = 104
        Me.lbl_Fu.Text = "Label1"
        '
        'img_Fup
        '
        Me.img_Fup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Fup.Location = New System.Drawing.Point(160, 125)
        Me.img_Fup.Name = "img_Fup"
        Me.img_Fup.Size = New System.Drawing.Size(37, 20)
        Me.img_Fup.TabIndex = 103
        Me.img_Fup.TabStop = False
        '
        'etq_UnitF1
        '
        Me.etq_UnitF1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitF1.AutoSize = True
        Me.etq_UnitF1.Location = New System.Drawing.Point(261, 129)
        Me.etq_UnitF1.Name = "etq_UnitF1"
        Me.etq_UnitF1.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitF1.TabIndex = 102
        Me.etq_UnitF1.Text = "kN"
        '
        'txt_Fup
        '
        Me.txt_Fup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Fup.Location = New System.Drawing.Point(197, 125)
        Me.txt_Fup.Name = "txt_Fup"
        Me.txt_Fup.Size = New System.Drawing.Size(58, 20)
        Me.txt_Fup.TabIndex = 101
        Me.txt_Fup.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_NbSpans
        '
        Me.lbl_NbSpans.Location = New System.Drawing.Point(8, 5)
        Me.lbl_NbSpans.Name = "lbl_NbSpans"
        Me.lbl_NbSpans.Size = New System.Drawing.Size(153, 31)
        Me.lbl_NbSpans.TabIndex = 3
        Me.lbl_NbSpans.Text = "lbl_NbSpans"
        '
        'lbl_Tpr
        '
        Me.lbl_Tpr.AutoSize = True
        Me.lbl_Tpr.Location = New System.Drawing.Point(8, 105)
        Me.lbl_Tpr.Name = "lbl_Tpr"
        Me.lbl_Tpr.Size = New System.Drawing.Size(39, 13)
        Me.lbl_Tpr.TabIndex = 100
        Me.lbl_Tpr.Text = "lbl_Tpr"
        '
        'img_Tpr
        '
        Me.img_Tpr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Tpr.Location = New System.Drawing.Point(160, 102)
        Me.img_Tpr.Name = "img_Tpr"
        Me.img_Tpr.Size = New System.Drawing.Size(37, 20)
        Me.img_Tpr.TabIndex = 99
        Me.img_Tpr.TabStop = False
        '
        'etq_UnitD1
        '
        Me.etq_UnitD1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitD1.AutoSize = True
        Me.etq_UnitD1.Location = New System.Drawing.Point(261, 106)
        Me.etq_UnitD1.Name = "etq_UnitD1"
        Me.etq_UnitD1.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitD1.TabIndex = 97
        Me.etq_UnitD1.Text = "kN"
        '
        'txt_Tpr
        '
        Me.txt_Tpr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Tpr.Location = New System.Drawing.Point(197, 102)
        Me.txt_Tpr.Name = "txt_Tpr"
        Me.txt_Tpr.Size = New System.Drawing.Size(58, 20)
        Me.txt_Tpr.TabIndex = 96
        Me.txt_Tpr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'img_m
        '
        Me.img_m.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_m.Location = New System.Drawing.Point(160, 6)
        Me.img_m.Name = "img_m"
        Me.img_m.Size = New System.Drawing.Size(37, 20)
        Me.img_m.TabIndex = 95
        Me.img_m.TabStop = False
        '
        'cmb_NbSpan
        '
        Me.cmb_NbSpan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_NbSpan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbSpan.FormattingEnabled = True
        Me.cmb_NbSpan.Location = New System.Drawing.Point(197, 5)
        Me.cmb_NbSpan.Name = "cmb_NbSpan"
        Me.cmb_NbSpan.Size = New System.Drawing.Size(75, 21)
        Me.cmb_NbSpan.TabIndex = 5
        '
        'pan_BacIndividuel
        '
        Me.pan_BacIndividuel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_BacIndividuel.Controls.Add(Me.img_bp)
        Me.pan_BacIndividuel.Controls.Add(Me.img_ap)
        Me.pan_BacIndividuel.Controls.Add(Me.lbl_SheetWidth)
        Me.pan_BacIndividuel.Controls.Add(Me.lbl_SheetLength)
        Me.pan_BacIndividuel.Controls.Add(Me.etq_UnitL4)
        Me.pan_BacIndividuel.Controls.Add(Me.txt_SheetWidth)
        Me.pan_BacIndividuel.Controls.Add(Me.etq_UnitL3)
        Me.pan_BacIndividuel.Controls.Add(Me.txt_SheetLength)
        Me.pan_BacIndividuel.Controls.Add(Me.lbl_IndSheetDimensions)
        Me.pan_BacIndividuel.Location = New System.Drawing.Point(1, 37)
        Me.pan_BacIndividuel.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_BacIndividuel.Name = "pan_BacIndividuel"
        Me.pan_BacIndividuel.Size = New System.Drawing.Size(292, 62)
        Me.pan_BacIndividuel.TabIndex = 5
        '
        'img_bp
        '
        Me.img_bp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_bp.Location = New System.Drawing.Point(159, 38)
        Me.img_bp.Name = "img_bp"
        Me.img_bp.Size = New System.Drawing.Size(37, 20)
        Me.img_bp.TabIndex = 89
        Me.img_bp.TabStop = False
        '
        'img_ap
        '
        Me.img_ap.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ap.Location = New System.Drawing.Point(159, 16)
        Me.img_ap.Name = "img_ap"
        Me.img_ap.Size = New System.Drawing.Size(37, 20)
        Me.img_ap.TabIndex = 94
        Me.img_ap.TabStop = False
        '
        'lbl_SheetWidth
        '
        Me.lbl_SheetWidth.Location = New System.Drawing.Point(6, 42)
        Me.lbl_SheetWidth.Name = "lbl_SheetWidth"
        Me.lbl_SheetWidth.Size = New System.Drawing.Size(136, 13)
        Me.lbl_SheetWidth.TabIndex = 93
        Me.lbl_SheetWidth.Text = "lbl_SheetWidth"
        Me.lbl_SheetWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_SheetLength
        '
        Me.lbl_SheetLength.Location = New System.Drawing.Point(6, 20)
        Me.lbl_SheetLength.Name = "lbl_SheetLength"
        Me.lbl_SheetLength.Size = New System.Drawing.Size(136, 13)
        Me.lbl_SheetLength.TabIndex = 92
        Me.lbl_SheetLength.Text = "lbl_SheetLength"
        Me.lbl_SheetLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitL4
        '
        Me.etq_UnitL4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL4.AutoSize = True
        Me.etq_UnitL4.Location = New System.Drawing.Point(260, 42)
        Me.etq_UnitL4.Name = "etq_UnitL4"
        Me.etq_UnitL4.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL4.TabIndex = 91
        Me.etq_UnitL4.Text = "kN"
        '
        'txt_SheetWidth
        '
        Me.txt_SheetWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_SheetWidth.Location = New System.Drawing.Point(196, 38)
        Me.txt_SheetWidth.Name = "txt_SheetWidth"
        Me.txt_SheetWidth.Size = New System.Drawing.Size(58, 20)
        Me.txt_SheetWidth.TabIndex = 90
        Me.txt_SheetWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitL3
        '
        Me.etq_UnitL3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL3.AutoSize = True
        Me.etq_UnitL3.Location = New System.Drawing.Point(260, 20)
        Me.etq_UnitL3.Name = "etq_UnitL3"
        Me.etq_UnitL3.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL3.TabIndex = 89
        Me.etq_UnitL3.Text = "kN"
        '
        'txt_SheetLength
        '
        Me.txt_SheetLength.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_SheetLength.Location = New System.Drawing.Point(196, 16)
        Me.txt_SheetLength.Name = "txt_SheetLength"
        Me.txt_SheetLength.Size = New System.Drawing.Size(58, 20)
        Me.txt_SheetLength.TabIndex = 88
        Me.txt_SheetLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        'pan_ToleK
        '
        Me.pan_ToleK.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ToleK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ToleK.Controls.Add(Me.img_info)
        Me.pan_ToleK.Controls.Add(Me.lbl_Explication_2)
        Me.pan_ToleK.Controls.Add(Me.lbl_Explication_1)
        Me.pan_ToleK.Controls.Add(Me.img_K)
        Me.pan_ToleK.Controls.Add(Me.txt_ToleK)
        Me.pan_ToleK.Controls.Add(Me.etq_UnitK2)
        Me.pan_ToleK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ToleK.Location = New System.Drawing.Point(0, 212)
        Me.pan_ToleK.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ToleK.Name = "pan_ToleK"
        Me.pan_ToleK.Size = New System.Drawing.Size(300, 159)
        Me.pan_ToleK.TabIndex = 5
        '
        'img_info
        '
        Me.img_info.Image = CType(resources.GetObject("img_info.Image"), System.Drawing.Image)
        Me.img_info.Location = New System.Drawing.Point(273, 129)
        Me.img_info.Margin = New System.Windows.Forms.Padding(0)
        Me.img_info.Name = "img_info"
        Me.img_info.Size = New System.Drawing.Size(20, 20)
        Me.img_info.TabIndex = 81
        Me.img_info.TabStop = False
        '
        'lbl_Explication_2
        '
        Me.lbl_Explication_2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Explication_2.Location = New System.Drawing.Point(7, 69)
        Me.lbl_Explication_2.Name = "lbl_Explication_2"
        Me.lbl_Explication_2.Size = New System.Drawing.Size(285, 51)
        Me.lbl_Explication_2.TabIndex = 132
        Me.lbl_Explication_2.Text = "lbl_Explication_2"
        '
        'lbl_Explication_1
        '
        Me.lbl_Explication_1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Explication_1.Location = New System.Drawing.Point(8, 7)
        Me.lbl_Explication_1.Name = "lbl_Explication_1"
        Me.lbl_Explication_1.Size = New System.Drawing.Size(284, 51)
        Me.lbl_Explication_1.TabIndex = 131
        Me.lbl_Explication_1.Text = "lbl_Explication_1"
        '
        'img_K
        '
        Me.img_K.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_K.Location = New System.Drawing.Point(159, 129)
        Me.img_K.Name = "img_K"
        Me.img_K.Size = New System.Drawing.Size(37, 20)
        Me.img_K.TabIndex = 105
        Me.img_K.TabStop = False
        '
        'txt_ToleK
        '
        Me.txt_ToleK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ToleK.Location = New System.Drawing.Point(196, 129)
        Me.txt_ToleK.Name = "txt_ToleK"
        Me.txt_ToleK.Size = New System.Drawing.Size(58, 20)
        Me.txt_ToleK.TabIndex = 104
        Me.txt_ToleK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'etq_UnitK2
        '
        Me.etq_UnitK2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitK2.AutoSize = True
        Me.etq_UnitK2.Location = New System.Drawing.Point(259, 132)
        Me.etq_UnitK2.Name = "etq_UnitK2"
        Me.etq_UnitK2.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitK2.TabIndex = 133
        Me.etq_UnitK2.Text = "mm"
        '
        'Frm_MaintienBacN_Tole
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 564)
        Me.Controls.Add(Me.pan_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_MaintienBacN_Tole"
        Me.Text = "Frm_MaintienBacN_Tole"
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_Panneau.ResumeLayout(False)
        Me.pan_Panneau.PerformLayout()
        CType(Me.img_Fup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Tpr, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_m, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_BacIndividuel.ResumeLayout(False)
        Me.pan_BacIndividuel.PerformLayout()
        CType(Me.img_bp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_ToleK.ResumeLayout(False)
        Me.pan_ToleK.PerformLayout()
        CType(Me.img_info, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_K, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Panneau As Label
    Friend WithEvents pan_Panneau As Panel
    Friend WithEvents lbl_Fu As Label
    Friend WithEvents img_Fup As PictureBox
    Friend WithEvents etq_UnitF1 As Label
    Friend WithEvents txt_Fup As TextBox
    Friend WithEvents lbl_NbSpans As Label
    Friend WithEvents lbl_Tpr As Label
    Friend WithEvents img_Tpr As PictureBox
    Friend WithEvents etq_UnitD1 As Label
    Friend WithEvents txt_Tpr As TextBox
    Friend WithEvents img_m As PictureBox
    Friend WithEvents cmb_NbSpan As ComboBox
    Friend WithEvents pan_BacIndividuel As Panel
    Friend WithEvents img_bp As PictureBox
    Friend WithEvents img_ap As PictureBox
    Friend WithEvents lbl_SheetWidth As Label
    Friend WithEvents lbl_SheetLength As Label
    Friend WithEvents etq_UnitL4 As Label
    Friend WithEvents txt_SheetWidth As TextBox
    Friend WithEvents etq_UnitL3 As Label
    Friend WithEvents txt_SheetLength As TextBox
    Friend WithEvents lbl_IndSheetDimensions As Label
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents lbl_ToleK As Label
    Friend WithEvents pan_ToleK As Panel
    Friend WithEvents img_K As PictureBox
    Friend WithEvents txt_ToleK As TextBox
    Friend WithEvents lbl_Explication_2 As Label
    Friend WithEvents lbl_Explication_1 As Label
    Friend WithEvents img_info As PictureBox
    Friend WithEvents etq_UnitK2 As Label
End Class
