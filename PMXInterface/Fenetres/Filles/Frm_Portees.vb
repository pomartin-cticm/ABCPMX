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


#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiserFenetre()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_PORTEES")
            BlocLine.CreationBloc(Bloc)

            Try

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

        Me.lbl_Portees.BackColor = CouleurBackBandeaux
        Me.lbl_Portees.ForeColor = CouleurForeBandeaux

        Me.TLPan_Portees.ColumnStyles(0).Width = LargeurColonneSaisie

        Me.TLPan_Gauche.RowStyles(2).Height = 0

        Dim Hcum As Integer = 0
        For i As Integer = 0 To 1
            Hcum += Me.TLPan_Gauche.RowStyles(i).Height
        Next
        Me.TLPan_Portees.Height = Hcum

        Me.img_Portees.Dock = DockStyle.Fill
    End Sub

    Private Sub AfficherPoutreEnCours()

        With MyProjet.Poutres(MyProjet.IndEnCours)

            Me.txt_MainSpan.Text = GetStringNoUnit(.LongueurTravee(1), Enu_TypeVariable.Longueur)

            Me.chk_ConsoleGauche.Checked = .lTraveeConsoleGauche
            Me.txt_PorteeConsoleG.Text = GetStringNoUnit(.LongueurTravee(0), Enu_TypeVariable.Longueur)

            Me.chk_ConsoleDroite.Checked = .lTraveeConsoleDroite
            Me.txt_PorteeConsoleD.Text = GetStringNoUnit(.LongueurTravee(.IndiceTraveeConsoleDroite), Enu_TypeVariable.Longueur)

        End With

        MAJ_PorteesConsoles()
    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitL1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitL3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

    End Sub

#End Region

#Region "===FERMETURE==="

    Public Sub TraitementSaisie()

    End Sub


#End Region

#Region " Dessins "

    Private Sub DessinPoutre(sender As Object, e As PaintEventArgs) Handles img_Portees.Paint

        DessinFrmPortee(e.Graphics, MyProjet.Poutres(MyProjet.IndEnCours), Me.img_Portees.ClientRectangle.Width, Me.img_Portees.ClientRectangle.Height, 1, iSelect, True)

    End Sub

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_L3.Paint, img_L2.Paint, img_L1.Paint

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

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_Alignement.Gauche,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub



#End Region

#Region " Evènements "


#End Region

#Region " Evènements saisie "

    Private Sub ChoixConsoles(sender As Object, e As EventArgs) Handles chk_ConsoleGauche.CheckedChanged, chk_ConsoleDroite.CheckedChanged
        If lBuild Then Exit Sub

        Select Case sender.name
            Case Me.chk_ConsoleGauche.Name
                MyProjet.Poutres(MyProjet.IndEnCours).lTraveeConsoleGauche = Me.chk_ConsoleGauche.Checked
                iSelect = 0
            Case Me.chk_ConsoleDroite.Name
                MyProjet.Poutres(MyProjet.IndEnCours).lTraveeConsoleDroite = Me.chk_ConsoleDroite.Checked
                iSelect = 99
        End Select

        MAJ_PorteesConsoles()
        Me.img_Portees.Invalidate()
    End Sub

    Private Sub MAJ_PorteesConsoles()

        Me.txt_PorteeConsoleG.Visible = MyProjet.Poutres(MyProjet.IndEnCours).lTraveeConsoleGauche
        Me.etq_UnitL2.Visible = MyProjet.Poutres(MyProjet.IndEnCours).lTraveeConsoleGauche
        Me.img_L2.Visible = MyProjet.Poutres(MyProjet.IndEnCours).lTraveeConsoleGauche

        Me.txt_PorteeConsoleD.Visible = MyProjet.Poutres(MyProjet.IndEnCours).lTraveeConsoleDroite
        Me.etq_UnitL3.Visible = MyProjet.Poutres(MyProjet.IndEnCours).lTraveeConsoleDroite
        Me.img_L3.Visible = MyProjet.Poutres(MyProjet.IndEnCours).lTraveeConsoleDroite

    End Sub


#End Region

End Class