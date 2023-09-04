<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Chargement
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.pan_General = New System.Windows.Forms.Panel()
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieMilieu = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_Dessin = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_Dessin = New System.Windows.Forms.Label()
        Me.pan_Dessin = New System.Windows.Forms.Panel()
        Me.TLPan_ReactionsAppuis = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_ReactionsAppuis = New System.Windows.Forms.Label()
        Me.pan_ReactionsAppuis = New System.Windows.Forms.Panel()
        Me.TLPan_PartieHaute = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_ChargesLineiques = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_ChargesLineiques = New System.Windows.Forms.Label()
        Me.pan_ChargesLineiques = New System.Windows.Forms.Panel()
        Me.TLPan_ChargesPonctuelles = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_ChargesPonctuelles = New System.Windows.Forms.Label()
        Me.pan_ChargesPonctuelles = New System.Windows.Forms.Panel()
        Me.TLPan_ChargesSurfaciques = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_ChargesSurfaciques = New System.Windows.Forms.Label()
        Me.pan_ChargesSurfaciques = New System.Windows.Forms.Panel()
        Me.TLPan_ChoixCharges = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_ChoixCharges = New System.Windows.Forms.Label()
        Me.pan_ChoixCharges = New System.Windows.Forms.Panel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.img_Connection = New System.Windows.Forms.PictureBox()
        Me.pan_General.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieMilieu.SuspendLayout()
        Me.TLPan_Dessin.SuspendLayout()
        Me.pan_Dessin.SuspendLayout()
        Me.TLPan_ReactionsAppuis.SuspendLayout()
        Me.TLPan_PartieHaute.SuspendLayout()
        Me.TLPan_ChargesLineiques.SuspendLayout()
        Me.TLPan_ChargesPonctuelles.SuspendLayout()
        Me.TLPan_ChargesSurfaciques.SuspendLayout()
        Me.TLPan_ChoixCharges.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        CType(Me.img_Connection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pan_General
        '
        Me.pan_General.Controls.Add(Me.TLpan_Main)
        Me.pan_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_General.Location = New System.Drawing.Point(0, 0)
        Me.pan_General.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_General.Name = "pan_General"
        Me.pan_General.Size = New System.Drawing.Size(1031, 602)
        Me.pan_General.TabIndex = 3
        '
        'TLpan_Main
        '
        Me.TLpan_Main.ColumnCount = 1
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Controls.Add(Me.TLPan_PartieMilieu, 0, 1)
        Me.TLpan_Main.Controls.Add(Me.TLPan_PartieHaute, 0, 0)
        Me.TLpan_Main.Controls.Add(Me.TLPan_PartieBasse, 0, 2)
        Me.TLpan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 3
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(1031, 602)
        Me.TLpan_Main.TabIndex = 0
        '
        'TLPan_PartieMilieu
        '
        Me.TLPan_PartieMilieu.ColumnCount = 2
        Me.TLPan_PartieMilieu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_PartieMilieu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieMilieu.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_PartieMilieu.Controls.Add(Me.TLPan_Dessin, 0, 0)
        Me.TLPan_PartieMilieu.Controls.Add(Me.TLPan_ReactionsAppuis, 0, 0)
        Me.TLPan_PartieMilieu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieMilieu.Location = New System.Drawing.Point(0, 281)
        Me.TLPan_PartieMilieu.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_PartieMilieu.Name = "TLPan_PartieMilieu"
        Me.TLPan_PartieMilieu.RowCount = 1
        Me.TLPan_PartieMilieu.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieMilieu.Size = New System.Drawing.Size(1031, 281)
        Me.TLPan_PartieMilieu.TabIndex = 1
        '
        'TLPan_Dessin
        '
        Me.TLPan_Dessin.ColumnCount = 1
        Me.TLPan_Dessin.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_Dessin.Controls.Add(Me.lbl_Dessin, 0, 0)
        Me.TLPan_Dessin.Controls.Add(Me.pan_Dessin, 0, 1)
        Me.TLPan_Dessin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_Dessin.Location = New System.Drawing.Point(250, 0)
        Me.TLPan_Dessin.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_Dessin.Name = "TLPan_Dessin"
        Me.TLPan_Dessin.RowCount = 2
        Me.TLPan_Dessin.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_Dessin.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_Dessin.Size = New System.Drawing.Size(781, 281)
        Me.TLPan_Dessin.TabIndex = 4
        '
        'lbl_Dessin
        '
        Me.lbl_Dessin.AutoSize = True
        Me.lbl_Dessin.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_Dessin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_Dessin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_Dessin.Location = New System.Drawing.Point(0, 0)
        Me.lbl_Dessin.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_Dessin.Name = "lbl_Dessin"
        Me.lbl_Dessin.Size = New System.Drawing.Size(781, 30)
        Me.lbl_Dessin.TabIndex = 0
        Me.lbl_Dessin.Text = "lbl_Dessin"
        Me.lbl_Dessin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_Dessin
        '
        Me.pan_Dessin.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Dessin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Dessin.Controls.Add(Me.img_Connection)
        Me.pan_Dessin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Dessin.Location = New System.Drawing.Point(0, 30)
        Me.pan_Dessin.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Dessin.Name = "pan_Dessin"
        Me.pan_Dessin.Size = New System.Drawing.Size(781, 251)
        Me.pan_Dessin.TabIndex = 1
        '
        'TLPan_ReactionsAppuis
        '
        Me.TLPan_ReactionsAppuis.ColumnCount = 1
        Me.TLPan_ReactionsAppuis.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_ReactionsAppuis.Controls.Add(Me.lbl_ReactionsAppuis, 0, 0)
        Me.TLPan_ReactionsAppuis.Controls.Add(Me.pan_ReactionsAppuis, 0, 1)
        Me.TLPan_ReactionsAppuis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_ReactionsAppuis.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_ReactionsAppuis.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_ReactionsAppuis.Name = "TLPan_ReactionsAppuis"
        Me.TLPan_ReactionsAppuis.RowCount = 2
        Me.TLPan_ReactionsAppuis.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_ReactionsAppuis.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_ReactionsAppuis.Size = New System.Drawing.Size(250, 281)
        Me.TLPan_ReactionsAppuis.TabIndex = 1
        '
        'lbl_ReactionsAppuis
        '
        Me.lbl_ReactionsAppuis.AutoSize = True
        Me.lbl_ReactionsAppuis.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_ReactionsAppuis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ReactionsAppuis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ReactionsAppuis.Location = New System.Drawing.Point(0, 0)
        Me.lbl_ReactionsAppuis.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ReactionsAppuis.Name = "lbl_ReactionsAppuis"
        Me.lbl_ReactionsAppuis.Size = New System.Drawing.Size(250, 30)
        Me.lbl_ReactionsAppuis.TabIndex = 0
        Me.lbl_ReactionsAppuis.Text = "lbl_ReactionsAppuis"
        Me.lbl_ReactionsAppuis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_ReactionsAppuis
        '
        Me.pan_ReactionsAppuis.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ReactionsAppuis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ReactionsAppuis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ReactionsAppuis.Location = New System.Drawing.Point(0, 30)
        Me.pan_ReactionsAppuis.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ReactionsAppuis.Name = "pan_ReactionsAppuis"
        Me.pan_ReactionsAppuis.Size = New System.Drawing.Size(250, 251)
        Me.pan_ReactionsAppuis.TabIndex = 1
        '
        'TLPan_PartieHaute
        '
        Me.TLPan_PartieHaute.ColumnCount = 4
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250.0!))
        Me.TLPan_PartieHaute.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 281.0!))
        Me.TLPan_PartieHaute.Controls.Add(Me.TLPan_ChargesLineiques, 2, 0)
        Me.TLPan_PartieHaute.Controls.Add(Me.TLPan_ChargesPonctuelles, 3, 0)
        Me.TLPan_PartieHaute.Controls.Add(Me.TLPan_ChargesSurfaciques, 1, 0)
        Me.TLPan_PartieHaute.Controls.Add(Me.TLPan_ChoixCharges, 0, 0)
        Me.TLPan_PartieHaute.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_PartieHaute.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_PartieHaute.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_PartieHaute.Name = "TLPan_PartieHaute"
        Me.TLPan_PartieHaute.RowCount = 1
        Me.TLPan_PartieHaute.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieHaute.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_PartieHaute.Size = New System.Drawing.Size(1031, 281)
        Me.TLPan_PartieHaute.TabIndex = 0
        '
        'TLPan_ChargesLineiques
        '
        Me.TLPan_ChargesLineiques.ColumnCount = 1
        Me.TLPan_ChargesLineiques.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_ChargesLineiques.Controls.Add(Me.lbl_ChargesLineiques, 0, 0)
        Me.TLPan_ChargesLineiques.Controls.Add(Me.pan_ChargesLineiques, 0, 1)
        Me.TLPan_ChargesLineiques.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_ChargesLineiques.Location = New System.Drawing.Point(500, 0)
        Me.TLPan_ChargesLineiques.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_ChargesLineiques.Name = "TLPan_ChargesLineiques"
        Me.TLPan_ChargesLineiques.RowCount = 2
        Me.TLPan_ChargesLineiques.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_ChargesLineiques.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_ChargesLineiques.Size = New System.Drawing.Size(250, 281)
        Me.TLPan_ChargesLineiques.TabIndex = 4
        '
        'lbl_ChargesLineiques
        '
        Me.lbl_ChargesLineiques.AutoSize = True
        Me.lbl_ChargesLineiques.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_ChargesLineiques.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ChargesLineiques.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ChargesLineiques.Location = New System.Drawing.Point(0, 0)
        Me.lbl_ChargesLineiques.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ChargesLineiques.Name = "lbl_ChargesLineiques"
        Me.lbl_ChargesLineiques.Size = New System.Drawing.Size(250, 30)
        Me.lbl_ChargesLineiques.TabIndex = 0
        Me.lbl_ChargesLineiques.Text = "lbl_ChargesLineiques"
        Me.lbl_ChargesLineiques.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_ChargesLineiques
        '
        Me.pan_ChargesLineiques.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ChargesLineiques.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ChargesLineiques.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ChargesLineiques.Location = New System.Drawing.Point(0, 30)
        Me.pan_ChargesLineiques.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ChargesLineiques.Name = "pan_ChargesLineiques"
        Me.pan_ChargesLineiques.Size = New System.Drawing.Size(250, 251)
        Me.pan_ChargesLineiques.TabIndex = 1
        '
        'TLPan_ChargesPonctuelles
        '
        Me.TLPan_ChargesPonctuelles.ColumnCount = 1
        Me.TLPan_ChargesPonctuelles.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_ChargesPonctuelles.Controls.Add(Me.lbl_ChargesPonctuelles, 0, 0)
        Me.TLPan_ChargesPonctuelles.Controls.Add(Me.pan_ChargesPonctuelles, 0, 1)
        Me.TLPan_ChargesPonctuelles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_ChargesPonctuelles.Location = New System.Drawing.Point(750, 0)
        Me.TLPan_ChargesPonctuelles.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_ChargesPonctuelles.Name = "TLPan_ChargesPonctuelles"
        Me.TLPan_ChargesPonctuelles.RowCount = 2
        Me.TLPan_ChargesPonctuelles.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_ChargesPonctuelles.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_ChargesPonctuelles.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_ChargesPonctuelles.Size = New System.Drawing.Size(281, 281)
        Me.TLPan_ChargesPonctuelles.TabIndex = 3
        '
        'lbl_ChargesPonctuelles
        '
        Me.lbl_ChargesPonctuelles.AutoSize = True
        Me.lbl_ChargesPonctuelles.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_ChargesPonctuelles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ChargesPonctuelles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ChargesPonctuelles.Location = New System.Drawing.Point(0, 0)
        Me.lbl_ChargesPonctuelles.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ChargesPonctuelles.Name = "lbl_ChargesPonctuelles"
        Me.lbl_ChargesPonctuelles.Size = New System.Drawing.Size(281, 30)
        Me.lbl_ChargesPonctuelles.TabIndex = 0
        Me.lbl_ChargesPonctuelles.Text = "lbl_ChargesPonctuelles"
        Me.lbl_ChargesPonctuelles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_ChargesPonctuelles
        '
        Me.pan_ChargesPonctuelles.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ChargesPonctuelles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ChargesPonctuelles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ChargesPonctuelles.Location = New System.Drawing.Point(0, 30)
        Me.pan_ChargesPonctuelles.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ChargesPonctuelles.Name = "pan_ChargesPonctuelles"
        Me.pan_ChargesPonctuelles.Size = New System.Drawing.Size(281, 251)
        Me.pan_ChargesPonctuelles.TabIndex = 1
        '
        'TLPan_ChargesSurfaciques
        '
        Me.TLPan_ChargesSurfaciques.ColumnCount = 1
        Me.TLPan_ChargesSurfaciques.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_ChargesSurfaciques.Controls.Add(Me.lbl_ChargesSurfaciques, 0, 0)
        Me.TLPan_ChargesSurfaciques.Controls.Add(Me.pan_ChargesSurfaciques, 0, 1)
        Me.TLPan_ChargesSurfaciques.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_ChargesSurfaciques.Location = New System.Drawing.Point(250, 0)
        Me.TLPan_ChargesSurfaciques.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_ChargesSurfaciques.Name = "TLPan_ChargesSurfaciques"
        Me.TLPan_ChargesSurfaciques.RowCount = 2
        Me.TLPan_ChargesSurfaciques.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_ChargesSurfaciques.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_ChargesSurfaciques.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLPan_ChargesSurfaciques.Size = New System.Drawing.Size(250, 281)
        Me.TLPan_ChargesSurfaciques.TabIndex = 2
        '
        'lbl_ChargesSurfaciques
        '
        Me.lbl_ChargesSurfaciques.AutoSize = True
        Me.lbl_ChargesSurfaciques.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_ChargesSurfaciques.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ChargesSurfaciques.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ChargesSurfaciques.Location = New System.Drawing.Point(0, 0)
        Me.lbl_ChargesSurfaciques.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ChargesSurfaciques.Name = "lbl_ChargesSurfaciques"
        Me.lbl_ChargesSurfaciques.Size = New System.Drawing.Size(250, 30)
        Me.lbl_ChargesSurfaciques.TabIndex = 0
        Me.lbl_ChargesSurfaciques.Text = "lbl_ChargesSurfaciques"
        Me.lbl_ChargesSurfaciques.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_ChargesSurfaciques
        '
        Me.pan_ChargesSurfaciques.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ChargesSurfaciques.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ChargesSurfaciques.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ChargesSurfaciques.Location = New System.Drawing.Point(0, 30)
        Me.pan_ChargesSurfaciques.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ChargesSurfaciques.Name = "pan_ChargesSurfaciques"
        Me.pan_ChargesSurfaciques.Size = New System.Drawing.Size(250, 251)
        Me.pan_ChargesSurfaciques.TabIndex = 1
        '
        'TLPan_ChoixCharges
        '
        Me.TLPan_ChoixCharges.ColumnCount = 1
        Me.TLPan_ChoixCharges.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_ChoixCharges.Controls.Add(Me.lbl_ChoixCharges, 0, 0)
        Me.TLPan_ChoixCharges.Controls.Add(Me.pan_ChoixCharges, 0, 1)
        Me.TLPan_ChoixCharges.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLPan_ChoixCharges.Location = New System.Drawing.Point(0, 0)
        Me.TLPan_ChoixCharges.Margin = New System.Windows.Forms.Padding(0)
        Me.TLPan_ChoixCharges.Name = "TLPan_ChoixCharges"
        Me.TLPan_ChoixCharges.RowCount = 2
        Me.TLPan_ChoixCharges.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TLPan_ChoixCharges.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170.0!))
        Me.TLPan_ChoixCharges.Size = New System.Drawing.Size(250, 281)
        Me.TLPan_ChoixCharges.TabIndex = 1
        '
        'lbl_ChoixCharges
        '
        Me.lbl_ChoixCharges.AutoSize = True
        Me.lbl_ChoixCharges.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.lbl_ChoixCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_ChoixCharges.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_ChoixCharges.Location = New System.Drawing.Point(0, 0)
        Me.lbl_ChoixCharges.Margin = New System.Windows.Forms.Padding(0)
        Me.lbl_ChoixCharges.Name = "lbl_ChoixCharges"
        Me.lbl_ChoixCharges.Size = New System.Drawing.Size(250, 30)
        Me.lbl_ChoixCharges.TabIndex = 0
        Me.lbl_ChoixCharges.Text = "lbl_ChoixCharges"
        Me.lbl_ChoixCharges.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pan_ChoixCharges
        '
        Me.pan_ChoixCharges.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_ChoixCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_ChoixCharges.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_ChoixCharges.Location = New System.Drawing.Point(0, 30)
        Me.pan_ChoixCharges.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_ChoixCharges.Name = "pan_ChoixCharges"
        Me.pan_ChoixCharges.Size = New System.Drawing.Size(250, 251)
        Me.pan_ChoixCharges.TabIndex = 1
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 565)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(1025, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(525, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(385, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'img_Connection
        '
        Me.img_Connection.Location = New System.Drawing.Point(19, 23)
        Me.img_Connection.Margin = New System.Windows.Forms.Padding(0)
        Me.img_Connection.Name = "img_Connection"
        Me.img_Connection.Size = New System.Drawing.Size(74, 57)
        Me.img_Connection.TabIndex = 76
        Me.img_Connection.TabStop = False
        '
        'Frm_Chargement
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(1031, 602)
        Me.Controls.Add(Me.pan_General)
        Me.Name = "Frm_Chargement"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_Chargement"
        Me.pan_General.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieMilieu.ResumeLayout(False)
        Me.TLPan_Dessin.ResumeLayout(False)
        Me.TLPan_Dessin.PerformLayout()
        Me.pan_Dessin.ResumeLayout(False)
        Me.TLPan_ReactionsAppuis.ResumeLayout(False)
        Me.TLPan_ReactionsAppuis.PerformLayout()
        Me.TLPan_PartieHaute.ResumeLayout(False)
        Me.TLPan_ChargesLineiques.ResumeLayout(False)
        Me.TLPan_ChargesLineiques.PerformLayout()
        Me.TLPan_ChargesPonctuelles.ResumeLayout(False)
        Me.TLPan_ChargesPonctuelles.PerformLayout()
        Me.TLPan_ChargesSurfaciques.ResumeLayout(False)
        Me.TLPan_ChargesSurfaciques.PerformLayout()
        Me.TLPan_ChoixCharges.ResumeLayout(False)
        Me.TLPan_ChoixCharges.PerformLayout()
        Me.TLPan_PartieBasse.ResumeLayout(False)
        CType(Me.img_Connection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_General As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents TLPan_PartieHaute As TableLayoutPanel
    Friend WithEvents TLPan_ChoixCharges As TableLayoutPanel
    Friend WithEvents lbl_ChoixCharges As Label
    Friend WithEvents pan_ChoixCharges As Panel
    Friend WithEvents TLPan_ChargesSurfaciques As TableLayoutPanel
    Friend WithEvents lbl_ChargesSurfaciques As Label
    Friend WithEvents pan_ChargesSurfaciques As Panel
    Friend WithEvents TLPan_ChargesLineiques As TableLayoutPanel
    Friend WithEvents lbl_ChargesLineiques As Label
    Friend WithEvents pan_ChargesLineiques As Panel
    Friend WithEvents TLPan_ChargesPonctuelles As TableLayoutPanel
    Friend WithEvents lbl_ChargesPonctuelles As Label
    Friend WithEvents pan_ChargesPonctuelles As Panel
    Friend WithEvents TLPan_PartieMilieu As TableLayoutPanel
    Friend WithEvents TLPan_Dessin As TableLayoutPanel
    Friend WithEvents lbl_Dessin As Label
    Friend WithEvents pan_Dessin As Panel
    Friend WithEvents TLPan_ReactionsAppuis As TableLayoutPanel
    Friend WithEvents lbl_ReactionsAppuis As Label
    Friend WithEvents pan_ReactionsAppuis As Panel
    Friend WithEvents img_Connection As PictureBox
End Class
