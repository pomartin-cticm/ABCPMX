Imports PMXMoteur2
Imports System.IO

Public Class Frm_Identification


#Region " Variables locales "

    Dim lBuild As Boolean = True
    Dim sUser As String
    Dim sCompany As String
    Dim sProject As String
    Dim sBeamID As String
    Dim sComment As String

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Identification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        InitialisationVariablesLocales()
        AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_IDENTIFICATION")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== GENERAL ==============================================================='

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== MENU PRINCIPAL ==============================================================='

                Me.lbl_User.Text = Bloc("USER")
                Me.lbl_Company.Text = Bloc("COMPANY")
                Me.lbl_Project.Text = Bloc("PROJECT")
                Me.lbl_BeamID.Text = Bloc("BEAMID")
                Me.lbl_Comment.Text = Bloc("COMMENT")


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

        Me.lbl_Identification.BackColor = CouleurBackBandeaux
        Me.lbl_Identification.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub InitialisationVariablesLocales()
        With MyProjet

            sUser = .Utilisateur
            sCompany = .Entreprise
            sProject = .Projet
            sBeamID = .Poutres(MyProjet.IndEnCours).BeamID
            sComment = .Poutres(MyProjet.IndEnCours).Commentaire
        End With
    End Sub

    Private Sub AfficherPoutreEnCours()
        Me.txt_User.Text = sUser
        Me.txt_Company.Text = sCompany
        Me.txt_Project.Text = sProject
        Me.txt_BeamID.Text = sBeamID
        Me.txt_Comment.Text = sComment
    End Sub

#End Region

#Region "===FERMETURE==="

    Public Sub TraitementSaisie()

    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Dim lModif As Boolean = False
        If ValideSaisieFenetre() Then

            TransfertSaisie(lModif)

            If lModif Then
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If
            Me.Close()
        End If
    End Sub


    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)
        lModif = False

        With MyProjet

            If .Utilisateur <> sUser Then
                lModif = True
                .Utilisateur = sUser
            End If

            If .Entreprise <> sCompany Then
                lModif = True
                .Entreprise = sCompany
            End If

            If .Projet <> sProject Then
                lModif = True
                .Projet = sProject
            End If
        End With

        With MyProjet.Poutres(MyProjet.IndEnCours)
            If .BeamID <> sBeamID Then
                lModif = True
                .BeamID = sBeamID
            End If

            If .Commentaire <> sComment Then
                lModif = True
                .Commentaire = sComment
            End If
        End With

    End Sub

    Private Sub lbl_User_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtbox_TextChanged(sender As Object, e As EventArgs) Handles txt_User.TextChanged, txt_Company.TextChanged, txt_Project.TextChanged, txt_BeamID.TextChanged, txt_Comment.TextChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case txt_User.Name
                sUser = sender.text
            Case txt_Company.Name
                sCompany = sender.text
            Case txt_Project.Name
                sProject = sender.text
            Case txt_BeamID.Name
                sBeamID = sender.text
            Case txt_Comment.Name
                sComment = sender.text
        End Select
    End Sub

#End Region

#Region " Dessins "



#End Region

#Region " Evènements "


#End Region

#Region " Evènements saisie "


#End Region

End Class