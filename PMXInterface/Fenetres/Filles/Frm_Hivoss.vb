Imports PMXMoteur2
Imports System.IO

Public Class Frm_Hivoss

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim MyPoutreLoc As cls_Poutre

    Dim strRatioQ() As String
    Dim strChoixQ() As String
    Dim strUtilisationPlancher() As String

#End Region

#Region "===OUVERTURE==="
    Private Sub Frm_Hivoss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub


    Public Sub InitialiserFenetre()
        InitialiserVariables()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        RemplirComboBox()
        AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub InitialiserVariables()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_HIVOSS")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== GENERAL ======================================================================

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== MENU PRINCIPAL ==============================================================='

                ReDim strRatioQ(5)

                strRatioQ(0) = "G"
                strRatioQ(1) = "G + 0.1 Q"
                strRatioQ(2) = "G + 0.2 Q"
                strRatioQ(3) = "G + 0.3 Q"
                strRatioQ(4) = "G + 0.4 Q"
                strRatioQ(5) = "G + 0.5 Q"

                ReDim strChoixQ(1)

                strChoixQ(0) = "Q1"
                strChoixQ(1) = "Q2"

                ReDim strUtilisationPlancher(9)

                strUtilisationPlancher(0) = Bloc("FLOORUSE_CRITAREA")
                strUtilisationPlancher(1) = Bloc("FLOORUSE_HOSP")
                strUtilisationPlancher(2) = Bloc("FLOORUSE_SCHOOL")
                strUtilisationPlancher(3) = Bloc("FLOORUSE_RESIDENTIAL")
                strUtilisationPlancher(4) = Bloc("FLOORUSE_OFFICE")
                strUtilisationPlancher(5) = Bloc("FLOORUSE_MEETING")
                strUtilisationPlancher(6) = Bloc("FLOORUSE_SENIOR")
                strUtilisationPlancher(7) = Bloc("FLOORUSE_HOTELS")
                strUtilisationPlancher(8) = Bloc("FLOORUSE_INDUSTRIAL")
                strUtilisationPlancher(9) = Bloc("FLOORUSE_SPORTS")


            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub RemplirComboBox()
        Me.cmb_ratioQ.Items.Clear()
        Me.cmb_ratioQ.Items.AddRange(strRatioQ)
        Me.cmb_ratioQ.SelectedIndex = 0

        Me.cmb_choixQ.Items.Clear()
        Me.cmb_choixQ.Items.AddRange(strChoixQ)
        Me.cmb_choixQ.SelectedIndex = 0

        Me.cmb_UtilisationPlancher.Items.Clear()
        Me.cmb_UtilisationPlancher.Items.AddRange(strUtilisationPlancher)
        Me.cmb_UtilisationPlancher.SelectedIndex = 0
    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_Options.BackColor = CouleurBackBandeaux
        Me.lbl_Options.ForeColor = CouleurForeBandeaux
        Me.lbl_Amortissement.BackColor = CouleurBackBandeaux
        Me.lbl_Amortissement.ForeColor = CouleurForeBandeaux
    End Sub

    Private Sub AfficherPoutreEnCours()

    End Sub

#End Region

#Region "===FERMETURE==="

    Public Sub TraitementSaisie()

    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click

    End Sub


    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

    End Sub

    Private Sub Frm_Basic_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles img_DtotSymbol.Click

    End Sub

    Private Sub pan_SaisieAmortissement_Paint(sender As Object, e As PaintEventArgs) Handles pan_SaisieAmortissement.Paint

    End Sub

#End Region

#Region " Dessins "



#End Region

#Region " Evènements "


#End Region

#Region " Evènements saisie "


#End Region

End Class