
Option Explicit Off


Imports System.Windows.Forms
Imports System.ComponentModel

Public Class POMbutton


#Region "   Variables "

    Private pLocked As Boolean = False
    Private pEnable As Boolean = True
    Private pChecked As Boolean = False
    Private pMouseOnBtn As Boolean = False
    Private pCouleurMouseOnBtn As Color = Color.Yellow
    Private pCouleurFond As Color = Color.WhiteSmoke
    Private pCouleurContour As Color = Color.Black
    Private pCouleurContourMouseOn As Color = Color.Black
    Private pCouleurContourChecked As Color = Color.Black
    Private pCouleurForGradient As Color = Color.WhiteSmoke
    Private pCouleurChecked As Color = Color.Orange
    Private pContourFond As Boolean = True              'Indique si tracé d'un contour quand tracé normal

    Private pRatioArrondi As Single = 0
    Private pCaption As String
    Private pCaptionAlignement As HorizontalAlignment = HorizontalAlignment.Center

#End Region

#Region "   Proprietés "

    Public Property Locked() As Boolean
        Get
            Return pLocked
        End Get
        Set(ByVal value As Boolean)
            pLocked = value
        End Set
    End Property

    Public Property Enable() As Boolean
        Get
            Return pEnable
        End Get
        Set(ByVal value As Boolean)
            pEnable = value
        End Set
    End Property

    Public Property Checked() As Boolean
        Get
            Return pChecked
        End Get
        Set(ByVal Value As Boolean)
            If Value <> pChecked Then
                pChecked = Value
                RaiseEvent CheckedChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    Public Property CouleurMouseOnBtn() As Color
        Get
            Return pCouleurMouseOnBtn
        End Get
        Set(ByVal Value As Color)
            If pCouleurMouseOnBtn <> Value Then
                pCouleurMouseOnBtn = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property Caption() As String
        Get
            Return pCaption
        End Get
        Set(ByVal Value As String)
            If pCaption <> Value Then
                pCaption = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property CouleurFond() As System.Drawing.Color
        Get
            Return pCouleurFond
        End Get
        Set(ByVal Value As System.Drawing.Color)
            If Me.pCouleurFond <> Value Then
                pCouleurFond = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property CouleurContour() As Color
        Get
            Return pCouleurContour
        End Get
        Set(ByVal Value As Color)
            If pCouleurContour <> Value Then
                pCouleurContour = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property CouleurForGradient() As Color
        Get
            Return pCouleurForGradient
        End Get
        Set(ByVal Value As Color)
            If pCouleurForGradient <> Value Then
                pCouleurForGradient = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property CouleurChecked() As Color
        Get
            Return pCouleurChecked
        End Get
        Set(ByVal Value As Color)
            If pCouleurChecked <> Value Then
                pCouleurChecked = Value
                Me.Invalidate()
            End If
        End Set
    End Property

    Public Property RatioArrondi() As Single
        Get
            Return pRatioArrondi
        End Get
        Set(ByVal Value As Single)
            pRatioArrondi = Value
        End Set
    End Property

    Public Property CaptionAlignement() As HorizontalAlignment
        Get
            Return pCaptionAlignement
        End Get
        Set(ByVal Value As HorizontalAlignment)
            pCaptionAlignement = Value
        End Set
    End Property

    Public Property CouleurContourChecked() As Color
        Get
            Return pCouleurContourChecked
        End Get
        Set(ByVal Value As Color)
            pCouleurContourChecked = Value
        End Set

    End Property

    Public Property LContourFond() As Boolean
        Get
            Return pContourFond
        End Get
        Set(ByVal Value As Boolean)
            pContourFond = Value
        End Set
    End Property

    Public Property CouleurContourMouseOn() As Color
        Get
            Return pCouleurContourMouseOn
        End Get
        Set(ByVal Value As Color)
            pCouleurContourMouseOn = Value
        End Set
    End Property

#End Region

#Region "   Gestion graphique "

    Private Sub POMButton_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint

        Dim sWI As Single = MyBase.ClientRectangle.Width - 1
        Dim sHI As Single = MyBase.ClientRectangle.Height - 1

        PDrawButton(e.Graphics, sWI, sHI, Me.Font)

    End Sub

    Private Sub PDrawButton(ByVal MyGr As Graphics, ByVal sWI As Single, ByVal sHI As Single, ByVal FontCap As Font)
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------
        '
        '   17/11/15 :  Création - V1.00 - POM
        '
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------
        '
        '   Gestion du paint sur un objet (Boutton ...)
        '
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics associé à l'objet redessiné
        '   sWI, sHI    [E] :   Dimension de la zone redessinée de l'objet
        '   Caption     [E] :   Texte à afficher au centre de l'objet
        '   FontCap     [E] :   Police pour afficher le texte Caption
        '   lContour    [E] :   Indique si on doit représenter le contour (quand pas selectionné)
        '   ColorContour[E] :   Couleur de contour
        '
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyBrush As Brush '= Nothing
        Dim MyBrush2 As Brush = Nothing
        Dim lArrondi As Boolean = (pRatioArrondi > 0.05)
        Dim MyPoints() As PointF = Nothing
        Dim MyPointsSup() As PointF = Nothing
        Dim MyPointsInf() As PointF = Nothing
        Dim Rayon As Single
        Dim xCenter(3), yCenter(3) As Single
        Dim Alpha0(3) As Single
        Dim lGradient As Boolean
        Dim CouleurContourSub As Color
        Dim lContour As Boolean
        Dim ColorBloque As Color = Color.DarkGray

        '--> Préparation du pinceau

        If pMouseOnBtn Then
            If pEnable Then
                MyBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New PointF(0, 0), New PointF(0, sHI / 2), pCouleurForGradient, pCouleurMouseOnBtn)
                MyBrush2 = New System.Drawing.Drawing2D.LinearGradientBrush(New PointF(0, sHI / 2 - 1), New PointF(0, sHI), pCouleurMouseOnBtn, pCouleurForGradient)
            Else
                MyBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New PointF(0, 0), New PointF(0, sHI / 2), pCouleurForGradient, ColorBloque)
                MyBrush2 = New System.Drawing.Drawing2D.LinearGradientBrush(New PointF(0, sHI / 2 - 1), New PointF(0, sHI), ColorBloque, pCouleurForGradient)
            End If
            lGradient = True
            CouleurContourSub = pCouleurContourMouseOn
            lContour = True
        ElseIf pChecked Then
            MyBrush = New System.Drawing.Drawing2D.LinearGradientBrush(New PointF(0, 0), New PointF(0, sHI / 2), Color.LightYellow, pCouleurChecked)
            MyBrush2 = New System.Drawing.Drawing2D.LinearGradientBrush(New PointF(0, sHI / 2 - 1), New PointF(0, sHI), pCouleurChecked, pCouleurForGradient)
            lGradient = True
            CouleurContourSub = pCouleurContourChecked
            lContour = True
        Else
            'lArrondi = lContour
            MyBrush = New SolidBrush(pCouleurFond)
            lGradient = False
            CouleurContourSub = pCouleurContour
            lContour = pContourFond
        End If

        '--> AffichageOptFeu

        If lArrondi Then
            Rayon = (Math.Min(Me.pRatioArrondi, 0.5!) * Math.Min(sWI, sHI))

            If lGradient Then
                GenereContourRectangulaireArrondi(0, 0, sWI, sHI, Rayon, True, True, True, True, MyPoints)
                GenereContourRectangulaireArrondi(0, 0, sWI, sHI / 2, Rayon, True, False, True, False, MyPointsSup)
                GenereContourRectangulaireArrondi(0, sHI / 2, sWI, sHI / 2, Rayon, False, True, False, True, MyPointsInf)
                MyGr.FillPolygon(MyBrush, MyPointsSup)
                MyGr.FillPolygon(MyBrush2, MyPointsInf)
                If lContour Then MyGr.DrawPolygon(New Pen(CouleurContourSub), MyPoints)
            Else
                GenereContourRectangulaireArrondi(0, 0, sWI, sHI, Rayon, True, True, True, True, MyPoints)
                MyGr.FillPolygon(MyBrush, MyPoints)
                If lContour Then MyGr.DrawPolygon(New Pen(CouleurContourSub), MyPoints)
            End If
        Else
            MyGr.FillRectangle(MyBrush, 0, 0, sWI, sHI)
            If lContour Then MyGr.DrawRectangle(New Pen(CouleurContourSub), 0, 0, sWI, sHI)
        End If

        '--> AffichageOptFeu du texte

        Dim xCap, yCap As Single

        yCap = (sHI - MyGr.MeasureString(Caption, FontCap).Height) / 2

        Select Case pCaptionAlignement
            Case HorizontalAlignment.Center
                xCap = (sWI - MyGr.MeasureString(Caption, FontCap).Width) / 2
            Case HorizontalAlignment.Right
                xCap = (sWI - MyGr.MeasureString(Caption, FontCap).Width - yCap)
            Case HorizontalAlignment.Left
                xCap = yCap
        End Select

        MyGr.DrawString(pCaption, FontCap, New SolidBrush(Me.ForeColor), xCap, yCap)

    End Sub

    Private _
    Sub GenereContourRectangulaireArrondi(ByVal x0 As Single, ByVal y0 As Single, ByVal sWI As Single, ByVal sHI As Single, ByVal RayonArrondi As Single,
                                          ByVal lRayTL As Boolean, ByVal lRayBL As Boolean, ByVal lRayTR As Boolean, ByVal lRayBR As Boolean, ByRef MyPoints() As PointF)
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------
        '
        '   17/11/15 :  Création - V1.00 - POM
        '
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------
        '
        '   Génère un contour polygonale rectangulaire avec arrondi aux angles
        '
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------
        '
        '   sWI, sHI    [E] :   Dimensions du rectangles
        '   x0, y0      [E] :   Position origine
        '   RayonArrondi[E] :   Valeur du rayon de l'arrondi aux angles
        '   lRayTL      [E] :   Indique si arrondi dans le coin supérieur gauche
        '   lRayBL      [E] :   Indique si arrondi dans le coin inférieur gauche
        '   lRayTR      [E] :   Indique si arrondi dans le coin supérieur droite
        '   lRayBR      [E] :   Indique si arrondi dans le coin inférieur droite
        '   
        '   MyPoints    [S] :   Tables de points pour le contour polygonal
        '
        '--------------------------------------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xCenter(3), yCenter(3) As Single
        Dim Angle, DeltaAlpha As Single
        Dim Alpha0(3) As Single
        Dim i, j As Integer
        Dim nbPoints, iPoint As Integer
        Dim lRayAngle(3) As Boolean
        Const NBQ As Integer = 4

        '--> Préparation Tableau

        nbPoints = 0
        If lRayBL Then nbPoints += NBQ Else nbPoints += 1
        If lRayTL Then nbPoints += NBQ Else nbPoints += 1
        If lRayBR Then nbPoints += NBQ Else nbPoints += 1
        If lRayTR Then nbPoints += NBQ Else nbPoints += 1

        ReDim MyPoints(nbPoints)

        '--> Initialisation

        DeltaAlpha = Math.PI / 2 / (NBQ - 1)

        If lRayTL Then
            xCenter(0) = RayonArrondi
            yCenter(0) = RayonArrondi
        Else
            xCenter(0) = 0
            yCenter(0) = 0
        End If
        If lRayTR Then
            xCenter(1) = sWI - RayonArrondi
            yCenter(1) = RayonArrondi
        Else
            xCenter(1) = sWI
            yCenter(1) = 0
        End If
        If lRayBR Then
            xCenter(2) = sWI - RayonArrondi
            yCenter(2) = sHI - RayonArrondi
        Else
            xCenter(2) = sWI
            yCenter(2) = sHI
        End If
        If lRayBL Then
            xCenter(3) = RayonArrondi
            yCenter(3) = sHI - RayonArrondi
        Else
            xCenter(3) = 0
            yCenter(3) = sHI
        End If
        Alpha0(0) = Math.PI
        Alpha0(1) = Math.PI / 2
        Alpha0(2) = 0
        Alpha0(3) = -Math.PI / 2
        lRayAngle(0) = lRayTL
        lRayAngle(1) = lRayTR
        lRayAngle(2) = lRayBR
        lRayAngle(3) = lRayBL

        iPoint = -1

        For i = 0 To 3

            xCenter(i) += x0
            yCenter(i) += y0

            If lRayAngle(i) Then
                For j = 0 To NBQ - 1
                    iPoint += 1
                    Angle = Alpha0(i) - j * DeltaAlpha
                    MyPoints(iPoint).X = xCenter(i) + RayonArrondi * CSng(Math.Cos(Angle))
                    MyPoints(iPoint).Y = yCenter(i) - RayonArrondi * CSng(Math.Sin(Angle))
                Next
            Else
                iPoint += 1
                MyPoints(iPoint).X = xCenter(i)
                MyPoints(iPoint).Y = yCenter(i)
            End If
        Next

        MyPoints(nbPoints).X = MyPoints(0).X
        MyPoints(nbPoints).Y = MyPoints(0).Y



    End Sub

#End Region

#Region "   Evènements "

    Public Event CheckedChanged As EventHandler

    Private Sub POMButton_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.MouseEnter
        Me.pMouseOnBtn = True
        Me.Invalidate()
    End Sub

    Private Sub POMButton_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.MouseLeave
        Me.pMouseOnBtn = False
        Me.Invalidate()
    End Sub

    Private Sub POMButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click
        If (Not Me.pLocked) And (Me.pEnable) Then
            Me.pChecked = Not Me.pChecked
            Me.Invalidate()
        End If
    End Sub

#End Region


End Class
