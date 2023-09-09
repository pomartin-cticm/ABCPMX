Public Class Frm_CasDeCharge


#Region " Variables "

    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_CasDeCharge_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionLangues()
        GestionStyle()

        AfficheCasdeCharge()

        lBuild = False
    End Sub

    Private Sub GestionLangues()

        Me.Text = "Load cases"
        Me.lbl_CasDeCharges.Text = "Load cases"
        Me.btn_Annuler.Text = "Cancel"
        Me.btn_OK.Text = "Close"

        Me.lbl_Case.Text = "Case"

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

    End Sub

    Private Sub cmb_Symbols_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Symbols.SelectedIndexChanged
        MAJI_CasdeCharge()
    End Sub


#End Region

#Region " Dessins "



#End Region


End Class