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

    Dim MyBrush As Brush
    Dim MyBrushBlue As Brush
    Dim MyBrushFond As Brush

    Dim MyBrushNoFond As Brush
    Dim MyBrushUnSelected As Brush
    Dim BrushBlue As Brush
    Dim BrushBlack As Brush
    Dim ColorSelect As Color = Color.LightGray
    Dim ColorUnSelect As Color
    Dim ColorFontUnSelected As Color = Color.Gray

    Dim FontNormal As Font
    Dim FontIndice As Font
    Dim FontSymbol As Font

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
            pCombi(i) = New List(Of Decimal)
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

        MyBrush = New SolidBrush(Color.Black)
        MyBrushBlue = New SolidBrush(Color.Blue)
        MyBrushFond = New SolidBrush(ColorSelect)

        ColorUnSelect = Me.pan_Predefinies.BackColor
        MyBrushNoFond = New SolidBrush(ColorUnSelect)
        MyBrushUnSelected = New SolidBrush(ColorFontUnSelected)

        FontNormal = New Font(Me.chk_Combinaison01.Font.Name, 8)
        FontIndice = New Font(Me.chk_Combinaison01.Font.Name, 7)
        FontSymbol = New Font("Symbol", 9)

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

#Region " Affichage des combinaisons dans les picturebox "


    Private Sub AffichageEquations(sender As Object, e As PaintEventArgs) Handles img_EL_Eq01.Paint

        Dim pWi, pHi As Single
        pWi = Me.img_EL_Eq01.ClientRectangle.Width
        pHi = Me.img_EL_Eq01.ClientRectangle.Height

        DrawEquationELU(e.Graphics, True, 1, pWi, pHi)

    End Sub


    Private Sub DrawEquationELU(ByVal MyGr As Graphics, ByVal lSelect As Boolean, ByVal Indice As Integer,
                             ByVal sWi As Single, ByVal sHi As Single)
        '----------------------------------------------------------------------------------------
        '
        '   21/02/08 :  Création - Version 1.00
        '
        '----------------------------------------------------------------------------------------
        '
        '   Affichage d'une combinaison réglementaire dans une Picture Box
        '
        '----------------------------------------------------------------------------------------

        '--> Déclarations

        Dim hCar As Single = MyGr.MeasureString("X", FontNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yLine1 As Single = (sHi / 2 - hCar) / 2
        Dim yLine2 As Single = yLine1 + sHi / 2
        Dim IndiceG As String
        Dim SymbolG As String = "G"         ' tabPoutres(iPoutreEnCours).Chargements(0).Symbole
        Dim SymbolQ1 As String = "Q1"       ' tabPoutres(iPoutreEnCours).Chargements(1).Symbole
        Dim SymbolQ2 As String = "Q2"       ' tabPoutres(iPoutreEnCours).Chargements(2).Symbole
        Dim lPsi0Q1, lPsi0Q2 As Boolean
        Dim sCar As Single = MyGr.MeasureString("x", FontNormal).Width / 5

        Dim MyGamma As Cls_Gamma

        MyGamma = MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma.Clone

        '--> Initialisations

        If lSelect Then
            MyGr.FillRectangle(MyBrushFond, 0, 0, sWi, sHi)
        Else
            MyGr.FillRectangle(MyBrushNoFond, 0, 0, sWi, sHi)
        End If

        '--> Tracé

        Dim xStart As Single = sWi * 0.05!
        Dim xPen As Single = xStart
        Dim xGroupe2 As Single = 0.35! * sWi
        Dim xGroupe3 As Single = 0.67! * sWi

        Dim xG As Single = 0.22! * sWi
        Dim xQ1 As Single = xG + 0.32! * sWi
        Dim xQ2 As Single = xQ1 + 0.32! * sWi
        Dim xPlus1 As Single = xG + 0.08! * sWi
        Dim xPlus2 As Single = xQ1 + 0.08! * sWi

        Dim strGammaG, strCoefQ1, strCoefQ2 As String

        Const kAdjust As Single = 0.5!

        Select Case Indice
            Case 1
                IndiceG = "G.Sup"
                lPsi0Q1 = False
                lPsi0Q2 = True
                strGammaG = Format(MyGamma.GammaG_sup, "0.00")
                strCoefQ1 = Format(MyGamma.GammaQ, "0.00")
                strCoefQ2 = Format(MyGamma.GammaQ * MyGamma.Psi0_Q2, "0.00")
            Case 2
                IndiceG = "G.Sup"
                lPsi0Q1 = True
                lPsi0Q2 = False
                strGammaG = Format(MyGamma.GammaG_sup, "0.00")
                strCoefQ1 = Format(MyGamma.GammaQ * MyGamma.Psi0_Q1, "0.00")
                strCoefQ2 = Format(MyGamma.GammaQ, "0.00")
            Case 3
                IndiceG = "G.Inf"
                lPsi0Q1 = False
                lPsi0Q2 = True
                strGammaG = Format(MyGamma.GammaG_inf, "0.00")
                strCoefQ1 = Format(MyGamma.GammaQ, "0.00")
                strCoefQ2 = Format(MyGamma.GammaQ * MyGamma.Psi0_Q2, "0.00")
            Case 4
                IndiceG = "G.Inf"
                lPsi0Q1 = True
                lPsi0Q2 = False
                strGammaG = Format(MyGamma.GammaG_inf, "0.00")
                strCoefQ1 = Format(MyGamma.GammaQ * MyGamma.Psi0_Q1, "0.00")
                strCoefQ2 = Format(MyGamma.GammaQ, "0.00")
        End Select

        If lSelect Then
            BrushBlue = MyBrushBlue
            BrushBlack = MyBrush
        Else
            BrushBlue = MyBrushUnSelected
            BrushBlack = MyBrushUnSelected
        End If

        '===============================================================================
        '   PREMIERE LIGNE
        '===============================================================================

        '--> GammaG G

        DrawSymbol(MyGr, BrushBlue, "g", IndiceG, xG - sCar, yLine1, hIndice, True, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        DrawSymbol(MyGr, BrushBlack, SymbolG, "", xG, yLine1, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

        '--> Premier Plus

        MyGr.DrawString("+", FontNormal, BrushBlack, xPlus1, yLine1)

        '--> GammaQ Psi Q1

        If lPsi0Q1 Then
            DrawSymbol(MyGr, BrushBlue, "y", "0", xQ1 - LongueurChaine(MyGr, "g", "Q", True, FontNormal, FontSymbol, FontIndice, kAdjust) - 2 * sCar, yLine1, hIndice, True, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        End If
        DrawSymbol(MyGr, BrushBlue, "g", "Q", xQ1 - sCar, yLine1, hIndice, True, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        DrawSymbol(MyGr, BrushBlack, SymbolQ1, "", xQ1, yLine1, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

        '--> Deuxième Plus

        MyGr.DrawString("+", FontNormal, BrushBlack, xPlus2, yLine1)

        '--> GammaQ Psi Q2

        If lPsi0Q2 Then
            DrawSymbol(MyGr, BrushBlue, "y", "0", xQ2 - LongueurChaine(MyGr, "g", "Q", True, FontNormal, FontSymbol, FontIndice, kAdjust) - 2 * sCar, yLine1, hIndice, True, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        End If
        DrawSymbol(MyGr, BrushBlue, "g", "Q", xQ2 - sCar, yLine1, hIndice, True, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        DrawSymbol(MyGr, BrushBlack, SymbolQ2, "", xQ2, yLine1, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

        '===============================================================================
        '   DEUXIEME LIGNE
        '===============================================================================

        DrawSymbol(MyGr, BrushBlue, strGammaG, "", xG - sCar, yLine2, hIndice, False, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        DrawSymbol(MyGr, BrushBlack, SymbolG, "", xG, yLine2, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

        '--> Premier Plus

        MyGr.DrawString("+", FontNormal, BrushBlack, xPlus1, yLine2)

        '--> GammaQ Psi Q1

        DrawSymbol(MyGr, BrushBlue, strCoefQ1, "", xQ1 - sCar, yLine2, hIndice, False, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        DrawSymbol(MyGr, BrushBlack, SymbolQ1, "", xQ1, yLine2, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

        '--> Deuxième Plus

        MyGr.DrawString("+", FontNormal, BrushBlack, xPlus2, yLine2)

        '--> GammaQ Psi Q2

        DrawSymbol(MyGr, BrushBlue, strCoefQ2, "", xQ2 - sCar, yLine2, hIndice, False, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        DrawSymbol(MyGr, BrushBlack, SymbolQ2, "", xQ2, yLine2, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

    End Sub

#End Region


End Class