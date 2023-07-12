Public Class cls_HivossParam

#Region "Enum"
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

#Region "Variables"

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

#End Region
#Region "Constructeur"
    Sub New()
        Me.lHivossMethod = True
        Me.ratioQ = 0.1
        Me.choixQ = Enu_Q.Q1
        Me.UtilisationPlancher = Enu_UtilisationPlancher.Bureau
        Me.Mobilier = Enu_Mobiliers.BureauAvecCloison
        lFauxPlafond = False
        lChappeFlottante = False

        CalculAmortissement()

    End Sub
#End Region

#Region "Calculs Amortissement"
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

#Region "Fonction de copie"
    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

#End Region

End Class
