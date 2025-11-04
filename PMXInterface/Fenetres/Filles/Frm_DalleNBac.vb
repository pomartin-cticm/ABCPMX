Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip
Imports PMXMoteur2

Public Class Frm_DalleNBac

#Region " Variables locales "
    Dim strAppuiTcontinus, strAppuiTRibContinu, strAppuiTBacNonContinu As String
    Dim strAppuiTDiscontinus As String
    Dim strAppuiLbacUncut As String
    Dim strAppuiLbacCut1, strAppuiLbacCut2 As String

    Dim lBuild As Boolean
    Dim lCofraPlus220 As Boolean

    Const kADJUST As Decimal = 0.95

    Const DECMAX As Decimal = 20 / 1000 ' 20 mm

    Dim FontFrm As Font
    Dim strHauteur As String

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(myBloc As Dictionary(Of String, String))
        lBuild = True
        GestionLangues(myBloc)
        GestionStyle()
        GestionUnites()
        PrepareFenetre()
        AfficherBacEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangues(myBloc As Dictionary(Of String, String))

        Dim strLoadedKey As String = ""
        Dim CLE As String = ""

        Try
            '=== BAC ==========================================================================

            CLE = "SHEETING" : Me.lbl_Bac.Text = myBloc(CLE)
            CLE = "NAME" : Me.lbl_BacNom.Text = myBloc(CLE)
            CLE = "MODIFYSH" : Me.btn_ModifierBac.Text = myBloc(CLE)
            CLE = "RIBORIENT" : Me.lbl_BacOrientation.Text = myBloc(CLE)
            CLE = "PARALLEL" : Me.rdb_BacParallele.Text = myBloc(CLE)
            CLE = "PERPENDICULAR" : Me.rdb_BacPerpendiculaire.Text = myBloc(CLE)

            CLE = "HEIGHT" : strHauteur = myBloc(CLE)
            CLE = "SWITCH" : Me.lbl_Decalage.Text = myBloc(CLE)

            CLE = "RIBCONFIG" : Me.lbl_BacConfiguration.Text = myBloc(CLE)              '"Configuration des nervures sur appui"

            CLE = "CONNECTORANDSHEET" : Me.lbl_ConnectorThroughTheWeb.Text = myBloc(CLE)
            CLE = "THROUGHDECKW" : Me.rdb_ATraversBac.Text = myBloc(CLE)
            CLE = "PREPUNCHED" : Me.rdb_Preperce.Text = myBloc(CLE)

            CLE = "CONTINUOUSRIBANDDECK" : Me.strAppuiTcontinus = myBloc(CLE)
            CLE = "CONTINUOUSRIB" : Me.strAppuiTRibContinu = myBloc(CLE)
            CLE = "DISCONTINUOUSDECK" : Me.strAppuiTBacNonContinu = myBloc(CLE)
            CLE = "DISCONTINUOUSRIBDECK" : Me.strAppuiTDiscontinus = myBloc(CLE)

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
        End Try

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

    End Sub

    Private Sub GestionStyle()

        Me.Pan_Main.Dock = DockStyle.Fill

        Me.rtxt_Configuration.BorderStyle = BorderStyle.None


        Me.lbl_Bac.BackColor = CouleurBackBandeaux
        Me.lbl_Bac.ForeColor = CouleurForeBandeaux

        '# Blocage des txtbox non modifiables

        Me.txt_BacNom.Enabled = False
        Me.txt_BacNom.BackColor = CouleurReadOnly

        '  PrepareTextBoxDipo(txt_Hp, False)

        Me.img_Bac.BorderStyle = BorderStyle.FixedSingle

    End Sub

    Private Sub PrepareFenetre()

        FontFrm = New Font(FontBase.Name, SizeFontFrm)

        Me.pan_Bac.Controls.Add(Me.pan_DecalageCofra220)

        Me.pan_DecalageCofra220.Left = Me.pan_ConfigurationNervures.Left
        Me.pan_DecalageCofra220.Top = Me.pan_ConfigurationNervures.Top
        Me.pan_DecalageCofra220.Width = Me.pan_ConfigurationNervures.Width

        Me.txt_DecalCofra220.Text = Frm_DalleN.myDalleLoc.Bac.eDecalage

    End Sub

    Private Sub AfficherBacEnCours()

        MAJI_ChangeBac()
        MAJI_PanDispoConnecteur()

        ''' -> Orientation
        Select Case Frm_DalleN.myDalleLoc.Bac.Orientation
            Case cls_Bac.Enum_Orientation.Parallele
                Me.rdb_BacParallele.Checked = True
            Case cls_Bac.Enum_Orientation.Perpendiculaire
                Me.rdb_BacPerpendiculaire.Checked = True
        End Select
        Me.txt_DecalCofra220.Text = GetStringInUnitN(Frm_DalleN.myDalleLoc.Bac.eDecalage, Enu_TypeVariable.Dimension, 4, 3, Enu_AfficheUnite.Non, True)

        '--> Bac prépercé
        If Frm_DalleN.myDalleLoc.Bac.lPreperce Then
            Me.rdb_Preperce.Checked = True
        Else
            Me.rdb_ATraversBac.Checked = True
        End If


        'Select Case Frm_DalleN.myDalleLoc.Bac.AppuiL 'Modig GuD: Réinitialise les chkbox, il y'a des cas où plusieurs checkbox étaient sélectionnés
        '    Case cls_Bac.EnuConfigLAppui.BacCoupe
        '        Me.chk_L_PA2.Checked = True
        '        UnselectChkTConfig(Me.chk_L_PA2.Name)

        '    Case cls_Bac.EnuConfigLAppui.BacNonCoupe
        '        Me.chk_L_PA1.Checked = True
        '        UnselectChkTConfig(Me.chk_L_PA1.Name)
        'End Select

        Select Case Frm_DalleN.myDalleLoc.Bac.AppuiT
            Case cls_Bac.EnuConfigTAppui.BetonSeulContinu
                Me.chk_T_PA2.Checked = True
                UnselectChkTConfig(Me.chk_T_PA2.Name)

            Case cls_Bac.EnuConfigTAppui.Discontinu
                Me.chk_T_PA3.Checked = True
                UnselectChkTConfig(Me.chk_T_PA3.Name)

            Case cls_Bac.EnuConfigTAppui.NervureEtBacContinus
                Me.chk_T_PA1.Checked = True
                UnselectChkTConfig(Me.chk_T_PA1.Name)
        End Select

        '--> Configurations sur appui
        MAJI_ConfigurationAppuiBac()
        MAJI_OrientationBac()
    End Sub

#End Region

#Region " Routines MAJI "
    Private Sub UnselectChkTConfig(NameSelect As String)
        Dim lbuildBack As Boolean = lBuild
        lBuild = True
        If Me.chk_T_PA1.Name <> NameSelect Then Me.chk_T_PA1.Checked = False
        If Me.chk_T_PA2.Name <> NameSelect Then Me.chk_T_PA2.Checked = False
        If Me.chk_T_PA3.Name <> NameSelect Then Me.chk_T_PA3.Checked = False
        lBuild = lbuildBack
    End Sub

    Private Sub MAJI_ChangeBac()
        lCofraPlus220 = Frm_DalleN.MyDalleLoc.Bac.lCofraplus220
        If lCofraPlus220 Then
            Me.rdb_BacPerpendiculaire.Checked = True
            Me.rdb_BacParallele.Visible = False
            Me.pan_ConfigurationNervures.Visible = False
            'Me.pan_DispoConnecteur.Visible = False
            Me.pan_DecalageCofra220.Visible = True
        Else
            Me.rdb_BacParallele.Visible = True
            Me.pan_DispoConnecteur.Visible = True
            ' Me.pan_ConfigurationNervures.Visible = True
            Me.pan_DecalageCofra220.Visible = False
        End If

        MAJI_OrientationBac()
    End Sub

    Private Sub MAJI_OrientationBac()

        Me.chk_L_PA1.Visible = (Frm_DalleN.myDalleLoc.Bac.Orientation = cls_Bac.Enum_Orientation.Parallele)
        Me.chk_L_PA2.Visible = (Frm_DalleN.myDalleLoc.Bac.Orientation = cls_Bac.Enum_Orientation.Parallele)
        Me.chk_T_PA1.Visible = (Frm_DalleN.MyDalleLoc.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire)
        Me.chk_T_PA2.Visible = (Frm_DalleN.MyDalleLoc.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire)
        Me.chk_T_PA3.Visible = (Frm_DalleN.MyDalleLoc.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire)

        Me.pan_DispoConnecteur.Visible = (Frm_DalleN.MyDalleLoc.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire) And Not lCofraPlus220
        Me.pan_ConfigurationNervures.Visible = (Frm_DalleN.MyDalleLoc.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire) And Not lCofraPlus220
    End Sub

    Private Sub MAJI_SaisieEpMixte()

        'PrepareTextBoxDipo(Me.txt_Td2, DefEpMixte = Enu_DefEpMixte.Totale)
        'PrepareTextBoxDipo(Me.txt_Tc, DefEpMixte = Enu_DefEpMixte.Pleine)

    End Sub

    Private Sub MAJI_ConfigurationAppuiBac()
        '----------------------------------------------------------------------------------------------------------------
        '   Mise à jour du texte d'explication en fct de la configuration d'appui du bac
        '----------------------------------------------------------------------------------------------------------------

        Select Case Frm_DalleN.myDalleLoc.Bac.Orientation
            Case cls_Bac.Enum_Orientation.Perpendiculaire
                Select Case Frm_DalleN.myDalleLoc.Bac.AppuiT
                    Case cls_Bac.EnuConfigTAppui.NervureEtBacContinus
                        Me.rtxt_Configuration.Text = strAppuiTcontinus
                    Case cls_Bac.EnuConfigTAppui.BetonSeulContinu
                        Me.rtxt_Configuration.Text = strAppuiTRibContinu & Chr(13) & strAppuiTBacNonContinu
                    Case cls_Bac.EnuConfigTAppui.Discontinu
                        Me.rtxt_Configuration.Text = strAppuiTDiscontinus
                End Select

            Case cls_Bac.Enum_Orientation.Parallele
                Select Case Frm_DalleN.myDalleLoc.Bac.AppuiL
                    Case cls_Bac.EnuConfigLAppui.BacCoupe
                        Me.rtxt_Configuration.Text = strAppuiLbacCut1 & Chr(13) & strAppuiLbacCut2
                    Case cls_Bac.EnuConfigLAppui.BacNonCoupe
                        Me.rtxt_Configuration.Text = strAppuiLbacUncut
                End Select
        End Select

    End Sub

#End Region

#Region " Evènements "

    Private Sub ConfigTCheckedChanged(sender As Object, e As EventArgs) Handles chk_T_PA3.CheckedChanged, chk_T_PA2.CheckedChanged, chk_T_PA1.CheckedChanged, chk_L_PA2.CheckedChanged, chk_L_PA1.CheckedChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.chk_T_PA1.Name

                Frm_DalleN.myDalleLoc.Bac.AppuiT = cls_Bac.EnuConfigTAppui.NervureEtBacContinus
                UnselectChkTConfig(Me.chk_T_PA1.Name)

            Case Me.chk_T_PA2.Name

                Frm_DalleN.myDalleLoc.Bac.AppuiT = cls_Bac.EnuConfigTAppui.BetonSeulContinu
                UnselectChkTConfig(Me.chk_T_PA2.Name)

            Case Me.chk_T_PA3.Name

                Frm_DalleN.myDalleLoc.Bac.AppuiT = cls_Bac.EnuConfigTAppui.Discontinu
                UnselectChkTConfig(Me.chk_T_PA3.Name)

        End Select

        MAJI_ConfigurationAppuiBac()
        Frm_DalleN.MAJI_ImageDalle()
        MAJI_PanDispoConnecteur()
    End Sub

    Private Sub MAJI_PanDispoConnecteur()

        Me.pan_DispoConnecteur.Visible = Not (Frm_DalleN.myDalleLoc.Bac.AppuiT = cls_Bac.EnuConfigTAppui.Discontinu)

    End Sub

    Private Sub OrientationBac_checkedChanged(sender As Object, e As EventArgs) Handles rdb_BacParallele.CheckedChanged, rdb_BacPerpendiculaire.CheckedChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.rdb_BacParallele.Name : Frm_DalleN.myDalleLoc.Bac.Orientation = cls_Bac.Enum_Orientation.Parallele
            Case Me.rdb_BacPerpendiculaire.Name : Frm_DalleN.myDalleLoc.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire
        End Select
        MAJI_ChangeBac()
        Frm_DalleN.img_Dalle.Invalidate()

    End Sub

    Private Sub rdb_Preperce_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Preperce.CheckedChanged, rdb_ATraversBac.CheckedChanged
        If lBuild Then Exit Sub
        Frm_DalleN.myDalleLoc.Bac.lPreperce = Me.rdb_Preperce.Checked
    End Sub

    Private Sub txt_DecalCofra220_TextChanged(sender As Object, e As EventArgs) Handles txt_DecalCofra220.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name

                Case Me.txt_DecalCofra220.Name
                    Frm_DalleN.myDalleLoc.Bac.eDecalage = Valeur

            End Select

            Frm_DalleN.MAJI_ImageDalle()
        End If

    End Sub

    ''' <summary>
    ''' Vérification de la saisie des paramètres
    ''' </summary>
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

            Case txt_DecalCofra220.Name
                ValMin = 0 / kUnit
                ValMax = OptionsDalle.DecalageMax / kUnit

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


    Private Sub btn_ModifierBac_Click(sender As Object, e As EventArgs) Handles btn_ModifierBac.Click, txt_BacNom.Click, img_Bac.Click

        Dim Tc As Decimal = Frm_DalleN.myDalleLoc.EpaisseurActive
        Dim lOldCfp220 As Boolean = lCofraPlus220

        iFrmAppel = EnuFenetres.DalleN
        Frm_BacN.ShowDialog()

        MAJI_ChangeBac()
        If lCofraPlus220 <> lOldCfp220 Then
            lBuild = True
            Dim Td As Decimal = Tc + Frm_DalleN.myDalleLoc.Bac.Hp
            Frm_DalleN.myDalleLoc.Ep_td = Td
            'Me.txt_Td2.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.Ep_td, Enu_TypeVariable.Dimension)
            'Me.rdb_EpPleine.Checked = True
            'DefEpMixte = Enu_DefEpMixte.Pleine
            MAJI_SaisieEpMixte()
            lBuild = False
        End If

        AfficheNomBacEnCours()
        Me.img_Bac.Invalidate()
        Frm_DalleN.MAJI_ImageDalle()

    End Sub

    Private Sub txt_DecalCofra220_Enter(sender As Object, e As EventArgs) Handles txt_DecalCofra220.Enter
        If lBuild Then Exit Sub
        Frm_DalleN.MAJI_SelectionTxtbox(401)
    End Sub


    Private Sub AfficheNomBacEnCours()
        Me.txt_BacNom.Text = Frm_DalleN.myDalleLoc.Bac.Etiquette
        '  Me.txt_Hp.Text = GetStringInUnitN(Frm_DalleN.MyDalleLoc.Bac.Hp, Enu_TypeVariable.Dimension, 4, 3, Enu_AfficheUnite.Non, True)
    End Sub

#End Region

#Region " Dessins "

    Private Sub img_Bac_Paint(sender As Object, e As PaintEventArgs) Handles img_Bac.Paint
        DessineBacTout(e.Graphics, Me.img_Bac.ClientRectangle.Width, Me.img_Bac.ClientRectangle.Height, Frm_DalleN.MyDalleLoc.Bac,
                       True, kADJUST, True, FontFrm, strhauteur)
    End Sub

#End Region

#Region " Symboles "
    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_Decal.Paint
        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height
        Dim xStart As Single = sWI * 0.95

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lIndice, lEgal As Boolean
        Dim xPen As Single = xStart
        Dim hCar As Single = e.Graphics.MeasureString("X", FontSymbolNormal).Height
        Dim yPen As Single = (sHI / 2 - hCar) / 2 + sHI * 0.15
        Dim AlignH As Enu_AlignementH = Enu_AlignementH.Droite

        '--> Initialisation
        lIndice = False
        lGrec = False
        lEgal = True

        strSymbol = "e"
        strIndice = "220"


        '--> Dessin
        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)
    End Sub

#End Region
End Class