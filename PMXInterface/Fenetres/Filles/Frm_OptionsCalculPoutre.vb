Imports PMXMoteur2
Imports System.IO

Public Class Frm_OptionsCalculPoutre


#Region " Variables "

    Dim lBuild As Boolean
    Dim MyParam As cls_OptionsCalcul

    Dim tabNorme(1) As String
#End Region


#Region "===OUVERTURE==="


    Private Sub Frm_OptionsCalculPoutre_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionLangues()
        InitialisationsFenetre()
        GestionStyle()
        GestionUnites()
        AfficherPoutreEnCours()

        lBuild = False
    End Sub

    Private Sub InitialisationsFenetre()
        MyParam = MyProjet.Poutres(MyProjet.IndEnCours).Param.Clone
        RemplirComboStandard()
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_BEAMOPTIONS")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")
                Me.btn_Annuler.Text = Bloc("CANCEL")
                Me.btn_OK.Text = Bloc("OK")

                Me.lbl_CadreNorm.Text = Bloc("TSTANDARD")
                Me.lbl_Norme.Text = Bloc("TSTANDARD")
                tabNorme(0) = Bloc("ENGEN1")
                tabNorme(1) = Bloc("ENGEN2")

                Me.lbl_CadreELU.Text = Bloc("TELUOPTIONS")
                Me.rdb_NormalDesign.Text = Bloc("NORMALDESIGN")
                Me.rdb_ElasticDesign.Text = Bloc("ELASTICDESIGN")

                Me.lbl_CadreELS.Text = Bloc("TELSOPTIONS")


            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub RemplirComboStandard()
        Me.cmb_Norme.Items.Clear()
        Me.cmb_Norme.Items.Add(Me.tabNorme(0))

        If LogicielOptions.lExpert Then
            Me.cmb_Norme.Items.Add(Me.tabNorme(1))
        End If

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon
    End Sub

    Private Sub AfficherPoutreEnCours()

        '==> Norme

        Select Case MyParam.Norme
            Case cls_OptionsCalcul.Enu_Normes.EurocodesG1
                Me.cmb_Norme.SelectedIndex = 0
            Case cls_OptionsCalcul.Enu_Normes.EurocodesG2
                Me.cmb_Norme.SelectedIndex = Math.Min(1, Me.cmb_Norme.Items.Count - 1)
        End Select

        '==> Options ELU

        If MyParam.lElasticDesign Then
            Me.rdb_ElasticDesign.Checked = True
        Else
            Me.rdb_NormalDesign.Checked = True
        End If

        '==> Options ELS

    End Sub

#End Region

#Region "===FERMETURE==="

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Dim lModif As Boolean = False
        If ValideSaisieFenetre() Then

            TransfertSaisie(lModif)

            If lModif Then

            End If
            Me.Close()
        End If
    End Sub


    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        GereTransfertValeur(MyParam.lElasticDesign, MyProjet.Poutres(MyProjet.IndEnCours).Param.lElasticDesign, lModif)

    End Sub

    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        Me.Close()
    End Sub


#End Region

#Region " Evènements "


    Private Sub ULSSectionDesign_CheckedChanged_1(sender As Object, e As EventArgs) Handles rdb_NormalDesign.CheckedChanged, rdb_ElasticDesign.CheckedChanged

        MyParam.lElasticDesign = Me.rdb_ElasticDesign.Checked

    End Sub

#End Region

End Class