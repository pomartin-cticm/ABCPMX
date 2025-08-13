<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ConnectionSlimConnecteur
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_ConnectionSlimConnecteur))
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Image = New System.Windows.Forms.Panel()
        Me.img_Stud = New System.Windows.Forms.PictureBox()
        Me.TLPan_Saisies = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Connecteur = New System.Windows.Forms.Label()
        Me.lbl_Type = New System.Windows.Forms.Label()
        Me.pan_TypeConnecteur = New System.Windows.Forms.Panel()
        Me.img_info = New System.Windows.Forms.PictureBox()
        Me.rdb_Armatures = New System.Windows.Forms.RadioButton()
        Me.rdb_GoujonAme = New System.Windows.Forms.RadioButton()
        Me.rdb_GoujonSemSup = New System.Windows.Forms.RadioButton()
        Me.pan_ConteneurConnecteur = New System.Windows.Forms.Panel()
        Me.pan_SaisieGoujons = New System.Windows.Forms.Panel()
        Me.etq_UnitPRd = New System.Windows.Forms.Label()
        Me.txt_PRd = New System.Windows.Forms.TextBox()
        Me.img_PRd = New System.Windows.Forms.PictureBox()
        Me.pan_Avertissement = New System.Windows.Forms.Panel()
        Me.lbl_Avertissement = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.lbl_Stud = New System.Windows.Forms.Label()
        Me.etq_UnitFu = New System.Windows.Forms.Label()
        Me.etq_UnitD = New System.Windows.Forms.Label()
        Me.etq_UnitHsc = New System.Windows.Forms.Label()
        Me.txt_fu = New System.Windows.Forms.TextBox()
        Me.txt_d = New System.Windows.Forms.TextBox()
        Me.txt_hsc = New System.Windows.Forms.TextBox()
        Me.img_fu = New System.Windows.Forms.PictureBox()
        Me.img_d = New System.Windows.Forms.PictureBox()
        Me.img_hsc = New System.Windows.Forms.PictureBox()
        Me.cmb_goujons = New System.Windows.Forms.ComboBox()
        Me.pan_SaisieArmature = New System.Windows.Forms.Panel()
        Me.cmb_Diametre = New System.Windows.Forms.ComboBox()
        Me.lbl_ClasseA = New System.Windows.Forms.Label()
        Me.lbl_Diameter = New System.Windows.Forms.Label()
        Me.cmb_Acier = New System.Windows.Forms.ComboBox()
        Me.etq_UnitFsk = New System.Windows.Forms.Label()
        Me.etq_UnitPhiS = New System.Windows.Forms.Label()
        Me.txt_Fsk = New System.Windows.Forms.TextBox()
        Me.img_Fsk = New System.Windows.Forms.PictureBox()
        Me.img_PhiS = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_PRdArma = New System.Windows.Forms.TextBox()
        Me.img_PRd2 = New System.Windows.Forms.PictureBox()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Main.SuspendLayout()
        Me.pan_Image.SuspendLayout()
        CType(Me.img_Stud, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLPan_Saisies.SuspendLayout()
        Me.pan_TypeConnecteur.SuspendLayout()
        CType(Me.img_info, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_SaisieGoujons.SuspendLayout()
        CType(Me.img_PRd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Avertissement.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_fu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_d, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_hsc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_SaisieArmature.SuspendLayout()
        CType(Me.img_Fsk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_PhiS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_PRd2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.Controls.Add(Me.TLPan_Main)
        Me.pan_Main.Location = New System.Drawing.Point(112, 78)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(486, 366)
        Me.pan_Main.TabIndex = 0
        '
        'TLPan_Main
        '
        Me.TLPan_Main.ColumnCount = 2
        Me.TLPan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Main.Controls.Add(Me.pan_Image, 1, 0)
        Me.TLPan_Main.Controls.Add(Me.TLPan_Saisies, 0, 0)
        Me.TLPan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Main.Name = "TLPan_Main"
        Me.TLPan_Main.RowCount = 1
        Me.TLPan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Main.Size = New System.Drawing.Size(486, 366)
        Me.TLPan_Main.TabIndex = 0
        '
        'pan_Image
        '
        Me.pan_Image.Controls.Add(Me.img_Stud)
        Me.pan_Image.Location = New System.Drawing.Point(251, 0)
        Me.pan_Image.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Image.Name = "pan_Image"
        Me.pan_Image.Size = New System.Drawing.Size(200, 100)
        Me.pan_Image.TabIndex = 0
        '
        'img_Stud
        '
        Me.img_Stud.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Stud.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Stud.Location = New System.Drawing.Point(8, 8)
        Me.img_Stud.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.img_Stud.Name = "img_Stud"
        Me.img_Stud.Size = New System.Drawing.Size(135, 48)
        Me.img_Stud.TabIndex = 77
        Me.img_Stud.TabStop = False
        '
        'TLPan_Saisies
        '
        Me.TLPan_Saisies.ColumnCount = 1
        Me.TLPan_Saisies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Saisies.Controls.Add(Me.lbl_Connecteur, 0, 2)
        Me.TLPan_Saisies.Controls.Add(Me.lbl_Type, 0, 0)
        Me.TLPan_Saisies.Controls.Add(Me.pan_TypeConnecteur, 0, 1)
        Me.TLPan_Saisies.Controls.Add(Me.pan_ConteneurConnecteur, 0, 3)
        Me.TLPan_Saisies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Saisies.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Saisies.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Saisies.Name = "TLPan_Saisies"
        Me.TLPan_Saisies.RowCount = 4
        Me.TLPan_Saisies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Saisies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TLPan_Saisies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Saisies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Saisies.Size = New System.Drawing.Size(250, 366)
        Me.TLPan_Saisies.TabIndex = 1
        '
        'lbl_Connecteur
        '
        Me.lbl_Connecteur.AutoSize = True
        Me.lbl_Connecteur.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Connecteur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Connecteur.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Connecteur.Location = New System.Drawing.Point(0, 120)
        Me.lbl_Connecteur.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Connecteur.Name = "lbl_Connecteur"
        Me.lbl_Connecteur.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Connecteur.TabIndex = 3
        Me.lbl_Connecteur.Text = "lbl_Connecteur"
        Me.lbl_Connecteur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Type
        '
        Me.lbl_Type.AutoSize = True
        Me.lbl_Type.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Type.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Type.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Type.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Type.Name = "lbl_Type"
        Me.lbl_Type.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Type.TabIndex = 1
        Me.lbl_Type.Text = "lbl_Type"
        Me.lbl_Type.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_TypeConnecteur
        '
        Me.pan_TypeConnecteur.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_TypeConnecteur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_TypeConnecteur.Controls.Add(Me.img_info)
        Me.pan_TypeConnecteur.Controls.Add(Me.rdb_Armatures)
        Me.pan_TypeConnecteur.Controls.Add(Me.rdb_GoujonAme)
        Me.pan_TypeConnecteur.Controls.Add(Me.rdb_GoujonSemSup)
        Me.pan_TypeConnecteur.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_TypeConnecteur.Location = New System.Drawing.Point(0, 30)
        Me.pan_TypeConnecteur.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_TypeConnecteur.Name = "pan_TypeConnecteur"
        Me.pan_TypeConnecteur.Size = New System.Drawing.Size(250, 89)
        Me.pan_TypeConnecteur.TabIndex = 2
        '
        'img_info
        '
        Me.img_info.Image = CType(resources.GetObject("img_info.Image"), System.Drawing.Image)
        Me.img_info.Location = New System.Drawing.Point(226, 58)
        Me.img_info.Margin = New System.Windows.Forms.Padding(0)
        Me.img_info.Name = "img_info"
        Me.img_info.Size = New System.Drawing.Size(20, 20)
        Me.img_info.TabIndex = 81
        Me.img_info.TabStop = False
        '
        'rdb_Armatures
        '
        Me.rdb_Armatures.AutoSize = True
        Me.rdb_Armatures.Location = New System.Drawing.Point(14, 59)
        Me.rdb_Armatures.Name = "rdb_Armatures"
        Me.rdb_Armatures.Size = New System.Drawing.Size(93, 17)
        Me.rdb_Armatures.TabIndex = 2
        Me.rdb_Armatures.TabStop = True
        Me.rdb_Armatures.Text = "rdb_Armatures"
        Me.rdb_Armatures.UseVisualStyleBackColor = True
        '
        'rdb_GoujonAme
        '
        Me.rdb_GoujonAme.AutoSize = True
        Me.rdb_GoujonAme.Location = New System.Drawing.Point(14, 35)
        Me.rdb_GoujonAme.Name = "rdb_GoujonAme"
        Me.rdb_GoujonAme.Size = New System.Drawing.Size(101, 17)
        Me.rdb_GoujonAme.TabIndex = 1
        Me.rdb_GoujonAme.TabStop = True
        Me.rdb_GoujonAme.Text = "rdb_GoujonAme"
        Me.rdb_GoujonAme.UseVisualStyleBackColor = True
        '
        'rdb_GoujonSemSup
        '
        Me.rdb_GoujonSemSup.AutoSize = True
        Me.rdb_GoujonSemSup.Location = New System.Drawing.Point(14, 12)
        Me.rdb_GoujonSemSup.Name = "rdb_GoujonSemSup"
        Me.rdb_GoujonSemSup.Size = New System.Drawing.Size(120, 17)
        Me.rdb_GoujonSemSup.TabIndex = 0
        Me.rdb_GoujonSemSup.TabStop = True
        Me.rdb_GoujonSemSup.Text = "rdb_GoujonSemSup"
        Me.rdb_GoujonSemSup.UseVisualStyleBackColor = True
        '
        'pan_ConteneurConnecteur
        '
        Me.pan_ConteneurConnecteur.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ConteneurConnecteur.Location = New System.Drawing.Point(0, 150)
        Me.pan_ConteneurConnecteur.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ConteneurConnecteur.Name = "pan_ConteneurConnecteur"
        Me.pan_ConteneurConnecteur.Size = New System.Drawing.Size(250, 216)
        Me.pan_ConteneurConnecteur.TabIndex = 4
        '
        'pan_SaisieGoujons
        '
        Me.pan_SaisieGoujons.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieGoujons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieGoujons.Controls.Add(Me.etq_UnitPRd)
        Me.pan_SaisieGoujons.Controls.Add(Me.txt_PRd)
        Me.pan_SaisieGoujons.Controls.Add(Me.img_PRd)
        Me.pan_SaisieGoujons.Controls.Add(Me.pan_Avertissement)
        Me.pan_SaisieGoujons.Controls.Add(Me.lbl_Stud)
        Me.pan_SaisieGoujons.Controls.Add(Me.etq_UnitFu)
        Me.pan_SaisieGoujons.Controls.Add(Me.etq_UnitD)
        Me.pan_SaisieGoujons.Controls.Add(Me.etq_UnitHsc)
        Me.pan_SaisieGoujons.Controls.Add(Me.txt_fu)
        Me.pan_SaisieGoujons.Controls.Add(Me.txt_d)
        Me.pan_SaisieGoujons.Controls.Add(Me.txt_hsc)
        Me.pan_SaisieGoujons.Controls.Add(Me.img_fu)
        Me.pan_SaisieGoujons.Controls.Add(Me.img_d)
        Me.pan_SaisieGoujons.Controls.Add(Me.img_hsc)
        Me.pan_SaisieGoujons.Controls.Add(Me.cmb_goujons)
        Me.pan_SaisieGoujons.Location = New System.Drawing.Point(665, 37)
        Me.pan_SaisieGoujons.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_SaisieGoujons.Name = "pan_SaisieGoujons"
        Me.pan_SaisieGoujons.Size = New System.Drawing.Size(270, 244)
        Me.pan_SaisieGoujons.TabIndex = 2
        '
        'etq_UnitPRd
        '
        Me.etq_UnitPRd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitPRd.AutoSize = True
        Me.etq_UnitPRd.Location = New System.Drawing.Point(202, 138)
        Me.etq_UnitPRd.Name = "etq_UnitPRd"
        Me.etq_UnitPRd.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitPRd.TabIndex = 81
        Me.etq_UnitPRd.Text = "kN"
        '
        'txt_PRd
        '
        Me.txt_PRd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PRd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRd.Location = New System.Drawing.Point(138, 134)
        Me.txt_PRd.Name = "txt_PRd"
        Me.txt_PRd.Size = New System.Drawing.Size(58, 20)
        Me.txt_PRd.TabIndex = 79
        '
        'img_PRd
        '
        Me.img_PRd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PRd.Location = New System.Drawing.Point(93, 134)
        Me.img_PRd.Name = "img_PRd"
        Me.img_PRd.Size = New System.Drawing.Size(46, 20)
        Me.img_PRd.TabIndex = 80
        Me.img_PRd.TabStop = False
        '
        'pan_Avertissement
        '
        Me.pan_Avertissement.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Avertissement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Avertissement.Controls.Add(Me.lbl_Avertissement)
        Me.pan_Avertissement.Controls.Add(Me.PictureBox2)
        Me.pan_Avertissement.Location = New System.Drawing.Point(6, 168)
        Me.pan_Avertissement.Name = "pan_Avertissement"
        Me.pan_Avertissement.Size = New System.Drawing.Size(253, 71)
        Me.pan_Avertissement.TabIndex = 78
        '
        'lbl_Avertissement
        '
        Me.lbl_Avertissement.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Avertissement.Location = New System.Drawing.Point(26, 3)
        Me.lbl_Avertissement.Name = "lbl_Avertissement"
        Me.lbl_Avertissement.Size = New System.Drawing.Size(213, 64)
        Me.lbl_Avertissement.TabIndex = 83
        Me.lbl_Avertissement.Text = "lbl_Avertissement"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(20, 20)
        Me.PictureBox2.TabIndex = 82
        Me.PictureBox2.TabStop = False
        '
        'lbl_Stud
        '
        Me.lbl_Stud.AutoSize = True
        Me.lbl_Stud.Location = New System.Drawing.Point(3, 23)
        Me.lbl_Stud.Name = "lbl_Stud"
        Me.lbl_Stud.Size = New System.Drawing.Size(45, 13)
        Me.lbl_Stud.TabIndex = 77
        Me.lbl_Stud.Text = "lbl_Stud"
        '
        'etq_UnitFu
        '
        Me.etq_UnitFu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitFu.AutoSize = True
        Me.etq_UnitFu.Location = New System.Drawing.Point(202, 105)
        Me.etq_UnitFu.Name = "etq_UnitFu"
        Me.etq_UnitFu.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitFu.TabIndex = 75
        Me.etq_UnitFu.Text = "kN"
        '
        'etq_UnitD
        '
        Me.etq_UnitD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitD.AutoSize = True
        Me.etq_UnitD.Location = New System.Drawing.Point(202, 79)
        Me.etq_UnitD.Name = "etq_UnitD"
        Me.etq_UnitD.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitD.TabIndex = 75
        Me.etq_UnitD.Text = "kN"
        '
        'etq_UnitHsc
        '
        Me.etq_UnitHsc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitHsc.AutoSize = True
        Me.etq_UnitHsc.Location = New System.Drawing.Point(202, 53)
        Me.etq_UnitHsc.Name = "etq_UnitHsc"
        Me.etq_UnitHsc.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitHsc.TabIndex = 75
        Me.etq_UnitHsc.Text = "kN"
        '
        'txt_fu
        '
        Me.txt_fu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_fu.Location = New System.Drawing.Point(138, 101)
        Me.txt_fu.Name = "txt_fu"
        Me.txt_fu.Size = New System.Drawing.Size(58, 20)
        Me.txt_fu.TabIndex = 73
        '
        'txt_d
        '
        Me.txt_d.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_d.Location = New System.Drawing.Point(138, 75)
        Me.txt_d.Name = "txt_d"
        Me.txt_d.Size = New System.Drawing.Size(58, 20)
        Me.txt_d.TabIndex = 73
        '
        'txt_hsc
        '
        Me.txt_hsc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_hsc.Location = New System.Drawing.Point(138, 49)
        Me.txt_hsc.Name = "txt_hsc"
        Me.txt_hsc.Size = New System.Drawing.Size(58, 20)
        Me.txt_hsc.TabIndex = 73
        '
        'img_fu
        '
        Me.img_fu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_fu.Location = New System.Drawing.Point(93, 101)
        Me.img_fu.Name = "img_fu"
        Me.img_fu.Size = New System.Drawing.Size(46, 20)
        Me.img_fu.TabIndex = 74
        Me.img_fu.TabStop = False
        '
        'img_d
        '
        Me.img_d.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_d.Location = New System.Drawing.Point(93, 75)
        Me.img_d.Name = "img_d"
        Me.img_d.Size = New System.Drawing.Size(46, 20)
        Me.img_d.TabIndex = 74
        Me.img_d.TabStop = False
        '
        'img_hsc
        '
        Me.img_hsc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_hsc.Location = New System.Drawing.Point(93, 49)
        Me.img_hsc.Name = "img_hsc"
        Me.img_hsc.Size = New System.Drawing.Size(46, 20)
        Me.img_hsc.TabIndex = 74
        Me.img_hsc.TabStop = False
        '
        'cmb_goujons
        '
        Me.cmb_goujons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_goujons.FormattingEnabled = True
        Me.cmb_goujons.Location = New System.Drawing.Point(120, 20)
        Me.cmb_goujons.Name = "cmb_goujons"
        Me.cmb_goujons.Size = New System.Drawing.Size(114, 21)
        Me.cmb_goujons.TabIndex = 0
        '
        'pan_SaisieArmature
        '
        Me.pan_SaisieArmature.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieArmature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieArmature.Controls.Add(Me.Label1)
        Me.pan_SaisieArmature.Controls.Add(Me.txt_PRdArma)
        Me.pan_SaisieArmature.Controls.Add(Me.img_PRd2)
        Me.pan_SaisieArmature.Controls.Add(Me.cmb_Diametre)
        Me.pan_SaisieArmature.Controls.Add(Me.lbl_ClasseA)
        Me.pan_SaisieArmature.Controls.Add(Me.lbl_Diameter)
        Me.pan_SaisieArmature.Controls.Add(Me.cmb_Acier)
        Me.pan_SaisieArmature.Controls.Add(Me.etq_UnitFsk)
        Me.pan_SaisieArmature.Controls.Add(Me.etq_UnitPhiS)
        Me.pan_SaisieArmature.Controls.Add(Me.txt_Fsk)
        Me.pan_SaisieArmature.Controls.Add(Me.img_Fsk)
        Me.pan_SaisieArmature.Controls.Add(Me.img_PhiS)
        Me.pan_SaisieArmature.Location = New System.Drawing.Point(665, 356)
        Me.pan_SaisieArmature.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_SaisieArmature.Name = "pan_SaisieArmature"
        Me.pan_SaisieArmature.Size = New System.Drawing.Size(270, 205)
        Me.pan_SaisieArmature.TabIndex = 3
        '
        'cmb_Diametre
        '
        Me.cmb_Diametre.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Diametre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Diametre.FormattingEnabled = True
        Me.cmb_Diametre.Location = New System.Drawing.Point(138, 15)
        Me.cmb_Diametre.Name = "cmb_Diametre"
        Me.cmb_Diametre.Size = New System.Drawing.Size(71, 21)
        Me.cmb_Diametre.TabIndex = 88
        '
        'lbl_ClasseA
        '
        Me.lbl_ClasseA.AutoSize = True
        Me.lbl_ClasseA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ClasseA.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_ClasseA.Location = New System.Drawing.Point(7, 57)
        Me.lbl_ClasseA.Name = "lbl_ClasseA"
        Me.lbl_ClasseA.Size = New System.Drawing.Size(61, 13)
        Me.lbl_ClasseA.TabIndex = 87
        Me.lbl_ClasseA.Text = "lbl_ClasseA"
        Me.lbl_ClasseA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Diameter
        '
        Me.lbl_Diameter.AutoSize = True
        Me.lbl_Diameter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Diameter.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Diameter.Location = New System.Drawing.Point(7, 19)
        Me.lbl_Diameter.Name = "lbl_Diameter"
        Me.lbl_Diameter.Size = New System.Drawing.Size(65, 13)
        Me.lbl_Diameter.TabIndex = 86
        Me.lbl_Diameter.Text = "lbl_Diameter"
        Me.lbl_Diameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmb_Acier
        '
        Me.cmb_Acier.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Acier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Acier.FormattingEnabled = True
        Me.cmb_Acier.Location = New System.Drawing.Point(138, 54)
        Me.cmb_Acier.Name = "cmb_Acier"
        Me.cmb_Acier.Size = New System.Drawing.Size(85, 21)
        Me.cmb_Acier.TabIndex = 84
        '
        'etq_UnitFsk
        '
        Me.etq_UnitFsk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitFsk.AutoSize = True
        Me.etq_UnitFsk.Location = New System.Drawing.Point(202, 84)
        Me.etq_UnitFsk.Name = "etq_UnitFsk"
        Me.etq_UnitFsk.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitFsk.TabIndex = 83
        Me.etq_UnitFsk.Text = "kN"
        '
        'etq_UnitPhiS
        '
        Me.etq_UnitPhiS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitPhiS.AutoSize = True
        Me.etq_UnitPhiS.Location = New System.Drawing.Point(225, 19)
        Me.etq_UnitPhiS.Name = "etq_UnitPhiS"
        Me.etq_UnitPhiS.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitPhiS.TabIndex = 83
        Me.etq_UnitPhiS.Text = "kN"
        Me.etq_UnitPhiS.Visible = False
        '
        'txt_Fsk
        '
        Me.txt_Fsk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Fsk.Location = New System.Drawing.Point(138, 80)
        Me.txt_Fsk.Name = "txt_Fsk"
        Me.txt_Fsk.ReadOnly = True
        Me.txt_Fsk.Size = New System.Drawing.Size(58, 20)
        Me.txt_Fsk.TabIndex = 81
        '
        'img_Fsk
        '
        Me.img_Fsk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Fsk.Location = New System.Drawing.Point(93, 80)
        Me.img_Fsk.Name = "img_Fsk"
        Me.img_Fsk.Size = New System.Drawing.Size(46, 20)
        Me.img_Fsk.TabIndex = 82
        Me.img_Fsk.TabStop = False
        '
        'img_PhiS
        '
        Me.img_PhiS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PhiS.Location = New System.Drawing.Point(93, 15)
        Me.img_PhiS.Name = "img_PhiS"
        Me.img_PhiS.Size = New System.Drawing.Size(46, 20)
        Me.img_PhiS.TabIndex = 82
        Me.img_PhiS.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(202, 129)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(21, 13)
        Me.Label1.TabIndex = 91
        Me.Label1.Text = "kN"
        '
        'txt_PRdArma
        '
        Me.txt_PRdArma.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PRdArma.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PRdArma.Location = New System.Drawing.Point(138, 125)
        Me.txt_PRdArma.Name = "txt_PRdArma"
        Me.txt_PRdArma.Size = New System.Drawing.Size(58, 20)
        Me.txt_PRdArma.TabIndex = 89
        '
        'img_PRd2
        '
        Me.img_PRd2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PRd2.Location = New System.Drawing.Point(93, 125)
        Me.img_PRd2.Name = "img_PRd2"
        Me.img_PRd2.Size = New System.Drawing.Size(46, 20)
        Me.img_PRd2.TabIndex = 90
        Me.img_PRd2.TabStop = False
        '
        'Frm_ConnectionSlimConnecteur
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 571)
        Me.Controls.Add(Me.pan_SaisieArmature)
        Me.Controls.Add(Me.pan_SaisieGoujons)
        Me.Controls.Add(Me.pan_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_ConnectionSlimConnecteur"
        Me.Text = "Frm_ConnectionSlimConnecteur"
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Main.ResumeLayout(False)
        Me.pan_Image.ResumeLayout(False)
        CType(Me.img_Stud, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLPan_Saisies.ResumeLayout(False)
        Me.TLPan_Saisies.PerformLayout()
        Me.pan_TypeConnecteur.ResumeLayout(False)
        Me.pan_TypeConnecteur.PerformLayout()
        CType(Me.img_info, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_SaisieGoujons.ResumeLayout(False)
        Me.pan_SaisieGoujons.PerformLayout()
        CType(Me.img_PRd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Avertissement.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_fu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_d, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_hsc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_SaisieArmature.ResumeLayout(False)
        Me.pan_SaisieArmature.PerformLayout()
        CType(Me.img_Fsk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_PhiS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_PRd2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Main As TableLayoutPanel
    Friend WithEvents pan_Image As Panel
    Friend WithEvents img_Stud As PictureBox
    Friend WithEvents TLPan_Saisies As TableLayoutPanel
    Friend WithEvents lbl_Connecteur As Label
    Friend WithEvents lbl_Type As Label
    Friend WithEvents pan_TypeConnecteur As Panel
    Friend WithEvents rdb_GoujonSemSup As RadioButton
    Friend WithEvents rdb_Armatures As RadioButton
    Friend WithEvents rdb_GoujonAme As RadioButton
    Friend WithEvents pan_ConteneurConnecteur As Panel
    Friend WithEvents pan_SaisieGoujons As Panel
    Friend WithEvents etq_UnitFu As Label
    Friend WithEvents etq_UnitD As Label
    Friend WithEvents etq_UnitHsc As Label
    Friend WithEvents txt_fu As TextBox
    Friend WithEvents txt_d As TextBox
    Friend WithEvents txt_hsc As TextBox
    Friend WithEvents img_fu As PictureBox
    Friend WithEvents img_d As PictureBox
    Friend WithEvents img_hsc As PictureBox
    Friend WithEvents cmb_goujons As ComboBox
    Friend WithEvents pan_SaisieArmature As Panel
    Friend WithEvents cmb_Acier As ComboBox
    Friend WithEvents etq_UnitFsk As Label
    Friend WithEvents etq_UnitPhiS As Label
    Friend WithEvents txt_Fsk As TextBox
    Friend WithEvents img_Fsk As PictureBox
    Friend WithEvents img_PhiS As PictureBox
    Friend WithEvents lbl_Stud As Label
    Friend WithEvents img_info As PictureBox
    Friend WithEvents pan_Avertissement As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents lbl_Avertissement As Label
    Friend WithEvents lbl_Diameter As Label
    Friend WithEvents lbl_ClasseA As Label
    Friend WithEvents cmb_Diametre As ComboBox
    Friend WithEvents etq_UnitPRd As Label
    Friend WithEvents txt_PRd As TextBox
    Friend WithEvents img_PRd As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_PRdArma As TextBox
    Friend WithEvents img_PRd2 As PictureBox
End Class
