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
        Me.pan_AffichageInfo = New System.Windows.Forms.Panel()
        Me.TLpan_SepHorizontal = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Entete = New System.Windows.Forms.Panel()
        Me.lbl_NomLogiciel = New System.Windows.Forms.Label()
        Me.pan_ImageEntete = New System.Windows.Forms.Panel()
        Me.lbl_Verification = New System.Windows.Forms.Label()
        Me.TLpan_SepEntreprises = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_CTICM = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.pic_cticm = New System.Windows.Forms.PictureBox()
        Me.txt_cticm = New System.Windows.Forms.TextBox()
        Me.lbk_SupportCTICM = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel_SiteCTICM = New System.Windows.Forms.LinkLabel()
        Me.Pan_ArcelorMittal = New System.Windows.Forms.Panel()
        Me.txt_Arcelor = New System.Windows.Forms.TextBox()
        Me.LinkLabel_SiteAM = New System.Windows.Forms.LinkLabel()
        Me.lbk_SupportAM = New System.Windows.Forms.LinkLabel()
        Me.pic_Arcelor = New System.Windows.Forms.PictureBox()
        Me.ImgList_logos = New System.Windows.Forms.ImageList(Me.components)
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_PartieHaute.SuspendLayout()
        Me.pan_AffichageInfo.SuspendLayout()
        Me.TLpan_SepHorizontal.SuspendLayout()
        Me.pan_Entete.SuspendLayout()
        Me.TLpan_SepEntreprises.SuspendLayout()
        Me.pan_CTICM.SuspendLayout()
        CType(Me.pic_cticm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Pan_ArcelorMittal.SuspendLayout()
        CType(Me.pic_Arcelor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(775, 613)
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
        Me.TLpan_Main.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 2
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(775, 613)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 3
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 1, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(4, 568)
        Me.TLPan_PartieBasse.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(767, 41)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(307, 4)
        Me.btn_OK.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(152, 33)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_PartieHaute)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(4, 4)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(767, 556)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_PartieHaute
        '
        Me.TLPan_PartieHaute.ColumnCount = 1
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieHaute.Controls.Add(Me.pan_AffichageInfo, 0, 0)
        Me.TLPan_PartieHaute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieHaute.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_PartieHaute.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_PartieHaute.Name = "TLPan_PartieHaute"
        Me.TLPan_PartieHaute.RowCount = 1
        Me.TLPan_PartieHaute.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 516.0!))
        Me.TLPan_PartieHaute.Size = New System.Drawing.Size(767, 556)
        Me.TLPan_PartieHaute.TabIndex = 0
        '
        'pan_AffichageInfo
        '
        Me.pan_AffichageInfo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_AffichageInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_AffichageInfo.Controls.Add(Me.TLpan_SepHorizontal)
        Me.pan_AffichageInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_AffichageInfo.Location = New System.Drawing.Point(1, 1)
        Me.pan_AffichageInfo.Margin = New System.Windows.Forms.Padding(1)
        Me.pan_AffichageInfo.Name = "pan_AffichageInfo"
        Me.pan_AffichageInfo.Size = New System.Drawing.Size(765, 554)
        Me.pan_AffichageInfo.TabIndex = 5
        '
        'TLpan_SepHorizontal
        '
        Me.TLpan_SepHorizontal.ColumnCount = 1
        Me.TLpan_SepHorizontal.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_SepHorizontal.Controls.Add(Me.pan_Entete, 0, 0)
        Me.TLpan_SepHorizontal.Controls.Add(Me.TLpan_SepEntreprises, 0, 1)
        Me.TLpan_SepHorizontal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_SepHorizontal.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_SepHorizontal.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_SepHorizontal.Name = "TLpan_SepHorizontal"
        Me.TLpan_SepHorizontal.RowCount = 2
        Me.TLpan_SepHorizontal.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 209.0!))
        Me.TLpan_SepHorizontal.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_SepHorizontal.Size = New System.Drawing.Size(763, 552)
        Me.TLpan_SepHorizontal.TabIndex = 0
        '
        'pan_Entete
        '
        Me.pan_Entete.Controls.Add(Me.lbl_NomLogiciel)
        Me.pan_Entete.Controls.Add(Me.pan_ImageEntete)
        Me.pan_Entete.Controls.Add(Me.lbl_Verification)
        Me.pan_Entete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Entete.Location = New System.Drawing.Point(0, 0)
        Me.pan_Entete.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Entete.Name = "pan_Entete"
        Me.pan_Entete.Size = New System.Drawing.Size(763, 209)
        Me.pan_Entete.TabIndex = 19
        '
        'lbl_NomLogiciel
        '
        Me.lbl_NomLogiciel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_NomLogiciel.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lbl_NomLogiciel.Location = New System.Drawing.Point(4, 174)
        Me.lbl_NomLogiciel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_NomLogiciel.Name = "lbl_NomLogiciel"
        Me.lbl_NomLogiciel.Size = New System.Drawing.Size(753, 28)
        Me.lbl_NomLogiciel.TabIndex = 19
        Me.lbl_NomLogiciel.Text = "lbl_NomLogiciel"
        Me.lbl_NomLogiciel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_ImageEntete
        '
        Me.pan_ImageEntete.BackgroundImage = CType(resources.GetObject("pan_ImageEntete.BackgroundImage"), System.Drawing.Image)
        Me.pan_ImageEntete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pan_ImageEntete.Location = New System.Drawing.Point(24, 10)
        Me.pan_ImageEntete.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_ImageEntete.Name = "pan_ImageEntete"
        Me.pan_ImageEntete.Size = New System.Drawing.Size(601, 101)
        Me.pan_ImageEntete.TabIndex = 11
        '
        'lbl_Verification
        '
        Me.lbl_Verification.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_Verification.Font = New System.Drawing.Font("Arial Narrow", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Verification.Location = New System.Drawing.Point(-1, 122)
        Me.lbl_Verification.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Verification.Name = "lbl_Verification"
        Me.lbl_Verification.Size = New System.Drawing.Size(765, 47)
        Me.lbl_Verification.TabIndex = 18
        Me.lbl_Verification.Text = "Verification of composite beams according to EN 1994-1-1"
        Me.lbl_Verification.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TLpan_SepEntreprises
        '
        Me.TLpan_SepEntreprises.ColumnCount = 2
        Me.TLpan_SepEntreprises.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_SepEntreprises.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_SepEntreprises.Controls.Add(Me.pan_CTICM, 1, 0)
        Me.TLpan_SepEntreprises.Controls.Add(Me.Pan_ArcelorMittal, 0, 0)
        Me.TLpan_SepEntreprises.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_SepEntreprises.Location = New System.Drawing.Point(0, 209)
        Me.TLpan_SepEntreprises.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_SepEntreprises.Name = "TLpan_SepEntreprises"
        Me.TLpan_SepEntreprises.RowCount = 1
        Me.TLpan_SepEntreprises.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_SepEntreprises.Size = New System.Drawing.Size(763, 343)
        Me.TLpan_SepEntreprises.TabIndex = 0
        '
        'pan_CTICM
        '
        Me.pan_CTICM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_CTICM.Controls.Add(Me.Button1)
        Me.pan_CTICM.Controls.Add(Me.pic_cticm)
        Me.pan_CTICM.Controls.Add(Me.txt_cticm)
        Me.pan_CTICM.Controls.Add(Me.lbk_SupportCTICM)
        Me.pan_CTICM.Controls.Add(Me.LinkLabel_SiteCTICM)
        Me.pan_CTICM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_CTICM.Location = New System.Drawing.Point(385, 4)
        Me.pan_CTICM.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_CTICM.Name = "pan_CTICM"
        Me.pan_CTICM.Size = New System.Drawing.Size(374, 335)
        Me.pan_CTICM.TabIndex = 19
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(256, 299)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 28)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "Test Errors"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'pic_cticm
        '
        Me.pic_cticm.BackgroundImage = CType(resources.GetObject("pic_cticm.BackgroundImage"), System.Drawing.Image)
        Me.pic_cticm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pic_cticm.Location = New System.Drawing.Point(21, 4)
        Me.pic_cticm.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pic_cticm.Name = "pic_cticm"
        Me.pic_cticm.Size = New System.Drawing.Size(335, 80)
        Me.pic_cticm.TabIndex = 17
        Me.pic_cticm.TabStop = False
        '
        'txt_cticm
        '
        Me.txt_cticm.BackColor = System.Drawing.SystemColors.Window
        Me.txt_cticm.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_cticm.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_cticm.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txt_cticm.Location = New System.Drawing.Point(21, 91)
        Me.txt_cticm.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_cticm.Multiline = True
        Me.txt_cticm.Name = "txt_cticm"
        Me.txt_cticm.ReadOnly = True
        Me.txt_cticm.Size = New System.Drawing.Size(227, 145)
        Me.txt_cticm.TabIndex = 14
        Me.txt_cticm.TabStop = False
        Me.txt_cticm.Text = "CTICM" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Espace Technologique" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "L'orme des merisiers" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Immeuble Apollo" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "91193 SAINT-A" &
    "UBIN" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "FRANCE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Tel. +33 1 60 13 8300" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'lbk_SupportCTICM
        '
        Me.lbk_SupportCTICM.AutoSize = True
        Me.lbk_SupportCTICM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbk_SupportCTICM.Location = New System.Drawing.Point(17, 240)
        Me.lbk_SupportCTICM.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbk_SupportCTICM.Name = "lbk_SupportCTICM"
        Me.lbk_SupportCTICM.Size = New System.Drawing.Size(175, 16)
        Me.lbk_SupportCTICM.TabIndex = 16
        Me.lbk_SupportCTICM.TabStop = True
        Me.lbk_SupportCTICM.Text = "support.logiciels@cticm.com"
        '
        'LinkLabel_SiteCTICM
        '
        Me.LinkLabel_SiteCTICM.AutoSize = True
        Me.LinkLabel_SiteCTICM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel_SiteCTICM.Location = New System.Drawing.Point(17, 278)
        Me.LinkLabel_SiteCTICM.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel_SiteCTICM.Name = "LinkLabel_SiteCTICM"
        Me.LinkLabel_SiteCTICM.Size = New System.Drawing.Size(98, 16)
        Me.LinkLabel_SiteCTICM.TabIndex = 16
        Me.LinkLabel_SiteCTICM.TabStop = True
        Me.LinkLabel_SiteCTICM.Text = "www.cticm.com"
        '
        'Pan_ArcelorMittal
        '
        Me.Pan_ArcelorMittal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pan_ArcelorMittal.Controls.Add(Me.txt_Arcelor)
        Me.Pan_ArcelorMittal.Controls.Add(Me.LinkLabel_SiteAM)
        Me.Pan_ArcelorMittal.Controls.Add(Me.lbk_SupportAM)
        Me.Pan_ArcelorMittal.Controls.Add(Me.pic_Arcelor)
        Me.Pan_ArcelorMittal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_ArcelorMittal.Location = New System.Drawing.Point(4, 4)
        Me.Pan_ArcelorMittal.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Pan_ArcelorMittal.Name = "Pan_ArcelorMittal"
        Me.Pan_ArcelorMittal.Size = New System.Drawing.Size(373, 335)
        Me.Pan_ArcelorMittal.TabIndex = 19
        '
        'txt_Arcelor
        '
        Me.txt_Arcelor.BackColor = System.Drawing.SystemColors.Window
        Me.txt_Arcelor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_Arcelor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Arcelor.Location = New System.Drawing.Point(20, 103)
        Me.txt_Arcelor.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txt_Arcelor.Multiline = True
        Me.txt_Arcelor.Name = "txt_Arcelor"
        Me.txt_Arcelor.ReadOnly = True
        Me.txt_Arcelor.Size = New System.Drawing.Size(289, 105)
        Me.txt_Arcelor.TabIndex = 12
        Me.txt_Arcelor.TabStop = False
        Me.txt_Arcelor.Text = "ArcelorMittal Commercial Sections" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Technical Advisory" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "66, rue du Luxembourg" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "L-4" &
    "009 ESCH-SUR-ALZETTE" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "LUXEMBOURG"
        '
        'LinkLabel_SiteAM
        '
        Me.LinkLabel_SiteAM.AutoSize = True
        Me.LinkLabel_SiteAM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel_SiteAM.Location = New System.Drawing.Point(16, 278)
        Me.LinkLabel_SiteAM.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel_SiteAM.Name = "LinkLabel_SiteAM"
        Me.LinkLabel_SiteAM.Size = New System.Drawing.Size(201, 16)
        Me.LinkLabel_SiteAM.TabIndex = 13
        Me.LinkLabel_SiteAM.TabStop = True
        Me.LinkLabel_SiteAM.Text = "https://sections.arcelormittal.com"
        '
        'lbk_SupportAM
        '
        Me.lbk_SupportAM.AutoSize = True
        Me.lbk_SupportAM.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbk_SupportAM.Location = New System.Drawing.Point(16, 240)
        Me.lbk_SupportAM.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbk_SupportAM.Name = "lbk_SupportAM"
        Me.lbk_SupportAM.Size = New System.Drawing.Size(253, 16)
        Me.lbk_SupportAM.TabIndex = 13
        Me.lbk_SupportAM.TabStop = True
        Me.lbk_SupportAM.Text = "steligence.engineering@arcelormittal.com "
        '
        'pic_Arcelor
        '
        Me.pic_Arcelor.BackColor = System.Drawing.SystemColors.Window
        Me.pic_Arcelor.BackgroundImage = CType(resources.GetObject("pic_Arcelor.BackgroundImage"), System.Drawing.Image)
        Me.pic_Arcelor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pic_Arcelor.Location = New System.Drawing.Point(73, 4)
        Me.pic_Arcelor.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pic_Arcelor.Name = "pic_Arcelor"
        Me.pic_Arcelor.Size = New System.Drawing.Size(252, 90)
        Me.pic_Arcelor.TabIndex = 15
        Me.pic_Arcelor.TabStop = False
        '
        'ImgList_logos
        '
        Me.ImgList_logos.ImageStream = CType(resources.GetObject("ImgList_logos.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgList_logos.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgList_logos.Images.SetKeyName(0, "CTICM_logo.jpg")
        Me.ImgList_logos.Images.SetKeyName(1, "ARCELORMITTAL_logo.png")
        '
        'Frm_About
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(775, 613)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_About"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_About"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_PartieHaute.ResumeLayout(False)
        Me.pan_AffichageInfo.ResumeLayout(False)
        Me.TLpan_SepHorizontal.ResumeLayout(False)
        Me.pan_Entete.ResumeLayout(False)
        Me.TLpan_SepEntreprises.ResumeLayout(False)
        Me.pan_CTICM.ResumeLayout(False)
        Me.pan_CTICM.PerformLayout()
        CType(Me.pic_cticm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Pan_ArcelorMittal.ResumeLayout(False)
        Me.Pan_ArcelorMittal.PerformLayout()
        CType(Me.pic_Arcelor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_PartieHaute As TableLayoutPanel
    Friend WithEvents ImgList_logos As ImageList
    Friend WithEvents pan_AffichageInfo As Panel
    Friend WithEvents lbl_Verification As Label
    Friend WithEvents LinkLabel_SiteCTICM As LinkLabel
    Friend WithEvents LinkLabel_SiteAM As LinkLabel
    Friend WithEvents txt_cticm As TextBox
    Friend WithEvents txt_Arcelor As TextBox
    Friend WithEvents pic_Arcelor As PictureBox
    Friend WithEvents pan_ImageEntete As Panel
    Friend WithEvents pic_cticm As PictureBox
    Friend WithEvents lbk_SupportCTICM As LinkLabel
    Friend WithEvents lbk_SupportAM As LinkLabel
    Friend WithEvents pan_CTICM As Panel
    Friend WithEvents Pan_ArcelorMittal As Panel
    Friend WithEvents TLpan_SepHorizontal As TableLayoutPanel
    Friend WithEvents pan_Entete As Panel
    Friend WithEvents TLpan_SepEntreprises As TableLayoutPanel
    Friend WithEvents Button1 As Button
    Friend WithEvents lbl_NomLogiciel As Label
End Class
