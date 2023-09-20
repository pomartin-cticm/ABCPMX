<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CasDeCharge
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
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.img_Analyse = New System.Windows.Forms.PictureBox()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_CdC = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_CasDeCharges = New System.Windows.Forms.Label()
        Me.Pan_Affichage = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_ChoixCas = New System.Windows.Forms.Panel()
        Me.cmb_Symbols = New System.Windows.Forms.ComboBox()
        Me.lbl_Case = New System.Windows.Forms.Label()
        Me.lbl_Name = New System.Windows.Forms.Label()
        Me.pan_EtatCdC = New System.Windows.Forms.Panel()
        Me.lbl_nEnrobage = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_Etat = New System.Windows.Forms.Label()
        Me.lbl_NeqEnrob = New System.Windows.Forms.Label()
        Me.lbl_NeqDalle = New System.Windows.Forms.Label()
        Me.lbl_Mixte = New System.Windows.Forms.Label()
        Me.Pan_Results = New System.Windows.Forms.Panel()
        Me.etq_UnitM2 = New System.Windows.Forms.Label()
        Me.etq_UnitM1 = New System.Windows.Forms.Label()
        Me.txt_Mmin = New System.Windows.Forms.TextBox()
        Me.txt_Mmax = New System.Windows.Forms.TextBox()
        Me.lbl_Mmin = New System.Windows.Forms.Label()
        Me.lbl_Mmax = New System.Windows.Forms.Label()
        Me.etq_UnitDim1 = New System.Windows.Forms.Label()
        Me.txt_Fleche = New System.Windows.Forms.TextBox()
        Me.lbl_Fleche = New System.Windows.Forms.Label()
        Me.etq_UnitForce2 = New System.Windows.Forms.Label()
        Me.etq_UnitForce1 = New System.Windows.Forms.Label()
        Me.txt_RZ2 = New System.Windows.Forms.TextBox()
        Me.txt_RZ1 = New System.Windows.Forms.TextBox()
        Me.lbl_RZ2 = New System.Windows.Forms.Label()
        Me.lbl_RZ1 = New System.Windows.Forms.Label()
        Me.lbl_RCalcul = New System.Windows.Forms.Label()
        Me.lbl_RunCalcul = New System.Windows.Forms.Label()
        Me.pan_Image = New System.Windows.Forms.Panel()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.chk_Fleches = New System.Windows.Forms.CheckBox()
        Me.btn_EditModel = New System.Windows.Forms.Button()
        Me.chk_Moment = New System.Windows.Forms.CheckBox()
        Me.chk_EffortTranchant = New System.Windows.Forms.CheckBox()
        Me.chk_Numerotation = New System.Windows.Forms.CheckBox()
        Me.chk_Inerties = New System.Windows.Forms.CheckBox()
        Me.chk_Chargement = New System.Windows.Forms.CheckBox()
        Me.chk_LocalEchelle = New System.Windows.Forms.CheckBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        CType(Me.img_Analyse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_CdC.SuspendLayout()
        Me.Pan_Affichage.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pan_ChoixCas.SuspendLayout()
        Me.pan_EtatCdC.SuspendLayout()
        Me.Pan_Results.SuspendLayout()
        Me.pan_Image.SuspendLayout()
        Me.TLpan_Gauche.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(1058, 588)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(1058, 588)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 551)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1052, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'img_Analyse
        '
        Me.img_Analyse.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Analyse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Analyse.Location = New System.Drawing.Point(149, 119)
        Me.img_Analyse.Margin = New System.Windows.Forms.Padding(0)
        Me.img_Analyse.Name = "img_Analyse"
        Me.img_Analyse.Size = New System.Drawing.Size(100, 34)
        Me.img_Analyse.TabIndex = 2
        Me.img_Analyse.TabStop = False
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(539, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(399, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_CdC)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(1052, 542)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_CdC
        '
        Me.TLPan_CdC.ColumnCount = 1
        Me.TLPan_CdC.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_CdC.Controls.Add(Me.lbl_CasDeCharges, 0, 0)
        Me.TLPan_CdC.Controls.Add(Me.Pan_Affichage, 0, 2)
        Me.TLPan_CdC.Controls.Add(Me.TableLayoutPanel1, 0, 1)
        Me.TLPan_CdC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_CdC.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_CdC.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_CdC.Name = "TLPan_CdC"
        Me.TLPan_CdC.RowCount = 3
        Me.TLPan_CdC.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_CdC.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TLPan_CdC.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_CdC.Size = New System.Drawing.Size(1052, 542)
        Me.TLPan_CdC.TabIndex = 0
        '
        'lbl_CasDeCharges
        '
        Me.lbl_CasDeCharges.AutoSize = True
        Me.lbl_CasDeCharges.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CasDeCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CasDeCharges.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CasDeCharges.Location = New System.Drawing.Point(0, 0)
        Me.lbl_CasDeCharges.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CasDeCharges.Name = "lbl_CasDeCharges"
        Me.lbl_CasDeCharges.Size = New System.Drawing.Size(1052, 30)
        Me.lbl_CasDeCharges.TabIndex = 3
        Me.lbl_CasDeCharges.Text = "lbl_CasDeCharges"
        Me.lbl_CasDeCharges.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pan_Affichage
        '
        Me.Pan_Affichage.Controls.Add(Me.TableLayoutPanel2)
        Me.Pan_Affichage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_Affichage.Location = New System.Drawing.Point(0, 101)
        Me.Pan_Affichage.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.Pan_Affichage.Name = "Pan_Affichage"
        Me.Pan_Affichage.Size = New System.Drawing.Size(1052, 441)
        Me.Pan_Affichage.TabIndex = 5
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.pan_Image, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TLpan_Gauche, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1052, 441)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.pan_ChoixCas, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pan_EtatCdC, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Pan_Results, 2, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 30)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1052, 70)
        Me.TableLayoutPanel1.TabIndex = 6
        '
        'pan_ChoixCas
        '
        Me.pan_ChoixCas.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ChoixCas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ChoixCas.Controls.Add(Me.cmb_Symbols)
        Me.pan_ChoixCas.Controls.Add(Me.lbl_Case)
        Me.pan_ChoixCas.Controls.Add(Me.lbl_Name)
        Me.pan_ChoixCas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ChoixCas.Location = New System.Drawing.Point(0, 1)
        Me.pan_ChoixCas.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.pan_ChoixCas.Name = "pan_ChoixCas"
        Me.pan_ChoixCas.Size = New System.Drawing.Size(250, 69)
        Me.pan_ChoixCas.TabIndex = 0
        '
        'cmb_Symbols
        '
        Me.cmb_Symbols.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Symbols.FormattingEnabled = True
        Me.cmb_Symbols.Location = New System.Drawing.Point(75, 5)
        Me.cmb_Symbols.Name = "cmb_Symbols"
        Me.cmb_Symbols.Size = New System.Drawing.Size(66, 21)
        Me.cmb_Symbols.TabIndex = 53
        '
        'lbl_Case
        '
        Me.lbl_Case.AutoSize = True
        Me.lbl_Case.Location = New System.Drawing.Point(14, 9)
        Me.lbl_Case.Name = "lbl_Case"
        Me.lbl_Case.Size = New System.Drawing.Size(47, 13)
        Me.lbl_Case.TabIndex = 54
        Me.lbl_Case.Text = "lbl_Case"
        '
        'lbl_Name
        '
        Me.lbl_Name.AutoSize = True
        Me.lbl_Name.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Name.Location = New System.Drawing.Point(14, 35)
        Me.lbl_Name.Name = "lbl_Name"
        Me.lbl_Name.Size = New System.Drawing.Size(51, 13)
        Me.lbl_Name.TabIndex = 55
        Me.lbl_Name.Text = "lbl_Name"
        '
        'pan_EtatCdC
        '
        Me.pan_EtatCdC.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_EtatCdC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_EtatCdC.Controls.Add(Me.lbl_nEnrobage)
        Me.pan_EtatCdC.Controls.Add(Me.Label1)
        Me.pan_EtatCdC.Controls.Add(Me.lbl_Etat)
        Me.pan_EtatCdC.Controls.Add(Me.lbl_NeqEnrob)
        Me.pan_EtatCdC.Controls.Add(Me.lbl_NeqDalle)
        Me.pan_EtatCdC.Controls.Add(Me.lbl_Mixte)
        Me.pan_EtatCdC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_EtatCdC.Location = New System.Drawing.Point(251, 1)
        Me.pan_EtatCdC.Margin = New System.Windows.Forms.Padding(1, 1, 0, 0)
        Me.pan_EtatCdC.Name = "pan_EtatCdC"
        Me.pan_EtatCdC.Size = New System.Drawing.Size(199, 69)
        Me.pan_EtatCdC.TabIndex = 4
        '
        'lbl_nEnrobage
        '
        Me.lbl_nEnrobage.AutoSize = True
        Me.lbl_nEnrobage.Location = New System.Drawing.Point(12, 48)
        Me.lbl_nEnrobage.Name = "lbl_nEnrobage"
        Me.lbl_nEnrobage.Size = New System.Drawing.Size(59, 13)
        Me.lbl_nEnrobage.TabIndex = 61
        Me.lbl_nEnrobage.Text = "nEnrobage"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 60
        Me.Label1.Text = "nDalle"
        '
        'lbl_Etat
        '
        Me.lbl_Etat.AutoSize = True
        Me.lbl_Etat.Location = New System.Drawing.Point(12, 6)
        Me.lbl_Etat.Name = "lbl_Etat"
        Me.lbl_Etat.Size = New System.Drawing.Size(42, 13)
        Me.lbl_Etat.TabIndex = 59
        Me.lbl_Etat.Text = "lbl_Etat"
        '
        'lbl_NeqEnrob
        '
        Me.lbl_NeqEnrob.AutoSize = True
        Me.lbl_NeqEnrob.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_NeqEnrob.Location = New System.Drawing.Point(77, 48)
        Me.lbl_NeqEnrob.Name = "lbl_NeqEnrob"
        Me.lbl_NeqEnrob.Size = New System.Drawing.Size(71, 13)
        Me.lbl_NeqEnrob.TabIndex = 58
        Me.lbl_NeqEnrob.Text = "lbl_NeqEnrob"
        '
        'lbl_NeqDalle
        '
        Me.lbl_NeqDalle.AutoSize = True
        Me.lbl_NeqDalle.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_NeqDalle.Location = New System.Drawing.Point(77, 27)
        Me.lbl_NeqDalle.Name = "lbl_NeqDalle"
        Me.lbl_NeqDalle.Size = New System.Drawing.Size(67, 13)
        Me.lbl_NeqDalle.TabIndex = 57
        Me.lbl_NeqDalle.Text = "lbl_NeqDalle"
        '
        'lbl_Mixte
        '
        Me.lbl_Mixte.AutoSize = True
        Me.lbl_Mixte.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Mixte.Location = New System.Drawing.Point(77, 5)
        Me.lbl_Mixte.Name = "lbl_Mixte"
        Me.lbl_Mixte.Size = New System.Drawing.Size(48, 13)
        Me.lbl_Mixte.TabIndex = 56
        Me.lbl_Mixte.Text = "lbl_Mixte"
        '
        'Pan_Results
        '
        Me.Pan_Results.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Pan_Results.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pan_Results.Controls.Add(Me.etq_UnitM2)
        Me.Pan_Results.Controls.Add(Me.etq_UnitM1)
        Me.Pan_Results.Controls.Add(Me.txt_Mmin)
        Me.Pan_Results.Controls.Add(Me.txt_Mmax)
        Me.Pan_Results.Controls.Add(Me.lbl_Mmin)
        Me.Pan_Results.Controls.Add(Me.lbl_Mmax)
        Me.Pan_Results.Controls.Add(Me.etq_UnitDim1)
        Me.Pan_Results.Controls.Add(Me.txt_Fleche)
        Me.Pan_Results.Controls.Add(Me.lbl_Fleche)
        Me.Pan_Results.Controls.Add(Me.etq_UnitForce2)
        Me.Pan_Results.Controls.Add(Me.etq_UnitForce1)
        Me.Pan_Results.Controls.Add(Me.txt_RZ2)
        Me.Pan_Results.Controls.Add(Me.txt_RZ1)
        Me.Pan_Results.Controls.Add(Me.lbl_RZ2)
        Me.Pan_Results.Controls.Add(Me.lbl_RZ1)
        Me.Pan_Results.Controls.Add(Me.lbl_RCalcul)
        Me.Pan_Results.Controls.Add(Me.lbl_RunCalcul)
        Me.Pan_Results.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_Results.Location = New System.Drawing.Point(451, 1)
        Me.Pan_Results.Margin = New System.Windows.Forms.Padding(1, 1, 0, 0)
        Me.Pan_Results.Name = "Pan_Results"
        Me.Pan_Results.Size = New System.Drawing.Size(601, 69)
        Me.Pan_Results.TabIndex = 5
        '
        'etq_UnitM2
        '
        Me.etq_UnitM2.AutoSize = True
        Me.etq_UnitM2.Location = New System.Drawing.Point(384, 49)
        Me.etq_UnitM2.Name = "etq_UnitM2"
        Me.etq_UnitM2.Size = New System.Drawing.Size(15, 13)
        Me.etq_UnitM2.TabIndex = 76
        Me.etq_UnitM2.Text = "N"
        '
        'etq_UnitM1
        '
        Me.etq_UnitM1.AutoSize = True
        Me.etq_UnitM1.Location = New System.Drawing.Point(384, 27)
        Me.etq_UnitM1.Name = "etq_UnitM1"
        Me.etq_UnitM1.Size = New System.Drawing.Size(15, 13)
        Me.etq_UnitM1.TabIndex = 75
        Me.etq_UnitM1.Text = "N"
        '
        'txt_Mmin
        '
        Me.txt_Mmin.ForeColor = System.Drawing.Color.DarkRed
        Me.txt_Mmin.Location = New System.Drawing.Point(312, 46)
        Me.txt_Mmin.Name = "txt_Mmin"
        Me.txt_Mmin.Size = New System.Drawing.Size(66, 20)
        Me.txt_Mmin.TabIndex = 74
        Me.txt_Mmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_Mmax
        '
        Me.txt_Mmax.ForeColor = System.Drawing.Color.DarkRed
        Me.txt_Mmax.Location = New System.Drawing.Point(312, 24)
        Me.txt_Mmax.Name = "txt_Mmax"
        Me.txt_Mmax.Size = New System.Drawing.Size(66, 20)
        Me.txt_Mmax.TabIndex = 73
        Me.txt_Mmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_Mmin
        '
        Me.lbl_Mmin.AutoSize = True
        Me.lbl_Mmin.Location = New System.Drawing.Point(271, 49)
        Me.lbl_Mmin.Name = "lbl_Mmin"
        Me.lbl_Mmin.Size = New System.Drawing.Size(32, 13)
        Me.lbl_Mmin.TabIndex = 72
        Me.lbl_Mmin.Text = "Mmin"
        Me.lbl_Mmin.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_Mmax
        '
        Me.lbl_Mmax.AutoSize = True
        Me.lbl_Mmax.Location = New System.Drawing.Point(271, 28)
        Me.lbl_Mmax.Name = "lbl_Mmax"
        Me.lbl_Mmax.Size = New System.Drawing.Size(35, 13)
        Me.lbl_Mmax.TabIndex = 71
        Me.lbl_Mmax.Text = "Mmax"
        Me.lbl_Mmax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitDim1
        '
        Me.etq_UnitDim1.AutoSize = True
        Me.etq_UnitDim1.Location = New System.Drawing.Point(384, 6)
        Me.etq_UnitDim1.Name = "etq_UnitDim1"
        Me.etq_UnitDim1.Size = New System.Drawing.Size(15, 13)
        Me.etq_UnitDim1.TabIndex = 70
        Me.etq_UnitDim1.Text = "N"
        '
        'txt_Fleche
        '
        Me.txt_Fleche.ForeColor = System.Drawing.Color.DarkRed
        Me.txt_Fleche.Location = New System.Drawing.Point(312, 3)
        Me.txt_Fleche.Name = "txt_Fleche"
        Me.txt_Fleche.Size = New System.Drawing.Size(66, 20)
        Me.txt_Fleche.TabIndex = 69
        Me.txt_Fleche.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_Fleche
        '
        Me.lbl_Fleche.AutoSize = True
        Me.lbl_Fleche.Location = New System.Drawing.Point(251, 6)
        Me.lbl_Fleche.Name = "lbl_Fleche"
        Me.lbl_Fleche.Size = New System.Drawing.Size(55, 13)
        Me.lbl_Fleche.TabIndex = 68
        Me.lbl_Fleche.Text = "lbl_Fleche"
        Me.lbl_Fleche.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitForce2
        '
        Me.etq_UnitForce2.AutoSize = True
        Me.etq_UnitForce2.Location = New System.Drawing.Point(218, 49)
        Me.etq_UnitForce2.Name = "etq_UnitForce2"
        Me.etq_UnitForce2.Size = New System.Drawing.Size(15, 13)
        Me.etq_UnitForce2.TabIndex = 67
        Me.etq_UnitForce2.Text = "N"
        '
        'etq_UnitForce1
        '
        Me.etq_UnitForce1.AutoSize = True
        Me.etq_UnitForce1.Location = New System.Drawing.Point(218, 27)
        Me.etq_UnitForce1.Name = "etq_UnitForce1"
        Me.etq_UnitForce1.Size = New System.Drawing.Size(15, 13)
        Me.etq_UnitForce1.TabIndex = 66
        Me.etq_UnitForce1.Text = "N"
        '
        'txt_RZ2
        '
        Me.txt_RZ2.ForeColor = System.Drawing.Color.DarkRed
        Me.txt_RZ2.Location = New System.Drawing.Point(146, 46)
        Me.txt_RZ2.Name = "txt_RZ2"
        Me.txt_RZ2.Size = New System.Drawing.Size(66, 20)
        Me.txt_RZ2.TabIndex = 65
        Me.txt_RZ2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_RZ1
        '
        Me.txt_RZ1.ForeColor = System.Drawing.Color.DarkRed
        Me.txt_RZ1.Location = New System.Drawing.Point(146, 24)
        Me.txt_RZ1.Name = "txt_RZ1"
        Me.txt_RZ1.Size = New System.Drawing.Size(66, 20)
        Me.txt_RZ1.TabIndex = 64
        Me.txt_RZ1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbl_RZ2
        '
        Me.lbl_RZ2.AutoSize = True
        Me.lbl_RZ2.Location = New System.Drawing.Point(112, 49)
        Me.lbl_RZ2.Name = "lbl_RZ2"
        Me.lbl_RZ2.Size = New System.Drawing.Size(28, 13)
        Me.lbl_RZ2.TabIndex = 63
        Me.lbl_RZ2.Text = "RZ2"
        Me.lbl_RZ2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_RZ1
        '
        Me.lbl_RZ1.AutoSize = True
        Me.lbl_RZ1.Location = New System.Drawing.Point(112, 28)
        Me.lbl_RZ1.Name = "lbl_RZ1"
        Me.lbl_RZ1.Size = New System.Drawing.Size(28, 13)
        Me.lbl_RZ1.TabIndex = 61
        Me.lbl_RZ1.Text = "RZ1"
        Me.lbl_RZ1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_RCalcul
        '
        Me.lbl_RCalcul.AutoSize = True
        Me.lbl_RCalcul.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_RCalcul.Location = New System.Drawing.Point(143, 6)
        Me.lbl_RCalcul.Name = "lbl_RCalcul"
        Me.lbl_RCalcul.Size = New System.Drawing.Size(60, 13)
        Me.lbl_RCalcul.TabIndex = 62
        Me.lbl_RCalcul.Text = "lbl_RCalcul"
        '
        'lbl_RunCalcul
        '
        Me.lbl_RunCalcul.AutoSize = True
        Me.lbl_RunCalcul.Location = New System.Drawing.Point(18, 6)
        Me.lbl_RunCalcul.Name = "lbl_RunCalcul"
        Me.lbl_RunCalcul.Size = New System.Drawing.Size(72, 13)
        Me.lbl_RunCalcul.TabIndex = 60
        Me.lbl_RunCalcul.Text = "lbl_RunCalcul"
        '
        'pan_Image
        '
        Me.pan_Image.Controls.Add(Me.img_Analyse)
        Me.pan_Image.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Image.Location = New System.Drawing.Point(151, 0)
        Me.pan_Image.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Image.Name = "pan_Image"
        Me.pan_Image.Size = New System.Drawing.Size(901, 441)
        Me.pan_Image.TabIndex = 3
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.chk_LocalEchelle, 0, 6)
        Me.TLpan_Gauche.Controls.Add(Me.chk_Chargement, 0, 5)
        Me.TLpan_Gauche.Controls.Add(Me.chk_Inerties, 0, 4)
        Me.TLpan_Gauche.Controls.Add(Me.chk_Numerotation, 0, 3)
        Me.TLpan_Gauche.Controls.Add(Me.chk_EffortTranchant, 0, 2)
        Me.TLpan_Gauche.Controls.Add(Me.chk_Moment, 0, 1)
        Me.TLpan_Gauche.Controls.Add(Me.chk_Fleches, 0, 0)
        Me.TLpan_Gauche.Controls.Add(Me.btn_EditModel, 0, 8)
        Me.TLpan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Gauche.Name = "TLpan_Gauche"
        Me.TLpan_Gauche.RowCount = 9
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Gauche.Size = New System.Drawing.Size(150, 441)
        Me.TLpan_Gauche.TabIndex = 4
        '
        'chk_Fleches
        '
        Me.chk_Fleches.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Fleches.AutoSize = True
        Me.chk_Fleches.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_Fleches.Location = New System.Drawing.Point(1, 1)
        Me.chk_Fleches.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_Fleches.Name = "chk_Fleches"
        Me.chk_Fleches.Size = New System.Drawing.Size(148, 28)
        Me.chk_Fleches.TabIndex = 0
        Me.chk_Fleches.Text = "chk_Fleches"
        Me.chk_Fleches.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_Fleches.UseVisualStyleBackColor = True
        '
        'btn_EditModel
        '
        Me.btn_EditModel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_EditModel.Location = New System.Drawing.Point(1, 412)
        Me.btn_EditModel.Margin = New System.Windows.Forms.Padding(1)
        Me.btn_EditModel.Name = "btn_EditModel"
        Me.btn_EditModel.Size = New System.Drawing.Size(148, 28)
        Me.btn_EditModel.TabIndex = 1
        Me.btn_EditModel.Text = "btn_EditModel"
        Me.btn_EditModel.UseVisualStyleBackColor = True
        '
        'chk_Moment
        '
        Me.chk_Moment.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Moment.AutoSize = True
        Me.chk_Moment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_Moment.Location = New System.Drawing.Point(1, 31)
        Me.chk_Moment.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_Moment.Name = "chk_Moment"
        Me.chk_Moment.Size = New System.Drawing.Size(148, 28)
        Me.chk_Moment.TabIndex = 2
        Me.chk_Moment.Text = "chk_Moment"
        Me.chk_Moment.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_Moment.UseVisualStyleBackColor = True
        '
        'chk_EffortTranchant
        '
        Me.chk_EffortTranchant.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_EffortTranchant.AutoSize = True
        Me.chk_EffortTranchant.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_EffortTranchant.Location = New System.Drawing.Point(1, 61)
        Me.chk_EffortTranchant.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_EffortTranchant.Name = "chk_EffortTranchant"
        Me.chk_EffortTranchant.Size = New System.Drawing.Size(148, 28)
        Me.chk_EffortTranchant.TabIndex = 3
        Me.chk_EffortTranchant.Text = "chk_EffortTranchant"
        Me.chk_EffortTranchant.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_EffortTranchant.UseVisualStyleBackColor = True
        '
        'chk_Numerotation
        '
        Me.chk_Numerotation.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Numerotation.AutoSize = True
        Me.chk_Numerotation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_Numerotation.Location = New System.Drawing.Point(1, 91)
        Me.chk_Numerotation.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_Numerotation.Name = "chk_Numerotation"
        Me.chk_Numerotation.Size = New System.Drawing.Size(148, 28)
        Me.chk_Numerotation.TabIndex = 4
        Me.chk_Numerotation.Text = "chk_Numerotation"
        Me.chk_Numerotation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_Numerotation.UseVisualStyleBackColor = True
        '
        'chk_Inerties
        '
        Me.chk_Inerties.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Inerties.AutoSize = True
        Me.chk_Inerties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_Inerties.Location = New System.Drawing.Point(1, 121)
        Me.chk_Inerties.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_Inerties.Name = "chk_Inerties"
        Me.chk_Inerties.Size = New System.Drawing.Size(148, 28)
        Me.chk_Inerties.TabIndex = 5
        Me.chk_Inerties.Text = "chk_Inerties"
        Me.chk_Inerties.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_Inerties.UseVisualStyleBackColor = True
        '
        'chk_Chargement
        '
        Me.chk_Chargement.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Chargement.AutoSize = True
        Me.chk_Chargement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_Chargement.Location = New System.Drawing.Point(1, 151)
        Me.chk_Chargement.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_Chargement.Name = "chk_Chargement"
        Me.chk_Chargement.Size = New System.Drawing.Size(148, 28)
        Me.chk_Chargement.TabIndex = 6
        Me.chk_Chargement.Text = "chk_Chargement"
        Me.chk_Chargement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_Chargement.UseVisualStyleBackColor = True
        '
        'chk_LocalEchelle
        '
        Me.chk_LocalEchelle.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_LocalEchelle.AutoSize = True
        Me.chk_LocalEchelle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_LocalEchelle.Location = New System.Drawing.Point(1, 181)
        Me.chk_LocalEchelle.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_LocalEchelle.Name = "chk_LocalEchelle"
        Me.chk_LocalEchelle.Size = New System.Drawing.Size(148, 28)
        Me.chk_LocalEchelle.TabIndex = 7
        Me.chk_LocalEchelle.Text = "chk_LocalEchelle"
        Me.chk_LocalEchelle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_LocalEchelle.UseVisualStyleBackColor = True
        '
        'Frm_CasDeCharge
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1058, 588)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_CasDeCharge"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_CasDeCharge"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        CType(Me.img_Analyse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_CdC.ResumeLayout(False)
        Me.TLPan_CdC.PerformLayout()
        Me.Pan_Affichage.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.pan_ChoixCas.ResumeLayout(False)
        Me.pan_ChoixCas.PerformLayout()
        Me.pan_EtatCdC.ResumeLayout(False)
        Me.pan_EtatCdC.PerformLayout()
        Me.Pan_Results.ResumeLayout(False)
        Me.Pan_Results.PerformLayout()
        Me.pan_Image.ResumeLayout(False)
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_CdC As TableLayoutPanel
    Friend WithEvents lbl_CasDeCharges As Label
    Friend WithEvents pan_EtatCdC As Panel
    Friend WithEvents Pan_Affichage As Panel
    Friend WithEvents img_Analyse As PictureBox
    Friend WithEvents lbl_Name As Label
    Friend WithEvents lbl_Case As Label
    Friend WithEvents cmb_Symbols As ComboBox
    Friend WithEvents lbl_NeqEnrob As Label
    Friend WithEvents lbl_NeqDalle As Label
    Friend WithEvents lbl_Mixte As Label
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents pan_ChoixCas As Panel
    Friend WithEvents lbl_Etat As Label
    Friend WithEvents lbl_RCalcul As Label
    Friend WithEvents lbl_RunCalcul As Label
    Friend WithEvents lbl_nEnrobage As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Pan_Results As Panel
    Friend WithEvents etq_UnitForce2 As Label
    Friend WithEvents etq_UnitForce1 As Label
    Friend WithEvents txt_RZ2 As TextBox
    Friend WithEvents txt_RZ1 As TextBox
    Friend WithEvents lbl_RZ2 As Label
    Friend WithEvents lbl_RZ1 As Label
    Friend WithEvents etq_UnitDim1 As Label
    Friend WithEvents txt_Fleche As TextBox
    Friend WithEvents lbl_Fleche As Label
    Friend WithEvents etq_UnitM2 As Label
    Friend WithEvents etq_UnitM1 As Label
    Friend WithEvents txt_Mmin As TextBox
    Friend WithEvents txt_Mmax As TextBox
    Friend WithEvents lbl_Mmin As Label
    Friend WithEvents lbl_Mmax As Label
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents pan_Image As Panel
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents chk_EffortTranchant As CheckBox
    Friend WithEvents chk_Moment As CheckBox
    Friend WithEvents chk_Fleches As CheckBox
    Friend WithEvents btn_EditModel As Button
    Friend WithEvents chk_Inerties As CheckBox
    Friend WithEvents chk_Numerotation As CheckBox
    Friend WithEvents chk_Chargement As CheckBox
    Friend WithEvents chk_LocalEchelle As CheckBox
End Class
