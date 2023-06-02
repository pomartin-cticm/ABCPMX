Public Class cls_Poutre

#Region " Enumérations et constantes "

    Const PORTEEDEFAUT As Decimal = 10
    Const PORTEECONSOLEDEFAUT As Decimal = 3

    Enum EnuTypeTravee
        ConsoleGauche
        ConsoleDroite
        DeuxAppuis
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
    ''' Sections par travéee
    ''' </summary>
    Public Sections() As cls_Section

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

        LongueurTravee(1) = PORTEEDEFAUT
        TypTravee(1) = EnuTypeTravee.DeuxAppuis

        LongueurTravee(0) = PORTEECONSOLEDEFAUT
        TypTravee(0) = EnuTypeTravee.ConsoleGauche

        LongueurTravee(2) = PORTEECONSOLEDEFAUT
        TypTravee(2) = EnuTypeTravee.ConsoleDroite

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


End Class
