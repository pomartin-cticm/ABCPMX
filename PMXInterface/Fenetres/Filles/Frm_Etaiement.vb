Imports PMXMoteur2
Imports System.IO

Public Class Frm_Etaiement

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim MyPoutreLoc As New cls_Poutre

#End Region

#Region "===OUVERTURE==="
    Private Sub Frm_Etaiement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre(sender, e)
    End Sub

    Public Sub InitialiserFenetre(sender As Object, e As EventArgs)
        GestionLangues()
        GestionStyle()
        GestionUnites()
        InitialiserVariables()
        AfficherPoutreEnCours(sender, e)
        lBuild = False
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

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_Etaiement.BackColor = CouleurBackBandeaux
        Me.lbl_Etaiement.ForeColor = CouleurForeBandeaux

        Me.TLPan_Etaiement.ColumnStyles(0).Width = LargeurColonneSaisie

        Me.img_Etaiement.Dock = DockStyle.Fill
        Me.img_Etaiement.BorderStyle = BorderStyle.FixedSingle


    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub InitialiserVariables()
        cls_Poutre.Clone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)

        Me.cmb_NbPoint.Items.Clear()
        For i As Integer = NBPROPPINGMIN To NBPROPPINGMAX
            Me.cmb_NbPoint.Items.Add(i)
        Next
    End Sub

    Private Sub AfficherPoutreEnCours(sender As Object, e As EventArgs)

        With MyProjet.Poutres(MyProjet.IndEnCours)

            Select Case .TypeEtaiement

                Case .EnuTypeEtaiement.UnPropped
                    Me.rad_UnPropped.Checked = True
                    Me.chk_EtaisConsole.Visible = False
                    Me.chk_EtaisConsole.Checked = False
                    Me.cmb_NbPoint.Visible = False
                    Me.cmb_NbPoint.Text = 0

                Case .EnuTypeEtaiement.FullyPropped
                    Me.rad_FullyPropped.Checked = True
                    Me.chk_EtaisConsole.Visible = False
                    Me.chk_EtaisConsole.Checked = False
                    Me.cmb_NbPoint.Visible = False
                    Me.cmb_NbPoint.Text = 0

                Case .EnuTypeEtaiement.PointPropped
                    Me.rad_PointPropped.Checked = True
                    Me.chk_EtaisConsole.Visible = True
                    Me.chk_EtaisConsole.Checked = .lEtaisConsole
                    Me.cmb_NbPoint.Visible = True
                    Me.cmb_NbPoint.Text = .pNbPropping

            End Select

        End With

        rad_PointPropped_CheckedChanged(sender, e)

    End Sub

#End Region

#Region "===FERMETURE==="

    Public Sub TraitementSaisie()

    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        If ValideSaisieFenetre() Then

            Dim lModif As Boolean = False

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

        lModif = False

        With MyProjet.Poutres(MyProjet.IndEnCours)

            If (MyPoutreLoc.TypeEtaiement <> .EnuTypeEtaiement.PointPropped) And (.TypeEtaiement <> MyPoutreLoc.TypeEtaiement) Then
                lModif = True
                .TypeEtaiement = MyPoutreLoc.TypeEtaiement
                .pNbPropping = 0
                .lEtaisConsole = False

            ElseIf MyPoutreLoc.TypeEtaiement = .EnuTypeEtaiement.PointPropped Then
                .TypeEtaiement = MyPoutreLoc.TypeEtaiement

                If .lEtaisConsole <> MyPoutreLoc.lEtaisConsole Then
                    lModif = True
                    .lEtaisConsole = MyPoutreLoc.lEtaisConsole
                End If

                If .pNbPropping <> MyPoutreLoc.pNbPropping Then
                    lModif = True
                    .pNbPropping = MyPoutreLoc.pNbPropping
                End If
            End If
        End With
    End Sub

#End Region

#Region " Dessins "

    Private Sub DessinPoutre(sender As Object, e As PaintEventArgs) Handles img_Etaiement.Paint

        DessinFrmEtaiement(e.Graphics, MyPoutreLoc, Me.img_Etaiement.ClientRectangle.Width, Me.img_Etaiement.ClientRectangle.Height, 1, True)

    End Sub

#End Region

#Region " Evènements "
    Private Sub rad_PointPropped_CheckedChanged(sender As Object, e As EventArgs) Handles rad_UnPropped.CheckedChanged, rad_FullyPropped.CheckedChanged, rad_PointPropped.CheckedChanged
        Select Case True

            Case rad_FullyPropped.Checked
                MyPoutreLoc.TypeEtaiement = MyPoutreLoc.EnuTypeEtaiement.FullyPropped

                Me.chk_EtaisConsole.Visible = False
                Me.cmb_NbPoint.Visible = False

            Case rad_UnPropped.Checked
                MyPoutreLoc.TypeEtaiement = MyPoutreLoc.EnuTypeEtaiement.UnPropped

                Me.chk_EtaisConsole.Visible = False
                Me.cmb_NbPoint.Visible = False

            Case rad_PointPropped.Checked
                MyPoutreLoc.TypeEtaiement = MyPoutreLoc.EnuTypeEtaiement.PointPropped

                Me.chk_EtaisConsole.Visible = True
                If Not MyPoutreLoc.lTraveeConsoleDroite And Not MyPoutreLoc.lTraveeConsoleGauche Then
                    Me.chk_EtaisConsole.Enabled = False
                    Me.chk_EtaisConsole.Checked = False
                Else
                    Me.chk_EtaisConsole.Enabled = True
                    Me.chk_EtaisConsole.Checked = True
                End If

                Me.cmb_NbPoint.Visible = True

        End Select

        img_Etaiement.Invalidate()

    End Sub

    Private Sub chk_EtaisConsole_CheckedChanged(sender As Object, e As EventArgs) Handles chk_EtaisConsole.CheckedChanged
        MyPoutreLoc.lEtaisConsole = chk_EtaisConsole.Checked

        img_Etaiement.Invalidate()
    End Sub

    Private Sub cmb_NbPoint_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_NbPoint.SelectedIndexChanged
        MyPoutreLoc.pNbPropping = cmb_NbPoint.Text

        img_Etaiement.Invalidate()
    End Sub

#End Region

#Region " Evènements saisie "


#End Region
End Class