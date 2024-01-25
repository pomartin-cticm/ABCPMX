<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Hivoss
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
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_PartieHaute = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Droite = New System.Windows.Forms.Panel()
        Me.TLPan_Droite = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Amortissement = New System.Windows.Forms.Label()
        Me.pan_SaisieAmortissement = New System.Windows.Forms.Panel()
        Me.cmb_D2Value = New System.Windows.Forms.ComboBox()
        Me.img_DtotValue = New System.Windows.Forms.PictureBox()
        Me.img_DtotSymbol = New System.Windows.Forms.PictureBox()
        Me.lbl_AmortissementTotal = New System.Windows.Forms.Label()
        Me.img_D3Value = New System.Windows.Forms.PictureBox()
        Me.img_D3Symbol = New System.Windows.Forms.PictureBox()
        Me.chk_ChappeFlottante = New System.Windows.Forms.CheckBox()
        Me.chk_FauxPlafond = New System.Windows.Forms.CheckBox()
        Me.lbl_AmortissementFinition = New System.Windows.Forms.Label()
        Me.cmb_AmortissementMobilier = New System.Windows.Forms.ComboBox()
        Me.lbl_AmortissementMobilier = New System.Windows.Forms.Label()
        Me.img_D1Value = New System.Windows.Forms.PictureBox()
        Me.img_D1Symbol = New System.Windows.Forms.PictureBox()
        Me.img_D2Value = New System.Windows.Forms.PictureBox()
        Me.img_D2Symbol = New System.Windows.Forms.PictureBox()
        Me.lbl_PoutreAcier = New System.Windows.Forms.Label()
        Me.lbl_AmortissementStructure = New System.Windows.Forms.Label()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Options = New System.Windows.Forms.Label()
        Me.pan_SaisieOptions = New System.Windows.Forms.Panel()
        Me.chk_FrenquenceDalle = New System.Windows.Forms.CheckBox()
        Me.cmb_UtilisationPlancher = New System.Windows.Forms.ComboBox()
        Me.lbl_UtilisationPlancher = New System.Windows.Forms.Label()
        Me.cmb_choixQ = New System.Windows.Forms.ComboBox()
        Me.lbl_avec = New System.Windows.Forms.Label()
        Me.cmb_ratioQ = New System.Windows.Forms.ComboBox()
        Me.lbl_ComboMasseFrequence = New System.Windows.Forms.Label()
        Me.chk_methodeHIVOSS = New System.Windows.Forms.CheckBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_PartieHaute.SuspendLayout()
        Me.pan_Droite.SuspendLayout()
        Me.TLPan_Droite.SuspendLayout()
        Me.pan_SaisieAmortissement.SuspendLayout()
        CType(Me.img_DtotValue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_DtotSymbol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_D3Value, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_D3Symbol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_D1Value, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_D1Symbol, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_D2Value, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_D2Symbol, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_SaisieOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(749, 336)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(749, 336)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 299)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(743, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(384, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(244, 3)
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
        Me.pan_Main.Size = New System.Drawing.Size(743, 290)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_PartieHaute
        '
        Me.TLPan_PartieHaute.ColumnCount = 2
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.0!))
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.0!))
        Me.TLPan_PartieHaute.Controls.Add(Me.pan_Droite, 0, 0)
        Me.TLPan_PartieHaute.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_PartieHaute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieHaute.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_PartieHaute.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_PartieHaute.Name = "TLPan_PartieHaute"
        Me.TLPan_PartieHaute.RowCount = 1
        Me.TLPan_PartieHaute.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieHaute.Size = New System.Drawing.Size(743, 290)
        Me.TLPan_PartieHaute.TabIndex = 0
        '
        'pan_Droite
        '
        Me.pan_Droite.AutoScroll = True
        Me.pan_Droite.Controls.Add(Me.TLPan_Droite)
        Me.pan_Droite.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Droite.Location = New System.Drawing.Point(349, 0)
        Me.pan_Droite.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Droite.Name = "pan_Droite"
        Me.pan_Droite.Size = New System.Drawing.Size(394, 290)
        Me.pan_Droite.TabIndex = 1
        '
        'TLPan_Droite
        '
        Me.TLPan_Droite.ColumnCount = 1
        Me.TLPan_Droite.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Droite.Controls.Add(Me.lbl_Amortissement, 0, 0)
        Me.TLPan_Droite.Controls.Add(Me.pan_SaisieAmortissement, 0, 1)
        Me.TLPan_Droite.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Droite.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Droite.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Droite.Name = "TLPan_Droite"
        Me.TLPan_Droite.RowCount = 2
        Me.TLPan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Droite.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_Droite.Size = New System.Drawing.Size(394, 290)
        Me.TLPan_Droite.TabIndex = 0
        '
        'lbl_Amortissement
        '
        Me.lbl_Amortissement.AutoSize = True
        Me.lbl_Amortissement.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Amortissement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Amortissement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Amortissement.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Amortissement.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Amortissement.Name = "lbl_Amortissement"
        Me.lbl_Amortissement.Size = New System.Drawing.Size(394, 30)
        Me.lbl_Amortissement.TabIndex = 0
        Me.lbl_Amortissement.Text = "lbl_Amortissement"
        Me.lbl_Amortissement.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieAmortissement
        '
        Me.pan_SaisieAmortissement.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieAmortissement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieAmortissement.Controls.Add(Me.cmb_D2Value)
        Me.pan_SaisieAmortissement.Controls.Add(Me.img_DtotValue)
        Me.pan_SaisieAmortissement.Controls.Add(Me.img_DtotSymbol)
        Me.pan_SaisieAmortissement.Controls.Add(Me.lbl_AmortissementTotal)
        Me.pan_SaisieAmortissement.Controls.Add(Me.img_D3Value)
        Me.pan_SaisieAmortissement.Controls.Add(Me.img_D3Symbol)
        Me.pan_SaisieAmortissement.Controls.Add(Me.chk_ChappeFlottante)
        Me.pan_SaisieAmortissement.Controls.Add(Me.chk_FauxPlafond)
        Me.pan_SaisieAmortissement.Controls.Add(Me.lbl_AmortissementFinition)
        Me.pan_SaisieAmortissement.Controls.Add(Me.cmb_AmortissementMobilier)
        Me.pan_SaisieAmortissement.Controls.Add(Me.lbl_AmortissementMobilier)
        Me.pan_SaisieAmortissement.Controls.Add(Me.img_D1Value)
        Me.pan_SaisieAmortissement.Controls.Add(Me.img_D1Symbol)
        Me.pan_SaisieAmortissement.Controls.Add(Me.img_D2Value)
        Me.pan_SaisieAmortissement.Controls.Add(Me.img_D2Symbol)
        Me.pan_SaisieAmortissement.Controls.Add(Me.lbl_PoutreAcier)
        Me.pan_SaisieAmortissement.Controls.Add(Me.lbl_AmortissementStructure)
        Me.pan_SaisieAmortissement.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieAmortissement.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisieAmortissement.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisieAmortissement.Name = "pan_SaisieAmortissement"
        Me.pan_SaisieAmortissement.Size = New System.Drawing.Size(394, 260)
        Me.pan_SaisieAmortissement.TabIndex = 1
        '
        'cmb_D2Value
        '
        Me.cmb_D2Value.FormattingEnabled = True
        Me.cmb_D2Value.Location = New System.Drawing.Point(302, 77)
        Me.cmb_D2Value.Name = "cmb_D2Value"
        Me.cmb_D2Value.Size = New System.Drawing.Size(57, 21)
        Me.cmb_D2Value.TabIndex = 20
        '
        'img_DtotValue
        '
        Me.img_DtotValue.Location = New System.Drawing.Point(302, 216)
        Me.img_DtotValue.Name = "img_DtotValue"
        Me.img_DtotValue.Size = New System.Drawing.Size(57, 21)
        Me.img_DtotValue.TabIndex = 19
        Me.img_DtotValue.TabStop = False
        '
        'img_DtotSymbol
        '
        Me.img_DtotSymbol.Location = New System.Drawing.Point(239, 216)
        Me.img_DtotSymbol.Name = "img_DtotSymbol"
        Me.img_DtotSymbol.Size = New System.Drawing.Size(57, 21)
        Me.img_DtotSymbol.TabIndex = 18
        Me.img_DtotSymbol.TabStop = False
        '
        'lbl_AmortissementTotal
        '
        Me.lbl_AmortissementTotal.AutoSize = True
        Me.lbl_AmortissementTotal.Location = New System.Drawing.Point(19, 219)
        Me.lbl_AmortissementTotal.Name = "lbl_AmortissementTotal"
        Me.lbl_AmortissementTotal.Size = New System.Drawing.Size(115, 13)
        Me.lbl_AmortissementTotal.TabIndex = 17
        Me.lbl_AmortissementTotal.Text = "lbl_AmortissementTotal"
        '
        'img_D3Value
        '
        Me.img_D3Value.Location = New System.Drawing.Point(302, 171)
        Me.img_D3Value.Name = "img_D3Value"
        Me.img_D3Value.Size = New System.Drawing.Size(57, 21)
        Me.img_D3Value.TabIndex = 16
        Me.img_D3Value.TabStop = False
        '
        'img_D3Symbol
        '
        Me.img_D3Symbol.Location = New System.Drawing.Point(239, 171)
        Me.img_D3Symbol.Name = "img_D3Symbol"
        Me.img_D3Symbol.Size = New System.Drawing.Size(57, 21)
        Me.img_D3Symbol.TabIndex = 15
        Me.img_D3Symbol.TabStop = False
        '
        'chk_ChappeFlottante
        '
        Me.chk_ChappeFlottante.AutoSize = True
        Me.chk_ChappeFlottante.Location = New System.Drawing.Point(34, 187)
        Me.chk_ChappeFlottante.Name = "chk_ChappeFlottante"
        Me.chk_ChappeFlottante.Size = New System.Drawing.Size(128, 17)
        Me.chk_ChappeFlottante.TabIndex = 14
        Me.chk_ChappeFlottante.Text = "chk_ChappeFlottante"
        Me.chk_ChappeFlottante.UseVisualStyleBackColor = True
        '
        'chk_FauxPlafond
        '
        Me.chk_FauxPlafond.AutoSize = True
        Me.chk_FauxPlafond.Location = New System.Drawing.Point(34, 164)
        Me.chk_FauxPlafond.Name = "chk_FauxPlafond"
        Me.chk_FauxPlafond.Size = New System.Drawing.Size(109, 17)
        Me.chk_FauxPlafond.TabIndex = 13
        Me.chk_FauxPlafond.Text = "chk_FauxPlafond"
        Me.chk_FauxPlafond.UseVisualStyleBackColor = True
        '
        'lbl_AmortissementFinition
        '
        Me.lbl_AmortissementFinition.AutoSize = True
        Me.lbl_AmortissementFinition.Location = New System.Drawing.Point(19, 137)
        Me.lbl_AmortissementFinition.Name = "lbl_AmortissementFinition"
        Me.lbl_AmortissementFinition.Size = New System.Drawing.Size(124, 13)
        Me.lbl_AmortissementFinition.TabIndex = 12
        Me.lbl_AmortissementFinition.Text = "lbl_AmortissementFinition"
        '
        'cmb_AmortissementMobilier
        '
        Me.cmb_AmortissementMobilier.FormattingEnabled = True
        Me.cmb_AmortissementMobilier.Location = New System.Drawing.Point(31, 104)
        Me.cmb_AmortissementMobilier.Name = "cmb_AmortissementMobilier"
        Me.cmb_AmortissementMobilier.Size = New System.Drawing.Size(202, 21)
        Me.cmb_AmortissementMobilier.TabIndex = 7
        '
        'lbl_AmortissementMobilier
        '
        Me.lbl_AmortissementMobilier.AutoSize = True
        Me.lbl_AmortissementMobilier.Location = New System.Drawing.Point(19, 75)
        Me.lbl_AmortissementMobilier.Name = "lbl_AmortissementMobilier"
        Me.lbl_AmortissementMobilier.Size = New System.Drawing.Size(127, 13)
        Me.lbl_AmortissementMobilier.TabIndex = 11
        Me.lbl_AmortissementMobilier.Text = "lbl_AmortissementMobilier"
        '
        'img_D1Value
        '
        Me.img_D1Value.Location = New System.Drawing.Point(302, 42)
        Me.img_D1Value.Name = "img_D1Value"
        Me.img_D1Value.Size = New System.Drawing.Size(57, 21)
        Me.img_D1Value.TabIndex = 10
        Me.img_D1Value.TabStop = False
        '
        'img_D1Symbol
        '
        Me.img_D1Symbol.Location = New System.Drawing.Point(239, 42)
        Me.img_D1Symbol.Name = "img_D1Symbol"
        Me.img_D1Symbol.Size = New System.Drawing.Size(57, 21)
        Me.img_D1Symbol.TabIndex = 9
        Me.img_D1Symbol.TabStop = False
        '
        'img_D2Value
        '
        Me.img_D2Value.Location = New System.Drawing.Point(302, 104)
        Me.img_D2Value.Name = "img_D2Value"
        Me.img_D2Value.Size = New System.Drawing.Size(57, 21)
        Me.img_D2Value.TabIndex = 10
        Me.img_D2Value.TabStop = False
        '
        'img_D2Symbol
        '
        Me.img_D2Symbol.Location = New System.Drawing.Point(239, 104)
        Me.img_D2Symbol.Name = "img_D2Symbol"
        Me.img_D2Symbol.Size = New System.Drawing.Size(57, 21)
        Me.img_D2Symbol.TabIndex = 9
        Me.img_D2Symbol.TabStop = False
        '
        'lbl_PoutreAcier
        '
        Me.lbl_PoutreAcier.AutoSize = True
        Me.lbl_PoutreAcier.Location = New System.Drawing.Point(31, 46)
        Me.lbl_PoutreAcier.Name = "lbl_PoutreAcier"
        Me.lbl_PoutreAcier.Size = New System.Drawing.Size(78, 13)
        Me.lbl_PoutreAcier.TabIndex = 8
        Me.lbl_PoutreAcier.Text = "lbl_PoutreAcier"
        '
        'lbl_AmortissementStructure
        '
        Me.lbl_AmortissementStructure.AutoSize = True
        Me.lbl_AmortissementStructure.Location = New System.Drawing.Point(19, 16)
        Me.lbl_AmortissementStructure.Name = "lbl_AmortissementStructure"
        Me.lbl_AmortissementStructure.Size = New System.Drawing.Size(134, 13)
        Me.lbl_AmortissementStructure.TabIndex = 7
        Me.lbl_AmortissementStructure.Text = "lbl_AmortissementStructure"
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(348, 290)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Options, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisieOptions, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 2
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(348, 290)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Options
        '
        Me.lbl_Options.AutoSize = True
        Me.lbl_Options.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Options.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Options.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Options.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Options.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Options.Name = "lbl_Options"
        Me.lbl_Options.Size = New System.Drawing.Size(348, 30)
        Me.lbl_Options.TabIndex = 0
        Me.lbl_Options.Text = "lbl_Options"
        Me.lbl_Options.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieOptions
        '
        Me.pan_SaisieOptions.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieOptions.Controls.Add(Me.chk_FrenquenceDalle)
        Me.pan_SaisieOptions.Controls.Add(Me.cmb_UtilisationPlancher)
        Me.pan_SaisieOptions.Controls.Add(Me.lbl_UtilisationPlancher)
        Me.pan_SaisieOptions.Controls.Add(Me.cmb_choixQ)
        Me.pan_SaisieOptions.Controls.Add(Me.lbl_avec)
        Me.pan_SaisieOptions.Controls.Add(Me.cmb_ratioQ)
        Me.pan_SaisieOptions.Controls.Add(Me.lbl_ComboMasseFrequence)
        Me.pan_SaisieOptions.Controls.Add(Me.chk_methodeHIVOSS)
        Me.pan_SaisieOptions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieOptions.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisieOptions.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisieOptions.Name = "pan_SaisieOptions"
        Me.pan_SaisieOptions.Size = New System.Drawing.Size(348, 260)
        Me.pan_SaisieOptions.TabIndex = 1
        '
        'chk_FrenquenceDalle
        '
        Me.chk_FrenquenceDalle.AutoSize = True
        Me.chk_FrenquenceDalle.Location = New System.Drawing.Point(19, 187)
        Me.chk_FrenquenceDalle.Name = "chk_FrenquenceDalle"
        Me.chk_FrenquenceDalle.Size = New System.Drawing.Size(131, 17)
        Me.chk_FrenquenceDalle.TabIndex = 7
        Me.chk_FrenquenceDalle.Text = "chk_FrenquenceDalle"
        Me.chk_FrenquenceDalle.UseVisualStyleBackColor = True
        '
        'cmb_UtilisationPlancher
        '
        Me.cmb_UtilisationPlancher.FormattingEnabled = True
        Me.cmb_UtilisationPlancher.Location = New System.Drawing.Point(31, 134)
        Me.cmb_UtilisationPlancher.Name = "cmb_UtilisationPlancher"
        Me.cmb_UtilisationPlancher.Size = New System.Drawing.Size(221, 21)
        Me.cmb_UtilisationPlancher.TabIndex = 6
        '
        'lbl_UtilisationPlancher
        '
        Me.lbl_UtilisationPlancher.AutoSize = True
        Me.lbl_UtilisationPlancher.Location = New System.Drawing.Point(16, 107)
        Me.lbl_UtilisationPlancher.Name = "lbl_UtilisationPlancher"
        Me.lbl_UtilisationPlancher.Size = New System.Drawing.Size(110, 13)
        Me.lbl_UtilisationPlancher.TabIndex = 5
        Me.lbl_UtilisationPlancher.Text = "lbl_UtilisationPlancher"
        '
        'cmb_choixQ
        '
        Me.cmb_choixQ.FormattingEnabled = True
        Me.cmb_choixQ.Location = New System.Drawing.Point(168, 72)
        Me.cmb_choixQ.Name = "cmb_choixQ"
        Me.cmb_choixQ.Size = New System.Drawing.Size(84, 21)
        Me.cmb_choixQ.TabIndex = 4
        '
        'lbl_avec
        '
        Me.lbl_avec.AutoSize = True
        Me.lbl_avec.Location = New System.Drawing.Point(123, 75)
        Me.lbl_avec.Name = "lbl_avec"
        Me.lbl_avec.Size = New System.Drawing.Size(47, 13)
        Me.lbl_avec.TabIndex = 3
        Me.lbl_avec.Text = "lbl_avec"
        '
        'cmb_ratioQ
        '
        Me.cmb_ratioQ.FormattingEnabled = True
        Me.cmb_ratioQ.Location = New System.Drawing.Point(31, 72)
        Me.cmb_ratioQ.Name = "cmb_ratioQ"
        Me.cmb_ratioQ.Size = New System.Drawing.Size(84, 21)
        Me.cmb_ratioQ.TabIndex = 2
        '
        'lbl_ComboMasseFrequence
        '
        Me.lbl_ComboMasseFrequence.AutoSize = True
        Me.lbl_ComboMasseFrequence.Location = New System.Drawing.Point(16, 46)
        Me.lbl_ComboMasseFrequence.Name = "lbl_ComboMasseFrequence"
        Me.lbl_ComboMasseFrequence.Size = New System.Drawing.Size(138, 13)
        Me.lbl_ComboMasseFrequence.TabIndex = 1
        Me.lbl_ComboMasseFrequence.Text = "lbl_ComboMasseFrequence"
        '
        'chk_methodeHIVOSS
        '
        Me.chk_methodeHIVOSS.AutoSize = True
        Me.chk_methodeHIVOSS.Location = New System.Drawing.Point(19, 16)
        Me.chk_methodeHIVOSS.Name = "chk_methodeHIVOSS"
        Me.chk_methodeHIVOSS.Size = New System.Drawing.Size(131, 17)
        Me.chk_methodeHIVOSS.TabIndex = 0
        Me.chk_methodeHIVOSS.Text = "chk_methodeHIVOSS"
        Me.chk_methodeHIVOSS.UseVisualStyleBackColor = True
        '
        'Frm_Hivoss
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(749, 336)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Hivoss"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Hivoss"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_PartieHaute.ResumeLayout(False)
        Me.pan_Droite.ResumeLayout(False)
        Me.TLPan_Droite.ResumeLayout(False)
        Me.TLPan_Droite.PerformLayout()
        Me.pan_SaisieAmortissement.ResumeLayout(False)
        Me.pan_SaisieAmortissement.PerformLayout()
        CType(Me.img_DtotValue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_DtotSymbol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_D3Value, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_D3Symbol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_D1Value, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_D1Symbol, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_D2Value, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_D2Symbol, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_SaisieOptions.ResumeLayout(False)
        Me.pan_SaisieOptions.PerformLayout()
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
    Friend WithEvents lbl_Options As Label
    Friend WithEvents pan_SaisieOptions As Panel
    Friend WithEvents pan_Droite As Panel
    Friend WithEvents TLPan_Droite As TableLayoutPanel
    Friend WithEvents lbl_Amortissement As Label
    Friend WithEvents pan_SaisieAmortissement As Panel
    Friend WithEvents lbl_ComboMasseFrequence As Label
    Friend WithEvents chk_methodeHIVOSS As CheckBox
    Friend WithEvents cmb_UtilisationPlancher As ComboBox
    Friend WithEvents lbl_UtilisationPlancher As Label
    Friend WithEvents cmb_choixQ As ComboBox
    Friend WithEvents lbl_avec As Label
    Friend WithEvents cmb_ratioQ As ComboBox
    Friend WithEvents lbl_PoutreAcier As Label
    Friend WithEvents lbl_AmortissementStructure As Label
    Friend WithEvents img_DtotValue As PictureBox
    Friend WithEvents img_DtotSymbol As PictureBox
    Friend WithEvents lbl_AmortissementTotal As Label
    Friend WithEvents img_D3Value As PictureBox
    Friend WithEvents img_D3Symbol As PictureBox
    Friend WithEvents chk_ChappeFlottante As CheckBox
    Friend WithEvents chk_FauxPlafond As CheckBox
    Friend WithEvents lbl_AmortissementFinition As Label
    Friend WithEvents cmb_AmortissementMobilier As ComboBox
    Friend WithEvents lbl_AmortissementMobilier As Label
    Friend WithEvents img_D1Value As PictureBox
    Friend WithEvents img_D1Symbol As PictureBox
    Friend WithEvents img_D2Value As PictureBox
    Friend WithEvents img_D2Symbol As PictureBox
    Friend WithEvents cmb_D2Value As ComboBox
    Friend WithEvents chk_FrenquenceDalle As CheckBox
End Class
