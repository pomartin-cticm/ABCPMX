<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ErreursMessages
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
        Me.TLpan_Affichage = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Lien = New System.Windows.Forms.Panel()
        Me.lbl_ContactSupport = New System.Windows.Forms.Label()
        Me.lbk_Support = New System.Windows.Forms.LinkLabel()
        Me.pan_Affichage = New System.Windows.Forms.Panel()
        Me.pan_Message = New System.Windows.Forms.Panel()
        Me.rtxt_Message = New System.Windows.Forms.RichTextBox()
        Me.pan_Source = New System.Windows.Forms.Panel()
        Me.lbl_Source = New System.Windows.Forms.Label()
        Me.lbl_General = New System.Windows.Forms.Label()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Affichage.SuspendLayout()
        Me.pan_Lien.SuspendLayout()
        Me.pan_Affichage.SuspendLayout()
        Me.pan_Message.SuspendLayout()
        Me.pan_Source.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(705, 372)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(705, 372)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 3
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 1, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(4, 327)
        Me.TLPan_PartieBasse.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(697, 41)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(272, 4)
        Me.btn_OK.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(152, 33)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLpan_Affichage)
        Me.pan_Main.Location = New System.Drawing.Point(4, 4)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(679, 249)
        Me.pan_Main.TabIndex = 1
        '
        'TLpan_Affichage
        '
        Me.TLpan_Affichage.ColumnCount = 1
        Me.TLpan_Affichage.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Affichage.Controls.Add(Me.pan_Lien, 0, 2)
        Me.TLpan_Affichage.Controls.Add(Me.pan_Affichage, 0, 1)
        Me.TLpan_Affichage.Controls.Add(Me.lbl_General, 0, 0)
        Me.TLpan_Affichage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Affichage.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Affichage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TLpan_Affichage.Name = "TLpan_Affichage"
        Me.TLpan_Affichage.RowCount = 3
        Me.TLpan_Affichage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLpan_Affichage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Affichage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 74.0!))
        Me.TLpan_Affichage.Size = New System.Drawing.Size(679, 249)
        Me.TLpan_Affichage.TabIndex = 1
        '
        'pan_Lien
        '
        Me.pan_Lien.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Lien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Lien.Controls.Add(Me.lbl_ContactSupport)
        Me.pan_Lien.Controls.Add(Me.lbk_Support)
        Me.pan_Lien.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Lien.Location = New System.Drawing.Point(0, 176)
        Me.pan_Lien.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.pan_Lien.Name = "pan_Lien"
        Me.pan_Lien.Size = New System.Drawing.Size(679, 73)
        Me.pan_Lien.TabIndex = 2
        '
        'lbl_ContactSupport
        '
        Me.lbl_ContactSupport.AutoSize = True
        Me.lbl_ContactSupport.Location = New System.Drawing.Point(11, 28)
        Me.lbl_ContactSupport.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_ContactSupport.Name = "lbl_ContactSupport"
        Me.lbl_ContactSupport.Size = New System.Drawing.Size(120, 16)
        Me.lbl_ContactSupport.TabIndex = 18
        Me.lbl_ContactSupport.Text = "lbl_ContactSupport"
        '
        'lbk_Support
        '
        Me.lbk_Support.AutoSize = True
        Me.lbk_Support.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbk_Support.Location = New System.Drawing.Point(231, 28)
        Me.lbk_Support.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbk_Support.Name = "lbk_Support"
        Me.lbk_Support.Size = New System.Drawing.Size(144, 14)
        Me.lbk_Support.TabIndex = 17
        Me.lbk_Support.TabStop = True
        Me.lbk_Support.Text = "support.logiciels@cticm.com"
        '
        'pan_Affichage
        '
        Me.pan_Affichage.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Affichage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Affichage.Controls.Add(Me.pan_Message)
        Me.pan_Affichage.Controls.Add(Me.pan_Source)
        Me.pan_Affichage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Affichage.Location = New System.Drawing.Point(0, 37)
        Me.pan_Affichage.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Affichage.Name = "pan_Affichage"
        Me.pan_Affichage.Size = New System.Drawing.Size(679, 138)
        Me.pan_Affichage.TabIndex = 1
        '
        'pan_Message
        '
        Me.pan_Message.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Message.Controls.Add(Me.rtxt_Message)
        Me.pan_Message.Location = New System.Drawing.Point(3, 60)
        Me.pan_Message.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_Message.Name = "pan_Message"
        Me.pan_Message.Size = New System.Drawing.Size(670, 72)
        Me.pan_Message.TabIndex = 1
        '
        'rtxt_Message
        '
        Me.rtxt_Message.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtxt_Message.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtxt_Message.Location = New System.Drawing.Point(4, 4)
        Me.rtxt_Message.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rtxt_Message.Name = "rtxt_Message"
        Me.rtxt_Message.Size = New System.Drawing.Size(662, 65)
        Me.rtxt_Message.TabIndex = 0
        Me.rtxt_Message.Text = ""
        '
        'pan_Source
        '
        Me.pan_Source.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Source.Controls.Add(Me.lbl_Source)
        Me.pan_Source.Location = New System.Drawing.Point(3, 4)
        Me.pan_Source.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_Source.Name = "pan_Source"
        Me.pan_Source.Size = New System.Drawing.Size(670, 53)
        Me.pan_Source.TabIndex = 0
        '
        'lbl_Source
        '
        Me.lbl_Source.AutoSize = True
        Me.lbl_Source.Location = New System.Drawing.Point(8, 18)
        Me.lbl_Source.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Source.Name = "lbl_Source"
        Me.lbl_Source.Size = New System.Drawing.Size(71, 16)
        Me.lbl_Source.TabIndex = 0
        Me.lbl_Source.Text = "lbl_Source"
        '
        'lbl_General
        '
        Me.lbl_General.AutoSize = True
        Me.lbl_General.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_General.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_General.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_General.Location = New System.Drawing.Point(0, 0)
        Me.lbl_General.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_General.Name = "lbl_General"
        Me.lbl_General.Size = New System.Drawing.Size(679, 37)
        Me.lbl_General.TabIndex = 0
        Me.lbl_General.Text = "lbl_General"
        Me.lbl_General.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_ErreursMessages
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(705, 372)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_ErreursMessages"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Frm_ErreursMessages"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Affichage.ResumeLayout(False)
        Me.TLpan_Affichage.PerformLayout()
        Me.pan_Lien.ResumeLayout(False)
        Me.pan_Lien.PerformLayout()
        Me.pan_Affichage.ResumeLayout(False)
        Me.pan_Message.ResumeLayout(False)
        Me.pan_Source.ResumeLayout(False)
        Me.pan_Source.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents lbl_General As Label
    Friend WithEvents pan_Affichage As Panel
    Friend WithEvents TLpan_Affichage As TableLayoutPanel
    Friend WithEvents pan_Lien As Panel
    Friend WithEvents lbk_Support As LinkLabel
    Friend WithEvents lbl_ContactSupport As Label
    Friend WithEvents pan_Source As Panel
    Friend WithEvents pan_Message As Panel
    Friend WithEvents rtxt_Message As RichTextBox
    Friend WithEvents lbl_Source As Label
End Class
