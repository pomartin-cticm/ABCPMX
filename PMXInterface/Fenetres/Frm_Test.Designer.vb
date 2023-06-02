<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Test
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
        Me.Button_Valider = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button_Valider
        '
        Me.Button_Valider.Location = New System.Drawing.Point(303, 180)
        Me.Button_Valider.Name = "Button_Valider"
        Me.Button_Valider.Size = New System.Drawing.Size(90, 30)
        Me.Button_Valider.TabIndex = 2
        Me.Button_Valider.Text = "Button_Valider"
        Me.Button_Valider.UseVisualStyleBackColor = True
        '
        'Frm_Test
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Button_Valider)
        Me.Name = "Frm_Test"
        Me.Text = "Frm_Test"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button_Valider As Button
End Class
