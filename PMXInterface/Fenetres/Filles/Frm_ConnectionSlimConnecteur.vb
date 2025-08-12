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

    End Sub

    Private Sub GestionUnites()

        '--> Etiquettes unités partie goujons soudés
        Me.etq_UnitD.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitHsc.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitFy.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)
        Me.etq_UnitFu.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)


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

        MAJ_DimensionsConnecteur()

        MAJI_Avertissement()

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
            Case Me.rdb_GoujonSemSup.Checked
                ' Goujons soudés sur la semelle supérieure
                Frm_ConnectionSlimN.localBeam.Dalle.typeConnecteur = PMXMoteur2.cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup
                MAJAfficheConnecteurEnCours()
                MAJI_Avertissement()
        End Select

        Me.img_Stud.Invalidate()

    End Sub

#End Region

#Region " Evènements saisie "

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
        img_Stud.Invalidate()

        'MAJ_Valeurs_Limites()
    End Sub

    Private Sub MAJ_DimensionsConnecteur()

        Me.txt_hsc.Text = GetStringInUnit(Frm_ConnectionSlimN.localBeam.Dalle.Goujons.hsc, Enu_TypeVariable.Dimension, 4, 0, False)
        Me.txt_d.Text = GetStringInUnit(Frm_ConnectionSlimN.localBeam.Dalle.Goujons.d, Enu_TypeVariable.Dimension, 4, 0, False)
        Me.txt_fy.Text = GetStringInUnit(Frm_ConnectionSlimN.localBeam.Dalle.Goujons.Fy, Enu_TypeVariable.Contrainte, 4, 0, False)
        Me.txt_fu.Text = GetStringInUnit(Frm_ConnectionSlimN.localBeam.Dalle.Goujons.Fu, Enu_TypeVariable.Contrainte, 4, 0, False)

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

#End Region

#Region " Dessins "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_d.Paint, img_hsc.Paint, img_fy.Paint, img_fu.Paint, img_PhiS.Paint, img_Fsk.Paint

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


            Case Me.img_fy.Name

                strSymbol = "f"
                strIndice = "y "

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