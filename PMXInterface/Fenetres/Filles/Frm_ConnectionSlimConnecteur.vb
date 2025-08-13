Imports PMXMoteur2

Public Class Frm_ConnectionSlimConnecteur

#Region " Variables "

    Dim lBuild As Boolean
    Const PrefixeG As String = "M "

    Dim lGoujon As Boolean              ' Indique si connexion par goujon, soit semelle soit âme
    Dim lArmaDispo As Boolean           ' Indique si connexion par armature disponible

    Dim strStud, strArma As String
    Dim strInfoW_ArmaConnex As String

    Dim tabLabelGoujons() As String

    Dim strGoujonsEnrobageMini As String
    Dim strGoujonsDepasseDalle As String

    Dim ClasseAcierArma() As String = cls_AcierArmature.tabClasseAcierArma


#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(Bloc As Dictionary(Of String, String))

        lBuild = True

        GestionLangues(Bloc)
        GestionStyle()
        GestionUnites()

        InitialiseVariablesLocales()

        PrepareFenetre()
        AfficheConnecteurEnCours()

        lBuild = False

    End Sub

    Private Sub GestionStyle()

        Me.pan_Main.Dock = DockStyle.Fill

        Me.pan_Image.Dock = DockStyle.Fill
        Me.img_Stud.Dock = DockStyle.Fill

        Me.lbl_Connecteur.BackColor = CouleurBackBandeaux
        Me.lbl_Connecteur.ForeColor = CouleurForeBandeaux

        Me.lbl_Type.BackColor = CouleurBackBandeaux
        Me.lbl_Type.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(txt_Fsk, False)
        PrepareTextBoxDipo(txt_d, False)
        PrepareTextBoxDipo(txt_fu, False)
        PrepareTextBoxDipo(txt_hsc, False)
        PrepareTextBoxDipo(txt_PRd, False)
        PrepareTextBoxDipo(txt_PRdarma, False)

    End Sub

    Private Sub GestionUnites()

        '--> Etiquettes unités partie goujons soudés
        Me.etq_UnitD.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitHsc.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitFu.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)
        Me.etq_UnitPRd.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)

        '--> Etiquettes unités partie armatures 
        Me.etq_UnitPhiS.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitFsk.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub InitialiseVariablesLocales()

        Dim nbStuds As Integer = BaseGoujons.Count
        ReDim tabLabelGoujons(nbStuds - 1)

        For iStud As Integer = 0 To nbStuds - 1
            tabLabelGoujons(iStud) = BaseGoujons(iStud).nom
        Next

    End Sub

    Private Sub GestionLangues(Bloc As Dictionary(Of String, String))
        Dim strLoadedKey As String = ""
        Dim CLE As String = ""

        Try

            CLE = "CONNECTORSTYPE" : Me.lbl_Type.Text = Bloc(CLE)

            CLE = "WEB_REINF" : Me.rdb_Armatures.Text = Bloc(CLE)
            CLE = "WEB_STUD" : Me.rdb_GoujonAme.Text = Bloc(CLE)
            CLE = "FLANGE_STUD" : Me.rdb_GoujonSemSup.Text = Bloc(CLE)

            CLE = "STUDS" : Me.lbl_Stud.Text = Bloc(CLE)

            CLE = "WSTUDS" : strStud = Bloc(CLE)
            CLE = "WEB_REINF" : strArma = Bloc(CLE)

            CLE = "INFOREINF" : strInfoW_ArmaConnex = Bloc(CLE)

            CLE = "W_MINIMUMCOVERAGE" : strGoujonsEnrobageMini = Bloc(CLE)
            CLE = "W_STUDABOVESLAB" : strGoujonsDepasseDalle = Bloc(CLE)


            CLE = "DIAMETER" : Me.lbl_Diameter.Text = Bloc(CLE)
            CLE = "CLASS" : Me.lbl_ClasseA.Text = Bloc(CLE)

        Catch ex As Exception

            GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)

        End Try
    End Sub

    Private Sub AfficheConnecteurEnCours()

        MAJAfficheConnecteurEnCours()

        Select Case Frm_ConnectionSlimN.localBeam.Dalle.typeConnecteur
            Case PMXMoteur2.cls_Dalle.Enum_TypeConnecteur.ArmatureAme
                Me.rdb_Armatures.Checked = True
            Case PMXMoteur2.cls_Dalle.Enum_TypeConnecteur.GoujonSoudeAme
                Me.rdb_GoujonAme.Checked = True
            Case PMXMoteur2.cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup
                Me.rdb_GoujonSemSup.Checked = True
        End Select

        Me.cmb_goujons.SelectedIndex = Array.IndexOf(tabLabelGoujons, Frm_ConnectionSlimN.localBeam.Dalle.Goujons.nom)

        '--( Affichage armature

        Dim Chaine As String
        With Frm_ConnectionSlimN.localBeam.Dalle.ConnecteurArmature
            Me.cmb_Diametre.SelectedIndex = Array.IndexOf(Cls_Armatures_Longi.TabDiametres, .Diametre)
            Chaine = .Acier.Classe
            If Me.ClasseAcierArma.Contains(Chaine) Then
                Me.cmb_Acier.SelectedIndex = Array.IndexOf(Me.ClasseAcierArma, Chaine)
            Else
                Me.cmb_Acier.SelectedIndex = 0
            End If

        End With

        MAJ_DimensionsConnecteur()

        MAJI_Avertissement()
        MAJI_ProprietesAcier()

        MAJI_PRd()
        MAJI_PRdArma()
    End Sub

    Private Sub MAJAfficheConnecteurEnCours()

        Dim lGoujonSave As Boolean = lGoujon
        lGoujon = Not (Frm_ConnectionSlimN.localBeam.Dalle.typeConnecteur = PMXMoteur2.cls_Dalle.Enum_TypeConnecteur.ArmatureAme)

        If lBuild Or Not (lGoujon = lGoujonSave) Then

            AffichagePanneauGoujon()

        End If

    End Sub

    Private Sub AffichagePanneauGoujon()

        If lGoujon Then
            Me.pan_ConteneurConnecteur.Controls.Clear()
            Me.pan_ConteneurConnecteur.Controls.Add(Me.pan_SaisieGoujons)
            Me.pan_SaisieGoujons.Dock = DockStyle.Fill
            Me.lbl_Connecteur.Text = strStud
        Else
            Me.pan_ConteneurConnecteur.Controls.Clear()
            Me.pan_ConteneurConnecteur.Controls.Add(Me.pan_SaisieArmature)
            Me.pan_SaisieArmature.Dock = DockStyle.Fill
            Me.lbl_Connecteur.Text = strArma
        End If

    End Sub

    Private Sub PrepareFenetre()

        RemplirComboGoujons()
        RemplirComboDiametreArma()
        RemplirComboAvecTableau(Me.cmb_Acier, ClasseAcierArma)

        lArmaDispo = IsGreaterOrEqual(Frm_ConnectionSlimN.localBeam.Section.ProfilA.Tw, OptionsSlimFloor.Twcdmin) _
                 And (Not Frm_ConnectionSlimN.localBeam.Section.lSlimFloor_IFB_B)

        Me.rdb_Armatures.Enabled = lArmaDispo
        Me.img_info.Visible = Not lArmaDispo

    End Sub

    Private Sub RemplirComboGoujons()

        Dim nbStuds As Integer = BaseGoujons.Count

        Me.cmb_goujons.Items.Clear()

        For iStud As Integer = 0 To nbStuds - 1

            Me.cmb_goujons.Items.Add(PrefixeG & BaseGoujons(iStud).nom)

        Next

        Me.cmb_goujons.SelectedIndex = 0
    End Sub

    Private Sub RemplirComboDiametreArma()

        Me.cmb_Diametre.Items.Clear()

        For i As Integer = 0 To Cls_Armatures_Longi.TabDiametres.Count - 1
            Me.cmb_Diametre.Items.Add(GetStringInUnitN(Cls_Armatures_Longi.TabDiametres(i), Enu_TypeVariable.Dimension, 4, 3, True, True))
        Next

        Me.cmb_Diametre.SelectedItem = 0

    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)

        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)

    End Sub

#End Region

#Region " Evenements rdb "
    Private Sub rdb_CheckedChanged(sender As Object, e As EventArgs) _
        Handles rdb_GoujonSemSup.CheckedChanged, rdb_GoujonAme.CheckedChanged, rdb_Armatures.CheckedChanged

        Select Case True
            Case Me.rdb_Armatures.Checked
                ' Armatures'
                Frm_ConnectionSlimN.localBeam.Dalle.typeConnecteur = PMXMoteur2.cls_Dalle.Enum_TypeConnecteur.ArmatureAme
                MAJAfficheConnecteurEnCours()
            Case Me.rdb_GoujonAme.Checked
                ' Goujons soudés sur les âmes
                Frm_ConnectionSlimN.localBeam.Dalle.typeConnecteur = PMXMoteur2.cls_Dalle.Enum_TypeConnecteur.GoujonSoudeAme
                MAJAfficheConnecteurEnCours()
                MAJI_Avertissement()
                MAJI_PRd()
            Case Me.rdb_GoujonSemSup.Checked
                ' Goujons soudés sur la semelle supérieure
                Frm_ConnectionSlimN.localBeam.Dalle.typeConnecteur = PMXMoteur2.cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup
                MAJAfficheConnecteurEnCours()
                MAJI_Avertissement()
                MAJI_PRd()
        End Select

        Me.img_Stud.Invalidate()

    End Sub

#End Region

#Region " Evènements saisie "


    Private Sub cmb_Diametre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Diametre.SelectedIndexChanged

        If lBuild Then Exit Sub

        Frm_ConnectionSlimN.localBeam.Dalle.ConnecteurArmature.Diametre = Cls_Armatures_Longi.TabDiametres(Me.cmb_Diametre.SelectedIndex)
        MAJI_PRdArma()
        img_Stud.Invalidate()

    End Sub

    Private Sub cmb_Acier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Acier.SelectedIndexChanged
        If lBuild Then Exit Sub

        Frm_ConnectionSlimN.localBeam.Dalle.ConnecteurArmature.Acier.Classe = cls_AcierArmature.tabClasseAcierArma(Me.cmb_Acier.SelectedIndex)
        MAJI_ProprietesAcier()
        MAJI_PRdArma()
    End Sub

    Private Sub cmb_goujons_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_goujons.SelectedIndexChanged
        If lBuild Then Exit Sub

        Dim iStud As Integer = cmb_goujons.SelectedIndex

        Frm_ConnectionSlimN.localBeam.Dalle.Goujons.nom = BaseGoujons(iStud).nom
        Frm_ConnectionSlimN.localBeam.Dalle.Goujons.hsc = BaseGoujons(iStud).hsc
        Frm_ConnectionSlimN.localBeam.Dalle.Goujons.d = BaseGoujons(iStud).d
        Frm_ConnectionSlimN.localBeam.Dalle.Goujons.Fy = BaseGoujons(iStud).Fy
        Frm_ConnectionSlimN.localBeam.Dalle.Goujons.Fu = BaseGoujons(iStud).Fu

        MAJ_DimensionsConnecteur()
        MAJI_Avertissement()
        MAJI_PRd()
        img_Stud.Invalidate()

        'MAJ_Valeurs_Limites()
    End Sub

    Private Sub MAJ_DimensionsConnecteur()

        Me.txt_hsc.Text = GetStringInUnit(Frm_ConnectionSlimN.localBeam.Dalle.Goujons.hsc, Enu_TypeVariable.Dimension, 4, 0, False)
        Me.txt_d.Text = GetStringInUnit(Frm_ConnectionSlimN.localBeam.Dalle.Goujons.d, Enu_TypeVariable.Dimension, 4, 0, False)
        Me.txt_fu.Text = GetStringInUnit(Frm_ConnectionSlimN.localBeam.Dalle.Goujons.Fu, Enu_TypeVariable.Contrainte, 4, 0, False)

    End Sub

    Private Sub MAJI_PRdArma()

        Dim PRd As Decimal
        Dim GammaVs As Decimal = Frm_ConnectionSlimN.localBeam.Param.Gamma.GammaVs
        Dim Ha As Decimal = Frm_ConnectionSlimN.localBeam.Section.ProfilA.ha
        Dim Tw As Decimal = Frm_ConnectionSlimN.localBeam.Section.ProfilA.Tw
        Dim Nuance As String = Frm_ConnectionSlimN.localBeam.Section.Acier.Nuance

        PRd = Frm_ConnectionSlimN.localBeam.Dalle.ConnecteurArmature.PRd(GammaVs, nuance, tw, ha)

        Me.txt_PRdArma.Text = GetStringInUnitN(PRd, Enu_TypeVariable.Effort, 4, 3, NON_U, True)

    End Sub

    Private Sub MAJI_PRd()

        Dim PRd As Decimal
        Dim lGeneration1 As Boolean = Frm_ConnectionSlimN.localBeam.Param.lGeneration1
        Dim lDalleP As Boolean = True
        Dim lPerp As Boolean = True
        Dim lPerpPRd As Boolean = True
        Dim lCofra220 As Boolean = False
        Dim FcK As Decimal = Frm_ConnectionSlimN.localBeam.Dalle.beton.Fck
        Dim Fctk_005 As Decimal = Frm_ConnectionSlimN.localBeam.Dalle.beton.Fctk_005
        Dim GammaVs As Decimal = Frm_ConnectionSlimN.localBeam.Param.Gamma.GammaVs
        Dim GammaVc As Decimal = Frm_ConnectionSlimN.localBeam.Param.Gamma.GammaVc
        Dim Ecm As Decimal = Frm_ConnectionSlimN.localBeam.Dalle.beton.Ecm
        Dim Nr As Integer = Frm_ConnectionSlimN.localBeam.NrTransZone(1, 0)

        PRd = Frm_ConnectionSlimN.localBeam.Dalle.Goujons.ResistancePRd(lGeneration1, lDalleP, lPerp, lCofra220, Frm_ConnectionSlimN.localBeam.Dalle.Bac, Nr, FcK, Ecm, Fctk_005, GammaVs, GammaVc)

        Me.txt_PRd.Text = GetStringInUnitN(PRd, Enu_TypeVariable.Effort, 4, 3, NON_U, True)

    End Sub

    Private Sub MAJI_Avertissement()

        ' 1 Vérification pour un goulon sur semelle sup

        If Frm_ConnectionSlimN.localBeam.Dalle.typeConnecteur = PMXMoteur2.cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup Then

            ' 2 Est ce que le conneteur dépasse au dessus de la dalle

            Dim zTopD, zTopG As Decimal

            zTopD = Frm_ConnectionSlimN.localBeam.Dalle.zTop
            zTopG = Frm_ConnectionSlimN.localBeam.Section.zSemSup + Frm_ConnectionSlimN.localBeam.Dalle.Goujons.hsc

            If IsGreater(zTopG, zTopD) Then

                Me.lbl_Avertissement.Text = strGoujonsDepasseDalle
                Me.pan_Avertissement.Visible = True

            ElseIf IsGreater(zTopG + Goujons_EnrobageMini(), zTopD) Then

                Dim Enrobage As Decimal = zTopD - zTopG
                Dim valMin As Decimal = Goujons_EnrobageMini()
                Dim strEnrobage As String = GetStringInUnitN(Enrobage, Enu_TypeVariable.Dimension, 4, 3, Enu_AfficheUnite.OuiNdC, True)
                Dim strvalmin As String = GetStringInUnitN(valMin, Enu_TypeVariable.Dimension, 4, 3, Enu_AfficheUnite.OuiNdC, True)

                Me.lbl_Avertissement.Text = RemplaceDollar(RemplaceDollar(strGoujonsEnrobageMini, strEnrobage), strvalmin)
                Me.pan_Avertissement.Visible = True

            Else

                Me.pan_Avertissement.Visible = False

            End If

        Else

            Me.pan_Avertissement.Visible = False

        End If

    End Sub

    Private Sub MAJI_ProprietesAcier()
        Frm_ConnectionSlimN.localBeam.Dalle.ConnecteurArmature.Acier.MAJProprietes()
        Me.txt_Fsk.Text = GetStringNoUnit(Frm_ConnectionSlimN.localBeam.Dalle.ConnecteurArmature.Acier.FsK, Enu_TypeVariable.Contrainte)
    End Sub

#End Region

#Region " Dessins "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_d.Paint, img_hsc.Paint, img_fu.Paint, img_PhiS.Paint, img_Fsk.Paint, img_PRd.Paint, img_PRd2.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height
        Dim xStart As Single = sWI * 0.95

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lIndice, lEgal As Boolean
        Dim xPen As Single = xStart
        Dim hCar As Single = e.Graphics.MeasureString("X", FontSymbolNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yPen As Single = (sHI / 2 - hCar) / 2 + sHI * 0.15

        '--> Initialisation

        lIndice = True
        lGrec = False
        lEgal = True

        Select Case sender.name
            Case Me.img_hsc.Name

                strSymbol = "h"
                strIndice = "sc"

            Case Me.img_d.Name

                strSymbol = "d"
                strIndice = "  "

            Case Me.img_PRd.Name, Me.img_PRd2.Name

                strSymbol = "P"
                strIndice = "Rd "

            Case Me.img_fu.Name

                strSymbol = "f"
                strIndice = "u "

            Case Me.img_PhiS.Name

                strSymbol = "f"
                strIndice = "s"

                lGrec = True

            Case Me.img_Fsk.Name

                strSymbol = "f"
                strIndice = "sk"

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

    Private Sub DessinConnecteurs(sender As Object, e As PaintEventArgs) Handles img_Stud.Paint

        DessineDalleConnectionSlimfloor(e.Graphics, Me.img_Stud.ClientRectangle.Width, Me.img_Stud.ClientRectangle.Height,
                                        Frm_ConnectionSlimN.localBeam, MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire)

    End Sub

#End Region

#Region " Infos W "

    Private Sub img_info_Click(sender As Object, e As EventArgs) Handles img_info.Click

        PublieInfoArmaConnex()

    End Sub


    Private Sub PublieInfoArmaConnex()

        InfoW.InitialiseInfo()
        InfoW.AddInfo(strInfoW_ArmaConnex)

        InfoW.Publie()

    End Sub

#End Region

End Class