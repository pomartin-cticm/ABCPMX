Imports PMXMoteur2

Public Class Frm_PPModePropre

#Region " Variables "

    Dim lBuild As Boolean

    Dim lDefinieQ(2) As Boolean
    Dim IndiceQ As New List(Of Integer)
    Dim LabelQ() As String = {"Q1", "Q2"}

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_PPModePropre_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionLangues()
        GestionStyle()

        PrepareFenetre(MyProjet.Poutres(MyProjet.IndEnCours))
        AfficheCalculModal(MyProjet.Poutres(MyProjet.IndEnCours))

        lBuild = False
    End Sub

    Private Sub GestionLangues()

        Me.Text = "Mode propre"
        Me.lbl_ModePropre.Text = "Eigen mode values"

        Me.lbl_Masses.Text = "Masses"
        Me.lbl_Frequence.Text = "Frequency"
        Me.lbl_Periode.Text = "Period"
        Me.lbl_Resultats.Text = "Results"

        Me.btn_OK.Text = "Close"

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_ModePropre.BackColor = CouleurBackBandeaux
        Me.lbl_ModePropre.ForeColor = CouleurForeBandeaux

        Me.img_ModePropre.Dock = DockStyle.Fill

        Me.TLPan_PartieBasse.ColumnStyles(1).Width = 0
        Me.TLPan_PartieBasse.ColumnStyles(2).Width = 0

    End Sub

    Private Sub PrepareFenetre(MyPoutre As cls_poutre)

        lDefinieQ(1) = MyPoutre.ChargesU("Q1").EstDefinie
        lDefinieQ(2) = MyPoutre.ChargesU("Q2").EstDefinie
        lDefinieQ(0) = lDefinieQ(1) Or lDefinieQ(2)

        Me.cmb_Q.Visible = lDefinieQ(0)
        Me.cmb_RatioQ.Visible = lDefinieQ(0)

        If lDefinieQ(0) Then
            RemplirCmbQ()
            RemplirCmbRatioQ()
        End If

    End Sub

    Private Sub RemplirCmbRatioQ()
        Me.cmb_RatioQ.Items.Clear()
        For i As Integer = 0 To 9
            Me.cmb_RatioQ.Items.Add(Format(i / 10, "0.0"))
        Next
        Me.cmb_RatioQ.SelectedIndex = 2
    End Sub

    Private Sub RemplirCmbQ()
        Me.cmb_Q.Items.Clear()
        Me.IndiceQ.Clear()

        For i As Integer = 1 To 2
            If lDefinieQ(i) Then
                Me.cmb_Q.Items.Add(LabelQ(i - 1))
                IndiceQ.Add(i)
            End If
        Next

        If Me.cmb_Q.Items.Count > 0 Then _
        Me.cmb_Q.SelectedIndex = 0

    End Sub

#End Region

#Region "===Fermeture==="

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub

#End Region

#Region " Calculs et affichage des résultats "

    Private Sub AfficheCalculModal(MyPoutre As cls_Poutre)

        '--> Déclarations

        Dim RatioQ As Decimal
        Dim IndexQ As Integer

        '--> Définitions des paramètres de calcul

        If lDefinieQ(0) Then
            RatioQ = Me.cmb_RatioQ.SelectedIndex / 10
            IndexQ = IndiceQ(Me.cmb_Q.SelectedIndex)
        Else
            RatioQ = 0
            IndexQ = -1
        End If

        '--> Analyse modale

        MyPoutre.Modal.Analyse(MyPoutre, RatioQ, IndexQ)

        '--> Affichage des résultats

        If MyPoutre.Modal.ErrorCode = 0 Then
            Me.txt_Frequence.Text = GetStringInUnit(MyPoutre.Modal.Frequence, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_Periode.Text = GetStringInUnit(MyPoutre.Modal.Periode, Enu_TypeVariable.SansType, 3, 2, False)
            Me.Rtxt_Error.Visible = False
        Else
            Me.txt_Frequence.Text = "-"
            Me.txt_Periode.Text = "-"
            Me.Rtxt_Error.Visible = True
            Me.Rtxt_Error.Text = MyPoutre.Modal.ErrorMsg
        End If
    End Sub

#End Region

#Region " Evènements "


#End Region

End Class