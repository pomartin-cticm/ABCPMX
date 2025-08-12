Imports PMXMoteur2

Public Class Frm_ConnectionSlimConnexion

#Region " Variables "

    Dim lBuild As Boolean

    'Définition des valeurs limites pour les caractéristiques longitudinales
    Dim Nb_Zones_MIN As Integer
    Dim Nb_Zones_MAX As Integer
    Dim Longueur_Zone_MIN As Decimal
    Dim Longueur_Zone_MAX As Decimal
    Dim Espacement_Longi_MIN As Decimal         'sxi,min dans les ST
    Dim Espacement_Longi_MAX As Decimal         'sxi,max dans les ST

    Dim Nb_TransV_Row_MIN As Integer = 1
    Dim Nb_TransV_Row_MAX As Integer
    Dim Espacement_Trans_MIN As Decimal
    Dim Pince_Trans_MIN As Decimal              'Correspond à eD,min dans les Specifications Techniques 

    Const traveeEnCours As Integer = 1          ' Poutre slim floor : 1 seule travée

    Dim lBtnAjouterSupprimer As Boolean         ' Indique si l'on est en train d'ajouter ou de supprimer une zone de connection

    Dim lSemSup As Boolean                      ' Indique si connexion par des goujons en semelle supérieure
    Dim lAme As Boolean                         ' Indique si connexion par des goujons dans l'âme
    Dim lGoujons As Boolean                     ' Indique si connexion par des goujons (semelle supérieure ou âme)

    Dim strStud, strArma As String

    Dim FontFrm As Font
#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(Bloc As Dictionary(Of String, String))

        lBuild = True

        GestionLangues(Bloc)
        GestionStyle()
        GestionUnites()

        ValeursLimites()
        PrepareFenetre()
        AfficheConnectionEnCours()

        lBuild = False

    End Sub

    Private Sub PrepareFenetre()


        Me.cmb_NbRow_I1.Visible = lGoujons
        Me.txt_NbRows.Visible = lGoujons

        RemplirCombosConnexion()

    End Sub

    Private Sub ValeursLimites()

        lSemSup = (Frm_ConnectionSlimN.localBeam.Dalle.lConnexSemSup)
        lAme = (Frm_ConnectionSlimN.localBeam.Dalle.lConnexAme)
        lGoujons = lSemSup Or lAme

        'Définition des valeurs limites pour les caractéristiques longitudinales
        Longueur_Zone_MIN = Math.Min(1, Frm_ConnectionSlimN.localBeam.LongueurTravee(traveeEnCours))
        Longueur_Zone_MAX = Frm_ConnectionSlimN.localBeam.LongueurTravee(traveeEnCours)
        Nb_Zones_MIN = 1

        If traveeEnCours = 0 Or traveeEnCours = Frm_ConnectionSlimN.localBeam.IndiceTraveeConsoleDroite Then
            Nb_Zones_MAX = 1 'dans le cas de consoles, on impose une seule zone de connection 
        Else 'cas d'une travée courante
            Nb_Zones_MAX = Math.Min(Math.Floor(Frm_ConnectionSlimN.localBeam.LongueurTravee(traveeEnCours) / Longueur_Zone_MIN), 3)
        End If

        Pince_Trans_MIN = GOUJ_DBORD_MIN

        Espacement_Trans_MIN = GOUJ_RAPESPYsurD_PLEINE_MIN * Frm_ConnectionSlimN.localBeam.Dalle.Goujons.d

        Nb_TransV_Row_MIN = 1

        Nb_TransV_Row_MAX = Math.Floor((Frm_ConnectionSlimN.localBeam.Section.ProfilA.Bfs - 2 * Pince_Trans_MIN - Frm_ConnectionSlimN.localBeam.Dalle.Goujons.d) / Espacement_Trans_MIN + 1)

        Nb_TransV_Row_MAX = Math.Max(1, Nb_TransV_Row_MAX)

        If (lSemSup Or lAme) Then
            Espacement_Longi_MIN = GOUJ_RAPESPXsurD_MIN * Frm_ConnectionSlimN.localBeam.Dalle.Goujons.d
            Espacement_Longi_MAX = Math.Min(GOUJ_RAPESPX, GOUJ_RAPESPXsurTD_MAX * Frm_ConnectionSlimN.localBeam.Dalle.Ep_td)
        Else
            Espacement_Longi_MIN = 0.025
            Espacement_Longi_MAX = ARMA_RAPESPX
        End If

    End Sub

    Private Sub GestionStyle()

        Me.pan_Main.Dock = DockStyle.Fill

        Me.pan_Img.Dock = DockStyle.Fill
        Me.img_Connexion.Dock = DockStyle.Fill

        Me.lbl_Connection.BackColor = CouleurBackBandeaux
        Me.lbl_Connection.ForeColor = CouleurForeBandeaux

        FontFrm = New Font(FontBase.Name, SizeFontFrm)

    End Sub

    Private Sub GestionUnites()



    End Sub

    Private Sub GestionLangues(Bloc As Dictionary(Of String, String))
        Try

            Me.lbl_Connection.Text = Bloc("CONNECTION")

            Me.btn_Ajouter.Text = Bloc("ADD")
            Me.btn_Supprimer.Text = Bloc("DELETE")

            Me.txt_Largeur.Text = Bloc("WIDTH") & " (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & ")"
            Me.txt_NbRows.Text = Bloc("ROW_NUMBER")
            Me.txt_EspacementLongi.Text = Bloc("SPACING") & " (" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & ")"


            strStud = Bloc("STUDS")
            strArma = Bloc("WEB_REINF")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub AfficheConnectionEnCours()

        MAJI_ObjetsConnexion()
        MAJI_SommeGoujons()

    End Sub

    Private Sub RemplirCombosConnexion()
        Me.cmb_NbRow_I1.Items.Clear()
        Me.cmb_NbRow_I2.Items.Clear()
        Me.cmb_NbRow_I3.Items.Clear()

        If lSemSup Then
            For i As Integer = Nb_TransV_Row_MIN To Nb_TransV_Row_MAX
                Me.cmb_NbRow_I1.Items.Add(i)
                Me.cmb_NbRow_I2.Items.Add(i)
                Me.cmb_NbRow_I3.Items.Add(i)
            Next
        Else
            Me.cmb_NbRow_I1.Items.Add(2)
            Me.cmb_NbRow_I2.Items.Add(2)
            Me.cmb_NbRow_I3.Items.Add(2)
        End If

        Me.cmb_NbRow_I1.SelectedIndex = 0
        Me.cmb_NbRow_I2.SelectedIndex = 0
        Me.cmb_NbRow_I3.SelectedIndex = 0
    End Sub

#End Region

#Region " Evènements "

    Private Sub btn_Ajouter_Click(sender As Object, e As EventArgs) Handles btn_Ajouter.Click
        If lBuild Then Exit Sub

        lBtnAjouterSupprimer = True

        If Frm_ConnectionSlimN.localBeam.NombreZones(traveeEnCours) <= Nb_Zones_MAX - 1 Then Frm_ConnectionSlimN.localBeam.NombreZones(traveeEnCours) += 1
        Me.btn_Ajouter.Enabled = Not Frm_ConnectionSlimN.localBeam.NombreZones(traveeEnCours) = Nb_Zones_MAX
        Me.btn_Supprimer.Enabled = Not Frm_ConnectionSlimN.localBeam.NombreZones(traveeEnCours) = Nb_Zones_MIN
        MAJI_Nb_Zone()
        MAJI_SommeGoujons()
        MAJI_ObjetsConnexion()

        ValideSaisieFenetre()
        Me.img_Connexion.Invalidate()

        lBtnAjouterSupprimer = False
    End Sub

    Private Sub btn_Supprimer_Click(sender As Object, e As EventArgs) Handles btn_Supprimer.Click
        If lBuild Then Exit Sub

        lBtnAjouterSupprimer = True

        If Frm_ConnectionSlimN.localBeam.NombreZones(traveeEnCours) >= Nb_Zones_MIN + 1 Then Frm_ConnectionSlimN.localBeam.NombreZones(traveeEnCours) -= 1
        Me.btn_Ajouter.Enabled = Not Frm_ConnectionSlimN.localBeam.NombreZones(traveeEnCours) = Nb_Zones_MAX
        Me.btn_Supprimer.Enabled = Not Frm_ConnectionSlimN.localBeam.NombreZones(traveeEnCours) = Nb_Zones_MIN
        MAJI_Nb_Zone()
        MAJI_SommeGoujons()
        MAJI_ObjetsConnexion()

        ValideSaisieFenetre()
        Me.img_Connexion.Invalidate()

        lBtnAjouterSupprimer = False
    End Sub

    ''' <summary>
    ''' Misa à jour de l'interface lors d'une modification du nombre de zones de connexion
    ''' </summary>
    Private Sub MAJI_ObjetsConnexion()
        '--------------------------------------------------------------------------------------

        Dim lBuildB As Boolean = lBuild
        lBuild = True

        With Frm_ConnectionSlimN.localBeam

            '--( Met à jour la visibilité des composants

            Me.txt_Largeur_I1.ReadOnly = (.NombreZones(traveeEnCours) = 1)

            Me.txt_I2.Visible = (.NombreZones(traveeEnCours) >= 2)
            Me.txt_Largeur_I2.Visible = (.NombreZones(traveeEnCours) >= 2)
            Me.txt_EspLongi_I2.Visible = (.NombreZones(traveeEnCours) >= 2)
            Me.cmb_NbRow_I2.Visible = (.NombreZones(traveeEnCours) >= 2) And lGoujons

            Me.txt_I3.Visible = (.NombreZones(traveeEnCours) >= 3)
            Me.txt_Largeur_I3.Visible = (.NombreZones(traveeEnCours) >= 3)
            Me.txt_EspLongi_I3.Visible = (.NombreZones(traveeEnCours) >= 3)
            Me.cmb_NbRow_I3.Visible = (.NombreZones(traveeEnCours) >= 3) And lGoujons

        End With

        AffichageParametresConnexion()

        lBuild = lBuildB

    End Sub

    Private Sub AffichageParametresConnexion()

        With Frm_ConnectionSlimN.localBeam

            '--( Met à jour les valeurs affichées

            Me.txt_Largeur_I1.Text = GetStringInUnitN(.LongueurZone(traveeEnCours, 0), Enu_TypeVariable.Longueur, 4, 2, NON_U, True)
            Me.txt_Largeur_I2.Text = GetStringInUnitN(.LongueurZone(traveeEnCours, 1), Enu_TypeVariable.Longueur, 4, 2, NON_U, True)
            Me.txt_Largeur_I3.Text = GetStringInUnitN(.LongueurZone(traveeEnCours, 2), Enu_TypeVariable.Longueur, 4, 2, NON_U, True)

            Me.cmb_NbRow_I1.SelectedIndex = .NrTransZone(traveeEnCours, 0) - 1
            Me.cmb_NbRow_I2.SelectedIndex = .NrTransZone(traveeEnCours, 1) - 1
            Me.cmb_NbRow_I3.SelectedIndex = .NrTransZone(traveeEnCours, 2) - 1

            Me.txt_EspLongi_I1.Text = GetStringInUnitN(.EspacementZone(traveeEnCours, 0), Enu_TypeVariable.Dimension, 4, 2, NON_U, True)
            Me.txt_EspLongi_I2.Text = GetStringInUnitN(.EspacementZone(traveeEnCours, 1), Enu_TypeVariable.Dimension, 4, 2, NON_U, True)
            Me.txt_EspLongi_I3.Text = GetStringInUnitN(.EspacementZone(traveeEnCours, 2), Enu_TypeVariable.Dimension, 4, 2, NON_U, True)

        End With


    End Sub

    Private Sub MAJI_Nb_Zone()

        'MAJ des longueurs de zone suite à un clique Ajouter ou Supprimer
        If lBuild Then Exit Sub

        With Frm_ConnectionSlimN.localBeam

            Select Case .NombreZones(traveeEnCours)
                Case 1
                    .LongueurZone(traveeEnCours, 0) = .LongueurTravee(traveeEnCours)
                    .LongueurZone(traveeEnCours, 1) = 0
                    .LongueurZone(traveeEnCours, 2) = 0
                Case 2
                    .LongueurZone(traveeEnCours, 0) = .LongueurTravee(traveeEnCours) / 2
                    .LongueurZone(traveeEnCours, 1) = .LongueurTravee(traveeEnCours) / 2
                    .LongueurZone(traveeEnCours, 2) = 0
                Case 3
                    .LongueurZone(traveeEnCours, 0) = .LongueurTravee(traveeEnCours) / 3
                    .LongueurZone(traveeEnCours, 1) = .LongueurTravee(traveeEnCours) / 3
                    .LongueurZone(traveeEnCours, 2) = .LongueurTravee(traveeEnCours) / 3
            End Select
        End With

    End Sub

    Private Sub MAJI_SommeGoujons()

        If lSemSup Then
            Me.etq_Somme.Text = Frm_ConnectionSlimN.localBeam.NombreGoujonTot(traveeEnCours) & " " & strStud
        ElseIf lAme Then
            Me.etq_Somme.Text = 2 * Frm_ConnectionSlimN.localBeam.NombreGoujonTot(traveeEnCours) & " " & strStud
        Else
            Me.etq_Somme.Text = Frm_ConnectionSlimN.localBeam.NombreGoujonTot(traveeEnCours) & " " & strArma
        End If

    End Sub

#End Region

#Region " Evènements de saisie "

    Private Sub cmb_NbRow_I1_I2_I3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_NbRow_I1.SelectedIndexChanged, cmb_NbRow_I2.SelectedIndexChanged, cmb_NbRow_I3.SelectedIndexChanged
        If lBuild Then Exit Sub 'Or lMAJAffichage Then Exit Sub

        Select Case sender.name
            Case cmb_NbRow_I1.Name
                Frm_ConnectionSlimN.localBeam.NrTransZone(traveeEnCours, 0) = cmb_NbRow_I1.SelectedIndex + 1
            Case cmb_NbRow_I2.Name
                Frm_ConnectionSlimN.localBeam.NrTransZone(traveeEnCours, 1) = cmb_NbRow_I2.SelectedIndex + 1
            Case cmb_NbRow_I3.Name
                Frm_ConnectionSlimN.localBeam.NrTransZone(traveeEnCours, 2) = cmb_NbRow_I3.SelectedIndex + 1
        End Select

        MAJI_SommeGoujons()
        'MAJ_affichage_txt_cmb_connection()
        Me.img_Connexion.Invalidate()
    End Sub


    Private Sub txt_Largeur_I1_I2_I3_TextChanged(sender As Object, e As EventArgs) Handles txt_Largeur_I1.TextChanged, txt_Largeur_I2.TextChanged, txt_Largeur_I3.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

            With Frm_ConnectionSlimN.localBeam

                Select Case sender.name
                    Case txt_Largeur_I1.Name
                        'If Not ltxt_Largeur_I1Enter Then Exit Sub
                        .LongueurZone(traveeEnCours, 0) = ValeurUI

                        Select Case .NombreZones(traveeEnCours)
                            Case 1
                                .LongueurZone(traveeEnCours, 1) = 0
                                .LongueurZone(traveeEnCours, 2) = 0
                            Case 2
                                .LongueurZone(traveeEnCours, 1) = .LongueurTravee(traveeEnCours) - .LongueurZone(traveeEnCours, 0)
                                .LongueurZone(traveeEnCours, 2) = 0
                            Case 3
                                .LongueurZone(traveeEnCours, 1) = (.LongueurTravee(traveeEnCours) - .LongueurZone(traveeEnCours, 0)) / 2
                                .LongueurZone(traveeEnCours, 2) = (.LongueurTravee(traveeEnCours) - .LongueurZone(traveeEnCours, 0)) / 2
                        End Select

                    Case txt_Largeur_I2.Name
                        ' If Not ltxt_Largeur_I2Enter Then Exit Sub
                        .LongueurZone(traveeEnCours, 1) = ValeurUI

                        If .NombreZones(traveeEnCours) = 2 Then
                            .LongueurZone(traveeEnCours, 0) = .LongueurTravee(traveeEnCours) - .LongueurZone(traveeEnCours, 1)
                            .LongueurZone(traveeEnCours, 2) = 0
                        Else '3 zones
                            .LongueurZone(traveeEnCours, 2) = .LongueurTravee(traveeEnCours) - .LongueurZone(traveeEnCours, 0) - .LongueurZone(traveeEnCours, 1)
                        End If

                    Case txt_Largeur_I3.Name
                        'If Not ltxt_Largeur_I3Enter Then Exit Sub
                        .LongueurZone(traveeEnCours, 2) = ValeurUI
                        .LongueurZone(traveeEnCours, 0) = .LongueurTravee(traveeEnCours) - .LongueurZone(traveeEnCours, 1) - .LongueurZone(traveeEnCours, 2)
                End Select

            End With

            MAJI_SommeGoujons()
            MAJI_ObjetsConnexion()

            Me.img_Connexion.Invalidate()
        End If
    End Sub


    Private Sub txt_EspLongi_I1_I2_I3_TextChanged(sender As Object, e As EventArgs) Handles txt_EspLongi_I1.TextChanged, txt_EspLongi_I2.TextChanged, txt_EspLongi_I3.TextChanged
        If lBuild Then Exit Sub

        Dim ValeurUI As Decimal

        If VerificationSaisie(sender, ValeurUI) Then

            With Frm_ConnectionSlimN.localBeam
                Select Case sender.name
                    Case txt_EspLongi_I1.Name
                        .EspacementZone(traveeEnCours, 0) = ValeurUI
                    Case txt_EspLongi_I2.Name
                        .EspacementZone(traveeEnCours, 1) = ValeurUI
                    Case txt_EspLongi_I3.Name
                        .EspacementZone(traveeEnCours, 2) = ValeurUI
                End Select
            End With

            MAJI_SommeGoujons()
            Me.img_Connexion.Invalidate()

        End If
    End Sub

#End Region

#Region " Vérifiation des données "
    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        Dim lOk As Boolean = True
        ErrorProvider.SetError(MyTxt, String.Empty)

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMin As Boolean = True
        Dim lValMax As Boolean = True
        Dim lValMaxConseillee As Boolean = False
        Dim kUnit As Decimal

        Select Case MyTxt.Name

            Case Me.txt_Largeur_I1.Name, Me.txt_Largeur_I2.Name, Me.txt_Largeur_I3.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)

                ValMin = Longueur_Zone_MIN / kUnit
                ValMax = Longueur_Zone_MAX / kUnit

            Case Me.txt_EspLongi_I1.Name, Me.txt_EspLongi_I2.Name, Me.txt_EspLongi_I3.Name
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

                ValMin = Espacement_Longi_MIN / kUnit
                ValMax = Espacement_Longi_MAX / kUnit

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


    Private Function ValideSaisieFenetre() As Boolean
        Dim lFrm_Valide As Boolean = True

        '== Vérification des textbox pour la définition des zones de saisie
        'Vérification des textbox au cas où
        Dim list_txtbox As New List(Of TextBox)

        With Frm_ConnectionSlimN.localBeam
            If .NombreZones(traveeEnCours) >= 1 Then list_txtbox.Add(Me.txt_Largeur_I1)
            If .NombreZones(traveeEnCours) >= 2 Then list_txtbox.Add(Me.txt_Largeur_I2)
            If .NombreZones(traveeEnCours) >= 3 Then list_txtbox.Add(Me.txt_Largeur_I3)

            If .NombreZones(traveeEnCours) >= 1 Then list_txtbox.Add(Me.txt_EspLongi_I1)
            If .NombreZones(traveeEnCours) >= 2 Then list_txtbox.Add(Me.txt_EspLongi_I2)
                If .NombreZones(traveeEnCours) >= 3 Then list_txtbox.Add(Me.txt_EspLongi_I3)

        End With

        Dim ValeurUI As Decimal

        For Each txtbox_loc As TextBox In list_txtbox
            VerificationSaisie(txtbox_loc, ValeurUI)
            If Not ErrorProvider.GetError(txtbox_loc) = String.Empty Then
                lFrm_Valide = False
                Exit For
            End If
        Next

        '== Vérification de l'enrobage de béton

        'Dim Cc, CcMin As Decimal
        'Dim Chaine As String = ""
        'Dim Reference As String

        'Cc = myBeamLoc.Dalle.zTop - myBeamLoc.Dalle.Goujons.hsc

        'CcMin = Goujons_EnrobageMini()

        'If IsSmaller(Cc, CcMin) And IsGreaterOrEqual(Cc, 0) Then

        '    Reference = Goujons_ReferenceEnrobageMini() & " (" & GetStringInUnitN(CcMin, Enu_TypeVariable.Dimension, 4, 3, Enu_AfficheUnite.OuiNdC, True) & ")"
        '    Chaine = RemplaceDollar(strWarningGoujons, GetStringInUnitN(Cc, Enu_TypeVariable.Dimension, 4, 3, Enu_AfficheUnite.OuiNdC, True))
        '    Chaine = RemplaceDollar(Chaine, Reference)

        '    Dim Rep As MsgBoxResult

        '    Rep = MsgBox(Chaine, MsgBoxStyle.OkCancel, LogicielInfo.Racine)

        '    lFrm_Valide = (Rep = MsgBoxResult.Ok)

        'End If

        Return lFrm_Valide
    End Function

#End Region

#Region " Dessin "
    Private Sub img_Connexion_Paint(sender As Object, e As PaintEventArgs) Handles img_Connexion.Paint

        Dim ChaineCx As String = strStud
        If Not lGoujons Then ChaineCx = strarma

        Const kADJUST As Decimal = 1
        DessinFrmConnection_ConnectionSlim(e.Graphics, Frm_ConnectionSlimN.localBeam,
                                           FontFrm, Me.img_Connexion.ClientRectangle.Width, Me.img_Connexion.ClientRectangle.Height,
                                           kADJUST, traveeEnCours, True, ChaineCx)

    End Sub


#End Region


End Class