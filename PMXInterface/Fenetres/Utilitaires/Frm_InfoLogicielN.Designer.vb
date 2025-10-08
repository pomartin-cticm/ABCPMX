<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_InfoLogicielN
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
        Me.pan_G2 = New System.Windows.Forms.Panel()
        Me.TLPan_General = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Lien = New System.Windows.Forms.Panel()
        Me.lbl_ContactSupport = New System.Windows.Forms.Label()
        Me.lbk_Support = New System.Windows.Forms.LinkLabel()
        Me.pan_Info = New System.Windows.Forms.Panel()
        Me.rtb_Info = New System.Windows.Forms.RichTextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_InfoW = New System.Windows.Forms.Label()
        Me.pan_General.SuspendLayout()
        Me.pan_G2.SuspendLayout()
        Me.TLPan_General.SuspendLayout()
        Me.pan_Lien.SuspendLayout()
        Me.pan_Info.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_General.Controls.Add(Me.pan_G2)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Padding = New System.Windows.Forms.Padding(4)
        Me.pan_General.Size = New System.Drawing.Size(618, 350)
        Me.pan_General.TabIndex = 1
        '
        'pan_G2
        '
        Me.pan_G2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_G2.Controls.Add(Me.TLPan_General)
        Me.pan_G2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_G2.Location = New System.Drawing.Point(4, 4)
        Me.pan_G2.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_G2.Name = "pan_G2"
        Me.pan_G2.Size = New System.Drawing.Size(610, 342)
        Me.pan_G2.TabIndex = 0
        '
        'TLPan_General
        '
        Me.TLPan_General.ColumnCount = 1
        Me.TLPan_General.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_General.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_General.Controls.Add(Me.pan_Lien, 0, 2)
        Me.TLPan_General.Controls.Add(Me.pan_Info, 0, 1)
        Me.TLPan_General.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.TLPan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_General.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_General.Name = "TLPan_General"
        Me.TLPan_General.RowCount = 3
        Me.TLPan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLPan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLPan_General.Size = New System.Drawing.Size(610, 342)
        Me.TLPan_General.TabIndex = 0
        '
        'pan_Lien
        '
        Me.pan_Lien.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Lien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Lien.Controls.Add(Me.lbl_ContactSupport)
        Me.pan_Lien.Controls.Add(Me.lbk_Support)
        Me.pan_Lien.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Lien.Location = New System.Drawing.Point(0, 305)
        Me.pan_Lien.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Lien.Name = "pan_Lien"
        Me.pan_Lien.Size = New System.Drawing.Size(610, 37)
        Me.pan_Lien.TabIndex = 84
        '
        'lbl_ContactSupport
        '
        Me.lbl_ContactSupport.AutoSize = True
        Me.lbl_ContactSupport.Location = New System.Drawing.Point(11, 10)
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
        Me.lbk_Support.Location = New System.Drawing.Point(231, 9)
        Me.lbk_Support.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbk_Support.Name = "lbk_Support"
        Me.lbk_Support.Size = New System.Drawing.Size(144, 14)
        Me.lbk_Support.TabIndex = 17
        Me.lbk_Support.TabStop = True
        Me.lbk_Support.Text = "support.logiciels@cticm.com"
        '
        'pan_Info
        '
        Me.pan_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Info.Controls.Add(Me.rtb_Info)
        Me.pan_Info.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Info.Location = New System.Drawing.Point(0, 38)
        Me.pan_Info.Margin = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.pan_Info.Name = "pan_Info"
        Me.pan_Info.Size = New System.Drawing.Size(610, 266)
        Me.pan_Info.TabIndex = 82
        '
        'rtb_Info
        '
        Me.rtb_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtb_Info.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb_Info.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtb_Info.Location = New System.Drawing.Point(0, 0)
        Me.rtb_Info.Margin = New System.Windows.Forms.Padding(4)
        Me.rtb_Info.Name = "rtb_Info"
        Me.rtb_Info.ReadOnly = True
        Me.rtb_Info.Size = New System.Drawing.Size(608, 264)
        Me.rtb_Info.TabIndex = 0
        Me.rtb_Info.Text = ""
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_InfoW, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(610, 37)
        Me.TableLayoutPanel1.TabIndex = 83
        '
        'lbl_InfoW
        '
        Me.lbl_InfoW.AutoSize = True
        Me.lbl_InfoW.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_InfoW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_InfoW.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_InfoW.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_InfoW.Location = New System.Drawing.Point(0, 0)
        Me.lbl_InfoW.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_InfoW.Name = "lbl_InfoW"
        Me.lbl_InfoW.Size = New System.Drawing.Size(610, 37)
        Me.lbl_InfoW.TabIndex = 3
        Me.lbl_InfoW.Text = "lbl_InfoW"
        Me.lbl_InfoW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_InfoLogicielN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(618, 350)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_InfoLogicielN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Frm_InfoLogicielN"
        Me.pan_General.ResumeLayout(False)
        Me.pan_G2.ResumeLayout(False)
        Me.TLPan_General.ResumeLayout(False)
        Me.pan_Lien.ResumeLayout(False)
        Me.pan_Lien.PerformLayout()
        Me.pan_Info.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents pan_G2 As Panel
    Friend WithEvents TLPan_General As TableLayoutPanel
    Friend WithEvents pan_Lien As Panel
    Friend WithEvents lbl_ContactSupport As Label
    Friend WithEvents lbk_Support As LinkLabel
    Friend WithEvents pan_Info As Panel
    Friend WithEvents rtb_Info As RichTextBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lbl_InfoW As Label
End Class
