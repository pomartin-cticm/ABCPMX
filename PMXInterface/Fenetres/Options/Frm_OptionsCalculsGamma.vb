Public Class Frm_OptionsCalculsGamma

#Region " Variables et constantes "

    Const BALISE As String = "OPTCALGAMMA"

    Const formatGAMMA As String = "0.00"
    Dim lBuild As Boolean

    Dim x_img_GammaCVSP As Decimal
    Dim x_txt_GammaCVSP As Decimal

    Dim y_txt_GammaVs As Decimal
    Dim y_txt_GammaVc As Decimal
    Dim y_txt_GammaS As Decimal
    Dim y_txt_GammaP As Decimal

    Dim y_decal As Decimal

#End Region

#Region "===OUVERTURE==="
    Private Sub Frm_OptionsCalculsGamma_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub InitialiseFrm()
        lBuild = True
        GestionLangue(Frm_OptionsCalcul.BlocLangues(BALISE))
        GestionStyle()
        GestionUnites()
        AfficherGammaEnCours()
        MAJI_GammaV_Unique()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Loads.Text = MyBloc("LOADS")
            Me.lbl_Combination.Text = MyBloc("COMBINATION")
            Me.lbl_Materials.Text = MyBloc("MATERIALS")

            Me.lbl_Acier.Text = MyBloc("STEEL")
            Me.lbl_Beton.Text = MyBloc("CONCRETE")
            Me.lbl_Fire.Text = MyBloc("FIRE")
            Me.chk_GammaV_Unique.Text = MyBloc("GAMMAV")

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_Gamma.Dock = DockStyle.Fill

        Me.lbl_Loads.BackColor = CouleurBackBandeaux
        Me.lbl_Loads.ForeColor = CouleurForeBandeaux

        Me.lbl_Combination.BackColor = CouleurBackBandeaux
        Me.lbl_Combination.ForeColor = CouleurForeBandeaux

        Me.lbl_Materials.BackColor = CouleurBackBandeaux
        Me.lbl_Materials.ForeColor = CouleurForeBandeaux

        y_txt_GammaVs = 85

        y_decal = 26

        x_img_GammaCVSP = 2
        x_txt_GammaCVSP = 48

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub AfficherGammaEnCours()

        Me.chk_GammaV_Unique.Checked = Frm_OptionsCalcul.GammaLoc.lGammaV_unique

        '--> Actions
        Me.txt_GammaGsup.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaG_sup, formatGAMMA)
        Me.txt_GammaGinf.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaG_inf, formatGAMMA)
        Me.txt_GammaQ.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaQ, formatGAMMA)

        '--> Combination factors
        Me.txt_Psi0_Q1.Text = Format(Frm_OptionsCalcul.GammaLoc.Psi0_Q1, formatGAMMA)
        Me.txt_Psi1_Q1.Text = Format(Frm_OptionsCalcul.GammaLoc.Psi1_Q1, formatGAMMA)
        Me.txt_Psi2_Q1.Text = Format(Frm_OptionsCalcul.GammaLoc.Psi2_Q1, formatGAMMA)

        Me.txt_Psi0_Q2.Text = Format(Frm_OptionsCalcul.GammaLoc.Psi0_Q2, formatGAMMA)
        Me.txt_Psi1_Q2.Text = Format(Frm_OptionsCalcul.GammaLoc.Psi1_Q2, formatGAMMA)
        Me.txt_Psi2_Q2.Text = Format(Frm_OptionsCalcul.GammaLoc.Psi2_Q2, formatGAMMA)

        '-->Acier
        Me.txt_GammaM0.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaM0, formatGAMMA)
        Me.txt_GammaM1.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaM1, formatGAMMA)
        Me.txt_GammaM2.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaM2, formatGAMMA)

        '--> Béton

        Me.txt_GammaC.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaC, formatGAMMA)
        Me.txt_GammaVs.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaVs, formatGAMMA)
        Me.txt_GammaVc.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaVc, formatGAMMA)
        Me.txt_GammaS.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaS, formatGAMMA)
        Me.txt_GammaP.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaP, formatGAMMA)

        '--> Incendie
        Me.txt_GammaM_fi.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaM_fi, formatGAMMA)
        Me.txt_GammaC_fi.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaC_fi, formatGAMMA)
        Me.txt_GammaV_fi.Text = Format(Frm_OptionsCalcul.GammaLoc.GammaV_fi, formatGAMMA)

    End Sub


#End Region

#Region " Dessins Symboles "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_GammaGsup.Paint, img_GammaGinf.Paint, img_GammaQ.Paint, img_Psi2.Paint, img_Psi1.Paint, img_Psi0.Paint, img_Q1.Paint, img_Q2.Paint, img_GammaVs.Paint, img_GammaV_fi.Paint, img_GammaVc.Paint, img_GammaS.Paint, img_GammaP.Paint, img_GammaM2.Paint, img_GammaM1.Paint, img_GammaM0.Paint, img_GammaM_fi.Paint, img_GammaC_fi.Paint, img_GammaC.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height
        'Dim xStart As Single = sWI * 0.95

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lIndice, lEgal As Boolean
        'Dim xPen As Single = xStart
        'Dim hCar As Single = e.Graphics.MeasureString("X", FontSymbolNormal).Height
        'Dim hIndice As Single = hCar / 2
        'Dim yPen As Single = (sHI / 2 - hCar) / 2 + sHI * 0.15

        '--> Initialisation

        lIndice = False
        lGrec = True
        lEgal = True

        Select Case sender.name

            Case Me.img_GammaGsup.Name

                strSymbol = "g"
                strIndice = "G,sup"

            Case Me.img_GammaGinf.Name

                strSymbol = "g"
                strIndice = "G,inf"

            Case Me.img_GammaQ.Name

                strSymbol = "g"
                strIndice = "Q"

            Case Me.img_Psi0.Name

                strSymbol = "y"
                strIndice = "0"

            Case Me.img_Psi1.Name

                strSymbol = "y"
                strIndice = "1"

            Case Me.img_Psi2.Name

                strSymbol = "y"
                strIndice = "2"

            Case Me.img_Q1.Name

                strSymbol = "Q"
                strIndice = "1"
                lGrec = False
                lEgal = False

            Case Me.img_Q2.Name

                strSymbol = "Q"
                strIndice = "2"
                lGrec = False
                lEgal = False


            Case Me.img_GammaM0.Name

                strSymbol = "g"
                strIndice = "M0"

            Case Me.img_GammaM1.Name

                strSymbol = "g"
                strIndice = "M1"

            Case Me.img_GammaM2.Name

                strSymbol = "g"
                strIndice = "M2"

            Case Me.img_GammaC.Name

                strSymbol = "g"
                strIndice = "C"

            Case Me.img_GammaVs.Name

                strSymbol = "g"
                strIndice = "Vs"

            Case Me.img_GammaVc.Name

                strSymbol = "g"
                strIndice = "Vc"

            Case Me.img_GammaS.Name

                strSymbol = "g"
                strIndice = "S"

            Case Me.img_GammaP.Name

                strSymbol = "g"
                strIndice = "P"

            Case Me.img_GammaM_fi.Name

                strSymbol = "g"
                strIndice = "M,fi"

            Case Me.img_GammaC_fi.Name

                strSymbol = "g"
                strIndice = "C,fi"

            Case Me.img_GammaV_fi.Name

                strSymbol = "g"
                strIndice = "V,fi"

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, Enu_AlignementH.Gauche,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Evènements saisie "
    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles txt_GammaGsup.TextChanged, txt_GammaGinf.TextChanged, txt_GammaQ.TextChanged, txt_Psi0_Q1.TextChanged, txt_Psi1_Q1.TextChanged, txt_Psi2_Q1.TextChanged, txt_GammaM0.TextChanged, txt_GammaM1.TextChanged, txt_GammaM2.TextChanged, txt_GammaC.TextChanged, txt_GammaVc.TextChanged, txt_GammaVs.TextChanged, txt_GammaS.TextChanged, txt_GammaP.TextChanged, txt_GammaM_fi.TextChanged, txt_GammaC_fi.TextChanged, txt_GammaV_fi.TextChanged, txt_Psi0_Q2.TextChanged, txt_Psi2_Q2.TextChanged, txt_Psi1_Q2.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Select Case sender.name
                Case txt_GammaGsup.Name
                    Frm_OptionsCalcul.GammaLoc.GammaG_sup = ValeurUI
                Case txt_GammaGinf.Name
                    Frm_OptionsCalcul.GammaLoc.GammaG_inf = ValeurUI
                Case txt_GammaQ.Name
                    Frm_OptionsCalcul.GammaLoc.GammaQ = ValeurUI
                Case txt_Psi0_Q1.Name
                    Frm_OptionsCalcul.GammaLoc.Psi0_Q1 = ValeurUI
                Case txt_Psi1_Q1.Name
                    Frm_OptionsCalcul.GammaLoc.Psi1_Q1 = ValeurUI
                Case txt_Psi2_Q1.Name
                    Frm_OptionsCalcul.GammaLoc.Psi2_Q1 = ValeurUI
                Case txt_Psi0_Q2.Name
                    Frm_OptionsCalcul.GammaLoc.Psi0_Q2 = ValeurUI
                Case txt_Psi1_Q2.Name
                    Frm_OptionsCalcul.GammaLoc.Psi1_Q2 = ValeurUI
                Case txt_Psi2_Q2.Name
                    Frm_OptionsCalcul.GammaLoc.Psi2_Q2 = ValeurUI
                Case txt_GammaM0.Name
                    Frm_OptionsCalcul.GammaLoc.GammaM0 = ValeurUI
                Case txt_GammaM1.Name
                    Frm_OptionsCalcul.GammaLoc.GammaM1 = ValeurUI
                Case txt_GammaM2.Name
                    Frm_OptionsCalcul.GammaLoc.GammaM2 = ValeurUI
                Case txt_GammaC.Name
                    Frm_OptionsCalcul.GammaLoc.GammaC = ValeurUI
                Case txt_GammaVs.Name
                    Frm_OptionsCalcul.GammaLoc.GammaVs = ValeurUI
                Case txt_GammaVc.Name
                    Frm_OptionsCalcul.GammaLoc.GammaVc = ValeurUI
                Case txt_GammaS.Name
                    Frm_OptionsCalcul.GammaLoc.GammaS = ValeurUI
                Case txt_GammaP.Name
                    Frm_OptionsCalcul.GammaLoc.GammaP = ValeurUI
                Case txt_GammaM_fi.Name
                    Frm_OptionsCalcul.GammaLoc.GammaM_fi = ValeurUI
                Case txt_GammaC_fi.Name
                    Frm_OptionsCalcul.GammaLoc.GammaC_fi = ValeurUI
                Case txt_GammaV_fi.Name
                    Frm_OptionsCalcul.GammaLoc.GammaV_fi = ValeurUI
            End Select

        End If

    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

        Select Case MyTxt.Name
            Case Me.txt_GammaGsup.Name, Me.txt_GammaGinf.Name, Me.txt_GammaQ.Name
                ValMin = GAMMA_ACTION_MIN
                ValMax = GAMMA_ACTION_MAX

            Case Me.txt_Psi0_Q1.Name, Me.txt_Psi1_Q1.Name, Me.txt_Psi2_Q1.Name, Me.txt_Psi0_Q2.Name, Me.txt_Psi1_Q2.Name, Me.txt_Psi2_Q2.Name
                ValMin = PSI_COMBINAISON_MIN
                ValMax = PSI_COMBINAISON_MAX

            Case Me.txt_GammaM0.Name, Me.txt_GammaM1.Name, Me.txt_GammaM2.Name, Me.txt_GammaC.Name, Me.txt_GammaVc.Name, Me.txt_GammaVs.Name, Me.txt_GammaS.Name, Me.txt_GammaP.Name, Me.txt_GammaM_fi.Name, Me.txt_GammaC_fi.Name, Me.txt_GammaV_fi.Name
                ValMin = GAMMA_RESISTANCE_MIN
                ValMax = GAMMA_RESISTANCE_MAX
        End Select

        iErreur = ValideSaisieNombre(MyTxt.Text, True, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, ValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            'ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

    Private Sub MAJI_GammaV_Unique()
        If Me.chk_GammaV_Unique.Checked Then
            y_txt_GammaS = y_txt_GammaVs + y_decal
            y_txt_GammaP = y_txt_GammaS + y_decal
            y_txt_GammaVc = y_txt_GammaP + y_decal

        Else

            y_txt_GammaVc = y_txt_GammaVs + y_decal
            y_txt_GammaS = y_txt_GammaVc + y_decal
            y_txt_GammaP = y_txt_GammaS + y_decal
        End If

        Me.txt_GammaVs.Visible = True
        Me.img_GammaVs.Visible = True
        Me.txt_GammaVc.Visible = Not Me.chk_GammaV_Unique.Checked
        Me.img_GammaVc.Visible = Not Me.chk_GammaV_Unique.Checked

        Me.img_GammaVs.Location = New Point(x_img_GammaCVSP, y_txt_GammaVs)
        Me.img_GammaVc.Location = New Point(x_img_GammaCVSP, y_txt_GammaVc)
        Me.img_GammaS.Location = New Point(x_img_GammaCVSP, y_txt_GammaS)
        Me.img_GammaP.Location = New Point(x_img_GammaCVSP, y_txt_GammaP)

        Me.txt_GammaVs.Location = New Point(x_txt_GammaCVSP, y_txt_GammaVs)
        Me.txt_GammaVc.Location = New Point(x_txt_GammaCVSP, y_txt_GammaVc)
        Me.txt_GammaS.Location = New Point(x_txt_GammaCVSP, y_txt_GammaS)
        Me.txt_GammaP.Location = New Point(x_txt_GammaCVSP, y_txt_GammaP)
    End Sub

    Private Sub chk_GammaV_Unique_CheckedChanged(sender As Object, e As EventArgs) Handles chk_GammaV_Unique.CheckedChanged
        If lBuild Then Exit Sub

        Frm_OptionsCalcul.GammaLoc.lGammaV_unique = chk_GammaV_Unique.Checked

        MAJI_GammaV_Unique()
    End Sub


#End Region

End Class