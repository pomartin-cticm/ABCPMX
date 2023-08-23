Imports PMXMoteur2
Imports System.IO

Public Class Frm_Combinaisons

#Region " Attributs "

    Dim lBuild As Boolean = True

    Private Enum Enu_AffichageEL
        ELS
        ELU
        ELF
    End Enum
    Private AffichageEL As Enu_AffichageEL = Enu_AffichageEL.ELU

    Dim nbCombELU As Integer
    Dim nbCombELS As Integer
    Dim nbCombFeu As Integer
    Dim nbCombELUConstruction As Integer
    Dim nbCombELSConstruction As Integer

    Private plCombELURules() As Boolean             'Indique si combinaison réglementaire ELU
    Private plCombELSRules() As Boolean             'Indique si combinaison réglementaire ELS
    Private plCombFeuRules() As Boolean             'Indique si combinaison réglementaire Feu
    Private plCombELCURules() As Boolean            'Indique si combinaison réglementaire ELU Phase de construction
    Private plCombELCSRules() As Boolean            'Indique si combinaison réglementaire ELU Phase de construction

    Private pCoefCombELU() As List(Of Decimal)      'Table des coefficients des combinaisons ELU
    Private pCoefCombELS() As List(Of Decimal)      'Table des coefficients des combinaisons ELS
    Private pCoefCombFeu() As List(Of Decimal)      'Table des coefficients des combinaisons Feu
    Private pCoefCombELCU() As List(Of Decimal)     'Table des coefficients des combinaisons ELU Construction
    Private pCoefCombELCS() As List(Of Decimal)     'Table des coefficients des combinaisons ELS Construction

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Combinaisons_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Private Sub InitialiserFenetre()
        lBuild = True

        GestionLangues()
        GestionStyle()
        GestionUnites()
        InitialiseVariables()
        AfficherPoutreEnCours()

        lBuild = False
    End Sub


    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_COMBINATIONS")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== GENERAL ======================================================================

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== CHOIX EL ==============================================================='

                Me.rdb_ELU.Text = Bloc("ULS")
                Me.rdb_ELS.Text = Bloc("SLS")
                Me.rdb_ELFire.Text = Bloc("FLS")

                '=== COMBINAISONS

                Me.lbl_Predefinies.Text = Bloc("PREDEFINED")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub TransfereCombi(ByRef pCombi() As List(Of Decimal), CombiSource() As List(Of Decimal), nbCombi As Integer)
        '----------------------------------------------------------------------------------------------------------------
        '   pCombi      [S] :   Tableau de combinaisons local
        '   CombiSource [E] :   Tableau de combonaisons source
        '   nbCombi     [E] :   Nombre de combinaisons à transférer
        '----------------------------------------------------------------------------------------------------------------

        For i As Integer = 0 To nbCombi
            pCombi(i).Clear()
            For j As Integer = 0 To CombiSource(i).Count - 1
                pCombi(i).Add(CombiSource(i)(j))
            Next
        Next
    End Sub

    Private Sub InitialiseVariables()

        nbCombELU = cls_Poutre.nbCombELU
        ReDim plCombELURules(nbCombELU)
        ReDim pCoefCombELU(nbCombELU)
        plCombELURules = MyProjet.Poutres(MyProjet.IndEnCours).lCombELCURules
        TransfereCombi(pCoefCombELU, MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELU, nbCombELU)
        For i As Integer = 0 To nbCombELU
            For j As Integer = 0 To pCoefCombELU(i).Count - 1
                pCoefCombELU(i).Add(MyProjet.Poutres(MyProjet.IndEnCours).CoefCombELU(i)(j))
            Next
        Next

            nbCombELS = cls_Poutre.nbCombELS
        ReDim plCombELSRules(nbCombELS)
        ReDim pCoefCombELS(nbCombELS)

        nbCombFeu = cls_Poutre.nbCombFeu
        ReDim plCombFeuRules(nbCombFeu)
        ReDim pCoefCombFeu(nbCombFeu)

        ' Private plCombELCURules(nbCombELUConstruction) As Boolean         'Indique si combinaison réglementaire ELU Phase de construction
        'Private plCombELCSRules(nbCombELSConstruction) As Boolean         'Indique si combinaison réglementaire ELU Phase de construction

        'Private pCoefCombELCU(nbCombELUConstruction) As List(Of Double) 'Table des coefficients des combinaisons ELU Construction
        ' Private pCoefCombELCS(nbCombELSConstruction) As List(Of Double) 'Table des coefficients des combinaisons ELS Construction
    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon
        Me.lbl_Predefinies.BackColor = CouleurBackBandeaux
        Me.lbl_Predefinies.ForeColor = CouleurForeBandeaux
        Me.lbl_Custom.BackColor = CouleurBackBandeaux
        Me.lbl_Custom.ForeColor = CouleurForeBandeaux
    End Sub

    Private Sub AfficherPoutreEnCours()
        Select Case AffichageEL
            Case Enu_AffichageEL.ELU : Me.rdb_ELU.Checked = True
            Case Enu_AffichageEL.ELS : Me.rdb_ELS.Checked = True
            Case Enu_AffichageEL.ELF : Me.rdb_ELFire.Checked = True
        End Select
        AfficherCombinaisonsEncours()
    End Sub

    Private Sub AfficherCombinaisonsEncours()

    End Sub

#End Region

#Region "===FERMETURE==="


#End Region




End Class