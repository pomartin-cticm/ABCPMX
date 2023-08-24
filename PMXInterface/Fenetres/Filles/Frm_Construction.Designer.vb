<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Construction
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pan_Interieur = New System.Windows.Forms.Panel()
        Me.lbl_Interieur = New System.Windows.Forms.Label()
        Me.cmb_DiaInterieur = New System.Windows.Forms.ComboBox()
        Me.cmb_NombreInterieur = New System.Windows.Forms.ComboBox()
        Me.pan_Mileu = New System.Windows.Forms.Panel()
        Me.lbl_Middle = New System.Windows.Forms.Label()
        Me.cmb_DiaMilieu = New System.Windows.Forms.ComboBox()
        Me.cmb_NombreMileu = New System.Windows.Forms.ComboBox()
        Me.pan_Exterieur = New System.Windows.Forms.Panel()
        Me.lbl_Exterieur = New System.Windows.Forms.Label()
        Me.cmb_NombreExt = New System.Windows.Forms.ComboBox()
        Me.cmb_DiaExt = New System.Windows.Forms.ComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.etq_UnitDim5 = New System.Windows.Forms.Label()
        Me.lbl_Nombre = New System.Windows.Forms.Label()
        Me.lbl_DiametreA = New System.Windows.Forms.Label()
        Me.img_PhiA = New System.Windows.Forms.PictureBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel1.SuspendLayout()
        Me.pan_Interieur.SuspendLayout()
        Me.pan_Mileu.SuspendLayout()
        Me.pan_Exterieur.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.img_PhiA, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel1.Controls.Add(Me.TabControl1)
        Me.Panel1.Controls.Add(Me.pan_Interieur)
        Me.Panel1.Controls.Add(Me.pan_Mileu)
        Me.Panel1.Controls.Add(Me.pan_Exterieur)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 450)
        Me.Panel1.TabIndex = 0
        '
        'pan_Interieur
        '
        Me.pan_Interieur.Controls.Add(Me.lbl_Interieur)
        Me.pan_Interieur.Controls.Add(Me.cmb_DiaInterieur)
        Me.pan_Interieur.Controls.Add(Me.cmb_NombreInterieur)
        Me.pan_Interieur.Location = New System.Drawing.Point(353, 225)
        Me.pan_Interieur.Name = "pan_Interieur"
        Me.pan_Interieur.Size = New System.Drawing.Size(87, 79)
        Me.pan_Interieur.TabIndex = 3
        '
        'lbl_Interieur
        '
        Me.lbl_Interieur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Interieur.Location = New System.Drawing.Point(3, 0)
        Me.lbl_Interieur.Name = "lbl_Interieur"
        Me.lbl_Interieur.Size = New System.Drawing.Size(80, 17)
        Me.lbl_Interieur.TabIndex = 25
        Me.lbl_Interieur.Text = "lbl_Interieur"
        Me.lbl_Interieur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmb_DiaInterieur
        '
        Me.cmb_DiaInterieur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_DiaInterieur.FormattingEnabled = True
        Me.cmb_DiaInterieur.Location = New System.Drawing.Point(3, 51)
        Me.cmb_DiaInterieur.Name = "cmb_DiaInterieur"
        Me.cmb_DiaInterieur.Size = New System.Drawing.Size(80, 21)
        Me.cmb_DiaInterieur.TabIndex = 63
        '
        'cmb_NombreInterieur
        '
        Me.cmb_NombreInterieur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NombreInterieur.FormattingEnabled = True
        Me.cmb_NombreInterieur.Location = New System.Drawing.Point(3, 24)
        Me.cmb_NombreInterieur.Name = "cmb_NombreInterieur"
        Me.cmb_NombreInterieur.Size = New System.Drawing.Size(80, 21)
        Me.cmb_NombreInterieur.TabIndex = 62
        '
        'pan_Mileu
        '
        Me.pan_Mileu.Controls.Add(Me.lbl_Middle)
        Me.pan_Mileu.Controls.Add(Me.cmb_DiaMilieu)
        Me.pan_Mileu.Controls.Add(Me.cmb_NombreMileu)
        Me.pan_Mileu.Location = New System.Drawing.Point(264, 225)
        Me.pan_Mileu.Name = "pan_Mileu"
        Me.pan_Mileu.Size = New System.Drawing.Size(87, 79)
        Me.pan_Mileu.TabIndex = 2
        '
        'lbl_Middle
        '
        Me.lbl_Middle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Middle.Location = New System.Drawing.Point(3, 0)
        Me.lbl_Middle.Name = "lbl_Middle"
        Me.lbl_Middle.Size = New System.Drawing.Size(80, 17)
        Me.lbl_Middle.TabIndex = 24
        Me.lbl_Middle.Text = "lbl_Middle"
        Me.lbl_Middle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmb_DiaMilieu
        '
        Me.cmb_DiaMilieu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_DiaMilieu.FormattingEnabled = True
        Me.cmb_DiaMilieu.Location = New System.Drawing.Point(3, 51)
        Me.cmb_DiaMilieu.Name = "cmb_DiaMilieu"
        Me.cmb_DiaMilieu.Size = New System.Drawing.Size(80, 21)
        Me.cmb_DiaMilieu.TabIndex = 61
        '
        'cmb_NombreMileu
        '
        Me.cmb_NombreMileu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NombreMileu.FormattingEnabled = True
        Me.cmb_NombreMileu.Location = New System.Drawing.Point(3, 24)
        Me.cmb_NombreMileu.Name = "cmb_NombreMileu"
        Me.cmb_NombreMileu.Size = New System.Drawing.Size(80, 21)
        Me.cmb_NombreMileu.TabIndex = 60
        '
        'pan_Exterieur
        '
        Me.pan_Exterieur.Controls.Add(Me.lbl_Exterieur)
        Me.pan_Exterieur.Controls.Add(Me.cmb_NombreExt)
        Me.pan_Exterieur.Controls.Add(Me.cmb_DiaExt)
        Me.pan_Exterieur.Location = New System.Drawing.Point(175, 225)
        Me.pan_Exterieur.Name = "pan_Exterieur"
        Me.pan_Exterieur.Size = New System.Drawing.Size(87, 79)
        Me.pan_Exterieur.TabIndex = 1
        '
        'lbl_Exterieur
        '
        Me.lbl_Exterieur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Exterieur.Location = New System.Drawing.Point(3, 0)
        Me.lbl_Exterieur.Name = "lbl_Exterieur"
        Me.lbl_Exterieur.Size = New System.Drawing.Size(80, 17)
        Me.lbl_Exterieur.TabIndex = 23
        Me.lbl_Exterieur.Text = "lbl_Exterieur"
        Me.lbl_Exterieur.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmb_NombreExt
        '
        Me.cmb_NombreExt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NombreExt.FormattingEnabled = True
        Me.cmb_NombreExt.Location = New System.Drawing.Point(3, 24)
        Me.cmb_NombreExt.Name = "cmb_NombreExt"
        Me.cmb_NombreExt.Size = New System.Drawing.Size(80, 21)
        Me.cmb_NombreExt.TabIndex = 57
        '
        'cmb_DiaExt
        '
        Me.cmb_DiaExt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_DiaExt.FormattingEnabled = True
        Me.cmb_DiaExt.Location = New System.Drawing.Point(3, 51)
        Me.cmb_DiaExt.Name = "cmb_DiaExt"
        Me.cmb_DiaExt.Size = New System.Drawing.Size(80, 21)
        Me.cmb_DiaExt.TabIndex = 59
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.etq_UnitDim5)
        Me.Panel2.Controls.Add(Me.lbl_Nombre)
        Me.Panel2.Controls.Add(Me.lbl_DiametreA)
        Me.Panel2.Controls.Add(Me.img_PhiA)
        Me.Panel2.Location = New System.Drawing.Point(67, 67)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(422, 121)
        Me.Panel2.TabIndex = 0
        '
        'etq_UnitDim5
        '
        Me.etq_UnitDim5.AutoSize = True
        Me.etq_UnitDim5.Location = New System.Drawing.Point(377, 78)
        Me.etq_UnitDim5.Name = "etq_UnitDim5"
        Me.etq_UnitDim5.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim5.TabIndex = 64
        Me.etq_UnitDim5.Text = "mm"
        '
        'lbl_Nombre
        '
        Me.lbl_Nombre.AutoSize = True
        Me.lbl_Nombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Nombre.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Nombre.Location = New System.Drawing.Point(19, 51)
        Me.lbl_Nombre.Name = "lbl_Nombre"
        Me.lbl_Nombre.Size = New System.Drawing.Size(60, 13)
        Me.lbl_Nombre.TabIndex = 58
        Me.lbl_Nombre.Text = "lbl_Nombre"
        Me.lbl_Nombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_DiametreA
        '
        Me.lbl_DiametreA.AutoSize = True
        Me.lbl_DiametreA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DiametreA.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_DiametreA.Location = New System.Drawing.Point(19, 78)
        Me.lbl_DiametreA.Name = "lbl_DiametreA"
        Me.lbl_DiametreA.Size = New System.Drawing.Size(65, 13)
        Me.lbl_DiametreA.TabIndex = 56
        Me.lbl_DiametreA.Text = "lbl_Diametre"
        Me.lbl_DiametreA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_PhiA
        '
        Me.img_PhiA.Location = New System.Drawing.Point(82, 76)
        Me.img_PhiA.Name = "img_PhiA"
        Me.img_PhiA.Size = New System.Drawing.Size(37, 20)
        Me.img_PhiA.TabIndex = 65
        Me.img_PhiA.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(567, 164)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(159, 192)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(151, 166)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(151, 166)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Frm_Construction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Construction"
        Me.Text = "Frm_Construction"
        Me.Panel1.ResumeLayout(False)
        Me.pan_Interieur.ResumeLayout(False)
        Me.pan_Mileu.ResumeLayout(False)
        Me.pan_Exterieur.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.img_PhiA, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lbl_Interieur As Label
    Friend WithEvents lbl_Middle As Label
    Friend WithEvents lbl_Exterieur As Label
    Friend WithEvents cmb_DiaInterieur As ComboBox
    Friend WithEvents cmb_NombreInterieur As ComboBox
    Friend WithEvents cmb_DiaMilieu As ComboBox
    Friend WithEvents cmb_NombreMileu As ComboBox
    Friend WithEvents cmb_DiaExt As ComboBox
    Friend WithEvents lbl_Nombre As Label
    Friend WithEvents cmb_NombreExt As ComboBox
    Friend WithEvents lbl_DiametreA As Label
    Friend WithEvents etq_UnitDim5 As Label
    Friend WithEvents img_PhiA As PictureBox
    Friend WithEvents pan_Interieur As Panel
    Friend WithEvents pan_Mileu As Panel
    Friend WithEvents pan_Exterieur As Panel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
End Class
