Imports PMXMoteur2
Imports System.IO

Public Class Frm_ModularRatio

#Region " Attributs "

    Dim lBuild As Boolean
    Dim tabPsiL() As Decimal = {0, 0.55, 1.1, 1.5}

    Dim AgeT As Integer = 50 * 365
    Dim AgeT0 As Integer = 28

    Dim MonBeton As New cls_Beton

    Dim RayonH0 As Decimal = 0.2

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_ModularRatio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True
        GestionLangues()
        GestionStyle()
        GestionUnites()
        InitialiserFenetre()
        IntialiseVariables()
        MAJI_ProprietesBeton()
        MAJI_Coefficients()
        lBuild = False
    End Sub

    Private Sub IntialiseVariables()

        MonBeton.lLeger = False
        MonBeton.Classe = cls_Beton.TabClasseBeton(0)

    End Sub

    Private Sub InitialiserFenetre()
        RemplirComboRH()
        RemplirComboBeton()
        RemplirComboPsiL()

        Me.txt_AgeT.Text = GetStringInUnit(AgeT, Enu_TypeVariable.SansType, 2, 0, False)
        Me.txt_AgeT0.Text = GetStringInUnit(AgeT0, Enu_TypeVariable.SansType, 2, 0, False)

        Me.txt_H0.Text = GetStringInUnit(RayonH0, Enu_TypeVariable.Dimension, 3, 2, False)

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitDim.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitModule.Text = "GPa"
        Me.etq_UnitSigma1.Text = "MPa"
        Me.etq_UnitSigma2.Text = "MPa"
        Me.etq_UnitSigma3.Text = "MPa"

    End Sub

    Private Sub RemplirComboRH()

        Me.cmb_RH.Items.Clear()

        For i As Integer = 0 To cls_OptionsCalcul.tabRH.GetUpperBound(0)

            Me.cmb_RH.Items.Add(GetStringInUnit(cls_OptionsCalcul.tabRH(i), Enu_TypeVariable.SansType, 2, 0, False) & "%")

        Next

        Me.cmb_RH.SelectedIndex = 0

    End Sub

    Private Sub RemplirComboPsiL()

        Me.cmb_PsiL.Items.Clear()

        For i As Integer = 0 To Me.tabPsiL.GetUpperBound(0)

            Me.cmb_PsiL.Items.Add(GetStringInUnit(Me.tabPsiL(i), Enu_TypeVariable.SansType, 2, 2, False))

        Next

        Me.cmb_PsiL.SelectedIndex = 0

    End Sub

    Private Sub RemplirComboBeton()

        Me.cmb_ClasseBeton.Items.Clear()

        Me.cmb_ClasseBeton.Items.AddRange(cls_Beton.TabClasseBeton)

        Me.cmb_ClasseBeton.SelectedIndex = 0

    End Sub

    Private Sub GestionLangues()

        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_MODULARRATIOTOOL")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")

                Me.btn_Annuler.Text = Bloc("CANCEL")
                Me.btn_OK.Text = Bloc("CLOSE")

                Me.lbl_Parameters.Text = Bloc("PARAMETERS")
                Me.lbl_Beton.Text = Bloc("CONCRETE")
                Me.lbl_PsiL.Text = Bloc("PSIL")
                Me.lbl_RelativeRH.Text = Bloc("RELATIVEHUMIDITY")
                Me.lbl_AgeT.Text = Bloc("AGET")
                Me.lbl_AgeT0.Text = Bloc("AGET0")
                Me.lbl_DimensionH0.Text = Bloc("NOTIONALSIZE")

                Me.lbl_PropBeton.Text = Bloc("CONCRETEPROP")
                Me.lbl_ResistanceCompression.Text = Bloc("RCOMPRESSION")
                Me.lbl_ResistanceTraction.Text = Bloc("RTENSION")

                Me.lbl_Resultats.Text = Bloc("RESULTS")
                Me.lbl_CoefLongTerme.Text = Bloc("LONGTERM_N")
                Me.lbl_CoefCourtTerme.Text = Bloc("SHORTTERM_N")

                Me.lbl_CoefficientAnnexB.Text = Bloc("COEFANNEXB")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, "Frm_ModularRatio/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.TLPan_PartieBasse.ColumnStyles(1).Width = 0
        Me.TLPan_PartieBasse.ColumnStyles(2).Width = 0

        Me.lbl_Parameters.BackColor = CouleurBackBandeaux
        Me.lbl_Parameters.ForeColor = CouleurForeBandeaux

        Me.lbl_PropBeton.BackColor = CouleurBackBandeaux
        Me.lbl_PropBeton.ForeColor = CouleurForeBandeaux

        Me.lbl_Resultats.BackColor = CouleurBackBandeaux
        Me.lbl_Resultats.ForeColor = CouleurForeBandeaux

        Me.lbl_CoefficientAnnexB.BackColor = CouleurBackBandeaux
        Me.lbl_CoefficientAnnexB.ForeColor = CouleurForeBandeaux


        PrepareTextBoxDipo(Me.txt_Ecm, False)
        PrepareTextBoxDipo(Me.txt_Fck, False)
        PrepareTextBoxDipo(Me.txt_Fcm, False)
        PrepareTextBoxDipo(Me.txt_Fctm, False)

        PrepareTextBoxDipo(Me.txt_BetaC, False)
        PrepareTextBoxDipo(Me.txt_BetaFcm, False)
        PrepareTextBoxDipo(Me.txt_BetaT0, False)
        PrepareTextBoxDipo(Me.txt_Phi0, False)
        PrepareTextBoxDipo(Me.txt_PhiRH, False)
        PrepareTextBoxDipo(Me.txt_PhiT, False)

        PrepareTextBoxDipo(Me.txt_n0, False)
        PrepareTextBoxDipo(Me.txt_nL, False)

    End Sub

#End Region

#Region " FERMETURE "

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub

#End Region

#Region " Mise à jour de la fenêtre"

    Private Sub MAJI_ProprietesBeton()
        '--------------------------------------------------------------------------------------------------
        '   20/11/23 :  Création - POM
        '--------------------------------------------------------------------------------------------------
        '   Mise à joure des propriétés du béton
        '--------------------------------------------------------------------------------------------------

        MonBeton.Calcul_Proprietes()

        Me.txt_Ecm.Text = GetStringInUnit(MonBeton.Ecm, Enu_TypeVariable.ContrainteGPa, 4, 3, False)
        Me.txt_Fck.Text = GetStringInUnit(MonBeton.Fck, Enu_TypeVariable.ContrainteMPa, 3, 2, False)
        Me.txt_Fcm.Text = GetStringInUnit(MonBeton.Fcm, Enu_TypeVariable.ContrainteMPa, 3, 2, False)
        Me.txt_Fctm.Text = GetStringInUnit(MonBeton.Fctm, Enu_TypeVariable.ContrainteMPa, 3, 2, False)

    End Sub

    Private Sub MAJI_Coefficients()

        Dim RH As Decimal = cls_OptionsCalcul.tabRH(Me.cmb_RH.SelectedIndex)
        Dim PsiL As Decimal = tabPsiL(Me.cmb_PsiL.SelectedIndex)
        Dim n0 As Decimal = MonBeton.CoefficientEquivalenceCT
        Dim nL As Decimal = MonBeton.CoefficientEquivalence(rh, RayonH0, AgeT, AgeT0, psil)

        Me.txt_n0.Text = GetStringInUnit(n0, Enu_TypeVariable.SansType, 3, 2, False)
        Me.txt_nL.Text = GetStringInUnit(nL, Enu_TypeVariable.SansType, 3, 2, False)

        Dim PhiRH As Decimal = MonBeton.PhiRH(RH, RayonH0)

        Me.txt_PhiRH.Text = GetStringInUnit(PhiRH, Enu_TypeVariable.SansType, 3, 2, False)

        Dim BetaFcm As Decimal = MonBeton.BetaFcm

        Me.txt_BetaFcm.Text = GetStringInUnit(BetaFcm, Enu_TypeVariable.SansType, 3, 2, False)

        Dim BetaT0 As Decimal = MonBeton.Beta_t0(AgeT0)

        Me.txt_BetaT0.Text = GetStringInUnit(BetaT0, Enu_TypeVariable.SansType, 3, 2, False)

        Dim Phi0 As Decimal = PhiRH * BetaFcm * BetaT0

        Me.txt_Phi0.Text = GetStringInUnit(Phi0, Enu_TypeVariable.SansType, 3, 2, False)

        Dim BetaC As Decimal = MonBeton.BetaC_tt0(RH, RayonH0, AgeT, AgeT0)

        Me.txt_BetaC.Text = GetStringInUnit(BetaC, Enu_TypeVariable.SansType, 3, 2, False)

        Dim PhiT As Decimal = Phi0 * BetaC

        Me.txt_PhiT.Text = GetStringInUnit(PhiT, Enu_TypeVariable.SansType, 3, 2, False)

    End Sub

#End Region

#Region " Evènements "

    Private Sub cmb_ClasseBeton_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ClasseBeton.SelectedIndexChanged

        If lBuild Then Exit Sub
        MonBeton.Classe = cls_Beton.TabClasseBeton(Me.cmb_ClasseBeton.SelectedIndex)
        MAJI_ProprietesBeton()
        MAJI_Coefficients()

    End Sub

    Private Sub cmb_RH_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_RH.SelectedIndexChanged

        If lBuild Then Exit Sub
        MAJI_Coefficients()

    End Sub

    Private Sub cmb_PsiL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_PsiL.SelectedIndexChanged

        If lBuild Then Exit Sub
        MAJI_Coefficients()

    End Sub

    Private Sub txt_Age_TextChanged(sender As Object, e As EventArgs) Handles txt_AgeT.TextChanged, txt_AgeT0.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name
                Case Me.txt_AgeT.Name
                    AgeT = Valeur
                Case Me.txt_AgeT0.Name
                    AgeT0 = Valeur
            End Select
        End If
        MAJI_Coefficients()
    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        '-- Déclaration - Initialisation

        Dim lOk As Boolean = True
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Select Case MyTxt.Name

            Case Me.txt_AgeT.Name
                ValMin = AgeT0 + 1
                lValMax = False

            Case Me.txt_AgeT0.Name
                ValMin = 1
                ValMax = AgeT - 1
                lValMax = True

            Case Me.txt_H0.Name
                ValMin = 0.02 / kUnit
                lValMax = False
        End Select

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

    Private Sub txt_H0_TextChanged(sender As Object, e As EventArgs) Handles txt_H0.TextChanged

        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name
                Case Me.txt_H0.Name
                    RayonH0 = Valeur

            End Select
        End If

        MAJI_Coefficients()
    End Sub


#End Region


#Region " Dessin des symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_PsiL.Paint, img_RH.Paint, img_H0.Paint, img_AgeT0.Paint, img_AgeT.Paint, img_Fctm.Paint, img_Fcm.Paint, img_Fck.Paint, img_Ecm.Paint, img_nL.Paint, img_n0.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height
        Dim xStart As Single = sWI * 0.95

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lIndice, lEgal As Boolean
        Dim xPen As Single = xStart
        Dim hCar As Single = e.Graphics.MeasureString("X", FontSymbolNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yPen As Single = (sHI / 2 - hCar) / 2 + sHI * 0.15
        Dim AlignH As Enu_AlignementH = Enu_AlignementH.Droite

        '--> Initialisation

        lIndice = False
        lGrec = False
        lEgal = True
        Select Case sender.name

            Case Me.img_RH.Name
                strSymbol = "RH"
                strIndice = ""

            Case Me.img_PsiL.Name
                strSymbol = "y"
                strIndice = "L"
                lGrec = True

            Case Me.img_AgeT.Name
                strSymbol = "t"
                strIndice = ""

            Case Me.img_AgeT0.Name
                strSymbol = "t"
                strIndice = "0"

            Case Me.img_H0.Name
                strSymbol = "h"
                strIndice = "0"

            Case Me.img_Ecm.Name
                strSymbol = "E"
                strIndice = "cm"

            Case Me.img_Fck.Name
                strSymbol = "f"
                strIndice = "ck"

            Case Me.img_Fcm.Name
                strSymbol = "f"
                strIndice = "cm"

            Case Me.img_Fctm.Name
                strSymbol = "f"
                strIndice = "ctm"

            Case Me.img_n0.Name
                strSymbol = "n"
                strIndice = "0"

            Case Me.img_nL.Name
                strSymbol = "n"
                strIndice = "L"


        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub


#End Region


#Region " Dessin des expressions (symboles) "

    Private Sub PaintExpression(sender As Object, e As PaintEventArgs) Handles img_PhiRH.Paint

        Dim myFormul As String = ""
        Dim MyFontNormal As New Font(Me.txt_AgeT.Font.Name, Me.txt_AgeT.Font.Size)
        ' myFormul = "σ\-w\- = B\-ω\- / I\-w\- [MPa]"
        myFormul = "\Sj\s\-RH\= ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_PhiRH.ClientRectangle.Width, Me.img_PhiRH.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub img_BetaFcm_Paint(sender As Object, e As PaintEventArgs) Handles img_BetaFcm.Paint
        Dim MyFontNormal As New Font(Me.txt_AgeT.Font.Name, Me.txt_AgeT.Font.Size)
        ' myFormul = "σ\-w\- = B\-ω\- / I\-w\- [MPa]"
        Dim myFormul As String = "\Sb\s(" & strItal & "f\i\-cm\=) ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaFcm.ClientRectangle.Width, Me.img_BetaFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub img_PhiT_Paint(sender As Object, e As PaintEventArgs) Handles img_PhiT.Paint
        Dim MyFontNormal As New Font(Me.txt_AgeT.Font.Name, Me.txt_AgeT.Font.Size)
        ' myFormul = "σ\-w\- = B\-ω\- / I\-w\- [MPa]"
        Dim myFormul As String = "\Sj\s(" & strItal & "t\i, " & strItal & "t\i\-0\=) ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaFcm.ClientRectangle.Width, Me.img_BetaFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub imgBetaC_Paint(sender As Object, e As PaintEventArgs) Handles imgBetaC.Paint
        Dim MyFontNormal As New Font(Me.txt_AgeT.Font.Name, Me.txt_AgeT.Font.Size)
        ' myFormul = "σ\-w\- = B\-ω\- / I\-w\- [MPa]"
        Dim myFormul As String = "\Sb\s\-c\=(" & strItal & "t\i, " & strItal & "t\i\-0\=) ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaFcm.ClientRectangle.Width, Me.img_BetaFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub img_Phi0_Paint(sender As Object, e As PaintEventArgs) Handles img_Phi0.Paint
        Dim MyFontNormal As New Font(Me.txt_AgeT.Font.Name, Me.txt_AgeT.Font.Size)
        ' myFormul = "σ\-w\- = B\-ω\- / I\-w\- [MPa]"
        Dim myFormul As String = "\Sj\s\-0\= ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaFcm.ClientRectangle.Width, Me.img_BetaFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub img_BetaT0_Paint(sender As Object, e As PaintEventArgs) Handles img_BetaT0.Paint
        Dim MyFontNormal As New Font(Me.txt_AgeT.Font.Name, Me.txt_AgeT.Font.Size)
        ' myFormul = "σ\-w\- = B\-ω\- / I\-w\- [MPa]"
        Dim myFormul As String = "\Sb\s(" & strItal & "t\i\-0\=) ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaFcm.ClientRectangle.Width, Me.img_BetaFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Const strItal As String = "\i"
#End Region

End Class