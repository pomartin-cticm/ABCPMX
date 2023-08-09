Public Class Frm_OptionsLogicielExpert

#Region " Variables locales "

    Const BALISE As String = "OPTSOFTEXPERT"
    Dim strNotValide As String

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
        MAJ_Fenetre_Expert()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_ExpertMode.Text = MyBloc("EXPERTMODE")

            Me.lbl_Activation.Text = MyBloc("ACTIVATEEXP")
            Me.lbl_Key.Text = MyBloc("KEY")

            Me.btn_Activer.Text = MyBloc("ACTIVATE")
            Me.btn_Desactiver.Text = MyBloc("DESACTIVATE")

            strNotValide = MyBloc("UNVALIDKEY")

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_Expert.Dock = DockStyle.Fill

        Me.lbl_ExpertMode.BackColor = CouleurBackBandeaux
        Me.lbl_ExpertMode.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub IntialiseObjets()



    End Sub

    Private Sub AfficherOptionsEnCours()


    End Sub



#End Region

#Region " Evènements "


    Private Sub btn_Activate_Click(sender As Object, e As EventArgs) Handles btn_Activer.Click
        '======== ACTIVATION ===================================================================

        TraitementSaisie()

    End Sub

    Private Sub Txt_Expert_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_Expert.KeyDown
        '======== ACTIVATION ===================================================================

        If e.KeyCode = Keys.Enter Then '--> Press Entrée - Validation de la saisie
            TraitementSaisie()
        End If

    End Sub

    Private Sub btn_Desactivate_Click(sender As Object, e As EventArgs) Handles btn_Desactiver.Click
        '======== DESACTIVATION ===================================================================

        Me.txt_Expert.Clear()
        Frm_OptionsLogiciel.pLocalLogicielOptions.lExpert = False
        Frm_OptionsLogiciel.MAJIExpert()
        MAJ_Fenetre_Expert()

    End Sub


    Private Sub TraitementSaisie()
        '--> Traitement du texte saisie par l'utilisateur

        Dim Mots() As String = Nothing
        Dim nMots As Integer

        DecomposeLine(Me.txt_Expert.Text, SEPARATEURS, Mots, nMots)

        If nMots >= 2 Then
            Dim n1, n2 As Integer
            n1 = Math.Min(4, Mots(1).Length)
            n2 = Math.Min(4, Mots(2).Length)
            If Mots(1).ToUpper.Substring(0, n1) = "EXPE" Then
                If Mots(2).ToUpper.Substring(0, n2) = "PMX" Then
                    Frm_OptionsLogiciel.pLocalLogicielOptions.lExpert = True
                ElseIf Mots(2).ToUpper.Substring(0, n2) = "NO" Then
                    Frm_OptionsLogiciel.pLocalLogicielOptions.lExpert = True
                    '--> Message d'erreur
                    MsgBox(strNotValide)
                End If
            End If
        Else
            Frm_OptionsLogiciel.pLocalLogicielOptions.lExpert = True = False
            '--> Message d'erreur
            MsgBox(strNotValide)
        End If

        '--> MAJ de l'interface
        MAJ_Fenetre_Expert()
        Frm_OptionsLogiciel.MAJIExpert()

    End Sub

    Private Sub MAJ_Fenetre_Expert()

        '--> Maj du bouton
        Me.btn_Desactiver.Enabled = Frm_OptionsLogiciel.pLocalLogicielOptions.lExpert

    End Sub

#End Region

End Class