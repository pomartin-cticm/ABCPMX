Imports PMXMoteur2
Imports System.IO

Public Class Frm_ErreursMessages

#Region " Variables "

    Public Source As String
    Public Message As String
    Public lError As Boolean = True

    Dim strOrigin As String
    Dim strError As String
    Dim strInfo As String


#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_ErreursMessages_Load(sender As Object, e As EventArgs) Handles Me.Load

        GestionLangues()
        GestionMessages()
        InitialiserFenetre()
        GestionStyle()

    End Sub

    Private Sub GestionLangues()

        If File.Exists(LogicielFichiers.Langue) Then

            Dim strLoadedKey As String = ""
            Const CLE As String = ""

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_ERRORS")
            BlocLine.CreationBloc(Bloc, strLoadedKey)

            Try

                '=== MENU PRINCIPAL ==============================================================='

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")

                Me.lbl_ContactSupport.Text = Bloc("CONTACT")

                strOrigin = Bloc("ORIGIN")
                strError = Bloc("ERROR")
                strInfo = Bloc("INFORMATION")

            Catch ex As Exception
                GestionLanguesBOOT()
                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try
        Else
            GestionLanguesBOOT()
        End If

    End Sub

    Private Sub GestionLanguesBOOT()

        Select Case LogicielInfo.Maitre

            Case EnuMaitre.CTICM

                Me.Text = "Notifications"
                Me.btn_OK.Text = "OK"

                Me.lbl_ContactSupport.Text = "Contactez le support logiciel : "

                strOrigin = "Origine"
                strError = "Erreur logiciel"
                strInfo = "Information"

            Case Else

                Me.Text = "Notifications"
                Me.btn_OK.Text = "OK"

                Me.lbl_ContactSupport.Text = "Contact software support: "

                strOrigin = "Origin"
                strError = "Software error"
                strInfo = "Information"

        End Select




    End Sub

    Private Sub GestionMessages()

        If Me.Source = "" Then
            Me.pan_Source.Visible = False
            Me.pan_Message.Top = Me.pan_Source.Top
        Else
            Me.pan_Source.Visible = True

            Me.lbl_Source.Text = strOrigin & " : " & Source
        End If

        Me.rtxt_Message.Text = Me.Message

        If lError Then Me.lbl_General.Text = strError Else Me.lbl_General.Text = strInfo

    End Sub

    Private Sub InitialiserFenetre()

        Me.Icon = Frm_PMX.Icon

        Me.pan_Main.Dock = DockStyle.Fill

        Me.lbk_Support.Text = LogicielInfo.MailSupport
        Me.lbk_Support.Left = Me.lbl_ContactSupport.Left + Me.lbl_ContactSupport.Width + 3

    End Sub

    Private Sub GestionStyle()

        Me.lbl_General.BackColor = MyOrange
        Me.lbl_General.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub lbk_Support_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbk_Support.LinkClicked
        Dim myDest As String = LogicielInfo.MailSupport

        Try

            PrepareMailSupport(myDest)

        Catch ex As Exception
            MsgBox("Erreur d'ouverture mail | Error opening mail", MsgBoxStyle.Critical, "Frm_About/LinkSupport_LinkClicked")
        End Try
    End Sub



#End Region

#Region "===FERMETURE==="
    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub

#End Region

#Region " Evènements "



#End Region

End Class