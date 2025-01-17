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
        Me.pan_Web = New System.Windows.Forms.Panel()
        Me.cmd_CheckUpdates = New System.Windows.Forms.Button()
        Me.chk_ControlFichier = New System.Windows.Forms.CheckBox()
        Me.chk_ControlVersion = New System.Windows.Forms.CheckBox()
        Me.lbl_Web = New System.Windows.Forms.Label()
        Me.pan_Version = New System.Windows.Forms.Panel()
        Me.cmb_Maitre = New System.Windows.Forms.ComboBox()
        Me.lbl_Version2 = New System.Windows.Forms.Label()
        Me.lbl_Version = New System.Windows.Forms.Label()
        Me.pan_Identification = New System.Windows.Forms.Panel()
        Me.lbl_UserName = New System.Windows.Forms.Label()
        Me.txt_UserName = New System.Windows.Forms.TextBox()
        Me.lbl_Firm = New System.Windows.Forms.Label()
        Me.txt_Firm = New System.Windows.Forms.TextBox()
        Me.lbl_Identification = New System.Windows.Forms.Label()
        Me.pan_Langues = New System.Windows.Forms.Panel()
        Me.lstbox_Test = New System.Windows.Forms.ListBox()
        Me.lbl_LangueNdC = New System.Windows.Forms.Label()
        Me.lst_LangueNdC = New System.Windows.Forms.ListBox()
        Me.lbl_LangueGUI = New System.Windows.Forms.Label()
        Me.lst_LangueGUI = New System.Windows.Forms.ListBox()
        Me.lbl_Langues = New System.Windows.Forms.Label()
        Me.pan_General.SuspendLayout()
        Me.pan_Web.SuspendLayout()
        Me.pan_Version.SuspendLayout()
        Me.pan_Identification.SuspendLayout()
        Me.pan_Langues.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_General.Controls.Add(Me.pan_Web)
        Me.pan_General.Controls.Add(Me.pan_Version)
        Me.pan_General.Controls.Add(Me.pan_Identification)
        Me.pan_General.Controls.Add(Me.pan_Langues)
        Me.pan_General.Location = New System.Drawing.Point(83, 30)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(739, 472)
        Me.pan_General.TabIndex = 1
        '
        'pan_Web
        '
        Me.pan_Web.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Web.Controls.Add(Me.cmd_CheckUpdates)
        Me.pan_Web.Controls.Add(Me.chk_ControlFichier)
        Me.pan_Web.Controls.Add(Me.chk_ControlVersion)
        Me.pan_Web.Controls.Add(Me.lbl_Web)
        Me.pan_Web.Location = New System.Drawing.Point(1, 223)
        Me.pan_Web.Name = "pan_Web"
        Me.pan_Web.Size = New System.Drawing.Size(737, 105)
        Me.pan_Web.TabIndex = 103
        '
        'cmd_CheckUpdates
        '
        Me.cmd_CheckUpdates.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmd_CheckUpdates.Location = New System.Drawing.Point(6, 77)
        Me.cmd_CheckUpdates.Name = "cmd_CheckUpdates"
        Me.cmd_CheckUpdates.Size = New System.Drawing.Size(728, 24)
        Me.cmd_CheckUpdates.TabIndex = 104
        Me.cmd_CheckUpdates.Text = "cmd_CheckUpdates"
        Me.cmd_CheckUpdates.UseVisualStyleBackColor = True
        '
        'chk_ControlFichier
        '
        Me.chk_ControlFichier.Location = New System.Drawing.Point(9, 50)
        Me.chk_ControlFichier.Name = "chk_ControlFichier"
        Me.chk_ControlFichier.Size = New System.Drawing.Size(351, 24)
        Me.chk_ControlFichier.TabIndex = 99
        Me.chk_ControlFichier.Text = "chk_ControleFichier"
        Me.chk_ControlFichier.UseVisualStyleBackColor = True
        '
        'chk_ControlVersion
        '
        Me.chk_ControlVersion.Location = New System.Drawing.Point(9, 26)
        Me.chk_ControlVersion.Name = "chk_ControlVersion"
        Me.chk_ControlVersion.Size = New System.Drawing.Size(351, 24)
        Me.chk_ControlVersion.TabIndex = 98
        Me.chk_ControlVersion.Text = "chk_ControlVersion"
        Me.chk_ControlVersion.UseVisualStyleBackColor = True
        '
        'lbl_Web
        '
        Me.lbl_Web.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Web.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Web.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Web.Name = "lbl_Web"
        Me.lbl_Web.Size = New System.Drawing.Size(737, 23)
        Me.lbl_Web.TabIndex = 97
        Me.lbl_Web.Text = "lbl_Web"
        Me.lbl_Web.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Version
        '
        Me.pan_Version.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Version.Controls.Add(Me.cmb_Maitre)
        Me.pan_Version.Controls.Add(Me.lbl_Version2)
        Me.pan_Version.Controls.Add(Me.lbl_Version)
        Me.pan_Version.Location = New System.Drawing.Point(1, 330)
        Me.pan_Version.Name = "pan_Version"
        Me.pan_Version.Size = New System.Drawing.Size(737, 67)
        Me.pan_Version.TabIndex = 102
        '
        'cmb_Maitre
        '
        Me.cmb_Maitre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Maitre.FormattingEnabled = True
        Me.cmb_Maitre.Location = New System.Drawing.Point(117, 31)
        Me.cmb_Maitre.Name = "cmb_Maitre"
        Me.cmb_Maitre.Size = New System.Drawing.Size(189, 21)
        Me.cmb_Maitre.TabIndex = 99
        '
        'lbl_Version2
        '
        Me.lbl_Version2.Location = New System.Drawing.Point(6, 32)
        Me.lbl_Version2.Name = "lbl_Version2"
        Me.lbl_Version2.Size = New System.Drawing.Size(105, 20)
        Me.lbl_Version2.TabIndex = 98
        Me.lbl_Version2.Text = "lbl_Version"
        Me.lbl_Version2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Version
        '
        Me.lbl_Version.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Version.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Version.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Version.Name = "lbl_Version"
        Me.lbl_Version.Size = New System.Drawing.Size(737, 23)
        Me.lbl_Version.TabIndex = 96
        Me.lbl_Version.Text = "lbl_Version"
        Me.lbl_Version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Identification
        '
        Me.pan_Identification.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Identification.Controls.Add(Me.lbl_UserName)
        Me.pan_Identification.Controls.Add(Me.txt_UserName)
        Me.pan_Identification.Controls.Add(Me.lbl_Firm)
        Me.pan_Identification.Controls.Add(Me.txt_Firm)
        Me.pan_Identification.Controls.Add(Me.lbl_Identification)
        Me.pan_Identification.Location = New System.Drawing.Point(1, 135)
        Me.pan_Identification.Name = "pan_Identification"
        Me.pan_Identification.Size = New System.Drawing.Size(737, 86)
        Me.pan_Identification.TabIndex = 101
        '
        'lbl_UserName
        '
        Me.lbl_UserName.Location = New System.Drawing.Point(6, 58)
        Me.lbl_UserName.Name = "lbl_UserName"
        Me.lbl_UserName.Size = New System.Drawing.Size(101, 20)
        Me.lbl_UserName.TabIndex = 100
        Me.lbl_UserName.Text = "lbl_Us.."
        Me.lbl_UserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_UserName
        '
        Me.txt_UserName.Location = New System.Drawing.Point(117, 59)
        Me.txt_UserName.Name = "txt_UserName"
        Me.txt_UserName.Size = New System.Drawing.Size(189, 20)
        Me.txt_UserName.TabIndex = 99
        '
        'lbl_Firm
        '
        Me.lbl_Firm.Location = New System.Drawing.Point(6, 32)
        Me.lbl_Firm.Name = "lbl_Firm"
        Me.lbl_Firm.Size = New System.Drawing.Size(105, 20)
        Me.lbl_Firm.TabIndex = 98
        Me.lbl_Firm.Text = "lbl_Firm"
        Me.lbl_Firm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_Firm
        '
        Me.txt_Firm.Location = New System.Drawing.Point(117, 33)
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
        Me.lbl_Identification.Size = New System.Drawing.Size(737, 23)
        Me.lbl_Identification.TabIndex = 96
        Me.lbl_Identification.Text = "lbl_Identification"
        Me.lbl_Identification.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Langues
        '
        Me.pan_Langues.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Langues.Controls.Add(Me.lstbox_Test)
        Me.pan_Langues.Controls.Add(Me.lbl_LangueNdC)
        Me.pan_Langues.Controls.Add(Me.lst_LangueNdC)
        Me.pan_Langues.Controls.Add(Me.lbl_LangueGUI)
        Me.pan_Langues.Controls.Add(Me.lst_LangueGUI)
        Me.pan_Langues.Controls.Add(Me.lbl_Langues)
        Me.pan_Langues.Location = New System.Drawing.Point(1, 1)
        Me.pan_Langues.Name = "pan_Langues"
        Me.pan_Langues.Size = New System.Drawing.Size(737, 132)
        Me.pan_Langues.TabIndex = 100
        '
        'lstbox_Test
        '
        Me.lstbox_Test.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.lstbox_Test.FormattingEnabled = True
        Me.lstbox_Test.HorizontalScrollbar = True
        Me.lstbox_Test.Location = New System.Drawing.Point(274, 45)
        Me.lstbox_Test.Name = "lstbox_Test"
        Me.lstbox_Test.Size = New System.Drawing.Size(128, 82)
        Me.lstbox_Test.TabIndex = 90
        Me.lstbox_Test.Visible = False
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
        Me.lbl_Langues.Size = New System.Drawing.Size(737, 23)
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
        Me.pan_Web.ResumeLayout(False)
        Me.pan_Version.ResumeLayout(False)
        Me.pan_Identification.ResumeLayout(False)
        Me.pan_Identification.PerformLayout()
        Me.pan_Langues.ResumeLayout(False)
        Me.pan_Langues.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents pan_Identification As Panel
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
    Friend WithEvents lstbox_Test As ListBox
    Friend WithEvents pan_Version As Panel
    Friend WithEvents lbl_Version2 As Label
    Friend WithEvents lbl_Version As Label
    Friend WithEvents cmb_Maitre As ComboBox
    Friend WithEvents pan_Web As Panel
    Friend WithEvents cmd_CheckUpdates As Button
    Friend WithEvents chk_ControlFichier As CheckBox
    Friend WithEvents chk_ControlVersion As CheckBox
    Friend WithEvents lbl_Web As Label
End Class
