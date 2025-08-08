<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_DalleSlimFloorNArma
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
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txt_Fsk = New System.Windows.Forms.TextBox()
        Me.img_Fy = New System.Windows.Forms.PictureBox()
        Me.etq_UnitSigma2 = New System.Windows.Forms.Label()
        Me.cmb_Acier = New System.Windows.Forms.ComboBox()
        Me.lbl_ClasseA = New System.Windows.Forms.Label()
        Me.lbl_Acier = New System.Windows.Forms.Label()
        Me.lbl_Arma = New System.Windows.Forms.Label()
        Me.pan_Arma = New System.Windows.Forms.Panel()
        Me.etq_UnitDim3 = New System.Windows.Forms.Label()
        Me.txt_ArmaY = New System.Windows.Forms.TextBox()
        Me.etq_UnitDim2 = New System.Windows.Forms.Label()
        Me.img_ArmaY = New System.Windows.Forms.PictureBox()
        Me.txt_ArmaX = New System.Windows.Forms.TextBox()
        Me.etq_UnitDim1 = New System.Windows.Forms.Label()
        Me.img_ArmaX = New System.Windows.Forms.PictureBox()
        Me.lbl_Location = New System.Windows.Forms.Label()
        Me.lbl_Diameter = New System.Windows.Forms.Label()
        Me.cmb_Diametre = New System.Windows.Forms.ComboBox()
        Me.lbl_Number = New System.Windows.Forms.Label()
        Me.cmb_Nombre = New System.Windows.Forms.ComboBox()
        Me.chk_Rebars = New System.Windows.Forms.CheckBox()
        Me.lbl_Avertissement = New System.Windows.Forms.Label()
        Me.pan_Option = New System.Windows.Forms.Panel()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.img_Fy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Arma.SuspendLayout()
        CType(Me.img_ArmaY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ArmaX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Option.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.Controls.Add(Me.TLpan_Main)
        Me.pan_Main.Location = New System.Drawing.Point(111, 148)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(359, 431)
        Me.pan_Main.TabIndex = 0
        '
        'TLpan_Main
        '
        Me.TLpan_Main.ColumnCount = 1
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Controls.Add(Me.Panel2, 0, 3)
        Me.TLpan_Main.Controls.Add(Me.lbl_Acier, 0, 2)
        Me.TLpan_Main.Controls.Add(Me.lbl_Arma, 0, 0)
        Me.TLpan_Main.Controls.Add(Me.pan_Arma, 0, 1)
        Me.TLpan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 4
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(359, 431)
        Me.TLpan_Main.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.txt_Fsk)
        Me.Panel2.Controls.Add(Me.img_Fy)
        Me.Panel2.Controls.Add(Me.etq_UnitSigma2)
        Me.Panel2.Controls.Add(Me.cmb_Acier)
        Me.Panel2.Controls.Add(Me.lbl_ClasseA)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 351)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(359, 80)
        Me.Panel2.TabIndex = 16
        '
        'txt_Fsk
        '
        Me.txt_Fsk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Fsk.Location = New System.Drawing.Point(257, 38)
        Me.txt_Fsk.Name = "txt_Fsk"
        Me.txt_Fsk.Size = New System.Drawing.Size(58, 20)
        Me.txt_Fsk.TabIndex = 59
        '
        'img_Fy
        '
        Me.img_Fy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Fy.Location = New System.Drawing.Point(201, 38)
        Me.img_Fy.Name = "img_Fy"
        Me.img_Fy.Size = New System.Drawing.Size(57, 20)
        Me.img_Fy.TabIndex = 60
        Me.img_Fy.TabStop = False
        '
        'etq_UnitSigma2
        '
        Me.etq_UnitSigma2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitSigma2.AutoSize = True
        Me.etq_UnitSigma2.Location = New System.Drawing.Point(321, 41)
        Me.etq_UnitSigma2.Name = "etq_UnitSigma2"
        Me.etq_UnitSigma2.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitSigma2.TabIndex = 58
        Me.etq_UnitSigma2.Text = "mm"
        '
        'cmb_Acier
        '
        Me.cmb_Acier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Acier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Acier.FormattingEnabled = True
        Me.cmb_Acier.Location = New System.Drawing.Point(127, 11)
        Me.cmb_Acier.Name = "cmb_Acier"
        Me.cmb_Acier.Size = New System.Drawing.Size(220, 21)
        Me.cmb_Acier.TabIndex = 57
        '
        'lbl_ClasseA
        '
        Me.lbl_ClasseA.AutoSize = True
        Me.lbl_ClasseA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ClasseA.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_ClasseA.Location = New System.Drawing.Point(11, 14)
        Me.lbl_ClasseA.Name = "lbl_ClasseA"
        Me.lbl_ClasseA.Size = New System.Drawing.Size(61, 13)
        Me.lbl_ClasseA.TabIndex = 56
        Me.lbl_ClasseA.Text = "lbl_ClasseA"
        Me.lbl_ClasseA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Acier
        '
        Me.lbl_Acier.AutoSize = True
        Me.lbl_Acier.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Acier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Acier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Acier.Location = New System.Drawing.Point(0, 321)
        Me.lbl_Acier.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Acier.Name = "lbl_Acier"
        Me.lbl_Acier.Size = New System.Drawing.Size(359, 30)
        Me.lbl_Acier.TabIndex = 4
        Me.lbl_Acier.Text = "lbl_Acier"
        Me.lbl_Acier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Arma
        '
        Me.lbl_Arma.AutoSize = True
        Me.lbl_Arma.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Arma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Arma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Arma.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Arma.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Arma.Name = "lbl_Arma"
        Me.lbl_Arma.Size = New System.Drawing.Size(359, 30)
        Me.lbl_Arma.TabIndex = 3
        Me.lbl_Arma.Text = "lbl_Arma"
        Me.lbl_Arma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Arma
        '
        Me.pan_Arma.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Arma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Arma.Controls.Add(Me.pan_Option)
        Me.pan_Arma.Controls.Add(Me.etq_UnitDim3)
        Me.pan_Arma.Controls.Add(Me.txt_ArmaY)
        Me.pan_Arma.Controls.Add(Me.etq_UnitDim2)
        Me.pan_Arma.Controls.Add(Me.img_ArmaY)
        Me.pan_Arma.Controls.Add(Me.txt_ArmaX)
        Me.pan_Arma.Controls.Add(Me.etq_UnitDim1)
        Me.pan_Arma.Controls.Add(Me.img_ArmaX)
        Me.pan_Arma.Controls.Add(Me.lbl_Location)
        Me.pan_Arma.Controls.Add(Me.lbl_Diameter)
        Me.pan_Arma.Controls.Add(Me.cmb_Diametre)
        Me.pan_Arma.Controls.Add(Me.lbl_Number)
        Me.pan_Arma.Controls.Add(Me.cmb_Nombre)
        Me.pan_Arma.Controls.Add(Me.lbl_Avertissement)
        Me.pan_Arma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Arma.Location = New System.Drawing.Point(0, 30)
        Me.pan_Arma.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_Arma.Name = "pan_Arma"
        Me.pan_Arma.Size = New System.Drawing.Size(359, 290)
        Me.pan_Arma.TabIndex = 5
        '
        'etq_UnitDim3
        '
        Me.etq_UnitDim3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim3.AutoSize = True
        Me.etq_UnitDim3.Location = New System.Drawing.Point(321, 96)
        Me.etq_UnitDim3.Name = "etq_UnitDim3"
        Me.etq_UnitDim3.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim3.TabIndex = 92
        Me.etq_UnitDim3.Text = "mm"
        Me.etq_UnitDim3.Visible = False
        '
        'txt_ArmaY
        '
        Me.txt_ArmaY.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ArmaY.Location = New System.Drawing.Point(244, 155)
        Me.txt_ArmaY.Name = "txt_ArmaY"
        Me.txt_ArmaY.Size = New System.Drawing.Size(58, 20)
        Me.txt_ArmaY.TabIndex = 90
        '
        'etq_UnitDim2
        '
        Me.etq_UnitDim2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim2.AutoSize = True
        Me.etq_UnitDim2.Location = New System.Drawing.Point(307, 158)
        Me.etq_UnitDim2.Name = "etq_UnitDim2"
        Me.etq_UnitDim2.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim2.TabIndex = 89
        Me.etq_UnitDim2.Text = "mm"
        '
        'img_ArmaY
        '
        Me.img_ArmaY.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ArmaY.Location = New System.Drawing.Point(207, 155)
        Me.img_ArmaY.Name = "img_ArmaY"
        Me.img_ArmaY.Size = New System.Drawing.Size(37, 20)
        Me.img_ArmaY.TabIndex = 91
        Me.img_ArmaY.TabStop = False
        '
        'txt_ArmaX
        '
        Me.txt_ArmaX.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ArmaX.Location = New System.Drawing.Point(244, 129)
        Me.txt_ArmaX.Name = "txt_ArmaX"
        Me.txt_ArmaX.Size = New System.Drawing.Size(58, 20)
        Me.txt_ArmaX.TabIndex = 87
        '
        'etq_UnitDim1
        '
        Me.etq_UnitDim1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim1.AutoSize = True
        Me.etq_UnitDim1.Location = New System.Drawing.Point(307, 132)
        Me.etq_UnitDim1.Name = "etq_UnitDim1"
        Me.etq_UnitDim1.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim1.TabIndex = 86
        Me.etq_UnitDim1.Text = "mm"
        '
        'img_ArmaX
        '
        Me.img_ArmaX.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ArmaX.Location = New System.Drawing.Point(207, 129)
        Me.img_ArmaX.Name = "img_ArmaX"
        Me.img_ArmaX.Size = New System.Drawing.Size(37, 20)
        Me.img_ArmaX.TabIndex = 88
        Me.img_ArmaX.TabStop = False
        '
        'lbl_Location
        '
        Me.lbl_Location.AutoSize = True
        Me.lbl_Location.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Location.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Location.Location = New System.Drawing.Point(15, 129)
        Me.lbl_Location.Name = "lbl_Location"
        Me.lbl_Location.Size = New System.Drawing.Size(64, 13)
        Me.lbl_Location.TabIndex = 61
        Me.lbl_Location.Text = "lbl_Location"
        Me.lbl_Location.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Diameter
        '
        Me.lbl_Diameter.AutoSize = True
        Me.lbl_Diameter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Diameter.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Diameter.Location = New System.Drawing.Point(15, 96)
        Me.lbl_Diameter.Name = "lbl_Diameter"
        Me.lbl_Diameter.Size = New System.Drawing.Size(65, 13)
        Me.lbl_Diameter.TabIndex = 59
        Me.lbl_Diameter.Text = "lbl_Diameter"
        Me.lbl_Diameter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmb_Diametre
        '
        Me.cmb_Diametre.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Diametre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Diametre.FormattingEnabled = True
        Me.cmb_Diametre.Location = New System.Drawing.Point(222, 93)
        Me.cmb_Diametre.Name = "cmb_Diametre"
        Me.cmb_Diametre.Size = New System.Drawing.Size(93, 21)
        Me.cmb_Diametre.TabIndex = 60
        '
        'lbl_Number
        '
        Me.lbl_Number.AutoSize = True
        Me.lbl_Number.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Number.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Number.Location = New System.Drawing.Point(15, 68)
        Me.lbl_Number.Name = "lbl_Number"
        Me.lbl_Number.Size = New System.Drawing.Size(60, 13)
        Me.lbl_Number.TabIndex = 57
        Me.lbl_Number.Text = "lbl_Number"
        Me.lbl_Number.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmb_Nombre
        '
        Me.cmb_Nombre.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Nombre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Nombre.FormattingEnabled = True
        Me.cmb_Nombre.Location = New System.Drawing.Point(222, 66)
        Me.cmb_Nombre.Name = "cmb_Nombre"
        Me.cmb_Nombre.Size = New System.Drawing.Size(93, 21)
        Me.cmb_Nombre.TabIndex = 58
        '
        'chk_Rebars
        '
        Me.chk_Rebars.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chk_Rebars.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chk_Rebars.Location = New System.Drawing.Point(3, 3)
        Me.chk_Rebars.Name = "chk_Rebars"
        Me.chk_Rebars.Size = New System.Drawing.Size(339, 42)
        Me.chk_Rebars.TabIndex = 1
        Me.chk_Rebars.Text = "chk_Rebars"
        Me.chk_Rebars.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chk_Rebars.UseVisualStyleBackColor = True
        '
        'lbl_Avertissement
        '
        Me.lbl_Avertissement.AutoSize = True
        Me.lbl_Avertissement.Location = New System.Drawing.Point(11, 417)
        Me.lbl_Avertissement.Name = "lbl_Avertissement"
        Me.lbl_Avertissement.Size = New System.Drawing.Size(89, 13)
        Me.lbl_Avertissement.TabIndex = 0
        Me.lbl_Avertissement.Text = "lbl_Avertissement"
        '
        'pan_Option
        '
        Me.pan_Option.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Option.Controls.Add(Me.chk_Rebars)
        Me.pan_Option.Location = New System.Drawing.Point(3, 3)
        Me.pan_Option.Name = "pan_Option"
        Me.pan_Option.Size = New System.Drawing.Size(351, 52)
        Me.pan_Option.TabIndex = 93
        '
        'Frm_DalleSlimFloorNArma
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(968, 793)
        Me.Controls.Add(Me.pan_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_DalleSlimFloorNArma"
        Me.Text = "Frm_DalleSlimFloorNArma"
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLpan_Main.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.img_Fy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Arma.ResumeLayout(False)
        Me.pan_Arma.PerformLayout()
        CType(Me.img_ArmaY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ArmaX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Option.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents lbl_Acier As Label
    Friend WithEvents lbl_Arma As Label
    Friend WithEvents pan_Arma As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txt_Fsk As TextBox
    Friend WithEvents img_Fy As PictureBox
    Friend WithEvents etq_UnitSigma2 As Label
    Friend WithEvents cmb_Acier As ComboBox
    Friend WithEvents lbl_ClasseA As Label
    Friend WithEvents lbl_Avertissement As Label
    Friend WithEvents chk_Rebars As CheckBox
    Friend WithEvents lbl_Number As Label
    Friend WithEvents cmb_Nombre As ComboBox
    Friend WithEvents lbl_Diameter As Label
    Friend WithEvents cmb_Diametre As ComboBox
    Friend WithEvents lbl_Location As Label
    Friend WithEvents txt_ArmaY As TextBox
    Friend WithEvents etq_UnitDim2 As Label
    Friend WithEvents img_ArmaY As PictureBox
    Friend WithEvents txt_ArmaX As TextBox
    Friend WithEvents etq_UnitDim1 As Label
    Friend WithEvents img_ArmaX As PictureBox
    Friend WithEvents etq_UnitDim3 As Label
    Friend WithEvents pan_Option As Panel
End Class
