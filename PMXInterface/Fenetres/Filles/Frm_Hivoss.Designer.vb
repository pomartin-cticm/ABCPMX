<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Hivoss
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
        Me.TLPan_Options = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Options = New System.Windows.Forms.Label()
        Me.pan_SaisieOptions = New System.Windows.Forms.Panel()
        Me.pan_Droite = New System.Windows.Forms.Panel()
        Me.TLPan_Droite = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Amortissement = New System.Windows.Forms.Label()
        Me.pan_SaisieAmortissement = New System.Windows.Forms.Panel()
        Me.chk_methodeHIVOSS = New System.Windows.Forms.CheckBox()
        Me.lbl_ComboMasseFrequence = New System.Windows.Forms.Label()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Options.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_SaisieOptions.SuspendLayout()
        Me.pan_Droite.SuspendLayout()
        Me.TLPan_Droite.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(800, 450)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(800, 450)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 413)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(794, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(410, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(270, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_Options)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(794, 404)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Options
        '
        Me.TLPan_Options.ColumnCount = 2
        Me.TLPan_Options.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_Options.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_Options.Controls.Add(Me.pan_Droite, 0, 0)
        Me.TLPan_Options.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Options.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Options.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Options.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Options.Name = "TLPan_Options"
        Me.TLPan_Options.RowCount = 1
        Me.TLPan_Options.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Options.Size = New System.Drawing.Size(794, 404)
        Me.TLPan_Options.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(396, 404)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Options, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisieOptions, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 2
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(396, 404)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Options
        '
        Me.lbl_Options.AutoSize = True
        Me.lbl_Options.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Options.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Options.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Options.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Options.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Options.Name = "lbl_Options"
        Me.lbl_Options.Size = New System.Drawing.Size(396, 30)
        Me.lbl_Options.TabIndex = 0
        Me.lbl_Options.Text = "lbl_Options"
        Me.lbl_Options.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieOptions
        '
        Me.pan_SaisieOptions.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieOptions.Controls.Add(Me.lbl_ComboMasseFrequence)
        Me.pan_SaisieOptions.Controls.Add(Me.chk_methodeHIVOSS)
        Me.pan_SaisieOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieOptions.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisieOptions.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisieOptions.Name = "pan_SaisieOptions"
        Me.pan_SaisieOptions.Size = New System.Drawing.Size(396, 374)
        Me.pan_SaisieOptions.TabIndex = 1
        '
        'pan_Droite
        '
        Me.pan_Droite.AutoScroll = True
        Me.pan_Droite.Controls.Add(Me.TLPan_Droite)
        Me.pan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Droite.Location = New System.Drawing.Point(397, 0)
        Me.pan_Droite.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Droite.Name = "pan_Droite"
        Me.pan_Droite.Size = New System.Drawing.Size(397, 404)
        Me.pan_Droite.TabIndex = 1
        '
        'TLPan_Droite
        '
        Me.TLPan_Droite.ColumnCount = 1
        Me.TLPan_Droite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Droite.Controls.Add(Me.lbl_Amortissement, 0, 0)
        Me.TLPan_Droite.Controls.Add(Me.pan_SaisieAmortissement, 0, 1)
        Me.TLPan_Droite.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Droite.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Droite.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Droite.Name = "TLPan_Droite"
        Me.TLPan_Droite.RowCount = 2
        Me.TLPan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_Droite.Size = New System.Drawing.Size(397, 404)
        Me.TLPan_Droite.TabIndex = 0
        '
        'lbl_Amortissement
        '
        Me.lbl_Amortissement.AutoSize = True
        Me.lbl_Amortissement.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Amortissement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Amortissement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Amortissement.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Amortissement.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Amortissement.Name = "lbl_Amortissement"
        Me.lbl_Amortissement.Size = New System.Drawing.Size(397, 30)
        Me.lbl_Amortissement.TabIndex = 0
        Me.lbl_Amortissement.Text = "Label1"
        Me.lbl_Amortissement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieAmortissement
        '
        Me.pan_SaisieAmortissement.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieAmortissement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieAmortissement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieAmortissement.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisieAmortissement.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisieAmortissement.Name = "pan_SaisieAmortissement"
        Me.pan_SaisieAmortissement.Size = New System.Drawing.Size(397, 374)
        Me.pan_SaisieAmortissement.TabIndex = 1
        '
        'chk_methodeHIVOSS
        '
        Me.chk_methodeHIVOSS.AutoSize = True
        Me.chk_methodeHIVOSS.Location = New System.Drawing.Point(19, 16)
        Me.chk_methodeHIVOSS.Name = "chk_methodeHIVOSS"
        Me.chk_methodeHIVOSS.Size = New System.Drawing.Size(131, 17)
        Me.chk_methodeHIVOSS.TabIndex = 0
        Me.chk_methodeHIVOSS.Text = "chk_methodeHIVOSS"
        Me.chk_methodeHIVOSS.UseVisualStyleBackColor = True
        '
        'lbl_ComboMasseFrequence
        '
        Me.lbl_ComboMasseFrequence.AutoSize = True
        Me.lbl_ComboMasseFrequence.Location = New System.Drawing.Point(16, 46)
        Me.lbl_ComboMasseFrequence.Name = "lbl_ComboMasseFrequence"
        Me.lbl_ComboMasseFrequence.Size = New System.Drawing.Size(138, 13)
        Me.lbl_ComboMasseFrequence.TabIndex = 1
        Me.lbl_ComboMasseFrequence.Text = "lbl_ComboMasseFrequence"
        '
        'Frm_Hivoss
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_Hivoss"
        Me.Text = "Frm_Hivoss"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Options.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_SaisieOptions.ResumeLayout(False)
        Me.pan_SaisieOptions.PerformLayout()
        Me.pan_Droite.ResumeLayout(False)
        Me.TLPan_Droite.ResumeLayout(False)
        Me.TLPan_Droite.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Options As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Options As Label
    Friend WithEvents pan_SaisieOptions As Panel
    Friend WithEvents pan_Droite As Panel
    Friend WithEvents TLPan_Droite As TableLayoutPanel
    Friend WithEvents lbl_Amortissement As Label
    Friend WithEvents pan_SaisieAmortissement As Panel
    Friend WithEvents lbl_ComboMasseFrequence As Label
    Friend WithEvents chk_methodeHIVOSS As CheckBox
End Class
