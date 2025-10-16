Imports PMXMoteur2

Public Class Frm_DalleNArma

#Region " Variables locales "

    Dim lBuild As Boolean
    Dim strLitNo(1) As String
    Dim strTauxArma As String
    Dim strToolTipAddRebar As String
    Dim strToolTipRemoveRebar As String

    Dim FontFrm As Font

    Dim ClasseAcierArma() As String = cls_AcierArmature.tabClasseAcierArma

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(myBloc As Dictionary(Of String, String))
        lBuild = True

        GestionStyle()
        GestionLangues(myBloc)
        GestionUnites()
        PreparerFenetre()
        AfficherDalleEnCours()
        lBuild = False

    End Sub

    Private Sub PreparerFenetre()

        RemplirComboAvecTableau(Me.cmb_Acier, ClasseAcierArma)

    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)

        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)

    End Sub

    Private Sub GestionStyle()

        Me.Pan_Contenu.Dock = DockStyle.Fill

        FontFrm = New Font(FontBase.Name, SizeFontFrm)

        Me.lbl_Armatures.BackColor = CouleurBackBandeaux
        Me.lbl_Armatures.ForeColor = CouleurForeBandeaux
        Me.lbl_Acier.BackColor = CouleurBackBandeaux
        Me.lbl_Acier.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(txt_Fsk, False)

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim6.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim7.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitSigma2.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub GestionLangues(myBloc As Dictionary(Of String, String))

        Dim strLoadedKey As String = ""
        Dim CLE As String = ""

        Try

            '=== ARMATURES ====================================================================

            CLE = "REBARS" : Me.lbl_Armatures.Text = myBloc(CLE)
            CLE = "REINFLAYER0" : Me.ToolTipDalle.SetToolTip(Me.chk_Lit0, myBloc(CLE))
            CLE = "REINFLAYER1" : Me.ToolTipDalle.SetToolTip(Me.chk_Lit1, myBloc(CLE))
            CLE = "REINFLAYER2" : Me.ToolTipDalle.SetToolTip(Me.chk_Lit2, myBloc(CLE))
            CLE = "DIAMETER" : Me.lbl_Diametre.Text = myBloc(CLE)
            CLE = "SPACING" : Me.lbl_Espacement.Text = myBloc(CLE)
            CLE = "LOCATION" : Me.lbl_zs.Text = myBloc(CLE)

            CLE = "ADDLAYER" : strToolTipAddRebar = myBloc(CLE)
            CLE = "REMOVELAYER" : strToolTipRemoveRebar = myBloc(CLE)
            CLE = "FIRSTLAYER" : strLitNo(0) = myBloc(CLE)
            CLE = "SECONDLAYER" : strLitNo(1) = myBloc(CLE)

            CLE = "REINFRATIO" : strTauxArma = myBloc(CLE)

            CLE = "NOREINFORCEMENT" : Me.lbl_NoArma.Text = myBloc(CLE)                   'Pour une poutre sans console, il n'est pas nécessaire de définir les lits d'armature"

            '=== ACIER DES ARMATURES ==========================================================

            CLE = "REBARSTEEL" : Me.lbl_Acier.Text = myBloc(CLE)        '"Reinforcement steel"
            CLE = "CLASS" : Me.lbl_ClasseA.Text = myBloc(CLE)



        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
        End Try

    End Sub

    Private Sub AfficherDalleEnCours()


    End Sub

#End Region

#Region " Routines MAJI "

    Private Sub MAJI_ProprietesAcier()
        Frm_DalleN.MyDalleLoc.AcierArmatures.MAJProprietes()
        Me.txt_Fsk.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.AcierArmatures.FsK, Enu_TypeVariable.Contrainte)
    End Sub

#End Region

#Region "===FERMETURE==="



#End Region


End Class