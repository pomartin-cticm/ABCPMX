<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsCalculScope
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
        Me.pan_Scope = New System.Windows.Forms.Panel()
        Me.TLpan_Conteneur = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Conteneur = New System.Windows.Forms.Panel()
        Me.pan_Materiau = New System.Windows.Forms.Panel()
        Me.img_RhoC = New System.Windows.Forms.PictureBox()
        Me.txt_RhoC_LWC_Min = New System.Windows.Forms.TextBox()
        Me.etq_UnitMassV1 = New System.Windows.Forms.Label()
        Me.lbl_RhoBetonLeger = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txt_RhoC_LWC_Max = New System.Windows.Forms.TextBox()
        Me.lbl_Materiau = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbl_Dalle = New System.Windows.Forms.Label()
        Me.img_ThetaRd = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_ThetaH = New System.Windows.Forms.TextBox()
        Me.lbl_EpDalleMixte = New System.Windows.Forms.Label()
        Me.etq_UnitA1 = New System.Windows.Forms.Label()
        Me.etq_UnitD2 = New System.Windows.Forms.Label()
        Me.lbl_ThetaRd = New System.Windows.Forms.Label()
        Me.txt_EpDalleMixteMin = New System.Windows.Forms.TextBox()
        Me.img_Td1 = New System.Windows.Forms.PictureBox()
        Me.img_EpDalleMixte = New System.Windows.Forms.PictureBox()
        Me.txt_EpDalleMin = New System.Windows.Forms.TextBox()
        Me.img_xTd = New System.Windows.Forms.PictureBox()
        Me.etq_UnitD1 = New System.Windows.Forms.Label()
        Me.txt_RatioEpReformis = New System.Windows.Forms.TextBox()
        Me.lbl_EpDallePleine = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_Renformis = New System.Windows.Forms.Label()
        Me.img_Th = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_DefinitionPoutre = New System.Windows.Forms.Label()
        Me.img_PorteeMini = New System.Windows.Forms.PictureBox()
        Me.txt_PorteeMini = New System.Windows.Forms.TextBox()
        Me.etq_UnitL1 = New System.Windows.Forms.Label()
        Me.txt_PorteeMaxi = New System.Windows.Forms.TextBox()
        Me.etq_UnitL2 = New System.Windows.Forms.Label()
        Me.lbl_SpanL = New System.Windows.Forms.Label()
        Me.img_PorteeConsoleMin = New System.Windows.Forms.PictureBox()
        Me.txt_PorteeConsoleMin = New System.Windows.Forms.TextBox()
        Me.etq_UnitL3 = New System.Windows.Forms.Label()
        Me.lbl_PorteeConsole = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_RatioConsoleMax = New System.Windows.Forms.TextBox()
        Me.img_PorteeL2 = New System.Windows.Forms.PictureBox()
        Me.lbl_Scope = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.etq_UnitMassV2 = New System.Windows.Forms.Label()
        Me.pan_Scope.SuspendLayout()
        Me.TLpan_Conteneur.SuspendLayout()
        Me.pan_Conteneur.SuspendLayout()
        Me.pan_Materiau.SuspendLayout()
        CType(Me.img_RhoC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.img_ThetaRd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Td1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_EpDalleMixte, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_xTd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Th, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.img_PorteeMini, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_PorteeConsoleMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_PorteeL2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Scope
        '
        Me.pan_Scope.AutoScroll = True
        Me.pan_Scope.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Scope.Controls.Add(Me.TLpan_Conteneur)
        Me.pan_Scope.Location = New System.Drawing.Point(30, 29)
        Me.pan_Scope.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Scope.Name = "pan_Scope"
        Me.pan_Scope.Size = New System.Drawing.Size(739, 472)
        Me.pan_Scope.TabIndex = 1
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
        Me.pan_Conteneur.Controls.Add(Me.pan_Materiau)
        Me.pan_Conteneur.Controls.Add(Me.Panel2)
        Me.pan_Conteneur.Controls.Add(Me.Panel1)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Scope)
        Me.pan_Conteneur.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Conteneur.Location = New System.Drawing.Point(0, 0)
        Me.pan_Conteneur.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Conteneur.Name = "pan_Conteneur"
        Me.pan_Conteneur.Size = New System.Drawing.Size(739, 436)
        Me.pan_Conteneur.TabIndex = 0
        '
        'pan_Materiau
        '
        Me.pan_Materiau.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Materiau.Controls.Add(Me.etq_UnitMassV2)
        Me.pan_Materiau.Controls.Add(Me.img_RhoC)
        Me.pan_Materiau.Controls.Add(Me.txt_RhoC_LWC_Min)
        Me.pan_Materiau.Controls.Add(Me.etq_UnitMassV1)
        Me.pan_Materiau.Controls.Add(Me.lbl_RhoBetonLeger)
        Me.pan_Materiau.Controls.Add(Me.Label9)
        Me.pan_Materiau.Controls.Add(Me.Label10)
        Me.pan_Materiau.Controls.Add(Me.txt_RhoC_LWC_Max)
        Me.pan_Materiau.Controls.Add(Me.lbl_Materiau)
        Me.pan_Materiau.Location = New System.Drawing.Point(5, 280)
        Me.pan_Materiau.Name = "pan_Materiau"
        Me.pan_Materiau.Size = New System.Drawing.Size(731, 100)
        Me.pan_Materiau.TabIndex = 147
        '
        'img_RhoC
        '
        Me.img_RhoC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_RhoC.Location = New System.Drawing.Point(550, 30)
        Me.img_RhoC.Name = "img_RhoC"
        Me.img_RhoC.Size = New System.Drawing.Size(46, 20)
        Me.img_RhoC.TabIndex = 129
        Me.img_RhoC.TabStop = False
        '
        'txt_RhoC_LWC_Min
        '
        Me.txt_RhoC_LWC_Min.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_RhoC_LWC_Min.Location = New System.Drawing.Point(445, 30)
        Me.txt_RhoC_LWC_Min.Name = "txt_RhoC_LWC_Min"
        Me.txt_RhoC_LWC_Min.Size = New System.Drawing.Size(58, 20)
        Me.txt_RhoC_LWC_Min.TabIndex = 128
        Me.txt_RhoC_LWC_Min.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'etq_UnitMassV1
        '
        Me.etq_UnitMassV1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitMassV1.AutoSize = True
        Me.etq_UnitMassV1.Location = New System.Drawing.Point(509, 34)
        Me.etq_UnitMassV1.Name = "etq_UnitMassV1"
        Me.etq_UnitMassV1.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitMassV1.TabIndex = 130
        Me.etq_UnitMassV1.Text = "mm"
        '
        'lbl_RhoBetonLeger
        '
        Me.lbl_RhoBetonLeger.AutoSize = True
        Me.lbl_RhoBetonLeger.Location = New System.Drawing.Point(38, 34)
        Me.lbl_RhoBetonLeger.Name = "lbl_RhoBetonLeger"
        Me.lbl_RhoBetonLeger.Size = New System.Drawing.Size(98, 13)
        Me.lbl_RhoBetonLeger.TabIndex = 131
        Me.lbl_RhoBetonLeger.Text = "lbl_RhoBetonLeger"
        Me.lbl_RhoBetonLeger.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(532, 33)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(13, 13)
        Me.Label9.TabIndex = 132
        Me.Label9.Text = "≤"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(602, 34)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(13, 13)
        Me.Label10.TabIndex = 133
        Me.Label10.Text = "≤"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_RhoC_LWC_Max
        '
        Me.txt_RhoC_LWC_Max.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_RhoC_LWC_Max.Location = New System.Drawing.Point(618, 30)
        Me.txt_RhoC_LWC_Max.Name = "txt_RhoC_LWC_Max"
        Me.txt_RhoC_LWC_Max.Size = New System.Drawing.Size(58, 20)
        Me.txt_RhoC_LWC_Max.TabIndex = 134
        '
        'lbl_Materiau
        '
        Me.lbl_Materiau.AutoSize = True
        Me.lbl_Materiau.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Materiau.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Materiau.Location = New System.Drawing.Point(4, 6)
        Me.lbl_Materiau.Name = "lbl_Materiau"
        Me.lbl_Materiau.Size = New System.Drawing.Size(64, 13)
        Me.lbl_Materiau.TabIndex = 127
        Me.lbl_Materiau.Text = "lbl_Materiau"
        Me.lbl_Materiau.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.lbl_Dalle)
        Me.Panel2.Controls.Add(Me.img_ThetaRd)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.txt_ThetaH)
        Me.Panel2.Controls.Add(Me.lbl_EpDalleMixte)
        Me.Panel2.Controls.Add(Me.etq_UnitA1)
        Me.Panel2.Controls.Add(Me.etq_UnitD2)
        Me.Panel2.Controls.Add(Me.lbl_ThetaRd)
        Me.Panel2.Controls.Add(Me.txt_EpDalleMixteMin)
        Me.Panel2.Controls.Add(Me.img_Td1)
        Me.Panel2.Controls.Add(Me.img_EpDalleMixte)
        Me.Panel2.Controls.Add(Me.txt_EpDalleMin)
        Me.Panel2.Controls.Add(Me.img_xTd)
        Me.Panel2.Controls.Add(Me.etq_UnitD1)
        Me.Panel2.Controls.Add(Me.txt_RatioEpReformis)
        Me.Panel2.Controls.Add(Me.lbl_EpDallePleine)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.lbl_Renformis)
        Me.Panel2.Controls.Add(Me.img_Th)
        Me.Panel2.Location = New System.Drawing.Point(5, 141)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(731, 133)
        Me.Panel2.TabIndex = 146
        '
        'lbl_Dalle
        '
        Me.lbl_Dalle.AutoSize = True
        Me.lbl_Dalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Dalle.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Dalle.Location = New System.Drawing.Point(5, 6)
        Me.lbl_Dalle.Name = "lbl_Dalle"
        Me.lbl_Dalle.Size = New System.Drawing.Size(47, 13)
        Me.lbl_Dalle.TabIndex = 126
        Me.lbl_Dalle.Text = "lbl_Dalle"
        Me.lbl_Dalle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_ThetaRd
        '
        Me.img_ThetaRd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ThetaRd.Location = New System.Drawing.Point(573, 26)
        Me.img_ThetaRd.Name = "img_ThetaRd"
        Me.img_ThetaRd.Size = New System.Drawing.Size(46, 20)
        Me.img_ThetaRd.TabIndex = 105
        Me.img_ThetaRd.TabStop = False
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(533, 109)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(13, 13)
        Me.Label7.TabIndex = 144
        Me.Label7.Text = "≤"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_ThetaH
        '
        Me.txt_ThetaH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ThetaH.Location = New System.Drawing.Point(619, 26)
        Me.txt_ThetaH.Name = "txt_ThetaH"
        Me.txt_ThetaH.Size = New System.Drawing.Size(58, 20)
        Me.txt_ThetaH.TabIndex = 104
        '
        'lbl_EpDalleMixte
        '
        Me.lbl_EpDalleMixte.AutoSize = True
        Me.lbl_EpDalleMixte.Location = New System.Drawing.Point(39, 110)
        Me.lbl_EpDalleMixte.Name = "lbl_EpDalleMixte"
        Me.lbl_EpDalleMixte.Size = New System.Drawing.Size(85, 13)
        Me.lbl_EpDalleMixte.TabIndex = 143
        Me.lbl_EpDalleMixte.Text = "lbl_EpDalleMixte"
        Me.lbl_EpDalleMixte.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitA1
        '
        Me.etq_UnitA1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitA1.AutoSize = True
        Me.etq_UnitA1.Location = New System.Drawing.Point(683, 29)
        Me.etq_UnitA1.Name = "etq_UnitA1"
        Me.etq_UnitA1.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitA1.TabIndex = 106
        Me.etq_UnitA1.Text = "Label1"
        '
        'etq_UnitD2
        '
        Me.etq_UnitD2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitD2.AutoSize = True
        Me.etq_UnitD2.Location = New System.Drawing.Point(510, 110)
        Me.etq_UnitD2.Name = "etq_UnitD2"
        Me.etq_UnitD2.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitD2.TabIndex = 142
        Me.etq_UnitD2.Text = "mm"
        '
        'lbl_ThetaRd
        '
        Me.lbl_ThetaRd.AutoSize = True
        Me.lbl_ThetaRd.Location = New System.Drawing.Point(39, 33)
        Me.lbl_ThetaRd.Name = "lbl_ThetaRd"
        Me.lbl_ThetaRd.Size = New System.Drawing.Size(65, 13)
        Me.lbl_ThetaRd.TabIndex = 107
        Me.lbl_ThetaRd.Text = "lbl_ThetaRd"
        '
        'txt_EpDalleMixteMin
        '
        Me.txt_EpDalleMixteMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EpDalleMixteMin.Location = New System.Drawing.Point(446, 106)
        Me.txt_EpDalleMixteMin.Name = "txt_EpDalleMixteMin"
        Me.txt_EpDalleMixteMin.Size = New System.Drawing.Size(58, 20)
        Me.txt_EpDalleMixteMin.TabIndex = 140
        Me.txt_EpDalleMixteMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'img_Td1
        '
        Me.img_Td1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Td1.Location = New System.Drawing.Point(551, 54)
        Me.img_Td1.Name = "img_Td1"
        Me.img_Td1.Size = New System.Drawing.Size(46, 20)
        Me.img_Td1.TabIndex = 128
        Me.img_Td1.TabStop = False
        '
        'img_EpDalleMixte
        '
        Me.img_EpDalleMixte.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_EpDalleMixte.Location = New System.Drawing.Point(551, 106)
        Me.img_EpDalleMixte.Name = "img_EpDalleMixte"
        Me.img_EpDalleMixte.Size = New System.Drawing.Size(46, 20)
        Me.img_EpDalleMixte.TabIndex = 141
        Me.img_EpDalleMixte.TabStop = False
        '
        'txt_EpDalleMin
        '
        Me.txt_EpDalleMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EpDalleMin.Location = New System.Drawing.Point(446, 54)
        Me.txt_EpDalleMin.Name = "txt_EpDalleMin"
        Me.txt_EpDalleMin.Size = New System.Drawing.Size(58, 20)
        Me.txt_EpDalleMin.TabIndex = 127
        Me.txt_EpDalleMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'img_xTd
        '
        Me.img_xTd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_xTd.Location = New System.Drawing.Point(677, 80)
        Me.img_xTd.Name = "img_xTd"
        Me.img_xTd.Size = New System.Drawing.Size(34, 20)
        Me.img_xTd.TabIndex = 139
        Me.img_xTd.TabStop = False
        '
        'etq_UnitD1
        '
        Me.etq_UnitD1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitD1.AutoSize = True
        Me.etq_UnitD1.Location = New System.Drawing.Point(510, 58)
        Me.etq_UnitD1.Name = "etq_UnitD1"
        Me.etq_UnitD1.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitD1.TabIndex = 129
        Me.etq_UnitD1.Text = "mm"
        '
        'txt_RatioEpReformis
        '
        Me.txt_RatioEpReformis.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_RatioEpReformis.Location = New System.Drawing.Point(619, 80)
        Me.txt_RatioEpReformis.Name = "txt_RatioEpReformis"
        Me.txt_RatioEpReformis.Size = New System.Drawing.Size(58, 20)
        Me.txt_RatioEpReformis.TabIndex = 138
        Me.txt_RatioEpReformis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_EpDallePleine
        '
        Me.lbl_EpDallePleine.AutoSize = True
        Me.lbl_EpDallePleine.Location = New System.Drawing.Point(39, 58)
        Me.lbl_EpDallePleine.Name = "lbl_EpDallePleine"
        Me.lbl_EpDallePleine.Size = New System.Drawing.Size(89, 13)
        Me.lbl_EpDallePleine.TabIndex = 132
        Me.lbl_EpDallePleine.Text = "lbl_EpDallePleine"
        Me.lbl_EpDallePleine.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(603, 84)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(13, 13)
        Me.Label6.TabIndex = 137
        Me.Label6.Text = "≤"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(533, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(13, 13)
        Me.Label1.TabIndex = 134
        Me.Label1.Text = "≤"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Renformis
        '
        Me.lbl_Renformis.AutoSize = True
        Me.lbl_Renformis.Location = New System.Drawing.Point(39, 84)
        Me.lbl_Renformis.Name = "lbl_Renformis"
        Me.lbl_Renformis.Size = New System.Drawing.Size(70, 13)
        Me.lbl_Renformis.TabIndex = 136
        Me.lbl_Renformis.Text = "lbl_Renformis"
        Me.lbl_Renformis.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'img_Th
        '
        Me.img_Th.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Th.Location = New System.Drawing.Point(551, 80)
        Me.img_Th.Name = "img_Th"
        Me.img_Th.Size = New System.Drawing.Size(46, 20)
        Me.img_Th.TabIndex = 135
        Me.img_Th.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.lbl_DefinitionPoutre)
        Me.Panel1.Controls.Add(Me.img_PorteeMini)
        Me.Panel1.Controls.Add(Me.txt_PorteeMini)
        Me.Panel1.Controls.Add(Me.etq_UnitL1)
        Me.Panel1.Controls.Add(Me.txt_PorteeMaxi)
        Me.Panel1.Controls.Add(Me.etq_UnitL2)
        Me.Panel1.Controls.Add(Me.lbl_SpanL)
        Me.Panel1.Controls.Add(Me.img_PorteeConsoleMin)
        Me.Panel1.Controls.Add(Me.txt_PorteeConsoleMin)
        Me.Panel1.Controls.Add(Me.etq_UnitL3)
        Me.Panel1.Controls.Add(Me.lbl_PorteeConsole)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txt_RatioConsoleMax)
        Me.Panel1.Controls.Add(Me.img_PorteeL2)
        Me.Panel1.Location = New System.Drawing.Point(4, 52)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(732, 83)
        Me.Panel1.TabIndex = 145
        '
        'lbl_DefinitionPoutre
        '
        Me.lbl_DefinitionPoutre.AutoSize = True
        Me.lbl_DefinitionPoutre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DefinitionPoutre.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_DefinitionPoutre.Location = New System.Drawing.Point(5, 4)
        Me.lbl_DefinitionPoutre.Name = "lbl_DefinitionPoutre"
        Me.lbl_DefinitionPoutre.Size = New System.Drawing.Size(98, 13)
        Me.lbl_DefinitionPoutre.TabIndex = 103
        Me.lbl_DefinitionPoutre.Text = "lbl_DefinitionPoutre"
        Me.lbl_DefinitionPoutre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_PorteeMini
        '
        Me.img_PorteeMini.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PorteeMini.Location = New System.Drawing.Point(551, 23)
        Me.img_PorteeMini.Name = "img_PorteeMini"
        Me.img_PorteeMini.Size = New System.Drawing.Size(46, 20)
        Me.img_PorteeMini.TabIndex = 100
        Me.img_PorteeMini.TabStop = False
        '
        'txt_PorteeMini
        '
        Me.txt_PorteeMini.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PorteeMini.Location = New System.Drawing.Point(446, 23)
        Me.txt_PorteeMini.Name = "txt_PorteeMini"
        Me.txt_PorteeMini.Size = New System.Drawing.Size(58, 20)
        Me.txt_PorteeMini.TabIndex = 99
        Me.txt_PorteeMini.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'etq_UnitL1
        '
        Me.etq_UnitL1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL1.AutoSize = True
        Me.etq_UnitL1.Location = New System.Drawing.Point(510, 27)
        Me.etq_UnitL1.Name = "etq_UnitL1"
        Me.etq_UnitL1.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitL1.TabIndex = 101
        Me.etq_UnitL1.Text = "mm"
        '
        'txt_PorteeMaxi
        '
        Me.txt_PorteeMaxi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PorteeMaxi.Location = New System.Drawing.Point(619, 23)
        Me.txt_PorteeMaxi.Name = "txt_PorteeMaxi"
        Me.txt_PorteeMaxi.Size = New System.Drawing.Size(58, 20)
        Me.txt_PorteeMaxi.TabIndex = 108
        '
        'etq_UnitL2
        '
        Me.etq_UnitL2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL2.AutoSize = True
        Me.etq_UnitL2.Location = New System.Drawing.Point(683, 27)
        Me.etq_UnitL2.Name = "etq_UnitL2"
        Me.etq_UnitL2.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitL2.TabIndex = 110
        Me.etq_UnitL2.Text = "Label1"
        '
        'lbl_SpanL
        '
        Me.lbl_SpanL.AutoSize = True
        Me.lbl_SpanL.Location = New System.Drawing.Point(39, 27)
        Me.lbl_SpanL.Name = "lbl_SpanL"
        Me.lbl_SpanL.Size = New System.Drawing.Size(54, 13)
        Me.lbl_SpanL.TabIndex = 112
        Me.lbl_SpanL.Text = "lbl_SpanL"
        Me.lbl_SpanL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'img_PorteeConsoleMin
        '
        Me.img_PorteeConsoleMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PorteeConsoleMin.Location = New System.Drawing.Point(551, 50)
        Me.img_PorteeConsoleMin.Name = "img_PorteeConsoleMin"
        Me.img_PorteeConsoleMin.Size = New System.Drawing.Size(46, 20)
        Me.img_PorteeConsoleMin.TabIndex = 114
        Me.img_PorteeConsoleMin.TabStop = False
        '
        'txt_PorteeConsoleMin
        '
        Me.txt_PorteeConsoleMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PorteeConsoleMin.Location = New System.Drawing.Point(446, 50)
        Me.txt_PorteeConsoleMin.Name = "txt_PorteeConsoleMin"
        Me.txt_PorteeConsoleMin.Size = New System.Drawing.Size(58, 20)
        Me.txt_PorteeConsoleMin.TabIndex = 113
        Me.txt_PorteeConsoleMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'etq_UnitL3
        '
        Me.etq_UnitL3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL3.AutoSize = True
        Me.etq_UnitL3.Location = New System.Drawing.Point(510, 54)
        Me.etq_UnitL3.Name = "etq_UnitL3"
        Me.etq_UnitL3.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitL3.TabIndex = 115
        Me.etq_UnitL3.Text = "mm"
        '
        'lbl_PorteeConsole
        '
        Me.lbl_PorteeConsole.AutoSize = True
        Me.lbl_PorteeConsole.Location = New System.Drawing.Point(39, 54)
        Me.lbl_PorteeConsole.Name = "lbl_PorteeConsole"
        Me.lbl_PorteeConsole.Size = New System.Drawing.Size(92, 13)
        Me.lbl_PorteeConsole.TabIndex = 117
        Me.lbl_PorteeConsole.Text = "lbl_PorteeConsole"
        Me.lbl_PorteeConsole.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(603, 26)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(13, 13)
        Me.Label18.TabIndex = 118
        Me.Label18.Text = "≤"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(533, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(13, 13)
        Me.Label3.TabIndex = 121
        Me.Label3.Text = "≤"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(533, 53)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(13, 13)
        Me.Label4.TabIndex = 122
        Me.Label4.Text = "≤"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(603, 54)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(13, 13)
        Me.Label5.TabIndex = 123
        Me.Label5.Text = "≤"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_RatioConsoleMax
        '
        Me.txt_RatioConsoleMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_RatioConsoleMax.Location = New System.Drawing.Point(619, 50)
        Me.txt_RatioConsoleMax.Name = "txt_RatioConsoleMax"
        Me.txt_RatioConsoleMax.Size = New System.Drawing.Size(58, 20)
        Me.txt_RatioConsoleMax.TabIndex = 124
        '
        'img_PorteeL2
        '
        Me.img_PorteeL2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PorteeL2.Location = New System.Drawing.Point(677, 50)
        Me.img_PorteeL2.Name = "img_PorteeL2"
        Me.img_PorteeL2.Size = New System.Drawing.Size(34, 20)
        Me.img_PorteeL2.TabIndex = 125
        Me.img_PorteeL2.TabStop = False
        '
        'lbl_Scope
        '
        Me.lbl_Scope.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Scope.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Scope.Location = New System.Drawing.Point(3, 2)
        Me.lbl_Scope.Name = "lbl_Scope"
        Me.lbl_Scope.Size = New System.Drawing.Size(733, 23)
        Me.lbl_Scope.TabIndex = 98
        Me.lbl_Scope.Text = "lbl_Scope"
        Me.lbl_Scope.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'etq_UnitMassV2
        '
        Me.etq_UnitMassV2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitMassV2.AutoSize = True
        Me.etq_UnitMassV2.Location = New System.Drawing.Point(682, 34)
        Me.etq_UnitMassV2.Name = "etq_UnitMassV2"
        Me.etq_UnitMassV2.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitMassV2.TabIndex = 135
        Me.etq_UnitMassV2.Text = "mm"
        '
        'Frm_OptionsCalculScope
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(853, 546)
        Me.Controls.Add(Me.pan_Scope)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsCalculScope"
        Me.Text = "Frm_OptionsCalculScope"
        Me.pan_Scope.ResumeLayout(False)
        Me.TLpan_Conteneur.ResumeLayout(False)
        Me.pan_Conteneur.ResumeLayout(False)
        Me.pan_Materiau.ResumeLayout(False)
        Me.pan_Materiau.PerformLayout()
        CType(Me.img_RhoC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.img_ThetaRd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Td1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_EpDalleMixte, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_xTd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Th, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.img_PorteeMini, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_PorteeConsoleMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_PorteeL2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Scope As Panel
    Friend WithEvents TLpan_Conteneur As TableLayoutPanel
    Friend WithEvents pan_Conteneur As Panel
    Friend WithEvents lbl_Scope As Label
    Friend WithEvents etq_UnitL1 As Label
    Friend WithEvents txt_PorteeMini As TextBox
    Friend WithEvents img_PorteeMini As PictureBox
    Friend WithEvents lbl_DefinitionPoutre As Label
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents lbl_ThetaRd As Label
    Friend WithEvents etq_UnitA1 As Label
    Friend WithEvents txt_ThetaH As TextBox
    Friend WithEvents img_ThetaRd As PictureBox
    Friend WithEvents etq_UnitL2 As Label
    Friend WithEvents txt_PorteeMaxi As TextBox
    Friend WithEvents lbl_SpanL As Label
    Friend WithEvents lbl_PorteeConsole As Label
    Friend WithEvents etq_UnitL3 As Label
    Friend WithEvents txt_PorteeConsoleMin As TextBox
    Friend WithEvents img_PorteeConsoleMin As PictureBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lbl_Dalle As Label
    Friend WithEvents img_PorteeL2 As PictureBox
    Friend WithEvents txt_RatioConsoleMax As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lbl_EpDallePleine As Label
    Friend WithEvents etq_UnitD1 As Label
    Friend WithEvents txt_EpDalleMin As TextBox
    Friend WithEvents img_Td1 As PictureBox
    Friend WithEvents img_xTd As PictureBox
    Friend WithEvents txt_RatioEpReformis As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents lbl_Renformis As Label
    Friend WithEvents img_Th As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents lbl_EpDalleMixte As Label
    Friend WithEvents etq_UnitD2 As Label
    Friend WithEvents txt_EpDalleMixteMin As TextBox
    Friend WithEvents img_EpDalleMixte As PictureBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents pan_Materiau As Panel
    Friend WithEvents img_RhoC As PictureBox
    Friend WithEvents txt_RhoC_LWC_Min As TextBox
    Friend WithEvents etq_UnitMassV1 As Label
    Friend WithEvents lbl_RhoBetonLeger As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txt_RhoC_LWC_Max As TextBox
    Friend WithEvents lbl_Materiau As Label
    Friend WithEvents etq_UnitMassV2 As Label
End Class
