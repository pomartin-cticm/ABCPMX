<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ConnectionSlimConnexion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_ConnectionSlimConnexion))
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Connection = New System.Windows.Forms.Label()
        Me.pan_Saisie = New System.Windows.Forms.Panel()
        Me.pan_Commandes = New System.Windows.Forms.Panel()
        Me.img_info = New System.Windows.Forms.PictureBox()
        Me.lbl_EtaSymbol = New System.Windows.Forms.Label()
        Me.lbl_DegreConnex = New System.Windows.Forms.Label()
        Me.etq_Somme = New System.Windows.Forms.Label()
        Me.btn_Supprimer = New System.Windows.Forms.Button()
        Me.btn_Ajouter = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txt_Largeur_I3 = New System.Windows.Forms.TextBox()
        Me.txt_Largeur_I2 = New System.Windows.Forms.TextBox()
        Me.txt_Largeur_I1 = New System.Windows.Forms.TextBox()
        Me.cmb_NbRow_I3 = New System.Windows.Forms.ComboBox()
        Me.txt_EspLongi_I3 = New System.Windows.Forms.TextBox()
        Me.txt_EspLongi_I2 = New System.Windows.Forms.TextBox()
        Me.txt_EspLongi_I1 = New System.Windows.Forms.TextBox()
        Me.cmb_NbRow_I2 = New System.Windows.Forms.ComboBox()
        Me.cmb_NbRow_I1 = New System.Windows.Forms.ComboBox()
        Me.txt_EspacementLongi = New System.Windows.Forms.TextBox()
        Me.txt_NbRows = New System.Windows.Forms.TextBox()
        Me.txt_I3 = New System.Windows.Forms.TextBox()
        Me.txt_I2 = New System.Windows.Forms.TextBox()
        Me.txt_I1 = New System.Windows.Forms.TextBox()
        Me.txt_Largeur = New System.Windows.Forms.TextBox()
        Me.txt_Indice = New System.Windows.Forms.TextBox()
        Me.pan_Img = New System.Windows.Forms.Panel()
        Me.img_Connexion = New System.Windows.Forms.PictureBox()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.pan_Saisie.SuspendLayout()
        Me.pan_Commandes.SuspendLayout()
        CType(Me.img_info, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pan_Img.SuspendLayout()
        CType(Me.img_Connexion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.Controls.Add(Me.TLpan_Main)
        Me.pan_Main.Location = New System.Drawing.Point(119, 90)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(673, 285)
        Me.pan_Main.TabIndex = 0
        '
        'TLpan_Main
        '
        Me.TLpan_Main.ColumnCount = 1
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Controls.Add(Me.lbl_Connection, 0, 0)
        Me.TLpan_Main.Controls.Add(Me.pan_Saisie, 0, 1)
        Me.TLpan_Main.Controls.Add(Me.pan_Img, 0, 2)
        Me.TLpan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 3
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 135.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(673, 285)
        Me.TLpan_Main.TabIndex = 0
        '
        'lbl_Connection
        '
        Me.lbl_Connection.AutoSize = True
        Me.lbl_Connection.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Connection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Connection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Connection.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Connection.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Connection.Name = "lbl_Connection"
        Me.lbl_Connection.Size = New System.Drawing.Size(673, 30)
        Me.lbl_Connection.TabIndex = 1
        Me.lbl_Connection.Text = "lbl_Connection"
        Me.lbl_Connection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Saisie
        '
        Me.pan_Saisie.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Saisie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Saisie.Controls.Add(Me.pan_Commandes)
        Me.pan_Saisie.Controls.Add(Me.Panel2)
        Me.pan_Saisie.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Saisie.Location = New System.Drawing.Point(0, 30)
        Me.pan_Saisie.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Saisie.Name = "pan_Saisie"
        Me.pan_Saisie.Size = New System.Drawing.Size(673, 135)
        Me.pan_Saisie.TabIndex = 2
        '
        'pan_Commandes
        '
        Me.pan_Commandes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pan_Commandes.Controls.Add(Me.img_info)
        Me.pan_Commandes.Controls.Add(Me.lbl_EtaSymbol)
        Me.pan_Commandes.Controls.Add(Me.lbl_DegreConnex)
        Me.pan_Commandes.Controls.Add(Me.etq_Somme)
        Me.pan_Commandes.Controls.Add(Me.btn_Supprimer)
        Me.pan_Commandes.Controls.Add(Me.btn_Ajouter)
        Me.pan_Commandes.Location = New System.Drawing.Point(531, 10)
        Me.pan_Commandes.Name = "pan_Commandes"
        Me.pan_Commandes.Size = New System.Drawing.Size(133, 109)
        Me.pan_Commandes.TabIndex = 84
        '
        'img_info
        '
        Me.img_info.Image = CType(resources.GetObject("img_info.Image"), System.Drawing.Image)
        Me.img_info.Location = New System.Drawing.Point(104, 23)
        Me.img_info.Margin = New System.Windows.Forms.Padding(0)
        Me.img_info.Name = "img_info"
        Me.img_info.Size = New System.Drawing.Size(20, 20)
        Me.img_info.TabIndex = 80
        Me.img_info.TabStop = False
        Me.img_info.Visible = False
        '
        'lbl_EtaSymbol
        '
        Me.lbl_EtaSymbol.Font = New System.Drawing.Font("Symbol", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.lbl_EtaSymbol.Location = New System.Drawing.Point(6, 26)
        Me.lbl_EtaSymbol.Name = "lbl_EtaSymbol"
        Me.lbl_EtaSymbol.Size = New System.Drawing.Size(36, 14)
        Me.lbl_EtaSymbol.TabIndex = 79
        Me.lbl_EtaSymbol.Text = "h"
        Me.lbl_EtaSymbol.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbl_EtaSymbol.Visible = False
        '
        'lbl_DegreConnex
        '
        Me.lbl_DegreConnex.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_DegreConnex.Location = New System.Drawing.Point(38, 26)
        Me.lbl_DegreConnex.Name = "lbl_DegreConnex"
        Me.lbl_DegreConnex.Size = New System.Drawing.Size(63, 14)
        Me.lbl_DegreConnex.TabIndex = 78
        Me.lbl_DegreConnex.Text = "lbl_DegreConnex"
        Me.lbl_DegreConnex.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbl_DegreConnex.Visible = False
        '
        'etq_Somme
        '
        Me.etq_Somme.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.etq_Somme.Location = New System.Drawing.Point(3, 6)
        Me.etq_Somme.Name = "etq_Somme"
        Me.etq_Somme.Size = New System.Drawing.Size(100, 14)
        Me.etq_Somme.TabIndex = 60
        Me.etq_Somme.Text = "etq_Somme"
        Me.etq_Somme.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btn_Supprimer
        '
        Me.btn_Supprimer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Supprimer.Location = New System.Drawing.Point(5, 76)
        Me.btn_Supprimer.Name = "btn_Supprimer"
        Me.btn_Supprimer.Size = New System.Drawing.Size(96, 24)
        Me.btn_Supprimer.TabIndex = 59
        Me.btn_Supprimer.Text = "btn_Supprimer"
        Me.btn_Supprimer.UseVisualStyleBackColor = True
        '
        'btn_Ajouter
        '
        Me.btn_Ajouter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Ajouter.Location = New System.Drawing.Point(5, 46)
        Me.btn_Ajouter.Name = "btn_Ajouter"
        Me.btn_Ajouter.Size = New System.Drawing.Size(96, 24)
        Me.btn_Ajouter.TabIndex = 58
        Me.btn_Ajouter.Text = "btn_Ajouter"
        Me.btn_Ajouter.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.txt_Largeur_I3)
        Me.Panel2.Controls.Add(Me.txt_Largeur_I2)
        Me.Panel2.Controls.Add(Me.txt_Largeur_I1)
        Me.Panel2.Controls.Add(Me.cmb_NbRow_I3)
        Me.Panel2.Controls.Add(Me.txt_EspLongi_I3)
        Me.Panel2.Controls.Add(Me.txt_EspLongi_I2)
        Me.Panel2.Controls.Add(Me.txt_EspLongi_I1)
        Me.Panel2.Controls.Add(Me.cmb_NbRow_I2)
        Me.Panel2.Controls.Add(Me.cmb_NbRow_I1)
        Me.Panel2.Controls.Add(Me.txt_EspacementLongi)
        Me.Panel2.Controls.Add(Me.txt_NbRows)
        Me.Panel2.Controls.Add(Me.txt_I3)
        Me.Panel2.Controls.Add(Me.txt_I2)
        Me.Panel2.Controls.Add(Me.txt_I1)
        Me.Panel2.Controls.Add(Me.txt_Largeur)
        Me.Panel2.Controls.Add(Me.txt_Indice)
        Me.Panel2.Location = New System.Drawing.Point(164, 29)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(364, 102)
        Me.Panel2.TabIndex = 83
        '
        'txt_Largeur_I3
        '
        Me.txt_Largeur_I3.Location = New System.Drawing.Point(288, 22)
        Me.txt_Largeur_I3.Name = "txt_Largeur_I3"
        Me.txt_Largeur_I3.Size = New System.Drawing.Size(66, 20)
        Me.txt_Largeur_I3.TabIndex = 74
        Me.txt_Largeur_I3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_Largeur_I2
        '
        Me.txt_Largeur_I2.Location = New System.Drawing.Point(221, 22)
        Me.txt_Largeur_I2.Name = "txt_Largeur_I2"
        Me.txt_Largeur_I2.Size = New System.Drawing.Size(66, 20)
        Me.txt_Largeur_I2.TabIndex = 74
        Me.txt_Largeur_I2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_Largeur_I1
        '
        Me.txt_Largeur_I1.Location = New System.Drawing.Point(154, 22)
        Me.txt_Largeur_I1.Name = "txt_Largeur_I1"
        Me.txt_Largeur_I1.Size = New System.Drawing.Size(66, 20)
        Me.txt_Largeur_I1.TabIndex = 74
        Me.txt_Largeur_I1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmb_NbRow_I3
        '
        Me.cmb_NbRow_I3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbRow_I3.FormattingEnabled = True
        Me.cmb_NbRow_I3.Location = New System.Drawing.Point(288, 41)
        Me.cmb_NbRow_I3.Name = "cmb_NbRow_I3"
        Me.cmb_NbRow_I3.Size = New System.Drawing.Size(66, 21)
        Me.cmb_NbRow_I3.TabIndex = 63
        '
        'txt_EspLongi_I3
        '
        Me.txt_EspLongi_I3.Location = New System.Drawing.Point(288, 64)
        Me.txt_EspLongi_I3.Name = "txt_EspLongi_I3"
        Me.txt_EspLongi_I3.Size = New System.Drawing.Size(66, 20)
        Me.txt_EspLongi_I3.TabIndex = 74
        Me.txt_EspLongi_I3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_EspLongi_I2
        '
        Me.txt_EspLongi_I2.Location = New System.Drawing.Point(221, 64)
        Me.txt_EspLongi_I2.Name = "txt_EspLongi_I2"
        Me.txt_EspLongi_I2.Size = New System.Drawing.Size(66, 20)
        Me.txt_EspLongi_I2.TabIndex = 74
        Me.txt_EspLongi_I2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_EspLongi_I1
        '
        Me.txt_EspLongi_I1.Location = New System.Drawing.Point(154, 64)
        Me.txt_EspLongi_I1.Name = "txt_EspLongi_I1"
        Me.txt_EspLongi_I1.Size = New System.Drawing.Size(66, 20)
        Me.txt_EspLongi_I1.TabIndex = 74
        Me.txt_EspLongi_I1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cmb_NbRow_I2
        '
        Me.cmb_NbRow_I2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbRow_I2.FormattingEnabled = True
        Me.cmb_NbRow_I2.Location = New System.Drawing.Point(221, 41)
        Me.cmb_NbRow_I2.Name = "cmb_NbRow_I2"
        Me.cmb_NbRow_I2.Size = New System.Drawing.Size(66, 21)
        Me.cmb_NbRow_I2.TabIndex = 62
        '
        'cmb_NbRow_I1
        '
        Me.cmb_NbRow_I1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbRow_I1.FormattingEnabled = True
        Me.cmb_NbRow_I1.Location = New System.Drawing.Point(154, 41)
        Me.cmb_NbRow_I1.Name = "cmb_NbRow_I1"
        Me.cmb_NbRow_I1.Size = New System.Drawing.Size(66, 21)
        Me.cmb_NbRow_I1.TabIndex = 61
        '
        'txt_EspacementLongi
        '
        Me.txt_EspacementLongi.BackColor = System.Drawing.SystemColors.Window
        Me.txt_EspacementLongi.Location = New System.Drawing.Point(6, 64)
        Me.txt_EspacementLongi.Name = "txt_EspacementLongi"
        Me.txt_EspacementLongi.ReadOnly = True
        Me.txt_EspacementLongi.Size = New System.Drawing.Size(147, 20)
        Me.txt_EspacementLongi.TabIndex = 53
        Me.txt_EspacementLongi.TabStop = False
        Me.txt_EspacementLongi.Text = "txt_EspacementLongi"
        Me.txt_EspacementLongi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_NbRows
        '
        Me.txt_NbRows.BackColor = System.Drawing.SystemColors.Window
        Me.txt_NbRows.Location = New System.Drawing.Point(6, 43)
        Me.txt_NbRows.Name = "txt_NbRows"
        Me.txt_NbRows.ReadOnly = True
        Me.txt_NbRows.Size = New System.Drawing.Size(147, 20)
        Me.txt_NbRows.TabIndex = 52
        Me.txt_NbRows.TabStop = False
        Me.txt_NbRows.Text = "txt_NbRows"
        Me.txt_NbRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_I3
        '
        Me.txt_I3.BackColor = System.Drawing.SystemColors.Window
        Me.txt_I3.Location = New System.Drawing.Point(288, 1)
        Me.txt_I3.Name = "txt_I3"
        Me.txt_I3.ReadOnly = True
        Me.txt_I3.Size = New System.Drawing.Size(66, 20)
        Me.txt_I3.TabIndex = 51
        Me.txt_I3.TabStop = False
        Me.txt_I3.Text = "3"
        Me.txt_I3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_I2
        '
        Me.txt_I2.BackColor = System.Drawing.SystemColors.Window
        Me.txt_I2.Location = New System.Drawing.Point(221, 1)
        Me.txt_I2.Name = "txt_I2"
        Me.txt_I2.ReadOnly = True
        Me.txt_I2.Size = New System.Drawing.Size(66, 20)
        Me.txt_I2.TabIndex = 50
        Me.txt_I2.TabStop = False
        Me.txt_I2.Text = "2"
        Me.txt_I2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_I1
        '
        Me.txt_I1.BackColor = System.Drawing.SystemColors.Window
        Me.txt_I1.Location = New System.Drawing.Point(154, 1)
        Me.txt_I1.Name = "txt_I1"
        Me.txt_I1.ReadOnly = True
        Me.txt_I1.Size = New System.Drawing.Size(66, 20)
        Me.txt_I1.TabIndex = 49
        Me.txt_I1.TabStop = False
        Me.txt_I1.Text = "1"
        Me.txt_I1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txt_Largeur
        '
        Me.txt_Largeur.BackColor = System.Drawing.SystemColors.Window
        Me.txt_Largeur.Location = New System.Drawing.Point(6, 22)
        Me.txt_Largeur.Name = "txt_Largeur"
        Me.txt_Largeur.ReadOnly = True
        Me.txt_Largeur.Size = New System.Drawing.Size(147, 20)
        Me.txt_Largeur.TabIndex = 48
        Me.txt_Largeur.TabStop = False
        Me.txt_Largeur.Text = "txt_Largeur"
        Me.txt_Largeur.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_Indice
        '
        Me.txt_Indice.BackColor = System.Drawing.SystemColors.Window
        Me.txt_Indice.Location = New System.Drawing.Point(126, 1)
        Me.txt_Indice.Name = "txt_Indice"
        Me.txt_Indice.ReadOnly = True
        Me.txt_Indice.Size = New System.Drawing.Size(27, 20)
        Me.txt_Indice.TabIndex = 47
        Me.txt_Indice.TabStop = False
        Me.txt_Indice.Text = "i"
        Me.txt_Indice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'pan_Img
        '
        Me.pan_Img.Controls.Add(Me.img_Connexion)
        Me.pan_Img.Location = New System.Drawing.Point(0, 166)
        Me.pan_Img.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.pan_Img.Name = "pan_Img"
        Me.pan_Img.Size = New System.Drawing.Size(200, 100)
        Me.pan_Img.TabIndex = 3
        '
        'img_Connexion
        '
        Me.img_Connexion.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Connexion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Connexion.Location = New System.Drawing.Point(43, 19)
        Me.img_Connexion.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.img_Connexion.Name = "img_Connexion"
        Me.img_Connexion.Size = New System.Drawing.Size(100, 50)
        Me.img_Connexion.TabIndex = 0
        Me.img_Connexion.TabStop = False
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Frm_ConnectionSlimConnexion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(877, 507)
        Me.Controls.Add(Me.pan_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_ConnectionSlimConnexion"
        Me.Text = "Frm_ConnectionSlimConnexion"
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLpan_Main.PerformLayout()
        Me.pan_Saisie.ResumeLayout(False)
        Me.pan_Commandes.ResumeLayout(False)
        CType(Me.img_info, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pan_Img.ResumeLayout(False)
        CType(Me.img_Connexion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents lbl_Connection As Label
    Friend WithEvents pan_Saisie As Panel
    Friend WithEvents pan_Img As Panel
    Friend WithEvents img_Connexion As PictureBox
    Friend WithEvents pan_Commandes As Panel
    Friend WithEvents img_info As PictureBox
    Friend WithEvents lbl_EtaSymbol As Label
    Friend WithEvents lbl_DegreConnex As Label
    Friend WithEvents etq_Somme As Label
    Friend WithEvents btn_Supprimer As Button
    Friend WithEvents btn_Ajouter As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txt_Largeur_I3 As TextBox
    Friend WithEvents txt_Largeur_I2 As TextBox
    Friend WithEvents txt_Largeur_I1 As TextBox
    Friend WithEvents cmb_NbRow_I3 As ComboBox
    Friend WithEvents txt_EspLongi_I3 As TextBox
    Friend WithEvents txt_EspLongi_I2 As TextBox
    Friend WithEvents txt_EspLongi_I1 As TextBox
    Friend WithEvents cmb_NbRow_I2 As ComboBox
    Friend WithEvents cmb_NbRow_I1 As ComboBox
    Friend WithEvents txt_EspacementLongi As TextBox
    Friend WithEvents txt_NbRows As TextBox
    Friend WithEvents txt_I3 As TextBox
    Friend WithEvents txt_I2 As TextBox
    Friend WithEvents txt_I1 As TextBox
    Friend WithEvents txt_Largeur As TextBox
    Friend WithEvents txt_Indice As TextBox
    Friend WithEvents ErrorProvider As ErrorProvider
End Class
