<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Dalle
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Dalle))
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Dalle = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLpan_PartageV = New System.Windows.Forms.TableLayoutPanel()
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
        Me.txt_Hp = New System.Windows.Forms.TextBox()
        Me.lbl_HauteurHp = New System.Windows.Forms.Label()
        Me.etq_UnitDim4 = New System.Windows.Forms.Label()
        Me.img_Hp = New System.Windows.Forms.PictureBox()
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
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txt_Fsk = New System.Windows.Forms.TextBox()
        Me.img_Fy = New System.Windows.Forms.PictureBox()
        Me.etq_UnitSigma2 = New System.Windows.Forms.Label()
        Me.cmb_Acier = New System.Windows.Forms.ComboBox()
        Me.lbl_ClasseA = New System.Windows.Forms.Label()
        Me.lbl_Acier = New System.Windows.Forms.Label()
        Me.lbl_Armatures = New System.Windows.Forms.Label()
        Me.pan_Beton = New System.Windows.Forms.Panel()
        Me.chk_BetonLeger = New System.Windows.Forms.CheckBox()
        Me.txt_RhoC = New System.Windows.Forms.TextBox()
        Me.img_RhoC = New System.Windows.Forms.PictureBox()
        Me.etq_UnitRhoC = New System.Windows.Forms.Label()
        Me.cmb_ClasseBetonDalle = New System.Windows.Forms.ComboBox()
        Me.lbl_ClasseE = New System.Windows.Forms.Label()
        Me.lbl_Beton = New System.Windows.Forms.Label()
        Me.pan_Type = New System.Windows.Forms.Panel()
        Me.lbl_TypeDalle = New System.Windows.Forms.Label()
        Me.cmb_TypeDalle = New System.Windows.Forms.ComboBox()
        Me.lbl_General = New System.Windows.Forms.Label()
        Me.pan_Armatures = New System.Windows.Forms.Panel()
        Me.chk_Lit0 = New System.Windows.Forms.CheckBox()
        Me.pan_DonneesArma = New System.Windows.Forms.Panel()
        Me.lbl_LitNo = New System.Windows.Forms.Label()
        Me.txt_zs = New System.Windows.Forms.TextBox()
        Me.lbl_zs = New System.Windows.Forms.Label()
        Me.etq_UnitDim6 = New System.Windows.Forms.Label()
        Me.img_zs = New System.Windows.Forms.PictureBox()
        Me.txt_esp = New System.Windows.Forms.TextBox()
        Me.lbl_Espacement = New System.Windows.Forms.Label()
        Me.etq_UnitDim5 = New System.Windows.Forms.Label()
        Me.img_esp = New System.Windows.Forms.PictureBox()
        Me.txt_PhiS = New System.Windows.Forms.TextBox()
        Me.lbl_Diametre = New System.Windows.Forms.Label()
        Me.etq_UnitDim7 = New System.Windows.Forms.Label()
        Me.img_PhiS = New System.Windows.Forms.PictureBox()
        Me.chk_AjouterSupprimerLit = New System.Windows.Forms.CheckBox()
        Me.chk_Lit2 = New System.Windows.Forms.CheckBox()
        Me.chk_Lit1 = New System.Windows.Forms.CheckBox()
        Me.lbl_NoArma = New System.Windows.Forms.Label()
        Me.pan_Img = New System.Windows.Forms.Panel()
        Me.TLpan_Images = New System.Windows.Forms.TableLayoutPanel()
        Me.img_Dalle = New System.Windows.Forms.PictureBox()
        Me.pan_TauxArma = New System.Windows.Forms.Panel()
        Me.lbl_TauxArma = New System.Windows.Forms.Label()
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
        Me.imgList_BOArma = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolTipDalle = New System.Windows.Forms.ToolTip(Me.components)
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Dalle.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLpan_PartageV.SuspendLayout()
        Me.TLpan_Milieu.SuspendLayout()
        Me.pan_Bac.SuspendLayout()
        Me.pan_DispoConnecteur.SuspendLayout()
        Me.pan_Orientation.SuspendLayout()
        CType(Me.img_Hp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_ConfigurationNervures.SuspendLayout()
        CType(Me.img_Bac, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLpan_Gauche.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.img_Fy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Beton.SuspendLayout()
        CType(Me.img_RhoC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Type.SuspendLayout()
        Me.pan_Armatures.SuspendLayout()
        Me.pan_DonneesArma.SuspendLayout()
        CType(Me.img_zs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_esp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_PhiS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Img.SuspendLayout()
        Me.TLpan_Images.SuspendLayout()
        CType(Me.img_Dalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_TauxArma.SuspendLayout()
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
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(1015, 577)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(1015, 577)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 540)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1009, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(517, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(377, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_Dalle)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(1009, 531)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Dalle
        '
        Me.TLPan_Dalle.ColumnCount = 2
        Me.TLPan_Dalle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 501.0!))
        Me.TLPan_Dalle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Dalle.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Dalle.Controls.Add(Me.pan_Img, 1, 0)
        Me.TLPan_Dalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Dalle.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Dalle.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Dalle.Name = "TLPan_Dalle"
        Me.TLPan_Dalle.RowCount = 1
        Me.TLPan_Dalle.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Dalle.Size = New System.Drawing.Size(1009, 531)
        Me.TLPan_Dalle.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLpan_PartageV)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(501, 531)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLpan_PartageV
        '
        Me.TLpan_PartageV.ColumnCount = 2
        Me.TLpan_PartageV.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_PartageV.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLpan_PartageV.Controls.Add(Me.TLpan_Milieu, 1, 0)
        Me.TLpan_PartageV.Controls.Add(Me.TLpan_Gauche, 0, 0)
        Me.TLpan_PartageV.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLpan_PartageV.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_PartageV.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_PartageV.Name = "TLpan_PartageV"
        Me.TLpan_PartageV.RowCount = 1
        Me.TLpan_PartageV.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_PartageV.Size = New System.Drawing.Size(501, 530)
        Me.TLpan_PartageV.TabIndex = 0
        '
        'TLpan_Milieu
        '
        Me.TLpan_Milieu.ColumnCount = 1
        Me.TLpan_Milieu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Milieu.Controls.Add(Me.pan_Bac, 0, 1)
        Me.TLpan_Milieu.Controls.Add(Me.lbl_Bac, 0, 0)
        Me.TLpan_Milieu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Milieu.Location = New System.Drawing.Point(252, 0)
        Me.TLpan_Milieu.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.TLpan_Milieu.Name = "TLpan_Milieu"
        Me.TLpan_Milieu.RowCount = 2
        Me.TLpan_Milieu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Milieu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Milieu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Milieu.Size = New System.Drawing.Size(249, 530)
        Me.TLpan_Milieu.TabIndex = 2
        '
        'pan_Bac
        '
        Me.pan_Bac.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Bac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Bac.Controls.Add(Me.pan_DispoConnecteur)
        Me.pan_Bac.Controls.Add(Me.pan_Orientation)
        Me.pan_Bac.Controls.Add(Me.txt_Hp)
        Me.pan_Bac.Controls.Add(Me.lbl_HauteurHp)
        Me.pan_Bac.Controls.Add(Me.etq_UnitDim4)
        Me.pan_Bac.Controls.Add(Me.img_Hp)
        Me.pan_Bac.Controls.Add(Me.pan_ConfigurationNervures)
        Me.pan_Bac.Controls.Add(Me.img_Bac)
        Me.pan_Bac.Controls.Add(Me.btn_ModifierBac)
        Me.pan_Bac.Controls.Add(Me.lbl_BacNom)
        Me.pan_Bac.Controls.Add(Me.txt_BacNom)
        Me.pan_Bac.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Bac.Location = New System.Drawing.Point(0, 30)
        Me.pan_Bac.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Bac.Name = "pan_Bac"
        Me.pan_Bac.Size = New System.Drawing.Size(249, 500)
        Me.pan_Bac.TabIndex = 9
        '
        'pan_DispoConnecteur
        '
        Me.pan_DispoConnecteur.Controls.Add(Me.rdb_Preperce)
        Me.pan_DispoConnecteur.Controls.Add(Me.rdb_ATraversBac)
        Me.pan_DispoConnecteur.Controls.Add(Me.lbl_ConnectorThroughTheWeb)
        Me.pan_DispoConnecteur.Location = New System.Drawing.Point(3, 390)
        Me.pan_DispoConnecteur.Name = "pan_DispoConnecteur"
        Me.pan_DispoConnecteur.Size = New System.Drawing.Size(242, 74)
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
        Me.pan_Orientation.Location = New System.Drawing.Point(3, 198)
        Me.pan_Orientation.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Orientation.Name = "pan_Orientation"
        Me.pan_Orientation.Size = New System.Drawing.Size(242, 67)
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
        'txt_Hp
        '
        Me.txt_Hp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Hp.Location = New System.Drawing.Point(155, 141)
        Me.txt_Hp.Name = "txt_Hp"
        Me.txt_Hp.Size = New System.Drawing.Size(58, 20)
        Me.txt_Hp.TabIndex = 84
        '
        'lbl_HauteurHp
        '
        Me.lbl_HauteurHp.AutoSize = True
        Me.lbl_HauteurHp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_HauteurHp.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_HauteurHp.Location = New System.Drawing.Point(16, 144)
        Me.lbl_HauteurHp.Name = "lbl_HauteurHp"
        Me.lbl_HauteurHp.Size = New System.Drawing.Size(39, 13)
        Me.lbl_HauteurHp.TabIndex = 82
        Me.lbl_HauteurHp.Text = "Label1"
        Me.lbl_HauteurHp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_UnitDim4
        '
        Me.etq_UnitDim4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim4.AutoSize = True
        Me.etq_UnitDim4.Location = New System.Drawing.Point(218, 144)
        Me.etq_UnitDim4.Name = "etq_UnitDim4"
        Me.etq_UnitDim4.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim4.TabIndex = 83
        Me.etq_UnitDim4.Text = "mm"
        '
        'img_Hp
        '
        Me.img_Hp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Hp.Location = New System.Drawing.Point(117, 141)
        Me.img_Hp.Name = "img_Hp"
        Me.img_Hp.Size = New System.Drawing.Size(37, 20)
        Me.img_Hp.TabIndex = 85
        Me.img_Hp.TabStop = False
        '
        'pan_ConfigurationNervures
        '
        Me.pan_ConfigurationNervures.Controls.Add(Me.chk_L_PA1)
        Me.pan_ConfigurationNervures.Controls.Add(Me.chk_L_PA2)
        Me.pan_ConfigurationNervures.Controls.Add(Me.rtxt_Configuration)
        Me.pan_ConfigurationNervures.Controls.Add(Me.chk_T_PA3)
        Me.pan_ConfigurationNervures.Controls.Add(Me.chk_T_PA2)
        Me.pan_ConfigurationNervures.Controls.Add(Me.chk_T_PA1)
        Me.pan_ConfigurationNervures.Controls.Add(Me.lbl_BacConfiguration)
        Me.pan_ConfigurationNervures.Location = New System.Drawing.Point(3, 265)
        Me.pan_ConfigurationNervures.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ConfigurationNervures.Name = "pan_ConfigurationNervures"
        Me.pan_ConfigurationNervures.Size = New System.Drawing.Size(242, 125)
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
        Me.rtxt_Configuration.Size = New System.Drawing.Size(213, 41)
        Me.rtxt_Configuration.TabIndex = 3
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
        Me.img_Bac.Size = New System.Drawing.Size(242, 104)
        Me.img_Bac.TabIndex = 80
        Me.img_Bac.TabStop = False
        '
        'btn_ModifierBac
        '
        Me.btn_ModifierBac.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_ModifierBac.Location = New System.Drawing.Point(3, 167)
        Me.btn_ModifierBac.Name = "btn_ModifierBac"
        Me.btn_ModifierBac.Size = New System.Drawing.Size(242, 27)
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
        Me.txt_BacNom.Location = New System.Drawing.Point(89, 9)
        Me.txt_BacNom.Name = "txt_BacNom"
        Me.txt_BacNom.Size = New System.Drawing.Size(156, 20)
        Me.txt_BacNom.TabIndex = 72
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
        Me.lbl_Bac.Size = New System.Drawing.Size(249, 30)
        Me.lbl_Bac.TabIndex = 2
        Me.lbl_Bac.Text = "lbl_Bac"
        Me.lbl_Bac.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.Panel2, 0, 7)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_Acier, 0, 6)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_Armatures, 0, 4)
        Me.TLpan_Gauche.Controls.Add(Me.pan_Beton, 0, 3)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_Beton, 0, 2)
        Me.TLpan_Gauche.Controls.Add(Me.pan_Type, 0, 1)
        Me.TLpan_Gauche.Controls.Add(Me.lbl_General, 0, 0)
        Me.TLpan_Gauche.Controls.Add(Me.pan_Armatures, 0, 5)
        Me.TLpan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Gauche.Name = "TLpan_Gauche"
        Me.TLpan_Gauche.RowCount = 8
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Size = New System.Drawing.Size(251, 530)
        Me.TLpan_Gauche.TabIndex = 0
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
        Me.Panel2.Location = New System.Drawing.Point(0, 467)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(251, 63)
        Me.Panel2.TabIndex = 15
        '
        'txt_Fsk
        '
        Me.txt_Fsk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Fsk.Location = New System.Drawing.Point(149, 33)
        Me.txt_Fsk.Name = "txt_Fsk"
        Me.txt_Fsk.Size = New System.Drawing.Size(58, 20)
        Me.txt_Fsk.TabIndex = 59
        '
        'img_Fy
        '
        Me.img_Fy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Fy.Location = New System.Drawing.Point(93, 33)
        Me.img_Fy.Name = "img_Fy"
        Me.img_Fy.Size = New System.Drawing.Size(57, 20)
        Me.img_Fy.TabIndex = 60
        Me.img_Fy.TabStop = False
        '
        'etq_UnitSigma2
        '
        Me.etq_UnitSigma2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitSigma2.AutoSize = True
        Me.etq_UnitSigma2.Location = New System.Drawing.Point(213, 36)
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
        Me.cmb_Acier.Size = New System.Drawing.Size(112, 21)
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
        Me.lbl_Acier.Location = New System.Drawing.Point(0, 437)
        Me.lbl_Acier.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Acier.Name = "lbl_Acier"
        Me.lbl_Acier.Size = New System.Drawing.Size(251, 30)
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
        Me.lbl_Armatures.Location = New System.Drawing.Point(0, 257)
        Me.lbl_Armatures.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Armatures.Name = "lbl_Armatures"
        Me.lbl_Armatures.Size = New System.Drawing.Size(251, 30)
        Me.lbl_Armatures.TabIndex = 10
        Me.lbl_Armatures.Text = "lbl_Armatures"
        Me.lbl_Armatures.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.pan_Beton.Location = New System.Drawing.Point(0, 172)
        Me.pan_Beton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Beton.Name = "pan_Beton"
        Me.pan_Beton.Size = New System.Drawing.Size(251, 84)
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
        Me.txt_RhoC.Location = New System.Drawing.Point(128, 56)
        Me.txt_RhoC.Name = "txt_RhoC"
        Me.txt_RhoC.Size = New System.Drawing.Size(58, 20)
        Me.txt_RhoC.TabIndex = 62
        '
        'img_RhoC
        '
        Me.img_RhoC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_RhoC.Location = New System.Drawing.Point(83, 56)
        Me.img_RhoC.Name = "img_RhoC"
        Me.img_RhoC.Size = New System.Drawing.Size(46, 20)
        Me.img_RhoC.TabIndex = 63
        Me.img_RhoC.TabStop = False
        '
        'etq_UnitRhoC
        '
        Me.etq_UnitRhoC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitRhoC.AutoSize = True
        Me.etq_UnitRhoC.Location = New System.Drawing.Point(192, 59)
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
        Me.cmb_ClasseBetonDalle.Size = New System.Drawing.Size(111, 21)
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
        Me.lbl_Beton.Location = New System.Drawing.Point(0, 142)
        Me.lbl_Beton.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Beton.Name = "lbl_Beton"
        Me.lbl_Beton.Size = New System.Drawing.Size(251, 30)
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
        Me.pan_Type.Size = New System.Drawing.Size(251, 111)
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
        Me.cmb_TypeDalle.Size = New System.Drawing.Size(143, 21)
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
        Me.lbl_General.Size = New System.Drawing.Size(251, 30)
        Me.lbl_General.TabIndex = 2
        Me.lbl_General.Text = "lbl_General"
        Me.lbl_General.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.pan_Armatures.Location = New System.Drawing.Point(0, 287)
        Me.pan_Armatures.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Armatures.Name = "pan_Armatures"
        Me.pan_Armatures.Size = New System.Drawing.Size(251, 149)
        Me.pan_Armatures.TabIndex = 11
        '
        'chk_Lit0
        '
        Me.chk_Lit0.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Lit0.Image = CType(resources.GetObject("chk_Lit0.Image"), System.Drawing.Image)
        Me.chk_Lit0.Location = New System.Drawing.Point(2, 4)
        Me.chk_Lit0.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_Lit0.Name = "chk_Lit0"
        Me.chk_Lit0.Size = New System.Drawing.Size(44, 44)
        Me.chk_Lit0.TabIndex = 6
        Me.chk_Lit0.UseVisualStyleBackColor = True
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
        Me.pan_DonneesArma.Location = New System.Drawing.Point(1, 48)
        Me.pan_DonneesArma.Name = "pan_DonneesArma"
        Me.pan_DonneesArma.Size = New System.Drawing.Size(246, 100)
        Me.pan_DonneesArma.TabIndex = 5
        '
        'lbl_LitNo
        '
        Me.lbl_LitNo.AutoSize = True
        Me.lbl_LitNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LitNo.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_LitNo.Location = New System.Drawing.Point(12, 6)
        Me.lbl_LitNo.Name = "lbl_LitNo"
        Me.lbl_LitNo.Size = New System.Drawing.Size(48, 13)
        Me.lbl_LitNo.TabIndex = 85
        Me.lbl_LitNo.Text = "lbl_LitNo"
        Me.lbl_LitNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_zs
        '
        Me.txt_zs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_zs.Location = New System.Drawing.Point(148, 75)
        Me.txt_zs.Name = "txt_zs"
        Me.txt_zs.Size = New System.Drawing.Size(58, 20)
        Me.txt_zs.TabIndex = 83
        '
        'lbl_zs
        '
        Me.lbl_zs.AutoSize = True
        Me.lbl_zs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_zs.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_zs.Location = New System.Drawing.Point(12, 78)
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
        Me.etq_UnitDim6.Location = New System.Drawing.Point(212, 78)
        Me.etq_UnitDim6.Name = "etq_UnitDim6"
        Me.etq_UnitDim6.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim6.TabIndex = 82
        Me.etq_UnitDim6.Text = "mm"
        '
        'img_zs
        '
        Me.img_zs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_zs.Location = New System.Drawing.Point(112, 75)
        Me.img_zs.Name = "img_zs"
        Me.img_zs.Size = New System.Drawing.Size(37, 20)
        Me.img_zs.TabIndex = 84
        Me.img_zs.TabStop = False
        '
        'txt_esp
        '
        Me.txt_esp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_esp.Location = New System.Drawing.Point(148, 51)
        Me.txt_esp.Name = "txt_esp"
        Me.txt_esp.Size = New System.Drawing.Size(58, 20)
        Me.txt_esp.TabIndex = 79
        '
        'lbl_Espacement
        '
        Me.lbl_Espacement.AutoSize = True
        Me.lbl_Espacement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Espacement.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Espacement.Location = New System.Drawing.Point(12, 54)
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
        Me.etq_UnitDim5.Location = New System.Drawing.Point(212, 54)
        Me.etq_UnitDim5.Name = "etq_UnitDim5"
        Me.etq_UnitDim5.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim5.TabIndex = 78
        Me.etq_UnitDim5.Text = "mm"
        '
        'img_esp
        '
        Me.img_esp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_esp.Location = New System.Drawing.Point(112, 51)
        Me.img_esp.Name = "img_esp"
        Me.img_esp.Size = New System.Drawing.Size(37, 20)
        Me.img_esp.TabIndex = 80
        Me.img_esp.TabStop = False
        '
        'txt_PhiS
        '
        Me.txt_PhiS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PhiS.Location = New System.Drawing.Point(148, 27)
        Me.txt_PhiS.Name = "txt_PhiS"
        Me.txt_PhiS.Size = New System.Drawing.Size(58, 20)
        Me.txt_PhiS.TabIndex = 75
        '
        'lbl_Diametre
        '
        Me.lbl_Diametre.AutoSize = True
        Me.lbl_Diametre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Diametre.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Diametre.Location = New System.Drawing.Point(12, 30)
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
        Me.etq_UnitDim7.Location = New System.Drawing.Point(212, 30)
        Me.etq_UnitDim7.Name = "etq_UnitDim7"
        Me.etq_UnitDim7.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim7.TabIndex = 74
        Me.etq_UnitDim7.Text = "mm"
        '
        'img_PhiS
        '
        Me.img_PhiS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PhiS.Location = New System.Drawing.Point(112, 27)
        Me.img_PhiS.Name = "img_PhiS"
        Me.img_PhiS.Size = New System.Drawing.Size(37, 20)
        Me.img_PhiS.TabIndex = 76
        Me.img_PhiS.TabStop = False
        '
        'chk_AjouterSupprimerLit
        '
        Me.chk_AjouterSupprimerLit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chk_AjouterSupprimerLit.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_AjouterSupprimerLit.Image = CType(resources.GetObject("chk_AjouterSupprimerLit.Image"), System.Drawing.Image)
        Me.chk_AjouterSupprimerLit.Location = New System.Drawing.Point(199, 4)
        Me.chk_AjouterSupprimerLit.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_AjouterSupprimerLit.Name = "chk_AjouterSupprimerLit"
        Me.chk_AjouterSupprimerLit.Size = New System.Drawing.Size(44, 44)
        Me.chk_AjouterSupprimerLit.TabIndex = 4
        Me.chk_AjouterSupprimerLit.UseVisualStyleBackColor = True
        '
        'chk_Lit2
        '
        Me.chk_Lit2.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Lit2.Image = CType(resources.GetObject("chk_Lit2.Image"), System.Drawing.Image)
        Me.chk_Lit2.Location = New System.Drawing.Point(90, 4)
        Me.chk_Lit2.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_Lit2.Name = "chk_Lit2"
        Me.chk_Lit2.Size = New System.Drawing.Size(44, 44)
        Me.chk_Lit2.TabIndex = 3
        Me.chk_Lit2.UseVisualStyleBackColor = True
        '
        'chk_Lit1
        '
        Me.chk_Lit1.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Lit1.Image = CType(resources.GetObject("chk_Lit1.Image"), System.Drawing.Image)
        Me.chk_Lit1.Location = New System.Drawing.Point(46, 4)
        Me.chk_Lit1.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_Lit1.Name = "chk_Lit1"
        Me.chk_Lit1.Size = New System.Drawing.Size(44, 44)
        Me.chk_Lit1.TabIndex = 2
        Me.chk_Lit1.UseVisualStyleBackColor = True
        '
        'lbl_NoArma
        '
        Me.lbl_NoArma.Location = New System.Drawing.Point(2, 48)
        Me.lbl_NoArma.Name = "lbl_NoArma"
        Me.lbl_NoArma.Size = New System.Drawing.Size(246, 57)
        Me.lbl_NoArma.TabIndex = 7
        Me.lbl_NoArma.Text = "lbl_NoArma"
        Me.lbl_NoArma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Img
        '
        Me.pan_Img.Controls.Add(Me.TLpan_Images)
        Me.pan_Img.Controls.Add(Me.pan_EpaisseurMixte)
        Me.pan_Img.Controls.Add(Me.pan_Epaisseur)
        Me.pan_Img.Controls.Add(Me.pan_Predalle)
        Me.pan_Img.Controls.Add(Me.pan_Renformis)
        Me.pan_Img.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Img.Location = New System.Drawing.Point(502, 0)
        Me.pan_Img.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Img.Name = "pan_Img"
        Me.pan_Img.Size = New System.Drawing.Size(507, 531)
        Me.pan_Img.TabIndex = 1
        '
        'TLpan_Images
        '
        Me.TLpan_Images.ColumnCount = 1
        Me.TLpan_Images.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Images.Controls.Add(Me.img_Dalle, 0, 0)
        Me.TLpan_Images.Controls.Add(Me.pan_TauxArma, 0, 1)
        Me.TLpan_Images.Location = New System.Drawing.Point(15, 217)
        Me.TLpan_Images.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.TLpan_Images.Name = "TLpan_Images"
        Me.TLpan_Images.RowCount = 2
        Me.TLpan_Images.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Images.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Images.Size = New System.Drawing.Size(384, 204)
        Me.TLpan_Images.TabIndex = 89
        '
        'img_Dalle
        '
        Me.img_Dalle.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Dalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Dalle.Location = New System.Drawing.Point(0, 0)
        Me.img_Dalle.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.img_Dalle.Name = "img_Dalle"
        Me.img_Dalle.Size = New System.Drawing.Size(100, 50)
        Me.img_Dalle.TabIndex = 3
        Me.img_Dalle.TabStop = False
        '
        'pan_TauxArma
        '
        Me.pan_TauxArma.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_TauxArma.Controls.Add(Me.lbl_TauxArma)
        Me.pan_TauxArma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_TauxArma.Location = New System.Drawing.Point(0, 174)
        Me.pan_TauxArma.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_TauxArma.Name = "pan_TauxArma"
        Me.pan_TauxArma.Size = New System.Drawing.Size(384, 30)
        Me.pan_TauxArma.TabIndex = 4
        '
        'lbl_TauxArma
        '
        Me.lbl_TauxArma.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.lbl_TauxArma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_TauxArma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_TauxArma.Location = New System.Drawing.Point(0, 0)
        Me.lbl_TauxArma.Margin = New System.Windows.Forms.Padding(0, 0, 0, 2)
        Me.lbl_TauxArma.Name = "lbl_TauxArma"
        Me.lbl_TauxArma.Size = New System.Drawing.Size(384, 30)
        Me.lbl_TauxArma.TabIndex = 0
        Me.lbl_TauxArma.Text = "lbl_TauxArma"
        Me.lbl_TauxArma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.pan_EpaisseurMixte.Location = New System.Drawing.Point(264, 123)
        Me.pan_EpaisseurMixte.Name = "pan_EpaisseurMixte"
        Me.pan_EpaisseurMixte.Size = New System.Drawing.Size(237, 52)
        Me.pan_EpaisseurMixte.TabIndex = 88
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
        Me.pan_Epaisseur.Location = New System.Drawing.Point(264, 65)
        Me.pan_Epaisseur.Name = "pan_Epaisseur"
        Me.pan_Epaisseur.Size = New System.Drawing.Size(237, 25)
        Me.pan_Epaisseur.TabIndex = 87
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
        Me.pan_Predalle.Location = New System.Drawing.Point(264, 10)
        Me.pan_Predalle.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Predalle.Name = "pan_Predalle"
        Me.pan_Predalle.Size = New System.Drawing.Size(237, 52)
        Me.pan_Predalle.TabIndex = 86
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
        Me.pan_Renformis.Location = New System.Drawing.Point(264, 93)
        Me.pan_Renformis.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Renformis.Name = "pan_Renformis"
        Me.pan_Renformis.Size = New System.Drawing.Size(237, 27)
        Me.pan_Renformis.TabIndex = 83
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
        'imgList_BOArma
        '
        Me.imgList_BOArma.ImageStream = CType(resources.GetObject("imgList_BOArma.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList_BOArma.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList_BOArma.Images.SetKeyName(0, "Ajouter")
        Me.imgList_BOArma.Images.SetKeyName(1, "Supprimer")
        '
        'Frm_Dalle
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(1015, 577)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Dalle"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Dalle"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Dalle.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLpan_PartageV.ResumeLayout(False)
        Me.TLpan_Milieu.ResumeLayout(False)
        Me.TLpan_Milieu.PerformLayout()
        Me.pan_Bac.ResumeLayout(False)
        Me.pan_Bac.PerformLayout()
        Me.pan_DispoConnecteur.ResumeLayout(False)
        Me.pan_DispoConnecteur.PerformLayout()
        Me.pan_Orientation.ResumeLayout(False)
        Me.pan_Orientation.PerformLayout()
        CType(Me.img_Hp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_ConfigurationNervures.ResumeLayout(False)
        Me.pan_ConfigurationNervures.PerformLayout()
        CType(Me.img_Bac, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.img_Fy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Beton.ResumeLayout(False)
        Me.pan_Beton.PerformLayout()
        CType(Me.img_RhoC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Type.ResumeLayout(False)
        Me.pan_Type.PerformLayout()
        Me.pan_Armatures.ResumeLayout(False)
        Me.pan_DonneesArma.ResumeLayout(False)
        Me.pan_DonneesArma.PerformLayout()
        CType(Me.img_zs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_esp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_PhiS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Img.ResumeLayout(False)
        Me.TLpan_Images.ResumeLayout(False)
        CType(Me.img_Dalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_TauxArma.ResumeLayout(False)
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

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Dalle As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLpan_PartageV As TableLayoutPanel
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents TLpan_Milieu As TableLayoutPanel
    Friend WithEvents pan_Bac As Panel
    Friend WithEvents txt_Hp As TextBox
    Friend WithEvents lbl_HauteurHp As Label
    Friend WithEvents etq_UnitDim4 As Label
    Friend WithEvents img_Hp As PictureBox
    Friend WithEvents pan_ConfigurationNervures As Panel
    Friend WithEvents chk_L_PA1 As CheckBox
    Friend WithEvents chk_L_PA2 As CheckBox
    Friend WithEvents rtxt_Configuration As RichTextBox
    Friend WithEvents chk_T_PA3 As CheckBox
    Friend WithEvents chk_T_PA2 As CheckBox
    Friend WithEvents chk_T_PA1 As CheckBox
    Friend WithEvents img_Bac As PictureBox
    Friend WithEvents lbl_BacConfiguration As Label
    Friend WithEvents btn_ModifierBac As Button
    Friend WithEvents lbl_BacNom As Label
    Friend WithEvents txt_BacNom As TextBox
    Friend WithEvents lbl_Bac As Label
    Friend WithEvents lbl_General As Label
    Friend WithEvents pan_Orientation As Panel
    Friend WithEvents lbl_BacOrientation As Label
    Friend WithEvents rdb_BacPerpendiculaire As RadioButton
    Friend WithEvents rdb_BacParallele As RadioButton
    Friend WithEvents pan_Type As Panel
    Friend WithEvents pan_Renformis As Panel
    Friend WithEvents txt_Hh As TextBox
    Friend WithEvents lbl_Renformis As Label
    Friend WithEvents Img_Hh As PictureBox
    Friend WithEvents etq_UnitDim3 As Label
    Friend WithEvents lbl_TypeDalle As Label
    Friend WithEvents cmb_TypeDalle As ComboBox
    Friend WithEvents lbl_Beton As Label
    Friend WithEvents pan_Beton As Panel
    Friend WithEvents txt_RhoC As TextBox
    Friend WithEvents img_RhoC As PictureBox
    Friend WithEvents etq_UnitRhoC As Label
    Friend WithEvents cmb_ClasseBetonDalle As ComboBox
    Friend WithEvents lbl_ClasseE As Label
    Friend WithEvents lbl_Armatures As Label
    Friend WithEvents pan_Armatures As Panel
    Friend WithEvents lbl_Acier As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txt_Fsk As TextBox
    Friend WithEvents img_Fy As PictureBox
    Friend WithEvents etq_UnitSigma2 As Label
    Friend WithEvents cmb_Acier As ComboBox
    Friend WithEvents lbl_ClasseA As Label
    Friend WithEvents chk_Lit1 As CheckBox
    Friend WithEvents chk_Lit2 As CheckBox
    Friend WithEvents chk_AjouterSupprimerLit As CheckBox
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
    Friend WithEvents img_Dalle As PictureBox
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents imgList_BOArma As ImageList
    Friend WithEvents ToolTipDalle As ToolTip
    Friend WithEvents pan_Predalle As Panel
    Friend WithEvents txt_EpJoint As TextBox
    Friend WithEvents lbl_EpJoint As Label
    Friend WithEvents img_EpJoint As PictureBox
    Friend WithEvents etq_UnitDim9 As Label
    Friend WithEvents txt_EpPredalle As TextBox
    Friend WithEvents lbl_EpPreDalle As Label
    Friend WithEvents img_EpPredalle As PictureBox
    Friend WithEvents etq_UnitDim8 As Label
    Friend WithEvents pan_DispoConnecteur As Panel
    Friend WithEvents rdb_Preperce As RadioButton
    Friend WithEvents rdb_ATraversBac As RadioButton
    Friend WithEvents lbl_ConnectorThroughTheWeb As Label
    Friend WithEvents chk_BetonLeger As CheckBox
    Friend WithEvents pan_Img As Panel
    Friend WithEvents pan_Epaisseur As Panel
    Friend WithEvents txt_Hd As TextBox
    Friend WithEvents lbl_Epaisseur As Label
    Friend WithEvents etq_UnitDim2 As Label
    Friend WithEvents Img_Hd As PictureBox
    Friend WithEvents pan_EpaisseurMixte As Panel
    Friend WithEvents txt_Tc As TextBox
    Friend WithEvents etq_UnitDim11 As Label
    Friend WithEvents txt_Td2 As TextBox
    Friend WithEvents img_Tc As PictureBox
    Friend WithEvents lbl_EpaisseurM As Label
    Friend WithEvents etq_UnitDim10 As Label
    Friend WithEvents img_Td2 As PictureBox
    Friend WithEvents rdb_EpPleine As RadioButton
    Friend WithEvents rdb_EpTotale As RadioButton
    Friend WithEvents TLpan_Images As TableLayoutPanel
    Friend WithEvents pan_TauxArma As Panel
    Friend WithEvents lbl_TauxArma As Label
    Friend WithEvents chk_Lit0 As CheckBox
    Friend WithEvents lbl_NoArma As Label
End Class
