<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Juridique
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Juridique))
        Me.TableLayoutPanel_Contenu = New System.Windows.Forms.TableLayoutPanel()
        Me.Btn_Accepter = New System.Windows.Forms.Button()
        Me.Label_Juridique = New System.Windows.Forms.Label()
        Me.Panel_Langue = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel_Langue = New System.Windows.Forms.TableLayoutPanel()
        Me.RadioButton_Fr = New System.Windows.Forms.RadioButton()
        Me.RadioButton_En = New System.Windows.Forms.RadioButton()
        Me.Label_Langue = New System.Windows.Forms.Label()
        Me.Panel_Juridique = New System.Windows.Forms.Panel()
        Me.Label_InfoJuridique = New System.Windows.Forms.Label()
        Me.Btn_Quitter = New System.Windows.Forms.Button()
        Me.PictureBox_CTICM = New System.Windows.Forms.PictureBox()
        Me.TableLayoutPanel_Contenu.SuspendLayout()
        Me.Panel_Langue.SuspendLayout()
        Me.TableLayoutPanel_Langue.SuspendLayout()
        Me.Panel_Juridique.SuspendLayout()
        CType(Me.PictureBox_CTICM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel_Contenu
        '
        Me.TableLayoutPanel_Contenu.ColumnCount = 2
        Me.TableLayoutPanel_Contenu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.0!))
        Me.TableLayoutPanel_Contenu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel_Contenu.Controls.Add(Me.Btn_Accepter, 1, 3)
        Me.TableLayoutPanel_Contenu.Controls.Add(Me.Label_Juridique, 0, 1)
        Me.TableLayoutPanel_Contenu.Controls.Add(Me.Panel_Langue, 1, 0)
        Me.TableLayoutPanel_Contenu.Controls.Add(Me.Panel_Juridique, 0, 2)
        Me.TableLayoutPanel_Contenu.Controls.Add(Me.Btn_Quitter, 0, 3)
        Me.TableLayoutPanel_Contenu.Controls.Add(Me.PictureBox_CTICM, 0, 0)
        Me.TableLayoutPanel_Contenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Contenu.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel_Contenu.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
        Me.TableLayoutPanel_Contenu.Name = "TableLayoutPanel_Contenu"
        Me.TableLayoutPanel_Contenu.RowCount = 4
        Me.TableLayoutPanel_Contenu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel_Contenu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanel_Contenu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.TableLayoutPanel_Contenu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel_Contenu.Size = New System.Drawing.Size(484, 471)
        Me.TableLayoutPanel_Contenu.TabIndex = 2
        '
        'Btn_Accepter
        '
        Me.Btn_Accepter.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.Btn_Accepter.Dock = System.Windows.Forms.DockStyle.Right
        Me.Btn_Accepter.Location = New System.Drawing.Point(388, 434)
        Me.Btn_Accepter.Margin = New System.Windows.Forms.Padding(3, 3, 6, 6)
        Me.Btn_Accepter.Name = "Btn_Accepter"
        Me.Btn_Accepter.Size = New System.Drawing.Size(90, 31)
        Me.Btn_Accepter.TabIndex = 1
        Me.Btn_Accepter.Text = "btn_Accepter"
        Me.Btn_Accepter.UseVisualStyleBackColor = True
        '
        'Label_Juridique
        '
        Me.Label_Juridique.AutoEllipsis = True
        Me.Label_Juridique.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label_Juridique.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel_Contenu.SetColumnSpan(Me.Label_Juridique, 2)
        Me.Label_Juridique.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label_Juridique.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label_Juridique.Location = New System.Drawing.Point(6, 164)
        Me.Label_Juridique.Margin = New System.Windows.Forms.Padding(6, 3, 6, 0)
        Me.Label_Juridique.Name = "Label_Juridique"
        Me.Label_Juridique.Size = New System.Drawing.Size(472, 25)
        Me.Label_Juridique.TabIndex = 1
        Me.Label_Juridique.Text = "Label_Juridique"
        Me.Label_Juridique.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel_Langue
        '
        Me.Panel_Langue.BackColor = System.Drawing.Color.White
        Me.Panel_Langue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Langue.Controls.Add(Me.TableLayoutPanel_Langue)
        Me.Panel_Langue.Controls.Add(Me.Label_Langue)
        Me.Panel_Langue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Langue.Location = New System.Drawing.Point(317, 6)
        Me.Panel_Langue.Margin = New System.Windows.Forms.Padding(3, 6, 6, 3)
        Me.Panel_Langue.Name = "Panel_Langue"
        Me.Panel_Langue.Size = New System.Drawing.Size(161, 152)
        Me.Panel_Langue.TabIndex = 1
        '
        'TableLayoutPanel_Langue
        '
        Me.TableLayoutPanel_Langue.ColumnCount = 1
        Me.TableLayoutPanel_Langue.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel_Langue.Controls.Add(Me.RadioButton_Fr, 0, 0)
        Me.TableLayoutPanel_Langue.Controls.Add(Me.RadioButton_En, 0, 1)
        Me.TableLayoutPanel_Langue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel_Langue.Location = New System.Drawing.Point(0, 25)
        Me.TableLayoutPanel_Langue.Name = "TableLayoutPanel_Langue"
        Me.TableLayoutPanel_Langue.RowCount = 3
        Me.TableLayoutPanel_Langue.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Langue.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Langue.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel_Langue.Size = New System.Drawing.Size(159, 125)
        Me.TableLayoutPanel_Langue.TabIndex = 4
        '
        'RadioButton_Fr
        '
        Me.RadioButton_Fr.AutoEllipsis = True
        Me.RadioButton_Fr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_Fr.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RadioButton_Fr.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton_Fr.Name = "RadioButton_Fr"
        Me.RadioButton_Fr.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.RadioButton_Fr.Size = New System.Drawing.Size(153, 35)
        Me.RadioButton_Fr.TabIndex = 2
        Me.RadioButton_Fr.Text = "RadioButton_Fr"
        Me.RadioButton_Fr.UseVisualStyleBackColor = True
        '
        'RadioButton_En
        '
        Me.RadioButton_En.AutoEllipsis = True
        Me.RadioButton_En.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton_En.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadioButton_En.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RadioButton_En.Location = New System.Drawing.Point(3, 44)
        Me.RadioButton_En.Name = "RadioButton_En"
        Me.RadioButton_En.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.RadioButton_En.Size = New System.Drawing.Size(153, 35)
        Me.RadioButton_En.TabIndex = 3
        Me.RadioButton_En.Text = "RadioButton_En"
        Me.RadioButton_En.UseVisualStyleBackColor = False
        '
        'Label_Langue
        '
        Me.Label_Langue.AutoEllipsis = True
        Me.Label_Langue.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Label_Langue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label_Langue.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label_Langue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label_Langue.Location = New System.Drawing.Point(0, 0)
        Me.Label_Langue.Name = "Label_Langue"
        Me.Label_Langue.Size = New System.Drawing.Size(159, 25)
        Me.Label_Langue.TabIndex = 1
        Me.Label_Langue.Text = "Label_Langue"
        Me.Label_Langue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel_Juridique
        '
        Me.Panel_Juridique.AutoScroll = True
        Me.Panel_Juridique.BackColor = System.Drawing.Color.White
        Me.Panel_Juridique.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TableLayoutPanel_Contenu.SetColumnSpan(Me.Panel_Juridique, 2)
        Me.Panel_Juridique.Controls.Add(Me.Label_InfoJuridique)
        Me.Panel_Juridique.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_Juridique.Location = New System.Drawing.Point(6, 189)
        Me.Panel_Juridique.Margin = New System.Windows.Forms.Padding(6, 0, 6, 3)
        Me.Panel_Juridique.Name = "Panel_Juridique"
        Me.Panel_Juridique.Size = New System.Drawing.Size(472, 239)
        Me.Panel_Juridique.TabIndex = 2
        '
        'Label_InfoJuridique
        '
        Me.Label_InfoJuridique.AutoSize = True
        Me.Label_InfoJuridique.Location = New System.Drawing.Point(0, 0)
        Me.Label_InfoJuridique.Margin = New System.Windows.Forms.Padding(0)
        Me.Label_InfoJuridique.MaximumSize = New System.Drawing.Size(450, 800)
        Me.Label_InfoJuridique.Name = "Label_InfoJuridique"
        Me.Label_InfoJuridique.Padding = New System.Windows.Forms.Padding(10, 0, 0, 10)
        Me.Label_InfoJuridique.Size = New System.Drawing.Size(109, 23)
        Me.Label_InfoJuridique.TabIndex = 3
        Me.Label_InfoJuridique.Text = "Label_InfoJuridique"
        '
        'Btn_Quitter
        '
        Me.Btn_Quitter.DialogResult = System.Windows.Forms.DialogResult.No
        Me.Btn_Quitter.Dock = System.Windows.Forms.DockStyle.Left
        Me.Btn_Quitter.Location = New System.Drawing.Point(6, 434)
        Me.Btn_Quitter.Margin = New System.Windows.Forms.Padding(6, 3, 3, 6)
        Me.Btn_Quitter.Name = "Btn_Quitter"
        Me.Btn_Quitter.Size = New System.Drawing.Size(90, 31)
        Me.Btn_Quitter.TabIndex = 2
        Me.Btn_Quitter.Text = "btn_Quitter"
        Me.Btn_Quitter.UseVisualStyleBackColor = True
        '
        'PictureBox_CTICM
        '
        Me.PictureBox_CTICM.BackColor = System.Drawing.Color.White
        Me.PictureBox_CTICM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox_CTICM.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox_CTICM.Image = CType(resources.GetObject("PictureBox_CTICM.Image"), System.Drawing.Image)
        Me.PictureBox_CTICM.Location = New System.Drawing.Point(6, 6)
        Me.PictureBox_CTICM.Margin = New System.Windows.Forms.Padding(6, 6, 3, 3)
        Me.PictureBox_CTICM.Name = "PictureBox_CTICM"
        Me.PictureBox_CTICM.Size = New System.Drawing.Size(305, 152)
        Me.PictureBox_CTICM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox_CTICM.TabIndex = 3
        Me.PictureBox_CTICM.TabStop = False
        '
        'Frm_Juridique
        '
        Me.AcceptButton = Me.Btn_Accepter
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Btn_Quitter
        Me.ClientSize = New System.Drawing.Size(484, 471)
        Me.Controls.Add(Me.TableLayoutPanel_Contenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_Juridique"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Frm_Juridique"
        Me.TableLayoutPanel_Contenu.ResumeLayout(False)
        Me.Panel_Langue.ResumeLayout(False)
        Me.TableLayoutPanel_Langue.ResumeLayout(False)
        Me.Panel_Juridique.ResumeLayout(False)
        Me.Panel_Juridique.PerformLayout()
        CType(Me.PictureBox_CTICM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanel_Contenu As TableLayoutPanel
    Friend WithEvents Btn_Accepter As Button
    Friend WithEvents Label_Juridique As Label
    Friend WithEvents Panel_Langue As Panel
    Friend WithEvents TableLayoutPanel_Langue As TableLayoutPanel
    Friend WithEvents RadioButton_Fr As RadioButton
    Friend WithEvents RadioButton_En As RadioButton
    Friend WithEvents Label_Langue As Label
    Friend WithEvents Panel_Juridique As Panel
    Friend WithEvents Label_InfoJuridique As Label
    Friend WithEvents Btn_Quitter As Button
    Friend WithEvents PictureBox_CTICM As PictureBox
End Class
