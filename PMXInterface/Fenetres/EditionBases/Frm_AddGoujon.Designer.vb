<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AddGoujon
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
        Me.ErrDiametreTige = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.etq_UnitFU = New System.Windows.Forms.Label()
        Me.txt_FU = New System.Windows.Forms.TextBox()
        Me.etq_UnitFY = New System.Windows.Forms.Label()
        Me.txt_FY = New System.Windows.Forms.TextBox()
        Me.etq_FY = New System.Windows.Forms.Label()
        Me.ErrHauteurTete = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.etq_FU = New System.Windows.Forms.Label()
        Me.etq_Unit_DiametreTete = New System.Windows.Forms.Label()
        Me.txt_DiametreTete = New System.Windows.Forms.TextBox()
        Me.etq_HauteurTete = New System.Windows.Forms.Label()
        Me.ErrDiametreTete = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.txt_Label = New System.Windows.Forms.TextBox()
        Me.etq_Label = New System.Windows.Forms.Label()
        Me.etq_Hauteur = New System.Windows.Forms.Label()
        Me.txt_Hauteur = New System.Windows.Forms.TextBox()
        Me.etq_UnitHauteur = New System.Windows.Forms.Label()
        Me.etq_DiametreTige = New System.Windows.Forms.Label()
        Me.txt_DiametreTige = New System.Windows.Forms.TextBox()
        Me.etq_Unit_DiametreTige = New System.Windows.Forms.Label()
        Me.ErrFU = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ErrFY = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Grp_Main = New System.Windows.Forms.GroupBox()
        Me.etq_DiametreTete = New System.Windows.Forms.Label()
        Me.txt_HauteurTete = New System.Windows.Forms.TextBox()
        Me.etq_Unit_HauteurTete = New System.Windows.Forms.Label()
        Me.ErrLabel = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.cmd_Valider = New System.Windows.Forms.Button()
        Me.ErrHauteur = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.cmd_Annuler = New System.Windows.Forms.Button()
        CType(Me.ErrDiametreTige, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrHauteurTete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrDiametreTete, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrFU, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrFY, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Grp_Main.SuspendLayout()
        CType(Me.ErrLabel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrHauteur, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ErrDiametreTige
        '
        Me.ErrDiametreTige.ContainerControl = Me
        '
        'etq_UnitFU
        '
        Me.etq_UnitFU.AutoSize = True
        Me.etq_UnitFU.Location = New System.Drawing.Point(228, 178)
        Me.etq_UnitFU.Name = "etq_UnitFU"
        Me.etq_UnitFU.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitFU.TabIndex = 32
        Me.etq_UnitFU.Text = "Label1"
        Me.etq_UnitFU.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_FU
        '
        Me.txt_FU.Location = New System.Drawing.Point(135, 175)
        Me.txt_FU.MaxLength = 12
        Me.txt_FU.Name = "txt_FU"
        Me.txt_FU.Size = New System.Drawing.Size(87, 20)
        Me.txt_FU.TabIndex = 6
        '
        'etq_UnitFY
        '
        Me.etq_UnitFY.AutoSize = True
        Me.etq_UnitFY.Location = New System.Drawing.Point(228, 152)
        Me.etq_UnitFY.Name = "etq_UnitFY"
        Me.etq_UnitFY.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitFY.TabIndex = 29
        Me.etq_UnitFY.Text = "Label1"
        Me.etq_UnitFY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_FY
        '
        Me.txt_FY.Location = New System.Drawing.Point(135, 149)
        Me.txt_FY.MaxLength = 12
        Me.txt_FY.Name = "txt_FY"
        Me.txt_FY.Size = New System.Drawing.Size(87, 20)
        Me.txt_FY.TabIndex = 5
        '
        'etq_FY
        '
        Me.etq_FY.Location = New System.Drawing.Point(17, 149)
        Me.etq_FY.Name = "etq_FY"
        Me.etq_FY.Size = New System.Drawing.Size(113, 18)
        Me.etq_FY.TabIndex = 27
        Me.etq_FY.Text = "etq_FY"
        Me.etq_FY.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ErrHauteurTete
        '
        Me.ErrHauteurTete.ContainerControl = Me
        '
        'etq_FU
        '
        Me.etq_FU.Location = New System.Drawing.Point(17, 175)
        Me.etq_FU.Name = "etq_FU"
        Me.etq_FU.Size = New System.Drawing.Size(113, 18)
        Me.etq_FU.TabIndex = 30
        Me.etq_FU.Text = "etq_FU"
        Me.etq_FU.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_Unit_DiametreTete
        '
        Me.etq_Unit_DiametreTete.AutoSize = True
        Me.etq_Unit_DiametreTete.Location = New System.Drawing.Point(228, 126)
        Me.etq_Unit_DiametreTete.Name = "etq_Unit_DiametreTete"
        Me.etq_Unit_DiametreTete.Size = New System.Drawing.Size(39, 13)
        Me.etq_Unit_DiametreTete.TabIndex = 26
        Me.etq_Unit_DiametreTete.Text = "Label1"
        Me.etq_Unit_DiametreTete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_DiametreTete
        '
        Me.txt_DiametreTete.Location = New System.Drawing.Point(135, 123)
        Me.txt_DiametreTete.MaxLength = 12
        Me.txt_DiametreTete.Name = "txt_DiametreTete"
        Me.txt_DiametreTete.Size = New System.Drawing.Size(87, 20)
        Me.txt_DiametreTete.TabIndex = 4
        '
        'etq_HauteurTete
        '
        Me.etq_HauteurTete.Location = New System.Drawing.Point(17, 97)
        Me.etq_HauteurTete.Name = "etq_HauteurTete"
        Me.etq_HauteurTete.Size = New System.Drawing.Size(113, 18)
        Me.etq_HauteurTete.TabIndex = 21
        Me.etq_HauteurTete.Text = "etq_HauteurTete"
        Me.etq_HauteurTete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ErrDiametreTete
        '
        Me.ErrDiametreTete.ContainerControl = Me
        '
        'txt_Label
        '
        Me.txt_Label.Location = New System.Drawing.Point(135, 19)
        Me.txt_Label.MaxLength = 20
        Me.txt_Label.Name = "txt_Label"
        Me.txt_Label.Size = New System.Drawing.Size(145, 20)
        Me.txt_Label.TabIndex = 0
        '
        'etq_Label
        '
        Me.etq_Label.Location = New System.Drawing.Point(17, 19)
        Me.etq_Label.Name = "etq_Label"
        Me.etq_Label.Size = New System.Drawing.Size(113, 18)
        Me.etq_Label.TabIndex = 13
        Me.etq_Label.Text = "etq_Label"
        Me.etq_Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_Hauteur
        '
        Me.etq_Hauteur.Location = New System.Drawing.Point(17, 45)
        Me.etq_Hauteur.Name = "etq_Hauteur"
        Me.etq_Hauteur.Size = New System.Drawing.Size(113, 18)
        Me.etq_Hauteur.TabIndex = 15
        Me.etq_Hauteur.Text = "etq_Hauteur"
        Me.etq_Hauteur.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Hauteur
        '
        Me.txt_Hauteur.Location = New System.Drawing.Point(135, 45)
        Me.txt_Hauteur.MaxLength = 12
        Me.txt_Hauteur.Name = "txt_Hauteur"
        Me.txt_Hauteur.Size = New System.Drawing.Size(87, 20)
        Me.txt_Hauteur.TabIndex = 1
        '
        'etq_UnitHauteur
        '
        Me.etq_UnitHauteur.AutoSize = True
        Me.etq_UnitHauteur.Location = New System.Drawing.Point(228, 48)
        Me.etq_UnitHauteur.Name = "etq_UnitHauteur"
        Me.etq_UnitHauteur.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitHauteur.TabIndex = 17
        Me.etq_UnitHauteur.Text = "Label1"
        Me.etq_UnitHauteur.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_DiametreTige
        '
        Me.etq_DiametreTige.Location = New System.Drawing.Point(17, 71)
        Me.etq_DiametreTige.Name = "etq_DiametreTige"
        Me.etq_DiametreTige.Size = New System.Drawing.Size(113, 18)
        Me.etq_DiametreTige.TabIndex = 18
        Me.etq_DiametreTige.Text = "etq_DiametreTige"
        Me.etq_DiametreTige.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_DiametreTige
        '
        Me.txt_DiametreTige.Location = New System.Drawing.Point(135, 71)
        Me.txt_DiametreTige.MaxLength = 12
        Me.txt_DiametreTige.Name = "txt_DiametreTige"
        Me.txt_DiametreTige.Size = New System.Drawing.Size(87, 20)
        Me.txt_DiametreTige.TabIndex = 2
        '
        'etq_Unit_DiametreTige
        '
        Me.etq_Unit_DiametreTige.AutoSize = True
        Me.etq_Unit_DiametreTige.Location = New System.Drawing.Point(228, 74)
        Me.etq_Unit_DiametreTige.Name = "etq_Unit_DiametreTige"
        Me.etq_Unit_DiametreTige.Size = New System.Drawing.Size(39, 13)
        Me.etq_Unit_DiametreTige.TabIndex = 20
        Me.etq_Unit_DiametreTige.Text = "Label1"
        Me.etq_Unit_DiametreTige.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ErrFU
        '
        Me.ErrFU.ContainerControl = Me
        '
        'ErrFY
        '
        Me.ErrFY.ContainerControl = Me
        '
        'Grp_Main
        '
        Me.Grp_Main.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grp_Main.Controls.Add(Me.txt_Label)
        Me.Grp_Main.Controls.Add(Me.etq_UnitFU)
        Me.Grp_Main.Controls.Add(Me.etq_Label)
        Me.Grp_Main.Controls.Add(Me.txt_FU)
        Me.Grp_Main.Controls.Add(Me.etq_Hauteur)
        Me.Grp_Main.Controls.Add(Me.etq_FU)
        Me.Grp_Main.Controls.Add(Me.txt_Hauteur)
        Me.Grp_Main.Controls.Add(Me.etq_UnitFY)
        Me.Grp_Main.Controls.Add(Me.etq_UnitHauteur)
        Me.Grp_Main.Controls.Add(Me.txt_FY)
        Me.Grp_Main.Controls.Add(Me.etq_DiametreTige)
        Me.Grp_Main.Controls.Add(Me.etq_FY)
        Me.Grp_Main.Controls.Add(Me.txt_DiametreTige)
        Me.Grp_Main.Controls.Add(Me.etq_Unit_DiametreTete)
        Me.Grp_Main.Controls.Add(Me.etq_Unit_DiametreTige)
        Me.Grp_Main.Controls.Add(Me.txt_DiametreTete)
        Me.Grp_Main.Controls.Add(Me.etq_HauteurTete)
        Me.Grp_Main.Controls.Add(Me.etq_DiametreTete)
        Me.Grp_Main.Controls.Add(Me.txt_HauteurTete)
        Me.Grp_Main.Controls.Add(Me.etq_Unit_HauteurTete)
        Me.Grp_Main.Location = New System.Drawing.Point(12, 12)
        Me.Grp_Main.Name = "Grp_Main"
        Me.Grp_Main.Size = New System.Drawing.Size(313, 212)
        Me.Grp_Main.TabIndex = 5
        Me.Grp_Main.TabStop = False
        '
        'etq_DiametreTete
        '
        Me.etq_DiametreTete.Location = New System.Drawing.Point(17, 123)
        Me.etq_DiametreTete.Name = "etq_DiametreTete"
        Me.etq_DiametreTete.Size = New System.Drawing.Size(113, 18)
        Me.etq_DiametreTete.TabIndex = 24
        Me.etq_DiametreTete.Text = "etq_DiametreTete"
        Me.etq_DiametreTete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_HauteurTete
        '
        Me.txt_HauteurTete.Location = New System.Drawing.Point(135, 97)
        Me.txt_HauteurTete.MaxLength = 12
        Me.txt_HauteurTete.Name = "txt_HauteurTete"
        Me.txt_HauteurTete.Size = New System.Drawing.Size(87, 20)
        Me.txt_HauteurTete.TabIndex = 3
        '
        'etq_Unit_HauteurTete
        '
        Me.etq_Unit_HauteurTete.AutoSize = True
        Me.etq_Unit_HauteurTete.Location = New System.Drawing.Point(228, 100)
        Me.etq_Unit_HauteurTete.Name = "etq_Unit_HauteurTete"
        Me.etq_Unit_HauteurTete.Size = New System.Drawing.Size(39, 13)
        Me.etq_Unit_HauteurTete.TabIndex = 23
        Me.etq_Unit_HauteurTete.Text = "Label1"
        Me.etq_Unit_HauteurTete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ErrLabel
        '
        Me.ErrLabel.ContainerControl = Me
        '
        'cmd_Valider
        '
        Me.cmd_Valider.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_Valider.Location = New System.Drawing.Point(155, 230)
        Me.cmd_Valider.Name = "cmd_Valider"
        Me.cmd_Valider.Size = New System.Drawing.Size(82, 24)
        Me.cmd_Valider.TabIndex = 3
        Me.cmd_Valider.Text = "cmd_Valider"
        Me.cmd_Valider.UseVisualStyleBackColor = True
        '
        'ErrHauteur
        '
        Me.ErrHauteur.ContainerControl = Me
        '
        'cmd_Annuler
        '
        Me.cmd_Annuler.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_Annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmd_Annuler.Location = New System.Drawing.Point(243, 230)
        Me.cmd_Annuler.Name = "cmd_Annuler"
        Me.cmd_Annuler.Size = New System.Drawing.Size(82, 24)
        Me.cmd_Annuler.TabIndex = 4
        Me.cmd_Annuler.Text = "cmd_Annuler"
        Me.cmd_Annuler.UseVisualStyleBackColor = True
        '
        'Frm_AddGoujon
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmd_Annuler
        Me.ClientSize = New System.Drawing.Size(337, 266)
        Me.Controls.Add(Me.Grp_Main)
        Me.Controls.Add(Me.cmd_Valider)
        Me.Controls.Add(Me.cmd_Annuler)
        Me.MinimizeBox = False
        Me.Name = "Frm_AddGoujon"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_AddGoujon"
        CType(Me.ErrDiametreTige, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrHauteurTete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrDiametreTete, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrFU, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrFY, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Grp_Main.ResumeLayout(False)
        Me.Grp_Main.PerformLayout()
        CType(Me.ErrLabel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrHauteur, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ErrDiametreTige As ErrorProvider
    Friend WithEvents cmd_Annuler As Button
    Friend WithEvents Grp_Main As GroupBox
    Friend WithEvents txt_Label As TextBox
    Friend WithEvents etq_UnitFU As Label
    Friend WithEvents etq_Label As Label
    Friend WithEvents txt_FU As TextBox
    Friend WithEvents etq_Hauteur As Label
    Friend WithEvents etq_FU As Label
    Friend WithEvents txt_Hauteur As TextBox
    Friend WithEvents etq_UnitFY As Label
    Friend WithEvents etq_UnitHauteur As Label
    Friend WithEvents txt_FY As TextBox
    Friend WithEvents etq_DiametreTige As Label
    Friend WithEvents etq_FY As Label
    Friend WithEvents txt_DiametreTige As TextBox
    Friend WithEvents etq_Unit_DiametreTete As Label
    Friend WithEvents etq_Unit_DiametreTige As Label
    Friend WithEvents txt_DiametreTete As TextBox
    Friend WithEvents etq_HauteurTete As Label
    Friend WithEvents etq_DiametreTete As Label
    Friend WithEvents txt_HauteurTete As TextBox
    Friend WithEvents etq_Unit_HauteurTete As Label
    Friend WithEvents cmd_Valider As Button
    Friend WithEvents ErrHauteurTete As ErrorProvider
    Friend WithEvents ErrDiametreTete As ErrorProvider
    Friend WithEvents ErrFU As ErrorProvider
    Friend WithEvents ErrFY As ErrorProvider
    Friend WithEvents ErrLabel As ErrorProvider
    Friend WithEvents HelpProvider1 As HelpProvider
    Friend WithEvents ErrHauteur As ErrorProvider
End Class
