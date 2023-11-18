Imports PMXMoteur2
Imports System.IO
'Imports System.Net.WebRequestMethods
Imports System.Reflection
Imports System.Security.Cryptography

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
    Dim strCritereSigmaA As String
    Dim strCritereSigmaC As String
    Dim strCritereV As String
    Dim strCritereMV As String
    Dim strNoCritere As String

    Dim lDessCritere As Boolean = True      ' Affichage du critère
    Dim lDessAction As Boolean = True       ' Affichage diagramme action
    Dim lDessResistance As Boolean = True   ' Affichage diagramme resistance
    Dim lDessNumeros As Boolean = False     ' Affichage des numéros noeuds

    Dim CritereA As New cls_Critere(MyProjet.Poutres(MyProjet.IndEnCours).Nodes.nbNodes)

    Const pDecAxe As Decimal = 0.05
    Const kADJV As Decimal = 0.95
    Const kTiret As Decimal = 0.025

    Dim TypeEffet As Enu_TypeVariable

    Dim strRacineELU As String = "ELU"
    Dim strRacineELS As String = "ELS"
    Dim strRacineELF As String = "ELF"


#End Region

#Region "===OUVERTURE==="
    Private Sub Frm_PPVerifications_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        'MyProjet.Poutres(MyProjet.IndEnCours).Param.lElasticDesign = True
        MyProjet.Poutres(MyProjet.IndEnCours).AAA_Verifications(NomChargesA, strRacineELU, strRacineELS, strRacineELF)
        GestionLangues()
        GestionStyle()
        PrepareFenetre()
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

        Me.TLPan_PartieBasse.ColumnStyles(1).Width = 0
        Me.TLPan_PartieBasse.ColumnStyles(2).Width = 0

    End Sub

    Private Sub GestionLangues()

        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_PPVERIFICATIONS")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")

                Me.lbl_Verification.Text = Bloc("VERIFICATIONS")

                Me.lbl_LimitState.Text = Bloc("LIMITSTATE")         ' "Limit State"
                Me.lbl_Critere.Text = Bloc("CRITERIA")              '  "Criteria"

                Me.chk_Action.Text = Bloc("ACTIONS")                '  "Actions"
                Me.chk_Resistance.Text = Bloc("RESISTANCES")        ' "Resistances"
                Me.chk_Numerotation.Text = Bloc("NUMBERING")        '  "Node numbers"
                Me.chk_Critere.Text = Bloc("CRITERION")             '  "Criterion"

                Me.lbl_Resultats.Text = Bloc("RESULTS")             '  "Resuts"
                Me.lbl_ValMaxCritere.Text = Bloc("MAXVALUE")        '  "Valeur maximale"
                Me.lbl_Node.Text = Bloc("NODE")                     '  "Node"
                Me.lbl_Combinaison.Text = Bloc("COMBINATION")       '  "Combination"

                Me.btn_OK.Text = Bloc("CLOSE")                      ' "Close"
                'Me.btn_Annuler.Text = Bloc("")                      ' "Annuler"

                strUltimate = Bloc("ULTIMATE")                      ' "Ultimate"
                strIncendie = Bloc("FIRE")                          ' "Fire"
                strConstruction = Bloc("CONSTRUCTION")              ' "Construction"
                strService = Bloc("SERVICEABILITY")                 ' "Serviceability"

                strCritereM = Bloc("CRITERIONM")                    ' "Resistance to bending moments"
                strCritereSigmaA = Bloc("CRITERIONSIGMAA")          ' "Resistance to bending moments"
                strCritereSigmaC = Bloc("CRITERIONSIGMAC")          ' "Resistance to bending moments"
                strCritereV = Bloc("CRITERIONV")                    ' "Resistance to shear forces"
                strCritereMV = Bloc("CRITERIONMV")                  ' "Resistance to MV interaction"

                strNoCritere = Bloc("NOCRITERIA")                    ' "No criterion"

            Catch ex As Exception
            End Try
        End If

        ' VERIFICATIONS = Verifications

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
                    Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                        RemplirComboCriterePoutreAcierELU(MyPoutre)
                    Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                        RemplirComboCriterePoutreMixteELU(MyPoutre)
                End Select

        End Select

        If Me.cmb_Critere.Items.Count > 0 Then
            Me.cmb_Critere.SelectedIndex = 0
        End If

    End Sub

    Private Sub RemplirComboCriterePoutreAcierELU(MyPoutre As cls_Poutre)
        Me.cmb_Critere.Items.Clear()

        Const iVerif As Integer = 0

        lNoCritere = True

        If Not (MyPoutre.VerifAcier Is Nothing) Then
            AjouteCritereDansCombo(strCritereM, MyPoutre.VerifAcier(iVerif).CritereM, lNoCritere)
            AjouteCritereDansCombo(strCritereSigmaA, MyPoutre.VerifAcier(iVerif).CritereSigmaA, lNoCritere)
            AjouteCritereDansCombo(strCritereV, MyPoutre.VerifAcier(iVerif).CritereV, lNoCritere)
        End If

        If lNoCritere Then
            Me.cmb_Critere.Items.Add(strNoCritere)
        End If
    End Sub
    Private Sub RemplirComboCriterePoutreMixteELU(MyPoutre As cls_Poutre)

        Me.cmb_Critere.Items.Clear()

        Const iVerif As Integer = 0

        lNoCritere = True

        If Not (MyPoutre.VerifMixte Is Nothing) Then
            AjouteCritereDansCombo(strCritereM, MyPoutre.VerifMixte(iVerif).CritereM, lNoCritere)
            AjouteCritereDansCombo(strCritereSigmaA, MyPoutre.VerifMixte(iVerif).CritereSigmaA, lNoCritere)
            AjouteCritereDansCombo(strCritereSigmaC, MyPoutre.VerifMixte(iVerif).CritereSigmaC, lNoCritere)
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

    Private Sub PrepareFenetre()
        Me.chk_Critere.Checked = lDessCritere
        Me.chk_Action.Checked = lDessAction
        Me.chk_Resistance.Checked = lDessResistance
        Me.chk_Numerotation.Checked = lDessNumeros
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
                    Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage

                        Select Case Me.cmb_Critere.Text
                            Case strCritereM
                                TransfertCritere(MyPoutre.VerifAcier(0).CritereM)
                                TypeEffet = Enu_TypeVariable.Moment
                            Case strCritereSigmaA
                                TransfertCritere(MyPoutre.VerifAcier(0).CritereSigmaA)
                                TypeEffet = Enu_TypeVariable.Contrainte
                            Case strCritereV
                                TransfertCritere(MyPoutre.VerifAcier(0).CritereV)
                                TypeEffet = Enu_TypeVariable.Effort
                            Case strNoCritere
                                lNoCritere = True
                        End Select

                    Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage

                        Select Case Me.cmb_Critere.Text
                            Case strCritereM
                                TransfertCritere(MyPoutre.VerifMixte(0).CritereM)
                                TypeEffet = Enu_TypeVariable.Moment
                            Case strCritereV
                                TransfertCritere(MyPoutre.VerifMixte(0).CritereV)
                                TypeEffet = Enu_TypeVariable.Effort
                            Case strCritereSigmaA
                                TransfertCritere(MyPoutre.VerifMixte(0).CritereSigmaA)
                                TypeEffet = Enu_TypeVariable.Contrainte
                            Case strCritereSigmaC
                                TransfertCritere(MyPoutre.VerifMixte(0).CritereSigmaC)
                                TypeEffet = Enu_TypeVariable.Contrainte
                            Case strNoCritere
                                lNoCritere = True
                        End Select

                End Select

        End Select

        If CritereA.iCombiM > -1 Then _
        Me.txt_Combi.Text = MyPoutre.CombiA_ELU.Symbole(CritereA.iCombiM)            ' Format(CritereA.iCombiM, "0")
        Me.txt_Node.Text = Format(CritereA.iNodeM + 1, "0")
        Me.txt_ValMax.Text = GetStringInUnit(CritereA.CritereMax, Enu_TypeVariable.SansType, 4, 3, False)

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


    Private Sub chk_Critere_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Critere.CheckedChanged
        lDessCritere = Me.chk_Critere.Checked
        Me.img_Verifications.Invalidate()
    End Sub

    Private Sub chk_Action_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Action.CheckedChanged
        lDessAction = Me.chk_Action.Checked
        Me.img_Verifications.Invalidate()
    End Sub

    Private Sub chk_Resistance_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Resistance.CheckedChanged
        lDessResistance = Me.chk_Resistance.Checked
        Me.img_Verifications.Invalidate()
    End Sub

    Private Sub chk_Numerotation_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Numerotation.CheckedChanged
        lDessNumeros = Me.chk_Numerotation.Checked
        Me.img_Verifications.Invalidate()
    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
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

        Dim MyFontNum As New Font("Arial", 7)

        Dim ColorResistance As Color = Color.DarkOrange
        Dim ColorCrit As Color = Color.DarkRed
        Dim ColorAction As Color = Color.DarkBlue
        Dim ColorPoutre As Color = Color.DarkGray
        Dim ColorSelect As Color = Color.OrangeRed

        'Dim xo, xe, yo, ye As Decimal
        Dim kEch As Decimal

        Dim valEdMax, valRdMax As Decimal
        Dim valMax As Decimal

        Dim MyPenPoutre As New Pen(ColorPoutre)
        Dim MyPenSelect As New Pen(ColorSelect, 2)
        Dim xAxe As Decimal

        '--> Initialisation

        If lBuild Then Exit Sub

        Dim MyBrushN As New SolidBrush(Color.White)

        dCar = Longueur * pDecAxe
        xMin = 0 - dCar
        xMax = Longueur + dCar

        yMin = 0
        yMax = +EcartZ / 2

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        dCar = 0.95 * EcartZ / 2

        '--> Affichage de la poutre

        AddLigne(myGr, MyPenPoutre, 0, 0, Longueur, 0, MyParAff)

        '--> Affichage des noeuds

        For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
            If iNode = myCritere.iNodeM Then
                AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), 0, DiaNode, MyParAff, True, MyPenSelect)
            Else
                AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), 0, DiaNode, MyParAff, True, MyPenPoutre)
            End If
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

        If lCritere And (Not lNoCritere) Then
            DessineCritere(myGr, myCritere.Critere, myCritere.CritereMax, MyPoutre, dCar, ColorCrit, MyParAff)
        End If

        '--> Affichage des actions et résistances

        If (lAction Or lResistance) And (Not lNoCritere) Then

            valEdMax = CritereA.Action.Max
            valRdMax = CritereA.Resistance.Max
            valMax = Math.Max(valEdMax, valRdMax)

            If IsEqual(valMax, 0) Then kEch = 1 Else kEch = dCar / valMax * kADJV

            If lAction Then DessineTableau(myGr, CritereA.Action, kEch, MyPoutre, ColorAction, 1, MyParAff)
            If lResistance Then DessineTableau(myGr, CritereA.Resistance, kEch, MyPoutre, ColorResistance, 1, MyParAff)

            xAxe = -Longueur * pDecAxe
            DessineAxeEffets(myGr, xAxe, valRdMax, valEdMax, kEch, dCar, ColorAction, ColorResistance, TypeEffet, MyParAff)

        End If

    End Sub

    Private Sub DessineAxeEffets(MyGr As Graphics, xAxe As Decimal, ValMaxR As Decimal, ValMaxA As Decimal,
                                  kEch As Decimal, dCar As Decimal, CouleurA As Color, CouleurR As Color,
                                 TypeV As Enu_TypeVariable, MyParAff As Struc_Affichage)
        '-----------------------------------------------------------------------------------------------
        '   20/10/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation de l'axe pour les effets (actions et résistances) 
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   xAxe        [E] :   Position de l'axe
        '   ValMaxR     [E] :   Valeur maximale des résistances
        '   ValMaxA     [E] :   Valeur maxi de l'action
        '   kEch        [E] :   Facteur d'échelle
        '   dCar        [E] :   
        '   Longueur    [E] :   Longueur de la poutre
        '   MyParAff    [E] :   Paramètres d'affichage
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PenAxe As New Pen(Color.Black)
        Dim MyPenB As SolidBrush
        Dim Chaine As String = ""
        Dim MyFont As New Font("Arial", 8)
        Dim pVal As Decimal
        Dim PenLimit As New Pen(Color.DarkRed, 0.5)
        Dim dTiret As Decimal = kTiret * dCar

        '--> Axe

        AddFleche(MyGr, PenAxe, xAxe, 0, xAxe, dCar, MyParAff, False, True)

        '--> Valeur maximale le long de l'axe pour la résistance

        MyPenB = New SolidBrush(CouleurR)
        Chaine = GetStringInUnit(ValMaxR, TypeV, 2, 1, False)
        AddLigne(MyGr, xAxe, kEch * ValMaxR, xAxe + dTiret, kEch * ValMaxR, MyParAff)
        AddTexte(MyGr, MyPenB, Chaine, MyFont, xAxe, kEch * ValMaxR, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)

        '--> Valeur maxi de l'effet

        MyPenB = New SolidBrush(CouleurA)
        Chaine = GetStringInUnit(ValMaxA, TypeV, 2, 1, False)
        AddLigne(MyGr, xAxe, kEch * ValMaxA, xAxe + dTiret, kEch * ValMaxA, MyParAff)
        AddTexte(MyGr, MyPenB, Chaine, MyFont, xAxe, kEch * ValMaxA, MyParAff, HorizontalAlignment.Left, VerticalAlignement.Middle)

    End Sub

    Private Sub DessineTableau(MyGr As Graphics, Tableau() As Decimal, kEch As Decimal, MyPoutre As cls_Poutre,
                               MyCouleur As Color, iPen As Integer, MyParAff As Struc_Affichage)
        '-----------------------------------------------------------------------------------------------
        '   06/10/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation du critère 
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   Tableau     [E] :   Table des valeurs à afficher (en diagramme le long de la poutre)
        '   kEch        [E] :   Facteur d'échelle
        '   MyPoutre    [E] :   Poutre à dessiner
        '   MyCouleur   [E] :   Couleur du pinceau
        '   iPen        [E] :   Largeur du trait
        '   MyParAff    [E] :   Paramètres d'affichage
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyPen As New Pen(MyCouleur, iPen)
        Dim xo, yo As Decimal
        Dim xe, ye As Decimal

        '--> Représentation de la courbe du critère

        For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 2

            xo = MyPoutre.Nodes.xGlobal(iNode)
            xe = MyPoutre.Nodes.xGlobal(iNode + 1)
            yo = kEch * Tableau(iNode)
            ye = kEch * Tableau(iNode + 1)

            AddLigne(MyGr, MyPen, xo, yo, xe, ye, MyParAff)

        Next

        '--> Extrémités

        If Not IsEqual(Tableau(0), 0) Then

            xo = 0
            xe = xo
            yo = 0
            ye = kEch * Tableau(0)

            AddLigne(MyGr, MyPen, xo, yo, xe, ye, MyParAff)

        End If

        If Not IsEqual(Tableau(MyPoutre.Nodes.nbNodes - 1), 0) Then

            xo = MyPoutre.Nodes.xGlobal(MyPoutre.Nodes.nbNodes - 1)
            xe = xo
            yo = 0
            ye = kEch * Tableau(MyPoutre.Nodes.nbNodes - 1)

            AddLigne(MyGr, MyPen, xo, yo, xe, ye, MyParAff)

        End If

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
        Dim xAxe As Decimal = MyPoutre.LongueurTotale * (1 + pDecAxe)

        '--> Initialisation

        If IsEqual(pValMax, 0) Then kEch = 1 Else kEch = kADJV * dCar / pValMax

        '--> Représentation de l'axe

        DessineAxeCritere(MyGr, xAxe, pValMax, valMax, kEch, dCar, MyPoutre.LongueurTotale, MyParAff)

        '--> Représentation de la courbe du critère

        DessineTableau(MyGr, valCrit, kEch, MyPoutre, MyCouleur, 2, MyParAff)

    End Sub

    Private Sub DessineAxeCritere(MyGr As Graphics, xAxe As Decimal, pValMax As Decimal, ValMaxC As Decimal,
                                  kEch As Decimal, dCar As Decimal, Longueur As Decimal, MyParAff As Struc_Affichage)
        '-----------------------------------------------------------------------------------------------
        '   20/10/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation de l'axe pour les critères
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   xAxe        [E] :   Position de l'axe
        '   pValMax     [E] :   Valeur maximale du critère affichée sur l'axe
        '   ValMax      [E] :   Valeur maxi du critère
        '   kEch        [E] :   Facteur d'échelle
        '   dCar        [E] :   
        '   Longueur    [E] :   Longueur de la poutre
        '   MyParAff    [E] :   Paramètres d'affichage
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim PenAxe As New Pen(Color.Black)
        Dim MyPenB As New SolidBrush(Color.Black)
        Dim Chaine As String = ""
        Dim MyFont As New Font("Arial", 8)
        Dim pVal As Decimal
        Dim PenLimit As New Pen(Color.DarkRed, 0.5)
        Dim dTiret As Decimal = kTiret * dCar

        '--> Axe

        AddFleche(MyGr, PenAxe, xAxe, 0, xAxe, dCar, MyParAff, False, True)

        '--> Valeur maximale le long de l'axe

        Chaine = GetStringInUnit(pValMax, Enu_TypeVariable.SansType, 2, 1, False)
        AddLigne(MyGr, xAxe, kEch * pValMax, xAxe * (1 - pDecAxe / 4), kEch * pValMax, MyParAff)
        AddTexte(MyGr, MyPenB, Chaine, MyFont, xAxe, kEch * pValMax, MyParAff, HorizontalAlignment.Right, VerticalAlignement.Middle)

        '--> Valeur 1

        If Not IsEqual(pValMax, 1) Then
            pVal = 1

            Chaine = GetStringInUnit(pVal, Enu_TypeVariable.SansType, 2, 1, False)
            AddLigne(MyGr, xAxe, kEch * pVal, xAxe - dTiret, kEch * pVal, MyParAff)
            AddTexte(MyGr, MyPenB, Chaine, MyFont, xAxe, kEch * pVal, MyParAff, HorizontalAlignment.Right, VerticalAlignement.Middle)

            AddLigne(MyGr, PenLimit, 0, kEch * pVal, Longueur, kEch * pVal, MyParAff)

        End If

        '--> Valeur maxi

        If Not IsEqual(pValMax, ValMaxC) Then
            pVal = ValMaxC

            Chaine = GetStringInUnit(pVal, Enu_TypeVariable.SansType, 2, 1, False)
            AddLigne(MyGr, xAxe, kEch * pVal, xAxe - dTiret, kEch * pVal, MyParAff)
            AddTexte(MyGr, MyPenB, Chaine, MyFont, xAxe, kEch * pVal, MyParAff, HorizontalAlignment.Right, VerticalAlignement.Middle)

            AddLigne(MyGr, PenLimit, 0, kEch * pVal, Longueur, kEch * pVal, MyParAff)

        End If

        '--> Divisions



    End Sub



#End Region


End Class