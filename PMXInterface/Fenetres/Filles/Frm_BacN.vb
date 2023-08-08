Imports PMXInterface.Frm_Dalle
Imports PMXMoteur2
Imports System.Collections.Specialized.BitVector32
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices.ComTypes
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Frm_BacN


#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim MyBac As Cls_Bac

    Dim ProducteursBacs As New List(Of String)

    Const iFRMBAC As Integer = 15

    Dim SANSPROD As String = "SANS"

    Dim strAM As String = "ArcelorMittal"
    Dim strTous As String
    Dim lChoixBacReduit As Boolean
    Const kADJUST As Decimal = 0.95

    Dim COULEURTXTACCESS As Color = SystemColors.Window
    Dim COULEURTXTREADONLY As Color = SystemColors.ControlDark

    Dim iSelect As Integer = -1
#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Basic_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        lBuild = True
        GestionLangues()
        GestionStyle()
        GestionUnites()
        InitialiseVariablesLocales()
        PreparerControles()
        AfficherBacEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_PSHEETING")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== FENETRE =====================================================================

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== BASE DE DONNEES ==============================================================

                Me.lbl_Database.Text = Bloc("DATABASE")
                Me.rdb_BacBase.Text = Bloc("FROMDATABASE")
                Me.lbl_Producteur.Text = Bloc("COMPANY")

                '=== DIMENSIONS ===================================================================

                Me.lbl_Dimensions.Text = Bloc("PROPERTIES")
                Me.rdb_BacCustom.Text = Bloc("FROMDIM")

                Me.lbl_Nom.Text = Bloc("NAME")

                strTous = Bloc("ALL")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDimB1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDimB2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDimB3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDimB4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDimB5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDimB6.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitSigma1.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

        Me.etq_UnitMuP.Text = "kg/m2"

    End Sub

    Private Sub PreparerControles()

        RemplirComboProducteur()
        PrepareLookGrille(Me.Grid_Bac, Me.Col_ListeSup, Me.pan_CustomBac.BackColor)
        RemplirGrilleBac(Me.Grid_Bac)

        'Me.img_Bac.Dock = DockStyle.Fill

    End Sub

    Private Sub InitialiseVariablesLocales()
        If iFrmAppel = EnuFenetres.DalleN Then
            MyBac = Frm_DalleN.MyDalleLoc.Bac.Clone
        Else
            MyBac = MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Clone
        End If
        InitialiseProducteursBacs()
    End Sub

    Private Sub InitialiseProducteursBacs()

        For Each kvp As KeyValuePair(Of String, Cls_Bac) In BaseBacs

            Dim MyProd As String
            If kvp.Value.Producteur = "" Then
                MyProd = SANSPROD
            Else
                MyProd = kvp.Value.Producteur
            End If

            If Not (ProducteursBacs.Contains(MyProd)) Then

                ProducteursBacs.Add(MyProd)

            End If

        Next

    End Sub


    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_Database.BackColor = CouleurBackBandeaux
        Me.lbl_Database.ForeColor = CouleurForeBandeaux

        Me.lbl_Dimensions.BackColor = CouleurBackBandeaux
        Me.lbl_Dimensions.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub AfficherBacEnCours()

        If MyBac.lDatabase Then
            If Not BaseBacs.ContainsKey(MyBac.Etiquette) Then
                MyBac.lDatabase = False
            End If

            If lChoixBacReduit Then
                If MyBac.Producteur <> strAM Then
                    MyBac.lDatabase = False
                End If
            End If
        End If

        MAJI_OptionBac()

        Dim Etiquette As String = MyBac.Etiquette

        If MyBac.lDatabase Then
            Me.rdb_BacBase.Checked = True
            Me.rdb_BacCustom.Checked = False

            Dim iBac As Integer = 0
            Dim lTrouve As Boolean = False

            Do While iBac < Me.Grid_Bac.Rows.Count And (Not lTrouve)
                iBac += 1
                lTrouve = (Me.Grid_Bac(0, iBac - 1).Value.ToString.Trim = MyBac.Etiquette)
            Loop
            If lTrouve Then Me.Grid_Bac(0, iBac - 1).Selected = True

            AfficherParametresBac(MyBac)

        Else
            Me.rdb_BacCustom.Checked = True
            Me.rdb_BacBase.Checked = False

            Me.Grid_Bac(0, 0).Selected = True
            AfficherParametresBac(MyBac)

        End If


    End Sub

    Sub PrepareLookGrille(ByVal MyGrille As DataGridView, ByVal ColListe As DataGridViewTextBoxColumn, ByVal BackColor As Color)
        '--------------------------------------------------------------------------
        '
        '   Préparation du "Look" de la grille
        '
        '--------------------------------------------------------------------------
        '
        '   MyGrille    [E] :   Grille à préparer
        '   ColListe
        '   BackColor   [E] :   Color de fond à appliquer à la grille
        '
        '--------------------------------------------------------------------------

        MyGrille.CellBorderStyle = DataGridViewCellBorderStyle.None

        MyGrille.DefaultCellStyle.Padding = New Padding(0)
        MyGrille.Font = New Font(MyGrille.Font.FontFamily, 8, FontStyle.Regular, GraphicsUnit.Point)
        MyGrille.BorderStyle = BorderStyle.Fixed3D
        MyGrille.BackgroundColor = BackColor

        ColListe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        ColListe.Width = CInt(MyGrille.Width * (1))

    End Sub

    Private Sub RemplirGrilleBac(MyGrille As DataGridView)
        '-----------------------------------------------------------------------------------
        '   Remplissage d'une grille avec tous les profilés d'une gamme
        '-----------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------

        '--> Déclarations

        Dim lBuildBack As Boolean = lBuild
        Dim iPro As Integer = 0
        Dim Chaine As String
        Dim iColor As Integer = 0
        ' Dim BackColors() = {Color.LightGray, Color.Orange}
        Dim BackColors() = {Color.LightGray, Color.White}
        Dim ColorNA As Color = Color.Gray
        Dim ProducteurBac As String
        Dim ProducteurImpose As String = ""
        Dim lAffiche, lTous As Boolean

        '--> Initialisation

        lBuild = True

        lTous = (Not lChoixBacReduit) And (Me.cmb_Producteur.SelectedIndex = 0)

        If Me.cmb_Producteur.SelectedIndex > 0 Then
            ProducteurImpose = Me.ProducteursBacs(Me.cmb_Producteur.SelectedIndex - 1)
        End If

        MyGrille.Rows.Clear()

        '--> Boucle sur tous les profilés de la gamme

        For Each kVs As KeyValuePair(Of String, Cls_Bac) In BaseBacs

            ProducteurBac = kVs.Value.Producteur

            lAffiche = (ProducteurBac = ProducteurImpose) Or lTous

            If lAffiche Then
                MyGrille.Rows.Add()
                iPro += 1
                MyGrille.Rows(iPro - 1).Height = 14


                MyGrille.Rows(iPro - 1).Cells(0).Style.BackColor = BackColors(iColor)
                'MyGrille.Rows(iPro - 1).Cells(1).Style.BackColor = BackColors(iColor)

                Chaine = kVs.Value.Etiquette
                MyGrille(0, iPro - 1).Value = Chaine

                iColor += 1
                If iColor > 1 Then iColor = 0

            End If

        Next kVs

        lBuild = lBuildBack
        'NbProGrille = iPro

    End Sub

    Private Sub RemplirComboProducteur()

        '========================================================================

        '== Dans la version mode Normal pour AM, on n'affiche que les bacs AM
        '== Dans les autres cas, on affiche tous les bacs

        If LogicielInfo.Maitre = EnuMaitre.CTICM Then
            lChoixBacReduit = False
        ElseIf LogicielInfo.Maitre = EnuMaitre.ArcelorMittal Then
            If LogicielOptions.lExpert Then
                lChoixBacReduit = False
            Else
                lChoixBacReduit = (ProducteursBacs.Contains(strAM))
            End If
        End If

        '========================================================================

        Me.cmb_Producteur.Items.Clear()
        If lChoixBacReduit Then
            Me.cmb_Producteur.Items.Add(strAM)
        Else
            Me.cmb_Producteur.Items.Add(strTous)
            For i As Integer = 0 To Me.ProducteursBacs.Count - 1
                Me.cmb_Producteur.Items.Add(ProducteursBacs(i))
            Next
        End If

        Me.cmb_Producteur.SelectedIndex = 0

    End Sub

#End Region

#Region "===FERMETURE==="

    Public Sub TraitementSaisie()

    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Dim lModif As Boolean = False
        If ValideSaisieFenetre() Then

            If iFrmAppel = EnuFenetres.DalleN Then
                TransfertSaisie(Frm_Dalle.MyDalleLoc.Bac, lModif)
            Else
                TransfertSaisie(MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac, lModif)
            End If

            If lModif Then

            End If
            Me.Close()
        End If
    End Sub

    Private Function ValideSaisieFenetre() As Boolean
        Dim lOK As Boolean = True

        If Not MyBac.lDatabase Then

        End If

        Return lOK
    End Function

    Private Sub TransfertSaisie(ByRef BacSave As Cls_Bac, ByRef lModif As Boolean)

        'If BacSave.lDatabase <> MyBac.lDatabase Then lModif = True
        'BacSave.lDatabase = MyBac.lDatabase

        'If BacSave.Etiquette <> MyBac.Etiquette Then lModif = True
        'BacSave.Etiquette = MyBac.Etiquette

        'If BacSave.fyp <> MyBac.fyp Then lModif = True
        'BacSave.fyp = MyBac.fyp

        'If BacSave.h_p <> MyBac.h_p Then lModif = True
        'BacSave.h_p = MyBac.h_p

        'If BacSave.h_rs <> MyBac.h_rs Then lModif = True
        'BacSave.h_rs = MyBac.h_rs

        'If BacSave.b_b <> MyBac.b_b Then lModif = True
        'BacSave.b_b = MyBac.b_b

        'If BacSave.b_t <> MyBac.b_t Then lModif = True
        'BacSave.b_t = MyBac.b_t

        'If BacSave.e_p <> MyBac.e_p Then lModif = True
        'BacSave.e_p = MyBac.e_p

        'If BacSave.tp <> MyBac.tp Then lModif = True
        'BacSave.tp = MyBac.tp

        BacSave.Copie(MyBac, lModif)

    End Sub


#End Region

#Region " Dessins "

    Private Sub img_Bac_Paint(sender As Object, e As PaintEventArgs) Handles img_Bac.Paint

        DessineBac(e.Graphics, Me.img_Bac.ClientRectangle.Width, Me.img_Bac.ClientRectangle.Height, kADJUST, MyBac,
                   MyBac.Hauteur_hpg * 1.75, iSelect, True, True, True)

    End Sub

#End Region

#Region " Evènements "

    Private Sub cmb_Producteur_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Producteur.SelectedIndexChanged
        If lBuild Then Exit Sub
        RemplirGrilleBac(Me.Grid_Bac)
    End Sub

    Private Sub EnterTxtBox(sender As Object, e As EventArgs) Handles txt_tp.Enter, txt_Name.Enter, txt_hpg.Enter, txt_Hp.Enter, txt_ep.Enter, txt_Bt.Enter, txt_Bb.Enter
        If lBuild Then Exit Sub
        Select Case sender.name
            Case Me.txt_Hp.Name : iSelect = 1
            Case Me.txt_hpg.Name : iSelect = 2
            Case Me.txt_ep.Name : iSelect = 3
            Case Me.txt_Bt.Name : iSelect = 4
            Case Me.txt_Bb.Name : iSelect = 5
            Case Me.txt_tp.Name : iSelect = 6
            Case Me.txt_Fyp.Name : iSelect = 7
            Case Me.txt_MuP.Name : iSelect = 8
        End Select
        Me.img_Bac.Invalidate()
    End Sub

    Private Sub LeaveTextBox(sender As Object, e As EventArgs) Handles txt_tp.Leave, txt_hpg.Leave, txt_Hp.Leave, txt_ep.Leave, txt_Bt.Leave, txt_Bb.Leave
        If lBuild Then Exit Sub
        iSelect = 0
        Me.img_Bac.Invalidate()
    End Sub




#End Region

#Region " Evènements saisie "

    Private Sub GestionChangeBac(sender As Object, e As EventArgs) Handles Grid_Bac.SelectionChanged
        If lBuild Then Exit Sub

        Dim indRow As Integer

        indRow = sender.SelectedCells(0).RowIndex

        Dim iBac As Integer = indRow

        Dim Etiquette As String = (Me.Grid_Bac(0, iBac).Value.ToString.Trim)

        TransfertSaisieGridBac(Etiquette, MyBac)

        AfficherParametresBac(MyBac)

        Me.img_Bac.Invalidate()
    End Sub

    Private Sub TransfertSaisieGridBac(Etiquette As String, ByRef MyBac As Cls_Bac)

        MyBac = BaseBacs(Etiquette).Clone

    End Sub

    Private Sub AfficherParametresBac(MyBac As Cls_Bac)

        Dim lBuildBack As Boolean = lBuild
        lBuild = True

        Me.txt_Name.Text = MyBac.Etiquette
        Me.lbl_EtiquetteBac.Text = MyBac.Etiquette

        Me.txt_Hp.Text = GetStringInUnit(MyBac.h_p, Enu_TypeVariable.Dimension, 3, 3, False)
        Me.txt_hpg.Text = GetStringInUnit(MyBac.h_p + MyBac.h_rs, Enu_TypeVariable.Dimension, 3, 3, False)
        Me.txt_ep.Text = GetStringInUnit(MyBac.e_p, Enu_TypeVariable.Dimension, 3, 3, False)
        Me.txt_Bt.Text = GetStringInUnit(MyBac.b_t, Enu_TypeVariable.Dimension, 3, 3, False)
        Me.txt_Bb.Text = GetStringInUnit(MyBac.b_b, Enu_TypeVariable.Dimension, 3, 3, False)
        Me.txt_tp.Text = GetStringInUnit(MyBac.tp, Enu_TypeVariable.Dimension, 3, 3, False)

        Me.txt_Fyp.Text = GetStringInUnit(MyBac.fyp, Enu_TypeVariable.Contrainte, 3, 1, False)
        Me.txt_MuP.Text = GetStringInUnit(MyBac.msurf, Enu_TypeVariable.SansType, 3, 1, False)

        lBuild = lBuildBack
    End Sub

    Private Sub SaisieBacTextNameChanged(sender As Object, e As EventArgs) Handles txt_Name.TextChanged
        If lBuild Then Exit Sub

        MyBac.Etiquette = Me.txt_Name.Text
        Me.lbl_EtiquetteBac.Text = MyBac.Etiquette

        Me.img_Bac.Invalidate()

    End Sub



    Private Sub SaisieBacTextChanged(sender As Object, e As EventArgs) Handles txt_tp.TextChanged, txt_hpg.TextChanged, txt_Hp.TextChanged, txt_ep.TextChanged, txt_Bt.TextChanged, txt_Bb.TextChanged
        If lBuild Then Exit Sub


        Dim Valeur As Decimal

        If VerificationDonnees(sender, Valeur) Then
            Select Case sender.name
                Case Me.txt_Hp.Name
                    MyBac.h_p = Valeur
                Case Me.txt_Bb.Name
                    MyBac.b_b = Valeur
                Case Me.txt_Bt.Name
                    MyBac.b_t = Valeur
                Case Me.txt_ep.Name
                    MyBac.e_p = Valeur
                Case Me.txt_Fyp.Name
                    MyBac.fyp = Valeur
                    'Case Me.txt_Hp.Name
                 '   MyBac. = Valeur
                Case Me.txt_tp.Name
                    MyBac.tp = Valeur
                Case Me.txt_hpg.Name
                    MyBac.h_rs = Valeur - MyBac.h_p
            End Select
        End If

        Me.img_Bac.Invalidate()

        lBuild = False
    End Sub

    Private Function VerificationDonnees(MyTxt As System.Windows.Forms.TextBox, ByRef ValeurUI As Decimal) As Boolean

        Const HPMINI As Decimal = 0.04
        Const HPMAXI As Decimal = 0.125
        Const TPMINI As Decimal = 0.0007
        Const TPMAXI As Decimal = 0.0015
        Const EPMAXI As Decimal = 0.6
        Const EPMINI As Decimal = 0.1
        Const FYMAXI As Decimal = 500
        Const FYMINI As Decimal = 200

        '--> Déclaration
        Dim lOk As Boolean = True
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Select Case MyTxt.Name
            Case Me.txt_Bb.Name, Me.txt_Bt.Name
                ValMin = 0
                ValMax = MyBac.e_p
            Case Me.txt_ep.Name
                ValMax = EPMAXI
                ValMin = EPMINI
            Case Me.txt_tp.Name
                ValMax = TPMAXI
                ValMin = TPMINI
            Case Me.txt_Hp.Name
                ValMin = HPMINI
                ValMax = HPMAXI
            Case Me.txt_hpg.Name
                ValMin = Math.Max(HPMINI, MyBac.h_p)
                ValMax = HPMAXI
            Case Me.txt_Fyp.Name
                ValMin = FYMINI
                ValMax = FYMAXI
                kUnit = LogicielInfo.Transfert_Contraintes(LogicielOptions.IndUnitContraintes)
        End Select

        iErreur = ValideSaisieNombre(MyTxt.Text, True, ValMin / kUnit, lValMax, ValMax / kUnit)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin / kUnit, ValMax / kUnit)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk

    End Function

    Private Sub GestionOption(sender As Object, e As EventArgs) Handles rdb_BacCustom.CheckedChanged, rdb_BacBase.CheckedChanged

        If lBuild Then Exit Sub
        lBuild = True
        Select Case sender.name
            Case Me.rdb_BacBase.Name
                MyBac.lDatabase = True
                Me.rdb_BacCustom.Checked = False

                Dim iBac As Integer = Me.Grid_Bac.SelectedCells(0).RowIndex
                Dim Etiquette As String = (Me.Grid_Bac(0, iBac).Value.ToString.Trim)
                MyBac = BaseBacs(Etiquette).Clone

                AfficherParametresBac(MyBac)

            Case Me.rdb_BacCustom.Name
                MyBac.lDatabase = False
                Me.rdb_BacBase.Checked = False
        End Select

        MAJI_OptionBac()

        Me.img_Bac.Invalidate()
        lBuild = False
    End Sub

    Private Sub MAJI_OptionBac()

        If MyBac.lDatabase Then
            Me.txt_Bb.BackColor = COULEURTXTREADONLY
            Me.txt_Bt.BackColor = COULEURTXTREADONLY
            Me.txt_ep.BackColor = COULEURTXTREADONLY
            Me.txt_Fyp.BackColor = COULEURTXTREADONLY
            Me.txt_Hp.BackColor = COULEURTXTREADONLY
            Me.txt_hpg.BackColor = COULEURTXTREADONLY
            Me.txt_Name.BackColor = COULEURTXTREADONLY
            Me.txt_tp.BackColor = COULEURTXTREADONLY
            Me.txt_MuP.BackColor = COULEURTXTREADONLY
        Else
            Me.txt_Bb.BackColor = COULEURTXTACCESS
            Me.txt_Bt.BackColor = COULEURTXTACCESS
            Me.txt_ep.BackColor = COULEURTXTACCESS
            Me.txt_Fyp.BackColor = COULEURTXTACCESS
            Me.txt_Hp.BackColor = COULEURTXTACCESS
            Me.txt_hpg.BackColor = COULEURTXTACCESS
            Me.txt_Name.BackColor = COULEURTXTACCESS
            Me.txt_tp.BackColor = COULEURTXTACCESS
            Me.txt_MuP.BackColor = COULEURTXTACCESS
        End If

        Me.txt_Bb.ReadOnly = MyBac.lDatabase
        Me.txt_Bt.ReadOnly = MyBac.lDatabase
        Me.txt_ep.ReadOnly = MyBac.lDatabase
        Me.txt_Fyp.ReadOnly = MyBac.lDatabase
        Me.txt_Hp.ReadOnly = MyBac.lDatabase
        Me.txt_hpg.ReadOnly = MyBac.lDatabase
        Me.txt_Name.ReadOnly = MyBac.lDatabase
        Me.txt_tp.ReadOnly = MyBac.lDatabase
        Me.txt_MuP.ReadOnly = MyBac.lDatabase

        Me.cmb_Producteur.Enabled = MyBac.lDatabase
        Me.Grid_Bac.Enabled = MyBac.lDatabase

    End Sub

#End Region


#Region " Dessin symboles "

    Private Sub Symbol_Paint(sender As Object, e As PaintEventArgs) Handles img_tp.Paint, img_hpg.Paint, img_hp.Paint, img_ep.Paint, img_Bt.Paint, img_Bb.Paint, img_Fyp.Paint, img_MuP.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height
        Dim xStart As Single = sWI * 0.95

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lIndice As Boolean
        Dim xPen As Single = xStart
        Dim hCar As Single = e.Graphics.MeasureString("X", FontSymbolNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yPen As Single = (sHI / 2 - hCar) / 2 + sHI * 0.15

        '--> Initialisation

        lIndice = False
        lGrec = False

        Select Case sender.name

            Case Me.img_Bb.Name
                strSymbol = "b"
                strIndice = "b"
            Case Me.img_Bt.Name
                strSymbol = "b"
                strIndice = "t"
            Case Me.img_hp.Name
                strSymbol = "h"
                strIndice = "p"
            Case Me.img_hpg.Name
                strSymbol = "h"
                strIndice = "pg"
            Case Me.img_ep.Name
                strSymbol = "e"
                strIndice = "p"
            Case Me.img_tp.Name
                strSymbol = "t"
                strIndice = "p"
            Case Me.img_Fyp.Name
                strSymbol = "f"
                strIndice = "yp"
            Case Me.img_MuP.Name
                strSymbol = "m"
                strIndice = "p"
                lGrec = True
        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_Alignement.Gauche,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, True)

    End Sub


#End Region

End Class