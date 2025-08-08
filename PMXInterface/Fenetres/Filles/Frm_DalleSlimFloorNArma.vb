Imports PMXMoteur2

Public Class Frm_DalleSlimFloorNArma

#Region " Variables "

    Dim lBuild As Boolean

    Dim ClasseAcierArma() As String = cls_AcierArmature.tabClasseAcierArma

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(myBloc As Dictionary(Of String, String))

        lBuild = True
        'lInter = plInter

        GestionStyle()
        GestionLangues(myBloc)
        GestionUnites()
        'InitialiseVariablesLocales()

        'PrepareFenetre()

        RemplirComboDiametre()
        RemplirComboNombre()
        RemplirComboAvecTableau(Me.cmb_Acier, ClasseAcierArma)

        'MAJI_TypeDalle()

        AfficheDalleEnCours()

        lBuild = False

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitSigma2.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub GestionStyle()

        Me.pan_Main.Dock = DockStyle.Fill

        Me.lbl_Acier.BackColor = CouleurBackBandeaux
        Me.lbl_Acier.ForeColor = CouleurForeBandeaux

        Me.lbl_Arma.BackColor = CouleurBackBandeaux
        Me.lbl_Arma.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub GestionLangues(myBloc As Dictionary(Of String, String))

        Dim strLoadedKey As String = ""
        Dim CLE As String = ""

        Try

            '** ACIER

            CLE = "REBARSTEEL" : Me.lbl_Acier.Text = myBloc(CLE)
            CLE = "CLASS" : Me.lbl_ClasseA.Text = myBloc(CLE)

            '** Armatures

            CLE = "FIREREBARSTITLE" : Me.lbl_Arma.Text = myBloc(CLE)
            CLE = "FIREREBARS" : Me.chk_Rebars.Text = myBloc(CLE)
            CLE = "FIREREB_NUMBER" : Me.lbl_Number.Text = myBloc(CLE)
            CLE = "FIREREB_DIAMETER" : Me.lbl_Diameter.Text = myBloc(CLE)
            CLE = "FIREREB_LOCATION" : Me.lbl_Location.Text = myBloc(CLE)

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
        End Try


    End Sub

    Private Sub AfficheDalleEnCours()

        ' ** Armatures **

        With Frm_DalleSlimFloorN.localDalle.ArmaSlimFeu

            Me.chk_Rebars.Checked = .lBarre
            Me.cmb_Nombre.SelectedIndex = .NbBarres - 1

            Me.cmb_Diametre.SelectedIndex = Array.IndexOf(Cls_Armatures_Longi.TabDiametres, .Diametre)

            Me.txt_ArmaX.Text = GetStringNoUnit(.xPos, Enu_TypeVariable.Dimension, True)
            Me.txt_ArmaY.Text = GetStringNoUnit(.zPos, Enu_TypeVariable.Dimension, True)

        End With

        '--> Acier

        Dim Chaine As String
        Chaine = Frm_DalleSlimFloorN.localDalle.AcierArmatures.Classe
        If Me.ClasseAcierArma.Contains(Chaine) Then
            Me.cmb_Acier.SelectedIndex = Array.IndexOf(Me.ClasseAcierArma, Chaine)
        Else
            Me.cmb_Acier.SelectedIndex = 0
        End If
        MAJI_ProprietesAcier()

    End Sub

    Private Sub RemplirComboDiametre()

        Me.cmb_Diametre.Items.Clear()

        For i As Integer = 0 To Cls_Armatures_Longi.TabDiametres.Count - 1
            Me.cmb_Diametre.Items.Add(GetStringInUnitN(Cls_Armatures_Longi.TabDiametres(i), Enu_TypeVariable.Dimension, 4, 3, True, True))
        Next

        Me.cmb_Diametre.SelectedItem = 0

    End Sub

    Private Sub RemplirComboNombre()

        Me.cmb_Nombre.Items.Clear()
        For i As Integer = 1 To 2
            Me.cmb_Nombre.Items.Add(CStr(i))
        Next
    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)

        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)

    End Sub

    Private Sub MAJI_ProprietesAcier()
        Frm_DalleSlimFloorN.localDalle.AcierArmatures.MAJProprietes()
        Me.txt_Fsk.Text = GetStringNoUnit(Frm_DalleSlimFloorN.localDalle.AcierArmatures.FsK, Enu_TypeVariable.Contrainte)
    End Sub


#End Region

#Region " Evenements saisie "

    Private Sub chk_Rebars_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Rebars.CheckedChanged
        If lBuild Then Exit Sub

        Frm_DalleSlimFloorN.localDalle.ArmaSlimFeu.lBarre = Me.chk_Rebars.Checked

        Frm_DalleSlimFloorN.RedessineDalle()

    End Sub

    Private Sub cmb_Nombre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Nombre.SelectedIndexChanged
        If lBuild Then Exit Sub

        Frm_DalleSlimFloorN.localDalle.ArmaSlimFeu.NbBarres = Me.cmb_Nombre.SelectedIndex + 1

        Frm_DalleSlimFloorN.RedessineDalle()

    End Sub

    Private Sub cmb_Diametre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Diametre.SelectedIndexChanged

        If lBuild Then Exit Sub

        Frm_DalleSlimFloorN.localDalle.ArmaSlimFeu.Diametre = Cls_Armatures_Longi.TabDiametres(Me.cmb_Diametre.SelectedIndex)

        Frm_DalleSlimFloorN.RedessineDalle()

    End Sub

    Private Sub cmb_Acier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Acier.SelectedIndexChanged

        If lBuild Then Exit Sub

        Frm_DalleSlimFloorN.localDalle.AcierArmatures.Classe = cls_AcierArmature.tabClasseAcierArma(Me.cmb_Acier.SelectedIndex)

        ' Frm_DalleSlimFloorN.RedessineDalle()


    End Sub

#End Region

End Class