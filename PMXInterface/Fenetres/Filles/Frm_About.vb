Imports PMXMoteur2
Imports System.IO

Public Class Frm_About

#Region " Variables locales "

    Dim lBuild As Boolean = True
    Dim lCTICM As Boolean

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_About_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True
        lCTICM = (LogicielInfo.Maitre = EnuMaitre.CTICM)
        GestionLangues()
        GestionStyle(lCTICM)
        AfficherInfoEnCours()
        AfficheNomLogiciel()
        lBuild = False
    End Sub

    Private Sub GestionLangues()

        'Dim myMsg As String

        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_ABOUT")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== MENU PRINCIPAL ==============================================================='

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")

            Catch ex As Exception

                GestionErreurAffichageLangue(Me.Name, "GestionLangues")

                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If

    End Sub

    'Private Sub GestionUnites()

    'End Sub

    Private Sub GestionStyle(ByVal lCTICM As Boolean)
        Me.Icon = Frm_PMX.Icon

        Dim xLeft As Integer

        xLeft = (Me.pan_Entete.Width - Me.pan_ImageEntete.Width) / 2

        Me.pan_ImageEntete.Left = xLeft

        Me.lbl_Verification.BackColor = CouleurBackBandeaux
        Me.lbl_Verification.ForeColor = CouleurForeBandeaux

        If lCTICM Then

            Me.txt_cticm.ForeColor = SystemColors.WindowText
            Me.txt_Arcelor.ForeColor = SystemColors.GrayText

        Else

            Me.txt_Arcelor.ForeColor = SystemColors.WindowText
            Me.txt_cticm.ForeColor = SystemColors.GrayText

        End If

    End Sub

    Private Sub AfficherInfoEnCours()
        Me.lbk_SupportAM.Text = EMAIL_ARCELORMITTAL
        Me.lbk_SupportCTICM.Text = EMAIL_CTICM
    End Sub

    Private Sub AfficheNomLogiciel()

        Dim Chaine As String = CStr(LogicielInfo.Version.Annee)
        If lCTICM Then
            Chaine = " (@ CTICM-ArcelorMittal " & Chaine & ")"
        Else
            Chaine = " (@ ArcelorMittal-CTICM " & Chaine & ")"
        End If

        Me.lbl_NomLogiciel.Text = LogicielInfo.Racine & " V" & LogicielInfo.Version.Label & Chaine
    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub

    Private Sub LinkLabel_SupportCTICM_Click(sender As Object, e As EventArgs) Handles pan_AffichageInfo.Click, lbk_SupportCTICM.Click, lbk_SupportAM.Click
        '--> Click sur le lien - Ouverture Mail

        Dim myDest As String = ""

        Select Case sender.name
            Case lbk_SupportCTICM.Name : myDest = EMAIL_CTICM
            Case lbk_SupportAM.Name : myDest = EMAIL_ARCELORMITTAL
        End Select

        Try

            PrepareMailSupport(myDest)

        Catch ex As Exception
            MsgBox("Erreur d'ouverture mail | Error opening mail", MsgBoxStyle.Critical, "Frm_About/LinkSupport_LinkClicked")
        End Try
    End Sub

    Private Sub LinkLabel_SiteAM_Click(sender As Object, e As EventArgs) Handles LinkLabel_SiteCTICM.Click, LinkLabel_SiteAM.Click

        '--> Click sur le lien - Ouverture page web
        Try
            Process.Start(sender.text)
        Catch ex As Exception
            MsgBox("Erreur d'ouverture du lien | Error opening link", MsgBoxStyle.Critical, "Frm_About/LinkLabel_LinkClicked")
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GestionErrorsPMX("", "", "Gestion des erreurs", True)
    End Sub

#End Region

End Class