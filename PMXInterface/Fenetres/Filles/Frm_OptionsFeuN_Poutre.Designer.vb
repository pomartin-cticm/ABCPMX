<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsFeuN_Poutre
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
        Me.pan_ParamPoutre = New System.Windows.Forms.Panel()
        Me.pan_SurfaceAcier = New System.Windows.Forms.Panel()
        Me.chk_ProtectionThermique = New System.Windows.Forms.CheckBox()
        Me.chk_AcierGalva = New System.Windows.Forms.CheckBox()
        Me.cmb_SurfaceType = New System.Windows.Forms.ComboBox()
        Me.lbl_SurfaceType = New System.Windows.Forms.Label()
        Me.pan_Protection = New System.Windows.Forms.Panel()
        Me.chk_ProtectionCreuxOndes = New System.Windows.Forms.CheckBox()
        Me.etq_UnitD2 = New System.Windows.Forms.Label()
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
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_General = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_ParamPoutre = New System.Windows.Forms.Label()
        Me.ErrorProvider_OptionsFeu = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_ParamPoutre.SuspendLayout()
        Me.pan_SurfaceAcier.SuspendLayout()
        Me.pan_Protection.SuspendLayout()
        CType(Me.img_EpProtec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_SpecificHeat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Density, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ThermalConductivity, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_General.SuspendLayout()
        Me.TLpan_General.SuspendLayout()
        CType(Me.ErrorProvider_OptionsFeu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_ParamPoutre
        '
        Me.pan_ParamPoutre.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ParamPoutre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ParamPoutre.Controls.Add(Me.pan_SurfaceAcier)
        Me.pan_ParamPoutre.Controls.Add(Me.cmb_SurfaceType)
        Me.pan_ParamPoutre.Controls.Add(Me.lbl_SurfaceType)
        Me.pan_ParamPoutre.Controls.Add(Me.pan_Protection)
        Me.pan_ParamPoutre.Controls.Add(Me.lbl_tDalleFEMmax)
        Me.pan_ParamPoutre.Controls.Add(Me.chk_CalculFeu)
        Me.pan_ParamPoutre.Controls.Add(Me.chk_ArmaFroid)
        Me.pan_ParamPoutre.Dock = System.Windows.Forms.DockStyle.Top
        Me.pan_ParamPoutre.Location = New System.Drawing.Point(0, 30)
        Me.pan_ParamPoutre.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_ParamPoutre.Name = "pan_ParamPoutre"
        Me.pan_ParamPoutre.Size = New System.Drawing.Size(380, 292)
        Me.pan_ParamPoutre.TabIndex = 2
        '
        'pan_SurfaceAcier
        '
        Me.pan_SurfaceAcier.Controls.Add(Me.chk_ProtectionThermique)
        Me.pan_SurfaceAcier.Controls.Add(Me.chk_AcierGalva)
        Me.pan_SurfaceAcier.Location = New System.Drawing.Point(2, 53)
        Me.pan_SurfaceAcier.Name = "pan_SurfaceAcier"
        Me.pan_SurfaceAcier.Size = New System.Drawing.Size(372, 48)
        Me.pan_SurfaceAcier.TabIndex = 124
        '
        'chk_ProtectionThermique
        '
        Me.chk_ProtectionThermique.AutoSize = True
        Me.chk_ProtectionThermique.Location = New System.Drawing.Point(5, 27)
        Me.chk_ProtectionThermique.Name = "chk_ProtectionThermique"
        Me.chk_ProtectionThermique.Size = New System.Drawing.Size(148, 17)
        Me.chk_ProtectionThermique.TabIndex = 80
        Me.chk_ProtectionThermique.Text = "chk_ProtectionThermique"
        Me.chk_ProtectionThermique.UseVisualStyleBackColor = True
        '
        'chk_AcierGalva
        '
        Me.chk_AcierGalva.AutoSize = True
        Me.chk_AcierGalva.Location = New System.Drawing.Point(5, 4)
        Me.chk_AcierGalva.Name = "chk_AcierGalva"
        Me.chk_AcierGalva.Size = New System.Drawing.Size(102, 17)
        Me.chk_AcierGalva.TabIndex = 4
        Me.chk_AcierGalva.Text = "chk_AcierGalva"
        Me.chk_AcierGalva.UseVisualStyleBackColor = True
        '
        'cmb_SurfaceType
        '
        Me.cmb_SurfaceType.FormattingEnabled = True
        Me.cmb_SurfaceType.Location = New System.Drawing.Point(223, 29)
        Me.cmb_SurfaceType.Name = "cmb_SurfaceType"
        Me.cmb_SurfaceType.Size = New System.Drawing.Size(137, 21)
        Me.cmb_SurfaceType.TabIndex = 122
        Me.cmb_SurfaceType.Visible = False
        '
        'lbl_SurfaceType
        '
        Me.lbl_SurfaceType.AutoSize = True
        Me.lbl_SurfaceType.Location = New System.Drawing.Point(261, 12)
        Me.lbl_SurfaceType.Name = "lbl_SurfaceType"
        Me.lbl_SurfaceType.Size = New System.Drawing.Size(84, 13)
        Me.lbl_SurfaceType.TabIndex = 121
        Me.lbl_SurfaceType.Text = "lbl_SurfaceType"
        Me.lbl_SurfaceType.Visible = False
        '
        'pan_Protection
        '
        Me.pan_Protection.Controls.Add(Me.chk_ProtectionCreuxOndes)
        Me.pan_Protection.Controls.Add(Me.etq_UnitD2)
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
        Me.pan_Protection.Location = New System.Drawing.Point(2, 103)
        Me.pan_Protection.Name = "pan_Protection"
        Me.pan_Protection.Size = New System.Drawing.Size(372, 184)
        Me.pan_Protection.TabIndex = 79
        '
        'chk_ProtectionCreuxOndes
        '
        Me.chk_ProtectionCreuxOndes.AutoSize = True
        Me.chk_ProtectionCreuxOndes.Location = New System.Drawing.Point(20, 160)
        Me.chk_ProtectionCreuxOndes.Name = "chk_ProtectionCreuxOndes"
        Me.chk_ProtectionCreuxOndes.Size = New System.Drawing.Size(156, 17)
        Me.chk_ProtectionCreuxOndes.TabIndex = 5
        Me.chk_ProtectionCreuxOndes.Text = "chk_ProtectionCreuxOndes"
        Me.chk_ProtectionCreuxOndes.UseVisualStyleBackColor = True
        '
        'etq_UnitD2
        '
        Me.etq_UnitD2.AutoSize = True
        Me.etq_UnitD2.Location = New System.Drawing.Point(239, 136)
        Me.etq_UnitD2.Name = "etq_UnitD2"
        Me.etq_UnitD2.Size = New System.Drawing.Size(61, 13)
        Me.etq_UnitD2.TabIndex = 79
        Me.etq_UnitD2.Text = "etq_UnitD2"
        '
        'lbl_EpProtec
        '
        Me.lbl_EpProtec.AutoSize = True
        Me.lbl_EpProtec.Location = New System.Drawing.Point(22, 136)
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
        Me.cmb_ProtectionType.Location = New System.Drawing.Point(128, 3)
        Me.cmb_ProtectionType.Name = "cmb_ProtectionType"
        Me.cmb_ProtectionType.Size = New System.Drawing.Size(200, 21)
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
        Me.lbl_ProtectionType.Location = New System.Drawing.Point(22, 6)
        Me.lbl_ProtectionType.Name = "lbl_ProtectionType"
        Me.lbl_ProtectionType.Size = New System.Drawing.Size(95, 13)
        Me.lbl_ProtectionType.TabIndex = 5
        Me.lbl_ProtectionType.Text = "lbl_ProtectionType"
        '
        'lbl_SpecificHeat
        '
        Me.lbl_SpecificHeat.AutoSize = True
        Me.lbl_SpecificHeat.Location = New System.Drawing.Point(22, 110)
        Me.lbl_SpecificHeat.Name = "lbl_SpecificHeat"
        Me.lbl_SpecificHeat.Size = New System.Drawing.Size(84, 13)
        Me.lbl_SpecificHeat.TabIndex = 15
        Me.lbl_SpecificHeat.Text = "lbl_SpecificHeat"
        '
        'lbl_UnitThermalCond
        '
        Me.lbl_UnitThermalCond.AutoSize = True
        Me.lbl_UnitThermalCond.Location = New System.Drawing.Point(239, 84)
        Me.lbl_UnitThermalCond.Name = "lbl_UnitThermalCond"
        Me.lbl_UnitThermalCond.Size = New System.Drawing.Size(105, 13)
        Me.lbl_UnitThermalCond.TabIndex = 14
        Me.lbl_UnitThermalCond.Text = "lbl_UnitThermalCond"
        '
        'lbl_InsulationType
        '
        Me.lbl_InsulationType.AutoSize = True
        Me.lbl_InsulationType.Location = New System.Drawing.Point(22, 33)
        Me.lbl_InsulationType.Name = "lbl_InsulationType"
        Me.lbl_InsulationType.Size = New System.Drawing.Size(92, 13)
        Me.lbl_InsulationType.TabIndex = 7
        Me.lbl_InsulationType.Text = "lbl_InsulationType"
        '
        'cmb_InsulationType
        '
        Me.cmb_InsulationType.FormattingEnabled = True
        Me.cmb_InsulationType.Location = New System.Drawing.Point(128, 30)
        Me.cmb_InsulationType.Name = "cmb_InsulationType"
        Me.cmb_InsulationType.Size = New System.Drawing.Size(200, 21)
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
        Me.lbl_ThermalConductivity.Location = New System.Drawing.Point(22, 85)
        Me.lbl_ThermalConductivity.Name = "lbl_ThermalConductivity"
        Me.lbl_ThermalConductivity.Size = New System.Drawing.Size(119, 13)
        Me.lbl_ThermalConductivity.TabIndex = 12
        Me.lbl_ThermalConductivity.Text = "lbl_ThermalConductivity"
        '
        'lbl_Density
        '
        Me.lbl_Density.AutoSize = True
        Me.lbl_Density.Location = New System.Drawing.Point(22, 60)
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
        Me.chk_CalculFeu.Location = New System.Drawing.Point(7, 8)
        Me.chk_CalculFeu.Name = "chk_CalculFeu"
        Me.chk_CalculFeu.Size = New System.Drawing.Size(97, 17)
        Me.chk_CalculFeu.TabIndex = 1
        Me.chk_CalculFeu.Text = "chk_CalculFeu"
        Me.chk_CalculFeu.UseVisualStyleBackColor = True
        '
        'chk_ArmaFroid
        '
        Me.chk_ArmaFroid.AutoSize = True
        Me.chk_ArmaFroid.Location = New System.Drawing.Point(7, 29)
        Me.chk_ArmaFroid.Name = "chk_ArmaFroid"
        Me.chk_ArmaFroid.Size = New System.Drawing.Size(97, 17)
        Me.chk_ArmaFroid.TabIndex = 19
        Me.chk_ArmaFroid.Text = "chk_ArmaFroid"
        Me.chk_ArmaFroid.UseVisualStyleBackColor = True
        '
        'pan_General
        '
        Me.pan_General.AutoScroll = True
        Me.pan_General.Controls.Add(Me.TLpan_General)
        Me.pan_General.Location = New System.Drawing.Point(12, 12)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(380, 393)
        Me.pan_General.TabIndex = 3
        '
        'TLpan_General
        '
        Me.TLpan_General.ColumnCount = 1
        Me.TLpan_General.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_General.Controls.Add(Me.lbl_ParamPoutre, 0, 0)
        Me.TLpan_General.Controls.Add(Me.pan_ParamPoutre, 0, 1)
        Me.TLpan_General.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLpan_General.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_General.Name = "TLpan_General"
        Me.TLpan_General.RowCount = 2
        Me.TLpan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_General.Size = New System.Drawing.Size(380, 324)
        Me.TLpan_General.TabIndex = 0
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
        Me.lbl_ParamPoutre.Size = New System.Drawing.Size(380, 30)
        Me.lbl_ParamPoutre.TabIndex = 3
        Me.lbl_ParamPoutre.Text = "lbl_ParamPoutre"
        Me.lbl_ParamPoutre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ErrorProvider_OptionsFeu
        '
        Me.ErrorProvider_OptionsFeu.ContainerControl = Me
        '
        'Frm_OptionsFeuN_Poutre
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 565)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsFeuN_Poutre"
        Me.Text = "Frm_OptionsFeuNPoutre"
        Me.pan_ParamPoutre.ResumeLayout(False)
        Me.pan_ParamPoutre.PerformLayout()
        Me.pan_SurfaceAcier.ResumeLayout(False)
        Me.pan_SurfaceAcier.PerformLayout()
        Me.pan_Protection.ResumeLayout(False)
        Me.pan_Protection.PerformLayout()
        CType(Me.img_EpProtec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_SpecificHeat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Density, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ThermalConductivity, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_General.ResumeLayout(False)
        Me.TLpan_General.PerformLayout()
        CType(Me.ErrorProvider_OptionsFeu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_ParamPoutre As Panel
    Friend WithEvents cmb_SurfaceType As ComboBox
    Friend WithEvents chk_ProtectionThermique As CheckBox
    Friend WithEvents lbl_SurfaceType As Label
    Friend WithEvents pan_Protection As Panel
    Friend WithEvents etq_UnitD2 As Label
    Friend WithEvents lbl_EpProtec As Label
    Friend WithEvents txt_EpProtec As TextBox
    Friend WithEvents img_EpProtec As PictureBox
    Friend WithEvents cmb_ProtectionType As ComboBox
    Friend WithEvents lbl_UnitSpecificHeat As Label
    Friend WithEvents lbl_ProtectionType As Label
    Friend WithEvents lbl_SpecificHeat As Label
    Friend WithEvents lbl_UnitThermalCond As Label
    Friend WithEvents lbl_InsulationType As Label
    Friend WithEvents cmb_InsulationType As ComboBox
    Friend WithEvents lbl_UnitDensity As Label
    Friend WithEvents lbl_ThermalConductivity As Label
    Friend WithEvents lbl_Density As Label
    Friend WithEvents txt_SpecificHeat As TextBox
    Friend WithEvents img_SpecificHeat As PictureBox
    Friend WithEvents txt_Density As TextBox
    Friend WithEvents img_Density As PictureBox
    Friend WithEvents txt_ThermalConductivity As TextBox
    Friend WithEvents img_ThermalConductivity As PictureBox
    Friend WithEvents lbl_tDalleFEMmax As Label
    Friend WithEvents chk_CalculFeu As CheckBox
    Friend WithEvents chk_ArmaFroid As CheckBox
    Friend WithEvents chk_AcierGalva As CheckBox
    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_General As TableLayoutPanel
    Friend WithEvents lbl_ParamPoutre As Label
    Friend WithEvents ErrorProvider_OptionsFeu As ErrorProvider
    Friend WithEvents chk_ProtectionCreuxOndes As CheckBox
    Friend WithEvents pan_SurfaceAcier As Panel
End Class
