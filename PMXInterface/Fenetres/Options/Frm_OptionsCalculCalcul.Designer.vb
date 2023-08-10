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
        Me.pan_Calcul = New System.Windows.Forms.Panel()
        Me.TLpan_Conteneur = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Conteneur = New System.Windows.Forms.Panel()
        Me.cmb_Norme = New System.Windows.Forms.ComboBox()
        Me.lbl_Norme = New System.Windows.Forms.Label()
        Me.lbl_YoungRebars = New System.Windows.Forms.Label()
        Me.etq_UnitL1 = New System.Windows.Forms.Label()
        Me.txt_PorteeMini = New System.Windows.Forms.TextBox()
        Me.img_PorteeMini = New System.Windows.Forms.PictureBox()
        Me.lbl_Calcul = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_CrossSectionProperties = New System.Windows.Forms.Label()
        Me.chk_SimplifiedEffectiveW = New System.Windows.Forms.CheckBox()
        Me.chk_RebarsInCompression = New System.Windows.Forms.CheckBox()
        Me.pan_Calcul.SuspendLayout()
        Me.TLpan_Conteneur.SuspendLayout()
        Me.pan_Conteneur.SuspendLayout()
        CType(Me.img_PorteeMini, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
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
        Me.pan_Calcul.Size = New System.Drawing.Size(739, 472)
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
        Me.TLpan_Conteneur.Size = New System.Drawing.Size(739, 436)
        Me.TLpan_Conteneur.TabIndex = 0
        '
        'pan_Conteneur
        '
        Me.pan_Conteneur.Controls.Add(Me.Panel1)
        Me.pan_Conteneur.Controls.Add(Me.cmb_Norme)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Norme)
        Me.pan_Conteneur.Controls.Add(Me.etq_UnitL1)
        Me.pan_Conteneur.Controls.Add(Me.txt_PorteeMini)
        Me.pan_Conteneur.Controls.Add(Me.img_PorteeMini)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Calcul)
        Me.pan_Conteneur.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Conteneur.Location = New System.Drawing.Point(0, 0)
        Me.pan_Conteneur.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Conteneur.Name = "pan_Conteneur"
        Me.pan_Conteneur.Size = New System.Drawing.Size(739, 436)
        Me.pan_Conteneur.TabIndex = 0
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
        'lbl_YoungRebars
        '
        Me.lbl_YoungRebars.AutoSize = True
        Me.lbl_YoungRebars.Location = New System.Drawing.Point(39, 78)
        Me.lbl_YoungRebars.Name = "lbl_YoungRebars"
        Me.lbl_YoungRebars.Size = New System.Drawing.Size(88, 13)
        Me.lbl_YoungRebars.TabIndex = 102
        Me.lbl_YoungRebars.Text = "lbl_YoungRebars"
        '
        'etq_UnitL1
        '
        Me.etq_UnitL1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL1.AutoSize = True
        Me.etq_UnitL1.Location = New System.Drawing.Point(678, 326)
        Me.etq_UnitL1.Name = "etq_UnitL1"
        Me.etq_UnitL1.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitL1.TabIndex = 101
        Me.etq_UnitL1.Text = "Label1"
        '
        'txt_PorteeMini
        '
        Me.txt_PorteeMini.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PorteeMini.Location = New System.Drawing.Point(614, 323)
        Me.txt_PorteeMini.Name = "txt_PorteeMini"
        Me.txt_PorteeMini.Size = New System.Drawing.Size(58, 20)
        Me.txt_PorteeMini.TabIndex = 99
        '
        'img_PorteeMini
        '
        Me.img_PorteeMini.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PorteeMini.Location = New System.Drawing.Point(568, 323)
        Me.img_PorteeMini.Name = "img_PorteeMini"
        Me.img_PorteeMini.Size = New System.Drawing.Size(46, 20)
        Me.img_PorteeMini.TabIndex = 100
        Me.img_PorteeMini.TabStop = False
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
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.chk_RebarsInCompression)
        Me.Panel1.Controls.Add(Me.chk_SimplifiedEffectiveW)
        Me.Panel1.Controls.Add(Me.lbl_CrossSectionProperties)
        Me.Panel1.Controls.Add(Me.lbl_YoungRebars)
        Me.Panel1.Location = New System.Drawing.Point(3, 108)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(732, 158)
        Me.Panel1.TabIndex = 105
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
        'Frm_OptionsCalculCalcul
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 599)
        Me.Controls.Add(Me.pan_Calcul)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsCalculCalcul"
        Me.Text = "Frm_OptionsCalculCalcul"
        Me.pan_Calcul.ResumeLayout(False)
        Me.TLpan_Conteneur.ResumeLayout(False)
        Me.pan_Conteneur.ResumeLayout(False)
        Me.pan_Conteneur.PerformLayout()
        CType(Me.img_PorteeMini, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Calcul As Panel
    Friend WithEvents TLpan_Conteneur As TableLayoutPanel
    Friend WithEvents pan_Conteneur As Panel
    Friend WithEvents lbl_Norme As Label
    Friend WithEvents lbl_YoungRebars As Label
    Friend WithEvents etq_UnitL1 As Label
    Friend WithEvents txt_PorteeMini As TextBox
    Friend WithEvents img_PorteeMini As PictureBox
    Friend WithEvents lbl_Calcul As Label
    Friend WithEvents cmb_Norme As ComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl_CrossSectionProperties As Label
    Friend WithEvents chk_RebarsInCompression As CheckBox
    Friend WithEvents chk_SimplifiedEffectiveW As CheckBox
End Class
