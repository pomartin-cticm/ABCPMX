<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MaintienBac
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
        Me.TLPan_MaitienB = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Panneau = New System.Windows.Forms.Label()
        Me.lbl_Floor = New System.Windows.Forms.Label()
        Me.pan_DefPlancher = New System.Windows.Forms.Panel()
        Me.chk_PriseEnCompteBac = New System.Windows.Forms.CheckBox()
        Me.cmb_Transition = New System.Windows.Forms.ComboBox()
        Me.lbl_Transition = New System.Windows.Forms.Label()
        Me.txt_NbSheetsTransverse = New System.Windows.Forms.TextBox()
        Me.lbl_NbSheetsTransverse = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_Largeur = New System.Windows.Forms.Label()
        Me.lbl_DimensionsGlobales = New System.Windows.Forms.Label()
        Me.lbl_Portee = New System.Windows.Forms.Label()
        Me.txt_LongueurP = New System.Windows.Forms.TextBox()
        Me.etq_UnitL2 = New System.Windows.Forms.Label()
        Me.etq_UnitL1 = New System.Windows.Forms.Label()
        Me.txt_LargeurP = New System.Windows.Forms.TextBox()
        Me.pan_Panneau = New System.Windows.Forms.Panel()
        Me.cmb_NbSpan = New System.Windows.Forms.ComboBox()
        Me.lbl_NbSpans = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbl_SheetWidth = New System.Windows.Forms.Label()
        Me.lbl_SheetLength = New System.Windows.Forms.Label()
        Me.etq_UnitL4 = New System.Windows.Forms.Label()
        Me.txt_SheetWidth = New System.Windows.Forms.TextBox()
        Me.etq_UnitL3 = New System.Windows.Forms.Label()
        Me.txt_SheetLength = New System.Windows.Forms.TextBox()
        Me.lbl_IndSheetDimensions = New System.Windows.Forms.Label()
        Me.img_Portees = New System.Windows.Forms.PictureBox()
        Me.TLpan_Centre = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Couturage = New System.Windows.Forms.Panel()
        Me.etq_UnitL5 = New System.Windows.Forms.Label()
        Me.txt_EspCouturage = New System.Windows.Forms.TextBox()
        Me.lbl_EspCouturage = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lbl_GlisseS_Info = New System.Windows.Forms.Label()
        Me.lbl_DiaS_Info = New System.Windows.Forms.Label()
        Me.lbl_GlisseS = New System.Windows.Forms.Label()
        Me.lbl_DiaS = New System.Windows.Forms.Label()
        Me.lbl_TypeCouturage = New System.Windows.Forms.Label()
        Me.cmb_TypSeamFastener = New System.Windows.Forms.ComboBox()
        Me.lbl_Couturage = New System.Windows.Forms.Label()
        Me.lbl_FixationSolive = New System.Windows.Forms.Label()
        Me.pan_FixationSolive = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lbl_SlipInfo = New System.Windows.Forms.Label()
        Me.lbl_DiametreInfo = New System.Windows.Forms.Label()
        Me.lbl_Glissement = New System.Windows.Forms.Label()
        Me.lbl_Diametre = New System.Windows.Forms.Label()
        Me.lbl_TypeFixation = New System.Windows.Forms.Label()
        Me.cmb_TypeFixation = New System.Windows.Forms.ComboBox()
        Me.cmb_FixationPoutre = New System.Windows.Forms.ComboBox()
        Me.lbl_Fixation = New System.Windows.Forms.Label()
        Me.TLpan_Calculs = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.img_Deck = New System.Windows.Forms.PictureBox()
        Me.pan_RigiditeShear = New System.Windows.Forms.Panel()
        Me.txt_Sact = New System.Windows.Forms.TextBox()
        Me.etq_UnitSact = New System.Windows.Forms.Label()
        Me.img_Sact = New System.Windows.Forms.PictureBox()
        Me.txt_c = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.img_c = New System.Windows.Forms.PictureBox()
        Me.txt_c22 = New System.Windows.Forms.TextBox()
        Me.etq_Unitc22 = New System.Windows.Forms.Label()
        Me.img_c22 = New System.Windows.Forms.PictureBox()
        Me.txt_c21 = New System.Windows.Forms.TextBox()
        Me.etq_Unitc21 = New System.Windows.Forms.Label()
        Me.img_c21 = New System.Windows.Forms.PictureBox()
        Me.txt_c12 = New System.Windows.Forms.TextBox()
        Me.etq_Unitc12 = New System.Windows.Forms.Label()
        Me.img_c12 = New System.Windows.Forms.PictureBox()
        Me.txt_c11 = New System.Windows.Forms.TextBox()
        Me.etq_UnitC11 = New System.Windows.Forms.Label()
        Me.img_c11 = New System.Windows.Forms.PictureBox()
        Me.txt_Alpha5 = New System.Windows.Forms.TextBox()
        Me.etq_UnitAlpha5 = New System.Windows.Forms.Label()
        Me.img_Alpha5 = New System.Windows.Forms.PictureBox()
        Me.txt_K = New System.Windows.Forms.TextBox()
        Me.etq_UnitK = New System.Windows.Forms.Label()
        Me.img_K = New System.Windows.Forms.PictureBox()
        Me.lbl_ShearRigidity = New System.Windows.Forms.Label()
        Me.pan_Rigidite = New System.Windows.Forms.Panel()
        Me.chk_Theta = New System.Windows.Forms.CheckBox()
        Me.txt_kTheta = New System.Windows.Forms.TextBox()
        Me.etq_UnitkTheta = New System.Windows.Forms.Label()
        Me.img_kTheta = New System.Windows.Forms.PictureBox()
        Me.txt_kThetaC = New System.Windows.Forms.TextBox()
        Me.etq_UnitkThetaC = New System.Windows.Forms.Label()
        Me.img_kThetaC = New System.Windows.Forms.PictureBox()
        Me.txt_kThetaA = New System.Windows.Forms.TextBox()
        Me.etq_UnitkThetaA = New System.Windows.Forms.Label()
        Me.img_kThetaA = New System.Windows.Forms.PictureBox()
        Me.lbl_BendingRigidity = New System.Windows.Forms.Label()
        Me.lbl_Calculs = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_MaitienB.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_DefPlancher.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pan_Panneau.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.img_Portees, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TLpan_Centre.SuspendLayout()
        Me.pan_Couturage.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pan_FixationSolive.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TLpan_Calculs.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.img_Deck, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_RigiditeShear.SuspendLayout()
        CType(Me.img_Sact, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_c, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_c22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_c21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_c12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_c11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Alpha5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_K, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Rigidite.SuspendLayout()
        CType(Me.img_kTheta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_kThetaC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_kThetaA, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pan_General.Size = New System.Drawing.Size(759, 427)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(759, 427)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 390)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(753, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(389, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(249, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_MaitienB)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(753, 381)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_MaitienB
        '
        Me.TLPan_MaitienB.ColumnCount = 4
        Me.TLPan_MaitienB.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_MaitienB.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 251.0!))
        Me.TLPan_MaitienB.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 251.0!))
        Me.TLPan_MaitienB.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_MaitienB.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_MaitienB.Controls.Add(Me.img_Portees, 3, 0)
        Me.TLPan_MaitienB.Controls.Add(Me.TLpan_Centre, 1, 0)
        Me.TLPan_MaitienB.Controls.Add(Me.TLpan_Calculs, 2, 0)
        Me.TLPan_MaitienB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_MaitienB.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_MaitienB.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_MaitienB.Name = "TLPan_MaitienB"
        Me.TLPan_MaitienB.RowCount = 1
        Me.TLPan_MaitienB.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_MaitienB.Size = New System.Drawing.Size(753, 381)
        Me.TLPan_MaitienB.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(250, 381)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Panneau, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Floor, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_DefPlancher, 0, 1)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Panneau, 0, 3)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 5
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(250, 381)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Panneau
        '
        Me.lbl_Panneau.AutoSize = True
        Me.lbl_Panneau.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Panneau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Panneau.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Panneau.Location = New System.Drawing.Point(0, 210)
        Me.lbl_Panneau.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Panneau.Name = "lbl_Panneau"
        Me.lbl_Panneau.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Panneau.TabIndex = 2
        Me.lbl_Panneau.Text = "lbl_Panneau"
        Me.lbl_Panneau.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Floor
        '
        Me.lbl_Floor.AutoSize = True
        Me.lbl_Floor.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Floor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Floor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Floor.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Floor.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Floor.Name = "lbl_Floor"
        Me.lbl_Floor.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Floor.TabIndex = 0
        Me.lbl_Floor.Text = "lbl_Floor"
        Me.lbl_Floor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_DefPlancher
        '
        Me.pan_DefPlancher.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_DefPlancher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_DefPlancher.Controls.Add(Me.chk_PriseEnCompteBac)
        Me.pan_DefPlancher.Controls.Add(Me.cmb_Transition)
        Me.pan_DefPlancher.Controls.Add(Me.lbl_Transition)
        Me.pan_DefPlancher.Controls.Add(Me.txt_NbSheetsTransverse)
        Me.pan_DefPlancher.Controls.Add(Me.lbl_NbSheetsTransverse)
        Me.pan_DefPlancher.Controls.Add(Me.Panel1)
        Me.pan_DefPlancher.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_DefPlancher.Location = New System.Drawing.Point(0, 30)
        Me.pan_DefPlancher.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_DefPlancher.Name = "pan_DefPlancher"
        Me.pan_DefPlancher.Size = New System.Drawing.Size(250, 179)
        Me.pan_DefPlancher.TabIndex = 1
        '
        'chk_PriseEnCompteBac
        '
        Me.chk_PriseEnCompteBac.AutoSize = True
        Me.chk_PriseEnCompteBac.Location = New System.Drawing.Point(8, 5)
        Me.chk_PriseEnCompteBac.Name = "chk_PriseEnCompteBac"
        Me.chk_PriseEnCompteBac.Size = New System.Drawing.Size(141, 17)
        Me.chk_PriseEnCompteBac.TabIndex = 7
        Me.chk_PriseEnCompteBac.Text = "chk_PriseEnCompteBac"
        Me.chk_PriseEnCompteBac.UseVisualStyleBackColor = True
        '
        'cmb_Transition
        '
        Me.cmb_Transition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Transition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Transition.FormattingEnabled = True
        Me.cmb_Transition.Location = New System.Drawing.Point(28, 152)
        Me.cmb_Transition.Name = "cmb_Transition"
        Me.cmb_Transition.Size = New System.Drawing.Size(196, 21)
        Me.cmb_Transition.TabIndex = 6
        '
        'lbl_Transition
        '
        Me.lbl_Transition.AutoSize = True
        Me.lbl_Transition.Location = New System.Drawing.Point(8, 134)
        Me.lbl_Transition.Name = "lbl_Transition"
        Me.lbl_Transition.Size = New System.Drawing.Size(69, 13)
        Me.lbl_Transition.TabIndex = 5
        Me.lbl_Transition.Text = "lbl_Transition"
        '
        'txt_NbSheetsTransverse
        '
        Me.txt_NbSheetsTransverse.Location = New System.Drawing.Point(149, 45)
        Me.txt_NbSheetsTransverse.Name = "txt_NbSheetsTransverse"
        Me.txt_NbSheetsTransverse.Size = New System.Drawing.Size(58, 20)
        Me.txt_NbSheetsTransverse.TabIndex = 1
        Me.txt_NbSheetsTransverse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_NbSheetsTransverse
        '
        Me.lbl_NbSheetsTransverse.AutoSize = True
        Me.lbl_NbSheetsTransverse.Location = New System.Drawing.Point(8, 29)
        Me.lbl_NbSheetsTransverse.Name = "lbl_NbSheetsTransverse"
        Me.lbl_NbSheetsTransverse.Size = New System.Drawing.Size(123, 13)
        Me.lbl_NbSheetsTransverse.TabIndex = 0
        Me.lbl_NbSheetsTransverse.Text = "lbl_NbSheetsTransverse"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbl_Largeur)
        Me.Panel1.Controls.Add(Me.lbl_DimensionsGlobales)
        Me.Panel1.Controls.Add(Me.lbl_Portee)
        Me.Panel1.Controls.Add(Me.txt_LongueurP)
        Me.Panel1.Controls.Add(Me.etq_UnitL2)
        Me.Panel1.Controls.Add(Me.etq_UnitL1)
        Me.Panel1.Controls.Add(Me.txt_LargeurP)
        Me.Panel1.Location = New System.Drawing.Point(1, 66)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(244, 64)
        Me.Panel1.TabIndex = 4
        '
        'lbl_Largeur
        '
        Me.lbl_Largeur.Location = New System.Drawing.Point(24, 43)
        Me.lbl_Largeur.Name = "lbl_Largeur"
        Me.lbl_Largeur.Size = New System.Drawing.Size(118, 13)
        Me.lbl_Largeur.TabIndex = 79
        Me.lbl_Largeur.Text = "lbl_Largeur"
        Me.lbl_Largeur.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_DimensionsGlobales
        '
        Me.lbl_DimensionsGlobales.AutoSize = True
        Me.lbl_DimensionsGlobales.Location = New System.Drawing.Point(7, 2)
        Me.lbl_DimensionsGlobales.Name = "lbl_DimensionsGlobales"
        Me.lbl_DimensionsGlobales.Size = New System.Drawing.Size(118, 13)
        Me.lbl_DimensionsGlobales.TabIndex = 2
        Me.lbl_DimensionsGlobales.Text = "lbl_DimensionsGlobales"
        '
        'lbl_Portee
        '
        Me.lbl_Portee.Location = New System.Drawing.Point(24, 22)
        Me.lbl_Portee.Name = "lbl_Portee"
        Me.lbl_Portee.Size = New System.Drawing.Size(118, 13)
        Me.lbl_Portee.TabIndex = 78
        Me.lbl_Portee.Text = "lbl_Portee"
        Me.lbl_Portee.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_LongueurP
        '
        Me.txt_LongueurP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_LongueurP.Location = New System.Drawing.Point(148, 18)
        Me.txt_LongueurP.Name = "txt_LongueurP"
        Me.txt_LongueurP.Size = New System.Drawing.Size(58, 20)
        Me.txt_LongueurP.TabIndex = 73
        Me.txt_LongueurP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitL2
        '
        Me.etq_UnitL2.AutoSize = True
        Me.etq_UnitL2.Location = New System.Drawing.Point(212, 43)
        Me.etq_UnitL2.Name = "etq_UnitL2"
        Me.etq_UnitL2.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL2.TabIndex = 77
        Me.etq_UnitL2.Text = "kN"
        '
        'etq_UnitL1
        '
        Me.etq_UnitL1.AutoSize = True
        Me.etq_UnitL1.Location = New System.Drawing.Point(212, 22)
        Me.etq_UnitL1.Name = "etq_UnitL1"
        Me.etq_UnitL1.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL1.TabIndex = 75
        Me.etq_UnitL1.Text = "kN"
        '
        'txt_LargeurP
        '
        Me.txt_LargeurP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_LargeurP.Location = New System.Drawing.Point(148, 39)
        Me.txt_LargeurP.Name = "txt_LargeurP"
        Me.txt_LargeurP.Size = New System.Drawing.Size(58, 20)
        Me.txt_LargeurP.TabIndex = 76
        Me.txt_LargeurP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'pan_Panneau
        '
        Me.pan_Panneau.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Panneau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Panneau.Controls.Add(Me.cmb_NbSpan)
        Me.pan_Panneau.Controls.Add(Me.lbl_NbSpans)
        Me.pan_Panneau.Controls.Add(Me.Panel2)
        Me.pan_Panneau.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Panneau.Location = New System.Drawing.Point(0, 240)
        Me.pan_Panneau.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Panneau.Name = "pan_Panneau"
        Me.pan_Panneau.Size = New System.Drawing.Size(250, 140)
        Me.pan_Panneau.TabIndex = 3
        '
        'cmb_NbSpan
        '
        Me.cmb_NbSpan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_NbSpan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbSpan.FormattingEnabled = True
        Me.cmb_NbSpan.Location = New System.Drawing.Point(149, 26)
        Me.cmb_NbSpan.Name = "cmb_NbSpan"
        Me.cmb_NbSpan.Size = New System.Drawing.Size(75, 21)
        Me.cmb_NbSpan.TabIndex = 5
        '
        'lbl_NbSpans
        '
        Me.lbl_NbSpans.AutoSize = True
        Me.lbl_NbSpans.Location = New System.Drawing.Point(8, 10)
        Me.lbl_NbSpans.Name = "lbl_NbSpans"
        Me.lbl_NbSpans.Size = New System.Drawing.Size(67, 13)
        Me.lbl_NbSpans.TabIndex = 3
        Me.lbl_NbSpans.Text = "lbl_NbSpans"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lbl_SheetWidth)
        Me.Panel2.Controls.Add(Me.lbl_SheetLength)
        Me.Panel2.Controls.Add(Me.etq_UnitL4)
        Me.Panel2.Controls.Add(Me.txt_SheetWidth)
        Me.Panel2.Controls.Add(Me.etq_UnitL3)
        Me.Panel2.Controls.Add(Me.txt_SheetLength)
        Me.Panel2.Controls.Add(Me.lbl_IndSheetDimensions)
        Me.Panel2.Location = New System.Drawing.Point(1, 54)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(244, 71)
        Me.Panel2.TabIndex = 5
        '
        'lbl_SheetWidth
        '
        Me.lbl_SheetWidth.Location = New System.Drawing.Point(24, 48)
        Me.lbl_SheetWidth.Name = "lbl_SheetWidth"
        Me.lbl_SheetWidth.Size = New System.Drawing.Size(118, 13)
        Me.lbl_SheetWidth.TabIndex = 93
        Me.lbl_SheetWidth.Text = "lbl_SheetWidth"
        Me.lbl_SheetWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_SheetLength
        '
        Me.lbl_SheetLength.Location = New System.Drawing.Point(24, 26)
        Me.lbl_SheetLength.Name = "lbl_SheetLength"
        Me.lbl_SheetLength.Size = New System.Drawing.Size(118, 13)
        Me.lbl_SheetLength.TabIndex = 92
        Me.lbl_SheetLength.Text = "lbl_SheetLength"
        Me.lbl_SheetLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitL4
        '
        Me.etq_UnitL4.AutoSize = True
        Me.etq_UnitL4.Location = New System.Drawing.Point(212, 48)
        Me.etq_UnitL4.Name = "etq_UnitL4"
        Me.etq_UnitL4.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL4.TabIndex = 91
        Me.etq_UnitL4.Text = "kN"
        '
        'txt_SheetWidth
        '
        Me.txt_SheetWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_SheetWidth.Location = New System.Drawing.Point(148, 44)
        Me.txt_SheetWidth.Name = "txt_SheetWidth"
        Me.txt_SheetWidth.Size = New System.Drawing.Size(58, 20)
        Me.txt_SheetWidth.TabIndex = 90
        Me.txt_SheetWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitL3
        '
        Me.etq_UnitL3.AutoSize = True
        Me.etq_UnitL3.Location = New System.Drawing.Point(212, 26)
        Me.etq_UnitL3.Name = "etq_UnitL3"
        Me.etq_UnitL3.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL3.TabIndex = 89
        Me.etq_UnitL3.Text = "kN"
        '
        'txt_SheetLength
        '
        Me.txt_SheetLength.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_SheetLength.Location = New System.Drawing.Point(148, 22)
        Me.txt_SheetLength.Name = "txt_SheetLength"
        Me.txt_SheetLength.Size = New System.Drawing.Size(58, 20)
        Me.txt_SheetLength.TabIndex = 88
        Me.txt_SheetLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_IndSheetDimensions
        '
        Me.lbl_IndSheetDimensions.AutoSize = True
        Me.lbl_IndSheetDimensions.Location = New System.Drawing.Point(7, 2)
        Me.lbl_IndSheetDimensions.Name = "lbl_IndSheetDimensions"
        Me.lbl_IndSheetDimensions.Size = New System.Drawing.Size(120, 13)
        Me.lbl_IndSheetDimensions.TabIndex = 87
        Me.lbl_IndSheetDimensions.Text = "lbl_IndSheetDimensions"
        '
        'img_Portees
        '
        Me.img_Portees.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Portees.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Portees.Location = New System.Drawing.Point(753, 0)
        Me.img_Portees.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.img_Portees.Name = "img_Portees"
        Me.img_Portees.Size = New System.Drawing.Size(1, 50)
        Me.img_Portees.TabIndex = 1
        Me.img_Portees.TabStop = False
        '
        'TLpan_Centre
        '
        Me.TLpan_Centre.ColumnCount = 1
        Me.TLpan_Centre.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Centre.Controls.Add(Me.pan_Couturage, 0, 3)
        Me.TLpan_Centre.Controls.Add(Me.lbl_Couturage, 0, 2)
        Me.TLpan_Centre.Controls.Add(Me.lbl_FixationSolive, 0, 0)
        Me.TLpan_Centre.Controls.Add(Me.pan_FixationSolive, 0, 1)
        Me.TLpan_Centre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Centre.Location = New System.Drawing.Point(251, 0)
        Me.TLpan_Centre.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.TLpan_Centre.Name = "TLpan_Centre"
        Me.TLpan_Centre.RowCount = 5
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180.0!))
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140.0!))
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Centre.Size = New System.Drawing.Size(250, 381)
        Me.TLpan_Centre.TabIndex = 2
        '
        'pan_Couturage
        '
        Me.pan_Couturage.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Couturage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Couturage.Controls.Add(Me.etq_UnitL5)
        Me.pan_Couturage.Controls.Add(Me.txt_EspCouturage)
        Me.pan_Couturage.Controls.Add(Me.lbl_EspCouturage)
        Me.pan_Couturage.Controls.Add(Me.Panel4)
        Me.pan_Couturage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Couturage.Location = New System.Drawing.Point(0, 240)
        Me.pan_Couturage.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Couturage.Name = "pan_Couturage"
        Me.pan_Couturage.Size = New System.Drawing.Size(250, 140)
        Me.pan_Couturage.TabIndex = 4
        '
        'etq_UnitL5
        '
        Me.etq_UnitL5.AutoSize = True
        Me.etq_UnitL5.Location = New System.Drawing.Point(214, 102)
        Me.etq_UnitL5.Name = "etq_UnitL5"
        Me.etq_UnitL5.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL5.TabIndex = 91
        Me.etq_UnitL5.Text = "kN"
        '
        'txt_EspCouturage
        '
        Me.txt_EspCouturage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EspCouturage.Location = New System.Drawing.Point(150, 98)
        Me.txt_EspCouturage.Name = "txt_EspCouturage"
        Me.txt_EspCouturage.Size = New System.Drawing.Size(58, 20)
        Me.txt_EspCouturage.TabIndex = 90
        Me.txt_EspCouturage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_EspCouturage
        '
        Me.lbl_EspCouturage.AutoSize = True
        Me.lbl_EspCouturage.Location = New System.Drawing.Point(8, 102)
        Me.lbl_EspCouturage.Name = "lbl_EspCouturage"
        Me.lbl_EspCouturage.Size = New System.Drawing.Size(90, 13)
        Me.lbl_EspCouturage.TabIndex = 12
        Me.lbl_EspCouturage.Text = "lbl_EspCouturage"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.lbl_GlisseS_Info)
        Me.Panel4.Controls.Add(Me.lbl_DiaS_Info)
        Me.Panel4.Controls.Add(Me.lbl_GlisseS)
        Me.Panel4.Controls.Add(Me.lbl_DiaS)
        Me.Panel4.Controls.Add(Me.lbl_TypeCouturage)
        Me.Panel4.Controls.Add(Me.cmb_TypSeamFastener)
        Me.Panel4.Location = New System.Drawing.Point(2, 4)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(244, 92)
        Me.Panel4.TabIndex = 11
        '
        'lbl_GlisseS_Info
        '
        Me.lbl_GlisseS_Info.AutoSize = True
        Me.lbl_GlisseS_Info.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_GlisseS_Info.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lbl_GlisseS_Info.Location = New System.Drawing.Point(114, 72)
        Me.lbl_GlisseS_Info.Name = "lbl_GlisseS_Info"
        Me.lbl_GlisseS_Info.Size = New System.Drawing.Size(82, 13)
        Me.lbl_GlisseS_Info.TabIndex = 12
        Me.lbl_GlisseS_Info.Text = "lbl_GlisseS_Info"
        '
        'lbl_DiaS_Info
        '
        Me.lbl_DiaS_Info.AutoSize = True
        Me.lbl_DiaS_Info.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DiaS_Info.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lbl_DiaS_Info.Location = New System.Drawing.Point(114, 53)
        Me.lbl_DiaS_Info.Name = "lbl_DiaS_Info"
        Me.lbl_DiaS_Info.Size = New System.Drawing.Size(70, 13)
        Me.lbl_DiaS_Info.TabIndex = 11
        Me.lbl_DiaS_Info.Text = "lbl_DiaS_Info"
        '
        'lbl_GlisseS
        '
        Me.lbl_GlisseS.AutoSize = True
        Me.lbl_GlisseS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Italic Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_GlisseS.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lbl_GlisseS.Location = New System.Drawing.Point(34, 72)
        Me.lbl_GlisseS.Name = "lbl_GlisseS"
        Me.lbl_GlisseS.Size = New System.Drawing.Size(58, 13)
        Me.lbl_GlisseS.TabIndex = 10
        Me.lbl_GlisseS.Text = "lbl_GlisseS"
        '
        'lbl_DiaS
        '
        Me.lbl_DiaS.AutoSize = True
        Me.lbl_DiaS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Italic Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DiaS.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lbl_DiaS.Location = New System.Drawing.Point(34, 53)
        Me.lbl_DiaS.Name = "lbl_DiaS"
        Me.lbl_DiaS.Size = New System.Drawing.Size(46, 13)
        Me.lbl_DiaS.TabIndex = 2
        Me.lbl_DiaS.Text = "lbl_DiaS"
        '
        'lbl_TypeCouturage
        '
        Me.lbl_TypeCouturage.AutoSize = True
        Me.lbl_TypeCouturage.Location = New System.Drawing.Point(6, 5)
        Me.lbl_TypeCouturage.Name = "lbl_TypeCouturage"
        Me.lbl_TypeCouturage.Size = New System.Drawing.Size(96, 13)
        Me.lbl_TypeCouturage.TabIndex = 8
        Me.lbl_TypeCouturage.Text = "lbl_TypeCouturage"
        '
        'cmb_TypSeamFastener
        '
        Me.cmb_TypSeamFastener.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_TypSeamFastener.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_TypSeamFastener.FormattingEnabled = True
        Me.cmb_TypSeamFastener.Location = New System.Drawing.Point(37, 24)
        Me.cmb_TypSeamFastener.Name = "cmb_TypSeamFastener"
        Me.cmb_TypSeamFastener.Size = New System.Drawing.Size(196, 21)
        Me.cmb_TypSeamFastener.TabIndex = 9
        '
        'lbl_Couturage
        '
        Me.lbl_Couturage.AutoSize = True
        Me.lbl_Couturage.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Couturage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Couturage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Couturage.Location = New System.Drawing.Point(0, 210)
        Me.lbl_Couturage.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Couturage.Name = "lbl_Couturage"
        Me.lbl_Couturage.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Couturage.TabIndex = 2
        Me.lbl_Couturage.Text = "lbl_Couturage"
        Me.lbl_Couturage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_FixationSolive
        '
        Me.lbl_FixationSolive.AutoSize = True
        Me.lbl_FixationSolive.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_FixationSolive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_FixationSolive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_FixationSolive.Location = New System.Drawing.Point(0, 0)
        Me.lbl_FixationSolive.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_FixationSolive.Name = "lbl_FixationSolive"
        Me.lbl_FixationSolive.Size = New System.Drawing.Size(250, 30)
        Me.lbl_FixationSolive.TabIndex = 1
        Me.lbl_FixationSolive.Text = "lbl_FixationSolive"
        Me.lbl_FixationSolive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_FixationSolive
        '
        Me.pan_FixationSolive.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_FixationSolive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_FixationSolive.Controls.Add(Me.Panel3)
        Me.pan_FixationSolive.Controls.Add(Me.cmb_FixationPoutre)
        Me.pan_FixationSolive.Controls.Add(Me.lbl_Fixation)
        Me.pan_FixationSolive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_FixationSolive.Location = New System.Drawing.Point(0, 30)
        Me.pan_FixationSolive.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_FixationSolive.Name = "pan_FixationSolive"
        Me.pan_FixationSolive.Size = New System.Drawing.Size(250, 179)
        Me.pan_FixationSolive.TabIndex = 3
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lbl_SlipInfo)
        Me.Panel3.Controls.Add(Me.lbl_DiametreInfo)
        Me.Panel3.Controls.Add(Me.lbl_Glissement)
        Me.Panel3.Controls.Add(Me.lbl_Diametre)
        Me.Panel3.Controls.Add(Me.lbl_TypeFixation)
        Me.Panel3.Controls.Add(Me.cmb_TypeFixation)
        Me.Panel3.Location = New System.Drawing.Point(2, 29)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(244, 92)
        Me.Panel3.TabIndex = 10
        '
        'lbl_SlipInfo
        '
        Me.lbl_SlipInfo.AutoSize = True
        Me.lbl_SlipInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_SlipInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lbl_SlipInfo.Location = New System.Drawing.Point(114, 72)
        Me.lbl_SlipInfo.Name = "lbl_SlipInfo"
        Me.lbl_SlipInfo.Size = New System.Drawing.Size(58, 13)
        Me.lbl_SlipInfo.TabIndex = 12
        Me.lbl_SlipInfo.Text = "lbl_SlipInfo"
        '
        'lbl_DiametreInfo
        '
        Me.lbl_DiametreInfo.AutoSize = True
        Me.lbl_DiametreInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DiametreInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lbl_DiametreInfo.Location = New System.Drawing.Point(114, 53)
        Me.lbl_DiametreInfo.Name = "lbl_DiametreInfo"
        Me.lbl_DiametreInfo.Size = New System.Drawing.Size(83, 13)
        Me.lbl_DiametreInfo.TabIndex = 11
        Me.lbl_DiametreInfo.Text = "lbl_DiametreInfo"
        '
        'lbl_Glissement
        '
        Me.lbl_Glissement.AutoSize = True
        Me.lbl_Glissement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Italic Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Glissement.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lbl_Glissement.Location = New System.Drawing.Point(34, 72)
        Me.lbl_Glissement.Name = "lbl_Glissement"
        Me.lbl_Glissement.Size = New System.Drawing.Size(74, 13)
        Me.lbl_Glissement.TabIndex = 10
        Me.lbl_Glissement.Text = "lbl_Glissement"
        '
        'lbl_Diametre
        '
        Me.lbl_Diametre.AutoSize = True
        Me.lbl_Diametre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Italic Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Diametre.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lbl_Diametre.Location = New System.Drawing.Point(34, 53)
        Me.lbl_Diametre.Name = "lbl_Diametre"
        Me.lbl_Diametre.Size = New System.Drawing.Size(65, 13)
        Me.lbl_Diametre.TabIndex = 2
        Me.lbl_Diametre.Text = "lbl_Diametre"
        '
        'lbl_TypeFixation
        '
        Me.lbl_TypeFixation.AutoSize = True
        Me.lbl_TypeFixation.Location = New System.Drawing.Point(6, 5)
        Me.lbl_TypeFixation.Name = "lbl_TypeFixation"
        Me.lbl_TypeFixation.Size = New System.Drawing.Size(83, 13)
        Me.lbl_TypeFixation.TabIndex = 8
        Me.lbl_TypeFixation.Text = "lbl_TypeFixation"
        '
        'cmb_TypeFixation
        '
        Me.cmb_TypeFixation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_TypeFixation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_TypeFixation.FormattingEnabled = True
        Me.cmb_TypeFixation.Location = New System.Drawing.Point(37, 24)
        Me.cmb_TypeFixation.Name = "cmb_TypeFixation"
        Me.cmb_TypeFixation.Size = New System.Drawing.Size(196, 21)
        Me.cmb_TypeFixation.TabIndex = 9
        '
        'cmb_FixationPoutre
        '
        Me.cmb_FixationPoutre.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_FixationPoutre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_FixationPoutre.FormattingEnabled = True
        Me.cmb_FixationPoutre.Location = New System.Drawing.Point(119, 6)
        Me.cmb_FixationPoutre.Name = "cmb_FixationPoutre"
        Me.cmb_FixationPoutre.Size = New System.Drawing.Size(116, 21)
        Me.cmb_FixationPoutre.TabIndex = 7
        '
        'lbl_Fixation
        '
        Me.lbl_Fixation.AutoSize = True
        Me.lbl_Fixation.Location = New System.Drawing.Point(8, 9)
        Me.lbl_Fixation.Name = "lbl_Fixation"
        Me.lbl_Fixation.Size = New System.Drawing.Size(59, 13)
        Me.lbl_Fixation.TabIndex = 6
        Me.lbl_Fixation.Text = "lbl_Fixation"
        '
        'TLpan_Calculs
        '
        Me.TLpan_Calculs.ColumnCount = 1
        Me.TLpan_Calculs.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Calculs.Controls.Add(Me.Panel5, 0, 1)
        Me.TLpan_Calculs.Controls.Add(Me.lbl_Calculs, 0, 0)
        Me.TLpan_Calculs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Calculs.Location = New System.Drawing.Point(502, 0)
        Me.TLpan_Calculs.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.TLpan_Calculs.Name = "TLpan_Calculs"
        Me.TLpan_Calculs.RowCount = 2
        Me.TLpan_Calculs.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Calculs.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Calculs.Size = New System.Drawing.Size(250, 381)
        Me.TLpan_Calculs.TabIndex = 3
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.img_Deck)
        Me.Panel5.Controls.Add(Me.pan_RigiditeShear)
        Me.Panel5.Controls.Add(Me.pan_Rigidite)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 30)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(250, 350)
        Me.Panel5.TabIndex = 4
        '
        'img_Deck
        '
        Me.img_Deck.Location = New System.Drawing.Point(3, 320)
        Me.img_Deck.Name = "img_Deck"
        Me.img_Deck.Size = New System.Drawing.Size(100, 25)
        Me.img_Deck.TabIndex = 122
        Me.img_Deck.TabStop = False
        '
        'pan_RigiditeShear
        '
        Me.pan_RigiditeShear.Controls.Add(Me.txt_Sact)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_UnitSact)
        Me.pan_RigiditeShear.Controls.Add(Me.img_Sact)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_c)
        Me.pan_RigiditeShear.Controls.Add(Me.Label1)
        Me.pan_RigiditeShear.Controls.Add(Me.img_c)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_c22)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_Unitc22)
        Me.pan_RigiditeShear.Controls.Add(Me.img_c22)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_c21)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_Unitc21)
        Me.pan_RigiditeShear.Controls.Add(Me.img_c21)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_c12)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_Unitc12)
        Me.pan_RigiditeShear.Controls.Add(Me.img_c12)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_c11)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_UnitC11)
        Me.pan_RigiditeShear.Controls.Add(Me.img_c11)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_Alpha5)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_UnitAlpha5)
        Me.pan_RigiditeShear.Controls.Add(Me.img_Alpha5)
        Me.pan_RigiditeShear.Controls.Add(Me.txt_K)
        Me.pan_RigiditeShear.Controls.Add(Me.etq_UnitK)
        Me.pan_RigiditeShear.Controls.Add(Me.img_K)
        Me.pan_RigiditeShear.Controls.Add(Me.lbl_ShearRigidity)
        Me.pan_RigiditeShear.Location = New System.Drawing.Point(0, 1)
        Me.pan_RigiditeShear.Name = "pan_RigiditeShear"
        Me.pan_RigiditeShear.Size = New System.Drawing.Size(250, 197)
        Me.pan_RigiditeShear.TabIndex = 121
        '
        'txt_Sact
        '
        Me.txt_Sact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sact.Location = New System.Drawing.Point(136, 170)
        Me.txt_Sact.Name = "txt_Sact"
        Me.txt_Sact.Size = New System.Drawing.Size(58, 20)
        Me.txt_Sact.TabIndex = 108
        Me.txt_Sact.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitSact
        '
        Me.etq_UnitSact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitSact.AutoSize = True
        Me.etq_UnitSact.Location = New System.Drawing.Point(199, 173)
        Me.etq_UnitSact.Name = "etq_UnitSact"
        Me.etq_UnitSact.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitSact.TabIndex = 107
        Me.etq_UnitSact.Text = "mm"
        '
        'img_Sact
        '
        Me.img_Sact.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Sact.Location = New System.Drawing.Point(99, 170)
        Me.img_Sact.Name = "img_Sact"
        Me.img_Sact.Size = New System.Drawing.Size(37, 20)
        Me.img_Sact.TabIndex = 109
        Me.img_Sact.TabStop = False
        '
        'txt_c
        '
        Me.txt_c.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_c.Location = New System.Drawing.Point(136, 149)
        Me.txt_c.Name = "txt_c"
        Me.txt_c.Size = New System.Drawing.Size(58, 20)
        Me.txt_c.TabIndex = 105
        Me.txt_c.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(199, 152)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 13)
        Me.Label1.TabIndex = 104
        Me.Label1.Text = "mm"
        '
        'img_c
        '
        Me.img_c.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_c.Location = New System.Drawing.Point(99, 149)
        Me.img_c.Name = "img_c"
        Me.img_c.Size = New System.Drawing.Size(37, 20)
        Me.img_c.TabIndex = 106
        Me.img_c.TabStop = False
        '
        'txt_c22
        '
        Me.txt_c22.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_c22.Location = New System.Drawing.Point(136, 128)
        Me.txt_c22.Name = "txt_c22"
        Me.txt_c22.Size = New System.Drawing.Size(58, 20)
        Me.txt_c22.TabIndex = 102
        Me.txt_c22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_Unitc22
        '
        Me.etq_Unitc22.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_Unitc22.AutoSize = True
        Me.etq_Unitc22.Location = New System.Drawing.Point(199, 131)
        Me.etq_Unitc22.Name = "etq_Unitc22"
        Me.etq_Unitc22.Size = New System.Drawing.Size(23, 13)
        Me.etq_Unitc22.TabIndex = 101
        Me.etq_Unitc22.Text = "mm"
        '
        'img_c22
        '
        Me.img_c22.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_c22.Location = New System.Drawing.Point(99, 128)
        Me.img_c22.Name = "img_c22"
        Me.img_c22.Size = New System.Drawing.Size(37, 20)
        Me.img_c22.TabIndex = 103
        Me.img_c22.TabStop = False
        '
        'txt_c21
        '
        Me.txt_c21.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_c21.Location = New System.Drawing.Point(136, 107)
        Me.txt_c21.Name = "txt_c21"
        Me.txt_c21.Size = New System.Drawing.Size(58, 20)
        Me.txt_c21.TabIndex = 99
        Me.txt_c21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_Unitc21
        '
        Me.etq_Unitc21.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_Unitc21.AutoSize = True
        Me.etq_Unitc21.Location = New System.Drawing.Point(199, 110)
        Me.etq_Unitc21.Name = "etq_Unitc21"
        Me.etq_Unitc21.Size = New System.Drawing.Size(23, 13)
        Me.etq_Unitc21.TabIndex = 98
        Me.etq_Unitc21.Text = "mm"
        '
        'img_c21
        '
        Me.img_c21.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_c21.Location = New System.Drawing.Point(99, 107)
        Me.img_c21.Name = "img_c21"
        Me.img_c21.Size = New System.Drawing.Size(37, 20)
        Me.img_c21.TabIndex = 100
        Me.img_c21.TabStop = False
        '
        'txt_c12
        '
        Me.txt_c12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_c12.Location = New System.Drawing.Point(136, 86)
        Me.txt_c12.Name = "txt_c12"
        Me.txt_c12.Size = New System.Drawing.Size(58, 20)
        Me.txt_c12.TabIndex = 96
        Me.txt_c12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_Unitc12
        '
        Me.etq_Unitc12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_Unitc12.AutoSize = True
        Me.etq_Unitc12.Location = New System.Drawing.Point(199, 89)
        Me.etq_Unitc12.Name = "etq_Unitc12"
        Me.etq_Unitc12.Size = New System.Drawing.Size(23, 13)
        Me.etq_Unitc12.TabIndex = 95
        Me.etq_Unitc12.Text = "mm"
        '
        'img_c12
        '
        Me.img_c12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_c12.Location = New System.Drawing.Point(99, 86)
        Me.img_c12.Name = "img_c12"
        Me.img_c12.Size = New System.Drawing.Size(37, 20)
        Me.img_c12.TabIndex = 97
        Me.img_c12.TabStop = False
        '
        'txt_c11
        '
        Me.txt_c11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_c11.Location = New System.Drawing.Point(136, 65)
        Me.txt_c11.Name = "txt_c11"
        Me.txt_c11.Size = New System.Drawing.Size(58, 20)
        Me.txt_c11.TabIndex = 93
        Me.txt_c11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitC11
        '
        Me.etq_UnitC11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitC11.AutoSize = True
        Me.etq_UnitC11.Location = New System.Drawing.Point(199, 68)
        Me.etq_UnitC11.Name = "etq_UnitC11"
        Me.etq_UnitC11.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitC11.TabIndex = 92
        Me.etq_UnitC11.Text = "mm"
        '
        'img_c11
        '
        Me.img_c11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_c11.Location = New System.Drawing.Point(99, 65)
        Me.img_c11.Name = "img_c11"
        Me.img_c11.Size = New System.Drawing.Size(37, 20)
        Me.img_c11.TabIndex = 94
        Me.img_c11.TabStop = False
        '
        'txt_Alpha5
        '
        Me.txt_Alpha5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Alpha5.Location = New System.Drawing.Point(136, 44)
        Me.txt_Alpha5.Name = "txt_Alpha5"
        Me.txt_Alpha5.Size = New System.Drawing.Size(58, 20)
        Me.txt_Alpha5.TabIndex = 90
        Me.txt_Alpha5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitAlpha5
        '
        Me.etq_UnitAlpha5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitAlpha5.AutoSize = True
        Me.etq_UnitAlpha5.Location = New System.Drawing.Point(199, 47)
        Me.etq_UnitAlpha5.Name = "etq_UnitAlpha5"
        Me.etq_UnitAlpha5.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitAlpha5.TabIndex = 89
        Me.etq_UnitAlpha5.Text = "mm"
        '
        'img_Alpha5
        '
        Me.img_Alpha5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Alpha5.Location = New System.Drawing.Point(99, 44)
        Me.img_Alpha5.Name = "img_Alpha5"
        Me.img_Alpha5.Size = New System.Drawing.Size(37, 20)
        Me.img_Alpha5.TabIndex = 91
        Me.img_Alpha5.TabStop = False
        '
        'txt_K
        '
        Me.txt_K.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_K.Location = New System.Drawing.Point(136, 23)
        Me.txt_K.Name = "txt_K"
        Me.txt_K.Size = New System.Drawing.Size(58, 20)
        Me.txt_K.TabIndex = 87
        Me.txt_K.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitK
        '
        Me.etq_UnitK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitK.AutoSize = True
        Me.etq_UnitK.Location = New System.Drawing.Point(199, 26)
        Me.etq_UnitK.Name = "etq_UnitK"
        Me.etq_UnitK.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitK.TabIndex = 86
        Me.etq_UnitK.Text = "mm"
        '
        'img_K
        '
        Me.img_K.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_K.Location = New System.Drawing.Point(99, 23)
        Me.img_K.Name = "img_K"
        Me.img_K.Size = New System.Drawing.Size(37, 20)
        Me.img_K.TabIndex = 88
        Me.img_K.TabStop = False
        '
        'lbl_ShearRigidity
        '
        Me.lbl_ShearRigidity.AutoSize = True
        Me.lbl_ShearRigidity.Location = New System.Drawing.Point(5, 4)
        Me.lbl_ShearRigidity.Name = "lbl_ShearRigidity"
        Me.lbl_ShearRigidity.Size = New System.Drawing.Size(85, 13)
        Me.lbl_ShearRigidity.TabIndex = 7
        Me.lbl_ShearRigidity.Text = "lbl_ShearRigidity"
        '
        'pan_Rigidite
        '
        Me.pan_Rigidite.Controls.Add(Me.chk_Theta)
        Me.pan_Rigidite.Controls.Add(Me.txt_kTheta)
        Me.pan_Rigidite.Controls.Add(Me.etq_UnitkTheta)
        Me.pan_Rigidite.Controls.Add(Me.img_kTheta)
        Me.pan_Rigidite.Controls.Add(Me.txt_kThetaC)
        Me.pan_Rigidite.Controls.Add(Me.etq_UnitkThetaC)
        Me.pan_Rigidite.Controls.Add(Me.img_kThetaC)
        Me.pan_Rigidite.Controls.Add(Me.txt_kThetaA)
        Me.pan_Rigidite.Controls.Add(Me.etq_UnitkThetaA)
        Me.pan_Rigidite.Controls.Add(Me.img_kThetaA)
        Me.pan_Rigidite.Controls.Add(Me.lbl_BendingRigidity)
        Me.pan_Rigidite.Location = New System.Drawing.Point(0, 198)
        Me.pan_Rigidite.Name = "pan_Rigidite"
        Me.pan_Rigidite.Size = New System.Drawing.Size(249, 137)
        Me.pan_Rigidite.TabIndex = 120
        '
        'chk_Theta
        '
        Me.chk_Theta.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chk_Theta.Location = New System.Drawing.Point(3, 100)
        Me.chk_Theta.Name = "chk_Theta"
        Me.chk_Theta.Size = New System.Drawing.Size(242, 34)
        Me.chk_Theta.TabIndex = 120
        Me.chk_Theta.Text = "chk_Theta"
        Me.chk_Theta.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chk_Theta.UseVisualStyleBackColor = True
        '
        'txt_kTheta
        '
        Me.txt_kTheta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_kTheta.Location = New System.Drawing.Point(136, 74)
        Me.txt_kTheta.Name = "txt_kTheta"
        Me.txt_kTheta.Size = New System.Drawing.Size(58, 20)
        Me.txt_kTheta.TabIndex = 118
        Me.txt_kTheta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitkTheta
        '
        Me.etq_UnitkTheta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitkTheta.AutoSize = True
        Me.etq_UnitkTheta.Location = New System.Drawing.Point(199, 77)
        Me.etq_UnitkTheta.Name = "etq_UnitkTheta"
        Me.etq_UnitkTheta.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitkTheta.TabIndex = 117
        Me.etq_UnitkTheta.Text = "mm"
        '
        'img_kTheta
        '
        Me.img_kTheta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_kTheta.Location = New System.Drawing.Point(75, 74)
        Me.img_kTheta.Name = "img_kTheta"
        Me.img_kTheta.Size = New System.Drawing.Size(61, 20)
        Me.img_kTheta.TabIndex = 119
        Me.img_kTheta.TabStop = False
        '
        'txt_kThetaC
        '
        Me.txt_kThetaC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_kThetaC.Location = New System.Drawing.Point(136, 52)
        Me.txt_kThetaC.Name = "txt_kThetaC"
        Me.txt_kThetaC.Size = New System.Drawing.Size(58, 20)
        Me.txt_kThetaC.TabIndex = 115
        Me.txt_kThetaC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitkThetaC
        '
        Me.etq_UnitkThetaC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitkThetaC.AutoSize = True
        Me.etq_UnitkThetaC.Location = New System.Drawing.Point(199, 55)
        Me.etq_UnitkThetaC.Name = "etq_UnitkThetaC"
        Me.etq_UnitkThetaC.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitkThetaC.TabIndex = 114
        Me.etq_UnitkThetaC.Text = "mm"
        '
        'img_kThetaC
        '
        Me.img_kThetaC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_kThetaC.Location = New System.Drawing.Point(75, 52)
        Me.img_kThetaC.Name = "img_kThetaC"
        Me.img_kThetaC.Size = New System.Drawing.Size(61, 20)
        Me.img_kThetaC.TabIndex = 116
        Me.img_kThetaC.TabStop = False
        '
        'txt_kThetaA
        '
        Me.txt_kThetaA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_kThetaA.Location = New System.Drawing.Point(136, 30)
        Me.txt_kThetaA.Name = "txt_kThetaA"
        Me.txt_kThetaA.Size = New System.Drawing.Size(58, 20)
        Me.txt_kThetaA.TabIndex = 112
        Me.txt_kThetaA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitkThetaA
        '
        Me.etq_UnitkThetaA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitkThetaA.AutoSize = True
        Me.etq_UnitkThetaA.Location = New System.Drawing.Point(199, 33)
        Me.etq_UnitkThetaA.Name = "etq_UnitkThetaA"
        Me.etq_UnitkThetaA.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitkThetaA.TabIndex = 111
        Me.etq_UnitkThetaA.Text = "mm"
        '
        'img_kThetaA
        '
        Me.img_kThetaA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_kThetaA.Location = New System.Drawing.Point(75, 30)
        Me.img_kThetaA.Name = "img_kThetaA"
        Me.img_kThetaA.Size = New System.Drawing.Size(61, 20)
        Me.img_kThetaA.TabIndex = 113
        Me.img_kThetaA.TabStop = False
        '
        'lbl_BendingRigidity
        '
        Me.lbl_BendingRigidity.AutoSize = True
        Me.lbl_BendingRigidity.Location = New System.Drawing.Point(5, 5)
        Me.lbl_BendingRigidity.Name = "lbl_BendingRigidity"
        Me.lbl_BendingRigidity.Size = New System.Drawing.Size(96, 13)
        Me.lbl_BendingRigidity.TabIndex = 110
        Me.lbl_BendingRigidity.Text = "lbl_BendingRigidity"
        '
        'lbl_Calculs
        '
        Me.lbl_Calculs.AutoSize = True
        Me.lbl_Calculs.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Calculs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Calculs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Calculs.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Calculs.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Calculs.Name = "lbl_Calculs"
        Me.lbl_Calculs.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Calculs.TabIndex = 2
        Me.lbl_Calculs.Text = "lbl_Calculs"
        Me.lbl_Calculs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Frm_MaintienBac
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(759, 427)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_MaintienBac"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_MaintienBac"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_MaitienB.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_DefPlancher.ResumeLayout(False)
        Me.pan_DefPlancher.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pan_Panneau.ResumeLayout(False)
        Me.pan_Panneau.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.img_Portees, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TLpan_Centre.ResumeLayout(False)
        Me.TLpan_Centre.PerformLayout()
        Me.pan_Couturage.ResumeLayout(False)
        Me.pan_Couturage.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.pan_FixationSolive.ResumeLayout(False)
        Me.pan_FixationSolive.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TLpan_Calculs.ResumeLayout(False)
        Me.TLpan_Calculs.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        CType(Me.img_Deck, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_RigiditeShear.ResumeLayout(False)
        Me.pan_RigiditeShear.PerformLayout()
        CType(Me.img_Sact, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_c, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_c22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_c21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_c12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_c11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Alpha5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_K, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Rigidite.ResumeLayout(False)
        Me.pan_Rigidite.PerformLayout()
        CType(Me.img_kTheta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_kThetaC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_kThetaA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_MaitienB As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Floor As Label
    Friend WithEvents pan_DefPlancher As Panel
    Friend WithEvents img_Portees As PictureBox
    Friend WithEvents lbl_DimensionsGlobales As Label
    Friend WithEvents txt_NbSheetsTransverse As TextBox
    Friend WithEvents lbl_NbSheetsTransverse As Label
    Friend WithEvents etq_UnitL2 As Label
    Friend WithEvents txt_LargeurP As TextBox
    Friend WithEvents etq_UnitL1 As Label
    Friend WithEvents txt_LongueurP As TextBox
    Friend WithEvents lbl_Portee As Label
    Friend WithEvents lbl_Largeur As Label
    Friend WithEvents lbl_Panneau As Label
    Friend WithEvents pan_Panneau As Panel
    Friend WithEvents lbl_NbSpans As Label
    Friend WithEvents cmb_NbSpan As ComboBox
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents cmb_Transition As ComboBox
    Friend WithEvents lbl_Transition As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lbl_SheetWidth As Label
    Friend WithEvents lbl_SheetLength As Label
    Friend WithEvents etq_UnitL4 As Label
    Friend WithEvents txt_SheetWidth As TextBox
    Friend WithEvents etq_UnitL3 As Label
    Friend WithEvents txt_SheetLength As TextBox
    Friend WithEvents lbl_IndSheetDimensions As Label
    Friend WithEvents TLpan_Centre As TableLayoutPanel
    Friend WithEvents pan_Couturage As Panel
    Friend WithEvents lbl_Couturage As Label
    Friend WithEvents lbl_FixationSolive As Label
    Friend WithEvents pan_FixationSolive As Panel
    Friend WithEvents cmb_FixationPoutre As ComboBox
    Friend WithEvents lbl_Fixation As Label
    Friend WithEvents cmb_TypeFixation As ComboBox
    Friend WithEvents lbl_TypeFixation As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents lbl_Diametre As Label
    Friend WithEvents lbl_Glissement As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents lbl_GlisseS_Info As Label
    Friend WithEvents lbl_DiaS_Info As Label
    Friend WithEvents lbl_GlisseS As Label
    Friend WithEvents lbl_DiaS As Label
    Friend WithEvents lbl_TypeCouturage As Label
    Friend WithEvents cmb_TypSeamFastener As ComboBox
    Friend WithEvents lbl_SlipInfo As Label
    Friend WithEvents lbl_DiametreInfo As Label
    Friend WithEvents etq_UnitL5 As Label
    Friend WithEvents txt_EspCouturage As TextBox
    Friend WithEvents lbl_EspCouturage As Label
    Friend WithEvents TLpan_Calculs As TableLayoutPanel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents lbl_ShearRigidity As Label
    Friend WithEvents lbl_Calculs As Label
    Friend WithEvents txt_c12 As TextBox
    Friend WithEvents etq_Unitc12 As Label
    Friend WithEvents img_c12 As PictureBox
    Friend WithEvents txt_c11 As TextBox
    Friend WithEvents etq_UnitC11 As Label
    Friend WithEvents img_c11 As PictureBox
    Friend WithEvents txt_Alpha5 As TextBox
    Friend WithEvents etq_UnitAlpha5 As Label
    Friend WithEvents img_Alpha5 As PictureBox
    Friend WithEvents txt_K As TextBox
    Friend WithEvents etq_UnitK As Label
    Friend WithEvents img_K As PictureBox
    Friend WithEvents txt_c22 As TextBox
    Friend WithEvents etq_Unitc22 As Label
    Friend WithEvents img_c22 As PictureBox
    Friend WithEvents txt_c21 As TextBox
    Friend WithEvents etq_Unitc21 As Label
    Friend WithEvents img_c21 As PictureBox
    Friend WithEvents txt_c As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents img_c As PictureBox
    Friend WithEvents txt_Sact As TextBox
    Friend WithEvents etq_UnitSact As Label
    Friend WithEvents img_Sact As PictureBox
    Friend WithEvents txt_kTheta As TextBox
    Friend WithEvents etq_UnitkTheta As Label
    Friend WithEvents img_kTheta As PictureBox
    Friend WithEvents txt_kThetaC As TextBox
    Friend WithEvents etq_UnitkThetaC As Label
    Friend WithEvents img_kThetaC As PictureBox
    Friend WithEvents txt_kThetaA As TextBox
    Friend WithEvents etq_UnitkThetaA As Label
    Friend WithEvents img_kThetaA As PictureBox
    Friend WithEvents lbl_BendingRigidity As Label
    Friend WithEvents chk_PriseEnCompteBac As CheckBox
    Friend WithEvents pan_RigiditeShear As Panel
    Friend WithEvents pan_Rigidite As Panel
    Friend WithEvents img_Deck As PictureBox
    Friend WithEvents chk_Theta As CheckBox
End Class
