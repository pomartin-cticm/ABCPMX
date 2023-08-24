Imports PMXMoteur2
Imports System.IO

Public Class Frm_Combinaisons

#Region " Attributs "

    Dim lBuild As Boolean = True

    Dim nbCombELU As Integer
    Dim nbCombELS As Integer
    Dim nbCombFeu As Integer
    Dim nbCombELUConstruction As Integer
    Dim nbCombELSConstruction As Integer

    Private plCombELU() As Boolean                  'Indique si combinaison ELU sélectionnée
    Private plCombELS() As Boolean                  'Indique si combinaison ELS sélectionnée
    Private plCombFeu() As Boolean                  'Indique si combinaison Feu sélectionnée
    Private plCombELCURules() As Boolean            'Indique si combinaison réglementaire ELU Phase de construction
    Private plCombELCSRules() As Boolean            'Indique si combinaison réglementaire ELU Phase de construction

    Private pCoefCombELU() As List(Of Decimal)      'Table des coefficients des combinaisons ELU
    Private pCoefCombELS() As List(Of Decimal)      'Table des coefficients des combinaisons ELS
    Private pCoefCombFeu() As List(Of Decimal)      'Table des coefficients des combinaisons Feu
    Private pCoefCombELCU() As List(Of Decimal)     'Table des coefficients des combinaisons ELU Construction
    Private pCoefCombELCS() As List(Of Decimal)     'Table des coefficients des combinaisons ELS Construction

    Public BlocLangues As Dictionary(Of String, Dictionary(Of String, String))
    Const BALISE As String = "FRM_COMBINATIONS"


#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Combinaisons_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Private Sub InitialiserFenetre()
        lBuild = True

        ChargesBlocsLangues()
        GestionLangues(BlocLangues(BALISE))
        GestionStyle()
        GestionUnites()
        InitialiseVariables()
        'MAJI_TypeEL()
        AfficherStatutBtnEL()
        AfficherCombinaisonsEncours()
        lBuild = False
    End Sub

    Public Sub ChargesBlocsLangues()
        '----------------------------------------------------------------------------------------
        '   Récupération des blocs langues pour toutes les fenêtres fille
        '----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Lines As New Cls_LinesOfFile(LogicielFichiers.Langue, False)
        Dim BlocALire() As String = {"FRM_COMBINATIONS", "FRM_COMBINATIONS1"}
        Dim lBlocEnCours As Boolean = False
        Dim BlocEnCours As String = Nothing
        Dim MotCle, Argument As String
        Dim MyBloc As Dictionary(Of String, String) = Nothing
        Dim Index As Integer

        '--> Initialisation

        BlocLangues = New Dictionary(Of String, Dictionary(Of String, String))

        '--> Boucle sur les lignes

        For i As Integer = 0 To Lines.Lines.Count - 1

            If Lines.Lines(i).Trim.IndexOf("#") = 0 Then
                MotCle = Lines.Lines(i).Trim.ToUpper.Substring(1)
                If (Array.IndexOf(BlocALire, MotCle) > -1) Then
                    lBlocEnCours = True
                    BlocEnCours = MotCle
                    MyBloc = New Dictionary(Of String, String)
                    MyBloc.Clear()
                End If
            ElseIf Lines.Lines(i).Trim.Length = 0 Then
                If lBlocEnCours Then
                    BlocLangues.Add(BlocEnCours, MyBloc)
                End If
                lBlocEnCours = False
            ElseIf lBlocEnCours Then
                Index = Lines.Lines(i).IndexOf("=")
                If Index > -1 Then
                    MotCle = Lines.Lines(i).Substring(0, Index).Trim
                    Argument = Lines.Lines(i).Substring(Index + 1).Trim
                    MyBloc.Add(MotCle, Argument)
                End If

            End If

        Next

        If lBlocEnCours Then
            BlocLangues.Add(BlocEnCours, MyBloc)
        End If
        lBlocEnCours = False

    End Sub

    Private Sub GestionLangues(ByVal MyBloc As Dictionary(Of String, String))

        Try

            '=== GENERAL ======================================================================

            Me.Text = MyBloc("TITLE")
            Me.btn_OK.Text = MyBloc("OK")
            Me.btn_Annuler.Text = MyBloc("CANCEL")

            '=== CHOIX EL ==============================================================='

            Me.rdb_ELU.Text = MyBloc("ULSTATES")
            Me.rdb_ELS.Text = MyBloc("SLSTATES")
            Me.rdb_ELFire.Text = MyBloc("FLSTATES")
            Me.rdb_Construction.Text = MyBloc("CONSTRUCTIONSTAGE")

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try

    End Sub

    Private Sub TransfereCombi(ByRef pCombi() As List(Of Decimal), CombiSource() As List(Of Decimal), nbCombi As Integer)
        '----------------------------------------------------------------------------------------------------------------
        '   pCombi      [S] :   Tableau de combinaisons local
        '   CombiSource [E] :   Tableau de combonaisons source
        '   nbCombi     [E] :   Nombre de combinaisons à transférer
        '----------------------------------------------------------------------------------------------------------------

        For i As Integer = 0 To nbCombi
            pCombi(i) = New List(Of Decimal)
            For j As Integer = 0 To CombiSource(i).Count - 1
                pCombi(i).Add(CombiSource(i)(j))
            Next
        Next
    End Sub

    Private Sub InitialiseVariables()

        nbCombELU = cls_Poutre.nbCombELU
        ReDim plCombELU(nbCombELU)
        ReDim pCoefCombELU(nbCombELU)
        plCombELU = MyProjet.Poutres(MyProjet.IndEnCours).lCombELU
        TransfereCombi(pCoefCombELU, MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELU, nbCombELU)
        'For i As Integer = 0 To nbCombELU
        '    For j As Integer = 0 To pCoefCombELU(i).Count - 1
        '        pCoefCombELU(i).Add(MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELU(i)(j))
        '    Next
        'Next

        nbCombELS = cls_Poutre.nbCombELS
        ReDim plCombELS(nbCombELS)
        ReDim pCoefCombELS(nbCombELS)
        plCombELS = MyProjet.Poutres(MyProjet.IndEnCours).lCombELS
        TransfereCombi(pCoefCombELS, MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELS, nbCombELS)

        nbCombFeu = cls_Poutre.nbCombFeu
        ReDim plCombFeu(nbCombFeu)
        ReDim pCoefCombFeu(nbCombFeu)
        plCombFeu = MyProjet.Poutres(MyProjet.IndEnCours).lCombFeu
        TransfereCombi(pCoefCombFeu, MyProjet.Poutres(MyProjet.IndEnCours).CoefCombFeu, nbCombFeu)

        ' Private plCombELCURules(nbCombELUConstruction) As Boolean         'Indique si combinaison réglementaire ELU Phase de construction
        'Private plCombELCSRules(nbCombELSConstruction) As Boolean         'Indique si combinaison réglementaire ELU Phase de construction

        'Private pCoefCombELCU(nbCombELUConstruction) As List(Of Double) 'Table des coefficients des combinaisons ELU Construction
        ' Private pCoefCombELCS(nbCombELSConstruction) As List(Of Double) 'Table des coefficients des combinaisons ELS Construction



    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

    End Sub

    Private Sub AfficherStatutBtnEL()
        Select Case AffichageEL
            Case Enu_AffichageEL.ELU : Me.rdb_ELU.Checked = True
            Case Enu_AffichageEL.ELS : Me.rdb_ELS.Checked = True
            Case Enu_AffichageEL.ELF : Me.rdb_ELFire.Checked = True
            Case Enu_AffichageEL.Construction : Me.rdb_Construction.Checked = True
        End Select
    End Sub

    Private Sub AfficherCombinaisonsEncours()
        Select Case AffichageEL
            Case Enu_AffichageEL.ELU
                Me.pan_Contenu.Controls.Add(Frm_CombinaisonsNormales.pan_Combinaisons)
                Frm_CombinaisonsNormales.InitialiserFenetre(plCombELU, pCoefCombELU, cls_Poutre.nbCombELU + 1, 4, 2)
            Case Enu_AffichageEL.ELS
                Me.pan_Contenu.Controls.Add(Frm_CombinaisonsNormales.pan_Combinaisons)
                Frm_CombinaisonsNormales.InitialiserFenetre(plCombELS, pCoefCombELS, cls_Poutre.nbCombELS + 1, 4, 2)
            Case Enu_AffichageEL.ELF
                Me.pan_Contenu.Controls.Add(Frm_CombinaisonsNormales.pan_Combinaisons)
                Frm_CombinaisonsNormales.InitialiserFenetre(plCombFeu, pCoefCombFeu, cls_Poutre.nbCombFeu + 1, 3, 2)
        End Select
    End Sub


#End Region

#Region " Evènements "

    Private Sub GestionSelectEtatLimite(sender As Object, e As EventArgs) Handles rdb_ELU.CheckedChanged, rdb_ELS.CheckedChanged, rdb_ELFire.CheckedChanged, rdb_Construction.CheckedChanged
        If lBuild Then Exit Sub

        If Me.rdb_ELU.Checked Then
            AffichageEL = Enu_AffichageEL.ELU
        ElseIf Me.rdb_ELS.Checked Then
            AffichageEL = Enu_AffichageEL.ELS
        ElseIf Me.rdb_ELFire.Checked Then
            AffichageEL = Enu_AffichageEL.ELF
        Else
            AffichageEL = Enu_AffichageEL.Construction
        End If

        AfficherCombinaisonsEncours()
        ' MAJI_Equations()
    End Sub

    Public Sub ModifieSelectionCombi(Indice As Integer, lEtat As Boolean)

        Select Case AffichageEL
            Case Enu_AffichageEL.ELU : Me.plCombELU(Indice) = lEtat
            Case Enu_AffichageEL.ELS : Me.plCombELS(Indice) = lEtat
            Case Enu_AffichageEL.ELF : Me.plCombFeu(Indice) = lEtat
        End Select

    End Sub

    Public Sub ModifieValeurCoefCombi(IndCombi As Integer, IndCharge As Integer, Gamma As Decimal)

        Select Case AffichageEL
            Case Enu_AffichageEL.ELU : Me.pCoefCombELU(IndCombi)(IndCharge) = Gamma
            Case Enu_AffichageEL.ELS : Me.pCoefCombELS(IndCombi)(IndCharge) = Gamma
            Case Enu_AffichageEL.ELF : Me.pCoefCombFeu(IndCombi)(IndCharge) = Gamma
        End Select

    End Sub
#End Region

#Region "===FERMETURE==="

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click

        Dim lModif As Boolean
        If ValideSaisie() Then

            TransfertSaisie(lModif)

            If lModif Then

            End If

            Me.Close()
        End If

    End Sub

    Private Function ValideSaisie() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        Dim i, j As Integer

        For i = 0 To nbCombELU
            GereTransfertValeur(plCombELU(i), MyProjet.Poutres(MyProjet.IndEnCours).lCombELU(i), lModif)
            For j = 0 To Me.pCoefCombELU(i).Count - 1
                GereTransfertValeur(pCoefCombELU(i)(j), MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELU(i)(j), lModif)
            Next
        Next

        For i = 0 To nbCombELS
            GereTransfertValeur(plCombELS(i), MyProjet.Poutres(MyProjet.IndEnCours).lCombELS(i), lModif)
            For j = 0 To Me.pCoefCombELU(i).Count - 1
                GereTransfertValeur(pCoefCombELS(i)(j), MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELS(i)(j), lModif)
            Next
        Next

        For i = 0 To nbCombFeu
            GereTransfertValeur(plCombFeu(i), MyProjet.Poutres(MyProjet.IndEnCours).lCombFeu(i), lModif)
            For j = 0 To Me.pCoefCombELU(i).Count - 1
                GereTransfertValeur(pCoefCombFeu(i)(j), MyProjet.Poutres(MyProjet.IndEnCours).CoefCombFeu(i)(j), lModif)
            Next
        Next

    End Sub

#End Region


End Class