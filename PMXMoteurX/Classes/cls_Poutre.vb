Imports System.ComponentModel
Imports CTICM_RDM

Public Class cls_Poutre

#Region " Enumérations et constantes "

    Public Const PORTEEDEFAUT As Decimal = 10
    Const PORTEECONSOLEDEFAUT As Decimal = 3
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

    Enum EnuTypeMaintiensPoutre
        NotRestrained
        FullyRestrained
        PointRestrained
    End Enum

    Enum EnuTypeLargeurParticipante
        LargeurGauche
        LargeurDroite
        LargeurTotale
    End Enum

#End Region

#Region " Variables "

    ''' <summary>
    ''' Identifiant de la poutre en cours
    ''' </summary>
    Public BeamID As String

    ''' <summary>
    ''' Commentaire associé à la poutre en cours 
    ''' </summary>
    Public Commentaire As String

    ''' <summary>
    ''' Nom de la poutre
    ''' </summary>
    Public Label As String

    '''' <summary>
    '''' Type de section de la poutre
    '''' </summary>
    'Public TypeSection As cls_Section.Enum_TypeSection

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
    Public NbPropping As Integer           'Il faut réserver la lettre p aux private

    ''' <summary>
    ''' Nombre de maintiens disposés sur la travée considérée
    ''' </summary>
    Public NbRestrain() As Integer

    ''' <summary>
    ''' Liste des maintiens disposés sur la poutre
    ''' </summary>
    Public Maintiens() As List(Of cls_Maintiens)

    ''' <summary>
    ''' Type de maintiens considéré sur la travée considérée
    ''' </summary>
    Public TypeMaintien As EnuTypeMaintiensPoutre

    ''' <summary>
    ''' Indice du maintien sélectionné pour le déplacer (utile pour le dessin uniquement)
    ''' </summary>
    Public pIndiceMaintienSelectionne As Integer

    'Public Sections() As cls_Section

    ''' <summary>
    ''' Sections par travéee
    ''' </summary>
    Public Section As New cls_Section

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

#Region " Attributs pour la connection "

    ''' <summary>
    ''' Indique si l'arrangement des goujons se fait automatiquement (True) ou non (False)
    ''' </summary>
    Public lAutomaticDesign As Boolean

    ''' <summary>
    ''' Indique l'espacement entre deux ondes consécutives dans le cas d'un bac transversal
    ''' </summary>
    Public ReadOnly Property Esp_longi_bac As Decimal
        Get
            Return Me.Dalle.Bac.Ep
        End Get
    End Property

    'Espacement = espacement longi entre les goujons 
    'Espacement_Bac_Trans = nombre d'ondes entre deux goujons 
    'nr = nombre de goujons disposés transversalement

    ''' <summary>
    ''' Longueur de la zone définie.
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' </summary>
    Public Longueur_Zone(,) As Decimal

    ''' <summary>
    ''' Nombre de zone définie pour une travée
    ''' </summary>
    Public NombreZone() As Integer

    ''' <summary>
    ''' Espacement longi entre goujons
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' </summary>
    Public Espacement(,) As Decimal

    ''' <summary>
    ''' Nombre d'ondes entre deux goujons consécutifs
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' </summary>
    Public Espacement_Bac_Trans(,) As Integer

    ''' <summary>
    ''' Nombre de goujons disposés transversalement
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' </summary>
    Public NombreGoujonsTransv(,) As Integer

    ''' <summary>
    ''' Nombre total de goujons disposés sur la travée considérée   (#POM ce devrait être une fonction ou une propriété)
    ''' </summary>
    Public NombreGoujonsTot() As Integer


#End Region

#Region " Attributs pour les chargements et les combinaisons "

    '--Cas de charge définis par l'utilisateur

    Public ChargesU As New Dictionary(Of String, Cls_ChargementUtilisateur)

    '--Combinaisons définies par l'utilisateur

    Public Const nbCombELU As Integer = 5                           ' Nombre de combinaisons ELU
    Public Const nbCombELS As Integer = 5                           ' Nombre de combinaisons ELS
    Public Const nbCombFeu As Integer = 4                           ' Nombre de combinaisons ELU incendie
    Public Const nbCombELUConstruction As Integer = 1
    Public Const nbCombELSConstruction As Integer = 1
    Public lCombELU(nbCombELU) As Boolean                           'Indique si combinaison ELU sélectionnée
    Public lCombELS(nbCombELS) As Boolean                           'Indique si combinaison ELS sélectionnée
    Public lCombFeu(nbCombFeu) As Boolean                           'Indique si combinaison Feu sélectionnée
    Public lCombELCURules(nbCombELUConstruction) As Boolean         'Indique si combinaison réglementaire ELU Phase de construction
    Public lCombELCSRules(nbCombELSConstruction) As Boolean         'Indique si combinaison réglementaire ELU Phase de construction

    Public CoefCombELU(nbCombELU) As List(Of Decimal)               'Table des coefficients des combinaisons ELU
    Public CoefCombELS(nbCombELS) As List(Of Decimal)               'Table des coefficients des combinaisons ELS
    Public CoefCombFeu(nbCombFeu) As List(Of Decimal)               'Table des coefficients des combinaisons Feu
    Public CoefCombELCU(nbCombELUConstruction) As List(Of Decimal)  'Table des coefficients des combinaisons ELU Construction
    Public CoefCombELCS(nbCombELSConstruction) As List(Of Decimal)  'Table des coefficients des combinaisons ELS Construction

    '--Cas de charge pour l'analyse

    Public ChargesA As New List(Of Cls_CasDeCharge)

#End Region

#Region " Variables pour les valeurs par défaut et le statut de la poutre "

    ''' <summary>
    ''' Indique si la définition des portées, entraxes et trémies est celle par défaut
    ''' </summary>
    Public lDefautPortee As Boolean
    Public lDefautEnrobage As Boolean
    Public lDefautEtaiement As Boolean
    Public lDefautDalle As Boolean

    ''' <summary>
    ''' Indique si la poutre est enregistrée
    ''' </summary>
    Public lDonneesSauvees As Boolean

    ''' <summary>
    ''' Indique si nouvelle poutre, jamais encore modifiée
    ''' </summary>
    Public NouvellePoutre As Boolean

#End Region

#Region " Variables pour la modélisation "

    Public Nodes As strucBeamNodes
    Public Elements As New List(Of strucBeamElements)

    Public Structure strucBeamNodes
        Dim nbNodes As Integer              ' Nombre de noeuds de discrétisation le long de la poutre
        Dim xGlobal() As Decimal            ' Position x globale du noeud / extrémité gauche de la poutre
        Dim xTravee() As Decimal            ' Position x dans la travée locale / extremité gauche de la travée 
        '                                     pour un noeud sur 2 travées, x de la travée à gauche
        Dim iNodeAppui(,) As Integer        ' Table des indices des noeuds au droit des extremités de console (indice 1: indice travée, indice 2 : 0 ou 1 pour extrémité)
    End Structure

    Public Structure strucBeamElements
        Dim lMixte As Boolean               ' Indique si propriétés mixtes ou acier
        Dim InertieY() As Decimal           ' Table des inerties des sections
        Dim Aire() As Decimal               ' Table des aires des sections
        Dim nEqC As Decimal                 ' si mixte, coefficient d'équivalence acier-béton pour la dalle
        Dim nEqEC As Decimal                ' si mixte, coefficient d'équivalence acier-béton pour l'enrobage partiel
    End Structure

#End Region

#Region " CONSTRUCTEURS "

    Private Sub InitialiseChargements()
        Me.ChargesU.Add("G1", New Cls_ChargementUtilisateur("Poids propre", Me.IndiceDerniereTravee))
        Me.ChargesU.Add("G2", New Cls_ChargementUtilisateur("Autres charges permanentes", Me.IndiceDerniereTravee))
        Me.ChargesU.Add("Q1", New Cls_ChargementUtilisateur("Charges d'expoitation 1", Me.IndiceDerniereTravee))
        Me.ChargesU.Add("Q2", New Cls_ChargementUtilisateur("Charges d'expoitation 2", Me.IndiceDerniereTravee))
        Me.ChargesU.Add("QC", New Cls_ChargementUtilisateur("Charges de construction", Me.IndiceDerniereTravee))
    End Sub

    Private Sub InitialiseTablesCombi()
        Dim nbCharges As Integer = 5
        Dim i, j As Integer
        For i = 0 To nbCombELU
            Me.CoefCombELU(i) = New List(Of Decimal)
            For j = 1 To nbCharges
                Me.CoefCombELU(i).Add(0)
            Next
        Next
        For i = 0 To nbCombELS
            Me.CoefCombELS(i) = New List(Of Decimal)
            For j = 1 To nbCharges
                Me.CoefCombELS(i).Add(0)
            Next
        Next
        For i = 0 To nbCombFeu
            Me.CoefCombFeu(i) = New List(Of Decimal)
            For j = 1 To nbCharges
                Me.CoefCombFeu(i).Add(0)
            Next
        Next
        For i = 0 To nbCombELUConstruction
            Me.CoefCombELCU(i) = New List(Of Decimal)
            For j = 1 To nbCharges
                Me.CoefCombELCU(i).Add(0)
            Next
        Next
        For i = 0 To nbCombELSConstruction
            Me.CoefCombELCS(i) = New List(Of Decimal)
            For j = 1 To nbCharges
                Me.CoefCombELCS(i).Add(0)
            Next
        Next
    End Sub

    Public Sub New()

        Me.TypeSection = cls_Section.Enum_TypeSection.Acier
        ParametresGenerauxDefaut()
        PoutreDefautAcier()
        InitialiseChargements()
        InitialiseTablesCombi()

    End Sub

    Public Sub New(MyTypeSection As cls_Section.Enum_TypeSection, NomPoutre As String)

        Me.TypeSection = MyTypeSection
        Me.Label = NomPoutre
        ParametresGenerauxDefaut()

        '--> Poutre par défaut

        Select Case MyTypeSection
            Case cls_Section.Enum_TypeSection.Acier
                PoutreDefautAcier()
            Case cls_Section.Enum_TypeSection.AcierEnrobage
                PoutreDefautAcier()
                EnrobageDefaut()
            Case cls_Section.Enum_TypeSection.Mixte
                PoutreDefautAcier()
                DalleDefaut()
            Case cls_Section.Enum_TypeSection.MixteEnrobage
                PoutreDefautAcier()
                EnrobageDefaut()
                DalleDefaut()
        End Select
        InitialiseChargements()
        InitialiseTablesCombi()

    End Sub

    Private Sub ParametresGenerauxDefaut()
        Me.lDefautPortee = True
        Me.lDefautEtaiement = True
        Me.lDefautEnrobage = True
        Me.lDefautDalle = True
        Me.lDonneesSauvees = False
        Me.NouvellePoutre = True
    End Sub

    Private Sub PoutreDefautAcier()
        pNbTravees = 1
        ReDim LongueurTravee(pNbTravees + 2)
        ReDim TypTravee(pNbTravees + 2)
        ReDim Maintiens(pNbTravees + 2)
        ReDim NbRestrain(pNbTravees + 2)
        'ReDim TypeMaintien(pNbTravees + 2)


        TypeMaintien = EnuTypeMaintiensPoutre.NotRestrained

        For i As Integer = 0 To Maintiens.Length - 1
            Maintiens(i) = New List(Of cls_Maintiens)
        Next

        lTraveeConsoleDroite = False
        lTraveeConsoleGauche = False

        TypeEtaiement = EnuTypeEtaiement.UnPropped
        NbPropping = NBPROPPINGDEFAUT
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

        ReDim Longueur_Zone(pNbTravees + 2, 2)
        ReDim NombreZone(pNbTravees + 2)
        ReDim Espacement(pNbTravees + 2, 2)
        ReDim Espacement_Bac_Trans(pNbTravees + 2, 2)
        ReDim NombreGoujonsTransv(pNbTravees + 2, 2)
        ReDim NombreGoujonsTot(pNbTravees + 2)

        For i As Integer = 0 To pNbTravees + 2
            Longueur_Zone(i, 0) = LongueurTravee(i)
            Longueur_Zone(i, 1) = 0
            Longueur_Zone(i, 2) = 0
            NombreZone(i) = 1
            Espacement(i, 0) = 200 / 1000
            Espacement(i, 1) = 200 / 1000
            Espacement(i, 2) = 200 / 1000
            Espacement_Bac_Trans(i, 0) = 1
            Espacement_Bac_Trans(i, 1) = 1
            Espacement_Bac_Trans(i, 2) = 1
            NombreGoujonsTransv(i, 0) = 1
            NombreGoujonsTransv(i, 1) = 1
            NombreGoujonsTransv(i, 2) = 1
            For j As Integer = 0 To 2
                NombreGoujonsTot(i) += Longueur_Zone(i, j) / Espacement(i, j)
            Next
        Next

        lAutomaticDesign = False

    End Sub

    Private Sub EnrobageDefaut()

    End Sub

    Private Sub DalleDefaut()

    End Sub

#End Region

#Region " Outils divers "

    ''' <summary>
    ''' Type de section de la poutre
    ''' </summary>
    ''' <returns></returns>
    Public Property TypeSection As cls_Section.Enum_TypeSection
        Get
            Return Me.Section.typeSection
        End Get
        Set(value As cls_Section.Enum_TypeSection)
            Me.Section.typeSection = value
        End Set
    End Property

    ''' <summary>
    ''' Indique si poutre mixte
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property lMixte As Boolean
        Get
            Return Me.Section.lMixte
        End Get
    End Property

    ''' <summary>
    ''' Indique si poutre avec enrobage partiel
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property lEnrobage As Boolean
        Get
            Return Me.Section.lEnrobage
        End Get
    End Property

    ''' <summary>
    ''' Mise à jour des paramètres après modifications
    ''' </summary>
    Public Sub EstModifiee()
        '--------------------------------------------------------------------------

        Me.NouvellePoutre = False
        Me.lDonneesSauvees = False

    End Sub

    ''' <summary>
    ''' Identifie les différentes parties modifiées ou validées par l'utilisateur
    ''' </summary>
    ''' <param name="iFenetre"></param>
    Public Sub EstValidee(iFenetre As Integer)
        '--------------------------------------------------------------------------
        '   iFenetre    [E] :   Indice de la fenêtre qui modifie
        '--------------------------------------------------------------------------
        '   1 : Portées, entraxes et trémies
        '--------------------------------------------------------------------------
        Const iFRMPORTEE As Integer = 1
        Const iFRMETAIEMENT As Integer = 2
        Const iFRMENROBAGE As Integer = 3
        Const iFRMDALLE As Integer = 4

        Select Case iFenetre
            Case iFRMPORTEE : Me.lDefautPortee = False
            Case iFRMETAIEMENT : Me.lDefautEtaiement = False
            Case iFRMENROBAGE : Me.lDefautEnrobage = False
            Case iFRMDALLE : Me.lDefautDalle = False
        End Select
    End Sub


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

    Public Property NombreTraveesDeuxAppuis As Integer
        Get
            Return pNbTravees
        End Get
        Set(ByVal value As Integer)
            pNbTravees = value
        End Set
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

    Public ReadOnly Property LongueurTraveeMax As Decimal
        Get
            Dim Longueur As Decimal
            Dim i0, i1 As Integer
            i0 = Me.IndicePremiereTravee
            i1 = Me.IndiceDerniereTravee
            Longueur = Me.LongueurTravee(i0)
            For i As Integer = i0 + 1 To i1
                Longueur = Math.Max(Longueur, Me.LongueurTravee(i))
            Next

            Return Longueur
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
            Dim pHauteur As Decimal = Me.Section.ProfilA.ha


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

    Public Shared Sub DeepClone(ByVal PoutreSource As cls_Poutre, ByRef PoutreCible As cls_Poutre)
        '------------------------------------------------------------------------------------------------
        '   05/06/23 :  Clonage d'une poutre source vers la poutre interne
        '------------------------------------------------------------------------------------------------

        PoutreCible = PoutreSource.Clone()

        ReDim PoutreCible.LongueurTravee(PoutreSource.LongueurTravee.GetUpperBound(0))
        PoutreCible.LongueurTravee = PoutreSource.LongueurTravee.Clone

        ReDim PoutreCible.TypTravee(PoutreSource.TypTravee.GetUpperBound(0))
        PoutreCible.TypTravee = PoutreSource.TypTravee.Clone

        ReDim PoutreCible.NbRestrain(PoutreSource.NbRestrain.GetUpperBound(0))
        PoutreCible.NbRestrain = PoutreSource.NbRestrain.Clone

        'ReDim PoutreCible.TypeMaintien(PoutreSource.TypeMaintien.GetUpperBound(0))
        'PoutreCible.TypeMaintien = PoutreSource.TypeMaintien.Clone

        Cls_Dalle.DeepClone(PoutreSource.Dalle, PoutreCible.Dalle)
        PoutreCible.Dalle = PoutreSource.Dalle.Clone

        ReDim PoutreCible.Maintiens(PoutreSource.Maintiens.Length - 1)

        For i As Integer = 0 To PoutreSource.Maintiens.Length - 1

            PoutreCible.Maintiens(i) = New List(Of cls_Maintiens)

            For Each maintien As cls_Maintiens In PoutreSource.Maintiens(i)
                Dim maintien_local As New cls_Maintiens()
                maintien_local = maintien.Clone()
                PoutreCible.Maintiens(i).Add(maintien_local)
            Next
        Next

        'Clone des attributs pour la connexion
        ReDim PoutreCible.Longueur_Zone(PoutreSource.Longueur_Zone.GetUpperBound(0), PoutreSource.Longueur_Zone.GetUpperBound(1))
        PoutreCible.Longueur_Zone = PoutreSource.Longueur_Zone.Clone

        ReDim PoutreCible.NombreZone(PoutreSource.NombreZone.GetUpperBound(0))
        PoutreCible.NombreZone = PoutreSource.NombreZone.Clone

        ReDim PoutreCible.Espacement(PoutreSource.Espacement.GetUpperBound(0), PoutreSource.Espacement.GetUpperBound(1))
        PoutreCible.Espacement = PoutreSource.Espacement.Clone

        ReDim PoutreCible.Espacement_Bac_Trans(PoutreSource.Espacement_Bac_Trans.GetUpperBound(0), PoutreSource.Espacement_Bac_Trans.GetUpperBound(1))
        PoutreCible.Espacement_Bac_Trans = PoutreSource.Espacement_Bac_Trans.Clone

        ReDim PoutreCible.NombreGoujonsTransv(PoutreSource.NombreGoujonsTransv.GetUpperBound(0), PoutreSource.NombreGoujonsTransv.GetUpperBound(1))
        PoutreCible.NombreGoujonsTransv = PoutreSource.NombreGoujonsTransv.Clone

        ReDim PoutreCible.NombreGoujonsTot(PoutreSource.NombreGoujonsTot.GetUpperBound(0))
        PoutreCible.NombreGoujonsTot = PoutreSource.NombreGoujonsTot.Clone

        'Clone Section



        'Clone param calcul


    End Sub


#End Region

#Region " Calculs largeur participante "

    Public Function BeffDalle(xPositionSection As Decimal, i_travee As Integer, lSimplifiedModel As Boolean, lAnalysisModel As Boolean, Optional TypeLargeur As EnuTypeLargeurParticipante = EnuTypeLargeurParticipante.LargeurTotale) As Decimal

        '------------------------------------------------------------------------------------------------------------------
        '   16/06/23 :  Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   Calcul la largeur de la dalle participante à une position donnée
        '   Selon NF EN 1994-1-1 § 5
        '------------------------------------------------------------------------------------------------------------------
        '   xPositionSection    [E] :   Position de la section par rapport à l'appui gauche le plus proche ou du bord libre
        '   i_travee            [E] :   Indique l'indice de la travée à laquelle appartient la section considérée
        '   lSimplifiedModel    [E] :   Indique si on considère un modèle simplifié pour le calcul de la largeur participante (=True)
        '   lAnalysisModel      [E] :   Si lSimplifiedModel = True, indique si on considère le modèle pour l'analyse de la poutre (lAnalysisModel = True) ou la vérification de la section (lAnalysisModel = False)
        '   TypeLargeur         [E] :   Permet d'indiquer si on souhaite retourner la largeur participante à gauche de la poutre, à droite ou la largeur totale (par défaut)
        '   beff                [S] :   Retourne la valeur de la largeur participante
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim beff As Decimal

        Dim b1 As Decimal       'Largeur disponible à gauche de la poutre
        Dim b2 As Decimal       'Largeur disponible à droite de la poutre

        'Variables pour le calcul de la largeur participante à mi-travée

        Dim be1_m As Decimal    'Largeur participante de la dalle à gauche de la poutre à mi-travée
        Dim be2_m As Decimal    'Largeur participante de la dalle à droite de la poutre à mi-travée

        Dim Le_m As Decimal     'Distance entre points de moment nul pour une section à mi-travée

        Dim beff_m As Decimal   'Largeur participante à mi-travée

        'Variables pour le calcul de la largeur participante sur appui d'extrémité gauche (appui A)

        Dim be1_s_A As Decimal  'Largeur participante de la dalle à gauche de la poutre sur appui d'extrémité gauche
        Dim be2_s_A As Decimal  'Largeur participante de la dalle à droite de la poutre sur appui d'extrémité gauche

        Dim beta_1_A As Decimal
        Dim beta_2_A As Decimal

        Dim Le_s_A As Decimal   'Distance entre points de moment nul pour une section sur appui d'extrémité gauche

        Dim beff_s_A As Decimal 'Largeur participante sur appui d'extrémité gauche

        'Variables pour le calcul de la largeur participante sur appui d'extrémité gauche (appui B)

        Dim be1_s_B As Decimal 'Largeur participante de la dalle à gauche de la poutre sur appui d'extrémité droite
        Dim be2_s_B As Decimal 'Largeur participante de la dalle à droite de la poutre sur appui d'extrémité droite

        Dim beta_1_B As Decimal
        Dim beta_2_B As Decimal

        Dim Le_s_B As Decimal 'Distance entre points de moment nul pour une section sur appui d'extrémité droite

        Dim beff_s_B As Decimal 'Largeur participante sur appui d'extrémité droite

        '----- Calcul de la largeur disponible -----

        If lIntermediaire Then 'poutre intermédiaire
            If lTremieGauche Then
                b1 = Math.Min(DistanceDsl1, EntraxeD1 / 2)
            Else
                b1 = EntraxeD1 / 2
            End If

            If lTremieDroite Then
                b2 = Math.Min(DistanceDsl2, EntraxeD2 / 2)
            Else
                b2 = EntraxeD2 / 2
            End If

        Else 'poutre de rive

            b1 = EntraxeD1 'pas de trémie gauche pour les poutres de rive

            If lTremieDroite Then
                b2 = Math.Min(DistanceDsl2, EntraxeD2 / 2)
            Else
                b2 = EntraxeD2 / 2
            End If

        End If

        '----- Calcul de la largeur participante -----

        If i_travee = 0 Then 'on est dans la console de gauche

            Le_s_A = 2 * LongueurTravee(0)

            be1_s_A = Math.Min(Le_s_A / 8, b1)
            be2_s_A = Math.Min(Le_s_A / 8, b2)

            beta_1_A = Math.Min(1, 0.55 + 0.025 * Le_s_A / be1_s_A)
            beta_2_A = Math.Min(1, 0.55 + 0.025 * Le_s_A / be2_s_A)

            Select Case TypeLargeur
                Case EnuTypeLargeurParticipante.LargeurGauche
                    beff_s_A = beta_1_A * be1_s_A
                Case EnuTypeLargeurParticipante.LargeurDroite
                    beff_s_A = beta_2_A * be2_s_A
                Case EnuTypeLargeurParticipante.LargeurTotale
                    beff_s_A = beta_1_A * be1_s_A + beta_2_A * be2_s_A
            End Select

            Return beff_s_A

        ElseIf i_travee = IndiceTraveeConsoleDroite Then 'on est dans la console de droite

            Le_s_B = 2 * LongueurTravee(IndiceTraveeConsoleDroite)

            be1_s_B = Math.Min(Le_s_B / 8, b1)
            be2_s_B = Math.Min(Le_s_B / 8, b2)

            beta_1_B = Math.Min(1, 0.55 + 0.025 * Le_s_B / be1_s_B)
            beta_2_B = Math.Min(1, 0.55 + 0.025 * Le_s_B / be2_s_B)

            Select Case TypeLargeur
                Case EnuTypeLargeurParticipante.LargeurGauche
                    beff_s_B = beta_1_B * be1_s_B
                Case EnuTypeLargeurParticipante.LargeurDroite
                    beff_s_B = beta_2_B * be2_s_B
                Case EnuTypeLargeurParticipante.LargeurTotale
                    beff_s_B = beta_1_B * be1_s_B + beta_2_B * be2_s_B
            End Select

            Return beff_s_B

        Else 'on est dans une travée sur 2 appuis

            '----- Calcul de la largeur participante à mi-travée -----

            Select Case NombreTraveesDeuxAppuis
                Case 1
                    If lTraveeConsoleGauche Or lTraveeConsoleDroite Then
                        If lTraveeConsoleGauche And lTraveeConsoleDroite Then
                            Le_m = 0.7 * LongueurTravee(i_travee)
                        Else
                            Le_m = 0.85 * LongueurTravee(i_travee)
                        End If
                    Else
                        Le_m = LongueurTravee(i_travee)
                    End If
                Case 2
                    If i_travee = 1 Then
                        If lTraveeConsoleGauche Then
                            Le_m = 0.7 * LongueurTravee(i_travee)
                        Else
                            Le_m = 0.85 * LongueurTravee(i_travee)
                        End If
                    Else 'i_travee = 2
                        If lTraveeConsoleDroite Then
                            Le_m = 0.7 * LongueurTravee(i_travee)
                        Else
                            Le_m = 0.85 * LongueurTravee(i_travee)
                        End If
                    End If
                Case Else
                    If i_travee = 1 Then
                        If lTraveeConsoleGauche Then
                            Le_m = 0.7 * LongueurTravee(i_travee)
                        Else
                            Le_m = 0.85 * LongueurTravee(i_travee)
                        End If
                    ElseIf i_travee = IndiceTraveeConsoleDroite - 1 Then
                        If lTraveeConsoleDroite Then
                            Le_m = 0.7 * LongueurTravee(i_travee)
                        Else
                            Le_m = 0.85 * LongueurTravee(i_travee)
                        End If
                    Else
                        Le_m = 0.7 * LongueurTravee(i_travee)
                    End If

            End Select

            be1_m = Math.Min(Le_m / 8, b1)
            be2_m = Math.Min(Le_m / 8, b2)

            Select Case TypeLargeur
                Case EnuTypeLargeurParticipante.LargeurGauche
                    beff_m = be1_m
                Case EnuTypeLargeurParticipante.LargeurDroite
                    beff_m = be2_m
                Case EnuTypeLargeurParticipante.LargeurTotale
                    beff_m = be1_m + be2_m
            End Select


            '----- Calcul de la largeur participante sur appui gauche (appui A) -----

            Select Case NombreTraveesDeuxAppuis
                Case 1
                    If lTraveeConsoleGauche Then
                        Le_s_A = 2 * LongueurTravee(0)
                    Else
                        Le_s_A = Le_m
                    End If
                Case Else
                    If i_travee = 1 Then
                        If lTraveeConsoleGauche Then
                            Le_s_A = 2 * LongueurTravee(0)
                        Else
                            Le_s_A = Le_m
                        End If
                    Else
                        Le_s_A = 0.25 * (LongueurTravee(i_travee - 1) + LongueurTravee(i_travee))
                    End If

            End Select

            be1_s_A = Math.Min(Le_s_A / 8, b1)
            be2_s_A = Math.Min(Le_s_A / 8, b2)

            beta_1_A = Math.Min(1, 0.55 + 0.025 * Le_s_A / be1_s_A)
            beta_2_A = Math.Min(1, 0.55 + 0.025 * Le_s_A / be2_s_A)

            Select Case TypeLargeur
                Case EnuTypeLargeurParticipante.LargeurGauche
                    beff_s_A = beta_1_A * be1_s_A
                Case EnuTypeLargeurParticipante.LargeurDroite
                    beff_s_A = beta_2_A * be2_s_A
                Case EnuTypeLargeurParticipante.LargeurTotale
                    beff_s_A = beta_1_A * be1_s_A + beta_2_A * be2_s_A
            End Select


            '----- Calcul de la largeur participante sur appui droite (appui B) -----

            Select Case NombreTraveesDeuxAppuis
                Case 1
                    If lTraveeConsoleDroite Then
                        Le_s_B = 2 * LongueurTravee(IndiceTraveeConsoleDroite)
                    Else
                        Le_s_B = Le_m
                    End If
                Case Else
                    If i_travee = IndiceTraveeConsoleDroite - 1 Then
                        If lTraveeConsoleDroite Then
                            Le_s_B = 2 * LongueurTravee(IndiceTraveeConsoleDroite)
                        Else
                            Le_s_B = Le_m
                        End If
                    Else
                        Le_s_B = 0.25 * (LongueurTravee(i_travee) + LongueurTravee(i_travee + 1))
                    End If

            End Select

            be1_s_B = Math.Min(Le_s_B / 8, b1)
            be2_s_B = Math.Min(Le_s_B / 8, b2)

            beta_1_B = Math.Min(1, 0.55 + 0.025 * Le_s_B / be1_s_B)
            beta_2_B = Math.Min(1, 0.55 + 0.025 * Le_s_B / be2_s_B)

            Select Case TypeLargeur
                Case EnuTypeLargeurParticipante.LargeurGauche
                    beff_s_B = beta_1_B * be1_s_B
                Case EnuTypeLargeurParticipante.LargeurDroite
                    beff_s_B = beta_2_B * be2_s_B
                Case EnuTypeLargeurParticipante.LargeurTotale
                    beff_s_B = beta_1_B * be1_s_B + beta_2_B * be2_s_B
            End Select

            '----- Calcul de la largeur participante pour une section quelconque -----


            If lSimplifiedModel Then 'Modèle simplifié pour le calcul de la largeur participante
                If lAnalysisModel Then 'Calcul de la largeur participante pour l'analyse
                    beff = beff_m
                Else 'Calcul de la largeur participante pour la vérification de la section
                    Select Case xPositionSection / LongueurTravee(i_travee)
                        Case <= 0.15
                            beff = beff_s_A
                        Case >= 0.85
                            beff = beff_s_B
                        Case Else
                            beff = beff_m
                    End Select
                End If

            Else 'Modèle non simplifié

                Select Case xPositionSection / LongueurTravee(i_travee)
                    Case <= 0.25
                        beff = beff_s_A + 4 * xPositionSection / LongueurTravee(i_travee) * (beff_m - beff_s_A)
                    Case >= 0.75
                        beff = beff_m + 4 * (xPositionSection / LongueurTravee(i_travee) - 0.75) * (beff_s_B - beff_m)
                    Case Else
                        beff = beff_m
                End Select

            End If

            Return beff

        End If

    End Function

#End Region

#Region " Préparation des sections de calcul de la poutre "

    Public Sub PrepareNodes(dEltMax As Decimal, nbMinInter As Integer, nbMinConsole As Integer)
        '-------------------------------------------------------------------------------------------
        '   11/08/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Préparation des sections de calcul de la poutre
        '-------------------------------------------------------------------------------------------
        '   dEltMax     [E] :   Distance maxi entre 2 noeuds
        '   nbMinInter  [E] :   Nombre mini de noeuds par travée intermédiaire
        '   nbMinConsole[E] :   Nombre mini de noeuds par console
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xo, DeltaX As Decimal
        Dim nDec As Integer
        Dim Longueur As Decimal
        Dim lFirst As Boolean = True
        Dim i0 As Integer = 0
        Dim iGauche As Integer
        Dim lConsole As Integer
        Dim iTraveeG, iTraveeD As Integer
        Dim nbMin As Integer

        '--> Initialisation

        ReDim Nodes.iNodeAppui(Me.IndiceDerniereTravee, 1)

        '--> Boucle sur les travées

        iTraveeG = Me.IndicePremiereTravee
        iTraveeD = Me.IndiceDerniereTravee

        For iTravee As Integer = iTraveeG To iTraveeD

            lConsole = (iTravee = 0) Or ((iTravee = iTraveeD) And Me.lTraveeConsoleDroite)

            xo = Me.xPositionAppui(True, iTravee)
            Longueur = Me.LongueurTravee(iTravee)

            nDec = Math.Floor(Longueur / dEltMax) + 1

            If lConsole Then nbMin = nbMinConsole Else nbMin = nbMinInter

            nDec = Math.Max(nbMin, nDec)

            ' On ne prend que des nombres pairs pour la découpe (cela garantit un point à mi portée)
            If nDec Mod 2 = 1 Then nDec += 1

            DeltaX = Longueur / nDec

            If lFirst Then
                Nodes.nbNodes = nDec + 1
                ReDim Me.Nodes.xTravee(nDec)
                ReDim Me.Nodes.xGlobal(nDec)
                Nodes.iNodeAppui(iTravee, 0) = 0
                Nodes.iNodeAppui(iTravee, 1) = nDec
                iGauche = 0
            Else
                iGauche = Nodes.nbNodes - 1
                Nodes.iNodeAppui(iTravee, 0) = Nodes.nbNodes - 1
                Nodes.iNodeAppui(iTravee, 1) = Nodes.nbNodes + nDec - 1
                Nodes.nbNodes += nDec
                ReDim Preserve Me.Nodes.xTravee(Nodes.nbNodes - 1)
                ReDim Preserve Me.Nodes.xGlobal(Nodes.nbNodes - 1)
            End If

            For i As Integer = i0 To nDec
                Me.Nodes.xTravee(iGauche + i) = DeltaX * i
                Me.Nodes.xGlobal(iGauche + i) = xo + DeltaX * i
            Next

            lFirst = False
            i0 = 1
        Next

    End Sub

    Public Sub PrepareNodesN(dEltMax As Decimal, nbMinInter As Integer, nbMinConsole As Integer)
        '-------------------------------------------------------------------------------------------
        '   17/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Préparation des sections de calcul de la poutre
        '-------------------------------------------------------------------------------------------
        '   dEltMax     [E] :   Distance maxi entre 2 noeuds
        '   nbMinInter  [E] :   Nombre mini de noeuds par travée intermédiaire
        '   nbMinConsole[E] :   Nombre mini de noeuds par console
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim xImp As New List(Of Decimal)      ' Position des noeuds imposés
        Dim xo, xe As Decimal
        Dim iTravee, i0, iGauche As Integer
        Dim xCum As Decimal
        Dim lTrouve As Boolean
        Dim lConsole As Boolean
        Dim Longueur, DeltaX As Decimal
        Dim nDec, nbMin As Integer
        Dim lFirst As Boolean = True
        Dim lAppG, lAppD As Boolean
        Dim dElMaxTravee, dElMaxTroncon As Decimal

        '--> Initialisation des noeuds imposés

        InitialiseNoeudsImposes(xImp)
        iTravee = Me.IndicePremiereTravee
        xCum = Me.xPositionAppui(False, iTravee)
        ReDim Nodes.iNodeAppui(Me.IndiceDerniereTravee, 1)
        i0 = 0
        Nodes.nbNodes = 0

        '--> Maillage des tronçons entre noeuds imposés

        For i As Integer = 0 To xImp.Count - 2
            xo = xImp(i)
            xe = xImp(i + 1)

            lTrouve = IsSmallerOrEqual(xe, xCum)

            Do While (Not lTrouve) And (iTravee < Me.IndiceDerniereTravee)
                iTravee += 1
                xCum = Me.xPositionAppui(False, iTravee)
                lTrouve = IsSmallerOrEqual(xe, xCum)
            Loop

            If Not lTrouve Then
                '== Gestion d'une erreur qui ne doit pas arriver
                MsgBox("Error creation of nodes", MsgBoxStyle.Critical, "cls_Poutre/PrepareNodeN")
                Exit Sub
            End If

            lConsole = (iTravee = 0) Or (iTravee > Me.NombreTraveesDeuxAppuis)
            lAppG = IsEqual(xo, Me.xPositionAppui(True, iTravee))
            lAppD = IsEqual(xe, Me.xPositionAppui(False, iTravee))

            Longueur = xe - xo

            If lConsole Then nbMin = nbMinConsole Else nbMin = nbMinInter
            dElMaxTravee = Me.LongueurTravee(iTravee) / nbMin
            dElMaxTroncon = Math.Min(dElMaxTravee, dEltMax)

            nDec = Math.Floor(Longueur / dElMaxTroncon)
            If Not IsEqual(nDec * dElMaxTroncon, Longueur) Then nDec += 1

            DeltaX = Longueur / nDec

            If lFirst Then
                Nodes.nbNodes = nDec + 1
                ReDim Me.Nodes.xTravee(nDec)
                ReDim Me.Nodes.xGlobal(nDec)
                iGauche = 0
            Else
                iGauche = Nodes.nbNodes - 1
                Nodes.nbNodes += nDec
                ReDim Preserve Me.Nodes.xTravee(Nodes.nbNodes - 1)
                ReDim Preserve Me.Nodes.xGlobal(Nodes.nbNodes - 1)
            End If

            If lAppG Then Nodes.iNodeAppui(iTravee, 0) = Nodes.nbNodes - nDec - 1
            If lAppD Then Nodes.iNodeAppui(iTravee, 1) = Nodes.nbNodes - 1

            For j As Integer = i0 To nDec
                Me.Nodes.xTravee(iGauche + j) = DeltaX * j + xo - Me.xPositionAppui(True, iTravee)
                Me.Nodes.xGlobal(iGauche + j) = xo + DeltaX * j
            Next

            lFirst = False
            i0 = 1
        Next
    End Sub

    Private Sub InitialiseNoeudsImposes(ByRef xImp As List(Of Decimal))

        '--> Initialisation

        xImp.Clear()

        '--> Définition des noeuds imposés

        '# Extrémités

        AjouteNoeudImpose(0, xImp)
        AjouteNoeudImpose(Me.LongueurTotale, xImp)

        '# Appuis intermédiaires

        Dim i0 As Integer = Me.IndicePremiereTravee
        For i As Integer = 0 To Me.NbTravees - 2
            AjouteNoeudImpose(Me.xPositionAppui(False, i + i0), xImp)
        Next

        '# Mi-travées des travées intermédiaires

        For i As Integer = 1 To Me.NombreTraveesDeuxAppuis
            AjouteNoeudImpose(Me.xPositionAppui(True, i) + Me.LongueurTravee(i) / 2, xImp)
        Next

        '# Position des étais ponctuels

        '# Maitiens latéraux

        '# Position des zones fissurées

        If Me.lMixte And Me.NbTravees > 1 Then
            For i As Integer = 1 To Me.NombreTraveesDeuxAppuis
                If (i = 1) Then
                    If Me.lTraveeConsoleGauche Then
                        AjouteNoeudImpose(Me.xPositionAppui(True, i) + Me.LongueurTravee(i) * 0.15, xImp)
                    End If
                End If
                If (i < Me.NbTravees) Then
                    AjouteNoeudImpose(Me.xPositionAppui(True, i) + Me.LongueurTravee(i) * 0.85, xImp)
                End If
            Next
        End If

    End Sub

    Private Sub AjouteNoeudImpose(MyxImp As Decimal, ByRef xImp As List(Of Decimal))
        '-------------------------------------------------------------------------------------------
        '   17/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Ajout d'un noeud dans la liste des noeuds imposés
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lTrouve As Boolean = False
        Dim iX As Integer = 0
        Dim lCont As Boolean = (iX <= xImp.Count - 1)
        Const DeltaX As Decimal = 0.01

        '--> La position envoyée est elle déjà dans la liste

        Do While lCont

            lTrouve = IsEqual(MyxImp, xImp(iX), DeltaX)

            If lTrouve Then
                lCont = False
            Else
                iX += 1
                lCont = (iX <= xImp.Count - 1)
            End If

        Loop

        If Not lTrouve Then
            xImp.Add(MyxImp)
            xImp.Sort()
        End If

    End Sub

#End Region

#Region " Maillage : propriétés des barres le longe de la poutre "

    Public Function IndiceTabElts(lMixte As Boolean, nEqDal As Decimal, nEqEc As Decimal) As Integer
        '-------------------------------------------------------------------------------------------
        '   07/09/23 :  Création - POM - V1.00
        '-------------------------------------------------------------------------------------------
        '   Renvoie l'indice de la table des propriétés des élements à prendre en compte dans le calcul
        '   Si la table demandée n'existe pas, elle est créée automatiquement
        '-------------------------------------------------------------------------------------------
        '   lMixte          [E] :   Indique si propriétés en phase mixte ou non mixte (pour la dalle)
        '   nEqDal          [E] :   Si mixte, coefficient d'équivalence acier béton pour la dalle
        '   nEqEc           [E] :   Coefficient d'équivalence acier béton pour l'enrobage
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lTrouve As Boolean = False
        Dim iTab As Integer = -1
        Dim indexT As Integer
        Dim SigneM() As Decimal

        '--> Initialisation

        InitialiseSigneMoment(SigneM)

        '--> On commence par chercher si la table demandée existe

        Do While (Not lTrouve) And (iTab < Me.Elements.Count - 1)
            iTab += 1
            lTrouve = (lMixte = Me.Elements(iTab).lMixte) _
                  And (nEqDal = Me.Elements(iTab).nEqC) _
                  And (nEqEc = Me.Elements(iTab).nEqEC)
        Loop

        If lTrouve Then
            indexT = iTab
        Else
            AjouteTabElements(lMixte, nEqDal, nEqEc, SigneM)
            indexT = Me.Elements.Count - 1
        End If

        Return indexT
    End Function

    Private Sub AjouteTabElements(lMixte As Boolean, nEqDal As Decimal, nEqEc As Decimal, pSigneM() As Decimal)
        '-------------------------------------------------------------------------------------------
        '   07/09/23 :  Création - POM - V1.00
        '-------------------------------------------------------------------------------------------
        '   Crée une table des propriétés des élements à prendre en compte dans le calcul
        '-------------------------------------------------------------------------------------------
        '   lMixte          [E] :   Indique si propriétés en phase mixte ou non mixte (pour la dalle)
        '   nEqDal          [E] :   Si mixte, coefficient d'équivalence acier béton pour la dalle
        '   nEqEc           [E] :   Coefficient d'équivalence acier béton pour l'enrobage
        '   pSigneM         [E] :   Table de signes de moment le long de la poutre
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyElts As strucBeamElements
        Dim iEltO, iEltE As Integer
        Dim lEnrob As Boolean = Me.lEnrobage
        Dim lPoutreMixte As Boolean = Me.lMixte
        Dim Aire, InertieY As Decimal
        Dim zANE, MelRd As Decimal
        Dim iElt As Integer
        Dim Beff As Decimal
        Dim xm As Decimal
        Dim BeffPrec As Decimal
        Dim SigneMprec As Decimal
        Dim lCalcul As Boolean

        '--> Initialisation

        MyElts.lMixte = lMixte
        MyElts.nEqC = nEqDal
        MyElts.nEqEC = nEqEc

        ReDim MyElts.Aire(Me.Nodes.nbNodes - 2)
        ReDim MyElts.InertieY(Me.Nodes.nbNodes - 2)

        '--> Cas très simple ou tout est constant

        If (Not lMixte) And (Not lEnrob) Then
            Me.Section.ProfilA.ProprietesElastiquesMyy(1, False, 1, zANE, InertieY, MelRd)
            Aire = Me.Section.ProfilA.Aire
            For iElt = 0 To Nodes.nbNodes - 2
                MyElts.Aire(iElt) = Aire
                MyElts.InertieY(iElt) = InertieY
            Next
        End If

        '--> Boucle sur les travées, dans le cas où il faut prendre en compte le béton

        For iTravee As Integer = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
            iEltO = Me.Nodes.iNodeAppui(iTravee, 0)
            iEltE = Me.Nodes.iNodeAppui(iTravee, 1) - 1

            For iElt = iEltO To iEltE
                xm = (Me.Nodes.xTravee(iElt) + Me.Nodes.xTravee(iElt + 1)) / 2
                Beff = Me.BeffDalle(xm, iTravee, False, True)

                If iElt = iEltO Then
                    lCalcul = True
                Else
                    lCalcul = Not ((SigneMprec = pSigneM(iElt)) And (BeffPrec = Beff))
                End If

                If lCalcul Then
                    InertieY = Me.Section.InertieYY(pSigneM(iElt), False, Me.Param.Gamma, nEqEc, lMixte, nEqDal, Beff, Me.Dalle)
                    MyElts.InertieY(iElt) = InertieY
                    BeffPrec = Beff
                    SigneMprec = pSigneM(iElt)
                End If
                MyElts.InertieY(iElt) = InertieY
            Next

        Next

        '--> Fin

        Me.Elements.Add(MyElts)
    End Sub

    Private Sub InitialiseSigneMoment(ByRef pSigneM() As Decimal)
        '-------------------------------------------------------------------------------------------
        '   07/09/23 :  Création - POM - V1.00
        '-------------------------------------------------------------------------------------------
        '   Génére pour chaque élément du maillage le signe de moment à considérer pour l'analyse
        '-------------------------------------------------------------------------------------------
        '   pSigneM     [S] :   Table de signes de moment
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iEltO, iEltE As Integer
        Dim lConsole As Boolean
        Dim lContinuG, lContinuD As Boolean
        Dim xm, xTo, xTe As Decimal

        '--> Initialisation

        ReDim pSigneM(Me.Nodes.nbNodes - 2)

        '--> Traitement

        For iTravee As Integer = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
            lConsole = (iTravee = 0) Or (iTravee > Me.NombreTraveesDeuxAppuis)
            iEltO = Me.Nodes.iNodeAppui(iTravee, 0)
            iEltE = Me.Nodes.iNodeAppui(iTravee, 1) - 1

            If lConsole Then
                For i As Integer = iEltO To iEltE
                    pSigneM(i) = -1
                Next
            Else
                lContinuG = (iTravee > 1) Or ((iTravee = 1) And Me.lTraveeConsoleGauche)
                lContinuD = (iTravee < Me.NombreTraveesDeuxAppuis) Or ((iTravee = Me.NombreTraveesDeuxAppuis) And Me.lTraveeConsoleDroite)
                xTo = Me.xPositionAppui(True, iTravee)
                xTe = Me.xPositionAppui(False, iTravee)

                For iElt As Integer = iEltO To iEltE
                    xm = (Me.Nodes.xTravee(iElt) + Me.Nodes.xTravee(iElt + 1)) / 2
                    If IsSmallerOrEqual(xm - xTo, 0.15 * Me.LongueurTravee(iTravee)) Then
                        If lContinuG Then
                            pSigneM(iElt) = -1
                        Else
                            pSigneM(iElt) = 1
                        End If
                    ElseIf IsSmallerOrEqual(xTe - xm, 0.15 * Me.LongueurTravee(iTravee)) Then
                        If lContinuD Then
                            pSigneM(iElt) = -1
                        Else
                            pSigneM(iElt) = 1
                        End If
                    Else
                        pSigneM(iElt) = 1
                    End If
                Next

            End If
        Next
    End Sub

    Private Sub ProprieteElement(IndElt As Integer, iTravee As Integer, lMixte As Boolean, nEqC As Decimal, nEqEc As Decimal,
                                 ByRef Aire As Decimal, ByRef Inertie As Decimal)
        '-------------------------------------------------------------------------------------------
        '   07/09/23 :  Création - POM - V1.00
        '-------------------------------------------------------------------------------------------
        '   Propriété d'une élement de la modélisation
        '-------------------------------------------------------------------------------------------
        '   IndElt          [E] :   Indice de l'élément
        '   iTravee         [E] :   Indice de la travée
        '   lMixte          [E] :   Indique si propriétés en phase mixte ou non mixte (pour la dalle)
        '   nEqC            [E] :   Si mixte, coefficient d'équivalence acier béton pour la dalle
        '   nEqEc           [E] :   Coefficient d'équivalence acier béton pour l'enrobage
        '
        '   Aire            [E] :   Aire de l'élément
        '   Inertie         [E] :   Inertie / axe fort de l'élément
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim xm As Decimal

        '--> Initialisation

        xm = (Me.Nodes.xTravee(IndElt) + Me.Nodes.xTravee(IndElt + 1)) / 2



    End Sub

#End Region

#Region " Chargements, poids propre et combinaisons "

    Public Sub InitialiseCasdeChargesCalcul()
        '-------------------------------------------------------------------------------------------
        '   07/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Initialisation des cas de charges à traiter par le moteur de calcul
        '-------------------------------------------------------------------------------------------
        '   
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lMixte As Boolean
        Dim lEtaitComplet As Boolean
        Dim lNonEtaye As Boolean
        Dim lEnrob As Boolean

        Dim nEqDalleCT, nEqDalleLT As Decimal
        Dim nEqEnrobCT, nEqEnrobLT As Decimal

        Dim strChargesPermanentes As String = "Permanent loads"
        Dim strPoidsPropre As String = "Self weight"
        Dim strPoidsPropreEtaye As String = "Self weight with props"
        Dim strPoidsPropreSansEtais As String = "Self weight without props"
        Dim strAutresChargesPermanentes As String = "Other permanent loads"
        Dim strExploitation As String = "Live loads"
        Dim strConfiguration As String = "Conf. no "
        Dim strRetraitDalle As String = "Shrinkage of the slab"
        Dim strRetraitEnrob As String = "Shrinkage of the encasement"
        Dim strConstruction As String = "Construction loads"

        Dim IndiceG As Integer
        Dim IndiceQ As Integer

        '--> Initialisation

        lMixte = Me.lMixte
        lEnrob = Me.lEnrobage
        lEtaitComplet = (TypeEtaiement = EnuTypeEtaiement.FullyPropped)
        lNonEtaye = (TypeEtaiement = EnuTypeEtaiement.UnPropped)

        nEqDalleCT = Me.Dalle.beton.CoefficientEquivalenceCT
        nEqEnrobCT = Me.Section.enrobage_partiel.Beton.CoefficientEquivalenceCT
        nEqDalleLT = 3 * nEqDalleCT
        nEqEnrobLT = 3 * nEqEnrobCT

        '--> Traitement des charges permanentes 

        '# Charges permanentes globales

        If (Not lMixte) Or lEtaitComplet Then
            IndiceG = Me.IndiceTabElts(lMixte, nEqDalleLT, nEqEnrobLT)
            Me.ChargesA.Add(New Cls_CasDeCharge(strChargesPermanentes, "G", IndiceG))
        End If

        '# Charges de poids propres pour les poutres mixtes non étayées

        If lMixte Then

            IndiceG = Me.IndiceTabElts(lMixte, nEqDalleLT, nEqEnrobLT)

            If lNonEtaye Then
                Me.ChargesA.Add(New Cls_CasDeCharge(strPoidsPropre, "G1", Me.IndiceTabElts(False, 0, nEqEnrobLT)))
            Else
                'Cas de l'étaiement ponctuel
                Me.ChargesA.Add(New Cls_CasDeCharge(strPoidsPropre, "G1pp", Me.IndiceTabElts(False, 0, nEqEnrobLT)))

                Me.ChargesA.Add(New Cls_CasDeCharge(strPoidsPropre, "G1c", IndiceG))

            End If

            Me.ChargesA.Add(New Cls_CasDeCharge(strAutresChargesPermanentes, "G2", IndiceG))

        End If

        '--> Charges d'exploitation

        IndiceQ = Me.IndiceTabElts(lMixte, nEqDalleCT, nEqEnrobCT)

        If Me.NbTravees = 1 Then
            Me.ChargesA.Add(New Cls_CasDeCharge(strExploitation & " 1", "Q1", IndiceQ))
            Me.ChargesA.Add(New Cls_CasDeCharge(strExploitation & " 2", "Q2", IndiceQ))

        Else

            Me.ChargesA.Add(New Cls_CasDeCharge(strExploitation & " 1 " & strConfiguration & " 1", "Q1#1", IndiceQ))
            Me.ChargesA.Add(New Cls_CasDeCharge(strExploitation & " 1 " & strConfiguration & " 2", "Q1#2", IndiceQ))
            Me.ChargesA.Add(New Cls_CasDeCharge(strExploitation & " 1 " & strConfiguration & " 3", "Q1#3", IndiceQ))

            Me.ChargesA.Add(New Cls_CasDeCharge(strExploitation & " 2 " & strConfiguration & " 1", "Q2#1", IndiceQ))
            Me.ChargesA.Add(New Cls_CasDeCharge(strExploitation & " 2 " & strConfiguration & " 2", "Q2#2", IndiceQ))
            Me.ChargesA.Add(New Cls_CasDeCharge(strExploitation & " 2 " & strConfiguration & " 3", "Q3#3", IndiceQ))

        End If

        '--> Retrait

        Dim IndiceSH As Integer = Me.IndiceTabElts(lMixte, nEqDalleLT, nEqEnrobLT)

        If lMixte Then
            Me.ChargesA.Add(New Cls_CasDeCharge(strRetraitDalle, "SHC", IndiceSH))
        End If

        If lEnrob Then
            Me.ChargesA.Add(New Cls_CasDeCharge(strRetraitEnrob, "SHE", IndiceSH))
        End If

        '--> Charges de construction

        If lMixte And (Not lEtaitComplet) Then
            Me.ChargesA.Add(New Cls_CasDeCharge(strConstruction, "QC", Me.IndiceTabElts(False, 0, nEqEnrobLT)))
        End If

    End Sub


    Public Sub InitialisePoidsPropres()
        '-------------------------------------------------------------------------------------------
        '   23/08/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Initialisation des charges réparties de poids propre
        '-------------------------------------------------------------------------------------------
        '   
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Const KEYPP As String = "G1"
        Dim qPP As Decimal
        Dim qPPA, qPPC, qPPP As Decimal
        Const G As Decimal = 9.81

        '--> Traitement

        qPPA = Me.Section.ProfilA.Aire * G

        For iTravee As Integer = Me.IndicePremiereTravee To Me.IndiceDerniereTravee

            Me.ChargesU(KEYPP).FReparties(iTravee).Add(New Cls_ForceRepartie(0, qPP, Me.LongueurTravee(iTravee), qPP))

        Next

    End Sub

    Public Sub MAJ_CoefficientsCombinaisons()
        '-------------------------------------------------------------------------------------
        '   21/08/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Mise à jour des coefficients des combinaisons selon règlement
        '-------------------------------------------------------------------------------------

        '--[ Combinaisons ELU ]---------------------------------------------

        '----| Première combinaison ELU

        Me.CoefCombELU(0)(0) = Me.Param.Gamma.GammaG_sup
        Me.CoefCombELU(0)(1) = Me.Param.Gamma.GammaQ
        Me.CoefCombELU(0)(2) = Me.Param.Gamma.GammaQ * Me.Param.Gamma.Psi0_Q2
        Me.CoefCombELU(0)(3) = 0
        Me.CoefCombELU(0)(4) = 0

        '----| Deuxième combinaison ELU

        Me.CoefCombELU(1)(0) = Me.Param.Gamma.GammaG_sup
        Me.CoefCombELU(1)(1) = Me.Param.Gamma.GammaQ * Me.Param.Gamma.Psi0_Q1
        Me.CoefCombELU(1)(2) = Me.Param.Gamma.GammaQ
        Me.CoefCombELU(1)(3) = 0
        Me.CoefCombELU(1)(4) = 0

        '----| Troisième combinaison ELU  

        Me.CoefCombELU(2)(0) = Me.Param.Gamma.GammaG_inf
        Me.CoefCombELU(2)(1) = Me.Param.Gamma.GammaQ
        Me.CoefCombELU(2)(2) = Me.Param.Gamma.GammaQ * Me.Param.Gamma.Psi0_Q2
        Me.CoefCombELU(2)(3) = 0
        Me.CoefCombELU(2)(4) = 0

        '----| Quatrième combinaison ELU

        Me.CoefCombELU(3)(0) = Me.Param.Gamma.GammaG_inf
        Me.CoefCombELU(3)(1) = Me.Param.Gamma.GammaQ * Me.Param.Gamma.Psi0_Q1
        Me.CoefCombELU(3)(2) = Me.Param.Gamma.GammaQ
        Me.CoefCombELU(3)(3) = 0
        Me.CoefCombELU(3)(4) = 0

        '--[ Combinaisons ELS ]---------------------------------------------

        '----| Première combinaison ELS

        Me.CoefCombELS(0)(0) = 1
        Me.CoefCombELS(0)(1) = 1
        Me.CoefCombELS(0)(2) = 0
        Me.CoefCombELS(0)(3) = 0
        Me.CoefCombELS(0)(4) = 0

        '----| Deuxième combinaison ELS

        Me.CoefCombELS(1)(0) = 1
        Me.CoefCombELS(1)(1) = 1
        Me.CoefCombELS(1)(2) = Me.Param.Gamma.Psi0_Q2
        Me.CoefCombELS(1)(3) = 0
        Me.CoefCombELS(1)(4) = 0

        '----| Troisième combinaison ELS

        Me.CoefCombELS(2)(0) = 1
        Me.CoefCombELS(2)(1) = 0
        Me.CoefCombELS(2)(2) = 1
        Me.CoefCombELS(2)(3) = 0
        Me.CoefCombELS(2)(4) = 0

        '----| Quatrième combinaison ELS

        Me.CoefCombELS(3)(0) = 1
        Me.CoefCombELS(3)(1) = Me.Param.Gamma.Psi0_Q1
        Me.CoefCombELS(3)(2) = 1
        Me.CoefCombELS(3)(3) = 0
        Me.CoefCombELS(3)(4) = 0

        '--[ Combinaisons FEU ]---------------------------------------------

        '----| Première combinaison ELU Feu

        Me.CoefCombFeu(0)(0) = 1.0!
        Me.CoefCombFeu(0)(1) = Me.Param.Gamma.Psi1_Q1
        Me.CoefCombFeu(0)(2) = Me.Param.Gamma.Psi2_Q2
        Me.CoefCombFeu(0)(3) = 0
        Me.CoefCombFeu(0)(4) = 0

        '----| Deuxième combinaison ELU Feu

        Me.CoefCombFeu(1)(0) = 1.0!
        Me.CoefCombFeu(1)(1) = Me.Param.Gamma.Psi0_Q2
        Me.CoefCombFeu(1)(2) = Me.Param.Gamma.Psi2_Q2
        Me.CoefCombFeu(1)(3) = 0
        Me.CoefCombFeu(1)(4) = 0

        '----| Première combinaison ELU Feu

        Me.CoefCombFeu(2)(0) = 1.0!
        Me.CoefCombFeu(2)(1) = Me.Param.Gamma.Psi2_Q1
        Me.CoefCombFeu(2)(2) = Me.Param.Gamma.Psi1_Q2
        Me.CoefCombFeu(2)(3) = 0
        Me.CoefCombFeu(2)(4) = 0

        '--[ Combinaisons Construction ]---------------------------------------------

        '----| Première combinaison ELU

        Me.CoefCombELCU(0)(0) = 0
        Me.CoefCombELCU(0)(1) = 0
        Me.CoefCombELCU(0)(2) = 0
        Me.CoefCombELCU(0)(3) = Me.Param.Gamma.GammaQ
        Me.CoefCombELCU(0)(4) = Me.Param.Gamma.GammaG_sup

        '----| Première combinaison ELS

        Me.CoefCombELCS(0)(0) = 0
        Me.CoefCombELCS(0)(1) = 0
        Me.CoefCombELCS(0)(2) = 0
        Me.CoefCombELCS(0)(3) = 1
        Me.CoefCombELCS(0)(4) = 1

    End Sub

#End Region

#Region " Analyse "

    Public Sub CalculMNVInternes()
        '-------------------------------------------------------------------------------------
        '   21/08/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Analyse globale pour tous les cas de charges
        '-------------------------------------------------------------------------------------
        '   Avant de lancer ce calcul, in est nécessaire d'avoir effectuer InitialiseCalculs
        '-------------------------------------------------------------------------------------

        '--> Déclarations

        Dim jCdc As Integer
        Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
        Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
        Dim CodeError_RDM As Integer
        Dim TextError_RDM As String = String.Empty
        Dim DonneesEF As CTICM_RDM.DATA_RDM.Struc_Donnees = Nothing

        '--> Préparation du modele EF

        Me.PrepareModeleEF(DonneesEF)

        '--> Boucle sur les cas de charge

        For jCdc = 0 To Me.ChargesA.Count - 1
            PrepareDonneesEFChargement(jCdc, DonneesEF)
        Next

        '=== LANCER LE CALCUL ===

        Call MyDLLRDM.CALCULER(DonneesEF, MyOutput_RDM, CodeError_RDM, TextError_RDM)

    End Sub

    Public Sub InitialiseCalculs()
        '-------------------------------------------------------------------------------------
        '   07/09/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Initialisation des calculs RDM
        '-------------------------------------------------------------------------------------

        Me.PrepareNodesN(Me.Param.dMaxNodes, Me.Param.nbMinNodesTravee, Me.Param.nbMinNodesConsole)
        Me.InitialiseCasdeChargesCalcul()

    End Sub

    Private Sub PrepareModeleEF(ByRef pDonneesEF As CTICM_RDM.DATA_RDM.Struc_Donnees)
        '-------------------------------------------------------------------------------------
        '   07/09/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Préparation du modele EF avant lancement des calculs (hors propriétés éléments et chargements)
        '-------------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer

        '--> Paramètres généraux

        pDonneesEF.EYOUNG = Me.Section.Acier.EYoung * kConvMPaPa

        '--> Définition des noeuds

        '# nombre de noeuds

        pDonneesEF.NbNodes = Me.Nodes.nbNodes

        '# Position des noeuds EF
        ReDim pDonneesEF.xNode(pDonneesEF.NbNodes - 1)
        For i = 0 To pDonneesEF.NbNodes - 1
            pDonneesEF.xNode(i) = Me.Nodes.xGlobal(i)
        Next

        '# Elements (dimensions uniquement)
        ReDim pDonneesEF.Aire(pDonneesEF.NbNodes - 2)
        ReDim pDonneesEF.InertieY(pDonneesEF.NbNodes - 2)

        '# Appuis
        pDonneesEF.NbAppuis = Me.NombreTraveesDeuxAppuis + 1

        ReDim pDonneesEF.iNodeAppui(pDonneesEF.NbAppuis - 1)
        ReDim pDonneesEF.lAppuiArticule(pDonneesEF.NbAppuis - 1)

        pDonneesEF.iNodeAppui(0) = Me.Nodes.iNodeAppui(1, 0)
        pDonneesEF.lAppuiArticule(0) = False

        For i = 1 To Me.NombreTraveesDeuxAppuis
            pDonneesEF.iNodeAppui(i) = Me.Nodes.iNodeAppui(i, 1)
            pDonneesEF.lAppuiArticule(i) = False
        Next

    End Sub

    Private Sub PrepareDonneesEFChargement(iCas As Integer, ByRef pDonneesEF As CTICM_RDM.DATA_RDM.Struc_Donnees)
        '-------------------------------------------------------------------------------------
        '   07/09/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Préparation du modele EF dépendant du cas de charge (propriétés elements et charges)
        '-------------------------------------------------------------------------------------
        '   iCas        [E] :   Indice du cas de charge
        '   pDonneesEF  [S] :   Donnes pour le calcul EF
        '-------------------------------------------------------------------------------------

        '--> Transfert des propriétés de section

        For i As Integer = 0 To pDonneesEF.NbNodes - 2
            pDonneesEF.Aire(i) = Me.Elements(Me.ChargesA(iCas).IndElts).Aire(i)
            pDonneesEF.InertieY(i) = Me.Elements(Me.ChargesA(iCas).IndElts).InertieY(i)
        Next

        '--> Transfert des charges

    End Sub

#End Region

End Class
