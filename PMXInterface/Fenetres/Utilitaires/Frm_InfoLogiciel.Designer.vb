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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_InfoLogiciel))
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.pan_G2 = New System.Windows.Forms.Panel()
        Me.TLPan_General = New System.Windows.Forms.TableLayoutPanel()
        Me.img_info = New System.Windows.Forms.PictureBox()
        Me.pan_Info = New System.Windows.Forms.Panel()
        Me.rtb_Info = New System.Windows.Forms.RichTextBox()
        Me.pan_General.SuspendLayout()
        Me.pan_G2.SuspendLayout()
        Me.TLPan_General.SuspendLayout()
        CType(Me.img_info, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Info.SuspendLayout()
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
        Me.pan_General.Size = New System.Drawing.Size(395, 134)
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
        Me.pan_G2.Size = New System.Drawing.Size(391, 130)
        Me.pan_G2.TabIndex = 0
        '
        'TLPan_General
        '
        Me.TLPan_General.ColumnCount = 2
        Me.TLPan_General.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_General.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_General.Controls.Add(Me.img_info, 0, 0)
        Me.TLPan_General.Controls.Add(Me.pan_Info, 1, 1)
        Me.TLPan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_General.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_General.Name = "TLPan_General"
        Me.TLPan_General.RowCount = 2
        Me.TLPan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_General.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_General.Size = New System.Drawing.Size(391, 130)
        Me.TLPan_General.TabIndex = 0
        '
        'img_info
        '
        Me.img_info.Dock = System.Windows.Forms.DockStyle.Fill
        Me.img_info.Image = CType(resources.GetObject("img_info.Image"), System.Drawing.Image)
        Me.img_info.Location = New System.Drawing.Point(5, 5)
        Me.img_info.Margin = New System.Windows.Forms.Padding(5)
        Me.img_info.Name = "img_info"
        Me.img_info.Size = New System.Drawing.Size(20, 20)
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
        Me.pan_Info.Size = New System.Drawing.Size(359, 98)
        Me.pan_Info.TabIndex = 82
        '
        'rtb_Info
        '
        Me.rtb_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtb_Info.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb_Info.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtb_Info.Location = New System.Drawing.Point(0, 0)
        Me.rtb_Info.Name = "rtb_Info"
        Me.rtb_Info.ReadOnly = True
        Me.rtb_Info.Size = New System.Drawing.Size(357, 96)
        Me.rtb_Info.TabIndex = 0
        Me.rtb_Info.Text = ""
        '
        'Frm_InfoLogiciel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(395, 134)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_InfoLogiciel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_InfoLogiciel"
        Me.pan_General.ResumeLayout(False)
        Me.pan_G2.ResumeLayout(False)
        Me.TLPan_General.ResumeLayout(False)
        CType(Me.img_info, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Info.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents pan_G2 As Panel
    Friend WithEvents TLPan_General As TableLayoutPanel
    Friend WithEvents img_info As PictureBox
    Friend WithEvents pan_Info As Panel
    Friend WithEvents rtb_Info As RichTextBox
End Class
