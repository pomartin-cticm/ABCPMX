Public Class Frm_OptionsCalculConnecteurs


#Region " Variables locales "


    Const BALISE As String = "OPTCALCONNECTORS"

    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiserFrm()
        lBuild = True
        GestionLangue(Frm_OptionsCalcul.BlocLangues(BALISE))
        GestionStyle()
        GestionUnites()
        AfficherValeurs()
        lBuild = False
    End Sub

    Private Sub GestionStyle()

        Me.pan_General.Dock = DockStyle.Fill
        Me.pan_Conteneur.Dock = DockStyle.Fill

        Me.lbl_Connecteurs.BackColor = CouleurBackBandeaux
        Me.lbl_Connecteurs.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(Me.txt_DMin, False)
        PrepareTextBoxDipo(Me.txt_DMax1, False)
        PrepareTextBoxDipo(Me.txt_DMax2, False)
        PrepareTextBoxDipo(Me.txt_DMax3, False)
        PrepareTextBoxDipo(Me.txt_eD, False)
        PrepareTextBoxDipo(Me.txt_HsurD_Bac, False)
        PrepareTextBoxDipo(Me.txt_RatioHsurD1, False)
        PrepareTextBoxDipo(Me.txt_RatioHsurD2, False)
        PrepareTextBoxDipo(Me.txt_SxMax1, False)
        PrepareTextBoxDipo(Me.txt_SxMax2, False)
        PrepareTextBoxDipo(Me.txt_syMin1, False)
        PrepareTextBoxDipo(Me.txt_syMin2, False)

    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try
            Me.lbl_Connecteurs.Text = MyBloc("TITLE")

            Me.lbl_Dimensions.Text = MyBloc("DIMENSIONS")
            Me.lbl_DiameterMin.Text = MyBloc("DIAMIN")
            Me.lbl_DiameterMax.Text = MyBloc("DIAMAX")

            Me.lbl_DMaxDefaut.Text = MyBloc("DEFAULT")
            Me.lbl_BacPreperce.Text = MyBloc("DMAXPREPUNCHED")
            Me.lbl_BacWT.Text = MyBloc("DMAXWELDEDTHROUGH")

            Me.lbl_RatioHsurDMax.Text = MyBloc("RATIOH_D")
            Me.lbl_Generation1.Text = MyBloc("EUROCODEGEN1")
            Me.lbl_Generation2.Text = MyBloc("EUROCODEGEN2")

            Me.lbl_Details.Text = MyBloc("DETAILING")
            Me.lbl_HauteurSurBac.Text = MyBloc("HEIGHTABOVESHEETING")
            Me.lbl_DistanceAuBord.Text = MyBloc("EDGEDISTANCE")

            Me.lbl_Espacements.Text = MyBloc("SPACING")
            Me.lbl_LongitudinalSX.Text = MyBloc("SPACINGLONGI")

            Me.lbl_LongitudinalSY.Text = MyBloc("SPACINGTRANS")
            Me.lbl_DalleMixte.Text = MyBloc("COMPOSITESLAB")
            Me.lbl_DallePleine.Text = MyBloc("SOLIDSLAB")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitD1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitD6.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

    End Sub

    Private Sub AfficherValeurs()

        Me.txt_DMin.Text = GetStringInUnitN(GOUJ_DMIN, Enu_TypeVariable.Dimension, 4, 3, False, True)
        Me.txt_DMax1.Text = GetStringInUnitN(GOUJ_DMAXDEF, Enu_TypeVariable.Dimension, 4, 3, False, True)
        Me.txt_DMax2.Text = GetStringInUnitN(GOUJ_DMAXPERPREP, Enu_TypeVariable.Dimension, 4, 3, False, True)
        Me.txt_DMax3.Text = GetStringInUnitN(GOUJ_DMAXPERWT, Enu_TypeVariable.Dimension, 4, 3, False, True)

        Me.txt_RatioHsurD1.Text = GetStringInUnitN(GOUJ_RAPHsurDMIN_G1, Enu_TypeVariable.SansType, 4, 3, False, True)
        Me.txt_RatioHsurD2.Text = GetStringInUnitN(GOUJ_RAPHsurDMIN_G2, Enu_TypeVariable.SansType, 4, 3, False, True)

        Me.txt_HsurD_Bac.Text = GetStringInUnitN(GOUJ_RAPHsurDSURBAC, Enu_TypeVariable.SansType, 4, 3, False, True)
        Me.txt_eD.Text = GetStringInUnitN(GOUJ_DBORD_MIN, Enu_TypeVariable.Dimension, 4, 3, False, True)

        Me.txt_SxMax1.Text = GetStringInUnitN(GOUJ_RAPESPX, Enu_TypeVariable.Dimension, 4, 3, False, True)
        Me.txt_SxMax2.Text = GetStringInUnitN(GOUJ_RAPESPXsurTD_MAX, Enu_TypeVariable.SansType, 4, 3, False, True)

        Me.txt_syMin1.Text = GetStringInUnitN(GOUJ_RAPESPYsurD_PLEINE_MIN, Enu_TypeVariable.SansType, 4, 3, False, True)
        Me.txt_syMin2.Text = GetStringInUnitN(GOUJ_RAPESPYsurD_MIXTE_MIN, Enu_TypeVariable.SansType, 4, 3, False, True)

    End Sub

#End Region

#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_Dmin.Paint,
        img_DMax1.Paint, img_DMax3.Paint, img_DMax2.Paint, img_RatioHsurD2.Paint, img_RatioHsurD1.Paint, img_HsurD.Paint,
        img_eD.Paint, img_Td.Paint, img_sx2.Paint, img_sx1.Paint, img_xd2.Paint, img_xd1.Paint, img_sy2.Paint, img_sy1.Paint

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
        lEgal = False
        AlignH = Enu_AlignementH.Droite

        Select Case sender.name
            Case Me.img_DMax1.Name, Me.img_DMax2.Name, Me.img_DMax3.Name, Me.img_Dmin.Name
                strSymbol = "d"
                strIndice = ""
                lEgal = False
            Case Me.img_RatioHsurD1.Name, Me.img_RatioHsurD2.Name
                strSymbol = "h/d"
                strIndice = ""
                lEgal = False
            Case Me.img_eD.Name
                strSymbol = "e"
                strIndice = "D"
                lEgal = False
            Case Me.img_HsurD.Name
                strSymbol = "h/d"
                strIndice = ""
                lEgal = False
            Case Me.img_sx1.Name, Me.img_sx2.Name
                strSymbol = "s"
                strIndice = "x"
                lEgal = False
            Case Me.img_Td.Name
                strSymbol = "x t"
                strIndice = "d"
                lEgal = False
                AlignH = Enu_AlignementH.Gauche
            Case Me.img_sy1.Name, Me.img_sy2.Name
                strSymbol = "s"
                strIndice = "y"
                lEgal = False
            Case Me.img_xd1.Name, Me.img_xd2.Name
                strSymbol = "x d"
                strIndice = ""
                lEgal = False
                AlignH = Enu_AlignementH.Gauche
        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

End Class