Imports PMXMoteur2
Imports System.IO
Public Class Frm_EditGoujons

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Public ColorModifie As Color = Color.Blue

    Structure TableMod
        Dim Info() As Integer
    End Structure

    Public ListGoujons As List(Of cls_GoujonSoude)

    Dim tabModif As List(Of TableMod)
    Dim tabErreurs As Dictionary(Of String, String)

    Public IndGoujon As Integer    'Communication avec la fenetre AddGoujon
    '                           'Si non nul, on modifie un goujon, sinon on ajoute

    Dim OriginalColor As Color
    Dim nCaseSelect As Integer = 0
    Dim lEdit As Boolean = False

    Dim lModif As Boolean

    Const HTMIN As Single = 0.001
    Const HTMAX As Single = 0.4
    Const DIAMIN As Single = 0.001
    Const DIAMAX As Single = 0.05
    Const STRENGHTMAX As Single = 10000
    Const STRENGHTMIN As Single = 1

    Const FIRSTCOL As Integer = 2

    Dim Bloc As New Dictionary(Of String, String)

    Dim ColorBCustom As Color = LightGreenAM
    Dim ColorBFixe As Color = PaleGrayAM

#End Region

#Region "Chargement de la fenetre"

    Private Sub Frm_EditGoujon_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        InitialisationVariables()
        GestionLangues()
        GestionStyle()
        lBuild = False
    End Sub

    Private Sub InitialisationVariables()
        ListGoujons = New List(Of cls_GoujonSoude)
        tabModif = New List(Of TableMod)
        tabErreurs = New Dictionary(Of String, String)
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            'Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRMEDITSTUDS")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== MENU PRINCIPAL ==============================================================='

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")

                '--> Entetes de la grille

                Me.Col_Label.HeaderText = Bloc("LABEL")

                Me.Col_Ind.HeaderCell.ToolTipText = Bloc("INDICE")
                Me.Col_Ht.HeaderText = "Ht (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
                Me.Col_Ht.HeaderCell.ToolTipText = Bloc("HTOTAL")
                Me.Col_PhiRod.HeaderText = "dr (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
                Me.Col_PhiRod.HeaderCell.ToolTipText = Bloc("PHIROD")
                Me.Col_HeadPhi.HeaderText = "dh (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
                Me.Col_HeadPhi.HeaderCell.ToolTipText = Bloc("PHIHEAD")
                Me.Col_HeadDepth.HeaderText = "Hh (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
                Me.Col_HeadDepth.HeaderCell.ToolTipText = Bloc("DEPTHHEAD")
                Me.Col_YieldStrength.HeaderText = "fy (MPa)" '& tabUnitLongueur(indUnitDimensions) & ")"
                Me.Col_YieldStrength.HeaderCell.ToolTipText = Bloc("FY")
                Me.Col_UltimateStrength.HeaderText = "fu (MPa)" '& tabUnitLongueur(indUnitDimensions) & ")"
                Me.Col_UltimateStrength.HeaderCell.ToolTipText = Bloc("FU")

                '--> ToolTip de la barre de menus

                Me.MenuNouveau.ToolTipText = Bloc("TTMNUNEW")
                Me.MenuModifier.ToolTipText = Bloc("TTMNUMODIFY")
                Me.MenuSupprimer.ToolTipText = Bloc("TTMNUDELETE")
                Me.MenuEnregistrerBase.ToolTipText = Bloc("TTMNUSAVE")

                '--> Recuperation des messages d'erreurs

                Me.tabErreurs.Add("ERRLABEL", Bloc("ERRLABEL"))
                Me.tabErreurs.Add("ERRLABELBASE", Bloc("ERRLABELBASE"))
                Me.tabErreurs.Add("ERRVALEUR", Bloc("ERRVALEUR"))
                Me.tabErreurs.Add("ERRVALNUM", Bloc("ERRVALNUM"))
                Me.tabErreurs.Add("ERRVALHLIM", Bloc("ERRVALHLIM"))
                Me.tabErreurs.Add("ERRFULTFY", Bloc("ERRFULTFY"))
                Me.tabErreurs.Add("ERRDHLTDT", Bloc("ERRDHLTDT"))
                Me.tabErreurs.Add("ERRHHGTHT", Bloc("ERRHHGTHT"))

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
                'Finally
                'Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub GestionStyle()

        Dim iStud As Integer
        'Dim Etiquette, Parametres As String
        'Dim Mots() As String, nMots As Integer
        Dim Row(6) As String

        '--> Icone et Help

        Me.Icon = Frm_PMX.Icon

        ''--> Initialisation

        Me.tabModif.Clear()
        lBuild = True
        'Me.cmd_Supprimer.Enabled = (nCaseSelect > 0)
        Me.Grid_Studs.Rows.Clear()
        Me.btn_Ok.Visible = True

        '--> Recupération des connecteurs dans les bases de données fixe et personnelles

        GetDataBaseStuds(ListGoujons)

        'Dim LinesStuds As New cls_LinesOfFile(FileACB.Studs)

        '--> Style de la grille

        Me.Grid_Studs.CellBorderStyle = DataGridViewCellBorderStyle.None
        Me.Col_Check.Visible = False
        Me.Grid_Studs.DefaultCellStyle.Padding = New Padding(0)
        Me.Grid_Studs.Font = New Font(Me.Grid_Studs.Font.FontFamily, 8, FontStyle.Regular, GraphicsUnit.Point)
        Me.Grid_Studs.BorderStyle = BorderStyle.FixedSingle
        Me.Col_HeadPhi.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_Ht.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_PhiRod.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_UltimateStrength.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_YieldStrength.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        Me.Col_HeadDepth.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        Me.Col_Label.DefaultCellStyle.BackColor = Color.GhostWhite

        '--> On place tous les connecteurs dans le tableau

        For iStud = 0 To ListGoujons.Count - 1

            AddGoujonDansGrille(ListGoujons(iStud))

        Next

        'If LinesStuds.Lines.Count > 0 Then OriginalColor = Me.Grid_Studs(2, 0).Style.BackColor

        lBuild = False
        lModif = False
        Me.MenuEnregistrerBase.Enabled = False

        Me.MAJ_BarreOutils(0)
    End Sub

#End Region

#Region "Gestion Ajout et Suppression de connecteurs dans les tables"

    Private Sub AddGoujonDansGrille(ByVal MyBaseG_loc As cls_GoujonSoude)
        '-----------------------------------------------------------------------------------------------
        '
        '   On rajoute un connecteur dans la grille d'affichage
        '
        '-----------------------------------------------------------------------------------------------

        '--[ Déclarations

        Dim iStud As Integer

        Dim i0 As Integer = FIRSTCOL

        '--[ On rajoute une ligne dans la grille

        Me.Grid_Studs.Rows.Add()

        '--[ Style

        iStud = Me.Grid_Studs.Rows.Count
        Me.Grid_Studs.Rows(iStud - 1).Height = 20

        '--[ AffichageOptFeu

        Me.Grid_Studs(i0 + 0, iStud - 1).Value = MyBaseG_loc.nom
        Me.Grid_Studs(i0 + 1, iStud - 1).Value = GetStringNoUnit(MyBaseG_loc.hsc, Enu_TypeVariable.Dimension) 'hauteur totale
        Me.Grid_Studs(i0 + 2, iStud - 1).Value = GetStringNoUnit(MyBaseG_loc.d, Enu_TypeVariable.Dimension) 'diametre 
        Me.Grid_Studs(i0 + 3, iStud - 1).Value = GetStringNoUnit(MyBaseG_loc.d_tete, Enu_TypeVariable.Dimension) 'diametre tete
        Me.Grid_Studs(i0 + 4, iStud - 1).Value = GetStringNoUnit(MyBaseG_loc.h_tete, Enu_TypeVariable.Dimension) 'hauteur tete
        Me.Grid_Studs(i0 + 5, iStud - 1).Value = GetStringNoUnit(MyBaseG_loc.Fy, Enu_TypeVariable.Contrainte) 'fy
        Me.Grid_Studs(i0 + 6, iStud - 1).Value = GetStringNoUnit(MyBaseG_loc.Fu, Enu_TypeVariable.Contrainte) 'fu
        Me.Grid_Studs(1, iStud - 1).Value = iStud

        If MyBaseG_loc.lCustom Then
            Me.Grid_Studs(i0, iStud - 1).Style.BackColor = ColorBCustom
        Else
            Me.Grid_Studs(i0, iStud - 1).Style.BackColor = ColorBFixe
        End If


    End Sub

    Public Sub AddGoujonDansTables(ByVal Etiquette As String, ByVal HTotal As Single, ByVal PhiTige As Single,
                                   ByVal HTete As Single, ByVal PhiTete As Single,
                                   ByVal fy As Single, ByVal fu As Single, ByVal lAff As Boolean)
        '-----------------------------------------------------------------------------------------------
        '
        '   On rajoute un connecteur dans la liste (cette routine peut être appelée de l'exterieur)
        '
        '-----------------------------------------------------------------------------------------------

        Dim iStud As Integer
        Dim LigneMod As TableMod
        ReDim LigneMod.Info(6)

        Me.tabModif.Add(LigneMod)

        Me.Grid_Studs.Rows.Add()
        iStud = Me.Grid_Studs.Rows.Count
        Me.Grid_Studs.Rows(iStud - 1).Height = 20

        Dim i0 As Integer = FIRSTCOL

        Me.Grid_Studs(i0 + 0, iStud - 1).Value = Etiquette
        Me.Grid_Studs(i0 + 1, iStud - 1).Value = GetStringNoUnit(HTotal, Enu_TypeVariable.Dimension)
        Me.Grid_Studs(i0 + 2, iStud - 1).Value = GetStringNoUnit(PhiTige, Enu_TypeVariable.Dimension)
        Me.Grid_Studs(i0 + 3, iStud - 1).Value = GetStringNoUnit(PhiTete, Enu_TypeVariable.Dimension)
        Me.Grid_Studs(i0 + 4, iStud - 1).Value = GetStringNoUnit(HTete, Enu_TypeVariable.Dimension)
        Me.Grid_Studs(i0 + 5, iStud - 1).Value = GetStringNoUnit(fy, Enu_TypeVariable.Contrainte)
        Me.Grid_Studs(i0 + 6, iStud - 1).Value = GetStringNoUnit(fu, Enu_TypeVariable.Contrainte)
        Me.Grid_Studs(1, iStud - 1).Value = iStud

        If lAff Then
            'On s'arrange pour que la dernière ligne soit visible
            Me.Grid_Studs.FirstDisplayedScrollingRowIndex = iStud - 1
        End If
        If Not lBuild Then
            lModif = True
            Me.MenuEnregistrerBase.Enabled = True
        End If
    End Sub

    Private Sub DeleteGoujon(ByVal iStud As Integer)
        '
        '   Supprime un goujon de la collection
        '
        '-----------------------------------------------------------------------------------

        Me.ListGoujons.Remove(Me.ListGoujons(iStud))

        Me.Grid_Studs.Rows.Remove(Me.Grid_Studs.Rows(iStud))

    End Sub

#End Region

#Region "Gestion des évènements sur les boutons"
    'Private Sub cmd_Valider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Valider.Click

    '    'On parcoure la table des modif pour voir si elle sont valides ou non

    '    Dim sb As New System.Text.StringBuilder
    '    Dim i, j As Integer
    '    Dim iModOK, iModPasOK, iModRow As Integer
    '    Dim IdentStud As String

    '    iModOK = 0
    '    iModPasOK = 0

    '    For i = 0 To Me.tabModif.Count - 1
    '        iModRow = 0
    '        For j = 0 To 6
    '            Select Case Me.tabModif(i).Info(j)
    '                Case 1
    '                    iModOK += 1
    '                Case -1, -2, -3
    '                    iModPasOK += 1
    '                    iModRow += 1
    '            End Select
    '        Next
    '        If iModRow > 0 Then
    '            IdentStud = "Stud n°" & (i + 1).ToString & " : "
    '            If Me.tabModif(i).Info(0) = -1 Then
    '                sb.Append(IdentStud)
    '                sb.Append("Saisir une étiquette")
    '                sb.Append(Environment.NewLine)
    '            ElseIf Me.tabModif(i).Info(0) = -2 Then
    '                sb.Append(IdentStud)
    '                sb.Append("Etiquette non valide")
    '                sb.Append(Environment.NewLine)
    '            End If
    '        End If
    '    Next

    '    'MessageBox.Show("Cellule modifiées : " & (iModOK + iModPasOK).ToString & " Dont OK : " & iModOK.ToString)
    '    MessageBox.Show(sb.ToString)
    '    Me.Close()
    'End Sub

    Private Sub cmd_Annuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Ok.Click
        Me.Close()
    End Sub
    Private Sub Frm_EditGoujons_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        '--> S'il y a eu des modifications dans la base sans sauvegarde, on demande confirmation

        If lModif Then

            Dim Rep As DialogResult = DemandeConfirmationYesNoCancel(Bloc("SAVEMODIF"))
            Select Case Rep
                Case Windows.Forms.DialogResult.No
                Case Windows.Forms.DialogResult.Yes
                    SaveBase()
                Case Windows.Forms.DialogResult.Cancel
                    e.Cancel = True
            End Select

        End If

    End Sub

#End Region

#Region "Gestion de la Grille"

    Private Sub Grid_Studs_CellMouseClick(sender As Object, e As DataGridViewCellEventArgs) Handles Grid_Studs.CellEnter
        Dim indCol As Integer = e.ColumnIndex
        Dim indRow As Integer = e.RowIndex

        If indCol = Me.Col_Check.Index Then

            Dim instance As DataGridViewCheckBoxCell = Me.Grid_Studs(indCol, indRow)

            Dim ColorCell As Color = OriginalColor
            If (Not instance.Value) Then
                ColorCell = Color.Yellow
                nCaseSelect += 1
            Else
                nCaseSelect -= 1
            End If
            For j As Integer = 1 To Me.Grid_Studs.Columns.Count - 1
                Me.Grid_Studs.Rows(indRow).Cells(j).Style.BackColor = ColorCell
            Next

        End If

        If Not indRow = -1 Then MAJ_BarreOutils(indRow)
    End Sub

    Sub MAJ_BarreOutils(ByVal IndiceStud As Integer)
        '--------------------------------------------------------------------------------
        '
        '   Mise à jour de la barre d'outils en fonction du bac sélectionné
        '
        '--------------------------------------------------------------------------------

        Me.MenuModifier.Enabled = ListGoujons(IndiceStud).lCustom
        Me.MenuSupprimer.Enabled = ListGoujons(IndiceStud).lCustom
    End Sub

#End Region

#Region "Fonctions Outils"

    Public Function NouveauLabelValide(ByVal Label As String, ByVal iExcept As Integer) As Boolean
        '
        '   Indique si un label figure déjà dans la liste des goujons
        '
        '-----------------------------------------------------------------------
        '
        '   Label   [E] :   Label de goujon testé
        '   iExcept [E] :   Indice de goujon à ne pas tester
        '
        '-----------------------------------------------------------------------

        If Me.ListGoujons.Count = 0 Then
            Return True
        Else
            Dim lTrouve As Boolean = False
            Dim i As Integer = 0
            Do While i < Me.ListGoujons.Count And Not lTrouve
                lTrouve = (Me.ListGoujons(i).nom.Trim.ToUpper = Label.ToUpper.Trim) And (i <> iExcept)
                i = i + 1
            Loop
            Return Not lTrouve
        End If

    End Function

    Public Function NouveauLabelValideGrille(ByVal Label As String,
    ByVal lExceptMe As Boolean, ByVal iMe As Integer) As Boolean
        '
        '   Indique si un label figure déjà dans la grille des goujons
        '
        '-----------------------------------------------------------------------
        '
        '   lExceptMe   [E] :   Si vrai, l'indice iMe n'est pas testé
        '   iMe         [E] :   Indice d'appel
        '
        '-----------------------------------------------------------------------


        If Me.Grid_Studs.Rows.Count = 0 Then
            Return True
        Else
            Dim lTrouve As Boolean = False
            Dim i As Integer = 0
            Do While i < Me.Grid_Studs.Rows.Count And Not lTrouve
                If (lExceptMe And i <> iMe) Or (Not lExceptMe) Then
                    lTrouve = (Me.Grid_Studs(Me.Col_Label.Index, i).Value.Trim.ToUpper = Label.ToUpper.Trim)
                End If
                i = i + 1
            Loop
            Return Not lTrouve
        End If

    End Function

#End Region

#Region "Gestion de la barre d'outils"

    Private Sub MenuNouveau_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuNouveau.Click
        IndGoujon = -1
        Frm_AddGoujon.ShowDialog()
        Frm_AddGoujon.Dispose()
    End Sub

    Private Sub MenuModifier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuModifier.Click, Grid_Studs.DoubleClick
        Dim SelectedCellCount As Integer = Me.Grid_Studs.GetCellCount(DataGridViewElementStates.Selected)

        If SelectedCellCount = Me.Grid_Studs.ColumnCount Then

            IndGoujon = CInt(Me.Grid_Studs(Me.Col_Ind.Index, Me.Grid_Studs.SelectedCells(0).RowIndex).Value)

            If ListGoujons(IndGoujon - 1).lCustom Then

            End If

            Frm_AddGoujon.ShowDialog()
            Frm_AddGoujon.Dispose()


            If ListGoujons(IndGoujon - 1).lGoujonModifie Then
                'Si on a modifié le goujon, il faut mettre à jour le tableau

                'Dim ColorCell As Color
                Dim Chaine As String
                If ListGoujons(IndGoujon - 1).nom <> Me.Grid_Studs(FIRSTCOL + 0, IndGoujon - 1).Value.ToString Then
                    Me.Grid_Studs(FIRSTCOL + 0, IndGoujon - 1).Style.ForeColor = ColorModifie
                    Me.Grid_Studs(FIRSTCOL + 0, IndGoujon - 1).Value = ListGoujons(IndGoujon - 1).nom
                    lModif = True
                End If
                Chaine = GetStringNoUnit(ListGoujons(IndGoujon - 1).hsc, Enu_TypeVariable.Dimension).Trim
                If Chaine <> Me.Grid_Studs(FIRSTCOL + 1, IndGoujon - 1).Value.ToString.Trim Then
                    Me.Grid_Studs(FIRSTCOL + 1, IndGoujon - 1).Style.ForeColor = ColorModifie
                    Me.Grid_Studs(FIRSTCOL + 1, IndGoujon - 1).Value = Chaine
                    lModif = True
                End If
                Chaine = GetStringNoUnit(ListGoujons(IndGoujon - 1).d, Enu_TypeVariable.Dimension).Trim
                If Chaine <> Me.Grid_Studs(FIRSTCOL + 2, IndGoujon - 1).Value.ToString.Trim Then
                    Me.Grid_Studs(FIRSTCOL + 2, IndGoujon - 1).Style.ForeColor = ColorModifie
                    Me.Grid_Studs(FIRSTCOL + 2, IndGoujon - 1).Value = Chaine
                    lModif = True
                End If
                Chaine = GetStringNoUnit(ListGoujons(IndGoujon - 1).d_tete, Enu_TypeVariable.Dimension).Trim
                If Chaine <> Me.Grid_Studs(FIRSTCOL + 3, IndGoujon - 1).Value.ToString.Trim Then
                    Me.Grid_Studs(FIRSTCOL + 3, IndGoujon - 1).Style.ForeColor = ColorModifie
                    Me.Grid_Studs(FIRSTCOL + 3, IndGoujon - 1).Value = Chaine
                    lModif = True
                End If
                Chaine = GetStringNoUnit(ListGoujons(IndGoujon - 1).h_tete, Enu_TypeVariable.Dimension).Trim
                If Chaine <> Me.Grid_Studs(FIRSTCOL + 4, IndGoujon - 1).Value.ToString.Trim Then
                    Me.Grid_Studs(FIRSTCOL + 4, IndGoujon - 1).Style.ForeColor = ColorModifie
                    Me.Grid_Studs(FIRSTCOL + 4, IndGoujon - 1).Value = Chaine
                    lModif = True
                End If
                Chaine = GetStringNoUnit(ListGoujons(IndGoujon - 1).Fy, Enu_TypeVariable.Contrainte).Trim
                If Chaine <> Me.Grid_Studs(FIRSTCOL + 5, IndGoujon - 1).Value.ToString.Trim Then
                    Me.Grid_Studs(FIRSTCOL + 5, IndGoujon - 1).Style.ForeColor = ColorModifie
                    Me.Grid_Studs(FIRSTCOL + 5, IndGoujon - 1).Value = Chaine
                    lModif = True
                End If
                Chaine = GetStringNoUnit(ListGoujons(IndGoujon - 1).Fu, Enu_TypeVariable.Contrainte).Trim
                If Chaine <> Me.Grid_Studs(FIRSTCOL + 6, IndGoujon - 1).Value.ToString.Trim Then
                    Me.Grid_Studs(FIRSTCOL + 6, IndGoujon - 1).Style.ForeColor = ColorModifie
                    Me.Grid_Studs(FIRSTCOL + 6, IndGoujon - 1).Value = Chaine
                    lModif = True
                End If

                ListGoujons(IndGoujon - 1).lGoujonModifie = False 'on vient d'afficher les modifications du goujons, cette variable est réinitialisée à False 

            End If
            If lModif Then Me.MenuEnregistrerBase.Enabled = True

        End If
    End Sub

    Private Sub MenuSupprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuSupprimer.Click

        Dim SelectedCellCount As Integer = Me.Grid_Studs.GetCellCount(DataGridViewElementStates.Selected)

        If SelectedCellCount > 0 Then

            If Me.Grid_Studs.AreAllCellsSelected(True) Then

                If DemandeConfirmation(Bloc("DELETEALL") & " ?") Then

                    Me.Grid_Studs.Rows.Clear()
                    Me.ListGoujons.Clear()
                    lModif = True
                    Me.MenuEnregistrerBase.Enabled = False

                End If

            Else

                Dim iStud As Integer
                Dim sB As New System.Text.StringBuilder()
                Dim sB2 As New System.Text.StringBuilder()

                Dim tabStud As New List(Of Integer)
                Dim tabRow As New List(Of Integer)

                sB.Append(Bloc("CONFIRM") & " :")
                Dim i As Integer
                For i = 0 To SelectedCellCount - 1

                    'sB.Append("Row: ")
                    'sB.Append(Me.Grid_Studs.SelectedCells(i).RowIndex _
                    '    .ToString())
                    'sB.Append(", Column: ")
                    'sB.Append(Me.Grid_Studs.SelectedCells(i).ColumnIndex _
                    '    .ToString())
                    'sB.Append(Environment.NewLine)

                    iStud = CInt(Me.Grid_Studs(Me.Col_Ind.Index, Me.Grid_Studs.SelectedCells(i).RowIndex).Value)

                    If Not tabStud.Contains(iStud) Then
                        tabStud.Add(iStud)
                        tabRow.Add(Me.Grid_Studs.SelectedCells(i).RowIndex)
                        sB.Append(Environment.NewLine)
                        sB.Append("     " & Me.ListGoujons(iStud - 1).nom)
                    End If

                    'sB.Append("Goujon ")
                    'sB.Append(iStud)
                    'sB.Append(" - ")
                    'sB.Append(Me.tabGoujons(iStud - 1).Etiquette & " - ")
                    'sB.Append(Me.Grid_Studs.SelectedCells(i).RowIndex)
                    'sB.Append(Environment.NewLine)
                Next i

                'sb.Append("Total: " + selectedCellCount.ToString())
                'MessageBox.Show(sB.ToString(), "Non triées")

                If DemandeConfirmation(sB.ToString) Then
                    tabStud.Sort()
                    tabRow.Sort()

                    For i = tabRow.Count - 1 To 0 Step -1
                        Me.Grid_Studs.Rows.Remove(Me.Grid_Studs.Rows(tabRow(i)))
                    Next

                    For i = tabStud.Count - 1 To 0 Step -1
                        Me.ListGoujons.Remove(Me.ListGoujons(tabStud(i) - 1))
                    Next

                    lModif = True
                    Me.MenuEnregistrerBase.Enabled = True

                    Me.Grid_Studs.Refresh()

                    'Dim Etiquette As String
                    Dim j As Integer, lTrouve As Boolean

                    For i = 0 To Me.ListGoujons.Count - 1
                        j = 0
                        lTrouve = False
                        Do While j < Me.Grid_Studs.Rows.Count And Not lTrouve
                            If Me.Grid_Studs(Me.Col_Label.Index, j).Value.ToString = Me.ListGoujons(i).nom Then
                                lTrouve = True
                                Me.Grid_Studs(Me.Col_Ind.Index, j).Value = i + 1
                            End If
                            j = j + 1
                        Loop
                    Next

                End If

            End If

        End If

    End Sub

    Private Sub MenuEnregistrerBase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuEnregistrerBase.Click

        SaveBase()

    End Sub

#End Region

#Region "Gestion de la modification des cellules"

    Private Sub GestionModif(ByVal iRow As Integer, ByVal iCol As Integer, ByVal iStud As Integer,
                             ByVal iVar As Integer, ByVal valRef As Single,
                             ByVal ValMin As Single, ByVal ValMax As Single,
                             ByRef iValid As Single, ByRef Valeur As Single)

        '
        '   Gestion de la modif d'une cellule
        '
        '---------------------------------------------------------------------------------
        '
        '   iValid          [S] :   0 - OK sans modif 1 - OK avec modif
        '                           -1 - pas de saisie
        '                           -2 - pas de valeur numerique
        '                           -3 - hors des bornes
        '   ValMin,ValMax   [E] :   Bornes de la saisie
        '
        '---------------------------------------------------------------------------------

        If Me.Grid_Studs(iCol, iRow).Value.ToString.Trim = "" Then
            iValid = -1
        Else
            Dim Chaine As String = TraiteReal(Me.Grid_Studs(iCol, iRow).Value.ToString)
            If Not IsNumeric(Chaine) Then
                iValid = -2
            Else
                Valeur = CSng(Chaine)
                If Valeur >= ValMin And Valeur <= ValMax Then
                    If Valeur = valRef Then
                        iValid = 0
                    Else
                        iValid = 1
                    End If
                Else
                    iValid = -3
                End If
            End If
        End If
        Me.tabModif(iStud - 1).Info(iVar) = iValid
    End Sub


#End Region

#Region "Recuperation et Enregistrement de la base"

    'Private Sub Transfert()
    '    '
    '    '   Transfert des éléments modifiés de la grille dans la table des goujons
    '    '
    '    Dim iStud As Integer
    '    For i As Integer = 0 To Me.Grid_Studs.Rows.Count - 1
    '        iStud = Me.Grid_Studs(Me.Col_Ind.Index, i).Value
    '        If tabModif(iStud - 1).Info(0) = 1 Then
    '            'Recuperation d'une nouvelle étiquette
    '            Me.ListGoujons(iStud - 1).Etiquette = Me.Grid_Studs(Me.Col_Label.Index, i).Value.ToString
    '        End If

    '        If tabModif(iStud - 1).Info(1) = 1 Then
    '            'Recuperation d'une nouvelle hauteur
    '            Me.ListGoujons(iStud - 1).Htot = CSng(TraiteReal(Me.Grid_Studs(Me.Col_Ht.Index, i).Value.ToString))
    '        End If

    '        If tabModif(iStud - 1).Info(2) = 1 Then
    '            'Recuperation d'un nouveau diametre de tige
    '            Me.ListGoujons(iStud - 1).PhiTige = CSng(TraiteReal(Me.Grid_Studs(Me.Col_PhiRod.Index, i).Value.ToString))
    '        End If

    '        If tabModif(iStud - 1).Info(3) = 1 Then
    '            'Recuperation d'une nouvelle hauteur de tete
    '            Me.ListGoujons(iStud - 1).HTete = CSng(TraiteReal(Me.Grid_Studs(Me.Col_HeadDepth.Index, i).Value.ToString))
    '        End If

    '        If tabModif(iStud - 1).Info(4) = 1 Then
    '            'Recuperation d'une nouvelle diametre de tete
    '            Me.ListGoujons(iStud - 1).PhiTete = CSng(TraiteReal(Me.Grid_Studs(Me.Col_HeadPhi.Index, i).Value.ToString))
    '        End If

    '        If tabModif(iStud - 1).Info(5) = 1 Then
    '            'Recuperation d'une limite d'elasticité
    '            Me.ListGoujons(iStud - 1).fy = CSng(TraiteReal(Me.Grid_Studs(Me.Col_YieldStrength.Index, i).Value.ToString))
    '        End If

    '        If tabModif(iStud - 1).Info(6) = 1 Then
    '            'Recuperation d'une nouvelle limite ultime
    '            Me.ListGoujons(iStud - 1).fu = CSng(TraiteReal(Me.Grid_Studs(Me.Col_UltimateStrength.Index, i).Value.ToString))
    '        End If

    '    Next

    'End Sub

    Private Sub StockeBase(ByVal NomFichier As String, ByRef nbPerso As Integer)
        '------------------------------------------------------------------
        '
        '   Stockage de la base de données des goujons personnalisées
        '
        '------------------------------------------------------------------

        '--[ Déclarations

        Dim Lines As New List(Of String)
        Dim fReel As String = "0.0000"
        Dim kUnit As Single = 1000
        Dim Chaine As String
        Dim tabTabul() As Integer = {30, 42, 54, 66, 78, 90, 102}

        '--[ Initialisations

        nbPerso = 0
        Lines.Clear()

        '--[ Entete du fichier

        Chaine = "Label"
        PositionneDansChaine(Chaine, tabTabul(0), "Ht (mm)")
        PositionneDansChaine(Chaine, tabTabul(1), "Ø rod (mm)")
        PositionneDansChaine(Chaine, tabTabul(2), "Ø head (mm)")
        PositionneDansChaine(Chaine, tabTabul(3), "Head h (mm)")
        PositionneDansChaine(Chaine, tabTabul(4), "fy (MPa)")
        PositionneDansChaine(Chaine, tabTabul(5), "fu (MPa)")

        Lines.Add(Chaine)

        '--[ Ecriture des goujons personnalisés

        For i As Integer = 0 To Me.ListGoujons.Count - 1
            If ListGoujons(i).lCustom Then
                nbPerso += 1

                Chaine = Me.ListGoujons(i).nom & ","
                PositionneDansChaine(Chaine, tabTabul(0), FrmReel(Me.ListGoujons(i).hsc * kUnit, 3))
                PositionneDansChaine(Chaine, tabTabul(1), FrmReel(Me.ListGoujons(i).d * kUnit, 3))
                PositionneDansChaine(Chaine, tabTabul(2), FrmReel(Me.ListGoujons(i).d_tete * kUnit, 3))    '==Correction V1.01
                PositionneDansChaine(Chaine, tabTabul(3), FrmReel(Me.ListGoujons(i).h_tete * kUnit, 3))      '==Correction V1.01
                PositionneDansChaine(Chaine, tabTabul(4), FrmReel(Me.ListGoujons(i).Fy, 3))
                PositionneDansChaine(Chaine, tabTabul(5), FrmReel(Me.ListGoujons(i).Fu, 3))
                Lines.Add(Chaine)
            End If
        Next

        '--[ Sauvegarde du fichier

        If nbPerso > 0 Then
           Cls_LinesOfFile.EcrireFile(NomFichier, Lines)
        Else
            If My.Computer.FileSystem.FileExists(NomFichier) Then
                My.Computer.FileSystem.DeleteFile(NomFichier)
            End If
        End If

    End Sub

    Private Sub SaveBase()

        'Transfert()
        Dim nbPerso As Integer

        lModif = False
        Me.MenuEnregistrerBase.Enabled = lModif
        '--[ Dupplication du fichier précédent

        If My.Computer.FileSystem.FileExists(LogicielFichiers.Base_Goujons_Perso) Then
            Dim RepStuds, Racine, PreviousFile As String
            RepStuds = Repertoire(LogicielFichiers.Base_Goujons_Perso)
            Racine = LogicielFichiers.Base_Goujons_Perso.Substring(RepStuds.Length)
            PreviousFile = RepStuds & "Previous_" & Racine

            If My.Computer.FileSystem.FileExists(PreviousFile) Then
                My.Computer.FileSystem.DeleteFile(PreviousFile)
            End If
            My.Computer.FileSystem.CopyFile(LogicielFichiers.Base_Goujons_Perso, PreviousFile)
        End If

        '--[ Sauvegarde du nouveau fichier
        StockeBase(LogicielFichiers.Base_Goujons_Perso, nbPerso)

        '--[ MAJ de l'ancienne BDD
        BaseGoujons = ListGoujons

        '--[ Information utilisateur
        If nbPerso > 0 Then
            UserInformation(RemplaceDollar(Bloc("SAVEBASE"), Format(nbPerso, "0")))
        Else
        End If

    End Sub

#End Region

End Class