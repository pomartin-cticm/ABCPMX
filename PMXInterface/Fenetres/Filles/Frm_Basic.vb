Imports PMXMoteur2
Imports System.IO

Public Class Frm_Basic

#Region " Variables locales "

    Dim lBuild As Boolean = True

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiserFenetre()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        AfficherPoutreEnCours()
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

    End Sub

    Private Sub AfficherPoutreEnCours()

    End Sub

#End Region

#Region "===FERMETURE==="

    Public Sub TraitementSaisie()

    End Sub

#End Region

#Region " Dessins "



#End Region

#Region " Evènements "


#End Region

#Region " Evènements saisie "


#End Region
End Class