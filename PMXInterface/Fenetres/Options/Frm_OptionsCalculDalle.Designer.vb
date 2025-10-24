<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsCalculDalle
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
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_Slimfloor = New System.Windows.Forms.Panel()
        Me.TLpan_Conteneur = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Conteneur = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.img_ecMax = New System.Windows.Forms.PictureBox()
        Me.txt_ecMax = New System.Windows.Forms.TextBox()
        Me.etq_UnitDim5 = New System.Windows.Forms.Label()
        Me.lbl_EntraxeCoutureMax = New System.Windows.Forms.Label()
        Me.lbl_MaintienBac = New System.Windows.Forms.Label()
        Me.pan_Dalles = New System.Windows.Forms.Panel()
        Me.txt_maxdecalage = New System.Windows.Forms.TextBox()
        Me.etq_UnitDim7 = New System.Windows.Forms.Label()
        Me.lbl_MaxDecalage = New System.Windows.Forms.Label()
        Me.img_bappmin = New System.Windows.Forms.PictureBox()
        Me.txt_bappmin = New System.Windows.Forms.TextBox()
        Me.etq_UnitDim2 = New System.Windows.Forms.Label()
        Me.lbl_bappmin = New System.Windows.Forms.Label()
        Me.lbl_Dalles = New System.Windows.Forms.Label()
        Me.pan_SlimFloors = New System.Windows.Forms.Panel()
        Me.lbl_Tcslimmin = New System.Windows.Forms.Label()
        Me.img_Tcslimmin = New System.Windows.Forms.PictureBox()
        Me.txt_TcSlimMin = New System.Windows.Forms.TextBox()
        Me.etq_UnitDim6 = New System.Windows.Forms.Label()
        Me.lbl_TwcdMin = New System.Windows.Forms.Label()
        Me.img_twcdmin = New System.Windows.Forms.PictureBox()
        Me.txt_twcdmin = New System.Windows.Forms.TextBox()
        Me.etq_UnitDim4 = New System.Windows.Forms.Label()
        Me.lbl_SlimFloor = New System.Windows.Forms.Label()
        Me.img_hslimmax = New System.Windows.Forms.PictureBox()
        Me.img_tpinfmin = New System.Windows.Forms.PictureBox()
        Me.txt_hslimmax = New System.Windows.Forms.TextBox()
        Me.txt_tpinfmin = New System.Windows.Forms.TextBox()
        Me.etq_UnitDim1 = New System.Windows.Forms.Label()
        Me.etq_UnitDim3 = New System.Windows.Forms.Label()
        Me.lbl_hslimmax = New System.Windows.Forms.Label()
        Me.lbl_tpinfmin = New System.Windows.Forms.Label()
        Me.lbl_Slimfloors = New System.Windows.Forms.Label()
        Me.img_maxdecalage = New System.Windows.Forms.PictureBox()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Slimfloor.SuspendLayout()
        Me.TLpan_Conteneur.SuspendLayout()
        Me.pan_Conteneur.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.img_ecMax, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_Dalles.SuspendLayout()
        CType(Me.img_bappmin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_SlimFloors.SuspendLayout()
        CType(Me.img_Tcslimmin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_twcdmin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_hslimmax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_tpinfmin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_maxdecalage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'pan_Slimfloor
        '
        Me.pan_Slimfloor.AutoScroll = True
        Me.pan_Slimfloor.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Slimfloor.Controls.Add(Me.TLpan_Conteneur)
        Me.pan_Slimfloor.Location = New System.Drawing.Point(30, 29)
        Me.pan_Slimfloor.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Slimfloor.Name = "pan_Slimfloor"
        Me.pan_Slimfloor.Size = New System.Drawing.Size(739, 472)
        Me.pan_Slimfloor.TabIndex = 2
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
        Me.pan_Conteneur.Controls.Add(Me.Panel1)
        Me.pan_Conteneur.Controls.Add(Me.lbl_MaintienBac)
        Me.pan_Conteneur.Controls.Add(Me.pan_Dalles)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Dalles)
        Me.pan_Conteneur.Controls.Add(Me.pan_SlimFloors)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Slimfloors)
        Me.pan_Conteneur.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Conteneur.Location = New System.Drawing.Point(0, 0)
        Me.pan_Conteneur.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Conteneur.Name = "pan_Conteneur"
        Me.pan_Conteneur.Size = New System.Drawing.Size(739, 436)
        Me.pan_Conteneur.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.img_ecMax)
        Me.Panel1.Controls.Add(Me.txt_ecMax)
        Me.Panel1.Controls.Add(Me.etq_UnitDim5)
        Me.Panel1.Controls.Add(Me.lbl_EntraxeCoutureMax)
        Me.Panel1.Location = New System.Drawing.Point(4, 125)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(731, 35)
        Me.Panel1.TabIndex = 150
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(603, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(13, 13)
        Me.Label4.TabIndex = 154
        Me.Label4.Text = "≥"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'img_ecMax
        '
        Me.img_ecMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ecMax.Location = New System.Drawing.Point(556, 7)
        Me.img_ecMax.Name = "img_ecMax"
        Me.img_ecMax.Size = New System.Drawing.Size(46, 20)
        Me.img_ecMax.TabIndex = 109
        Me.img_ecMax.TabStop = False
        '
        'txt_ecMax
        '
        Me.txt_ecMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ecMax.Location = New System.Drawing.Point(619, 7)
        Me.txt_ecMax.Name = "txt_ecMax"
        Me.txt_ecMax.Size = New System.Drawing.Size(58, 20)
        Me.txt_ecMax.TabIndex = 108
        Me.txt_ecMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitDim5
        '
        Me.etq_UnitDim5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim5.AutoSize = True
        Me.etq_UnitDim5.Location = New System.Drawing.Point(683, 10)
        Me.etq_UnitDim5.Name = "etq_UnitDim5"
        Me.etq_UnitDim5.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim5.TabIndex = 110
        Me.etq_UnitDim5.Text = "mm"
        '
        'lbl_EntraxeCoutureMax
        '
        Me.lbl_EntraxeCoutureMax.AutoSize = True
        Me.lbl_EntraxeCoutureMax.Location = New System.Drawing.Point(39, 10)
        Me.lbl_EntraxeCoutureMax.Name = "lbl_EntraxeCoutureMax"
        Me.lbl_EntraxeCoutureMax.Size = New System.Drawing.Size(116, 13)
        Me.lbl_EntraxeCoutureMax.TabIndex = 111
        Me.lbl_EntraxeCoutureMax.Text = "lbl_EntraxeCoutureMax"
        '
        'lbl_MaintienBac
        '
        Me.lbl_MaintienBac.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_MaintienBac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_MaintienBac.Location = New System.Drawing.Point(3, 99)
        Me.lbl_MaintienBac.Name = "lbl_MaintienBac"
        Me.lbl_MaintienBac.Size = New System.Drawing.Size(734, 23)
        Me.lbl_MaintienBac.TabIndex = 149
        Me.lbl_MaintienBac.Text = "lbl_MaintienBac"
        Me.lbl_MaintienBac.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Dalles
        '
        Me.pan_Dalles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Dalles.Controls.Add(Me.img_maxdecalage)
        Me.pan_Dalles.Controls.Add(Me.txt_maxdecalage)
        Me.pan_Dalles.Controls.Add(Me.etq_UnitDim7)
        Me.pan_Dalles.Controls.Add(Me.lbl_MaxDecalage)
        Me.pan_Dalles.Controls.Add(Me.img_bappmin)
        Me.pan_Dalles.Controls.Add(Me.txt_bappmin)
        Me.pan_Dalles.Controls.Add(Me.etq_UnitDim2)
        Me.pan_Dalles.Controls.Add(Me.lbl_bappmin)
        Me.pan_Dalles.Location = New System.Drawing.Point(4, 30)
        Me.pan_Dalles.Name = "pan_Dalles"
        Me.pan_Dalles.Size = New System.Drawing.Size(731, 66)
        Me.pan_Dalles.TabIndex = 148
        '
        'txt_maxdecalage
        '
        Me.txt_maxdecalage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_maxdecalage.Location = New System.Drawing.Point(618, 33)
        Me.txt_maxdecalage.Name = "txt_maxdecalage"
        Me.txt_maxdecalage.Size = New System.Drawing.Size(58, 20)
        Me.txt_maxdecalage.TabIndex = 112
        Me.txt_maxdecalage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitDim7
        '
        Me.etq_UnitDim7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim7.AutoSize = True
        Me.etq_UnitDim7.Location = New System.Drawing.Point(682, 36)
        Me.etq_UnitDim7.Name = "etq_UnitDim7"
        Me.etq_UnitDim7.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim7.TabIndex = 114
        Me.etq_UnitDim7.Text = "mm"
        '
        'lbl_MaxDecalage
        '
        Me.lbl_MaxDecalage.AutoSize = True
        Me.lbl_MaxDecalage.Location = New System.Drawing.Point(38, 36)
        Me.lbl_MaxDecalage.Name = "lbl_MaxDecalage"
        Me.lbl_MaxDecalage.Size = New System.Drawing.Size(89, 13)
        Me.lbl_MaxDecalage.TabIndex = 115
        Me.lbl_MaxDecalage.Text = "lbl_MaxDecalage"
        '
        'img_bappmin
        '
        Me.img_bappmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_bappmin.Location = New System.Drawing.Point(541, 7)
        Me.img_bappmin.Name = "img_bappmin"
        Me.img_bappmin.Size = New System.Drawing.Size(78, 20)
        Me.img_bappmin.TabIndex = 109
        Me.img_bappmin.TabStop = False
        '
        'txt_bappmin
        '
        Me.txt_bappmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_bappmin.Location = New System.Drawing.Point(619, 7)
        Me.txt_bappmin.Name = "txt_bappmin"
        Me.txt_bappmin.Size = New System.Drawing.Size(58, 20)
        Me.txt_bappmin.TabIndex = 108
        Me.txt_bappmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitDim2
        '
        Me.etq_UnitDim2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim2.AutoSize = True
        Me.etq_UnitDim2.Location = New System.Drawing.Point(683, 10)
        Me.etq_UnitDim2.Name = "etq_UnitDim2"
        Me.etq_UnitDim2.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim2.TabIndex = 110
        Me.etq_UnitDim2.Text = "mm"
        '
        'lbl_bappmin
        '
        Me.lbl_bappmin.AutoSize = True
        Me.lbl_bappmin.Location = New System.Drawing.Point(39, 10)
        Me.lbl_bappmin.Name = "lbl_bappmin"
        Me.lbl_bappmin.Size = New System.Drawing.Size(63, 13)
        Me.lbl_bappmin.TabIndex = 111
        Me.lbl_bappmin.Text = "lbl_bappmin"
        '
        'lbl_Dalles
        '
        Me.lbl_Dalles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Dalles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Dalles.Location = New System.Drawing.Point(3, 4)
        Me.lbl_Dalles.Name = "lbl_Dalles"
        Me.lbl_Dalles.Size = New System.Drawing.Size(734, 23)
        Me.lbl_Dalles.TabIndex = 147
        Me.lbl_Dalles.Text = "lbl_Dalles"
        Me.lbl_Dalles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SlimFloors
        '
        Me.pan_SlimFloors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_SlimFloors.Controls.Add(Me.lbl_Tcslimmin)
        Me.pan_SlimFloors.Controls.Add(Me.img_Tcslimmin)
        Me.pan_SlimFloors.Controls.Add(Me.txt_TcSlimMin)
        Me.pan_SlimFloors.Controls.Add(Me.etq_UnitDim6)
        Me.pan_SlimFloors.Controls.Add(Me.lbl_TwcdMin)
        Me.pan_SlimFloors.Controls.Add(Me.img_twcdmin)
        Me.pan_SlimFloors.Controls.Add(Me.txt_twcdmin)
        Me.pan_SlimFloors.Controls.Add(Me.etq_UnitDim4)
        Me.pan_SlimFloors.Controls.Add(Me.lbl_SlimFloor)
        Me.pan_SlimFloors.Controls.Add(Me.img_hslimmax)
        Me.pan_SlimFloors.Controls.Add(Me.img_tpinfmin)
        Me.pan_SlimFloors.Controls.Add(Me.txt_hslimmax)
        Me.pan_SlimFloors.Controls.Add(Me.txt_tpinfmin)
        Me.pan_SlimFloors.Controls.Add(Me.etq_UnitDim1)
        Me.pan_SlimFloors.Controls.Add(Me.etq_UnitDim3)
        Me.pan_SlimFloors.Controls.Add(Me.lbl_hslimmax)
        Me.pan_SlimFloors.Controls.Add(Me.lbl_tpinfmin)
        Me.pan_SlimFloors.Location = New System.Drawing.Point(3, 189)
        Me.pan_SlimFloors.Name = "pan_SlimFloors"
        Me.pan_SlimFloors.Size = New System.Drawing.Size(731, 141)
        Me.pan_SlimFloors.TabIndex = 146
        '
        'lbl_Tcslimmin
        '
        Me.lbl_Tcslimmin.AutoSize = True
        Me.lbl_Tcslimmin.Location = New System.Drawing.Point(39, 106)
        Me.lbl_Tcslimmin.Name = "lbl_Tcslimmin"
        Me.lbl_Tcslimmin.Size = New System.Drawing.Size(69, 13)
        Me.lbl_Tcslimmin.TabIndex = 134
        Me.lbl_Tcslimmin.Text = "lbl_Tcslimmin"
        '
        'img_Tcslimmin
        '
        Me.img_Tcslimmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Tcslimmin.Location = New System.Drawing.Point(556, 103)
        Me.img_Tcslimmin.Name = "img_Tcslimmin"
        Me.img_Tcslimmin.Size = New System.Drawing.Size(63, 20)
        Me.img_Tcslimmin.TabIndex = 132
        Me.img_Tcslimmin.TabStop = False
        '
        'txt_TcSlimMin
        '
        Me.txt_TcSlimMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_TcSlimMin.Location = New System.Drawing.Point(619, 103)
        Me.txt_TcSlimMin.Name = "txt_TcSlimMin"
        Me.txt_TcSlimMin.Size = New System.Drawing.Size(58, 20)
        Me.txt_TcSlimMin.TabIndex = 131
        Me.txt_TcSlimMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitDim6
        '
        Me.etq_UnitDim6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim6.AutoSize = True
        Me.etq_UnitDim6.Location = New System.Drawing.Point(683, 106)
        Me.etq_UnitDim6.Name = "etq_UnitDim6"
        Me.etq_UnitDim6.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim6.TabIndex = 133
        Me.etq_UnitDim6.Text = "mm"
        '
        'lbl_TwcdMin
        '
        Me.lbl_TwcdMin.AutoSize = True
        Me.lbl_TwcdMin.Location = New System.Drawing.Point(39, 82)
        Me.lbl_TwcdMin.Name = "lbl_TwcdMin"
        Me.lbl_TwcdMin.Size = New System.Drawing.Size(67, 13)
        Me.lbl_TwcdMin.TabIndex = 130
        Me.lbl_TwcdMin.Text = "lbl_TwcdMin"
        '
        'img_twcdmin
        '
        Me.img_twcdmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_twcdmin.Location = New System.Drawing.Point(556, 79)
        Me.img_twcdmin.Name = "img_twcdmin"
        Me.img_twcdmin.Size = New System.Drawing.Size(63, 20)
        Me.img_twcdmin.TabIndex = 128
        Me.img_twcdmin.TabStop = False
        '
        'txt_twcdmin
        '
        Me.txt_twcdmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_twcdmin.Location = New System.Drawing.Point(619, 79)
        Me.txt_twcdmin.Name = "txt_twcdmin"
        Me.txt_twcdmin.Size = New System.Drawing.Size(58, 20)
        Me.txt_twcdmin.TabIndex = 127
        Me.txt_twcdmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitDim4
        '
        Me.etq_UnitDim4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim4.AutoSize = True
        Me.etq_UnitDim4.Location = New System.Drawing.Point(683, 82)
        Me.etq_UnitDim4.Name = "etq_UnitDim4"
        Me.etq_UnitDim4.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim4.TabIndex = 129
        Me.etq_UnitDim4.Text = "mm"
        '
        'lbl_SlimFloor
        '
        Me.lbl_SlimFloor.AutoSize = True
        Me.lbl_SlimFloor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_SlimFloor.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_SlimFloor.Location = New System.Drawing.Point(5, 6)
        Me.lbl_SlimFloor.Name = "lbl_SlimFloor"
        Me.lbl_SlimFloor.Size = New System.Drawing.Size(65, 13)
        Me.lbl_SlimFloor.TabIndex = 126
        Me.lbl_SlimFloor.Text = "lbl_SlimFloor"
        Me.lbl_SlimFloor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_hslimmax
        '
        Me.img_hslimmax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_hslimmax.Location = New System.Drawing.Point(556, 27)
        Me.img_hslimmax.Name = "img_hslimmax"
        Me.img_hslimmax.Size = New System.Drawing.Size(63, 20)
        Me.img_hslimmax.TabIndex = 105
        Me.img_hslimmax.TabStop = False
        '
        'img_tpinfmin
        '
        Me.img_tpinfmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_tpinfmin.Location = New System.Drawing.Point(556, 53)
        Me.img_tpinfmin.Name = "img_tpinfmin"
        Me.img_tpinfmin.Size = New System.Drawing.Size(63, 20)
        Me.img_tpinfmin.TabIndex = 105
        Me.img_tpinfmin.TabStop = False
        '
        'txt_hslimmax
        '
        Me.txt_hslimmax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_hslimmax.Location = New System.Drawing.Point(619, 27)
        Me.txt_hslimmax.Name = "txt_hslimmax"
        Me.txt_hslimmax.Size = New System.Drawing.Size(58, 20)
        Me.txt_hslimmax.TabIndex = 104
        Me.txt_hslimmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_tpinfmin
        '
        Me.txt_tpinfmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_tpinfmin.Location = New System.Drawing.Point(619, 53)
        Me.txt_tpinfmin.Name = "txt_tpinfmin"
        Me.txt_tpinfmin.Size = New System.Drawing.Size(58, 20)
        Me.txt_tpinfmin.TabIndex = 104
        Me.txt_tpinfmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitDim1
        '
        Me.etq_UnitDim1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim1.AutoSize = True
        Me.etq_UnitDim1.Location = New System.Drawing.Point(683, 30)
        Me.etq_UnitDim1.Name = "etq_UnitDim1"
        Me.etq_UnitDim1.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim1.TabIndex = 106
        Me.etq_UnitDim1.Text = "mm"
        '
        'etq_UnitDim3
        '
        Me.etq_UnitDim3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim3.AutoSize = True
        Me.etq_UnitDim3.Location = New System.Drawing.Point(683, 56)
        Me.etq_UnitDim3.Name = "etq_UnitDim3"
        Me.etq_UnitDim3.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim3.TabIndex = 106
        Me.etq_UnitDim3.Text = "mm"
        '
        'lbl_hslimmax
        '
        Me.lbl_hslimmax.AutoSize = True
        Me.lbl_hslimmax.Location = New System.Drawing.Point(39, 34)
        Me.lbl_hslimmax.Name = "lbl_hslimmax"
        Me.lbl_hslimmax.Size = New System.Drawing.Size(65, 13)
        Me.lbl_hslimmax.TabIndex = 107
        Me.lbl_hslimmax.Text = "lbl_hslimmax"
        '
        'lbl_tpinfmin
        '
        Me.lbl_tpinfmin.AutoSize = True
        Me.lbl_tpinfmin.Location = New System.Drawing.Point(39, 60)
        Me.lbl_tpinfmin.Name = "lbl_tpinfmin"
        Me.lbl_tpinfmin.Size = New System.Drawing.Size(59, 13)
        Me.lbl_tpinfmin.TabIndex = 107
        Me.lbl_tpinfmin.Text = "lbl_tpinfmin"
        '
        'lbl_Slimfloors
        '
        Me.lbl_Slimfloors.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Slimfloors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Slimfloors.Location = New System.Drawing.Point(3, 163)
        Me.lbl_Slimfloors.Name = "lbl_Slimfloors"
        Me.lbl_Slimfloors.Size = New System.Drawing.Size(733, 23)
        Me.lbl_Slimfloors.TabIndex = 98
        Me.lbl_Slimfloors.Text = "lbl_Slimfloors"
        Me.lbl_Slimfloors.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'img_maxdecalage
        '
        Me.img_maxdecalage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_maxdecalage.Location = New System.Drawing.Point(541, 33)
        Me.img_maxdecalage.Name = "img_maxdecalage"
        Me.img_maxdecalage.Size = New System.Drawing.Size(78, 20)
        Me.img_maxdecalage.TabIndex = 135
        Me.img_maxdecalage.TabStop = False
        '
        'Frm_OptionsCalculDalle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(853, 546)
        Me.Controls.Add(Me.pan_Slimfloor)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsCalculDalle"
        Me.Text = "Frm_OptionsCalculSlimFloor"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Slimfloor.ResumeLayout(False)
        Me.TLpan_Conteneur.ResumeLayout(False)
        Me.pan_Conteneur.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.img_ecMax, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_Dalles.ResumeLayout(False)
        Me.pan_Dalles.PerformLayout()
        CType(Me.img_bappmin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_SlimFloors.ResumeLayout(False)
        Me.pan_SlimFloors.PerformLayout()
        CType(Me.img_Tcslimmin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_twcdmin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_hslimmax, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_tpinfmin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_maxdecalage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents pan_Slimfloor As Panel
    Friend WithEvents TLpan_Conteneur As TableLayoutPanel
    Friend WithEvents pan_Conteneur As Panel
    Friend WithEvents lbl_Slimfloors As Label
    Friend WithEvents pan_SlimFloors As Panel
    Friend WithEvents lbl_SlimFloor As Label
    Friend WithEvents img_hslimmax As PictureBox
    Friend WithEvents txt_hslimmax As TextBox
    Friend WithEvents etq_UnitDim1 As Label
    Friend WithEvents lbl_hslimmax As Label
    Friend WithEvents img_tpinfmin As PictureBox
    Friend WithEvents txt_tpinfmin As TextBox
    Friend WithEvents etq_UnitDim3 As Label
    Friend WithEvents lbl_tpinfmin As Label
    Friend WithEvents pan_Dalles As Panel
    Friend WithEvents lbl_Dalles As Label
    Friend WithEvents img_bappmin As PictureBox
    Friend WithEvents txt_bappmin As TextBox
    Friend WithEvents etq_UnitDim2 As Label
    Friend WithEvents lbl_bappmin As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents img_ecMax As PictureBox
    Friend WithEvents txt_ecMax As TextBox
    Friend WithEvents etq_UnitDim5 As Label
    Friend WithEvents lbl_EntraxeCoutureMax As Label
    Friend WithEvents lbl_MaintienBac As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lbl_TwcdMin As Label
    Friend WithEvents img_twcdmin As PictureBox
    Friend WithEvents txt_twcdmin As TextBox
    Friend WithEvents etq_UnitDim4 As Label
    Friend WithEvents lbl_Tcslimmin As Label
    Friend WithEvents img_Tcslimmin As PictureBox
    Friend WithEvents txt_TcSlimMin As TextBox
    Friend WithEvents etq_UnitDim6 As Label
    Friend WithEvents txt_maxdecalage As TextBox
    Friend WithEvents etq_UnitDim7 As Label
    Friend WithEvents lbl_MaxDecalage As Label
    Friend WithEvents img_maxdecalage As PictureBox
End Class
