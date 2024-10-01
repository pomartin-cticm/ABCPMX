Imports PMXMoteur2

Public Class Frm_OptionsCalculSlimFloor

#Region " Variables locales "


    Const BALISE As String = "OPTCALSLIMFLOOR"

    Const formatGAMMA As String = "0.00"
    Dim lBuild As Boolean


#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFrm()
        lBuild = True
        GestionLangue(Frm_OptionsCalcul.BlocLangues(BALISE))
        GestionStyle()
        GestionUnites()
        AfficherScopeEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Slimfloors.Text = MyBloc("TITLE")
            Me.lbl_Dalles.Text = MyBloc("SLABS")

            '#-------------------- GEOMETRIE POUTRE

            Me.lbl_SlimFloor.Text = MyBloc("SLIMFLOORDIM")
            Me.lbl_hslimmax.Text = MyBloc("DEPTHMAX")
            Me.lbl_bappmin.Text = MyBloc("SLABSUPPORTMIN")
            Me.lbl_tpinfmin.Text = MyBloc("THICKNESSMIN")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_Slimfloor.Dock = DockStyle.Fill

        Me.lbl_Slimfloors.BackColor = CouleurBackBandeaux
        Me.lbl_Slimfloors.ForeColor = CouleurForeBandeaux
        Me.lbl_Dalles.BackColor = CouleurBackBandeaux
        Me.lbl_Dalles.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(Me.txt_hslimmax, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_bappmin, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_tpinfmin, LogicielOptions.lExpert)

    End Sub

    Private Sub GestionUnites()
        Me.lbl_UnitDim.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.lbl_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.lbl_UnitDim3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

    End Sub

    Private Sub AfficherScopeEnCours()

        '--> Portées

        Me.txt_hslimmax.Text = GetStringInUnit(LocalOptionsSlimFloor.hslimmax, Enu_TypeVariable.Dimension, 4, 2, False)
        Me.txt_bappmin.Text = GetStringInUnit(LocalOptionsSlimFloor.bappmin, Enu_TypeVariable.Dimension, 4, 2, False)
        Me.txt_tpinfmin.Text = GetStringInUnit(LocalOptionsSlimFloor.tpinfmin, Enu_TypeVariable.Dimension, 4, 2, False)


    End Sub

#End Region

#Region " Evènements saisie "

    Private Sub SaisieText(sender As Object, e As EventArgs) Handles txt_hslimmax.TextChanged, txt_tpinfmin.TextChanged

        If lBuild Then Exit Sub
        Dim lPortees As Boolean = False
        Dim lCoupe As Boolean = False

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Select Case sender.name
                Case Me.txt_hslimmax.Name
                    LocalOptionsSlimFloor.hslimmax = ValeurUI
                Case Me.txt_bappmin.Name
                    LocalOptionsSlimFloor.bappmin = ValeurUI
                Case Me.txt_tpinfmin.Name
                    LocalOptionsSlimFloor.tpinfmin = ValeurUI
            End Select

        End If

    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Const HAUTEURMIN As Decimal = 50 / 1000 '50 mm
        Const HAUTEURMAX As Decimal = 1 '1 m

        Const BAPPMIN As Decimal = 0
        Const BAPPMAX As Decimal = 500 / 1000 '500 mm

        Const EPMIN As Decimal = 1 / 1000 '1 mm
        Const EPMAX As Decimal = 100 / 1000 ' 10 cm

        Select Case MyTxt.Name
            Case Me.txt_hslimmax.Name

                ValMin = HAUTEURMIN
                ValMax = HAUTEURMAX
                lValMax = False

            Case Me.txt_bappmin.Name

                ValMin = BAPPMIN
                ValMax = BAPPMAX
                lValMax = False

            Case Me.txt_tpinfmin.Name

                ValMin = EPMIN
                ValMax = EPMAX
                lValMax = False

        End Select
        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, lValMin, ValMax, lValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

#End Region

#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_hslimmax.Paint, img_tpinfmin.Paint, img_bappmin.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lIndice, lEgal As Boolean
        Dim AlignH As Enu_AlignementH

        '--> Initialisation

        lIndice = False
        lGrec = False
        lEgal = False
        AlignH = Enu_AlignementH.Centre

        Select Case sender.name
            Case Me.img_hslimmax.Name
                strSymbol = "h"
                strIndice = "slim,max"

            Case Me.img_bappmin.Name
                strSymbol = "b"
                strIndice = "app,min"

            Case Me.img_tpinfmin.Name
                strSymbol = "t"
                strIndice = "p,inf,min"

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

End Class