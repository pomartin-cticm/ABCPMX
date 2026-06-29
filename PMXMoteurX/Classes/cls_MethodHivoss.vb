Imports System.Xml
Imports PMXMoteur2.cls_Poutre

Public Class cls_MethodHivoss

#Region " Enumérations "
    ''' <summary>
    ''' Indique si on utilise Q1 ou Q2 dans la combinaison de masses pour la fréquence
    ''' </summary>
    Enum Enu_Q
        Q1
        Q2
    End Enum

    Enum Enu_UtilisationPlancher
        ZoneSensible
        Sante
        Education
        Residentiel
        Bureau
        Reunion
        MaisonRetraite
        Hotel
        Industriel
        Sports
    End Enum

    ''' <summary>
    ''' Indique quel type de mobilier est présent dans la structure
    ''' </summary>
    Enum Enu_Mobiliers
        BureauAvecCloison
        BureauSansArmoires
        BureauPaysager
        Bibliotheque
        Residentiel
        Ecole
        SalleDeSport
        Personnalise
    End Enum

#End Region

#Region " Variables "

    ''' <summary>
    ''' Indique si on applique la méthode Hivoss (=True) ou non (False)
    ''' </summary>
    Public lHivossMethod As Boolean

    ''' <summary>
    ''' Ratio de Q que l'on considère dans la combinaison de masses pour la fréquence
    ''' </summary>
    Public ratioQ As Decimal

    ''' <summary>
    ''' Indique si on utilise Q1 ou Q2 dans la combinaison de masses pour la fréquence
    ''' </summary>
    Public choixQ As Enu_Q

    ''' <summary>
    ''' Indique l'utilisation du plancher
    ''' </summary>
    Public UtilisationPlancher As Enu_UtilisationPlancher

    ''' <summary>
    ''' Indique quel type de mobilier est présent dans la structure
    ''' </summary>
    Public Mobilier As Enu_Mobiliers

    ''' <summary>
    ''' Indique la présence (=True) ou non (=False) de faux plafonds
    ''' </summary>
    Public lFauxPlafond As Boolean

    ''' <summary>
    ''' Indique la présence (=True) ou non (=False) d'une chappe flottante
    ''' </summary>
    Public lChappeFlottante As Boolean

    ''' <summary>
    ''' Amortissement de la structure 
    ''' </summary>
    Public AmortiStructure_D1 As Decimal

    ''' <summary>
    ''' Amortissement induit par la présence du mobilier
    ''' </summary>
    Public AmortiMobilier_D2 As Decimal

    ''' <summary>
    ''' Amortissement induit par les finitions (faux plafonds et/ou chappe flottante)
    ''' </summary>
    Public AmortiFinition_D3 As Decimal

    ''' <summary>
    ''' Amortissement total pris en compte 
    ''' </summary>
    Public AmortiTotal_Dtot As Decimal

    Public lFreqDalle As Boolean                    ' Indique si on prend en compte la frequence propre de la dalle

    Public Frequence As Decimal                     ' Fréquence propre de la poutre
    Public MassModale As Decimal                    ' Masse modale
    Public Amortissement As Decimal                 ' Amortissement

    Public FreqDalle As Decimal                     ' Frequence propre dalle seule
    Public FreqPoutre As Decimal                    ' Frequence propre profilé seul

    Public HCategorie As String                     ' Resultat de l'analyse Hivoss (A, B ...)
    Public OsRMS As Decimal                         ' Valeur du paramètre OS RMS issue de l'analyse Hivoss
    Public indConfort As Integer                    ' Indice de confort (0 pour recommended ; 1 pour critical ; 2 pour not recommended)

    Public lMgenNumerique As Boolean                ' Indique si on prend la valeur de masse généralisée issue de la DLL_CTICM_MODAL

#End Region

#Region " Variables privées "

    Private P1 As New strHivossTable
    Private P2 As New strHivossTable
    Private P3 As New strHivossTable
    Private P4 As New strHivossTable
    Private P5 As New strHivossTable
    Private P6 As New strHivossTable
    Private P7 As New strHivossTable
    Private P8 As New strHivossTable
    Private P9 As New strHivossTable

    Public Const MASSMODMIN As Decimal = 100
    Public Const MASSMODMAX As Decimal = 100000

    Public Structure strHivossTable

        Public nbLigne As Integer
        Public listeLigneMasse As struc_HivossLigne()
        Public listeFrequence As Decimal()

    End Structure

    Public Structure struc_HivossLigne

        Public valeurs As Decimal() '--> frontières (kg)
        Public valGauche As Decimal '--> valeur de gauche pour 100 kg (-1 si inutile)
        Public valDroite As Decimal '--> valeur de droite pour 100 000 kg (-1 si inutile)

        '--> Constructeur
        Public Sub New(ByVal v As Decimal(), ByVal g As Decimal, ByVal d As Decimal)
            valeurs = v
            valGauche = g
            valDroite = d
        End Sub

    End Structure

    Private pLimitesIntervalles As Decimal() = {51.2, 12.8, 3.2, 0.8, 0.2, 0.1, 0}   '--> Constantes des frontières
    Private pLettresIntervalles As Char() = {"!", "F", "E", "D", "C", "B", "A"}      '--> Catégories

#End Region

#Region " Constructeur "
    Sub New()

        Me.lHivossMethod = True
        Me.ratioQ = 0.1
        Me.choixQ = Enu_Q.Q1
        Me.UtilisationPlancher = Enu_UtilisationPlancher.Bureau
        Me.Mobilier = Enu_Mobiliers.BureauAvecCloison
        lFauxPlafond = False
        lChappeFlottante = False
        lFreqDalle = False

        CalculAmortissement()

        lMgenNumerique = True

    End Sub

#End Region

#Region " Outils "

    Public Function LettresIntervalles() As Char()
        Return pLettresIntervalles
    End Function

    Public Function LimitesIntervalles() As Decimal()
        Return pLimitesIntervalles
    End Function

    Public ReadOnly Property IndexQ As Integer
        Get
            Dim myInd As Integer
            Select Case Me.choixQ
                Case Enu_Q.Q1 : myInd = 1
                Case Enu_Q.Q1 : myInd = 2
            End Select
            Return myInd
        End Get
    End Property

#End Region

#Region " Calculs Amortissement "
    ''' <summary>
    ''' Calcul les différentes valeurs des amortissements en fonction des différentes paramètres renseignés
    ''' </summary>
    Sub CalculAmortissement()
        Me.AmortiStructure_D1 = 1 / 100

        Select Case Mobilier
            Case Enu_Mobiliers.BureauAvecCloison
                Me.AmortiMobilier_D2 = 2 / 100
            Case Enu_Mobiliers.BureauSansArmoires
                Me.AmortiMobilier_D2 = 0 / 100
            Case Enu_Mobiliers.BureauPaysager
                Me.AmortiMobilier_D2 = 1 / 100
            Case Enu_Mobiliers.Bibliotheque
                Me.AmortiMobilier_D2 = 1 / 100
            Case Enu_Mobiliers.Residentiel
                Me.AmortiMobilier_D2 = 1 / 100
            Case Enu_Mobiliers.Ecole
                Me.AmortiMobilier_D2 = 0 / 100
            Case Enu_Mobiliers.SalleDeSport
                Me.AmortiMobilier_D2 = 0 / 100
            Case Enu_Mobiliers.Personnalise

        End Select

        If lFauxPlafond Or lChappeFlottante Then
            If lFauxPlafond And lChappeFlottante Then
                Me.AmortiFinition_D3 = 2 / 100
            Else
                Me.AmortiFinition_D3 = 1 / 100
            End If
        Else
            Me.AmortiFinition_D3 = 0 / 100
        End If

        Me.AmortiTotal_Dtot = Me.AmortiStructure_D1 + Me.AmortiMobilier_D2 + Me.AmortiFinition_D3

    End Sub

#End Region

#Region " Fonction de copie "
    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

#End Region

#Region " Initialisation des Tables "

    Public Sub ChargerValeursHivoss(ByRef AllFloorVibration As Dictionary(Of Integer, strHivossTable))
        '------------------------------------------------------
        ' Insertion de toutes les données dans le dictionnaire
        '------------------------------------------------------

        '--> 1%
        'For i = 0 To 39
        '    P1.Add(Hz_P1(i), Kg_P1(i))
        'Next

        P1.listeFrequence = Hz_P1
        P1.listeLigneMasse = Kg_P1
        P1.nbLigne = Hz_P1.Length

        AllFloorVibration.Add(1, P1)

        '--> 2%
        'For i = 0 To 35
        '    P2.Add(Hz_P2(i), Kg_P2(i))
        'Next
        P2.listeFrequence = Hz_P2
        P2.listeLigneMasse = Kg_P2
        P2.nbLigne = Hz_P2.Length

        AllFloorVibration.Add(2, P2)

        '--> 3%
        'For i = 0 To 36
        '    P3.Add(Hz_P3(i), Kg_P3(i))
        'Next
        P3.listeFrequence = Hz_P3
        P3.listeLigneMasse = Kg_P3
        P3.nbLigne = Hz_P3.Length

        AllFloorVibration.Add(3, P3)

        '--> 4%
        'For i = 0 To 37
        '    P4.Add(Hz_P4(i), Kg_P4(i))
        'Next
        P4.listeFrequence = Hz_P4
        P4.listeLigneMasse = Kg_P4
        P4.nbLigne = Hz_P4.Length

        AllFloorVibration.Add(4, P4)

        '--> 5%
        'For i = 0 To 36
        '    P5.Add(Hz_P5(i), Kg_P5(i))
        'Next
        P5.listeFrequence = Hz_P5
        P5.listeLigneMasse = Kg_P5
        P5.nbLigne = Hz_P5.Length

        AllFloorVibration.Add(5, P5)

        '--> 6%
        'For i = 0 To 35
        '    P6.Add(Hz_P6(i), Kg_P6(i))
        'Next
        P6.listeFrequence = Hz_P6
        P6.listeLigneMasse = Kg_P6
        P6.nbLigne = Hz_P6.Length

        AllFloorVibration.Add(6, P6)

        '--> 7%
        'For i = 0 To 35
        '    P7.Add(Hz_P7(i), Kg_P7(i))
        'Next
        P7.listeFrequence = Hz_P7
        P7.listeLigneMasse = Kg_P7
        P7.nbLigne = Hz_P7.Length

        AllFloorVibration.Add(7, P7)

        '--> 8%
        'For i = 0 To 34
        '    P8.Add(Hz_P8(i), Kg_P8(i))
        'Next
        P8.listeFrequence = Hz_P8
        P8.listeLigneMasse = Kg_P8
        P8.nbLigne = Hz_P8.Length

        AllFloorVibration.Add(8, P8)

        '--> 9%
        'For i = 0 To 33
        '    P9.Add(Hz_P9(i), Kg_P9(i))
        'Next
        P9.listeFrequence = Hz_P9
        P9.listeLigneMasse = Kg_P9
        P9.nbLigne = Hz_P9.Length

        AllFloorVibration.Add(9, P9)

    End Sub

#End Region

#Region "-- Pourcent 1 --"

    '==> 40 lignes

    Private Hz_P1 As Decimal() =
        {1, 1.261, 1.504, 1.579, 1.752, 1.911, 2, 2.089, 2.246, 2.458,
         2.498, 2.745, 3, 3.251, 3.285, 3.758, 4, 4.253, 4.511, 4.762,
         5, 5.168, 6, 6.52, 6.734, 7, 7.314, 7.498, 8.01, 8.491,
         9, 9.521, 9.659, 10, 12, 13.211, 14, 16, 18, 20}

    Private Kg_P1 As struc_HivossLigne() =
       {New struc_HivossLigne(New Decimal() {426.1, 1727, 6174, 25947, -1, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {491.4, 1966, 7706, 33011, -1, -1}, -1, 0.25),
        New struc_HivossLigne(New Decimal() {734.6, 2910, 12866, 47120, -1, -1}, -1, 0.4),
        New struc_HivossLigne(New Decimal() {1431, 6090, 23855, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {3319, 14615, 52641, -1, -1, -1}, -1, 1.7),
        New struc_HivossLigne(New Decimal() {6170, 24141, 100000, -1, -1, -1}, -1, 3.2),
        New struc_HivossLigne(New Decimal() {7760, 31230, -1, -1, -1, -1}, -1, 3.8),
        New struc_HivossLigne(New Decimal() {6296, 24614, 100000, -1, -1, -1}, -1, 3.2),
        New struc_HivossLigne(New Decimal() {3924, 17077, 61846, -1, -1, -1}, -1, 2),
        New struc_HivossLigne(New Decimal() {1630, 6300, 26222, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {1317, 4752, 20000, 77230, -1, -1}, -1, 0.65),
        New struc_HivossLigne(New Decimal() {610, 2361, 9476, 38455, -1, -1}, -1, 0.3),
        New struc_HivossLigne(New Decimal() {426.3, 1701, 6143, 25696, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {400, 1586, 5639, 23401, 90000, -1}, -1, 0.18),
        New struc_HivossLigne(New Decimal() {429.8, 1701, 6467, 26172, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {1603, 5618, 23837, 91027, -1, -1}, -1, 0.7),
        New struc_HivossLigne(New Decimal() {1450, 5111, 20957, 82714, -1, -1}, -1, 0.65),
        New struc_HivossLigne(New Decimal() {920.5, 3615, 16192, 57966, -1, -1}, -1, 0.48),
        New struc_HivossLigne(New Decimal() {772.2, 3096, 13900, 50000, -1, -1}, -1, 0.4),
        New struc_HivossLigne(New Decimal() {416.4, 1662, 6000, 24798, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {381.7, 1508, 5435, 22352, 86980, -1}, -1, 0.18),
        New struc_HivossLigne(New Decimal() {421.8, 1382, 6332, 25612, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {715.5, 2796, 12037, 45210, -1, -1}, -1, 0.35),
        New struc_HivossLigne(New Decimal() {500, 2000, 8000, 32471, -1, -1}, -1, 0.25),
        New struc_HivossLigne(New Decimal() {385.9, 1511, 6362, 24594, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {272.4, 1079, 4221, 17963, 67143, -1}, -1, 0.16),
        New struc_HivossLigne(New Decimal() {195.2, 786.3, 3147, 12490, 50000, 100000}, -1, 0.1),
        New struc_HivossLigne(New Decimal() {173.9, 652.8, 2548, 10162, 40526, 81042}, -1, -1),
        New struc_HivossLigne(New Decimal() {197.2, 773.5, 3131, 13845, 50000, 100000}, -1, 0.1),
        New struc_HivossLigne(New Decimal() {221.6, 881.5, 3513, 15634, 56589, -1}, -1, 0.12),
        New struc_HivossLigne(New Decimal() {200, 800, 3184, 13998, 50000, -1}, -1, 0.1),
        New struc_HivossLigne(New Decimal() {195.9, 846.7, 3348, 14912, 53805, -1}, -1, 0.11),
        New struc_HivossLigne(New Decimal() {209.9, 784.4, 3108, 13587, 50000, 100000}, -1, 0.1),
        New struc_HivossLigne(New Decimal() {177.5, 678.2, 2635, 10952, 42341, 84351}, -1, -1),
        New struc_HivossLigne(New Decimal() {155.2, 561.4, 2228, 8857, 35903, 67494}, -1, -1),
        New struc_HivossLigne(New Decimal() {100, 400, 1594, 6372, 24565, 50000}, 51.2, -1),
        New struc_HivossLigne(New Decimal() {-1, 322.9, 1292, 4681, 19271, 38327}, 38.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 245.4, 988.5, 3946, 17224, 32234}, 31, -1),
        New struc_HivossLigne(New Decimal() {-1, 187.2, 726.4, 2879, 12430, 23677}, 23, -1),
        New struc_HivossLigne(New Decimal() {-1, 157.5, 584.9, 2285, 9138, 19033}, 18.5, -1)}

#End Region

#Region "-- Pourcent 2 --"

    '==> 36 lignes

    Private Hz_P2 As Decimal() =
        {1, 1.258, 1.505, 1.599, 1.748, 2, 2.251, 2.453, 2.512, 2.737,
         3, 3.252, 3.285, 3.751, 4.25, 4.49, 4.74, 5, 5.59, 6,
         6.488, 6.61, 7.235, 7.494, 8, 8.497, 9, 9.528, 10, 10.64,
         12, 14, 16, 18, 19.74, 20}

    Private Kg_P2 As struc_HivossLigne() =
        {New struc_HivossLigne(New Decimal() {428.4, 1716, 6119, 25997, -1, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {490.2, 1945, 7612, 31156, -1, -1}, -1, 0.25),
        New struc_HivossLigne(New Decimal() {735.3, 2902, 12743, 47123, -1, -1}, -1, 0.4),
        New struc_HivossLigne(New Decimal() {1427.5, 6085, 24135, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {2803, 12077, 45337, -1, -1, -1}, -1, 1.4),
        New struc_HivossLigne(New Decimal() {4928, 20000, 78799, -1, -1, -1}, -1, 2.5),
        New struc_HivossLigne(New Decimal() {3482, 15427, 55761, -1, -1, -1}, -1, 1.8),
        New struc_HivossLigne(New Decimal() {1687, 6532, 25165, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {1261, 4501, 18980, 73722, -1, -1}, -1, 0.6),
        New struc_HivossLigne(New Decimal() {638.4, 2463, 9790, 40000, -1, -1}, -1, 0.31),
        New struc_HivossLigne(New Decimal() {422.6, 1674, 6187, 25636, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {395.4, 1545, 5616, 23381, 90000, -1}, -1, 0.18),
        New struc_HivossLigne(New Decimal() {414.2, 1647, 6145, 25459, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {963.3, 3856, 17047, 62255, -1, -1}, -1, 0.49),
        New struc_HivossLigne(New Decimal() {706.8, 2819, 12061, 45102, -1, -1}, -1, 0.32),
        New struc_HivossLigne(New Decimal() {706.8, 2819, 12161, 45202, -1, -1}, -1, 0.32),
        New struc_HivossLigne(New Decimal() {406.1, 1627, 5867, 24437, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {354.8, 1422, 5064, 20541, 81107, -1}, -1, 0.17),
        New struc_HivossLigne(New Decimal() {417.1, 1674, 6283, 25402, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {464.5, 1868, 7041, 29053, -1, -1}, -1, 0.24),
        New struc_HivossLigne(New Decimal() {455.9, 1829, 6746, 28099, -1, -1}, -1, 0.22),
        New struc_HivossLigne(New Decimal() {400, 1579, 6272, 24988, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {200, 800, 3207, 12942, 50000, 100000}, -1, 0.1),
        New struc_HivossLigne(New Decimal() {153.2, 558.7, 2198, 8729, 35350, 68858}, -1, -1),
        New struc_HivossLigne(New Decimal() {157.1, 575.2, 2277, 9000, 36578, 71279}, -1, -1),
        New struc_HivossLigne(New Decimal() {171.1, 640.3, 2473, 9856, 39433, 79028}, -1, -1),
        New struc_HivossLigne(New Decimal() {145.3, 523.6, 2086, 8271, 33760, 66558}, -1, -1),
        New struc_HivossLigne(New Decimal() {144.1, 510.4, 2059, 8129, 33807, 65723}, -1, -1),
        New struc_HivossLigne(New Decimal() {111.5, 454.2, 1838, 6757, 28090, 54816}, -1, -1),
        New struc_HivossLigne(New Decimal() {100, 431.4, 1741, 6436, 26534, 51316}, 51.2, -1),
        New struc_HivossLigne(New Decimal() {-1, 392.8, 1554, 5596, 23243, 46577}, 45, -1),
        New struc_HivossLigne(New Decimal() {-1, 198.7, 800, 3223, 14205, 26754}, 26, -1),
        New struc_HivossLigne(New Decimal() {-1, 178.2, 683.2, 2675, 11057, 21553}, 21, -1),
        New struc_HivossLigne(New Decimal() {-1, 121.6, 471.9, 1881, 7172, 16000}, 15.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 100, 435.9, 1731, 6396, 14366}, 12.8, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 433.5, 1720, 6248, 14214}, 12.5, -1)}

#End Region

#Region "-- Pourcent 3 --"

    '==> 37 lignes

    Private Hz_P3 As Decimal() =
        {1, 1.254, 1.503, 1.625, 1.754, 2, 2.254, 2.446, 2.503, 2.755,
         3, 3.252, 3.306, 3.501, 3.754, 4.257, 4.487, 4.711, 4.744, 5,
         6, 6.501, 7, 7.194, 7.494, 8, 8.495, 9, 9.502, 11,
         12, 14, 15, 16, 17.487, 18, 20}

    Private Kg_P3 As struc_HivossLigne() =
        {New struc_HivossLigne(New Decimal() {430.2, 1708, 6188, 26072, -1, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {488.8, 1948, 7632, 31044, -1, -1}, -1, 0.25),
        New struc_HivossLigne(New Decimal() {728.3, 2899, 12634, 46755, -1, -1}, -1, 0.4),
        New struc_HivossLigne(New Decimal() {1464, 6075, 24146, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {2397, 9402, 38628, -1, -1, -1}, -1, 1.2),
        New struc_HivossLigne(New Decimal() {3520, 15508, 55842, -1, -1, -1}, -1, 1.75),
        New struc_HivossLigne(New Decimal() {3057, 13446, 48452, -1, -1, -1}, -1, 1.5),
        New struc_HivossLigne(New Decimal() {1572, 6192, 24811, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {1261, 4537, 19263, 74318, -1, -1}, -1, 0.6),
        New struc_HivossLigne(New Decimal() {600, 2341, 9126, 37693, -1, -1}, -1, 0.3),
        New struc_HivossLigne(New Decimal() {421.2, 1681, 6086, 25447, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {386.4, 1547, 5498, 22847, 87792, -1}, -1, 0.18),
        New struc_HivossLigne(New Decimal() {414.9, 1662, 6226, 25232, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {568.3, 2234, 8821, 36370, -1, -1}, -1, 0.28),
        New struc_HivossLigne(New Decimal() {704.9, 2784, 11747, 44645, -1, -1}, -1, 0.35),
        New struc_HivossLigne(New Decimal() {565, 2228, 8754, 36151, -1, -1}, -1, 0.27),
        New struc_HivossLigne(New Decimal() {635.9, 2463, 9778, 39750, -1, -1}, -1, 0.31),
        New struc_HivossLigne(New Decimal() {415.4, 1645, 6320, 25369, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {395.7, 1552, 5556, 23307, 87810, -1}, -1, 0.18),
        New struc_HivossLigne(New Decimal() {342.5, 1366, 4870, 19749, 77852, -1}, -1, 0.16),
        New struc_HivossLigne(New Decimal() {376.2, 1507, 5366, 22038, 85581, -1}, -1, 0.17),
        New struc_HivossLigne(New Decimal() {379.2, 1525, 5378, 22349, 86380, -1}, -1, 0.17),
        New struc_HivossLigne(New Decimal() {237.8, 951.2, 3826, 16858, 60000, -1}, -1, 0.13),
        New struc_HivossLigne(New Decimal() {195.7, 792.4, 3172, 13021, 50000, 100000}, -1, 0.1),
        New struc_HivossLigne(New Decimal() {149.2, 547.9, 2177, 8527, 35281, 68786}, -1, -1),
        New struc_HivossLigne(New Decimal() {124.8, 474.8, 1903, 7260, 29640, 58636}, -1, -1),
        New struc_HivossLigne(New Decimal() {140.4, 496.1, 2000, 8000, 32720, 64335}, -1, -1),
        New struc_HivossLigne(New Decimal() {113.2, 455.4, 1839, 6865, 28466, 55265}, -1, -1),
        New struc_HivossLigne(New Decimal() {100, 434.7, 1745, 6289, 26324, 50429}, 51.2, -1),
        New struc_HivossLigne(New Decimal() {-1, 310.3, 1236, 4788, 19353, 38014}, 38, -1),
        New struc_HivossLigne(New Decimal() {-1, 256, 1011, 4052, 17644, 32586}, 32.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 185.9, 709, 2797, 11925, 23210}, 22.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 167.9, 630.6, 2499, 9905, 20000}, 19.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 153.1, 564.7, 2227, 8722, 18616}, 17.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 100, 429.8, 1711, 6473, 13681}, 12.8, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 400, 1578, 5712, 12403}, 11.5, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 372.8, 1466, 5255, 10998}, 10.5, -1)}

#End Region

#Region "-- Pourcent 4 --"

    '==> 38 lignes

    Private Hz_P4 As Decimal() =
        {1, 1.253, 1.505, 1.658, 1.752, 2, 2.254, 2.422, 2.5, 2.729,
         3, 3.25, 3.352, 3.508, 3.748, 4, 4.253, 4.488, 4.662, 4.735,
         5, 6, 6.498, 7, 7.159, 7.5, 8, 8.5, 8.787, 9,
         9.709, 10, 12, 14, 16, 16.839, 18, 20}

    Private Kg_P4 As struc_HivossLigne() =
        {New struc_HivossLigne(New Decimal() {430.2, 1704, 6210, 25979, -1, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {488.7, 1957, 7617, 30792, -1, -1}, -1, 0.25),
        New struc_HivossLigne(New Decimal() {725.9, 2885, 12618, 46481, -1, -1}, -1, 0.39),
        New struc_HivossLigne(New Decimal() {1449, 6087, 23901, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {2031, 8000, 32825, -1, -1, -1}, -1, 1.05),
        New struc_HivossLigne(New Decimal() {2818, 12030, 45100, -1, -1, -1}, -1, 1.42),
        New struc_HivossLigne(New Decimal() {2533, 10153, 40391, -1, -1, -1}, -1, 1.28),
        New struc_HivossLigne(New Decimal() {1581, 6260, 24885, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {1218, 4547, 19082, 73170, -1, -1}, -1, 0.6),
        New struc_HivossLigne(New Decimal() {634.4, 2488, 10000, 40000, -1, -1}, -1, 0.32),
        New struc_HivossLigne(New Decimal() {422, 1703, 6119, 25547, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {376.9, 1518, 5371, 22273, 85716, -1}, -1, 0.18),
        New struc_HivossLigne(New Decimal() {415, 1664, 6170, 24979, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {478.1, 1924, 7497, 29707, -1, -1}, -1, 0.25),
        New struc_HivossLigne(New Decimal() {584.8, 2308, 9099, 37060, -1, -1}, -1, 0.29),
        New struc_HivossLigne(New Decimal() {484.3, 1950, 7611, 30408, -1, -1}, -1, 0.26),
        New struc_HivossLigne(New Decimal() {478.2, 1927, 7479, 29595, -1, -1}, -1, 0.265),
        New struc_HivossLigne(New Decimal() {535.9, 2143, 8475, 34559, -1, -1}, -1, 0.28),
        New struc_HivossLigne(New Decimal() {460.9, 1656, 6349, 25050, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {374.5, 1494, 5337, 21881, 85015, -1}, -1, 0.18),
        New struc_HivossLigne(New Decimal() {285.5, 1122, 4283, 18402, 69113, -1}, -1, 0.15),
        New struc_HivossLigne(New Decimal() {270.8, 1070, 4174, 18187, 66562, -1}, -1, 0.14),
        New struc_HivossLigne(New Decimal() {302.4, 1192, 4431, 18826, 71447, -1}, -1, 0.15),
        New struc_HivossLigne(New Decimal() {223.5, 900, 3556, 15885, 56624, -1}, -1, 0.12),
        New struc_HivossLigne(New Decimal() {194.3, 763.1, 3036, 13035, 50000, 100000}, -1, 0.1),
        New struc_HivossLigne(New Decimal() {144.1, 520.3, 2088, 8301, 33542, 66755}, -1, -1),
        New struc_HivossLigne(New Decimal() {100, 439.6, 1756, 6396, 26648, 51258}, 51.2, -1),
        New struc_HivossLigne(New Decimal() {110, 454.8, 1821, 6786, 27983, 54597}, -1, -1),
        New struc_HivossLigne(New Decimal() {100, 438.1, 1758, 6453, 26600, 51550}, 51.2, -1),
        New struc_HivossLigne(New Decimal() {-1, 426.4, 1710, 6131, 25655, 49518}, 49, -1),
        New struc_HivossLigne(New Decimal() {-1, 344.1, 1376, 5000, 20000, 40000}, 39, -1),
        New struc_HivossLigne(New Decimal() {-1, 308.3, 1233, 4508, 19066, 36807}, 36.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 211.9, 848.6, 3335, 15064, 27453}, 27, -1),
        New struc_HivossLigne(New Decimal() {-1, 176.9, 666.8, 2634, 10786, 21199}, 20.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 128.1, 483.2, 1960, 7545, 16662}, 14.8, -1),
        New struc_HivossLigne(New Decimal() {-1, 100, 419.6, 1682, 6437, 13235}, 12.8, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 347.8, 1395, 4964, 9923}, 10, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 307.8, 1230, 4498, 9000}, 9, -1)}

#End Region

#Region "-- Pourcent 5 --"

    '==> 37 lignes

    Private Hz_P5 As Decimal() =
        {1, 1.257, 1.505, 1.698, 1.754, 2, 2.255, 2.4, 2.508, 2.75,
         3, 3.255, 3.429, 3.503, 3.752, 4, 4.256, 4.493, 4.6, 4.746,
         5, 6, 6.487, 7, 7.487, 8, 8.485, 9, 9.23, 10,
         12, 14, 15, 16, 16.283, 18, 20}

    Private Kg_P5 As struc_HivossLigne() =
        {New struc_HivossLigne(New Decimal() {429.2, 1709, 6198, 25932, -1, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {488.4, 1945, 7644, 30848, -1, -1}, -1, 0.25),
        New struc_HivossLigne(New Decimal() {718.8, 2856, 12310, 45431, -1, -1}, -1, 0.39),
        New struc_HivossLigne(New Decimal() {1555, 6220, 24797, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {1880, 7119, 29252, -1, -1, -1}, -1, 0.9),
        New struc_HivossLigne(New Decimal() {2357, 9319, 38025, -1, -1, -1}, -1, 1.19),
        New struc_HivossLigne(New Decimal() {2248, 8843, 36168, -1, -1, -1}, -1, 1.1),
        New struc_HivossLigne(New Decimal() {1532, 6218, 24358, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {1155, 4330, 18400, 70000, -1, -1}, -1, 0.58),
        New struc_HivossLigne(New Decimal() {587, 2299, 9107, 37313, -1, -1}, -1, 0.3),
        New struc_HivossLigne(New Decimal() {421.6, 1674, 6000, 25169, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {366.9, 1479, 5250, 21384, 83532, -1}, -1, 0.18),
        New struc_HivossLigne(New Decimal() {421.4, 1700, 6227, 25472, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {447.6, 1811, 6653, 27422, -1, -1}, -1, 0.22),
        New struc_HivossLigne(New Decimal() {483.6, 1928, 7469, 30000, -1, -1}, -1, 0.25),
        New struc_HivossLigne(New Decimal() {439.5, 1772, 6419, 26864, -1, -1}, -1, 0.22),
        New struc_HivossLigne(New Decimal() {445.6, 1792, 6597, 27225, -1, -1}, -1, 0.23),
        New struc_HivossLigne(New Decimal() {469, 1902, 7189, 29309, -1, -1}, -1, 0.24),
        New struc_HivossLigne(New Decimal() {412, 1646, 6253, 25024, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {342.3, 1366, 4963, 20000, 80000, -1}, -1, 0.16),
        New struc_HivossLigne(New Decimal() {263.4, 1056, 4155, 17844, 65882, -1}, -1, 0.14),
        New struc_HivossLigne(New Decimal() {221.6, 885.7, 3510, 15655, 56326, -1}, -1, 0.12),
        New struc_HivossLigne(New Decimal() {244.5, 977.3, 3912, 17282, 62307, -1}, -1, 0.13),
        New struc_HivossLigne(New Decimal() {200, 800, 3222, 14333, 50629, 100000}, -1, 0.1),
        New struc_HivossLigne(New Decimal() {138.9, 500, 2000, 8000, 32118, 63961}, -1, -1),
        New struc_HivossLigne(New Decimal() {100, 419.1, 1671, 6000, 25036, 48001}, 51.2, -1),
        New struc_HivossLigne(New Decimal() {-1, 424.9, 1707, 6127, 25590, 48721}, 47, -1),
        New struc_HivossLigne(New Decimal() {-1, 391.1, 1572, 5586, 22948, 44938}, 44, -1),
        New struc_HivossLigne(New Decimal() {-1, 353, 1413, 5126, 20217, 40503}, 41, -1),
        New struc_HivossLigne(New Decimal() {-1, 251.5, 1000, 4000, 17452, 32162}, 32, -1),
        New struc_HivossLigne(New Decimal() {-1, 192.4, 744.8, 2970, 13093, 24424}, 23.8, -1),
        New struc_HivossLigne(New Decimal() {-1, 168.3, 629.5, 2454, 9630, 19626}, 19, -1),
        New struc_HivossLigne(New Decimal() {-1, 136, 514.2, 2054, 8184, 17305}, 16.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 111.6, 454.4, 1841, 6836, 15273}, 13.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 100, 429.4, 1733, 6503, 14108}, 12.8, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 307.5, 1232, 4547, 9000}, 9, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 265.5, 1055, 4147, 8272}, 8.3, -1)}

#End Region

#Region "-- Pourcent 6 --"

    '==> 36 lignes

    Private Hz_P6 As Decimal() =
        {1, 1.254, 1.505, 1.748, 2, 2.255, 2.362, 2.501, 2.747, 3,
         3.251, 3.505, 3.662, 3.747, 3.794, 4, 4.26, 4.491, 4.701, 5,
         6, 6.497, 6.792, 7, 7.492, 7.842, 8, 8.513, 9, 9.662,
         10, 12, 14, 16, 18, 20}

    Private Kg_P6 As struc_HivossLigne() =
        {New struc_HivossLigne(New Decimal() {428.3, 1709, 6198, 25885, -1, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {488.2, 1940, 7605, 30709, -1, -1}, -1, 0.25),
        New struc_HivossLigne(New Decimal() {707.9, 2832, 12078, 45000, -1, -1}, -1, 0.37),
        New struc_HivossLigne(New Decimal() {1723, 6264, 25864, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {1968, 7820, 31819, -1, -1, -1}, -1, 1),
        New struc_HivossLigne(New Decimal() {1965, 7820, 31801, -1, -1, -1}, -1, 1),
        New struc_HivossLigne(New Decimal() {1509, 6127, 24153, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {1089, 4220, 18062, 67577, -1, -1}, -1, 0.56),
        New struc_HivossLigne(New Decimal() {578.9, 2272, 9000, 36729, -1, -1}, -1, 0.3),
        New struc_HivossLigne(New Decimal() {415.4, 1665, 6000, 25035, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {359.1, 1437, 5131, 20582, 81678, -1}, -1, 0.18),
        New struc_HivossLigne(New Decimal() {414.6, 1669, 6000, 25015, 95024, -1}, -1, 0.195),
        New struc_HivossLigne(New Decimal() {427.6, 1722, 6207, 26079, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {434.3, 1753, 6352, 26685, -1, -1}, -1, 0.205),
        New struc_HivossLigne(New Decimal() {427.8, 1721, 6221, 26087, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {400, 1609, 5702, 23847, 90000, -1}, -1, 0.19),
        New struc_HivossLigne(New Decimal() {404.3, 1604, 5765, 24095, 91659, -1}, -1, 0.192),
        New struc_HivossLigne(New Decimal() {432.9, 1729, 6283, 26305, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {338.1, 1333, 5000, 20000, 80000, -1}, -1, 0.17),
        New struc_HivossLigne(New Decimal() {233.8, 931.9, 3735, 16506, 59334, -1}, -1, 0.12),
        New struc_HivossLigne(New Decimal() {196.3, 778.1, 3113, 13862, 49044, -1}, -1, 0.1),
        New struc_HivossLigne(New Decimal() {214, 851.6, 3388, 15135, 54279, -1}, -1, 0.115),
        New struc_HivossLigne(New Decimal() {196.6, 781.3, 3100, 13758, 50000, 100000}, -1, 0.1),
        New struc_HivossLigne(New Decimal() {189.4, 728.6, 2915, 12874, 46781, 92409}, -1, -1),
        New struc_HivossLigne(New Decimal() {134.3, 489.3, 2000, 8000, 31076, 62524}, -1, -1),
        New struc_HivossLigne(New Decimal() {100, 425.6, 1718, 6424, 26029, 50595}, 51.2, -1),
        New struc_HivossLigne(New Decimal() {-1, 400, 1604, 5719, 24065, 45776}, 47, -1),
        New struc_HivossLigne(New Decimal() {-1, 393.9, 1594, 5646, 23247, 45391}, 45, -1),
        New struc_HivossLigne(New Decimal() {-1, 364.2, 1437, 5152, 20839, 41507}, 41, -1),
        New struc_HivossLigne(New Decimal() {-1, 255.1, 1018, 4044, 17573, 32878}, 32, -1),
        New struc_HivossLigne(New Decimal() {-1, 227.1, 900, 3636, 16032, 29206}, 29, -1),
        New struc_HivossLigne(New Decimal() {-1, 178.8, 683.5, 2668, 11056, 21686}, 21, -1),
        New struc_HivossLigne(New Decimal() {-1, 154.2, 570, 2249, 8821, 18743}, 17.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 100, 434.4, 1750, 6345, 14194}, 12.8, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 279.3, 1120, 4236, 8445}, 8.8, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 240.9, 962.4, 3843, 7593}, 7.6, -1)}

#End Region

#Region "-- Pourcent 7 --"

    '==> 36 lignes

    Private Hz_P7 As Decimal() =
        {1, 1.259, 1.504, 1.751, 1.878, 2, 2.256, 2.315, 2.502, 2.755,
         3, 3.258, 3.509, 3.756, 4, 4.27, 4.488, 4.735, 5, 5.549,
         6, 6.501, 7, 7.418, 7.782, 8, 8.543, 9, 9.419, 10,
         12, 14, 15.52, 16, 18, 20}

    Private Kg_P7 As struc_HivossLigne() =
        {New struc_HivossLigne(New Decimal() {427, 1703, 6204, 25952, -1, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {482.5, 1955, 7636, 30532, -1, -1}, -1, 0.25),
        New struc_HivossLigne(New Decimal() {707.9, 2784, 11840, 43008, -1, -1}, -1, 0.37),
        New struc_HivossLigne(New Decimal() {1582, 5626, 23461, 90000, -1, -1}, -1, 0.72),
        New struc_HivossLigne(New Decimal() {1701, 6232, 25705, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {1816, 6724, 27867, -1, -1, -1}, -1, 0.84),
        New struc_HivossLigne(New Decimal() {1855, 6903, 28517, -1, -1, -1}, -1, 0.86),
        New struc_HivossLigne(New Decimal() {1596, 6248, 25075, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {1000, 4000, 17659, 64737, -1, -1}, -1, 0.52),
        New struc_HivossLigne(New Decimal() {568.1, 2238, 8828, 35998, -1, -1}, -1, 0.29),
        New struc_HivossLigne(New Decimal() {415.8, 1654, 6000, 24927, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {341.1, 1347, 4869, 19804, 78230, -1}, -1, 0.165),
        New struc_HivossLigne(New Decimal() {384.4, 1525, 5450, 22839, 88088, -1}, -1, 0.175),
        New struc_HivossLigne(New Decimal() {389.8, 1567, 5598, 23076, 89292, -1}, -1, 0.18),
        New struc_HivossLigne(New Decimal() {364.1, 1451, 5185, 21025, 83037, -1}, -1, 0.172),
        New struc_HivossLigne(New Decimal() {371.5, 1491, 5328, 21855, 85323, -1}, -1, 0.18),
        New struc_HivossLigne(New Decimal() {394.6, 1570, 5648, 23454, 90000, -1}, -1, 0.185),
        New struc_HivossLigne(New Decimal() {288.3, 1135, 4338, 18554, 69147, -1}, -1, 0.15),
        New struc_HivossLigne(New Decimal() {219.8, 879.8, 3470, 15531, 55915, -1}, -1, 0.12),
        New struc_HivossLigne(New Decimal() {196.5, 774.4, 3095, 13446, 49709, 100000}, -1, 0.1),
        New struc_HivossLigne(New Decimal() {186.2, 711.1, 2832, 11970, 44895, 89235}, -1, -1),
        New struc_HivossLigne(New Decimal() {192.3, 744.7, 3000, 13246, 48268, 95487}, -1, -1),
        New struc_HivossLigne(New Decimal() {179.3, 681, 2655, 10960, 42219, 85054}, -1, -1),
        New struc_HivossLigne(New Decimal() {135.9, 489.1, 2011, 8000, 31598, 61999}, -1, -1),
        New struc_HivossLigne(New Decimal() {100, 425.8, 1709, 6401, 25669, 51345}, 51.2, -1),
        New struc_HivossLigne(New Decimal() {-1, 388.8, 1568, 5606, 23076, 44938}, 45, -1),
        New struc_HivossLigne(New Decimal() {-1, 367, 1459, 5212, 21543, 41893}, 41.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 325.4, 1304, 4722, 19597, 38037}, 37.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 251.7, 1000, 4035, 17670, 32574}, 32.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 212.9, 853, 3386, 15129, 27824}, 27, -1),
        New struc_HivossLigne(New Decimal() {-1, 171, 636.4, 2487, 9777, 20100}, 20, -1),
        New struc_HivossLigne(New Decimal() {-1, 146.1, 530.3, 2111, 8306, 18295}, 16.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 100, 431.3, 1736, 6500, 13990}, 12.8, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 407.6, 1641, 5871, 12890}, 11.8, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 256.3, 1025, 4070, 8098}, 8.2, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 224.6, 912.2, 3582, 7084}, 7.2, -1)}

#End Region

#Region "-- Pourcent 8 --"

    '==> 35 lignes

    Private Hz_P8 As Decimal() =
        {1, 1.254, 1.506, 1.752, 2, 2.183, 2.256, 2.389, 2.507, 2.755,
         3, 3.25, 3.496, 3.753, 4, 4.265, 4.492, 4.759, 5, 5.315,
         6, 6.495, 7, 7.716, 8, 8.509, 9, 9.286, 10, 12,
         14, 15.18, 16, 18, 20}

    Private Kg_P8 As struc_HivossLigne() =
        {New struc_HivossLigne(New Decimal() {429.2, 1707, 6215, 25990, -1, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {483.8, 1957, 7549, 30495, -1, -1}, -1, 0.25),
        New struc_HivossLigne(New Decimal() {700, 2766, 11612, 43922, -1, -1}, -1, 0.36),
        New struc_HivossLigne(New Decimal() {1433, 5069, 20635, 81490, -1, -1}, -1, 0.65),
        New struc_HivossLigne(New Decimal() {1709, 6149, 25663, 99085, -1, -1}, -1, 0.795),
        New struc_HivossLigne(New Decimal() {1725, 6262, 26146, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {1728, 6294, 26345, 100000, -1, -1}, -1, 0.8),
        New struc_HivossLigne(New Decimal() {1241, 4874, 19793, 78744, -1, -1}, -1, 0.64),
        New struc_HivossLigne(New Decimal() {952, 3799, 16907, 61135, -1, -1}, -1, 0.49),
        New struc_HivossLigne(New Decimal() {553.3, 2209, 8628, 35580, -1, -1}, -1, 0.29),
        New struc_HivossLigne(New Decimal() {410.5, 1650, 5861, 24553, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {330.6, 1318, 4767, 19526, 76122, -1}, -1, 0.19),
        New struc_HivossLigne(New Decimal() {354.8, 1424, 5080, 20492, 81205, -1}, -1, 0.165),
        New struc_HivossLigne(New Decimal() {358.5, 1425, 5081, 20766, 81490, -1}, -1, 0.173),
        New struc_HivossLigne(New Decimal() {331.7, 1316, 4759, 19495, 76146, -1}, -1, 0.17),
        New struc_HivossLigne(New Decimal() {334.4, 1338, 4836, 19845, 76775, -1}, -1, 0.168),
        New struc_HivossLigne(New Decimal() {352, 1387, 4922, 20000, 80000, -1}, -1, 0.165),
        New struc_HivossLigne(New Decimal() {251.3, 996.3, 4017, 17617, 64441, -1}, -1, 0.135),
        New struc_HivossLigne(New Decimal() {212.9, 847.9, 3390, 15112, 53921, -1}, -1, 0.11),
        New struc_HivossLigne(New Decimal() {195.6, 781.6, 3099, 13450, 50000, 100000}, -1, 0.1),
        New struc_HivossLigne(New Decimal() {174.3, 668.3, 2566, 10551, 41293, 81802}, -1, -1),
        New struc_HivossLigne(New Decimal() {181.3, 692.8, 2726, 11616, 43694, 86847}, -1, -1),
        New struc_HivossLigne(New Decimal() {166.4, 617.5, 2424, 9584, 38809, 76946}, -1, -1),
        New struc_HivossLigne(New Decimal() {100, 434.7, 1733, 6524, 26146, 51836}, 51.2, -1),
        New struc_HivossLigne(New Decimal() {-1, 388.6, 1534, 5488, 22630, 43922}, 44.7, -1),
        New struc_HivossLigne(New Decimal() {-1, 351.5, 1384, 4960, 20000, 40000}, 39, -1),
        New struc_HivossLigne(New Decimal() {-1, 292.5, 1165, 4395, 18607, 36099}, 34.8, -1),
        New struc_HivossLigne(New Decimal() {-1, 250.2, 1000, 4000, 17497, 33315}, 32, -1),
        New struc_HivossLigne(New Decimal() {-1, 200, 814.9, 3236, 14289, 27357}, 26, -1),
        New struc_HivossLigne(New Decimal() {-1, 160.4, 598.5, 2342, 9259, 19263}, 19, -1),
        New struc_HivossLigne(New Decimal() {-1, 134.6, 495.3, 1977, 7808, 17245}, 15, -1),
        New struc_HivossLigne(New Decimal() {-1, 100, 429.1, 1715, 6518, 13861}, 12.8, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 386.8, 1548, 5495, 11847}, 11, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 245.6, 975.8, 3932, 7748}, 7.9, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 213.6, 818.4, 3393, 6783}, 6.8, -1)}

#End Region

#Region "-- Pourcent 9 --"

    '==> 34 lignes

    Private Hz_P9 As Decimal() =
        {1, 1.247, 1.505, 1.751, 2, 2.254, 2.339, 2.512, 2.786, 2.966,
         3, 3.254, 3.748, 4, 4.245, 4.495, 5, 5.168, 6, 6.472,
         7, 7.24, 7.668, 8, 8.532, 9, 9.534, 10, 12, 14,
         14.87, 16, 18, 20}

    Private Kg_P9 As struc_HivossLigne() =
        {New struc_HivossLigne(New Decimal() {430.6, 1705, 6217, 25899, -1, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {480.9, 1957, 7514, 30000, -1, -1}, -1, 0.25),
        New struc_HivossLigne(New Decimal() {686.1, 2692, 11160, 43009, -1, -1}, -1, 0.36),
        New struc_HivossLigne(New Decimal() {1305, 4709, 19463, 75579, -1, -1}, -1, 0.6),
        New struc_HivossLigne(New Decimal() {1571, 5565, 23121, 88894, -1, -1}, -1, 0.72),
        New struc_HivossLigne(New Decimal() {1587, 5661, 23546, 90000, -1, -1}, -1, 0.73),
        New struc_HivossLigne(New Decimal() {1316, 4961, 20000, 79170, -1, -1}, -1, 0.65),
        New struc_HivossLigne(New Decimal() {910.6, 3633, 16180, 58230, -1, -1}, -1, 0.48),
        New struc_HivossLigne(New Decimal() {515.7, 2068, 8169, 33030, -1, -1}, -1, 0.28),
        New struc_HivossLigne(New Decimal() {424.3, 1693, 6286, 25432, 100000, -1}, -1, 0.2),
        New struc_HivossLigne(New Decimal() {414.9, 1636, 5920, 25432, 94023, -1}, -1, 0.19),
        New struc_HivossLigne(New Decimal() {314.1, 1263, 4628, 19300, 73227, -1}, -1, 0.16),
        New struc_HivossLigne(New Decimal() {333.3, 1329, 4795, 19469, 76706, -1}, -1, 0.17),
        New struc_HivossLigne(New Decimal() {294.8, 1170, 4388, 18613, 69230, -1}, -1, 0.15),
        New struc_HivossLigne(New Decimal() {310.9, 1230, 4550, 19068, 72491, -1}, -1, 0.16),
        New struc_HivossLigne(New Decimal() {306.4, 1230, 4497, 19081, 72613, -1}, -1, 0.16),
        New struc_HivossLigne(New Decimal() {204.2, 824.2, 3274, 14644, 52107, -1}, -1, 0.11),
        New struc_HivossLigne(New Decimal() {195.2, 782.8, 3112, 13572, 49484, 100000}, -1, 0.1),
        New struc_HivossLigne(New Decimal() {168.5, 629.6, 2453, 9658, 38852, 76577}, -1, -1),
        New struc_HivossLigne(New Decimal() {172.2, 656.2, 2521, 9894, 39854, 79597}, -1, -1),
        New struc_HivossLigne(New Decimal() {155.1, 575.4, 2273, 8931, 36523, 71091}, -1, -1),
        New struc_HivossLigne(New Decimal() {134.2, 500, 2013, 8000, 32093, 64154}, -1, -1),
        New struc_HivossLigne(New Decimal() {100, 430.3, 1730, 6487, 26102, 51536}, 51.2, -1),
        New struc_HivossLigne(New Decimal() {-1, 378.9, 1501, 5402, 22049, 43051}, 43, -1),
        New struc_HivossLigne(New Decimal() {-1, 324.7, 1288, 4687, 19378, 38165}, 37.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 270.4, 1076, 4199, 18113, 33636}, 33, -1),
        New struc_HivossLigne(New Decimal() {-1, 225.9, 900, 3561, 15948, 28931}, 28, -1),
        New struc_HivossLigne(New Decimal() {-1, 194.6, 771.6, 3080, 13684, 25706}, 24.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 157.1, 570.9, 2266, 8900, 18820}, 18, -1),
        New struc_HivossLigne(New Decimal() {-1, 126.9, 475.9, 1935, 7424, 16428}, 14.5, -1),
        New struc_HivossLigne(New Decimal() {-1, 100, 427.1, 1709, 6502, 13551}, 12.8, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 367.7, 1467, 5209, 10810}, 10.5, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 235.6, 946.1, 3775, 7487}, 7.6, -1),
        New struc_HivossLigne(New Decimal() {-1, -1, 200, 813.6, 3226, 6426}, 6.4, -1)}

#End Region

#Region "===Application générale de la méthode==="

    Public Sub ApplicationMethode(myBeam As cls_Poutre, lFreqDalle As Boolean)
        '------------------------------------------------------------------------------------------------------------------------
        '   13/08/24 :  Création - V1.00 - POM
        '------------------------------------------------------------------------------------------------------------------------
        '   Gestion globale de l'application de la méthode
        '------------------------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   lFreqDalle  [E] :   Indique si on prend en compte la fréquence propre de la dalle
        '------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim lMixte As Boolean
        Dim AllFloorVibration As New Dictionary(Of Integer, cls_MethodHivoss.strHivossTable)
        Dim MasseProfil As Decimal
        Dim PorteeDalle As Decimal

        Const kPC As Decimal = 100
        Dim MsurfDalle As Decimal

        '--( Initialisations

        lMixte = myBeam.lMixte

        Me.CalculAmortissement()

        Me.Amortissement = myBeam.Hivoss.AmortiTotal_Dtot

        Me.ChargerValeursHivoss(AllFloorVibration)

        myBeam.Modal.Analyse(myBeam, Me.ratioQ, Me.IndexQ)
        Me.Frequence = myBeam.Modal.Frequence

        If (myBeam.NbTravees = 1) And Not Me.lMgenNumerique Then
            Me.MassModale = myBeam.Modal.MassTotal / 2
        Else
            Me.MassModale = myBeam.Modal.MassGeneral
        End If

        ''--[ Prise en compte de la fréquence propre de dalle pour les poutres mixtes:

        If lFreqDalle Then
            MasseProfil = myBeam.Section.ProfilA.Aire * cls_Acier.RHOACIER
            PorteeDalle = myBeam.PorteeDalle
            MsurfDalle = Me.MasseSurfPlancher(myBeam, 1)

            Me.FreqDalle = myBeam.Dalle.FrequenceDalle(myBeam.LongueurTravee(1), PorteeDalle, myBeam.LargeurInfluence, MsurfDalle,
                                                       myBeam.Param.GraviteG, myBeam.Param.lGeneration1)
            Me.FreqPoutre = Me.Frequence
            Me.Frequence = CDec(1 / Math.Sqrt(1 / Me.FreqPoutre ^ 2 + 1 / Me.FreqDalle ^ 2))
        End If

        ''--[ Calcul Hivoss

        myBeam.Hivoss.CalculMethodHivoss(CInt(Me.Amortissement * kPC), Me.Frequence, Me.MassModale, Me.HCategorie, Me.OsRMS)
        Me.indConfort = myBeam.Hivoss.ConfortAssessment(Me.HCategorie)

    End Sub

    Private Function MasseSurfPlancher(myBeam As cls_Poutre, iTravee As Integer) As Decimal
        '------------------------------------------------------------------------------------------------------------------------
        '   05/06/26 :  Création - V1.20 - POM
        '------------------------------------------------------------------------------------------------------------------------
        '   Calcul de la masse surfacique associée au plancher pour un calcul Hivoss
        '------------------------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iTravee     [E] :   Indice de la travée considérée (1 à NbTravees)
        '------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim MassePlancher As Decimal                                ' Masse surfacique du plancher (kg/m²)
        Dim G_PP As StructPoidsPropres = myBeam.ChargeRepartiePP()
        Dim PorteeDalle As Decimal = myBeam.PorteeDalle
        Dim G As Decimal = myBeam.Param.GraviteG
        Dim lMixte As Boolean = myBeam.lMixte
        Dim i As Integer

        '--( Calculs )=====================================================================================

        '--( Masse de poids propre de la dalle et du bac acier répartie sur la portée de la dalle

        MassePlancher = (G_PP.qPP_BacAcier + G_PP.qPP_DalleBeton) / PorteeDalle / G

        '--( Masse associée aux autres charges permanentes

        MassePlancher += myBeam.ChargesU(cls_Poutre.symbG1).QSurf(iTravee) / G
        For i = 1 To myBeam.ChargesU(cls_Poutre.symbG1).FReparties(iTravee).Count - 1
            MassePlancher += myBeam.ChargesU(cls_Poutre.symbG1).FReparties(iTravee)(i).ForceRepMoyenne / G / PorteeDalle
        Next

        If lMixte Then
            MassePlancher += myBeam.ChargesU(cls_Poutre.symbG2).QSurf(iTravee) / G
            For i = 0 To myBeam.ChargesU(cls_Poutre.symbG2).FReparties(iTravee).Count - 1
                MassePlancher += myBeam.ChargesU(cls_Poutre.symbG2).FReparties(iTravee)(i).ForceRepMoyenne / G / PorteeDalle
            Next
        End If

        '--( Masse associée aux charges d'exploitation

        Dim symbQ As String = ""
        Dim kHivoss As Decimal = myBeam.Hivoss.ratioQ
        Select Case myBeam.Hivoss.choixQ
            Case Enu_Q.Q1 : symbQ = cls_Poutre.symbQ1
            Case Enu_Q.Q2 : symbQ = cls_Poutre.symbQ2
        End Select

        MassePlancher += kHivoss * myBeam.ChargesU(symbQ).QSurf(iTravee) / G
        For i = 0 To myBeam.ChargesU(symbQ).FReparties(iTravee).Count - 1
            MassePlancher += myBeam.ChargesU(symbQ).FReparties(iTravee)(i).ForceRepMoyenne / G / PorteeDalle
        Next

        Return MassePlancher

    End Function



#End Region

#Region "===CALCUL du OS RMS==="

    Public Sub CalculMethodHivoss(ByVal Pourcent As Integer, ByVal MyFreq As Decimal, ByVal MyMasse As Decimal, ByRef ResultText As String, ByRef ResultVal As Decimal)
        '---------------------------------------------------------------------------------------------------------------
        '   Création BD - 12/08/19
        '---------------------------------------------------------------------------------------------------------------
        '   [E] : AllFloorVibration - Table contenant les courbes Hoivoss
        '   [E] : Pourcent - Damping ratio (de 1 à 9%)
        '   [E] : MyFreq - Eigenfrequency of the floor (de 1 à 20 Hz) 
        '   [E] : Masse - Modal mass of the floor (de 100 à 100000 kg)
        '   [S] : ResultText - Texte de la catégorie (!/F/E/D/C/B/A)
        '   [S] : ResultVal - Valeur calculée (de 51.2 à 0.1) 
        '---------------------------------------------------------------------------------------------------------------

        '--> Déclaration
        Dim ligneHz As Integer
        Dim v, v1, v2, a, a1, a2 As Decimal
        Dim colonneKg As Integer
        Dim Frequence As Decimal
        Dim Masse As Decimal
        Dim AllFloorVibration As New Dictionary(Of Integer, strHivossTable)

        '--> Initialisations

        Me.ChargerValeursHivoss(AllFloorVibration)

        '---/ On contrôle que la fréquence et la masse modale sont à l'interieur des limites des courbes

        If MyFreq > AllFloorVibration(Pourcent).listeFrequence(AllFloorVibration(Pourcent).nbLigne - 1) Then
            Frequence = AllFloorVibration(Pourcent).listeFrequence(AllFloorVibration(Pourcent).nbLigne - 1)
        ElseIf MyFreq < AllFloorVibration(Pourcent).listeFrequence(0) Then
            Frequence = AllFloorVibration(Pourcent).listeFrequence(0)
        Else
            Frequence = MyFreq
        End If

        If MyMasse > MASSMODMAX Then
            Masse = MASSMODMAX
        ElseIf MyMasse < MASSMODMIN Then
            Masse = MASSMODMIN
        Else
            Masse = MyMasse
        End If

        '--> Recherche de l'intervalle Hz du point donné 

        'While ligneHz < AllFloorVibration(Pourcent).nbLigne And HZ > AllFloorVibration(Pourcent).Keys(ligneHz)
        While ligneHz < AllFloorVibration(Pourcent).nbLigne And Frequence > AllFloorVibration(Pourcent).listeFrequence(ligneHz)
            ligneHz += 1
        End While

        'If HZ = AllFloorVibration(Pourcent).Keys(ligneHz) Then  '===> Le point se situe sur une ligne Hz de la BDD <=================================
        If Frequence = AllFloorVibration(Pourcent).listeFrequence(ligneHz) Then  '===> Le point se situe sur une ligne Hz de la BDD <=================================

            Dim lgauche As Boolean = False
            Dim ldroite As Boolean = False

            '--> Calcul le nombre de -1 au début de la ligne
            Dim nbMoins1Debut As Integer
            'While nbMoins1Debut < 6 And AllFloorVibration(Pourcent).Item(HZ).valeurs(nbMoins1Debut) = -1
            While nbMoins1Debut < 6 And AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valeurs(nbMoins1Debut) = -1
                nbMoins1Debut += 1
            End While

            '--> Calcul le nombre de -1 à la fin de la ligne
            Dim nbMoins1Fin As Integer
            Dim p1 As Integer = 5

            'While p1 >= 0 And AllFloorVibration(Pourcent).Item(HZ).valeurs(p1) = -1
            While p1 >= 0 And AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valeurs(p1) = -1
                nbMoins1Fin += 1
                p1 -= 1
            End While

            '--> Recherche de l'intervalle Kg du point donné entre les frontières
            For y As Integer = 0 To 5
                'If KG > AllFloorVibration(Pourcent).Item(HZ).valeurs(y) And AllFloorVibration(Pourcent).Item(HZ).valeurs(y) <> -1 Then
                If Masse > AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valeurs(y) And AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valeurs(y) <> -1 Then
                    colonneKg = y + 1
                End If
            Next

            If colonneKg = 0 Then '--> Ajout des -1 du début s'il y en a + Utilisation de valGauche
                lgauche = True
                colonneKg = nbMoins1Debut
            ElseIf colonneKg + nbMoins1Fin = 6 Then '--> Utilisation de valDroite
                ldroite = True
            End If

            If colonneKg = 0 Then '--> Valeur supérieur au premier intervalle (51.2)
                ResultText = pLettresIntervalles(colonneKg)
                ResultVal = pLimitesIntervalles(colonneKg)

            ElseIf colonneKg = 6 Then '--> Valeur inférieur au dernier intervalle (0.1)

                ResultText = pLettresIntervalles(colonneKg)
                ResultVal = pLimitesIntervalles(colonneKg - 1)

                'ElseIf KG = AllFloorVibration(Pourcent).Item(HZ).valeurs(colonneKg) Then  '--> Masse donnée = Masse de la frontière
            ElseIf Masse = AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valeurs(colonneKg) Then  '--> Masse donnée = Masse de la frontière

                ResultText = pLettresIntervalles(colonneKg) & " & " & pLettresIntervalles(colonneKg + 1)
                ResultVal = pLimitesIntervalles(colonneKg)

            Else  '--> Calcul par interpolation de la valeur

                If lgauche Then
                    a1 = Math.Log10(100)
                    'a2 = Math.Log10(AllFloorVibration(Pourcent).Item(HZ).valeurs(colonneKg))
                    a2 = Math.Log10(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valeurs(colonneKg))
                    a = Math.Log10(Masse)
                    'v1 = Math.Log10(AllFloorVibration(Pourcent).Item(HZ).valGauche)
                    v1 = Math.Log10(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valGauche)
                    v2 = Math.Log10(pLimitesIntervalles(colonneKg))
                ElseIf ldroite Then
                    'a1 = Math.Log10(AllFloorVibration(Pourcent).Item(HZ).valeurs(colonneKg - 1))
                    a1 = Math.Log10(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valeurs(colonneKg - 1))
                    a2 = Math.Log10(100000)
                    a = Math.Log10(Masse)
                    v1 = Math.Log10(pLimitesIntervalles(colonneKg - 1))
                    'v2 = Math.Log10(AllFloorVibration(Pourcent).Item(HZ).valDroite)
                    v2 = Math.Log10(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valDroite)
                Else
                    'a1 = Math.Log10(AllFloorVibration(Pourcent).Item(HZ).valeurs(colonneKg - 1))
                    a1 = Math.Log10(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valeurs(colonneKg - 1))
                    'a2 = Math.Log10(AllFloorVibration(Pourcent).Item(HZ).valeurs(colonneKg))
                    a2 = Math.Log10(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valeurs(colonneKg))
                    a = Math.Log10(Masse)
                    v1 = Math.Log10(pLimitesIntervalles(colonneKg - 1))
                    v2 = Math.Log10(pLimitesIntervalles(colonneKg))
                End If

                v = 10 ^ (v2 + (v1 - v2) / (a1 - a2) * (a - a2))

                ResultText = pLettresIntervalles(colonneKg)
                ResultVal = v

            End If

        Else '===> Le point se situe entre deux lignes <=============================================================================================

            '--> Déclaration
            Dim Freq1, Freq2 As Decimal
            Dim frontiereKgHaut As New List(Of Decimal)
            Dim frontiereKgBas As New List(Of Decimal)
            Dim frontiereKgVal As New List(Of Decimal)
            Dim nbMoins1DebutBas, nbMoins1DebutHaut, nbMoins1 As Integer

            '--> Fréquence en haut et en bas de la valeur donnée
            'HZ1 = AllFloorVibration(Pourcent).Keys(ligneHz - 1)
            'HZ2 = AllFloorVibration(Pourcent).Keys(ligneHz)
            Freq1 = AllFloorVibration(Pourcent).listeFrequence(ligneHz - 1)
            Freq2 = AllFloorVibration(Pourcent).listeFrequence(ligneHz)

            '--> Récupération des frontières des lignes en haut et en bas de la valeur donnée
            For i As Integer = 0 To 5
                'frontiereKgBas.Add(AllFloorVibration(Pourcent).Item(HZ1).valeurs(i))
                'frontiereKgHaut.Add(AllFloorVibration(Pourcent).Item(HZ2).valeurs(i))
                frontiereKgBas.Add(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz - 1).valeurs(i))
                frontiereKgHaut.Add(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valeurs(i))
            Next

            '--> Calcul le nombre de -1 maximum au début des deux listes
            While nbMoins1DebutBas < 6 And frontiereKgBas(nbMoins1DebutBas) = -1
                nbMoins1DebutBas += 1
            End While
            While nbMoins1DebutHaut < 6 And frontiereKgHaut(nbMoins1DebutHaut) = -1
                nbMoins1DebutHaut += 1
            End While
            nbMoins1 = Math.Max(nbMoins1DebutBas, nbMoins1DebutHaut)

            '--> Calcul des frontières de la ligne de la valeur donnée - Interpolation entre ligne haut et bas
            For i As Integer = 0 To 5

                If (frontiereKgBas(i) = -1 Or frontiereKgHaut(i) = -1) And nbMoins1 >= i + 1 Then '--> -1 du début
                    v1 = Math.Log10(100)
                    v2 = Math.Log10(100)
                ElseIf (frontiereKgBas(i) = -1 Or frontiereKgHaut(i) = -1) Then '--> -1 de la fin
                    v1 = Math.Log10(100000)
                    v2 = Math.Log10(100000)
                Else '--> Valeur classique
                    v1 = Math.Log10(frontiereKgBas(i))
                    v2 = Math.Log10(frontiereKgHaut(i))
                End If

                a = Math.Log10(Frequence)
                a1 = Math.Log10(Freq1)
                a2 = Math.Log10(Freq2)

                v = 10 ^ (v2 + (v1 - v2) / (a1 - a2) * (a - a2))

                '--> Ajout du résultat à la liste
                frontiereKgVal.Add(v)

            Next

            '--> Recherche de l'intervalle de la valeur donné entre les frontières
            While Masse >= frontiereKgVal(colonneKg) And colonneKg < 5
                colonneKg += 1
            End While

            If colonneKg = 0 Then  '--> Valeur supérieur au premier intervalle (51.2)

                ResultText = pLettresIntervalles(colonneKg)
                ResultVal = pLimitesIntervalles(colonneKg)

            ElseIf colonneKg = 5 And Masse > frontiereKgVal(5) Then  '--> Valeur inférieur au dernier intervalle (0.1)

                ResultText = pLettresIntervalles(colonneKg + 1)
                ResultVal = pLimitesIntervalles(colonneKg)

            ElseIf frontiereKgVal(colonneKg - 1) = 100000 And frontiereKgVal(colonneKg) = 100000 Then

                '--> Cas particulier lorsque la valeur recherchée est de 100 000 Kg et qu'il y a plus d'un -1 à la fin de la liste des frontières

                Dim i1, i2, y1, y2, y As Decimal
                'i1 = Math.Log10(AllFloorVibration(Pourcent).Item(HZ1).valDroite)
                'i2 = Math.Log10(AllFloorVibration(Pourcent).Item(HZ2).valDroite)
                i1 = Math.Log10(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz - 1).valDroite)
                i2 = Math.Log10(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valDroite)
                y = Math.Log10(Frequence)
                y1 = Math.Log10(Freq1)
                y2 = Math.Log10(Freq2)
                v = 10 ^ (i2 + (i1 - i2) / (y1 - y2) * (y - y2))

                ResultText = pLettresIntervalles(colonneKg - 1)
                ResultVal = v

            Else '--> Calcul par interpolation de la valeur

                If frontiereKgVal(colonneKg - 1) = 100 Then

                    '--> Calcul de la valeur Gauche entre les deux lignes - Interpolation entre valGauche en haut et valGauche en bas

                    Dim i1, i2, y1, y2, y As Decimal
                    'i1 = Math.Log10(AllFloorVibration(Pourcent).Item(HZ1).valGauche)
                    'i2 = Math.Log10(AllFloorVibration(Pourcent).Item(HZ2).valGauche)
                    i1 = Math.Log10(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz - 1).valGauche)
                    i2 = Math.Log10(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valGauche)
                    y = Math.Log10(Frequence)
                    y1 = Math.Log10(Freq1)
                    y2 = Math.Log10(Freq2)
                    v1 = (i2 + (i1 - i2) / (y1 - y2) * (y - y2))

                Else

                    v1 = Math.Log10(pLimitesIntervalles(colonneKg - 1))

                End If

                If frontiereKgVal(colonneKg) = 100000 Then

                    '--> Calcul de la valeur Droite entre les deux lignes - Interpolation entre valDroite en haut et valDroite en bas

                    Dim i1, i2, y1, y2, y As Decimal
                    'i1 = Math.Log10(AllFloorVibration(Pourcent).Item(HZ1).valDroite)
                    'i2 = Math.Log10(AllFloorVibration(Pourcent).Item(HZ2).valDroite)
                    i1 = Math.Log10(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz - 1).valDroite)
                    i2 = Math.Log10(AllFloorVibration(Pourcent).listeLigneMasse(ligneHz).valDroite)
                    y = Math.Log10(Frequence)
                    y1 = Math.Log10(Freq1)
                    y2 = Math.Log10(Freq2)
                    v2 = (i2 + (i1 - i2) / (y1 - y2) * (y - y2))

                Else

                    v2 = Math.Log10(pLimitesIntervalles(colonneKg))

                End If

                a = Math.Log10(Masse)
                a1 = Math.Log10(frontiereKgVal(colonneKg - 1))
                a2 = Math.Log10(frontiereKgVal(colonneKg))

                v = 10 ^ (v2 + (v1 - v2) / (a1 - a2) * (a - a2))

                ResultText = pLettresIntervalles(colonneKg)
                ResultVal = v

            End If

        End If

    End Sub

    Public Function ConfortAssessment(ByVal HResult As String) As Integer
        '-------------------------------------------------------------------------------------------------
        '   01/12/23 :  Création - POM - V1
        '-------------------------------------------------------------------------------------------------
        '   Renvoie le critère de confort
        '-------------------------------------------------------------------------------------------------
        '   HResult     [E] :   Classe de perception suivant les courbes Hivoss
        '
        '   Paramètre retourné :    0 pour recommended
        '                           1 pour critical
        '                           2 pour not recommended
        '-------------------------------------------------------------------------------------------------

        '--> Declaration

        Dim LimitRecommended() As String = {"A", "C", "C", "D", "D", "D", "D", "D", "E", "E"}
        Dim LimitCritical() As String = {"B", "D", "D", "E", "E", "E", "E", "E", "F", "F"}

        Dim Indice As Integer = Me.IndUsage(Me.UtilisationPlancher)

        '--> Traitement

        If HResult <= LimitRecommended(Indice) Then
            Return 0
        ElseIf HResult <= LimitCritical(Indice) Then
            Return 1
        Else
            Return 2
        End If

    End Function

    Private Function IndUsage(Usage As Enu_UtilisationPlancher) As Integer
        '----------------------------------------------------------------------------------------
        '   01/12/23 :  Création - POM - V1
        '----------------------------------------------------------------------------------------
        '   Attribut pour chaque usage un indice
        '----------------------------------------------------------------------------------------
        '----------------------------------------------------------------------------------------

        Dim Indice As Integer = -1

        Select Case Usage
            Case Enu_UtilisationPlancher.ZoneSensible : Indice = 0
            Case Enu_UtilisationPlancher.Sante : Indice = 1
            Case Enu_UtilisationPlancher.Education : Indice = 2
            Case Enu_UtilisationPlancher.Residentiel : Indice = 3
            Case Enu_UtilisationPlancher.Bureau : Indice = 4
            Case Enu_UtilisationPlancher.Reunion : Indice = 5
            Case Enu_UtilisationPlancher.MaisonRetraite : Indice = 6
            Case Enu_UtilisationPlancher.Hotel : Indice = 7
            Case Enu_UtilisationPlancher.Industriel : Indice = 8
            Case Enu_UtilisationPlancher.Sports : Indice = 9
        End Select

        Return Indice

    End Function

#End Region

#Region " Outils pour les routines dessins "

    Public Sub RecupereDonnees(ByVal MyDamp As Integer, ByVal nbPoints As Integer, ByRef DonneeX As List(Of Decimal()), ByRef DonneeY() As Decimal)
        '--------------------------------------------------------------------------------------------------
        '   04/12/23 :  Création - Version 1.00
        '--------------------------------------------------------------------------------------------------
        '   Récupération des points à tracer pour le diagramme (en fonction de l'amortissement)
        '--------------------------------------------------------------------------------------------------
        '   MyDamp          [E] :   Amortissement
        '   nbPoints        [E] :   Nombre de points dans les courbes
        '   DonneeX         [S] :   Liste des abscisses
        '   DonneeY         [S] :   Liste des ordonnées
        '--------------------------------------------------------------------------------------------------
        Select Case MyDamp
            Case 1
                TransfertTable(P1, nbPoints, DonneeX, DonneeY)
            Case 2
                TransfertTable(P2, nbPoints, DonneeX, DonneeY)
            Case 3
                TransfertTable(P3, nbPoints, DonneeX, DonneeY)
            Case 4
                TransfertTable(P4, nbPoints, DonneeX, DonneeY)
            Case 5
                TransfertTable(P5, nbPoints, DonneeX, DonneeY)
            Case 6
                TransfertTable(P6, nbPoints, DonneeX, DonneeY)
            Case 7
                TransfertTable(P7, nbPoints, DonneeX, DonneeY)
            Case 8
                TransfertTable(P8, nbPoints, DonneeX, DonneeY)
            Case 9
                TransfertTable(P9, nbPoints, DonneeX, DonneeY)
        End Select

    End Sub
    Private Sub TransfertTable(ByVal MyP As strHivossTable, ByVal nbPoints As Integer, ByRef DonneeX As List(Of Decimal()), ByRef DonneeY() As Decimal)

        Dim i As Integer

        For i = 0 To nbPoints - 1
            DonneeY(i) = MyP.listeFrequence(i)
            DonneeX.Add(MyP.listeLigneMasse(i).valeurs.Clone)
        Next

    End Sub


#End Region

End Class
