Imports System.IO

Public Class Frm_CombinaisonsConstruction


#Region " Variables "
    Dim lBuild As Boolean

    Dim SymbolG As String = "g"
    Dim SymbolQ1 As String = "Qc"

    Dim str_Combinaison, strNb As String
    Dim strELU, strELS As String

    Const BALISE As String = "FRM_COMBINATIONS2"

    '-- Paramètres pour l'affichage des combinaisons prédéfinies
    Dim MyBrush As Brush
    Dim MyBrushBlue As Brush
    Dim MyBrushFond As Brush

    Dim MyBrushNoFond As Brush
    Dim MyBrushUnSelected As Brush
    Dim BrushBlue As Brush
    Dim BrushBlack As Brush
    Dim ColorSelect As Color = Color.LightGray
    Dim ColorUnSelect As Color
    Dim ColorFontUnSelected As Color = Color.Gray

    Dim FontNormal As Font
    Dim FontIndice As Font
    Dim FontSymbol As Font

    Dim Ind_Custom As Integer
    Dim fmtGamma As String = "0.00"

#End Region


#Region "===OUVERTURE==="

    Public Sub InitialiserFenetre(lComb() As Boolean, CoefCombi() As List(Of Decimal), nbCombi As Integer, nbPredef As Integer, nbCustom As Integer)
        '--------------------------------------------------------------------------------------------------------------------
        '   lComb       [E] :   Table des combinaisons sélectionnées
        '   CoefCombi   [E] :   Coefficients des combinaisons
        '   nbCombi     [E] :   Nombre de combinaisons, au total
        '   nbPredef    [E] :   Nombre de combinaisons prédéfinies
        '   nbCustom    [E] :   Nombre de combinaisons personnalisées
        '--------------------------------------------------------------------------------------------------------------------

        lBuild = True

        GestionLangues(Frm_Combinaisons.BlocLangues(BALISE))
        PrepareFenetre()
        GestionStyle()
        ' MAJI_TypeEL()
        ' AfficheCombinaisonEnCours(lComb, CoefCombi, nbCombi, nbPredef, nbCustom)
        ' MAJI_Equations()

        lBuild = False
    End Sub

    Private Sub GestionLangues(ByVal MyBloc As Dictionary(Of String, String))

        Try

            '=== COMBINAISONS

            Me.lbl_ELU.Text = MyBloc("ULSTATES")
            Me.lbl_ELS.Text = MyBloc("SLSTATES")

            '=== Textes

            str_Combinaison = MyBloc("COMBINATION")
            strELU = MyBloc("ULS")
            strELS = MyBloc("SLS")


        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try

    End Sub

    Private Sub PrepareFenetre()

        '--[ Préparation des étiquettes de combinaison Custom

        Me.etq_Custom01_G.Text = SymbolG & " +"
        Me.etq_Custom01_Q1.Text = SymbolQ1

        Me.etq_Custom02_G.Text = SymbolG & " +"
        Me.etq_Custom02_Q1.Text = SymbolQ1

        Dim Chaine As String = str_Combinaison
        Dim ChaineEL As String


        Chaine = str_Combinaison & " "

        Me.chk_Combinaison01.Text = Chaine & strELU & " " & strNb & "1"
        Me.chk_Combinaison02.Text = Chaine & strELS & " " & strNb & "1"

        Me.chk_CombiCustom01.Text = Chaine & strELU & " " & strNb & "2"
        Me.chk_CombiCustom02.Text = Chaine & strELS & " " & strNb & "2"
    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon
        Me.lbl_ELU.BackColor = CouleurBackBandeaux
        Me.lbl_ELU.ForeColor = CouleurForeBandeaux
        Me.lbl_ELS.BackColor = CouleurBackBandeaux
        Me.lbl_ELS.ForeColor = CouleurForeBandeaux

        MyBrush = New SolidBrush(Color.Black)
        MyBrushBlue = New SolidBrush(Color.Blue)
        MyBrushFond = New SolidBrush(ColorSelect)

        ColorUnSelect = Me.pan_Predefinies.BackColor
        MyBrushNoFond = New SolidBrush(ColorUnSelect)
        MyBrushUnSelected = New SolidBrush(ColorFontUnSelected)

        FontNormal = New Font(Me.chk_Combinaison01.Font.Name, 8)
        FontIndice = New Font(Me.chk_Combinaison01.Font.Name, 7)
        FontSymbol = New Font("Symbol", 9)

        Me.pan_Combinaisons.Dock = DockStyle.Fill
    End Sub

#End Region

End Class