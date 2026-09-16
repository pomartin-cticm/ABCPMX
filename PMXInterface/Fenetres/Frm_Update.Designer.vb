<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Update
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
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Portees = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Fichier = New System.Windows.Forms.Panel()
        Me.MyProgressB = New System.Windows.Forms.ProgressBar()
        Me.etq_MessageSteels = New System.Windows.Forms.Label()
        Me.etq_Avertissement = New System.Windows.Forms.Label()
        Me.etq_MessageSections = New System.Windows.Forms.Label()
        Me.cmd_Telecharger = New System.Windows.Forms.Button()
        Me.lbl_Fichier = New System.Windows.Forms.Label()
        Me.lbl_Version = New System.Windows.Forms.Label()
        Me.pan_Version = New System.Windows.Forms.Panel()
        Me.img_WarningTest = New System.Windows.Forms.PictureBox()
        Me.lkl_Update = New System.Windows.Forms.LinkLabel()
        Me.etq_Message02 = New System.Windows.Forms.Label()
        Me.etq_Message01 = New System.Windows.Forms.Label()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Portees.SuspendLayout()
        Me.pan_Fichier.SuspendLayout()
        Me.pan_Version.SuspendLayout()
        CType(Me.img_WarningTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(567, 456)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(567, 456)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 3
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 1, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 419)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(561, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(223, 3)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(114, 28)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_Portees)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(561, 410)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Portees
        '
        Me.TLPan_Portees.ColumnCount = 1
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Controls.Add(Me.pan_Fichier, 0, 3)
        Me.TLPan_Portees.Controls.Add(Me.lbl_Fichier, 0, 2)
        Me.TLPan_Portees.Controls.Add(Me.lbl_Version, 0, 0)
        Me.TLPan_Portees.Controls.Add(Me.pan_Version, 0, 1)
        Me.TLPan_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Portees.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Portees.Name = "TLPan_Portees"
        Me.TLPan_Portees.RowCount = 4
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Portees.Size = New System.Drawing.Size(561, 410)
        Me.TLPan_Portees.TabIndex = 0
        '
        'pan_Fichier
        '
        Me.pan_Fichier.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Fichier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Fichier.Controls.Add(Me.MyProgressB)
        Me.pan_Fichier.Controls.Add(Me.etq_MessageSteels)
        Me.pan_Fichier.Controls.Add(Me.etq_Avertissement)
        Me.pan_Fichier.Controls.Add(Me.etq_MessageSections)
        Me.pan_Fichier.Controls.Add(Me.cmd_Telecharger)
        Me.pan_Fichier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Fichier.Location = New System.Drawing.Point(0, 235)
        Me.pan_Fichier.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Fichier.Name = "pan_Fichier"
        Me.pan_Fichier.Size = New System.Drawing.Size(561, 175)
        Me.pan_Fichier.TabIndex = 3
        '
        'MyProgressB
        '
        Me.MyProgressB.Location = New System.Drawing.Point(212, 94)
        Me.MyProgressB.Name = "MyProgressB"
        Me.MyProgressB.Size = New System.Drawing.Size(212, 20)
        Me.MyProgressB.TabIndex = 15
        '
        'etq_MessageSteels
        '
        Me.etq_MessageSteels.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_MessageSteels.Location = New System.Drawing.Point(9, 35)
        Me.etq_MessageSteels.Name = "etq_MessageSteels"
        Me.etq_MessageSteels.Size = New System.Drawing.Size(542, 15)
        Me.etq_MessageSteels.TabIndex = 14
        Me.etq_MessageSteels.Text = "etq_MessageSteels"
        '
        'etq_Avertissement
        '
        Me.etq_Avertissement.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_Avertissement.Location = New System.Drawing.Point(9, 97)
        Me.etq_Avertissement.Name = "etq_Avertissement"
        Me.etq_Avertissement.Size = New System.Drawing.Size(500, 15)
        Me.etq_Avertissement.TabIndex = 13
        Me.etq_Avertissement.Text = "etq_Avertissement"
        '
        'etq_MessageSections
        '
        Me.etq_MessageSections.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_MessageSections.Location = New System.Drawing.Point(9, 12)
        Me.etq_MessageSections.Name = "etq_MessageSections"
        Me.etq_MessageSections.Size = New System.Drawing.Size(545, 15)
        Me.etq_MessageSections.TabIndex = 12
        Me.etq_MessageSections.Text = "etq_MessageSections"
        '
        'cmd_Telecharger
        '
        Me.cmd_Telecharger.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_Telecharger.Location = New System.Drawing.Point(6, 57)
        Me.cmd_Telecharger.Name = "cmd_Telecharger"
        Me.cmd_Telecharger.Size = New System.Drawing.Size(545, 24)
        Me.cmd_Telecharger.TabIndex = 11
        Me.cmd_Telecharger.Text = "cmd_Telecharger"
        Me.cmd_Telecharger.UseVisualStyleBackColor = True
        '
        'lbl_Fichier
        '
        Me.lbl_Fichier.AutoSize = True
        Me.lbl_Fichier.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Fichier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Fichier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Fichier.Location = New System.Drawing.Point(0, 205)
        Me.lbl_Fichier.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Fichier.Name = "lbl_Fichier"
        Me.lbl_Fichier.Size = New System.Drawing.Size(561, 30)
        Me.lbl_Fichier.TabIndex = 2
        Me.lbl_Fichier.Text = "lbl_Fichier"
        Me.lbl_Fichier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Version
        '
        Me.lbl_Version.AutoSize = True
        Me.lbl_Version.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Version.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Version.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Version.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Version.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Version.Name = "lbl_Version"
        Me.lbl_Version.Size = New System.Drawing.Size(561, 30)
        Me.lbl_Version.TabIndex = 1
        Me.lbl_Version.Text = "lbl_Version"
        Me.lbl_Version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Version
        '
        Me.pan_Version.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Version.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Version.Controls.Add(Me.img_WarningTest)
        Me.pan_Version.Controls.Add(Me.lkl_Update)
        Me.pan_Version.Controls.Add(Me.etq_Message02)
        Me.pan_Version.Controls.Add(Me.etq_Message01)
        Me.pan_Version.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Version.Location = New System.Drawing.Point(0, 30)
        Me.pan_Version.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Version.Name = "pan_Version"
        Me.pan_Version.Size = New System.Drawing.Size(561, 174)
        Me.pan_Version.TabIndex = 0
        '
        'img_WarningTest
        '
        Me.img_WarningTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_WarningTest.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.img_WarningTest.Location = New System.Drawing.Point(532, 140)
        Me.img_WarningTest.Margin = New System.Windows.Forms.Padding(0)
        Me.img_WarningTest.Name = "img_WarningTest"
        Me.img_WarningTest.Size = New System.Drawing.Size(22, 22)
        Me.img_WarningTest.TabIndex = 87
        Me.img_WarningTest.TabStop = False
        '
        'lkl_Update
        '
        Me.lkl_Update.AutoSize = True
        Me.lkl_Update.Location = New System.Drawing.Point(35, 72)
        Me.lkl_Update.Name = "lkl_Update"
        Me.lkl_Update.Size = New System.Drawing.Size(66, 15)
        Me.lkl_Update.TabIndex = 5
        Me.lkl_Update.TabStop = True
        Me.lkl_Update.Text = "lkl_Update"
        '
        'etq_Message02
        '
        Me.etq_Message02.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_Message02.Location = New System.Drawing.Point(9, 40)
        Me.etq_Message02.Name = "etq_Message02"
        Me.etq_Message02.Size = New System.Drawing.Size(542, 15)
        Me.etq_Message02.TabIndex = 4
        Me.etq_Message02.Text = "etq_Message02"
        '
        'etq_Message01
        '
        Me.etq_Message01.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_Message01.Location = New System.Drawing.Point(8, 13)
        Me.etq_Message01.Name = "etq_Message01"
        Me.etq_Message01.Size = New System.Drawing.Size(543, 15)
        Me.etq_Message01.TabIndex = 3
        Me.etq_Message01.Text = "etq_Message01"
        '
        'Frm_Update
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(567, 456)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Update"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Frm_Update"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Portees.ResumeLayout(False)
        Me.TLPan_Portees.PerformLayout()
        Me.pan_Fichier.ResumeLayout(False)
        Me.pan_Version.ResumeLayout(False)
        Me.pan_Version.PerformLayout()
        CType(Me.img_WarningTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Portees As TableLayoutPanel
    Friend WithEvents pan_Version As Panel
    Friend WithEvents pan_Fichier As Panel
    Friend WithEvents lbl_Fichier As Label
    Friend WithEvents lbl_Version As Label
    Friend WithEvents MyProgressB As ProgressBar
    Friend WithEvents etq_MessageSteels As Label
    Friend WithEvents etq_Avertissement As Label
    Friend WithEvents etq_MessageSections As Label
    Friend WithEvents cmd_Telecharger As Button
    Friend WithEvents lkl_Update As LinkLabel
    Friend WithEvents etq_Message02 As Label
    Friend WithEvents etq_Message01 As Label
    Friend WithEvents img_WarningTest As PictureBox
End Class
