Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip
Imports PMXMoteur2

Public Class Frm_DalleNBac

#Region " Variables locales "

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

    End Sub

    Private Sub AfficherBacEnCours()

        AfficheNomBacEnCours()

        Me.txt_DecalCofra220.Text = GetStringInUnitN(Frm_DalleN.myDalleLoc.Bac.eDecalage, Enu_TypeVariable.Dimension, 4, 3, Enu_AfficheUnite.Non, True)

    End Sub

#End Region

#Region " Routines MAJI "

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

        Me.chk_L_PA1.Visible = False '   (MyDalleLoc.Bac.orientation = Cls_Bac.Enum_Orientation.Parallele)
        Me.chk_L_PA2.Visible = False '   (MyDalleLoc.Bac.orientation = Cls_Bac.Enum_Orientation.Parallele)
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

#End Region

#Region " Evènements "

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
                ValMax = DECMAX / kUnit

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

        Dim Tc As Decimal = Frm_DalleN.MyDalleLoc.EpaisseurActive
        Dim lOldCfp220 As Boolean = lCofraPlus220

        iFrmAppel = EnuFenetres.DalleN
        Frm_BacN.ShowDialog()

        MAJI_ChangeBac()
        If lCofraPlus220 <> lOldCfp220 Then
            lBuild = True
            Dim Td As Decimal = Tc + Frm_DalleN.MyDalleLoc.Bac.Hp
            Frm_DalleN.MyDalleLoc.Ep_td = Td
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

    Private Sub AfficheNomBacEnCours()
        Me.txt_BacNom.Text = Frm_DalleN.MyDalleLoc.Bac.Etiquette
        '  Me.txt_Hp.Text = GetStringInUnitN(Frm_DalleN.MyDalleLoc.Bac.Hp, Enu_TypeVariable.Dimension, 4, 3, Enu_AfficheUnite.Non, True)
    End Sub

#End Region


#Region " Dessins "

    Private Sub img_Bac_Paint(sender As Object, e As PaintEventArgs) Handles img_Bac.Paint
        DessineBacTout(e.Graphics, Me.img_Bac.ClientRectangle.Width, Me.img_Bac.ClientRectangle.Height, Frm_DalleN.MyDalleLoc.Bac,
                       True, kADJUST, True, FontFrm, strhauteur)
    End Sub

#End Region
End Class