Imports PMXMoteur2
Imports System.IO

Public Class Frm_MaintienBacN

#Region " Déclarations "

    Enum enu_AffParametres
        Plancher
        Fixation
    End Enum

#End Region

#Region " Variables "

    Dim lBuild As Boolean = True
    Public localMaitienBac As New cls_MaintienBac

    Dim AffParam As enu_AffParametres

    Dim Bloc As New Dictionary(Of String, String)

    Dim lAffCalculs As Boolean = False
    Dim lAffFirst As Boolean = True

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_MaintienBacN_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lBuild = True

        GestionLangues()
        InitialiseVariables()
        InitialisationFenetre()
        GestionStyle()

        lBuild = False

    End Sub

    Private Sub InitialisationFenetre()
        AffichageParametres()
        MAJI_Calculs()
    End Sub

    Private Sub AffichageParametres()

        Me.pan_ContenuG.Controls.Clear()

        Select Case AffParam
            Case enu_AffParametres.Plancher
                Me.pan_ContenuG.Controls.Add(Frm_MaintienBacN_Plancher.pan_Main)
                Frm_MaintienBacN_Plancher.InitialiseFenetre(bloc)
            Case enu_AffParametres.Fixation
        End Select

    End Sub

    Private Sub InitialiseVariables()
        localMaitienBac = MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.Clone
        AffParam = enu_AffParametres.Plancher
    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.img_Deck.Dock = DockStyle.Fill

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_DECKRESTRAINT")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== GENERAL ======================================================================

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                Me.rdb_Plancher.Text = Bloc("FLOOR")
                Me.rdb_Fixations.Text = Bloc("FASTENINGRDB")
                Me.chk_Calculs.Text = Bloc("RESULTS")

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues")
                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                'Bloc.Clear()
            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If

    End Sub

#End Region

#Region " Gestion des mises à jour de la fenêtre "

    Public Sub MAJI_Dessin()

        Me.img_Deck.Invalidate()

    End Sub

    Public Sub MAJI_Calculs()

        If lAffCalculs Then

            Me.TLpan_MaintienBac.ColumnStyles(1).Width = 250

            If lAffFirst Then

                Me.pan_Calculs.Controls.Add(Frm_MaintienBacN_Calculs.pan_Main)
                Frm_MaintienBacN_Calculs.InitialiseFenetre(Bloc)

                lAffFirst = False
            End If
        Else
            Me.TLpan_MaintienBac.ColumnStyles(1).Width = 0
        End If

    End Sub


#End Region

#Region " Evènements "

    Private Sub chk_Calculs_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Calculs.CheckedChanged
        If lBuild Then Exit Sub
        lAffCalculs = Not lAffCalculs
        MAJI_Calculs()
    End Sub

#End Region

#Region "===FERMETURE==="

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Dim lModif As Boolean = False
        If ValideSaisieFenetre() Then

            TransfertSaisie(lModif)

            If lModif Then
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If

            'MyProjet.Poutres(MyProjet.IndEnCours).EstValidee(iFRMslab)

            Me.Close()
        End If
    End Sub

    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        GereTransfertValeur(localMaitienBac.m, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.m, lModif)
        GereTransfertValeur(localMaitienBac.nt, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.nt, lModif)
        GereTransfertValeur(localMaitienBac.ec, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.ec, lModif)
        GereTransfertValeur(localMaitienBac.lMaintienBac, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.lMaintienBac, lModif)
        GereTransfertValeur(localMaitienBac.lTheta, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.lTheta, lModif)

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.Transition <> localMaitienBac.Transition Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.Transition = localMaitienBac.Transition

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixNervuresMod <> localMaitienBac.FixNervuresMod Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixNervuresMod = localMaitienBac.FixNervuresMod

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixnervuresTyp <> localMaitienBac.FixnervuresTyp Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixnervuresTyp = localMaitienBac.FixnervuresTyp

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixCoutureType <> localMaitienBac.FixCoutureType Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixCoutureType = localMaitienBac.FixCoutureType

    End Sub


#End Region

#Region " Dessin du plancher "




#End Region

End Class