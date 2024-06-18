<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_ConnectionSlimFloor
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_ConnectionSlimFloor))
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Connecteurs = New System.Windows.Forms.Label()
        Me.pan_SaisieTypeArma = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.cmb_TypeConnection = New System.Windows.Forms.ComboBox()
        Me.pan_SaisieGoujons = New System.Windows.Forms.Panel()
        Me.etq_UnitFu = New System.Windows.Forms.Label()
        Me.etq_UnitFy = New System.Windows.Forms.Label()
        Me.etq_UnitD = New System.Windows.Forms.Label()
        Me.etq_UnitHsc = New System.Windows.Forms.Label()
        Me.txt_fu = New System.Windows.Forms.TextBox()
        Me.txt_fy = New System.Windows.Forms.TextBox()
        Me.txt_d = New System.Windows.Forms.TextBox()
        Me.txt_hsc = New System.Windows.Forms.TextBox()
        Me.img_fu = New System.Windows.Forms.PictureBox()
        Me.img_fy = New System.Windows.Forms.PictureBox()
        Me.img_d = New System.Windows.Forms.PictureBox()
        Me.img_hsc = New System.Windows.Forms.PictureBox()
        Me.cmb_goujons = New System.Windows.Forms.ComboBox()
        Me.pan_SaisieArmature = New System.Windows.Forms.Panel()
        Me.lbl_ClasseA = New System.Windows.Forms.Label()
        Me.cmb_Acier = New System.Windows.Forms.ComboBox()
        Me.etq_UnitFsk = New System.Windows.Forms.Label()
        Me.etq_UnitPhiS = New System.Windows.Forms.Label()
        Me.txt_Fsk = New System.Windows.Forms.TextBox()
        Me.img_Fsk = New System.Windows.Forms.PictureBox()
        Me.txt_PhiS = New System.Windows.Forms.TextBox()
        Me.img_PhiS = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lbl_Connection = New System.Windows.Forms.Label()
        Me.pan_SaisieConnection = New System.Windows.Forms.Panel()
        Me.lbl_EspacementLongi = New System.Windows.Forms.Label()
        Me.lbl_NbRows = New System.Windows.Forms.Label()
        Me.txt_EspLongi_I1 = New System.Windows.Forms.TextBox()
        Me.cmb_NbRow_I1 = New System.Windows.Forms.ComboBox()
        Me.etq_Somme = New System.Windows.Forms.Label()
        Me.img_Stud = New System.Windows.Forms.PictureBox()
        Me.imgList_Navigation = New System.Windows.Forms.ImageList(Me.components)
        Me.ErrorProvider_Frm_Connection = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_PartieHaute = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Droite = New System.Windows.Forms.Panel()
        Me.TLPan_Droite = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_SaisieTypeArma.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_SaisieGoujons.SuspendLayout()
        CType(Me.img_fu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_fy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_d, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_hsc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_SaisieArmature.SuspendLayout()
        CType(Me.img_Fsk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_PhiS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_SaisieConnection.SuspendLayout()
        CType(Me.img_Stud, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider_Frm_Connection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_PartieHaute.SuspendLayout()
        Me.pan_Droite.SuspendLayout()
        Me.TLPan_Droite.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(270, 498)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Connecteurs, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisieTypeArma, 0, 1)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisieGoujons, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisieArmature, 0, 3)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Connection, 0, 4)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisieConnection, 0, 5)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 6
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(270, 498)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Connecteurs
        '
        Me.lbl_Connecteurs.AutoSize = True
        Me.lbl_Connecteurs.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Connecteurs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Connecteurs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Connecteurs.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Connecteurs.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Connecteurs.Name = "lbl_Connecteurs"
        Me.lbl_Connecteurs.Size = New System.Drawing.Size(270, 30)
        Me.lbl_Connecteurs.TabIndex = 0
        Me.lbl_Connecteurs.Text = "lbl_Connecteurs"
        Me.lbl_Connecteurs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieTypeArma
        '
        Me.pan_SaisieTypeArma.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieTypeArma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieTypeArma.Controls.Add(Me.Label2)
        Me.pan_SaisieTypeArma.Controls.Add(Me.TextBox2)
        Me.pan_SaisieTypeArma.Controls.Add(Me.PictureBox2)
        Me.pan_SaisieTypeArma.Controls.Add(Me.cmb_TypeConnection)
        Me.pan_SaisieTypeArma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieTypeArma.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisieTypeArma.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_SaisieTypeArma.Name = "pan_SaisieTypeArma"
        Me.pan_SaisieTypeArma.Size = New System.Drawing.Size(270, 49)
        Me.pan_SaisieTypeArma.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(162, 156)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(21, 13)
        Me.Label2.TabIndex = 75
        Me.Label2.Text = "kN"
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.Location = New System.Drawing.Point(98, 152)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(58, 20)
        Me.TextBox2.TabIndex = 73
        '
        'PictureBox2
        '
        Me.PictureBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox2.Location = New System.Drawing.Point(53, 152)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(46, 20)
        Me.PictureBox2.TabIndex = 74
        Me.PictureBox2.TabStop = False
        '
        'cmb_TypeConnection
        '
        Me.cmb_TypeConnection.FormattingEnabled = True
        Me.cmb_TypeConnection.Location = New System.Drawing.Point(38, 13)
        Me.cmb_TypeConnection.Name = "cmb_TypeConnection"
        Me.cmb_TypeConnection.Size = New System.Drawing.Size(211, 21)
        Me.cmb_TypeConnection.TabIndex = 76
        '
        'pan_SaisieGoujons
        '
        Me.pan_SaisieGoujons.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieGoujons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieGoujons.Controls.Add(Me.etq_UnitFu)
        Me.pan_SaisieGoujons.Controls.Add(Me.etq_UnitFy)
        Me.pan_SaisieGoujons.Controls.Add(Me.etq_UnitD)
        Me.pan_SaisieGoujons.Controls.Add(Me.etq_UnitHsc)
        Me.pan_SaisieGoujons.Controls.Add(Me.txt_fu)
        Me.pan_SaisieGoujons.Controls.Add(Me.txt_fy)
        Me.pan_SaisieGoujons.Controls.Add(Me.txt_d)
        Me.pan_SaisieGoujons.Controls.Add(Me.txt_hsc)
        Me.pan_SaisieGoujons.Controls.Add(Me.img_fu)
        Me.pan_SaisieGoujons.Controls.Add(Me.img_fy)
        Me.pan_SaisieGoujons.Controls.Add(Me.img_d)
        Me.pan_SaisieGoujons.Controls.Add(Me.img_hsc)
        Me.pan_SaisieGoujons.Controls.Add(Me.cmb_goujons)
        Me.pan_SaisieGoujons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieGoujons.Location = New System.Drawing.Point(0, 80)
        Me.pan_SaisieGoujons.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_SaisieGoujons.Name = "pan_SaisieGoujons"
        Me.pan_SaisieGoujons.Size = New System.Drawing.Size(270, 169)
        Me.pan_SaisieGoujons.TabIndex = 1
        '
        'etq_UnitFu
        '
        Me.etq_UnitFu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitFu.AutoSize = True
        Me.etq_UnitFu.Location = New System.Drawing.Point(162, 127)
        Me.etq_UnitFu.Name = "etq_UnitFu"
        Me.etq_UnitFu.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitFu.TabIndex = 75
        Me.etq_UnitFu.Text = "kN"
        '
        'etq_UnitFy
        '
        Me.etq_UnitFy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitFy.AutoSize = True
        Me.etq_UnitFy.Location = New System.Drawing.Point(162, 101)
        Me.etq_UnitFy.Name = "etq_UnitFy"
        Me.etq_UnitFy.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitFy.TabIndex = 75
        Me.etq_UnitFy.Text = "kN"
        '
        'etq_UnitD
        '
        Me.etq_UnitD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitD.AutoSize = True
        Me.etq_UnitD.Location = New System.Drawing.Point(162, 75)
        Me.etq_UnitD.Name = "etq_UnitD"
        Me.etq_UnitD.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitD.TabIndex = 75
        Me.etq_UnitD.Text = "kN"
        '
        'etq_UnitHsc
        '
        Me.etq_UnitHsc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitHsc.AutoSize = True
        Me.etq_UnitHsc.Location = New System.Drawing.Point(162, 49)
        Me.etq_UnitHsc.Name = "etq_UnitHsc"
        Me.etq_UnitHsc.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitHsc.TabIndex = 75
        Me.etq_UnitHsc.Text = "kN"
        '
        'txt_fu
        '
        Me.txt_fu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_fu.Location = New System.Drawing.Point(98, 123)
        Me.txt_fu.Name = "txt_fu"
        Me.txt_fu.Size = New System.Drawing.Size(58, 20)
        Me.txt_fu.TabIndex = 73
        '
        'txt_fy
        '
        Me.txt_fy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_fy.Location = New System.Drawing.Point(98, 97)
        Me.txt_fy.Name = "txt_fy"
        Me.txt_fy.Size = New System.Drawing.Size(58, 20)
        Me.txt_fy.TabIndex = 73
        '
        'txt_d
        '
        Me.txt_d.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_d.Location = New System.Drawing.Point(98, 71)
        Me.txt_d.Name = "txt_d"
        Me.txt_d.Size = New System.Drawing.Size(58, 20)
        Me.txt_d.TabIndex = 73
        '
        'txt_hsc
        '
        Me.txt_hsc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_hsc.Location = New System.Drawing.Point(98, 45)
        Me.txt_hsc.Name = "txt_hsc"
        Me.txt_hsc.Size = New System.Drawing.Size(58, 20)
        Me.txt_hsc.TabIndex = 73
        '
        'img_fu
        '
        Me.img_fu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_fu.Location = New System.Drawing.Point(53, 123)
        Me.img_fu.Name = "img_fu"
        Me.img_fu.Size = New System.Drawing.Size(46, 20)
        Me.img_fu.TabIndex = 74
        Me.img_fu.TabStop = False
        '
        'img_fy
        '
        Me.img_fy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_fy.Location = New System.Drawing.Point(53, 97)
        Me.img_fy.Name = "img_fy"
        Me.img_fy.Size = New System.Drawing.Size(46, 20)
        Me.img_fy.TabIndex = 74
        Me.img_fy.TabStop = False
        '
        'img_d
        '
        Me.img_d.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_d.Location = New System.Drawing.Point(53, 71)
        Me.img_d.Name = "img_d"
        Me.img_d.Size = New System.Drawing.Size(46, 20)
        Me.img_d.TabIndex = 74
        Me.img_d.TabStop = False
        '
        'img_hsc
        '
        Me.img_hsc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_hsc.Location = New System.Drawing.Point(53, 45)
        Me.img_hsc.Name = "img_hsc"
        Me.img_hsc.Size = New System.Drawing.Size(46, 20)
        Me.img_hsc.TabIndex = 74
        Me.img_hsc.TabStop = False
        '
        'cmb_goujons
        '
        Me.cmb_goujons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_goujons.FormattingEnabled = True
        Me.cmb_goujons.Location = New System.Drawing.Point(53, 16)
        Me.cmb_goujons.Name = "cmb_goujons"
        Me.cmb_goujons.Size = New System.Drawing.Size(141, 21)
        Me.cmb_goujons.TabIndex = 0
        '
        'pan_SaisieArmature
        '
        Me.pan_SaisieArmature.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieArmature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieArmature.Controls.Add(Me.lbl_ClasseA)
        Me.pan_SaisieArmature.Controls.Add(Me.cmb_Acier)
        Me.pan_SaisieArmature.Controls.Add(Me.etq_UnitFsk)
        Me.pan_SaisieArmature.Controls.Add(Me.etq_UnitPhiS)
        Me.pan_SaisieArmature.Controls.Add(Me.txt_Fsk)
        Me.pan_SaisieArmature.Controls.Add(Me.img_Fsk)
        Me.pan_SaisieArmature.Controls.Add(Me.txt_PhiS)
        Me.pan_SaisieArmature.Controls.Add(Me.img_PhiS)
        Me.pan_SaisieArmature.Controls.Add(Me.Label1)
        Me.pan_SaisieArmature.Controls.Add(Me.TextBox1)
        Me.pan_SaisieArmature.Controls.Add(Me.PictureBox1)
        Me.pan_SaisieArmature.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieArmature.Location = New System.Drawing.Point(0, 250)
        Me.pan_SaisieArmature.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_SaisieArmature.Name = "pan_SaisieArmature"
        Me.pan_SaisieArmature.Size = New System.Drawing.Size(270, 119)
        Me.pan_SaisieArmature.TabIndex = 2
        '
        'lbl_ClasseA
        '
        Me.lbl_ClasseA.Location = New System.Drawing.Point(16, 56)
        Me.lbl_ClasseA.Name = "lbl_ClasseA"
        Me.lbl_ClasseA.Size = New System.Drawing.Size(76, 18)
        Me.lbl_ClasseA.TabIndex = 85
        Me.lbl_ClasseA.Text = "lbl_ClasseA"
        Me.lbl_ClasseA.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmb_Acier
        '
        Me.cmb_Acier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Acier.FormattingEnabled = True
        Me.cmb_Acier.Location = New System.Drawing.Point(98, 56)
        Me.cmb_Acier.Name = "cmb_Acier"
        Me.cmb_Acier.Size = New System.Drawing.Size(85, 21)
        Me.cmb_Acier.TabIndex = 84
        '
        'etq_UnitFsk
        '
        Me.etq_UnitFsk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitFsk.AutoSize = True
        Me.etq_UnitFsk.Location = New System.Drawing.Point(162, 86)
        Me.etq_UnitFsk.Name = "etq_UnitFsk"
        Me.etq_UnitFsk.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitFsk.TabIndex = 83
        Me.etq_UnitFsk.Text = "kN"
        '
        'etq_UnitPhiS
        '
        Me.etq_UnitPhiS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitPhiS.AutoSize = True
        Me.etq_UnitPhiS.Location = New System.Drawing.Point(162, 21)
        Me.etq_UnitPhiS.Name = "etq_UnitPhiS"
        Me.etq_UnitPhiS.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitPhiS.TabIndex = 83
        Me.etq_UnitPhiS.Text = "kN"
        '
        'txt_Fsk
        '
        Me.txt_Fsk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Fsk.Location = New System.Drawing.Point(98, 82)
        Me.txt_Fsk.Name = "txt_Fsk"
        Me.txt_Fsk.ReadOnly = True
        Me.txt_Fsk.Size = New System.Drawing.Size(58, 20)
        Me.txt_Fsk.TabIndex = 81
        '
        'img_Fsk
        '
        Me.img_Fsk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Fsk.Location = New System.Drawing.Point(53, 82)
        Me.img_Fsk.Name = "img_Fsk"
        Me.img_Fsk.Size = New System.Drawing.Size(46, 20)
        Me.img_Fsk.TabIndex = 82
        Me.img_Fsk.TabStop = False
        '
        'txt_PhiS
        '
        Me.txt_PhiS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PhiS.Location = New System.Drawing.Point(98, 17)
        Me.txt_PhiS.Name = "txt_PhiS"
        Me.txt_PhiS.Size = New System.Drawing.Size(58, 20)
        Me.txt_PhiS.TabIndex = 81
        '
        'img_PhiS
        '
        Me.img_PhiS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PhiS.Location = New System.Drawing.Point(53, 17)
        Me.img_PhiS.Name = "img_PhiS"
        Me.img_PhiS.Size = New System.Drawing.Size(46, 20)
        Me.img_PhiS.TabIndex = 82
        Me.img_PhiS.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(162, 156)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(21, 13)
        Me.Label1.TabIndex = 75
        Me.Label1.Text = "kN"
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(98, 152)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(58, 20)
        Me.TextBox1.TabIndex = 73
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Location = New System.Drawing.Point(53, 152)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(46, 20)
        Me.PictureBox1.TabIndex = 74
        Me.PictureBox1.TabStop = False
        '
        'lbl_Connection
        '
        Me.lbl_Connection.AutoSize = True
        Me.lbl_Connection.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Connection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Connection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Connection.Location = New System.Drawing.Point(0, 370)
        Me.lbl_Connection.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Connection.Name = "lbl_Connection"
        Me.lbl_Connection.Size = New System.Drawing.Size(270, 30)
        Me.lbl_Connection.TabIndex = 0
        Me.lbl_Connection.Text = "lbl_Connection"
        Me.lbl_Connection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieConnection
        '
        Me.pan_SaisieConnection.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieConnection.Controls.Add(Me.lbl_EspacementLongi)
        Me.pan_SaisieConnection.Controls.Add(Me.lbl_NbRows)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_EspLongi_I1)
        Me.pan_SaisieConnection.Controls.Add(Me.cmb_NbRow_I1)
        Me.pan_SaisieConnection.Controls.Add(Me.etq_Somme)
        Me.pan_SaisieConnection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieConnection.Location = New System.Drawing.Point(0, 401)
        Me.pan_SaisieConnection.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.pan_SaisieConnection.Name = "pan_SaisieConnection"
        Me.pan_SaisieConnection.Size = New System.Drawing.Size(270, 97)
        Me.pan_SaisieConnection.TabIndex = 1
        '
        'lbl_EspacementLongi
        '
        Me.lbl_EspacementLongi.Location = New System.Drawing.Point(13, 42)
        Me.lbl_EspacementLongi.Name = "lbl_EspacementLongi"
        Me.lbl_EspacementLongi.Size = New System.Drawing.Size(150, 13)
        Me.lbl_EspacementLongi.TabIndex = 75
        Me.lbl_EspacementLongi.Text = "lbl_EspacementLongi"
        Me.lbl_EspacementLongi.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_NbRows
        '
        Me.lbl_NbRows.Location = New System.Drawing.Point(13, 20)
        Me.lbl_NbRows.Name = "lbl_NbRows"
        Me.lbl_NbRows.Size = New System.Drawing.Size(150, 13)
        Me.lbl_NbRows.TabIndex = 75
        Me.lbl_NbRows.Text = "lbl_NbRows"
        Me.lbl_NbRows.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_EspLongi_I1
        '
        Me.txt_EspLongi_I1.Location = New System.Drawing.Point(167, 40)
        Me.txt_EspLongi_I1.Name = "txt_EspLongi_I1"
        Me.txt_EspLongi_I1.Size = New System.Drawing.Size(67, 20)
        Me.txt_EspLongi_I1.TabIndex = 74
        '
        'cmb_NbRow_I1
        '
        Me.cmb_NbRow_I1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbRow_I1.FormattingEnabled = True
        Me.cmb_NbRow_I1.Location = New System.Drawing.Point(168, 17)
        Me.cmb_NbRow_I1.Name = "cmb_NbRow_I1"
        Me.cmb_NbRow_I1.Size = New System.Drawing.Size(66, 21)
        Me.cmb_NbRow_I1.TabIndex = 61
        '
        'etq_Somme
        '
        Me.etq_Somme.AutoSize = True
        Me.etq_Somme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.etq_Somme.Location = New System.Drawing.Point(35, 71)
        Me.etq_Somme.Name = "etq_Somme"
        Me.etq_Somme.Size = New System.Drawing.Size(72, 13)
        Me.etq_Somme.TabIndex = 60
        Me.etq_Somme.Text = "etq_Somme"
        Me.etq_Somme.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'img_Stud
        '
        Me.img_Stud.Dock = System.Windows.Forms.DockStyle.Fill
        Me.img_Stud.Location = New System.Drawing.Point(0, 0)
        Me.img_Stud.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.img_Stud.Name = "img_Stud"
        Me.img_Stud.Size = New System.Drawing.Size(392, 496)
        Me.img_Stud.TabIndex = 76
        Me.img_Stud.TabStop = False
        '
        'imgList_Navigation
        '
        Me.imgList_Navigation.ImageStream = CType(resources.GetObject("imgList_Navigation.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList_Navigation.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList_Navigation.Images.SetKeyName(0, "Precedent")
        Me.imgList_Navigation.Images.SetKeyName(1, "PrecedentNonDispo")
        Me.imgList_Navigation.Images.SetKeyName(2, "Suivant")
        Me.imgList_Navigation.Images.SetKeyName(3, "SuivantNonDispo")
        '
        'ErrorProvider_Frm_Connection
        '
        Me.ErrorProvider_Frm_Connection.ContainerControl = Me
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(671, 544)
        Me.pan_General.TabIndex = 4
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
        Me.TLpan_Main.Size = New System.Drawing.Size(671, 544)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 5
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 3, 0)
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_Annuler, 1, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 507)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(665, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(345, 3)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(114, 28)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'btn_Annuler
        '
        Me.btn_Annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Annuler.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Annuler.Location = New System.Drawing.Point(205, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_PartieHaute)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(665, 498)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_PartieHaute
        '
        Me.TLPan_PartieHaute.ColumnCount = 2
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 270.0!))
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieHaute.Controls.Add(Me.pan_Droite, 0, 0)
        Me.TLPan_PartieHaute.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_PartieHaute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieHaute.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_PartieHaute.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_PartieHaute.Name = "TLPan_PartieHaute"
        Me.TLPan_PartieHaute.RowCount = 1
        Me.TLPan_PartieHaute.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieHaute.Size = New System.Drawing.Size(665, 498)
        Me.TLPan_PartieHaute.TabIndex = 0
        '
        'pan_Droite
        '
        Me.pan_Droite.AutoScroll = True
        Me.pan_Droite.Controls.Add(Me.TLPan_Droite)
        Me.pan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Droite.Location = New System.Drawing.Point(271, 0)
        Me.pan_Droite.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Droite.Name = "pan_Droite"
        Me.pan_Droite.Size = New System.Drawing.Size(394, 498)
        Me.pan_Droite.TabIndex = 1
        '
        'TLPan_Droite
        '
        Me.TLPan_Droite.ColumnCount = 1
        Me.TLPan_Droite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Droite.Controls.Add(Me.Panel1, 0, 0)
        Me.TLPan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Droite.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Droite.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.TLPan_Droite.Name = "TLPan_Droite"
        Me.TLPan_Droite.RowCount = 1
        Me.TLPan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Droite.Size = New System.Drawing.Size(394, 498)
        Me.TLPan_Droite.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.img_Stud)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(394, 498)
        Me.Panel1.TabIndex = 77
        '
        'Frm_ConnectionSlimFloor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(671, 544)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_ConnectionSlimFloor"
        Me.Text = "Frm_ConnectionSlimFloor"
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_SaisieTypeArma.ResumeLayout(False)
        Me.pan_SaisieTypeArma.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_SaisieGoujons.ResumeLayout(False)
        Me.pan_SaisieGoujons.PerformLayout()
        CType(Me.img_fu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_fy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_d, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_hsc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_SaisieArmature.ResumeLayout(False)
        Me.pan_SaisieArmature.PerformLayout()
        CType(Me.img_Fsk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_PhiS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_SaisieConnection.ResumeLayout(False)
        Me.pan_SaisieConnection.PerformLayout()
        CType(Me.img_Stud, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider_Frm_Connection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_PartieHaute.ResumeLayout(False)
        Me.pan_Droite.ResumeLayout(False)
        Me.TLPan_Droite.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents img_Stud As PictureBox
    Friend WithEvents lbl_Connecteurs As Label
    Friend WithEvents pan_SaisieGoujons As Panel
    Friend WithEvents etq_UnitFu As Label
    Friend WithEvents etq_UnitFy As Label
    Friend WithEvents etq_UnitD As Label
    Friend WithEvents etq_UnitHsc As Label
    Friend WithEvents txt_fu As TextBox
    Friend WithEvents txt_fy As TextBox
    Friend WithEvents txt_d As TextBox
    Friend WithEvents txt_hsc As TextBox
    Friend WithEvents img_fu As PictureBox
    Friend WithEvents img_fy As PictureBox
    Friend WithEvents img_d As PictureBox
    Friend WithEvents img_hsc As PictureBox
    Friend WithEvents cmb_goujons As ComboBox
    Friend WithEvents imgList_Navigation As ImageList
    Friend WithEvents ErrorProvider_Frm_Connection As ErrorProvider
    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_PartieHaute As TableLayoutPanel
    Friend WithEvents pan_Droite As Panel
    Friend WithEvents TLPan_Droite As TableLayoutPanel
    Friend WithEvents lbl_Connection As Label
    Friend WithEvents pan_SaisieConnection As Panel
    Friend WithEvents cmb_NbRow_I1 As ComboBox
    Friend WithEvents etq_Somme As Label
    Friend WithEvents cmb_TypeConnection As ComboBox
    Friend WithEvents txt_EspLongi_I1 As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl_EspacementLongi As Label
    Friend WithEvents lbl_NbRows As Label
    Friend WithEvents pan_SaisieArmature As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents pan_SaisieTypeArma As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents etq_UnitPhiS As Label
    Friend WithEvents txt_PhiS As TextBox
    Friend WithEvents img_PhiS As PictureBox
    Friend WithEvents lbl_ClasseA As Label
    Friend WithEvents cmb_Acier As ComboBox
    Friend WithEvents etq_UnitFsk As Label
    Friend WithEvents txt_Fsk As TextBox
    Friend WithEvents img_Fsk As PictureBox
End Class
