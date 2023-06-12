<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Etaiement
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
        Me.TLPan_Etaiement = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Etaiement = New System.Windows.Forms.Label()
        Me.pan_SaisieEtaiement = New System.Windows.Forms.Panel()
        Me.rad_PointPropped = New System.Windows.Forms.RadioButton()
        Me.rad_FullyPropped = New System.Windows.Forms.RadioButton()
        Me.rad_UnPropped = New System.Windows.Forms.RadioButton()
        Me.pan_PointProps = New System.Windows.Forms.Panel()
        Me.cmb_NbPoint = New System.Windows.Forms.ComboBox()
        Me.chk_EtaisConsoleDroite = New System.Windows.Forms.CheckBox()
        Me.lbl_NbPP = New System.Windows.Forms.Label()
        Me.chk_EtaisConsoleGauche = New System.Windows.Forms.CheckBox()
        Me.img_Etaiement = New System.Windows.Forms.PictureBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Etaiement.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_SaisieEtaiement.SuspendLayout()
        Me.pan_PointProps.SuspendLayout()
        CType(Me.img_Etaiement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(822, 335)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(822, 335)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 298)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(816, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(421, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(281, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_Etaiement)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(816, 289)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Etaiement
        '
        Me.TLPan_Etaiement.ColumnCount = 2
        Me.TLPan_Etaiement.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_Etaiement.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Etaiement.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Etaiement.Controls.Add(Me.img_Etaiement, 1, 0)
        Me.TLPan_Etaiement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Etaiement.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Etaiement.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Etaiement.Name = "TLPan_Etaiement"
        Me.TLPan_Etaiement.RowCount = 1
        Me.TLPan_Etaiement.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Etaiement.Size = New System.Drawing.Size(816, 289)
        Me.TLPan_Etaiement.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(250, 289)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Etaiement, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisieEtaiement, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 2
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(250, 289)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Etaiement
        '
        Me.lbl_Etaiement.AutoSize = True
        Me.lbl_Etaiement.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Etaiement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Etaiement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Etaiement.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Etaiement.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Etaiement.Name = "lbl_Etaiement"
        Me.lbl_Etaiement.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Etaiement.TabIndex = 0
        Me.lbl_Etaiement.Text = "lbl_Etaiement"
        Me.lbl_Etaiement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieEtaiement
        '
        Me.pan_SaisieEtaiement.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieEtaiement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieEtaiement.Controls.Add(Me.rad_PointPropped)
        Me.pan_SaisieEtaiement.Controls.Add(Me.rad_FullyPropped)
        Me.pan_SaisieEtaiement.Controls.Add(Me.rad_UnPropped)
        Me.pan_SaisieEtaiement.Controls.Add(Me.pan_PointProps)
        Me.pan_SaisieEtaiement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieEtaiement.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisieEtaiement.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisieEtaiement.Name = "pan_SaisieEtaiement"
        Me.pan_SaisieEtaiement.Size = New System.Drawing.Size(250, 259)
        Me.pan_SaisieEtaiement.TabIndex = 1
        '
        'rad_PointPropped
        '
        Me.rad_PointPropped.AutoSize = True
        Me.rad_PointPropped.Location = New System.Drawing.Point(16, 94)
        Me.rad_PointPropped.Name = "rad_PointPropped"
        Me.rad_PointPropped.Size = New System.Drawing.Size(110, 17)
        Me.rad_PointPropped.TabIndex = 2
        Me.rad_PointPropped.TabStop = True
        Me.rad_PointPropped.Text = "rad_PointPropped"
        Me.rad_PointPropped.UseVisualStyleBackColor = True
        '
        'rad_FullyPropped
        '
        Me.rad_FullyPropped.AutoSize = True
        Me.rad_FullyPropped.Location = New System.Drawing.Point(16, 57)
        Me.rad_FullyPropped.Name = "rad_FullyPropped"
        Me.rad_FullyPropped.Size = New System.Drawing.Size(107, 17)
        Me.rad_FullyPropped.TabIndex = 1
        Me.rad_FullyPropped.TabStop = True
        Me.rad_FullyPropped.Text = "rad_FullyPropped"
        Me.rad_FullyPropped.UseVisualStyleBackColor = True
        '
        'rad_UnPropped
        '
        Me.rad_UnPropped.AutoSize = True
        Me.rad_UnPropped.Location = New System.Drawing.Point(16, 19)
        Me.rad_UnPropped.Name = "rad_UnPropped"
        Me.rad_UnPropped.Size = New System.Drawing.Size(100, 17)
        Me.rad_UnPropped.TabIndex = 0
        Me.rad_UnPropped.TabStop = True
        Me.rad_UnPropped.Text = "rad_UnPropped"
        Me.rad_UnPropped.UseVisualStyleBackColor = True
        '
        'pan_PointProps
        '
        Me.pan_PointProps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_PointProps.Controls.Add(Me.cmb_NbPoint)
        Me.pan_PointProps.Controls.Add(Me.chk_EtaisConsoleDroite)
        Me.pan_PointProps.Controls.Add(Me.lbl_NbPP)
        Me.pan_PointProps.Controls.Add(Me.chk_EtaisConsoleGauche)
        Me.pan_PointProps.Location = New System.Drawing.Point(8, 105)
        Me.pan_PointProps.Name = "pan_PointProps"
        Me.pan_PointProps.Size = New System.Drawing.Size(232, 132)
        Me.pan_PointProps.TabIndex = 8
        '
        'cmb_NbPoint
        '
        Me.cmb_NbPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbPoint.FormattingEnabled = True
        Me.cmb_NbPoint.Location = New System.Drawing.Point(68, 97)
        Me.cmb_NbPoint.Name = "cmb_NbPoint"
        Me.cmb_NbPoint.Size = New System.Drawing.Size(111, 21)
        Me.cmb_NbPoint.TabIndex = 3
        '
        'chk_EtaisConsoleDroite
        '
        Me.chk_EtaisConsoleDroite.AutoSize = True
        Me.chk_EtaisConsoleDroite.Location = New System.Drawing.Point(4, 44)
        Me.chk_EtaisConsoleDroite.Name = "chk_EtaisConsoleDroite"
        Me.chk_EtaisConsoleDroite.Size = New System.Drawing.Size(139, 17)
        Me.chk_EtaisConsoleDroite.TabIndex = 7
        Me.chk_EtaisConsoleDroite.Text = "chk_EtaisConsoleDroite"
        Me.chk_EtaisConsoleDroite.UseVisualStyleBackColor = True
        '
        'lbl_NbPP
        '
        Me.lbl_NbPP.AutoSize = True
        Me.lbl_NbPP.Location = New System.Drawing.Point(1, 78)
        Me.lbl_NbPP.Name = "lbl_NbPP"
        Me.lbl_NbPP.Size = New System.Drawing.Size(51, 13)
        Me.lbl_NbPP.TabIndex = 6
        Me.lbl_NbPP.Text = "lbl_NbPP"
        '
        'chk_EtaisConsoleGauche
        '
        Me.chk_EtaisConsoleGauche.AutoSize = True
        Me.chk_EtaisConsoleGauche.Location = New System.Drawing.Point(4, 21)
        Me.chk_EtaisConsoleGauche.Name = "chk_EtaisConsoleGauche"
        Me.chk_EtaisConsoleGauche.Size = New System.Drawing.Size(149, 17)
        Me.chk_EtaisConsoleGauche.TabIndex = 5
        Me.chk_EtaisConsoleGauche.Text = "chk_EtaisConsoleGauche"
        Me.chk_EtaisConsoleGauche.UseVisualStyleBackColor = True
        '
        'img_Etaiement
        '
        Me.img_Etaiement.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Etaiement.Location = New System.Drawing.Point(251, 0)
        Me.img_Etaiement.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.img_Etaiement.Name = "img_Etaiement"
        Me.img_Etaiement.Size = New System.Drawing.Size(100, 50)
        Me.img_Etaiement.TabIndex = 1
        Me.img_Etaiement.TabStop = False
        '
        'Frm_Etaiement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(822, 335)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Etaiement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Etaiement"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Etaiement.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_SaisieEtaiement.ResumeLayout(False)
        Me.pan_SaisieEtaiement.PerformLayout()
        Me.pan_PointProps.ResumeLayout(False)
        Me.pan_PointProps.PerformLayout()
        CType(Me.img_Etaiement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Etaiement As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Etaiement As Label
    Friend WithEvents pan_SaisieEtaiement As Panel
    Friend WithEvents img_Etaiement As PictureBox
    Friend WithEvents rad_UnPropped As RadioButton
    Friend WithEvents rad_PointPropped As RadioButton
    Friend WithEvents rad_FullyPropped As RadioButton
    Friend WithEvents cmb_NbPoint As ComboBox
    Friend WithEvents chk_EtaisConsoleGauche As CheckBox
    Friend WithEvents lbl_NbPP As Label
    Friend WithEvents chk_EtaisConsoleDroite As CheckBox
    Friend WithEvents pan_PointProps As Panel
End Class
