<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsCalculCalcul
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
        Me.pan_Calcul = New System.Windows.Forms.Panel()
        Me.TLpan_Conteneur = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Conteneur = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_NbMiniNConsole = New System.Windows.Forms.TextBox()
        Me.img_NbNodes2 = New System.Windows.Forms.PictureBox()
        Me.txt_NbMiniNTravee = New System.Windows.Forms.TextBox()
        Me.img_NbNodes1 = New System.Windows.Forms.PictureBox()
        Me.lbl_Console = New System.Windows.Forms.Label()
        Me.lbl_TraveesI = New System.Windows.Forms.Label()
        Me.lbl_NbNodes = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.etq_UnitL1 = New System.Windows.Forms.Label()
        Me.txt_EspNoeuds = New System.Windows.Forms.TextBox()
        Me.img_dNodes = New System.Windows.Forms.PictureBox()
        Me.lbl_DistanceMaxNoeuds = New System.Windows.Forms.Label()
        Me.lbl_Discretisation = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chk_RebarsInCompression = New System.Windows.Forms.CheckBox()
        Me.chk_SimplifiedEffectiveW = New System.Windows.Forms.CheckBox()
        Me.lbl_CrossSectionProperties = New System.Windows.Forms.Label()
        Me.etq_UnitEs = New System.Windows.Forms.Label()
        Me.lbl_YoungRebars = New System.Windows.Forms.Label()
        Me.txt_Es = New System.Windows.Forms.TextBox()
        Me.img_Es = New System.Windows.Forms.PictureBox()
        Me.cmb_Norme = New System.Windows.Forms.ComboBox()
        Me.lbl_Norme = New System.Windows.Forms.Label()
        Me.lbl_Calcul = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txt_PsiLSH = New System.Windows.Forms.TextBox()
        Me.img_PsiLSH = New System.Windows.Forms.PictureBox()
        Me.lbl_ShrinkageLC = New System.Windows.Forms.Label()
        Me.lbl_PermanentLC = New System.Windows.Forms.Label()
        Me.txt_PsiLG = New System.Windows.Forms.TextBox()
        Me.img_PsiLG = New System.Windows.Forms.PictureBox()
        Me.lbl_CreepMultiplier = New System.Windows.Forms.Label()
        Me.lbl_LoadCases = New System.Windows.Forms.Label()
        Me.lbl_TimeT0 = New System.Windows.Forms.Label()
        Me.lbl_Dalle = New System.Windows.Forms.Label()
        Me.lbl_Enrobage = New System.Windows.Forms.Label()
        Me.txt_t0G1Enrob = New System.Windows.Forms.TextBox()
        Me.txt_t0G1Dalle = New System.Windows.Forms.TextBox()
        Me.txt_t0G2Dalle = New System.Windows.Forms.TextBox()
        Me.txt_t0G2Enrob = New System.Windows.Forms.TextBox()
        Me.txt_t0SHDalle = New System.Windows.Forms.TextBox()
        Me.txt_t0SHEnrob = New System.Windows.Forms.TextBox()
        Me.etq_UnitJour1 = New System.Windows.Forms.Label()
        Me.etq_UnitJour3 = New System.Windows.Forms.Label()
        Me.etq_UnitJour5 = New System.Windows.Forms.Label()
        Me.etq_UnitJour6 = New System.Windows.Forms.Label()
        Me.etq_UnitJour4 = New System.Windows.Forms.Label()
        Me.etq_UnitJour2 = New System.Windows.Forms.Label()
        Me.lbl_G1 = New System.Windows.Forms.Label()
        Me.lbl_G2 = New System.Windows.Forms.Label()
        Me.lbl_SH = New System.Windows.Forms.Label()
        Me.img_T0G1 = New System.Windows.Forms.PictureBox()
        Me.img_T0G2 = New System.Windows.Forms.PictureBox()
        Me.img_T0SH = New System.Windows.Forms.PictureBox()
        Me.pan_Calcul.SuspendLayout()
        Me.TLpan_Conteneur.SuspendLayout()
        Me.pan_Conteneur.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.img_NbNodes2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_NbNodes1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_dNodes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.img_Es, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.img_PsiLSH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_PsiLG, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_T0G1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_T0G2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_T0SH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Calcul
        '
        Me.pan_Calcul.AutoScroll = True
        Me.pan_Calcul.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Calcul.Controls.Add(Me.TLpan_Conteneur)
        Me.pan_Calcul.Location = New System.Drawing.Point(31, 63)
        Me.pan_Calcul.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Calcul.Name = "pan_Calcul"
        Me.pan_Calcul.Size = New System.Drawing.Size(739, 658)
        Me.pan_Calcul.TabIndex = 2
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
        Me.TLpan_Conteneur.Size = New System.Drawing.Size(739, 637)
        Me.TLpan_Conteneur.TabIndex = 0
        '
        'pan_Conteneur
        '
        Me.pan_Conteneur.Controls.Add(Me.Panel3)
        Me.pan_Conteneur.Controls.Add(Me.Panel2)
        Me.pan_Conteneur.Controls.Add(Me.Panel1)
        Me.pan_Conteneur.Controls.Add(Me.cmb_Norme)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Norme)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Calcul)
        Me.pan_Conteneur.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Conteneur.Location = New System.Drawing.Point(0, 0)
        Me.pan_Conteneur.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Conteneur.Name = "pan_Conteneur"
        Me.pan_Conteneur.Size = New System.Drawing.Size(739, 637)
        Me.pan_Conteneur.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.txt_NbMiniNConsole)
        Me.Panel2.Controls.Add(Me.img_NbNodes2)
        Me.Panel2.Controls.Add(Me.txt_NbMiniNTravee)
        Me.Panel2.Controls.Add(Me.img_NbNodes1)
        Me.Panel2.Controls.Add(Me.lbl_Console)
        Me.Panel2.Controls.Add(Me.lbl_TraveesI)
        Me.Panel2.Controls.Add(Me.lbl_NbNodes)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.etq_UnitL1)
        Me.Panel2.Controls.Add(Me.txt_EspNoeuds)
        Me.Panel2.Controls.Add(Me.img_dNodes)
        Me.Panel2.Controls.Add(Me.lbl_DistanceMaxNoeuds)
        Me.Panel2.Controls.Add(Me.lbl_Discretisation)
        Me.Panel2.Location = New System.Drawing.Point(3, 222)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(732, 134)
        Me.Panel2.TabIndex = 106
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(597, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(13, 13)
        Me.Label1.TabIndex = 133
        Me.Label1.Text = "≥"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(597, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(13, 13)
        Me.Label2.TabIndex = 132
        Me.Label2.Text = "≥"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txt_NbMiniNConsole
        '
        Me.txt_NbMiniNConsole.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_NbMiniNConsole.Location = New System.Drawing.Point(610, 73)
        Me.txt_NbMiniNConsole.Name = "txt_NbMiniNConsole"
        Me.txt_NbMiniNConsole.Size = New System.Drawing.Size(58, 20)
        Me.txt_NbMiniNConsole.TabIndex = 130
        Me.txt_NbMiniNConsole.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'img_NbNodes2
        '
        Me.img_NbNodes2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_NbNodes2.Location = New System.Drawing.Point(551, 73)
        Me.img_NbNodes2.Name = "img_NbNodes2"
        Me.img_NbNodes2.Size = New System.Drawing.Size(46, 20)
        Me.img_NbNodes2.TabIndex = 131
        Me.img_NbNodes2.TabStop = False
        '
        'txt_NbMiniNTravee
        '
        Me.txt_NbMiniNTravee.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_NbMiniNTravee.Location = New System.Drawing.Point(610, 50)
        Me.txt_NbMiniNTravee.Name = "txt_NbMiniNTravee"
        Me.txt_NbMiniNTravee.Size = New System.Drawing.Size(58, 20)
        Me.txt_NbMiniNTravee.TabIndex = 128
        Me.txt_NbMiniNTravee.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'img_NbNodes1
        '
        Me.img_NbNodes1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_NbNodes1.Location = New System.Drawing.Point(551, 50)
        Me.img_NbNodes1.Name = "img_NbNodes1"
        Me.img_NbNodes1.Size = New System.Drawing.Size(46, 20)
        Me.img_NbNodes1.TabIndex = 129
        Me.img_NbNodes1.TabStop = False
        '
        'lbl_Console
        '
        Me.lbl_Console.AutoSize = True
        Me.lbl_Console.Location = New System.Drawing.Point(237, 76)
        Me.lbl_Console.Name = "lbl_Console"
        Me.lbl_Console.Size = New System.Drawing.Size(61, 13)
        Me.lbl_Console.TabIndex = 127
        Me.lbl_Console.Text = "lbl_Console"
        '
        'lbl_TraveesI
        '
        Me.lbl_TraveesI.AutoSize = True
        Me.lbl_TraveesI.Location = New System.Drawing.Point(237, 53)
        Me.lbl_TraveesI.Name = "lbl_TraveesI"
        Me.lbl_TraveesI.Size = New System.Drawing.Size(65, 13)
        Me.lbl_TraveesI.TabIndex = 126
        Me.lbl_TraveesI.Text = "lbl_TraveesI"
        '
        'lbl_NbNodes
        '
        Me.lbl_NbNodes.AutoSize = True
        Me.lbl_NbNodes.Location = New System.Drawing.Point(39, 53)
        Me.lbl_NbNodes.Name = "lbl_NbNodes"
        Me.lbl_NbNodes.Size = New System.Drawing.Size(68, 13)
        Me.lbl_NbNodes.TabIndex = 125
        Me.lbl_NbNodes.Text = "lbl_NbNodes"
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
        Me.txt_EspNoeuds.Size = New System.Drawing.Size(58, 20)
        Me.txt_EspNoeuds.TabIndex = 107
        Me.txt_EspNoeuds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        'lbl_Discretisation
        '
        Me.lbl_Discretisation.AutoSize = True
        Me.lbl_Discretisation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Discretisation.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Discretisation.Location = New System.Drawing.Point(5, 5)
        Me.lbl_Discretisation.Name = "lbl_Discretisation"
        Me.lbl_Discretisation.Size = New System.Drawing.Size(86, 13)
        Me.lbl_Discretisation.TabIndex = 105
        Me.lbl_Discretisation.Text = "lbl_Discretisation"
        Me.lbl_Discretisation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.chk_RebarsInCompression)
        Me.Panel1.Controls.Add(Me.chk_SimplifiedEffectiveW)
        Me.Panel1.Controls.Add(Me.lbl_CrossSectionProperties)
        Me.Panel1.Controls.Add(Me.etq_UnitEs)
        Me.Panel1.Controls.Add(Me.lbl_YoungRebars)
        Me.Panel1.Controls.Add(Me.txt_Es)
        Me.Panel1.Controls.Add(Me.img_Es)
        Me.Panel1.Location = New System.Drawing.Point(3, 61)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(732, 158)
        Me.Panel1.TabIndex = 105
        '
        'chk_RebarsInCompression
        '
        Me.chk_RebarsInCompression.AutoSize = True
        Me.chk_RebarsInCompression.Location = New System.Drawing.Point(42, 53)
        Me.chk_RebarsInCompression.Name = "chk_RebarsInCompression"
        Me.chk_RebarsInCompression.Size = New System.Drawing.Size(153, 17)
        Me.chk_RebarsInCompression.TabIndex = 110
        Me.chk_RebarsInCompression.Text = "chk_RebarsInCompression"
        Me.chk_RebarsInCompression.UseVisualStyleBackColor = True
        '
        'chk_SimplifiedEffectiveW
        '
        Me.chk_SimplifiedEffectiveW.AutoSize = True
        Me.chk_SimplifiedEffectiveW.Location = New System.Drawing.Point(42, 30)
        Me.chk_SimplifiedEffectiveW.Name = "chk_SimplifiedEffectiveW"
        Me.chk_SimplifiedEffectiveW.Size = New System.Drawing.Size(147, 17)
        Me.chk_SimplifiedEffectiveW.TabIndex = 109
        Me.chk_SimplifiedEffectiveW.Text = "chk_SimplifiedEffectiveW"
        Me.chk_SimplifiedEffectiveW.UseVisualStyleBackColor = True
        '
        'lbl_CrossSectionProperties
        '
        Me.lbl_CrossSectionProperties.AutoSize = True
        Me.lbl_CrossSectionProperties.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_CrossSectionProperties.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_CrossSectionProperties.Location = New System.Drawing.Point(5, 5)
        Me.lbl_CrossSectionProperties.Name = "lbl_CrossSectionProperties"
        Me.lbl_CrossSectionProperties.Size = New System.Drawing.Size(132, 13)
        Me.lbl_CrossSectionProperties.TabIndex = 104
        Me.lbl_CrossSectionProperties.Text = "lbl_CrossSectionProperties"
        Me.lbl_CrossSectionProperties.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_UnitEs
        '
        Me.etq_UnitEs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitEs.AutoSize = True
        Me.etq_UnitEs.Location = New System.Drawing.Point(674, 74)
        Me.etq_UnitEs.Name = "etq_UnitEs"
        Me.etq_UnitEs.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitEs.TabIndex = 101
        Me.etq_UnitEs.Text = "Label1"
        '
        'lbl_YoungRebars
        '
        Me.lbl_YoungRebars.AutoSize = True
        Me.lbl_YoungRebars.Location = New System.Drawing.Point(39, 78)
        Me.lbl_YoungRebars.Name = "lbl_YoungRebars"
        Me.lbl_YoungRebars.Size = New System.Drawing.Size(88, 13)
        Me.lbl_YoungRebars.TabIndex = 102
        Me.lbl_YoungRebars.Text = "lbl_YoungRebars"
        '
        'txt_Es
        '
        Me.txt_Es.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Es.Location = New System.Drawing.Point(610, 71)
        Me.txt_Es.Name = "txt_Es"
        Me.txt_Es.Size = New System.Drawing.Size(58, 20)
        Me.txt_Es.TabIndex = 99
        Me.txt_Es.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'img_Es
        '
        Me.img_Es.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Es.Location = New System.Drawing.Point(564, 71)
        Me.img_Es.Name = "img_Es"
        Me.img_Es.Size = New System.Drawing.Size(46, 20)
        Me.img_Es.TabIndex = 100
        Me.img_Es.TabStop = False
        '
        'cmb_Norme
        '
        Me.cmb_Norme.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Norme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Norme.FormattingEnabled = True
        Me.cmb_Norme.Location = New System.Drawing.Point(186, 33)
        Me.cmb_Norme.Name = "cmb_Norme"
        Me.cmb_Norme.Size = New System.Drawing.Size(460, 21)
        Me.cmb_Norme.TabIndex = 104
        '
        'lbl_Norme
        '
        Me.lbl_Norme.AutoSize = True
        Me.lbl_Norme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Norme.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Norme.Location = New System.Drawing.Point(8, 36)
        Me.lbl_Norme.Name = "lbl_Norme"
        Me.lbl_Norme.Size = New System.Drawing.Size(54, 13)
        Me.lbl_Norme.TabIndex = 103
        Me.lbl_Norme.Text = "lbl_Norme"
        Me.lbl_Norme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Calcul
        '
        Me.lbl_Calcul.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Calcul.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Calcul.Location = New System.Drawing.Point(3, 2)
        Me.lbl_Calcul.Name = "lbl_Calcul"
        Me.lbl_Calcul.Size = New System.Drawing.Size(733, 23)
        Me.lbl_Calcul.TabIndex = 98
        Me.lbl_Calcul.Text = "lbl_Calcul"
        Me.lbl_Calcul.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.Controls.Add(Me.img_T0SH)
        Me.Panel3.Controls.Add(Me.img_T0G2)
        Me.Panel3.Controls.Add(Me.img_T0G1)
        Me.Panel3.Controls.Add(Me.lbl_SH)
        Me.Panel3.Controls.Add(Me.lbl_G2)
        Me.Panel3.Controls.Add(Me.lbl_G1)
        Me.Panel3.Controls.Add(Me.etq_UnitJour6)
        Me.Panel3.Controls.Add(Me.etq_UnitJour4)
        Me.Panel3.Controls.Add(Me.etq_UnitJour2)
        Me.Panel3.Controls.Add(Me.etq_UnitJour5)
        Me.Panel3.Controls.Add(Me.etq_UnitJour3)
        Me.Panel3.Controls.Add(Me.etq_UnitJour1)
        Me.Panel3.Controls.Add(Me.txt_t0SHDalle)
        Me.Panel3.Controls.Add(Me.txt_t0SHEnrob)
        Me.Panel3.Controls.Add(Me.txt_t0G2Dalle)
        Me.Panel3.Controls.Add(Me.txt_t0G2Enrob)
        Me.Panel3.Controls.Add(Me.txt_t0G1Dalle)
        Me.Panel3.Controls.Add(Me.txt_t0G1Enrob)
        Me.Panel3.Controls.Add(Me.lbl_Enrobage)
        Me.Panel3.Controls.Add(Me.lbl_Dalle)
        Me.Panel3.Controls.Add(Me.lbl_TimeT0)
        Me.Panel3.Controls.Add(Me.txt_PsiLSH)
        Me.Panel3.Controls.Add(Me.img_PsiLSH)
        Me.Panel3.Controls.Add(Me.lbl_ShrinkageLC)
        Me.Panel3.Controls.Add(Me.lbl_PermanentLC)
        Me.Panel3.Controls.Add(Me.txt_PsiLG)
        Me.Panel3.Controls.Add(Me.img_PsiLG)
        Me.Panel3.Controls.Add(Me.lbl_CreepMultiplier)
        Me.Panel3.Controls.Add(Me.lbl_LoadCases)
        Me.Panel3.Location = New System.Drawing.Point(3, 359)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(732, 254)
        Me.Panel3.TabIndex = 107
        '
        'txt_PsiLSH
        '
        Me.txt_PsiLSH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PsiLSH.Location = New System.Drawing.Point(610, 50)
        Me.txt_PsiLSH.Name = "txt_PsiLSH"
        Me.txt_PsiLSH.Size = New System.Drawing.Size(58, 20)
        Me.txt_PsiLSH.TabIndex = 128
        Me.txt_PsiLSH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'img_PsiLSH
        '
        Me.img_PsiLSH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PsiLSH.Location = New System.Drawing.Point(563, 50)
        Me.img_PsiLSH.Name = "img_PsiLSH"
        Me.img_PsiLSH.Size = New System.Drawing.Size(46, 20)
        Me.img_PsiLSH.TabIndex = 129
        Me.img_PsiLSH.TabStop = False
        '
        'lbl_ShrinkageLC
        '
        Me.lbl_ShrinkageLC.AutoSize = True
        Me.lbl_ShrinkageLC.Location = New System.Drawing.Point(237, 53)
        Me.lbl_ShrinkageLC.Name = "lbl_ShrinkageLC"
        Me.lbl_ShrinkageLC.Size = New System.Drawing.Size(84, 13)
        Me.lbl_ShrinkageLC.TabIndex = 127
        Me.lbl_ShrinkageLC.Text = "lbl_ShrinkageLC"
        '
        'lbl_PermanentLC
        '
        Me.lbl_PermanentLC.AutoSize = True
        Me.lbl_PermanentLC.Location = New System.Drawing.Point(237, 30)
        Me.lbl_PermanentLC.Name = "lbl_PermanentLC"
        Me.lbl_PermanentLC.Size = New System.Drawing.Size(87, 13)
        Me.lbl_PermanentLC.TabIndex = 126
        Me.lbl_PermanentLC.Text = "lbl_PermanentLC"
        '
        'txt_PsiLG
        '
        Me.txt_PsiLG.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PsiLG.Location = New System.Drawing.Point(610, 27)
        Me.txt_PsiLG.Name = "txt_PsiLG"
        Me.txt_PsiLG.Size = New System.Drawing.Size(58, 20)
        Me.txt_PsiLG.TabIndex = 107
        Me.txt_PsiLG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'img_PsiLG
        '
        Me.img_PsiLG.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PsiLG.Location = New System.Drawing.Point(563, 27)
        Me.img_PsiLG.Name = "img_PsiLG"
        Me.img_PsiLG.Size = New System.Drawing.Size(46, 20)
        Me.img_PsiLG.TabIndex = 108
        Me.img_PsiLG.TabStop = False
        '
        'lbl_CreepMultiplier
        '
        Me.lbl_CreepMultiplier.AutoSize = True
        Me.lbl_CreepMultiplier.Location = New System.Drawing.Point(39, 30)
        Me.lbl_CreepMultiplier.Name = "lbl_CreepMultiplier"
        Me.lbl_CreepMultiplier.Size = New System.Drawing.Size(92, 13)
        Me.lbl_CreepMultiplier.TabIndex = 106
        Me.lbl_CreepMultiplier.Text = "lbl_CreepMultiplier"
        '
        'lbl_LoadCases
        '
        Me.lbl_LoadCases.AutoSize = True
        Me.lbl_LoadCases.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_LoadCases.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_LoadCases.Location = New System.Drawing.Point(5, 5)
        Me.lbl_LoadCases.Name = "lbl_LoadCases"
        Me.lbl_LoadCases.Size = New System.Drawing.Size(76, 13)
        Me.lbl_LoadCases.TabIndex = 105
        Me.lbl_LoadCases.Text = "lbl_LoadCases"
        Me.lbl_LoadCases.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_TimeT0
        '
        Me.lbl_TimeT0.AutoSize = True
        Me.lbl_TimeT0.Location = New System.Drawing.Point(39, 81)
        Me.lbl_TimeT0.Name = "lbl_TimeT0"
        Me.lbl_TimeT0.Size = New System.Drawing.Size(59, 13)
        Me.lbl_TimeT0.TabIndex = 130
        Me.lbl_TimeT0.Text = "lbl_TimeT0"
        '
        'lbl_Dalle
        '
        Me.lbl_Dalle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Dalle.AutoSize = True
        Me.lbl_Dalle.Location = New System.Drawing.Point(530, 81)
        Me.lbl_Dalle.Name = "lbl_Dalle"
        Me.lbl_Dalle.Size = New System.Drawing.Size(47, 13)
        Me.lbl_Dalle.TabIndex = 131
        Me.lbl_Dalle.Text = "lbl_Dalle"
        '
        'lbl_Enrobage
        '
        Me.lbl_Enrobage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Enrobage.AutoSize = True
        Me.lbl_Enrobage.Location = New System.Drawing.Point(607, 81)
        Me.lbl_Enrobage.Name = "lbl_Enrobage"
        Me.lbl_Enrobage.Size = New System.Drawing.Size(69, 13)
        Me.lbl_Enrobage.TabIndex = 132
        Me.lbl_Enrobage.Text = "lbl_Enrobage"
        '
        'txt_t0G1Enrob
        '
        Me.txt_t0G1Enrob.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_t0G1Enrob.Location = New System.Drawing.Point(612, 97)
        Me.txt_t0G1Enrob.Name = "txt_t0G1Enrob"
        Me.txt_t0G1Enrob.Size = New System.Drawing.Size(58, 20)
        Me.txt_t0G1Enrob.TabIndex = 133
        Me.txt_t0G1Enrob.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_t0G1Dalle
        '
        Me.txt_t0G1Dalle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_t0G1Dalle.Location = New System.Drawing.Point(523, 97)
        Me.txt_t0G1Dalle.Name = "txt_t0G1Dalle"
        Me.txt_t0G1Dalle.Size = New System.Drawing.Size(58, 20)
        Me.txt_t0G1Dalle.TabIndex = 134
        Me.txt_t0G1Dalle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_t0G2Dalle
        '
        Me.txt_t0G2Dalle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_t0G2Dalle.Location = New System.Drawing.Point(523, 119)
        Me.txt_t0G2Dalle.Name = "txt_t0G2Dalle"
        Me.txt_t0G2Dalle.Size = New System.Drawing.Size(58, 20)
        Me.txt_t0G2Dalle.TabIndex = 136
        Me.txt_t0G2Dalle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_t0G2Enrob
        '
        Me.txt_t0G2Enrob.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_t0G2Enrob.Location = New System.Drawing.Point(612, 119)
        Me.txt_t0G2Enrob.Name = "txt_t0G2Enrob"
        Me.txt_t0G2Enrob.Size = New System.Drawing.Size(58, 20)
        Me.txt_t0G2Enrob.TabIndex = 135
        Me.txt_t0G2Enrob.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_t0SHDalle
        '
        Me.txt_t0SHDalle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_t0SHDalle.Location = New System.Drawing.Point(523, 141)
        Me.txt_t0SHDalle.Name = "txt_t0SHDalle"
        Me.txt_t0SHDalle.Size = New System.Drawing.Size(58, 20)
        Me.txt_t0SHDalle.TabIndex = 138
        Me.txt_t0SHDalle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_t0SHEnrob
        '
        Me.txt_t0SHEnrob.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_t0SHEnrob.Location = New System.Drawing.Point(612, 141)
        Me.txt_t0SHEnrob.Name = "txt_t0SHEnrob"
        Me.txt_t0SHEnrob.Size = New System.Drawing.Size(58, 20)
        Me.txt_t0SHEnrob.TabIndex = 137
        Me.txt_t0SHEnrob.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitJour1
        '
        Me.etq_UnitJour1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitJour1.AutoSize = True
        Me.etq_UnitJour1.Location = New System.Drawing.Point(587, 100)
        Me.etq_UnitJour1.Name = "etq_UnitJour1"
        Me.etq_UnitJour1.Size = New System.Drawing.Size(9, 13)
        Me.etq_UnitJour1.TabIndex = 139
        Me.etq_UnitJour1.Text = "j"
        '
        'etq_UnitJour3
        '
        Me.etq_UnitJour3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitJour3.AutoSize = True
        Me.etq_UnitJour3.Location = New System.Drawing.Point(587, 122)
        Me.etq_UnitJour3.Name = "etq_UnitJour3"
        Me.etq_UnitJour3.Size = New System.Drawing.Size(9, 13)
        Me.etq_UnitJour3.TabIndex = 140
        Me.etq_UnitJour3.Text = "j"
        '
        'etq_UnitJour5
        '
        Me.etq_UnitJour5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitJour5.AutoSize = True
        Me.etq_UnitJour5.Location = New System.Drawing.Point(587, 144)
        Me.etq_UnitJour5.Name = "etq_UnitJour5"
        Me.etq_UnitJour5.Size = New System.Drawing.Size(9, 13)
        Me.etq_UnitJour5.TabIndex = 141
        Me.etq_UnitJour5.Text = "j"
        '
        'etq_UnitJour6
        '
        Me.etq_UnitJour6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitJour6.AutoSize = True
        Me.etq_UnitJour6.Location = New System.Drawing.Point(674, 144)
        Me.etq_UnitJour6.Name = "etq_UnitJour6"
        Me.etq_UnitJour6.Size = New System.Drawing.Size(9, 13)
        Me.etq_UnitJour6.TabIndex = 144
        Me.etq_UnitJour6.Text = "j"
        '
        'etq_UnitJour4
        '
        Me.etq_UnitJour4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitJour4.AutoSize = True
        Me.etq_UnitJour4.Location = New System.Drawing.Point(674, 122)
        Me.etq_UnitJour4.Name = "etq_UnitJour4"
        Me.etq_UnitJour4.Size = New System.Drawing.Size(9, 13)
        Me.etq_UnitJour4.TabIndex = 143
        Me.etq_UnitJour4.Text = "j"
        '
        'etq_UnitJour2
        '
        Me.etq_UnitJour2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitJour2.AutoSize = True
        Me.etq_UnitJour2.Location = New System.Drawing.Point(674, 100)
        Me.etq_UnitJour2.Name = "etq_UnitJour2"
        Me.etq_UnitJour2.Size = New System.Drawing.Size(9, 13)
        Me.etq_UnitJour2.TabIndex = 142
        Me.etq_UnitJour2.Text = "j"
        '
        'lbl_G1
        '
        Me.lbl_G1.AutoSize = True
        Me.lbl_G1.Location = New System.Drawing.Point(237, 100)
        Me.lbl_G1.Name = "lbl_G1"
        Me.lbl_G1.Size = New System.Drawing.Size(37, 13)
        Me.lbl_G1.TabIndex = 145
        Me.lbl_G1.Text = "lbl_G1"
        '
        'lbl_G2
        '
        Me.lbl_G2.AutoSize = True
        Me.lbl_G2.Location = New System.Drawing.Point(237, 122)
        Me.lbl_G2.Name = "lbl_G2"
        Me.lbl_G2.Size = New System.Drawing.Size(37, 13)
        Me.lbl_G2.TabIndex = 146
        Me.lbl_G2.Text = "lbl_G2"
        '
        'lbl_SH
        '
        Me.lbl_SH.AutoSize = True
        Me.lbl_SH.Location = New System.Drawing.Point(237, 144)
        Me.lbl_SH.Name = "lbl_SH"
        Me.lbl_SH.Size = New System.Drawing.Size(38, 13)
        Me.lbl_SH.TabIndex = 147
        Me.lbl_SH.Text = "lbl_SH"
        '
        'img_T0G1
        '
        Me.img_T0G1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_T0G1.Location = New System.Drawing.Point(476, 97)
        Me.img_T0G1.Name = "img_T0G1"
        Me.img_T0G1.Size = New System.Drawing.Size(46, 20)
        Me.img_T0G1.TabIndex = 148
        Me.img_T0G1.TabStop = False
        '
        'img_T0G2
        '
        Me.img_T0G2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_T0G2.Location = New System.Drawing.Point(476, 119)
        Me.img_T0G2.Name = "img_T0G2"
        Me.img_T0G2.Size = New System.Drawing.Size(46, 20)
        Me.img_T0G2.TabIndex = 149
        Me.img_T0G2.TabStop = False
        '
        'img_T0SH
        '
        Me.img_T0SH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_T0SH.Location = New System.Drawing.Point(476, 141)
        Me.img_T0SH.Name = "img_T0SH"
        Me.img_T0SH.Size = New System.Drawing.Size(46, 20)
        Me.img_T0SH.TabIndex = 150
        Me.img_T0SH.TabStop = False
        '
        'Frm_OptionsCalculCalcul
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 746)
        Me.Controls.Add(Me.pan_Calcul)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsCalculCalcul"
        Me.Text = "Frm_OptionsCalculCalcul"
        Me.pan_Calcul.ResumeLayout(False)
        Me.TLpan_Conteneur.ResumeLayout(False)
        Me.pan_Conteneur.ResumeLayout(False)
        Me.pan_Conteneur.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.img_NbNodes2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_NbNodes1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_dNodes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.img_Es, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.img_PsiLSH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_PsiLG, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_T0G1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_T0G2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_T0SH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Calcul As Panel
    Friend WithEvents TLpan_Conteneur As TableLayoutPanel
    Friend WithEvents pan_Conteneur As Panel
    Friend WithEvents lbl_Norme As Label
    Friend WithEvents lbl_YoungRebars As Label
    Friend WithEvents etq_UnitEs As Label
    Friend WithEvents txt_Es As TextBox
    Friend WithEvents img_Es As PictureBox
    Friend WithEvents lbl_Calcul As Label
    Friend WithEvents cmb_Norme As ComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl_CrossSectionProperties As Label
    Friend WithEvents chk_RebarsInCompression As CheckBox
    Friend WithEvents chk_SimplifiedEffectiveW As CheckBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents etq_UnitL1 As Label
    Friend WithEvents txt_EspNoeuds As TextBox
    Friend WithEvents img_dNodes As PictureBox
    Friend WithEvents lbl_DistanceMaxNoeuds As Label
    Friend WithEvents lbl_Discretisation As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txt_NbMiniNConsole As TextBox
    Friend WithEvents img_NbNodes2 As PictureBox
    Friend WithEvents txt_NbMiniNTravee As TextBox
    Friend WithEvents img_NbNodes1 As PictureBox
    Friend WithEvents lbl_Console As Label
    Friend WithEvents lbl_TraveesI As Label
    Friend WithEvents lbl_NbNodes As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents Panel3 As Panel
    Friend WithEvents txt_PsiLSH As TextBox
    Friend WithEvents img_PsiLSH As PictureBox
    Friend WithEvents lbl_ShrinkageLC As Label
    Friend WithEvents lbl_PermanentLC As Label
    Friend WithEvents txt_PsiLG As TextBox
    Friend WithEvents img_PsiLG As PictureBox
    Friend WithEvents lbl_CreepMultiplier As Label
    Friend WithEvents lbl_LoadCases As Label
    Friend WithEvents lbl_TimeT0 As Label
    Friend WithEvents lbl_SH As Label
    Friend WithEvents lbl_G2 As Label
    Friend WithEvents lbl_G1 As Label
    Friend WithEvents etq_UnitJour6 As Label
    Friend WithEvents etq_UnitJour4 As Label
    Friend WithEvents etq_UnitJour2 As Label
    Friend WithEvents etq_UnitJour5 As Label
    Friend WithEvents etq_UnitJour3 As Label
    Friend WithEvents etq_UnitJour1 As Label
    Friend WithEvents txt_t0SHDalle As TextBox
    Friend WithEvents txt_t0SHEnrob As TextBox
    Friend WithEvents txt_t0G2Dalle As TextBox
    Friend WithEvents txt_t0G2Enrob As TextBox
    Friend WithEvents txt_t0G1Dalle As TextBox
    Friend WithEvents txt_t0G1Enrob As TextBox
    Friend WithEvents lbl_Enrobage As Label
    Friend WithEvents lbl_Dalle As Label
    Friend WithEvents img_T0SH As PictureBox
    Friend WithEvents img_T0G2 As PictureBox
    Friend WithEvents img_T0G1 As PictureBox
End Class
