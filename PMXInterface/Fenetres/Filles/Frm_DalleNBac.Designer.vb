<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_DalleNBac
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_DalleNBac))
        Me.Pan_Main = New System.Windows.Forms.Panel()
        Me.TLpan_Milieu = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Bac = New System.Windows.Forms.Panel()
        Me.pan_DispoConnecteur = New System.Windows.Forms.Panel()
        Me.rdb_Preperce = New System.Windows.Forms.RadioButton()
        Me.rdb_ATraversBac = New System.Windows.Forms.RadioButton()
        Me.lbl_ConnectorThroughTheWeb = New System.Windows.Forms.Label()
        Me.pan_Orientation = New System.Windows.Forms.Panel()
        Me.lbl_BacOrientation = New System.Windows.Forms.Label()
        Me.rdb_BacPerpendiculaire = New System.Windows.Forms.RadioButton()
        Me.rdb_BacParallele = New System.Windows.Forms.RadioButton()
        Me.pan_ConfigurationNervures = New System.Windows.Forms.Panel()
        Me.chk_L_PA1 = New System.Windows.Forms.CheckBox()
        Me.chk_L_PA2 = New System.Windows.Forms.CheckBox()
        Me.rtxt_Configuration = New System.Windows.Forms.RichTextBox()
        Me.chk_T_PA3 = New System.Windows.Forms.CheckBox()
        Me.chk_T_PA2 = New System.Windows.Forms.CheckBox()
        Me.chk_T_PA1 = New System.Windows.Forms.CheckBox()
        Me.lbl_BacConfiguration = New System.Windows.Forms.Label()
        Me.img_Bac = New System.Windows.Forms.PictureBox()
        Me.btn_ModifierBac = New System.Windows.Forms.Button()
        Me.lbl_BacNom = New System.Windows.Forms.Label()
        Me.txt_BacNom = New System.Windows.Forms.TextBox()
        Me.lbl_Bac = New System.Windows.Forms.Label()
        Me.pan_DecalageCofra220 = New System.Windows.Forms.Panel()
        Me.txt_DecalCofra220 = New System.Windows.Forms.TextBox()
        Me.img_Decal = New System.Windows.Forms.PictureBox()
        Me.etq_UnitDim1 = New System.Windows.Forms.Label()
        Me.lbl_Decalage = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Pan_Main.SuspendLayout()
        Me.TLpan_Milieu.SuspendLayout()
        Me.pan_Bac.SuspendLayout()
        Me.pan_DispoConnecteur.SuspendLayout()
        Me.pan_Orientation.SuspendLayout()
        Me.pan_ConfigurationNervures.SuspendLayout()
        CType(Me.img_Bac, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_DecalageCofra220.SuspendLayout()
        CType(Me.img_Decal, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Pan_Main
        '
        Me.Pan_Main.Controls.Add(Me.TLpan_Milieu)
        Me.Pan_Main.Location = New System.Drawing.Point(152, 15)
        Me.Pan_Main.Margin = New System.Windows.Forms.Padding(2)
        Me.Pan_Main.Name = "Pan_Main"
        Me.Pan_Main.Size = New System.Drawing.Size(296, 470)
        Me.Pan_Main.TabIndex = 1
        '
        'TLpan_Milieu
        '
        Me.TLpan_Milieu.ColumnCount = 1
        Me.TLpan_Milieu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Milieu.Controls.Add(Me.pan_Bac, 0, 1)
        Me.TLpan_Milieu.Controls.Add(Me.lbl_Bac, 0, 0)
        Me.TLpan_Milieu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Milieu.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Milieu.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.TLpan_Milieu.Name = "TLpan_Milieu"
        Me.TLpan_Milieu.RowCount = 2
        Me.TLpan_Milieu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Milieu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Milieu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Milieu.Size = New System.Drawing.Size(296, 470)
        Me.TLpan_Milieu.TabIndex = 3
        '
        'pan_Bac
        '
        Me.pan_Bac.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Bac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Bac.Controls.Add(Me.pan_DispoConnecteur)
        Me.pan_Bac.Controls.Add(Me.pan_Orientation)
        Me.pan_Bac.Controls.Add(Me.pan_ConfigurationNervures)
        Me.pan_Bac.Controls.Add(Me.img_Bac)
        Me.pan_Bac.Controls.Add(Me.btn_ModifierBac)
        Me.pan_Bac.Controls.Add(Me.lbl_BacNom)
        Me.pan_Bac.Controls.Add(Me.txt_BacNom)
        Me.pan_Bac.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Bac.Location = New System.Drawing.Point(0, 30)
        Me.pan_Bac.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Bac.Name = "pan_Bac"
        Me.pan_Bac.Size = New System.Drawing.Size(296, 440)
        Me.pan_Bac.TabIndex = 9
        '
        'pan_DispoConnecteur
        '
        Me.pan_DispoConnecteur.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_DispoConnecteur.Controls.Add(Me.rdb_Preperce)
        Me.pan_DispoConnecteur.Controls.Add(Me.rdb_ATraversBac)
        Me.pan_DispoConnecteur.Controls.Add(Me.lbl_ConnectorThroughTheWeb)
        Me.pan_DispoConnecteur.Location = New System.Drawing.Point(3, 355)
        Me.pan_DispoConnecteur.Name = "pan_DispoConnecteur"
        Me.pan_DispoConnecteur.Size = New System.Drawing.Size(290, 74)
        Me.pan_DispoConnecteur.TabIndex = 86
        '
        'rdb_Preperce
        '
        Me.rdb_Preperce.AutoSize = True
        Me.rdb_Preperce.Location = New System.Drawing.Point(37, 25)
        Me.rdb_Preperce.Name = "rdb_Preperce"
        Me.rdb_Preperce.Size = New System.Drawing.Size(89, 17)
        Me.rdb_Preperce.TabIndex = 78
        Me.rdb_Preperce.TabStop = True
        Me.rdb_Preperce.Text = "rdb_Preperce"
        Me.rdb_Preperce.UseVisualStyleBackColor = True
        '
        'rdb_ATraversBac
        '
        Me.rdb_ATraversBac.AutoSize = True
        Me.rdb_ATraversBac.Location = New System.Drawing.Point(37, 47)
        Me.rdb_ATraversBac.Name = "rdb_ATraversBac"
        Me.rdb_ATraversBac.Size = New System.Drawing.Size(108, 17)
        Me.rdb_ATraversBac.TabIndex = 79
        Me.rdb_ATraversBac.TabStop = True
        Me.rdb_ATraversBac.Text = "rdb_ATraversBac"
        Me.rdb_ATraversBac.UseVisualStyleBackColor = True
        '
        'lbl_ConnectorThroughTheWeb
        '
        Me.lbl_ConnectorThroughTheWeb.AutoSize = True
        Me.lbl_ConnectorThroughTheWeb.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ConnectorThroughTheWeb.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_ConnectorThroughTheWeb.Location = New System.Drawing.Point(9, 7)
        Me.lbl_ConnectorThroughTheWeb.Name = "lbl_ConnectorThroughTheWeb"
        Me.lbl_ConnectorThroughTheWeb.Size = New System.Drawing.Size(154, 13)
        Me.lbl_ConnectorThroughTheWeb.TabIndex = 76
        Me.lbl_ConnectorThroughTheWeb.Text = "lbl_ConnectorThroughTheWeb"
        Me.lbl_ConnectorThroughTheWeb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pan_Orientation
        '
        Me.pan_Orientation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Orientation.Controls.Add(Me.lbl_BacOrientation)
        Me.pan_Orientation.Controls.Add(Me.rdb_BacPerpendiculaire)
        Me.pan_Orientation.Controls.Add(Me.rdb_BacParallele)
        Me.pan_Orientation.Location = New System.Drawing.Point(3, 165)
        Me.pan_Orientation.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Orientation.Name = "pan_Orientation"
        Me.pan_Orientation.Size = New System.Drawing.Size(290, 67)
        Me.pan_Orientation.TabIndex = 82
        '
        'lbl_BacOrientation
        '
        Me.lbl_BacOrientation.AutoSize = True
        Me.lbl_BacOrientation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_BacOrientation.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_BacOrientation.Location = New System.Drawing.Point(9, 3)
        Me.lbl_BacOrientation.Name = "lbl_BacOrientation"
        Me.lbl_BacOrientation.Size = New System.Drawing.Size(93, 13)
        Me.lbl_BacOrientation.TabIndex = 75
        Me.lbl_BacOrientation.Text = "lbl_BacOrientation"
        Me.lbl_BacOrientation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rdb_BacPerpendiculaire
        '
        Me.rdb_BacPerpendiculaire.AutoSize = True
        Me.rdb_BacPerpendiculaire.Location = New System.Drawing.Point(37, 22)
        Me.rdb_BacPerpendiculaire.Name = "rdb_BacPerpendiculaire"
        Me.rdb_BacPerpendiculaire.Size = New System.Drawing.Size(138, 17)
        Me.rdb_BacPerpendiculaire.TabIndex = 76
        Me.rdb_BacPerpendiculaire.TabStop = True
        Me.rdb_BacPerpendiculaire.Text = "rdb_BacPerpendiculaire"
        Me.rdb_BacPerpendiculaire.UseVisualStyleBackColor = True
        '
        'rdb_BacParallele
        '
        Me.rdb_BacParallele.AutoSize = True
        Me.rdb_BacParallele.Location = New System.Drawing.Point(37, 44)
        Me.rdb_BacParallele.Name = "rdb_BacParallele"
        Me.rdb_BacParallele.Size = New System.Drawing.Size(105, 17)
        Me.rdb_BacParallele.TabIndex = 77
        Me.rdb_BacParallele.TabStop = True
        Me.rdb_BacParallele.Text = "rdb_BacParallele"
        Me.rdb_BacParallele.UseVisualStyleBackColor = True
        '
        'pan_ConfigurationNervures
        '
        Me.pan_ConfigurationNervures.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_ConfigurationNervures.Controls.Add(Me.chk_L_PA1)
        Me.pan_ConfigurationNervures.Controls.Add(Me.chk_L_PA2)
        Me.pan_ConfigurationNervures.Controls.Add(Me.rtxt_Configuration)
        Me.pan_ConfigurationNervures.Controls.Add(Me.chk_T_PA3)
        Me.pan_ConfigurationNervures.Controls.Add(Me.chk_T_PA2)
        Me.pan_ConfigurationNervures.Controls.Add(Me.chk_T_PA1)
        Me.pan_ConfigurationNervures.Controls.Add(Me.lbl_BacConfiguration)
        Me.pan_ConfigurationNervures.Location = New System.Drawing.Point(3, 231)
        Me.pan_ConfigurationNervures.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ConfigurationNervures.Name = "pan_ConfigurationNervures"
        Me.pan_ConfigurationNervures.Size = New System.Drawing.Size(290, 125)
        Me.pan_ConfigurationNervures.TabIndex = 81
        '
        'chk_L_PA1
        '
        Me.chk_L_PA1.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_L_PA1.Image = CType(resources.GetObject("chk_L_PA1.Image"), System.Drawing.Image)
        Me.chk_L_PA1.Location = New System.Drawing.Point(151, 31)
        Me.chk_L_PA1.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_L_PA1.Name = "chk_L_PA1"
        Me.chk_L_PA1.Size = New System.Drawing.Size(44, 44)
        Me.chk_L_PA1.TabIndex = 5
        Me.chk_L_PA1.UseVisualStyleBackColor = True
        '
        'chk_L_PA2
        '
        Me.chk_L_PA2.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_L_PA2.Image = CType(resources.GetObject("chk_L_PA2.Image"), System.Drawing.Image)
        Me.chk_L_PA2.Location = New System.Drawing.Point(195, 31)
        Me.chk_L_PA2.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_L_PA2.Name = "chk_L_PA2"
        Me.chk_L_PA2.Size = New System.Drawing.Size(44, 44)
        Me.chk_L_PA2.TabIndex = 4
        Me.chk_L_PA2.UseVisualStyleBackColor = True
        '
        'rtxt_Configuration
        '
        Me.rtxt_Configuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtxt_Configuration.Location = New System.Drawing.Point(15, 78)
        Me.rtxt_Configuration.Name = "rtxt_Configuration"
        Me.rtxt_Configuration.ReadOnly = True
        Me.rtxt_Configuration.Size = New System.Drawing.Size(213, 29)
        Me.rtxt_Configuration.TabIndex = 3
        Me.rtxt_Configuration.TabStop = False
        Me.rtxt_Configuration.Text = ""
        '
        'chk_T_PA3
        '
        Me.chk_T_PA3.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_T_PA3.Image = CType(resources.GetObject("chk_T_PA3.Image"), System.Drawing.Image)
        Me.chk_T_PA3.Location = New System.Drawing.Point(100, 29)
        Me.chk_T_PA3.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_T_PA3.Name = "chk_T_PA3"
        Me.chk_T_PA3.Size = New System.Drawing.Size(44, 44)
        Me.chk_T_PA3.TabIndex = 2
        Me.chk_T_PA3.UseVisualStyleBackColor = True
        '
        'chk_T_PA2
        '
        Me.chk_T_PA2.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_T_PA2.Image = CType(resources.GetObject("chk_T_PA2.Image"), System.Drawing.Image)
        Me.chk_T_PA2.Location = New System.Drawing.Point(56, 29)
        Me.chk_T_PA2.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_T_PA2.Name = "chk_T_PA2"
        Me.chk_T_PA2.Size = New System.Drawing.Size(44, 44)
        Me.chk_T_PA2.TabIndex = 1
        Me.chk_T_PA2.UseVisualStyleBackColor = True
        '
        'chk_T_PA1
        '
        Me.chk_T_PA1.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_T_PA1.Image = CType(resources.GetObject("chk_T_PA1.Image"), System.Drawing.Image)
        Me.chk_T_PA1.Location = New System.Drawing.Point(12, 29)
        Me.chk_T_PA1.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_T_PA1.Name = "chk_T_PA1"
        Me.chk_T_PA1.Size = New System.Drawing.Size(44, 44)
        Me.chk_T_PA1.TabIndex = 0
        Me.chk_T_PA1.UseVisualStyleBackColor = True
        '
        'lbl_BacConfiguration
        '
        Me.lbl_BacConfiguration.AutoSize = True
        Me.lbl_BacConfiguration.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_BacConfiguration.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_BacConfiguration.Location = New System.Drawing.Point(9, 7)
        Me.lbl_BacConfiguration.Name = "lbl_BacConfiguration"
        Me.lbl_BacConfiguration.Size = New System.Drawing.Size(104, 13)
        Me.lbl_BacConfiguration.TabIndex = 78
        Me.lbl_BacConfiguration.Text = "lbl_BacConfiguration"
        Me.lbl_BacConfiguration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_Bac
        '
        Me.img_Bac.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Bac.Location = New System.Drawing.Point(3, 31)
        Me.img_Bac.Name = "img_Bac"
        Me.img_Bac.Size = New System.Drawing.Size(288, 104)
        Me.img_Bac.TabIndex = 80
        Me.img_Bac.TabStop = False
        '
        'btn_ModifierBac
        '
        Me.btn_ModifierBac.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_ModifierBac.Location = New System.Drawing.Point(3, 137)
        Me.btn_ModifierBac.Name = "btn_ModifierBac"
        Me.btn_ModifierBac.Size = New System.Drawing.Size(290, 27)
        Me.btn_ModifierBac.TabIndex = 74
        Me.btn_ModifierBac.Text = "btn_ModifierBac"
        Me.btn_ModifierBac.UseVisualStyleBackColor = True
        '
        'lbl_BacNom
        '
        Me.lbl_BacNom.AutoSize = True
        Me.lbl_BacNom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_BacNom.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_BacNom.Location = New System.Drawing.Point(12, 12)
        Me.lbl_BacNom.Name = "lbl_BacNom"
        Me.lbl_BacNom.Size = New System.Drawing.Size(64, 13)
        Me.lbl_BacNom.TabIndex = 73
        Me.lbl_BacNom.Text = "lbl_BacNom"
        Me.lbl_BacNom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_BacNom
        '
        Me.txt_BacNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_BacNom.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txt_BacNom.Location = New System.Drawing.Point(89, 9)
        Me.txt_BacNom.Name = "txt_BacNom"
        Me.txt_BacNom.Size = New System.Drawing.Size(204, 20)
        Me.txt_BacNom.TabIndex = 72
        Me.txt_BacNom.Text = "BAC"
        Me.txt_BacNom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_Bac
        '
        Me.lbl_Bac.AutoSize = True
        Me.lbl_Bac.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Bac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Bac.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Bac.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Bac.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Bac.Name = "lbl_Bac"
        Me.lbl_Bac.Size = New System.Drawing.Size(296, 30)
        Me.lbl_Bac.TabIndex = 2
        Me.lbl_Bac.Text = "lbl_Bac"
        Me.lbl_Bac.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_DecalageCofra220
        '
        Me.pan_DecalageCofra220.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_DecalageCofra220.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_DecalageCofra220.Controls.Add(Me.txt_DecalCofra220)
        Me.pan_DecalageCofra220.Controls.Add(Me.img_Decal)
        Me.pan_DecalageCofra220.Controls.Add(Me.etq_UnitDim1)
        Me.pan_DecalageCofra220.Controls.Add(Me.lbl_Decalage)
        Me.pan_DecalageCofra220.Location = New System.Drawing.Point(547, 145)
        Me.pan_DecalageCofra220.Name = "pan_DecalageCofra220"
        Me.pan_DecalageCofra220.Size = New System.Drawing.Size(290, 66)
        Me.pan_DecalageCofra220.TabIndex = 87
        '
        'txt_DecalCofra220
        '
        Me.txt_DecalCofra220.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_DecalCofra220.Location = New System.Drawing.Point(182, 27)
        Me.txt_DecalCofra220.Name = "txt_DecalCofra220"
        Me.txt_DecalCofra220.Size = New System.Drawing.Size(58, 20)
        Me.txt_DecalCofra220.TabIndex = 78
        Me.txt_DecalCofra220.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'img_Decal
        '
        Me.img_Decal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Decal.Location = New System.Drawing.Point(137, 27)
        Me.img_Decal.Name = "img_Decal"
        Me.img_Decal.Size = New System.Drawing.Size(46, 20)
        Me.img_Decal.TabIndex = 79
        Me.img_Decal.TabStop = False
        '
        'etq_UnitDim1
        '
        Me.etq_UnitDim1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim1.AutoSize = True
        Me.etq_UnitDim1.Location = New System.Drawing.Point(246, 30)
        Me.etq_UnitDim1.Name = "etq_UnitDim1"
        Me.etq_UnitDim1.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim1.TabIndex = 77
        Me.etq_UnitDim1.Text = "mm"
        '
        'lbl_Decalage
        '
        Me.lbl_Decalage.AutoSize = True
        Me.lbl_Decalage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Decalage.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Decalage.Location = New System.Drawing.Point(9, 7)
        Me.lbl_Decalage.Name = "lbl_Decalage"
        Me.lbl_Decalage.Size = New System.Drawing.Size(69, 13)
        Me.lbl_Decalage.TabIndex = 76
        Me.lbl_Decalage.Text = "lbl_Decalage"
        Me.lbl_Decalage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Frm_DalleNBac
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1036, 500)
        Me.Controls.Add(Me.pan_DecalageCofra220)
        Me.Controls.Add(Me.Pan_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Frm_DalleNBac"
        Me.Text = "Frm_DalleNBac"
        Me.Pan_Main.ResumeLayout(False)
        Me.TLpan_Milieu.ResumeLayout(False)
        Me.TLpan_Milieu.PerformLayout()
        Me.pan_Bac.ResumeLayout(False)
        Me.pan_Bac.PerformLayout()
        Me.pan_DispoConnecteur.ResumeLayout(False)
        Me.pan_DispoConnecteur.PerformLayout()
        Me.pan_Orientation.ResumeLayout(False)
        Me.pan_Orientation.PerformLayout()
        Me.pan_ConfigurationNervures.ResumeLayout(False)
        Me.pan_ConfigurationNervures.PerformLayout()
        CType(Me.img_Bac, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_DecalageCofra220.ResumeLayout(False)
        Me.pan_DecalageCofra220.PerformLayout()
        CType(Me.img_Decal, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Pan_Main As Panel
    Friend WithEvents TLpan_Milieu As TableLayoutPanel
    Friend WithEvents pan_Bac As Panel
    Friend WithEvents pan_DispoConnecteur As Panel
    Friend WithEvents rdb_Preperce As RadioButton
    Friend WithEvents rdb_ATraversBac As RadioButton
    Friend WithEvents lbl_ConnectorThroughTheWeb As Label
    Friend WithEvents pan_Orientation As Panel
    Friend WithEvents lbl_BacOrientation As Label
    Friend WithEvents rdb_BacPerpendiculaire As RadioButton
    Friend WithEvents rdb_BacParallele As RadioButton
    Friend WithEvents pan_ConfigurationNervures As Panel
    Friend WithEvents chk_L_PA1 As CheckBox
    Friend WithEvents chk_L_PA2 As CheckBox
    Friend WithEvents rtxt_Configuration As RichTextBox
    Friend WithEvents chk_T_PA3 As CheckBox
    Friend WithEvents chk_T_PA2 As CheckBox
    Friend WithEvents chk_T_PA1 As CheckBox
    Friend WithEvents lbl_BacConfiguration As Label
    Friend WithEvents img_Bac As PictureBox
    Friend WithEvents btn_ModifierBac As Button
    Friend WithEvents lbl_BacNom As Label
    Friend WithEvents txt_BacNom As TextBox
    Friend WithEvents lbl_Bac As Label
    Friend WithEvents pan_DecalageCofra220 As Panel
    Friend WithEvents lbl_Decalage As Label
    Friend WithEvents txt_DecalCofra220 As TextBox
    Friend WithEvents img_Decal As PictureBox
    Friend WithEvents etq_UnitDim1 As Label
    Friend WithEvents ErrorProvider As ErrorProvider
End Class
