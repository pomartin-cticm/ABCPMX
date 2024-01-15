Imports PMXMoteur2
Imports System.IO

Public Class Frm_Combinaisons

#Region " Attributs "

    Dim lBuild As Boolean = True

    Dim nbCombELU_Loc As Integer
    Dim nbCombELS_Loc As Integer
    Dim nbCombFeu_Loc As Integer
    Dim nbCombELUConstruction_Loc As Integer
    Dim nbCombELSConstruction_Loc As Integer

    Private plCombELU_Loc() As Boolean                  'Indique si combinaison ELU sélectionnée
    Private plCombELS_Loc() As Boolean                  'Indique si combinaison ELS sélectionnée
    Private plCombFeu_Loc() As Boolean                  'Indique si combinaison Feu sélectionnée
    Private plCombELCURules_Loc() As Boolean            'Indique si combinaison réglementaire ELU Phase de construction
    Private plCombELCSRules_Loc() As Boolean            'Indique si combinaison réglementaire ELU Phase de construction

    Private pCoefCombELU_Loc() As List(Of Decimal)      'Table des coefficients des combinaisons ELU
    Private pCoefCombELS_Loc() As List(Of Decimal)      'Table des coefficients des combinaisons ELS
    Private pCoefCombFeu_Loc() As List(Of Decimal)      'Table des coefficients des combinaisons Feu
    Private pCoefCombELCU_Loc() As List(Of Decimal)     'Table des coefficients des combinaisons ELU Construction
    Private pCoefCombELCS_Loc() As List(Of Decimal)     'Table des coefficients des combinaisons ELS Construction

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
        Dim BlocALire() As String = {"FRM_COMBINATIONS", "FRM_COMBINATIONS1", "FRM_COMBINATIONS2"}
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

    Private Sub TransfereCombi(ByRef pCombiCible() As List(Of Decimal), CombiSource() As List(Of Decimal), nbCombi As Integer)
        '----------------------------------------------------------------------------------------------------------------
        '   pCombi      [S] :   Tableau de combinaisons local
        '   CombiSource [E] :   Tableau de combonaisons source
        '   nbCombi     [E] :   Nombre de combinaisons à transférer
        '----------------------------------------------------------------------------------------------------------------

        For i As Integer = 0 To nbCombi - 1
            pCombiCible(i) = New List(Of Decimal)
            For j As Integer = 0 To CombiSource(i).Count - 1
                pCombiCible(i).Add(CombiSource(i)(j))
            Next
        Next
    End Sub

    Private Sub InitialiseVariables()

        nbCombELU_Loc = cls_Poutre.nbCombELU
        ReDim plCombELU_Loc(nbCombELU_Loc)
        ReDim pCoefCombELU_Loc(nbCombELU_Loc)
        plCombELU_Loc = MyProjet.Poutres(MyProjet.IndEnCours).lCombELU.Clone
        TransfereCombi(pCoefCombELU_Loc, MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELU, nbCombELU_Loc)
        'For i As Integer = 0 To nbCombELU
        '    For j As Integer = 0 To pCoefCombELU(i).Count - 1
        '        pCoefCombELU(i).Add(MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELU(i)(j))
        '    Next
        'Next

        nbCombELS_Loc = cls_Poutre.nbCombELS
        ReDim plCombELS_Loc(nbCombELS_Loc)
        ReDim pCoefCombELS_Loc(nbCombELS_Loc)
        plCombELS_Loc = MyProjet.Poutres(MyProjet.IndEnCours).lCombELS.Clone
        TransfereCombi(pCoefCombELS_Loc, MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELS, nbCombELS_Loc)

        nbCombFeu_Loc = cls_Poutre.nbCombFeu
        ReDim plCombFeu_Loc(nbCombFeu_Loc)
        ReDim pCoefCombFeu_Loc(nbCombFeu_Loc)
        plCombFeu_Loc = MyProjet.Poutres(MyProjet.IndEnCours).lCombFeu.Clone
        TransfereCombi(pCoefCombFeu_Loc, MyProjet.Poutres(MyProjet.IndEnCours).CoefCombFeu, nbCombFeu_Loc)

        ' Private plCombELCURules(nbCombELUConstruction) As Boolean         'Indique si combinaison réglementaire ELU Phase de construction
        'Private plCombELCSRules(nbCombELSConstruction) As Boolean         'Indique si combinaison réglementaire ELU Phase de construction

        'Private pCoefCombELCU(nbCombELUConstruction) As List(Of Double) 'Table des coefficients des combinaisons ELU Construction
        ' Private pCoefCombELCS(nbCombELSConstruction) As List(Of Double) 'Table des coefficients des combinaisons ELS Construction

        nbCombELUConstruction_Loc = cls_Poutre.nbCombELUConstruction
        ReDim plCombELCURules_Loc(nbCombELUConstruction_Loc)
        ReDim pCoefCombELCU_Loc(nbCombELUConstruction_Loc)
        plCombELCURules_Loc = MyProjet.Poutres(MyProjet.IndEnCours).lCombELCURules.Clone
        TransfereCombi(pCoefCombELCU_Loc, MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELCU, nbCombELUConstruction_Loc)

        nbCombELSConstruction_Loc = cls_Poutre.nbCombELSConstruction
        ReDim plCombELCSRules_Loc(nbCombELSConstruction_Loc)
        ReDim pCoefCombELCS_Loc(nbCombELSConstruction_Loc)
        plCombELCSRules_Loc = MyProjet.Poutres(MyProjet.IndEnCours).lCombELCSRules.Clone
        TransfereCombi(pCoefCombELCS_Loc, MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELCS, nbCombELSConstruction_Loc)

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        If MyProjet.Poutres(MyProjet.IndEnCours).lMixte Then
            Me.rdb_Construction.Visible = True
            For Each columnStyle As ColumnStyle In Me.TLpan_ChoixEL.ColumnStyles
                columnStyle.Width = 25
            Next
        Else
            Me.rdb_Construction.Visible = False
            For Each columnStyle As ColumnStyle In Me.TLpan_ChoixEL.ColumnStyles
                If Me.TLpan_ChoixEL.ColumnStyles.IndexOf(columnStyle) = Me.TLpan_ChoixEL.ColumnStyles.Count - 1 Then
                    columnStyle.Width = 0
                Else
                    columnStyle.Width = 33
                End If
            Next
        End If

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

        Me.pan_Contenu.Controls.Clear()

        Select Case AffichageEL
            Case Enu_AffichageEL.ELU
                Me.pan_Contenu.Controls.Add(Frm_CombinaisonsNormales.pan_Combinaisons)
                Frm_CombinaisonsNormales.InitialiserFenetre(plCombELU_Loc, pCoefCombELU_Loc, cls_Poutre.nbCombELU + 1, 4, 2)
            Case Enu_AffichageEL.ELS
                Me.pan_Contenu.Controls.Add(Frm_CombinaisonsNormales.pan_Combinaisons)
                Frm_CombinaisonsNormales.InitialiserFenetre(plCombELS_Loc, pCoefCombELS_Loc, cls_Poutre.nbCombELS + 1, 4, 2)
            Case Enu_AffichageEL.ELF
                Me.pan_Contenu.Controls.Add(Frm_CombinaisonsNormales.pan_Combinaisons)
                Frm_CombinaisonsNormales.InitialiserFenetre(plCombFeu_Loc, pCoefCombFeu_Loc, cls_Poutre.nbCombFeu + 1, 3, 2)
            Case Enu_AffichageEL.Construction
                Me.pan_Contenu.Controls.Add(Frm_CombinaisonsConstruction.pan_Combinaisons)
                Frm_CombinaisonsConstruction.InitialiserFenetre(plCombELCURules_Loc, pCoefCombELCU_Loc, cls_Poutre.nbCombELUConstruction, 1, 1,
                                                                plCombELCSRules_Loc, pCoefCombELCS_Loc, cls_Poutre.nbCombELSConstruction, 1, 1)
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

    Public Sub ModifieSelectionCombi(Indice As Integer, lEtat As Boolean, Optional lConstructionELU As Boolean = False)

        Select Case AffichageEL
            Case Enu_AffichageEL.ELU : Me.plCombELU_Loc(Indice) = lEtat
            Case Enu_AffichageEL.ELS : Me.plCombELS_Loc(Indice) = lEtat
            Case Enu_AffichageEL.ELF : Me.plCombFeu_Loc(Indice) = lEtat
            Case Enu_AffichageEL.Construction
                If lConstructionELU Then
                    Me.plCombELCURules_Loc(Indice) = lEtat
                Else
                    Me.plCombELCSRules_Loc(Indice) = lEtat
                End If
        End Select

    End Sub

    Public Sub ModifieValeurCoefCombi(IndCombi As Integer, IndCharge As Integer, Gamma As Decimal, Optional lConstructionELU As Boolean = False)

        Select Case AffichageEL
            Case Enu_AffichageEL.ELU : Me.pCoefCombELU_Loc(IndCombi)(IndCharge) = Gamma
            Case Enu_AffichageEL.ELS : Me.pCoefCombELS_Loc(IndCombi)(IndCharge) = Gamma
            Case Enu_AffichageEL.ELF : Me.pCoefCombFeu_Loc(IndCombi)(IndCharge) = Gamma
            Case Enu_AffichageEL.Construction
                If lConstructionELU Then
                    Me.pCoefCombELCU_Loc(IndCombi)(IndCharge) = Gamma
                Else
                    Me.pCoefCombELCS_Loc(IndCombi)(IndCharge) = Gamma
                End If
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

        For i = 0 To nbCombELU_Loc - 1
            GereTransfertValeur(plCombELU_Loc(i), MyProjet.Poutres(MyProjet.IndEnCours).lCombELU(i), lModif)
            For j = 0 To Me.pCoefCombELU_Loc(i).Count - 1
                GereTransfertValeur(pCoefCombELU_Loc(i)(j), MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELU(i)(j), lModif)
            Next
        Next

        For i = 0 To nbCombELS_Loc - 1
            GereTransfertValeur(plCombELS_Loc(i), MyProjet.Poutres(MyProjet.IndEnCours).lCombELS(i), lModif)
            For j = 0 To Me.pCoefCombELS_Loc(i).Count - 1
                GereTransfertValeur(pCoefCombELS_Loc(i)(j), MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELS(i)(j), lModif)
            Next
        Next

        For i = 0 To nbCombFeu_Loc - 1
            GereTransfertValeur(plCombFeu_Loc(i), MyProjet.Poutres(MyProjet.IndEnCours).lCombFeu(i), lModif)
            For j = 0 To Me.pCoefCombFeu_Loc(i).Count - 1
                GereTransfertValeur(pCoefCombFeu_Loc(i)(j), MyProjet.Poutres(MyProjet.IndEnCours).CoefCombFeu(i)(j), lModif)
            Next
        Next

        For i = 0 To nbCombELUConstruction_Loc - 1
            GereTransfertValeur(plCombELCURules_Loc(i), MyProjet.Poutres(MyProjet.IndEnCours).lCombELCURules(i), lModif)
            For j = 0 To Me.pCoefCombELCU_Loc(i).Count - 1
                GereTransfertValeur(pCoefCombELCU_Loc(i)(j), MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELCU(i)(j), lModif)
            Next
        Next

        For i = 0 To nbCombELSConstruction_Loc - 1
            GereTransfertValeur(plCombELCSRules_Loc(i), MyProjet.Poutres(MyProjet.IndEnCours).lCombELCSRules(i), lModif)
            For j = 0 To Me.pCoefCombELCS_Loc(i).Count - 1
                GereTransfertValeur(pCoefCombELCS_Loc(i)(j), MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELCS(i)(j), lModif)
            Next
        Next

    End Sub

#End Region


End Class