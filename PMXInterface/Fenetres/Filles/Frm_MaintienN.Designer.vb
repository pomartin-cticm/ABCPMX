<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MaintienN
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
        Me.TSpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Maintiens = New System.Windows.Forms.Label()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.TLpan_ChoixEtat = New System.Windows.Forms.TableLayoutPanel()
        Me.PoMbtn_Construction = New PMXInterface.POMbutton()
        Me.PoMbtn_Normal = New PMXInterface.POMbutton()
        Me.pan_Test = New System.Windows.Forms.Panel()
        Me.TSpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.TLpan_ChoixEtat.SuspendLayout()
        Me.SuspendLayout()
        '
        'TSpan_Main
        '
        Me.TSpan_Main.ColumnCount = 1
        Me.TSpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TSpan_Main.Controls.Add(Me.lbl_Maintiens, 0, 0)
        Me.TSpan_Main.Controls.Add(Me.TLPan_PartieBasse, 0, 3)
        Me.TSpan_Main.Controls.Add(Me.TLpan_ChoixEtat, 0, 1)
        Me.TSpan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TSpan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TSpan_Main.Margin = New System.Windows.Forms.Padding(0)
        Me.TSpan_Main.Name = "TSpan_Main"
        Me.TSpan_Main.RowCount = 4
        Me.TSpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TSpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TSpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TSpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TSpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TSpan_Main.Size = New System.Drawing.Size(1014, 531)
        Me.TSpan_Main.TabIndex = 0
        '
        'lbl_Maintiens
        '
        Me.lbl_Maintiens.AutoSize = True
        Me.lbl_Maintiens.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Maintiens.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Maintiens.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Maintiens.Location = New System.Drawing.Point(1, 0)
        Me.lbl_Maintiens.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.lbl_Maintiens.Name = "lbl_Maintiens"
        Me.lbl_Maintiens.Size = New System.Drawing.Size(1012, 30)
        Me.lbl_Maintiens.TabIndex = 2
        Me.lbl_Maintiens.Text = "lbl_Maintiens"
        Me.lbl_Maintiens.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.TLPan_PartieBasse.Controls.Add(Me.pan_Test, 0, 0)
        Me.TLPan_PartieBasse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 494)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1008, 34)
        Me.TLPan_PartieBasse.TabIndex = 1
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(517, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(377, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'TLpan_ChoixEtat
        '
        Me.TLpan_ChoixEtat.ColumnCount = 3
        Me.TLpan_ChoixEtat.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLpan_ChoixEtat.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLpan_ChoixEtat.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_ChoixEtat.Controls.Add(Me.PoMbtn_Construction, 0, 0)
        Me.TLpan_ChoixEtat.Controls.Add(Me.PoMbtn_Normal, 0, 0)
        Me.TLpan_ChoixEtat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_ChoixEtat.Location = New System.Drawing.Point(0, 30)
        Me.TLpan_ChoixEtat.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_ChoixEtat.Name = "TLpan_ChoixEtat"
        Me.TLpan_ChoixEtat.RowCount = 1
        Me.TLpan_ChoixEtat.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_ChoixEtat.Size = New System.Drawing.Size(1014, 40)
        Me.TLpan_ChoixEtat.TabIndex = 3
        '
        'PoMbtn_Construction
        '
        Me.PoMbtn_Construction.Caption = "PoMbtn_Construction"
        Me.PoMbtn_Construction.CaptionAlignement = System.Windows.Forms.HorizontalAlignment.Center
        Me.PoMbtn_Construction.Checked = False
        Me.PoMbtn_Construction.CouleurChecked = System.Drawing.Color.Orange
        Me.PoMbtn_Construction.CouleurContour = System.Drawing.Color.Black
        Me.PoMbtn_Construction.CouleurContourChecked = System.Drawing.Color.Black
        Me.PoMbtn_Construction.CouleurContourMouseOn = System.Drawing.Color.Black
        Me.PoMbtn_Construction.CouleurFond = System.Drawing.Color.WhiteSmoke
        Me.PoMbtn_Construction.CouleurForGradient = System.Drawing.Color.WhiteSmoke
        Me.PoMbtn_Construction.CouleurMouseOnBtn = System.Drawing.Color.Yellow
        Me.PoMbtn_Construction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PoMbtn_Construction.Enable = True
        Me.PoMbtn_Construction.LContourFond = True
        Me.PoMbtn_Construction.Location = New System.Drawing.Point(253, 3)
        Me.PoMbtn_Construction.Name = "PoMbtn_Construction"
        Me.PoMbtn_Construction.RatioArrondi = 0!
        Me.PoMbtn_Construction.Size = New System.Drawing.Size(244, 34)
        Me.PoMbtn_Construction.TabIndex = 7
        '
        'PoMbtn_Normal
        '
        Me.PoMbtn_Normal.Caption = "PoMbtn_Normal"
        Me.PoMbtn_Normal.CaptionAlignement = System.Windows.Forms.HorizontalAlignment.Center
        Me.PoMbtn_Normal.Checked = False
        Me.PoMbtn_Normal.CouleurChecked = System.Drawing.Color.Orange
        Me.PoMbtn_Normal.CouleurContour = System.Drawing.Color.Black
        Me.PoMbtn_Normal.CouleurContourChecked = System.Drawing.Color.Black
        Me.PoMbtn_Normal.CouleurContourMouseOn = System.Drawing.Color.Black
        Me.PoMbtn_Normal.CouleurFond = System.Drawing.Color.WhiteSmoke
        Me.PoMbtn_Normal.CouleurForGradient = System.Drawing.Color.WhiteSmoke
        Me.PoMbtn_Normal.CouleurMouseOnBtn = System.Drawing.Color.Yellow
        Me.PoMbtn_Normal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PoMbtn_Normal.Enable = True
        Me.PoMbtn_Normal.LContourFond = True
        Me.PoMbtn_Normal.Location = New System.Drawing.Point(3, 3)
        Me.PoMbtn_Normal.Name = "PoMbtn_Normal"
        Me.PoMbtn_Normal.RatioArrondi = 0!
        Me.PoMbtn_Normal.Size = New System.Drawing.Size(244, 34)
        Me.PoMbtn_Normal.TabIndex = 6
        '
        'pan_Test
        '
        Me.pan_Test.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Test.Location = New System.Drawing.Point(3, 3)
        Me.pan_Test.Name = "pan_Test"
        Me.pan_Test.Size = New System.Drawing.Size(14, 14)
        Me.pan_Test.TabIndex = 2
        Me.pan_Test.Visible = False
        '
        'Frm_MaintienN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1014, 531)
        Me.Controls.Add(Me.TSpan_Main)
        Me.Name = "Frm_MaintienN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_MaintienN"
        Me.TSpan_Main.ResumeLayout(False)
        Me.TSpan_Main.PerformLayout()
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.TLpan_ChoixEtat.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TSpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents lbl_Maintiens As Label
    Friend WithEvents TLpan_ChoixEtat As TableLayoutPanel
    Friend WithEvents PoMbtn_Construction As POMbutton
    Friend WithEvents PoMbtn_Normal As POMbutton
    Friend WithEvents pan_Test As Panel
End Class
