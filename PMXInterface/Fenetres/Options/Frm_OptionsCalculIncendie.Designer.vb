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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.etq_UnitL1 = New System.Windows.Forms.Label()
        Me.txt_EspNoeuds = New System.Windows.Forms.TextBox()
        Me.img_dNodes = New System.Windows.Forms.PictureBox()
        Me.lbl_DistanceMaxNoeuds = New System.Windows.Forms.Label()
        Me.lbl_Parametres = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_Constantes = New System.Windows.Forms.Label()
        Me.etq_UnitEs = New System.Windows.Forms.Label()
        Me.lbl_YoungRebars = New System.Windows.Forms.Label()
        Me.txt_Es = New System.Windows.Forms.TextBox()
        Me.img_Es = New System.Windows.Forms.PictureBox()
        Me.lbl_Incendie = New System.Windows.Forms.Label()
        Me.pan_Incendie.SuspendLayout()
        Me.TLpan_Conteneur.SuspendLayout()
        Me.pan_Conteneur.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.img_dNodes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.img_Es, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pan_Conteneur.Controls.Add(Me.Label1)
        Me.pan_Conteneur.Controls.Add(Me.Label5)
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
        Me.Panel2.Controls.Add(Me.etq_UnitL1)
        Me.Panel2.Controls.Add(Me.txt_EspNoeuds)
        Me.Panel2.Controls.Add(Me.img_dNodes)
        Me.Panel2.Controls.Add(Me.lbl_DistanceMaxNoeuds)
        Me.Panel2.Controls.Add(Me.lbl_Parametres)
        Me.Panel2.Location = New System.Drawing.Point(3, 192)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(732, 134)
        Me.Panel2.TabIndex = 106
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(236, 389)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(13, 13)
        Me.Label1.TabIndex = 133
        Me.Label1.Text = "≥"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(236, 343)
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
        Me.Panel1.Controls.Add(Me.lbl_Constantes)
        Me.Panel1.Controls.Add(Me.etq_UnitEs)
        Me.Panel1.Controls.Add(Me.lbl_YoungRebars)
        Me.Panel1.Controls.Add(Me.txt_Es)
        Me.Panel1.Controls.Add(Me.img_Es)
        Me.Panel1.Location = New System.Drawing.Point(3, 30)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(732, 158)
        Me.Panel1.TabIndex = 105
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
        'etq_UnitEs
        '
        Me.etq_UnitEs.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitEs.AutoSize = True
        Me.etq_UnitEs.Location = New System.Drawing.Point(674, 26)
        Me.etq_UnitEs.Name = "etq_UnitEs"
        Me.etq_UnitEs.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitEs.TabIndex = 101
        Me.etq_UnitEs.Text = "Label1"
        '
        'lbl_YoungRebars
        '
        Me.lbl_YoungRebars.AutoSize = True
        Me.lbl_YoungRebars.Location = New System.Drawing.Point(39, 30)
        Me.lbl_YoungRebars.Name = "lbl_YoungRebars"
        Me.lbl_YoungRebars.Size = New System.Drawing.Size(88, 13)
        Me.lbl_YoungRebars.TabIndex = 102
        Me.lbl_YoungRebars.Text = "lbl_YoungRebars"
        '
        'txt_Es
        '
        Me.txt_Es.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_Es.Location = New System.Drawing.Point(610, 23)
        Me.txt_Es.Name = "txt_Es"
        Me.txt_Es.Size = New System.Drawing.Size(58, 20)
        Me.txt_Es.TabIndex = 99
        '
        'img_Es
        '
        Me.img_Es.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Es.Location = New System.Drawing.Point(564, 23)
        Me.img_Es.Name = "img_Es"
        Me.img_Es.Size = New System.Drawing.Size(46, 20)
        Me.img_Es.TabIndex = 100
        Me.img_Es.TabStop = False
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
        Me.pan_Conteneur.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.img_dNodes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.img_Es, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents etq_UnitEs As Label
    Friend WithEvents lbl_YoungRebars As Label
    Friend WithEvents txt_Es As TextBox
    Friend WithEvents img_Es As PictureBox
    Friend WithEvents lbl_Incendie As Label
End Class
