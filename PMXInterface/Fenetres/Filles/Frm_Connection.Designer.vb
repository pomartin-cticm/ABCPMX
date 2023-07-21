<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Connection
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
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_PartieHaute = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Droite = New System.Windows.Forms.Panel()
        Me.TLPan_Droite = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Connection = New System.Windows.Forms.Label()
        Me.pan_ImgConnection = New System.Windows.Forms.Panel()
        Me.img_Connection = New System.Windows.Forms.PictureBox()
        Me.pan_SaisieConnection = New System.Windows.Forms.Panel()
        Me.txt_Portee = New System.Windows.Forms.TextBox()
        Me.cmb_Travee = New System.Windows.Forms.ComboBox()
        Me.txt_EspLongi_I3 = New System.Windows.Forms.TextBox()
        Me.txt_Largeur_I3 = New System.Windows.Forms.TextBox()
        Me.txt_EspLongi_I2 = New System.Windows.Forms.TextBox()
        Me.txt_Largeur_I2 = New System.Windows.Forms.TextBox()
        Me.txt_EspLongi_I1 = New System.Windows.Forms.TextBox()
        Me.txt_Largeur_I1 = New System.Windows.Forms.TextBox()
        Me.cmb_EspLongi_I3 = New System.Windows.Forms.ComboBox()
        Me.cmb_NbRow_I3 = New System.Windows.Forms.ComboBox()
        Me.cmb_EspLongi_I2 = New System.Windows.Forms.ComboBox()
        Me.cmb_EspLongi_I1 = New System.Windows.Forms.ComboBox()
        Me.cmb_NbRow_I2 = New System.Windows.Forms.ComboBox()
        Me.cmb_NbRow_I1 = New System.Windows.Forms.ComboBox()
        Me.etq_Somme = New System.Windows.Forms.Label()
        Me.btn_Supprimer = New System.Windows.Forms.Button()
        Me.btn_Ajouter = New System.Windows.Forms.Button()
        Me.txt_EspacementLongi = New System.Windows.Forms.TextBox()
        Me.txt_NbRows = New System.Windows.Forms.TextBox()
        Me.txt_I3 = New System.Windows.Forms.TextBox()
        Me.txt_I2 = New System.Windows.Forms.TextBox()
        Me.txt_I1 = New System.Windows.Forms.TextBox()
        Me.txt_Largeur = New System.Windows.Forms.TextBox()
        Me.txt_Indice = New System.Windows.Forms.TextBox()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Connecteurs = New System.Windows.Forms.Label()
        Me.pan_SaisieConnecteurs = New System.Windows.Forms.Panel()
        Me.img_Stud = New System.Windows.Forms.PictureBox()
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
        Me.ErrorProvider_Frm_Connection = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_PartieHaute.SuspendLayout()
        Me.pan_Droite.SuspendLayout()
        Me.TLPan_Droite.SuspendLayout()
        Me.pan_ImgConnection.SuspendLayout()
        CType(Me.img_Connection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_SaisieConnection.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_SaisieConnecteurs.SuspendLayout()
        CType(Me.img_Stud, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_fu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_fy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_d, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_hsc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider_Frm_Connection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(789, 432)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(789, 432)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 395)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(783, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(404, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(264, 3)
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
        Me.pan_Main.Size = New System.Drawing.Size(783, 386)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_PartieHaute
        '
        Me.TLPan_PartieHaute.ColumnCount = 2
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieHaute.Controls.Add(Me.pan_Droite, 0, 0)
        Me.TLPan_PartieHaute.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_PartieHaute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieHaute.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_PartieHaute.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_PartieHaute.Name = "TLPan_PartieHaute"
        Me.TLPan_PartieHaute.RowCount = 1
        Me.TLPan_PartieHaute.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieHaute.Size = New System.Drawing.Size(783, 386)
        Me.TLPan_PartieHaute.TabIndex = 0
        '
        'pan_Droite
        '
        Me.pan_Droite.AutoScroll = True
        Me.pan_Droite.Controls.Add(Me.TLPan_Droite)
        Me.pan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Droite.Location = New System.Drawing.Point(250, 0)
        Me.pan_Droite.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Droite.Name = "pan_Droite"
        Me.pan_Droite.Size = New System.Drawing.Size(533, 386)
        Me.pan_Droite.TabIndex = 1
        '
        'TLPan_Droite
        '
        Me.TLPan_Droite.ColumnCount = 1
        Me.TLPan_Droite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Droite.Controls.Add(Me.lbl_Connection, 0, 0)
        Me.TLPan_Droite.Controls.Add(Me.pan_ImgConnection, 0, 1)
        Me.TLPan_Droite.Controls.Add(Me.pan_SaisieConnection, 0, 2)
        Me.TLPan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Droite.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Droite.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.TLPan_Droite.Name = "TLPan_Droite"
        Me.TLPan_Droite.RowCount = 3
        Me.TLPan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Droite.Size = New System.Drawing.Size(533, 386)
        Me.TLPan_Droite.TabIndex = 0
        '
        'lbl_Connection
        '
        Me.lbl_Connection.AutoSize = True
        Me.lbl_Connection.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Connection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Connection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Connection.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Connection.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Connection.Name = "lbl_Connection"
        Me.lbl_Connection.Size = New System.Drawing.Size(533, 30)
        Me.lbl_Connection.TabIndex = 0
        Me.lbl_Connection.Text = "lbl_Connection"
        Me.lbl_Connection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_ImgConnection
        '
        Me.pan_ImgConnection.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ImgConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ImgConnection.Controls.Add(Me.img_Connection)
        Me.pan_ImgConnection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ImgConnection.Location = New System.Drawing.Point(0, 30)
        Me.pan_ImgConnection.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ImgConnection.Name = "pan_ImgConnection"
        Me.pan_ImgConnection.Size = New System.Drawing.Size(533, 178)
        Me.pan_ImgConnection.TabIndex = 2
        '
        'img_Connection
        '
        Me.img_Connection.Location = New System.Drawing.Point(98, 9)
        Me.img_Connection.Margin = New System.Windows.Forms.Padding(0)
        Me.img_Connection.Name = "img_Connection"
        Me.img_Connection.Size = New System.Drawing.Size(141, 83)
        Me.img_Connection.TabIndex = 75
        Me.img_Connection.TabStop = False
        '
        'pan_SaisieConnection
        '
        Me.pan_SaisieConnection.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieConnection.Controls.Add(Me.txt_Portee)
        Me.pan_SaisieConnection.Controls.Add(Me.cmb_Travee)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_EspLongi_I3)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_Largeur_I3)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_EspLongi_I2)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_Largeur_I2)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_EspLongi_I1)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_Largeur_I1)
        Me.pan_SaisieConnection.Controls.Add(Me.cmb_EspLongi_I3)
        Me.pan_SaisieConnection.Controls.Add(Me.cmb_NbRow_I3)
        Me.pan_SaisieConnection.Controls.Add(Me.cmb_EspLongi_I2)
        Me.pan_SaisieConnection.Controls.Add(Me.cmb_EspLongi_I1)
        Me.pan_SaisieConnection.Controls.Add(Me.cmb_NbRow_I2)
        Me.pan_SaisieConnection.Controls.Add(Me.cmb_NbRow_I1)
        Me.pan_SaisieConnection.Controls.Add(Me.etq_Somme)
        Me.pan_SaisieConnection.Controls.Add(Me.btn_Supprimer)
        Me.pan_SaisieConnection.Controls.Add(Me.btn_Ajouter)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_EspacementLongi)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_NbRows)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_I3)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_I2)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_I1)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_Largeur)
        Me.pan_SaisieConnection.Controls.Add(Me.txt_Indice)
        Me.pan_SaisieConnection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieConnection.Location = New System.Drawing.Point(0, 208)
        Me.pan_SaisieConnection.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisieConnection.Name = "pan_SaisieConnection"
        Me.pan_SaisieConnection.Size = New System.Drawing.Size(533, 178)
        Me.pan_SaisieConnection.TabIndex = 1
        '
        'txt_Portee
        '
        Me.txt_Portee.BackColor = System.Drawing.SystemColors.Window
        Me.txt_Portee.Location = New System.Drawing.Point(20, 21)
        Me.txt_Portee.Name = "txt_Portee"
        Me.txt_Portee.ReadOnly = True
        Me.txt_Portee.Size = New System.Drawing.Size(147, 20)
        Me.txt_Portee.TabIndex = 76
        Me.txt_Portee.TabStop = False
        Me.txt_Portee.Text = "txt_Portee"
        Me.txt_Portee.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmb_Travee
        '
        Me.cmb_Travee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Travee.FormattingEnabled = True
        Me.cmb_Travee.Location = New System.Drawing.Point(173, 20)
        Me.cmb_Travee.Name = "cmb_Travee"
        Me.cmb_Travee.Size = New System.Drawing.Size(133, 21)
        Me.cmb_Travee.TabIndex = 75
        '
        'txt_EspLongi_I3
        '
        Me.txt_EspLongi_I3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EspLongi_I3.Location = New System.Drawing.Point(307, 153)
        Me.txt_EspLongi_I3.Name = "txt_EspLongi_I3"
        Me.txt_EspLongi_I3.Size = New System.Drawing.Size(66, 20)
        Me.txt_EspLongi_I3.TabIndex = 74
        '
        'txt_Largeur_I3
        '
        Me.txt_Largeur_I3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Largeur_I3.Location = New System.Drawing.Point(307, 88)
        Me.txt_Largeur_I3.Name = "txt_Largeur_I3"
        Me.txt_Largeur_I3.Size = New System.Drawing.Size(66, 20)
        Me.txt_Largeur_I3.TabIndex = 74
        '
        'txt_EspLongi_I2
        '
        Me.txt_EspLongi_I2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EspLongi_I2.Location = New System.Drawing.Point(240, 153)
        Me.txt_EspLongi_I2.Name = "txt_EspLongi_I2"
        Me.txt_EspLongi_I2.Size = New System.Drawing.Size(66, 20)
        Me.txt_EspLongi_I2.TabIndex = 74
        '
        'txt_Largeur_I2
        '
        Me.txt_Largeur_I2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Largeur_I2.Location = New System.Drawing.Point(240, 88)
        Me.txt_Largeur_I2.Name = "txt_Largeur_I2"
        Me.txt_Largeur_I2.Size = New System.Drawing.Size(66, 20)
        Me.txt_Largeur_I2.TabIndex = 74
        '
        'txt_EspLongi_I1
        '
        Me.txt_EspLongi_I1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EspLongi_I1.Location = New System.Drawing.Point(173, 153)
        Me.txt_EspLongi_I1.Name = "txt_EspLongi_I1"
        Me.txt_EspLongi_I1.Size = New System.Drawing.Size(66, 20)
        Me.txt_EspLongi_I1.TabIndex = 74
        '
        'txt_Largeur_I1
        '
        Me.txt_Largeur_I1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Largeur_I1.Location = New System.Drawing.Point(173, 88)
        Me.txt_Largeur_I1.Name = "txt_Largeur_I1"
        Me.txt_Largeur_I1.Size = New System.Drawing.Size(66, 20)
        Me.txt_Largeur_I1.TabIndex = 74
        '
        'cmb_EspLongi_I3
        '
        Me.cmb_EspLongi_I3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_EspLongi_I3.FormattingEnabled = True
        Me.cmb_EspLongi_I3.Location = New System.Drawing.Point(307, 129)
        Me.cmb_EspLongi_I3.Name = "cmb_EspLongi_I3"
        Me.cmb_EspLongi_I3.Size = New System.Drawing.Size(66, 21)
        Me.cmb_EspLongi_I3.TabIndex = 63
        '
        'cmb_NbRow_I3
        '
        Me.cmb_NbRow_I3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbRow_I3.FormattingEnabled = True
        Me.cmb_NbRow_I3.Location = New System.Drawing.Point(307, 107)
        Me.cmb_NbRow_I3.Name = "cmb_NbRow_I3"
        Me.cmb_NbRow_I3.Size = New System.Drawing.Size(66, 21)
        Me.cmb_NbRow_I3.TabIndex = 63
        '
        'cmb_EspLongi_I2
        '
        Me.cmb_EspLongi_I2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_EspLongi_I2.FormattingEnabled = True
        Me.cmb_EspLongi_I2.Location = New System.Drawing.Point(240, 129)
        Me.cmb_EspLongi_I2.Name = "cmb_EspLongi_I2"
        Me.cmb_EspLongi_I2.Size = New System.Drawing.Size(66, 21)
        Me.cmb_EspLongi_I2.TabIndex = 62
        '
        'cmb_EspLongi_I1
        '
        Me.cmb_EspLongi_I1.FormattingEnabled = True
        Me.cmb_EspLongi_I1.Location = New System.Drawing.Point(173, 129)
        Me.cmb_EspLongi_I1.Name = "cmb_EspLongi_I1"
        Me.cmb_EspLongi_I1.Size = New System.Drawing.Size(66, 21)
        Me.cmb_EspLongi_I1.TabIndex = 61
        '
        'cmb_NbRow_I2
        '
        Me.cmb_NbRow_I2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbRow_I2.FormattingEnabled = True
        Me.cmb_NbRow_I2.Location = New System.Drawing.Point(240, 107)
        Me.cmb_NbRow_I2.Name = "cmb_NbRow_I2"
        Me.cmb_NbRow_I2.Size = New System.Drawing.Size(66, 21)
        Me.cmb_NbRow_I2.TabIndex = 62
        '
        'cmb_NbRow_I1
        '
        Me.cmb_NbRow_I1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbRow_I1.FormattingEnabled = True
        Me.cmb_NbRow_I1.Location = New System.Drawing.Point(173, 107)
        Me.cmb_NbRow_I1.Name = "cmb_NbRow_I1"
        Me.cmb_NbRow_I1.Size = New System.Drawing.Size(66, 21)
        Me.cmb_NbRow_I1.TabIndex = 61
        '
        'etq_Somme
        '
        Me.etq_Somme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.etq_Somme.Location = New System.Drawing.Point(389, 74)
        Me.etq_Somme.Name = "etq_Somme"
        Me.etq_Somme.Size = New System.Drawing.Size(100, 14)
        Me.etq_Somme.TabIndex = 60
        Me.etq_Somme.Text = "etq_Somme"
        Me.etq_Somme.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btn_Supprimer
        '
        Me.btn_Supprimer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Supprimer.Location = New System.Drawing.Point(392, 127)
        Me.btn_Supprimer.Name = "btn_Supprimer"
        Me.btn_Supprimer.Size = New System.Drawing.Size(96, 24)
        Me.btn_Supprimer.TabIndex = 59
        Me.btn_Supprimer.Text = "btn_Supprimer"
        Me.btn_Supprimer.UseVisualStyleBackColor = True
        '
        'btn_Ajouter
        '
        Me.btn_Ajouter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Ajouter.Location = New System.Drawing.Point(392, 97)
        Me.btn_Ajouter.Name = "btn_Ajouter"
        Me.btn_Ajouter.Size = New System.Drawing.Size(96, 24)
        Me.btn_Ajouter.TabIndex = 58
        Me.btn_Ajouter.Text = "btn_Ajouter"
        Me.btn_Ajouter.UseVisualStyleBackColor = True
        '
        'txt_EspacementLongi
        '
        Me.txt_EspacementLongi.BackColor = System.Drawing.SystemColors.Window
        Me.txt_EspacementLongi.Location = New System.Drawing.Point(25, 130)
        Me.txt_EspacementLongi.Name = "txt_EspacementLongi"
        Me.txt_EspacementLongi.ReadOnly = True
        Me.txt_EspacementLongi.Size = New System.Drawing.Size(147, 20)
        Me.txt_EspacementLongi.TabIndex = 53
        Me.txt_EspacementLongi.TabStop = False
        Me.txt_EspacementLongi.Text = "txt_EspacementLongi"
        Me.txt_EspacementLongi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_NbRows
        '
        Me.txt_NbRows.BackColor = System.Drawing.SystemColors.Window
        Me.txt_NbRows.Location = New System.Drawing.Point(25, 109)
        Me.txt_NbRows.Name = "txt_NbRows"
        Me.txt_NbRows.ReadOnly = True
        Me.txt_NbRows.Size = New System.Drawing.Size(147, 20)
        Me.txt_NbRows.TabIndex = 52
        Me.txt_NbRows.TabStop = False
        Me.txt_NbRows.Text = "txt_NbRows"
        Me.txt_NbRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_I3
        '
        Me.txt_I3.BackColor = System.Drawing.SystemColors.Window
        Me.txt_I3.Location = New System.Drawing.Point(307, 67)
        Me.txt_I3.Name = "txt_I3"
        Me.txt_I3.ReadOnly = True
        Me.txt_I3.Size = New System.Drawing.Size(66, 20)
        Me.txt_I3.TabIndex = 51
        Me.txt_I3.TabStop = False
        Me.txt_I3.Text = "3"
        Me.txt_I3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_I2
        '
        Me.txt_I2.BackColor = System.Drawing.SystemColors.Window
        Me.txt_I2.Location = New System.Drawing.Point(240, 67)
        Me.txt_I2.Name = "txt_I2"
        Me.txt_I2.ReadOnly = True
        Me.txt_I2.Size = New System.Drawing.Size(66, 20)
        Me.txt_I2.TabIndex = 50
        Me.txt_I2.TabStop = False
        Me.txt_I2.Text = "2"
        Me.txt_I2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_I1
        '
        Me.txt_I1.BackColor = System.Drawing.SystemColors.Window
        Me.txt_I1.Location = New System.Drawing.Point(173, 67)
        Me.txt_I1.Name = "txt_I1"
        Me.txt_I1.ReadOnly = True
        Me.txt_I1.Size = New System.Drawing.Size(66, 20)
        Me.txt_I1.TabIndex = 49
        Me.txt_I1.TabStop = False
        Me.txt_I1.Text = "1"
        Me.txt_I1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_Largeur
        '
        Me.txt_Largeur.BackColor = System.Drawing.SystemColors.Window
        Me.txt_Largeur.Location = New System.Drawing.Point(25, 88)
        Me.txt_Largeur.Name = "txt_Largeur"
        Me.txt_Largeur.ReadOnly = True
        Me.txt_Largeur.Size = New System.Drawing.Size(147, 20)
        Me.txt_Largeur.TabIndex = 48
        Me.txt_Largeur.TabStop = False
        Me.txt_Largeur.Text = "txt_Largeur"
        Me.txt_Largeur.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_Indice
        '
        Me.txt_Indice.BackColor = System.Drawing.SystemColors.Window
        Me.txt_Indice.Location = New System.Drawing.Point(145, 67)
        Me.txt_Indice.Name = "txt_Indice"
        Me.txt_Indice.ReadOnly = True
        Me.txt_Indice.Size = New System.Drawing.Size(27, 20)
        Me.txt_Indice.TabIndex = 47
        Me.txt_Indice.TabStop = False
        Me.txt_Indice.Text = "i"
        Me.txt_Indice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(250, 386)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Connecteurs, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisieConnecteurs, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 2
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(250, 386)
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
        Me.lbl_Connecteurs.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Connecteurs.TabIndex = 0
        Me.lbl_Connecteurs.Text = "lbl_Connecteurs"
        Me.lbl_Connecteurs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieConnecteurs
        '
        Me.pan_SaisieConnecteurs.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieConnecteurs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieConnecteurs.Controls.Add(Me.img_Stud)
        Me.pan_SaisieConnecteurs.Controls.Add(Me.etq_UnitFu)
        Me.pan_SaisieConnecteurs.Controls.Add(Me.etq_UnitFy)
        Me.pan_SaisieConnecteurs.Controls.Add(Me.etq_UnitD)
        Me.pan_SaisieConnecteurs.Controls.Add(Me.etq_UnitHsc)
        Me.pan_SaisieConnecteurs.Controls.Add(Me.txt_fu)
        Me.pan_SaisieConnecteurs.Controls.Add(Me.txt_fy)
        Me.pan_SaisieConnecteurs.Controls.Add(Me.txt_d)
        Me.pan_SaisieConnecteurs.Controls.Add(Me.txt_hsc)
        Me.pan_SaisieConnecteurs.Controls.Add(Me.img_fu)
        Me.pan_SaisieConnecteurs.Controls.Add(Me.img_fy)
        Me.pan_SaisieConnecteurs.Controls.Add(Me.img_d)
        Me.pan_SaisieConnecteurs.Controls.Add(Me.img_hsc)
        Me.pan_SaisieConnecteurs.Controls.Add(Me.cmb_goujons)
        Me.pan_SaisieConnecteurs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieConnecteurs.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisieConnecteurs.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisieConnecteurs.Name = "pan_SaisieConnecteurs"
        Me.pan_SaisieConnecteurs.Size = New System.Drawing.Size(250, 356)
        Me.pan_SaisieConnecteurs.TabIndex = 1
        '
        'img_Stud
        '
        Me.img_Stud.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Stud.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Stud.Location = New System.Drawing.Point(37, 186)
        Me.img_Stud.Name = "img_Stud"
        Me.img_Stud.Size = New System.Drawing.Size(188, 127)
        Me.img_Stud.TabIndex = 76
        Me.img_Stud.TabStop = False
        '
        'etq_UnitFu
        '
        Me.etq_UnitFu.AutoSize = True
        Me.etq_UnitFu.Location = New System.Drawing.Point(147, 131)
        Me.etq_UnitFu.Name = "etq_UnitFu"
        Me.etq_UnitFu.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitFu.TabIndex = 75
        Me.etq_UnitFu.Text = "kN"
        '
        'etq_UnitFy
        '
        Me.etq_UnitFy.AutoSize = True
        Me.etq_UnitFy.Location = New System.Drawing.Point(147, 105)
        Me.etq_UnitFy.Name = "etq_UnitFy"
        Me.etq_UnitFy.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitFy.TabIndex = 75
        Me.etq_UnitFy.Text = "kN"
        '
        'etq_UnitD
        '
        Me.etq_UnitD.AutoSize = True
        Me.etq_UnitD.Location = New System.Drawing.Point(147, 79)
        Me.etq_UnitD.Name = "etq_UnitD"
        Me.etq_UnitD.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitD.TabIndex = 75
        Me.etq_UnitD.Text = "kN"
        '
        'etq_UnitHsc
        '
        Me.etq_UnitHsc.AutoSize = True
        Me.etq_UnitHsc.Location = New System.Drawing.Point(147, 53)
        Me.etq_UnitHsc.Name = "etq_UnitHsc"
        Me.etq_UnitHsc.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitHsc.TabIndex = 75
        Me.etq_UnitHsc.Text = "kN"
        '
        'txt_fu
        '
        Me.txt_fu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_fu.Location = New System.Drawing.Point(83, 127)
        Me.txt_fu.Name = "txt_fu"
        Me.txt_fu.Size = New System.Drawing.Size(58, 20)
        Me.txt_fu.TabIndex = 73
        '
        'txt_fy
        '
        Me.txt_fy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_fy.Location = New System.Drawing.Point(83, 101)
        Me.txt_fy.Name = "txt_fy"
        Me.txt_fy.Size = New System.Drawing.Size(58, 20)
        Me.txt_fy.TabIndex = 73
        '
        'txt_d
        '
        Me.txt_d.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_d.Location = New System.Drawing.Point(83, 75)
        Me.txt_d.Name = "txt_d"
        Me.txt_d.Size = New System.Drawing.Size(58, 20)
        Me.txt_d.TabIndex = 73
        '
        'txt_hsc
        '
        Me.txt_hsc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_hsc.Location = New System.Drawing.Point(83, 49)
        Me.txt_hsc.Name = "txt_hsc"
        Me.txt_hsc.Size = New System.Drawing.Size(58, 20)
        Me.txt_hsc.TabIndex = 73
        '
        'img_fu
        '
        Me.img_fu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_fu.Location = New System.Drawing.Point(38, 127)
        Me.img_fu.Name = "img_fu"
        Me.img_fu.Size = New System.Drawing.Size(46, 20)
        Me.img_fu.TabIndex = 74
        Me.img_fu.TabStop = False
        '
        'img_fy
        '
        Me.img_fy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_fy.Location = New System.Drawing.Point(38, 101)
        Me.img_fy.Name = "img_fy"
        Me.img_fy.Size = New System.Drawing.Size(46, 20)
        Me.img_fy.TabIndex = 74
        Me.img_fy.TabStop = False
        '
        'img_d
        '
        Me.img_d.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_d.Location = New System.Drawing.Point(38, 75)
        Me.img_d.Name = "img_d"
        Me.img_d.Size = New System.Drawing.Size(46, 20)
        Me.img_d.TabIndex = 74
        Me.img_d.TabStop = False
        '
        'img_hsc
        '
        Me.img_hsc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_hsc.Location = New System.Drawing.Point(38, 49)
        Me.img_hsc.Name = "img_hsc"
        Me.img_hsc.Size = New System.Drawing.Size(46, 20)
        Me.img_hsc.TabIndex = 74
        Me.img_hsc.TabStop = False
        '
        'cmb_goujons
        '
        Me.cmb_goujons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_goujons.FormattingEnabled = True
        Me.cmb_goujons.Location = New System.Drawing.Point(37, 12)
        Me.cmb_goujons.Name = "cmb_goujons"
        Me.cmb_goujons.Size = New System.Drawing.Size(174, 21)
        Me.cmb_goujons.TabIndex = 0
        '
        'ErrorProvider_Frm_Connection
        '
        Me.ErrorProvider_Frm_Connection.ContainerControl = Me
        '
        'Frm_Connection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(789, 432)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Frm_Connection"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Connection"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_PartieHaute.ResumeLayout(False)
        Me.pan_Droite.ResumeLayout(False)
        Me.TLPan_Droite.ResumeLayout(False)
        Me.TLPan_Droite.PerformLayout()
        Me.pan_ImgConnection.ResumeLayout(False)
        CType(Me.img_Connection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_SaisieConnection.ResumeLayout(False)
        Me.pan_SaisieConnection.PerformLayout()
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_SaisieConnecteurs.ResumeLayout(False)
        Me.pan_SaisieConnecteurs.PerformLayout()
        CType(Me.img_Stud, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_fu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_fy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_d, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_hsc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider_Frm_Connection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_PartieHaute As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Connecteurs As Label
    Friend WithEvents pan_SaisieConnecteurs As Panel
    Friend WithEvents pan_Droite As Panel
    Friend WithEvents TLPan_Droite As TableLayoutPanel
    Friend WithEvents lbl_Connection As Label
    Friend WithEvents pan_SaisieConnection As Panel
    Friend WithEvents cmb_goujons As ComboBox
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
    Friend WithEvents pan_ImgConnection As Panel
    Friend WithEvents img_Connection As PictureBox
    Friend WithEvents etq_Somme As Label
    Friend WithEvents btn_Supprimer As Button
    Friend WithEvents btn_Ajouter As Button
    Friend WithEvents txt_EspacementLongi As TextBox
    Friend WithEvents txt_NbRows As TextBox
    Friend WithEvents txt_I3 As TextBox
    Friend WithEvents txt_I2 As TextBox
    Friend WithEvents txt_I1 As TextBox
    Friend WithEvents txt_Largeur As TextBox
    Friend WithEvents txt_Indice As TextBox
    Friend WithEvents txt_Largeur_I3 As TextBox
    Friend WithEvents txt_Largeur_I2 As TextBox
    Friend WithEvents txt_Largeur_I1 As TextBox
    Friend WithEvents cmb_EspLongi_I3 As ComboBox
    Friend WithEvents cmb_NbRow_I3 As ComboBox
    Friend WithEvents cmb_EspLongi_I2 As ComboBox
    Friend WithEvents cmb_EspLongi_I1 As ComboBox
    Friend WithEvents cmb_NbRow_I2 As ComboBox
    Friend WithEvents cmb_NbRow_I1 As ComboBox
    Friend WithEvents img_Stud As PictureBox
    Friend WithEvents txt_Portee As TextBox
    Friend WithEvents cmb_Travee As ComboBox
    Friend WithEvents txt_EspLongi_I3 As TextBox
    Friend WithEvents txt_EspLongi_I2 As TextBox
    Friend WithEvents txt_EspLongi_I1 As TextBox
    Friend WithEvents ErrorProvider_Frm_Connection As ErrorProvider
End Class
