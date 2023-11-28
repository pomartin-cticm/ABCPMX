<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EditGoujons
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_EditGoujons))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.MenuNouveau = New System.Windows.Forms.ToolStripButton()
        Me.MenuModifier = New System.Windows.Forms.ToolStripButton()
        Me.MenuSupprimer = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuEnregistrerBase = New System.Windows.Forms.ToolStripButton()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.Grid_Studs = New System.Windows.Forms.DataGridView()
        Me.Col_Check = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Col_Ind = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Label = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Ht = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_PhiRod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_HeadPhi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_HeadDepth = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_YieldStrength = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_UltimateStrength = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Vide = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.etq_test = New System.Windows.Forms.Label()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.Grid_Studs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuNouveau, Me.MenuModifier, Me.MenuSupprimer, Me.ToolStripSeparator1, Me.MenuEnregistrerBase})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(675, 31)
        Me.ToolStrip1.TabIndex = 13
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'MenuNouveau
        '
        Me.MenuNouveau.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MenuNouveau.Image = CType(resources.GetObject("MenuNouveau.Image"), System.Drawing.Image)
        Me.MenuNouveau.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.MenuNouveau.Name = "MenuNouveau"
        Me.MenuNouveau.Size = New System.Drawing.Size(28, 28)
        Me.MenuNouveau.Text = "ToolStripButton1"
        '
        'MenuModifier
        '
        Me.MenuModifier.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MenuModifier.Image = CType(resources.GetObject("MenuModifier.Image"), System.Drawing.Image)
        Me.MenuModifier.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.MenuModifier.Name = "MenuModifier"
        Me.MenuModifier.Size = New System.Drawing.Size(28, 28)
        Me.MenuModifier.Text = "ToolStripButton1"
        '
        'MenuSupprimer
        '
        Me.MenuSupprimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MenuSupprimer.Image = CType(resources.GetObject("MenuSupprimer.Image"), System.Drawing.Image)
        Me.MenuSupprimer.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.MenuSupprimer.Name = "MenuSupprimer"
        Me.MenuSupprimer.Size = New System.Drawing.Size(28, 28)
        Me.MenuSupprimer.Text = "ToolStripButton1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 31)
        '
        'MenuEnregistrerBase
        '
        Me.MenuEnregistrerBase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MenuEnregistrerBase.Image = CType(resources.GetObject("MenuEnregistrerBase.Image"), System.Drawing.Image)
        Me.MenuEnregistrerBase.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.MenuEnregistrerBase.Name = "MenuEnregistrerBase"
        Me.MenuEnregistrerBase.Size = New System.Drawing.Size(28, 28)
        Me.MenuEnregistrerBase.Text = "ToolStripButton2"
        '
        'Grid_Studs
        '
        Me.Grid_Studs.AllowUserToAddRows = False
        Me.Grid_Studs.AllowUserToDeleteRows = False
        Me.Grid_Studs.AllowUserToResizeColumns = False
        Me.Grid_Studs.AllowUserToResizeRows = False
        Me.Grid_Studs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grid_Studs.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Grid_Studs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Grid_Studs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Check, Me.Col_Ind, Me.Col_Label, Me.Col_Ht, Me.Col_PhiRod, Me.Col_HeadPhi, Me.Col_HeadDepth, Me.Col_YieldStrength, Me.Col_UltimateStrength, Me.Col_Vide})
        Me.Grid_Studs.Location = New System.Drawing.Point(10, 34)
        Me.Grid_Studs.MultiSelect = False
        Me.Grid_Studs.Name = "Grid_Studs"
        Me.Grid_Studs.ReadOnly = True
        Me.Grid_Studs.RowHeadersVisible = False
        Me.Grid_Studs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Grid_Studs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid_Studs.Size = New System.Drawing.Size(661, 454)
        Me.Grid_Studs.TabIndex = 8
        '
        'Col_Check
        '
        Me.Col_Check.Frozen = True
        Me.Col_Check.HeaderText = ""
        Me.Col_Check.Name = "Col_Check"
        Me.Col_Check.ReadOnly = True
        Me.Col_Check.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_Check.Width = 20
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
        'Col_Ht
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Col_Ht.DefaultCellStyle = DataGridViewCellStyle1
        Me.Col_Ht.Frozen = True
        Me.Col_Ht.HeaderText = "Ht (mm)"
        Me.Col_Ht.Name = "Col_Ht"
        Me.Col_Ht.ReadOnly = True
        Me.Col_Ht.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_Ht.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Ht.Width = 80
        '
        'Col_PhiRod
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Col_PhiRod.DefaultCellStyle = DataGridViewCellStyle2
        Me.Col_PhiRod.Frozen = True
        Me.Col_PhiRod.HeaderText = "Rod Diameter (mm)"
        Me.Col_PhiRod.Name = "Col_PhiRod"
        Me.Col_PhiRod.ReadOnly = True
        Me.Col_PhiRod.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_PhiRod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_PhiRod.Width = 80
        '
        'Col_HeadPhi
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Col_HeadPhi.DefaultCellStyle = DataGridViewCellStyle3
        Me.Col_HeadPhi.Frozen = True
        Me.Col_HeadPhi.HeaderText = "Head Diameter (mm)"
        Me.Col_HeadPhi.Name = "Col_HeadPhi"
        Me.Col_HeadPhi.ReadOnly = True
        Me.Col_HeadPhi.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_HeadPhi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_HeadPhi.Width = 80
        '
        'Col_HeadDepth
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Col_HeadDepth.DefaultCellStyle = DataGridViewCellStyle4
        Me.Col_HeadDepth.Frozen = True
        Me.Col_HeadDepth.HeaderText = "Head Depth (mm)"
        Me.Col_HeadDepth.Name = "Col_HeadDepth"
        Me.Col_HeadDepth.ReadOnly = True
        Me.Col_HeadDepth.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_HeadDepth.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_HeadDepth.Width = 80
        '
        'Col_YieldStrength
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Col_YieldStrength.DefaultCellStyle = DataGridViewCellStyle5
        Me.Col_YieldStrength.Frozen = True
        Me.Col_YieldStrength.HeaderText = "Yielding Strength (mm)"
        Me.Col_YieldStrength.Name = "Col_YieldStrength"
        Me.Col_YieldStrength.ReadOnly = True
        Me.Col_YieldStrength.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_YieldStrength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_YieldStrength.Width = 80
        '
        'Col_UltimateStrength
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Col_UltimateStrength.DefaultCellStyle = DataGridViewCellStyle6
        Me.Col_UltimateStrength.Frozen = True
        Me.Col_UltimateStrength.HeaderText = "Ultimate Strength (MPa)"
        Me.Col_UltimateStrength.Name = "Col_UltimateStrength"
        Me.Col_UltimateStrength.ReadOnly = True
        Me.Col_UltimateStrength.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_UltimateStrength.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_UltimateStrength.Width = 80
        '
        'Col_Vide
        '
        Me.Col_Vide.Frozen = True
        Me.Col_Vide.HeaderText = ""
        Me.Col_Vide.Name = "Col_Vide"
        Me.Col_Vide.ReadOnly = True
        Me.Col_Vide.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'etq_test
        '
        Me.etq_test.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.etq_test.AutoSize = True
        Me.etq_test.Location = New System.Drawing.Point(54, 496)
        Me.etq_test.Name = "etq_test"
        Me.etq_test.Size = New System.Drawing.Size(39, 13)
        Me.etq_test.TabIndex = 14
        Me.etq_test.Text = "Label1"
        Me.etq_test.Visible = False
        '
        'btn_Cancel
        '
        Me.btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Cancel.Location = New System.Drawing.Point(576, 496)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(82, 24)
        Me.btn_Cancel.TabIndex = 16
        Me.btn_Cancel.Text = "btn_Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'btn_OK
        '
        Me.btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_OK.Location = New System.Drawing.Point(488, 496)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(82, 24)
        Me.btn_OK.TabIndex = 15
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'Frm_EditGoujons
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Cancel
        Me.ClientSize = New System.Drawing.Size(675, 530)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.etq_test)
        Me.Controls.Add(Me.Grid_Studs)
        Me.Controls.Add(Me.btn_Cancel)
        Me.Controls.Add(Me.btn_OK)
        Me.MaximumSize = New System.Drawing.Size(691, 800)
        Me.MinimumSize = New System.Drawing.Size(691, 500)
        Me.Name = "Frm_EditGoujons"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_EditGoujons"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.Grid_Studs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents MenuNouveau As ToolStripButton
    Friend WithEvents MenuModifier As ToolStripButton
    Friend WithEvents MenuSupprimer As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents MenuEnregistrerBase As ToolStripButton
    Friend WithEvents HelpProvider1 As HelpProvider
    Friend WithEvents Grid_Studs As DataGridView
    Friend WithEvents Col_Check As DataGridViewCheckBoxColumn
    Friend WithEvents Col_Ind As DataGridViewTextBoxColumn
    Friend WithEvents Col_Label As DataGridViewTextBoxColumn
    Friend WithEvents Col_Ht As DataGridViewTextBoxColumn
    Friend WithEvents Col_PhiRod As DataGridViewTextBoxColumn
    Friend WithEvents Col_HeadPhi As DataGridViewTextBoxColumn
    Friend WithEvents Col_HeadDepth As DataGridViewTextBoxColumn
    Friend WithEvents Col_YieldStrength As DataGridViewTextBoxColumn
    Friend WithEvents Col_UltimateStrength As DataGridViewTextBoxColumn
    Friend WithEvents Col_Vide As DataGridViewTextBoxColumn
    Friend WithEvents etq_test As Label
    Friend WithEvents btn_Cancel As Button
    Friend WithEvents btn_OK As Button
End Class
