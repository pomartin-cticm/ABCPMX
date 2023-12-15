Imports PMXMoteur2
Imports System.Drawing.Drawing2D
Imports System.IO

Public Class Frm_PPCasDeCharge

#Region " Variables "

    Dim lBuild As Boolean

    Private DiagrammesNDC As New Cls_DiagrammeNDC 'indice du cas de charge à afficher (utile pour le dessin RDM dans la note de calcul)

    'Dim tab_fMin() As Decimal
    'Dim tab_fMax() As Decimal
    'Dim fMaxG As Decimal
    'Dim .fMinG As Decimal

    'Dim tab_Mmin() As Decimal
    'Dim tab_iNodeMmin() As Decimal
    'Dim tab_Mmax() As Decimal
    'Dim tab_iNodeMmax() As Decimal
    'Dim tab_Vmin() As Decimal
    'Dim tab_Vmax() As Decimal
    'Dim MmaxG As Decimal
    'Dim MminG As Decimal
    'Dim VmaxG As Decimal
    'Dim VminG As Decimal

    'Dim lDessDeformee As Boolean = True     ' Affichage de la déformée
    'Dim lDessMoment As Boolean = True       ' Affichage diagramme moments
    'Dim lDessEffortT As Boolean = True      ' Affichage diagramme efforts tranchants
    'Dim lDessInerties As Boolean = True     ' Affichage des inerties
    'Dim lDessNumeros As Boolean = False     ' Affichage des numéros noeuds
    'Dim lDessCharges As Boolean = False     ' Affichage des charges
    'Dim lDessEchLocal As Boolean = False

    'Private ColorDeg As Color = Color.Cornsilk

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


        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_PPLOADCASE")
            BlocLine.CreationBloc(Bloc)

            Try
                Me.Text = Bloc("TITLE")                             ' "Load cases"
                Me.lbl_CasDeCharges.Text = Bloc("LOADCASE")         ' "Load cases"
                Me.btn_Annuler.Text = "Cancel"
                Me.btn_OK.Text = Bloc("CLOSE")                      ' "Close"

                Me.lbl_Case.Text = Bloc("CASE")                     ' "Case"
                Me.lbl_Etat.Text = Bloc("STATE")                    ' "Etat"
                Me.lbl_RunCalcul.Text = Bloc("CALCULATIONOK")       ' "Calcul effectué ?"
                Me.lbl_Fleche.Text = Bloc("MAXDEFLECTION")          ' "Flèche maxi"

                Me.chk_Chargement.Text = Bloc("LOADS")              ' "Chargement"
                Me.chk_EffortTranchant.Text = Bloc("CURVEV")        ' "Diagramme V"
                Me.chk_Moment.Text = Bloc("CURVEM")                 ' "Diagramme M"
                Me.chk_Numerotation.Text = Bloc("NUMBERING")        ' "Numérotation"
                Me.chk_Fleches.Text = Bloc("DEFLECTION")            ' "Déformée"
                Me.chk_Inerties.Text = Bloc("INERTIA")              ' "Inerties des barres"
                Me.btn_EditModel.Text = Bloc("EDITMODEL")           ' "Editer le modèle"

            Catch ex As Exception

            End Try
        End If

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

        Me.TLPan_PartieBasse.ColumnStyles(1).Width = 0
        Me.TLPan_PartieBasse.ColumnStyles(2).Width = 0

    End Sub

    Private Sub PrepareFenetre()

        With DiagrammesNDC
            .lDessDeformee = True     ' Affichage de la déformée
            .lDessMoment = True       ' Affichage diagramme moments
            .lDessEffortT = True      ' Affichage diagramme efforts tranchants
            .lDessInerties = True     ' Affichage des inerties
            .lDessNumeros = False     ' Affichage des numéros noeuds
            .lDessCharges = False     ' Affichage des charges
            .lDessEchLocal = False

            Me.chk_Fleches.Checked = .lDessDeformee
            Me.chk_Moment.Checked = .lDessMoment
            Me.chk_EffortTranchant.Checked = .lDessEffortT
            Me.chk_Numerotation.Checked = .lDessNumeros
            Me.chk_Inerties.Checked = .lDessInerties
            Me.chk_Chargement.Checked = .lDessCharges
            Me.chk_LocalEchelle.Checked = .lDessEchLocal

        End With
    End Sub

    Private Sub AfficheCasdeCharge()

        MyProjet.Poutres(MyProjet.IndEnCours).InitialiseCalculs(NomChargesA)
        MyProjet.Poutres(MyProjet.IndEnCours).AAA_CalculMNVInternesN()
        DiagrammesNDC.InitialiseVariablesLocales()

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

                Me.txt_Mmax.Text = GetStringInUnit(Me.DiagrammesNDC.tab_Mmax(Me.cmb_Symbols.SelectedIndex), Enu_TypeVariable.Moment, 3, 3, False)
                Me.txt_Mmin.Text = GetStringInUnit(Me.DiagrammesNDC.tab_Mmin(Me.cmb_Symbols.SelectedIndex), Enu_TypeVariable.Moment, 3, 3, False)
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
       MyProjet.Poutres(MyProjet.IndEnCours), DiagrammesNDC, Me.cmb_Symbols.SelectedIndex)
    End Sub


#End Region

#Region " Evènements "

    Private Sub chk_Fleches_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Fleches.CheckedChanged
        If lBuild Then Exit Sub

        DiagrammesNDC.lDessDeformee = Me.chk_Fleches.Checked

        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub chk_Moment_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Moment.CheckedChanged
        If lBuild Then Exit Sub

        DiagrammesNDC.lDessMoment = Me.chk_Moment.Checked

        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub chk_EffortTranchant_CheckedChanged(sender As Object, e As EventArgs) Handles chk_EffortTranchant.CheckedChanged
        If lBuild Then Exit Sub

        DiagrammesNDC.lDessEffortT = Me.chk_EffortTranchant.Checked

        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub chk_Numerotation_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Numerotation.CheckedChanged
        If lBuild Then Exit Sub

        DiagrammesNDC.lDessNumeros = Me.chk_Numerotation.Checked

        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub chk_Inerties_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Inerties.CheckedChanged
        If lBuild Then Exit Sub

        DiagrammesNDC.lDessInerties = Me.chk_Inerties.Checked

        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub chk_Chargement_CheckedChanged(sender As Object, e As EventArgs) Handles chk_Chargement.CheckedChanged
        If lBuild Then Exit Sub

        DiagrammesNDC.lDessCharges = Me.chk_Chargement.Checked

        Me.img_Analyse.Invalidate()
    End Sub

    Private Sub btn_EditModel_Click(sender As Object, e As EventArgs) Handles btn_EditModel.Click
        ABB_EditeModeleCalcul(MyProjet.Poutres(MyProjet.IndEnCours), MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(Me.cmb_Symbols.SelectedIndex).IndElts)
    End Sub

    Private Sub chk_LocalEchelle_CheckedChanged(sender As Object, e As EventArgs) Handles chk_LocalEchelle.CheckedChanged
        If lBuild Then Exit Sub

        DiagrammesNDC.lDessEchLocal = Me.chk_LocalEchelle.Checked

        Me.img_Analyse.Invalidate()
    End Sub

#End Region

End Class