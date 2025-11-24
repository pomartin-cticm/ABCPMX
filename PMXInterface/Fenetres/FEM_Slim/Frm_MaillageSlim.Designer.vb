<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_MaillageSlim
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
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Portees = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Maillage = New System.Windows.Forms.Label()
        Me.pan_Maillage = New System.Windows.Forms.Panel()
        Me.chk_AffChampTherm = New System.Windows.Forms.CheckBox()
        Me.prb_CalculTh = New System.Windows.Forms.ProgressBar()
        Me.cmb_TempR = New System.Windows.Forms.ComboBox()
        Me.chk_CalculTherm = New System.Windows.Forms.CheckBox()
        Me.chk_CoutourSeul = New System.Windows.Forms.CheckBox()
        Me.lbl_SuivantY = New System.Windows.Forms.Label()
        Me.txt_NbMailY = New System.Windows.Forms.TextBox()
        Me.lbl_SuivantX = New System.Windows.Forms.Label()
        Me.txt_NbMailX = New System.Windows.Forms.TextBox()
        Me.lbl_NbMailles = New System.Windows.Forms.Label()
        Me.pan_Image = New System.Windows.Forms.Panel()
        Me.tlp_Images = New System.Windows.Forms.TableLayoutPanel()
        Me.img_Legende = New System.Windows.Forms.PictureBox()
        Me.img_Maillage = New System.Windows.Forms.PictureBox()
        Me.chk_AffChampThermPoutre = New System.Windows.Forms.CheckBox()
        Me.chk_AffChampThermDalle = New System.Windows.Forms.CheckBox()
        Me.chk_AffChampThermVideOuvert = New System.Windows.Forms.CheckBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Portees.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_Maillage.SuspendLayout()
        Me.pan_Image.SuspendLayout()
        Me.tlp_Images.SuspendLayout()
        CType(Me.img_Legende, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Maillage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(631, 384)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(631, 384)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 3
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 1, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 347)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(625, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(255, 3)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(114, 28)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_Portees)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(625, 338)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Portees
        '
        Me.TLPan_Portees.ColumnCount = 2
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Portees.Controls.Add(Me.pan_Image, 1, 0)
        Me.TLPan_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Portees.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Portees.Name = "TLPan_Portees"
        Me.TLPan_Portees.RowCount = 1
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Size = New System.Drawing.Size(625, 338)
        Me.TLPan_Portees.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(250, 338)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Maillage, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Maillage, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 2
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(250, 336)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Maillage
        '
        Me.lbl_Maillage.AutoSize = True
        Me.lbl_Maillage.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Maillage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Maillage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Maillage.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Maillage.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Maillage.Name = "lbl_Maillage"
        Me.lbl_Maillage.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Maillage.TabIndex = 0
        Me.lbl_Maillage.Text = "lbl_Portee"
        Me.lbl_Maillage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Maillage
        '
        Me.pan_Maillage.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Maillage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Maillage.Controls.Add(Me.chk_AffChampThermVideOuvert)
        Me.pan_Maillage.Controls.Add(Me.chk_AffChampThermDalle)
        Me.pan_Maillage.Controls.Add(Me.chk_AffChampThermPoutre)
        Me.pan_Maillage.Controls.Add(Me.chk_AffChampTherm)
        Me.pan_Maillage.Controls.Add(Me.prb_CalculTh)
        Me.pan_Maillage.Controls.Add(Me.cmb_TempR)
        Me.pan_Maillage.Controls.Add(Me.chk_CalculTherm)
        Me.pan_Maillage.Controls.Add(Me.chk_CoutourSeul)
        Me.pan_Maillage.Controls.Add(Me.lbl_SuivantY)
        Me.pan_Maillage.Controls.Add(Me.txt_NbMailY)
        Me.pan_Maillage.Controls.Add(Me.lbl_SuivantX)
        Me.pan_Maillage.Controls.Add(Me.txt_NbMailX)
        Me.pan_Maillage.Controls.Add(Me.lbl_NbMailles)
        Me.pan_Maillage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Maillage.Location = New System.Drawing.Point(0, 30)
        Me.pan_Maillage.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Maillage.Name = "pan_Maillage"
        Me.pan_Maillage.Size = New System.Drawing.Size(250, 306)
        Me.pan_Maillage.TabIndex = 1
        '
        'chk_AffChampTherm
        '
        Me.chk_AffChampTherm.AutoSize = True
        Me.chk_AffChampTherm.Location = New System.Drawing.Point(10, 169)
        Me.chk_AffChampTherm.Name = "chk_AffChampTherm"
        Me.chk_AffChampTherm.Size = New System.Drawing.Size(126, 17)
        Me.chk_AffChampTherm.TabIndex = 128
        Me.chk_AffChampTherm.Text = "chk_AffChampTherm"
        Me.chk_AffChampTherm.UseVisualStyleBackColor = True
        '
        'prb_CalculTh
        '
        Me.prb_CalculTh.Location = New System.Drawing.Point(10, 136)
        Me.prb_CalculTh.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.prb_CalculTh.Name = "prb_CalculTh"
        Me.prb_CalculTh.Size = New System.Drawing.Size(228, 19)
        Me.prb_CalculTh.TabIndex = 127
        '
        'cmb_TempR
        '
        Me.cmb_TempR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_TempR.FormattingEnabled = True
        Me.cmb_TempR.Location = New System.Drawing.Point(161, 106)
        Me.cmb_TempR.Name = "cmb_TempR"
        Me.cmb_TempR.Size = New System.Drawing.Size(78, 21)
        Me.cmb_TempR.TabIndex = 126
        '
        'chk_CalculTherm
        '
        Me.chk_CalculTherm.AutoSize = True
        Me.chk_CalculTherm.Location = New System.Drawing.Point(8, 107)
        Me.chk_CalculTherm.Name = "chk_CalculTherm"
        Me.chk_CalculTherm.Size = New System.Drawing.Size(109, 17)
        Me.chk_CalculTherm.TabIndex = 125
        Me.chk_CalculTherm.Text = "chk_CalculTherm"
        Me.chk_CalculTherm.UseVisualStyleBackColor = True
        '
        'chk_CoutourSeul
        '
        Me.chk_CoutourSeul.AutoSize = True
        Me.chk_CoutourSeul.Location = New System.Drawing.Point(8, 84)
        Me.chk_CoutourSeul.Name = "chk_CoutourSeul"
        Me.chk_CoutourSeul.Size = New System.Drawing.Size(108, 17)
        Me.chk_CoutourSeul.TabIndex = 65
        Me.chk_CoutourSeul.Text = "chk_CoutourSeul"
        Me.chk_CoutourSeul.UseVisualStyleBackColor = True
        '
        'lbl_SuivantY
        '
        Me.lbl_SuivantY.Location = New System.Drawing.Point(62, 56)
        Me.lbl_SuivantY.Name = "lbl_SuivantY"
        Me.lbl_SuivantY.Size = New System.Drawing.Size(67, 13)
        Me.lbl_SuivantY.TabIndex = 76
        Me.lbl_SuivantY.Text = "lbl_SuivantY"
        Me.lbl_SuivantY.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_NbMailY
        '
        Me.txt_NbMailY.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_NbMailY.Location = New System.Drawing.Point(135, 54)
        Me.txt_NbMailY.Name = "txt_NbMailY"
        Me.txt_NbMailY.Size = New System.Drawing.Size(58, 20)
        Me.txt_NbMailY.TabIndex = 75
        Me.txt_NbMailY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_SuivantX
        '
        Me.lbl_SuivantX.Location = New System.Drawing.Point(62, 32)
        Me.lbl_SuivantX.Name = "lbl_SuivantX"
        Me.lbl_SuivantX.Size = New System.Drawing.Size(67, 13)
        Me.lbl_SuivantX.TabIndex = 74
        Me.lbl_SuivantX.Text = "lbl_SuivantX"
        Me.lbl_SuivantX.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_NbMailX
        '
        Me.txt_NbMailX.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_NbMailX.Location = New System.Drawing.Point(135, 29)
        Me.txt_NbMailX.Name = "txt_NbMailX"
        Me.txt_NbMailX.Size = New System.Drawing.Size(58, 20)
        Me.txt_NbMailX.TabIndex = 73
        Me.txt_NbMailX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_NbMailles
        '
        Me.lbl_NbMailles.AutoSize = True
        Me.lbl_NbMailles.Location = New System.Drawing.Point(6, 10)
        Me.lbl_NbMailles.Name = "lbl_NbMailles"
        Me.lbl_NbMailles.Size = New System.Drawing.Size(69, 13)
        Me.lbl_NbMailles.TabIndex = 72
        Me.lbl_NbMailles.Text = "lbl_NbMailles"
        '
        'pan_Image
        '
        Me.pan_Image.Controls.Add(Me.tlp_Images)
        Me.pan_Image.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Image.Location = New System.Drawing.Point(251, 0)
        Me.pan_Image.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Image.Name = "pan_Image"
        Me.pan_Image.Size = New System.Drawing.Size(374, 338)
        Me.pan_Image.TabIndex = 1
        '
        'tlp_Images
        '
        Me.tlp_Images.ColumnCount = 1
        Me.tlp_Images.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_Images.Controls.Add(Me.img_Legende, 0, 1)
        Me.tlp_Images.Controls.Add(Me.img_Maillage, 0, 0)
        Me.tlp_Images.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlp_Images.Location = New System.Drawing.Point(0, 0)
        Me.tlp_Images.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.tlp_Images.Name = "tlp_Images"
        Me.tlp_Images.RowCount = 2
        Me.tlp_Images.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_Images.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41.0!))
        Me.tlp_Images.Size = New System.Drawing.Size(374, 338)
        Me.tlp_Images.TabIndex = 0
        '
        'img_Legende
        '
        Me.img_Legende.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Legende.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Legende.Dock = System.Windows.Forms.DockStyle.Fill
        Me.img_Legende.Location = New System.Drawing.Point(0, 297)
        Me.img_Legende.Margin = New System.Windows.Forms.Padding(0)
        Me.img_Legende.Name = "img_Legende"
        Me.img_Legende.Size = New System.Drawing.Size(374, 41)
        Me.img_Legende.TabIndex = 2
        Me.img_Legende.TabStop = False
        '
        'img_Maillage
        '
        Me.img_Maillage.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Maillage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Maillage.Location = New System.Drawing.Point(0, 0)
        Me.img_Maillage.Margin = New System.Windows.Forms.Padding(0)
        Me.img_Maillage.Name = "img_Maillage"
        Me.img_Maillage.Size = New System.Drawing.Size(100, 50)
        Me.img_Maillage.TabIndex = 1
        Me.img_Maillage.TabStop = False
        '
        'chk_AffChampThermPoutre
        '
        Me.chk_AffChampThermPoutre.AutoSize = True
        Me.chk_AffChampThermPoutre.Enabled = False
        Me.chk_AffChampThermPoutre.Location = New System.Drawing.Point(35, 192)
        Me.chk_AffChampThermPoutre.Name = "chk_AffChampThermPoutre"
        Me.chk_AffChampThermPoutre.Size = New System.Drawing.Size(157, 17)
        Me.chk_AffChampThermPoutre.TabIndex = 129
        Me.chk_AffChampThermPoutre.Text = "chk_AffChampThermPoutre"
        Me.chk_AffChampThermPoutre.UseVisualStyleBackColor = True
        '
        'chk_AffChampThermDalle
        '
        Me.chk_AffChampThermDalle.AutoSize = True
        Me.chk_AffChampThermDalle.Location = New System.Drawing.Point(35, 215)
        Me.chk_AffChampThermDalle.Name = "chk_AffChampThermDalle"
        Me.chk_AffChampThermDalle.Size = New System.Drawing.Size(150, 17)
        Me.chk_AffChampThermDalle.TabIndex = 130
        Me.chk_AffChampThermDalle.Text = "chk_AffChampThermDalle"
        Me.chk_AffChampThermDalle.UseVisualStyleBackColor = True
        '
        'chk_AffChampThermVideOuvert
        '
        Me.chk_AffChampThermVideOuvert.AutoSize = True
        Me.chk_AffChampThermVideOuvert.Location = New System.Drawing.Point(36, 238)
        Me.chk_AffChampThermVideOuvert.Name = "chk_AffChampThermVideOuvert"
        Me.chk_AffChampThermVideOuvert.Size = New System.Drawing.Size(179, 17)
        Me.chk_AffChampThermVideOuvert.TabIndex = 131
        Me.chk_AffChampThermVideOuvert.Text = "chk_AffChampThermVideOuvert"
        Me.chk_AffChampThermVideOuvert.UseVisualStyleBackColor = True
        '
        'Frm_MaillageSlim
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 384)
        Me.Controls.Add(Me.pan_General)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.MinimizeBox = False
        Me.Name = "Frm_MaillageSlim"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_MaillageSlim"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Portees.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_Maillage.ResumeLayout(False)
        Me.pan_Maillage.PerformLayout()
        Me.pan_Image.ResumeLayout(False)
        Me.tlp_Images.ResumeLayout(False)
        CType(Me.img_Legende, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Maillage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Portees As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Maillage As Label
    Friend WithEvents pan_Maillage As Panel
    Friend WithEvents pan_Image As Panel
    Friend WithEvents img_Maillage As PictureBox
    Friend WithEvents lbl_SuivantY As Label
    Friend WithEvents txt_NbMailY As TextBox
    Friend WithEvents lbl_SuivantX As Label
    Friend WithEvents txt_NbMailX As TextBox
    Friend WithEvents lbl_NbMailles As Label
    Friend WithEvents chk_CoutourSeul As CheckBox
    Friend WithEvents cmb_TempR As ComboBox
    Friend WithEvents chk_CalculTherm As CheckBox
    Friend WithEvents prb_CalculTh As ProgressBar
    Friend WithEvents chk_AffChampTherm As CheckBox
    Friend WithEvents tlp_Images As TableLayoutPanel
    Friend WithEvents img_Legende As PictureBox
    Friend WithEvents chk_AffChampThermVideOuvert As CheckBox
    Friend WithEvents chk_AffChampThermDalle As CheckBox
    Friend WithEvents chk_AffChampThermPoutre As CheckBox
End Class
