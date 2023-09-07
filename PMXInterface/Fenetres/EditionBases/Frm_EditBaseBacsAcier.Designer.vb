<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EditBaseBacsAcier
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
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_OrganisationColonnes = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_AffichageBase = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_BacAffiche = New System.Windows.Forms.Label()
        Me.img_Bac = New System.Windows.Forms.PictureBox()
        Me.lbl_Base = New System.Windows.Forms.Label()
        Me.pan_Base = New System.Windows.Forms.Panel()
        Me.Grid_Sheets = New System.Windows.Forms.DataGridView()
        Me.Col_Ind = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Label = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_b1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_b2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_E = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_H = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_hpg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_T = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_M = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Fy = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Vide = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_OrganisationColonnes.SuspendLayout()
        Me.TLpan_AffichageBase.SuspendLayout()
        CType(Me.img_Bac, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Base.SuspendLayout()
        CType(Me.Grid_Sheets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(907, 588)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(907, 588)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 551)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(901, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(393, 3)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(114, 28)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_OrganisationColonnes)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(901, 542)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_OrganisationColonnes
        '
        Me.TLPan_OrganisationColonnes.ColumnCount = 2
        Me.TLPan_OrganisationColonnes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31.0!))
        Me.TLPan_OrganisationColonnes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_OrganisationColonnes.Controls.Add(Me.TLpan_AffichageBase, 1, 0)
        Me.TLPan_OrganisationColonnes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_OrganisationColonnes.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_OrganisationColonnes.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_OrganisationColonnes.Name = "TLPan_OrganisationColonnes"
        Me.TLPan_OrganisationColonnes.RowCount = 1
        Me.TLPan_OrganisationColonnes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_OrganisationColonnes.Size = New System.Drawing.Size(901, 542)
        Me.TLPan_OrganisationColonnes.TabIndex = 0
        '
        'TLpan_AffichageBase
        '
        Me.TLpan_AffichageBase.ColumnCount = 1
        Me.TLpan_AffichageBase.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_AffichageBase.Controls.Add(Me.lbl_BacAffiche, 0, 2)
        Me.TLpan_AffichageBase.Controls.Add(Me.img_Bac, 0, 3)
        Me.TLpan_AffichageBase.Controls.Add(Me.lbl_Base, 0, 0)
        Me.TLpan_AffichageBase.Controls.Add(Me.pan_Base, 0, 1)
        Me.TLpan_AffichageBase.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_AffichageBase.Location = New System.Drawing.Point(31, 0)
        Me.TLpan_AffichageBase.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_AffichageBase.Name = "TLpan_AffichageBase"
        Me.TLpan_AffichageBase.RowCount = 4
        Me.TLpan_AffichageBase.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_AffichageBase.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_AffichageBase.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_AffichageBase.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TLpan_AffichageBase.Size = New System.Drawing.Size(870, 542)
        Me.TLpan_AffichageBase.TabIndex = 0
        '
        'lbl_BacAffiche
        '
        Me.lbl_BacAffiche.AutoSize = True
        Me.lbl_BacAffiche.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_BacAffiche.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_BacAffiche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_BacAffiche.Location = New System.Drawing.Point(0, 362)
        Me.lbl_BacAffiche.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_BacAffiche.Name = "lbl_BacAffiche"
        Me.lbl_BacAffiche.Size = New System.Drawing.Size(870, 30)
        Me.lbl_BacAffiche.TabIndex = 6
        Me.lbl_BacAffiche.Text = "Label1"
        Me.lbl_BacAffiche.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'img_Bac
        '
        Me.img_Bac.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Bac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Bac.Location = New System.Drawing.Point(0, 393)
        Me.img_Bac.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.img_Bac.Name = "img_Bac"
        Me.img_Bac.Size = New System.Drawing.Size(100, 50)
        Me.img_Bac.TabIndex = 5
        Me.img_Bac.TabStop = False
        '
        'lbl_Base
        '
        Me.lbl_Base.AutoSize = True
        Me.lbl_Base.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Base.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Base.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Base.Name = "lbl_Base"
        Me.lbl_Base.Size = New System.Drawing.Size(870, 30)
        Me.lbl_Base.TabIndex = 3
        Me.lbl_Base.Text = "lbl_Base"
        Me.lbl_Base.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Base
        '
        Me.pan_Base.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Base.Controls.Add(Me.Grid_Sheets)
        Me.pan_Base.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Base.Location = New System.Drawing.Point(0, 31)
        Me.pan_Base.Margin = New System.Windows.Forms.Padding(0, 1, 0, 1)
        Me.pan_Base.Name = "pan_Base"
        Me.pan_Base.Size = New System.Drawing.Size(870, 330)
        Me.pan_Base.TabIndex = 4
        '
        'Grid_Sheets
        '
        Me.Grid_Sheets.AllowUserToAddRows = False
        Me.Grid_Sheets.AllowUserToDeleteRows = False
        Me.Grid_Sheets.AllowUserToResizeColumns = False
        Me.Grid_Sheets.AllowUserToResizeRows = False
        Me.Grid_Sheets.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Grid_Sheets.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Grid_Sheets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid_Sheets.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Ind, Me.Col_Label, Me.Col_b1, Me.Col_b2, Me.Col_E, Me.Col_H, Me.Col_hpg, Me.Col_T, Me.Col_M, Me.Col_Fy, Me.Col_Vide})
        Me.Grid_Sheets.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid_Sheets.Location = New System.Drawing.Point(0, 0)
        Me.Grid_Sheets.MultiSelect = False
        Me.Grid_Sheets.Name = "Grid_Sheets"
        Me.Grid_Sheets.ReadOnly = True
        Me.Grid_Sheets.RowHeadersVisible = False
        Me.Grid_Sheets.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Grid_Sheets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid_Sheets.Size = New System.Drawing.Size(868, 328)
        Me.Grid_Sheets.TabIndex = 18
        '
        'Col_Ind
        '
        Me.Col_Ind.Frozen = True
        Me.Col_Ind.HeaderText = ""
        Me.Col_Ind.Name = "Col_Ind"
        Me.Col_Ind.ReadOnly = True
        Me.Col_Ind.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Ind.Width = 30
        '
        'Col_Label
        '
        Me.Col_Label.Frozen = True
        Me.Col_Label.HeaderText = "Labelx"
        Me.Col_Label.Name = "Col_Label"
        Me.Col_Label.ReadOnly = True
        Me.Col_Label.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_Label.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Label.Width = 150
        '
        'Col_b1
        '
        DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Col_b1.DefaultCellStyle = DataGridViewCellStyle25
        Me.Col_b1.Frozen = True
        Me.Col_b1.HeaderText = "b1"
        Me.Col_b1.Name = "Col_b1"
        Me.Col_b1.ReadOnly = True
        Me.Col_b1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_b1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_b1.Width = 80
        '
        'Col_b2
        '
        DataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Col_b2.DefaultCellStyle = DataGridViewCellStyle26
        Me.Col_b2.Frozen = True
        Me.Col_b2.HeaderText = "b2"
        Me.Col_b2.Name = "Col_b2"
        Me.Col_b2.ReadOnly = True
        Me.Col_b2.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_b2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_b2.Width = 80
        '
        'Col_E
        '
        DataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Col_E.DefaultCellStyle = DataGridViewCellStyle27
        Me.Col_E.Frozen = True
        Me.Col_E.HeaderText = "E"
        Me.Col_E.Name = "Col_E"
        Me.Col_E.ReadOnly = True
        Me.Col_E.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_E.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_E.Width = 80
        '
        'Col_H
        '
        DataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Col_H.DefaultCellStyle = DataGridViewCellStyle28
        Me.Col_H.Frozen = True
        Me.Col_H.HeaderText = "H"
        Me.Col_H.Name = "Col_H"
        Me.Col_H.ReadOnly = True
        Me.Col_H.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_H.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_H.Width = 80
        '
        'Col_hpg
        '
        Me.Col_hpg.Frozen = True
        Me.Col_hpg.HeaderText = "Hpg"
        Me.Col_hpg.Name = "Col_hpg"
        Me.Col_hpg.ReadOnly = True
        '
        'Col_T
        '
        DataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Col_T.DefaultCellStyle = DataGridViewCellStyle29
        Me.Col_T.Frozen = True
        Me.Col_T.HeaderText = "T"
        Me.Col_T.Name = "Col_T"
        Me.Col_T.ReadOnly = True
        Me.Col_T.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_T.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_T.Width = 80
        '
        'Col_M
        '
        DataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Col_M.DefaultCellStyle = DataGridViewCellStyle30
        Me.Col_M.Frozen = True
        Me.Col_M.HeaderText = "M"
        Me.Col_M.Name = "Col_M"
        Me.Col_M.ReadOnly = True
        Me.Col_M.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_M.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_M.Width = 80
        '
        'Col_Fy
        '
        Me.Col_Fy.Frozen = True
        Me.Col_Fy.HeaderText = "Fy"
        Me.Col_Fy.Name = "Col_Fy"
        Me.Col_Fy.ReadOnly = True
        Me.Col_Fy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Col_Vide
        '
        Me.Col_Vide.Frozen = True
        Me.Col_Vide.HeaderText = ""
        Me.Col_Vide.Name = "Col_Vide"
        Me.Col_Vide.ReadOnly = True
        Me.Col_Vide.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Frm_EditBaseBacsAcier
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_OK
        Me.ClientSize = New System.Drawing.Size(907, 588)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_EditBaseBacsAcier"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_EditBaseBacsAcier"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_OrganisationColonnes.ResumeLayout(False)
        Me.TLpan_AffichageBase.ResumeLayout(False)
        Me.TLpan_AffichageBase.PerformLayout()
        CType(Me.img_Bac, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Base.ResumeLayout(False)
        CType(Me.Grid_Sheets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_OrganisationColonnes As TableLayoutPanel
    Friend WithEvents TLpan_AffichageBase As TableLayoutPanel
    Friend WithEvents lbl_Base As Label
    Friend WithEvents pan_Base As Panel
    Friend WithEvents img_Bac As PictureBox
    Friend WithEvents lbl_BacAffiche As Label
    Friend WithEvents Grid_Sheets As DataGridView
    Friend WithEvents Col_Ind As DataGridViewTextBoxColumn
    Friend WithEvents Col_Label As DataGridViewTextBoxColumn
    Friend WithEvents Col_b1 As DataGridViewTextBoxColumn
    Friend WithEvents Col_b2 As DataGridViewTextBoxColumn
    Friend WithEvents Col_E As DataGridViewTextBoxColumn
    Friend WithEvents Col_H As DataGridViewTextBoxColumn
    Friend WithEvents Col_hpg As DataGridViewTextBoxColumn
    Friend WithEvents Col_T As DataGridViewTextBoxColumn
    Friend WithEvents Col_M As DataGridViewTextBoxColumn
    Friend WithEvents Col_Fy As DataGridViewTextBoxColumn
    Friend WithEvents Col_Vide As DataGridViewTextBoxColumn
End Class
