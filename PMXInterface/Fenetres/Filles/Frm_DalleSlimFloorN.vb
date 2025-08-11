Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip
Imports PMXInterface.Frm_ConnectionSlimN
Imports PMXMoteur2

Public Class Frm_DalleSlimFloorN

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim Bloc As New Dictionary(Of String, String)

    Dim ClasseAcierArma() As String = cls_AcierArmature.tabClasseAcierArma

    Dim localBeam As New cls_Poutre(NomChargements)
    Public localDalle As New cls_Dalle

    Const kADJUST As Decimal = 0.95

    Dim lCofraPlus220 As Boolean
    Dim DefautWrdb As Integer

    Enum Enu_AffParamSlim
        General
        Armatures
        Bac
    End Enum

    Dim AffParam As Enu_AffParamSlim = Enu_AffParamSlim.General

    Dim msgDessin(1) As String
    Dim lCote As Boolean = True 'indique si on affiche les cotations ou non

    Dim iSelect As Integer = -1

    Dim myFontFrm As New Font(FontBase.Name, SizeFontFrm)

#End Region

#Region "===OUVERTURE==="
    Private Sub Frm_DalleSlimFloorN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True
        GestionLangues()
        GestionStyle()
        'GestionUnites()
        InitialisationVariablesLocales()
        PrepareFenetre()
        'AfficherDalleEnCours()
        AffichageFille()
        lBuild = False
    End Sub

    Private Sub InitialisationVariablesLocales()

        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), localBeam)

        localDalle = localBeam.Dalle

        lCofraPlus220 = localDalle.Bac.lCofraplus220

        DefautWrdb = Me.TLpan_Choix.ColumnStyles(2).Width
    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon
        Me.img_Dalle.Dock = DockStyle.Fill

    End Sub

    Private Sub PrepareFenetre()

        Select Case AffParam
            Case Enu_AffParamSlim.Armatures : Me.rdb_Arma.Checked = True
            Case Enu_AffParamSlim.Bac : Me.rdb_Bac.Checked = True
            Case Enu_AffParamSlim.General : Me.rdb_General.Checked = True
        End Select
        MAJI_RdbTypeDalle()
    End Sub

    Public Sub MAJI_RdbTypeDalle()
        '---------------------------------------------------------------------------------------
        '   On cache le bouton bac pour les dalles sans bac
        '---------------------------------------------------------------------------------------

        Dim lBac As Boolean

        Select Case localDalle.type
            Case cls_Dalle.Enum_TypeDalle.Mixte
                lBac = True
                'Case cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
            Case Else
                lBac = False
        End Select

        If lBac Then
            Me.TLpan_Choix.ColumnStyles(2).Width = DefautWrdb
        Else
            Me.TLpan_Choix.ColumnStyles(2).Width = 0
        End If

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim strLoadedKey As String = ""
            Dim CLE As String = ""

            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_SLAB")
            BlocLine.CreationBloc(Bloc, strLoadedKey)

            Try

                '=== FENETRE =====================================================================

                CLE = "TITLE" : Me.Text = Bloc(CLE)
                CLE = "OK" : Me.btn_OK.Text = Bloc(CLE)
                CLE = "CANCEL" : Me.btn_Annuler.Text = Bloc(CLE)

                Dim str_TableCofra() As String = cls_Cofradal.Get_ListName_Cofradal()

                CLE = "RDB_REINFORCEMENT" : Me.rdb_Arma.Text = Bloc(CLE)
                CLE = "RDB_SHEET" : Me.rdb_Bac.Text = Bloc(CLE)
                CLE = "RDB_MAIN" : Me.rdb_General.Text = Bloc(CLE)

                CLE = "CONCRETE" : msgDessin(0) = Bloc(CLE)
                CLE = "REBARSTEEL" : msgDessin(1) = Bloc(CLE)

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
            Finally

            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If

    End Sub

#End Region

#Region " Outils "

    Public Function LocalSectionHec() As Decimal

        Return localBeam.Section.hec

    End Function

#End Region

#Region " Events gestion des boutons "

    Private Sub rdb_CheckedChanged(sender As Object, e As EventArgs) _
        Handles rdb_Arma.CheckedChanged, rdb_General.CheckedChanged

        If lBuild Then Exit Sub

        Select Case True
            Case Me.rdb_Arma.Checked
                AffParam = Enu_AffParamSlim.Armatures
            Case Me.rdb_Bac.Checked
                AffParam = Enu_AffParamSlim.Bac
            Case Me.rdb_General.Checked
                AffParam = Enu_AffParamSlim.General
        End Select

        AffichageFille()

    End Sub

    Private Sub AffichageFille()

        Me.pan_ContenuFille.Controls.Clear()

        Select Case AffParam
            Case Enu_AffParamSlim.General
                Me.pan_ContenuFille.Controls.Add(Frm_DalleSlimFloorNGeneral.pan_Main)
                Frm_DalleSlimFloorNGeneral.InitialiseFenetre(Bloc, localBeam.lIntermediaire)
            Case Enu_AffParamSlim.Armatures
                Me.pan_ContenuFille.Controls.Add(Frm_DalleSlimFloorNArma.pan_Main)
                Frm_DalleSlimFloorNArma.InitialiseFenetre(Bloc)
            Case Enu_AffParamSlim.Bac
                Me.pan_ContenuFille.Controls.Add(Frm_DalleSlimFloorNBac.pan_Main)
                Frm_DalleSlimFloorNBac.InitialiseFenetre(Bloc)

        End Select

    End Sub

#End Region

#Region " Gestion Evenements fenêtres filles "

    Public Sub MAJI_ProprietesBeton()

        Me.localDalle.beton.Calcul_Proprietes(localBeam.Param.lGeneration1)

    End Sub

#End Region

#Region " Dessins "

    Private Sub img_Dalle_Paint(sender As Object, e As PaintEventArgs) Handles img_Dalle.Paint

        DessineDalleFrmDalleSlimFloor(e.Graphics, Me.img_Dalle.ClientRectangle.Width, Me.img_Dalle.ClientRectangle.Height,
                                      localBeam, myFontFrm, localBeam.lIntermediaire, iSelect, msgDessin, lCote)

        'DessineDalle(e.Graphics, Me.img_Dalle.ClientRectangle.Width, Me.img_Dalle.ClientRectangle.Height,
        '             localBeam, myFontFrm, localBeam.lIntermediaire, iSelect, msgDessin, lCote)

    End Sub

    Public Sub RedessineDalle()

        Me.img_Dalle.Invalidate()

    End Sub

    Public Sub Gestion_iSelect(myIselect As Integer)

        iSelect = myIselect
        RedessineDalle()

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


    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False

        '-- Type et géométrie dalle ----------------------------------------------------------------------------------------------------

        If (localDalle.type <> MyProjet.Poutres(MyProjet.IndEnCours).Dalle.type) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.type = localDalle.type
        End If

        GereTransfertValeur(localDalle.lRiveRemplie, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.lRiveRemplie, lModif)
        GereTransfertValeur(localDalle.Ep_td, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Ep_td, lModif)

        If localDalle.type = cls_Dalle.Enum_TypeDalle.Pleine Then _
        GereTransfertValeur(localDalle.Ep_th, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Ep_th, lModif)

        If localDalle.type = cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee Then
            GereTransfertValeur(localDalle.preDalle_ep, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.preDalle_ep, lModif)
            GereTransfertValeur(localDalle.preDalle_tjoint, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.preDalle_tjoint, lModif)
        End If

        '--> Béton de la dalle

        GereTransfertValeur(localDalle.beton.lLeger, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.beton.lLeger, lModif)
        GereTransfertValeur(localDalle.beton.RhoC, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.beton.RhoC, lModif)

        GereTransfertValeur(localDalle.beton.Classe, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.beton.Classe, lModif)

        '--> Acier des armatures

        GereTransfertValeur(localDalle.AcierArmatures.Classe, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.AcierArmatures.Classe, lModif)

        '--> Bac

        MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Copie(localDalle.Bac, lModif)
        MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.CopieAutresParam(localDalle.Bac, lModif)

        '--> Cofradal

        'If cmb_Cofradal.SelectedIndex = 0 And Me.txt_NameCustomCofra.Text = "" Then
        '    MyDalleLoc.Cofradal.nom = Me.cmb_Cofradal.Items(0) 'on ajoute un nom par défaut = User ou Utilisateur
        'End If

        GereTransfertValeur(localDalle.Cofradal.Nom, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Cofradal.Nom, lModif)
        GereTransfertValeur(localDalle.Cofradal.dp, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Cofradal.dp, lModif)
        GereTransfertValeur(localDalle.Cofradal.mSurf, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Cofradal.mSurf, lModif)
        GereTransfertValeur(localDalle.Cofradal.lCustom, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Cofradal.lCustom, lModif)

    End Sub

#End Region

End Class