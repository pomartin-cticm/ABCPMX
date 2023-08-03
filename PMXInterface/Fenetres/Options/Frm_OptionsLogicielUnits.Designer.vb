<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsLogicielUnits
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
        Me.pan_Units = New System.Windows.Forms.Panel()
        Me.lbl_Units = New System.Windows.Forms.Label()
        Me.pan_Loads = New System.Windows.Forms.Panel()
        Me.lbl_Contraintes = New System.Windows.Forms.Label()
        Me.lbl_Moments = New System.Windows.Forms.Label()
        Me.lbl_Forces = New System.Windows.Forms.Label()
        Me.lbl_Inertie = New System.Windows.Forms.Label()
        Me.lbl_ModuleW = New System.Windows.Forms.Label()
        Me.lbl_Longueur = New System.Windows.Forms.Label()
        Me.lbl_Dimensions = New System.Windows.Forms.Label()
        Me.cmb_Dimensions = New System.Windows.Forms.ComboBox()
        Me.cmb_Longueur = New System.Windows.Forms.ComboBox()
        Me.pan_Units.SuspendLayout()
        Me.pan_Loads.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_Units
        '
        Me.pan_Units.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Units.Controls.Add(Me.lbl_Units)
        Me.pan_Units.Controls.Add(Me.pan_Loads)
        Me.pan_Units.Location = New System.Drawing.Point(21, 20)
        Me.pan_Units.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Units.Name = "pan_Units"
        Me.pan_Units.Size = New System.Drawing.Size(739, 401)
        Me.pan_Units.TabIndex = 2
        '
        'lbl_Units
        '
        Me.lbl_Units.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Units.Location = New System.Drawing.Point(11, 7)
        Me.lbl_Units.Name = "lbl_Units"
        Me.lbl_Units.Size = New System.Drawing.Size(295, 23)
        Me.lbl_Units.TabIndex = 85
        Me.lbl_Units.Text = "lbl_Units"
        Me.lbl_Units.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Loads
        '
        Me.pan_Loads.Controls.Add(Me.cmb_Longueur)
        Me.pan_Loads.Controls.Add(Me.cmb_Dimensions)
        Me.pan_Loads.Controls.Add(Me.lbl_Contraintes)
        Me.pan_Loads.Controls.Add(Me.lbl_Moments)
        Me.pan_Loads.Controls.Add(Me.lbl_Forces)
        Me.pan_Loads.Controls.Add(Me.lbl_Inertie)
        Me.pan_Loads.Controls.Add(Me.lbl_ModuleW)
        Me.pan_Loads.Controls.Add(Me.lbl_Longueur)
        Me.pan_Loads.Controls.Add(Me.lbl_Dimensions)
        Me.pan_Loads.Location = New System.Drawing.Point(11, 7)
        Me.pan_Loads.Name = "pan_Loads"
        Me.pan_Loads.Size = New System.Drawing.Size(295, 288)
        Me.pan_Loads.TabIndex = 98
        '
        'lbl_Contraintes
        '
        Me.lbl_Contraintes.AutoSize = True
        Me.lbl_Contraintes.Location = New System.Drawing.Point(12, 180)
        Me.lbl_Contraintes.Name = "lbl_Contraintes"
        Me.lbl_Contraintes.Size = New System.Drawing.Size(76, 13)
        Me.lbl_Contraintes.TabIndex = 10
        Me.lbl_Contraintes.Text = "lbl_Contraintes"
        '
        'lbl_Moments
        '
        Me.lbl_Moments.AutoSize = True
        Me.lbl_Moments.Location = New System.Drawing.Point(12, 156)
        Me.lbl_Moments.Name = "lbl_Moments"
        Me.lbl_Moments.Size = New System.Drawing.Size(66, 13)
        Me.lbl_Moments.TabIndex = 9
        Me.lbl_Moments.Text = "lbl_Moments"
        '
        'lbl_Forces
        '
        Me.lbl_Forces.AutoSize = True
        Me.lbl_Forces.Location = New System.Drawing.Point(12, 133)
        Me.lbl_Forces.Name = "lbl_Forces"
        Me.lbl_Forces.Size = New System.Drawing.Size(55, 13)
        Me.lbl_Forces.TabIndex = 8
        Me.lbl_Forces.Text = "lbl_Forces"
        '
        'lbl_Inertie
        '
        Me.lbl_Inertie.AutoSize = True
        Me.lbl_Inertie.Location = New System.Drawing.Point(12, 109)
        Me.lbl_Inertie.Name = "lbl_Inertie"
        Me.lbl_Inertie.Size = New System.Drawing.Size(52, 13)
        Me.lbl_Inertie.TabIndex = 7
        Me.lbl_Inertie.Text = "lbl_Inertie"
        '
        'lbl_ModuleW
        '
        Me.lbl_ModuleW.AutoSize = True
        Me.lbl_ModuleW.Location = New System.Drawing.Point(12, 85)
        Me.lbl_ModuleW.Name = "lbl_ModuleW"
        Me.lbl_ModuleW.Size = New System.Drawing.Size(69, 13)
        Me.lbl_ModuleW.TabIndex = 6
        Me.lbl_ModuleW.Text = "lbl_ModuleW"
        '
        'lbl_Longueur
        '
        Me.lbl_Longueur.AutoSize = True
        Me.lbl_Longueur.Location = New System.Drawing.Point(12, 60)
        Me.lbl_Longueur.Name = "lbl_Longueur"
        Me.lbl_Longueur.Size = New System.Drawing.Size(68, 13)
        Me.lbl_Longueur.TabIndex = 5
        Me.lbl_Longueur.Text = "lbl_Longueur"
        '
        'lbl_Dimensions
        '
        Me.lbl_Dimensions.AutoSize = True
        Me.lbl_Dimensions.Location = New System.Drawing.Point(12, 36)
        Me.lbl_Dimensions.Name = "lbl_Dimensions"
        Me.lbl_Dimensions.Size = New System.Drawing.Size(77, 13)
        Me.lbl_Dimensions.TabIndex = 4
        Me.lbl_Dimensions.Text = "lbl_Dimensions"
        '
        'cmb_Dimensions
        '
        Me.cmb_Dimensions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Dimensions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Dimensions.FormattingEnabled = True
        Me.cmb_Dimensions.Location = New System.Drawing.Point(134, 33)
        Me.cmb_Dimensions.Name = "cmb_Dimensions"
        Me.cmb_Dimensions.Size = New System.Drawing.Size(98, 21)
        Me.cmb_Dimensions.TabIndex = 58
        '
        'cmb_Longueur
        '
        Me.cmb_Longueur.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Longueur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Longueur.FormattingEnabled = True
        Me.cmb_Longueur.Location = New System.Drawing.Point(134, 57)
        Me.cmb_Longueur.Name = "cmb_Longueur"
        Me.cmb_Longueur.Size = New System.Drawing.Size(98, 21)
        Me.cmb_Longueur.TabIndex = 59
        '
        'Frm_OptionsLogicielUnits
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pan_Units)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsLogicielUnits"
        Me.Text = "Frm_OptionsLogicielUnits"
        Me.pan_Units.ResumeLayout(False)
        Me.pan_Loads.ResumeLayout(False)
        Me.pan_Loads.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Units As Panel
    Friend WithEvents lbl_Units As Label
    Friend WithEvents pan_Loads As Panel
    Friend WithEvents lbl_Contraintes As Label
    Friend WithEvents lbl_Moments As Label
    Friend WithEvents lbl_Forces As Label
    Friend WithEvents lbl_Inertie As Label
    Friend WithEvents lbl_ModuleW As Label
    Friend WithEvents lbl_Longueur As Label
    Friend WithEvents lbl_Dimensions As Label
    Friend WithEvents cmb_Dimensions As ComboBox
    Friend WithEvents cmb_Longueur As ComboBox
End Class
