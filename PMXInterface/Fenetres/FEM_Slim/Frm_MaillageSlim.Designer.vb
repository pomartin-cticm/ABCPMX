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
        Me.chk_EchelleTempPerso = New System.Windows.Forms.CheckBox()
        Me.chk_Aff2D = New System.Windows.Forms.CheckBox()
        Me.lbl_arma = New System.Windows.Forms.Label()
        Me.lbl_vide = New System.Windows.Forms.Label()
        Me.lbl_dalle = New System.Windows.Forms.Label()
        Me.lbl_poutre = New System.Windows.Forms.Label()
        Me.chk_AffChThArmature = New System.Windows.Forms.CheckBox()
        Me.chk_AffArmature = New System.Windows.Forms.CheckBox()
        Me.chk_Lissage_Couleur = New System.Windows.Forms.CheckBox()
        Me.lbl_ch_th = New System.Windows.Forms.Label()
        Me.lbl_elements = New System.Windows.Forms.Label()
        Me.chk_AffVide = New System.Windows.Forms.CheckBox()
        Me.chk_AffDalle = New System.Windows.Forms.CheckBox()
        Me.chk_AffPoutre = New System.Windows.Forms.CheckBox()
        Me.chk_AffChThVideOuvert = New System.Windows.Forms.CheckBox()
        Me.chk_AffChThDalle = New System.Windows.Forms.CheckBox()
        Me.chk_AffChThPoutre = New System.Windows.Forms.CheckBox()
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
        Me.pan_General.Size = New System.Drawing.Size(755, 514)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(755, 514)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 477)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(749, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(317, 3)
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
        Me.pan_Main.Size = New System.Drawing.Size(749, 468)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Portees
        '
        Me.TLPan_Portees.ColumnCount = 2
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 299.0!))
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Portees.Controls.Add(Me.pan_Image, 1, 0)
        Me.TLPan_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Portees.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Portees.Name = "TLPan_Portees"
        Me.TLPan_Portees.RowCount = 1
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Size = New System.Drawing.Size(749, 468)
        Me.TLPan_Portees.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(299, 468)
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
        Me.TLPan_Gauche.Size = New System.Drawing.Size(299, 429)
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
        Me.lbl_Maillage.Size = New System.Drawing.Size(299, 30)
        Me.lbl_Maillage.TabIndex = 0
        Me.lbl_Maillage.Text = "lbl_Portee"
        Me.lbl_Maillage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Maillage
        '
        Me.pan_Maillage.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Maillage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Maillage.Controls.Add(Me.chk_EchelleTempPerso)
        Me.pan_Maillage.Controls.Add(Me.chk_Aff2D)
        Me.pan_Maillage.Controls.Add(Me.lbl_arma)
        Me.pan_Maillage.Controls.Add(Me.lbl_vide)
        Me.pan_Maillage.Controls.Add(Me.lbl_dalle)
        Me.pan_Maillage.Controls.Add(Me.lbl_poutre)
        Me.pan_Maillage.Controls.Add(Me.chk_AffChThArmature)
        Me.pan_Maillage.Controls.Add(Me.chk_AffArmature)
        Me.pan_Maillage.Controls.Add(Me.chk_Lissage_Couleur)
        Me.pan_Maillage.Controls.Add(Me.lbl_ch_th)
        Me.pan_Maillage.Controls.Add(Me.lbl_elements)
        Me.pan_Maillage.Controls.Add(Me.chk_AffVide)
        Me.pan_Maillage.Controls.Add(Me.chk_AffDalle)
        Me.pan_Maillage.Controls.Add(Me.chk_AffPoutre)
        Me.pan_Maillage.Controls.Add(Me.chk_AffChThVideOuvert)
        Me.pan_Maillage.Controls.Add(Me.chk_AffChThDalle)
        Me.pan_Maillage.Controls.Add(Me.chk_AffChThPoutre)
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
        Me.pan_Maillage.Size = New System.Drawing.Size(299, 399)
        Me.pan_Maillage.TabIndex = 1
        '
        'chk_EchelleTempPerso
        '
        Me.chk_EchelleTempPerso.AutoSize = True
        Me.chk_EchelleTempPerso.Location = New System.Drawing.Point(28, 238)
        Me.chk_EchelleTempPerso.Name = "chk_EchelleTempPerso"
        Me.chk_EchelleTempPerso.Size = New System.Drawing.Size(139, 17)
        Me.chk_EchelleTempPerso.TabIndex = 145
        Me.chk_EchelleTempPerso.Text = "chk_EchelleTempPerso"
        Me.chk_EchelleTempPerso.UseVisualStyleBackColor = True
        '
        'chk_Aff2D
        '
        Me.chk_Aff2D.AutoSize = True
        Me.chk_Aff2D.Location = New System.Drawing.Point(27, 261)
        Me.chk_Aff2D.Name = "chk_Aff2D"
        Me.chk_Aff2D.Size = New System.Drawing.Size(77, 17)
        Me.chk_Aff2D.TabIndex = 144
        Me.chk_Aff2D.Text = "chk_Aff2D"
        Me.chk_Aff2D.UseVisualStyleBackColor = True
        '
        'lbl_arma
        '
        Me.lbl_arma.AutoSize = True
        Me.lbl_arma.Location = New System.Drawing.Point(33, 368)
        Me.lbl_arma.Name = "lbl_arma"
        Me.lbl_arma.Size = New System.Drawing.Size(46, 13)
        Me.lbl_arma.TabIndex = 143
        Me.lbl_arma.Text = "lbl_arma"
        '
        'lbl_vide
        '
        Me.lbl_vide.AutoSize = True
        Me.lbl_vide.Location = New System.Drawing.Point(33, 345)
        Me.lbl_vide.Name = "lbl_vide"
        Me.lbl_vide.Size = New System.Drawing.Size(43, 13)
        Me.lbl_vide.TabIndex = 142
        Me.lbl_vide.Text = "lbl_vide"
        '
        'lbl_dalle
        '
        Me.lbl_dalle.AutoSize = True
        Me.lbl_dalle.Location = New System.Drawing.Point(33, 322)
        Me.lbl_dalle.Name = "lbl_dalle"
        Me.lbl_dalle.Size = New System.Drawing.Size(45, 13)
        Me.lbl_dalle.TabIndex = 141
        Me.lbl_dalle.Text = "lbl_dalle"
        '
        'lbl_poutre
        '
        Me.lbl_poutre.AutoSize = True
        Me.lbl_poutre.Location = New System.Drawing.Point(33, 299)
        Me.lbl_poutre.Name = "lbl_poutre"
        Me.lbl_poutre.Size = New System.Drawing.Size(53, 13)
        Me.lbl_poutre.TabIndex = 140
        Me.lbl_poutre.Text = "lbl_poutre"
        '
        'chk_AffChThArmature
        '
        Me.chk_AffChThArmature.AutoSize = True
        Me.chk_AffChThArmature.Location = New System.Drawing.Point(232, 368)
        Me.chk_AffChThArmature.Name = "chk_AffChThArmature"
        Me.chk_AffChThArmature.Size = New System.Drawing.Size(131, 17)
        Me.chk_AffChThArmature.TabIndex = 139
        Me.chk_AffChThArmature.Text = "chk_AffChThArmature"
        Me.chk_AffChThArmature.UseVisualStyleBackColor = True
        '
        'chk_AffArmature
        '
        Me.chk_AffArmature.AutoSize = True
        Me.chk_AffArmature.Location = New System.Drawing.Point(132, 368)
        Me.chk_AffArmature.Name = "chk_AffArmature"
        Me.chk_AffArmature.Size = New System.Drawing.Size(105, 17)
        Me.chk_AffArmature.TabIndex = 138
        Me.chk_AffArmature.Text = "chk_AffArmature"
        Me.chk_AffArmature.UseVisualStyleBackColor = True
        '
        'chk_Lissage_Couleur
        '
        Me.chk_Lissage_Couleur.AutoSize = True
        Me.chk_Lissage_Couleur.Location = New System.Drawing.Point(28, 215)
        Me.chk_Lissage_Couleur.Name = "chk_Lissage_Couleur"
        Me.chk_Lissage_Couleur.Size = New System.Drawing.Size(128, 17)
        Me.chk_Lissage_Couleur.TabIndex = 137
        Me.chk_Lissage_Couleur.Text = "chk_Lissage_Couleur"
        Me.chk_Lissage_Couleur.UseVisualStyleBackColor = True
        '
        'lbl_ch_th
        '
        Me.lbl_ch_th.AutoSize = True
        Me.lbl_ch_th.Location = New System.Drawing.Point(195, 283)
        Me.lbl_ch_th.Name = "lbl_ch_th"
        Me.lbl_ch_th.Size = New System.Drawing.Size(99, 13)
        Me.lbl_ch_th.TabIndex = 136
        Me.lbl_ch_th.Text = "Champs thermiques"
        '
        'lbl_elements
        '
        Me.lbl_elements.AutoSize = True
        Me.lbl_elements.Location = New System.Drawing.Point(114, 283)
        Me.lbl_elements.Name = "lbl_elements"
        Me.lbl_elements.Size = New System.Drawing.Size(50, 13)
        Me.lbl_elements.TabIndex = 135
        Me.lbl_elements.Text = "Eléments"
        '
        'chk_AffVide
        '
        Me.chk_AffVide.AutoSize = True
        Me.chk_AffVide.Location = New System.Drawing.Point(132, 345)
        Me.chk_AffVide.Name = "chk_AffVide"
        Me.chk_AffVide.Size = New System.Drawing.Size(84, 17)
        Me.chk_AffVide.TabIndex = 134
        Me.chk_AffVide.Text = "chk_AffVide"
        Me.chk_AffVide.UseVisualStyleBackColor = True
        '
        'chk_AffDalle
        '
        Me.chk_AffDalle.AutoSize = True
        Me.chk_AffDalle.Location = New System.Drawing.Point(132, 322)
        Me.chk_AffDalle.Name = "chk_AffDalle"
        Me.chk_AffDalle.Size = New System.Drawing.Size(87, 17)
        Me.chk_AffDalle.TabIndex = 133
        Me.chk_AffDalle.Text = "chk_AffDalle"
        Me.chk_AffDalle.UseVisualStyleBackColor = True
        '
        'chk_AffPoutre
        '
        Me.chk_AffPoutre.AutoSize = True
        Me.chk_AffPoutre.Location = New System.Drawing.Point(132, 299)
        Me.chk_AffPoutre.Name = "chk_AffPoutre"
        Me.chk_AffPoutre.Size = New System.Drawing.Size(94, 17)
        Me.chk_AffPoutre.TabIndex = 132
        Me.chk_AffPoutre.Text = "chk_AffPoutre"
        Me.chk_AffPoutre.UseVisualStyleBackColor = True
        '
        'chk_AffChThVideOuvert
        '
        Me.chk_AffChThVideOuvert.AutoSize = True
        Me.chk_AffChThVideOuvert.Location = New System.Drawing.Point(232, 345)
        Me.chk_AffChThVideOuvert.Name = "chk_AffChThVideOuvert"
        Me.chk_AffChThVideOuvert.Size = New System.Drawing.Size(142, 17)
        Me.chk_AffChThVideOuvert.TabIndex = 131
        Me.chk_AffChThVideOuvert.Text = "chk_AffChThVideOuvert"
        Me.chk_AffChThVideOuvert.UseVisualStyleBackColor = True
        '
        'chk_AffChThDalle
        '
        Me.chk_AffChThDalle.AutoSize = True
        Me.chk_AffChThDalle.Location = New System.Drawing.Point(232, 322)
        Me.chk_AffChThDalle.Name = "chk_AffChThDalle"
        Me.chk_AffChThDalle.Size = New System.Drawing.Size(113, 17)
        Me.chk_AffChThDalle.TabIndex = 130
        Me.chk_AffChThDalle.Text = "chk_AffChThDalle"
        Me.chk_AffChThDalle.UseVisualStyleBackColor = True
        '
        'chk_AffChThPoutre
        '
        Me.chk_AffChThPoutre.AutoSize = True
        Me.chk_AffChThPoutre.Enabled = False
        Me.chk_AffChThPoutre.Location = New System.Drawing.Point(232, 299)
        Me.chk_AffChThPoutre.Name = "chk_AffChThPoutre"
        Me.chk_AffChThPoutre.Size = New System.Drawing.Size(120, 17)
        Me.chk_AffChThPoutre.TabIndex = 129
        Me.chk_AffChThPoutre.Text = "chk_AffChThPoutre"
        Me.chk_AffChThPoutre.UseVisualStyleBackColor = True
        '
        'chk_AffChampTherm
        '
        Me.chk_AffChampTherm.AutoSize = True
        Me.chk_AffChampTherm.Location = New System.Drawing.Point(3, 192)
        Me.chk_AffChampTherm.Name = "chk_AffChampTherm"
        Me.chk_AffChampTherm.Size = New System.Drawing.Size(126, 17)
        Me.chk_AffChampTherm.TabIndex = 128
        Me.chk_AffChampTherm.Text = "chk_AffChampTherm"
        Me.chk_AffChampTherm.UseVisualStyleBackColor = True
        '
        'prb_CalculTh
        '
        Me.prb_CalculTh.Location = New System.Drawing.Point(35, 150)
        Me.prb_CalculTh.Margin = New System.Windows.Forms.Padding(2)
        Me.prb_CalculTh.Name = "prb_CalculTh"
        Me.prb_CalculTh.Size = New System.Drawing.Size(228, 19)
        Me.prb_CalculTh.TabIndex = 127
        '
        'cmb_TempR
        '
        Me.cmb_TempR.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_TempR.FormattingEnabled = True
        Me.cmb_TempR.Location = New System.Drawing.Point(185, 126)
        Me.cmb_TempR.Name = "cmb_TempR"
        Me.cmb_TempR.Size = New System.Drawing.Size(78, 21)
        Me.cmb_TempR.TabIndex = 126
        '
        'chk_CalculTherm
        '
        Me.chk_CalculTherm.AutoSize = True
        Me.chk_CalculTherm.Location = New System.Drawing.Point(8, 128)
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
        Me.txt_NbMailY.Location = New System.Drawing.Point(184, 54)
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
        Me.txt_NbMailX.Location = New System.Drawing.Point(184, 29)
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
        Me.pan_Image.Location = New System.Drawing.Point(300, 0)
        Me.pan_Image.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Image.Name = "pan_Image"
        Me.pan_Image.Size = New System.Drawing.Size(449, 468)
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
        Me.tlp_Images.Margin = New System.Windows.Forms.Padding(2)
        Me.tlp_Images.Name = "tlp_Images"
        Me.tlp_Images.RowCount = 2
        Me.tlp_Images.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp_Images.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41.0!))
        Me.tlp_Images.Size = New System.Drawing.Size(449, 468)
        Me.tlp_Images.TabIndex = 0
        '
        'img_Legende
        '
        Me.img_Legende.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Legende.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Legende.Dock = System.Windows.Forms.DockStyle.Fill
        Me.img_Legende.Location = New System.Drawing.Point(0, 427)
        Me.img_Legende.Margin = New System.Windows.Forms.Padding(0)
        Me.img_Legende.Name = "img_Legende"
        Me.img_Legende.Size = New System.Drawing.Size(449, 41)
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
        'Frm_MaillageSlim
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(755, 514)
        Me.Controls.Add(Me.pan_General)
        Me.Margin = New System.Windows.Forms.Padding(2)
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
    Friend WithEvents chk_AffChThVideOuvert As CheckBox
    Friend WithEvents chk_AffChThDalle As CheckBox
    Friend WithEvents chk_AffChThPoutre As CheckBox
    Friend WithEvents chk_AffVide As CheckBox
    Friend WithEvents chk_AffDalle As CheckBox
    Friend WithEvents chk_AffPoutre As CheckBox
    Friend WithEvents lbl_ch_th As Label
    Friend WithEvents lbl_elements As Label
    Friend WithEvents chk_Lissage_Couleur As CheckBox
    Friend WithEvents chk_AffChThArmature As CheckBox
    Friend WithEvents chk_AffArmature As CheckBox
    Friend WithEvents lbl_poutre As Label
    Friend WithEvents lbl_dalle As Label
    Friend WithEvents chk_EchelleTempPerso As CheckBox
    Friend WithEvents chk_Aff2D As CheckBox
    Friend WithEvents lbl_arma As Label
    Friend WithEvents lbl_vide As Label
End Class
