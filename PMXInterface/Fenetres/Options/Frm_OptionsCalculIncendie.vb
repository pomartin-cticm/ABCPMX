Public Class Frm_OptionsCalculIncendie

#Region " Attributs "

    Dim lBuild As Boolean
    Const BALISE As String = "OPTCALFIRE"


#End Region

#Region "===Ouverture==="

    Public Sub InitialiserFenetre()
        lBuild = True
        GestionLangue(Frm_OptionsCalcul.BlocLangues(BALISE))
        GestionStyle()
        GestionUnites()
        AfficherOptionsEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Incendie.Text = MyBloc("TITLE")

            Me.lbl_Constantes.Text = MyBloc("CONSTANTS")
            Me.lbl_Boltzman.Text = MyBloc("BOLTZMAN")

        Catch ex As Exception
            MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_Incendie.Dock = DockStyle.Fill

        Me.lbl_Incendie.BackColor = CouleurBackBandeaux
        Me.lbl_Incendie.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitBoltzman.Text = LogicielInfo.Unit_ModulesY(LogicielOptions.IndUnitModulesY)
        Me.etq_UnitL1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

    End Sub

    Private Sub AfficherOptionsEnCours()

    End Sub

#End Region



#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_Sigma.Paint, img_dNodes.Paint, img_T0.Paint, img_EpsilonF.Paint, img_EpsilonA.Paint, img_Deltat.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lIndice, lEgal As Boolean
        Dim AlignH As Enu_AlignementH

        '--> Initialisation

        lIndice = False
        lGrec = False
        lEgal = True
        AlignH = Enu_AlignementH.Droite

        Select Case sender.name
            Case Me.img_Sigma.Name
                strSymbol = "s"
                strIndice = ""
                lEgal = True
                lGrec = True
                'AlignH = Enu_AlignementH.Droite
            Case Me.img_dNodes.Name
                strSymbol = "d"
                strIndice = ""
                lEgal = False
            Case Me.img_T0.Name
                strSymbol = "q"
                strIndice = "0"
                lGrec = True
            Case Me.img_EpsilonA.Name
                strSymbol = "e"
                strIndice = "m"
                lGrec = True
            Case Me.img_EpsilonF.Name
                strSymbol = "e"
                strIndice = "f"
                lGrec = True
            Case Me.img_Deltat.Name
                strSymbol = "D"
                strIndice = "t"
                lGrec = True
        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region


#Region "   Dessin des unités spéciales"

    Private Sub img_UnitBoltzmann_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_UnitBoltzmann.Paint
        DrawUnitBoltzmann(e.Graphics, Me.img_UnitBoltzmann.ClientRectangle.Width, Me.img_UnitBoltzmann.ClientRectangle.Height)
    End Sub

    Private Sub DrawUnitBoltzmann(ByVal MyGr As Graphics, ByVal sWI As Single, ByVal sHI As Single)
        '----------------------------------------------------------------------------------------
        '   30/09/09 :  Création - Version 2.00
        '----------------------------------------------------------------------------------------
        '   Affiche unités cte de Boltzman
        '----------------------------------------------------------------------------------------

        Dim Chaine As String
        Dim xPen, yPen As Single
        Dim sCar, xDec, hDec As Single
        Dim FontNormal As New Font(Me.txt_Sigma.Font.Name, 8.25)
        Dim FontExp As New Font(Me.txt_Sigma.Font.Name, 6.25)
        Const kMatch As Single = 0.93

        Chaine = "x10"
        sCar = MyGr.MeasureString(Chaine, FontNormal).Height
        xDec = MyGr.MeasureString(Chaine, FontNormal).Width

        yPen = (sHI / 2 - sCar) / 2
        xPen = 1

        MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

        xPen += kMatch * xDec
        hDec = sCar / 4

        Chaine = "-8"
        xDec = MyGr.MeasureString(Chaine, FontExp).Width

        MyGr.DrawString(Chaine, FontExp, Brushes.Black, xPen, yPen - hDec)


        xPen += kMatch * xDec
        Chaine = " W/m"

        xDec = MyGr.MeasureString(Chaine, FontNormal).Width

        MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

        xPen += kMatch * xDec

        Chaine = "2"
        xDec = MyGr.MeasureString(Chaine, FontExp).Width

        MyGr.DrawString(Chaine, FontExp, Brushes.Black, xPen, yPen - hDec)

        xPen += kMatch * xDec
        Chaine = "K"

        xDec = MyGr.MeasureString(Chaine, FontNormal).Width

        MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

        xPen += kMatch * xDec

        Chaine = "4"

        MyGr.DrawString(Chaine, FontExp, Brushes.Black, xPen, yPen - hDec)

        FontNormal.Dispose()
        FontExp.Dispose()
    End Sub

    'Private Sub img_TempReference_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_TempReference.Paint
    '    DrawSymbol(e.Graphics, Brushes.Black, "q", "ref", 1, 1, 0, True, Enu_Alignement.Centre, MyFontNormal, MyFontNormal, MyFontNormal, 0.95)
    'End Sub


    Private Sub img_UnitThermConvection_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_UnitThermConvection.Paint
        DrawUnitConvection(e.Graphics, Me.img_UnitThermConvection.ClientRectangle.Width, Me.img_UnitThermConvection.ClientRectangle.Height)
    End Sub

    Private Sub DrawUnitConvection(ByVal MyGr As Graphics, ByVal sWI As Single, ByVal sHI As Single)
        '----------------------------------------------------------------------------------------
        '   30/09/09 :  Création - Version 2.00
        '----------------------------------------------------------------------------------------
        '   Affiche unités cte de Boltzman
        '----------------------------------------------------------------------------------------

        Dim Chaine As String
        Dim xPen, yPen As Single
        Dim sCar, xDec, hDec As Single
        Dim FontNormal As New Font(Me.txt_Sigma.Font.Name, 8.25)
        Dim FontExp As New Font(Me.txt_Sigma.Font.Name, 6.25)
        Const kMatch As Single = 0.97

        Chaine = "W/m"
        sCar = MyGr.MeasureString(Chaine, FontNormal).Height
        xDec = MyGr.MeasureString(Chaine, FontNormal).Width

        yPen = (sHI - sCar) / 2
        xPen = 1

        MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

        xPen += kMatch * xDec
        hDec = sCar / 4

        Chaine = "2"
        xDec = MyGr.MeasureString(Chaine, FontExp).Width

        MyGr.DrawString(Chaine, FontExp, Brushes.Black, xPen, yPen - hDec)


        xPen += kMatch * xDec
        Chaine = "K"

        xDec = MyGr.MeasureString(Chaine, FontNormal).Width

        MyGr.DrawString(Chaine, FontNormal, Brushes.Black, xPen, yPen)

        FontNormal.Dispose()
        FontExp.Dispose()
    End Sub

#End Region


End Class