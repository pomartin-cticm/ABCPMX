<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Ouverture
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Ouverture))
        Me.TabControl_MenuProject = New System.Windows.Forms.TabControl()
        Me.TabPage_NewProject = New System.Windows.Forms.TabPage()
        Me.TabPage_OpenProject = New System.Windows.Forms.TabPage()
        Me.pan_Open = New System.Windows.Forms.Panel()
        Me.TLpan_OpenProject = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_OpenFile = New System.Windows.Forms.Label()
        Me.lbl_RecentFiles = New System.Windows.Forms.Label()
        Me.Button_OpenProject = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ListBox_RecentFiles = New System.Windows.Forms.ListBox()
        Me.TableLayoutPanel_NewProject = New System.Windows.Forms.TableLayoutPanel()
        Me.Button_Valider = New System.Windows.Forms.Button()
        Me.OpenFileDialog_Project = New System.Windows.Forms.OpenFileDialog()
        Me.TabControl_MenuProject.SuspendLayout()
        Me.TabPage_OpenProject.SuspendLayout()
        Me.pan_Open.SuspendLayout()
        Me.TLpan_OpenProject.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel_NewProject.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl_MenuProject
        '
        Me.TableLayoutPanel_NewProject.SetColumnSpan(Me.TabControl_MenuProject, 3)
        Me.TabControl_MenuProject.Controls.Add(Me.TabPage_NewProject)
        Me.TabControl_MenuProject.Controls.Add(Me.TabPage_OpenProject)
        Me.TabControl_MenuProject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl_MenuProject.ItemSize = New System.Drawing.Size(117, 20)
        Me.TabControl_MenuProject.Location = New System.Drawing.Point(3, 3)
        Me.TabControl_MenuProject.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.TabControl_MenuProject.Multiline = True
        Me.TabControl_MenuProject.Name = "TabControl_MenuProject"
        Me.TabControl_MenuProject.Padding = New System.Drawing.Point(12, 3)
        Me.TabControl_MenuProject.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TabControl_MenuProject.SelectedIndex = 0
        Me.TabControl_MenuProject.Size = New System.Drawing.Size(747, 420)
        Me.TabControl_MenuProject.TabIndex = 0
        '
        'TabPage_NewProject
        '
        Me.TabPage_NewProject.Location = New System.Drawing.Point(4, 24)
        Me.TabPage_NewProject.Name = "TabPage_NewProject"
        Me.TabPage_NewProject.Size = New System.Drawing.Size(739, 392)
        Me.TabPage_NewProject.TabIndex = 0
        Me.TabPage_NewProject.Text = "TabPage_NewProject"
        Me.TabPage_NewProject.UseVisualStyleBackColor = True
        '
        'TabPage_OpenProject
        '
        Me.TabPage_OpenProject.Controls.Add(Me.pan_Open)
        Me.TabPage_OpenProject.Location = New System.Drawing.Point(4, 24)
        Me.TabPage_OpenProject.Name = "TabPage_OpenProject"
        Me.TabPage_OpenProject.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_OpenProject.Size = New System.Drawing.Size(739, 392)
        Me.TabPage_OpenProject.TabIndex = 1
        Me.TabPage_OpenProject.Text = "TabPage_OpenProject"
        Me.TabPage_OpenProject.UseVisualStyleBackColor = True
        '
        'pan_Open
        '
        Me.pan_Open.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Open.Controls.Add(Me.TLpan_OpenProject)
        Me.pan_Open.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Open.Location = New System.Drawing.Point(3, 3)
        Me.pan_Open.Name = "pan_Open"
        Me.pan_Open.Size = New System.Drawing.Size(733, 386)
        Me.pan_Open.TabIndex = 2
        '
        'TLpan_OpenProject
        '
        Me.TLpan_OpenProject.ColumnCount = 5
        Me.TLpan_OpenProject.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TLpan_OpenProject.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_OpenProject.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 146.0!))
        Me.TLpan_OpenProject.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_OpenProject.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TLpan_OpenProject.Controls.Add(Me.lbl_OpenFile, 1, 1)
        Me.TLpan_OpenProject.Controls.Add(Me.lbl_RecentFiles, 1, 5)
        Me.TLpan_OpenProject.Controls.Add(Me.Button_OpenProject, 2, 3)
        Me.TLpan_OpenProject.Controls.Add(Me.Panel1, 1, 7)
        Me.TLpan_OpenProject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_OpenProject.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_OpenProject.Name = "TLpan_OpenProject"
        Me.TLpan_OpenProject.RowCount = 9
        Me.TLpan_OpenProject.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.27273!))
        Me.TLpan_OpenProject.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TLpan_OpenProject.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090907!))
        Me.TLpan_OpenProject.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TLpan_OpenProject.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.27273!))
        Me.TLpan_OpenProject.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.TLpan_OpenProject.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090907!))
        Me.TLpan_OpenProject.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TLpan_OpenProject.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.27273!))
        Me.TLpan_OpenProject.Size = New System.Drawing.Size(731, 384)
        Me.TLpan_OpenProject.TabIndex = 1
        '
        'lbl_OpenFile
        '
        Me.lbl_OpenFile.AutoEllipsis = True
        Me.lbl_OpenFile.BackColor = System.Drawing.Color.Gold
        Me.lbl_OpenFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TLpan_OpenProject.SetColumnSpan(Me.lbl_OpenFile, 3)
        Me.lbl_OpenFile.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_OpenFile.Location = New System.Drawing.Point(18, 37)
        Me.lbl_OpenFile.Name = "lbl_OpenFile"
        Me.lbl_OpenFile.Size = New System.Drawing.Size(694, 26)
        Me.lbl_OpenFile.TabIndex = 4
        Me.lbl_OpenFile.Text = "lbl_OpenFile"
        Me.lbl_OpenFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_RecentFiles
        '
        Me.lbl_RecentFiles.AutoEllipsis = True
        Me.lbl_RecentFiles.BackColor = System.Drawing.Color.Gold
        Me.lbl_RecentFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TLpan_OpenProject.SetColumnSpan(Me.lbl_RecentFiles, 3)
        Me.lbl_RecentFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_RecentFiles.Location = New System.Drawing.Point(18, 148)
        Me.lbl_RecentFiles.Name = "lbl_RecentFiles"
        Me.lbl_RecentFiles.Size = New System.Drawing.Size(694, 26)
        Me.lbl_RecentFiles.TabIndex = 2
        Me.lbl_RecentFiles.Text = "lbl_RecentFiles"
        Me.lbl_RecentFiles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Button_OpenProject
        '
        Me.Button_OpenProject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_OpenProject.Image = CType(resources.GetObject("Button_OpenProject.Image"), System.Drawing.Image)
        Me.Button_OpenProject.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button_OpenProject.Location = New System.Drawing.Point(295, 78)
        Me.Button_OpenProject.Name = "Button_OpenProject"
        Me.Button_OpenProject.Size = New System.Drawing.Size(140, 30)
        Me.Button_OpenProject.TabIndex = 3
        Me.Button_OpenProject.Text = "Button_OpenProject"
        Me.Button_OpenProject.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.TLpan_OpenProject.SetColumnSpan(Me.Panel1, 3)
        Me.Panel1.Controls.Add(Me.ListBox_RecentFiles)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(18, 187)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 1, 3, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(694, 159)
        Me.Panel1.TabIndex = 5
        '
        'ListBox_RecentFiles
        '
        Me.ListBox_RecentFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox_RecentFiles.FormattingEnabled = True
        Me.ListBox_RecentFiles.HorizontalScrollbar = True
        Me.ListBox_RecentFiles.Location = New System.Drawing.Point(0, 0)
        Me.ListBox_RecentFiles.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.ListBox_RecentFiles.Name = "ListBox_RecentFiles"
        Me.ListBox_RecentFiles.Size = New System.Drawing.Size(694, 159)
        Me.ListBox_RecentFiles.TabIndex = 0
        '
        'TableLayoutPanel_NewProject
        '
        Me.TableLayoutPanel_NewProject.ColumnCount = 3
        Me.TableLayoutPanel_NewProject.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_NewProject.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96.0!))
        Me.TableLayoutPanel_NewProject.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel_NewProject.Controls.Add(Me.TabControl_MenuProject, 0, 0)
        Me.TableLayoutPanel_NewProject.Controls.Add(Me.Button_Valider, 1, 1)
        Me.TableLayoutPanel_NewProject.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_NewProject.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_NewProject.Name = "TableLayoutPanel_NewProject"
        Me.TableLayoutPanel_NewProject.RowCount = 2
        Me.TableLayoutPanel_NewProject.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_NewProject.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel_NewProject.Size = New System.Drawing.Size(753, 459)
        Me.TableLayoutPanel_NewProject.TabIndex = 2
        '
        'Button_Valider
        '
        Me.Button_Valider.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button_Valider.Location = New System.Drawing.Point(331, 426)
        Me.Button_Valider.Name = "Button_Valider"
        Me.Button_Valider.Size = New System.Drawing.Size(90, 30)
        Me.Button_Valider.TabIndex = 1
        Me.Button_Valider.Text = "Button_Valider"
        Me.Button_Valider.UseVisualStyleBackColor = True
        '
        'OpenFileDialog_Project
        '
        Me.OpenFileDialog_Project.FileName = "OpenFileDialog1"
        '
        'Frm_Ouverture
        '
        Me.AcceptButton = Me.Button_Valider
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(753, 459)
        Me.Controls.Add(Me.TableLayoutPanel_NewProject)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Ouverture"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Frm_Ouverture"
        Me.TabControl_MenuProject.ResumeLayout(False)
        Me.TabPage_OpenProject.ResumeLayout(False)
        Me.pan_Open.ResumeLayout(False)
        Me.TLpan_OpenProject.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel_NewProject.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl_MenuProject As TabControl
    Friend WithEvents TableLayoutPanel_NewProject As TableLayoutPanel
    Friend WithEvents Button_Valider As Button
    Friend WithEvents TabPage_NewProject As TabPage
    Friend WithEvents TabPage_OpenProject As TabPage
    Friend WithEvents TLpan_OpenProject As TableLayoutPanel
    Friend WithEvents lbl_OpenFile As Label
    Friend WithEvents lbl_RecentFiles As Label
    Friend WithEvents ListBox_RecentFiles As ListBox
    Friend WithEvents Button_OpenProject As Button
    Friend WithEvents OpenFileDialog_Project As OpenFileDialog
    Friend WithEvents Panel1 As Panel
    Friend WithEvents pan_Open As Panel
End Class
