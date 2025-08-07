Imports System.Collections.Specialized.BitVector32
Imports PMXMoteur2

Public Class Frm_InfoLogiciel

#Region " Variables "

    Dim CouleurBase As Color

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Info_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GestionLangues()
        GestionStyle()
        PrepareFenetre()
    End Sub

    Private Sub GestionStyle()

        CouleurBase = InfoW.CouleurBase
        Me.lbl_InfoW.BackColor = CouleurBase
        Me.pan_General.BackColor = CouleurBase
        Me.lbl_InfoW.ForeColor = CouleurForeBandeaux
        Me.rtb_Info.BorderStyle = BorderStyle.None

        'If InfoW.linfo Then
        '    Me.img_info.Image = imgList_Info.Images("Info")
        'Else
        '    Me.img_info.Image = imgList_Info.Images("Warning")
        'End If

        If InfoW.Mode = Cls_InfoW.enu_ModeW.Information Then
            Me.TLPan_General.RowStyles(2).Height = 0
        End If

    End Sub

    Private Sub GestionLangues()

        Dim Cle As String = "INFO"

        Select Case InfoW.Mode
            Case Cls_InfoW.enu_ModeW.Information : Cle = "INFO"
            Case Cls_InfoW.enu_ModeW.Erreur : Cle = "ERROR"
            Case Cls_InfoW.enu_ModeW.Avertissement : Cle = "WARNING"
        End Select

        Me.lbl_InfoW.Text = LogicielInfo.NomLogiciel & " - " & InfoW.BlocF(Cle)


        Me.lbl_ContactSupport.Text = InfoW.BlocF("CONTACT")

    End Sub

    Private Sub PrepareFenetre()

        '== Affichage des informations du logiciel ==

        Me.rtb_Info.Text = String.Join(Environment.NewLine, InfoW.InfoW_msg)

        Me.lbk_Support.Text = LogicielInfo.MailSupport
        Me.lbk_Support.Left = Me.lbl_ContactSupport.Left + Me.lbl_ContactSupport.Width + 3

    End Sub

#End Region

#Region "===FERMETURE==="
    Private Sub Frm_InfoLogiciel_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove

    End Sub

    Private Sub Fermer()
        Me.Close()
    End Sub

    Private Sub img_info_Click(sender As Object, e As EventArgs) Handles img_info.Click
        Fermer()
    End Sub

    'Private Sub rtb_Info_MouseMove(sender As Object, e As MouseEventArgs) Handles rtb_Info.MouseMove
    '    Fermer()
    'End Sub

    Private Sub img_Close_Click(sender As Object, e As EventArgs) Handles img_Close.Click
        Fermer()
    End Sub

    Private Sub lbl_InfoW_Click(sender As Object, e As EventArgs) Handles lbl_InfoW.Click
        Fermer()
    End Sub

#End Region

#Region " Evènements "
    Private Sub lbk_Support_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbk_Support.LinkClicked
        Dim myDest As String = LogicielInfo.MailSupport

        Try

            PrepareMailSupport(myDest)

        Catch ex As Exception
            MsgBox("Erreur d'ouverture mail | Error opening mail", MsgBoxStyle.Critical, "Frm_About/LinkSupport_LinkClicked")
        End Try
    End Sub


#End Region

#Region " Paint des images symboles "

    Private Sub img_info_Paint(sender As Object, e As PaintEventArgs) Handles img_info.Paint

        DessinSymbole(e.Graphics, Me.img_info.ClientRectangle.Width, Me.img_info.ClientRectangle.Height, 1, InfoW.CouleurBase)

    End Sub

    Private Sub DessinSymbole(ByRef myGr As Graphics, sWi As Decimal, sHi As Decimal, kAdjust As Double, ColorSy As Color)
        '-----------------------------------------------------------------------------------------------------------
        '   02/08/25 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Dessin du symbole dans un objet image
        '-----------------------------------------------------------------------------------------------------------

        Select Case InfoW.Mode
            Case Cls_InfoW.enu_ModeW.Information
                DessinSymbInfo(myGr, sWi, sHi, kAdjust, ColorSy)
            Case Cls_InfoW.enu_ModeW.Erreur
                DessinSymbError(myGr, sWi, sHi, kAdjust, ColorSy)
            Case Cls_InfoW.enu_ModeW.Avertissement
        End Select
    End Sub

    Private Sub DessinSymbError(ByRef myGr As Graphics, sWi As Decimal, sHi As Decimal, kAdjust As Double, ColorSy As Color)
        '-----------------------------------------------------------------------------------------------------------
        '   02/08/25 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Dessin d'un symbole information dans un objet image
        '-----------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal

        Dim xLeft As Decimal = 0
        Dim yTop As Decimal = 0

        Dim myParAff As Struc_Affichage
        Dim myBrushSy As New SolidBrush(ColorSy)
        Dim myBrushT As New SolidBrush(Me.pan_Info.BackColor)
        Dim myFontLoc As Font

        '--( Initialisations

        xMax = 1
        yMax = 1
        yMin = -1
        xMin = -1

        ParametresAffichage(myParAff, xMin, yMin, xMax - xMin, yMax - yMin, sWi, sHi, xLeft, yTop, kAdjust)

        '--( Symbole

        AddCerclePlein(myGr, myBrushSy, 0, 0, 2, myParAff, False)

        myFontLoc = Me.Label1.Font

        AddTexte(myGr, myBrushT, "x", myFontLoc, 0.05, -0.05, myParAff, HorizontalAlignment.Center, VerticalAlignement.Middle)

    End Sub

    Private Sub DessinSymbInfo(ByRef myGr As Graphics, sWi As Decimal, sHi As Decimal, kAdjust As Double, ColorSy As Color)
        '-----------------------------------------------------------------------------------------------------------
        '   02/08/25 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------
        '   Dessin d'un symbole information dans un objet image
        '-----------------------------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal

        Dim xLeft As Decimal = 0
        Dim yTop As Decimal = 0

        Dim myParAff As Struc_Affichage
        Dim myBrushSy As New SolidBrush(ColorSy)
        Dim myBrushT As New SolidBrush(Me.pan_Info.BackColor)
        Dim myFontLoc As Font

        '--( Initialisations

        xMax = 1
        yMax = 1
        yMin = -1
        xMin = -1

        ParametresAffichage(myParAff, xMin, yMin, xMax - xMin, yMax - yMin, sWi, sHi, xLeft, yTop, kAdjust)

        '--( Cercle

        AddCerclePlein(myGr, myBrushSy, 0, 0, 2, myParAff, False)

        myFontLoc = Me.Label1.Font

        AddTexte(myGr, myBrushT, "i", myFontLoc, 0.05, -0.05, myParAff, HorizontalAlignment.Center, VerticalAlignement.Middle)

    End Sub


#End Region

End Class