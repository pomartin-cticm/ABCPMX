Imports PMXMoteur2

Imports System.IO
Imports CTICM_DATA_DLLS

Public Class Frm_ChargementN

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Const formatTxtBox As String = "0.00"

    ''' <summary>
    ''' Définition d'une poutre_loc afin d'enregistrer les actions de l'utilisateur
    ''' </summary>
    Dim MyPoutreLoc As New cls_Poutre(NomChargements)

    ''' <summary>
    ''' Définition d'une liste de string pour remplir le cmb_travee
    ''' </summary>
    Dim strTypeTravee() As String
    Dim strTypeTravee_ConsoleGauche As String
    Dim strTypeTravee_TraveeCentrale As String
    Dim strTypeTravee_ConsoleDroite As String
    Dim strSpan As String

    'Définition de strings locaux pour le nom de la variable en cours 
    Dim strInfoG As String
    Dim strInfoG1 As String
    Dim strInfoG2 As String
    Dim strInfoQ1 As String
    Dim strInfoQ2 As String
    Dim strInfoQc As String

    'Variables locales
    Dim NbTravees As Integer
    Dim NbChargeLineique, NbChargePonctuelle As Integer
    Const NbChargeLineiqueMAX As Integer = 4
    Const NbChargePonctuelleMAX As Integer = 8

    Dim traveeEnCours As Integer        'Donne l'indice de la travée en cours (POUR L'OBJET CLS_POUTRE)
    Dim traveeMouse As Integer = -1     'Donne l'indice de la travée surlaquelle se situe la souris
    Dim iTraveeSelect As Integer = 1    ' indice qui informe du numéro de travée en cours (UNIQUEMENT POUR LE DESSIN)
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

    Dim DonneesEF As New CTICM_DATA_DLLS.DATA_DLLS
    Dim SigneM() As Decimal = Nothing


    Dim MyParAff As Struc_Affichage

    Const FORCECDEF As Decimal = 1000           ' Force concentrée par défaut

    Dim strPoutreMixte As String = "Poutre mixte"
    Dim strPoutreAcierEnrob As String = "Poutre acier partiellement enrobée"
    Dim strPoutreAcier As String = "Poutre acier"

    Dim strEtaiementSans As String = "Sans étaiement"
    Dim strEtaiementTotal As String = "Étaiement complet"
    Dim strEtaiementPoints As String = "Étaiements ponctuels"
    Dim strReference As String

    Dim lGeneration1 As Boolean = MyProjet.Poutres(MyProjet.IndEnCours).Param.lGeneration1

    Dim lSelectInfoPP As Boolean = False
    Dim lSelectInfoPsi2 As Boolean = False

    Dim strAutoPP, strCustomPP As String
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
        MAJIAffichageChargeEnCours()
        MAJIAffichageChargeSurfacique()
        MAJIAffichageTableauxLineique()
        MAJIAffichageButtonsLineiques()
        MAJIAffichageTableauxPonctuel()
        MAJIAffichageButtonsPonctuels()
        MAJIReactions()
        'MAJI_InfoCharges()
        lBuild = False
    End Sub

    Private Sub InitialiserVariables()
        MyPoutreLoc = New cls_Poutre(NomChargements)
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)
        'myBeamLoc.InitialisePoidsPropres()
        PreparerCalculEF(MyPoutreLoc)

        NbTravees = MyPoutreLoc.NbTravees

        For Each element As KeyValuePair(Of String, cls_ChargementUtilisateur) In MyPoutreLoc.ChargesU
            'ReDim Preserve element.Value.WSurf(myBeamLoc.IndiceDerniereTravee)
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

            Dim strLoadedKey As String = ""
            Dim CLE As String = ""

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_CHARGEMENT")
            BlocLine.CreationBloc(Bloc, strLoadedKey)

            Try
                '=== MENU PRINCIPAL ==============================================================='

                CLE = Bloc("TITLE") : Me.Text = CLE                     ' Bloc("TITLE")
                CLE = Bloc("OK") : Me.btn_OK.Text = CLE                 '  Bloc("OK")
                CLE = Bloc("CANCEL") : Me.btn_Annuler.Text = CLE        '  Bloc("CANCEL")

                '=== CHOIX DE LA CHARGE ==============================================================='

                CLE = Bloc("LOADCASE") : Me.lbl_ChoixCharges.Text = CLE    '  Bloc("LOADCASE")
                CLE = Bloc("LOADCASE") : Me.lbl_ChoixCharge.Text = CLE     '  Bloc("LOADCASE")

                Me.rad_G1.Text = "G1"
                Me.rad_G2.Text = "G2"
                Me.rad_Q1.Text = "Q1"
                Me.rad_Q2.Text = "Q2"
                Me.rad_Qc.Text = "QC"

                CLE = Bloc("SPAN") : Me.lbl_Span.Text = CLE                     '  Bloc("SPAN")
                CLE = Bloc("LEFTCANT") : strTypeTravee_ConsoleGauche = CLE      '  Bloc("LEFTCANT")
                CLE = Bloc("MAINSPAN") : strTypeTravee_TraveeCentrale = CLE     '  Bloc("MAINSPAN")
                CLE = Bloc("RIGHTCANT") : strTypeTravee_ConsoleDroite = CLE     '  Bloc("RIGHTCANT")
                CLE = Bloc("SPAN") : strSpan = CLE                              '  Bloc("SPAN")

                CLE = Bloc("WARNING_CMBTRAVEE") : WarningMessage_CmbTravee = CLE      ' Bloc("WARNING_CMBTRAVEE")

                CLE = Bloc("INFO_G") : strInfoG = CLE                           ' Bloc("INFO_G")
                CLE = Bloc("INFO_G1") : strInfoG1 = CLE                         ' Bloc("INFO_G1")
                CLE = Bloc("INFO_G2") : strInfoG2 = CLE                         ' Bloc("INFO_G2")
                CLE = Bloc("INFO_Q1") : strInfoQ1 = CLE                         ' Bloc("INFO_Q1")
                CLE = Bloc("INFO_Q2") : strInfoQ2 = CLE                         ' Bloc("INFO_Q2")
                CLE = Bloc("INFO_QC") : strInfoQc = CLE                         ' Bloc("INFO_QC")

                '=== FORCE SURFACIQUE ==============================================================='
                CLE = Bloc("SURFACELOAD") : Me.lbl_ChargesSurfaciques.Text = CLE        ' Bloc("SURFACELOAD")
                CLE = Bloc("WIDTHAPPLICATION") : Me.lbl_WidthApplication.Text = CLE     ' Bloc("WIDTHAPPLICATION")
                CLE = Bloc("UNIFORMLOAD") : Me.lbl_UniformLoad.Text = CLE               ' Bloc("UNIFORMLOAD")
                CLE = Bloc("RESULTINGFORCE") : Me.lbl_ResultingForce.Text = CLE         ' Bloc("RESULTINGFORCE")

                'Me.lbl_ResultingForce.Location = New Point(257, 38)

                '=== FORCE LINEIQUE ==============================================================='
                CLE = Bloc("DISTRIBUTEDLOAD") : Me.lbl_ChargesLineiques.Text = CLE      ' Bloc("DISTRIBUTEDLOAD")
                CLE = Bloc("ADD") : Me.btn_AjouterLineique.Text = CLE                   '  Bloc("ADD")
                CLE = Bloc("DELETE") : Me.btn_SupprimerLineique.Text = CLE              ' Bloc("DELETE")
                CLE = Bloc("INFORMATION") : Me.btn_InfoPP.Text = CLE                    ' Bloc("INFORMATION")

                '=== FORCE PONCTUELLE ==============================================================='
                CLE = Bloc("CONCENTRATEDLOAD") : Me.lbl_ChargesPonctuelles.Text = CLE   ' Bloc("CONCENTRATEDLOAD")
                CLE = Bloc("ADD") : Me.btn_AjouterPonctuelle.Text = CLE                 ' Bloc("ADD")
                CLE = Bloc("DELETE") : Me.btn_SupprimerPonctuelle.Text = CLE            ' Bloc("DELETE")

                '=== REACTIONS D'APPUIS ==============================================================='
                CLE = Bloc("FORCEENDSUPPORT") : Me.lbl_ReactionsAppuis.Text = CLE       ' Bloc("FORCEENDSUPPORT")
                CLE = Bloc("LEFTSUPPORT") : Me.lbl_LeftSupport.Text = CLE               ' Bloc("LEFTSUPPORT")
                CLE = Bloc("RIGHTSUPPORT") : Me.lbl_RightSupport.Text = CLE             ' Bloc("RIGHTSUPPORT")

                '=== Etat de la dalle

                CLE = Bloc("COMPOSITEBEAM") : strPoutreMixte = CLE              '  Bloc("COMPOSITEBEAM")
                CLE = Bloc("STEELBEAM") : strPoutreAcier = CLE                  '  Bloc("STEELBEAM")
                CLE = Bloc("STEELENCASEDBEAM") : strPoutreAcierEnrob = CLE      '  Bloc("STEELENCASEDBEAM")
                CLE = Bloc("MODULAR") : Me.lbl_NCoef.Text = CLE                 '  Bloc("MODULAR")
                CLE = Bloc("SLAB") : Me.lbl_Ndalle.Text = CLE                   '  Bloc("SLAB")
                CLE = Bloc("ENCASEMENT") : Me.lbl_Nenrob.Text = CLE             '  Bloc("ENCASEMENT")

                CLE = Bloc("NOPROP") : strEtaiementSans = CLE                   ' "Sans étaiement"
                CLE = Bloc("FULLPROP") : strEtaiementTotal = CLE                ' "Étaiement complet"
                CLE = Bloc("POINTPROP") : strEtaiementPoints = CLE              ' "Étaiements ponctuels"
                CLE = Bloc("REFERENCELTPSI2") : strReference = CLE

                '=== CT LT ==============================================================='

                If (LogicielInfo.ListeLangue(LogicielOptions.IndLangue) = FRANCAIS) Then
                    Me.lbl_CT.Text = "CT"
                    Me.lbl_LT.Text = "LT"
                Else
                    Me.lbl_CT.Text = "ST"
                    Me.lbl_LT.Text = "LT"
                End If

                CLE = Bloc("SHORTTERM") : Me.ToolTip1.SetToolTip(Me.lbl_CT, CLE)
                CLE = Bloc("LONGTERM") : Me.ToolTip1.SetToolTip(Me.lbl_LT, CLE)

                CLE = Bloc("INFO_PP_AUTO") : strAutoPP = CLE
                CLE = Bloc("INFO_PP_CUSTOM") : strCustomPP = CLE

            Catch ex As Exception

                GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)

            Finally
                Bloc.Clear()
            End Try
        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If

    End Sub

    Private Sub GestionUnites()

        Me.lbl_UnitWidthApplication.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.lbl_UnitUniformLoad.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & "²"
        Me.lbl_UnitResultingForce.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)

        Me.txt_x1_Lineique.Text = "x (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
        Me.txt_F1_Lineique.Text = "q (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
        Me.txt_x2_Lineique.Text = "x (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
        Me.txt_F2_Lineique.Text = "q (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"


        Me.txt_x_Ponctuelle.Text = "x (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
        Me.txt_F_Ponctuelle.Text = "F (" & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & ")"

        Me.lbl_UnitLeftSupport.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)
        Me.lbl_UnitRightSupport.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.rad_Qc.Visible = MyPoutreLoc.lMixte
        If MyPoutreLoc.lMixte Then
            Me.rad_G1.Text = "G1"
        Else
            Me.rad_G1.Text = "G"
        End If
        Me.rad_G2.Visible = MyPoutreLoc.lMixte


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

        Me.lbl_Span.BackColor = CouleurBackBandeaux
        Me.lbl_Span.ForeColor = CouleurForeBandeaux

        Me.img_Chargement.Dock = DockStyle.Fill
        'Me.img_Chargement.BorderStyle = BorderStyle.FixedSingle

        '==POM
        'Me.pan_ChoixTravee.Visible = (MyProjet.Poutres(MyProjet.IndEnCours).NbTravees > 1)

        If Not (MyProjet.Poutres(MyProjet.IndEnCours).NbTravees > 1) Then
            Me.TLpan_PanneauGauche.RowStyles(2).Height = 0
            Me.TLpan_PanneauGauche.RowStyles(3).Height = 0
        Else
            Me.TLpan_PanneauGauche.RowStyles(2).Height = 30
            Me.TLpan_PanneauGauche.RowStyles(3).Height = 56
        End If

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
                    'GereTransfertValeur(chgtU.Value.WSurf(i), .ChargesU(chgtU.Key).WSurf(i), lModif)

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

        DessinFrmChargementN(e.Graphics, MyPoutreLoc, Me.img_Chargement.ClientRectangle.Width, Me.img_Chargement.ClientRectangle.Height, 1, traveeEnCours, traveeMouse, chargeEnCours, MyParAff, iChargePonctuelleSelect, iChargeRepartieSelect)

    End Sub

#End Region

#Region " RDM "

    Private Sub PreparerCalculEF(MyPoutre As cls_Poutre)
        '--------------------------------------------------------------------------------------
        '   04/11/23 :  Création - POM
        '--------------------------------------------------------------------------------------

        '--> Déclarations

        Dim zANE, InertieY, MelRd, zANP, MplRd, Aire As Decimal

        '--> Noeuds

        MyPoutre.PrepareNodesN()

        '--> Préparation du modèle EF

        '# Maillage

        MyPoutre.Analyse = New cls_AnalyseEFinis(MyPoutre.Section.Acier.EYoung, MyPoutre.Param.GraviteG, MyPoutre.Nodes)

        '# Appuis

        MyPoutre.Analyse.Appuis(MyPoutre.Nodes, False)

        '# Propriétés des éléments

        MyPoutre.Section.ProfilA.ProprietesMyy(1, True, 1, zANE, InertieY, MelRd, zANP, MplRd)
        Aire = MyPoutre.Section.ProfilA.Aire

        MyPoutre.Analyse.AttribuerProprietesConstantes(InertieY, Aire)

    End Sub


    Private Sub MAJIReactions()

        Dim lOk As Boolean

        MyPoutreLoc.Analyse.TransfertChargementU(MyPoutreLoc.ChargesU(chargeEnCours), MyPoutreLoc.IndicePremiereTravee, MyPoutreLoc.IndiceDerniereTravee,
                                                 MyPoutreLoc.LongueurTravee, MyPoutreLoc.LargeurInfluence)

        MyPoutreLoc.Analyse.RunRDM(lOk)

        If lOk Then

            Me.txt_LeftSupport.Text = GetStringInUnit(MyPoutreLoc.Analyse.Reaction(0), Enu_TypeVariable.Effort, 3, 2, False)
            Me.txt_RightSupport.Text = GetStringInUnit(MyPoutreLoc.Analyse.Reaction(1), Enu_TypeVariable.Effort, 3, 2, False)

        Else

            Me.txt_LeftSupport.Text = "-"
            Me.txt_RightSupport.Text = "-"


        End If

    End Sub

#End Region

#Region " Evènements "

    Private Sub MAJIAffichageChargeEnCours()

        '--( Déclarations

        Dim lMixte As Boolean = MyPoutreLoc.lMixte
        Dim lEnrob As Boolean = MyPoutreLoc.lEnrobage
        Dim strCasNormal As String
        Dim lNDalle As Boolean = False
        Dim lNEnrob As Boolean = False
        Dim nDalle, nDalleLT As Decimal
        'Dim nEnrob, nEnrobLT As Decimal
        Dim nEnrob As Decimal
        Dim RH As Decimal
        Dim t0 As Decimal
        Dim PsiL As Decimal
        Dim TimeT As Decimal = MyPoutreLoc.Param.AgeT
        Dim H0Dalle As Decimal = MyPoutreLoc.Dalle.NotionalSizeH0(MyPoutreLoc)
        Dim H0Enrob As Decimal = MyPoutreLoc.Section.NotionalSizeEnrobage
        Dim lConstruction As Boolean
        Dim Psi(2) As Decimal
        Dim lPsi As Boolean = False
        Dim lChargeQ As Boolean = False
        Dim lDoubleN As Boolean

        '--( Initialisation

        RH = MyPoutreLoc.Param.RH

        Me.img_InfoPP.Visible = (rad_G1.Checked)

        '=== MISE A JOUR DU NOM DU CHARGEMENT =============================================================================

        Select Case True
            Case rad_G1.Checked
                If lMixte Then
                    Me.lbl_NameLoad.Text = strInfoG1
                Else
                    Me.lbl_NameLoad.Text = strInfoG
                End If

            Case rad_G2.Checked
                Me.lbl_NameLoad.Text = strInfoG2
            Case rad_Q1.Checked
                Me.lbl_NameLoad.Text = strInfoQ1
            Case rad_Q2.Checked
                Me.lbl_NameLoad.Text = strInfoQ2
            Case rad_Qc.Checked
                Me.lbl_NameLoad.Text = strInfoQc
        End Select

        '=== MISE A JOUR DU TYPE DE CHARGE ==================================================================================

        Me.lbl_Etaiement.Text = ""

        If lMixte Then
            strCasNormal = strPoutreMixte
        ElseIf lEnrob Then
            strCasNormal = strPoutreAcierEnrob
        Else
            strCasNormal = strPoutreAcier
        End If
        Select Case True
            Case rad_G1.Checked, rad_Qc.Checked
                lConstruction = rad_Qc.Checked
                If lMixte Then
                    lNEnrob = lEnrob
                    Select Case MyPoutreLoc.TypeEtaiement
                        Case cls_Poutre.EnuTypeEtaiement.FullyPropped
                            Me.lbl_EtatDalle.Text = strPoutreMixte
                            lNDalle = True
                            Me.lbl_Etaiement.Text = strEtaiementTotal
                        Case cls_Poutre.EnuTypeEtaiement.PointPropped
                            lNDalle = False
                            Me.lbl_Etaiement.Text = strEtaiementPoints
                        Case cls_Poutre.EnuTypeEtaiement.UnPropped
                            If lEnrob Then
                                Me.lbl_EtatDalle.Text = strPoutreAcierEnrob
                            Else
                                Me.lbl_EtatDalle.Text = strPoutreAcier
                            End If
                            lNDalle = False
                            Me.lbl_Etaiement.Text = strEtaiementSans
                    End Select
                Else
                    Me.lbl_EtatDalle.Text = strCasNormal
                    lNDalle = False
                    lNEnrob = lEnrob
                End If
                PsiL = MyPoutreLoc.Param.PsiLPermanent
                If lNDalle Then
                    t0 = MyPoutreLoc.Param.AgeT0G1(0)
                    nDalle = MyPoutreLoc.Dalle.beton.CoefficientEquivalence(RH, H0Dalle, TimeT, t0, PsiL, lGeneration1)
                End If
                If lNEnrob Then
                    If lConstruction Then
                        nEnrob = MyPoutreLoc.Section.Enrobage.Beton.CoefficientEquivalenceCT(lGeneration1)
                    Else
                        t0 = MyPoutreLoc.Param.AgeT0G1(1)
                        nEnrob = MyPoutreLoc.Section.Enrobage.Beton.CoefficientEquivalence(RH, H0Enrob, TimeT, t0, PsiL, lGeneration1)
                    End If
                End If

            Case rad_G2.Checked
                Me.lbl_EtatDalle.Text = strCasNormal
                lNDalle = lMixte
                lNEnrob = lEnrob
                PsiL = MyPoutreLoc.Param.PsiLPermanent
                If lNDalle Then
                    t0 = MyPoutreLoc.Param.AgeT0G2(0)
                    nDalle = MyPoutreLoc.Dalle.beton.CoefficientEquivalence(RH, H0Dalle, TimeT, t0, PsiL, lGeneration1)
                End If
                If lNEnrob Then
                    t0 = MyPoutreLoc.Param.AgeT0G2(1)
                    nEnrob = MyPoutreLoc.Section.Enrobage.Beton.CoefficientEquivalence(RH, H0Enrob, TimeT, t0, PsiL, lGeneration1)
                End If
            Case rad_Q1.Checked
                Me.lbl_EtatDalle.Text = strCasNormal
                lNDalle = lMixte
                lNEnrob = lEnrob
                If lNDalle Then
                    nDalle = MyPoutreLoc.Dalle.beton.CoefficientEquivalenceCT(lGeneration1)
                End If
                If lNEnrob Then
                    nEnrob = MyPoutreLoc.Section.Enrobage.Beton.CoefficientEquivalenceCT(lGeneration1)
                End If
                lPsi = True
                Psi = {MyPoutreLoc.Param.Gamma.Psi0_Q1, MyPoutreLoc.Param.Gamma.Psi1_Q1, MyPoutreLoc.Param.Gamma.Psi2_Q1}
                lChargeQ = True
            Case rad_Q2.Checked
                Me.lbl_EtatDalle.Text = strCasNormal
                lNDalle = lMixte
                lNEnrob = lEnrob
                If lNDalle Then
                    nDalle = MyPoutreLoc.Dalle.beton.CoefficientEquivalenceCT(lGeneration1)
                End If
                If lNEnrob Then
                    nEnrob = MyPoutreLoc.Section.Enrobage.Beton.CoefficientEquivalenceCT(lGeneration1)
                End If
                lPsi = True
                Psi = {MyPoutreLoc.Param.Gamma.Psi0_Q2, MyPoutreLoc.Param.Gamma.Psi1_Q2, MyPoutreLoc.Param.Gamma.Psi2_Q2}
                lChargeQ = True
        End Select

        '=== MISE A JOUR DES COEFFICIENTS EQUIVALENCE ACIER BETON ===========================================================

        '***( indentification des cas avec double coefficient d'équivalence

        lDoubleN = lChargeQ And MyPoutreLoc.Param.lPsi2LongTerm And (MyPoutreLoc.lMixte Or MyPoutreLoc.lEnrobage)

        Me.pan_DoubleN.Visible = lDoubleN
        Me.txt_NDalleLTPsi2.Visible = lDoubleN
        Me.txt_NenrobLTPsi2.Visible = lDoubleN
        'Me.img_info.Visible = lDoubleN
        'Me.img_InfoPsi2.Visible = lDoubleN

        '***( Coefficient de base

        Dim lGene1 As Boolean = MyPoutreLoc.Param.lGeneration1

        Me.pan_Ndalle.Visible = lNDalle
        If lNDalle Then
            Me.txt_Ndalle.Text = GetStringInUnitN(nDalle, Enu_TypeVariable.SansType, 3, 2, Enu_AfficheUnite.Non, True)

            If lDoubleN Then
                nDalleLT = MyPoutreLoc.Dalle.beton.CoefficientEquivalence(RH, H0Dalle, TimeT, MyPoutreLoc.Param.AgeT0G2(0), MyPoutreLoc.Param.PsiLPermanent, lGene1)

                Me.txt_NDalleLTPsi2.Text = GetStringInUnitN(nDalleLT, Enu_TypeVariable.SansType, 3, 2, Enu_AfficheUnite.Non, True)
            End If

        End If

        Me.pan_Nenrob.Visible = lNEnrob
        If lNEnrob Then
            Me.txt_Nenrob.Text = GetStringInUnitN(nEnrob, Enu_TypeVariable.SansType, 3, 2, Enu_AfficheUnite.Non, True)

            If lDoubleN Then
                ' nEqEnrobG2 = Me.Dalle.beton.CoefficientEquivalence(RH, H0Enrob, TimeT, Me.Param.AgeT0G2(1), Me.Param.PsiLPermanent, lGene1)

            End If
        End If

        Me.lbl_NCoef.Visible = lNEnrob Or lNDalle
        Me.pan_CoefEquivalence.Visible = lNEnrob Or lNDalle

        Me.pan_Psi.Visible = lPsi
        If lPsi Then
            Me.txt_Psi0.Text = GetStringInUnitN(Psi(0), Enu_TypeVariable.SansType, 4, 3, NON_U, True)
        Me.txt_Psi1.Text = GetStringInUnitN(Psi(1), Enu_TypeVariable.SansType, 4, 3, NON_U, True)
            Me.txt_Psi2.Text = GetStringInUnitN(Psi(2), Enu_TypeVariable.SansType, 4, 3, NON_U, True)
        End If
    End Sub

    Private Sub MAJI_InfoCharges()

        Me.pan_Parametres.Visible = MyPoutreLoc.lMixte Or MyPoutreLoc.lEnrobage

        Me.pan_Ndalle.Visible = MyPoutreLoc.lMixte

        Me.pan_Nenrob.Visible = MyPoutreLoc.lEnrobage

    End Sub

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

            MAJIAffichageChargeEnCours()
            MAJIAffichageChargeSurfacique()
            MAJIAffichageButtonsLineiques()
            MAJIAffichageTableauxLineique()
            MAJIAffichageButtonsPonctuels()
            MAJIAffichageTableauxPonctuel()

            MAJIReactions()

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

        MAJIReactions()

        img_Chargement.Invalidate()

    End Sub

    Private Sub btn_AjouterSupprimerPonctuel_Click(sender As Object, e As EventArgs) Handles btn_AjouterPonctuelle.Click, btn_SupprimerPonctuelle.Click
        If lBuild Then Exit Sub

        Dim nbF As Integer = MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours).Count
        Dim xPos, xPrec As Decimal
        Const kPOS As Decimal = 1 / 10

        Select Case sender.name
            Case btn_AjouterPonctuelle.Name
                If nbF = 0 Then
                    xPos = MyPoutreLoc.LongueurTravee(traveeEnCours) * kPOS
                Else
                    xPrec = MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours)(nbF - 1).xPosT
                    xPos = xPrec + (MyPoutreLoc.LongueurTravee(traveeEnCours) - xPrec) / (1 / kPOS - CDec(nbF))
                End If
                'myBeamLoc.ChargesU(chargeEnCours).Forces(traveeEnCours).Add(New cls_Force(myBeamLoc.LongueurTravee(traveeEnCours) / 2, 10 ^ 3, myBeamLoc.xPositionAppui(True, traveeEnCours))) '1kN
                MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours).Add(New cls_Force(xPos, FORCECDEF, MyPoutreLoc.xPositionAppui(True, traveeEnCours))) '1kN
                NbChargePonctuelle = Math.Min(NbChargePonctuelle + 1, NbChargePonctuelleMAX)
                iChargePonctuelleSelect = nbF
            Case btn_SupprimerPonctuelle.Name
                MyPoutreLoc.ChargesU(chargeEnCours).Forces(traveeEnCours).RemoveAt(NbChargePonctuelle - 1)
                NbChargePonctuelle = Math.Max(NbChargePonctuelle - 1, 0)
                iChargePonctuelleSelect = -1
        End Select

        MAJIAffichageButtonsPonctuels()
        MAJIAffichageTableauxPonctuel()

        MAJIReactions()

        img_Chargement.Invalidate()

    End Sub

    Private Sub MAJIAffichageChargeSurfacique(Optional lMAJLargeur As Boolean = True, Optional lMAJPression As Boolean = True)

        'MAJ AffichageOptFeu des valeurs dans la section charge surfacique 

        'If lMAJLargeur Then txt_WidthApplication.Text = myBeamLoc.ChargesU(chargeEnCours).WSurf(traveeEnCours) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
        If lMAJLargeur Then
            'txt_WidthApplication.Text = MyPoutreLoc.LargeurInfluence
            txt_WidthApplication.Text = GetStringInUnitN(MyPoutreLoc.LargeurInfluence, Enu_TypeVariable.Longueur, 4, 3, NON_U, True)
        End If
        If lMAJPression Then
            'txt_UniformLoad.Text = MyPoutreLoc.ChargesU(chargeEnCours).QSurf(traveeEnCours) / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur) ^ 2)
            Dim qSload = MyPoutreLoc.ChargesU(chargeEnCours).QSurf(traveeEnCours) / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur) ^ 2)
            txt_UniformLoad.Text = GetStringInUnitN(qSload, Enu_TypeVariable.SansType, 4, 3, NON_U, False)
        End If
        'txt_ResultingForce.Text = myBeamLoc.ChargesU(chargeEnCours).WSurf(traveeEnCours) * myBeamLoc.ChargesU(chargeEnCours).QSurf(traveeEnCours) / LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort)

        'Dim qSResult As Decimal = MyPoutreLoc.LongueurTravee(traveeEnCours) * MyPoutreLoc.LargeurInfluence * MyPoutreLoc.ChargesU(chargeEnCours).QSurf(traveeEnCours) / LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort)
        Dim qSResult As Decimal = MyPoutreLoc.LongueurTravee(traveeEnCours) * MyPoutreLoc.LargeurInfluence * MyPoutreLoc.ChargesU(chargeEnCours).QSurf(traveeEnCours) ' / LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort)

        txt_ResultingForce.Text = GetStringInUnitN(qSResult, Enu_TypeVariable.Effort, 4, 3, NON_U, True)

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
            MAJIAffichageChargeSurfacique()


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
                    'myBeamLoc.ChargesU(chargeEnCours).WSurf(traveeEnCours) = ValeurUI
                    MAJIAffichageChargeSurfacique(False, True)
                Case txt_UniformLoad.Name
                    MyPoutreLoc.ChargesU(chargeEnCours).QSurf(traveeEnCours) = ValeurUI
                    MAJIAffichageChargeSurfacique(True, False)
            End Select

            MAJIReactions()

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

            MAJIReactions()
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

            MAJIReactions()
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
        Dim lMAJ As Boolean = False

        If MyPoutreLoc.Param.lAutoPP <> MyProjet.Poutres(MyProjet.IndEnCours).Param.lAutoPP Then
            '== MISE A JOUR

            lMAJ = True
            MyPoutreLoc.Param.lAutoPP = MyProjet.Poutres(MyProjet.IndEnCours).Param.lAutoPP
            MyPoutreLoc.Param.qPPCustom = MyProjet.Poutres(MyProjet.IndEnCours).Param.qPPCustom

        ElseIf MyPoutreLoc.Param.qPPCustom <> MyProjet.Poutres(MyProjet.IndEnCours).Param.qPPCustom Then
            '== MISE A JOUR
            lMAJ = True
            MyPoutreLoc.Param.qPPCustom = MyProjet.Poutres(MyProjet.IndEnCours).Param.qPPCustom

        End If

        If lMAJ Then

            MyPoutreLoc.InitialisePoidsPropres()
            MAJIAffichageTableauxLineique()

            MAJIReactions()

        End If


    End Sub

    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider_Frm_Chargement.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim kUnit As Decimal

        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True

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


        iErreur = ValideSaisieNombre(MyTxt.Text, lValMin, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider_Frm_Chargement, ValMin, lValMin, ValMax, lValMax)
        Else

            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
        End If

        lOk = (iErreur = 0)
        Return lOk
    End Function

#End Region

#Region " Gestion de la souris "

    Private Sub img_Chargement_MouseMove(sender As Object, e As MouseEventArgs) Handles img_Chargement.MouseMove

        Dim xSouris, ySouris As Single
        Dim xReel, yReel As Decimal

        '# Coordonnées de la souris dans l'univers écran

        xSouris = e.X
        ySouris = e.Y

        '# Conversion dans le repère de la poutre

        xReel = XUnivers(MyParAff, xSouris)
        yReel = YUnivers(MyParAff, ySouris)

        '# Recherche dans quelle partie se situe-t-on

        WhereIsTheMouse(xReel, yReel)

    End Sub

    Private Sub WhereIsTheMouse(xReel As Decimal, yReel As Decimal)
        '--------------------------------------------------------------------------------------------------------
        '   01/12/23 :      Création - POM
        '--------------------------------------------------------------------------------------------------------
        '   Recherche de la position de la souris dans la poutre (quelle travée est survolée par la souris
        '--------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lTrouve As Boolean = False
        Dim iTravee As Integer = MyPoutreLoc.IndicePremiereTravee
        Dim iFinT As Integer = MyPoutreLoc.IndiceDerniereTravee
        Dim zDalle As Decimal = MyPoutreLoc.Dalle.zTop
        Dim zSem As Decimal = -MyPoutreLoc.Section.ProfilA.ha
        Dim traveeMouseEnCours As Integer = traveeMouse

        '--> On recherche si la souris est positionnée sur une travée

        Do While (Not lTrouve) And (iTravee <= iFinT)

            If IsGreater(xReel, MyPoutreLoc.xPositionAppui(True, iTravee)) And IsSmaller(xReel, MyPoutreLoc.xPositionAppui(False, iTravee)) _
            And IsGreater(yReel, zSem) And IsSmaller(yReel, zDalle) Then
                lTrouve = True
                traveeMouse = iTravee
            Else
                iTravee += 1
            End If

        Loop

        If Not lTrouve Then traveeMouse = -1

        If traveeMouse <> traveeMouseEnCours Then
            Me.img_Chargement.Invalidate()
        End If

    End Sub


    Private Sub img_Chargement_MouseUp(sender As Object, e As MouseEventArgs) Handles img_Chargement.MouseUp

        '--> Gestion de la selection d'une travée par la souris

        If traveeMouse <> -1 Then
            Me.cmb_Travee.SelectedIndex = traveeMouse - MyPoutreLoc.IndicePremiereTravee

        End If

    End Sub

#End Region

#Region " Symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_Psi2.Paint, img_Psi1.Paint, img_Psi0.Paint
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

        lIndice = True
        lGrec = True
        lEgal = True

        Select Case sender.name
            Case Me.img_Psi0.Name

                strSymbol = "y"
                strIndice = "0"

            Case Me.img_Psi1.Name

                strSymbol = "y"
                strIndice = "1"

            Case Me.img_Psi2.Name

                strSymbol = "y"
                strIndice = "2"
        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

#Region " Infos W "

    '==== INFO PP ===================================================================================================

    Private Sub img_InfoPP_MouseEnter(sender As Object, e As EventArgs) Handles img_InfoPP.MouseEnter
        lSelectInfoPP = True
        Me.img_InfoPP.Invalidate()
    End Sub

    Private Sub img_InfoPP_MouseLeave(sender As Object, e As EventArgs) Handles img_InfoPP.MouseLeave
        lSelectInfoPP = False
        Me.img_InfoPP.Invalidate()
    End Sub

    Private Sub img_InfoPP_Click(sender As Object, e As EventArgs) Handles img_InfoPP.Click
        PublieInfoPP()
    End Sub

    Private Sub img_Icone_1_Paint(sender As Object, e As PaintEventArgs) Handles img_InfoPP.Paint

        DessineIconeInfo(e.Graphics, Me.img_InfoPP, lSelectInfoPP)

    End Sub

    Private Sub PublieInfoPP()

        Dim strMessage As String = ""

        Dim lAutoPP As Boolean = MyPoutreLoc.Param.lAutoPP

        If lAutoPP Then
            strMessage = strAutoPP
        Else
            strMessage = strCustomPP
        End If

        InfoW.InitialiseInfo()
        InfoW.AddInfo(strMessage)

        InfoW.Publie()

    End Sub

    '==== INFO PSI2 ===================================================================================================
    Private Sub img_InfoPsi2_MouseEnter(sender As Object, e As EventArgs) Handles img_InfoPsi2.MouseEnter
        lSelectInfoPsi2 = True
        Me.img_InfoPsi2.Invalidate()
    End Sub

    Private Sub img_InfoPsi2_MouseLeave(sender As Object, e As EventArgs) Handles img_InfoPsi2.MouseLeave
        lSelectInfoPsi2 = False
        Me.img_InfoPsi2.Invalidate()
    End Sub

    Private Sub img_InfoPsi2_Click(sender As Object, e As EventArgs) Handles img_InfoPsi2.Click
        PublieInfoPsi2()
    End Sub
    Private Sub img_InfoPsi2_Paint(sender As Object, e As PaintEventArgs) Handles img_InfoPsi2.Paint

        DessineIconeInfo(e.Graphics, Me.img_InfoPsi2, lSelectInfoPsi2)

    End Sub

    Private Sub img_info_Click(sender As Object, e As EventArgs) Handles img_info.Click
        'If InfoW_lVisible Then
        '    InfoW_Fermer()
        'Else
        PublieInfoPsi2
        'End If
    End Sub

    Private Sub PublieInfoPsi2()

        Dim strMessage As String
        Dim RefEN As String = ""

        If MyPoutreLoc.Param.lGeneration1 Then
            RefEN = "EN1990:2003, 4.1.3(1) c)"
        Else
            RefEN = "EN1990:2023, 6.1.2.3(3) Note 4"
        End If

        strMessage = RemplaceDollar(strReference, RefEN)

        InfoW.InitialiseInfo()
        InfoW.AddInfo(strMessage)

        InfoW.Publie()

    End Sub

#End Region


End Class