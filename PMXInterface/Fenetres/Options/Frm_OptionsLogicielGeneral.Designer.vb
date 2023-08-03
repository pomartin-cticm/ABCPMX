<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsLogicielGeneral
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
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.lbl_Loads = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txt_GammaV_fi = New System.Windows.Forms.TextBox()
        Me.img_GammaV_fi = New System.Windows.Forms.PictureBox()
        Me.txt_GammaC_fi = New System.Windows.Forms.TextBox()
        Me.img_GammaC_fi = New System.Windows.Forms.PictureBox()
        Me.txt_GammaM_fi = New System.Windows.Forms.TextBox()
        Me.img_GammaM_fi = New System.Windows.Forms.PictureBox()
        Me.lbl_Fire = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txt_GammaVs = New System.Windows.Forms.TextBox()
        Me.img_GammaVs = New System.Windows.Forms.PictureBox()
        Me.lbl_Beton = New System.Windows.Forms.Label()
        Me.txt_GammaC = New System.Windows.Forms.TextBox()
        Me.txt_GammaP = New System.Windows.Forms.TextBox()
        Me.img_GammaC = New System.Windows.Forms.PictureBox()
        Me.img_GammaP = New System.Windows.Forms.PictureBox()
        Me.img_GammaV = New System.Windows.Forms.PictureBox()
        Me.txt_GammaS = New System.Windows.Forms.TextBox()
        Me.txt_GammaV = New System.Windows.Forms.TextBox()
        Me.img_GammaS = New System.Windows.Forms.PictureBox()
        Me.pan_Steel = New System.Windows.Forms.Panel()
        Me.lbl_Acier = New System.Windows.Forms.Label()
        Me.txt_GammaM2 = New System.Windows.Forms.TextBox()
        Me.img_GammaM2 = New System.Windows.Forms.PictureBox()
        Me.txt_GammaM1 = New System.Windows.Forms.TextBox()
        Me.img_GammaM1 = New System.Windows.Forms.PictureBox()
        Me.txt_GammaM0 = New System.Windows.Forms.TextBox()
        Me.img_GammaM0 = New System.Windows.Forms.PictureBox()
        Me.pan_Combination = New System.Windows.Forms.Panel()
        Me.lbl_Combination = New System.Windows.Forms.Label()
        Me.img_Psi0 = New System.Windows.Forms.PictureBox()
        Me.img_Psi1 = New System.Windows.Forms.PictureBox()
        Me.txt_Psi0 = New System.Windows.Forms.TextBox()
        Me.txt_Psi2 = New System.Windows.Forms.TextBox()
        Me.txt_Psi1 = New System.Windows.Forms.TextBox()
        Me.img_Psi2 = New System.Windows.Forms.PictureBox()
        Me.pan_Loads = New System.Windows.Forms.Panel()
        Me.img_GammaGinf = New System.Windows.Forms.PictureBox()
        Me.txt_GammaGinf = New System.Windows.Forms.TextBox()
        Me.img_GammaGsup = New System.Windows.Forms.PictureBox()
        Me.img_GammaQ = New System.Windows.Forms.PictureBox()
        Me.txt_GammaGsup = New System.Windows.Forms.TextBox()
        Me.txt_GammaQ = New System.Windows.Forms.TextBox()
        Me.lbl_Materials = New System.Windows.Forms.Label()
        Me.pan_General.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.img_GammaV_fi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaC_fi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaM_fi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.img_GammaVs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaV, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Steel.SuspendLayout()
        CType(Me.img_GammaM2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaM1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaM0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Combination.SuspendLayout()
        CType(Me.img_Psi0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Psi1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Psi2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Loads.SuspendLayout()
        CType(Me.img_GammaGinf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaGsup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaQ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_General.Controls.Add(Me.lbl_Loads)
        Me.pan_General.Controls.Add(Me.Panel2)
        Me.pan_General.Controls.Add(Me.Panel1)
        Me.pan_General.Controls.Add(Me.pan_Steel)
        Me.pan_General.Controls.Add(Me.pan_Combination)
        Me.pan_General.Controls.Add(Me.pan_Loads)
        Me.pan_General.Controls.Add(Me.lbl_Materials)
        Me.pan_General.Location = New System.Drawing.Point(83, 30)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(739, 472)
        Me.pan_General.TabIndex = 1
        '
        'lbl_Loads
        '
        Me.lbl_Loads.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Loads.Location = New System.Drawing.Point(11, 7)
        Me.lbl_Loads.Name = "lbl_Loads"
        Me.lbl_Loads.Size = New System.Drawing.Size(150, 23)
        Me.lbl_Loads.TabIndex = 85
        Me.lbl_Loads.Text = "lbl_Loads"
        Me.lbl_Loads.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txt_GammaV_fi)
        Me.Panel2.Controls.Add(Me.img_GammaV_fi)
        Me.Panel2.Controls.Add(Me.txt_GammaC_fi)
        Me.Panel2.Controls.Add(Me.img_GammaC_fi)
        Me.Panel2.Controls.Add(Me.txt_GammaM_fi)
        Me.Panel2.Controls.Add(Me.img_GammaM_fi)
        Me.Panel2.Controls.Add(Me.lbl_Fire)
        Me.Panel2.Location = New System.Drawing.Point(285, 161)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(139, 147)
        Me.Panel2.TabIndex = 102
        '
        'txt_GammaV_fi
        '
        Me.txt_GammaV_fi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_GammaV_fi.Location = New System.Drawing.Point(48, 86)
        Me.txt_GammaV_fi.Name = "txt_GammaV_fi"
        Me.txt_GammaV_fi.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaV_fi.TabIndex = 98
        '
        'img_GammaV_fi
        '
        Me.img_GammaV_fi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_GammaV_fi.Location = New System.Drawing.Point(3, 86)
        Me.img_GammaV_fi.Name = "img_GammaV_fi"
        Me.img_GammaV_fi.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaV_fi.TabIndex = 99
        Me.img_GammaV_fi.TabStop = False
        '
        'txt_GammaC_fi
        '
        Me.txt_GammaC_fi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_GammaC_fi.Location = New System.Drawing.Point(48, 60)
        Me.txt_GammaC_fi.Name = "txt_GammaC_fi"
        Me.txt_GammaC_fi.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaC_fi.TabIndex = 96
        '
        'img_GammaC_fi
        '
        Me.img_GammaC_fi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_GammaC_fi.Location = New System.Drawing.Point(3, 60)
        Me.img_GammaC_fi.Name = "img_GammaC_fi"
        Me.img_GammaC_fi.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaC_fi.TabIndex = 97
        Me.img_GammaC_fi.TabStop = False
        '
        'txt_GammaM_fi
        '
        Me.txt_GammaM_fi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_GammaM_fi.Location = New System.Drawing.Point(48, 34)
        Me.txt_GammaM_fi.Name = "txt_GammaM_fi"
        Me.txt_GammaM_fi.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaM_fi.TabIndex = 94
        '
        'img_GammaM_fi
        '
        Me.img_GammaM_fi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_GammaM_fi.Location = New System.Drawing.Point(3, 34)
        Me.img_GammaM_fi.Name = "img_GammaM_fi"
        Me.img_GammaM_fi.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaM_fi.TabIndex = 95
        Me.img_GammaM_fi.TabStop = False
        '
        'lbl_Fire
        '
        Me.lbl_Fire.AutoSize = True
        Me.lbl_Fire.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Fire.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Fire.Location = New System.Drawing.Point(3, 9)
        Me.lbl_Fire.Name = "lbl_Fire"
        Me.lbl_Fire.Size = New System.Drawing.Size(40, 13)
        Me.lbl_Fire.TabIndex = 93
        Me.lbl_Fire.Text = "lbl_Fire"
        Me.lbl_Fire.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txt_GammaVs)
        Me.Panel1.Controls.Add(Me.img_GammaVs)
        Me.Panel1.Controls.Add(Me.lbl_Beton)
        Me.Panel1.Controls.Add(Me.txt_GammaC)
        Me.Panel1.Controls.Add(Me.txt_GammaP)
        Me.Panel1.Controls.Add(Me.img_GammaC)
        Me.Panel1.Controls.Add(Me.img_GammaP)
        Me.Panel1.Controls.Add(Me.img_GammaV)
        Me.Panel1.Controls.Add(Me.txt_GammaS)
        Me.Panel1.Controls.Add(Me.txt_GammaV)
        Me.Panel1.Controls.Add(Me.img_GammaS)
        Me.Panel1.Location = New System.Drawing.Point(146, 161)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(139, 195)
        Me.Panel1.TabIndex = 101
        '
        'txt_GammaVs
        '
        Me.txt_GammaVs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_GammaVs.Location = New System.Drawing.Point(48, 173)
        Me.txt_GammaVs.Name = "txt_GammaVs"
        Me.txt_GammaVs.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaVs.TabIndex = 106
        '
        'img_GammaVs
        '
        Me.img_GammaVs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_GammaVs.Location = New System.Drawing.Point(2, 173)
        Me.img_GammaVs.Name = "img_GammaVs"
        Me.img_GammaVs.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaVs.TabIndex = 110
        Me.img_GammaVs.TabStop = False
        '
        'lbl_Beton
        '
        Me.lbl_Beton.AutoSize = True
        Me.lbl_Beton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Beton.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Beton.Location = New System.Drawing.Point(3, 9)
        Me.lbl_Beton.Name = "lbl_Beton"
        Me.lbl_Beton.Size = New System.Drawing.Size(51, 13)
        Me.lbl_Beton.TabIndex = 93
        Me.lbl_Beton.Text = "lbl_Beton"
        Me.lbl_Beton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_GammaC
        '
        Me.txt_GammaC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_GammaC.Location = New System.Drawing.Point(48, 34)
        Me.txt_GammaC.Name = "txt_GammaC"
        Me.txt_GammaC.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaC.TabIndex = 102
        '
        'txt_GammaP
        '
        Me.txt_GammaP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_GammaP.Location = New System.Drawing.Point(48, 113)
        Me.txt_GammaP.Name = "txt_GammaP"
        Me.txt_GammaP.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaP.TabIndex = 111
        '
        'img_GammaC
        '
        Me.img_GammaC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_GammaC.Location = New System.Drawing.Point(2, 34)
        Me.img_GammaC.Name = "img_GammaC"
        Me.img_GammaC.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaC.TabIndex = 103
        Me.img_GammaC.TabStop = False
        '
        'img_GammaP
        '
        Me.img_GammaP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_GammaP.Location = New System.Drawing.Point(2, 113)
        Me.img_GammaP.Name = "img_GammaP"
        Me.img_GammaP.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaP.TabIndex = 108
        Me.img_GammaP.TabStop = False
        '
        'img_GammaV
        '
        Me.img_GammaV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_GammaV.Location = New System.Drawing.Point(2, 60)
        Me.img_GammaV.Name = "img_GammaV"
        Me.img_GammaV.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaV.TabIndex = 105
        Me.img_GammaV.TabStop = False
        '
        'txt_GammaS
        '
        Me.txt_GammaS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_GammaS.Location = New System.Drawing.Point(48, 86)
        Me.txt_GammaS.Name = "txt_GammaS"
        Me.txt_GammaS.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaS.TabIndex = 109
        '
        'txt_GammaV
        '
        Me.txt_GammaV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_GammaV.Location = New System.Drawing.Point(48, 60)
        Me.txt_GammaV.Name = "txt_GammaV"
        Me.txt_GammaV.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaV.TabIndex = 104
        '
        'img_GammaS
        '
        Me.img_GammaS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_GammaS.Location = New System.Drawing.Point(2, 86)
        Me.img_GammaS.Name = "img_GammaS"
        Me.img_GammaS.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaS.TabIndex = 107
        Me.img_GammaS.TabStop = False
        '
        'pan_Steel
        '
        Me.pan_Steel.Controls.Add(Me.lbl_Acier)
        Me.pan_Steel.Controls.Add(Me.txt_GammaM2)
        Me.pan_Steel.Controls.Add(Me.img_GammaM2)
        Me.pan_Steel.Controls.Add(Me.txt_GammaM1)
        Me.pan_Steel.Controls.Add(Me.img_GammaM1)
        Me.pan_Steel.Controls.Add(Me.txt_GammaM0)
        Me.pan_Steel.Controls.Add(Me.img_GammaM0)
        Me.pan_Steel.Location = New System.Drawing.Point(11, 161)
        Me.pan_Steel.Name = "pan_Steel"
        Me.pan_Steel.Size = New System.Drawing.Size(135, 117)
        Me.pan_Steel.TabIndex = 100
        '
        'lbl_Acier
        '
        Me.lbl_Acier.AutoSize = True
        Me.lbl_Acier.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Acier.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Acier.Location = New System.Drawing.Point(3, 9)
        Me.lbl_Acier.Name = "lbl_Acier"
        Me.lbl_Acier.Size = New System.Drawing.Size(47, 13)
        Me.lbl_Acier.TabIndex = 93
        Me.lbl_Acier.Text = "lbl_Acier"
        Me.lbl_Acier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_GammaM2
        '
        Me.txt_GammaM2.Location = New System.Drawing.Point(48, 86)
        Me.txt_GammaM2.Name = "txt_GammaM2"
        Me.txt_GammaM2.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaM2.TabIndex = 91
        '
        'img_GammaM2
        '
        Me.img_GammaM2.Location = New System.Drawing.Point(2, 86)
        Me.img_GammaM2.Name = "img_GammaM2"
        Me.img_GammaM2.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaM2.TabIndex = 92
        Me.img_GammaM2.TabStop = False
        '
        'txt_GammaM1
        '
        Me.txt_GammaM1.Location = New System.Drawing.Point(48, 60)
        Me.txt_GammaM1.Name = "txt_GammaM1"
        Me.txt_GammaM1.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaM1.TabIndex = 89
        '
        'img_GammaM1
        '
        Me.img_GammaM1.Location = New System.Drawing.Point(2, 60)
        Me.img_GammaM1.Name = "img_GammaM1"
        Me.img_GammaM1.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaM1.TabIndex = 90
        Me.img_GammaM1.TabStop = False
        '
        'txt_GammaM0
        '
        Me.txt_GammaM0.Location = New System.Drawing.Point(48, 34)
        Me.txt_GammaM0.Name = "txt_GammaM0"
        Me.txt_GammaM0.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaM0.TabIndex = 87
        '
        'img_GammaM0
        '
        Me.img_GammaM0.Location = New System.Drawing.Point(2, 34)
        Me.img_GammaM0.Name = "img_GammaM0"
        Me.img_GammaM0.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaM0.TabIndex = 88
        Me.img_GammaM0.TabStop = False
        '
        'pan_Combination
        '
        Me.pan_Combination.Controls.Add(Me.lbl_Combination)
        Me.pan_Combination.Controls.Add(Me.img_Psi0)
        Me.pan_Combination.Controls.Add(Me.img_Psi1)
        Me.pan_Combination.Controls.Add(Me.txt_Psi0)
        Me.pan_Combination.Controls.Add(Me.txt_Psi2)
        Me.pan_Combination.Controls.Add(Me.txt_Psi1)
        Me.pan_Combination.Controls.Add(Me.img_Psi2)
        Me.pan_Combination.Location = New System.Drawing.Point(197, 7)
        Me.pan_Combination.Name = "pan_Combination"
        Me.pan_Combination.Size = New System.Drawing.Size(155, 117)
        Me.pan_Combination.TabIndex = 99
        '
        'lbl_Combination
        '
        Me.lbl_Combination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Combination.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Combination.Name = "lbl_Combination"
        Me.lbl_Combination.Size = New System.Drawing.Size(152, 23)
        Me.lbl_Combination.TabIndex = 96
        Me.lbl_Combination.Text = "lbl_Combination"
        Me.lbl_Combination.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'img_Psi0
        '
        Me.img_Psi0.Location = New System.Drawing.Point(2, 29)
        Me.img_Psi0.Name = "img_Psi0"
        Me.img_Psi0.Size = New System.Drawing.Size(46, 20)
        Me.img_Psi0.TabIndex = 90
        Me.img_Psi0.TabStop = False
        '
        'img_Psi1
        '
        Me.img_Psi1.Location = New System.Drawing.Point(2, 55)
        Me.img_Psi1.Name = "img_Psi1"
        Me.img_Psi1.Size = New System.Drawing.Size(46, 20)
        Me.img_Psi1.TabIndex = 91
        Me.img_Psi1.TabStop = False
        '
        'txt_Psi0
        '
        Me.txt_Psi0.Location = New System.Drawing.Point(48, 29)
        Me.txt_Psi0.Name = "txt_Psi0"
        Me.txt_Psi0.Size = New System.Drawing.Size(58, 20)
        Me.txt_Psi0.TabIndex = 92
        '
        'txt_Psi2
        '
        Me.txt_Psi2.Location = New System.Drawing.Point(48, 81)
        Me.txt_Psi2.Name = "txt_Psi2"
        Me.txt_Psi2.Size = New System.Drawing.Size(58, 20)
        Me.txt_Psi2.TabIndex = 95
        '
        'txt_Psi1
        '
        Me.txt_Psi1.Location = New System.Drawing.Point(48, 55)
        Me.txt_Psi1.Name = "txt_Psi1"
        Me.txt_Psi1.Size = New System.Drawing.Size(58, 20)
        Me.txt_Psi1.TabIndex = 93
        '
        'img_Psi2
        '
        Me.img_Psi2.Location = New System.Drawing.Point(2, 81)
        Me.img_Psi2.Name = "img_Psi2"
        Me.img_Psi2.Size = New System.Drawing.Size(46, 20)
        Me.img_Psi2.TabIndex = 94
        Me.img_Psi2.TabStop = False
        '
        'pan_Loads
        '
        Me.pan_Loads.Controls.Add(Me.img_GammaGinf)
        Me.pan_Loads.Controls.Add(Me.txt_GammaGinf)
        Me.pan_Loads.Controls.Add(Me.img_GammaGsup)
        Me.pan_Loads.Controls.Add(Me.img_GammaQ)
        Me.pan_Loads.Controls.Add(Me.txt_GammaGsup)
        Me.pan_Loads.Controls.Add(Me.txt_GammaQ)
        Me.pan_Loads.Location = New System.Drawing.Point(11, 7)
        Me.pan_Loads.Name = "pan_Loads"
        Me.pan_Loads.Size = New System.Drawing.Size(155, 117)
        Me.pan_Loads.TabIndex = 98
        '
        'img_GammaGinf
        '
        Me.img_GammaGinf.Location = New System.Drawing.Point(2, 55)
        Me.img_GammaGinf.Name = "img_GammaGinf"
        Me.img_GammaGinf.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaGinf.TabIndex = 82
        Me.img_GammaGinf.TabStop = False
        '
        'txt_GammaGinf
        '
        Me.txt_GammaGinf.Location = New System.Drawing.Point(48, 55)
        Me.txt_GammaGinf.Name = "txt_GammaGinf"
        Me.txt_GammaGinf.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaGinf.TabIndex = 81
        '
        'img_GammaGsup
        '
        Me.img_GammaGsup.Location = New System.Drawing.Point(2, 29)
        Me.img_GammaGsup.Name = "img_GammaGsup"
        Me.img_GammaGsup.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaGsup.TabIndex = 80
        Me.img_GammaGsup.TabStop = False
        '
        'img_GammaQ
        '
        Me.img_GammaQ.Location = New System.Drawing.Point(2, 81)
        Me.img_GammaQ.Name = "img_GammaQ"
        Me.img_GammaQ.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaQ.TabIndex = 84
        Me.img_GammaQ.TabStop = False
        '
        'txt_GammaGsup
        '
        Me.txt_GammaGsup.Location = New System.Drawing.Point(48, 29)
        Me.txt_GammaGsup.Name = "txt_GammaGsup"
        Me.txt_GammaGsup.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaGsup.TabIndex = 79
        '
        'txt_GammaQ
        '
        Me.txt_GammaQ.Location = New System.Drawing.Point(48, 81)
        Me.txt_GammaQ.Name = "txt_GammaQ"
        Me.txt_GammaQ.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaQ.TabIndex = 83
        '
        'lbl_Materials
        '
        Me.lbl_Materials.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Materials.Location = New System.Drawing.Point(11, 135)
        Me.lbl_Materials.Name = "lbl_Materials"
        Me.lbl_Materials.Size = New System.Drawing.Size(413, 23)
        Me.lbl_Materials.TabIndex = 97
        Me.lbl_Materials.Text = "lbl_Materials"
        Me.lbl_Materials.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_OptionsLogicielGeneral
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(904, 533)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsLogicielGeneral"
        Me.Text = "Frm_OptionsLogicielGeneral"
        Me.pan_General.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.img_GammaV_fi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaC_fi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaM_fi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.img_GammaVs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaV, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Steel.ResumeLayout(False)
        Me.pan_Steel.PerformLayout()
        CType(Me.img_GammaM2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaM1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaM0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Combination.ResumeLayout(False)
        Me.pan_Combination.PerformLayout()
        CType(Me.img_Psi0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Psi1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Psi2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Loads.ResumeLayout(False)
        Me.pan_Loads.PerformLayout()
        CType(Me.img_GammaGinf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaGsup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaQ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents lbl_Loads As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txt_GammaV_fi As TextBox
    Friend WithEvents img_GammaV_fi As PictureBox
    Friend WithEvents txt_GammaC_fi As TextBox
    Friend WithEvents img_GammaC_fi As PictureBox
    Friend WithEvents txt_GammaM_fi As TextBox
    Friend WithEvents img_GammaM_fi As PictureBox
    Friend WithEvents lbl_Fire As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txt_GammaVs As TextBox
    Friend WithEvents img_GammaVs As PictureBox
    Friend WithEvents lbl_Beton As Label
    Friend WithEvents txt_GammaC As TextBox
    Friend WithEvents txt_GammaP As TextBox
    Friend WithEvents img_GammaC As PictureBox
    Friend WithEvents img_GammaP As PictureBox
    Friend WithEvents img_GammaV As PictureBox
    Friend WithEvents txt_GammaS As TextBox
    Friend WithEvents txt_GammaV As TextBox
    Friend WithEvents img_GammaS As PictureBox
    Friend WithEvents pan_Steel As Panel
    Friend WithEvents lbl_Acier As Label
    Friend WithEvents txt_GammaM2 As TextBox
    Friend WithEvents img_GammaM2 As PictureBox
    Friend WithEvents txt_GammaM1 As TextBox
    Friend WithEvents img_GammaM1 As PictureBox
    Friend WithEvents txt_GammaM0 As TextBox
    Friend WithEvents img_GammaM0 As PictureBox
    Friend WithEvents pan_Combination As Panel
    Friend WithEvents lbl_Combination As Label
    Friend WithEvents img_Psi0 As PictureBox
    Friend WithEvents img_Psi1 As PictureBox
    Friend WithEvents txt_Psi0 As TextBox
    Friend WithEvents txt_Psi2 As TextBox
    Friend WithEvents txt_Psi1 As TextBox
    Friend WithEvents img_Psi2 As PictureBox
    Friend WithEvents pan_Loads As Panel
    Friend WithEvents img_GammaGinf As PictureBox
    Friend WithEvents txt_GammaGinf As TextBox
    Friend WithEvents img_GammaGsup As PictureBox
    Friend WithEvents img_GammaQ As PictureBox
    Friend WithEvents txt_GammaGsup As TextBox
    Friend WithEvents txt_GammaQ As TextBox
    Friend WithEvents lbl_Materials As Label
End Class
