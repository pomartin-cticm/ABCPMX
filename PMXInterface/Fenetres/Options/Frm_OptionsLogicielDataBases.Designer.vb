<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsLogicielDataBases
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
        Me.pan_DataBases = New System.Windows.Forms.Panel()
        Me.pan_Aciers = New System.Windows.Forms.Panel()
        Me.txt_Acier = New System.Windows.Forms.TextBox()
        Me.lbl_Acier = New System.Windows.Forms.Label()
        Me.lbl_VersionSteel = New System.Windows.Forms.Label()
        Me.lbl_ChoixSteel = New System.Windows.Forms.Label()
        Me.chk_AciersNonCompatibleEpaisseur = New System.Windows.Forms.CheckBox()
        Me.cmb_ChoixAcier = New System.Windows.Forms.ComboBox()
        Me.pan_Profiles = New System.Windows.Forms.Panel()
        Me.txt_Profiles = New System.Windows.Forms.TextBox()
        Me.lbl_Profiles = New System.Windows.Forms.Label()
        Me.lbl_VersionProfiles = New System.Windows.Forms.Label()
        Me.lbl_FiltreSoft = New System.Windows.Forms.Label()
        Me.txt_FiltreSoft = New System.Windows.Forms.TextBox()
        Me.chk_HideProfileFilter = New System.Windows.Forms.CheckBox()
        Me.lbl_UpdatedDataBases = New System.Windows.Forms.Label()
        Me.btn_EffacerFichiersWeb = New System.Windows.Forms.Button()
        Me.txt_NbFichiersWeb = New System.Windows.Forms.TextBox()
        Me.lbl_NbFichiersWeb = New System.Windows.Forms.Label()
        Me.lbl_ClickToEdit = New System.Windows.Forms.Label()
        Me.txt_Bac = New System.Windows.Forms.TextBox()
        Me.lbl_Bac = New System.Windows.Forms.Label()
        Me.txt_Studs = New System.Windows.Forms.TextBox()
        Me.lbl_Studs = New System.Windows.Forms.Label()
        Me.lbl_DataBases2 = New System.Windows.Forms.Label()
        Me.lbl_DataBases = New System.Windows.Forms.Label()
        Me.pan_DataBases.SuspendLayout()
        Me.pan_Aciers.SuspendLayout()
        Me.pan_Profiles.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_DataBases
        '
        Me.pan_DataBases.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_DataBases.Controls.Add(Me.pan_Aciers)
        Me.pan_DataBases.Controls.Add(Me.pan_Profiles)
        Me.pan_DataBases.Controls.Add(Me.lbl_UpdatedDataBases)
        Me.pan_DataBases.Controls.Add(Me.btn_EffacerFichiersWeb)
        Me.pan_DataBases.Controls.Add(Me.txt_NbFichiersWeb)
        Me.pan_DataBases.Controls.Add(Me.lbl_NbFichiersWeb)
        Me.pan_DataBases.Controls.Add(Me.lbl_ClickToEdit)
        Me.pan_DataBases.Controls.Add(Me.txt_Bac)
        Me.pan_DataBases.Controls.Add(Me.lbl_Bac)
        Me.pan_DataBases.Controls.Add(Me.txt_Studs)
        Me.pan_DataBases.Controls.Add(Me.lbl_Studs)
        Me.pan_DataBases.Controls.Add(Me.lbl_DataBases2)
        Me.pan_DataBases.Controls.Add(Me.lbl_DataBases)
        Me.pan_DataBases.Location = New System.Drawing.Point(31, 88)
        Me.pan_DataBases.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_DataBases.Name = "pan_DataBases"
        Me.pan_DataBases.Size = New System.Drawing.Size(739, 436)
        Me.pan_DataBases.TabIndex = 2
        '
        'pan_Aciers
        '
        Me.pan_Aciers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Aciers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Aciers.Controls.Add(Me.txt_Acier)
        Me.pan_Aciers.Controls.Add(Me.lbl_Acier)
        Me.pan_Aciers.Controls.Add(Me.lbl_VersionSteel)
        Me.pan_Aciers.Controls.Add(Me.lbl_ChoixSteel)
        Me.pan_Aciers.Controls.Add(Me.chk_AciersNonCompatibleEpaisseur)
        Me.pan_Aciers.Controls.Add(Me.cmb_ChoixAcier)
        Me.pan_Aciers.Location = New System.Drawing.Point(15, 174)
        Me.pan_Aciers.Name = "pan_Aciers"
        Me.pan_Aciers.Size = New System.Drawing.Size(708, 74)
        Me.pan_Aciers.TabIndex = 122
        '
        'txt_Acier
        '
        Me.txt_Acier.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_Acier.Location = New System.Drawing.Point(202, 3)
        Me.txt_Acier.Name = "txt_Acier"
        Me.txt_Acier.Size = New System.Drawing.Size(164, 20)
        Me.txt_Acier.TabIndex = 107
        '
        'lbl_Acier
        '
        Me.lbl_Acier.Location = New System.Drawing.Point(9, 3)
        Me.lbl_Acier.Name = "lbl_Acier"
        Me.lbl_Acier.Size = New System.Drawing.Size(187, 18)
        Me.lbl_Acier.TabIndex = 106
        Me.lbl_Acier.Text = "lbl_Acier"
        Me.lbl_Acier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_VersionSteel
        '
        Me.lbl_VersionSteel.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_VersionSteel.Location = New System.Drawing.Point(372, 4)
        Me.lbl_VersionSteel.Name = "lbl_VersionSteel"
        Me.lbl_VersionSteel.Size = New System.Drawing.Size(97, 17)
        Me.lbl_VersionSteel.TabIndex = 110
        Me.lbl_VersionSteel.Text = "lbl_VersionSteel"
        Me.lbl_VersionSteel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_ChoixSteel
        '
        Me.lbl_ChoixSteel.Location = New System.Drawing.Point(3, 47)
        Me.lbl_ChoixSteel.Name = "lbl_ChoixSteel"
        Me.lbl_ChoixSteel.Size = New System.Drawing.Size(193, 18)
        Me.lbl_ChoixSteel.TabIndex = 114
        Me.lbl_ChoixSteel.Text = "lbl_ChoixSteel"
        Me.lbl_ChoixSteel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chk_AciersNonCompatibleEpaisseur
        '
        Me.chk_AciersNonCompatibleEpaisseur.AutoSize = True
        Me.chk_AciersNonCompatibleEpaisseur.Location = New System.Drawing.Point(202, 27)
        Me.chk_AciersNonCompatibleEpaisseur.Name = "chk_AciersNonCompatibleEpaisseur"
        Me.chk_AciersNonCompatibleEpaisseur.Size = New System.Drawing.Size(197, 17)
        Me.chk_AciersNonCompatibleEpaisseur.TabIndex = 115
        Me.chk_AciersNonCompatibleEpaisseur.Text = "chk_AciersNonCompatibleEpaisseur"
        Me.chk_AciersNonCompatibleEpaisseur.UseVisualStyleBackColor = True
        '
        'cmb_ChoixAcier
        '
        Me.cmb_ChoixAcier.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_ChoixAcier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_ChoixAcier.FormattingEnabled = True
        Me.cmb_ChoixAcier.Location = New System.Drawing.Point(202, 47)
        Me.cmb_ChoixAcier.Name = "cmb_ChoixAcier"
        Me.cmb_ChoixAcier.Size = New System.Drawing.Size(321, 21)
        Me.cmb_ChoixAcier.TabIndex = 116
        '
        'pan_Profiles
        '
        Me.pan_Profiles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Profiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Profiles.Controls.Add(Me.txt_Profiles)
        Me.pan_Profiles.Controls.Add(Me.lbl_Profiles)
        Me.pan_Profiles.Controls.Add(Me.lbl_VersionProfiles)
        Me.pan_Profiles.Controls.Add(Me.lbl_FiltreSoft)
        Me.pan_Profiles.Controls.Add(Me.txt_FiltreSoft)
        Me.pan_Profiles.Controls.Add(Me.chk_HideProfileFilter)
        Me.pan_Profiles.Location = New System.Drawing.Point(15, 103)
        Me.pan_Profiles.Name = "pan_Profiles"
        Me.pan_Profiles.Size = New System.Drawing.Size(709, 69)
        Me.pan_Profiles.TabIndex = 121
        '
        'txt_Profiles
        '
        Me.txt_Profiles.Cursor = System.Windows.Forms.Cursors.Hand
        Me.txt_Profiles.Location = New System.Drawing.Point(202, 3)
        Me.txt_Profiles.Name = "txt_Profiles"
        Me.txt_Profiles.Size = New System.Drawing.Size(164, 20)
        Me.txt_Profiles.TabIndex = 105
        '
        'lbl_Profiles
        '
        Me.lbl_Profiles.Location = New System.Drawing.Point(9, 3)
        Me.lbl_Profiles.Name = "lbl_Profiles"
        Me.lbl_Profiles.Size = New System.Drawing.Size(187, 18)
        Me.lbl_Profiles.TabIndex = 104
        Me.lbl_Profiles.Text = "lbl_Profiles"
        Me.lbl_Profiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_VersionProfiles
        '
        Me.lbl_VersionProfiles.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_VersionProfiles.Location = New System.Drawing.Point(372, 4)
        Me.lbl_VersionProfiles.Name = "lbl_VersionProfiles"
        Me.lbl_VersionProfiles.Size = New System.Drawing.Size(97, 17)
        Me.lbl_VersionProfiles.TabIndex = 109
        Me.lbl_VersionProfiles.Text = "lbl_VersionProfiles"
        Me.lbl_VersionProfiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_FiltreSoft
        '
        Me.lbl_FiltreSoft.Location = New System.Drawing.Point(66, 29)
        Me.lbl_FiltreSoft.Name = "lbl_FiltreSoft"
        Me.lbl_FiltreSoft.Size = New System.Drawing.Size(130, 17)
        Me.lbl_FiltreSoft.TabIndex = 111
        Me.lbl_FiltreSoft.Text = "lbl_FiltreSoft"
        Me.lbl_FiltreSoft.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_FiltreSoft
        '
        Me.txt_FiltreSoft.Location = New System.Drawing.Point(202, 26)
        Me.txt_FiltreSoft.Name = "txt_FiltreSoft"
        Me.txt_FiltreSoft.Size = New System.Drawing.Size(101, 20)
        Me.txt_FiltreSoft.TabIndex = 112
        '
        'chk_HideProfileFilter
        '
        Me.chk_HideProfileFilter.AutoSize = True
        Me.chk_HideProfileFilter.Location = New System.Drawing.Point(202, 50)
        Me.chk_HideProfileFilter.Name = "chk_HideProfileFilter"
        Me.chk_HideProfileFilter.Size = New System.Drawing.Size(123, 17)
        Me.chk_HideProfileFilter.TabIndex = 113
        Me.chk_HideProfileFilter.Text = "chk_HideProfileFilter"
        Me.chk_HideProfileFilter.UseVisualStyleBackColor = True
        '
        'lbl_UpdatedDataBases
        '
        Me.lbl_UpdatedDataBases.AutoSize = True
        Me.lbl_UpdatedDataBases.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_UpdatedDataBases.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_UpdatedDataBases.Location = New System.Drawing.Point(12, 269)
        Me.lbl_UpdatedDataBases.Name = "lbl_UpdatedDataBases"
        Me.lbl_UpdatedDataBases.Size = New System.Drawing.Size(121, 13)
        Me.lbl_UpdatedDataBases.TabIndex = 120
        Me.lbl_UpdatedDataBases.Text = "etq_UpdatedDataBases"
        Me.lbl_UpdatedDataBases.Visible = False
        '
        'btn_EffacerFichiersWeb
        '
        Me.btn_EffacerFichiersWeb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_EffacerFichiersWeb.Location = New System.Drawing.Point(275, 285)
        Me.btn_EffacerFichiersWeb.Name = "btn_EffacerFichiersWeb"
        Me.btn_EffacerFichiersWeb.Size = New System.Drawing.Size(253, 23)
        Me.btn_EffacerFichiersWeb.TabIndex = 119
        Me.btn_EffacerFichiersWeb.Text = "btn_EffacerFichiersWeb"
        Me.btn_EffacerFichiersWeb.UseVisualStyleBackColor = True
        Me.btn_EffacerFichiersWeb.Visible = False
        '
        'txt_NbFichiersWeb
        '
        Me.txt_NbFichiersWeb.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_NbFichiersWeb.Location = New System.Drawing.Point(217, 287)
        Me.txt_NbFichiersWeb.Name = "txt_NbFichiersWeb"
        Me.txt_NbFichiersWeb.Size = New System.Drawing.Size(52, 20)
        Me.txt_NbFichiersWeb.TabIndex = 118
        Me.txt_NbFichiersWeb.Visible = False
        '
        'lbl_NbFichiersWeb
        '
        Me.lbl_NbFichiersWeb.Location = New System.Drawing.Point(19, 287)
        Me.lbl_NbFichiersWeb.Name = "lbl_NbFichiersWeb"
        Me.lbl_NbFichiersWeb.Size = New System.Drawing.Size(192, 19)
        Me.lbl_NbFichiersWeb.TabIndex = 117
        Me.lbl_NbFichiersWeb.Text = "etq_NbFichiersWeb"
        Me.lbl_NbFichiersWeb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbl_NbFichiersWeb.Visible = False
        '
        'lbl_ClickToEdit
        '
        Me.lbl_ClickToEdit.AutoSize = True
        Me.lbl_ClickToEdit.ForeColor = System.Drawing.Color.Blue
        Me.lbl_ClickToEdit.Location = New System.Drawing.Point(220, 35)
        Me.lbl_ClickToEdit.Name = "lbl_ClickToEdit"
        Me.lbl_ClickToEdit.Size = New System.Drawing.Size(77, 13)
        Me.lbl_ClickToEdit.TabIndex = 108
        Me.lbl_ClickToEdit.Text = "lbl_ClickToEdit"
        '
        'txt_Bac
        '
        Me.txt_Bac.Cursor = System.Windows.Forms.Cursors.Hand
        Me.txt_Bac.Location = New System.Drawing.Point(217, 78)
        Me.txt_Bac.Name = "txt_Bac"
        Me.txt_Bac.Size = New System.Drawing.Size(164, 20)
        Me.txt_Bac.TabIndex = 103
        '
        'lbl_Bac
        '
        Me.lbl_Bac.Location = New System.Drawing.Point(27, 79)
        Me.lbl_Bac.Name = "lbl_Bac"
        Me.lbl_Bac.Size = New System.Drawing.Size(184, 17)
        Me.lbl_Bac.TabIndex = 102
        Me.lbl_Bac.Text = "lbl_Bac"
        Me.lbl_Bac.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_Studs
        '
        Me.txt_Studs.Cursor = System.Windows.Forms.Cursors.Hand
        Me.txt_Studs.Location = New System.Drawing.Point(217, 57)
        Me.txt_Studs.Name = "txt_Studs"
        Me.txt_Studs.Size = New System.Drawing.Size(164, 20)
        Me.txt_Studs.TabIndex = 101
        '
        'lbl_Studs
        '
        Me.lbl_Studs.Location = New System.Drawing.Point(27, 58)
        Me.lbl_Studs.Name = "lbl_Studs"
        Me.lbl_Studs.Size = New System.Drawing.Size(184, 17)
        Me.lbl_Studs.TabIndex = 100
        Me.lbl_Studs.Text = "lbl_Studs"
        Me.lbl_Studs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_DataBases2
        '
        Me.lbl_DataBases2.AutoSize = True
        Me.lbl_DataBases2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DataBases2.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_DataBases2.Location = New System.Drawing.Point(12, 35)
        Me.lbl_DataBases2.Name = "lbl_DataBases2"
        Me.lbl_DataBases2.Size = New System.Drawing.Size(86, 13)
        Me.lbl_DataBases2.TabIndex = 99
        Me.lbl_DataBases2.Text = "etq_DataBases2"
        '
        'lbl_DataBases
        '
        Me.lbl_DataBases.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_DataBases.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_DataBases.Location = New System.Drawing.Point(3, 2)
        Me.lbl_DataBases.Name = "lbl_DataBases"
        Me.lbl_DataBases.Size = New System.Drawing.Size(733, 23)
        Me.lbl_DataBases.TabIndex = 98
        Me.lbl_DataBases.Text = "lbl_DataBases"
        Me.lbl_DataBases.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_OptionsLogicielDataBases
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 613)
        Me.Controls.Add(Me.pan_DataBases)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_OptionsLogicielDataBases"
        Me.Text = "Frm_OptionsLogicielDataBases"
        Me.pan_DataBases.ResumeLayout(False)
        Me.pan_DataBases.PerformLayout()
        Me.pan_Aciers.ResumeLayout(False)
        Me.pan_Aciers.PerformLayout()
        Me.pan_Profiles.ResumeLayout(False)
        Me.pan_Profiles.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_DataBases As Panel
    Friend WithEvents lbl_DataBases As Label
    Friend WithEvents pan_Aciers As Panel
    Friend WithEvents txt_Acier As TextBox
    Friend WithEvents lbl_Acier As Label
    Friend WithEvents lbl_VersionSteel As Label
    Friend WithEvents lbl_ChoixSteel As Label
    Friend WithEvents chk_AciersNonCompatibleEpaisseur As CheckBox
    Friend WithEvents cmb_ChoixAcier As ComboBox
    Friend WithEvents pan_Profiles As Panel
    Friend WithEvents txt_Profiles As TextBox
    Friend WithEvents lbl_Profiles As Label
    Friend WithEvents lbl_VersionProfiles As Label
    Friend WithEvents lbl_FiltreSoft As Label
    Friend WithEvents txt_FiltreSoft As TextBox
    Friend WithEvents chk_HideProfileFilter As CheckBox
    Friend WithEvents lbl_UpdatedDataBases As Label
    Friend WithEvents btn_EffacerFichiersWeb As Button
    Friend WithEvents txt_NbFichiersWeb As TextBox
    Friend WithEvents lbl_NbFichiersWeb As Label
    Friend WithEvents lbl_ClickToEdit As Label
    Friend WithEvents txt_Bac As TextBox
    Friend WithEvents lbl_Bac As Label
    Friend WithEvents txt_Studs As TextBox
    Friend WithEvents lbl_Studs As Label
    Friend WithEvents lbl_DataBases2 As Label
End Class
