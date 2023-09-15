'Imports PropMix_Engine.Cls_Dalle

Public Class cls_Bac

#Region " Autres déclarations "

    '-- Ratios pour la représentation des raidisseurs de bac
    Public Const RATIOB1R As Double = 0.2
    Public Const RATIOB2R As Double = 0.25

    Private Const WAPPMIN As Decimal = 0.05      'Largeur minimal de l'appui d'un bac : 50 mm

    Const cofraplus220 As String = "COFRAPLUS_220"

    Public Enum EnuConfigTAppui
        Discontinu
        NervureEtBacContinus
        BetonSeulContinu
    End Enum

    Public Enum EnuConfigLAppui
        BacNonCoupe
        BacCoupe
    End Enum

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
    Public lDatabase As Boolean

#End Region

#Region " Attributs "

    ''' <summary>
    ''' hauteur du raidisseur supérieur (0 si pas de raidisseur)
    ''' </summary>
    Public h_rs As Decimal

    ''' <summary>
    ''' hauteur du bac, non compris le raidisseur supérieur
    ''' </summary>
    Public Hp As Decimal

    ''' <summary>
    ''' Largeur de nervure en creux d'onde (en bas du bac) b1
    ''' </summary>
    Public Bb As Decimal

    ''' <summary>
    ''' Largeur de nervure au somment (en haut du bac) b2
    ''' </summary>
    Public Bt As Decimal

    ''' <summary>
    ''' Entraxe des nervures
    ''' </summary>
    Public Ep As Decimal

    ''' <summary>
    ''' Epaisseur de tôle
    ''' </summary>
    Public Tp As Decimal

    ''' <summary>
    ''' Orientation du bac
    ''' </summary>
    Public Orientation As Enum_Orientation

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

    ''' <summary>
    ''' Configuration du bac au droit de l'appui (poutre) dans le cas d'une disposition transversale
    ''' </summary>
    Public AppuiT As EnuConfigTAppui

    ''' <summary>
    ''' Configuration du bac au droit de l'appui (poutre) dans le cas d'une disposition longitudinale
    ''' </summary>
    Public AppuiL As EnuConfigLAppui

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
        Me.Orientation = Enum_Orientation.Perpendiculaire

        Me.Bb = 0.062
        Me.Bt = 0.101
        Me.h_rs = 0
        Me.Hp = 0.058
        Me.Ep = 0.207

        Me.Etiquette = "Cofraplus_60 1.00"
        Me.lPreperce = True
        AppuiT = EnuConfigTAppui.BetonSeulContinu
        AppuiL = EnuConfigLAppui.BacNonCoupe
    End Sub

    Sub New(MyFab As String, ByVal My_etiquette As String, ByVal Mybb As Decimal, ByVal Mybt As Decimal, ByVal Myhp As Decimal, ByVal Myhrs As Decimal,
            ByVal Myep As Decimal, ByVal Myt As Decimal, MymSurf As Decimal, Myfyp As Decimal)

        Me.Producteur = MyFab
        Me.lDatabase = True
        Me.Etiquette = My_etiquette
        Me.Bb = Mybb
        Me.Bt = Mybt

        Me.Hp = Myhp
        Me.Ep = Myep
        Me.Tp = Myt
        Me.h_rs = Myhrs

        Me.msurf = MymSurf
        Me.fyp = Myfyp
        Me.lPreperce = True
        AppuiT = EnuConfigTAppui.BetonSeulContinu
        AppuiL = EnuConfigLAppui.BacNonCoupe
    End Sub


    Sub New(MyFab As String, ByVal My_etiquette As String, ByVal Mybb As Decimal, ByVal Mybt As Decimal, ByVal Myhp As Decimal, ByVal Myhrs As Decimal,
            ByVal Myep As Decimal, ByVal Myt As Decimal, MymSurf As Decimal, Myfyp As Decimal, MyLMod As Decimal, MyIeff As Decimal)

        Me.Producteur = MyFab
        Me.lDatabase = True
        Me.Etiquette = My_etiquette
        Me.Bb = Mybb
        Me.Bt = Mybt

        Me.Hp = Myhp
        Me.Ep = Myep
        Me.Tp = Myt
        Me.h_rs = Myhrs

        Me.msurf = MymSurf
        Me.fyp = Myfyp

        Me.Ieff = MyIeff
        Me.LargeurModule = MyLMod
        Me.lPreperce = True
        AppuiT = EnuConfigTAppui.BetonSeulContinu
        AppuiL = EnuConfigLAppui.BacNonCoupe
    End Sub


    Public Sub InitialiseCofraPlus60()
        Me.Bb = 0.062
        Me.Bt = 0.101
        Me.h_rs = 0
        Me.Hp = 0.058
        Me.Ep = 0.207

        Me.Etiquette = "Cofraplus_60 1.00"

    End Sub

#End Region

#Region " Ecriture Fichier "

    ''' <summary>
    ''' Ecriture des attributs pour enregistrement dans un fichier 
    ''' </summary>
    ''' <param name="Lines">Lignes d'écriture</param>
    Public Sub EcrireFile(ByRef Lines As List(Of String))

        Lines.Add("   BOrient       = " & Orientation)
        Lines.Add("   BDatabase     = " & lDatabase)
        Lines.Add("   BEtiquette    = " & Etiquette)
        Lines.Add("   Bbb           = " & Bb)
        Lines.Add("   Bbt           = " & Bt)
        'Lines.Add("   Bhpg          = " & h_pg)
        Lines.Add("   Bhp           = " & Hp)
        Lines.Add("   Bep           = " & Ep)

    End Sub

#End Region

#Region " Fonctions de copie "

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Sub Copie(BacSource As cls_Bac, ByRef lModif As Boolean)
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

        If Me.msurf <> BacSource.msurf Then lModif = True
        Me.msurf = BacSource.msurf

        If Me.Hp <> BacSource.Hp Then lModif = True
        Me.Hp = BacSource.Hp

        If Me.h_rs <> BacSource.h_rs Then lModif = True
        Me.h_rs = BacSource.h_rs

        If Me.Bb <> BacSource.Bb Then lModif = True
        Me.Bb = BacSource.Bb

        If Me.Bt <> BacSource.Bt Then lModif = True
        Me.Bt = BacSource.Bt

        If Me.Ep <> BacSource.Ep Then lModif = True
        Me.Ep = BacSource.Ep

        If Me.Tp <> BacSource.Tp Then lModif = True
        Me.Tp = BacSource.Tp

    End Sub

    Public Sub CopieAutresParam(BacSource As cls_Bac, ByRef lModif As Boolean)
        '-----------------------------------------------------------------
        '   26/06/23 : Création - POM
        '-----------------------------------------------------------------
        '   Copie d'une bac avec suivi de modif
        '   (Paramètres définis dans la fenêtre Frm_DalleN)
        '-----------------------------------------------------------------
        '   BacSource   [E] :   Bac d'origine
        '   lModif      [S] :   Indique si une paramètre au moinx a été modifié
        '-----------------------------------------------------------------

        If Me.Orientation <> BacSource.Orientation Then lModif = True
        Me.Orientation = BacSource.Orientation

        If Me.lPreperce <> BacSource.lPreperce Then lModif = True
        Me.lPreperce = BacSource.lPreperce

        If Me.AppuiL <> BacSource.AppuiL Then lModif = True
        Me.AppuiL = BacSource.AppuiL

        If Me.AppuiT <> BacSource.AppuiT Then lModif = True
        Me.AppuiT = BacSource.AppuiT
    End Sub

#End Region

#Region " Outils "

    ''' <summary>
    ''' Largeur b0 pour les coefficients kT et kL
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property LargeurB0 As Decimal
        Get
            Return Math.Min(Me.LargeurBmoyenne, Me.Bt)
        End Get
    End Property

    ''' <summary>
    ''' Largeur moyenne d'une nervure
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property LargeurBmoyenne As Decimal
        Get
            Return (Me.Bb + Me.Bt) / 2
        End Get

    End Property

    Public ReadOnly Property Hauteur_hpg As Decimal
        Get
            Return Me.Hp + Me.h_rs
        End Get
    End Property

    Public ReadOnly Property HasRaidisseurSup() As Boolean
        Get
            Const EPSILONH As Double = 0.001
            Return (Me.h_rs > EPSILONH)
        End Get
    End Property

    ''' <summary>
    ''' Renvoie la largeur d'appui minimale du bac sur la semelle
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property wAppui As Decimal
        Get
            Return WAPPMIN
        End Get
    End Property

    Public ReadOnly Property lCofraplus220 As Boolean
        Get
            Dim NomBac As String
            Dim CharSep As String = " "
            Dim jSep As Integer = Me.Etiquette.Trim.IndexOf(CharSep)

            If jSep >= 0 Then
                NomBac = Me.Etiquette.Trim.Substring(0, jSep).ToUpper
            Else
                NomBac = Me.Etiquette.Trim.ToUpper
            End If

            Return NomBac = cofraplus220
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
        bSupBac = Me.Ep - Me.Bt
        b1Raid = RATIOB1R * bSupBac
        b2Raid = RATIOB2R * bSupBac

        '--> Préparation des pts

        xPts(0) = 0
        yPts(0) = CSng(Hpg)

        For i = 1 To nbOndes
            DeltaX = (i - 1) * Me.Ep

            xPts((i - 1) * NBOP + 1) = CSng(DeltaX + b2Raid / 2)
            yPts((i - 1) * NBOP + 1) = CSng(Hpg)

            xPts((i - 1) * NBOP + 2) = CSng(DeltaX + b1Raid / 2)
            yPts((i - 1) * NBOP + 2) = CSng(Me.Hp)

            xPts((i - 1) * NBOP + 3) = CSng(DeltaX + Me.Ep / 2 - Me.Bt / 2)
            yPts((i - 1) * NBOP + 3) = CSng(Me.Hp)

            xPts((i - 1) * NBOP + 4) = CSng(DeltaX + Me.Ep / 2 - Me.Bb / 2)
            yPts((i - 1) * NBOP + 4) = 0

            xPts((i - 1) * NBOP + 5) = CSng(DeltaX + Me.Ep / 2 + Me.Bb / 2)
            yPts((i - 1) * NBOP + 5) = 0

            xPts((i - 1) * NBOP + 6) = CSng(DeltaX + Me.Ep / 2 + Me.Bt / 2)
            yPts((i - 1) * NBOP + 6) = CSng(Me.Hp)

            xPts((i - 1) * NBOP + 7) = CSng(DeltaX + Me.Ep - b1Raid / 2)
            yPts((i - 1) * NBOP + 7) = CSng(Me.Hp)

            xPts((i - 1) * NBOP + 8) = CSng(DeltaX + Me.Ep - b2Raid / 2)
            yPts((i - 1) * NBOP + 8) = CSng(Hpg)

        Next

        xPts(nbOndes * NBOP + 1) = CSng(nbOndes * Me.Ep)
        yPts(nbOndes * NBOP + 1) = CSng(Hpg)
        xPts(nbOndes * NBOP + 2) = CSng(nbOndes * Me.Ep)
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
            DeltaX = (i - 1) * Me.Ep

            xPts((i - 1) * 4 + 1) = CSng(DeltaX + Me.Ep / 2 - Me.Bt / 2)
            yPts((i - 1) * 4 + 1) = CSng(Me.Hp)

            xPts((i - 1) * 4 + 2) = CSng(DeltaX + Me.Ep / 2 - Me.Bb / 2)
            yPts((i - 1) * 4 + 2) = 0

            xPts((i - 1) * 4 + 3) = CSng(DeltaX + Me.Ep / 2 + Me.Bb / 2)
            yPts((i - 1) * 4 + 3) = 0

            xPts((i - 1) * 4 + 4) = CSng(DeltaX + Me.Ep / 2 + Me.Bt / 2)
            yPts((i - 1) * 4 + 4) = CSng(Me.Hp)
        Next

        xPts(0) = 0
        yPts(0) = CSng(Me.Hp)
        xPts(nbOndes * 4 + 1) = CSng(nbOndes * Me.Ep)
        yPts(nbOndes * 4 + 1) = CSng(Me.Hp)
        xPts(nbOndes * 4 + 2) = CSng(nbOndes * Me.Ep)
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
            wBac = Me.Ep
        Else
            nbOndes = Math.Max(1, Math.Floor(Me.LargeurModule / Me.Ep))
            wBac = Math.Max(Me.Ep, Me.LargeurModule)
        End If
        dXnerv = (Me.Bt - Me.Bb) / 2
        DeltaX0 = (wBac - nbOndes * Me.Ep) / 2
        nbPts = 0

        decalX = Me.Tp / 2 / Math.Tan((Math.PI - Math.Atan(Me.Hp / dXnerv)) / 2)
        decalZ = Me.Tp / 2

        '===== SENS ALLER ===============================================================================

        AjoutePoint(0, decalZ, xPts, yPts, nbPts)

        xCenter = DeltaX0 + Me.Ep / 2

        For i As Integer = 1 To nbOndes

            AjoutePoint(xCenter - Ep / 2 + Bb / 2 - decalX, decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter - Ep / 2 + Bt / 2 - decalX, Me.Hp + decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter + Ep / 2 - Bt / 2 + decalX, Me.Hp + decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter + Ep / 2 - Bb / 2 + decalX, decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter + Ep / 2, decalZ, xPts, yPts, nbPts)

            xCenter += Me.Ep
        Next

        '===== SENS RETOUR ===============================================================================

        AjoutePoint(wBac, -decalZ, xPts, yPts, nbPts)

        xCenter = wBac - DeltaX0 - Me.Ep / 2

        For i As Integer = 1 To nbOndes

            AjoutePoint(xCenter + Ep / 2 - Bb / 2 - decalX, -decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter + Ep / 2 - Bt / 2 - decalX, Me.Hp - decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter - Ep / 2 + Bt / 2 + decalX, Me.Hp - decalZ, xPts, yPts, nbPts)
            AjoutePoint(xCenter - Ep / 2 + Bb / 2 + decalX, -decalZ, xPts, yPts, nbPts)

            AjoutePoint(xCenter - Ep / 2, -decalZ, xPts, yPts, nbPts)

            xCenter -= Me.Ep
        Next

    End Sub

    Public Sub PrepareContourBacRaidi1Nervure(ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '--------------------------------------------------------------------------------------------------------------------
        '   27/06/23 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------
        '   Préparation du contour d'une nervure, avec épaisseur, bac RAIDI
        '--------------------------------------------------------------------------------------------------------------------
        '   xPts, yPts  [S] :   Table des points définissant le contour
        '   nbPts       [S] :   Nombre de pts dans le contour
        '--------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim nbOndes As Integer
        Dim wBac As Decimal

        Dim dXnerv As Decimal
        Dim DeltaX0 As Decimal

        Dim decalX, decalZ As Decimal
        Dim hP, eP, ptP, hpg As Decimal
        Const kTP As Decimal = 2
        Dim bbRaid, btraid As Decimal
        Dim decalXraid, decalZaid As Decimal

        '--> Initialisation

        ptP = kTP * Me.Tp
        hP = Me.Hp
        hpg = Me.Hauteur_hpg
        eP = Me.Ep
        nbOndes = 1
        wBac = eP
        bbRaid = RATIOB1R * (eP - Me.Bt)
        btraid = RATIOB2R * (eP - Me.Bt)

        dXnerv = (Me.Bt - Me.Bb) / 2
        DeltaX0 = (wBac - nbOndes * eP) / 2
        nbPts = 0

        decalX = Math.Abs(ptP / 2 / Math.Tan((Math.PI - Math.Atan(hP / dXnerv)) / 2))
        decalZ = ptP / 2

        decalZaid = (hpg - hP)
        decalXraid = Math.Abs(ptP / 2 / Math.Tan((Math.PI - Math.Atan(decalZaid / ((RATIOB2R - RATIOB1R) * (eP - Me.Bt)))) / 2))

        '===== SENS ALLER ===============================================================================

        AjoutePoint(-eP / 2, hpg + decalZ, xPts, yPts, nbPts)

        AjoutePoint(-eP / 2 + btraid + decalXraid, hpg + decalZ, xPts, yPts, nbPts)
        AjoutePoint(-eP / 2 + bbRaid + decalXraid, hP + decalZ, xPts, yPts, nbPts)

        AjoutePoint(-Bt / 2 + decalX, hP + decalZ, xPts, yPts, nbPts)
        AjoutePoint(-Bb / 2 + decalX, 0 + decalZ, xPts, yPts, nbPts)
        AjoutePoint(Bb / 2 - decalX, 0 + decalZ, xPts, yPts, nbPts)
        AjoutePoint(Bt / 2 - decalX, hP + decalZ, xPts, yPts, nbPts)

        AjoutePoint(eP / 2 - bbRaid - decalXraid, hP + decalZ, xPts, yPts, nbPts)
        AjoutePoint(eP / 2 - btraid - decalXraid, hpg + decalZ, xPts, yPts, nbPts)

        AjoutePoint(eP / 2, hpg + decalZ, xPts, yPts, nbPts)

        '===== SENS RETOUR ===============================================================================

        AjoutePoint(eP / 2, hpg - decalZ, xPts, yPts, nbPts)
        AjoutePoint(eP / 2 - btraid + decalXraid, hpg - decalZ, xPts, yPts, nbPts)
        AjoutePoint(eP / 2 - bbRaid + decalXraid, hP - decalZ, xPts, yPts, nbPts)

        AjoutePoint(Bt / 2 + decalX, hP - decalZ, xPts, yPts, nbPts)
        AjoutePoint(Bb / 2 + decalX, 0 - decalZ, xPts, yPts, nbPts)
        AjoutePoint(-Bb / 2 - decalX, 0 - decalZ, xPts, yPts, nbPts)
        AjoutePoint(-Bt / 2 - decalX, hP - decalZ, xPts, yPts, nbPts)

        AjoutePoint(-eP / 2 + bbRaid - decalXraid, hP - decalZ, xPts, yPts, nbPts)
        AjoutePoint(-eP / 2 + btraid - decalXraid, hpg - decalZ, xPts, yPts, nbPts)

        AjoutePoint(-eP / 2, hpg - decalZ, xPts, yPts, nbPts)

    End Sub

    Public Sub PrepareContourBacSimple1Nervure(ByRef xPts() As Single, ByRef yPts() As Single, ByRef nbPts As Integer)
        '--------------------------------------------------------------------------------------------------------------------
        '   27/06/23 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------
        '   Préparation du contour d'une nervure, avec épaisseur, bac non raidi
        '--------------------------------------------------------------------------------------------------------------------
        '   xPts, yPts  [S] :   Table des points définissant le contour
        '   nbPts       [S] :   Nombre de pts dans le contour
        '--------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim nbOndes As Integer
        Dim wBac As Decimal

        Dim dXnerv As Decimal
        Dim DeltaX0 As Decimal

        Dim decalX, decalZ As Decimal
        Dim hP, eP, ptP As Decimal
        Const kTP As Decimal = 2

        '--> Initialisation

        ptP = kTP * Me.Tp
        hP = Me.Hp
        eP = Me.Ep
        nbOndes = 1
        wBac = eP

        dXnerv = (Me.Bt - Me.Bb) / 2
        DeltaX0 = (wBac - nbOndes * eP) / 2
        nbPts = 0

        decalX = Math.Abs(ptP / 2 / Math.Tan((Math.PI - Math.Atan(hP / dXnerv)) / 2))
        decalZ = ptP / 2

        '===== SENS ALLER ===============================================================================

        AjoutePoint(-eP / 2, hP + decalZ, xPts, yPts, nbPts)
        AjoutePoint(-Bt / 2 + decalX, hP + decalZ, xPts, yPts, nbPts)
        AjoutePoint(-Bb / 2 + decalX, 0 + decalZ, xPts, yPts, nbPts)
        AjoutePoint(Bb / 2 - decalX, 0 + decalZ, xPts, yPts, nbPts)
        AjoutePoint(Bt / 2 - decalX, hP + decalZ, xPts, yPts, nbPts)
        AjoutePoint(eP / 2, hP + decalZ, xPts, yPts, nbPts)

        '===== SENS RETOUR ===============================================================================

        AjoutePoint(eP / 2, hP - decalZ, xPts, yPts, nbPts)
        AjoutePoint(Bt / 2 + decalX, hP - decalZ, xPts, yPts, nbPts)
        AjoutePoint(Bb / 2 + decalX, 0 - decalZ, xPts, yPts, nbPts)
        AjoutePoint(-Bb / 2 - decalX, 0 - decalZ, xPts, yPts, nbPts)
        AjoutePoint(-Bt / 2 - decalX, hP - decalZ, xPts, yPts, nbPts)
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

        nbOndes = Math.Max(1, Math.Floor(Me.LargeurModule / Me.Ep))
        wBac = Math.Max(Me.Ep, Me.LargeurModule)
        dXnerv = (Me.Bt - Me.Bb) / 2
        DeltaX0 = (wBac - nbOndes * Me.Ep) / 2
        nbPts = 0

        bSupBac = Me.Ep - Me.Bt
        bbRaid = RATIOB1R * bSupBac
        btRaid = RATIOB2R * bSupBac
        hpg = Me.Hauteur_hpg

        '--> Début du module

        If dXnerv > 0 Then
            xp = -dXnerv * REBORD
            yp = Me.Hp * REBORD

            AjoutePoint(xp, yp, xPts, yPts, nbPts)
        End If

        AjoutePoint(0, 0, xPts, yPts, nbPts)

        If DeltaX0 > 0 Then
            AjoutePoint(DeltaX0, 0, xPts, yPts, nbPts)
        End If

        '--> Boucle sur les nervures

        xCenter = DeltaX0 + Me.Ep / 2

        For i As Integer = 1 To nbOndes

            AjoutePoint(xCenter - Ep / 2 + Bb / 2, 0, xPts, yPts, nbPts)
            AjoutePoint(xCenter - Ep / 2 + Bt / 2, Me.Hp, xPts, yPts, nbPts)
            AjoutePoint(xCenter - bbRaid / 2, Me.Hp, xPts, yPts, nbPts)
            AjoutePoint(xCenter - btRaid / 2, hpg, xPts, yPts, nbPts)

            AjoutePoint(xCenter + btRaid / 2, hpg, xPts, yPts, nbPts)
            AjoutePoint(xCenter + bbRaid / 2, Me.Hp, xPts, yPts, nbPts)
            AjoutePoint(xCenter + Ep / 2 - Bt / 2, Me.Hp, xPts, yPts, nbPts)
            AjoutePoint(xCenter + Ep / 2 - Bb / 2, 0, xPts, yPts, nbPts)

            AjoutePoint(xCenter + Ep / 2, 0, xPts, yPts, nbPts)

            xCenter += Me.Ep
        Next

        '--> Fin du module

        If DeltaX0 > 0 Then
            AjoutePoint(wBac, 0, xPts, yPts, nbPts)
        End If

        If dXnerv > 0 Then
            xp = wBac + dXnerv * REBORD
            yp = Me.Hp * REBORD

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

        nbOndes = Math.Max(1, Math.Floor(Me.LargeurModule / Me.Ep))
        wBac = Math.Max(Me.Ep, Me.LargeurModule)
        dXnerv = (Me.Bt - Me.Bb) / 2
        DeltaX0 = (wBac - nbOndes * Me.Ep) / 2
        nbPts = 0

        '--> Début du module

        If dXnerv > 0 Then
            xp = -dXnerv * REBORD
            yp = Me.Hp * REBORD

            AjoutePoint(xp, yp, xPts, yPts, nbPts)
        End If

        AjoutePoint(0, 0, xPts, yPts, nbPts)

        If DeltaX0 > 0 Then
            AjoutePoint(DeltaX0, 0, xPts, yPts, nbPts)
        End If

        '--> Boucle sur les nervures

        xCenter = DeltaX0 + Me.Ep / 2

        For i As Integer = 1 To nbOndes

            AjoutePoint(xCenter - Ep / 2 + Bb / 2, 0, xPts, yPts, nbPts)
            AjoutePoint(xCenter - Ep / 2 + Bt / 2, Me.Hp, xPts, yPts, nbPts)
            AjoutePoint(xCenter + Ep / 2 - Bt / 2, Me.Hp, xPts, yPts, nbPts)
            AjoutePoint(xCenter + Ep / 2 - Bb / 2, 0, xPts, yPts, nbPts)
            AjoutePoint(xCenter + Ep / 2, 0, xPts, yPts, nbPts)

            xCenter += Me.Ep
        Next

        '--> Fin du module

        If DeltaX0 > 0 Then
            AjoutePoint(wBac, 0, xPts, yPts, nbPts)
        End If

        If dXnerv > 0 Then
            xp = wBac + dXnerv * REBORD
            yp = Me.Hp * REBORD

            AjoutePoint(xp, yp, xPts, yPts, nbPts)
        End If
    End Sub

#End Region

#Region " Fonctions de calcul des paramètres K1 et K2 "

    Private ReadOnly Property ThetaPDegres As Decimal
        '-----------------------------------------------------------------------------------------------
        '   25/08/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Calcul de l'inclinaison des nervures par rapport à la verticale en degres
        '-----------------------------------------------------------------------------------------------
        Get
            Return Math.Atan((Me.Bt - Me.Bb) / (2 * Me.Hp)) * 180 / Math.PI
        End Get
    End Property
    Private ReadOnly Property RatioHpSurEp As Decimal
        '-----------------------------------------------------------------------------------------------
        '   25/08/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Calcul de ratio Hauteur / Entraxe
        '-----------------------------------------------------------------------------------------------
        Get
            Return Me.Hp / Me.Ep
        End Get
    End Property
    Private ReadOnly Property RatioBSurEp As Decimal
        '-----------------------------------------------------------------------------------------------
        '   25/08/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Calcul de ratio Largeur inter nervure / Entraxe
        '-----------------------------------------------------------------------------------------------
        Get
            Return (Me.Ep - Me.Bt) / Me.Ep
        End Get
    End Property

    ''' <summary>
    ''' Coefficient K2 d'après tableau 5.6 dans le guide CECM n°88
    ''' </summary>
    ''' <param name="lOK"></param>
    ''' <returns></returns>
    Public Function CoefK1(ByRef lOK As Boolean) As Decimal
        '-----------------------------------------------------------------------------------------------
        '   25/08/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Calcul du coefficient K1 
        '-----------------------------------------------------------------------------------------------

        Return Me.CalculK1K2(Me.ThetaPDegres, Me.RatioHpSurEp, Me.RatioBSurEp, lOK, True)

    End Function

    ''' <summary>
    ''' Coefficient K2 d'après tableau 5.7 dans le guide CECM n°88
    ''' </summary>
    ''' <param name="lOK"></param>
    ''' <returns></returns>
    Public Function CoefK2(ByRef lOK As Boolean) As Decimal
        '-----------------------------------------------------------------------------------------------
        '   25/08/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------
        '   Calcul du coefficient K2 
        '-----------------------------------------------------------------------------------------------

        Return Me.CalculK1K2(Me.ThetaPDegres, Me.RatioHpSurEp, Me.RatioBSurEp, lOK, False)

    End Function



    Private Function CalculK1K2(Theta As Decimal, HsurE As Decimal, BsurE As Decimal, ByRef lOK As Boolean, lK1 As Boolean) As Decimal
        '-----------------------------------------------------------------------------------------------
        '   26/06/23 :  Création - CVP
        '-----------------------------------------------------------------------------------------------
        '   Calcul du coefficient K1 ou K2, d'après le guide ECCS n°88
        '-----------------------------------------------------------------------------------------------
        '   Theta       [E] :   Angle d'inclinaison du bac
        '   HeSurEp     [E] :   Hp sur Ep    (hauteur nervure sur entraxe)
        '   BsurE       [E] :   Rapport (ep-bt)/ep, bt largeur nervure au sommet
        '   lOK         [S] :   Indique si processus OK
        '   lK1         [E] :   Indique si K1 (vrai) ou K2 (faux)
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyTableauK(1) As List(Of Decimal())
        Dim ThetaBorne(1) As Decimal
        Dim NbTheta As Integer
        Dim NbHsurE As Integer
        Dim NbBsurE As Integer

        Dim lContTheta As Boolean = True
        Dim lContHsurE As Boolean = True
        Dim lContBsurE As Boolean = True

        Dim ThetaPredefinies() As Decimal = {0, 5, 10, 15, 20, 25, 30, 35, 40, 45}
        Dim HsurEPredefinies() As Decimal = {0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8}
        Dim BsurEPredefinies() As Decimal = {0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9}

        Dim NbThetaTab As Integer
        Dim NbHsurETab As Integer
        Dim NbBsurETab As Integer

        Dim iTheta As Integer = -1
        Dim iHsurE As Integer = -1
        Dim iBsurE As Integer = -1


        Const EPSTHETA As Decimal = 0.001
        Const EPSHsurE As Decimal = 0.0001
        Const EPSBsurE As Decimal = 0.0001

        Dim ltrouveTheta As Boolean = False
        Dim ltrouveHsurE As Boolean = False
        Dim ltrouveBsurE As Boolean = False

        'Const MYINDEXBsurE As Integer = 6
        'Const MYINDEXHsurE As Integer = 3

        '--> Initialisation

        NbThetaTab = ThetaPredefinies.GetUpperBound(0)
        NbHsurETab = HsurEPredefinies.GetUpperBound(0)
        NbBsurETab = BsurEPredefinies.GetUpperBound(0)

        lOK = True

        '--> Recherche position de Thetat

        Do While lContTheta

            iTheta += 1

            If Math.Abs(Theta - ThetaPredefinies(iTheta)) < EPSTHETA Then

                '--> Cas où on est sur une valeur de theta dans le tableau

                ltrouveTheta = True
                NbTheta = 1

            ElseIf Theta < ThetaPredefinies(iTheta) Then

                '--> Cas où la valeur de theta est entre deux valeur du tableau

                ltrouveTheta = True
                NbTheta = 2

            End If

            lContTheta = Not ltrouveTheta And iTheta < NbThetaTab

        Loop

        '--> Recherche position de HsurE

        Do While lContHsurE

            iHsurE += 1

            If Math.Abs(HsurE - HsurEPredefinies(iHsurE)) < EPSHsurE Then

                '--> Cas où on est sur une valeur de theta dans le tableau

                ltrouveHsurE = True
                NbHsurE = 1

            ElseIf HsurE < HsurEPredefinies(iHsurE) Then

                '--> Cas où la valeur de theta est entre deux valeur du tableau

                ltrouveHsurE = True
                NbHsurE = 2

            End If

            lContHsurE = Not ltrouveHsurE And iHsurE < NbHsurETab

        Loop

        '--> Recherche position de BsurE

        Do While lContBsurE

            iBsurE += 1

            If Math.Abs(BsurE - BsurEPredefinies(iBsurE)) < EPSBsurE Then

                '--> Cas où on est sur une valeur de theta dans le tableau

                ltrouveBsurE = True
                NbBsurE = 1

            ElseIf BsurE < BsurEPredefinies(iBsurE) Then

                '--> Cas où la valeur de theta est entre deux valeur du tableau

                ltrouveBsurE = True
                NbBsurE = 2

            End If

            lContBsurE = Not ltrouveBsurE And iBsurE < NbBsurETab

        Loop

        '--> Interpolation pour trouver des valeurs

        If NbTheta = 1 Then

            ConstruireTableauK(ThetaPredefinies(iTheta), MyTableauK(0), lK1)

            If NbHsurE = 1 Then
                If NbBsurE = 1 Then
                    If MyTableauK(0)(iHsurE)(iBsurE) = -1 Then
                        '--> Cas où la valeur en dehors du tableau
                        lOK = False
                    Else
                        Return MyTableauK(0)(iHsurE)(iBsurE)
                    End If

                ElseIf NbBsurE = 2 Then
                    If MyTableauK(0)(iHsurE)(iBsurE - 1) = -1 Or MyTableauK(0)(iHsurE)(iBsurE) = -1 Then
                        '--> Cas où la valeur en dehors du tableau
                        lOK = False
                    Else
                        Return Interpole(BsurE, BsurEPredefinies(iBsurE - 1), BsurEPredefinies(iBsurE), MyTableauK(0)(iHsurE)(iBsurE - 1), MyTableauK(0)(iHsurE)(iBsurE))
                    End If

                End If
            ElseIf NbHsurE = 2 Then

                If NbBsurE = 1 Then
                    If MyTableauK(0)(iHsurE - 1)(iBsurE) = -1 Or MyTableauK(0)(iHsurE)(iBsurE) = -1 Then

                        '--> Cas où la valeur en dehors du tableau
                        lOK = False
                    Else
                        Return Interpole(HsurE, HsurEPredefinies(iHsurE - 1), HsurEPredefinies(iHsurE), MyTableauK(0)(iHsurE - 1)(iBsurE), MyTableauK(0)(iHsurE)(iBsurE))
                    End If
                ElseIf NbBsurE = 2 Then

                    '--> Premimère interpolation selon BsurE

                    Dim Valeur1 As Decimal = Interpole(BsurE, BsurEPredefinies(iBsurE - 1), BsurEPredefinies(iBsurE), MyTableauK(0)(iHsurE - 1)(iBsurE - 1), MyTableauK(0)(iHsurE - 1)(iBsurE))

                    Dim Valeur2 As Decimal = Interpole(BsurE, BsurEPredefinies(iBsurE - 1), BsurEPredefinies(iBsurE), MyTableauK(0)(iHsurE)(iBsurE - 1), MyTableauK(0)(iHsurE)(iBsurE))

                    '--> Deuximère interpolation selon HsurE

                    If MyTableauK(0)(iHsurE - 1)(iBsurE - 1) = -1 Or MyTableauK(0)(iHsurE - 1)(iBsurE) = -1 Or MyTableauK(0)(iHsurE)(iBsurE - 1) = -1 Or MyTableauK(0)(iHsurE)(iBsurE) = -1 Then

                        '--> Cas où la valeur en dehors du tableau
                        lOK = False
                    Else
                        Return Interpole(HsurE, HsurEPredefinies(iHsurE - 1), HsurEPredefinies(iHsurE), Valeur1, Valeur2)
                    End If

                End If

            End If

        ElseIf NbTheta = 2 Then

            ConstruireTableauK(ThetaPredefinies(iTheta - 1), MyTableauK(0), lK1)
            ConstruireTableauK(ThetaPredefinies(iTheta), MyTableauK(1), lK1)

            If NbHsurE = 1 Then
                If NbBsurE = 1 Then
                    If MyTableauK(0)(iHsurE)(iBsurE) = -1 Or MyTableauK(1)(iHsurE)(iBsurE) = -1 Then

                        '--> Cas où la valeur en dehors du tableau
                        lOK = False
                    Else
                        Return Interpole(Theta, ThetaPredefinies(iTheta - 1), ThetaPredefinies(iTheta), MyTableauK(0)(iHsurE)(iBsurE), MyTableauK(1)(iHsurE)(iBsurE))
                    End If

                ElseIf NbBsurE = 2 Then
                    '--> Premimère interpolation selon BsurE

                    Dim Valeur1 As Decimal = Interpole(BsurE, BsurEPredefinies(iBsurE - 1), BsurEPredefinies(iBsurE), MyTableauK(0)(iHsurE)(iBsurE - 1), MyTableauK(0)(iHsurE)(iBsurE))

                    Dim Valeur2 As Decimal = Interpole(BsurE, BsurEPredefinies(iBsurE - 1), BsurEPredefinies(iBsurE), MyTableauK(1)(iHsurE)(iBsurE - 1), MyTableauK(1)(iHsurE)(iBsurE))

                    '--> Deuximère interpolation selon Theta
                    If MyTableauK(0)(iHsurE)(iBsurE - 1) = -1 Or MyTableauK(0)(iHsurE)(iBsurE) = -1 Or MyTableauK(1)(iHsurE)(iBsurE - 1) = -1 Or MyTableauK(1)(iHsurE)(iBsurE) = -1 Then

                        '--> Cas où la valeur en dehors du tableau
                        lOK = False
                    Else

                        Return Interpole(Theta, ThetaPredefinies(iTheta - 1), ThetaPredefinies(iTheta), Valeur1, Valeur2)
                    End If

                End If

            ElseIf NbHsurE = 2 Then

                If NbBsurE = 1 Then
                    '--> Premimère interpolation selon HsurE

                    Dim Valeur1 As Decimal = Interpole(HsurE, HsurEPredefinies(iHsurE - 1), HsurEPredefinies(iHsurE), MyTableauK(0)(iHsurE - 1)(iBsurE), MyTableauK(0)(iHsurE)(iBsurE))

                    Dim Valeur2 As Decimal = Interpole(HsurE, HsurEPredefinies(iHsurE - 1), HsurEPredefinies(iHsurE), MyTableauK(1)(iHsurE - 1)(iBsurE), MyTableauK(1)(iHsurE)(iBsurE))

                    '--> Deuximère interpolation selon Theta

                    If MyTableauK(0)(iHsurE - 1)(iBsurE) = -1 Or MyTableauK(0)(iHsurE)(iBsurE) = -1 Or MyTableauK(1)(iHsurE - 1)(iBsurE) = -1 Or MyTableauK(1)(iHsurE)(iBsurE) = -1 Then

                        '--> Cas où la valeur en dehors du tableau
                        lOK = False
                    Else

                        Return Interpole(Theta, ThetaPredefinies(iTheta - 1), ThetaPredefinies(iTheta), Valeur1, Valeur2)
                    End If


                ElseIf NbBsurE = 2 Then
                    '--> Premimère interpolation selon HsurE

                    Dim Valeur1 As Decimal = Interpole(HsurE, HsurEPredefinies(iHsurE - 1), HsurEPredefinies(iHsurE), MyTableauK(0)(iHsurE - 1)(iBsurE - 1), MyTableauK(0)(iHsurE)(iBsurE - 1))

                    Dim Valeur2 As Decimal = Interpole(HsurE, HsurEPredefinies(iHsurE - 1), HsurEPredefinies(iHsurE), MyTableauK(0)(iHsurE - 1)(iBsurE), MyTableauK(0)(iHsurE)(iBsurE))

                    Dim Valeur3 As Decimal = Interpole(HsurE, HsurEPredefinies(iHsurE - 1), HsurEPredefinies(iHsurE), MyTableauK(1)(iHsurE - 1)(iBsurE - 1), MyTableauK(1)(iHsurE)(iBsurE - 1))

                    Dim Valeur4 As Decimal = Interpole(HsurE, HsurEPredefinies(iHsurE - 1), HsurEPredefinies(iHsurE), MyTableauK(1)(iHsurE - 1)(iBsurE), MyTableauK(1)(iHsurE)(iBsurE))

                    '--> Deuximère interpolation selon BsurE
                    Dim Valeur5 As Decimal = Interpole(BsurE, BsurEPredefinies(iBsurE - 1), BsurEPredefinies(iBsurE), Valeur1, Valeur2)
                    Dim Valeur6 As Decimal = Interpole(BsurE, BsurEPredefinies(iBsurE - 1), BsurEPredefinies(iBsurE), Valeur3, Valeur4)

                    '--> Troisimère interpolation selon Theta

                    If MyTableauK(0)(iHsurE - 1)(iBsurE - 1) = -1 Or MyTableauK(0)(iHsurE)(iBsurE - 1) = -1 Or MyTableauK(0)(iHsurE - 1)(iBsurE) = -1 Or MyTableauK(0)(iHsurE)(iBsurE) = -1 Or MyTableauK(1)(iHsurE - 1)(iBsurE - 1) = -1 Or MyTableauK(1)(iHsurE)(iBsurE - 1) = -1 Or MyTableauK(1)(iHsurE - 1)(iBsurE) = -1 Or MyTableauK(1)(iHsurE)(iBsurE) = -1 Then

                        '--> Cas où la valeur en dehors du tableau
                        lOK = False
                    Else
                        Return Interpole(Theta, ThetaPredefinies(iTheta - 1), ThetaPredefinies(iTheta), Valeur5, Valeur6)
                    End If
                End If



            End If
        End If




    End Function

    Private Sub ConstruireTableauK(Theta As Decimal, ByRef MyTabK As List(Of Decimal()), lK1 As Boolean)
        '-----------------------------------------------------------------------------------------------
        '   26/06/23 :  Création - CVP
        '-----------------------------------------------------------------------------------------------
        '  Construction d'une ligne du tableau K1 en function de Theta
        '-----------------------------------------------------------------------------------------------
        '   Theta       [E] :   Angle d'inclinaison du bac
        '   MyTabK      [S]:    Tableau K1 pour la valeur de Theta
        '   lK1         [E] :   Indique si K1 (vrai) ou K2 (faux)
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        'Dim MyLigne() As Decimal

        '--> Initialistiton

        MyTabK = New List(Of Decimal())

        MyTabK.Clear()

        '--> Ajout des deux premières lignes de tableau K1 pour la valeur de theta demandée

        For pas As Decimal = 0.1 To 0.8 Step 0.1

            If lK1 Then
                MyTabK.Add(InitialiseLigneTableauK1(Theta, pas))
            Else
                MyTabK.Add(InitialiseLigneTableauK2(Theta, pas))
            End If

        Next

    End Sub

    Private Function InitialiseLigneTableauK1(Theta As Decimal, HeSurEp As Decimal) As Decimal()
        '-----------------------------------------------------------------------------------------------
        '   26/06/23 :  Création - CVP
        '-----------------------------------------------------------------------------------------------
        '   Construction d'une ligne du tableau K1 en function de Theta et de hp sur ep
        '   Paramètre K1 d'après le Tablea 5.6 dans le guide CECM/ECCS n°88
        '-----------------------------------------------------------------------------------------------
        '   Theta       [E] :   Angle d'inclinaison du bac
        '   HeSurEp     [E] :   Hp sur Ep
        '-----------------------------------------------------------------------------------------------

        Select Case Theta
            Case 0
                Select Case HeSurEp
                    Case 0.1
                        Return {0.013, 0.03, 0.041, 0.041, 0.046, 0.05, 0.066, 0.103, 0.193}
                    Case 0.2
                        Return {0.042, 0.096, 0.131, 0.142, 0.142, 0.153, 0.199, 0.311, 0.602}
                    Case 0.3
                        Return {0.086, 0.194, 0.264, 0.285, 0.283, 0.302, 0.388, 0.601, 1.188}
                    Case 0.4
                        Return {0.144, 0.323, 0.438, 0.473, 0.468, 0.494, 0.629, 0.972, 1.935}
                    Case 0.5
                        Return {0.216, 0.438, 0.654, 0.703, 0.695, 0.729, 0.922, 1.42, 2.837}
                    Case 0.6
                        Return {0.302, 0.674, 0.911, 0.98, 0.965, 1.008, 1.266, 1.938, 3.892}
                    Case 0.7
                        Return {0.402, 0.895, 1.208, 1.3, 1.277, 1.329, 1.661, 2.536, 5.098}
                    Case 0.8
                        Return {0.516, 1.146, 1.546, 1.662, 1.631, 1.692, 2.107, 3.208, 6.453}
                End Select

            Case 5
                Select Case HeSurEp
                    Case 0.1
                        Return {0.014, 0.031, 0.041, 0.044, 0.044, 0.049, 0.066, 0.107, 0.205}
                    Case 0.2
                        Return {0.05, 0.099, 0.128, 0.134, 0.132, 0.146, 0.198, 0.336, 0.652}
                    Case 0.3
                        Return {0.107, 0.202, 0.253, 0.26, 0.254, 0.28, 0.386, 0.681, 1.548}
                    Case 0.4
                        Return {0.188, 0.338, 0.413, 0.417, 0.404, 0.448, 0.629, 1.158, 2.639}
                    Case 0.5
                        Return {0.295, 0.507, 0.604, 0.601, 0.578, 0.648, 0.934, 1.783, -1}
                    Case 0.6
                        Return {0.429, 0.706, 0.823, 0.826, 0.772, 0.877, 1.306, 2.586, -1}
                    Case 0.7
                        Return {0.591, 0.935, 1.066, 1.028, 0.983, 1.385, 1.756, 3.605, -1}
                    Case 0.8
                        Return {0.78, 1.191, 1.328, 1.264, 1.208, 1.423, 2.99, 4.838, -1}
                End Select

            Case 10
                Select Case HeSurEp
                    Case 0.1
                        Return {0.016, 0.031, 0.04, 0.042, 0.042, 0.048, 0.065, 0.111, 0.221}
                    Case 0.2
                        Return {0.056, 0.101, 0.123, 0.125, 0.123, 0.139, 0.2, 0.366, 0.873}
                    Case 0.3
                        Return {0.125, 0.204, 0.238, 0.233, 0.26, 0.264, 0.402, 0.786, -1}
                    Case 0.4
                        Return {0.222, 0.338, 0.375, 0.356, 0.345, 0.418, 0.689, 1.445, -1}
                    Case 0.5
                        Return {0.349, 0.494, 0.526, 0.486, 0.473, 0.605, 1.082, 2.428, -1}
                    Case 0.6
                        Return {0.502, 0.668, 0.682, 0.615, 0.608, 0.837, 1.607, -1, -1}
                    Case 0.7
                        Return {0.677, 0.851, 0.834, 0.736, 0.752, 1.128, 2.308, -1, -1}
                    Case 0.8
                        Return {0.869, 1.035, 0.975, 0.844, 0.907, 1.494, 3.2, -1, -1}
                End Select

            Case 15
                Select Case HeSurEp
                    Case 0.1
                        Return {0.017, 0.031, 0.04, 0.041, 0.041, 0.047, 0.066, 0.115, 0.241}
                    Case 0.2
                        Return {0.062, 0.102, 0.118, 0.115, 0.113, 0.134, 0.209, 0.403, -1}
                    Case 0.3
                        Return {0.139, 0.202, 0.218, 0.204, 0.2, 0.254, 0.44, 0.945, -1}
                    Case 0.4
                        Return {0.244, 0.321, 0.325, 0.293, 0.294, 0.414, 0.796, -1, -1}
                    Case 0.5
                        Return {0.37, 0.448, 0.426, 0.371, 0.396, 0.636, 1.329, -1, -1}
                    Case 0.6
                        Return {0.508, 0.568, 0.508, 0.434, 0.513, 0.941, -1, -1, -1}
                    Case 0.7
                        Return {0.646, 0.668, 0.561, 0.483, 0.664, 1.349, -1, -1, -1}
                    Case 0.8
                        Return {0.768, 0.735, 0.578, 0.527, 0.861, -1, -1, -1, -1}
                End Select
            Case 20
                Select Case HeSurEp
                    Case 0.1
                        Return {0.018, 0.032, 0.039, 0.039, 0.039, 0.046, 0.066, 0.111, 0.276}
                    Case 0.2
                        Return {0.068, 0.101, 0.111, 0.106, 0.104, 0.131, 0.221, 0.452, -1}
                    Case 0.3
                        Return {0.148, 0.193, 0.194, 0.174, 0.177, 0.255, 0.492, -1, -1}
                    Case 0.4
                        Return {0.249, 0.289, 0.267, 0.23, 0.259, 0.444, 0.931, -1, -1}
                    Case 0.5
                        Return {0.356, 0.372, 0.315, 0.27, 0.364, 0.725, -1, -1, -1}
                    Case 0.6
                        Return {0.448, 0.42, 0.326, 0.303, 0.512, -1, -1, -1, -1}
                    Case 0.7
                        Return {0.5, 0.423, 0.301, 0.346, -1, -1, -1, -1, -1}
                    Case 0.8
                        Return {0.521, 0.372, 0.259, 0.413, -1, -1, -1, -1, -1}
                End Select
            Case 25
                Select Case HeSurEp
                    Case 0.1
                        Return {0.019, 0.032, 0.038, 0.038, 0.038, 0.045, 0.068, 0.126, 0.313}
                    Case 0.2
                        Return {0.072, 0.099, 0.103, 0.095, 0.095, 0.129, 0.236, 0.513, -1}
                    Case 0.3
                        Return {0.151, 0.178, 0.166, 0.144, 0.16, 0.268, 0.557, -1, -1}
                    Case 0.4
                        Return {0.238, 0.244, 0.204, 0.176, 0.247, 0.494, -1, -1, -1}
                    Case 0.5
                        Return {0.306, 0.272, 0.203, 0.204, 0.376, -1, -1, -1, -1}
                    Case 0.6
                        Return {0.333, 0.248, 0.172, 0.241, -1, -1, -1, -1, -1}
                    Case 0.7
                        Return {0.3, 0.174, 0.142, -1, -1, -1, -1, -1, -1}
                    Case 0.8
                        Return {0.204, 0.081, -1, -1, -1, -1, -1, -1, -1}
                End Select
            Case 30
                Select Case HeSurEp
                    Case 0.1
                        Return {0.02, 0.032, 0.037, 0.036, 0.036, 0.044, 0.07, 0.133, -1}
                    Case 0.2
                        Return {0.075, 0.095, 0.094, 0.084, 0.087, 0.132, 0.256, -1, -1}
                    Case 0.3
                        Return {0.148, 0.157, 0.135, 0.116, 0.152, 0.291, -1, -1, -1}
                    Case 0.4
                        Return {0.208, 0.186, 0.139, 0.139, 0.253, -1, -1, -1, -1}
                    Case 0.5
                        Return {0.226, 0.161, 0.112, 0.176, -1, -1, -1, -1, -1}
                    Case 0.6
                        Return {0.18, 0.089, 0.093, -1, -1, -1, -1, -1, -1}
                    Case 0.7
                        Return {0.077, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.8
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                End Select
            Case 35
                Select Case HeSurEp
                    Case 0.1
                        Return {0.021, 0.032, 0.036, 0.034, 0.034, 0.043, 0.072, 0.142, -1}
                    Case 0.2
                        Return {0.076, 0.089, 0.083, 0.072, 0.082, 0.137, 0.281, -1, -1}
                    Case 0.3
                        Return {0.137, 0.13, 0.102, 0.093, 0.151, -1, -1, -1, -1}
                    Case 0.4
                        Return {0.162, 0.119, 0.082, 0.12, -1, -1, -1, -1, -1}
                    Case 0.5
                        Return {0.123, 0.059, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.6
                        Return {0.032, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.7
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.8
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                End Select
            Case 40
                Select Case HeSurEp
                    Case 0.1
                        Return {0.023, 0.032, 0.034, 0.032, 0.032, 0.043, 0.075, -1, -1}
                    Case 0.2
                        Return {0.075, 0.081, 0.07, 0.06, 0.077, 0.146, -1, -1, -1}
                    Case 0.3
                        Return {0.116, 0.096, 0.068, 0.078, -1, -1, -1, -1, -1}
                    Case 0.4
                        Return {0.1, 0.053, 0.048, -1, -1, -1, -1, -1, -1}
                    Case 0.5
                        Return {0.024, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.6
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.7
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.8
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                End Select
            Case 45
                Select Case HeSurEp
                    Case 0.1
                        Return {0.024, 0.031, 0.032, 0.029, 0.03, 0.043, 0.079, -1, -1}
                    Case 0.2
                        Return {0.071, 0.069, 0.056, 0.05, 0.073, -1, -1, -1, -1}
                    Case 0.3
                        Return {0.086, 0.057, 0.041, -1, -1, -1, -1, -1, -1}
                    Case 0.4
                        Return {0.032, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.5
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.6
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.7
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.8
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                End Select
        End Select

    End Function

    Private Function InitialiseLigneTableauK2(Theta As Decimal, HeSurEp As Decimal) As Decimal()
        '-----------------------------------------------------------------------------------------------
        '   26/06/23 :  Création - CVP
        '-----------------------------------------------------------------------------------------------
        '   Construction d'une ligne du tableau K2 en function de Theta et de hp sur ep
        '   Paramètre K2 d'après le Tablea 5.7 dans le guide CECM/ECCS n°88
        '-----------------------------------------------------------------------------------------------
        '   Theta       [E] :   Angle d'inclinaison du bac
        '   HeSurEp     [E] :   Hp sur Ep
        '-----------------------------------------------------------------------------------------------

        Select Case Theta
            Case 0
                Select Case HeSurEp
                    Case 0.1
                        Return {0.014, 0.025, 0.036, 0.046, 0.054, 0.061, 0.07, 0.108, 0.211}
                    Case 0.2
                        Return {0.031, 0.065, 0.099, 0.129, 0.151, 0.169, 0.206, 0.318, 0.649}
                    Case 0.3
                        Return {0.054, 0.123, 0.192, 0.252, 0.294, 0.328, 0.402, 0.608, 1.269}
                    Case 0.4
                        Return {0.084, 0.202, 0.316, 0.414, 0.482, 0.535, 0.653, 0.968, 2.056}
                    Case 0.5
                        Return {0.123, 0.299, 0.468, 0.614, 0.712, 0.79, 0.958, 1.41, 3.006}
                    Case 0.6
                        Return {0.169, 0.415, 0.649, 0.846, 0.982, 1.09, 1.318, 1.928, 4.113}
                    Case 0.7
                        Return {0.222, 0.549, 0.855, 1.108, 1.286, 1.433, 1.73, 2.525, 5.383}
                    Case 0.8
                        Return {0.284, 0.699, 1.086, 1.398, 1.398, 1.818, 2.196, 3.198, 6.811}

                End Select

            Case 5
                Select Case HeSurEp
                    Case 0.1
                        Return {0.089, 0.138, 0.184, 0.288, 0.269, 0.311, 0.359, 0.432, 0.59}
                    Case 0.2
                        Return {0.3, 0.433, 0.564, 0.69, 0.81, 0.934, 1.091, 1.358, 2.046}
                    Case 0.3
                        Return {0.627, 0.872, 1.113, 1.345, 1.569, 1.806, 2.215, 2.71, 4.441}
                    Case 0.4
                        Return {1.076, 1.453, 1.826, 2.187, 2.535, 2.91, 3.446, 4.498, 8.057}
                    Case 0.5
                        Return {1.644, 2.171, 2.694, 3.205, 3.703, 4.244, 5.058, 6.671, 12.94}
                    Case 0.6
                        Return {2.28, 2.961, 3.639, 4.313, 4.999, 5.797, 6.971, 9.571, -1}
                    Case 0.7
                        Return {2.961, 3.803, 4.62, 5.443, 6.347, 7.479, 9.206, 13.01, -1}
                    Case 0.8
                        Return {3.802, 4.838, 5.788, 6.612, 7.701, 9.257, 11.76, 17.2, -1}
                End Select

            Case 10
                Select Case HeSurEp
                    Case 0.1
                        Return {0.091, 0.14, 0.186, 0.229, 0.27, 0.312, 0.362, 0.44, 0.627}
                    Case 0.2
                        Return {0.312, 0.446, 0.575, 0.699, 0.817, 0.943, 1.112, 1.425, 2.472}
                    Case 0.3
                        Return {0.665, 0.907, 1.144, 1.37, 1.589, 1.835, 2.204, 2.979, -1}
                    Case 0.4
                        Return {1.156, 1.529, 1.891, 2.239, 2.578, 2.984, 3.655, 5.251, -1}
                    Case 0.5
                        Return {1.793, 2.313, 2.819, 3.305, 3.782, 4.397, 5.519, 7.872, -1}
                    Case 0.6
                        Return {2.533, 3.206, 3.858, 4.509, 5.192, 6.096, 7.875, -1, -1}
                    Case 0.7
                        Return {3.334, 4.148, 4.949, 5.78, 6.737, 8.112, 10.82, -1, -1}
                    Case 0.8
                        Return {4.236, 5.17, 6.051, 7.066, 8.404, 10.47, 12.59, -1, -1}
                End Select

            Case 15
                Select Case HeSurEp
                    Case 0.1
                        Return {0.093, 0.142, 0.198, 0.231, 0.271, 0.313, 0.364, 0.448, 0.682}
                    Case 0.2
                        Return {0.325, 0.458, 0.586, 0.707, 0.824, 0.953, 1.14, 1.523, -1}
                    Case 0.3
                        Return {0.703, 0.942, 1.174, 1.393, 1.61, 1.874, 2.316, 3.411, -1}
                    Case 0.4
                        Return {1.237, 1.602, 1.953, 2.285, 2.624, 3.089, 3.981, -1, -1}
                    Case 0.5
                        Return {1.937, 2.443, 2.926, 3.379, 3.869, 4.64, 6.256, -1, -1}
                    Case 0.6
                        Return {2.778, 3.428, 4.058, 4.664, 5.366, 6.581, -1, -1, -1}
                    Case 0.7
                        Return {3.692, 4.488, 5.273, 6.081, 7.138, 8.902, -1, -1, -1}
                    Case 0.8
                        Return {4.648, 5.57, 6.516, 7.628, 9.91, -1, -1, -1, -1}
                End Select
            Case 20
                Select Case HeSurEp
                    Case 0.1
                        Return {0.096, 0.144, 0.19, 0.232, 0.273, 0.315, 0.368, 0.459, 0.68}
                    Case 0.2
                        Return {0.339, 0.472, 0.597, 0.716, 0.832, 0.966, 1.177, 1.659, -1}
                    Case 0.3
                        Return {0.743, 0.976, 1.204, 1.416, 1.633, 1.927, 2.461, -1, -1}
                    Case 0.4
                        Return {1.317, 1.673, 2.009, 2.325, 2.679, 3.246, 3.84, -1, -1}
                    Case 0.5
                        Return {2.075, 2.559, 3.011, 3.436, 3.993, 4.969, -1, -1, -1}
                    Case 0.6
                        Return {3.006, 3.625, 4.194, 4.752, 5.588, -1, -1, -1, -1}
                    Case 0.7
                        Return {4.042, 4.789, 5.494, 6.272, -1, -1, -1, -1, -1}
                    Case 0.8
                        Return {5.122, 6.013, 6.883, 7.861, -1, -1, -1, -1, -1}
                End Select
            Case 25
                Select Case HeSurEp
                    Case 0.1
                        Return {0.098, 0.147, 0.192, 0.234, 0.274, 0.317, 0.373, 0.475, 0.665}
                    Case 0.2
                        Return {0.355, 0.485, 0.609, 0.725, 0.84, 0.983, 1.226, 1.566, -1}
                    Case 0.3
                        Return {0.784, 1.015, 1.233, 1.437, 1.66, 2, 2.589, -1, -1}
                    Case 0.4
                        Return {1.398, 1.74, 2.057, 2.359, 2.753, 3.427, -1, -1, -1}
                    Case 0.5
                        Return {2.205, 2.659, 3.064, 3.49, 4.114, -1, -1, -1, -1}
                    Case 0.6
                        Return {2, 3.752, 4.218, 4.797, -1, -1, -1, -1, -1}
                    Case 0.7
                        Return {4.318, 4.941, 5.48, -1, -1, -1, -1, -1, -1}
                    Case 0.8
                        Return {5.487, 6.132, -1, -1, -1, -1, -1, -1, -1}
                End Select
            Case 30
                Select Case HeSurEp
                    Case 0.1
                        Return {0.101, 0.15, 0.194, 0.236, 0.276, 0.319, 0.378, 0.495, -1}
                    Case 0.2
                        Return {0.372, 0.5, 0.621, 0.734, 0.85, 1.005, 1.298, -1, -1}
                    Case 0.3
                        Return {0.827, 1.051, 1.26, 1.456, 1.697, 2.098, -1, -1, -1}
                    Case 0.4
                        Return {1.477, 1.801, 2.092, 2.393, 2.83, -1, -1, -1, -1}
                    Case 0.5
                        Return {2.319, 2.727, 3.075, 3.499, -1, -1, -1, -1, -1}
                    Case 0.6
                        Return {3.32, 3.728, 4.041, -1, -1, -1, -1, -1, -1}
                    Case 0.7
                        Return {4.378, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.8
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                End Select
            Case 35
                Select Case HeSurEp
                    Case 0.1
                        Return {0.105, 0.153, 0.197, 0.238, 0.278, 0.322, 0.385, 0.525, -1}
                    Case 0.2
                        Return {0.39, 0.516, 0.634, 0.744, 0.862, 1.035, 1.329, -1, -1}
                    Case 0.3
                        Return {0.872, 1.088, 1.284, 1.548, 1.741, -1, -1, -1, -1}
                    Case 0.4
                        Return {1.553, 1.849, 2.105, 2.412, -1, -1, -1, -1, -1}
                    Case 0.5
                        Return {2.4, 2.713, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.6
                        Return {3.278, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.7
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.8
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                End Select
            Case 40
                Select Case HeSurEp
                    Case 0.1
                        Return {0.109, 0.156, 0.2, 0.241, 0.28, 0.325, 0.394, 0.569, -1}
                    Case 0.2
                        Return {0.411, 0.538, 0.647, 0.753, 0.878, 1.077, -1, -1, -1}
                    Case 0.3
                        Return {0.919, 1.122, 1.301, 1.496, -1, -1, -1, -1, -1}
                    Case 0.4
                        Return {1.614, 1.859, 2.085, -1, -1, -1, -1, -1, -1}
                    Case 0.5
                        Return {2.376, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.6
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.7
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.8
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                End Select
            Case 45
                Select Case HeSurEp
                    Case 0.1
                        Return {0.114, 0.16, 0.203, 0.243, 0.282, 0.329, 0.409, -1, -1}
                    Case 0.2
                        Return {0.434, 0.553, 0.661, 0.764, 0.899, -1, -1, -1, -1}
                    Case 0.3
                        Return {0.965, 1.148, 1.306, -1, -1, -1, -1, -1, -1}
                    Case 0.4
                        Return {1.634, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.5
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.6
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.7
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                    Case 0.8
                        Return {-1, -1, -1, -1, -1, -1, -1, -1, -1}
                End Select
        End Select







    End Function

    Private Function Interpole(x0 As Decimal, x1 As Decimal, x2 As Decimal, Val1 As Decimal, Val2 As Decimal) As Decimal
        '-----------------------------------------------------------------------------------------------
        '   26/06/23 :  Création - CVP
        '-----------------------------------------------------------------------------------------------
        '  Interpolation linéaire pour la recherche d'une valeur
        '-----------------------------------------------------------------------------------------------
        '   x0          [E] :   Position x où on recherche la valeur
        '   x1, x2      [E] :   Positions de x où la valeur est connue
        '   Val1, Val2  [E] :   Valeurs aux position x1 et x2
        '-----------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyVal As Decimal

        '--> Calcul

        MyVal = Val1 + (Val2 - Val1) / (x2 - x1) * (x0 - x1)

        Return MyVal

    End Function

#End Region

End Class
