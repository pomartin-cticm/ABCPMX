<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_OptionsLogicielGeneral
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_UserName = New System.Windows.Forms.Label()
        Me.txt_UserName = New System.Windows.Forms.TextBox()
        Me.lbl_Firm = New System.Windows.Forms.Label()
        Me.txt_Firm = New System.Windows.Forms.TextBox()
        Me.lbl_Identification = New System.Windows.Forms.Label()
        Me.pan_Langues = New System.Windows.Forms.Panel()
        Me.lbl_LangueNdC = New System.Windows.Forms.Label()
        Me.lst_LangueNdC = New System.Windows.Forms.ListBox()
        Me.lbl_LangueGUI = New System.Windows.Forms.Label()
        Me.lst_LangueGUI = New System.Windows.Forms.ListBox()
        Me.lbl_Langues = New System.Windows.Forms.Label()
        Me.pan_General.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pan_Langues.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_General.Controls.Add(Me.Panel1)
        Me.pan_General.Controls.Add(Me.pan_Langues)
        Me.pan_General.Location = New System.Drawing.Point(83, 30)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(739, 472)
        Me.pan_General.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.lbl_UserName)
        Me.Panel1.Controls.Add(Me.txt_UserName)
        Me.Panel1.Controls.Add(Me.lbl_Firm)
        Me.Panel1.Controls.Add(Me.txt_Firm)
        Me.Panel1.Controls.Add(Me.lbl_Identification)
        Me.Panel1.Location = New System.Drawing.Point(3, 144)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(733, 96)
        Me.Panel1.TabIndex = 101
        '
        'lbl_UserName
        '
        Me.lbl_UserName.Location = New System.Drawing.Point(6, 58)
        Me.lbl_UserName.Name = "lbl_UserName"
        Me.lbl_UserName.Size = New System.Drawing.Size(78, 20)
        Me.lbl_UserName.TabIndex = 100
        Me.lbl_UserName.Text = "lbl_Us.."
        Me.lbl_UserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_UserName
        '
        Me.txt_UserName.Location = New System.Drawing.Point(90, 59)
        Me.txt_UserName.Name = "txt_UserName"
        Me.txt_UserName.Size = New System.Drawing.Size(189, 20)
        Me.txt_UserName.TabIndex = 99
        '
        'lbl_Firm
        '
        Me.lbl_Firm.Location = New System.Drawing.Point(6, 32)
        Me.lbl_Firm.Name = "lbl_Firm"
        Me.lbl_Firm.Size = New System.Drawing.Size(78, 20)
        Me.lbl_Firm.TabIndex = 98
        Me.lbl_Firm.Text = "lbl_Firm"
        Me.lbl_Firm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Firm
        '
        Me.txt_Firm.Location = New System.Drawing.Point(90, 33)
        Me.txt_Firm.Name = "txt_Firm"
        Me.txt_Firm.Size = New System.Drawing.Size(189, 20)
        Me.txt_Firm.TabIndex = 97
        '
        'lbl_Identification
        '
        Me.lbl_Identification.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Identification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Identification.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Identification.Name = "lbl_Identification"
        Me.lbl_Identification.Size = New System.Drawing.Size(733, 23)
        Me.lbl_Identification.TabIndex = 96
        Me.lbl_Identification.Text = "lbl_Identification"
        Me.lbl_Identification.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Langues
        '
        Me.pan_Langues.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Langues.Controls.Add(Me.lbl_LangueNdC)
        Me.pan_Langues.Controls.Add(Me.lst_LangueNdC)
        Me.pan_Langues.Controls.Add(Me.lbl_LangueGUI)
        Me.pan_Langues.Controls.Add(Me.lst_LangueGUI)
        Me.pan_Langues.Controls.Add(Me.lbl_Langues)
        Me.pan_Langues.Location = New System.Drawing.Point(3, 1)
        Me.pan_Langues.Name = "pan_Langues"
        Me.pan_Langues.Size = New System.Drawing.Size(733, 137)
        Me.pan_Langues.TabIndex = 100
        '
        'lbl_LangueNdC
        '
        Me.lbl_LangueNdC.AutoSize = True
        Me.lbl_LangueNdC.Location = New System.Drawing.Point(140, 27)
        Me.lbl_LangueNdC.Name = "lbl_LangueNdC"
        Me.lbl_LangueNdC.Size = New System.Drawing.Size(80, 13)
        Me.lbl_LangueNdC.TabIndex = 89
        Me.lbl_LangueNdC.Text = "lbl_LangueNdC"
        '
        'lst_LangueNdC
        '
        Me.lst_LangueNdC.FormattingEnabled = True
        Me.lst_LangueNdC.HorizontalScrollbar = True
        Me.lst_LangueNdC.Location = New System.Drawing.Point(140, 45)
        Me.lst_LangueNdC.Name = "lst_LangueNdC"
        Me.lst_LangueNdC.Size = New System.Drawing.Size(128, 82)
        Me.lst_LangueNdC.TabIndex = 88
        '
        'lbl_LangueGUI
        '
        Me.lbl_LangueGUI.AutoSize = True
        Me.lbl_LangueGUI.Location = New System.Drawing.Point(6, 27)
        Me.lbl_LangueGUI.Name = "lbl_LangueGUI"
        Me.lbl_LangueGUI.Size = New System.Drawing.Size(78, 13)
        Me.lbl_LangueGUI.TabIndex = 87
        Me.lbl_LangueGUI.Text = "lbl_LangueGUI"
        '
        'lst_LangueGUI
        '
        Me.lst_LangueGUI.FormattingEnabled = True
        Me.lst_LangueGUI.HorizontalScrollbar = True
        Me.lst_LangueGUI.Location = New System.Drawing.Point(6, 45)
        Me.lst_LangueGUI.Name = "lst_LangueGUI"
        Me.lst_LangueGUI.Size = New System.Drawing.Size(128, 82)
        Me.lst_LangueGUI.TabIndex = 86
        '
        'lbl_Langues
        '
        Me.lbl_Langues.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Langues.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Langues.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Langues.Name = "lbl_Langues"
        Me.lbl_Langues.Size = New System.Drawing.Size(733, 23)
        Me.lbl_Langues.TabIndex = 85
        Me.lbl_Langues.Text = "lbl_Langues"
        Me.lbl_Langues.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_OptionsLogicielGeneral
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(904, 533)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsLogicielGeneral"
        Me.Text = "Frm_OptionsLogicielGeneral"
        Me.pan_General.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pan_Langues.ResumeLayout(False)
        Me.pan_Langues.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl_UserName As Label
    Friend WithEvents txt_UserName As TextBox
    Friend WithEvents lbl_Firm As Label
    Friend WithEvents txt_Firm As TextBox
    Friend WithEvents lbl_Identification As Label
    Friend WithEvents pan_Langues As Panel
    Friend WithEvents lbl_LangueNdC As Label
    Friend WithEvents lst_LangueNdC As ListBox
    Friend WithEvents lbl_LangueGUI As Label
    Friend WithEvents lst_LangueGUI As ListBox
    Friend WithEvents lbl_Langues As Label
End Class
