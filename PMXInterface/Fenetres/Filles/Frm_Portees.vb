Imports PMXMoteur2
Imports System.IO

Public Class Frm_Portees

#Region " Variables locales "

    Dim lBuild As Boolean = True
    Dim iSelect As Integer = -1
    '----------------------------------------------
    '   1 pour la travée principale
    '   -1 si rien de selectionné
    '   0 console gauche
    '   99 console droite
    '----------------------------------------------

    Const iFRMPORTEES As Integer = 1

    Dim MyPoutreLoc As New cls_Poutre(NomChargements)

    Const formatLONGUEUR As String = "0.00"

    Dim FontFrm As Font

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Portees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        InitialiserVariables()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub InitialiserVariables()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_SPANLENGHTS")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== MENU PRINCIPAL ==============================================================='

                Me.lbl_Portees.Text = Bloc("SPANS")

                Me.lbl_MainSpan.Text = Bloc("MAINSPAN")
                Me.chk_ConsoleGauche.Text = Bloc("LEFTCANT")
                Me.chk_ConsoleDroite.Text = Bloc("RIGHTCANT")

                Me.lbl_Entraxe.Text = Bloc("SPACINGS")
                Me.lbl_Entraxes.Text = Bloc("SPACINGS")
                Me.rad_Intermediaire.Text = Bloc("INTERMEDIATEBEAM")
                Me.rad_Rive.Text = Bloc("EDGEBEAM")
                Me.lbl_Tremies.Text = Bloc("OPENINGS")
                Me.chk_TremieGauche.Text = Bloc("LEFTOPENING")
                Me.chk_TremieDroite.Text = Bloc("RIGHTOPENING")

                Me.chk_ContinuiteDalleAppGauche.Text = Bloc("LEFTSLABCONTINUITY")
                Me.chk_ContinuiteDalleAppDroit.Text = Bloc("RIGHTSLABCONTINUITY")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If
    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        FontFrm = New Font(FontBase.Name, SizeFontFrm)

        Me.lbl_Portees.BackColor = CouleurBackBandeaux
        Me.lbl_Portees.ForeColor = CouleurForeBandeaux
        Me.lbl_Entraxe.BackColor = CouleurBackBandeaux
        Me.lbl_Entraxe.ForeColor = CouleurForeBandeaux
        Me.lbl_Tremies.BackColor = CouleurBackBandeaux
        Me.lbl_Tremies.ForeColor = CouleurForeBandeaux

        Me.TLPan_Portees.ColumnStyles(0).Width = LargeurColonneSaisie

        'Me.TLPan_Gauche.RowStyles(2).Height = 0

        'Dim Hcum As Integer = 0
        'For i As Integer = 0 To 1
        '    Hcum += Me.TLPan_Gauche.RowStyles(i).Height
        'Next
        'Me.TLPan_Portees.Height = Hcum

        Me.img_Portees.Dock = DockStyle.Fill
        Me.img_Portees.BorderStyle = BorderStyle.FixedSingle
        Me.img_Coupe.Dock = DockStyle.Fill
        Me.img_Coupe.BorderStyle = BorderStyle.FixedSingle

        'On affiche la possibilités de définir des trémies uniquement pour les poutres mixtes

        If Not MyPoutreLoc.lMixte Then

            Me.TLPan_Tremies.ColumnStyles(0).Width = 0
            Me.lbl_Tremies.Visible = False
            Me.pan_Tremies.Visible = False

        End If

        chk_ConsoleGauche.Visible = Not MyPoutreLoc.lSlimFloor
        chk_ContinuiteDalleAppGauche.Visible = Not MyPoutreLoc.lSlimFloor
        chk_ConsoleDroite.Visible = Not MyPoutreLoc.lSlimFloor
        chk_ContinuiteDalleAppDroit.Visible = Not MyPoutreLoc.lSlimFloor

    End Sub

    Private Sub AfficherPoutreEnCours()

        With MyProjet.Poutres(MyProjet.IndEnCours)

            Me.txt_MainSpan.Text = GetStringInUnit(.LongueurTravee(1), Enu_TypeVariable.Longueur, 4, 2, False)

            Me.chk_ConsoleGauche.Checked = .lTraveeConsoleGauche
            Me.txt_PorteeConsoleG.Text = GetStringInUnit(.LongueurTravee(0), Enu_TypeVariable.Longueur, 4, 2, False)

            Me.chk_ConsoleDroite.Checked = .lTraveeConsoleDroite
            Me.txt_PorteeConsoleD.Text = GetStringInUnit(.LongueurTravee(.IndiceTraveeConsoleDroite), Enu_TypeVariable.Longueur, 4, 2, False)

            Me.txt_D1.Text = GetStringInUnit(.EntraxeD1, Enu_TypeVariable.Longueur, 4, 2, False)
            Me.txt_D2.Text = GetStringInUnit(.EntraxeD2, Enu_TypeVariable.Longueur, 4, 2, False)

            Me.rad_Intermediaire.Checked = MyPoutreLoc.lIntermediaire
            Me.rad_Rive.Checked = Not MyPoutreLoc.lIntermediaire

            If Not MyPoutreLoc.lIntermediaire And MyPoutreLoc.Section.lSlimFloor Then
                Me.txt_D1.Enabled = False
            Else
                Me.txt_D1.Enabled = True
            End If

            Me.chk_TremieGauche.Enabled = .lIntermediaire
            Me.chk_TremieGauche.Checked = .lTremieGauche And .lIntermediaire
            'If .lTremieGauche Then
            Me.txt_TremieGauche.Text = GetStringInUnit(.DistanceDsl1, Enu_TypeVariable.Longueur, 4, 2, False)

            Me.chk_TremieDroite.Checked = .lTremieDroite
            'If .lTremieDroite Then
            Me.txt_TremieDroite.Text = GetStringInUnit(.DistanceDsl2, Enu_TypeVariable.Longueur, 4, 2, False)

            Me.chk_ContinuiteDalleAppGauche.Checked = .lDalleContinueGauche
            Me.chk_ContinuiteDalleAppDroit.Checked = .lDalleContinueDroite

        End With

        MAJI_PorteesConsoles()
        MAJI_Tremies()

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitL1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL6.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL7.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

    End Sub

#End Region

#Region "===FERMETURE==="
    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        If ValideSaisieFenetre() Then

            Dim lModif As Boolean = False

            TransfertSaisie(lModif)

            If lModif Then
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If

            'MyProjet.Poutres(MyProjet.IndEnCours).EstValidee(iFRMETAIEMENT)

            Me.Close()
        End If
    End Sub

    Private Function ValideSaisieFenetre() As Boolean
        Dim lFrm_Valide As Boolean = True

        Dim list_txtbox As New List(Of TextBox)
        list_txtbox.Add(txt_MainSpan)
        If chk_ConsoleGauche.Checked Then list_txtbox.Add(txt_PorteeConsoleG)
        If chk_ConsoleDroite.Checked Then list_txtbox.Add(txt_PorteeConsoleD)
        list_txtbox.Add(txt_D1)
        list_txtbox.Add(txt_D2)
        If chk_TremieGauche.Checked Then list_txtbox.Add(txt_TremieGauche)
        If chk_TremieDroite.Checked Then list_txtbox.Add(txt_TremieDroite)

        Dim ValeurUI As Decimal

        For Each txtbox_loc As TextBox In list_txtbox
            VerificationSaisie(txtbox_loc, ValeurUI)
            If Not ErrorProvider.GetError(txtbox_loc) = String.Empty Then
                lFrm_Valide = False
                Exit For
            End If
        Next

        Return lFrm_Valide
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False

        With MyProjet.Poutres(MyProjet.IndEnCours)

            For i_travee As Integer = 0 To MyPoutreLoc.IndiceTraveeConsoleDroite
                If .LongueurTravee(i_travee) <> MyPoutreLoc.LongueurTravee(i_travee) Then
                    lModif = True
                    .LongueurTravee(i_travee) = MyPoutreLoc.LongueurTravee(i_travee)
                    Reinitialiser_Connection(i_travee)
                End If
            Next

            GereTransfertValeur(MyPoutreLoc.lDalleContinueGauche, .lDalleContinueGauche, lModif)
            GereTransfertValeur(MyPoutreLoc.lDalleContinueDroite, .lDalleContinueDroite, lModif)

            GereTransfertValeur(MyPoutreLoc.lTraveeConsoleGauche, .lTraveeConsoleGauche, lModif)
            GereTransfertValeur(MyPoutreLoc.lTraveeConsoleDroite, .lTraveeConsoleDroite, lModif)

            GereTransfertValeur(MyPoutreLoc.lIntermediaire, .lIntermediaire, lModif)

            GereTransfertValeur(MyPoutreLoc.EntraxeD1, .EntraxeD1, lModif)
            GereTransfertValeur(MyPoutreLoc.EntraxeD2, .EntraxeD2, lModif)

            GereTransfertValeur(MyPoutreLoc.lTremieGauche, .lTremieGauche, lModif)
            If .lTremieGauche Then _
                GereTransfertValeur(MyPoutreLoc.DistanceDsl1, .DistanceDsl1, lModif)

            GereTransfertValeur(MyPoutreLoc.lTremieDroite, .lTremieDroite, lModif)
            If .lTremieDroite Then _
                GereTransfertValeur(MyPoutreLoc.DistanceDsl2, .DistanceDsl2, lModif)

        End With
    End Sub

    ''' <summary>
    ''' Lorsqu'une travée voit sa longueur modifiée, il faut réinitialiser les paramètres de celle-ci vis-à-vis de la connection
    ''' </summary>
    ''' <param name="indTravee"></param>
    Private Sub Reinitialiser_Connection(ByVal indTravee As Integer)
        With MyProjet.Poutres(MyProjet.IndEnCours)
            .LongueurZone(indTravee, 0) = .LongueurTravee(indTravee)
            .LongueurZone(indTravee, 1) = 0
            .LongueurZone(indTravee, 2) = 0
            .NombreZones(indTravee) = 1
            .EspacementZone(indTravee, 0) = 200 / 1000
            .EspacementZone(indTravee, 1) = 200 / 1000
            .EspacementZone(indTravee, 2) = 200 / 1000
            .Espacement_Bac_TransZone(indTravee, 0) = 1
            .Espacement_Bac_TransZone(indTravee, 1) = 1
            .Espacement_Bac_TransZone(indTravee, 2) = 1
            .NombreGoujonsTransv(indTravee, 0) = 1
            .NombreGoujonsTransv(indTravee, 1) = 1
            .NombreGoujonsTransv(indTravee, 2) = 1
            'For j As Integer = 0 To 2
            '    .NombreGoujonsTot(indTravee) += .ZoneLongueur(indTravee, j) / .ZoneEspacement(indTravee, j)
            'Next
        End With
    End Sub
#End Region

#Region " Dessins "

    Private Sub img_Coupe_Paint(sender As Object, e As PaintEventArgs) Handles img_Coupe.Paint

        DessinFrmCoupe(e.Graphics, MyPoutreLoc, FontFrm, Me.img_Coupe.ClientRectangle.Width, Me.img_Coupe.ClientRectangle.Height, 1, iSelect, True)

    End Sub


    Private Sub DessinPoutre(sender As Object, e As PaintEventArgs) Handles img_Portees.Paint

        DessinFrmPortee(e.Graphics, MyPoutreLoc, fontfrm, Me.img_Portees.ClientRectangle.Width, Me.img_Portees.ClientRectangle.Height, 1, iSelect, True)

    End Sub

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_L3.Paint, img_L2.Paint, img_L1.Paint, img_D1.Paint, img_D2.Paint, img_TremieGauche.Paint, img_TremieDroite.Paint

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

        '--> Initialisation

        lIndice = True
        lGrec = False
        lEgal = True

        Select Case sender.name
            Case Me.img_L1.Name

                strSymbol = "L"
                strIndice = ""
                lIndice = False

            Case Me.img_L2.Name

                strSymbol = "L"
                strIndice = "g"

            Case Me.img_L3.Name

                strSymbol = "L"
                strIndice = "d"

            Case img_D1.Name
                strSymbol = "d"
                strIndice = "1"

            Case img_D2.Name
                strSymbol = "d"
                strIndice = "2"

            Case Me.img_TremieGauche.Name
                strSymbol = "d"
                strIndice = "sl,1"

            Case Me.img_TremieDroite.Name
                strSymbol = "d"
                strIndice = "sl,2"

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub



#End Region

#Region " Evènements "

    Private Sub img_Portees_Resize(sender As Object, e As EventArgs) Handles img_Portees.Resize
        Me.img_Portees.Invalidate()
    End Sub

    Private Sub img_Coupe_Resize(sender As Object, e As EventArgs) Handles img_Coupe.Resize
        Me.img_Coupe.Invalidate()
    End Sub

    Private Sub EnterTextBox(sender As Object, e As EventArgs) Handles txt_TremieGauche.Enter, txt_PorteeConsoleG.Enter, txt_PorteeConsoleD.Enter, txt_MainSpan.Enter, txt_D2.Enter, txt_D1.Enter, txt_TremieDroite.Enter, txt_TremieGauche.Enter
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.txt_MainSpan.Name : iSelect = 1
            Case Me.txt_PorteeConsoleG.Name : iSelect = 0
            Case Me.txt_PorteeConsoleD.Name : iSelect = 99
            Case Me.txt_D1.Name : iSelect = 101
            Case Me.txt_D2.Name : iSelect = 102
            Case Me.txt_TremieGauche.Name : iSelect = 103
            Case Me.txt_TremieDroite.Name : iSelect = 104

        End Select

        Me.img_Coupe.Invalidate()
        Me.img_Portees.Invalidate()
    End Sub

    Private Sub LeaveTextBox(sender As Object, e As EventArgs) Handles txt_TremieGauche.Leave, txt_PorteeConsoleG.Leave, txt_PorteeConsoleD.Leave, txt_MainSpan.Leave, txt_D2.Leave, txt_D1.Leave, txt_TremieDroite.Leave, txt_TremieGauche.Leave

        If lBuild Then Exit Sub
        iSelect = -1

        Me.img_Coupe.Invalidate()
        Me.img_Portees.Invalidate()

    End Sub

    Private Sub MAJ_txt_Tremie(sender As Object, e As EventArgs) Handles txt_TremieGauche.VisibleChanged, txt_TremieDroite.VisibleChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case txt_TremieGauche.Name
                If txt_TremieGauche.Visible Then
                    Dim ValMin As Decimal = 0
                    Dim ValMax As Decimal = MyPoutreLoc.EntraxeD1 / 2

                    txt_TremieGauche.Text = GetStringInUnit(Math.Min(Math.Max(MyPoutreLoc.DistanceDsl1, ValMin), ValMax), Enu_TypeVariable.Longueur, 4, 2, False)

                End If

            Case txt_TremieDroite.Name
                If txt_TremieDroite.Visible Then
                    Dim ValMin As Decimal = 0
                    Dim ValMax As Decimal = MyPoutreLoc.EntraxeD2 / 2

                    txt_TremieDroite.Text = GetStringInUnit(Math.Min(Math.Max(MyPoutreLoc.DistanceDsl2, ValMin), ValMax), Enu_TypeVariable.Longueur, 4, 2, False)

                End If

        End Select

    End Sub



#End Region

#Region " Evènements saisie "

    Private Sub SaisieText(sender As Object, e As EventArgs) Handles txt_PorteeConsoleG.TextChanged, txt_PorteeConsoleD.TextChanged, txt_MainSpan.TextChanged,
                                                                     txt_TremieDroite.TextChanged, txt_TremieGauche.TextChanged,
                                                                     txt_D1.TextChanged, txt_D2.TextChanged

        If lBuild Then Exit Sub
        Dim lPortees As Boolean = False
        Dim lCoupe As Boolean = False

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Select Case sender.name
                Case Me.txt_MainSpan.Name
                    MyPoutreLoc.LongueurTravee(1) = ValeurUI
                    lPortees = True
                Case Me.txt_PorteeConsoleG.Name
                    MyPoutreLoc.LongueurTravee(0) = ValeurUI
                    lPortees = True
                Case Me.txt_PorteeConsoleD.Name
                    MyPoutreLoc.LongueurTravee(MyPoutreLoc.IndiceTraveeConsoleDroite) = ValeurUI
                    lPortees = True
                Case Me.txt_D1.Name
                    MyPoutreLoc.EntraxeD1 = ValeurUI
                    lCoupe = True
                Case Me.txt_D2.Name
                    MyPoutreLoc.EntraxeD2 = ValeurUI
                    lCoupe = True
                Case Me.txt_TremieGauche.Name
                    MyPoutreLoc.DistanceDsl1 = ValeurUI
                    lCoupe = True
                Case Me.txt_TremieDroite.Name
                    MyPoutreLoc.DistanceDsl2 = ValeurUI
                    lCoupe = True
            End Select

            If lPortees Then Me.img_Portees.Invalidate()
            If lCoupe Then Me.img_Coupe.Invalidate()

        End If


    End Sub


    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

        Select Case MyTxt.Name
            Case Me.txt_MainSpan.Name

                'IL NE FAUT PAS TRANFORMER UNE VALEUR NUMERIQUE EN STRING POUR LA METTRE DANS UN DECIMAL
                'ValMin = Format(OptionsScope.PorteeMin / kUnit, formatLONGUEUR)
                'ValMax = Format(PORTEEMAX / kUnit, formatLONGUEUR)
                ValMin = OptionsScope.PorteeMin / kUnit
                ValMax = OptionsScope.PorteeMax / kUnit

            Case Me.txt_PorteeConsoleG.Name, Me.txt_PorteeConsoleD.Name

                ValMin = OptionsScope.PorteeConsoleMin / kUnit
                ValMax = OptionsScope.RatioPorteeConsoleMax * MyPoutreLoc.LongueurTravee(1) / kUnit
                'ValMin = Format(CONSOLEMIN / kUnit, formatLONGUEUR)
                'ValMax = Format(RATIOCONSOLEMAX * MyPoutreLoc.LongueurTravee(1) / kUnit, formatLONGUEUR)

            Case Me.txt_D1.Name

                ValMin = ENTRAXEMIN / kUnit
                ValMax = ENTRAXEMAX / kUnit

                If Not MyPoutreLoc.lIntermediaire And MyPoutreLoc.Section.lSlimFloor Then
                    lValMin = False
                    lValMax = False
                End If

            Case Me.txt_D2.Name

                ValMin = ENTRAXEMIN / kUnit
                ValMax = ENTRAXEMAX / kUnit

            Case Me.txt_TremieGauche.Name
                ValMin = 0
                ValMax = MyPoutreLoc.EntraxeD1 / 2

            Case Me.txt_TremieDroite.Name
                ValMin = 0
                ValMax = MyPoutreLoc.EntraxeD2 / 2

        End Select
        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, lValMin, ValMax, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            'ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

    Private Sub ContinuiteDalle_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ContinuiteDalleAppGauche.CheckedChanged, chk_ContinuiteDalleAppDroit.CheckedChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.chk_ContinuiteDalleAppGauche.Name
                MyPoutreLoc.lDalleContinueGauche = Me.chk_ContinuiteDalleAppGauche.Checked
            Case Me.chk_ContinuiteDalleAppDroit.Name
                MyPoutreLoc.lDalleContinueDroite = Me.chk_ContinuiteDalleAppDroit.Checked
        End Select
        Me.img_Portees.Invalidate()

    End Sub

    Private Sub ChoixConsoles(sender As Object, e As EventArgs) Handles chk_ConsoleGauche.CheckedChanged, chk_ConsoleDroite.CheckedChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.chk_ConsoleGauche.Name
                MyPoutreLoc.lTraveeConsoleGauche = Me.chk_ConsoleGauche.Checked
                iSelect = 0
            Case Me.chk_ConsoleDroite.Name
                MyPoutreLoc.lTraveeConsoleDroite = Me.chk_ConsoleDroite.Checked
                iSelect = 99
        End Select

        MAJI_PorteesConsoles()
        Me.img_Portees.Invalidate()
    End Sub

    Private Sub MAJI_PorteesConsoles()

        Me.txt_PorteeConsoleG.Visible = MyPoutreLoc.lTraveeConsoleGauche
        Me.etq_UnitL2.Visible = MyPoutreLoc.lTraveeConsoleGauche
        Me.img_L2.Visible = MyPoutreLoc.lTraveeConsoleGauche
        Me.chk_ContinuiteDalleAppGauche.Visible = (Not MyPoutreLoc.lTraveeConsoleGauche) And (MyPoutreLoc.lMixte)

        Me.txt_PorteeConsoleD.Visible = MyPoutreLoc.lTraveeConsoleDroite
        Me.etq_UnitL3.Visible = MyPoutreLoc.lTraveeConsoleDroite
        Me.img_L3.Visible = MyPoutreLoc.lTraveeConsoleDroite
        Me.chk_ContinuiteDalleAppDroit.Visible = (Not MyPoutreLoc.lTraveeConsoleDroite) And (MyPoutreLoc.lMixte)

    End Sub

    Private Sub ChoixPositionPoutre(sender As Object, e As EventArgs) Handles rad_Intermediaire.CheckedChanged
        If lBuild Then Exit Sub

        MyPoutreLoc.lIntermediaire = rad_Intermediaire.Checked

        If Not MyPoutreLoc.lIntermediaire Then Me.chk_TremieGauche.Checked = False

        Me.chk_TremieGauche.Enabled = MyPoutreLoc.lIntermediaire

        If Not MyPoutreLoc.lIntermediaire And MyPoutreLoc.Section.lSlimFloor Then
            Me.txt_D1.Text = 0
            Me.txt_D1.Enabled = False
        Else
            Me.txt_D1.Text = Me.txt_D2.Text
            Me.txt_D1.Enabled = True
        End If

        Me.img_Coupe.Invalidate()

    End Sub

    Private Sub ChoixTremies(sender As Object, e As EventArgs) Handles chk_TremieGauche.CheckedChanged, chk_TremieDroite.CheckedChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.chk_TremieGauche.Name
                MyPoutreLoc.lTremieGauche = Me.chk_TremieGauche.Checked

            Case Me.chk_TremieDroite.Name
                MyPoutreLoc.lTremieDroite = Me.chk_TremieDroite.Checked

        End Select

        MAJI_Tremies()

        Me.img_Coupe.Invalidate()

    End Sub

    Private Sub MAJI_Tremies()

        Me.txt_TremieGauche.Visible = MyPoutreLoc.lTremieGauche
        Me.etq_UnitL6.Visible = MyPoutreLoc.lTremieGauche
        Me.img_TremieGauche.Visible = MyPoutreLoc.lTremieGauche

        Me.txt_TremieDroite.Visible = MyPoutreLoc.lTremieDroite
        Me.etq_UnitL7.Visible = MyPoutreLoc.lTremieDroite
        Me.img_TremieDroite.Visible = MyPoutreLoc.lTremieDroite

    End Sub


#End Region

End Class