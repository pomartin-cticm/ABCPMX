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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Enrobage))
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Enrobage = New System.Windows.Forms.TableLayoutPanel()
        Me.img_Enrobage = New System.Windows.Forms.PictureBox()
        Me.pan_ControleG = New System.Windows.Forms.Panel()
        Me.TLpan_ControlesG = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_SeparationH = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_SupGauche = New System.Windows.Forms.TableLayoutPanel()
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
        Me.cmb_RatioBc = New System.Windows.Forms.ComboBox()
        Me.img_BcX = New System.Windows.Forms.PictureBox()
        Me.img_Bf = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_Bc = New System.Windows.Forms.TextBox()
        Me.img_Bc = New System.Windows.Forms.PictureBox()
        Me.lbl_Largeur = New System.Windows.Forms.Label()
        Me.etq_UnitDim1 = New System.Windows.Forms.Label()
        Me.lbl_Dimensions = New System.Windows.Forms.Label()
        Me.TLpan_SupDroite = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txt_Fsk = New System.Windows.Forms.TextBox()
        Me.img_Fy = New System.Windows.Forms.PictureBox()
        Me.etq_UnitSigma2 = New System.Windows.Forms.Label()
        Me.cmb_Acier = New System.Windows.Forms.ComboBox()
        Me.lbl_ClasseA = New System.Windows.Forms.Label()
        Me.lbl_Acier = New System.Windows.Forms.Label()
        Me.lbl_Beton = New System.Windows.Forms.Label()
        Me.pan_Beton = New System.Windows.Forms.Panel()
        Me.txt_Ecm = New System.Windows.Forms.TextBox()
        Me.img_Ecm = New System.Windows.Forms.PictureBox()
        Me.etq_UnitModule1 = New System.Windows.Forms.Label()
        Me.txt_Fck = New System.Windows.Forms.TextBox()
        Me.img_Fck = New System.Windows.Forms.PictureBox()
        Me.etq_UnitSigma1 = New System.Windows.Forms.Label()
        Me.cmb_ClasseBetonEnrobage = New System.Windows.Forms.ComboBox()
        Me.lbl_ClasseE = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Pan_ArmaLongi = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_ArmaLongi = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txt_zArma = New System.Windows.Forms.TextBox()
        Me.img_zArma = New System.Windows.Forms.PictureBox()
        Me.etq_UnitDim6 = New System.Windows.Forms.Label()
        Me.lbl_PositionZarma = New System.Windows.Forms.Label()
        Me.etq_UnitDim5 = New System.Windows.Forms.Label()
        Me.pan_Interieur = New System.Windows.Forms.Panel()
        Me.lbl_Interieur = New System.Windows.Forms.Label()
        Me.cmb_DiaInt = New System.Windows.Forms.ComboBox()
        Me.cmb_NombreInt = New System.Windows.Forms.ComboBox()
        Me.pan_Mileu = New System.Windows.Forms.Panel()
        Me.lbl_Middle = New System.Windows.Forms.Label()
        Me.cmb_DiaMil = New System.Windows.Forms.ComboBox()
        Me.cmb_NombreMil = New System.Windows.Forms.ComboBox()
        Me.pan_Exterieur = New System.Windows.Forms.Panel()
        Me.lbl_Exterieur = New System.Windows.Forms.Label()
        Me.cmb_NombreExt = New System.Windows.Forms.ComboBox()
        Me.cmb_DiaExt = New System.Windows.Forms.ComboBox()
        Me.lbl_Nombre = New System.Windows.Forms.Label()
        Me.lbl_DiametreA = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chk_LitSup = New System.Windows.Forms.CheckBox()
        Me.chk_LitInter = New System.Windows.Forms.CheckBox()
        Me.chk_LitInf = New System.Windows.Forms.CheckBox()
        Me.pan_InforArma = New System.Windows.Forms.Panel()
        Me.txt_As = New System.Windows.Forms.TextBox()
        Me.img_As = New System.Windows.Forms.PictureBox()
        Me.etq_UnitDimCarre = New System.Windows.Forms.Label()
        Me.lbl_LitSelectionne = New System.Windows.Forms.Label()
        Me.lbl_ArmaLongi = New System.Windows.Forms.Label()
        Me.MyImgList = New System.Windows.Forms.ImageList(Me.components)
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.lbl_Active = New System.Windows.Forms.Label()
        Me.chk_ActiveExt = New System.Windows.Forms.CheckBox()
        Me.chk_ActiveMil = New System.Windows.Forms.CheckBox()
        Me.chk_ActiveInt = New System.Windows.Forms.CheckBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Enrobage.SuspendLayout()
        CType(Me.img_Enrobage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_ControleG.SuspendLayout()
        Me.TLpan_ControlesG.SuspendLayout()
        Me.TLpan_SeparationH.SuspendLayout()
        Me.TLpan_SupGauche.SuspendLayout()
        Me.Pan_Etriers.SuspendLayout()
        CType(Me.img_uz, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ux, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_PhiEtrier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Pan_Dimensions.SuspendLayout()
        CType(Me.img_BcX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Bf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Bc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLpan_SupDroite.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.img_Fy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Beton.SuspendLayout()
        CType(Me.img_Ecm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Fck, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Pan_ArmaLongi.SuspendLayout()
        Me.TableLayoutPanel_ArmaLongi.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.img_zArma, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Interieur.SuspendLayout()
        Me.pan_Mileu.SuspendLayout()
        Me.pan_Exterieur.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pan_InforArma.SuspendLayout()
        CType(Me.img_As, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pan_General.Size = New System.Drawing.Size(1099, 503)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(1099, 503)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 466)
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
        Me.pan_Main.Controls.Add(Me.TLPan_Enrobage)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(1093, 457)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Enrobage
        '
        Me.TLPan_Enrobage.ColumnCount = 2
        Me.TLPan_Enrobage.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 501.0!))
        Me.TLPan_Enrobage.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Enrobage.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Enrobage.Controls.Add(Me.img_Enrobage, 1, 0)
        Me.TLPan_Enrobage.Controls.Add(Me.pan_ControleG, 0, 0)
        Me.TLPan_Enrobage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Enrobage.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Enrobage.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Enrobage.Name = "TLPan_Enrobage"
        Me.TLPan_Enrobage.RowCount = 1
        Me.TLPan_Enrobage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Enrobage.Size = New System.Drawing.Size(1093, 457)
        Me.TLPan_Enrobage.TabIndex = 0
        '
        'img_Enrobage
        '
        Me.img_Enrobage.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Enrobage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Enrobage.Location = New System.Drawing.Point(502, 0)
        Me.img_Enrobage.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.img_Enrobage.Name = "img_Enrobage"
        Me.img_Enrobage.Size = New System.Drawing.Size(100, 50)
        Me.img_Enrobage.TabIndex = 1
        Me.img_Enrobage.TabStop = False
        '
        'pan_ControleG
        '
        Me.pan_ControleG.Controls.Add(Me.TLpan_ControlesG)
        Me.pan_ControleG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ControleG.Location = New System.Drawing.Point(0, 0)
        Me.pan_ControleG.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ControleG.Name = "pan_ControleG"
        Me.pan_ControleG.Size = New System.Drawing.Size(501, 457)
        Me.pan_ControleG.TabIndex = 2
        '
        'TLpan_ControlesG
        '
        Me.TLpan_ControlesG.ColumnCount = 1
        Me.TLpan_ControlesG.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_ControlesG.Controls.Add(Me.TLpan_SeparationH, 0, 0)
        Me.TLpan_ControlesG.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TLpan_ControlesG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_ControlesG.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_ControlesG.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_ControlesG.Name = "TLpan_ControlesG"
        Me.TLpan_ControlesG.RowCount = 2
        Me.TLpan_ControlesG.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 235.0!))
        Me.TLpan_ControlesG.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_ControlesG.Size = New System.Drawing.Size(501, 457)
        Me.TLpan_ControlesG.TabIndex = 0
        '
        'TLpan_SeparationH
        '
        Me.TLpan_SeparationH.ColumnCount = 2
        Me.TLpan_SeparationH.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLpan_SeparationH.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_SeparationH.Controls.Add(Me.TLpan_SupGauche, 0, 0)
        Me.TLpan_SeparationH.Controls.Add(Me.TLpan_SupDroite, 1, 0)
        Me.TLpan_SeparationH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_SeparationH.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_SeparationH.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_SeparationH.Name = "TLpan_SeparationH"
        Me.TLpan_SeparationH.RowCount = 1
        Me.TLpan_SeparationH.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_SeparationH.Size = New System.Drawing.Size(501, 235)
        Me.TLpan_SeparationH.TabIndex = 0
        '
        'TLpan_SupGauche
        '
        Me.TLpan_SupGauche.ColumnCount = 1
        Me.TLpan_SupGauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_SupGauche.Controls.Add(Me.Pan_Etriers, 0, 3)
        Me.TLpan_SupGauche.Controls.Add(Me.lbl_Etriers, 0, 2)
        Me.TLpan_SupGauche.Controls.Add(Me.Pan_Dimensions, 0, 1)
        Me.TLpan_SupGauche.Controls.Add(Me.lbl_Dimensions, 0, 0)
        Me.TLpan_SupGauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_SupGauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_SupGauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_SupGauche.Name = "TLpan_SupGauche"
        Me.TLpan_SupGauche.RowCount = 4
        Me.TLpan_SupGauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_SupGauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56.0!))
        Me.TLpan_SupGauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_SupGauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_SupGauche.Size = New System.Drawing.Size(250, 235)
        Me.TLpan_SupGauche.TabIndex = 0
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
        Me.Pan_Etriers.Location = New System.Drawing.Point(0, 116)
        Me.Pan_Etriers.Margin = New System.Windows.Forms.Padding(0)
        Me.Pan_Etriers.Name = "Pan_Etriers"
        Me.Pan_Etriers.Size = New System.Drawing.Size(250, 119)
        Me.Pan_Etriers.TabIndex = 12
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
        Me.lbl_Etriers.Location = New System.Drawing.Point(0, 86)
        Me.lbl_Etriers.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Etriers.Name = "lbl_Etriers"
        Me.lbl_Etriers.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Etriers.TabIndex = 11
        Me.lbl_Etriers.Text = "lbl_Etriers"
        Me.lbl_Etriers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pan_Dimensions
        '
        Me.Pan_Dimensions.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Pan_Dimensions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pan_Dimensions.Controls.Add(Me.cmb_RatioBc)
        Me.Pan_Dimensions.Controls.Add(Me.img_BcX)
        Me.Pan_Dimensions.Controls.Add(Me.img_Bf)
        Me.Pan_Dimensions.Controls.Add(Me.Label1)
        Me.Pan_Dimensions.Controls.Add(Me.txt_Bc)
        Me.Pan_Dimensions.Controls.Add(Me.img_Bc)
        Me.Pan_Dimensions.Controls.Add(Me.lbl_Largeur)
        Me.Pan_Dimensions.Controls.Add(Me.etq_UnitDim1)
        Me.Pan_Dimensions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_Dimensions.Location = New System.Drawing.Point(0, 30)
        Me.Pan_Dimensions.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.Pan_Dimensions.Name = "Pan_Dimensions"
        Me.Pan_Dimensions.Size = New System.Drawing.Size(250, 55)
        Me.Pan_Dimensions.TabIndex = 10
        '
        'cmb_RatioBc
        '
        Me.cmb_RatioBc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_RatioBc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_RatioBc.FormattingEnabled = True
        Me.cmb_RatioBc.Location = New System.Drawing.Point(135, 6)
        Me.cmb_RatioBc.Name = "cmb_RatioBc"
        Me.cmb_RatioBc.Size = New System.Drawing.Size(58, 21)
        Me.cmb_RatioBc.TabIndex = 52
        '
        'img_BcX
        '
        Me.img_BcX.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_BcX.Location = New System.Drawing.Point(99, 7)
        Me.img_BcX.Name = "img_BcX"
        Me.img_BcX.Size = New System.Drawing.Size(37, 20)
        Me.img_BcX.TabIndex = 53
        Me.img_BcX.TabStop = False
        '
        'img_Bf
        '
        Me.img_Bf.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Bf.Location = New System.Drawing.Point(206, 7)
        Me.img_Bf.Name = "img_Bf"
        Me.img_Bf.Size = New System.Drawing.Size(19, 20)
        Me.img_Bf.TabIndex = 39
        Me.img_Bf.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(195, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(12, 13)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "x"
        '
        'txt_Bc
        '
        Me.txt_Bc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Bc.Location = New System.Drawing.Point(135, 30)
        Me.txt_Bc.Name = "txt_Bc"
        Me.txt_Bc.Size = New System.Drawing.Size(58, 20)
        Me.txt_Bc.TabIndex = 34
        '
        'img_Bc
        '
        Me.img_Bc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Bc.Location = New System.Drawing.Point(99, 30)
        Me.img_Bc.Name = "img_Bc"
        Me.img_Bc.Size = New System.Drawing.Size(37, 20)
        Me.img_Bc.TabIndex = 35
        Me.img_Bc.TabStop = False
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
        'etq_UnitDim1
        '
        Me.etq_UnitDim1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim1.AutoSize = True
        Me.etq_UnitDim1.Location = New System.Drawing.Point(199, 33)
        Me.etq_UnitDim1.Name = "etq_UnitDim1"
        Me.etq_UnitDim1.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim1.TabIndex = 33
        Me.etq_UnitDim1.Text = "mm"
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
        Me.lbl_Dimensions.TabIndex = 1
        Me.lbl_Dimensions.Text = "lbl_Dimensions"
        Me.lbl_Dimensions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TLpan_SupDroite
        '
        Me.TLpan_SupDroite.ColumnCount = 1
        Me.TLpan_SupDroite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_SupDroite.Controls.Add(Me.Panel1, 0, 3)
        Me.TLpan_SupDroite.Controls.Add(Me.lbl_Acier, 0, 2)
        Me.TLpan_SupDroite.Controls.Add(Me.lbl_Beton, 0, 0)
        Me.TLpan_SupDroite.Controls.Add(Me.pan_Beton, 0, 1)
        Me.TLpan_SupDroite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_SupDroite.Location = New System.Drawing.Point(251, 0)
        Me.TLpan_SupDroite.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.TLpan_SupDroite.Name = "TLpan_SupDroite"
        Me.TLpan_SupDroite.RowCount = 4
        Me.TLpan_SupDroite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_SupDroite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TLpan_SupDroite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_SupDroite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_SupDroite.Size = New System.Drawing.Size(250, 235)
        Me.TLpan_SupDroite.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txt_Fsk)
        Me.Panel1.Controls.Add(Me.img_Fy)
        Me.Panel1.Controls.Add(Me.etq_UnitSigma2)
        Me.Panel1.Controls.Add(Me.cmb_Acier)
        Me.Panel1.Controls.Add(Me.lbl_ClasseA)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 150)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(250, 85)
        Me.Panel1.TabIndex = 13
        '
        'txt_Fsk
        '
        Me.txt_Fsk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Fsk.Location = New System.Drawing.Point(127, 33)
        Me.txt_Fsk.Name = "txt_Fsk"
        Me.txt_Fsk.Size = New System.Drawing.Size(58, 20)
        Me.txt_Fsk.TabIndex = 59
        '
        'img_Fy
        '
        Me.img_Fy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Fy.Location = New System.Drawing.Point(71, 33)
        Me.img_Fy.Name = "img_Fy"
        Me.img_Fy.Size = New System.Drawing.Size(57, 20)
        Me.img_Fy.TabIndex = 60
        Me.img_Fy.TabStop = False
        '
        'etq_UnitSigma2
        '
        Me.etq_UnitSigma2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitSigma2.AutoSize = True
        Me.etq_UnitSigma2.Location = New System.Drawing.Point(191, 36)
        Me.etq_UnitSigma2.Name = "etq_UnitSigma2"
        Me.etq_UnitSigma2.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitSigma2.TabIndex = 58
        Me.etq_UnitSigma2.Text = "mm"
        '
        'cmb_Acier
        '
        Me.cmb_Acier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Acier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Acier.FormattingEnabled = True
        Me.cmb_Acier.Location = New System.Drawing.Point(127, 6)
        Me.cmb_Acier.Name = "cmb_Acier"
        Me.cmb_Acier.Size = New System.Drawing.Size(111, 21)
        Me.cmb_Acier.TabIndex = 57
        '
        'lbl_ClasseA
        '
        Me.lbl_ClasseA.AutoSize = True
        Me.lbl_ClasseA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ClasseA.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_ClasseA.Location = New System.Drawing.Point(11, 9)
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
        Me.lbl_Acier.Location = New System.Drawing.Point(0, 120)
        Me.lbl_Acier.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Acier.Name = "lbl_Acier"
        Me.lbl_Acier.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Acier.TabIndex = 12
        Me.lbl_Acier.Text = "lbl_Acier"
        Me.lbl_Acier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Beton
        '
        Me.lbl_Beton.AutoSize = True
        Me.lbl_Beton.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Beton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Beton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Beton.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Beton.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Beton.Name = "lbl_Beton"
        Me.lbl_Beton.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Beton.TabIndex = 2
        Me.lbl_Beton.Text = "lbl_Beton"
        Me.lbl_Beton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.pan_Beton.Location = New System.Drawing.Point(0, 30)
        Me.pan_Beton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Beton.Name = "pan_Beton"
        Me.pan_Beton.Size = New System.Drawing.Size(250, 89)
        Me.pan_Beton.TabIndex = 3
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
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Pan_ArmaLongi, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.lbl_ArmaLongi, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 236)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(501, 221)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'Pan_ArmaLongi
        '
        Me.Pan_ArmaLongi.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Pan_ArmaLongi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pan_ArmaLongi.Controls.Add(Me.TableLayoutPanel_ArmaLongi)
        Me.Pan_ArmaLongi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_ArmaLongi.Location = New System.Drawing.Point(0, 30)
        Me.Pan_ArmaLongi.Margin = New System.Windows.Forms.Padding(0)
        Me.Pan_ArmaLongi.Name = "Pan_ArmaLongi"
        Me.Pan_ArmaLongi.Size = New System.Drawing.Size(501, 191)
        Me.Pan_ArmaLongi.TabIndex = 14
        '
        'TableLayoutPanel_ArmaLongi
        '
        Me.TableLayoutPanel_ArmaLongi.ColumnCount = 1
        Me.TableLayoutPanel_ArmaLongi.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_ArmaLongi.Controls.Add(Me.Panel3, 0, 1)
        Me.TableLayoutPanel_ArmaLongi.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.TableLayoutPanel_ArmaLongi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_ArmaLongi.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_ArmaLongi.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel_ArmaLongi.Name = "TableLayoutPanel_ArmaLongi"
        Me.TableLayoutPanel_ArmaLongi.RowCount = 2
        Me.TableLayoutPanel_ArmaLongi.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TableLayoutPanel_ArmaLongi.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_ArmaLongi.Size = New System.Drawing.Size(499, 189)
        Me.TableLayoutPanel_ArmaLongi.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lbl_Active)
        Me.Panel3.Controls.Add(Me.txt_zArma)
        Me.Panel3.Controls.Add(Me.img_zArma)
        Me.Panel3.Controls.Add(Me.etq_UnitDim6)
        Me.Panel3.Controls.Add(Me.lbl_PositionZarma)
        Me.Panel3.Controls.Add(Me.etq_UnitDim5)
        Me.Panel3.Controls.Add(Me.pan_Interieur)
        Me.Panel3.Controls.Add(Me.pan_Mileu)
        Me.Panel3.Controls.Add(Me.pan_Exterieur)
        Me.Panel3.Controls.Add(Me.lbl_Nombre)
        Me.Panel3.Controls.Add(Me.lbl_DiametreA)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 50)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(499, 139)
        Me.Panel3.TabIndex = 1
        '
        'txt_zArma
        '
        Me.txt_zArma.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_zArma.Location = New System.Drawing.Point(153, 109)
        Me.txt_zArma.Name = "txt_zArma"
        Me.txt_zArma.Size = New System.Drawing.Size(58, 20)
        Me.txt_zArma.TabIndex = 56
        '
        'img_zArma
        '
        Me.img_zArma.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_zArma.Location = New System.Drawing.Point(99, 109)
        Me.img_zArma.Name = "img_zArma"
        Me.img_zArma.Size = New System.Drawing.Size(55, 20)
        Me.img_zArma.TabIndex = 57
        Me.img_zArma.TabStop = False
        '
        'etq_UnitDim6
        '
        Me.etq_UnitDim6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim6.AutoSize = True
        Me.etq_UnitDim6.Location = New System.Drawing.Point(217, 112)
        Me.etq_UnitDim6.Name = "etq_UnitDim6"
        Me.etq_UnitDim6.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim6.TabIndex = 55
        Me.etq_UnitDim6.Text = "mm"
        '
        'lbl_PositionZarma
        '
        Me.lbl_PositionZarma.AutoSize = True
        Me.lbl_PositionZarma.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_PositionZarma.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_PositionZarma.Location = New System.Drawing.Point(10, 112)
        Me.lbl_PositionZarma.Name = "lbl_PositionZarma"
        Me.lbl_PositionZarma.Size = New System.Drawing.Size(90, 13)
        Me.lbl_PositionZarma.TabIndex = 54
        Me.lbl_PositionZarma.Text = "lbl_PositionZarma"
        Me.lbl_PositionZarma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_UnitDim5
        '
        Me.etq_UnitDim5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim5.AutoSize = True
        Me.etq_UnitDim5.Location = New System.Drawing.Point(388, 58)
        Me.etq_UnitDim5.Name = "etq_UnitDim5"
        Me.etq_UnitDim5.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim5.TabIndex = 52
        Me.etq_UnitDim5.Text = "mm"
        '
        'pan_Interieur
        '
        Me.pan_Interieur.Controls.Add(Me.chk_ActiveInt)
        Me.pan_Interieur.Controls.Add(Me.lbl_Interieur)
        Me.pan_Interieur.Controls.Add(Me.cmb_DiaInt)
        Me.pan_Interieur.Controls.Add(Me.cmb_NombreInt)
        Me.pan_Interieur.Location = New System.Drawing.Point(295, 7)
        Me.pan_Interieur.Name = "pan_Interieur"
        Me.pan_Interieur.Size = New System.Drawing.Size(87, 93)
        Me.pan_Interieur.TabIndex = 53
        '
        'lbl_Interieur
        '
        Me.lbl_Interieur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Interieur.Location = New System.Drawing.Point(3, 0)
        Me.lbl_Interieur.Name = "lbl_Interieur"
        Me.lbl_Interieur.Size = New System.Drawing.Size(80, 17)
        Me.lbl_Interieur.TabIndex = 25
        Me.lbl_Interieur.Text = "lbl_Interieur"
        Me.lbl_Interieur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmb_DiaInt
        '
        Me.cmb_DiaInt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_DiaInt.FormattingEnabled = True
        Me.cmb_DiaInt.Location = New System.Drawing.Point(3, 47)
        Me.cmb_DiaInt.Name = "cmb_DiaInt"
        Me.cmb_DiaInt.Size = New System.Drawing.Size(80, 21)
        Me.cmb_DiaInt.TabIndex = 63
        '
        'cmb_NombreInt
        '
        Me.cmb_NombreInt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NombreInt.FormattingEnabled = True
        Me.cmb_NombreInt.Location = New System.Drawing.Point(3, 22)
        Me.cmb_NombreInt.Name = "cmb_NombreInt"
        Me.cmb_NombreInt.Size = New System.Drawing.Size(80, 21)
        Me.cmb_NombreInt.TabIndex = 62
        '
        'pan_Mileu
        '
        Me.pan_Mileu.Controls.Add(Me.chk_ActiveMil)
        Me.pan_Mileu.Controls.Add(Me.lbl_Middle)
        Me.pan_Mileu.Controls.Add(Me.cmb_DiaMil)
        Me.pan_Mileu.Controls.Add(Me.cmb_NombreMil)
        Me.pan_Mileu.Location = New System.Drawing.Point(206, 7)
        Me.pan_Mileu.Name = "pan_Mileu"
        Me.pan_Mileu.Size = New System.Drawing.Size(87, 93)
        Me.pan_Mileu.TabIndex = 52
        '
        'lbl_Middle
        '
        Me.lbl_Middle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Middle.Location = New System.Drawing.Point(3, 0)
        Me.lbl_Middle.Name = "lbl_Middle"
        Me.lbl_Middle.Size = New System.Drawing.Size(80, 17)
        Me.lbl_Middle.TabIndex = 24
        Me.lbl_Middle.Text = "lbl_Middle"
        Me.lbl_Middle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmb_DiaMil
        '
        Me.cmb_DiaMil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_DiaMil.FormattingEnabled = True
        Me.cmb_DiaMil.Location = New System.Drawing.Point(3, 47)
        Me.cmb_DiaMil.Name = "cmb_DiaMil"
        Me.cmb_DiaMil.Size = New System.Drawing.Size(80, 21)
        Me.cmb_DiaMil.TabIndex = 61
        '
        'cmb_NombreMil
        '
        Me.cmb_NombreMil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NombreMil.FormattingEnabled = True
        Me.cmb_NombreMil.Location = New System.Drawing.Point(3, 22)
        Me.cmb_NombreMil.Name = "cmb_NombreMil"
        Me.cmb_NombreMil.Size = New System.Drawing.Size(80, 21)
        Me.cmb_NombreMil.TabIndex = 60
        '
        'pan_Exterieur
        '
        Me.pan_Exterieur.Controls.Add(Me.chk_ActiveExt)
        Me.pan_Exterieur.Controls.Add(Me.lbl_Exterieur)
        Me.pan_Exterieur.Controls.Add(Me.cmb_NombreExt)
        Me.pan_Exterieur.Controls.Add(Me.cmb_DiaExt)
        Me.pan_Exterieur.Location = New System.Drawing.Point(117, 7)
        Me.pan_Exterieur.Name = "pan_Exterieur"
        Me.pan_Exterieur.Size = New System.Drawing.Size(87, 93)
        Me.pan_Exterieur.TabIndex = 51
        '
        'lbl_Exterieur
        '
        Me.lbl_Exterieur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Exterieur.Location = New System.Drawing.Point(3, 0)
        Me.lbl_Exterieur.Name = "lbl_Exterieur"
        Me.lbl_Exterieur.Size = New System.Drawing.Size(80, 17)
        Me.lbl_Exterieur.TabIndex = 23
        Me.lbl_Exterieur.Text = "lbl_Exterieur"
        Me.lbl_Exterieur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmb_NombreExt
        '
        Me.cmb_NombreExt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NombreExt.FormattingEnabled = True
        Me.cmb_NombreExt.Location = New System.Drawing.Point(3, 22)
        Me.cmb_NombreExt.Name = "cmb_NombreExt"
        Me.cmb_NombreExt.Size = New System.Drawing.Size(80, 21)
        Me.cmb_NombreExt.TabIndex = 57
        '
        'cmb_DiaExt
        '
        Me.cmb_DiaExt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_DiaExt.FormattingEnabled = True
        Me.cmb_DiaExt.Location = New System.Drawing.Point(3, 47)
        Me.cmb_DiaExt.Name = "cmb_DiaExt"
        Me.cmb_DiaExt.Size = New System.Drawing.Size(80, 21)
        Me.cmb_DiaExt.TabIndex = 59
        '
        'lbl_Nombre
        '
        Me.lbl_Nombre.AutoSize = True
        Me.lbl_Nombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Nombre.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Nombre.Location = New System.Drawing.Point(10, 31)
        Me.lbl_Nombre.Name = "lbl_Nombre"
        Me.lbl_Nombre.Size = New System.Drawing.Size(60, 13)
        Me.lbl_Nombre.TabIndex = 50
        Me.lbl_Nombre.Text = "lbl_Nombre"
        Me.lbl_Nombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_DiametreA
        '
        Me.lbl_DiametreA.AutoSize = True
        Me.lbl_DiametreA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DiametreA.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_DiametreA.Location = New System.Drawing.Point(10, 57)
        Me.lbl_DiametreA.Name = "lbl_DiametreA"
        Me.lbl_DiametreA.Size = New System.Drawing.Size(65, 13)
        Me.lbl_DiametreA.TabIndex = 48
        Me.lbl_DiametreA.Text = "lbl_Diametre"
        Me.lbl_DiametreA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pan_InforArma, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(499, 50)
        Me.TableLayoutPanel1.TabIndex = 2
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
        Me.Panel2.Size = New System.Drawing.Size(249, 50)
        Me.Panel2.TabIndex = 3
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
        Me.chk_LitInter.Location = New System.Drawing.Point(99, 3)
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
        Me.chk_LitInf.Location = New System.Drawing.Point(22, 3)
        Me.chk_LitInf.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_LitInf.Name = "chk_LitInf"
        Me.chk_LitInf.Size = New System.Drawing.Size(44, 44)
        Me.chk_LitInf.TabIndex = 0
        Me.chk_LitInf.UseVisualStyleBackColor = True
        '
        'pan_InforArma
        '
        Me.pan_InforArma.Controls.Add(Me.txt_As)
        Me.pan_InforArma.Controls.Add(Me.img_As)
        Me.pan_InforArma.Controls.Add(Me.etq_UnitDimCarre)
        Me.pan_InforArma.Controls.Add(Me.lbl_LitSelectionne)
        Me.pan_InforArma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_InforArma.Location = New System.Drawing.Point(249, 0)
        Me.pan_InforArma.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_InforArma.Name = "pan_InforArma"
        Me.pan_InforArma.Size = New System.Drawing.Size(250, 50)
        Me.pan_InforArma.TabIndex = 4
        '
        'txt_As
        '
        Me.txt_As.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_As.Location = New System.Drawing.Point(97, 24)
        Me.txt_As.Name = "txt_As"
        Me.txt_As.Size = New System.Drawing.Size(58, 20)
        Me.txt_As.TabIndex = 59
        '
        'img_As
        '
        Me.img_As.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_As.Location = New System.Drawing.Point(46, 24)
        Me.img_As.Name = "img_As"
        Me.img_As.Size = New System.Drawing.Size(52, 20)
        Me.img_As.TabIndex = 60
        Me.img_As.TabStop = False
        '
        'etq_UnitDimCarre
        '
        Me.etq_UnitDimCarre.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDimCarre.AutoSize = True
        Me.etq_UnitDimCarre.Location = New System.Drawing.Point(161, 27)
        Me.etq_UnitDimCarre.Name = "etq_UnitDimCarre"
        Me.etq_UnitDimCarre.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDimCarre.TabIndex = 58
        Me.etq_UnitDimCarre.Text = "mm"
        '
        'lbl_LitSelectionne
        '
        Me.lbl_LitSelectionne.Location = New System.Drawing.Point(3, 2)
        Me.lbl_LitSelectionne.Name = "lbl_LitSelectionne"
        Me.lbl_LitSelectionne.Size = New System.Drawing.Size(245, 19)
        Me.lbl_LitSelectionne.TabIndex = 0
        Me.lbl_LitSelectionne.Text = "lbl_LitSelectionne"
        Me.lbl_LitSelectionne.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_ArmaLongi
        '
        Me.lbl_ArmaLongi.BackColor = System.Drawing.SystemColors.ControlDark
        Me.lbl_ArmaLongi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ArmaLongi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ArmaLongi.Location = New System.Drawing.Point(0, 0)
        Me.lbl_ArmaLongi.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ArmaLongi.Name = "lbl_ArmaLongi"
        Me.lbl_ArmaLongi.Size = New System.Drawing.Size(501, 30)
        Me.lbl_ArmaLongi.TabIndex = 13
        Me.lbl_ArmaLongi.Text = "lbl_ArmaLongi"
        Me.lbl_ArmaLongi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MyImgList
        '
        Me.MyImgList.ImageStream = CType(resources.GetObject("MyImgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.MyImgList.TransparentColor = System.Drawing.Color.Transparent
        Me.MyImgList.Images.SetKeyName(0, "LitInfNonS")
        Me.MyImgList.Images.SetKeyName(1, "LitInf")
        Me.MyImgList.Images.SetKeyName(2, "LitInterNonS")
        Me.MyImgList.Images.SetKeyName(3, "LitInter")
        Me.MyImgList.Images.SetKeyName(4, "LitSupNonS")
        Me.MyImgList.Images.SetKeyName(5, "LitSup")
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'lbl_Active
        '
        Me.lbl_Active.AutoSize = True
        Me.lbl_Active.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Active.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Active.Location = New System.Drawing.Point(10, 82)
        Me.lbl_Active.Name = "lbl_Active"
        Me.lbl_Active.Size = New System.Drawing.Size(53, 13)
        Me.lbl_Active.TabIndex = 58
        Me.lbl_Active.Text = "lbl_Active"
        Me.lbl_Active.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chk_ActiveExt
        '
        Me.chk_ActiveExt.AutoSize = True
        Me.chk_ActiveExt.Location = New System.Drawing.Point(36, 74)
        Me.chk_ActiveExt.Name = "chk_ActiveExt"
        Me.chk_ActiveExt.Size = New System.Drawing.Size(15, 14)
        Me.chk_ActiveExt.TabIndex = 59
        Me.chk_ActiveExt.UseVisualStyleBackColor = True
        '
        'chk_ActiveMil
        '
        Me.chk_ActiveMil.AutoSize = True
        Me.chk_ActiveMil.Location = New System.Drawing.Point(37, 74)
        Me.chk_ActiveMil.Name = "chk_ActiveMil"
        Me.chk_ActiveMil.Size = New System.Drawing.Size(15, 14)
        Me.chk_ActiveMil.TabIndex = 60
        Me.chk_ActiveMil.UseVisualStyleBackColor = True
        '
        'chk_ActiveInt
        '
        Me.chk_ActiveInt.AutoSize = True
        Me.chk_ActiveInt.Location = New System.Drawing.Point(37, 74)
        Me.chk_ActiveInt.Name = "chk_ActiveInt"
        Me.chk_ActiveInt.Size = New System.Drawing.Size(15, 14)
        Me.chk_ActiveInt.TabIndex = 60
        Me.chk_ActiveInt.UseVisualStyleBackColor = True
        '
        'Frm_Enrobage
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(1099, 503)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_Enrobage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Enrobage"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Enrobage.ResumeLayout(False)
        CType(Me.img_Enrobage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_ControleG.ResumeLayout(False)
        Me.TLpan_ControlesG.ResumeLayout(False)
        Me.TLpan_SeparationH.ResumeLayout(False)
        Me.TLpan_SupGauche.ResumeLayout(False)
        Me.TLpan_SupGauche.PerformLayout()
        Me.Pan_Etriers.ResumeLayout(False)
        Me.Pan_Etriers.PerformLayout()
        CType(Me.img_uz, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ux, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_PhiEtrier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Pan_Dimensions.ResumeLayout(False)
        Me.Pan_Dimensions.PerformLayout()
        CType(Me.img_BcX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Bf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Bc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLpan_SupDroite.ResumeLayout(False)
        Me.TLpan_SupDroite.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.img_Fy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Beton.ResumeLayout(False)
        Me.pan_Beton.PerformLayout()
        CType(Me.img_Ecm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Fck, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Pan_ArmaLongi.ResumeLayout(False)
        Me.TableLayoutPanel_ArmaLongi.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.img_zArma, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Interieur.ResumeLayout(False)
        Me.pan_Interieur.PerformLayout()
        Me.pan_Mileu.ResumeLayout(False)
        Me.pan_Mileu.PerformLayout()
        Me.pan_Exterieur.ResumeLayout(False)
        Me.pan_Exterieur.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pan_InforArma.ResumeLayout(False)
        Me.pan_InforArma.PerformLayout()
        CType(Me.img_As, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Enrobage As TableLayoutPanel
    Friend WithEvents img_Enrobage As PictureBox
    Friend WithEvents MyImgList As ImageList
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents pan_ControleG As Panel
    Friend WithEvents TLpan_ControlesG As TableLayoutPanel
    Friend WithEvents TLpan_SeparationH As TableLayoutPanel
    Friend WithEvents TLpan_SupGauche As TableLayoutPanel
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
    Friend WithEvents cmb_RatioBc As ComboBox
    Friend WithEvents img_BcX As PictureBox
    Friend WithEvents img_Bf As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txt_Bc As TextBox
    Friend WithEvents img_Bc As PictureBox
    Friend WithEvents lbl_Largeur As Label
    Friend WithEvents etq_UnitDim1 As Label
    Friend WithEvents lbl_Dimensions As Label
    Friend WithEvents TLpan_SupDroite As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txt_Fsk As TextBox
    Friend WithEvents img_Fy As PictureBox
    Friend WithEvents etq_UnitSigma2 As Label
    Friend WithEvents cmb_Acier As ComboBox
    Friend WithEvents lbl_ClasseA As Label
    Friend WithEvents lbl_Acier As Label
    Friend WithEvents lbl_Beton As Label
    Friend WithEvents pan_Beton As Panel
    Friend WithEvents txt_Fck As TextBox
    Friend WithEvents img_Fck As PictureBox
    Friend WithEvents etq_UnitSigma1 As Label
    Friend WithEvents cmb_ClasseBetonEnrobage As ComboBox
    Friend WithEvents lbl_ClasseE As Label
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents Pan_ArmaLongi As Panel
    Friend WithEvents TableLayoutPanel_ArmaLongi As TableLayoutPanel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents lbl_Nombre As Label
    Friend WithEvents lbl_DiametreA As Label
    Friend WithEvents lbl_LitSelectionne As Label
    Friend WithEvents lbl_ArmaLongi As Label
    Friend WithEvents txt_Ecm As TextBox
    Friend WithEvents img_Ecm As PictureBox
    Friend WithEvents etq_UnitModule1 As Label
    Friend WithEvents etq_UnitDim5 As Label
    Friend WithEvents pan_Interieur As Panel
    Friend WithEvents lbl_Interieur As Label
    Friend WithEvents cmb_DiaInt As ComboBox
    Friend WithEvents cmb_NombreInt As ComboBox
    Friend WithEvents pan_Mileu As Panel
    Friend WithEvents lbl_Middle As Label
    Friend WithEvents cmb_DiaMil As ComboBox
    Friend WithEvents cmb_NombreMil As ComboBox
    Friend WithEvents pan_Exterieur As Panel
    Friend WithEvents lbl_Exterieur As Label
    Friend WithEvents cmb_NombreExt As ComboBox
    Friend WithEvents cmb_DiaExt As ComboBox
    Friend WithEvents txt_zArma As TextBox
    Friend WithEvents img_zArma As PictureBox
    Friend WithEvents etq_UnitDim6 As Label
    Friend WithEvents lbl_PositionZarma As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents chk_LitSup As CheckBox
    Friend WithEvents chk_LitInter As CheckBox
    Friend WithEvents chk_LitInf As CheckBox
    Friend WithEvents pan_InforArma As Panel
    Friend WithEvents txt_As As TextBox
    Friend WithEvents img_As As PictureBox
    Friend WithEvents etq_UnitDimCarre As Label
    Friend WithEvents lbl_Active As Label
    Friend WithEvents chk_ActiveInt As CheckBox
    Friend WithEvents chk_ActiveMil As CheckBox
    Friend WithEvents chk_ActiveExt As CheckBox
End Class
