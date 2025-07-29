

Public Class Frm_ConnectionSlimConnecteur

#Region " Variables "

    Dim lBuild As Boolean
    Const PrefixeG As String = "M "

    Dim lGoujon As Boolean              ' Indique si connexion par goujon, soit semelle soit âme
    Dim lArmaDispo As Boolean           ' Indique si connexion par armature disponible

    Dim strStud, strArma As String
    Dim strInfoW_ArmaConnex As String

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(Bloc As Dictionary(Of String, String))

        lBuild = True

        GestionLangues(Bloc)
        GestionStyle()
        GestionUnites()

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

    Private Sub GestionLangues(Bloc As Dictionary(Of String, String))
        Try

            Me.lbl_Type.Text = Bloc("CONNECTORSTYPE")

            Me.rdb_Armatures.Text = Bloc("WEB_REINF")
            Me.rdb_GoujonAme.Text = Bloc("WEB_STUD")
            Me.rdb_GoujonSemSup.Text = Bloc("FLANGE_STUD")

            Me.lbl_Stud.Text = Bloc("STUDS")

            strStud = Bloc("WSTUDS")
            strArma = Bloc("WEB_REINF")

            strInfoW_ArmaConnex = Bloc("INFOREINF")

            'Me.lbl_Connecteur.Text = Bloc("CONNECTEUR")

        Catch ex As Exception

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

        MAJ_DimensionsConnecteur()

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

        lArmaDispo = IsGreaterOrEqual(Frm_ConnectionSlimN.localBeam.Section.ProfilA.Tw, TWMINARMA) _
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
            Case Me.rdb_GoujonSemSup.Checked
                ' Goujons soudés sur la semelle supérieure
                Frm_ConnectionSlimN.localBeam.Dalle.typeConnecteur = PMXMoteur2.cls_Dalle.Enum_TypeConnecteur.GoujonSoudeSemelleSup
                MAJAfficheConnecteurEnCours()
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
        img_Stud.Invalidate()

        'MAJ_Valeurs_Limites()
    End Sub

    Private Sub MAJ_DimensionsConnecteur()

        Me.txt_hsc.Text = GetStringInUnit(Frm_ConnectionSlimN.localBeam.Dalle.Goujons.hsc, Enu_TypeVariable.Dimension, 4, 0, False)
        Me.txt_d.Text = GetStringInUnit(Frm_ConnectionSlimN.localBeam.Dalle.Goujons.d, Enu_TypeVariable.Dimension, 4, 0, False)
        Me.txt_fy.Text = GetStringInUnit(Frm_ConnectionSlimN.localBeam.Dalle.Goujons.Fy, Enu_TypeVariable.Contrainte, 4, 0, False)
        Me.txt_fu.Text = GetStringInUnit(Frm_ConnectionSlimN.localBeam.Dalle.Goujons.Fu, Enu_TypeVariable.Contrainte, 4, 0, False)

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

        InfoW_Initialise()
        InfoW_Add(strInfoW_ArmaConnex)

        InfosW_Publie()

    End Sub

#End Region

End Class