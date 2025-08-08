<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_DalleSlimFloorN
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
        Me.TLPan_Dalle = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_ContenuFille = New System.Windows.Forms.Panel()
        Me.TLpan_Choix = New System.Windows.Forms.TableLayoutPanel()
        Me.rdb_Bac = New System.Windows.Forms.RadioButton()
        Me.rdb_Arma = New System.Windows.Forms.RadioButton()
        Me.rdb_General = New System.Windows.Forms.RadioButton()
        Me.pan_Img = New System.Windows.Forms.Panel()
        Me.img_Dalle = New System.Windows.Forms.PictureBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Dalle.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLpan_Gauche.SuspendLayout()
        Me.TLpan_Choix.SuspendLayout()
        Me.pan_Img.SuspendLayout()
        CType(Me.img_Dalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(914, 472)
        Me.pan_General.TabIndex = 5
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
        Me.TLpan_Main.Size = New System.Drawing.Size(914, 472)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 435)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(908, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(467, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(327, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_Dalle)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(908, 426)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Dalle
        '
        Me.TLPan_Dalle.ColumnCount = 2
        Me.TLPan_Dalle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_Dalle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Dalle.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Dalle.Controls.Add(Me.pan_Img, 1, 0)
        Me.TLPan_Dalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Dalle.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Dalle.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Dalle.Name = "TLPan_Dalle"
        Me.TLPan_Dalle.RowCount = 1
        Me.TLPan_Dalle.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Dalle.Size = New System.Drawing.Size(908, 426)
        Me.TLPan_Dalle.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLpan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(250, 426)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.pan_ContenuFille, 0, 1)
        Me.TLpan_Gauche.Controls.Add(Me.TLpan_Choix, 0, 0)
        Me.TLpan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Gauche.Name = "TLpan_Gauche"
        Me.TLpan_Gauche.RowCount = 2
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Size = New System.Drawing.Size(250, 426)
        Me.TLpan_Gauche.TabIndex = 0
        '
        'pan_ContenuFille
        '
        Me.pan_ContenuFille.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ContenuFille.Location = New System.Drawing.Point(0, 30)
        Me.pan_ContenuFille.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ContenuFille.Name = "pan_ContenuFille"
        Me.pan_ContenuFille.Size = New System.Drawing.Size(250, 396)
        Me.pan_ContenuFille.TabIndex = 3
        '
        'TLpan_Choix
        '
        Me.TLpan_Choix.ColumnCount = 3
        Me.TLpan_Choix.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_Choix.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_Choix.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_Choix.Controls.Add(Me.rdb_Bac, 2, 0)
        Me.TLpan_Choix.Controls.Add(Me.rdb_Arma, 1, 0)
        Me.TLpan_Choix.Controls.Add(Me.rdb_General, 0, 0)
        Me.TLpan_Choix.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Choix.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Choix.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Choix.Name = "TLpan_Choix"
        Me.TLpan_Choix.RowCount = 1
        Me.TLpan_Choix.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Choix.Size = New System.Drawing.Size(250, 30)
        Me.TLpan_Choix.TabIndex = 2
        '
        'rdb_Bac
        '
        Me.rdb_Bac.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_Bac.AutoSize = True
        Me.rdb_Bac.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_Bac.Location = New System.Drawing.Point(166, 0)
        Me.rdb_Bac.Margin = New System.Windows.Forms.Padding(0)
        Me.rdb_Bac.Name = "rdb_Bac"
        Me.rdb_Bac.Size = New System.Drawing.Size(84, 30)
        Me.rdb_Bac.TabIndex = 4
        Me.rdb_Bac.Text = "rdb_Bac"
        Me.rdb_Bac.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_Bac.UseVisualStyleBackColor = True
        '
        'rdb_Arma
        '
        Me.rdb_Arma.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_Arma.AutoSize = True
        Me.rdb_Arma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_Arma.Location = New System.Drawing.Point(83, 0)
        Me.rdb_Arma.Margin = New System.Windows.Forms.Padding(0)
        Me.rdb_Arma.Name = "rdb_Arma"
        Me.rdb_Arma.Size = New System.Drawing.Size(83, 30)
        Me.rdb_Arma.TabIndex = 3
        Me.rdb_Arma.Text = "rdb_Arma"
        Me.rdb_Arma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_Arma.UseVisualStyleBackColor = True
        '
        'rdb_General
        '
        Me.rdb_General.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_General.AutoSize = True
        Me.rdb_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_General.Location = New System.Drawing.Point(0, 0)
        Me.rdb_General.Margin = New System.Windows.Forms.Padding(0)
        Me.rdb_General.Name = "rdb_General"
        Me.rdb_General.Size = New System.Drawing.Size(83, 30)
        Me.rdb_General.TabIndex = 2
        Me.rdb_General.Text = "rdb_General"
        Me.rdb_General.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_General.UseVisualStyleBackColor = True
        '
        'pan_Img
        '
        Me.pan_Img.Controls.Add(Me.img_Dalle)
        Me.pan_Img.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Img.Location = New System.Drawing.Point(251, 0)
        Me.pan_Img.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Img.Name = "pan_Img"
        Me.pan_Img.Size = New System.Drawing.Size(657, 426)
        Me.pan_Img.TabIndex = 1
        '
        'img_Dalle
        '
        Me.img_Dalle.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Dalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Dalle.Location = New System.Drawing.Point(117, 10)
        Me.img_Dalle.Margin = New System.Windows.Forms.Padding(1, 0, 0, 1)
        Me.img_Dalle.Name = "img_Dalle"
        Me.img_Dalle.Size = New System.Drawing.Size(100, 50)
        Me.img_Dalle.TabIndex = 3
        Me.img_Dalle.TabStop = False
        '
        'Frm_DalleSlimFloorN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(914, 472)
        Me.Controls.Add(Me.pan_General)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_DalleSlimFloorN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Frm_DalleSlimFloorN"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Dalle.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_Choix.ResumeLayout(False)
        Me.TLpan_Choix.PerformLayout()
        Me.pan_Img.ResumeLayout(False)
        CType(Me.img_Dalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Dalle As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents pan_Img As Panel
    Friend WithEvents img_Dalle As PictureBox
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents TLpan_Choix As TableLayoutPanel
    Friend WithEvents rdb_Bac As RadioButton
    Friend WithEvents rdb_Arma As RadioButton
    Friend WithEvents rdb_General As RadioButton
    Friend WithEvents pan_ContenuFille As Panel
End Class
