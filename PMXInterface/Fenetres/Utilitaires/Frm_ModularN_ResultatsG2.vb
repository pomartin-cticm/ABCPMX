Public Class Frm_ModularN_ResultatsG2

#Region " Déclarations "
    Const strItal As String = "\i"

#End Region

#Region " Variables "

    Dim lBuild As Boolean
    Dim MyFontNormal As Font

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFenetre(Bloc As Dictionary(Of String, String))

        lBuild = True
        Me.pan_ResultsAnnexB.Dock = DockStyle.Top
        MyFontNormal = New Font(Me.txt_BetaC_Bc.Font.Name, Me.txt_BetaC_Bc.Font.Size)

        GestionLangue(Bloc)
        GestionStyle()

        lBuild = False

    End Sub

    Private Sub GestionStyle()

        PrepareTextBoxDipo(Me.txt_BetaT0, False)

        PrepareTextBoxDipo(Me.txt_BetaC_Bc, False)
        PrepareTextBoxDipo(Me.txt_BetaC_Dc, False)

        PrepareTextBoxDipo(Me.txt_BetaBcFcm, False)
        PrepareTextBoxDipo(Me.txt_BetaDcFcm, False)
        PrepareTextBoxDipo(Me.txt_PhiRH, False)
        PrepareTextBoxDipo(Me.txt_PhiT, False)
        PrepareTextBoxDipo(Me.txt_PhiTBc, False)
        PrepareTextBoxDipo(Me.txt_PhiTDc, False)

    End Sub

    Private Sub GestionLangue(Bloc As Dictionary(Of String, String))

        Me.lbl_BetaC.Text = Bloc("BETAC")
        Me.lbl_BetaFcm.Text = Bloc("BETAFCM")
        Me.lbl_BetaT0.Text = Bloc("BETAT0")
        Me.lbl_PhiRH.Text = Bloc("PHIRH")
        Me.lbl_PhiT.Text = Bloc("PHIT")

    End Sub

    Public Sub AfficherResultats(PhiT As Decimal, PhiTBc As Decimal, PhiTDc As Decimal, BetaDcRH As Decimal, BetaBcFcm As Decimal, BetaDcFcm As Decimal,
                                 BetaCBc As Decimal, BetaCDc As Decimal, BetaDcT0 As Decimal)

        Me.txt_PhiRH.Text = GetStringInUnitN(BetaDcRH, Enu_TypeVariable.SansType, 3, 2, NON_U, False)


        Me.txt_BetaBcFcm.Text = GetStringInUnitN(BetaBcFcm, Enu_TypeVariable.SansType, 3, 2, NON_U, False)
        Me.txt_BetaDcFcm.Text = GetStringInUnitN(BetaDcFcm, Enu_TypeVariable.SansType, 3, 2, NON_U, False)

        Me.txt_BetaT0.Text = GetStringInUnitN(BetaDcT0, Enu_TypeVariable.SansType, 3, 2, NON_U, False)


        'Me.txt_Phi0.Text = GetStringInUnitN(Phi0, Enu_TypeVariable.SansType, 3, 2, NON_U, False)


        Me.txt_BetaC_Bc.Text = GetStringInUnitN(BetaCBc, Enu_TypeVariable.SansType, 3, 2, NON_U, False)
        Me.txt_BetaC_Dc.Text = GetStringInUnitN(BetaCDc, Enu_TypeVariable.SansType, 3, 2, NON_U, False)

        Me.txt_PhiT.Text = GetStringInUnitN(PhiT, Enu_TypeVariable.SansType, 3, 2, NON_U, False)
        Me.txt_PhiTBc.Text = GetStringInUnitN(PhiTBc, Enu_TypeVariable.SansType, 3, 2, NON_U, False)
        Me.txt_PhiTDc.Text = GetStringInUnitN(PhiTDc, Enu_TypeVariable.SansType, 3, 2, NON_U, False)

    End Sub


#End Region

#Region " Dessin des expressions (symboles) "

    Private Sub img_PhiDc_Paint(sender As Object, e As PaintEventArgs) Handles img_PhiDc.Paint, img_PhiBc.Paint

        Dim Indice As String = ""

        Dim myFormul As String

        Select Case sender.name
            Case img_PhiDc.Name
                Indice = "d"
            Case img_PhiBc.Name
                Indice = "b"
        End Select
        myFormul = "\Sb\s\-" & Indice & "c\=(" & strItal & "t\i, " & strItal & "t\i\-0\=) ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaBcFcm.ClientRectangle.Width, Me.img_BetaBcFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub img_PhiT_Paint(sender As Object, e As PaintEventArgs) Handles img_PhiT.Paint

        Dim myFormul As String = "\Sj\s(" & strItal & "t\i, " & strItal & "t\i\-0\=) ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaBcFcm.ClientRectangle.Width, Me.img_BetaBcFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub img_PhiRH_Paint(sender As Object, e As PaintEventArgs) Handles img_PhiRH.Paint
        Dim myFormul As String = ""

        myFormul = "\Sb\s\-dc,RH\= ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_PhiRH.ClientRectangle.Width, Me.img_PhiRH.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub img_BetaDcFcm_Paint(sender As Object, e As PaintEventArgs) Handles img_BetaDcFcm.Paint, img_BetaBcFcm.Paint

        Dim Indice As String = ""

        Dim myFormul As String

        Select Case sender.name
            Case img_BetaDcFcm.Name
                Indice = "d"
            Case img_BetaBcFcm.Name
                Indice = "b"
        End Select
        myFormul = "\Sb\s\-" & Indice & "c,fcm\= ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaBcFcm.ClientRectangle.Width, Me.img_BetaBcFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)


    End Sub

    Private Sub img_BetaC_Dc_Paint(sender As Object, e As PaintEventArgs) Handles img_BetaC_Bc.Paint, img_BetaC_Dc.Paint
        Dim Indice As String = ""

        Dim myFormul As String

        Select Case sender.name
            Case img_BetaC_Dc.Name
                Indice = "d"
            Case img_BetaC_Bc.Name
                Indice = "b"
        End Select
        myFormul = "\Sb\s\-" & Indice & "c,t-t0\= ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaBcFcm.ClientRectangle.Width, Me.img_BetaBcFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub img_BetaT0_Paint(sender As Object, e As PaintEventArgs) Handles img_BetaT0.Paint
        Dim myFormul As String = ""

        myFormul = "\Sb\s\-dc,t0\= ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_PhiRH.ClientRectangle.Width, Me.img_PhiRH.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub


#End Region

End Class