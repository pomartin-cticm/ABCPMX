Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports PMXMoteur2

Public Class Frm_DalleNArma

#Region " Variables locales "

    Dim lBuild As Boolean
    Dim strLitNo(1) As String
    Dim strTauxArma As String
    Dim strToolTipAddRebar As String
    Dim strToolTipRemoveRebar As String

    Dim iSelect As Integer = -1
    Dim iLitSelect As Integer = 0       'Indice du lit d'armatures à l'affichage

    Dim FontFrm As Font

    Dim ClasseAcierArma() As String = cls_AcierArmature.tabClasseAcierArma


    ''' <summary>
    ''' Ajout GUD: les positions des lits d'armatures peuvent être relatives dans le cas de plusieurs nappes 
    ''' </summary>
    Dim zMin_Rel, zMax_Rel As Decimal

#End Region

#Region " Constantes locales "
    Private Const HPMINI As Decimal = 0.04
    Private Const PHIMIN As Decimal = 0.003
    Private Const PHIMAX As Decimal = 0.04
    Private Const ESPMIN As Decimal = 0.05
    Private Const ESPMAX As Decimal = 0.5
    Private Const ZMIN As Decimal = 0.02
#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(myBloc As Dictionary(Of String, String))
        lBuild = True

        GestionStyle()
        GestionLangues(myBloc)
        GestionUnites()
        PreparerFenetre()
        AfficherDalleEnCours()
        lBuild = False

    End Sub

    Private Sub PreparerFenetre()
        MAJI_StatutBOArma()
        MAJI_BOArmatures()
        AfficherLitEncours()

        RemplirComboAvecTableau(Me.cmb_Acier, ClasseAcierArma)

    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As System.Windows.Forms.ComboBox, tabValeurs() As String)

        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)

    End Sub

    Private Sub GestionStyle()

        Me.Pan_Contenu.Dock = DockStyle.Fill

        FontFrm = New Font(FontBase.Name, SizeFontFrm)

        Me.lbl_Armatures.BackColor = CouleurBackBandeaux
        Me.lbl_Armatures.ForeColor = CouleurForeBandeaux
        Me.lbl_Acier.BackColor = CouleurBackBandeaux
        Me.lbl_Acier.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(txt_Fsk, False)

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim6.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim7.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitSigma2.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub GestionLangues(myBloc As Dictionary(Of String, String))

        Dim strLoadedKey As String = ""
        Dim CLE As String = ""

        Try

            '=== ARMATURES ====================================================================

            CLE = "REBARS" : Me.lbl_Armatures.Text = myBloc(CLE)
            CLE = "REINFLAYER0" : Me.ToolTipDalle.SetToolTip(Me.chk_Lit0, myBloc(CLE))
            CLE = "REINFLAYER1" : Me.ToolTipDalle.SetToolTip(Me.chk_Lit1, myBloc(CLE))
            CLE = "REINFLAYER2" : Me.ToolTipDalle.SetToolTip(Me.chk_Lit2, myBloc(CLE))
            CLE = "DIAMETER" : Me.lbl_Diametre.Text = myBloc(CLE)
            CLE = "SPACING" : Me.lbl_Espacement.Text = myBloc(CLE)
            CLE = "LOCATION" : Me.lbl_zs.Text = myBloc(CLE)

            CLE = "ADDLAYER" : strToolTipAddRebar = myBloc(CLE)
            CLE = "REMOVELAYER" : strToolTipRemoveRebar = myBloc(CLE)
            CLE = "FIRSTLAYER" : strLitNo(0) = myBloc(CLE)
            CLE = "SECONDLAYER" : strLitNo(1) = myBloc(CLE)

            CLE = "REINFRATIO" : strTauxArma = myBloc(CLE)

            CLE = "NOREINFORCEMENT" : Me.lbl_NoArma.Text = myBloc(CLE)                   'Pour une poutre sans console, il n'est pas nécessaire de définir les lits d'armature"

            '=== ACIER DES ARMATURES ==========================================================

            CLE = "REBARSTEEL" : Me.lbl_Acier.Text = myBloc(CLE)        '"Reinforcement steel"
            CLE = "CLASS" : Me.lbl_ClasseA.Text = myBloc(CLE)



        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
        End Try

    End Sub


    Private Sub AfficherLitEncours()

        If iLitSelect = -1 Then

            Me.pan_DonneesArma.Visible = False

        Else

            Me.pan_DonneesArma.Visible = True

            Me.lbl_LitNo.Text = strLitNo(iLitSelect)
            Me.txt_PhiS.Text = GetStringInUnit(Frm_DalleN.myDalleLoc.LitArma(iLitSelect).PhiS, Enu_TypeVariable.Dimension, 4, 3, False)
            Me.txt_esp.Text = GetStringInUnit(Frm_DalleN.myDalleLoc.LitArma(iLitSelect).EspBar, Enu_TypeVariable.Dimension, 4, 3, False)
            Me.txt_zs.Text = GetStringInUnit(Frm_DalleN.myDalleLoc.LitArma(iLitSelect).z_s, Enu_TypeVariable.Dimension, 4, 3, False)

            Me.img_esp.Invalidate()
            Me.img_PhiS.Invalidate()
            Me.img_zs.Invalidate()

        End If
    End Sub


    Private Sub AfficherDalleEnCours()

        If Frm_DalleN.myDalleLoc.lNoArma Then
            iLitSelect = -1
            iSelect = -1
        Else
            'Ajout GuD: Permet de réinitialiser la variable iLitSelect à l'ouverture
            If Frm_DalleN.myDalleLoc.LitArma(1).lActive Then
                iLitSelect = 1
                iSelect = 200
            Else
                iLitSelect = 0
                iSelect = 100
            End If
        End If

        Dim Chaine = Frm_DalleN.myDalleLoc.AcierArmatures.Classe
        If Me.ClasseAcierArma.Contains(Chaine) Then
            Me.cmb_Acier.SelectedIndex = Array.IndexOf(Me.ClasseAcierArma, Chaine)
        Else
            Me.cmb_Acier.SelectedIndex = 0
        End If
        MAJI_ProprietesAcier()
    End Sub

#End Region

#Region " Routines MAJI "

    Private Sub MAJI_ProprietesAcier()
        Frm_DalleN.MyDalleLoc.AcierArmatures.MAJProprietes()
        Me.txt_Fsk.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.AcierArmatures.FsK, Enu_TypeVariable.Contrainte)
    End Sub

    Private Sub MAJI_BOArmatures()
        Select Case Frm_DalleN.myDalleLoc.NbLitsArmaActifs
            Case 1
                'Me.TLpan_ChoixLit.ColumnStyles(1).Width = 0
                Me.chk_AjouterSupprimerLit.Image = imgList_BOArma.Images("Ajouter")
                Me.ToolTipDalle.SetToolTip(Me.chk_AjouterSupprimerLit, strToolTipAddRebar)
                Me.chk_Lit2.Visible = False
            Case 2
                'Me.TLpan_ChoixLit.ColumnStyles(1).Width = 46
                Me.chk_AjouterSupprimerLit.Image = imgList_BOArma.Images("Supprimer")
                Me.ToolTipDalle.SetToolTip(Me.chk_AjouterSupprimerLit, strToolTipRemoveRebar)
                Me.chk_Lit2.Visible = True
        End Select
    End Sub

    Private Sub MAJI_StatutBOArma()
        Dim lBuildBack As Boolean = lBuild
        lBuild = True

        Me.chk_Lit0.Checked = (iLitSelect = -1)
        Me.chk_Lit1.Checked = (iLitSelect = 0)
        Me.chk_Lit2.Checked = (iLitSelect = 1)

        Me.chk_AjouterSupprimerLit.Visible = Not (iLitSelect = -1)
        Me.chk_AjouterSupprimerLit.Checked = False

        lBuild = lBuildBack
    End Sub

    Private Sub MAJ_ValeursLimites()

        Me.zMin_Rel = ZMIN
        Me.zMax_Rel = (Frm_DalleN.myDalleLoc.EpaisseurActive - ZMIN)

        If Frm_DalleN.myDalleLoc.NbLitsArmaActifs = 2 Then
            If iLitSelect = 0 Then 'permiere nappe
                ' Me.zMax_Rel = Math.Min(Me.zMax_Rel, Me.MyDalleLoc.LitArma(1).z_s - Me.MyDalleLoc.LitArma(0).PhiS / 2 - Me.MyDalleLoc.LitArma(1).PhiS / 2)
                Me.zMax_Rel = Math.Min(Me.zMax_Rel, Frm_DalleN.myDalleLoc.LitArma(1).z_s)
            Else 'deuxieme nappe
                ' Me.zMin_Rel = Math.Max(Me.zMin_Rel, Me.MyDalleLoc.LitArma(0).z_s + Me.MyDalleLoc.LitArma(0).PhiS / 2 + Me.MyDalleLoc.LitArma(1).PhiS / 2)
                Me.zMin_Rel = Math.Max(Me.zMin_Rel, Frm_DalleN.myDalleLoc.LitArma(0).z_s)
            End If
        End If

    End Sub
#End Region

#Region " Symboles "
    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_Fy.Paint, img_zs.Paint, img_PhiS.Paint, img_esp.Paint
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
        'phis -> diamètre
        'esp  -> espacement
        'zs   -> position
        'fy   -> classe acier
        lIndice = False
        lGrec = False
        lEgal = True
        Select Case sender.name

            Case Me.img_Fy.Name
                strSymbol = "f"
                strIndice = "sk"

            Case Me.img_zs.Name
                strSymbol = "z"
                strIndice = "s" & (iLitSelect + 1).ToString

            Case Me.img_PhiS.Name
                strSymbol = "f"
                strIndice = "s" & (iLitSelect + 1).ToString
                lGrec = True

            Case Me.img_esp.Name
                strSymbol = "e"
                strIndice = "s" & (iLitSelect + 1).ToString

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)
    End Sub

#End Region





#Region "===FERMETURE==="
    '''TODO
#End Region



#Region " Evènements sur la BO Armatures + évènements TextBox "

    Private Sub BOArma_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Lit2.CheckedChanged, chk_Lit1.CheckedChanged, chk_Lit0.CheckedChanged

        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.chk_Lit0.Name : iLitSelect = -1 : iSelect = -1

            Case Me.chk_Lit1.Name : iLitSelect = 0 : iSelect = 100
            Case Me.chk_Lit2.Name : iLitSelect = 1 : iSelect = 200
        End Select

        Frm_DalleN.myDalleLoc.lNoArma = (iLitSelect = -1)

        MAJ_ValeursLimites()

        'Dim ValeurUI As Decimal
        'VerificationSaisie(Me.txt_zs, ValeurUI)

        AfficherLitEncours()
        MAJI_StatutBOArma()
        MAJI_BOArmatures()
        Frm_DalleN.img_Dalle.Invalidate()

    End Sub

    Private Sub SaisieArma(sender As Object, e As EventArgs) Handles txt_zs.TextChanged, txt_PhiS.TextChanged, txt_esp.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then
            Select Case sender.name

                Case Me.txt_PhiS.Name
                    Frm_DalleN.myDalleLoc.LitArma(iLitSelect).PhiS = Valeur

                Case Me.txt_esp.Name
                    Frm_DalleN.myDalleLoc.LitArma(iLitSelect).EspBar = Valeur

                Case Me.txt_zs.Name
                    Frm_DalleN.myDalleLoc.LitArma(iLitSelect).z_s = Valeur

            End Select

            MAJ_ValeursLimites()
            Frm_DalleN.MAJI_TauxArma()
            Frm_DalleN.img_Dalle.Invalidate()
        End If

    End Sub

    Private Sub chk_AjouterSupprimerLit_CheckedChanged(sender As Object, e As EventArgs) Handles chk_AjouterSupprimerLit.CheckedChanged
        If lBuild Then Exit Sub

        Select Case iLitSelect
            Case 0
                '# Cas où on ajoute un lit = on sélectionne le second (créé)
                Frm_DalleN.myDalleLoc.LitArma(1).lActive = True
                iLitSelect = 1 : iSelect = 200
                Frm_DalleN.myDalleLoc.LitArma(1).z_s = Math.Max(Frm_DalleN.myDalleLoc.LitArma(0).z_s, Frm_DalleN.myDalleLoc.LitArma(1).z_s)
                Frm_DalleN.myDalleLoc.LitArma(1).z_s = Math.Min(Frm_DalleN.myDalleLoc.EpaisseurActive - ZMIN, Frm_DalleN.myDalleLoc.LitArma(1).z_s)
            Case 1
                '# Cas où on supprime le second lit : on sélectionne le premier
                Frm_DalleN.myDalleLoc.LitArma(1).lActive = False
                iLitSelect = 0 : iSelect = 100
        End Select

        Frm_DalleN.img_Dalle.Invalidate()
        MAJI_BOArmatures()
        MAJI_StatutBOArma()
        MAJ_ValeursLimites()
        Frm_DalleN.MAJI_TauxArma()
        AfficherLitEncours()

    End Sub

    Private Sub EnterTxtBoxes(sender As Object, e As EventArgs) Handles txt_zs.Enter, txt_PhiS.Enter, txt_esp.Enter
        If lBuild Then Exit Sub
        Dim iSelect As Integer
        Select Case sender.name
            Case Me.txt_PhiS.Name
                iSelect = (iLitSelect + 1) * 100 + 1
            Case Me.txt_esp.Name
                iSelect = (iLitSelect + 1) * 100 + 2
            Case Me.txt_zs.Name
                iSelect = (iLitSelect + 1) * 100 + 3
        End Select

        Frm_DalleN.MAJI_SelectionTxtbox(iSelect)
    End Sub

    Private Sub LeaveTxtBoxes(sender As Object, e As EventArgs) Handles txt_zs.Leave, txt_PhiS.Leave, txt_esp.Leave
        If lBuild Then Exit Sub
        Frm_DalleN.MAJI_DeselectionTxtbox()
    End Sub

    Private Sub LeaveAcierArma(sender As Object, e As EventArgs) Handles cmb_Acier.Leave
        If lBuild Then Exit Sub
        iSelect = -1
        Frm_DalleN.img_Dalle.Invalidate()
    End Sub

    Private Sub EnterAcierArma(sender As Object, e As EventArgs) Handles cmb_Acier.Enter
        If lBuild Then Exit Sub
        Frm_DalleN.MAJI_SelectionTxtbox(1001)
    End Sub

#End Region

#Region " Evenement sur Clase Acier "
    Private Sub cmb_Acier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Acier.SelectedIndexChanged
        If lBuild Then Exit Sub
        Frm_DalleN.myDalleLoc.AcierArmatures.Classe = Me.ClasseAcierArma(Me.cmb_Acier.SelectedIndex)
        MAJI_ProprietesAcier()

        Frm_DalleN.img_Dalle.Invalidate()
    End Sub
#End Region

    ''' <summary>
    ''' Vérification de la saisie des paramètres
    ''' </summary>
    Private Function VerificationSaisie(MyTxt As Object, ByRef ValeurUI As Decimal) As Boolean

        '-- Déclaration - Initialisation
        Dim lOk As Boolean = True
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Select Case MyTxt.Name
            Case Me.txt_PhiS.Name
                ValMin = PHIMIN / kUnit
                ValMax = PHIMAX / kUnit

                Debug.WriteLine("phi")

            Case Me.txt_esp.Name
                ValMin = ESPMIN / kUnit
                ValMax = ESPMAX / kUnit

                Debug.WriteLine("espacement")

            Case Me.txt_zs.Name
                ValMin = Me.zMin_Rel / kUnit
                ValMax = Me.zMax_Rel / kUnit

                Debug.WriteLine("position")

        End Select

        Debug.WriteLine("val min " & ValMin)
        Debug.WriteLine("val max " & ValMax)


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

End Class