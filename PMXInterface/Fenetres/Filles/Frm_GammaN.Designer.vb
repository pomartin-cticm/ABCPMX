<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_GammaN
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
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_PartieHaute = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Resistance = New System.Windows.Forms.Panel()
        Me.TLPan_Resistance = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Resistance = New System.Windows.Forms.Label()
        Me.pan_SaisieResistance = New System.Windows.Forms.Panel()
        Me.pan_Accompagnement = New System.Windows.Forms.Panel()
        Me.TLPan_Accompagnement = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Accompagnement = New System.Windows.Forms.Label()
        Me.pan_SaisieAccompagnement = New System.Windows.Forms.Panel()
        Me.lbl_Q2 = New System.Windows.Forms.Label()
        Me.lbl_Q1 = New System.Windows.Forms.Label()
        Me.txt_Psi2_Q2 = New System.Windows.Forms.TextBox()
        Me.txt_Psi2_Q1 = New System.Windows.Forms.TextBox()
        Me.txt_Psi1_Q2 = New System.Windows.Forms.TextBox()
        Me.txt_Psi1_Q1 = New System.Windows.Forms.TextBox()
        Me.txt_Psi0_Q2 = New System.Windows.Forms.TextBox()
        Me.txt_Psi0_Q1 = New System.Windows.Forms.TextBox()
        Me.pan_Chargement = New System.Windows.Forms.Panel()
        Me.TLPan_Chargement = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Reset = New System.Windows.Forms.Label()
        Me.lbl_Chargement = New System.Windows.Forms.Label()
        Me.pan_SaisieChargement = New System.Windows.Forms.Panel()
        Me.txt_GammaQ = New System.Windows.Forms.TextBox()
        Me.txt_GammaGinf = New System.Windows.Forms.TextBox()
        Me.txt_GammaGsup = New System.Windows.Forms.TextBox()
        Me.pan_Reset = New System.Windows.Forms.Panel()
        Me.btn_Reini = New System.Windows.Forms.Button()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.TLpan_ChoixResistance = New System.Windows.Forms.TableLayoutPanel()
        Me.rdb_Acier = New System.Windows.Forms.RadioButton()
        Me.rdb_Beton = New System.Windows.Forms.RadioButton()
        Me.rdb_Feu = New System.Windows.Forms.RadioButton()
        Me.img_Q2 = New System.Windows.Forms.PictureBox()
        Me.img_Q1 = New System.Windows.Forms.PictureBox()
        Me.img_Psi2 = New System.Windows.Forms.PictureBox()
        Me.img_Psi1 = New System.Windows.Forms.PictureBox()
        Me.img_Psi0 = New System.Windows.Forms.PictureBox()
        Me.img_GammaQ = New System.Windows.Forms.PictureBox()
        Me.img_GammaGinf = New System.Windows.Forms.PictureBox()
        Me.img_GammaGsup = New System.Windows.Forms.PictureBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_PartieHaute.SuspendLayout()
        Me.pan_Resistance.SuspendLayout()
        Me.TLPan_Resistance.SuspendLayout()
        Me.pan_Accompagnement.SuspendLayout()
        Me.TLPan_Accompagnement.SuspendLayout()
        Me.pan_SaisieAccompagnement.SuspendLayout()
        Me.pan_Chargement.SuspendLayout()
        Me.TLPan_Chargement.SuspendLayout()
        Me.pan_SaisieChargement.SuspendLayout()
        Me.pan_Reset.SuspendLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLpan_ChoixResistance.SuspendLayout()
        CType(Me.img_Q2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Q1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Psi2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Psi1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Psi0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaQ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaGinf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_GammaGsup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(624, 285)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(624, 285)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 248)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(618, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(322, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(182, 3)
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
        Me.pan_Main.Size = New System.Drawing.Size(618, 239)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_PartieHaute
        '
        Me.TLPan_PartieHaute.ColumnCount = 3
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLPan_PartieHaute.Controls.Add(Me.pan_Resistance, 0, 0)
        Me.TLPan_PartieHaute.Controls.Add(Me.pan_Accompagnement, 0, 0)
        Me.TLPan_PartieHaute.Controls.Add(Me.pan_Chargement, 0, 0)
        Me.TLPan_PartieHaute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieHaute.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_PartieHaute.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_PartieHaute.Name = "TLPan_PartieHaute"
        Me.TLPan_PartieHaute.RowCount = 1
        Me.TLPan_PartieHaute.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieHaute.Size = New System.Drawing.Size(618, 239)
        Me.TLPan_PartieHaute.TabIndex = 0
        '
        'pan_Resistance
        '
        Me.pan_Resistance.AutoScroll = True
        Me.pan_Resistance.Controls.Add(Me.TLPan_Resistance)
        Me.pan_Resistance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Resistance.Location = New System.Drawing.Point(410, 0)
        Me.pan_Resistance.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Resistance.Name = "pan_Resistance"
        Me.pan_Resistance.Size = New System.Drawing.Size(208, 239)
        Me.pan_Resistance.TabIndex = 2
        '
        'TLPan_Resistance
        '
        Me.TLPan_Resistance.ColumnCount = 1
        Me.TLPan_Resistance.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Resistance.Controls.Add(Me.TLpan_ChoixResistance, 0, 1)
        Me.TLPan_Resistance.Controls.Add(Me.lbl_Resistance, 0, 0)
        Me.TLPan_Resistance.Controls.Add(Me.pan_SaisieResistance, 0, 2)
        Me.TLPan_Resistance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Resistance.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Resistance.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Resistance.Name = "TLPan_Resistance"
        Me.TLPan_Resistance.RowCount = 3
        Me.TLPan_Resistance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Resistance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Resistance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_Resistance.Size = New System.Drawing.Size(208, 239)
        Me.TLPan_Resistance.TabIndex = 0
        '
        'lbl_Resistance
        '
        Me.lbl_Resistance.AutoSize = True
        Me.lbl_Resistance.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Resistance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Resistance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Resistance.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Resistance.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Resistance.Name = "lbl_Resistance"
        Me.lbl_Resistance.Size = New System.Drawing.Size(208, 30)
        Me.lbl_Resistance.TabIndex = 0
        Me.lbl_Resistance.Text = "lbl_Resistance"
        Me.lbl_Resistance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieResistance
        '
        Me.pan_SaisieResistance.BackColor = System.Drawing.SystemColors.Control
        Me.pan_SaisieResistance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieResistance.Location = New System.Drawing.Point(0, 60)
        Me.pan_SaisieResistance.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisieResistance.Name = "pan_SaisieResistance"
        Me.pan_SaisieResistance.Size = New System.Drawing.Size(208, 179)
        Me.pan_SaisieResistance.TabIndex = 1
        '
        'pan_Accompagnement
        '
        Me.pan_Accompagnement.AutoScroll = True
        Me.pan_Accompagnement.Controls.Add(Me.TLPan_Accompagnement)
        Me.pan_Accompagnement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Accompagnement.Location = New System.Drawing.Point(206, 0)
        Me.pan_Accompagnement.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.pan_Accompagnement.Name = "pan_Accompagnement"
        Me.pan_Accompagnement.Size = New System.Drawing.Size(203, 239)
        Me.pan_Accompagnement.TabIndex = 1
        '
        'TLPan_Accompagnement
        '
        Me.TLPan_Accompagnement.ColumnCount = 1
        Me.TLPan_Accompagnement.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Accompagnement.Controls.Add(Me.lbl_Accompagnement, 0, 0)
        Me.TLPan_Accompagnement.Controls.Add(Me.pan_SaisieAccompagnement, 0, 1)
        Me.TLPan_Accompagnement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Accompagnement.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Accompagnement.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Accompagnement.Name = "TLPan_Accompagnement"
        Me.TLPan_Accompagnement.RowCount = 2
        Me.TLPan_Accompagnement.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Accompagnement.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_Accompagnement.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_Accompagnement.Size = New System.Drawing.Size(203, 239)
        Me.TLPan_Accompagnement.TabIndex = 0
        '
        'lbl_Accompagnement
        '
        Me.lbl_Accompagnement.AutoSize = True
        Me.lbl_Accompagnement.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Accompagnement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Accompagnement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Accompagnement.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Accompagnement.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Accompagnement.Name = "lbl_Accompagnement"
        Me.lbl_Accompagnement.Size = New System.Drawing.Size(203, 30)
        Me.lbl_Accompagnement.TabIndex = 0
        Me.lbl_Accompagnement.Text = "lbl_Accompagnement"
        Me.lbl_Accompagnement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieAccompagnement
        '
        Me.pan_SaisieAccompagnement.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieAccompagnement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieAccompagnement.Controls.Add(Me.img_Q2)
        Me.pan_SaisieAccompagnement.Controls.Add(Me.img_Q1)
        Me.pan_SaisieAccompagnement.Controls.Add(Me.lbl_Q2)
        Me.pan_SaisieAccompagnement.Controls.Add(Me.lbl_Q1)
        Me.pan_SaisieAccompagnement.Controls.Add(Me.txt_Psi2_Q2)
        Me.pan_SaisieAccompagnement.Controls.Add(Me.txt_Psi2_Q1)
        Me.pan_SaisieAccompagnement.Controls.Add(Me.img_Psi2)
        Me.pan_SaisieAccompagnement.Controls.Add(Me.txt_Psi1_Q2)
        Me.pan_SaisieAccompagnement.Controls.Add(Me.txt_Psi1_Q1)
        Me.pan_SaisieAccompagnement.Controls.Add(Me.txt_Psi0_Q2)
        Me.pan_SaisieAccompagnement.Controls.Add(Me.txt_Psi0_Q1)
        Me.pan_SaisieAccompagnement.Controls.Add(Me.img_Psi1)
        Me.pan_SaisieAccompagnement.Controls.Add(Me.img_Psi0)
        Me.pan_SaisieAccompagnement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieAccompagnement.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisieAccompagnement.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisieAccompagnement.Name = "pan_SaisieAccompagnement"
        Me.pan_SaisieAccompagnement.Size = New System.Drawing.Size(203, 209)
        Me.pan_SaisieAccompagnement.TabIndex = 1
        '
        'lbl_Q2
        '
        Me.lbl_Q2.AutoSize = True
        Me.lbl_Q2.Location = New System.Drawing.Point(11, 172)
        Me.lbl_Q2.Name = "lbl_Q2"
        Me.lbl_Q2.Size = New System.Drawing.Size(37, 13)
        Me.lbl_Q2.TabIndex = 92
        Me.lbl_Q2.Text = "lbl_Q2"
        '
        'lbl_Q1
        '
        Me.lbl_Q1.AutoSize = True
        Me.lbl_Q1.Location = New System.Drawing.Point(11, 146)
        Me.lbl_Q1.Name = "lbl_Q1"
        Me.lbl_Q1.Size = New System.Drawing.Size(37, 13)
        Me.lbl_Q1.TabIndex = 91
        Me.lbl_Q1.Text = "lbl_Q1"
        '
        'txt_Psi2_Q2
        '
        Me.txt_Psi2_Q2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Psi2_Q2.Location = New System.Drawing.Point(126, 103)
        Me.txt_Psi2_Q2.Name = "txt_Psi2_Q2"
        Me.txt_Psi2_Q2.Size = New System.Drawing.Size(58, 20)
        Me.txt_Psi2_Q2.TabIndex = 90
        '
        'txt_Psi2_Q1
        '
        Me.txt_Psi2_Q1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Psi2_Q1.Location = New System.Drawing.Point(62, 103)
        Me.txt_Psi2_Q1.Name = "txt_Psi2_Q1"
        Me.txt_Psi2_Q1.Size = New System.Drawing.Size(58, 20)
        Me.txt_Psi2_Q1.TabIndex = 89
        '
        'txt_Psi1_Q2
        '
        Me.txt_Psi1_Q2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Psi1_Q2.Location = New System.Drawing.Point(126, 77)
        Me.txt_Psi1_Q2.Name = "txt_Psi1_Q2"
        Me.txt_Psi1_Q2.Size = New System.Drawing.Size(58, 20)
        Me.txt_Psi1_Q2.TabIndex = 85
        '
        'txt_Psi1_Q1
        '
        Me.txt_Psi1_Q1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Psi1_Q1.Location = New System.Drawing.Point(62, 77)
        Me.txt_Psi1_Q1.Name = "txt_Psi1_Q1"
        Me.txt_Psi1_Q1.Size = New System.Drawing.Size(58, 20)
        Me.txt_Psi1_Q1.TabIndex = 84
        '
        'txt_Psi0_Q2
        '
        Me.txt_Psi0_Q2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Psi0_Q2.Location = New System.Drawing.Point(126, 51)
        Me.txt_Psi0_Q2.Name = "txt_Psi0_Q2"
        Me.txt_Psi0_Q2.Size = New System.Drawing.Size(58, 20)
        Me.txt_Psi0_Q2.TabIndex = 83
        '
        'txt_Psi0_Q1
        '
        Me.txt_Psi0_Q1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Psi0_Q1.Location = New System.Drawing.Point(62, 51)
        Me.txt_Psi0_Q1.Name = "txt_Psi0_Q1"
        Me.txt_Psi0_Q1.Size = New System.Drawing.Size(58, 20)
        Me.txt_Psi0_Q1.TabIndex = 82
        '
        'pan_Chargement
        '
        Me.pan_Chargement.AutoScroll = True
        Me.pan_Chargement.Controls.Add(Me.TLPan_Chargement)
        Me.pan_Chargement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Chargement.Location = New System.Drawing.Point(0, 0)
        Me.pan_Chargement.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Chargement.Name = "pan_Chargement"
        Me.pan_Chargement.Size = New System.Drawing.Size(205, 239)
        Me.pan_Chargement.TabIndex = 0
        '
        'TLPan_Chargement
        '
        Me.TLPan_Chargement.ColumnCount = 1
        Me.TLPan_Chargement.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Chargement.Controls.Add(Me.lbl_Reset, 0, 2)
        Me.TLPan_Chargement.Controls.Add(Me.lbl_Chargement, 0, 0)
        Me.TLPan_Chargement.Controls.Add(Me.pan_SaisieChargement, 0, 1)
        Me.TLPan_Chargement.Controls.Add(Me.pan_Reset, 0, 3)
        Me.TLPan_Chargement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Chargement.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Chargement.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Chargement.Name = "TLPan_Chargement"
        Me.TLPan_Chargement.RowCount = 4
        Me.TLPan_Chargement.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Chargement.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.TLPan_Chargement.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Chargement.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Chargement.Size = New System.Drawing.Size(205, 239)
        Me.TLPan_Chargement.TabIndex = 0
        '
        'lbl_Reset
        '
        Me.lbl_Reset.AutoSize = True
        Me.lbl_Reset.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Reset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Reset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Reset.Location = New System.Drawing.Point(0, 160)
        Me.lbl_Reset.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Reset.Name = "lbl_Reset"
        Me.lbl_Reset.Size = New System.Drawing.Size(205, 30)
        Me.lbl_Reset.TabIndex = 2
        Me.lbl_Reset.Text = "lbl_Reset"
        Me.lbl_Reset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Chargement
        '
        Me.lbl_Chargement.AutoSize = True
        Me.lbl_Chargement.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Chargement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Chargement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Chargement.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Chargement.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Chargement.Name = "lbl_Chargement"
        Me.lbl_Chargement.Size = New System.Drawing.Size(205, 30)
        Me.lbl_Chargement.TabIndex = 0
        Me.lbl_Chargement.Text = "lbl_Chargement"
        Me.lbl_Chargement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieChargement
        '
        Me.pan_SaisieChargement.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieChargement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieChargement.Controls.Add(Me.txt_GammaQ)
        Me.pan_SaisieChargement.Controls.Add(Me.img_GammaQ)
        Me.pan_SaisieChargement.Controls.Add(Me.txt_GammaGinf)
        Me.pan_SaisieChargement.Controls.Add(Me.img_GammaGinf)
        Me.pan_SaisieChargement.Controls.Add(Me.txt_GammaGsup)
        Me.pan_SaisieChargement.Controls.Add(Me.img_GammaGsup)
        Me.pan_SaisieChargement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieChargement.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisieChargement.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_SaisieChargement.Name = "pan_SaisieChargement"
        Me.pan_SaisieChargement.Size = New System.Drawing.Size(205, 129)
        Me.pan_SaisieChargement.TabIndex = 1
        '
        'txt_GammaQ
        '
        Me.txt_GammaQ.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_GammaQ.Location = New System.Drawing.Point(90, 77)
        Me.txt_GammaQ.Name = "txt_GammaQ"
        Me.txt_GammaQ.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaQ.TabIndex = 77
        '
        'txt_GammaGinf
        '
        Me.txt_GammaGinf.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_GammaGinf.Location = New System.Drawing.Point(90, 51)
        Me.txt_GammaGinf.Name = "txt_GammaGinf"
        Me.txt_GammaGinf.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaGinf.TabIndex = 75
        '
        'txt_GammaGsup
        '
        Me.txt_GammaGsup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_GammaGsup.Location = New System.Drawing.Point(90, 25)
        Me.txt_GammaGsup.Name = "txt_GammaGsup"
        Me.txt_GammaGsup.Size = New System.Drawing.Size(58, 20)
        Me.txt_GammaGsup.TabIndex = 73
        '
        'pan_Reset
        '
        Me.pan_Reset.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Reset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Reset.Controls.Add(Me.btn_Reini)
        Me.pan_Reset.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Reset.Location = New System.Drawing.Point(0, 190)
        Me.pan_Reset.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Reset.Name = "pan_Reset"
        Me.pan_Reset.Size = New System.Drawing.Size(205, 49)
        Me.pan_Reset.TabIndex = 3
        '
        'btn_Reini
        '
        Me.btn_Reini.Location = New System.Drawing.Point(9, 13)
        Me.btn_Reini.Name = "btn_Reini"
        Me.btn_Reini.Size = New System.Drawing.Size(185, 23)
        Me.btn_Reini.TabIndex = 79
        Me.btn_Reini.Text = "btn_Reini"
        Me.btn_Reini.UseVisualStyleBackColor = True
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'TLpan_ChoixResistance
        '
        Me.TLpan_ChoixResistance.ColumnCount = 3
        Me.TLpan_ChoixResistance.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_ChoixResistance.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_ChoixResistance.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_ChoixResistance.Controls.Add(Me.rdb_Feu, 2, 0)
        Me.TLpan_ChoixResistance.Controls.Add(Me.rdb_Beton, 1, 0)
        Me.TLpan_ChoixResistance.Controls.Add(Me.rdb_Acier, 0, 0)
        Me.TLpan_ChoixResistance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_ChoixResistance.Location = New System.Drawing.Point(0, 30)
        Me.TLpan_ChoixResistance.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_ChoixResistance.Name = "TLpan_ChoixResistance"
        Me.TLpan_ChoixResistance.RowCount = 1
        Me.TLpan_ChoixResistance.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_ChoixResistance.Size = New System.Drawing.Size(208, 30)
        Me.TLpan_ChoixResistance.TabIndex = 0
        '
        'rdb_Acier
        '
        Me.rdb_Acier.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_Acier.AutoSize = True
        Me.rdb_Acier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_Acier.Location = New System.Drawing.Point(0, 0)
        Me.rdb_Acier.Margin = New System.Windows.Forms.Padding(0)
        Me.rdb_Acier.Name = "rdb_Acier"
        Me.rdb_Acier.Size = New System.Drawing.Size(69, 30)
        Me.rdb_Acier.TabIndex = 2
        Me.rdb_Acier.Text = "rdb_Acier"
        Me.rdb_Acier.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_Acier.UseVisualStyleBackColor = True
        '
        'rdb_Beton
        '
        Me.rdb_Beton.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_Beton.AutoSize = True
        Me.rdb_Beton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_Beton.Location = New System.Drawing.Point(69, 0)
        Me.rdb_Beton.Margin = New System.Windows.Forms.Padding(0)
        Me.rdb_Beton.Name = "rdb_Beton"
        Me.rdb_Beton.Size = New System.Drawing.Size(69, 30)
        Me.rdb_Beton.TabIndex = 3
        Me.rdb_Beton.Text = "rdb_Beton"
        Me.rdb_Beton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_Beton.UseVisualStyleBackColor = True
        '
        'rdb_Feu
        '
        Me.rdb_Feu.Appearance = System.Windows.Forms.Appearance.Button
        Me.rdb_Feu.AutoSize = True
        Me.rdb_Feu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rdb_Feu.Location = New System.Drawing.Point(138, 0)
        Me.rdb_Feu.Margin = New System.Windows.Forms.Padding(0)
        Me.rdb_Feu.Name = "rdb_Feu"
        Me.rdb_Feu.Size = New System.Drawing.Size(70, 30)
        Me.rdb_Feu.TabIndex = 4
        Me.rdb_Feu.Text = "rdb_Feu"
        Me.rdb_Feu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rdb_Feu.UseVisualStyleBackColor = True
        '
        'img_Q2
        '
        Me.img_Q2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Q2.Location = New System.Drawing.Point(126, 25)
        Me.img_Q2.Name = "img_Q2"
        Me.img_Q2.Size = New System.Drawing.Size(58, 20)
        Me.img_Q2.TabIndex = 94
        Me.img_Q2.TabStop = False
        '
        'img_Q1
        '
        Me.img_Q1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Q1.Location = New System.Drawing.Point(62, 25)
        Me.img_Q1.Name = "img_Q1"
        Me.img_Q1.Size = New System.Drawing.Size(58, 20)
        Me.img_Q1.TabIndex = 93
        Me.img_Q1.TabStop = False
        '
        'img_Psi2
        '
        Me.img_Psi2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Psi2.Location = New System.Drawing.Point(10, 103)
        Me.img_Psi2.Name = "img_Psi2"
        Me.img_Psi2.Size = New System.Drawing.Size(46, 20)
        Me.img_Psi2.TabIndex = 88
        Me.img_Psi2.TabStop = False
        '
        'img_Psi1
        '
        Me.img_Psi1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Psi1.Location = New System.Drawing.Point(10, 77)
        Me.img_Psi1.Name = "img_Psi1"
        Me.img_Psi1.Size = New System.Drawing.Size(46, 20)
        Me.img_Psi1.TabIndex = 81
        Me.img_Psi1.TabStop = False
        '
        'img_Psi0
        '
        Me.img_Psi0.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Psi0.Location = New System.Drawing.Point(10, 51)
        Me.img_Psi0.Name = "img_Psi0"
        Me.img_Psi0.Size = New System.Drawing.Size(46, 20)
        Me.img_Psi0.TabIndex = 80
        Me.img_Psi0.TabStop = False
        '
        'img_GammaQ
        '
        Me.img_GammaQ.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_GammaQ.Location = New System.Drawing.Point(24, 77)
        Me.img_GammaQ.Name = "img_GammaQ"
        Me.img_GammaQ.Size = New System.Drawing.Size(67, 20)
        Me.img_GammaQ.TabIndex = 78
        Me.img_GammaQ.TabStop = False
        '
        'img_GammaGinf
        '
        Me.img_GammaGinf.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_GammaGinf.Location = New System.Drawing.Point(24, 51)
        Me.img_GammaGinf.Name = "img_GammaGinf"
        Me.img_GammaGinf.Size = New System.Drawing.Size(67, 20)
        Me.img_GammaGinf.TabIndex = 76
        Me.img_GammaGinf.TabStop = False
        '
        'img_GammaGsup
        '
        Me.img_GammaGsup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_GammaGsup.Location = New System.Drawing.Point(24, 25)
        Me.img_GammaGsup.Name = "img_GammaGsup"
        Me.img_GammaGsup.Size = New System.Drawing.Size(67, 20)
        Me.img_GammaGsup.TabIndex = 74
        Me.img_GammaGsup.TabStop = False
        '
        'Frm_GammaN
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(624, 285)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_GammaN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Gamma"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_PartieHaute.ResumeLayout(False)
        Me.pan_Resistance.ResumeLayout(False)
        Me.TLPan_Resistance.ResumeLayout(False)
        Me.TLPan_Resistance.PerformLayout()
        Me.pan_Accompagnement.ResumeLayout(False)
        Me.TLPan_Accompagnement.ResumeLayout(False)
        Me.TLPan_Accompagnement.PerformLayout()
        Me.pan_SaisieAccompagnement.ResumeLayout(False)
        Me.pan_SaisieAccompagnement.PerformLayout()
        Me.pan_Chargement.ResumeLayout(False)
        Me.TLPan_Chargement.ResumeLayout(False)
        Me.TLPan_Chargement.PerformLayout()
        Me.pan_SaisieChargement.ResumeLayout(False)
        Me.pan_SaisieChargement.PerformLayout()
        Me.pan_Reset.ResumeLayout(False)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLpan_ChoixResistance.ResumeLayout(False)
        Me.TLpan_ChoixResistance.PerformLayout()
        CType(Me.img_Q2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Q1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Psi2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Psi1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Psi0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaQ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaGinf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_GammaGsup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_PartieHaute As TableLayoutPanel
    Friend WithEvents pan_Chargement As Panel
    Friend WithEvents TLPan_Chargement As TableLayoutPanel
    Friend WithEvents lbl_Chargement As Label
    Friend WithEvents pan_SaisieChargement As Panel
    Friend WithEvents pan_Accompagnement As Panel
    Friend WithEvents TLPan_Accompagnement As TableLayoutPanel
    Friend WithEvents lbl_Accompagnement As Label
    Friend WithEvents pan_SaisieAccompagnement As Panel
    Friend WithEvents pan_Resistance As Panel
    Friend WithEvents TLPan_Resistance As TableLayoutPanel
    Friend WithEvents lbl_Resistance As Label
    Friend WithEvents pan_SaisieResistance As Panel
    Friend WithEvents txt_GammaGsup As TextBox
    Friend WithEvents img_GammaGsup As PictureBox
    Friend WithEvents txt_GammaGinf As TextBox
    Friend WithEvents img_GammaGinf As PictureBox
    Friend WithEvents btn_Reini As Button
    Friend WithEvents txt_GammaQ As TextBox
    Friend WithEvents img_GammaQ As PictureBox
    Friend WithEvents txt_Psi2_Q2 As TextBox
    Friend WithEvents txt_Psi2_Q1 As TextBox
    Friend WithEvents img_Psi2 As PictureBox
    Friend WithEvents txt_Psi1_Q2 As TextBox
    Friend WithEvents txt_Psi1_Q1 As TextBox
    Friend WithEvents txt_Psi0_Q2 As TextBox
    Friend WithEvents txt_Psi0_Q1 As TextBox
    Friend WithEvents img_Psi1 As PictureBox
    Friend WithEvents img_Psi0 As PictureBox
    Friend WithEvents lbl_Q2 As Label
    Friend WithEvents lbl_Q1 As Label
    Friend WithEvents img_Q2 As PictureBox
    Friend WithEvents img_Q1 As PictureBox
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents lbl_Reset As Label
    Friend WithEvents pan_Reset As Panel
    Friend WithEvents TLpan_ChoixResistance As TableLayoutPanel
    Friend WithEvents rdb_Feu As RadioButton
    Friend WithEvents rdb_Beton As RadioButton
    Friend WithEvents rdb_Acier As RadioButton
End Class
