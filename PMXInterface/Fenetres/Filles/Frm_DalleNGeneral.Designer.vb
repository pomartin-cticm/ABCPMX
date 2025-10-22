<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_DalleNGeneral
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
        Me.Pan_Contenu = New System.Windows.Forms.Panel()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Beton = New System.Windows.Forms.Panel()
        Me.txt_Ecm = New System.Windows.Forms.TextBox()
        Me.img_Ecm = New System.Windows.Forms.PictureBox()
        Me.etq_UnitSigma2 = New System.Windows.Forms.Label()
        Me.txt_Fck = New System.Windows.Forms.TextBox()
        Me.img_Fck = New System.Windows.Forms.PictureBox()
        Me.etq_UnitSigma1 = New System.Windows.Forms.Label()
        Me.lbl_RhoB = New System.Windows.Forms.Label()
        Me.lst_ClasseBetonDalle = New System.Windows.Forms.ListBox()
        Me.txt_RhoC = New System.Windows.Forms.TextBox()
        Me.img_RhoC = New System.Windows.Forms.PictureBox()
        Me.chk_BetonLeger = New System.Windows.Forms.CheckBox()
        Me.lbl_ClasseE = New System.Windows.Forms.Label()
        Me.etq_UnitRhoC = New System.Windows.Forms.Label()
        Me.lbl_Beton = New System.Windows.Forms.Label()
        Me.pan_Type = New System.Windows.Forms.Panel()
        Me.lbl_TypeDalle = New System.Windows.Forms.Label()
        Me.cmb_TypeDalle = New System.Windows.Forms.ComboBox()
        Me.lbl_General = New System.Windows.Forms.Label()
        Me.cmb_ClasseBetonDalle = New System.Windows.Forms.ComboBox()
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
        Me.pan_Renformis = New System.Windows.Forms.Panel()
        Me.txt_Hh = New System.Windows.Forms.TextBox()
        Me.lbl_Renformis = New System.Windows.Forms.Label()
        Me.Img_Hh = New System.Windows.Forms.PictureBox()
        Me.etq_UnitDim3 = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTipDalle = New System.Windows.Forms.ToolTip(Me.components)
        Me.Pan_Contenu.SuspendLayout()
        Me.TLpan_Gauche.SuspendLayout()
        Me.pan_Beton.SuspendLayout()
        CType(Me.img_Ecm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Fck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_RhoC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Type.SuspendLayout()
        Me.pan_EpaisseurMixte.SuspendLayout()
        CType(Me.img_Tc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Td2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Epaisseur.SuspendLayout()
        CType(Me.Img_Hd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Predalle.SuspendLayout()
        CType(Me.img_EpJoint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_EpPredalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Renformis.SuspendLayout()
        CType(Me.Img_Hh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Pan_Contenu
        '
        Me.Pan_Contenu.Controls.Add(Me.TLpan_Gauche)
        Me.Pan_Contenu.Location = New System.Drawing.Point(22, 16)
        Me.Pan_Contenu.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Pan_Contenu.Name = "Pan_Contenu"
        Me.Pan_Contenu.Size = New System.Drawing.Size(296, 470)
        Me.Pan_Contenu.TabIndex = 0
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.pan_Beton, 0, 3)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_Beton, 0, 2)
        Me.TLpan_Gauche.Controls.Add(Me.pan_Type, 0, 1)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_General, 0, 0)
        Me.TLpan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Gauche.Name = "TLpan_Gauche"
        Me.TLpan_Gauche.RowCount = 4
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TLpan_Gauche.Size = New System.Drawing.Size(296, 470)
        Me.TLpan_Gauche.TabIndex = 1
        '
        'pan_Beton
        '
        Me.pan_Beton.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Beton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Beton.Controls.Add(Me.txt_Ecm)
        Me.pan_Beton.Controls.Add(Me.img_Ecm)
        Me.pan_Beton.Controls.Add(Me.etq_UnitSigma2)
        Me.pan_Beton.Controls.Add(Me.txt_Fck)
        Me.pan_Beton.Controls.Add(Me.img_Fck)
        Me.pan_Beton.Controls.Add(Me.etq_UnitSigma1)
        Me.pan_Beton.Controls.Add(Me.lbl_RhoB)
        Me.pan_Beton.Controls.Add(Me.lst_ClasseBetonDalle)
        Me.pan_Beton.Controls.Add(Me.txt_RhoC)
        Me.pan_Beton.Controls.Add(Me.img_RhoC)
        Me.pan_Beton.Controls.Add(Me.chk_BetonLeger)
        Me.pan_Beton.Controls.Add(Me.lbl_ClasseE)
        Me.pan_Beton.Controls.Add(Me.etq_UnitRhoC)
        Me.pan_Beton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Beton.Location = New System.Drawing.Point(0, 172)
        Me.pan_Beton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Beton.Name = "pan_Beton"
        Me.pan_Beton.Size = New System.Drawing.Size(296, 297)
        Me.pan_Beton.TabIndex = 9
        '
        'txt_Ecm
        '
        Me.txt_Ecm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Ecm.Location = New System.Drawing.Point(190, 168)
        Me.txt_Ecm.Name = "txt_Ecm"
        Me.txt_Ecm.Size = New System.Drawing.Size(58, 20)
        Me.txt_Ecm.TabIndex = 71
        Me.txt_Ecm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'img_Ecm
        '
        Me.img_Ecm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Ecm.Location = New System.Drawing.Point(146, 168)
        Me.img_Ecm.Name = "img_Ecm"
        Me.img_Ecm.Size = New System.Drawing.Size(46, 20)
        Me.img_Ecm.TabIndex = 72
        Me.img_Ecm.TabStop = False
        '
        'etq_UnitSigma2
        '
        Me.etq_UnitSigma2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitSigma2.AutoSize = True
        Me.etq_UnitSigma2.Location = New System.Drawing.Point(254, 171)
        Me.etq_UnitSigma2.Name = "etq_UnitSigma2"
        Me.etq_UnitSigma2.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitSigma2.TabIndex = 70
        Me.etq_UnitSigma2.Text = "mm"
        '
        'txt_Fck
        '
        Me.txt_Fck.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Fck.Location = New System.Drawing.Point(190, 147)
        Me.txt_Fck.Name = "txt_Fck"
        Me.txt_Fck.Size = New System.Drawing.Size(58, 20)
        Me.txt_Fck.TabIndex = 68
        Me.txt_Fck.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'img_Fck
        '
        Me.img_Fck.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Fck.Location = New System.Drawing.Point(146, 147)
        Me.img_Fck.Name = "img_Fck"
        Me.img_Fck.Size = New System.Drawing.Size(46, 20)
        Me.img_Fck.TabIndex = 69
        Me.img_Fck.TabStop = False
        '
        'etq_UnitSigma1
        '
        Me.etq_UnitSigma1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitSigma1.AutoSize = True
        Me.etq_UnitSigma1.Location = New System.Drawing.Point(254, 150)
        Me.etq_UnitSigma1.Name = "etq_UnitSigma1"
        Me.etq_UnitSigma1.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitSigma1.TabIndex = 67
        Me.etq_UnitSigma1.Text = "mm"
        '
        'lbl_RhoB
        '
        Me.lbl_RhoB.AutoSize = True
        Me.lbl_RhoB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_RhoB.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_RhoB.Location = New System.Drawing.Point(13, 197)
        Me.lbl_RhoB.Name = "lbl_RhoB"
        Me.lbl_RhoB.Size = New System.Drawing.Size(50, 13)
        Me.lbl_RhoB.TabIndex = 66
        Me.lbl_RhoB.Text = "lbl_RhoB"
        Me.lbl_RhoB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lst_ClasseBetonDalle
        '
        Me.lst_ClasseBetonDalle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lst_ClasseBetonDalle.FormattingEnabled = True
        Me.lst_ClasseBetonDalle.Location = New System.Drawing.Point(146, 33)
        Me.lst_ClasseBetonDalle.Name = "lst_ClasseBetonDalle"
        Me.lst_ClasseBetonDalle.Size = New System.Drawing.Size(103, 108)
        Me.lst_ClasseBetonDalle.TabIndex = 65
        '
        'txt_RhoC
        '
        Me.txt_RhoC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_RhoC.Location = New System.Drawing.Point(190, 214)
        Me.txt_RhoC.Name = "txt_RhoC"
        Me.txt_RhoC.Size = New System.Drawing.Size(58, 20)
        Me.txt_RhoC.TabIndex = 62
        '
        'img_RhoC
        '
        Me.img_RhoC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_RhoC.Location = New System.Drawing.Point(146, 214)
        Me.img_RhoC.Name = "img_RhoC"
        Me.img_RhoC.Size = New System.Drawing.Size(46, 20)
        Me.img_RhoC.TabIndex = 63
        Me.img_RhoC.TabStop = False
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
        'etq_UnitRhoC
        '
        Me.etq_UnitRhoC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitRhoC.AutoSize = True
        Me.etq_UnitRhoC.Location = New System.Drawing.Point(254, 217)
        Me.etq_UnitRhoC.Name = "etq_UnitRhoC"
        Me.etq_UnitRhoC.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitRhoC.TabIndex = 61
        Me.etq_UnitRhoC.Text = "mm"
        '
        'lbl_Beton
        '
        Me.lbl_Beton.AutoSize = True
        Me.lbl_Beton.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Beton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Beton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Beton.Location = New System.Drawing.Point(0, 142)
        Me.lbl_Beton.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Beton.Name = "lbl_Beton"
        Me.lbl_Beton.Size = New System.Drawing.Size(296, 30)
        Me.lbl_Beton.TabIndex = 8
        Me.lbl_Beton.Text = "lbl_Beton"
        Me.lbl_Beton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Type
        '
        Me.pan_Type.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Type.Controls.Add(Me.lbl_TypeDalle)
        Me.pan_Type.Controls.Add(Me.cmb_TypeDalle)
        Me.pan_Type.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Type.Location = New System.Drawing.Point(0, 30)
        Me.pan_Type.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Type.Name = "pan_Type"
        Me.pan_Type.Size = New System.Drawing.Size(296, 111)
        Me.pan_Type.TabIndex = 7
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
        Me.cmb_TypeDalle.Size = New System.Drawing.Size(188, 21)
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
        Me.lbl_General.Size = New System.Drawing.Size(296, 30)
        Me.lbl_General.TabIndex = 2
        Me.lbl_General.Text = "lbl_General"
        Me.lbl_General.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmb_ClasseBetonDalle
        '
        Me.cmb_ClasseBetonDalle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_ClasseBetonDalle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_ClasseBetonDalle.FormattingEnabled = True
        Me.cmb_ClasseBetonDalle.Location = New System.Drawing.Point(379, 258)
        Me.cmb_ClasseBetonDalle.Name = "cmb_ClasseBetonDalle"
        Me.cmb_ClasseBetonDalle.Size = New System.Drawing.Size(156, 21)
        Me.cmb_ClasseBetonDalle.TabIndex = 57
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
        Me.pan_EpaisseurMixte.Location = New System.Drawing.Point(368, 130)
        Me.pan_EpaisseurMixte.Name = "pan_EpaisseurMixte"
        Me.pan_EpaisseurMixte.Size = New System.Drawing.Size(237, 52)
        Me.pan_EpaisseurMixte.TabIndex = 92
        '
        'rdb_EpPleine
        '
        Me.rdb_EpPleine.AutoSize = True
        Me.rdb_EpPleine.Location = New System.Drawing.Point(93, 28)
        Me.rdb_EpPleine.Name = "rdb_EpPleine"
        Me.rdb_EpPleine.Size = New System.Drawing.Size(14, 13)
        Me.rdb_EpPleine.TabIndex = 93
        Me.rdb_EpPleine.TabStop = True
        Me.rdb_EpPleine.UseVisualStyleBackColor = True
        '
        'rdb_EpTotale
        '
        Me.rdb_EpTotale.AutoSize = True
        Me.rdb_EpTotale.Location = New System.Drawing.Point(93, 7)
        Me.rdb_EpTotale.Name = "rdb_EpTotale"
        Me.rdb_EpTotale.Size = New System.Drawing.Size(14, 13)
        Me.rdb_EpTotale.TabIndex = 92
        Me.rdb_EpTotale.TabStop = True
        Me.rdb_EpTotale.UseVisualStyleBackColor = True
        '
        'txt_Tc
        '
        Me.txt_Tc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Tc.Location = New System.Drawing.Point(146, 24)
        Me.txt_Tc.Name = "txt_Tc"
        Me.txt_Tc.Size = New System.Drawing.Size(58, 20)
        Me.txt_Tc.TabIndex = 90
        '
        'etq_UnitDim11
        '
        Me.etq_UnitDim11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim11.AutoSize = True
        Me.etq_UnitDim11.Location = New System.Drawing.Point(211, 27)
        Me.etq_UnitDim11.Name = "etq_UnitDim11"
        Me.etq_UnitDim11.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim11.TabIndex = 89
        Me.etq_UnitDim11.Text = "mm"
        '
        'txt_Td2
        '
        Me.txt_Td2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Td2.Location = New System.Drawing.Point(146, 3)
        Me.txt_Td2.Name = "txt_Td2"
        Me.txt_Td2.Size = New System.Drawing.Size(58, 20)
        Me.txt_Td2.TabIndex = 75
        '
        'img_Tc
        '
        Me.img_Tc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Tc.Location = New System.Drawing.Point(111, 24)
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
        Me.etq_UnitDim10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim10.AutoSize = True
        Me.etq_UnitDim10.Location = New System.Drawing.Point(211, 6)
        Me.etq_UnitDim10.Name = "etq_UnitDim10"
        Me.etq_UnitDim10.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim10.TabIndex = 74
        Me.etq_UnitDim10.Text = "mm"
        '
        'img_Td2
        '
        Me.img_Td2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Td2.Location = New System.Drawing.Point(111, 3)
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
        Me.pan_Epaisseur.Location = New System.Drawing.Point(368, 72)
        Me.pan_Epaisseur.Name = "pan_Epaisseur"
        Me.pan_Epaisseur.Size = New System.Drawing.Size(237, 25)
        Me.pan_Epaisseur.TabIndex = 91
        '
        'txt_Hd
        '
        Me.txt_Hd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Hd.Location = New System.Drawing.Point(146, 3)
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
        Me.etq_UnitDim2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim2.AutoSize = True
        Me.etq_UnitDim2.Location = New System.Drawing.Point(211, 6)
        Me.etq_UnitDim2.Name = "etq_UnitDim2"
        Me.etq_UnitDim2.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim2.TabIndex = 74
        Me.etq_UnitDim2.Text = "mm"
        '
        'Img_Hd
        '
        Me.Img_Hd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Img_Hd.Location = New System.Drawing.Point(111, 3)
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
        Me.pan_Predalle.Location = New System.Drawing.Point(368, 17)
        Me.pan_Predalle.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Predalle.Name = "pan_Predalle"
        Me.pan_Predalle.Size = New System.Drawing.Size(237, 52)
        Me.pan_Predalle.TabIndex = 90
        '
        'txt_EpJoint
        '
        Me.txt_EpJoint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EpJoint.Location = New System.Drawing.Point(146, 26)
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
        Me.img_EpJoint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_EpJoint.Location = New System.Drawing.Point(110, 26)
        Me.img_EpJoint.Name = "img_EpJoint"
        Me.img_EpJoint.Size = New System.Drawing.Size(37, 20)
        Me.img_EpJoint.TabIndex = 84
        Me.img_EpJoint.TabStop = False
        '
        'etq_UnitDim9
        '
        Me.etq_UnitDim9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim9.AutoSize = True
        Me.etq_UnitDim9.Location = New System.Drawing.Point(210, 29)
        Me.etq_UnitDim9.Name = "etq_UnitDim9"
        Me.etq_UnitDim9.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim9.TabIndex = 82
        Me.etq_UnitDim9.Text = "mm"
        '
        'txt_EpPredalle
        '
        Me.txt_EpPredalle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EpPredalle.Location = New System.Drawing.Point(146, 4)
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
        Me.img_EpPredalle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_EpPredalle.Location = New System.Drawing.Point(110, 4)
        Me.img_EpPredalle.Name = "img_EpPredalle"
        Me.img_EpPredalle.Size = New System.Drawing.Size(37, 20)
        Me.img_EpPredalle.TabIndex = 80
        Me.img_EpPredalle.TabStop = False
        '
        'etq_UnitDim8
        '
        Me.etq_UnitDim8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim8.AutoSize = True
        Me.etq_UnitDim8.Location = New System.Drawing.Point(210, 7)
        Me.etq_UnitDim8.Name = "etq_UnitDim8"
        Me.etq_UnitDim8.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim8.TabIndex = 78
        Me.etq_UnitDim8.Text = "mm"
        '
        'pan_Renformis
        '
        Me.pan_Renformis.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Renformis.Controls.Add(Me.txt_Hh)
        Me.pan_Renformis.Controls.Add(Me.lbl_Renformis)
        Me.pan_Renformis.Controls.Add(Me.Img_Hh)
        Me.pan_Renformis.Controls.Add(Me.etq_UnitDim3)
        Me.pan_Renformis.Location = New System.Drawing.Point(368, 100)
        Me.pan_Renformis.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Renformis.Name = "pan_Renformis"
        Me.pan_Renformis.Size = New System.Drawing.Size(237, 27)
        Me.pan_Renformis.TabIndex = 89
        '
        'txt_Hh
        '
        Me.txt_Hh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Hh.Location = New System.Drawing.Point(146, 4)
        Me.txt_Hh.Name = "txt_Hh"
        Me.txt_Hh.Size = New System.Drawing.Size(58, 20)
        Me.txt_Hh.TabIndex = 79
        '
        'lbl_Renformis
        '
        Me.lbl_Renformis.AutoSize = True
        Me.lbl_Renformis.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Renformis.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Renformis.Location = New System.Drawing.Point(9, 7)
        Me.lbl_Renformis.Name = "lbl_Renformis"
        Me.lbl_Renformis.Size = New System.Drawing.Size(70, 13)
        Me.lbl_Renformis.TabIndex = 77
        Me.lbl_Renformis.Text = "lbl_Renformis"
        Me.lbl_Renformis.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Img_Hh
        '
        Me.Img_Hh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Img_Hh.Location = New System.Drawing.Point(110, 4)
        Me.Img_Hh.Name = "Img_Hh"
        Me.Img_Hh.Size = New System.Drawing.Size(37, 20)
        Me.Img_Hh.TabIndex = 80
        Me.Img_Hh.TabStop = False
        '
        'etq_UnitDim3
        '
        Me.etq_UnitDim3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim3.AutoSize = True
        Me.etq_UnitDim3.Location = New System.Drawing.Point(210, 7)
        Me.etq_UnitDim3.Name = "etq_UnitDim3"
        Me.etq_UnitDim3.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim3.TabIndex = 78
        Me.etq_UnitDim3.Text = "mm"
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Frm_DalleNGeneral
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(638, 496)
        Me.Controls.Add(Me.pan_EpaisseurMixte)
        Me.Controls.Add(Me.Pan_Contenu)
        Me.Controls.Add(Me.pan_Epaisseur)
        Me.Controls.Add(Me.pan_Predalle)
        Me.Controls.Add(Me.pan_Renformis)
        Me.Controls.Add(Me.cmb_ClasseBetonDalle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "Frm_DalleNGeneral"
        Me.Text = "Frm_DalleNGeneral"
        Me.Pan_Contenu.ResumeLayout(False)
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.PerformLayout()
        Me.pan_Beton.ResumeLayout(False)
        Me.pan_Beton.PerformLayout()
        CType(Me.img_Ecm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Fck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_RhoC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Type.ResumeLayout(False)
        Me.pan_Type.PerformLayout()
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
        Me.pan_Renformis.ResumeLayout(False)
        Me.pan_Renformis.PerformLayout()
        CType(Me.Img_Hh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Pan_Contenu As Panel
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
    Friend WithEvents pan_Renformis As Panel
    Friend WithEvents txt_Hh As TextBox
    Friend WithEvents lbl_Renformis As Label
    Friend WithEvents Img_Hh As PictureBox
    Friend WithEvents etq_UnitDim3 As Label
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents ToolTipDalle As ToolTip
    Friend WithEvents lst_ClasseBetonDalle As ListBox
    Friend WithEvents txt_Ecm As TextBox
    Friend WithEvents img_Ecm As PictureBox
    Friend WithEvents etq_UnitSigma2 As Label
    Friend WithEvents txt_Fck As TextBox
    Friend WithEvents img_Fck As PictureBox
    Friend WithEvents etq_UnitSigma1 As Label
    Friend WithEvents lbl_RhoB As Label
End Class
