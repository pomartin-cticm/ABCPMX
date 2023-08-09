<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsLogicielExpert
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
        Me.pan_Expert = New System.Windows.Forms.Panel()
        Me.btn_Desactiver = New System.Windows.Forms.Button()
        Me.btn_Activer = New System.Windows.Forms.Button()
        Me.lbl_Activation = New System.Windows.Forms.Label()
        Me.lbl_Key = New System.Windows.Forms.Label()
        Me.txt_Expert = New System.Windows.Forms.TextBox()
        Me.lbl_ExpertMode = New System.Windows.Forms.Label()
        Me.pan_Expert.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_Expert
        '
        Me.pan_Expert.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Expert.Controls.Add(Me.btn_Desactiver)
        Me.pan_Expert.Controls.Add(Me.btn_Activer)
        Me.pan_Expert.Controls.Add(Me.lbl_Activation)
        Me.pan_Expert.Controls.Add(Me.lbl_Key)
        Me.pan_Expert.Controls.Add(Me.txt_Expert)
        Me.pan_Expert.Controls.Add(Me.lbl_ExpertMode)
        Me.pan_Expert.Location = New System.Drawing.Point(8, 8)
        Me.pan_Expert.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Expert.Name = "pan_Expert"
        Me.pan_Expert.Size = New System.Drawing.Size(739, 436)
        Me.pan_Expert.TabIndex = 1
        '
        'btn_Desactiver
        '
        Me.btn_Desactiver.Location = New System.Drawing.Point(88, 89)
        Me.btn_Desactiver.Name = "btn_Desactiver"
        Me.btn_Desactiver.Size = New System.Drawing.Size(280, 23)
        Me.btn_Desactiver.TabIndex = 105
        Me.btn_Desactiver.Text = "btn_Desactiver"
        Me.btn_Desactiver.UseVisualStyleBackColor = True
        '
        'btn_Activer
        '
        Me.btn_Activer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Activer.Location = New System.Drawing.Point(372, 61)
        Me.btn_Activer.Name = "btn_Activer"
        Me.btn_Activer.Size = New System.Drawing.Size(362, 23)
        Me.btn_Activer.TabIndex = 104
        Me.btn_Activer.Text = "btn_Activate"
        Me.btn_Activer.UseVisualStyleBackColor = True
        '
        'lbl_Activation
        '
        Me.lbl_Activation.AutoSize = True
        Me.lbl_Activation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Activation.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Activation.Location = New System.Drawing.Point(16, 39)
        Me.lbl_Activation.Name = "lbl_Activation"
        Me.lbl_Activation.Size = New System.Drawing.Size(70, 13)
        Me.lbl_Activation.TabIndex = 103
        Me.lbl_Activation.Text = "lbl_Activation"
        Me.lbl_Activation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Key
        '
        Me.lbl_Key.AutoSize = True
        Me.lbl_Key.Location = New System.Drawing.Point(41, 65)
        Me.lbl_Key.Name = "lbl_Key"
        Me.lbl_Key.Size = New System.Drawing.Size(41, 13)
        Me.lbl_Key.TabIndex = 102
        Me.lbl_Key.Text = "lbl_Key"
        '
        'txt_Expert
        '
        Me.txt_Expert.Location = New System.Drawing.Point(88, 63)
        Me.txt_Expert.Name = "txt_Expert"
        Me.txt_Expert.Size = New System.Drawing.Size(280, 20)
        Me.txt_Expert.TabIndex = 99
        '
        'lbl_ExpertMode
        '
        Me.lbl_ExpertMode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_ExpertMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ExpertMode.Location = New System.Drawing.Point(3, 2)
        Me.lbl_ExpertMode.Name = "lbl_ExpertMode"
        Me.lbl_ExpertMode.Size = New System.Drawing.Size(733, 23)
        Me.lbl_ExpertMode.TabIndex = 98
        Me.lbl_ExpertMode.Text = "lbl_ExpertMode"
        Me.lbl_ExpertMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_OptionsLogicielExpert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 545)
        Me.Controls.Add(Me.pan_Expert)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsLogicielExpert"
        Me.Text = "Frm_OptionsLogicielExpert"
        Me.pan_Expert.ResumeLayout(False)
        Me.pan_Expert.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Expert As Panel
    Friend WithEvents lbl_Activation As Label
    Friend WithEvents lbl_Key As Label
    Friend WithEvents txt_Expert As TextBox
    Friend WithEvents lbl_ExpertMode As Label
    Friend WithEvents btn_Desactiver As Button
    Friend WithEvents btn_Activer As Button
End Class
