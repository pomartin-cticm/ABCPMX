<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsCalculsGamma
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
        Me.pan_Gamma = New System.Windows.Forms.Panel()
        Me.txt_GammaQ = New System.Windows.Forms.TextBox()
        Me.txt_GammaGsup = New System.Windows.Forms.TextBox()
        Me.img_GammaQ = New System.Windows.Forms.PictureBox()
        Me.img_GammaGsup = New System.Windows.Forms.PictureBox()
        Me.txt_GammaGinf = New System.Windows.Forms.TextBox()
        Me.img_GammaGinf = New System.Windows.Forms.PictureBox()
        Me.lbl_Loads = New System.Windows.Forms.Label()
        Me.txt_Psi2 = New System.Windows.Forms.TextBox()
        Me.img_Psi2 = New System.Windows.Forms.PictureBox()
        Me.txt_Psi1 = New System.Windows.Forms.TextBox()
        Me.txt_Psi0 = New System.Windows.Forms.TextBox()
        Me.img_Psi1 = New System.Windows.Forms.PictureBox()
        Me.img_Psi0 = New System.Windows.Forms.PictureBox()
        Me.lbl_Combination = New System.Windows.Forms.Label()
        Me.lbl_Materials = New System.Windows.Forms.Label()
        Me.pan_Gamma.SuspendLayout()
        CType(Me.img_GammaQ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaGsup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaGinf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Psi2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Psi1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Psi0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Gamma
        '
        Me.pan_Gamma.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Gamma.Controls.Add(Me.lbl_Materials)
        Me.pan_Gamma.Controls.Add(Me.lbl_Combination)
        Me.pan_Gamma.Controls.Add(Me.txt_Psi2)
        Me.pan_Gamma.Controls.Add(Me.img_Psi2)
        Me.pan_Gamma.Controls.Add(Me.txt_Psi1)
        Me.pan_Gamma.Controls.Add(Me.txt_Psi0)
        Me.pan_Gamma.Controls.Add(Me.img_Psi1)
        Me.pan_Gamma.Controls.Add(Me.img_Psi0)
        Me.pan_Gamma.Controls.Add(Me.lbl_Loads)
        Me.pan_Gamma.Controls.Add(Me.txt_GammaQ)
        Me.pan_Gamma.Controls.Add(Me.txt_GammaGsup)
        Me.pan_Gamma.Controls.Add(Me.img_GammaQ)
        Me.pan_Gamma.Controls.Add(Me.img_GammaGsup)
        Me.pan_Gamma.Controls.Add(Me.txt_GammaGinf)
        Me.pan_Gamma.Controls.Add(Me.img_GammaGinf)
        Me.pan_Gamma.Location = New System.Drawing.Point(51, 28)
        Me.pan_Gamma.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gamma.Name = "pan_Gamma"
        Me.pan_Gamma.Size = New System.Drawing.Size(739, 472)
        Me.pan_Gamma.TabIndex = 0
        '
        'txt_GammaQ
        '
        Me.txt_GammaQ.Location = New System.Drawing.Point(74, 93)
        Me.txt_GammaQ.Name = "txt_GammaQ"
        Me.txt_GammaQ.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaQ.TabIndex = 83
        '
        'txt_GammaGsup
        '
        Me.txt_GammaGsup.Location = New System.Drawing.Point(74, 41)
        Me.txt_GammaGsup.Name = "txt_GammaGsup"
        Me.txt_GammaGsup.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaGsup.TabIndex = 79
        '
        'img_GammaQ
        '
        Me.img_GammaQ.Location = New System.Drawing.Point(29, 93)
        Me.img_GammaQ.Name = "img_GammaQ"
        Me.img_GammaQ.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaQ.TabIndex = 84
        Me.img_GammaQ.TabStop = False
        '
        'img_GammaGsup
        '
        Me.img_GammaGsup.Location = New System.Drawing.Point(29, 41)
        Me.img_GammaGsup.Name = "img_GammaGsup"
        Me.img_GammaGsup.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaGsup.TabIndex = 80
        Me.img_GammaGsup.TabStop = False
        '
        'txt_GammaGinf
        '
        Me.txt_GammaGinf.Location = New System.Drawing.Point(74, 67)
        Me.txt_GammaGinf.Name = "txt_GammaGinf"
        Me.txt_GammaGinf.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaGinf.TabIndex = 81
        '
        'img_GammaGinf
        '
        Me.img_GammaGinf.Location = New System.Drawing.Point(29, 67)
        Me.img_GammaGinf.Name = "img_GammaGinf"
        Me.img_GammaGinf.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaGinf.TabIndex = 82
        Me.img_GammaGinf.TabStop = False
        '
        'lbl_Loads
        '
        Me.lbl_Loads.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Loads.Location = New System.Drawing.Point(30, 12)
        Me.lbl_Loads.Name = "lbl_Loads"
        Me.lbl_Loads.Size = New System.Drawing.Size(150, 23)
        Me.lbl_Loads.TabIndex = 85
        Me.lbl_Loads.Text = "lbl_Loads"
        Me.lbl_Loads.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_Psi2
        '
        Me.txt_Psi2.Location = New System.Drawing.Point(288, 93)
        Me.txt_Psi2.Name = "txt_Psi2"
        Me.txt_Psi2.Size = New System.Drawing.Size(58, 20)
        Me.txt_Psi2.TabIndex = 95
        '
        'img_Psi2
        '
        Me.img_Psi2.Location = New System.Drawing.Point(241, 93)
        Me.img_Psi2.Name = "img_Psi2"
        Me.img_Psi2.Size = New System.Drawing.Size(46, 20)
        Me.img_Psi2.TabIndex = 94
        Me.img_Psi2.TabStop = False
        '
        'txt_Psi1
        '
        Me.txt_Psi1.Location = New System.Drawing.Point(288, 67)
        Me.txt_Psi1.Name = "txt_Psi1"
        Me.txt_Psi1.Size = New System.Drawing.Size(58, 20)
        Me.txt_Psi1.TabIndex = 93
        '
        'txt_Psi0
        '
        Me.txt_Psi0.Location = New System.Drawing.Point(288, 41)
        Me.txt_Psi0.Name = "txt_Psi0"
        Me.txt_Psi0.Size = New System.Drawing.Size(58, 20)
        Me.txt_Psi0.TabIndex = 92
        '
        'img_Psi1
        '
        Me.img_Psi1.Location = New System.Drawing.Point(241, 67)
        Me.img_Psi1.Name = "img_Psi1"
        Me.img_Psi1.Size = New System.Drawing.Size(46, 20)
        Me.img_Psi1.TabIndex = 91
        Me.img_Psi1.TabStop = False
        '
        'img_Psi0
        '
        Me.img_Psi0.Location = New System.Drawing.Point(241, 41)
        Me.img_Psi0.Name = "img_Psi0"
        Me.img_Psi0.Size = New System.Drawing.Size(46, 20)
        Me.img_Psi0.TabIndex = 90
        Me.img_Psi0.TabStop = False
        '
        'lbl_Combination
        '
        Me.lbl_Combination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Combination.Location = New System.Drawing.Point(241, 12)
        Me.lbl_Combination.Name = "lbl_Combination"
        Me.lbl_Combination.Size = New System.Drawing.Size(150, 23)
        Me.lbl_Combination.TabIndex = 96
        Me.lbl_Combination.Text = "lbl_Combination"
        Me.lbl_Combination.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Materials
        '
        Me.lbl_Materials.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Materials.Location = New System.Drawing.Point(30, 135)
        Me.lbl_Materials.Name = "lbl_Materials"
        Me.lbl_Materials.Size = New System.Drawing.Size(412, 23)
        Me.lbl_Materials.TabIndex = 97
        Me.lbl_Materials.Text = "lbl_Materials"
        Me.lbl_Materials.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_OptionsCalculsGamma
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1260, 535)
        Me.Controls.Add(Me.pan_Gamma)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsCalculsGamma"
        Me.Text = "Frm_OptionsCalculsGamma"
        Me.pan_Gamma.ResumeLayout(False)
        Me.pan_Gamma.PerformLayout()
        CType(Me.img_GammaQ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaGsup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaGinf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Psi2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Psi1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Psi0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Gamma As Panel
    Friend WithEvents txt_GammaQ As TextBox
    Friend WithEvents txt_GammaGsup As TextBox
    Friend WithEvents img_GammaQ As PictureBox
    Friend WithEvents img_GammaGsup As PictureBox
    Friend WithEvents txt_GammaGinf As TextBox
    Friend WithEvents img_GammaGinf As PictureBox
    Friend WithEvents lbl_Loads As Label
    Friend WithEvents lbl_Combination As Label
    Friend WithEvents txt_Psi2 As TextBox
    Friend WithEvents img_Psi2 As PictureBox
    Friend WithEvents txt_Psi1 As TextBox
    Friend WithEvents txt_Psi0 As TextBox
    Friend WithEvents img_Psi1 As PictureBox
    Friend WithEvents img_Psi0 As PictureBox
    Friend WithEvents lbl_Materials As Label
End Class
