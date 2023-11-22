Imports PMXMoteur2
Imports System.IO

Public Class Frm_OptionsCalculPoutre


#Region " Variables "

    Dim lBuild As Boolean
    Dim MyParam As cls_OptionsCalcul
    ' Dim ArmaYoung As Decimal

    Dim tabNorme(1) As String

    Const kUnitEpsilon As Decimal = 10 ^ -6

#End Region


#Region "===OUVERTURE==="


    Private Sub Frm_OptionsCalculPoutre_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionLangues()
        InitialisationsFenetre()
        GestionStyle()
        GestionUnites()
        AfficherPoutreEnCours()

        lBuild = False
    End Sub

    Private Sub InitialisationsFenetre()
        MyParam = MyProjet.Poutres(MyProjet.IndEnCours).Param.Clone
        RemplirComboStandard()
        RemplirComboRH()
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_BEAMOPTIONS")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")
                Me.btn_Annuler.Text = Bloc("CANCEL")
                Me.btn_OK.Text = Bloc("OK")

                Me.lbl_CadreNorm.Text = Bloc("TSTANDARD")
                Me.lbl_Norme.Text = Bloc("TSTANDARD")
                tabNorme(0) = Bloc("ENGEN1")
                tabNorme(1) = Bloc("ENGEN2")

                Me.lbl_CadreELU.Text = Bloc("TELUOPTIONS")
                Me.rdb_NormalDesign.Text = Bloc("NORMALDESIGN")
                Me.rdb_ElasticDesign.Text = Bloc("ELASTICDESIGN")

                Me.lbl_CadreELS.Text = Bloc("TELSOPTIONS")

                Me.lbl_CadreBeton.Text = Bloc("TCONCRETE")
                Me.lbl_BetonMessage.Text = Bloc("CONCRETEMSG")
                Me.lbl_RH.Text = Bloc("RELATIVEHUMIDITY")
                Me.lbl_Shrinkage.Text = Bloc("SHRINKAGEDEFORMATION")
                Me.chk_RetraitEnrobage.Text = Bloc("SHRINKAGETOENCASEMENT")
                Me.lbl_ArmaYoung.Text = Bloc("YOUNGSMODULUSREBAR")

                Me.lbl_CadreSections.Text = Bloc("SECTIONSPROP")
                Me.chk_ArmaComprimees.Text = Bloc("REBARSINCOMPRESSION")
                Me.chk_LargeursPartipantesSimples.Text = Bloc("SIMPLIFIEDBEFF")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub RemplirComboStandard()
        Me.cmb_Norme.Items.Clear()
        Me.cmb_Norme.Items.Add(Me.tabNorme(0))

        If LogicielOptions.lExpert Then
            Me.cmb_Norme.Items.Add(Me.tabNorme(1))
        End If

    End Sub

    Private Sub RemplirComboRH()

        Me.cmb_RH.Items.Clear()

        For i = 0 To cls_OptionsCalcul.tabRH.GetUpperBound(0)
            Me.cmb_RH.Items.Add(GetStringInUnit(cls_OptionsCalcul.tabRH(i), Enu_TypeVariable.SansType, 2, 0, False) & "%")
        Next

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_CadreELU.BackColor = CouleurBackBandeaux
        Me.lbl_CadreELU.ForeColor = CouleurForeBandeaux

        Me.lbl_CadreELS.BackColor = CouleurBackBandeaux
        Me.lbl_CadreELS.ForeColor = CouleurForeBandeaux

        Me.lbl_CadreNorm.BackColor = CouleurBackBandeaux
        Me.lbl_CadreNorm.ForeColor = CouleurForeBandeaux

        Me.lbl_CadreBeton.BackColor = CouleurBackBandeaux
        Me.lbl_CadreBeton.ForeColor = CouleurForeBandeaux

        Me.lbl_CadreSections.BackColor = CouleurBackBandeaux
        Me.lbl_CadreSections.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub AfficherPoutreEnCours()

        '==> Norme

        Select Case MyParam.Norme
            Case cls_OptionsCalcul.Enu_Normes.EurocodesG1
                Me.cmb_Norme.SelectedIndex = 0
            Case cls_OptionsCalcul.Enu_Normes.EurocodesG2
                Me.cmb_Norme.SelectedIndex = Math.Min(1, Me.cmb_Norme.Items.Count - 1)
        End Select

        '==> Options ELU

        If MyParam.lElasticDesign Then
            Me.rdb_ElasticDesign.Checked = True
        Else
            Me.rdb_NormalDesign.Checked = True
        End If

        '==> Options ELS

        '==> Béton

        Me.cmb_RH.SelectedIndex = Array.IndexOf(cls_OptionsCalcul.tabRH, MyParam.RH)
        Me.txt_EpsilonSh.Text = GetStringInUnit(MyParam.EpsilonSH / kUnitEpsilon, Enu_TypeVariable.SansType, 3, 0, False)
        Me.chk_RetraitEnrobage.Checked = MyParam.lRetraitEnrobage
        Me.txt_Es.Text = GetStringInUnit(MyParam.ArmaYoung, Enu_TypeVariable.ModuleY, 3, 1, False)

        '==> Propriétés sections

        Me.chk_LargeursPartipantesSimples.Checked = MyParam.lLargeurEfficaceSimplifiee

    End Sub

#End Region

#Region "===FERMETURE==="

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

        GereTransfertValeur(MyParam.lElasticDesign, MyProjet.Poutres(MyProjet.IndEnCours).Param.lElasticDesign, lModif)
        GereTransfertValeur(MyParam.RH, MyProjet.Poutres(MyProjet.IndEnCours).Param.RH, lModif)
        GereTransfertValeur(MyParam.EpsilonSH, MyProjet.Poutres(MyProjet.IndEnCours).Param.EpsilonSH, lModif)
        GereTransfertValeur(MyParam.ArmaYoung, MyProjet.Poutres(MyProjet.IndEnCours).Param.ArmaYoung, lModif)
        GereTransfertValeur(MyParam.lRetraitEnrobage, MyProjet.Poutres(MyProjet.IndEnCours).Param.lRetraitEnrobage, lModif)

    End Sub

    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        Me.Close()
    End Sub

#End Region

#Region " Evènements "


    Private Sub ULSSectionDesign_CheckedChanged_1(sender As Object, e As EventArgs) Handles rdb_NormalDesign.CheckedChanged, rdb_ElasticDesign.CheckedChanged

        MyParam.lElasticDesign = Me.rdb_ElasticDesign.Checked

    End Sub

    Private Sub cmb_RH_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_RH.SelectedIndexChanged
        MyParam.RH = cls_OptionsCalcul.tabRH(Me.cmb_RH.SelectedIndex)
    End Sub

    Private Sub chk_RetraitEnrobage_CheckedChanged(sender As Object, e As EventArgs) Handles chk_RetraitEnrobage.CheckedChanged
        MyParam.lRetraitEnrobage = Me.chk_RetraitEnrobage.Checked
    End Sub



    Private Sub txt_EpsilonSh_TextChanged(sender As Object, e As EventArgs) Handles txt_EpsilonSh.TextChanged, txt_Es.TextChanged
        If lBuild Then Exit Sub

        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name
                Case Me.txt_EpsilonSh.Name
                    MyParam.EpsilonSH = Valeur
                Case Me.txt_Es.Name
                    MyParam.ArmaYoung = Valeur
            End Select
        End If
    End Sub


    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        '-- Déclaration - Initialisation

        Dim lOk As Boolean = True
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Select Case MyTxt.Name

            Case Me.txt_EpsilonSh.Name
                ValMin = 0
                lValMax = False
                kUnit = kUnitEpsilon
            Case Me.txt_Es.Name
                kUnit = LogicielInfo.Transfert_ModulesY(LogicielOptions.IndUnitModulesY)
                ValMin = 190000 / kUnit
                ValMax = 210000 / kUnit
                lValMax = True

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

#End Region

#Region " Dessin des symboles "

    Private Sub DrawSymbols(sender As Object, e As PaintEventArgs) Handles img_RH.Paint, img_EpsilonSh.Paint, img_Es.Paint
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

            Case Me.img_Es.Name
                strSymbol = "E"
                strIndice = "s"

            Case Me.img_EpsilonSh.Name
                strSymbol = "e"
                strIndice = "sh"
                lGrec = True

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub



#End Region

End Class