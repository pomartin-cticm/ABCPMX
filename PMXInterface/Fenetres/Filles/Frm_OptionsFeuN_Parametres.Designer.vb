<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsFeuN_Parametres
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
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_General = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_ParamCalcul = New System.Windows.Forms.Panel()
        Me.lbl_EmissiviteBeton = New System.Windows.Forms.Label()
        Me.txt_EmissiviteBeton = New System.Windows.Forms.TextBox()
        Me.img_EmissiviteBeton = New System.Windows.Forms.PictureBox()
        Me.img_UnitThermConvection2 = New System.Windows.Forms.PictureBox()
        Me.img_UnitThermConvection = New System.Windows.Forms.PictureBox()
        Me.lbl_SurDalle = New System.Windows.Forms.Label()
        Me.lbl_SousDalle = New System.Windows.Forms.Label()
        Me.img_UnitBoltzmann = New System.Windows.Forms.PictureBox()
        Me.lbl_ConcreteResistance = New System.Windows.Forms.Label()
        Me.lbl_ShadowEffect = New System.Windows.Forms.Label()
        Me.lbl_ConvectionFactor = New System.Windows.Forms.Label()
        Me.lbl_EmissivityFire = New System.Windows.Forms.Label()
        Me.etq_UnitTemp2 = New System.Windows.Forms.Label()
        Me.lbl_FormFactor = New System.Windows.Forms.Label()
        Me.etq_UnitTemp1 = New System.Windows.Forms.Label()
        Me.lbl_MaxTemp = New System.Windows.Forms.Label()
        Me.etq_UnitTime = New System.Windows.Forms.Label()
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
        Me.img_ReferenceTemp = New System.Windows.Forms.PictureBox()
        Me.lbl_Boltzmann = New System.Windows.Forms.Label()
        Me.img_TimeIncrement = New System.Windows.Forms.PictureBox()
        Me.txt_Boltzmann = New System.Windows.Forms.TextBox()
        Me.img_Boltzmann = New System.Windows.Forms.PictureBox()
        Me.lbl_ParamCalcul = New System.Windows.Forms.Label()
        Me.ErrorProvider_FeuParam = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_General.SuspendLayout()
        Me.TLpan_General.SuspendLayout()
        Me.pan_ParamCalcul.SuspendLayout()
        CType(Me.img_EmissiviteBeton, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_UnitThermConvection2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_UnitThermConvection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_UnitBoltzmann, System.ComponentModel.ISupportInitialize).BeginInit()
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
        CType(Me.ErrorProvider_FeuParam, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.AutoScroll = True
        Me.pan_General.Controls.Add(Me.TLpan_General)
        Me.pan_General.Location = New System.Drawing.Point(206, 9)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(380, 472)
        Me.pan_General.TabIndex = 4
        '
        'TLpan_General
        '
        Me.TLpan_General.ColumnCount = 1
        Me.TLpan_General.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_General.Controls.Add(Me.pan_ParamCalcul, 0, 1)
        Me.TLpan_General.Controls.Add(Me.lbl_ParamCalcul, 0, 0)
        Me.TLpan_General.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLpan_General.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_General.Name = "TLpan_General"
        Me.TLpan_General.RowCount = 2
        Me.TLpan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_General.Size = New System.Drawing.Size(380, 412)
        Me.TLpan_General.TabIndex = 0
        '
        'pan_ParamCalcul
        '
        Me.pan_ParamCalcul.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ParamCalcul.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_EmissiviteBeton)
        Me.pan_ParamCalcul.Controls.Add(Me.txt_EmissiviteBeton)
        Me.pan_ParamCalcul.Controls.Add(Me.img_EmissiviteBeton)
        Me.pan_ParamCalcul.Controls.Add(Me.img_UnitThermConvection2)
        Me.pan_ParamCalcul.Controls.Add(Me.img_UnitThermConvection)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_SurDalle)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_SousDalle)
        Me.pan_ParamCalcul.Controls.Add(Me.img_UnitBoltzmann)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_ConcreteResistance)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_ShadowEffect)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_ConvectionFactor)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_EmissivityFire)
        Me.pan_ParamCalcul.Controls.Add(Me.etq_UnitTemp2)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_FormFactor)
        Me.pan_ParamCalcul.Controls.Add(Me.etq_UnitTemp1)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_MaxTemp)
        Me.pan_ParamCalcul.Controls.Add(Me.etq_UnitTime)
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
        Me.pan_ParamCalcul.Controls.Add(Me.img_ReferenceTemp)
        Me.pan_ParamCalcul.Controls.Add(Me.lbl_Boltzmann)
        Me.pan_ParamCalcul.Controls.Add(Me.img_TimeIncrement)
        Me.pan_ParamCalcul.Controls.Add(Me.txt_Boltzmann)
        Me.pan_ParamCalcul.Controls.Add(Me.img_Boltzmann)
        Me.pan_ParamCalcul.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ParamCalcul.Location = New System.Drawing.Point(0, 30)
        Me.pan_ParamCalcul.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ParamCalcul.Name = "pan_ParamCalcul"
        Me.pan_ParamCalcul.Size = New System.Drawing.Size(380, 382)
        Me.pan_ParamCalcul.TabIndex = 2
        '
        'lbl_EmissiviteBeton
        '
        Me.lbl_EmissiviteBeton.AutoSize = True
        Me.lbl_EmissiviteBeton.Location = New System.Drawing.Point(10, 176)
        Me.lbl_EmissiviteBeton.Name = "lbl_EmissiviteBeton"
        Me.lbl_EmissiviteBeton.Size = New System.Drawing.Size(97, 13)
        Me.lbl_EmissiviteBeton.TabIndex = 113
        Me.lbl_EmissiviteBeton.Text = "lbl_EmissiviteBeton"
        '
        'txt_EmissiviteBeton
        '
        Me.txt_EmissiviteBeton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EmissiviteBeton.Location = New System.Drawing.Point(214, 173)
        Me.txt_EmissiviteBeton.Name = "txt_EmissiviteBeton"
        Me.txt_EmissiviteBeton.Size = New System.Drawing.Size(58, 20)
        Me.txt_EmissiviteBeton.TabIndex = 114
        '
        'img_EmissiviteBeton
        '
        Me.img_EmissiviteBeton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_EmissiviteBeton.Location = New System.Drawing.Point(169, 173)
        Me.img_EmissiviteBeton.Name = "img_EmissiviteBeton"
        Me.img_EmissiviteBeton.Size = New System.Drawing.Size(46, 20)
        Me.img_EmissiviteBeton.TabIndex = 115
        Me.img_EmissiviteBeton.TabStop = False
        '
        'img_UnitThermConvection2
        '
        Me.img_UnitThermConvection2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_UnitThermConvection2.Location = New System.Drawing.Point(277, 262)
        Me.img_UnitThermConvection2.Name = "img_UnitThermConvection2"
        Me.img_UnitThermConvection2.Size = New System.Drawing.Size(95, 20)
        Me.img_UnitThermConvection2.TabIndex = 112
        Me.img_UnitThermConvection2.TabStop = False
        '
        'img_UnitThermConvection
        '
        Me.img_UnitThermConvection.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_UnitThermConvection.Location = New System.Drawing.Point(277, 236)
        Me.img_UnitThermConvection.Name = "img_UnitThermConvection"
        Me.img_UnitThermConvection.Size = New System.Drawing.Size(95, 20)
        Me.img_UnitThermConvection.TabIndex = 111
        Me.img_UnitThermConvection.TabStop = False
        '
        'lbl_SurDalle
        '
        Me.lbl_SurDalle.AutoSize = True
        Me.lbl_SurDalle.Location = New System.Drawing.Point(56, 265)
        Me.lbl_SurDalle.Name = "lbl_SurDalle"
        Me.lbl_SurDalle.Size = New System.Drawing.Size(63, 13)
        Me.lbl_SurDalle.TabIndex = 98
        Me.lbl_SurDalle.Text = "lbl_SurDalle"
        '
        'lbl_SousDalle
        '
        Me.lbl_SousDalle.AutoSize = True
        Me.lbl_SousDalle.Location = New System.Drawing.Point(56, 239)
        Me.lbl_SousDalle.Name = "lbl_SousDalle"
        Me.lbl_SousDalle.Size = New System.Drawing.Size(71, 13)
        Me.lbl_SousDalle.TabIndex = 97
        Me.lbl_SousDalle.Text = "lbl_SousDalle"
        '
        'img_UnitBoltzmann
        '
        Me.img_UnitBoltzmann.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_UnitBoltzmann.Location = New System.Drawing.Point(277, 18)
        Me.img_UnitBoltzmann.Name = "img_UnitBoltzmann"
        Me.img_UnitBoltzmann.Size = New System.Drawing.Size(95, 20)
        Me.img_UnitBoltzmann.TabIndex = 96
        Me.img_UnitBoltzmann.TabStop = False
        '
        'lbl_ConcreteResistance
        '
        Me.lbl_ConcreteResistance.AutoSize = True
        Me.lbl_ConcreteResistance.Location = New System.Drawing.Point(10, 332)
        Me.lbl_ConcreteResistance.Name = "lbl_ConcreteResistance"
        Me.lbl_ConcreteResistance.Size = New System.Drawing.Size(119, 13)
        Me.lbl_ConcreteResistance.TabIndex = 52
        Me.lbl_ConcreteResistance.Text = "lbl_ConcreteResistance"
        '
        'lbl_ShadowEffect
        '
        Me.lbl_ShadowEffect.AutoSize = True
        Me.lbl_ShadowEffect.Location = New System.Drawing.Point(10, 301)
        Me.lbl_ShadowEffect.Name = "lbl_ShadowEffect"
        Me.lbl_ShadowEffect.Size = New System.Drawing.Size(90, 13)
        Me.lbl_ShadowEffect.TabIndex = 46
        Me.lbl_ShadowEffect.Text = "lbl_ShadowEffect"
        '
        'lbl_ConvectionFactor
        '
        Me.lbl_ConvectionFactor.AutoSize = True
        Me.lbl_ConvectionFactor.Location = New System.Drawing.Point(10, 218)
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
        'etq_UnitTemp2
        '
        Me.etq_UnitTemp2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitTemp2.AutoSize = True
        Me.etq_UnitTemp2.Location = New System.Drawing.Point(277, 98)
        Me.etq_UnitTemp2.Name = "etq_UnitTemp2"
        Me.etq_UnitTemp2.Size = New System.Drawing.Size(80, 13)
        Me.etq_UnitTemp2.TabIndex = 36
        Me.etq_UnitTemp2.Text = "etq_UnitTemp2"
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
        'etq_UnitTemp1
        '
        Me.etq_UnitTemp1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitTemp1.AutoSize = True
        Me.etq_UnitTemp1.Location = New System.Drawing.Point(277, 72)
        Me.etq_UnitTemp1.Name = "etq_UnitTemp1"
        Me.etq_UnitTemp1.Size = New System.Drawing.Size(80, 13)
        Me.etq_UnitTemp1.TabIndex = 33
        Me.etq_UnitTemp1.Text = "etq_UnitTemp1"
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
        'etq_UnitTime
        '
        Me.etq_UnitTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitTime.AutoSize = True
        Me.etq_UnitTime.Location = New System.Drawing.Point(277, 46)
        Me.etq_UnitTime.Name = "etq_UnitTime"
        Me.etq_UnitTime.Size = New System.Drawing.Size(70, 13)
        Me.etq_UnitTime.TabIndex = 30
        Me.etq_UnitTime.Text = "etq_UnitTime"
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
        Me.txt_ConcreteResistance.Location = New System.Drawing.Point(214, 353)
        Me.txt_ConcreteResistance.Name = "txt_ConcreteResistance"
        Me.txt_ConcreteResistance.Size = New System.Drawing.Size(58, 20)
        Me.txt_ConcreteResistance.TabIndex = 53
        '
        'txt_ConvectionSlab
        '
        Me.txt_ConvectionSlab.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ConvectionSlab.Location = New System.Drawing.Point(214, 262)
        Me.txt_ConvectionSlab.Name = "txt_ConvectionSlab"
        Me.txt_ConvectionSlab.Size = New System.Drawing.Size(58, 20)
        Me.txt_ConvectionSlab.TabIndex = 50
        '
        'img_ConcreteResistance
        '
        Me.img_ConcreteResistance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ConcreteResistance.Location = New System.Drawing.Point(169, 353)
        Me.img_ConcreteResistance.Name = "img_ConcreteResistance"
        Me.img_ConcreteResistance.Size = New System.Drawing.Size(46, 20)
        Me.img_ConcreteResistance.TabIndex = 78
        Me.img_ConcreteResistance.TabStop = False
        '
        'txt_ShadowEffect
        '
        Me.txt_ShadowEffect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ShadowEffect.Location = New System.Drawing.Point(214, 298)
        Me.txt_ShadowEffect.Name = "txt_ShadowEffect"
        Me.txt_ShadowEffect.Size = New System.Drawing.Size(58, 20)
        Me.txt_ShadowEffect.TabIndex = 47
        '
        'img_ConvectionSlab
        '
        Me.img_ConvectionSlab.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ConvectionSlab.Location = New System.Drawing.Point(169, 262)
        Me.img_ConvectionSlab.Name = "img_ConvectionSlab"
        Me.img_ConvectionSlab.Size = New System.Drawing.Size(46, 20)
        Me.img_ConvectionSlab.TabIndex = 78
        Me.img_ConvectionSlab.TabStop = False
        '
        'txt_ConvectionFactor
        '
        Me.txt_ConvectionFactor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ConvectionFactor.Location = New System.Drawing.Point(214, 236)
        Me.txt_ConvectionFactor.Name = "txt_ConvectionFactor"
        Me.txt_ConvectionFactor.Size = New System.Drawing.Size(58, 20)
        Me.txt_ConvectionFactor.TabIndex = 44
        '
        'img_ShadowEffect
        '
        Me.img_ShadowEffect.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ShadowEffect.Location = New System.Drawing.Point(169, 298)
        Me.img_ShadowEffect.Name = "img_ShadowEffect"
        Me.img_ShadowEffect.Size = New System.Drawing.Size(46, 20)
        Me.img_ShadowEffect.TabIndex = 78
        Me.img_ShadowEffect.TabStop = False
        '
        'txt_EmissivityFire
        '
        Me.txt_EmissivityFire.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EmissivityFire.Location = New System.Drawing.Point(214, 147)
        Me.txt_EmissivityFire.Name = "txt_EmissivityFire"
        Me.txt_EmissivityFire.Size = New System.Drawing.Size(58, 20)
        Me.txt_EmissivityFire.TabIndex = 41
        '
        'img_ConvectionFactor
        '
        Me.img_ConvectionFactor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ConvectionFactor.Location = New System.Drawing.Point(169, 236)
        Me.img_ConvectionFactor.Name = "img_ConvectionFactor"
        Me.img_ConvectionFactor.Size = New System.Drawing.Size(46, 20)
        Me.img_ConvectionFactor.TabIndex = 78
        Me.img_ConvectionFactor.TabStop = False
        '
        'img_EmissivityFire
        '
        Me.img_EmissivityFire.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_EmissivityFire.Location = New System.Drawing.Point(169, 147)
        Me.img_EmissivityFire.Name = "img_EmissivityFire"
        Me.img_EmissivityFire.Size = New System.Drawing.Size(46, 20)
        Me.img_EmissivityFire.TabIndex = 78
        Me.img_EmissivityFire.TabStop = False
        '
        'txt_FormFactor
        '
        Me.txt_FormFactor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_FormFactor.Location = New System.Drawing.Point(214, 121)
        Me.txt_FormFactor.Name = "txt_FormFactor"
        Me.txt_FormFactor.Size = New System.Drawing.Size(58, 20)
        Me.txt_FormFactor.TabIndex = 38
        '
        'txt_MaxTemp
        '
        Me.txt_MaxTemp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_MaxTemp.Location = New System.Drawing.Point(214, 95)
        Me.txt_MaxTemp.Name = "txt_MaxTemp"
        Me.txt_MaxTemp.Size = New System.Drawing.Size(58, 20)
        Me.txt_MaxTemp.TabIndex = 35
        '
        'img_FormFactor
        '
        Me.img_FormFactor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_FormFactor.Location = New System.Drawing.Point(169, 121)
        Me.img_FormFactor.Name = "img_FormFactor"
        Me.img_FormFactor.Size = New System.Drawing.Size(46, 20)
        Me.img_FormFactor.TabIndex = 78
        Me.img_FormFactor.TabStop = False
        '
        'txt_ReferenceTemp
        '
        Me.txt_ReferenceTemp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ReferenceTemp.Location = New System.Drawing.Point(214, 69)
        Me.txt_ReferenceTemp.Name = "txt_ReferenceTemp"
        Me.txt_ReferenceTemp.Size = New System.Drawing.Size(58, 20)
        Me.txt_ReferenceTemp.TabIndex = 32
        '
        'img_MaxTemp
        '
        Me.img_MaxTemp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_MaxTemp.Location = New System.Drawing.Point(169, 95)
        Me.img_MaxTemp.Name = "img_MaxTemp"
        Me.img_MaxTemp.Size = New System.Drawing.Size(46, 20)
        Me.img_MaxTemp.TabIndex = 78
        Me.img_MaxTemp.TabStop = False
        '
        'txt_TimeIncrement
        '
        Me.txt_TimeIncrement.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TimeIncrement.Location = New System.Drawing.Point(214, 43)
        Me.txt_TimeIncrement.Name = "txt_TimeIncrement"
        Me.txt_TimeIncrement.Size = New System.Drawing.Size(58, 20)
        Me.txt_TimeIncrement.TabIndex = 29
        '
        'img_ReferenceTemp
        '
        Me.img_ReferenceTemp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ReferenceTemp.Location = New System.Drawing.Point(169, 69)
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
        Me.img_TimeIncrement.Location = New System.Drawing.Point(169, 43)
        Me.img_TimeIncrement.Name = "img_TimeIncrement"
        Me.img_TimeIncrement.Size = New System.Drawing.Size(46, 20)
        Me.img_TimeIncrement.TabIndex = 78
        Me.img_TimeIncrement.TabStop = False
        '
        'txt_Boltzmann
        '
        Me.txt_Boltzmann.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Boltzmann.Location = New System.Drawing.Point(214, 18)
        Me.txt_Boltzmann.Name = "txt_Boltzmann"
        Me.txt_Boltzmann.Size = New System.Drawing.Size(58, 20)
        Me.txt_Boltzmann.TabIndex = 26
        '
        'img_Boltzmann
        '
        Me.img_Boltzmann.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Boltzmann.Location = New System.Drawing.Point(169, 18)
        Me.img_Boltzmann.Name = "img_Boltzmann"
        Me.img_Boltzmann.Size = New System.Drawing.Size(46, 20)
        Me.img_Boltzmann.TabIndex = 78
        Me.img_Boltzmann.TabStop = False
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
        Me.lbl_ParamCalcul.Size = New System.Drawing.Size(380, 30)
        Me.lbl_ParamCalcul.TabIndex = 1
        Me.lbl_ParamCalcul.Text = "lbl_ParamCalcul"
        Me.lbl_ParamCalcul.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ErrorProvider_FeuParam
        '
        Me.ErrorProvider_FeuParam.ContainerControl = Me
        '
        'Frm_OptionsFeuN_Parametres
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 572)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsFeuN_Parametres"
        Me.Text = "Frm_OptionsFeuN_Parametres"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_General.ResumeLayout(False)
        Me.TLpan_General.PerformLayout()
        Me.pan_ParamCalcul.ResumeLayout(False)
        Me.pan_ParamCalcul.PerformLayout()
        CType(Me.img_EmissiviteBeton, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_UnitThermConvection2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_UnitThermConvection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_UnitBoltzmann, System.ComponentModel.ISupportInitialize).EndInit()
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
        CType(Me.ErrorProvider_FeuParam, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_General As TableLayoutPanel
    Friend WithEvents lbl_ParamCalcul As Label
    Friend WithEvents pan_ParamCalcul As Panel
    Friend WithEvents lbl_EmissiviteBeton As Label
    Friend WithEvents txt_EmissiviteBeton As TextBox
    Friend WithEvents img_EmissiviteBeton As PictureBox
    Friend WithEvents img_UnitThermConvection2 As PictureBox
    Friend WithEvents img_UnitThermConvection As PictureBox
    Friend WithEvents lbl_SurDalle As Label
    Friend WithEvents lbl_SousDalle As Label
    Friend WithEvents img_UnitBoltzmann As PictureBox
    Friend WithEvents lbl_ConcreteResistance As Label
    Friend WithEvents lbl_ShadowEffect As Label
    Friend WithEvents lbl_ConvectionFactor As Label
    Friend WithEvents lbl_EmissivityFire As Label
    Friend WithEvents etq_UnitTemp2 As Label
    Friend WithEvents lbl_FormFactor As Label
    Friend WithEvents etq_UnitTemp1 As Label
    Friend WithEvents lbl_MaxTemp As Label
    Friend WithEvents etq_UnitTime As Label
    Friend WithEvents lbl_ReferenceTemp As Label
    Friend WithEvents lbl_TimeIncrement As Label
    Friend WithEvents txt_ConcreteResistance As TextBox
    Friend WithEvents txt_ConvectionSlab As TextBox
    Friend WithEvents img_ConcreteResistance As PictureBox
    Friend WithEvents txt_ShadowEffect As TextBox
    Friend WithEvents img_ConvectionSlab As PictureBox
    Friend WithEvents txt_ConvectionFactor As TextBox
    Friend WithEvents img_ShadowEffect As PictureBox
    Friend WithEvents txt_EmissivityFire As TextBox
    Friend WithEvents img_ConvectionFactor As PictureBox
    Friend WithEvents img_EmissivityFire As PictureBox
    Friend WithEvents txt_FormFactor As TextBox
    Friend WithEvents txt_MaxTemp As TextBox
    Friend WithEvents img_FormFactor As PictureBox
    Friend WithEvents txt_ReferenceTemp As TextBox
    Friend WithEvents img_MaxTemp As PictureBox
    Friend WithEvents txt_TimeIncrement As TextBox
    Friend WithEvents img_ReferenceTemp As PictureBox
    Friend WithEvents lbl_Boltzmann As Label
    Friend WithEvents img_TimeIncrement As PictureBox
    Friend WithEvents txt_Boltzmann As TextBox
    Friend WithEvents img_Boltzmann As PictureBox
    Friend WithEvents ErrorProvider_FeuParam As ErrorProvider
End Class
