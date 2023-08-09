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
        Me.lbl_DefinitionPoutre = New System.Windows.Forms.Label()
        Me.lbl_PorteeMini = New System.Windows.Forms.Label()
        Me.etq_UnitL1 = New System.Windows.Forms.Label()
        Me.txt_PorteeMini = New System.Windows.Forms.TextBox()
        Me.img_PorteeMini = New System.Windows.Forms.PictureBox()
        Me.lbl_Scope = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.lbl_ThetaRd = New System.Windows.Forms.Label()
        Me.etq_UnitA1 = New System.Windows.Forms.Label()
        Me.txt_ThetaH = New System.Windows.Forms.TextBox()
        Me.img_ThetaRd = New System.Windows.Forms.PictureBox()
        Me.pan_Scope.SuspendLayout()
        Me.TLpan_Conteneur.SuspendLayout()
        Me.pan_Conteneur.SuspendLayout()
        CType(Me.img_PorteeMini, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ThetaRd, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.pan_Conteneur.Controls.Add(Me.lbl_ThetaRd)
        Me.pan_Conteneur.Controls.Add(Me.etq_UnitA1)
        Me.pan_Conteneur.Controls.Add(Me.txt_ThetaH)
        Me.pan_Conteneur.Controls.Add(Me.img_ThetaRd)
        Me.pan_Conteneur.Controls.Add(Me.lbl_DefinitionPoutre)
        Me.pan_Conteneur.Controls.Add(Me.lbl_PorteeMini)
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
        'lbl_PorteeMini
        '
        Me.lbl_PorteeMini.AutoSize = True
        Me.lbl_PorteeMini.Location = New System.Drawing.Point(16, 62)
        Me.lbl_PorteeMini.Name = "lbl_PorteeMini"
        Me.lbl_PorteeMini.Size = New System.Drawing.Size(73, 13)
        Me.lbl_PorteeMini.TabIndex = 102
        Me.lbl_PorteeMini.Text = "lbl_PorteeMini"
        '
        'etq_UnitL1
        '
        Me.etq_UnitL1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitL1.AutoSize = True
        Me.etq_UnitL1.Location = New System.Drawing.Point(652, 58)
        Me.etq_UnitL1.Name = "etq_UnitL1"
        Me.etq_UnitL1.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitL1.TabIndex = 101
        Me.etq_UnitL1.Text = "Label1"
        '
        'txt_PorteeMini
        '
        Me.txt_PorteeMini.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_PorteeMini.Location = New System.Drawing.Point(588, 55)
        Me.txt_PorteeMini.Name = "txt_PorteeMini"
        Me.txt_PorteeMini.Size = New System.Drawing.Size(58, 20)
        Me.txt_PorteeMini.TabIndex = 99
        '
        'img_PorteeMini
        '
        Me.img_PorteeMini.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PorteeMini.Location = New System.Drawing.Point(542, 55)
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
        'lbl_ThetaRd
        '
        Me.lbl_ThetaRd.AutoSize = True
        Me.lbl_ThetaRd.Location = New System.Drawing.Point(16, 416)
        Me.lbl_ThetaRd.Name = "lbl_ThetaRd"
        Me.lbl_ThetaRd.Size = New System.Drawing.Size(65, 13)
        Me.lbl_ThetaRd.TabIndex = 107
        Me.lbl_ThetaRd.Text = "lbl_ThetaRd"
        '
        'etq_UnitA1
        '
        Me.etq_UnitA1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitA1.AutoSize = True
        Me.etq_UnitA1.Location = New System.Drawing.Point(652, 412)
        Me.etq_UnitA1.Name = "etq_UnitA1"
        Me.etq_UnitA1.Size = New System.Drawing.Size(39, 13)
        Me.etq_UnitA1.TabIndex = 106
        Me.etq_UnitA1.Text = "Label1"
        '
        'txt_ThetaH
        '
        Me.txt_ThetaH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_ThetaH.Location = New System.Drawing.Point(588, 409)
        Me.txt_ThetaH.Name = "txt_ThetaH"
        Me.txt_ThetaH.Size = New System.Drawing.Size(58, 20)
        Me.txt_ThetaH.TabIndex = 104
        '
        'img_ThetaRd
        '
        Me.img_ThetaRd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ThetaRd.Location = New System.Drawing.Point(542, 409)
        Me.img_ThetaRd.Name = "img_ThetaRd"
        Me.img_ThetaRd.Size = New System.Drawing.Size(46, 20)
        Me.img_ThetaRd.TabIndex = 105
        Me.img_ThetaRd.TabStop = False
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
        CType(Me.img_PorteeMini, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ThetaRd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Scope As Panel
    Friend WithEvents TLpan_Conteneur As TableLayoutPanel
    Friend WithEvents pan_Conteneur As Panel
    Friend WithEvents lbl_Scope As Label
    Friend WithEvents lbl_PorteeMini As Label
    Friend WithEvents etq_UnitL1 As Label
    Friend WithEvents txt_PorteeMini As TextBox
    Friend WithEvents img_PorteeMini As PictureBox
    Friend WithEvents lbl_DefinitionPoutre As Label
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents lbl_ThetaRd As Label
    Friend WithEvents etq_UnitA1 As Label
    Friend WithEvents txt_ThetaH As TextBox
    Friend WithEvents img_ThetaRd As PictureBox
End Class
