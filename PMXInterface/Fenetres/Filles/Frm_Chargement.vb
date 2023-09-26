Imports PMXMoteur2
Imports System.IO

Public Class Frm_Chargement

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Const formatTxtBox As String = "0.00"

    ''' <summary>
    ''' Définition d'une poutre_loc afin d'enregistrer les actions de l'utilisateur
    ''' </summary>
    Dim MyPoutreLoc As New cls_Poutre

    ''' <summary>
    ''' Définition d'une liste de string pour remplir le cmb_travee
    ''' </summary>
    Dim strTypeTravee() As String
    Dim strTypeTravee_ConsoleGauche As String
    Dim strTypeTravee_TraveeCentrale As String
    Dim strTypeTravee_ConsoleDroite As String
    Dim strSpan As String

    'Variables locales
    Dim NbTravees As Integer
    Dim NbChargeLineique, NbChargePonctuelle As Integer
    Const NbChargeLineiqueMAX As Integer = 4
    Const NbChargePonctuelleMAX As Integer = 8

    Dim traveeEnCours As Integer 'Donne l'indice de la travée en cours (POUR L'OBJET CLS_POUTRE)
    Dim iTraveeSelect As Integer = 1 ' indice qui informe du numéro de travée en cours (UNIQUEMENT POUR LE DESSIN)
    '----------------------------------------------
    '   1 pour la travée principale
    '   -1 si rien de selectionné
    '   0 console gauche
    '   99 console droite
    '----------------------------------------------
    Dim iChargeRepartieSelect As Integer = -1 ' indice qui informe de l'indice de la charge répartie en cours (UNIQUEMENT POUR LE DESSIN)
    Dim iChargePonctuelleSelect As Integer = -1 ' indice qui informe de l'indice de la charge répartie en cours (UNIQUEMENT POUR LE DESSIN)

    Dim chargeEnCours As String
    Dim Old_SelectedIndex_cmbTravee As Integer

    Dim WarningMessage_CmbTravee As String ' Message d'avertissement à afficher en cas de changement du cmb_travee alors qu'il y'a des erreurs à corriger

    Dim forceSurfaciqueMIN As Decimal
    Dim forceSurfaciqueMAX As Decimal
    Dim largeurSurfaciqueMIN As Decimal
    Dim largeurSurfaciqueMAX As Decimal

    Dim forcePonctuelleMIN As Decimal
    Dim forcePonctuelleMAX As Decimal
    Dim positionPonctuelleMIN As Decimal
    Dim positionPonctuelleMAX As Decimal

    Dim forceRepartieMIN As Decimal
    Dim forceRepartieMAX As Decimal
    Dim positionRepartieMIN As Decimal
    Dim positionRepartieMAX As Decimal

    Dim tableau_txtbox_ChargesLineiques(,) As TextBox
    Dim tableau_txtbox_ChargesPonctuelles(,) As TextBox

#End Region

#Region "===OUVERTURE==="


    Private Sub Frm_Chargement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        lBuild = True
        InitialiserVariables()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        RemplirCombobox()
        PrepareFlechesNavigation()
        MAJI_BtnNavigation()
        AfficherPoutreEnCours()
        MAJIAffichageChargeSurfacique()
        MAJIAffichageTableauxLineique()
        MAJIAffichageButtonsLineiques()
        MAJIAffichageTableauxPonctuel()
        MAJIAffichageButtonsPonctuels()
        lBuild = False
    End Sub

    Private Sub InitialiserVariables()
        MyPoutreLoc = New cls_Poutre()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)
        MyPoutreLoc.InitialisePoidsPropres()

        NbTravees = MyPoutreLoc.NbTravees

        For Each element As KeyValuePair(Of String, cls_ChargementUtilisateur) In MyPoutreLoc.ChargesU
            ReDim Preserve element.Value.WSurf(MyPoutreLoc.IndiceDerniereTravee)
            ReDim Preserve element.Value.QSurf(MyPoutreLoc.IndiceDerniereTravee)
            ReDim Preserve element.Value.Forces(MyPoutreLoc.IndiceDerniereTravee)
            ReDim Preserve element.Value.FReparties(MyPoutreLoc.IndiceDerniereTravee)

            For i As Integer = 0 To MyPoutreLoc.IndiceDerniereTravee
                If element.Value.Forces(i) Is Nothing Then element.Value.Forces(i) = New List(Of cls_Force)
                If element.Value.FReparties(i) Is Nothing Then element.Value.FReparties(i) = New List(Of cls_ForceRepartie)
            Next
        Next

        'Par défaut on affiche la première travée sur deux appuis
        traveeEnCours = 1

        'Par défaut on démarre sur G1
        chargeEnCours = "G1"
        rad_G1.Checked = True

        NbChargeLineique = MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours).Count
        NbChargePonctuelle = MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours).Count

        '-----

        forceSurfaciqueMIN = 0 / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur) ^ 2)
        forceSurfaciqueMAX = 10 ^ 9 / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur) ^ 2) '10^6 kN/m2

        largeurSurfaciqueMIN = 0 / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
        largeurSurfaciqueMAX = (MyPoutreLoc.EntraxeD1 + MyPoutreLoc.EntraxeD2) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

        forceRepartieMIN = 0 / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur))
        forceRepartieMAX = 10 ^ 9 / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)) '10^6 kN/m

        positionRepartieMIN = 0 / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
        positionRepartieMAX = MyPoutreLoc.LongueurTravee(traveeEnCours) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

        forcePonctuelleMIN = 0 / LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort)
        forcePonctuelleMAX = 10 ^ 9 / LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) '10^6 kN

        positionPonctuelleMIN = 0 / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
        positionPonctuelleMAX = MyPoutreLoc.LongueurTravee(traveeEnCours) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

        '-----

        ReDim tableau_txtbox_ChargesLineiques(NbChargeLineiqueMAX - 1, 5)

        tableau_txtbox_ChargesLineiques(0, 0) = Me.txt_Indice_Lineique_1
        tableau_txtbox_ChargesLineiques(0, 1) = Me.txt_x1_Lineique_1
        tableau_txtbox_ChargesLineiques(0, 2) = Me.txt_F1_Lineique_1
        tableau_txtbox_ChargesLineiques(0, 3) = Me.txt_x2_Lineique_1
        tableau_txtbox_ChargesLineiques(0, 4) = Me.txt_F2_Lineique_1

        tableau_txtbox_ChargesLineiques(1, 0) = Me.txt_Indice_Lineique_2
        tableau_txtbox_ChargesLineiques(1, 1) = Me.txt_x1_Lineique_2
        tableau_txtbox_ChargesLineiques(1, 2) = Me.txt_F1_Lineique_2
        tableau_txtbox_ChargesLineiques(1, 3) = Me.txt_x2_Lineique_2
        tableau_txtbox_ChargesLineiques(1, 4) = Me.txt_F2_Lineique_2

        tableau_txtbox_ChargesLineiques(2, 0) = Me.txt_Indice_Lineique_3
        tableau_txtbox_ChargesLineiques(2, 1) = Me.txt_x1_Lineique_3
        tableau_txtbox_ChargesLineiques(2, 2) = Me.txt_F1_Lineique_3
        tableau_txtbox_ChargesLineiques(2, 3) = Me.txt_x2_Lineique_3
        tableau_txtbox_ChargesLineiques(2, 4) = Me.txt_F2_Lineique_3

        tableau_txtbox_ChargesLineiques(3, 0) = Me.txt_Indice_Lineique_4
        tableau_txtbox_ChargesLineiques(3, 1) = Me.txt_x1_Lineique_4
        tableau_txtbox_ChargesLineiques(3, 2) = Me.txt_F1_Lineique_4
        tableau_txtbox_ChargesLineiques(3, 3) = Me.txt_x2_Lineique_4
        tableau_txtbox_ChargesLineiques(3, 4) = Me.txt_F2_Lineique_4

        '-----

        ReDim tableau_txtbox_ChargesPonctuelles(NbChargePonctuelleMAX - 1, 2)

        tableau_txtbox_ChargesPonctuelles(0, 0) = Me.txt_Indice_Ponctuelle_1
        tableau_txtbox_ChargesPonctuelles(0, 1) = Me.txt_x_Ponctuelle_1
        tableau_txtbox_ChargesPonctuelles(0, 2) = Me.txt_F_Ponctuelle_1

        tableau_txtbox_ChargesPonctuelles(1, 0) = Me.txt_Indice_Ponctuelle_2
        tableau_txtbox_ChargesPonctuelles(1, 1) = Me.txt_x_Ponctuelle_2
        tableau_txtbox_ChargesPonctuelles(1, 2) = Me.txt_F_Ponctuelle_2

        tableau_txtbox_ChargesPonctuelles(2, 0) = Me.txt_Indice_Ponctuelle_3
        tableau_txtbox_ChargesPonctuelles(2, 1) = Me.txt_x_Ponctuelle_3
        tableau_txtbox_ChargesPonctuelles(2, 2) = Me.txt_F_Ponctuelle_3

        tableau_txtbox_ChargesPonctuelles(3, 0) = Me.txt_Indice_Ponctuelle_4
        tableau_txtbox_ChargesPonctuelles(3, 1) = Me.txt_x_Ponctuelle_4
        tableau_txtbox_ChargesPonctuelles(3, 2) = Me.txt_F_Ponctuelle_4

        tableau_txtbox_ChargesPonctuelles(4, 0) = Me.txt_Indice_Ponctuelle_5
        tableau_txtbox_ChargesPonctuelles(4, 1) = Me.txt_x_Ponctuelle_5
        tableau_txtbox_ChargesPonctuelles(4, 2) = Me.txt_F_Ponctuelle_5

        tableau_txtbox_ChargesPonctuelles(5, 0) = Me.txt_Indice_Ponctuelle_6
        tableau_txtbox_ChargesPonctuelles(5, 1) = Me.txt_x_Ponctuelle_6
        tableau_txtbox_ChargesPonctuelles(5, 2) = Me.txt_F_Ponctuelle_6

        tableau_txtbox_ChargesPonctuelles(6, 0) = Me.txt_Indice_Ponctuelle_7
        tableau_txtbox_ChargesPonctuelles(6, 1) = Me.txt_x_Ponctuelle_7
        tableau_txtbox_ChargesPonctuelles(6, 2) = Me.txt_F_Ponctuelle_7

        tableau_txtbox_ChargesPonctuelles(7, 0) = Me.txt_Indice_Ponctuelle_8
        tableau_txtbox_ChargesPonctuelles(7, 1) = Me.txt_x_Ponctuelle_8
        tableau_txtbox_ChargesPonctuelles(7, 2) = Me.txt_F_Ponctuelle_8

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_CHARGEMENT")
            BlocLine.CreationBloc(Bloc)

            Try
                '=== MENU PRINCIPAL ==============================================================='

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== CHOIX DE LA CHARGE ==============================================================='

                Me.lbl_ChoixCharges.Text = Bloc("LOADCASE")
                Me.lbl_ChoixCharge.Text = Bloc("LOADCASE")

                Me.rad_G1.Text = "G1"
                Me.rad_G2.Text = "G2"
                Me.rad_Q1.Text = "Q1"
                Me.rad_Q2.Text = "Q2"
                Me.rad_Qc.Text = "QC"

                Me.lbl_ChoixTravee.Text = Bloc("SPAN")
                strTypeTravee_ConsoleGauche = Bloc("LEFTCANT")
                strTypeTravee_TraveeCentrale = Bloc("MAINSPAN")
                strTypeTravee_ConsoleDroite = Bloc("RIGHTCANT")
                strSpan = Bloc("SPAN")

                WarningMessage_CmbTravee = Bloc("WARNING_CMBTRAVEE")

                '=== FORCE SURFACIQUE ==============================================================='
                Me.lbl_ChargesSurfaciques.Text = Bloc("SURFACELOAD")
                Me.lbl_WidthApplication.Text = Bloc("WIDTHAPPLICATION")
                Me.lbl_UniformLoad.Text = Bloc("UNIFORMLOAD")
                Me.lbl_ResultingForce.Text = Bloc("RESULTINGFORCE")

                Me.lbl_UnitWidthApplication.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
                Me.lbl_UnitUniformLoad.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
                Me.lbl_UnitResultingForce.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)

                'Me.lbl_ResultingForce.Location = New Point(257, 38)

                '=== FORCE LINEIQUE ==============================================================='
                Me.lbl_ChargesLineiques.Text = Bloc("DISTRIBUTEDLOAD")
                Me.btn_AjouterLineique.Text = Bloc("ADD")
                Me.btn_SupprimerLineique.Text = Bloc("DELETE")
                Me.btn_InfoPP.Text = Bloc("INFORMATION")

                Me.txt_x1_Lineique.Text = "x (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
                Me.txt_F1_Lineique.Text = "F (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
                Me.txt_x2_Lineique.Text = "x (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
                Me.txt_F2_Lineique.Text = "F (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"

                '=== FORCE PONCTUELLE ==============================================================='
                Me.lbl_ChargesPonctuelles.Text = Bloc("CONCENTRATEDLOAD")
                Me.btn_AjouterPonctuelle.Text = Bloc("ADD")
                Me.btn_SupprimerPonctuelle.Text = Bloc("DELETE")

                Me.txt_x_Ponctuelle.Text = "x (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
                Me.txt_F_Ponctuelle.Text = "F (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & ")"

                '=== REACTIONS D'APPUIS ==============================================================='
                Me.lbl_ReactionsAppuis.Text = Bloc("FORCEENDSUPPORT")
                Me.lbl_LeftSupport.Text = Bloc("LEFTSUPPORT")
                Me.lbl_RightSupport.Text = Bloc("RIGHTSUPPORT")

                Me.lbl_UnitLeftSupport.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)
                Me.lbl_UnitRightSupport.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)



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

        Me.rad_Qc.Visible = MyPoutreLoc.lMixte

        Me.lbl_ChoixCharges.BackColor = CouleurBackBandeaux
        Me.lbl_ChoixCharges.ForeColor = CouleurForeBandeaux

        Me.lbl_ChargesSurfaciques.BackColor = CouleurBackBandeaux
        Me.lbl_ChargesSurfaciques.ForeColor = CouleurForeBandeaux

        Me.lbl_ChargesLineiques.BackColor = CouleurBackBandeaux
        Me.lbl_ChargesLineiques.ForeColor = CouleurForeBandeaux

        Me.lbl_ChargesPonctuelles.BackColor = CouleurBackBandeaux
        Me.lbl_ChargesPonctuelles.ForeColor = CouleurForeBandeaux

        Me.lbl_ReactionsAppuis.BackColor = CouleurBackBandeaux
        Me.lbl_ReactionsAppuis.ForeColor = CouleurForeBandeaux

        Me.img_Chargement.Dock = DockStyle.Fill
        'Me.img_Chargement.BorderStyle = BorderStyle.FixedSingle
    End Sub

    Private Sub RemplirCombobox()
        Dim index As Integer = 0

        ReDim strTypeTravee(NbTravees - 1)
        Dim lCentral As Boolean = (MyPoutreLoc.NombreTraveesDeuxAppuis = 1)
        If MyPoutreLoc.lTraveeConsoleGauche Then
            strTypeTravee(index) = strTypeTravee_ConsoleGauche
            index += 1
        End If
        For i As Integer = 1 To MyPoutreLoc.NombreTraveesDeuxAppuis
            If lCentral Then
                strTypeTravee(index) = strTypeTravee_TraveeCentrale
            Else
                strTypeTravee(index) = strSpan & " no " & CStr(i)
            End If
            index += 1
        Next
        If MyPoutreLoc.lTraveeConsoleDroite Then strTypeTravee(index) = strTypeTravee_ConsoleDroite

        Me.cmb_Travee.Items.Clear()
        Me.cmb_Travee.Items.AddRange(strTypeTravee)
        If MyPoutreLoc.lTraveeConsoleGauche Then
            Me.cmb_Travee.SelectedIndex = 1
        Else
            Me.cmb_Travee.SelectedIndex = 0
        End If

        Old_SelectedIndex_cmbTravee = Me.cmb_Travee.SelectedIndex


    End Sub

    Private Sub PrepareFlechesNavigation()
        Me.btn_Suivant.Visible = (NbTravees > 1)
        Me.btn_Precedent.Visible = (NbTravees > 1)
    End Sub

    Private Sub MAJI_BtnNavigation()
        If (NbTravees > 1) Then
            If Me.cmb_Travee.SelectedIndex = 0 Then
                Me.btn_Precedent.Image = imgList_Navigation.Images("PrecedentNonDispo")
            Else
                Me.btn_Precedent.Image = imgList_Navigation.Images("Precedent")
            End If
            If Me.cmb_Travee.SelectedIndex = NbTravees - 1 Then
                Me.btn_Suivant.Image = imgList_Navigation.Images("SuivantNonDispo")
            Else
                Me.btn_Suivant.Image = imgList_Navigation.Images("Suivant")
            End If
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
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If
            Me.ErrorProvider_Frm_Chargement.Clear()
            Me.Close()
        End If
    End Sub


    Private Function ValideSaisieFenetre() As Boolean
        Dim lFrmValide As Boolean = True

        Dim ValeurUI As Decimal

        VerificationSaisie(txt_WidthApplication, ValeurUI)
        If Not ErrorProvider_Frm_Chargement.GetError(txt_WidthApplication) = String.Empty Then
            lFrmValide = False
            Return lFrmValide
            Exit Function
        End If

        VerificationSaisie(txt_UniformLoad, ValeurUI)
        If Not ErrorProvider_Frm_Chargement.GetError(txt_UniformLoad) = String.Empty Then
            lFrmValide = False
            Return lFrmValide
            Exit Function
        End If

        For i As Integer = 0 To NbChargeLineique - 1
            For j As Integer = 1 To 4
                VerificationSaisie(tableau_txtbox_ChargesLineiques(i, j), ValeurUI)
                If Not ErrorProvider_Frm_Chargement.GetError(tableau_txtbox_ChargesLineiques(i, j)) = String.Empty Then
                    lFrmValide = False
                    Return lFrmValide
                    Exit Function
                End If
            Next
        Next

        For i As Integer = 0 To NbChargePonctuelle - 1
            For j As Integer = 1 To 2
                VerificationSaisie(tableau_txtbox_ChargesPonctuelles(i, j), ValeurUI)
                If Not ErrorProvider_Frm_Chargement.GetError(tableau_txtbox_ChargesPonctuelles(i, j)) = String.Empty Then
                    lFrmValide = False
                    Return lFrmValide
                    Exit Function
                End If
            Next
        Next

        Return lFrmValide


    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        With MyProjet.Poutres(MyProjet.IndEnCours)

            lModif = False

            For i As Integer = MyPoutreLoc.IndicePremiereTravee To MyPoutreLoc.IndiceDerniereTravee
                For Each chgtU As KeyValuePair(Of String, cls_ChargementUtilisateur) In MyPoutreLoc.ChargesU

                    GereTransfertValeur(chgtU.Value.QSurf(i), .ChargesU(chgtU.Key).QSurf(i), lModif)
                    GereTransfertValeur(chgtU.Value.WSurf(i), .ChargesU(chgtU.Key).WSurf(i), lModif)

                    If chgtU.Value.Forces(i).Count = .ChargesU(chgtU.Key).Forces(i).Count Then

                        For j = 0 To chgtU.Value.Forces(i).Count - 1

                            If Not chgtU.Value.Forces(i)(j).Equals(.ChargesU(chgtU.Key).Forces(i)(j)) Then

                                .ChargesU(chgtU.Key).Forces(i) = New List(Of cls_Force)
                                .ChargesU(chgtU.Key).Forces(i) = chgtU.Value.Forces(i)
                                lModif = True

                                Exit For
                            End If

                        Next

                    Else

                        .ChargesU(chgtU.Key).Forces(i) = New List(Of cls_Force)
                        .ChargesU(chgtU.Key).Forces(i) = chgtU.Value.Forces(i)
                        lModif = True

                    End If


                    If chgtU.Value.FReparties(i).Count = .ChargesU(chgtU.Key).FReparties(i).Count Then

                        For j = 0 To chgtU.Value.FReparties(i).Count - 1

                            If Not chgtU.Value.FReparties(i)(j).Equals(.ChargesU(chgtU.Key).FReparties(i)(j)) Then

                                .ChargesU(chgtU.Key).FReparties(i) = New List(Of cls_ForceRepartie)
                                .ChargesU(chgtU.Key).FReparties(i) = chgtU.Value.FReparties(i)
                                lModif = True

                                Exit For
                            End If

                        Next

                    Else

                        .ChargesU(chgtU.Key).FReparties(i) = New List(Of cls_ForceRepartie)
                        .ChargesU(chgtU.Key).FReparties(i) = chgtU.Value.FReparties(i)
                        lModif = True

                    End If



                Next
            Next


        End With

    End Sub

    Private Sub btn_Annuler_Click(sender As Object, e As EventArgs) Handles btn_Annuler.Click
        ErrorProvider_Frm_Chargement.Clear()
    End Sub




#End Region

#Region " Dessins "

    Private Sub DessinPoutre(sender As Object, e As PaintEventArgs) Handles img_Chargement.Paint

        DessinFrmChargement(e.Graphics, MyPoutreLoc, Me.img_Chargement.ClientRectangle.Width, Me.img_Chargement.ClientRectangle.Height, 1, iTraveeSelect, traveeEnCours, chargeEnCours, iChargePonctuelleSelect, iChargeRepartieSelect)

    End Sub

#End Region

#Region " Evènements "


#End Region

#Region " Evènements saisie "


    ''' <summary>
    ''' On enregistre le nom de la charge 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SelectionChargement(sender As Object, e As EventArgs) Handles rad_G1.CheckedChanged, rad_G2.CheckedChanged, rad_Q1.CheckedChanged, rad_Q2.CheckedChanged, rad_Qc.CheckedChanged
        If lBuild Or Not sender.checked Then Exit Sub

        Dim OldChargeEnCours As String = chargeEnCours



        If ValideSaisieFenetre() Then

            Select Case sender.name
                Case rad_G1.Name
                    chargeEnCours = "G1"
                Case rad_G2.Name
                    chargeEnCours = "G2"
                Case rad_Q1.Name
                    chargeEnCours = "Q1"
                Case rad_Q2.Name
                    chargeEnCours = "Q2"
                Case rad_Qc.Name
                    chargeEnCours = "QC"
            End Select

            NbChargeLineique = MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours).Count
            NbChargePonctuelle = MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours).Count

            MAJIAffichageChargeSurfacique()
            MAJIAffichageButtonsLineiques()
            MAJIAffichageTableauxLineique()
            MAJIAffichageButtonsPonctuels()
            MAJIAffichageTableauxPonctuel()

        Else

            Select Case sender.name
                Case rad_G1.Name
                    chargeEnCours = "G1"
                Case rad_G2.Name
                    chargeEnCours = "G2"
                Case rad_Q1.Name
                    chargeEnCours = "Q1"
                Case rad_Q2.Name
                    chargeEnCours = "Q2"
                Case rad_Qc.Name
                    chargeEnCours = "QC"
            End Select

            If Not OldChargeEnCours = chargeEnCours Then MsgBox(WarningMessage_CmbTravee)
            chargeEnCours = OldChargeEnCours

            lBuild = True

            Select Case chargeEnCours
                Case "G1"
                    rad_G1.Checked = True
                Case "G2"
                    rad_G2.Checked = True
                Case "Q1"
                    rad_Q1.Checked = True
                Case "Q2"
                    rad_Q2.Checked = True
                Case "QC"
                    rad_Qc.Checked = True
            End Select

            lBuild = False

        End If

                img_Chargement.Invalidate()

    End Sub

    Private Sub GestionNavigation(sender As Object, e As EventArgs) Handles btn_Suivant.Click, btn_Precedent.Click
        If lBuild Then Exit Sub

        If ValideSaisieFenetre() Then

            Dim Index As Integer = Me.cmb_Travee.SelectedIndex
            Select Case sender.name
                Case Me.btn_Precedent.Name
                    Me.cmb_Travee.SelectedIndex = Math.Max(0, Index - 1)
                Case Me.btn_Suivant.Name
                    Me.cmb_Travee.SelectedIndex = Math.Min(NbTravees - 1, Index + 1)
            End Select

            Select Case cmb_Travee.Text
                Case strTypeTravee_ConsoleGauche
                    traveeEnCours = 0
                    iTraveeSelect = 0
                Case strTypeTravee_TraveeCentrale
                    traveeEnCours = 1
                    iTraveeSelect = 1
                Case strTypeTravee_ConsoleDroite
                    traveeEnCours = MyPoutreLoc.IndiceTraveeConsoleDroite
                    iTraveeSelect = 99
            End Select

            NbChargeLineique = MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours).Count
            NbChargePonctuelle = MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours).Count

            positionRepartieMAX = MyPoutreLoc.LongueurTravee(traveeEnCours) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
            positionPonctuelleMAX = MyPoutreLoc.LongueurTravee(traveeEnCours) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

            MAJI_BtnNavigation()
            MAJIAffichageChargeSurfacique()
            MAJIAffichageButtonsLineiques()
            MAJIAffichageTableauxLineique()
            MAJIAffichageButtonsPonctuels()
            MAJIAffichageTableauxPonctuel()



            img_Chargement.Invalidate()

        Else
            MsgBox(WarningMessage_CmbTravee)
            cmb_Travee.SelectedIndex = Old_SelectedIndex_cmbTravee
        End If
    End Sub

    Private Sub btn_AjouterSupprimerLineique_Click(sender As Object, e As EventArgs) Handles btn_AjouterLineique.Click, btn_SupprimerLineique.Click
        If lBuild Then Exit Sub

        Select Case sender.name
            Case btn_AjouterLineique.Name
                MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours).Add(New cls_ForceRepartie(0, 10 ^ 3, MyPoutreLoc.LongueurTravee(traveeEnCours), 10 ^ 3, MyPoutreLoc.xPositionAppui(True, traveeEnCours)))
                NbChargeLineique = Math.Min(NbChargeLineique + 1, NbChargeLineiqueMAX)
            Case btn_SupprimerLineique.Name
                MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours).RemoveAt(NbChargeLineique - 1)
                NbChargeLineique = Math.Max(NbChargeLineique - 1, 0)
        End Select


        MAJIAffichageButtonsLineiques()
        MAJIAffichageTableauxLineique()

        img_Chargement.Invalidate()

    End Sub

    Private Sub btn_AjouterSupprimerPonctuel_Click(sender As Object, e As EventArgs) Handles btn_AjouterPonctuelle.Click, btn_SupprimerPonctuelle.Click
        If lBuild Then Exit Sub

        Select Case sender.name
            Case btn_AjouterPonctuelle.Name
                MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours).Add(New cls_Force(MyPoutreLoc.LongueurTravee(traveeEnCours) / 2, 10 ^ 3, MyPoutreLoc.xPositionAppui(True, traveeEnCours))) '1kN
                NbChargePonctuelle = Math.Min(NbChargePonctuelle + 1, NbChargePonctuelleMAX)
            Case btn_SupprimerPonctuelle.Name
                MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours).RemoveAt(NbChargePonctuelle - 1)
                NbChargePonctuelle = Math.Max(NbChargePonctuelle - 1, 0)
        End Select

        MAJIAffichageButtonsPonctuels()
        MAJIAffichageTableauxPonctuel()

        img_Chargement.Invalidate()

    End Sub

    Private Sub MAJIAffichageChargeSurfacique(Optional lMAJLargeur As Boolean = True, Optional lMAJPression As Boolean = True)

        'MAJ Affichage des valeurs dans la section charge surfacique 

        If lMAJLargeur Then txt_WidthApplication.Text = MyPoutreLoc.ChargesU(chargeEnCours).WSurf(traveeEnCours) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
        If lMAJPression Then txt_UniformLoad.Text = MyPoutreLoc.ChargesU(chargeEnCours).QSurf(traveeEnCours) / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur) ^ 2)
        txt_ResultingForce.Text = MyPoutreLoc.ChargesU(chargeEnCours).WSurf(traveeEnCours) * MyPoutreLoc.ChargesU(chargeEnCours).QSurf(traveeEnCours) / LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort)

    End Sub

    Private Sub MAJIAffichageTableauxLineique()

        'MAJ affichage des tableau 

        For i As Integer = 0 To NbChargeLineique - 1
            For j As Integer = 0 To 4
                tableau_txtbox_ChargesLineiques(i, j).Visible = True
                tableau_txtbox_ChargesLineiques(i, 0).Text = i + 1
                tableau_txtbox_ChargesLineiques(i, 1).Text = Format(MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).xPosT(0) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur), formatTxtBox)
                tableau_txtbox_ChargesLineiques(i, 2).Text = Format(MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).Force(0) / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)), formatTxtBox)
                tableau_txtbox_ChargesLineiques(i, 3).Text = Format(MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).xPosT(1) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur), formatTxtBox)
                tableau_txtbox_ChargesLineiques(i, 4).Text = Format(MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).Force(1) / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)), formatTxtBox)

                If chargeEnCours = "G1" And i = 0 Then
                    tableau_txtbox_ChargesLineiques(i, j).ReadOnly = True
                    tableau_txtbox_ChargesLineiques(i, j).BackColor = Color.LightGray
                Else
                    tableau_txtbox_ChargesLineiques(i, j).ReadOnly = False
                    tableau_txtbox_ChargesLineiques(i, j).BackColor = Color.White
                End If
            Next
        Next

        For i As Integer = NbChargeLineique To NbChargeLineiqueMAX - 1
            For j As Integer = 0 To 4
                tableau_txtbox_ChargesLineiques(i, j).Visible = False
                tableau_txtbox_ChargesLineiques(i, j).Text = ""
            Next
        Next

    End Sub

    Private Sub MAJIAffichageTableauxPonctuel()

        'MAJ affichage des tableau 

        For i As Integer = 0 To NbChargePonctuelle - 1
            For j As Integer = 0 To 2
                tableau_txtbox_ChargesPonctuelles(i, j).Visible = True
                tableau_txtbox_ChargesPonctuelles(i, 0).Text = i + 1
                tableau_txtbox_ChargesPonctuelles(i, 1).Text = Format(MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours)(i).xPosT / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur), formatTxtBox)
                tableau_txtbox_ChargesPonctuelles(i, 2).Text = Format(MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours)(i).Force / LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort), formatTxtBox)
            Next
        Next

        For i As Integer = NbChargePonctuelle To NbChargePonctuelleMAX - 1
            For j As Integer = 0 To 2
                tableau_txtbox_ChargesPonctuelles(i, j).Visible = False
                tableau_txtbox_ChargesPonctuelles(i, j).Text = ""
            Next
        Next

    End Sub

    Private Sub MAJIAffichageButtonsLineiques()

        'MAJ affichage des boutons ajouter, supprimer et information de la section charges linéiques

        btn_InfoPP.Enabled = chargeEnCours = "G1"
        btn_AjouterLineique.Enabled = Not (NbChargeLineique = NbChargeLineiqueMAX)
        If chargeEnCours = "G1" Then
            btn_SupprimerLineique.Enabled = Not (NbChargeLineique = 1)
        Else
            btn_SupprimerLineique.Enabled = Not (NbChargeLineique = 0)
        End If

    End Sub

    Private Sub cmb_Travee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Travee.SelectedIndexChanged
        If lBuild Then Exit Sub

        If ValideSaisieFenetre() Then

            Old_SelectedIndex_cmbTravee = cmb_Travee.SelectedIndex

            'permet de mettre à jour les variables locales qui tracent l'indice de la travée en cours 

            Select Case cmb_Travee.Text
                Case strTypeTravee_ConsoleGauche
                    traveeEnCours = 0
                    iTraveeSelect = 0
                Case strTypeTravee_TraveeCentrale
                    traveeEnCours = 1
                    iTraveeSelect = 1
                Case strTypeTravee_ConsoleDroite
                    traveeEnCours = MyPoutreLoc.IndiceTraveeConsoleDroite
                    iTraveeSelect = 99
            End Select

            NbChargeLineique = MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours).Count
            NbChargePonctuelle = MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours).Count

            positionRepartieMAX = MyPoutreLoc.LongueurTravee(traveeEnCours) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
            positionPonctuelleMAX = MyPoutreLoc.LongueurTravee(traveeEnCours) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)


            MAJI_BtnNavigation()

            MAJIAffichageButtonsLineiques()
            MAJIAffichageTableauxLineique()
            MAJIAffichageButtonsPonctuels()
            MAJIAffichageTableauxPonctuel()



            img_Chargement.Invalidate()

        Else

            If Not cmb_Travee.SelectedIndex = Old_SelectedIndex_cmbTravee Then MsgBox(WarningMessage_CmbTravee)
            cmb_Travee.SelectedIndex = Old_SelectedIndex_cmbTravee

        End If

    End Sub

    Private Sub MAJIAffichageButtonsPonctuels()

        'MAJ affichage des boutons ajouter et supprimer de la section charges ponctuelles

        btn_AjouterPonctuelle.Enabled = Not (NbChargePonctuelle = NbChargePonctuelleMAX)
        btn_SupprimerPonctuelle.Enabled = Not (NbChargePonctuelle = 0)

    End Sub

    Private Sub txt_txtbox_ChargementSurfacique_TextChanged(sender As Object, e As EventArgs) Handles txt_WidthApplication.TextChanged, txt_UniformLoad.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then
            Select Case sender.name
                Case txt_WidthApplication.Name
                    MyPoutreLoc.ChargesU(chargeEnCours).WSurf(traveeEnCours) = ValeurUI
                    MAJIAffichageChargeSurfacique(False, True)
                Case txt_UniformLoad.Name
                    MyPoutreLoc.ChargesU(chargeEnCours).QSurf(traveeEnCours) = ValeurUI
                    MAJIAffichageChargeSurfacique(True, False)
            End Select

        End If

        img_Chargement.Invalidate()
    End Sub

    Private Sub txt_txtbox_ChargementLineique_TextChanged(sender As Object, e As EventArgs) Handles txt_x2_Lineique_4.TextChanged, txt_x2_Lineique_3.TextChanged, txt_x2_Lineique_2.TextChanged, txt_x2_Lineique_1.TextChanged, txt_x1_Lineique_4.TextChanged, txt_x1_Lineique_3.TextChanged, txt_x1_Lineique_2.TextChanged, txt_x1_Lineique_1.TextChanged, txt_F2_Lineique_4.TextChanged, txt_F2_Lineique_3.TextChanged, txt_F2_Lineique_2.TextChanged, txt_F2_Lineique_1.TextChanged, txt_F1_Lineique_4.TextChanged, txt_F1_Lineique_3.TextChanged, txt_F1_Lineique_2.TextChanged, txt_F1_Lineique_1.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            For i As Integer = 0 To NbChargeLineique - 1
                Select Case sender.Name
                    Case tableau_txtbox_ChargesLineiques(i, 1).Name
                        MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).xPosT(0) = ValeurUI

                    Case tableau_txtbox_ChargesLineiques(i, 2).Name
                        MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).Force(0) = ValeurUI

                    Case tableau_txtbox_ChargesLineiques(i, 3).Name
                        MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).xPosT(1) = ValeurUI

                    Case tableau_txtbox_ChargesLineiques(i, 4).Name
                        MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).Force(1) = ValeurUI

                End Select
            Next


        End If

        img_Chargement.Invalidate()
    End Sub

    Private Sub txt_txtbox_ChargementPonctuelle_TextChanged(sender As Object, e As EventArgs) Handles txt_x_Ponctuelle_8.TextChanged, txt_x_Ponctuelle_7.TextChanged, txt_x_Ponctuelle_6.TextChanged, txt_x_Ponctuelle_5.TextChanged, txt_x_Ponctuelle_4.TextChanged, txt_x_Ponctuelle_3.TextChanged, txt_x_Ponctuelle_2.TextChanged, txt_x_Ponctuelle_1.TextChanged, txt_F_Ponctuelle_8.TextChanged, txt_F_Ponctuelle_7.TextChanged, txt_F_Ponctuelle_6.TextChanged, txt_F_Ponctuelle_5.TextChanged, txt_F_Ponctuelle_4.TextChanged, txt_F_Ponctuelle_3.TextChanged, txt_F_Ponctuelle_2.TextChanged, txt_F_Ponctuelle_1.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal



        If VerificationSaisie(sender, ValeurUI) Then

            For i As Integer = 0 To NbChargePonctuelle - 1
                Select Case sender.name
                    Case tableau_txtbox_ChargesPonctuelles(i, 1).Name
                        MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours)(i).xPosT = ValeurUI

                    Case tableau_txtbox_ChargesPonctuelles(i, 2).Name
                        MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours)(i).Force = ValeurUI

                End Select
            Next


        End If

        img_Chargement.Invalidate()
    End Sub

    ''' <summary>
    ''' Gère la sélection de la charge répartie en cours (POUR LE DESSIN)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub txt_txtbox_ChargementLineique_Enter(sender As Object, e As EventArgs) Handles txt_x2_Lineique_4.Enter, txt_x2_Lineique_3.Enter, txt_x2_Lineique_2.Enter, txt_x2_Lineique_1.Enter, txt_x1_Lineique_4.Enter, txt_x1_Lineique_3.Enter, txt_x1_Lineique_2.Enter, txt_x1_Lineique_1.Enter, txt_Indice_Lineique_4.Enter, txt_Indice_Lineique_3.Enter, txt_Indice_Lineique_2.Enter, txt_Indice_Lineique_1.Enter, txt_F2_Lineique_4.Enter, txt_F2_Lineique_3.Enter, txt_F2_Lineique_2.Enter, txt_F2_Lineique_1.Enter, txt_F1_Lineique_4.Enter, txt_F1_Lineique_3.Enter, txt_F1_Lineique_2.Enter, txt_F1_Lineique_1.Enter
        If lBuild Then Exit Sub
        iChargeRepartieSelect = -1

        For i As Integer = 0 To NbChargeLineique - 1
            For j As Integer = 0 To 4

                If sender.name = tableau_txtbox_ChargesLineiques(i, j).Name Then
                    iChargeRepartieSelect = i
                    For k As Integer = 0 To 4
                        tableau_txtbox_ChargesLineiques(i, k).BackColor = Color.LightBlue
                    Next
                    img_Chargement.Invalidate()
                    Exit Sub

                End If

            Next
        Next

        img_Chargement.Invalidate()

    End Sub

    ''' <summary>
    ''' Gère la sélection de la charge répartie en cours (POUR LE DESSIN)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub txt_txtbox_ChargementLineique_Leave(sender As Object, e As EventArgs) Handles txt_x2_Lineique_4.Leave, txt_x2_Lineique_3.Leave, txt_x2_Lineique_2.Leave, txt_x2_Lineique_1.Leave, txt_x1_Lineique_4.Leave, txt_x1_Lineique_3.Leave, txt_x1_Lineique_2.Leave, txt_x1_Lineique_1.Leave, txt_Indice_Lineique_4.Leave, txt_Indice_Lineique_3.Leave, txt_Indice_Lineique_2.Leave, txt_Indice_Lineique_1.Leave, txt_F2_Lineique_4.Leave, txt_F2_Lineique_3.Leave, txt_F2_Lineique_2.Leave, txt_F2_Lineique_1.Leave, txt_F1_Lineique_4.Leave, txt_F1_Lineique_3.Leave, txt_F1_Lineique_2.Leave, txt_F1_Lineique_1.Leave
        If lBuild Then Exit Sub

        If iChargeRepartieSelect = -1 Then
            Exit Sub
        Else
            For k As Integer = 0 To 4
                If chargeEnCours = "G1" And iChargeRepartieSelect = 0 Then
                    tableau_txtbox_ChargesLineiques(iChargeRepartieSelect, k).BackColor = Color.LightGray
                Else
                    tableau_txtbox_ChargesLineiques(iChargeRepartieSelect, k).BackColor = Color.White
                End If
            Next
            iChargeRepartieSelect = -1

        End If

        img_Chargement.Invalidate()

    End Sub

    ''' <summary>
    ''' Gère la sélection de la charge ponctuelle en cours (POUR LE DESSIN)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub txt_txtbox_ChargementPonctuelle_Enter(sender As Object, e As EventArgs) Handles txt_x_Ponctuelle_8.Enter, txt_x_Ponctuelle_7.Enter, txt_x_Ponctuelle_6.Enter, txt_x_Ponctuelle_5.Enter, txt_x_Ponctuelle_4.Enter, txt_x_Ponctuelle_3.Enter, txt_x_Ponctuelle_2.Enter, txt_x_Ponctuelle_1.Enter, txt_Indice_Ponctuelle_8.Enter, txt_Indice_Ponctuelle_7.Enter, txt_Indice_Ponctuelle_6.Enter, txt_Indice_Ponctuelle_5.Enter, txt_Indice_Ponctuelle_4.Enter, txt_Indice_Ponctuelle_3.Enter, txt_Indice_Ponctuelle_2.Enter, txt_Indice_Ponctuelle_1.Enter, txt_F_Ponctuelle_8.Enter, txt_F_Ponctuelle_7.Enter, txt_F_Ponctuelle_6.Enter, txt_F_Ponctuelle_5.Enter, txt_F_Ponctuelle_4.Enter, txt_F_Ponctuelle_3.Enter, txt_F_Ponctuelle_2.Enter, txt_F_Ponctuelle_1.Enter
        If lBuild Then Exit Sub
        iChargePonctuelleSelect = -1

        For i As Integer = 0 To NbChargePonctuelle - 1
            For j As Integer = 0 To 2

                If sender.name = tableau_txtbox_ChargesPonctuelles(i, j).Name Then
                    iChargePonctuelleSelect = i
                    For k As Integer = 0 To 2
                        tableau_txtbox_ChargesPonctuelles(i, k).BackColor = Color.LightBlue
                    Next
                    img_Chargement.Invalidate()
                    Exit Sub

                End If

            Next
        Next

        img_Chargement.Invalidate()

    End Sub

    ''' <summary>
    ''' Gère la sélection de la charge ponctuelle en cours (POUR LE DESSIN)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub txt_txtbox_ChargementPonctuelle_Leave(sender As Object, e As EventArgs) Handles txt_x_Ponctuelle_8.Leave, txt_x_Ponctuelle_7.Leave, txt_x_Ponctuelle_6.Leave, txt_x_Ponctuelle_5.Leave, txt_x_Ponctuelle_4.Leave, txt_x_Ponctuelle_3.Leave, txt_x_Ponctuelle_2.Leave, txt_x_Ponctuelle_1.Leave, txt_Indice_Ponctuelle_8.Leave, txt_Indice_Ponctuelle_7.Leave, txt_Indice_Ponctuelle_6.Leave, txt_Indice_Ponctuelle_5.Leave, txt_Indice_Ponctuelle_4.Leave, txt_Indice_Ponctuelle_3.Leave, txt_Indice_Ponctuelle_2.Leave, txt_Indice_Ponctuelle_1.Leave, txt_F_Ponctuelle_8.Leave, txt_F_Ponctuelle_7.Leave, txt_F_Ponctuelle_6.Leave, txt_F_Ponctuelle_5.Leave, txt_F_Ponctuelle_4.Leave, txt_F_Ponctuelle_3.Leave, txt_F_Ponctuelle_2.Leave, txt_F_Ponctuelle_1.Leave
        If lBuild Then Exit Sub

        If iChargePonctuelleSelect = -1 Then
            Exit Sub
        Else
            For k As Integer = 0 To 2
                tableau_txtbox_ChargesPonctuelles(iChargePonctuelleSelect, k).BackColor = Color.White
            Next
            iChargePonctuelleSelect = -1

        End If

        img_Chargement.Invalidate()

    End Sub

    Private Sub btn_InfoPP_Click(sender As Object, e As EventArgs) Handles btn_InfoPP.Click
        Frm_InformationPP.ShowDialog()
    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider_Frm_Chargement.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim kUnit As Decimal

        Select Case MyTxt.Name
            Case txt_WidthApplication.Name

                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

                ValMin = largeurSurfaciqueMIN / kUnit
                ValMax = largeurSurfaciqueMAX / kUnit

            Case txt_UniformLoad.Name

                kUnit = LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / (LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)) ^ 2

                ValMin = forceSurfaciqueMIN / kUnit
                ValMax = forceSurfaciqueMAX / kUnit

        End Select

        For i As Integer = 0 To NbChargeLineique - 1
            Select Case MyTxt.Name
                Case tableau_txtbox_ChargesLineiques(i, 1).Name

                    kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
                    ValMin = positionRepartieMIN / kUnit
                    ValMax = Math.Min(MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).xPosT(1) / kUnit, positionRepartieMAX / kUnit)

                Case tableau_txtbox_ChargesLineiques(i, 2).Name

                    kUnit = LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
                    ValMin = forceRepartieMIN / kUnit
                    ValMax = forceRepartieMAX / kUnit

                Case tableau_txtbox_ChargesLineiques(i, 3).Name

                    kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
                    ValMin = Math.Max(MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).xPosT(0) / kUnit, positionRepartieMIN / kUnit)
                    ValMax = positionRepartieMAX / kUnit

                Case tableau_txtbox_ChargesLineiques(i, 4).Name

                    kUnit = LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
                    ValMin = forceRepartieMIN / kUnit
                    ValMax = forceRepartieMAX / kUnit

            End Select
        Next

        For i As Integer = 0 To NbChargePonctuelle - 1
            Select Case MyTxt.Name
                Case tableau_txtbox_ChargesPonctuelles(i, 1).Name

                    kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
                    ValMin = positionPonctuelleMIN / kUnit
                    ValMax = positionPonctuelleMAX / kUnit

                Case tableau_txtbox_ChargesPonctuelles(i, 2).Name

                    kUnit = LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort)
                    ValMin = forcePonctuelleMIN / kUnit
                    ValMax = forcePonctuelleMAX / kUnit

            End Select
        Next


        iErreur = ValideSaisieNombre(MyTxt.Text, True, ValMin, True, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider_Frm_Chargement, ValMin, ValMax)
        Else

            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

#End Region

End Class