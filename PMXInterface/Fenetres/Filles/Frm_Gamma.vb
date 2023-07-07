Imports PMXMoteur2
Imports System.IO

Public Class Frm_Gamma

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim MyPoutreLoc As New cls_Poutre

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Gamma_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_BASIC")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== MENU PRINCIPAL ==============================================================='



            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_Chargement.BackColor = CouleurBackBandeaux
        Me.lbl_Chargement.ForeColor = CouleurForeBandeaux
        Me.lbl_Accompagnement.BackColor = CouleurBackBandeaux
        Me.lbl_Accompagnement.ForeColor = CouleurForeBandeaux
        Me.lbl_Resistance.BackColor = CouleurBackBandeaux
        Me.lbl_Resistance.ForeColor = CouleurForeBandeaux

        If Not MyPoutreLoc.lMixte Then
            Me.Tab_Dalle.Visible = False
        End If

    End Sub

    Private Sub AfficherPoutreEnCours()

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

            End If
            Me.Close()
        End If
    End Sub


    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

    End Sub

    Private Sub Frm_Basic_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub img_L1_Click(sender As Object, e As EventArgs) Handles img_GammaGsup.Click

    End Sub

    Private Sub txt_MainSpan_TextChanged(sender As Object, e As EventArgs) Handles txt_GammaGsup.TextChanged

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

#End Region

#Region " Dessins "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_GammaGsup.Paint, img_GammaGinf.Paint, img_GammaQ.Paint, img_Psi0.Paint, img_Q1.paint, img_Q2.paint, img_Psi1.Paint, img_Psi2.Paint, img_GammaM0.Paint, img_GammaM1.Paint, img_GammaM2.Paint, img_GammaC.Paint, img_GammaV.Paint, img_GammaVs.Paint, img_GammaVp.Paint, img_GammaS.Paint, img_GammaP.Paint, img_GammaM_fi.Paint, img_GammaC_fi.Paint, img_GammaV_fi.Paint

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
        lGrec = True
        lEgal = True

        Select Case sender.name

            Case Me.img_GammaGsup.Name

                strSymbol = "g"
                strIndice = "G,sup"

            Case Me.img_GammaGinf.Name

                strSymbol = "g"
                strIndice = "G,inf"

            Case Me.img_GammaQ.Name

                strSymbol = "g"
                strIndice = "Q"

            Case Me.img_Q1.Name

                lGrec = False
                lEgal = False

                xStart = sWI * 0.7
                xPen = xStart

                strSymbol = "Q"
                strIndice = "1"

            Case Me.img_Q2.Name

                lGrec = False
                lEgal = False

                xStart = sWI * 0.7
                xPen = xStart

                strSymbol = "Q"
                strIndice = "2"

            Case Me.img_Psi0.Name

                strSymbol = "y"
                strIndice = "0"

            Case Me.img_Psi1.Name

                strSymbol = "y"
                strIndice = "1"

            Case Me.img_Psi2.Name

                strSymbol = "y"
                strIndice = "2"

            Case Me.img_GammaM0.Name

                strSymbol = "g"
                strIndice = "M0"

            Case Me.img_GammaM1.Name

                strSymbol = "g"
                strIndice = "M1"

            Case Me.img_GammaM2.Name

                strSymbol = "g"
                strIndice = "M2"

            Case Me.img_GammaC.Name

                strSymbol = "g"
                strIndice = "C"

            Case Me.img_GammaV.Name

                strSymbol = "g"
                strIndice = "V"

            Case Me.img_GammaVs.Name

                strSymbol = "g"
                strIndice = "Vs"

            Case Me.img_GammaVp.Name

                strSymbol = "g"
                strIndice = "Vp"

            Case Me.img_GammaS.Name

                strSymbol = "g"
                strIndice = "S"

            Case Me.img_GammaP.Name

                strSymbol = "g"
                strIndice = "P"

            Case Me.img_GammaM_fi.Name

                strSymbol = "g"
                strIndice = "M,fi"

            Case Me.img_GammaC_fi.Name

                strSymbol = "g"
                strIndice = "C,fi"

            Case Me.img_GammaV_fi.Name

                strSymbol = "g"
                strIndice = "V,fi"

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_Alignement.Gauche,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Evènements "


#End Region

#Region " Evènements saisie "


#End Region
End Class