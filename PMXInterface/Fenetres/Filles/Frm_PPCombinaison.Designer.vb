<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PPCombinaison
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
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.lbl_Combi = New System.Windows.Forms.Label()
        Me.TLpan_Modules = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_ChoixCombi = New System.Windows.Forms.Panel()
        Me.cmb_Combi = New System.Windows.Forms.ComboBox()
        Me.lbl_SymbCombi = New System.Windows.Forms.Label()
        Me.Pan_Affichage = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Image = New System.Windows.Forms.Panel()
        Me.img_Analyse = New System.Windows.Forms.PictureBox()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.chk_LocalEchelle = New System.Windows.Forms.CheckBox()
        Me.chk_Chargement = New System.Windows.Forms.CheckBox()
        Me.chk_Inerties = New System.Windows.Forms.CheckBox()
        Me.chk_Numerotation = New System.Windows.Forms.CheckBox()
        Me.chk_EffortTranchant = New System.Windows.Forms.CheckBox()
        Me.chk_Moment = New System.Windows.Forms.CheckBox()
        Me.chk_Fleches = New System.Windows.Forms.CheckBox()
        Me.btn_EditModel = New System.Windows.Forms.Button()
        Me.TLpan_HAffichage = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pan_AffichageCombi = New System.Windows.Forms.Panel()
        Me.lbl_CombiSelect = New System.Windows.Forms.Label()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.TLpan_Modules.SuspendLayout()
        Me.pan_ChoixCombi.SuspendLayout()
        Me.Pan_Affichage.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.pan_Image.SuspendLayout()
        CType(Me.img_Analyse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLpan_Gauche.SuspendLayout()
        Me.TLpan_HAffichage.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pan_AffichageCombi.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.Controls.Add(Me.TLpan_Main)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(0, 0)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(1037, 562)
        Me.pan_Main.TabIndex = 0
        '
        'TLpan_Main
        '
        Me.TLpan_Main.ColumnCount = 1
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Controls.Add(Me.Pan_Affichage, 0, 2)
        Me.TLpan_Main.Controls.Add(Me.TLPan_PartieBasse, 0, 3)
        Me.TLpan_Main.Controls.Add(Me.lbl_Combi, 0, 0)
        Me.TLpan_Main.Controls.Add(Me.TLpan_Modules, 0, 1)
        Me.TLpan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Main.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 4
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(1037, 562)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 525)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1031, 34)
        Me.TLPan_PartieBasse.TabIndex = 5
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(528, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(388, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'lbl_Combi
        '
        Me.lbl_Combi.AutoSize = True
        Me.lbl_Combi.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Combi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Combi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Combi.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Combi.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Combi.Name = "lbl_Combi"
        Me.lbl_Combi.Size = New System.Drawing.Size(1037, 30)
        Me.lbl_Combi.TabIndex = 4
        Me.lbl_Combi.Text = "lbl_Combi"
        Me.lbl_Combi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TLpan_Modules
        '
        Me.TLpan_Modules.ColumnCount = 3
        Me.TLpan_Modules.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TLpan_Modules.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TLpan_Modules.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Modules.Controls.Add(Me.pan_ChoixCombi, 0, 0)
        Me.TLpan_Modules.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Modules.Location = New System.Drawing.Point(0, 30)
        Me.TLpan_Modules.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Modules.Name = "TLpan_Modules"
        Me.TLpan_Modules.RowCount = 1
        Me.TLpan_Modules.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Modules.Size = New System.Drawing.Size(1037, 70)
        Me.TLpan_Modules.TabIndex = 6
        '
        'pan_ChoixCombi
        '
        Me.pan_ChoixCombi.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ChoixCombi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ChoixCombi.Controls.Add(Me.cmb_Combi)
        Me.pan_ChoixCombi.Controls.Add(Me.lbl_SymbCombi)
        Me.pan_ChoixCombi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ChoixCombi.Location = New System.Drawing.Point(0, 1)
        Me.pan_ChoixCombi.Margin = New System.Windows.Forms.Padding(0, 1, 1, 0)
        Me.pan_ChoixCombi.Name = "pan_ChoixCombi"
        Me.pan_ChoixCombi.Size = New System.Drawing.Size(299, 69)
        Me.pan_ChoixCombi.TabIndex = 0
        '
        'cmb_Combi
        '
        Me.cmb_Combi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Combi.FormattingEnabled = True
        Me.cmb_Combi.Location = New System.Drawing.Point(96, 7)
        Me.cmb_Combi.Name = "cmb_Combi"
        Me.cmb_Combi.Size = New System.Drawing.Size(89, 21)
        Me.cmb_Combi.TabIndex = 55
        '
        'lbl_SymbCombi
        '
        Me.lbl_SymbCombi.AutoSize = True
        Me.lbl_SymbCombi.Location = New System.Drawing.Point(12, 10)
        Me.lbl_SymbCombi.Name = "lbl_SymbCombi"
        Me.lbl_SymbCombi.Size = New System.Drawing.Size(78, 13)
        Me.lbl_SymbCombi.TabIndex = 56
        Me.lbl_SymbCombi.Text = "lbl_SymbCombi"
        '
        'Pan_Affichage
        '
        Me.Pan_Affichage.Controls.Add(Me.TableLayoutPanel2)
        Me.Pan_Affichage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_Affichage.Location = New System.Drawing.Point(0, 101)
        Me.Pan_Affichage.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.Pan_Affichage.Name = "Pan_Affichage"
        Me.Pan_Affichage.Size = New System.Drawing.Size(1037, 421)
        Me.Pan_Affichage.TabIndex = 7
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
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1037, 421)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'pan_Image
        '
        Me.pan_Image.Controls.Add(Me.TLpan_HAffichage)
        Me.pan_Image.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Image.Location = New System.Drawing.Point(151, 0)
        Me.pan_Image.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Image.Name = "pan_Image"
        Me.pan_Image.Size = New System.Drawing.Size(886, 421)
        Me.pan_Image.TabIndex = 3
        '
        'img_Analyse
        '
        Me.img_Analyse.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Analyse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Analyse.Location = New System.Drawing.Point(135, 115)
        Me.img_Analyse.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.img_Analyse.Name = "img_Analyse"
        Me.img_Analyse.Size = New System.Drawing.Size(100, 34)
        Me.img_Analyse.TabIndex = 2
        Me.img_Analyse.TabStop = False
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
        Me.TLpan_Gauche.Size = New System.Drawing.Size(150, 421)
        Me.TLpan_Gauche.TabIndex = 4
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
        Me.btn_EditModel.Location = New System.Drawing.Point(1, 392)
        Me.btn_EditModel.Margin = New System.Windows.Forms.Padding(1)
        Me.btn_EditModel.Name = "btn_EditModel"
        Me.btn_EditModel.Size = New System.Drawing.Size(148, 28)
        Me.btn_EditModel.TabIndex = 1
        Me.btn_EditModel.Text = "btn_EditModel"
        Me.btn_EditModel.UseVisualStyleBackColor = True
        '
        'TLpan_HAffichage
        '
        Me.TLpan_HAffichage.ColumnCount = 1
        Me.TLpan_HAffichage.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_HAffichage.Controls.Add(Me.Panel1, 0, 1)
        Me.TLpan_HAffichage.Controls.Add(Me.pan_AffichageCombi, 0, 0)
        Me.TLpan_HAffichage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_HAffichage.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_HAffichage.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_HAffichage.Name = "TLpan_HAffichage"
        Me.TLpan_HAffichage.RowCount = 2
        Me.TLpan_HAffichage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_HAffichage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_HAffichage.Size = New System.Drawing.Size(886, 421)
        Me.TLpan_HAffichage.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.img_Analyse)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 30)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(886, 391)
        Me.Panel1.TabIndex = 0
        '
        'pan_AffichageCombi
        '
        Me.pan_AffichageCombi.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_AffichageCombi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_AffichageCombi.Controls.Add(Me.lbl_CombiSelect)
        Me.pan_AffichageCombi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_AffichageCombi.Location = New System.Drawing.Point(0, 0)
        Me.pan_AffichageCombi.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_AffichageCombi.Name = "pan_AffichageCombi"
        Me.pan_AffichageCombi.Size = New System.Drawing.Size(886, 30)
        Me.pan_AffichageCombi.TabIndex = 1
        '
        'lbl_CombiSelect
        '
        Me.lbl_CombiSelect.AutoSize = True
        Me.lbl_CombiSelect.Location = New System.Drawing.Point(12, 8)
        Me.lbl_CombiSelect.Name = "lbl_CombiSelect"
        Me.lbl_CombiSelect.Size = New System.Drawing.Size(82, 13)
        Me.lbl_CombiSelect.TabIndex = 0
        Me.lbl_CombiSelect.Text = "lbl_CombiSelect"
        '
        'Frm_PPCombinaison
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1037, 562)
        Me.Controls.Add(Me.pan_Main)
        Me.Name = "Frm_PPCombinaison"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_PPCombinaison"
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLpan_Main.PerformLayout()
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.TLpan_Modules.ResumeLayout(False)
        Me.pan_ChoixCombi.ResumeLayout(False)
        Me.pan_ChoixCombi.PerformLayout()
        Me.Pan_Affichage.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.pan_Image.ResumeLayout(False)
        CType(Me.img_Analyse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.PerformLayout()
        Me.TLpan_HAffichage.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pan_AffichageCombi.ResumeLayout(False)
        Me.pan_AffichageCombi.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents lbl_Combi As Label
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents TLpan_Modules As TableLayoutPanel
    Friend WithEvents pan_ChoixCombi As Panel
    Friend WithEvents cmb_Combi As ComboBox
    Friend WithEvents lbl_SymbCombi As Label
    Friend WithEvents Pan_Affichage As Panel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents pan_Image As Panel
    Friend WithEvents TLpan_HAffichage As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents img_Analyse As PictureBox
    Friend WithEvents pan_AffichageCombi As Panel
    Friend WithEvents lbl_CombiSelect As Label
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents chk_LocalEchelle As CheckBox
    Friend WithEvents chk_Chargement As CheckBox
    Friend WithEvents chk_Inerties As CheckBox
    Friend WithEvents chk_Numerotation As CheckBox
    Friend WithEvents chk_EffortTranchant As CheckBox
    Friend WithEvents chk_Moment As CheckBox
    Friend WithEvents chk_Fleches As CheckBox
    Friend WithEvents btn_EditModel As Button
End Class
