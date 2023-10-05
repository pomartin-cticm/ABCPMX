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

#End Region


#Region "===OUVERTURE==="
    Private Sub Frm_PPVerifications_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        MyProjet.Poutres(MyProjet.IndEnCours).AAA_Verifications()
        GestionLangues()
        GestionStyle()
        RemplirComboLS()
        RemplirComboCriteres(MyProjet.Poutres(MyProjet.IndEnCours))

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
        Me.img_Verifications.Invalidate()
    End Sub


#End Region

#Region " Dessin "

    Private Sub img_Verifications_Paint(sender As Object, e As PaintEventArgs) Handles img_Verifications.Paint

    End Sub

#End Region


End Class