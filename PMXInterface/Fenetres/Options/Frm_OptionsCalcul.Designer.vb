<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_OptionsCalcul
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
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_PourLesBoutons = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.btn_Appliquer = New System.Windows.Forms.Button()
        Me.pan_Contenu = New System.Windows.Forms.Panel()
        Me.PoMbutton2 = New PMXInterface.POMbutton()
        Me.PoMbtn_Scope = New PMXInterface.POMbutton()
        Me.PoMBtn_Gamma = New PMXInterface.POMbutton()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
        Me.TLpan_Gauche.SuspendLayout()
        Me.TLpan_PourLesBoutons.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pan_Main.Controls.Add(Me.TLpan_Main)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(0, 0)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(800, 450)
        Me.pan_Main.TabIndex = 1
        '
        'TLpan_Main
        '
        Me.TLpan_Main.ColumnCount = 2
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Controls.Add(Me.pan_Gauche, 0, 0)
        Me.TLpan_Main.Controls.Add(Me.pan_Contenu, 1, 0)
        Me.TLpan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Main.Margin = New System.Windows.Forms.Padding(2)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 1
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(800, 450)
        Me.TLpan_Main.TabIndex = 0
        '
        'pan_Gauche
        '
        Me.pan_Gauche.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Gauche.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Gauche.Controls.Add(Me.TLpan_Gauche)
        Me.pan_Gauche.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Gauche.Location = New System.Drawing.Point(3, 3)
        Me.pan_Gauche.Name = "pan_Gauche"
        Me.pan_Gauche.Size = New System.Drawing.Size(244, 444)
        Me.pan_Gauche.TabIndex = 1
        '
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.PoMbutton2, 0, 2)
        Me.TLpan_Gauche.Controls.Add(Me.PoMbtn_Scope, 0, 1)
        Me.TLpan_Gauche.Controls.Add(Me.PoMBtn_Gamma, 0, 0)
        Me.TLpan_Gauche.Controls.Add(Me.TLpan_PourLesBoutons, 0, 6)
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
        Me.TLpan_Gauche.Size = New System.Drawing.Size(242, 442)
        Me.TLpan_Gauche.TabIndex = 0
        '
        'TLpan_PourLesBoutons
        '
        Me.TLpan_PourLesBoutons.ColumnCount = 2
        Me.TLpan_PourLesBoutons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_PourLesBoutons.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_PourLesBoutons.Controls.Add(Me.btn_Cancel, 1, 0)
        Me.TLpan_PourLesBoutons.Controls.Add(Me.btn_Appliquer, 0, 0)
        Me.TLpan_PourLesBoutons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_PourLesBoutons.Location = New System.Drawing.Point(0, 412)
        Me.TLpan_PourLesBoutons.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_PourLesBoutons.Name = "TLpan_PourLesBoutons"
        Me.TLpan_PourLesBoutons.RowCount = 1
        Me.TLpan_PourLesBoutons.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_PourLesBoutons.Size = New System.Drawing.Size(242, 30)
        Me.TLpan_PourLesBoutons.TabIndex = 1
        '
        'btn_Cancel
        '
        Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Cancel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Cancel.Location = New System.Drawing.Point(124, 3)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(115, 24)
        Me.btn_Cancel.TabIndex = 1
        Me.btn_Cancel.Text = "btn_Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'btn_Appliquer
        '
        Me.btn_Appliquer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_Appliquer.Location = New System.Drawing.Point(3, 3)
        Me.btn_Appliquer.Name = "btn_Appliquer"
        Me.btn_Appliquer.Size = New System.Drawing.Size(115, 24)
        Me.btn_Appliquer.TabIndex = 0
        Me.btn_Appliquer.Text = "btn_Appliquer"
        Me.btn_Appliquer.UseVisualStyleBackColor = True
        '
        'pan_Contenu
        '
        Me.pan_Contenu.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Contenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Contenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Contenu.Location = New System.Drawing.Point(250, 3)
        Me.pan_Contenu.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.pan_Contenu.Name = "pan_Contenu"
        Me.pan_Contenu.Size = New System.Drawing.Size(547, 444)
        Me.pan_Contenu.TabIndex = 2
        '
        'PoMbutton2
        '
        Me.PoMbutton2.Caption = "PoMBtn_Gamma"
        Me.PoMbutton2.CaptionAlignement = System.Windows.Forms.HorizontalAlignment.Center
        Me.PoMbutton2.Checked = False
        Me.PoMbutton2.CouleurChecked = System.Drawing.Color.Orange
        Me.PoMbutton2.CouleurContour = System.Drawing.Color.Black
        Me.PoMbutton2.CouleurContourChecked = System.Drawing.Color.Black
        Me.PoMbutton2.CouleurContourMouseOn = System.Drawing.Color.Black
        Me.PoMbutton2.CouleurFond = System.Drawing.Color.WhiteSmoke
        Me.PoMbutton2.CouleurForGradient = System.Drawing.Color.WhiteSmoke
        Me.PoMbutton2.CouleurMouseOnBtn = System.Drawing.Color.Yellow
        Me.PoMbutton2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PoMbutton2.Enable = True
        Me.PoMbutton2.LContourFond = True
        Me.PoMbutton2.Location = New System.Drawing.Point(5, 105)
        Me.PoMbutton2.Margin = New System.Windows.Forms.Padding(5)
        Me.PoMbutton2.Name = "PoMbutton2"
        Me.PoMbutton2.RatioArrondi = 0!
        Me.PoMbutton2.Size = New System.Drawing.Size(232, 40)
        Me.PoMbutton2.TabIndex = 3
        '
        'PoMbtn_Scope
        '
        Me.PoMbtn_Scope.Caption = "PoMbtn_Scope"
        Me.PoMbtn_Scope.CaptionAlignement = System.Windows.Forms.HorizontalAlignment.Center
        Me.PoMbtn_Scope.Checked = False
        Me.PoMbtn_Scope.CouleurChecked = System.Drawing.Color.Orange
        Me.PoMbtn_Scope.CouleurContour = System.Drawing.Color.Black
        Me.PoMbtn_Scope.CouleurContourChecked = System.Drawing.Color.Black
        Me.PoMbtn_Scope.CouleurContourMouseOn = System.Drawing.Color.Black
        Me.PoMbtn_Scope.CouleurFond = System.Drawing.Color.WhiteSmoke
        Me.PoMbtn_Scope.CouleurForGradient = System.Drawing.Color.WhiteSmoke
        Me.PoMbtn_Scope.CouleurMouseOnBtn = System.Drawing.Color.Yellow
        Me.PoMbtn_Scope.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PoMbtn_Scope.Enable = True
        Me.PoMbtn_Scope.LContourFond = True
        Me.PoMbtn_Scope.Location = New System.Drawing.Point(5, 55)
        Me.PoMbtn_Scope.Margin = New System.Windows.Forms.Padding(5)
        Me.PoMbtn_Scope.Name = "PoMbtn_Scope"
        Me.PoMbtn_Scope.RatioArrondi = 0!
        Me.PoMbtn_Scope.Size = New System.Drawing.Size(232, 40)
        Me.PoMbtn_Scope.TabIndex = 2
        '
        'PoMBtn_Gamma
        '
        Me.PoMBtn_Gamma.Caption = "PoMBtn_Gamma"
        Me.PoMBtn_Gamma.CaptionAlignement = System.Windows.Forms.HorizontalAlignment.Center
        Me.PoMBtn_Gamma.Checked = False
        Me.PoMBtn_Gamma.CouleurChecked = System.Drawing.Color.Orange
        Me.PoMBtn_Gamma.CouleurContour = System.Drawing.Color.Black
        Me.PoMBtn_Gamma.CouleurContourChecked = System.Drawing.Color.Black
        Me.PoMBtn_Gamma.CouleurContourMouseOn = System.Drawing.Color.Black
        Me.PoMBtn_Gamma.CouleurFond = System.Drawing.Color.WhiteSmoke
        Me.PoMBtn_Gamma.CouleurForGradient = System.Drawing.Color.WhiteSmoke
        Me.PoMBtn_Gamma.CouleurMouseOnBtn = System.Drawing.Color.Yellow
        Me.PoMBtn_Gamma.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PoMBtn_Gamma.Enable = True
        Me.PoMBtn_Gamma.LContourFond = True
        Me.PoMBtn_Gamma.Location = New System.Drawing.Point(5, 5)
        Me.PoMBtn_Gamma.Margin = New System.Windows.Forms.Padding(5)
        Me.PoMBtn_Gamma.Name = "PoMBtn_Gamma"
        Me.PoMBtn_Gamma.RatioArrondi = 0!
        Me.PoMBtn_Gamma.Size = New System.Drawing.Size(232, 40)
        Me.PoMBtn_Gamma.TabIndex = 0
        '
        'Frm_OptionsCalcul
        '
        Me.AcceptButton = Me.btn_Appliquer
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Cancel
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pan_Main)
        Me.Name = "Frm_OptionsCalcul"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_OptionsCalcul"
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.TLpan_PourLesBoutons.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents pan_Gauche As Panel
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents PoMBtn_Gamma As POMbutton
    Friend WithEvents pan_Contenu As Panel
    Friend WithEvents TLpan_PourLesBoutons As TableLayoutPanel
    Friend WithEvents btn_Cancel As Button
    Friend WithEvents btn_Appliquer As Button
    Friend WithEvents PoMbutton2 As POMbutton
    Friend WithEvents PoMbtn_Scope As POMbutton
End Class
