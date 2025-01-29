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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.img_ecMax = New System.Windows.Forms.PictureBox()
        Me.txt_ecMax = New System.Windows.Forms.TextBox()
        Me.etq_UnitDim4 = New System.Windows.Forms.Label()
        Me.lbl_EntraxeCoutureMax = New System.Windows.Forms.Label()
        Me.lbl_MaintienBac = New System.Windows.Forms.Label()
        Me.pan_Dalles = New System.Windows.Forms.Panel()
        Me.img_bappmin = New System.Windows.Forms.PictureBox()
        Me.txt_bappmin = New System.Windows.Forms.TextBox()
        Me.lbl_UnitDim2 = New System.Windows.Forms.Label()
        Me.lbl_bappmin = New System.Windows.Forms.Label()
        Me.lbl_Dalles = New System.Windows.Forms.Label()
        Me.pan_SlimFloors = New System.Windows.Forms.Panel()
        Me.lbl_SlimFloor = New System.Windows.Forms.Label()
        Me.img_hslimmax = New System.Windows.Forms.PictureBox()
        Me.img_tpinfmin = New System.Windows.Forms.PictureBox()
        Me.txt_hslimmax = New System.Windows.Forms.TextBox()
        Me.txt_tpinfmin = New System.Windows.Forms.TextBox()
        Me.lbl_UnitDim = New System.Windows.Forms.Label()
        Me.lbl_UnitDim3 = New System.Windows.Forms.Label()
        Me.lbl_hslimmax = New System.Windows.Forms.Label()
        Me.lbl_tpinfmin = New System.Windows.Forms.Label()
        Me.lbl_Slimfloors = New System.Windows.Forms.Label()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Slimfloor.SuspendLayout()
        Me.TLpan_Conteneur.SuspendLayout()
        Me.pan_Conteneur.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.img_ecMax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Dalles.SuspendLayout()
        CType(Me.img_bappmin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_SlimFloors.SuspendLayout()
        CType(Me.img_hslimmax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_tpinfmin, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pan_Conteneur.Controls.Add(Me.Panel1)
        Me.pan_Conteneur.Controls.Add(Me.lbl_MaintienBac)
        Me.pan_Conteneur.Controls.Add(Me.pan_Dalles)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Dalles)
        Me.pan_Conteneur.Controls.Add(Me.pan_SlimFloors)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Slimfloors)
        Me.pan_Conteneur.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Conteneur.Location = New System.Drawing.Point(0, 0)
        Me.pan_Conteneur.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Conteneur.Name = "pan_Conteneur"
        Me.pan_Conteneur.Size = New System.Drawing.Size(739, 436)
        Me.pan_Conteneur.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.img_ecMax)
        Me.Panel1.Controls.Add(Me.txt_ecMax)
        Me.Panel1.Controls.Add(Me.etq_UnitDim4)
        Me.Panel1.Controls.Add(Me.lbl_EntraxeCoutureMax)
        Me.Panel1.Location = New System.Drawing.Point(4, 94)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(731, 35)
        Me.Panel1.TabIndex = 150
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(603, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(13, 13)
        Me.Label4.TabIndex = 154
        Me.Label4.Text = "≥"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'img_ecMax
        '
        Me.img_ecMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ecMax.Location = New System.Drawing.Point(556, 7)
        Me.img_ecMax.Name = "img_ecMax"
        Me.img_ecMax.Size = New System.Drawing.Size(46, 20)
        Me.img_ecMax.TabIndex = 109
        Me.img_ecMax.TabStop = False
        '
        'txt_ecMax
        '
        Me.txt_ecMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ecMax.Location = New System.Drawing.Point(619, 7)
        Me.txt_ecMax.Name = "txt_ecMax"
        Me.txt_ecMax.Size = New System.Drawing.Size(58, 20)
        Me.txt_ecMax.TabIndex = 108
        '
        'etq_UnitDim4
        '
        Me.etq_UnitDim4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim4.AutoSize = True
        Me.etq_UnitDim4.Location = New System.Drawing.Point(683, 10)
        Me.etq_UnitDim4.Name = "etq_UnitDim4"
        Me.etq_UnitDim4.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim4.TabIndex = 110
        Me.etq_UnitDim4.Text = "mm"
        '
        'lbl_EntraxeCoutureMax
        '
        Me.lbl_EntraxeCoutureMax.AutoSize = True
        Me.lbl_EntraxeCoutureMax.Location = New System.Drawing.Point(39, 10)
        Me.lbl_EntraxeCoutureMax.Name = "lbl_EntraxeCoutureMax"
        Me.lbl_EntraxeCoutureMax.Size = New System.Drawing.Size(116, 13)
        Me.lbl_EntraxeCoutureMax.TabIndex = 111
        Me.lbl_EntraxeCoutureMax.Text = "lbl_EntraxeCoutureMax"
        '
        'lbl_MaintienBac
        '
        Me.lbl_MaintienBac.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_MaintienBac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_MaintienBac.Location = New System.Drawing.Point(3, 68)
        Me.lbl_MaintienBac.Name = "lbl_MaintienBac"
        Me.lbl_MaintienBac.Size = New System.Drawing.Size(734, 23)
        Me.lbl_MaintienBac.TabIndex = 149
        Me.lbl_MaintienBac.Text = "lbl_MaintienBac"
        Me.lbl_MaintienBac.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Dalles
        '
        Me.pan_Dalles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Dalles.Controls.Add(Me.img_bappmin)
        Me.pan_Dalles.Controls.Add(Me.txt_bappmin)
        Me.pan_Dalles.Controls.Add(Me.lbl_UnitDim2)
        Me.pan_Dalles.Controls.Add(Me.lbl_bappmin)
        Me.pan_Dalles.Location = New System.Drawing.Point(4, 30)
        Me.pan_Dalles.Name = "pan_Dalles"
        Me.pan_Dalles.Size = New System.Drawing.Size(731, 35)
        Me.pan_Dalles.TabIndex = 148
        '
        'img_bappmin
        '
        Me.img_bappmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_bappmin.Location = New System.Drawing.Point(541, 7)
        Me.img_bappmin.Name = "img_bappmin"
        Me.img_bappmin.Size = New System.Drawing.Size(78, 20)
        Me.img_bappmin.TabIndex = 109
        Me.img_bappmin.TabStop = False
        '
        'txt_bappmin
        '
        Me.txt_bappmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_bappmin.Location = New System.Drawing.Point(619, 7)
        Me.txt_bappmin.Name = "txt_bappmin"
        Me.txt_bappmin.Size = New System.Drawing.Size(58, 20)
        Me.txt_bappmin.TabIndex = 108
        '
        'lbl_UnitDim2
        '
        Me.lbl_UnitDim2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_UnitDim2.AutoSize = True
        Me.lbl_UnitDim2.Location = New System.Drawing.Point(683, 10)
        Me.lbl_UnitDim2.Name = "lbl_UnitDim2"
        Me.lbl_UnitDim2.Size = New System.Drawing.Size(23, 13)
        Me.lbl_UnitDim2.TabIndex = 110
        Me.lbl_UnitDim2.Text = "mm"
        '
        'lbl_bappmin
        '
        Me.lbl_bappmin.AutoSize = True
        Me.lbl_bappmin.Location = New System.Drawing.Point(39, 10)
        Me.lbl_bappmin.Name = "lbl_bappmin"
        Me.lbl_bappmin.Size = New System.Drawing.Size(63, 13)
        Me.lbl_bappmin.TabIndex = 111
        Me.lbl_bappmin.Text = "lbl_bappmin"
        '
        'lbl_Dalles
        '
        Me.lbl_Dalles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Dalles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Dalles.Location = New System.Drawing.Point(3, 4)
        Me.lbl_Dalles.Name = "lbl_Dalles"
        Me.lbl_Dalles.Size = New System.Drawing.Size(734, 23)
        Me.lbl_Dalles.TabIndex = 147
        Me.lbl_Dalles.Text = "lbl_Dalles"
        Me.lbl_Dalles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SlimFloors
        '
        Me.pan_SlimFloors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_SlimFloors.Controls.Add(Me.lbl_SlimFloor)
        Me.pan_SlimFloors.Controls.Add(Me.img_hslimmax)
        Me.pan_SlimFloors.Controls.Add(Me.img_tpinfmin)
        Me.pan_SlimFloors.Controls.Add(Me.txt_hslimmax)
        Me.pan_SlimFloors.Controls.Add(Me.txt_tpinfmin)
        Me.pan_SlimFloors.Controls.Add(Me.lbl_UnitDim)
        Me.pan_SlimFloors.Controls.Add(Me.lbl_UnitDim3)
        Me.pan_SlimFloors.Controls.Add(Me.lbl_hslimmax)
        Me.pan_SlimFloors.Controls.Add(Me.lbl_tpinfmin)
        Me.pan_SlimFloors.Location = New System.Drawing.Point(4, 160)
        Me.pan_SlimFloors.Name = "pan_SlimFloors"
        Me.pan_SlimFloors.Size = New System.Drawing.Size(731, 87)
        Me.pan_SlimFloors.TabIndex = 146
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
        Me.img_tpinfmin.Location = New System.Drawing.Point(556, 53)
        Me.img_tpinfmin.Name = "img_tpinfmin"
        Me.img_tpinfmin.Size = New System.Drawing.Size(63, 20)
        Me.img_tpinfmin.TabIndex = 105
        Me.img_tpinfmin.TabStop = False
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
        Me.txt_tpinfmin.Location = New System.Drawing.Point(619, 53)
        Me.txt_tpinfmin.Name = "txt_tpinfmin"
        Me.txt_tpinfmin.Size = New System.Drawing.Size(58, 20)
        Me.txt_tpinfmin.TabIndex = 104
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
        Me.lbl_UnitDim3.Location = New System.Drawing.Point(683, 56)
        Me.lbl_UnitDim3.Name = "lbl_UnitDim3"
        Me.lbl_UnitDim3.Size = New System.Drawing.Size(23, 13)
        Me.lbl_UnitDim3.TabIndex = 106
        Me.lbl_UnitDim3.Text = "mm"
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
        Me.lbl_tpinfmin.Location = New System.Drawing.Point(39, 60)
        Me.lbl_tpinfmin.Name = "lbl_tpinfmin"
        Me.lbl_tpinfmin.Size = New System.Drawing.Size(59, 13)
        Me.lbl_tpinfmin.TabIndex = 107
        Me.lbl_tpinfmin.Text = "lbl_tpinfmin"
        '
        'lbl_Slimfloors
        '
        Me.lbl_Slimfloors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Slimfloors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Slimfloors.Location = New System.Drawing.Point(3, 134)
        Me.lbl_Slimfloors.Name = "lbl_Slimfloors"
        Me.lbl_Slimfloors.Size = New System.Drawing.Size(733, 23)
        Me.lbl_Slimfloors.TabIndex = 98
        Me.lbl_Slimfloors.Text = "lbl_Slimfloors"
        Me.lbl_Slimfloors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.img_ecMax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Dalles.ResumeLayout(False)
        Me.pan_Dalles.PerformLayout()
        CType(Me.img_bappmin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_SlimFloors.ResumeLayout(False)
        Me.pan_SlimFloors.PerformLayout()
        CType(Me.img_hslimmax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_tpinfmin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents pan_Slimfloor As Panel
    Friend WithEvents TLpan_Conteneur As TableLayoutPanel
    Friend WithEvents pan_Conteneur As Panel
    Friend WithEvents lbl_Slimfloors As Label
    Friend WithEvents pan_SlimFloors As Panel
    Friend WithEvents lbl_SlimFloor As Label
    Friend WithEvents img_hslimmax As PictureBox
    Friend WithEvents txt_hslimmax As TextBox
    Friend WithEvents lbl_UnitDim As Label
    Friend WithEvents lbl_hslimmax As Label
    Friend WithEvents img_tpinfmin As PictureBox
    Friend WithEvents txt_tpinfmin As TextBox
    Friend WithEvents lbl_UnitDim3 As Label
    Friend WithEvents lbl_tpinfmin As Label
    Friend WithEvents pan_Dalles As Panel
    Friend WithEvents lbl_Dalles As Label
    Friend WithEvents img_bappmin As PictureBox
    Friend WithEvents txt_bappmin As TextBox
    Friend WithEvents lbl_UnitDim2 As Label
    Friend WithEvents lbl_bappmin As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents img_ecMax As PictureBox
    Friend WithEvents txt_ecMax As TextBox
    Friend WithEvents etq_UnitDim4 As Label
    Friend WithEvents lbl_EntraxeCoutureMax As Label
    Friend WithEvents lbl_MaintienBac As Label
    Friend WithEvents Label4 As Label
End Class
