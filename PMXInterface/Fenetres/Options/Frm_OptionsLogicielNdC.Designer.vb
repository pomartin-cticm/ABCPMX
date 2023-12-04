<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsLogicielNdC
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
        Me.pan_NdC = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_ELS = New System.Windows.Forms.Label()
        Me.pan_Sollicitations = New System.Windows.Forms.Panel()
        Me.lbl_Sollicitations = New System.Windows.Forms.Label()
        Me.chk_ShowHivossDiagram = New System.Windows.Forms.CheckBox()
        Me.lbl_Hivoss = New System.Windows.Forms.Label()
        Me.pan_NdC.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pan_Sollicitations.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_NdC
        '
        Me.pan_NdC.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_NdC.Controls.Add(Me.Panel1)
        Me.pan_NdC.Controls.Add(Me.pan_Sollicitations)
        Me.pan_NdC.Location = New System.Drawing.Point(31, 44)
        Me.pan_NdC.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_NdC.Name = "pan_NdC"
        Me.pan_NdC.Size = New System.Drawing.Size(739, 472)
        Me.pan_NdC.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.lbl_Hivoss)
        Me.Panel1.Controls.Add(Me.chk_ShowHivossDiagram)
        Me.Panel1.Controls.Add(Me.lbl_ELS)
        Me.Panel1.Location = New System.Drawing.Point(3, 146)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(733, 96)
        Me.Panel1.TabIndex = 101
        '
        'lbl_ELS
        '
        Me.lbl_ELS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_ELS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ELS.Location = New System.Drawing.Point(0, 0)
        Me.lbl_ELS.Name = "lbl_ELS"
        Me.lbl_ELS.Size = New System.Drawing.Size(733, 23)
        Me.lbl_ELS.TabIndex = 96
        Me.lbl_ELS.Text = "lbl_ELS"
        Me.lbl_ELS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Sollicitations
        '
        Me.pan_Sollicitations.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Sollicitations.Controls.Add(Me.lbl_Sollicitations)
        Me.pan_Sollicitations.Location = New System.Drawing.Point(3, 3)
        Me.pan_Sollicitations.Name = "pan_Sollicitations"
        Me.pan_Sollicitations.Size = New System.Drawing.Size(733, 137)
        Me.pan_Sollicitations.TabIndex = 100
        '
        'lbl_Sollicitations
        '
        Me.lbl_Sollicitations.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Sollicitations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Sollicitations.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Sollicitations.Name = "lbl_Sollicitations"
        Me.lbl_Sollicitations.Size = New System.Drawing.Size(733, 23)
        Me.lbl_Sollicitations.TabIndex = 85
        Me.lbl_Sollicitations.Text = "lbl_Sollicitations"
        Me.lbl_Sollicitations.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chk_ShowHivossDiagram
        '
        Me.chk_ShowHivossDiagram.AutoSize = True
        Me.chk_ShowHivossDiagram.Location = New System.Drawing.Point(25, 54)
        Me.chk_ShowHivossDiagram.Name = "chk_ShowHivossDiagram"
        Me.chk_ShowHivossDiagram.Size = New System.Drawing.Size(148, 17)
        Me.chk_ShowHivossDiagram.TabIndex = 97
        Me.chk_ShowHivossDiagram.Text = "chk_ShowHivossDiagram"
        Me.chk_ShowHivossDiagram.UseVisualStyleBackColor = True
        '
        'lbl_Hivoss
        '
        Me.lbl_Hivoss.AutoSize = True
        Me.lbl_Hivoss.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Hivoss.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Hivoss.Location = New System.Drawing.Point(3, 31)
        Me.lbl_Hivoss.Name = "lbl_Hivoss"
        Me.lbl_Hivoss.Size = New System.Drawing.Size(55, 13)
        Me.lbl_Hivoss.TabIndex = 100
        Me.lbl_Hivoss.Text = "lbl_Hivoss"
        '
        'Frm_OptionsLogicielNdC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 561)
        Me.Controls.Add(Me.pan_NdC)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsLogicielNdC"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Frm_OptionsLogicielNdC"
        Me.pan_NdC.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pan_Sollicitations.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_NdC As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl_ELS As Label
    Friend WithEvents pan_Sollicitations As Panel
    Friend WithEvents lbl_Sollicitations As Label
    Friend WithEvents chk_ShowHivossDiagram As CheckBox
    Friend WithEvents lbl_Hivoss As Label
End Class
