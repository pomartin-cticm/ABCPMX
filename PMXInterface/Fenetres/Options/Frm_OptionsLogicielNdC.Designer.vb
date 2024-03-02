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
        Me.pan_ELU = New System.Windows.Forms.Panel()
        Me.chk_DisplayMelPoutreMixte = New System.Windows.Forms.CheckBox()
        Me.lbl_ELU = New System.Windows.Forms.Label()
        Me.pan_ELS = New System.Windows.Forms.Panel()
        Me.lbl_Hivoss = New System.Windows.Forms.Label()
        Me.chk_ShowHivossDiagram = New System.Windows.Forms.CheckBox()
        Me.lbl_ELS = New System.Windows.Forms.Label()
        Me.pan_Sollicitations = New System.Windows.Forms.Panel()
        Me.chk_SigmaCharges = New System.Windows.Forms.CheckBox()
        Me.chk_Diagrammes = New System.Windows.Forms.CheckBox()
        Me.chk_DisplayFM_ELF = New System.Windows.Forms.CheckBox()
        Me.chk_DisplayFM_ELS = New System.Windows.Forms.CheckBox()
        Me.chk_DisplayFM_ELU = New System.Windows.Forms.CheckBox()
        Me.chk_DisplayLoadCases = New System.Windows.Forms.CheckBox()
        Me.lbl_Sollicitations = New System.Windows.Forms.Label()
        Me.pan_NdC.SuspendLayout()
        Me.pan_ELU.SuspendLayout()
        Me.pan_ELS.SuspendLayout()
        Me.pan_Sollicitations.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_NdC
        '
        Me.pan_NdC.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_NdC.Controls.Add(Me.pan_ELU)
        Me.pan_NdC.Controls.Add(Me.pan_ELS)
        Me.pan_NdC.Controls.Add(Me.pan_Sollicitations)
        Me.pan_NdC.Location = New System.Drawing.Point(31, 44)
        Me.pan_NdC.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_NdC.Name = "pan_NdC"
        Me.pan_NdC.Size = New System.Drawing.Size(739, 472)
        Me.pan_NdC.TabIndex = 2
        '
        'pan_ELU
        '
        Me.pan_ELU.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_ELU.Controls.Add(Me.chk_DisplayMelPoutreMixte)
        Me.pan_ELU.Controls.Add(Me.lbl_ELU)
        Me.pan_ELU.Location = New System.Drawing.Point(3, 188)
        Me.pan_ELU.Name = "pan_ELU"
        Me.pan_ELU.Size = New System.Drawing.Size(733, 69)
        Me.pan_ELU.TabIndex = 102
        '
        'chk_DisplayMelPoutreMixte
        '
        Me.chk_DisplayMelPoutreMixte.AutoSize = True
        Me.chk_DisplayMelPoutreMixte.Location = New System.Drawing.Point(25, 37)
        Me.chk_DisplayMelPoutreMixte.Name = "chk_DisplayMelPoutreMixte"
        Me.chk_DisplayMelPoutreMixte.Size = New System.Drawing.Size(157, 17)
        Me.chk_DisplayMelPoutreMixte.TabIndex = 97
        Me.chk_DisplayMelPoutreMixte.Text = "chk_DisplayMelPoutreMixte"
        Me.chk_DisplayMelPoutreMixte.UseVisualStyleBackColor = True
        '
        'lbl_ELU
        '
        Me.lbl_ELU.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_ELU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ELU.Location = New System.Drawing.Point(0, 0)
        Me.lbl_ELU.Name = "lbl_ELU"
        Me.lbl_ELU.Size = New System.Drawing.Size(733, 23)
        Me.lbl_ELU.TabIndex = 96
        Me.lbl_ELU.Text = "lbl_ELU"
        Me.lbl_ELU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_ELS
        '
        Me.pan_ELS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_ELS.Controls.Add(Me.lbl_Hivoss)
        Me.pan_ELS.Controls.Add(Me.chk_ShowHivossDiagram)
        Me.pan_ELS.Controls.Add(Me.lbl_ELS)
        Me.pan_ELS.Location = New System.Drawing.Point(3, 264)
        Me.pan_ELS.Name = "pan_ELS"
        Me.pan_ELS.Size = New System.Drawing.Size(733, 96)
        Me.pan_ELS.TabIndex = 101
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
        Me.pan_Sollicitations.Controls.Add(Me.chk_SigmaCharges)
        Me.pan_Sollicitations.Controls.Add(Me.chk_Diagrammes)
        Me.pan_Sollicitations.Controls.Add(Me.chk_DisplayFM_ELF)
        Me.pan_Sollicitations.Controls.Add(Me.chk_DisplayFM_ELS)
        Me.pan_Sollicitations.Controls.Add(Me.chk_DisplayFM_ELU)
        Me.pan_Sollicitations.Controls.Add(Me.chk_DisplayLoadCases)
        Me.pan_Sollicitations.Controls.Add(Me.lbl_Sollicitations)
        Me.pan_Sollicitations.Location = New System.Drawing.Point(3, 3)
        Me.pan_Sollicitations.Name = "pan_Sollicitations"
        Me.pan_Sollicitations.Size = New System.Drawing.Size(733, 182)
        Me.pan_Sollicitations.TabIndex = 100
        '
        'chk_SigmaCharges
        '
        Me.chk_SigmaCharges.AutoSize = True
        Me.chk_SigmaCharges.Location = New System.Drawing.Point(25, 150)
        Me.chk_SigmaCharges.Name = "chk_SigmaCharges"
        Me.chk_SigmaCharges.Size = New System.Drawing.Size(118, 17)
        Me.chk_SigmaCharges.TabIndex = 103
        Me.chk_SigmaCharges.Text = "chk_SigmaCharges"
        Me.chk_SigmaCharges.UseVisualStyleBackColor = True
        '
        'chk_Diagrammes
        '
        Me.chk_Diagrammes.AutoSize = True
        Me.chk_Diagrammes.Location = New System.Drawing.Point(25, 58)
        Me.chk_Diagrammes.Name = "chk_Diagrammes"
        Me.chk_Diagrammes.Size = New System.Drawing.Size(108, 17)
        Me.chk_Diagrammes.TabIndex = 102
        Me.chk_Diagrammes.Text = "chk_Diagrammes"
        Me.chk_Diagrammes.UseVisualStyleBackColor = True
        '
        'chk_DisplayFM_ELF
        '
        Me.chk_DisplayFM_ELF.AutoSize = True
        Me.chk_DisplayFM_ELF.Location = New System.Drawing.Point(25, 127)
        Me.chk_DisplayFM_ELF.Name = "chk_DisplayFM_ELF"
        Me.chk_DisplayFM_ELF.Size = New System.Drawing.Size(124, 17)
        Me.chk_DisplayFM_ELF.TabIndex = 101
        Me.chk_DisplayFM_ELF.Text = "chk_DisplayFM_ELF"
        Me.chk_DisplayFM_ELF.UseVisualStyleBackColor = True
        '
        'chk_DisplayFM_ELS
        '
        Me.chk_DisplayFM_ELS.AutoSize = True
        Me.chk_DisplayFM_ELS.Location = New System.Drawing.Point(25, 104)
        Me.chk_DisplayFM_ELS.Name = "chk_DisplayFM_ELS"
        Me.chk_DisplayFM_ELS.Size = New System.Drawing.Size(125, 17)
        Me.chk_DisplayFM_ELS.TabIndex = 100
        Me.chk_DisplayFM_ELS.Text = "chk_DisplayFM_ELS"
        Me.chk_DisplayFM_ELS.UseVisualStyleBackColor = True
        '
        'chk_DisplayFM_ELU
        '
        Me.chk_DisplayFM_ELU.AutoSize = True
        Me.chk_DisplayFM_ELU.Location = New System.Drawing.Point(25, 81)
        Me.chk_DisplayFM_ELU.Name = "chk_DisplayFM_ELU"
        Me.chk_DisplayFM_ELU.Size = New System.Drawing.Size(126, 17)
        Me.chk_DisplayFM_ELU.TabIndex = 99
        Me.chk_DisplayFM_ELU.Text = "chk_DisplayFM_ELU"
        Me.chk_DisplayFM_ELU.UseVisualStyleBackColor = True
        '
        'chk_DisplayLoadCases
        '
        Me.chk_DisplayLoadCases.AutoSize = True
        Me.chk_DisplayLoadCases.Location = New System.Drawing.Point(25, 35)
        Me.chk_DisplayLoadCases.Name = "chk_DisplayLoadCases"
        Me.chk_DisplayLoadCases.Size = New System.Drawing.Size(137, 17)
        Me.chk_DisplayLoadCases.TabIndex = 98
        Me.chk_DisplayLoadCases.Text = "chk_DisplayLoadCases"
        Me.chk_DisplayLoadCases.UseVisualStyleBackColor = True
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
        Me.pan_ELU.ResumeLayout(False)
        Me.pan_ELU.PerformLayout()
        Me.pan_ELS.ResumeLayout(False)
        Me.pan_ELS.PerformLayout()
        Me.pan_Sollicitations.ResumeLayout(False)
        Me.pan_Sollicitations.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_NdC As Panel
    Friend WithEvents pan_ELS As Panel
    Friend WithEvents lbl_ELS As Label
    Friend WithEvents pan_Sollicitations As Panel
    Friend WithEvents lbl_Sollicitations As Label
    Friend WithEvents chk_ShowHivossDiagram As CheckBox
    Friend WithEvents lbl_Hivoss As Label
    Friend WithEvents chk_DisplayFM_ELF As CheckBox
    Friend WithEvents chk_DisplayFM_ELS As CheckBox
    Friend WithEvents chk_DisplayFM_ELU As CheckBox
    Friend WithEvents chk_DisplayLoadCases As CheckBox
    Friend WithEvents chk_Diagrammes As CheckBox
    Friend WithEvents chk_SigmaCharges As CheckBox
    Friend WithEvents pan_ELU As Panel
    Friend WithEvents chk_DisplayMelPoutreMixte As CheckBox
    Friend WithEvents lbl_ELU As Label
End Class
