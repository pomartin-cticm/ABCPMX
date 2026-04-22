<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CalculEnCours
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
        Me.TLP_Contenu = New System.Windows.Forms.TableLayoutPanel()
        Me.LBL_Titre = New System.Windows.Forms.Label()
        Me.PNL_Contenu = New System.Windows.Forms.Panel()
        Me.LBL_ProgressionDansEtape = New System.Windows.Forms.Label()
        Me.LBL_ProgressionEtape = New System.Windows.Forms.Label()
        Me.PGB_Etape = New System.Windows.Forms.ProgressBar()
        Me.PGB_DansEtape = New System.Windows.Forms.ProgressBar()
        Me.TLP_Contenu.SuspendLayout()
        Me.PNL_Contenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'TLP_Contenu
        '
        Me.TLP_Contenu.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TLP_Contenu.ColumnCount = 3
        Me.TLP_Contenu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLP_Contenu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLP_Contenu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLP_Contenu.Controls.Add(Me.LBL_Titre, 0, 0)
        Me.TLP_Contenu.Controls.Add(Me.PNL_Contenu, 1, 1)
        Me.TLP_Contenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLP_Contenu.Location = New System.Drawing.Point(0, 0)
        Me.TLP_Contenu.Name = "TLP_Contenu"
        Me.TLP_Contenu.RowCount = 2
        Me.TLP_Contenu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLP_Contenu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLP_Contenu.Size = New System.Drawing.Size(433, 174)
        Me.TLP_Contenu.TabIndex = 2
        Me.TLP_Contenu.UseWaitCursor = True
        '
        'LBL_Titre
        '
        Me.LBL_Titre.AutoSize = True
        Me.LBL_Titre.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.LBL_Titre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TLP_Contenu.SetColumnSpan(Me.LBL_Titre, 3)
        Me.LBL_Titre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LBL_Titre.Location = New System.Drawing.Point(0, 0)
        Me.LBL_Titre.Margin = New System.Windows.Forms.Padding(0)
        Me.LBL_Titre.Name = "LBL_Titre"
        Me.LBL_Titre.Size = New System.Drawing.Size(433, 30)
        Me.LBL_Titre.TabIndex = 2
        Me.LBL_Titre.Text = "LBL_Titre"
        Me.LBL_Titre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LBL_Titre.UseWaitCursor = True
        '
        'PNL_Contenu
        '
        Me.PNL_Contenu.Controls.Add(Me.LBL_ProgressionDansEtape)
        Me.PNL_Contenu.Controls.Add(Me.LBL_ProgressionEtape)
        Me.PNL_Contenu.Controls.Add(Me.PGB_Etape)
        Me.PNL_Contenu.Controls.Add(Me.PGB_DansEtape)
        Me.PNL_Contenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PNL_Contenu.Location = New System.Drawing.Point(30, 30)
        Me.PNL_Contenu.Margin = New System.Windows.Forms.Padding(0)
        Me.PNL_Contenu.Name = "PNL_Contenu"
        Me.PNL_Contenu.Size = New System.Drawing.Size(373, 144)
        Me.PNL_Contenu.TabIndex = 1
        Me.PNL_Contenu.UseWaitCursor = True
        '
        'LBL_ProgressionDansEtape
        '
        Me.LBL_ProgressionDansEtape.AutoSize = True
        Me.LBL_ProgressionDansEtape.Location = New System.Drawing.Point(-3, 80)
        Me.LBL_ProgressionDansEtape.Name = "LBL_ProgressionDansEtape"
        Me.LBL_ProgressionDansEtape.Size = New System.Drawing.Size(0, 13)
        Me.LBL_ProgressionDansEtape.TabIndex = 5
        Me.LBL_ProgressionDansEtape.UseWaitCursor = True
        '
        'LBL_ProgressionEtape
        '
        Me.LBL_ProgressionEtape.AutoSize = True
        Me.LBL_ProgressionEtape.Location = New System.Drawing.Point(-3, 25)
        Me.LBL_ProgressionEtape.Name = "LBL_ProgressionEtape"
        Me.LBL_ProgressionEtape.Size = New System.Drawing.Size(0, 13)
        Me.LBL_ProgressionEtape.TabIndex = 4
        Me.LBL_ProgressionEtape.UseWaitCursor = True
        '
        'PGB_Etape
        '
        Me.PGB_Etape.Location = New System.Drawing.Point(3, 41)
        Me.PGB_Etape.Name = "PGB_Etape"
        Me.PGB_Etape.Size = New System.Drawing.Size(367, 23)
        Me.PGB_Etape.TabIndex = 3
        Me.PGB_Etape.UseWaitCursor = True
        '
        'PGB_DansEtape
        '
        Me.PGB_DansEtape.Location = New System.Drawing.Point(3, 96)
        Me.PGB_DansEtape.Name = "PGB_DansEtape"
        Me.PGB_DansEtape.Size = New System.Drawing.Size(367, 23)
        Me.PGB_DansEtape.TabIndex = 0
        Me.PGB_DansEtape.UseWaitCursor = True
        '
        'Frm_CalculEnCours
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.ClientSize = New System.Drawing.Size(433, 174)
        Me.ControlBox = False
        Me.Controls.Add(Me.TLP_Contenu)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_CalculEnCours"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_CalculEnCours"
        Me.UseWaitCursor = True
        Me.TLP_Contenu.ResumeLayout(False)
        Me.TLP_Contenu.PerformLayout()
        Me.PNL_Contenu.ResumeLayout(False)
        Me.PNL_Contenu.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TLP_Contenu As TableLayoutPanel
    Friend WithEvents PNL_Contenu As Panel
    Friend WithEvents PGB_DansEtape As ProgressBar
    Friend WithEvents PGB_Etape As ProgressBar
    Friend WithEvents LBL_ProgressionEtape As Label
    Friend WithEvents LBL_ProgressionDansEtape As Label
    Friend WithEvents LBL_Titre As Label
End Class
