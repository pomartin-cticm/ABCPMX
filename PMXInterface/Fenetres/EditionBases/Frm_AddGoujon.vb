Imports PMXMoteur2
Imports System.IO

Public Class Frm_AddGoujon

#Region "Variables de la fenetre"

    Dim Bloc As New Dictionary(Of String, String)
    Dim ColorError As Color = Color.Red
    Dim ColorOK As Color = Color.Black
    Const HTMIN As Double = 0.001
    Const HTMAX As Double = 0.4
    Const DIAMIN As Double = 0.001
    Const DIAMAX As Double = 0.05
    Const STRENGHTMAX As Double = 10000
    Const STRENGHTMIN As Double = 1
    Dim lAdd As Boolean
    Dim iStud As Integer

    Dim ColorFixe As Color = Color.LightGray

    Dim lCustom As Boolean

#End Region

#Region "Ouverture de la fenetre"

    Private Sub Frm_AddGoujon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitialisationFenetre()
        GestionLangue()
    End Sub

    Sub GestionLangue()
        '
        '   Gestion de la langue pour la fenetre
        '
        '------------------------------------------------------------------------------------------------

        '--> Chargement des blocs langues

        Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRMADDSTUD")

        BlocLine.CreationBloc(Bloc)

        Try

            If lAdd Then
                Me.Text = Bloc("TITLEADD")
            ElseIf lCustom Then
                Me.Text = Bloc("TITLEMODIFY") & " : " & Frm_EditGoujons.ListGoujons(Frm_EditGoujons.IndGoujon - 1).nom
            Else
                Me.Text = Bloc("TITLEEDIT") & " : " & Frm_EditGoujons.ListGoujons(Frm_EditGoujons.IndGoujon - 1).nom
            End If
            Me.cmd_Valider.Text = Bloc("OK")
            If lCustom Then
                Me.cmd_Annuler.Text = Bloc("CLOSE")
            Else
                Me.cmd_Annuler.Text = Bloc("CANCEL")
            End If

            '--> Etiquettes

            Me.etq_DiametreTete.Text = Bloc("PHIHEAD")
            Me.etq_DiametreTige.Text = Bloc("PHIROD")
            Me.etq_FU.Text = Bloc("FU")
            Me.etq_FY.Text = Bloc("FY")
            Me.etq_Hauteur.Text = Bloc("HTOTAL")
            Me.etq_HauteurTete.Text = Bloc("DEPTHHEAD")
            Me.etq_Label.Text = Bloc("LABEL")

            Me.etq_Unit_DiametreTete.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
            Me.etq_Unit_DiametreTige.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
            Me.etq_Unit_HauteurTete.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
            Me.etq_UnitHauteur.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
            Me.etq_UnitFU.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)
            Me.etq_UnitFY.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        End Try

    End Sub

    Sub InitialisationFenetre()

        '--[ General

        Me.Icon = Frm_PMX.Icon
        'Me.HelpProvider1.HelpNamespace = FileACB.Help

        iStud = Frm_EditGoujons.IndGoujon - 1
        'iComWnd = Enu_Fenetre.Nulle

        lAdd = (Frm_EditGoujons.IndGoujon = -1)
        If lAdd Then
            lCustom = True
        Else
            lCustom = Frm_EditGoujons.ListGoujons(iStud).lCustom
        End If
        Me.cmd_Valider.Visible = lCustom
        If lCustom Then Me.AcceptButton = Me.cmd_Valider

        '--> Préparation des ErrorProviders

        PrepareErreurTextBox(Me.ErrLabel, Me.txt_Label, True)
        PrepareErreurTextBox(Me.ErrHauteur, Me.txt_Hauteur, False)
        PrepareErreurTextBox(Me.ErrDiametreTige, Me.txt_DiametreTige, False)
        PrepareErreurTextBox(Me.ErrHauteurTete, Me.txt_HauteurTete, False)
        PrepareErreurTextBox(Me.ErrDiametreTete, Me.txt_DiametreTete, False)
        PrepareErreurTextBox(Me.ErrFY, Me.txt_FY, False)
        PrepareErreurTextBox(Me.ErrFU, Me.txt_FU, False)

        '--> Initialisation des text box

        Me.txt_DiametreTete.ReadOnly = Not lCustom
        Me.txt_DiametreTige.ReadOnly = Not lCustom
        Me.txt_FU.ReadOnly = Not lCustom
        Me.txt_FY.ReadOnly = Not lCustom
        Me.txt_Hauteur.ReadOnly = Not lCustom
        Me.txt_HauteurTete.ReadOnly = Not lCustom
        Me.txt_Label.ReadOnly = Not lCustom

        If Not lCustom Then

            Me.txt_Label.BackColor = ColorFixe
            Me.txt_HauteurTete.BackColor = ColorFixe
            Me.txt_Hauteur.BackColor = ColorFixe
            Me.txt_FY.BackColor = ColorFixe
            Me.txt_FU.BackColor = ColorFixe
            Me.txt_DiametreTige.BackColor = ColorFixe
            Me.txt_DiametreTete.BackColor = ColorFixe

        End If

        If lAdd Then
            'Quand on ajoute, tous les champs sont effacés
            For Each TxtBox As Control In Me.Controls
                If TypeOf (TxtBox) Is TextBox Then TxtBox.Text = ""
            Next
        Else
            'Quand on modifie, on affiche le goujon à modifier
            Me.txt_Label.Text = Frm_EditGoujons.ListGoujons(iStud).nom
            Me.txt_Hauteur.Text = GetStringNoUnit(Frm_EditGoujons.ListGoujons(iStud).hsc, Enu_TypeVariable.Dimension)
            Me.txt_DiametreTige.Text = GetStringNoUnit(Frm_EditGoujons.ListGoujons(iStud).d, Enu_TypeVariable.Dimension)
            Me.txt_HauteurTete.Text = GetStringNoUnit(Frm_EditGoujons.ListGoujons(iStud).h_tete, Enu_TypeVariable.Dimension)
            Me.txt_DiametreTete.Text = GetStringNoUnit(Frm_EditGoujons.ListGoujons(iStud).d_tete, Enu_TypeVariable.Dimension)
            Me.txt_FY.Text = Format(Frm_EditGoujons.ListGoujons(iStud).Fy, "0.0")
            Me.txt_FU.Text = Format(Frm_EditGoujons.ListGoujons(iStud).Fu, "0.0")

        End If

    End Sub

#End Region

#Region "Fermeture Fenetre"

    Private Sub cmd_Annuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Annuler.Click
        'iComWnd = Enu_Fenetre.Annuler
        Me.Close()
    End Sub

    Private Sub cmd_Valider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Valider.Click

        Dim lValid As Boolean = True
        Dim iValid As Integer
        Dim Etiquette As String = ""
        Dim HTotal, HTete, PhiTige, PhiTete, Fy, Fu As Double
        Dim ValMin, ValMax As Double

        '--> Verification Label

        If Me.txt_Label.Text.Trim = "" Then
            Me.ErrLabel.SetError(Me.txt_Label, Bloc("ERRLABEL"))
            lValid = False
        Else
            If Not Frm_EditGoujons.NouveauLabelValide(Me.txt_Label.Text, iStud) Then
                lValid = False
                Me.ErrLabel.SetError(Me.txt_Label, Bloc("ERRLABELBASE"))
            Else
                Etiquette = Me.txt_Label.Text
            End If
        End If

        '--> Verification Hauteur

        ValMin = HTMIN / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
        ValMax = HTMAX / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        iValid = ValideSaisieTextBox(Me.txt_Hauteur, True, ValMin, True, ValMax, HTotal)

        lValid = lValid And iValid = 0

        Select Case iValid
            Case -1
                Me.ErrHauteur.SetError(Me.txt_Hauteur, Bloc("ERRVALEUR"))
            Case -2
                Me.ErrHauteur.SetError(Me.txt_Hauteur, Bloc("ERRVALNUM"))
            Case -3, -4
                Me.ErrHauteur.SetError(Me.txt_Hauteur, Bloc("ERRVALHLIM") & " : " & ValMin.ToString & " <= Ht <= " & ValMax.ToString)
        End Select

        '--> Verification Diametre Tige

        ValMin = DIAMIN / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
        ValMax = DIAMAX / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        iValid = ValideSaisieTextBox(Me.txt_DiametreTige, True, ValMin, True, ValMax, PhiTige)

        lValid = lValid And iValid = 0

        Select Case iValid
            Case -1
                Me.ErrDiametreTige.SetError(Me.txt_DiametreTige, Bloc("ERRVALEUR"))
            Case -2
                Me.ErrDiametreTige.SetError(Me.txt_DiametreTige, Bloc("ERRVALNUM"))
            Case -3, -4
                Me.ErrDiametreTige.SetError(Me.txt_DiametreTige, Bloc("ERRVALHLIM") & " : " & ValMin.ToString & " <= dr <= " & ValMax.ToString)
        End Select

        '--> Verification Diametre Tete

        ValMin = DIAMIN / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
        ValMax = DIAMAX / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        iValid = ValideSaisieTextBox(Me.txt_DiametreTete, True, ValMin, True, ValMax, PhiTete)

        lValid = lValid And iValid = 0

        Select Case iValid
            Case -1
                Me.ErrDiametreTete.SetError(Me.txt_DiametreTete, Bloc("ERRVALEUR"))
            Case -2
                Me.ErrDiametreTete.SetError(Me.txt_DiametreTete, Bloc("ERRVALNUM"))
            Case -3, -4
                Me.ErrDiametreTete.SetError(Me.txt_DiametreTete, Bloc("ERRVALHLIM") & " : " & ValMin.ToString & " <= dh <= " & ValMax.ToString)
        End Select

        '--> Verification Hauteur tete

        ValMin = DIAMIN / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
        ValMax = DIAMAX / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        iValid = ValideSaisieTextBox(Me.txt_HauteurTete, True, ValMin, True, ValMax, HTete)

        lValid = lValid And iValid = 0

        Select Case iValid
            Case -1
                Me.ErrHauteurTete.SetError(Me.txt_HauteurTete, Bloc("ERRVALEUR"))
            Case -2
                Me.ErrHauteurTete.SetError(Me.txt_HauteurTete, Bloc("ERRVALNUM"))
            Case -3, -4
                Me.ErrHauteurTete.SetError(Me.txt_HauteurTete, Bloc("ERRVALHLIM") & " : " & ValMin.ToString & " <= Hh <= " & ValMax.ToString)
        End Select

        '--> Verification Limite d'Elasticité

        ValMin = STRENGHTMIN
        ValMax = STRENGHTMAX

        iValid = ValideSaisieTextBox(Me.txt_FY, True, ValMin, True, ValMax, Fy)

        lValid = lValid And iValid = 0

        Select Case iValid
            Case -1
                Me.ErrFY.SetError(Me.txt_FY, Bloc("ERRVALEUR"))
            Case -2
                Me.ErrFY.SetError(Me.txt_FY, Bloc("ERRVALNUM"))
            Case -3, -4
                Me.ErrFY.SetError(Me.txt_FY, Bloc("ERRVALHLIM") & " : " & ValMin.ToString & " <= fy <= " & ValMax.ToString)
        End Select

        '--> Verification Résistance ultime

        ValMin = STRENGHTMIN
        ValMax = STRENGHTMAX

        iValid = ValideSaisieTextBox(Me.txt_FU, True, ValMin, True, ValMax, Fu)

        lValid = lValid And iValid = 0

        Select Case iValid
            Case -1
                Me.ErrFU.SetError(Me.txt_FU, Bloc("ERRVALEUR"))
            Case -2
                Me.ErrFU.SetError(Me.txt_FU, Bloc("ERRVALNUM"))
            Case -3, -4
                Me.ErrFU.SetError(Me.txt_FU, Bloc("ERRVALHLIM") & " : " & ValMin.ToString & " <= fu <= " & ValMax.ToString)
        End Select

        '--> Verification cohérence

        Dim strError As String = Bloc("ERROR")
        Dim lMessage As Boolean = False
        Dim NextErreur As String = Chr(13) & Chr(9) & "- "

        If lValid Then

            'Le diametre Tete > 1,5 x diametre Tige

            If PhiTige * 1.5 > PhiTete Then
                lValid = False
                lMessage = True
                strError = strError & NextErreur & RemplaceDollar(Bloc("ERRDHLTDT"), GetStringInUnit(1.5 * PhiTige * LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension), Enu_TypeVariable.Dimension, 4, 3, True))
            End If

            'La hauteur Tete < Hauteur totale

            If HTete >= HTotal Then
                lValid = False
                lMessage = True
                strError = strError & NextErreur & Bloc("ERRHHGTHT")
            End If

            If HTete < 0.4 * PhiTige Then
                lValid = False
                lMessage = True
                strError = strError & NextErreur & RemplaceDollar(Bloc("ERRHH04D"), GetStringInUnit(0.4 * PhiTige * LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension), Enu_TypeVariable.Dimension, 4, 3, True))
            End If

            If HTotal < 3 * PhiTige Then
                lValid = False
                lMessage = True
                strError = strError & NextErreur & RemplaceDollar(Bloc("ERRHT3D"), GetStringInUnit(3 * PhiTige * LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension), Enu_TypeVariable.Dimension, 4, 3, True))
            End If

            'Fy <= Fu

            If Fy > Fu Then
                lValid = False
                lMessage = True
                strError = strError & NextErreur & Bloc("ERRFULTFY")
            End If
        End If

        '--> Fin

        If lValid Then

            HTotal = HTotal * LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
            HTete = HTete * LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
            PhiTige = PhiTige * LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
            PhiTete = PhiTete * LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

            If lAdd Then
                Frm_EditGoujons.AddGoujonDansTables(Etiquette, HTotal, PhiTige, HTete, PhiTete, Fy, Fu, True)
                Frm_EditGoujons.ListGoujons.Add(New cls_GoujonSoude(Etiquette, HTotal, PhiTige, PhiTete, HTete, Fy, Fu))
                Frm_EditGoujons.ListGoujons(Frm_EditGoujons.ListGoujons.Count - 1).lCustom = True
            Else
                Frm_EditGoujons.ListGoujons(iStud).nom = Etiquette
                Frm_EditGoujons.ListGoujons(iStud).Fu = Fu
                Frm_EditGoujons.ListGoujons(iStud).Fy = Fy
                Frm_EditGoujons.ListGoujons(iStud).hsc = HTotal
                Frm_EditGoujons.ListGoujons(iStud).h_tete = HTete
                Frm_EditGoujons.ListGoujons(iStud).d = PhiTige
                Frm_EditGoujons.ListGoujons(iStud).d_tete = PhiTete
                Frm_EditGoujons.ListGoujons(iStud).lGoujonModifie = True
            End If
            Me.Close()

        Else
            If lMessage Then
                MessageBox.Show(strError, Bloc("ERRORFRM"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

#End Region

#Region "Gestion des évènements sur zones de saisie"

    Private Sub txt_Label_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Label.TextChanged
        Me.ErrLabel.SetError(Me.txt_Label, "")
    End Sub

    Private Sub txt_Hauteur_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Hauteur.TextChanged

        Dim Valeur As Double

        If ValideSaisieTextBox(Me.txt_Hauteur, True, HTMIN / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension),
                                               True, HTMAX / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension), Valeur) <> 0 Then
            Me.txt_Hauteur.ForeColor = ColorError
        Else
            Me.txt_Hauteur.ForeColor = ColorOK
        End If
        Me.ErrHauteur.SetError(Me.txt_Hauteur, "")

    End Sub

    Private Sub ChangeDiametre(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_DiametreTige.TextChanged, txt_DiametreTete.TextChanged, txt_HauteurTete.TextChanged

        Dim Valeur As Single
        Dim ValMin, ValMax As Double
        Select Case sender.name
            Case Me.txt_DiametreTige.Name
                ValMin = DIASTUDMIN
                ValMax = DIASTUDMAX
            Case Me.txt_DiametreTete.Name
                ValMin = 1.5 * DIASTUDMIN
                ValMax = 5 * DIASTUDMAX
            Case Me.txt_HauteurTete.Name
                ValMin = 0.4 * DIASTUDMIN
                ValMax = 5 * DIASTUDMAX
        End Select

        If ValideSaisieTextBox(sender, True, ValMin / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension),
                                       True, ValMax / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension), Valeur) <> 0 Then
            sender.ForeColor = ColorError
        Else
            sender.ForeColor = ColorOK
        End If
        Select Case sender.name
            Case Me.txt_DiametreTige.Name
                Me.ErrDiametreTige.SetError(Me.txt_DiametreTige, "")
            Case Me.txt_DiametreTete.Name
                Me.ErrDiametreTete.SetError(Me.txt_DiametreTete, "")
            Case Me.txt_HauteurTete.Name
                Me.ErrHauteurTete.SetError(Me.txt_HauteurTete, "")
        End Select

    End Sub

    Private Sub StrenghtChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_FY.TextChanged, txt_FU.TextChanged
        Dim Valeur As Single

        If ValideSaisieTextBox(sender, True, STRENGHTMIN,
                                       True, STRENGHTMAX, Valeur) <> 0 Then
            sender.ForeColor = ColorError
        Else
            sender.ForeColor = ColorOK
        End If
        Select Case sender.name
            Case Me.txt_FY.Name
                Me.ErrFY.SetError(Me.txt_FY, "")
            Case Me.txt_FU.Name
                Me.ErrFU.SetError(Me.txt_FU, "")
        End Select

    End Sub
#End Region

#Region "Gestion du Focus sur les TextBox"

    Private Sub GestionEnterTextBox(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Label.Enter, txt_HauteurTete.Enter, txt_Hauteur.Enter, txt_FY.Enter, txt_FU.Enter, txt_DiametreTige.Enter, txt_DiametreTete.Enter
        'sender.HideSelection = False
        sender.selectall()
    End Sub

    'Private Sub GestionExitTextBox(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_Label.Leave, txt_HauteurTete.Leave, txt_Hauteur.Leave, txt_FY.Leave, txt_FU.Leave, txt_DiametreTige.Leave, txt_DiametreTete.Leave
    '    sender.HideSelection = True
    'End Sub
#End Region
End Class