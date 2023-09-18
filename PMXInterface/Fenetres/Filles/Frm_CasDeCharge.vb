Imports PMXMoteur2

Public Class Frm_CasDeCharge

#Region " Variables "

    Dim lBuild As Boolean

    Dim tab_fMin() As Decimal
    Dim tab_fMax() As Decimal
    Dim fMaxG As Decimal
    Dim fMinG As Decimal

    Dim tab_Mmin() As Decimal
    Dim tab_Mmax() As Decimal
    Dim MmaxG As Decimal
    Dim MminG As Decimal

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_CasDeCharge_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionLangues()
        GestionStyle()
        GestionUnites()
        'InitialiseVariablesLocales()

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

    Private Sub AfficheCasdeCharge()

        MyProjet.Poutres(MyProjet.IndEnCours).InitialiseCalculs()
        MyProjet.Poutres(MyProjet.IndEnCours).CalculMNVInternes()
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

        For i As Integer = 0 To NbC - 1
            MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i).EnveloppesFleche(tab_fMax(i), tab_fMin(i))
            If i = 0 Then
                fMaxG = tab_fMax(i)
                fMinG = tab_fMin(i)
            Else
                fMaxG = Math.Max(fMaxG, tab_fMax(i))
                fMinG = Math.Min(fMinG, tab_fMin(i))
            End If

            MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i).EnveloppesMoments(tab_Mmax(i), tab_Mmin(i))
            If i = 0 Then
                MmaxG = tab_Mmax(i)
                MminG = tab_Mmin(i)
            Else
                MmaxG = Math.Max(MmaxG, tab_Mmax(i))
                MminG = Math.Min(MminG, tab_Mmin(i))
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
        Dim nEqDalle As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).Elements(IndexElts).nEqC
        Dim nEqEnrob As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).Elements(IndexElts).nEqEC

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
                   MyProjet.Poutres(MyProjet.IndEnCours), Me.cmb_Symbols.SelectedIndex)
    End Sub

    Public Sub DessineRDM(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyPoutre As cls_Poutre,
                          iCas As Integer, ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   11/08/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation des largeurs efficaces
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   MyPoutre    [E] :   Poutre à dessiner
        '   iCas        [E] :   Cas de charge à afficher
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

        '--> Initialisation

        If lBuild Then Exit Sub
        Dim MyBrushN As New SolidBrush(Color.White)
        Dim MyPenDef As New Pen(Color.DarkRed)
        Dim MyPenM As New Pen(Color.Blue)

        xMin = 0 - dCar
        xMax = Longueur + dCar

        yMin = -EcartZ / 2
        yMax = +EcartZ / 2

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        dCar = 0.8 * EcartZ / 2

        '--> Affichage de la poutre

        AddLigne(myGr, 0, 0, Longueur, 0, MyParAff)

        '--> Affichage des noeuds

        For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
            AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), 0, dianode, MyParAff, True)
        Next

        '--> Affichage des appuis

        DessineAppui(myGr, MyPoutre.xPositionAppui(True, 1), dApp, MyParAff)
        DessineAppui(myGr, MyPoutre.xPositionAppui(False, 1), dApp, MyParAff)

        '--> Déformée

        Dim fMin, fMax As Decimal
        Dim Uz As Decimal
        Dim xo, xe, yo, ye As Decimal

        'MyPoutre.ChargesA(iCas).EnveloppesFleche(fMin, fMax)

        If ((Not IsEqual(fMinG, 0)) Or (Not (IsEqual(fMinG, 0)))) And lResult Then

            kEch = EcartZ / (2 * Math.Max(Math.Abs(fMinG), fMaxG))

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

        DessineChargement(myGr, MyPoutre.ChargesA(iCas), MyPoutre.IndicePremiereTravee, MyPoutre.IndiceDerniereTravee, kEch, dCar, MyParAff)

        '--> Inerties

        DessineProp(myGr, MyPoutre, iCas, dCar, MyParAff)

        '--> Diagramme de Moments de flexion

        If lResult And ((Not IsEqual(Math.Abs(MminG), 0)) Or (Not (IsEqual(MmaxG, 0)))) Then

            kEchM = EcartZ / (2 * Math.Max(Math.Abs(MminG), MmaxG)) * SigneM

            xo = 0
            xe = 0
            yo = 0
            ye = MyPoutre.ChargesA(iCas).MYY(0, 1) * kEchM
            If Not IsEqual(yo, ye) Then
                AddLigne(myGr, MyPenM, xo, yo, xe, ye, MyParAff)
            End If

            For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 2
                xo = MyPoutre.Nodes.xGlobal(iNode)
                xe = MyPoutre.Nodes.xGlobal(iNode + 1)
                yo = MyPoutre.ChargesA(iCas).MYY(iNode, 1) * kEchM
                ye = MyPoutre.ChargesA(iCas).MYY(iNode + 1, 0) * kEchM
                AddLigne(myGr, MyPenM, xo, yo, xe, ye, MyParAff)

                If iNode < MyPoutre.Nodes.nbNodes - 2 Then
                    yo = MyPoutre.ChargesA(iCas).MYY(iNode + 1, 1) * kEchM
                    If Not IsEqual(yo, ye) Then
                        AddLigne(myGr, MyPenM, xe, yo, xe, ye, MyParAff)
                    End If
                End If

            Next

            xe = MyPoutre.LongueurTotale
            yo = 0
            ye = MyPoutre.ChargesA(iCas).MYY(MyPoutre.Nodes.nbNodes - 1, 0) * kEchM

            If Not IsEqual(yo, ye) Then
                AddLigne(myGr, MyPenM, xe, yo, xe, ye, MyParAff)
            End If

        End If

    End Sub

    Private Sub DessineChargement(ByRef myGr As Graphics, MyChargeA As cls_CasDeCharge, iTravD As Integer, iTravF As Integer,
                                  kEchDef As Decimal, dCar As Decimal, myParAff As Struc_Affichage)
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

        Dim xPos, yPos As Decimal
        Dim dFleche As Decimal
        Dim fMax As Decimal
        Dim kFleche As Decimal

        '--> Calcul des coefficients d'échelle

        fMax = MyChargeA.EffortPmax(iTravD, iTravF)
        If IsEqual(fMax, 0) Then
            kFleche = 1
        Else
            kFleche = dCar / fMax / 2
        End If

        '--> Représentation des efforts

        For iTrav As Integer = iTravD To iTravF

            For j As Integer = 0 To MyChargeA.Forces(iTrav).Count - 1

                xPos = MyChargeA.Forces(iTrav)(j).xPosG
                yPos = 0
                dFleche = MyChargeA.Forces(iTrav)(j).Force * kFleche
                DessinForcePonctuelle(myGr, xPos, yPos, dFleche, myParAff)

            Next

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

        '--> Initialisation

        iElts = MyPoutre.ChargesA(iCas).IndElts

        InertieMax = MyPoutre.Elements(iElts).InertieY(0) * kunitI

        For i = 1 To MyPoutre.Nodes.nbNodes - 2
            InertieMax = Math.Max(InertieMax, MyPoutre.Elements(iElts).InertieY(i) * kunitI)
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

    End Sub


#End Region


End Class