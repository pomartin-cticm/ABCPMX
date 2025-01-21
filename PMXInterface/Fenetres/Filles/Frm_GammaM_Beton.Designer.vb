<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_GammaM_Beton
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
        Me.chk_GammaV_Unique = New System.Windows.Forms.CheckBox()
        Me.txt_GammaVc = New System.Windows.Forms.TextBox()
        Me.txt_GammaP = New System.Windows.Forms.TextBox()
        Me.txt_GammaS = New System.Windows.Forms.TextBox()
        Me.txt_GammaVs = New System.Windows.Forms.TextBox()
        Me.txt_GammaC = New System.Windows.Forms.TextBox()
        Me.img_GammaVc = New System.Windows.Forms.PictureBox()
        Me.img_GammaP = New System.Windows.Forms.PictureBox()
        Me.img_GammaS = New System.Windows.Forms.PictureBox()
        Me.img_GammaVs = New System.Windows.Forms.PictureBox()
        Me.img_GammaC = New System.Windows.Forms.PictureBox()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_GammaM.SuspendLayout()
        CType(Me.img_GammaVc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaVs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_GammaM
        '
        Me.pan_GammaM.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_GammaM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_GammaM.Controls.Add(Me.chk_GammaV_Unique)
        Me.pan_GammaM.Controls.Add(Me.txt_GammaVc)
        Me.pan_GammaM.Controls.Add(Me.txt_GammaP)
        Me.pan_GammaM.Controls.Add(Me.txt_GammaS)
        Me.pan_GammaM.Controls.Add(Me.txt_GammaVs)
        Me.pan_GammaM.Controls.Add(Me.txt_GammaC)
        Me.pan_GammaM.Controls.Add(Me.img_GammaVc)
        Me.pan_GammaM.Controls.Add(Me.img_GammaP)
        Me.pan_GammaM.Controls.Add(Me.img_GammaS)
        Me.pan_GammaM.Controls.Add(Me.img_GammaVs)
        Me.pan_GammaM.Controls.Add(Me.img_GammaC)
        Me.pan_GammaM.Location = New System.Drawing.Point(302, 139)
        Me.pan_GammaM.Name = "pan_GammaM"
        Me.pan_GammaM.Size = New System.Drawing.Size(197, 173)
        Me.pan_GammaM.TabIndex = 1
        '
        'chk_GammaV_Unique
        '
        Me.chk_GammaV_Unique.AutoSize = True
        Me.chk_GammaV_Unique.Location = New System.Drawing.Point(15, 7)
        Me.chk_GammaV_Unique.Name = "chk_GammaV_Unique"
        Me.chk_GammaV_Unique.Size = New System.Drawing.Size(133, 17)
        Me.chk_GammaV_Unique.TabIndex = 110
        Me.chk_GammaV_Unique.Text = "chk_GammaV_Unique"
        Me.chk_GammaV_Unique.UseVisualStyleBackColor = True
        '
        'txt_GammaVc
        '
        Me.txt_GammaVc.Location = New System.Drawing.Point(79, 148)
        Me.txt_GammaVc.Name = "txt_GammaVc"
        Me.txt_GammaVc.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaVc.TabIndex = 104
        '
        'txt_GammaP
        '
        Me.txt_GammaP.Location = New System.Drawing.Point(79, 109)
        Me.txt_GammaP.Name = "txt_GammaP"
        Me.txt_GammaP.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaP.TabIndex = 109
        '
        'txt_GammaS
        '
        Me.txt_GammaS.Location = New System.Drawing.Point(79, 82)
        Me.txt_GammaS.Name = "txt_GammaS"
        Me.txt_GammaS.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaS.TabIndex = 107
        '
        'txt_GammaVs
        '
        Me.txt_GammaVs.Location = New System.Drawing.Point(79, 56)
        Me.txt_GammaVs.Name = "txt_GammaVs"
        Me.txt_GammaVs.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaVs.TabIndex = 102
        '
        'txt_GammaC
        '
        Me.txt_GammaC.Location = New System.Drawing.Point(79, 30)
        Me.txt_GammaC.Name = "txt_GammaC"
        Me.txt_GammaC.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaC.TabIndex = 100
        '
        'img_GammaVc
        '
        Me.img_GammaVc.Location = New System.Drawing.Point(34, 148)
        Me.img_GammaVc.Name = "img_GammaVc"
        Me.img_GammaVc.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaVc.TabIndex = 108
        Me.img_GammaVc.TabStop = False
        '
        'img_GammaP
        '
        Me.img_GammaP.Location = New System.Drawing.Point(34, 109)
        Me.img_GammaP.Name = "img_GammaP"
        Me.img_GammaP.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaP.TabIndex = 106
        Me.img_GammaP.TabStop = False
        '
        'img_GammaS
        '
        Me.img_GammaS.Location = New System.Drawing.Point(34, 82)
        Me.img_GammaS.Name = "img_GammaS"
        Me.img_GammaS.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaS.TabIndex = 105
        Me.img_GammaS.TabStop = False
        '
        'img_GammaVs
        '
        Me.img_GammaVs.Location = New System.Drawing.Point(34, 56)
        Me.img_GammaVs.Name = "img_GammaVs"
        Me.img_GammaVs.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaVs.TabIndex = 103
        Me.img_GammaVs.TabStop = False
        '
        'img_GammaC
        '
        Me.img_GammaC.Location = New System.Drawing.Point(34, 30)
        Me.img_GammaC.Name = "img_GammaC"
        Me.img_GammaC.Size = New System.Drawing.Size(46, 20)
        Me.img_GammaC.TabIndex = 101
        Me.img_GammaC.TabStop = False
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Frm_GammaM_Beton
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pan_GammaM)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_GammaM_Beton"
        Me.Text = "Frm_GammaMBeton"
        Me.pan_GammaM.ResumeLayout(False)
        Me.pan_GammaM.PerformLayout()
        CType(Me.img_GammaVc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaVs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_GammaM As Panel
    Friend WithEvents chk_GammaV_Unique As CheckBox
    Friend WithEvents txt_GammaVc As TextBox
    Friend WithEvents txt_GammaP As TextBox
    Friend WithEvents txt_GammaS As TextBox
    Friend WithEvents txt_GammaVs As TextBox
    Friend WithEvents txt_GammaC As TextBox
    Friend WithEvents img_GammaVc As PictureBox
    Friend WithEvents img_GammaP As PictureBox
    Friend WithEvents img_GammaS As PictureBox
    Friend WithEvents img_GammaVs As PictureBox
    Friend WithEvents img_GammaC As PictureBox
    Friend WithEvents ErrorProvider As ErrorProvider
End Class
