Imports PMXMoteur2

Public Class Frm_DalleNArma

#Region " Variables locales "

    Dim lBuild As Boolean
    Dim strLitNo(1) As String
    Dim strTauxArma As String
    Dim strToolTipAddRebar As String
    Dim strToolTipRemoveRebar As String

    Dim FontFrm As Font

    Dim ClasseAcierArma() As String = cls_AcierArmature.tabClasseAcierArma

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(myBloc As Dictionary(Of String, String))
        lBuild = True

        GestionStyle()
        GestionLangues(myBloc)
        GestionUnites()
        PreparerFenetre()
        AfficherDalleEnCours()
        lBuild = False

    End Sub

    Private Sub PreparerFenetre()

        RemplirComboAvecTableau(Me.cmb_Acier, ClasseAcierArma)

    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)

        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)

    End Sub

    Private Sub GestionStyle()

        Me.Pan_Contenu.Dock = DockStyle.Fill

        FontFrm = New Font(FontBase.Name, SizeFontFrm)

        Me.lbl_Armatures.BackColor = CouleurBackBandeaux
        Me.lbl_Armatures.ForeColor = CouleurForeBandeaux
        Me.lbl_Acier.BackColor = CouleurBackBandeaux
        Me.lbl_Acier.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(txt_Fsk, False)

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim6.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim7.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitSigma2.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub GestionLangues(myBloc As Dictionary(Of String, String))

        Dim strLoadedKey As String = ""
        Dim CLE As String = ""

        Try

            '=== ARMATURES ====================================================================

            CLE = "REBARS" : Me.lbl_Armatures.Text = myBloc(CLE)
            CLE = "REINFLAYER0" : Me.ToolTipDalle.SetToolTip(Me.chk_Lit0, myBloc(CLE))
            CLE = "REINFLAYER1" : Me.ToolTipDalle.SetToolTip(Me.chk_Lit1, myBloc(CLE))
            CLE = "REINFLAYER2" : Me.ToolTipDalle.SetToolTip(Me.chk_Lit2, myBloc(CLE))
            CLE = "DIAMETER" : Me.lbl_Diametre.Text = myBloc(CLE)
            CLE = "SPACING" : Me.lbl_Espacement.Text = myBloc(CLE)
            CLE = "LOCATION" : Me.lbl_zs.Text = myBloc(CLE)

            CLE = "ADDLAYER" : strToolTipAddRebar = myBloc(CLE)
            CLE = "REMOVELAYER" : strToolTipRemoveRebar = myBloc(CLE)
            CLE = "FIRSTLAYER" : strLitNo(0) = myBloc(CLE)
            CLE = "SECONDLAYER" : strLitNo(1) = myBloc(CLE)

            CLE = "REINFRATIO" : strTauxArma = myBloc(CLE)

            CLE = "NOREINFORCEMENT" : Me.lbl_NoArma.Text = myBloc(CLE)                   'Pour une poutre sans console, il n'est pas nécessaire de définir les lits d'armature"

            '=== ACIER DES ARMATURES ==========================================================

            CLE = "REBARSTEEL" : Me.lbl_Acier.Text = myBloc(CLE)        '"Reinforcement steel"
            CLE = "CLASS" : Me.lbl_ClasseA.Text = myBloc(CLE)



        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
        End Try

    End Sub

    Private Sub AfficherDalleEnCours()


    End Sub

#End Region

#Region " Routines MAJI "

    Private Sub MAJI_ProprietesAcier()
        Frm_DalleN.MyDalleLoc.AcierArmatures.MAJProprietes()
        Me.txt_Fsk.Text = GetStringNoUnit(Frm_DalleN.MyDalleLoc.AcierArmatures.FsK, Enu_TypeVariable.Contrainte)
    End Sub

#End Region

#Region " Symboles "
    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_Fy.Paint, img_zs.Paint, img_PhiS.Paint, img_esp.Paint
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
        'phis -> diamètre
        'esp  -> espacement
        'zs   -> position
        'fy   -> classe acier
        lIndice = False
        lGrec = False
        lEgal = True
        Select Case sender.name

            Case Me.img_Fy.Name
                strSymbol = "f"
                strIndice = "sk"

            Case Me.img_zs.Name
                strSymbol = "z"
                strIndice = "s1"

            Case Me.img_PhiS.Name
                strSymbol = "f"
                strIndice = "s"
                lGrec = True

            Case Me.img_esp.Name
                strSymbol = "e"
                strIndice = "s1"

        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)
    End Sub

#End Region





#Region "===FERMETURE==="



#End Region


End Class