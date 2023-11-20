Imports PMXMoteur2
Imports System.IO

Public Class Frm_ModularRatio

#Region " Attributs "

    Dim tabPsiL() As Decimal = {0, 0.55, 1.1, 1.5}

    Dim AgeT As Integer = 50 * 365
    Dim AgeT0 As Integer = 28

    Dim MonBeton As New cls_Beton

    Dim RayonH0 As Decimal = 0.2

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_ModularRatio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GestionLangues()
        GestionStyle()
        GestionUnites()
        InitialiserFenetre()
        IntialiseVariables()
    End Sub

    Private Sub IntialiseVariables()

        MonBeton.lLeger = False
        MonBeton.Classe = cls_Beton.TabClasseBeton(0)

    End Sub

    Private Sub InitialiserFenetre()
        RemplirComboRH()
        RemplirComboBeton()
        RemplirComboPsiL()

        Me.txt_AgeT.Text = GetStringInUnit(AgeT, Enu_TypeVariable.SansType, 2, 0, False)
        Me.txt_AgeT0.Text = GetStringInUnit(AgeT0, Enu_TypeVariable.SansType, 2, 0, False)

        Me.txt_H0.Text = GetStringInUnit(RayonH0, Enu_TypeVariable.Dimension, 3, 2, False)

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitDim.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
    End Sub

    Private Sub RemplirComboRH()

        Me.cmb_RH.Items.Clear()

        For i As Integer = 0 To cls_OptionsCalcul.tabRH.GetUpperBound(0)

            Me.cmb_RH.Items.Add(GetStringInUnit(cls_OptionsCalcul.tabRH(i), Enu_TypeVariable.SansType, 2, 0, False) & "%")

        Next

        Me.cmb_RH.SelectedIndex = 0

    End Sub

    Private Sub RemplirComboPsiL()

        Me.cmb_PsiL.Items.Clear()

        For i As Integer = 0 To Me.tabPsiL.GetUpperBound(0)

            Me.cmb_PsiL.Items.Add(GetStringInUnit(Me.tabPsiL(i), Enu_TypeVariable.SansType, 2, 2, False))

        Next

        Me.cmb_RH.SelectedIndex = 0

    End Sub

    Private Sub RemplirComboBeton()

        Me.cmb_ClasseBeton.Items.Clear()

        Me.cmb_ClasseBeton.Items.AddRange(cls_Beton.TabClasseBeton)

        Me.cmb_ClasseBeton.SelectedIndex = 0

    End Sub

    Private Sub GestionLangues()

        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_MODULARRATIOTOOL")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")

                Me.btn_Annuler.Text = Bloc("CANCEL")
                Me.btn_OK.Text = Bloc("CLOSE")

                Me.lbl_PsiL.Text = Bloc("PSIL")
                Me.lbl_RelativeRH.Text = Bloc("RELATIVEHUMIDITY")


            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, "Frm_ModularRatio/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.TLPan_PartieBasse.ColumnStyles(1).Width = 0
        Me.TLPan_PartieBasse.ColumnStyles(2).Width = 0

        Me.lbl_Parameters.BackColor = CouleurBackBandeaux
        Me.lbl_Parameters.ForeColor = CouleurForeBandeaux

    End Sub

#End Region

#Region " Dessin des symboles "


    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_PsiL.Paint, img_RH.Paint, img_H0.Paint, img_AgeT0.Paint, img_AgeT.Paint

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
        Dim AlignH As Enu_AlignementH = Enu_AlignementH.Droite

        '--> Initialisation

        lIndice = False
        lGrec = False
        lEgal = True
        Select Case sender.name

            Case Me.img_RH.Name
                strSymbol = "RH"
                strIndice = ""

            Case Me.img_PsiL.Name
                strSymbol = "y"
                strIndice = "L"
                lGrec = True

            Case Me.img_AgeT.Name
                strSymbol = "t"
                strIndice = ""

            Case Me.img_AgeT0.Name
                strSymbol = "t"
                strIndice = "0"

            Case Me.img_H0.Name
                strSymbol = "h"
                strIndice = "0"
        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub


#End Region

End Class