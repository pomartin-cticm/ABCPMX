<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Etaiement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Etaiement))
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Etaiement = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Etaiement = New System.Windows.Forms.Label()
        Me.pan_SaisieEtaiement = New System.Windows.Forms.Panel()
        Me.rad_PointPropped = New System.Windows.Forms.RadioButton()
        Me.rad_FullyPropped = New System.Windows.Forms.RadioButton()
        Me.rad_UnPropped = New System.Windows.Forms.RadioButton()
        Me.pan_PointProps = New System.Windows.Forms.Panel()
        Me.pan_Nombre = New System.Windows.Forms.Panel()
        Me.lbl_NbPP = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btn_Precedent = New System.Windows.Forms.Button()
        Me.btn_Suivant = New System.Windows.Forms.Button()
        Me.cmb_NbPoints = New System.Windows.Forms.ComboBox()
        Me.pan_PositionCharges = New System.Windows.Forms.Panel()
        Me.lbl_LocPP = New System.Windows.Forms.Label()
        Me.rad_UnderBeam = New System.Windows.Forms.RadioButton()
        Me.rad_UnderSlab = New System.Windows.Forms.RadioButton()
        Me.pan_Consoles = New System.Windows.Forms.Panel()
        Me.chk_EtaisConsoleGauche = New System.Windows.Forms.CheckBox()
        Me.chk_EtaisConsoleDroite = New System.Windows.Forms.CheckBox()
        Me.img_Etaiement = New System.Windows.Forms.PictureBox()
        Me.imgList_PlusMoins = New System.Windows.Forms.ImageList(Me.components)
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Etaiement.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_SaisieEtaiement.SuspendLayout()
        Me.pan_PointProps.SuspendLayout()
        Me.pan_Nombre.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pan_PositionCharges.SuspendLayout()
        Me.pan_Consoles.SuspendLayout()
        CType(Me.img_Etaiement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(1096, 478)
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
        Me.TLpan_Main.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 2
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(1096, 478)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 5
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 3, 0)
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_Annuler, 1, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(4, 433)
        Me.TLPan_PartieBasse.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1088, 41)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(561, 4)
        Me.btn_OK.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(152, 33)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'btn_Annuler
        '
        Me.btn_Annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Annuler.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Annuler.Location = New System.Drawing.Point(374, 4)
        Me.btn_Annuler.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(152, 33)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_Etaiement)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(4, 4)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(1088, 421)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Etaiement
        '
        Me.TLPan_Etaiement.ColumnCount = 2
        Me.TLPan_Etaiement.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 333.0!))
        Me.TLPan_Etaiement.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Etaiement.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Etaiement.Controls.Add(Me.img_Etaiement, 1, 0)
        Me.TLPan_Etaiement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Etaiement.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Etaiement.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Etaiement.Name = "TLPan_Etaiement"
        Me.TLPan_Etaiement.RowCount = 1
        Me.TLPan_Etaiement.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Etaiement.Size = New System.Drawing.Size(1088, 421)
        Me.TLPan_Etaiement.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(333, 421)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Etaiement, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisieEtaiement, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 2
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 209.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(333, 421)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Etaiement
        '
        Me.lbl_Etaiement.AutoSize = True
        Me.lbl_Etaiement.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Etaiement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Etaiement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Etaiement.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Etaiement.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Etaiement.Name = "lbl_Etaiement"
        Me.lbl_Etaiement.Size = New System.Drawing.Size(333, 37)
        Me.lbl_Etaiement.TabIndex = 0
        Me.lbl_Etaiement.Text = "lbl_Etaiement"
        Me.lbl_Etaiement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieEtaiement
        '
        Me.pan_SaisieEtaiement.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieEtaiement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieEtaiement.Controls.Add(Me.rad_PointPropped)
        Me.pan_SaisieEtaiement.Controls.Add(Me.rad_FullyPropped)
        Me.pan_SaisieEtaiement.Controls.Add(Me.rad_UnPropped)
        Me.pan_SaisieEtaiement.Controls.Add(Me.pan_PointProps)
        Me.pan_SaisieEtaiement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieEtaiement.Location = New System.Drawing.Point(0, 37)
        Me.pan_SaisieEtaiement.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisieEtaiement.Name = "pan_SaisieEtaiement"
        Me.pan_SaisieEtaiement.Size = New System.Drawing.Size(333, 384)
        Me.pan_SaisieEtaiement.TabIndex = 1
        '
        'rad_PointPropped
        '
        Me.rad_PointPropped.AutoSize = True
        Me.rad_PointPropped.Location = New System.Drawing.Point(21, 116)
        Me.rad_PointPropped.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rad_PointPropped.Name = "rad_PointPropped"
        Me.rad_PointPropped.Size = New System.Drawing.Size(135, 20)
        Me.rad_PointPropped.TabIndex = 2
        Me.rad_PointPropped.TabStop = True
        Me.rad_PointPropped.Text = "rad_PointPropped"
        Me.rad_PointPropped.UseVisualStyleBackColor = True
        '
        'rad_FullyPropped
        '
        Me.rad_FullyPropped.AutoSize = True
        Me.rad_FullyPropped.Location = New System.Drawing.Point(21, 70)
        Me.rad_FullyPropped.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rad_FullyPropped.Name = "rad_FullyPropped"
        Me.rad_FullyPropped.Size = New System.Drawing.Size(133, 20)
        Me.rad_FullyPropped.TabIndex = 1
        Me.rad_FullyPropped.TabStop = True
        Me.rad_FullyPropped.Text = "rad_FullyPropped"
        Me.rad_FullyPropped.UseVisualStyleBackColor = True
        '
        'rad_UnPropped
        '
        Me.rad_UnPropped.AutoSize = True
        Me.rad_UnPropped.Location = New System.Drawing.Point(21, 23)
        Me.rad_UnPropped.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rad_UnPropped.Name = "rad_UnPropped"
        Me.rad_UnPropped.Size = New System.Drawing.Size(122, 20)
        Me.rad_UnPropped.TabIndex = 0
        Me.rad_UnPropped.TabStop = True
        Me.rad_UnPropped.Text = "rad_UnPropped"
        Me.rad_UnPropped.UseVisualStyleBackColor = True
        '
        'pan_PointProps
        '
        Me.pan_PointProps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_PointProps.Controls.Add(Me.pan_Nombre)
        Me.pan_PointProps.Controls.Add(Me.pan_PositionCharges)
        Me.pan_PointProps.Controls.Add(Me.pan_Consoles)
        Me.pan_PointProps.Location = New System.Drawing.Point(11, 129)
        Me.pan_PointProps.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_PointProps.Name = "pan_PointProps"
        Me.pan_PointProps.Size = New System.Drawing.Size(309, 237)
        Me.pan_PointProps.TabIndex = 8
        '
        'pan_Nombre
        '
        Me.pan_Nombre.Controls.Add(Me.lbl_NbPP)
        Me.pan_Nombre.Controls.Add(Me.Panel1)
        Me.pan_Nombre.Location = New System.Drawing.Point(0, 69)
        Me.pan_Nombre.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_Nombre.Name = "pan_Nombre"
        Me.pan_Nombre.Size = New System.Drawing.Size(307, 68)
        Me.pan_Nombre.TabIndex = 14
        '
        'lbl_NbPP
        '
        Me.lbl_NbPP.AutoSize = True
        Me.lbl_NbPP.Location = New System.Drawing.Point(5, 6)
        Me.lbl_NbPP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_NbPP.Name = "lbl_NbPP"
        Me.lbl_NbPP.Size = New System.Drawing.Size(64, 16)
        Me.lbl_NbPP.TabIndex = 6
        Me.lbl_NbPP.Text = "lbl_NbPP"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btn_Precedent)
        Me.Panel1.Controls.Add(Me.btn_Suivant)
        Me.Panel1.Controls.Add(Me.cmb_NbPoints)
        Me.Panel1.Location = New System.Drawing.Point(65, 28)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(181, 32)
        Me.Panel1.TabIndex = 12
        '
        'btn_Precedent
        '
        Me.btn_Precedent.Image = CType(resources.GetObject("btn_Precedent.Image"), System.Drawing.Image)
        Me.btn_Precedent.Location = New System.Drawing.Point(101, 1)
        Me.btn_Precedent.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_Precedent.Name = "btn_Precedent"
        Me.btn_Precedent.Size = New System.Drawing.Size(31, 28)
        Me.btn_Precedent.TabIndex = 10
        Me.btn_Precedent.TabStop = False
        Me.btn_Precedent.UseVisualStyleBackColor = True
        '
        'btn_Suivant
        '
        Me.btn_Suivant.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Suivant.Image = CType(resources.GetObject("btn_Suivant.Image"), System.Drawing.Image)
        Me.btn_Suivant.Location = New System.Drawing.Point(131, 1)
        Me.btn_Suivant.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_Suivant.Name = "btn_Suivant"
        Me.btn_Suivant.Size = New System.Drawing.Size(31, 28)
        Me.btn_Suivant.TabIndex = 9
        Me.btn_Suivant.TabStop = False
        Me.btn_Suivant.UseVisualStyleBackColor = True
        '
        'cmb_NbPoints
        '
        Me.cmb_NbPoints.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_NbPoints.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbPoints.FormattingEnabled = True
        Me.cmb_NbPoints.Location = New System.Drawing.Point(4, 2)
        Me.cmb_NbPoints.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmb_NbPoints.Name = "cmb_NbPoints"
        Me.cmb_NbPoints.Size = New System.Drawing.Size(93, 24)
        Me.cmb_NbPoints.TabIndex = 4
        '
        'pan_PositionCharges
        '
        Me.pan_PositionCharges.Controls.Add(Me.lbl_LocPP)
        Me.pan_PositionCharges.Controls.Add(Me.rad_UnderBeam)
        Me.pan_PositionCharges.Controls.Add(Me.rad_UnderSlab)
        Me.pan_PositionCharges.Location = New System.Drawing.Point(0, 140)
        Me.pan_PositionCharges.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_PositionCharges.Name = "pan_PositionCharges"
        Me.pan_PositionCharges.Size = New System.Drawing.Size(307, 95)
        Me.pan_PositionCharges.TabIndex = 14
        '
        'lbl_LocPP
        '
        Me.lbl_LocPP.AutoSize = True
        Me.lbl_LocPP.Location = New System.Drawing.Point(5, 5)
        Me.lbl_LocPP.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_LocPP.Name = "lbl_LocPP"
        Me.lbl_LocPP.Size = New System.Drawing.Size(68, 16)
        Me.lbl_LocPP.TabIndex = 6
        Me.lbl_LocPP.Text = "lbl_LocPP"
        '
        'rad_UnderBeam
        '
        Me.rad_UnderBeam.AutoSize = True
        Me.rad_UnderBeam.Checked = True
        Me.rad_UnderBeam.Location = New System.Drawing.Point(60, 32)
        Me.rad_UnderBeam.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rad_UnderBeam.Name = "rad_UnderBeam"
        Me.rad_UnderBeam.Size = New System.Drawing.Size(125, 20)
        Me.rad_UnderBeam.TabIndex = 8
        Me.rad_UnderBeam.TabStop = True
        Me.rad_UnderBeam.Text = "rad_UnderBeam"
        Me.rad_UnderBeam.UseVisualStyleBackColor = True
        '
        'rad_UnderSlab
        '
        Me.rad_UnderSlab.AutoSize = True
        Me.rad_UnderSlab.Location = New System.Drawing.Point(60, 60)
        Me.rad_UnderSlab.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.rad_UnderSlab.Name = "rad_UnderSlab"
        Me.rad_UnderSlab.Size = New System.Drawing.Size(117, 20)
        Me.rad_UnderSlab.TabIndex = 8
        Me.rad_UnderSlab.Text = "rad_UnderSlab"
        Me.rad_UnderSlab.UseVisualStyleBackColor = True
        '
        'pan_Consoles
        '
        Me.pan_Consoles.Controls.Add(Me.chk_EtaisConsoleGauche)
        Me.pan_Consoles.Controls.Add(Me.chk_EtaisConsoleDroite)
        Me.pan_Consoles.Location = New System.Drawing.Point(0, 6)
        Me.pan_Consoles.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_Consoles.Name = "pan_Consoles"
        Me.pan_Consoles.Size = New System.Drawing.Size(307, 59)
        Me.pan_Consoles.TabIndex = 13
        '
        'chk_EtaisConsoleGauche
        '
        Me.chk_EtaisConsoleGauche.AutoSize = True
        Me.chk_EtaisConsoleGauche.Location = New System.Drawing.Point(5, 6)
        Me.chk_EtaisConsoleGauche.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chk_EtaisConsoleGauche.Name = "chk_EtaisConsoleGauche"
        Me.chk_EtaisConsoleGauche.Size = New System.Drawing.Size(181, 20)
        Me.chk_EtaisConsoleGauche.TabIndex = 5
        Me.chk_EtaisConsoleGauche.Text = "chk_EtaisConsoleGauche"
        Me.chk_EtaisConsoleGauche.UseVisualStyleBackColor = True
        '
        'chk_EtaisConsoleDroite
        '
        Me.chk_EtaisConsoleDroite.AutoSize = True
        Me.chk_EtaisConsoleDroite.Location = New System.Drawing.Point(5, 34)
        Me.chk_EtaisConsoleDroite.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chk_EtaisConsoleDroite.Name = "chk_EtaisConsoleDroite"
        Me.chk_EtaisConsoleDroite.Size = New System.Drawing.Size(170, 20)
        Me.chk_EtaisConsoleDroite.TabIndex = 7
        Me.chk_EtaisConsoleDroite.Text = "chk_EtaisConsoleDroite"
        Me.chk_EtaisConsoleDroite.UseVisualStyleBackColor = True
        '
        'img_Etaiement
        '
        Me.img_Etaiement.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Etaiement.Location = New System.Drawing.Point(334, 0)
        Me.img_Etaiement.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.img_Etaiement.Name = "img_Etaiement"
        Me.img_Etaiement.Size = New System.Drawing.Size(133, 62)
        Me.img_Etaiement.TabIndex = 1
        Me.img_Etaiement.TabStop = False
        '
        'imgList_PlusMoins
        '
        Me.imgList_PlusMoins.ImageStream = CType(resources.GetObject("imgList_PlusMoins.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList_PlusMoins.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList_PlusMoins.Images.SetKeyName(0, "Moins")
        Me.imgList_PlusMoins.Images.SetKeyName(1, "MoinsNonDispo")
        Me.imgList_PlusMoins.Images.SetKeyName(2, "Plus")
        Me.imgList_PlusMoins.Images.SetKeyName(3, "PlusNonDispo")
        '
        'Frm_Etaiement
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(1096, 478)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Etaiement"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Etaiement"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Etaiement.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_SaisieEtaiement.ResumeLayout(False)
        Me.pan_SaisieEtaiement.PerformLayout()
        Me.pan_PointProps.ResumeLayout(False)
        Me.pan_Nombre.ResumeLayout(False)
        Me.pan_Nombre.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pan_PositionCharges.ResumeLayout(False)
        Me.pan_PositionCharges.PerformLayout()
        Me.pan_Consoles.ResumeLayout(False)
        Me.pan_Consoles.PerformLayout()
        CType(Me.img_Etaiement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Etaiement As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Etaiement As Label
    Friend WithEvents pan_SaisieEtaiement As Panel
    Friend WithEvents img_Etaiement As PictureBox
    Friend WithEvents rad_UnPropped As RadioButton
    Friend WithEvents rad_PointPropped As RadioButton
    Friend WithEvents rad_FullyPropped As RadioButton
    Friend WithEvents chk_EtaisConsoleGauche As CheckBox
    Friend WithEvents lbl_NbPP As Label
    Friend WithEvents chk_EtaisConsoleDroite As CheckBox
    Friend WithEvents pan_PointProps As Panel
    Friend WithEvents lbl_LocPP As Label
    Friend WithEvents rad_UnderSlab As RadioButton
    Friend WithEvents rad_UnderBeam As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btn_Precedent As Button
    Friend WithEvents btn_Suivant As Button
    Friend WithEvents cmb_NbPoints As ComboBox
    Friend WithEvents imgList_PlusMoins As ImageList
    Friend WithEvents pan_Consoles As Panel
    Friend WithEvents pan_Nombre As Panel
    Friend WithEvents pan_PositionCharges As Panel
End Class
