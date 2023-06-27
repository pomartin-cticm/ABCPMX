<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Maintiens
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
        Me.TLPan_Maintiens = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_ControlDessin = New System.Windows.Forms.Panel()
        Me.btn_Delete = New System.Windows.Forms.Button()
        Me.btn_Add = New System.Windows.Forms.Button()
        Me.lbl_Maintiens = New System.Windows.Forms.Label()
        Me.lbl_ControlDessin = New System.Windows.Forms.Label()
        Me.pan_Maintiens = New System.Windows.Forms.Panel()
        Me.rad_NonRestrain = New System.Windows.Forms.RadioButton()
        Me.lbl_Travee = New System.Windows.Forms.Label()
        Me.cmb_Travee = New System.Windows.Forms.ComboBox()
        Me.rad_PointRestrain = New System.Windows.Forms.RadioButton()
        Me.rad_FullyRestrain = New System.Windows.Forms.RadioButton()
        Me.pan_Img_Maintiens = New System.Windows.Forms.Panel()
        Me.txt_Cotations = New System.Windows.Forms.TextBox()
        Me.img_Maintiens = New System.Windows.Forms.PictureBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Maintiens.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_ControlDessin.SuspendLayout()
        Me.pan_Maintiens.SuspendLayout()
        Me.pan_Img_Maintiens.SuspendLayout()
        CType(Me.img_Maintiens, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(1482, 349)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(1482, 349)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 312)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1476, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(751, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(611, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_Maintiens)
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(1476, 303)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Maintiens
        '
        Me.TLPan_Maintiens.ColumnCount = 2
        Me.TLPan_Maintiens.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_Maintiens.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Maintiens.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Maintiens.Controls.Add(Me.pan_Img_Maintiens, 1, 0)
        Me.TLPan_Maintiens.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Maintiens.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Maintiens.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Maintiens.Name = "TLPan_Maintiens"
        Me.TLPan_Maintiens.RowCount = 1
        Me.TLPan_Maintiens.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Maintiens.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Maintiens.Size = New System.Drawing.Size(1476, 303)
        Me.TLPan_Maintiens.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(250, 303)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.pan_ControlDessin, 0, 3)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Maintiens, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_ControlDessin, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Maintiens, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 4
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(250, 303)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'pan_ControlDessin
        '
        Me.pan_ControlDessin.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ControlDessin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ControlDessin.Controls.Add(Me.btn_Delete)
        Me.pan_ControlDessin.Controls.Add(Me.btn_Add)
        Me.pan_ControlDessin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ControlDessin.Location = New System.Drawing.Point(0, 240)
        Me.pan_ControlDessin.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ControlDessin.Name = "pan_ControlDessin"
        Me.pan_ControlDessin.Size = New System.Drawing.Size(250, 63)
        Me.pan_ControlDessin.TabIndex = 2
        '
        'btn_Delete
        '
        Me.btn_Delete.Location = New System.Drawing.Point(128, 18)
        Me.btn_Delete.Name = "btn_Delete"
        Me.btn_Delete.Size = New System.Drawing.Size(114, 28)
        Me.btn_Delete.TabIndex = 3
        Me.btn_Delete.Text = "btn_Delete"
        Me.btn_Delete.UseVisualStyleBackColor = True
        '
        'btn_Add
        '
        Me.btn_Add.Location = New System.Drawing.Point(7, 18)
        Me.btn_Add.Name = "btn_Add"
        Me.btn_Add.Size = New System.Drawing.Size(114, 28)
        Me.btn_Add.TabIndex = 2
        Me.btn_Add.Text = "btn_Add"
        Me.btn_Add.UseVisualStyleBackColor = True
        '
        'lbl_Maintiens
        '
        Me.lbl_Maintiens.AutoSize = True
        Me.lbl_Maintiens.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Maintiens.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Maintiens.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Maintiens.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Maintiens.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Maintiens.Name = "lbl_Maintiens"
        Me.lbl_Maintiens.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Maintiens.TabIndex = 0
        Me.lbl_Maintiens.Text = "lbl_Maintiens"
        Me.lbl_Maintiens.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_ControlDessin
        '
        Me.lbl_ControlDessin.AutoSize = True
        Me.lbl_ControlDessin.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_ControlDessin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ControlDessin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ControlDessin.Location = New System.Drawing.Point(0, 210)
        Me.lbl_ControlDessin.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ControlDessin.Name = "lbl_ControlDessin"
        Me.lbl_ControlDessin.Size = New System.Drawing.Size(250, 30)
        Me.lbl_ControlDessin.TabIndex = 3
        Me.lbl_ControlDessin.Text = "lbl_ControlDessin"
        Me.lbl_ControlDessin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Maintiens
        '
        Me.pan_Maintiens.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Maintiens.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Maintiens.Controls.Add(Me.rad_NonRestrain)
        Me.pan_Maintiens.Controls.Add(Me.lbl_Travee)
        Me.pan_Maintiens.Controls.Add(Me.cmb_Travee)
        Me.pan_Maintiens.Controls.Add(Me.rad_PointRestrain)
        Me.pan_Maintiens.Controls.Add(Me.rad_FullyRestrain)
        Me.pan_Maintiens.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Maintiens.Location = New System.Drawing.Point(0, 30)
        Me.pan_Maintiens.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Maintiens.Name = "pan_Maintiens"
        Me.pan_Maintiens.Size = New System.Drawing.Size(250, 179)
        Me.pan_Maintiens.TabIndex = 1
        '
        'rad_NonRestrain
        '
        Me.rad_NonRestrain.AutoSize = True
        Me.rad_NonRestrain.Location = New System.Drawing.Point(11, 82)
        Me.rad_NonRestrain.Name = "rad_NonRestrain"
        Me.rad_NonRestrain.Size = New System.Drawing.Size(105, 17)
        Me.rad_NonRestrain.TabIndex = 8
        Me.rad_NonRestrain.TabStop = True
        Me.rad_NonRestrain.Text = "rad_NonRestrain"
        Me.rad_NonRestrain.UseVisualStyleBackColor = True
        '
        'lbl_Travee
        '
        Me.lbl_Travee.AutoSize = True
        Me.lbl_Travee.Location = New System.Drawing.Point(8, 17)
        Me.lbl_Travee.Name = "lbl_Travee"
        Me.lbl_Travee.Size = New System.Drawing.Size(57, 13)
        Me.lbl_Travee.TabIndex = 7
        Me.lbl_Travee.Text = "lbl_Travee"
        '
        'cmb_Travee
        '
        Me.cmb_Travee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Travee.FormattingEnabled = True
        Me.cmb_Travee.Location = New System.Drawing.Point(65, 41)
        Me.cmb_Travee.Name = "cmb_Travee"
        Me.cmb_Travee.Size = New System.Drawing.Size(111, 21)
        Me.cmb_Travee.TabIndex = 4
        '
        'rad_PointRestrain
        '
        Me.rad_PointRestrain.AutoSize = True
        Me.rad_PointRestrain.Location = New System.Drawing.Point(11, 138)
        Me.rad_PointRestrain.Name = "rad_PointRestrain"
        Me.rad_PointRestrain.Size = New System.Drawing.Size(109, 17)
        Me.rad_PointRestrain.TabIndex = 3
        Me.rad_PointRestrain.TabStop = True
        Me.rad_PointRestrain.Text = "rad_PointRestrain"
        Me.rad_PointRestrain.UseVisualStyleBackColor = True
        '
        'rad_FullyRestrain
        '
        Me.rad_FullyRestrain.AutoSize = True
        Me.rad_FullyRestrain.Location = New System.Drawing.Point(11, 110)
        Me.rad_FullyRestrain.Name = "rad_FullyRestrain"
        Me.rad_FullyRestrain.Size = New System.Drawing.Size(106, 17)
        Me.rad_FullyRestrain.TabIndex = 2
        Me.rad_FullyRestrain.TabStop = True
        Me.rad_FullyRestrain.Text = "rad_FullyRestrain"
        Me.rad_FullyRestrain.UseVisualStyleBackColor = True
        '
        'pan_Img_Maintiens
        '
        Me.pan_Img_Maintiens.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Img_Maintiens.Controls.Add(Me.txt_Cotations)
        Me.pan_Img_Maintiens.Controls.Add(Me.img_Maintiens)
        Me.pan_Img_Maintiens.Location = New System.Drawing.Point(251, 0)
        Me.pan_Img_Maintiens.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Img_Maintiens.Name = "pan_Img_Maintiens"
        Me.pan_Img_Maintiens.Size = New System.Drawing.Size(1225, 303)
        Me.pan_Img_Maintiens.TabIndex = 1
        '
        'txt_Cotations
        '
        Me.txt_Cotations.Location = New System.Drawing.Point(308, 141)
        Me.txt_Cotations.Name = "txt_Cotations"
        Me.txt_Cotations.Size = New System.Drawing.Size(100, 20)
        Me.txt_Cotations.TabIndex = 2
        '
        'img_Maintiens
        '
        Me.img_Maintiens.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Maintiens.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Maintiens.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Maintiens.Location = New System.Drawing.Point(1, 0)
        Me.img_Maintiens.Margin = New System.Windows.Forms.Padding(0)
        Me.img_Maintiens.Name = "img_Maintiens"
        Me.img_Maintiens.Size = New System.Drawing.Size(100, 50)
        Me.img_Maintiens.TabIndex = 1
        Me.img_Maintiens.TabStop = False
        '
        'Frm_Maintiens
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1482, 349)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_Maintiens"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Maintiens"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Maintiens.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_ControlDessin.ResumeLayout(False)
        Me.pan_Maintiens.ResumeLayout(False)
        Me.pan_Maintiens.PerformLayout()
        Me.pan_Img_Maintiens.ResumeLayout(False)
        Me.pan_Img_Maintiens.PerformLayout()
        CType(Me.img_Maintiens, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Maintiens As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Maintiens As Label
    Friend WithEvents pan_Maintiens As Panel
    Friend WithEvents img_Maintiens As PictureBox
    Friend WithEvents rad_FullyRestrain As RadioButton
    Friend WithEvents rad_PointRestrain As RadioButton
    Friend WithEvents cmb_Travee As ComboBox
    Friend WithEvents pan_ControlDessin As Panel
    Friend WithEvents btn_Delete As Button
    Friend WithEvents btn_Add As Button
    Friend WithEvents lbl_ControlDessin As Label
    Friend WithEvents lbl_Travee As Label
    Friend WithEvents rad_NonRestrain As RadioButton
    Friend WithEvents pan_Img_Maintiens As Panel
    Friend WithEvents txt_Cotations As TextBox
End Class
