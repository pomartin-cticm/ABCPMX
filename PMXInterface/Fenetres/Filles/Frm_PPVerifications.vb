Imports PMXMoteur2

Public Class Frm_PPVerifications


#Region " Variables "

    Dim lBuild As Boolean

    Dim strUltimate As String
    Dim strService As String
    Dim strIncendie As String
    Dim strConstruction As String

    Dim iLimitState As Integer
    Const INDULTIME As Integer = 0
    Const INDSERVICE As Integer = 1
    Const INDINCENDIE As Integer = 2
    Const INDCONST As Integer = 3

    Dim lNoCritere As Boolean
    Dim strCritereM As String
    Dim strCritereV As String
    Dim strCritereMV As String
    Dim strNoCritere As String

    Dim lDessCritere As Boolean = True      ' Affichage du critère
    Dim lDessAction As Boolean = True       ' Affichage diagramme action
    Dim lDessResistance As Boolean = True   ' Affichage diagramme resistance
    Dim lDessNumeros As Boolean = False     ' Affichage des numéros noeuds

    Dim CritereA As New cls_Critere(MyProjet.Poutres(MyProjet.IndEnCours).Nodes.nbNodes)

#End Region

#Region "===OUVERTURE==="
    Private Sub Frm_PPVerifications_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        MyProjet.Poutres(MyProjet.IndEnCours).AAA_Verifications()
        GestionLangues()
        GestionStyle()
        RemplirComboLS()
        RemplirComboCriteres(MyProjet.Poutres(MyProjet.IndEnCours))
        MAJ_CritereAAfficher(MyProjet.Poutres(MyProjet.IndEnCours))
        Me.img_Verifications.Invalidate()

        lBuild = False
    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_Verification.BackColor = CouleurBackBandeaux
        Me.lbl_Verification.ForeColor = CouleurForeBandeaux

        Me.img_Verifications.Dock = DockStyle.Fill
    End Sub

    Private Sub GestionLangues()


        Me.lbl_LimitState.Text = "Limit State"
        Me.lbl_Critere.Text = "Criteria"

        strUltimate = "Ultimate"
        strIncendie = "Fire"
        strConstruction = "Construction"
        strService = "Serviceability"

        strCritereM = "Resistance to bending moments"
        strCritereV = "Resistance to shear forces"
        strCritereMV = "Resistance to MV interaction"

        strNoCritere = "No criterion"
    End Sub

    Private Sub RemplirComboLS()

        Me.cmb_LimitState.Items.Clear()

        Me.cmb_LimitState.Items.Add(strUltimate)
        Me.cmb_LimitState.Items.Add(strService)
        Me.cmb_LimitState.Items.Add(strIncendie)
        Me.cmb_LimitState.Items.Add(strConstruction)

        Me.cmb_LimitState.SelectedIndex = 0
        iLimitState = 0

    End Sub

    Private Sub RemplirComboCriteres(MyPoutre As cls_Poutre)

        Select Case iLimitState
            Case INDULTIME

                Select Case MyPoutre.TypeSection
                    Case cls_Section.Enum_TypeSection.Acier, cls_Section.Enum_TypeSection.AcierEnrobage
                    Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                        RemplirComboCriterePoutreMixteELU(MyPoutre)
                End Select

        End Select

        If Me.cmb_Critere.Items.Count > 0 Then
            Me.cmb_Critere.SelectedIndex = 0
        End If

    End Sub

    Private Sub RemplirComboCriterePoutreMixteELU(MyPoutre As cls_Poutre)

        Me.cmb_Critere.Items.Clear()

        Const iVerif As Integer = 0

        lNoCritere = True

        If Not (MyPoutre.VerifMixte Is Nothing) Then
            AjouteCritereDansCombo(strCritereM, MyPoutre.VerifMixte(iVerif).CritereM, lNoCritere)
            AjouteCritereDansCombo(strCritereV, MyPoutre.VerifMixte(iVerif).CritereV, lNoCritere)

        End If

        If lNoCritere Then
            Me.cmb_Critere.Items.Add(strNoCritere)
        End If

    End Sub

    Private Sub AjouteCritereDansCombo(strCritere As String, MyCritere As cls_Critere, ByRef plNoCritere As Boolean)

        If Not (MyCritere Is Nothing) Then

            If MyCritere.lDefini Then

                Me.cmb_Critere.Items.Add(strCritere)
                plNoCritere = False

            End If

        End If

    End Sub

#End Region

#Region " Evènements "

    Private Sub cmb_LimitState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_LimitState.SelectedIndexChanged
        RemplirComboCriteres(MyProjet.Poutres(MyProjet.IndEnCours))
        Me.img_Verifications.Invalidate()
    End Sub

    Private Sub cmb_Critere_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Critere.SelectedIndexChanged
        MAJ_CritereAAfficher(MyProjet.Poutres(MyProjet.IndEnCours))
        Me.img_Verifications.Invalidate()
    End Sub

    Private Sub MAJ_CritereAAfficher(MyPoutre As cls_Poutre)


        Select Case iLimitState
            Case INDULTIME

                Select Case MyPoutre.TypeSection
                    Case cls_Section.Enum_TypeSection.Acier, cls_Section.Enum_TypeSection.AcierEnrobage
                    Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage

                        If Me.cmb_Critere.Text = strCritereM Then
                            TransfertCritere(MyPoutre.VerifMixte(0).CritereM)
                        End If
                End Select

        End Select


    End Sub

    Private Sub TransfertCritere(MyCritere As cls_Critere)

        CritereA.Critere = MyCritere.Critere
        CritereA.Action = MyCritere.Action
        CritereA.Resistance = MyCritere.Resistance
        CritereA.iCombiNodeM = MyCritere.iCombiNodeM
        CritereA.CritereMax = MyCritere.CritereMax
        CritereA.iCombiM = MyCritere.iCombiM
        CritereA.iNodeM = MyCritere.iNodeM

    End Sub

#End Region

#Region " Dessin "

    Private Sub img_Verifications_Paint(sender As Object, e As PaintEventArgs) Handles img_Verifications.Paint

        DessineCrit(e.Graphics, Me.img_Verifications.ClientRectangle.Width, Me.img_Verifications.ClientRectangle.Height,
                    MyProjet.Poutres(MyProjet.IndEnCours), CritereA, lDessCritere, lDessAction, lDessResistance, lDessNumeros)

    End Sub

    Public Sub DessineCrit(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyPoutre As cls_Poutre, myCritere As cls_Critere,
                           lCritere As Boolean, lAction As Boolean, lResistance As Boolean, lNum As Boolean,
                           ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   06/10/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation d'un critère de vérification
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   MyPoutre    [E] :   Poutre à dessiner
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
        Dim dApp As Decimal = Longueur / 50
        Dim DiaNode As Decimal = Longueur / 200

        Dim Chaine As String
        Dim MyPenB As New SolidBrush(Color.Gray)
        Const SigneM As Decimal = -1
        Const SigneV As Decimal = -1
        Dim MyFontNum As New Font("Arial", 7)

        Dim ColorResistance = Color.DarkOrange
        Dim ColorCrit = Color.DarkRed
        Dim ColorAction = Color.DarkBlue

        Dim xo, xe, yo, ye As Decimal
        Dim kEch As Decimal

        '--> Initialisation

        If lBuild Then Exit Sub

        Dim MyBrushN As New SolidBrush(Color.White)
        ' Dim MyPenDef As New Pen(ColorDef)
        'Dim MyPenM As New Pen(Color.Blue)

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
            AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), 0, DiaNode, MyParAff, True)
            If lDessNumeros Then
                Chaine = "N" & CStr(iNode + 1)
                AddTexte(myGr, MyPenB, Chaine, MyFontNum, MyPoutre.Nodes.xGlobal(iNode), 0, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Top)
            End If
        Next

        '--> Affichage des appuis

        Dim indAppuis() As Integer = Nothing
        Dim NbApp As Integer
        MyPoutre.ExtraireIndiceNoeudsAppuis(False, indAppuis, NbApp)

        For iApp As Integer = 0 To NbApp - 1
            DessineAppui(myGr, MyPoutre.Nodes.xGlobal(indAppuis(iApp)), dApp, MyParAff)
        Next

        '--> Affichage du critère

        If lCritere Then
            DessineCritere(myGr, myCritere.Critere, myCritere.CritereMax, MyPoutre, dCar, ColorCrit, MyParAff)
        End If

        '--> Affichage des actions et résistances



    End Sub

    Private Sub DessineCritere(MyGr As Graphics, valCrit() As Decimal, valMax As Decimal, MyPoutre As cls_Poutre,
                               dCar As Decimal, MyCouleur As Color, MyParAff As Struc_Affichage)
        '-----------------------------------------------------------------------------------------------
        '   06/10/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation du critère 
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   valCrit     [E] :   Table des valeurs du critère
        '   valMax      [E] :   Valeur maxi du critère
        '   MyPoutre    [E] :   Poutre à dessiner
        '   MyParAff    [E] :   Paramètres d'affichage
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pValMax As Decimal = Math.Ceiling(valMax)
        Dim kEch As Decimal
        Dim MyPen As New Pen(MyCouleur)
        Dim xo, yo As Decimal
        Dim xe, ye As Decimal

        '--> Initialisation

        If IsEqual(pValMax, 0) Then kEch = 1 Else kEch = dCar / pValMax

        '--> Représentation de l'axe

        '--> Représentation de la courbe du critère

        For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 2

            xo = MyPoutre.Nodes.xGlobal(iNode)
            xe = MyPoutre.Nodes.xGlobal(iNode + 1)
            yo = kEch * CritereA.Critere(iNode)
            ye = kEch * CritereA.Critere(iNode + 1)

            AddLigne(MyGr, MyPen, xo, yo, xe, ye, MyParAff)

        Next



    End Sub



#End Region


End Class