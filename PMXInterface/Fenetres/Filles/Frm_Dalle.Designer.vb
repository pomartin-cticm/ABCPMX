<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Dalle
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
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Portees = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Armatures = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txt_Hd = New System.Windows.Forms.TextBox()
        Me.etq_Epaisseur = New System.Windows.Forms.Label()
        Me.txt_Hh = New System.Windows.Forms.TextBox()
        Me.etq_UnitDim2 = New System.Windows.Forms.Label()
        Me.etq_Renformis = New System.Windows.Forms.Label()
        Me.Img_Hd = New System.Windows.Forms.PictureBox()
        Me.Img_Hh = New System.Windows.Forms.PictureBox()
        Me.etq_UnitDim3 = New System.Windows.Forms.Label()
        Me.etq_TypeDalle = New System.Windows.Forms.Label()
        Me.cmb_TypeDalle = New System.Windows.Forms.ComboBox()
        Me.pan_Beton = New System.Windows.Forms.Panel()
        Me.txt_Ecm = New System.Windows.Forms.TextBox()
        Me.img_Ecm = New System.Windows.Forms.PictureBox()
        Me.etq_UnitModule1 = New System.Windows.Forms.Label()
        Me.txt_Fck = New System.Windows.Forms.TextBox()
        Me.img_Fck = New System.Windows.Forms.PictureBox()
        Me.etq_UnitSigma1 = New System.Windows.Forms.Label()
        Me.cmb_ClasseBetonEnrobage = New System.Windows.Forms.ComboBox()
        Me.lbl_ClasseE = New System.Windows.Forms.Label()
        Me.lbl_Beton = New System.Windows.Forms.Label()
        Me.lbl_General = New System.Windows.Forms.Label()
        Me.pan_Armatures = New System.Windows.Forms.Panel()
        Me.img_Dalle = New System.Windows.Forms.PictureBox()
        Me.pan_Droite = New System.Windows.Forms.Panel()
        Me.TLpan_Droite = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Bac = New System.Windows.Forms.Panel()
        Me.rdb_BacCustom = New System.Windows.Forms.RadioButton()
        Me.lbl_Producteur = New System.Windows.Forms.Label()
        Me.cmb_Producteur = New System.Windows.Forms.ComboBox()
        Me.rdb_BacBase = New System.Windows.Forms.RadioButton()
        Me.lbl_Bac = New System.Windows.Forms.Label()
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.Grid_Bac = New System.Windows.Forms.DataGridView()
        Me.img_Bac = New System.Windows.Forms.PictureBox()
        Me.Col_ListeSup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Portees.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.Img_Hd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Img_Hh, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Beton.SuspendLayout()
        CType(Me.img_Ecm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Fck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Dalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Droite.SuspendLayout()
        Me.TLpan_Droite.SuspendLayout()
        Me.pan_Bac.SuspendLayout()
        Me.pan_General.SuspendLayout()
        CType(Me.Grid_Bac, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Bac, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.TLpan_Main.Size = New System.Drawing.Size(879, 529)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 492)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(873, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(449, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(309, 3)
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
        Me.pan_Main.Size = New System.Drawing.Size(873, 483)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Portees
        '
        Me.TLPan_Portees.ColumnCount = 3
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 501.0!))
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Portees.Controls.Add(Me.img_Dalle, 2, 0)
        Me.TLPan_Portees.Controls.Add(Me.pan_Droite, 1, 0)
        Me.TLPan_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Portees.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Portees.Name = "TLPan_Portees"
        Me.TLPan_Portees.RowCount = 2
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Portees.Size = New System.Drawing.Size(873, 483)
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
        Me.pan_Gauche.Size = New System.Drawing.Size(250, 463)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Armatures, 0, 4)
        Me.TLPan_Gauche.Controls.Add(Me.Panel1, 0, 1)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Beton, 0, 3)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Beton, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_General, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Armatures, 0, 5)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 8
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(250, 463)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Armatures
        '
        Me.lbl_Armatures.AutoSize = True
        Me.lbl_Armatures.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Armatures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Armatures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Armatures.Location = New System.Drawing.Point(0, 240)
        Me.lbl_Armatures.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Armatures.Name = "lbl_Armatures"
        Me.lbl_Armatures.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Armatures.TabIndex = 4
        Me.lbl_Armatures.Text = "lbl_Armatures"
        Me.lbl_Armatures.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txt_Hd)
        Me.Panel1.Controls.Add(Me.etq_Epaisseur)
        Me.Panel1.Controls.Add(Me.txt_Hh)
        Me.Panel1.Controls.Add(Me.etq_UnitDim2)
        Me.Panel1.Controls.Add(Me.etq_Renformis)
        Me.Panel1.Controls.Add(Me.Img_Hd)
        Me.Panel1.Controls.Add(Me.Img_Hh)
        Me.Panel1.Controls.Add(Me.etq_UnitDim3)
        Me.Panel1.Controls.Add(Me.etq_TypeDalle)
        Me.Panel1.Controls.Add(Me.cmb_TypeDalle)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 30)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(250, 89)
        Me.Panel1.TabIndex = 5
        '
        'txt_Hd
        '
        Me.txt_Hd.Location = New System.Drawing.Point(152, 34)
        Me.txt_Hd.Name = "txt_Hd"
        Me.txt_Hd.Size = New System.Drawing.Size(58, 20)
        Me.txt_Hd.TabIndex = 71
        '
        'etq_Epaisseur
        '
        Me.etq_Epaisseur.AutoSize = True
        Me.etq_Epaisseur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.etq_Epaisseur.ForeColor = System.Drawing.Color.DarkRed
        Me.etq_Epaisseur.Location = New System.Drawing.Point(13, 37)
        Me.etq_Epaisseur.Name = "etq_Epaisseur"
        Me.etq_Epaisseur.Size = New System.Drawing.Size(74, 13)
        Me.etq_Epaisseur.TabIndex = 69
        Me.etq_Epaisseur.Text = "etq_Epaisseur"
        Me.etq_Epaisseur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_Hh
        '
        Me.txt_Hh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Hh.Location = New System.Drawing.Point(152, 57)
        Me.txt_Hh.Name = "txt_Hh"
        Me.txt_Hh.Size = New System.Drawing.Size(58, 20)
        Me.txt_Hh.TabIndex = 75
        '
        'etq_UnitDim2
        '
        Me.etq_UnitDim2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim2.AutoSize = True
        Me.etq_UnitDim2.Location = New System.Drawing.Point(216, 37)
        Me.etq_UnitDim2.Name = "etq_UnitDim2"
        Me.etq_UnitDim2.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim2.TabIndex = 70
        Me.etq_UnitDim2.Text = "mm"
        '
        'etq_Renformis
        '
        Me.etq_Renformis.AutoSize = True
        Me.etq_Renformis.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.etq_Renformis.ForeColor = System.Drawing.Color.DarkRed
        Me.etq_Renformis.Location = New System.Drawing.Point(13, 60)
        Me.etq_Renformis.Name = "etq_Renformis"
        Me.etq_Renformis.Size = New System.Drawing.Size(75, 13)
        Me.etq_Renformis.TabIndex = 73
        Me.etq_Renformis.Text = "etq_Renformis"
        Me.etq_Renformis.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Img_Hd
        '
        Me.Img_Hd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Img_Hd.Location = New System.Drawing.Point(116, 34)
        Me.Img_Hd.Name = "Img_Hd"
        Me.Img_Hd.Size = New System.Drawing.Size(37, 20)
        Me.Img_Hd.TabIndex = 72
        Me.Img_Hd.TabStop = False
        '
        'Img_Hh
        '
        Me.Img_Hh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Img_Hh.Location = New System.Drawing.Point(116, 57)
        Me.Img_Hh.Name = "Img_Hh"
        Me.Img_Hh.Size = New System.Drawing.Size(37, 20)
        Me.Img_Hh.TabIndex = 76
        Me.Img_Hh.TabStop = False
        '
        'etq_UnitDim3
        '
        Me.etq_UnitDim3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim3.AutoSize = True
        Me.etq_UnitDim3.Location = New System.Drawing.Point(216, 60)
        Me.etq_UnitDim3.Name = "etq_UnitDim3"
        Me.etq_UnitDim3.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim3.TabIndex = 74
        Me.etq_UnitDim3.Text = "mm"
        '
        'etq_TypeDalle
        '
        Me.etq_TypeDalle.AutoSize = True
        Me.etq_TypeDalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.etq_TypeDalle.ForeColor = System.Drawing.Color.DarkRed
        Me.etq_TypeDalle.Location = New System.Drawing.Point(13, 11)
        Me.etq_TypeDalle.Name = "etq_TypeDalle"
        Me.etq_TypeDalle.Size = New System.Drawing.Size(76, 13)
        Me.etq_TypeDalle.TabIndex = 55
        Me.etq_TypeDalle.Text = "etq_TypeDalle"
        Me.etq_TypeDalle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmb_TypeDalle
        '
        Me.cmb_TypeDalle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_TypeDalle.FormattingEnabled = True
        Me.cmb_TypeDalle.Location = New System.Drawing.Point(96, 8)
        Me.cmb_TypeDalle.Name = "cmb_TypeDalle"
        Me.cmb_TypeDalle.Size = New System.Drawing.Size(143, 21)
        Me.cmb_TypeDalle.TabIndex = 56
        '
        'pan_Beton
        '
        Me.pan_Beton.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Beton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Beton.Controls.Add(Me.txt_Ecm)
        Me.pan_Beton.Controls.Add(Me.img_Ecm)
        Me.pan_Beton.Controls.Add(Me.etq_UnitModule1)
        Me.pan_Beton.Controls.Add(Me.txt_Fck)
        Me.pan_Beton.Controls.Add(Me.img_Fck)
        Me.pan_Beton.Controls.Add(Me.etq_UnitSigma1)
        Me.pan_Beton.Controls.Add(Me.cmb_ClasseBetonEnrobage)
        Me.pan_Beton.Controls.Add(Me.lbl_ClasseE)
        Me.pan_Beton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Beton.Location = New System.Drawing.Point(0, 150)
        Me.pan_Beton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Beton.Name = "pan_Beton"
        Me.pan_Beton.Size = New System.Drawing.Size(250, 89)
        Me.pan_Beton.TabIndex = 4
        '
        'txt_Ecm
        '
        Me.txt_Ecm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Ecm.Location = New System.Drawing.Point(127, 58)
        Me.txt_Ecm.Name = "txt_Ecm"
        Me.txt_Ecm.Size = New System.Drawing.Size(58, 20)
        Me.txt_Ecm.TabIndex = 62
        '
        'img_Ecm
        '
        Me.img_Ecm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Ecm.Location = New System.Drawing.Point(82, 58)
        Me.img_Ecm.Name = "img_Ecm"
        Me.img_Ecm.Size = New System.Drawing.Size(46, 20)
        Me.img_Ecm.TabIndex = 63
        Me.img_Ecm.TabStop = False
        '
        'etq_UnitModule1
        '
        Me.etq_UnitModule1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitModule1.AutoSize = True
        Me.etq_UnitModule1.Location = New System.Drawing.Point(191, 61)
        Me.etq_UnitModule1.Name = "etq_UnitModule1"
        Me.etq_UnitModule1.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitModule1.TabIndex = 61
        Me.etq_UnitModule1.Text = "mm"
        '
        'txt_Fck
        '
        Me.txt_Fck.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Fck.Location = New System.Drawing.Point(127, 34)
        Me.txt_Fck.Name = "txt_Fck"
        Me.txt_Fck.Size = New System.Drawing.Size(58, 20)
        Me.txt_Fck.TabIndex = 59
        '
        'img_Fck
        '
        Me.img_Fck.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Fck.Location = New System.Drawing.Point(82, 34)
        Me.img_Fck.Name = "img_Fck"
        Me.img_Fck.Size = New System.Drawing.Size(46, 20)
        Me.img_Fck.TabIndex = 60
        Me.img_Fck.TabStop = False
        '
        'etq_UnitSigma1
        '
        Me.etq_UnitSigma1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitSigma1.AutoSize = True
        Me.etq_UnitSigma1.Location = New System.Drawing.Point(191, 37)
        Me.etq_UnitSigma1.Name = "etq_UnitSigma1"
        Me.etq_UnitSigma1.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitSigma1.TabIndex = 58
        Me.etq_UnitSigma1.Text = "mm"
        '
        'cmb_ClasseBetonEnrobage
        '
        Me.cmb_ClasseBetonEnrobage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_ClasseBetonEnrobage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_ClasseBetonEnrobage.FormattingEnabled = True
        Me.cmb_ClasseBetonEnrobage.Location = New System.Drawing.Point(127, 6)
        Me.cmb_ClasseBetonEnrobage.Name = "cmb_ClasseBetonEnrobage"
        Me.cmb_ClasseBetonEnrobage.Size = New System.Drawing.Size(111, 21)
        Me.cmb_ClasseBetonEnrobage.TabIndex = 57
        '
        'lbl_ClasseE
        '
        Me.lbl_ClasseE.AutoSize = True
        Me.lbl_ClasseE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ClasseE.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_ClasseE.Location = New System.Drawing.Point(11, 9)
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
        Me.lbl_Beton.Location = New System.Drawing.Point(0, 120)
        Me.lbl_Beton.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Beton.Name = "lbl_Beton"
        Me.lbl_Beton.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Beton.TabIndex = 3
        Me.lbl_Beton.Text = "lbl_Beton"
        Me.lbl_Beton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.lbl_General.Size = New System.Drawing.Size(250, 30)
        Me.lbl_General.TabIndex = 0
        Me.lbl_General.Text = "lbl_General"
        Me.lbl_General.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Armatures
        '
        Me.pan_Armatures.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Armatures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Armatures.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Armatures.Location = New System.Drawing.Point(0, 270)
        Me.pan_Armatures.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Armatures.Name = "pan_Armatures"
        Me.pan_Armatures.Size = New System.Drawing.Size(250, 70)
        Me.pan_Armatures.TabIndex = 6
        '
        'img_Dalle
        '
        Me.img_Dalle.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Dalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Dalle.Location = New System.Drawing.Point(752, 0)
        Me.img_Dalle.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.img_Dalle.Name = "img_Dalle"
        Me.img_Dalle.Size = New System.Drawing.Size(100, 50)
        Me.img_Dalle.TabIndex = 1
        Me.img_Dalle.TabStop = False
        '
        'pan_Droite
        '
        Me.pan_Droite.Controls.Add(Me.TLpan_Droite)
        Me.pan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Droite.Location = New System.Drawing.Point(251, 0)
        Me.pan_Droite.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Droite.Name = "pan_Droite"
        Me.pan_Droite.Size = New System.Drawing.Size(500, 463)
        Me.pan_Droite.TabIndex = 2
        '
        'TLpan_Droite
        '
        Me.TLpan_Droite.ColumnCount = 1
        Me.TLpan_Droite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Droite.Controls.Add(Me.pan_Bac, 0, 1)
        Me.TLpan_Droite.Controls.Add(Me.lbl_Bac, 0, 0)
        Me.TLpan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Droite.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Droite.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Droite.Name = "TLpan_Droite"
        Me.TLpan_Droite.RowCount = 2
        Me.TLpan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Droite.Size = New System.Drawing.Size(500, 463)
        Me.TLpan_Droite.TabIndex = 0
        '
        'pan_Bac
        '
        Me.pan_Bac.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Bac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Bac.Controls.Add(Me.img_Bac)
        Me.pan_Bac.Controls.Add(Me.Grid_Bac)
        Me.pan_Bac.Controls.Add(Me.rdb_BacCustom)
        Me.pan_Bac.Controls.Add(Me.lbl_Producteur)
        Me.pan_Bac.Controls.Add(Me.cmb_Producteur)
        Me.pan_Bac.Controls.Add(Me.rdb_BacBase)
        Me.pan_Bac.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Bac.Location = New System.Drawing.Point(0, 30)
        Me.pan_Bac.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Bac.Name = "pan_Bac"
        Me.pan_Bac.Size = New System.Drawing.Size(500, 433)
        Me.pan_Bac.TabIndex = 7
        '
        'rdb_BacCustom
        '
        Me.rdb_BacCustom.AutoSize = True
        Me.rdb_BacCustom.Location = New System.Drawing.Point(262, 12)
        Me.rdb_BacCustom.Name = "rdb_BacCustom"
        Me.rdb_BacCustom.Size = New System.Drawing.Size(100, 17)
        Me.rdb_BacCustom.TabIndex = 59
        Me.rdb_BacCustom.TabStop = True
        Me.rdb_BacCustom.Text = "rdb_BacCustom"
        Me.rdb_BacCustom.UseVisualStyleBackColor = True
        '
        'lbl_Producteur
        '
        Me.lbl_Producteur.AutoSize = True
        Me.lbl_Producteur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Producteur.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Producteur.Location = New System.Drawing.Point(13, 40)
        Me.lbl_Producteur.Name = "lbl_Producteur"
        Me.lbl_Producteur.Size = New System.Drawing.Size(39, 13)
        Me.lbl_Producteur.TabIndex = 57
        Me.lbl_Producteur.Text = "Label1"
        Me.lbl_Producteur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmb_Producteur
        '
        Me.cmb_Producteur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Producteur.FormattingEnabled = True
        Me.cmb_Producteur.Location = New System.Drawing.Point(96, 37)
        Me.cmb_Producteur.Name = "cmb_Producteur"
        Me.cmb_Producteur.Size = New System.Drawing.Size(143, 21)
        Me.cmb_Producteur.TabIndex = 58
        '
        'rdb_BacBase
        '
        Me.rdb_BacBase.AutoSize = True
        Me.rdb_BacBase.Location = New System.Drawing.Point(18, 12)
        Me.rdb_BacBase.Name = "rdb_BacBase"
        Me.rdb_BacBase.Size = New System.Drawing.Size(89, 17)
        Me.rdb_BacBase.TabIndex = 0
        Me.rdb_BacBase.TabStop = True
        Me.rdb_BacBase.Text = "rdb_BacBase"
        Me.rdb_BacBase.UseVisualStyleBackColor = True
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
        Me.lbl_Bac.Size = New System.Drawing.Size(500, 30)
        Me.lbl_Bac.TabIndex = 1
        Me.lbl_Bac.Text = "lbl_Bac"
        Me.lbl_Bac.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(879, 529)
        Me.pan_General.TabIndex = 3
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
        Me.Grid_Bac.Location = New System.Drawing.Point(57, 64)
        Me.Grid_Bac.MultiSelect = False
        Me.Grid_Bac.Name = "Grid_Bac"
        Me.Grid_Bac.RowHeadersVisible = False
        Me.Grid_Bac.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Grid_Bac.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid_Bac.ShowCellToolTips = False
        Me.Grid_Bac.Size = New System.Drawing.Size(182, 160)
        Me.Grid_Bac.TabIndex = 60
        '
        'img_Bac
        '
        Me.img_Bac.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Bac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Bac.Location = New System.Drawing.Point(8, 227)
        Me.img_Bac.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.img_Bac.Name = "img_Bac"
        Me.img_Bac.Size = New System.Drawing.Size(480, 82)
        Me.img_Bac.TabIndex = 3
        Me.img_Bac.TabStop = False
        '
        'Col_ListeSup
        '
        Me.Col_ListeSup.HeaderText = "Col_Liste"
        Me.Col_ListeSup.Name = "Col_ListeSup"
        Me.Col_ListeSup.ReadOnly = True
        '
        'Frm_Dalle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(879, 529)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_Dalle"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Dalle"
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Portees.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Img_Hd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Img_Hh, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Beton.ResumeLayout(False)
        Me.pan_Beton.PerformLayout()
        CType(Me.img_Ecm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Fck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Dalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Droite.ResumeLayout(False)
        Me.TLpan_Droite.ResumeLayout(False)
        Me.TLpan_Droite.PerformLayout()
        Me.pan_Bac.ResumeLayout(False)
        Me.pan_Bac.PerformLayout()
        Me.pan_General.ResumeLayout(False)
        CType(Me.Grid_Bac, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Bac, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Portees As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_General As Label
    Friend WithEvents img_Dalle As PictureBox
    Friend WithEvents pan_Droite As Panel
    Friend WithEvents TLpan_Droite As TableLayoutPanel
    Friend WithEvents lbl_Bac As Label
    Friend WithEvents pan_General As Panel
    Friend WithEvents lbl_Beton As Label
    Friend WithEvents lbl_Armatures As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txt_Hd As TextBox
    Friend WithEvents etq_Epaisseur As Label
    Friend WithEvents Img_Hd As PictureBox
    Friend WithEvents etq_UnitDim2 As Label
    Friend WithEvents txt_Hh As TextBox
    Friend WithEvents etq_Renformis As Label
    Friend WithEvents Img_Hh As PictureBox
    Friend WithEvents etq_UnitDim3 As Label
    Friend WithEvents etq_TypeDalle As Label
    Friend WithEvents cmb_TypeDalle As ComboBox
    Friend WithEvents pan_Beton As Panel
    Friend WithEvents txt_Ecm As TextBox
    Friend WithEvents img_Ecm As PictureBox
    Friend WithEvents etq_UnitModule1 As Label
    Friend WithEvents txt_Fck As TextBox
    Friend WithEvents img_Fck As PictureBox
    Friend WithEvents etq_UnitSigma1 As Label
    Friend WithEvents cmb_ClasseBetonEnrobage As ComboBox
    Friend WithEvents lbl_ClasseE As Label
    Friend WithEvents pan_Armatures As Panel
    Friend WithEvents pan_Bac As Panel
    Friend WithEvents rdb_BacCustom As RadioButton
    Friend WithEvents lbl_Producteur As Label
    Friend WithEvents cmb_Producteur As ComboBox
    Friend WithEvents rdb_BacBase As RadioButton
    Friend WithEvents Grid_Bac As DataGridView
    Friend WithEvents img_Bac As PictureBox
    Friend WithEvents Col_ListeSup As DataGridViewTextBoxColumn
End Class
