<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Portees
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
        Me.TLPan_Portees = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Portees = New System.Windows.Forms.Label()
        Me.pan_SaisiePortee = New System.Windows.Forms.Panel()
        Me.img_Portees = New System.Windows.Forms.PictureBox()
        Me.lbl_MainSpan = New System.Windows.Forms.Label()
        Me.etq_UnitL1 = New System.Windows.Forms.Label()
        Me.txt_MainSpan = New System.Windows.Forms.TextBox()
        Me.img_L1 = New System.Windows.Forms.PictureBox()
        Me.chk_ConsoleGauche = New System.Windows.Forms.CheckBox()
        Me.etq_UnitL2 = New System.Windows.Forms.Label()
        Me.txt_PorteeConsoleG = New System.Windows.Forms.TextBox()
        Me.img_L2 = New System.Windows.Forms.PictureBox()
        Me.etq_UnitL3 = New System.Windows.Forms.Label()
        Me.txt_PorteeConsoleD = New System.Windows.Forms.TextBox()
        Me.img_L3 = New System.Windows.Forms.PictureBox()
        Me.chk_ConsoleDroite = New System.Windows.Forms.CheckBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Portees.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_SaisiePortee.SuspendLayout()
        CType(Me.img_Portees, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_L1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_L2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_L3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(954, 540)
        Me.pan_General.TabIndex = 1
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
        Me.TLpan_Main.Size = New System.Drawing.Size(954, 540)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 503)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(948, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(487, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(347, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Main.Controls.Add(Me.TLPan_Portees)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(948, 494)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Portees
        '
        Me.TLPan_Portees.ColumnCount = 2
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Portees.Controls.Add(Me.img_Portees, 1, 0)
        Me.TLPan_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Portees.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Portees.Name = "TLPan_Portees"
        Me.TLPan_Portees.RowCount = 1
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Size = New System.Drawing.Size(946, 492)
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
        Me.pan_Gauche.Size = New System.Drawing.Size(250, 492)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Portees, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisiePortee, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 3
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(250, 203)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Portees
        '
        Me.lbl_Portees.AutoSize = True
        Me.lbl_Portees.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Portees.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Portees.Name = "lbl_Portees"
        Me.lbl_Portees.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Portees.TabIndex = 0
        Me.lbl_Portees.Text = "lbl_Portee"
        Me.lbl_Portees.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisiePortee
        '
        Me.pan_SaisiePortee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisiePortee.Controls.Add(Me.etq_UnitL3)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_PorteeConsoleD)
        Me.pan_SaisiePortee.Controls.Add(Me.img_L3)
        Me.pan_SaisiePortee.Controls.Add(Me.chk_ConsoleDroite)
        Me.pan_SaisiePortee.Controls.Add(Me.etq_UnitL2)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_PorteeConsoleG)
        Me.pan_SaisiePortee.Controls.Add(Me.img_L2)
        Me.pan_SaisiePortee.Controls.Add(Me.chk_ConsoleGauche)
        Me.pan_SaisiePortee.Controls.Add(Me.etq_UnitL1)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_MainSpan)
        Me.pan_SaisiePortee.Controls.Add(Me.img_L1)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_MainSpan)
        Me.pan_SaisiePortee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisiePortee.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisiePortee.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisiePortee.Name = "pan_SaisiePortee"
        Me.pan_SaisiePortee.Size = New System.Drawing.Size(250, 170)
        Me.pan_SaisiePortee.TabIndex = 1
        '
        'img_Portees
        '
        Me.img_Portees.Location = New System.Drawing.Point(251, 0)
        Me.img_Portees.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.img_Portees.Name = "img_Portees"
        Me.img_Portees.Size = New System.Drawing.Size(100, 50)
        Me.img_Portees.TabIndex = 1
        Me.img_Portees.TabStop = False
        '
        'lbl_MainSpan
        '
        Me.lbl_MainSpan.AutoSize = True
        Me.lbl_MainSpan.Location = New System.Drawing.Point(7, 6)
        Me.lbl_MainSpan.Name = "lbl_MainSpan"
        Me.lbl_MainSpan.Size = New System.Drawing.Size(71, 13)
        Me.lbl_MainSpan.TabIndex = 0
        Me.lbl_MainSpan.Text = "lbl_MainSpan"
        '
        'etq_UnitL1
        '
        Me.etq_UnitL1.AutoSize = True
        Me.etq_UnitL1.Location = New System.Drawing.Point(200, 29)
        Me.etq_UnitL1.Name = "etq_UnitL1"
        Me.etq_UnitL1.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL1.TabIndex = 72
        Me.etq_UnitL1.Text = "kN"
        '
        'txt_MainSpan
        '
        Me.txt_MainSpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_MainSpan.Location = New System.Drawing.Point(136, 25)
        Me.txt_MainSpan.Name = "txt_MainSpan"
        Me.txt_MainSpan.Size = New System.Drawing.Size(58, 20)
        Me.txt_MainSpan.TabIndex = 70
        '
        'img_L1
        '
        Me.img_L1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_L1.Location = New System.Drawing.Point(91, 25)
        Me.img_L1.Name = "img_L1"
        Me.img_L1.Size = New System.Drawing.Size(46, 20)
        Me.img_L1.TabIndex = 71
        Me.img_L1.TabStop = False
        '
        'chk_ConsoleGauche
        '
        Me.chk_ConsoleGauche.AutoSize = True
        Me.chk_ConsoleGauche.Location = New System.Drawing.Point(7, 54)
        Me.chk_ConsoleGauche.Name = "chk_ConsoleGauche"
        Me.chk_ConsoleGauche.Size = New System.Drawing.Size(126, 17)
        Me.chk_ConsoleGauche.TabIndex = 73
        Me.chk_ConsoleGauche.Text = "chk_ConsoleGauche"
        Me.chk_ConsoleGauche.UseVisualStyleBackColor = True
        '
        'etq_UnitL2
        '
        Me.etq_UnitL2.AutoSize = True
        Me.etq_UnitL2.Location = New System.Drawing.Point(200, 81)
        Me.etq_UnitL2.Name = "etq_UnitL2"
        Me.etq_UnitL2.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL2.TabIndex = 76
        Me.etq_UnitL2.Text = "kN"
        '
        'txt_PorteeConsoleG
        '
        Me.txt_PorteeConsoleG.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PorteeConsoleG.Location = New System.Drawing.Point(136, 77)
        Me.txt_PorteeConsoleG.Name = "txt_PorteeConsoleG"
        Me.txt_PorteeConsoleG.Size = New System.Drawing.Size(58, 20)
        Me.txt_PorteeConsoleG.TabIndex = 74
        '
        'img_L2
        '
        Me.img_L2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_L2.Location = New System.Drawing.Point(91, 77)
        Me.img_L2.Name = "img_L2"
        Me.img_L2.Size = New System.Drawing.Size(46, 20)
        Me.img_L2.TabIndex = 75
        Me.img_L2.TabStop = False
        '
        'etq_UnitL3
        '
        Me.etq_UnitL3.AutoSize = True
        Me.etq_UnitL3.Location = New System.Drawing.Point(200, 137)
        Me.etq_UnitL3.Name = "etq_UnitL3"
        Me.etq_UnitL3.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL3.TabIndex = 80
        Me.etq_UnitL3.Text = "kN"
        '
        'txt_PorteeConsoleD
        '
        Me.txt_PorteeConsoleD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PorteeConsoleD.Location = New System.Drawing.Point(136, 133)
        Me.txt_PorteeConsoleD.Name = "txt_PorteeConsoleD"
        Me.txt_PorteeConsoleD.Size = New System.Drawing.Size(58, 20)
        Me.txt_PorteeConsoleD.TabIndex = 78
        '
        'img_L3
        '
        Me.img_L3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_L3.Location = New System.Drawing.Point(91, 133)
        Me.img_L3.Name = "img_L3"
        Me.img_L3.Size = New System.Drawing.Size(46, 20)
        Me.img_L3.TabIndex = 79
        Me.img_L3.TabStop = False
        '
        'chk_ConsoleDroite
        '
        Me.chk_ConsoleDroite.AutoSize = True
        Me.chk_ConsoleDroite.Location = New System.Drawing.Point(7, 110)
        Me.chk_ConsoleDroite.Name = "chk_ConsoleDroite"
        Me.chk_ConsoleDroite.Size = New System.Drawing.Size(116, 17)
        Me.chk_ConsoleDroite.TabIndex = 77
        Me.chk_ConsoleDroite.Text = "chk_ConsoleDroite"
        Me.chk_ConsoleDroite.UseVisualStyleBackColor = True
        '
        'Frm_Portees
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(954, 540)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_Portees"
        Me.Text = "Frm_Portees"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Portees.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_SaisiePortee.ResumeLayout(False)
        Me.pan_SaisiePortee.PerformLayout()
        CType(Me.img_Portees, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_L1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_L2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_L3, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lbl_Portees As Label
    Friend WithEvents pan_SaisiePortee As Panel
    Friend WithEvents img_Portees As PictureBox
    Friend WithEvents etq_UnitL3 As Label
    Friend WithEvents txt_PorteeConsoleD As TextBox
    Friend WithEvents img_L3 As PictureBox
    Friend WithEvents chk_ConsoleDroite As CheckBox
    Friend WithEvents etq_UnitL2 As Label
    Friend WithEvents txt_PorteeConsoleG As TextBox
    Friend WithEvents img_L2 As PictureBox
    Friend WithEvents chk_ConsoleGauche As CheckBox
    Friend WithEvents etq_UnitL1 As Label
    Friend WithEvents txt_MainSpan As TextBox
    Friend WithEvents img_L1 As PictureBox
    Friend WithEvents lbl_MainSpan As Label
End Class
