<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MaintienBacN
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
        Me.TLpan_MaintienBac = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Image = New System.Windows.Forms.Panel()
        Me.img_Deck = New System.Windows.Forms.PictureBox()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_Choix = New System.Windows.Forms.TableLayoutPanel()
        Me.rdb_Fixations = New System.Windows.Forms.RadioButton()
        Me.rdb_Plancher = New System.Windows.Forms.RadioButton()
        Me.chk_Calculs = New System.Windows.Forms.CheckBox()
        Me.pan_ContenuG = New System.Windows.Forms.Panel()
        Me.pan_Calculs = New System.Windows.Forms.Panel()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_MaintienBac.SuspendLayout()
        Me.pan_Image.SuspendLayout()
        CType(Me.img_Deck, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLpan_Gauche.SuspendLayout()
        Me.TLpan_Choix.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(1029, 535)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(1029, 535)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 498)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1023, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(524, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(384, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLpan_MaintienBac)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(1023, 489)
        Me.pan_Main.TabIndex = 1
        '
        'TLpan_MaintienBac
        '
        Me.TLpan_MaintienBac.ColumnCount = 3
        Me.TLpan_MaintienBac.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TLpan_MaintienBac.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLpan_MaintienBac.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_MaintienBac.Controls.Add(Me.pan_Image, 2, 0)
        Me.TLpan_MaintienBac.Controls.Add(Me.TLpan_Gauche, 0, 0)
        Me.TLpan_MaintienBac.Controls.Add(Me.pan_Calculs, 1, 0)
        Me.TLpan_MaintienBac.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_MaintienBac.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_MaintienBac.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_MaintienBac.Name = "TLpan_MaintienBac"
        Me.TLpan_MaintienBac.RowCount = 1
        Me.TLpan_MaintienBac.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_MaintienBac.Size = New System.Drawing.Size(1023, 489)
        Me.TLpan_MaintienBac.TabIndex = 0
        '
        'pan_Image
        '
        Me.pan_Image.Controls.Add(Me.img_Deck)
        Me.pan_Image.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Image.Location = New System.Drawing.Point(550, 0)
        Me.pan_Image.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Image.Name = "pan_Image"
        Me.pan_Image.Size = New System.Drawing.Size(473, 489)
        Me.pan_Image.TabIndex = 0
        '
        'img_Deck
        '
        Me.img_Deck.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Deck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Deck.Cursor = System.Windows.Forms.Cursors.Default
        Me.img_Deck.Location = New System.Drawing.Point(213, 230)
        Me.img_Deck.Name = "img_Deck"
        Me.img_Deck.Size = New System.Drawing.Size(100, 25)
        Me.img_Deck.TabIndex = 123
        Me.img_Deck.TabStop = False
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.TLpan_Choix, 0, 0)
        Me.TLpan_Gauche.Controls.Add(Me.pan_ContenuG, 0, 1)
        Me.TLpan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Gauche.Name = "TLpan_Gauche"
        Me.TLpan_Gauche.RowCount = 2
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Size = New System.Drawing.Size(300, 489)
        Me.TLpan_Gauche.TabIndex = 1
        '
        'TLpan_Choix
        '
        Me.TLpan_Choix.ColumnCount = 3
        Me.TLpan_Choix.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_Choix.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_Choix.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_Choix.Controls.Add(Me.rdb_Fixations, 1, 0)
        Me.TLpan_Choix.Controls.Add(Me.rdb_Plancher, 0, 0)
        Me.TLpan_Choix.Controls.Add(Me.chk_Calculs, 2, 0)
        Me.TLpan_Choix.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Choix.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Choix.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Choix.Name = "TLpan_Choix"
        Me.TLpan_Choix.RowCount = 1
        Me.TLpan_Choix.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Choix.Size = New System.Drawing.Size(300, 30)
        Me.TLpan_Choix.TabIndex = 1
        '
        'rdb_Fixations
        '
        Me.rdb_Fixations.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_Fixations.AutoSize = True
        Me.rdb_Fixations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_Fixations.Location = New System.Drawing.Point(99, 0)
        Me.rdb_Fixations.Margin = New System.Windows.Forms.Padding(0)
        Me.rdb_Fixations.Name = "rdb_Fixations"
        Me.rdb_Fixations.Size = New System.Drawing.Size(99, 30)
        Me.rdb_Fixations.TabIndex = 3
        Me.rdb_Fixations.Text = "rdb_Fixations"
        Me.rdb_Fixations.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_Fixations.UseVisualStyleBackColor = True
        '
        'rdb_Plancher
        '
        Me.rdb_Plancher.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_Plancher.AutoSize = True
        Me.rdb_Plancher.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_Plancher.Location = New System.Drawing.Point(0, 0)
        Me.rdb_Plancher.Margin = New System.Windows.Forms.Padding(0)
        Me.rdb_Plancher.Name = "rdb_Plancher"
        Me.rdb_Plancher.Size = New System.Drawing.Size(99, 30)
        Me.rdb_Plancher.TabIndex = 2
        Me.rdb_Plancher.Text = "rdb_Plancher"
        Me.rdb_Plancher.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_Plancher.UseVisualStyleBackColor = True
        '
        'chk_Calculs
        '
        Me.chk_Calculs.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Calculs.AutoSize = True
        Me.chk_Calculs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_Calculs.Location = New System.Drawing.Point(198, 0)
        Me.chk_Calculs.Margin = New System.Windows.Forms.Padding(0)
        Me.chk_Calculs.Name = "chk_Calculs"
        Me.chk_Calculs.Size = New System.Drawing.Size(102, 30)
        Me.chk_Calculs.TabIndex = 4
        Me.chk_Calculs.Text = "chk_Calculs"
        Me.chk_Calculs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_Calculs.UseVisualStyleBackColor = True
        '
        'pan_ContenuG
        '
        Me.pan_ContenuG.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ContenuG.Location = New System.Drawing.Point(0, 30)
        Me.pan_ContenuG.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ContenuG.Name = "pan_ContenuG"
        Me.pan_ContenuG.Size = New System.Drawing.Size(300, 459)
        Me.pan_ContenuG.TabIndex = 2
        '
        'pan_Calculs
        '
        Me.pan_Calculs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Calculs.Location = New System.Drawing.Point(300, 0)
        Me.pan_Calculs.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Calculs.Name = "pan_Calculs"
        Me.pan_Calculs.Size = New System.Drawing.Size(250, 489)
        Me.pan_Calculs.TabIndex = 2
        '
        'Frm_MaintienBacN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1029, 535)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_MaintienBacN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Frm_MaintienBacN"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_MaintienBac.ResumeLayout(False)
        Me.pan_Image.ResumeLayout(False)
        CType(Me.img_Deck, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_Choix.ResumeLayout(False)
        Me.TLpan_Choix.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_MaintienBac As TableLayoutPanel
    Friend WithEvents pan_Image As Panel
    Friend WithEvents img_Deck As PictureBox
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents TLpan_Choix As TableLayoutPanel
    Friend WithEvents rdb_Fixations As RadioButton
    Friend WithEvents rdb_Plancher As RadioButton
    Friend WithEvents pan_ContenuG As Panel
    Friend WithEvents chk_Calculs As CheckBox
    Friend WithEvents pan_Calculs As Panel
End Class
