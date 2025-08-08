<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_DalleSlimFloorNGeneral
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
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Masses = New System.Windows.Forms.Label()
        Me.pan_Beton = New System.Windows.Forms.Panel()
        Me.chk_BetonLeger = New System.Windows.Forms.CheckBox()
        Me.txt_RhoC = New System.Windows.Forms.TextBox()
        Me.img_RhoC = New System.Windows.Forms.PictureBox()
        Me.etq_UnitRhoC = New System.Windows.Forms.Label()
        Me.cmb_ClasseBetonDalle = New System.Windows.Forms.ComboBox()
        Me.lbl_ClasseE = New System.Windows.Forms.Label()
        Me.lbl_Beton = New System.Windows.Forms.Label()
        Me.pan_Type = New System.Windows.Forms.Panel()
        Me.pan_OptionRive = New System.Windows.Forms.Panel()
        Me.chk_ChambreRivePleine = New System.Windows.Forms.CheckBox()
        Me.lbl_TypeDalle = New System.Windows.Forms.Label()
        Me.cmb_TypeDalle = New System.Windows.Forms.ComboBox()
        Me.lbl_General = New System.Windows.Forms.Label()
        Me.pan_Masses = New System.Windows.Forms.Panel()
        Me.pan_EpaisseurMixte = New System.Windows.Forms.Panel()
        Me.rdb_EpPleine = New System.Windows.Forms.RadioButton()
        Me.rdb_EpTotale = New System.Windows.Forms.RadioButton()
        Me.txt_Tc = New System.Windows.Forms.TextBox()
        Me.etq_UnitDim11 = New System.Windows.Forms.Label()
        Me.txt_Td2 = New System.Windows.Forms.TextBox()
        Me.img_Tc = New System.Windows.Forms.PictureBox()
        Me.lbl_EpaisseurM = New System.Windows.Forms.Label()
        Me.etq_UnitDim10 = New System.Windows.Forms.Label()
        Me.img_Td2 = New System.Windows.Forms.PictureBox()
        Me.pan_Epaisseur = New System.Windows.Forms.Panel()
        Me.txt_Hd = New System.Windows.Forms.TextBox()
        Me.lbl_Epaisseur = New System.Windows.Forms.Label()
        Me.etq_UnitDim2 = New System.Windows.Forms.Label()
        Me.Img_Hd = New System.Windows.Forms.PictureBox()
        Me.pan_Predalle = New System.Windows.Forms.Panel()
        Me.txt_EpJoint = New System.Windows.Forms.TextBox()
        Me.lbl_EpJoint = New System.Windows.Forms.Label()
        Me.img_EpJoint = New System.Windows.Forms.PictureBox()
        Me.etq_UnitDim9 = New System.Windows.Forms.Label()
        Me.txt_EpPredalle = New System.Windows.Forms.TextBox()
        Me.lbl_EpPreDalle = New System.Windows.Forms.Label()
        Me.img_EpPredalle = New System.Windows.Forms.PictureBox()
        Me.etq_UnitDim8 = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_Cofradal = New System.Windows.Forms.Panel()
        Me.cmb_Cofradal = New System.Windows.Forms.ComboBox()
        Me.txt_mupf = New System.Windows.Forms.TextBox()
        Me.txt_NameCustomCofra = New System.Windows.Forms.TextBox()
        Me.txt_dp = New System.Windows.Forms.TextBox()
        Me.lbl_mupf = New System.Windows.Forms.Label()
        Me.lbl_Name = New System.Windows.Forms.Label()
        Me.img_mupf = New System.Windows.Forms.PictureBox()
        Me.lbl_dp = New System.Windows.Forms.Label()
        Me.img_dp = New System.Windows.Forms.PictureBox()
        Me.etq_UnitDim13 = New System.Windows.Forms.Label()
        Me.lbl_Cofradal = New System.Windows.Forms.Label()
        Me.etq_UnitDim12 = New System.Windows.Forms.Label()
        Me.txt_MassSurf = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.etq_UnitMassSurf = New System.Windows.Forms.Label()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Gauche.SuspendLayout()
        Me.pan_Beton.SuspendLayout()
        CType(Me.img_RhoC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Type.SuspendLayout()
        Me.pan_OptionRive.SuspendLayout()
        Me.pan_Masses.SuspendLayout()
        Me.pan_EpaisseurMixte.SuspendLayout()
        CType(Me.img_Tc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Td2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Epaisseur.SuspendLayout()
        CType(Me.Img_Hd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Predalle.SuspendLayout()
        CType(Me.img_EpJoint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_EpPredalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Cofradal.SuspendLayout()
        CType(Me.img_mupf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_dp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.Controls.Add(Me.TLpan_Gauche)
        Me.pan_Main.Location = New System.Drawing.Point(208, 51)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(357, 451)
        Me.pan_Main.TabIndex = 0
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.lbl_Masses, 0, 4)
        Me.TLpan_Gauche.Controls.Add(Me.pan_Beton, 0, 3)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_Beton, 0, 2)
        Me.TLpan_Gauche.Controls.Add(Me.pan_Type, 0, 1)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_General, 0, 0)
        Me.TLpan_Gauche.Controls.Add(Me.pan_Masses, 0, 5)
        Me.TLpan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Gauche.Name = "TLpan_Gauche"
        Me.TLpan_Gauche.RowCount = 6
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Gauche.Size = New System.Drawing.Size(357, 451)
        Me.TLpan_Gauche.TabIndex = 1
        '
        'lbl_Masses
        '
        Me.lbl_Masses.AutoSize = True
        Me.lbl_Masses.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Masses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Masses.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Masses.Location = New System.Drawing.Point(0, 290)
        Me.lbl_Masses.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Masses.Name = "lbl_Masses"
        Me.lbl_Masses.Size = New System.Drawing.Size(357, 30)
        Me.lbl_Masses.TabIndex = 10
        Me.lbl_Masses.Text = "lbl_Masses"
        Me.lbl_Masses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Beton
        '
        Me.pan_Beton.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Beton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Beton.Controls.Add(Me.chk_BetonLeger)
        Me.pan_Beton.Controls.Add(Me.txt_RhoC)
        Me.pan_Beton.Controls.Add(Me.img_RhoC)
        Me.pan_Beton.Controls.Add(Me.etq_UnitRhoC)
        Me.pan_Beton.Controls.Add(Me.cmb_ClasseBetonDalle)
        Me.pan_Beton.Controls.Add(Me.lbl_ClasseE)
        Me.pan_Beton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Beton.Location = New System.Drawing.Point(0, 200)
        Me.pan_Beton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Beton.Name = "pan_Beton"
        Me.pan_Beton.Size = New System.Drawing.Size(357, 89)
        Me.pan_Beton.TabIndex = 9
        '
        'chk_BetonLeger
        '
        Me.chk_BetonLeger.AutoSize = True
        Me.chk_BetonLeger.Location = New System.Drawing.Point(13, 9)
        Me.chk_BetonLeger.Name = "chk_BetonLeger"
        Me.chk_BetonLeger.Size = New System.Drawing.Size(105, 17)
        Me.chk_BetonLeger.TabIndex = 64
        Me.chk_BetonLeger.Text = "chk_BetonLeger"
        Me.chk_BetonLeger.UseVisualStyleBackColor = True
        '
        'txt_RhoC
        '
        Me.txt_RhoC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_RhoC.Location = New System.Drawing.Point(234, 56)
        Me.txt_RhoC.Name = "txt_RhoC"
        Me.txt_RhoC.Size = New System.Drawing.Size(58, 20)
        Me.txt_RhoC.TabIndex = 62
        '
        'img_RhoC
        '
        Me.img_RhoC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_RhoC.Location = New System.Drawing.Point(189, 56)
        Me.img_RhoC.Name = "img_RhoC"
        Me.img_RhoC.Size = New System.Drawing.Size(46, 20)
        Me.img_RhoC.TabIndex = 63
        Me.img_RhoC.TabStop = False
        '
        'etq_UnitRhoC
        '
        Me.etq_UnitRhoC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitRhoC.AutoSize = True
        Me.etq_UnitRhoC.Location = New System.Drawing.Point(298, 59)
        Me.etq_UnitRhoC.Name = "etq_UnitRhoC"
        Me.etq_UnitRhoC.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitRhoC.TabIndex = 61
        Me.etq_UnitRhoC.Text = "mm"
        '
        'cmb_ClasseBetonDalle
        '
        Me.cmb_ClasseBetonDalle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_ClasseBetonDalle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_ClasseBetonDalle.FormattingEnabled = True
        Me.cmb_ClasseBetonDalle.Location = New System.Drawing.Point(128, 30)
        Me.cmb_ClasseBetonDalle.Name = "cmb_ClasseBetonDalle"
        Me.cmb_ClasseBetonDalle.Size = New System.Drawing.Size(217, 21)
        Me.cmb_ClasseBetonDalle.TabIndex = 57
        '
        'lbl_ClasseE
        '
        Me.lbl_ClasseE.AutoSize = True
        Me.lbl_ClasseE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ClasseE.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_ClasseE.Location = New System.Drawing.Point(11, 33)
        Me.lbl_ClasseE.Name = "lbl_ClasseE"
        Me.lbl_ClasseE.Size = New System.Drawing.Size(61, 13)
        Me.lbl_ClasseE.TabIndex = 56
        Me.lbl_ClasseE.Text = "lbl_ClasseE"
        Me.lbl_ClasseE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Beton
        '
        Me.lbl_Beton.AutoSize = True
        Me.lbl_Beton.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Beton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Beton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Beton.Location = New System.Drawing.Point(0, 170)
        Me.lbl_Beton.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Beton.Name = "lbl_Beton"
        Me.lbl_Beton.Size = New System.Drawing.Size(357, 30)
        Me.lbl_Beton.TabIndex = 8
        Me.lbl_Beton.Text = "lbl_Beton"
        Me.lbl_Beton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Type
        '
        Me.pan_Type.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Type.Controls.Add(Me.pan_OptionRive)
        Me.pan_Type.Controls.Add(Me.lbl_TypeDalle)
        Me.pan_Type.Controls.Add(Me.cmb_TypeDalle)
        Me.pan_Type.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Type.Location = New System.Drawing.Point(0, 30)
        Me.pan_Type.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Type.Name = "pan_Type"
        Me.pan_Type.Size = New System.Drawing.Size(357, 139)
        Me.pan_Type.TabIndex = 7
        '
        'pan_OptionRive
        '
        Me.pan_OptionRive.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_OptionRive.Controls.Add(Me.chk_ChambreRivePleine)
        Me.pan_OptionRive.Location = New System.Drawing.Point(16, 35)
        Me.pan_OptionRive.Name = "pan_OptionRive"
        Me.pan_OptionRive.Size = New System.Drawing.Size(329, 23)
        Me.pan_OptionRive.TabIndex = 93
        '
        'chk_ChambreRivePleine
        '
        Me.chk_ChambreRivePleine.AutoSize = True
        Me.chk_ChambreRivePleine.Location = New System.Drawing.Point(3, 3)
        Me.chk_ChambreRivePleine.Name = "chk_ChambreRivePleine"
        Me.chk_ChambreRivePleine.Size = New System.Drawing.Size(143, 17)
        Me.chk_ChambreRivePleine.TabIndex = 65
        Me.chk_ChambreRivePleine.Text = "chk_ChambreRivePleine"
        Me.chk_ChambreRivePleine.UseVisualStyleBackColor = True
        '
        'lbl_TypeDalle
        '
        Me.lbl_TypeDalle.AutoSize = True
        Me.lbl_TypeDalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_TypeDalle.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_TypeDalle.Location = New System.Drawing.Point(13, 11)
        Me.lbl_TypeDalle.Name = "lbl_TypeDalle"
        Me.lbl_TypeDalle.Size = New System.Drawing.Size(71, 13)
        Me.lbl_TypeDalle.TabIndex = 55
        Me.lbl_TypeDalle.Text = "lbl_TypeDalle"
        Me.lbl_TypeDalle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmb_TypeDalle
        '
        Me.cmb_TypeDalle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_TypeDalle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_TypeDalle.FormattingEnabled = True
        Me.cmb_TypeDalle.Location = New System.Drawing.Point(96, 8)
        Me.cmb_TypeDalle.Name = "cmb_TypeDalle"
        Me.cmb_TypeDalle.Size = New System.Drawing.Size(249, 21)
        Me.cmb_TypeDalle.TabIndex = 56
        '
        'lbl_General
        '
        Me.lbl_General.AutoSize = True
        Me.lbl_General.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_General.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_General.Location = New System.Drawing.Point(0, 0)
        Me.lbl_General.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_General.Name = "lbl_General"
        Me.lbl_General.Size = New System.Drawing.Size(357, 30)
        Me.lbl_General.TabIndex = 2
        Me.lbl_General.Text = "lbl_General"
        Me.lbl_General.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Masses
        '
        Me.pan_Masses.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Masses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Masses.Controls.Add(Me.txt_MassSurf)
        Me.pan_Masses.Controls.Add(Me.PictureBox1)
        Me.pan_Masses.Controls.Add(Me.etq_UnitMassSurf)
        Me.pan_Masses.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Masses.Location = New System.Drawing.Point(0, 320)
        Me.pan_Masses.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Masses.Name = "pan_Masses"
        Me.pan_Masses.Size = New System.Drawing.Size(357, 131)
        Me.pan_Masses.TabIndex = 11
        '
        'pan_EpaisseurMixte
        '
        Me.pan_EpaisseurMixte.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_EpaisseurMixte.Controls.Add(Me.rdb_EpPleine)
        Me.pan_EpaisseurMixte.Controls.Add(Me.rdb_EpTotale)
        Me.pan_EpaisseurMixte.Controls.Add(Me.txt_Tc)
        Me.pan_EpaisseurMixte.Controls.Add(Me.etq_UnitDim11)
        Me.pan_EpaisseurMixte.Controls.Add(Me.txt_Td2)
        Me.pan_EpaisseurMixte.Controls.Add(Me.img_Tc)
        Me.pan_EpaisseurMixte.Controls.Add(Me.lbl_EpaisseurM)
        Me.pan_EpaisseurMixte.Controls.Add(Me.etq_UnitDim10)
        Me.pan_EpaisseurMixte.Controls.Add(Me.img_Td2)
        Me.pan_EpaisseurMixte.Location = New System.Drawing.Point(657, 206)
        Me.pan_EpaisseurMixte.Name = "pan_EpaisseurMixte"
        Me.pan_EpaisseurMixte.Size = New System.Drawing.Size(238, 52)
        Me.pan_EpaisseurMixte.TabIndex = 91
        '
        'rdb_EpPleine
        '
        Me.rdb_EpPleine.AutoSize = True
        Me.rdb_EpPleine.Location = New System.Drawing.Point(91, 29)
        Me.rdb_EpPleine.Name = "rdb_EpPleine"
        Me.rdb_EpPleine.Size = New System.Drawing.Size(14, 13)
        Me.rdb_EpPleine.TabIndex = 93
        Me.rdb_EpPleine.TabStop = True
        Me.rdb_EpPleine.UseVisualStyleBackColor = True
        '
        'rdb_EpTotale
        '
        Me.rdb_EpTotale.AutoSize = True
        Me.rdb_EpTotale.Location = New System.Drawing.Point(91, 8)
        Me.rdb_EpTotale.Name = "rdb_EpTotale"
        Me.rdb_EpTotale.Size = New System.Drawing.Size(14, 13)
        Me.rdb_EpTotale.TabIndex = 92
        Me.rdb_EpTotale.TabStop = True
        Me.rdb_EpTotale.UseVisualStyleBackColor = True
        '
        'txt_Tc
        '
        Me.txt_Tc.Location = New System.Drawing.Point(144, 25)
        Me.txt_Tc.Name = "txt_Tc"
        Me.txt_Tc.Size = New System.Drawing.Size(58, 20)
        Me.txt_Tc.TabIndex = 90
        '
        'etq_UnitDim11
        '
        Me.etq_UnitDim11.AutoSize = True
        Me.etq_UnitDim11.Location = New System.Drawing.Point(209, 28)
        Me.etq_UnitDim11.Name = "etq_UnitDim11"
        Me.etq_UnitDim11.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim11.TabIndex = 89
        Me.etq_UnitDim11.Text = "mm"
        '
        'txt_Td2
        '
        Me.txt_Td2.Location = New System.Drawing.Point(144, 4)
        Me.txt_Td2.Name = "txt_Td2"
        Me.txt_Td2.Size = New System.Drawing.Size(58, 20)
        Me.txt_Td2.TabIndex = 75
        '
        'img_Tc
        '
        Me.img_Tc.Location = New System.Drawing.Point(109, 25)
        Me.img_Tc.Name = "img_Tc"
        Me.img_Tc.Size = New System.Drawing.Size(37, 20)
        Me.img_Tc.TabIndex = 91
        Me.img_Tc.TabStop = False
        '
        'lbl_EpaisseurM
        '
        Me.lbl_EpaisseurM.AutoSize = True
        Me.lbl_EpaisseurM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_EpaisseurM.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_EpaisseurM.Location = New System.Drawing.Point(7, 6)
        Me.lbl_EpaisseurM.Name = "lbl_EpaisseurM"
        Me.lbl_EpaisseurM.Size = New System.Drawing.Size(78, 13)
        Me.lbl_EpaisseurM.TabIndex = 73
        Me.lbl_EpaisseurM.Text = "lbl_EpaisseurM"
        Me.lbl_EpaisseurM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_UnitDim10
        '
        Me.etq_UnitDim10.AutoSize = True
        Me.etq_UnitDim10.Location = New System.Drawing.Point(209, 7)
        Me.etq_UnitDim10.Name = "etq_UnitDim10"
        Me.etq_UnitDim10.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim10.TabIndex = 74
        Me.etq_UnitDim10.Text = "mm"
        '
        'img_Td2
        '
        Me.img_Td2.Location = New System.Drawing.Point(109, 4)
        Me.img_Td2.Name = "img_Td2"
        Me.img_Td2.Size = New System.Drawing.Size(37, 20)
        Me.img_Td2.TabIndex = 76
        Me.img_Td2.TabStop = False
        '
        'pan_Epaisseur
        '
        Me.pan_Epaisseur.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Epaisseur.Controls.Add(Me.txt_Hd)
        Me.pan_Epaisseur.Controls.Add(Me.lbl_Epaisseur)
        Me.pan_Epaisseur.Controls.Add(Me.etq_UnitDim2)
        Me.pan_Epaisseur.Controls.Add(Me.Img_Hd)
        Me.pan_Epaisseur.Location = New System.Drawing.Point(657, 167)
        Me.pan_Epaisseur.Name = "pan_Epaisseur"
        Me.pan_Epaisseur.Size = New System.Drawing.Size(238, 25)
        Me.pan_Epaisseur.TabIndex = 90
        '
        'txt_Hd
        '
        Me.txt_Hd.Location = New System.Drawing.Point(144, 2)
        Me.txt_Hd.Name = "txt_Hd"
        Me.txt_Hd.Size = New System.Drawing.Size(58, 20)
        Me.txt_Hd.TabIndex = 75
        '
        'lbl_Epaisseur
        '
        Me.lbl_Epaisseur.AutoSize = True
        Me.lbl_Epaisseur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Epaisseur.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Epaisseur.Location = New System.Drawing.Point(7, 6)
        Me.lbl_Epaisseur.Name = "lbl_Epaisseur"
        Me.lbl_Epaisseur.Size = New System.Drawing.Size(69, 13)
        Me.lbl_Epaisseur.TabIndex = 73
        Me.lbl_Epaisseur.Text = "lbl_Epaisseur"
        Me.lbl_Epaisseur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_UnitDim2
        '
        Me.etq_UnitDim2.AutoSize = True
        Me.etq_UnitDim2.Location = New System.Drawing.Point(209, 5)
        Me.etq_UnitDim2.Name = "etq_UnitDim2"
        Me.etq_UnitDim2.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim2.TabIndex = 74
        Me.etq_UnitDim2.Text = "mm"
        '
        'Img_Hd
        '
        Me.Img_Hd.Location = New System.Drawing.Point(109, 2)
        Me.Img_Hd.Name = "Img_Hd"
        Me.Img_Hd.Size = New System.Drawing.Size(37, 20)
        Me.Img_Hd.TabIndex = 76
        Me.Img_Hd.TabStop = False
        '
        'pan_Predalle
        '
        Me.pan_Predalle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Predalle.Controls.Add(Me.txt_EpJoint)
        Me.pan_Predalle.Controls.Add(Me.lbl_EpJoint)
        Me.pan_Predalle.Controls.Add(Me.img_EpJoint)
        Me.pan_Predalle.Controls.Add(Me.etq_UnitDim9)
        Me.pan_Predalle.Controls.Add(Me.txt_EpPredalle)
        Me.pan_Predalle.Controls.Add(Me.lbl_EpPreDalle)
        Me.pan_Predalle.Controls.Add(Me.img_EpPredalle)
        Me.pan_Predalle.Controls.Add(Me.etq_UnitDim8)
        Me.pan_Predalle.Location = New System.Drawing.Point(657, 103)
        Me.pan_Predalle.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Predalle.Name = "pan_Predalle"
        Me.pan_Predalle.Size = New System.Drawing.Size(238, 52)
        Me.pan_Predalle.TabIndex = 89
        '
        'txt_EpJoint
        '
        Me.txt_EpJoint.Location = New System.Drawing.Point(146, 29)
        Me.txt_EpJoint.Name = "txt_EpJoint"
        Me.txt_EpJoint.Size = New System.Drawing.Size(58, 20)
        Me.txt_EpJoint.TabIndex = 83
        '
        'lbl_EpJoint
        '
        Me.lbl_EpJoint.AutoSize = True
        Me.lbl_EpJoint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_EpJoint.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_EpJoint.Location = New System.Drawing.Point(9, 29)
        Me.lbl_EpJoint.Name = "lbl_EpJoint"
        Me.lbl_EpJoint.Size = New System.Drawing.Size(58, 13)
        Me.lbl_EpJoint.TabIndex = 81
        Me.lbl_EpJoint.Text = "lbl_EpJoint"
        Me.lbl_EpJoint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_EpJoint
        '
        Me.img_EpJoint.Location = New System.Drawing.Point(110, 29)
        Me.img_EpJoint.Name = "img_EpJoint"
        Me.img_EpJoint.Size = New System.Drawing.Size(37, 20)
        Me.img_EpJoint.TabIndex = 84
        Me.img_EpJoint.TabStop = False
        '
        'etq_UnitDim9
        '
        Me.etq_UnitDim9.AutoSize = True
        Me.etq_UnitDim9.Location = New System.Drawing.Point(210, 32)
        Me.etq_UnitDim9.Name = "etq_UnitDim9"
        Me.etq_UnitDim9.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim9.TabIndex = 82
        Me.etq_UnitDim9.Text = "mm"
        '
        'txt_EpPredalle
        '
        Me.txt_EpPredalle.Location = New System.Drawing.Point(146, 7)
        Me.txt_EpPredalle.Name = "txt_EpPredalle"
        Me.txt_EpPredalle.Size = New System.Drawing.Size(58, 20)
        Me.txt_EpPredalle.TabIndex = 79
        '
        'lbl_EpPreDalle
        '
        Me.lbl_EpPreDalle.AutoSize = True
        Me.lbl_EpPreDalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_EpPreDalle.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_EpPreDalle.Location = New System.Drawing.Point(9, 7)
        Me.lbl_EpPreDalle.Name = "lbl_EpPreDalle"
        Me.lbl_EpPreDalle.Size = New System.Drawing.Size(76, 13)
        Me.lbl_EpPreDalle.TabIndex = 77
        Me.lbl_EpPreDalle.Text = "lbl_EpPreDalle"
        Me.lbl_EpPreDalle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_EpPredalle
        '
        Me.img_EpPredalle.Location = New System.Drawing.Point(110, 7)
        Me.img_EpPredalle.Name = "img_EpPredalle"
        Me.img_EpPredalle.Size = New System.Drawing.Size(37, 20)
        Me.img_EpPredalle.TabIndex = 80
        Me.img_EpPredalle.TabStop = False
        '
        'etq_UnitDim8
        '
        Me.etq_UnitDim8.AutoSize = True
        Me.etq_UnitDim8.Location = New System.Drawing.Point(210, 10)
        Me.etq_UnitDim8.Name = "etq_UnitDim8"
        Me.etq_UnitDim8.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim8.TabIndex = 78
        Me.etq_UnitDim8.Text = "mm"
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'pan_Cofradal
        '
        Me.pan_Cofradal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Cofradal.Controls.Add(Me.cmb_Cofradal)
        Me.pan_Cofradal.Controls.Add(Me.txt_mupf)
        Me.pan_Cofradal.Controls.Add(Me.txt_NameCustomCofra)
        Me.pan_Cofradal.Controls.Add(Me.txt_dp)
        Me.pan_Cofradal.Controls.Add(Me.lbl_mupf)
        Me.pan_Cofradal.Controls.Add(Me.lbl_Name)
        Me.pan_Cofradal.Controls.Add(Me.img_mupf)
        Me.pan_Cofradal.Controls.Add(Me.lbl_dp)
        Me.pan_Cofradal.Controls.Add(Me.img_dp)
        Me.pan_Cofradal.Controls.Add(Me.etq_UnitDim13)
        Me.pan_Cofradal.Controls.Add(Me.lbl_Cofradal)
        Me.pan_Cofradal.Controls.Add(Me.etq_UnitDim12)
        Me.pan_Cofradal.Location = New System.Drawing.Point(657, 265)
        Me.pan_Cofradal.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Cofradal.Name = "pan_Cofradal"
        Me.pan_Cofradal.Size = New System.Drawing.Size(238, 111)
        Me.pan_Cofradal.TabIndex = 92
        '
        'cmb_Cofradal
        '
        Me.cmb_Cofradal.FormattingEnabled = True
        Me.cmb_Cofradal.Location = New System.Drawing.Point(108, 8)
        Me.cmb_Cofradal.Name = "cmb_Cofradal"
        Me.cmb_Cofradal.Size = New System.Drawing.Size(123, 21)
        Me.cmb_Cofradal.TabIndex = 81
        '
        'txt_mupf
        '
        Me.txt_mupf.Location = New System.Drawing.Point(144, 86)
        Me.txt_mupf.Name = "txt_mupf"
        Me.txt_mupf.Size = New System.Drawing.Size(58, 20)
        Me.txt_mupf.TabIndex = 79
        '
        'txt_NameCustomCofra
        '
        Me.txt_NameCustomCofra.Location = New System.Drawing.Point(109, 35)
        Me.txt_NameCustomCofra.Name = "txt_NameCustomCofra"
        Me.txt_NameCustomCofra.Size = New System.Drawing.Size(122, 20)
        Me.txt_NameCustomCofra.TabIndex = 79
        '
        'txt_dp
        '
        Me.txt_dp.Location = New System.Drawing.Point(144, 64)
        Me.txt_dp.Name = "txt_dp"
        Me.txt_dp.Size = New System.Drawing.Size(58, 20)
        Me.txt_dp.TabIndex = 79
        '
        'lbl_mupf
        '
        Me.lbl_mupf.AutoSize = True
        Me.lbl_mupf.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_mupf.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_mupf.Location = New System.Drawing.Point(9, 86)
        Me.lbl_mupf.Name = "lbl_mupf"
        Me.lbl_mupf.Size = New System.Drawing.Size(46, 13)
        Me.lbl_mupf.TabIndex = 77
        Me.lbl_mupf.Text = "lbl_mupf"
        Me.lbl_mupf.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Name
        '
        Me.lbl_Name.AutoSize = True
        Me.lbl_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Name.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Name.Location = New System.Drawing.Point(9, 35)
        Me.lbl_Name.Name = "lbl_Name"
        Me.lbl_Name.Size = New System.Drawing.Size(51, 13)
        Me.lbl_Name.TabIndex = 77
        Me.lbl_Name.Text = "lbl_Name"
        Me.lbl_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_mupf
        '
        Me.img_mupf.Location = New System.Drawing.Point(108, 86)
        Me.img_mupf.Name = "img_mupf"
        Me.img_mupf.Size = New System.Drawing.Size(37, 20)
        Me.img_mupf.TabIndex = 80
        Me.img_mupf.TabStop = False
        '
        'lbl_dp
        '
        Me.lbl_dp.AutoSize = True
        Me.lbl_dp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_dp.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_dp.Location = New System.Drawing.Point(9, 64)
        Me.lbl_dp.Name = "lbl_dp"
        Me.lbl_dp.Size = New System.Drawing.Size(35, 13)
        Me.lbl_dp.TabIndex = 77
        Me.lbl_dp.Text = "lbl_dp"
        Me.lbl_dp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_dp
        '
        Me.img_dp.Location = New System.Drawing.Point(108, 64)
        Me.img_dp.Name = "img_dp"
        Me.img_dp.Size = New System.Drawing.Size(37, 20)
        Me.img_dp.TabIndex = 80
        Me.img_dp.TabStop = False
        '
        'etq_UnitDim13
        '
        Me.etq_UnitDim13.AutoSize = True
        Me.etq_UnitDim13.Location = New System.Drawing.Point(208, 85)
        Me.etq_UnitDim13.Name = "etq_UnitDim13"
        Me.etq_UnitDim13.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim13.TabIndex = 78
        Me.etq_UnitDim13.Text = "mm"
        '
        'lbl_Cofradal
        '
        Me.lbl_Cofradal.AutoSize = True
        Me.lbl_Cofradal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Cofradal.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Cofradal.Location = New System.Drawing.Point(9, 7)
        Me.lbl_Cofradal.Name = "lbl_Cofradal"
        Me.lbl_Cofradal.Size = New System.Drawing.Size(62, 13)
        Me.lbl_Cofradal.TabIndex = 77
        Me.lbl_Cofradal.Text = "lbl_Cofradal"
        Me.lbl_Cofradal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_UnitDim12
        '
        Me.etq_UnitDim12.AutoSize = True
        Me.etq_UnitDim12.Location = New System.Drawing.Point(208, 63)
        Me.etq_UnitDim12.Name = "etq_UnitDim12"
        Me.etq_UnitDim12.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim12.TabIndex = 78
        Me.etq_UnitDim12.Text = "mm"
        '
        'txt_MassSurf
        '
        Me.txt_MassSurf.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_MassSurf.Location = New System.Drawing.Point(234, 26)
        Me.txt_MassSurf.Name = "txt_MassSurf"
        Me.txt_MassSurf.Size = New System.Drawing.Size(58, 20)
        Me.txt_MassSurf.TabIndex = 65
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Location = New System.Drawing.Point(189, 26)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(46, 20)
        Me.PictureBox1.TabIndex = 66
        Me.PictureBox1.TabStop = False
        '
        'etq_UnitMassSurf
        '
        Me.etq_UnitMassSurf.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitMassSurf.AutoSize = True
        Me.etq_UnitMassSurf.Location = New System.Drawing.Point(298, 29)
        Me.etq_UnitMassSurf.Name = "etq_UnitMassSurf"
        Me.etq_UnitMassSurf.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitMassSurf.TabIndex = 64
        Me.etq_UnitMassSurf.Text = "mm"
        '
        'Frm_DalleSlimFloorNGeneral
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1018, 514)
        Me.Controls.Add(Me.pan_Cofradal)
        Me.Controls.Add(Me.pan_EpaisseurMixte)
        Me.Controls.Add(Me.pan_Epaisseur)
        Me.Controls.Add(Me.pan_Predalle)
        Me.Controls.Add(Me.pan_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_DalleSlimFloorNGeneral"
        Me.Text = "Frm_DalleSlimFloorNGeneral"
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.PerformLayout()
        Me.pan_Beton.ResumeLayout(False)
        Me.pan_Beton.PerformLayout()
        CType(Me.img_RhoC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Type.ResumeLayout(False)
        Me.pan_Type.PerformLayout()
        Me.pan_OptionRive.ResumeLayout(False)
        Me.pan_OptionRive.PerformLayout()
        Me.pan_Masses.ResumeLayout(False)
        Me.pan_Masses.PerformLayout()
        Me.pan_EpaisseurMixte.ResumeLayout(False)
        Me.pan_EpaisseurMixte.PerformLayout()
        CType(Me.img_Tc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Td2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Epaisseur.ResumeLayout(False)
        Me.pan_Epaisseur.PerformLayout()
        CType(Me.Img_Hd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Predalle.ResumeLayout(False)
        Me.pan_Predalle.PerformLayout()
        CType(Me.img_EpJoint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_EpPredalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Cofradal.ResumeLayout(False)
        Me.pan_Cofradal.PerformLayout()
        CType(Me.img_mupf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_dp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents pan_Beton As Panel
    Friend WithEvents chk_BetonLeger As CheckBox
    Friend WithEvents txt_RhoC As TextBox
    Friend WithEvents img_RhoC As PictureBox
    Friend WithEvents etq_UnitRhoC As Label
    Friend WithEvents cmb_ClasseBetonDalle As ComboBox
    Friend WithEvents lbl_ClasseE As Label
    Friend WithEvents lbl_Beton As Label
    Friend WithEvents pan_Type As Panel
    Friend WithEvents lbl_TypeDalle As Label
    Friend WithEvents cmb_TypeDalle As ComboBox
    Friend WithEvents lbl_General As Label
    Friend WithEvents pan_EpaisseurMixte As Panel
    Friend WithEvents rdb_EpPleine As RadioButton
    Friend WithEvents rdb_EpTotale As RadioButton
    Friend WithEvents txt_Tc As TextBox
    Friend WithEvents etq_UnitDim11 As Label
    Friend WithEvents txt_Td2 As TextBox
    Friend WithEvents img_Tc As PictureBox
    Friend WithEvents lbl_EpaisseurM As Label
    Friend WithEvents etq_UnitDim10 As Label
    Friend WithEvents img_Td2 As PictureBox
    Friend WithEvents pan_Epaisseur As Panel
    Friend WithEvents txt_Hd As TextBox
    Friend WithEvents lbl_Epaisseur As Label
    Friend WithEvents etq_UnitDim2 As Label
    Friend WithEvents Img_Hd As PictureBox
    Friend WithEvents pan_Predalle As Panel
    Friend WithEvents txt_EpJoint As TextBox
    Friend WithEvents lbl_EpJoint As Label
    Friend WithEvents img_EpJoint As PictureBox
    Friend WithEvents etq_UnitDim9 As Label
    Friend WithEvents txt_EpPredalle As TextBox
    Friend WithEvents lbl_EpPreDalle As Label
    Friend WithEvents img_EpPredalle As PictureBox
    Friend WithEvents etq_UnitDim8 As Label
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents pan_Cofradal As Panel
    Friend WithEvents cmb_Cofradal As ComboBox
    Friend WithEvents txt_mupf As TextBox
    Friend WithEvents txt_NameCustomCofra As TextBox
    Friend WithEvents txt_dp As TextBox
    Friend WithEvents lbl_mupf As Label
    Friend WithEvents lbl_Name As Label
    Friend WithEvents img_mupf As PictureBox
    Friend WithEvents lbl_dp As Label
    Friend WithEvents img_dp As PictureBox
    Friend WithEvents etq_UnitDim13 As Label
    Friend WithEvents lbl_Cofradal As Label
    Friend WithEvents etq_UnitDim12 As Label
    Friend WithEvents pan_OptionRive As Panel
    Friend WithEvents chk_ChambreRivePleine As CheckBox
    Friend WithEvents lbl_Masses As Label
    Friend WithEvents pan_Masses As Panel
    Friend WithEvents txt_MassSurf As TextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents etq_UnitMassSurf As Label
End Class
