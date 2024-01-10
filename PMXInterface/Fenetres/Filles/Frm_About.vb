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
        '  GestionUnites()
        ' AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_ABOUT")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== MENU PRINCIPAL ==============================================================='

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")




            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    'Private Sub GestionUnites()

    'End Sub

    Private Sub GestionStyle(ByVal lCTICM As Boolean)
        Me.Icon = Frm_PMX.Icon

        'Me.lbl_Description.BackColor = CouleurBackBandeaux
        'Me.lbl_Description.ForeColor = CouleurForeBandeaux

        'Me.lbl_Information.BackColor = CouleurBackBandeaux
        'Me.lbl_Information.ForeColor = CouleurForeBandeaux

        'Me.lbl_Maitre.BackColor = CouleurBackBandeaux
        'Me.lbl_Maitre.ForeColor = CouleurForeBandeaux


        'Me.img_CTICM.Visible = lCTICM
        'Me.img_AM.Visible = Not lCTICM

        'If lCTICM Then
        '    Me.img_AM.Dock = DockStyle.None
        '    Me.img_CTICM.Dock = DockStyle.Fill
        'Else
        '    Me.img_CTICM.Dock = DockStyle.None
        '    Me.img_AM.Dock = DockStyle.Fill

        'End If

    End Sub

    'Private Sub AfficherPoutreEnCours()

    'End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub

    Private Sub LinkLabel_SupportCTICM_Click(sender As Object, e As EventArgs) Handles Panel1.Click, LinkLabel_SupportCTICM.Click, LinkLabel_SupportAM.Click
        '--> Click sur le lien - Ouverture Mail
        Try

            PrepareMailSupport()

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



#End Region

End Class