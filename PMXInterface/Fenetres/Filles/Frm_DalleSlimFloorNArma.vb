Imports System.Reflection
Imports PMXMoteur2

Public Class Frm_DalleSlimFloorNArma

#Region " Variables "

    Dim lBuild As Boolean

    Dim ClasseAcierArma() As String = cls_AcierArmature.tabClasseAcierArma

    Dim iSelect As Integer

    Const SELECT_RFEUXPOS As Integer = 101
    Const SELECT_RFEUYPOS As Integer = 102
    Const SELECT_RFEUDIA As Integer = 103
    Const SELECT_RFEUNB As Integer = 104
    Const SELECT_RFEUFSK As Integer = 1001

    Dim XMAX As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).Section.LargeurPlatInfSlim / 2
    Dim YMIN, YMAX As Decimal

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(myBloc As Dictionary(Of String, String))

        lBuild = True
        'lInter = plInter

        GestionStyle()
        GestionLangues(myBloc)
        GestionUnites()
        InitialiseVariablesLocales()

        'PrepareFenetre()

        RemplirComboDiametre()
        RemplirComboNombre()
        RemplirComboAvecTableau(Me.cmb_Acier, ClasseAcierArma)

        'MAJI_TypeDalle()

        AfficheDalleEnCours()

        lBuild = False

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitSigma2.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub GestionStyle()

        Me.pan_Main.Dock = DockStyle.Fill

        Me.lbl_Acier.BackColor = CouleurBackBandeaux
        Me.lbl_Acier.ForeColor = CouleurForeBandeaux

        Me.lbl_Arma.BackColor = CouleurBackBandeaux
        Me.lbl_Arma.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(txt_Fsk, False)
    End Sub

    Private Sub GestionLangues(myBloc As Dictionary(Of String, String))

        Dim strLoadedKey As String = ""
        Dim CLE As String = ""

        Try

            '** ACIER

            CLE = "REBARSTEEL" : Me.lbl_Acier.Text = myBloc(CLE)
            CLE = "CLASS" : Me.lbl_ClasseA.Text = myBloc(CLE)

            '** Armatures

            CLE = "FIREREBARSTITLE" : Me.lbl_Arma.Text = myBloc(CLE)
            CLE = "FIREREBARS" : Me.chk_Rebars.Text = myBloc(CLE)
            CLE = "FIREREB_NUMBER" : Me.lbl_Number.Text = myBloc(CLE)
            CLE = "FIREREB_DIAMETER" : Me.lbl_Diameter.Text = myBloc(CLE)
            CLE = "FIREREB_LOCATION" : Me.lbl_Location.Text = myBloc(CLE)

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
        End Try


    End Sub

    Private Sub AfficheDalleEnCours()

        ' ** Armatures **

        With Frm_DalleSlimFloorN.localDalle.ArmaSlimFeu

            Me.chk_Rebars.Checked = .lBarre
            Me.cmb_Nombre.SelectedIndex = .NbBarres - 1

            Me.cmb_Diametre.SelectedIndex = Array.IndexOf(Cls_Armatures_Longi.TabDiametres, .Diametre)

            Me.txt_ArmaX.Text = GetStringNoUnit(.xPos, Enu_TypeVariable.Dimension, True)
            Me.txt_ArmaY.Text = GetStringNoUnit(.zPos, Enu_TypeVariable.Dimension, True)

        End With

        '--> Acier

        Dim Chaine As String
        Chaine = Frm_DalleSlimFloorN.localDalle.AcierArmatures.Classe
        If Me.ClasseAcierArma.Contains(Chaine) Then
            Me.cmb_Acier.SelectedIndex = Array.IndexOf(Me.ClasseAcierArma, Chaine)
        Else
            Me.cmb_Acier.SelectedIndex = 0
        End If
        MAJI_ProprietesAcier()

    End Sub

    Private Sub RemplirComboDiametre()

        Me.cmb_Diametre.Items.Clear()

        For i As Integer = 0 To Cls_Armatures_Longi.TabDiametres.Count - 1
            Me.cmb_Diametre.Items.Add(GetStringInUnitN(Cls_Armatures_Longi.TabDiametres(i), Enu_TypeVariable.Dimension, 4, 3, True, True))
        Next

        Me.cmb_Diametre.SelectedItem = 0

    End Sub

    Private Sub RemplirComboNombre()

        Me.cmb_Nombre.Items.Clear()
        For i As Integer = 1 To 2
            Me.cmb_Nombre.Items.Add(CStr(i))
        Next
    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)

        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)

    End Sub

    Private Sub MAJI_ProprietesAcier()
        Frm_DalleSlimFloorN.localDalle.AcierArmatures.MAJProprietes()
        Me.txt_Fsk.Text = GetStringNoUnit(Frm_DalleSlimFloorN.localDalle.AcierArmatures.FsK, Enu_TypeVariable.Contrainte)
    End Sub

    Private Sub InitialiseVariablesLocales()

        Dim Tfi, Tfs As Decimal

        YMIN = Frm_DalleSlimFloorN.localDalle.ArmaSlimFeu.Diametre * 1.5

        With MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA
            Select Case .typeProfileAcier
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA
                    Tfi = 0
                    Tfs = .Tfs
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB
                    Tfi = 0
                    Tfs = .Plat_t
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB
                    Tfi = .Tfi
                    Tfs = .Tfs
                Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB
                    Tfi = .Tfi
                    Tfs = .Tfs
            End Select
        End With

        YMIN += Tfi

        YMAX = MyProjet.Poutres(MyProjet.IndEnCours).Section.zSemSup
        YMAX -= Frm_DalleSlimFloorN.localDalle.ArmaSlimFeu.Diametre * 1.5 + Tfs

    End Sub

#End Region

#Region " Evenements saisie "

    Private Sub chk_Rebars_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Rebars.CheckedChanged
        If lBuild Then Exit Sub

        Frm_DalleSlimFloorN.localDalle.ArmaSlimFeu.lBarre = Me.chk_Rebars.Checked

        Frm_DalleSlimFloorN.RedessineDalle()

    End Sub

    Private Sub cmb_Nombre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Nombre.SelectedIndexChanged
        If lBuild Then Exit Sub

        Frm_DalleSlimFloorN.localDalle.ArmaSlimFeu.NbBarres = Me.cmb_Nombre.SelectedIndex + 1

        Frm_DalleSlimFloorN.RedessineDalle()

    End Sub

    Private Sub cmb_Diametre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Diametre.SelectedIndexChanged

        If lBuild Then Exit Sub

        Frm_DalleSlimFloorN.localDalle.ArmaSlimFeu.Diametre = Cls_Armatures_Longi.TabDiametres(Me.cmb_Diametre.SelectedIndex)

        Frm_DalleSlimFloorN.RedessineDalle()

    End Sub

    Private Sub cmb_Acier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Acier.SelectedIndexChanged

        If lBuild Then Exit Sub

        Frm_DalleSlimFloorN.localDalle.AcierArmatures.Classe = cls_AcierArmature.tabClasseAcierArma(Me.cmb_Acier.SelectedIndex)
        MAJI_ProprietesAcier()

        ' Frm_DalleSlimFloorN.RedessineDalle()

    End Sub

#End Region

#Region " Evenements saisie TextBox "


    Private Sub Textbox_TextChanged(sender As Object, e As EventArgs) Handles txt_ArmaY.TextChanged, txt_ArmaX.TextChanged

        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name

                Case Me.txt_ArmaX.Name

                    Frm_DalleSlimFloorN.localDalle.ArmaSlimFeu.xPos = Valeur

                Case Me.txt_ArmaY.Name

                    Frm_DalleSlimFloorN.localDalle.ArmaSlimFeu.zPos = Valeur

            End Select

            Frm_DalleSlimFloorN.RedessineDalle()
        End If

    End Sub

    ''' <summary>
    ''' Vérification de la saisie des paramètres
    ''' </summary>
    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        '-- Déclaration - Initialisation

        Dim lOk As Boolean = True
        ErrorProvider.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMax As Boolean = True
        Dim lValMin As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Select Case MyTxt.Name

            Case txt_ArmaX.Name
                ValMin = Frm_DalleSlimFloorN.localDalle.ArmaSlimFeu.Diametre * 1.5 / kUnit
                ValMax = XMAX / kUnit

            Case Me.txt_ArmaY.Name
                ValMin = YMIN / kUnit
                ValMax = ymax / kUnit

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

#Region " Enter et Leave des objets "

    Private Sub txt_Enter(sender As Object, e As EventArgs) Handles txt_Fsk.Enter, txt_ArmaY.Enter, txt_ArmaX.Enter

        If lBuild Then Exit Sub
        Select Case sender.name
            Case Me.txt_Fsk.Name
                iSelect = 0
            Case Me.txt_ArmaX.Name
                iSelect = SELECT_RFEUXPOS
            Case Me.txt_ArmaY.Name
                iSelect = SELECT_RFEUYPOS
        End Select

        Frm_DalleSlimFloorN.Gestion_iSelect(iSelect)

    End Sub

    Private Sub txt_Leave(sender As Object, e As EventArgs) Handles txt_Fsk.Leave, txt_ArmaY.Leave, txt_ArmaX.Leave
        If lBuild Then Exit Sub
        iSelect = -1

        Frm_DalleSlimFloorN.Gestion_iSelect(iSelect)
    End Sub

    Private Sub cmb_Leave(sender As Object, e As EventArgs) Handles cmb_Diametre.Leave, cmb_Nombre.Leave, cmb_Acier.Leave
        If lBuild Then Exit Sub
        iSelect = -1

        Frm_DalleSlimFloorN.Gestion_iSelect(iSelect)
    End Sub

    Private Sub cmb_Enter(sender As Object, e As EventArgs) Handles cmb_Diametre.Enter, cmb_Nombre.Enter, cmb_Acier.Enter
        If lBuild Then Exit Sub

        Select Case sender.name
            Case cmb_Diametre.Name
                iSelect = SELECT_RFEUDIA
            Case cmb_Nombre.Name
                iSelect = SELECT_RFEUNB
            Case cmb_Acier.Name
                iSelect = SELECT_RFEUFSK
        End Select

        Frm_DalleSlimFloorN.Gestion_iSelect(iSelect)

    End Sub

#End Region

#Region " Symboles "
    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_Fy.Paint, img_ArmaY.Paint, img_ArmaX.Paint
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

            Case Me.img_Fy.Name
                strSymbol = "f"
                strIndice = "sk"

            Case Me.img_ArmaX.Name
                strSymbol = "x"
                strIndice = "s"

            Case Me.img_ArmaY.Name
                strSymbol = "y"
                strIndice = "s"

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)
    End Sub

#End Region


End Class