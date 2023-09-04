Imports PMXMoteur2

Public Class Frm_LargeurEfficace

#Region " Attributs "

    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="


    Private Sub Frm_LargeurEfficace_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True
        InitialiserFenetre()
        lBuild = False
    End Sub

    Private Sub InitialiserFenetre()

        Me.Icon = Frm_PMX.Icon
        Me.img_Portees.Dock = DockStyle.Fill

    End Sub

    Private Sub GestionStyle()


    End Sub


#End Region

#Region " Dessins "

    Private Sub img_Portees_Paint(sender As Object, e As PaintEventArgs) Handles img_Portees.Paint
        DessineLargeurEff(e.Graphics, Me.img_Portees.ClientRectangle.Width, Me.img_Portees.ClientRectangle.Height, MyProjet.Poutres(MyProjet.IndEnCours), 0)
    End Sub

    Public Sub DessineLargeurEff(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyPoutre As cls_Poutre,
                                 iSelect As Integer, ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   11/08/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Représentation des largeurs efficaces
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   MyPoutre    [E] :   Poutre à dessiner
        '   iSelect     [E] :   Indice de la cote selectionnée
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '-----------------------------------------------------------------------------------------------

        '--> Declarations

        Dim MyParAff As Struc_Affichage
        Dim xMin, yMin, xMax, yMax As Double
        Dim dCar As Double
        Const kADJUST As Decimal = 0.95
        Dim MyPenP As New Pen(Color.Red, 2)
        Dim MyPenBeff As New Pen(Color.Red, 1)
        Dim MyPenPi As New Pen(Color.DarkGray, 2)
        Dim MyPenD As New Pen(Color.Black, 1)
        Dim MyPenApp As New Pen(Color.DarkGreen, 1.5)
        Dim MyPenSec As New Pen(Color.Gray, 0.5)
        Dim xe, ye As Decimal
        Dim xo, yo As Decimal
        Dim MyColor As Color
        Dim lContour As Boolean = lCONTOURCOTE
        Dim MyFontNormal As Font = FontBase

        Dim MyPenC As New Pen(Color.Black, 0.8)
        Dim yCote As Decimal
        Dim Chaine As String
        Dim xCote As Decimal

        Dim Beff(,) As Decimal

        ' Dim dashValues As Single() = {5, 2, 15, 4}
        Dim MyPenDot As New Pen(Color.Black, 0.75)
        MyPenDot.DashPattern = {2, 1, 3, 1}
        Dim Longueur As Decimal = MyPoutre.LongueurTotale

        '--> Preparation de la zone d'affichage - Calcul de ParAff

        dCar = Math.Sqrt((Longueur) ^ 2 + (MyPoutre.EntraxeD1 + MyPoutre.EntraxeD2) ^ 2) / 20


        xMin = 0 - dCar
        xMax = Longueur + dCar

        yMin = -MyPoutre.EntraxeD2 - dCar
        yMax = MyPoutre.EntraxeD1 + dCar

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        '--> Représentation des poutres et des axes à mi portée

        '# Poutre principale (étudiée)
        xe = Longueur
        AddLigne(myGr, MyPenP, 0, 0, xe, 0, MyParAff)

        '# poutre voisine côté D2
        yo = -MyPoutre.EntraxeD2
        AddLigne(myGr, MyPenPi, 0, yo, xe, yo, MyParAff)
        yo = yo / 2
        AddLigne(myGr, MyPenDot, 0, yo, xe, yo, MyParAff)

        '# Poutre voisine ou bord côté D1
        If MyPoutre.lIntermediaire Then
            yo = MyPoutre.EntraxeD1
            AddLigne(myGr, MyPenPi, 0, yo, xe, yo, MyParAff)
            yo = yo / 2
            AddLigne(myGr, MyPenDot, 0, yo, xe, yo, MyParAff)
            yo = MyPoutre.EntraxeD1 + dCar
        Else
            yo = MyPoutre.EntraxeD1
            AddLigne(myGr, MyPenD, 0, yo, xe, yo, MyParAff)
        End If

        '--> Représentation des bords perpendiculaire axe des poutres

        ye = -MyPoutre.EntraxeD2 - dCar
        AddLigne(myGr, MyPenD, xe, yo, xe, ye, MyParAff)
        AddLigne(myGr, MyPenD, 0, yo, 0, ye, MyParAff)

        '--> Représentation des appuis intermédiaires

        Dim xCum As Decimal '= MyPoutre.xPositionAppui(False, 0)
        For i As Integer = 1 To MyPoutre.NbTravees - 1
            xCum = MyPoutre.xPositionAppui(True, i)
            AddLigne(myGr, MyPenApp, xCum, yo, xCum, ye, MyParAff)
        Next

        '--> Préparation des noeuds de calcul et représentation

        MyPoutre.PrepareNodes(0.5, 10, 5)

        For i As Integer = 0 To MyPoutre.Nodes.nbNodes - 1
            xo = MyPoutre.Nodes.xGlobal(i)
            yo = -Longueur / 200
            ye = -yo
            AddLigne(myGr, MyPenSec, xo, yo, xo, ye, MyParAff)
        Next

        '--> Largeurs efficaces de la dalle

        ReDim Beff(MyPoutre.Nodes.nbNodes - 1, 1)

        For iTravee As Integer = MyPoutre.IndicePremiereTravee To MyPoutre.IndiceDerniereTravee

            Dim iGauche As Integer

            For i As Integer = MyPoutre.Nodes.iNodeAppui(iTravee, 0) To MyPoutre.Nodes.iNodeAppui(iTravee, 1)
                If i = MyPoutre.Nodes.iNodeAppui(iTravee, 0) Then 'Ajout GUD pour corriger bug du tracé de la largeur efficace
                    Beff(i, 0) = MyPoutre.BeffDalle(0, iTravee, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
                    Beff(i, 1) = MyPoutre.BeffDalle(0, iTravee, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
                Else
                    Beff(i, 0) = MyPoutre.BeffDalle(MyPoutre.Nodes.xTravee(i), iTravee, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurGauche)
                    Beff(i, 1) = MyPoutre.BeffDalle(MyPoutre.Nodes.xTravee(i), iTravee, False, False, cls_Poutre.EnuTypeLargeurParticipante.LargeurDroite)
                End If

            Next

        Next

        For iNode As Integer = 0 To MyPoutre.Nodes.nbNodes - 2
            xo = MyPoutre.Nodes.xGlobal(iNode)
            xe = MyPoutre.Nodes.xGlobal(iNode + 1)
            yo = Beff(iNode, 0)
            ye = Beff(iNode + 1, 0)
            AddLigne(myGr, MyPenBeff, xo, yo, xe, ye, MyParAff)

            yo = -Beff(iNode, 1)
            ye = -Beff(iNode + 1, 1)
            AddLigne(myGr, MyPenBeff, xo, yo, xe, ye, MyParAff)
        Next

        '--> Cotation

        yCote = yMin
        xCote = -dCar

        '# Portée

        MyColor = StyleCouleur(iSelect, -2)
        MyPenC.Color = MyColor

        For i = MyPoutre.IndicePremiereTravee To MyPoutre.IndiceDerniereTravee
            xo = MyPoutre.xPositionAppui(True, i)
            xe = MyPoutre.xPositionAppui(False, i)
            AddFleche(myGr, MyPenC, xo, yCote, xe, yCote, MyParAff, True, True)
            Chaine = GetStringNoUnit(xe - xo, Enu_TypeVariable.Longueur)
            AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, (xo + xe) / 2, yCote, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPenC, lContour)
        Next

        '# Entraxes D1 et D2

        yo = 0
        ye = -MyPoutre.EntraxeD2
        AddFleche(myGr, MyPenC, xCote, yo, xCote, ye, MyParAff, True, True)
        Chaine = GetStringNoUnit(MyPoutre.EntraxeD2, Enu_TypeVariable.Longueur)
        AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xCote, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPenC, lContour)

        ye = MyPoutre.EntraxeD1
        AddFleche(myGr, MyPenC, xCote, yo, xCote, ye, MyParAff, True, True)
        Chaine = GetStringNoUnit(MyPoutre.EntraxeD1, Enu_TypeVariable.Longueur)
        AddTexteFond(myGr, New SolidBrush(MyColor), Chaine, MyFontNormal, xCote, (yo + ye) / 2, MyParAff, HorizontalAlignment.Center, VerticalAlignement.Middle, New SolidBrush(SystemColors.ControlLightLight), MyPenC, lContour)

    End Sub


#End Region

End Class