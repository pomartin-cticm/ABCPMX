Imports PMXMoteur2
Imports System.IO

Public Class Frm_OptionsFeuN

#Region " Parametres "

    Dim lBuild As Boolean

    Dim Bloc As Dictionary(Of String, String)

    Public BeamLoc As cls_Poutre

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_OptionsFeuN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True
        GestionLangues()
        InitialiseFenetre()
        lBuild = False
    End Sub

    Private Sub InitialiseFenetre()

        Me.Icon = Frm_PMX.Icon

        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), BeamLoc)

        Select Case AffichageOptFeu
            Case Enu_AffichageOptions.Calcul
                Me.rdb_OptionsCalcul.Checked = True
            Case Enu_AffichageOptions.Parametres
                Me.rdb_Parametres.Checked = True
            Case Enu_AffichageOptions.Poutre
                Me.rdb_Poutre.Checked = True
        End Select

        AffichageOptionsEnCours()

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Bloc = New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_FIREOPTIONS")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== PARAMETRES POUTRES ==============================================================='

                Me.rdb_Poutre.Text = Bloc("BEAMPARAM")

                '=== OPTIONS DE CALCUL ==============================================================='

                Me.rdb_OptionsCalcul.Text = Bloc("CALCULOPTIONS")

                '=== PARAMETRES CALCUL ==============================================================='

                Me.rdb_Parametres.Text = Bloc("CALCULPARAM")

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues")
                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally

            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If
    End Sub

    Private Sub AffichageOptionsEnCours()

        Me.pan_Contenu.Controls.Clear()

        Select Case AffichageOptFeu
            Case Enu_AffichageOptions.Poutre
                Me.pan_Contenu.Controls.Add(Frm_OptionsFeuN_Poutre.pan_General)
                Frm_OptionsFeuN_Poutre.InitialiseFenetre(Bloc, BeamLoc)
            Case Enu_AffichageOptions.Calcul
                Me.pan_Contenu.Controls.Add(Frm_OptionsFeuN_Calcul.pan_General)
                Frm_OptionsFeuN_Calcul.InitialiseFenetre(Bloc)
            Case Enu_AffichageOptions.Parametres
                Me.pan_Contenu.Controls.Add(Frm_OptionsFeuN_Parametres.pan_General)
                Frm_OptionsFeuN_Parametres.InitialiseFenetre(Bloc)
        End Select


    End Sub

#End Region

#Region " Evenements "
    Private Sub rdb_Poutre_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Poutre.CheckedChanged,
           rdb_OptionsCalcul.CheckedChanged, rdb_Parametres.CheckedChanged

        If lBuild Then Exit Sub

        Select Case True
            Case Me.rdb_Poutre.Checked
                AffichageOptFeu = Enu_AffichageOptions.Poutre
            Case Me.rdb_Parametres.Checked
                AffichageOptFeu = Enu_AffichageOptions.Parametres
            Case Me.rdb_OptionsCalcul.Checked
                AffichageOptFeu = Enu_AffichageOptions.Calcul
        End Select

        AffichageOptionsEnCours()

    End Sub

#End Region

#Region "===FERMETURE==="

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Dim lModif As Boolean = False

        If ValideSaisieFenetre() Then

            TransfertSaisie(lModif)

            If lModif Then
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If
            Me.Close()
        End If
    End Sub

    Private Function ValideSaisieFenetre() As Boolean
        'lFrm_Valide = True

        'Dim ValeurUI As Decimal

        'For Each txtbox_loc As TextBox In list_txtbox
        '    If txtbox_loc.Name = txt_ThermalConductivity.Name And (txtbox_loc.ReadOnly Or Not txtbox_loc.Enabled Or Not txtbox_loc.Visible) Then
        '        ErrorProvider_Frm_OptionsFeu.SetError(txtbox_loc, String.Empty)
        '        Continue For
        '    End If
        '    VerificationSaisie(txtbox_loc, ValeurUI)
        '    If Not ErrorProvider_Frm_OptionsFeu.GetError(txtbox_loc) = String.Empty Then
        '        lFrm_Valide = False
        '        Exit For
        '    End If
        'Next
        'Return lFrm_Valide
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)
        lModif = False

        With MyProjet.Poutres(MyProjet.IndEnCours).ParamFeu

            '--> Partie paramètres de la poutre

            GereTransfertValeur(BeamLoc.ParamFeu.lCalculFeu, .lCalculFeu, lModif)

            If BeamLoc.ParamFeu.TypeSurface <> .TypeSurface Then
                lModif = True
                .TypeSurface = BeamLoc.ParamFeu.TypeSurface
            End If

            If BeamLoc.ParamFeu.Protection <> .Protection Then
                lModif = True
                .Protection = BeamLoc.ParamFeu.Protection
            End If

            If BeamLoc.ParamFeu.Protection = cls_OptionsFeu.enu_TypeProtection.IntumescentPaint Then
                GereTransfertValeur(BeamLoc.ParamFeu.CustomLambdaP, .CustomLambdaP, lModif)
            End If
            GereTransfertValeur(BeamLoc.ParamFeu.EpProtection, .EpProtection, lModif)
            GereTransfertValeur(BeamLoc.ParamFeu.lArmaFormeeAFroid, .lArmaFormeeAFroid, lModif)
            GereTransfertValeur(BeamLoc.ParamFeu.lCreuxProteges, .lCreuxProteges, lModif)

            '--> Options de calcul

            GereTransfertValeur(BeamLoc.ParamFeu.lCongesEnrobe, .lCongesEnrobe, lModif)
            GereTransfertValeur(BeamLoc.ParamFeu.lArmaCompression, .lArmaCompression, lModif)
            GereTransfertValeur(BeamLoc.ParamFeu.lDalleFEM, .lDalleFEM, lModif)
            If .lDalleFEM Then
                GereTransfertValeur(BeamLoc.ParamFeu.tDalleEFmax, .tDalleEFmax, lModif)
            End If
            GereTransfertValeur(BeamLoc.ParamFeu.lReductionConcreteStrength, .lReductionConcreteStrength, lModif)
            If BeamLoc.ParamFeu.MethodTempArma <> .MethodTempArma Then
                lModif = True
                .MethodTempArma = BeamLoc.ParamFeu.MethodTempArma
            End If

            '--> Partie paramètres de calcul

            GereTransfertValeur(BeamLoc.ParamFeu.DeltaTCalcul, .DeltaTCalcul, lModif)
            GereTransfertValeur(BeamLoc.ParamFeu.TempRef, .TempRef, lModif)
            GereTransfertValeur(BeamLoc.ParamFeu.PhiViewFactor, .PhiViewFactor, lModif)
            'GereTransfertValeur(myBeamLoc.ParamFeu.EmissivitySteel, .EmissivitySteel, lModif)
            GereTransfertValeur(BeamLoc.ParamFeu.EmissivityFire, .EmissivityFire, lModif)
            GereTransfertValeur(BeamLoc.ParamFeu.EmissivityC, .EmissivityC, lModif)
            GereTransfertValeur(BeamLoc.ParamFeu.ConvectionCoef, .ConvectionCoef, lModif)
            GereTransfertValeur(BeamLoc.ParamFeu.ksh, .ksh, lModif)

            '--> Options calcul FEM

            GereTransfertValeur(BeamLoc.ParamFeu.lANFrance, .lANFrance, lModif)
            GereTransfertValeur(BeamLoc.ParamFeu.lRhoCvar, .lRhoCvar, lModif)
            GereTransfertValeur(BeamLoc.ParamFeu.TeneurU, .TeneurU, lModif)

        End With

    End Sub

    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        'ErrorProvider_OptionsFeu.Clear()
    End Sub

#End Region

End Class