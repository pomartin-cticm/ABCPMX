Imports PMXMoteur2
Imports System.IO

Public Class Frm_EditBaseBacsAcier

#Region " Variables "

    Dim lBuild As Boolean

    Const FIRSTCOL As Integer = 1

    Dim ColorBCustom As Color = LightGreenAM
    Dim ColorBFixe As Color = PaleGrayAM

    Dim KeyBac As Integer
    Dim CleBacs As New List(Of String)

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_EditBaseBacsAcier_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lBuild = True

        GestionLangues()
        GestionStyle()
        PrepareGrille()
        AfficherBacsDansGrille()
        MAJI_SelectionBac()

        lBuild = False

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_EDITSTEELSHEETINGS")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("CLOSE")
                Me.lbl_Base.Text = "Base"

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If
    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_Base.BackColor = CouleurBackBandeaux
        Me.lbl_Base.ForeColor = CouleurForeBandeaux

        Me.lbl_BacAffiche.BackColor = CouleurBackBandeaux
        Me.lbl_BacAffiche.ForeColor = CouleurForeBandeaux

        Me.img_Bac.Dock = DockStyle.Fill

        Me.TLPan_OrganisationColonnes.ColumnStyles(0).Width = 0

    End Sub

    Private Sub PrepareGrille()

        '--[ Style de la grille

        Me.Grid_Sheets.CellBorderStyle = DataGridViewCellBorderStyle.None
        Me.Grid_Sheets.DefaultCellStyle.Padding = New Padding(0)
        Me.Grid_Sheets.Font = New Font(Me.Grid_Sheets.Font.FontFamily, 8, FontStyle.Regular, GraphicsUnit.Point)
        Me.Grid_Sheets.BorderStyle = BorderStyle.FixedSingle

        Me.Col_Ind.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_Label.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_b1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_b2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_E.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_H.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_hpg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_T.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_M.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_Fy.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        Me.Col_Label.DefaultCellStyle.BackColor = Color.GhostWhite

        '--[ Largeur des colonnes

        Const COLRATIO As Single = 0.09!
        Me.Col_Ind.Width = CInt(Me.Grid_Sheets.Width * 0.035!)
        Me.Col_Label.Width = CInt(Me.Grid_Sheets.Width * 0.225!)
        Me.Col_b1.Width = CInt(COLRATIO * Me.Grid_Sheets.Width)
        Me.Col_b2.Width = CInt(COLRATIO * Me.Grid_Sheets.Width)
        Me.Col_H.Width = CInt(COLRATIO * Me.Grid_Sheets.Width)
        Me.Col_hpg.Width = CInt(COLRATIO * Me.Grid_Sheets.Width)
        Me.Col_E.Width = CInt(COLRATIO * Me.Grid_Sheets.Width)
        Me.Col_T.Width = CInt(COLRATIO * Me.Grid_Sheets.Width)
        Me.Col_M.Width = CInt(COLRATIO * Me.Grid_Sheets.Width)
        Me.Col_Fy.Width = CInt(COLRATIO * Me.Grid_Sheets.Width)

        Me.Col_hpg.Visible = True

    End Sub


    Private Sub AfficherBacsDansGrille()
        '---------------------------------------------------------------------------------
        '   Affiche la liste des bacs acier dans la grille
        '---------------------------------------------------------------------------------

        '--> Déclarations

        Dim iColor As Integer = 0
        Dim ProducteurBac As String
        Dim ProducteurImpose As String = ""
        Dim lAffiche, lTous As Boolean

        '--> Initialisation

        Me.Grid_Sheets.Rows.Clear()
        Me.CleBacs.Clear()
        lTous = True

        '--> Boucle sur les bacs de la base de données

        For Each kVs As KeyValuePair(Of String, Cls_Bac) In BaseBacs

            ProducteurBac = kVs.Value.Producteur

            lAffiche = (ProducteurBac = ProducteurImpose) Or lTous

            If lAffiche Then
                AjouteBacDansGrille(kVs.Value, False)
                Me.CleBacs.Add(kVs.Key)
            End If
        Next

        KeyBac = CInt(Me.Grid_Sheets(Me.Col_Ind.Index, Me.Grid_Sheets.SelectedCells(0).RowIndex).Value) - 1
    End Sub

    Private Sub AjouteBacDansGrille(ByVal MyBac As Cls_Bac, ByVal lNew As Boolean)
        '---------------------------------------------------------------------------------
        '   Ajoute une ligne dans la grille
        '--------------------------------------------------------------------------------------
        '   lNew    [E] :   Indique s'il s'agit d'un nouveau bac définit par l'utilisateur
        '--------------------------------------------------------------------------------------
        Dim iBac As Integer
        Dim i0 As Integer = FIRSTCOL
        Dim iDec As Integer

        Me.Grid_Sheets.Rows.Add()
        iBac = Me.Grid_Sheets.Rows.Count
        Me.Grid_Sheets.Rows(iBac - 1).Height = 20
        'If lNew Then Me.Grid_Sheets.Rows(iBac - 1).DefaultCellStyle.ForeColor = ColorModifie.Couleur

        'If ListeBac(Key).lCustom Then
        '    Me.Grid_Sheets(i0, iBac - 1).Style.BackColor = ColorBCustom
        'Else
        Me.Grid_Sheets(i0, iBac - 1).Style.BackColor = ColorBFixe
        'End If

        Me.Grid_Sheets(i0 + 0, iBac - 1).Value = MyBac.Etiquette
        Me.Grid_Sheets(i0 + 1, iBac - 1).Value = GetStringNoUnit(MyBac.Bt, Enu_TypeVariable.Dimension)
        Me.Grid_Sheets(i0 + 2, iBac - 1).Value = GetStringNoUnit(MyBac.Bb, Enu_TypeVariable.Dimension)
        Me.Grid_Sheets(i0 + 3, iBac - 1).Value = GetStringNoUnit(MyBac.Ep, Enu_TypeVariable.Dimension)
        Me.Grid_Sheets(i0 + 4, iBac - 1).Value = GetStringNoUnit(MyBac.Hp, Enu_TypeVariable.Dimension)
        iDec = i0 + 5
        Me.Grid_Sheets(iDec, iBac - 1).Value = GetStringNoUnit(MyBac.Hauteur_hpg, Enu_TypeVariable.Dimension)
        iDec += 1

        Me.Grid_Sheets(iDec, iBac - 1).Value = GetStringNoUnit(MyBac.Tp, Enu_TypeVariable.Dimension)
        Me.Grid_Sheets(iDec + 1, iBac - 1).Value = Format(MyBac.msurf, "0.00")
        Me.Grid_Sheets(iDec + 2, iBac - 1).Value = GetStringNoUnit(MyBac.fyp, Enu_TypeVariable.Contrainte)

        Me.Grid_Sheets(0, iBac - 1).Value = iBac

    End Sub

#End Region

#Region "   Gestion evenements de la grille "

    Private Sub GestionClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grid_Sheets.Click
        Dim SelectedCellCount As Integer = Me.Grid_Sheets.GetCellCount(DataGridViewElementStates.Selected)

        'If SelectedCellCount = Me.Grid_Sheets.ColumnCount Then
        KeyBac = CInt(Me.Grid_Sheets(Me.Col_Ind.Index, Me.Grid_Sheets.SelectedCells(0).RowIndex).Value) - 1
        'End If
        Me.img_Bac.Invalidate()
        MAJI_SelectionBac()

    End Sub

    Private Sub Grid_Sheets_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Grid_Sheets.SelectionChanged

        If lBuild Then Exit Sub

        KeyBac = CInt(Me.Grid_Sheets(Me.Col_Ind.Index, Me.Grid_Sheets.SelectedCells(0).RowIndex).Value) - 1
        Me.img_Bac.Invalidate()
        MAJI_SelectionBac()

    End Sub

    Private Sub MAJI_SelectionBac()

        Me.lbl_BacAffiche.Text = BaseBacs(CleBacs(KeyBac)).Etiquette

    End Sub


#End Region

#Region "===FERMETURE==="

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub


#End Region

#Region " Dessin du bac "

    Private Sub img_Bac_Paint(sender As Object, e As PaintEventArgs) Handles img_Bac.Paint

        'DessineBac(e.Graphics, Me.img_Bac.ClientRectangle.Width, Me.img_Bac.ClientRectangle.Height, 1, BaseBacs(CleBacs(KeyBac)), 0.1, -1, True, False, False)

        DessineBacTout(e.Graphics, Me.img_Bac.ClientRectangle.Width, Me.img_Bac.ClientRectangle.Height, BaseBacs(CleBacs(KeyBac)), True, 1)

    End Sub

#End Region

End Class