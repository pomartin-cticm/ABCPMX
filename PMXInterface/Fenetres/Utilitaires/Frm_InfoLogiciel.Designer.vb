<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_InfoLogiciel
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_InfoLogiciel))
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.pan_G2 = New System.Windows.Forms.Panel()
        Me.TLPan_General = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Lien = New System.Windows.Forms.Panel()
        Me.lbl_ContactSupport = New System.Windows.Forms.Label()
        Me.lbk_Support = New System.Windows.Forms.LinkLabel()
        Me.img_info = New System.Windows.Forms.PictureBox()
        Me.pan_Info = New System.Windows.Forms.Panel()
        Me.rtb_Info = New System.Windows.Forms.RichTextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_InfoW = New System.Windows.Forms.Label()
        Me.img_Close = New System.Windows.Forms.PictureBox()
        Me.imgList_Info = New System.Windows.Forms.ImageList(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pan_General.SuspendLayout()
        Me.pan_G2.SuspendLayout()
        Me.TLPan_General.SuspendLayout()
        Me.pan_Lien.SuspendLayout()
        CType(Me.img_info, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Info.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.img_Close, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.BackColor = System.Drawing.Color.OrangeRed
        Me.pan_General.Controls.Add(Me.pan_G2)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Padding = New System.Windows.Forms.Padding(1)
        Me.pan_General.Size = New System.Drawing.Size(395, 183)
        Me.pan_General.TabIndex = 0
        '
        'pan_G2
        '
        Me.pan_G2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_G2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_G2.Controls.Add(Me.TLPan_General)
        Me.pan_G2.Location = New System.Drawing.Point(2, 2)
        Me.pan_G2.Name = "pan_G2"
        Me.pan_G2.Size = New System.Drawing.Size(391, 179)
        Me.pan_G2.TabIndex = 0
        '
        'TLPan_General
        '
        Me.TLPan_General.ColumnCount = 2
        Me.TLPan_General.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_General.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_General.Controls.Add(Me.pan_Lien, 1, 2)
        Me.TLPan_General.Controls.Add(Me.img_info, 0, 0)
        Me.TLPan_General.Controls.Add(Me.pan_Info, 1, 1)
        Me.TLPan_General.Controls.Add(Me.TableLayoutPanel1, 1, 0)
        Me.TLPan_General.Controls.Add(Me.Label1, 0, 2)
        Me.TLPan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_General.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_General.Name = "TLPan_General"
        Me.TLPan_General.RowCount = 3
        Me.TLPan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_General.Size = New System.Drawing.Size(391, 179)
        Me.TLPan_General.TabIndex = 0
        '
        'pan_Lien
        '
        Me.pan_Lien.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Lien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Lien.Controls.Add(Me.lbl_ContactSupport)
        Me.pan_Lien.Controls.Add(Me.lbk_Support)
        Me.pan_Lien.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Lien.Location = New System.Drawing.Point(31, 149)
        Me.pan_Lien.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Lien.Name = "pan_Lien"
        Me.pan_Lien.Size = New System.Drawing.Size(360, 30)
        Me.pan_Lien.TabIndex = 84
        '
        'lbl_ContactSupport
        '
        Me.lbl_ContactSupport.AutoSize = True
        Me.lbl_ContactSupport.Location = New System.Drawing.Point(8, 8)
        Me.lbl_ContactSupport.Name = "lbl_ContactSupport"
        Me.lbl_ContactSupport.Size = New System.Drawing.Size(97, 13)
        Me.lbl_ContactSupport.TabIndex = 18
        Me.lbl_ContactSupport.Text = "lbl_ContactSupport"
        '
        'lbk_Support
        '
        Me.lbk_Support.AutoSize = True
        Me.lbk_Support.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbk_Support.Location = New System.Drawing.Point(173, 7)
        Me.lbk_Support.Name = "lbk_Support"
        Me.lbk_Support.Size = New System.Drawing.Size(144, 14)
        Me.lbk_Support.TabIndex = 17
        Me.lbk_Support.TabStop = True
        Me.lbk_Support.Text = "support.logiciels@cticm.com"
        '
        'img_info
        '
        Me.img_info.Dock = System.Windows.Forms.DockStyle.Fill
        Me.img_info.Location = New System.Drawing.Point(2, 2)
        Me.img_info.Margin = New System.Windows.Forms.Padding(2)
        Me.img_info.Name = "img_info"
        Me.img_info.Size = New System.Drawing.Size(26, 26)
        Me.img_info.TabIndex = 81
        Me.img_info.TabStop = False
        '
        'pan_Info
        '
        Me.pan_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Info.Controls.Add(Me.rtb_Info)
        Me.pan_Info.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Info.Location = New System.Drawing.Point(31, 31)
        Me.pan_Info.Margin = New System.Windows.Forms.Padding(1)
        Me.pan_Info.Name = "pan_Info"
        Me.pan_Info.Size = New System.Drawing.Size(359, 117)
        Me.pan_Info.TabIndex = 82
        '
        'rtb_Info
        '
        Me.rtb_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtb_Info.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb_Info.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtb_Info.Location = New System.Drawing.Point(0, 0)
        Me.rtb_Info.Name = "rtb_Info"
        Me.rtb_Info.ReadOnly = True
        Me.rtb_Info.Size = New System.Drawing.Size(357, 115)
        Me.rtb_Info.TabIndex = 0
        Me.rtb_Info.Text = ""
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_InfoW, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.img_Close, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(30, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(361, 30)
        Me.TableLayoutPanel1.TabIndex = 83
        '
        'lbl_InfoW
        '
        Me.lbl_InfoW.AutoSize = True
        Me.lbl_InfoW.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_InfoW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_InfoW.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_InfoW.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_InfoW.Location = New System.Drawing.Point(1, 0)
        Me.lbl_InfoW.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lbl_InfoW.Name = "lbl_InfoW"
        Me.lbl_InfoW.Size = New System.Drawing.Size(329, 30)
        Me.lbl_InfoW.TabIndex = 3
        Me.lbl_InfoW.Text = "lbl_InfoW"
        Me.lbl_InfoW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'img_Close
        '
        Me.img_Close.Image = CType(resources.GetObject("img_Close.Image"), System.Drawing.Image)
        Me.img_Close.Location = New System.Drawing.Point(336, 5)
        Me.img_Close.Margin = New System.Windows.Forms.Padding(5)
        Me.img_Close.Name = "img_Close"
        Me.img_Close.Size = New System.Drawing.Size(20, 20)
        Me.img_Close.TabIndex = 4
        Me.img_Close.TabStop = False
        '
        'imgList_Info
        '
        Me.imgList_Info.ImageStream = CType(resources.GetObject("imgList_Info.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList_Info.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList_Info.Images.SetKeyName(0, "Warning")
        Me.imgList_Info.Images.SetKeyName(1, "Info")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Arial Black", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(2, 151)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 26)
        Me.Label1.TabIndex = 85
        Me.Label1.Text = "i"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label1.Visible = False
        '
        'Frm_InfoLogiciel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(395, 183)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_InfoLogiciel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_InfoLogiciel"
        Me.pan_General.ResumeLayout(False)
        Me.pan_G2.ResumeLayout(False)
        Me.TLPan_General.ResumeLayout(False)
        Me.TLPan_General.PerformLayout()
        Me.pan_Lien.ResumeLayout(False)
        Me.pan_Lien.PerformLayout()
        CType(Me.img_info, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Info.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.img_Close, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents pan_G2 As Panel
    Friend WithEvents TLPan_General As TableLayoutPanel
    Friend WithEvents img_info As PictureBox
    Friend WithEvents pan_Info As Panel
    Friend WithEvents rtb_Info As RichTextBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lbl_InfoW As Label
    Friend WithEvents img_Close As PictureBox
    Friend WithEvents imgList_Info As ImageList
    Friend WithEvents pan_Lien As Panel
    Friend WithEvents lbl_ContactSupport As Label
    Friend WithEvents lbk_Support As LinkLabel
    Friend WithEvents Label1 As Label
End Class
