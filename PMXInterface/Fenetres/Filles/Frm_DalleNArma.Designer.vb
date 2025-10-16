<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_DalleNArma
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_DalleNArma))
        Me.Pan_Contenu = New System.Windows.Forms.Panel()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txt_Fsk = New System.Windows.Forms.TextBox()
        Me.etq_UnitSigma2 = New System.Windows.Forms.Label()
        Me.cmb_Acier = New System.Windows.Forms.ComboBox()
        Me.lbl_ClasseA = New System.Windows.Forms.Label()
        Me.lbl_Acier = New System.Windows.Forms.Label()
        Me.lbl_Armatures = New System.Windows.Forms.Label()
        Me.pan_Armatures = New System.Windows.Forms.Panel()
        Me.pan_DonneesArma = New System.Windows.Forms.Panel()
        Me.lbl_LitNo = New System.Windows.Forms.Label()
        Me.txt_zs = New System.Windows.Forms.TextBox()
        Me.lbl_zs = New System.Windows.Forms.Label()
        Me.etq_UnitDim6 = New System.Windows.Forms.Label()
        Me.txt_esp = New System.Windows.Forms.TextBox()
        Me.lbl_Espacement = New System.Windows.Forms.Label()
        Me.etq_UnitDim5 = New System.Windows.Forms.Label()
        Me.txt_PhiS = New System.Windows.Forms.TextBox()
        Me.lbl_Diametre = New System.Windows.Forms.Label()
        Me.etq_UnitDim7 = New System.Windows.Forms.Label()
        Me.lbl_NoArma = New System.Windows.Forms.Label()
        Me.img_Fy = New System.Windows.Forms.PictureBox()
        Me.chk_Lit0 = New System.Windows.Forms.CheckBox()
        Me.img_zs = New System.Windows.Forms.PictureBox()
        Me.img_esp = New System.Windows.Forms.PictureBox()
        Me.img_PhiS = New System.Windows.Forms.PictureBox()
        Me.chk_AjouterSupprimerLit = New System.Windows.Forms.CheckBox()
        Me.chk_Lit2 = New System.Windows.Forms.CheckBox()
        Me.chk_Lit1 = New System.Windows.Forms.CheckBox()
        Me.ToolTipDalle = New System.Windows.Forms.ToolTip(Me.components)
        Me.Pan_Contenu.SuspendLayout()
        Me.TLpan_Gauche.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pan_Armatures.SuspendLayout()
        Me.pan_DonneesArma.SuspendLayout()
        CType(Me.img_Fy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_zs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_esp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_PhiS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Pan_Contenu
        '
        Me.Pan_Contenu.Controls.Add(Me.TLpan_Gauche)
        Me.Pan_Contenu.Location = New System.Drawing.Point(203, 6)
        Me.Pan_Contenu.Name = "Pan_Contenu"
        Me.Pan_Contenu.Size = New System.Drawing.Size(394, 578)
        Me.Pan_Contenu.TabIndex = 1
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.Panel2, 0, 3)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_Acier, 0, 2)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_Armatures, 0, 0)
        Me.TLpan_Gauche.Controls.Add(Me.pan_Armatures, 0, 1)
        Me.TLpan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Gauche.Name = "TLpan_Gauche"
        Me.TLpan_Gauche.RowCount = 4
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 185.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Gauche.Size = New System.Drawing.Size(394, 578)
        Me.TLpan_Gauche.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.txt_Fsk)
        Me.Panel2.Controls.Add(Me.img_Fy)
        Me.Panel2.Controls.Add(Me.etq_UnitSigma2)
        Me.Panel2.Controls.Add(Me.cmb_Acier)
        Me.Panel2.Controls.Add(Me.lbl_ClasseA)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 259)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(394, 319)
        Me.Panel2.TabIndex = 15
        '
        'txt_Fsk
        '
        Me.txt_Fsk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Fsk.Location = New System.Drawing.Point(259, 41)
        Me.txt_Fsk.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_Fsk.Name = "txt_Fsk"
        Me.txt_Fsk.Size = New System.Drawing.Size(76, 22)
        Me.txt_Fsk.TabIndex = 59
        '
        'etq_UnitSigma2
        '
        Me.etq_UnitSigma2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitSigma2.AutoSize = True
        Me.etq_UnitSigma2.Location = New System.Drawing.Point(344, 44)
        Me.etq_UnitSigma2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.etq_UnitSigma2.Name = "etq_UnitSigma2"
        Me.etq_UnitSigma2.Size = New System.Drawing.Size(29, 16)
        Me.etq_UnitSigma2.TabIndex = 58
        Me.etq_UnitSigma2.Text = "mm"
        '
        'cmb_Acier
        '
        Me.cmb_Acier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Acier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Acier.FormattingEnabled = True
        Me.cmb_Acier.Location = New System.Drawing.Point(169, 7)
        Me.cmb_Acier.Margin = New System.Windows.Forms.Padding(4)
        Me.cmb_Acier.Name = "cmb_Acier"
        Me.cmb_Acier.Size = New System.Drawing.Size(208, 24)
        Me.cmb_Acier.TabIndex = 57
        '
        'lbl_ClasseA
        '
        Me.lbl_ClasseA.AutoSize = True
        Me.lbl_ClasseA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ClasseA.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_ClasseA.Location = New System.Drawing.Point(15, 11)
        Me.lbl_ClasseA.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ClasseA.Name = "lbl_ClasseA"
        Me.lbl_ClasseA.Size = New System.Drawing.Size(61, 13)
        Me.lbl_ClasseA.TabIndex = 56
        Me.lbl_ClasseA.Text = "lbl_ClasseA"
        Me.lbl_ClasseA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Acier
        '
        Me.lbl_Acier.BackColor = System.Drawing.SystemColors.ControlDark
        Me.lbl_Acier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Acier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Acier.Location = New System.Drawing.Point(0, 222)
        Me.lbl_Acier.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Acier.Name = "lbl_Acier"
        Me.lbl_Acier.Size = New System.Drawing.Size(394, 37)
        Me.lbl_Acier.TabIndex = 14
        Me.lbl_Acier.Text = "lbl_Acier"
        Me.lbl_Acier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Armatures
        '
        Me.lbl_Armatures.AutoSize = True
        Me.lbl_Armatures.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Armatures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Armatures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Armatures.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Armatures.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Armatures.Name = "lbl_Armatures"
        Me.lbl_Armatures.Size = New System.Drawing.Size(394, 37)
        Me.lbl_Armatures.TabIndex = 10
        Me.lbl_Armatures.Text = "lbl_Armatures"
        Me.lbl_Armatures.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Armatures
        '
        Me.pan_Armatures.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Armatures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Armatures.Controls.Add(Me.chk_Lit0)
        Me.pan_Armatures.Controls.Add(Me.pan_DonneesArma)
        Me.pan_Armatures.Controls.Add(Me.chk_AjouterSupprimerLit)
        Me.pan_Armatures.Controls.Add(Me.chk_Lit2)
        Me.pan_Armatures.Controls.Add(Me.chk_Lit1)
        Me.pan_Armatures.Controls.Add(Me.lbl_NoArma)
        Me.pan_Armatures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Armatures.Location = New System.Drawing.Point(0, 37)
        Me.pan_Armatures.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Armatures.Name = "pan_Armatures"
        Me.pan_Armatures.Size = New System.Drawing.Size(394, 184)
        Me.pan_Armatures.TabIndex = 11
        '
        'pan_DonneesArma
        '
        Me.pan_DonneesArma.Controls.Add(Me.lbl_LitNo)
        Me.pan_DonneesArma.Controls.Add(Me.txt_zs)
        Me.pan_DonneesArma.Controls.Add(Me.lbl_zs)
        Me.pan_DonneesArma.Controls.Add(Me.etq_UnitDim6)
        Me.pan_DonneesArma.Controls.Add(Me.img_zs)
        Me.pan_DonneesArma.Controls.Add(Me.txt_esp)
        Me.pan_DonneesArma.Controls.Add(Me.lbl_Espacement)
        Me.pan_DonneesArma.Controls.Add(Me.etq_UnitDim5)
        Me.pan_DonneesArma.Controls.Add(Me.img_esp)
        Me.pan_DonneesArma.Controls.Add(Me.txt_PhiS)
        Me.pan_DonneesArma.Controls.Add(Me.lbl_Diametre)
        Me.pan_DonneesArma.Controls.Add(Me.etq_UnitDim7)
        Me.pan_DonneesArma.Controls.Add(Me.img_PhiS)
        Me.pan_DonneesArma.Location = New System.Drawing.Point(1, 59)
        Me.pan_DonneesArma.Margin = New System.Windows.Forms.Padding(4)
        Me.pan_DonneesArma.Name = "pan_DonneesArma"
        Me.pan_DonneesArma.Size = New System.Drawing.Size(328, 123)
        Me.pan_DonneesArma.TabIndex = 5
        '
        'lbl_LitNo
        '
        Me.lbl_LitNo.AutoSize = True
        Me.lbl_LitNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LitNo.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_LitNo.Location = New System.Drawing.Point(16, 7)
        Me.lbl_LitNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_LitNo.Name = "lbl_LitNo"
        Me.lbl_LitNo.Size = New System.Drawing.Size(48, 13)
        Me.lbl_LitNo.TabIndex = 85
        Me.lbl_LitNo.Text = "lbl_LitNo"
        Me.lbl_LitNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_zs
        '
        Me.txt_zs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_zs.Location = New System.Drawing.Point(197, 92)
        Me.txt_zs.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_zs.Name = "txt_zs"
        Me.txt_zs.Size = New System.Drawing.Size(76, 22)
        Me.txt_zs.TabIndex = 83
        '
        'lbl_zs
        '
        Me.lbl_zs.AutoSize = True
        Me.lbl_zs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_zs.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_zs.Location = New System.Drawing.Point(16, 96)
        Me.lbl_zs.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_zs.Name = "lbl_zs"
        Me.lbl_zs.Size = New System.Drawing.Size(33, 13)
        Me.lbl_zs.TabIndex = 81
        Me.lbl_zs.Text = "lbl_zs"
        Me.lbl_zs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_UnitDim6
        '
        Me.etq_UnitDim6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim6.AutoSize = True
        Me.etq_UnitDim6.Location = New System.Drawing.Point(283, 96)
        Me.etq_UnitDim6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.etq_UnitDim6.Name = "etq_UnitDim6"
        Me.etq_UnitDim6.Size = New System.Drawing.Size(29, 16)
        Me.etq_UnitDim6.TabIndex = 82
        Me.etq_UnitDim6.Text = "mm"
        '
        'txt_esp
        '
        Me.txt_esp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_esp.Location = New System.Drawing.Point(197, 63)
        Me.txt_esp.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_esp.Name = "txt_esp"
        Me.txt_esp.Size = New System.Drawing.Size(76, 22)
        Me.txt_esp.TabIndex = 79
        '
        'lbl_Espacement
        '
        Me.lbl_Espacement.AutoSize = True
        Me.lbl_Espacement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Espacement.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Espacement.Location = New System.Drawing.Point(16, 66)
        Me.lbl_Espacement.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Espacement.Name = "lbl_Espacement"
        Me.lbl_Espacement.Size = New System.Drawing.Size(82, 13)
        Me.lbl_Espacement.TabIndex = 77
        Me.lbl_Espacement.Text = "lbl_Espacement"
        Me.lbl_Espacement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_UnitDim5
        '
        Me.etq_UnitDim5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim5.AutoSize = True
        Me.etq_UnitDim5.Location = New System.Drawing.Point(283, 66)
        Me.etq_UnitDim5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.etq_UnitDim5.Name = "etq_UnitDim5"
        Me.etq_UnitDim5.Size = New System.Drawing.Size(29, 16)
        Me.etq_UnitDim5.TabIndex = 78
        Me.etq_UnitDim5.Text = "mm"
        '
        'txt_PhiS
        '
        Me.txt_PhiS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PhiS.Location = New System.Drawing.Point(197, 33)
        Me.txt_PhiS.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_PhiS.Name = "txt_PhiS"
        Me.txt_PhiS.Size = New System.Drawing.Size(76, 22)
        Me.txt_PhiS.TabIndex = 75
        '
        'lbl_Diametre
        '
        Me.lbl_Diametre.AutoSize = True
        Me.lbl_Diametre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Diametre.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Diametre.Location = New System.Drawing.Point(16, 37)
        Me.lbl_Diametre.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Diametre.Name = "lbl_Diametre"
        Me.lbl_Diametre.Size = New System.Drawing.Size(65, 13)
        Me.lbl_Diametre.TabIndex = 73
        Me.lbl_Diametre.Text = "lbl_Diametre"
        Me.lbl_Diametre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_UnitDim7
        '
        Me.etq_UnitDim7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim7.AutoSize = True
        Me.etq_UnitDim7.Location = New System.Drawing.Point(283, 37)
        Me.etq_UnitDim7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.etq_UnitDim7.Name = "etq_UnitDim7"
        Me.etq_UnitDim7.Size = New System.Drawing.Size(29, 16)
        Me.etq_UnitDim7.TabIndex = 74
        Me.etq_UnitDim7.Text = "mm"
        '
        'lbl_NoArma
        '
        Me.lbl_NoArma.Location = New System.Drawing.Point(3, 59)
        Me.lbl_NoArma.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_NoArma.Name = "lbl_NoArma"
        Me.lbl_NoArma.Size = New System.Drawing.Size(328, 70)
        Me.lbl_NoArma.TabIndex = 7
        Me.lbl_NoArma.Text = "lbl_NoArma"
        Me.lbl_NoArma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'img_Fy
        '
        Me.img_Fy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Fy.Location = New System.Drawing.Point(184, 41)
        Me.img_Fy.Margin = New System.Windows.Forms.Padding(4)
        Me.img_Fy.Name = "img_Fy"
        Me.img_Fy.Size = New System.Drawing.Size(76, 25)
        Me.img_Fy.TabIndex = 60
        Me.img_Fy.TabStop = False
        '
        'chk_Lit0
        '
        Me.chk_Lit0.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Lit0.Image = CType(resources.GetObject("chk_Lit0.Image"), System.Drawing.Image)
        Me.chk_Lit0.Location = New System.Drawing.Point(3, 5)
        Me.chk_Lit0.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_Lit0.Name = "chk_Lit0"
        Me.chk_Lit0.Size = New System.Drawing.Size(59, 54)
        Me.chk_Lit0.TabIndex = 6
        Me.chk_Lit0.UseVisualStyleBackColor = True
        '
        'img_zs
        '
        Me.img_zs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_zs.Location = New System.Drawing.Point(149, 92)
        Me.img_zs.Margin = New System.Windows.Forms.Padding(4)
        Me.img_zs.Name = "img_zs"
        Me.img_zs.Size = New System.Drawing.Size(49, 25)
        Me.img_zs.TabIndex = 84
        Me.img_zs.TabStop = False
        '
        'img_esp
        '
        Me.img_esp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_esp.Location = New System.Drawing.Point(149, 63)
        Me.img_esp.Margin = New System.Windows.Forms.Padding(4)
        Me.img_esp.Name = "img_esp"
        Me.img_esp.Size = New System.Drawing.Size(49, 25)
        Me.img_esp.TabIndex = 80
        Me.img_esp.TabStop = False
        '
        'img_PhiS
        '
        Me.img_PhiS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PhiS.Location = New System.Drawing.Point(149, 33)
        Me.img_PhiS.Margin = New System.Windows.Forms.Padding(4)
        Me.img_PhiS.Name = "img_PhiS"
        Me.img_PhiS.Size = New System.Drawing.Size(49, 25)
        Me.img_PhiS.TabIndex = 76
        Me.img_PhiS.TabStop = False
        '
        'chk_AjouterSupprimerLit
        '
        Me.chk_AjouterSupprimerLit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chk_AjouterSupprimerLit.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_AjouterSupprimerLit.Image = CType(resources.GetObject("chk_AjouterSupprimerLit.Image"), System.Drawing.Image)
        Me.chk_AjouterSupprimerLit.Location = New System.Drawing.Point(325, 5)
        Me.chk_AjouterSupprimerLit.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_AjouterSupprimerLit.Name = "chk_AjouterSupprimerLit"
        Me.chk_AjouterSupprimerLit.Size = New System.Drawing.Size(59, 54)
        Me.chk_AjouterSupprimerLit.TabIndex = 4
        Me.chk_AjouterSupprimerLit.UseVisualStyleBackColor = True
        '
        'chk_Lit2
        '
        Me.chk_Lit2.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Lit2.Image = CType(resources.GetObject("chk_Lit2.Image"), System.Drawing.Image)
        Me.chk_Lit2.Location = New System.Drawing.Point(120, 5)
        Me.chk_Lit2.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_Lit2.Name = "chk_Lit2"
        Me.chk_Lit2.Size = New System.Drawing.Size(59, 54)
        Me.chk_Lit2.TabIndex = 3
        Me.chk_Lit2.UseVisualStyleBackColor = True
        '
        'chk_Lit1
        '
        Me.chk_Lit1.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Lit1.Image = CType(resources.GetObject("chk_Lit1.Image"), System.Drawing.Image)
        Me.chk_Lit1.Location = New System.Drawing.Point(61, 5)
        Me.chk_Lit1.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_Lit1.Name = "chk_Lit1"
        Me.chk_Lit1.Size = New System.Drawing.Size(59, 54)
        Me.chk_Lit1.TabIndex = 2
        Me.chk_Lit1.UseVisualStyleBackColor = True
        '
        'Frm_DalleNArma
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 590)
        Me.Controls.Add(Me.Pan_Contenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_DalleNArma"
        Me.Text = "Frm_DanneNArma"
        Me.Pan_Contenu.ResumeLayout(False)
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pan_Armatures.ResumeLayout(False)
        Me.pan_DonneesArma.ResumeLayout(False)
        Me.pan_DonneesArma.PerformLayout()
        CType(Me.img_Fy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_zs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_esp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_PhiS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Pan_Contenu As Panel
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txt_Fsk As TextBox
    Friend WithEvents img_Fy As PictureBox
    Friend WithEvents etq_UnitSigma2 As Label
    Friend WithEvents cmb_Acier As ComboBox
    Friend WithEvents lbl_ClasseA As Label
    Friend WithEvents lbl_Acier As Label
    Friend WithEvents lbl_Armatures As Label
    Friend WithEvents pan_Armatures As Panel
    Friend WithEvents chk_Lit0 As CheckBox
    Friend WithEvents pan_DonneesArma As Panel
    Friend WithEvents lbl_LitNo As Label
    Friend WithEvents txt_zs As TextBox
    Friend WithEvents lbl_zs As Label
    Friend WithEvents etq_UnitDim6 As Label
    Friend WithEvents img_zs As PictureBox
    Friend WithEvents txt_esp As TextBox
    Friend WithEvents lbl_Espacement As Label
    Friend WithEvents etq_UnitDim5 As Label
    Friend WithEvents img_esp As PictureBox
    Friend WithEvents txt_PhiS As TextBox
    Friend WithEvents lbl_Diametre As Label
    Friend WithEvents etq_UnitDim7 As Label
    Friend WithEvents img_PhiS As PictureBox
    Friend WithEvents chk_AjouterSupprimerLit As CheckBox
    Friend WithEvents chk_Lit2 As CheckBox
    Friend WithEvents chk_Lit1 As CheckBox
    Friend WithEvents lbl_NoArma As Label
    Friend WithEvents ToolTipDalle As ToolTip
End Class
