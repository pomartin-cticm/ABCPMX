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

    Dim MyPoutreLoc As New cls_Poutre

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Portees_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        InitialiserVariables()
        AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub InitialiserVariables()
        cls_Poutre.Clone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_PORTEES")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITRE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== MENU PRINCIPAL ==============================================================='

                Me.lbl_Portees.Text = Bloc("TRAVEES")

                Me.lbl_MainSpan.Text = Bloc("MAINSPAN")
                Me.chk_ConsoleGauche.Text = Bloc("CONSOLEG")
                Me.chk_ConsoleDroite.Text = Bloc("CONSOLED")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If
    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

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

    End Sub

    Private Sub AfficherPoutreEnCours()

        With MyProjet.Poutres(MyProjet.IndEnCours)

            Me.txt_MainSpan.Text = GetStringNoUnit(.LongueurTravee(1), Enu_TypeVariable.Longueur)

            Me.chk_ConsoleGauche.Checked = .lTraveeConsoleGauche
            Me.txt_PorteeConsoleG.Text = GetStringNoUnit(.LongueurTravee(0), Enu_TypeVariable.Longueur)

            Me.chk_ConsoleDroite.Checked = .lTraveeConsoleDroite
            Me.txt_PorteeConsoleD.Text = GetStringNoUnit(.LongueurTravee(.IndiceTraveeConsoleDroite), Enu_TypeVariable.Longueur)

            Me.txt_D1.Text = GetStringNoUnit(.EntraxeD1, Enu_TypeVariable.Longueur)
            Me.txt_D2.Text = GetStringNoUnit(.EntraxeD2, Enu_TypeVariable.Longueur)

            Me.rad_Intermediaire.Checked = MyPoutreLoc.lIntermediaire

            Me.chk_TremieGauche.Checked = .lTremieGauche
            If .lTremieGauche Then Me.txt_D1.Text = GetStringNoUnit(.DistanceDsl1, Enu_TypeVariable.Longueur)

            Me.chk_TremieDroite.Checked = .lTremieDroite
            If .lTremieDroite Then Me.txt_D2.Text = GetStringNoUnit(.DistanceDsl2, Enu_TypeVariable.Longueur)

        End With

        MAJ_PorteesConsoles()
        MAJ_Tremies()

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

    Public Sub TraitementSaisie()

    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click

        If ValideSaisieFenetre() Then

            Dim lModif As Boolean = False

            TransfertSaisie(lModif)

            If lModif Then

            End If

            Me.Close()
        End If

    End Sub

    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        Dim Indice As Integer = MyPoutreLoc.IndiceTraveeConsoleDroite

        '--> Portée travée principale

        lModif = False
        If MyProjet.Poutres(MyProjet.IndEnCours).LongueurTravee(1) <> MyPoutreLoc.LongueurTravee(1) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).LongueurTravee(1) = MyPoutreLoc.LongueurTravee(1)
        End If

        '--> Console gauche

        If MyProjet.Poutres(MyProjet.IndEnCours).lTraveeConsoleGauche <> MyPoutreLoc.lTraveeConsoleGauche Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).lTraveeConsoleGauche = MyPoutreLoc.lTraveeConsoleGauche
        End If

        If MyPoutreLoc.lTraveeConsoleGauche Then
            If MyProjet.Poutres(MyProjet.IndEnCours).LongueurTravee(0) <> MyPoutreLoc.LongueurTravee(0) Then
                lModif = True
                MyProjet.Poutres(MyProjet.IndEnCours).LongueurTravee(0) = MyPoutreLoc.LongueurTravee(0)
            End If
        End If

        '--> Console droite

        If MyProjet.Poutres(MyProjet.IndEnCours).lTraveeConsoleDroite <> MyPoutreLoc.lTraveeConsoleDroite Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).lTraveeConsoleDroite = MyPoutreLoc.lTraveeConsoleDroite
        End If

        If MyPoutreLoc.lTraveeConsoleDroite Then
            If MyProjet.Poutres(MyProjet.IndEnCours).LongueurTravee(Indice) <> MyPoutreLoc.LongueurTravee(Indice) Then
                lModif = True
                MyProjet.Poutres(MyProjet.IndEnCours).LongueurTravee(Indice) = MyPoutreLoc.LongueurTravee(Indice)
            End If
        End If

    End Sub

    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        Me.Close()
    End Sub

#End Region

#Region " Dessins "

    Private Sub img_Coupe_Paint(sender As Object, e As PaintEventArgs) Handles img_Coupe.Paint

        DessinFrmCoupe(e.Graphics, MyPoutreLoc, Me.img_Coupe.ClientRectangle.Width, Me.img_Coupe.ClientRectangle.Height, 1, iSelect, True)

    End Sub


    Private Sub DessinPoutre(sender As Object, e As PaintEventArgs) Handles img_Portees.Paint

        DessinFrmPortee(e.Graphics, MyPoutreLoc, Me.img_Portees.ClientRectangle.Width, Me.img_Portees.ClientRectangle.Height, 1, iSelect, True)

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

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_Alignement.Gauche,
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



#End Region

#Region " Evènements saisie "

    Private Sub SaisieText(sender As Object, e As EventArgs) Handles txt_PorteeConsoleG.TextChanged, txt_PorteeConsoleD.TextChanged, txt_MainSpan.TextChanged,
                                                                     txt_TremieDroite.TextChanged, txt_TremieGauche.TextChanged
        'txt_D2.TextChanged, txt_D1.TextChanged

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
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

        Select Case MyTxt.Name
            Case Me.txt_MainSpan.Name

                ValMin = PORTEEMIN / kUnit
                ValMax = PORTEEMAX / kUnit

            Case Me.txt_PorteeConsoleG.Name, Me.txt_PorteeConsoleD.Name

                ValMin = CONSOLEMIN / kUnit
                ValMax = RATIOCONSOLEMAX * MyPoutreLoc.LongueurTravee(1) / kUnit

            Case Me.txt_TremieGauche.Name
                ValMin = 0
                ValMax = MyPoutreLoc.EntraxeD1 / 2

            Case Me.txt_TremieDroite.Name
                ValMin = 0
                ValMax = MyPoutreLoc.EntraxeD2 / 2

        End Select
        iErreur = ValideSaisieNombre(MyTxt.Text, True, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, ValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

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

        MAJ_PorteesConsoles()
        Me.img_Portees.Invalidate()
    End Sub

    Private Sub MAJ_PorteesConsoles()

        Me.txt_PorteeConsoleG.Visible = MyPoutreLoc.lTraveeConsoleGauche
        Me.etq_UnitL2.Visible = MyPoutreLoc.lTraveeConsoleGauche
        Me.img_L2.Visible = MyPoutreLoc.lTraveeConsoleGauche

        Me.txt_PorteeConsoleD.Visible = MyPoutreLoc.lTraveeConsoleDroite
        Me.etq_UnitL3.Visible = MyPoutreLoc.lTraveeConsoleDroite
        Me.img_L3.Visible = MyPoutreLoc.lTraveeConsoleDroite

    End Sub

    Private Sub ChoixPositionPoutre(sender As Object, e As EventArgs) Handles rad_Intermediaire.CheckedChanged
        If lBuild Then Exit Sub

        MyPoutreLoc.lIntermediaire = rad_Intermediaire.Checked

        If Not MyPoutreLoc.lIntermediaire Then Me.chk_TremieGauche.Checked = False

        Me.chk_TremieGauche.Enabled = MyPoutreLoc.lIntermediaire

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

        MAJ_Tremies()

    End Sub

    Private Sub MAJ_Tremies()

        Me.txt_TremieGauche.Visible = MyPoutreLoc.lTremieGauche
        Me.etq_UnitL6.Visible = MyPoutreLoc.lTremieGauche
        Me.img_TremieGauche.Visible = MyPoutreLoc.lTremieGauche

        Me.txt_TremieDroite.Visible = MyPoutreLoc.lTremieDroite
        Me.etq_UnitL7.Visible = MyPoutreLoc.lTremieDroite
        Me.img_TremieDroite.Visible = MyPoutreLoc.lTremieDroite

    End Sub








#End Region

End Class