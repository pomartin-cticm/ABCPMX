<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PPVerifications
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
        Me.img_Verifications = New System.Windows.Forms.PictureBox()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.chk_Retrait = New System.Windows.Forms.CheckBox()
        Me.chk_Numerotation = New System.Windows.Forms.CheckBox()
        Me.chk_Resistance = New System.Windows.Forms.CheckBox()
        Me.chk_Action = New System.Windows.Forms.CheckBox()
        Me.chk_Critere = New System.Windows.Forms.CheckBox()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.lbl_Verification = New System.Windows.Forms.Label()
        Me.TLpan_Modules = New System.Windows.Forms.TableLayoutPanel()
        Me.Pan_Results = New System.Windows.Forms.Panel()
        Me.etq_UnitM2 = New System.Windows.Forms.Label()
        Me.etq_UnitM1 = New System.Windows.Forms.Label()
        Me.txt_Node = New System.Windows.Forms.TextBox()
        Me.txt_Combi = New System.Windows.Forms.TextBox()
        Me.lbl_Node = New System.Windows.Forms.Label()
        Me.lbl_Combinaison = New System.Windows.Forms.Label()
        Me.etq_UnitDim1 = New System.Windows.Forms.Label()
        Me.txt_ValMax = New System.Windows.Forms.TextBox()
        Me.lbl_ValMaxCritere = New System.Windows.Forms.Label()
        Me.lbl_Resultats = New System.Windows.Forms.Label()
        Me.pan_ChoixCombi = New System.Windows.Forms.Panel()
        Me.cmb_LimitState = New System.Windows.Forms.ComboBox()
        Me.lbl_LimitState = New System.Windows.Forms.Label()
        Me.cmb_Critere = New System.Windows.Forms.ComboBox()
        Me.lbl_Critere = New System.Windows.Forms.Label()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.Pan_Affichage.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.pan_Image.SuspendLayout()
        Me.TLpan_HAffichage.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.img_Verifications, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pan_Main.Size = New System.Drawing.Size(947, 572)
        Me.pan_Main.TabIndex = 1
        '
        'TLpan_Main
        '
        Me.TLpan_Main.ColumnCount = 1
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Controls.Add(Me.Pan_Affichage, 0, 2)
        Me.TLpan_Main.Controls.Add(Me.TLPan_PartieBasse, 0, 3)
        Me.TLpan_Main.Controls.Add(Me.lbl_Verification, 0, 0)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(947, 572)
        Me.TLpan_Main.TabIndex = 0
        '
        'Pan_Affichage
        '
        Me.Pan_Affichage.Controls.Add(Me.TableLayoutPanel2)
        Me.Pan_Affichage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_Affichage.Location = New System.Drawing.Point(0, 101)
        Me.Pan_Affichage.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.Pan_Affichage.Name = "Pan_Affichage"
        Me.Pan_Affichage.Size = New System.Drawing.Size(947, 431)
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
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(947, 431)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'pan_Image
        '
        Me.pan_Image.Controls.Add(Me.TLpan_HAffichage)
        Me.pan_Image.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Image.Location = New System.Drawing.Point(151, 0)
        Me.pan_Image.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Image.Name = "pan_Image"
        Me.pan_Image.Size = New System.Drawing.Size(796, 431)
        Me.pan_Image.TabIndex = 3
        '
        'TLpan_HAffichage
        '
        Me.TLpan_HAffichage.ColumnCount = 1
        Me.TLpan_HAffichage.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_HAffichage.Controls.Add(Me.Panel1, 0, 0)
        Me.TLpan_HAffichage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_HAffichage.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_HAffichage.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_HAffichage.Name = "TLpan_HAffichage"
        Me.TLpan_HAffichage.RowCount = 1
        Me.TLpan_HAffichage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_HAffichage.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 431.0!))
        Me.TLpan_HAffichage.Size = New System.Drawing.Size(796, 431)
        Me.TLpan_HAffichage.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.img_Verifications)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(796, 431)
        Me.Panel1.TabIndex = 0
        '
        'img_Verifications
        '
        Me.img_Verifications.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Verifications.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Verifications.Location = New System.Drawing.Point(135, 115)
        Me.img_Verifications.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.img_Verifications.Name = "img_Verifications"
        Me.img_Verifications.Size = New System.Drawing.Size(100, 34)
        Me.img_Verifications.TabIndex = 2
        Me.img_Verifications.TabStop = False
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.chk_Retrait, 0, 4)
        Me.TLpan_Gauche.Controls.Add(Me.chk_Numerotation, 0, 3)
        Me.TLpan_Gauche.Controls.Add(Me.chk_Resistance, 0, 2)
        Me.TLpan_Gauche.Controls.Add(Me.chk_Action, 0, 1)
        Me.TLpan_Gauche.Controls.Add(Me.chk_Critere, 0, 0)
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
        Me.TLpan_Gauche.Size = New System.Drawing.Size(150, 431)
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
        Me.chk_Retrait.Visible = False
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
        'chk_Resistance
        '
        Me.chk_Resistance.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Resistance.AutoSize = True
        Me.chk_Resistance.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_Resistance.Location = New System.Drawing.Point(1, 61)
        Me.chk_Resistance.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_Resistance.Name = "chk_Resistance"
        Me.chk_Resistance.Size = New System.Drawing.Size(148, 28)
        Me.chk_Resistance.TabIndex = 3
        Me.chk_Resistance.Text = "chk_Resistance"
        Me.chk_Resistance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_Resistance.UseVisualStyleBackColor = True
        '
        'chk_Action
        '
        Me.chk_Action.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Action.AutoSize = True
        Me.chk_Action.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_Action.Location = New System.Drawing.Point(1, 31)
        Me.chk_Action.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_Action.Name = "chk_Action"
        Me.chk_Action.Size = New System.Drawing.Size(148, 28)
        Me.chk_Action.TabIndex = 2
        Me.chk_Action.Text = "chk_Action"
        Me.chk_Action.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_Action.UseVisualStyleBackColor = True
        '
        'chk_Critere
        '
        Me.chk_Critere.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_Critere.AutoSize = True
        Me.chk_Critere.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_Critere.Location = New System.Drawing.Point(1, 1)
        Me.chk_Critere.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_Critere.Name = "chk_Critere"
        Me.chk_Critere.Size = New System.Drawing.Size(148, 28)
        Me.chk_Critere.TabIndex = 0
        Me.chk_Critere.Text = "chk_Critere"
        Me.chk_Critere.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chk_Critere.UseVisualStyleBackColor = True
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 535)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(941, 34)
        Me.TLPan_PartieBasse.TabIndex = 5
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(483, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(343, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'lbl_Verification
        '
        Me.lbl_Verification.AutoSize = True
        Me.lbl_Verification.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Verification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Verification.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Verification.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Verification.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Verification.Name = "lbl_Verification"
        Me.lbl_Verification.Size = New System.Drawing.Size(947, 30)
        Me.lbl_Verification.TabIndex = 4
        Me.lbl_Verification.Text = "lbl_Verification"
        Me.lbl_Verification.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.TLpan_Modules.Size = New System.Drawing.Size(947, 70)
        Me.TLpan_Modules.TabIndex = 6
        '
        'Pan_Results
        '
        Me.Pan_Results.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Pan_Results.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pan_Results.Controls.Add(Me.etq_UnitM2)
        Me.Pan_Results.Controls.Add(Me.etq_UnitM1)
        Me.Pan_Results.Controls.Add(Me.txt_Node)
        Me.Pan_Results.Controls.Add(Me.txt_Combi)
        Me.Pan_Results.Controls.Add(Me.lbl_Node)
        Me.Pan_Results.Controls.Add(Me.lbl_Combinaison)
        Me.Pan_Results.Controls.Add(Me.etq_UnitDim1)
        Me.Pan_Results.Controls.Add(Me.txt_ValMax)
        Me.Pan_Results.Controls.Add(Me.lbl_ValMaxCritere)
        Me.Pan_Results.Controls.Add(Me.lbl_Resultats)
        Me.Pan_Results.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_Results.Location = New System.Drawing.Point(301, 1)
        Me.Pan_Results.Margin = New System.Windows.Forms.Padding(1, 1, 0, 0)
        Me.Pan_Results.Name = "Pan_Results"
        Me.Pan_Results.Size = New System.Drawing.Size(646, 69)
        Me.Pan_Results.TabIndex = 6
        '
        'etq_UnitM2
        '
        Me.etq_UnitM2.AutoSize = True
        Me.etq_UnitM2.Location = New System.Drawing.Point(310, 49)
        Me.etq_UnitM2.Name = "etq_UnitM2"
        Me.etq_UnitM2.Size = New System.Drawing.Size(15, 13)
        Me.etq_UnitM2.TabIndex = 76
        Me.etq_UnitM2.Text = "N"
        Me.etq_UnitM2.Visible = False
        '
        'etq_UnitM1
        '
        Me.etq_UnitM1.AutoSize = True
        Me.etq_UnitM1.Location = New System.Drawing.Point(310, 27)
        Me.etq_UnitM1.Name = "etq_UnitM1"
        Me.etq_UnitM1.Size = New System.Drawing.Size(15, 13)
        Me.etq_UnitM1.TabIndex = 75
        Me.etq_UnitM1.Text = "N"
        Me.etq_UnitM1.Visible = False
        '
        'txt_Node
        '
        Me.txt_Node.ForeColor = System.Drawing.Color.DarkRed
        Me.txt_Node.Location = New System.Drawing.Point(238, 46)
        Me.txt_Node.Name = "txt_Node"
        Me.txt_Node.Size = New System.Drawing.Size(66, 20)
        Me.txt_Node.TabIndex = 74
        Me.txt_Node.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_Combi
        '
        Me.txt_Combi.ForeColor = System.Drawing.Color.DarkRed
        Me.txt_Combi.Location = New System.Drawing.Point(238, 24)
        Me.txt_Combi.Name = "txt_Combi"
        Me.txt_Combi.Size = New System.Drawing.Size(66, 20)
        Me.txt_Combi.TabIndex = 73
        Me.txt_Combi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_Node
        '
        Me.lbl_Node.Location = New System.Drawing.Point(125, 45)
        Me.lbl_Node.Name = "lbl_Node"
        Me.lbl_Node.Size = New System.Drawing.Size(107, 17)
        Me.lbl_Node.TabIndex = 72
        Me.lbl_Node.Text = "lbl_Node"
        Me.lbl_Node.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_Combinaison
        '
        Me.lbl_Combinaison.Location = New System.Drawing.Point(122, 27)
        Me.lbl_Combinaison.Name = "lbl_Combinaison"
        Me.lbl_Combinaison.Size = New System.Drawing.Size(110, 13)
        Me.lbl_Combinaison.TabIndex = 71
        Me.lbl_Combinaison.Text = "lbl_Combinaison"
        Me.lbl_Combinaison.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitDim1
        '
        Me.etq_UnitDim1.AutoSize = True
        Me.etq_UnitDim1.Location = New System.Drawing.Point(310, 6)
        Me.etq_UnitDim1.Name = "etq_UnitDim1"
        Me.etq_UnitDim1.Size = New System.Drawing.Size(15, 13)
        Me.etq_UnitDim1.TabIndex = 70
        Me.etq_UnitDim1.Text = "N"
        Me.etq_UnitDim1.Visible = False
        '
        'txt_ValMax
        '
        Me.txt_ValMax.ForeColor = System.Drawing.Color.DarkRed
        Me.txt_ValMax.Location = New System.Drawing.Point(238, 3)
        Me.txt_ValMax.Name = "txt_ValMax"
        Me.txt_ValMax.Size = New System.Drawing.Size(66, 20)
        Me.txt_ValMax.TabIndex = 69
        Me.txt_ValMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_ValMaxCritere
        '
        Me.lbl_ValMaxCritere.Location = New System.Drawing.Point(119, 6)
        Me.lbl_ValMaxCritere.Name = "lbl_ValMaxCritere"
        Me.lbl_ValMaxCritere.Size = New System.Drawing.Size(113, 13)
        Me.lbl_ValMaxCritere.TabIndex = 68
        Me.lbl_ValMaxCritere.Text = "lbl_ValMaxCritere"
        Me.lbl_ValMaxCritere.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_Resultats
        '
        Me.lbl_Resultats.AutoSize = True
        Me.lbl_Resultats.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Resultats.Location = New System.Drawing.Point(18, 6)
        Me.lbl_Resultats.Name = "lbl_Resultats"
        Me.lbl_Resultats.Size = New System.Drawing.Size(67, 13)
        Me.lbl_Resultats.TabIndex = 60
        Me.lbl_Resultats.Text = "lbl_Resultats"
        '
        'pan_ChoixCombi
        '
        Me.pan_ChoixCombi.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ChoixCombi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ChoixCombi.Controls.Add(Me.cmb_LimitState)
        Me.pan_ChoixCombi.Controls.Add(Me.lbl_LimitState)
        Me.pan_ChoixCombi.Controls.Add(Me.cmb_Critere)
        Me.pan_ChoixCombi.Controls.Add(Me.lbl_Critere)
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
        Me.cmb_LimitState.Size = New System.Drawing.Size(186, 21)
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
        'cmb_Critere
        '
        Me.cmb_Critere.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Critere.FormattingEnabled = True
        Me.cmb_Critere.Location = New System.Drawing.Point(96, 34)
        Me.cmb_Critere.Name = "cmb_Critere"
        Me.cmb_Critere.Size = New System.Drawing.Size(186, 21)
        Me.cmb_Critere.TabIndex = 55
        '
        'lbl_Critere
        '
        Me.lbl_Critere.AutoSize = True
        Me.lbl_Critere.Location = New System.Drawing.Point(12, 37)
        Me.lbl_Critere.Name = "lbl_Critere"
        Me.lbl_Critere.Size = New System.Drawing.Size(53, 13)
        Me.lbl_Critere.TabIndex = 56
        Me.lbl_Critere.Text = "lbl_Critere"
        '
        'Frm_PPVerifications
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(947, 572)
        Me.Controls.Add(Me.pan_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_PPVerifications"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_PPVerifications"
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLpan_Main.PerformLayout()
        Me.Pan_Affichage.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.pan_Image.ResumeLayout(False)
        Me.TLpan_HAffichage.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.img_Verifications, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Pan_Affichage As Panel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents pan_Image As Panel
    Friend WithEvents TLpan_HAffichage As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents img_Verifications As PictureBox
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents chk_Retrait As CheckBox
    Friend WithEvents chk_Numerotation As CheckBox
    Friend WithEvents chk_Resistance As CheckBox
    Friend WithEvents chk_Action As CheckBox
    Friend WithEvents chk_Critere As CheckBox
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents lbl_Verification As Label
    Friend WithEvents TLpan_Modules As TableLayoutPanel
    Friend WithEvents Pan_Results As Panel
    Friend WithEvents etq_UnitM2 As Label
    Friend WithEvents etq_UnitM1 As Label
    Friend WithEvents txt_Node As TextBox
    Friend WithEvents txt_Combi As TextBox
    Friend WithEvents lbl_Node As Label
    Friend WithEvents lbl_Combinaison As Label
    Friend WithEvents etq_UnitDim1 As Label
    Friend WithEvents txt_ValMax As TextBox
    Friend WithEvents lbl_ValMaxCritere As Label
    Friend WithEvents lbl_Resultats As Label
    Friend WithEvents pan_ChoixCombi As Panel
    Friend WithEvents cmb_LimitState As ComboBox
    Friend WithEvents lbl_LimitState As Label
    Friend WithEvents cmb_Critere As ComboBox
    Friend WithEvents lbl_Critere As Label
End Class
