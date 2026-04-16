Imports PMXMoteur2

Public Class Frm_MaintienBacN_Calculs

#Region " Variables "

    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(Bloc As Dictionary(Of String, String))

        lBuild = True

        GestionLangues(Bloc)
        GestionStyle()
        GestionUnites()
        'PrepareFenetre()
        AfficheResultats()

        lBuild = False

    End Sub

    Private Sub GestionStyle()

        Me.pan_Main.Dock = DockStyle.Fill

        Me.lbl_Calculs.BackColor = CouleurBackBandeaux
        Me.lbl_Calculs.ForeColor = CouleurForeBandeaux
        Me.lbl_BendingRigidityTitre.BackColor = CouleurBackBandeaux
        Me.lbl_BendingRigidityTitre.ForeColor = CouleurForeBandeaux
        Me.lbl_ShearRigidityTitre.BackColor = CouleurBackBandeaux
        Me.lbl_ShearRigidityTitre.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(Me.txt_Alpha5, False)
        PrepareTextBoxDipo(Me.txt_c, False)
        PrepareTextBoxDipo(Me.txt_c11, False)
        PrepareTextBoxDipo(Me.txt_c12, False)
        PrepareTextBoxDipo(Me.txt_c21, False)
        PrepareTextBoxDipo(Me.txt_c22, False)
        PrepareTextBoxDipo(Me.txt_K, False)
        PrepareTextBoxDipo(Me.txt_kTheta, False)
        PrepareTextBoxDipo(Me.txt_kThetaA, False)
        PrepareTextBoxDipo(Me.txt_kThetaC, False)
        PrepareTextBoxDipo(Me.txt_Sact, False)

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitSact.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

        Me.etq_UnitC11.Text = "mm/kN"
        Me.etq_Unitc12.Text = "mm/kN"
        Me.etq_Unitc21.Text = "mm/kN"
        Me.etq_Unitc22.Text = "mm/kN"

        Me.etq_UnitK.Text = ""
        Me.etq_UnitAlpha5.Text = ""

        Me.etq_UnitkTheta.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "." & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitkThetaA.Text = Me.etq_UnitkTheta.Text
        Me.etq_UnitkThetaC.Text = Me.etq_UnitkTheta.Text

    End Sub

    Private Sub GestionLangues(Bloc As Dictionary(Of String, String))

        Try

            '=== CALCULS =======================================================================

            Me.lbl_Calculs.Text = Bloc("PARAMETERS")
            Me.lbl_BendingRigidity.Text = Bloc("BENDINGSTIFF")
            Me.lbl_ShearRigidity.Text = Bloc("SHEARSTIFF")
            Me.lbl_BendingRigidityTitre.Text = Bloc("BENDINGSTIFF")
            Me.lbl_ShearRigidityTitre.Text = Bloc("SHEARSTIFF")

            'strResultats = Bloc("PARAMETERS")
            'strDessin = Bloc("DRAWING")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
        Finally

        End Try

    End Sub

#End Region

#Region " Affichage des résultats de calcul "

    Public Sub AfficheResultats()

        Const kUnitFlex As Decimal = 10 ^ 6
        Dim PorteeL As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).LongueurTravee(1)
        Dim eYoung As Decimal = cls_Acier.EYACIER
        Dim Poisson As Decimal = cls_Acier.NU
        Dim EntraxeD As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).EntraxeSolive

        Dim c11 As Decimal = Frm_MaintienBacN.localMaitienBac.Flexibilite_C11_DistorsionBac(PorteeL, EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac, eYoung)
        Dim c12 As Decimal = Frm_MaintienBacN.localMaitienBac.Flexibilite_C12_Shear(PorteeL, EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac, eYoung, Poisson)
        Dim c21 As Decimal = Frm_MaintienBacN.localMaitienBac.Flexibilite_C21_BeamFasteners(PorteeL, EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.Ep)
        Dim c22 As Decimal = Frm_MaintienBacN.localMaitienBac.Flexibilite_C22_SeamFastener(PorteeL, EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac)
        Dim cCumul As Decimal
        Dim SAct As Decimal


        Me.txt_Alpha5.Text = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.Alpha5, Enu_TypeVariable.SansType, 3, 3, NON_U, False)
        Me.txt_K.Text = GetStringInUnitN(Frm_MaintienBacN.localMaitienBac.CoefficientK(MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac), Enu_TypeVariable.SansType, 3, 3, NON_U, False)
        Me.txt_c11.Text = GetStringInUnitN(c11 * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, NON_U, False)
        Me.txt_c12.Text = GetStringInUnitN(c12 * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, NON_U, False)
        Me.txt_c21.Text = GetStringInUnitN(c21 * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, NON_U, False)
        Me.txt_c22.Text = GetStringInUnitN(c22 * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, NON_U, False)

        cCumul = c11 + c12 + c21 + c22
        SAct = PorteeL / cCumul

        Me.txt_c.Text = GetStringInUnitN(cCumul * kUnitFlex, Enu_TypeVariable.SansType, 4, 3, NON_U, False)
        Me.txt_Sact.Text = GetStringInUnitN(SAct, Enu_TypeVariable.Rigidite, 4, 3, NON_U, False)

        Dim kTheta, kThetaA, kThetaC As Decimal
        Dim bFs As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Bfs

        kThetaA = MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.RigiditeFlexionnelleA(Frm_MaintienBacN.localMaitienBac.FixNervuresMod = cls_MaintienBac.Enu_FixationNervures.Toutes, bFs)
        kThetaC = MyProjet.Poutres(MyProjet.IndEnCours).Dalle.Bac.RigiditeFlexionnelleC(EntraxeD, MyProjet.Poutres(MyProjet.IndEnCours).lIntermediaire)
        kTheta = 1 / (1 / kThetaA + 1 / kThetaC)

        Me.txt_kTheta.Text = GetStringInUnitN(kTheta, Enu_TypeVariable.Effort, 3, 2, NON_U, False)
        Me.txt_kThetaA.Text = GetStringInUnitN(kThetaA, Enu_TypeVariable.Effort, 3, 2, NON_U, False)
        Me.txt_kThetaC.Text = GetStringInUnitN(kThetaC, Enu_TypeVariable.Effort, 3, 2, NON_U, False)

    End Sub


#End Region

#Region " Symboles "

    Private Sub PaintSymbols(sender As Object, e As PaintEventArgs) _
        Handles img_K.Paint, img_c12.Paint, img_c11.Paint, img_Alpha5.Paint, img_c22.Paint, img_c21.Paint, img_c.Paint, img_Sact.Paint, img_kThetaC.Paint, img_kThetaA.Paint, img_kTheta.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height
        Dim xStart As Single = sWI * 0.95

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lIndice, lEgal As Boolean
        Dim xPen As Single = xStart
        Dim hCar As Single = e.Graphics.MeasureString("X", FontSymbolNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yPen As Single = (sHI / 2 - hCar) / 2 + sHI * 0.15
        Dim AlignH As Enu_AlignementH = Enu_AlignementH.Droite

        '--> Initialisation

        lIndice = False
        lGrec = False
        lEgal = True
        Select Case sender.name

            Case Me.img_K.Name
                strSymbol = "K"
                strIndice = ""

            Case Me.img_Alpha5.Name
                strSymbol = "a"
                strIndice = "5"
                lGrec = True

            Case Me.img_c11.Name
                strSymbol = "c"
                strIndice = "11"

            Case Me.img_c12.Name
                strSymbol = "c"
                strIndice = "12"

            Case Me.img_c21.Name
                strSymbol = "c"
                strIndice = "21"

            Case Me.img_c22.Name
                strSymbol = "c"
                strIndice = "22"

            Case Me.img_c.Name
                strSymbol = "c"
                strIndice = ""

            Case Me.img_Sact.Name
                strSymbol = "S"
                strIndice = "act"
            Case Me.img_kTheta.Name
                strSymbol = "k"
                strIndice = "theta"
            Case Me.img_kThetaA.Name
                strSymbol = "k"
                strIndice = "theta,A"
            Case Me.img_kThetaC.Name
                strSymbol = "k"
                strIndice = "theta,C"
        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub


#End Region

End Class