Imports System.IO
Imports PMXMoteur2

Public Class Frm_AjoutePP

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim NomCas() As String = {"G1", "G2", "Q", "QC"}

    Dim strProjet, strPoutre As String
    Dim indProjetN, indPoutreN As Integer

    'MODIF GUD car Dalle n'est plus dans la Cls_Section

    Dim MyPoutreAcier As New cls_Poutre(NomCas)
    Dim MyPoutreAcierEnrobe As New cls_Poutre(NomCas)
    Dim MyPoutreMixte As New cls_Poutre(NomCas)
    Dim MyPoutreMixteEnrobe As New cls_Poutre(NomCas)
    Dim MyPoutreSFB As New cls_Poutre(NomCas)
    Dim MyPoutreIFB_A As New cls_Poutre(NomCas)
    Dim MyPoutreIFB_B As New cls_Poutre(NomCas)
    Dim MyPoutreSAB As New cls_Poutre(NomCas)
    Dim MyPoutreSFBmixte As New cls_Poutre(NomCas)
    Dim MyPoutreIFB_Amixte As New cls_Poutre(NomCas)
    Dim MyPoutreIFB_Bmixte As New cls_Poutre(NomCas)
    Dim MyPoutreSABmixte As New cls_Poutre(NomCas)

    Dim MySectionAcier As cls_Section = MyPoutreAcier.Section
    Dim MySectionAcierEnrobe As cls_Section = MyPoutreAcierEnrobe.Section
    Dim MySectionMixte As cls_Section = MyPoutreMixte.Section
    Dim MySectionMixteEnrobe As cls_Section = MyPoutreMixteEnrobe.Section
    Dim MySectionSFB As cls_Section = MyPoutreSFB.Section
    Dim MySectionIFB_A As cls_Section = MyPoutreIFB_A.Section
    Dim MySectionIFB_B As cls_Section = MyPoutreIFB_B.Section
    Dim MySectionSAB As cls_Section = MyPoutreSAB.Section
    Dim MySectionSFBmixte As cls_Section = MyPoutreSFBmixte.Section
    Dim MySectionIFB_Amixte As cls_Section = MyPoutreIFB_Amixte.Section
    Dim MySectionIFB_Bmixte As cls_Section = MyPoutreIFB_Bmixte.Section
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

    Dim TypeSection As cls_Section.Enum_TypeSection = cls_Section.Enum_TypeSection.AcierSeul

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
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_AJOUTEPP")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")

                Me.btn_Annuler.Text = Bloc("CANCEL")
                Me.btn_OK.Text = Bloc("OK")

                Me.chk_NouveauProjet.Text = ""
                Me.lbl_NouveauProjet.Text = Bloc("PROJECTNAME")
                Me.chk_ProjetEnCours.Text = Bloc("PROJECT")

                Me.chk_NouvellePoutre.Text = ""
                Me.lbl_NouvellePoutre.Text = Bloc("NEWBEAM")
                Me.chk_PoutreEnCours.Text = Bloc("MODIFYBEAM")

                strProjet = Bloc("PROJECT")
                strPoutre = Bloc("BEAM")

                Me.lbl_TypeSection.Text = Bloc("SECTIONTYPE")
                strType = Bloc("SECTIONTYPE")

                'Me.chk_SectionAcier.Text = "Section non mixte"
                'Me.chk_SectionAcierEnrobe.Text = "Section acier avec enrobage partiel"


                'tabType.Clear()
                'tabType.Add(cls_Section.Enum_TypeSection.Acier, "Section non mixte")
                'tabType.Add(cls_Section.Enum_TypeSection.AcierEnrobage, "Section acier avec enrobage partiel")
                'tabType.Add(cls_Section.Enum_TypeSection.Mixte, "Section mixte")
                'tabType.Add(cls_Section.Enum_TypeSection.MixteEnrobage, "Section mixte avec enrobage partiel")
                'tabType.Add(cls_Section.Enum_TypeSection.SFB, "Section dalle mince SFB non mixte")
                'tabType.Add(cls_Section.Enum_TypeSection.SFBmixte, "Section dalle mince SFB mixte")
                'tabType.Add(cls_Section.Enum_TypeSection.SAB, "Section dalle mince SAB non mixte")
                'tabType.Add(cls_Section.Enum_TypeSection.SABmixte, "Section dalle mince SAB mixte")

                tabType.Clear()
                tabType.Add(cls_Section.Enum_TypeSection.AcierSeul, Bloc("STEELSECTION"))
                tabType.Add(cls_Section.Enum_TypeSection.AcierSeulEnrobage, Bloc("STEELSECTIONPARTENC"))
                tabType.Add(cls_Section.Enum_TypeSection.Mixte, Bloc("COMPOSITESECTION"))
                tabType.Add(cls_Section.Enum_TypeSection.MixteEnrobage, Bloc("COMPOSITESECTIONPARTENC"))
                tabType.Add(cls_Section.Enum_TypeSection.SFB, Bloc("SLIMFLOORSFB"))
                tabType.Add(cls_Section.Enum_TypeSection.SFBmixte, Bloc("SLIMFLOORSFBCOMPO"))
                tabType.Add(cls_Section.Enum_TypeSection.IFB_A, Bloc("SLIMFLOORIFB_A"))
                tabType.Add(cls_Section.Enum_TypeSection.IFB_Amixte, Bloc("SLIMFLOORIFB_ACOMPO"))
                tabType.Add(cls_Section.Enum_TypeSection.IFB_B, Bloc("SLIMFLOORIFB_B"))
                tabType.Add(cls_Section.Enum_TypeSection.IFB_Bmixte, Bloc("SLIMFLOORIFB_BCOMPO"))
                tabType.Add(cls_Section.Enum_TypeSection.SAB, Bloc("SLIMFLOORSAB"))
                tabType.Add(cls_Section.Enum_TypeSection.SABmixte, Bloc("SLIMFLOORSABCOMPO"))

                Me.MyToolTip.SetToolTip(Me.chk_SectionAcier, tabType(cls_Section.Enum_TypeSection.AcierSeul))
                Me.MyToolTip.SetToolTip(Me.chk_SectionAcierEnrobe, tabType(cls_Section.Enum_TypeSection.AcierSeulEnrobage))
                Me.MyToolTip.SetToolTip(Me.chk_SectionMixte, tabType(cls_Section.Enum_TypeSection.Mixte))
                Me.MyToolTip.SetToolTip(Me.chk_SectionMixteEnrobe, tabType(cls_Section.Enum_TypeSection.MixteEnrobage))
                Me.MyToolTip.SetToolTip(Me.chk_SFBAcier, tabType(cls_Section.Enum_TypeSection.SFB))
                Me.MyToolTip.SetToolTip(Me.chk_SFBMixte, tabType(cls_Section.Enum_TypeSection.SFBmixte))
                Me.MyToolTip.SetToolTip(Me.chk_IFB_A_Acier, tabType(cls_Section.Enum_TypeSection.IFB_A))
                Me.MyToolTip.SetToolTip(Me.chk_IFB_A_Mixte, tabType(cls_Section.Enum_TypeSection.IFB_Amixte))
                Me.MyToolTip.SetToolTip(Me.chk_IFB_B_Acier, tabType(cls_Section.Enum_TypeSection.IFB_B))
                Me.MyToolTip.SetToolTip(Me.chk_IFB_B_Mixte, tabType(cls_Section.Enum_TypeSection.IFB_Bmixte))
                Me.MyToolTip.SetToolTip(Me.chk_SABAcier, tabType(cls_Section.Enum_TypeSection.SAB))
                Me.MyToolTip.SetToolTip(Me.chk_SABMixte, tabType(cls_Section.Enum_TypeSection.SABmixte))

                Me.chk_SectionAcier.Text = ""
                Me.chk_SectionAcierEnrobe.Text = ""
                Me.chk_SectionMixte.Text = ""
                Me.chk_SectionMixteEnrobe.Text = ""
                Me.chk_SFBAcier.Text = ""
                Me.chk_SFBMixte.Text = ""
                Me.chk_IFB_A_Acier.Text = ""
                Me.chk_IFB_A_Mixte.Text = ""
                Me.chk_IFB_B_Acier.Text = ""
                Me.chk_IFB_B_Mixte.Text = ""
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
        MySectionAcierEnrobe.typeSection = cls_Section.Enum_TypeSection.AcierSeulEnrobage
        MySectionAcierEnrobe.ProfilA.Tfs = 0.03
        MySectionAcierEnrobe.ProfilA.Tfi = 0.03
        MySectionAcierEnrobe.ProfilA.Rcs = 0.03
        MySectionAcierEnrobe.ProfilA.Rci = 0.03

        'MySectionAcierEnrobe.Enrobage.LitsArmaOLD(0).nbArma = 1
        'MySectionAcierEnrobe.Enrobage.LitsArmaOLD(0).Phi = 0.025
        'MySectionAcierEnrobe.Enrobage.LitsArmaOLD(2).nbArma = 1
        'MySectionAcierEnrobe.Enrobage.LitsArmaOLD(2).Phi = 0.025

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

        'MySectionMixteEnrobe.Enrobage.LitsArmaOLD(0).nbArma = 1
        'MySectionMixteEnrobe.Enrobage.LitsArmaOLD(0).Phi = 0.025
        'MySectionMixteEnrobe.Enrobage.LitsArmaOLD(2).nbArma = 1
        'MySectionMixteEnrobe.Enrobage.LitsArmaOLD(2).Phi = 0.025

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

        '--> Définition de la section IFB_A non mixte
        MySectionIFB_A.typeSection = cls_Section.Enum_TypeSection.IFB_A
        MySectionIFB_A.ProfilA.Tfs = 0.03
        MySectionIFB_A.ProfilA.Tfi = 0
        MySectionIFB_A.ProfilA.Rcs = 0.03
        MySectionIFB_A.ProfilA.Rci = 0
        MySectionIFB_A.ProfilA.Plat_t = 0.03
        MySectionIFB_A.ProfilA.Plat_b = 0.45

        '--> Définition de la section IFB_B non mixte
        MySectionIFB_B.typeSection = cls_Section.Enum_TypeSection.IFB_B
        MySectionIFB_B.ProfilA.Bfs = 0
        MySectionIFB_B.ProfilA.Tfs = 0
        MySectionIFB_B.ProfilA.Tfi = 0.03
        MySectionIFB_B.ProfilA.Rcs = 0
        MySectionIFB_B.ProfilA.Rci = 0.03
        MySectionIFB_B.ProfilA.Plat_t = 0.03
        MySectionIFB_B.ProfilA.Plat_b = 2 * MySectionIFB_B.ProfilA.Bfi / 3

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

        '--> Définition de la section IFB_A mixte
        MySectionIFB_Amixte.typeSection = cls_Section.Enum_TypeSection.IFB_Amixte
        MySectionIFB_Amixte.ProfilA.Tfs = 0.03
        MySectionIFB_Amixte.ProfilA.Tfi = 0
        MySectionIFB_Amixte.ProfilA.Rcs = 0.03
        MySectionIFB_Amixte.ProfilA.Rci = 0
        MySectionIFB_Amixte.ProfilA.Plat_t = 0.03
        MySectionIFB_Amixte.ProfilA.Plat_b = 0.45

        MyPoutreIFB_Amixte.Dalle.t_d = 0.05
        'MySectionSFBmixte.Dalle.t_d = 0.05
        MyPoutreIFB_Amixte.Dalle.Beff = 0.75
        'MySectionSFBmixte.Dalle.Beff = 0.75

        '--> Définition de la section IFB_B mixte
        MySectionIFB_Bmixte.typeSection = cls_Section.Enum_TypeSection.IFB_Bmixte
        MySectionIFB_Bmixte.ProfilA.Bfs = 0
        MySectionIFB_Bmixte.ProfilA.Tfs = 0
        MySectionIFB_Bmixte.ProfilA.Tfi = 0.03
        MySectionIFB_Bmixte.ProfilA.Rcs = 0
        MySectionIFB_Bmixte.ProfilA.Rci = 0.03
        MySectionIFB_Bmixte.ProfilA.Plat_t = 0.03
        MySectionIFB_Bmixte.ProfilA.Plat_b = 2 * MySectionIFB_Bmixte.ProfilA.Bfi / 3

        MyPoutreIFB_Bmixte.Dalle.t_d = 0.05
        'MySectionSFBmixte.Dalle.t_d = 0.05
        MyPoutreIFB_Bmixte.Dalle.Beff = 0.75
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

    Private Sub PaintBoutons(sender As Object, e As PaintEventArgs) Handles chk_SectionAcier.Paint, chk_SectionAcierEnrobe.Paint, chk_SectionMixteEnrobe.Paint, chk_SectionMixte.Paint, chk_SFBAcier.Paint, chk_SFBMixte.Paint, chk_SABMixte.Paint, chk_SABAcier.Paint, chk_IFB_B_Acier.Paint, chk_IFB_A_Mixte.Paint, chk_IFB_B_Mixte.Paint, chk_IFB_A_Acier.Paint

        Const kAdjust As Single = 0.9
        Dim MyFont As New Font("Arial", 8)

        Select Case sender.name
            Case Me.chk_SectionAcier.Name
                DessinFrmTypeSection(e.Graphics, MySectionAcier, MyPoutreAcier.Dalle, Me.chk_SectionAcier.ClientRectangle.Width, Me.chk_SectionAcier.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.AcierSeul)

            Case Me.chk_SectionAcierEnrobe.Name
                DessinFrmTypeSection(e.Graphics, MySectionAcierEnrobe, MyPoutreAcierEnrobe.Dalle, Me.chk_SectionAcierEnrobe.ClientRectangle.Width, Me.chk_SectionAcierEnrobe.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.AcierSeulEnrobage)

            Case Me.chk_SectionMixte.Name
                DessinFrmTypeSection(e.Graphics, MySectionMixte, MyPoutreMixte.Dalle, Me.chk_SectionMixte.ClientRectangle.Width, Me.chk_SectionMixte.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.Mixte)

            Case Me.chk_SectionMixteEnrobe.Name
                DessinFrmTypeSection(e.Graphics, MySectionMixteEnrobe, MyPoutreMixteEnrobe.Dalle, Me.chk_SectionMixteEnrobe.ClientRectangle.Width, Me.chk_SectionMixteEnrobe.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.MixteEnrobage)

            Case Me.chk_SFBAcier.Name
                DessinFrmTypeSection(e.Graphics, MySectionSFB, MyPoutreSFB.Dalle, Me.chk_SFBAcier.ClientRectangle.Width, Me.chk_SFBAcier.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.SFB)

            Case Me.chk_IFB_A_Acier.Name
                DessinFrmTypeSection(e.Graphics, MySectionIFB_A, MyPoutreIFB_A.Dalle, Me.chk_IFB_A_Acier.ClientRectangle.Width, Me.chk_IFB_A_Acier.Height,
                MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.IFB_A)

            Case Me.chk_IFB_A_Mixte.Name
                DessinFrmTypeSection(e.Graphics, MySectionIFB_Amixte, MyPoutreIFB_Amixte.Dalle, Me.chk_IFB_A_Mixte.ClientRectangle.Width, Me.chk_IFB_A_Mixte.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.IFB_Amixte)

            Case Me.chk_IFB_B_Acier.Name
                DessinFrmTypeSection(e.Graphics, MySectionIFB_B, MyPoutreIFB_B.Dalle, Me.chk_IFB_B_Acier.ClientRectangle.Width, Me.chk_IFB_B_Acier.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.IFB_B)

            Case Me.chk_IFB_B_Mixte.Name
                DessinFrmTypeSection(e.Graphics, MySectionIFB_Bmixte, MyPoutreIFB_Bmixte.Dalle, Me.chk_IFB_B_Mixte.ClientRectangle.Width, Me.chk_IFB_B_Mixte.Height,
                                     MyFont, kAdjust, TypeSection = cls_Section.Enum_TypeSection.IFB_Bmixte)

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

    Private Sub ChoixSection_Changed(sender As Object, e As EventArgs) Handles chk_SectionAcierEnrobe.CheckedChanged, chk_SectionAcier.CheckedChanged, chk_SectionMixteEnrobe.CheckedChanged, chk_SectionMixte.CheckedChanged, chk_SFBAcier.CheckedChanged, chk_SFBMixte.CheckedChanged, chk_SABMixte.CheckedChanged, chk_SABAcier.CheckedChanged, chk_IFB_B_Acier.CheckedChanged, chk_IFB_A_Mixte.CheckedChanged, chk_IFB_B_Mixte.CheckedChanged, chk_IFB_A_Acier.CheckedChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.chk_SectionAcier.Name : TypeSection = cls_Section.Enum_TypeSection.AcierSeul
            Case Me.chk_SectionAcierEnrobe.Name : TypeSection = cls_Section.Enum_TypeSection.AcierSeulEnrobage
            Case Me.chk_SectionMixte.Name : TypeSection = cls_Section.Enum_TypeSection.Mixte
            Case Me.chk_SectionMixteEnrobe.Name : TypeSection = cls_Section.Enum_TypeSection.MixteEnrobage
            Case Me.chk_SFBAcier.Name : TypeSection = cls_Section.Enum_TypeSection.SFB
            Case Me.chk_SFBMixte.Name : TypeSection = cls_Section.Enum_TypeSection.SFBmixte
            Case Me.chk_IFB_A_Acier.Name : TypeSection = cls_Section.Enum_TypeSection.IFB_A
            Case Me.chk_IFB_A_Mixte.Name : TypeSection = cls_Section.Enum_TypeSection.IFB_Amixte
            Case Me.chk_IFB_B_Acier.Name : TypeSection = cls_Section.Enum_TypeSection.IFB_B
            Case Me.chk_IFB_B_Mixte.Name : TypeSection = cls_Section.Enum_TypeSection.IFB_Bmixte
            Case Me.chk_SABAcier.Name : TypeSection = cls_Section.Enum_TypeSection.SAB
            Case Me.chk_SABMixte.Name : TypeSection = cls_Section.Enum_TypeSection.SABmixte
        End Select

        lBuild = True
        MAJ_EtatChkSection()
        lBuild = False
    End Sub

    Private Sub MAJ_EtatChkSection()

        Me.chk_SectionAcier.Checked = TypeSection = cls_Section.Enum_TypeSection.AcierSeul
        Me.chk_SectionAcierEnrobe.Checked = TypeSection = cls_Section.Enum_TypeSection.AcierSeulEnrobage
        Me.chk_SectionMixte.Checked = TypeSection = cls_Section.Enum_TypeSection.Mixte
        Me.chk_SectionMixteEnrobe.Checked = TypeSection = cls_Section.Enum_TypeSection.MixteEnrobage
        Me.chk_SFBAcier.Checked = TypeSection = cls_Section.Enum_TypeSection.SFB
        Me.chk_SFBMixte.Checked = TypeSection = cls_Section.Enum_TypeSection.SFBmixte
        Me.chk_IFB_A_Acier.Checked = TypeSection = cls_Section.Enum_TypeSection.IFB_A
        Me.chk_IFB_A_Mixte.Checked = TypeSection = cls_Section.Enum_TypeSection.IFB_Amixte
        Me.chk_IFB_B_Acier.Checked = TypeSection = cls_Section.Enum_TypeSection.IFB_B
        Me.chk_IFB_B_Mixte.Checked = TypeSection = cls_Section.Enum_TypeSection.IFB_Bmixte
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
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.AcierSeul, NomPoutre, NomChargements))
            End If

            If Me.chk_SectionAcierEnrobe.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.AcierSeulEnrobage, NomPoutre, NomChargements))
            End If

            If Me.chk_SectionMixte.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.Mixte, NomPoutre, NomChargements))
            End If

            If Me.chk_SectionMixteEnrobe.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.MixteEnrobage, NomPoutre, NomChargements))
            End If

            If Me.chk_SFBAcier.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.SFB, NomPoutre, NomChargements))
            End If

            If Me.chk_SFBMixte.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.SFBmixte, NomPoutre, NomChargements))
            End If

            If Me.chk_IFB_A_Acier.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.IFB_A, NomPoutre, NomChargements))
            End If

            If Me.chk_IFB_A_Mixte.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.IFB_Amixte, NomPoutre, NomChargements))
            End If

            If Me.chk_IFB_B_Acier.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.IFB_B, NomPoutre, NomChargements))
            End If

            If Me.chk_IFB_B_Mixte.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.IFB_Bmixte, NomPoutre, NomChargements))
            End If


            If Me.chk_SABAcier.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.SAB, NomPoutre, NomChargements))
            End If

            If Me.chk_SABMixte.Checked Then
                lAjout = True
                MyProjet.Poutres.Add(New cls_Poutre(cls_Section.Enum_TypeSection.SABmixte, NomPoutre, NomChargements))
            End If


            InitialisePoutreDeBases(MyProjet.Poutres(MyProjet.Poutres.Count - 1), lOK)

            InitialiseBacDeBase(MyProjet.Poutres(MyProjet.Poutres.Count - 1).Dalle.Bac, lTrouve)
            InitialiseGoujonDeBase(MyProjet.Poutres(MyProjet.Poutres.Count - 1).Dalle.Connecteur, lTrouve)

            MyProjet.Poutres(MyProjet.Poutres.Count - 1).Initialise_CoefficientsCombinaisons()

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