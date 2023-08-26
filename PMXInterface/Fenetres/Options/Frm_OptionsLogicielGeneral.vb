Public Class Frm_OptionsLogicielGeneral


#Region " Variables et constantes "

    Const BALISE As String = "OPTSOFTGENERAL"

    Const formatGAMMA As String = "0.00"
    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFrm()
        lBuild = True
        GestionLangue(Frm_OptionsLogiciel.BlocLangues(BALISE))
        GestionStyle()
        'GestionUnites()
        'InitialiseLangues("ACBPlus", Me.lst_LangueGUI, tabAbbrGUI, LogicielInfo.Langue)

        AfficherOptionsEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Langues.Text = MyBloc("LANGUAGES")
            Me.lbl_LangueGUI.Text = MyBloc("USERGI")
            Me.lbl_LangueNdC.Text = MyBloc("CALCULREPORT")

            Me.lbl_Identification.Text = MyBloc("IDENTIFICATION")


        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_General.Dock = DockStyle.Fill

        Me.lbl_Langues.BackColor = CouleurBackBandeaux
        Me.lbl_Langues.ForeColor = CouleurForeBandeaux

        Me.lbl_Identification.BackColor = CouleurBackBandeaux
        Me.lbl_Identification.ForeColor = CouleurForeBandeaux


        ' Me.lst_LangueNdC.Items(Me.lst_LangueNdC.SelectedIndex)
    End Sub

    Private Sub AfficherOptionsEnCours()

        InitialiseLangues(Me.lst_LangueGUI, LogicielInfo.ListeLangue, LogicielOptions.IndLangue)
        InitialiseLangues(Me.lst_LangueNdC, LogicielInfo.ListeLangueNDC, LogicielOptions.IndLangueNDC)

    End Sub


    Private Sub InitialiseLangues(ByRef MyLst As ListBox, ByVal tabLangues As String(), IndexL As Integer)

        '--> Déclarations

        Dim i As Integer

        '--> Remplissage liste langue

        MyLst.Items.Clear()
        For i = 0 To tabLangues.Count - 1
            MyLst.Items.Add(tabLangues(i))
        Next i

        MyLst.SelectedIndex = IndexL
        MyLst.Refresh()

    End Sub

#End Region

#Region " Evènements "

    Private Sub GestionChangeLangue(sender As Object, e As EventArgs) Handles lst_LangueNdC.SelectedIndexChanged, lst_LangueGUI.SelectedIndexChanged

        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.lst_LangueGUI.Name

                Frm_OptionsLogiciel.pLocalLogicielOptions.IndLangue = Me.lst_LangueGUI.SelectedIndex
                Frm_OptionsLogiciel.ChargesBlocsLangues()
                Frm_OptionsLogiciel.ReinitLangues()
                GestionLangue(Frm_OptionsLogiciel.BlocLangues(BALISE))

            Case Me.lst_LangueNdC.Name

                Frm_OptionsLogiciel.pLocalLogicielOptions.IndLangueNDC = Me.lst_LangueNdC.SelectedIndex

        End Select



    End Sub


#End Region

#Region " Tests "

    'Private Sub BranchListBox_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lst_LangueGUI.DrawItem

    '    'exit this event in case there is not valid index.
    '    If e.Index = -1 Then
    '        Exit Sub
    '    End If

    '    Dim mybrush As New System.Drawing.SolidBrush(Color.FromArgb(0, 177, 89))
    '    mybrush.Color = Color.FromArgb(0, 177, 89)

    '    Try
    '        e.DrawBackground()

    '        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
    '            e.Graphics.FillRectangle(mybrush, e.Bounds)
    '        End If

    '        Using b As New SolidBrush(e.ForeColor)
    '            e.Graphics.DrawString(lst_LangueGUI.GetItemText(lst_LangueGUI.Items(e.Index)), e.Font, b, e.Bounds)
    '        End Using

    '        e.DrawFocusRectangle()
    '    Catch ex As Exception
    '        'ColorAppend(LogsBox, Color.Red, TimeOfDay.ToString("h:mm:ss") & SystemLog & ex.Message & Environment.NewLine)
    '        'LogsBox.ScrollToCaret()
    '    End Try
    'End Sub


#End Region

End Class