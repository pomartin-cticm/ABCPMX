Public Class Frm_OptionsCalculCalcul


#Region " Variables locales "


    Const BALISE As String = "OPTCALCALCUL"

    Const formatGAMMA As String = "0.00"
    Dim lBuild As Boolean

    Dim tabNorme(1) As String

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFrm()
        lBuild = True
        GestionLangue(Frm_OptionsCalcul.BlocLangues(BALISE))
        GestionStyle()
        GestionUnites()
        RemplirCombos()
        AfficherScopeEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Calcul.Text = MyBloc("TITLE")

            Me.lbl_Norme.Text = MyBloc("STANDARD")
            tabNorme(0) = MyBloc("ENGEN1")
            tabNorme(1) = MyBloc("ENGEN2")

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_Calcul.Dock = DockStyle.Fill

        Me.lbl_Calcul.BackColor = CouleurBackBandeaux
        Me.lbl_Calcul.ForeColor = CouleurForeBandeaux

        If Not LogicielOptions.lExpert Then
            Me.txt_PorteeMini.Enabled = False
            Me.txt_PorteeMini.BackColor = CouleurReadOnly
        Else

        End If

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitL1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

    End Sub

    Private Sub AfficherScopeEnCours()

        '--> Norme

        Select Case LocalOptionsCalcul.Norme
            Case Enu_Normes.Eurocodes_G1 : Me.cmb_Norme.SelectedIndex = 0
            Case Enu_Normes.Eurocodes_G2 : Me.cmb_Norme.SelectedIndex = 1
        End Select

        '--> Portée

        Me.txt_PorteeMini.Text = GetStringInUnit(LocalOptionsScope.PorteeMin, Enu_TypeVariable.Longueur, 4, 2, False)





    End Sub

    Private Sub RemplirCombos()
        Me.cmb_Norme.Items.Clear()
        Me.cmb_Norme.Items.AddRange(tabNorme)
        Me.cmb_Norme.SelectedIndex = 0
    End Sub



#End Region

#Region " Evènements "

    Private Sub cmb_Norme_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Norme.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_Norme.SelectedIndex
            Case 0 : LocalOptionsCalcul.Norme = Enu_Normes.Eurocodes_G1
            Case 1 : LocalOptionsCalcul.Norme = Enu_Normes.Eurocodes_G2
        End Select

    End Sub

#End Region

End Class