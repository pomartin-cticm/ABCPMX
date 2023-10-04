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
        Me.Pan_Affichage = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Image = New System.Windows.Forms.Panel()
        Me.TLpan_HAffichage = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.img_Analyse = New System.Windows.Forms.PictureBox()
        Me.pan_AffichageCombi = New System.Windows.Forms.Panel()
        Me.lbl_CombiSelect = New System.Windows.Forms.Label()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.chk_Retrait = New System.Windows.Forms.CheckBox()
        Me.chk_Numerotation = New System.Windows.Forms.CheckBox()
        Me.chk_EffortTranchant = New System.Windows.Forms.CheckBox()
        Me.chk_Moment = New System.Windows.Forms.CheckBox()
        Me.chk_Fleches = New System.Windows.Forms.CheckBox()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.lbl_Combi = New System.Windows.Forms.Label()
        Me.TLpan_Modules = New System.Windows.Forms.TableLayoutPanel()
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
        Me.pan_ChoixCombi = New System.Windows.Forms.Panel()
        Me.cmb_LimitState = New System.Windows.Forms.ComboBox()
        Me.lbl_LimitState = New System.Windows.Forms.Label()
        Me.cmb_Combi = New System.Windows.Forms.ComboBox()
        Me.lbl_SymbCombi = New System.Windows.Forms.Label()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.Pan_Affichage.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.pan_Image.SuspendLayout()
        Me.TLpan_HAffichage.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.img_Analyse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_AffichageCombi.SuspendLayout()
        Me.TLpan_Gauche.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.TLpan_Modules.SuspendLayout()
        Me.Pan_Results.SuspendLayout()
        Me.pan_ChoixCombi.SuspendLayout()
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
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.chk_Retrait, 0, 4)
        Me.TLpan_Gauche.Controls.Add(Me.chk_Numerotation, 0, 3)
        Me.TLpan_Gauche.Controls.Add(Me.chk_EffortTranchant, 0, 2)
        Me.TLpan_Gauche.Controls.Add(Me.chk_Moment, 0, 1)
        Me.TLpan_Gauche.Controls.Add(Me.chk_Fleches, 0, 0)
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
        'chk_Retrait
        '
        Me.chk_Retrait.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Retrait.AutoSize = True
        Me.chk_Retrait.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_Retrait.Location = New System.Drawing.Point(1, 121)
        Me.chk_Retrait.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_Retrait.Name = "chk_Retrait"
        Me.chk_Retrait.Size = New System.Drawing.Size(148, 28)
        Me.chk_Retrait.TabIndex = 5
        Me.chk_Retrait.Text = "chk_Retrait"
        Me.chk_Retrait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_Retrait.UseVisualStyleBackColor = True
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
        Me.TLpan_Modules.ColumnCount = 2
        Me.TLpan_Modules.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TLpan_Modules.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Modules.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Modules.Controls.Add(Me.Pan_Results, 0, 0)
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
        Me.Pan_Results.Location = New System.Drawing.Point(301, 1)
        Me.Pan_Results.Margin = New System.Windows.Forms.Padding(1, 1, 0, 0)
        Me.Pan_Results.Name = "Pan_Results"
        Me.Pan_Results.Size = New System.Drawing.Size(736, 69)
        Me.Pan_Results.TabIndex = 6
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
        'pan_ChoixCombi
        '
        Me.pan_ChoixCombi.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ChoixCombi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ChoixCombi.Controls.Add(Me.cmb_LimitState)
        Me.pan_ChoixCombi.Controls.Add(Me.lbl_LimitState)
        Me.pan_ChoixCombi.Controls.Add(Me.cmb_Combi)
        Me.pan_ChoixCombi.Controls.Add(Me.lbl_SymbCombi)
        Me.pan_ChoixCombi.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ChoixCombi.Location = New System.Drawing.Point(0, 1)
        Me.pan_ChoixCombi.Margin = New System.Windows.Forms.Padding(0, 1, 1, 0)
        Me.pan_ChoixCombi.Name = "pan_ChoixCombi"
        Me.pan_ChoixCombi.Size = New System.Drawing.Size(299, 69)
        Me.pan_ChoixCombi.TabIndex = 0
        '
        'cmb_LimitState
        '
        Me.cmb_LimitState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_LimitState.FormattingEnabled = True
        Me.cmb_LimitState.Location = New System.Drawing.Point(96, 7)
        Me.cmb_LimitState.Name = "cmb_LimitState"
        Me.cmb_LimitState.Size = New System.Drawing.Size(165, 21)
        Me.cmb_LimitState.TabIndex = 58
        '
        'lbl_LimitState
        '
        Me.lbl_LimitState.AutoSize = True
        Me.lbl_LimitState.Location = New System.Drawing.Point(12, 10)
        Me.lbl_LimitState.Name = "lbl_LimitState"
        Me.lbl_LimitState.Size = New System.Drawing.Size(69, 13)
        Me.lbl_LimitState.TabIndex = 57
        Me.lbl_LimitState.Text = "lbl_LimitState"
        '
        'cmb_Combi
        '
        Me.cmb_Combi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Combi.FormattingEnabled = True
        Me.cmb_Combi.Location = New System.Drawing.Point(96, 34)
        Me.cmb_Combi.Name = "cmb_Combi"
        Me.cmb_Combi.Size = New System.Drawing.Size(165, 21)
        Me.cmb_Combi.TabIndex = 55
        '
        'lbl_SymbCombi
        '
        Me.lbl_SymbCombi.AutoSize = True
        Me.lbl_SymbCombi.Location = New System.Drawing.Point(12, 37)
        Me.lbl_SymbCombi.Name = "lbl_SymbCombi"
        Me.lbl_SymbCombi.Size = New System.Drawing.Size(78, 13)
        Me.lbl_SymbCombi.TabIndex = 56
        Me.lbl_SymbCombi.Text = "lbl_SymbCombi"
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
        Me.Pan_Affichage.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.pan_Image.ResumeLayout(False)
        Me.TLpan_HAffichage.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.img_Analyse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_AffichageCombi.ResumeLayout(False)
        Me.pan_AffichageCombi.PerformLayout()
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.PerformLayout()
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.TLpan_Modules.ResumeLayout(False)
        Me.Pan_Results.ResumeLayout(False)
        Me.Pan_Results.PerformLayout()
        Me.pan_ChoixCombi.ResumeLayout(False)
        Me.pan_ChoixCombi.PerformLayout()
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
    Friend WithEvents chk_Numerotation As CheckBox
    Friend WithEvents chk_EffortTranchant As CheckBox
    Friend WithEvents chk_Moment As CheckBox
    Friend WithEvents chk_Fleches As CheckBox
    Friend WithEvents Pan_Results As Panel
    Friend WithEvents etq_UnitM2 As Label
    Friend WithEvents etq_UnitM1 As Label
    Friend WithEvents txt_Mmin As TextBox
    Friend WithEvents txt_Mmax As TextBox
    Friend WithEvents lbl_Mmin As Label
    Friend WithEvents lbl_Mmax As Label
    Friend WithEvents etq_UnitDim1 As Label
    Friend WithEvents txt_Fleche As TextBox
    Friend WithEvents lbl_Fleche As Label
    Friend WithEvents etq_UnitForce2 As Label
    Friend WithEvents etq_UnitForce1 As Label
    Friend WithEvents txt_RZ2 As TextBox
    Friend WithEvents txt_RZ1 As TextBox
    Friend WithEvents lbl_RZ2 As Label
    Friend WithEvents lbl_RZ1 As Label
    Friend WithEvents lbl_RCalcul As Label
    Friend WithEvents lbl_RunCalcul As Label
    Friend WithEvents chk_Retrait As CheckBox
    Friend WithEvents cmb_LimitState As ComboBox
    Friend WithEvents lbl_LimitState As Label
End Class
