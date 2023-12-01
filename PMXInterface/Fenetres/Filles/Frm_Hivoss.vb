Imports PMXMoteur2
Imports System.IO

Public Class Frm_Hivoss

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim MyPoutreLoc As cls_Poutre

    Dim strRatioQ() As (String, Decimal)
    Dim strChoixQ() As (String, cls_OptionsHivoss.Enu_Q)
    Dim strUtilisationPlancher() As (String, cls_OptionsHivoss.Enu_UtilisationPlancher)
    Dim strAmortissementMobilier() As (String, cls_OptionsHivoss.Enu_Mobiliers)

    Dim strD2Value() As (String, Decimal)

    'Repère l'ordonnée de l'image ou du combobox actif indiquant D3 
    Dim y_cmb_img_actif As Decimal
    'Repère l'ordonnée de l'image ou du combobox passif indiquant D3 
    Dim y_cmb_img_passif As Decimal
    'Indique si c'est l'image qui est active (=True) ou non (=False)
    Dim l_img_actif As Boolean

    Const formatAMORTISSEMENT As String = "0"


#End Region

#Region "===OUVERTURE==="
    Private Sub Frm_Hivoss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub


    Public Sub InitialiserFenetre()
        InitialiserVariables()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        RemplirComboBox()
        AfficherPoutreEnCours()
        MAJI_img_cmb_D3()
        lBuild = False
    End Sub

    Private Sub InitialiserVariables()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)
        y_cmb_img_passif = 77
        y_cmb_img_actif = 104
        If MyPoutreLoc.Param.HivossParam.Mobilier = cls_OptionsHivoss.Enu_Mobiliers.Personnalise Then
            l_img_actif = False
        Else
            l_img_actif = True
        End If
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_HIVOSS")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== GENERAL ======================================================================

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== MENU PRINCIPAL ==============================================================='

                Me.lbl_Options.Text = Bloc("OPTIONS")
                Me.lbl_Amortissement.Text = Bloc("DAMPING")

                Me.chk_methodeHIVOSS.Text = Bloc("HIVOSSMETHOD")
                Me.lbl_ComboMasseFrequence.Text = Bloc("COMBOMASSFREQUENCY")

                ReDim strRatioQ(5)

                strRatioQ(0).Item1 = "G"
                strRatioQ(1).Item1 = "G + 0.1 Q"
                strRatioQ(2).Item1 = "G + 0.2 Q"
                strRatioQ(3).Item1 = "G + 0.3 Q"
                strRatioQ(4).Item1 = "G + 0.4 Q"
                strRatioQ(5).Item1 = "G + 0.5 Q"

                strRatioQ(0).Item2 = 0
                strRatioQ(1).Item2 = 0.1
                strRatioQ(2).Item2 = 0.2
                strRatioQ(3).Item2 = 0.3
                strRatioQ(4).Item2 = 0.4
                strRatioQ(5).Item2 = 0.5

                Me.lbl_avec.Text = Bloc("WITH")

                ReDim strChoixQ(1)

                strChoixQ(0).Item1 = "Q1"
                strChoixQ(1).Item1 = "Q2"

                strChoixQ(0).Item2 = cls_OptionsHivoss.Enu_Q.Q1
                strChoixQ(0).Item2 = cls_OptionsHivoss.Enu_Q.Q2

                Me.lbl_UtilisationPlancher.Text = Bloc("FLOORUSE")

                ReDim strUtilisationPlancher(9)

                strUtilisationPlancher(0).Item1 = Bloc("FLOORUSE_CRITAREA")
                strUtilisationPlancher(1).Item1 = Bloc("FLOORUSE_HOSP")
                strUtilisationPlancher(2).Item1 = Bloc("FLOORUSE_SCHOOL")
                strUtilisationPlancher(3).Item1 = Bloc("FLOORUSE_RESIDENTIAL")
                strUtilisationPlancher(4).Item1 = Bloc("FLOORUSE_OFFICE")
                strUtilisationPlancher(5).Item1 = Bloc("FLOORUSE_MEETING")
                strUtilisationPlancher(6).Item1 = Bloc("FLOORUSE_SENIOR")
                strUtilisationPlancher(7).Item1 = Bloc("FLOORUSE_HOTELS")
                strUtilisationPlancher(8).Item1 = Bloc("FLOORUSE_INDUSTRIAL")
                strUtilisationPlancher(9).Item1 = Bloc("FLOORUSE_SPORTS")

                strUtilisationPlancher(0).Item2 = cls_OptionsHivoss.Enu_UtilisationPlancher.ZoneSensible
                strUtilisationPlancher(1).Item2 = cls_OptionsHivoss.Enu_UtilisationPlancher.Sante
                strUtilisationPlancher(2).Item2 = cls_OptionsHivoss.Enu_UtilisationPlancher.Education
                strUtilisationPlancher(3).Item2 = cls_OptionsHivoss.Enu_UtilisationPlancher.Residentiel
                strUtilisationPlancher(4).Item2 = cls_OptionsHivoss.Enu_UtilisationPlancher.Bureau
                strUtilisationPlancher(5).Item2 = cls_OptionsHivoss.Enu_UtilisationPlancher.Reunion
                strUtilisationPlancher(6).Item2 = cls_OptionsHivoss.Enu_UtilisationPlancher.MaisonRetraite
                strUtilisationPlancher(7).Item2 = cls_OptionsHivoss.Enu_UtilisationPlancher.Hotel
                strUtilisationPlancher(8).Item2 = cls_OptionsHivoss.Enu_UtilisationPlancher.Industriel
                strUtilisationPlancher(9).Item2 = cls_OptionsHivoss.Enu_UtilisationPlancher.Sports

                Me.lbl_AmortissementStructure.Text = Bloc("STRUCTURALDAMPING")
                Me.lbl_PoutreAcier.Text = Bloc("STEELBEAM")
                Me.lbl_AmortissementMobilier.Text = Bloc("FURNITUREDAMPING")

                ReDim strAmortissementMobilier(7)

                strAmortissementMobilier(0).Item1 = Bloc("DAMPINGFURN_TRADOFFICE")
                strAmortissementMobilier(1).Item1 = Bloc("DAMPINGFURN_PAPEROFFICE")
                strAmortissementMobilier(2).Item1 = Bloc("DAMPINGFURN_OPENOFFICE")
                strAmortissementMobilier(3).Item1 = Bloc("DAMPINGFURN_LIBRARY")
                strAmortissementMobilier(4).Item1 = Bloc("DAMPINGFURN_HOUSE")
                strAmortissementMobilier(5).Item1 = Bloc("DAMPINGFURN_SCHOOLS")
                strAmortissementMobilier(6).Item1 = Bloc("DAMPINGFURN_GYM")
                strAmortissementMobilier(7).Item1 = Bloc("DAMPINGFURN_CUSTOM")

                strAmortissementMobilier(0).Item2 = cls_OptionsHivoss.Enu_Mobiliers.BureauAvecCloison
                strAmortissementMobilier(1).Item2 = cls_OptionsHivoss.Enu_Mobiliers.BureauSansArmoires
                strAmortissementMobilier(2).Item2 = cls_OptionsHivoss.Enu_Mobiliers.BureauPaysager
                strAmortissementMobilier(3).Item2 = cls_OptionsHivoss.Enu_Mobiliers.Bibliotheque
                strAmortissementMobilier(4).Item2 = cls_OptionsHivoss.Enu_Mobiliers.Residentiel
                strAmortissementMobilier(5).Item2 = cls_OptionsHivoss.Enu_Mobiliers.Ecole
                strAmortissementMobilier(6).Item2 = cls_OptionsHivoss.Enu_Mobiliers.SalleDeSport
                strAmortissementMobilier(7).Item2 = cls_OptionsHivoss.Enu_Mobiliers.Personnalise

                ReDim strD2Value(5)

                strD2Value(0).Item1 = "0 %"
                strD2Value(1).Item1 = "1 %"
                strD2Value(2).Item1 = "2 %"
                strD2Value(3).Item1 = "3 %"
                strD2Value(4).Item1 = "4 %"
                strD2Value(5).Item1 = "5 %"

                strD2Value(0).Item2 = 0.00
                strD2Value(1).Item2 = 0.01
                strD2Value(2).Item2 = 0.02
                strD2Value(3).Item2 = 0.03
                strD2Value(4).Item2 = 0.04
                strD2Value(5).Item2 = 0.05

                Me.lbl_AmortissementFinition.Text = Bloc("FINISHINGDAMPING")
                Me.chk_FauxPlafond.Text = Bloc("CEILINGFLOOR")
                Me.chk_ChappeFlottante.Text = Bloc("SWIMMINGSCREED")
                Me.lbl_AmortissementTotal.Text = Bloc("TOTALDAMPING")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub RemplirComboBox()
        Me.cmb_ratioQ.Items.Clear()
        For i As Integer = 0 To strRatioQ.Length - 1
            Me.cmb_ratioQ.Items.Add(strRatioQ(i).Item1)
        Next
        Me.cmb_ratioQ.SelectedIndex = 0

        Me.cmb_choixQ.Items.Clear()
        For i As Integer = 0 To strChoixQ.Length - 1
            Me.cmb_choixQ.Items.Add(strChoixQ(i).Item1)
        Next
        Me.cmb_choixQ.SelectedIndex = 0

        Me.cmb_UtilisationPlancher.Items.Clear()
        For i As Integer = 0 To strUtilisationPlancher.Length - 1
            Me.cmb_UtilisationPlancher.Items.Add(strUtilisationPlancher(i).Item1)
        Next
        Me.cmb_UtilisationPlancher.SelectedIndex = 0

        Me.cmb_AmortissementMobilier.Items.Clear()
        For i As Integer = 0 To strAmortissementMobilier.Length - 1
            Me.cmb_AmortissementMobilier.Items.Add(strAmortissementMobilier(i).Item1)
        Next
        Me.cmb_AmortissementMobilier.SelectedIndex = 0

        Me.cmb_D2Value.Items.Clear()
        For i As Integer = 0 To strD2Value.Length - 1
            Me.cmb_D2Value.Items.Add(strD2Value(i).Item1)
        Next
        Me.cmb_D2Value.SelectedIndex = 0

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_Options.BackColor = CouleurBackBandeaux
        Me.lbl_Options.ForeColor = CouleurForeBandeaux
        Me.lbl_Amortissement.BackColor = CouleurBackBandeaux
        Me.lbl_Amortissement.ForeColor = CouleurForeBandeaux

        Me.img_D1Value.BorderStyle = BorderStyle.FixedSingle
        Me.img_D2Value.BorderStyle = BorderStyle.FixedSingle
        Me.img_D3Value.BorderStyle = BorderStyle.FixedSingle
        Me.img_DtotValue.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub AfficherPoutreEnCours()

        Me.chk_methodeHIVOSS.Checked = MyPoutreLoc.Param.HivossParam.lHivossMethod

        For i As Integer = 0 To strRatioQ.Length - 1
            If MyPoutreLoc.Param.HivossParam.ratioQ = strRatioQ(i).Item2 Then
                cmb_ratioQ.SelectedItem = strRatioQ(i).Item1
                Exit For
            End If
        Next

        For i As Integer = 0 To strChoixQ.Length - 1
            If MyPoutreLoc.Param.HivossParam.choixQ = strChoixQ(i).Item2 Then
                cmb_choixQ.SelectedItem = strChoixQ(i).Item1
                Exit For
            End If
        Next


        For i As Integer = 0 To strUtilisationPlancher.Length - 1
            If MyPoutreLoc.Param.HivossParam.UtilisationPlancher = strUtilisationPlancher(i).Item2 Then
                cmb_UtilisationPlancher.SelectedItem = strUtilisationPlancher(i).Item1
                Exit For
            End If
        Next

        For i As Integer = 0 To strAmortissementMobilier.Length - 1
            If MyPoutreLoc.Param.HivossParam.Mobilier = strAmortissementMobilier(i).Item2 Then
                cmb_AmortissementMobilier.SelectedItem = strAmortissementMobilier(i).Item1
                Exit For
            End If
        Next

        MAJI_CoefficientsAmortissementD()

        chk_ChappeFlottante.Checked = MyPoutreLoc.Param.HivossParam.lChappeFlottante
        chk_FauxPlafond.Checked = MyPoutreLoc.Param.HivossParam.lFauxPlafond



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
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If
            Me.Close()
        End If
    End Sub


    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        With MyProjet.Poutres(MyProjet.IndEnCours).Param.HivossParam

            If .lHivossMethod <> MyPoutreLoc.Param.HivossParam.lHivossMethod Then
                lModif = True
                .lHivossMethod = MyPoutreLoc.Param.HivossParam.lHivossMethod
            End If

            If .ratioQ <> MyPoutreLoc.Param.HivossParam.ratioQ Then
                lModif = True
                .ratioQ = MyPoutreLoc.Param.HivossParam.ratioQ
            End If

            If .choixQ <> MyPoutreLoc.Param.HivossParam.choixQ Then
                lModif = True
                .choixQ = MyPoutreLoc.Param.HivossParam.choixQ
            End If

            If .UtilisationPlancher <> MyPoutreLoc.Param.HivossParam.UtilisationPlancher Then
                lModif = True
                .UtilisationPlancher = MyPoutreLoc.Param.HivossParam.UtilisationPlancher
            End If

            If .Mobilier <> MyPoutreLoc.Param.HivossParam.Mobilier Then
                lModif = True
                .Mobilier = MyPoutreLoc.Param.HivossParam.Mobilier
            End If

            If .lFauxPlafond <> MyPoutreLoc.Param.HivossParam.lFauxPlafond Then
                lModif = True
                .lFauxPlafond = MyPoutreLoc.Param.HivossParam.lFauxPlafond
            End If

            If .lChappeFlottante <> MyPoutreLoc.Param.HivossParam.lChappeFlottante Then
                lModif = True
                .lChappeFlottante = MyPoutreLoc.Param.HivossParam.lChappeFlottante
            End If

            If .AmortiStructure_D1 <> MyPoutreLoc.Param.HivossParam.AmortiStructure_D1 Then
                lModif = True
                .AmortiStructure_D1 = MyPoutreLoc.Param.HivossParam.AmortiStructure_D1
            End If

            If .AmortiMobilier_D2 <> MyPoutreLoc.Param.HivossParam.AmortiMobilier_D2 Then
                lModif = True
                .AmortiMobilier_D2 = MyPoutreLoc.Param.HivossParam.AmortiMobilier_D2
            End If

            If .AmortiFinition_D3 <> MyPoutreLoc.Param.HivossParam.AmortiFinition_D3 Then
                lModif = True
                .AmortiFinition_D3 = MyPoutreLoc.Param.HivossParam.AmortiFinition_D3
            End If

            If .AmortiTotal_Dtot <> MyPoutreLoc.Param.HivossParam.AmortiTotal_Dtot Then
                lModif = True
                .AmortiTotal_Dtot = MyPoutreLoc.Param.HivossParam.AmortiTotal_Dtot
            End If

        End With

    End Sub

#End Region

#Region " Dessins "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_D1Symbol.Paint, img_D1Value.Paint, img_D2Symbol.Paint, img_D2Value.Paint, img_D3Symbol.Paint, img_D3Value.Paint, img_DtotSymbol.Paint, img_DtotValue.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height
        Dim xStart As Single = sWI * 0.95

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lItalic, lEgal As Boolean
        Dim xPen As Single = xStart
        Dim hCar As Single = e.Graphics.MeasureString("X", FontSymbolNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yPen As Single = (sHI / 2 - hCar) / 2 + sHI * 0.15

        '--> Initialisation

        lItalic = False
        lGrec = False
        lEgal = True

        Select Case sender.name
            Case Me.img_D1Symbol.Name

                strSymbol = "D"
                strIndice = "1"

            Case Me.img_D2Symbol.Name

                strSymbol = "D"
                strIndice = "2"

            Case Me.img_D3Symbol.Name

                strSymbol = "D"
                strIndice = "3"

            Case Me.img_DtotSymbol.Name

                strSymbol = "D"
                strIndice = ""

            Case Me.img_D1Value.Name

                strSymbol = Format(MyPoutreLoc.Param.HivossParam.AmortiStructure_D1 * 100, formatAMORTISSEMENT) & " %"
                strIndice = " "

                lEgal = False

            Case Me.img_D2Value.Name

                strSymbol = Format(MyPoutreLoc.Param.HivossParam.AmortiMobilier_D2 * 100, formatAMORTISSEMENT) & " %"
                strIndice = ""

                lEgal = False

            Case Me.img_D3Value.Name

                strSymbol = Format(MyPoutreLoc.Param.HivossParam.AmortiFinition_D3 * 100, formatAMORTISSEMENT) & " %"
                strIndice = ""

                lEgal = False

            Case Me.img_DtotValue.Name

                strSymbol = CStr(Format(MyPoutreLoc.Param.HivossParam.AmortiTotal_Dtot * 100, formatAMORTISSEMENT) & " %")
                strIndice = ""

                lEgal = False

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lItalic, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Evènements "

    Sub MAJI_img_cmb_D3()
        If l_img_actif Then
            Me.img_D2Value.Location = New Point(Me.img_D3Value.Location.X, y_cmb_img_actif)
            Me.img_D2Value.Visible = l_img_actif

            Me.cmb_D2Value.Location = New Point(Me.img_D3Value.Location.X, y_cmb_img_passif)
            Me.cmb_D2Value.Visible = Not l_img_actif

            Me.img_D2Value.Invalidate()
        Else
            Me.img_D2Value.Location = New Point(Me.img_D3Value.Location.X, y_cmb_img_passif)
            Me.img_D2Value.Visible = l_img_actif

            Me.cmb_D2Value.Location = New Point(Me.img_D3Value.Location.X, y_cmb_img_actif)
            Me.cmb_D2Value.Visible = Not l_img_actif

            For i As Integer = 0 To strD2Value.Length - 1
                If strD2Value(i).Item2 = MyPoutreLoc.Param.HivossParam.AmortiMobilier_D2 Then
                    cmb_D2Value.SelectedIndex = i

                    Exit For
                End If
            Next

        End If
    End Sub

    Sub MAJI_CoefficientsAmortissementD()
        MyPoutreLoc.Param.HivossParam.CalculAmortissement()
        img_D1Value.Invalidate()
        img_D2Value.Invalidate()
        img_D3Value.Invalidate()
        img_DtotValue.Invalidate()

    End Sub

#End Region

#Region " Evènements saisie "

    Private Sub chk_methodeHIVOSS_CheckedChanged(sender As Object, e As EventArgs) Handles chk_methodeHIVOSS.CheckedChanged
        If lBuild Then Exit Sub
        MyPoutreLoc.Param.HivossParam.lHivossMethod = chk_methodeHIVOSS.Checked
    End Sub

    Private Sub cmb_ratioQ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ratioQ.SelectedIndexChanged
        If lBuild Then Exit Sub

        For i As Integer = 0 To strRatioQ.Length - 1
            If cmb_ratioQ.SelectedItem = strRatioQ(i).Item1 Then
                MyPoutreLoc.Param.HivossParam.ratioQ = strRatioQ(i).Item2
                Exit For
            End If
        Next
    End Sub

    Private Sub cmb_choixQ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_choixQ.SelectedIndexChanged
        If lBuild Then Exit Sub

        For i As Integer = 0 To strChoixQ.Length - 1
            If cmb_choixQ.SelectedItem = strChoixQ(i).Item1 Then
                MyPoutreLoc.Param.HivossParam.choixQ = strChoixQ(i).Item2
                Exit For
            End If
        Next
    End Sub

    Private Sub cmb_UtilisationPlancher_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_UtilisationPlancher.SelectedIndexChanged
        If lBuild Then Exit Sub

        For i As Integer = 0 To strUtilisationPlancher.Length - 1
            If cmb_UtilisationPlancher.SelectedItem = strUtilisationPlancher(i).Item1 Then
                MyPoutreLoc.Param.HivossParam.UtilisationPlancher = strUtilisationPlancher(i).Item2
                Exit For
            End If
        Next
    End Sub

    Private Sub cmb_AmortissementMobilier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_AmortissementMobilier.SelectedIndexChanged
        If lBuild Then Exit Sub

        For i As Integer = 0 To strAmortissementMobilier.Length - 1
            If cmb_AmortissementMobilier.SelectedItem = strAmortissementMobilier(i).Item1 Then
                MyPoutreLoc.Param.HivossParam.Mobilier = strAmortissementMobilier(i).Item2
                Exit For
            End If
        Next

        MAJI_CoefficientsAmortissementD()

        If MyPoutreLoc.Param.HivossParam.Mobilier = cls_OptionsHivoss.Enu_Mobiliers.Personnalise Then
            l_img_actif = False
            MAJI_img_cmb_D3()
        Else
            l_img_actif = True
            MAJI_img_cmb_D3()
        End If


    End Sub

    Private Sub chk_FauxPlafond_ChappeFlottante_CheckedChanged(sender As Object, e As EventArgs) Handles chk_FauxPlafond.CheckedChanged, chk_ChappeFlottante.CheckedChanged
        If lBuild Then Exit Sub

        MyPoutreLoc.Param.HivossParam.lFauxPlafond = chk_FauxPlafond.Checked
        MyPoutreLoc.Param.HivossParam.lChappeFlottante = chk_ChappeFlottante.Checked

        MAJI_CoefficientsAmortissementD()

    End Sub

    Private Sub cmb_D2Value_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_D2Value.SelectedIndexChanged
        If lBuild Then Exit Sub

        MyPoutreLoc.Param.HivossParam.AmortiMobilier_D2 = strD2Value(cmb_D2Value.SelectedIndex).Item2

        MAJI_CoefficientsAmortissementD()
    End Sub

#End Region

End Class