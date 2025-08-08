<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_DalleSlimFloorNBac
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
        Me.TLpan_Milieu = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Bac = New System.Windows.Forms.Panel()
        Me.txt_Hp = New System.Windows.Forms.TextBox()
        Me.lbl_HauteurHp = New System.Windows.Forms.Label()
        Me.etq_UnitDim4 = New System.Windows.Forms.Label()
        Me.img_Hp = New System.Windows.Forms.PictureBox()
        Me.img_Bac = New System.Windows.Forms.PictureBox()
        Me.btn_ModifierBac = New System.Windows.Forms.Button()
        Me.lbl_BacNom = New System.Windows.Forms.Label()
        Me.txt_BacNom = New System.Windows.Forms.TextBox()
        Me.lbl_Bac = New System.Windows.Forms.Label()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Milieu.SuspendLayout()
        Me.pan_Bac.SuspendLayout()
        CType(Me.img_Hp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Bac, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.Controls.Add(Me.TLpan_Milieu)
        Me.pan_Main.Location = New System.Drawing.Point(111, 94)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(415, 496)
        Me.pan_Main.TabIndex = 0
        '
        'TLpan_Milieu
        '
        Me.TLpan_Milieu.ColumnCount = 1
        Me.TLpan_Milieu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Milieu.Controls.Add(Me.pan_Bac, 0, 1)
        Me.TLpan_Milieu.Controls.Add(Me.lbl_Bac, 0, 0)
        Me.TLpan_Milieu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Milieu.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Milieu.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.TLpan_Milieu.Name = "TLpan_Milieu"
        Me.TLpan_Milieu.RowCount = 2
        Me.TLpan_Milieu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Milieu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Milieu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Milieu.Size = New System.Drawing.Size(415, 496)
        Me.TLpan_Milieu.TabIndex = 3
        '
        'pan_Bac
        '
        Me.pan_Bac.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Bac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Bac.Controls.Add(Me.txt_Hp)
        Me.pan_Bac.Controls.Add(Me.lbl_HauteurHp)
        Me.pan_Bac.Controls.Add(Me.etq_UnitDim4)
        Me.pan_Bac.Controls.Add(Me.img_Hp)
        Me.pan_Bac.Controls.Add(Me.img_Bac)
        Me.pan_Bac.Controls.Add(Me.btn_ModifierBac)
        Me.pan_Bac.Controls.Add(Me.lbl_BacNom)
        Me.pan_Bac.Controls.Add(Me.txt_BacNom)
        Me.pan_Bac.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Bac.Location = New System.Drawing.Point(0, 30)
        Me.pan_Bac.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Bac.Name = "pan_Bac"
        Me.pan_Bac.Size = New System.Drawing.Size(415, 466)
        Me.pan_Bac.TabIndex = 9
        '
        'txt_Hp
        '
        Me.txt_Hp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Hp.Location = New System.Drawing.Point(321, 141)
        Me.txt_Hp.Name = "txt_Hp"
        Me.txt_Hp.Size = New System.Drawing.Size(58, 20)
        Me.txt_Hp.TabIndex = 84
        '
        'lbl_HauteurHp
        '
        Me.lbl_HauteurHp.AutoSize = True
        Me.lbl_HauteurHp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_HauteurHp.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_HauteurHp.Location = New System.Drawing.Point(16, 144)
        Me.lbl_HauteurHp.Name = "lbl_HauteurHp"
        Me.lbl_HauteurHp.Size = New System.Drawing.Size(39, 13)
        Me.lbl_HauteurHp.TabIndex = 82
        Me.lbl_HauteurHp.Text = "Label1"
        Me.lbl_HauteurHp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_UnitDim4
        '
        Me.etq_UnitDim4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim4.AutoSize = True
        Me.etq_UnitDim4.Location = New System.Drawing.Point(384, 144)
        Me.etq_UnitDim4.Name = "etq_UnitDim4"
        Me.etq_UnitDim4.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim4.TabIndex = 83
        Me.etq_UnitDim4.Text = "mm"
        '
        'img_Hp
        '
        Me.img_Hp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Hp.Location = New System.Drawing.Point(284, 141)
        Me.img_Hp.Name = "img_Hp"
        Me.img_Hp.Size = New System.Drawing.Size(37, 20)
        Me.img_Hp.TabIndex = 85
        Me.img_Hp.TabStop = False
        '
        'img_Bac
        '
        Me.img_Bac.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Bac.Location = New System.Drawing.Point(3, 31)
        Me.img_Bac.Name = "img_Bac"
        Me.img_Bac.Size = New System.Drawing.Size(408, 104)
        Me.img_Bac.TabIndex = 80
        Me.img_Bac.TabStop = False
        '
        'btn_ModifierBac
        '
        Me.btn_ModifierBac.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_ModifierBac.Location = New System.Drawing.Point(3, 167)
        Me.btn_ModifierBac.Name = "btn_ModifierBac"
        Me.btn_ModifierBac.Size = New System.Drawing.Size(408, 27)
        Me.btn_ModifierBac.TabIndex = 74
        Me.btn_ModifierBac.Text = "btn_ModifierBac"
        Me.btn_ModifierBac.UseVisualStyleBackColor = True
        '
        'lbl_BacNom
        '
        Me.lbl_BacNom.AutoSize = True
        Me.lbl_BacNom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_BacNom.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_BacNom.Location = New System.Drawing.Point(12, 12)
        Me.lbl_BacNom.Name = "lbl_BacNom"
        Me.lbl_BacNom.Size = New System.Drawing.Size(64, 13)
        Me.lbl_BacNom.TabIndex = 73
        Me.lbl_BacNom.Text = "lbl_BacNom"
        Me.lbl_BacNom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_BacNom
        '
        Me.txt_BacNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_BacNom.Location = New System.Drawing.Point(89, 9)
        Me.txt_BacNom.Name = "txt_BacNom"
        Me.txt_BacNom.Size = New System.Drawing.Size(322, 20)
        Me.txt_BacNom.TabIndex = 72
        '
        'lbl_Bac
        '
        Me.lbl_Bac.AutoSize = True
        Me.lbl_Bac.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Bac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Bac.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Bac.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Bac.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Bac.Name = "lbl_Bac"
        Me.lbl_Bac.Size = New System.Drawing.Size(415, 30)
        Me.lbl_Bac.TabIndex = 2
        Me.lbl_Bac.Text = "lbl_Bac"
        Me.lbl_Bac.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_DalleSlimFloorNBac
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 693)
        Me.Controls.Add(Me.pan_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_DalleSlimFloorNBac"
        Me.Text = "Frm_DalleSlimFloorNBac"
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Milieu.ResumeLayout(False)
        Me.TLpan_Milieu.PerformLayout()
        Me.pan_Bac.ResumeLayout(False)
        Me.pan_Bac.PerformLayout()
        CType(Me.img_Hp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Bac, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Milieu As TableLayoutPanel
    Friend WithEvents pan_Bac As Panel
    Friend WithEvents txt_Hp As TextBox
    Friend WithEvents lbl_HauteurHp As Label
    Friend WithEvents etq_UnitDim4 As Label
    Friend WithEvents img_Hp As PictureBox
    Friend WithEvents img_Bac As PictureBox
    Friend WithEvents btn_ModifierBac As Button
    Friend WithEvents lbl_BacNom As Label
    Friend WithEvents txt_BacNom As TextBox
    Friend WithEvents lbl_Bac As Label
End Class
