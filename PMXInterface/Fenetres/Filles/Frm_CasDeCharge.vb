Public Class Frm_CasDeCharge


#Region " Variables "

    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_CasDeCharge_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionLangues()
        GestionStyle()
        GestionUnites()

        AfficheCasdeCharge()

        lBuild = False
    End Sub

    Private Sub GestionLangues()

        Me.Text = "Load cases"
        Me.lbl_CasDeCharges.Text = "Load cases"
        Me.btn_Annuler.Text = "Cancel"
        Me.btn_OK.Text = "Close"

        Me.lbl_Case.Text = "Case"
        Me.lbl_Etat.Text = "Etat"
        Me.lbl_RunCalcul.Text = "Calcul effectué ?"

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitDim1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitForce1.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)
        Me.etq_UnitForce2.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)
    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_CasDeCharges.BackColor = CouleurBackBandeaux
        Me.lbl_CasDeCharges.ForeColor = CouleurForeBandeaux

        Me.img_Analyse.Dock = DockStyle.Fill

    End Sub

    Private Sub AfficheCasdeCharge()

        InitialiseOptionsCalculPoutre(MyProjet.Poutres(MyProjet.IndEnCours))
        MyProjet.Poutres(MyProjet.IndEnCours).InitialiseCalculs()
        MyProjet.Poutres(MyProjet.IndEnCours).CalculMNVInternes()

        Me.cmb_Symbols.Items.Clear()

        For i As Integer = 0 To MyProjet.Poutres(MyProjet.IndEnCours).ChargesA.Count - 1
            Me.cmb_Symbols.Items.Add(MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i).Symbol)
        Next

        Me.cmb_Symbols.SelectedIndex = 0

        MAJI_CasdeCharge()

    End Sub

#End Region

#Region "===FERMETURE==="


    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub

#End Region

#Region " Evènements "

    Private Sub MAJI_CasdeCharge()

        Me.lbl_Name.Text = MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(Me.cmb_Symbols.SelectedIndex).Nom

        Dim IndexElts As Integer = MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(Me.cmb_Symbols.SelectedIndex).IndElts
        Dim lMixte As Boolean = MyProjet.Poutres(MyProjet.IndEnCours).Elements(IndexElts).lMixte
        Dim nEqDalle As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).Elements(IndexElts).nEqC
        Dim nEqEnrob As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).Elements(IndexElts).nEqEC

        If lMixte Then
            Me.lbl_Mixte.Text = "mixte"
            Me.lbl_NeqDalle.Visible = True
            Me.lbl_NeqDalle.Text = "Dalle : " & GetStringInUnit(nEqDalle, Enu_TypeVariable.SansType, 3, 3, False)
        Else
            Me.lbl_Mixte.Text = "non mixte"
            Me.lbl_NeqDalle.Visible = False
        End If

        If MyProjet.Poutres(MyProjet.IndEnCours).lEnrobage Then
            Me.lbl_NeqEnrob.Visible = True
            Me.lbl_NeqEnrob.Text = "Enrobage : " & GetStringInUnit(nEqEnrob, Enu_TypeVariable.SansType, 3, 3, False)
        Else
            Me.lbl_NeqEnrob.Visible = False
        End If

        Dim Chaine As String
        Dim lCalcul As Boolean = MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(Me.cmb_Symbols.SelectedIndex).lRunCalcul

        If lCalcul Then Chaine = "Oui" Else Chaine = "Non"
        Me.lbl_RCalcul.Text = Chaine

        PrepareFenetreResults(lCalcul)
        If lCalcul Then

            With MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(Me.cmb_Symbols.SelectedIndex)
                Me.txt_RZ1.Text = GetStringInUnit(.RZ(0), Enu_TypeVariable.Effort, 3, 3, False)
                Me.txt_RZ2.Text = GetStringInUnit(.RZ(1), Enu_TypeVariable.Effort, 3, 3, False)
                Me.txt_Fleche.Text = GetStringInUnit(.FlecheMax, Enu_TypeVariable.Dimension, 3, 3, False)
            End With

        End If
    End Sub

    Private Sub PrepareFenetreResults(lDispo As Boolean)

        Me.lbl_RZ1.Visible = lDispo
        Me.lbl_RZ2.Visible = lDispo
        Me.txt_RZ1.Visible = lDispo
        Me.txt_RZ2.Visible = lDispo
        Me.etq_UnitForce1.Visible = lDispo
        Me.etq_UnitForce2.Visible = lDispo
        Me.etq_UnitDim1.Visible = lDispo

        Me.lbl_Fleche.Visible = lDispo
        Me.txt_Fleche.Visible = lDispo

    End Sub

    Private Sub cmb_Symbols_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Symbols.SelectedIndexChanged
        MAJI_CasdeCharge()
    End Sub


#End Region

#Region " Dessins "



#End Region


End Class