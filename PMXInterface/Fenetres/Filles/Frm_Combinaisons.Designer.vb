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
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLpan_Lignes = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_ChoixEL = New System.Windows.Forms.TableLayoutPanel()
        Me.rdb_ELU = New System.Windows.Forms.RadioButton()
        Me.rdb_ELS = New System.Windows.Forms.RadioButton()
        Me.rdb_ELFire = New System.Windows.Forms.RadioButton()
        Me.lbl_Predefinies = New System.Windows.Forms.Label()
        Me.pan_Predefinies = New System.Windows.Forms.Panel()
        Me.lbl_Custom = New System.Windows.Forms.Label()
        Me.img_EL_Eq01 = New System.Windows.Forms.PictureBox()
        Me.chk_Combinaison01 = New System.Windows.Forms.CheckBox()
        Me.pan_General.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Lignes.SuspendLayout()
        Me.TLpan_ChoixEL.SuspendLayout()
        Me.pan_Predefinies.SuspendLayout()
        CType(Me.img_EL_Eq01, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TLpan_Lignes.Controls.Add(Me.lbl_Custom, 0, 3)
        Me.TLpan_Lignes.Controls.Add(Me.lbl_Predefinies, 0, 1)
        Me.TLpan_Lignes.Controls.Add(Me.TLpan_ChoixEL, 0, 0)
        Me.TLpan_Lignes.Controls.Add(Me.pan_Predefinies, 0, 2)
        Me.TLpan_Lignes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Lignes.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Lignes.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Lignes.Name = "TLpan_Lignes"
        Me.TLpan_Lignes.RowCount = 5
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Lignes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Lignes.Size = New System.Drawing.Size(467, 452)
        Me.TLpan_Lignes.TabIndex = 0
        '
        'TLpan_ChoixEL
        '
        Me.TLpan_ChoixEL.ColumnCount = 3
        Me.TLpan_ChoixEL.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_ChoixEL.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_ChoixEL.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
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
        'rdb_ELU
        '
        Me.rdb_ELU.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_ELU.AutoSize = True
        Me.rdb_ELU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_ELU.Location = New System.Drawing.Point(1, 1)
        Me.rdb_ELU.Margin = New System.Windows.Forms.Padding(1)
        Me.rdb_ELU.Name = "rdb_ELU"
        Me.rdb_ELU.Size = New System.Drawing.Size(153, 38)
        Me.rdb_ELU.TabIndex = 0
        Me.rdb_ELU.Text = "rdb_ELU"
        Me.rdb_ELU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_ELU.UseVisualStyleBackColor = True
        '
        'rdb_ELS
        '
        Me.rdb_ELS.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_ELS.AutoSize = True
        Me.rdb_ELS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_ELS.Location = New System.Drawing.Point(156, 1)
        Me.rdb_ELS.Margin = New System.Windows.Forms.Padding(1)
        Me.rdb_ELS.Name = "rdb_ELS"
        Me.rdb_ELS.Size = New System.Drawing.Size(153, 38)
        Me.rdb_ELS.TabIndex = 1
        Me.rdb_ELS.Text = "rdb_ELS"
        Me.rdb_ELS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_ELS.UseVisualStyleBackColor = True
        '
        'rdb_ELFire
        '
        Me.rdb_ELFire.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_ELFire.AutoSize = True
        Me.rdb_ELFire.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_ELFire.Location = New System.Drawing.Point(311, 1)
        Me.rdb_ELFire.Margin = New System.Windows.Forms.Padding(1)
        Me.rdb_ELFire.Name = "rdb_ELFire"
        Me.rdb_ELFire.Size = New System.Drawing.Size(155, 38)
        Me.rdb_ELFire.TabIndex = 2
        Me.rdb_ELFire.Text = "rdb_ELFire"
        Me.rdb_ELFire.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_ELFire.UseVisualStyleBackColor = True
        '
        'lbl_Predefinies
        '
        Me.lbl_Predefinies.AutoSize = True
        Me.lbl_Predefinies.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Predefinies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Predefinies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Predefinies.Location = New System.Drawing.Point(0, 40)
        Me.lbl_Predefinies.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Predefinies.Name = "lbl_Predefinies"
        Me.lbl_Predefinies.Size = New System.Drawing.Size(467, 30)
        Me.lbl_Predefinies.TabIndex = 2
        Me.lbl_Predefinies.Text = "lbl_Predefinies"
        Me.lbl_Predefinies.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Predefinies
        '
        Me.pan_Predefinies.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Predefinies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Predefinies.Controls.Add(Me.img_EL_Eq01)
        Me.pan_Predefinies.Controls.Add(Me.chk_Combinaison01)
        Me.pan_Predefinies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Predefinies.Location = New System.Drawing.Point(0, 71)
        Me.pan_Predefinies.Margin = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.pan_Predefinies.Name = "pan_Predefinies"
        Me.pan_Predefinies.Size = New System.Drawing.Size(467, 218)
        Me.pan_Predefinies.TabIndex = 3
        '
        'lbl_Custom
        '
        Me.lbl_Custom.AutoSize = True
        Me.lbl_Custom.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Custom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Custom.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Custom.Location = New System.Drawing.Point(0, 290)
        Me.lbl_Custom.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Custom.Name = "lbl_Custom"
        Me.lbl_Custom.Size = New System.Drawing.Size(467, 30)
        Me.lbl_Custom.TabIndex = 4
        Me.lbl_Custom.Text = "lbl_Custom"
        Me.lbl_Custom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'img_EL_Eq01
        '
        Me.img_EL_Eq01.Location = New System.Drawing.Point(181, 11)
        Me.img_EL_Eq01.Name = "img_EL_Eq01"
        Me.img_EL_Eq01.Size = New System.Drawing.Size(276, 59)
        Me.img_EL_Eq01.TabIndex = 8
        Me.img_EL_Eq01.TabStop = False
        '
        'chk_Combinaison01
        '
        Me.chk_Combinaison01.AutoSize = True
        Me.chk_Combinaison01.Location = New System.Drawing.Point(9, 12)
        Me.chk_Combinaison01.Name = "chk_Combinaison01"
        Me.chk_Combinaison01.Size = New System.Drawing.Size(122, 17)
        Me.chk_Combinaison01.TabIndex = 7
        Me.chk_Combinaison01.Text = "chk_Combinaison01"
        Me.chk_Combinaison01.UseVisualStyleBackColor = True
        '
        'Frm_Combinaisons
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(473, 498)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_Combinaisons"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Combinaisons"
        Me.pan_General.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Lignes.ResumeLayout(False)
        Me.TLpan_Lignes.PerformLayout()
        Me.TLpan_ChoixEL.ResumeLayout(False)
        Me.TLpan_ChoixEL.PerformLayout()
        Me.pan_Predefinies.ResumeLayout(False)
        Me.pan_Predefinies.PerformLayout()
        CType(Me.img_EL_Eq01, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lbl_Predefinies As Label
    Friend WithEvents lbl_Custom As Label
    Friend WithEvents pan_Predefinies As Panel
    Friend WithEvents img_EL_Eq01 As PictureBox
    Friend WithEvents chk_Combinaison01 As CheckBox
End Class
