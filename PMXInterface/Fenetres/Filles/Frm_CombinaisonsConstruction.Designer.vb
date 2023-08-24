<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CombinaisonsConstruction
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
        Me.pan_Combinaisons = New System.Windows.Forms.Panel()
        Me.TLpan_Lignes = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_ELS = New System.Windows.Forms.Label()
        Me.lbl_ELU = New System.Windows.Forms.Label()
        Me.pan_Predefinies = New System.Windows.Forms.Panel()
        Me.img_Combinaison02 = New System.Windows.Forms.PictureBox()
        Me.chk_Combinaison02 = New System.Windows.Forms.CheckBox()
        Me.img_Combinaison01 = New System.Windows.Forms.PictureBox()
        Me.chk_Combinaison01 = New System.Windows.Forms.CheckBox()
        Me.pan_Custom = New System.Windows.Forms.Panel()
        Me.pan_CombiCustom02 = New System.Windows.Forms.Panel()
        Me.txt_Custom02_Q1 = New System.Windows.Forms.TextBox()
        Me.etq_Custom02_Q1 = New System.Windows.Forms.Label()
        Me.txt_Custom02_G = New System.Windows.Forms.TextBox()
        Me.etq_Custom02_G = New System.Windows.Forms.Label()
        Me.img_CombiCustom02 = New System.Windows.Forms.PictureBox()
        Me.chk_CombiCustom02 = New System.Windows.Forms.CheckBox()
        Me.chk_CombiCustom01 = New System.Windows.Forms.CheckBox()
        Me.pan_CombiCustom01 = New System.Windows.Forms.Panel()
        Me.txt_Custom01_Q1 = New System.Windows.Forms.TextBox()
        Me.etq_Custom01_Q1 = New System.Windows.Forms.Label()
        Me.txt_Custom01_G = New System.Windows.Forms.TextBox()
        Me.etq_Custom01_G = New System.Windows.Forms.Label()
        Me.img_CombiCustom01 = New System.Windows.Forms.PictureBox()
        Me.pan_Combinaisons.SuspendLayout()
        Me.TLpan_Lignes.SuspendLayout()
        Me.pan_Predefinies.SuspendLayout()
        CType(Me.img_Combinaison02, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Combinaison01, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Custom.SuspendLayout()
        Me.pan_CombiCustom02.SuspendLayout()
        CType(Me.img_CombiCustom02, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_CombiCustom01.SuspendLayout()
        CType(Me.img_CombiCustom01, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Combinaisons
        '
        Me.pan_Combinaisons.Controls.Add(Me.TLpan_Lignes)
        Me.pan_Combinaisons.Location = New System.Drawing.Point(168, 68)
        Me.pan_Combinaisons.Name = "pan_Combinaisons"
        Me.pan_Combinaisons.Size = New System.Drawing.Size(465, 505)
        Me.pan_Combinaisons.TabIndex = 1
        '
        'TLpan_Lignes
        '
        Me.TLpan_Lignes.ColumnCount = 1
        Me.TLpan_Lignes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Lignes.Controls.Add(Me.lbl_ELS, 0, 2)
        Me.TLpan_Lignes.Controls.Add(Me.lbl_ELU, 0, 0)
        Me.TLpan_Lignes.Controls.Add(Me.pan_Predefinies, 0, 1)
        Me.TLpan_Lignes.Controls.Add(Me.pan_Custom, 0, 3)
        Me.TLpan_Lignes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Lignes.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Lignes.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Lignes.Name = "TLpan_Lignes"
        Me.TLpan_Lignes.RowCount = 4
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Lignes.Size = New System.Drawing.Size(465, 505)
        Me.TLpan_Lignes.TabIndex = 1
        '
        'lbl_ELS
        '
        Me.lbl_ELS.AutoSize = True
        Me.lbl_ELS.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_ELS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ELS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ELS.Location = New System.Drawing.Point(0, 180)
        Me.lbl_ELS.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ELS.Name = "lbl_ELS"
        Me.lbl_ELS.Size = New System.Drawing.Size(465, 30)
        Me.lbl_ELS.TabIndex = 4
        Me.lbl_ELS.Text = "lbl_ELS"
        Me.lbl_ELS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_ELU
        '
        Me.lbl_ELU.AutoSize = True
        Me.lbl_ELU.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_ELU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ELU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ELU.Location = New System.Drawing.Point(0, 0)
        Me.lbl_ELU.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ELU.Name = "lbl_ELU"
        Me.lbl_ELU.Size = New System.Drawing.Size(465, 30)
        Me.lbl_ELU.TabIndex = 2
        Me.lbl_ELU.Text = "lbl_ELU"
        Me.lbl_ELU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Predefinies
        '
        Me.pan_Predefinies.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Predefinies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Predefinies.Controls.Add(Me.chk_CombiCustom01)
        Me.pan_Predefinies.Controls.Add(Me.img_Combinaison01)
        Me.pan_Predefinies.Controls.Add(Me.pan_CombiCustom01)
        Me.pan_Predefinies.Controls.Add(Me.chk_Combinaison01)
        Me.pan_Predefinies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Predefinies.Location = New System.Drawing.Point(0, 31)
        Me.pan_Predefinies.Margin = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.pan_Predefinies.Name = "pan_Predefinies"
        Me.pan_Predefinies.Size = New System.Drawing.Size(465, 148)
        Me.pan_Predefinies.TabIndex = 3
        '
        'img_Combinaison02
        '
        Me.img_Combinaison02.Location = New System.Drawing.Point(181, 7)
        Me.img_Combinaison02.Name = "img_Combinaison02"
        Me.img_Combinaison02.Size = New System.Drawing.Size(276, 59)
        Me.img_Combinaison02.TabIndex = 10
        Me.img_Combinaison02.TabStop = False
        '
        'chk_Combinaison02
        '
        Me.chk_Combinaison02.AutoSize = True
        Me.chk_Combinaison02.Location = New System.Drawing.Point(9, 8)
        Me.chk_Combinaison02.Name = "chk_Combinaison02"
        Me.chk_Combinaison02.Size = New System.Drawing.Size(122, 17)
        Me.chk_Combinaison02.TabIndex = 9
        Me.chk_Combinaison02.Text = "chk_Combinaison02"
        Me.chk_Combinaison02.UseVisualStyleBackColor = True
        '
        'img_Combinaison01
        '
        Me.img_Combinaison01.Location = New System.Drawing.Point(181, 4)
        Me.img_Combinaison01.Name = "img_Combinaison01"
        Me.img_Combinaison01.Size = New System.Drawing.Size(276, 59)
        Me.img_Combinaison01.TabIndex = 8
        Me.img_Combinaison01.TabStop = False
        '
        'chk_Combinaison01
        '
        Me.chk_Combinaison01.AutoSize = True
        Me.chk_Combinaison01.Location = New System.Drawing.Point(9, 5)
        Me.chk_Combinaison01.Name = "chk_Combinaison01"
        Me.chk_Combinaison01.Size = New System.Drawing.Size(122, 17)
        Me.chk_Combinaison01.TabIndex = 7
        Me.chk_Combinaison01.Text = "chk_Combinaison01"
        Me.chk_Combinaison01.UseVisualStyleBackColor = True
        '
        'pan_Custom
        '
        Me.pan_Custom.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Custom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Custom.Controls.Add(Me.pan_CombiCustom02)
        Me.pan_Custom.Controls.Add(Me.img_Combinaison02)
        Me.pan_Custom.Controls.Add(Me.chk_Combinaison02)
        Me.pan_Custom.Controls.Add(Me.chk_CombiCustom02)
        Me.pan_Custom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Custom.Location = New System.Drawing.Point(0, 211)
        Me.pan_Custom.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.pan_Custom.Name = "pan_Custom"
        Me.pan_Custom.Size = New System.Drawing.Size(465, 294)
        Me.pan_Custom.TabIndex = 5
        '
        'pan_CombiCustom02
        '
        Me.pan_CombiCustom02.Controls.Add(Me.txt_Custom02_Q1)
        Me.pan_CombiCustom02.Controls.Add(Me.etq_Custom02_Q1)
        Me.pan_CombiCustom02.Controls.Add(Me.txt_Custom02_G)
        Me.pan_CombiCustom02.Controls.Add(Me.etq_Custom02_G)
        Me.pan_CombiCustom02.Controls.Add(Me.img_CombiCustom02)
        Me.pan_CombiCustom02.Location = New System.Drawing.Point(181, 69)
        Me.pan_CombiCustom02.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_CombiCustom02.Name = "pan_CombiCustom02"
        Me.pan_CombiCustom02.Size = New System.Drawing.Size(276, 38)
        Me.pan_CombiCustom02.TabIndex = 16
        '
        'txt_Custom02_Q1
        '
        Me.txt_Custom02_Q1.Location = New System.Drawing.Point(91, 9)
        Me.txt_Custom02_Q1.Name = "txt_Custom02_Q1"
        Me.txt_Custom02_Q1.Size = New System.Drawing.Size(45, 20)
        Me.txt_Custom02_Q1.TabIndex = 21
        Me.txt_Custom02_Q1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'etq_Custom02_Q1
        '
        Me.etq_Custom02_Q1.AutoSize = True
        Me.etq_Custom02_Q1.Location = New System.Drawing.Point(142, 13)
        Me.etq_Custom02_Q1.Name = "etq_Custom02_Q1"
        Me.etq_Custom02_Q1.Size = New System.Drawing.Size(38, 13)
        Me.etq_Custom02_Q1.TabIndex = 20
        Me.etq_Custom02_Q1.Text = "WW +"
        '
        'txt_Custom02_G
        '
        Me.txt_Custom02_G.Location = New System.Drawing.Point(10, 9)
        Me.txt_Custom02_G.Name = "txt_Custom02_G"
        Me.txt_Custom02_G.Size = New System.Drawing.Size(45, 20)
        Me.txt_Custom02_G.TabIndex = 19
        Me.txt_Custom02_G.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'etq_Custom02_G
        '
        Me.etq_Custom02_G.AutoSize = True
        Me.etq_Custom02_G.Location = New System.Drawing.Point(61, 13)
        Me.etq_Custom02_G.Name = "etq_Custom02_G"
        Me.etq_Custom02_G.Size = New System.Drawing.Size(24, 13)
        Me.etq_Custom02_G.TabIndex = 18
        Me.etq_Custom02_G.Text = "G +"
        '
        'img_CombiCustom02
        '
        Me.img_CombiCustom02.Dock = System.Windows.Forms.DockStyle.Fill
        Me.img_CombiCustom02.Location = New System.Drawing.Point(0, 0)
        Me.img_CombiCustom02.Margin = New System.Windows.Forms.Padding(0)
        Me.img_CombiCustom02.Name = "img_CombiCustom02"
        Me.img_CombiCustom02.Size = New System.Drawing.Size(276, 38)
        Me.img_CombiCustom02.TabIndex = 12
        Me.img_CombiCustom02.TabStop = False
        '
        'chk_CombiCustom02
        '
        Me.chk_CombiCustom02.AutoSize = True
        Me.chk_CombiCustom02.Location = New System.Drawing.Point(9, 78)
        Me.chk_CombiCustom02.Name = "chk_CombiCustom02"
        Me.chk_CombiCustom02.Size = New System.Drawing.Size(126, 17)
        Me.chk_CombiCustom02.TabIndex = 15
        Me.chk_CombiCustom02.Text = "chk_CombiCustom02"
        Me.chk_CombiCustom02.UseVisualStyleBackColor = True
        '
        'chk_CombiCustom01
        '
        Me.chk_CombiCustom01.AutoSize = True
        Me.chk_CombiCustom01.Location = New System.Drawing.Point(9, 79)
        Me.chk_CombiCustom01.Name = "chk_CombiCustom01"
        Me.chk_CombiCustom01.Size = New System.Drawing.Size(126, 17)
        Me.chk_CombiCustom01.TabIndex = 14
        Me.chk_CombiCustom01.Text = "chk_CombiCustom01"
        Me.chk_CombiCustom01.UseVisualStyleBackColor = True
        '
        'pan_CombiCustom01
        '
        Me.pan_CombiCustom01.Controls.Add(Me.txt_Custom01_Q1)
        Me.pan_CombiCustom01.Controls.Add(Me.etq_Custom01_Q1)
        Me.pan_CombiCustom01.Controls.Add(Me.txt_Custom01_G)
        Me.pan_CombiCustom01.Controls.Add(Me.etq_Custom01_G)
        Me.pan_CombiCustom01.Controls.Add(Me.img_CombiCustom01)
        Me.pan_CombiCustom01.Location = New System.Drawing.Point(181, 66)
        Me.pan_CombiCustom01.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_CombiCustom01.Name = "pan_CombiCustom01"
        Me.pan_CombiCustom01.Size = New System.Drawing.Size(276, 38)
        Me.pan_CombiCustom01.TabIndex = 13
        '
        'txt_Custom01_Q1
        '
        Me.txt_Custom01_Q1.Location = New System.Drawing.Point(91, 9)
        Me.txt_Custom01_Q1.Name = "txt_Custom01_Q1"
        Me.txt_Custom01_Q1.Size = New System.Drawing.Size(45, 20)
        Me.txt_Custom01_Q1.TabIndex = 21
        Me.txt_Custom01_Q1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'etq_Custom01_Q1
        '
        Me.etq_Custom01_Q1.AutoSize = True
        Me.etq_Custom01_Q1.Location = New System.Drawing.Point(142, 13)
        Me.etq_Custom01_Q1.Name = "etq_Custom01_Q1"
        Me.etq_Custom01_Q1.Size = New System.Drawing.Size(38, 13)
        Me.etq_Custom01_Q1.TabIndex = 20
        Me.etq_Custom01_Q1.Text = "WW +"
        '
        'txt_Custom01_G
        '
        Me.txt_Custom01_G.Location = New System.Drawing.Point(10, 9)
        Me.txt_Custom01_G.Name = "txt_Custom01_G"
        Me.txt_Custom01_G.Size = New System.Drawing.Size(45, 20)
        Me.txt_Custom01_G.TabIndex = 19
        Me.txt_Custom01_G.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'etq_Custom01_G
        '
        Me.etq_Custom01_G.AutoSize = True
        Me.etq_Custom01_G.Location = New System.Drawing.Point(61, 13)
        Me.etq_Custom01_G.Name = "etq_Custom01_G"
        Me.etq_Custom01_G.Size = New System.Drawing.Size(24, 13)
        Me.etq_Custom01_G.TabIndex = 18
        Me.etq_Custom01_G.Text = "G +"
        '
        'img_CombiCustom01
        '
        Me.img_CombiCustom01.Dock = System.Windows.Forms.DockStyle.Fill
        Me.img_CombiCustom01.Location = New System.Drawing.Point(0, 0)
        Me.img_CombiCustom01.Margin = New System.Windows.Forms.Padding(0)
        Me.img_CombiCustom01.Name = "img_CombiCustom01"
        Me.img_CombiCustom01.Size = New System.Drawing.Size(276, 38)
        Me.img_CombiCustom01.TabIndex = 12
        Me.img_CombiCustom01.TabStop = False
        '
        'Frm_CombinaisonsConstruction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 640)
        Me.Controls.Add(Me.pan_Combinaisons)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_CombinaisonsConstruction"
        Me.Text = "Frm_CombinaisonsConstruction"
        Me.pan_Combinaisons.ResumeLayout(False)
        Me.TLpan_Lignes.ResumeLayout(False)
        Me.TLpan_Lignes.PerformLayout()
        Me.pan_Predefinies.ResumeLayout(False)
        Me.pan_Predefinies.PerformLayout()
        CType(Me.img_Combinaison02, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Combinaison01, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Custom.ResumeLayout(False)
        Me.pan_Custom.PerformLayout()
        Me.pan_CombiCustom02.ResumeLayout(False)
        Me.pan_CombiCustom02.PerformLayout()
        CType(Me.img_CombiCustom02, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_CombiCustom01.ResumeLayout(False)
        Me.pan_CombiCustom01.PerformLayout()
        CType(Me.img_CombiCustom01, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Combinaisons As Panel
    Friend WithEvents TLpan_Lignes As TableLayoutPanel
    Friend WithEvents lbl_ELS As Label
    Friend WithEvents lbl_ELU As Label
    Friend WithEvents pan_Predefinies As Panel
    Friend WithEvents pan_CombiCustom02 As Panel
    Friend WithEvents txt_Custom02_Q1 As TextBox
    Friend WithEvents etq_Custom02_Q1 As Label
    Friend WithEvents txt_Custom02_G As TextBox
    Friend WithEvents etq_Custom02_G As Label
    Friend WithEvents img_CombiCustom02 As PictureBox
    Friend WithEvents chk_CombiCustom02 As CheckBox
    Friend WithEvents img_Combinaison01 As PictureBox
    Friend WithEvents chk_Combinaison01 As CheckBox
    Friend WithEvents pan_Custom As Panel
    Friend WithEvents img_Combinaison02 As PictureBox
    Friend WithEvents chk_CombiCustom01 As CheckBox
    Friend WithEvents chk_Combinaison02 As CheckBox
    Friend WithEvents pan_CombiCustom01 As Panel
    Friend WithEvents txt_Custom01_Q1 As TextBox
    Friend WithEvents etq_Custom01_Q1 As Label
    Friend WithEvents txt_Custom01_G As TextBox
    Friend WithEvents etq_Custom01_G As Label
    Friend WithEvents img_CombiCustom01 As PictureBox
End Class
