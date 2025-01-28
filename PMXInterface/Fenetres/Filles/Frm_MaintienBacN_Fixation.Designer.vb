<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MaintienBacN_Fixation
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
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLpan_Centre = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Couturage = New System.Windows.Forms.Panel()
        Me.etq_UnitD3 = New System.Windows.Forms.Label()
        Me.txt_EspCouturage = New System.Windows.Forms.TextBox()
        Me.lbl_EspCouturage = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.etq_UnitD2 = New System.Windows.Forms.Label()
        Me.txt_DiametreCouture = New System.Windows.Forms.TextBox()
        Me.lbl_Diametre2 = New System.Windows.Forms.Label()
        Me.lbl_GlisseS_Info = New System.Windows.Forms.Label()
        Me.lbl_DiaS_Info = New System.Windows.Forms.Label()
        Me.lbl_GlisseS = New System.Windows.Forms.Label()
        Me.lbl_DiaS = New System.Windows.Forms.Label()
        Me.lbl_TypeCouturage = New System.Windows.Forms.Label()
        Me.cmb_TypSeamFastener = New System.Windows.Forms.ComboBox()
        Me.lbl_Couturage = New System.Windows.Forms.Label()
        Me.lbl_FixationSolive = New System.Windows.Forms.Label()
        Me.pan_FixationSolive = New System.Windows.Forms.Panel()
        Me.pan_FixationNervure = New System.Windows.Forms.Panel()
        Me.etq_UnitD1 = New System.Windows.Forms.Label()
        Me.txt_DiametreFixNerv = New System.Windows.Forms.TextBox()
        Me.lbl_Diametre1 = New System.Windows.Forms.Label()
        Me.lbl_SlipInfo = New System.Windows.Forms.Label()
        Me.lbl_DiametreInfo = New System.Windows.Forms.Label()
        Me.lbl_Glissement = New System.Windows.Forms.Label()
        Me.lbl_Diametre = New System.Windows.Forms.Label()
        Me.lbl_TypeFixation = New System.Windows.Forms.Label()
        Me.cmb_TypeFixation = New System.Windows.Forms.ComboBox()
        Me.cmb_FixationPoutre = New System.Windows.Forms.ComboBox()
        Me.lbl_Fixation = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Centre.SuspendLayout()
        Me.pan_Couturage.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pan_FixationSolive.SuspendLayout()
        Me.pan_FixationNervure.SuspendLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.AutoScroll = True
        Me.pan_Main.Controls.Add(Me.TLpan_Centre)
        Me.pan_Main.Location = New System.Drawing.Point(171, 49)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(336, 535)
        Me.pan_Main.TabIndex = 1
        '
        'TLpan_Centre
        '
        Me.TLpan_Centre.ColumnCount = 1
        Me.TLpan_Centre.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Centre.Controls.Add(Me.pan_Couturage, 0, 3)
        Me.TLpan_Centre.Controls.Add(Me.lbl_Couturage, 0, 2)
        Me.TLpan_Centre.Controls.Add(Me.lbl_FixationSolive, 0, 0)
        Me.TLpan_Centre.Controls.Add(Me.pan_FixationSolive, 0, 1)
        Me.TLpan_Centre.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLpan_Centre.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Centre.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.TLpan_Centre.Name = "TLpan_Centre"
        Me.TLpan_Centre.RowCount = 5
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TLpan_Centre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Centre.Size = New System.Drawing.Size(336, 383)
        Me.TLpan_Centre.TabIndex = 3
        '
        'pan_Couturage
        '
        Me.pan_Couturage.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Couturage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Couturage.Controls.Add(Me.etq_UnitD3)
        Me.pan_Couturage.Controls.Add(Me.txt_EspCouturage)
        Me.pan_Couturage.Controls.Add(Me.lbl_EspCouturage)
        Me.pan_Couturage.Controls.Add(Me.Panel4)
        Me.pan_Couturage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Couturage.Location = New System.Drawing.Point(1, 210)
        Me.pan_Couturage.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.pan_Couturage.Name = "pan_Couturage"
        Me.pan_Couturage.Size = New System.Drawing.Size(335, 150)
        Me.pan_Couturage.TabIndex = 4
        '
        'etq_UnitD3
        '
        Me.etq_UnitD3.AutoSize = True
        Me.etq_UnitD3.Location = New System.Drawing.Point(214, 126)
        Me.etq_UnitD3.Name = "etq_UnitD3"
        Me.etq_UnitD3.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitD3.TabIndex = 91
        Me.etq_UnitD3.Text = "kN"
        '
        'txt_EspCouturage
        '
        Me.txt_EspCouturage.Location = New System.Drawing.Point(150, 123)
        Me.txt_EspCouturage.Name = "txt_EspCouturage"
        Me.txt_EspCouturage.Size = New System.Drawing.Size(58, 20)
        Me.txt_EspCouturage.TabIndex = 90
        Me.txt_EspCouturage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_EspCouturage
        '
        Me.lbl_EspCouturage.AutoSize = True
        Me.lbl_EspCouturage.Location = New System.Drawing.Point(8, 126)
        Me.lbl_EspCouturage.Name = "lbl_EspCouturage"
        Me.lbl_EspCouturage.Size = New System.Drawing.Size(90, 13)
        Me.lbl_EspCouturage.TabIndex = 12
        Me.lbl_EspCouturage.Text = "lbl_EspCouturage"
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.Controls.Add(Me.etq_UnitD2)
        Me.Panel4.Controls.Add(Me.txt_DiametreCouture)
        Me.Panel4.Controls.Add(Me.lbl_Diametre2)
        Me.Panel4.Controls.Add(Me.lbl_GlisseS_Info)
        Me.Panel4.Controls.Add(Me.lbl_DiaS_Info)
        Me.Panel4.Controls.Add(Me.lbl_GlisseS)
        Me.Panel4.Controls.Add(Me.lbl_DiaS)
        Me.Panel4.Controls.Add(Me.lbl_TypeCouturage)
        Me.Panel4.Controls.Add(Me.cmb_TypSeamFastener)
        Me.Panel4.Location = New System.Drawing.Point(2, 4)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(331, 116)
        Me.Panel4.TabIndex = 11
        '
        'etq_UnitD2
        '
        Me.etq_UnitD2.AutoSize = True
        Me.etq_UnitD2.Location = New System.Drawing.Point(212, 96)
        Me.etq_UnitD2.Name = "etq_UnitD2"
        Me.etq_UnitD2.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitD2.TabIndex = 94
        Me.etq_UnitD2.Text = "kN"
        '
        'txt_DiametreCouture
        '
        Me.txt_DiametreCouture.Location = New System.Drawing.Point(148, 93)
        Me.txt_DiametreCouture.Name = "txt_DiametreCouture"
        Me.txt_DiametreCouture.Size = New System.Drawing.Size(58, 20)
        Me.txt_DiametreCouture.TabIndex = 93
        Me.txt_DiametreCouture.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_Diametre2
        '
        Me.lbl_Diametre2.AutoSize = True
        Me.lbl_Diametre2.Location = New System.Drawing.Point(6, 96)
        Me.lbl_Diametre2.Name = "lbl_Diametre2"
        Me.lbl_Diametre2.Size = New System.Drawing.Size(71, 13)
        Me.lbl_Diametre2.TabIndex = 92
        Me.lbl_Diametre2.Text = "lbl_Diametre2"
        '
        'lbl_GlisseS_Info
        '
        Me.lbl_GlisseS_Info.AutoSize = True
        Me.lbl_GlisseS_Info.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lbl_DiaS_Info.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lbl_GlisseS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lbl_DiaS.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.cmb_TypSeamFastener.Size = New System.Drawing.Size(283, 21)
        Me.cmb_TypSeamFastener.TabIndex = 9
        '
        'lbl_Couturage
        '
        Me.lbl_Couturage.AutoSize = True
        Me.lbl_Couturage.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Couturage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Couturage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Couturage.Location = New System.Drawing.Point(1, 180)
        Me.lbl_Couturage.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.lbl_Couturage.Name = "lbl_Couturage"
        Me.lbl_Couturage.Size = New System.Drawing.Size(335, 30)
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
        Me.lbl_FixationSolive.Location = New System.Drawing.Point(1, 0)
        Me.lbl_FixationSolive.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.lbl_FixationSolive.Name = "lbl_FixationSolive"
        Me.lbl_FixationSolive.Size = New System.Drawing.Size(335, 30)
        Me.lbl_FixationSolive.TabIndex = 1
        Me.lbl_FixationSolive.Text = "lbl_FixationSolive"
        Me.lbl_FixationSolive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_FixationSolive
        '
        Me.pan_FixationSolive.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_FixationSolive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_FixationSolive.Controls.Add(Me.pan_FixationNervure)
        Me.pan_FixationSolive.Controls.Add(Me.cmb_FixationPoutre)
        Me.pan_FixationSolive.Controls.Add(Me.lbl_Fixation)
        Me.pan_FixationSolive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_FixationSolive.Location = New System.Drawing.Point(1, 30)
        Me.pan_FixationSolive.Margin = New System.Windows.Forms.Padding(1, 0, 0, 1)
        Me.pan_FixationSolive.Name = "pan_FixationSolive"
        Me.pan_FixationSolive.Size = New System.Drawing.Size(335, 149)
        Me.pan_FixationSolive.TabIndex = 3
        '
        'pan_FixationNervure
        '
        Me.pan_FixationNervure.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_FixationNervure.Controls.Add(Me.etq_UnitD1)
        Me.pan_FixationNervure.Controls.Add(Me.txt_DiametreFixNerv)
        Me.pan_FixationNervure.Controls.Add(Me.lbl_Diametre1)
        Me.pan_FixationNervure.Controls.Add(Me.lbl_SlipInfo)
        Me.pan_FixationNervure.Controls.Add(Me.lbl_DiametreInfo)
        Me.pan_FixationNervure.Controls.Add(Me.lbl_Glissement)
        Me.pan_FixationNervure.Controls.Add(Me.lbl_Diametre)
        Me.pan_FixationNervure.Controls.Add(Me.lbl_TypeFixation)
        Me.pan_FixationNervure.Controls.Add(Me.cmb_TypeFixation)
        Me.pan_FixationNervure.Location = New System.Drawing.Point(2, 29)
        Me.pan_FixationNervure.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_FixationNervure.Name = "pan_FixationNervure"
        Me.pan_FixationNervure.Size = New System.Drawing.Size(331, 115)
        Me.pan_FixationNervure.TabIndex = 10
        '
        'etq_UnitD1
        '
        Me.etq_UnitD1.AutoSize = True
        Me.etq_UnitD1.Location = New System.Drawing.Point(212, 95)
        Me.etq_UnitD1.Name = "etq_UnitD1"
        Me.etq_UnitD1.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitD1.TabIndex = 94
        Me.etq_UnitD1.Text = "kN"
        '
        'txt_DiametreFixNerv
        '
        Me.txt_DiametreFixNerv.Location = New System.Drawing.Point(148, 92)
        Me.txt_DiametreFixNerv.Name = "txt_DiametreFixNerv"
        Me.txt_DiametreFixNerv.Size = New System.Drawing.Size(58, 20)
        Me.txt_DiametreFixNerv.TabIndex = 93
        Me.txt_DiametreFixNerv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_Diametre1
        '
        Me.lbl_Diametre1.AutoSize = True
        Me.lbl_Diametre1.Location = New System.Drawing.Point(6, 95)
        Me.lbl_Diametre1.Name = "lbl_Diametre1"
        Me.lbl_Diametre1.Size = New System.Drawing.Size(71, 13)
        Me.lbl_Diametre1.TabIndex = 92
        Me.lbl_Diametre1.Text = "lbl_Diametre1"
        '
        'lbl_SlipInfo
        '
        Me.lbl_SlipInfo.AutoSize = True
        Me.lbl_SlipInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lbl_DiametreInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lbl_Glissement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.lbl_Diametre.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.cmb_TypeFixation.Location = New System.Drawing.Point(24, 24)
        Me.cmb_TypeFixation.Name = "cmb_TypeFixation"
        Me.cmb_TypeFixation.Size = New System.Drawing.Size(296, 21)
        Me.cmb_TypeFixation.TabIndex = 9
        '
        'cmb_FixationPoutre
        '
        Me.cmb_FixationPoutre.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_FixationPoutre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_FixationPoutre.FormattingEnabled = True
        Me.cmb_FixationPoutre.Location = New System.Drawing.Point(103, 6)
        Me.cmb_FixationPoutre.Name = "cmb_FixationPoutre"
        Me.cmb_FixationPoutre.Size = New System.Drawing.Size(217, 21)
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
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Frm_MaintienBacN_Fixation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 632)
        Me.Controls.Add(Me.pan_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_MaintienBacN_Fixation"
        Me.Text = "Frm_MaintienBacN_Fixation"
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Centre.ResumeLayout(False)
        Me.TLpan_Centre.PerformLayout()
        Me.pan_Couturage.ResumeLayout(False)
        Me.pan_Couturage.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.pan_FixationSolive.ResumeLayout(False)
        Me.pan_FixationSolive.PerformLayout()
        Me.pan_FixationNervure.ResumeLayout(False)
        Me.pan_FixationNervure.PerformLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Centre As TableLayoutPanel
    Friend WithEvents pan_Couturage As Panel
    Friend WithEvents etq_UnitD3 As Label
    Friend WithEvents txt_EspCouturage As TextBox
    Friend WithEvents lbl_EspCouturage As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents lbl_GlisseS_Info As Label
    Friend WithEvents lbl_DiaS_Info As Label
    Friend WithEvents lbl_GlisseS As Label
    Friend WithEvents lbl_DiaS As Label
    Friend WithEvents lbl_TypeCouturage As Label
    Friend WithEvents cmb_TypSeamFastener As ComboBox
    Friend WithEvents lbl_Couturage As Label
    Friend WithEvents lbl_FixationSolive As Label
    Friend WithEvents pan_FixationSolive As Panel
    Friend WithEvents pan_FixationNervure As Panel
    Friend WithEvents lbl_SlipInfo As Label
    Friend WithEvents lbl_DiametreInfo As Label
    Friend WithEvents lbl_Glissement As Label
    Friend WithEvents lbl_Diametre As Label
    Friend WithEvents lbl_TypeFixation As Label
    Friend WithEvents cmb_TypeFixation As ComboBox
    Friend WithEvents cmb_FixationPoutre As ComboBox
    Friend WithEvents lbl_Fixation As Label
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents etq_UnitD2 As Label
    Friend WithEvents txt_DiametreCouture As TextBox
    Friend WithEvents lbl_Diametre2 As Label
    Friend WithEvents etq_UnitD1 As Label
    Friend WithEvents txt_DiametreFixNerv As TextBox
    Friend WithEvents lbl_Diametre1 As Label
End Class
