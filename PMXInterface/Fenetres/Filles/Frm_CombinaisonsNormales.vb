Imports PMXMoteur2
Imports System.IO

Public Class Frm_CombinaisonsNormales

#Region " Variables "
    Dim lBuild As Boolean

    Dim SymbolG As String = "G"         ' tabPoutres(iPoutreEnCours).Chargements(0).Symbole
    Dim SymbolQ1 As String = "Q1"       ' tabPoutres(iPoutreEnCours).Chargements(1).Symbole
    Dim SymbolQ2 As String = "Q2"       ' tabPoutres(iPoutreEnCours).Chargements(2).Symbole

    Dim str_Combinaison, strNb As String
    Dim strELU, strELS, strELF As String

    Const BALISE As String = "FRM_COMBINATIONS1"

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

    Public Sub InitialiserFenetre(lComb() As Boolean, CoefCombi() As List(Of Decimal), nbCombi As Integer, nbPredef As Integer, nbCustom As Integer)
        '--------------------------------------------------------------------------------------------------------------------
        '   lComb       [E] :   Table des combinaisons sélectionnées
        '   CoefCombi   [E] :   Coefficients des combinaisons
        '   nbCombi     [E] :   Nombre de combinaisons, au total
        '   nbPredef    [E] :   Nombre de combinaisons prédéfinies
        '   nbCustom    [E] :   Nombre de combinaisons personnalisées
        '--------------------------------------------------------------------------------------------------------------------

        lBuild = True

        GestionLangues(Frm_Combinaisons.BlocLangues(BALISE))
        PrepareFenetre(nbPredef, nbCustom)
        GestionStyle()
        MAJI_TypeEL()
        AfficheCombinaisonEnCours(lComb, CoefCombi, nbCombi, nbPredef, nbCustom)
        MAJI_Equations()

        lBuild = False
    End Sub

    Private Sub GestionLangues(ByVal MyBloc As Dictionary(Of String, String))
        If File.Exists(LogicielFichiers.Langue) Then
            Try

                '=== COMBINAISONS

                Me.lbl_Predefinies.Text = MyBloc("PREDEFINED")
                Me.lbl_Custom.Text = MyBloc("CUSTOM")

                '=== Textes

                str_Combinaison = MyBloc("COMBINATION")
                strELU = MyBloc("ULS")
                strELS = MyBloc("SLS")
                strELF = MyBloc("FLS")
                strNb = MyBloc("NUMBER")

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues")
                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
            End Try
        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If


    End Sub

    Private Sub PrepareFenetre(nbPredef As Integer, nbCustom As Integer)
        Me.chk_Combinaison04.Visible = (nbPredef >= 4)
        Me.chk_Combinaison03.Visible = (nbPredef >= 3)
        Me.chk_Combinaison02.Visible = (nbPredef >= 2)
        Me.img_Combinaison04.Visible = (nbPredef >= 4)
        Me.img_Combinaison03.Visible = (nbPredef >= 3)
        Me.img_Combinaison02.Visible = (nbPredef >= 2)

        Select Case AffichageEL
            Case Enu_AffichageEL.ELF : Ind_Custom = 3
            Case Enu_AffichageEL.ELS : Ind_Custom = 4
            Case Enu_AffichageEL.ELU : Ind_Custom = 4
        End Select

        '--[ Préparation des étiquettes de combinaison Custom

        Me.etq_Custom01_G.Text = SymbolG & " +"
        Me.etq_Custom01_Q1.Text = SymbolQ1 & " +"
        Me.etq_Custom01_Q2.Text = SymbolQ2

        Me.etq_Custom02_G.Text = SymbolG & " +"
        Me.etq_Custom02_Q1.Text = SymbolQ1 & " +"
        Me.etq_Custom02_Q2.Text = SymbolQ2

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon
        Me.lbl_Predefinies.BackColor = CouleurBackBandeaux
        Me.lbl_Predefinies.ForeColor = CouleurForeBandeaux
        Me.lbl_Custom.BackColor = CouleurBackBandeaux
        Me.lbl_Custom.ForeColor = CouleurForeBandeaux
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
        MAJI_CadreEquation(Me.chk_Combinaison03, Me.img_Combinaison03)
        MAJI_CadreEquation(Me.chk_Combinaison04, Me.img_Combinaison04)
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
        'Dim Chaine As String = str_Combinaison
        Dim ChaineEL As String = ""

        Select Case AffichageEL
            Case Enu_AffichageEL.ELF : ChaineEL = strELF
            Case Enu_AffichageEL.ELS : ChaineEL = strELS
            Case Enu_AffichageEL.ELU : ChaineEL = strELU
        End Select

        'Chaine = str_Combinaison & " " & ChaineEL & " " & strNb & " "

        Me.chk_Combinaison01.Text = str_Combinaison & " " & cls_Poutre.SymboleCombi(ChaineEL, 0)                'Chaine & "1"
        Me.chk_Combinaison02.Text = str_Combinaison & " " & cls_Poutre.SymboleCombi(ChaineEL, 1)                'Chaine & "2"
        Me.chk_Combinaison03.Text = str_Combinaison & " " & cls_Poutre.SymboleCombi(ChaineEL, 2)                'Chaine & "3"
        Me.chk_Combinaison04.Text = str_Combinaison & " " & cls_Poutre.SymboleCombi(ChaineEL, 3)                'Chaine & "4"

        Me.chk_CombiCustom01.Text = str_Combinaison & " " & cls_Poutre.SymboleCombi(ChaineEL, Ind_Custom + 0)   ' Chaine & CStr(Ind_Custom + 1)
        Me.chk_CombiCustom02.Text = str_Combinaison & " " & cls_Poutre.SymboleCombi(ChaineEL, Ind_Custom + 1)   ' Chaine & CStr(Ind_Custom + 2)

    End Sub

    Private Sub AfficheCombinaisonEnCours(lComb() As Boolean, CoefCombi() As List(Of Decimal), nbCombi As Integer, nbPredef As Integer, nbCustom As Integer)
        '--------------------------------------------------------------------------------------------------------------------
        '   lComb       [E] :   Table des combinaisons sélectionnées
        '   CoefCombi   [E] :   Coefficients des combinaisons
        '   nbCombi     [E] :   Nombre de combinaisons, au total
        '   nbPredef    [E] :   Nombre de combinaisons prédéfinies
        '   nbCustom    [E] :   Nombre de combinaisons personnalisées
        '--------------------------------------------------------------------------------------------------------------------

        '--> Combinaisons prédéfinies

        If nbPredef >= 1 Then
            Me.chk_Combinaison01.Checked = lComb(0)
        End If
        If nbPredef >= 2 Then
            Me.chk_Combinaison02.Checked = lComb(1)
        End If
        If nbPredef >= 3 Then
            Me.chk_Combinaison03.Checked = lComb(2)
        End If
        If nbPredef >= 4 Then
            Me.chk_Combinaison04.Checked = lComb(3)
        End If

        '--> Combinaisons utilisateurs

        If nbCustom >= 1 Then
            Me.chk_CombiCustom01.Checked = lComb(Ind_Custom)
        End If

        If nbCustom >= 2 Then
            Me.chk_CombiCustom02.Checked = lComb(Ind_Custom + 1)
        End If

        '--> Coefficients pour les combinaisons utilisateurs

        Me.txt_Custom01_G.Text = Format(CoefCombi(Ind_Custom)(0), fmtGamma)
        Me.txt_Custom01_Q1.Text = Format(CoefCombi(Ind_Custom)(1), fmtGamma)
        Me.txt_Custom01_Q2.Text = Format(CoefCombi(Ind_Custom)(2), fmtGamma)

        Me.txt_Custom02_G.Text = Format(CoefCombi(Ind_Custom + 1)(0), fmtGamma)
        Me.txt_Custom02_Q1.Text = Format(CoefCombi(Ind_Custom + 1)(1), fmtGamma)
        Me.txt_Custom02_Q2.Text = Format(CoefCombi(Ind_Custom + 1)(2), fmtGamma)

    End Sub

#End Region

#Region " Evènements "

    Private Sub GestionSaisionChkCombi(sender As Object, e As EventArgs) Handles chk_Combinaison04.CheckedChanged, chk_Combinaison03.CheckedChanged, chk_Combinaison02.CheckedChanged, chk_Combinaison01.CheckedChanged, chk_CombiCustom02.CheckedChanged, chk_CombiCustom01.CheckedChanged
        If lBuild Then Exit Sub
        Dim Indice As Integer
        Dim lEtat As Boolean

        Select Case sender.name
            Case Me.chk_Combinaison01.Name : Indice = 0 : lEtat = Me.chk_Combinaison01.Checked
            Case Me.chk_Combinaison02.Name : Indice = 1 : lEtat = Me.chk_Combinaison02.Checked
            Case Me.chk_Combinaison03.Name : Indice = 2 : lEtat = Me.chk_Combinaison03.Checked
            Case Me.chk_Combinaison04.Name : Indice = 3 : lEtat = Me.chk_Combinaison04.Checked
            Case Me.chk_CombiCustom01.Name : Indice = Ind_Custom : lEtat = Me.chk_CombiCustom01.Checked
            Case Me.chk_CombiCustom02.Name : Indice = Ind_Custom + 1 : lEtat = Me.chk_CombiCustom02.Checked
        End Select

        Frm_Combinaisons.ModifieSelectionCombi(Indice, lEtat)
        MAJI_Equations()

    End Sub

    Private Sub GestionTextChanged(sender As Object, e As EventArgs) Handles txt_Custom02_Q2.TextChanged, txt_Custom02_Q1.TextChanged, txt_Custom02_G.TextChanged, txt_Custom01_Q2.TextChanged, txt_Custom01_Q1.TextChanged, txt_Custom01_G.TextChanged
        If lBuild Then Exit Sub
        lBuild = True
        Dim Valeur As Decimal
        Dim IndCombi, IndVar As Integer

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name

                Case Me.txt_Custom01_G.Name, Me.txt_Custom01_Q1.Name, Me.txt_Custom01_Q2.Name
                    IndCombi = Ind_Custom
                Case Me.txt_Custom02_G.Name, Me.txt_Custom02_Q1.Name, Me.txt_Custom02_Q2.Name
                    IndCombi = Ind_Custom + 1

            End Select

            Select Case sender.name

                Case Me.txt_Custom01_G.Name, Me.txt_Custom02_G.Name
                    IndVar = 0
                Case Me.txt_Custom01_Q1.Name, Me.txt_Custom02_Q1.Name
                    IndVar = 1
                Case Me.txt_Custom01_Q2.Name, Me.txt_Custom02_Q2.Name
                    IndVar = 2

            End Select

            Frm_Combinaisons.ModifieValeurCoefCombi(IndCombi, IndVar, Valeur)

        End If

        lBuild = False
    End Sub


    ''' <summary>
    ''' Vérification de la saisie des paramètres
    ''' </summary>
    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        '-- Déclaration - Initialisation

        Dim lOk As Boolean = True
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = 1

        Const GammaMAXI As Decimal = 10
        Const GammaMINI As Decimal = 0

        ValMin = GammaMINI
        ValMax = GammaMAXI
        lValMax = True

        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, lValMin, ValMax, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            ErrorProvider.Clear()
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

    Private Sub AffichageEquations(sender As Object, e As PaintEventArgs) Handles img_Combinaison04.Paint, img_Combinaison03.Paint, img_Combinaison02.Paint, img_Combinaison01.Paint

        Dim pWi, pHi As Single
        Dim Indice As Integer
        Dim lSelect As Boolean

        pWi = Me.img_Combinaison01.ClientRectangle.Width
        pHi = Me.img_Combinaison01.ClientRectangle.Height

        Select Case sender.name
            Case Me.img_Combinaison01.Name : Indice = 1 : lSelect = Me.chk_Combinaison01.Checked
            Case Me.img_Combinaison02.Name : Indice = 2 : lSelect = Me.chk_Combinaison02.Checked
            Case Me.img_Combinaison03.Name : Indice = 3 : lSelect = Me.chk_Combinaison03.Checked
            Case Me.img_Combinaison04.Name : Indice = 4 : lSelect = Me.chk_Combinaison04.Checked
        End Select
        'lSelect = False

        Select Case AffichageEL
            Case Enu_AffichageEL.ELU
                DrawEquationELU(e.Graphics, lSelect, Indice, pWi, pHi)
            Case Enu_AffichageEL.ELS
                DrawEquationELS(e.Graphics, lSelect, Indice, pWi, pHi)
            Case Enu_AffichageEL.ELF
                DrawEquationFeu(e.Graphics, lSelect, Indice, pWi, pHi)
        End Select

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
        Dim lPsi0Q1, lPsi0Q2 As Boolean
        Dim sCar As Single = MyGr.MeasureString("x", FontNormal).Width / 5

        If lSelect Then
            MyGr.FillRectangle(MyBrushFond, 0, 0, sWi, sHi)
        Else
            MyGr.FillRectangle(MyBrushNoFond, 0, 0, sWi, sHi)
        End If


        Dim MyGamma As cls_Gamma

        MyGamma = MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma.Clone

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

        With MyProjet.Poutres(MyProjet.IndEnCours)
            strGammaG = Format(.CoefCombELU(Indice - 1)(0), "0.00")
            strCoefQ1 = Format(.CoefCombELU(Indice - 1)(1), "0.00")
            strCoefQ2 = Format(.CoefCombELU(Indice - 1)(2), "0.00")
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
        Dim lPsi0Q1, lPsi0Q2 As Boolean
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
        Dim xQ1 As Single = xG + 0.32! * sWi
        Dim xQ2 As Single = xQ1 + 0.32! * sWi
        Dim xPlus1 As Single = xG + 0.08! * sWi
        Dim xPlus2 As Single = xQ1 + 0.08! * sWi

        Dim strGammaG As String = ""
        Dim strCoefQ1 As String = ""
        Dim strCoefQ2 As String = ""
        Dim lQ1, lQ2 As Boolean

        Const kAdjust As Single = 0.5!

        strGammaG = Format(1, "0.00")
        Select Case Indice
            Case 1
                lPsi0Q1 = False
                lPsi0Q2 = False
                lQ1 = True
                lQ2 = False
                strCoefQ1 = Format(1, "0.00")
                strCoefQ2 = ""
            Case 2
                lPsi0Q1 = False
                lPsi0Q2 = True
                lQ1 = True
                lQ2 = True
                strCoefQ1 = Format(1, "0.00")
                strCoefQ2 = Format(MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma.Psi0_Q2, "0.00")
            Case 3
                lPsi0Q1 = False
                lPsi0Q2 = False
                lQ1 = False
                lQ2 = True
                strCoefQ1 = ""
                strCoefQ2 = Format(1, "0.00")
            Case 4
                lPsi0Q1 = True
                lPsi0Q2 = False
                lQ1 = True
                lQ2 = True
                strCoefQ1 = Format(MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma.Psi0_Q1, "0.00")
                strCoefQ2 = Format(1, "0.00")
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

        If lQ1 Then
            If lPsi0Q1 Then
                DrawSymbol(MyGr, BrushBlue, "y", "0", xQ1 - sCar, yLine1, hIndice, True, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
            End If
            DrawSymbol(MyGr, BrushBlack, SymbolQ1, "", xQ1, yLine1, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        End If

        '--> Deuxième Plus

        If lQ1 And lQ2 Then MyGr.DrawString("+", FontNormal, BrushBlack, xPlus2, yLine1)

        '--> GammaQ Psi Q2

        If lQ2 Then
            Dim xQ As Single = xQ1
            If lQ1 Then xQ = xQ2
            If lPsi0Q2 Then
                DrawSymbol(MyGr, BrushBlue, "y", "0", xQ - 2 * sCar, yLine1, hIndice, True, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
            End If
            DrawSymbol(MyGr, BrushBlack, SymbolQ2, "", xQ, yLine1, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        End If

        '===============================================================================
        '   DEUXIEME LIGNE
        '===============================================================================

        DrawSymbol(MyGr, BrushBlack, SymbolG, "", xG, yLine2, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

        '--> Premier Plus

        MyGr.DrawString("+", FontNormal, BrushBlack, xPlus1, yLine2)

        '--> GammaQ Psi Q1

        If lQ1 Then
            DrawSymbol(MyGr, BrushBlue, strCoefQ1, "", xQ1 - sCar, yLine2, hIndice, False, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
            DrawSymbol(MyGr, BrushBlack, SymbolQ1, "", xQ1, yLine2, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        End If

        '--> Deuxième Plus

        If lQ1 And lQ2 Then MyGr.DrawString("+", FontNormal, BrushBlack, xPlus2, yLine2)

        '--> GammaQ Psi Q2
        If lQ2 Then
            Dim xQ As Single = xQ1
            If lQ1 Then xQ = xQ2

            DrawSymbol(MyGr, BrushBlue, strCoefQ2, "", xQ - sCar, yLine2, hIndice, False, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
            DrawSymbol(MyGr, BrushBlack, SymbolQ2, "", xQ, yLine2, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        End If

    End Sub

    Private Sub DrawEquationFeu(ByVal MyGr As Graphics, ByVal lSelect As Boolean, ByVal Indice As Integer,
                                ByVal sWi As Single, ByVal sHi As Single)
        '----------------------------------------------------------------------------------------
        '
        '   21/02/08 :  Création - Version 1.00
        '
        '----------------------------------------------------------------------------------------
        '
        '   Affichage d'une combinaison réglementaire dans une Picture Box
        '   Combinaison réglementaire Feu
        '
        '----------------------------------------------------------------------------------------


        Dim hCar As Single = MyGr.MeasureString("X", FontNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yLine1 As Single = (sHi / 2 - hCar) / 2
        Dim yLine2 As Single = yLine1 + sHi / 2
        Dim IndiceQ1 As String = ""
        Dim IndiceQ2 As String = ""

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
        Dim xQ1 As Single = xG + 0.32! * sWi
        Dim xQ2 As Single = xQ1 + 0.32! * sWi
        Dim xPlus1 As Single = xG + 0.08! * sWi
        Dim xPlus2 As Single = xQ1 + 0.08! * sWi

        Dim strCoefQ2 As String = ""
        Dim strCoefQ1 As String = ""

        Const kAdjust As Single = 0.5!

        Select Case Indice
            Case 1
                IndiceQ1 = "1,1"
                IndiceQ2 = "2,2"
                ' strCoefQ1 = Format(tabPoutres(iPoutreEnCours).Chargements(1).Psi1, "0.00")
                'strCoefQ2 = Format(tabPoutres(iPoutreEnCours).Chargements(2).Psi2, "0.00")
                strCoefQ1 = Format(MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma.Psi1_Q1, "0.00")
                strCoefQ2 = Format(MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma.Psi2_Q2, "0.00")

            Case 2
                IndiceQ1 = "2,1"
                IndiceQ2 = "2,2"
                'strCoefQ1 = Format(tabPoutres(iPoutreEnCours).Chargements(1).Psi2, "0.00")
                'strCoefQ2 = Format(tabPoutres(iPoutreEnCours).Chargements(2).Psi2, "0.00")
                strCoefQ1 = Format(MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma.Psi2_Q1, "0.00")
                strCoefQ2 = Format(MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma.Psi2_Q2, "0.00")
            Case 3
                IndiceQ1 = "2,1"
                IndiceQ2 = "1,2"
                'strCoefQ1 = Format(tabPoutres(iPoutreEnCours).Chargements(1).Psi2, "0.00")
                'strCoefQ2 = Format(tabPoutres(iPoutreEnCours).Chargements(2).Psi1, "0.00")
                strCoefQ1 = Format(MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma.Psi2_Q1, "0.00")
                strCoefQ2 = Format(MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma.Psi1_Q2, "0.00")

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

        DrawSymbol(MyGr, BrushBlack, SymbolG, "", xG, yLine1, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

        '--> Premier Plus

        MyGr.DrawString("+", FontNormal, BrushBlack, xPlus1, yLine1)

        '--> GammaQ Psi Q1

        DrawSymbol(MyGr, BrushBlue, "y", IndiceQ1, xQ1 - 2 * sCar, yLine1, hIndice, True, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        DrawSymbol(MyGr, BrushBlack, SymbolQ1, "", xQ1, yLine1, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

        '--> Deuxième Plus

        MyGr.DrawString("+", FontNormal, BrushBlack, xPlus2, yLine1)

        '--> GammaQ Psi Q2

        DrawSymbol(MyGr, BrushBlue, "y", IndiceQ2, xQ2 - 2 * sCar, yLine1, hIndice, True, Enu_AlignementH.Droite, FontNormal, FontSymbol, FontIndice, kAdjust, False)
        DrawSymbol(MyGr, BrushBlack, SymbolQ2, "", xQ2, yLine1, 0, False, Enu_AlignementH.Gauche, FontNormal, FontSymbol, FontIndice, kAdjust, False)

        '===============================================================================
        '   DEUXIEME LIGNE
        '===============================================================================

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
        Me.etq_Custom01_Q1.BackColor = colorLabel
        Me.etq_Custom01_Q2.BackColor = colorLabel

        If Me.chk_CombiCustom02.Checked Then
            colorLabel = ColorSelect
        Else
            colorLabel = SystemColors.ControlLightLight
        End If
        Me.etq_Custom02_G.BackColor = colorLabel
        Me.etq_Custom02_Q1.BackColor = colorLabel
        Me.etq_Custom02_Q2.BackColor = colorLabel
    End Sub

#End Region

End Class