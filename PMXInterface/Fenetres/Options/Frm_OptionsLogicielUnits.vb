Public Class Frm_OptionsLogicielUnits


#Region " Variables et constantes "

    Const BALISE As String = "OPTSOFTUNITS"

    Const formatGAMMA As String = "0.00"
    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFrm()
        lBuild = True
        GestionLangue(Frm_OptionsLogiciel.BlocLangues(BALISE))
        GestionStyle()
        'GestionUnites()
        IntialiseObjets()
        AfficherOptionsEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Units.Text = MyBloc("UNITS")

            Me.lbl_Dimensions.Text = MyBloc("DIMENSIONS")
            Me.lbl_Longueur.Text = MyBloc("LENGTHS")

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_Units.Dock = DockStyle.Fill

        Me.lbl_Units.BackColor = CouleurBackBandeaux
        Me.lbl_Units.ForeColor = CouleurForeBandeaux


    End Sub

    Private Sub IntialiseObjets()
        RemplirComboAvecTableau(Me.cmb_Dimensions, LogicielInfo.Unit_Longueur)
        RemplirComboAvecTableau(Me.cmb_Longueur, LogicielInfo.Unit_Longueur)
    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)
        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)
    End Sub


    Private Sub AfficherOptionsEnCours()


    End Sub

#End Region

End Class