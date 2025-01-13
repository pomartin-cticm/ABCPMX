<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_OptionsCalculIncendie
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
        Me.pan_Incendie = New System.Windows.Forms.Panel()
        Me.TLpan_Conteneur = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Conteneur = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.img_TempMax = New System.Windows.Forms.PictureBox()
        Me.lbl_TempMax = New System.Windows.Forms.Label()
        Me.txt_TempMax = New System.Windows.Forms.TextBox()
        Me.etq_UnitTemp2 = New System.Windows.Forms.Label()
        Me.img_Deltat = New System.Windows.Forms.PictureBox()
        Me.img_T0 = New System.Windows.Forms.PictureBox()
        Me.lbl_IncrementTemps = New System.Windows.Forms.Label()
        Me.txt_IncrementTemps = New System.Windows.Forms.TextBox()
        Me.etq_UnitIncrementTemps = New System.Windows.Forms.Label()
        Me.lbl_TempReference = New System.Windows.Forms.Label()
        Me.txt_TempReference = New System.Windows.Forms.TextBox()
        Me.etq_UnitTemp1 = New System.Windows.Forms.Label()
        Me.lbl_Parametres = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.img_Phi = New System.Windows.Forms.PictureBox()
        Me.lbl_FormFactorPhi = New System.Windows.Forms.Label()
        Me.txt_Phi = New System.Windows.Forms.TextBox()
        Me.img_ksh = New System.Windows.Forms.PictureBox()
        Me.lbl_ShadowKsh = New System.Windows.Forms.Label()
        Me.txt_ksh = New System.Windows.Forms.TextBox()
        Me.img_AlphaCC = New System.Windows.Forms.PictureBox()
        Me.img_UnitAlphaCC = New System.Windows.Forms.PictureBox()
        Me.txt_AlphaCC = New System.Windows.Forms.TextBox()
        Me.lbl_AlphaCC = New System.Windows.Forms.Label()
        Me.img_AlphaC = New System.Windows.Forms.PictureBox()
        Me.img_UnitBoltzmann = New System.Windows.Forms.PictureBox()
        Me.img_EpsilonF = New System.Windows.Forms.PictureBox()
        Me.img_UnitAlphaC = New System.Windows.Forms.PictureBox()
        Me.img_EpsilonC = New System.Windows.Forms.PictureBox()
        Me.txt_AlphaC = New System.Windows.Forms.TextBox()
        Me.lbl_AlphaC = New System.Windows.Forms.Label()
        Me.lbl_EmissiviteFeu = New System.Windows.Forms.Label()
        Me.txt_EmissiviteFeu = New System.Windows.Forms.TextBox()
        Me.lbl_Constantes = New System.Windows.Forms.Label()
        Me.lbl_EmissiviteBeton = New System.Windows.Forms.Label()
        Me.txt_EmissiviteBeton = New System.Windows.Forms.TextBox()
        Me.lbl_Boltzman = New System.Windows.Forms.Label()
        Me.txt_Sigma = New System.Windows.Forms.TextBox()
        Me.img_Sigma = New System.Windows.Forms.PictureBox()
        Me.lbl_Incendie = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_Incendie.SuspendLayout()
        Me.TLpan_Conteneur.SuspendLayout()
        Me.pan_Conteneur.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.img_TempMax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Deltat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_T0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.img_Phi, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ksh, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_AlphaCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_UnitAlphaCC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_AlphaC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_UnitBoltzmann, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_EpsilonF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_UnitAlphaC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_EpsilonC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Sigma, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Incendie
        '
        Me.pan_Incendie.AutoScroll = True
        Me.pan_Incendie.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Incendie.Controls.Add(Me.TLpan_Conteneur)
        Me.pan_Incendie.Location = New System.Drawing.Point(24, 33)
        Me.pan_Incendie.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Incendie.Name = "pan_Incendie"
        Me.pan_Incendie.Size = New System.Drawing.Size(739, 499)
        Me.pan_Incendie.TabIndex = 3
        '
        'TLpan_Conteneur
        '
        Me.TLpan_Conteneur.ColumnCount = 1
        Me.TLpan_Conteneur.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_Conteneur.Controls.Add(Me.pan_Conteneur, 0, 0)
        Me.TLpan_Conteneur.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLpan_Conteneur.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Conteneur.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Conteneur.Name = "TLpan_Conteneur"
        Me.TLpan_Conteneur.RowCount = 1
        Me.TLpan_Conteneur.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_Conteneur.Size = New System.Drawing.Size(739, 477)
        Me.TLpan_Conteneur.TabIndex = 0
        '
        'pan_Conteneur
        '
        Me.pan_Conteneur.Controls.Add(Me.Panel2)
        Me.pan_Conteneur.Controls.Add(Me.Panel1)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Incendie)
        Me.pan_Conteneur.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Conteneur.Location = New System.Drawing.Point(0, 0)
        Me.pan_Conteneur.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Conteneur.Name = "pan_Conteneur"
        Me.pan_Conteneur.Size = New System.Drawing.Size(739, 477)
        Me.pan_Conteneur.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.img_TempMax)
        Me.Panel2.Controls.Add(Me.lbl_TempMax)
        Me.Panel2.Controls.Add(Me.txt_TempMax)
        Me.Panel2.Controls.Add(Me.etq_UnitTemp2)
        Me.Panel2.Controls.Add(Me.img_Deltat)
        Me.Panel2.Controls.Add(Me.img_T0)
        Me.Panel2.Controls.Add(Me.lbl_IncrementTemps)
        Me.Panel2.Controls.Add(Me.txt_IncrementTemps)
        Me.Panel2.Controls.Add(Me.etq_UnitIncrementTemps)
        Me.Panel2.Controls.Add(Me.lbl_TempReference)
        Me.Panel2.Controls.Add(Me.txt_TempReference)
        Me.Panel2.Controls.Add(Me.etq_UnitTemp1)
        Me.Panel2.Controls.Add(Me.lbl_Parametres)
        Me.Panel2.Location = New System.Drawing.Point(3, 241)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(732, 195)
        Me.Panel2.TabIndex = 106
        '
        'img_TempMax
        '
        Me.img_TempMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_TempMax.Location = New System.Drawing.Point(532, 53)
        Me.img_TempMax.Name = "img_TempMax"
        Me.img_TempMax.Size = New System.Drawing.Size(46, 20)
        Me.img_TempMax.TabIndex = 141
        Me.img_TempMax.TabStop = False
        '
        'lbl_TempMax
        '
        Me.lbl_TempMax.AutoSize = True
        Me.lbl_TempMax.Location = New System.Drawing.Point(39, 53)
        Me.lbl_TempMax.Name = "lbl_TempMax"
        Me.lbl_TempMax.Size = New System.Drawing.Size(70, 13)
        Me.lbl_TempMax.TabIndex = 140
        Me.lbl_TempMax.Text = "lbl_TempMax"
        Me.lbl_TempMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_TempMax
        '
        Me.txt_TempMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TempMax.Location = New System.Drawing.Point(578, 53)
        Me.txt_TempMax.Name = "txt_TempMax"
        Me.txt_TempMax.Size = New System.Drawing.Size(55, 20)
        Me.txt_TempMax.TabIndex = 138
        '
        'etq_UnitTemp2
        '
        Me.etq_UnitTemp2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitTemp2.AutoSize = True
        Me.etq_UnitTemp2.Location = New System.Drawing.Point(639, 56)
        Me.etq_UnitTemp2.Name = "etq_UnitTemp2"
        Me.etq_UnitTemp2.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitTemp2.TabIndex = 139
        Me.etq_UnitTemp2.Text = "Label1"
        Me.etq_UnitTemp2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_Deltat
        '
        Me.img_Deltat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Deltat.Location = New System.Drawing.Point(532, 80)
        Me.img_Deltat.Name = "img_Deltat"
        Me.img_Deltat.Size = New System.Drawing.Size(46, 20)
        Me.img_Deltat.TabIndex = 137
        Me.img_Deltat.TabStop = False
        Me.img_Deltat.Visible = False
        '
        'img_T0
        '
        Me.img_T0.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_T0.Location = New System.Drawing.Point(532, 27)
        Me.img_T0.Name = "img_T0"
        Me.img_T0.Size = New System.Drawing.Size(46, 20)
        Me.img_T0.TabIndex = 136
        Me.img_T0.TabStop = False
        '
        'lbl_IncrementTemps
        '
        Me.lbl_IncrementTemps.AutoSize = True
        Me.lbl_IncrementTemps.Location = New System.Drawing.Point(39, 80)
        Me.lbl_IncrementTemps.Name = "lbl_IncrementTemps"
        Me.lbl_IncrementTemps.Size = New System.Drawing.Size(102, 13)
        Me.lbl_IncrementTemps.TabIndex = 127
        Me.lbl_IncrementTemps.Text = "lbl_IncrementTemps"
        Me.lbl_IncrementTemps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_IncrementTemps.Visible = False
        '
        'txt_IncrementTemps
        '
        Me.txt_IncrementTemps.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_IncrementTemps.Location = New System.Drawing.Point(578, 80)
        Me.txt_IncrementTemps.Name = "txt_IncrementTemps"
        Me.txt_IncrementTemps.Size = New System.Drawing.Size(55, 20)
        Me.txt_IncrementTemps.TabIndex = 120
        Me.txt_IncrementTemps.Visible = False
        '
        'etq_UnitIncrementTemps
        '
        Me.etq_UnitIncrementTemps.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitIncrementTemps.AutoSize = True
        Me.etq_UnitIncrementTemps.Location = New System.Drawing.Point(639, 83)
        Me.etq_UnitIncrementTemps.Name = "etq_UnitIncrementTemps"
        Me.etq_UnitIncrementTemps.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitIncrementTemps.TabIndex = 126
        Me.etq_UnitIncrementTemps.Text = "Label1"
        Me.etq_UnitIncrementTemps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.etq_UnitIncrementTemps.Visible = False
        '
        'lbl_TempReference
        '
        Me.lbl_TempReference.AutoSize = True
        Me.lbl_TempReference.Location = New System.Drawing.Point(39, 27)
        Me.lbl_TempReference.Name = "lbl_TempReference"
        Me.lbl_TempReference.Size = New System.Drawing.Size(100, 13)
        Me.lbl_TempReference.TabIndex = 125
        Me.lbl_TempReference.Text = "lbl_TempReference"
        Me.lbl_TempReference.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_TempReference
        '
        Me.txt_TempReference.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TempReference.Location = New System.Drawing.Point(578, 27)
        Me.txt_TempReference.Name = "txt_TempReference"
        Me.txt_TempReference.Size = New System.Drawing.Size(55, 20)
        Me.txt_TempReference.TabIndex = 119
        '
        'etq_UnitTemp1
        '
        Me.etq_UnitTemp1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitTemp1.AutoSize = True
        Me.etq_UnitTemp1.Location = New System.Drawing.Point(639, 30)
        Me.etq_UnitTemp1.Name = "etq_UnitTemp1"
        Me.etq_UnitTemp1.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitTemp1.TabIndex = 124
        Me.etq_UnitTemp1.Text = "Label1"
        Me.etq_UnitTemp1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Parametres
        '
        Me.lbl_Parametres.AutoSize = True
        Me.lbl_Parametres.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Parametres.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Parametres.Location = New System.Drawing.Point(5, 5)
        Me.lbl_Parametres.Name = "lbl_Parametres"
        Me.lbl_Parametres.Size = New System.Drawing.Size(76, 13)
        Me.lbl_Parametres.TabIndex = 105
        Me.lbl_Parametres.Text = "lbl_Parametres"
        Me.lbl_Parametres.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.img_Phi)
        Me.Panel1.Controls.Add(Me.lbl_FormFactorPhi)
        Me.Panel1.Controls.Add(Me.txt_Phi)
        Me.Panel1.Controls.Add(Me.img_ksh)
        Me.Panel1.Controls.Add(Me.lbl_ShadowKsh)
        Me.Panel1.Controls.Add(Me.txt_ksh)
        Me.Panel1.Controls.Add(Me.img_AlphaCC)
        Me.Panel1.Controls.Add(Me.img_UnitAlphaCC)
        Me.Panel1.Controls.Add(Me.txt_AlphaCC)
        Me.Panel1.Controls.Add(Me.lbl_AlphaCC)
        Me.Panel1.Controls.Add(Me.img_AlphaC)
        Me.Panel1.Controls.Add(Me.img_UnitBoltzmann)
        Me.Panel1.Controls.Add(Me.img_EpsilonF)
        Me.Panel1.Controls.Add(Me.img_UnitAlphaC)
        Me.Panel1.Controls.Add(Me.img_EpsilonC)
        Me.Panel1.Controls.Add(Me.txt_AlphaC)
        Me.Panel1.Controls.Add(Me.lbl_AlphaC)
        Me.Panel1.Controls.Add(Me.lbl_EmissiviteFeu)
        Me.Panel1.Controls.Add(Me.txt_EmissiviteFeu)
        Me.Panel1.Controls.Add(Me.lbl_Constantes)
        Me.Panel1.Controls.Add(Me.lbl_EmissiviteBeton)
        Me.Panel1.Controls.Add(Me.txt_EmissiviteBeton)
        Me.Panel1.Controls.Add(Me.lbl_Boltzman)
        Me.Panel1.Controls.Add(Me.txt_Sigma)
        Me.Panel1.Controls.Add(Me.img_Sigma)
        Me.Panel1.Location = New System.Drawing.Point(3, 31)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(732, 209)
        Me.Panel1.TabIndex = 105
        '
        'img_Phi
        '
        Me.img_Phi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Phi.Location = New System.Drawing.Point(532, 179)
        Me.img_Phi.Name = "img_Phi"
        Me.img_Phi.Size = New System.Drawing.Size(46, 20)
        Me.img_Phi.TabIndex = 149
        Me.img_Phi.TabStop = False
        '
        'lbl_FormFactorPhi
        '
        Me.lbl_FormFactorPhi.AutoSize = True
        Me.lbl_FormFactorPhi.Location = New System.Drawing.Point(39, 182)
        Me.lbl_FormFactorPhi.Name = "lbl_FormFactorPhi"
        Me.lbl_FormFactorPhi.Size = New System.Drawing.Size(91, 13)
        Me.lbl_FormFactorPhi.TabIndex = 148
        Me.lbl_FormFactorPhi.Text = "lbl_FormFactorPhi"
        Me.lbl_FormFactorPhi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_Phi
        '
        Me.txt_Phi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Phi.Location = New System.Drawing.Point(578, 179)
        Me.txt_Phi.Name = "txt_Phi"
        Me.txt_Phi.Size = New System.Drawing.Size(55, 20)
        Me.txt_Phi.TabIndex = 147
        Me.txt_Phi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'img_ksh
        '
        Me.img_ksh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ksh.Location = New System.Drawing.Point(532, 153)
        Me.img_ksh.Name = "img_ksh"
        Me.img_ksh.Size = New System.Drawing.Size(46, 20)
        Me.img_ksh.TabIndex = 146
        Me.img_ksh.TabStop = False
        '
        'lbl_ShadowKsh
        '
        Me.lbl_ShadowKsh.AutoSize = True
        Me.lbl_ShadowKsh.Location = New System.Drawing.Point(39, 156)
        Me.lbl_ShadowKsh.Name = "lbl_ShadowKsh"
        Me.lbl_ShadowKsh.Size = New System.Drawing.Size(80, 13)
        Me.lbl_ShadowKsh.TabIndex = 145
        Me.lbl_ShadowKsh.Text = "lbl_ShadowKsh"
        Me.lbl_ShadowKsh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_ksh
        '
        Me.txt_ksh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ksh.Location = New System.Drawing.Point(578, 153)
        Me.txt_ksh.Name = "txt_ksh"
        Me.txt_ksh.Size = New System.Drawing.Size(55, 20)
        Me.txt_ksh.TabIndex = 144
        Me.txt_ksh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'img_AlphaCC
        '
        Me.img_AlphaCC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_AlphaCC.Location = New System.Drawing.Point(532, 127)
        Me.img_AlphaCC.Name = "img_AlphaCC"
        Me.img_AlphaCC.Size = New System.Drawing.Size(46, 20)
        Me.img_AlphaCC.TabIndex = 143
        Me.img_AlphaCC.TabStop = False
        '
        'img_UnitAlphaCC
        '
        Me.img_UnitAlphaCC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_UnitAlphaCC.Location = New System.Drawing.Point(634, 127)
        Me.img_UnitAlphaCC.Name = "img_UnitAlphaCC"
        Me.img_UnitAlphaCC.Size = New System.Drawing.Size(61, 20)
        Me.img_UnitAlphaCC.TabIndex = 142
        Me.img_UnitAlphaCC.TabStop = False
        '
        'txt_AlphaCC
        '
        Me.txt_AlphaCC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_AlphaCC.Location = New System.Drawing.Point(578, 127)
        Me.txt_AlphaCC.Name = "txt_AlphaCC"
        Me.txt_AlphaCC.Size = New System.Drawing.Size(55, 20)
        Me.txt_AlphaCC.TabIndex = 140
        Me.txt_AlphaCC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_AlphaCC
        '
        Me.lbl_AlphaCC.AutoSize = True
        Me.lbl_AlphaCC.Location = New System.Drawing.Point(39, 128)
        Me.lbl_AlphaCC.Name = "lbl_AlphaCC"
        Me.lbl_AlphaCC.Size = New System.Drawing.Size(64, 13)
        Me.lbl_AlphaCC.TabIndex = 141
        Me.lbl_AlphaCC.Text = "lbl_AlphaCC"
        Me.lbl_AlphaCC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_AlphaC
        '
        Me.img_AlphaC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_AlphaC.Location = New System.Drawing.Point(532, 101)
        Me.img_AlphaC.Name = "img_AlphaC"
        Me.img_AlphaC.Size = New System.Drawing.Size(46, 20)
        Me.img_AlphaC.TabIndex = 139
        Me.img_AlphaC.TabStop = False
        '
        'img_UnitBoltzmann
        '
        Me.img_UnitBoltzmann.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_UnitBoltzmann.Location = New System.Drawing.Point(634, 23)
        Me.img_UnitBoltzmann.Name = "img_UnitBoltzmann"
        Me.img_UnitBoltzmann.Size = New System.Drawing.Size(95, 20)
        Me.img_UnitBoltzmann.TabIndex = 138
        Me.img_UnitBoltzmann.TabStop = False
        '
        'img_EpsilonF
        '
        Me.img_EpsilonF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_EpsilonF.Location = New System.Drawing.Point(532, 75)
        Me.img_EpsilonF.Name = "img_EpsilonF"
        Me.img_EpsilonF.Size = New System.Drawing.Size(46, 20)
        Me.img_EpsilonF.TabIndex = 135
        Me.img_EpsilonF.TabStop = False
        '
        'img_UnitAlphaC
        '
        Me.img_UnitAlphaC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_UnitAlphaC.Location = New System.Drawing.Point(634, 101)
        Me.img_UnitAlphaC.Name = "img_UnitAlphaC"
        Me.img_UnitAlphaC.Size = New System.Drawing.Size(61, 20)
        Me.img_UnitAlphaC.TabIndex = 129
        Me.img_UnitAlphaC.TabStop = False
        '
        'img_EpsilonC
        '
        Me.img_EpsilonC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_EpsilonC.Location = New System.Drawing.Point(532, 49)
        Me.img_EpsilonC.Name = "img_EpsilonC"
        Me.img_EpsilonC.Size = New System.Drawing.Size(46, 20)
        Me.img_EpsilonC.TabIndex = 134
        Me.img_EpsilonC.TabStop = False
        '
        'txt_AlphaC
        '
        Me.txt_AlphaC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_AlphaC.Location = New System.Drawing.Point(578, 101)
        Me.txt_AlphaC.Name = "txt_AlphaC"
        Me.txt_AlphaC.Size = New System.Drawing.Size(55, 20)
        Me.txt_AlphaC.TabIndex = 121
        Me.txt_AlphaC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_AlphaC
        '
        Me.lbl_AlphaC.AutoSize = True
        Me.lbl_AlphaC.Location = New System.Drawing.Point(39, 102)
        Me.lbl_AlphaC.Name = "lbl_AlphaC"
        Me.lbl_AlphaC.Size = New System.Drawing.Size(57, 13)
        Me.lbl_AlphaC.TabIndex = 128
        Me.lbl_AlphaC.Text = "lbl_AlphaC"
        Me.lbl_AlphaC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_EmissiviteFeu
        '
        Me.lbl_EmissiviteFeu.AutoSize = True
        Me.lbl_EmissiviteFeu.Location = New System.Drawing.Point(39, 78)
        Me.lbl_EmissiviteFeu.Name = "lbl_EmissiviteFeu"
        Me.lbl_EmissiviteFeu.Size = New System.Drawing.Size(87, 13)
        Me.lbl_EmissiviteFeu.TabIndex = 131
        Me.lbl_EmissiviteFeu.Text = "lbl_EmissiviteFeu"
        Me.lbl_EmissiviteFeu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_EmissiviteFeu
        '
        Me.txt_EmissiviteFeu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EmissiviteFeu.Location = New System.Drawing.Point(578, 75)
        Me.txt_EmissiviteFeu.Name = "txt_EmissiviteFeu"
        Me.txt_EmissiviteFeu.Size = New System.Drawing.Size(55, 20)
        Me.txt_EmissiviteFeu.TabIndex = 123
        Me.txt_EmissiviteFeu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_Constantes
        '
        Me.lbl_Constantes.AutoSize = True
        Me.lbl_Constantes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Constantes.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Constantes.Location = New System.Drawing.Point(5, 5)
        Me.lbl_Constantes.Name = "lbl_Constantes"
        Me.lbl_Constantes.Size = New System.Drawing.Size(76, 13)
        Me.lbl_Constantes.TabIndex = 104
        Me.lbl_Constantes.Text = "lbl_Constantes"
        Me.lbl_Constantes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_EmissiviteBeton
        '
        Me.lbl_EmissiviteBeton.AutoSize = True
        Me.lbl_EmissiviteBeton.Location = New System.Drawing.Point(39, 52)
        Me.lbl_EmissiviteBeton.Name = "lbl_EmissiviteBeton"
        Me.lbl_EmissiviteBeton.Size = New System.Drawing.Size(97, 13)
        Me.lbl_EmissiviteBeton.TabIndex = 130
        Me.lbl_EmissiviteBeton.Text = "lbl_EmissiviteBeton"
        Me.lbl_EmissiviteBeton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_EmissiviteBeton
        '
        Me.txt_EmissiviteBeton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EmissiviteBeton.Location = New System.Drawing.Point(578, 49)
        Me.txt_EmissiviteBeton.Name = "txt_EmissiviteBeton"
        Me.txt_EmissiviteBeton.Size = New System.Drawing.Size(55, 20)
        Me.txt_EmissiviteBeton.TabIndex = 122
        Me.txt_EmissiviteBeton.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_Boltzman
        '
        Me.lbl_Boltzman.AutoSize = True
        Me.lbl_Boltzman.Location = New System.Drawing.Point(39, 30)
        Me.lbl_Boltzman.Name = "lbl_Boltzman"
        Me.lbl_Boltzman.Size = New System.Drawing.Size(66, 13)
        Me.lbl_Boltzman.TabIndex = 102
        Me.lbl_Boltzman.Text = "lbl_Boltzman"
        '
        'txt_Sigma
        '
        Me.txt_Sigma.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Sigma.Location = New System.Drawing.Point(578, 23)
        Me.txt_Sigma.Name = "txt_Sigma"
        Me.txt_Sigma.Size = New System.Drawing.Size(55, 20)
        Me.txt_Sigma.TabIndex = 99
        Me.txt_Sigma.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'img_Sigma
        '
        Me.img_Sigma.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Sigma.Location = New System.Drawing.Point(532, 23)
        Me.img_Sigma.Name = "img_Sigma"
        Me.img_Sigma.Size = New System.Drawing.Size(46, 20)
        Me.img_Sigma.TabIndex = 100
        Me.img_Sigma.TabStop = False
        '
        'lbl_Incendie
        '
        Me.lbl_Incendie.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Incendie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Incendie.Location = New System.Drawing.Point(3, 2)
        Me.lbl_Incendie.Name = "lbl_Incendie"
        Me.lbl_Incendie.Size = New System.Drawing.Size(733, 23)
        Me.lbl_Incendie.TabIndex = 98
        Me.lbl_Incendie.Text = "lbl_Incendie"
        Me.lbl_Incendie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Frm_OptionsCalculIncendie
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 541)
        Me.Controls.Add(Me.pan_Incendie)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsCalculIncendie"
        Me.Text = "Frm_OptionsCalculIncendie"
        Me.pan_Incendie.ResumeLayout(False)
        Me.TLpan_Conteneur.ResumeLayout(False)
        Me.pan_Conteneur.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.img_TempMax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Deltat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_T0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.img_Phi, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ksh, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_AlphaCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_UnitAlphaCC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_AlphaC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_UnitBoltzmann, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_EpsilonF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_UnitAlphaC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_EpsilonC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Sigma, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Incendie As Panel
    Friend WithEvents TLpan_Conteneur As TableLayoutPanel
    Friend WithEvents pan_Conteneur As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lbl_Parametres As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl_Constantes As Label
    Friend WithEvents lbl_Boltzman As Label
    Friend WithEvents txt_Sigma As TextBox
    Friend WithEvents img_Sigma As PictureBox
    Friend WithEvents lbl_Incendie As Label
    Friend WithEvents img_Deltat As PictureBox
    Friend WithEvents img_T0 As PictureBox
    Friend WithEvents img_UnitAlphaC As PictureBox
    Friend WithEvents lbl_AlphaC As Label
    Friend WithEvents txt_AlphaC As TextBox
    Friend WithEvents lbl_IncrementTemps As Label
    Friend WithEvents txt_IncrementTemps As TextBox
    Friend WithEvents etq_UnitIncrementTemps As Label
    Friend WithEvents lbl_TempReference As Label
    Friend WithEvents txt_TempReference As TextBox
    Friend WithEvents etq_UnitTemp1 As Label
    Friend WithEvents img_EpsilonF As PictureBox
    Friend WithEvents img_EpsilonC As PictureBox
    Friend WithEvents lbl_EmissiviteFeu As Label
    Friend WithEvents txt_EmissiviteFeu As TextBox
    Friend WithEvents lbl_EmissiviteBeton As Label
    Friend WithEvents txt_EmissiviteBeton As TextBox
    Friend WithEvents img_UnitBoltzmann As PictureBox
    Friend WithEvents img_AlphaC As PictureBox
    Friend WithEvents img_AlphaCC As PictureBox
    Friend WithEvents img_UnitAlphaCC As PictureBox
    Friend WithEvents txt_AlphaCC As TextBox
    Friend WithEvents lbl_AlphaCC As Label
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents img_Phi As PictureBox
    Friend WithEvents lbl_FormFactorPhi As Label
    Friend WithEvents txt_Phi As TextBox
    Friend WithEvents img_ksh As PictureBox
    Friend WithEvents lbl_ShadowKsh As Label
    Friend WithEvents txt_ksh As TextBox
    Friend WithEvents img_TempMax As PictureBox
    Friend WithEvents lbl_TempMax As Label
    Friend WithEvents txt_TempMax As TextBox
    Friend WithEvents etq_UnitTemp2 As Label
End Class
