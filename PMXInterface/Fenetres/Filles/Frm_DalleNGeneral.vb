Imports System.Reflection
Imports PMXMoteur2

Public Class Frm_DalleNGeneral


#Region " Variables "

    Dim lBuild As Boolean

    Dim strType(2) As String

    Dim ClasseBeton() As String = cls_Beton.TabClasseBeton
    Dim ClasseBetonLeger() As String = cls_Beton.TabClasseBetonLeger

    Private Enum Enu_DefEpMixte
        Totale                  ' Définition d'une dalle mixte par son épaisseur totale
        Pleine                  ' Définition d'une dalle mixte par son épaisseur au dessus du bac
    End Enum
    Dim DefEpMixte As Enu_DefEpMixte = Enu_DefEpMixte.Totale

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(myBloc As Dictionary(Of String, String))

        lBuild = True

        GestionLangue(myBloc)
        GestionUnites()
        GestionStyle()
        PreparerFenetre()
        AfficherDalleEnCours()

        lBuild = False
    End Sub

    Private Sub GestionLangue(myBloc As Dictionary(Of String, String))

        Dim strLoadedKey As String = ""
        Dim CLE As String = ""

        Try
            '=== GENERAL ======================================================================

            CLE = "GENERAL" : Me.lbl_General.Text = myBloc(CLE)
            CLE = "SOLIDSLAB" : strType(0) = myBloc(CLE)
            CLE = "COMPOSITESLAB" : strType(1) = myBloc(CLE)
            CLE = "PRECASTSLAB" : strType(2) = myBloc(CLE)

            CLE = "TYPE" : Me.lbl_TypeDalle.Text = myBloc(CLE)
            CLE = "THICKNESS" : Me.lbl_Epaisseur.Text = myBloc(CLE)
            CLE = "THICKNESS" : Me.lbl_EpaisseurM.Text = myBloc(CLE)
            CLE = "HAUNCH" : Me.lbl_Renformis.Text = myBloc(CLE)
            CLE = "PRESLAB" : Me.lbl_EpPreDalle.Text = myBloc(CLE)
            CLE = "JOINT" : Me.lbl_EpJoint.Text = myBloc(CLE)

            '=== BETON ========================================================================

            CLE = "CONCRETE" : Me.lbl_Beton.Text = myBloc(CLE)

            CLE = "CLASS" : Me.lbl_ClasseE.Text = myBloc(CLE)
            CLE = "LIGHTCONCRETE" : Me.chk_BetonLeger.Text = myBloc(CLE)

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
        End Try

    End Sub

    Private Sub PreparerFenetre()

        RemplirComboAvecTableau(Me.cmb_TypeDalle, strType)
        RemplirComboClasseBeton()

        Const MARGEPAN As Integer = 0

        Me.pan_Type.Controls.Add(Me.pan_Epaisseur)
        Me.pan_Epaisseur.Left = 5
        Me.pan_Epaisseur.Top = 35

        Me.pan_Type.Controls.Add(Me.pan_EpaisseurMixte)
        Me.pan_EpaisseurMixte.Left = 5
        Me.pan_EpaisseurMixte.Top = Me.pan_Epaisseur.Top

        Me.pan_Type.Controls.Add(Me.pan_Renformis)
        Me.pan_Renformis.Left = 5
        Me.pan_Renformis.Top = Me.pan_Epaisseur.Top + Me.pan_Epaisseur.Height + MARGEPAN

        Me.pan_Type.Controls.Add(Me.pan_Predalle)
        Me.pan_Predalle.Left = 5
        Me.pan_Predalle.Top = Me.pan_Renformis.Top

        Select Case DefEpMixte
            Case Enu_DefEpMixte.Pleine
                Me.rdb_EpPleine.Checked = True
            Case Enu_DefEpMixte.Totale
                Me.rdb_EpTotale.Checked = True
        End Select

        MAJI_SaisieEpMixte()

    End Sub

    Private Sub RemplirComboClasseBeton()
        If Frm_DalleN.MyDalleLoc.beton.lLeger Then
            RemplirComboAvecTableau(Me.cmb_ClasseBetonDalle, ClasseBetonLeger)
        Else
            RemplirComboAvecTableau(Me.cmb_ClasseBetonDalle, ClasseBeton)
        End If
    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)

        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.Pan_Contenu.Dock = DockStyle.Fill

        Me.lbl_General.BackColor = CouleurBackBandeaux
        Me.lbl_General.ForeColor = CouleurForeBandeaux


        Me.lbl_Beton.BackColor = CouleurBackBandeaux
        Me.lbl_Beton.ForeColor = CouleurForeBandeaux

        Me.pan_Predalle.Top = Me.pan_Renformis.Top

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitDim8.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim9.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitRhoC.Text = "kg/m3"

    End Sub

    Private Sub AfficherDalleEnCours()

        '--> Type de dalle

        Select Case Frm_DalleN.MyDalleLoc.type
            Case cls_Dalle.Enum_TypeDalle.Pleine
                Me.cmb_TypeDalle.SelectedIndex = 0
            Case cls_Dalle.Enum_TypeDalle.Mixte
                Me.cmb_TypeDalle.SelectedIndex = 1
            Case cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
                Me.cmb_TypeDalle.SelectedIndex = 2
        End Select
        MAJI_TypeDalle()
        Frm_DalleN.MAJI_TauxArma()

        '--> Epaisseur

        Me.txt_Td2.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.Ep_td, Enu_TypeVariable.Dimension, True)
        Me.txt_Tc.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.Ep_td - Frm_DalleN.MyDalleLoc.Bac.Hp, Enu_TypeVariable.Dimension, True)

        Me.txt_Hd.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.Ep_td, Enu_TypeVariable.Dimension, True)
        Me.txt_Hh.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.Ep_th, Enu_TypeVariable.Dimension, True)

        Me.txt_EpPredalle.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.preDalle_ep, Enu_TypeVariable.Dimension, True)
        Me.txt_EpJoint.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.preDalle_tjoint, Enu_TypeVariable.Dimension, True)

        '--> Béton

        Dim Chaine As String
        Chaine = Frm_DalleN.MyDalleLoc.beton.Classe
        If Me.ClasseBeton.Contains(Chaine) And Not Frm_DalleN.MyDalleLoc.beton.lLeger Then
            Me.cmb_ClasseBetonDalle.SelectedIndex = Array.IndexOf(Me.ClasseBeton, Chaine)
        ElseIf Me.ClasseBetonLeger.Contains(Chaine) And Frm_DalleN.MyDalleLoc.beton.lLeger Then
            Me.cmb_ClasseBetonDalle.SelectedIndex = Array.IndexOf(Me.ClasseBetonLeger, Chaine)
        Else
            Me.cmb_ClasseBetonDalle.SelectedIndex = 0
        End If

        Me.txt_RhoC.Text = GetStringInUnit(Frm_DalleN.MyDalleLoc.beton.RhoC, Enu_TypeVariable.SansType, 3, 0, False, True)
        Me.chk_BetonLeger.Checked = Frm_DalleN.MyDalleLoc.beton.lLeger

        MAJI_ProprietesBeton()

    End Sub

#End Region

#Region " Evènements de saisie "

    Private Sub cmb_TypeDalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TypeDalle.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_TypeDalle.SelectedIndex
            Case 0 : Frm_DalleN.MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.Pleine
            Case 1 : Frm_DalleN.MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.Mixte
            Case 2 : Frm_DalleN.MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
        End Select

        MAJI_TypeDalle()
        Frm_DalleN.MAJI_TypeDalle()

        ' MAJ_ValeursLimites()

        ' AfficherLitEncours() 'Permet de relancer la vérification 

        Frm_DalleN.MAJI_ImageDalle()
    End Sub

#End Region

#Region " Autres évènements "

    Private Sub SaisieRdBDefEpMixte(sender As Object, e As EventArgs) Handles rdb_EpTotale.CheckedChanged, rdb_EpPleine.CheckedChanged
        If lBuild Then Exit Sub
        If Me.rdb_EpPleine.Checked Then
            DefEpMixte = Enu_DefEpMixte.Pleine
        Else
            DefEpMixte = Enu_DefEpMixte.Totale
        End If
        MAJI_SaisieEpMixte()
    End Sub

#End Region

#Region " Routines MAJI "


    Private Sub MAJI_ProprietesBeton()

        Frm_DalleN.MyDalleLoc.beton.Calcul_Proprietes(MyProjet.Poutres(MyProjet.IndEnCours).Param.lGeneration1)

    End Sub

    Private Sub MAJI_TypeDalle()
        '------------------------------------------------------------------------------------
        '   27/06/23 :  Création - POM
        '------------------------------------------------------------------------------------
        '   MAJ de l'interface en fonction du type de dalle
        '------------------------------------------------------------------------------------
        '------------------------------------------------------------------------------------

        Select Case Frm_DalleN.MyDalleLoc.type
            Case cls_Dalle.Enum_TypeDalle.Pleine
                ' Me.pan_Bac.Enabled = False

                Me.pan_Renformis.Visible = True
                Me.pan_Predalle.Visible = False
                Me.pan_EpaisseurMixte.Visible = False
                Me.pan_Epaisseur.Visible = True

                'Me.TLpan_PartageV.ColumnStyles(1).Width = 0
                'Me.TLPan_Dalle.ColumnStyles(0).Width = 255

            Case cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
                '  Me.pan_Bac.Enabled = False
                Me.pan_Predalle.Visible = True
                Me.pan_Renformis.Visible = False
                Me.pan_EpaisseurMixte.Visible = False
                Me.pan_Epaisseur.Visible = True

                'Me.TLpan_PartageV.ColumnStyles(1).Width = 0
                'Me.TLPan_Dalle.ColumnStyles(0).Width = 280

            Case cls_Dalle.Enum_TypeDalle.Mixte
                '  Me.pan_Bac.Enabled = True
                Me.pan_Predalle.Visible = False
                Me.pan_Renformis.Visible = False
                Me.pan_EpaisseurMixte.Visible = True
                Me.pan_Epaisseur.Visible = False

                'Me.TLPan_Dalle.ColumnStyles(0).Width = 501
                'Me.TLpan_PartageV.ColumnStyles(1).Width = 250
        End Select

    End Sub

      Private Sub MAJI_SaisieEpMixte()

        PrepareTextBoxDipo(Me.txt_Td2, DefEpMixte = Enu_DefEpMixte.Totale)
        PrepareTextBoxDipo(Me.txt_Tc, DefEpMixte = Enu_DefEpMixte.Pleine)

    End Sub

#End Region

End Class