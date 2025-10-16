Public Class Frm_DalleSlimFloorNBac

#Region " Variables "

    Dim lBuild As Boolean

    Dim lCofraPlus220 As Boolean

#End Region

#Region "===OUVERTURE==="


    Public Sub InitialiseFenetre(myBloc As Dictionary(Of String, String))

        lBuild = True
        'lInter = plInter

        GestionStyle()
        GestionLangues(myBloc)
        GestionUnites()

        AfficheDalleEnCours()

        lBuild = False

    End Sub


    Private Sub GestionLangues(myBloc As Dictionary(Of String, String))

        Dim strLoadedKey As String = ""
        Dim CLE As String = ""

        Try

            '** BAC

            CLE = "SHEETING" : Me.lbl_Bac.Text = myBloc("SHEETING")
            CLE = "NAME" : Me.lbl_BacNom.Text = myBloc("NAME")
            CLE = "MODIFYSH" : Me.btn_ModifierBac.Text = myBloc("MODIFYSH")

            CLE = "HEIGHT" : Me.lbl_HauteurHp.Text = myBloc("HEIGHT")



        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)
        End Try


    End Sub

    Private Sub GestionStyle()

        Me.pan_Main.Dock = DockStyle.Fill

        Me.lbl_Bac.BackColor = CouleurBackBandeaux
        Me.lbl_Bac.ForeColor = CouleurForeBandeaux

        PrepareTextBoxDipo(txt_Hp, False)

        Me.img_Bac.BorderStyle = BorderStyle.FixedSingle

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

    End Sub

    Private Sub AfficheDalleEnCours()
        AfficheNomBacEnCours()
    End Sub

    Private Sub InitialiseVariablesLocales()

        lCofraPlus220 = Frm_DalleSlimFloorN.localDalle.Bac.lCofraplus220

    End Sub

#End Region

#Region " Dessin du bac "

    Private Sub img_Bac_Paint(sender As Object, e As PaintEventArgs) Handles img_Bac.Paint
        Const kADJS As Decimal = 0.95
        DessineBacTout(e.Graphics, Me.img_Bac.ClientRectangle.Width, Me.img_Bac.ClientRectangle.Height,
                       Frm_DalleSlimFloorN.localDalle.Bac, True, kADJS, False, FontBase, "")
    End Sub

#End Region

#Region " Modification du bac "

    Private Sub btn_ModifierBac_Click(sender As Object, e As EventArgs) Handles btn_ModifierBac.Click, txt_BacNom.Click, img_Bac.Click

        Dim Tc As Decimal = Frm_DalleSlimFloorN.localDalle.EpaisseurActive
        Dim lOldCfp220 As Boolean = lCofraPlus220

        iFrmAppel = EnuFenetres.DalleSlimFloor
        Frm_BacN.ShowDialog()

        If lCofraPlus220 <> lOldCfp220 Then
            Dim Td As Decimal = Tc + Frm_DalleSlimFloorN.localDalle.Bac.Hp
            Frm_DalleSlimFloorN.localDalle.Ep_td = Td
            'Me.txt_Hd.Text = GetStringNoUnit(Frm_DalleSlimFloorN.localDalle.Ep_td, Enu_TypeVariable.Dimension)
        End If

        AfficheNomBacEnCours()
        Me.img_Bac.Invalidate()
        Frm_DalleSlimFloorN.RedessineDalle()

    End Sub

    Private Sub AfficheNomBacEnCours()
        Me.txt_BacNom.Text = Frm_DalleSlimFloorN.localDalle.Bac.Etiquette
        Me.txt_Hp.Text = GetStringInUnit(Frm_DalleSlimFloorN.localDalle.Bac.Hp, Enu_TypeVariable.Dimension, 4, 3, False)
    End Sub

#End Region


#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_Hp.Paint

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

            Case Me.img_Hp.Name
                strSymbol = "h"
                strIndice = "p"


        End Select

        '--> Dessin

        DrawSymbolN(e.Graphics, Brushes.Black, strSymbol, strIndice, sWI, sHI, lGrec, lIndice, AlignH,
                    FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

End Class