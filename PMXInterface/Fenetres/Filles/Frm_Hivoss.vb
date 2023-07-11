Imports PMXMoteur2
Imports System.IO

Public Class Frm_Hivoss

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim MyPoutreLoc As cls_Poutre

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
        AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub InitialiserVariables()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_BASIC")
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

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click

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