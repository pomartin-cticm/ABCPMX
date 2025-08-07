Imports System.Reflection
Imports PMXMoteur2
Imports System.IO

Public Class Frm_AjoutPoutreDeFichier

#Region " Variables de la fenêtre "

    Public FileName As String

    Dim Poutres As New List(Of String)          ' Nom des poutres contenues dans le fichier
    Dim iPoutres As New List(Of Integer)        ' Indice des poutres contenures dans le fichier

    Dim Lines As New List(Of String)

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_AjoutPoutreDeFichier_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GestionLangue()
        InitialiseFenetre()
        GestionStyle()

    End Sub

    Private Sub GestionLangue()


        If File.Exists(LogicielFichiers.Langue) Then
            Dim Bloc As New Dictionary(Of String, String)

            Dim strLoadedKey As String = ""
            Const CLE As String = ""

            Try
                Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_ADDBEAMFILE")
                BlocLine.CreationBloc(Bloc, strLoadedKey)

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                Me.lbl_Projet.Text = RemplaceDollar(Bloc("PROJECT"), MyProjet.Nom)
                Me.lbl_ChoisirPoutre.Text = Bloc("ADDBEAM")

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_Projet.BackColor = CouleurBackBandeaux
        Me.lbl_Projet.ForeColor = CouleurForeBandeaux
    End Sub

    Private Sub InitialiseFenetre()
        AnalyseFichier()
        RemplirListPoutre()
    End Sub

    Private Sub RemplirListPoutre()

        Me.chklst_Beams.Items.Clear()

        For i As Integer = 0 To Poutres.Count - 1

            Me.chklst_Beams.Items.Add(Poutres(i))

        Next

        Me.chklst_Beams.CheckOnClick = True

    End Sub

    Private Sub AnalyseFichier()

        '--( Déclaration

        Dim lOK As Boolean

        '--( Récupération des lignes du projet

        cls_Projet.ReadLineFile(Me.FileName, Lines, lOK)

        '--( Traitement

        cls_Projet.AnalyseFichier(Lines, Poutres, iPoutres)

    End Sub

#End Region

#Region "===FERMETURE==="

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        TraitementSaisie()
        Me.Close()
    End Sub

    Private Sub TraitementSaisie()

        Dim iBeamChk As Integer
        Dim iStop As Integer
        Dim nbPoutres As Integer = Poutres.Count

        Dim nbInitial As Integer = MyProjet.Poutres.Count

        For i As Integer = 0 To chklst_Beams.CheckedIndices.Count - 1

            iBeamChk = chklst_Beams.CheckedIndices(i)

            If iBeamChk = nbPoutres - 1 Then iStop = Lines.Count - 1 Else iStop = iPoutres(iBeamChk + 1)

            MyProjet.AddBeamFromFile(Lines, NomChargements, iPoutres(iBeamChk), iStop)

        Next i

        '-- Mise à jour des poutres chargées

        Dim lTrouve As Boolean

        '--( Mise à jour des données de la poutre rechargée

        For iBeam As Integer = nbInitial To MyProjet.Poutres.Count - 1
            InitialiseParametresSectionBase(MyProjet.Poutres(iBeam), lTrouve)
            MiseAJourPoutre(MyProjet.Poutres(iBeam), msgErreurs)
        Next

        MyProjet.IndEnCours = MyProjet.Poutres.Count - 1
    End Sub

#End Region


End Class