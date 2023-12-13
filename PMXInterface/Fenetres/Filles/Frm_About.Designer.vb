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
        Me.TLPan_Information = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Information = New System.Windows.Forms.Label()
        Me.pan_SaisiePortee = New System.Windows.Forms.Panel()
        Me.lbl_Support = New System.Windows.Forms.Label()
        Me.lbl_Year = New System.Windows.Forms.Label()
        Me.lbl_Version = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TLPan_Description = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Description = New System.Windows.Forms.Label()
        Me.pan_SaisieDescription = New System.Windows.Forms.Panel()
        Me.lbl_DescriptionContain = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TLPan_Maitre = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Maitre = New System.Windows.Forms.Label()
        Me.pan_SaisieMaitre = New System.Windows.Forms.Panel()
        Me.lbl_Copyrights = New System.Windows.Forms.Label()
        Me.Pan_logo = New System.Windows.Forms.Panel()
        Me.img_CTICM = New System.Windows.Forms.PictureBox()
        Me.img_AM = New System.Windows.Forms.PictureBox()
        Me.ImgList_logos = New System.Windows.Forms.ImageList(Me.components)
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_PartieHaute.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TLPan_Information.SuspendLayout()
        Me.pan_SaisiePortee.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TLPan_Description.SuspendLayout()
        Me.pan_SaisieDescription.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TLPan_Maitre.SuspendLayout()
        Me.pan_SaisieMaitre.SuspendLayout()
        Me.Pan_logo.SuspendLayout()
        CType(Me.img_CTICM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_AM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(464, 352)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(464, 352)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 315)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(458, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(172, 3)
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
        Me.pan_Main.Size = New System.Drawing.Size(458, 306)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_PartieHaute
        '
        Me.TLPan_PartieHaute.ColumnCount = 2
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieHaute.Controls.Add(Me.Panel1, 0, 0)
        Me.TLPan_PartieHaute.Controls.Add(Me.Panel2, 1, 0)
        Me.TLPan_PartieHaute.Controls.Add(Me.Panel3, 1, 1)
        Me.TLPan_PartieHaute.Controls.Add(Me.Pan_logo, 0, 1)
        Me.TLPan_PartieHaute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieHaute.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_PartieHaute.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_PartieHaute.Name = "TLPan_PartieHaute"
        Me.TLPan_PartieHaute.RowCount = 2
        Me.TLPan_PartieHaute.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130.0!))
        Me.TLPan_PartieHaute.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieHaute.Size = New System.Drawing.Size(458, 306)
        Me.TLPan_PartieHaute.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TLPan_Information)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(1, 1)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(227, 128)
        Me.Panel1.TabIndex = 5
        '
        'TLPan_Information
        '
        Me.TLPan_Information.ColumnCount = 1
        Me.TLPan_Information.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Information.Controls.Add(Me.lbl_Information, 0, 0)
        Me.TLPan_Information.Controls.Add(Me.pan_SaisiePortee, 0, 1)
        Me.TLPan_Information.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Information.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Information.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Information.Name = "TLPan_Information"
        Me.TLPan_Information.RowCount = 2
        Me.TLPan_Information.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Information.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Information.Size = New System.Drawing.Size(227, 128)
        Me.TLPan_Information.TabIndex = 0
        '
        'lbl_Information
        '
        Me.lbl_Information.AutoSize = True
        Me.lbl_Information.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Information.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Information.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Information.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Information.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Information.Name = "lbl_Information"
        Me.lbl_Information.Size = New System.Drawing.Size(227, 30)
        Me.lbl_Information.TabIndex = 0
        Me.lbl_Information.Text = "lbl_Information"
        Me.lbl_Information.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisiePortee
        '
        Me.pan_SaisiePortee.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisiePortee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_Support)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_Year)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_Version)
        Me.pan_SaisiePortee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisiePortee.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisiePortee.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisiePortee.Name = "pan_SaisiePortee"
        Me.pan_SaisiePortee.Size = New System.Drawing.Size(227, 98)
        Me.pan_SaisiePortee.TabIndex = 1
        '
        'lbl_Support
        '
        Me.lbl_Support.AutoSize = True
        Me.lbl_Support.Location = New System.Drawing.Point(27, 72)
        Me.lbl_Support.Name = "lbl_Support"
        Me.lbl_Support.Size = New System.Drawing.Size(60, 13)
        Me.lbl_Support.TabIndex = 0
        Me.lbl_Support.Text = "lbl_Support"
        '
        'lbl_Year
        '
        Me.lbl_Year.AutoSize = True
        Me.lbl_Year.Location = New System.Drawing.Point(27, 42)
        Me.lbl_Year.Name = "lbl_Year"
        Me.lbl_Year.Size = New System.Drawing.Size(45, 13)
        Me.lbl_Year.TabIndex = 0
        Me.lbl_Year.Text = "lbl_Year"
        '
        'lbl_Version
        '
        Me.lbl_Version.AutoSize = True
        Me.lbl_Version.Location = New System.Drawing.Point(27, 11)
        Me.lbl_Version.Name = "lbl_Version"
        Me.lbl_Version.Size = New System.Drawing.Size(58, 13)
        Me.lbl_Version.TabIndex = 0
        Me.lbl_Version.Text = "lbl_Version"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TLPan_Description)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(229, 1)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0, 1, 1, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(228, 128)
        Me.Panel2.TabIndex = 6
        '
        'TLPan_Description
        '
        Me.TLPan_Description.ColumnCount = 1
        Me.TLPan_Description.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Description.Controls.Add(Me.lbl_Description, 0, 0)
        Me.TLPan_Description.Controls.Add(Me.pan_SaisieDescription, 0, 1)
        Me.TLPan_Description.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Description.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Description.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Description.Name = "TLPan_Description"
        Me.TLPan_Description.RowCount = 2
        Me.TLPan_Description.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Description.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Description.Size = New System.Drawing.Size(228, 128)
        Me.TLPan_Description.TabIndex = 2
        '
        'lbl_Description
        '
        Me.lbl_Description.AutoSize = True
        Me.lbl_Description.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Description.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Description.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Description.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Description.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Description.Name = "lbl_Description"
        Me.lbl_Description.Size = New System.Drawing.Size(228, 30)
        Me.lbl_Description.TabIndex = 0
        Me.lbl_Description.Text = "lbl_Description"
        Me.lbl_Description.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieDescription
        '
        Me.pan_SaisieDescription.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieDescription.Controls.Add(Me.lbl_DescriptionContain)
        Me.pan_SaisieDescription.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieDescription.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisieDescription.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisieDescription.Name = "pan_SaisieDescription"
        Me.pan_SaisieDescription.Size = New System.Drawing.Size(228, 98)
        Me.pan_SaisieDescription.TabIndex = 1
        '
        'lbl_DescriptionContain
        '
        Me.lbl_DescriptionContain.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_DescriptionContain.Location = New System.Drawing.Point(0, 0)
        Me.lbl_DescriptionContain.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_DescriptionContain.Name = "lbl_DescriptionContain"
        Me.lbl_DescriptionContain.Size = New System.Drawing.Size(226, 97)
        Me.lbl_DescriptionContain.TabIndex = 2
        Me.lbl_DescriptionContain.Text = "lbl_DescriptionContain"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.TLPan_Maitre)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(229, 130)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0, 0, 1, 1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(228, 175)
        Me.Panel3.TabIndex = 7
        '
        'TLPan_Maitre
        '
        Me.TLPan_Maitre.ColumnCount = 1
        Me.TLPan_Maitre.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Maitre.Controls.Add(Me.lbl_Maitre, 0, 0)
        Me.TLPan_Maitre.Controls.Add(Me.pan_SaisieMaitre, 0, 1)
        Me.TLPan_Maitre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Maitre.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Maitre.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Maitre.Name = "TLPan_Maitre"
        Me.TLPan_Maitre.RowCount = 2
        Me.TLPan_Maitre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Maitre.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Maitre.Size = New System.Drawing.Size(228, 175)
        Me.TLPan_Maitre.TabIndex = 3
        '
        'lbl_Maitre
        '
        Me.lbl_Maitre.AutoSize = True
        Me.lbl_Maitre.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Maitre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Maitre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Maitre.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Maitre.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Maitre.Name = "lbl_Maitre"
        Me.lbl_Maitre.Size = New System.Drawing.Size(228, 30)
        Me.lbl_Maitre.TabIndex = 0
        Me.lbl_Maitre.Text = "lbl_Maitre"
        Me.lbl_Maitre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisieMaitre
        '
        Me.pan_SaisieMaitre.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisieMaitre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisieMaitre.Controls.Add(Me.lbl_Copyrights)
        Me.pan_SaisieMaitre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisieMaitre.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisieMaitre.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisieMaitre.Name = "pan_SaisieMaitre"
        Me.pan_SaisieMaitre.Size = New System.Drawing.Size(228, 145)
        Me.pan_SaisieMaitre.TabIndex = 1
        '
        'lbl_Copyrights
        '
        Me.lbl_Copyrights.Dock = System.Windows.Forms.DockStyle.Top
        Me.lbl_Copyrights.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Copyrights.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Copyrights.Name = "lbl_Copyrights"
        Me.lbl_Copyrights.Size = New System.Drawing.Size(226, 143)
        Me.lbl_Copyrights.TabIndex = 3
        Me.lbl_Copyrights.Text = "lbl_Copyrights"
        '
        'Pan_logo
        '
        Me.Pan_logo.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.Pan_logo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Pan_logo.Controls.Add(Me.img_CTICM)
        Me.Pan_logo.Controls.Add(Me.img_AM)
        Me.Pan_logo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_logo.Location = New System.Drawing.Point(1, 130)
        Me.Pan_logo.Margin = New System.Windows.Forms.Padding(1, 0, 1, 1)
        Me.Pan_logo.Name = "Pan_logo"
        Me.Pan_logo.Size = New System.Drawing.Size(227, 175)
        Me.Pan_logo.TabIndex = 4
        '
        'img_CTICM
        '
        Me.img_CTICM.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_CTICM.Image = CType(resources.GetObject("img_CTICM.Image"), System.Drawing.Image)
        Me.img_CTICM.InitialImage = CType(resources.GetObject("img_CTICM.InitialImage"), System.Drawing.Image)
        Me.img_CTICM.Location = New System.Drawing.Point(120, 57)
        Me.img_CTICM.Margin = New System.Windows.Forms.Padding(0)
        Me.img_CTICM.Name = "img_CTICM"
        Me.img_CTICM.Size = New System.Drawing.Size(76, 43)
        Me.img_CTICM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.img_CTICM.TabIndex = 4
        Me.img_CTICM.TabStop = False
        '
        'img_AM
        '
        Me.img_AM.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_AM.Image = CType(resources.GetObject("img_AM.Image"), System.Drawing.Image)
        Me.img_AM.Location = New System.Drawing.Point(0, 0)
        Me.img_AM.Margin = New System.Windows.Forms.Padding(0)
        Me.img_AM.Name = "img_AM"
        Me.img_AM.Size = New System.Drawing.Size(76, 43)
        Me.img_AM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.img_AM.TabIndex = 4
        Me.img_AM.TabStop = False
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
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(464, 352)
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
        Me.TLPan_Information.ResumeLayout(False)
        Me.TLPan_Information.PerformLayout()
        Me.pan_SaisiePortee.ResumeLayout(False)
        Me.pan_SaisiePortee.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.TLPan_Description.ResumeLayout(False)
        Me.TLPan_Description.PerformLayout()
        Me.pan_SaisieDescription.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.TLPan_Maitre.ResumeLayout(False)
        Me.TLPan_Maitre.PerformLayout()
        Me.pan_SaisieMaitre.ResumeLayout(False)
        Me.Pan_logo.ResumeLayout(False)
        CType(Me.img_CTICM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_AM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_PartieHaute As TableLayoutPanel
    Friend WithEvents TLPan_Information As TableLayoutPanel
    Friend WithEvents lbl_Information As Label
    Friend WithEvents pan_SaisiePortee As Panel
    Friend WithEvents TLPan_Description As TableLayoutPanel
    Friend WithEvents lbl_Description As Label
    Friend WithEvents pan_SaisieDescription As Panel
    Friend WithEvents TLPan_Maitre As TableLayoutPanel
    Friend WithEvents lbl_Maitre As Label
    Friend WithEvents pan_SaisieMaitre As Panel
    Friend WithEvents img_CTICM As PictureBox
    Friend WithEvents ImgList_logos As ImageList
    Friend WithEvents Pan_logo As Panel
    Friend WithEvents img_AM As PictureBox
    Friend WithEvents lbl_DescriptionContain As Label
    Friend WithEvents lbl_Support As Label
    Friend WithEvents lbl_Year As Label
    Friend WithEvents lbl_Version As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents lbl_Copyrights As Label
End Class
