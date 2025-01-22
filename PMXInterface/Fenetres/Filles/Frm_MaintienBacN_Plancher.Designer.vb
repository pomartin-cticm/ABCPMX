<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MaintienBacN_Plancher
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
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Panneau = New System.Windows.Forms.Label()
        Me.lbl_Floor = New System.Windows.Forms.Label()
        Me.pan_DefPlancher = New System.Windows.Forms.Panel()
        Me.chk_PriseEnCompteBac = New System.Windows.Forms.CheckBox()
        Me.cmb_Transition = New System.Windows.Forms.ComboBox()
        Me.lbl_Transition = New System.Windows.Forms.Label()
        Me.txt_NbSheetsTransverse = New System.Windows.Forms.TextBox()
        Me.lbl_NbSheetsTransverse = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_Largeur = New System.Windows.Forms.Label()
        Me.lbl_DimensionsGlobales = New System.Windows.Forms.Label()
        Me.lbl_Portee = New System.Windows.Forms.Label()
        Me.txt_LongueurP = New System.Windows.Forms.TextBox()
        Me.etq_UnitL2 = New System.Windows.Forms.Label()
        Me.etq_UnitL1 = New System.Windows.Forms.Label()
        Me.txt_LargeurP = New System.Windows.Forms.TextBox()
        Me.pan_Panneau = New System.Windows.Forms.Panel()
        Me.cmb_NbSpan = New System.Windows.Forms.ComboBox()
        Me.lbl_NbSpans = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.img_bp = New System.Windows.Forms.PictureBox()
        Me.img_ap = New System.Windows.Forms.PictureBox()
        Me.lbl_SheetWidth = New System.Windows.Forms.Label()
        Me.lbl_SheetLength = New System.Windows.Forms.Label()
        Me.etq_UnitL4 = New System.Windows.Forms.Label()
        Me.txt_SheetWidth = New System.Windows.Forms.TextBox()
        Me.etq_UnitL3 = New System.Windows.Forms.Label()
        Me.txt_SheetLength = New System.Windows.Forms.TextBox()
        Me.lbl_IndSheetDimensions = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_DefPlancher.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pan_Panneau.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.img_bp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_ap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Main.Location = New System.Drawing.Point(135, 68)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(393, 411)
        Me.pan_Main.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Panneau, 0, 2)
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Floor, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_DefPlancher, 0, 1)
        Me.TLPan_Gauche.Controls.Add(Me.pan_Panneau, 0, 3)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 5
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(393, 411)
        Me.TLPan_Gauche.TabIndex = 1
        '
        'lbl_Panneau
        '
        Me.lbl_Panneau.AutoSize = True
        Me.lbl_Panneau.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Panneau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Panneau.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Panneau.Location = New System.Drawing.Point(0, 210)
        Me.lbl_Panneau.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Panneau.Name = "lbl_Panneau"
        Me.lbl_Panneau.Size = New System.Drawing.Size(393, 30)
        Me.lbl_Panneau.TabIndex = 2
        Me.lbl_Panneau.Text = "lbl_Panneau"
        Me.lbl_Panneau.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_Floor
        '
        Me.lbl_Floor.AutoSize = True
        Me.lbl_Floor.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Floor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Floor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Floor.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Floor.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Floor.Name = "lbl_Floor"
        Me.lbl_Floor.Size = New System.Drawing.Size(393, 30)
        Me.lbl_Floor.TabIndex = 0
        Me.lbl_Floor.Text = "lbl_Floor"
        Me.lbl_Floor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_DefPlancher
        '
        Me.pan_DefPlancher.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_DefPlancher.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_DefPlancher.Controls.Add(Me.chk_PriseEnCompteBac)
        Me.pan_DefPlancher.Controls.Add(Me.cmb_Transition)
        Me.pan_DefPlancher.Controls.Add(Me.lbl_Transition)
        Me.pan_DefPlancher.Controls.Add(Me.txt_NbSheetsTransverse)
        Me.pan_DefPlancher.Controls.Add(Me.lbl_NbSheetsTransverse)
        Me.pan_DefPlancher.Controls.Add(Me.Panel1)
        Me.pan_DefPlancher.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_DefPlancher.Location = New System.Drawing.Point(0, 30)
        Me.pan_DefPlancher.Margin = New System.Windows.Forms.Padding(0, 0, 0, 1)
        Me.pan_DefPlancher.Name = "pan_DefPlancher"
        Me.pan_DefPlancher.Size = New System.Drawing.Size(393, 179)
        Me.pan_DefPlancher.TabIndex = 1
        '
        'chk_PriseEnCompteBac
        '
        Me.chk_PriseEnCompteBac.AutoSize = True
        Me.chk_PriseEnCompteBac.Location = New System.Drawing.Point(8, 5)
        Me.chk_PriseEnCompteBac.Name = "chk_PriseEnCompteBac"
        Me.chk_PriseEnCompteBac.Size = New System.Drawing.Size(141, 17)
        Me.chk_PriseEnCompteBac.TabIndex = 7
        Me.chk_PriseEnCompteBac.Text = "chk_PriseEnCompteBac"
        Me.chk_PriseEnCompteBac.UseVisualStyleBackColor = True
        '
        'cmb_Transition
        '
        Me.cmb_Transition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Transition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Transition.FormattingEnabled = True
        Me.cmb_Transition.Location = New System.Drawing.Point(6, 149)
        Me.cmb_Transition.Name = "cmb_Transition"
        Me.cmb_Transition.Size = New System.Drawing.Size(380, 21)
        Me.cmb_Transition.TabIndex = 6
        '
        'lbl_Transition
        '
        Me.lbl_Transition.AutoSize = True
        Me.lbl_Transition.Location = New System.Drawing.Point(8, 134)
        Me.lbl_Transition.Name = "lbl_Transition"
        Me.lbl_Transition.Size = New System.Drawing.Size(69, 13)
        Me.lbl_Transition.TabIndex = 5
        Me.lbl_Transition.Text = "lbl_Transition"
        '
        'txt_NbSheetsTransverse
        '
        Me.txt_NbSheetsTransverse.Location = New System.Drawing.Point(149, 45)
        Me.txt_NbSheetsTransverse.Name = "txt_NbSheetsTransverse"
        Me.txt_NbSheetsTransverse.Size = New System.Drawing.Size(58, 20)
        Me.txt_NbSheetsTransverse.TabIndex = 1
        Me.txt_NbSheetsTransverse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_NbSheetsTransverse
        '
        Me.lbl_NbSheetsTransverse.AutoSize = True
        Me.lbl_NbSheetsTransverse.Location = New System.Drawing.Point(8, 29)
        Me.lbl_NbSheetsTransverse.Name = "lbl_NbSheetsTransverse"
        Me.lbl_NbSheetsTransverse.Size = New System.Drawing.Size(123, 13)
        Me.lbl_NbSheetsTransverse.TabIndex = 0
        Me.lbl_NbSheetsTransverse.Text = "lbl_NbSheetsTransverse"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbl_Largeur)
        Me.Panel1.Controls.Add(Me.lbl_DimensionsGlobales)
        Me.Panel1.Controls.Add(Me.lbl_Portee)
        Me.Panel1.Controls.Add(Me.txt_LongueurP)
        Me.Panel1.Controls.Add(Me.etq_UnitL2)
        Me.Panel1.Controls.Add(Me.etq_UnitL1)
        Me.Panel1.Controls.Add(Me.txt_LargeurP)
        Me.Panel1.Location = New System.Drawing.Point(1, 66)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(244, 64)
        Me.Panel1.TabIndex = 4
        '
        'lbl_Largeur
        '
        Me.lbl_Largeur.Location = New System.Drawing.Point(24, 43)
        Me.lbl_Largeur.Name = "lbl_Largeur"
        Me.lbl_Largeur.Size = New System.Drawing.Size(118, 13)
        Me.lbl_Largeur.TabIndex = 79
        Me.lbl_Largeur.Text = "lbl_Largeur"
        Me.lbl_Largeur.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_DimensionsGlobales
        '
        Me.lbl_DimensionsGlobales.AutoSize = True
        Me.lbl_DimensionsGlobales.Location = New System.Drawing.Point(7, 2)
        Me.lbl_DimensionsGlobales.Name = "lbl_DimensionsGlobales"
        Me.lbl_DimensionsGlobales.Size = New System.Drawing.Size(118, 13)
        Me.lbl_DimensionsGlobales.TabIndex = 2
        Me.lbl_DimensionsGlobales.Text = "lbl_DimensionsGlobales"
        '
        'lbl_Portee
        '
        Me.lbl_Portee.Location = New System.Drawing.Point(24, 22)
        Me.lbl_Portee.Name = "lbl_Portee"
        Me.lbl_Portee.Size = New System.Drawing.Size(118, 13)
        Me.lbl_Portee.TabIndex = 78
        Me.lbl_Portee.Text = "lbl_Portee"
        Me.lbl_Portee.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txt_LongueurP
        '
        Me.txt_LongueurP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_LongueurP.Location = New System.Drawing.Point(148, 18)
        Me.txt_LongueurP.Name = "txt_LongueurP"
        Me.txt_LongueurP.Size = New System.Drawing.Size(58, 20)
        Me.txt_LongueurP.TabIndex = 73
        Me.txt_LongueurP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitL2
        '
        Me.etq_UnitL2.AutoSize = True
        Me.etq_UnitL2.Location = New System.Drawing.Point(212, 43)
        Me.etq_UnitL2.Name = "etq_UnitL2"
        Me.etq_UnitL2.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL2.TabIndex = 77
        Me.etq_UnitL2.Text = "kN"
        '
        'etq_UnitL1
        '
        Me.etq_UnitL1.AutoSize = True
        Me.etq_UnitL1.Location = New System.Drawing.Point(212, 22)
        Me.etq_UnitL1.Name = "etq_UnitL1"
        Me.etq_UnitL1.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL1.TabIndex = 75
        Me.etq_UnitL1.Text = "kN"
        '
        'txt_LargeurP
        '
        Me.txt_LargeurP.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_LargeurP.Location = New System.Drawing.Point(148, 39)
        Me.txt_LargeurP.Name = "txt_LargeurP"
        Me.txt_LargeurP.Size = New System.Drawing.Size(58, 20)
        Me.txt_LargeurP.TabIndex = 76
        Me.txt_LargeurP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'pan_Panneau
        '
        Me.pan_Panneau.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Panneau.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Panneau.Controls.Add(Me.cmb_NbSpan)
        Me.pan_Panneau.Controls.Add(Me.lbl_NbSpans)
        Me.pan_Panneau.Controls.Add(Me.Panel2)
        Me.pan_Panneau.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Panneau.Location = New System.Drawing.Point(0, 240)
        Me.pan_Panneau.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Panneau.Name = "pan_Panneau"
        Me.pan_Panneau.Size = New System.Drawing.Size(393, 140)
        Me.pan_Panneau.TabIndex = 3
        '
        'cmb_NbSpan
        '
        Me.cmb_NbSpan.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_NbSpan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_NbSpan.FormattingEnabled = True
        Me.cmb_NbSpan.Location = New System.Drawing.Point(149, 26)
        Me.cmb_NbSpan.Name = "cmb_NbSpan"
        Me.cmb_NbSpan.Size = New System.Drawing.Size(218, 21)
        Me.cmb_NbSpan.TabIndex = 5
        '
        'lbl_NbSpans
        '
        Me.lbl_NbSpans.AutoSize = True
        Me.lbl_NbSpans.Location = New System.Drawing.Point(8, 10)
        Me.lbl_NbSpans.Name = "lbl_NbSpans"
        Me.lbl_NbSpans.Size = New System.Drawing.Size(67, 13)
        Me.lbl_NbSpans.TabIndex = 3
        Me.lbl_NbSpans.Text = "lbl_NbSpans"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.img_bp)
        Me.Panel2.Controls.Add(Me.img_ap)
        Me.Panel2.Controls.Add(Me.lbl_SheetWidth)
        Me.Panel2.Controls.Add(Me.lbl_SheetLength)
        Me.Panel2.Controls.Add(Me.etq_UnitL4)
        Me.Panel2.Controls.Add(Me.txt_SheetWidth)
        Me.Panel2.Controls.Add(Me.etq_UnitL3)
        Me.Panel2.Controls.Add(Me.txt_SheetLength)
        Me.Panel2.Controls.Add(Me.lbl_IndSheetDimensions)
        Me.Panel2.Location = New System.Drawing.Point(1, 54)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(244, 71)
        Me.Panel2.TabIndex = 5
        '
        'img_bp
        '
        Me.img_bp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_bp.Location = New System.Drawing.Point(111, 44)
        Me.img_bp.Name = "img_bp"
        Me.img_bp.Size = New System.Drawing.Size(37, 20)
        Me.img_bp.TabIndex = 89
        Me.img_bp.TabStop = False
        '
        'img_ap
        '
        Me.img_ap.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_ap.Location = New System.Drawing.Point(111, 22)
        Me.img_ap.Name = "img_ap"
        Me.img_ap.Size = New System.Drawing.Size(37, 20)
        Me.img_ap.TabIndex = 94
        Me.img_ap.TabStop = False
        '
        'lbl_SheetWidth
        '
        Me.lbl_SheetWidth.Location = New System.Drawing.Point(6, 48)
        Me.lbl_SheetWidth.Name = "lbl_SheetWidth"
        Me.lbl_SheetWidth.Size = New System.Drawing.Size(100, 13)
        Me.lbl_SheetWidth.TabIndex = 93
        Me.lbl_SheetWidth.Text = "lbl_SheetWidth"
        Me.lbl_SheetWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_SheetLength
        '
        Me.lbl_SheetLength.Location = New System.Drawing.Point(6, 26)
        Me.lbl_SheetLength.Name = "lbl_SheetLength"
        Me.lbl_SheetLength.Size = New System.Drawing.Size(100, 13)
        Me.lbl_SheetLength.TabIndex = 92
        Me.lbl_SheetLength.Text = "lbl_SheetLength"
        Me.lbl_SheetLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'etq_UnitL4
        '
        Me.etq_UnitL4.AutoSize = True
        Me.etq_UnitL4.Location = New System.Drawing.Point(212, 48)
        Me.etq_UnitL4.Name = "etq_UnitL4"
        Me.etq_UnitL4.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL4.TabIndex = 91
        Me.etq_UnitL4.Text = "kN"
        '
        'txt_SheetWidth
        '
        Me.txt_SheetWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_SheetWidth.Location = New System.Drawing.Point(148, 44)
        Me.txt_SheetWidth.Name = "txt_SheetWidth"
        Me.txt_SheetWidth.Size = New System.Drawing.Size(58, 20)
        Me.txt_SheetWidth.TabIndex = 90
        Me.txt_SheetWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'etq_UnitL3
        '
        Me.etq_UnitL3.AutoSize = True
        Me.etq_UnitL3.Location = New System.Drawing.Point(212, 26)
        Me.etq_UnitL3.Name = "etq_UnitL3"
        Me.etq_UnitL3.Size = New System.Drawing.Size(21, 13)
        Me.etq_UnitL3.TabIndex = 89
        Me.etq_UnitL3.Text = "kN"
        '
        'txt_SheetLength
        '
        Me.txt_SheetLength.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_SheetLength.Location = New System.Drawing.Point(148, 22)
        Me.txt_SheetLength.Name = "txt_SheetLength"
        Me.txt_SheetLength.Size = New System.Drawing.Size(58, 20)
        Me.txt_SheetLength.TabIndex = 88
        Me.txt_SheetLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lbl_IndSheetDimensions
        '
        Me.lbl_IndSheetDimensions.AutoSize = True
        Me.lbl_IndSheetDimensions.Location = New System.Drawing.Point(7, 2)
        Me.lbl_IndSheetDimensions.Name = "lbl_IndSheetDimensions"
        Me.lbl_IndSheetDimensions.Size = New System.Drawing.Size(120, 13)
        Me.lbl_IndSheetDimensions.TabIndex = 87
        Me.lbl_IndSheetDimensions.Text = "lbl_IndSheetDimensions"
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Frm_MaintienBacN_Plancher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 622)
        Me.Controls.Add(Me.pan_Main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frm_MaintienBacN_Plancher"
        Me.Text = "Frm_MaintienBacN_Plancher"
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_DefPlancher.ResumeLayout(False)
        Me.pan_DefPlancher.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pan_Panneau.ResumeLayout(False)
        Me.pan_Panneau.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.img_bp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_ap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Panneau As Label
    Friend WithEvents lbl_Floor As Label
    Friend WithEvents pan_DefPlancher As Panel
    Friend WithEvents chk_PriseEnCompteBac As CheckBox
    Friend WithEvents cmb_Transition As ComboBox
    Friend WithEvents lbl_Transition As Label
    Friend WithEvents txt_NbSheetsTransverse As TextBox
    Friend WithEvents lbl_NbSheetsTransverse As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl_Largeur As Label
    Friend WithEvents lbl_DimensionsGlobales As Label
    Friend WithEvents lbl_Portee As Label
    Friend WithEvents txt_LongueurP As TextBox
    Friend WithEvents etq_UnitL2 As Label
    Friend WithEvents etq_UnitL1 As Label
    Friend WithEvents txt_LargeurP As TextBox
    Friend WithEvents pan_Panneau As Panel
    Friend WithEvents cmb_NbSpan As ComboBox
    Friend WithEvents lbl_NbSpans As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lbl_SheetWidth As Label
    Friend WithEvents lbl_SheetLength As Label
    Friend WithEvents etq_UnitL4 As Label
    Friend WithEvents txt_SheetWidth As TextBox
    Friend WithEvents etq_UnitL3 As Label
    Friend WithEvents txt_SheetLength As TextBox
    Friend WithEvents lbl_IndSheetDimensions As Label
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents img_bp As PictureBox
    Friend WithEvents img_ap As PictureBox
End Class
