Public Class cls_Poutre

#Region " Enumérations et constantes "

    Public Const PORTEEDEFAUT As Decimal = 10.25
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

    Enum EnuTypeMaintiensPoutre
        NotRestrained
        FullyRestrained
        PointRestrained
    End Enum

#End Region

#Region " Variables "

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
    Public TypeMaintien() As EnuTypeMaintiensPoutre

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

#Region "Attributs pour la connection"

    ''' <summary>
    ''' Indique si l'arrangement des goujons se fait automatiquement (True) ou non (False)
    ''' </summary>
    Public lAutomaticDesign As Boolean

    ''' <summary>
    ''' Indique si l'arrangement tient compte de la présence d'un bac transversal
    ''' </summary>
    Public ReadOnly Property LBacTransv As Boolean
        Get
            Return Me.Dalle.type = Me.Dalle.Enum_TypeDalle.Mixte
        End Get
    End Property

    ''' <summary>
    ''' Indique l'espacement entre deux ondes consécutives dans le cas d'un bac transversal
    ''' </summary>
    Public ReadOnly Property Esp_longi_bac As Decimal
        Get
            Return Me.Dalle.Bac.e_p
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
    ''' Nombre total de goujons disposés sur la travée considérée
    ''' </summary>
    Public NombreGoujonsTot() As Integer


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

#Region " CONSTRUCTEURS "

    Public Sub New()

        Me.TypeSection = cls_Section.Enum_TypeSection.Acier
        ParametresGenerauxDefaut()
        PoutreDefautAcier()

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

        ReDim TypeMaintien(pNbTravees + 2)

        For i As Integer = 0 To TypeMaintien.Length - 1
            TypeMaintien(i) = EnuTypeMaintiensPoutre.NotRestrained
        Next

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

        PoutreCible = PoutreSource.Clone
        ReDim PoutreCible.LongueurTravee(PoutreSource.LongueurTravee.GetUpperBound(0))
        PoutreCible.LongueurTravee = PoutreSource.LongueurTravee.Clone
        PoutreCible.TypTravee = PoutreSource.TypTravee.Clone
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


    End Sub


#End Region

#Region "Calculs largeur participante"
    Public Function EffectiveWidth(xPositionSection As Decimal, i_travee As Integer, lSimplifiedModel As Boolean, lAnalysisModel As Boolean) As Decimal

        '------------------------------------------------------------------------------------------------------------------
        '   16/06/23 :  Création - GuD
        '------------------------------------------------------------------------------------------------------------------
        '   Calcul la largeur de la dalle participante à une position donnée
        '------------------------------------------------------------------------------------------------------------------
        '   xPositionSection    [E] :   Position de la section par rapport à l'appui gauche le plus proche ou du bord libre
        '   i_travee            [E] :   Indique l'indice de la travée à laquelle appartient la section considérée
        '   lSimplifiedModel    [E] :   Indique si on considère un modèle simplifié pour le calcul de la largeur participante (=True)
        '   lAnalysisModel      [E] :   Si lSimplifiedModel = True, indique si on considère le modèle pour l'analyse de la poutre (lAnalysisModel = True) ou la vérification de la section (lAnalysisModel = False)
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

            beff_s_A = beta_1_A * be1_s_A + beta_2_A * be2_s_A

            Return beff_s_A

        ElseIf i_travee = IndiceTraveeConsoleDroite Then 'on est dans la console de droite

            Le_s_B = 2 * LongueurTravee(IndiceTraveeConsoleDroite)

            be1_s_B = Math.Min(Le_s_B / 8, b1)
            be2_s_B = Math.Min(Le_s_B / 8, b2)

            beta_1_B = Math.Min(1, 0.55 + 0.025 * Le_s_B / be1_s_B)
            beta_2_B = Math.Min(1, 0.55 + 0.025 * Le_s_B / be2_s_B)

            beff_s_B = beta_1_B * be1_s_B + beta_2_B * be2_s_B

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

            beff_m = be1_m + be2_m

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

            beff_s_A = beta_1_A * be1_s_A + beta_2_A * be2_s_A

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

            beff_s_B = beta_1_B * be1_s_B + beta_2_B * be2_s_B

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

End Class
