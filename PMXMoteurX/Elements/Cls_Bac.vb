'Imports PropMix_Engine.Cls_Dalle

Public Class Cls_Bac

#Region " Autres déclarations "

    '-- Ratios pour la représentation des raidisseurs de bac
    Const RATIOB1R As Double = 0.2
    Const RATIOB2R As Double = 0.25

#End Region

#Region " Attributs pour l'interface "

    ''' <summary>
    ''' Nom du bac
    ''' </summary>
    Public Etiquette As String

    ''' <summary>
    ''' Nom du fabricant
    ''' </summary>
    Public Producteur As String

    ''' <summary>
    ''' Indique si il est récupéré de la base de données ou non
    ''' </summary>
    Public lDatabase As Decimal

#End Region

#Region " Attributs "

    ''' <summary>
    ''' hauteur du raidisseur supérieur (0 si pas de raidisseur)
    ''' </summary>
    Public h_rs As Decimal

    ''' <summary>
    ''' hauteur du bac, non compris le raidisseur supérieur
    ''' </summary>
    Public h_p As Decimal

    ''' <summary>
    ''' Largeur de nervure en creux d'onde (en bas du bac) b1
    ''' </summary>
    Public b_b As Decimal

    ''' <summary>
    ''' Largeur de nervure au somment (en haut du bac) b2
    ''' </summary>
    Public b_t As Decimal

    ''' <summary>
    ''' Entraxe des nervures
    ''' </summary>
    Public e_p As Decimal

    ''' <summary>
    ''' Epaisseur de tôle
    ''' </summary>
    Public tp As Decimal

    ''' <summary>
    ''' Orientation du bac
    ''' </summary>
    Public orientation As Enum_Orientation

    ''' <summary>
    ''' Masse surfacique, par m2 de plancher [kg/m2]
    ''' </summary>
    Public msurf As Decimal

    ''' <summary>
    ''' Limite d'élasticité du bac
    ''' </summary>
    Public fyp As Decimal

    ''' <summary>
    ''' Largeur d'un bac livré par le fabricant
    ''' </summary>
    Public LargeurModule As Decimal

    ''' <summary>
    ''' Moment d'inertie par unité de longueur
    ''' </summary>
    Public Ieff As Decimal

    ''' <summary>
    ''' Bac prépercé (ou non !)
    ''' </summary>
    Public lPreperce As Boolean

#End Region

#Region " Enumérations "

    Public Enum Enum_Orientation
        Parallele
        Perpendiculaire
    End Enum

#End Region

#Region " Constructeur "

    Sub New()

        Me.lDatabase = True
        Me.orientation = Enum_Orientation.Perpendiculaire

        Me.b_b = 0.062
        Me.b_t = 0.101
        Me.h_rs = 0
        Me.h_p = 0.058
        Me.e_p = 0.207

        Me.Etiquette = "Cofraplus_60 1.00"
        Me.lPreperce = True
    End Sub

    Sub New(MyFab As String, ByVal My_etiquette As String, ByVal Mybb As Decimal, ByVal Mybt As Decimal, ByVal Myhp As Decimal, ByVal Myhrs As Decimal,
            ByVal Myep As Decimal, ByVal Myt As Decimal, MymSurf As Decimal, Myfyp As Decimal)

        Me.Producteur = MyFab
        Me.lDatabase = True
        Me.Etiquette = My_etiquette
        Me.b_b = Mybb
        Me.b_t = Mybt

        Me.h_p = Myhp
        Me.e_p = Myep
        Me.tp = Myt
        Me.h_rs = Myhrs

        Me.msurf = MymSurf
        Me.fyp = Myfyp

    End Sub


    Sub New(MyFab As String, ByVal My_etiquette As String, ByVal Mybb As Decimal, ByVal Mybt As Decimal, ByVal Myhp As Decimal, ByVal Myhrs As Decimal,
            ByVal Myep As Decimal, ByVal Myt As Decimal, MymSurf As Decimal, Myfyp As Decimal, MyLMod As Decimal, MyIeff As Decimal)

        Me.Producteur = MyFab
        Me.lDatabase = True
        Me.Etiquette = My_etiquette
        Me.b_b = Mybb
        Me.b_t = Mybt

        Me.h_p = Myhp
        Me.e_p = Myep
        Me.tp = Myt
        Me.h_rs = Myhrs

        Me.msurf = MymSurf
        Me.fyp = Myfyp

        Me.Ieff = MyIeff
        Me.LargeurModule = MyLMod
    End Sub

#End Region

#Region " Ecriture Fichier "

    ''' <summary>
    ''' Ecriture des attributs pour enregistrement dans un fichier 
    ''' </summary>
    ''' <param name="Lines">Lignes d'écriture</param>
    Public Sub EcrireFile(ByRef Lines As List(Of String))

        Lines.Add("   BOrient       = " & orientation)
        Lines.Add("   BDatabase     = " & lDatabase)
        Lines.Add("   BEtiquette    = " & Etiquette)
        Lines.Add("   Bbb           = " & b_b)
        Lines.Add("   Bbt           = " & b_t)
        'Lines.Add("   Bhpg          = " & h_pg)
        Lines.Add("   Bhp           = " & h_p)
        Lines.Add("   Bep           = " & e_p)

    End Sub

#End Region

#Region " Fonctions de copie "

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Sub Copie(BacSource As Cls_Bac, ByRef lModif As Boolean)
        '-----------------------------------------------------------------
        '   26/06/23 : Création - POM
        '-----------------------------------------------------------------
        '   Copie d'une bac avec suivi de modif
        '   (Paramètres définis dans la fenêtre Frm_Bac)
        '-----------------------------------------------------------------
        '   BacSource   [E] :   Bac d'origine
        '   lModif      [S] :   Indique si une paramètre au moinx a été modifié
        '-----------------------------------------------------------------
        If Me.lDatabase <> BacSource.lDatabase Then lModif = True
        Me.lDatabase = BacSource.lDatabase

        If Me.Etiquette <> BacSource.Etiquette Then lModif = True
        Me.Etiquette = BacSource.Etiquette

        If Me.fyp <> BacSource.fyp Then lModif = True
        Me.fyp = BacSource.fyp

        If Me.h_p <> BacSource.h_p Then lModif = True
        Me.h_p = BacSource.h_p

        If Me.h_rs <> BacSource.h_rs Then lModif = True
        Me.h_rs = BacSource.h_rs

        If Me.b_b <> BacSource.b_b Then lModif = True
        Me.b_b = BacSource.b_b

        If Me.b_t <> BacSource.b_t Then lModif = True
        Me.b_t = BacSource.b_t

        If Me.e_p <> BacSource.e_p Then lModif = True
        Me.e_p = BacSource.e_p

        If Me.tp <> BacSource.tp Then lModif = True
        Me.tp = BacSource.tp

    End Sub

    Public Sub CopieAutresParam(BacSource As Cls_Bac, ByRef lModif As Boolean)
        '-----------------------------------------------------------------
        '   26/06/23 : Création - POM
        '-----------------------------------------------------------------
        '   Copie d'une bac avec suivi de modif
        '   (Paramètres définis dans la fenêtre Frm_DalleN)
        '-----------------------------------------------------------------
        '   BacSource   [E] :   Bac d'origine
        '   lModif      [S] :   Indique si une paramètre au moinx a été modifié
        '-----------------------------------------------------------------

        If Me.orientation <> BacSource.orientation Then lModif = True
        Me.orientation = BacSource.orientation

        If Me.lPreperce <> BacSource.lPreperce Then lModif = True
        Me.lPreperce = BacSource.lPreperce

    End Sub

#End Region

#Region " Outils "

    Public ReadOnly Property LargeurBmoyenne As Decimal
        Get
            Return (Me.b_b + Me.b_t) / 2
        End Get

    End Property

    Public ReadOnly Property Hauteur_hpg As Decimal
        Get
            Return Me.h_p + Me.h_rs
        End Get
    End Property

    Public ReadOnly Property HasRaidisseurSup() As Boolean
        Get
            Const EPSILONH As Double = 0.001
            Return (Me.h_rs > EPSILONH)
        End Get
    End Property

#End Region

#Region " Outils pour le dessin "

    Public Sub PrepareContourDalleBacRaidi(ByVal nbOndes As Integer, ByVal EpDalle As Double, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '-----------------------------------------------------------------------------------------------
        '   26/10/21 :  Création - POM 
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier - Préparation des points du contour pour le cas avec raidisseur
        '-----------------------------------------------------------------------------------------------
        '
        '   nbOndes     [E] :   Nombre d'ondes  sur lequel on représente le bac
        '   EpDalle     [E] :   Epaisseur de la dalle béton
        '   
        '   xPts, yPts  [S] :   Tableaux des coordonnées des points du contour
        '   nbPts       [S] :   Nombre de points du contour
        '
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer

        Const NBOP As Integer = 8
        ReDim xPts(nbOndes * NBOP + 3)
        ReDim yPts(nbOndes * NBOP + 3)

        Dim DeltaX As Double
        Dim Hpg As Double = Me.Hauteur_hpg
        Dim b1Raid, b2Raid As Double
        Dim bSupBac As Double

        '--> Initialisation

        nbPts = nbOndes * NBOP + 4
        bSupBac = Me.e_p - Me.b_t
        b1Raid = RATIOB1R * bSupBac
        b2Raid = RATIOB2R * bSupBac

        '--> Préparation des pts

        xPts(0) = 0
        yPts(0) = CSng(Hpg)

        For i = 1 To nbOndes
            DeltaX = (i - 1) * Me.e_p

            xPts((i - 1) * NBOP + 1) = CSng(DeltaX + b2Raid / 2)
            yPts((i - 1) * NBOP + 1) = CSng(Hpg)

            xPts((i - 1) * NBOP + 2) = CSng(DeltaX + b1Raid / 2)
            yPts((i - 1) * NBOP + 2) = CSng(Me.h_p)

            xPts((i - 1) * NBOP + 3) = CSng(DeltaX + Me.e_p / 2 - Me.b_t / 2)
            yPts((i - 1) * NBOP + 3) = CSng(Me.h_p)

            xPts((i - 1) * NBOP + 4) = CSng(DeltaX + Me.e_p / 2 - Me.b_b / 2)
            yPts((i - 1) * NBOP + 4) = 0

            xPts((i - 1) * NBOP + 5) = CSng(DeltaX + Me.e_p / 2 + Me.b_b / 2)
            yPts((i - 1) * NBOP + 5) = 0

            xPts((i - 1) * NBOP + 6) = CSng(DeltaX + Me.e_p / 2 + Me.b_t / 2)
            yPts((i - 1) * NBOP + 6) = CSng(Me.h_p)

            xPts((i - 1) * NBOP + 7) = CSng(DeltaX + Me.e_p - b1Raid / 2)
            yPts((i - 1) * NBOP + 7) = CSng(Me.h_p)

            xPts((i - 1) * NBOP + 8) = CSng(DeltaX + Me.e_p - b2Raid / 2)
            yPts((i - 1) * NBOP + 8) = CSng(Hpg)

        Next

        xPts(nbOndes * NBOP + 1) = CSng(nbOndes * Me.e_p)
        yPts(nbOndes * NBOP + 1) = CSng(Hpg)
        xPts(nbOndes * NBOP + 2) = CSng(nbOndes * Me.e_p)
        yPts(nbOndes * NBOP + 2) = CSng(EpDalle)
        xPts(nbOndes * NBOP + 3) = 0
        yPts(nbOndes * NBOP + 3) = CSng(EpDalle)

    End Sub

    Public Sub PrepareContourDalleBacSimple(ByVal nbOndes As Integer, ByVal EpDalle As Double, ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '-----------------------------------------------------------------------------------------------
        '   26/10/21 :  Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier - Préparation des points du contour pour le cas sans raidisseur
        '-----------------------------------------------------------------------------------------------
        '
        '   nbOndes     [E] :   Nombre d'ondes  sur lequel on représente le bac
        '   EpDalle     [E] :   Epaisseur de la dalle béton
        '   
        '   xPts, yPts  [S] :   Tableaux des coordonnées des points du contour
        '   nbPts       [S] :   Nombre de points du contour
        '
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer

        ReDim xPts(nbOndes * 4 + 3)
        ReDim yPts(nbOndes * 4 + 3)

        Dim DeltaX As Double

        '--> Initialisation

        nbPts = nbOndes * 4 + 4

        xPts(0) = 0
        yPts(0) = 0

        For i = 1 To nbOndes
            DeltaX = (i - 1) * Me.e_p

            xPts((i - 1) * 4 + 1) = CSng(DeltaX + Me.e_p / 2 - Me.b_t / 2)
            yPts((i - 1) * 4 + 1) = CSng(Me.h_p)

            xPts((i - 1) * 4 + 2) = CSng(DeltaX + Me.e_p / 2 - Me.b_b / 2)
            yPts((i - 1) * 4 + 2) = 0

            xPts((i - 1) * 4 + 3) = CSng(DeltaX + Me.e_p / 2 + Me.b_b / 2)
            yPts((i - 1) * 4 + 3) = 0

            xPts((i - 1) * 4 + 4) = CSng(DeltaX + Me.e_p / 2 + Me.b_t / 2)
            yPts((i - 1) * 4 + 4) = CSng(Me.h_p)
        Next

        xPts(0) = 0
        yPts(0) = CSng(Me.h_p)
        xPts(nbOndes * 4 + 1) = CSng(nbOndes * Me.e_p)
        yPts(nbOndes * 4 + 1) = CSng(Me.h_p)
        xPts(nbOndes * 4 + 2) = CSng(nbOndes * Me.e_p)
        yPts(nbOndes * 4 + 2) = CSng(EpDalle)
        xPts(nbOndes * 4 + 3) = 0
        yPts(nbOndes * 4 + 3) = CSng(EpDalle)

    End Sub


    Public Sub PrepareContourBacSimpleSeul2(ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer, lUn As Boolean)


        '--> Déclaration

        Dim nbOndes As Integer
        Dim wBac As Decimal
        Dim xp, yp As Decimal
        Const REBORD As Decimal = 0.2
        Dim dXnerv As Decimal
        Dim DeltaX0 As Decimal
        Dim xCenter As Decimal
        Dim decalX, decalZ As Decimal

        '--> Initialisation

        If lUn Then
            nbOndes = 1
            wBac = Me.e_p
        Else
            nbOndes = Math.Max(1, Math.Floor(Me.LargeurModule / Me.e_p))
            wBac = Math.Max(Me.e_p, Me.LargeurModule)
        End If
        dXnerv = (Me.b_t - Me.b_b) / 2
        DeltaX0 = (wBac - nbOndes * Me.e_p) / 2
        nbPts = 0

        decalX = Me.tp / 2 / Math.Tan((Math.PI - Math.Atan(Me.h_p / dXnerv)) / 2)
        decalZ = Me.tp / 2

        '===== SENS ALLER ===============================================================================

        AjoutePoint(0, decalZ, xPts, yPts, nbPts)

        xCenter = DeltaX0 + Me.e_p / 2

        For i As Integer = 1 To nbOndes

            AjoutePoint(xCenter - e_p / 2 + b_b / 2 - decalX, decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter - e_p / 2 + b_t / 2 - decalX, Me.h_p + decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter + e_p / 2 - b_t / 2 + decalX, Me.h_p + decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter + e_p / 2 - b_b / 2 + decalX, decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter + e_p / 2, decalZ, xPts, yPts, nbPts)

            xCenter += Me.e_p
        Next

        '===== SENS RETOUR ===============================================================================

        AjoutePoint(wBac, -decalZ, xPts, yPts, nbPts)

        xCenter = wBac - DeltaX0 - Me.e_p / 2

        For i As Integer = 1 To nbOndes

            AjoutePoint(xCenter + e_p / 2 - b_b / 2 - decalX, -decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter + e_p / 2 - b_t / 2 - decalX, Me.h_p - decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter - e_p / 2 + b_t / 2 + decalX, Me.h_p - decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter - e_p / 2 + b_b / 2 + decalX, -decalZ, xPts, yPts, nbPts)

            AjoutePoint(xCenter - e_p / 2, -decalZ, xPts, yPts, nbPts)

            xCenter -= Me.e_p
        Next

    End Sub

    Public Sub PrepareContourBacSimple1Nervure(ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)

        '--> Déclaration

        Dim nbOndes As Integer
        Dim wBac As Decimal

        Dim dXnerv As Decimal
        Dim DeltaX0 As Decimal

        Dim decalX, decalZ As Decimal
        Dim hP, eP, ptP As Decimal
        Const kTP As Decimal = 2

        '--> Initialisation

        ptP = kTP * Me.tp
        hP = Me.h_p
        eP = Me.e_p
        nbOndes = 1
        wBac = eP

        dXnerv = (Me.b_t - Me.b_b) / 2
        DeltaX0 = (wBac - nbOndes * eP) / 2
        nbPts = 0

        decalX = Math.Abs(ptP / 2 / Math.Tan((Math.PI - Math.Atan(hP / dXnerv)) / 2))
        decalZ = ptP / 2

        '===== SENS ALLER ===============================================================================

        AjoutePoint(-eP / 2, hP + decalZ, xPts, yPts, nbPts)
        AjoutePoint(-b_t / 2 + decalX, hP + decalZ, xPts, yPts, nbPts)
        AjoutePoint(-b_b / 2 + decalX, 0 + decalZ, xPts, yPts, nbPts)
        AjoutePoint(b_b / 2 - decalX, 0 + decalZ, xPts, yPts, nbPts)
        AjoutePoint(b_t / 2 - decalX, hP + decalZ, xPts, yPts, nbPts)
        AjoutePoint(eP / 2, hP + decalZ, xPts, yPts, nbPts)

        '===== SENS RETOUR ===============================================================================

        AjoutePoint(eP / 2, hP - decalZ, xPts, yPts, nbPts)
        AjoutePoint(b_t / 2 + decalX, hP - decalZ, xPts, yPts, nbPts)
        AjoutePoint(b_b / 2 + decalX, 0 - decalZ, xPts, yPts, nbPts)
        AjoutePoint(-b_b / 2 - decalX, 0 - decalZ, xPts, yPts, nbPts)
        AjoutePoint(-b_t / 2 - decalX, hP - decalZ, xPts, yPts, nbPts)
        AjoutePoint(-eP / 2, hP - decalZ, xPts, yPts, nbPts)

    End Sub

    Public Sub PrepareContourModuleBacRaidi(ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '-----------------------------------------------------------------------------------------------
        '   6/06/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier - Préparation des points du contour pour le cas avec raidisseur
        '   Dessin du bac seul, sans dalle,
        '   Largeur : celle du module
        '-----------------------------------------------------------------------------------------------
        '
        '   xPts, yPts  [S] :   Tableaux des coordonnées des points du contour
        '   nbPts       [S] :   Nombre de points du contour
        '
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim nbOndes As Integer
        Dim wBac As Decimal
        Dim xp, yp As Decimal
        Const REBORD As Decimal = 0.2
        Dim dXnerv As Decimal
        Dim DeltaX0 As Decimal
        Dim xCenter As Decimal
        Dim bbRaid, btRaid As Double
        Dim bSupBac, hpg As Double

        '--> Initialisation

        nbOndes = Math.Max(1, Math.Floor(Me.LargeurModule / Me.e_p))
        wBac = Math.Max(Me.e_p, Me.LargeurModule)
        dXnerv = (Me.b_t - Me.b_b) / 2
        DeltaX0 = (wBac - nbOndes * Me.e_p) / 2
        nbPts = 0

        bSupBac = Me.e_p - Me.b_t
        bbRaid = RATIOB1R * bSupBac
        btRaid = RATIOB2R * bSupBac
        hpg = Me.Hauteur_hpg

        '--> Début du module

        If dXnerv > 0 Then
            xp = -dXnerv * REBORD
            yp = Me.h_p * REBORD

            AjoutePoint(xp, yp, xPts, yPts, nbPts)
        End If

        AjoutePoint(0, 0, xPts, yPts, nbPts)

        If DeltaX0 > 0 Then
            AjoutePoint(DeltaX0, 0, xPts, yPts, nbPts)
        End If

        '--> Boucle sur les nervures

        xCenter = DeltaX0 + Me.e_p / 2

        For i As Integer = 1 To nbOndes

            AjoutePoint(xCenter - e_p / 2 + b_b / 2, 0, xPts, yPts, nbPts)
            AjoutePoint(xCenter - e_p / 2 + b_t / 2, Me.h_p, xPts, yPts, nbPts)
            AjoutePoint(xCenter - bbRaid / 2, Me.h_p, xPts, yPts, nbPts)
            AjoutePoint(xCenter - btRaid / 2, hpg, xPts, yPts, nbPts)

            AjoutePoint(xCenter + btRaid / 2, hpg, xPts, yPts, nbPts)
            AjoutePoint(xCenter + bbRaid / 2, Me.h_p, xPts, yPts, nbPts)
            AjoutePoint(xCenter + e_p / 2 - b_t / 2, Me.h_p, xPts, yPts, nbPts)
            AjoutePoint(xCenter + e_p / 2 - b_b / 2, 0, xPts, yPts, nbPts)

            AjoutePoint(xCenter + e_p / 2, 0, xPts, yPts, nbPts)

            xCenter += Me.e_p
        Next

        '--> Fin du module

        If DeltaX0 > 0 Then
            AjoutePoint(wBac, 0, xPts, yPts, nbPts)
        End If

        If dXnerv > 0 Then
            xp = wBac + dXnerv * REBORD
            yp = Me.h_p * REBORD

            AjoutePoint(xp, yp, xPts, yPts, nbPts)
        End If
    End Sub

    Public Sub PrepareContourModuleBacSimple(ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '-----------------------------------------------------------------------------------------------
        '   24/06/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier - Préparation des points du contour pour le cas sans raidisseur
        '   Dessin du bac seul, sans dalle,
        '   Largeur : celle du module
        '-----------------------------------------------------------------------------------------------
        '
        '   xPts, yPts  [S] :   Tableaux des coordonnées des points du contour
        '   nbPts       [S] :   Nombre de points du contour
        '
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim nbOndes As Integer
        Dim wBac As Decimal
        Dim xp, yp As Decimal
        Const REBORD As Decimal = 0.2
        Dim dXnerv As Decimal
        Dim DeltaX0 As Decimal
        Dim xCenter As Decimal

        '--> Initialisation

        nbOndes = Math.Max(1, Math.Floor(Me.LargeurModule / Me.e_p))
        wBac = Math.Max(Me.e_p, Me.LargeurModule)
        dXnerv = (Me.b_t - Me.b_b) / 2
        DeltaX0 = (wBac - nbOndes * Me.e_p) / 2
        nbPts = 0

        '--> Début du module

        If dXnerv > 0 Then
            xp = -dXnerv * REBORD
            yp = Me.h_p * REBORD

            AjoutePoint(xp, yp, xPts, yPts, nbPts)
        End If

        AjoutePoint(0, 0, xPts, yPts, nbPts)

        If DeltaX0 > 0 Then
            AjoutePoint(DeltaX0, 0, xPts, yPts, nbPts)
        End If

        '--> Boucle sur les nervures

        xCenter = DeltaX0 + Me.e_p / 2

        For i As Integer = 1 To nbOndes

            AjoutePoint(xCenter - e_p / 2 + b_b / 2, 0, xPts, yPts, nbPts)
            AjoutePoint(xCenter - e_p / 2 + b_t / 2, Me.h_p, xPts, yPts, nbPts)
            AjoutePoint(xCenter + e_p / 2 - b_t / 2, Me.h_p, xPts, yPts, nbPts)
            AjoutePoint(xCenter + e_p / 2 - b_b / 2, 0, xPts, yPts, nbPts)
            AjoutePoint(xCenter + e_p / 2, 0, xPts, yPts, nbPts)

            xCenter += Me.e_p
        Next

        '--> Fin du module

        If DeltaX0 > 0 Then
            AjoutePoint(wBac, 0, xPts, yPts, nbPts)
        End If

        If dXnerv > 0 Then
            xp = wBac + dXnerv * REBORD
            yp = Me.h_p * REBORD

            AjoutePoint(xp, yp, xPts, yPts, nbPts)
        End If
    End Sub

#End Region


End Class
