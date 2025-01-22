<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_GammaM_Feu
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
        Me.txt_GammaV_fi = New System.Windows.Forms.TextBox()
        Me.img_GammaV_fi = New System.Windows.Forms.PictureBox()
        Me.txt_GammaS_fi = New System.Windows.Forms.TextBox()
        Me.img_GammaS_fi = New System.Windows.Forms.PictureBox()
        Me.txt_GammaC_fi = New System.Windows.Forms.TextBox()
        Me.img_GammaC_fi = New System.Windows.Forms.PictureBox()
        Me.txt_GammaM_fi = New System.Windows.Forms.TextBox()
        Me.img_GammaM_fi = New System.Windows.Forms.PictureBox()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_GammaM.SuspendLayout()
        CType(Me.img_GammaV_fi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaS_fi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaC_fi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaM_fi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_GammaM
        '
        Me.pan_GammaM.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_GammaM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_GammaM.Controls.Add(Me.txt_GammaV_fi)
        Me.pan_GammaM.Controls.Add(Me.img_GammaV_fi)
        Me.pan_GammaM.Controls.Add(Me.txt_GammaS_fi)
        Me.pan_GammaM.Controls.Add(Me.img_GammaS_fi)
        Me.pan_GammaM.Controls.Add(Me.txt_GammaC_fi)
        Me.pan_GammaM.Controls.Add(Me.img_GammaC_fi)
        Me.pan_GammaM.Controls.Add(Me.txt_GammaM_fi)
        Me.pan_GammaM.Controls.Add(Me.img_GammaM_fi)
        Me.pan_GammaM.Location = New System.Drawing.Point(302, 139)
        Me.pan_GammaM.Name = "pan_GammaM"
        Me.pan_GammaM.Size = New System.Drawing.Size(197, 173)
        Me.pan_GammaM.TabIndex = 1
        '
        'txt_GammaV_fi
        '
        Me.txt_GammaV_fi.Location = New System.Drawing.Point(88, 105)
        Me.txt_GammaV_fi.Name = "txt_GammaV_fi"
        Me.txt_GammaV_fi.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaV_fi.TabIndex = 99
        '
        'img_GammaV_fi
        '
        Me.img_GammaV_fi.Location = New System.Drawing.Point(37, 105)
        Me.img_GammaV_fi.Name = "img_GammaV_fi"
        Me.img_GammaV_fi.Size = New System.Drawing.Size(52, 20)
        Me.img_GammaV_fi.TabIndex = 100
        Me.img_GammaV_fi.TabStop = False
        '
        'txt_GammaS_fi
        '
        Me.txt_GammaS_fi.Location = New System.Drawing.Point(88, 79)
        Me.txt_GammaS_fi.Name = "txt_GammaS_fi"
        Me.txt_GammaS_fi.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaS_fi.TabIndex = 95
        '
        'img_GammaS_fi
        '
        Me.img_GammaS_fi.Location = New System.Drawing.Point(37, 79)
        Me.img_GammaS_fi.Name = "img_GammaS_fi"
        Me.img_GammaS_fi.Size = New System.Drawing.Size(52, 20)
        Me.img_GammaS_fi.TabIndex = 97
        Me.img_GammaS_fi.TabStop = False
        '
        'txt_GammaC_fi
        '
        Me.txt_GammaC_fi.Location = New System.Drawing.Point(88, 53)
        Me.txt_GammaC_fi.Name = "txt_GammaC_fi"
        Me.txt_GammaC_fi.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaC_fi.TabIndex = 96
        '
        'img_GammaC_fi
        '
        Me.img_GammaC_fi.Location = New System.Drawing.Point(37, 53)
        Me.img_GammaC_fi.Name = "img_GammaC_fi"
        Me.img_GammaC_fi.Size = New System.Drawing.Size(52, 20)
        Me.img_GammaC_fi.TabIndex = 98
        Me.img_GammaC_fi.TabStop = False
        '
        'txt_GammaM_fi
        '
        Me.txt_GammaM_fi.Location = New System.Drawing.Point(88, 27)
        Me.txt_GammaM_fi.Name = "txt_GammaM_fi"
        Me.txt_GammaM_fi.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaM_fi.TabIndex = 93
        '
        'img_GammaM_fi
        '
        Me.img_GammaM_fi.Location = New System.Drawing.Point(37, 27)
        Me.img_GammaM_fi.Name = "img_GammaM_fi"
        Me.img_GammaM_fi.Size = New System.Drawing.Size(52, 20)
        Me.img_GammaM_fi.TabIndex = 94
        Me.img_GammaM_fi.TabStop = False
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Frm_GammaM_Feu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pan_GammaM)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_GammaM_Feu"
        Me.Text = "Frm_GammaM_Feu"
        Me.pan_GammaM.ResumeLayout(False)
        Me.pan_GammaM.PerformLayout()
        CType(Me.img_GammaV_fi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaS_fi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaC_fi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaM_fi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_GammaM As Panel
    Friend WithEvents txt_GammaV_fi As TextBox
    Friend WithEvents img_GammaV_fi As PictureBox
    Friend WithEvents txt_GammaS_fi As TextBox
    Friend WithEvents img_GammaS_fi As PictureBox
    Friend WithEvents txt_GammaC_fi As TextBox
    Friend WithEvents img_GammaC_fi As PictureBox
    Friend WithEvents txt_GammaM_fi As TextBox
    Friend WithEvents img_GammaM_fi As PictureBox
    Friend WithEvents ErrorProvider As ErrorProvider
End Class
