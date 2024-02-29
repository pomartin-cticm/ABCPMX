Imports System.IO
Imports PMXMoteur2

Public Class Frm_Juridique

#Region "=== Ouverture Fenetre ==="

    Private Sub Frm_Juridique_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '--------------------------------------------------------------------------------
        '   Ouverture de la fenêtre
        '--------------------------------------------------------------------------------

        '--> Textes qui ne changent pas
        Me.Text = LogicielInfo.NomLogiciel
        Me.RadioButton_En.Text = LogicielInfo.ListeLangue(0)
        Me.RadioButton_Fr.Text = LogicielInfo.ListeLangue(1)

        '--> Initialisation des controls
        If LogicielInfo.ListeLangue(LogicielOptions.IndLangue) = LogicielInfo.ListeLangue(0) Then
            RadioButton_En.Checked = True
        ElseIf LogicielInfo.ListeLangue(LogicielOptions.IndLangue) = LogicielInfo.ListeLangue(1) Then
            RadioButton_Fr.Checked = True
        End If

        '--> Si pas le mode expert = Obligation du français
        'If LogicielOptions.lExpert = False Then
        'RadioButton_En.Visible = False
        'LogicielOptions.IndLangue = 1
        'RadioButton_Fr.Checked = True
        'End If

        GestionLangue()
        GestionStyle()

    End Sub

    Private Sub GestionLangue()
        '--------------------------------------------------------------------
        '   Affichage du texte de la fenêtre dans la langue séléctionnée
        '--------------------------------------------------------------------

        If File.Exists(LogicielFichiers.Langue) Then
            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_JURIDIQUE")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.lbl_Langue.Text = Bloc("LANGUAGE").ToUpper
                Me.Btn_Accepter.Text = Bloc("AGREE")
                Me.Btn_Quitter.Text = Bloc("EXIT")

                Me.lbl_Juridique.Text = Bloc("WARNING").ToUpper

                '--> Texte juridique/d'avertissement
                Me.Label_InfoJuridique.Text = Chr(13) & Bloc("TEXT1") & Chr(13)
                Me.Label_InfoJuridique.Text += Chr(13) & Bloc("TEXT2") & Chr(13)
                Me.Label_InfoJuridique.Text += Chr(13) & Bloc("TEXT3") & Chr(13)
                Me.Label_InfoJuridique.Text += Chr(13) & Bloc("TEXT4")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, "Frm_Juridique/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionStyle()

        Me.lbl_Langue.BackColor = CouleurBackBandeaux
        Me.lbl_Langue.ForeColor = CouleurForeBandeaux

        Me.lbl_Juridique.BackColor = CouleurBackBandeaux
        Me.lbl_Juridique.ForeColor = CouleurForeBandeaux


    End Sub

#End Region

#Region " Gestion Evenement "

    Private Sub Langue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton_En.CheckedChanged, RadioButton_Fr.CheckedChanged
        '--------------------------------------------------------------------------------
        '   Changement de langue
        '--------------------------------------------------------------------------------

        '--> Mise à jour des couleurs
        RadioButton_En.BackColor = Color.Empty
        RadioButton_Fr.BackColor = Color.Empty
        RadioButton_En.ForeColor = Color.Black
        RadioButton_Fr.ForeColor = Color.Black
        'sender.backColor = Color.Gold
        sender.backColor = MyOrange
        sender.forecolor = Color.White

        '--> Mise à jour de la langue et des textes
        If RadioButton_En.Checked And sender.name = RadioButton_En.Name Then
            LogicielOptions.IndLangue = 0
        ElseIf RadioButton_Fr.Checked And sender.name = RadioButton_Fr.Name Then
            LogicielOptions.IndLangue = 1
        End If

        '--> Récupération du chemin du fichier
        InitialiseLNGFileName(LogicielOptions.IndLangue, LogicielFichiers.Langue)

        '--> MAJ du texte affiché
        GestionLangue()
        'Frm_MAIN.GestionLangue()

    End Sub

#End Region

End Class