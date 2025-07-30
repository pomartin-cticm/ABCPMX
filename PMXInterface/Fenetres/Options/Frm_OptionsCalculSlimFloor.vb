Imports PMXMoteur2

Public Class Frm_OptionsCalculSlimFloor

#Region " Variables locales "


    Const BALISE As String = "OPTCALSLIMFLOOR"

    Const formatGAMMA As String = "0.00"
    Dim lBuild As Boolean


#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFrm(Optional lAfficheOnly As Boolean = False)
        lBuild = True
        If Not lAfficheOnly Then
            GestionLangue(Frm_OptionsCalcul.BlocLangues(BALISE))
            GestionStyle()
            GestionUnites()
        End If
        AfficherScopeEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal myBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Slimfloors.Text = myBloc("TITLE")
            Me.lbl_Dalles.Text = myBloc("SLABS")

            '#-------------------- GEOMETRIE SELPOUTRE

            Me.lbl_SlimFloor.Text = myBloc("SLIMFLOORDIM")
            Me.lbl_hslimmax.Text = myBloc("DEPTHMAX")
            Me.lbl_bappmin.Text = myBloc("SLABSUPPORTMIN")
            Me.lbl_tpinfmin.Text = myBloc("THICKNESSMIN")
            Me.lbl_TwcdMin.Text = myBloc("THICKNESSWMIN")


            Me.lbl_MaintienBac.Text = myBloc("RESTRAINTBYSHEETS")
            Me.lbl_EntraxeCoutureMax.Text = myBloc("ECSEAMMAX")

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
        Me.lbl_MaintienBac.BackColor = CouleurBackBandeaux
        Me.lbl_MaintienBac.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(Me.txt_hslimmax, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_bappmin, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_tpinfmin, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_twcdmin, LogicielOptions.lExpert)
        PrepareTextBoxDipo(Me.txt_ecMax, False)

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

    End Sub

    Private Sub AfficherScopeEnCours()

        '--> Paramètres

        Me.txt_hslimmax.Text = GetStringInUnitN(LocalOptionsSlimFloor.Hslimmax, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
        Me.txt_bappmin.Text = GetStringInUnitN(LocalOptionsSlimFloor.Bappmin, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
        Me.txt_tpinfmin.Text = GetStringInUnitN(LocalOptionsSlimFloor.Tpinfmin, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
        Me.txt_twcdmin.Text = GetStringInUnitN(LocalOptionsSlimFloor.Twcdmin, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)
        Me.txt_ecMax.Text = GetStringInUnitN(LocalOptionsSlimFloor.Tpinfmin, Enu_TypeVariable.Dimension, 4, 3, NON_U, True)

    End Sub

#End Region

#Region " Evènements saisie "

    Private Sub SaisieText(sender As Object, e As EventArgs) Handles txt_hslimmax.TextChanged, txt_tpinfmin.TextChanged, txt_twcdmin.TextChanged, txt_bappmin.TextChanged

        If lBuild Then Exit Sub
        Dim lPortees As Boolean = False
        Dim lCoupe As Boolean = False

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Select Case sender.name
                Case Me.txt_hslimmax.Name
                    LocalOptionsSlimFloor.Hslimmax = ValeurUI
                Case Me.txt_bappmin.Name
                    LocalOptionsSlimFloor.Bappmin = ValeurUI
                Case Me.txt_tpinfmin.Name
                    LocalOptionsSlimFloor.Tpinfmin = ValeurUI
                Case Me.txt_twcdmin.Name
                    LocalOptionsSlimFloor.Twcdmin = ValeurUI
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

            Case Me.txt_twcdmin.Name

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

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_hslimmax.Paint, img_tpinfmin.Paint, img_bappmin.Paint, img_ecMax.Paint, img_twcdmin.Paint

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

            Case Me.img_twcdmin.Name
                strSymbol = "t"
                strIndice = "w,cd,min"

            Case Me.img_ecMax.Name
                strSymbol = "e"
                strIndice = "c"

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

End Class