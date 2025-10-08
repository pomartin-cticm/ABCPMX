<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Basic
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Basic))
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLPan_Portees = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLPan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Portees = New System.Windows.Forms.Label()
        Me.pan_SaisiePortee = New System.Windows.Forms.Panel()
        Me.btn_Moins = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btn_Precedent = New System.Windows.Forms.Button()
        Me.img_Portees = New System.Windows.Forms.PictureBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Main.SuspendLayout()
        Me.TLPan_Portees.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLPan_Gauche.SuspendLayout()
        Me.pan_SaisiePortee.SuspendLayout()
        CType(Me.img_Portees, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(1067, 554)
        Me.pan_General.TabIndex = 2
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
        Me.TLpan_Main.Size = New System.Drawing.Size(1067, 554)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieBasse
        '
        Me.TLPan_PartieBasse.ColumnCount = 5
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.TLPan_PartieBasse.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_OK, 3, 0)
        Me.TLPan_PartieBasse.Controls.Add(Me.btn_Annuler, 1, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(4, 509)
        Me.TLPan_PartieBasse.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1059, 41)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(547, 4)
        Me.btn_OK.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_OK.Name = "btn_OK"
        Me.btn_OK.Size = New System.Drawing.Size(152, 33)
        Me.btn_OK.TabIndex = 1
        Me.btn_OK.Text = "btn_OK"
        Me.btn_OK.UseVisualStyleBackColor = True
        '
        'btn_Annuler
        '
        Me.btn_Annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Annuler.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Annuler.Location = New System.Drawing.Point(360, 4)
        Me.btn_Annuler.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(152, 33)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.Control
        Me.pan_Main.Controls.Add(Me.TLPan_Portees)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(4, 4)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(1059, 497)
        Me.pan_Main.TabIndex = 1
        '
        'TLPan_Portees
        '
        Me.TLPan_Portees.ColumnCount = 2
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 333.0!))
        Me.TLPan_Portees.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLPan_Portees.Controls.Add(Me.img_Portees, 1, 0)
        Me.TLPan_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Portees.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Portees.Name = "TLPan_Portees"
        Me.TLPan_Portees.RowCount = 1
        Me.TLPan_Portees.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Portees.Size = New System.Drawing.Size(1059, 497)
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
        Me.pan_Gauche.Size = New System.Drawing.Size(333, 497)
        Me.pan_Gauche.TabIndex = 0
        '
        'TLPan_Gauche
        '
        Me.TLPan_Gauche.ColumnCount = 1
        Me.TLPan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Controls.Add(Me.lbl_Portees, 0, 0)
        Me.TLPan_Gauche.Controls.Add(Me.pan_SaisiePortee, 0, 1)
        Me.TLPan_Gauche.Dock = System.Windows.Forms.DockStyle.Top
        Me.TLPan_Gauche.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_Gauche.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Gauche.Name = "TLPan_Gauche"
        Me.TLPan_Gauche.RowCount = 3
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 209.0!))
        Me.TLPan_Gauche.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Gauche.Size = New System.Drawing.Size(333, 250)
        Me.TLPan_Gauche.TabIndex = 0
        '
        'lbl_Portees
        '
        Me.lbl_Portees.AutoSize = True
        Me.lbl_Portees.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Portees.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Portees.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Portees.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Portees.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Portees.Name = "lbl_Portees"
        Me.lbl_Portees.Size = New System.Drawing.Size(333, 37)
        Me.lbl_Portees.TabIndex = 0
        Me.lbl_Portees.Text = "lbl_Portee"
        Me.lbl_Portees.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_SaisiePortee
        '
        Me.pan_SaisiePortee.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_SaisiePortee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_SaisiePortee.Controls.Add(Me.btn_Moins)
        Me.pan_SaisiePortee.Controls.Add(Me.Button1)
        Me.pan_SaisiePortee.Controls.Add(Me.btn_Precedent)
        Me.pan_SaisiePortee.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_SaisiePortee.Location = New System.Drawing.Point(0, 37)
        Me.pan_SaisiePortee.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_SaisiePortee.Name = "pan_SaisiePortee"
        Me.pan_SaisiePortee.Size = New System.Drawing.Size(333, 209)
        Me.pan_SaisiePortee.TabIndex = 1
        '
        'btn_Moins
        '
        Me.btn_Moins.Image = CType(resources.GetObject("btn_Moins.Image"), System.Drawing.Image)
        Me.btn_Moins.Location = New System.Drawing.Point(199, 103)
        Me.btn_Moins.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_Moins.Name = "btn_Moins"
        Me.btn_Moins.Size = New System.Drawing.Size(31, 15)
        Me.btn_Moins.TabIndex = 13
        Me.btn_Moins.TabStop = False
        Me.btn_Moins.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(199, 90)
        Me.Button1.Margin = New System.Windows.Forms.Padding(0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(31, 15)
        Me.Button1.TabIndex = 12
        Me.Button1.TabStop = False
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btn_Precedent
        '
        Me.btn_Precedent.Image = CType(resources.GetObject("btn_Precedent.Image"), System.Drawing.Image)
        Me.btn_Precedent.Location = New System.Drawing.Point(168, 90)
        Me.btn_Precedent.Margin = New System.Windows.Forms.Padding(0)
        Me.btn_Precedent.Name = "btn_Precedent"
        Me.btn_Precedent.Size = New System.Drawing.Size(31, 28)
        Me.btn_Precedent.TabIndex = 11
        Me.btn_Precedent.TabStop = False
        Me.btn_Precedent.UseVisualStyleBackColor = True
        '
        'img_Portees
        '
        Me.img_Portees.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.img_Portees.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.img_Portees.Location = New System.Drawing.Point(334, 0)
        Me.img_Portees.Margin = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.img_Portees.Name = "img_Portees"
        Me.img_Portees.Size = New System.Drawing.Size(133, 61)
        Me.img_Portees.TabIndex = 1
        Me.img_Portees.TabStop = False
        '
        'Frm_Basic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1067, 554)
        Me.Controls.Add(Me.pan_General)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Basic"
        Me.ShowInTaskbar = False
        Me.Text = "Frm_Basic"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Main.ResumeLayout(False)
        Me.TLPan_Portees.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.ResumeLayout(False)
        Me.TLPan_Gauche.PerformLayout()
        Me.pan_SaisiePortee.ResumeLayout(False)
        CType(Me.img_Portees, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lbl_Portees As Label
    Friend WithEvents pan_SaisiePortee As Panel
    Friend WithEvents img_Portees As PictureBox
    Friend WithEvents btn_Moins As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents btn_Precedent As Button
End Class
