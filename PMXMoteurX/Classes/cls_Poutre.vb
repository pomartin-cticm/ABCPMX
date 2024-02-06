Imports System.ComponentModel
Imports CTICM_RDM
Imports CTICM_DATA_DLLS
Imports System.Security.Policy

Public Class cls_Poutre

#Region " Enumérations et constantes "

    Public Const PORTEEDEFAUT As Decimal = 10
    Const PORTEECONSOLEDEFAUT As Decimal = 3
    Const ENTRAXEDEFAUT As Decimal = 2
    Const DISTANCETREMIEDEFAUT As Decimal = ENTRAXEDEFAUT / 2
    Const NBPROPPINGDEFAUT As Integer = 0
    Public Const KEYPP As String = "G1"


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

#Region " Attributs "

    '#####################################################################################
    '# Identification
    '#####################################################################################

    Public BeamID As String                                 ' Identifiant de la poutre 
    Public Commentaire As String                            ' Commentaire associé à la poutre 

    '#####################################################################################
    '# Définition de la poutre
    '#####################################################################################

    Public Section As New cls_Section                   ' Section (uniforme sur toute la longueur de la poutre)
    Public Dalle As New cls_Dalle                       ' Dalle béton de la poutre

    Public lIntermediaire As Boolean                    ' Vrai => Poutre intermédiaire | Faux => Poutre de rive
    Public EntraxeD1 As Decimal                         ' Entraxes aux poutres voisines
    Public EntraxeD2 As Decimal

    Public lTraveeConsoleGauche As Boolean              ' Indique si présence d'une travée en console à gauche
    Public lTraveeConsoleDroite As Boolean              ' Indique si présence d'une travée en console à droite

    ''' <summary>
    ''' Distance entre la poutre et le bord des trémies
    ''' </summary>
    Public DistanceDsl1 As Decimal                      ' Distance entre la poutre et le bord des trémies
    Public DistanceDsl2 As Decimal

    Public lTremieGauche As Boolean                     ' Indique si présence d'une trémie à gauche de la poutre
    Public lTremieDroite As Boolean                     ' Indique si présence d'une trémie à droite de la poutre

    ''' <summary>
    ''' Nombre de travées sur 2 appuis
    ''' </summary>
    Private pNbTravees As Integer
    '========================================================
    '   en indice O = travée en console gauche, si définie
    '   en indice 1 = 1ere travée centrale
    '   en indice pNbTtravees+1 = travee en cosole droite si définie
    '========================================================

    Public LongueurTravee() As Decimal                  ' Tableau des longueurs de travée
    Public TypTravee() As EnuTypeTravee                 ' Type des travées

    '#####################################################################################
    '# Définition de l'étaiement
    '#####################################################################################

    Public TypeEtaiement As EnuTypeEtaiement            ' Type d'étaiement (pour les poutres mixtes)

    Public lEtaisConsoleGauche As Boolean               ' Indique si présence d'un étai à l'extrémité de la console gauche
    Public lEtaisConsoleDroite As Boolean               ' Indique si présence d'un étai à l'extrémité de la console droite

    Public NbEtaiement As Integer                       ' Nombre d'étais disposés par través entre deux appuis consécutifs
    Public lEtaisSousProfileAcier As Boolean            ' Indique si les étais sont positionnés sous le profilé métallique (True) ou sous la dalle (False)

    '#####################################################################################
    '# Définition des maintiens latéraux
    '#####################################################################################

    Public NbMaintiens() As Integer                     ' Nombre de maintiens disposés sur la travée considérée
    Public Maintiens() As List(Of cls_Maintiens)        ' Liste des maintiens disposés sur la poutre
    Public TypeMaintien As EnuTypeMaintiensPoutre       ' Type de maintiens considéré sur la travée considérée

    Public pIndiceMaintienSelectionne As Integer        ' Indice du maintien sélectionné pour le déplacer (utile pour le dessin uniquement)

    Public MaintienBac As cls_MaintienBac               ' Conditions de maintien par le bac en phase de construction

    'Renvoi la somme de tous les maintiens disposés sur l'ensemble des travées 
    Public ReadOnly Property NombreTotalMaintiens As Integer
        Get
            Dim tot As Integer = 0
            If Me.TypeMaintien = EnuTypeEtaiement.PointPropped Then
                For i As Integer = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
                    tot += Maintiens(i).Count
                Next
            End If
            Return tot
        End Get
    End Property

    '#####################################################################################
    '# Paramètres pour les options de calcul
    '#####################################################################################

    ''' <summary>
    ''' Options de calcul pour la poutre
    ''' </summary>
    Public Param As New cls_OptionsCalcul

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
    Public LongueurZone(,) As Decimal

    ''' <summary>
    ''' Nombre de zone définie pour une travée
    ''' </summary>
    Public NombreZones() As Integer

    ''' <summary>
    ''' Abscisse du début d'une zone définie
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0,1,2)
    ''' </summary>
    Public ReadOnly Property xDebutZone As Decimal(,)
        Get
            Dim xDebutZoneRetour As Decimal(,)
            ReDim xDebutZoneRetour(Me.IndiceDerniereTravee, 2)
            For i_travee As Integer = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
                For j_zone As Integer = 0 To 2
                    For k_boucle As Integer = 0 To j_zone - 1
                        xDebutZoneRetour(i_travee, j_zone) += LongueurZone(i_travee, k_boucle)
                    Next
                Next
            Next

            Return xDebutZoneRetour
        End Get
    End Property

    ''' <summary>
    ''' Abscisse de la fin début d'une zone définie
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0,1,2)
    ''' </summary>
    Public ReadOnly Property xFinZone As Decimal(,)
        Get
            Dim xFinZoneRetour As Decimal(,)
            ReDim xFinZoneRetour(Me.IndiceDerniereTravee, 2)
            For i_travee As Integer = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
                For j_zone As Integer = 0 To 2
                    For k_boucle As Integer = 0 To j_zone
                        xFinZoneRetour(i_travee, j_zone) += LongueurZone(i_travee, k_boucle)
                    Next
                Next
            Next

            Return xFinZoneRetour
        End Get
    End Property

    ''' <summary>
    ''' Permet de renvoyer la valeur min de nr sur toute la poutre
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property nr_min As Integer
        Get
            Dim nr_retour As Integer = Me.NombreGoujonsTransv(Me.IndicePremiereTravee, 0)

            For i_travee As Integer = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
                For j_zone As Integer = 0 To Me.NombreZones(i_travee) - 1
                    nr_retour = Math.Min(nr_retour, Me.NombreGoujonsTransv(i_travee, j_zone))
                Next
            Next

            Return nr_retour
        End Get
    End Property

    ''' <summary>
    ''' Permt de renvoyer la valeur max de nr sur toute la poutre 
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property nr_max As Integer
        Get
            Dim nr_retour As Integer = Me.NombreGoujonsTransv(Me.IndicePremiereTravee, 0)

            For i_travee As Integer = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
                For j_zone As Integer = 0 To Me.NombreZones(i_travee) - 1
                    nr_retour = Math.Max(nr_retour, Me.NombreGoujonsTransv(i_travee, j_zone))
                Next
            Next

            Return nr_retour
        End Get
    End Property

    ''' <summary>
    ''' Espacement longi entre goujons
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' </summary>
    Public EspacementZone(,) As Decimal

    ''' <summary>
    ''' Nombre d'ondes entre deux goujons consécutifs
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' </summary>
    Public Espacement_Bac_TransZone(,) As Integer

    ''' <summary>
    ''' Nombre de goujons disposés transversalement
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' </summary>
    Public NombreGoujonsTransv(,) As Integer

    '''' <summary>
    '''' Nombre total de goujons disposés sur la travée considérée   
    '''' </summary>
    'Public NombreGoujonsTot() As Integer

    ''' <summary>
    ''' Densité de connexion par zone de connexion (PRd / unité de longueur)
    ''' </summary>
    Public DensiteConnexionZone(,) As Decimal

    ''' <summary>
    ''' Contrainte tangentielle / zone de flexion positive (True) ou négative (False) / Type de surface de ruine 
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' 3eme indice: indice de la zone de ruine: a-a (0), b-b (1) ou d-d (2)
    ''' </summary>
    Public TauEd(,,) As Decimal

    ''' <summary>
    ''' Angle de la bielle de compression EN RADIAN / zone de flexion positive (True) ou négative (False) / Type de surface de ruine 
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' 3eme indice: indice de la zone de ruine: a-a (0), b-b (1) ou d-d (2)
    ''' </summary>
    Public Thetaf(,,) As Decimal

    ''' <summary>
    ''' Angle min de la bielle de compression EN RADIAN (dépend de si la zone se situe en flexion positive ou négative)
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' </summary>
    Public Thetaf_min(,) As Decimal

    ''' <summary>
    ''' Vérification de la bielle de compression 
    ''' (GUD: pour l'instant je mets ici l'attribut car le critère est constant le long d'une zone de connexion. A voir s'il faut le déplacer dans la classe vérification)
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' 3eme indice: indice de la zone de ruine: a-a (0), b-b (1) ou d-d (2)
    ''' </summary>
    Public Gamma_sf(,,) As Decimal

    ''' <summary>
    ''' Aire par unité de longueur des armatures transversales / zone de flexion positive (True) ou négative (False) / Type de surface de ruine 
    ''' 1er indice: indice de la travée
    ''' 2eme indice: indice de la zone (0, 1 ou 2)
    ''' 3eme indice: indice de la zone de ruine: a-a (0), b-b (1) ou d-d (2)
    ''' </summary>
    Public As_s_transv(,,) As Decimal

    ''' <summary>
    ''' Propriétés renvoyant le nombre de lits d'armatures transversales disposées 
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property NbTransverseLayer As Integer
        Get
            Dim NbLayer As Integer

            If Me.Dalle.lMixte Then
                NbLayer = 1
            Else
                If Me.Dalle.Connecteur.hsc - 70 / 1000 <= Me.Dalle.t_h Then 'espace suffisant pour disposer 3 lits d'armatures transversales 
                    NbLayer = 3
                Else
                    NbLayer = 2
                End If

            End If

            Return NbLayer
        End Get
    End Property

    ''' <summary>
    ''' Fonction renvoyant la densité d'armatures min à disposer (m2/m2)
    ''' </summary>
    ''' <returns></returns>
    Public Function rho_t_min() As Decimal
        Dim rho_loc As Decimal
        rho_loc = 0.08 * Math.Sqrt(Me.Dalle.beton.Fck) / Me.Dalle.AcierArmatures.FsK
        Return rho_loc

    End Function

    ''' <summary>
    ''' Fonction renvoyant la quantité d'armatures min à disposer (m2/m) selon le §9.2.2 (5) de l'EC2
    ''' </summary>
    ''' <returns></returns>
    Public Function As_min_EC2() As Decimal
        Dim Asmin As Decimal
        Asmin = Me.rho_t_min * Me.Dalle.EpaisseurActive
        Return Asmin
    End Function

    ''' <summary>
    ''' Fonction renvoyant la quantité d'armatures min à disposer (m2/m) selon le §9.2.1 (4) de l'EC4
    ''' </summary>
    ''' <returns></returns>
    Public Function As_min_EC4() As Decimal
        Return 80 * 10 ^ (-6) '80 mm2/m
    End Function

    'Private Function IndiceZoneConnexion(iTravee As Integer, xPos As Decimal) As Integer
    '    '-----------------------------------------------------------------------------------------------------------------
    '    '   03/02/24 :  Création - POM - V1.00
    '    '-----------------------------------------------------------------------------------------------------------------
    '    '   Retourne l'indice de la zone de connexion en fonctin de la position
    '    '-----------------------------------------------------------------------------------------------------------------
    '    '   iTravee     [E] :   Indice de la travée
    '    '   xPos        [E] :   Position par rapport à l'appui gauche de la travée
    '    '-----------------------------------------------------------------------------------------------------------------

    '    '--( Déclaration

    '    Dim iZone As Integer = 0
    '    Dim i As Integer
    '    Dim lTrouve As Boolean
    '    Dim sCum As Decimal = 0

    '    '--( Traitement

    '    If Me.NombreZones(iTravee) > 1 Then
    '        i = -1
    '        lTrouve = False
    '        Do While ((Not lTrouve) And (i < Me.NombreZones(iTravee) - 1))
    '            i += 1
    '            lTrouve = IsSmallerOrEqual(xPos, sCum + Me.LongueurZone(iTravee, i))
    '            If Not lTrouve Then sCum += Me.LongueurZone(iTravee, i)
    '        Loop
    '        iZone = i
    '    End If

    '    Return iZone

    'End Function

    Private Function EntraxeLongiGoujons(iTravee As Integer, iZone As Integer) As Decimal
        '--------------------------------------------------------------------------------------------------------
        '   03/02/24 :  Création - POM - V1.00
        '--------------------------------------------------------------------------------------------------------
        '   Retourne l'entraxe longi entre rangée de connecteurs
        '--------------------------------------------------------------------------------------------------------
        '   iTravee     [E] :   Indice de la travée
        '   iZone       [E] :   Indice de la zone de connexion
        '--------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim lRib As Boolean     ' Espacement multiple de l'entraxe des nervures
        Dim Entraxe As Decimal

        '--( Initialisation

        lRib = (Me.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte) _
            And (Me.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire) _
            And (Me.Dalle.Bac.AppuiT <> cls_Bac.EnuConfigTAppui.Discontinu)

        '--( Calcul

        If lRib Then
            Entraxe = Me.Espacement_Bac_TransZone(iTravee, iZone) * Me.Dalle.Bac.Ep
        Else
            Entraxe = Me.EspacementZone(iTravee, iZone)
        End If

        Return Entraxe

    End Function

#End Region

#Region " Attributs pour les chargements et les combinaisons "

    '--Cas de charge définis par l'utilisateur

    Public ChargesU As New Dictionary(Of String, cls_ChargementUtilisateur)

    '--Combinaisons définies par l'utilisateur

    Public Const nbCombELU As Integer = 6                           ' Nombre de combinaisons ELU
    Public Const nbCombELS As Integer = 6                           ' Nombre de combinaisons ELS
    Public Const nbCombFeu As Integer = 5                           ' Nombre de combinaisons ELU incendie
    Public Const nbCombELUConstruction As Integer = 2
    Public Const nbCombELSConstruction As Integer = 2
    Public lCombELU(nbCombELU - 1) As Boolean                        'Indique si combinaison ELU sélectionnée
    Public lCombELS(nbCombELS - 1) As Boolean                        'Indique si combinaison ELS sélectionnée
    Public lCombFeu(nbCombFeu - 1) As Boolean                        'Indique si combinaison Feu sélectionnée
    Public lCombELCURules(nbCombELUConstruction - 1) As Boolean      'Indique si combinaison réglementaire ELU Phase de construction
    Public lCombELCSRules(nbCombELSConstruction - 1) As Boolean      'Indique si combinaison réglementaire ELU Phase de construction

    Public CoefCombELU(nbCombELU - 1) As List(Of Decimal)            'Table des coefficients des combinaisons ELU
    Public CoefCombELS(nbCombELS - 1) As List(Of Decimal)            'Table des coefficients des combinaisons ELS
    Public CoefCombFeu(nbCombFeu - 1) As List(Of Decimal)            'Table des coefficients des combinaisons Feu
    Public CoefCombELCU(nbCombELUConstruction - 1) As List(Of Decimal)  'Table des coefficients des combinaisons ELU Construction
    Public CoefCombELCS(nbCombELSConstruction - 1) As List(Of Decimal)  'Table des coefficients des combinaisons ELS Construction
    '                                                               ' Indices pour les combinaisons utilisateurs, dans toutes les tables :
    '                                                               ' 0 = G ; 1 = Q1 ; 2 = Q2 ; 3 = QC ; 4 : g pour la construction

    Public CombiA_ELU As New cls_Combinaisons                       'Combinaisons ELU pour l'analyse
    Public CombiA_ELS As New cls_Combinaisons                       'Combinaisons ELS pour l'analyse
    Public CombiA_ELF As New cls_Combinaisons                       'Combinaisons ELF pour l'analyse
    Public CombiA_ELCU As New cls_Combinaisons                       'Combinaisons ELF pour l'analyse
    Public CombiA_ELCS As New cls_Combinaisons                       'Combinaisons ELF pour l'analyse

    '--Cas de charge pour l'analyse

    Public ChargesA As New List(Of cls_CasDeCharge)

    Public Const symbG1PP As String = "G1pp"
    Const symbG1C As String = "G1c"
    Const symbG1 As String = "G1"
    Const symbG2 As String = "G2"
    Const symbQ1 As String = "Q1"
    Const symbQ2 As String = "Q2"
    Const symbQC As String = "QC"
    'Const symbQ1D1 As String = "Q1#1"

    Dim lMultiQ(2) As Boolean                                       ' Indique si les chargements Q1, Q2 et QC sont appliqués sur plusieurs travées ou non
    Dim indiceCasRetrait As Integer

    '--Points de calcul des contraintes normales
    Public PtsSigma As New cls_PointsSigma

#End Region

#Region " Attibuts pour les analyses "

    Public Modal As New cls_AnalyseModale                           ' Analyse modale

    Public Analyse As cls_AnalyseEFinis                             ' Analyse par éléments finis

    Public Hivoss As cls_MethodHivoss         ' Coefficients pour le calcul dynamique définits dans la Frm_Hivoss

#End Region

#Region " Attributs pour les vérifications "

    Public VerifAcier() As cls_VerificationsAcier                   ' Classe pour la vérification des poutres acier (ou phase de construction)
    Public VerifMixte() As cls_VerificationsMixtes                  ' Classe pour la vérification des poutres mixtes (phase finale)

#End Region

#Region " Propriétés "

    ''' <summary>
    ''' Renvoi la masse totale de la poutre en cours 
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MasseTotalePoutre As Decimal
        Get
            Dim mass As Decimal

            mass = Me.Section.MassLineiqueProfilA * Me.LongueurTotale

            Return mass
        End Get
    End Property

    ''' <summary>
    ''' Renvoi la masse totale de la poutre en cours 
    ''' </summary>
    ''' <returns></returns>
    Public Function SurfacePeintureTotalePoutre(lAvecFaceSup As Boolean) As Decimal
        Dim surface As Decimal

        surface = Me.Section.ProfilA.PerimetreSection(lAvecFaceSup) * Me.LongueurTotale

        Return surface
    End Function

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

    Public lPoutreModifiee As Boolean

#End Region

#Region " Variables pour la modélisation "

    Public Nodes As strucBeamNodes
    Public Elements As New List(Of strucBeamElements)

    Public Structure strucBeamNodes
        Dim nbNodes As Integer              ' Nombre de noeuds de discrétisation le long de la poutre
        Dim xGlobal() As Decimal            ' Position x globale du noeud / extrémité gauche de la poutre
        Dim xTravee() As Decimal            ' Position x dans la travée locale / extremité gauche de la travée 
        '                                     pour un noeud sur 2 travées, x de la travée à gauche
        Dim iNodeExtTrav(,) As Integer      ' Table des indices des noeuds au droit des extremités de console (indice 1: indice travée, indice 2 : 0 ou 1 pour extrémité)
        Dim iNodeAppui() As Integer         ' Table des indices des noeuds appui global
        Dim NbAppuis As Integer             ' Nombre de noeuds sur appui
        Dim NbEtais As Integer              ' Nombre de noeuds sur appui temporaire d'étais
        Dim iNodeEtais() As Integer         ' Table des noeuds pour les appuis d'étais en phase de construction
    End Structure

    Public Structure strucBeamElements
        Dim lMixte As Boolean               ' Indique si propriétés mixtes ou acier
        Dim InertieY() As Decimal           ' Table des inerties des sections
        Dim Aire() As Decimal               ' Table des aires des sections
        Dim zANE() As Decimal               ' Table des positions des axe neutres élastiques (pour le calcul des contraintes)
        Dim nEqDalle As Decimal             ' si mixte, coefficient d'équivalence acier-béton pour la dalle
        Dim nEqEnrob As Decimal             ' si mixte, coefficient d'équivalence acier-béton pour l'enrobage partiel
        Dim lShadow As Boolean              ' Indique si table pour un cas de charge shadow
    End Structure

#End Region

#Region " CONSTRUCTEURS "

    Private Sub InitialiseChargements()
        '-------------------------------------------------------------------------------------------------------
        '   00/10/23 :  Création - POM
        '-------------------------------------------------------------------------------------------------------
        '   Initialisation des cas de charges utilisateur
        '-------------------------------------------------------------------------------------------------------
        '   MsgChargements  [E] :   Nom des cas de charges dans la langue utilisateur
        '-------------------------------------------------------------------------------------------------------

        Me.ChargesU.Add("G1", New cls_ChargementUtilisateur(NomChargements(0), Me.IndiceTraveeConsoleDroite))
        Me.ChargesU.Add("G2", New cls_ChargementUtilisateur(NomChargements(1), Me.IndiceTraveeConsoleDroite))
        Me.ChargesU.Add("Q1", New cls_ChargementUtilisateur(NomChargements(2) & " 1", Me.IndiceTraveeConsoleDroite))
        Me.ChargesU.Add("Q2", New cls_ChargementUtilisateur(NomChargements(2) & " 2", Me.IndiceTraveeConsoleDroite))
        Me.ChargesU.Add("QC", New cls_ChargementUtilisateur(NomChargements(3), Me.IndiceTraveeConsoleDroite))

    End Sub

    Private Sub InitialiseTablesCombi()
        Dim nbCharges As Integer = 5
        Dim i, j As Integer
        For i = 0 To nbCombELU - 1
            Me.CoefCombELU(i) = New List(Of Decimal)
            For j = 1 To nbCharges
                Me.CoefCombELU(i).Add(0)
            Next
            Me.lCombELU(i) = False
        Next
        Me.lCombELU(0) = True
        For i = 0 To nbCombELS - 1
            Me.CoefCombELS(i) = New List(Of Decimal)
            For j = 1 To nbCharges
                Me.CoefCombELS(i).Add(0)
            Next
            Me.lCombELS(i) = False
        Next
        Me.lCombELS(0) = True
        For i = 0 To nbCombFeu - 1
            Me.CoefCombFeu(i) = New List(Of Decimal)
            For j = 1 To nbCharges
                Me.CoefCombFeu(i).Add(0)
            Next
            Me.lCombFeu(i) = False
        Next
        For i = 0 To nbCombELUConstruction - 1
            Me.CoefCombELCU(i) = New List(Of Decimal)
            For j = 1 To nbCharges
                Me.CoefCombELCU(i).Add(0)
            Next
            Me.lCombELCURules(i) = False
        Next
        If Me.lMixte Then lCombELCURules(0) = True
        For i = 0 To nbCombELSConstruction - 1
            Me.CoefCombELCS(i) = New List(Of Decimal)
            For j = 1 To nbCharges
                Me.CoefCombELCS(i).Add(0)
            Next
            Me.lCombELCSRules(i) = False
        Next
        If Me.lMixte Then lCombELCSRules(0) = True
    End Sub

    Public Sub New()

        Me.TypeSection = cls_Section.Enum_TypeSection.AcierSeul
        ParametresGenerauxDefaut()
        PoutreDefautAcier()
        InitialiseChargements()
        InitialiseTablesCombi()
        InitialisePoidsPropres()

    End Sub

    Public Sub New(MyTypeSection As cls_Section.Enum_TypeSection, NomPoutre As String)

        Me.TypeSection = MyTypeSection
        Me.BeamID = NomPoutre
        ParametresGenerauxDefaut()

        '--> Poutre par défaut

        Select Case MyTypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul
                PoutreDefautAcier()
            Case cls_Section.Enum_TypeSection.AcierSeulEnrobage
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
        InitialisePoidsPropres()

    End Sub

    Private Sub ParametresGenerauxDefaut()
        Me.lDefautPortee = True
        Me.lDefautEtaiement = True
        Me.lDefautEnrobage = True
        Me.lDefautDalle = True
        Me.lDonneesSauvees = False
        Me.NouvellePoutre = True
        Me.lPoutreModifiee = False

        '--> Initialisation Classes

        Me.Hivoss = New cls_MethodHivoss
        Me.MaintienBac = New cls_MaintienBac
    End Sub

    Private Sub PoutreDefautAcier()
        pNbTravees = 1
        ReDim LongueurTravee(IndiceTraveeConsoleDroite)
        ReDim TypTravee(IndiceTraveeConsoleDroite)
        ReDim Maintiens(IndiceTraveeConsoleDroite)
        ReDim NbMaintiens(IndiceTraveeConsoleDroite)
        'ReDim TypeMaintien(IndiceTraveeConsoleDroite + 2)


        TypeMaintien = EnuTypeMaintiensPoutre.NotRestrained

        For i As Integer = 0 To Maintiens.Length - 1
            Maintiens(i) = New List(Of cls_Maintiens)
        Next

        lTraveeConsoleDroite = False
        lTraveeConsoleGauche = False

        TypeEtaiement = EnuTypeEtaiement.UnPropped
        NbEtaiement = NBPROPPINGDEFAUT
        lEtaisConsoleGauche = False
        lEtaisConsoleDroite = False

        lEtaisSousProfileAcier = True

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

        ReDim LongueurZone(IndiceTraveeConsoleDroite, 2)
        ReDim NombreZones(IndiceTraveeConsoleDroite)
        ReDim EspacementZone(IndiceTraveeConsoleDroite, 2)
        ReDim Espacement_Bac_TransZone(IndiceTraveeConsoleDroite, 2)
        ReDim NombreGoujonsTransv(IndiceTraveeConsoleDroite, 2)
        'ReDim NombreGoujonsTot(IndiceTraveeConsoleDroite + 2)

        ReDim TauEd(IndiceTraveeConsoleDroite, 2, 2)
        ReDim Thetaf(IndiceTraveeConsoleDroite, 2, 2)
        ReDim Thetaf_min(IndiceTraveeConsoleDroite, 2)
        ReDim Gamma_sf(IndiceTraveeConsoleDroite, 2, 2)
        ReDim As_s_transv(IndiceTraveeConsoleDroite, 2, 2)

        For i As Integer = 0 To IndiceTraveeConsoleDroite
            LongueurZone(i, 0) = LongueurTravee(i)
            LongueurZone(i, 1) = 0
            LongueurZone(i, 2) = 0
            NombreZones(i) = 1
            EspacementZone(i, 0) = 200 / 1000
            EspacementZone(i, 1) = 200 / 1000
            EspacementZone(i, 2) = 200 / 1000
            Espacement_Bac_TransZone(i, 0) = 1
            Espacement_Bac_TransZone(i, 1) = 1
            Espacement_Bac_TransZone(i, 2) = 1
            NombreGoujonsTransv(i, 0) = 1
            NombreGoujonsTransv(i, 1) = 1
            NombreGoujonsTransv(i, 2) = 1
            'For j As Integer = 0 To 2
            'NombreGoujonsTot(i) += ZoneLongueur(i, j) / ZoneEspacement(i, j)
            'Next
        Next

        lAutomaticDesign = False

    End Sub

    Private Sub EnrobageDefaut()

    End Sub

    Private Sub DalleDefaut()

    End Sub

#End Region

#Region " Outils divers "

    Public ReadOnly Property lMaintienBacPossible As Boolean
        '---------------------------------------------------------------------------------------
        '   01/02/24 :  Création - V1.00 - POM
        '---------------------------------------------------------------------------------------
        '   Indique si le maitien par le bac est possible
        '---------------------------------------------------------------------------------------
        Get
            Dim lPossible As Boolean
            Dim plMixte, plEtaiement, plDalleMixte, plBacPerp As Boolean

            plMixte = Me.lMixte
            plEtaiement = (Me.TypeEtaiement = cls_Poutre.EnuTypeEtaiement.FullyPropped)
            plDalleMixte = (Me.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte)
            plBacPerp = (Me.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire)

            lPossible = plMixte And (Not plEtaiement) And plDalleMixte And plBacPerp
            Return lPossible
        End Get
    End Property


    Public ReadOnly Property EntraxeSolive As Decimal
        Get
            Dim MyD As Decimal

            If Me.lIntermediaire Then
                MyD = (Me.EntraxeD1 + Me.EntraxeD2) / 2
            Else
                MyD = Me.EntraxeD2
            End If

            Return MyD
        End Get
    End Property

    Public ReadOnly Property lMultiSpan As Boolean
        Get
            Return (Me.NbTravees > 1)
        End Get
    End Property

    Public ReadOnly Property PorteeDalle As Decimal
        Get
            Dim portee As Decimal

            If Me.lIntermediaire Then
                portee = (Me.EntraxeD1 + Me.EntraxeD2) / 2
            Else
                portee = Me.EntraxeD2
            End If
            Return portee
        End Get
    End Property

    Public Function GetNbCombi(lComb As Boolean()) As Integer
        Dim nbRetour As Integer

        For i As Integer = 0 To lComb.Count - 1
            If lComb(i) Then nbRetour += 1
        Next

        Return nbRetour
    End Function

    ''' <summary>
    ''' Renvoie le nombre total de maitiens latéraux sur la poutre
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property NombreTotalMaintiensLateraux As Integer
        Get
            Dim Nombre As Integer = 0
            Dim iTravee As Integer
            For iTravee = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
                Nombre += Me.Maintiens(iTravee).Count
            Next
            Return Nombre
        End Get
    End Property

    ''' <summary>
    ''' Largeur surlaquelle sont appliquée les charges surfaciques
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property LargeurInfluence
        Get
            Dim Largeur As Decimal = 0

            If Me.lIntermediaire Then
                Largeur = (Me.EntraxeD1 + Me.EntraxeD2) / 2
            Else
                Largeur = Me.EntraxeD1 + (Me.EntraxeD2) / 2
            End If

            Return Largeur
        End Get
    End Property

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
        Me.lPoutreModifiee = True

        Me.InitialisePoidsPropres()

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

    ''' <summary>
    ''' Routine qui permet de réinitialiser les dimensions des tableaux de la classe poutre lorsque le nombre de travée a été modifié 
    ''' (sera utile + tard quand on pourra modifier le nombre de travées sur 2 appuis)
    ''' </summary>
    Public Sub MAJI_Tableaux_NbTravee_Modifie()

        'MAJ de la partie concernant les dimensions de la poutre

        ReDim Preserve Me.TypTravee(IndiceTraveeConsoleDroite)
        For i As Integer = 0 To IndiceTraveeConsoleDroite
            Select Case i
                Case 0 : Me.TypTravee(i) = EnuTypeTravee.ConsoleGauche
                Case IndiceTraveeConsoleDroite : Me.TypTravee(i) = EnuTypeTravee.ConsoleDroite
                Case Else : Me.TypTravee(i) = EnuTypeTravee.DeuxAppuis
            End Select
        Next

        ReDim Preserve Me.LongueurTravee(IndiceTraveeConsoleDroite)
        For i As Integer = 0 To IndiceTraveeConsoleDroite
            If Me.LongueurTravee(i) = 0 Then 'Permet de savoir si la dimension i est remplie d'éléments nuls, auquel cas on initialise avec les paramètres par défaut
                Select Case Me.TypTravee(i)
                    Case EnuTypeTravee.ConsoleGauche, EnuTypeTravee.ConsoleDroite : LongueurTravee(i) = PORTEECONSOLEDEFAUT
                    Case EnuTypeTravee.DeuxAppuis : LongueurTravee(i) = PORTEEDEFAUT
                End Select
            End If
        Next

        'MAJ de la partie concernant la connection 

        ReDim Preserve LongueurZone(IndiceTraveeConsoleDroite, 2)
        ReDim Preserve NombreZones(IndiceTraveeConsoleDroite)
        ReDim Preserve EspacementZone(IndiceTraveeConsoleDroite, 2)
        ReDim Preserve Espacement_Bac_TransZone(IndiceTraveeConsoleDroite, 2)
        ReDim Preserve NombreGoujonsTransv(IndiceTraveeConsoleDroite, 2)

        For i As Integer = 0 To IndiceTraveeConsoleDroite
            If LongueurZone(i, 0) = 0 Then 'Permet de savoir si la dimension i est remplie d'éléments nuls, auquel cas on initialise avec les paramètres par défaut
                LongueurZone(i, 0) = LongueurTravee(i)
                LongueurZone(i, 1) = 0
                LongueurZone(i, 2) = 0
                NombreZones(i) = 1
                EspacementZone(i, 0) = 200 / 1000
                EspacementZone(i, 1) = 200 / 1000
                EspacementZone(i, 2) = 200 / 1000
                Espacement_Bac_TransZone(i, 0) = 1
                Espacement_Bac_TransZone(i, 1) = 1
                Espacement_Bac_TransZone(i, 2) = 1
                NombreGoujonsTransv(i, 0) = 1
                NombreGoujonsTransv(i, 1) = 1
                NombreGoujonsTransv(i, 2) = 1
            End If
        Next

        'MAJ de la partie concernant les maintiens latéraux
        ReDim Preserve Me.NbMaintiens(IndiceTraveeConsoleDroite)
        ReDim Preserve Me.Maintiens(IndiceTraveeConsoleDroite)

        For i As Integer = 0 To IndiceTraveeConsoleDroite
            If IsNothing(Me.Maintiens(i)) Then
                Me.Maintiens(i) = New List(Of cls_Maintiens)
            End If
        Next




        'MAJ de la partie concernant le chargement 

        For Each element As KeyValuePair(Of String, cls_ChargementUtilisateur) In Me.ChargesU


            ReDim Preserve Me.ChargesU(element.Key).FReparties(IndiceTraveeConsoleDroite)
            For i As Integer = 0 To IndiceTraveeConsoleDroite
                If IsNothing(Me.ChargesU(element.Key).FReparties(i)) Then
                    Me.ChargesU(element.Key).FReparties(i) = New List(Of cls_ForceRepartie)
                End If
            Next

            ReDim Preserve Me.ChargesU(element.Key).Forces(IndiceTraveeConsoleDroite)
            For i As Integer = 0 To IndiceTraveeConsoleDroite
                If IsNothing(Me.ChargesU(element.Key).Forces(i)) Then
                    Me.ChargesU(element.Key).Forces(i) = New List(Of cls_Force)
                End If
            Next

            ReDim Preserve Me.ChargesU(element.Key).QSurf(IndiceDerniereTravee) 'pas besoin de faire appel au constructeur ici car c'est un tableau de décimal

        Next


    End Sub
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

        ReDim PoutreCible.NbMaintiens(PoutreSource.NbMaintiens.GetUpperBound(0))
        PoutreCible.NbMaintiens = PoutreSource.NbMaintiens.Clone

        'ReDim PoutreCible.TypeMaintien(PoutreSource.TypeMaintien.GetUpperBound(0))
        'PoutreCible.TypeMaintien = PoutreSource.TypeMaintien.Clone

        cls_Dalle.DeepClone(PoutreSource.Dalle, PoutreCible.Dalle)
        'PoutreCible.Dalle = PoutreSource.Dalle.Clone 'GUD -> J'ai enlevé cette ligne de code car déjà fait dans le DeepClone + elle annule les effets du DeepClone (introduisait un bug dans la Frm_Connection) 

        'Clone Section
        cls_Section.DeepClone(PoutreSource.Section, PoutreCible.Section)

        'Clone param calcul
        cls_OptionsCalcul.DeepClone(PoutreSource.Param, PoutreCible.Param)

        'Clone HIVOSS
        PoutreCible.Hivoss = PoutreSource.Hivoss.Clone()

        'Clone Maintien par le bac
        PoutreCible.MaintienBac = PoutreSource.MaintienBac.Clone()

        'Clone maintiens
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
        ReDim PoutreCible.LongueurZone(PoutreSource.LongueurZone.GetUpperBound(0), PoutreSource.LongueurZone.GetUpperBound(1))
        PoutreCible.LongueurZone = PoutreSource.LongueurZone.Clone

        ReDim PoutreCible.NombreZones(PoutreSource.NombreZones.GetUpperBound(0))
        PoutreCible.NombreZones = PoutreSource.NombreZones.Clone

        ReDim PoutreCible.EspacementZone(PoutreSource.EspacementZone.GetUpperBound(0), PoutreSource.EspacementZone.GetUpperBound(1))
        PoutreCible.EspacementZone = PoutreSource.EspacementZone.Clone

        ReDim PoutreCible.Espacement_Bac_TransZone(PoutreSource.Espacement_Bac_TransZone.GetUpperBound(0), PoutreSource.Espacement_Bac_TransZone.GetUpperBound(1))
        PoutreCible.Espacement_Bac_TransZone = PoutreSource.Espacement_Bac_TransZone.Clone

        ReDim PoutreCible.NombreGoujonsTransv(PoutreSource.NombreGoujonsTransv.GetUpperBound(0), PoutreSource.NombreGoujonsTransv.GetUpperBound(1))
        PoutreCible.NombreGoujonsTransv = PoutreSource.NombreGoujonsTransv.Clone

        'Clone ChargeUtilisateur
        PoutreCible.ChargesU = New Dictionary(Of String, cls_ChargementUtilisateur)
        For Each element As KeyValuePair(Of String, cls_ChargementUtilisateur) In PoutreSource.ChargesU
            Dim element_local As cls_ChargementUtilisateur
            element_local.DeepClone(element.Value, element_local)
            PoutreCible.ChargesU.Add(element.Key, element_local)
        Next

        'Clone combinaisons définies par l'utilisateur
        'cls_Combinaisons.DeepClone(PoutreSource.CombiA_ELU, PoutreCible.CombiA_ELU)
        'cls_Combinaisons.DeepClone(PoutreSource.CombiA_ELS, PoutreCible.CombiA_ELS)
        'cls_Combinaisons.DeepClone(PoutreSource.CombiA_ELF, PoutreCible.CombiA_ELF)
        'cls_Combinaisons.DeepClone(PoutreSource.CombiA_ELCU, PoutreCible.CombiA_ELCU)
        'cls_Combinaisons.DeepClone(PoutreSource.CombiA_ELCS, PoutreCible.CombiA_ELCS)

        PoutreCible.InitialisePoidsPropres() 'relance le calcul du poids propre une fois que toutes les données ont été clonées 

    End Sub


#End Region

#Region " Calculs largeur participante "

    Public Function BeffDalle(xPositionSection As Decimal, i_travee As Integer, lSimplifiedModel As Boolean, lAnalysisModel As Boolean, Optional TypeLargeur As EnuTypeLargeurParticipante = EnuTypeLargeurParticipante.LargeurTotale, Optional ByRef LargeursParticipantes(,) As Decimal = Nothing) As Decimal

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

            If LargeursParticipantes IsNot Nothing Then 'Enregistrement des données dans un tableau quand un tableau est passé en argument avant renvoi de beff
                '1ere colonne: Appui gauche A, 2eme colonne: mi travee, 3eme colonne: Appui droite B
                ReDim LargeursParticipantes(5, 0)

                LargeursParticipantes(0, 0) = be1_s_A
                LargeursParticipantes(1, 0) = be2_s_A
                LargeursParticipantes(2, 0) = beta_1_A
                LargeursParticipantes(3, 0) = beta_2_A
                LargeursParticipantes(4, 0) = Le_s_A
                LargeursParticipantes(5, 0) = beff_s_A

            End If

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

            If LargeursParticipantes IsNot Nothing Then 'Enregistrement des données dans un tableau quand un tableau est passé en argument avant renvoi de beff
                '1ere colonne: Appui gauche A, 2eme colonne: mi travee, 3eme colonne: Appui droite B
                ReDim LargeursParticipantes(5, 2)

                LargeursParticipantes(0, 2) = be1_s_B
                LargeursParticipantes(1, 2) = be2_s_B
                LargeursParticipantes(2, 2) = beta_1_B
                LargeursParticipantes(3, 2) = beta_2_B
                LargeursParticipantes(4, 2) = Le_s_B
                LargeursParticipantes(5, 2) = beff_s_B

            End If

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

                    Dim epsilon As Decimal = 10 ^ (-6) 'GuD: On définit une petite valeur pour pouvoir renvoyer la valeur la plus faible entre beff_s et beff_m lorsqu'on est proche de 0.15 ou 0.85

                    Select Case xPositionSection / LongueurTravee(i_travee)
                        Case <= 0.15 + epsilon
                            beff = beff_s_A
                        Case >= 0.85 - epsilon
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

            If LargeursParticipantes IsNot Nothing Then 'Enregistrement des données dans un tableau quand un tableau est passé en argument avant renvoi de beff
                '1ere colonne: Appui gauche A, 2eme colonne: mi travee, 3eme colonne: Appui droite B
                ReDim LargeursParticipantes(5, 2)

                LargeursParticipantes(0, 0) = be1_s_A
                LargeursParticipantes(1, 0) = be2_s_A
                LargeursParticipantes(2, 0) = beta_1_A
                LargeursParticipantes(3, 0) = beta_2_A
                LargeursParticipantes(4, 0) = Le_s_A
                LargeursParticipantes(5, 0) = beff_s_A

                LargeursParticipantes(0, 1) = be1_m
                LargeursParticipantes(1, 1) = be2_m
                LargeursParticipantes(2, 1) = 1
                LargeursParticipantes(3, 1) = 1
                LargeursParticipantes(4, 1) = Le_m
                LargeursParticipantes(5, 1) = beff_m

                LargeursParticipantes(0, 2) = be1_s_B
                LargeursParticipantes(1, 2) = be2_s_B
                LargeursParticipantes(2, 2) = beta_1_B
                LargeursParticipantes(3, 2) = beta_2_B
                LargeursParticipantes(4, 2) = Le_s_B
                LargeursParticipantes(5, 2) = beff_s_B

            End If

            Return beff

        End If

    End Function

    Public Sub MaillageBeff(lSimple As Boolean, lAnalyse As Boolean, ByRef Beff() As Decimal)
        '--------------------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Calcul des largeurs participantes de dalle le long de la poutre, au droit des noeuds
        '   Attention : il faut avoir initialisé les noeuds auparavant
        '--------------------------------------------------------------------------------------------
        '   lSimple     [E] :   Indique si modèle simplifié
        '   lAnalyse    [E] :   Indique si modèle pour analyse ou pour vérifications
        '   Beff        [S] :   Largeurs participantes de dalle (0 to NbNodes-1)
        '--------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode As Integer
        Dim iTravee As Integer
        Dim iTravD, iTravF As Integer
        Dim iNodD, iNodF As Integer
        Dim xPos, xExtG As Decimal

        '--> Initialisation

        ReDim Beff(Me.Nodes.nbNodes)

        iTravD = Me.IndicePremiereTravee
        iTravF = Me.IndiceDerniereTravee

        '--> Calculs en travée

        For iTravee = iTravD To iTravF

            iNodD = Me.Nodes.iNodeExtTrav(iTravee, 0)
            iNodF = Me.Nodes.iNodeExtTrav(iTravee, 1)
            xExtG = Me.xPositionAppui(True, iTravee)

            For iNode = iNodD + 1 To iNodF - 1
                xPos = Me.Nodes.xGlobal(iNode) - xExtG
                Beff(iNode) = Me.BeffDalle(xPos, iTravee, lSimple, lAnalyse)
            Next

        Next

        '--> Calcul aux extrémités

        Beff(0) = Me.BeffDalle(0, iTravD, lSimple, lAnalyse)
        Beff(Me.Nodes.nbNodes - 1) = Me.BeffDalle(LongueurTravee(iTravF), iTravF, lSimple, lAnalyse)

        '--> Calcul sur les appuis intermédiaires

        For iTravee = iTravD To iTravF - 1
            iNode = Me.Nodes.iNodeExtTrav(iTravee, 1)
            Beff(iNode) = Math.Min(Me.BeffDalle(LongueurTravee(iTravee), iTravee, lSimple, lAnalyse),
                                   Me.BeffDalle(0, iTravee + 1, lSimple, lAnalyse))
        Next

    End Sub

#End Region

#Region " Préparation des sections de calcul de la poutre et outils sur les noeuds "

    Public Sub PrepareNodesN()
        '-------------------------------------------------------------------------------------------
        '   03/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Préparation des sections de calcul de la poutre
        '-------------------------------------------------------------------------------------------

        Me.PrepareNodesN(Me.Param.dMaxNodes, Me.Param.nbMinNodesTravee, Me.Param.nbMinNodesConsole)

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
        Dim x0 As Decimal

        '--> Initialisation des noeuds imposés

        InitialiseNoeudsImposes(xImp)
        iTravee = Me.IndicePremiereTravee
        xCum = Me.xPositionAppui(False, iTravee)
        ReDim Nodes.iNodeExtTrav(Me.IndiceDerniereTravee, 1)
        i0 = 0
        Nodes.nbNodes = 0
        Nodes.NbAppuis = Me.NombreTraveesDeuxAppuis + 1
        ReDim Nodes.iNodeAppui(Nodes.NbAppuis - 1)

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

            '# Si on est sur un tronçon d'extremite, on initialise la table des extrémités
            If lAppG Then Nodes.iNodeExtTrav(iTravee, 0) = Nodes.nbNodes - nDec - 1
            If lAppD Then Nodes.iNodeExtTrav(iTravee, 1) = Nodes.nbNodes - 1

            '# Position des noeuds dans le tronçon
            For j As Integer = i0 To nDec
                Me.Nodes.xTravee(iGauche + j) = DeltaX * j + xo - Me.xPositionAppui(True, iTravee)
                Me.Nodes.xGlobal(iGauche + j) = xo + DeltaX * j
            Next

            lFirst = False
            i0 = 1
        Next

        '--> Initialisation des noeuds sur appui

        Nodes.iNodeAppui(0) = Nodes.iNodeExtTrav(1, 0)

        For i As Integer = 1 To Me.NombreTraveesDeuxAppuis
            Nodes.iNodeAppui(i) = Nodes.iNodeExtTrav(i, 1)
        Next

        '--> Initialisation des noeuds appuis temporaire d'étais

        Nodes.NbEtais = 0
        If Me.lMixte And (Me.TypeEtaiement = EnuTypeEtaiement.PointPropped) Then
            If Me.lEtaisConsoleGauche Then Nodes.NbEtais += 1
            If Me.lEtaisConsoleDroite Then Nodes.NbEtais += 1

            Nodes.NbEtais += Me.NbEtaiement

            If Me.NbEtaiement > 0 Then
                ReDim Nodes.iNodeEtais(Me.NbEtaiement - 1)
                i0 = 0
                If Me.lEtaisConsoleGauche Then
                    Nodes.iNodeEtais(0) = 0
                    i0 = 1
                End If
                x0 = Me.xPositionAppui(True, 1)
                DeltaX = Me.LongueurTravee(1) / (1 + Me.NbEtaiement)
                For i As Integer = 1 To Me.NbEtaiement
                    Nodes.iNodeEtais(i0 + i - 1) = IndiceNodeFromXpos(x0 + i * DeltaX)
                Next
                If Me.lEtaisConsoleDroite Then
                    Nodes.iNodeEtais(Me.NbEtaiement - 1) = Me.Nodes.nbNodes - 1
                End If
            End If
        End If
    End Sub

    Private Sub InitialiseNoeudsImposes(ByRef xImp As List(Of Decimal))
        '-------------------------------------------------------------------------------------------
        '   17/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Ajout d'un noeud dans la liste des noeuds imposés
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim DeltaX, x0 As Decimal

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

        If Me.lMixte And (Me.TypeEtaiement = EnuTypeEtaiement.PointPropped) Then
            DeltaX = Me.LongueurTravee(1) / (Me.NbEtaiement + 1)
            x0 = Me.xPositionAppui(True, 1)
            For i As Integer = 1 To Me.NbEtaiement
                AjouteNoeudImpose(x0 + i * DeltaX, xImp)
            Next
        End If

        '# Maitiens latéraux

        For iTravee = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
            x0 = Me.xPositionAppui(True, iTravee)
            For jm = 0 To Me.NbMaintiens(iTravee) - 1

                If Me.Maintiens(iTravee)(jm).EstEfficace Then
                    AjouteNoeudImpose(x0 + Me.Maintiens(iTravee)(jm).x_Loc, xImp)
                End If

            Next
        Next

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

    Private Function IndiceNodeFromXpos(xPos As Decimal, Optional DeltaX As Decimal = 0.001) As Integer
        '-------------------------------------------------------------------------------------------
        '   20/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Retourne l'indice d'un noeud à partir de sa position
        '-------------------------------------------------------------------------------------------
        '   xPos        [E] :   Position du noeud
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lTrouve As Boolean = False
        Dim iNode As Integer = -1
        Dim myInd As Integer = -1

        '--> Recherche

        Do While (Not lTrouve) And (iNode < Me.Nodes.nbNodes - 1)
            iNode += 1
            lTrouve = IsEqual(Nodes.xGlobal(iNode), xPos, DeltaX)
        Loop
        If lTrouve Then myInd = iNode
        Return myInd
    End Function


    Public Function GetIndiceNoeudFromXglobal(XPos As Decimal) As Integer
        '------------------------------------------------------------------------------------------------------------------
        '   07/12/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Retourne l'indice d'un noeud à partir d'une position X globale
        '------------------------------------------------------------------------------------------------------------------
        '   xPos    [E] :   Position X dans le repère global
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lTrouve As Boolean = False
        Dim iNode As Integer = -1
        Const EPSILONX As Decimal = 0.001

        '--> Recherche du noeud

        Do While (Not lTrouve) And (iNode < Me.Nodes.nbNodes - 1)
            iNode += 1
            lTrouve = IsEqual(XPos, Me.Nodes.xGlobal(iNode), EPSILONX)
        Loop

        If Not lTrouve Then iNode = -1

        Return iNode
    End Function
#End Region

#Region "Calcul des armatures transversales"

    ''' <summary>
    ''' Calcul la contrainte tangentielle induite par les connecteurs 
    ''' </summary>
    Sub CalculArmaturesTransversales()
        '------------------------------------------------------------------------------------------------------------------
        '    17/11/23 : Création - GUD
        '------------------------------------------------------------------------------------------------------------------
        '   Calcul la contrainte tangentielle induite par les connecteurs
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclaration
        Dim nr As Integer
        Dim PRd As Decimal
        Dim sx As Decimal
        Dim lGeneration1 As Boolean
        Dim lDallePleine As Boolean
        Dim lPerp As Boolean
        Dim Ecm, Fck, Fcd, nu As Decimal
        Dim fypd, fsd As Decimal
        Dim gammaVs, gammaVc As Decimal
        Dim v_x_Ed As Decimal
        Dim k_sf_aa_sA, k_sf_bb_sA, k_sf_dd_sA As Decimal   'Definition des coefficients lorsque l'on se trouve au droit de l'appui A (voir Figure 5.1 de l'EC4 et §5.1 des specifications techniques)
        Dim k_sf_aa_m, k_sf_bb_m, k_sf_dd_m As Decimal      'Definition des coefficients lorsque l'on se trouve à mi-travee (voir Figure 5.1 de l'EC4 et §5.1 des specifications techniques)
        Dim k_sf_aa_sB, k_sf_bb_sB, k_sf_dd_sB As Decimal   'Definition des coefficients lorsque l'on se trouve au droit de l'appui B (voir Figure 5.1 de l'EC4 et §5.1 des specifications techniques)
        Dim hf_aa, hf_bb, hf_dd As Decimal
        Dim b0, b0min As Decimal
        Dim LargeurParticipante(0, 0) As Decimal
        Dim be1, be2, beta1, beta2, bem, bes As Decimal
        Dim k_bacPE1 As Decimal 'coefficient qui indique la présence du bac acier (=1) ou non (=0)
        Dim xDebutZoneLoc, xFinZoneLoc As Decimal(,)
        Dim lSupportA, lSupportB, lMiTravee As Boolean 'sera utile pour + tard, permet de savoir si la zone de connection etudiee empiete sur la zone de support A, B ou mi-travee (selon la Figure 5.1 de l'EC4)
        Dim thetaf_min_pos, thetaf_min_neg, thetaf_max As Decimal 'angle min de la bielle de compression en fonction de si on se trouve en zone de flexion positive ou négative 

        '--> Initialisation
        lGeneration1 = Me.Param.lGeneration1
        lDallePleine = (Me.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine) Or (Me.Dalle.type = cls_Dalle.Enum_TypeDalle.Prefabriquee)
        lPerp = (Me.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire)
        Ecm = Me.Dalle.beton.Ecm
        Fck = Me.Dalle.beton.Fck
        Fcd = Me.Dalle.beton.Fck / Me.Param.Gamma.GammaC
        fypd = Me.Dalle.Bac.fyp / Me.Param.Gamma.GammaP
        fsd = Me.Dalle.AcierArmatures.FsK / Me.Param.Gamma.GammaS
        If Me.Param.lGeneration1 Then
            nu = 0.6 * (1 - Fck / 250)
        Else
            nu = 0.5
        End If
        gammaVs = Me.Param.Gamma.GammaVs
        gammaVc = Me.Param.Gamma.GammaVc
        LargeurParticipante(0, 0) = 0 'initialisation avec une valeur quelconque pour pas que le tableau soit considéré comme Nothing dans la fonction BeffDalle

        thetaf_min_pos = 27 / 180 * Math.PI 'angle min de la bielle en zone de flexion positive
        thetaf_min_neg = 36 / 180 * Math.PI 'angle min  de la bielle en zone de flexion négative
        thetaf_max = Math.PI / 4

        If Me.Dalle.lMixte Then
            b0min = 4 * Me.Dalle.Connecteur.d
        Else
            b0min = 2.5 * Me.Dalle.Connecteur.d
        End If

        'Stockage local des abscisses afin de ne pas faire tourner plusieurs fois les calculs des proprietes Me.xDebutZone et Me.xFinZone
        xDebutZoneLoc = Me.xDebutZone
        xFinZoneLoc = Me.xFinZone

        For i_travee As Integer = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
            For j_zone As Integer = 0 To Me.NombreZones(i_travee) - 1

                '---
                'CALCUL DE LA CONTRAINTE TANGENTIELLE
                '---

                nr = Me.NombreGoujonsTransv(i_travee, j_zone)
                PRd = Me.Dalle.Connecteur.ResistancePRd(lGeneration1, lDallePleine, lPerp, Me.Dalle.Bac, nr, Fck, Ecm, gammaVs, gammaVc)
                sx = Me.EspacementZone(i_travee, j_zone)
                v_x_Ed = nr * PRd / sx

                b0 = (nr - 1) * b0min

                Me.BeffDalle(Me.LongueurTravee(i_travee) / 2, i_travee, False, False, EnuTypeLargeurParticipante.LargeurTotale, LargeurParticipante)

                'Calcul de hf qui correspond à la longueur developpe de la surface de ruine 
                hf_aa = Me.Dalle.EpaisseurActive
                If nr = 1 Then
                    hf_bb = 2 * Me.Dalle.Connecteur.hsc + Me.Dalle.Connecteur.d 'GUD: a confirmer avec les corrections apportées dans le MT 
                Else
                    hf_bb = 2 * Me.Dalle.Connecteur.hsc + b0
                End If
                hf_dd = b0 + 2 * (Me.Section.ProfilA.Bfs - b0 + Me.Dalle.Connecteur.hsc * Math.Tan(Me.Dalle.ThetaRd)) / Math.Sqrt(1 + Math.Tan(Me.Dalle.ThetaRd) ^ 2)

                Select Case i_travee
                    Case 0 'on est dans le cas de la console gauche

                        'Les consoles sont forcement en flexion négative, on calcul uniquement le coefficient au droit de l'appui A
                        be1 = LargeurParticipante(0, 0)
                        be2 = LargeurParticipante(1, 0)
                        beta1 = LargeurParticipante(2, 0)
                        beta2 = LargeurParticipante(3, 0)
                        bes = LargeurParticipante(5, 0)

                        k_sf_aa_sA = Math.Max((beta1 * be1 - b0 / 2) / bes, (beta2 * be2 - b0 / 2) / bes)
                        k_sf_bb_sA = 1
                        k_sf_dd_sA = 1

                        Me.TauEd(i_travee, j_zone, 0) = k_sf_aa_sA * v_x_Ed / hf_aa
                        Me.TauEd(i_travee, j_zone, 1) = k_sf_bb_sA * v_x_Ed / hf_bb
                        Me.TauEd(i_travee, j_zone, 2) = k_sf_dd_sA * v_x_Ed / hf_dd

                        Me.Thetaf_min(i_travee, j_zone) = thetaf_min_neg

                    Case Me.IndiceTraveeConsoleDroite 'on est dans le cas de la console droite 

                        'Les consoles sont forcement en flexion négative, on calcul uniquement le coefficient au droit de l'appui B
                        be1 = LargeurParticipante(0, 2)
                        be2 = LargeurParticipante(1, 2)
                        beta1 = LargeurParticipante(2, 2)
                        beta2 = LargeurParticipante(3, 2)
                        bes = LargeurParticipante(5, 2)

                        k_sf_aa_sB = Math.Max((beta1 * be1 - b0 / 2) / bes, (beta2 * be2 - b0 / 2) / bes)
                        k_sf_bb_sB = 1
                        k_sf_dd_sB = 1

                        Me.TauEd(i_travee, j_zone, 0) = k_sf_aa_sB * v_x_Ed / hf_aa
                        Me.TauEd(i_travee, j_zone, 1) = k_sf_bb_sB * v_x_Ed / hf_bb
                        Me.TauEd(i_travee, j_zone, 2) = k_sf_dd_sB * v_x_Ed / hf_dd

                        Me.Thetaf_min(i_travee, j_zone) = thetaf_min_neg

                    Case Else 'on est dans le cas d'une travée centrale

                        'Calcul des coefficients k_sf au droit de l'appui A
                        be1 = LargeurParticipante(0, 0)
                        be2 = LargeurParticipante(1, 0)
                        beta1 = LargeurParticipante(2, 0)
                        beta2 = LargeurParticipante(3, 0)
                        bes = LargeurParticipante(5, 0)

                        k_sf_aa_sA = Math.Max((beta1 * be1 - b0 / 2) / bes, (beta2 * be2 - b0 / 2) / bes)
                        k_sf_bb_sA = 1
                        k_sf_dd_sA = 1

                        'Calcul des coefficients k_sf a mi travee
                        be1 = LargeurParticipante(0, 1)
                        be2 = LargeurParticipante(1, 1)
                        bem = LargeurParticipante(5, 1)

                        k_sf_aa_m = Math.Max((be1 - b0 / 2) / bem, (be2 - b0 / 2) / bem)
                        k_sf_bb_m = 1
                        k_sf_dd_m = 1

                        'Calcul des coefficients k_sf au droit de l'appui B
                        be1 = LargeurParticipante(0, 2)
                        be2 = LargeurParticipante(1, 2)
                        beta1 = LargeurParticipante(2, 2)
                        beta2 = LargeurParticipante(3, 2)
                        bes = LargeurParticipante(5, 2)

                        k_sf_aa_sB = Math.Max((beta1 * be1 - b0 / 2) / bes, (beta2 * be2 - b0 / 2) / bes)
                        k_sf_bb_sB = 1
                        k_sf_dd_sB = 1

                        If Me.lTraveeConsoleGauche Then
                            If Me.lTraveeConsoleDroite Then 'Presence de console a gauche ET a droite
                                Select Case Me.xDebutZone(i_travee, j_zone)
                                    Case <= Me.LongueurTravee(i_travee) / 4 'la zone étudiée commence avant L/4
                                        Select Case Me.xFinZone(i_travee, j_zone)
                                            Case <= Me.LongueurTravee(i_travee) / 4 'la zone etudiée commence et finie avant L/4
                                                lSupportA = True
                                                lMiTravee = False
                                                lSupportB = False
                                            Case <= 3 * Me.LongueurTravee(i_travee) / 4 'la zone étudiée commence avant L/4 et finie entre L/4 et 3L/4
                                                lSupportA = True
                                                lMiTravee = True
                                                lSupportB = False
                                            Case >= 3 * Me.LongueurTravee(i_travee) / 4 'la eone étudiée commence avant L/4 et finie après 3L/4
                                                lSupportA = True
                                                lMiTravee = True
                                                lSupportB = True
                                        End Select
                                    Case <= 3 * Me.LongueurTravee(i_travee) / 4 'la zone étudiée commence après L/4 et avant 3L/4
                                        Select Case Me.xFinZone(i_travee, j_zone) 'le cas ou la zone finie avant L/4 n a pas de sens et n est pas étudiée 
                                            Case <= 3 * Me.LongueurTravee(i_travee) / 4 'la zone étudiée commence et finie entre L/4 et 3L/4 
                                                lSupportA = False
                                                lMiTravee = True
                                                lSupportB = False
                                            Case >= 3 * Me.LongueurTravee(i_travee) / 4 'la zone étudiée commence entre L/4 et 3L/4 et finie après 3L/4
                                                lSupportA = False
                                                lMiTravee = True
                                                lSupportB = True
                                        End Select
                                    Case >= 3 * Me.LongueurTravee(i_travee) / 4 'la zone finie nécessairement après 3L/4 donc pas besoin de boucle 
                                        lSupportA = False
                                        lMiTravee = False
                                        lSupportB = True
                                End Select

                            Else 'Presence de console a gauche uniquement
                                lSupportB = False 'il n'y a pas de console a droite, ce qui fait qu'il ne peut pas y avoir de zone de moment négatif proche de l appui de droite 

                                Select Case Me.xDebutZone(i_travee, j_zone)
                                    Case <= Me.LongueurTravee(i_travee) / 4 'la zone étudiée commence avant L/4
                                        Select Case Me.xFinZone(i_travee, j_zone)
                                            Case <= Me.LongueurTravee(i_travee) / 4 'la zone etudiée commence et finie avant L/4
                                                lSupportA = True
                                                lMiTravee = False
                                            Case >= Me.LongueurTravee(i_travee) / 4 'la zone étudiée commence avant L/4 et finie après L/4
                                                lSupportA = True
                                                lMiTravee = True
                                        End Select
                                    Case >= Me.LongueurTravee(i_travee) / 4 'la zone  commence et finie nécessairement après L/4 donc pas besoin de boucle 
                                        lSupportA = False
                                        lMiTravee = True
                                End Select
                            End If
                        Else
                            If Me.lTraveeConsoleDroite Then 'Presence de console a droite uniquement
                                lSupportA = False 'il n'y a pas de console a gauche, ce qui fait qu'il ne peut pas y avoir de zone de moment négatif proche de l appui de droite 

                                Select Case Me.xDebutZone(i_travee, j_zone)
                                    Case <= 3 * Me.LongueurTravee(i_travee) / 4
                                        Select Case Me.xFinZone(i_travee, j_zone)
                                            Case <= 3 * Me.LongueurTravee(i_travee) / 4 'la zone etudiée commence et finie avant 3L/4
                                                lMiTravee = True
                                                lSupportB = False
                                            Case >= 3 * Me.LongueurTravee(i_travee) / 4 'la zone étudiée commence avant 3L/4 et finie après 3L/4
                                                lMiTravee = True
                                                lSupportB = True
                                        End Select
                                    Case >= 3 * Me.LongueurTravee(i_travee) / 4 'la zone  commence et finie nécessairement après 3L/4 donc pas besoin de boucle 
                                        lMiTravee = False
                                        lSupportB = True
                                End Select
                            Else 'Aucune console, la zone étudiée se trouve nécessairement en zone de flexion positive 
                                lSupportA = False
                                lMiTravee = True
                                lSupportB = False
                            End If
                        End If


                        If lSupportA Then
                            Me.TauEd(i_travee, j_zone, 0) = k_sf_aa_sA * v_x_Ed / hf_aa
                            Me.TauEd(i_travee, j_zone, 1) = k_sf_bb_sA * v_x_Ed / hf_bb
                            Me.TauEd(i_travee, j_zone, 2) = k_sf_dd_sA * v_x_Ed / hf_dd
                        End If

                        If lMiTravee Then
                            Me.TauEd(i_travee, j_zone, 0) = Math.Max(Me.TauEd(i_travee, j_zone, 0), k_sf_aa_m * v_x_Ed / hf_aa)
                            Me.TauEd(i_travee, j_zone, 1) = Math.Max(Me.TauEd(i_travee, j_zone, 1), k_sf_bb_m * v_x_Ed / hf_bb)
                            Me.TauEd(i_travee, j_zone, 2) = Math.Max(Me.TauEd(i_travee, j_zone, 2), k_sf_dd_m * v_x_Ed / hf_dd)
                        End If

                        If lSupportB Then
                            Me.TauEd(i_travee, j_zone, 0) = Math.Max(Me.TauEd(i_travee, j_zone, 0), k_sf_aa_sB * v_x_Ed / hf_aa)
                            Me.TauEd(i_travee, j_zone, 1) = Math.Max(Me.TauEd(i_travee, j_zone, 1), k_sf_bb_sB * v_x_Ed / hf_bb)
                            Me.TauEd(i_travee, j_zone, 2) = Math.Max(Me.TauEd(i_travee, j_zone, 2), k_sf_dd_sB * v_x_Ed / hf_dd)
                        End If

                        If lSupportA Or lSupportB Then 'la zone de connection étudiée traverse au moins une zone de flexion négative
                            Me.Thetaf_min(i_travee, j_zone) = thetaf_min_neg
                        Else 'la zone de connection étudiée est entièrement en zone de flexion comprimée 
                            Me.Thetaf_min(i_travee, j_zone) = thetaf_min_pos
                        End If

                End Select

                '---
                'CALCUL DE L'ANGLE DE LA BIELLE DE COMPRESSION ET DE LA QUANTITE D'ARMATURE PAR UNITE DE LONGUEUR NECESSAIRE
                '---

                Dim TauEd_max As Decimal = nu * Fcd / 2

                For k_ruine As Integer = 0 To 2
                    'Conversion de Pa a MPa des contraintes tangentielles
                    Me.TauEd(i_travee, j_zone, k_ruine) /= kConvMPaPa

                    If TauEd(i_travee, j_zone, k_ruine) <= TauEd_max Then
                        Me.Thetaf(i_travee, j_zone, k_ruine) = 0.5 * Math.Asin(2 * TauEd(i_travee, j_zone, k_ruine) / (nu * Fcd))
                    Else 'la contrainte tangentielle est trop importante, la bielle de compression n'est pas vérifiée. On considère alors l'angle de la bielle max pour la suite du calcul 
                        Me.Thetaf(i_travee, j_zone, k_ruine) = thetaf_max
                    End If

                    Me.Thetaf(i_travee, j_zone, k_ruine) = Math.Min(Me.Thetaf(i_travee, j_zone, k_ruine), thetaf_max)
                    Me.Thetaf(i_travee, j_zone, k_ruine) = Math.Max(Me.Thetaf(i_travee, j_zone, k_ruine), Me.Thetaf_min(i_travee, j_zone))

                    'Calcul du critère de vérification de la bielle de compression
                    Me.Gamma_sf(i_travee, j_zone, k_ruine) = Me.TauEd(i_travee, j_zone, k_ruine) / (nu * Fcd * Math.Sin(Me.Thetaf(i_travee, j_zone, k_ruine)) * Math.Cos(Me.Thetaf(i_travee, j_zone, k_ruine)))

                Next

                If Me.Dalle.lMixte And Me.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire And Me.Dalle.Bac.AppuiT = cls_Bac.EnuConfigTAppui.NervureEtBacContinus Then
                    k_bacPE1 = 1
                Else
                    k_bacPE1 = 0
                End If

                Me.As_s_transv(i_travee, j_zone, 0) = Math.Max((TauEd(i_travee, j_zone, 0) * hf_aa * Math.Tan(Me.Thetaf(i_travee, j_zone, 0)) - k_bacPE1 * Me.Dalle.Bac.Ape * fypd) / fsd, 0)
                Me.As_s_transv(i_travee, j_zone, 1) = Math.Max((TauEd(i_travee, j_zone, 1) * hf_bb * Math.Tan(Me.Thetaf(i_travee, j_zone, 1)) - k_bacPE1 * Me.Dalle.Bac.Ape * fypd) / fsd, 0)
                Me.As_s_transv(i_travee, j_zone, 2) = Math.Max((TauEd(i_travee, j_zone, 2) * hf_dd * Math.Tan(Me.Thetaf(i_travee, j_zone, 2)) - k_bacPE1 * Me.Dalle.Bac.Ape * fypd) / fsd, 0)


            Next
        Next



    End Sub

#End Region

#Region " Maillage : propriétés des barres le long de la poutre "

    Public Sub ExtraireNeqEnrobage(ByRef Neq As List(Of Decimal))
        '-------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM - V1.00
        '-------------------------------------------------------------------------------------------
        '   Récupération de la liste des coefficients equivalence pour l'enrobage
        '-------------------------------------------------------------------------------------------
        '   Neq     [S] :   Liste des coefficients d'équivalence
        '-------------------------------------------------------------------------------------------

        Neq = New List(Of Decimal)

        For iTab As Integer = 0 To Me.Elements.Count - 1
            Neq.Add(Me.Elements(iTab).nEqEnrob)
        Next

    End Sub

    Public Sub ExtraireListeNeqDalleEnrobage(ByRef lDalle() As Boolean, ByRef NeqDalle() As Decimal, ByRef NeqEnrob() As Decimal)
        '-------------------------------------------------------------------------------------------
        '   14/12/23 :  Création - POM - V1.00
        '-------------------------------------------------------------------------------------------
        '   Récupération de la liste des coefficients equivalence et des états de la dalle pour les p mixtes
        '-------------------------------------------------------------------------------------------
        '   lDalle  [S] :   Indique si dalle active
        '   NeqDalle[S] :   Table des coef d'equivalence pour la dalle
        '   NeqEnrob[S] :   Table des coef d'equivalence pour l'enrobage (si présent)
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iTri() As Integer
        Dim iTab, j, k As Integer
        Dim lTrouve As Boolean
        Dim nbTab As Integer = 0

        '--> Initialisation

        '# Nombre de tables d'éléments (hors shadow)

        For iTab = 0 To Me.Elements.Count - 1
            If Not Me.Elements(iTab).lShadow Then nbTab += 1
        Next

        '# Dimensionnement des Tableaux

        ReDim lDalle(nbTab - 1)
        ReDim NeqDalle(nbTab - 1)
        ReDim NeqEnrob(nbTab - 1)
        ReDim iTri(nbTab - 1)

        '--> Tri

        iTri(0) = 0
        For iTab = 0 To nbTab - 1
            If Not Me.Elements(iTab).lMixte Then
                '-- Phase non mixte : on le place en premier
                For k = iTab To 1 Step -1
                    iTri(k) = iTri(k - 1)
                Next
                iTri(0) = iTab
            Else
                'on positionne en fonction de la valeur de neqdalle
                k = -1
                lTrouve = False
                Do While (Not lTrouve) And (k < iTab - 1)
                    k += 1
                    lTrouve = (Me.Elements(iTab).nEqDalle < Me.Elements(iTri(k)).nEqDalle)
                Loop
                If lTrouve Then
                    For j = iTab To k + 1 Step -1
                        iTri(j) = iTri(j - 1)
                    Next
                    iTri(k) = iTab
                Else
                    iTri(iTab) = iTab
                End If
            End If

        Next

        '--> Transfert

        For iTab = 0 To nbTab - 1
            lDalle(iTab) = Me.Elements(iTri(iTab)).lMixte
            NeqDalle(iTab) = Me.Elements(iTri(iTab)).nEqDalle
            NeqEnrob(iTab) = Me.Elements(iTri(iTab)).nEqEnrob
        Next


    End Sub

    Public Function IndiceTabElts(lMixte As Boolean, nEqDal As Decimal, nEqEc As Decimal, Optional lShadow As Boolean = False) As Integer
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
        Dim SigneM() As Decimal = Nothing

        '--> Initialisation

        Me.InitialiseSigneMoment(SigneM)

        '--> On commence par chercher si la table demandée existe

        Do While (Not lTrouve) And (iTab < Me.Elements.Count - 1)
            iTab += 1
            lTrouve = (lMixte = Me.Elements(iTab).lMixte) _
                  And (nEqDal = Me.Elements(iTab).nEqDalle) _
                  And (nEqEc = Me.Elements(iTab).nEqEnrob) _
                  And (lShadow = Me.Elements(iTab).lShadow)
        Loop

        If lTrouve Then
            indexT = iTab
        Else
            AjouteTabElements(lMixte, nEqDal, nEqEc, SigneM, lShadow)
            indexT = Me.Elements.Count - 1
        End If

        Return indexT
    End Function

    Private Sub AjouteTabElements(lMixte As Boolean, nEqDal As Decimal, nEqEc As Decimal, pSigneM() As Decimal, lShadow As Boolean)
        '-------------------------------------------------------------------------------------------
        '   07/09/23 :  Création - POM - V1.00
        '-------------------------------------------------------------------------------------------
        '   Crée une table des propriétés des élements à prendre en compte dans le calcul
        '-------------------------------------------------------------------------------------------
        '   lMixte          [E] :   Indique si propriétés en phase mixte ou non mixte (pour la dalle)
        '   nEqDal          [E] :   Si mixte, coefficient d'équivalence acier béton pour la dalle
        '   nEqEc           [E] :   Coefficient d'équivalence acier béton pour l'enrobage
        '   pSigneM         [E] :   Table de signes de moment le long de la poutre
        '   lShadow         [E] :   Indique une table d'élts pour cas de charge shadow
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyElts As strucBeamElements
        Dim lEnrob As Boolean = Me.lEnrobage
        Dim lPoutreMixte As Boolean = Me.lMixte

        '--> Initialisation

        MyElts.lMixte = lMixte
        MyElts.nEqDalle = nEqDal
        MyElts.nEqEnrob = nEqEc
        MyElts.lShadow = lShadow

        ReDim MyElts.Aire(Me.Nodes.nbNodes - 2)
        ReDim MyElts.InertieY(Me.Nodes.nbNodes - 2)
        ReDim MyElts.zANE(Me.Nodes.nbNodes - 2)

        '--> Cas très simple ou tout est constant

        If (Not lMixte) And (Not lEnrob) And (Not lShadow) Then

            Me.MaillageProprietesElementsAcierNonEnrob(MyElts)

        End If

        '--> Boucle sur les travées, dans le cas où il faut prendre en compte le béton

        If lShadow Then
            Me.MaillageProprietesElementsMixteShadow(lMixte, nEqDal, nEqEc, pSigneM, MyElts)
        Else
            Me.MaillageProprietesElementsMixteouEnrob(lMixte, nEqDal, nEqEc, pSigneM, MyElts)
        End If

        '--> Fin

        Me.Elements.Add(MyElts)
    End Sub

    Public Sub MaillageProprietesElements(lMixte As Boolean, nEqDal As Decimal, nEqEc As Decimal, pSigneM() As Decimal, ByRef pMyElts As strucBeamElements)
        '---------------------------------------------------------------------------------------------
        '   03/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------
        '   Calcul des propriétés des barres du maillage
        '---------------------------------------------------------------------------------------------
        '   lMixte          [E] :   Indique si propriétés en phase mixte ou non mixte (pour la dalle)
        '   nEqDal          [E] :   Si mixte, coefficient d'équivalence acier béton pour la dalle
        '   nEqEc           [E] :   Coefficient d'équivalence acier béton pour l'enrobage
        '   pSigneM         [E] :   Table de signes de moment le long de la poutre
        '   pMyElts         [S] :   Propriétés des éléments
        '---------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lEnrob As Boolean = Me.lEnrobage

        '--> Initialisation

        pMyElts.lMixte = lMixte
        pMyElts.nEqDalle = nEqDal
        pMyElts.nEqEnrob = nEqEc

        ReDim pMyElts.Aire(Me.Nodes.nbNodes - 2)
        ReDim pMyElts.InertieY(Me.Nodes.nbNodes - 2)
        ReDim pMyElts.zANE(Me.Nodes.nbNodes - 2)

        '--> Traitement selon le type de section

        If (Not lMixte) And (Not lEnrob) Then

            '# Cas d'une poutre non mixte et sans enrobage

            Me.MaillageProprietesElementsAcierNonEnrob(pMyElts)

        Else
            '# Poutre mixte ou avec enrobage

            Me.MaillageProprietesElementsMixteouEnrob(lMixte, nEqDal, nEqEc, pSigneM, pMyElts)

        End If

    End Sub

    Private Sub MaillageProprietesElementsMixteShadow(lMixte As Boolean, nEqDal As Decimal, nEqEc As Decimal, pSigneM() As Decimal, ByRef pMyElts As strucBeamElements)
        '---------------------------------------------------------------------------------------------
        '   03/02/24 :  Création - POM
        '---------------------------------------------------------------------------------------------
        '   Calcul des propriétés des barres du maillage pour une poutre mixte
        '   en prenant en compte la rigidité de la connexion
        '   Calcul de l'inertie selon l'équation prEN 1994-1-1 9.3.1 (5)
        '---------------------------------------------------------------------------------------------
        '   lMixte          [E] :   Indique si propriétés en phase mixte ou non mixte (pour la dalle)
        '   nEqDal          [E] :   Si mixte, coefficient d'équivalence acier béton pour la dalle
        '   nEqEc           [E] :   Coefficient d'équivalence acier béton pour l'enrobage
        '   pSigneM         [E] :   Table de signes de moment le long de la poutre
        '   pMyElts         [S] :   Propriétés des éléments
        '---------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iEltO, iEltE As Integer
        Dim Beff, xm As Decimal
        Dim BeffPrec As Decimal
        Dim SigneMprec As Decimal
        Dim lCalcul As Boolean
        Dim Aire, InertieY, zANe As Decimal
        Dim Le() As Decimal
        Dim cStiff, cStiffPrec As Decimal
        Dim iTraveeDeb, iTraveeFin As Integer
        Dim iTravee As Integer
        Dim kLe As Decimal
        Dim IndZoneConnex() As Integer
        Dim indZonePrec As Integer = -1
        Dim nR, sX As Decimal
        Dim kSc, PRd As Decimal
        Dim DeltaD As Decimal
        Dim lGeneration1 As Boolean = Me.Param.lGeneration1
        Dim lDallePleine, lPerp As Boolean
        Dim Ecm, Fck As Decimal
        Dim gammaVs, gammaVc As Decimal

        '--( Initialisation

        iTraveeDeb = Me.IndicePremiereTravee
        iTraveeFin = Me.IndiceDerniereTravee
        '# Longueur entre points de moments nuls dans les travees centrales
        ReDim Le(iTraveeFin)
        For iTravee = iTraveeDeb To iTraveeFin
            If iTravee > 0 Then
                If Not (iTravee = iTraveeFin And Me.lTraveeConsoleDroite) Then
                    kLe = 1
                    If iTravee > iTraveeDeb Then kLe -= 0.15
                    If iTravee < iTraveeFin Then kLe -= 0.15

                    Le(iTravee) = Me.LongueurTravee(iTravee) * kLe
                End If
            End If
        Next
        '# Zone de connexion
        ReDim IndZoneConnex(Me.Nodes.iNodeExtTrav(iTraveeFin, 1) - 1)

        lDallePleine = (Me.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine) Or (Me.Dalle.type = cls_Dalle.Enum_TypeDalle.Prefabriquee)
        lPerp = (Me.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire) And (Me.Dalle.Bac.AppuiT <> cls_Bac.EnuConfigTAppui.Discontinu)
        Ecm = Me.Dalle.beton.Ecm
        Fck = Me.Dalle.beton.Fck
        gammaVs = 1
        gammaVc = 1

        '--> Boucle sur les travées, dans le cas où il faut prendre en compte le béton

        For iTravee = iTraveeDeb To iTraveeFin
            iEltO = Me.Nodes.iNodeExtTrav(iTravee, 0)
            iEltE = Me.Nodes.iNodeExtTrav(iTravee, 1) - 1

            For iElt = iEltO To iEltE
                '# Position moyenne de l'élément par rpt  l'appui gauche de la travée
                xm = (Me.Nodes.xTravee(iElt) + Me.Nodes.xTravee(iElt + 1)) / 2
                '# Largeur efficace de dalle
                Beff = Me.BeffDalle(xm, iTravee, False, True)
                '# Zone de connexion 
                IndZoneConnex(iElt) = Me.IndiceZoneFromPosition(iTravee, xm)

                If iElt = iEltO Then
                    lCalcul = True
                Else
                    'lCalcul = Not ((SigneMprec = pSigneM(iElt)) And (BeffPrec = Beff) And IsEqual(cStiff, cStiffPrec))
                    lCalcul = Not ((SigneMprec = pSigneM(iElt)) And (BeffPrec = Beff) And (IndZoneConnex(iElt) = indZonePrec))
                End If

                If lCalcul Then
                    If pSigneM(iElt) < 0 Then
                        InertieY = Me.Section.InertieYY(pSigneM(iElt), False, Me.Param.Gamma, nEqEc, lMixte, nEqDal, Beff, Me.Dalle, zANe)
                    Else
                        '# Raideur de la connexion
                        DeltaD = Me.Param.DeltaD

                        nR = Me.NombreGoujonsTransv(iTravee, IndZoneConnex(iElt))
                        sX = Me.EntraxeLongiGoujons(iTravee, IndZoneConnex(iElt))
                        PRd = Me.Dalle.Connecteur.ResistancePRd(lGeneration1, lDallePleine, lPerp, Me.Dalle.Bac, nR, Fck, Ecm, gammaVs, gammaVc)
                        kSc = 0.7 * PRd / DeltaD

                        cStiff = nR * kSc / sX

                        '# Calcul
                        InertieY = Me.Section.InertieYYMixteSlip(1, False, Me.Param.Gamma, nEqEc, nEqDal, Beff, Me.Dalle, Le(iTravee), cls_Acier.EYACIER, cStiff, zANe)
                    End If

                    Aire = Me.Section.ProfilA.Aire      ' A changer pour aire homgonénéisée

                    pMyElts.InertieY(iElt) = InertieY
                    BeffPrec = Beff
                    SigneMprec = pSigneM(iElt)
                    cStiffPrec = cStiff
                    indZonePrec = IndZoneConnex(iElt)
                End If
                pMyElts.InertieY(iElt) = InertieY
                pMyElts.Aire(iElt) = Aire

            Next

        Next

    End Sub

    Private Sub MaillageProprietesElementsMixteouEnrob(lMixte As Boolean, nEqDal As Decimal, nEqEc As Decimal, pSigneM() As Decimal, ByRef pMyElts As strucBeamElements)
        '---------------------------------------------------------------------------------------------
        '   03/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------
        '   Calcul des propriétés des barres du maillage pour une poutre mixte avec ou sans enrobage
        '---------------------------------------------------------------------------------------------
        '   lMixte          [E] :   Indique si propriétés en phase mixte ou non mixte (pour la dalle)
        '   nEqDal          [E] :   Si mixte, coefficient d'équivalence acier béton pour la dalle
        '   nEqEc           [E] :   Coefficient d'équivalence acier béton pour l'enrobage
        '   pSigneM         [E] :   Table de signes de moment le long de la poutre
        '   pMyElts         [S] :   Propriétés des éléments
        '---------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iEltO, iEltE As Integer
        Dim Beff, xm As Decimal
        Dim BeffPrec As Decimal
        Dim SigneMprec As Decimal
        Dim lCalcul As Boolean
        Dim Aire, InertieY, zANe As Decimal

        '--> Boucle sur les travées, dans le cas où il faut prendre en compte le béton

        For iTravee As Integer = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
            iEltO = Me.Nodes.iNodeExtTrav(iTravee, 0)
            iEltE = Me.Nodes.iNodeExtTrav(iTravee, 1) - 1

            For iElt = iEltO To iEltE
                xm = (Me.Nodes.xTravee(iElt) + Me.Nodes.xTravee(iElt + 1)) / 2
                Beff = Me.BeffDalle(xm, iTravee, False, True)

                If iElt = iEltO Then
                    lCalcul = True
                Else
                    lCalcul = Not ((SigneMprec = pSigneM(iElt)) And (BeffPrec = Beff))
                End If

                If lCalcul Then
                    InertieY = Me.Section.InertieYY(pSigneM(iElt), False, Me.Param.Gamma, nEqEc, lMixte, nEqDal, Beff, Me.Dalle, zANe)
                    Aire = Me.Section.ProfilA.Aire      ' A changer pour aire homgonénéisée

                    pMyElts.InertieY(iElt) = InertieY
                    BeffPrec = Beff
                    SigneMprec = pSigneM(iElt)
                End If
                pMyElts.InertieY(iElt) = InertieY
                pMyElts.Aire(iElt) = Aire

            Next

        Next

    End Sub

    Private Sub MaillageProprietesElementsAcierNonEnrob(ByRef pMyElts As strucBeamElements)
        '---------------------------------------------------------------------------------------------
        '   03/11/23 :  Création - POM
        '---------------------------------------------------------------------------------------------
        '   Calcul des propriétés des barres du maillage pour une poutre acier sans enrobage
        '   (Propiétés constantes le long de la barre)
        '---------------------------------------------------------------------------------------------
        '   pMyElts     [S] :   Propriétés des éléments
        '---------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Aire, InertieY As Decimal
        Dim zANE, MelRd, zANP, MplRd As Decimal
        Dim iElt As Integer

        '--> Calculs des propriétés

        Me.Section.ProfilA.ProprietesMyy(1, False, 1, zANE, InertieY, MelRd, zANP, MplRd)
        Aire = Me.Section.ProfilA.Aire

        '--> Attribution à tous les éléments 

        For iElt = 0 To Nodes.nbNodes - 2
            pMyElts.Aire(iElt) = Aire
            pMyElts.InertieY(iElt) = InertieY
            pMyElts.zANE(iElt) = zANE
        Next

    End Sub


    Public Sub InitialiseSigneMoment(ByRef pSigneM() As Decimal)
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
            iEltO = Me.Nodes.iNodeExtTrav(iTravee, 0)
            iEltE = Me.Nodes.iNodeExtTrav(iTravee, 1) - 1

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
                    xm = (Me.Nodes.xGlobal(iElt) + Me.Nodes.xGlobal(iElt + 1)) / 2
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

    Public Sub MaillagePropElastiquesNonMixtes(Signe As Decimal, lValRd As Boolean, ByRef InertieY(,) As Decimal, ByRef zANE(,) As Decimal)

    End Sub

    Public Sub MaillagePropElastiquesMixtes(Beff() As Decimal, Signe As Decimal, lValRd As Boolean, nEqEnrob As Decimal, nEqDalle As Decimal,
                                            ByRef InertieY(,) As Decimal, ByRef zANE(,) As Decimal, Optional lDalle As Boolean = True)
        '------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '------------------------------------------------------------------------------
        '   Calcul des moments plastiques le long de la poutre (sur les noeuds du modèle)
        '------------------------------------------------------------------------------
        '   Beff        [E] :   Largeur participante de dalle
        '   Signe       [E] :   Signe du moment à considérer
        '   lValRd      [E] :   Indique si valeurs de calcul
        '   MplRd       [S] :   Table des moments plastiques au droit des noeuds du modèle
        '   zANP        [S] :   Table des position des ANP
        '   lDalle      [E] :   Indique si on prend en compte la dalle, pour les poutres mixtes (cela permet le calcul en acier seul)
        '------------------------------------------------------------------------------

        '--> Déclaration

        Dim iNode As Integer
        Dim k, kDeb, kFin As Integer
        Dim BeffPrec As Decimal = -1
        Dim pInertieY, p_zANE As Decimal
        Dim pMelRd As Decimal
        Dim lNonMixte As Boolean = (Me.Section.typeSection = cls_Section.Enum_TypeSection.AcierSeul) Or (Not lDalle)

        '--> Initialisation

        ReDim InertieY(Me.Nodes.nbNodes - 1, 1)
        ReDim zANE(Me.Nodes.nbNodes - 1, 1)
        If lNonMixte Then
            Me.Section.ProprietesElastiquesAcierMyy(lValRd, Me.Param.Gamma, p_zANE, pInertieY, pMelRd)
        End If

        '--> Boucle sur les noeuds

        For iNode = 0 To Me.Nodes.nbNodes - 1
            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = Me.Nodes.nbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kDeb To kFin

                If lNonMixte Then
                    '== PROPRIETES SANS PRISE EN COMPTE DE LA DALLE 
                    ' qui est forcément constante le long de la poutre
                    InertieY(iNode, k) = pInertieY
                    zANE(iNode, k) = p_zANE
                Else
                    '== PROPRIETES AVEC PRISE EN COMPTE DE LA DALLE
                    If IsEqual(Beff(iNode), BeffPrec) Then
                        InertieY(iNode, k) = pInertieY
                        zANE(iNode, k) = p_zANE
                    Else

                        Me.Section.ProprietesElastiquesMixteMyy(Signe, lValRd, Me.Param.Gamma, nEqEnrob, nEqDalle, Beff(iNode), Me.Dalle, p_zANE, pInertieY, pMelRd, lDalle)

                        InertieY(iNode, k) = pInertieY
                        zANE(iNode, k) = p_zANE
                    End If
                End If

            Next
        Next
    End Sub

    Public Sub MaillagePropPlastiquesMixtes(Beff() As Decimal, Signe As Decimal, lValRd As Boolean, ByRef MplRd() As Decimal, ByRef zANP() As Decimal)
        '------------------------------------------------------------------------------
        '   05/10/23 :  Création - POM
        '------------------------------------------------------------------------------
        '   Calcul des moments plastiques le long de la poutre (sur les noeuds du modèle)
        '------------------------------------------------------------------------------
        '   Beff        [E] :   Largeur participante de dalle
        '   Signe       [E] :   Signe du moment à considérer
        '   lValRd      [E] :   Indique si valeurs de calcul
        '   MplRd       [S] :   Table des moments plastiques au droit des noeuds du modèle
        '   zANP        [S] :   Table des position des ANP
        '------------------------------------------------------------------------------

        '--> Déclaration

        Dim BeffPrec As Decimal = -1
        Dim iNode As Integer
        Dim Eta As Decimal = 1      '#ALERTE : à adapter sur chaque section

        '--> Initialisation

        ReDim MplRd(Me.Nodes.nbNodes - 1)
        ReDim zANP(Me.Nodes.nbNodes - 1)

        '--> Boucle sur les noeuds

        For iNode = 0 To Me.Nodes.nbNodes - 1

            If IsEqual(Beff(iNode), BeffPrec) Then
                MplRd(iNode) = MplRd(iNode - 1)
                zANP(iNode) = zANP(iNode - 1)
            Else
                Me.Section.ProprietesPlastiquesMixteMyy(Signe, lValRd, Me.Param.Gamma, 0, Beff(iNode), Me.Dalle, zANP(iNode), MplRd(iNode))
                BeffPrec = Beff(iNode)
            End If

        Next

    End Sub

    Public Sub ProprietesVerifAcier(lValRd As Boolean, ByRef MplRd As Decimal, ByRef zANP As Decimal, ByRef MelRd As Decimal, ByRef zANE As Decimal)
        '------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '------------------------------------------------------------------------------
        '   Calcul des propriétés élastiques et élastiques des sections le long d'une poutre acier,
        '   Pour les vérifications
        '   Pour une poutre acier, les propriétés de section sont constantes le long d'une poutre
        '------------------------------------------------------------------------------
        '   lValRd      [E] :   Indique si valeurs de calcul
        '   MplRd       [S] :   Moment plastique
        '   zANP        [S] :   Position des ANP
        '   MelRd       [S] :   Moment élastique
        '   zANE        [S] :   Position des ANE
        '------------------------------------------------------------------------------

        '--> Déclaration

        Dim InertieY As Decimal

        '--> Propriétés

        '# Elastiques

        Me.Section.ProprietesElastiquesAcierMyy(lValRd, Me.Param.Gamma, zANE, InertieY, MelRd)

        '# Plastiques

        Me.Section.ProprietesPlastiquesMyy(1, lValRd, Me.Param.Gamma, 0, zANP, MplRd)

    End Sub


#End Region

#Region " Combinaisons "

    'Public Sub InitialiseCombiELUPreDefU()
    '    '------------------------------------------------------------------------------
    '    '   27/09/23 :  Création - POM
    '    '------------------------------------------------------------------------------
    '    '   Initialisation des combinaisons Prédéfines
    '    '------------------------------------------------------------------------------
    '    '------------------------------------------------------------------------------

    '    '--> Déclarations

    '    Dim i As Integer

    '    '--> Combinaisons

    '    '# ELU 01

    '    i = 0
    '    Me.CoefCombELU(i)(0) = Me.Param.Gamma.GammaG_sup
    '    Me.CoefCombELU(i)(1) = Me.Param.Gamma.GammaQ
    '    Me.CoefCombELU(i)(2) = Me.Param.Gamma.GammaQ * Me.Param.Gamma.Psi0_Q2
    '    Me.CoefCombELU(i)(3) = 0

    '    i = 1
    '    Me.CoefCombELU(i)(0) = Me.Param.Gamma.GammaG_sup
    '    Me.CoefCombELU(i)(1) = Me.Param.Gamma.GammaQ * Me.Param.Gamma.Psi0_Q1
    '    Me.CoefCombELU(i)(2) = Me.Param.Gamma.GammaQ
    '    Me.CoefCombELU(i)(3) = 0

    '    i = 2
    '    Me.CoefCombELU(i)(0) = Me.Param.Gamma.GammaG_inf
    '    Me.CoefCombELU(i)(1) = Me.Param.Gamma.GammaQ
    '    Me.CoefCombELU(i)(2) = Me.Param.Gamma.GammaQ * Me.Param.Gamma.Psi0_Q2
    '    Me.CoefCombELU(i)(3) = 0

    '    i = 3
    '    Me.CoefCombELU(i)(0) = Me.Param.Gamma.GammaG_inf
    '    Me.CoefCombELU(i)(1) = Me.Param.Gamma.GammaQ * Me.Param.Gamma.Psi0_Q1
    '    Me.CoefCombELU(i)(2) = Me.Param.Gamma.GammaQ
    '    Me.CoefCombELU(i)(3) = 0


    'End Sub

    'Public Sub InitialiseCombiA_ELU()
    '    '---------------------------------------------------------------------------
    '    '   27/09/23 :  Création - POM 
    '    '---------------------------------------------------------------------------
    '    '   Préparation des tables de coef de combinaisons ELU
    '    '---------------------------------------------------------------------------

    '    If Me.ChargesA.Count = 0 Then Exit Sub

    '    '--> Déclarations

    '    Dim iCombi, i, j As Integer
    '    Dim TabCoef() As Decimal
    '    Dim NbCombQ As Integer = 1
    '    Dim IndiceQ(1) As Integer
    '    Dim iMatriceQ(,) = {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}}
    '    Dim SymboleQ() As String = {symbQ1, symbQ2}
    '    ' Dim SymboleQDiez(1) As String
    '    Dim lChargeNonNulle() As Boolean

    '    Dim iTravP As Integer = IndicePremiereTravee
    '    Dim iTravd As Integer = IndiceDerniereTravee
    '    Dim NbCharges As Integer = Me.ChargesA.Count
    '    Dim Symbole As String
    '    Dim SymbolExt() As String = {"#1", "#2", "#3"}

    '    '--> Initialisation

    '    Me.CombiA_ELU.nbCombi = 0
    '    ReDim TabCoef(NbCharges - 1)
    '    ReDim lChargeNonNulle(NbCharges - 1)
    '    If lMultiQ(1) Or lMultiQ(0) Then NbCombQ = 3
    '    For i = 0 To 1
    '        If lMultiQ(i) Then
    '            IndiceQ(i) = IndiceCasParSymbole(SymboleQ(i) & "#1")
    '        Else
    '            IndiceQ(i) = IndiceCasParSymbole(SymboleQ(i))
    '        End If
    '    Next
    '    If Not (lMultiQ(0) Or lMultiQ(1)) Then SymbolExt(0) = ""

    '    For i = 0 To NbCharges - 1
    '        If ChargesA(i).Type <> cls_CasDeCharge.EnuType.Retrait Then
    '            lChargeNonNulle(i) = Me.ChargesA(i).EstNonNul(iTravP, iTravd)
    '        Else
    '            lChargeNonNulle(i) = True
    '        End If
    '    Next

    '    '--> Boucle sur les combinaisons définies par l'utilisateur

    '    For iCombi = 0 To cls_Poutre.nbCombELU

    '        If lCombELU(iCombi) And (Not lCombiELUNulle(iCombi)) Then

    '            Symbole = "ELU_0" & CStr(iCombi + 1)

    '            For i = 0 To NbCharges - 1
    '                Select Case Me.ChargesA(i).Type
    '                    Case cls_CasDeCharge.EnuType.Permanente
    '                        TabCoef(i) = Me.CoefCombELU(iCombi)(0)
    '                    Case cls_CasDeCharge.EnuType.Retrait
    '                        TabCoef(i) = Me.CoefCombELU(iCombi)(0)
    '                    Case cls_CasDeCharge.EnuType.Construction
    '                        TabCoef(i) = 0
    '                End Select
    '            Next

    '            For i = 0 To NbCombQ - 1

    '                For j = 0 To 1

    '                    If lMultiQ(j) Then
    '                        For k = 0 To 2
    '                            TabCoef(IndiceQ(j) + k) = Me.CoefCombELU(iCombi)(j) * iMatriceQ(i, k)
    '                        Next
    '                    Else
    '                        TabCoef(IndiceQ(j)) = Me.CoefCombELU(iCombi)(j + 1)
    '                    End If

    '                Next

    '                Me.CombiA_ELU.AjouteCombi(Symbole & SymbolExt(i), TabCoef, NbCharges)

    '            Next
    '        End If

    '    Next

    'End Sub


    Public Sub InitialiseCombiA(nbCombi As Integer, lCombi() As Boolean, CoefCombi() As List(Of Decimal),
                                RacSymbolEL As String, ByRef MyCombi As cls_Combinaisons)
        '---------------------------------------------------------------------------
        '   27/09/23 :  Création - POM 
        '---------------------------------------------------------------------------
        '   Préparation des tables de coef de combinaisons 
        '---------------------------------------------------------------------------
        '   nbCombi     [E] :   Nombre de combinaisons utilisateurs
        '   lCombi      [E] :   Table indiquant si la combi utilisateur est prise en compte
        '   CoefCombi   [E] :   Table des coefficients de combinaison utilisateur
        '   RacSymbEL   [E] :   Racine pour le symbole de l'état limite
        '   MyCombi     [S] :   Combinaisons pour l'analyse
        '---------------------------------------------------------------------------

        If Me.ChargesA.Count = 0 Then Exit Sub

        '--> Déclarations

        Dim iCombi, i, j As Integer
        Dim TabCoef() As Decimal
        Dim NbCombQ As Integer = 1
        Dim NbCombQConstruct As Integer = 1
        Dim IndiceQ(2) As Integer
        Dim iMatriceQ(,) = {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}}
        Dim SymboleQ() As String = {symbQ1, symbQ2, symbQC}
        ' Dim SymboleQDiez(1) As String
        Dim lChargeNonNulle() As Boolean

        Dim iTravP As Integer = IndicePremiereTravee
        Dim iTravd As Integer = IndiceDerniereTravee
        Dim NbCharges As Integer = Me.ChargesA.Count
        Dim Symbole As String
        Dim SymbolExt() As String = {"#1", "#2", "#3"}
        Dim SymbolExtConstruct() As String = {"#1", "#2", "#3"}

        '--> Initialisation

        MyCombi.nbCombi = 0
        ReDim TabCoef(NbCharges - 1)
        ReDim lChargeNonNulle(NbCharges - 1)

        If (MyCombi.Equals(CombiA_ELCU) Or MyCombi.Equals(CombiA_ELCS)) Then
            NbCombQ = 0
            If lMultiQ(2) Then NbCombQConstruct = 3
        Else
            NbCombQConstruct = 0
            If lMultiQ(1) Or lMultiQ(0) Then NbCombQ = 3
        End If

        For i = 0 To IndiceQ.Length - 1
            If lMultiQ(i) Then
                IndiceQ(i) = IndiceCasParSymbole(SymboleQ(i) & "#1")
            Else
                IndiceQ(i) = IndiceCasParSymbole(SymboleQ(i))
            End If
        Next
        If Not (lMultiQ(0) Or lMultiQ(1)) Then SymbolExt(0) = ""
        If Not lMultiQ(2) Then SymbolExtConstruct(0) = ""

        For i = 0 To NbCharges - 1
            If ChargesA(i).Type <> cls_CasDeCharge.EnuType.Retrait Then
                lChargeNonNulle(i) = Me.ChargesA(i).EstNonNul(iTravP, iTravd)
            Else
                lChargeNonNulle(i) = True
            End If
        Next

        '--> Boucle sur les combinaisons définies par l'utilisateur

        For iCombi = 0 To nbCombi - 1

            If lCombi(iCombi) And (Not lCombinaisonNulle(iCombi, CoefCombi)) Then

                Symbole = RacSymbolEL & "_0" & CStr(iCombi + 1)

                For i = 0 To NbCharges - 1
                    Select Case Me.ChargesA(i).Type
                        Case cls_CasDeCharge.EnuType.Permanente
                            If (MyCombi.Equals(CombiA_ELCU) Or MyCombi.Equals(CombiA_ELCS)) Then
                                If Me.ChargesA(i).Symbol = "G1" Then TabCoef(i) = CoefCombi(iCombi)(4)
                            Else
                                TabCoef(i) = CoefCombi(iCombi)(0)
                            End If
                        Case cls_CasDeCharge.EnuType.Retrait
                            TabCoef(i) = CoefCombi(iCombi)(0)
                            'Case cls_CasDeCharge.EnuType.Construction
                            '    TabCoef(i) = CoefCombi(iCombi)(4)
                    End Select
                Next

                For i = 0 To NbCombQ - 1

                    For j = 0 To 1

                        If lMultiQ(j) Then
                            For k = 0 To 2
                                TabCoef(IndiceQ(j) + k) = CoefCombi(iCombi)(j + 1) * iMatriceQ(i, k)
                            Next
                        Else
                            TabCoef(IndiceQ(j)) = CoefCombi(iCombi)(j + 1)
                        End If

                    Next

                    MyCombi.AjouteCombi(Symbole & SymbolExt(i), TabCoef, NbCharges)

                Next

                For i = 0 To NbCombQConstruct - 1

                    If lMultiQ(2) Then
                        For k = 0 To 2
                            TabCoef(IndiceQ(2) + k) = CoefCombi(iCombi)(3) * iMatriceQ(i, k)
                        Next
                    Else
                        TabCoef(IndiceQ(2)) = CoefCombi(iCombi)(3)
                    End If


                    MyCombi.AjouteCombi(Symbole & SymbolExtConstruct(i), TabCoef, NbCharges)

                Next
            End If

        Next

    End Sub

    Private Function lCombinaisonNulle(iCombi As Integer, CoefCombi() As List(Of Decimal)) As Boolean
        '---------------------------------------------------------------------------
        '   27/09/23 :  Création - POM 
        '---------------------------------------------------------------------------
        '   Indique si une combinaison ELU définie par l'utilisateur a tous ses coef nuls
        '---------------------------------------------------------------------------
        '   iCombi      [E] :   Indice de la combinaison
        '   CoefCombi   [E] :   Table des coefficients de combinaison
        '---------------------------------------------------------------------------

        Dim lNul As Boolean = True

        For i As Integer = 0 To 3
            If Not IsEqual(CoefCombi(iCombi)(i), 0) Then lNul = False
        Next

        Return lNul

    End Function

    Private Function lCombiELUNulle(iCombi As Integer) As Boolean
        '---------------------------------------------------------------------------
        '   27/09/23 :  Création - POM 
        '---------------------------------------------------------------------------
        '   Indique si une combinaison ELU définie par l'utilisateur a tous ses coef nuls
        '---------------------------------------------------------------------------

        Dim lNul As Boolean = True

        For i As Integer = 0 To 3
            If Not IsEqual(Me.CoefCombELU(iCombi)(i), 0) Then lNul = False
        Next

        Return lNul

    End Function

#End Region

#Region " Préparation des cas de charge "
    Private Function IndiceCasG1C() As Integer
        '-------------------------------------------------------------------------------------------
        '   20/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Renvoie l'indice du cas de charge G1C
        '-------------------------------------------------------------------------------------------
        '-------------------------------------------------------------------------------------------

        Return IndiceCasParSymbole(symbG1C)

    End Function

    Public Function IndiceCasG1PP() As Integer
        '-------------------------------------------------------------------------------------------
        '   20/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Renvoie l'indice du cas de charge G1PP
        '-------------------------------------------------------------------------------------------
        '-------------------------------------------------------------------------------------------

        Return IndiceCasParSymbole(symbG1PP)

    End Function

    Private Function IndiceCasParSymbole(symbCas As String) As Integer
        '-------------------------------------------------------------------------------------------
        '   20/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Renvoie l'indice du cas de charge par son symbole
        '-------------------------------------------------------------------------------------------
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lTrouve As Boolean = False
        Dim iCas As Integer = -1

        '--> Recherche

        Do While (Not lTrouve) And iCas < Me.ChargesA.Count - 1
            iCas += 1
            lTrouve = Me.ChargesA(iCas).Symbol = symbCas
        Loop

        Return iCas
    End Function

    Private Sub InitialiseChargeEtais(ByRef MyCas As cls_CasDeCharge, Reactions() As Decimal)
        '-------------------------------------------------------------------------------------------
        '   20/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Initialisation du cas de charges réaction des étais
        '   A partir des réactions issues du calcul de G1PP
        '-------------------------------------------------------------------------------------------
        '   Reactions       [E] :   Réactions issues de G1PP
        '   MyCas           [S] :   Cas de charge
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iLast As Integer = Me.IndiceDerniereTravee
        Dim i0 As Integer = 1
        Const RSigne As Decimal = 1
        Dim iTrav As Integer
        Dim xPos, xApp As Decimal

        '--> Traitement des consoles

        If Me.lEtaisConsoleGauche Then
            MyCas.Forces(0).Add(New cls_Force(0, RSigne * Reactions(0), 0))
            i0 += 1
        End If
        If Me.lEtaisConsoleGauche Then
            MyCas.Forces(iLast).Add(New cls_Force(Me.LongueurTravee(iLast), RSigne * Reactions(Reactions.GetUpperBound(0)), Me.xPositionAppui(True, iLast)))
        End If

        '--> Traitement de étais de la travée centrale

        iTrav = 1
        For i As Integer = 0 To Me.NbEtaiement - 1
            xApp = Me.xPositionAppui(True, iTrav)
            xPos = Me.Nodes.xGlobal(Me.Nodes.iNodeEtais(i0 + i - 1)) - xApp
            MyCas.Forces(iLast).Add(New cls_Force(xPos, RSigne * Reactions(i0 + i), xApp))
        Next

    End Sub

    Private Sub InitialiseChargesPP(ByRef MyCas As cls_CasDeCharge)
        '-------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Préparation du cas de charge "Poids propre" pour toutes les poutres
        '-------------------------------------------------------------------------------------------
        '   MyCas       [S] :   Cas de charge
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        'Dim qPP As Decimal = Me.ChargeRepartiePP().qPP_Total * 5 'GuD: J'ai corrigé pour pouvoir compiler mais je ne sais pas pourquoi on multiplie par 5 ici
        Dim G_PP As StructPoidsPropres = Me.ChargeRepartiePP()

        '--> Préparation du cas de charge

        For iTrav As Integer = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
            If MyCas.FReparties(iTrav).Count = 0 Then
                MyCas.FReparties(iTrav).Add(New cls_ForceRepartie(0, G_PP.qPP_Total, Me.LongueurTravee(iTrav), G_PP.qPP_Total, Me.xPositionAppui(True, iTrav)))
            Else
                MyCas.FReparties(iTrav)(0) = New cls_ForceRepartie(0, G_PP.qPP_Total, Me.LongueurTravee(iTrav), G_PP.qPP_Total, Me.xPositionAppui(True, iTrav))
            End If
        Next

    End Sub

    Private Sub InitialiseRetraitEnrobage(ByRef MyCas As cls_CasDeCharge)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Préparation du cas de charge "Retrait" de l'enrobage pour les poutres partiellement enrobées
        '-------------------------------------------------------------------------------------------
        '   MyCas       [S] :   Cas de charge
        '-------------------------------------------------------------------------------------------

        '/!\

        '=== JE pense qu'il n'y a pas de retrait dans l'enrobage


    End Sub

    Private Sub InitialiseChargesRetraitDalle(ByRef MyCas As cls_CasDeCharge)
        '-------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Préparation du cas de charge "Retrait" pour les poutres mixtes usuelles
        '-------------------------------------------------------------------------------------------
        '   MyCas       [S] :   Cas de charge
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim nEqSH As Decimal
        Dim Nsh, Msh As Decimal
        Const SIGNESH As Decimal = -1
        Dim iTravPrem As Integer = Me.IndicePremiereTravee
        Dim iTravDern As Integer = Me.IndiceDerniereTravee
        Dim xGauche, xDroite As Decimal
        Dim xApp As Decimal

        '--> Initialisation

        nEqSH = Me.Elements(MyCas.IndElts).nEqDalle

        '--> Préparation du cas de charge

        For iTrav As Integer = 1 To Me.NombreTraveesDeuxAppuis

            xApp = Me.xPositionAppui(True, iTrav)
            Me.ParametresRetraitDalle(nEqSH, iTrav, Nsh, Msh)

            If iTrav = iTravPrem Then
                xGauche = 0
            Else
                xGauche = 0.15 * Me.LongueurTravee(iTrav)
            End If

            If iTrav = iTravDern Then
                xDroite = Me.LongueurTravee(iTrav)
            Else
                xDroite = 0.85 * Me.LongueurTravee(iTrav)
            End If

            MyCas.Moments(iTrav).Add(New cls_Moment(xGauche, SIGNESH * Msh, xApp))
            MyCas.Moments(iTrav).Add(New cls_Moment(xDroite, -SIGNESH * Msh, xApp))

        Next

    End Sub

    Public Sub ParametresRetraitDalle(nEqDal As Decimal, iTrav As Integer, ByRef Nsh As Decimal, ByRef Msh As Decimal)
        '-------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Calcul des paramètres permettant le calcul du cas de charge retrait de la dalle
        '-------------------------------------------------------------------------------------------
        '   nEqDal      [E] :   Coefficient d'équivalence pour le béton de la dalle
        '   iTrav       [E] :   Indice de la travée
        '   Nsh         [S] :   Effort normal dans la dalle du au retrait
        '   Msh         [S] :   Moment fléchissant correspondant
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim Beff As Decimal
        Dim zANe As Decimal
        Dim InertieY, MelRd As Decimal
        Dim nEqEc As Decimal = nEqDal
        Dim lSimpleM As Boolean = False
        Dim Tc As Decimal
        Dim EpsilonSh As Decimal = Me.Param.EpsilonSH
        Dim DeltaZ As Decimal

        '--> Calcul des propriétés à mi-travée

        Beff = Me.BeffDalle(Me.LongueurTravee(iTrav) / 2, iTrav, lSimpleM, True, cls_Poutre.EnuTypeLargeurParticipante.LargeurTotale)
        Tc = Me.Dalle.EpaisseurActive
        Me.Section.ProprietesElastiquesMixteMyy(1, True, Me.Param.Gamma, nEqEc, nEqDal, Beff, Me.Dalle, zANe, InertieY, MelRd)
        DeltaZ = Me.Dalle.zTop - Tc / 2 - zANe

        '--> Effort normal dans la dalle

        Nsh = Beff * Tc / nEqDal * Me.Section.Acier.EYoung * kConvMPaPa * EpsilonSh
        Msh = Nsh * DeltaZ

    End Sub

    Private Sub InitialiseChargeA(ByRef MyCdCA As cls_CasDeCharge, MyChargeU As cls_ChargementUtilisateur, iTravees As List(Of Integer))
        '-------------------------------------------------------------------------------------------
        '   18/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Initialisation d'un cas de charge calcul à partir d'un cas défini par l'utilisateur
        '-------------------------------------------------------------------------------------------
        '   MyCdCA      [S] :   Cas de charge pour le calcul
        '   MyChargeU   [E] :   Chargement défini par l'utilisateur
        '   iTravees    [E] :   Liste des travées où le chargement est appliqué
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iTrav, i, kTrav As Integer
        Dim xAppG As Decimal
        Dim x0, fr0, x1, fr1, xg As Decimal

        '--> Initialisation



        '--> Traitement

        For iTrav = 0 To iTravees.Count - 1

            kTrav = iTravees(iTrav)
            xAppG = Me.xPositionAppui(True, kTrav)

            '# charges concentrées

            For i = 0 To MyChargeU.Forces(kTrav).Count - 1
                MyCdCA.Forces(kTrav).Add(New cls_Force(MyChargeU.Forces(kTrav)(i).xPosT, MyChargeU.Forces(kTrav)(i).Force, xAppG))
            Next

            '# charges réparties

            For i = 0 To MyChargeU.FReparties(kTrav).Count - 1

                x0 = MyChargeU.FReparties(kTrav)(i).xPosT(0)
                x1 = MyChargeU.FReparties(kTrav)(i).xPosT(1)
                fr0 = MyChargeU.FReparties(kTrav)(i).Force(0)
                fr1 = MyChargeU.FReparties(kTrav)(i).Force(1)
                xg = Me.xPositionAppui(True, kTrav)

                MyCdCA.FReparties(kTrav).Add(New cls_ForceRepartie(x0, fr0, x1, fr1, xg))

            Next

            '# Charges surfaciques

            MyCdCA.QSurf(kTrav) = MyChargeU.QSurf(kTrav)

        Next

    End Sub


#End Region

#Region " Chargements, poids propre "

    Public Sub InitialiseCasdeChargesCalcul(NomChargesA() As String)
        '-------------------------------------------------------------------------------------------
        '   07/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Initialisation des cas de charges à traiter par le moteur de calcul
        '-------------------------------------------------------------------------------------------
        '   NomChargesA     [E] :   Nom des cas de charge (dans les langue utilisateur)
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lMixte As Boolean
        Dim lEtaitComplet As Boolean
        Dim lNonEtaye As Boolean
        Dim lEnrob As Boolean

        Dim nEqDalleCT, nEqEnrobCT As Decimal
        ' Dim nEqDalleCT, nEqDalleLT As Decimal
        ' Dim nEqEnrobCT, nEqEnrobLT As Decimal

        Dim nEqDalleG1, nEqEnrobG1 As Decimal
        Dim nEqDalleG2, nEqEnrobG2 As Decimal
        Dim nEqDalleSH, nEqEnrobSH As Decimal

        '# ZZZ Assurer la liaison avec les fichiers langue
        Dim strChargesPermanentes As String = NomChargesA(0)        ' "Permanent loads"
        Dim strPoidsPropre As String = NomChargesA(1)               ' "Self weight"
        Dim strPoidsPropreEtaye As String = NomChargesA(2)          ' "Self weight with props"
        Dim strPoidsPropreSansEtais As String = NomChargesA(3)      ' "Self weight without props"
        Dim strAutresChargesPermanentes As String = NomChargesA(4)  ' "Other permanent loads"
        Dim strExploitation As String = NomChargesA(5)              ' "Live loads"
        Dim strConfiguration As String = NomChargesA(6)             ' "Conf. no "
        Dim strRetraitDalle As String = NomChargesA(7)              ' "Shrinkage of the slab"
        Dim strRetraitEnrob As String = NomChargesA(8)              ' "Shrinkage of the encasement"
        Dim strConstruction As String = NomChargesA(9)              ' "Construction loads"

        Dim IndiceG As Integer
        Dim IndiceQ As Integer
        Dim IndiceG1, IndiceG2, IndiceSH As Integer

        Dim NbTrav, iTrav0 As Integer

        Dim TraveesTous As New List(Of Integer)
        Dim TraveesConsoles As New List(Of Integer)
        Dim TraveesCentrale As New List(Of Integer)
        Dim pEtatDalle As cls_CasDeCharge.EnuEtatDalle
        Dim pEtatDalleNonMixte As cls_CasDeCharge.EnuEtatDalle = cls_CasDeCharge.EnuEtatDalle.Acier
        Dim pEtatDalleMixte As cls_CasDeCharge.EnuEtatDalle = cls_CasDeCharge.EnuEtatDalle.Mixte

        '--> Initialisation

        Me.Elements.Clear()
        lMixte = Me.lMixte
        lEnrob = Me.lEnrobage
        lEtaitComplet = (TypeEtaiement = EnuTypeEtaiement.FullyPropped)
        lNonEtaye = (TypeEtaiement = EnuTypeEtaiement.UnPropped)
        If (TypeEtaiement = EnuTypeEtaiement.PointPropped) Then
            If (Not Me.lEtaisConsoleGauche) And (Not Me.lEtaisConsoleDroite) And (Me.NbEtaiement = 0) Then
                lNonEtaye = True
            End If
        End If

        nEqDalleCT = Me.Dalle.beton.CoefficientEquivalenceCT
        nEqEnrobCT = Me.Section.Enrobage.Beton.CoefficientEquivalenceCT
        'nEqDalleLT = 3 * nEqDalleCT
        'nEqEnrobLT = 3 * nEqEnrobCT

        Dim RH As Decimal = Me.Param.RH
        Dim TimeT As Decimal = Me.Param.AgeT
        Dim H0Dalle As Decimal = Me.Dalle.NotionalSizeH0(Me.Section.ProfilA.Bfs)
        Dim H0Enrob As Decimal = Me.Section.NotionalSizeEnrobage

        If lMixte Then
            nEqDalleG1 = Me.Dalle.beton.CoefficientEquivalence(RH, H0Dalle, TimeT, Me.Param.AgeT0G1(0), Me.Param.PsiLPermanent)
            nEqDalleG2 = Me.Dalle.beton.CoefficientEquivalence(RH, H0Dalle, TimeT, Me.Param.AgeT0G2(0), Me.Param.PsiLPermanent)
            nEqDalleSH = Me.Dalle.beton.CoefficientEquivalence(RH, H0Dalle, TimeT, Me.Param.AgeT0SH(0), Me.Param.PsiLRetrait)
        End If

        If lEnrob Then
            nEqEnrobG1 = Me.Dalle.beton.CoefficientEquivalence(RH, H0Enrob, TimeT, Me.Param.AgeT0G1(1), Me.Param.PsiLPermanent)
            nEqEnrobG2 = Me.Dalle.beton.CoefficientEquivalence(RH, H0Enrob, TimeT, Me.Param.AgeT0G2(1), Me.Param.PsiLPermanent)
            nEqEnrobSH = Me.Dalle.beton.CoefficientEquivalence(RH, H0Enrob, TimeT, Me.Param.AgeT0SH(1), Me.Param.PsiLRetrait)
        End If

        iTrav0 = Me.IndicePremiereTravee
        NbTrav = Me.NbTravees

        Me.ChargesA.Clear()

        For i = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
            TraveesTous.Add(i)
        Next
        If Me.lTraveeConsoleGauche Then TraveesConsoles.Add(0)
        If Me.lTraveeConsoleDroite Then TraveesConsoles.Add(Me.IndiceDerniereTravee)
        TraveesCentrale.Add(1)

        '--> Traitement des charges permanentes 

        '# Charges permanentes globales

        If Not lMixte Then pEtatDalle = cls_CasDeCharge.EnuEtatDalle.Acier Else pEtatDalle = cls_CasDeCharge.EnuEtatDalle.Mixte

        If (Not lMixte) Or lEtaitComplet Then
            IndiceG = Me.IndiceTabElts(lMixte, nEqDalleG1, nEqEnrobG1)

            Me.ChargesA.Add(New cls_CasDeCharge(strChargesPermanentes, "G", IndiceG, iTrav0, NbTrav, cls_CasDeCharge.EnuType.Permanente, pEtatDalle))
            InitialiseChargeA(Me.ChargesA(Me.ChargesA.Count - 1), Me.ChargesU("G1"), TraveesTous)
        End If

        '# Charges de poids propres pour les poutres mixtes non étayées

        If lMixte Then

            IndiceG1 = Me.IndiceTabElts(lMixte, nEqDalleG1, nEqEnrobG1)
            IndiceG2 = Me.IndiceTabElts(lMixte, nEqDalleG2, nEqEnrobG2)

            If lNonEtaye Then
                Me.ChargesA.Add(New cls_CasDeCharge(strPoidsPropre, symbG1, Me.IndiceTabElts(False, 0, nEqEnrobG1), iTrav0, NbTrav, cls_CasDeCharge.EnuType.Permanente, pEtatDalleNonMixte))
                'InitialiseChargesPP(Me.ChargesA(Me.ChargesA.Count - 1))
                InitialiseChargeA(Me.ChargesA(Me.ChargesA.Count - 1), Me.ChargesU("G1"), TraveesTous)
            Else
                'Cas de l'étaiement ponctuel
                Me.ChargesA.Add(New cls_CasDeCharge(strPoidsPropre, symbG1PP, Me.IndiceTabElts(False, 0, nEqEnrobG1), iTrav0, NbTrav, cls_CasDeCharge.EnuType.Permanente, pEtatDalleNonMixte))
                'InitialiseChargesPP(Me.ChargesA(Me.ChargesA.Count - 1))
                InitialiseChargeA(Me.ChargesA(Me.ChargesA.Count - 1), Me.ChargesU("G1"), TraveesTous)

                Me.ChargesA.Add(New cls_CasDeCharge(strPoidsPropre, symbG1C, IndiceG1, iTrav0, NbTrav, cls_CasDeCharge.EnuType.Permanente, pEtatDalleMixte))
                'Il n'y a pas besoin d'initialiser G1C ici : cette partie se fait après le calcul de G1pp
            End If

            Me.ChargesA.Add(New cls_CasDeCharge(strAutresChargesPermanentes, symbG2, IndiceG2, iTrav0, NbTrav, cls_CasDeCharge.EnuType.Permanente, pEtatDalleMixte))
            InitialiseChargeA(Me.ChargesA(Me.ChargesA.Count - 1), Me.ChargesU("G2"), TraveesTous)
        End If

        '--> Charges d'exploitation

        IndiceQ = Me.IndiceTabElts(lMixte, nEqDalleCT, nEqEnrobCT)

        Dim LabelQ() As String = {symbQ1, symbQ2, symbQC}
        Dim lMultiT As Boolean
        Dim ChaineEx As String
        Me.lMultiQ = {False, False, False}

        For iq As Integer = 0 To 2
            lMultiT = Me.ChargesU(LabelQ(iq)).EstMultiTravee(Me.IndicePremiereTravee, Me.IndiceDerniereTravee)
            Me.lMultiQ(iq) = lMultiT
            ChaineEx = strExploitation & " " & CStr(iq + 1)
            If (Me.NbTravees = 1) Or (Not lMultiT) Then
                Me.ChargesA.Add(New cls_CasDeCharge(ChaineEx, LabelQ(iq), IndiceQ, iTrav0, NbTrav, cls_CasDeCharge.EnuType.Exploitation, pEtatDalle))
                InitialiseChargeA(Me.ChargesA(Me.ChargesA.Count - 1), Me.ChargesU(LabelQ(iq)), TraveesTous)
            Else
                Me.ChargesA.Add(New cls_CasDeCharge(ChaineEx & " " & strConfiguration & " 1", LabelQ(iq) & "#1", IndiceQ, iTrav0, NbTrav, cls_CasDeCharge.EnuType.Exploitation, pEtatDalle))
                InitialiseChargeA(Me.ChargesA(Me.ChargesA.Count - 1), Me.ChargesU(LabelQ(iq)), TraveesTous)
                Me.ChargesA.Add(New cls_CasDeCharge(ChaineEx & " " & strConfiguration & " 2", LabelQ(iq) & "#2", IndiceQ, iTrav0, NbTrav, cls_CasDeCharge.EnuType.Exploitation, pEtatDalle))
                InitialiseChargeA(Me.ChargesA(Me.ChargesA.Count - 1), Me.ChargesU(LabelQ(iq)), TraveesCentrale)
                Me.ChargesA.Add(New cls_CasDeCharge(ChaineEx & " " & strConfiguration & " 3", LabelQ(iq) & "#3", IndiceQ, iTrav0, NbTrav, cls_CasDeCharge.EnuType.Exploitation, pEtatDalle))
                InitialiseChargeA(Me.ChargesA(Me.ChargesA.Count - 1), Me.ChargesU(LabelQ(iq)), TraveesConsoles)
            End If
        Next

        '--> Retrait

        IndiceSH = Me.IndiceTabElts(lMixte, nEqDalleSH, nEqEnrobSH)
        Me.indiceCasRetrait = -1

        If lMixte Then
            Me.ChargesA.Add(New cls_CasDeCharge(strRetraitDalle, "SHC", IndiceSH, iTrav0, NbTrav, cls_CasDeCharge.EnuType.Retrait, pEtatDalleMixte))
            InitialiseChargesRetraitDalle(Me.ChargesA(Me.ChargesA.Count - 1))
            Me.indiceCasRetrait = Me.ChargesA.Count - 1
        End If

        'If lEnrob And Me.Param.lRetraitEnrobage Then
        '    Me.ChargesA.Add(New cls_CasDeCharge(strRetraitEnrob, "SHE", IndiceSH, iTrav0, NbTrav, cls_CasDeCharge.EnuType.Retrait, pEtatDalle))
        'End If

        ''--> Charges de construction

        'If lMixte And (Not lEtaitComplet) Then
        '    Me.ChargesA.Add(New cls_CasDeCharge(strConstruction, "QC", Me.IndiceTabElts(False, 0, nEqEnrobG1), iTrav0, NbTrav, cls_CasDeCharge.EnuType.Construction, pEtatDalleNonMixte))
        '    InitialiseChargeA(Me.ChargesA(Me.ChargesA.Count - 1), Me.ChargesU("QC"), TraveesTous)
        'End If

        '--> Préparation des cas de charges shadow pour les poutres mixtes

        If lMixte And Me.Param.lFlechesETA Then Me.InitialiseCasdeChargesCalculShadow()

    End Sub

    Private Sub InitialiseCasdeChargesCalculShadow()
        '-------------------------------------------------------------------------------------------
        '   03/02/24 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Initialisation des cas de charges shadow à traiter par le moteur de calcul
        '   Les cas de charges shadow sont créés temporairement pour les poutres mixtes
        '   Chaque cas est la doublure d'un cas réel
        '   Il permet de calculer la flèche de la poutre mixte en prenant en compte la rigidité de la connexion
        '   Les cas de charges shadow sont supprimés après l'analyse globale
        '-------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim iCas As Integer
        Dim indiceElt, indEltShadow As Integer
        Dim lCasMixte As Boolean
        Dim nEqDalle, nEqEnrob As Double
        Dim nbCas As Integer = Me.ChargesA.Count
        Dim lDefini As Boolean
        Dim iTrav0, iTrav1 As Decimal

        '--( Initialisation

        iTrav0 = Me.IndicePremiereTravee
        iTrav1 = Me.IndiceDerniereTravee

        '--( Boucle sur les cas de charges

        If Me.lMixte Then

            For iCas = 0 To nbCas - 1

                indiceElt = Me.ChargesA(iCas).IndElts
                lCasMixte = Me.Elements(indiceElt).lMixte
                lDefini = Me.ChargesA(iCas).EstNonNul(iTrav0, iTrav1)

                '--( On ne dédouble que les cas mixtes et défini

                If lCasMixte And lDefini Then

                    nEqDalle = Me.Elements(indiceElt).nEqDalle
                    nEqEnrob = Me.Elements(indiceElt).nEqEnrob

                    indEltShadow = Me.IndiceTabElts(lMixte, nEqDalle, nEqEnrob, True)

                    Me.ChargesA.Add(New cls_CasDeCharge(Me.ChargesA(iCas).Nom, Me.ChargesA(iCas).Symbol, iCas, indEltShadow))

                End If

            Next

        End If

    End Sub

    Public Structure StructPoidsPropres
        Dim qPP_ProfilAcier As Decimal
        Dim qPP_DalleBeton As Decimal
        Dim qPP_BacAcier As Decimal
        Dim qPP_BetonEnrobage As Decimal
        Dim qPP_Total As Decimal
    End Structure

    Public Sub InitialisePoidsPropres()
        '-------------------------------------------------------------------------------------------
        '   23/08/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Initialisation des charges réparties de poids propre
        '-------------------------------------------------------------------------------------------
        '   
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim G_PP As StructPoidsPropres = Me.ChargeRepartiePP()

        '--> Traitement

        For iTravee As Integer = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
            If Me.ChargesU(KEYPP).FReparties(iTravee).Count = 0 Then
                Me.ChargesU(KEYPP).FReparties(iTravee).Add(New cls_ForceRepartie(0, G_PP.qPP_Total, Me.LongueurTravee(iTravee), G_PP.qPP_Total, Me.xPositionAppui(True, iTravee)))
            Else
                Me.ChargesU(KEYPP).FReparties(iTravee)(0) = New cls_ForceRepartie(0, G_PP.qPP_Total, Me.LongueurTravee(iTravee), G_PP.qPP_Total, Me.xPositionAppui(True, iTravee))
            End If

        Next

    End Sub

    Public Function ChargeRepartiePP() As StructPoidsPropres
        '-------------------------------------------------------------------------------------------
        '   09/09/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Retourne la charge répartie de poids propre
        '-------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim G_PP As StructPoidsPropres
        Dim G As Decimal = Me.Param.GraviteG

        Dim dc As Decimal 'largeur de calcul pour le PP
        If lIntermediaire Then
            dc = Me.EntraxeD1 / 2 + Me.EntraxeD2 / 2
        Else
            dc = Me.EntraxeD1 + Me.EntraxeD2 / 2
        End If

        '--> Calcul
        With G_PP

            '# Profilé acier
            .qPP_ProfilAcier = Me.Section.ProfilA.Aire * Me.Section.Acier.Rho * G

            '# Béton d'enrobage
            If Me.Section.lEnrobage Then
                .qPP_BetonEnrobage = Me.Section.AireEnrobagePartielAec * Me.Section.Enrobage.Beton.RhoC * G
            Else
                .qPP_BetonEnrobage = 0
            End If

            '# Dalle
            .qPP_DalleBeton = Me.Dalle.Aire(dc, Me.Section.ProfilA.Bfs) * Me.Dalle.beton.RhoC * G

            '# Bac acier
            If Me.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte Then
                .qPP_BacAcier = Me.Dalle.Bac.msurf * dc * G
            Else
                .qPP_BacAcier = 0
            End If


            '--> Bilan et fin
            .qPP_Total = .qPP_ProfilAcier + .qPP_DalleBeton + .qPP_BacAcier + .qPP_BetonEnrobage

        End With

        Return G_PP

    End Function

    Public Sub Initialise_CoefficientsCombinaisons()
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

    Public Sub AAA_CalculMNVInternesN(Optional lSigma As Boolean = False)
        '-------------------------------------------------------------------------------------
        '   04/11/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Analyse globale pour tous les cas de charges
        '-------------------------------------------------------------------------------------
        '   Avant de lancer ce calcul, il est nécessaire d'avoir effectuer InitialiseCalculs
        '-------------------------------------------------------------------------------------
        '   lSigma      [E] :   Indique si on calcule aussi les contraintes
        '-------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iTravP, iTravD As Integer
        Dim jCdc, jCasTransfert As Integer
        Dim lPrem As Boolean = True
        Dim lAppuisOK As Boolean = False
        Dim lOK As Boolean
        Dim IndEltPrec As Integer = -1
        Dim lAppuisEtais As Boolean
        Dim lAppuisEtaisPrec As Boolean

        '--> Initialisation

        iTravP = Me.IndicePremiereTravee
        iTravD = Me.IndiceDerniereTravee

        '--> Préparation du modele EF

        Me.Analyse = New cls_AnalyseEFinis(Me.Section.Acier.EYoung, Me.Param.GraviteG, Me.Nodes)

        '--> Boucle sur les cas de charge

        For jCdc = 0 To Me.ChargesA.Count - 1
            If (Me.ChargesA(jCdc).EstNonNul(iTravP, iTravD) Or Me.ChargesA(jCdc).lShadow) Then

                '# Préparation des propriétés des éléments
                If (IndEltPrec <> Me.ChargesA(jCdc).IndElts) Then
                    Me.Analyse.AttribueProprietesElements(Me.Elements(Me.ChargesA(jCdc).IndElts).Aire, Me.Elements(Me.ChargesA(jCdc).IndElts).InertieY)
                    IndEltPrec = Me.ChargesA(jCdc).IndElts
                End If

                '# Transfert du chargement
                If Me.ChargesA(jCdc).lShadow Then jCasTransfert = Me.ChargesA(jCdc).iShadow Else jCasTransfert = jCdc
                Me.Analyse.TransfertChargementA(Me.ChargesA(jCasTransfert), iTravP, iTravD, Me.LongueurTravee, Me.LargeurInfluence)

                '# Préparation des appuis (dans le cas des étais ponctuels)
                lAppuisEtais = (Me.ChargesA(jCdc).Symbol = symbG1PP)

                If lPrem Then
                    Me.Analyse.Appuis(Me.Nodes, lAppuisEtais)
                    lAppuisEtaisPrec = lAppuisEtais
                    lPrem = False
                Else
                    If Not (lAppuisEtaisPrec = lAppuisEtais) Then
                        Me.Analyse.Appuis(Me.Nodes, lAppuisEtais)
                        lAppuisEtaisPrec = lAppuisEtais
                    End If
                End If

                '=== LANCER LE CALCUL ===
                Me.Analyse.RunRDM(lOK)

                '== Récupération des résultats
                '# Récupération des réactions aux étais pour préparer le cas de charge G1C
                If (Me.ChargesA(jCdc).Symbol = symbG1PP) And (Not Me.ChargesA(jCdc).lShadow) Then
                    Me.InitialiseChargeEtais(Me.ChargesA(Me.IndiceCasG1C), Me.Analyse.Reactions)
                End If

                If lOK Then
                    If Me.ChargesA(jCdc).lShadow Then
                        Me.ChargesA(Me.ChargesA(jCdc).iShadow).RecupereFlecheEta(Me.Analyse.Fleches)
                    Else
                        Me.ChargesA(jCdc).RecupereResultats(Me.Analyse.Tranchants, Me.Analyse.Moments, Me.Analyse.Fleches, Me.Analyse.Rotations, Me.Analyse.Reactions)
                    End If
                Else
                    MsgBox("Error calculation of " & Me.ChargesA(jCdc).Nom, MsgBoxStyle.Critical, "cls_Poutre/AAA_CalculMNVInternesN")
                End If

            End If
        Next

        '--( Nettoyage des cas de charges shadow

        Me.SupprimeCasShadow()

    End Sub

    Private Sub SupprimeCasShadow()
        '-------------------------------------------------------------------------------------
        '   03/02/24 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Suppression des cas de charges shadow
        '-------------------------------------------------------------------------------------

        '--( Déclaration

        Dim nbCas As Integer = Me.ChargesA.Count
        Dim iCas As Integer

        '--( Traitement des cas de charges

        If Me.lMixte And Me.Param.lFlechesETA Then

            For iCas = nbCas - 1 To 0 Step -1

                If Me.ChargesA(iCas).lShadow Then Me.ChargesA.Remove(Me.ChargesA(iCas))

            Next

        End If

    End Sub

    'Public Sub AAA_CalculMNVInternes(Optional lSigma As Boolean = False)
    '    '-------------------------------------------------------------------------------------
    '    '   21/08/23 :  Création - Version 1.00 - POM
    '    '-------------------------------------------------------------------------------------
    '    '   Analyse globale pour tous les cas de charges
    '    '-------------------------------------------------------------------------------------
    '    '   Avant de lancer ce calcul, il est nécessaire d'avoir effectuer InitialiseCalculs
    '    '-------------------------------------------------------------------------------------
    '    '   lSigma      [E] :   Indique si on calcule aussi les contraintes
    '    '-------------------------------------------------------------------------------------

    '    '--> Déclarations

    '    Dim jCdc As Integer
    '    Dim MyDLLRDM As New CTICM_RDM.CALCUL_RDM
    '    Dim MyOutput_RDM As CTICM_RDM.DATA_RDM.Struc_Output = Nothing
    '    Dim CodeError_RDM As Integer
    '    Dim TextError_RDM As String = String.Empty
    '    Dim DonneesEF As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees = Nothing
    '    Dim iTravP, iTravD As Integer
    '    Dim lPrem As Boolean = True
    '    Dim lAppuisEtais As Boolean = False
    '    Dim lAppuisEtaisPrec As Boolean = False
    '    Dim lAppuisOK As Boolean

    '    '--> Initialisation

    '    iTravP = Me.IndicePremiereTravee
    '    iTravD = Me.IndiceDerniereTravee

    '    'If lSigma Then
    '    '    Me.PtsSigma.Initialise(Me)
    '    'End If

    '    '--> Préparation du modele EF

    '    Me.PrepareModeleEF(DonneesEF, lAppuisOK)

    '    '--> Boucle sur les cas de charge

    '    For jCdc = 0 To Me.ChargesA.Count - 1
    '        If Me.ChargesA(jCdc).EstNonNul(iTravP, iTravD) Then

    '            '# Préparation du chargement
    '            PrepareDonneesEFChargement(jCdc, iTravP, iTravD, DonneesEF)

    '            '# Préparation des appuis (dans le cas des étais ponctuels)
    '            If Not lAppuisOK Then
    '                If lPrem Then
    '                    lAppuisEtais = (Me.ChargesA(jCdc).Symbol = symbG1PP)
    '                    PrepareAppuisModeleEF(lAppuisEtais, DonneesEF)
    '                    lAppuisEtaisPrec = lAppuisEtais
    '                    lPrem = False
    '                Else
    '                    lAppuisEtais = (Me.ChargesA(jCdc).Symbol = symbG1PP)
    '                    If Not (lAppuisEtaisPrec = lAppuisEtais) Then
    '                        PrepareAppuisModeleEF(lAppuisEtais, DonneesEF)
    '                        lAppuisEtaisPrec = lAppuisEtais
    '                    End If
    '                End If
    '            End If

    '            '=== LANCER LE CALCUL ===
    '            Call MyDLLRDM.CALCULER(DonneesEF, MyOutput_RDM, CodeError_RDM, TextError_RDM)

    '            '# Récupération des réactions aux étais pour préparer le cas de charge G1C
    '            If (Me.ChargesA(jCdc).Symbol = symbG1PP) Then
    '                Me.InitialiseChargeEtais(Me.ChargesA(Me.IndiceCasG1C), MyOutput_RDM.RZ)
    '            End If

    '            If CodeError_RDM = 0 Then
    '                Me.ChargesA(jCdc).RecupereResultats(MyOutput_RDM, DonneesEF.NbNodes)
    '                'If lSigma Then
    '                '    Me.PtsSigma.CalculsContraintes(Me, Me.ChargesA(jCdc), Me.ChargesA(jCdc).Sigma)
    '                'End If
    '            Else
    '                MsgBox("Error calculation of " & Me.ChargesA(jCdc).Nom, MsgBoxStyle.Critical, "cls_Poutre/CalculMNVInternes")
    '            End If

    '        End If
    '    Next

    'End Sub

    Public Sub InitialiseCalculs(NomChargesA() As String)
        '-------------------------------------------------------------------------------------
        '   07/09/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Initialisation des calculs RDM
        '-------------------------------------------------------------------------------------

        Me.PrepareNodesN(Me.Param.dMaxNodes, Me.Param.nbMinNodesTravee, Me.Param.nbMinNodesConsole)
        Me.InitialiseCasdeChargesCalcul(NomChargesA)

        Me.Dalle.AcierArmatures.Es = Me.Param.ArmaYoung
        Me.Section.Enrobage.AcierArmatures.Es = Me.Param.ArmaYoung

    End Sub

    Public Sub PrepareModeleEF(ByRef pDonneesEF As CTICM_DATA_DLLS.DATA_DLLS, ByRef lAppuisOK As Boolean)
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

        lAppuisOK = False
        If Me.Nodes.NbAppuis = 0 Then
            PrepareAppuisModeleEF(False, pDonneesEF)
            lAppuisOK = True
        End If

    End Sub

    'Private Sub PrepareAppuisModeleEFOld(lEtais As Boolean, ByRef pDonneesEF As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees)
    '    '-------------------------------------------------------------------------------------
    '    '   20/09/23 :  Création - Version 1.00 - POM
    '    '-------------------------------------------------------------------------------------
    '    '   Préparation des appuis du modele EF avant lancement des calculs
    '    '   en prenant en compte les appuis des étais, le cas échéant        
    '    '-------------------------------------------------------------------------------------

    '    If lEtais Then
    '        pDonneesEF.NbAppuis = Me.Nodes.NbAppuis + Me.Nodes.NbEtais
    '    Else
    '        pDonneesEF.NbAppuis = Me.Nodes.NbAppuis
    '    End If

    '    ReDim pDonneesEF.iNodeAppui(pDonneesEF.NbAppuis - 1)
    '    ReDim pDonneesEF.lAppuiArticule(pDonneesEF.NbAppuis - 1)

    '    For i As Integer = 0 To Me.Nodes.NbAppuis - 1
    '        pDonneesEF.iNodeAppui(i) = Me.Nodes.iNodeAppui(i)
    '    Next
    '    If lEtais Then
    '        For i As Integer = 0 To Me.Nodes.NbEtais - 1
    '            pDonneesEF.iNodeAppui(Me.Nodes.NbAppuis - 1 + i) = Me.Nodes.iNodeEtais(i)
    '        Next
    '        Array.Sort(pDonneesEF.iNodeAppui)
    '    End If
    '    For i As Integer = 0 To pDonneesEF.NbAppuis - 1
    '        pDonneesEF.lAppuiArticule(i) = False
    '    Next
    'End Sub

    Public Sub PrepareAppuisModeleEF(lEtais As Boolean, ByRef pDonneesEF As CTICM_DATA_DLLS.DATA_DLLS)
        '-------------------------------------------------------------------------------------
        '   20/09/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Préparation des appuis du modele EF avant lancement des calculs
        '   en prenant en compte les appuis des étais, le cas échéant        
        '-------------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbApp As Integer
        Dim indAppuis() As Integer = Nothing

        '--> Initialisation

        ExtraireIndiceNoeudsAppuis(lEtais, indAppuis, NbApp)

        pDonneesEF.NbAppuis = NbApp

        ReDim pDonneesEF.iNodeAppui(pDonneesEF.NbAppuis - 1)
        ReDim pDonneesEF.lAppuiArticule(pDonneesEF.NbAppuis - 1)

        '--> Traitement

        For i As Integer = 0 To pDonneesEF.NbAppuis - 1
            pDonneesEF.iNodeAppui(i) = indAppuis(i)
            pDonneesEF.lAppuiArticule(i) = False
        Next
    End Sub

    Public Sub ExtraireIndiceNoeudsAppuis(lEtais As Boolean, ByRef indAppuis() As Integer, ByRef NbApp As Integer)
        '-------------------------------------------------------------------------------------
        '   20/09/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Préparation des appuis du modele EF avant lancement des calculs
        '   en prenant en compte les appuis des étais, le cas échéant
        '-------------------------------------------------------------------------------------
        '   lEtais      [E] :   INdique si on ajoute les étais ponctuels comme appuis 
        '   indAppuis   [E] :   Liste des indices des noeuds appuyés
        '-------------------------------------------------------------------------------------

        '--> Déclaration 



        '--> Nombre d'appuis

        If lEtais Then
            NbApp = Me.Nodes.NbAppuis + Me.Nodes.NbEtais
        Else
            NbApp = Me.Nodes.NbAppuis
        End If

        ReDim indAppuis(NbApp - 1)

        '--> Appuis des travées

        For i As Integer = 0 To Me.Nodes.NbAppuis - 1
            indAppuis(i) = Me.Nodes.iNodeAppui(i)
        Next

        '--> Etais

        If lEtais Then
            For i As Integer = 0 To Me.Nodes.NbEtais - 1
                indAppuis(Me.Nodes.NbAppuis + i) = Me.Nodes.iNodeEtais(i)
            Next
            Array.Sort(indAppuis)
        End If
    End Sub

    'Private Sub PrepareDonneesEFChargement(iCas As Integer, iTravP As Integer, iTravD As Integer, ByRef pDonneesEF As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees)
    '    '-------------------------------------------------------------------------------------
    '    '   07/09/23 :  Création - Version 1.00 - POM
    '    '-------------------------------------------------------------------------------------
    '    '   Préparation du modele EF dépendant du cas de charge (propriétés elements et charges)
    '    '-------------------------------------------------------------------------------------
    '    '   iCas        [E] :   Indice du cas de charge
    '    '   pDonneesEF  [S] :   Donnes pour le calcul EF
    '    '   iTravP      [E] :   Indice première travée
    '    '   iTravD      [E] :   Indice dernière travée
    '    '-------------------------------------------------------------------------------------

    '    '--> Déclarations

    '    Dim iTrav As Integer
    '    Dim NbfRep, NbQSurf As Integer
    '    Dim Compteur As Integer = -1
    '    Dim xo, xe As Decimal
    '    Dim qsurfD As Decimal

    '    '--> Transfert des propriétés de section

    '    For i As Integer = 0 To pDonneesEF.NbNodes - 2
    '        pDonneesEF.Aire(i) = Me.Elements(Me.ChargesA(iCas).IndElts).Aire(i)
    '        pDonneesEF.InertieY(i) = Me.Elements(Me.ChargesA(iCas).IndElts).InertieY(i)
    '    Next

    '    '--> Initialisation

    '    NbQSurf = 0
    '    For iTrav = iTravP To iTravD
    '        If Not IsEqual(Me.ChargesA(iCas).QSurf(iTrav), 0) Then NbQSurf += 1
    '    Next
    '    NbfRep = Me.ChargesA(iCas).NombreFRep(Me.IndicePremiereTravee, Me.IndiceDerniereTravee)
    '    pDonneesEF.NbForcesRep = NbfRep + NbQSurf

    '    If pDonneesEF.NbForcesRep > 0 Then
    '        ReDim pDonneesEF.xForceRep(pDonneesEF.NbForcesRep - 1, 1)
    '        ReDim pDonneesEF.ForceRep(pDonneesEF.NbForcesRep - 1, 1)
    '    End If

    '    pDonneesEF.NbForcesPon = 0
    '    pDonneesEF.NbMoments = 0

    '    '--> Transfert des charges

    '    For iTrav = iTravP To iTravD

    '        '# Charges surfaciques

    '        qsurfD = Me.ChargesA(iCas).QSurf(iTrav)
    '        If Not IsEqual(qsurfD, 0) Then
    '            xo = Me.xPositionAppui(True, iTrav)
    '            xe = Me.xPositionAppui(False, iTrav)
    '            Compteur += 1
    '            'AjouteForceRep(Compteur, xo, xe, qsurfD, qsurfD, pDonneesEF)
    '        End If

    '        '# Forces

    '        For iFor As Integer = 0 To Me.ChargesA(iCas).Forces(iTrav).Count - 1
    '            AjouteForce(Me.ChargesA(iCas).Forces(iTrav)(iFor).xPosG, Me.ChargesA(iCas).Forces(iTrav)(iFor).Force, pDonneesEF)
    '        Next

    '        '# Moments

    '        For iMom As Integer = 0 To Me.ChargesA(iCas).Moments(iTrav).Count - 1
    '            AjouteMoment(Me.ChargesA(iCas).Moments(iTrav)(iMom).xPosG, Me.ChargesA(iCas).Moments(iTrav)(iMom).Moment, pDonneesEF)
    '        Next

    '        '# Charges réparties

    '        If NbfRep > 0 Then

    '            For iQqq As Integer = 0 To Me.ChargesA(iCas).FReparties(iTrav).Count - 1
    '                Compteur += 1
    '                AjouteForceRep(Compteur, Me.ChargesA(iCas).FReparties(iTrav)(iQqq).xPosG(0), Me.ChargesA(iCas).FReparties(iTrav)(iQqq).xPosG(1),
    '                                         Me.ChargesA(iCas).FReparties(iTrav)(iQqq).Force(0), Me.ChargesA(iCas).FReparties(iTrav)(iQqq).Force(1), pDonneesEF)
    '            Next

    '        End If

    '    Next

    'End Sub

    'Private Sub AjouteForceRep(IndFrep As Integer, xo As Decimal, xe As Decimal, qo As Decimal, qe As Decimal, ByRef pDonneesEF As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees)
    '    '-------------------------------------------------------------------------------------
    '    '   09/09/23 :  Création - Version 1.00 - POM
    '    '-------------------------------------------------------------------------------------
    '    '   Ajout d'une force répartie dans les paramètres préparatoires au calcul EF
    '    '-------------------------------------------------------------------------------------
    '    '   xo          [E] :   Position gauche de la force répartie
    '    '   xe          [E] :   Position droite de la force répartie
    '    '   qo          [E] :   Valeur à gauche de la force répartie
    '    '   qe          [E] :   Valeur à droite de la force répartie
    '    '   pDonneesEF  [S] :   Donnes pour le calcul EF
    '    '-------------------------------------------------------------------------------------

    '    pDonneesEF.xForceRep(IndFrep, 0) = xo
    '    pDonneesEF.xForceRep(IndFrep, 1) = xe
    '    pDonneesEF.ForceRep(IndFrep, 0) = qo
    '    pDonneesEF.ForceRep(IndFrep, 1) = qe

    'End Sub

    'Private Sub AjouteMoment(xMom As Decimal, Moment As Decimal, ByRef pDonneesEF As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees)
    '    '-------------------------------------------------------------------------------------
    '    '   09/09/23 :  Création - Version 1.00 - POM
    '    '-------------------------------------------------------------------------------------
    '    '   Ajout d'un moment dans les paramètres préparatoires au calcul EF
    '    '-------------------------------------------------------------------------------------
    '    '   xMom        [E] :   Position du moment
    '    '   Moment      [E] :   Valeur du moment
    '    '   pDonneesEF  [S] :   Donnes pour le calcul EF
    '    '-------------------------------------------------------------------------------------

    '    pDonneesEF.NbMoments += 1
    '    If pDonneesEF.NbMoments = 1 Then
    '        ReDim pDonneesEF.Moment(pDonneesEF.NbMoments - 1)
    '        ReDim pDonneesEF.xMoment(pDonneesEF.NbMoments - 1)
    '    Else
    '        ReDim Preserve pDonneesEF.Moment(pDonneesEF.NbMoments - 1)
    '        ReDim Preserve pDonneesEF.xMoment(pDonneesEF.NbMoments - 1)
    '    End If

    '    pDonneesEF.Moment(pDonneesEF.NbMoments - 1) = Moment
    '    pDonneesEF.xMoment(pDonneesEF.NbMoments - 1) = xMom

    'End Sub


    'Private Sub AjouteForce(xFor As Decimal, Force As Decimal, ByRef pDonneesEF As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees)
    '    '-------------------------------------------------------------------------------------
    '    '   18/09/23 :  Création - Version 1.00 - POM
    '    '-------------------------------------------------------------------------------------
    '    '   Ajout d'un effort vertical dans les paramètres préparatoires au calcul EF
    '    '-------------------------------------------------------------------------------------
    '    '   xFor        [E] :   Position de la force
    '    '   Force       [E] :   Valeur de la force
    '    '   pDonneesEF  [S] :   Donnes pour le calcul EF
    '    '-------------------------------------------------------------------------------------

    '    pDonneesEF.NbForcesPon += 1
    '    If pDonneesEF.NbForcesPon = 1 Then
    '        ReDim pDonneesEF.ForcePon(pDonneesEF.NbForcesPon - 1)
    '        ReDim pDonneesEF.xForcePon(pDonneesEF.NbForcesPon - 1)
    '    Else
    '        ReDim Preserve pDonneesEF.ForcePon(pDonneesEF.NbForcesPon - 1)
    '        ReDim Preserve pDonneesEF.xForcePon(pDonneesEF.NbForcesPon - 1)
    '    End If

    '    pDonneesEF.ForcePon(pDonneesEF.NbForcesPon - 1) = Force
    '    pDonneesEF.xForcePon(pDonneesEF.NbForcesPon - 1) = xFor

    'End Sub

#End Region

#Region " Outils post traitement de l'analyse "

    Public Sub AnalyseDiagrammeMoments(MEd(,) As Decimal, ByRef iNodeMmax() As Integer, ByRef Mmax() As Decimal,
                                       ByRef xMZero(,) As Decimal, ByRef lTraveeMomNeg() As Boolean)
        '-------------------------------------------------------------------------------------
        '   19/10/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Analyse d'un diagramme de moments pour les travées intermédiaires
        '   Recherche du point de moment maxi
        '   Recherche des points de moments nuls
        '-------------------------------------------------------------------------------------
        '   MEd             [E] :   Diagrame de moments, sur tous les noeuds de la poutre
        '   iNodeMmax       [S] :   Indice du noeud de moment max, pour les travées intermédiaires
        '   Mmax            [S] :   Tableau des valeurs de moment max, pour les travées intermédiaires
        '   xMZero          [S] :   Tableau de la position x où les moment sont nuls (iTravee,k), k=0 à gauche, 1 à droite de la travée, par rapport au point de moment max)
        '   lTraveeMomNeg   [S] :   Indique une travée entièrement sous moment négatif
        '-------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iTravee As Integer

        '--> Initialisation

        ReDim iNodeMmax(Me.NombreTraveesDeuxAppuis)
        ReDim Mmax(Me.NombreTraveesDeuxAppuis)
        ReDim lTraveeMomNeg(Me.NombreTraveesDeuxAppuis)
        ReDim xMZero(Me.NombreTraveesDeuxAppuis, 1)

        '--> Calcul par travée

        For iTravee = 1 To Me.NombreTraveesDeuxAppuis
            lTraveeMomNeg(iTravee) = Me.EstTraveeEntierementMomentNegatif(iTravee, MEd)
            If Not lTraveeMomNeg(iTravee) Then
                RechercheMomentMaxTravee(iTravee, MEd, iNodeMmax(iTravee), Mmax(iTravee))
                RechercheMomentsNuls(iTravee, MEd, Mmax(iTravee), iNodeMmax(iTravee), True, xMZero(iTravee, 0))
                RechercheMomentsNuls(iTravee, MEd, Mmax(iTravee), iNodeMmax(iTravee), False, xMZero(iTravee, 1))
            End If
        Next

    End Sub

    Private Function EstTraveeEntierementMomentNegatif(iTravee As Integer, MEd(,) As Decimal) As Boolean
        '-------------------------------------------------------------------------------------
        '   20/10/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Détecte une travée entièrement sous moment négatif
        '-------------------------------------------------------------------------------------
        '   iTravee     [E] :   Indice de la travée
        '   MEd         [E] :   Diagrame de moments, sur tous les noeuds de la poutre
        '-------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iNode, iDeb, iFin As Integer
        Dim lNeg As Boolean = True
        Dim MedZero As Decimal = -1

        '--> Initialisation

        iDeb = Me.Nodes.iNodeExtTrav(iTravee, 0)
        iFin = Me.Nodes.iNodeExtTrav(iTravee, 1)

        '--> Extremités

        lNeg = (IsSmaller(MEd(iDeb, 1), MedZero)) And (IsSmaller(MEd(iFin, 0), MedZero))

        '--> Travée

        iNode = iDeb
        Do While (lNeg And (iNode < iFin - 1))
            iNode += 1
            lNeg = (IsSmaller(MEd(iNode, 1), MedZero)) And (IsSmaller(MEd(iNode, 0), MedZero))
        Loop

        Return lNeg
    End Function

    Private Sub RechercheMomentsNuls(iTravee As Integer, MEd(,) As Decimal, Mmax As Decimal,
                                     iNodeMMax As Integer, lGauche As Boolean, ByRef xMZero As Decimal)
        '-------------------------------------------------------------------------------------
        '   20/10/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Recherche dans un diagramme de moment
        '   du point de moment nul dans une travée particulière
        '   Cela suppose que l'on a auparavant déterminé la position du point de moment max
        '   et que la travée n'est pas entièrement sous moment négatif
        '-------------------------------------------------------------------------------------
        '   iTravee     [E] :   Indice de la travée
        '   MEd         [E] :   Diagrame de moments, sur tous les noeuds de la poutre
        '   iNodeMmax   [E] :   Indice du noeud de moment max, pour la travée calculée
        '   Mmax        [E] :   Valeur de moment max, pour la travée calculée
        '   lGauche     [E] :   Indique si on cherche le point de moment nul à gauche ou à droite du pt de moment max
        '   xMzero      [S] :   Position x du point de moment nul
        '-------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iNode, iDeb, iFin, iStep, kDeb As Integer
        Dim MZero As Decimal
        Dim lTrouve As Boolean

        '--> Initialisation

        iFin = iNodeMMax
        If lGauche Then
            iDeb = Me.Nodes.iNodeExtTrav(iTravee, 0)
            iStep = 1
        Else
            iStep = -1
            iDeb = Me.Nodes.iNodeExtTrav(iTravee, 1)
        End If
        MZero = Mmax / 10000
        kDeb = 0.5 + iStep / 2

        '--> Recherche du point de moment nul

        lTrouve = IsSmaller(MEd(iDeb, kDeb), MZero)
        iNode = iDeb

        If lTrouve Then
            xMZero = Me.Nodes.xTravee(iNode)
        Else

            Do While (Not lTrouve) And (iNode <> iFin + iStep)
                iNode += iStep
                lTrouve = IsSmaller(Math.Abs(MEd(iNode, 0)), MZero) And IsSmaller(Math.Abs(MEd(iNode, 1)), MZero)

                If lTrouve Then
                    xMZero = Me.Nodes.xTravee(iNode)
                Else

                    lTrouve = IsSmaller(MEd(iNode - iStep, 1), -MZero) And IsGreater(MEd(iNode, 0), MZero)

                    If lTrouve Then

                        xMZero = Me.Nodes.xTravee(iNode - iStep) _
                               + (Me.Nodes.xTravee(iNode) - Me.Nodes.xTravee(iNode - iStep)) / (MEd(iNode, 1) - MEd(iNode - iStep, 0)) _
                               * (MEd(iNode, 1))

                    End If

                End If

            Loop

        End If

        If Not lTrouve Then
            xMZero = -1
        End If
    End Sub

    Private Sub RechercheMomentMaxTravee(iTravee As Integer, MEd(,) As Decimal, ByRef iNodeMm As Integer, ByRef Mmax As Decimal)
        '-------------------------------------------------------------------------------------
        '   19/10/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Recherche dans un diagramme de moment
        '   du moment max dans une travée particulière
        '-------------------------------------------------------------------------------------
        '   iTravee     [E] :   Indice de la travée
        '   MEd         [E] :   Diagrame de moments, sur tous les noeuds de la poutre
        '   iNodeMmax   [S] :   Indice du noeud de moment max, pour la travée calculée
        '   Mmax        [S] :   Valeur de moment max, pour la travée calculée
        '-------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iNode, iDeb, iFin As Integer
        Dim iT1, iT2 As Integer
        Dim pMmax As Decimal = 0
        Dim kFin As Integer

        '--> Initialisation

        iDeb = Me.Nodes.iNodeExtTrav(iTravee, 0)
        iFin = Me.Nodes.iNodeExtTrav(iTravee, 1)

        '--> Traitement

        pMmax = MEd(iDeb, 1)
        iT1 = iDeb
        iT2 = iT1

        kFin = 1

        For iNode = iDeb + 1 To iFin

            If iNode = iFin Then kFin = 0
            For k = 0 To kFin
                If IsGreater(MEd(iNode, k), pMmax) Then
                    pMmax = MEd(iNode, k)
                    iT1 = iNode
                    iT2 = iNode
                ElseIf IsEqual(MEd(iNode, k), pMmax) Then
                    iT2 = iNode
                Else

                End If
            Next

        Next

        If iT2 > iT1 Then
            iNodeMm = Math.Floor((iT2 + iT1) / 2)
            Mmax = pMmax
        Else
            iNodeMm = iT1
            Mmax = pMmax
        End If

    End Sub

    Public Sub RechercheANEFromSigma(SigmaELU(,,) As Decimal, MEd(,) As Decimal, NbNodes As Integer, ByRef m_zANE(,) As Decimal)
        '--------------------------------------------------------------------------------------------
        '   25/10/23 :  Création - Version 1.00 - POM
        '--------------------------------------------------------------------------------------------
        '   Calcul de la position de l'ANE à partir des contraintes élastiques
        '--------------------------------------------------------------------------------------------
        '   SigmaELU        [E] :   Table des contraintes normales aux ELU
        '   MEd             [E] :   Table des moments ELU
        '   NbNodes         [E] :   Nombre de noeuds dans le modèle
        '   m_zANE          [S] :   Table de la position des ANE
        '--------------------------------------------------------------------------------------------

        '--> Déclarations

        Const iTOP As Integer = 1       ' Indice du point de calcul de la contrainte dans la semelle sup
        Const iBOT As Integer = 5       ' Indice du point de calcul de la contrainte dans la semelle inf

        Dim zTop As Decimal = 0
        Dim zBot As Decimal = -Me.Section.ProfilA.ha

        Dim iNode, k As Integer
        Dim kDeb, kFin As Integer

        Const CONVMPOS As Decimal = 1
        Const CONVSIGCOMP As Decimal = -1

        Dim SigmaTop, SigmaBot As Decimal

        '--> Initialisation

        ReDim m_zANE(NbNodes - 1, 1)

        '--> Traitement

        If (Me.PtsSigma.iProfile(0) >= 0) _
        And ((Me.PtsSigma.iProfile(1) - Me.PtsSigma.iProfile(0)) >= (iBOT - iTOP)) Then

            For iNode = 0 To NbNodes - 1

                If iNode = 0 Then kDeb = 1 Else kDeb = 1
                If iNode = NbNodes - 1 Then kFin = 0 Else kFin = 1

                For k = kDeb To kFin
                    SigmaTop = SigmaELU(Me.PtsSigma.iProfile(0) + iTOP - 1, iNode, k)
                    SigmaBot = SigmaELU(Me.PtsSigma.iProfile(0) + iBOT - 1, iNode, k)

                    If IsEqual(SigmaTop, SigmaBot) Then
                        '== Si les contraintes sont égales
                        If (CONVMPOS * MEd(iNode, k) >= 0) Then
                            If CONVSIGCOMP * SigmaTop >= 0 Then
                                m_zANE(iNode, k) = zTop
                            Else
                                m_zANE(iNode, k) = zBot
                            End If
                        Else
                            If CONVSIGCOMP * SigmaTop < 0 Then
                                m_zANE(iNode, k) = zTop
                            Else
                                m_zANE(iNode, k) = zBot
                            End If
                        End If

                    Else

                        m_zANE(iNode, k) = zTop + (zBot - zTop) / (SigmaBot - SigmaTop) * (0 - SigmaBot)
                    End If

                Next

            Next
        End If



    End Sub

#End Region

#Region " Outils pour la connexion "

    Public Function xZoneT(iTravee As Integer, iZone As Integer) As Decimal
        '---------------------------------------------------------------------------
        '   31/10/23 :  Création - POM
        '---------------------------------------------------------------------------
        '   Donne la position de l'extrémité droite d'une zone de connexion
        '   (par rapport à l'appui gauche de la travée)
        '---------------------------------------------------------------------------
        '   iTravee     [E] :   Indice de la travée
        '   iZone       [E] :   Indice de la zone
        '---------------------------------------------------------------------------

        '--> Déclarations

        Dim xCum As Decimal
        Dim iZe As Integer
        '--> Traitement

        xCum = Me.LongueurZone(iTravee, 0)
        For iZe = 1 To iZone
            xCum += Me.LongueurZone(iTravee, iZe)
        Next

        '--> Fin

        Return xCum

    End Function

    'Fonction qui retourne le nombre de goujon tot sur la travée en argument
    Public Function NombreGoujonTot(indTravee As Integer)
        Dim resultat As Integer = 0

        For j As Integer = 0 To 2
            resultat += NombreGoujonTotParZone(indTravee, j)
        Next

        Return resultat

    End Function

    Public Function NombreGoujonTotParZone(indTravee As Integer, indZone As Integer)
        Dim resultat As Integer = 0

        resultat += NombreGoujonsTransv(indTravee, indZone) * LongueurZone(indTravee, indZone) / EspacementZone(indTravee, indZone)

        Return resultat

    End Function

#End Region

#Region " Vérifications "

    Public Sub AAA_Verifications(NomCharges() As String, strRacineELU As String, strRacineELS As String, strRacineELF As String, strRacineELUC As String, strRacineELSC As String)
        '-------------------------------------------------------------------------------------
        '   05/10/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Routine générale pour gérér les vérifications de la poutre
        '-------------------------------------------------------------------------------------

        '--> Déclarations

        'Dim i As Integer

        '--> Initialisation des tableaux de verification

        Select Case Me.TypeSection
            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                ReDim Me.VerifAcier(0)
                Me.VerifAcier(0) = New cls_VerificationsAcier
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                ReDim Me.VerifMixte(0)
                Me.VerifMixte(0) = New cls_VerificationsMixtes
                If Me.TypeEtaiement <> EnuTypeEtaiement.FullyPropped Then
                    ' Quand on est pas totalement étayé, on ajoute la vérification en phase de construction
                    ReDim Me.VerifAcier(0)
                    Me.VerifAcier(0) = New cls_VerificationsAcier
                End If
        End Select

        '--> Initialisation des calculs
        Me.InitialisePoidsPropres()
        Me.InitialiseCalculs(NomCharges)
        Me.AAA_CalculMNVInternesN()
        'MyPoutre.InitialiseCombiA_ELU()
        Me.InitialiseCombiA(cls_Poutre.nbCombELU, Me.lCombELU, Me.CoefCombELU, strRacineELU, Me.CombiA_ELU)
        Me.InitialiseCombiA(cls_Poutre.nbCombELS, Me.lCombELS, Me.CoefCombELS, strRacineELS, Me.CombiA_ELS)
        Me.InitialiseCombiA(cls_Poutre.nbCombFeu, Me.lCombFeu, Me.CoefCombFeu, strRacineELF, Me.CombiA_ELF)
        Me.InitialiseCombiA(cls_Poutre.nbCombELUConstruction, Me.lCombELCURules, Me.CoefCombELCU, strRacineELUC, Me.CombiA_ELCU)
        Me.InitialiseCombiA(cls_Poutre.nbCombELSConstruction, Me.lCombELCSRules, Me.CoefCombELCS, strRacineELSC, Me.CombiA_ELCS)

        '--> Vérifications

        Select Case Me.TypeSection
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                '# Vérification des poutres mixtes en phase finale aux ELU
                Me.VerifMixte(0).Z_VerificationELU(Me)
                Me.VerifAcier(0).Z_VerificationELU(Me, True)

            Case cls_Section.Enum_TypeSection.AcierSeul, cls_Section.Enum_TypeSection.AcierSeulEnrobage
                Me.VerifAcier(0).Z_VerificationELU(Me, False)

        End Select


    End Sub

    Public Function VerificationsELUDispo(vlMixte As Boolean) As Boolean
        '-------------------------------------------------------------------------------------
        '   20/11/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Indique si les vérifications ont été effectuées et sont disponibles
        '-------------------------------------------------------------------------------------
        '    vlMixte    [E] :   Indique si on teste la verification mixte ou acier
        '-------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lOK As Boolean = True

        '--> Traitement

        '# La classe a t elle été créée ?

        If vlMixte Then
            If Me.VerifMixte Is Nothing Then
                lOK = False
            Else
                If Me.VerifMixte.GetUpperBound(0) < 0 Then
                    lOK = False
                Else
                    If Me.VerifMixte(0) Is Nothing Then lOK = False
                End If
            End If
        Else
            If Me.VerifAcier Is Nothing Then
                lOK = False
            Else
                If Me.VerifAcier.GetUpperBound(0) < 0 Then
                    lOK = False
                Else
                    If Me.VerifAcier(0) Is Nothing Then lOK = False
                End If
            End If

        End If

        '# Les critères de la classe sont ils définis

        If lOK Then
            If vlMixte Then
                lOK = Me.VerifMixte(0).CriteresInitialises
            Else
            End If
        End If


        Return lOK

    End Function


#End Region

#Region " Outils poutres mixtes "

    Public Sub MaillageDegreConnexion(bEff() As Decimal, SigneM As Decimal, ByRef DegreC() As Decimal)
        '------------------------------------------------------------------------------------------------------------------
        '    07/10/23 : Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Calcul du degré de connexion le long de la barre (au droit des noeuds du maillage)
        '------------------------------------------------------------------------------------------------------------------
        '   bEff        [E] :   Largeur de dalle
        '   SigneM      [E] :   Signe du moment
        '   DegreC      [S] :   Degré de connexion
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim RConnexG, RConnexD, RConnex As Decimal

        '--> Initialisation

        ReDim DegreC(Me.Nodes.nbNodes - 1)


        '--> Boucle sur les noeuds

        For iNode As Integer = 0 To Me.Nodes.nbNodes - 1

            'RConnexG = ResistanceConnexionMixte(xSec, MyBeam.PorteeH, MyBeam.SectionSup.Bf, MyBeam.Dalle, CoefGammaVRd1, CoefGammaVRd2, False, True)
            'RConnexD = ResistanceConnexionMixte(xSec, MyBeam.PorteeH, MyBeam.SectionSup.Bf, MyBeam.Dalle, CoefGammaVRd1, CoefGammaVRd2, False, False)
            RConnex = Math.Min(RConnexG, RConnexD)


        Next

    End Sub

    Public Sub MaillageRConnexion(xMZero(,) As Decimal, ByRef DeltaRd() As List(Of Decimal))
        '------------------------------------------------------------------------------------------------------------------
        '    31/10/23 : Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Calcul de la resistance de connexion le long de la barre (au droit des noeuds du maillage), par rapport au points de moments nuls
        '------------------------------------------------------------------------------------------------------------------
        '   xMZero      [E] :   Position des points de moments nuls dans les travées intermédiaires
        '   DeltaRd     [S] :   Somme des résistances des connecteurs entre le noeud et le point de moment nul
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iTravee, iNode As Integer
        Dim pDeltaRd(1) As Decimal

        '--> Initialisation

        ReDim DeltaRd(Me.IndiceDerniereTravee)

        For iTravee = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
            DeltaRd(iTravee) = New List(Of Decimal)
        Next
        Me.InitialiseDensiteConnexion()

        '--> Traitement des travées en console

        If Me.lTraveeConsoleGauche Then
            iTravee = 0
            For iNode = Me.Nodes.iNodeExtTrav(iTravee, 0) To Me.Nodes.iNodeExtTrav(iTravee, 1)
                DeltaRd(iTravee).Add(Me.DeltaRdX(iTravee, Me.Nodes.xTravee(iNode), 0))
            Next
        End If
        If Me.lTraveeConsoleDroite Then
            iTravee = Me.IndiceDerniereTravee
            For iNode = Me.Nodes.iNodeExtTrav(iTravee, 0) To Me.Nodes.iNodeExtTrav(iTravee, 1)
                DeltaRd(iTravee).Add(Me.DeltaRdX(iTravee, Me.Nodes.xTravee(iNode), Me.LongueurTotale))
            Next
        End If

        '--> Traitement des travées intermédiaires

        For iTravee = 1 To Me.NombreTraveesDeuxAppuis
            For iNode = Me.Nodes.iNodeExtTrav(iTravee, 0) To Me.Nodes.iNodeExtTrav(iTravee, 1)
                For i As Integer = 0 To 1
                    pDeltaRd(i) = Me.DeltaRdX(iTravee, Me.Nodes.xTravee(iNode), xMZero(iTravee, i))
                Next
                DeltaRd(iTravee).Add(pDeltaRd.Min)
            Next
        Next

    End Sub

    Private Function DeltaRdX(iTravee As Decimal, xPosT As Decimal, xRefT As Decimal) As Decimal
        '------------------------------------------------------------------------------------------------------------------
        '    31/10/23 : Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Renvoie le cumul des résistances des connecteurs entre deux positions
        '------------------------------------------------------------------------------------------------------------------
        '   iTravee     [E] :   Indice de la travée
        '   xPosT       [E] :   Position de la section étudiée  (par rapport à l'extrémité gauche de la travée)
        '   xRefT       [E] :   Position de référence (idem)
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pDeltaRd As Decimal = 0
        Dim pxPos(1) As Decimal
        Dim iZone(1) As Integer

        '--> Traitement

        If Not IsEqual(xPosT, xRefT) Then

            pxPos(0) = Math.Min(xPosT, xRefT)
            pxPos(1) = Math.Max(xPosT, xRefT)

            iZone(0) = IndiceZoneFromPosition(iTravee, pxPos(0))
            iZone(1) = IndiceZoneFromPosition(iTravee, pxPos(1))

            If (iZone(0) = iZone(1)) Then
                '# Cas où les deux positions sont dans la même zone de connexion
                pDeltaRd = (pxPos(1) - pxPos(0)) * Me.DensiteConnexionZone(iTravee, iZone(0))
            Else
                pDeltaRd = (Me.xZoneT(iTravee, iZone(0)) - pxPos(0)) * Me.DensiteConnexionZone(iTravee, iZone(0))
                pDeltaRd += (pxPos(1) - Me.xZoneT(iTravee, iZone(1) - 1)) * Me.DensiteConnexionZone(iTravee, iZone(1))
                For iZe As Integer = iZone(0) + 1 To iZone(1) - 1
                    pDeltaRd += Me.LongueurZone(iTravee, iZe) * Me.DensiteConnexionZone(iTravee, iZe)
                Next
            End If
        End If

        '--> Fin

        Return pDeltaRd

    End Function

    Private Function IndiceZoneFromPosition(iTravee As Integer, xPosT As Decimal) As Integer
        '------------------------------------------------------------------------------------------------------------------
        '    31/10/23 : Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Renvoie l'indice de la zone de connexion dans laquelle se trouve une position x
        '------------------------------------------------------------------------------------------------------------------
        '   iTravee     [E] :   Indice de la travée
        '   xPosT       [E] :   Position de la section étudiée  (par rapport à l'extrémité gauche de la travée)
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim pIndice As Integer = 0
        Dim xCum As Decimal
        Dim iZone As Integer
        Dim lTrouve As Boolean

        '--> Traitement

        If Me.NombreZones(iTravee) > 1 Then
            iZone = 0
            xCum = Me.LongueurZone(iTravee, iZone)
            lTrouve = IsSmallerOrEqual(xPosT, xCum)
            Do While (Not lTrouve) And (iZone < Me.NombreZones(iTravee) - 1)
                iZone += 1
                xCum += Me.LongueurZone(iTravee, iZone)
                lTrouve = IsSmallerOrEqual(xPosT, xCum)
            Loop
            If lTrouve Then pIndice = iZone Else pIndice = -1
        End If

        '--> Fin 

        Return pIndice
    End Function

    Public Sub InitialiseDensiteConnexion()
        '------------------------------------------------------------------------------------------------------------------
        '    31/10/23 : Création - POM
        '------------------------------------------------------------------------------------------------------------------
        '   Initialise la table DensiteConnexion
        '------------------------------------------------------------------------------------------------------------------
        '------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iTravee, iZone As Integer
        Dim PRd As Decimal
        Dim lGeneration1 As Boolean
        Dim lDallePleine As Boolean
        Dim lPerp As Boolean
        Dim Ecm, Fck As Decimal
        Dim gammaVs, gammaVc As Decimal
        Dim nR As Integer
        Dim pEspace As Decimal
        Dim lBacNervuresPerpContinues As Boolean

        '--> Initialisation

        ReDim Me.DensiteConnexionZone(Me.IndiceDerniereTravee, 2)
        lGeneration1 = Me.Param.lGeneration1
        lDallePleine = (Me.Dalle.type = cls_Dalle.Enum_TypeDalle.Pleine) Or (Me.Dalle.type = cls_Dalle.Enum_TypeDalle.Prefabriquee)
        lPerp = (Me.Dalle.Bac.Orientation = cls_Bac.Enum_Orientation.Perpendiculaire) And (Me.Dalle.Bac.AppuiT <> cls_Bac.EnuConfigTAppui.Discontinu)
        Ecm = Me.Dalle.beton.Ecm
        Fck = Me.Dalle.beton.Fck
        gammaVs = Me.Param.Gamma.GammaVs
        gammaVc = Me.Param.Gamma.GammaVc
        lBacNervuresPerpContinues = (Me.Dalle.lMixte And Me.Dalle.Bac.lPerpendiculaire And Me.Dalle.Bac.lNervuresContinues)

        '--> Traitement

        For iTravee = Me.IndicePremiereTravee To Me.IndiceDerniereTravee
            For iZone = 0 To Me.NombreZones(iTravee) - 1

                'If lBacNervuresPerpContinues Then
                '    pEspace = Me.Espacement_Bac_TransZone(iTravee, iZone) * Me.Dalle.Bac.Ep
                'Else
                '    pEspace = Me.EspacementZone(iTravee, iZone)
                'End If

                pEspace = Me.EntraxeLongiGoujons(iTravee, iZone)

                nR = Me.NombreGoujonsTransv(iTravee, iZone)
                PRd = Me.Dalle.Connecteur.ResistancePRd(lGeneration1, lDallePleine, lPerp, Me.Dalle.Bac, nR, Fck, Ecm, gammaVs, gammaVc)

                Me.DensiteConnexionZone(iTravee, iZone) = PRd * nR / pEspace

            Next
        Next


    End Sub



#End Region

End Class
