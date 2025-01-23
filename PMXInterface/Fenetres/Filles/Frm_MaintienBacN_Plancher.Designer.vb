<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MaintienBacN_Plancher
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
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Options = New System.Windows.Forms.Label()
        Me.lbl_Panneau = New System.Windows.Forms.Label()
        Me.lbl_Floor = New System.Windows.Forms.Label()
        Me.pan_DefPlancher = New System.Windows.Forms.Panel()
        Me.lbl_EntraxeD = New System.Windows.Forms.Label()
        Me.txt_EntraxeD = New System.Windows.Forms.TextBox()
        Me.etq_UnitL5 = New System.Windows.Forms.Label()
        Me.cmb_Transition = New System.Windows.Forms.ComboBox()
        Me.lbl_Transition = New System.Windows.Forms.Label()
        Me.txt_NbSheetsTransverse = New System.Windows.Forms.TextBox()
        Me.lbl_NbSheetsTransverse = New System.Windows.Forms.Label()
        Me.pan_DimPlancher = New System.Windows.Forms.Panel()
        Me.lbl_Largeur = New System.Windows.Forms.Label()
        Me.lbl_DimensionsGlobales = New System.Windows.Forms.Label()
        Me.lbl_Portee = New System.Windows.Forms.Label()
        Me.txt_LongueurP = New System.Windows.Forms.TextBox()
        Me.etq_UnitL2 = New System.Windows.Forms.Label()
        Me.etq_UnitL1 = New System.Windows.Forms.Label()
        Me.txt_LargeurP = New System.Windows.Forms.TextBox()
        Me.pan_Panneau = New System.Windows.Forms.Panel()
        Me.cmb_NbSpan = New System.Windows.Forms.ComboBox()
        Me.lbl_NbSpans = New System.Windows.Forms.Label()
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
        Me.pan_Options = New System.Windows.Forms.Panel()
        Me.chk_Theta = New System.Windows.Forms.CheckBox()
        Me.chk_PriseEnCompteBac = New System.Windows.Forms.CheckBox()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.img_m = New System.Windows.Forms.PictureBox()
        Me.img_nt = New System.Windows.Forms.PictureBox()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_DefPlancher.SuspendLayout()
        Me.pan_DimPlancher.SuspendLayout()
        Me.pan_Panneau.SuspendLayout()
        Me.pan_BacIndividuel.SuspendLayout()
        CType(Me.img_bp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Options.SuspendLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_m, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_nt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.AutoScroll = True
        Me.pan_Main.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Main.Location = New System.Drawing.Point(135, 68)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(300, 524)
        Me.pan_Main.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.AutoScroll = True
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Options, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Panneau, 0, 4)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Floor, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.pan_DefPlancher, 0, 3)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Panneau, 0, 5)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Options, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 7
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 165.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(300, 451)
        Me.TLPan_Gauche.TabIndex = 1
        '
        'lbl_Options
        '
        Me.lbl_Options.AutoSize = True
        Me.lbl_Options.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Options.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Options.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Options.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Options.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Options.Name = "lbl_Options"
        Me.lbl_Options.Size = New System.Drawing.Size(300, 30)
        Me.lbl_Options.TabIndex = 4
        Me.lbl_Options.Text = "lbl_Options"
        Me.lbl_Options.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Panneau
        '
        Me.lbl_Panneau.AutoSize = True
        Me.lbl_Panneau.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Panneau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Panneau.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Panneau.Location = New System.Drawing.Point(0, 305)
        Me.lbl_Panneau.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Panneau.Name = "lbl_Panneau"
        Me.lbl_Panneau.Size = New System.Drawing.Size(300, 30)
        Me.lbl_Panneau.TabIndex = 2
        Me.lbl_Panneau.Text = "lbl_Panneau"
        Me.lbl_Panneau.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Floor
        '
        Me.lbl_Floor.AutoSize = True
        Me.lbl_Floor.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Floor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Floor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Floor.Location = New System.Drawing.Point(0, 110)
        Me.lbl_Floor.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Floor.Name = "lbl_Floor"
        Me.lbl_Floor.Size = New System.Drawing.Size(300, 30)
        Me.lbl_Floor.TabIndex = 0
        Me.lbl_Floor.Text = "lbl_Floor"
        Me.lbl_Floor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_DefPlancher
        '
        Me.pan_DefPlancher.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_DefPlancher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_DefPlancher.Controls.Add(Me.img_nt)
        Me.pan_DefPlancher.Controls.Add(Me.lbl_EntraxeD)
        Me.pan_DefPlancher.Controls.Add(Me.txt_EntraxeD)
        Me.pan_DefPlancher.Controls.Add(Me.etq_UnitL5)
        Me.pan_DefPlancher.Controls.Add(Me.cmb_Transition)
        Me.pan_DefPlancher.Controls.Add(Me.lbl_Transition)
        Me.pan_DefPlancher.Controls.Add(Me.txt_NbSheetsTransverse)
        Me.pan_DefPlancher.Controls.Add(Me.lbl_NbSheetsTransverse)
        Me.pan_DefPlancher.Controls.Add(Me.pan_DimPlancher)
        Me.pan_DefPlancher.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_DefPlancher.Location = New System.Drawing.Point(0, 140)
        Me.pan_DefPlancher.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_DefPlancher.Name = "pan_DefPlancher"
        Me.pan_DefPlancher.Size = New System.Drawing.Size(300, 164)
        Me.pan_DefPlancher.TabIndex = 1
        '
        'lbl_EntraxeD
        '
        Me.lbl_EntraxeD.AutoSize = True
        Me.lbl_EntraxeD.Location = New System.Drawing.Point(7, 36)
        Me.lbl_EntraxeD.Name = "lbl_EntraxeD"
        Me.lbl_EntraxeD.Size = New System.Drawing.Size(67, 13)
        Me.lbl_EntraxeD.TabIndex = 78
        Me.lbl_EntraxeD.Text = "lbl_EntraxeD"
        '
        'txt_EntraxeD
        '
        Me.txt_EntraxeD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EntraxeD.Location = New System.Drawing.Point(197, 33)
        Me.txt_EntraxeD.Name = "txt_EntraxeD"
        Me.txt_EntraxeD.Size = New System.Drawing.Size(58, 20)
        Me.txt_EntraxeD.TabIndex = 76
        Me.txt_EntraxeD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitL5
        '
        Me.etq_UnitL5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL5.AutoSize = True
        Me.etq_UnitL5.Location = New System.Drawing.Point(261, 37)
        Me.etq_UnitL5.Name = "etq_UnitL5"
        Me.etq_UnitL5.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL5.TabIndex = 77
        Me.etq_UnitL5.Text = "kN"
        '
        'cmb_Transition
        '
        Me.cmb_Transition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Transition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Transition.FormattingEnabled = True
        Me.cmb_Transition.Location = New System.Drawing.Point(6, 137)
        Me.cmb_Transition.Name = "cmb_Transition"
        Me.cmb_Transition.Size = New System.Drawing.Size(287, 21)
        Me.cmb_Transition.TabIndex = 6
        '
        'lbl_Transition
        '
        Me.lbl_Transition.AutoSize = True
        Me.lbl_Transition.Location = New System.Drawing.Point(8, 122)
        Me.lbl_Transition.Name = "lbl_Transition"
        Me.lbl_Transition.Size = New System.Drawing.Size(69, 13)
        Me.lbl_Transition.TabIndex = 5
        Me.lbl_Transition.Text = "lbl_Transition"
        '
        'txt_NbSheetsTransverse
        '
        Me.txt_NbSheetsTransverse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_NbSheetsTransverse.Location = New System.Drawing.Point(197, 9)
        Me.txt_NbSheetsTransverse.Name = "txt_NbSheetsTransverse"
        Me.txt_NbSheetsTransverse.Size = New System.Drawing.Size(58, 20)
        Me.txt_NbSheetsTransverse.TabIndex = 1
        Me.txt_NbSheetsTransverse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_NbSheetsTransverse
        '
        Me.lbl_NbSheetsTransverse.Location = New System.Drawing.Point(8, 6)
        Me.lbl_NbSheetsTransverse.Name = "lbl_NbSheetsTransverse"
        Me.lbl_NbSheetsTransverse.Size = New System.Drawing.Size(153, 26)
        Me.lbl_NbSheetsTransverse.TabIndex = 0
        Me.lbl_NbSheetsTransverse.Text = "lbl_NbSheetsTransverse"
        '
        'pan_DimPlancher
        '
        Me.pan_DimPlancher.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_DimPlancher.Controls.Add(Me.lbl_Largeur)
        Me.pan_DimPlancher.Controls.Add(Me.lbl_DimensionsGlobales)
        Me.pan_DimPlancher.Controls.Add(Me.lbl_Portee)
        Me.pan_DimPlancher.Controls.Add(Me.txt_LongueurP)
        Me.pan_DimPlancher.Controls.Add(Me.etq_UnitL2)
        Me.pan_DimPlancher.Controls.Add(Me.etq_UnitL1)
        Me.pan_DimPlancher.Controls.Add(Me.txt_LargeurP)
        Me.pan_DimPlancher.Location = New System.Drawing.Point(1, 58)
        Me.pan_DimPlancher.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_DimPlancher.Name = "pan_DimPlancher"
        Me.pan_DimPlancher.Size = New System.Drawing.Size(292, 60)
        Me.pan_DimPlancher.TabIndex = 4
        '
        'lbl_Largeur
        '
        Me.lbl_Largeur.Location = New System.Drawing.Point(10, 43)
        Me.lbl_Largeur.Name = "lbl_Largeur"
        Me.lbl_Largeur.Size = New System.Drawing.Size(132, 13)
        Me.lbl_Largeur.TabIndex = 79
        Me.lbl_Largeur.Text = "lbl_Largeur"
        Me.lbl_Largeur.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        'lbl_Portee
        '
        Me.lbl_Portee.Location = New System.Drawing.Point(10, 22)
        Me.lbl_Portee.Name = "lbl_Portee"
        Me.lbl_Portee.Size = New System.Drawing.Size(132, 13)
        Me.lbl_Portee.TabIndex = 78
        Me.lbl_Portee.Text = "lbl_Portee"
        Me.lbl_Portee.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_LongueurP
        '
        Me.txt_LongueurP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_LongueurP.Location = New System.Drawing.Point(196, 18)
        Me.txt_LongueurP.Name = "txt_LongueurP"
        Me.txt_LongueurP.Size = New System.Drawing.Size(58, 20)
        Me.txt_LongueurP.TabIndex = 73
        Me.txt_LongueurP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitL2
        '
        Me.etq_UnitL2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL2.AutoSize = True
        Me.etq_UnitL2.Location = New System.Drawing.Point(260, 43)
        Me.etq_UnitL2.Name = "etq_UnitL2"
        Me.etq_UnitL2.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL2.TabIndex = 77
        Me.etq_UnitL2.Text = "kN"
        '
        'etq_UnitL1
        '
        Me.etq_UnitL1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL1.AutoSize = True
        Me.etq_UnitL1.Location = New System.Drawing.Point(260, 22)
        Me.etq_UnitL1.Name = "etq_UnitL1"
        Me.etq_UnitL1.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL1.TabIndex = 75
        Me.etq_UnitL1.Text = "kN"
        '
        'txt_LargeurP
        '
        Me.txt_LargeurP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_LargeurP.Location = New System.Drawing.Point(196, 39)
        Me.txt_LargeurP.Name = "txt_LargeurP"
        Me.txt_LargeurP.Size = New System.Drawing.Size(58, 20)
        Me.txt_LargeurP.TabIndex = 76
        Me.txt_LargeurP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'pan_Panneau
        '
        Me.pan_Panneau.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Panneau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Panneau.Controls.Add(Me.img_m)
        Me.pan_Panneau.Controls.Add(Me.cmb_NbSpan)
        Me.pan_Panneau.Controls.Add(Me.lbl_NbSpans)
        Me.pan_Panneau.Controls.Add(Me.pan_BacIndividuel)
        Me.pan_Panneau.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Panneau.Location = New System.Drawing.Point(0, 335)
        Me.pan_Panneau.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Panneau.Name = "pan_Panneau"
        Me.pan_Panneau.Size = New System.Drawing.Size(300, 115)
        Me.pan_Panneau.TabIndex = 3
        '
        'cmb_NbSpan
        '
        Me.cmb_NbSpan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_NbSpan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbSpan.FormattingEnabled = True
        Me.cmb_NbSpan.Location = New System.Drawing.Point(197, 10)
        Me.cmb_NbSpan.Name = "cmb_NbSpan"
        Me.cmb_NbSpan.Size = New System.Drawing.Size(75, 21)
        Me.cmb_NbSpan.TabIndex = 5
        '
        'lbl_NbSpans
        '
        Me.lbl_NbSpans.Location = New System.Drawing.Point(8, 10)
        Me.lbl_NbSpans.Name = "lbl_NbSpans"
        Me.lbl_NbSpans.Size = New System.Drawing.Size(153, 31)
        Me.lbl_NbSpans.TabIndex = 3
        Me.lbl_NbSpans.Text = "lbl_NbSpans"
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
        Me.pan_BacIndividuel.Location = New System.Drawing.Point(1, 43)
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
        'pan_Options
        '
        Me.pan_Options.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Options.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Options.Controls.Add(Me.chk_Theta)
        Me.pan_Options.Controls.Add(Me.chk_PriseEnCompteBac)
        Me.pan_Options.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Options.Location = New System.Drawing.Point(0, 30)
        Me.pan_Options.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Options.Name = "pan_Options"
        Me.pan_Options.Size = New System.Drawing.Size(300, 79)
        Me.pan_Options.TabIndex = 5
        '
        'chk_Theta
        '
        Me.chk_Theta.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chk_Theta.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chk_Theta.Location = New System.Drawing.Point(11, 35)
        Me.chk_Theta.Name = "chk_Theta"
        Me.chk_Theta.Size = New System.Drawing.Size(282, 34)
        Me.chk_Theta.TabIndex = 121
        Me.chk_Theta.Text = "chk_Theta"
        Me.chk_Theta.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chk_Theta.UseVisualStyleBackColor = True
        '
        'chk_PriseEnCompteBac
        '
        Me.chk_PriseEnCompteBac.AutoSize = True
        Me.chk_PriseEnCompteBac.Location = New System.Drawing.Point(11, 12)
        Me.chk_PriseEnCompteBac.Name = "chk_PriseEnCompteBac"
        Me.chk_PriseEnCompteBac.Size = New System.Drawing.Size(141, 17)
        Me.chk_PriseEnCompteBac.TabIndex = 7
        Me.chk_PriseEnCompteBac.Text = "chk_PriseEnCompteBac"
        Me.chk_PriseEnCompteBac.UseVisualStyleBackColor = True
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'img_m
        '
        Me.img_m.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_m.Location = New System.Drawing.Point(160, 11)
        Me.img_m.Name = "img_m"
        Me.img_m.Size = New System.Drawing.Size(37, 20)
        Me.img_m.TabIndex = 95
        Me.img_m.TabStop = False
        '
        'img_nt
        '
        Me.img_nt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_nt.Location = New System.Drawing.Point(160, 9)
        Me.img_nt.Name = "img_nt"
        Me.img_nt.Size = New System.Drawing.Size(37, 20)
        Me.img_nt.TabIndex = 96
        Me.img_nt.TabStop = False
        '
        'Frm_MaintienBacN_Plancher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 622)
        Me.Controls.Add(Me.pan_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_MaintienBacN_Plancher"
        Me.Text = "Frm_MaintienBacN_Plancher"
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_DefPlancher.ResumeLayout(False)
        Me.pan_DefPlancher.PerformLayout()
        Me.pan_DimPlancher.ResumeLayout(False)
        Me.pan_DimPlancher.PerformLayout()
        Me.pan_Panneau.ResumeLayout(False)
        Me.pan_BacIndividuel.ResumeLayout(False)
        Me.pan_BacIndividuel.PerformLayout()
        CType(Me.img_bp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Options.ResumeLayout(False)
        Me.pan_Options.PerformLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_m, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_nt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Panneau As Label
    Friend WithEvents lbl_Floor As Label
    Friend WithEvents pan_DefPlancher As Panel
    Friend WithEvents chk_PriseEnCompteBac As CheckBox
    Friend WithEvents cmb_Transition As ComboBox
    Friend WithEvents lbl_Transition As Label
    Friend WithEvents txt_NbSheetsTransverse As TextBox
    Friend WithEvents lbl_NbSheetsTransverse As Label
    Friend WithEvents pan_DimPlancher As Panel
    Friend WithEvents lbl_Largeur As Label
    Friend WithEvents lbl_DimensionsGlobales As Label
    Friend WithEvents lbl_Portee As Label
    Friend WithEvents txt_LongueurP As TextBox
    Friend WithEvents etq_UnitL2 As Label
    Friend WithEvents etq_UnitL1 As Label
    Friend WithEvents txt_LargeurP As TextBox
    Friend WithEvents pan_Panneau As Panel
    Friend WithEvents cmb_NbSpan As ComboBox
    Friend WithEvents lbl_NbSpans As Label
    Friend WithEvents pan_BacIndividuel As Panel
    Friend WithEvents lbl_SheetWidth As Label
    Friend WithEvents lbl_SheetLength As Label
    Friend WithEvents etq_UnitL4 As Label
    Friend WithEvents txt_SheetWidth As TextBox
    Friend WithEvents etq_UnitL3 As Label
    Friend WithEvents txt_SheetLength As TextBox
    Friend WithEvents lbl_IndSheetDimensions As Label
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents img_bp As PictureBox
    Friend WithEvents img_ap As PictureBox
    Friend WithEvents lbl_Options As Label
    Friend WithEvents pan_Options As Panel
    Friend WithEvents chk_Theta As CheckBox
    Friend WithEvents lbl_EntraxeD As Label
    Friend WithEvents txt_EntraxeD As TextBox
    Friend WithEvents etq_UnitL5 As Label
    Friend WithEvents img_m As PictureBox
    Friend WithEvents img_nt As PictureBox
End Class
