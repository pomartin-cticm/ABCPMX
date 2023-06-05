Imports System.IO
Imports PMXMoteur2

Public Class Frm_AjoutePP

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim strProjet, strPoutre As String
    Dim indProjetN, indPoutreN As Integer

    Dim MySectionAcier As New cls_Section
    Dim MySectionAcierEnrobe As New cls_Section
    Dim MySectionMixte As New cls_Section
    Dim MySectionMixteEnrobe As New cls_Section
    Dim MySectionSFB As New cls_Section
    Dim MySectionSFBmixte As New cls_Section

    'Dim CouleurAcierNormal As Color = Color.DarkSlateBlue
    'Dim CouleurAcierSelect As Color = Color.DarkOrange

    Dim TypeSection As cls_Section.Enum_TypeSection = cls_Section.Enum_TypeSection.Acier

    Dim strType As String
    Dim tabType As New Dictionary(Of cls_Section.Enum_TypeSection, String)

    Public lOuverture As Boolean = False    ' Indique si appel depuis la fenêtre ouverture

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_AjoutePP_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lBuild = True

        GestionLangue()
        GestionStyle()
        PreparationSectionsType()
        InitialiseFenetre()

        lBuild = False

    End Sub

    Public Sub InitialiseFromOutside()
        lBuild = True

        GestionLangue()
        GestionStyle()
        PreparationSectionsType()
        InitialiseFenetre()

        lBuild = False
    End Sub

    Private Sub GestionLangue()

        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_AjoutePP")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = "Nouveau projet ZZ"

                Me.btn_Annuler.Text = "Annuler ZZ"
                Me.btn_OK.Text = "OK ZZ"

                Me.chk_NouveauProjet.Text = ""
                Me.lbl_NouveauProjet.Text = "Nom du projet:"
                Me.chk_ProjetEnCours.Text = "Projet :"

                Me.chk_NouvellePoutre.Text = ""
                Me.lbl_NouvellePoutre.Text = "Ajouter la poutre :"
                Me.chk_PoutreEnCours.Text = "Modifier la poutre :"

                strProjet = "Project"
                strPoutre = "Poutre"

                Me.lbl_TypeSection.Text = "Type de section"
                strType = "Type de section"

                'Me.chk_SectionAcier.Text = "Section non mixte"
                'Me.chk_SectionAcierEnrobe.Text = "Section acier avec enrobage partiel"

                tabType.Clear()
                tabType.Add(cls_Section.Enum_TypeSection.Acier, "Section non mixte")
                tabType.Add(cls_Section.Enum_TypeSection.AcierEnrobage, "Section acier avec enrobage partiel")
                tabType.Add(cls_Section.Enum_TypeSection.Mixte, "Section mixte")
                tabType.Add(cls_Section.Enum_TypeSection.MixteEnrobage, "Section mixte avec enrobage partiel")
                tabType.Add(cls_Section.Enum_TypeSection.SFB, "Section dalle mince SFB non mixte")
                tabType.Add(cls_Section.Enum_TypeSection.SFBmixte, "Section dalle mince SFB mixte")
                tabType.Add(cls_Section.Enum_TypeSection.SAB, "Section dalle mince SAB non mixte")
                tabType.Add(cls_Section.Enum_TypeSection.SABmixte, "Section dalle mince SAB mixte")

                Me.MyToolTip.SetToolTip(Me.chk_SectionAcier, tabType(cls_Section.Enum_TypeSection.Acier))
                Me.MyToolTip.SetToolTip(Me.chk_SectionAcierEnrobe, tabType(cls_Section.Enum_TypeSection.AcierEnrobage))
                Me.MyToolTip.SetToolTip(Me.chk_SectionMixte, tabType(cls_Section.Enum_TypeSection.Mixte))
                Me.MyToolTip.SetToolTip(Me.chk_SectionMixteEnrobe, tabType(cls_Section.Enum_TypeSection.MixteEnrobage))
                Me.MyToolTip.SetToolTip(Me.chk_SFBAcier, tabType(cls_Section.Enum_TypeSection.SFB))
                Me.MyToolTip.SetToolTip(Me.chk_SFBMixte, tabType(cls_Section.Enum_TypeSection.SFBmixte))

                Me.chk_SectionAcier.Text = ""
                Me.chk_SectionAcierEnrobe.Text = ""
                Me.chk_SectionMixte.Text = ""
                Me.chk_SectionMixteEnrobe.Text = ""
                Me.chk_SFBAcier.Text = ""
                Me.chk_SFBMixte.Text = ""

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, "Frm_AjoutePP/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub InitialiseFenetre()

        'indProjetN = Project.Count + 1
        indProjetN = 1
        indPoutreN = MyProjet.Poutres.Count + 1

        Me.txt_NomNouveauProjet.Text = strProjet & " Pr#" & CStr(indProjetN)
        Me.txt_NomNouvellePoutre.Text = strPoutre & " B#" & CStr(indPoutreN)

        Me.chk_NouveauProjet.Checked = True
        Me.chk_NouvellePoutre.Checked = True
        Me.chk_NouvellePoutre.Enabled = False
        Me.chk_NouveauProjet.Enabled = False

        MAJ_EtatChkSection()

    End Sub

    Private Sub PreparationSectionsType()
        '--> Définition de la section acier
        MySectionAcier.ProfilA.t_fs = 0.03
        MySectionAcier.ProfilA.t_fi = 0.03
        MySectionAcier.ProfilA.r_cs = 0.03
        MySectionAcier.ProfilA.r_ci = 0.03

        '--> Définition de la section acier enrobée
        MySectionAcierEnrobe.typeSection = cls_Section.Enum_TypeSection.AcierEnrobage
        MySectionAcierEnrobe.ProfilA.t_fs = 0.03
        MySectionAcierEnrobe.ProfilA.t_fi = 0.03
        MySectionAcierEnrobe.ProfilA.r_cs = 0.03
        MySectionAcierEnrobe.ProfilA.r_ci = 0.03

        MySectionAcierEnrobe.enrobage_partiel.LitsArma(0).nbArma = 1
        MySectionAcierEnrobe.enrobage_partiel.LitsArma(0).Phi = 0.025
        MySectionAcierEnrobe.enrobage_partiel.LitsArma(2).nbArma = 1
        MySectionAcierEnrobe.enrobage_partiel.LitsArma(2).Phi = 0.025

        '--> Définition de la section mixte
        MySectionMixte.typeSection = cls_Section.Enum_TypeSection.Mixte
        MySectionMixte.ProfilA.t_fs = 0.03
        MySectionMixte.ProfilA.t_fi = 0.03
        MySectionMixte.ProfilA.r_cs = 0.03
        MySectionMixte.ProfilA.r_ci = 0.03

        MySectionMixte.Dalle.Beff = 1

        '--> Définition de la section mixte enrobée
        MySectionMixteEnrobe.typeSection = cls_Section.Enum_TypeSection.MixteEnrobage
        MySectionMixteEnrobe.ProfilA.t_fs = 0.03
        MySectionMixteEnrobe.ProfilA.t_fi = 0.03
        MySectionMixteEnrobe.ProfilA.r_cs = 0.03
        MySectionMixteEnrobe.ProfilA.r_ci = 0.03

        MySectionMixteEnrobe.enrobage_partiel.LitsArma(0).nbArma = 1
        MySectionMixteEnrobe.enrobage_partiel.LitsArma(0).Phi = 0.025
        MySectionMixteEnrobe.enrobage_partiel.LitsArma(2).nbArma = 1
        MySectionMixteEnrobe.enrobage_partiel.LitsArma(2).Phi = 0.025

        MySectionMixteEnrobe.Dalle.Beff = 1

        '--> Définition de la section SFB non mixte
        MySectionSFB.typeSection = cls_Section.Enum_TypeSection.SFB
        MySectionSFB.ProfilA.t_fs = 0.03
        MySectionSFB.ProfilA.t_fi = 0.03
        MySectionSFB.ProfilA.r_cs = 0.03
        MySectionSFB.ProfilA.r_ci = 0.03
        MySectionSFB.ProfilA.Plat_t = 0.03
        MySectionSFB.ProfilA.Plat_b = 0.45

        '--> Définition de la section SFB non mixte
        MySectionSFBmixte.typeSection = cls_Section.Enum_TypeSection.SFBmixte
        MySectionSFBmixte.ProfilA.t_fs = 0.03
        MySectionSFBmixte.ProfilA.t_fi = 0.03
        MySectionSFBmixte.ProfilA.r_cs = 0.03
        MySectionSFBmixte.ProfilA.r_ci = 0.03
        MySectionSFBmixte.ProfilA.Plat_t = 0.03
        MySectionSFBmixte.ProfilA.Plat_b = 0.45

        MySectionSFBmixte.Dalle.t_d = 0.05
        MySectionSFBmixte.Dalle.Beff = 0.75
    End Sub


    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_TypeSection.BackColor = CouleurBackBandeaux
        Me.lbl_TypeSection.ForeColor = CouleurForeBandeaux

    End Sub

#End Region

#Region " DESSINS "

    Private Sub PaintBoutons(sender As Object, e As PaintEventArgs) Handles chk_SectionAcier.Paint, chk_SectionAcierEnrobe.Paint, chk_SectionMixteEnrobe.Paint, chk_SectionMixte.Paint, chk_SFBAcier.Paint, chk_SFBMixte.Paint, chk_SABMixte.Paint, chk_SABAcier.Paint, CheckBox6.Paint, CheckBox5.Paint, CheckBox3.Paint, CheckBox2.Paint

        Const kAdjust As Single = 0.9
        Dim MyFont As New Font("Arial", 8)

        Select Case sender.name
            Case Me.chk_SectionAcier.Name
                DessinFrmTypeSection(e.Graphics, MySectionAcier, MySectionAcier.Dalle, Me.chk_SectionAcier.ClientRectangle.Width, Me.chk_SectionAcier.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.Acier)

            Case Me.chk_SectionAcierEnrobe.Name
                DessinFrmTypeSection(e.Graphics, MySectionAcierEnrobe, MySectionAcierEnrobe.Dalle, Me.chk_SectionAcierEnrobe.ClientRectangle.Width, Me.chk_SectionAcierEnrobe.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.AcierEnrobage)

            Case Me.chk_SectionMixte.Name
                DessinFrmTypeSection(e.Graphics, MySectionMixte, MySectionMixte.Dalle, Me.chk_SectionMixte.ClientRectangle.Width, Me.chk_SectionMixte.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.Mixte)

            Case Me.chk_SectionMixteEnrobe.Name
                DessinFrmTypeSection(e.Graphics, MySectionMixteEnrobe, MySectionMixteEnrobe.Dalle, Me.chk_SectionMixteEnrobe.ClientRectangle.Width, Me.chk_SectionMixteEnrobe.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.MixteEnrobage)

            Case Me.chk_SFBAcier.Name
                DessinFrmTypeSection(e.Graphics, MySectionSFB, MySectionSFB.Dalle, Me.chk_SFBAcier.ClientRectangle.Width, Me.chk_SFBAcier.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.SFB)

            Case Me.chk_SFBMixte.Name
                DessinFrmTypeSection(e.Graphics, MySectionSFBmixte, MySectionSFBmixte.Dalle, Me.chk_SFBMixte.ClientRectangle.Width, Me.chk_SFBMixte.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.SFBmixte)

        End Select

    End Sub


#End Region

#Region " Evènements "

    Private Sub ChoixSection_Changed(sender As Object, e As EventArgs) Handles chk_SectionAcierEnrobe.CheckedChanged, chk_SectionAcier.CheckedChanged, chk_SectionMixteEnrobe.CheckedChanged, chk_SectionMixte.CheckedChanged, chk_SFBAcier.CheckedChanged, chk_SFBMixte.CheckedChanged, chk_SABMixte.CheckedChanged, chk_SABAcier.CheckedChanged, CheckBox6.CheckedChanged, CheckBox5.CheckedChanged, CheckBox3.CheckedChanged, CheckBox2.CheckedChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.chk_SectionAcier.Name : TypeSection = cls_Section.Enum_TypeSection.Acier
            Case Me.chk_SectionAcierEnrobe.Name : TypeSection = cls_Section.Enum_TypeSection.AcierEnrobage
            Case Me.chk_SectionMixte.Name : TypeSection = cls_Section.Enum_TypeSection.Mixte
            Case Me.chk_SectionMixteEnrobe.Name : TypeSection = cls_Section.Enum_TypeSection.MixteEnrobage
            Case Me.chk_SFBAcier.Name : TypeSection = cls_Section.Enum_TypeSection.SFB
            Case Me.chk_SFBMixte.Name : TypeSection = cls_Section.Enum_TypeSection.SFBmixte
        End Select

        lBuild = True
        MAJ_EtatChkSection()
        lBuild = False
    End Sub

    Private Sub MAJ_EtatChkSection()

        Me.chk_SectionAcier.Checked = TypeSection = cls_Section.Enum_TypeSection.Acier
        Me.chk_SectionAcierEnrobe.Checked = TypeSection = cls_Section.Enum_TypeSection.AcierEnrobage
        Me.chk_SectionMixte.Checked = TypeSection = cls_Section.Enum_TypeSection.Mixte
        Me.chk_SectionMixteEnrobe.Checked = TypeSection = cls_Section.Enum_TypeSection.MixteEnrobage
        Me.chk_SFBAcier.Checked = TypeSection = cls_Section.Enum_TypeSection.SFB
        Me.chk_SFBMixte.Checked = TypeSection = cls_Section.Enum_TypeSection.SFBmixte

        Me.lbl_TypeSection.Text = strType & " : " & tabType(TypeSection)

    End Sub



#End Region

#Region "===FERMETURE==="
    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub Frm_AjoutePP_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        MsgBox("pom2")
    End Sub

    'Private Sub Frm_AjoutePP_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
    '    MsgBox("pom")
    'End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        TraitementSaisie()
    End Sub

    Public Sub TraitementSaisie()

        Dim lAjout As Boolean = False

        'If Me.chk_NouveauProjet.Checked Then

        'End If

        Dim NomPoutre As String = Me.txt_NomNouvellePoutre.Text

        If Me.chk_NouvellePoutre.Checked Then

            If Me.chk_SectionAcier.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.Acier, NomPoutre))
            End If

            If Me.chk_SectionAcierEnrobe.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.AcierEnrobage, NomPoutre))
            End If

            If Me.chk_SectionMixte.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.Mixte, NomPoutre))
            End If

            If Me.chk_SectionMixteEnrobe.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.MixteEnrobage, NomPoutre))
            End If

            If Me.chk_SFBAcier.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.SFB, NomPoutre))
            End If

        End If

        If lAjout Then
            MyProjet.IndEnCours = MyProjet.Poutres.Count - 1
            MyProjet.Nom = Me.txt_NomNouveauProjet.Text
        End If
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

#End Region



End Class