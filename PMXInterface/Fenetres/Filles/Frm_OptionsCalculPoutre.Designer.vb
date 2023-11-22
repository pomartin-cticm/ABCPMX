<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_OptionsCalculPoutre
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Portees = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_OptionsELS = New System.Windows.Forms.Panel()
        Me.lbl_CadreELS = New System.Windows.Forms.Label()
        Me.pan_OptionsELU = New System.Windows.Forms.Panel()
        Me.rdb_ElasticDesign = New System.Windows.Forms.RadioButton()
        Me.rdb_NormalDesign = New System.Windows.Forms.RadioButton()
        Me.lbl_CadreELU = New System.Windows.Forms.Label()
        Me.lbl_CadreNorm = New System.Windows.Forms.Label()
        Me.pan_Norm = New System.Windows.Forms.Panel()
        Me.cmb_Norme = New System.Windows.Forms.ComboBox()
        Me.lbl_Norme = New System.Windows.Forms.Label()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Portees.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_OptionsELU.SuspendLayout()
        Me.pan_Norm.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(580, 543)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(580, 543)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 506)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(574, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(300, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(160, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_Portees)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(574, 497)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Portees
        '
        Me.TLPan_Portees.ColumnCount = 1
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Portees.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Portees.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Portees.Name = "TLPan_Portees"
        Me.TLPan_Portees.RowCount = 1
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Size = New System.Drawing.Size(574, 497)
        Me.TLPan_Portees.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(574, 497)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.pan_OptionsELS, 0, 5)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreELS, 0, 4)
        Me.TLPan_Gauche.Controls.Add(Me.pan_OptionsELU, 0, 3)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreELU, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_CadreNorm, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Norm, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 7
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(557, 603)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'pan_OptionsELS
        '
        Me.pan_OptionsELS.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_OptionsELS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_OptionsELS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_OptionsELS.Location = New System.Drawing.Point(0, 225)
        Me.pan_OptionsELS.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_OptionsELS.Name = "pan_OptionsELS"
        Me.pan_OptionsELS.Size = New System.Drawing.Size(557, 120)
        Me.pan_OptionsELS.TabIndex = 5
        '
        'lbl_CadreELS
        '
        Me.lbl_CadreELS.AutoSize = True
        Me.lbl_CadreELS.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreELS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreELS.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreELS.Location = New System.Drawing.Point(0, 195)
        Me.lbl_CadreELS.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreELS.Name = "lbl_CadreELS"
        Me.lbl_CadreELS.Size = New System.Drawing.Size(557, 30)
        Me.lbl_CadreELS.TabIndex = 4
        Me.lbl_CadreELS.Text = "lbl_CadreELS"
        Me.lbl_CadreELS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_OptionsELU
        '
        Me.pan_OptionsELU.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_OptionsELU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_OptionsELU.Controls.Add(Me.rdb_ElasticDesign)
        Me.pan_OptionsELU.Controls.Add(Me.rdb_NormalDesign)
        Me.pan_OptionsELU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_OptionsELU.Location = New System.Drawing.Point(0, 115)
        Me.pan_OptionsELU.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_OptionsELU.Name = "pan_OptionsELU"
        Me.pan_OptionsELU.Size = New System.Drawing.Size(557, 80)
        Me.pan_OptionsELU.TabIndex = 3
        '
        'rdb_ElasticDesign
        '
        Me.rdb_ElasticDesign.AutoSize = True
        Me.rdb_ElasticDesign.Location = New System.Drawing.Point(21, 39)
        Me.rdb_ElasticDesign.Name = "rdb_ElasticDesign"
        Me.rdb_ElasticDesign.Size = New System.Drawing.Size(110, 17)
        Me.rdb_ElasticDesign.TabIndex = 1
        Me.rdb_ElasticDesign.TabStop = True
        Me.rdb_ElasticDesign.Text = "rdb_ElasticDesign"
        Me.rdb_ElasticDesign.UseVisualStyleBackColor = True
        '
        'rdb_NormalDesign
        '
        Me.rdb_NormalDesign.AutoSize = True
        Me.rdb_NormalDesign.Location = New System.Drawing.Point(21, 16)
        Me.rdb_NormalDesign.Name = "rdb_NormalDesign"
        Me.rdb_NormalDesign.Size = New System.Drawing.Size(112, 17)
        Me.rdb_NormalDesign.TabIndex = 0
        Me.rdb_NormalDesign.TabStop = True
        Me.rdb_NormalDesign.Text = "rbd_NormalDesign"
        Me.rdb_NormalDesign.UseVisualStyleBackColor = True
        '
        'lbl_CadreELU
        '
        Me.lbl_CadreELU.AutoSize = True
        Me.lbl_CadreELU.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreELU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreELU.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreELU.Location = New System.Drawing.Point(0, 85)
        Me.lbl_CadreELU.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreELU.Name = "lbl_CadreELU"
        Me.lbl_CadreELU.Size = New System.Drawing.Size(557, 30)
        Me.lbl_CadreELU.TabIndex = 2
        Me.lbl_CadreELU.Text = "lbl_CadreELU"
        Me.lbl_CadreELU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_CadreNorm
        '
        Me.lbl_CadreNorm.AutoSize = True
        Me.lbl_CadreNorm.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CadreNorm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CadreNorm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CadreNorm.Location = New System.Drawing.Point(0, 0)
        Me.lbl_CadreNorm.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CadreNorm.Name = "lbl_CadreNorm"
        Me.lbl_CadreNorm.Size = New System.Drawing.Size(557, 30)
        Me.lbl_CadreNorm.TabIndex = 0
        Me.lbl_CadreNorm.Text = "lbl_CadreNorm"
        Me.lbl_CadreNorm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Norm
        '
        Me.pan_Norm.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Norm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Norm.Controls.Add(Me.cmb_Norme)
        Me.pan_Norm.Controls.Add(Me.lbl_Norme)
        Me.pan_Norm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Norm.Location = New System.Drawing.Point(0, 30)
        Me.pan_Norm.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Norm.Name = "pan_Norm"
        Me.pan_Norm.Size = New System.Drawing.Size(557, 55)
        Me.pan_Norm.TabIndex = 1
        '
        'cmb_Norme
        '
        Me.cmb_Norme.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Norme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Norme.FormattingEnabled = True
        Me.cmb_Norme.Location = New System.Drawing.Point(186, 15)
        Me.cmb_Norme.Name = "cmb_Norme"
        Me.cmb_Norme.Size = New System.Drawing.Size(355, 21)
        Me.cmb_Norme.TabIndex = 106
        '
        'lbl_Norme
        '
        Me.lbl_Norme.AutoSize = True
        Me.lbl_Norme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Norme.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl_Norme.Location = New System.Drawing.Point(8, 18)
        Me.lbl_Norme.Name = "lbl_Norme"
        Me.lbl_Norme.Size = New System.Drawing.Size(54, 13)
        Me.lbl_Norme.TabIndex = 105
        Me.lbl_Norme.Text = "lbl_Norme"
        Me.lbl_Norme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Frm_OptionsCalculPoutre
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(580, 543)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_OptionsCalculPoutre"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_OptionsCalcuPoutre"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Portees.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_OptionsELU.ResumeLayout(False)
        Me.pan_OptionsELU.PerformLayout()
        Me.pan_Norm.ResumeLayout(False)
        Me.pan_Norm.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Portees As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_CadreNorm As Label
    Friend WithEvents pan_Norm As Panel
    Friend WithEvents cmb_Norme As ComboBox
    Friend WithEvents lbl_Norme As Label
    Friend WithEvents pan_OptionsELU As Panel
    Friend WithEvents lbl_CadreELU As Label
    Friend WithEvents rdb_ElasticDesign As RadioButton
    Friend WithEvents rdb_NormalDesign As RadioButton
    Friend WithEvents pan_OptionsELS As Panel
    Friend WithEvents lbl_CadreELS As Label
End Class
