Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip
Imports PMXMoteur2

Public Class Frm_DalleNBac

#Region " Variables locales "

    Dim lBuild As Boolean
    Dim lCofraPlus220 As Boolean

    Const kADJUST As Decimal = 0.95

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

            CLE = "RIBCONFIG" : Me.lbl_BacConfiguration.Text = myBloc(CLE)              '"Configuration des nervures sur appui"

            CLE = "CONNECTORANDSHEET" : Me.lbl_ConnectorThroughTheWeb.Text = myBloc(CLE)
            CLE = "THROUGHDECKW" : Me.rdb_ATraversBac.Text = myBloc(CLE)
            CLE = "PREPUNCHED" : Me.rdb_Preperce.Text = myBloc(CLE)

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
        End Try

    End Sub

    Private Sub GestionUnites()


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

    End Sub

    Private Sub AfficherBacEnCours()

        AfficheNomBacEnCours()

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
        Else
            Me.rdb_BacParallele.Visible = True
            Me.pan_DispoConnecteur.Visible = True
            ' Me.pan_ConfigurationNervures.Visible = True
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