<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsCalculSlimFloor
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
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_Slimfloor = New System.Windows.Forms.Panel()
        Me.TLpan_Conteneur = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Conteneur = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbl_SlimFloor = New System.Windows.Forms.Label()
        Me.img_hslimmax = New System.Windows.Forms.PictureBox()
        Me.img_tpinfmin = New System.Windows.Forms.PictureBox()
        Me.img_bappmin = New System.Windows.Forms.PictureBox()
        Me.txt_hslimmax = New System.Windows.Forms.TextBox()
        Me.txt_tpinfmin = New System.Windows.Forms.TextBox()
        Me.txt_bappmin = New System.Windows.Forms.TextBox()
        Me.lbl_UnitDim = New System.Windows.Forms.Label()
        Me.lbl_UnitDim3 = New System.Windows.Forms.Label()
        Me.lbl_UnitDim2 = New System.Windows.Forms.Label()
        Me.lbl_hslimmax = New System.Windows.Forms.Label()
        Me.lbl_tpinfmin = New System.Windows.Forms.Label()
        Me.lbl_bappmin = New System.Windows.Forms.Label()
        Me.lbl_Scope = New System.Windows.Forms.Label()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Slimfloor.SuspendLayout()
        Me.TLpan_Conteneur.SuspendLayout()
        Me.pan_Conteneur.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.img_hslimmax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_tpinfmin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_bappmin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'pan_Slimfloor
        '
        Me.pan_Slimfloor.AutoScroll = True
        Me.pan_Slimfloor.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Slimfloor.Controls.Add(Me.TLpan_Conteneur)
        Me.pan_Slimfloor.Location = New System.Drawing.Point(30, 29)
        Me.pan_Slimfloor.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Slimfloor.Name = "pan_Slimfloor"
        Me.pan_Slimfloor.Size = New System.Drawing.Size(739, 472)
        Me.pan_Slimfloor.TabIndex = 2
        '
        'TLpan_Conteneur
        '
        Me.TLpan_Conteneur.ColumnCount = 1
        Me.TLpan_Conteneur.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_Conteneur.Controls.Add(Me.pan_Conteneur, 0, 0)
        Me.TLpan_Conteneur.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLpan_Conteneur.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Conteneur.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Conteneur.Name = "TLpan_Conteneur"
        Me.TLpan_Conteneur.RowCount = 1
        Me.TLpan_Conteneur.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_Conteneur.Size = New System.Drawing.Size(739, 436)
        Me.TLpan_Conteneur.TabIndex = 0
        '
        'pan_Conteneur
        '
        Me.pan_Conteneur.Controls.Add(Me.Panel2)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Scope)
        Me.pan_Conteneur.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Conteneur.Location = New System.Drawing.Point(0, 0)
        Me.pan_Conteneur.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Conteneur.Name = "pan_Conteneur"
        Me.pan_Conteneur.Size = New System.Drawing.Size(739, 436)
        Me.pan_Conteneur.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.lbl_SlimFloor)
        Me.Panel2.Controls.Add(Me.img_hslimmax)
        Me.Panel2.Controls.Add(Me.img_tpinfmin)
        Me.Panel2.Controls.Add(Me.img_bappmin)
        Me.Panel2.Controls.Add(Me.txt_hslimmax)
        Me.Panel2.Controls.Add(Me.txt_tpinfmin)
        Me.Panel2.Controls.Add(Me.txt_bappmin)
        Me.Panel2.Controls.Add(Me.lbl_UnitDim)
        Me.Panel2.Controls.Add(Me.lbl_UnitDim3)
        Me.Panel2.Controls.Add(Me.lbl_UnitDim2)
        Me.Panel2.Controls.Add(Me.lbl_hslimmax)
        Me.Panel2.Controls.Add(Me.lbl_tpinfmin)
        Me.Panel2.Controls.Add(Me.lbl_bappmin)
        Me.Panel2.Location = New System.Drawing.Point(4, 32)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(731, 133)
        Me.Panel2.TabIndex = 146
        '
        'lbl_SlimFloor
        '
        Me.lbl_SlimFloor.AutoSize = True
        Me.lbl_SlimFloor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_SlimFloor.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_SlimFloor.Location = New System.Drawing.Point(5, 6)
        Me.lbl_SlimFloor.Name = "lbl_SlimFloor"
        Me.lbl_SlimFloor.Size = New System.Drawing.Size(65, 13)
        Me.lbl_SlimFloor.TabIndex = 126
        Me.lbl_SlimFloor.Text = "lbl_SlimFloor"
        Me.lbl_SlimFloor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_hslimmax
        '
        Me.img_hslimmax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_hslimmax.Location = New System.Drawing.Point(556, 27)
        Me.img_hslimmax.Name = "img_hslimmax"
        Me.img_hslimmax.Size = New System.Drawing.Size(63, 20)
        Me.img_hslimmax.TabIndex = 105
        Me.img_hslimmax.TabStop = False
        '
        'img_tpinfmin
        '
        Me.img_tpinfmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_tpinfmin.Location = New System.Drawing.Point(556, 85)
        Me.img_tpinfmin.Name = "img_tpinfmin"
        Me.img_tpinfmin.Size = New System.Drawing.Size(63, 20)
        Me.img_tpinfmin.TabIndex = 105
        Me.img_tpinfmin.TabStop = False
        '
        'img_bappmin
        '
        Me.img_bappmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_bappmin.Location = New System.Drawing.Point(556, 56)
        Me.img_bappmin.Name = "img_bappmin"
        Me.img_bappmin.Size = New System.Drawing.Size(63, 20)
        Me.img_bappmin.TabIndex = 105
        Me.img_bappmin.TabStop = False
        '
        'txt_hslimmax
        '
        Me.txt_hslimmax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_hslimmax.Location = New System.Drawing.Point(619, 27)
        Me.txt_hslimmax.Name = "txt_hslimmax"
        Me.txt_hslimmax.Size = New System.Drawing.Size(58, 20)
        Me.txt_hslimmax.TabIndex = 104
        '
        'txt_tpinfmin
        '
        Me.txt_tpinfmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_tpinfmin.Location = New System.Drawing.Point(619, 85)
        Me.txt_tpinfmin.Name = "txt_tpinfmin"
        Me.txt_tpinfmin.Size = New System.Drawing.Size(58, 20)
        Me.txt_tpinfmin.TabIndex = 104
        '
        'txt_bappmin
        '
        Me.txt_bappmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_bappmin.Location = New System.Drawing.Point(619, 56)
        Me.txt_bappmin.Name = "txt_bappmin"
        Me.txt_bappmin.Size = New System.Drawing.Size(58, 20)
        Me.txt_bappmin.TabIndex = 104
        '
        'lbl_UnitDim
        '
        Me.lbl_UnitDim.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitDim.AutoSize = True
        Me.lbl_UnitDim.Location = New System.Drawing.Point(683, 30)
        Me.lbl_UnitDim.Name = "lbl_UnitDim"
        Me.lbl_UnitDim.Size = New System.Drawing.Size(23, 13)
        Me.lbl_UnitDim.TabIndex = 106
        Me.lbl_UnitDim.Text = "mm"
        '
        'lbl_UnitDim3
        '
        Me.lbl_UnitDim3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitDim3.AutoSize = True
        Me.lbl_UnitDim3.Location = New System.Drawing.Point(683, 88)
        Me.lbl_UnitDim3.Name = "lbl_UnitDim3"
        Me.lbl_UnitDim3.Size = New System.Drawing.Size(23, 13)
        Me.lbl_UnitDim3.TabIndex = 106
        Me.lbl_UnitDim3.Text = "mm"
        '
        'lbl_UnitDim2
        '
        Me.lbl_UnitDim2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitDim2.AutoSize = True
        Me.lbl_UnitDim2.Location = New System.Drawing.Point(683, 59)
        Me.lbl_UnitDim2.Name = "lbl_UnitDim2"
        Me.lbl_UnitDim2.Size = New System.Drawing.Size(23, 13)
        Me.lbl_UnitDim2.TabIndex = 106
        Me.lbl_UnitDim2.Text = "mm"
        '
        'lbl_hslimmax
        '
        Me.lbl_hslimmax.AutoSize = True
        Me.lbl_hslimmax.Location = New System.Drawing.Point(39, 34)
        Me.lbl_hslimmax.Name = "lbl_hslimmax"
        Me.lbl_hslimmax.Size = New System.Drawing.Size(65, 13)
        Me.lbl_hslimmax.TabIndex = 107
        Me.lbl_hslimmax.Text = "lbl_hslimmax"
        '
        'lbl_tpinfmin
        '
        Me.lbl_tpinfmin.AutoSize = True
        Me.lbl_tpinfmin.Location = New System.Drawing.Point(39, 92)
        Me.lbl_tpinfmin.Name = "lbl_tpinfmin"
        Me.lbl_tpinfmin.Size = New System.Drawing.Size(59, 13)
        Me.lbl_tpinfmin.TabIndex = 107
        Me.lbl_tpinfmin.Text = "lbl_tpinfmin"
        '
        'lbl_bappmin
        '
        Me.lbl_bappmin.AutoSize = True
        Me.lbl_bappmin.Location = New System.Drawing.Point(39, 63)
        Me.lbl_bappmin.Name = "lbl_bappmin"
        Me.lbl_bappmin.Size = New System.Drawing.Size(63, 13)
        Me.lbl_bappmin.TabIndex = 107
        Me.lbl_bappmin.Text = "lbl_bappmin"
        '
        'lbl_Scope
        '
        Me.lbl_Scope.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Scope.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Scope.Location = New System.Drawing.Point(3, 2)
        Me.lbl_Scope.Name = "lbl_Scope"
        Me.lbl_Scope.Size = New System.Drawing.Size(733, 23)
        Me.lbl_Scope.TabIndex = 98
        Me.lbl_Scope.Text = "lbl_Scope"
        Me.lbl_Scope.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_OptionsCalculSlimFloor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(853, 546)
        Me.Controls.Add(Me.pan_Slimfloor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsCalculSlimFloor"
        Me.Text = "Frm_OptionsCalculSlimFloor"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Slimfloor.ResumeLayout(False)
        Me.TLpan_Conteneur.ResumeLayout(False)
        Me.pan_Conteneur.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.img_hslimmax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_tpinfmin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_bappmin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents pan_Slimfloor As Panel
    Friend WithEvents TLpan_Conteneur As TableLayoutPanel
    Friend WithEvents pan_Conteneur As Panel
    Friend WithEvents lbl_Scope As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lbl_SlimFloor As Label
    Friend WithEvents img_bappmin As PictureBox
    Friend WithEvents txt_bappmin As TextBox
    Friend WithEvents lbl_UnitDim2 As Label
    Friend WithEvents lbl_bappmin As Label
    Friend WithEvents img_hslimmax As PictureBox
    Friend WithEvents txt_hslimmax As TextBox
    Friend WithEvents lbl_UnitDim As Label
    Friend WithEvents lbl_hslimmax As Label
    Friend WithEvents img_tpinfmin As PictureBox
    Friend WithEvents txt_tpinfmin As TextBox
    Friend WithEvents lbl_UnitDim3 As Label
    Friend WithEvents lbl_tpinfmin As Label
End Class
