<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Identification
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
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Identification = New System.Windows.Forms.Label()
        Me.pan_SaisiePortee = New System.Windows.Forms.Panel()
        Me.lbl_Comment = New System.Windows.Forms.Label()
        Me.lbl_BeamID = New System.Windows.Forms.Label()
        Me.lbl_Project = New System.Windows.Forms.Label()
        Me.lbl_Company = New System.Windows.Forms.Label()
        Me.lbl_User = New System.Windows.Forms.Label()
        Me.txt_Comment = New System.Windows.Forms.TextBox()
        Me.txt_BeamID = New System.Windows.Forms.TextBox()
        Me.txt_Project = New System.Windows.Forms.TextBox()
        Me.txt_Company = New System.Windows.Forms.TextBox()
        Me.txt_User = New System.Windows.Forms.TextBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_SaisiePortee.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(592, 304)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(592, 304)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 5
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 3, 0)
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_Annuler, 1, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(4, 259)
        Me.TLPan_PartieBasse.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(584, 41)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(309, 4)
        Me.btn_OK.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(152, 33)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'btn_Annuler
        '
        Me.btn_Annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Annuler.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Annuler.Location = New System.Drawing.Point(122, 4)
        Me.btn_Annuler.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(152, 33)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(4, 4)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(584, 247)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Identification, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisiePortee, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 2
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 209.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(584, 247)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Identification
        '
        Me.lbl_Identification.AutoSize = True
        Me.lbl_Identification.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Identification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Identification.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Identification.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Identification.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Identification.Name = "lbl_Identification"
        Me.lbl_Identification.Size = New System.Drawing.Size(584, 37)
        Me.lbl_Identification.TabIndex = 0
        Me.lbl_Identification.Text = "lbl_Identification"
        Me.lbl_Identification.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisiePortee
        '
        Me.pan_SaisiePortee.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisiePortee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_Comment)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_BeamID)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_Project)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_Company)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_User)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_Comment)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_BeamID)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_Project)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_Company)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_User)
        Me.pan_SaisiePortee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisiePortee.Location = New System.Drawing.Point(0, 38)
        Me.pan_SaisiePortee.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.pan_SaisiePortee.Name = "pan_SaisiePortee"
        Me.pan_SaisiePortee.Size = New System.Drawing.Size(584, 209)
        Me.pan_SaisiePortee.TabIndex = 1
        '
        'lbl_Comment
        '
        Me.lbl_Comment.Location = New System.Drawing.Point(11, 151)
        Me.lbl_Comment.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Comment.Name = "lbl_Comment"
        Me.lbl_Comment.Size = New System.Drawing.Size(148, 21)
        Me.lbl_Comment.TabIndex = 78
        Me.lbl_Comment.Text = "lbl_Comment"
        Me.lbl_Comment.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_BeamID
        '
        Me.lbl_BeamID.Location = New System.Drawing.Point(11, 119)
        Me.lbl_BeamID.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_BeamID.Name = "lbl_BeamID"
        Me.lbl_BeamID.Size = New System.Drawing.Size(148, 21)
        Me.lbl_BeamID.TabIndex = 78
        Me.lbl_BeamID.Text = "lbl_BeamID"
        Me.lbl_BeamID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_Project
        '
        Me.lbl_Project.Location = New System.Drawing.Point(11, 87)
        Me.lbl_Project.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Project.Name = "lbl_Project"
        Me.lbl_Project.Size = New System.Drawing.Size(148, 21)
        Me.lbl_Project.TabIndex = 78
        Me.lbl_Project.Text = "lbl_Project"
        Me.lbl_Project.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_Company
        '
        Me.lbl_Company.Location = New System.Drawing.Point(11, 55)
        Me.lbl_Company.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Company.Name = "lbl_Company"
        Me.lbl_Company.Size = New System.Drawing.Size(148, 21)
        Me.lbl_Company.TabIndex = 78
        Me.lbl_Company.Text = "lbl_Company"
        Me.lbl_Company.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_User
        '
        Me.lbl_User.Location = New System.Drawing.Point(11, 23)
        Me.lbl_User.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_User.Name = "lbl_User"
        Me.lbl_User.Size = New System.Drawing.Size(148, 21)
        Me.lbl_User.TabIndex = 78
        Me.lbl_User.Text = "lbl_User"
        Me.lbl_User.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Comment
        '
        Me.txt_Comment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Comment.Location = New System.Drawing.Point(184, 151)
        Me.txt_Comment.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_Comment.Name = "txt_Comment"
        Me.txt_Comment.Size = New System.Drawing.Size(352, 22)
        Me.txt_Comment.TabIndex = 80
        '
        'txt_BeamID
        '
        Me.txt_BeamID.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_BeamID.Location = New System.Drawing.Point(184, 119)
        Me.txt_BeamID.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_BeamID.Name = "txt_BeamID"
        Me.txt_BeamID.Size = New System.Drawing.Size(352, 22)
        Me.txt_BeamID.TabIndex = 79
        '
        'txt_Project
        '
        Me.txt_Project.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Project.Location = New System.Drawing.Point(184, 87)
        Me.txt_Project.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_Project.Name = "txt_Project"
        Me.txt_Project.Size = New System.Drawing.Size(352, 22)
        Me.txt_Project.TabIndex = 78
        '
        'txt_Company
        '
        Me.txt_Company.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Company.Location = New System.Drawing.Point(184, 55)
        Me.txt_Company.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_Company.Name = "txt_Company"
        Me.txt_Company.Size = New System.Drawing.Size(352, 22)
        Me.txt_Company.TabIndex = 77
        '
        'txt_User
        '
        Me.txt_User.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_User.Location = New System.Drawing.Point(184, 23)
        Me.txt_User.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_User.Name = "txt_User"
        Me.txt_User.Size = New System.Drawing.Size(352, 22)
        Me.txt_User.TabIndex = 76
        '
        'Frm_Identification
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(592, 304)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Identification"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Frm_Identification"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_SaisiePortee.ResumeLayout(False)
        Me.pan_SaisiePortee.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Identification As Label
    Friend WithEvents pan_SaisiePortee As Panel
    Friend WithEvents txt_User As TextBox
    Friend WithEvents txt_Company As TextBox
    Friend WithEvents txt_Comment As TextBox
    Friend WithEvents txt_BeamID As TextBox
    Friend WithEvents txt_Project As TextBox
    Friend WithEvents lbl_Comment As Label
    Friend WithEvents lbl_BeamID As Label
    Friend WithEvents lbl_Project As Label
    Friend WithEvents lbl_Company As Label
    Friend WithEvents lbl_User As Label
End Class
