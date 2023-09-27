Imports PMXMoteur2

Public Class Frm_PPCombinaison

#Region " Variables "

    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="


    Private Sub Frm_PPCombinaison_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionStyle()
        GestionLangues()
        InitialiseFenetre()

        lBuild = False
    End Sub

    Private Sub InitialiseFenetre()

        MyProjet.Poutres(MyProjet.IndEnCours).InitialiseCalculs()
        MyProjet.Poutres(MyProjet.IndEnCours).CalculMNVInternes()
        MyProjet.Poutres(MyProjet.IndEnCours).InitialiseCombiA_ELU()

        RemplirComboCombi()

    End Sub

    Private Sub RemplirComboCombi()

        Me.cmb_Combi.Items.Clear()

        For i As Integer = 0 To MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELU.nbCombi - 1

            Me.cmb_Combi.Items.Add(MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELU.Symbole(i))

        Next

        Me.cmb_Combi.SelectedIndex = 0

        MAJI_Combinaison()

    End Sub

    Private Sub GestionLangues()

        Me.lbl_Combi.Text = "Combinaisons"

        Me.lbl_SymbCombi.Text = "Combi"

        Me.btn_Annuler.Text = "Close"
        Me.btn_OK.Text = "OK"

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_Combi.BackColor = CouleurBackBandeaux
        Me.lbl_Combi.ForeColor = CouleurForeBandeaux

        Me.img_Analyse.Dock = DockStyle.Fill

    End Sub

#End Region

#Region " Evènements "

    Private Sub MAJI_Combinaison()

        Dim Indice As Integer = Me.cmb_Combi.SelectedIndex

        Dim Chaine As String = ""
        Dim lFirst As Boolean = True
        Dim nbCharges As Integer = MyProjet.Poutres(MyProjet.IndEnCours).ChargesA.Count

        With MyProjet.Poutres(MyProjet.IndEnCours)
            For i As Integer = 0 To nbCharges - 1

                If Not IsEqual(.CombiA_ELU.CoefCombi(Indice)(i), 0) Then

                    If lFirst Then
                        Chaine = "= "
                        lFirst = False
                    Else
                        Chaine = Chaine & " + "
                    End If

                    Chaine = Chaine & GetStringInUnit(.CombiA_ELU.CoefCombi(Indice)(i), Enu_TypeVariable.SansType, 3, 2, False) & " " & .ChargesA(i).Symbol
                End If

            Next
        End With

        Me.lbl_CombiSelect.Text = Chaine
    End Sub

    Private Sub cmb_Combi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Combi.SelectedIndexChanged

        MAJI_Combinaison()

    End Sub

#End Region

End Class