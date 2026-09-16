Imports PMXMoteur2

Public Class Frm_MaintienBacN_Tole

#Region " Variables "

    Dim lBuild As Boolean
    Dim strTransitionOptions(2) As String

    Dim strInfoW_ToleK As String

    Dim EntraxeD As Decimal
    Dim lOpenRib As Boolean

    Dim lSelectInfoK As Boolean = False

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(Bloc As Dictionary(Of String, String))

        lBuild = True

        GestionLangues(Bloc)
        GestionStyle()
        GestionUnites()
        PrepareFenetre()
        AffichePoutreEnCours()

        lBuild = False

    End Sub

    Private Sub GestionStyle()

        Me.pan_Main.Dock = DockStyle.Fill

        Me.lbl_Panneau.BackColor = CouleurBackBandeaux
        Me.lbl_Panneau.ForeColor = CouleurForeBandeaux
        Me.lbl_ToleK.BackColor = CouleurBackBandeaux
        Me.lbl_ToleK.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(Me.txt_SheetLength, False)
        PrepareTextBoxDipo(Me.txt_SheetWidth, False)

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitL3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitD1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitF1.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)
        Me.etq_UnitK2.Text = ""

    End Sub

    Private Sub GestionLangues(Bloc As Dictionary(Of String, String))

        Try

            '=== PANNEAU ELEMENTAIRE ==========================================================

            Me.lbl_Panneau.Text = Bloc("INDIVIDUALSHEET")
            Me.lbl_NbSpans.Text = Bloc("NUMBERSPANS")
            Me.lbl_IndSheetDimensions.Text = Bloc("DIMENSIONS")
            Me.lbl_SheetLength.Text = Bloc("SHEETLENGTH")
            Me.lbl_SheetWidth.Text = Bloc("SHEETWIDTH")

            Me.lbl_Tpr.Text = Bloc("THCOATING")
            Me.lbl_Fu.Text = Bloc("FUP")

            Me.lbl_ToleK.Text = Bloc("DISTORSIONFLEX")
            Me.lbl_Explication_1.Text = Bloc("REINTRANTRIBSW1")
            Me.lbl_Explication_2.Text = Bloc("REINTRANTRIBSW2")

            strInfoW_ToleK = Bloc("INFOW_TOLEK")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally

        End Try

    End Sub

    Private Sub PrepareFenetre()
        EntraxeD = MyProjet.Poutres(MyProjet.IndEnCours).EntraxeSolive
        lOpenRib = MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.lNervuresOuvertes

        If lOpenRib Then

            'Me.TLPan_Gauche.RowStyles(3).Height = 0
            Me.TLPan_Gauche.RowStyles(2).Height = 0

            Me.pan_ToleK.Visible = False

        Else
            'Me.TLPan_Gauche.RowStyles(3).Height = 0
            Me.TLPan_Gauche.RowStyles(2).Height = 30

            Me.pan_ToleK.Visible = True

        End If


        RemplirCmbNbSpans()
    End Sub

    Private Sub RemplirCmbNbSpans()
        Me.cmb_NbSpan.Items.Clear()
        For i As Integer = 1 To 5
            Me.cmb_NbSpan.Items.Add(CStr(i))
        Next
    End Sub

    Private Sub AffichePoutreEnCours()

        Me.cmb_NbSpan.SelectedIndex = Frm_MaintienBacN.localMaitienBac.m - 1

        Me.txt_Tpr.Text = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.Tpr, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
        Me.txt_Fup.Text = GetStringInUnitN(Frm_MaintienBacN.localFup, Enu_TypeVariable.Contrainte, 4, 3, NON_U, True)

        MAJI_DimensionsPanneau()

        If Not lOpenRib Then
            Me.txt_ToleK.Text = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.KUser, Enu_TypeVariable.SansType, 4, 3, NON_U, True)
        End If

    End Sub
    Private Sub MAJI_DimensionsPanneau()

        Me.txt_SheetLength.Text = GetStringInUnit(Frm_MaintienBacN.localMaitienBac.LongueurPanneau(EntraxeD), Enu_TypeVariable.Longueur, 4, 2, False)
        Me.txt_SheetWidth.Text = GetStringInUnit(MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.LargeurModule, Enu_TypeVariable.Longueur, 4, 2, False)

    End Sub

#End Region

#Region "   Evenements "

    Private Sub txt_ToleK_TextChanged(sender As Object, e As EventArgs) Handles txt_ToleK.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then
            Frm_MaintienBacN.localMaitienBac.KUser = Valeur

            MAJI_Calculs()

        End If
    End Sub

    Private Sub txt_Tpr_TextChanged(sender As Object, e As EventArgs) Handles txt_Tpr.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then
            Frm_MaintienBacN.localMaitienBac.Tpr = Valeur

            MAJI_Calculs()

        End If
    End Sub

    Private Sub txt_Fup_TextChanged(sender As Object, e As EventArgs) Handles txt_Fup.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then
            Frm_MaintienBacN.localMaitienBac.Tpr = Valeur

            MAJI_Calculs()

        End If
    End Sub

    Private Sub cmb_NbSpan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_NbSpan.SelectedIndexChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then
            Frm_MaintienBacN.localMaitienBac.nt = CInt(Valeur)

            MAJI_Dessin()
        End If
    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean
        '----------------------------------------------------------------------------------------------------
        '   18/12/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------
        '   Vérification de la saisie des textbox
        '----------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------

        '-- Déclaration - Initialisation

        Dim lOk As Boolean = True
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Select Case MyTxt.Name

            Case Me.txt_ToleK.Name
                ValMin = 0
                ValMax = 10
                lValMax = True
                kUnit = 1

            Case Me.txt_Tpr.Name

                ValMin = 0
                ValMax = (MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Tp / 10) / kUnit
                lValMax = True

            Case Me.txt_Fup.Name

                kUnit = LogicielInfo.Transfert_Contraintes(LogicielOptions.IndUnitContraintes)

                ValMin = 1
                ValMax = 1000 / kUnit
                lValMax = True
                kUnit = LogicielInfo.Transfert_Contraintes(LogicielOptions.IndUnitContraintes)

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

#End Region

#Region "   Fonctions MAJI "

    Private Sub MAJI_Calculs()
        Frm_MaintienBacN.MAJI_Calculs()
    End Sub

    Private Sub MAJI_Dessin()

        Frm_MaintienBacN.MAJI_Dessin()

    End Sub

#End Region

#Region " Infos W "

    Private Sub img_InfoK_MouseEnter(sender As Object, e As EventArgs) Handles img_InfoK.MouseEnter
        lSelectInfok = True
        Me.img_InfoK.Invalidate()
    End Sub

    Private Sub img_InfoK_MouseLeave(sender As Object, e As EventArgs) Handles img_InfoK.MouseLeave
        lSelectInfok = False
        Me.img_InfoK.Invalidate()
    End Sub

    Private Sub img_InfoK_Click(sender As Object, e As EventArgs) Handles img_InfoK.Click
        PublieInfoToleK()
    End Sub

    Private Sub img_Icone_1_Paint(sender As Object, e As PaintEventArgs) Handles img_InfoK.Paint

        DessineIconeInfo(e.Graphics, Me.img_InfoK, lSelectInfok)

    End Sub


    Private Sub img_info_Click(sender As Object, e As EventArgs) Handles img_info.Click
        'If InfoW_lVisible Then
        '    InfoW_Fermer()
        'Else
        PublieInfoToleK()
        'End If
    End Sub

    Private Sub PublieInfoToleK()

        InfoW.InitialiseInfo()
        InfoW.AddInfo(strInfoW_ToleK)
        'InfoW_Add("Le degré de connexion est calculé à mi-travée de la poutre, en supposant que la poutre en entièrement sous moment positif")

        InfoW.Publie()

    End Sub

#End Region

#Region " Symboles "

    Private Sub PaintSymbols(sender As Object, e As PaintEventArgs) _
        Handles img_K.Paint, img_ap.Paint, img_bp.Paint, img_Tpr.Paint, img_Fup.Paint

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

            Case Me.img_K.Name
                strSymbol = "K"
                strIndice = ""

            Case Me.img_ap.Name
                strSymbol = "a"
                strIndice = "p"

            Case Me.img_bp.Name
                strSymbol = "b"
                strIndice = "p"

            Case Me.img_m.Name
                strSymbol = "m"
                strIndice = ""

            Case Me.img_Tpr.Name
                strSymbol = "t"
                strIndice = "pr"

            Case Me.img_Fup.Name
                strSymbol = "f"
                strIndice = "up"

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

End Class