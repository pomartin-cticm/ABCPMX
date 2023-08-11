Public Class Cls_Prop_Elastique


    ' A SUPPRIMER ? POM

#Region " Charges d'exploitation "

    ''' <summary>
    ''' Coefficient d'equivalence pour les charges d'exploitation
    ''' </summary> 
    Public CE_n_L As Decimal

#End Region

#Region " Commun à Retrait et Charges permanentes"

    ''' <summary>
    ''' Humidité relative de l'environnement ambiant en %
    ''' </summary> 
    Public RH As Decimal

    ''' <summary>
    ''' Type de définition du paramètre t
    ''' </summary>
    Public type_def_t As Enum_Definition_t

    ''' <summary>
    ''' Durée de vie en jour
    ''' </summary> 
    Public t As Decimal

    ''' <summary>
    ''' Rayon moyen de l'élément
    ''' </summary> 
    Public h_0 As Decimal

#End Region

#Region " Retrait "

    ''' <summary>
    ''' Age au moment du chargement pour le retrait en jour
    ''' </summary> 
    Public R_t_0 As Decimal

    ''' <summary>
    ''' Coefficient d'equivalence pour le retrait
    ''' </summary> 
    Public R_n_L As Decimal

#End Region

#Region " Charges permanentes "

    ''' <summary>
    ''' Age au moment du chargement pour les charges permanentes en jour
    ''' </summary> 
    Public CP_t_0 As Decimal

    ''' <summary>
    ''' Coefficient d'equivalence pour les charges permanentes
    ''' </summary> 
    Public CP_n_L As Decimal

#End Region

#Region " Enumérations "

    Public Enum Enum_Definition_t
        CinquanteAns
        Infini
        Utilisateur
    End Enum

#End Region

#Region " Constructeur "

    Sub New()

        Me.RH = 50

        Me.type_def_t = Enum_Definition_t.CinquanteAns
        Me.t = 18250

        Me.R_t_0 = 1
        Me.CP_t_0 = 30

    End Sub

#End Region

#Region " Fonction de calcul "

    ''' <summary>
    ''' Calcul du coefficient d'équivalence acier-béton
    ''' </summary>
    ''' <param name="Module_Young">Valeur en Pa</param>
    ''' <param name="f_cm">Valeur en Pa</param>
    ''' <param name="E_cm">Valeur en Pa</param>
    Public Sub Calcul_Coeff(ByVal Module_Young As Decimal, ByVal f_cm As Decimal, ByVal E_cm As Decimal)

        Dim local_f_cm As Decimal = f_cm / 10 ^ 6
        Dim local_h_0 As Decimal = h_0 * 1000

        '--> Multiplicateur de fluage
        Const PSI_L_CP As Decimal = 1.1   'pour les charges permanentes
        Const PSI_L_R As Decimal = 0.55   'pour le retrait
        'inutilisé --> Const PSI_L_CE As Decimal = 0     'pour les charges d'exploitation

        '--> Coefficient d’équivalence à court terme
        Dim n_0 As Decimal = Module_Young / E_cm

        '######################################################################
        '### Calcul pour les charges d'exploitation ###########################
        '######################################################################

        CE_n_L = n_0

        '######################################################################
        '### Calcul commun ####################################################
        '######################################################################

        'coefficient dépendant de l'humidité relative et du rayon moyen de l'élément
        Dim Beta_H As Decimal
        'facteur tenant compte de l’influence de l’humidité relative
        Dim Phi_RH As Decimal

        If local_f_cm <= 35 Then 'f_cm <= 35 MPa

            Beta_H = Math.Min(1.5 * (1 + (3 * RH / 250) ^ 18) * local_h_0 + 250, 1500)

            Phi_RH = 1 + (1 - RH / 100) / (0.1 * local_h_0 ^ (1 / 3))

        Else                        'f_cm > 35 MPa

            Dim a_1 As Decimal = (35 / local_f_cm) ^ 0.7
            Dim a_2 As Decimal = (35 / local_f_cm) ^ 0.2
            Dim a_3 As Decimal = (35 / local_f_cm) ^ 0.5

            Beta_H = Math.Min(1.5 * (1 + (3 * RH / 250) ^ 18) * local_h_0 + 250 * a_3, 1500 * a_3)

            Phi_RH = a_2 * (1 + a_1 * ((1 - RH / 100) / (0.1 * local_h_0 ^ (1 / 3))))

        End If

        ' facteur tenant compte de l’influence de la résistance du béton sur le coefficient de fluage conventionnel
        Dim Beta_f_cm As Decimal = 16.8 / Math.Sqrt(local_f_cm)

        '######################################################################
        '### Calcul pour les charges permantantes #############################
        '######################################################################

        'coefficient qui rend compte du développement du fluage avec le temps après chargement
        Dim Beta_c_t_t_0 As Decimal
        If type_def_t = Enum_Definition_t.Infini Then
            Beta_c_t_t_0 = 1
        Else
            Beta_c_t_t_0 = ((t - CP_t_0) / (Beta_H + t - CP_t_0)) ^ 0.3
        End If

        'facteur tenant compte de l’influence de l’âge du béton au moment du chargement sur le coefficient de fluage conventionnel
        Dim Beta_t_0 As Decimal = 1 / (1 + CP_t_0 ^ 0.2)

        ' coefficient de fluage conventionnel
        Dim phi_0 As Decimal = Phi_RH * Beta_f_cm * Beta_t_0

        ' coefficient de fluage, en fonction de l’âge t du béton au moment considéré et de l’âge t_0 au moment du chargement 
        Dim phi_t As Decimal = phi_0 * Beta_c_t_t_0

        CP_n_L = n_0 * (1 + PSI_L_CP * phi_t)

        '######################################################################
        '### Calcul pour le retrait ###########################################
        '######################################################################

        'coefficient qui rend compte du développement du fluage avec le temps après chargement
        If type_def_t = Enum_Definition_t.Infini Then
            Beta_c_t_t_0 = 1
        Else
            Beta_c_t_t_0 = ((t - R_t_0) / (Beta_H + t - R_t_0)) ^ 0.3
        End If

        'facteur tenant compte de l’influence de l’âge du béton au moment du chargement sur le coefficient de fluage conventionnel
        Beta_t_0 = 1 / (1 + R_t_0 ^ 0.2)

        ' coefficient de fluage conventionnel
        phi_0 = Phi_RH * Beta_f_cm * Beta_t_0

        ' coefficient de fluage, en fonction de l’âge t du béton au moment considéré et de l’âge t_0 au moment du chargement 
        phi_t = phi_0 * Beta_c_t_t_0

        R_n_L = n_0 * (1 + PSI_L_R * phi_t)

    End Sub

#End Region

#Region " Fonction de copie "

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

#End Region

End Class
