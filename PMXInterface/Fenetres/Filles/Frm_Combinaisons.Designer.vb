<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Combinaisons
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
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLpan_Lignes = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_ChoixEL = New System.Windows.Forms.TableLayoutPanel()
        Me.rdb_Construction = New System.Windows.Forms.RadioButton()
        Me.rdb_ELFire = New System.Windows.Forms.RadioButton()
        Me.rdb_ELS = New System.Windows.Forms.RadioButton()
        Me.rdb_ELU = New System.Windows.Forms.RadioButton()
        Me.pan_Contenu = New System.Windows.Forms.Panel()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Lignes.SuspendLayout()
        Me.TLpan_ChoixEL.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(473, 498)
        Me.pan_General.TabIndex = 3
        '
        'TLpan_Main
        '
        Me.TLpan_Main.ColumnCount = 1
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Controls.Add(Me.TLPan_PartieBasse, 0, 1)
        Me.TLpan_Main.Controls.Add(Me.pan_Main, 0, 0)
        Me.TLpan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 2
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(473, 498)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 5
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 3, 0)
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_Annuler, 1, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 461)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(467, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(246, 3)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(114, 28)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'btn_Annuler
        '
        Me.btn_Annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Annuler.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Annuler.Location = New System.Drawing.Point(106, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLpan_Lignes)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(467, 452)
        Me.pan_Main.TabIndex = 1
        '
        'TLpan_Lignes
        '
        Me.TLpan_Lignes.ColumnCount = 1
        Me.TLpan_Lignes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Lignes.Controls.Add(Me.TLpan_ChoixEL, 0, 0)
        Me.TLpan_Lignes.Controls.Add(Me.pan_Contenu, 0, 1)
        Me.TLpan_Lignes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Lignes.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Lignes.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Lignes.Name = "TLpan_Lignes"
        Me.TLpan_Lignes.RowCount = 2
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Lignes.Size = New System.Drawing.Size(467, 452)
        Me.TLpan_Lignes.TabIndex = 0
        '
        'TLpan_ChoixEL
        '
        Me.TLpan_ChoixEL.ColumnCount = 4
        Me.TLpan_ChoixEL.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TLpan_ChoixEL.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TLpan_ChoixEL.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TLpan_ChoixEL.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TLpan_ChoixEL.Controls.Add(Me.rdb_Construction, 3, 0)
        Me.TLpan_ChoixEL.Controls.Add(Me.rdb_ELFire, 2, 0)
        Me.TLpan_ChoixEL.Controls.Add(Me.rdb_ELS, 1, 0)
        Me.TLpan_ChoixEL.Controls.Add(Me.rdb_ELU, 0, 0)
        Me.TLpan_ChoixEL.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_ChoixEL.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_ChoixEL.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_ChoixEL.Name = "TLpan_ChoixEL"
        Me.TLpan_ChoixEL.RowCount = 1
        Me.TLpan_ChoixEL.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_ChoixEL.Size = New System.Drawing.Size(467, 40)
        Me.TLpan_ChoixEL.TabIndex = 0
        '
        'rdb_Construction
        '
        Me.rdb_Construction.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_Construction.AutoSize = True
        Me.rdb_Construction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_Construction.Location = New System.Drawing.Point(349, 0)
        Me.rdb_Construction.Margin = New System.Windows.Forms.Padding(1, 0, 0, 1)
        Me.rdb_Construction.Name = "rdb_Construction"
        Me.rdb_Construction.Size = New System.Drawing.Size(118, 39)
        Me.rdb_Construction.TabIndex = 3
        Me.rdb_Construction.Text = "rdb_Construction"
        Me.rdb_Construction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_Construction.UseVisualStyleBackColor = True
        '
        'rdb_ELFire
        '
        Me.rdb_ELFire.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_ELFire.AutoSize = True
        Me.rdb_ELFire.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_ELFire.Location = New System.Drawing.Point(232, 0)
        Me.rdb_ELFire.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.rdb_ELFire.Name = "rdb_ELFire"
        Me.rdb_ELFire.Size = New System.Drawing.Size(116, 39)
        Me.rdb_ELFire.TabIndex = 2
        Me.rdb_ELFire.Text = "rdb_ELFire"
        Me.rdb_ELFire.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_ELFire.UseVisualStyleBackColor = True
        '
        'rdb_ELS
        '
        Me.rdb_ELS.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_ELS.AutoSize = True
        Me.rdb_ELS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_ELS.Location = New System.Drawing.Point(116, 0)
        Me.rdb_ELS.Margin = New System.Windows.Forms.Padding(0, 0, 1, 1)
        Me.rdb_ELS.Name = "rdb_ELS"
        Me.rdb_ELS.Size = New System.Drawing.Size(115, 39)
        Me.rdb_ELS.TabIndex = 1
        Me.rdb_ELS.Text = "rdb_ELS"
        Me.rdb_ELS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_ELS.UseVisualStyleBackColor = True
        '
        'rdb_ELU
        '
        Me.rdb_ELU.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_ELU.AutoSize = True
        Me.rdb_ELU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_ELU.Location = New System.Drawing.Point(0, 0)
        Me.rdb_ELU.Margin = New System.Windows.Forms.Padding(0, 0, 1, 1)
        Me.rdb_ELU.Name = "rdb_ELU"
        Me.rdb_ELU.Size = New System.Drawing.Size(115, 39)
        Me.rdb_ELU.TabIndex = 0
        Me.rdb_ELU.Text = "rdb_ELU"
        Me.rdb_ELU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_ELU.UseVisualStyleBackColor = True
        '
        'pan_Contenu
        '
        Me.pan_Contenu.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Contenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Contenu.Location = New System.Drawing.Point(0, 41)
        Me.pan_Contenu.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.pan_Contenu.Name = "pan_Contenu"
        Me.pan_Contenu.Size = New System.Drawing.Size(467, 411)
        Me.pan_Contenu.TabIndex = 5
        '
        'Frm_Combinaisons
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(473, 498)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Frm_Combinaisons"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Combinaisons"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Lignes.ResumeLayout(False)
        Me.TLpan_ChoixEL.ResumeLayout(False)
        Me.TLpan_ChoixEL.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Lignes As TableLayoutPanel
    Friend WithEvents TLpan_ChoixEL As TableLayoutPanel
    Friend WithEvents rdb_ELFire As RadioButton
    Friend WithEvents rdb_ELS As RadioButton
    Friend WithEvents rdb_ELU As RadioButton
    Friend WithEvents rdb_Construction As RadioButton
    Friend WithEvents pan_Contenu As Panel
End Class
