<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsLogiciel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_OptionsLogiciel))
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Contenu = New System.Windows.Forms.Panel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.PoMbtn_Expert = New PMXInterface.POMbutton()
        Me.PoMbtn_Units = New PMXInterface.POMbutton()
        Me.PoMbtn_Directories = New PMXInterface.POMbutton()
        Me.TLpan_PourLesBoutons = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.btn_Appliquer = New System.Windows.Forms.Button()
        Me.PoMBtn_General = New PMXInterface.POMbutton()
        Me.pan_Expert = New System.Windows.Forms.Panel()
        Me.lbl_ExpertMode = New System.Windows.Forms.Label()
        Me.img_Expert = New System.Windows.Forms.PictureBox()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLpan_Gauche.SuspendLayout()
        Me.TLpan_PourLesBoutons.SuspendLayout()
        Me.pan_Expert.SuspendLayout()
        CType(Me.img_Expert, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.Controls.Add(Me.TLpan_Main)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(0, 0)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(800, 399)
        Me.pan_Main.TabIndex = 0
        '
        'TLpan_Main
        '
        Me.TLpan_Main.ColumnCount = 2
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Controls.Add(Me.pan_Contenu, 0, 0)
        Me.TLpan_Main.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLpan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Main.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 1
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(800, 399)
        Me.TLpan_Main.TabIndex = 0
        '
        'pan_Contenu
        '
        Me.pan_Contenu.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Contenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Contenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Contenu.Location = New System.Drawing.Point(250, 3)
        Me.pan_Contenu.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pan_Contenu.Name = "pan_Contenu"
        Me.pan_Contenu.Size = New System.Drawing.Size(547, 393)
        Me.pan_Contenu.TabIndex = 3
        '
        'pan_Gauche
        '
        Me.pan_Gauche.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Gauche.Controls.Add(Me.TLpan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0, 0, 1, 0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(249, 399)
        Me.pan_Gauche.TabIndex = 1
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.PoMbtn_Expert, 0, 3)
        Me.TLpan_Gauche.Controls.Add(Me.PoMbtn_Units, 0, 2)
        Me.TLpan_Gauche.Controls.Add(Me.PoMbtn_Directories, 0, 1)
        Me.TLpan_Gauche.Controls.Add(Me.TLpan_PourLesBoutons, 0, 6)
        Me.TLpan_Gauche.Controls.Add(Me.PoMBtn_General, 0, 0)
        Me.TLpan_Gauche.Controls.Add(Me.pan_Expert, 0, 5)
        Me.TLpan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Gauche.Margin = New System.Windows.Forms.Padding(1)
        Me.TLpan_Gauche.Name = "TLpan_Gauche"
        Me.TLpan_Gauche.RowCount = 7
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLpan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Gauche.Size = New System.Drawing.Size(249, 399)
        Me.TLpan_Gauche.TabIndex = 0
        '
        'PoMbtn_Expert
        '
        Me.PoMbtn_Expert.Caption = "PoMbtn_Expert"
        Me.PoMbtn_Expert.CaptionAlignement = System.Windows.Forms.HorizontalAlignment.Center
        Me.PoMbtn_Expert.Checked = False
        Me.PoMbtn_Expert.CouleurChecked = System.Drawing.Color.Orange
        Me.PoMbtn_Expert.CouleurContour = System.Drawing.Color.Black
        Me.PoMbtn_Expert.CouleurContourChecked = System.Drawing.Color.Black
        Me.PoMbtn_Expert.CouleurContourMouseOn = System.Drawing.Color.Black
        Me.PoMbtn_Expert.CouleurFond = System.Drawing.Color.WhiteSmoke
        Me.PoMbtn_Expert.CouleurForGradient = System.Drawing.Color.WhiteSmoke
        Me.PoMbtn_Expert.CouleurMouseOnBtn = System.Drawing.Color.Yellow
        Me.PoMbtn_Expert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PoMbtn_Expert.Enable = True
        Me.PoMbtn_Expert.LContourFond = True
        Me.PoMbtn_Expert.Location = New System.Drawing.Point(3, 153)
        Me.PoMbtn_Expert.Name = "PoMbtn_Expert"
        Me.PoMbtn_Expert.RatioArrondi = 0!
        Me.PoMbtn_Expert.Size = New System.Drawing.Size(243, 44)
        Me.PoMbtn_Expert.TabIndex = 5
        '
        'PoMbtn_Units
        '
        Me.PoMbtn_Units.Caption = "PoMbtn_Units"
        Me.PoMbtn_Units.CaptionAlignement = System.Windows.Forms.HorizontalAlignment.Center
        Me.PoMbtn_Units.Checked = False
        Me.PoMbtn_Units.CouleurChecked = System.Drawing.Color.Orange
        Me.PoMbtn_Units.CouleurContour = System.Drawing.Color.Black
        Me.PoMbtn_Units.CouleurContourChecked = System.Drawing.Color.Black
        Me.PoMbtn_Units.CouleurContourMouseOn = System.Drawing.Color.Black
        Me.PoMbtn_Units.CouleurFond = System.Drawing.Color.WhiteSmoke
        Me.PoMbtn_Units.CouleurForGradient = System.Drawing.Color.WhiteSmoke
        Me.PoMbtn_Units.CouleurMouseOnBtn = System.Drawing.Color.Yellow
        Me.PoMbtn_Units.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PoMbtn_Units.Enable = True
        Me.PoMbtn_Units.LContourFond = True
        Me.PoMbtn_Units.Location = New System.Drawing.Point(3, 103)
        Me.PoMbtn_Units.Name = "PoMbtn_Units"
        Me.PoMbtn_Units.RatioArrondi = 0!
        Me.PoMbtn_Units.Size = New System.Drawing.Size(243, 44)
        Me.PoMbtn_Units.TabIndex = 4
        '
        'PoMbtn_Directories
        '
        Me.PoMbtn_Directories.Caption = "PoMbtn_Directories"
        Me.PoMbtn_Directories.CaptionAlignement = System.Windows.Forms.HorizontalAlignment.Center
        Me.PoMbtn_Directories.Checked = False
        Me.PoMbtn_Directories.CouleurChecked = System.Drawing.Color.Orange
        Me.PoMbtn_Directories.CouleurContour = System.Drawing.Color.Black
        Me.PoMbtn_Directories.CouleurContourChecked = System.Drawing.Color.Black
        Me.PoMbtn_Directories.CouleurContourMouseOn = System.Drawing.Color.Black
        Me.PoMbtn_Directories.CouleurFond = System.Drawing.Color.WhiteSmoke
        Me.PoMbtn_Directories.CouleurForGradient = System.Drawing.Color.WhiteSmoke
        Me.PoMbtn_Directories.CouleurMouseOnBtn = System.Drawing.Color.Yellow
        Me.PoMbtn_Directories.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PoMbtn_Directories.Enable = True
        Me.PoMbtn_Directories.LContourFond = True
        Me.PoMbtn_Directories.Location = New System.Drawing.Point(3, 53)
        Me.PoMbtn_Directories.Name = "PoMbtn_Directories"
        Me.PoMbtn_Directories.RatioArrondi = 0!
        Me.PoMbtn_Directories.Size = New System.Drawing.Size(243, 44)
        Me.PoMbtn_Directories.TabIndex = 3
        '
        'TLpan_PourLesBoutons
        '
        Me.TLpan_PourLesBoutons.ColumnCount = 2
        Me.TLpan_PourLesBoutons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_PourLesBoutons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_PourLesBoutons.Controls.Add(Me.btn_Cancel, 1, 0)
        Me.TLpan_PourLesBoutons.Controls.Add(Me.btn_Appliquer, 0, 0)
        Me.TLpan_PourLesBoutons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_PourLesBoutons.Location = New System.Drawing.Point(0, 369)
        Me.TLpan_PourLesBoutons.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_PourLesBoutons.Name = "TLpan_PourLesBoutons"
        Me.TLpan_PourLesBoutons.RowCount = 1
        Me.TLpan_PourLesBoutons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_PourLesBoutons.Size = New System.Drawing.Size(249, 30)
        Me.TLpan_PourLesBoutons.TabIndex = 2
        '
        'btn_Cancel
        '
        Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Cancel.Location = New System.Drawing.Point(127, 3)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(119, 24)
        Me.btn_Cancel.TabIndex = 1
        Me.btn_Cancel.Text = "btn_Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'btn_Appliquer
        '
        Me.btn_Appliquer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Appliquer.Location = New System.Drawing.Point(3, 3)
        Me.btn_Appliquer.Name = "btn_Appliquer"
        Me.btn_Appliquer.Size = New System.Drawing.Size(118, 24)
        Me.btn_Appliquer.TabIndex = 0
        Me.btn_Appliquer.Text = "btn_Appliquer"
        Me.btn_Appliquer.UseVisualStyleBackColor = True
        '
        'PoMBtn_General
        '
        Me.PoMBtn_General.Caption = "PoMBtn_General"
        Me.PoMBtn_General.CaptionAlignement = System.Windows.Forms.HorizontalAlignment.Center
        Me.PoMBtn_General.Checked = False
        Me.PoMBtn_General.CouleurChecked = System.Drawing.Color.Orange
        Me.PoMBtn_General.CouleurContour = System.Drawing.Color.Black
        Me.PoMBtn_General.CouleurContourChecked = System.Drawing.Color.Black
        Me.PoMBtn_General.CouleurContourMouseOn = System.Drawing.Color.Black
        Me.PoMBtn_General.CouleurFond = System.Drawing.Color.WhiteSmoke
        Me.PoMBtn_General.CouleurForGradient = System.Drawing.Color.WhiteSmoke
        Me.PoMBtn_General.CouleurMouseOnBtn = System.Drawing.Color.Yellow
        Me.PoMBtn_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PoMBtn_General.Enable = True
        Me.PoMBtn_General.LContourFond = True
        Me.PoMBtn_General.Location = New System.Drawing.Point(3, 3)
        Me.PoMBtn_General.Name = "PoMBtn_General"
        Me.PoMBtn_General.RatioArrondi = 0!
        Me.PoMBtn_General.Size = New System.Drawing.Size(243, 44)
        Me.PoMBtn_General.TabIndex = 0
        '
        'pan_Expert
        '
        Me.pan_Expert.Controls.Add(Me.lbl_ExpertMode)
        Me.pan_Expert.Controls.Add(Me.img_Expert)
        Me.pan_Expert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Expert.Location = New System.Drawing.Point(0, 339)
        Me.pan_Expert.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Expert.Name = "pan_Expert"
        Me.pan_Expert.Size = New System.Drawing.Size(249, 30)
        Me.pan_Expert.TabIndex = 6
        '
        'lbl_ExpertMode
        '
        Me.lbl_ExpertMode.AutoSize = True
        Me.lbl_ExpertMode.Location = New System.Drawing.Point(35, 9)
        Me.lbl_ExpertMode.Name = "lbl_ExpertMode"
        Me.lbl_ExpertMode.Size = New System.Drawing.Size(80, 13)
        Me.lbl_ExpertMode.TabIndex = 39
        Me.lbl_ExpertMode.Text = "lbl_ExpertMode"
        '
        'img_Expert
        '
        Me.img_Expert.Image = CType(resources.GetObject("img_Expert.Image"), System.Drawing.Image)
        Me.img_Expert.Location = New System.Drawing.Point(6, 3)
        Me.img_Expert.Name = "img_Expert"
        Me.img_Expert.Size = New System.Drawing.Size(23, 24)
        Me.img_Expert.TabIndex = 38
        Me.img_Expert.TabStop = False
        '
        'Frm_OptionsLogiciel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 399)
        Me.Controls.Add(Me.pan_Main)
        Me.Name = "Frm_OptionsLogiciel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_OptionsLogiciel"
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_PourLesBoutons.ResumeLayout(False)
        Me.pan_Expert.ResumeLayout(False)
        Me.pan_Expert.PerformLayout()
        CType(Me.img_Expert, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents PoMBtn_General As POMbutton
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents pan_Contenu As Panel
    Friend WithEvents TLpan_PourLesBoutons As TableLayoutPanel
    Friend WithEvents btn_Cancel As Button
    Friend WithEvents btn_Appliquer As Button
    Friend WithEvents PoMbtn_Units As POMbutton
    Friend WithEvents PoMbtn_Directories As POMbutton
    Friend WithEvents PoMbtn_Expert As POMbutton
    Friend WithEvents pan_Expert As Panel
    Friend WithEvents lbl_ExpertMode As Label
    Friend WithEvents img_Expert As PictureBox
End Class
