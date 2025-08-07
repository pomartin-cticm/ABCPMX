Imports PMXMoteur2
Imports System.IO

Public Class Frm_GammaN

#Region " Déclarations "

    Enum Enu_Resistance
        Acier
        Beton
        Feu
    End Enum


#End Region

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim AffGammaM As Enu_Resistance = Enu_Resistance.Acier

    Dim MyPoutreLoc As New cls_Poutre(NomChargements)
    Public locGammaM As New cls_Gamma

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

    Dim msgChoixGammaV As String

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_GammaN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        locGammaM = MyPoutreLoc.Param.Gamma

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
        'list_txtbox.Add(txt_GammaM0)
        'list_txtbox.Add(txt_GammaM1)
        'list_txtbox.Add(txt_GammaM2)
        'list_txtbox.Add(txt_GammaC)
        'list_txtbox.Add(txt_GammaVs)
        'list_txtbox.Add(txt_GammaVc)
        ''list_txtbox.Add((txt_GammaVp, Not myBeamLoc.Param.Gamma.lGammaV_unique))
        'list_txtbox.Add(txt_GammaS)
        'list_txtbox.Add(txt_GammaP)
        'list_txtbox.Add(txt_GammaM_fi)
        'list_txtbox.Add(txt_GammaC_fi)
        'list_txtbox.Add(txt_GammaS_fi)
        'list_txtbox.Add(txt_GammaV_fi)

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim strLoadedKey As String = ""
            Const CLE As String = ""

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_GAMMA")
            BlocLine.CreationBloc(Bloc, strLoadedKey)

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
                Me.rdb_Acier.Text = Bloc("TAB_STEEL")
                Me.rdb_Beton.Text = Bloc("TAB_SLAB")
                Me.rdb_Feu.Text = Bloc("TAB_FIRE")
                msgchoixgammav = Bloc("GAMMAV")
                Me.lbl_Reset.Text = Bloc("RESET")

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
            Finally
                Bloc.Clear()
            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

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
            'Me.Tab_Dalle.Visible = False

            Me.TLpan_ChoixResistance.ColumnStyles(1).Width = 0
        End If

    End Sub

    Private Sub AfficherPoutreEnCours()

        Select Case AffGammaM
            Case Enu_Resistance.Acier : Me.rdb_Acier.Checked = True
            Case Enu_Resistance.Beton : Me.rdb_Beton.Checked = True
            Case Enu_Resistance.Feu : Me.rdb_Feu.Checked = True
        End Select

        MAJI_AffichageGammaM()

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

        End With

        ' MAJI_GammaV_Unique()

    End Sub

#End Region

#Region "===FERMETURE==="

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

        '==R25-003
        With MyProjet.Poutres(MyProjet.IndEnCours).Param.Gamma

            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaG_sup, .GammaG_sup, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaG_inf, .GammaG_inf, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.GammaQ, .GammaQ, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.Psi0_Q1, .Psi0_Q1, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.Psi1_Q1, .Psi1_Q1, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.Psi2_Q1, .Psi2_Q1, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.Psi0_Q2, .Psi0_Q2, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.Psi1_Q2, .Psi1_Q2, lModif)
            GereTransfertValeur(MyPoutreLoc.Param.Gamma.Psi2_Q2, .Psi2_Q2, lModif)


            GereTransfertValeur(locGammaM.GammaM0, .GammaM0, lModif)
            GereTransfertValeur(locGammaM.GammaM1, .GammaM1, lModif)
            GereTransfertValeur(locGammaM.GammaM2, .GammaM2, lModif)

            GereTransfertValeur(locGammaM.GammaC, .GammaC, lModif)
            GereTransfertValeur(locGammaM.GammaVc, .GammaVc, lModif)
            GereTransfertValeur(locGammaM.GammaVs, .GammaVs, lModif)
            GereTransfertValeur(locGammaM.GammaS, .GammaS, lModif)
            GereTransfertValeur(locGammaM.GammaP, .GammaP, lModif)
            GereTransfertValeur(locGammaM.lGammaV_unique, .lGammaV_unique, lModif)

            GereTransfertValeur(locGammaM.GammaC_fi, .GammaC_fi, lModif)
            GereTransfertValeur(locGammaM.GammaM_fi, .GammaM_fi, lModif)
            GereTransfertValeur(locGammaM.GammaS_fi, .GammaS_fi, lModif)
            GereTransfertValeur(locGammaM.GammaV_fi, .GammaV_fi, lModif)

        End With

        If lModif Then
            MyProjet.Poutres(MyProjet.IndEnCours).Initialise_CoefficientsCombinaisons()
        End If
    End Sub

#End Region

#Region " Dessins "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_GammaGsup.Paint, img_GammaGinf.Paint, img_GammaQ.Paint, img_Psi0.Paint, img_Q1.Paint, img_Q2.Paint, img_Psi1.Paint, img_Psi2.Paint

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

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Evènements "


    Private Sub rdb_Acier_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Feu.CheckedChanged, rdb_Beton.CheckedChanged, rdb_Acier.CheckedChanged
        If lBuild Then Exit Sub

        Select Case True
            Case Me.rdb_Acier.Checked
                AffGammaM = Enu_Resistance.Acier
            Case Me.rdb_Beton.Checked
                AffGammaM = Enu_Resistance.Beton
            Case Me.rdb_Feu.Checked
                AffGammaM = Enu_Resistance.Feu
        End Select

        MAJI_AffichageGammaM()

    End Sub

    Private Sub MAJI_AffichageGammaM()

        Me.pan_SaisieResistance.Controls.Clear()

        Select Case AffGammaM
            Case Enu_Resistance.Acier
                Me.pan_SaisieResistance.Controls.Add(Frm_GammaM_Acier.pan_GammaM)
                Frm_GammaM_Acier.InitialiseFenetre()
            Case Enu_Resistance.Beton
                Me.pan_SaisieResistance.Controls.Add(Frm_GammaM_Beton.pan_GammaM)
                Frm_GammaM_Beton.InitialiseFenetre(msgchoixgammav)
            Case Enu_Resistance.Feu
                Me.pan_SaisieResistance.Controls.Add(Frm_GammaM_Feu.pan_GammaM)
                Frm_GammaM_Feu.InitialiseFenetre()
        End Select

    End Sub

#End Region

#Region " Evènements saisie "

    Private Sub TextBox_TextChanged(sender As Object, e As EventArgs) Handles txt_GammaGsup.TextChanged, txt_GammaGinf.TextChanged, txt_GammaQ.TextChanged, txt_Psi0_Q1.TextChanged, txt_Psi1_Q1.TextChanged, txt_Psi2_Q1.TextChanged, txt_Psi0_Q2.TextChanged, txt_Psi1_Q2.TextChanged, txt_Psi2_Q2.TextChanged
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

                'Case Me.txt_GammaM0.Name, Me.txt_GammaM1.Name, Me.txt_GammaM2.Name, Me.txt_GammaC.Name, Me.txt_GammaVs.Name, Me.txt_GammaVc.Name, Me.txt_GammaS.Name, Me.txt_GammaP.Name, Me.txt_GammaM_fi.Name, Me.txt_GammaC_fi.Name, Me.txt_GammaS_fi.Name, Me.txt_GammaV_fi.Name 'Me.txt_GammaVp.Name,
                '    ValMin = GAMMA_RESISTANCE_MIN
                '    ValMax = GAMMA_RESISTANCE_MAX
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

        locGammaM.GammaM0 = LogicielOptions.Gamma.GammaM0
        locGammaM.GammaM1 = LogicielOptions.Gamma.GammaM1
        locGammaM.GammaM2 = LogicielOptions.Gamma.GammaM2

        locGammaM.GammaC = LogicielOptions.Gamma.GammaC
        locGammaM.GammaVc = LogicielOptions.Gamma.GammaVc
        locGammaM.GammaVs = LogicielOptions.Gamma.GammaVs
        locGammaM.GammaS = LogicielOptions.Gamma.GammaS
        locGammaM.GammaP = LogicielOptions.Gamma.GammaP
        locGammaM.lGammaV_unique = LogicielOptions.Gamma.lGammaV_unique

        locGammaM.GammaM_fi = LogicielOptions.Gamma.GammaM_fi
        locGammaM.GammaC_fi = LogicielOptions.Gamma.GammaC_fi
        locGammaM.GammaS_fi = LogicielOptions.Gamma.GammaS_fi
        locGammaM.GammaV_fi = LogicielOptions.Gamma.GammaV_fi

        Select Case AffGammaM
            Case Enu_Resistance.Acier : Frm_GammaM_Acier.ReInit()
            Case Enu_Resistance.Beton : Frm_GammaM_Beton.ReInit()
            Case Enu_Resistance.Feu : Frm_GammaM_Feu.ReInit()
        End Select

    End Sub

#End Region

End Class