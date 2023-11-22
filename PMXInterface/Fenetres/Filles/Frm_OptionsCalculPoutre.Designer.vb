<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_OptionsCalculPoutre
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
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Portees = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_CadreBeton = New System.Windows.Forms.Label()
        Me.pan_OptionsELS = New System.Windows.Forms.Panel()
        Me.lbl_CadreELS = New System.Windows.Forms.Label()
        Me.pan_OptionsELU = New System.Windows.Forms.Panel()
        Me.rdb_ElasticDesign = New System.Windows.Forms.RadioButton()
        Me.rdb_NormalDesign = New System.Windows.Forms.RadioButton()
        Me.lbl_CadreELU = New System.Windows.Forms.Label()
        Me.lbl_CadreNorm = New System.Windows.Forms.Label()
        Me.pan_Norm = New System.Windows.Forms.Panel()
        Me.cmb_Norme = New System.Windows.Forms.ComboBox()
        Me.lbl_Norme = New System.Windows.Forms.Label()
        Me.pan_Beton = New System.Windows.Forms.Panel()
        Me.etq_UnitModuleY = New System.Windows.Forms.Label()
        Me.txt_Es = New System.Windows.Forms.TextBox()
        Me.img_Es = New System.Windows.Forms.PictureBox()
        Me.lbl_ArmaYoung = New System.Windows.Forms.Label()
        Me.chk_RetraitEnrobage = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.etq_UnitEpsilon = New System.Windows.Forms.Label()
        Me.txt_EpsilonSh = New System.Windows.Forms.TextBox()
        Me.img_EpsilonSh = New System.Windows.Forms.PictureBox()
        Me.img_RH = New System.Windows.Forms.PictureBox()
        Me.lbl_Shrinkage = New System.Windows.Forms.Label()
        Me.cmb_RH = New System.Windows.Forms.ComboBox()
        Me.lbl_RH = New System.Windows.Forms.Label()
        Me.lbl_BetonMessage = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.lbl_CadreSections = New System.Windows.Forms.Label()
        Me.pan_Sections = New System.Windows.Forms.Panel()
        Me.chk_LargeursPartipantesSimples = New System.Windows.Forms.CheckBox()
        Me.chk_ArmaComprimees = New System.Windows.Forms.CheckBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Portees.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_OptionsELU.SuspendLayout()
        Me.pan_Norm.SuspendLayout()
        Me.pan_Beton.SuspendLayout()
        CType(Me.img_Es, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_EpsilonSh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_RH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Sections.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(465, 543)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(465, 543)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 506)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(459, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(242, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(102, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_Portees)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(459, 497)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Portees
        '
        Me.TLPan_Portees.ColumnCount = 1
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Portees.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Portees.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Portees.Name = "TLPan_Portees"
        Me.TLPan_Portees.RowCount = 1
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Size = New System.Drawing.Size(459, 497)
        Me.TLPan_Portees.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(459, 497)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.pan_Sections, 0, 9)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreSections, 0, 8)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreBeton, 0, 6)
        Me.TLPan_Gauche.Controls.Add(Me.pan_OptionsELS, 0, 5)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreELS, 0, 4)
        Me.TLPan_Gauche.Controls.Add(Me.pan_OptionsELU, 0, 3)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreELU, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreNorm, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Norm, 0, 1)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Beton, 0, 7)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 11
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(442, 757)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_CadreBeton
        '
        Me.lbl_CadreBeton.AutoSize = True
        Me.lbl_CadreBeton.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreBeton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreBeton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreBeton.Location = New System.Drawing.Point(0, 345)
        Me.lbl_CadreBeton.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreBeton.Name = "lbl_CadreBeton"
        Me.lbl_CadreBeton.Size = New System.Drawing.Size(442, 30)
        Me.lbl_CadreBeton.TabIndex = 6
        Me.lbl_CadreBeton.Text = "lbl_CadreBeton"
        Me.lbl_CadreBeton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_OptionsELS
        '
        Me.pan_OptionsELS.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_OptionsELS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_OptionsELS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_OptionsELS.Location = New System.Drawing.Point(0, 225)
        Me.pan_OptionsELS.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_OptionsELS.Name = "pan_OptionsELS"
        Me.pan_OptionsELS.Size = New System.Drawing.Size(442, 119)
        Me.pan_OptionsELS.TabIndex = 5
        '
        'lbl_CadreELS
        '
        Me.lbl_CadreELS.AutoSize = True
        Me.lbl_CadreELS.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreELS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreELS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreELS.Location = New System.Drawing.Point(0, 195)
        Me.lbl_CadreELS.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreELS.Name = "lbl_CadreELS"
        Me.lbl_CadreELS.Size = New System.Drawing.Size(442, 30)
        Me.lbl_CadreELS.TabIndex = 4
        Me.lbl_CadreELS.Text = "lbl_CadreELS"
        Me.lbl_CadreELS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_OptionsELU
        '
        Me.pan_OptionsELU.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_OptionsELU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_OptionsELU.Controls.Add(Me.rdb_ElasticDesign)
        Me.pan_OptionsELU.Controls.Add(Me.rdb_NormalDesign)
        Me.pan_OptionsELU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_OptionsELU.Location = New System.Drawing.Point(0, 115)
        Me.pan_OptionsELU.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_OptionsELU.Name = "pan_OptionsELU"
        Me.pan_OptionsELU.Size = New System.Drawing.Size(442, 79)
        Me.pan_OptionsELU.TabIndex = 3
        '
        'rdb_ElasticDesign
        '
        Me.rdb_ElasticDesign.AutoSize = True
        Me.rdb_ElasticDesign.Location = New System.Drawing.Point(21, 39)
        Me.rdb_ElasticDesign.Name = "rdb_ElasticDesign"
        Me.rdb_ElasticDesign.Size = New System.Drawing.Size(110, 17)
        Me.rdb_ElasticDesign.TabIndex = 1
        Me.rdb_ElasticDesign.TabStop = True
        Me.rdb_ElasticDesign.Text = "rdb_ElasticDesign"
        Me.rdb_ElasticDesign.UseVisualStyleBackColor = True
        '
        'rdb_NormalDesign
        '
        Me.rdb_NormalDesign.AutoSize = True
        Me.rdb_NormalDesign.Location = New System.Drawing.Point(21, 16)
        Me.rdb_NormalDesign.Name = "rdb_NormalDesign"
        Me.rdb_NormalDesign.Size = New System.Drawing.Size(112, 17)
        Me.rdb_NormalDesign.TabIndex = 0
        Me.rdb_NormalDesign.TabStop = True
        Me.rdb_NormalDesign.Text = "rbd_NormalDesign"
        Me.rdb_NormalDesign.UseVisualStyleBackColor = True
        '
        'lbl_CadreELU
        '
        Me.lbl_CadreELU.AutoSize = True
        Me.lbl_CadreELU.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreELU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreELU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreELU.Location = New System.Drawing.Point(0, 85)
        Me.lbl_CadreELU.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreELU.Name = "lbl_CadreELU"
        Me.lbl_CadreELU.Size = New System.Drawing.Size(442, 30)
        Me.lbl_CadreELU.TabIndex = 2
        Me.lbl_CadreELU.Text = "lbl_CadreELU"
        Me.lbl_CadreELU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_CadreNorm
        '
        Me.lbl_CadreNorm.AutoSize = True
        Me.lbl_CadreNorm.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreNorm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreNorm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreNorm.Location = New System.Drawing.Point(0, 0)
        Me.lbl_CadreNorm.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreNorm.Name = "lbl_CadreNorm"
        Me.lbl_CadreNorm.Size = New System.Drawing.Size(442, 30)
        Me.lbl_CadreNorm.TabIndex = 0
        Me.lbl_CadreNorm.Text = "lbl_CadreNorm"
        Me.lbl_CadreNorm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Norm
        '
        Me.pan_Norm.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Norm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Norm.Controls.Add(Me.cmb_Norme)
        Me.pan_Norm.Controls.Add(Me.lbl_Norme)
        Me.pan_Norm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Norm.Location = New System.Drawing.Point(0, 30)
        Me.pan_Norm.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Norm.Name = "pan_Norm"
        Me.pan_Norm.Size = New System.Drawing.Size(442, 54)
        Me.pan_Norm.TabIndex = 1
        '
        'cmb_Norme
        '
        Me.cmb_Norme.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Norme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Norme.FormattingEnabled = True
        Me.cmb_Norme.Location = New System.Drawing.Point(186, 15)
        Me.cmb_Norme.Name = "cmb_Norme"
        Me.cmb_Norme.Size = New System.Drawing.Size(240, 21)
        Me.cmb_Norme.TabIndex = 106
        '
        'lbl_Norme
        '
        Me.lbl_Norme.AutoSize = True
        Me.lbl_Norme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Norme.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl_Norme.Location = New System.Drawing.Point(8, 18)
        Me.lbl_Norme.Name = "lbl_Norme"
        Me.lbl_Norme.Size = New System.Drawing.Size(54, 13)
        Me.lbl_Norme.TabIndex = 105
        Me.lbl_Norme.Text = "lbl_Norme"
        Me.lbl_Norme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pan_Beton
        '
        Me.pan_Beton.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Beton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Beton.Controls.Add(Me.etq_UnitModuleY)
        Me.pan_Beton.Controls.Add(Me.txt_Es)
        Me.pan_Beton.Controls.Add(Me.img_Es)
        Me.pan_Beton.Controls.Add(Me.lbl_ArmaYoung)
        Me.pan_Beton.Controls.Add(Me.chk_RetraitEnrobage)
        Me.pan_Beton.Controls.Add(Me.Label1)
        Me.pan_Beton.Controls.Add(Me.etq_UnitEpsilon)
        Me.pan_Beton.Controls.Add(Me.txt_EpsilonSh)
        Me.pan_Beton.Controls.Add(Me.img_EpsilonSh)
        Me.pan_Beton.Controls.Add(Me.img_RH)
        Me.pan_Beton.Controls.Add(Me.lbl_Shrinkage)
        Me.pan_Beton.Controls.Add(Me.cmb_RH)
        Me.pan_Beton.Controls.Add(Me.lbl_RH)
        Me.pan_Beton.Controls.Add(Me.lbl_BetonMessage)
        Me.pan_Beton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Beton.Location = New System.Drawing.Point(0, 375)
        Me.pan_Beton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Beton.Name = "pan_Beton"
        Me.pan_Beton.Size = New System.Drawing.Size(442, 179)
        Me.pan_Beton.TabIndex = 7
        '
        'etq_UnitModuleY
        '
        Me.etq_UnitModuleY.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitModuleY.AutoSize = True
        Me.etq_UnitModuleY.Location = New System.Drawing.Point(305, 85)
        Me.etq_UnitModuleY.Name = "etq_UnitModuleY"
        Me.etq_UnitModuleY.Size = New System.Drawing.Size(28, 13)
        Me.etq_UnitModuleY.TabIndex = 118
        Me.etq_UnitModuleY.Text = "GPa"
        '
        'txt_Es
        '
        Me.txt_Es.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Es.Location = New System.Drawing.Point(241, 81)
        Me.txt_Es.Name = "txt_Es"
        Me.txt_Es.Size = New System.Drawing.Size(58, 20)
        Me.txt_Es.TabIndex = 117
        '
        'img_Es
        '
        Me.img_Es.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Es.Location = New System.Drawing.Point(193, 81)
        Me.img_Es.Name = "img_Es"
        Me.img_Es.Size = New System.Drawing.Size(46, 20)
        Me.img_Es.TabIndex = 116
        Me.img_Es.TabStop = False
        '
        'lbl_ArmaYoung
        '
        Me.lbl_ArmaYoung.AutoSize = True
        Me.lbl_ArmaYoung.Location = New System.Drawing.Point(8, 85)
        Me.lbl_ArmaYoung.Name = "lbl_ArmaYoung"
        Me.lbl_ArmaYoung.Size = New System.Drawing.Size(78, 13)
        Me.lbl_ArmaYoung.TabIndex = 115
        Me.lbl_ArmaYoung.Text = "lbl_ArmaYoung"
        '
        'chk_RetraitEnrobage
        '
        Me.chk_RetraitEnrobage.AutoSize = True
        Me.chk_RetraitEnrobage.Location = New System.Drawing.Point(375, 57)
        Me.chk_RetraitEnrobage.Name = "chk_RetraitEnrobage"
        Me.chk_RetraitEnrobage.Size = New System.Drawing.Size(127, 17)
        Me.chk_RetraitEnrobage.TabIndex = 114
        Me.chk_RetraitEnrobage.Text = "chk_RetraitEnrobage"
        Me.chk_RetraitEnrobage.UseVisualStyleBackColor = True
        Me.chk_RetraitEnrobage.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(328, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(13, 13)
        Me.Label1.TabIndex = 113
        Me.Label1.Text = "6"
        '
        'etq_UnitEpsilon
        '
        Me.etq_UnitEpsilon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitEpsilon.AutoSize = True
        Me.etq_UnitEpsilon.Location = New System.Drawing.Point(305, 58)
        Me.etq_UnitEpsilon.Name = "etq_UnitEpsilon"
        Me.etq_UnitEpsilon.Size = New System.Drawing.Size(27, 13)
        Me.etq_UnitEpsilon.TabIndex = 112
        Me.etq_UnitEpsilon.Text = "x 10"
        '
        'txt_EpsilonSh
        '
        Me.txt_EpsilonSh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EpsilonSh.Location = New System.Drawing.Point(241, 55)
        Me.txt_EpsilonSh.Name = "txt_EpsilonSh"
        Me.txt_EpsilonSh.Size = New System.Drawing.Size(58, 20)
        Me.txt_EpsilonSh.TabIndex = 111
        '
        'img_EpsilonSh
        '
        Me.img_EpsilonSh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_EpsilonSh.Location = New System.Drawing.Point(193, 55)
        Me.img_EpsilonSh.Name = "img_EpsilonSh"
        Me.img_EpsilonSh.Size = New System.Drawing.Size(46, 20)
        Me.img_EpsilonSh.TabIndex = 110
        Me.img_EpsilonSh.TabStop = False
        '
        'img_RH
        '
        Me.img_RH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_RH.Location = New System.Drawing.Point(193, 30)
        Me.img_RH.Name = "img_RH"
        Me.img_RH.Size = New System.Drawing.Size(46, 20)
        Me.img_RH.TabIndex = 109
        Me.img_RH.TabStop = False
        '
        'lbl_Shrinkage
        '
        Me.lbl_Shrinkage.AutoSize = True
        Me.lbl_Shrinkage.Location = New System.Drawing.Point(8, 59)
        Me.lbl_Shrinkage.Name = "lbl_Shrinkage"
        Me.lbl_Shrinkage.Size = New System.Drawing.Size(71, 13)
        Me.lbl_Shrinkage.TabIndex = 108
        Me.lbl_Shrinkage.Text = "lbl_Shrinkage"
        '
        'cmb_RH
        '
        Me.cmb_RH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_RH.FormattingEnabled = True
        Me.cmb_RH.Location = New System.Drawing.Point(241, 29)
        Me.cmb_RH.Name = "cmb_RH"
        Me.cmb_RH.Size = New System.Drawing.Size(104, 21)
        Me.cmb_RH.TabIndex = 107
        '
        'lbl_RH
        '
        Me.lbl_RH.AutoSize = True
        Me.lbl_RH.Location = New System.Drawing.Point(8, 32)
        Me.lbl_RH.Name = "lbl_RH"
        Me.lbl_RH.Size = New System.Drawing.Size(39, 13)
        Me.lbl_RH.TabIndex = 61
        Me.lbl_RH.Text = "lbl_RH"
        '
        'lbl_BetonMessage
        '
        Me.lbl_BetonMessage.AutoSize = True
        Me.lbl_BetonMessage.Location = New System.Drawing.Point(8, 9)
        Me.lbl_BetonMessage.Name = "lbl_BetonMessage"
        Me.lbl_BetonMessage.Size = New System.Drawing.Size(94, 13)
        Me.lbl_BetonMessage.TabIndex = 60
        Me.lbl_BetonMessage.Text = "lbl_BetonMessage"
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'lbl_CadreSections
        '
        Me.lbl_CadreSections.AutoSize = True
        Me.lbl_CadreSections.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreSections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreSections.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreSections.Location = New System.Drawing.Point(0, 555)
        Me.lbl_CadreSections.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreSections.Name = "lbl_CadreSections"
        Me.lbl_CadreSections.Size = New System.Drawing.Size(442, 30)
        Me.lbl_CadreSections.TabIndex = 6
        Me.lbl_CadreSections.Text = "lbl_CadreSections"
        Me.lbl_CadreSections.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Sections
        '
        Me.pan_Sections.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Sections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Sections.Controls.Add(Me.chk_ArmaComprimees)
        Me.pan_Sections.Controls.Add(Me.chk_LargeursPartipantesSimples)
        Me.pan_Sections.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Sections.Location = New System.Drawing.Point(0, 585)
        Me.pan_Sections.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Sections.Name = "pan_Sections"
        Me.pan_Sections.Size = New System.Drawing.Size(442, 119)
        Me.pan_Sections.TabIndex = 8
        '
        'chk_LargeursPartipantesSimples
        '
        Me.chk_LargeursPartipantesSimples.AutoSize = True
        Me.chk_LargeursPartipantesSimples.Location = New System.Drawing.Point(11, 14)
        Me.chk_LargeursPartipantesSimples.Name = "chk_LargeursPartipantesSimples"
        Me.chk_LargeursPartipantesSimples.Size = New System.Drawing.Size(180, 17)
        Me.chk_LargeursPartipantesSimples.TabIndex = 115
        Me.chk_LargeursPartipantesSimples.Text = "chk_LargeursPartipantesSimples"
        Me.chk_LargeursPartipantesSimples.UseVisualStyleBackColor = True
        Me.chk_LargeursPartipantesSimples.Visible = False
        '
        'chk_ArmaComprimees
        '
        Me.chk_ArmaComprimees.AutoSize = True
        Me.chk_ArmaComprimees.Location = New System.Drawing.Point(11, 37)
        Me.chk_ArmaComprimees.Name = "chk_ArmaComprimees"
        Me.chk_ArmaComprimees.Size = New System.Drawing.Size(131, 17)
        Me.chk_ArmaComprimees.TabIndex = 116
        Me.chk_ArmaComprimees.Text = "chk_ArmaComprimees"
        Me.chk_ArmaComprimees.UseVisualStyleBackColor = True
        Me.chk_ArmaComprimees.Visible = False
        '
        'Frm_OptionsCalculPoutre
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(465, 543)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_OptionsCalculPoutre"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_OptionsCalcuPoutre"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Portees.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_OptionsELU.ResumeLayout(False)
        Me.pan_OptionsELU.PerformLayout()
        Me.pan_Norm.ResumeLayout(False)
        Me.pan_Norm.PerformLayout()
        Me.pan_Beton.ResumeLayout(False)
        Me.pan_Beton.PerformLayout()
        CType(Me.img_Es, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_EpsilonSh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_RH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Sections.ResumeLayout(False)
        Me.pan_Sections.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Portees As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_CadreNorm As Label
    Friend WithEvents pan_Norm As Panel
    Friend WithEvents cmb_Norme As ComboBox
    Friend WithEvents lbl_Norme As Label
    Friend WithEvents pan_OptionsELU As Panel
    Friend WithEvents lbl_CadreELU As Label
    Friend WithEvents rdb_ElasticDesign As RadioButton
    Friend WithEvents rdb_NormalDesign As RadioButton
    Friend WithEvents pan_OptionsELS As Panel
    Friend WithEvents lbl_CadreELS As Label
    Friend WithEvents lbl_CadreBeton As Label
    Friend WithEvents pan_Beton As Panel
    Friend WithEvents cmb_RH As ComboBox
    Friend WithEvents lbl_RH As Label
    Friend WithEvents lbl_BetonMessage As Label
    Friend WithEvents lbl_Shrinkage As Label
    Friend WithEvents img_EpsilonSh As PictureBox
    Friend WithEvents img_RH As PictureBox
    Friend WithEvents etq_UnitEpsilon As Label
    Friend WithEvents txt_EpsilonSh As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents chk_RetraitEnrobage As CheckBox
    Friend WithEvents etq_UnitModuleY As Label
    Friend WithEvents txt_Es As TextBox
    Friend WithEvents img_Es As PictureBox
    Friend WithEvents lbl_ArmaYoung As Label
    Friend WithEvents pan_Sections As Panel
    Friend WithEvents chk_ArmaComprimees As CheckBox
    Friend WithEvents chk_LargeursPartipantesSimples As CheckBox
    Friend WithEvents lbl_CadreSections As Label
End Class
