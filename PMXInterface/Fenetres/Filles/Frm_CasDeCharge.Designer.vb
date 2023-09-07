<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CasDeCharge
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
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_CdC = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_CasDeCharges = New System.Windows.Forms.Label()
        Me.pan_ChoixCdC = New System.Windows.Forms.Panel()
        Me.lbl_Name = New System.Windows.Forms.Label()
        Me.lbl_Case = New System.Windows.Forms.Label()
        Me.cmb_Symbols = New System.Windows.Forms.ComboBox()
        Me.Pan_Affichage = New System.Windows.Forms.Panel()
        Me.img_Analyse = New System.Windows.Forms.PictureBox()
        Me.lbl_Mixte = New System.Windows.Forms.Label()
        Me.lbl_NeqDalle = New System.Windows.Forms.Label()
        Me.lbl_NeqEnrob = New System.Windows.Forms.Label()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_CdC.SuspendLayout()
        Me.pan_ChoixCdC.SuspendLayout()
        Me.Pan_Affichage.SuspendLayout()
        CType(Me.img_Analyse, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(1058, 588)
        Me.pan_General.TabIndex = 4
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
        Me.TLpan_Main.Size = New System.Drawing.Size(1058, 588)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 5
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 3, 0)
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_Annuler, 1, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 551)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1052, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(539, 3)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(114, 28)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'btn_Annuler
        '
        Me.btn_Annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Annuler.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Annuler.Location = New System.Drawing.Point(399, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_CdC)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(1052, 542)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_CdC
        '
        Me.TLPan_CdC.ColumnCount = 1
        Me.TLPan_CdC.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_CdC.Controls.Add(Me.lbl_CasDeCharges, 0, 0)
        Me.TLPan_CdC.Controls.Add(Me.pan_ChoixCdC, 0, 1)
        Me.TLPan_CdC.Controls.Add(Me.Pan_Affichage, 0, 2)
        Me.TLPan_CdC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_CdC.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_CdC.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_CdC.Name = "TLPan_CdC"
        Me.TLPan_CdC.RowCount = 3
        Me.TLPan_CdC.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_CdC.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.TLPan_CdC.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_CdC.Size = New System.Drawing.Size(1052, 542)
        Me.TLPan_CdC.TabIndex = 0
        '
        'lbl_CasDeCharges
        '
        Me.lbl_CasDeCharges.AutoSize = True
        Me.lbl_CasDeCharges.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_CasDeCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_CasDeCharges.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_CasDeCharges.Location = New System.Drawing.Point(0, 0)
        Me.lbl_CasDeCharges.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_CasDeCharges.Name = "lbl_CasDeCharges"
        Me.lbl_CasDeCharges.Size = New System.Drawing.Size(1052, 30)
        Me.lbl_CasDeCharges.TabIndex = 3
        Me.lbl_CasDeCharges.Text = "lbl_CasDeCharges"
        Me.lbl_CasDeCharges.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_ChoixCdC
        '
        Me.pan_ChoixCdC.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ChoixCdC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ChoixCdC.Controls.Add(Me.lbl_NeqEnrob)
        Me.pan_ChoixCdC.Controls.Add(Me.lbl_NeqDalle)
        Me.pan_ChoixCdC.Controls.Add(Me.lbl_Mixte)
        Me.pan_ChoixCdC.Controls.Add(Me.lbl_Name)
        Me.pan_ChoixCdC.Controls.Add(Me.lbl_Case)
        Me.pan_ChoixCdC.Controls.Add(Me.cmb_Symbols)
        Me.pan_ChoixCdC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ChoixCdC.Location = New System.Drawing.Point(0, 31)
        Me.pan_ChoixCdC.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.pan_ChoixCdC.Name = "pan_ChoixCdC"
        Me.pan_ChoixCdC.Size = New System.Drawing.Size(1052, 59)
        Me.pan_ChoixCdC.TabIndex = 4
        '
        'lbl_Name
        '
        Me.lbl_Name.AutoSize = True
        Me.lbl_Name.Location = New System.Drawing.Point(164, 22)
        Me.lbl_Name.Name = "lbl_Name"
        Me.lbl_Name.Size = New System.Drawing.Size(39, 13)
        Me.lbl_Name.TabIndex = 55
        Me.lbl_Name.Text = "Label2"
        '
        'lbl_Case
        '
        Me.lbl_Case.AutoSize = True
        Me.lbl_Case.Location = New System.Drawing.Point(24, 22)
        Me.lbl_Case.Name = "lbl_Case"
        Me.lbl_Case.Size = New System.Drawing.Size(39, 13)
        Me.lbl_Case.TabIndex = 54
        Me.lbl_Case.Text = "Label1"
        '
        'cmb_Symbols
        '
        Me.cmb_Symbols.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_Symbols.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_Symbols.FormattingEnabled = True
        Me.cmb_Symbols.Location = New System.Drawing.Point(79, 18)
        Me.cmb_Symbols.Name = "cmb_Symbols"
        Me.cmb_Symbols.Size = New System.Drawing.Size(66, 21)
        Me.cmb_Symbols.TabIndex = 53
        '
        'Pan_Affichage
        '
        Me.Pan_Affichage.Controls.Add(Me.img_Analyse)
        Me.Pan_Affichage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Pan_Affichage.Location = New System.Drawing.Point(0, 91)
        Me.Pan_Affichage.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.Pan_Affichage.Name = "Pan_Affichage"
        Me.Pan_Affichage.Size = New System.Drawing.Size(1052, 451)
        Me.Pan_Affichage.TabIndex = 5
        '
        'img_Analyse
        '
        Me.img_Analyse.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Analyse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Analyse.Location = New System.Drawing.Point(476, 200)
        Me.img_Analyse.Margin = New System.Windows.Forms.Padding(0)
        Me.img_Analyse.Name = "img_Analyse"
        Me.img_Analyse.Size = New System.Drawing.Size(100, 50)
        Me.img_Analyse.TabIndex = 2
        Me.img_Analyse.TabStop = False
        '
        'lbl_Mixte
        '
        Me.lbl_Mixte.AutoSize = True
        Me.lbl_Mixte.Location = New System.Drawing.Point(569, 22)
        Me.lbl_Mixte.Name = "lbl_Mixte"
        Me.lbl_Mixte.Size = New System.Drawing.Size(48, 13)
        Me.lbl_Mixte.TabIndex = 56
        Me.lbl_Mixte.Text = "lbl_Mixte"
        '
        'lbl_NeqDalle
        '
        Me.lbl_NeqDalle.AutoSize = True
        Me.lbl_NeqDalle.Location = New System.Drawing.Point(688, 22)
        Me.lbl_NeqDalle.Name = "lbl_NeqDalle"
        Me.lbl_NeqDalle.Size = New System.Drawing.Size(67, 13)
        Me.lbl_NeqDalle.TabIndex = 57
        Me.lbl_NeqDalle.Text = "lbl_NeqDalle"
        '
        'lbl_NeqEnrob
        '
        Me.lbl_NeqEnrob.AutoSize = True
        Me.lbl_NeqEnrob.Location = New System.Drawing.Point(811, 22)
        Me.lbl_NeqEnrob.Name = "lbl_NeqEnrob"
        Me.lbl_NeqEnrob.Size = New System.Drawing.Size(71, 13)
        Me.lbl_NeqEnrob.TabIndex = 58
        Me.lbl_NeqEnrob.Text = "lbl_NeqEnrob"
        '
        'Frm_CasDeCharge
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1058, 588)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_CasDeCharge"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_CasDeCharge"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_CdC.ResumeLayout(False)
        Me.TLPan_CdC.PerformLayout()
        Me.pan_ChoixCdC.ResumeLayout(False)
        Me.pan_ChoixCdC.PerformLayout()
        Me.Pan_Affichage.ResumeLayout(False)
        CType(Me.img_Analyse, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_CdC As TableLayoutPanel
    Friend WithEvents lbl_CasDeCharges As Label
    Friend WithEvents pan_ChoixCdC As Panel
    Friend WithEvents Pan_Affichage As Panel
    Friend WithEvents img_Analyse As PictureBox
    Friend WithEvents lbl_Name As Label
    Friend WithEvents lbl_Case As Label
    Friend WithEvents cmb_Symbols As ComboBox
    Friend WithEvents lbl_NeqEnrob As Label
    Friend WithEvents lbl_NeqDalle As Label
    Friend WithEvents lbl_Mixte As Label
End Class
