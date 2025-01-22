<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MaintienBacN_Calculs
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
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLpan_Calculs = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.img_Deck = New System.Windows.Forms.PictureBox()
        Me.pan_RigiditeShear = New System.Windows.Forms.Panel()
        Me.txt_Sact = New System.Windows.Forms.TextBox()
        Me.etq_UnitSact = New System.Windows.Forms.Label()
        Me.img_Sact = New System.Windows.Forms.PictureBox()
        Me.txt_c = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.img_c = New System.Windows.Forms.PictureBox()
        Me.txt_c22 = New System.Windows.Forms.TextBox()
        Me.etq_Unitc22 = New System.Windows.Forms.Label()
        Me.img_c22 = New System.Windows.Forms.PictureBox()
        Me.txt_c21 = New System.Windows.Forms.TextBox()
        Me.etq_Unitc21 = New System.Windows.Forms.Label()
        Me.img_c21 = New System.Windows.Forms.PictureBox()
        Me.txt_c12 = New System.Windows.Forms.TextBox()
        Me.etq_Unitc12 = New System.Windows.Forms.Label()
        Me.img_c12 = New System.Windows.Forms.PictureBox()
        Me.txt_c11 = New System.Windows.Forms.TextBox()
        Me.etq_UnitC11 = New System.Windows.Forms.Label()
        Me.img_c11 = New System.Windows.Forms.PictureBox()
        Me.txt_Alpha5 = New System.Windows.Forms.TextBox()
        Me.etq_UnitAlpha5 = New System.Windows.Forms.Label()
        Me.img_Alpha5 = New System.Windows.Forms.PictureBox()
        Me.txt_K = New System.Windows.Forms.TextBox()
        Me.etq_UnitK = New System.Windows.Forms.Label()
        Me.img_K = New System.Windows.Forms.PictureBox()
        Me.lbl_ShearRigidity = New System.Windows.Forms.Label()
        Me.pan_Rigidite = New System.Windows.Forms.Panel()
        Me.chk_Theta = New System.Windows.Forms.CheckBox()
        Me.txt_kTheta = New System.Windows.Forms.TextBox()
        Me.etq_UnitkTheta = New System.Windows.Forms.Label()
        Me.img_kTheta = New System.Windows.Forms.PictureBox()
        Me.txt_kThetaC = New System.Windows.Forms.TextBox()
        Me.etq_UnitkThetaC = New System.Windows.Forms.Label()
        Me.img_kThetaC = New System.Windows.Forms.PictureBox()
        Me.txt_kThetaA = New System.Windows.Forms.TextBox()
        Me.etq_UnitkThetaA = New System.Windows.Forms.Label()
        Me.img_kThetaA = New System.Windows.Forms.PictureBox()
        Me.lbl_BendingRigidity = New System.Windows.Forms.Label()
        Me.lbl_Calculs = New System.Windows.Forms.Label()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Calculs.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.img_Deck, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_RigiditeShear.SuspendLayout()
        CType(Me.img_Sact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_c, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_c22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_c21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_c12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_c11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Alpha5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_K, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Rigidite.SuspendLayout()
        CType(Me.img_kTheta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_kThetaC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_kThetaA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.Controls.Add(Me.TLpan_Calculs)
        Me.pan_Main.Location = New System.Drawing.Point(118, 96)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(459, 535)
        Me.pan_Main.TabIndex = 0
        '
        'TLpan_Calculs
        '
        Me.TLpan_Calculs.ColumnCount = 1
        Me.TLpan_Calculs.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Calculs.Controls.Add(Me.Panel5, 0, 1)
        Me.TLpan_Calculs.Controls.Add(Me.lbl_Calculs, 0, 0)
        Me.TLpan_Calculs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Calculs.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Calculs.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.TLpan_Calculs.Name = "TLpan_Calculs"
        Me.TLpan_Calculs.RowCount = 2
        Me.TLpan_Calculs.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Calculs.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Calculs.Size = New System.Drawing.Size(459, 535)
        Me.TLpan_Calculs.TabIndex = 4
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.img_Deck)
        Me.Panel5.Controls.Add(Me.pan_RigiditeShear)
        Me.Panel5.Controls.Add(Me.pan_Rigidite)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 30)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(459, 504)
        Me.Panel5.TabIndex = 4
        '
        'img_Deck
        '
        Me.img_Deck.Location = New System.Drawing.Point(3, 320)
        Me.img_Deck.Name = "img_Deck"
        Me.img_Deck.Size = New System.Drawing.Size(100, 25)
        Me.img_Deck.TabIndex = 122
        Me.img_Deck.TabStop = False
        '
        'pan_RigiditeShear
        '
        Me.pan_RigiditeShear.Controls.Add(Me.txt_Sact)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_UnitSact)
        Me.pan_RigiditeShear.Controls.Add(Me.img_Sact)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_c)
        Me.pan_RigiditeShear.Controls.Add(Me.Label1)
        Me.pan_RigiditeShear.Controls.Add(Me.img_c)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_c22)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_Unitc22)
        Me.pan_RigiditeShear.Controls.Add(Me.img_c22)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_c21)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_Unitc21)
        Me.pan_RigiditeShear.Controls.Add(Me.img_c21)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_c12)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_Unitc12)
        Me.pan_RigiditeShear.Controls.Add(Me.img_c12)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_c11)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_UnitC11)
        Me.pan_RigiditeShear.Controls.Add(Me.img_c11)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_Alpha5)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_UnitAlpha5)
        Me.pan_RigiditeShear.Controls.Add(Me.img_Alpha5)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_K)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_UnitK)
        Me.pan_RigiditeShear.Controls.Add(Me.img_K)
        Me.pan_RigiditeShear.Controls.Add(Me.lbl_ShearRigidity)
        Me.pan_RigiditeShear.Location = New System.Drawing.Point(0, 1)
        Me.pan_RigiditeShear.Name = "pan_RigiditeShear"
        Me.pan_RigiditeShear.Size = New System.Drawing.Size(250, 197)
        Me.pan_RigiditeShear.TabIndex = 121
        '
        'txt_Sact
        '
        Me.txt_Sact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sact.Location = New System.Drawing.Point(136, 170)
        Me.txt_Sact.Name = "txt_Sact"
        Me.txt_Sact.Size = New System.Drawing.Size(58, 20)
        Me.txt_Sact.TabIndex = 108
        Me.txt_Sact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitSact
        '
        Me.etq_UnitSact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitSact.AutoSize = True
        Me.etq_UnitSact.Location = New System.Drawing.Point(199, 173)
        Me.etq_UnitSact.Name = "etq_UnitSact"
        Me.etq_UnitSact.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitSact.TabIndex = 107
        Me.etq_UnitSact.Text = "mm"
        '
        'img_Sact
        '
        Me.img_Sact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Sact.Location = New System.Drawing.Point(99, 170)
        Me.img_Sact.Name = "img_Sact"
        Me.img_Sact.Size = New System.Drawing.Size(37, 20)
        Me.img_Sact.TabIndex = 109
        Me.img_Sact.TabStop = False
        '
        'txt_c
        '
        Me.txt_c.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_c.Location = New System.Drawing.Point(136, 149)
        Me.txt_c.Name = "txt_c"
        Me.txt_c.Size = New System.Drawing.Size(58, 20)
        Me.txt_c.TabIndex = 105
        Me.txt_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(199, 152)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 13)
        Me.Label1.TabIndex = 104
        Me.Label1.Text = "mm"
        '
        'img_c
        '
        Me.img_c.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_c.Location = New System.Drawing.Point(99, 149)
        Me.img_c.Name = "img_c"
        Me.img_c.Size = New System.Drawing.Size(37, 20)
        Me.img_c.TabIndex = 106
        Me.img_c.TabStop = False
        '
        'txt_c22
        '
        Me.txt_c22.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_c22.Location = New System.Drawing.Point(136, 128)
        Me.txt_c22.Name = "txt_c22"
        Me.txt_c22.Size = New System.Drawing.Size(58, 20)
        Me.txt_c22.TabIndex = 102
        Me.txt_c22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_Unitc22
        '
        Me.etq_Unitc22.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_Unitc22.AutoSize = True
        Me.etq_Unitc22.Location = New System.Drawing.Point(199, 131)
        Me.etq_Unitc22.Name = "etq_Unitc22"
        Me.etq_Unitc22.Size = New System.Drawing.Size(23, 13)
        Me.etq_Unitc22.TabIndex = 101
        Me.etq_Unitc22.Text = "mm"
        '
        'img_c22
        '
        Me.img_c22.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_c22.Location = New System.Drawing.Point(99, 128)
        Me.img_c22.Name = "img_c22"
        Me.img_c22.Size = New System.Drawing.Size(37, 20)
        Me.img_c22.TabIndex = 103
        Me.img_c22.TabStop = False
        '
        'txt_c21
        '
        Me.txt_c21.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_c21.Location = New System.Drawing.Point(136, 107)
        Me.txt_c21.Name = "txt_c21"
        Me.txt_c21.Size = New System.Drawing.Size(58, 20)
        Me.txt_c21.TabIndex = 99
        Me.txt_c21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_Unitc21
        '
        Me.etq_Unitc21.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_Unitc21.AutoSize = True
        Me.etq_Unitc21.Location = New System.Drawing.Point(199, 110)
        Me.etq_Unitc21.Name = "etq_Unitc21"
        Me.etq_Unitc21.Size = New System.Drawing.Size(23, 13)
        Me.etq_Unitc21.TabIndex = 98
        Me.etq_Unitc21.Text = "mm"
        '
        'img_c21
        '
        Me.img_c21.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_c21.Location = New System.Drawing.Point(99, 107)
        Me.img_c21.Name = "img_c21"
        Me.img_c21.Size = New System.Drawing.Size(37, 20)
        Me.img_c21.TabIndex = 100
        Me.img_c21.TabStop = False
        '
        'txt_c12
        '
        Me.txt_c12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_c12.Location = New System.Drawing.Point(136, 86)
        Me.txt_c12.Name = "txt_c12"
        Me.txt_c12.Size = New System.Drawing.Size(58, 20)
        Me.txt_c12.TabIndex = 96
        Me.txt_c12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_Unitc12
        '
        Me.etq_Unitc12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_Unitc12.AutoSize = True
        Me.etq_Unitc12.Location = New System.Drawing.Point(199, 89)
        Me.etq_Unitc12.Name = "etq_Unitc12"
        Me.etq_Unitc12.Size = New System.Drawing.Size(23, 13)
        Me.etq_Unitc12.TabIndex = 95
        Me.etq_Unitc12.Text = "mm"
        '
        'img_c12
        '
        Me.img_c12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_c12.Location = New System.Drawing.Point(99, 86)
        Me.img_c12.Name = "img_c12"
        Me.img_c12.Size = New System.Drawing.Size(37, 20)
        Me.img_c12.TabIndex = 97
        Me.img_c12.TabStop = False
        '
        'txt_c11
        '
        Me.txt_c11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_c11.Location = New System.Drawing.Point(136, 65)
        Me.txt_c11.Name = "txt_c11"
        Me.txt_c11.Size = New System.Drawing.Size(58, 20)
        Me.txt_c11.TabIndex = 93
        Me.txt_c11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitC11
        '
        Me.etq_UnitC11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitC11.AutoSize = True
        Me.etq_UnitC11.Location = New System.Drawing.Point(199, 68)
        Me.etq_UnitC11.Name = "etq_UnitC11"
        Me.etq_UnitC11.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitC11.TabIndex = 92
        Me.etq_UnitC11.Text = "mm"
        '
        'img_c11
        '
        Me.img_c11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_c11.Location = New System.Drawing.Point(99, 65)
        Me.img_c11.Name = "img_c11"
        Me.img_c11.Size = New System.Drawing.Size(37, 20)
        Me.img_c11.TabIndex = 94
        Me.img_c11.TabStop = False
        '
        'txt_Alpha5
        '
        Me.txt_Alpha5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Alpha5.Location = New System.Drawing.Point(136, 44)
        Me.txt_Alpha5.Name = "txt_Alpha5"
        Me.txt_Alpha5.Size = New System.Drawing.Size(58, 20)
        Me.txt_Alpha5.TabIndex = 90
        Me.txt_Alpha5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitAlpha5
        '
        Me.etq_UnitAlpha5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitAlpha5.AutoSize = True
        Me.etq_UnitAlpha5.Location = New System.Drawing.Point(199, 47)
        Me.etq_UnitAlpha5.Name = "etq_UnitAlpha5"
        Me.etq_UnitAlpha5.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitAlpha5.TabIndex = 89
        Me.etq_UnitAlpha5.Text = "mm"
        '
        'img_Alpha5
        '
        Me.img_Alpha5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Alpha5.Location = New System.Drawing.Point(99, 44)
        Me.img_Alpha5.Name = "img_Alpha5"
        Me.img_Alpha5.Size = New System.Drawing.Size(37, 20)
        Me.img_Alpha5.TabIndex = 91
        Me.img_Alpha5.TabStop = False
        '
        'txt_K
        '
        Me.txt_K.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_K.Location = New System.Drawing.Point(136, 23)
        Me.txt_K.Name = "txt_K"
        Me.txt_K.Size = New System.Drawing.Size(58, 20)
        Me.txt_K.TabIndex = 87
        Me.txt_K.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitK
        '
        Me.etq_UnitK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitK.AutoSize = True
        Me.etq_UnitK.Location = New System.Drawing.Point(199, 26)
        Me.etq_UnitK.Name = "etq_UnitK"
        Me.etq_UnitK.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitK.TabIndex = 86
        Me.etq_UnitK.Text = "mm"
        '
        'img_K
        '
        Me.img_K.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_K.Location = New System.Drawing.Point(99, 23)
        Me.img_K.Name = "img_K"
        Me.img_K.Size = New System.Drawing.Size(37, 20)
        Me.img_K.TabIndex = 88
        Me.img_K.TabStop = False
        '
        'lbl_ShearRigidity
        '
        Me.lbl_ShearRigidity.AutoSize = True
        Me.lbl_ShearRigidity.Location = New System.Drawing.Point(5, 4)
        Me.lbl_ShearRigidity.Name = "lbl_ShearRigidity"
        Me.lbl_ShearRigidity.Size = New System.Drawing.Size(85, 13)
        Me.lbl_ShearRigidity.TabIndex = 7
        Me.lbl_ShearRigidity.Text = "lbl_ShearRigidity"
        '
        'pan_Rigidite
        '
        Me.pan_Rigidite.Controls.Add(Me.chk_Theta)
        Me.pan_Rigidite.Controls.Add(Me.txt_kTheta)
        Me.pan_Rigidite.Controls.Add(Me.etq_UnitkTheta)
        Me.pan_Rigidite.Controls.Add(Me.img_kTheta)
        Me.pan_Rigidite.Controls.Add(Me.txt_kThetaC)
        Me.pan_Rigidite.Controls.Add(Me.etq_UnitkThetaC)
        Me.pan_Rigidite.Controls.Add(Me.img_kThetaC)
        Me.pan_Rigidite.Controls.Add(Me.txt_kThetaA)
        Me.pan_Rigidite.Controls.Add(Me.etq_UnitkThetaA)
        Me.pan_Rigidite.Controls.Add(Me.img_kThetaA)
        Me.pan_Rigidite.Controls.Add(Me.lbl_BendingRigidity)
        Me.pan_Rigidite.Location = New System.Drawing.Point(0, 198)
        Me.pan_Rigidite.Name = "pan_Rigidite"
        Me.pan_Rigidite.Size = New System.Drawing.Size(249, 137)
        Me.pan_Rigidite.TabIndex = 120
        '
        'chk_Theta
        '
        Me.chk_Theta.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chk_Theta.Location = New System.Drawing.Point(3, 100)
        Me.chk_Theta.Name = "chk_Theta"
        Me.chk_Theta.Size = New System.Drawing.Size(242, 34)
        Me.chk_Theta.TabIndex = 120
        Me.chk_Theta.Text = "chk_Theta"
        Me.chk_Theta.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chk_Theta.UseVisualStyleBackColor = True
        '
        'txt_kTheta
        '
        Me.txt_kTheta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_kTheta.Location = New System.Drawing.Point(136, 74)
        Me.txt_kTheta.Name = "txt_kTheta"
        Me.txt_kTheta.Size = New System.Drawing.Size(58, 20)
        Me.txt_kTheta.TabIndex = 118
        Me.txt_kTheta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitkTheta
        '
        Me.etq_UnitkTheta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitkTheta.AutoSize = True
        Me.etq_UnitkTheta.Location = New System.Drawing.Point(199, 77)
        Me.etq_UnitkTheta.Name = "etq_UnitkTheta"
        Me.etq_UnitkTheta.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitkTheta.TabIndex = 117
        Me.etq_UnitkTheta.Text = "mm"
        '
        'img_kTheta
        '
        Me.img_kTheta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_kTheta.Location = New System.Drawing.Point(75, 74)
        Me.img_kTheta.Name = "img_kTheta"
        Me.img_kTheta.Size = New System.Drawing.Size(61, 20)
        Me.img_kTheta.TabIndex = 119
        Me.img_kTheta.TabStop = False
        '
        'txt_kThetaC
        '
        Me.txt_kThetaC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_kThetaC.Location = New System.Drawing.Point(136, 52)
        Me.txt_kThetaC.Name = "txt_kThetaC"
        Me.txt_kThetaC.Size = New System.Drawing.Size(58, 20)
        Me.txt_kThetaC.TabIndex = 115
        Me.txt_kThetaC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitkThetaC
        '
        Me.etq_UnitkThetaC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitkThetaC.AutoSize = True
        Me.etq_UnitkThetaC.Location = New System.Drawing.Point(199, 55)
        Me.etq_UnitkThetaC.Name = "etq_UnitkThetaC"
        Me.etq_UnitkThetaC.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitkThetaC.TabIndex = 114
        Me.etq_UnitkThetaC.Text = "mm"
        '
        'img_kThetaC
        '
        Me.img_kThetaC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_kThetaC.Location = New System.Drawing.Point(75, 52)
        Me.img_kThetaC.Name = "img_kThetaC"
        Me.img_kThetaC.Size = New System.Drawing.Size(61, 20)
        Me.img_kThetaC.TabIndex = 116
        Me.img_kThetaC.TabStop = False
        '
        'txt_kThetaA
        '
        Me.txt_kThetaA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_kThetaA.Location = New System.Drawing.Point(136, 30)
        Me.txt_kThetaA.Name = "txt_kThetaA"
        Me.txt_kThetaA.Size = New System.Drawing.Size(58, 20)
        Me.txt_kThetaA.TabIndex = 112
        Me.txt_kThetaA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitkThetaA
        '
        Me.etq_UnitkThetaA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitkThetaA.AutoSize = True
        Me.etq_UnitkThetaA.Location = New System.Drawing.Point(199, 33)
        Me.etq_UnitkThetaA.Name = "etq_UnitkThetaA"
        Me.etq_UnitkThetaA.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitkThetaA.TabIndex = 111
        Me.etq_UnitkThetaA.Text = "mm"
        '
        'img_kThetaA
        '
        Me.img_kThetaA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_kThetaA.Location = New System.Drawing.Point(75, 30)
        Me.img_kThetaA.Name = "img_kThetaA"
        Me.img_kThetaA.Size = New System.Drawing.Size(61, 20)
        Me.img_kThetaA.TabIndex = 113
        Me.img_kThetaA.TabStop = False
        '
        'lbl_BendingRigidity
        '
        Me.lbl_BendingRigidity.AutoSize = True
        Me.lbl_BendingRigidity.Location = New System.Drawing.Point(5, 5)
        Me.lbl_BendingRigidity.Name = "lbl_BendingRigidity"
        Me.lbl_BendingRigidity.Size = New System.Drawing.Size(96, 13)
        Me.lbl_BendingRigidity.TabIndex = 110
        Me.lbl_BendingRigidity.Text = "lbl_BendingRigidity"
        '
        'lbl_Calculs
        '
        Me.lbl_Calculs.AutoSize = True
        Me.lbl_Calculs.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Calculs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Calculs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Calculs.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Calculs.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Calculs.Name = "lbl_Calculs"
        Me.lbl_Calculs.Size = New System.Drawing.Size(459, 30)
        Me.lbl_Calculs.TabIndex = 2
        Me.lbl_Calculs.Text = "lbl_Calculs"
        Me.lbl_Calculs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_MaintienBacN_Calculs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 643)
        Me.Controls.Add(Me.pan_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_MaintienBacN_Calculs"
        Me.Text = "Frm_MaintienBacN_Calculs"
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Calculs.ResumeLayout(False)
        Me.TLpan_Calculs.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        CType(Me.img_Deck, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_RigiditeShear.ResumeLayout(False)
        Me.pan_RigiditeShear.PerformLayout()
        CType(Me.img_Sact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_c, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_c22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_c21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_c12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_c11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Alpha5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_K, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Rigidite.ResumeLayout(False)
        Me.pan_Rigidite.PerformLayout()
        CType(Me.img_kTheta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_kThetaC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_kThetaA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Calculs As TableLayoutPanel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents img_Deck As PictureBox
    Friend WithEvents pan_RigiditeShear As Panel
    Friend WithEvents txt_Sact As TextBox
    Friend WithEvents etq_UnitSact As Label
    Friend WithEvents img_Sact As PictureBox
    Friend WithEvents txt_c As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents img_c As PictureBox
    Friend WithEvents txt_c22 As TextBox
    Friend WithEvents etq_Unitc22 As Label
    Friend WithEvents img_c22 As PictureBox
    Friend WithEvents txt_c21 As TextBox
    Friend WithEvents etq_Unitc21 As Label
    Friend WithEvents img_c21 As PictureBox
    Friend WithEvents txt_c12 As TextBox
    Friend WithEvents etq_Unitc12 As Label
    Friend WithEvents img_c12 As PictureBox
    Friend WithEvents txt_c11 As TextBox
    Friend WithEvents etq_UnitC11 As Label
    Friend WithEvents img_c11 As PictureBox
    Friend WithEvents txt_Alpha5 As TextBox
    Friend WithEvents etq_UnitAlpha5 As Label
    Friend WithEvents img_Alpha5 As PictureBox
    Friend WithEvents txt_K As TextBox
    Friend WithEvents etq_UnitK As Label
    Friend WithEvents img_K As PictureBox
    Friend WithEvents lbl_ShearRigidity As Label
    Friend WithEvents pan_Rigidite As Panel
    Friend WithEvents chk_Theta As CheckBox
    Friend WithEvents txt_kTheta As TextBox
    Friend WithEvents etq_UnitkTheta As Label
    Friend WithEvents img_kTheta As PictureBox
    Friend WithEvents txt_kThetaC As TextBox
    Friend WithEvents etq_UnitkThetaC As Label
    Friend WithEvents img_kThetaC As PictureBox
    Friend WithEvents txt_kThetaA As TextBox
    Friend WithEvents etq_UnitkThetaA As Label
    Friend WithEvents img_kThetaA As PictureBox
    Friend WithEvents lbl_BendingRigidity As Label
    Friend WithEvents lbl_Calculs As Label
End Class
