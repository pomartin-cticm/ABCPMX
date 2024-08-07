<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PPHivoss
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
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.Pan_Affichage = New System.Windows.Forms.Panel()
        Me.TLpan_AffichageCentral = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Image = New System.Windows.Forms.Panel()
        Me.TLpan_HAffichage = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.img_Hivoss = New System.Windows.Forms.PictureBox()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.lbl_Hivoss = New System.Windows.Forms.Label()
        Me.TLpan_Modules = New System.Windows.Forms.TableLayoutPanel()
        Me.Pan_Results = New System.Windows.Forms.Panel()
        Me.txt_Classe = New System.Windows.Forms.TextBox()
        Me.lbl_Classe = New System.Windows.Forms.Label()
        Me.etq_UnitOSRMS = New System.Windows.Forms.Label()
        Me.txt_OSRMS = New System.Windows.Forms.TextBox()
        Me.lbl_OSRMS = New System.Windows.Forms.Label()
        Me.etq_UnitMass2 = New System.Windows.Forms.Label()
        Me.txt_MassModal = New System.Windows.Forms.TextBox()
        Me.lbl_MassModal = New System.Windows.Forms.Label()
        Me.etq_UnitAmortissement = New System.Windows.Forms.Label()
        Me.txt_Amortissement = New System.Windows.Forms.TextBox()
        Me.lbl_Amortissement = New System.Windows.Forms.Label()
        Me.etq_UnitFreq = New System.Windows.Forms.Label()
        Me.txt_Frequence = New System.Windows.Forms.TextBox()
        Me.lbl_Frequence = New System.Windows.Forms.Label()
        Me.lbl_Resultats = New System.Windows.Forms.Label()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.Pan_Affichage.SuspendLayout()
        Me.TLpan_AffichageCentral.SuspendLayout()
        Me.pan_Image.SuspendLayout()
        Me.TLpan_HAffichage.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.img_Hivoss, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.TLpan_Modules.SuspendLayout()
        Me.Pan_Results.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.Controls.Add(Me.TLpan_Main)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(0, 0)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(700, 520)
        Me.pan_Main.TabIndex = 3
        '
        'TLpan_Main
        '
        Me.TLpan_Main.ColumnCount = 1
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Controls.Add(Me.Pan_Affichage, 0, 2)
        Me.TLpan_Main.Controls.Add(Me.TLPan_PartieBasse, 0, 3)
        Me.TLpan_Main.Controls.Add(Me.lbl_Hivoss, 0, 0)
        Me.TLpan_Main.Controls.Add(Me.TLpan_Modules, 0, 1)
        Me.TLpan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Main.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 4
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(700, 520)
        Me.TLpan_Main.TabIndex = 0
        '
        'Pan_Affichage
        '
        Me.Pan_Affichage.Controls.Add(Me.TLpan_AffichageCentral)
        Me.Pan_Affichage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_Affichage.Location = New System.Drawing.Point(0, 101)
        Me.Pan_Affichage.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.Pan_Affichage.Name = "Pan_Affichage"
        Me.Pan_Affichage.Size = New System.Drawing.Size(700, 379)
        Me.Pan_Affichage.TabIndex = 7
        '
        'TLpan_AffichageCentral
        '
        Me.TLpan_AffichageCentral.ColumnCount = 1
        Me.TLpan_AffichageCentral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 700.0!))
        Me.TLpan_AffichageCentral.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_AffichageCentral.Controls.Add(Me.pan_Image, 0, 0)
        Me.TLpan_AffichageCentral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_AffichageCentral.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_AffichageCentral.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_AffichageCentral.Name = "TLpan_AffichageCentral"
        Me.TLpan_AffichageCentral.RowCount = 1
        Me.TLpan_AffichageCentral.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_AffichageCentral.Size = New System.Drawing.Size(700, 379)
        Me.TLpan_AffichageCentral.TabIndex = 0
        '
        'pan_Image
        '
        Me.pan_Image.Controls.Add(Me.TLpan_HAffichage)
        Me.pan_Image.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Image.Location = New System.Drawing.Point(1, 1)
        Me.pan_Image.Margin = New System.Windows.Forms.Padding(1, 1, 1, 0)
        Me.pan_Image.Name = "pan_Image"
        Me.pan_Image.Size = New System.Drawing.Size(698, 378)
        Me.pan_Image.TabIndex = 3
        '
        'TLpan_HAffichage
        '
        Me.TLpan_HAffichage.ColumnCount = 1
        Me.TLpan_HAffichage.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_HAffichage.Controls.Add(Me.Panel1, 0, 0)
        Me.TLpan_HAffichage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_HAffichage.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_HAffichage.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_HAffichage.Name = "TLpan_HAffichage"
        Me.TLpan_HAffichage.RowCount = 1
        Me.TLpan_HAffichage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_HAffichage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 378.0!))
        Me.TLpan_HAffichage.Size = New System.Drawing.Size(698, 378)
        Me.TLpan_HAffichage.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.img_Hivoss)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(698, 378)
        Me.Panel1.TabIndex = 0
        '
        'img_Hivoss
        '
        Me.img_Hivoss.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Hivoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Hivoss.Location = New System.Drawing.Point(135, 115)
        Me.img_Hivoss.Margin = New System.Windows.Forms.Padding(0)
        Me.img_Hivoss.Name = "img_Hivoss"
        Me.img_Hivoss.Size = New System.Drawing.Size(100, 34)
        Me.img_Hivoss.TabIndex = 2
        Me.img_Hivoss.TabStop = False
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 483)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(694, 34)
        Me.TLPan_PartieBasse.TabIndex = 5
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(360, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(220, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'lbl_Hivoss
        '
        Me.lbl_Hivoss.AutoSize = True
        Me.lbl_Hivoss.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Hivoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Hivoss.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Hivoss.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Hivoss.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Hivoss.Name = "lbl_Hivoss"
        Me.lbl_Hivoss.Size = New System.Drawing.Size(700, 30)
        Me.lbl_Hivoss.TabIndex = 4
        Me.lbl_Hivoss.Text = "lbl_Hivoss"
        Me.lbl_Hivoss.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TLpan_Modules
        '
        Me.TLpan_Modules.ColumnCount = 1
        Me.TLpan_Modules.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Modules.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Modules.Controls.Add(Me.Pan_Results, 0, 0)
        Me.TLpan_Modules.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Modules.Location = New System.Drawing.Point(0, 30)
        Me.TLpan_Modules.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Modules.Name = "TLpan_Modules"
        Me.TLpan_Modules.RowCount = 1
        Me.TLpan_Modules.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Modules.Size = New System.Drawing.Size(700, 70)
        Me.TLpan_Modules.TabIndex = 6
        '
        'Pan_Results
        '
        Me.Pan_Results.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Pan_Results.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pan_Results.Controls.Add(Me.txt_Classe)
        Me.Pan_Results.Controls.Add(Me.lbl_Classe)
        Me.Pan_Results.Controls.Add(Me.etq_UnitOSRMS)
        Me.Pan_Results.Controls.Add(Me.txt_OSRMS)
        Me.Pan_Results.Controls.Add(Me.lbl_OSRMS)
        Me.Pan_Results.Controls.Add(Me.etq_UnitMass2)
        Me.Pan_Results.Controls.Add(Me.txt_MassModal)
        Me.Pan_Results.Controls.Add(Me.lbl_MassModal)
        Me.Pan_Results.Controls.Add(Me.etq_UnitAmortissement)
        Me.Pan_Results.Controls.Add(Me.txt_Amortissement)
        Me.Pan_Results.Controls.Add(Me.lbl_Amortissement)
        Me.Pan_Results.Controls.Add(Me.etq_UnitFreq)
        Me.Pan_Results.Controls.Add(Me.txt_Frequence)
        Me.Pan_Results.Controls.Add(Me.lbl_Frequence)
        Me.Pan_Results.Controls.Add(Me.lbl_Resultats)
        Me.Pan_Results.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_Results.Location = New System.Drawing.Point(1, 1)
        Me.Pan_Results.Margin = New System.Windows.Forms.Padding(1, 1, 1, 0)
        Me.Pan_Results.Name = "Pan_Results"
        Me.Pan_Results.Size = New System.Drawing.Size(698, 69)
        Me.Pan_Results.TabIndex = 6
        '
        'txt_Classe
        '
        Me.txt_Classe.ForeColor = System.Drawing.Color.DarkRed
        Me.txt_Classe.Location = New System.Drawing.Point(461, 24)
        Me.txt_Classe.Name = "txt_Classe"
        Me.txt_Classe.Size = New System.Drawing.Size(66, 20)
        Me.txt_Classe.TabIndex = 87
        Me.txt_Classe.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_Classe
        '
        Me.lbl_Classe.Location = New System.Drawing.Point(342, 27)
        Me.lbl_Classe.Name = "lbl_Classe"
        Me.lbl_Classe.Size = New System.Drawing.Size(113, 13)
        Me.lbl_Classe.TabIndex = 86
        Me.lbl_Classe.Text = "lbl_Classe"
        Me.lbl_Classe.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitOSRMS
        '
        Me.etq_UnitOSRMS.AutoSize = True
        Me.etq_UnitOSRMS.Location = New System.Drawing.Point(533, 7)
        Me.etq_UnitOSRMS.Name = "etq_UnitOSRMS"
        Me.etq_UnitOSRMS.Size = New System.Drawing.Size(25, 13)
        Me.etq_UnitOSRMS.TabIndex = 85
        Me.etq_UnitOSRMS.Text = "m/s"
        '
        'txt_OSRMS
        '
        Me.txt_OSRMS.ForeColor = System.Drawing.Color.DarkRed
        Me.txt_OSRMS.Location = New System.Drawing.Point(461, 3)
        Me.txt_OSRMS.Name = "txt_OSRMS"
        Me.txt_OSRMS.Size = New System.Drawing.Size(66, 20)
        Me.txt_OSRMS.TabIndex = 84
        Me.txt_OSRMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_OSRMS
        '
        Me.lbl_OSRMS.Location = New System.Drawing.Point(342, 6)
        Me.lbl_OSRMS.Name = "lbl_OSRMS"
        Me.lbl_OSRMS.Size = New System.Drawing.Size(113, 13)
        Me.lbl_OSRMS.TabIndex = 83
        Me.lbl_OSRMS.Text = "lbl_OSRMS"
        Me.lbl_OSRMS.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitMass2
        '
        Me.etq_UnitMass2.AutoSize = True
        Me.etq_UnitMass2.Location = New System.Drawing.Point(310, 28)
        Me.etq_UnitMass2.Name = "etq_UnitMass2"
        Me.etq_UnitMass2.Size = New System.Drawing.Size(19, 13)
        Me.etq_UnitMass2.TabIndex = 82
        Me.etq_UnitMass2.Text = "kg"
        '
        'txt_MassModal
        '
        Me.txt_MassModal.ForeColor = System.Drawing.Color.DarkRed
        Me.txt_MassModal.Location = New System.Drawing.Point(238, 24)
        Me.txt_MassModal.Name = "txt_MassModal"
        Me.txt_MassModal.Size = New System.Drawing.Size(66, 20)
        Me.txt_MassModal.TabIndex = 81
        Me.txt_MassModal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_MassModal
        '
        Me.lbl_MassModal.Location = New System.Drawing.Point(119, 27)
        Me.lbl_MassModal.Name = "lbl_MassModal"
        Me.lbl_MassModal.Size = New System.Drawing.Size(113, 13)
        Me.lbl_MassModal.TabIndex = 80
        Me.lbl_MassModal.Text = "lbl_MassModal"
        Me.lbl_MassModal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitAmortissement
        '
        Me.etq_UnitAmortissement.AutoSize = True
        Me.etq_UnitAmortissement.Location = New System.Drawing.Point(310, 49)
        Me.etq_UnitAmortissement.Name = "etq_UnitAmortissement"
        Me.etq_UnitAmortissement.Size = New System.Drawing.Size(15, 13)
        Me.etq_UnitAmortissement.TabIndex = 79
        Me.etq_UnitAmortissement.Text = "%"
        '
        'txt_Amortissement
        '
        Me.txt_Amortissement.ForeColor = System.Drawing.Color.DarkRed
        Me.txt_Amortissement.Location = New System.Drawing.Point(238, 45)
        Me.txt_Amortissement.Name = "txt_Amortissement"
        Me.txt_Amortissement.Size = New System.Drawing.Size(66, 20)
        Me.txt_Amortissement.TabIndex = 78
        Me.txt_Amortissement.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_Amortissement
        '
        Me.lbl_Amortissement.Location = New System.Drawing.Point(119, 48)
        Me.lbl_Amortissement.Name = "lbl_Amortissement"
        Me.lbl_Amortissement.Size = New System.Drawing.Size(113, 13)
        Me.lbl_Amortissement.TabIndex = 77
        Me.lbl_Amortissement.Text = "lbl_Amortissement"
        Me.lbl_Amortissement.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitFreq
        '
        Me.etq_UnitFreq.AutoSize = True
        Me.etq_UnitFreq.Location = New System.Drawing.Point(310, 7)
        Me.etq_UnitFreq.Name = "etq_UnitFreq"
        Me.etq_UnitFreq.Size = New System.Drawing.Size(20, 13)
        Me.etq_UnitFreq.TabIndex = 70
        Me.etq_UnitFreq.Text = "Hz"
        '
        'txt_Frequence
        '
        Me.txt_Frequence.ForeColor = System.Drawing.Color.DarkRed
        Me.txt_Frequence.Location = New System.Drawing.Point(238, 3)
        Me.txt_Frequence.Name = "txt_Frequence"
        Me.txt_Frequence.Size = New System.Drawing.Size(66, 20)
        Me.txt_Frequence.TabIndex = 69
        Me.txt_Frequence.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_Frequence
        '
        Me.lbl_Frequence.Location = New System.Drawing.Point(119, 6)
        Me.lbl_Frequence.Name = "lbl_Frequence"
        Me.lbl_Frequence.Size = New System.Drawing.Size(113, 13)
        Me.lbl_Frequence.TabIndex = 68
        Me.lbl_Frequence.Text = "lbl_Frequence"
        Me.lbl_Frequence.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_Resultats
        '
        Me.lbl_Resultats.AutoSize = True
        Me.lbl_Resultats.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Resultats.Location = New System.Drawing.Point(18, 6)
        Me.lbl_Resultats.Name = "lbl_Resultats"
        Me.lbl_Resultats.Size = New System.Drawing.Size(67, 13)
        Me.lbl_Resultats.TabIndex = 60
        Me.lbl_Resultats.Text = "lbl_Resultats"
        '
        'Frm_PPHivoss
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(700, 520)
        Me.Controls.Add(Me.pan_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_PPHivoss"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_PPHivoss"
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLpan_Main.PerformLayout()
        Me.Pan_Affichage.ResumeLayout(False)
        Me.TLpan_AffichageCentral.ResumeLayout(False)
        Me.pan_Image.ResumeLayout(False)
        Me.TLpan_HAffichage.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.img_Hivoss, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.TLpan_Modules.ResumeLayout(False)
        Me.Pan_Results.ResumeLayout(False)
        Me.Pan_Results.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents Pan_Affichage As Panel
    Friend WithEvents TLpan_AffichageCentral As TableLayoutPanel
    Friend WithEvents pan_Image As Panel
    Friend WithEvents TLpan_HAffichage As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents img_Hivoss As PictureBox
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents lbl_Hivoss As Label
    Friend WithEvents TLpan_Modules As TableLayoutPanel
    Friend WithEvents Pan_Results As Panel
    Friend WithEvents txt_Classe As TextBox
    Friend WithEvents lbl_Classe As Label
    Friend WithEvents etq_UnitOSRMS As Label
    Friend WithEvents txt_OSRMS As TextBox
    Friend WithEvents lbl_OSRMS As Label
    Friend WithEvents etq_UnitMass2 As Label
    Friend WithEvents txt_MassModal As TextBox
    Friend WithEvents lbl_MassModal As Label
    Friend WithEvents etq_UnitAmortissement As Label
    Friend WithEvents txt_Amortissement As TextBox
    Friend WithEvents lbl_Amortissement As Label
    Friend WithEvents etq_UnitFreq As Label
    Friend WithEvents txt_Frequence As TextBox
    Friend WithEvents lbl_Frequence As Label
    Friend WithEvents lbl_Resultats As Label
End Class
