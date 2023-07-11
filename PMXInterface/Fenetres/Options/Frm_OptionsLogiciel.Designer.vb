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
        Me.pan_Main = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_Gauche = New System.Windows.Forms.TableLayoutPanel()
        Me.PoMBtn_General = New PMXInterface.POMbutton()
        Me.pan_Gauche = New System.Windows.Forms.Panel()
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLpan_Gauche.SuspendLayout()
        Me.pan_Gauche.SuspendLayout()
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
        'TLpan_Gauche
        '
        Me.TLpan_Gauche.ColumnCount = 1
        Me.TLpan_Gauche.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Gauche.Controls.Add(Me.PoMBtn_General, 0, 0)
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
        Me.TLpan_Gauche.Size = New System.Drawing.Size(249, 399)
        Me.TLpan_Gauche.TabIndex = 0
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
        'Frm_OptionsLogiciel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 399)
        Me.Controls.Add(Me.pan_Main)
        Me.Name = "Frm_OptionsLogiciel"
        Me.Text = "Frm_OptionsLogiciel"
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLpan_Gauche.ResumeLayout(False)
        Me.pan_Gauche.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLpan_Gauche As TableLayoutPanel
    Friend WithEvents PoMBtn_General As POMbutton
    Friend WithEvents pan_Gauche As Panel
End Class
