Imports PMXMoteur2
Imports System.IO

Public Class Frm_Gamma

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim MyPoutreLoc As New cls_Poutre()

    'Permet la gestion de l'activation ou non du checkbox

    Dim x_img_GammaCVSP As Decimal
    Dim x_txt_GammaCVSP As Decimal

    Dim y_txt_GammaC As Decimal
    Dim y_txt_GammaVs As Decimal
    Dim y_txt_GammaVc As Decimal
    Dim y_txt_GammaS As Decimal
    Dim y_txt_GammaP As Decimal
    Dim y_btn_ReiniReistanceDalle As Decimal

    Dim y_decal As Decimal

    'Dispose de tous les textbox dans une seule liste (utile pour la gestion des erreurs)
    Dim list_txtbox As New List(Of TextBox)

    'Indique si aucune erreur n'a été constaté et permet de valider ou non la fenetre
    Dim lFrm_Valide As Boolean = True

    Const formatGAMMA As String = "0.00"

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Gamma_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        lBuild = True
        InitialiserVariables()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        AfficherPoutreEnCours()
        lBuild = False
    End Sub
    Private Sub InitialiserVariables()

        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)

        x_img_GammaCVSP = 44
        x_txt_GammaCVSP = 89

        y_txt_GammaC = 39
        y_txt_GammaVs = 65
        y_txt_GammaVc = 153
        'y_txt_GammaVp = 179
        y_txt_GammaS = 91
        y_txt_GammaP = 118

        y_decal = Math.Abs(y_txt_GammaVs - y_txt_GammaS)

        list_txtbox.Add(Me.txt_GammaGsup)
        list_txtbox.Add(txt_GammaGinf)
        list_txtbox.Add(txt_GammaQ)
        list_txtbox.Add(txt_Psi0_Q1)
        list_txtbox.Add(txt_Psi1_Q1)
        list_txtbox.Add(txt_Psi2_Q1)
        list_txtbox.Add(txt_Psi0_Q2)
        list_txtbox.Add(txt_Psi1_Q2)
        list_txtbox.Add(txt_Psi2_Q2)
        list_txtbox.Add(txt_GammaM0)
        list_txtbox.Add(txt_GammaM1)
        list_txtbox.Add(txt_GammaM2)
        list_txtbox.Add(txt_GammaC)
        list_txtbox.Add(txt_GammaVs)
        list_txtbox.Add(txt_GammaVc)
        'list_txtbox.Add((txt_GammaVp, Not MyPoutreLoc.Param.Gamma.lGammaV_unique))
        list_txtbox.Add(txt_GammaS)
        list_txtbox.Add(txt_GammaP)
        list_txtbox.Add(txt_GammaM_fi_a)
        list_txtbox.Add(txt_GammaC_fi)
        list_txtbox.Add(txt_GammaM_fi_s)
        list_txtbox.Add(txt_GammaV_fi)

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_GAMMA")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== MENU PRINCIPAL ==============================================================='

                Me.lbl_Chargement.Text = Bloc("LOADING")
                Me.lbl_Accompagnement.Text = Bloc("ACCOMPAGNEMENT")
                Me.lbl_Resistance.Text = Bloc("RESISTANCE")
                Me.lbl_Q1.Text = Bloc("LBL_Q1")
                Me.lbl_Q2.Text = Bloc("LBL_Q2")
                Me.btn_Reini.Text = Bloc("REINI")
                Me.Tab_Acier.Text = Bloc("TAB_STEEL")
                Me.Tab_Dalle.Text = Bloc("TAB_SLAB")
                Me.Tab_Incendie.Text = Bloc("TAB_FIRE")
                Me.chk_GammaV_Unique.Text = Bloc("GAMMAV")
                Me.lbl_Reset.Text = Bloc("RESET")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub GestionStyle()

        'Gestion du style graphique
        Me.Icon = Frm_PMX.Icon

        Me.lbl_Chargement.BackColor = CouleurBackBandeaux
        Me.lbl_Chargement.ForeColor = CouleurForeBandeaux
        Me.lbl_Accompagnement.BackColor = CouleurBackBandeaux
        Me.lbl_Accompagnement.ForeColor = CouleurForeBandeaux
        Me.lbl_Resistance.BackColor = CouleurBackBandeaux
        Me.lbl_Resistance.ForeColor = CouleurForeBandeaux
        Me.lbl_Reset.BackColor = CouleurBackBandeaux
        Me.lbl_Reset.ForeColor = CouleurForeBandeaux

        If Not MyPoutreLoc.lMixte Then
            Me.Tab_Dalle.Visible = False
        End If

    End Sub

    Private Sub AfficherPoutreEnCours()

        With MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma

            '==POM->GUD Ne pas utiliser CDEC pour convertir en text ====================================================

            'Me.txt_GammaGsup.Text = CDec(.GammaG_sup)
            Me.txt_GammaGsup.Text = Format(.GammaG_sup, formatGAMMA)

            Me.txt_GammaGinf.Text = Format(.GammaG_inf, formatGAMMA)
            Me.txt_GammaQ.Text = Format(.GammaQ, formatGAMMA)

            Me.txt_Psi0_Q1.Text = Format(.Psi0_Q1, formatGAMMA)
            Me.txt_Psi1_Q1.Text = Format(.Psi1_Q1, formatGAMMA)
            Me.txt_Psi2_Q1.Text = Format(.Psi2_Q1, formatGAMMA)

            Me.txt_Psi0_Q2.Text = Format(.Psi0_Q2, formatGAMMA)
            Me.txt_Psi1_Q2.Text = Format(.Psi1_Q2, formatGAMMA)
            Me.txt_Psi2_Q2.Text = Format(.Psi2_Q2, formatGAMMA)

            Me.txt_GammaM0.Text = Format(.GammaM0, formatGAMMA)
            Me.txt_GammaM1.Text = Format(.GammaM1, formatGAMMA)
            Me.txt_GammaM2.Text = Format(.GammaM2, formatGAMMA)

            Me.txt_GammaC.Text = Format(.GammaC, formatGAMMA)
            Me.chk_GammaV_Unique.Checked = .lGammaV_unique
            Me.txt_GammaVs.Text = Format(.GammaVs, formatGAMMA)
            'Me.txt_GammaVp.Text = Format(.GammaVp, formatGAMMA)
            Me.txt_GammaVc.Text = Format(.GammaVc, formatGAMMA)
            Me.txt_GammaS.Text = Format(.GammaS, formatGAMMA)
            Me.txt_GammaP.Text = Format(.GammaP, formatGAMMA)

            Me.txt_GammaM_fi_a.Text = Format(.GammaM_fi_a, formatGAMMA)
            Me.txt_GammaC_fi.Text = Format(.GammaC_fi, formatGAMMA)
            Me.txt_GammaM_fi_s.Text = Format(.GammaM_fi_s, formatGAMMA)
            Me.txt_GammaV_fi.Text = Format(.GammaV_fi, formatGAMMA)

        End With

        MAJI_GammaV_Unique()


    End Sub

#End Region

#Region "===FERMETURE==="

    Public Sub TraitementSaisie()

    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Dim lModif As Boolean = False
        If ValideSaisieFenetre() Then

            TransfertSaisie(lModif)

            If lModif Then
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Vide la fenêtre des erreurs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        ErrorProvider.Clear()
    End Sub



    Private Function ValideSaisieFenetre() As Boolean
        lFrm_Valide = True

        For Each txtbox_loc As TextBox In list_txtbox
            If Not ErrorProvider.GetError(txtbox_loc) = String.Empty Then
                lFrm_Valide = False
                Exit For
            End If
        Next
        Return lFrm_Valide
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)
        lModif = False

        With MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma

            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaG_sup, .GammaG_sup, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaG_inf, .GammaG_inf, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaQ, .GammaQ, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.Psi0_Q1, .Psi0_Q1, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.Psi1_Q1, .Psi1_Q1, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.Psi2_Q1, .Psi1_Q1, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.Psi0_Q2, .Psi0_Q2, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.Psi1_Q2, .Psi1_Q2, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.Psi2_Q2, .Psi1_Q2, lModif)

            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaM0, .GammaM0, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaM1, .GammaM1, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaM2, .GammaM2, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaC, .GammaC, lModif)

            GereTransfertValeur(MyPoutreLoc.Param.Gamma.lGammaV_unique, .lGammaV_unique, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaVs, .GammaVs, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaVc, .GammaVc, lModif)

            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaS, .GammaS, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaP, .GammaP, lModif)

            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaM_fi_a, .GammaM_fi_a, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaC_fi, .GammaC_fi, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaM_fi_s, .GammaM_fi_s, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaV_fi, .GammaV_fi, lModif)

        End With

        If lModif Then
            MyProjet.Poutres(MyProjet.IndEnCours).Initialise_CoefficientsCombinaisons()
        End If
    End Sub

#End Region

#Region " Dessins "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_GammaGsup.Paint, img_GammaGinf.Paint, img_GammaQ.Paint, img_Psi0.Paint, img_Q1.Paint, img_Q2.Paint, img_Psi1.Paint, img_Psi2.Paint, img_GammaM0.Paint, img_GammaM1.Paint, img_GammaM2.Paint, img_GammaC.Paint, img_GammaVs.Paint, img_GammaVc.Paint, img_GammaS.Paint, img_GammaP.Paint, img_GammaM_fi_a.Paint, img_GammaC_fi.Paint, img_GammaV_fi.Paint, img_GammaM_fi_s.Paint

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

        '--> Initialisation

        lIndice = True
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

            Case Me.img_Q1.Name

                lGrec = False
                lEgal = False

                xStart = sWI * 0.7
                xPen = xStart

                strSymbol = "Q"
                strIndice = "1"

            Case Me.img_Q2.Name

                lGrec = False
                lEgal = False

                xStart = sWI * 0.7
                xPen = xStart

                strSymbol = "Q"
                strIndice = "2"

            Case Me.img_Psi0.Name

                strSymbol = "y"
                strIndice = "0"

            Case Me.img_Psi1.Name

                strSymbol = "y"
                strIndice = "1"

            Case Me.img_Psi2.Name

                strSymbol = "y"
                strIndice = "2"

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

            'Case Me.img_GammaVp.Name

            '    strSymbol = "g"
            '    strIndice = "Vp"

            Case Me.img_GammaS.Name

                strSymbol = "g"
                strIndice = "S"

            Case Me.img_GammaP.Name

                strSymbol = "g"
                strIndice = "P"

            Case Me.img_GammaM_fi_a.Name

                strSymbol = "g"
                strIndice = "M,fi,a"

            Case Me.img_GammaC_fi.Name

                strSymbol = "g"
                strIndice = "C,fi"

            Case Me.img_GammaM_fi_s.Name

                strSymbol = "g"
                strIndice = "M,fi,s"

            Case Me.img_GammaV_fi.Name

                strSymbol = "g"
                strIndice = "V,fi"

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Evènements "

    Private Sub MAJI_GammaV_Unique()
        If Me.chk_GammaV_Unique.Checked Then
            y_txt_GammaVs = y_txt_GammaC + y_decal
            y_txt_GammaS = y_txt_GammaVs + y_decal
            y_txt_GammaP = y_txt_GammaS + y_decal

            y_txt_GammaVc = y_txt_GammaP + y_decal
            'y_txt_GammaVp = y_txt_GammaVc + y_decal

            y_btn_ReiniReistanceDalle = y_txt_GammaP + y_decal


        Else
            y_txt_GammaVs = y_txt_GammaC + y_decal
            y_txt_GammaVc = y_txt_GammaVs + y_decal
            y_txt_GammaS = y_txt_GammaVc + y_decal
            y_txt_GammaP = y_txt_GammaS + y_decal

            'y_txt_GammaVs = y_txt_GammaP + y_decal

            y_btn_ReiniReistanceDalle = y_txt_GammaP + y_decal

        End If

        Me.txt_GammaVs.Visible = True
        Me.img_GammaVs.Visible = True
        Me.txt_GammaVc.Visible = Not Me.chk_GammaV_Unique.Checked
        Me.img_GammaVc.Visible = Not Me.chk_GammaV_Unique.Checked
        'Me.txt_GammaVp.Visible = Not Me.chk_GammaV_Unique.Checked
        'Me.img_GammaVp.Visible = Not Me.chk_GammaV_Unique.Checked

        Me.img_GammaVs.Location = New Point(x_img_GammaCVSP, y_txt_GammaVs)
        Me.img_GammaVc.Location = New Point(x_img_GammaCVSP, y_txt_GammaVc)
        'Me.img_GammaVp.Location = New Point(x_img_GammaCVSP, y_txt_GammaVp)
        Me.img_GammaS.Location = New Point(x_img_GammaCVSP, y_txt_GammaS)
        Me.img_GammaP.Location = New Point(x_img_GammaCVSP, y_txt_GammaP)

        Me.txt_GammaVs.Location = New Point(x_txt_GammaCVSP, y_txt_GammaVs)
        Me.txt_GammaVc.Location = New Point(x_txt_GammaCVSP, y_txt_GammaVc)
        'Me.txt_GammaVp.Location = New Point(x_txt_GammaCVSP, y_txt_GammaVp)
        Me.txt_GammaS.Location = New Point(x_txt_GammaCVSP, y_txt_GammaS)
        Me.txt_GammaP.Location = New Point(x_txt_GammaCVSP, y_txt_GammaP)

        'List(Of (TextBox, Boolean))

    End Sub

    Private Sub chk_GammaV_Unique_CheckedChanged(sender As Object, e As EventArgs) Handles chk_GammaV_Unique.CheckedChanged
        If lBuild Then Exit Sub

        MyPoutreLoc.Param.Gamma.lGammaV_unique = chk_GammaV_Unique.Checked

        If chk_GammaV_Unique.Checked Then
            MyPoutreLoc.Param.Gamma.GammaVc = MyPoutreLoc.Param.Gamma.GammaVs
            Me.txt_GammaVc.Text = Format(MyPoutreLoc.Param.Gamma.GammaVc, formatGAMMA)
        End If

        MAJI_GammaV_Unique()


    End Sub

#End Region

#Region " Evènements saisie "
    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles txt_GammaGsup.TextChanged, txt_GammaGinf.TextChanged, txt_GammaQ.TextChanged, txt_Psi0_Q1.TextChanged, txt_Psi1_Q1.TextChanged, txt_Psi2_Q1.TextChanged, txt_Psi0_Q2.TextChanged, txt_Psi1_Q2.TextChanged, txt_Psi2_Q2.TextChanged, txt_GammaM0.TextChanged, txt_GammaM1.TextChanged, txt_GammaM2.TextChanged, txt_GammaC.TextChanged, txt_GammaVs.TextChanged, txt_GammaVc.TextChanged, txt_GammaS.TextChanged, txt_GammaP.TextChanged, txt_GammaM_fi_a.TextChanged, txt_GammaC_fi.TextChanged, txt_GammaV_fi.TextAlignChanged, txt_GammaM_fi_s.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            With MyPoutreLoc.Param.Gamma
                Select Case sender.name
                    Case txt_GammaGsup.Name
                        .GammaG_sup = ValeurUI
                    Case txt_GammaGinf.Name
                        .GammaG_inf = ValeurUI
                    Case txt_GammaQ.Name
                        .GammaQ = ValeurUI
                    Case txt_Psi0_Q1.Name
                        .Psi0_Q1 = ValeurUI
                    Case txt_Psi1_Q1.Name
                        .Psi1_Q1 = ValeurUI
                    Case txt_Psi2_Q1.Name
                        .Psi2_Q1 = ValeurUI
                    Case txt_Psi0_Q2.Name
                        .Psi0_Q2 = ValeurUI
                    Case txt_Psi1_Q2.Name
                        .Psi1_Q2 = ValeurUI
                    Case txt_Psi2_Q2.Name
                        .Psi2_Q2 = ValeurUI
                    Case txt_GammaM0.Name
                        .GammaM0 = ValeurUI
                    Case txt_GammaM1.Name
                        .GammaM1 = ValeurUI
                    Case txt_GammaM2.Name
                        .GammaM2 = ValeurUI
                    Case txt_GammaC.Name
                        .GammaC = ValeurUI
                    Case txt_GammaVs.Name
                        If .lGammaV_unique Then
                            .GammaVs = ValeurUI
                            .GammaVc = ValeurUI
                        Else
                            .GammaVs = ValeurUI
                        End If
                    Case txt_GammaVc.Name
                        .GammaVc = ValeurUI
                        'Case txt_GammaVp.Name
                        '.GammaVp = ValeurUI
                    Case txt_GammaS.Name
                        .GammaS = ValeurUI
                    Case txt_GammaP.Name
                        .GammaP = ValeurUI
                    Case txt_GammaM_fi_a.Name
                        .GammaM_fi_a = ValeurUI
                    Case txt_GammaC_fi.Name
                        .GammaC_fi = ValeurUI
                    Case txt_GammaM_fi_s.Name
                        .GammaM_fi_s = ValeurUI
                    Case txt_GammaV_fi.Name
                        .GammaV_fi = ValeurUI
                End Select

            End With

        End If

    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        'Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

        Select Case MyTxt.Name
            Case Me.txt_GammaGsup.Name, Me.txt_GammaGinf.Name, Me.txt_GammaQ.Name
                ValMin = GAMMA_ACTION_MIN
                ValMax = GAMMA_ACTION_MAX

            Case Me.txt_Psi0_Q1.Name, Me.txt_Psi1_Q1.Name, Me.txt_Psi2_Q1.Name, Me.txt_Psi0_Q2.Name, Me.txt_Psi1_Q2.Name, Me.txt_Psi2_Q2.Name
                ValMin = PSI_COMBINAISON_MIN
                ValMax = PSI_COMBINAISON_MAX

            Case Me.txt_GammaM0.Name, Me.txt_GammaM1.Name, Me.txt_GammaM2.Name, Me.txt_GammaC.Name, Me.txt_GammaVs.Name, Me.txt_GammaVc.Name, Me.txt_GammaS.Name, Me.txt_GammaP.Name, Me.txt_GammaM_fi_a.Name, Me.txt_GammaC_fi.Name, Me.txt_GammaM_fi_s.Name, Me.txt_GammaV_fi.Name 'Me.txt_GammaVp.Name,
                ValMin = GAMMA_RESISTANCE_MIN
                ValMax = GAMMA_RESISTANCE_MAX
        End Select

        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, lValMin, ValMax, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) '* kUnit
            'ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

    Private Sub btn_Reini_Click(sender As Object, e As EventArgs) Handles btn_Reini.Click

        Me.txt_GammaGsup.Text = Format(LogicielOptions.Gamma.GammaG_sup, formatGAMMA)
        Me.txt_GammaGinf.Text = Format(LogicielOptions.Gamma.GammaG_inf, formatGAMMA)
        Me.txt_GammaQ.Text = Format(LogicielOptions.Gamma.GammaQ, formatGAMMA)

        Me.txt_Psi0_Q1.Text = Format(LogicielOptions.Gamma.Psi0_Q1, formatGAMMA)
        Me.txt_Psi1_Q1.Text = Format(LogicielOptions.Gamma.Psi1_Q1, formatGAMMA)
        Me.txt_Psi2_Q1.Text = Format(LogicielOptions.Gamma.Psi2_Q1, formatGAMMA)

        Me.txt_Psi0_Q2.Text = Format(LogicielOptions.Gamma.Psi0_Q2, formatGAMMA)
        Me.txt_Psi1_Q2.Text = Format(LogicielOptions.Gamma.Psi1_Q2, formatGAMMA)
        Me.txt_Psi2_Q2.Text = Format(LogicielOptions.Gamma.Psi2_Q2, formatGAMMA)

        Me.txt_GammaM0.Text = Format(LogicielOptions.Gamma.GammaM0, formatGAMMA)
        Me.txt_GammaM1.Text = Format(LogicielOptions.Gamma.GammaM1, formatGAMMA)
        Me.txt_GammaM2.Text = Format(LogicielOptions.Gamma.GammaM2, formatGAMMA)

        Me.txt_GammaC.Text = Format(LogicielOptions.Gamma.GammaC, formatGAMMA)
        Me.txt_GammaVs.Text = Format(LogicielOptions.Gamma.GammaVs, formatGAMMA)
        Me.txt_GammaVc.Text = Format(LogicielOptions.Gamma.GammaVc, formatGAMMA)
        'Me.txt_GammaVp.Text = Format(LogicielOptions.Gamma.GammaVc, formatGAMMA)
        Me.txt_GammaS.Text = Format(LogicielOptions.Gamma.GammaS, formatGAMMA)
        Me.txt_GammaP.Text = Format(LogicielOptions.Gamma.GammaP, formatGAMMA)

        Me.txt_GammaM_fi_a.Text = Format(LogicielOptions.Gamma.GammaM_fi_a, formatGAMMA)
        Me.txt_GammaC_fi.Text = Format(LogicielOptions.Gamma.GammaC_fi, formatGAMMA)
        Me.txt_GammaM_fi_s.Text = Format(LogicielOptions.Gamma.GammaM_fi_s, formatGAMMA)
        Me.txt_GammaV_fi.Text = Format(LogicielOptions.Gamma.GammaV_fi, formatGAMMA)

    End Sub

#End Region

End Class