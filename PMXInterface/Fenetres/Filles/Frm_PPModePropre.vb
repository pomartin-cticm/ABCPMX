Imports System.Net.NetworkInformation
Imports PMXMoteur2

Public Class Frm_PPModePropre

#Region " Variables "

    Dim lBuild As Boolean

    Dim lDefinieQ(2) As Boolean
    Dim IndiceQ As New List(Of Integer)
    Dim LabelQ() As String = {"Q1", "Q2"}

    Dim lDessNumeros As Boolean = True

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_PPModePropre_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionLangues()
        GestionStyle()

        PrepareFenetre(MyProjet.Poutres(MyProjet.IndEnCours))
        AfficheCalculModal(MyProjet.Poutres(MyProjet.IndEnCours))

        lBuild = False
    End Sub

    Private Sub GestionLangues()

        Me.Text = "Mode propre"
        Me.lbl_ModePropre.Text = "Eigen mode values"

        Me.lbl_Masses.Text = "Masses"
        Me.lbl_Frequence.Text = "Frequency"
        Me.lbl_Periode.Text = "Period"
        Me.lbl_Resultats.Text = "Results"

        Me.lbl_MassTotale.Text = "Masse totale"
        Me.lbl_MassModal.Text = "Masse modale"

        Me.btn_OK.Text = "Close"

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_ModePropre.BackColor = CouleurBackBandeaux
        Me.lbl_ModePropre.ForeColor = CouleurForeBandeaux

        Me.img_ModePropre.Dock = DockStyle.Fill

        Me.TLPan_PartieBasse.ColumnStyles(1).Width = 0
        Me.TLPan_PartieBasse.ColumnStyles(2).Width = 0
    End Sub

    Private Sub PrepareFenetre(MyPoutre As cls_Poutre)

        lDefinieQ(1) = MyPoutre.ChargesU("Q1").EstDefinie
        lDefinieQ(2) = MyPoutre.ChargesU("Q2").EstDefinie
        lDefinieQ(0) = lDefinieQ(1) Or lDefinieQ(2)

        Me.cmb_Q.Visible = lDefinieQ(0)
        Me.cmb_RatioQ.Visible = lDefinieQ(0)

        If lDefinieQ(0) Then
            RemplirCmbQ()
            RemplirCmbRatioQ()
        End If

    End Sub

    Private Sub RemplirCmbRatioQ()
        Me.cmb_RatioQ.Items.Clear()
        For i As Integer = 0 To 9
            Me.cmb_RatioQ.Items.Add(Format(i / 10, "0.0"))
        Next
        Me.cmb_RatioQ.SelectedIndex = 2
    End Sub

    Private Sub RemplirCmbQ()
        Me.cmb_Q.Items.Clear()
        Me.IndiceQ.Clear()

        For i As Integer = 1 To 2
            If lDefinieQ(i) Then
                Me.cmb_Q.Items.Add(LabelQ(i - 1))
                IndiceQ.Add(i)
            End If
        Next

        If Me.cmb_Q.Items.Count > 0 Then _
        Me.cmb_Q.SelectedIndex = 0

    End Sub

#End Region

#Region "===Fermeture==="

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub

#End Region

#Region " Calculs et affichage des résultats "

    Private Sub AfficheCalculModal(MyPoutre As cls_Poutre)

        '--> Déclarations

        Dim RatioQ As Decimal
        Dim IndexQ As Integer

        '--> Définitions des paramètres de calcul

        If lDefinieQ(0) Then
            RatioQ = Me.cmb_RatioQ.SelectedIndex / 10
            IndexQ = IndiceQ(Me.cmb_Q.SelectedIndex)
        Else
            RatioQ = 0
            IndexQ = -1
        End If

        '--> Analyse modale

        MyPoutre.Modal.Analyse(MyPoutre, RatioQ, IndexQ)

        '--> Affichage des résultats

        If MyPoutre.Modal.ErrorCode = 0 Then
            Me.txt_Frequence.Text = GetStringInUnit(MyPoutre.Modal.Frequence, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_Periode.Text = GetStringInUnit(MyPoutre.Modal.Periode, Enu_TypeVariable.SansType, 3, 2, False)
            Me.txt_MassModal.Text = GetStringInUnit(MyPoutre.Modal.MassModal, Enu_TypeVariable.SansType, 3, 0, False)
            Me.txt_MassTotale.Text = GetStringInUnit(MyPoutre.Modal.MassTotal, Enu_TypeVariable.SansType, 3, 0, False)
            Me.Rtxt_Error.Visible = False
        Else
            Me.txt_Frequence.Text = "-"
            Me.txt_Periode.Text = "-"
            Me.txt_MassTotale.Text = "-"
            Me.txt_MassModal.Text = "-"
            Me.Rtxt_Error.Visible = True
            Me.Rtxt_Error.Text = MyPoutre.Modal.ErrorMsg
        End If

        Me.img_ModePropre.Invalidate()

    End Sub


#End Region

#Region " Evènements "

    Private Sub cmb_RatioQ_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_RatioQ.SelectedIndexChanged
        If lBuild Then Exit Sub
        AfficheCalculModal(MyProjet.Poutres(MyProjet.IndEnCours))
    End Sub

    Private Sub cmb_Q_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Q.SelectedIndexChanged
        If lBuild Then Exit Sub
        AfficheCalculModal(MyProjet.Poutres(MyProjet.IndEnCours))
    End Sub
#End Region

#Region " Dessin "

    Private Sub img_Analyse_Paint(sender As Object, e As PaintEventArgs) Handles img_ModePropre.Paint
        DessineMode(e.Graphics, Me.img_ModePropre.ClientRectangle.Width, Me.img_ModePropre.ClientRectangle.Height,
                    MyProjet.Poutres(MyProjet.IndEnCours), lDessNumeros)
    End Sub

    Public Sub DessineMode(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyPoutre As cls_Poutre,
                           lNum As Boolean, ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
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
        'Dim lResult As Boolean = MyPoutre.ChargesA(iCas).lRunCalcul
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

        Dim dMax As Decimal

        '--> Initialisation

        If lBuild Then Exit Sub

        Me.EnveloppeTableau(MyPoutre.Modal.Deformee, MyPoutre.Nodes.nbNodes, dmax, iNodeMax)


        Dim MyBrushN As New SolidBrush(Color.White)
        Dim MyPenDef As New Pen(ColorDef)
        Dim MyPenM As New Pen(Color.Blue)

        xMin = 0 - dCar
        xMax = Longueur + dCar

        yMin = -EcartZ / 2
        yMax = +EcartZ / 2

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        dCar = 0.8 * EcartZ / 2

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

        Dim indAppuis() As Integer = Nothing
        Dim NbApp As Integer
        Dim lEtais As Boolean = False
        MyPoutre.ExtraireIndiceNoeudsAppuis(lEtais, indAppuis, NbApp)

        For iApp As Integer = 0 To NbApp - 1
            DessineAppui(myGr, MyPoutre.Nodes.xGlobal(indAppuis(iApp)), dApp, MyParAff)
        Next

        '--> Déformée

        Dim xo, xe, yo, ye, uZ As Decimal
        Dim Deformee() As Decimal = MyPoutre.Modal.Deformee

        If (Not IsEqual(dMax, 0)) And (MyPoutre.Modal.ErrorCode = 0) Then
            kEch = EcartZ / 2 / dMax
            For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 2
                xo = MyPoutre.Nodes.xGlobal(iNode)
                xe = MyPoutre.Nodes.xGlobal(iNode + 1)
                yo = Deformee(iNode) * kEch
                ye = Deformee(iNode + 1) * kEch
                AddLigne(myGr, MyPenDef, xo, yo, xe, ye, MyParAff)
            Next
            For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
                If iNode = iNodeMax Then
                    MyPen = MyPenSelect
                Else
                    MyPen = MyPenDef
                End If
                uZ = Deformee(iNode)
                AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), kEch * uZ, DiaNode, MyParAff, True, MyPen)
            Next
        End If


    End Sub


    Private Sub EnveloppeTableau(Table() As Decimal, NbNodes As Integer, ByRef TableMax As Decimal, ByRef iNodeMax As Integer)
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   03/11/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   Renvoie la valeur max d'un tableau
        '-----------------------------------------------------------------------------------------------------------------------------------
        '   Table       [E] :   Tableau à tester
        '   NbNodes     [E] :   Dimension du tableau
        '   TableMax    [S] :   Valeur maxi du tableau
        '   iNodeMax    [S] :   Indice du tableau pour lequel valeur max
        '-----------------------------------------------------------------------------------------------------------------------------------

        '--> Initialisation

        iNodeMax = 0
        TableMax = Math.Abs(Table(0))

        '--> Boucle

        For i As Integer = 1 To NbNodes - 1
            If IsGreater(Math.Abs(Table(i)), TableMax) Then
                TableMax = Math.Abs(Table(i))
                iNodeMax = i
            End If
        Next

    End Sub

#End Region

End Class