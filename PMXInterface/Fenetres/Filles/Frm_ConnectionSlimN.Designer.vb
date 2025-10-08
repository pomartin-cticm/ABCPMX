<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_ConnectionSlimN
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
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_Choix = New System.Windows.Forms.TableLayoutPanel()
        Me.rdb_Connexion = New System.Windows.Forms.RadioButton()
        Me.rdb_Connecteurs = New System.Windows.Forms.RadioButton()
        Me.pan_ContenuFille = New System.Windows.Forms.Panel()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
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
        Me.pan_General.Size = New System.Drawing.Size(841, 577)
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
        Me.TLpan_Main.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 2
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(841, 577)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 5
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 3, 0)
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_Annuler, 1, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(4, 532)
        Me.TLPan_PartieBasse.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(833, 41)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(434, 4)
        Me.btn_OK.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(152, 33)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'btn_Annuler
        '
        Me.btn_Annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Annuler.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Annuler.Location = New System.Drawing.Point(247, 4)
        Me.btn_Annuler.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(152, 33)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLpan_Gauche)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(4, 4)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(833, 520)
        Me.pan_Main.TabIndex = 1
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.TLpan_Choix, 0, 0)
        Me.TLpan_Gauche.Controls.Add(Me.pan_ContenuFille, 0, 1)
        Me.TLpan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Gauche.Name = "TLpan_Gauche"
        Me.TLpan_Gauche.RowCount = 2
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Size = New System.Drawing.Size(833, 520)
        Me.TLpan_Gauche.TabIndex = 2
        '
        'TLpan_Choix
        '
        Me.TLpan_Choix.ColumnCount = 2
        Me.TLpan_Choix.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_Choix.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_Choix.Controls.Add(Me.rdb_Connexion, 1, 0)
        Me.TLpan_Choix.Controls.Add(Me.rdb_Connecteurs, 0, 0)
        Me.TLpan_Choix.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Choix.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Choix.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Choix.Name = "TLpan_Choix"
        Me.TLpan_Choix.RowCount = 1
        Me.TLpan_Choix.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Choix.Size = New System.Drawing.Size(833, 37)
        Me.TLpan_Choix.TabIndex = 1
        '
        'rdb_Connexion
        '
        Me.rdb_Connexion.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_Connexion.AutoSize = True
        Me.rdb_Connexion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_Connexion.Location = New System.Drawing.Point(416, 0)
        Me.rdb_Connexion.Margin = New System.Windows.Forms.Padding(0)
        Me.rdb_Connexion.Name = "rdb_Connexion"
        Me.rdb_Connexion.Size = New System.Drawing.Size(417, 37)
        Me.rdb_Connexion.TabIndex = 3
        Me.rdb_Connexion.Text = "rdb_Connexion"
        Me.rdb_Connexion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_Connexion.UseVisualStyleBackColor = True
        '
        'rdb_Connecteurs
        '
        Me.rdb_Connecteurs.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_Connecteurs.AutoSize = True
        Me.rdb_Connecteurs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_Connecteurs.Location = New System.Drawing.Point(0, 0)
        Me.rdb_Connecteurs.Margin = New System.Windows.Forms.Padding(0)
        Me.rdb_Connecteurs.Name = "rdb_Connecteurs"
        Me.rdb_Connecteurs.Size = New System.Drawing.Size(416, 37)
        Me.rdb_Connecteurs.TabIndex = 2
        Me.rdb_Connecteurs.Text = "rdb_Connecteurs"
        Me.rdb_Connecteurs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_Connecteurs.UseVisualStyleBackColor = True
        '
        'pan_ContenuFille
        '
        Me.pan_ContenuFille.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ContenuFille.Location = New System.Drawing.Point(0, 37)
        Me.pan_ContenuFille.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ContenuFille.Name = "pan_ContenuFille"
        Me.pan_ContenuFille.Size = New System.Drawing.Size(833, 483)
        Me.pan_ContenuFille.TabIndex = 2
        '
        'Frm_ConnectionSlimN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(841, 577)
        Me.Controls.Add(Me.pan_General)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_ConnectionSlimN"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_ConnectionSlimN"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
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
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents TLpan_Choix As TableLayoutPanel
    Friend WithEvents rdb_Connexion As RadioButton
    Friend WithEvents rdb_Connecteurs As RadioButton
    Friend WithEvents pan_ContenuFille As Panel
End Class
