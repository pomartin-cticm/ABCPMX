Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip
Imports PMXMoteur2

Public Class Frm_DalleN

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim Bloc As New Dictionary(Of String, String)

    Dim FontFrm As Font
    Dim iSelect As Integer = -1
    Dim lCote As Boolean = True 'indique si on affiche les cotations ou non

    Dim msgDessin(1) As String

    Dim strType(2) As String

    Public myPoutreLoc As New cls_Poutre(NomChargements)
    Public myDalleLoc As New cls_Dalle
    'Dim COULEURTXTREADONLY As Color = SystemColors.ControlDark
    Const kADJUST As Decimal = 0.95

    Enum Enu_AffParam
        General
        Armatures
        Bac
    End Enum

    Dim AffParam As Enu_AffParam = Enu_AffParam.General

    Dim lCofraPlus220 As Boolean

    Dim strTauxArma As String = ""
    Dim strErreurLeger As String = ""
    Dim strErreurNormal As String = ""
    Dim strErreurEnrob(1) As String

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Basic_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Private Sub InitialiserFenetre()

        lBuild = True
        GestionLangues()
        GestionStyle()
        GestionUnites()
        InitialisationVariablesLocales()
        PreparerFenetre()
        AffichageFille()
        lBuild = False

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

                CLE = "ERRORLWC" : strErreurLeger = Bloc(CLE)
                CLE = "ERRORNWC" : strErreurNormal = Bloc(CLE)

                CLE = "ERRORCCOVER1" : strErreurEnrob(0) = Bloc(CLE)
                CLE = "ERRORCCOVER2" : strErreurEnrob(1) = Bloc(CLE)

                CLE = "REINFRATIO" : strTauxArma = Bloc(CLE)

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
            Finally

            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If
    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        FontFrm = New Font(FontBase.Name, SizeFontFrm)

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub InitialisationVariablesLocales()

        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)

        MyDalleLoc = MyPoutreLoc.Dalle

        lCofraPlus220 = MyDalleLoc.Bac.lCofraplus220

    End Sub

    Private Sub PreparerFenetre()

        '  Me.TLpan_Choix.ColumnStyles(1).Width = 0

        Me.TLpan_Images.Dock = DockStyle.Fill
        Me.img_Dalle.Dock = DockStyle.Fill

        If MyPoutreLoc.lMultiSpan Then
            Me.TLpan_Images.RowStyles(1).Height = 32
        Else
            Me.TLpan_Images.RowStyles(1).Height = 0
        End If

        Select Case AffParam
            Case Enu_AffParam.Armatures
                Me.rdb_Arma.Checked = True
            Case Enu_AffParam.Bac
                Me.rdb_Bac.Checked = True
            Case Enu_AffParam.General
                Me.rdb_General.Checked = True
        End Select

        MAJI_TauxArma()
    End Sub

#End Region

#Region " Events gestion des boutons "

    Private Sub rdb_CheckedChanged(sender As Object, e As EventArgs) _
        Handles rdb_Arma.CheckedChanged, rdb_General.CheckedChanged, rdb_Bac.CheckedChanged

        If lBuild Then Exit Sub

        Select Case True
            Case Me.rdb_Arma.Checked
                AffParam = Enu_AffParam.Armatures
            Case Me.rdb_Bac.Checked
                AffParam = Enu_AffParam.Bac
            Case Me.rdb_General.Checked
                AffParam = Enu_AffParam.General
        End Select

        AffichageFille()


    End Sub

    Private Sub AffichageFille()

        Me.pan_ContenuFille.Controls.Clear()

        Select Case AffParam
            Case Enu_AffParam.General
                Me.pan_ContenuFille.Controls.Add(Frm_DalleNGeneral.Pan_Contenu)
                Frm_DalleNGeneral.InitialiseFenetre(Bloc)
            Case Enu_AffParam.Armatures
                Me.pan_ContenuFille.Controls.Add(Frm_DalleNArma.Pan_Contenu)
                Frm_DalleNArma.InitialiseFenetre(Bloc)
            Case Enu_AffParam.Bac
                Me.pan_ContenuFille.Controls.Add(Frm_DalleNBac.Pan_Main)
                Frm_DalleNBac.InitialiseFenetre(Bloc)

        End Select

        iSelect = -1
        MAJI_ImageDalle()

    End Sub

#End Region

#Region " Routines MAJI "

    Public Sub MAJI_TauxArma()

        Dim Taux As Decimal = MyDalleLoc.TauxArma * 100

        Me.lbl_TauxArma.Text = strTauxArma & " : " & GetStringInUnitN(Taux, Enu_TypeVariable.SansType, 3, 2, NON_U, True) & " %"

    End Sub

    Public Sub MAJI_TypeDalle()

        Me.rdb_Bac.Enabled = (MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.Mixte)

    End Sub

    Public Sub MAJI_ImageDalle()

        Me.img_Dalle.Invalidate()

    End Sub

    Public Sub MAJI_SelectionTxtbox(pSelect As Integer)

        iSelect = pSelect

        Me.img_Dalle.Invalidate()

    End Sub

    Public Sub MAJI_DeselectionTxtbox()

        iSelect = -1

        Me.img_Dalle.Invalidate()

    End Sub

#End Region

#Region " Dessins "

    Private Sub img_Dalle_Paint(sender As Object, e As PaintEventArgs) Handles img_Dalle.Paint
        DessineDalle(e.Graphics, Me.img_Dalle.ClientRectangle.Width, Me.img_Dalle.ClientRectangle.Height,
                     myPoutreLoc, FontFrm, MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire, iSelect, msgDessin, lCote)
    End Sub

#End Region

#Region " Evènements divers "
    Private Sub Frm_DalleN_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Me.MAJI_ImageDalle()
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

            'MyProjet.Poutres(MyProjet.IndEnCours).EstValidee(iFRMslab)

            Me.Close()
        End If
    End Sub


    Private Function ValideSaisieFenetre() As Boolean

        '--( Déclaration

        Dim lOK As Boolean = True

        '--( Propriétés du béton

        ValideSaisieBeton(lOK, True)

        '--( Enrobage des armatures

        ValideSaisieEnrobageArma(lOK)

        Return lOK
    End Function

    Private Sub ValideSaisieEnrobageArma(ByRef lOK As Boolean)
        '----------------------------------------------------------------------------------------------------------------------------
        '   24/01/25 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------------
        '   Vérifie les valeurs d'enrobage des armatures
        '----------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim iArma As Integer
        Dim zArma As Decimal
        Dim zTop As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).Dalle.zTop
        Dim cTop, cBot As Decimal
        Dim PhiS As Decimal
        Dim Tc As Decimal = MyDalleLoc.EpaisseurActive
        Dim ZBot As Decimal = zTop - Tc
        Dim cMin As Decimal
        Dim EnrobMin As Decimal
        Dim Chaine As New List(Of String)
        Dim ChaineArma As String = ""
        Dim strEnrob As String
        Dim strEnrobMin As String

        '--( Traitement des armatures

        For iArma = 0 To MyDalleLoc.LitArma.Count - 1
            If MyDalleLoc.LitArma(iArma).lActive Then

                '# position et diamètre
                zArma = zTop - MyDalleLoc.LitArma(iArma).z_s
                PhiS = MyDalleLoc.LitArma(iArma).PhiS

                '# valeurs d'enrobage en dessous et au dessus
                cTop = zTop - zArma - PhiS / 2
                cBot = zArma - ZBot - PhiS / 2

                '# enrobage min
                cMin = Math.Max(10 / 1000, PhiS)
                EnrobMin = cMin + OptionsCalcul.DeltaCDev

                If IsSmaller(cTop, EnrobMin) Or IsSmaller(cBot, EnrobMin) Then

                    strEnrob = GetStringInUnitN(Math.Min(cTop, cBot), Enu_TypeVariable.Dimension, 4, 3, Enu_AfficheUnite.OuiInterface, True)
                    strEnrobMin = GetStringInUnitN(EnrobMin, Enu_TypeVariable.Dimension, 4, 3, Enu_AfficheUnite.OuiInterface, True)

                    ChaineArma = RemplaceDollar(strErreurEnrob(0), CStr(iArma + 1)) & " : "

                    ChaineArma += RemplaceDollar(RemplaceDollar(strErreurEnrob(1), strEnrob), strEnrobMin)
                    Chaine.Add(ChaineArma)
                End If

            End If
        Next

        If Chaine.Count > 0 Then
            lOK = False
            Dim Rep As MsgBoxResult
            Dim ChaineMsgBox As String = Chaine(0)
            For iArma = 1 To Chaine.Count - 1
                ChaineMsgBox += Chr(13) & Chaine(iArma)
            Next
            Rep = MsgBox(ChaineMsgBox, MsgBoxStyle.OkCancel, LogicielInfo.Racine)
            lOK = (Rep = MsgBoxResult.Ok)
        End If

    End Sub

    Private Sub ValideSaisieBeton(ByRef lOK As Boolean, lAffMesg As Boolean)
        '----------------------------------------------------------------------------------------------------------------------------
        '   24/01/25 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------------
        '   Vérifie les valeurs de Rho béton
        '----------------------------------------------------------------------------------------------------------------------------



        '--( Déclarations
        Dim valMax, valMin As Decimal
        Dim iErreur As Integer
        Const lValMin As Boolean = True
        Dim lValMax As Boolean = False
        Dim MessageErreur As String = ""
        Dim strValMin As String = ""
        Dim strValMax As String = ""

        '--( Traitement

        If MyDalleLoc.beton.lLeger Then
            lValMax = True
            valMax = OptionsScope.RhoCBetonLegerMax
            valMin = OptionsScope.RhoCBetonLegerMin
        Else
            valMin = OptionsScope.RhoCBetonNormalMin
        End If

        '  iErreur = ValideSaisieNombre(Me.txt_RhoC.Text, lValMin, valMin, lValMax, valMax)

        If iErreur = -3 Then
            lOK = False
            If MyDalleLoc.beton.lLeger Then
                strValMin = GetStringInUnitN(OptionsScope.RhoCBetonLegerMin, Enu_TypeVariable.MasseVolumique, 4, 1, Enu_AfficheUnite.OuiInterface, True)
                strValMax = GetStringInUnitN(OptionsScope.RhoCBetonLegerMax, Enu_TypeVariable.MasseVolumique, 4, 1, Enu_AfficheUnite.OuiInterface, True)
                MessageErreur = RemplaceDollar(RemplaceDollar(strErreurLeger, strValMin), strValMax)
            Else
                strValMin = GetStringInUnitN(OptionsScope.RhoCBetonNormalMin, Enu_TypeVariable.MasseVolumique, 4, 1, Enu_AfficheUnite.OuiInterface, True)
                MessageErreur = RemplaceDollar(strErreurNormal, strValMin)
            End If
            '    ErrorProvider.SetError(Me.txt_RhoC, MessageErreur)
        ElseIf iErreur = 0 Then
            '      ErrorProvider.Clear()
        End If

    End Sub

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False

        '-- Type et géométrie dalle ----------------------------------------------------------------------------------------------------

        If (MyDalleLoc.type <> MyProjet.Poutres(MyProjet.IndEnCours).Dalle.type) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.type = MyDalleLoc.type
        End If

        GereTransfertValeur(MyDalleLoc.Ep_td, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Ep_td, lModif)

        If MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.Pleine Then _
        GereTransfertValeur(MyDalleLoc.Ep_th, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Ep_th, lModif)

        If MyDalleLoc.type = cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee Then
            GereTransfertValeur(MyDalleLoc.preDalle_ep, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.preDalle_ep, lModif)
            GereTransfertValeur(MyDalleLoc.preDalle_tjoint, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.preDalle_tjoint, lModif)
        End If

        '--> Béton de la dalle

        GereTransfertValeur(MyDalleLoc.beton.lLeger, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.beton.lLeger, lModif)
        GereTransfertValeur(MyDalleLoc.beton.RhoC, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.beton.RhoC, lModif)

        GereTransfertValeur(MyDalleLoc.beton.Classe, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.beton.Classe, lModif)

        '--> Acier des armatures

        GereTransfertValeur(MyDalleLoc.AcierArmatures.Classe, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.AcierArmatures.Classe, lModif)

        '--> Bac

        MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Copie(MyDalleLoc.Bac, lModif)
        MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.CopieAutresParam(MyDalleLoc.Bac, lModif)

        '--> Armatures

        ' GereTransfertValeur(MyDalleLoc.NbLitsArmaActifs, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.NbLitsArmaActifs, lModif)
        GereTransfertValeur(MyDalleLoc.lNoArma, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.lNoArma, lModif)

        For i As Integer = 0 To 1

            GereTransfertValeur(MyDalleLoc.LitArma(i).PhiS, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.LitArma(i).PhiS, lModif)
            GereTransfertValeur(MyDalleLoc.LitArma(i).lActive, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.LitArma(i).lActive, lModif)
            GereTransfertValeur(MyDalleLoc.LitArma(i).EspBar, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.LitArma(i).EspBar, lModif)
            GereTransfertValeur(MyDalleLoc.LitArma(i).z_s, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.LitArma(i).z_s, lModif)
            GereTransfertValeur(MyDalleLoc.LitArma(i).lActive, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.LitArma(i).lActive, lModif)

        Next

    End Sub

#End Region

End Class