<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_OptionsFeuN_CalculSlim
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
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_General = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_CalculOptions = New System.Windows.Forms.Panel()
        Me.btn_Maillage = New System.Windows.Forms.Button()
        Me.pan_OptionsFEM = New System.Windows.Forms.Panel()
        Me.lbl_Beff2D = New System.Windows.Forms.Label()
        Me.img_bEff2D = New System.Windows.Forms.PictureBox()
        Me.txt_tbEff2D = New System.Windows.Forms.TextBox()
        Me.lbl_UnitD1 = New System.Windows.Forms.Label()
        Me.lbl_CalculOptions = New System.Windows.Forms.Label()
        Me.pan_General.SuspendLayout()
        Me.TLpan_General.SuspendLayout()
        Me.pan_CalculOptions.SuspendLayout()
        Me.pan_OptionsFEM.SuspendLayout()
        CType(Me.img_bEff2D, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.AutoScroll = True
        Me.pan_General.Controls.Add(Me.TLpan_General)
        Me.pan_General.Location = New System.Drawing.Point(246, 91)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(507, 459)
        Me.pan_General.TabIndex = 5
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
        Me.TLpan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLpan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_General.Size = New System.Drawing.Size(507, 375)
        Me.TLpan_General.TabIndex = 0
        '
        'pan_CalculOptions
        '
        Me.pan_CalculOptions.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_CalculOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_CalculOptions.Controls.Add(Me.btn_Maillage)
        Me.pan_CalculOptions.Controls.Add(Me.pan_OptionsFEM)
        Me.pan_CalculOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_CalculOptions.Location = New System.Drawing.Point(0, 37)
        Me.pan_CalculOptions.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_CalculOptions.Name = "pan_CalculOptions"
        Me.pan_CalculOptions.Size = New System.Drawing.Size(507, 338)
        Me.pan_CalculOptions.TabIndex = 4
        '
        'btn_Maillage
        '
        Me.btn_Maillage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Maillage.Location = New System.Drawing.Point(3, 229)
        Me.btn_Maillage.Name = "btn_Maillage"
        Me.btn_Maillage.Size = New System.Drawing.Size(499, 33)
        Me.btn_Maillage.TabIndex = 125
        Me.btn_Maillage.Text = "Button1"
        Me.btn_Maillage.UseVisualStyleBackColor = True
        '
        'pan_OptionsFEM
        '
        Me.pan_OptionsFEM.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_OptionsFEM.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_OptionsFEM.Controls.Add(Me.lbl_Beff2D)
        Me.pan_OptionsFEM.Controls.Add(Me.img_bEff2D)
        Me.pan_OptionsFEM.Controls.Add(Me.txt_tbEff2D)
        Me.pan_OptionsFEM.Controls.Add(Me.lbl_UnitD1)
        Me.pan_OptionsFEM.Location = New System.Drawing.Point(4, 16)
        Me.pan_OptionsFEM.Margin = New System.Windows.Forms.Padding(4)
        Me.pan_OptionsFEM.Name = "pan_OptionsFEM"
        Me.pan_OptionsFEM.Size = New System.Drawing.Size(497, 121)
        Me.pan_OptionsFEM.TabIndex = 122
        '
        'lbl_Beff2D
        '
        Me.lbl_Beff2D.AutoSize = True
        Me.lbl_Beff2D.Location = New System.Drawing.Point(4, 10)
        Me.lbl_Beff2D.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Beff2D.Name = "lbl_Beff2D"
        Me.lbl_Beff2D.Size = New System.Drawing.Size(68, 16)
        Me.lbl_Beff2D.TabIndex = 123
        Me.lbl_Beff2D.Text = "lbl_Beff2D"
        '
        'img_bEff2D
        '
        Me.img_bEff2D.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_bEff2D.Location = New System.Drawing.Point(252, 10)
        Me.img_bEff2D.Margin = New System.Windows.Forms.Padding(4)
        Me.img_bEff2D.Name = "img_bEff2D"
        Me.img_bEff2D.Size = New System.Drawing.Size(83, 25)
        Me.img_bEff2D.TabIndex = 76
        Me.img_bEff2D.TabStop = False
        '
        'txt_tbEff2D
        '
        Me.txt_tbEff2D.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_tbEff2D.Location = New System.Drawing.Point(337, 10)
        Me.txt_tbEff2D.Margin = New System.Windows.Forms.Padding(4)
        Me.txt_tbEff2D.Name = "txt_tbEff2D"
        Me.txt_tbEff2D.Size = New System.Drawing.Size(76, 22)
        Me.txt_tbEff2D.TabIndex = 23
        '
        'lbl_UnitD1
        '
        Me.lbl_UnitD1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitD1.AutoSize = True
        Me.lbl_UnitD1.Location = New System.Drawing.Point(417, 14)
        Me.lbl_UnitD1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_UnitD1.Name = "lbl_UnitD1"
        Me.lbl_UnitD1.Size = New System.Drawing.Size(68, 16)
        Me.lbl_UnitD1.TabIndex = 24
        Me.lbl_UnitD1.Text = "lbl_UnitD1"
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
        Me.lbl_CalculOptions.Size = New System.Drawing.Size(507, 37)
        Me.lbl_CalculOptions.TabIndex = 3
        Me.lbl_CalculOptions.Text = "lbl_CalculOptions"
        Me.lbl_CalculOptions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_OptionsFeuN_CalculSlim
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1116, 733)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsFeuN_CalculSlim"
        Me.Text = "Frm_OptionsFeuN_CalculSlim"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_General.ResumeLayout(False)
        Me.TLpan_General.PerformLayout()
        Me.pan_CalculOptions.ResumeLayout(False)
        Me.pan_OptionsFEM.ResumeLayout(False)
        Me.pan_OptionsFEM.PerformLayout()
        CType(Me.img_bEff2D, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_General As TableLayoutPanel
    Friend WithEvents pan_CalculOptions As Panel
    Friend WithEvents pan_OptionsFEM As Panel
    Friend WithEvents lbl_Beff2D As Label
    Friend WithEvents img_bEff2D As PictureBox
    Friend WithEvents txt_tbEff2D As TextBox
    Friend WithEvents lbl_UnitD1 As Label
    Friend WithEvents lbl_CalculOptions As Label
    Friend WithEvents btn_Maillage As Button
End Class
