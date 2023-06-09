Public Class cls_Poutre

#Region " Enumérations et constantes "

    Const PORTEEDEFAUT As Decimal = 10.25
    Const PORTEECONSOLEDEFAUT As Decimal = 3.256
    Const ENTRAXEDEFAUT As Decimal = 2
    Const DISTANCETREMIEDEFAUT As Decimal = ENTRAXEDEFAUT / 2
    Const NBPROPPINGDEFAUT As Integer = 0


    Enum EnuTypeTravee
        ConsoleGauche
        ConsoleDroite
        DeuxAppuis
    End Enum

    Enum EnuTypeEtaiement
        UnPropped
        FullyPropped
        PointPropped
    End Enum
#End Region

#Region " Variables "

    ''' <summary>
    ''' Nom de la poutre
    ''' </summary>
    Public Label As String

    ''' <summary>
    ''' Type de section de la poutre
    ''' </summary>
    Public TypeSection As cls_Section.Enum_TypeSection

    ''' <summary>
    ''' Indique si présence d'une travée en console à gauche
    ''' </summary>
    Public lTraveeConsoleGauche As Boolean

    ''' <summary>
    ''' Indique si présence d'une travée en console à droite
    ''' </summary>
    Public lTraveeConsoleDroite As Boolean

    ''' <summary>
    ''' Indique si présence d'une trémie à gauche de la poutre
    ''' </summary>
    Public lTremieGauche As Boolean

    ''' <summary>
    ''' Indique si présence d'une trémie à droite de la poutre
    ''' </summary>
    Public lTremieDroite As Boolean

    ''' <summary>
    ''' Nombre de travées sur 2 appuis
    ''' </summary>
    Private pNbTravees As Integer
    '========================================================
    '   en indice O = travée en console gauche, si définie
    '   en indice 1 = 1ere travée centrale
    '   en indice pNbTtravees+1 = travee en cosole droite si définie
    '========================================================

    ''' <summary>
    ''' Longueur de chaque travee
    ''' </summary>
    Public LongueurTravee() As Decimal

    ''' <summary>
    ''' Types des travées
    ''' </summary>
    Public TypTravee() As EnuTypeTravee

    ''' <summary>
    ''' Type d'étaiement
    ''' </summary>
    Public TypeEtaiement As EnuTypeEtaiement

    ''' <summary>
    ''' Indique si présence d'un étai à l'extrémité de la console gauche
    ''' </summary>
    Public lEtaisConsoleGauche As Boolean

    ''' <summary>
    ''' Indique si présence d'un étai à l'extrémité de la console droite
    ''' </summary>
    Public lEtaisConsoleDroite As Boolean

    ''' <summary>
    ''' Nombre d'étais disposés par través entre deux appuis consécutifs
    ''' </summary>
    Public pNbPropping As Integer

    ''' <summary>
    ''' Sections par travéee
    ''' </summary>
    Public Sections() As cls_Section

    ''' <summary>
    ''' Entraxes aux poutres voisines
    ''' </summary>
    Public EntraxeD1 As Decimal
    Public EntraxeD2 As Decimal

    ''' <summary>
    ''' Distance entre la poutre et le bord des trémies
    ''' </summary>
    Public DistanceDsl1 As Decimal
    Public DistanceDsl2 As Decimal

    ''' <summary>
    ''' Type de poutre intermédiaire ou de rive
    ''' </summary>
    Public lIntermediaire As Boolean            ' Vrai => Poutre intermédiaire | Faux => Poutre de rive

    ''' <summary>
    ''' Dalle béton de la poutre
    ''' </summary>
    Public Dalle As New Cls_Dalle

    ''' <summary>
    ''' Options de calcul pour la poutre
    ''' </summary>
    Public Param As New Cls_OptionsCalcul

#End Region

#Region " CONSTRUCTEURS "

    Public Sub New()
        Me.TypeSection = cls_Section.Enum_TypeSection.Acier
        PoutreDefautAcier()
    End Sub

    Public Sub New(MyTypeSection As cls_Section.Enum_TypeSection, NomPoutre As String)

        Me.TypeSection = MyTypeSection
        Me.Label = NomPoutre

        '--> Poutre par défaut

        Select Case MyTypeSection
            Case cls_Section.Enum_TypeSection.Acier
                PoutreDefautAcier()
            Case cls_Section.Enum_TypeSection.AcierEnrobage
                PoutreDefautAcier()
                EnrobageDefaut()
        End Select


    End Sub

    Private Sub PoutreDefautAcier()
        pNbTravees = 1
        ReDim LongueurTravee(pNbTravees + 2)
        ReDim TypTravee(pNbTravees + 2)
        ReDim Sections(pNbTravees + 2)

        lTraveeConsoleDroite = False
        lTraveeConsoleGauche = False

        TypeEtaiement = EnuTypeEtaiement.UnPropped
        pNbPropping = NBPROPPINGDEFAUT
        lEtaisConsoleGauche = False
        lEtaisConsoleDroite = False

        lTremieGauche = False
        lTremieDroite = False


        LongueurTravee(1) = PORTEEDEFAUT
        TypTravee(1) = EnuTypeTravee.DeuxAppuis

        LongueurTravee(0) = PORTEECONSOLEDEFAUT
        TypTravee(0) = EnuTypeTravee.ConsoleGauche

        LongueurTravee(2) = PORTEECONSOLEDEFAUT
        TypTravee(2) = EnuTypeTravee.ConsoleDroite

        EntraxeD1 = ENTRAXEDEFAUT
        EntraxeD2 = ENTRAXEDEFAUT

        DistanceDsl1 = DISTANCETREMIEDEFAUT
        DistanceDsl2 = DISTANCETREMIEDEFAUT

        lIntermediaire = True

        For i As Int16 = 0 To 2
            Me.Sections(i) = New cls_Section
        Next
    End Sub

    Private Sub EnrobageDefaut()

    End Sub

    Private Sub DalleDefaut()

    End Sub

#End Region

#Region " Outils divers "

    ''' <summary>
    ''' Renvoie l'indice de travées où sont stockées les infos sur la console droite
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IndiceTraveeConsoleDroite As Integer
        Get
            Return Me.pNbTravees + 1
        End Get
    End Property

    ''' <summary>
    ''' Renvoie le nombre total de travées de la poutre
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property NbTravees As Integer
        Get
            Dim NbT As Integer = pNbTravees
            If Me.lTraveeConsoleGauche Then NbT += 1
            If Me.lTraveeConsoleDroite Then NbT += 1
            Return NbT
        End Get
    End Property

    ''' <summary>
    ''' Retourne l'indice de la première travée
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IndicePremiereTravee As Integer
        Get
            Dim Indice As Integer = 1
            If Me.lTraveeConsoleGauche Then Indice = 0
            Return Indice
        End Get
    End Property

    ''' <summary>
    ''' Retourne l'indice de la dernière travée
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property IndiceDerniereTravee As Integer
        Get
            Dim Indice As Integer = pNbTravees
            If Me.lTraveeConsoleDroite Then Indice += 1
            Return Indice
        End Get
    End Property

    Public ReadOnly Property NombreTraveesDeuxAppuis As Integer
        Get
            Return pNbTravees
        End Get
    End Property


    ''' <summary>
    ''' Longueur cumulée de toutes les travées
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property LongueurTotale As Decimal
        Get
            Dim pLongueur As Decimal = 0

            For i As Integer = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
                pLongueur += Me.LongueurTravee(i)
            Next

            Return pLongueur
        End Get
    End Property

    Public ReadOnly Property HauteurTotale As Decimal
        Get
            Dim pHauteur As Decimal = 0.5


            Return pHauteur
        End Get
    End Property

    Public ReadOnly Property HauteurMaxiProfiles As Decimal
        Get
            Dim Indice0 As Integer = Me.IndicePremiereTravee
            Dim pHauteur As Decimal = Me.Sections(Indice0).ProfilA.ha

            For i As Integer = Me.IndicePremiereTravee + 1 To Me.IndiceDerniereTravee
                pHauteur = Math.Max(pHauteur, Me.Sections(i).ProfilA.ha)
            Next
            Return pHauteur
        End Get
    End Property


    Public Function xPositionAppui(lGauche As Decimal, iTravee As Integer) As Decimal
        '-------------------------------------------------------------------------------------------
        '   02/06/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Renvoie la position x globale d'un appui de travée
        '   position x = 0 : à gauche de la première travée
        '-------------------------------------------------------------------------------------------
        '   lGauche     [E] :   Indique si  appui gauche ou droite
        '   iTravee     [E] :   Indice de la travée
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xPos As Decimal = 0

        For i As Integer = Me.IndicePremiereTravee To iTravee - 1
            xPos += Me.LongueurTravee(i)
        Next

        If Not lGauche Then xPos += Me.LongueurTravee(iTravee)

        Return xPos
    End Function
#End Region

#Region " Fonctions de copie "
    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function


    Public Shared Sub Clone(PoutreSource As cls_Poutre, ByRef PoutreCible As cls_Poutre)
        '------------------------------------------------------------------------------------------------
        '   05/06/23 :  Clonage d'une poutre source vers la poutre interne
        '------------------------------------------------------------------------------------------------

        PoutreCible = PoutreSource.Clone
        PoutreCible.Dalle = PoutreSource.Dalle.Clone

    End Sub


#End Region
End Class
