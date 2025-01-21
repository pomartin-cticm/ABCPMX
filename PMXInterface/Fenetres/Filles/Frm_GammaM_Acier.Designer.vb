<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_GammaM_Acier
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
        Me.pan_GammaM = New System.Windows.Forms.Panel()
        Me.txt_GammaM2 = New System.Windows.Forms.TextBox()
        Me.img_GammaM2 = New System.Windows.Forms.PictureBox()
        Me.txt_GammaM1 = New System.Windows.Forms.TextBox()
        Me.img_GammaM1 = New System.Windows.Forms.PictureBox()
        Me.txt_GammaM0 = New System.Windows.Forms.TextBox()
        Me.img_GammaM0 = New System.Windows.Forms.PictureBox()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_GammaM.SuspendLayout()
        CType(Me.img_GammaM2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaM1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaM0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_GammaM
        '
        Me.pan_GammaM.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_GammaM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_GammaM.Controls.Add(Me.txt_GammaM2)
        Me.pan_GammaM.Controls.Add(Me.img_GammaM2)
        Me.pan_GammaM.Controls.Add(Me.txt_GammaM1)
        Me.pan_GammaM.Controls.Add(Me.img_GammaM1)
        Me.pan_GammaM.Controls.Add(Me.txt_GammaM0)
        Me.pan_GammaM.Controls.Add(Me.img_GammaM0)
        Me.pan_GammaM.Location = New System.Drawing.Point(154, 85)
        Me.pan_GammaM.Name = "pan_GammaM"
        Me.pan_GammaM.Size = New System.Drawing.Size(197, 173)
        Me.pan_GammaM.TabIndex = 0
        '
        'txt_GammaM2
        '
        Me.txt_GammaM2.Location = New System.Drawing.Point(82, 79)
        Me.txt_GammaM2.Name = "txt_GammaM2"
        Me.txt_GammaM2.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaM2.TabIndex = 91
        '
        'img_GammaM2
        '
        Me.img_GammaM2.Location = New System.Drawing.Point(37, 79)
        Me.img_GammaM2.Name = "img_GammaM2"
        Me.img_GammaM2.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaM2.TabIndex = 92
        Me.img_GammaM2.TabStop = False
        '
        'txt_GammaM1
        '
        Me.txt_GammaM1.Location = New System.Drawing.Point(82, 53)
        Me.txt_GammaM1.Name = "txt_GammaM1"
        Me.txt_GammaM1.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaM1.TabIndex = 89
        '
        'img_GammaM1
        '
        Me.img_GammaM1.Location = New System.Drawing.Point(37, 53)
        Me.img_GammaM1.Name = "img_GammaM1"
        Me.img_GammaM1.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaM1.TabIndex = 90
        Me.img_GammaM1.TabStop = False
        '
        'txt_GammaM0
        '
        Me.txt_GammaM0.Location = New System.Drawing.Point(82, 27)
        Me.txt_GammaM0.Name = "txt_GammaM0"
        Me.txt_GammaM0.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaM0.TabIndex = 87
        '
        'img_GammaM0
        '
        Me.img_GammaM0.Location = New System.Drawing.Point(37, 27)
        Me.img_GammaM0.Name = "img_GammaM0"
        Me.img_GammaM0.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaM0.TabIndex = 88
        Me.img_GammaM0.TabStop = False
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Frm_GammaM_Acier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 458)
        Me.Controls.Add(Me.pan_GammaM)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_GammaM_Acier"
        Me.Text = "Frm_GammaM_Acier"
        Me.pan_GammaM.ResumeLayout(False)
        Me.pan_GammaM.PerformLayout()
        CType(Me.img_GammaM2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaM1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaM0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_GammaM As Panel
    Friend WithEvents txt_GammaM2 As TextBox
    Friend WithEvents img_GammaM2 As PictureBox
    Friend WithEvents txt_GammaM1 As TextBox
    Friend WithEvents img_GammaM1 As PictureBox
    Friend WithEvents txt_GammaM0 As TextBox
    Friend WithEvents img_GammaM0 As PictureBox
    Friend WithEvents ErrorProvider As ErrorProvider
End Class
