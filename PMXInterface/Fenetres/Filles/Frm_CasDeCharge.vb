Imports PMXMoteur2

Public Class Frm_CasDeCharge


#Region " Variables "

    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_CasDeCharge_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionLangues()
        GestionStyle()
        GestionUnites()

        AfficheCasdeCharge()

        lBuild = False
    End Sub

    Private Sub GestionLangues()

        Me.Text = "Load cases"
        Me.lbl_CasDeCharges.Text = "Load cases"
        Me.btn_Annuler.Text = "Cancel"
        Me.btn_OK.Text = "Close"

        Me.lbl_Case.Text = "Case"
        Me.lbl_Etat.Text = "Etat"
        Me.lbl_RunCalcul.Text = "Calcul effectué ?"

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitDim1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitForce1.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)
        Me.etq_UnitForce2.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)
    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_CasDeCharges.BackColor = CouleurBackBandeaux
        Me.lbl_CasDeCharges.ForeColor = CouleurForeBandeaux

        Me.img_Analyse.Dock = DockStyle.Fill

    End Sub

    Private Sub AfficheCasdeCharge()

        InitialiseOptionsCalculPoutre(MyProjet.Poutres(MyProjet.IndEnCours))
        MyProjet.Poutres(MyProjet.IndEnCours).InitialiseCalculs()
        MyProjet.Poutres(MyProjet.IndEnCours).CalculMNVInternes()

        Me.cmb_Symbols.Items.Clear()

        For i As Integer = 0 To MyProjet.Poutres(MyProjet.IndEnCours).ChargesA.Count - 1
            Me.cmb_Symbols.Items.Add(MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i).Symbol)
        Next

        Me.cmb_Symbols.SelectedIndex = 0

        MAJI_CasdeCharge()

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

    End Sub

    Private Sub cmb_Symbols_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Symbols.SelectedIndexChanged
        MAJI_CasdeCharge()
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
        Dim kEch As Decimal

        '--> Initialisation

        Dim MyBrushN As New SolidBrush(Color.White)
        Dim MyPenDef As New Pen(Color.DarkRed)

        xMin = 0 - dCar
        xMax = Longueur + dCar

        yMin = -EcartZ / 2
        yMax = +EcartZ / 2

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        '--> Affichage de la poutre

        AddLigne(myGr, 0, 0, Longueur, 0, MyParAff)

        '--> Affichage des noeuds

        For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
            AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), 0, dianode, MyParAff, True)
        Next

        '--> Affichage des appuis

        DessineAppui(myGr, MyPoutre.xPositionAppui(True, 1), Dapp, MyParAff)

        '--> Déformée

        Dim fMin, fMax As Decimal
        Dim Uz As Decimal
        Dim xo, xe, yo, ye As Decimal

        MyPoutre.ChargesA(iCas).EnveloppesFleche(fMin, fMax)
        kEch = EcartZ / (2 * Math.Max(Math.Abs(fMin), fMax))

        For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 2
            xo = MyPoutre.Nodes.xGlobal(iNode)
            xe = MyPoutre.Nodes.xGlobal(iNode + 1)
            yo = MyPoutre.ChargesA(iCas).UZ(iNode) * kEch
            ye = MyPoutre.ChargesA(iCas).UZ(iNode + 1) * kEch
            AddLigne(myGr, MyPenDef, xo, yo, xe, ye, MyParAff)
        Next

        For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
            Uz = MyPoutre.ChargesA(iCas).UZ(iNode)
            AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), kEch * Uz, DiaNode, MyParAff, True, mypendef)
        Next

    End Sub

#End Region


End Class