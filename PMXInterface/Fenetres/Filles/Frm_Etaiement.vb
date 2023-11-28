Imports PMXMoteur2
Imports System.IO

Public Class Frm_Etaiement

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim MyPoutreLoc As New cls_Poutre(NomChargements)

    Const iFRMETAIEMENT As Integer = 2

#End Region

#Region "===OUVERTURE==="
    Private Sub Frm_Etaiement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre(sender, e)
    End Sub

    Public Sub InitialiserFenetre(sender As Object, e As EventArgs)
        lBuild = True
        GestionLangues()
        InitialiserVariables()
        GestionStyle()
        GestionUnites()
        AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_PROPPING")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== MENU PRINCIPAL ==============================================================='

                Me.lbl_Etaiement.Text = Bloc("PROPPINGTYPE")
                Me.rad_UnPropped.Text = Bloc("UNPROPPED")
                Me.rad_FullyPropped.Text = Bloc("FULLYPROPPED")
                Me.rad_PointPropped.Text = Bloc("POINTPROPPED")
                Me.chk_EtaisConsoleGauche.Text = Bloc("LEFTCANTPROPP")
                Me.chk_EtaisConsoleDroite.Text = Bloc("RIGHTCANTPROPP")
                Me.lbl_NbPP.Text = Bloc("NUMBPROPPING")
                Me.lbl_LocPP.Text = Bloc("PROPPINGLOC")

                'Me.cmb_LocPP.Items.Clear()
                'Me.cmb_LocPP.Items.Add(Bloc("UNDERSTEELBEAM"))
                'Me.cmb_LocPP.Items.Add(Bloc("UNDERSLAB"))
                'Me.cmb_LocPP.SelectedIndex = 0

                Me.rad_UnderBeam.Text = (Bloc("UNDERSTEELBEAM"))
                Me.rad_UnderSlab.Text = (Bloc("UNDERSLAB"))


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

        Me.chk_EtaisConsoleGauche.Enabled = MyPoutreLoc.lTraveeConsoleGauche
        Me.chk_EtaisConsoleDroite.Enabled = MyPoutreLoc.lTraveeConsoleDroite

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub InitialiserVariables()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)

        Me.cmb_NbPoint.Items.Clear()
        For i As Integer = NBPROPPINGMIN To OptionsScope.NbMaxiEtaisP
            Me.cmb_NbPoint.Items.Add(i)
        Next
        Me.cmb_NbPoint.SelectedIndex = 0

        Me.rad_UnderBeam.Checked = True

    End Sub

    Private Sub AfficherPoutreEnCours()

        With MyPoutreLoc

            Select Case .TypeEtaiement

                Case .EnuTypeEtaiement.UnPropped
                    Me.rad_UnPropped.Checked = True

                Case .EnuTypeEtaiement.FullyPropped
                    Me.rad_FullyPropped.Checked = True

                Case .EnuTypeEtaiement.PointPropped
                    Me.rad_PointPropped.Checked = True

                    If Not .lTraveeConsoleDroite Then
                        Me.chk_EtaisConsoleDroite.Checked = False
                    Else
                        Me.chk_EtaisConsoleDroite.Checked = .lEtaisConsoleDroite
                    End If

                    If Not .lTraveeConsoleGauche Then
                        Me.chk_EtaisConsoleGauche.Checked = False
                    Else
                        Me.chk_EtaisConsoleGauche.Checked = .lEtaisConsoleGauche
                    End If

                    Me.cmb_NbPoint.SelectedItem = .NbEtaiement

                    If .lEtaisSousProfileAcier Then
                        'Me.cmb_LocPP.SelectedIndex = 0
                        Me.rad_UnderBeam.Checked = True
                    Else
                        'Me.cmb_LocPP.SelectedIndex = 1
                        Me.rad_UnderSlab.Checked = True
                    End If


            End Select

        End With

        MAJI_PointProps()

    End Sub

    Private Sub MAJI_PointProps()

        Me.pan_PointProps.Visible = (MyPoutreLoc.TypeEtaiement = cls_Poutre.EnuTypeEtaiement.PointPropped)

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
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If

            MyProjet.Poutres(MyProjet.IndEnCours).EstValidee(iFRMETAIEMENT)

            Me.Close()
        End If
    End Sub

    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False

        With MyProjet.Poutres(MyProjet.IndEnCours)

            If MyPoutreLoc.TypeEtaiement = cls_Poutre.EnuTypeEtaiement.PointPropped _
                And Not MyPoutreLoc.lTraveeConsoleGauche And Not MyPoutreLoc.lTraveeConsoleDroite _
                And Not MyPoutreLoc.lEtaisConsoleGauche And Not MyPoutreLoc.lEtaisConsoleDroite And MyPoutreLoc.NbEtaiement = 0 Then

                MyPoutreLoc.TypeEtaiement = cls_Poutre.EnuTypeEtaiement.UnPropped

            End If

            If (MyPoutreLoc.TypeEtaiement <> .EnuTypeEtaiement.PointPropped) And (.TypeEtaiement <> MyPoutreLoc.TypeEtaiement) Then
                lModif = True
                .TypeEtaiement = MyPoutreLoc.TypeEtaiement
                .NbEtaiement = 0
                .lEtaisConsoleGauche = False
                .lEtaisConsoleDroite = False

            ElseIf MyPoutreLoc.TypeEtaiement = .EnuTypeEtaiement.PointPropped Then
                .TypeEtaiement = MyPoutreLoc.TypeEtaiement

                If .lEtaisConsoleGauche <> MyPoutreLoc.lEtaisConsoleGauche Then
                    lModif = True
                    .lEtaisConsoleGauche = MyPoutreLoc.lEtaisConsoleGauche
                End If

                If .lEtaisConsoleDroite <> MyPoutreLoc.lEtaisConsoleDroite Then
                    lModif = True
                    .lEtaisConsoleDroite = MyPoutreLoc.lEtaisConsoleDroite
                End If

                If .NbEtaiement <> MyPoutreLoc.NbEtaiement Then
                    lModif = True
                    .NbEtaiement = MyPoutreLoc.NbEtaiement
                End If

                If .lEtaisSousProfileAcier <> MyPoutreLoc.lEtaisSousProfileAcier Then
                    lModif = True
                    .lEtaisSousProfileAcier = MyPoutreLoc.lEtaisSousProfileAcier
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
        If lBuild Then Exit Sub

        Select Case True

            Case rad_FullyPropped.Checked
                MyPoutreLoc.TypeEtaiement = MyPoutreLoc.EnuTypeEtaiement.FullyPropped

                'Me.chk_EtaisConsoleGauche.Visible = False
                'Me.chk_EtaisConsoleGauche.Checked = False
                'Me.chk_EtaisConsoleDroite.Visible = False
                'Me.chk_EtaisConsoleDroite.Checked = False
                'Me.lbl_NbPP.Visible = False
                'Me.cmb_NbPoint.Visible = False

            Case rad_UnPropped.Checked
                MyPoutreLoc.TypeEtaiement = MyPoutreLoc.EnuTypeEtaiement.UnPropped

                'Me.chk_EtaisConsoleGauche.Visible = False
                'Me.chk_EtaisConsoleGauche.Checked = False
                'Me.chk_EtaisConsoleDroite.Visible = False
                'Me.chk_EtaisConsoleDroite.Checked = False
                'Me.lbl_NbPP.Visible = False
                'Me.cmb_NbPoint.Visible = False

            Case rad_PointPropped.Checked
                MyPoutreLoc.TypeEtaiement = MyPoutreLoc.EnuTypeEtaiement.PointPropped

                'Me.chk_EtaisConsoleGauche.Visible = True
                'If Not MyPoutreLoc.lTraveeConsoleGauche Then
                '    Me.chk_EtaisConsoleGauche.Enabled = False
                '    Me.chk_EtaisConsoleGauche.Checked = False
                'Else
                '    Me.chk_EtaisConsoleGauche.Enabled = True
                '    Me.chk_EtaisConsoleGauche.Checked = MyPoutreLoc.lEtaisConsoleGauche
                'End If

                'Me.chk_EtaisConsoleDroite.Visible = True
                'If Not MyPoutreLoc.lTraveeConsoleDroite Then
                '    Me.chk_EtaisConsoleDroite.Enabled = False
                '    Me.chk_EtaisConsoleDroite.Checked = False
                'Else
                '    Me.chk_EtaisConsoleDroite.Enabled = True
                '    Me.chk_EtaisConsoleDroite.Checked = MyPoutreLoc.lEtaisConsoleDroite
                'End If

                'Me.lbl_NbPP.Visible = True
                'Me.cmb_NbPoint.Visible = True

                If MyPoutreLoc.lEtaisSousProfileAcier Then
                    'Me.cmb_LocPP.SelectedIndex = 0
                    Me.rad_UnderBeam.Checked = True
                Else
                    'Me.cmb_LocPP.SelectedIndex = 1
                    Me.rad_UnderSlab.Checked = True
                End If

        End Select

        img_Etaiement.Invalidate()
        MAJI_PointProps()

    End Sub

    Private Sub chk_EtaisConsole_CheckedChanged(sender As Object, e As EventArgs) Handles chk_EtaisConsoleGauche.CheckedChanged, chk_EtaisConsoleDroite.CheckedChanged
        MyPoutreLoc.lEtaisConsoleGauche = chk_EtaisConsoleGauche.Checked
        MyPoutreLoc.lEtaisConsoleDroite = chk_EtaisConsoleDroite.Checked

        img_Etaiement.Invalidate()
    End Sub

    Private Sub cmb_NbPoint_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_NbPoint.TextChanged
        If lBuild Then Exit Sub
        MyPoutreLoc.NbEtaiement = cmb_NbPoint.SelectedItem

        img_Etaiement.Invalidate()
    End Sub

    Private Sub cmb_LocPP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rad_UnderSlab.CheckedChanged, rad_UnderBeam.CheckedChanged
        If lBuild Then Exit Sub

        'MyPoutreLoc.lEtaisSousProfileAcier = cmb_LocPP.SelectedIndex = 0
        MyPoutreLoc.lEtaisSousProfileAcier = rad_UnderBeam.Checked

        img_Etaiement.Invalidate()

    End Sub

#End Region

#Region " Evènements saisie "


#End Region

End Class