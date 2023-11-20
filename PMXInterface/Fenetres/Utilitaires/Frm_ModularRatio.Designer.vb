<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ModularRatio
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
        Me.TLPan_Portees = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Parameters = New System.Windows.Forms.Label()
        Me.pan_SaisiePortee = New System.Windows.Forms.Panel()
        Me.cmb_PsiL = New System.Windows.Forms.ComboBox()
        Me.img_PsiL = New System.Windows.Forms.PictureBox()
        Me.lbl_PsiL = New System.Windows.Forms.Label()
        Me.cmb_RH = New System.Windows.Forms.ComboBox()
        Me.img_RH = New System.Windows.Forms.PictureBox()
        Me.lbl_RelativeRH = New System.Windows.Forms.Label()
        Me.lbl_AgeT = New System.Windows.Forms.Label()
        Me.txt_AgeT = New System.Windows.Forms.TextBox()
        Me.img_AgeT = New System.Windows.Forms.PictureBox()
        Me.txt_AgeT0 = New System.Windows.Forms.TextBox()
        Me.img_AgeT0 = New System.Windows.Forms.PictureBox()
        Me.lbl_AgeT0 = New System.Windows.Forms.Label()
        Me.lbl_Beton = New System.Windows.Forms.Label()
        Me.cmb_ClasseBeton = New System.Windows.Forms.ComboBox()
        Me.txt_H0 = New System.Windows.Forms.TextBox()
        Me.img_H0 = New System.Windows.Forms.PictureBox()
        Me.lbl_DimensionH0 = New System.Windows.Forms.Label()
        Me.etq_UnitDim = New System.Windows.Forms.Label()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Portees.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_SaisiePortee.SuspendLayout()
        CType(Me.img_PsiL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_RH, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_AgeT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_AgeT0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.img_H0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(1029, 607)
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
        Me.TLpan_Main.Size = New System.Drawing.Size(1029, 607)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 570)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1023, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(524, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(384, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_Portees)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(3, 3)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(1023, 561)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Portees
        '
        Me.TLPan_Portees.ColumnCount = 2
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Portees.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Portees.Name = "TLPan_Portees"
        Me.TLPan_Portees.RowCount = 1
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Size = New System.Drawing.Size(1023, 561)
        Me.TLPan_Portees.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.AutoScroll = True
        Me.pan_Gauche.Controls.Add(Me.TLPan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.pan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(250, 561)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Parameters, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisiePortee, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 3
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 370.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(250, 561)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Parameters
        '
        Me.lbl_Parameters.AutoSize = True
        Me.lbl_Parameters.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Parameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Parameters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Parameters.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Parameters.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Parameters.Name = "lbl_Parameters"
        Me.lbl_Parameters.Size = New System.Drawing.Size(250, 30)
        Me.lbl_Parameters.TabIndex = 0
        Me.lbl_Parameters.Text = "lbl_Parameters"
        Me.lbl_Parameters.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisiePortee
        '
        Me.pan_SaisiePortee.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisiePortee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisiePortee.Controls.Add(Me.etq_UnitDim)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_H0)
        Me.pan_SaisiePortee.Controls.Add(Me.img_H0)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_DimensionH0)
        Me.pan_SaisiePortee.Controls.Add(Me.cmb_ClasseBeton)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_Beton)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_AgeT0)
        Me.pan_SaisiePortee.Controls.Add(Me.img_AgeT0)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_AgeT0)
        Me.pan_SaisiePortee.Controls.Add(Me.txt_AgeT)
        Me.pan_SaisiePortee.Controls.Add(Me.img_AgeT)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_AgeT)
        Me.pan_SaisiePortee.Controls.Add(Me.cmb_PsiL)
        Me.pan_SaisiePortee.Controls.Add(Me.img_PsiL)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_PsiL)
        Me.pan_SaisiePortee.Controls.Add(Me.cmb_RH)
        Me.pan_SaisiePortee.Controls.Add(Me.img_RH)
        Me.pan_SaisiePortee.Controls.Add(Me.lbl_RelativeRH)
        Me.pan_SaisiePortee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisiePortee.Location = New System.Drawing.Point(0, 30)
        Me.pan_SaisiePortee.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisiePortee.Name = "pan_SaisiePortee"
        Me.pan_SaisiePortee.Size = New System.Drawing.Size(250, 370)
        Me.pan_SaisiePortee.TabIndex = 1
        '
        'cmb_PsiL
        '
        Me.cmb_PsiL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_PsiL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_PsiL.FormattingEnabled = True
        Me.cmb_PsiL.Location = New System.Drawing.Point(134, 130)
        Me.cmb_PsiL.Name = "cmb_PsiL"
        Me.cmb_PsiL.Size = New System.Drawing.Size(85, 21)
        Me.cmb_PsiL.TabIndex = 76
        '
        'img_PsiL
        '
        Me.img_PsiL.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_PsiL.Location = New System.Drawing.Point(87, 130)
        Me.img_PsiL.Name = "img_PsiL"
        Me.img_PsiL.Size = New System.Drawing.Size(46, 20)
        Me.img_PsiL.TabIndex = 75
        Me.img_PsiL.TabStop = False
        '
        'lbl_PsiL
        '
        Me.lbl_PsiL.AutoSize = True
        Me.lbl_PsiL.Location = New System.Drawing.Point(8, 112)
        Me.lbl_PsiL.Name = "lbl_PsiL"
        Me.lbl_PsiL.Size = New System.Drawing.Size(43, 13)
        Me.lbl_PsiL.TabIndex = 74
        Me.lbl_PsiL.Text = "lbl_PsiL"
        '
        'cmb_RH
        '
        Me.cmb_RH.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_RH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_RH.FormattingEnabled = True
        Me.cmb_RH.Location = New System.Drawing.Point(134, 76)
        Me.cmb_RH.Name = "cmb_RH"
        Me.cmb_RH.Size = New System.Drawing.Size(85, 21)
        Me.cmb_RH.TabIndex = 73
        '
        'img_RH
        '
        Me.img_RH.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_RH.Location = New System.Drawing.Point(87, 76)
        Me.img_RH.Name = "img_RH"
        Me.img_RH.Size = New System.Drawing.Size(46, 20)
        Me.img_RH.TabIndex = 72
        Me.img_RH.TabStop = False
        '
        'lbl_RelativeRH
        '
        Me.lbl_RelativeRH.AutoSize = True
        Me.lbl_RelativeRH.Location = New System.Drawing.Point(8, 58)
        Me.lbl_RelativeRH.Name = "lbl_RelativeRH"
        Me.lbl_RelativeRH.Size = New System.Drawing.Size(78, 13)
        Me.lbl_RelativeRH.TabIndex = 1
        Me.lbl_RelativeRH.Text = "lbl_RelativeRH"
        '
        'lbl_AgeT
        '
        Me.lbl_AgeT.AutoSize = True
        Me.lbl_AgeT.Location = New System.Drawing.Point(8, 167)
        Me.lbl_AgeT.Name = "lbl_AgeT"
        Me.lbl_AgeT.Size = New System.Drawing.Size(49, 13)
        Me.lbl_AgeT.TabIndex = 77
        Me.lbl_AgeT.Text = "lbl_AgeT"
        '
        'txt_AgeT
        '
        Me.txt_AgeT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_AgeT.Location = New System.Drawing.Point(134, 187)
        Me.txt_AgeT.Name = "txt_AgeT"
        Me.txt_AgeT.Size = New System.Drawing.Size(58, 20)
        Me.txt_AgeT.TabIndex = 78
        '
        'img_AgeT
        '
        Me.img_AgeT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_AgeT.Location = New System.Drawing.Point(87, 187)
        Me.img_AgeT.Name = "img_AgeT"
        Me.img_AgeT.Size = New System.Drawing.Size(46, 20)
        Me.img_AgeT.TabIndex = 79
        Me.img_AgeT.TabStop = False
        '
        'txt_AgeT0
        '
        Me.txt_AgeT0.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_AgeT0.Location = New System.Drawing.Point(134, 239)
        Me.txt_AgeT0.Name = "txt_AgeT0"
        Me.txt_AgeT0.Size = New System.Drawing.Size(58, 20)
        Me.txt_AgeT0.TabIndex = 81
        '
        'img_AgeT0
        '
        Me.img_AgeT0.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_AgeT0.Location = New System.Drawing.Point(87, 239)
        Me.img_AgeT0.Name = "img_AgeT0"
        Me.img_AgeT0.Size = New System.Drawing.Size(46, 20)
        Me.img_AgeT0.TabIndex = 82
        Me.img_AgeT0.TabStop = False
        '
        'lbl_AgeT0
        '
        Me.lbl_AgeT0.AutoSize = True
        Me.lbl_AgeT0.Location = New System.Drawing.Point(8, 219)
        Me.lbl_AgeT0.Name = "lbl_AgeT0"
        Me.lbl_AgeT0.Size = New System.Drawing.Size(55, 13)
        Me.lbl_AgeT0.TabIndex = 80
        Me.lbl_AgeT0.Text = "lbl_AgeT0"
        '
        'lbl_Beton
        '
        Me.lbl_Beton.AutoSize = True
        Me.lbl_Beton.Location = New System.Drawing.Point(8, 15)
        Me.lbl_Beton.Name = "lbl_Beton"
        Me.lbl_Beton.Size = New System.Drawing.Size(51, 13)
        Me.lbl_Beton.TabIndex = 83
        Me.lbl_Beton.Text = "lbl_Beton"
        '
        'cmb_ClasseBeton
        '
        Me.cmb_ClasseBeton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmb_ClasseBeton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmb_ClasseBeton.FormattingEnabled = True
        Me.cmb_ClasseBeton.Location = New System.Drawing.Point(134, 30)
        Me.cmb_ClasseBeton.Name = "cmb_ClasseBeton"
        Me.cmb_ClasseBeton.Size = New System.Drawing.Size(85, 21)
        Me.cmb_ClasseBeton.TabIndex = 84
        '
        'txt_H0
        '
        Me.txt_H0.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_H0.Location = New System.Drawing.Point(134, 292)
        Me.txt_H0.Name = "txt_H0"
        Me.txt_H0.Size = New System.Drawing.Size(58, 20)
        Me.txt_H0.TabIndex = 86
        '
        'img_H0
        '
        Me.img_H0.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.img_H0.Location = New System.Drawing.Point(87, 292)
        Me.img_H0.Name = "img_H0"
        Me.img_H0.Size = New System.Drawing.Size(46, 20)
        Me.img_H0.TabIndex = 87
        Me.img_H0.TabStop = False
        '
        'lbl_DimensionH0
        '
        Me.lbl_DimensionH0.AutoSize = True
        Me.lbl_DimensionH0.Location = New System.Drawing.Point(8, 272)
        Me.lbl_DimensionH0.Name = "lbl_DimensionH0"
        Me.lbl_DimensionH0.Size = New System.Drawing.Size(86, 13)
        Me.lbl_DimensionH0.TabIndex = 85
        Me.lbl_DimensionH0.Text = "lbl_DimensionH0"
        '
        'etq_UnitDim
        '
        Me.etq_UnitDim.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.etq_UnitDim.AutoSize = True
        Me.etq_UnitDim.Location = New System.Drawing.Point(198, 295)
        Me.etq_UnitDim.Name = "etq_UnitDim"
        Me.etq_UnitDim.Size = New System.Drawing.Size(23, 13)
        Me.etq_UnitDim.TabIndex = 88
        Me.etq_UnitDim.Text = "mm"
        '
        'Frm_ModularRatio
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1029, 607)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_ModularRatio"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_ModularRatio"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Portees.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_SaisiePortee.ResumeLayout(False)
        Me.pan_SaisiePortee.PerformLayout()
        CType(Me.img_PsiL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_RH, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_AgeT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_AgeT0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.img_H0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLPan_Portees As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLPan_Gauche As TableLayoutPanel
    Friend WithEvents lbl_Parameters As Label
    Friend WithEvents pan_SaisiePortee As Panel
    Friend WithEvents lbl_RelativeRH As Label
    Friend WithEvents img_RH As PictureBox
    Friend WithEvents cmb_RH As ComboBox
    Friend WithEvents cmb_PsiL As ComboBox
    Friend WithEvents img_PsiL As PictureBox
    Friend WithEvents lbl_PsiL As Label
    Friend WithEvents lbl_AgeT As Label
    Friend WithEvents cmb_ClasseBeton As ComboBox
    Friend WithEvents lbl_Beton As Label
    Friend WithEvents txt_AgeT0 As TextBox
    Friend WithEvents img_AgeT0 As PictureBox
    Friend WithEvents lbl_AgeT0 As Label
    Friend WithEvents txt_AgeT As TextBox
    Friend WithEvents img_AgeT As PictureBox
    Friend WithEvents txt_H0 As TextBox
    Friend WithEvents img_H0 As PictureBox
    Friend WithEvents lbl_DimensionH0 As Label
    Friend WithEvents etq_UnitDim As Label
End Class
