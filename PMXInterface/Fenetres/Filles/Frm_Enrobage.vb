Imports PMXMoteur2
Imports System.IO

Public Class Frm_Enrobage


#Region " Variables locales "

    Dim lBuild As Boolean = True

    Const kAdjust As Decimal = 0.9
    Dim MyEnrobage As New Cls_Enrobage_Partiel

    Dim MyBf As Decimal

    '--> Options d'affichage
    Enum Enu_LitArmaEnCours
        Superieur
        Intermediaire
        Inferieur
    End Enum
    Dim LitArmaEnCours As Enu_LitArmaEnCours = Enu_LitArmaEnCours.Inferieur

    '--> 
    Dim DiametreEtriers() As Decimal = {0.006, 0.008, 0.01}
    Dim DiametreArmaConst() As Decimal = {0.008, 0.01}
    Dim DiametreArmaInf() As Decimal = {0.008, 0.01, 0.012, 0.016, 0.02}
    Dim DiametreArmaNormal() As Decimal = {0.008, 0.01, 0.012}

    '--> Textes
    Dim str_Lit(2) As String
    Dim strTypeEtriers(2) As String

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Enrobage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

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
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_ENROBAGE")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== MENU PRINCIPAL ==============================================================='

                Me.lbl_Dimensions.Text = "Dimension"
                Me.lbl_Largeur.Text = "Largeur"

                Me.lbl_Type.Text = "Type"
                strTypeEtriers(0) = "Cadre"
                strTypeEtriers(1) = "Etrier soudé"
                strTypeEtriers(2) = "Cadre traversant"
                Me.lbl_Etriers.Text = "Etriers"
                Me.lbl_EnrobageEtrier.Text = "Enrobage"
                Me.lbl_DiametreE.Text = "Diamètre"

                Me.lbl_ArmaLongi.Text = "Armatures longitudinales"

                'Me.etq_OptionsG.Text = "Options du dessin"


            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim6.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

    End Sub

    Private Sub InitialiseVariable()
        MyBf = MyProjet.Poutres(MyProjet.IndEnCours).Sections(1).ProfilA.b_fs

        Cls_Enrobage_Partiel.DeepCopie(MyProjet.Poutres(MyProjet.IndEnCours).Sections(1).enrobage_partiel, MyEnrobage)

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.img_Enrobage.Dock = DockStyle.Fill

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


#End Region

#Region " Dessins "

    Private Sub img_Enrobage_Paint(sender As Object, e As PaintEventArgs) Handles img_Enrobage.Paint

        DessinFrmEnrobage(e.Graphics, MyProjet.Poutres(MyProjet.IndEnCours).Sections(1), MyEnrobage,
                          Me.img_Enrobage.ClientRectangle.Width, Me.img_Enrobage.ClientRectangle.Height, kAdjust, True, False, -1)
    End Sub


#End Region

#Region " Evènements "


#End Region

#Region " Evènements saisie "


#End Region

#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_Bc.Paint, img_Bc2.Paint, img_uz.Paint, img_ux.Paint, img_PhiEtrier.Paint, img_PhiAC.Paint, img_PhiA.Paint

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

        lIndice = False
        lGrec = False
        lEgal = True
        Select Case sender.name
            Case Me.img_Bc.Name
                strSymbol = "b"
                strIndice = "c"
            Case Me.img_Bc2.Name
                strSymbol = "b"
                strIndice = "f"
                lEgal = False
            Case Me.img_PhiEtrier.Name
                strSymbol = "j"
                strIndice = "e"
                lGrec = True
            Case Me.img_ux.Name
                strSymbol = "u"
                strIndice = "y"
            Case Me.img_uz.Name
                strSymbol = "u"
                strIndice = "z"
            Case Me.img_PhiA.Name
                strSymbol = "j"
                strIndice = "a"
                lGrec = True
            Case Me.img_PhiAC.Name
                strSymbol = "j"
                strIndice = "ac"
                lGrec = True
        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_Alignement.Gauche,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub


#End Region
End Class