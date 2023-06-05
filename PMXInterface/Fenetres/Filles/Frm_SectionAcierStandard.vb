Imports System.IO
Imports PMXMoteur2

Public Class Frm_SectionAcierStandard

#Region " Variables "
    Dim lBuild As Boolean = True
#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_SectionAcierStandard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub


    Public Sub InitialiserFenetre()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_SECTIONACIERSTANDARD")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== MENU PRINCIPAL ==============================================================='



            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_Section.BackColor = CouleurBackBandeaux
        Me.lbl_Section.ForeColor = CouleurForeBandeaux

        Me.lbl_Acier.BackColor = CouleurBackBandeaux
        Me.lbl_Acier.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub AfficherPoutreEnCours()

    End Sub

#End Region

#Region "===FERMETURE==="

    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        Me.Close()
    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        If ValideSaisieFenetre() Then

            Dim lModif As Boolean = False

            TransfertSaisie(lModif)

            If lModif Then

            End If

            Me.Close()
        End If
    End Sub

    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

    End Sub

#End Region

End Class