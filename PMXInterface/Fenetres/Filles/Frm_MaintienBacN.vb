Imports PMXMoteur2
Imports System.IO
Imports System.Runtime.CompilerServices

Public Class Frm_MaintienBacN

#Region " Déclarations "

    Enum enu_AffParametres
        Plancher
        Fixation
    End Enum

    Const SELBACINDI As Integer = 1
    Const SELPOUTRE As Integer = 2
    Const SELLARGEURP As Integer = 3
    Const SELPORTEE As Integer = 4
    Const SELENTRAXE As Integer = 5
    Const SELAP As Integer = 6
    Const SELBP As Integer = 7
#End Region

#Region " Variables "

    Dim lBuild As Boolean = True
    Public localMaitienBac As New cls_MaintienBac
    Public localFup As Decimal

    Dim AffParam As enu_AffParametres

    Dim Bloc As New Dictionary(Of String, String)

    Dim lAffCalculs As Boolean = False
    Dim lAffFirst As Boolean = True

    Dim iSelect As Integer = -1

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_MaintienBacN_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lBuild = True

        GestionLangues()
        InitialiseVariables()
        InitialisationFenetre()
        GestionStyle()

        lBuild = False

    End Sub

    Private Sub InitialisationFenetre()
        Select Case AffParam
            Case enu_AffParametres.Fixation : Me.rdb_Fixations.Checked = True
            Case enu_AffParametres.Plancher : Me.rdb_Plancher.Checked = True
        End Select
        AffichageParametres()
        MAJI_Calculs()
    End Sub

    Private Sub AffichageParametres()

        Me.pan_ContenuG.Controls.Clear()

        Select Case AffParam
            Case enu_AffParametres.Plancher
                Me.pan_ContenuG.Controls.Add(Frm_MaintienBacN_Plancher.pan_Main)
                Frm_MaintienBacN_Plancher.InitialiseFenetre(Bloc)
            Case enu_AffParametres.Fixation
                Me.pan_ContenuG.Controls.Add(Frm_MaintienBacN_Fixation.pan_Main)
                Frm_MaintienBacN_Fixation.InitialiseFenetre(Bloc)
        End Select

    End Sub

    Private Sub InitialiseVariables()
        localMaitienBac = MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.Clone
        localFup = MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Fup
        AffParam = enu_AffParametres.Plancher
    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.img_Deck.Dock = DockStyle.Fill

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_DECKRESTRAINT")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== GENERAL ======================================================================

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                Me.rdb_Plancher.Text = Bloc("FLOOR")
                Me.rdb_Fixations.Text = Bloc("FASTENINGRDB")
                Me.chk_Calculs.Text = Bloc("RESULTS")

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues")
                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                'Bloc.Clear()
            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If

    End Sub

#End Region

#Region " Gestion des mises à jour de la fenêtre "

    Public Sub MAJI_Dessin()

        Me.img_Deck.Invalidate()

    End Sub

    Public Sub MAJI_Calculs()

        If lAffCalculs Then

            Me.TLpan_MaintienBac.ColumnStyles(1).Width = 250

            If lAffFirst Then

                Me.pan_Calculs.Controls.Add(Frm_MaintienBacN_Calculs.pan_Main)
                Frm_MaintienBacN_Calculs.InitialiseFenetre(Bloc)

                lAffFirst = False
            Else
                Frm_MaintienBacN_Calculs.AfficheResultats()
            End If
        Else
            Me.TLpan_MaintienBac.ColumnStyles(1).Width = 0
        End If

    End Sub


#End Region

#Region " Evènements "

    Private Sub chk_Calculs_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Calculs.CheckedChanged
        If lBuild Then Exit Sub
        lAffCalculs = Not lAffCalculs
        MAJI_Calculs()
        MAJI_Dessin()
    End Sub

    Private Sub rdb_Fixations_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Fixations.CheckedChanged
        If lBuild Then Exit Sub

        Select Case True
            Case Me.rdb_Plancher.Checked
                AffParam = enu_AffParametres.Plancher
            Case Me.rdb_Fixations.Checked
                AffParam = enu_AffParametres.Fixation
        End Select

        AffichageParametres()
    End Sub

    Private Sub Frm_MaintienBacN_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        MAJI_Dessin()
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

            'MyProjet.Poutres(MyProjet.IndEnCours).EstValidee(iFRMslab)

            Me.Close()
        End If
    End Sub

    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        GereTransfertValeur(localMaitienBac.m, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.m, lModif)
        GereTransfertValeur(localMaitienBac.nt, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.nt, lModif)
        GereTransfertValeur(localMaitienBac.ec, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.ec, lModif)
        GereTransfertValeur(localMaitienBac.Tpr, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.Tpr, lModif)
        GereTransfertValeur(localFup, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Fup, lModif)

        GereTransfertValeur(localMaitienBac.lMaintienBac, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.lMaintienBac, lModif)
        GereTransfertValeur(localMaitienBac.lTheta, MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.lTheta, lModif)

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.Transition <> localMaitienBac.Transition Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.Transition = localMaitienBac.Transition

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixNervuresMod <> localMaitienBac.FixNervuresMod Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixNervuresMod = localMaitienBac.FixNervuresMod

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixnervuresTyp <> localMaitienBac.FixnervuresTyp Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixnervuresTyp = localMaitienBac.FixnervuresTyp

        If MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixCoutureType <> localMaitienBac.FixCoutureType Then lModif = True
        MyProjet.Poutres(MyProjet.IndEnCours).MaintienBac.FixCoutureType = localMaitienBac.FixCoutureType

    End Sub


#End Region

#Region " Dessin du plancher "

    Private Sub img_Deck_Paint(sender As Object, e As PaintEventArgs) Handles img_Deck.Paint

        DrawPlancher(e.Graphics, Me.img_Deck.ClientRectangle.Width, Me.img_Deck.ClientRectangle.Height,
                     localMaitienBac, MyProjet.Poutres(MyProjet.IndEnCours), iSelect)

    End Sub

    Private Sub DrawPlancher(ByVal MyGr As Graphics, ByVal pWi As Single, ByVal pHi As Single,
                             myDeck As cls_MaintienBac, myBeam As cls_Poutre, iSelect As Integer, ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   06/01/24:   Création - POM - ACBPMX V1
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   Représentation du plancher et de la disposition des bacs pour le maitien
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics
        '   pWi, pHi    [E] :   Dimensions de l'image
        '   myDeck      [E] :   Conditions de maintiens par le bac
        '   myBeam      [E] :   Poutre traitée
        '   iSelect     [E] :   Indice de la partie sélectionée
        '-----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyParAffD As Struc_Affichage
        Const kAdjust As Decimal = 0.9

        Dim xMin, xMax As Decimal
        Dim yMin, yMax As Decimal
        Dim dCar, dCarC As Decimal
        Dim EntraxeD As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).EntraxeSolive
        Dim LargeurP As Decimal = myDeck.LargeurPlancher(EntraxeD)
        Dim NbPoutres As Integer
        Dim PorteeL As Decimal
        Dim MyPen As Pen
        Dim CouleurSN As Color = Color.DarkBlue
        Dim MyPenNormal As New Pen(Color.Black, 1.0)
        Dim MyPenSelect As New Pen(CouleurSN, 2.5)
        Dim iPoutreRef As Integer
        Dim nbLongi As Integer
        Dim xC, yC As Decimal
        Dim LongBac As Decimal
        Dim ap As Decimal = (EntraxeD * myDeck.m)
        Dim bp As Decimal = myBeam.Dalle.Bac.LargeurModule
        Dim xPoutre, xPoutreRef As Decimal
        Dim CouleurNormal As Color = Color.Black
        'Dim CouleurSelect As Color = Color.DarkRed
        Dim CouleurSelect As Color = Color.Red
        Dim xe, ye As Decimal
        Dim xo, yo As Decimal
        Dim xm, ym As Decimal
        Dim iBacRef As Integer

        '--> Initialisation

        PorteeL = myBeam.LongueurTravee(1)
        If myBeam.lIntermediaire Then iPoutreRef = 2 Else iPoutreRef = 1
        ap = (EntraxeD * myDeck.m)
        bp = myBeam.Dalle.Bac.LargeurModule

        If MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire Then
            xPoutreRef = EntraxeD
        Else
            xPoutreRef = 0
        End If

        iBacRef = 2

        '--> Initialisation des paramètres d'affichage

        dCar = EntraxeD / 10
        dCarC = 0.6 * Math.Sqrt(LargeurP ^ 2 + PorteeL ^ 2) / 12
        yMin = 0 - 2 * dCarC
        xMin = 0 - dCarC
        xMax = LargeurP
        yMax = PorteeL + dCarC

        ParametresAffichage(MyParAffD, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kAdjust)

        '--> Représentation des bacs (individuels)

        '# nombre de bac sur la longueur de la poutre
        nbLongi = Math.Floor(PorteeL / bp)
        Select Case myDeck.Transition
            Case cls_MaintienBac.Enu_Transition.Aboutage
                LongBac = ap
            Case cls_MaintienBac.Enu_Transition.Adistance
                LongBac = ap - dCar
            Case cls_MaintienBac.Enu_Transition.Emboitement
                LongBac = ap + dCar
        End Select

        '# représentation des panneaux entiers
        For iTrans As Integer = 1 To myDeck.nt

            For iLongi As Integer = 1 To nbLongi

                xC = (iTrans - 1 / 2) * ap
                yC = (iLongi - 1 / 2) * bp

                DrawBacInd(MyGr, MyParAffD, xC, yC, LongBac, bp, myBeam.Dalle.Bac, False, MyPenNormal)

            Next

        Next

        '# représentation du reste
        If IsSmaller(nbLongi * myBeam.Dalle.Bac.LargeurModule, PorteeL) Then

            Dim DeltaL As Decimal = PorteeL - nbLongi * myBeam.Dalle.Bac.LargeurModule

            yC = PorteeL - DeltaL / 2
            For iTrans As Integer = 1 To myDeck.nt

                xC = (iTrans - 1 / 2) * (ap)

                DrawBacInd(MyGr, MyParAffD, xC, yC, LongBac, DeltaL, myBeam.Dalle.Bac, False, MyPenNormal)

            Next

        End If

        '# représentation du bac sélectionné

        If (iSelect = SELBACINDI) Or (iSelect = SELAP) Or (iSelect = SELBP) Then

            xC = ap / 2
            yC = (iBacRef - 1 / 2) * bp
            MyPenSelect.Color = CouleurSelect
            DrawBacInd(MyGr, MyParAffD, xC, yC, LongBac, bp, myBeam.Dalle.Bac, False, MyPenSelect)

        End If

        '--> Représentation des poutres

        NbPoutres = myDeck.m * myDeck.nt + 1
        MyPenSelect.Color = CouleurSN

        For iPoutre As Integer = 1 To NbPoutres

            xPoutre = (iPoutre - 1) * EntraxeD
            If iPoutreRef = iPoutre Then MyPen = MyPenSelect Else MyPen = MyPenNormal
            AddLigne(MyGr, MyPen, xPoutre, 0, xPoutre, PorteeL, MyParAffD)

        Next

        If iSelect = SELPOUTRE Then

            MyPenSelect.Color = CouleurSelect
            AddLigne(MyGr, MyPenSelect, xPoutreRef, 0, xPoutreRef, PorteeL, MyParAffD)

        End If

        '--> Représentation du maintien par le bac

        Dim dCarM As Decimal = EntraxeD / 10

        If myDeck.lMaintienBac Then

            If iSelect = SELPOUTRE Then
                MyPen = MyPenSelect
            Else
                MyPen = MyPenNormal
            End If

            Dim nbOndes As Integer = CInt(PorteeL / 2 / dCarM)

            dCarM = PorteeL / 2 / nbOndes

            For i As Integer = 1 To nbOndes

                xo = xPoutreRef
                xe = xPoutreRef + dCarM
                yo = (i - 1) * 2 * dCarM
                ye = yo + dCarM

                AddLigne(MyGr, MyPen, xo, yo, xe, ye, MyParAffD)

                yo = i * 2 * dCarM
                'ye = yo + dCarM

                AddLigne(MyGr, MyPen, xo, yo, xe, ye, MyParAffD)

            Next

        End If

        '--( Symbole pour le maintien

        Dim iBacS As Integer = nbLongi - 1

        If myDeck.lMaintienBac Then

            Const kECH As Decimal = 0.95

            If iSelect = SELPOUTRE Then
                MyPen = MyPenSelect
            Else
                MyPen = MyPenNormal
            End If

            xC = xPoutreRef + 3 * dCarM
            yC = (iBacS - 1 / 2) * bp + dCarM

            DessineSymbolShear(MyGr, MyParAffD, xC, yC, kECH * dCarM, MyPen)

            If myDeck.lTheta Then

                yC = (iBacS - 1 / 2) * bp - dCarM

                DessineSymbolBending(MyGr, MyParAffD, xC, yC, kECH * dCarM, MyPen)

            End If

        End If

        '--( Cotation

        Dim myPenC As New Pen(Color.Black, 1)
        Dim myPenBrusch As New SolidBrush(Color.Black)
        Dim myBrushFond As New SolidBrush(SystemColors.ControlLightLight)
        Dim chaine As String
        Dim CouleurF As Color
        Dim myFont As New Font(FontBase.Name, SizeFontFrm)

        '# Largeur du plancher

        If iSelect = SELLARGEURP Then
            CouleurF = CouleurSelect
        Else
            CouleurF = CouleurNormal
        End If

        yo = -2 * dCarC
        ye = yo
        xo = 0
        xe = LargeurP
        xm = (xo + xe) / 2
        ym = yo

        myPenC.Color = CouleurF
        myPenBrusch.Color = CouleurF

        AddFleche(MyGr, myPenC, xo, yo, xe, ye, MyParAffD, True, True)
        chaine = GetStringInUnitN(LargeurP, Enu_TypeVariable.Longueur, 4, 3, NON_U, True)
        AddTexteFond(MyGr, myPenBrusch, chaine, myFont, xm, ym, MyParAffD, HorizontalAlignment.Center, VerticalAlignement.Middle, myBrushFond, myPenC, False)

        '# Longueur du plancher

        If iSelect = SELPORTEE Then
            CouleurF = CouleurSelect
        Else
            CouleurF = CouleurNormal
        End If

        yo = 0
        ye = PorteeL
        xo = -dCarC
        xe = -dCarC
        ym = (yo + ye) / 2
        xm = -dCarC

        myPenC.Color = CouleurF
        myPenBrusch.Color = CouleurF

        AddFleche(MyGr, myPenC, xo, yo, xe, ye, MyParAffD, True, True)
        chaine = GetStringInUnitN(PorteeL, Enu_TypeVariable.Longueur, 4, 3, NON_U, True)
        AddTexteFond(MyGr, myPenBrusch, chaine, myFont, xm, ym, MyParAffD, HorizontalAlignment.Center, VerticalAlignement.Middle, myBrushFond, myPenC, False)

        '# Longueur Panneau individuel

        If (iSelect = SELBACINDI) Or (iSelect = SELAP) Then
            CouleurF = CouleurSelect
        Else
            CouleurF = CouleurNormal
        End If

        yo = -1 * dCarC
        ye = yo
        xo = 0
        xe = ap
        xm = (xo + xe) / 2
        ym = -dCarC

        myPenC.Color = CouleurF
        myPenBrusch.Color = CouleurF

        AddFleche(MyGr, myPenC, xo, yo, xe, ye, MyParAffD, True, True)
        chaine = GetStringInUnitN(ap, Enu_TypeVariable.Longueur, 4, 3, NON_U, True)
        AddTexteFond(MyGr, myPenBrusch, chaine, myFont, xm, ym, MyParAffD, HorizontalAlignment.Center, VerticalAlignement.Middle, myBrushFond, myPenC, False)

        '# Largeur panneau individuel

        If (iSelect = SELBACINDI) Or (iSelect = SELBP) Then
            CouleurF = CouleurSelect
        Else
            CouleurF = CouleurNormal
        End If

        yo = bp * (iBacRef - 1)
        ye = bp * iBacRef
        xo = dCarC
        xe = xo
        ym = (yo + ye) / 2
        xm = xo

        myPenC.Color = CouleurF
        myPenBrusch.Color = CouleurF

        AddFleche(MyGr, myPenC, xo, yo, xe, ye, MyParAffD, True, True)
        chaine = GetStringInUnitN(bp, Enu_TypeVariable.Longueur, 4, 3, NON_U, True)
        AddTexteFond(MyGr, myPenBrusch, chaine, myFont, xm, ym, MyParAffD, HorizontalAlignment.Center, VerticalAlignement.Middle, myBrushFond, myPenC, False)

        '# Entraxe des solives

        If iSelect = SELENTRAXE Then
            CouleurF = CouleurSelect
        Else
            CouleurF = CouleurNormal
        End If

        yo = PorteeL + dCarC
        ye = yo
        xo = 0
        xe = EntraxeD
        xm = (xo + xe) / 2
        ym = yo

        ' CouleurF = Color.Black
        myPenC.Color = CouleurF
        myPenBrusch.Color = CouleurF

        AddFleche(MyGr, myPenC, xo, yo, xe, ye, MyParAffD, True, True)
        chaine = GetStringInUnitN(EntraxeD, Enu_TypeVariable.Longueur, 4, 3, NON_U, True)
        AddTexteFond(MyGr, myPenBrusch, chaine, myFont, xm, ym, MyParAffD, HorizontalAlignment.Center, VerticalAlignement.Middle, myBrushFond, myPenC, False)

    End Sub

    Private Sub DessineSymbolShear(MyGr As Graphics, myParAff As Struc_Affichage, xC As Decimal, yC As Decimal, dCar As Decimal, myPen As Pen)
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   23/01/25:   Création - POM - ACBPMX V1
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   Représentation du symbole maintien en cisaillement
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myParAff    [E] :   Paramètres d'affichage
        '   xC, yC      [E] :   Centre du symbole
        '   dCar        [E] :   Dimension du symbole
        '   myPen       [E] :   Pen
        '-----------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Const THETA As Decimal = 30 * Math.PI / 180

        Dim xPoints As New List(Of Decimal)
        Dim yPoints As New List(Of Decimal)

        Dim dSin As Decimal = dCar / 2 * Math.Sin(THETA)
        Dim dCos As Decimal = dCar / 2 * Math.Cos(THETA)

        '--( Préparation des points

        xPoints.Add(xC - 0.75 * dCar + dSin)
        yPoints.Add(yC + dCos)

        xPoints.Add(xC + 0.75 * dCar + dSin)
        yPoints.Add(yC + dCos)

        xPoints.Add(xC + 0.75 * dCar - dSin)
        yPoints.Add(yC - dCos)

        xPoints.Add(xC - 0.75 * dCar - dSin)
        yPoints.Add(yC - dCos)

        xPoints.Add(xPoints(0))
        yPoints.Add(yPoints(0))

        '--( Affichage du symbole

        For i As Integer = 0 To xPoints.Count - 2

            AddLigne(MyGr, myPen, xPoints(i), yPoints(i), xPoints(i + 1), yPoints(i + 1), myParAff)

        Next

    End Sub

    Private Sub DessineSymbolBending(MyGr As Graphics, myParAff As Struc_Affichage, xC As Decimal, yC As Decimal, dCar As Decimal, myPen As Pen)
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   23/01/25:   Création - POM - ACBPMX V1
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   Représentation du symbole maintien en flexion
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myParAff    [E] :   Paramètres d'affichage
        '   xC, yC      [E] :   Centre du symbole
        '   dCar        [E] :   Dimension du symbole
        '   myPen       [E] :   Pen
        '-----------------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim xPoints As New List(Of Decimal)
        Dim yPoints As New List(Of Decimal)
        Const NbPts As Integer = 20
        Dim i As Integer
        Dim PI As Decimal = Math.PI
        Dim DeltaA As Decimal = 2 * PI / (NbPts - 1)

        '--( Préparation des points

        For i = 0 To NbPts
            xPoints.Add(xC + 0.75 * dCar * ((i * DeltaA / 2) - 1))
            yPoints.Add(yC + 0.5 * dCar * (Math.Sin(i * DeltaA)))
        Next

        '--( Affichage du symbole

        For i = 0 To xPoints.Count - 2

            AddLigne(MyGr, myPen, xPoints(i), yPoints(i), xPoints(i + 1), yPoints(i + 1), myParAff)

        Next

    End Sub

    Private Sub DrawBacInd(MyGr As Graphics, myParAff As Struc_Affichage, xC As Decimal, yC As Decimal, LongueurB As Decimal, LargeurB As Decimal,
                           MyBac As cls_Bac, lNervures As Boolean, myPen As Pen)
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   06/01/24:   Création - POM - ACBPMX V1
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   Représentation d'un bac individuel
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics
        '   myParAff    [E] :   Paramètres d'affichage
        '   xC, yC      [E] :   Centre du bac
        '   LongueurB   [E] :   Longueur d'un panneau
        '   LargeurB  [E] :   Largeur d'un panneau
        '   myBac       [E] :   Bac
        '   lNervures   [E] :   
        '-----------------------------------------------------------------------------------------------------------------------------------


        Dim xo, yo As Decimal
        Dim xe, ye As Decimal

        xo = xC - LongueurB / 2
        yo = yC - LargeurB / 2
        xe = xC + LongueurB / 2
        ye = yC + LargeurB / 2        ' MyBac.LargeurModule / 2

        AddRectanglePlein(MyGr, New SolidBrush(Color.White), myPen, xo, yo, xe, ye, myParAff, False, True)

    End Sub

    Public Sub ChangeSelect(pSel As Integer)

        iSelect = pSel
        MAJI_Dessin()

    End Sub

    Public Sub ChangeSelectBacIndi()
        ChangeSelect(SELBACINDI)
    End Sub
    Public Sub ChangeSelectPoutre()
        ChangeSelect(SELPOUTRE)
    End Sub
    Public Sub ChangeSelectLargeurP()
        ChangeSelect(SELLARGEURP)
    End Sub
    Public Sub ChangeSelectPortee()
        ChangeSelect(SELPORTEE)
    End Sub
    Public Sub ChangeSelectEntraxe()
        ChangeSelect(SELENTRAXE)
    End Sub
    Public Sub ChangeSelectAP()
        ChangeSelect(SELAP)
    End Sub
    Public Sub ChangeSelectBP()
        ChangeSelect(SELBP)
    End Sub

#End Region

End Class