Imports PMXMoteur2
Imports System.IO

Public Class Frm_Gamma

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim MyPoutreLoc As New cls_Poutre

    'Permet la gestion de l'activation ou non du checkbox

    Dim x_img_GammaCVSP As Decimal
    Dim x_txt_GammaCVSP As Decimal

    Dim y_txt_GammaC As Decimal
    Dim y_txt_GammaV As Decimal
    Dim y_txt_GammaVs As Decimal
    Dim y_txt_GammaVp As Decimal
    Dim y_txt_GammaS As Decimal
    Dim y_txt_GammaP As Decimal

    Dim y_decal As Decimal

    'Dispose de tous les textbox dans une seule liste (utile pour la gestion des erreurs)
    Dim list_txtbox As New List(Of (TextBox, Boolean))

    'Indique si aucune erreur n'a été constaté et permet de valider ou non la fenetre
    Dim lFrm_Valide As Boolean = True


#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Gamma_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        InitialiserVariables()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        AfficherPoutreEnCours()
        lBuild = False
    End Sub
    Private Sub InitialiserVariables()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)



        x_img_GammaCVSP = img_GammaC.Location.X
        x_txt_GammaCVSP = txt_GammaC.Location.X

        y_txt_GammaC = txt_GammaC.Location.Y
        y_txt_GammaV = txt_GammaV.Location.Y
        y_txt_GammaVs = txt_GammaVs.Location.Y
        y_txt_GammaVp = txt_GammaVp.Location.Y
        y_txt_GammaS = txt_GammaS.Location.Y
        y_txt_GammaP = txt_GammaP.Location.Y

        y_decal = Math.Abs(y_txt_GammaV - y_txt_GammaS)

        list_txtbox.Add((Me.txt_GammaGsup, True))
        list_txtbox.Add((txt_GammaGinf, True))
        list_txtbox.Add((txt_GammaQ, True))
        list_txtbox.Add((txt_Psi0_Q1, True))
        list_txtbox.Add((txt_Psi1_Q1, True))
        list_txtbox.Add((txt_Psi2_Q1, True))
        list_txtbox.Add((txt_Psi0_Q2, True))
        list_txtbox.Add((txt_Psi1_Q2, True))
        list_txtbox.Add((txt_Psi2_Q2, True))
        list_txtbox.Add((txt_GammaM0, True))
        list_txtbox.Add((txt_GammaM1, True))
        list_txtbox.Add((txt_GammaM2, True))
        list_txtbox.Add((txt_GammaC, True))
        list_txtbox.Add((txt_GammaV, MyPoutreLoc.Param.Gamma.lGammaV_unique))
        list_txtbox.Add((txt_GammaVs, Not MyPoutreLoc.Param.Gamma.lGammaV_unique))
        list_txtbox.Add((txt_GammaVp, Not MyPoutreLoc.Param.Gamma.lGammaV_unique))
        list_txtbox.Add((txt_GammaS, True))
        list_txtbox.Add((txt_GammaP, True))
        list_txtbox.Add((txt_GammaM_fi, True))
        list_txtbox.Add((txt_GammaC_fi, True))
        list_txtbox.Add((txt_GammaV_fi, True))

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_BASIC")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== MENU PRINCIPAL ==============================================================='



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

        If Not MyPoutreLoc.lMixte Then
            Me.Tab_Dalle.Visible = False
        End If

        MAJI_GammaV_Unique()


    End Sub

    Private Sub AfficherPoutreEnCours()

        With MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma

            Me.txt_GammaGsup.Text = CDec(.GammaG_sup)
            Me.txt_GammaGinf.Text = CDec(.GammaG_inf)
            Me.txt_GammaQ.Text = CDec(.GammaQ)

            Me.txt_Psi0_Q1.Text = CDec(.Psi0_Q1)
            Me.txt_Psi1_Q1.Text = CDec(.Psi1_Q1)
            Me.txt_Psi2_Q1.Text = CDec(.Psi2_Q1)

            Me.txt_Psi0_Q2.Text = CDec(.Psi0_Q2)
            Me.txt_Psi1_Q2.Text = CDec(.Psi1_Q2)
            Me.txt_Psi2_Q2.Text = CDec(.Psi2_Q2)

            Me.txt_GammaM0.Text = CDec(.GammaM0)
            Me.txt_GammaM1.Text = CDec(.GammaM1)
            Me.txt_GammaM2.Text = CDec(.GammaM2)

            Me.txt_GammaC.Text = CDec(.GammaC)
            Me.chk_GammaV_Unique.Checked = .lGammaV_unique
            Me.txt_GammaV.Text = CDec(.GammaV)
            Me.txt_GammaVp.Text = CDec(.GammaVp)
            Me.txt_GammaVs.Text = CDec(.GammaVs)
            Me.txt_GammaS.Text = CDec(.GammaS)
            Me.txt_GammaP.Text = CDec(.GammaP)

            Me.txt_GammaM_fi.Text = CDec(.GammaM_fi)
            Me.txt_GammaC_fi.Text = CDec(.GammaC_fi)
            Me.txt_GammaV_fi.Text = CDec(.GammaV_fi)

        End With


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


    Private Function ValideSaisieFenetre() As Boolean
        lFrm_Valide = True

        For Each tuple_txtbox_loc As (TextBox, Boolean) In list_txtbox
            If tuple_txtbox_loc.Item2 Then
                If Not ErrorProvider.GetError(tuple_txtbox_loc.Item1) = String.Empty Then
                    lFrm_Valide = False
                    Exit For
                End If
            End If
        Next
        Return lFrm_Valide
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)
        lModif = False

        With MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma

            If .GammaG_sup <> MyPoutreLoc.Param.Gamma.GammaG_sup Then
                lModif = True
                .GammaG_sup = MyPoutreLoc.Param.Gamma.GammaG_sup
            End If

            If .GammaG_inf <> MyPoutreLoc.Param.Gamma.GammaG_inf Then
                lModif = True
                .GammaG_inf = MyPoutreLoc.Param.Gamma.GammaG_inf
            End If

            If .GammaQ <> MyPoutreLoc.Param.Gamma.GammaQ Then
                lModif = True
                .GammaQ = MyPoutreLoc.Param.Gamma.GammaQ
            End If

            If .Psi0_Q1 <> MyPoutreLoc.Param.Gamma.Psi0_Q1 Then
                lModif = True
                .Psi0_Q1 = MyPoutreLoc.Param.Gamma.Psi0_Q1
            End If

            If .Psi1_Q1 <> MyPoutreLoc.Param.Gamma.Psi1_Q1 Then
                lModif = True
                .Psi1_Q1 = MyPoutreLoc.Param.Gamma.Psi1_Q1
            End If

            If .Psi2_Q1 <> MyPoutreLoc.Param.Gamma.Psi2_Q1 Then
                lModif = True
                .Psi2_Q1 = MyPoutreLoc.Param.Gamma.Psi2_Q1
            End If

            If .Psi0_Q2 <> MyPoutreLoc.Param.Gamma.Psi0_Q2 Then
                lModif = True
                .Psi0_Q2 = MyPoutreLoc.Param.Gamma.Psi0_Q2
            End If

            If .Psi1_Q2 <> MyPoutreLoc.Param.Gamma.Psi1_Q2 Then
                lModif = True
                .Psi1_Q2 = MyPoutreLoc.Param.Gamma.Psi1_Q2
            End If

            If .Psi2_Q2 <> MyPoutreLoc.Param.Gamma.Psi2_Q2 Then
                lModif = True
                .Psi2_Q2 = MyPoutreLoc.Param.Gamma.Psi2_Q2
            End If

            If .GammaM0 <> MyPoutreLoc.Param.Gamma.GammaM0 Then
                lModif = True
                .GammaM0 = MyPoutreLoc.Param.Gamma.GammaM0
            End If

            If .GammaM1 <> MyPoutreLoc.Param.Gamma.GammaM1 Then
                lModif = True
                .GammaM1 = MyPoutreLoc.Param.Gamma.GammaM1
            End If

            If .GammaM2 <> MyPoutreLoc.Param.Gamma.GammaM2 Then
                lModif = True
                .GammaM2 = MyPoutreLoc.Param.Gamma.GammaM2
            End If

            If .GammaC <> MyPoutreLoc.Param.Gamma.GammaC Then
                lModif = True
                .GammaC = MyPoutreLoc.Param.Gamma.GammaC
            End If

            If .GammaV <> MyPoutreLoc.Param.Gamma.GammaV Then
                lModif = True
                .GammaV = MyPoutreLoc.Param.Gamma.GammaV
            End If

            If .lGammaV_unique <> MyPoutreLoc.Param.Gamma.lGammaV_unique Then
                lModif = True
                .lGammaV_unique = MyPoutreLoc.Param.Gamma.lGammaV_unique
            End If

            If .GammaVs <> MyPoutreLoc.Param.Gamma.GammaVs Then
                lModif = True
                .GammaVs = MyPoutreLoc.Param.Gamma.GammaVs
            End If

            If .GammaVp <> MyPoutreLoc.Param.Gamma.GammaVp Then
                lModif = True
                .GammaVp = MyPoutreLoc.Param.Gamma.GammaVp
            End If

            If .GammaS <> MyPoutreLoc.Param.Gamma.GammaS Then
                lModif = True
                .GammaS = MyPoutreLoc.Param.Gamma.GammaS
            End If

            If .GammaP <> MyPoutreLoc.Param.Gamma.GammaP Then
                lModif = True
                .GammaP = MyPoutreLoc.Param.Gamma.GammaP
            End If

            If .GammaM_fi <> MyPoutreLoc.Param.Gamma.GammaM_fi Then
                lModif = True
                .GammaM_fi = MyPoutreLoc.Param.Gamma.GammaM_fi
            End If

            If .GammaC_fi <> MyPoutreLoc.Param.Gamma.GammaC_fi Then
                lModif = True
                .GammaC_fi = MyPoutreLoc.Param.Gamma.GammaC_fi
            End If

            If .GammaV_fi <> MyPoutreLoc.Param.Gamma.GammaV_fi Then
                lModif = True
                .GammaV_fi = MyPoutreLoc.Param.Gamma.GammaV_fi
            End If

        End With
    End Sub

#End Region

#Region " Dessins "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_GammaGsup.Paint, img_GammaGinf.Paint, img_GammaQ.Paint, img_Psi0.Paint, img_Q1.paint, img_Q2.paint, img_Psi1.Paint, img_Psi2.Paint, img_GammaM0.Paint, img_GammaM1.Paint, img_GammaM2.Paint, img_GammaC.Paint, img_GammaV.Paint, img_GammaVs.Paint, img_GammaVp.Paint, img_GammaS.Paint, img_GammaP.Paint, img_GammaM_fi.Paint, img_GammaC_fi.Paint, img_GammaV_fi.Paint

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

            Case Me.img_GammaV.Name

                strSymbol = "g"
                strIndice = "V"

            Case Me.img_GammaVs.Name

                strSymbol = "g"
                strIndice = "Vs"

            Case Me.img_GammaVp.Name

                strSymbol = "g"
                strIndice = "Vp"

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

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_Alignement.Gauche,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Evènements "

    Private Sub MAJI_GammaV_Unique()
        If Me.chk_GammaV_Unique.Checked Then
            y_txt_GammaV = y_txt_GammaC + y_decal
            y_txt_GammaS = y_txt_GammaV + y_decal
            y_txt_GammaP = y_txt_GammaS + y_decal

            y_txt_GammaVs = y_txt_GammaP + y_decal
            y_txt_GammaVp = y_txt_GammaVs + y_decal


        Else
            y_txt_GammaVs = y_txt_GammaC + y_decal
            y_txt_GammaVp = y_txt_GammaVs + y_decal
            y_txt_GammaS = y_txt_GammaVp + y_decal
            y_txt_GammaP = y_txt_GammaS + y_decal

            y_txt_GammaV = y_txt_GammaP + y_decal

        End If

        Me.txt_GammaV.Visible = Me.chk_GammaV_Unique.Checked
        Me.img_GammaV.Visible = Me.chk_GammaV_Unique.Checked
        Me.txt_GammaVs.Visible = Not Me.chk_GammaV_Unique.Checked
        Me.img_GammaVs.Visible = Not Me.chk_GammaV_Unique.Checked
        Me.txt_GammaVp.Visible = Not Me.chk_GammaV_Unique.Checked
        Me.img_GammaVp.Visible = Not Me.chk_GammaV_Unique.Checked

        Me.img_GammaV.Location = New Point(x_img_GammaCVSP, y_txt_GammaV)
        Me.img_GammaVs.Location = New Point(x_img_GammaCVSP, y_txt_GammaVs)
        Me.img_GammaVp.Location = New Point(x_img_GammaCVSP, y_txt_GammaVp)
        Me.img_GammaS.Location = New Point(x_img_GammaCVSP, y_txt_GammaS)
        Me.img_GammaP.Location = New Point(x_img_GammaCVSP, y_txt_GammaP)

        Me.txt_GammaV.Location = New Point(x_txt_GammaCVSP, y_txt_GammaV)
        Me.txt_GammaVs.Location = New Point(x_txt_GammaCVSP, y_txt_GammaVs)
        Me.txt_GammaVp.Location = New Point(x_txt_GammaCVSP, y_txt_GammaVp)
        Me.txt_GammaS.Location = New Point(x_txt_GammaCVSP, y_txt_GammaS)
        Me.txt_GammaP.Location = New Point(x_txt_GammaCVSP, y_txt_GammaP)

        chk_GammaV_Unique.Checked = MyPoutreLoc.Param.Gamma.lGammaV_unique

        'List(Of (TextBox, Boolean))

    End Sub

    Private Sub chk_GammaV_Unique_CheckedChanged(sender As Object, e As EventArgs) Handles chk_GammaV_Unique.CheckedChanged
        If lBuild Then Exit Sub

        MyPoutreLoc.Param.Gamma.lGammaV_unique = chk_GammaV_Unique.Checked

        MAJI_GammaV_Unique()


    End Sub

#End Region

#Region " Evènements saisie "
    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles txt_GammaGsup.TextChanged, txt_GammaGinf.TextChanged, txt_GammaQ.TextChanged, txt_Psi0_Q1.TextChanged, txt_Psi1_Q1.TextChanged, txt_Psi2_Q1.TextChanged, txt_Psi0_Q2.TextChanged, txt_Psi1_Q2.TextChanged, txt_Psi2_Q2.TextChanged, txt_GammaM0.TextChanged, txt_GammaM1.TextChanged, txt_GammaM2.TextChanged, txt_GammaC.TextChanged, txt_GammaV.TextChanged, txt_GammaVs.TextChanged, txt_GammaVp.TextChanged, txt_GammaS.TextChanged, txt_GammaP.TextChanged, txt_GammaM_fi.TextChanged, txt_GammaC_fi.TextChanged, txt_GammaV_fi.TextAlignChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            With MyPoutreLoc.Param.Gamma
                Select Case sender.name
                    Case txt_GammaGsup.Text
                        .GammaG_sup = ValeurUI
                    Case txt_GammaGinf.Text
                        .GammaG_inf = ValeurUI
                    Case txt_GammaQ.Text
                        .GammaQ = ValeurUI
                    Case txt_Psi0_Q1.Text
                        .Psi0_Q1 = ValeurUI
                    Case txt_Psi1_Q1.Text
                        .Psi1_Q1 = ValeurUI
                    Case txt_Psi2_Q1.Text
                        .Psi2_Q1 = ValeurUI
                    Case txt_Psi0_Q2.Text
                        .Psi0_Q2 = ValeurUI
                    Case txt_Psi1_Q2.Text
                        .Psi1_Q2 = ValeurUI
                    Case txt_Psi2_Q2.Text
                        .Psi2_Q2 = ValeurUI
                    Case txt_GammaM0.Text
                        .GammaM0 = ValeurUI
                    Case txt_GammaM1.Text
                        .GammaM1 = ValeurUI
                    Case txt_GammaM2.Text
                        .GammaM2 = ValeurUI
                    Case txt_GammaC.Text
                        .GammaC = ValeurUI
                    Case txt_GammaV.Text
                        .GammaV = ValeurUI
                    Case txt_GammaVs.Text
                        .GammaVs = ValeurUI
                    Case txt_GammaVp.Text
                        .GammaVp = ValeurUI
                    Case txt_GammaS.Text
                        .GammaS = ValeurUI
                    Case txt_GammaP.Text
                        .GammaP = ValeurUI
                    Case txt_GammaM_fi.Text
                        .GammaM_fi = ValeurUI
                    Case txt_GammaC_fi.Text
                        .GammaC_fi = ValeurUI
                    Case txt_GammaV_fi.Text
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
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

        Select Case MyTxt.Name
            Case Me.txt_GammaGsup.Name, Me.txt_GammaGinf.Name, Me.txt_GammaQ.Name
                ValMin = GAMMA_ACTION_INF
                ValMax = GAMMA_ACTION_SUP

            Case Me.txt_Psi0_Q1.Name, Me.txt_Psi1_Q1.Name, Me.txt_Psi2_Q1.Name, Me.txt_Psi0_Q2.Name, Me.txt_Psi1_Q2.Name, Me.txt_Psi2_Q2.Name
                ValMin = PSI_COMBINAISON_INF
                ValMax = PSI_COMBINAISON_SUP

            Case Me.txt_GammaM0.Name, Me.txt_GammaM1.Name, Me.txt_GammaM2.Name, Me.txt_GammaC.Name, Me.txt_GammaV.Name, Me.txt_GammaVs.Name, Me.txt_GammaVp.Name, Me.txt_GammaS.Name, Me.txt_GammaP.Name, Me.txt_GammaM_fi.Name, Me.txt_GammaC_fi.Name, Me.txt_GammaV_fi.Name
                ValMin = GAMMA_RESISTANCE_INF
                ValMax = GAMMA_RESISTANCE_SUP
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

    Private Sub btn_Reini_Click(sender As Object, e As EventArgs) Handles btn_ReiniChargement.Click, btn_ReiniAccompagnement.Click, btn_ReiniResistanceAcier.Click, btn_ReiniResistanceDalle.Click, btn_ReiniResistanceIncendie.Click
        Select Case sender.name
            Case btn_ReiniChargement.Name
                Me.txt_GammaGsup.Text = LogicielOptions.Gamma.GammaG_sup
                Me.txt_GammaGinf.Text = LogicielOptions.Gamma.GammaG_inf
                Me.txt_GammaQ.Text = LogicielOptions.Gamma.GammaQ

            Case btn_ReiniAccompagnement.Name
                Me.txt_Psi0_Q1.Text = LogicielOptions.Gamma.Psi0_Q1
                Me.txt_Psi1_Q1.Text = LogicielOptions.Gamma.Psi1_Q1
                Me.txt_Psi2_Q1.Text = LogicielOptions.Gamma.Psi2_Q1

                Me.txt_Psi0_Q2.Text = LogicielOptions.Gamma.Psi0_Q2
                Me.txt_Psi1_Q2.Text = LogicielOptions.Gamma.Psi1_Q2
                Me.txt_Psi2_Q2.Text = LogicielOptions.Gamma.Psi2_Q2

            Case btn_ReiniResistanceAcier.Name
                Me.txt_GammaM0.Text = LogicielOptions.Gamma.GammaM0
                Me.txt_GammaM1.Text = LogicielOptions.Gamma.GammaM1
                Me.txt_GammaM2.Text = LogicielOptions.Gamma.GammaM2

            Case btn_ReiniResistanceDalle.Name
                Me.txt_GammaC.Text = LogicielOptions.Gamma.GammaC
                Me.txt_GammaV.Text = LogicielOptions.Gamma.GammaV
                Me.txt_GammaVs.Text = LogicielOptions.Gamma.GammaVs
                Me.txt_GammaVp.Text = LogicielOptions.Gamma.GammaVs
                Me.txt_GammaS.Text = LogicielOptions.Gamma.GammaS
                Me.txt_GammaP.Text = LogicielOptions.Gamma.GammaP

            Case btn_ReiniResistanceIncendie.Name
                Me.txt_GammaM_fi.Text = LogicielOptions.Gamma.GammaM_fi
                Me.txt_GammaC_fi.Text = LogicielOptions.Gamma.GammaC_fi
                Me.txt_GammaV_fi.Text = LogicielOptions.Gamma.GammaV_fi

        End Select
    End Sub

#End Region
End Class