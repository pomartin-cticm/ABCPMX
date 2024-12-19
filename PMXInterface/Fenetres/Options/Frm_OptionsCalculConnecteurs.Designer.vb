<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_OptionsCalculConnecteurs
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
        Me.TLpan_Conteneur = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Conteneur = New System.Windows.Forms.Panel()
        Me.pan_Dimensions = New System.Windows.Forms.Panel()
        Me.lbl_Dimensions = New System.Windows.Forms.Label()
        Me.etq_UnitD2 = New System.Windows.Forms.Label()
        Me.etq_UnitD1 = New System.Windows.Forms.Label()
        Me.lbl_DiameterMax = New System.Windows.Forms.Label()
        Me.txt_DMax1 = New System.Windows.Forms.TextBox()
        Me.lbl_DiameterMin = New System.Windows.Forms.Label()
        Me.img_DMax1 = New System.Windows.Forms.PictureBox()
        Me.txt_DMin = New System.Windows.Forms.TextBox()
        Me.img_Dmin = New System.Windows.Forms.PictureBox()
        Me.lbl_Connecteurs = New System.Windows.Forms.Label()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Conteneur.SuspendLayout()
        Me.pan_Conteneur.SuspendLayout()
        Me.pan_Dimensions.SuspendLayout()
        CType(Me.img_DMax1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Dmin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.AutoScroll = True
        Me.pan_General.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_General.Controls.Add(Me.TLpan_Conteneur)
        Me.pan_General.Location = New System.Drawing.Point(193, 24)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(739, 658)
        Me.pan_General.TabIndex = 3
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
        Me.TLpan_Conteneur.Size = New System.Drawing.Size(739, 292)
        Me.TLpan_Conteneur.TabIndex = 0
        '
        'pan_Conteneur
        '
        Me.pan_Conteneur.Controls.Add(Me.pan_Dimensions)
        Me.pan_Conteneur.Controls.Add(Me.lbl_Connecteurs)
        Me.pan_Conteneur.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Conteneur.Location = New System.Drawing.Point(0, 0)
        Me.pan_Conteneur.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Conteneur.Name = "pan_Conteneur"
        Me.pan_Conteneur.Size = New System.Drawing.Size(739, 292)
        Me.pan_Conteneur.TabIndex = 0
        '
        'pan_Dimensions
        '
        Me.pan_Dimensions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Dimensions.Controls.Add(Me.lbl_Dimensions)
        Me.pan_Dimensions.Controls.Add(Me.etq_UnitD2)
        Me.pan_Dimensions.Controls.Add(Me.etq_UnitD1)
        Me.pan_Dimensions.Controls.Add(Me.lbl_DiameterMax)
        Me.pan_Dimensions.Controls.Add(Me.txt_DMax1)
        Me.pan_Dimensions.Controls.Add(Me.lbl_DiameterMin)
        Me.pan_Dimensions.Controls.Add(Me.img_DMax1)
        Me.pan_Dimensions.Controls.Add(Me.txt_DMin)
        Me.pan_Dimensions.Controls.Add(Me.img_Dmin)
        Me.pan_Dimensions.Location = New System.Drawing.Point(3, 29)
        Me.pan_Dimensions.Name = "pan_Dimensions"
        Me.pan_Dimensions.Size = New System.Drawing.Size(732, 158)
        Me.pan_Dimensions.TabIndex = 105
        '
        'lbl_Dimensions
        '
        Me.lbl_Dimensions.AutoSize = True
        Me.lbl_Dimensions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Dimensions.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_Dimensions.Location = New System.Drawing.Point(5, 5)
        Me.lbl_Dimensions.Name = "lbl_Dimensions"
        Me.lbl_Dimensions.Size = New System.Drawing.Size(132, 13)
        Me.lbl_Dimensions.TabIndex = 104
        Me.lbl_Dimensions.Text = "lbl_CrossSectionProperties"
        Me.lbl_Dimensions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'etq_UnitD2
        '
        Me.etq_UnitD2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitD2.AutoSize = True
        Me.etq_UnitD2.Location = New System.Drawing.Point(674, 54)
        Me.etq_UnitD2.Name = "etq_UnitD2"
        Me.etq_UnitD2.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitD2.TabIndex = 101
        Me.etq_UnitD2.Text = "Label1"
        '
        'etq_UnitD1
        '
        Me.etq_UnitD1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitD1.AutoSize = True
        Me.etq_UnitD1.Location = New System.Drawing.Point(674, 28)
        Me.etq_UnitD1.Name = "etq_UnitD1"
        Me.etq_UnitD1.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitD1.TabIndex = 101
        Me.etq_UnitD1.Text = "Label1"
        '
        'lbl_DiameterMax
        '
        Me.lbl_DiameterMax.AutoSize = True
        Me.lbl_DiameterMax.Location = New System.Drawing.Point(39, 51)
        Me.lbl_DiameterMax.Name = "lbl_DiameterMax"
        Me.lbl_DiameterMax.Size = New System.Drawing.Size(85, 13)
        Me.lbl_DiameterMax.TabIndex = 102
        Me.lbl_DiameterMax.Text = "lbl_DiameterMax"
        '
        'txt_DMax1
        '
        Me.txt_DMax1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_DMax1.Location = New System.Drawing.Point(610, 51)
        Me.txt_DMax1.Name = "txt_DMax1"
        Me.txt_DMax1.Size = New System.Drawing.Size(58, 20)
        Me.txt_DMax1.TabIndex = 99
        Me.txt_DMax1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_DiameterMin
        '
        Me.lbl_DiameterMin.AutoSize = True
        Me.lbl_DiameterMin.Location = New System.Drawing.Point(39, 28)
        Me.lbl_DiameterMin.Name = "lbl_DiameterMin"
        Me.lbl_DiameterMin.Size = New System.Drawing.Size(82, 13)
        Me.lbl_DiameterMin.TabIndex = 102
        Me.lbl_DiameterMin.Text = "lbl_DiameterMin"
        '
        'img_DMax1
        '
        Me.img_DMax1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_DMax1.Location = New System.Drawing.Point(551, 51)
        Me.img_DMax1.Name = "img_DMax1"
        Me.img_DMax1.Size = New System.Drawing.Size(59, 20)
        Me.img_DMax1.TabIndex = 100
        Me.img_DMax1.TabStop = False
        '
        'txt_DMin
        '
        Me.txt_DMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_DMin.Location = New System.Drawing.Point(610, 25)
        Me.txt_DMin.Name = "txt_DMin"
        Me.txt_DMin.Size = New System.Drawing.Size(58, 20)
        Me.txt_DMin.TabIndex = 99
        Me.txt_DMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'img_Dmin
        '
        Me.img_Dmin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Dmin.Location = New System.Drawing.Point(551, 25)
        Me.img_Dmin.Name = "img_Dmin"
        Me.img_Dmin.Size = New System.Drawing.Size(59, 20)
        Me.img_Dmin.TabIndex = 100
        Me.img_Dmin.TabStop = False
        '
        'lbl_Connecteurs
        '
        Me.lbl_Connecteurs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Connecteurs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Connecteurs.Location = New System.Drawing.Point(3, 2)
        Me.lbl_Connecteurs.Name = "lbl_Connecteurs"
        Me.lbl_Connecteurs.Size = New System.Drawing.Size(733, 23)
        Me.lbl_Connecteurs.TabIndex = 98
        Me.lbl_Connecteurs.Text = "lbl_Connecteurs"
        Me.lbl_Connecteurs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_OptionsCalculConnecteurs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1124, 734)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsCalculConnecteurs"
        Me.Text = "Frm_OptionsCalculConnecteurs"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Conteneur.ResumeLayout(False)
        Me.pan_Conteneur.ResumeLayout(False)
        Me.pan_Dimensions.ResumeLayout(False)
        Me.pan_Dimensions.PerformLayout()
        CType(Me.img_DMax1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Dmin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Conteneur As TableLayoutPanel
    Friend WithEvents pan_Conteneur As Panel
    Friend WithEvents pan_Dimensions As Panel
    Friend WithEvents lbl_Dimensions As Label
    Friend WithEvents etq_UnitD2 As Label
    Friend WithEvents etq_UnitD1 As Label
    Friend WithEvents lbl_DiameterMax As Label
    Friend WithEvents txt_DMax1 As TextBox
    Friend WithEvents lbl_DiameterMin As Label
    Friend WithEvents img_DMax1 As PictureBox
    Friend WithEvents txt_DMin As TextBox
    Friend WithEvents img_Dmin As PictureBox
    Friend WithEvents lbl_Connecteurs As Label
End Class
