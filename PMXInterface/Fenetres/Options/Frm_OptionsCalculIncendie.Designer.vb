<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsCalculIncendie
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
        Me.pan_Incendie = New System.Windows.Forms.Panel()
        Me.TLpan_Conteneur = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Conteneur = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.img_UnitBoltzmann = New System.Windows.Forms.PictureBox()
        Me.img_Deltat = New System.Windows.Forms.PictureBox()
        Me.img_T0 = New System.Windows.Forms.PictureBox()
        Me.img_UnitThermConvection = New System.Windows.Forms.PictureBox()
        Me.lbl_ThermConvection = New System.Windows.Forms.Label()
        Me.txt_ThermConvection = New System.Windows.Forms.TextBox()
        Me.lbl_IncrementTemps = New System.Windows.Forms.Label()
        Me.txt_IncrementTemps = New System.Windows.Forms.TextBox()
        Me.etq_UnitIncrementTemps = New System.Windows.Forms.Label()
        Me.lbl_TempReference = New System.Windows.Forms.Label()
        Me.txt_TempReference = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.etq_UnitTempReference = New System.Windows.Forms.Label()
        Me.etq_UnitL1 = New System.Windows.Forms.Label()
        Me.txt_EspNoeuds = New System.Windows.Forms.TextBox()
        Me.img_dNodes = New System.Windows.Forms.PictureBox()
        Me.lbl_DistanceMaxNoeuds = New System.Windows.Forms.Label()
        Me.lbl_Parametres = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.img_EpsilonF = New System.Windows.Forms.PictureBox()
        Me.img_EpsilonA = New System.Windows.Forms.PictureBox()
        Me.lbl_EmissiviteFeu = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_EmissiviteFeu = New System.Windows.Forms.TextBox()
        Me.lbl_Constantes = New System.Windows.Forms.Label()
        Me.lbl_EmissiviteAcier = New System.Windows.Forms.Label()
        Me.txt_EmissiviteAcier = New System.Windows.Forms.TextBox()
        Me.etq_UnitBoltzman = New System.Windows.Forms.Label()
        Me.lbl_Boltzman = New System.Windows.Forms.Label()
        Me.txt_Sigma = New System.Windows.Forms.TextBox()
        Me.img_Sigma = New System.Windows.Forms.PictureBox()
        Me.lbl_Incendie = New System.Windows.Forms.Label()
        Me.pan_Incendie.SuspendLayout()
        Me.TLpan_Conteneur.SuspendLayout()
        Me.pan_Conteneur.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.img_UnitBoltzmann, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Deltat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_T0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_UnitThermConvection, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_dNodes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.img_EpsilonF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_EpsilonA, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Sigma, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pan_Incendie.Size = New System.Drawing.Size(739, 472)
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
        Me.TLpan_Conteneur.Size = New System.Drawing.Size(739, 436)
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
        Me.pan_Conteneur.Size = New System.Drawing.Size(739, 436)
        Me.pan_Conteneur.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.img_UnitBoltzmann)
        Me.Panel2.Controls.Add(Me.img_Deltat)
        Me.Panel2.Controls.Add(Me.img_T0)
        Me.Panel2.Controls.Add(Me.img_UnitThermConvection)
        Me.Panel2.Controls.Add(Me.lbl_ThermConvection)
        Me.Panel2.Controls.Add(Me.txt_ThermConvection)
        Me.Panel2.Controls.Add(Me.lbl_IncrementTemps)
        Me.Panel2.Controls.Add(Me.txt_IncrementTemps)
        Me.Panel2.Controls.Add(Me.etq_UnitIncrementTemps)
        Me.Panel2.Controls.Add(Me.lbl_TempReference)
        Me.Panel2.Controls.Add(Me.txt_TempReference)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.etq_UnitTempReference)
        Me.Panel2.Controls.Add(Me.etq_UnitL1)
        Me.Panel2.Controls.Add(Me.txt_EspNoeuds)
        Me.Panel2.Controls.Add(Me.img_dNodes)
        Me.Panel2.Controls.Add(Me.lbl_DistanceMaxNoeuds)
        Me.Panel2.Controls.Add(Me.lbl_Parametres)
        Me.Panel2.Location = New System.Drawing.Point(3, 193)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(732, 210)
        Me.Panel2.TabIndex = 106
        '
        'img_UnitBoltzmann
        '
        Me.img_UnitBoltzmann.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_UnitBoltzmann.Location = New System.Drawing.Point(321, 148)
        Me.img_UnitBoltzmann.Name = "img_UnitBoltzmann"
        Me.img_UnitBoltzmann.Size = New System.Drawing.Size(95, 20)
        Me.img_UnitBoltzmann.TabIndex = 138
        Me.img_UnitBoltzmann.TabStop = False
        '
        'img_Deltat
        '
        Me.img_Deltat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Deltat.Location = New System.Drawing.Point(564, 80)
        Me.img_Deltat.Name = "img_Deltat"
        Me.img_Deltat.Size = New System.Drawing.Size(46, 20)
        Me.img_Deltat.TabIndex = 137
        Me.img_Deltat.TabStop = False
        '
        'img_T0
        '
        Me.img_T0.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_T0.Location = New System.Drawing.Point(564, 54)
        Me.img_T0.Name = "img_T0"
        Me.img_T0.Size = New System.Drawing.Size(46, 20)
        Me.img_T0.TabIndex = 136
        Me.img_T0.TabStop = False
        '
        'img_UnitThermConvection
        '
        Me.img_UnitThermConvection.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_UnitThermConvection.Location = New System.Drawing.Point(671, 106)
        Me.img_UnitThermConvection.Name = "img_UnitThermConvection"
        Me.img_UnitThermConvection.Size = New System.Drawing.Size(61, 20)
        Me.img_UnitThermConvection.TabIndex = 129
        Me.img_UnitThermConvection.TabStop = False
        '
        'lbl_ThermConvection
        '
        Me.lbl_ThermConvection.AutoSize = True
        Me.lbl_ThermConvection.Location = New System.Drawing.Point(39, 106)
        Me.lbl_ThermConvection.Name = "lbl_ThermConvection"
        Me.lbl_ThermConvection.Size = New System.Drawing.Size(107, 13)
        Me.lbl_ThermConvection.TabIndex = 128
        Me.lbl_ThermConvection.Text = "lbl_ThermConvection"
        Me.lbl_ThermConvection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_ThermConvection
        '
        Me.txt_ThermConvection.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ThermConvection.Location = New System.Drawing.Point(610, 106)
        Me.txt_ThermConvection.Name = "txt_ThermConvection"
        Me.txt_ThermConvection.Size = New System.Drawing.Size(55, 20)
        Me.txt_ThermConvection.TabIndex = 121
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
        '
        'txt_IncrementTemps
        '
        Me.txt_IncrementTemps.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_IncrementTemps.Location = New System.Drawing.Point(610, 80)
        Me.txt_IncrementTemps.Name = "txt_IncrementTemps"
        Me.txt_IncrementTemps.Size = New System.Drawing.Size(55, 20)
        Me.txt_IncrementTemps.TabIndex = 120
        '
        'etq_UnitIncrementTemps
        '
        Me.etq_UnitIncrementTemps.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitIncrementTemps.AutoSize = True
        Me.etq_UnitIncrementTemps.Location = New System.Drawing.Point(671, 83)
        Me.etq_UnitIncrementTemps.Name = "etq_UnitIncrementTemps"
        Me.etq_UnitIncrementTemps.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitIncrementTemps.TabIndex = 126
        Me.etq_UnitIncrementTemps.Text = "Label1"
        Me.etq_UnitIncrementTemps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_TempReference
        '
        Me.lbl_TempReference.AutoSize = True
        Me.lbl_TempReference.Location = New System.Drawing.Point(39, 54)
        Me.lbl_TempReference.Name = "lbl_TempReference"
        Me.lbl_TempReference.Size = New System.Drawing.Size(100, 13)
        Me.lbl_TempReference.TabIndex = 125
        Me.lbl_TempReference.Text = "lbl_TempReference"
        Me.lbl_TempReference.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_TempReference
        '
        Me.txt_TempReference.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TempReference.Location = New System.Drawing.Point(610, 54)
        Me.txt_TempReference.Name = "txt_TempReference"
        Me.txt_TempReference.Size = New System.Drawing.Size(55, 20)
        Me.txt_TempReference.TabIndex = 119
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(597, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(13, 13)
        Me.Label5.TabIndex = 124
        Me.Label5.Text = "≤"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'etq_UnitTempReference
        '
        Me.etq_UnitTempReference.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitTempReference.AutoSize = True
        Me.etq_UnitTempReference.Location = New System.Drawing.Point(671, 57)
        Me.etq_UnitTempReference.Name = "etq_UnitTempReference"
        Me.etq_UnitTempReference.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitTempReference.TabIndex = 124
        Me.etq_UnitTempReference.Text = "Label1"
        Me.etq_UnitTempReference.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_UnitL1
        '
        Me.etq_UnitL1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL1.AutoSize = True
        Me.etq_UnitL1.Location = New System.Drawing.Point(674, 30)
        Me.etq_UnitL1.Name = "etq_UnitL1"
        Me.etq_UnitL1.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitL1.TabIndex = 109
        Me.etq_UnitL1.Text = "Label1"
        '
        'txt_EspNoeuds
        '
        Me.txt_EspNoeuds.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EspNoeuds.Location = New System.Drawing.Point(610, 27)
        Me.txt_EspNoeuds.Name = "txt_EspNoeuds"
        Me.txt_EspNoeuds.Size = New System.Drawing.Size(55, 20)
        Me.txt_EspNoeuds.TabIndex = 107
        '
        'img_dNodes
        '
        Me.img_dNodes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_dNodes.Location = New System.Drawing.Point(551, 27)
        Me.img_dNodes.Name = "img_dNodes"
        Me.img_dNodes.Size = New System.Drawing.Size(46, 20)
        Me.img_dNodes.TabIndex = 108
        Me.img_dNodes.TabStop = False
        '
        'lbl_DistanceMaxNoeuds
        '
        Me.lbl_DistanceMaxNoeuds.AutoSize = True
        Me.lbl_DistanceMaxNoeuds.Location = New System.Drawing.Point(39, 30)
        Me.lbl_DistanceMaxNoeuds.Name = "lbl_DistanceMaxNoeuds"
        Me.lbl_DistanceMaxNoeuds.Size = New System.Drawing.Size(122, 13)
        Me.lbl_DistanceMaxNoeuds.TabIndex = 106
        Me.lbl_DistanceMaxNoeuds.Text = "lbl_DistanceMaxNoeuds"
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
        Me.Panel1.Controls.Add(Me.img_EpsilonF)
        Me.Panel1.Controls.Add(Me.img_EpsilonA)
        Me.Panel1.Controls.Add(Me.lbl_EmissiviteFeu)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txt_EmissiviteFeu)
        Me.Panel1.Controls.Add(Me.lbl_Constantes)
        Me.Panel1.Controls.Add(Me.lbl_EmissiviteAcier)
        Me.Panel1.Controls.Add(Me.txt_EmissiviteAcier)
        Me.Panel1.Controls.Add(Me.etq_UnitBoltzman)
        Me.Panel1.Controls.Add(Me.lbl_Boltzman)
        Me.Panel1.Controls.Add(Me.txt_Sigma)
        Me.Panel1.Controls.Add(Me.img_Sigma)
        Me.Panel1.Location = New System.Drawing.Point(3, 31)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(732, 158)
        Me.Panel1.TabIndex = 105
        '
        'img_EpsilonF
        '
        Me.img_EpsilonF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_EpsilonF.Location = New System.Drawing.Point(564, 75)
        Me.img_EpsilonF.Name = "img_EpsilonF"
        Me.img_EpsilonF.Size = New System.Drawing.Size(46, 20)
        Me.img_EpsilonF.TabIndex = 135
        Me.img_EpsilonF.TabStop = False
        '
        'img_EpsilonA
        '
        Me.img_EpsilonA.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_EpsilonA.Location = New System.Drawing.Point(564, 49)
        Me.img_EpsilonA.Name = "img_EpsilonA"
        Me.img_EpsilonA.Size = New System.Drawing.Size(46, 20)
        Me.img_EpsilonA.TabIndex = 134
        Me.img_EpsilonA.TabStop = False
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
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(268, 118)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(13, 13)
        Me.Label1.TabIndex = 133
        Me.Label1.Text = "≥"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_EmissiviteFeu
        '
        Me.txt_EmissiviteFeu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EmissiviteFeu.Location = New System.Drawing.Point(610, 75)
        Me.txt_EmissiviteFeu.Name = "txt_EmissiviteFeu"
        Me.txt_EmissiviteFeu.Size = New System.Drawing.Size(55, 20)
        Me.txt_EmissiviteFeu.TabIndex = 123
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
        'lbl_EmissiviteAcier
        '
        Me.lbl_EmissiviteAcier.AutoSize = True
        Me.lbl_EmissiviteAcier.Location = New System.Drawing.Point(39, 52)
        Me.lbl_EmissiviteAcier.Name = "lbl_EmissiviteAcier"
        Me.lbl_EmissiviteAcier.Size = New System.Drawing.Size(93, 13)
        Me.lbl_EmissiviteAcier.TabIndex = 130
        Me.lbl_EmissiviteAcier.Text = "lbl_EmissiviteAcier"
        Me.lbl_EmissiviteAcier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_EmissiviteAcier
        '
        Me.txt_EmissiviteAcier.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EmissiviteAcier.Location = New System.Drawing.Point(610, 49)
        Me.txt_EmissiviteAcier.Name = "txt_EmissiviteAcier"
        Me.txt_EmissiviteAcier.Size = New System.Drawing.Size(55, 20)
        Me.txt_EmissiviteAcier.TabIndex = 122
        '
        'etq_UnitBoltzman
        '
        Me.etq_UnitBoltzman.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitBoltzman.AutoSize = True
        Me.etq_UnitBoltzman.Location = New System.Drawing.Point(674, 26)
        Me.etq_UnitBoltzman.Name = "etq_UnitBoltzman"
        Me.etq_UnitBoltzman.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitBoltzman.TabIndex = 101
        Me.etq_UnitBoltzman.Text = "Label1"
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
        Me.txt_Sigma.Location = New System.Drawing.Point(610, 23)
        Me.txt_Sigma.Name = "txt_Sigma"
        Me.txt_Sigma.Size = New System.Drawing.Size(55, 20)
        Me.txt_Sigma.TabIndex = 99
        '
        'img_Sigma
        '
        Me.img_Sigma.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Sigma.Location = New System.Drawing.Point(564, 23)
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
        CType(Me.img_UnitBoltzmann, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Deltat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_T0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_UnitThermConvection, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_dNodes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.img_EpsilonF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_EpsilonA, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Sigma, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Incendie As Panel
    Friend WithEvents TLpan_Conteneur As TableLayoutPanel
    Friend WithEvents pan_Conteneur As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents etq_UnitL1 As Label
    Friend WithEvents txt_EspNoeuds As TextBox
    Friend WithEvents img_dNodes As PictureBox
    Friend WithEvents lbl_DistanceMaxNoeuds As Label
    Friend WithEvents lbl_Parametres As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl_Constantes As Label
    Friend WithEvents etq_UnitBoltzman As Label
    Friend WithEvents lbl_Boltzman As Label
    Friend WithEvents txt_Sigma As TextBox
    Friend WithEvents img_Sigma As PictureBox
    Friend WithEvents lbl_Incendie As Label
    Friend WithEvents img_Deltat As PictureBox
    Friend WithEvents img_T0 As PictureBox
    Friend WithEvents img_UnitThermConvection As PictureBox
    Friend WithEvents lbl_ThermConvection As Label
    Friend WithEvents txt_ThermConvection As TextBox
    Friend WithEvents lbl_IncrementTemps As Label
    Friend WithEvents txt_IncrementTemps As TextBox
    Friend WithEvents etq_UnitIncrementTemps As Label
    Friend WithEvents lbl_TempReference As Label
    Friend WithEvents txt_TempReference As TextBox
    Friend WithEvents etq_UnitTempReference As Label
    Friend WithEvents img_EpsilonF As PictureBox
    Friend WithEvents img_EpsilonA As PictureBox
    Friend WithEvents lbl_EmissiviteFeu As Label
    Friend WithEvents txt_EmissiviteFeu As TextBox
    Friend WithEvents lbl_EmissiviteAcier As Label
    Friend WithEvents txt_EmissiviteAcier As TextBox
    Friend WithEvents img_UnitBoltzmann As PictureBox
End Class
