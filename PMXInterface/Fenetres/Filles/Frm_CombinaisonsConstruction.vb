Imports PMXMoteur2
Imports System.IO

Public Class Frm_CombinaisonsConstruction

#Region " Variables "
    Dim lBuild As Boolean

    Dim SymbolG As String = "g"
    Dim SymbolQc As String = "Qc"

    Dim str_Combinaison, strNb As String
    Dim strELU, strELS As String

    Const BALISE As String = "FRM_COMBINATIONS2"

    '-- Paramètres pour l'affichage des combinaisons prédéfinies
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

    Dim Ind_Custom As Integer
    Dim fmtGamma As String = "0.00"

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiserFenetre(lCombELU() As Boolean, CoefCombiELU() As List(Of Decimal), nbCombiELU As Integer, nbPredefELU As Integer, nbCustomELU As Integer,
                                  lCombELS() As Boolean, CoefCombiELS() As List(Of Decimal), nbCombiELS As Integer, nbPredefELS As Integer, nbCustomELS As Integer)
        '--------------------------------------------------------------------------------------------------------------------
        '   lComb       [E] :   Table des combinaisons sélectionnées
        '   CoefCombi   [E] :   Coefficients des combinaisons
        '   nbCombi     [E] :   Nombre de combinaisons, au total
        '   nbPredef    [E] :   Nombre de combinaisons prédéfinies
        '   nbCustom    [E] :   Nombre de combinaisons personnalisées
        '--------------------------------------------------------------------------------------------------------------------

        lBuild = True

        GestionLangues(Frm_Combinaisons.BlocLangues(BALISE))
        PrepareFenetre()
        GestionStyle()
        MAJI_TypeEL()
        AfficheCombinaisonEnCours(lCombELU, CoefCombiELU, nbCombiELU, nbPredefELU, nbCustomELU,
                                    lCombELS, CoefCombiELS, nbCombiELS, nbPredefELS, nbCustomELS)
        MAJI_Equations()

        lBuild = False
    End Sub

    Private Sub GestionLangues(ByVal MyBloc As Dictionary(Of String, String))

        Try

            '=== COMBINAISONS

            Me.lbl_ELU.Text = MyBloc("ULSTATES")
            Me.lbl_ELS.Text = MyBloc("SLSTATES")

            '=== Textes

            str_Combinaison = MyBloc("COMBINATION")
            strELU = MyBloc("ULS") & "-C"
            strELS = MyBloc("SLS") & "-C"
            strNb = MyBloc("NUMBER")


        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try

    End Sub

    Private Sub PrepareFenetre()

        '--[ Préparation des étiquettes de combinaison Custom

        Me.etq_Custom01_G.Text = SymbolG & " +"
        Me.etq_Custom01_Qc.Text = SymbolQc

        Me.etq_Custom02_G.Text = SymbolG & " +"
        Me.etq_Custom02_Qc.Text = SymbolQc

        Dim Chaine As String = str_Combinaison

        Chaine = str_Combinaison & " "

        Ind_Custom = 1

        Me.chk_Combinaison01.Text = Chaine & cls_Poutre.SymboleCombi(strELU, 0)             ' & strELU & " " & strNb & "1"
        Me.chk_Combinaison02.Text = Chaine & cls_Poutre.SymboleCombi(strELS, 0)             ' & strELS & " " & strNb & "1"

        Me.chk_CombiCustom01.Text = Chaine & cls_Poutre.SymboleCombi(strELU, 1)             ' & strELU & " " & strNb & "2"
        Me.chk_CombiCustom02.Text = Chaine & cls_Poutre.SymboleCombi(strELU, 1)             ' & strELS & " " & strNb & "2"


    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon
        Me.lbl_ELU.BackColor = CouleurBackBandeaux
        Me.lbl_ELU.ForeColor = CouleurForeBandeaux
        Me.lbl_ELS.BackColor = CouleurBackBandeaux
        Me.lbl_ELS.ForeColor = CouleurForeBandeaux

        MyBrush = New SolidBrush(Color.Black)
        MyBrushBlue = New SolidBrush(Color.Blue)
        MyBrushFond = New SolidBrush(ColorSelect)

        ColorUnSelect = Me.pan_Predefinies.BackColor
        MyBrushNoFond = New SolidBrush(ColorUnSelect)
        MyBrushUnSelected = New SolidBrush(ColorFontUnSelected)

        FontNormal = New Font(Me.chk_Combinaison01.Font.Name, 8)
        FontIndice = New Font(Me.chk_Combinaison01.Font.Name, 7)
        FontSymbol = New Font("Symbol", 9)

        Me.pan_Combinaisons.Dock = DockStyle.Fill
    End Sub

    Private Sub MAJI_Equations()

        For Each prO As Object In Me.pan_Predefinies.Controls

            If TypeOf (prO) Is PictureBox Then
                CType(prO, PictureBox).Invalidate()
            End If

        Next

        Me.img_CombiCustom01.Invalidate()
        Me.img_CombiCustom02.Invalidate()

        MAJI_CadreEquation(Me.chk_Combinaison01, Me.img_Combinaison01)
        MAJI_CadreEquation(Me.chk_Combinaison02, Me.img_Combinaison02)
        'MAJI_CadreEquation(Me.chk_Combinaison03, Me.img_Combinaison03)
        'MAJI_CadreEquation(Me.chk_Combinaison04, Me.img_Combinaison04)
        MAJI_CadreEquation(Me.chk_CombiCustom01, Me.img_CombiCustom01)
        MAJI_CadreEquation(Me.chk_CombiCustom02, Me.img_CombiCustom02)

    End Sub

    Private Sub MAJI_CadreEquation(MychkBox As CheckBox, ByRef MyImg As PictureBox)
        If MychkBox.Checked Then
            MyImg.BorderStyle = BorderStyle.FixedSingle
        Else
            MyImg.BorderStyle = BorderStyle.None
        End If

    End Sub

    Private Sub MAJI_TypeEL()

        Me.chk_Combinaison01.Text = str_Combinaison & " " & cls_Poutre.SymboleCombi(strELU, 0) ' Chaine & "1"
        Me.chk_CombiCustom01.Text = str_Combinaison & " " & cls_Poutre.SymboleCombi(strELU, 1) ' Chaine & "2"

        Me.chk_Combinaison02.Text = str_Combinaison & " " & cls_Poutre.SymboleCombi(strELS, 0) ' Chaine & "1"
        Me.chk_CombiCustom02.Text = str_Combinaison & " " & cls_Poutre.SymboleCombi(strELS, 1) ' Chaine & "2"

    End Sub


    Private Sub AfficheCombinaisonEnCours(lCombELU() As Boolean, CoefCombiELU() As List(Of Decimal), nbCombiELU As Integer, nbPredefELU As Integer, nbCustomELU As Integer,
                                          lCombELS() As Boolean, CoefCombiELS() As List(Of Decimal), nbCombiELS As Integer, nbPredefELS As Integer, nbCustomELS As Integer)
        '--------------------------------------------------------------------------------------------------------------------
        '   lComb       [E] :   Table des combinaisons sélectionnées
        '   CoefCombi   [E] :   Coefficients des combinaisons
        '   nbCombi     [E] :   Nombre de combinaisons, au total
        '   nbPredef    [E] :   Nombre de combinaisons prédéfinies
        '   nbCustom    [E] :   Nombre de combinaisons personnalisées
        '--------------------------------------------------------------------------------------------------------------------

        '--> Combinaisons prédéfinies

        If nbPredefELU >= 1 Then
            Me.chk_Combinaison01.Checked = lCombELU(0)
        End If
        If nbPredefELS >= 1 Then
            Me.chk_Combinaison02.Checked = lCombELS(0)
        End If

        '--> Combinaisons utilisateurs

        If nbCustomELU >= 1 Then
            Me.chk_CombiCustom01.Checked = lCombELU(Ind_Custom)
        End If

        If nbCustomELS >= 1 Then
            Me.chk_CombiCustom02.Checked = lCombELS(Ind_Custom)
        End If

        '--> Coefficients pour les combinaisons utilisateurs

        Me.txt_Custom01_G.Text = Format(CoefCombiELU(Ind_Custom)(4), fmtGamma)
        Me.txt_Custom01_Qc.Text = Format(CoefCombiELU(Ind_Custom)(3), fmtGamma)
        'Me.txt_Custom01_Q2.Text = Format(CoefCombi(Ind_Custom)(2), fmtGamma)

        Me.txt_Custom02_G.Text = Format(CoefCombiELS(Ind_Custom)(4), fmtGamma)
        Me.txt_Custom02_Qc.Text = Format(CoefCombiELS(Ind_Custom)(3), fmtGamma)
        'Me.txt_Custom02_Q2.Text = Format(CoefCombi(Ind_Custom + 1)(2), fmtGamma)

    End Sub

#End Region

#Region " Evènements "

    Private Sub GestionSaisionChkCombi(sender As Object, e As EventArgs) Handles chk_Combinaison02.CheckedChanged, chk_Combinaison01.CheckedChanged, chk_CombiCustom02.CheckedChanged, chk_CombiCustom01.CheckedChanged
        If lBuild Then Exit Sub
        Dim Indice As Integer
        Dim lEtat As Boolean
        Dim lConstructionELU As Boolean = False

        Select Case sender.name
            Case Me.chk_Combinaison01.Name : Indice = 0 : lEtat = Me.chk_Combinaison01.Checked : lConstructionELU = True
            Case Me.chk_Combinaison02.Name : Indice = 0 : lEtat = Me.chk_Combinaison02.Checked
            'Case Me.chk_Combinaison03.Name : Indice = 2 : lEtat = Me.chk_Combinaison03.Checked
            'Case Me.chk_Combinaison04.Name : Indice = 3 : lEtat = Me.chk_Combinaison04.Checked
            Case Me.chk_CombiCustom01.Name : Indice = Ind_Custom : lEtat = Me.chk_CombiCustom01.Checked : lConstructionELU = True
            Case Me.chk_CombiCustom02.Name : Indice = Ind_Custom : lEtat = Me.chk_CombiCustom02.Checked
        End Select

        Frm_Combinaisons.ModifieSelectionCombi(Indice, lEtat, lConstructionELU)
        MAJI_Equations()

    End Sub

    Private Sub GestionTextChanged(sender As Object, e As EventArgs) Handles txt_Custom02_Qc.TextChanged, txt_Custom02_G.TextChanged, txt_Custom01_Qc.TextChanged, txt_Custom01_G.TextChanged
        If lBuild Then Exit Sub
        lBuild = True
        Dim Valeur As Decimal
        Dim IndCombi, IndVar As Integer
        Dim lConstructionELU As Boolean = False

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name

                Case Me.txt_Custom01_G.Name, Me.txt_Custom01_Qc.Name
                    IndCombi = Ind_Custom
                    lConstructionELU = True
                Case Me.txt_Custom02_G.Name, Me.txt_Custom02_Qc.Name
                    IndCombi = Ind_Custom

            End Select

            Select Case sender.name

                Case Me.txt_Custom01_G.Name, Me.txt_Custom02_G.Name
                    IndVar = 4
                Case Me.txt_Custom01_Qc.Name, Me.txt_Custom02_Qc.Name
                    IndVar = 3

            End Select

            Frm_Combinaisons.ModifieValeurCoefCombi(IndCombi, IndVar, Valeur, lConstructionELU)

        End If

        lBuild = False
    End Sub


    ''' <summary>
    ''' Vérification de la saisie des paramètres
    ''' </summary>
    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        '-- Déclaration - Initialisation

        Dim lOk As Boolean = True
        ErrorProvider1.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = 1

        Const GammaMAXI As Decimal = 10
        Const GammaMINI As Decimal = 0

        ValMin = GammaMINI
        ValMax = GammaMAXI
        lValMax = True

        iErreur = ValideSaisieNombre(MyTxt.Text, True, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider1, ValMin, ValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            ErrorProvider1.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk

    End Function

#End Region

#Region " Affichage des combinaisons dans les picturebox "

    Private Sub AffichageFondCustom(sender As Object, e As PaintEventArgs) Handles img_CombiCustom02.Paint, img_CombiCustom01.Paint
        Dim lSelect As Boolean
        Dim pWi, pHi As Single

        pWi = Me.img_CombiCustom01.ClientRectangle.Width
        pHi = Me.img_CombiCustom01.ClientRectangle.Height

        Select Case sender.name
            Case Me.img_CombiCustom01.Name : lSelect = Me.chk_CombiCustom01.Checked
            Case Me.img_CombiCustom02.Name : lSelect = Me.chk_CombiCustom02.Checked
        End Select
        'lSelect = False
        DrawFond(e.Graphics, lSelect, pWi, pHi)
    End Sub

    Private Sub AffichageEquations(sender As Object, e As PaintEventArgs) Handles img_Combinaison02.Paint, img_Combinaison01.Paint

        Dim pWi, pHi As Single
        Dim Indice As Integer
        Dim lSelect As Boolean

        pWi = Me.img_Combinaison01.ClientRectangle.Width
        pHi = Me.img_Combinaison01.ClientRectangle.Height

        Indice = 1

        Select Case sender.name
            Case Me.img_Combinaison01.Name : lSelect = Me.chk_Combinaison01.Checked : DrawEquationELU(e.Graphics, lSelect, Indice, pWi, pHi)
            Case Me.img_Combinaison02.Name : lSelect = Me.chk_Combinaison02.Checked : DrawEquationELS(e.Graphics, lSelect, Indice, pWi, pHi)
        End Select
        'lSelect = False

    End Sub

    Private Sub DrawEquationELU(ByVal MyGr As Graphics, ByVal lSelect As Boolean, ByVal Indice As Integer,
                                ByVal sWi As Single, ByVal sHi As Single)
        '----------------------------------------------------------------------------------------
        '   21/08/23 :  Création - Version 1.00
        '----------------------------------------------------------------------------------------
        '   Affichage d'une combinaison EN1990 dans une Picture Box
        '----------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   lSelect     [E] :   Indique si combinaison sélectionnée
        '   LimitS      [E] :   Etat Limite (U, S ou F)
        '   Indice      [E] :   Indice de la combinaison prédéfinie
        '   sWI, sHI    [E] :   Dimensions de la picture box
        '----------------------------------------------------------------------------------------


        '--> Déclarations

        Dim hCar As Single = MyGr.MeasureString("X", FontNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yLine1 As Single = (sHi / 2 - hCar) / 2
        Dim yLine2 As Single = yLine1 + sHi / 2
        Dim IndiceG As String = ""
        Dim lPsi0Qc As Boolean
        Dim sCar As Single = MyGr.MeasureString("x", FontNormal).Width / 5

        Dim MyGamma As cls_Gamma

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
        Dim xQc As Single = xG + 0.32! * sWi
        'Dim xQ2 As Single = xQ1 + 0.32! * sWi
        Dim xPlus1 As Single = xG + 0.08! * sWi
        'Dim xPlus2 As Single = xQ1 + 0.08! * sWi

        Dim strGammaG, strCoefQc As String

        Const kAdjust As Single = 0.5!

        Select Case Indice
            Case 1
                IndiceG = "G.Sup"
                lPsi0Qc = False
                strGammaG = Format(MyGamma.GammaG_sup, "0.00")
                strCoefQc = Format(MyGamma.GammaQ, "0.00")
            Case 2
                IndiceG = "G.Sup"
                lPsi0Qc = True
                strGammaG = Format(MyGamma.GammaG_sup, "0.00")
                strCoefQc = Format(MyGamma.GammaQ * MyGamma.Psi0_Q1, "0.00")
            Case 3
                IndiceG = "G.Inf"
                lPsi0Qc = False
                strGammaG = Format(MyGamma.GammaG_inf, "0.00")
                strCoefQc = Format(MyGamma.GammaQ, "0.00")
            Case 4
                IndiceG = "G.Inf"
                lPsi0Qc = True
                strGammaG = Format(MyGamma.GammaG_inf, "0.00")
                strCoefQc = Format(MyGamma.GammaQ * MyGamma.Psi0_Q1, "0.00")
        End Select

        With MyProjet.Poutres(MyProjet.IndEnCours)
            strGammaG = Format(.CoefCombELU(Indice - 1)(0), "0.00")
            strCoefQc = Format(.CoefCombELU(Indice - 1)(1), "0.00")
        End With


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

        '--> GammaQ Psi Qc

        If lPsi0Qc Then
            DrawSymbol(MyGr, BrushBlue, "y", "0", xQc - LongueurChaine(MyGr, "g", "Q", True, FontNormal, FontSymbol, FontIndice, kAdjust) - 2 * sCar, yLine1, hIndice, True, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        End If
        DrawSymbol(MyGr, BrushBlue, "g", "Q", xQc - sCar, yLine1, hIndice, True, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        DrawSymbol(MyGr, BrushBlack, SymbolQc, "", xQc, yLine1, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)


        '===============================================================================
        '   DEUXIEME LIGNE
        '===============================================================================

        DrawSymbol(MyGr, BrushBlue, strGammaG, "", xG - sCar, yLine2, hIndice, False, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        DrawSymbol(MyGr, BrushBlack, SymbolG, "", xG, yLine2, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

        '--> Premier Plus

        MyGr.DrawString("+", FontNormal, BrushBlack, xPlus1, yLine2)

        '--> GammaQ Psi Q1

        DrawSymbol(MyGr, BrushBlue, strCoefQc, "", xQc - sCar, yLine2, hIndice, False, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        DrawSymbol(MyGr, BrushBlack, SymbolQc, "", xQc, yLine2, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

    End Sub

    Private Sub DrawEquationELS(ByVal MyGr As Graphics, ByVal lSelect As Boolean, ByVal Indice As Integer,
                                ByVal sWi As Single, ByVal sHi As Single)
        '----------------------------------------------------------------------------------------
        '
        '   21/02/08 :  Création - Version 1.00
        '
        '----------------------------------------------------------------------------------------
        '
        '   Affichage d'une combinaison réglementaire ELS dans une Picture Box
        '
        '----------------------------------------------------------------------------------------

        Dim hCar As Single = MyGr.MeasureString("X", FontNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yLine1 As Single = (sHi / 2 - hCar) / 2
        Dim yLine2 As Single = yLine1 + sHi / 2
        Dim lPsi0Qc As Boolean
        Dim sCar As Single = MyGr.MeasureString("x", FontNormal).Width / 5

        If lSelect Then
            MyGr.FillRectangle(MyBrushFond, 0, 0, sWi, sHi)
        Else
            MyGr.FillRectangle(MyBrushNoFond, 0, 0, sWi, sHi)
        End If

        Dim xStart As Single = sWi * 0.05!
        Dim xPen As Single = xStart
        Dim xGroupe2 As Single = 0.35! * sWi
        Dim xGroupe3 As Single = 0.67! * sWi

        Dim xG As Single = 0.22! * sWi
        Dim xQc As Single = xG + 0.32! * sWi
        'Dim xQ2 As Single = xQ1 + 0.32! * sWi
        Dim xPlus1 As Single = xG + 0.08! * sWi
        'Dim xPlus2 As Single = xQc + 0.08! * sWi

        Dim strGammaG As String = ""
        Dim strCoefQ1 As String = ""
        'Dim strCoefQ2 As String = ""
        Dim lQc As Boolean

        Const kAdjust As Single = 0.5!

        strGammaG = Format(1, "0.00")
        Select Case Indice
            Case 1
                lPsi0Qc = False
                lQc = True
                strCoefQ1 = Format(1, "0.00")
            Case 2
                lPsi0Qc = False
                lQc = True
                strCoefQ1 = Format(1, "0.00")
            Case 3
                lPsi0Qc = False
                lQc = False
                strCoefQ1 = ""
            Case 4
                lPsi0Qc = True
                lQc = True
                strCoefQ1 = Format(MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma.Psi0_Q1, "0.00")
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

        DrawSymbol(MyGr, BrushBlue, strGammaG, "", xG - sCar, yLine2, hIndice, False, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        DrawSymbol(MyGr, BrushBlack, SymbolG, "", xG, yLine1, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

        '--> Premier Plus

        MyGr.DrawString("+", FontNormal, BrushBlack, xPlus1, yLine1)

        '--> GammaQ Psi Q1

        If lQc Then
            If lPsi0Qc Then
                DrawSymbol(MyGr, BrushBlue, "y", "0", xQc - sCar, yLine1, hIndice, True, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
            End If
            DrawSymbol(MyGr, BrushBlack, SymbolQc, "", xQc, yLine1, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        End If

        '===============================================================================
        '   DEUXIEME LIGNE
        '===============================================================================

        DrawSymbol(MyGr, BrushBlack, SymbolG, "", xG, yLine2, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

        '--> Premier Plus

        MyGr.DrawString("+", FontNormal, BrushBlack, xPlus1, yLine2)

        '--> GammaQ Psi Q1

        If lQc Then
            DrawSymbol(MyGr, BrushBlue, strCoefQ1, "", xQc - sCar, yLine2, hIndice, False, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
            DrawSymbol(MyGr, BrushBlack, SymbolQc, "", xQc, yLine2, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        End If

    End Sub

    Private Sub DrawFond(ByVal MyGr As Graphics, ByVal lSelect As Boolean, ByVal sWi As Single, ByVal sHi As Single)
        If lSelect Then
            MyGr.FillRectangle(MyBrushFond, 0, 0, sWi, sHi)
        Else
            MyGr.FillRectangle(MyBrushNoFond, 0, 0, sWi, sHi)
        End If

        Dim colorLabel As Color

        If Me.chk_CombiCustom01.Checked Then
            colorLabel = ColorSelect
        Else
            colorLabel = SystemColors.ControlLightLight
        End If
        Me.etq_Custom01_G.BackColor = colorLabel
        Me.etq_Custom01_Qc.BackColor = colorLabel

        If Me.chk_CombiCustom02.Checked Then
            colorLabel = ColorSelect
        Else
            colorLabel = SystemColors.ControlLightLight
        End If
        Me.etq_Custom02_G.BackColor = colorLabel
        Me.etq_Custom02_Qc.BackColor = colorLabel

    End Sub

#End Region

End Class