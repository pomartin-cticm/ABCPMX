<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_About
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_About))
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_PartieHaute = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ImgList_logos = New System.Windows.Forms.ImageList(Me.components)
        Me.lbl_Verification = New System.Windows.Forms.Label()
        Me.LinkLabel_SiteCTICM = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_SiteAM = New System.Windows.Forms.LinkLabel()
        Me.txt_cticm = New System.Windows.Forms.TextBox()
        Me.txt_Arcelor = New System.Windows.Forms.TextBox()
        Me.pic_Arcelor = New System.Windows.Forms.PictureBox()
        Me.panel_entete = New System.Windows.Forms.Panel()
        Me.pic_cticm = New System.Windows.Forms.PictureBox()
        Me.LinkLabel_SupportAM = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_SupportCTICM = New System.Windows.Forms.LinkLabel()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_PartieHaute.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.pic_Arcelor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_cticm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(485, 468)
        Me.pan_General.TabIndex = 3
        '
        'TLpan_Main
        '
        Me.TLpan_Main.ColumnCount = 1
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Controls.Add(Me.TLPan_PartieBasse, 0, 1)
        Me.TLpan_Main.Controls.Add(Me.pan_Main, 0, 0)
        Me.TLpan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 2
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(485, 468)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 3
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 1, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 431)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(479, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(182, 3)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(114, 28)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_PartieHaute)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(479, 422)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_PartieHaute
        '
        Me.TLPan_PartieHaute.ColumnCount = 1
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieHaute.Controls.Add(Me.Panel1, 0, 0)
        Me.TLPan_PartieHaute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieHaute.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_PartieHaute.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_PartieHaute.Name = "TLPan_PartieHaute"
        Me.TLPan_PartieHaute.RowCount = 1
        Me.TLPan_PartieHaute.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 419.0!))
        Me.TLPan_PartieHaute.Size = New System.Drawing.Size(479, 422)
        Me.TLPan_PartieHaute.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Panel1.Controls.Add(Me.lbl_Verification)
        Me.Panel1.Controls.Add(Me.LinkLabel_SupportCTICM)
        Me.Panel1.Controls.Add(Me.LinkLabel_SiteCTICM)
        Me.Panel1.Controls.Add(Me.LinkLabel_SupportAM)
        Me.Panel1.Controls.Add(Me.LinkLabel_SiteAM)
        Me.Panel1.Controls.Add(Me.txt_cticm)
        Me.Panel1.Controls.Add(Me.txt_Arcelor)
        Me.Panel1.Controls.Add(Me.pic_Arcelor)
        Me.Panel1.Controls.Add(Me.panel_entete)
        Me.Panel1.Controls.Add(Me.pic_cticm)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(1, 1)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(477, 420)
        Me.Panel1.TabIndex = 5
        '
        'ImgList_logos
        '
        Me.ImgList_logos.ImageStream = CType(resources.GetObject("ImgList_logos.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgList_logos.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgList_logos.Images.SetKeyName(0, "CTICM_logo.jpg")
        Me.ImgList_logos.Images.SetKeyName(1, "ARCELORMITTAL_logo.png")
        '
        'lbl_Verification
        '
        Me.lbl_Verification.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Verification.Font = New System.Drawing.Font("Arial Narrow", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Verification.Location = New System.Drawing.Point(20, 91)
        Me.lbl_Verification.Name = "lbl_Verification"
        Me.lbl_Verification.Size = New System.Drawing.Size(435, 22)
        Me.lbl_Verification.TabIndex = 18
        Me.lbl_Verification.Text = "Verification of composite beams according to EN 1994-1-1"
        Me.lbl_Verification.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LinkLabel_SiteCTICM
        '
        Me.LinkLabel_SiteCTICM.AutoSize = True
        Me.LinkLabel_SiteCTICM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel_SiteCTICM.Location = New System.Drawing.Point(278, 375)
        Me.LinkLabel_SiteCTICM.Name = "LinkLabel_SiteCTICM"
        Me.LinkLabel_SiteCTICM.Size = New System.Drawing.Size(99, 16)
        Me.LinkLabel_SiteCTICM.TabIndex = 16
        Me.LinkLabel_SiteCTICM.TabStop = True
        Me.LinkLabel_SiteCTICM.Text = "www.cticm.com"
        '
        'LinkLabel_SiteAM
        '
        Me.LinkLabel_SiteAM.AutoSize = True
        Me.LinkLabel_SiteAM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel_SiteAM.Location = New System.Drawing.Point(12, 375)
        Me.LinkLabel_SiteAM.Name = "LinkLabel_SiteAM"
        Me.LinkLabel_SiteAM.Size = New System.Drawing.Size(202, 16)
        Me.LinkLabel_SiteAM.TabIndex = 13
        Me.LinkLabel_SiteAM.TabStop = True
        Me.LinkLabel_SiteAM.Text = "https://sections.arcelormittal.com"
        '
        'txt_cticm
        '
        Me.txt_cticm.BackColor = System.Drawing.SystemColors.Window
        Me.txt_cticm.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_cticm.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_cticm.Location = New System.Drawing.Point(281, 218)
        Me.txt_cticm.Multiline = True
        Me.txt_cticm.Name = "txt_cticm"
        Me.txt_cticm.ReadOnly = True
        Me.txt_cticm.Size = New System.Drawing.Size(170, 118)
        Me.txt_cticm.TabIndex = 14
        Me.txt_cticm.TabStop = False
        Me.txt_cticm.Text = "CTICM" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Espace Technologique" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "L'orme des merisiers" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Immeuble Apollo" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "91193 SAINT-A" &
    "UBIN" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FRANCE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Tel. +33 1 60 13 8300" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'txt_Arcelor
        '
        Me.txt_Arcelor.BackColor = System.Drawing.SystemColors.Window
        Me.txt_Arcelor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Arcelor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Arcelor.Location = New System.Drawing.Point(15, 218)
        Me.txt_Arcelor.Multiline = True
        Me.txt_Arcelor.Name = "txt_Arcelor"
        Me.txt_Arcelor.ReadOnly = True
        Me.txt_Arcelor.Size = New System.Drawing.Size(217, 85)
        Me.txt_Arcelor.TabIndex = 12
        Me.txt_Arcelor.TabStop = False
        Me.txt_Arcelor.Text = "ArcelorMittal Commercial Sections" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Technical Advisory" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "66, rue du Luxembourg" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "L-4" &
    "009 ESCH-SUR-ALZETTE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "LUXEMBOURG"
        '
        'pic_Arcelor
        '
        Me.pic_Arcelor.BackColor = System.Drawing.SystemColors.Window
        Me.pic_Arcelor.BackgroundImage = CType(resources.GetObject("pic_Arcelor.BackgroundImage"), System.Drawing.Image)
        Me.pic_Arcelor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pic_Arcelor.Location = New System.Drawing.Point(15, 129)
        Me.pic_Arcelor.Name = "pic_Arcelor"
        Me.pic_Arcelor.Size = New System.Drawing.Size(189, 73)
        Me.pic_Arcelor.TabIndex = 15
        Me.pic_Arcelor.TabStop = False
        '
        'panel_entete
        '
        Me.panel_entete.BackgroundImage = CType(resources.GetObject("panel_entete.BackgroundImage"), System.Drawing.Image)
        Me.panel_entete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel_entete.Location = New System.Drawing.Point(3, -5)
        Me.panel_entete.Name = "panel_entete"
        Me.panel_entete.Size = New System.Drawing.Size(451, 82)
        Me.panel_entete.TabIndex = 11
        '
        'pic_cticm
        '
        Me.pic_cticm.BackgroundImage = CType(resources.GetObject("pic_cticm.BackgroundImage"), System.Drawing.Image)
        Me.pic_cticm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pic_cticm.Location = New System.Drawing.Point(281, 152)
        Me.pic_cticm.Name = "pic_cticm"
        Me.pic_cticm.Size = New System.Drawing.Size(120, 50)
        Me.pic_cticm.TabIndex = 17
        Me.pic_cticm.TabStop = False
        '
        'LinkLabel_SupportAM
        '
        Me.LinkLabel_SupportAM.AutoSize = True
        Me.LinkLabel_SupportAM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel_SupportAM.Location = New System.Drawing.Point(12, 342)
        Me.LinkLabel_SupportAM.Name = "LinkLabel_SupportAM"
        Me.LinkLabel_SupportAM.Size = New System.Drawing.Size(254, 16)
        Me.LinkLabel_SupportAM.TabIndex = 13
        Me.LinkLabel_SupportAM.TabStop = True
        Me.LinkLabel_SupportAM.Text = "steligence.engineering@arcelormittal.com "
        '
        'LinkLabel_SupportCTICM
        '
        Me.LinkLabel_SupportCTICM.AutoSize = True
        Me.LinkLabel_SupportCTICM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel_SupportCTICM.Location = New System.Drawing.Point(278, 342)
        Me.LinkLabel_SupportCTICM.Name = "LinkLabel_SupportCTICM"
        Me.LinkLabel_SupportCTICM.Size = New System.Drawing.Size(176, 16)
        Me.LinkLabel_SupportCTICM.TabIndex = 16
        Me.LinkLabel_SupportCTICM.TabStop = True
        Me.LinkLabel_SupportCTICM.Text = "support.logiciels@cticm.com"
        '
        'Frm_About
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(485, 468)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Frm_About"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_About"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_PartieHaute.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.pic_Arcelor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_cticm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_PartieHaute As TableLayoutPanel
    Friend WithEvents ImgList_logos As ImageList
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl_Verification As Label
    Friend WithEvents LinkLabel_SiteCTICM As LinkLabel
    Friend WithEvents LinkLabel_SiteAM As LinkLabel
    Friend WithEvents txt_cticm As TextBox
    Friend WithEvents txt_Arcelor As TextBox
    Friend WithEvents pic_Arcelor As PictureBox
    Friend WithEvents panel_entete As Panel
    Friend WithEvents pic_cticm As PictureBox
    Friend WithEvents LinkLabel_SupportCTICM As LinkLabel
    Friend WithEvents LinkLabel_SupportAM As LinkLabel
End Class
