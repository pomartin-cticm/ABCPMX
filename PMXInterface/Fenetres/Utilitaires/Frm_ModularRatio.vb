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

                Me.lbl_Beton.Text = Bloc("CONCRETE")
                Me.lbl_PsiL.Text = Bloc("PSIL")
                Me.lbl_RelativeRH.Text = Bloc("RELATIVEHUMIDITY")
                Me.lbl_AgeT.Text = Bloc("AGET")
                Me.lbl_AgeT0.Text = Bloc("AGET0")

                Me.lbl_PropBeton.Text = Bloc("CONCRETEPROP")

                Me.lbl_Resultats.Text = Bloc("RESULTS")

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

        PrepareTextBoxDipo(Me.txt_Ecm, False)
        PrepareTextBoxDipo(Me.txt_Fck, False)
        PrepareTextBoxDipo(Me.txt_Fcm, False)
        PrepareTextBoxDipo(Me.txt_Fctm, False)

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
        Me.txt_Fck.Text = GetStringInUnit(MonBeton.Fck, Enu_TypeVariable.ContrainteMPa, 3, 0, False)
        Me.txt_Fcm.Text = GetStringInUnit(MonBeton.Fcm, Enu_TypeVariable.ContrainteMPa, 3, 0, False)
        Me.txt_Fctm.Text = GetStringInUnit(MonBeton.Fctm, Enu_TypeVariable.ContrainteMPa, 3, 0, False)

    End Sub

    Private Sub MAJI_Coefficients()

        Dim RH As Decimal = cls_OptionsCalcul.tabRH(Me.cmb_RH.SelectedIndex)
        Dim PsiL As Decimal = tabPsiL(Me.cmb_PsiL.SelectedIndex)
        Dim n0 As Decimal = MonBeton.CoefficientEquivalenceCT
        Dim nL As Decimal = MonBeton.CoefficientEquivalence(rh, RayonH0, AgeT, AgeT0, psil)

        Me.txt_n0.Text = GetStringInUnit(n0, Enu_TypeVariable.SansType, 3, 2, False)
        Me.txt_nL.Text = GetStringInUnit(nL, Enu_TypeVariable.SansType, 3, 2, False)

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

    End Sub

    Private Sub txt_H0_TextChanged(sender As Object, e As EventArgs) Handles txt_H0.TextChanged

        If lBuild Then Exit Sub

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

End Class