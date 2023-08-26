Imports System.IO
Imports PMXMoteur2

Public Class Frm_AjoutePP

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim strProjet, strPoutre As String
    Dim indProjetN, indPoutreN As Integer

    'MODIF GUD car Dalle n'est plus dans la Cls_Section

    Dim MyPoutreAcier As New cls_Poutre
    Dim MyPoutreAcierEnrobe As New cls_Poutre
    Dim MyPoutreMixte As New cls_Poutre
    Dim MyPoutreMixteEnrobe As New cls_Poutre
    Dim MyPoutreSFB As New cls_Poutre
    Dim MyPoutreSAB As New cls_Poutre
    Dim MyPoutreSFBmixte As New cls_Poutre
    Dim MyPoutreSABmixte As New cls_Poutre

    Dim MySectionAcier As cls_Section = MyPoutreAcier.Section
    Dim MySectionAcierEnrobe As cls_Section = MyPoutreAcierEnrobe.Section
    Dim MySectionMixte As cls_Section = MyPoutreMixte.Section
    Dim MySectionMixteEnrobe As cls_Section = MyPoutreMixteEnrobe.Section
    Dim MySectionSFB As cls_Section = MyPoutreSFB.Section
    Dim MySectionSAB As cls_Section = MyPoutreSAB.Section
    Dim MySectionSFBmixte As cls_Section = MyPoutreSFBmixte.Section
    Dim MySectionSABmixte As cls_Section = MyPoutreSABmixte.Section


    'Dim MySectionAcier As New cls_Section
    'Dim MySectionAcierEnrobe As New cls_Section
    'Dim MySectionMixte As New cls_Section
    'Dim MySectionMixteEnrobe As New cls_Section
    'Dim MySectionSFB As New cls_Section
    'Dim MySectionSAB As New cls_Section
    'Dim MySectionSFBmixte As New cls_Section
    'Dim MySectionSABmixte As New cls_Section

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
                Me.MyToolTip.SetToolTip(Me.chk_SABAcier, tabType(cls_Section.Enum_TypeSection.SAB))
                Me.MyToolTip.SetToolTip(Me.chk_SABMixte, tabType(cls_Section.Enum_TypeSection.SABmixte))

                Me.chk_SectionAcier.Text = ""
                Me.chk_SectionAcierEnrobe.Text = ""
                Me.chk_SectionMixte.Text = ""
                Me.chk_SectionMixteEnrobe.Text = ""
                Me.chk_SFBAcier.Text = ""
                Me.chk_SFBMixte.Text = ""
                Me.chk_SABAcier.Text = ""
                Me.chk_SABMixte.Text = ""

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
        MySectionAcier.ProfilA.Tfs = 0.03
        MySectionAcier.ProfilA.Tfi = 0.03
        MySectionAcier.ProfilA.Rcs = 0.03
        MySectionAcier.ProfilA.Rci = 0.03

        '--> Définition de la section acier enrobée
        MySectionAcierEnrobe.typeSection = cls_Section.Enum_TypeSection.AcierEnrobage
        MySectionAcierEnrobe.ProfilA.Tfs = 0.03
        MySectionAcierEnrobe.ProfilA.Tfi = 0.03
        MySectionAcierEnrobe.ProfilA.Rcs = 0.03
        MySectionAcierEnrobe.ProfilA.Rci = 0.03

        MySectionAcierEnrobe.enrobage_partiel.LitsArmaOLD(0).nbArma = 1
        MySectionAcierEnrobe.enrobage_partiel.LitsArmaOLD(0).Phi = 0.025
        MySectionAcierEnrobe.enrobage_partiel.LitsArmaOLD(2).nbArma = 1
        MySectionAcierEnrobe.enrobage_partiel.LitsArmaOLD(2).Phi = 0.025

        '--> Définition de la section mixte
        MySectionMixte.typeSection = cls_Section.Enum_TypeSection.Mixte
        MySectionMixte.ProfilA.Tfs = 0.03
        MySectionMixte.ProfilA.Tfi = 0.03
        MySectionMixte.ProfilA.Rcs = 0.03
        MySectionMixte.ProfilA.Rci = 0.03

        MyPoutreMixte.Dalle.Beff = 1
        'MySectionMixte.Dalle.Beff = 1

        '--> Définition de la section mixte enrobée
        MySectionMixteEnrobe.typeSection = cls_Section.Enum_TypeSection.MixteEnrobage
        MySectionMixteEnrobe.ProfilA.Tfs = 0.03
        MySectionMixteEnrobe.ProfilA.Tfi = 0.03
        MySectionMixteEnrobe.ProfilA.Rcs = 0.03
        MySectionMixteEnrobe.ProfilA.Rci = 0.03

        MySectionMixteEnrobe.enrobage_partiel.LitsArmaOLD(0).nbArma = 1
        MySectionMixteEnrobe.enrobage_partiel.LitsArmaOLD(0).Phi = 0.025
        MySectionMixteEnrobe.enrobage_partiel.LitsArmaOLD(2).nbArma = 1
        MySectionMixteEnrobe.enrobage_partiel.LitsArmaOLD(2).Phi = 0.025

        MyPoutreMixteEnrobe.Dalle.Beff = 1
        'MySectionMixteEnrobe.Dalle.Beff = 1

        '--> Définition de la section SFB non mixte
        MySectionSFB.typeSection = cls_Section.Enum_TypeSection.SFB
        MySectionSFB.ProfilA.Tfs = 0.03
        MySectionSFB.ProfilA.Tfi = 0.03
        MySectionSFB.ProfilA.Rcs = 0.03
        MySectionSFB.ProfilA.Rci = 0.03
        MySectionSFB.ProfilA.Plat_t = 0.03
        MySectionSFB.ProfilA.Plat_b = 0.45

        '--> Définition de la section SAB non mixte
        MySectionSAB.typeSection = cls_Section.Enum_TypeSection.SAB
        MySectionSAB.ProfilA.Bfs *= 5 / 8
        MySectionSAB.ProfilA.Tfs = 0.03
        MySectionSAB.ProfilA.Tfi = 0.03
        MySectionSAB.ProfilA.Rcs = 0.03
        MySectionSAB.ProfilA.Rci = 0.03
        MySectionSAB.ProfilA.Plat_t = 0.03
        MySectionSAB.ProfilA.Plat_b = 0.45

        '--> Définition de la section SFB mixte
        MySectionSFBmixte.typeSection = cls_Section.Enum_TypeSection.SFBmixte
        MySectionSFBmixte.ProfilA.Tfs = 0.03
        MySectionSFBmixte.ProfilA.Tfi = 0.03
        MySectionSFBmixte.ProfilA.Rcs = 0.03
        MySectionSFBmixte.ProfilA.Rci = 0.03
        MySectionSFBmixte.ProfilA.Plat_t = 0.03
        MySectionSFBmixte.ProfilA.Plat_b = 0.45

        MyPoutreSFBmixte.Dalle.t_d = 0.05
        'MySectionSFBmixte.Dalle.t_d = 0.05
        MyPoutreSFBmixte.Dalle.Beff = 0.75
        'MySectionSFBmixte.Dalle.Beff = 0.75

        '--> Définition de la section SAB mixte
        MySectionSABmixte.typeSection = cls_Section.Enum_TypeSection.SABmixte
        MySectionSABmixte.ProfilA.Tw = 0.015
        MySectionSABmixte.ProfilA.Bfs *= 5 / 8
        MySectionSABmixte.ProfilA.Tfs = 0.03
        MySectionSABmixte.ProfilA.Tfi = 0.03
        MySectionSABmixte.ProfilA.Rcs = 0.03
        MySectionSABmixte.ProfilA.Rci = 0.03

        MyPoutreSABmixte.Dalle.t_d = 0.05
        'MySectionSABmixte.Dalle.t_d = 0.05
        MyPoutreSABmixte.Dalle.Beff = 0.75
        'MySectionSABmixte.Dalle.Beff = 0.75
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
                DessinFrmTypeSection(e.Graphics, MySectionAcier, MyPoutreAcier.Dalle, Me.chk_SectionAcier.ClientRectangle.Width, Me.chk_SectionAcier.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.Acier)

            Case Me.chk_SectionAcierEnrobe.Name
                DessinFrmTypeSection(e.Graphics, MySectionAcierEnrobe, MyPoutreAcierEnrobe.Dalle, Me.chk_SectionAcierEnrobe.ClientRectangle.Width, Me.chk_SectionAcierEnrobe.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.AcierEnrobage)

            Case Me.chk_SectionMixte.Name
                DessinFrmTypeSection(e.Graphics, MySectionMixte, MyPoutreMixte.Dalle, Me.chk_SectionMixte.ClientRectangle.Width, Me.chk_SectionMixte.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.Mixte)

            Case Me.chk_SectionMixteEnrobe.Name
                DessinFrmTypeSection(e.Graphics, MySectionMixteEnrobe, MyPoutreMixteEnrobe.Dalle, Me.chk_SectionMixteEnrobe.ClientRectangle.Width, Me.chk_SectionMixteEnrobe.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.MixteEnrobage)

            Case Me.chk_SFBAcier.Name
                DessinFrmTypeSection(e.Graphics, MySectionSFB, MyPoutreSFB.Dalle, Me.chk_SFBAcier.ClientRectangle.Width, Me.chk_SFBAcier.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.SFB)

            Case Me.chk_SABAcier.Name
                DessinFrmTypeSection(e.Graphics, MySectionSAB, MyPoutreSAB.Dalle, Me.chk_SABAcier.ClientRectangle.Width, Me.chk_SABAcier.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.SAB)

            Case Me.chk_SFBMixte.Name
                DessinFrmTypeSection(e.Graphics, MySectionSFBmixte, MyPoutreSFBmixte.Dalle, Me.chk_SFBMixte.ClientRectangle.Width, Me.chk_SFBMixte.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.SFBmixte)

            Case Me.chk_SABMixte.Name
                DessinFrmTypeSection(e.Graphics, MySectionSABmixte, MyPoutreSABmixte.Dalle, Me.chk_SABMixte.ClientRectangle.Width, Me.chk_SABMixte.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.SABmixte)

        End Select

        'Select Case sender.name
        '    Case Me.chk_SectionAcier.Name
        '        DessinFrmTypeSection(e.Graphics, MySectionAcier, MySectionAcier.Dalle, Me.chk_SectionAcier.ClientRectangle.Width, Me.chk_SectionAcier.Height,
        '                             MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.Acier)

        '    Case Me.chk_SectionAcierEnrobe.Name
        '        DessinFrmTypeSection(e.Graphics, MySectionAcierEnrobe, MySectionAcierEnrobe.Dalle, Me.chk_SectionAcierEnrobe.ClientRectangle.Width, Me.chk_SectionAcierEnrobe.Height,
        '                             MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.AcierEnrobage)

        '    Case Me.chk_SectionMixte.Name
        '        DessinFrmTypeSection(e.Graphics, MySectionMixte, MySectionMixte.Dalle, Me.chk_SectionMixte.ClientRectangle.Width, Me.chk_SectionMixte.Height,
        '                             MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.Mixte)

        '    Case Me.chk_SectionMixteEnrobe.Name
        '        DessinFrmTypeSection(e.Graphics, MySectionMixteEnrobe, MySectionMixteEnrobe.Dalle, Me.chk_SectionMixteEnrobe.ClientRectangle.Width, Me.chk_SectionMixteEnrobe.Height,
        '                             MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.MixteEnrobage)

        '    Case Me.chk_SFBAcier.Name
        '        DessinFrmTypeSection(e.Graphics, MySectionSFB, MySectionSFB.Dalle, Me.chk_SFBAcier.ClientRectangle.Width, Me.chk_SFBAcier.Height,
        '                             MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.SFB)

        '    Case Me.chk_SABAcier.Name
        '        DessinFrmTypeSection(e.Graphics, MySectionSAB, MySectionSAB.Dalle, Me.chk_SABAcier.ClientRectangle.Width, Me.chk_SABAcier.Height,
        '                             MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.SAB)

        '    Case Me.chk_SFBMixte.Name
        '        DessinFrmTypeSection(e.Graphics, MySectionSFBmixte, MySectionSFBmixte.Dalle, Me.chk_SFBMixte.ClientRectangle.Width, Me.chk_SFBMixte.Height,
        '                             MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.SFBmixte)

        '    Case Me.chk_SABMixte.Name
        '        DessinFrmTypeSection(e.Graphics, MySectionSABmixte, MySectionSABmixte.Dalle, Me.chk_SABMixte.ClientRectangle.Width, Me.chk_SABMixte.Height,
        '                             MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.SABmixte)

        'End Select

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
            Case Me.chk_SABAcier.Name : TypeSection = cls_Section.Enum_TypeSection.SAB
            Case Me.chk_SABMixte.Name : TypeSection = cls_Section.Enum_TypeSection.SABmixte
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
        Me.chk_SABAcier.Checked = TypeSection = cls_Section.Enum_TypeSection.SAB
        Me.chk_SABMixte.Checked = TypeSection = cls_Section.Enum_TypeSection.SABmixte

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
        Dim lOK As Boolean
        Dim lTrouve As Boolean

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

            If Me.chk_SABMixte.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.SABmixte, NomPoutre))
            End If

            If Me.chk_SABAcier.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.SAB, NomPoutre))
            End If

            InitialisePoutreDeBases(MyProjet.Poutres(MyProjet.Poutres.Count - 1), lOK)

            InitialiseBacDeBase(MyProjet.Poutres(MyProjet.Poutres.Count - 1).Dalle.Bac, lTrouve)
            InitialiseGoujonDeBase(MyProjet.Poutres(MyProjet.Poutres.Count - 1).Dalle.Connecteur, lTrouve)

            MyProjet.Poutres(MyProjet.Poutres.Count - 1).MAJ_CoefficientsCombinaisons()

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