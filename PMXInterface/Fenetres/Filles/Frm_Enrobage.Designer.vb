<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Enrobage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Enrobage))
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Portees = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.Pan_ArmaLongi = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_ArmaLongi = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chk_LitSup = New System.Windows.Forms.CheckBox()
        Me.chk_LitInter = New System.Windows.Forms.CheckBox()
        Me.chk_LitInf = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.cmb_DiametreC = New System.Windows.Forms.ComboBox()
        Me.cmb_Diametre = New System.Windows.Forms.ComboBox()
        Me.img_PhiAC = New System.Windows.Forms.PictureBox()
        Me.etq_UnitDim6 = New System.Windows.Forms.Label()
        Me.chk_Constructif = New System.Windows.Forms.CheckBox()
        Me.lbl_Nombre = New System.Windows.Forms.Label()
        Me.cmb_Nombre = New System.Windows.Forms.ComboBox()
        Me.lbl_DiametreA = New System.Windows.Forms.Label()
        Me.img_PhiA = New System.Windows.Forms.PictureBox()
        Me.etq_UnitDim5 = New System.Windows.Forms.Label()
        Me.lbl_LitSelectionne = New System.Windows.Forms.Label()
        Me.lbl_ArmaLongi = New System.Windows.Forms.Label()
        Me.Pan_Etriers = New System.Windows.Forms.Panel()
        Me.cmb_DiametreEtriers = New System.Windows.Forms.ComboBox()
        Me.cmb_TypeEtriers = New System.Windows.Forms.ComboBox()
        Me.lbl_Type = New System.Windows.Forms.Label()
        Me.txt_EtrierUz = New System.Windows.Forms.TextBox()
        Me.txt_EtrierUy = New System.Windows.Forms.TextBox()
        Me.lbl_EnrobageEtrier = New System.Windows.Forms.Label()
        Me.lbl_DiametreE = New System.Windows.Forms.Label()
        Me.img_uz = New System.Windows.Forms.PictureBox()
        Me.img_ux = New System.Windows.Forms.PictureBox()
        Me.img_PhiEtrier = New System.Windows.Forms.PictureBox()
        Me.etq_UnitDim4 = New System.Windows.Forms.Label()
        Me.etq_UnitDim3 = New System.Windows.Forms.Label()
        Me.etq_UnitDim2 = New System.Windows.Forms.Label()
        Me.lbl_Etriers = New System.Windows.Forms.Label()
        Me.Pan_Dimensions = New System.Windows.Forms.Panel()
        Me.img_Bc2 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_Bc = New System.Windows.Forms.TextBox()
        Me.chk_PcBc = New System.Windows.Forms.CheckBox()
        Me.txt_pcBc = New System.Windows.Forms.TextBox()
        Me.chk_Bc = New System.Windows.Forms.CheckBox()
        Me.img_Bc = New System.Windows.Forms.PictureBox()
        Me.etq_UnitDim1 = New System.Windows.Forms.Label()
        Me.lbl_Largeur = New System.Windows.Forms.Label()
        Me.lbl_Dimensions = New System.Windows.Forms.Label()
        Me.img_Enrobage = New System.Windows.Forms.PictureBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Portees.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.Pan_ArmaLongi.SuspendLayout()
        Me.TableLayoutPanel_ArmaLongi.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.img_PhiAC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_PhiA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Pan_Etriers.SuspendLayout()
        CType(Me.img_uz, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ux, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_PhiEtrier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Pan_Dimensions.SuspendLayout()
        CType(Me.img_Bc2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Bc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Enrobage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(1099, 511)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(1099, 511)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 474)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1093, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(559, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(419, 3)
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
        Me.pan_Main.Size = New System.Drawing.Size(1093, 465)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Portees
        '
        Me.TLPan_Portees.ColumnCount = 2
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Portees.Controls.Add(Me.img_Enrobage, 1, 0)
        Me.TLPan_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Portees.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Portees.Name = "TLPan_Portees"
        Me.TLPan_Portees.RowCount = 1
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Size = New System.Drawing.Size(1093, 465)
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
        Me.pan_Gauche.Size = New System.Drawing.Size(250, 465)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.Pan_ArmaLongi, 0, 5)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_ArmaLongi, 0, 4)
        Me.TLPan_Gauche.Controls.Add(Me.Pan_Etriers, 0, 3)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Etriers, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.Pan_Dimensions, 0, 1)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Dimensions, 0, 0)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 7
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(250, 465)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'Pan_ArmaLongi
        '
        Me.Pan_ArmaLongi.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Pan_ArmaLongi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pan_ArmaLongi.Controls.Add(Me.TableLayoutPanel_ArmaLongi)
        Me.Pan_ArmaLongi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_ArmaLongi.Location = New System.Drawing.Point(0, 265)
        Me.Pan_ArmaLongi.Margin = New System.Windows.Forms.Padding(0)
        Me.Pan_ArmaLongi.Name = "Pan_ArmaLongi"
        Me.Pan_ArmaLongi.Size = New System.Drawing.Size(250, 200)
        Me.Pan_ArmaLongi.TabIndex = 13
        '
        'TableLayoutPanel_ArmaLongi
        '
        Me.TableLayoutPanel_ArmaLongi.ColumnCount = 1
        Me.TableLayoutPanel_ArmaLongi.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_ArmaLongi.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel_ArmaLongi.Controls.Add(Me.Panel3, 0, 1)
        Me.TableLayoutPanel_ArmaLongi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_ArmaLongi.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_ArmaLongi.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_ArmaLongi.Name = "TableLayoutPanel_ArmaLongi"
        Me.TableLayoutPanel_ArmaLongi.RowCount = 2
        Me.TableLayoutPanel_ArmaLongi.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel_ArmaLongi.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_ArmaLongi.Size = New System.Drawing.Size(248, 198)
        Me.TableLayoutPanel_ArmaLongi.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chk_LitSup)
        Me.Panel2.Controls.Add(Me.chk_LitInter)
        Me.Panel2.Controls.Add(Me.chk_LitInf)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(248, 50)
        Me.Panel2.TabIndex = 0
        '
        'chk_LitSup
        '
        Me.chk_LitSup.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_LitSup.Image = CType(resources.GetObject("chk_LitSup.Image"), System.Drawing.Image)
        Me.chk_LitSup.Location = New System.Drawing.Point(178, 3)
        Me.chk_LitSup.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_LitSup.Name = "chk_LitSup"
        Me.chk_LitSup.Size = New System.Drawing.Size(44, 44)
        Me.chk_LitSup.TabIndex = 2
        Me.chk_LitSup.UseVisualStyleBackColor = True
        '
        'chk_LitInter
        '
        Me.chk_LitInter.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_LitInter.Image = CType(resources.GetObject("chk_LitInter.Image"), System.Drawing.Image)
        Me.chk_LitInter.Location = New System.Drawing.Point(102, 3)
        Me.chk_LitInter.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_LitInter.Name = "chk_LitInter"
        Me.chk_LitInter.Size = New System.Drawing.Size(44, 44)
        Me.chk_LitInter.TabIndex = 1
        Me.chk_LitInter.UseVisualStyleBackColor = True
        '
        'chk_LitInf
        '
        Me.chk_LitInf.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_LitInf.Image = CType(resources.GetObject("chk_LitInf.Image"), System.Drawing.Image)
        Me.chk_LitInf.Location = New System.Drawing.Point(28, 3)
        Me.chk_LitInf.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_LitInf.Name = "chk_LitInf"
        Me.chk_LitInf.Size = New System.Drawing.Size(44, 44)
        Me.chk_LitInf.TabIndex = 0
        Me.chk_LitInf.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.cmb_DiametreC)
        Me.Panel3.Controls.Add(Me.cmb_Diametre)
        Me.Panel3.Controls.Add(Me.img_PhiAC)
        Me.Panel3.Controls.Add(Me.etq_UnitDim6)
        Me.Panel3.Controls.Add(Me.chk_Constructif)
        Me.Panel3.Controls.Add(Me.lbl_Nombre)
        Me.Panel3.Controls.Add(Me.cmb_Nombre)
        Me.Panel3.Controls.Add(Me.lbl_DiametreA)
        Me.Panel3.Controls.Add(Me.img_PhiA)
        Me.Panel3.Controls.Add(Me.etq_UnitDim5)
        Me.Panel3.Controls.Add(Me.lbl_LitSelectionne)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 50)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(248, 148)
        Me.Panel3.TabIndex = 1
        '
        'cmb_DiametreC
        '
        Me.cmb_DiametreC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_DiametreC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_DiametreC.FormattingEnabled = True
        Me.cmb_DiametreC.Location = New System.Drawing.Point(151, 113)
        Me.cmb_DiametreC.Name = "cmb_DiametreC"
        Me.cmb_DiametreC.Size = New System.Drawing.Size(58, 21)
        Me.cmb_DiametreC.TabIndex = 56
        '
        'cmb_Diametre
        '
        Me.cmb_Diametre.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Diametre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Diametre.FormattingEnabled = True
        Me.cmb_Diametre.Location = New System.Drawing.Point(151, 63)
        Me.cmb_Diametre.Name = "cmb_Diametre"
        Me.cmb_Diametre.Size = New System.Drawing.Size(58, 21)
        Me.cmb_Diametre.TabIndex = 55
        '
        'img_PhiAC
        '
        Me.img_PhiAC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PhiAC.Location = New System.Drawing.Point(115, 114)
        Me.img_PhiAC.Name = "img_PhiAC"
        Me.img_PhiAC.Size = New System.Drawing.Size(37, 20)
        Me.img_PhiAC.TabIndex = 54
        Me.img_PhiAC.TabStop = False
        '
        'etq_UnitDim6
        '
        Me.etq_UnitDim6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim6.AutoSize = True
        Me.etq_UnitDim6.Location = New System.Drawing.Point(215, 117)
        Me.etq_UnitDim6.Name = "etq_UnitDim6"
        Me.etq_UnitDim6.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim6.TabIndex = 52
        Me.etq_UnitDim6.Text = "mm"
        '
        'chk_Constructif
        '
        Me.chk_Constructif.AutoSize = True
        Me.chk_Constructif.Location = New System.Drawing.Point(13, 97)
        Me.chk_Constructif.Name = "chk_Constructif"
        Me.chk_Constructif.Size = New System.Drawing.Size(100, 17)
        Me.chk_Constructif.TabIndex = 51
        Me.chk_Constructif.Text = "chk_Constructif"
        Me.chk_Constructif.UseVisualStyleBackColor = True
        '
        'lbl_Nombre
        '
        Me.lbl_Nombre.AutoSize = True
        Me.lbl_Nombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Nombre.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Nombre.Location = New System.Drawing.Point(10, 39)
        Me.lbl_Nombre.Name = "lbl_Nombre"
        Me.lbl_Nombre.Size = New System.Drawing.Size(60, 13)
        Me.lbl_Nombre.TabIndex = 50
        Me.lbl_Nombre.Text = "lbl_Nombre"
        Me.lbl_Nombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmb_Nombre
        '
        Me.cmb_Nombre.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Nombre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Nombre.FormattingEnabled = True
        Me.cmb_Nombre.Location = New System.Drawing.Point(151, 36)
        Me.cmb_Nombre.Name = "cmb_Nombre"
        Me.cmb_Nombre.Size = New System.Drawing.Size(58, 21)
        Me.cmb_Nombre.TabIndex = 49
        '
        'lbl_DiametreA
        '
        Me.lbl_DiametreA.AutoSize = True
        Me.lbl_DiametreA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DiametreA.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_DiametreA.Location = New System.Drawing.Point(10, 67)
        Me.lbl_DiametreA.Name = "lbl_DiametreA"
        Me.lbl_DiametreA.Size = New System.Drawing.Size(65, 13)
        Me.lbl_DiametreA.TabIndex = 48
        Me.lbl_DiametreA.Text = "lbl_Diametre"
        Me.lbl_DiametreA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_PhiA
        '
        Me.img_PhiA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PhiA.Location = New System.Drawing.Point(115, 64)
        Me.img_PhiA.Name = "img_PhiA"
        Me.img_PhiA.Size = New System.Drawing.Size(37, 20)
        Me.img_PhiA.TabIndex = 47
        Me.img_PhiA.TabStop = False
        '
        'etq_UnitDim5
        '
        Me.etq_UnitDim5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim5.AutoSize = True
        Me.etq_UnitDim5.Location = New System.Drawing.Point(215, 67)
        Me.etq_UnitDim5.Name = "etq_UnitDim5"
        Me.etq_UnitDim5.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim5.TabIndex = 45
        Me.etq_UnitDim5.Text = "mm"
        '
        'lbl_LitSelectionne
        '
        Me.lbl_LitSelectionne.Location = New System.Drawing.Point(3, 6)
        Me.lbl_LitSelectionne.Name = "lbl_LitSelectionne"
        Me.lbl_LitSelectionne.Size = New System.Drawing.Size(242, 19)
        Me.lbl_LitSelectionne.TabIndex = 0
        Me.lbl_LitSelectionne.Text = "lbl_LitSelectionne"
        Me.lbl_LitSelectionne.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_ArmaLongi
        '
        Me.lbl_ArmaLongi.BackColor = System.Drawing.SystemColors.ControlDark
        Me.lbl_ArmaLongi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ArmaLongi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ArmaLongi.Location = New System.Drawing.Point(0, 235)
        Me.lbl_ArmaLongi.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ArmaLongi.Name = "lbl_ArmaLongi"
        Me.lbl_ArmaLongi.Size = New System.Drawing.Size(250, 30)
        Me.lbl_ArmaLongi.TabIndex = 12
        Me.lbl_ArmaLongi.Text = "lbl_ArmaLongi"
        Me.lbl_ArmaLongi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pan_Etriers
        '
        Me.Pan_Etriers.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Pan_Etriers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pan_Etriers.Controls.Add(Me.cmb_DiametreEtriers)
        Me.Pan_Etriers.Controls.Add(Me.cmb_TypeEtriers)
        Me.Pan_Etriers.Controls.Add(Me.lbl_Type)
        Me.Pan_Etriers.Controls.Add(Me.txt_EtrierUz)
        Me.Pan_Etriers.Controls.Add(Me.txt_EtrierUy)
        Me.Pan_Etriers.Controls.Add(Me.lbl_EnrobageEtrier)
        Me.Pan_Etriers.Controls.Add(Me.lbl_DiametreE)
        Me.Pan_Etriers.Controls.Add(Me.img_uz)
        Me.Pan_Etriers.Controls.Add(Me.img_ux)
        Me.Pan_Etriers.Controls.Add(Me.img_PhiEtrier)
        Me.Pan_Etriers.Controls.Add(Me.etq_UnitDim4)
        Me.Pan_Etriers.Controls.Add(Me.etq_UnitDim3)
        Me.Pan_Etriers.Controls.Add(Me.etq_UnitDim2)
        Me.Pan_Etriers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_Etriers.Location = New System.Drawing.Point(0, 115)
        Me.Pan_Etriers.Margin = New System.Windows.Forms.Padding(0)
        Me.Pan_Etriers.Name = "Pan_Etriers"
        Me.Pan_Etriers.Size = New System.Drawing.Size(250, 120)
        Me.Pan_Etriers.TabIndex = 11
        '
        'cmb_DiametreEtriers
        '
        Me.cmb_DiametreEtriers.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_DiametreEtriers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_DiametreEtriers.FormattingEnabled = True
        Me.cmb_DiametreEtriers.Location = New System.Drawing.Point(144, 40)
        Me.cmb_DiametreEtriers.Name = "cmb_DiametreEtriers"
        Me.cmb_DiametreEtriers.Size = New System.Drawing.Size(58, 21)
        Me.cmb_DiametreEtriers.TabIndex = 51
        '
        'cmb_TypeEtriers
        '
        Me.cmb_TypeEtriers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_TypeEtriers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_TypeEtriers.FormattingEnabled = True
        Me.cmb_TypeEtriers.Location = New System.Drawing.Point(63, 8)
        Me.cmb_TypeEtriers.Name = "cmb_TypeEtriers"
        Me.cmb_TypeEtriers.Size = New System.Drawing.Size(168, 21)
        Me.cmb_TypeEtriers.TabIndex = 50
        '
        'lbl_Type
        '
        Me.lbl_Type.AutoSize = True
        Me.lbl_Type.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Type.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Type.Location = New System.Drawing.Point(4, 11)
        Me.lbl_Type.Name = "lbl_Type"
        Me.lbl_Type.Size = New System.Drawing.Size(47, 13)
        Me.lbl_Type.TabIndex = 46
        Me.lbl_Type.Text = "lbl_Type"
        Me.lbl_Type.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_EtrierUz
        '
        Me.txt_EtrierUz.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EtrierUz.Location = New System.Drawing.Point(144, 85)
        Me.txt_EtrierUz.Name = "txt_EtrierUz"
        Me.txt_EtrierUz.Size = New System.Drawing.Size(58, 20)
        Me.txt_EtrierUz.TabIndex = 40
        '
        'txt_EtrierUy
        '
        Me.txt_EtrierUy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EtrierUy.Location = New System.Drawing.Point(144, 66)
        Me.txt_EtrierUy.Name = "txt_EtrierUy"
        Me.txt_EtrierUy.Size = New System.Drawing.Size(58, 20)
        Me.txt_EtrierUy.TabIndex = 38
        '
        'lbl_EnrobageEtrier
        '
        Me.lbl_EnrobageEtrier.AutoSize = True
        Me.lbl_EnrobageEtrier.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_EnrobageEtrier.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_EnrobageEtrier.Location = New System.Drawing.Point(3, 70)
        Me.lbl_EnrobageEtrier.Name = "lbl_EnrobageEtrier"
        Me.lbl_EnrobageEtrier.Size = New System.Drawing.Size(93, 13)
        Me.lbl_EnrobageEtrier.TabIndex = 45
        Me.lbl_EnrobageEtrier.Text = "lbl_EnrobageEtrier"
        Me.lbl_EnrobageEtrier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_DiametreE
        '
        Me.lbl_DiametreE.AutoSize = True
        Me.lbl_DiametreE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DiametreE.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_DiametreE.Location = New System.Drawing.Point(3, 43)
        Me.lbl_DiametreE.Name = "lbl_DiametreE"
        Me.lbl_DiametreE.Size = New System.Drawing.Size(72, 13)
        Me.lbl_DiametreE.TabIndex = 44
        Me.lbl_DiametreE.Text = "lbl_DiametreE"
        Me.lbl_DiametreE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_uz
        '
        Me.img_uz.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_uz.Location = New System.Drawing.Point(108, 85)
        Me.img_uz.Name = "img_uz"
        Me.img_uz.Size = New System.Drawing.Size(37, 20)
        Me.img_uz.TabIndex = 43
        Me.img_uz.TabStop = False
        '
        'img_ux
        '
        Me.img_ux.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ux.Location = New System.Drawing.Point(108, 66)
        Me.img_ux.Name = "img_ux"
        Me.img_ux.Size = New System.Drawing.Size(37, 20)
        Me.img_ux.TabIndex = 42
        Me.img_ux.TabStop = False
        '
        'img_PhiEtrier
        '
        Me.img_PhiEtrier.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PhiEtrier.Location = New System.Drawing.Point(108, 40)
        Me.img_PhiEtrier.Name = "img_PhiEtrier"
        Me.img_PhiEtrier.Size = New System.Drawing.Size(37, 20)
        Me.img_PhiEtrier.TabIndex = 41
        Me.img_PhiEtrier.TabStop = False
        '
        'etq_UnitDim4
        '
        Me.etq_UnitDim4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim4.AutoSize = True
        Me.etq_UnitDim4.Location = New System.Drawing.Point(208, 88)
        Me.etq_UnitDim4.Name = "etq_UnitDim4"
        Me.etq_UnitDim4.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim4.TabIndex = 39
        Me.etq_UnitDim4.Text = "mm"
        '
        'etq_UnitDim3
        '
        Me.etq_UnitDim3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim3.AutoSize = True
        Me.etq_UnitDim3.Location = New System.Drawing.Point(208, 69)
        Me.etq_UnitDim3.Name = "etq_UnitDim3"
        Me.etq_UnitDim3.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim3.TabIndex = 37
        Me.etq_UnitDim3.Text = "mm"
        '
        'etq_UnitDim2
        '
        Me.etq_UnitDim2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim2.AutoSize = True
        Me.etq_UnitDim2.Location = New System.Drawing.Point(208, 43)
        Me.etq_UnitDim2.Name = "etq_UnitDim2"
        Me.etq_UnitDim2.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim2.TabIndex = 35
        Me.etq_UnitDim2.Text = "mm"
        '
        'lbl_Etriers
        '
        Me.lbl_Etriers.BackColor = System.Drawing.SystemColors.ControlDark
        Me.lbl_Etriers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Etriers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Etriers.Location = New System.Drawing.Point(0, 85)
        Me.lbl_Etriers.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Etriers.Name = "lbl_Etriers"
        Me.lbl_Etriers.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Etriers.TabIndex = 10
        Me.lbl_Etriers.Text = "lbl_Etriers"
        Me.lbl_Etriers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pan_Dimensions
        '
        Me.Pan_Dimensions.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Pan_Dimensions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pan_Dimensions.Controls.Add(Me.img_Bc2)
        Me.Pan_Dimensions.Controls.Add(Me.Label1)
        Me.Pan_Dimensions.Controls.Add(Me.txt_Bc)
        Me.Pan_Dimensions.Controls.Add(Me.chk_PcBc)
        Me.Pan_Dimensions.Controls.Add(Me.txt_pcBc)
        Me.Pan_Dimensions.Controls.Add(Me.chk_Bc)
        Me.Pan_Dimensions.Controls.Add(Me.img_Bc)
        Me.Pan_Dimensions.Controls.Add(Me.etq_UnitDim1)
        Me.Pan_Dimensions.Controls.Add(Me.lbl_Largeur)
        Me.Pan_Dimensions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_Dimensions.Location = New System.Drawing.Point(0, 30)
        Me.Pan_Dimensions.Margin = New System.Windows.Forms.Padding(0)
        Me.Pan_Dimensions.Name = "Pan_Dimensions"
        Me.Pan_Dimensions.Size = New System.Drawing.Size(250, 55)
        Me.Pan_Dimensions.TabIndex = 9
        '
        'img_Bc2
        '
        Me.img_Bc2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Bc2.Location = New System.Drawing.Point(170, 26)
        Me.img_Bc2.Name = "img_Bc2"
        Me.img_Bc2.Size = New System.Drawing.Size(19, 20)
        Me.img_Bc2.TabIndex = 39
        Me.img_Bc2.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(159, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(12, 13)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "x"
        '
        'txt_Bc
        '
        Me.txt_Bc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Bc.Location = New System.Drawing.Point(144, 6)
        Me.txt_Bc.Name = "txt_Bc"
        Me.txt_Bc.Size = New System.Drawing.Size(58, 20)
        Me.txt_Bc.TabIndex = 34
        '
        'chk_PcBc
        '
        Me.chk_PcBc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chk_PcBc.AutoSize = True
        Me.chk_PcBc.Location = New System.Drawing.Point(86, 28)
        Me.chk_PcBc.Name = "chk_PcBc"
        Me.chk_PcBc.Size = New System.Drawing.Size(15, 14)
        Me.chk_PcBc.TabIndex = 38
        Me.chk_PcBc.UseVisualStyleBackColor = True
        '
        'txt_pcBc
        '
        Me.txt_pcBc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_pcBc.Location = New System.Drawing.Point(108, 25)
        Me.txt_pcBc.Name = "txt_pcBc"
        Me.txt_pcBc.Size = New System.Drawing.Size(49, 20)
        Me.txt_pcBc.TabIndex = 37
        '
        'chk_Bc
        '
        Me.chk_Bc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chk_Bc.AutoSize = True
        Me.chk_Bc.Location = New System.Drawing.Point(86, 9)
        Me.chk_Bc.Name = "chk_Bc"
        Me.chk_Bc.Size = New System.Drawing.Size(15, 14)
        Me.chk_Bc.TabIndex = 36
        Me.chk_Bc.UseVisualStyleBackColor = True
        '
        'img_Bc
        '
        Me.img_Bc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Bc.Location = New System.Drawing.Point(108, 6)
        Me.img_Bc.Name = "img_Bc"
        Me.img_Bc.Size = New System.Drawing.Size(37, 20)
        Me.img_Bc.TabIndex = 35
        Me.img_Bc.TabStop = False
        '
        'etq_UnitDim1
        '
        Me.etq_UnitDim1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim1.AutoSize = True
        Me.etq_UnitDim1.Location = New System.Drawing.Point(208, 9)
        Me.etq_UnitDim1.Name = "etq_UnitDim1"
        Me.etq_UnitDim1.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim1.TabIndex = 33
        Me.etq_UnitDim1.Text = "mm"
        '
        'lbl_Largeur
        '
        Me.lbl_Largeur.AutoSize = True
        Me.lbl_Largeur.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Largeur.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Largeur.Location = New System.Drawing.Point(3, 6)
        Me.lbl_Largeur.Name = "lbl_Largeur"
        Me.lbl_Largeur.Size = New System.Drawing.Size(59, 13)
        Me.lbl_Largeur.TabIndex = 32
        Me.lbl_Largeur.Text = "lbl_Largeur"
        Me.lbl_Largeur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.lbl_Dimensions.TabIndex = 0
        Me.lbl_Dimensions.Text = "lbl_Dimensions"
        Me.lbl_Dimensions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'img_Enrobage
        '
        Me.img_Enrobage.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Enrobage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Enrobage.Location = New System.Drawing.Point(251, 0)
        Me.img_Enrobage.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.img_Enrobage.Name = "img_Enrobage"
        Me.img_Enrobage.Size = New System.Drawing.Size(100, 50)
        Me.img_Enrobage.TabIndex = 1
        Me.img_Enrobage.TabStop = False
        '
        'Frm_Enrobage
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(1099, 511)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_Enrobage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Enrobage"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Portees.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.Pan_ArmaLongi.ResumeLayout(False)
        Me.TableLayoutPanel_ArmaLongi.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.img_PhiAC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_PhiA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Pan_Etriers.ResumeLayout(False)
        Me.Pan_Etriers.PerformLayout()
        CType(Me.img_uz, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ux, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_PhiEtrier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Pan_Dimensions.ResumeLayout(False)
        Me.Pan_Dimensions.PerformLayout()
        CType(Me.img_Bc2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Bc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Enrobage, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lbl_Dimensions As Label
    Friend WithEvents img_Enrobage As PictureBox
    Friend WithEvents Pan_ArmaLongi As Panel
    Friend WithEvents TableLayoutPanel_ArmaLongi As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents chk_LitSup As CheckBox
    Friend WithEvents chk_LitInter As CheckBox
    Friend WithEvents chk_LitInf As CheckBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents cmb_DiametreC As ComboBox
    Friend WithEvents cmb_Diametre As ComboBox
    Friend WithEvents img_PhiAC As PictureBox
    Friend WithEvents etq_UnitDim6 As Label
    Friend WithEvents chk_Constructif As CheckBox
    Friend WithEvents lbl_Nombre As Label
    Friend WithEvents cmb_Nombre As ComboBox
    Friend WithEvents lbl_DiametreA As Label
    Friend WithEvents img_PhiA As PictureBox
    Friend WithEvents etq_UnitDim5 As Label
    Friend WithEvents lbl_LitSelectionne As Label
    Friend WithEvents lbl_ArmaLongi As Label
    Friend WithEvents Pan_Etriers As Panel
    Friend WithEvents cmb_DiametreEtriers As ComboBox
    Friend WithEvents cmb_TypeEtriers As ComboBox
    Friend WithEvents lbl_Type As Label
    Friend WithEvents txt_EtrierUz As TextBox
    Friend WithEvents txt_EtrierUy As TextBox
    Friend WithEvents lbl_EnrobageEtrier As Label
    Friend WithEvents lbl_DiametreE As Label
    Friend WithEvents img_uz As PictureBox
    Friend WithEvents img_ux As PictureBox
    Friend WithEvents img_PhiEtrier As PictureBox
    Friend WithEvents etq_UnitDim4 As Label
    Friend WithEvents etq_UnitDim3 As Label
    Friend WithEvents etq_UnitDim2 As Label
    Friend WithEvents lbl_Etriers As Label
    Friend WithEvents Pan_Dimensions As Panel
    Friend WithEvents img_Bc2 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_Bc As TextBox
    Friend WithEvents chk_PcBc As CheckBox
    Friend WithEvents txt_pcBc As TextBox
    Friend WithEvents chk_Bc As CheckBox
    Friend WithEvents img_Bc As PictureBox
    Friend WithEvents etq_UnitDim1 As Label
    Friend WithEvents lbl_Largeur As Label
End Class
