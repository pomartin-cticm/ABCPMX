<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Portees
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
        Me.TLPan_Portees = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Entraxe = New System.Windows.Forms.Label()
        Me.lbl_Portees = New System.Windows.Forms.Label()
        Me.pan_SaisiePortee = New System.Windows.Forms.Panel()
        Me.etq_UnitL3 = New System.Windows.Forms.Label()
        Me.txt_PorteeConsoleD = New System.Windows.Forms.TextBox()
        Me.img_L3 = New System.Windows.Forms.PictureBox()
        Me.chk_ConsoleDroite = New System.Windows.Forms.CheckBox()
        Me.etq_UnitL2 = New System.Windows.Forms.Label()
        Me.txt_PorteeConsoleG = New System.Windows.Forms.TextBox()
        Me.img_L2 = New System.Windows.Forms.PictureBox()
        Me.chk_ConsoleGauche = New System.Windows.Forms.CheckBox()
        Me.etq_UnitL1 = New System.Windows.Forms.Label()
        Me.txt_MainSpan = New System.Windows.Forms.TextBox()
        Me.img_L1 = New System.Windows.Forms.PictureBox()
        Me.lbl_MainSpan = New System.Windows.Forms.Label()
        Me.pan_Coupe = New System.Windows.Forms.Panel()
        Me.etq_UnitL5 = New System.Windows.Forms.Label()
        Me.txt_D2 = New System.Windows.Forms.TextBox()
        Me.img_D2 = New System.Windows.Forms.PictureBox()
        Me.lbl_Entraxes = New System.Windows.Forms.Label()
        Me.etq_UnitL4 = New System.Windows.Forms.Label()
        Me.txt_D1 = New System.Windows.Forms.TextBox()
        Me.img_D1 = New System.Windows.Forms.PictureBox()
        Me.rad_Rive = New System.Windows.Forms.RadioButton()
        Me.rad_Intermediaire = New System.Windows.Forms.RadioButton()
        Me.TLpan_Droite = New System.Windows.Forms.TableLayoutPanel()
        Me.img_Portees = New System.Windows.Forms.PictureBox()
        Me.TLPan_Tremies = New System.Windows.Forms.TableLayoutPanel()
        Me.img_Coupe = New System.Windows.Forms.PictureBox()
        Me.pan_Milieu = New System.Windows.Forms.Panel()
        Me.TLpan_Milieu = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Tremies = New System.Windows.Forms.Label()
        Me.pan_Tremies = New System.Windows.Forms.Panel()
        Me.etq_UnitL7 = New System.Windows.Forms.Label()
        Me.etq_UnitL6 = New System.Windows.Forms.Label()
        Me.txt_TremieDroite = New System.Windows.Forms.TextBox()
        Me.img_TremieDroite = New System.Windows.Forms.PictureBox()
        Me.txt_TremieGauche = New System.Windows.Forms.TextBox()
        Me.chk_TremieDroite = New System.Windows.Forms.CheckBox()
        Me.img_TremieGauche = New System.Windows.Forms.PictureBox()
        Me.chk_TremieGauche = New System.Windows.Forms.CheckBox()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Portees.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_SaisiePortee.SuspendLayout()
        CType(Me.img_L3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_L2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_L1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Coupe.SuspendLayout()
        CType(Me.img_D2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_D1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLpan_Droite.SuspendLayout()
        CType(Me.img_Portees, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLPan_Tremies.SuspendLayout()
        CType(Me.img_Coupe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Milieu.SuspendLayout()
        Me.TLpan_Milieu.SuspendLayout()
        Me.pan_Tremies.SuspendLayout()
        CType(Me.img_TremieDroite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_TremieGauche, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(954, 428)
        Me.pan_General.TabIndex = 1
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
        Me.TLpan_Main.Size = New System.Drawing.Size(954, 428)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 391)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(948, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(487, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(347, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Main.Controls.Add(Me.TLPan_Portees)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(948, 382)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Portees
        '
        Me.TLPan_Portees.ColumnCount = 2
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Portees.Controls.Add(Me.TLpan_Droite, 1, 0)
        Me.TLPan_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Portees.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Portees.Name = "TLPan_Portees"
        Me.TLPan_Portees.RowCount = 1
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Size = New System.Drawing.Size(946, 380)
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
        Me.pan_Gauche.Size = New System.Drawing.Size(250, 380)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Entraxe, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Portees, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisiePortee, 0, 1)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Coupe, 0, 3)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 5
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(250, 380)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Entraxe
        '
        Me.lbl_Entraxe.AutoSize = True
        Me.lbl_Entraxe.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Entraxe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Entraxe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Entraxe.Location = New System.Drawing.Point(0, 200)
        Me.lbl_Entraxe.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Entraxe.Name = "lbl_Entraxe"
        Me.lbl_Entraxe.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Entraxe.TabIndex = 2
        Me.lbl_Entraxe.Text = "lbl_Entraxe"
        Me.lbl_Entraxe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Portees
        '
        Me.lbl_Portees.AutoSize = True
        Me.lbl_Portees.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Portees.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Portees.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Portees.Name = "lbl_Portees"
        Me.lbl_Portees.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Portees.TabIndex = 0
        Me.lbl_Portees.Text = "lbl_Portee"
        Me.lbl_Portees.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisiePortee
        '
        Me.pan_SaisiePortee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisiePortee.Controls.Add(Me.etq_UnitL3)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_PorteeConsoleD)
        Me.pan_SaisiePortee.Controls.Add(Me.img_L3)
        Me.pan_SaisiePortee.Controls.Add(Me.chk_ConsoleDroite)
        Me.pan_SaisiePortee.Controls.Add(Me.etq_UnitL2)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_PorteeConsoleG)
        Me.pan_SaisiePortee.Controls.Add(Me.img_L2)
        Me.pan_SaisiePortee.Controls.Add(Me.chk_ConsoleGauche)
        Me.pan_SaisiePortee.Controls.Add(Me.etq_UnitL1)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_MainSpan)
        Me.pan_SaisiePortee.Controls.Add(Me.img_L1)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_MainSpan)
        Me.pan_SaisiePortee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisiePortee.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisiePortee.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_SaisiePortee.Name = "pan_SaisiePortee"
        Me.pan_SaisiePortee.Size = New System.Drawing.Size(250, 169)
        Me.pan_SaisiePortee.TabIndex = 1
        '
        'etq_UnitL3
        '
        Me.etq_UnitL3.AutoSize = True
        Me.etq_UnitL3.Location = New System.Drawing.Point(200, 137)
        Me.etq_UnitL3.Name = "etq_UnitL3"
        Me.etq_UnitL3.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL3.TabIndex = 80
        Me.etq_UnitL3.Text = "kN"
        '
        'txt_PorteeConsoleD
        '
        Me.txt_PorteeConsoleD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PorteeConsoleD.Location = New System.Drawing.Point(136, 133)
        Me.txt_PorteeConsoleD.Name = "txt_PorteeConsoleD"
        Me.txt_PorteeConsoleD.Size = New System.Drawing.Size(58, 20)
        Me.txt_PorteeConsoleD.TabIndex = 78
        '
        'img_L3
        '
        Me.img_L3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_L3.Location = New System.Drawing.Point(91, 133)
        Me.img_L3.Name = "img_L3"
        Me.img_L3.Size = New System.Drawing.Size(46, 20)
        Me.img_L3.TabIndex = 79
        Me.img_L3.TabStop = False
        '
        'chk_ConsoleDroite
        '
        Me.chk_ConsoleDroite.AutoSize = True
        Me.chk_ConsoleDroite.Location = New System.Drawing.Point(7, 110)
        Me.chk_ConsoleDroite.Name = "chk_ConsoleDroite"
        Me.chk_ConsoleDroite.Size = New System.Drawing.Size(116, 17)
        Me.chk_ConsoleDroite.TabIndex = 77
        Me.chk_ConsoleDroite.Text = "chk_ConsoleDroite"
        Me.chk_ConsoleDroite.UseVisualStyleBackColor = True
        '
        'etq_UnitL2
        '
        Me.etq_UnitL2.AutoSize = True
        Me.etq_UnitL2.Location = New System.Drawing.Point(200, 81)
        Me.etq_UnitL2.Name = "etq_UnitL2"
        Me.etq_UnitL2.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL2.TabIndex = 76
        Me.etq_UnitL2.Text = "kN"
        '
        'txt_PorteeConsoleG
        '
        Me.txt_PorteeConsoleG.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PorteeConsoleG.Location = New System.Drawing.Point(136, 77)
        Me.txt_PorteeConsoleG.Name = "txt_PorteeConsoleG"
        Me.txt_PorteeConsoleG.Size = New System.Drawing.Size(58, 20)
        Me.txt_PorteeConsoleG.TabIndex = 74
        '
        'img_L2
        '
        Me.img_L2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_L2.Location = New System.Drawing.Point(91, 77)
        Me.img_L2.Name = "img_L2"
        Me.img_L2.Size = New System.Drawing.Size(46, 20)
        Me.img_L2.TabIndex = 75
        Me.img_L2.TabStop = False
        '
        'chk_ConsoleGauche
        '
        Me.chk_ConsoleGauche.AutoSize = True
        Me.chk_ConsoleGauche.Location = New System.Drawing.Point(7, 54)
        Me.chk_ConsoleGauche.Name = "chk_ConsoleGauche"
        Me.chk_ConsoleGauche.Size = New System.Drawing.Size(126, 17)
        Me.chk_ConsoleGauche.TabIndex = 73
        Me.chk_ConsoleGauche.Text = "chk_ConsoleGauche"
        Me.chk_ConsoleGauche.UseVisualStyleBackColor = True
        '
        'etq_UnitL1
        '
        Me.etq_UnitL1.AutoSize = True
        Me.etq_UnitL1.Location = New System.Drawing.Point(200, 29)
        Me.etq_UnitL1.Name = "etq_UnitL1"
        Me.etq_UnitL1.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL1.TabIndex = 72
        Me.etq_UnitL1.Text = "kN"
        '
        'txt_MainSpan
        '
        Me.txt_MainSpan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_MainSpan.Location = New System.Drawing.Point(136, 25)
        Me.txt_MainSpan.Name = "txt_MainSpan"
        Me.txt_MainSpan.Size = New System.Drawing.Size(58, 20)
        Me.txt_MainSpan.TabIndex = 70
        '
        'img_L1
        '
        Me.img_L1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_L1.Location = New System.Drawing.Point(91, 25)
        Me.img_L1.Name = "img_L1"
        Me.img_L1.Size = New System.Drawing.Size(46, 20)
        Me.img_L1.TabIndex = 71
        Me.img_L1.TabStop = False
        '
        'lbl_MainSpan
        '
        Me.lbl_MainSpan.AutoSize = True
        Me.lbl_MainSpan.Location = New System.Drawing.Point(7, 6)
        Me.lbl_MainSpan.Name = "lbl_MainSpan"
        Me.lbl_MainSpan.Size = New System.Drawing.Size(71, 13)
        Me.lbl_MainSpan.TabIndex = 0
        Me.lbl_MainSpan.Text = "lbl_MainSpan"
        '
        'pan_Coupe
        '
        Me.pan_Coupe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Coupe.Controls.Add(Me.etq_UnitL5)
        Me.pan_Coupe.Controls.Add(Me.txt_D2)
        Me.pan_Coupe.Controls.Add(Me.img_D2)
        Me.pan_Coupe.Controls.Add(Me.lbl_Entraxes)
        Me.pan_Coupe.Controls.Add(Me.etq_UnitL4)
        Me.pan_Coupe.Controls.Add(Me.txt_D1)
        Me.pan_Coupe.Controls.Add(Me.img_D1)
        Me.pan_Coupe.Controls.Add(Me.rad_Rive)
        Me.pan_Coupe.Controls.Add(Me.rad_Intermediaire)
        Me.pan_Coupe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Coupe.Location = New System.Drawing.Point(0, 230)
        Me.pan_Coupe.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Coupe.Name = "pan_Coupe"
        Me.pan_Coupe.Size = New System.Drawing.Size(250, 150)
        Me.pan_Coupe.TabIndex = 3
        '
        'etq_UnitL5
        '
        Me.etq_UnitL5.AutoSize = True
        Me.etq_UnitL5.Location = New System.Drawing.Point(191, 112)
        Me.etq_UnitL5.Name = "etq_UnitL5"
        Me.etq_UnitL5.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL5.TabIndex = 79
        Me.etq_UnitL5.Text = "kN"
        '
        'txt_D2
        '
        Me.txt_D2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_D2.Location = New System.Drawing.Point(127, 108)
        Me.txt_D2.Name = "txt_D2"
        Me.txt_D2.Size = New System.Drawing.Size(58, 20)
        Me.txt_D2.TabIndex = 77
        '
        'img_D2
        '
        Me.img_D2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_D2.Location = New System.Drawing.Point(82, 108)
        Me.img_D2.Name = "img_D2"
        Me.img_D2.Size = New System.Drawing.Size(46, 20)
        Me.img_D2.TabIndex = 78
        Me.img_D2.TabStop = False
        '
        'lbl_Entraxes
        '
        Me.lbl_Entraxes.AutoSize = True
        Me.lbl_Entraxes.Location = New System.Drawing.Point(7, 64)
        Me.lbl_Entraxes.Name = "lbl_Entraxes"
        Me.lbl_Entraxes.Size = New System.Drawing.Size(64, 13)
        Me.lbl_Entraxes.TabIndex = 76
        Me.lbl_Entraxes.Text = "lbl_Entraxes"
        '
        'etq_UnitL4
        '
        Me.etq_UnitL4.AutoSize = True
        Me.etq_UnitL4.Location = New System.Drawing.Point(191, 86)
        Me.etq_UnitL4.Name = "etq_UnitL4"
        Me.etq_UnitL4.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL4.TabIndex = 75
        Me.etq_UnitL4.Text = "kN"
        '
        'txt_D1
        '
        Me.txt_D1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_D1.Location = New System.Drawing.Point(127, 82)
        Me.txt_D1.Name = "txt_D1"
        Me.txt_D1.Size = New System.Drawing.Size(58, 20)
        Me.txt_D1.TabIndex = 73
        '
        'img_D1
        '
        Me.img_D1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_D1.Location = New System.Drawing.Point(82, 82)
        Me.img_D1.Name = "img_D1"
        Me.img_D1.Size = New System.Drawing.Size(46, 20)
        Me.img_D1.TabIndex = 74
        Me.img_D1.TabStop = False
        '
        'rad_Rive
        '
        Me.rad_Rive.AutoSize = True
        Me.rad_Rive.Location = New System.Drawing.Point(10, 37)
        Me.rad_Rive.Name = "rad_Rive"
        Me.rad_Rive.Size = New System.Drawing.Size(68, 17)
        Me.rad_Rive.TabIndex = 1
        Me.rad_Rive.TabStop = True
        Me.rad_Rive.Text = "rad_Rive"
        Me.rad_Rive.UseVisualStyleBackColor = True
        '
        'rad_Intermediaire
        '
        Me.rad_Intermediaire.AutoSize = True
        Me.rad_Intermediaire.Location = New System.Drawing.Point(10, 14)
        Me.rad_Intermediaire.Name = "rad_Intermediaire"
        Me.rad_Intermediaire.Size = New System.Drawing.Size(106, 17)
        Me.rad_Intermediaire.TabIndex = 0
        Me.rad_Intermediaire.TabStop = True
        Me.rad_Intermediaire.Text = "rad_Intermediaire"
        Me.rad_Intermediaire.UseVisualStyleBackColor = True
        '
        'TLpan_Droite
        '
        Me.TLpan_Droite.ColumnCount = 1
        Me.TLpan_Droite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Droite.Controls.Add(Me.img_Portees, 0, 0)
        Me.TLpan_Droite.Controls.Add(Me.TLPan_Tremies, 0, 1)
        Me.TLpan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Droite.Location = New System.Drawing.Point(250, 0)
        Me.TLpan_Droite.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Droite.Name = "TLpan_Droite"
        Me.TLpan_Droite.RowCount = 2
        Me.TLpan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TLpan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Droite.Size = New System.Drawing.Size(696, 380)
        Me.TLpan_Droite.TabIndex = 1
        '
        'img_Portees
        '
        Me.img_Portees.Location = New System.Drawing.Point(1, 0)
        Me.img_Portees.Margin = New System.Windows.Forms.Padding(1, 0, 0, 1)
        Me.img_Portees.Name = "img_Portees"
        Me.img_Portees.Size = New System.Drawing.Size(100, 50)
        Me.img_Portees.TabIndex = 1
        Me.img_Portees.TabStop = False
        '
        'TLPan_Tremies
        '
        Me.TLPan_Tremies.ColumnCount = 2
        Me.TLPan_Tremies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_Tremies.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Tremies.Controls.Add(Me.img_Coupe, 1, 0)
        Me.TLPan_Tremies.Controls.Add(Me.pan_Milieu, 0, 0)
        Me.TLPan_Tremies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Tremies.Location = New System.Drawing.Point(0, 200)
        Me.TLPan_Tremies.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Tremies.Name = "TLPan_Tremies"
        Me.TLPan_Tremies.RowCount = 1
        Me.TLPan_Tremies.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Tremies.Size = New System.Drawing.Size(696, 180)
        Me.TLPan_Tremies.TabIndex = 2
        '
        'img_Coupe
        '
        Me.img_Coupe.Location = New System.Drawing.Point(251, 0)
        Me.img_Coupe.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.img_Coupe.Name = "img_Coupe"
        Me.img_Coupe.Size = New System.Drawing.Size(100, 50)
        Me.img_Coupe.TabIndex = 2
        Me.img_Coupe.TabStop = False
        '
        'pan_Milieu
        '
        Me.pan_Milieu.AutoScroll = True
        Me.pan_Milieu.Controls.Add(Me.TLpan_Milieu)
        Me.pan_Milieu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Milieu.Location = New System.Drawing.Point(1, 0)
        Me.pan_Milieu.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Milieu.Name = "pan_Milieu"
        Me.pan_Milieu.Size = New System.Drawing.Size(249, 180)
        Me.pan_Milieu.TabIndex = 4
        '
        'TLpan_Milieu
        '
        Me.TLpan_Milieu.ColumnCount = 1
        Me.TLpan_Milieu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Milieu.Controls.Add(Me.lbl_Tremies, 0, 0)
        Me.TLpan_Milieu.Controls.Add(Me.pan_Tremies, 0, 1)
        Me.TLpan_Milieu.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLpan_Milieu.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Milieu.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Milieu.Name = "TLpan_Milieu"
        Me.TLpan_Milieu.RowCount = 3
        Me.TLpan_Milieu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Milieu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TLpan_Milieu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Milieu.Size = New System.Drawing.Size(249, 180)
        Me.TLpan_Milieu.TabIndex = 3
        '
        'lbl_Tremies
        '
        Me.lbl_Tremies.AutoSize = True
        Me.lbl_Tremies.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Tremies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Tremies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Tremies.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Tremies.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Tremies.Name = "lbl_Tremies"
        Me.lbl_Tremies.Size = New System.Drawing.Size(249, 30)
        Me.lbl_Tremies.TabIndex = 3
        Me.lbl_Tremies.Text = "lbl_Tremies"
        Me.lbl_Tremies.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Tremies
        '
        Me.pan_Tremies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Tremies.Controls.Add(Me.etq_UnitL7)
        Me.pan_Tremies.Controls.Add(Me.etq_UnitL6)
        Me.pan_Tremies.Controls.Add(Me.txt_TremieDroite)
        Me.pan_Tremies.Controls.Add(Me.img_TremieDroite)
        Me.pan_Tremies.Controls.Add(Me.txt_TremieGauche)
        Me.pan_Tremies.Controls.Add(Me.chk_TremieDroite)
        Me.pan_Tremies.Controls.Add(Me.img_TremieGauche)
        Me.pan_Tremies.Controls.Add(Me.chk_TremieGauche)
        Me.pan_Tremies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Tremies.Location = New System.Drawing.Point(0, 30)
        Me.pan_Tremies.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Tremies.Name = "pan_Tremies"
        Me.pan_Tremies.Size = New System.Drawing.Size(249, 150)
        Me.pan_Tremies.TabIndex = 4
        '
        'etq_UnitL7
        '
        Me.etq_UnitL7.AutoSize = True
        Me.etq_UnitL7.Location = New System.Drawing.Point(187, 100)
        Me.etq_UnitL7.Name = "etq_UnitL7"
        Me.etq_UnitL7.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL7.TabIndex = 83
        Me.etq_UnitL7.Text = "kN"
        '
        'etq_UnitL6
        '
        Me.etq_UnitL6.AutoSize = True
        Me.etq_UnitL6.Location = New System.Drawing.Point(187, 43)
        Me.etq_UnitL6.Name = "etq_UnitL6"
        Me.etq_UnitL6.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL6.TabIndex = 83
        Me.etq_UnitL6.Text = "kN"
        '
        'txt_TremieDroite
        '
        Me.txt_TremieDroite.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TremieDroite.Location = New System.Drawing.Point(123, 96)
        Me.txt_TremieDroite.Name = "txt_TremieDroite"
        Me.txt_TremieDroite.Size = New System.Drawing.Size(58, 20)
        Me.txt_TremieDroite.TabIndex = 81
        '
        'img_TremieDroite
        '
        Me.img_TremieDroite.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_TremieDroite.Location = New System.Drawing.Point(78, 96)
        Me.img_TremieDroite.Name = "img_TremieDroite"
        Me.img_TremieDroite.Size = New System.Drawing.Size(46, 20)
        Me.img_TremieDroite.TabIndex = 82
        Me.img_TremieDroite.TabStop = False
        '
        'txt_TremieGauche
        '
        Me.txt_TremieGauche.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TremieGauche.Location = New System.Drawing.Point(123, 39)
        Me.txt_TremieGauche.Name = "txt_TremieGauche"
        Me.txt_TremieGauche.Size = New System.Drawing.Size(58, 20)
        Me.txt_TremieGauche.TabIndex = 81
        '
        'chk_TremieDroite
        '
        Me.chk_TremieDroite.AutoSize = True
        Me.chk_TremieDroite.Location = New System.Drawing.Point(7, 73)
        Me.chk_TremieDroite.Name = "chk_TremieDroite"
        Me.chk_TremieDroite.Size = New System.Drawing.Size(110, 17)
        Me.chk_TremieDroite.TabIndex = 80
        Me.chk_TremieDroite.Text = "chk_TremieDroite"
        Me.chk_TremieDroite.UseVisualStyleBackColor = True
        '
        'img_TremieGauche
        '
        Me.img_TremieGauche.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_TremieGauche.Location = New System.Drawing.Point(78, 39)
        Me.img_TremieGauche.Name = "img_TremieGauche"
        Me.img_TremieGauche.Size = New System.Drawing.Size(46, 20)
        Me.img_TremieGauche.TabIndex = 82
        Me.img_TremieGauche.TabStop = False
        '
        'chk_TremieGauche
        '
        Me.chk_TremieGauche.AutoSize = True
        Me.chk_TremieGauche.Location = New System.Drawing.Point(7, 16)
        Me.chk_TremieGauche.Name = "chk_TremieGauche"
        Me.chk_TremieGauche.Size = New System.Drawing.Size(120, 17)
        Me.chk_TremieGauche.TabIndex = 80
        Me.chk_TremieGauche.Text = "chk_TremieGauche"
        Me.chk_TremieGauche.UseVisualStyleBackColor = True
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Frm_Portees
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(954, 428)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Portees"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Portees"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Portees.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_SaisiePortee.ResumeLayout(False)
        Me.pan_SaisiePortee.PerformLayout()
        CType(Me.img_L3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_L2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_L1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Coupe.ResumeLayout(False)
        Me.pan_Coupe.PerformLayout()
        CType(Me.img_D2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_D1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLpan_Droite.ResumeLayout(False)
        CType(Me.img_Portees, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLPan_Tremies.ResumeLayout(False)
        CType(Me.img_Coupe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Milieu.ResumeLayout(False)
        Me.TLpan_Milieu.ResumeLayout(False)
        Me.TLpan_Milieu.PerformLayout()
        Me.pan_Tremies.ResumeLayout(False)
        Me.pan_Tremies.PerformLayout()
        CType(Me.img_TremieDroite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_TremieGauche, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Portees As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Portees As Label
    Friend WithEvents pan_SaisiePortee As Panel
    Friend WithEvents img_Portees As PictureBox
    Friend WithEvents etq_UnitL3 As Label
    Friend WithEvents txt_PorteeConsoleD As TextBox
    Friend WithEvents img_L3 As PictureBox
    Friend WithEvents chk_ConsoleDroite As CheckBox
    Friend WithEvents etq_UnitL2 As Label
    Friend WithEvents txt_PorteeConsoleG As TextBox
    Friend WithEvents img_L2 As PictureBox
    Friend WithEvents chk_ConsoleGauche As CheckBox
    Friend WithEvents etq_UnitL1 As Label
    Friend WithEvents txt_MainSpan As TextBox
    Friend WithEvents img_L1 As PictureBox
    Friend WithEvents lbl_MainSpan As Label
    Friend WithEvents lbl_Entraxe As Label
    Friend WithEvents TLpan_Droite As TableLayoutPanel
    Friend WithEvents img_Coupe As PictureBox
    Friend WithEvents pan_Coupe As Panel
    Friend WithEvents etq_UnitL5 As Label
    Friend WithEvents txt_D2 As TextBox
    Friend WithEvents img_D2 As PictureBox
    Friend WithEvents lbl_Entraxes As Label
    Friend WithEvents etq_UnitL4 As Label
    Friend WithEvents txt_D1 As TextBox
    Friend WithEvents img_D1 As PictureBox
    Friend WithEvents rad_Rive As RadioButton
    Friend WithEvents rad_Intermediaire As RadioButton
    Friend WithEvents chk_TremieGauche As CheckBox
    Friend WithEvents TLPan_Tremies As TableLayoutPanel
    Friend WithEvents pan_Milieu As Panel
    Friend WithEvents TLpan_Milieu As TableLayoutPanel
    Friend WithEvents lbl_Tremies As Label
    Friend WithEvents pan_Tremies As Panel
    Friend WithEvents etq_UnitL6 As Label
    Friend WithEvents txt_TremieGauche As TextBox
    Friend WithEvents img_TremieGauche As PictureBox
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents etq_UnitL7 As Label
    Friend WithEvents txt_TremieDroite As TextBox
    Friend WithEvents img_TremieDroite As PictureBox
    Friend WithEvents chk_TremieDroite As CheckBox
End Class
