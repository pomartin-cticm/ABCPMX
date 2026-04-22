Imports PMXMoteur2

Public Class Frm_OptionsFeuN_CalculSlim


#Region " Variables locales "

    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(BlocL As Dictionary(Of String, String))
        lBuild = True

        GestionStyle()
        GestionLangues(BlocL)
        GestionUnites()

        Me.pan_General.Dock = DockStyle.Fill

        AffichePoutreEnCours(Frm_OptionsFeuN.BeamLoc)

        lBuild = False
    End Sub

    Private Sub GestionStyle()

        Me.lbl_CalculOptions.BackColor = CouleurBackBandeaux
        Me.lbl_CalculOptions.ForeColor = CouleurForeBandeaux

        btn_PostTraitementEchauff.Enabled = MyProjet.Poutres(MyProjet.IndEnCours).lCalculOK And MyProjet.Poutres(MyProjet.IndEnCours).lSlimFloor
        'btn_PostTraitementEchauff.Enabled = MyProjet.Poutres(MyProjet.IndEnCours).lCalculCharge And MyProjet.Poutres(MyProjet.IndEnCours).lSlimFloor

    End Sub

    Private Sub GestionUnites()

        lbl_UnitD1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        PrepareTextBoxDipo(Me.txt_tbEff2D, False)

    End Sub

    Private Sub GestionLangues(Bloc As Dictionary(Of String, String))

        Try

            '--> chk_ReductionConcreteStrenght

            '=== OPTIONS DE CALCUL ==============================================================='

            Me.lbl_CalculOptions.Text = Bloc("CALCULOPTIONS")


            Me.lbl_Beff2D.Text = "Largeur de dalle effet 2D"

            Me.btn_PostTraitementEchauff.Text = Bloc("POSTPROCESSHEATING")


            'Me.lbl_Beff2D.Text = Bloc("SIZEELT")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try

    End Sub


    Private Sub AffichePoutreEnCours(myBeam As cls_Poutre)

        Me.txt_tbEff2D.Text = GetStringInUnitN(myBeam.ParamFeu.bEffect2D, Enu_TypeVariable.Dimension, 3, 2, Enu_AfficheUnite.Non, True)

    End Sub


#End Region

#Region " Evènements "

    Private Sub btn_Maillage_Click(sender As Object, e As EventArgs) Handles btn_PostTraitementEchauff.Click

        Frm_MaillageSlim.ShowDialog()

    End Sub


#End Region

#Region " Dessins des symboles "

    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_bEff2D.Paint

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

        '--> Initialisation

        lIndice = True
        lGrec = True
        lEgal = True

        Select Case sender.name

            Case Me.img_bEff2D.Name

                strSymbol = "b"
                strIndice = "eff2D"

                lGrec = False

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

End Class