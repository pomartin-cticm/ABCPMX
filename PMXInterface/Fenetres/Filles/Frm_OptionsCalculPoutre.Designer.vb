<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_OptionsCalculPoutre
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_OptionsCalculPoutre))
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Portees = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Parametres = New System.Windows.Forms.Panel()
        Me.etq_UnitG = New System.Windows.Forms.Label()
        Me.img_G = New System.Windows.Forms.PictureBox()
        Me.cmb_GraviteG = New System.Windows.Forms.ComboBox()
        Me.lbl_GraviteG = New System.Windows.Forms.Label()
        Me.lbl_CadreParametres = New System.Windows.Forms.Label()
        Me.pan_Sections = New System.Windows.Forms.Panel()
        Me.chk_EnrobagePropSection = New System.Windows.Forms.CheckBox()
        Me.chk_ArmaComprimees = New System.Windows.Forms.CheckBox()
        Me.chk_LargeursPartipantesSimples = New System.Windows.Forms.CheckBox()
        Me.lbl_CadreSections = New System.Windows.Forms.Label()
        Me.lbl_CadreBeton = New System.Windows.Forms.Label()
        Me.pan_OptionsELS = New System.Windows.Forms.Panel()
        Me.txt_CtrFlecheFab = New System.Windows.Forms.TextBox()
        Me.etq_UnitDimension2 = New System.Windows.Forms.Label()
        Me.img_CtrFlecheFab = New System.Windows.Forms.PictureBox()
        Me.chk_CtrFlecheFab = New System.Windows.Forms.CheckBox()
        Me.chk_Psi2LongTerme = New System.Windows.Forms.CheckBox()
        Me.etq_UnitLargeurF = New System.Windows.Forms.Label()
        Me.cmb_Wk = New System.Windows.Forms.ComboBox()
        Me.img_Wk = New System.Windows.Forms.PictureBox()
        Me.lbl_LargeurFissure = New System.Windows.Forms.Label()
        Me.chk_MaitriseFissuration = New System.Windows.Forms.CheckBox()
        Me.lbl_StudDeflection = New System.Windows.Forms.Label()
        Me.etq_UnitDimension1 = New System.Windows.Forms.Label()
        Me.txt_Se = New System.Windows.Forms.TextBox()
        Me.img_se = New System.Windows.Forms.PictureBox()
        Me.chk_FlechesETA = New System.Windows.Forms.CheckBox()
        Me.lbl_CombinationVibration = New System.Windows.Forms.Label()
        Me.lbl_CadreELS = New System.Windows.Forms.Label()
        Me.pan_OptionsELU = New System.Windows.Forms.Panel()
        Me.rdb_ElasticDesignClasse3 = New System.Windows.Forms.RadioButton()
        Me.rdb_ElasticDesignVM = New System.Windows.Forms.RadioButton()
        Me.rdb_NormalDesign = New System.Windows.Forms.RadioButton()
        Me.txt_eta = New System.Windows.Forms.TextBox()
        Me.lbl_eta = New System.Windows.Forms.Label()
        Me.img_eta = New System.Windows.Forms.PictureBox()
        Me.lbl_CadreELU = New System.Windows.Forms.Label()
        Me.lbl_CadreNorm = New System.Windows.Forms.Label()
        Me.pan_Norm = New System.Windows.Forms.Panel()
        Me.cmb_Norme = New System.Windows.Forms.ComboBox()
        Me.lbl_Norme = New System.Windows.Forms.Label()
        Me.pan_Beton = New System.Windows.Forms.Panel()
        Me.img_AgeT = New System.Windows.Forms.PictureBox()
        Me.etq_UnitJour7 = New System.Windows.Forms.Label()
        Me.txt_AgeT = New System.Windows.Forms.TextBox()
        Me.lbl_AgeT = New System.Windows.Forms.Label()
        Me.lbl_SH = New System.Windows.Forms.Label()
        Me.lbl_G2 = New System.Windows.Forms.Label()
        Me.lbl_G1 = New System.Windows.Forms.Label()
        Me.lbl_TimeT0 = New System.Windows.Forms.Label()
        Me.img_T0SH = New System.Windows.Forms.PictureBox()
        Me.img_T0G2 = New System.Windows.Forms.PictureBox()
        Me.img_T0G1 = New System.Windows.Forms.PictureBox()
        Me.etq_UnitJour6 = New System.Windows.Forms.Label()
        Me.etq_UnitJour4 = New System.Windows.Forms.Label()
        Me.etq_UnitJour2 = New System.Windows.Forms.Label()
        Me.etq_UnitJour5 = New System.Windows.Forms.Label()
        Me.etq_UnitJour3 = New System.Windows.Forms.Label()
        Me.etq_UnitJour1 = New System.Windows.Forms.Label()
        Me.txt_t0SHDalle = New System.Windows.Forms.TextBox()
        Me.txt_t0SHEnrob = New System.Windows.Forms.TextBox()
        Me.txt_t0G2Dalle = New System.Windows.Forms.TextBox()
        Me.txt_t0G2Enrob = New System.Windows.Forms.TextBox()
        Me.txt_t0G1Dalle = New System.Windows.Forms.TextBox()
        Me.txt_t0G1Enrob = New System.Windows.Forms.TextBox()
        Me.lbl_Enrobage = New System.Windows.Forms.Label()
        Me.lbl_Dalle = New System.Windows.Forms.Label()
        Me.etq_UnitModuleY = New System.Windows.Forms.Label()
        Me.txt_Es = New System.Windows.Forms.TextBox()
        Me.img_Es = New System.Windows.Forms.PictureBox()
        Me.lbl_ArmaYoung = New System.Windows.Forms.Label()
        Me.chk_RetraitEnrobage = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.etq_UnitEpsilon = New System.Windows.Forms.Label()
        Me.txt_EpsilonSh = New System.Windows.Forms.TextBox()
        Me.img_EpsilonSh = New System.Windows.Forms.PictureBox()
        Me.img_RH = New System.Windows.Forms.PictureBox()
        Me.lbl_Shrinkage = New System.Windows.Forms.Label()
        Me.cmb_RH = New System.Windows.Forms.ComboBox()
        Me.lbl_RH = New System.Windows.Forms.Label()
        Me.lbl_BetonMessage = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.img_info = New System.Windows.Forms.PictureBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Portees.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_Parametres.SuspendLayout()
        CType(Me.img_G, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Sections.SuspendLayout()
        Me.pan_OptionsELS.SuspendLayout()
        CType(Me.img_CtrFlecheFab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Wk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_se, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_OptionsELU.SuspendLayout()
        CType(Me.img_eta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Norm.SuspendLayout()
        Me.pan_Beton.SuspendLayout()
        CType(Me.img_AgeT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_T0SH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_T0G2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_T0G1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Es, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_EpsilonSh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_RH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_info, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(465, 543)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(465, 543)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 506)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(459, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(242, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(102, 3)
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
        Me.pan_Main.Size = New System.Drawing.Size(459, 497)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Portees
        '
        Me.TLPan_Portees.ColumnCount = 1
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Portees.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Portees.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Portees.Name = "TLPan_Portees"
        Me.TLPan_Portees.RowCount = 1
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Size = New System.Drawing.Size(459, 497)
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
        Me.pan_Gauche.Size = New System.Drawing.Size(459, 497)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.pan_Parametres, 0, 11)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreParametres, 0, 10)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Sections, 0, 9)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreSections, 0, 8)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreBeton, 0, 6)
        Me.TLPan_Gauche.Controls.Add(Me.pan_OptionsELS, 0, 5)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreELS, 0, 4)
        Me.TLPan_Gauche.Controls.Add(Me.pan_OptionsELU, 0, 3)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreELU, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreNorm, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Norm, 0, 1)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Beton, 0, 7)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 13
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 240.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(442, 964)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'pan_Parametres
        '
        Me.pan_Parametres.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Parametres.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Parametres.Controls.Add(Me.etq_UnitG)
        Me.pan_Parametres.Controls.Add(Me.img_G)
        Me.pan_Parametres.Controls.Add(Me.cmb_GraviteG)
        Me.pan_Parametres.Controls.Add(Me.lbl_GraviteG)
        Me.pan_Parametres.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Parametres.Location = New System.Drawing.Point(0, 905)
        Me.pan_Parametres.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Parametres.Name = "pan_Parametres"
        Me.pan_Parametres.Size = New System.Drawing.Size(442, 49)
        Me.pan_Parametres.TabIndex = 10
        '
        'etq_UnitG
        '
        Me.etq_UnitG.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitG.AutoSize = True
        Me.etq_UnitG.Location = New System.Drawing.Point(338, 16)
        Me.etq_UnitG.Name = "etq_UnitG"
        Me.etq_UnitG.Size = New System.Drawing.Size(28, 13)
        Me.etq_UnitG.TabIndex = 119
        Me.etq_UnitG.Text = "GPa"
        '
        'img_G
        '
        Me.img_G.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_G.Location = New System.Drawing.Point(192, 14)
        Me.img_G.Name = "img_G"
        Me.img_G.Size = New System.Drawing.Size(46, 20)
        Me.img_G.TabIndex = 112
        Me.img_G.TabStop = False
        '
        'cmb_GraviteG
        '
        Me.cmb_GraviteG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_GraviteG.FormattingEnabled = True
        Me.cmb_GraviteG.Location = New System.Drawing.Point(241, 13)
        Me.cmb_GraviteG.Name = "cmb_GraviteG"
        Me.cmb_GraviteG.Size = New System.Drawing.Size(91, 21)
        Me.cmb_GraviteG.TabIndex = 111
        '
        'lbl_GraviteG
        '
        Me.lbl_GraviteG.AutoSize = True
        Me.lbl_GraviteG.Location = New System.Drawing.Point(8, 16)
        Me.lbl_GraviteG.Name = "lbl_GraviteG"
        Me.lbl_GraviteG.Size = New System.Drawing.Size(65, 13)
        Me.lbl_GraviteG.TabIndex = 110
        Me.lbl_GraviteG.Text = "lbl_GraviteG"
        '
        'lbl_CadreParametres
        '
        Me.lbl_CadreParametres.AutoSize = True
        Me.lbl_CadreParametres.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreParametres.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreParametres.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreParametres.Location = New System.Drawing.Point(0, 875)
        Me.lbl_CadreParametres.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreParametres.Name = "lbl_CadreParametres"
        Me.lbl_CadreParametres.Size = New System.Drawing.Size(442, 30)
        Me.lbl_CadreParametres.TabIndex = 9
        Me.lbl_CadreParametres.Text = "lbl_CadreParameters (10)"
        Me.lbl_CadreParametres.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Sections
        '
        Me.pan_Sections.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Sections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Sections.Controls.Add(Me.chk_EnrobagePropSection)
        Me.pan_Sections.Controls.Add(Me.chk_ArmaComprimees)
        Me.pan_Sections.Controls.Add(Me.chk_LargeursPartipantesSimples)
        Me.pan_Sections.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Sections.Location = New System.Drawing.Point(0, 785)
        Me.pan_Sections.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Sections.Name = "pan_Sections"
        Me.pan_Sections.Size = New System.Drawing.Size(442, 89)
        Me.pan_Sections.TabIndex = 8
        '
        'chk_EnrobagePropSection
        '
        Me.chk_EnrobagePropSection.AutoSize = True
        Me.chk_EnrobagePropSection.Location = New System.Drawing.Point(12, 58)
        Me.chk_EnrobagePropSection.Name = "chk_EnrobagePropSection"
        Me.chk_EnrobagePropSection.Size = New System.Drawing.Size(154, 17)
        Me.chk_EnrobagePropSection.TabIndex = 117
        Me.chk_EnrobagePropSection.Text = "chk_EnrobagePropSection"
        Me.chk_EnrobagePropSection.UseVisualStyleBackColor = True
        '
        'chk_ArmaComprimees
        '
        Me.chk_ArmaComprimees.AutoSize = True
        Me.chk_ArmaComprimees.Location = New System.Drawing.Point(12, 35)
        Me.chk_ArmaComprimees.Name = "chk_ArmaComprimees"
        Me.chk_ArmaComprimees.Size = New System.Drawing.Size(131, 17)
        Me.chk_ArmaComprimees.TabIndex = 116
        Me.chk_ArmaComprimees.Text = "chk_ArmaComprimees"
        Me.chk_ArmaComprimees.UseVisualStyleBackColor = True
        '
        'chk_LargeursPartipantesSimples
        '
        Me.chk_LargeursPartipantesSimples.AutoSize = True
        Me.chk_LargeursPartipantesSimples.Location = New System.Drawing.Point(12, 12)
        Me.chk_LargeursPartipantesSimples.Name = "chk_LargeursPartipantesSimples"
        Me.chk_LargeursPartipantesSimples.Size = New System.Drawing.Size(180, 17)
        Me.chk_LargeursPartipantesSimples.TabIndex = 115
        Me.chk_LargeursPartipantesSimples.Text = "chk_LargeursPartipantesSimples"
        Me.chk_LargeursPartipantesSimples.UseVisualStyleBackColor = True
        '
        'lbl_CadreSections
        '
        Me.lbl_CadreSections.AutoSize = True
        Me.lbl_CadreSections.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreSections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreSections.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreSections.Location = New System.Drawing.Point(0, 755)
        Me.lbl_CadreSections.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreSections.Name = "lbl_CadreSections"
        Me.lbl_CadreSections.Size = New System.Drawing.Size(442, 30)
        Me.lbl_CadreSections.TabIndex = 6
        Me.lbl_CadreSections.Text = "lbl_CadreSections (8)"
        Me.lbl_CadreSections.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_CadreBeton
        '
        Me.lbl_CadreBeton.AutoSize = True
        Me.lbl_CadreBeton.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreBeton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreBeton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreBeton.Location = New System.Drawing.Point(0, 485)
        Me.lbl_CadreBeton.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreBeton.Name = "lbl_CadreBeton"
        Me.lbl_CadreBeton.Size = New System.Drawing.Size(442, 30)
        Me.lbl_CadreBeton.TabIndex = 6
        Me.lbl_CadreBeton.Text = "lbl_CadreBeton (6)"
        Me.lbl_CadreBeton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_OptionsELS
        '
        Me.pan_OptionsELS.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_OptionsELS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_OptionsELS.Controls.Add(Me.img_info)
        Me.pan_OptionsELS.Controls.Add(Me.txt_CtrFlecheFab)
        Me.pan_OptionsELS.Controls.Add(Me.etq_UnitDimension2)
        Me.pan_OptionsELS.Controls.Add(Me.img_CtrFlecheFab)
        Me.pan_OptionsELS.Controls.Add(Me.chk_CtrFlecheFab)
        Me.pan_OptionsELS.Controls.Add(Me.chk_Psi2LongTerme)
        Me.pan_OptionsELS.Controls.Add(Me.etq_UnitLargeurF)
        Me.pan_OptionsELS.Controls.Add(Me.cmb_Wk)
        Me.pan_OptionsELS.Controls.Add(Me.img_Wk)
        Me.pan_OptionsELS.Controls.Add(Me.lbl_LargeurFissure)
        Me.pan_OptionsELS.Controls.Add(Me.chk_MaitriseFissuration)
        Me.pan_OptionsELS.Controls.Add(Me.lbl_StudDeflection)
        Me.pan_OptionsELS.Controls.Add(Me.etq_UnitDimension1)
        Me.pan_OptionsELS.Controls.Add(Me.txt_Se)
        Me.pan_OptionsELS.Controls.Add(Me.img_se)
        Me.pan_OptionsELS.Controls.Add(Me.chk_FlechesETA)
        Me.pan_OptionsELS.Controls.Add(Me.lbl_CombinationVibration)
        Me.pan_OptionsELS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_OptionsELS.Location = New System.Drawing.Point(0, 265)
        Me.pan_OptionsELS.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_OptionsELS.Name = "pan_OptionsELS"
        Me.pan_OptionsELS.Size = New System.Drawing.Size(442, 219)
        Me.pan_OptionsELS.TabIndex = 5
        '
        'txt_CtrFlecheFab
        '
        Me.txt_CtrFlecheFab.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_CtrFlecheFab.Location = New System.Drawing.Point(241, 39)
        Me.txt_CtrFlecheFab.Name = "txt_CtrFlecheFab"
        Me.txt_CtrFlecheFab.Size = New System.Drawing.Size(58, 20)
        Me.txt_CtrFlecheFab.TabIndex = 133
        '
        'etq_UnitDimension2
        '
        Me.etq_UnitDimension2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDimension2.AutoSize = True
        Me.etq_UnitDimension2.Location = New System.Drawing.Point(303, 43)
        Me.etq_UnitDimension2.Name = "etq_UnitDimension2"
        Me.etq_UnitDimension2.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDimension2.TabIndex = 132
        Me.etq_UnitDimension2.Text = "mm"
        '
        'img_CtrFlecheFab
        '
        Me.img_CtrFlecheFab.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_CtrFlecheFab.Location = New System.Drawing.Point(192, 39)
        Me.img_CtrFlecheFab.Name = "img_CtrFlecheFab"
        Me.img_CtrFlecheFab.Size = New System.Drawing.Size(46, 20)
        Me.img_CtrFlecheFab.TabIndex = 130
        Me.img_CtrFlecheFab.TabStop = False
        '
        'chk_CtrFlecheFab
        '
        Me.chk_CtrFlecheFab.AutoSize = True
        Me.chk_CtrFlecheFab.Location = New System.Drawing.Point(11, 42)
        Me.chk_CtrFlecheFab.Name = "chk_CtrFlecheFab"
        Me.chk_CtrFlecheFab.Size = New System.Drawing.Size(113, 17)
        Me.chk_CtrFlecheFab.TabIndex = 129
        Me.chk_CtrFlecheFab.Text = "chk_CtrFlecheFab"
        Me.chk_CtrFlecheFab.UseVisualStyleBackColor = True
        '
        'chk_Psi2LongTerme
        '
        Me.chk_Psi2LongTerme.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chk_Psi2LongTerme.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chk_Psi2LongTerme.Location = New System.Drawing.Point(11, 9)
        Me.chk_Psi2LongTerme.Name = "chk_Psi2LongTerme"
        Me.chk_Psi2LongTerme.Size = New System.Drawing.Size(413, 38)
        Me.chk_Psi2LongTerme.TabIndex = 128
        Me.chk_Psi2LongTerme.Text = "chk_Psi2LongTerme"
        Me.chk_Psi2LongTerme.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chk_Psi2LongTerme.UseVisualStyleBackColor = True
        '
        'etq_UnitLargeurF
        '
        Me.etq_UnitLargeurF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitLargeurF.AutoSize = True
        Me.etq_UnitLargeurF.Location = New System.Drawing.Point(303, 180)
        Me.etq_UnitLargeurF.Name = "etq_UnitLargeurF"
        Me.etq_UnitLargeurF.Size = New System.Drawing.Size(28, 13)
        Me.etq_UnitLargeurF.TabIndex = 127
        Me.etq_UnitLargeurF.Text = "GPa"
        '
        'cmb_Wk
        '
        Me.cmb_Wk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Wk.FormattingEnabled = True
        Me.cmb_Wk.Location = New System.Drawing.Point(240, 176)
        Me.cmb_Wk.Name = "cmb_Wk"
        Me.cmb_Wk.Size = New System.Drawing.Size(59, 21)
        Me.cmb_Wk.TabIndex = 126
        '
        'img_Wk
        '
        Me.img_Wk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Wk.Location = New System.Drawing.Point(192, 177)
        Me.img_Wk.Name = "img_Wk"
        Me.img_Wk.Size = New System.Drawing.Size(46, 20)
        Me.img_Wk.TabIndex = 125
        Me.img_Wk.TabStop = False
        '
        'lbl_LargeurFissure
        '
        Me.lbl_LargeurFissure.AutoSize = True
        Me.lbl_LargeurFissure.Location = New System.Drawing.Point(12, 180)
        Me.lbl_LargeurFissure.Name = "lbl_LargeurFissure"
        Me.lbl_LargeurFissure.Size = New System.Drawing.Size(92, 13)
        Me.lbl_LargeurFissure.TabIndex = 124
        Me.lbl_LargeurFissure.Text = "lbl_LargeurFissure"
        '
        'chk_MaitriseFissuration
        '
        Me.chk_MaitriseFissuration.AutoSize = True
        Me.chk_MaitriseFissuration.Location = New System.Drawing.Point(11, 160)
        Me.chk_MaitriseFissuration.Name = "chk_MaitriseFissuration"
        Me.chk_MaitriseFissuration.Size = New System.Drawing.Size(136, 17)
        Me.chk_MaitriseFissuration.TabIndex = 123
        Me.chk_MaitriseFissuration.Text = "chk_MaitriseFissuration"
        Me.chk_MaitriseFissuration.UseVisualStyleBackColor = True
        '
        'lbl_StudDeflection
        '
        Me.lbl_StudDeflection.AutoSize = True
        Me.lbl_StudDeflection.Location = New System.Drawing.Point(13, 117)
        Me.lbl_StudDeflection.Name = "lbl_StudDeflection"
        Me.lbl_StudDeflection.Size = New System.Drawing.Size(93, 13)
        Me.lbl_StudDeflection.TabIndex = 122
        Me.lbl_StudDeflection.Text = "lbl_StudDeflection"
        '
        'etq_UnitDimension1
        '
        Me.etq_UnitDimension1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDimension1.AutoSize = True
        Me.etq_UnitDimension1.Location = New System.Drawing.Point(306, 139)
        Me.etq_UnitDimension1.Name = "etq_UnitDimension1"
        Me.etq_UnitDimension1.Size = New System.Drawing.Size(28, 13)
        Me.etq_UnitDimension1.TabIndex = 121
        Me.etq_UnitDimension1.Text = "GPa"
        '
        'txt_Se
        '
        Me.txt_Se.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Se.Location = New System.Drawing.Point(241, 135)
        Me.txt_Se.Name = "txt_Se"
        Me.txt_Se.Size = New System.Drawing.Size(58, 20)
        Me.txt_Se.TabIndex = 120
        '
        'img_se
        '
        Me.img_se.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_se.Location = New System.Drawing.Point(193, 135)
        Me.img_se.Name = "img_se"
        Me.img_se.Size = New System.Drawing.Size(46, 20)
        Me.img_se.TabIndex = 119
        Me.img_se.TabStop = False
        '
        'chk_FlechesETA
        '
        Me.chk_FlechesETA.AutoSize = True
        Me.chk_FlechesETA.Location = New System.Drawing.Point(11, 78)
        Me.chk_FlechesETA.Name = "chk_FlechesETA"
        Me.chk_FlechesETA.Size = New System.Drawing.Size(108, 17)
        Me.chk_FlechesETA.TabIndex = 116
        Me.chk_FlechesETA.Text = "chk_FlechesETA"
        Me.chk_FlechesETA.UseVisualStyleBackColor = True
        '
        'lbl_CombinationVibration
        '
        Me.lbl_CombinationVibration.AutoSize = True
        Me.lbl_CombinationVibration.Location = New System.Drawing.Point(409, 17)
        Me.lbl_CombinationVibration.Name = "lbl_CombinationVibration"
        Me.lbl_CombinationVibration.Size = New System.Drawing.Size(122, 13)
        Me.lbl_CombinationVibration.TabIndex = 61
        Me.lbl_CombinationVibration.Text = "lbl_CombinationVibration"
        Me.lbl_CombinationVibration.Visible = False
        '
        'lbl_CadreELS
        '
        Me.lbl_CadreELS.AutoSize = True
        Me.lbl_CadreELS.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreELS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreELS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreELS.Location = New System.Drawing.Point(0, 235)
        Me.lbl_CadreELS.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreELS.Name = "lbl_CadreELS"
        Me.lbl_CadreELS.Size = New System.Drawing.Size(442, 30)
        Me.lbl_CadreELS.TabIndex = 4
        Me.lbl_CadreELS.Text = "lbl_CadreELS (4)"
        Me.lbl_CadreELS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_OptionsELU
        '
        Me.pan_OptionsELU.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_OptionsELU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_OptionsELU.Controls.Add(Me.rdb_ElasticDesignClasse3)
        Me.pan_OptionsELU.Controls.Add(Me.rdb_ElasticDesignVM)
        Me.pan_OptionsELU.Controls.Add(Me.rdb_NormalDesign)
        Me.pan_OptionsELU.Controls.Add(Me.txt_eta)
        Me.pan_OptionsELU.Controls.Add(Me.lbl_eta)
        Me.pan_OptionsELU.Controls.Add(Me.img_eta)
        Me.pan_OptionsELU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_OptionsELU.Location = New System.Drawing.Point(0, 115)
        Me.pan_OptionsELU.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_OptionsELU.Name = "pan_OptionsELU"
        Me.pan_OptionsELU.Size = New System.Drawing.Size(442, 119)
        Me.pan_OptionsELU.TabIndex = 3
        '
        'rdb_ElasticDesignClasse3
        '
        Me.rdb_ElasticDesignClasse3.AutoSize = True
        Me.rdb_ElasticDesignClasse3.Location = New System.Drawing.Point(21, 62)
        Me.rdb_ElasticDesignClasse3.Name = "rdb_ElasticDesignClasse3"
        Me.rdb_ElasticDesignClasse3.Size = New System.Drawing.Size(147, 17)
        Me.rdb_ElasticDesignClasse3.TabIndex = 2
        Me.rdb_ElasticDesignClasse3.TabStop = True
        Me.rdb_ElasticDesignClasse3.Text = "rdb_ElasticDesignClasse3"
        Me.rdb_ElasticDesignClasse3.UseVisualStyleBackColor = True
        '
        'rdb_ElasticDesignVM
        '
        Me.rdb_ElasticDesignVM.AutoSize = True
        Me.rdb_ElasticDesignVM.Location = New System.Drawing.Point(21, 39)
        Me.rdb_ElasticDesignVM.Name = "rdb_ElasticDesignVM"
        Me.rdb_ElasticDesignVM.Size = New System.Drawing.Size(110, 17)
        Me.rdb_ElasticDesignVM.TabIndex = 1
        Me.rdb_ElasticDesignVM.TabStop = True
        Me.rdb_ElasticDesignVM.Text = "rdb_ElasticDesign"
        Me.rdb_ElasticDesignVM.UseVisualStyleBackColor = True
        '
        'rdb_NormalDesign
        '
        Me.rdb_NormalDesign.AutoSize = True
        Me.rdb_NormalDesign.Location = New System.Drawing.Point(21, 16)
        Me.rdb_NormalDesign.Name = "rdb_NormalDesign"
        Me.rdb_NormalDesign.Size = New System.Drawing.Size(112, 17)
        Me.rdb_NormalDesign.TabIndex = 0
        Me.rdb_NormalDesign.TabStop = True
        Me.rdb_NormalDesign.Text = "rbd_NormalDesign"
        Me.rdb_NormalDesign.UseVisualStyleBackColor = True
        '
        'txt_eta
        '
        Me.txt_eta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_eta.Location = New System.Drawing.Point(237, 89)
        Me.txt_eta.Name = "txt_eta"
        Me.txt_eta.Size = New System.Drawing.Size(58, 20)
        Me.txt_eta.TabIndex = 117
        '
        'lbl_eta
        '
        Me.lbl_eta.AutoSize = True
        Me.lbl_eta.Location = New System.Drawing.Point(21, 93)
        Me.lbl_eta.Name = "lbl_eta"
        Me.lbl_eta.Size = New System.Drawing.Size(38, 13)
        Me.lbl_eta.TabIndex = 115
        Me.lbl_eta.Text = "lbl_eta"
        '
        'img_eta
        '
        Me.img_eta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_eta.Location = New System.Drawing.Point(189, 89)
        Me.img_eta.Name = "img_eta"
        Me.img_eta.Size = New System.Drawing.Size(46, 20)
        Me.img_eta.TabIndex = 116
        Me.img_eta.TabStop = False
        '
        'lbl_CadreELU
        '
        Me.lbl_CadreELU.AutoSize = True
        Me.lbl_CadreELU.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreELU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreELU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreELU.Location = New System.Drawing.Point(0, 85)
        Me.lbl_CadreELU.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreELU.Name = "lbl_CadreELU"
        Me.lbl_CadreELU.Size = New System.Drawing.Size(442, 30)
        Me.lbl_CadreELU.TabIndex = 2
        Me.lbl_CadreELU.Text = "lbl_CadreELU (2)"
        Me.lbl_CadreELU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_CadreNorm
        '
        Me.lbl_CadreNorm.AutoSize = True
        Me.lbl_CadreNorm.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreNorm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreNorm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreNorm.Location = New System.Drawing.Point(0, 0)
        Me.lbl_CadreNorm.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreNorm.Name = "lbl_CadreNorm"
        Me.lbl_CadreNorm.Size = New System.Drawing.Size(442, 30)
        Me.lbl_CadreNorm.TabIndex = 0
        Me.lbl_CadreNorm.Text = "lbl_CadreNorm (0)"
        Me.lbl_CadreNorm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Norm
        '
        Me.pan_Norm.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Norm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Norm.Controls.Add(Me.cmb_Norme)
        Me.pan_Norm.Controls.Add(Me.lbl_Norme)
        Me.pan_Norm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Norm.Location = New System.Drawing.Point(0, 30)
        Me.pan_Norm.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Norm.Name = "pan_Norm"
        Me.pan_Norm.Size = New System.Drawing.Size(442, 54)
        Me.pan_Norm.TabIndex = 1
        '
        'cmb_Norme
        '
        Me.cmb_Norme.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Norme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Norme.FormattingEnabled = True
        Me.cmb_Norme.Location = New System.Drawing.Point(186, 15)
        Me.cmb_Norme.Name = "cmb_Norme"
        Me.cmb_Norme.Size = New System.Drawing.Size(240, 21)
        Me.cmb_Norme.TabIndex = 106
        '
        'lbl_Norme
        '
        Me.lbl_Norme.AutoSize = True
        Me.lbl_Norme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Norme.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl_Norme.Location = New System.Drawing.Point(8, 18)
        Me.lbl_Norme.Name = "lbl_Norme"
        Me.lbl_Norme.Size = New System.Drawing.Size(54, 13)
        Me.lbl_Norme.TabIndex = 105
        Me.lbl_Norme.Text = "lbl_Norme"
        Me.lbl_Norme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pan_Beton
        '
        Me.pan_Beton.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Beton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Beton.Controls.Add(Me.img_AgeT)
        Me.pan_Beton.Controls.Add(Me.etq_UnitJour7)
        Me.pan_Beton.Controls.Add(Me.txt_AgeT)
        Me.pan_Beton.Controls.Add(Me.lbl_AgeT)
        Me.pan_Beton.Controls.Add(Me.lbl_SH)
        Me.pan_Beton.Controls.Add(Me.lbl_G2)
        Me.pan_Beton.Controls.Add(Me.lbl_G1)
        Me.pan_Beton.Controls.Add(Me.lbl_TimeT0)
        Me.pan_Beton.Controls.Add(Me.img_T0SH)
        Me.pan_Beton.Controls.Add(Me.img_T0G2)
        Me.pan_Beton.Controls.Add(Me.img_T0G1)
        Me.pan_Beton.Controls.Add(Me.etq_UnitJour6)
        Me.pan_Beton.Controls.Add(Me.etq_UnitJour4)
        Me.pan_Beton.Controls.Add(Me.etq_UnitJour2)
        Me.pan_Beton.Controls.Add(Me.etq_UnitJour5)
        Me.pan_Beton.Controls.Add(Me.etq_UnitJour3)
        Me.pan_Beton.Controls.Add(Me.etq_UnitJour1)
        Me.pan_Beton.Controls.Add(Me.txt_t0SHDalle)
        Me.pan_Beton.Controls.Add(Me.txt_t0SHEnrob)
        Me.pan_Beton.Controls.Add(Me.txt_t0G2Dalle)
        Me.pan_Beton.Controls.Add(Me.txt_t0G2Enrob)
        Me.pan_Beton.Controls.Add(Me.txt_t0G1Dalle)
        Me.pan_Beton.Controls.Add(Me.txt_t0G1Enrob)
        Me.pan_Beton.Controls.Add(Me.lbl_Enrobage)
        Me.pan_Beton.Controls.Add(Me.lbl_Dalle)
        Me.pan_Beton.Controls.Add(Me.etq_UnitModuleY)
        Me.pan_Beton.Controls.Add(Me.txt_Es)
        Me.pan_Beton.Controls.Add(Me.img_Es)
        Me.pan_Beton.Controls.Add(Me.lbl_ArmaYoung)
        Me.pan_Beton.Controls.Add(Me.chk_RetraitEnrobage)
        Me.pan_Beton.Controls.Add(Me.Label1)
        Me.pan_Beton.Controls.Add(Me.etq_UnitEpsilon)
        Me.pan_Beton.Controls.Add(Me.txt_EpsilonSh)
        Me.pan_Beton.Controls.Add(Me.img_EpsilonSh)
        Me.pan_Beton.Controls.Add(Me.img_RH)
        Me.pan_Beton.Controls.Add(Me.lbl_Shrinkage)
        Me.pan_Beton.Controls.Add(Me.cmb_RH)
        Me.pan_Beton.Controls.Add(Me.lbl_RH)
        Me.pan_Beton.Controls.Add(Me.lbl_BetonMessage)
        Me.pan_Beton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Beton.Location = New System.Drawing.Point(0, 515)
        Me.pan_Beton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Beton.Name = "pan_Beton"
        Me.pan_Beton.Size = New System.Drawing.Size(442, 239)
        Me.pan_Beton.TabIndex = 7
        '
        'img_AgeT
        '
        Me.img_AgeT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_AgeT.Location = New System.Drawing.Point(194, 211)
        Me.img_AgeT.Name = "img_AgeT"
        Me.img_AgeT.Size = New System.Drawing.Size(46, 20)
        Me.img_AgeT.TabIndex = 175
        Me.img_AgeT.TabStop = False
        '
        'etq_UnitJour7
        '
        Me.etq_UnitJour7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitJour7.AutoSize = True
        Me.etq_UnitJour7.Location = New System.Drawing.Point(305, 214)
        Me.etq_UnitJour7.Name = "etq_UnitJour7"
        Me.etq_UnitJour7.Size = New System.Drawing.Size(9, 13)
        Me.etq_UnitJour7.TabIndex = 174
        Me.etq_UnitJour7.Text = "j"
        '
        'txt_AgeT
        '
        Me.txt_AgeT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_AgeT.Location = New System.Drawing.Point(240, 211)
        Me.txt_AgeT.Name = "txt_AgeT"
        Me.txt_AgeT.Size = New System.Drawing.Size(58, 20)
        Me.txt_AgeT.TabIndex = 173
        Me.txt_AgeT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_AgeT
        '
        Me.lbl_AgeT.AutoSize = True
        Me.lbl_AgeT.Location = New System.Drawing.Point(8, 214)
        Me.lbl_AgeT.Name = "lbl_AgeT"
        Me.lbl_AgeT.Size = New System.Drawing.Size(49, 13)
        Me.lbl_AgeT.TabIndex = 172
        Me.lbl_AgeT.Text = "lbl_AgeT"
        '
        'lbl_SH
        '
        Me.lbl_SH.AutoSize = True
        Me.lbl_SH.Location = New System.Drawing.Point(42, 188)
        Me.lbl_SH.Name = "lbl_SH"
        Me.lbl_SH.Size = New System.Drawing.Size(38, 13)
        Me.lbl_SH.TabIndex = 171
        Me.lbl_SH.Text = "lbl_SH"
        '
        'lbl_G2
        '
        Me.lbl_G2.AutoSize = True
        Me.lbl_G2.Location = New System.Drawing.Point(42, 166)
        Me.lbl_G2.Name = "lbl_G2"
        Me.lbl_G2.Size = New System.Drawing.Size(37, 13)
        Me.lbl_G2.TabIndex = 170
        Me.lbl_G2.Text = "lbl_G2"
        '
        'lbl_G1
        '
        Me.lbl_G1.AutoSize = True
        Me.lbl_G1.Location = New System.Drawing.Point(42, 144)
        Me.lbl_G1.Name = "lbl_G1"
        Me.lbl_G1.Size = New System.Drawing.Size(37, 13)
        Me.lbl_G1.TabIndex = 169
        Me.lbl_G1.Text = "lbl_G1"
        '
        'lbl_TimeT0
        '
        Me.lbl_TimeT0.AutoSize = True
        Me.lbl_TimeT0.Location = New System.Drawing.Point(8, 114)
        Me.lbl_TimeT0.Name = "lbl_TimeT0"
        Me.lbl_TimeT0.Size = New System.Drawing.Size(59, 13)
        Me.lbl_TimeT0.TabIndex = 168
        Me.lbl_TimeT0.Text = "lbl_TimeT0"
        '
        'img_T0SH
        '
        Me.img_T0SH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_T0SH.Location = New System.Drawing.Point(194, 185)
        Me.img_T0SH.Name = "img_T0SH"
        Me.img_T0SH.Size = New System.Drawing.Size(46, 20)
        Me.img_T0SH.TabIndex = 167
        Me.img_T0SH.TabStop = False
        '
        'img_T0G2
        '
        Me.img_T0G2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_T0G2.Location = New System.Drawing.Point(194, 163)
        Me.img_T0G2.Name = "img_T0G2"
        Me.img_T0G2.Size = New System.Drawing.Size(46, 20)
        Me.img_T0G2.TabIndex = 166
        Me.img_T0G2.TabStop = False
        '
        'img_T0G1
        '
        Me.img_T0G1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_T0G1.Location = New System.Drawing.Point(194, 141)
        Me.img_T0G1.Name = "img_T0G1"
        Me.img_T0G1.Size = New System.Drawing.Size(46, 20)
        Me.img_T0G1.TabIndex = 165
        Me.img_T0G1.TabStop = False
        '
        'etq_UnitJour6
        '
        Me.etq_UnitJour6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitJour6.AutoSize = True
        Me.etq_UnitJour6.Location = New System.Drawing.Point(392, 188)
        Me.etq_UnitJour6.Name = "etq_UnitJour6"
        Me.etq_UnitJour6.Size = New System.Drawing.Size(9, 13)
        Me.etq_UnitJour6.TabIndex = 164
        Me.etq_UnitJour6.Text = "j"
        '
        'etq_UnitJour4
        '
        Me.etq_UnitJour4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitJour4.AutoSize = True
        Me.etq_UnitJour4.Location = New System.Drawing.Point(392, 166)
        Me.etq_UnitJour4.Name = "etq_UnitJour4"
        Me.etq_UnitJour4.Size = New System.Drawing.Size(9, 13)
        Me.etq_UnitJour4.TabIndex = 163
        Me.etq_UnitJour4.Text = "j"
        '
        'etq_UnitJour2
        '
        Me.etq_UnitJour2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitJour2.AutoSize = True
        Me.etq_UnitJour2.Location = New System.Drawing.Point(392, 144)
        Me.etq_UnitJour2.Name = "etq_UnitJour2"
        Me.etq_UnitJour2.Size = New System.Drawing.Size(9, 13)
        Me.etq_UnitJour2.TabIndex = 162
        Me.etq_UnitJour2.Text = "j"
        '
        'etq_UnitJour5
        '
        Me.etq_UnitJour5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitJour5.AutoSize = True
        Me.etq_UnitJour5.Location = New System.Drawing.Point(305, 188)
        Me.etq_UnitJour5.Name = "etq_UnitJour5"
        Me.etq_UnitJour5.Size = New System.Drawing.Size(9, 13)
        Me.etq_UnitJour5.TabIndex = 161
        Me.etq_UnitJour5.Text = "j"
        '
        'etq_UnitJour3
        '
        Me.etq_UnitJour3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitJour3.AutoSize = True
        Me.etq_UnitJour3.Location = New System.Drawing.Point(305, 166)
        Me.etq_UnitJour3.Name = "etq_UnitJour3"
        Me.etq_UnitJour3.Size = New System.Drawing.Size(9, 13)
        Me.etq_UnitJour3.TabIndex = 160
        Me.etq_UnitJour3.Text = "j"
        '
        'etq_UnitJour1
        '
        Me.etq_UnitJour1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitJour1.AutoSize = True
        Me.etq_UnitJour1.Location = New System.Drawing.Point(305, 144)
        Me.etq_UnitJour1.Name = "etq_UnitJour1"
        Me.etq_UnitJour1.Size = New System.Drawing.Size(9, 13)
        Me.etq_UnitJour1.TabIndex = 159
        Me.etq_UnitJour1.Text = "j"
        '
        'txt_t0SHDalle
        '
        Me.txt_t0SHDalle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_t0SHDalle.Location = New System.Drawing.Point(240, 185)
        Me.txt_t0SHDalle.Name = "txt_t0SHDalle"
        Me.txt_t0SHDalle.Size = New System.Drawing.Size(58, 20)
        Me.txt_t0SHDalle.TabIndex = 158
        Me.txt_t0SHDalle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_t0SHEnrob
        '
        Me.txt_t0SHEnrob.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_t0SHEnrob.Location = New System.Drawing.Point(329, 185)
        Me.txt_t0SHEnrob.Name = "txt_t0SHEnrob"
        Me.txt_t0SHEnrob.Size = New System.Drawing.Size(58, 20)
        Me.txt_t0SHEnrob.TabIndex = 157
        Me.txt_t0SHEnrob.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_t0G2Dalle
        '
        Me.txt_t0G2Dalle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_t0G2Dalle.Location = New System.Drawing.Point(240, 163)
        Me.txt_t0G2Dalle.Name = "txt_t0G2Dalle"
        Me.txt_t0G2Dalle.Size = New System.Drawing.Size(58, 20)
        Me.txt_t0G2Dalle.TabIndex = 156
        Me.txt_t0G2Dalle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_t0G2Enrob
        '
        Me.txt_t0G2Enrob.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_t0G2Enrob.Location = New System.Drawing.Point(329, 163)
        Me.txt_t0G2Enrob.Name = "txt_t0G2Enrob"
        Me.txt_t0G2Enrob.Size = New System.Drawing.Size(58, 20)
        Me.txt_t0G2Enrob.TabIndex = 155
        Me.txt_t0G2Enrob.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_t0G1Dalle
        '
        Me.txt_t0G1Dalle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_t0G1Dalle.Location = New System.Drawing.Point(240, 141)
        Me.txt_t0G1Dalle.Name = "txt_t0G1Dalle"
        Me.txt_t0G1Dalle.Size = New System.Drawing.Size(58, 20)
        Me.txt_t0G1Dalle.TabIndex = 154
        Me.txt_t0G1Dalle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_t0G1Enrob
        '
        Me.txt_t0G1Enrob.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_t0G1Enrob.Location = New System.Drawing.Point(329, 141)
        Me.txt_t0G1Enrob.Name = "txt_t0G1Enrob"
        Me.txt_t0G1Enrob.Size = New System.Drawing.Size(58, 20)
        Me.txt_t0G1Enrob.TabIndex = 153
        Me.txt_t0G1Enrob.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_Enrobage
        '
        Me.lbl_Enrobage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Enrobage.AutoSize = True
        Me.lbl_Enrobage.Location = New System.Drawing.Point(324, 125)
        Me.lbl_Enrobage.Name = "lbl_Enrobage"
        Me.lbl_Enrobage.Size = New System.Drawing.Size(69, 13)
        Me.lbl_Enrobage.TabIndex = 152
        Me.lbl_Enrobage.Text = "lbl_Enrobage"
        '
        'lbl_Dalle
        '
        Me.lbl_Dalle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Dalle.AutoSize = True
        Me.lbl_Dalle.Location = New System.Drawing.Point(248, 125)
        Me.lbl_Dalle.Name = "lbl_Dalle"
        Me.lbl_Dalle.Size = New System.Drawing.Size(47, 13)
        Me.lbl_Dalle.TabIndex = 151
        Me.lbl_Dalle.Text = "lbl_Dalle"
        '
        'etq_UnitModuleY
        '
        Me.etq_UnitModuleY.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitModuleY.AutoSize = True
        Me.etq_UnitModuleY.Location = New System.Drawing.Point(305, 85)
        Me.etq_UnitModuleY.Name = "etq_UnitModuleY"
        Me.etq_UnitModuleY.Size = New System.Drawing.Size(28, 13)
        Me.etq_UnitModuleY.TabIndex = 118
        Me.etq_UnitModuleY.Text = "GPa"
        '
        'txt_Es
        '
        Me.txt_Es.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Es.Location = New System.Drawing.Point(240, 81)
        Me.txt_Es.Name = "txt_Es"
        Me.txt_Es.Size = New System.Drawing.Size(58, 20)
        Me.txt_Es.TabIndex = 117
        '
        'img_Es
        '
        Me.img_Es.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Es.Location = New System.Drawing.Point(192, 81)
        Me.img_Es.Name = "img_Es"
        Me.img_Es.Size = New System.Drawing.Size(46, 20)
        Me.img_Es.TabIndex = 116
        Me.img_Es.TabStop = False
        '
        'lbl_ArmaYoung
        '
        Me.lbl_ArmaYoung.AutoSize = True
        Me.lbl_ArmaYoung.Location = New System.Drawing.Point(8, 85)
        Me.lbl_ArmaYoung.Name = "lbl_ArmaYoung"
        Me.lbl_ArmaYoung.Size = New System.Drawing.Size(78, 13)
        Me.lbl_ArmaYoung.TabIndex = 115
        Me.lbl_ArmaYoung.Text = "lbl_ArmaYoung"
        '
        'chk_RetraitEnrobage
        '
        Me.chk_RetraitEnrobage.AutoSize = True
        Me.chk_RetraitEnrobage.Location = New System.Drawing.Point(375, 57)
        Me.chk_RetraitEnrobage.Name = "chk_RetraitEnrobage"
        Me.chk_RetraitEnrobage.Size = New System.Drawing.Size(127, 17)
        Me.chk_RetraitEnrobage.TabIndex = 114
        Me.chk_RetraitEnrobage.Text = "chk_RetraitEnrobage"
        Me.chk_RetraitEnrobage.UseVisualStyleBackColor = True
        Me.chk_RetraitEnrobage.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(327, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 13)
        Me.Label1.TabIndex = 113
        Me.Label1.Text = "-6"
        '
        'etq_UnitEpsilon
        '
        Me.etq_UnitEpsilon.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitEpsilon.AutoSize = True
        Me.etq_UnitEpsilon.Location = New System.Drawing.Point(305, 58)
        Me.etq_UnitEpsilon.Name = "etq_UnitEpsilon"
        Me.etq_UnitEpsilon.Size = New System.Drawing.Size(27, 13)
        Me.etq_UnitEpsilon.TabIndex = 112
        Me.etq_UnitEpsilon.Text = "x 10"
        '
        'txt_EpsilonSh
        '
        Me.txt_EpsilonSh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EpsilonSh.Location = New System.Drawing.Point(240, 55)
        Me.txt_EpsilonSh.Name = "txt_EpsilonSh"
        Me.txt_EpsilonSh.Size = New System.Drawing.Size(58, 20)
        Me.txt_EpsilonSh.TabIndex = 111
        '
        'img_EpsilonSh
        '
        Me.img_EpsilonSh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_EpsilonSh.Location = New System.Drawing.Point(192, 55)
        Me.img_EpsilonSh.Name = "img_EpsilonSh"
        Me.img_EpsilonSh.Size = New System.Drawing.Size(46, 20)
        Me.img_EpsilonSh.TabIndex = 110
        Me.img_EpsilonSh.TabStop = False
        '
        'img_RH
        '
        Me.img_RH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_RH.Location = New System.Drawing.Point(192, 30)
        Me.img_RH.Name = "img_RH"
        Me.img_RH.Size = New System.Drawing.Size(46, 20)
        Me.img_RH.TabIndex = 109
        Me.img_RH.TabStop = False
        '
        'lbl_Shrinkage
        '
        Me.lbl_Shrinkage.AutoSize = True
        Me.lbl_Shrinkage.Location = New System.Drawing.Point(8, 59)
        Me.lbl_Shrinkage.Name = "lbl_Shrinkage"
        Me.lbl_Shrinkage.Size = New System.Drawing.Size(71, 13)
        Me.lbl_Shrinkage.TabIndex = 108
        Me.lbl_Shrinkage.Text = "lbl_Shrinkage"
        '
        'cmb_RH
        '
        Me.cmb_RH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_RH.FormattingEnabled = True
        Me.cmb_RH.Location = New System.Drawing.Point(241, 29)
        Me.cmb_RH.Name = "cmb_RH"
        Me.cmb_RH.Size = New System.Drawing.Size(104, 21)
        Me.cmb_RH.TabIndex = 107
        '
        'lbl_RH
        '
        Me.lbl_RH.AutoSize = True
        Me.lbl_RH.Location = New System.Drawing.Point(8, 32)
        Me.lbl_RH.Name = "lbl_RH"
        Me.lbl_RH.Size = New System.Drawing.Size(39, 13)
        Me.lbl_RH.TabIndex = 61
        Me.lbl_RH.Text = "lbl_RH"
        '
        'lbl_BetonMessage
        '
        Me.lbl_BetonMessage.AutoSize = True
        Me.lbl_BetonMessage.Location = New System.Drawing.Point(8, 9)
        Me.lbl_BetonMessage.Name = "lbl_BetonMessage"
        Me.lbl_BetonMessage.Size = New System.Drawing.Size(94, 13)
        Me.lbl_BetonMessage.TabIndex = 60
        Me.lbl_BetonMessage.Text = "lbl_BetonMessage"
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'img_info
        '
        Me.img_info.Image = CType(resources.GetObject("img_info.Image"), System.Drawing.Image)
        Me.img_info.Location = New System.Drawing.Point(327, 39)
        Me.img_info.Margin = New System.Windows.Forms.Padding(0)
        Me.img_info.Name = "img_info"
        Me.img_info.Size = New System.Drawing.Size(20, 20)
        Me.img_info.TabIndex = 134
        Me.img_info.TabStop = False
        '
        'Frm_OptionsCalculPoutre
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(465, 543)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_OptionsCalculPoutre"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Frm_OptionsCalcuPoutre"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Portees.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_Parametres.ResumeLayout(False)
        Me.pan_Parametres.PerformLayout()
        CType(Me.img_G, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Sections.ResumeLayout(False)
        Me.pan_Sections.PerformLayout()
        Me.pan_OptionsELS.ResumeLayout(False)
        Me.pan_OptionsELS.PerformLayout()
        CType(Me.img_CtrFlecheFab, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Wk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_se, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_OptionsELU.ResumeLayout(False)
        Me.pan_OptionsELU.PerformLayout()
        CType(Me.img_eta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Norm.ResumeLayout(False)
        Me.pan_Norm.PerformLayout()
        Me.pan_Beton.ResumeLayout(False)
        Me.pan_Beton.PerformLayout()
        CType(Me.img_AgeT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_T0SH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_T0G2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_T0G1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Es, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_EpsilonSh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_RH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_info, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lbl_CadreNorm As Label
    Friend WithEvents pan_Norm As Panel
    Friend WithEvents cmb_Norme As ComboBox
    Friend WithEvents lbl_Norme As Label
    Friend WithEvents pan_OptionsELU As Panel
    Friend WithEvents lbl_CadreELU As Label
    Friend WithEvents rdb_ElasticDesignVM As RadioButton
    Friend WithEvents rdb_NormalDesign As RadioButton
    Friend WithEvents pan_OptionsELS As Panel
    Friend WithEvents lbl_CadreELS As Label
    Friend WithEvents lbl_CadreBeton As Label
    Friend WithEvents pan_Beton As Panel
    Friend WithEvents cmb_RH As ComboBox
    Friend WithEvents lbl_RH As Label
    Friend WithEvents lbl_BetonMessage As Label
    Friend WithEvents lbl_Shrinkage As Label
    Friend WithEvents img_EpsilonSh As PictureBox
    Friend WithEvents img_RH As PictureBox
    Friend WithEvents etq_UnitEpsilon As Label
    Friend WithEvents txt_EpsilonSh As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents chk_RetraitEnrobage As CheckBox
    Friend WithEvents etq_UnitModuleY As Label
    Friend WithEvents txt_Es As TextBox
    Friend WithEvents img_Es As PictureBox
    Friend WithEvents lbl_ArmaYoung As Label
    Friend WithEvents pan_Sections As Panel
    Friend WithEvents chk_ArmaComprimees As CheckBox
    Friend WithEvents chk_LargeursPartipantesSimples As CheckBox
    Friend WithEvents lbl_CadreSections As Label
    Friend WithEvents pan_Parametres As Panel
    Friend WithEvents lbl_CadreParametres As Label
    Friend WithEvents lbl_CombinationVibration As Label
    Friend WithEvents img_T0SH As PictureBox
    Friend WithEvents img_T0G2 As PictureBox
    Friend WithEvents img_T0G1 As PictureBox
    Friend WithEvents etq_UnitJour6 As Label
    Friend WithEvents etq_UnitJour4 As Label
    Friend WithEvents etq_UnitJour2 As Label
    Friend WithEvents etq_UnitJour5 As Label
    Friend WithEvents etq_UnitJour3 As Label
    Friend WithEvents etq_UnitJour1 As Label
    Friend WithEvents txt_t0SHDalle As TextBox
    Friend WithEvents txt_t0SHEnrob As TextBox
    Friend WithEvents txt_t0G2Dalle As TextBox
    Friend WithEvents txt_t0G2Enrob As TextBox
    Friend WithEvents txt_t0G1Dalle As TextBox
    Friend WithEvents txt_t0G1Enrob As TextBox
    Friend WithEvents lbl_Enrobage As Label
    Friend WithEvents lbl_Dalle As Label
    Friend WithEvents lbl_TimeT0 As Label
    Friend WithEvents lbl_SH As Label
    Friend WithEvents lbl_G2 As Label
    Friend WithEvents lbl_G1 As Label
    Friend WithEvents img_AgeT As PictureBox
    Friend WithEvents etq_UnitJour7 As Label
    Friend WithEvents txt_AgeT As TextBox
    Friend WithEvents lbl_AgeT As Label
    Friend WithEvents etq_UnitG As Label
    Friend WithEvents img_G As PictureBox
    Friend WithEvents cmb_GraviteG As ComboBox
    Friend WithEvents lbl_GraviteG As Label
    Friend WithEvents chk_FlechesETA As CheckBox
    Friend WithEvents lbl_StudDeflection As Label
    Friend WithEvents etq_UnitDimension1 As Label
    Friend WithEvents txt_Se As TextBox
    Friend WithEvents img_se As PictureBox
    Friend WithEvents rdb_ElasticDesignClasse3 As RadioButton
    Friend WithEvents txt_eta As TextBox
    Friend WithEvents lbl_eta As Label
    Friend WithEvents img_eta As PictureBox
    Friend WithEvents chk_MaitriseFissuration As CheckBox
    Friend WithEvents etq_UnitLargeurF As Label
    Friend WithEvents cmb_Wk As ComboBox
    Friend WithEvents img_Wk As PictureBox
    Friend WithEvents lbl_LargeurFissure As Label
    Friend WithEvents chk_EnrobagePropSection As CheckBox
    Friend WithEvents chk_Psi2LongTerme As CheckBox
    Friend WithEvents etq_UnitDimension2 As Label
    Friend WithEvents img_CtrFlecheFab As PictureBox
    Friend WithEvents chk_CtrFlecheFab As CheckBox
    Friend WithEvents txt_CtrFlecheFab As TextBox
    Friend WithEvents img_info As PictureBox
End Class
