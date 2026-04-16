Imports System.IO
Imports PMXMoteur2

Public Class Frm_CalculEnCours

#Region " Attributs "
    Dim strProgressionEtape As String
#End Region

#Region " Ouverture "

    Private Sub Frm_CalculEnCours_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GestionLangue()
        GestionStyle()
    End Sub

    Private Sub GestionLangue()

        If File.Exists(LogicielFichiers.Langue) Then

            Dim strLoadedKey As String = ""
            Dim CLE As String = ""

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_CALCULENCOURS")
            BlocLine.CreationBloc(Bloc, strLoadedKey)

            Try

                CLE = "TITLE" : Text = Bloc(CLE)
                CLE = "TITLELABL" : LBL_Titre.Text = Bloc(CLE)
                CLE = "PROGRESTEP" : strProgressionEtape = Bloc(CLE)
                CLE = "PROGREINSTEP" : LBL_ProgressionDansEtape.Text = Bloc(CLE)

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
            Finally
                Bloc.Clear()
            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If
    End Sub

    Private Sub GestionStyle()

        Icon = Frm_PMX.Icon

        LBL_Titre.BackColor = CouleurBackBandeaux
        LBL_Titre.ForeColor = CouleurForeBandeaux

    End Sub

#End Region

#Region " Evenements "

    Public Sub UpdateGlobal(valeurs As Struc_MAJEtape)
        PGB_Etape.Value = valeurs.ValeurProgression
        LBL_ProgressionEtape.Text = strProgressionEtape & " " & valeurs.ValeurEtape.ToString & " minutes..." 'fonctionne pour l'anglais et le français
    End Sub

    Public Sub UpdateStep(value As Integer)
        PGB_DansEtape.Value = value
    End Sub


#End Region

End Class