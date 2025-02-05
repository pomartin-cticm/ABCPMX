Public Class Frm_ModularN_ResultatsG1

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
        Me.pan_AnnexB.Dock = DockStyle.Fill
        MyFontNormal = New Font(Me.txt_BetaC.Font.Name, Me.txt_BetaC.Font.Size)

        GestionLangue(Bloc)
        GestionStyle()

        lBuild = False

    End Sub

    Private Sub GestionStyle()

        PrepareTextBoxDipo(Me.txt_BetaC, False)
        PrepareTextBoxDipo(Me.txt_BetaFcm, False)
        PrepareTextBoxDipo(Me.txt_BetaT0, False)
        PrepareTextBoxDipo(Me.txt_Phi0, False)
        PrepareTextBoxDipo(Me.txt_PhiRH, False)
        PrepareTextBoxDipo(Me.txt_PhiT, False)

    End Sub

    Private Sub GestionLangue(Bloc As Dictionary(Of String, String))

        Me.lbl_BetaC.Text = Bloc("BETAC")
        Me.lbl_BetaFcm.Text = Bloc("BETAFCM")
        Me.lbl_BetaT0.Text = Bloc("BETAT0")
        Me.lbl_Phi0.Text = Bloc("PHI0")
        Me.lbl_PhiRH.Text = Bloc("PHIRH")
        Me.lbl_PhiT.Text = Bloc("PHIT")

    End Sub

    Public Sub AfficherResultats(PhiRh As Decimal, BetaFcm As Decimal, BetaT0 As Decimal, Phi0 As Decimal, BetaC As Decimal, PhiT As Decimal)

        Me.txt_PhiRH.Text = GetStringInUnitN(PhiRh, Enu_TypeVariable.SansType, 3, 2, NON_U, False)


        Me.txt_BetaFcm.Text = GetStringInUnitN(BetaFcm, Enu_TypeVariable.SansType, 3, 2, NON_U, False)


        Me.txt_BetaT0.Text = GetStringInUnitN(BetaT0, Enu_TypeVariable.SansType, 3, 2, NON_U, False)


        Me.txt_Phi0.Text = GetStringInUnitN(Phi0, Enu_TypeVariable.SansType, 3, 2, NON_U, False)


        Me.txt_BetaC.Text = GetStringInUnitN(BetaC, Enu_TypeVariable.SansType, 3, 2, NON_U, False)


        Me.txt_PhiT.Text = GetStringInUnitN(PhiT, Enu_TypeVariable.SansType, 3, 2, NON_U, False)



    End Sub

#End Region

#Region " Dessin des expressions (symboles) "

    Private Sub PaintExpression(sender As Object, e As PaintEventArgs) Handles img_PhiRH.Paint

        Dim myFormul As String = ""

        ' myFormul = "σ\-w\- = B\-ω\- / I\-w\- [MPa]"
        myFormul = "\Sj\s\-RH\= ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_PhiRH.ClientRectangle.Width, Me.img_PhiRH.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub img_BetaFcm_Paint(sender As Object, e As PaintEventArgs) Handles img_BetaFcm.Paint
        ' myFormul = "σ\-w\- = B\-ω\- / I\-w\- [MPa]"
        Dim myFormul As String = "\Sb\s(" & strItal & "f\i\-cm\=) ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaFcm.ClientRectangle.Width, Me.img_BetaFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub img_PhiT_Paint(sender As Object, e As PaintEventArgs) Handles img_PhiT.Paint

        Dim myFormul As String = "\Sj\s(" & strItal & "t\i, " & strItal & "t\i\-0\=) ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaFcm.ClientRectangle.Width, Me.img_BetaFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub imgBetaC_Paint(sender As Object, e As PaintEventArgs) Handles imgBetaC.Paint

        Dim myFormul As String = "\Sb\s\-c\=(" & strItal & "t\i, " & strItal & "t\i\-0\=) ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaFcm.ClientRectangle.Width, Me.img_BetaFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub img_Phi0_Paint(sender As Object, e As PaintEventArgs) Handles img_Phi0.Paint

        Dim myFormul As String = "\Sj\s\-0\= ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaFcm.ClientRectangle.Width, Me.img_BetaFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub

    Private Sub img_BetaT0_Paint(sender As Object, e As PaintEventArgs) Handles img_BetaT0.Paint

        Dim myFormul As String = "\Sb\s(" & strItal & "t\i\-0\=) ="

        DrawExpression(e.Graphics, Brushes.Black, myFormul, Me.img_BetaFcm.ClientRectangle.Width, Me.img_BetaFcm.ClientRectangle.Height, MyFontNormal, Enu_AlignementH.Droite)

    End Sub



#End Region




End Class