Imports System.Net.NetworkInformation
Imports Microsoft
Imports System.Reflection
Imports PMXMoteur2
Imports System.IO
Public Class Frm_PPCombinaison

#Region " Variables "

    Dim lBuild As Boolean

    Dim strNoCombi As String
    Dim lNoCombi As Boolean
    Dim strUltimate As String
    Dim strService As String
    Dim strIncendie As String
    Dim strConstruction As String
    Dim strRacineELU As String
    Dim strRacineELS As String
    Dim strRacineELF As String
    Dim strNoCombiELU As String
    Dim strNoCombiELS As String
    Dim strNoCombiELF As String
    Dim strNoCombiELC As String

    Dim iLimitState As Integer
    Const INDULTIME As Integer = 0
    Const INDSERVICE As Integer = 1
    Const INDINCENDIE As Integer = 2
    Const INDCONST As Integer = 3

    '# Résultats pour la combinaisons sélectionnée

    Dim EL_Uz() As Decimal                  ' Flèches
    Dim EL_My(,) As Decimal                 ' Moments fléchissants
    Dim EL_Vz(,) As Decimal                 ' Efforts tranchants
    Dim EL_Rz() As Decimal                  ' Réactions aux appuis
    Dim fMin, fMax As Decimal               ' Flèches enveloppes
    Dim Mmin, Mmax As Decimal               ' Moments enveloppes
    Dim Vmin, Vmax As Decimal               ' Effort tranchants enveloppes
    Dim iNodeMmax, iNodeMmin As Integer     ' Position des moments enveloppes

    Dim lCombiRetrait As Boolean = True     ' Indique si on combine le retrait ou non

    '# Options pour le dessin

    Dim lDessDeformee As Boolean = True     ' Affichage de la déformée
    Dim lDessMoment As Boolean = True       ' Affichage diagramme moments
    Dim lDessEffortT As Boolean = True      ' Affichage diagramme efforts tranchants
    Dim lDessNumeros As Boolean = False     ' Affichage des numéros noeuds

#End Region

#Region "===OUVERTURE==="


    Private Sub Frm_PPCombinaison_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionStyle()
        GestionLangues()
        GestionUnites()
        InitialiseFenetre()

        lBuild = False
    End Sub

    Private Sub InitialiseFenetre()

        InitialiseCalcul(MyProjet.Poutres(MyProjet.IndEnCours))

        RemplirComboLS()
        RemplirComboCombi(MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELU)

        Me.chk_Fleches.Checked = lDessDeformee
        Me.chk_Moment.Checked = lDessMoment
        Me.chk_EffortTranchant.Checked = lDessEffortT
        Me.chk_Numerotation.Checked = lDessNumeros
        Me.chk_Retrait.Checked = lCombiRetrait

        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub InitialiseCalcul(MyPoutre As cls_Poutre)

        MyPoutre.InitialiseCalculs(NomChargesA)
        MyPoutre.AAA_CalculMNVInternesN()
        'MyPoutre.InitialiseCombiA_ELU()
        MyPoutre.InitialiseCombiA(cls_Poutre.nbCombELU, MyPoutre.lCombELU, MyPoutre.CoefCombELU, strRacineELU, MyPoutre.CombiA_ELU)
        MyPoutre.InitialiseCombiA(cls_Poutre.nbCombELS, MyPoutre.lCombELS, MyPoutre.CoefCombELS, strRacineELS, MyPoutre.CombiA_ELS)
        MyPoutre.InitialiseCombiA(cls_Poutre.nbCombFeu, MyPoutre.lCombFeu, MyPoutre.CoefCombFeu, strRacineELF, MyPoutre.CombiA_ELF)

    End Sub

    Private Sub RemplirComboCombi(MyCombi As cls_Combinaisons)

        Me.cmb_Combi.Items.Clear()

        If MyCombi.nbCombi = 0 Then
            Me.cmb_Combi.Items.Add(strNoCombi)
            lNoCombi = True
        Else
            For i As Integer = 0 To MyCombi.nbCombi - 1

                Me.cmb_Combi.Items.Add(MyCombi.Symbole(i))

            Next
            lNoCombi = False
        End If

        Me.cmb_Combi.SelectedIndex = 0

        MAJI_Combinaison()

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

    Private Sub GestionLangues()

        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_PPCOMBINATIONS")
            BlocLine.CreationBloc(Bloc)

            Try
                Me.Text = Bloc("TITLE")                              ' "Combinations"
                Me.lbl_Combi.Text = Bloc("COMBINATIONS")             ' "Combinaisons"
                Me.lbl_LimitState.Text = Bloc("LIMITSTATE")          ' "Limit States"
                Me.lbl_SymbCombi.Text = Bloc("COMBI")                ' "Combi"

                'Me.btn_Annuler.Text = Bloc("TITLE")                          ' "Close"
                Me.btn_OK.Text = Bloc("CLOSE")                       ' "OK"

                Me.chk_EffortTranchant.Text = Bloc("CURVEV")        '"Diagramme V"
                Me.chk_Moment.Text = Bloc("CURVEM")                 ' "Diagramme M"
                Me.chk_Numerotation.Text = Bloc("NUMBERING")        ' "Numérotation"
                Me.chk_Fleches.Text = Bloc("DEFLECTION")            ' "Déformée"
                Me.chk_Retrait.Text = Bloc("WITHSHRNKAGE")          ' "avec le retrait"

                strNoCombi = Bloc("NOCOMBINATION")                  ' "No combination"
                strUltimate = Bloc("ULTIMATE")                      ' "Ultimate"
                strIncendie = Bloc("FIRE")                          ' "Fire"
                strConstruction = Bloc("CONSTRUCTION")              ' "Construction"
                strService = Bloc("SERVICEABILITY")                          ' "Serviceability"

                strRacineELU = Bloc("ULS")                              ' "ULS"
                strRacineELS = Bloc("SLS")                              ' "SLS"
                strRacineELF = Bloc("FLS")                              ' "FLS"
                strNoCombiELU = Bloc("NOCOMBINATIONFORULS")             ' "No defined combinations for ultimate limite state"
                strNoCombiELC = Bloc("NOCOMBINATIONFORULSC")            ' "No defined combinations for ultimate limite state in construction phase"
                strNoCombiELF = Bloc("NOCOMBINATIONFORFLS")             ' "No defined combinations for fire limite state"
                strNoCombiELS = Bloc("NOCOMBINATIONFORSLS")             ' "No defined combinations for serviceability limite state"
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_Combi.BackColor = CouleurBackBandeaux
        Me.lbl_Combi.ForeColor = CouleurForeBandeaux

        Me.img_Analyse.Dock = DockStyle.Fill

        Me.TLPan_PartieBasse.ColumnStyles(1).Width = 0
        Me.TLPan_PartieBasse.ColumnStyles(2).Width = 0

    End Sub


    Private Sub GestionUnites()

        Me.etq_UnitDim1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitForce1.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)
        Me.etq_UnitForce2.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)
        Me.etq_UnitM1.Text = LogicielInfo.Unit_Moment(LogicielOptions.IndUnitMoment)
        Me.etq_UnitM2.Text = LogicielInfo.Unit_Moment(LogicielOptions.IndUnitMoment)

    End Sub


#End Region

#Region " Evènements "

    Private Sub MAJI_Combinaison()

        Dim Indice As Integer = Me.cmb_Combi.SelectedIndex

        '--> Affichage de la combinaison sélectionnée

        Select Case iLimitState
            Case INDULTIME
                AfficheCombinaisonSelectionnee(Indice, strNoCombiELU, MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELU.CoefCombi)
            Case INDSERVICE
                AfficheCombinaisonSelectionnee(Indice, strNoCombiELU, MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELS.CoefCombi)
            Case INDINCENDIE
                AfficheCombinaisonSelectionnee(Indice, strNoCombiELF, MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELF.CoefCombi)
        End Select
        ' AfficheCombinaisonSelectionnee(Indice)

        If Not lNoCombi Then

            '--> Initialisation des calculs (flèches, moments etc)

            Select Case iLimitState
                Case INDULTIME
                    InitialiseCalculsCombinaison(MyProjet.Poutres(MyProjet.IndEnCours), MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELU, Indice)
                Case INDSERVICE
                    InitialiseCalculsCombinaison(MyProjet.Poutres(MyProjet.IndEnCours), MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELS, Indice)
                Case INDINCENDIE
                    InitialiseCalculsCombinaison(MyProjet.Poutres(MyProjet.IndEnCours), MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELF, Indice)
            End Select

            '--> Valeurs enveloppes

            '# Fleches

            ValMaxTableau1D(EL_Uz, fMin, fMax)
            Me.txt_Fleche.Text = GetStringInUnit(Math.Max(Math.Abs(fMin), fMax), Enu_TypeVariable.Dimension, 3, 3, False)

            '# Moments

            ValMaxTableau2D(EL_My, Mmin, Mmax, iNodeMmax, iNodeMmin)
            Me.txt_Mmax.Text = GetStringInUnit(Mmax, Enu_TypeVariable.Moment, 3, 3, False)
            Me.txt_Mmin.Text = GetStringInUnit(Mmin, Enu_TypeVariable.Moment, 3, 3, False)

            '# Efforts trachants

            ValMaxTableau2D(EL_Vz, Vmin, Vmax)

            '# Réactions

            Me.txt_RZ1.Text = GetStringInUnit(EL_Rz(0), Enu_TypeVariable.Effort, 3, 3, False)
            Me.txt_RZ2.Text = GetStringInUnit(EL_Rz(1), Enu_TypeVariable.Effort, 3, 3, False)

        Else

            Me.txt_Fleche.Text = "-"
            Me.txt_Mmax.Text = "-"
            Me.txt_Mmin.Text = "-"
            Me.txt_RZ1.Text = "-"
            Me.txt_RZ2.Text = "-"

        End If

    End Sub

    Private Sub InitialiseCalculsCombinaison(MyPoutre As cls_Poutre, ByRef MyCombi As cls_Combinaisons, Indice As Integer)
        '----------------------------------------------------------------------------------------------------------------------
        '   04/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------
        '   Initialisation du calcul des flèches, moments etc pour une combinaison
        '----------------------------------------------------------------------------------------------------------------------
        '   MyPoutre        [E] :   Poutre traitée
        '   MyCombi         [E] :   Groupe de combinaisons traité
        '   Indice          [E] :   Indice de la combinaison traitée
        '----------------------------------------------------------------------------------------------------------------------

        If lNoCombi Then Exit Sub

        '# Combinaison des flèches

        MyCombi.CombineFleches(Indice, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, Me.EL_Uz, lCombiRetrait)

        '# Combinaison des Moments

        MyCombi.CombineMoments(Indice, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, Me.EL_My, lCombiRetrait)

        '# Combinaison des efforts tranchants

        MyCombi.CombineEffortsT(Indice, MyPoutre.Nodes.nbNodes, MyPoutre.ChargesA, Me.EL_Vz, lCombiRetrait)

        '# Combinaison des Réactions

        MyCombi.CombineReactions(Indice, MyPoutre.Nodes.NbAppuis, MyPoutre.ChargesA, Me.EL_Rz, lCombiRetrait)

    End Sub

    Private Sub AfficheCombinaisonSelectionnee(Indice As Integer)
        '----------------------------------------------------------------------------------------------------------------
        '   04/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim Chaine As String = ""
        Dim lFirst As Boolean = True
        Dim nbCharges As Integer = MyProjet.Poutres(MyProjet.IndEnCours).ChargesA.Count
        Dim lCombi As Boolean

        '--> Traitement

        If lNoCombi Then

            Chaine = "No defined combinations for" & Me.cmb_LimitState.Text & " limite state"

        Else

            Chaine = Me.cmb_Combi.Text

            With MyProjet.Poutres(MyProjet.IndEnCours)
                For i As Integer = 0 To nbCharges - 1

                    If Not IsEqual(.CombiA_ELU.CoefCombi(Indice)(i), 0) Then

                        If .ChargesA(i).Type = cls_CasDeCharge.EnuType.Retrait Then lCombi = lCombiRetrait Else lCombi = True

                        If lCombi Then
                            If lFirst Then
                                Chaine = Chaine & " = "
                                lFirst = False
                            Else
                                Chaine = Chaine & " + "
                            End If

                            Chaine = Chaine & GetStringInUnit(.CombiA_ELU.CoefCombi(Indice)(i), Enu_TypeVariable.SansType, 3, 2, False) & " " & .ChargesA(i).Symbol

                        End If
                    End If

                Next
            End With
        End If

        Me.lbl_CombiSelect.Text = Chaine

    End Sub

    Private Sub AfficheCombinaisonSelectionnee(Indice As Integer, strMessageNo As String, CoefCombi() As List(Of Decimal))
        '----------------------------------------------------------------------------------------------------------------
        '   04/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------
        '   Affiche dans la fenêtre le détail de la combinaison sélectionnée
        '----------------------------------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim Chaine As String = ""
        Dim lFirst As Boolean = True
        Dim nbCharges As Integer = MyProjet.Poutres(MyProjet.IndEnCours).ChargesA.Count
        Dim lCombi As Boolean

        '--> Traitement

        If lNoCombi Then

            Chaine = strMessageNo

        Else

            Chaine = Me.cmb_Combi.Text

            With MyProjet.Poutres(MyProjet.IndEnCours)
                For i As Integer = 0 To nbCharges - 1

                    If Not IsEqual(CoefCombi(Indice)(i), 0) Then

                        If .ChargesA(i).Type = cls_CasDeCharge.EnuType.Retrait Then lCombi = lCombiRetrait Else lCombi = True

                        If lCombi Then
                            If lFirst Then
                                Chaine = Chaine & " = "
                                lFirst = False
                            Else
                                Chaine = Chaine & " + "
                            End If

                            Chaine = Chaine & GetStringInUnit(CoefCombi(Indice)(i), Enu_TypeVariable.SansType, 3, 2, False) & " " & .ChargesA(i).Symbol

                            If (Not .ChargesA(i).lRunCalcul) Then
                                Chaine = Chaine & "(*)"
                            End If

                        End If
                    End If

                Next
            End With
        End If

        Me.lbl_CombiSelect.Text = Chaine

    End Sub

    Private Sub ValMaxTableau2D(MonTableau(,) As Decimal, ByRef ValMin As Decimal, ByRef ValMax As Decimal)
        '----------------------------------------------------------------------------------------------------------------
        '   04/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------
        '   Renvoie les valeurs min et max d'un tableau à une dimension
        '----------------------------------------------------------------------------------------------------------------
        '   MonTableau  [E] :   Tableau à traiter
        '   ValMin      [S] :   
        '   ValMax      [S] :
        '----------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim nbVal As Integer = MonTableau.GetUpperBound(0)

        '--> Traitement


        ValMax = Math.Max(MonTableau(0, 0), MonTableau(0, 1))
        ValMin = Math.Min(MonTableau(0, 0), MonTableau(0, 1))

        For i As Integer = 1 To nbVal
            ValMax = Math.Max(ValMax, Math.Max(MonTableau(i, 0), MonTableau(i, 1)))
            ValMin = Math.Min(ValMin, Math.Min(MonTableau(i, 0), MonTableau(i, 1)))
        Next
    End Sub


    Private Sub ValMaxTableau2D(MonTableau(,) As Decimal, ByRef ValMin As Decimal, ByRef ValMax As Decimal,
                                ByRef iNodeMax As Integer, ByRef iNodeMin As Integer)
        '----------------------------------------------------------------------------------------------------------------
        '   04/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------
        '   Renvoie les valeurs min et max d'un tableau à une dimension
        '----------------------------------------------------------------------------------------------------------------
        '   MonTableau  [E] :   Tableau à traiter
        '   ValMin      [S] :   Valeur mini du tableau    
        '   ValMax      [S] :   Valeur maxi du tableau
        '   iNodeMax    [S] :   Indice du tableau ou est obtenue la valeur max
        '   iNodeMin    [S] :   Indice du tableau ou est obtenue la valeur min
        '----------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim nbVal() As Integer = {MonTableau.GetUpperBound(0), MonTableau.GetUpperBound(1)}
        Dim i, j As Integer

        '--> Traitement

        ValMax = MonTableau(0, 0)
        ValMin = MonTableau(0, 0)

        For j = 1 To nbVal(1)
            ValMax = Math.Max(ValMax, MonTableau(0, j))
            ValMin = Math.Min(ValMin, MonTableau(0, j))
        Next
        iNodeMin = 0
        iNodeMax = 0

        For i = 1 To nbVal(0)

            For j = 0 To nbVal(1)

                If IsGreater(MonTableau(i, j), ValMax) Then
                    ValMax = MonTableau(i, j)
                    iNodeMax = i
                End If

                If IsSmaller(MonTableau(i, j), ValMin) Then
                    ValMin = MonTableau(i, j)
                    iNodeMin = i
                End If

            Next

        Next
    End Sub

    Private Sub ValMaxTableau1D(MonTableau() As Decimal, ByRef ValMin As Decimal, ByRef ValMax As Decimal)
        '----------------------------------------------------------------------------------------------------------------
        '   04/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------
        '   Renvoie les valeurs min et max d'un tableau à une dimension
        '----------------------------------------------------------------------------------------------------------------
        '   MonTableau  [E] :   Tableau à traiter
        '   ValMin      [S] :   
        '   ValMax      [S] :
        '----------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim nbVal As Integer = MonTableau.GetUpperBound(0)

        '--> Traitement

        ValMax = MonTableau.Max
        ValMin = MonTableau.Min
        'ValMax = MonTableau(0)
        'ValMin = MonTableau(0)

        'For i As Integer = 1 To nbVal
        '    ValMax = Math.Max(ValMax, MonTableau(i))
        '    ValMin = Math.Min(ValMin, MonTableau(i))
        'Next
    End Sub

    Private Sub cmb_LimitState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_LimitState.SelectedIndexChanged
        If lBuild Then Exit Sub

        iLimitState = Me.cmb_LimitState.SelectedIndex

        Select Case iLimitState
            Case INDULTIME
                RemplirComboCombi(MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELU)
            Case INDSERVICE
                RemplirComboCombi(MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELS)
            Case INDINCENDIE
                RemplirComboCombi(MyProjet.Poutres(MyProjet.IndEnCours).CombiA_ELF)
            Case INDCONST

        End Select

    End Sub

    Private Sub cmb_Combi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Combi.SelectedIndexChanged
        If lBuild Then Exit Sub

        MAJI_Combinaison()

        Me.img_Analyse.Invalidate()
    End Sub


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

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub

    Private Sub chk_Retrait_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Retrait.CheckedChanged
        If lBuild Then Exit Sub

        lCombiRetrait = Me.chk_Retrait.Checked

        MAJI_Combinaison()

        Me.img_Analyse.Invalidate()

    End Sub

#End Region

#Region " Dessin "

    Private Sub img_Analyse_Paint(sender As Object, e As PaintEventArgs) Handles img_Analyse.Paint
        DessineRDM(e.Graphics, Me.img_Analyse.ClientRectangle.Width, Me.img_Analyse.ClientRectangle.Height,
                   MyProjet.Poutres(MyProjet.IndEnCours),
                   lDessDeformee, lDessMoment, lDessEffortT, lDessNumeros)
    End Sub

    Public Sub DessineRDM(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyPoutre As cls_Poutre,
                          lDef As Boolean, lMom As Boolean, lTranchant As Boolean, lNum As Boolean,
                           ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   11/08/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation des largeurs efficaces
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

        Dim ColorDef = Color.DarkOrange
        Dim ColorDiagM = Color.DarkRed
        Dim ColorDiagV = Color.DarkBlue

        Dim xo, xe, yo, ye As Decimal
        Dim kEch As Decimal

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

        '--> Affichage de la poutre

        AddLigne(myGr, 0, 0, Longueur, 0, MyParAff)

        '--> Affichage des noeuds

        For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
            If iNode = iNodeMmax Then MyPen = MyPenSelect Else MyPen = MyPenPoutre

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
        MyPoutre.ExtraireIndiceNoeudsAppuis(False, indAppuis, NbApp)

        For iApp As Integer = 0 To NbApp - 1
            DessineAppui(myGr, MyPoutre.Nodes.xGlobal(indAppuis(iApp)), dApp, MyParAff)
        Next

        '--> Déformée

        Dim Uz As Decimal

        'MyPoutre.ChargesA(iCas).EnveloppesFleche(fMin, fMax)

        If lDef And (Not lNoCombi) Then

            kEch = CoefEchelleDessin(fMin, fMax, EcartZ / 2)

            For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 2
                xo = MyPoutre.Nodes.xGlobal(iNode)
                xe = MyPoutre.Nodes.xGlobal(iNode + 1)
                yo = EL_Uz(iNode) * kEch
                ye = EL_Uz(iNode + 1) * kEch
                AddLigne(myGr, MyPenDef, xo, yo, xe, ye, MyParAff)
            Next

            For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
                Uz = EL_Uz(iNode)
                AddCerclePlein(myGr, MyBrushN, MyPoutre.Nodes.xGlobal(iNode), kEch * Uz, DiaNode, MyParAff, True, MyPenDef)
            Next

        End If

        '--> Diagramme de Moments de flexion

        If lMom And (Not lNoCombi) Then

            kEch = CoefEchelleDessin(Mmin, Mmax, EcartZ / 2) * SigneM

            DessineDiagrammeRDM(myGr, MyPoutre, EL_My, kEch, ColorDiagM, MyParAff)

        End If

        '--> Diagramme de efforts tranchants

        If lTranchant And (Not lNoCombi) Then

            kEch = CoefEchelleDessin(Vmin, Vmax, EcartZ / 2) * SigneV

            DessineDiagrammeRDM(myGr, MyPoutre, EL_Vz, kEch, ColorDiagV, MyParAff)

        End If
    End Sub

    Private Function CoefEchelleDessin(valMin As Decimal, valMax As Decimal, dCar As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------
        '   04/10/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Retourne le coefficient d'échelle à utiliser pour un diagramme
        '-----------------------------------------------------------------------------------------------
        '   valMin      [E] :   valeur mini du diagramme
        '   valMax      [E] :   valeur maxi du diagramme
        '   dCar        [E] :   valeur caractéristique pour l'affichage du diagramme
        '-----------------------------------------------------------------------------------------------

        Dim kEch As Decimal
        Dim valAbsMax As Decimal

        valAbsMax = Math.Max(Math.Abs(valMin), valMax)
        If IsEqual(valAbsMax, 0) Then kEch = 1 Else kEch = dCar / valAbsMax

        Return kEch
    End Function

#End Region

End Class