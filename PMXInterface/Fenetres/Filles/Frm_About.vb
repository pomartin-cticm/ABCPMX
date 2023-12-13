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

                Me.lbl_Information.Text = Bloc("INFORMATION")
                Me.lbl_Description.Text = Bloc("DESCRIPTION")
                Me.lbl_DescriptionContain.Text = Bloc("DESCRIPTION_CONT")
                Me.lbl_Maitre.Text = Bloc("MAITRE")

                Me.lbl_Version.Text = Bloc("VERSION") & " " & LogicielInfo.Version
                Me.lbl_Year.Text = Bloc("YEARVERSION") & " " & LogicielInfo.AnneeVersion
                Me.lbl_Support.Text = Bloc("SUPPORT") & " " & LogicielInfo.MailSupport

                Me.lbl_Copyrights.Text = "CTICM" & Chr(10) &
                                         "Espace Technologique" & Chr(10) &
                                         "L'Orme des Merisiers" & Chr(10) &
                                         "Immeuble Apollo" & Chr(10) &
                                         "91190 SAINT-AUBIN" & Chr(10) &
                                         "FRANCE" & Chr(10) &
                                         "Tel. +33 1 60 13 83 00" & Chr(10) & Chr(10) &
                                         "https://www.cticm.com/"



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

        Me.lbl_Description.BackColor = CouleurBackBandeaux
        Me.lbl_Description.ForeColor = CouleurForeBandeaux

        Me.lbl_Information.BackColor = CouleurBackBandeaux
        Me.lbl_Information.ForeColor = CouleurForeBandeaux

        Me.lbl_Maitre.BackColor = CouleurBackBandeaux
        Me.lbl_Maitre.ForeColor = CouleurForeBandeaux


        Me.img_CTICM.Visible = lCTICM
        Me.img_AM.Visible = Not lCTICM

        If lCTICM Then
            Me.img_AM.Dock = DockStyle.None
            Me.img_CTICM.Dock = DockStyle.Fill
        Else
            Me.img_CTICM.Dock = DockStyle.None
            Me.img_AM.Dock = DockStyle.Fill

        End If

    End Sub

    'Private Sub AfficherPoutreEnCours()

    'End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub



#End Region

End Class