<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsLogicielDirectories
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
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.lbl_Repertoires = New System.Windows.Forms.Label()
        Me.pan_Rep = New System.Windows.Forms.Panel()
        Me.opt_Default = New System.Windows.Forms.RadioButton()
        Me.opt_Last = New System.Windows.Forms.RadioButton()
        Me.lbl_OptionsRep = New System.Windows.Forms.Label()
        Me.lbl_Install = New System.Windows.Forms.Label()
        Me.img_Install = New System.Windows.Forms.PictureBox()
        Me.lbl_Travail = New System.Windows.Forms.Label()
        Me.img_Travail = New System.Windows.Forms.PictureBox()
        Me.lbl_Configuration = New System.Windows.Forms.Label()
        Me.img_Configuration = New System.Windows.Forms.PictureBox()
        Me.lbl_TravailEnCours = New System.Windows.Forms.Label()
        Me.img_TravailEnCours = New System.Windows.Forms.PictureBox()
        Me.pan_General.SuspendLayout()
        Me.pan_Rep.SuspendLayout()
        CType(Me.img_Install, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Travail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_Configuration, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_TravailEnCours, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_General.Controls.Add(Me.lbl_Repertoires)
        Me.pan_General.Controls.Add(Me.pan_Rep)
        Me.pan_General.Location = New System.Drawing.Point(31, 25)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(739, 401)
        Me.pan_General.TabIndex = 3
        '
        'lbl_Repertoires
        '
        Me.lbl_Repertoires.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Repertoires.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Repertoires.Location = New System.Drawing.Point(11, 7)
        Me.lbl_Repertoires.Name = "lbl_Repertoires"
        Me.lbl_Repertoires.Size = New System.Drawing.Size(714, 23)
        Me.lbl_Repertoires.TabIndex = 85
        Me.lbl_Repertoires.Text = "lbl_Repertoires"
        Me.lbl_Repertoires.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Rep
        '
        Me.pan_Rep.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Rep.Controls.Add(Me.lbl_TravailEnCours)
        Me.pan_Rep.Controls.Add(Me.img_TravailEnCours)
        Me.pan_Rep.Controls.Add(Me.opt_Default)
        Me.pan_Rep.Controls.Add(Me.opt_Last)
        Me.pan_Rep.Controls.Add(Me.lbl_OptionsRep)
        Me.pan_Rep.Controls.Add(Me.lbl_Install)
        Me.pan_Rep.Controls.Add(Me.img_Install)
        Me.pan_Rep.Controls.Add(Me.lbl_Travail)
        Me.pan_Rep.Controls.Add(Me.img_Travail)
        Me.pan_Rep.Controls.Add(Me.lbl_Configuration)
        Me.pan_Rep.Controls.Add(Me.img_Configuration)
        Me.pan_Rep.Location = New System.Drawing.Point(11, 7)
        Me.pan_Rep.Name = "pan_Rep"
        Me.pan_Rep.Size = New System.Drawing.Size(714, 350)
        Me.pan_Rep.TabIndex = 98
        '
        'opt_Default
        '
        Me.opt_Default.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.opt_Default.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.opt_Default.Location = New System.Drawing.Point(16, 282)
        Me.opt_Default.Name = "opt_Default"
        Me.opt_Default.Size = New System.Drawing.Size(645, 20)
        Me.opt_Default.TabIndex = 87
        Me.opt_Default.TabStop = True
        Me.opt_Default.Text = "opt_Default"
        Me.opt_Default.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.opt_Default.UseVisualStyleBackColor = True
        '
        'opt_Last
        '
        Me.opt_Last.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.opt_Last.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.opt_Last.Location = New System.Drawing.Point(16, 308)
        Me.opt_Last.Name = "opt_Last"
        Me.opt_Last.Size = New System.Drawing.Size(645, 22)
        Me.opt_Last.TabIndex = 88
        Me.opt_Last.TabStop = True
        Me.opt_Last.Text = "opt_Last"
        Me.opt_Last.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.opt_Last.UseVisualStyleBackColor = True
        '
        'lbl_OptionsRep
        '
        Me.lbl_OptionsRep.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_OptionsRep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_OptionsRep.Location = New System.Drawing.Point(0, 244)
        Me.lbl_OptionsRep.Name = "lbl_OptionsRep"
        Me.lbl_OptionsRep.Size = New System.Drawing.Size(714, 23)
        Me.lbl_OptionsRep.TabIndex = 86
        Me.lbl_OptionsRep.Text = "Label1"
        Me.lbl_OptionsRep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Install
        '
        Me.lbl_Install.AutoSize = True
        Me.lbl_Install.Location = New System.Drawing.Point(13, 33)
        Me.lbl_Install.Name = "lbl_Install"
        Me.lbl_Install.Size = New System.Drawing.Size(50, 13)
        Me.lbl_Install.TabIndex = 23
        Me.lbl_Install.Text = "lbl_Install"
        Me.lbl_Install.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_Install
        '
        Me.img_Install.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Install.Location = New System.Drawing.Point(31, 53)
        Me.img_Install.Name = "img_Install"
        Me.img_Install.Size = New System.Drawing.Size(643, 21)
        Me.img_Install.TabIndex = 22
        Me.img_Install.TabStop = False
        '
        'lbl_Travail
        '
        Me.lbl_Travail.AutoSize = True
        Me.lbl_Travail.Location = New System.Drawing.Point(13, 137)
        Me.lbl_Travail.Name = "lbl_Travail"
        Me.lbl_Travail.Size = New System.Drawing.Size(55, 13)
        Me.lbl_Travail.TabIndex = 21
        Me.lbl_Travail.Text = "lbl_Travail"
        Me.lbl_Travail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_Travail
        '
        Me.img_Travail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Travail.Cursor = System.Windows.Forms.Cursors.Hand
        Me.img_Travail.Location = New System.Drawing.Point(31, 157)
        Me.img_Travail.Name = "img_Travail"
        Me.img_Travail.Size = New System.Drawing.Size(643, 21)
        Me.img_Travail.TabIndex = 20
        Me.img_Travail.TabStop = False
        '
        'lbl_Configuration
        '
        Me.lbl_Configuration.AutoSize = True
        Me.lbl_Configuration.Location = New System.Drawing.Point(13, 85)
        Me.lbl_Configuration.Name = "lbl_Configuration"
        Me.lbl_Configuration.Size = New System.Drawing.Size(85, 13)
        Me.lbl_Configuration.TabIndex = 19
        Me.lbl_Configuration.Text = "lbl_Configuration"
        Me.lbl_Configuration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_Configuration
        '
        Me.img_Configuration.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_Configuration.Location = New System.Drawing.Point(31, 105)
        Me.img_Configuration.Name = "img_Configuration"
        Me.img_Configuration.Size = New System.Drawing.Size(643, 21)
        Me.img_Configuration.TabIndex = 18
        Me.img_Configuration.TabStop = False
        '
        'lbl_TravailEnCours
        '
        Me.lbl_TravailEnCours.AutoSize = True
        Me.lbl_TravailEnCours.Location = New System.Drawing.Point(13, 189)
        Me.lbl_TravailEnCours.Name = "lbl_TravailEnCours"
        Me.lbl_TravailEnCours.Size = New System.Drawing.Size(95, 13)
        Me.lbl_TravailEnCours.TabIndex = 90
        Me.lbl_TravailEnCours.Text = "lbl_TravailEnCours"
        Me.lbl_TravailEnCours.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'img_TravailEnCours
        '
        Me.img_TravailEnCours.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_TravailEnCours.Location = New System.Drawing.Point(31, 209)
        Me.img_TravailEnCours.Name = "img_TravailEnCours"
        Me.img_TravailEnCours.Size = New System.Drawing.Size(643, 21)
        Me.img_TravailEnCours.TabIndex = 89
        Me.img_TravailEnCours.TabStop = False
        '
        'Frm_OptionsLogicielDirectories
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsLogicielDirectories"
        Me.Text = "Frm_OptionsLogicielDirectories"
        Me.pan_General.ResumeLayout(False)
        Me.pan_Rep.ResumeLayout(False)
        Me.pan_Rep.PerformLayout()
        CType(Me.img_Install, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Travail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_Configuration, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_TravailEnCours, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents lbl_Repertoires As Label
    Friend WithEvents pan_Rep As Panel
    Friend WithEvents lbl_OptionsRep As Label
    Friend WithEvents lbl_Install As Label
    Friend WithEvents img_Install As PictureBox
    Friend WithEvents lbl_Travail As Label
    Friend WithEvents img_Travail As PictureBox
    Friend WithEvents lbl_Configuration As Label
    Friend WithEvents img_Configuration As PictureBox
    Friend WithEvents opt_Default As RadioButton
    Friend WithEvents opt_Last As RadioButton
    Friend WithEvents lbl_TravailEnCours As Label
    Friend WithEvents img_TravailEnCours As PictureBox
End Class
