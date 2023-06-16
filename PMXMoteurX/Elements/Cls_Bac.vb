Imports PropMix_Engine.Cls_Dalle

Public Class Cls_Bac

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

#End Region

#Region " Enumérations "

    Public Enum Enum_Orientation
        Parallele
        Perpendiculaire
    End Enum

#End Region

#Region " Constructeur "

    Sub New()

        Me.lDatabase = False
        Me.orientation = Enum_Orientation.Parallele

        Me.b_b = 0.062
        Me.b_t = 0.101
        Me.h_rs = 0
        Me.h_p = 0.058
        Me.e_p = 0.207

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

#Region " Fonction de copie "

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

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

        Const RATIOB1R As Double = 0.2
        Const RATIOB2R As Double = 0.25

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

#End Region


End Class
