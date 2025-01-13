Public Class Frm_OptionsLogicielDataBases

#Region " Variables "

    Dim lBuild As Boolean
    Const BALISE As String = "OPTSOFTDATABASES"

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFrm()
        lBuild = True

        GestionStyle()
        GestionLangue(Frm_OptionsLogiciel.BlocLangues(BALISE))
        PrepareFenetre()
        AfficheOptionsEnCours()

        lBuild = False
    End Sub


    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_DataBases.Text = MyBloc("DATABASES")

            Me.lbl_DataBases2.Text = MyBloc("DATABASES")
            Me.lbl_ClickToEdit.Text = MyBloc("CLICKTOEDIT")
            Me.lbl_Studs.Text = MyBloc("STUDS")
            Me.lbl_Bac.Text = MyBloc("SHEETINGS")

            Me.lbl_Profiles.Text = MyBloc("PROFILES")
            Me.lbl_FiltreSoft.Text = MyBloc("FILTRESOFT")
            Me.chk_HideProfileFilter.Text = MyBloc("HIDEFILTER")

            Me.lbl_Acier.Text = MyBloc("STEELS")



        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_DataBases.Dock = DockStyle.Fill

        Me.lbl_DataBases.BackColor = CouleurBackBandeaux
        Me.lbl_DataBases.ForeColor = CouleurForeBandeaux


    End Sub

    Private Sub PrepareFenetre()

        Me.txt_Bac.ForeColor = Me.lbl_ClickToEdit.ForeColor
        Me.txt_Studs.ForeColor = Me.lbl_ClickToEdit.ForeColor
        Me.txt_Profiles.ForeColor = Me.lbl_ClickToEdit.ForeColor
        PrepareReadOnly(Me.txt_Bac)
        PrepareReadOnly(Me.txt_Studs)
        PrepareReadOnly(Me.txt_Profiles)

    End Sub

    Private Sub PrepareReadOnly(ByRef MyTextB As TextBox)
        MyTextB.ReadOnly = True
        MyTextB.BackColor = CouleurReadOnly
    End Sub

    Private Sub AfficheOptionsEnCours()
        Me.txt_Bac.Text = NomFichierSeul(LogicielFichiers.Base_Bacs.Trim)
        Me.txt_Studs.Text = NomFichierSeul(LogicielFichiers.Base_Goujons.Trim)
        'Me.txt_Profiles.Text = NomFichierSeul(NomFichierBaseSection)
        Me.txt_Profiles.Text = NomFichierSeul(LogicielFichiers.Base_Sections.Trim)
        Me.txt_Acier.Text = NomFichierSeul(LogicielFichiers.Base_Aciers.Trim)

        Me.lbl_VersionProfiles.Text = "V" & OptionsDatabase.VersionBaseProfiles.Year.ToString & "_" & OptionsDatabase.VersionBaseProfiles.Indice.ToString
        Me.lbl_VersionSteel.Text = "V" & OptionsDatabase.VersionBaseAciers.Year.ToString & "_" & OptionsDatabase.VersionBaseAciers.Indice.ToString

        Me.txt_FiltreSoft.Text = OptionsDatabase.FiltreSoft
    End Sub

#End Region

#Region " Evènements "

    Private Sub ClickToEditDataBase(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Studs.Click, txt_Bac.Click, txt_Profiles.Click
        If lBuild Then Exit Sub

        Select Case sender.name

            Case Me.txt_Bac.Name

                Frm_EditBaseBacsAcier.ShowDialog()
                Frm_EditBaseBacsAcier.Dispose()


            Case Me.txt_Studs.Name

                Frm_EditGoujons.ShowDialog()
                Frm_EditGoujons.Dispose()

            Case Me.txt_Profiles.Name

                'If My.Computer.Keyboard.CtrlKeyDown Then
                '    ChangeReglageSub()
                'Else
                '    Frm_Catalogue.ShowDialog()
                '    Frm_Catalogue.Dispose()
                'End If
                Frm_Catalogue.ShowDialog()
                Frm_Catalogue.Dispose()

        End Select

    End Sub


#End Region

End Class