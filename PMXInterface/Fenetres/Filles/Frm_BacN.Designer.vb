<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BacN
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
        Me.TLpan_SepHorizon = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_SepVerticale = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Dimensions = New System.Windows.Forms.Label()
        Me.pan_Droite = New System.Windows.Forms.Panel()
        Me.lbl_Nom = New System.Windows.Forms.Label()
        Me.txt_Name = New System.Windows.Forms.TextBox()
        Me.pan_CustomBac = New System.Windows.Forms.Panel()
        Me.txt_MuP = New System.Windows.Forms.TextBox()
        Me.etq_UnitMuP = New System.Windows.Forms.Label()
        Me.img_MuP = New System.Windows.Forms.PictureBox()
        Me.txt_Fyp = New System.Windows.Forms.TextBox()
        Me.etq_UnitSigma1 = New System.Windows.Forms.Label()
        Me.img_Fyp = New System.Windows.Forms.PictureBox()
        Me.txt_hpg = New System.Windows.Forms.TextBox()
        Me.etq_UnitDimB5 = New System.Windows.Forms.Label()
        Me.img_hpg = New System.Windows.Forms.PictureBox()
        Me.txt_tp = New System.Windows.Forms.TextBox()
        Me.etq_UnitDimB6 = New System.Windows.Forms.Label()
        Me.img_tp = New System.Windows.Forms.PictureBox()
        Me.txt_Bb = New System.Windows.Forms.TextBox()
        Me.etq_UnitDimB4 = New System.Windows.Forms.Label()
        Me.img_Bb = New System.Windows.Forms.PictureBox()
        Me.txt_Bt = New System.Windows.Forms.TextBox()
        Me.etq_UnitDimB3 = New System.Windows.Forms.Label()
        Me.img_Bt = New System.Windows.Forms.PictureBox()
        Me.txt_ep = New System.Windows.Forms.TextBox()
        Me.etq_UnitDimB2 = New System.Windows.Forms.Label()
        Me.img_ep = New System.Windows.Forms.PictureBox()
        Me.txt_Hp = New System.Windows.Forms.TextBox()
        Me.etq_UnitDimB1 = New System.Windows.Forms.Label()
        Me.img_hp = New System.Windows.Forms.PictureBox()
        Me.rdb_BacCustom = New System.Windows.Forms.RadioButton()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Database = New System.Windows.Forms.Label()
        Me.pan_DataBase = New System.Windows.Forms.Panel()
        Me.Grid_Bac = New System.Windows.Forms.DataGridView()
        Me.Col_ListeSup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lbl_Producteur = New System.Windows.Forms.Label()
        Me.cmb_Producteur = New System.Windows.Forms.ComboBox()
        Me.rdb_BacBase = New System.Windows.Forms.RadioButton()
        Me.pan_Representation = New System.Windows.Forms.Panel()
        Me.lbl_EtiquetteBac = New System.Windows.Forms.Label()
        Me.img_Bac = New System.Windows.Forms.PictureBox()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_SepHorizon.SuspendLayout()
        Me.TLpan_SepVerticale.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pan_Droite.SuspendLayout()
        Me.pan_CustomBac.SuspendLayout()
        CType(Me.img_MuP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Fyp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_hpg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_tp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Bb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Bt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_hp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLpan_Gauche.SuspendLayout()
        Me.pan_DataBase.SuspendLayout()
        CType(Me.Grid_Bac, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Representation.SuspendLayout()
        CType(Me.img_Bac, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(507, 509)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(507, 509)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 472)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(501, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(263, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(123, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLpan_SepHorizon)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(501, 463)
        Me.pan_Main.TabIndex = 1
        '
        'TLpan_SepHorizon
        '
        Me.TLpan_SepHorizon.ColumnCount = 1
        Me.TLpan_SepHorizon.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_SepHorizon.Controls.Add(Me.TLpan_SepVerticale, 0, 0)
        Me.TLpan_SepHorizon.Controls.Add(Me.pan_Representation, 0, 1)
        Me.TLpan_SepHorizon.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_SepHorizon.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_SepHorizon.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_SepHorizon.Name = "TLpan_SepHorizon"
        Me.TLpan_SepHorizon.RowCount = 2
        Me.TLpan_SepHorizon.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TLpan_SepHorizon.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_SepHorizon.Size = New System.Drawing.Size(501, 463)
        Me.TLpan_SepHorizon.TabIndex = 0
        '
        'TLpan_SepVerticale
        '
        Me.TLpan_SepVerticale.ColumnCount = 2
        Me.TLpan_SepVerticale.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_SepVerticale.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_SepVerticale.Controls.Add(Me.TableLayoutPanel1, 1, 0)
        Me.TLpan_SepVerticale.Controls.Add(Me.TLpan_Gauche, 0, 0)
        Me.TLpan_SepVerticale.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_SepVerticale.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_SepVerticale.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_SepVerticale.Name = "TLpan_SepVerticale"
        Me.TLpan_SepVerticale.RowCount = 1
        Me.TLpan_SepVerticale.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_SepVerticale.Size = New System.Drawing.Size(501, 300)
        Me.TLpan_SepVerticale.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_Dimensions, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pan_Droite, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(251, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(250, 300)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'lbl_Dimensions
        '
        Me.lbl_Dimensions.AutoSize = True
        Me.lbl_Dimensions.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Dimensions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Dimensions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Dimensions.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Dimensions.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Dimensions.Name = "lbl_Dimensions"
        Me.lbl_Dimensions.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Dimensions.TabIndex = 2
        Me.lbl_Dimensions.Text = "lbl_Dimensions"
        Me.lbl_Dimensions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Droite
        '
        Me.pan_Droite.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Droite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Droite.Controls.Add(Me.lbl_Nom)
        Me.pan_Droite.Controls.Add(Me.txt_Name)
        Me.pan_Droite.Controls.Add(Me.pan_CustomBac)
        Me.pan_Droite.Controls.Add(Me.rdb_BacCustom)
        Me.pan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Droite.Location = New System.Drawing.Point(0, 30)
        Me.pan_Droite.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Droite.Name = "pan_Droite"
        Me.pan_Droite.Size = New System.Drawing.Size(250, 270)
        Me.pan_Droite.TabIndex = 3
        '
        'lbl_Nom
        '
        Me.lbl_Nom.AutoSize = True
        Me.lbl_Nom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Nom.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Nom.Location = New System.Drawing.Point(8, 42)
        Me.lbl_Nom.Name = "lbl_Nom"
        Me.lbl_Nom.Size = New System.Drawing.Size(45, 13)
        Me.lbl_Nom.TabIndex = 76
        Me.lbl_Nom.Text = "lbl_Nom"
        Me.lbl_Nom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_Name
        '
        Me.txt_Name.Location = New System.Drawing.Point(79, 40)
        Me.txt_Name.Name = "txt_Name"
        Me.txt_Name.Size = New System.Drawing.Size(138, 20)
        Me.txt_Name.TabIndex = 75
        '
        'pan_CustomBac
        '
        Me.pan_CustomBac.Controls.Add(Me.txt_MuP)
        Me.pan_CustomBac.Controls.Add(Me.etq_UnitMuP)
        Me.pan_CustomBac.Controls.Add(Me.img_MuP)
        Me.pan_CustomBac.Controls.Add(Me.txt_Fyp)
        Me.pan_CustomBac.Controls.Add(Me.etq_UnitSigma1)
        Me.pan_CustomBac.Controls.Add(Me.img_Fyp)
        Me.pan_CustomBac.Controls.Add(Me.txt_hpg)
        Me.pan_CustomBac.Controls.Add(Me.etq_UnitDimB5)
        Me.pan_CustomBac.Controls.Add(Me.img_hpg)
        Me.pan_CustomBac.Controls.Add(Me.txt_tp)
        Me.pan_CustomBac.Controls.Add(Me.etq_UnitDimB6)
        Me.pan_CustomBac.Controls.Add(Me.img_tp)
        Me.pan_CustomBac.Controls.Add(Me.txt_Bb)
        Me.pan_CustomBac.Controls.Add(Me.etq_UnitDimB4)
        Me.pan_CustomBac.Controls.Add(Me.img_Bb)
        Me.pan_CustomBac.Controls.Add(Me.txt_Bt)
        Me.pan_CustomBac.Controls.Add(Me.etq_UnitDimB3)
        Me.pan_CustomBac.Controls.Add(Me.img_Bt)
        Me.pan_CustomBac.Controls.Add(Me.txt_ep)
        Me.pan_CustomBac.Controls.Add(Me.etq_UnitDimB2)
        Me.pan_CustomBac.Controls.Add(Me.img_ep)
        Me.pan_CustomBac.Controls.Add(Me.txt_Hp)
        Me.pan_CustomBac.Controls.Add(Me.etq_UnitDimB1)
        Me.pan_CustomBac.Controls.Add(Me.img_hp)
        Me.pan_CustomBac.Location = New System.Drawing.Point(18, 66)
        Me.pan_CustomBac.Name = "pan_CustomBac"
        Me.pan_CustomBac.Size = New System.Drawing.Size(184, 199)
        Me.pan_CustomBac.TabIndex = 62
        '
        'txt_MuP
        '
        Me.txt_MuP.Location = New System.Drawing.Point(61, 157)
        Me.txt_MuP.Name = "txt_MuP"
        Me.txt_MuP.Size = New System.Drawing.Size(58, 20)
        Me.txt_MuP.TabIndex = 95
        '
        'etq_UnitMuP
        '
        Me.etq_UnitMuP.AutoSize = True
        Me.etq_UnitMuP.Location = New System.Drawing.Point(125, 160)
        Me.etq_UnitMuP.Name = "etq_UnitMuP"
        Me.etq_UnitMuP.Size = New System.Drawing.Size(29, 13)
        Me.etq_UnitMuP.TabIndex = 94
        Me.etq_UnitMuP.Text = "MuP"
        '
        'img_MuP
        '
        Me.img_MuP.Location = New System.Drawing.Point(24, 157)
        Me.img_MuP.Name = "img_MuP"
        Me.img_MuP.Size = New System.Drawing.Size(37, 20)
        Me.img_MuP.TabIndex = 96
        Me.img_MuP.TabStop = False
        '
        'txt_Fyp
        '
        Me.txt_Fyp.Location = New System.Drawing.Point(61, 135)
        Me.txt_Fyp.Name = "txt_Fyp"
        Me.txt_Fyp.Size = New System.Drawing.Size(58, 20)
        Me.txt_Fyp.TabIndex = 92
        '
        'etq_UnitSigma1
        '
        Me.etq_UnitSigma1.AutoSize = True
        Me.etq_UnitSigma1.Location = New System.Drawing.Point(125, 138)
        Me.etq_UnitSigma1.Name = "etq_UnitSigma1"
        Me.etq_UnitSigma1.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitSigma1.TabIndex = 91
        Me.etq_UnitSigma1.Text = "fyp"
        '
        'img_Fyp
        '
        Me.img_Fyp.Location = New System.Drawing.Point(24, 135)
        Me.img_Fyp.Name = "img_Fyp"
        Me.img_Fyp.Size = New System.Drawing.Size(37, 20)
        Me.img_Fyp.TabIndex = 93
        Me.img_Fyp.TabStop = False
        '
        'txt_hpg
        '
        Me.txt_hpg.Location = New System.Drawing.Point(61, 25)
        Me.txt_hpg.Name = "txt_hpg"
        Me.txt_hpg.Size = New System.Drawing.Size(58, 20)
        Me.txt_hpg.TabIndex = 89
        '
        'etq_UnitDimB5
        '
        Me.etq_UnitDimB5.AutoSize = True
        Me.etq_UnitDimB5.Location = New System.Drawing.Point(125, 28)
        Me.etq_UnitDimB5.Name = "etq_UnitDimB5"
        Me.etq_UnitDimB5.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDimB5.TabIndex = 88
        Me.etq_UnitDimB5.Text = "mm"
        '
        'img_hpg
        '
        Me.img_hpg.Location = New System.Drawing.Point(24, 25)
        Me.img_hpg.Name = "img_hpg"
        Me.img_hpg.Size = New System.Drawing.Size(37, 20)
        Me.img_hpg.TabIndex = 90
        Me.img_hpg.TabStop = False
        '
        'txt_tp
        '
        Me.txt_tp.Location = New System.Drawing.Point(61, 113)
        Me.txt_tp.Name = "txt_tp"
        Me.txt_tp.Size = New System.Drawing.Size(58, 20)
        Me.txt_tp.TabIndex = 86
        '
        'etq_UnitDimB6
        '
        Me.etq_UnitDimB6.AutoSize = True
        Me.etq_UnitDimB6.Location = New System.Drawing.Point(125, 116)
        Me.etq_UnitDimB6.Name = "etq_UnitDimB6"
        Me.etq_UnitDimB6.Size = New System.Drawing.Size(16, 13)
        Me.etq_UnitDimB6.TabIndex = 85
        Me.etq_UnitDimB6.Text = "tp"
        '
        'img_tp
        '
        Me.img_tp.Location = New System.Drawing.Point(24, 113)
        Me.img_tp.Name = "img_tp"
        Me.img_tp.Size = New System.Drawing.Size(37, 20)
        Me.img_tp.TabIndex = 87
        Me.img_tp.TabStop = False
        '
        'txt_Bb
        '
        Me.txt_Bb.Location = New System.Drawing.Point(61, 91)
        Me.txt_Bb.Name = "txt_Bb"
        Me.txt_Bb.Size = New System.Drawing.Size(58, 20)
        Me.txt_Bb.TabIndex = 83
        '
        'etq_UnitDimB4
        '
        Me.etq_UnitDimB4.AutoSize = True
        Me.etq_UnitDimB4.Location = New System.Drawing.Point(125, 94)
        Me.etq_UnitDimB4.Name = "etq_UnitDimB4"
        Me.etq_UnitDimB4.Size = New System.Drawing.Size(19, 13)
        Me.etq_UnitDimB4.TabIndex = 82
        Me.etq_UnitDimB4.Text = "bb"
        '
        'img_Bb
        '
        Me.img_Bb.Location = New System.Drawing.Point(24, 91)
        Me.img_Bb.Name = "img_Bb"
        Me.img_Bb.Size = New System.Drawing.Size(37, 20)
        Me.img_Bb.TabIndex = 84
        Me.img_Bb.TabStop = False
        '
        'txt_Bt
        '
        Me.txt_Bt.Location = New System.Drawing.Point(61, 69)
        Me.txt_Bt.Name = "txt_Bt"
        Me.txt_Bt.Size = New System.Drawing.Size(58, 20)
        Me.txt_Bt.TabIndex = 80
        '
        'etq_UnitDimB3
        '
        Me.etq_UnitDimB3.AutoSize = True
        Me.etq_UnitDimB3.Location = New System.Drawing.Point(125, 72)
        Me.etq_UnitDimB3.Name = "etq_UnitDimB3"
        Me.etq_UnitDimB3.Size = New System.Drawing.Size(16, 13)
        Me.etq_UnitDimB3.TabIndex = 79
        Me.etq_UnitDimB3.Text = "bt"
        '
        'img_Bt
        '
        Me.img_Bt.Location = New System.Drawing.Point(24, 69)
        Me.img_Bt.Name = "img_Bt"
        Me.img_Bt.Size = New System.Drawing.Size(37, 20)
        Me.img_Bt.TabIndex = 81
        Me.img_Bt.TabStop = False
        '
        'txt_ep
        '
        Me.txt_ep.Location = New System.Drawing.Point(61, 47)
        Me.txt_ep.Name = "txt_ep"
        Me.txt_ep.Size = New System.Drawing.Size(58, 20)
        Me.txt_ep.TabIndex = 77
        '
        'etq_UnitDimB2
        '
        Me.etq_UnitDimB2.AutoSize = True
        Me.etq_UnitDimB2.Location = New System.Drawing.Point(125, 50)
        Me.etq_UnitDimB2.Name = "etq_UnitDimB2"
        Me.etq_UnitDimB2.Size = New System.Drawing.Size(19, 13)
        Me.etq_UnitDimB2.TabIndex = 76
        Me.etq_UnitDimB2.Text = "ep"
        '
        'img_ep
        '
        Me.img_ep.Location = New System.Drawing.Point(24, 47)
        Me.img_ep.Name = "img_ep"
        Me.img_ep.Size = New System.Drawing.Size(37, 20)
        Me.img_ep.TabIndex = 78
        Me.img_ep.TabStop = False
        '
        'txt_Hp
        '
        Me.txt_Hp.Location = New System.Drawing.Point(61, 3)
        Me.txt_Hp.Name = "txt_Hp"
        Me.txt_Hp.Size = New System.Drawing.Size(58, 20)
        Me.txt_Hp.TabIndex = 74
        '
        'etq_UnitDimB1
        '
        Me.etq_UnitDimB1.AutoSize = True
        Me.etq_UnitDimB1.Location = New System.Drawing.Point(125, 6)
        Me.etq_UnitDimB1.Name = "etq_UnitDimB1"
        Me.etq_UnitDimB1.Size = New System.Drawing.Size(19, 13)
        Me.etq_UnitDimB1.TabIndex = 73
        Me.etq_UnitDimB1.Text = "hp"
        '
        'img_hp
        '
        Me.img_hp.Location = New System.Drawing.Point(24, 3)
        Me.img_hp.Name = "img_hp"
        Me.img_hp.Size = New System.Drawing.Size(37, 20)
        Me.img_hp.TabIndex = 75
        Me.img_hp.TabStop = False
        '
        'rdb_BacCustom
        '
        Me.rdb_BacCustom.AutoSize = True
        Me.rdb_BacCustom.Location = New System.Drawing.Point(11, 12)
        Me.rdb_BacCustom.Name = "rdb_BacCustom"
        Me.rdb_BacCustom.Size = New System.Drawing.Size(100, 17)
        Me.rdb_BacCustom.TabIndex = 60
        Me.rdb_BacCustom.TabStop = True
        Me.rdb_BacCustom.Text = "rdb_BacCustom"
        Me.rdb_BacCustom.UseVisualStyleBackColor = True
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.lbl_Database, 0, 0)
        Me.TLpan_Gauche.Controls.Add(Me.pan_DataBase, 0, 1)
        Me.TLpan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Gauche.Name = "TLpan_Gauche"
        Me.TLpan_Gauche.RowCount = 2
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Size = New System.Drawing.Size(250, 300)
        Me.TLpan_Gauche.TabIndex = 0
        '
        'lbl_Database
        '
        Me.lbl_Database.AutoSize = True
        Me.lbl_Database.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Database.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Database.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Database.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Database.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Database.Name = "lbl_Database"
        Me.lbl_Database.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Database.TabIndex = 2
        Me.lbl_Database.Text = "lbl_Database"
        Me.lbl_Database.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_DataBase
        '
        Me.pan_DataBase.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_DataBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_DataBase.Controls.Add(Me.Grid_Bac)
        Me.pan_DataBase.Controls.Add(Me.lbl_Producteur)
        Me.pan_DataBase.Controls.Add(Me.cmb_Producteur)
        Me.pan_DataBase.Controls.Add(Me.rdb_BacBase)
        Me.pan_DataBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_DataBase.Location = New System.Drawing.Point(0, 30)
        Me.pan_DataBase.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_DataBase.Name = "pan_DataBase"
        Me.pan_DataBase.Size = New System.Drawing.Size(250, 270)
        Me.pan_DataBase.TabIndex = 3
        '
        'Grid_Bac
        '
        Me.Grid_Bac.AllowUserToAddRows = False
        Me.Grid_Bac.AllowUserToDeleteRows = False
        Me.Grid_Bac.AllowUserToResizeColumns = False
        Me.Grid_Bac.AllowUserToResizeRows = False
        Me.Grid_Bac.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid_Bac.ColumnHeadersVisible = False
        Me.Grid_Bac.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_ListeSup})
        Me.Grid_Bac.Location = New System.Drawing.Point(11, 66)
        Me.Grid_Bac.MultiSelect = False
        Me.Grid_Bac.Name = "Grid_Bac"
        Me.Grid_Bac.RowHeadersVisible = False
        Me.Grid_Bac.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Grid_Bac.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid_Bac.ShowCellToolTips = False
        Me.Grid_Bac.Size = New System.Drawing.Size(225, 188)
        Me.Grid_Bac.TabIndex = 64
        '
        'Col_ListeSup
        '
        Me.Col_ListeSup.HeaderText = "Col_Liste"
        Me.Col_ListeSup.Name = "Col_ListeSup"
        Me.Col_ListeSup.ReadOnly = True
        '
        'lbl_Producteur
        '
        Me.lbl_Producteur.AutoSize = True
        Me.lbl_Producteur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Producteur.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Producteur.Location = New System.Drawing.Point(8, 42)
        Me.lbl_Producteur.Name = "lbl_Producteur"
        Me.lbl_Producteur.Size = New System.Drawing.Size(75, 13)
        Me.lbl_Producteur.TabIndex = 62
        Me.lbl_Producteur.Text = "lbl_Producteur"
        Me.lbl_Producteur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmb_Producteur
        '
        Me.cmb_Producteur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Producteur.FormattingEnabled = True
        Me.cmb_Producteur.Location = New System.Drawing.Point(93, 39)
        Me.cmb_Producteur.Name = "cmb_Producteur"
        Me.cmb_Producteur.Size = New System.Drawing.Size(143, 21)
        Me.cmb_Producteur.TabIndex = 63
        '
        'rdb_BacBase
        '
        Me.rdb_BacBase.AutoSize = True
        Me.rdb_BacBase.Location = New System.Drawing.Point(8, 12)
        Me.rdb_BacBase.Name = "rdb_BacBase"
        Me.rdb_BacBase.Size = New System.Drawing.Size(89, 17)
        Me.rdb_BacBase.TabIndex = 61
        Me.rdb_BacBase.TabStop = True
        Me.rdb_BacBase.Text = "rdb_BacBase"
        Me.rdb_BacBase.UseVisualStyleBackColor = True
        '
        'pan_Representation
        '
        Me.pan_Representation.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Representation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Representation.Controls.Add(Me.lbl_EtiquetteBac)
        Me.pan_Representation.Controls.Add(Me.img_Bac)
        Me.pan_Representation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Representation.Location = New System.Drawing.Point(0, 301)
        Me.pan_Representation.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.pan_Representation.Name = "pan_Representation"
        Me.pan_Representation.Size = New System.Drawing.Size(501, 162)
        Me.pan_Representation.TabIndex = 1
        '
        'lbl_EtiquetteBac
        '
        Me.lbl_EtiquetteBac.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_EtiquetteBac.Location = New System.Drawing.Point(5, 5)
        Me.lbl_EtiquetteBac.Name = "lbl_EtiquetteBac"
        Me.lbl_EtiquetteBac.Size = New System.Drawing.Size(489, 20)
        Me.lbl_EtiquetteBac.TabIndex = 5
        Me.lbl_EtiquetteBac.Text = "lbl_EtiquetteBac"
        Me.lbl_EtiquetteBac.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'img_Bac
        '
        Me.img_Bac.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Bac.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Bac.Location = New System.Drawing.Point(5, 29)
        Me.img_Bac.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.img_Bac.Name = "img_Bac"
        Me.img_Bac.Size = New System.Drawing.Size(489, 131)
        Me.img_Bac.TabIndex = 4
        Me.img_Bac.TabStop = False
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Frm_BacN
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(507, 509)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Frm_BacN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_BacN"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_SepHorizon.ResumeLayout(False)
        Me.TLpan_SepVerticale.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.pan_Droite.ResumeLayout(False)
        Me.pan_Droite.PerformLayout()
        Me.pan_CustomBac.ResumeLayout(False)
        Me.pan_CustomBac.PerformLayout()
        CType(Me.img_MuP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Fyp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_hpg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_tp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Bb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Bt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_hp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.PerformLayout()
        Me.pan_DataBase.ResumeLayout(False)
        Me.pan_DataBase.PerformLayout()
        CType(Me.Grid_Bac, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Representation.ResumeLayout(False)
        CType(Me.img_Bac, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_SepHorizon As TableLayoutPanel
    Friend WithEvents TLpan_SepVerticale As TableLayoutPanel
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Database As Label
    Friend WithEvents pan_DataBase As Panel
    Friend WithEvents img_Bac As PictureBox
    Friend WithEvents Grid_Bac As DataGridView
    Friend WithEvents Col_ListeSup As DataGridViewTextBoxColumn
    Friend WithEvents lbl_Producteur As Label
    Friend WithEvents cmb_Producteur As ComboBox
    Friend WithEvents rdb_BacBase As RadioButton
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lbl_Dimensions As Label
    Friend WithEvents pan_Droite As Panel
    Friend WithEvents rdb_BacCustom As RadioButton
    Friend WithEvents pan_CustomBac As Panel
    Friend WithEvents txt_hpg As TextBox
    Friend WithEvents etq_UnitDimB5 As Label
    Friend WithEvents img_hpg As PictureBox
    Friend WithEvents txt_tp As TextBox
    Friend WithEvents etq_UnitDimB6 As Label
    Friend WithEvents img_tp As PictureBox
    Friend WithEvents txt_Bb As TextBox
    Friend WithEvents etq_UnitDimB4 As Label
    Friend WithEvents img_Bb As PictureBox
    Friend WithEvents txt_Bt As TextBox
    Friend WithEvents etq_UnitDimB3 As Label
    Friend WithEvents img_Bt As PictureBox
    Friend WithEvents txt_ep As TextBox
    Friend WithEvents etq_UnitDimB2 As Label
    Friend WithEvents img_ep As PictureBox
    Friend WithEvents txt_Hp As TextBox
    Friend WithEvents etq_UnitDimB1 As Label
    Friend WithEvents img_hp As PictureBox
    Friend WithEvents txt_Fyp As TextBox
    Friend WithEvents etq_UnitSigma1 As Label
    Friend WithEvents img_Fyp As PictureBox
    Friend WithEvents lbl_Nom As Label
    Friend WithEvents txt_Name As TextBox
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents pan_Representation As Panel
    Friend WithEvents lbl_EtiquetteBac As Label
    Friend WithEvents txt_MuP As TextBox
    Friend WithEvents etq_UnitMuP As Label
    Friend WithEvents img_MuP As PictureBox
End Class
