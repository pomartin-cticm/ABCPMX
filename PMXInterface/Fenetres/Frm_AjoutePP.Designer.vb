<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AjoutePP
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
        Me.TLpan_Main = New System.Windows.Forms.TableLayoutPanel()
        Me.TLPan_PartieBasse = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_OK = New System.Windows.Forms.Button()
        Me.btn_Annuler = New System.Windows.Forms.Button()
        Me.pan_Choix = New System.Windows.Forms.Panel()
        Me.TLpan_SeparationHorizontale = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_CadreSuperieur = New System.Windows.Forms.TableLayoutPanel()
        Me.pan_Poutre = New System.Windows.Forms.Panel()
        Me.lbl_NouvellePoutre = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.chk_PoutreEnCours = New System.Windows.Forms.CheckBox()
        Me.txt_NomNouvellePoutre = New System.Windows.Forms.TextBox()
        Me.chk_NouvellePoutre = New System.Windows.Forms.CheckBox()
        Me.pan_Projet = New System.Windows.Forms.Panel()
        Me.lbl_NouveauProjet = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.chk_ProjetEnCours = New System.Windows.Forms.CheckBox()
        Me.txt_NomNouveauProjet = New System.Windows.Forms.TextBox()
        Me.chk_NouveauProjet = New System.Windows.Forms.CheckBox()
        Me.pan_TypeSection = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TLpan_ChoixSection = New System.Windows.Forms.TableLayoutPanel()
        Me.chk_SFBMixte = New System.Windows.Forms.CheckBox()
        Me.chk_IFB_B_Acier = New System.Windows.Forms.CheckBox()
        Me.chk_IFB_A_Mixte = New System.Windows.Forms.CheckBox()
        Me.chk_SABAcier = New System.Windows.Forms.CheckBox()
        Me.chk_IFB_B_Mixte = New System.Windows.Forms.CheckBox()
        Me.chk_IFB_A_Acier = New System.Windows.Forms.CheckBox()
        Me.chk_SABMixte = New System.Windows.Forms.CheckBox()
        Me.chk_SFBAcier = New System.Windows.Forms.CheckBox()
        Me.chk_SectionMixte = New System.Windows.Forms.CheckBox()
        Me.chk_SectionMixteEnrobe = New System.Windows.Forms.CheckBox()
        Me.chk_SectionAcierEnrobe = New System.Windows.Forms.CheckBox()
        Me.chk_SectionAcier = New System.Windows.Forms.CheckBox()
        Me.lbl_TypeSection = New System.Windows.Forms.Label()
        Me.MyToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.pan_Main.SuspendLayout()
        Me.TLpan_Main.SuspendLayout()
        Me.TLPan_PartieBasse.SuspendLayout()
        Me.pan_Choix.SuspendLayout()
        Me.TLpan_SeparationHorizontale.SuspendLayout()
        Me.TLpan_CadreSuperieur.SuspendLayout()
        Me.pan_Poutre.SuspendLayout()
        Me.pan_Projet.SuspendLayout()
        Me.pan_TypeSection.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TLpan_ChoixSection.SuspendLayout()
        Me.SuspendLayout()
        '
        'pan_Main
        '
        Me.pan_Main.Controls.Add(Me.TLpan_Main)
        Me.pan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Main.Location = New System.Drawing.Point(0, 0)
        Me.pan_Main.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_Main.Name = "pan_Main"
        Me.pan_Main.Size = New System.Drawing.Size(701, 485)
        Me.pan_Main.TabIndex = 0
        '
        'TLpan_Main
        '
        Me.TLpan_Main.ColumnCount = 1
        Me.TLpan_Main.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.Controls.Add(Me.TLPan_PartieBasse, 0, 1)
        Me.TLpan_Main.Controls.Add(Me.pan_Choix, 0, 0)
        Me.TLpan_Main.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_Main.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_Main.Name = "TLpan_Main"
        Me.TLpan_Main.RowCount = 2
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_Main.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TLpan_Main.Size = New System.Drawing.Size(701, 485)
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
        Me.TLPan_PartieBasse.Location = New System.Drawing.Point(3, 448)
        Me.TLPan_PartieBasse.Name = "TLPan_PartieBasse"
        Me.TLPan_PartieBasse.RowCount = 1
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TLPan_PartieBasse.Size = New System.Drawing.Size(695, 34)
        Me.TLPan_PartieBasse.TabIndex = 0
        '
        'btn_OK
        '
        Me.btn_OK.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btn_OK.Location = New System.Drawing.Point(360, 3)
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
        Me.btn_Annuler.Location = New System.Drawing.Point(220, 3)
        Me.btn_Annuler.Name = "btn_Annuler"
        Me.btn_Annuler.Size = New System.Drawing.Size(114, 28)
        Me.btn_Annuler.TabIndex = 0
        Me.btn_Annuler.Text = "btn_Annuler"
        Me.btn_Annuler.UseVisualStyleBackColor = True
        '
        'pan_Choix
        '
        Me.pan_Choix.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.pan_Choix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Choix.Controls.Add(Me.TLpan_SeparationHorizontale)
        Me.pan_Choix.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Choix.Location = New System.Drawing.Point(3, 3)
        Me.pan_Choix.Name = "pan_Choix"
        Me.pan_Choix.Size = New System.Drawing.Size(695, 439)
        Me.pan_Choix.TabIndex = 1
        '
        'TLpan_SeparationHorizontale
        '
        Me.TLpan_SeparationHorizontale.ColumnCount = 1
        Me.TLpan_SeparationHorizontale.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_SeparationHorizontale.Controls.Add(Me.TLpan_CadreSuperieur, 0, 0)
        Me.TLpan_SeparationHorizontale.Controls.Add(Me.pan_TypeSection, 0, 1)
        Me.TLpan_SeparationHorizontale.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_SeparationHorizontale.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_SeparationHorizontale.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_SeparationHorizontale.Name = "TLpan_SeparationHorizontale"
        Me.TLpan_SeparationHorizontale.RowCount = 2
        Me.TLpan_SeparationHorizontale.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TLpan_SeparationHorizontale.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TLpan_SeparationHorizontale.Size = New System.Drawing.Size(693, 437)
        Me.TLpan_SeparationHorizontale.TabIndex = 0
        '
        'TLpan_CadreSuperieur
        '
        Me.TLpan_CadreSuperieur.ColumnCount = 2
        Me.TLpan_CadreSuperieur.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_CadreSuperieur.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_CadreSuperieur.Controls.Add(Me.pan_Poutre, 1, 0)
        Me.TLpan_CadreSuperieur.Controls.Add(Me.pan_Projet, 0, 0)
        Me.TLpan_CadreSuperieur.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_CadreSuperieur.Location = New System.Drawing.Point(0, 0)
        Me.TLpan_CadreSuperieur.Margin = New System.Windows.Forms.Padding(0)
        Me.TLpan_CadreSuperieur.Name = "TLpan_CadreSuperieur"
        Me.TLpan_CadreSuperieur.RowCount = 1
        Me.TLpan_CadreSuperieur.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TLpan_CadreSuperieur.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TLpan_CadreSuperieur.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TLpan_CadreSuperieur.Size = New System.Drawing.Size(693, 80)
        Me.TLpan_CadreSuperieur.TabIndex = 0
        '
        'pan_Poutre
        '
        Me.pan_Poutre.Controls.Add(Me.lbl_NouvellePoutre)
        Me.pan_Poutre.Controls.Add(Me.TextBox2)
        Me.pan_Poutre.Controls.Add(Me.chk_PoutreEnCours)
        Me.pan_Poutre.Controls.Add(Me.txt_NomNouvellePoutre)
        Me.pan_Poutre.Controls.Add(Me.chk_NouvellePoutre)
        Me.pan_Poutre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Poutre.Location = New System.Drawing.Point(349, 3)
        Me.pan_Poutre.Name = "pan_Poutre"
        Me.pan_Poutre.Size = New System.Drawing.Size(341, 74)
        Me.pan_Poutre.TabIndex = 1
        '
        'lbl_NouvellePoutre
        '
        Me.lbl_NouvellePoutre.AutoSize = True
        Me.lbl_NouvellePoutre.Location = New System.Drawing.Point(32, 18)
        Me.lbl_NouvellePoutre.Name = "lbl_NouvellePoutre"
        Me.lbl_NouvellePoutre.Size = New System.Drawing.Size(96, 13)
        Me.lbl_NouvellePoutre.TabIndex = 7
        Me.lbl_NouvellePoutre.Text = "lbl_NouvellePoutre"
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.Location = New System.Drawing.Point(167, 40)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(165, 20)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.Visible = False
        '
        'chk_PoutreEnCours
        '
        Me.chk_PoutreEnCours.AutoSize = True
        Me.chk_PoutreEnCours.Location = New System.Drawing.Point(14, 42)
        Me.chk_PoutreEnCours.Name = "chk_PoutreEnCours"
        Me.chk_PoutreEnCours.Size = New System.Drawing.Size(121, 17)
        Me.chk_PoutreEnCours.TabIndex = 2
        Me.chk_PoutreEnCours.Text = "chk_PoutreEnCours"
        Me.chk_PoutreEnCours.UseVisualStyleBackColor = True
        Me.chk_PoutreEnCours.Visible = False
        '
        'txt_NomNouvellePoutre
        '
        Me.txt_NomNouvellePoutre.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_NomNouvellePoutre.Location = New System.Drawing.Point(167, 14)
        Me.txt_NomNouvellePoutre.Name = "txt_NomNouvellePoutre"
        Me.txt_NomNouvellePoutre.Size = New System.Drawing.Size(165, 20)
        Me.txt_NomNouvellePoutre.TabIndex = 1
        '
        'chk_NouvellePoutre
        '
        Me.chk_NouvellePoutre.AutoSize = True
        Me.chk_NouvellePoutre.Location = New System.Drawing.Point(14, 16)
        Me.chk_NouvellePoutre.Name = "chk_NouvellePoutre"
        Me.chk_NouvellePoutre.Size = New System.Drawing.Size(123, 17)
        Me.chk_NouvellePoutre.TabIndex = 0
        Me.chk_NouvellePoutre.Text = "chk_NouvellePoutre"
        Me.chk_NouvellePoutre.UseVisualStyleBackColor = True
        '
        'pan_Projet
        '
        Me.pan_Projet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pan_Projet.Controls.Add(Me.lbl_NouveauProjet)
        Me.pan_Projet.Controls.Add(Me.TextBox1)
        Me.pan_Projet.Controls.Add(Me.chk_ProjetEnCours)
        Me.pan_Projet.Controls.Add(Me.txt_NomNouveauProjet)
        Me.pan_Projet.Controls.Add(Me.chk_NouveauProjet)
        Me.pan_Projet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_Projet.Location = New System.Drawing.Point(3, 3)
        Me.pan_Projet.Name = "pan_Projet"
        Me.pan_Projet.Size = New System.Drawing.Size(340, 74)
        Me.pan_Projet.TabIndex = 0
        '
        'lbl_NouveauProjet
        '
        Me.lbl_NouveauProjet.AutoSize = True
        Me.lbl_NouveauProjet.Location = New System.Drawing.Point(32, 17)
        Me.lbl_NouveauProjet.Name = "lbl_NouveauProjet"
        Me.lbl_NouveauProjet.Size = New System.Drawing.Size(94, 13)
        Me.lbl_NouveauProjet.TabIndex = 6
        Me.lbl_NouveauProjet.Text = "lbl_NouveauProjet"
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(167, 40)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(162, 20)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Visible = False
        '
        'chk_ProjetEnCours
        '
        Me.chk_ProjetEnCours.AutoSize = True
        Me.chk_ProjetEnCours.Location = New System.Drawing.Point(14, 42)
        Me.chk_ProjetEnCours.Name = "chk_ProjetEnCours"
        Me.chk_ProjetEnCours.Size = New System.Drawing.Size(117, 17)
        Me.chk_ProjetEnCours.TabIndex = 2
        Me.chk_ProjetEnCours.Text = "chk_ProjetEnCours"
        Me.chk_ProjetEnCours.UseVisualStyleBackColor = True
        Me.chk_ProjetEnCours.Visible = False
        '
        'txt_NomNouveauProjet
        '
        Me.txt_NomNouveauProjet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txt_NomNouveauProjet.Location = New System.Drawing.Point(167, 14)
        Me.txt_NomNouveauProjet.Name = "txt_NomNouveauProjet"
        Me.txt_NomNouveauProjet.Size = New System.Drawing.Size(162, 20)
        Me.txt_NomNouveauProjet.TabIndex = 1
        '
        'chk_NouveauProjet
        '
        Me.chk_NouveauProjet.AutoSize = True
        Me.chk_NouveauProjet.Location = New System.Drawing.Point(14, 16)
        Me.chk_NouveauProjet.Name = "chk_NouveauProjet"
        Me.chk_NouveauProjet.Size = New System.Drawing.Size(121, 17)
        Me.chk_NouveauProjet.TabIndex = 0
        Me.chk_NouveauProjet.Text = "chk_NouveauProjet"
        Me.chk_NouveauProjet.UseVisualStyleBackColor = True
        '
        'pan_TypeSection
        '
        Me.pan_TypeSection.AutoScroll = True
        Me.pan_TypeSection.Controls.Add(Me.TableLayoutPanel1)
        Me.pan_TypeSection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pan_TypeSection.Location = New System.Drawing.Point(0, 80)
        Me.pan_TypeSection.Margin = New System.Windows.Forms.Padding(0)
        Me.pan_TypeSection.Name = "pan_TypeSection"
        Me.pan_TypeSection.Size = New System.Drawing.Size(693, 357)
        Me.pan_TypeSection.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TLpan_ChoixSection, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_TypeSection, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(693, 357)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'TLpan_ChoixSection
        '
        Me.TLpan_ChoixSection.ColumnCount = 4
        Me.TLpan_ChoixSection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TLpan_ChoixSection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TLpan_ChoixSection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TLpan_ChoixSection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TLpan_ChoixSection.Controls.Add(Me.chk_SFBMixte, 0, 2)
        Me.TLpan_ChoixSection.Controls.Add(Me.chk_IFB_B_Acier, 2, 1)
        Me.TLpan_ChoixSection.Controls.Add(Me.chk_IFB_A_Mixte, 1, 2)
        Me.TLpan_ChoixSection.Controls.Add(Me.chk_SABAcier, 3, 1)
        Me.TLpan_ChoixSection.Controls.Add(Me.chk_IFB_B_Mixte, 2, 2)
        Me.TLpan_ChoixSection.Controls.Add(Me.chk_IFB_A_Acier, 1, 1)
        Me.TLpan_ChoixSection.Controls.Add(Me.chk_SABMixte, 3, 2)
        Me.TLpan_ChoixSection.Controls.Add(Me.chk_SFBAcier, 0, 1)
        Me.TLpan_ChoixSection.Controls.Add(Me.chk_SectionMixte, 2, 0)
        Me.TLpan_ChoixSection.Controls.Add(Me.chk_SectionMixteEnrobe, 3, 0)
        Me.TLpan_ChoixSection.Controls.Add(Me.chk_SectionAcierEnrobe, 1, 0)
        Me.TLpan_ChoixSection.Controls.Add(Me.chk_SectionAcier, 0, 0)
        Me.TLpan_ChoixSection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TLpan_ChoixSection.Location = New System.Drawing.Point(3, 33)
        Me.TLpan_ChoixSection.Name = "TLpan_ChoixSection"
        Me.TLpan_ChoixSection.RowCount = 3
        Me.TLpan_ChoixSection.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_ChoixSection.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_ChoixSection.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TLpan_ChoixSection.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TLpan_ChoixSection.Size = New System.Drawing.Size(687, 321)
        Me.TLpan_ChoixSection.TabIndex = 0
        '
        'chk_SFBMixte
        '
        Me.chk_SFBMixte.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_SFBMixte.AutoSize = True
        Me.chk_SFBMixte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_SFBMixte.Location = New System.Drawing.Point(1, 215)
        Me.chk_SFBMixte.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_SFBMixte.Name = "chk_SFBMixte"
        Me.chk_SFBMixte.Size = New System.Drawing.Size(169, 105)
        Me.chk_SFBMixte.TabIndex = 11
        Me.chk_SFBMixte.Text = "chk_SFBMixte"
        Me.chk_SFBMixte.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chk_SFBMixte.UseVisualStyleBackColor = True
        '
        'chk_IFB_B_Acier
        '
        Me.chk_IFB_B_Acier.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_IFB_B_Acier.AutoSize = True
        Me.chk_IFB_B_Acier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_IFB_B_Acier.Location = New System.Drawing.Point(343, 108)
        Me.chk_IFB_B_Acier.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_IFB_B_Acier.Name = "chk_IFB_B_Acier"
        Me.chk_IFB_B_Acier.Size = New System.Drawing.Size(169, 105)
        Me.chk_IFB_B_Acier.TabIndex = 10
        Me.chk_IFB_B_Acier.Text = "chk_IFB_B_Acier"
        Me.chk_IFB_B_Acier.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chk_IFB_B_Acier.UseVisualStyleBackColor = True
        '
        'chk_IFB_A_Mixte
        '
        Me.chk_IFB_A_Mixte.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_IFB_A_Mixte.AutoSize = True
        Me.chk_IFB_A_Mixte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_IFB_A_Mixte.Location = New System.Drawing.Point(172, 215)
        Me.chk_IFB_A_Mixte.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_IFB_A_Mixte.Name = "chk_IFB_A_Mixte"
        Me.chk_IFB_A_Mixte.Size = New System.Drawing.Size(169, 105)
        Me.chk_IFB_A_Mixte.TabIndex = 9
        Me.chk_IFB_A_Mixte.Text = "chk_IFB_A_Mixte"
        Me.chk_IFB_A_Mixte.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chk_IFB_A_Mixte.UseVisualStyleBackColor = True
        '
        'chk_SABAcier
        '
        Me.chk_SABAcier.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_SABAcier.AutoSize = True
        Me.chk_SABAcier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_SABAcier.Location = New System.Drawing.Point(514, 108)
        Me.chk_SABAcier.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_SABAcier.Name = "chk_SABAcier"
        Me.chk_SABAcier.Size = New System.Drawing.Size(172, 105)
        Me.chk_SABAcier.TabIndex = 8
        Me.chk_SABAcier.Text = "chk_SABAcier"
        Me.chk_SABAcier.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chk_SABAcier.UseVisualStyleBackColor = True
        '
        'chk_IFB_B_Mixte
        '
        Me.chk_IFB_B_Mixte.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_IFB_B_Mixte.AutoSize = True
        Me.chk_IFB_B_Mixte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_IFB_B_Mixte.Location = New System.Drawing.Point(343, 215)
        Me.chk_IFB_B_Mixte.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_IFB_B_Mixte.Name = "chk_IFB_B_Mixte"
        Me.chk_IFB_B_Mixte.Size = New System.Drawing.Size(169, 105)
        Me.chk_IFB_B_Mixte.TabIndex = 7
        Me.chk_IFB_B_Mixte.Text = "chk_IFB_B_Mixte"
        Me.chk_IFB_B_Mixte.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chk_IFB_B_Mixte.UseVisualStyleBackColor = True
        '
        'chk_IFB_A_Acier
        '
        Me.chk_IFB_A_Acier.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_IFB_A_Acier.AutoSize = True
        Me.chk_IFB_A_Acier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_IFB_A_Acier.Location = New System.Drawing.Point(172, 108)
        Me.chk_IFB_A_Acier.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_IFB_A_Acier.Name = "chk_IFB_A_Acier"
        Me.chk_IFB_A_Acier.Size = New System.Drawing.Size(169, 105)
        Me.chk_IFB_A_Acier.TabIndex = 6
        Me.chk_IFB_A_Acier.Text = "chk_IFB_A_Acier"
        Me.chk_IFB_A_Acier.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chk_IFB_A_Acier.UseVisualStyleBackColor = True
        '
        'chk_SABMixte
        '
        Me.chk_SABMixte.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_SABMixte.AutoSize = True
        Me.chk_SABMixte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_SABMixte.Location = New System.Drawing.Point(514, 215)
        Me.chk_SABMixte.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_SABMixte.Name = "chk_SABMixte"
        Me.chk_SABMixte.Size = New System.Drawing.Size(172, 105)
        Me.chk_SABMixte.TabIndex = 5
        Me.chk_SABMixte.Text = "chk_SABMixte"
        Me.chk_SABMixte.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chk_SABMixte.UseVisualStyleBackColor = True
        '
        'chk_SFBAcier
        '
        Me.chk_SFBAcier.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_SFBAcier.AutoSize = True
        Me.chk_SFBAcier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_SFBAcier.Location = New System.Drawing.Point(1, 108)
        Me.chk_SFBAcier.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_SFBAcier.Name = "chk_SFBAcier"
        Me.chk_SFBAcier.Size = New System.Drawing.Size(169, 105)
        Me.chk_SFBAcier.TabIndex = 4
        Me.chk_SFBAcier.Text = "chk_SFBAcier"
        Me.chk_SFBAcier.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chk_SFBAcier.UseVisualStyleBackColor = True
        '
        'chk_SectionMixte
        '
        Me.chk_SectionMixte.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_SectionMixte.AutoSize = True
        Me.chk_SectionMixte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_SectionMixte.Location = New System.Drawing.Point(343, 1)
        Me.chk_SectionMixte.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_SectionMixte.Name = "chk_SectionMixte"
        Me.chk_SectionMixte.Size = New System.Drawing.Size(169, 105)
        Me.chk_SectionMixte.TabIndex = 3
        Me.chk_SectionMixte.Text = "chk_SectionMixte"
        Me.chk_SectionMixte.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chk_SectionMixte.UseVisualStyleBackColor = True
        '
        'chk_SectionMixteEnrobe
        '
        Me.chk_SectionMixteEnrobe.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_SectionMixteEnrobe.AutoSize = True
        Me.chk_SectionMixteEnrobe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_SectionMixteEnrobe.Location = New System.Drawing.Point(514, 1)
        Me.chk_SectionMixteEnrobe.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_SectionMixteEnrobe.Name = "chk_SectionMixteEnrobe"
        Me.chk_SectionMixteEnrobe.Size = New System.Drawing.Size(172, 105)
        Me.chk_SectionMixteEnrobe.TabIndex = 2
        Me.chk_SectionMixteEnrobe.Text = "chk_SectionMixteEnrobe"
        Me.chk_SectionMixteEnrobe.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chk_SectionMixteEnrobe.UseVisualStyleBackColor = True
        '
        'chk_SectionAcierEnrobe
        '
        Me.chk_SectionAcierEnrobe.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_SectionAcierEnrobe.AutoSize = True
        Me.chk_SectionAcierEnrobe.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_SectionAcierEnrobe.Location = New System.Drawing.Point(172, 1)
        Me.chk_SectionAcierEnrobe.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_SectionAcierEnrobe.Name = "chk_SectionAcierEnrobe"
        Me.chk_SectionAcierEnrobe.Size = New System.Drawing.Size(169, 105)
        Me.chk_SectionAcierEnrobe.TabIndex = 1
        Me.chk_SectionAcierEnrobe.Text = "chk_SectionAcierEnrobe"
        Me.chk_SectionAcierEnrobe.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chk_SectionAcierEnrobe.UseVisualStyleBackColor = True
        '
        'chk_SectionAcier
        '
        Me.chk_SectionAcier.Appearance = System.Windows.Forms.Appearance.Button
        Me.chk_SectionAcier.AutoSize = True
        Me.chk_SectionAcier.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chk_SectionAcier.Location = New System.Drawing.Point(1, 1)
        Me.chk_SectionAcier.Margin = New System.Windows.Forms.Padding(1)
        Me.chk_SectionAcier.Name = "chk_SectionAcier"
        Me.chk_SectionAcier.Size = New System.Drawing.Size(169, 105)
        Me.chk_SectionAcier.TabIndex = 0
        Me.chk_SectionAcier.Text = "chk_SectionAcier"
        Me.chk_SectionAcier.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.chk_SectionAcier.UseVisualStyleBackColor = True
        '
        'lbl_TypeSection
        '
        Me.lbl_TypeSection.AutoSize = True
        Me.lbl_TypeSection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_TypeSection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbl_TypeSection.Location = New System.Drawing.Point(3, 0)
        Me.lbl_TypeSection.Name = "lbl_TypeSection"
        Me.lbl_TypeSection.Size = New System.Drawing.Size(687, 30)
        Me.lbl_TypeSection.TabIndex = 1
        Me.lbl_TypeSection.Text = "lbl_TypeSection"
        Me.lbl_TypeSection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Frm_AjoutePP
        '
        Me.AcceptButton = Me.btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Annuler
        Me.ClientSize = New System.Drawing.Size(701, 485)
        Me.Controls.Add(Me.pan_Main)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_AjoutePP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frm_AjoutePP"
        Me.pan_Main.ResumeLayout(False)
        Me.TLpan_Main.ResumeLayout(False)
        Me.TLPan_PartieBasse.ResumeLayout(False)
        Me.pan_Choix.ResumeLayout(False)
        Me.TLpan_SeparationHorizontale.ResumeLayout(False)
        Me.TLpan_CadreSuperieur.ResumeLayout(False)
        Me.pan_Poutre.ResumeLayout(False)
        Me.pan_Poutre.PerformLayout()
        Me.pan_Projet.ResumeLayout(False)
        Me.pan_Projet.PerformLayout()
        Me.pan_TypeSection.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TLpan_ChoixSection.ResumeLayout(False)
        Me.TLpan_ChoixSection.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pan_Main As Panel
    Friend WithEvents TLpan_Main As TableLayoutPanel
    Friend WithEvents TLPan_PartieBasse As TableLayoutPanel
    Friend WithEvents btn_OK As Button
    Friend WithEvents btn_Annuler As Button
    Friend WithEvents pan_Choix As Panel
    Friend WithEvents TLpan_SeparationHorizontale As TableLayoutPanel
    Friend WithEvents TLpan_CadreSuperieur As TableLayoutPanel
    Friend WithEvents pan_Poutre As Panel
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents chk_PoutreEnCours As CheckBox
    Friend WithEvents txt_NomNouvellePoutre As TextBox
    Friend WithEvents chk_NouvellePoutre As CheckBox
    Friend WithEvents pan_Projet As Panel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents chk_ProjetEnCours As CheckBox
    Friend WithEvents txt_NomNouveauProjet As TextBox
    Friend WithEvents chk_NouveauProjet As CheckBox
    Friend WithEvents pan_TypeSection As Panel
    Friend WithEvents TLpan_ChoixSection As TableLayoutPanel
    Friend WithEvents chk_SectionAcier As CheckBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents lbl_TypeSection As Label
    Friend WithEvents chk_SectionAcierEnrobe As CheckBox
    Friend WithEvents chk_SectionMixte As CheckBox
    Friend WithEvents chk_SectionMixteEnrobe As CheckBox
    Friend WithEvents chk_SFBAcier As CheckBox
    Friend WithEvents MyToolTip As ToolTip
    Friend WithEvents lbl_NouvellePoutre As Label
    Friend WithEvents lbl_NouveauProjet As Label
    Friend WithEvents chk_SFBMixte As CheckBox
    Friend WithEvents chk_IFB_B_Acier As CheckBox
    Friend WithEvents chk_IFB_A_Mixte As CheckBox
    Friend WithEvents chk_SABAcier As CheckBox
    Friend WithEvents chk_IFB_B_Mixte As CheckBox
    Friend WithEvents chk_IFB_A_Acier As CheckBox
    Friend WithEvents chk_SABMixte As CheckBox
End Class
