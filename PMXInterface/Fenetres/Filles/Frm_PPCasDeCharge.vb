Imports PMXMoteur2
Imports System.Drawing.Drawing2D

Public Class Frm_PPCasDeCharge

#Region " Variables "

    Dim lBuild As Boolean

    Dim tab_fMin() As Decimal
    Dim tab_fMax() As Decimal
    Dim fMaxG As Decimal
    Dim fMinG As Decimal

    Dim tab_Mmin() As Decimal
    Dim tab_iNodeMmin() As Decimal
    Dim tab_Mmax() As Decimal
    Dim tab_iNodeMmax() As Decimal
    Dim tab_Vmin() As Decimal
    Dim tab_Vmax() As Decimal
    Dim MmaxG As Decimal
    Dim MminG As Decimal
    Dim VmaxG As Decimal
    Dim VminG As Decimal

    Dim lDessDeformee As Boolean = True     ' Affichage de la déformée
    Dim lDessMoment As Boolean = True       ' Affichage diagramme moments
    Dim lDessEffortT As Boolean = True      ' Affichage diagramme efforts tranchants
    Dim lDessInerties As Boolean = True     ' Affichage des inerties
    Dim lDessNumeros As Boolean = False     ' Affichage des numéros noeuds
    Dim lDessCharges As Boolean = False     ' Affichage des charges
    Dim lDessEchLocal As Boolean = False

    Private ColorDeg As Color = Color.Cornsilk

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_CasDeCharge_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionLangues()
        GestionStyle()
        GestionUnites()
        'InitialiseVariablesLocales()
        PrepareFenetre()
        AfficheCasdeCharge()

        lBuild = False
        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub GestionLangues()

        Me.Text = "Load cases"
        Me.lbl_CasDeCharges.Text = "Load cases"
        Me.btn_Annuler.Text = "Cancel"
        Me.btn_OK.Text = "Close"

        Me.lbl_Case.Text = "Case"
        Me.lbl_Etat.Text = "Etat"
        Me.lbl_RunCalcul.Text = "Calcul effectué ?"
        Me.lbl_Fleche.Text = "Flèche maxi"


        Me.chk_Chargement.Text = "Chargement"
        Me.chk_EffortTranchant.Text = "Diagramme V"
        Me.chk_Moment.Text = "Diagramme M"
        Me.chk_Numerotation.Text = "Numérotation"
        Me.chk_Fleches.Text = "Déformée"
        Me.chk_Inerties.Text = "Inerties des barres"
        Me.btn_EditModel.Text = "Editer le modèle"

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitDim1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitForce1.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)
        Me.etq_UnitForce2.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)
        Me.etq_UnitM1.Text = LogicielInfo.Unit_Moment(LogicielOptions.IndUnitMoment)
        Me.etq_UnitM2.Text = LogicielInfo.Unit_Moment(LogicielOptions.IndUnitMoment)

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_CasDeCharges.BackColor = CouleurBackBandeaux
        Me.lbl_CasDeCharges.ForeColor = CouleurForeBandeaux

        Me.img_Analyse.Dock = DockStyle.Fill

    End Sub

    Private Sub PrepareFenetre()
        Me.chk_Fleches.Checked = lDessDeformee
        Me.chk_Moment.Checked = lDessMoment
        Me.chk_EffortTranchant.Checked = lDessEffortT
        Me.chk_Numerotation.Checked = lDessNumeros
        Me.chk_Inerties.Checked = lDessInerties
        Me.chk_Chargement.Checked = lDessCharges
        Me.chk_LocalEchelle.Checked = lDessEchLocal
    End Sub

    Private Sub AfficheCasdeCharge()

        MyProjet.Poutres(MyProjet.IndEnCours).InitialiseCalculs(NomChargesA)
        MyProjet.Poutres(MyProjet.IndEnCours).AAA_CalculMNVInternes()
        InitialiseVariablesLocales()

        Me.cmb_Symbols.Items.Clear()

        For i As Integer = 0 To MyProjet.Poutres(MyProjet.IndEnCours).ChargesA.Count - 1
            Me.cmb_Symbols.Items.Add(MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i).Symbol)
        Next

        Me.cmb_Symbols.SelectedIndex = 0

        MAJI_CasdeCharge()

    End Sub

    Private Sub InitialiseVariablesLocales()

        Dim NbC As Integer = MyProjet.Poutres(MyProjet.IndEnCours).ChargesA.Count

        ReDim tab_fMax(NbC - 1)
        ReDim tab_fMin(NbC - 1)
        ReDim tab_Mmax(NbC - 1)
        ReDim tab_Mmin(NbC - 1)
        ReDim tab_iNodeMmax(NbC - 1)
        ReDim tab_iNodeMmin(NbC - 1)
        ReDim tab_Vmax(NbC - 1)
        ReDim tab_Vmin(NbC - 1)

        For i As Integer = 0 To NbC - 1
            MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i).EnveloppesFleche(tab_fMax(i), tab_fMin(i))
            If i = 0 Then
                fMaxG = tab_fMax(i)
                fMinG = tab_fMin(i)
            Else
                fMaxG = Math.Max(fMaxG, tab_fMax(i))
                fMinG = Math.Min(fMinG, tab_fMin(i))
            End If

            MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i).EnveloppesMoments(tab_Mmax(i), tab_iNodeMmax(i), tab_Mmin(i), tab_iNodeMmin(i))
            If i = 0 Then
                MmaxG = tab_Mmax(i)
                MminG = tab_Mmin(i)
            Else
                MmaxG = Math.Max(MmaxG, tab_Mmax(i))
                MminG = Math.Min(MminG, tab_Mmin(i))
            End If

            MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i).EnveloppesTranchants(tab_Vmax(i), tab_Vmin(i))
            If i = 0 Then
                VmaxG = tab_Vmax(i)
                VminG = tab_Vmin(i)
            Else
                VmaxG = Math.Max(VmaxG, tab_Vmax(i))
                VminG = Math.Min(VminG, tab_Vmin(i))
            End If

        Next

    End Sub

#End Region

#Region "===FERMETURE==="


    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub

#End Region

#Region " Evènements "

    Private Sub MAJI_CasdeCharge()

        Me.lbl_Name.Text = MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(Me.cmb_Symbols.SelectedIndex).Nom

        Dim IndexElts As Integer = MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(Me.cmb_Symbols.SelectedIndex).IndElts
        Dim lMixte As Boolean = MyProjet.Poutres(MyProjet.IndEnCours).Elements(IndexElts).lMixte
        Dim nEqDalle As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).Elements(IndexElts).nEqDalle
        Dim nEqEnrob As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).Elements(IndexElts).nEqEnrob

        If lMixte Then
            Me.lbl_Mixte.Text = "mixte"
            Me.lbl_NeqDalle.Visible = True
            Me.lbl_NeqDalle.Text = "Dalle : " & GetStringInUnit(nEqDalle, Enu_TypeVariable.SansType, 3, 3, False)
        Else
            Me.lbl_Mixte.Text = "non mixte"
            Me.lbl_NeqDalle.Visible = False
        End If

        If MyProjet.Poutres(MyProjet.IndEnCours).lEnrobage Then
            Me.lbl_NeqEnrob.Visible = True
            Me.lbl_NeqEnrob.Text = "Enrobage : " & GetStringInUnit(nEqEnrob, Enu_TypeVariable.SansType, 3, 3, False)
        Else
            Me.lbl_NeqEnrob.Visible = False
        End If

        Dim Chaine As String
        Dim lCalcul As Boolean = MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(Me.cmb_Symbols.SelectedIndex).lRunCalcul

        If lCalcul Then Chaine = "Oui" Else Chaine = "Non"
        Me.lbl_RCalcul.Text = Chaine

        PrepareFenetreResults(lCalcul)
        If lCalcul Then

            With MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(Me.cmb_Symbols.SelectedIndex)
                Me.txt_RZ1.Text = GetStringInUnit(.RZ(0), Enu_TypeVariable.Effort, 3, 3, False)
                Me.txt_RZ2.Text = GetStringInUnit(.RZ(1), Enu_TypeVariable.Effort, 3, 3, False)
                Me.txt_Fleche.Text = GetStringInUnit(.FlecheMax, Enu_TypeVariable.Dimension, 3, 3, False)

                Me.txt_Mmax.Text = GetStringInUnit(Me.tab_Mmax(Me.cmb_Symbols.SelectedIndex), Enu_TypeVariable.Moment, 3, 3, False)
                Me.txt_Mmin.Text = GetStringInUnit(Me.tab_Mmin(Me.cmb_Symbols.SelectedIndex), Enu_TypeVariable.Moment, 3, 3, False)
            End With

        End If
    End Sub

    Private Sub PrepareFenetreResults(lDispo As Boolean)

        Me.lbl_RZ1.Visible = lDispo
        Me.lbl_RZ2.Visible = lDispo
        Me.txt_RZ1.Visible = lDispo
        Me.txt_RZ2.Visible = lDispo
        Me.etq_UnitForce1.Visible = lDispo
        Me.etq_UnitForce2.Visible = lDispo
        Me.etq_UnitDim1.Visible = lDispo

        Me.lbl_Fleche.Visible = lDispo
        Me.txt_Fleche.Visible = lDispo

        Me.lbl_Mmax.Visible = lDispo
        Me.lbl_Mmin.Visible = lDispo
        Me.txt_Mmax.Visible = lDispo
        Me.etq_UnitM1.Visible = lDispo
        Me.txt_Mmin.Visible = lDispo
        Me.etq_UnitM2.Visible = lDispo

    End Sub

    Private Sub cmb_Symbols_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Symbols.SelectedIndexChanged
        MAJI_CasdeCharge()
        Me.img_Analyse.Invalidate()
    End Sub


#End Region

#Region " Dessins "

    Private Sub img_Analyse_Paint(sender As Object, e As PaintEventArgs) Handles img_Analyse.Paint
        DessineRDM(e.Graphics, Me.img_Analyse.ClientRectangle.Width, Me.img_Analyse.ClientRectangle.Height,
                   MyProjet.Poutres(MyProjet.IndEnCours), Me.cmb_Symbols.SelectedIndex,
                   lDessDeformee, lDessMoment, lDessEffortT, lDessNumeros, lDessInerties, lDessCharges, lDessEchLocal)
    End Sub

    Public Sub DessineRDM(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyPoutre As cls_Poutre,
                          iCas As Integer, lDef As Boolean, lMom As Boolean, lTranchant As Boolean, lNum As Boolean, lInertie As Boolean,
                          lChargement As Boolean, lEchLocal As Boolean, ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   11/08/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation des largeurs efficaces
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   MyPoutre    [E] :   Poutre à dessiner
        '   iCas        [E] :   Cas de charge à afficher
        '   lDef        [E] :   Indique si affichage des déformées
        '   lMom        [E] :   Indique si affichage du diagramme de moment
        '   lTranchant  [E] :   Indique si affichage du diagramme de tranchant
        '   lNum        [E] :   Indique si affichage des numéros de noeuds
        '   lInertie    [E] :   Indique si affichage des inerties
        '   lChargement [E] :   Indique si affichage du chargement
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '-----------------------------------------------------------------------------------------------

        '--> Declarations

        Dim MyParAff As Struc_Affichage
        Dim xMin, yMin, xMax, yMax As Double
        Dim dCar As Double = 0
        Const kADJUST As Decimal = 0.95
        Dim Longueur As Decimal = MyPoutre.LongueurTotale

        Dim EcartZ As Decimal = Longueur * pHi / pWi
        Dim DiaNode As Decimal = Longueur / 200
        Dim dApp As Decimal = Longueur / 50
        Dim kEch, kEchM As Decimal
        Dim lResult As Boolean = MyPoutre.ChargesA(iCas).lRunCalcul
        Const SigneM As Decimal = -1
        Const SigneV As Decimal = -1
        Dim MyFontNum As New Font("Arial", 7)
        Dim Chaine As String
        Dim MyPenB As New SolidBrush(Color.Gray)
        'Dim valMin, valMax As Decimal

        Dim ColorDef = Color.DarkOrange
        Dim ColorDiagM = Color.DarkRed
        Dim ColorDiagV = Color.DarkBlue

        Dim iNodeMax, iNodeMin As Integer

        Dim ColorPoutre As Color = Color.DarkGray

        Dim MyPenPoutre As New Pen(ColorPoutre)
        Dim MyPenSelect As New Pen(ColorSelect, 2)
        Dim MyPen As Pen

        '--> Initialisation

        If lBuild Then Exit Sub


        Dim MyBrushN As New SolidBrush(Color.White)
        Dim MyPenDef As New Pen(ColorDef)
        Dim MyPenM As New Pen(Color.Blue)

        xMin = 0 - dCar
        xMax = Longueur + dCar

        yMin = -EcartZ / 2
        yMax = +EcartZ / 2

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        dCar = 0.8 * EcartZ / 2

        'If SigneM = 1 Then
        iNodeMax = tab_iNodeMmax(iCas)
        iNodeMin = tab_iNodeMmin(iCas)
        'Else
        'iNodeMax = tab_iNodeMmin(iCas)
        'iNodeMin = tab_iNodeMmax(iCas)
        'End If

        '--> Affichage de la poutre

        AddLigne(myGr, MyPenPoutre, 0, 0, Longueur, 0, MyParAff)

        '--> Affichage des noeuds

        For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
            If iNode = iNodeMax Then
                MyPen = MyPenSelect
            Else
                MyPen = MyPenPoutre
            End If
            AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), 0, DiaNode, MyParAff, True, MyPen)
            If lDessNumeros Then
                Chaine = "N" & CStr(iNode + 1)
                AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(iNode), 0, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
            End If
        Next

        '--> Affichage des appuis

        'DessineAppui(myGr, MyPoutre.xPositionAppui(True, 1), dApp, MyParAff)
        'DessineAppui(myGr, MyPoutre.xPositionAppui(False, 1), dApp, MyParAff)
        Dim indAppuis() As Integer
        Dim NbApp As Integer
        Dim lEtais As Boolean = (iCas = MyPoutre.IndiceCasG1PP)
        MyPoutre.ExtraireIndiceNoeudsAppuis(lEtais, indAppuis, NbApp)

        For iApp As Integer = 0 To NbApp - 1
            DessineAppui(myGr, MyPoutre.Nodes.xGlobal(indAppuis(iApp)), dApp, MyParAff)
        Next

        '--> Déformée

        Dim fMin, fMax As Decimal
        Dim Uz As Decimal
        Dim xo, xe, yo, ye As Decimal

        'MyPoutre.ChargesA(iCas).EnveloppesFleche(fMin, fMax)

        If ((Not IsEqual(fMinG, 0)) Or (Not (IsEqual(fMinG, 0)))) And lResult And lDef Then

            ' kEch = EcartZ / (2 * Math.Max(Math.Abs(fMinG), fMaxG))
            kEch = CoefEchelleDessin(fMaxG, fMinG, tab_fMax(iCas), tab_fMin(iCas), EcartZ / 2, 10 ^ -9)

            For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 2
                xo = MyPoutre.Nodes.xGlobal(iNode)
                xe = MyPoutre.Nodes.xGlobal(iNode + 1)
                yo = MyPoutre.ChargesA(iCas).UZ(iNode) * kEch
                ye = MyPoutre.ChargesA(iCas).UZ(iNode + 1) * kEch
                AddLigne(myGr, MyPenDef, xo, yo, xe, ye, MyParAff)
            Next

            For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
                Uz = MyPoutre.ChargesA(iCas).UZ(iNode)
                AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), kEch * Uz, DiaNode, MyParAff, True, MyPenDef)
            Next

        End If

        '--> Représentation du chargement

        If lChargement Then
            DessineChargement(myGr, MyPoutre.ChargesA(iCas), MyPoutre.IndicePremiereTravee, MyPoutre.IndiceDerniereTravee, kEch, dCar,
                              MyPoutre.Nodes.xGlobal, MyPoutre.Nodes.nbNodes, MyParAff)
        End If

        '--> Inerties

        If lDessInerties Then
            DessineProp(myGr, MyPoutre, iCas, dCar, MyParAff)
        End If

        '--> Diagramme de Moments de flexion

        If lMom And lResult Then

            kEchM = CoefEchelleDessin(MmaxG, MminG, tab_Mmax(iCas), tab_Mmin(iCas), EcartZ / 2) * SigneM

            DessineDiagrammeRDM(myGr, MyPoutre, MyPoutre.ChargesA(iCas).MYY, kEchM, ColorDiagM, MyParAff)

        End If

        '--> Diagramme de efforts tranchants

        If lTranchant And lResult Then

            kEchM = CoefEchelleDessin(VmaxG, VminG, tab_Vmax(iCas), tab_Vmin(iCas), EcartZ / 2) * SigneV

            DessineDiagrammeRDM(myGr, MyPoutre, MyPoutre.ChargesA(iCas).VZ, kEchM, ColorDiagV, MyParAff)

        End If
    End Sub

    Private Function CoefEchelleDessin(RmaxG As Decimal, RminG As Decimal, RmaxL As Decimal, RminL As Decimal, dCar As Decimal, Optional Epsilon As Decimal = 0.001) As Decimal
        '-----------------------------------------------------------------------------------------------
        '   18/09/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation d'un diagramme moment ou effort tranchant
        '-----------------------------------------------------------------------------------------------
        '   RmaxG, RminG    [E] :   Valeurs min et max de la variable obtenues pour tous les cas de charge
        '   RmaxL, RminL    [E] :   Valeurs min et max de la variable obtenues pour le cas de charge traité
        '   myParAff        [E] :   Paramètre affichage
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim kEch As Decimal
        Dim valMin, valMax As Decimal

        '--> Traitement

        If lDessEchLocal Then
            valMin = RminL
            valMax = RmaxL
        Else
            valMin = RminG
            valMax = RmaxG
        End If

        If IsEqual(Math.Abs(valMin), 0, Epsilon) And (IsEqual(valMax, 0, Epsilon)) Then
            kEch = 1
        Else
            kEch = dCar / (Math.Max(Math.Abs(valMin), valMax))
        End If

        Return kEch
    End Function


    'Private Sub DessineDiagrammeRDM(myGr As Graphics, myPoutre As cls_Poutre,
    '                                Courbe(,) As Decimal, kEchC As Decimal, CouleurC As Color, myParAff As Struc_Affichage)
    '    '-----------------------------------------------------------------------------------------------
    '    '   18/09/23 :  Version 1.00
    '    '-----------------------------------------------------------------------------------------------
    '    '   Représentation d'un diagramme moment ou effort tranchant
    '    '-----------------------------------------------------------------------------------------------
    '    '   myGr        [E] :   Graphics dans lequel on dessine
    '    '   MyChargeA   [E] :   Cas de charge
    '    '   myParAff    [E] :   Paramètre affichage
    '    '-----------------------------------------------------------------------------------------------

    '    '--> Déclarations

    '    Dim xo, xe, yo, ye As Decimal
    '    Dim myPenC As New Pen(CouleurC, 1.5)

    '    '--> Affichage

    '    xo = 0
    '    xe = 0
    '    yo = 0
    '    ye = Courbe(0, 1) * kEchC
    '    If Not IsEqual(yo, ye) Then
    '        AddLigne(myGr, myPenC, xo, yo, xe, ye, myParAff)
    '    End If

    '    For iNode As Integer = 0 To myPoutre.Nodes.nbNodes - 2
    '        xo = myPoutre.Nodes.xGlobal(iNode)
    '        xe = myPoutre.Nodes.xGlobal(iNode + 1)
    '        yo = Courbe(iNode, 1) * kEchC
    '        ye = Courbe(iNode + 1, 0) * kEchC
    '        AddLigne(myGr, myPenC, xo, yo, xe, ye, myParAff)

    '        If iNode < myPoutre.Nodes.nbNodes - 2 Then
    '            yo = Courbe(iNode + 1, 1) * kEchC
    '            If Not IsEqual(yo, ye) Then
    '                AddLigne(myGr, myPenC, xe, yo, xe, ye, myParAff)
    '            End If
    '        End If

    '    Next

    '    xe = myPoutre.LongueurTotale
    '    yo = 0
    '    ye = Courbe(myPoutre.Nodes.nbNodes - 1, 0) * kEchC

    '    If Not IsEqual(yo, ye) Then
    '        AddLigne(myGr, myPenC, xe, yo, xe, ye, myParAff)
    '    End If

    'End Sub

    Private Sub DessineChargement(ByRef myGr As Graphics, MyChargeA As cls_CasDeCharge, iTravD As Integer, iTravF As Integer,
                                  kEchDef As Decimal, dCar As Decimal, xSec() As Decimal, NbSec As Integer, myParAff As Struc_Affichage)
        '-----------------------------------------------------------------------------------------------
        '   18/09/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation du chargement
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   MyChargeA   [E] :   Cas de charge
        '   iTravD      [E] :   Indice première travée
        '   iTravF      [E] :   Indice dernière travée
        '   kEchDef     [E] :   Facteur d'échelle pour les déformée
        '   dCar        [E] :   
        '   xSec        [E] :   Position des noeuds
        '   NbSec       [E] ;   Nombre de noeuds
        '   myParAff    [E] :   Paramètre affichage
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xPos, yPos As Decimal
        Dim dFleche As Decimal
        Dim ForceMax, FRepMax As Decimal
        Dim kEchF As Decimal
        Dim iTrav, j As Integer
        Dim kEchR As Decimal
        Dim CouleurC As Color = Color.DarkGreen
        Dim CouleurR As Color = Color.DimGray

        '--> Calcul des coefficients d'échelle

        ForceMax = MyChargeA.EffortPmax(iTravD, iTravF)
        If IsEqual(ForceMax, 0) Then
            kEchF = 1
        Else
            kEchF = dCar / ForceMax / 2
        End If

        FRepMax = MyChargeA.EffortRepMax(iTravD, iTravF)
        If IsEqual(FRepMax, 0) Then
            kEchR = 1
        Else
            kEchR = dCar / FRepMax / 2
        End If

        '--> Représentation des efforts

        For iTrav = iTravD To iTravF

            For j = 0 To MyChargeA.Forces(iTrav).Count - 1

                xPos = MyChargeA.Forces(iTrav)(j).xPosG
                yPos = 0
                dFleche = MyChargeA.Forces(iTrav)(j).Force * kEchF
                'DessinForcePonctuelle(myGr, xPos, yPos, dFleche, myParAff)
                AddFlecheForce(myGr, xPos, yPos, dFleche / 5, dFleche, 0, CouleurR, CouleurC, True, False, True, myParAff)
            Next

        Next

        '--> Représentation des moments

        For iTrav = iTravD To iTravF
            For j = 0 To MyChargeA.Moments(iTrav).Count - 1

                xPos = MyChargeA.Moments(iTrav)(j).xPosG
                yPos = 0
                '                dFleche = MyChargeA.Forces(iTrav)(j).Force * kFleche

                AddFlecheMoment(myGr, xPos, yPos, MyChargeA.Moments(iTrav)(j).Moment > 0, dCar / 5, CouleurR, CouleurC, True, myParAff)
            Next
        Next

        '--> Représentation des charges réparties

        For iTrav = iTravD To iTravF
            For j = 0 To MyChargeA.FReparties(iTrav).Count - 1
                DessineForceRep(myGr, MyChargeA.FReparties(iTrav)(j).xPosG, MyChargeA.FReparties(iTrav)(j).Force, kEchR, xSec, NbSec, CouleurC, myParAff)
            Next
        Next

    End Sub

    Private Sub DessineForceRep(ByRef myGr As Graphics, xPos() As Decimal, ForceR() As Decimal, kEch As Decimal,
                                xSec() As Decimal, NbSec As Integer, CouleurC As Color, myParAff As Struc_Affichage)
        '-----------------------------------------------------------------------------------------------
        '   18/09/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation d'une force répartie
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   xPos        [E] :   Position des extrémités du chargement
        '   ForceR      [E] :   Force aux extrémités du chargement (par Unité de L)
        '   xSec        [E] :   Position des noeuds
        '   NbSec       [E] ;   Nombre de noeuds
        '   myParAff    [E] :   Paramètre affichage
        '-----------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xo, yo As Decimal
        Dim xe, ye As Decimal
        Dim myPen As New Pen(CouleurC, 0.75)
        Const EPSX As Decimal = 0.5
        Dim qR As Decimal

        '--> Affichage

        xo = xPos(0)
        xe = xo
        yo = 0
        ye = kEch * ForceR(0)

        AddFleche(myGr, myPen, xo, yo, xe, ye, myParAff, True, False)

        xo = xPos(1)
        xe = xo
        yo = 0
        ye = kEch * ForceR(1)

        AddFleche(myGr, myPen, xo, yo, xe, ye, myParAff, True, False)

        xo = xPos(0)
        xe = xPos(1)
        yo = kEch * ForceR(0)
        ye = kEch * ForceR(1)

        AddLigne(myGr, myPen, xo, yo, xe, ye, myParAff)

        '--> Tracé sur les noeuds

        For i As Integer = 0 To NbSec - 1
            If IsGreater(xSec(i), xPos(0), EPSX) And IsSmaller(xSec(i), xPos(1), EPSX) Then
                qR = ForceR(0) + (ForceR(1) - ForceR(0)) / (xPos(1) - xPos(0)) * (xSec(i) - xPos(0))
                AddFleche(myGr, myPen, xSec(i), 0, xSec(i), qR * kEch, myParAff, True, False)
            End If
        Next

    End Sub

    Private Sub DessineProp(ByRef myGr As Graphics, MyPoutre As cls_Poutre, iCas As Integer,
                            dCar As Decimal, myParAff As Struc_Affichage)
        '-----------------------------------------------------------------------------------------------
        '   18/08/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation du chargement
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   MyChargeA   [E] :   Cas de charge
        '   myParAff    [E] :   Paramètre affichage
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iElts As Integer
        Dim InertieMax As Decimal
        Dim i As Integer
        Dim kIne As Decimal
        Dim xo, yo As Decimal
        Dim xe, ye As Decimal
        Const kUnitI As Decimal = 10 ^ 8
        Dim MyFontNum As New Font("Arial", 7)
        Dim Chaine As String
        Dim MyPenB As New SolidBrush(Color.Gray)

        '--> Initialisation

        iElts = MyPoutre.ChargesA(iCas).IndElts

        InertieMax = MyPoutre.Elements(iElts).InertieY(0) * kUnitI

        For i = 1 To MyPoutre.Nodes.nbNodes - 2
            InertieMax = Math.Max(InertieMax, MyPoutre.Elements(iElts).InertieY(i) * kUnitI)
        Next

        If IsEqual(InertieMax, 0) Then kIne = 1 Else kIne = dCar / InertieMax

        '--> Représentation de l'inertie

        For i = 0 To MyPoutre.Nodes.nbNodes - 2

            xo = MyPoutre.Nodes.xGlobal(i)
            xe = MyPoutre.Nodes.xGlobal(i + 1)
            yo = MyPoutre.Elements(iElts).InertieY(i) * kIne * kUnitI
            ye = yo
            AddLigne(myGr, xo, yo, xe, ye, myParAff)

        Next

        '>> Valeurs

        '# Extrémités

        i = 0
        Chaine = GetStringInUnit(MyPoutre.Elements(iElts).InertieY(i), Enu_TypeVariable.InertieCM4, 4, 0, False)
        yo = MyPoutre.Elements(iElts).InertieY(i) * kIne * kUnitI
        AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(i), yo, myParAff, HorizontalAlignment.Right, VerticalAlignement.Top)

        i = MyPoutre.Nodes.nbNodes - 2
        Chaine = GetStringInUnit(MyPoutre.Elements(iElts).InertieY(i), Enu_TypeVariable.InertieCM4, 4, 0, False)
        yo = MyPoutre.Elements(iElts).InertieY(i) * kIne * kUnitI
        AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(i + 1), yo, myParAff, HorizontalAlignment.Left, VerticalAlignement.Top)

        '# Travée

        If MyPoutre.lTraveeConsoleGauche Then

            i = MyPoutre.Nodes.iNodeExtTrav(1, 0)
            Chaine = GetStringInUnit(MyPoutre.Elements(iElts).InertieY(i), Enu_TypeVariable.InertieCM4, 4, 0, False)
            yo = MyPoutre.Elements(iElts).InertieY(i) * kIne * kUnitI
            AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(i), yo, myParAff, HorizontalAlignment.Right, VerticalAlignement.Top)

        End If

        If MyPoutre.lTraveeConsoleDroite Then

            i = MyPoutre.Nodes.iNodeExtTrav(1, 1)
            Chaine = GetStringInUnit(MyPoutre.Elements(iElts).InertieY(i - 1), Enu_TypeVariable.InertieCM4, 4, 0, False)
            yo = MyPoutre.Elements(iElts).InertieY(i - 1) * kIne * kUnitI
            AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(i), yo, myParAff, HorizontalAlignment.Left, VerticalAlignement.Top)

        End If

        '# Mi travée

        i = (MyPoutre.Nodes.iNodeExtTrav(1, 0) + MyPoutre.Nodes.iNodeExtTrav(1, 1)) / 2
        Chaine = GetStringInUnit(MyPoutre.Elements(iElts).InertieY(i), Enu_TypeVariable.InertieCM4, 4, 0, False)
        yo = MyPoutre.Elements(iElts).InertieY(i) * kIne * kUnitI
        AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(i), yo, myParAff, HorizontalAlignment.Right, VerticalAlignement.Top)

    End Sub

    Private Sub AddFlecheForce(ByVal MyGr As Graphics, ByVal xForce As Double, ByVal yForce As Double,
                               ByVal dCarac As Double, ByVal hFleche As Double, ByVal Alpha As Double,
                               ByVal Couleur As Color, CouleurContour As Color, ByVal lRemplissage As Boolean,
                               ByVal lSelected As Boolean, ByVal lPositif As Boolean, myParAff As Struc_Affichage)
        '----------------------------------------------------------------------------------------------------------------
        '   19/09/23 :  Création - POM - V1.0
        '----------------------------------------------------------------------------------------------------------------
        '   Dessin d'un flèche représentant une force ponctuelle
        '----------------------------------------------------------------------------------------------------------------
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   xForce,yForce   [E] :   Position de la flèche
        '   dCarac          [E] :   Dimension caracteristique
        '   hFleche         [E] :   Hauteur de la flèche
        '   Alpha           [E] :   Inclinaison de la flèche / axe vertical
        '   Couleur         [E] :   Couleur d'affichage (remplissage)
        '   lRemplissage    [E] :   Indique si remplissage de la flèche
        '   lSelected       [E] :   Indique si fleche selectionnee on non
        '   lPositif        [E] :   Indique le sens de representation de l'effort
        '----------------------------------------------------------------------------------------------------------------

        '--> Déclarations 

        Dim myBrush As LinearGradientBrush
        Dim DeltaAlpha As Double = 0
        Dim DeltaY As Double = 0
        Dim DeltaX As Double
        If Not lPositif Then
            DeltaAlpha = Math.PI
            DeltaY = hFleche * (Math.Cos(Alpha))
            DeltaX = hFleche * (Math.Sin(Alpha))
        End If
        Dim CosA As Double = (Math.Cos(Alpha + DeltaAlpha))
        Dim SinA As Double = (Math.Sin(Alpha + DeltaAlpha))

        '--> Préparation du pinceau

        If lSelected Then
            myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xForce + DeltaX + dCarac * CosA), YEcran(myParAff, yForce + DeltaY + dCarac * SinA)),
                                              New PointF(XEcran(myParAff, xForce + DeltaX - dCarac * CosA + hFleche * SinA), YEcran(myParAff, yForce + DeltaY + hFleche * CosA + dCarac * SinA)), Color.Red, ColorDeg)
        Else
            myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xForce + DeltaX - dCarac * CosA + hFleche * SinA), YEcran(myParAff, yForce + DeltaY + hFleche * CosA + dCarac * SinA)),
                                              New PointF(XEcran(myParAff, xForce + DeltaX + dCarac * CosA), YEcran(myParAff, yForce + DeltaY + dCarac * SinA)), ColorDeg, Couleur)
        End If

        '--> Dessin de la flèche

        AddFlecheGeneral(MyGr, myBrush, xForce + DeltaX, yForce + DeltaY, hFleche, dCarac, dCarac, 2 * dCarac, Alpha + DeltaAlpha, myParAff, True, lRemplissage, False, CouleurContour)

        myBrush.Dispose()
    End Sub

    Private Sub AddFlecheMomentGeneral(ByVal MyGr As Graphics,
                                       ByVal xPos As Double, ByVal yPos As Double, ByVal Angle As Double,
                                       ByVal lGauche As Boolean, ByVal lPositif As Boolean,
                                       ByVal dCarac As Double, ByVal Couleur As Color, myParAff As Struc_Affichage)
        '----------------------------------------------------------------------------------------------------------------
        '   19/09/23 :  Création - POM - V1.0
        '----------------------------------------------------------------------------------------------------------------
        '   Dessin d'un flèche représentant un moment aux extrémités
        '----------------------------------------------------------------------------------------------------------------
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   xPos,yPos       [E] :   Position de l'extremite de la poutre concernée
        '   Angle           [E] :   Inclinaison supplémentaire de la flèche
        '   lGauche         [E] :   Indique si extremité gauche ou droite
        '   lPositif        [E] :   Inidique si Moment positif
        '   dCarac          [E] :   Dimension caractéristique
        '   Couleur         [E] :   Couleur de remplissage de la flèche
        '----------------------------------------------------------------------------------------------------------------

        Dim myBrush As LinearGradientBrush
        Dim xCentre, yCentre As Double
        Dim AlphaO, AlphaE As Double

        yCentre = yPos
        If lGauche Then
            xCentre = xPos - dCarac * 0.75
            If lPositif Then
                AlphaE = 3 * Math.PI / 4 + Angle
                AlphaO = 5 * Math.PI / 4 + Angle
                myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre + dCarac / 2)),
                                                  New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre - dCarac / 2 - dCarac / 3)), ColorDeg, Couleur)
            Else
                AlphaE = 5 * Math.PI / 4 + Angle
                AlphaO = 3 * Math.PI / 4 + Angle
                myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre - dCarac / 2)),
                                                  New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre + dCarac / 2 + dCarac / 3)), ColorDeg, Couleur)
            End If
        Else
            xCentre = xPos + dCarac * 0.75
            If lPositif Then
                AlphaE = -Math.PI / 4 - Angle
                AlphaO = Math.PI / 4 - Angle
                myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre + dCarac / 2 + dCarac / 3)),
                                                  New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre - dCarac / 2)), Couleur, ColorDeg)
            Else
                AlphaE = Math.PI / 4 - Angle
                AlphaO = -Math.PI / 4 - Angle
                myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre - dCarac / 2 - dCarac / 3)),
                                                  New PointF(XEcran(myParAff, xCentre), YEcran(myParAff, yCentre + dCarac / 2)), Couleur, ColorDeg)
            End If
        End If

        AddFlecheCourbe(MyGr, myBrush, xCentre, yCentre, AlphaO, AlphaE, dCarac / 2, dCarac / 4, dCarac / 3, dCarac / 2, myParAff, True, True, Color.Black)

        myBrush.Dispose()

    End Sub

    Private Sub AddFlecheMoment(ByVal MyGr As Graphics, ByVal xPos As Double, ByVal yRef As Double,
                                ByVal lPositif As Boolean,
                                ByVal dCarac As Double, ByVal Couleur As Color, CouleurContour As Color, lRemplissage As Boolean, myParAff As Struc_Affichage)
        '----------------------------------------------------------------------------------------------------------------
        '   19/09/23 :  Création - POM - V1.0
        '----------------------------------------------------------------------------------------------------------------
        '   Dessin d'un flèche représentant un moment aux extrémités
        '----------------------------------------------------------------------------------------------------------------
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   xPos            [E] :   Position de la fleche
        '   yRef            [E] :   Position y de la fleche
        '   lPositif        [E] :   Indique si moment positif
        '   dCarac          [E] :   Dimension caracteristique
        '   Couleur         [E] :   Couleur de remplissage de la flèche
        '   CouleurContour  [E] :   Couleur de contour de la flèche
        '   lRemplissage    [E] :   
        '   myParAff        [E] :
        '----------------------------------------------------------------------------------------------------------------

        Dim myBrush As LinearGradientBrush
        Dim xRef As Double
        Dim AlphaO, AlphaE As Double

        'If lGauche Then
        'xRef = -dCarac * 0.75
        If lPositif Then
            AlphaE = 3 * Math.PI / 2 'Math.PI / 4
            AlphaO = 5 * Math.PI / 2 '3 * Math.PI / 4
            myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xRef), YEcran(myParAff, yRef + dCarac / 2)),
                                                  New PointF(XEcran(myParAff, xRef), YEcran(myParAff, yRef - dCarac / 2 - dCarac / 3)), ColorDeg, Couleur)
        Else
            AlphaE = -Math.PI / 2
            AlphaO = -3 * Math.PI / 2
            myBrush = New LinearGradientBrush(New PointF(XEcran(myParAff, xRef), YEcran(myParAff, yRef - dCarac / 2)),
                                                  New PointF(XEcran(myParAff, xRef), YEcran(myParAff, yRef + dCarac / 2 + dCarac / 3)), ColorDeg, Couleur)
        End If

        AddFlecheCourbe(MyGr, myBrush, xPos, yRef, AlphaO, AlphaE, dCarac / 2, dCarac / 4, dCarac / 3, dCarac / 2, myParAff, True, lRemplissage, CouleurContour)

        myBrush.Dispose()
    End Sub

    Sub AddFlecheCourbe(ByRef MyGr As Graphics, ByVal MyBrush As Brush,
                        ByVal xP As Double, ByVal yP As Double,
                        ByVal AlphaO As Double, ByVal AlphaE As Double,
                        ByVal RayInt As Double, ByVal Epb As Double,
                        ByVal HPointe As Double, ByVal EpPointe As Double,
                        ByRef ParAff As Struc_Affichage,
                        ByVal lContour As Boolean, ByVal lRemplissage As Boolean, CouleurContour As Color)
        '-------------------------------------------------------------------------------------------------
        '   13/02/08 :  Création - Version 1.00
        '-------------------------------------------------------------------------------------------------
        '
        '   Ajout d'une flèche courbe pour représenter un moemnt
        '
        '-------------------------------------------------------------------------------------------------
        '
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   MyBrush         [E] :   Pinceau pour le remplissage de la flèche
        '   xP, yP          [E] :   Coordonnées du centre
        '   Alpha0          [E] :   Angle origine
        '   AlphaE          [E] :   Angle extremite
        '   RayInt          [E] :   Rayon intérieur 
        '   Epb             [E] :   Largeur de la jambe de la flèche
        '   HPoint          [E] :   Hauteur de l'extremite de la flèche
        '   EpPointe        [E] :   Largeur de l'extremite de la flèche
        '   ParAff          [E] :   Paramètres de l'affichage
        '   lContour        [E] :   Indique si la routine appelante requiert le tracé du contour de la flèche
        '   lRemplissage    [E] :   Indique si remplissage
        '
        '-------------------------------------------------------------------------------------------------

        'LA POINTE DE LA FLECHE EST DU COTE ORIGINE

        Const nDiv As Integer = 8
        Dim DeltaA As Double = (AlphaE - AlphaO) / (nDiv - 1)
        Dim RayExt As Double = RayInt + Epb

        Dim x1, x2, y1, y2 As Double
        Dim x12, y12 As Double
        Dim Norme As Double

        Dim PtsFleche(2 * nDiv + 2) As Point

        For i As Integer = 0 To nDiv - 1
            PtsFleche(i).X = CInt(XEcran(ParAff, xP + RayInt * (Math.Cos(AlphaO + i * DeltaA))))
            PtsFleche(i).Y = CInt(YEcran(ParAff, yP + RayInt * (Math.Sin(AlphaO + i * DeltaA))))
            PtsFleche(i + nDiv).X = CInt(XEcran(ParAff, xP + RayExt * (Math.Cos(AlphaE - i * DeltaA))))
            PtsFleche(i + nDiv).Y = CInt(YEcran(ParAff, yP + RayExt * (Math.Sin(AlphaE - i * DeltaA))))
        Next

        Dim d As Double = (EpPointe - Epb) / 2

        x1 = XEcran(ParAff, xP + (RayExt + d) * (Math.Cos(AlphaO)))
        x2 = XEcran(ParAff, xP + (RayInt - d) * (Math.Cos(AlphaO)))
        y1 = YEcran(ParAff, yP + (RayExt + d) * (Math.Sin(AlphaO)))
        y2 = YEcran(ParAff, yP + (RayInt - d) * (Math.Sin(AlphaO)))

        PtsFleche(2 * nDiv).X = CInt(x1)
        PtsFleche(2 * nDiv).Y = CInt(y1)
        PtsFleche(2 * nDiv + 2).X = CInt(x2)
        PtsFleche(2 * nDiv + 2).Y = CInt(y2)

        x12 = x2 - x1
        y12 = y2 - y1
        Norme = (Math.Sqrt(x12 ^ 2 + y12 ^ 2))
        Dim kSigne As Double = 1
        If AlphaE < AlphaO Then kSigne = -1

        PtsFleche(2 * nDiv + 1).X = CInt(x1 + x12 / 2 + kSigne * y12 / Norme * HPointe * ParAff.CRed)
        PtsFleche(2 * nDiv + 1).Y = CInt(y1 + y12 / 2 - kSigne * x12 / Norme * HPointe * ParAff.CRed)

        If lRemplissage Then MyGr.FillPolygon(MyBrush, PtsFleche)

        If lContour Then MyGr.DrawPolygon(New Pen(CouleurContour), PtsFleche)

    End Sub

    Sub AddFlecheGeneral(ByRef MyGr As Graphics, ByVal MyBrush As Brush,
                         ByVal xP As Single, ByVal yP As Single,
                         ByVal Ht As Single, ByVal Epb As Single,
                         ByVal HPointe As Single, ByVal EpPointe As Single,
                         ByVal AlphaF As Single, ByRef ParAff As Struc_Affichage,
                         ByVal lContour As Boolean, ByVal lRemplissage As Boolean, ByVal lAxe As Boolean, CouleurContour As Color)
        '-------------------------------------------------------------------------------------------------
        '
        '   10/02/08 :  Création - Version 1.00
        '
        '-------------------------------------------------------------------------------------------------
        '
        '   Ajout d'une flèche selon un vecteur directeur
        '
        '-------------------------------------------------------------------------------------------------
        '
        '   MyGr            [E] :   Graphics dans lequel on dessine
        '   MyBrush         [E] :   Pinceau pour le remplissage de la flèche
        '   xP, yP          [E] :   Coordonnées de la pointe de la flèche
        '   Ht              [E] :   Hauteur de la flèche (coordonnée poutre)
        '   Epb             [E] :   Largeur de la jambe de la flèche
        '   HPoint          [E] :   Hauteur de l'extremite de la flèche
        '   EpPointe        [E] :   Largeur de l'extremite de la flèche
        '   AlphaF          [E] :   Orientation de la fleche (angle de rotation trigo / axe vertical)
        '   ParAff          [E] :   Paramètres de l'affichage
        '   lContour        [E] :   Indique si la routine appelante requiert le tracé du contour de la flèche
        '   lRemplissage    [E] :   Indique si remplissage
        '   lAxe            [E] :   Indique si le tracé de l'axe est requis !!!
        '
        '-------------------------------------------------------------------------------------------------

        Dim PtsFleche(6) As Point

        PtsFleche(0).X = CInt(XEcran(ParAff, xP))
        PtsFleche(0).Y = CInt(YEcran(ParAff, yP))
        PtsFleche(1).X = CInt(XEcran(ParAff, xP + EpPointe / 2))
        PtsFleche(1).Y = CInt(YEcran(ParAff, yP + HPointe))
        PtsFleche(2).X = CInt(XEcran(ParAff, xP + Epb / 2))
        PtsFleche(2).Y = CInt(YEcran(ParAff, yP + HPointe))
        PtsFleche(3).X = CInt(XEcran(ParAff, xP + Epb / 2))
        PtsFleche(3).Y = CInt(YEcran(ParAff, yP + Ht))
        PtsFleche(4).X = CInt(XEcran(ParAff, xP - Epb / 2))
        PtsFleche(4).Y = CInt(YEcran(ParAff, yP + Ht))
        PtsFleche(5).X = CInt(XEcran(ParAff, xP - Epb / 2))
        PtsFleche(5).Y = CInt(YEcran(ParAff, yP + HPointe))
        PtsFleche(6).X = CInt(XEcran(ParAff, xP - EpPointe / 2))
        PtsFleche(6).Y = CInt(YEcran(ParAff, yP + HPointe))

        If AlphaF <> 0 Then
            Dim CosinusA As Single = CSng(Math.Cos(AlphaF))
            Dim SinusA As Single = CSng(Math.Sin(AlphaF))
            Dim xNew, yNew As Single
            For i As Integer = 1 To 6
                xNew = PtsFleche(0).X + (PtsFleche(i).X - PtsFleche(0).X) * CosinusA - (PtsFleche(i).Y - PtsFleche(0).Y) * SinusA
                yNew = PtsFleche(0).Y + (PtsFleche(i).X - PtsFleche(0).X) * SinusA + (PtsFleche(i).Y - PtsFleche(0).Y) * CosinusA
                PtsFleche(i).X = CInt(xNew)
                PtsFleche(i).Y = CInt(yNew)
            Next
        End If

        If lRemplissage Then MyGr.FillPolygon(MyBrush, PtsFleche)

        If lContour Then MyGr.DrawPolygon(New Pen(CouleurContour), PtsFleche)

        Dim xo, yo, xe, ye As Single

        If lAxe Then
            xo = XEcran(ParAff, xP)
            yo = YEcran(ParAff, yP)
            xe = xo
            ye = YEcran(ParAff, yP + Ht)
            MyGr.DrawLine(Pens.Red, xo, yo, xe, ye)
        End If

    End Sub

#End Region

#Region " Evènements "

    Private Sub chk_Fleches_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Fleches.CheckedChanged
        If lBuild Then Exit Sub

        lDessDeformee = Me.chk_Fleches.Checked

        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub chk_Moment_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Moment.CheckedChanged
        If lBuild Then Exit Sub

        lDessMoment = Me.chk_Moment.Checked

        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub chk_EffortTranchant_CheckedChanged(sender As Object, e As EventArgs) Handles chk_EffortTranchant.CheckedChanged
        If lBuild Then Exit Sub

        lDessEffortT = Me.chk_EffortTranchant.Checked

        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub chk_Numerotation_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Numerotation.CheckedChanged
        If lBuild Then Exit Sub

        lDessNumeros = Me.chk_Numerotation.Checked

        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub chk_Inerties_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Inerties.CheckedChanged
        If lBuild Then Exit Sub

        lDessInerties = Me.chk_Inerties.Checked

        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub chk_Chargement_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Chargement.CheckedChanged
        If lBuild Then Exit Sub

        lDessCharges = Me.chk_Chargement.Checked

        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub btn_EditModel_Click(sender As Object, e As EventArgs) Handles btn_EditModel.Click

    End Sub

    Private Sub chk_LocalEchelle_CheckedChanged(sender As Object, e As EventArgs) Handles chk_LocalEchelle.CheckedChanged
        If lBuild Then Exit Sub

        lDessEchLocal = Me.chk_LocalEchelle.Checked

        Me.img_Analyse.Invalidate()
    End Sub


#End Region

End Class