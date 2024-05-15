<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_OptionsFeu
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
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_PartieHaute = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Droite = New System.Windows.Forms.Panel()
        Me.TLPan_Droite = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_ParamCalcul = New System.Windows.Forms.Label()
        Me.pan_ParamCalcul = New System.Windows.Forms.Panel()
        Me.lbl_UnitConcreteResistance = New System.Windows.Forms.Label()
        Me.lbl_UnitConvectionSlab = New System.Windows.Forms.Label()
        Me.lbl_ConcreteResistance = New System.Windows.Forms.Label()
        Me.lbl_UnitShadowEffect = New System.Windows.Forms.Label()
        Me.lbl_ConvectionSlab = New System.Windows.Forms.Label()
        Me.lbl_UnitConvectionFactor = New System.Windows.Forms.Label()
        Me.lbl_ShadowEffect = New System.Windows.Forms.Label()
        Me.lbl_UnitEmissivityFire = New System.Windows.Forms.Label()
        Me.lbl_ConvectionFactor = New System.Windows.Forms.Label()
        Me.lbl_EmissivityFire = New System.Windows.Forms.Label()
        Me.lbl_UnitFormFactor = New System.Windows.Forms.Label()
        Me.lbl_UnitMaxTemp = New System.Windows.Forms.Label()
        Me.lbl_FormFactor = New System.Windows.Forms.Label()
        Me.lbl_UnitReferenceTemp = New System.Windows.Forms.Label()
        Me.lbl_MaxTemp = New System.Windows.Forms.Label()
        Me.lbl_UnitTimeIncrement = New System.Windows.Forms.Label()
        Me.lbl_ReferenceTemp = New System.Windows.Forms.Label()
        Me.lbl_TimeIncrement = New System.Windows.Forms.Label()
        Me.txt_ConcreteResistance = New System.Windows.Forms.TextBox()
        Me.txt_ConvectionSlab = New System.Windows.Forms.TextBox()
        Me.img_ConcreteResistance = New System.Windows.Forms.PictureBox()
        Me.txt_ShadowEffect = New System.Windows.Forms.TextBox()
        Me.img_ConvectionSlab = New System.Windows.Forms.PictureBox()
        Me.txt_ConvectionFactor = New System.Windows.Forms.TextBox()
        Me.img_ShadowEffect = New System.Windows.Forms.PictureBox()
        Me.txt_EmissivityFire = New System.Windows.Forms.TextBox()
        Me.img_ConvectionFactor = New System.Windows.Forms.PictureBox()
        Me.img_EmissivityFire = New System.Windows.Forms.PictureBox()
        Me.txt_FormFactor = New System.Windows.Forms.TextBox()
        Me.txt_MaxTemp = New System.Windows.Forms.TextBox()
        Me.img_FormFactor = New System.Windows.Forms.PictureBox()
        Me.txt_ReferenceTemp = New System.Windows.Forms.TextBox()
        Me.img_MaxTemp = New System.Windows.Forms.PictureBox()
        Me.txt_TimeIncrement = New System.Windows.Forms.TextBox()
        Me.lbl_UnitBoltzmann = New System.Windows.Forms.Label()
        Me.img_ReferenceTemp = New System.Windows.Forms.PictureBox()
        Me.lbl_Boltzmann = New System.Windows.Forms.Label()
        Me.img_TimeIncrement = New System.Windows.Forms.PictureBox()
        Me.txt_Boltzmann = New System.Windows.Forms.TextBox()
        Me.img_Boltzmann = New System.Windows.Forms.PictureBox()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_CalculOptions = New System.Windows.Forms.Panel()
        Me.chk_DalleFEM = New System.Windows.Forms.CheckBox()
        Me.chk_ArmaComp = New System.Windows.Forms.CheckBox()
        Me.img_tDalleFEMmax = New System.Windows.Forms.PictureBox()
        Me.lbl_UnittDalleFEMmax = New System.Windows.Forms.Label()
        Me.txt_tDalleFEMmax = New System.Windows.Forms.TextBox()
        Me.chk_ReductionConcreteStrenght = New System.Windows.Forms.CheckBox()
        Me.lbl_CalculOptions = New System.Windows.Forms.Label()
        Me.lbl_ParamPoutre = New System.Windows.Forms.Label()
        Me.pan_ParamPoutre = New System.Windows.Forms.Panel()
        Me.pan_Protection = New System.Windows.Forms.Panel()
        Me.etq_UnitD = New System.Windows.Forms.Label()
        Me.lbl_EpProtec = New System.Windows.Forms.Label()
        Me.txt_EpProtec = New System.Windows.Forms.TextBox()
        Me.img_EpProtec = New System.Windows.Forms.PictureBox()
        Me.cmb_ProtectionType = New System.Windows.Forms.ComboBox()
        Me.lbl_UnitSpecificHeat = New System.Windows.Forms.Label()
        Me.lbl_ProtectionType = New System.Windows.Forms.Label()
        Me.lbl_SpecificHeat = New System.Windows.Forms.Label()
        Me.lbl_UnitThermalCond = New System.Windows.Forms.Label()
        Me.lbl_InsulationType = New System.Windows.Forms.Label()
        Me.cmb_InsulationType = New System.Windows.Forms.ComboBox()
        Me.lbl_UnitDensity = New System.Windows.Forms.Label()
        Me.lbl_ThermalConductivity = New System.Windows.Forms.Label()
        Me.lbl_Density = New System.Windows.Forms.Label()
        Me.txt_SpecificHeat = New System.Windows.Forms.TextBox()
        Me.img_SpecificHeat = New System.Windows.Forms.PictureBox()
        Me.txt_Density = New System.Windows.Forms.TextBox()
        Me.img_Density = New System.Windows.Forms.PictureBox()
        Me.txt_ThermalConductivity = New System.Windows.Forms.TextBox()
        Me.img_ThermalConductivity = New System.Windows.Forms.PictureBox()
        Me.lbl_tDalleFEMmax = New System.Windows.Forms.Label()
        Me.chk_CalculFeu = New System.Windows.Forms.CheckBox()
        Me.chk_ArmaFroid = New System.Windows.Forms.CheckBox()
        Me.chk_AcierGalva = New System.Windows.Forms.CheckBox()
        Me.cmb_SurfaceType = New System.Windows.Forms.ComboBox()
        Me.lbl_SurfaceType = New System.Windows.Forms.Label()
        Me.ErrorProvider_Frm_OptionsFeu = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_PartieHaute.SuspendLayout()
        Me.pan_Droite.SuspendLayout()
        Me.TLPan_Droite.SuspendLayout()
        Me.pan_ParamCalcul.SuspendLayout()
        CType(Me.img_ConcreteResistance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ConvectionSlab, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ShadowEffect, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ConvectionFactor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_EmissivityFire, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_FormFactor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_MaxTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ReferenceTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_TimeIncrement, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Boltzmann, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_CalculOptions.SuspendLayout()
        CType(Me.img_tDalleFEMmax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_ParamPoutre.SuspendLayout()
        Me.pan_Protection.SuspendLayout()
        CType(Me.img_EpProtec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_SpecificHeat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Density, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ThermalConductivity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider_Frm_OptionsFeu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(779, 551)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(779, 551)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 514)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(773, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(399, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(259, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_PartieHaute)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(773, 505)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_PartieHaute
        '
        Me.TLPan_PartieHaute.ColumnCount = 2
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 365.0!))
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieHaute.Controls.Add(Me.pan_Droite, 0, 0)
        Me.TLPan_PartieHaute.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_PartieHaute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieHaute.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_PartieHaute.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_PartieHaute.Name = "TLPan_PartieHaute"
        Me.TLPan_PartieHaute.RowCount = 1
        Me.TLPan_PartieHaute.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieHaute.Size = New System.Drawing.Size(773, 505)
        Me.TLPan_PartieHaute.TabIndex = 0
        '
        'pan_Droite
        '
        Me.pan_Droite.AutoScroll = True
        Me.pan_Droite.Controls.Add(Me.TLPan_Droite)
        Me.pan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Droite.Location = New System.Drawing.Point(365, 0)
        Me.pan_Droite.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Droite.Name = "pan_Droite"
        Me.pan_Droite.Size = New System.Drawing.Size(408, 505)
        Me.pan_Droite.TabIndex = 1
        '
        'TLPan_Droite
        '
        Me.TLPan_Droite.ColumnCount = 1
        Me.TLPan_Droite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Droite.Controls.Add(Me.lbl_ParamCalcul, 0, 0)
        Me.TLPan_Droite.Controls.Add(Me.pan_ParamCalcul, 0, 1)
        Me.TLPan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Droite.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Droite.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Droite.Name = "TLPan_Droite"
        Me.TLPan_Droite.RowCount = 2
        Me.TLPan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Droite.Size = New System.Drawing.Size(408, 505)
        Me.TLPan_Droite.TabIndex = 0
        '
        'lbl_ParamCalcul
        '
        Me.lbl_ParamCalcul.AutoSize = True
        Me.lbl_ParamCalcul.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_ParamCalcul.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ParamCalcul.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ParamCalcul.Location = New System.Drawing.Point(0, 0)
        Me.lbl_ParamCalcul.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ParamCalcul.Name = "lbl_ParamCalcul"
        Me.lbl_ParamCalcul.Size = New System.Drawing.Size(408, 30)
        Me.lbl_ParamCalcul.TabIndex = 0
        Me.lbl_ParamCalcul.Text = "lbl_ParamCalcul"
        Me.lbl_ParamCalcul.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_ParamCalcul
        '
        Me.pan_ParamCalcul.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ParamCalcul.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_UnitConcreteResistance)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_UnitConvectionSlab)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_ConcreteResistance)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_UnitShadowEffect)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_ConvectionSlab)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_UnitConvectionFactor)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_ShadowEffect)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_UnitEmissivityFire)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_ConvectionFactor)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_EmissivityFire)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_UnitFormFactor)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_UnitMaxTemp)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_FormFactor)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_UnitReferenceTemp)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_MaxTemp)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_UnitTimeIncrement)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_ReferenceTemp)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_TimeIncrement)
        Me.pan_ParamCalcul.Controls.Add(Me.txt_ConcreteResistance)
        Me.pan_ParamCalcul.Controls.Add(Me.txt_ConvectionSlab)
        Me.pan_ParamCalcul.Controls.Add(Me.img_ConcreteResistance)
        Me.pan_ParamCalcul.Controls.Add(Me.txt_ShadowEffect)
        Me.pan_ParamCalcul.Controls.Add(Me.img_ConvectionSlab)
        Me.pan_ParamCalcul.Controls.Add(Me.txt_ConvectionFactor)
        Me.pan_ParamCalcul.Controls.Add(Me.img_ShadowEffect)
        Me.pan_ParamCalcul.Controls.Add(Me.txt_EmissivityFire)
        Me.pan_ParamCalcul.Controls.Add(Me.img_ConvectionFactor)
        Me.pan_ParamCalcul.Controls.Add(Me.img_EmissivityFire)
        Me.pan_ParamCalcul.Controls.Add(Me.txt_FormFactor)
        Me.pan_ParamCalcul.Controls.Add(Me.txt_MaxTemp)
        Me.pan_ParamCalcul.Controls.Add(Me.img_FormFactor)
        Me.pan_ParamCalcul.Controls.Add(Me.txt_ReferenceTemp)
        Me.pan_ParamCalcul.Controls.Add(Me.img_MaxTemp)
        Me.pan_ParamCalcul.Controls.Add(Me.txt_TimeIncrement)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_UnitBoltzmann)
        Me.pan_ParamCalcul.Controls.Add(Me.img_ReferenceTemp)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_Boltzmann)
        Me.pan_ParamCalcul.Controls.Add(Me.img_TimeIncrement)
        Me.pan_ParamCalcul.Controls.Add(Me.txt_Boltzmann)
        Me.pan_ParamCalcul.Controls.Add(Me.img_Boltzmann)
        Me.pan_ParamCalcul.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ParamCalcul.Location = New System.Drawing.Point(0, 30)
        Me.pan_ParamCalcul.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ParamCalcul.Name = "pan_ParamCalcul"
        Me.pan_ParamCalcul.Size = New System.Drawing.Size(408, 475)
        Me.pan_ParamCalcul.TabIndex = 1
        '
        'lbl_UnitConcreteResistance
        '
        Me.lbl_UnitConcreteResistance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitConcreteResistance.AutoSize = True
        Me.lbl_UnitConcreteResistance.Location = New System.Drawing.Point(291, 341)
        Me.lbl_UnitConcreteResistance.Name = "lbl_UnitConcreteResistance"
        Me.lbl_UnitConcreteResistance.Size = New System.Drawing.Size(138, 13)
        Me.lbl_UnitConcreteResistance.TabIndex = 54
        Me.lbl_UnitConcreteResistance.Text = "lbl_UnitConcreteResistance"
        '
        'lbl_UnitConvectionSlab
        '
        Me.lbl_UnitConvectionSlab.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitConvectionSlab.AutoSize = True
        Me.lbl_UnitConvectionSlab.Location = New System.Drawing.Point(291, 291)
        Me.lbl_UnitConvectionSlab.Name = "lbl_UnitConvectionSlab"
        Me.lbl_UnitConvectionSlab.Size = New System.Drawing.Size(117, 13)
        Me.lbl_UnitConvectionSlab.TabIndex = 51
        Me.lbl_UnitConvectionSlab.Text = "lbl_UnitConvectionSlab"
        '
        'lbl_ConcreteResistance
        '
        Me.lbl_ConcreteResistance.AutoSize = True
        Me.lbl_ConcreteResistance.Location = New System.Drawing.Point(10, 317)
        Me.lbl_ConcreteResistance.Name = "lbl_ConcreteResistance"
        Me.lbl_ConcreteResistance.Size = New System.Drawing.Size(119, 13)
        Me.lbl_ConcreteResistance.TabIndex = 52
        Me.lbl_ConcreteResistance.Text = "lbl_ConcreteResistance"
        '
        'lbl_UnitShadowEffect
        '
        Me.lbl_UnitShadowEffect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitShadowEffect.AutoSize = True
        Me.lbl_UnitShadowEffect.Location = New System.Drawing.Point(291, 243)
        Me.lbl_UnitShadowEffect.Name = "lbl_UnitShadowEffect"
        Me.lbl_UnitShadowEffect.Size = New System.Drawing.Size(109, 13)
        Me.lbl_UnitShadowEffect.TabIndex = 48
        Me.lbl_UnitShadowEffect.Text = "lbl_UnitShadowEffect"
        '
        'lbl_ConvectionSlab
        '
        Me.lbl_ConvectionSlab.AutoSize = True
        Me.lbl_ConvectionSlab.Location = New System.Drawing.Point(10, 269)
        Me.lbl_ConvectionSlab.Name = "lbl_ConvectionSlab"
        Me.lbl_ConvectionSlab.Size = New System.Drawing.Size(98, 13)
        Me.lbl_ConvectionSlab.TabIndex = 49
        Me.lbl_ConvectionSlab.Text = "lbl_ConvectionSlab"
        '
        'lbl_UnitConvectionFactor
        '
        Me.lbl_UnitConvectionFactor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitConvectionFactor.AutoSize = True
        Me.lbl_UnitConvectionFactor.Location = New System.Drawing.Point(291, 197)
        Me.lbl_UnitConvectionFactor.Name = "lbl_UnitConvectionFactor"
        Me.lbl_UnitConvectionFactor.Size = New System.Drawing.Size(126, 13)
        Me.lbl_UnitConvectionFactor.TabIndex = 45
        Me.lbl_UnitConvectionFactor.Text = "lbl_UnitConvectionFactor"
        '
        'lbl_ShadowEffect
        '
        Me.lbl_ShadowEffect.AutoSize = True
        Me.lbl_ShadowEffect.Location = New System.Drawing.Point(10, 221)
        Me.lbl_ShadowEffect.Name = "lbl_ShadowEffect"
        Me.lbl_ShadowEffect.Size = New System.Drawing.Size(90, 13)
        Me.lbl_ShadowEffect.TabIndex = 46
        Me.lbl_ShadowEffect.Text = "lbl_ShadowEffect"
        '
        'lbl_UnitEmissivityFire
        '
        Me.lbl_UnitEmissivityFire.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitEmissivityFire.AutoSize = True
        Me.lbl_UnitEmissivityFire.Location = New System.Drawing.Point(291, 150)
        Me.lbl_UnitEmissivityFire.Name = "lbl_UnitEmissivityFire"
        Me.lbl_UnitEmissivityFire.Size = New System.Drawing.Size(104, 13)
        Me.lbl_UnitEmissivityFire.TabIndex = 42
        Me.lbl_UnitEmissivityFire.Text = "lbl_UnitEmissivityFire"
        '
        'lbl_ConvectionFactor
        '
        Me.lbl_ConvectionFactor.AutoSize = True
        Me.lbl_ConvectionFactor.Location = New System.Drawing.Point(10, 176)
        Me.lbl_ConvectionFactor.Name = "lbl_ConvectionFactor"
        Me.lbl_ConvectionFactor.Size = New System.Drawing.Size(107, 13)
        Me.lbl_ConvectionFactor.TabIndex = 43
        Me.lbl_ConvectionFactor.Text = "lbl_ConvectionFactor"
        '
        'lbl_EmissivityFire
        '
        Me.lbl_EmissivityFire.AutoSize = True
        Me.lbl_EmissivityFire.Location = New System.Drawing.Point(10, 150)
        Me.lbl_EmissivityFire.Name = "lbl_EmissivityFire"
        Me.lbl_EmissivityFire.Size = New System.Drawing.Size(85, 13)
        Me.lbl_EmissivityFire.TabIndex = 40
        Me.lbl_EmissivityFire.Text = "lbl_EmissivityFire"
        '
        'lbl_UnitFormFactor
        '
        Me.lbl_UnitFormFactor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitFormFactor.AutoSize = True
        Me.lbl_UnitFormFactor.Location = New System.Drawing.Point(291, 124)
        Me.lbl_UnitFormFactor.Name = "lbl_UnitFormFactor"
        Me.lbl_UnitFormFactor.Size = New System.Drawing.Size(95, 13)
        Me.lbl_UnitFormFactor.TabIndex = 39
        Me.lbl_UnitFormFactor.Text = "lbl_UnitFormFactor"
        '
        'lbl_UnitMaxTemp
        '
        Me.lbl_UnitMaxTemp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitMaxTemp.AutoSize = True
        Me.lbl_UnitMaxTemp.Location = New System.Drawing.Point(291, 98)
        Me.lbl_UnitMaxTemp.Name = "lbl_UnitMaxTemp"
        Me.lbl_UnitMaxTemp.Size = New System.Drawing.Size(89, 13)
        Me.lbl_UnitMaxTemp.TabIndex = 36
        Me.lbl_UnitMaxTemp.Text = "lbl_UnitMaxTemp"
        '
        'lbl_FormFactor
        '
        Me.lbl_FormFactor.AutoSize = True
        Me.lbl_FormFactor.Location = New System.Drawing.Point(10, 124)
        Me.lbl_FormFactor.Name = "lbl_FormFactor"
        Me.lbl_FormFactor.Size = New System.Drawing.Size(76, 13)
        Me.lbl_FormFactor.TabIndex = 37
        Me.lbl_FormFactor.Text = "lbl_FormFactor"
        '
        'lbl_UnitReferenceTemp
        '
        Me.lbl_UnitReferenceTemp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitReferenceTemp.AutoSize = True
        Me.lbl_UnitReferenceTemp.Location = New System.Drawing.Point(291, 72)
        Me.lbl_UnitReferenceTemp.Name = "lbl_UnitReferenceTemp"
        Me.lbl_UnitReferenceTemp.Size = New System.Drawing.Size(119, 13)
        Me.lbl_UnitReferenceTemp.TabIndex = 33
        Me.lbl_UnitReferenceTemp.Text = "lbl_UnitReferenceTemp"
        '
        'lbl_MaxTemp
        '
        Me.lbl_MaxTemp.AutoSize = True
        Me.lbl_MaxTemp.Location = New System.Drawing.Point(10, 98)
        Me.lbl_MaxTemp.Name = "lbl_MaxTemp"
        Me.lbl_MaxTemp.Size = New System.Drawing.Size(70, 13)
        Me.lbl_MaxTemp.TabIndex = 34
        Me.lbl_MaxTemp.Text = "lbl_MaxTemp"
        '
        'lbl_UnitTimeIncrement
        '
        Me.lbl_UnitTimeIncrement.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitTimeIncrement.AutoSize = True
        Me.lbl_UnitTimeIncrement.Location = New System.Drawing.Point(291, 46)
        Me.lbl_UnitTimeIncrement.Name = "lbl_UnitTimeIncrement"
        Me.lbl_UnitTimeIncrement.Size = New System.Drawing.Size(112, 13)
        Me.lbl_UnitTimeIncrement.TabIndex = 30
        Me.lbl_UnitTimeIncrement.Text = "lbl_UnitTimeIncrement"
        '
        'lbl_ReferenceTemp
        '
        Me.lbl_ReferenceTemp.AutoSize = True
        Me.lbl_ReferenceTemp.Location = New System.Drawing.Point(10, 72)
        Me.lbl_ReferenceTemp.Name = "lbl_ReferenceTemp"
        Me.lbl_ReferenceTemp.Size = New System.Drawing.Size(100, 13)
        Me.lbl_ReferenceTemp.TabIndex = 31
        Me.lbl_ReferenceTemp.Text = "lbl_ReferenceTemp"
        '
        'lbl_TimeIncrement
        '
        Me.lbl_TimeIncrement.AutoSize = True
        Me.lbl_TimeIncrement.Location = New System.Drawing.Point(10, 46)
        Me.lbl_TimeIncrement.Name = "lbl_TimeIncrement"
        Me.lbl_TimeIncrement.Size = New System.Drawing.Size(93, 13)
        Me.lbl_TimeIncrement.TabIndex = 28
        Me.lbl_TimeIncrement.Text = "lbl_TimeIncrement"
        '
        'txt_ConcreteResistance
        '
        Me.txt_ConcreteResistance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ConcreteResistance.Location = New System.Drawing.Point(228, 338)
        Me.txt_ConcreteResistance.Name = "txt_ConcreteResistance"
        Me.txt_ConcreteResistance.Size = New System.Drawing.Size(58, 20)
        Me.txt_ConcreteResistance.TabIndex = 53
        '
        'txt_ConvectionSlab
        '
        Me.txt_ConvectionSlab.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ConvectionSlab.Location = New System.Drawing.Point(228, 288)
        Me.txt_ConvectionSlab.Name = "txt_ConvectionSlab"
        Me.txt_ConvectionSlab.Size = New System.Drawing.Size(58, 20)
        Me.txt_ConvectionSlab.TabIndex = 50
        '
        'img_ConcreteResistance
        '
        Me.img_ConcreteResistance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ConcreteResistance.Location = New System.Drawing.Point(183, 338)
        Me.img_ConcreteResistance.Name = "img_ConcreteResistance"
        Me.img_ConcreteResistance.Size = New System.Drawing.Size(46, 20)
        Me.img_ConcreteResistance.TabIndex = 78
        Me.img_ConcreteResistance.TabStop = False
        '
        'txt_ShadowEffect
        '
        Me.txt_ShadowEffect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ShadowEffect.Location = New System.Drawing.Point(228, 240)
        Me.txt_ShadowEffect.Name = "txt_ShadowEffect"
        Me.txt_ShadowEffect.Size = New System.Drawing.Size(58, 20)
        Me.txt_ShadowEffect.TabIndex = 47
        '
        'img_ConvectionSlab
        '
        Me.img_ConvectionSlab.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ConvectionSlab.Location = New System.Drawing.Point(183, 288)
        Me.img_ConvectionSlab.Name = "img_ConvectionSlab"
        Me.img_ConvectionSlab.Size = New System.Drawing.Size(46, 20)
        Me.img_ConvectionSlab.TabIndex = 78
        Me.img_ConvectionSlab.TabStop = False
        '
        'txt_ConvectionFactor
        '
        Me.txt_ConvectionFactor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ConvectionFactor.Location = New System.Drawing.Point(228, 194)
        Me.txt_ConvectionFactor.Name = "txt_ConvectionFactor"
        Me.txt_ConvectionFactor.Size = New System.Drawing.Size(58, 20)
        Me.txt_ConvectionFactor.TabIndex = 44
        '
        'img_ShadowEffect
        '
        Me.img_ShadowEffect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ShadowEffect.Location = New System.Drawing.Point(183, 240)
        Me.img_ShadowEffect.Name = "img_ShadowEffect"
        Me.img_ShadowEffect.Size = New System.Drawing.Size(46, 20)
        Me.img_ShadowEffect.TabIndex = 78
        Me.img_ShadowEffect.TabStop = False
        '
        'txt_EmissivityFire
        '
        Me.txt_EmissivityFire.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EmissivityFire.Location = New System.Drawing.Point(228, 147)
        Me.txt_EmissivityFire.Name = "txt_EmissivityFire"
        Me.txt_EmissivityFire.Size = New System.Drawing.Size(58, 20)
        Me.txt_EmissivityFire.TabIndex = 41
        '
        'img_ConvectionFactor
        '
        Me.img_ConvectionFactor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ConvectionFactor.Location = New System.Drawing.Point(183, 194)
        Me.img_ConvectionFactor.Name = "img_ConvectionFactor"
        Me.img_ConvectionFactor.Size = New System.Drawing.Size(46, 20)
        Me.img_ConvectionFactor.TabIndex = 78
        Me.img_ConvectionFactor.TabStop = False
        '
        'img_EmissivityFire
        '
        Me.img_EmissivityFire.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_EmissivityFire.Location = New System.Drawing.Point(183, 147)
        Me.img_EmissivityFire.Name = "img_EmissivityFire"
        Me.img_EmissivityFire.Size = New System.Drawing.Size(46, 20)
        Me.img_EmissivityFire.TabIndex = 78
        Me.img_EmissivityFire.TabStop = False
        '
        'txt_FormFactor
        '
        Me.txt_FormFactor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_FormFactor.Location = New System.Drawing.Point(228, 121)
        Me.txt_FormFactor.Name = "txt_FormFactor"
        Me.txt_FormFactor.Size = New System.Drawing.Size(58, 20)
        Me.txt_FormFactor.TabIndex = 38
        '
        'txt_MaxTemp
        '
        Me.txt_MaxTemp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_MaxTemp.Location = New System.Drawing.Point(228, 95)
        Me.txt_MaxTemp.Name = "txt_MaxTemp"
        Me.txt_MaxTemp.Size = New System.Drawing.Size(58, 20)
        Me.txt_MaxTemp.TabIndex = 35
        '
        'img_FormFactor
        '
        Me.img_FormFactor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_FormFactor.Location = New System.Drawing.Point(183, 121)
        Me.img_FormFactor.Name = "img_FormFactor"
        Me.img_FormFactor.Size = New System.Drawing.Size(46, 20)
        Me.img_FormFactor.TabIndex = 78
        Me.img_FormFactor.TabStop = False
        '
        'txt_ReferenceTemp
        '
        Me.txt_ReferenceTemp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ReferenceTemp.Location = New System.Drawing.Point(228, 69)
        Me.txt_ReferenceTemp.Name = "txt_ReferenceTemp"
        Me.txt_ReferenceTemp.Size = New System.Drawing.Size(58, 20)
        Me.txt_ReferenceTemp.TabIndex = 32
        '
        'img_MaxTemp
        '
        Me.img_MaxTemp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_MaxTemp.Location = New System.Drawing.Point(183, 95)
        Me.img_MaxTemp.Name = "img_MaxTemp"
        Me.img_MaxTemp.Size = New System.Drawing.Size(46, 20)
        Me.img_MaxTemp.TabIndex = 78
        Me.img_MaxTemp.TabStop = False
        '
        'txt_TimeIncrement
        '
        Me.txt_TimeIncrement.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TimeIncrement.Location = New System.Drawing.Point(228, 43)
        Me.txt_TimeIncrement.Name = "txt_TimeIncrement"
        Me.txt_TimeIncrement.Size = New System.Drawing.Size(58, 20)
        Me.txt_TimeIncrement.TabIndex = 29
        '
        'lbl_UnitBoltzmann
        '
        Me.lbl_UnitBoltzmann.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitBoltzmann.AutoSize = True
        Me.lbl_UnitBoltzmann.Location = New System.Drawing.Point(291, 21)
        Me.lbl_UnitBoltzmann.Name = "lbl_UnitBoltzmann"
        Me.lbl_UnitBoltzmann.Size = New System.Drawing.Size(91, 13)
        Me.lbl_UnitBoltzmann.TabIndex = 27
        Me.lbl_UnitBoltzmann.Text = "lbl_UnitBoltzmann"
        '
        'img_ReferenceTemp
        '
        Me.img_ReferenceTemp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ReferenceTemp.Location = New System.Drawing.Point(183, 69)
        Me.img_ReferenceTemp.Name = "img_ReferenceTemp"
        Me.img_ReferenceTemp.Size = New System.Drawing.Size(46, 20)
        Me.img_ReferenceTemp.TabIndex = 78
        Me.img_ReferenceTemp.TabStop = False
        '
        'lbl_Boltzmann
        '
        Me.lbl_Boltzmann.AutoSize = True
        Me.lbl_Boltzmann.Location = New System.Drawing.Point(10, 21)
        Me.lbl_Boltzmann.Name = "lbl_Boltzmann"
        Me.lbl_Boltzmann.Size = New System.Drawing.Size(72, 13)
        Me.lbl_Boltzmann.TabIndex = 25
        Me.lbl_Boltzmann.Text = "lbl_Boltzmann"
        '
        'img_TimeIncrement
        '
        Me.img_TimeIncrement.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_TimeIncrement.Location = New System.Drawing.Point(183, 43)
        Me.img_TimeIncrement.Name = "img_TimeIncrement"
        Me.img_TimeIncrement.Size = New System.Drawing.Size(46, 20)
        Me.img_TimeIncrement.TabIndex = 78
        Me.img_TimeIncrement.TabStop = False
        '
        'txt_Boltzmann
        '
        Me.txt_Boltzmann.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Boltzmann.Location = New System.Drawing.Point(228, 18)
        Me.txt_Boltzmann.Name = "txt_Boltzmann"
        Me.txt_Boltzmann.Size = New System.Drawing.Size(58, 20)
        Me.txt_Boltzmann.TabIndex = 26
        '
        'img_Boltzmann
        '
        Me.img_Boltzmann.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Boltzmann.Location = New System.Drawing.Point(183, 18)
        Me.img_Boltzmann.Name = "img_Boltzmann"
        Me.img_Boltzmann.Size = New System.Drawing.Size(46, 20)
        Me.img_Boltzmann.TabIndex = 78
        Me.img_Boltzmann.TabStop = False
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(364, 505)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.pan_CalculOptions, 0, 3)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CalculOptions, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_ParamPoutre, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_ParamPoutre, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 4
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(364, 505)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'pan_CalculOptions
        '
        Me.pan_CalculOptions.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_CalculOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_CalculOptions.Controls.Add(Me.chk_DalleFEM)
        Me.pan_CalculOptions.Controls.Add(Me.chk_ArmaComp)
        Me.pan_CalculOptions.Controls.Add(Me.img_tDalleFEMmax)
        Me.pan_CalculOptions.Controls.Add(Me.lbl_UnittDalleFEMmax)
        Me.pan_CalculOptions.Controls.Add(Me.txt_tDalleFEMmax)
        Me.pan_CalculOptions.Controls.Add(Me.chk_ReductionConcreteStrenght)
        Me.pan_CalculOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_CalculOptions.Location = New System.Drawing.Point(0, 360)
        Me.pan_CalculOptions.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_CalculOptions.Name = "pan_CalculOptions"
        Me.pan_CalculOptions.Size = New System.Drawing.Size(364, 145)
        Me.pan_CalculOptions.TabIndex = 3
        '
        'chk_DalleFEM
        '
        Me.chk_DalleFEM.AutoSize = True
        Me.chk_DalleFEM.Location = New System.Drawing.Point(7, 44)
        Me.chk_DalleFEM.Name = "chk_DalleFEM"
        Me.chk_DalleFEM.Size = New System.Drawing.Size(96, 17)
        Me.chk_DalleFEM.TabIndex = 21
        Me.chk_DalleFEM.Text = "chk_DalleFEM"
        Me.chk_DalleFEM.UseVisualStyleBackColor = True
        '
        'chk_ArmaComp
        '
        Me.chk_ArmaComp.AutoSize = True
        Me.chk_ArmaComp.Location = New System.Drawing.Point(7, 18)
        Me.chk_ArmaComp.Name = "chk_ArmaComp"
        Me.chk_ArmaComp.Size = New System.Drawing.Size(101, 17)
        Me.chk_ArmaComp.TabIndex = 20
        Me.chk_ArmaComp.Text = "chk_ArmaComp"
        Me.chk_ArmaComp.UseVisualStyleBackColor = True
        '
        'img_tDalleFEMmax
        '
        Me.img_tDalleFEMmax.Location = New System.Drawing.Point(131, 65)
        Me.img_tDalleFEMmax.Name = "img_tDalleFEMmax"
        Me.img_tDalleFEMmax.Size = New System.Drawing.Size(46, 20)
        Me.img_tDalleFEMmax.TabIndex = 76
        Me.img_tDalleFEMmax.TabStop = False
        '
        'lbl_UnittDalleFEMmax
        '
        Me.lbl_UnittDalleFEMmax.AutoSize = True
        Me.lbl_UnittDalleFEMmax.Location = New System.Drawing.Point(242, 68)
        Me.lbl_UnittDalleFEMmax.Name = "lbl_UnittDalleFEMmax"
        Me.lbl_UnittDalleFEMmax.Size = New System.Drawing.Size(110, 13)
        Me.lbl_UnittDalleFEMmax.TabIndex = 24
        Me.lbl_UnittDalleFEMmax.Text = "lbl_UnittDalleFEMmax"
        '
        'txt_tDalleFEMmax
        '
        Me.txt_tDalleFEMmax.Location = New System.Drawing.Point(178, 65)
        Me.txt_tDalleFEMmax.Name = "txt_tDalleFEMmax"
        Me.txt_tDalleFEMmax.Size = New System.Drawing.Size(58, 20)
        Me.txt_tDalleFEMmax.TabIndex = 23
        '
        'chk_ReductionConcreteStrenght
        '
        Me.chk_ReductionConcreteStrenght.AutoSize = True
        Me.chk_ReductionConcreteStrenght.Location = New System.Drawing.Point(7, 101)
        Me.chk_ReductionConcreteStrenght.Name = "chk_ReductionConcreteStrenght"
        Me.chk_ReductionConcreteStrenght.Size = New System.Drawing.Size(182, 17)
        Me.chk_ReductionConcreteStrenght.TabIndex = 18
        Me.chk_ReductionConcreteStrenght.Text = "chk_ReductionConcreteStrenght"
        Me.chk_ReductionConcreteStrenght.UseVisualStyleBackColor = True
        '
        'lbl_CalculOptions
        '
        Me.lbl_CalculOptions.AutoSize = True
        Me.lbl_CalculOptions.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CalculOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CalculOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CalculOptions.Location = New System.Drawing.Point(0, 330)
        Me.lbl_CalculOptions.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CalculOptions.Name = "lbl_CalculOptions"
        Me.lbl_CalculOptions.Size = New System.Drawing.Size(364, 30)
        Me.lbl_CalculOptions.TabIndex = 2
        Me.lbl_CalculOptions.Text = "lbl_CalculOptions"
        Me.lbl_CalculOptions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_ParamPoutre
        '
        Me.lbl_ParamPoutre.AutoSize = True
        Me.lbl_ParamPoutre.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_ParamPoutre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ParamPoutre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ParamPoutre.Location = New System.Drawing.Point(0, 0)
        Me.lbl_ParamPoutre.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ParamPoutre.Name = "lbl_ParamPoutre"
        Me.lbl_ParamPoutre.Size = New System.Drawing.Size(364, 30)
        Me.lbl_ParamPoutre.TabIndex = 0
        Me.lbl_ParamPoutre.Text = "lbl_ParamPoutre"
        Me.lbl_ParamPoutre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_ParamPoutre
        '
        Me.pan_ParamPoutre.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ParamPoutre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ParamPoutre.Controls.Add(Me.pan_Protection)
        Me.pan_ParamPoutre.Controls.Add(Me.lbl_tDalleFEMmax)
        Me.pan_ParamPoutre.Controls.Add(Me.chk_CalculFeu)
        Me.pan_ParamPoutre.Controls.Add(Me.chk_ArmaFroid)
        Me.pan_ParamPoutre.Controls.Add(Me.chk_AcierGalva)
        Me.pan_ParamPoutre.Controls.Add(Me.cmb_SurfaceType)
        Me.pan_ParamPoutre.Controls.Add(Me.lbl_SurfaceType)
        Me.pan_ParamPoutre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ParamPoutre.Location = New System.Drawing.Point(0, 30)
        Me.pan_ParamPoutre.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_ParamPoutre.Name = "pan_ParamPoutre"
        Me.pan_ParamPoutre.Size = New System.Drawing.Size(364, 299)
        Me.pan_ParamPoutre.TabIndex = 1
        '
        'pan_Protection
        '
        Me.pan_Protection.Controls.Add(Me.etq_UnitD)
        Me.pan_Protection.Controls.Add(Me.lbl_EpProtec)
        Me.pan_Protection.Controls.Add(Me.txt_EpProtec)
        Me.pan_Protection.Controls.Add(Me.img_EpProtec)
        Me.pan_Protection.Controls.Add(Me.cmb_ProtectionType)
        Me.pan_Protection.Controls.Add(Me.lbl_UnitSpecificHeat)
        Me.pan_Protection.Controls.Add(Me.lbl_ProtectionType)
        Me.pan_Protection.Controls.Add(Me.lbl_SpecificHeat)
        Me.pan_Protection.Controls.Add(Me.lbl_UnitThermalCond)
        Me.pan_Protection.Controls.Add(Me.lbl_InsulationType)
        Me.pan_Protection.Controls.Add(Me.cmb_InsulationType)
        Me.pan_Protection.Controls.Add(Me.lbl_UnitDensity)
        Me.pan_Protection.Controls.Add(Me.lbl_ThermalConductivity)
        Me.pan_Protection.Controls.Add(Me.lbl_Density)
        Me.pan_Protection.Controls.Add(Me.txt_SpecificHeat)
        Me.pan_Protection.Controls.Add(Me.img_SpecificHeat)
        Me.pan_Protection.Controls.Add(Me.txt_Density)
        Me.pan_Protection.Controls.Add(Me.img_Density)
        Me.pan_Protection.Controls.Add(Me.txt_ThermalConductivity)
        Me.pan_Protection.Controls.Add(Me.img_ThermalConductivity)
        Me.pan_Protection.Location = New System.Drawing.Point(3, 88)
        Me.pan_Protection.Name = "pan_Protection"
        Me.pan_Protection.Size = New System.Drawing.Size(357, 168)
        Me.pan_Protection.TabIndex = 79
        '
        'etq_UnitD
        '
        Me.etq_UnitD.AutoSize = True
        Me.etq_UnitD.Location = New System.Drawing.Point(239, 136)
        Me.etq_UnitD.Name = "etq_UnitD"
        Me.etq_UnitD.Size = New System.Drawing.Size(55, 13)
        Me.etq_UnitD.TabIndex = 79
        Me.etq_UnitD.Text = "etq_UnitD"
        '
        'lbl_EpProtec
        '
        Me.lbl_EpProtec.AutoSize = True
        Me.lbl_EpProtec.Location = New System.Drawing.Point(10, 135)
        Me.lbl_EpProtec.Name = "lbl_EpProtec"
        Me.lbl_EpProtec.Size = New System.Drawing.Size(67, 13)
        Me.lbl_EpProtec.TabIndex = 77
        Me.lbl_EpProtec.Text = "lbl_EpProtec"
        '
        'txt_EpProtec
        '
        Me.txt_EpProtec.Location = New System.Drawing.Point(175, 132)
        Me.txt_EpProtec.Name = "txt_EpProtec"
        Me.txt_EpProtec.Size = New System.Drawing.Size(58, 20)
        Me.txt_EpProtec.TabIndex = 78
        '
        'img_EpProtec
        '
        Me.img_EpProtec.Location = New System.Drawing.Point(130, 132)
        Me.img_EpProtec.Name = "img_EpProtec"
        Me.img_EpProtec.Size = New System.Drawing.Size(46, 20)
        Me.img_EpProtec.TabIndex = 80
        Me.img_EpProtec.TabStop = False
        '
        'cmb_ProtectionType
        '
        Me.cmb_ProtectionType.FormattingEnabled = True
        Me.cmb_ProtectionType.Location = New System.Drawing.Point(112, 3)
        Me.cmb_ProtectionType.Name = "cmb_ProtectionType"
        Me.cmb_ProtectionType.Size = New System.Drawing.Size(186, 21)
        Me.cmb_ProtectionType.TabIndex = 6
        '
        'lbl_UnitSpecificHeat
        '
        Me.lbl_UnitSpecificHeat.AutoSize = True
        Me.lbl_UnitSpecificHeat.Location = New System.Drawing.Point(239, 110)
        Me.lbl_UnitSpecificHeat.Name = "lbl_UnitSpecificHeat"
        Me.lbl_UnitSpecificHeat.Size = New System.Drawing.Size(103, 13)
        Me.lbl_UnitSpecificHeat.TabIndex = 17
        Me.lbl_UnitSpecificHeat.Text = "lbl_UnitSpecificHeat"
        '
        'lbl_ProtectionType
        '
        Me.lbl_ProtectionType.AutoSize = True
        Me.lbl_ProtectionType.Location = New System.Drawing.Point(7, 6)
        Me.lbl_ProtectionType.Name = "lbl_ProtectionType"
        Me.lbl_ProtectionType.Size = New System.Drawing.Size(95, 13)
        Me.lbl_ProtectionType.TabIndex = 5
        Me.lbl_ProtectionType.Text = "lbl_ProtectionType"
        '
        'lbl_SpecificHeat
        '
        Me.lbl_SpecificHeat.AutoSize = True
        Me.lbl_SpecificHeat.Location = New System.Drawing.Point(10, 109)
        Me.lbl_SpecificHeat.Name = "lbl_SpecificHeat"
        Me.lbl_SpecificHeat.Size = New System.Drawing.Size(84, 13)
        Me.lbl_SpecificHeat.TabIndex = 15
        Me.lbl_SpecificHeat.Text = "lbl_SpecificHeat"
        '
        'lbl_UnitThermalCond
        '
        Me.lbl_UnitThermalCond.AutoSize = True
        Me.lbl_UnitThermalCond.Location = New System.Drawing.Point(239, 85)
        Me.lbl_UnitThermalCond.Name = "lbl_UnitThermalCond"
        Me.lbl_UnitThermalCond.Size = New System.Drawing.Size(105, 13)
        Me.lbl_UnitThermalCond.TabIndex = 14
        Me.lbl_UnitThermalCond.Text = "lbl_UnitThermalCond"
        '
        'lbl_InsulationType
        '
        Me.lbl_InsulationType.AutoSize = True
        Me.lbl_InsulationType.Location = New System.Drawing.Point(7, 33)
        Me.lbl_InsulationType.Name = "lbl_InsulationType"
        Me.lbl_InsulationType.Size = New System.Drawing.Size(92, 13)
        Me.lbl_InsulationType.TabIndex = 7
        Me.lbl_InsulationType.Text = "lbl_InsulationType"
        '
        'cmb_InsulationType
        '
        Me.cmb_InsulationType.FormattingEnabled = True
        Me.cmb_InsulationType.Location = New System.Drawing.Point(112, 30)
        Me.cmb_InsulationType.Name = "cmb_InsulationType"
        Me.cmb_InsulationType.Size = New System.Drawing.Size(186, 21)
        Me.cmb_InsulationType.TabIndex = 8
        '
        'lbl_UnitDensity
        '
        Me.lbl_UnitDensity.AutoSize = True
        Me.lbl_UnitDensity.Location = New System.Drawing.Point(239, 61)
        Me.lbl_UnitDensity.Name = "lbl_UnitDensity"
        Me.lbl_UnitDensity.Size = New System.Drawing.Size(77, 13)
        Me.lbl_UnitDensity.TabIndex = 11
        Me.lbl_UnitDensity.Text = "lbl_UnitDensity"
        '
        'lbl_ThermalConductivity
        '
        Me.lbl_ThermalConductivity.AutoSize = True
        Me.lbl_ThermalConductivity.Location = New System.Drawing.Point(10, 84)
        Me.lbl_ThermalConductivity.Name = "lbl_ThermalConductivity"
        Me.lbl_ThermalConductivity.Size = New System.Drawing.Size(119, 13)
        Me.lbl_ThermalConductivity.TabIndex = 12
        Me.lbl_ThermalConductivity.Text = "lbl_ThermalConductivity"
        '
        'lbl_Density
        '
        Me.lbl_Density.AutoSize = True
        Me.lbl_Density.Location = New System.Drawing.Point(10, 59)
        Me.lbl_Density.Name = "lbl_Density"
        Me.lbl_Density.Size = New System.Drawing.Size(58, 13)
        Me.lbl_Density.TabIndex = 9
        Me.lbl_Density.Text = "lbl_Density"
        '
        'txt_SpecificHeat
        '
        Me.txt_SpecificHeat.Location = New System.Drawing.Point(175, 106)
        Me.txt_SpecificHeat.Name = "txt_SpecificHeat"
        Me.txt_SpecificHeat.Size = New System.Drawing.Size(58, 20)
        Me.txt_SpecificHeat.TabIndex = 16
        '
        'img_SpecificHeat
        '
        Me.img_SpecificHeat.Location = New System.Drawing.Point(130, 106)
        Me.img_SpecificHeat.Name = "img_SpecificHeat"
        Me.img_SpecificHeat.Size = New System.Drawing.Size(46, 20)
        Me.img_SpecificHeat.TabIndex = 76
        Me.img_SpecificHeat.TabStop = False
        '
        'txt_Density
        '
        Me.txt_Density.Location = New System.Drawing.Point(175, 57)
        Me.txt_Density.Name = "txt_Density"
        Me.txt_Density.Size = New System.Drawing.Size(58, 20)
        Me.txt_Density.TabIndex = 10
        '
        'img_Density
        '
        Me.img_Density.Location = New System.Drawing.Point(130, 57)
        Me.img_Density.Name = "img_Density"
        Me.img_Density.Size = New System.Drawing.Size(46, 20)
        Me.img_Density.TabIndex = 76
        Me.img_Density.TabStop = False
        '
        'txt_ThermalConductivity
        '
        Me.txt_ThermalConductivity.Location = New System.Drawing.Point(175, 81)
        Me.txt_ThermalConductivity.Name = "txt_ThermalConductivity"
        Me.txt_ThermalConductivity.Size = New System.Drawing.Size(58, 20)
        Me.txt_ThermalConductivity.TabIndex = 13
        '
        'img_ThermalConductivity
        '
        Me.img_ThermalConductivity.Location = New System.Drawing.Point(130, 81)
        Me.img_ThermalConductivity.Name = "img_ThermalConductivity"
        Me.img_ThermalConductivity.Size = New System.Drawing.Size(46, 20)
        Me.img_ThermalConductivity.TabIndex = 76
        Me.img_ThermalConductivity.TabStop = False
        '
        'lbl_tDalleFEMmax
        '
        Me.lbl_tDalleFEMmax.AutoSize = True
        Me.lbl_tDalleFEMmax.Location = New System.Drawing.Point(8, 397)
        Me.lbl_tDalleFEMmax.Name = "lbl_tDalleFEMmax"
        Me.lbl_tDalleFEMmax.Size = New System.Drawing.Size(91, 13)
        Me.lbl_tDalleFEMmax.TabIndex = 22
        Me.lbl_tDalleFEMmax.Text = "lbl_tDalleFEMmax"
        '
        'chk_CalculFeu
        '
        Me.chk_CalculFeu.AutoSize = True
        Me.chk_CalculFeu.Location = New System.Drawing.Point(7, 21)
        Me.chk_CalculFeu.Name = "chk_CalculFeu"
        Me.chk_CalculFeu.Size = New System.Drawing.Size(97, 17)
        Me.chk_CalculFeu.TabIndex = 1
        Me.chk_CalculFeu.Text = "chk_CalculFeu"
        Me.chk_CalculFeu.UseVisualStyleBackColor = True
        '
        'chk_ArmaFroid
        '
        Me.chk_ArmaFroid.AutoSize = True
        Me.chk_ArmaFroid.Location = New System.Drawing.Point(7, 268)
        Me.chk_ArmaFroid.Name = "chk_ArmaFroid"
        Me.chk_ArmaFroid.Size = New System.Drawing.Size(97, 17)
        Me.chk_ArmaFroid.TabIndex = 19
        Me.chk_ArmaFroid.Text = "chk_ArmaFroid"
        Me.chk_ArmaFroid.UseVisualStyleBackColor = True
        '
        'chk_AcierGalva
        '
        Me.chk_AcierGalva.AutoSize = True
        Me.chk_AcierGalva.Location = New System.Drawing.Point(7, 68)
        Me.chk_AcierGalva.Name = "chk_AcierGalva"
        Me.chk_AcierGalva.Size = New System.Drawing.Size(102, 17)
        Me.chk_AcierGalva.TabIndex = 4
        Me.chk_AcierGalva.Text = "chk_AcierGalva"
        Me.chk_AcierGalva.UseVisualStyleBackColor = True
        '
        'cmb_SurfaceType
        '
        Me.cmb_SurfaceType.FormattingEnabled = True
        Me.cmb_SurfaceType.Location = New System.Drawing.Point(115, 46)
        Me.cmb_SurfaceType.Name = "cmb_SurfaceType"
        Me.cmb_SurfaceType.Size = New System.Drawing.Size(186, 21)
        Me.cmb_SurfaceType.TabIndex = 3
        '
        'lbl_SurfaceType
        '
        Me.lbl_SurfaceType.AutoSize = True
        Me.lbl_SurfaceType.Location = New System.Drawing.Point(10, 49)
        Me.lbl_SurfaceType.Name = "lbl_SurfaceType"
        Me.lbl_SurfaceType.Size = New System.Drawing.Size(84, 13)
        Me.lbl_SurfaceType.TabIndex = 2
        Me.lbl_SurfaceType.Text = "lbl_SurfaceType"
        '
        'ErrorProvider_Frm_OptionsFeu
        '
        Me.ErrorProvider_Frm_OptionsFeu.ContainerControl = Me
        '
        'Frm_OptionsFeu
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(779, 551)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Frm_OptionsFeu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_OptionsFeu"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_PartieHaute.ResumeLayout(False)
        Me.pan_Droite.ResumeLayout(False)
        Me.TLPan_Droite.ResumeLayout(False)
        Me.TLPan_Droite.PerformLayout()
        Me.pan_ParamCalcul.ResumeLayout(False)
        Me.pan_ParamCalcul.PerformLayout()
        CType(Me.img_ConcreteResistance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ConvectionSlab, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ShadowEffect, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ConvectionFactor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_EmissivityFire, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_FormFactor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_MaxTemp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ReferenceTemp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_TimeIncrement, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Boltzmann, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_CalculOptions.ResumeLayout(False)
        Me.pan_CalculOptions.PerformLayout()
        CType(Me.img_tDalleFEMmax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_ParamPoutre.ResumeLayout(False)
        Me.pan_ParamPoutre.PerformLayout()
        Me.pan_Protection.ResumeLayout(False)
        Me.pan_Protection.PerformLayout()
        CType(Me.img_EpProtec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_SpecificHeat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Density, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ThermalConductivity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider_Frm_OptionsFeu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_PartieHaute As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_ParamPoutre As Label
    Friend WithEvents pan_ParamPoutre As Panel
    Friend WithEvents pan_Droite As Panel
    Friend WithEvents TLPan_Droite As TableLayoutPanel
    Friend WithEvents lbl_ParamCalcul As Label
    Friend WithEvents pan_ParamCalcul As Panel
    Friend WithEvents cmb_InsulationType As ComboBox
    Friend WithEvents lbl_InsulationType As Label
    Friend WithEvents cmb_ProtectionType As ComboBox
    Friend WithEvents lbl_ProtectionType As Label
    Friend WithEvents cmb_SurfaceType As ComboBox
    Friend WithEvents lbl_SurfaceType As Label
    Friend WithEvents txt_Density As TextBox
    Friend WithEvents img_Density As PictureBox
    Friend WithEvents chk_ReductionConcreteStrenght As CheckBox
    Friend WithEvents txt_SpecificHeat As TextBox
    Friend WithEvents img_SpecificHeat As PictureBox
    Friend WithEvents txt_ThermalConductivity As TextBox
    Friend WithEvents img_ThermalConductivity As PictureBox
    Friend WithEvents lbl_TimeIncrement As Label
    Friend WithEvents txt_TimeIncrement As TextBox
    Friend WithEvents lbl_Boltzmann As Label
    Friend WithEvents img_TimeIncrement As PictureBox
    Friend WithEvents txt_Boltzmann As TextBox
    Friend WithEvents img_Boltzmann As PictureBox
    Friend WithEvents lbl_FormFactor As Label
    Friend WithEvents lbl_MaxTemp As Label
    Friend WithEvents lbl_ReferenceTemp As Label
    Friend WithEvents txt_FormFactor As TextBox
    Friend WithEvents txt_MaxTemp As TextBox
    Friend WithEvents img_FormFactor As PictureBox
    Friend WithEvents txt_ReferenceTemp As TextBox
    Friend WithEvents img_MaxTemp As PictureBox
    Friend WithEvents img_ReferenceTemp As PictureBox
    Friend WithEvents lbl_ConvectionFactor As Label
    Friend WithEvents lbl_EmissivityFire As Label
    Friend WithEvents txt_ConvectionFactor As TextBox
    Friend WithEvents txt_EmissivityFire As TextBox
    Friend WithEvents img_ConvectionFactor As PictureBox
    Friend WithEvents img_EmissivityFire As PictureBox
    Friend WithEvents lbl_ConcreteResistance As Label
    Friend WithEvents lbl_ConvectionSlab As Label
    Friend WithEvents lbl_ShadowEffect As Label
    Friend WithEvents txt_ConcreteResistance As TextBox
    Friend WithEvents txt_ConvectionSlab As TextBox
    Friend WithEvents img_ConcreteResistance As PictureBox
    Friend WithEvents txt_ShadowEffect As TextBox
    Friend WithEvents img_ConvectionSlab As PictureBox
    Friend WithEvents img_ShadowEffect As PictureBox
    Friend WithEvents lbl_Density As Label
    Friend WithEvents lbl_SpecificHeat As Label
    Friend WithEvents lbl_ThermalConductivity As Label
    Friend WithEvents lbl_UnitConcreteResistance As Label
    Friend WithEvents lbl_UnitConvectionSlab As Label
    Friend WithEvents lbl_UnitShadowEffect As Label
    Friend WithEvents lbl_UnitConvectionFactor As Label
    Friend WithEvents lbl_UnitEmissivityFire As Label
    Friend WithEvents lbl_UnitFormFactor As Label
    Friend WithEvents lbl_UnitMaxTemp As Label
    Friend WithEvents lbl_UnitReferenceTemp As Label
    Friend WithEvents lbl_UnitTimeIncrement As Label
    Friend WithEvents lbl_UnitBoltzmann As Label
    Friend WithEvents lbl_UnitSpecificHeat As Label
    Friend WithEvents lbl_UnitThermalCond As Label
    Friend WithEvents lbl_UnitDensity As Label
    Friend WithEvents ErrorProvider_Frm_OptionsFeu As ErrorProvider
    Friend WithEvents chk_CalculFeu As CheckBox
    Friend WithEvents chk_ArmaComp As CheckBox
    Friend WithEvents chk_ArmaFroid As CheckBox
    Friend WithEvents chk_DalleFEM As CheckBox
    Friend WithEvents chk_AcierGalva As CheckBox
    Friend WithEvents lbl_UnittDalleFEMmax As Label
    Friend WithEvents lbl_tDalleFEMmax As Label
    Friend WithEvents txt_tDalleFEMmax As TextBox
    Friend WithEvents img_tDalleFEMmax As PictureBox
    Friend WithEvents pan_CalculOptions As Panel
    Friend WithEvents lbl_CalculOptions As Label
    Friend WithEvents pan_Protection As Panel
    Friend WithEvents etq_UnitD As Label
    Friend WithEvents lbl_EpProtec As Label
    Friend WithEvents txt_EpProtec As TextBox
    Friend WithEvents img_EpProtec As PictureBox
End Class
