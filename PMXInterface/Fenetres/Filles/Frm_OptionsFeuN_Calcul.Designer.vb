<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsFeuN_Calcul
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
        Me.pan_CalculOptions = New System.Windows.Forms.Panel()
        Me.pan_TempArma = New System.Windows.Forms.Panel()
        Me.lbl_TempRebars = New System.Windows.Forms.Label()
        Me.cmb_TempRebars = New System.Windows.Forms.ComboBox()
        Me.chk_CongeEnrobe = New System.Windows.Forms.CheckBox()
        Me.pan_OptionsFEM = New System.Windows.Forms.Panel()
        Me.lbl_SizeElt = New System.Windows.Forms.Label()
        Me.etq_UnitU = New System.Windows.Forms.Label()
        Me.chk_RhoCconstante = New System.Windows.Forms.CheckBox()
        Me.txt_U = New System.Windows.Forms.TextBox()
        Me.chk_ANFrance = New System.Windows.Forms.CheckBox()
        Me.img_U = New System.Windows.Forms.PictureBox()
        Me.lbl_TeneurEau = New System.Windows.Forms.Label()
        Me.img_tDalleFEMmax = New System.Windows.Forms.PictureBox()
        Me.txt_tDalleFEMmax = New System.Windows.Forms.TextBox()
        Me.lbl_UnitD1 = New System.Windows.Forms.Label()
        Me.chk_DalleFEM = New System.Windows.Forms.CheckBox()
        Me.chk_ArmaComp = New System.Windows.Forms.CheckBox()
        Me.chk_ReductionConcreteStrenght = New System.Windows.Forms.CheckBox()
        Me.lbl_CalculOptions = New System.Windows.Forms.Label()
        Me.ErrorProvider_FeuCal = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_General.SuspendLayout()
        Me.TLpan_General.SuspendLayout()
        Me.pan_CalculOptions.SuspendLayout()
        Me.pan_TempArma.SuspendLayout()
        Me.pan_OptionsFEM.SuspendLayout()
        CType(Me.img_U, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_tDalleFEMmax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider_FeuCal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.AutoScroll = True
        Me.pan_General.Controls.Add(Me.TLpan_General)
        Me.pan_General.Location = New System.Drawing.Point(246, 118)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(380, 373)
        Me.pan_General.TabIndex = 4
        '
        'TLpan_General
        '
        Me.TLpan_General.ColumnCount = 1
        Me.TLpan_General.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_General.Controls.Add(Me.pan_CalculOptions, 0, 1)
        Me.TLpan_General.Controls.Add(Me.lbl_CalculOptions, 0, 0)
        Me.TLpan_General.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLpan_General.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_General.Name = "TLpan_General"
        Me.TLpan_General.RowCount = 2
        Me.TLpan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_General.Size = New System.Drawing.Size(380, 305)
        Me.TLpan_General.TabIndex = 0
        '
        'pan_CalculOptions
        '
        Me.pan_CalculOptions.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_CalculOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_CalculOptions.Controls.Add(Me.pan_TempArma)
        Me.pan_CalculOptions.Controls.Add(Me.chk_CongeEnrobe)
        Me.pan_CalculOptions.Controls.Add(Me.pan_OptionsFEM)
        Me.pan_CalculOptions.Controls.Add(Me.chk_DalleFEM)
        Me.pan_CalculOptions.Controls.Add(Me.chk_ArmaComp)
        Me.pan_CalculOptions.Controls.Add(Me.chk_ReductionConcreteStrenght)
        Me.pan_CalculOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_CalculOptions.Location = New System.Drawing.Point(0, 30)
        Me.pan_CalculOptions.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_CalculOptions.Name = "pan_CalculOptions"
        Me.pan_CalculOptions.Size = New System.Drawing.Size(380, 275)
        Me.pan_CalculOptions.TabIndex = 4
        '
        'pan_TempArma
        '
        Me.pan_TempArma.Controls.Add(Me.lbl_TempRebars)
        Me.pan_TempArma.Controls.Add(Me.cmb_TempRebars)
        Me.pan_TempArma.Location = New System.Drawing.Point(7, 237)
        Me.pan_TempArma.Name = "pan_TempArma"
        Me.pan_TempArma.Size = New System.Drawing.Size(350, 32)
        Me.pan_TempArma.TabIndex = 124
        '
        'lbl_TempRebars
        '
        Me.lbl_TempRebars.AutoSize = True
        Me.lbl_TempRebars.Location = New System.Drawing.Point(3, 10)
        Me.lbl_TempRebars.Name = "lbl_TempRebars"
        Me.lbl_TempRebars.Size = New System.Drawing.Size(84, 13)
        Me.lbl_TempRebars.TabIndex = 77
        Me.lbl_TempRebars.Text = "lbl_TempRebars"
        '
        'cmb_TempRebars
        '
        Me.cmb_TempRebars.FormattingEnabled = True
        Me.cmb_TempRebars.Location = New System.Drawing.Point(210, 5)
        Me.cmb_TempRebars.Name = "cmb_TempRebars"
        Me.cmb_TempRebars.Size = New System.Drawing.Size(137, 21)
        Me.cmb_TempRebars.TabIndex = 78
        '
        'chk_CongeEnrobe
        '
        Me.chk_CongeEnrobe.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chk_CongeEnrobe.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chk_CongeEnrobe.Location = New System.Drawing.Point(7, 199)
        Me.chk_CongeEnrobe.Name = "chk_CongeEnrobe"
        Me.chk_CongeEnrobe.Size = New System.Drawing.Size(349, 32)
        Me.chk_CongeEnrobe.TabIndex = 123
        Me.chk_CongeEnrobe.Text = "Root fillet are taken into account in the fire design of encased section accordin" &
    "g to Annex F of EN 1994-1-2"
        Me.chk_CongeEnrobe.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chk_CongeEnrobe.UseVisualStyleBackColor = True
        '
        'pan_OptionsFEM
        '
        Me.pan_OptionsFEM.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_OptionsFEM.Controls.Add(Me.lbl_SizeElt)
        Me.pan_OptionsFEM.Controls.Add(Me.etq_UnitU)
        Me.pan_OptionsFEM.Controls.Add(Me.chk_RhoCconstante)
        Me.pan_OptionsFEM.Controls.Add(Me.txt_U)
        Me.pan_OptionsFEM.Controls.Add(Me.chk_ANFrance)
        Me.pan_OptionsFEM.Controls.Add(Me.img_U)
        Me.pan_OptionsFEM.Controls.Add(Me.lbl_TeneurEau)
        Me.pan_OptionsFEM.Controls.Add(Me.img_tDalleFEMmax)
        Me.pan_OptionsFEM.Controls.Add(Me.txt_tDalleFEMmax)
        Me.pan_OptionsFEM.Controls.Add(Me.lbl_UnitD1)
        Me.pan_OptionsFEM.Location = New System.Drawing.Point(20, 61)
        Me.pan_OptionsFEM.Name = "pan_OptionsFEM"
        Me.pan_OptionsFEM.Size = New System.Drawing.Size(336, 98)
        Me.pan_OptionsFEM.TabIndex = 122
        '
        'lbl_SizeElt
        '
        Me.lbl_SizeElt.AutoSize = True
        Me.lbl_SizeElt.Location = New System.Drawing.Point(22, 8)
        Me.lbl_SizeElt.Name = "lbl_SizeElt"
        Me.lbl_SizeElt.Size = New System.Drawing.Size(55, 13)
        Me.lbl_SizeElt.TabIndex = 123
        Me.lbl_SizeElt.Text = "lbl_SizeElt"
        '
        'etq_UnitU
        '
        Me.etq_UnitU.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitU.AutoSize = True
        Me.etq_UnitU.Location = New System.Drawing.Point(278, 78)
        Me.etq_UnitU.Name = "etq_UnitU"
        Me.etq_UnitU.Size = New System.Drawing.Size(55, 13)
        Me.etq_UnitU.TabIndex = 122
        Me.etq_UnitU.Text = "etq_UnitU"
        '
        'chk_RhoCconstante
        '
        Me.chk_RhoCconstante.AutoSize = True
        Me.chk_RhoCconstante.Location = New System.Drawing.Point(3, 31)
        Me.chk_RhoCconstante.Name = "chk_RhoCconstante"
        Me.chk_RhoCconstante.Size = New System.Drawing.Size(124, 17)
        Me.chk_RhoCconstante.TabIndex = 116
        Me.chk_RhoCconstante.Text = "chk_RhoCconstante"
        Me.chk_RhoCconstante.UseVisualStyleBackColor = True
        '
        'txt_U
        '
        Me.txt_U.Location = New System.Drawing.Point(218, 75)
        Me.txt_U.Name = "txt_U"
        Me.txt_U.Size = New System.Drawing.Size(58, 20)
        Me.txt_U.TabIndex = 119
        '
        'chk_ANFrance
        '
        Me.chk_ANFrance.AutoSize = True
        Me.chk_ANFrance.Location = New System.Drawing.Point(3, 54)
        Me.chk_ANFrance.Name = "chk_ANFrance"
        Me.chk_ANFrance.Size = New System.Drawing.Size(98, 17)
        Me.chk_ANFrance.TabIndex = 117
        Me.chk_ANFrance.Text = "chk_ANFrance"
        Me.chk_ANFrance.UseVisualStyleBackColor = True
        '
        'img_U
        '
        Me.img_U.Location = New System.Drawing.Point(173, 75)
        Me.img_U.Name = "img_U"
        Me.img_U.Size = New System.Drawing.Size(46, 20)
        Me.img_U.TabIndex = 120
        Me.img_U.TabStop = False
        '
        'lbl_TeneurEau
        '
        Me.lbl_TeneurEau.AutoSize = True
        Me.lbl_TeneurEau.Location = New System.Drawing.Point(22, 78)
        Me.lbl_TeneurEau.Name = "lbl_TeneurEau"
        Me.lbl_TeneurEau.Size = New System.Drawing.Size(76, 13)
        Me.lbl_TeneurEau.TabIndex = 118
        Me.lbl_TeneurEau.Text = "lbl_TeneurEau"
        '
        'img_tDalleFEMmax
        '
        Me.img_tDalleFEMmax.Location = New System.Drawing.Point(171, 8)
        Me.img_tDalleFEMmax.Name = "img_tDalleFEMmax"
        Me.img_tDalleFEMmax.Size = New System.Drawing.Size(46, 20)
        Me.img_tDalleFEMmax.TabIndex = 76
        Me.img_tDalleFEMmax.TabStop = False
        '
        'txt_tDalleFEMmax
        '
        Me.txt_tDalleFEMmax.Location = New System.Drawing.Point(218, 8)
        Me.txt_tDalleFEMmax.Name = "txt_tDalleFEMmax"
        Me.txt_tDalleFEMmax.Size = New System.Drawing.Size(58, 20)
        Me.txt_tDalleFEMmax.TabIndex = 23
        '
        'lbl_UnitD1
        '
        Me.lbl_UnitD1.AutoSize = True
        Me.lbl_UnitD1.Location = New System.Drawing.Point(278, 11)
        Me.lbl_UnitD1.Name = "lbl_UnitD1"
        Me.lbl_UnitD1.Size = New System.Drawing.Size(56, 13)
        Me.lbl_UnitD1.TabIndex = 24
        Me.lbl_UnitD1.Text = "lbl_UnitD1"
        '
        'chk_DalleFEM
        '
        Me.chk_DalleFEM.AutoSize = True
        Me.chk_DalleFEM.Location = New System.Drawing.Point(7, 38)
        Me.chk_DalleFEM.Name = "chk_DalleFEM"
        Me.chk_DalleFEM.Size = New System.Drawing.Size(96, 17)
        Me.chk_DalleFEM.TabIndex = 21
        Me.chk_DalleFEM.Text = "chk_DalleFEM"
        Me.chk_DalleFEM.UseVisualStyleBackColor = True
        '
        'chk_ArmaComp
        '
        Me.chk_ArmaComp.AutoSize = True
        Me.chk_ArmaComp.Location = New System.Drawing.Point(7, 13)
        Me.chk_ArmaComp.Name = "chk_ArmaComp"
        Me.chk_ArmaComp.Size = New System.Drawing.Size(101, 17)
        Me.chk_ArmaComp.TabIndex = 20
        Me.chk_ArmaComp.Text = "chk_ArmaComp"
        Me.chk_ArmaComp.UseVisualStyleBackColor = True
        '
        'chk_ReductionConcreteStrenght
        '
        Me.chk_ReductionConcreteStrenght.AutoSize = True
        Me.chk_ReductionConcreteStrenght.Location = New System.Drawing.Point(7, 176)
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
        Me.lbl_CalculOptions.Location = New System.Drawing.Point(0, 0)
        Me.lbl_CalculOptions.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CalculOptions.Name = "lbl_CalculOptions"
        Me.lbl_CalculOptions.Size = New System.Drawing.Size(380, 30)
        Me.lbl_CalculOptions.TabIndex = 3
        Me.lbl_CalculOptions.Text = "lbl_CalculOptions"
        Me.lbl_CalculOptions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ErrorProvider_FeuCal
        '
        Me.ErrorProvider_FeuCal.ContainerControl = Me
        '
        'Frm_OptionsFeuN_Calcul
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(873, 554)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsFeuN_Calcul"
        Me.Text = "Frm_OptionsFeuN_Calcul"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_General.ResumeLayout(False)
        Me.TLpan_General.PerformLayout()
        Me.pan_CalculOptions.ResumeLayout(False)
        Me.pan_CalculOptions.PerformLayout()
        Me.pan_TempArma.ResumeLayout(False)
        Me.pan_TempArma.PerformLayout()
        Me.pan_OptionsFEM.ResumeLayout(False)
        Me.pan_OptionsFEM.PerformLayout()
        CType(Me.img_U, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_tDalleFEMmax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider_FeuCal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_General As TableLayoutPanel
    Friend WithEvents lbl_CalculOptions As Label
    Friend WithEvents pan_CalculOptions As Panel
    Friend WithEvents cmb_TempRebars As ComboBox
    Friend WithEvents lbl_TempRebars As Label
    Friend WithEvents chk_DalleFEM As CheckBox
    Friend WithEvents chk_ArmaComp As CheckBox
    Friend WithEvents img_tDalleFEMmax As PictureBox
    Friend WithEvents lbl_UnitD1 As Label
    Friend WithEvents txt_tDalleFEMmax As TextBox
    Friend WithEvents chk_ReductionConcreteStrenght As CheckBox
    Friend WithEvents pan_OptionsFEM As Panel
    Friend WithEvents etq_UnitU As Label
    Friend WithEvents chk_RhoCconstante As CheckBox
    Friend WithEvents txt_U As TextBox
    Friend WithEvents chk_ANFrance As CheckBox
    Friend WithEvents img_U As PictureBox
    Friend WithEvents lbl_TeneurEau As Label
    Friend WithEvents ErrorProvider_FeuCal As ErrorProvider
    Friend WithEvents chk_CongeEnrobe As CheckBox
    Friend WithEvents lbl_SizeElt As Label
    Friend WithEvents pan_TempArma As Panel
End Class
