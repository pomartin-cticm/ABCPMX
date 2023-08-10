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
        Me.lbl_Dalle = New System.Windows.Forms.Label()
        Me.img_PorteeL2 = New System.Windows.Forms.PictureBox()
        Me.txt_RatioConsoleMax = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lbl_PorteeConsole = New System.Windows.Forms.Label()
        Me.etq_UnitL3 = New System.Windows.Forms.Label()
        Me.txt_PorteeConsoleMin = New System.Windows.Forms.TextBox()
        Me.img_PorteeConsoleMin = New System.Windows.Forms.PictureBox()
        Me.lbl_SpanL = New System.Windows.Forms.Label()
        Me.etq_UnitL2 = New System.Windows.Forms.Label()
        Me.txt_PorteeMaxi = New System.Windows.Forms.TextBox()
        Me.lbl_ThetaRd = New System.Windows.Forms.Label()
        Me.etq_UnitA1 = New System.Windows.Forms.Label()
        Me.txt_ThetaH = New System.Windows.Forms.TextBox()
        Me.img_ThetaRd = New System.Windows.Forms.PictureBox()
        Me.lbl_DefinitionPoutre = New System.Windows.Forms.Label()
        Me.etq_UnitL1 = New System.Windows.Forms.Label()
        Me.txt_PorteeMini = New System.Windows.Forms.TextBox()
        Me.img_PorteeMini = New System.Windows.Forms.PictureBox()
        Me.lbl_Scope = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_EpDallePleine = New System.Windows.Forms.Label()
        Me.etq_UnitD1 = New System.Windows.Forms.Label()
        Me.txt_EpDalleMin = New System.Windows.Forms.TextBox()
        Me.img_Td1 = New System.Windows.Forms.PictureBox()
        Me.pan_Scope.SuspendLayout()
        Me.TLpan_Conteneur.SuspendLayout()
        Me.pan_Conteneur.SuspendLayout()
        CType(Me.img_PorteeL2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_PorteeConsoleMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ThetaRd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_PorteeMini, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Td1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pan_Conteneur.Controls.Add(Me.Label1)
        Me.pan_Conteneur.Controls.Add(Me.lbl_EpDallePleine)
        Me.pan_Conteneur.Controls.Add(Me.etq_UnitD1)
        Me.pan_Conteneur.Controls.Add(Me.txt_EpDalleMin)
        Me.pan_Conteneur.Controls.Add(Me.img_Td1)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Dalle)
        Me.pan_Conteneur.Controls.Add(Me.img_PorteeL2)
        Me.pan_Conteneur.Controls.Add(Me.txt_RatioConsoleMax)
        Me.pan_Conteneur.Controls.Add(Me.Label5)
        Me.pan_Conteneur.Controls.Add(Me.Label4)
        Me.pan_Conteneur.Controls.Add(Me.Label3)
        Me.pan_Conteneur.Controls.Add(Me.Label2)
        Me.pan_Conteneur.Controls.Add(Me.Label18)
        Me.pan_Conteneur.Controls.Add(Me.lbl_PorteeConsole)
        Me.pan_Conteneur.Controls.Add(Me.etq_UnitL3)
        Me.pan_Conteneur.Controls.Add(Me.txt_PorteeConsoleMin)
        Me.pan_Conteneur.Controls.Add(Me.img_PorteeConsoleMin)
        Me.pan_Conteneur.Controls.Add(Me.lbl_SpanL)
        Me.pan_Conteneur.Controls.Add(Me.etq_UnitL2)
        Me.pan_Conteneur.Controls.Add(Me.txt_PorteeMaxi)
        Me.pan_Conteneur.Controls.Add(Me.lbl_ThetaRd)
        Me.pan_Conteneur.Controls.Add(Me.etq_UnitA1)
        Me.pan_Conteneur.Controls.Add(Me.txt_ThetaH)
        Me.pan_Conteneur.Controls.Add(Me.img_ThetaRd)
        Me.pan_Conteneur.Controls.Add(Me.lbl_DefinitionPoutre)
        Me.pan_Conteneur.Controls.Add(Me.etq_UnitL1)
        Me.pan_Conteneur.Controls.Add(Me.txt_PorteeMini)
        Me.pan_Conteneur.Controls.Add(Me.img_PorteeMini)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Scope)
        Me.pan_Conteneur.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Conteneur.Location = New System.Drawing.Point(0, 0)
        Me.pan_Conteneur.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Conteneur.Name = "pan_Conteneur"
        Me.pan_Conteneur.Size = New System.Drawing.Size(739, 436)
        Me.pan_Conteneur.TabIndex = 0
        '
        'lbl_Dalle
        '
        Me.lbl_Dalle.AutoSize = True
        Me.lbl_Dalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Dalle.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Dalle.Location = New System.Drawing.Point(8, 178)
        Me.lbl_Dalle.Name = "lbl_Dalle"
        Me.lbl_Dalle.Size = New System.Drawing.Size(47, 13)
        Me.lbl_Dalle.TabIndex = 126
        Me.lbl_Dalle.Text = "lbl_Dalle"
        Me.lbl_Dalle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_PorteeL2
        '
        Me.img_PorteeL2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PorteeL2.Location = New System.Drawing.Point(646, 82)
        Me.img_PorteeL2.Name = "img_PorteeL2"
        Me.img_PorteeL2.Size = New System.Drawing.Size(34, 20)
        Me.img_PorteeL2.TabIndex = 125
        Me.img_PorteeL2.TabStop = False
        '
        'txt_RatioConsoleMax
        '
        Me.txt_RatioConsoleMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_RatioConsoleMax.Location = New System.Drawing.Point(588, 82)
        Me.txt_RatioConsoleMax.Name = "txt_RatioConsoleMax"
        Me.txt_RatioConsoleMax.Size = New System.Drawing.Size(58, 20)
        Me.txt_RatioConsoleMax.TabIndex = 124
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(572, 86)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(13, 13)
        Me.Label5.TabIndex = 123
        Me.Label5.Text = "≤"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(502, 85)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(13, 13)
        Me.Label4.TabIndex = 122
        Me.Label4.Text = "≤"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(502, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(13, 13)
        Me.Label3.TabIndex = 121
        Me.Label3.Text = "≤"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(703, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(13, 13)
        Me.Label2.TabIndex = 119
        Me.Label2.Text = "≥"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(572, 58)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(13, 13)
        Me.Label18.TabIndex = 118
        Me.Label18.Text = "≤"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_PorteeConsole
        '
        Me.lbl_PorteeConsole.AutoSize = True
        Me.lbl_PorteeConsole.Location = New System.Drawing.Point(42, 86)
        Me.lbl_PorteeConsole.Name = "lbl_PorteeConsole"
        Me.lbl_PorteeConsole.Size = New System.Drawing.Size(92, 13)
        Me.lbl_PorteeConsole.TabIndex = 117
        Me.lbl_PorteeConsole.Text = "lbl_PorteeConsole"
        Me.lbl_PorteeConsole.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitL3
        '
        Me.etq_UnitL3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL3.AutoSize = True
        Me.etq_UnitL3.Location = New System.Drawing.Point(479, 86)
        Me.etq_UnitL3.Name = "etq_UnitL3"
        Me.etq_UnitL3.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitL3.TabIndex = 115
        Me.etq_UnitL3.Text = "mm"
        '
        'txt_PorteeConsoleMin
        '
        Me.txt_PorteeConsoleMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PorteeConsoleMin.Location = New System.Drawing.Point(415, 82)
        Me.txt_PorteeConsoleMin.Name = "txt_PorteeConsoleMin"
        Me.txt_PorteeConsoleMin.Size = New System.Drawing.Size(58, 20)
        Me.txt_PorteeConsoleMin.TabIndex = 113
        '
        'img_PorteeConsoleMin
        '
        Me.img_PorteeConsoleMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PorteeConsoleMin.Location = New System.Drawing.Point(520, 82)
        Me.img_PorteeConsoleMin.Name = "img_PorteeConsoleMin"
        Me.img_PorteeConsoleMin.Size = New System.Drawing.Size(46, 20)
        Me.img_PorteeConsoleMin.TabIndex = 114
        Me.img_PorteeConsoleMin.TabStop = False
        '
        'lbl_SpanL
        '
        Me.lbl_SpanL.AutoSize = True
        Me.lbl_SpanL.Location = New System.Drawing.Point(42, 59)
        Me.lbl_SpanL.Name = "lbl_SpanL"
        Me.lbl_SpanL.Size = New System.Drawing.Size(54, 13)
        Me.lbl_SpanL.TabIndex = 112
        Me.lbl_SpanL.Text = "lbl_SpanL"
        Me.lbl_SpanL.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitL2
        '
        Me.etq_UnitL2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL2.AutoSize = True
        Me.etq_UnitL2.Location = New System.Drawing.Point(652, 59)
        Me.etq_UnitL2.Name = "etq_UnitL2"
        Me.etq_UnitL2.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitL2.TabIndex = 110
        Me.etq_UnitL2.Text = "Label1"
        '
        'txt_PorteeMaxi
        '
        Me.txt_PorteeMaxi.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PorteeMaxi.Location = New System.Drawing.Point(588, 55)
        Me.txt_PorteeMaxi.Name = "txt_PorteeMaxi"
        Me.txt_PorteeMaxi.Size = New System.Drawing.Size(58, 20)
        Me.txt_PorteeMaxi.TabIndex = 108
        '
        'lbl_ThetaRd
        '
        Me.lbl_ThetaRd.AutoSize = True
        Me.lbl_ThetaRd.Location = New System.Drawing.Point(42, 205)
        Me.lbl_ThetaRd.Name = "lbl_ThetaRd"
        Me.lbl_ThetaRd.Size = New System.Drawing.Size(65, 13)
        Me.lbl_ThetaRd.TabIndex = 107
        Me.lbl_ThetaRd.Text = "lbl_ThetaRd"
        '
        'etq_UnitA1
        '
        Me.etq_UnitA1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitA1.AutoSize = True
        Me.etq_UnitA1.Location = New System.Drawing.Point(652, 201)
        Me.etq_UnitA1.Name = "etq_UnitA1"
        Me.etq_UnitA1.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitA1.TabIndex = 106
        Me.etq_UnitA1.Text = "Label1"
        '
        'txt_ThetaH
        '
        Me.txt_ThetaH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ThetaH.Location = New System.Drawing.Point(588, 198)
        Me.txt_ThetaH.Name = "txt_ThetaH"
        Me.txt_ThetaH.Size = New System.Drawing.Size(58, 20)
        Me.txt_ThetaH.TabIndex = 104
        '
        'img_ThetaRd
        '
        Me.img_ThetaRd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ThetaRd.Location = New System.Drawing.Point(542, 198)
        Me.img_ThetaRd.Name = "img_ThetaRd"
        Me.img_ThetaRd.Size = New System.Drawing.Size(46, 20)
        Me.img_ThetaRd.TabIndex = 105
        Me.img_ThetaRd.TabStop = False
        '
        'lbl_DefinitionPoutre
        '
        Me.lbl_DefinitionPoutre.AutoSize = True
        Me.lbl_DefinitionPoutre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DefinitionPoutre.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_DefinitionPoutre.Location = New System.Drawing.Point(8, 36)
        Me.lbl_DefinitionPoutre.Name = "lbl_DefinitionPoutre"
        Me.lbl_DefinitionPoutre.Size = New System.Drawing.Size(98, 13)
        Me.lbl_DefinitionPoutre.TabIndex = 103
        Me.lbl_DefinitionPoutre.Text = "lbl_DefinitionPoutre"
        Me.lbl_DefinitionPoutre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_UnitL1
        '
        Me.etq_UnitL1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL1.AutoSize = True
        Me.etq_UnitL1.Location = New System.Drawing.Point(479, 59)
        Me.etq_UnitL1.Name = "etq_UnitL1"
        Me.etq_UnitL1.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitL1.TabIndex = 101
        Me.etq_UnitL1.Text = "mm"
        '
        'txt_PorteeMini
        '
        Me.txt_PorteeMini.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PorteeMini.Location = New System.Drawing.Point(415, 55)
        Me.txt_PorteeMini.Name = "txt_PorteeMini"
        Me.txt_PorteeMini.Size = New System.Drawing.Size(58, 20)
        Me.txt_PorteeMini.TabIndex = 99
        '
        'img_PorteeMini
        '
        Me.img_PorteeMini.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PorteeMini.Location = New System.Drawing.Point(520, 55)
        Me.img_PorteeMini.Name = "img_PorteeMini"
        Me.img_PorteeMini.Size = New System.Drawing.Size(46, 20)
        Me.img_PorteeMini.TabIndex = 100
        Me.img_PorteeMini.TabStop = False
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
        Me.lbl_Scope.Text = "lbl_Materials"
        Me.lbl_Scope.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(502, 229)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(13, 13)
        Me.Label1.TabIndex = 134
        Me.Label1.Text = "≤"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_EpDallePleine
        '
        Me.lbl_EpDallePleine.AutoSize = True
        Me.lbl_EpDallePleine.Location = New System.Drawing.Point(42, 230)
        Me.lbl_EpDallePleine.Name = "lbl_EpDallePleine"
        Me.lbl_EpDallePleine.Size = New System.Drawing.Size(89, 13)
        Me.lbl_EpDallePleine.TabIndex = 132
        Me.lbl_EpDallePleine.Text = "lbl_EpDallePleine"
        Me.lbl_EpDallePleine.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitD1
        '
        Me.etq_UnitD1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitD1.AutoSize = True
        Me.etq_UnitD1.Location = New System.Drawing.Point(479, 230)
        Me.etq_UnitD1.Name = "etq_UnitD1"
        Me.etq_UnitD1.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitD1.TabIndex = 129
        Me.etq_UnitD1.Text = "mm"
        '
        'txt_EpDalleMin
        '
        Me.txt_EpDalleMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_EpDalleMin.Location = New System.Drawing.Point(415, 226)
        Me.txt_EpDalleMin.Name = "txt_EpDalleMin"
        Me.txt_EpDalleMin.Size = New System.Drawing.Size(58, 20)
        Me.txt_EpDalleMin.TabIndex = 127
        '
        'img_Td1
        '
        Me.img_Td1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Td1.Location = New System.Drawing.Point(520, 226)
        Me.img_Td1.Name = "img_Td1"
        Me.img_Td1.Size = New System.Drawing.Size(46, 20)
        Me.img_Td1.TabIndex = 128
        Me.img_Td1.TabStop = False
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
        Me.pan_Conteneur.PerformLayout()
        CType(Me.img_PorteeL2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_PorteeConsoleMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ThetaRd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_PorteeMini, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Td1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Label2 As Label
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
End Class
