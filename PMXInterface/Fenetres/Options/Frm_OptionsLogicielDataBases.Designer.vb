<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsLogicielDataBases
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
        Me.pan_DataBases = New System.Windows.Forms.Panel()
        Me.lbl_Activation = New System.Windows.Forms.Label()
        Me.lbl_DataBases = New System.Windows.Forms.Label()
        Me.pan_DataBases.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_DataBases
        '
        Me.pan_DataBases.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_DataBases.Controls.Add(Me.lbl_Activation)
        Me.pan_DataBases.Controls.Add(Me.lbl_DataBases)
        Me.pan_DataBases.Location = New System.Drawing.Point(31, 88)
        Me.pan_DataBases.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_DataBases.Name = "pan_DataBases"
        Me.pan_DataBases.Size = New System.Drawing.Size(739, 436)
        Me.pan_DataBases.TabIndex = 2
        '
        'lbl_Activation
        '
        Me.lbl_Activation.AutoSize = True
        Me.lbl_Activation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Activation.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Activation.Location = New System.Drawing.Point(12, 252)
        Me.lbl_Activation.Name = "lbl_Activation"
        Me.lbl_Activation.Size = New System.Drawing.Size(70, 13)
        Me.lbl_Activation.TabIndex = 103
        Me.lbl_Activation.Text = "lbl_Activation"
        Me.lbl_Activation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_DataBases
        '
        Me.lbl_DataBases.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_DataBases.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_DataBases.Location = New System.Drawing.Point(3, 2)
        Me.lbl_DataBases.Name = "lbl_DataBases"
        Me.lbl_DataBases.Size = New System.Drawing.Size(733, 23)
        Me.lbl_DataBases.TabIndex = 98
        Me.lbl_DataBases.Text = "lbl_DataBases"
        Me.lbl_DataBases.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_OptionsLogicielDataBases
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 613)
        Me.Controls.Add(Me.pan_DataBases)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsLogicielDataBases"
        Me.Text = "Frm_OptionsLogicielDataBases"
        Me.pan_DataBases.ResumeLayout(False)
        Me.pan_DataBases.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_DataBases As Panel
    Friend WithEvents lbl_Activation As Label
    Friend WithEvents lbl_DataBases As Label
End Class
