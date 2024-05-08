Public Class cls_OptionsFeu

    '### CLASSE POUR GERER LES OPTIONS DU CALCUL à L'INCENDIE ###################################

#Region " Attributs "

    Private DeltaTsimple As Decimal                         ' Incrément de temps utilisé pour le calcul d'échauffement des structures acier non protégées
    Private DeltaTprotege As Decimal                        ' Incrément de temps utilisé pour le calcul d'échauffement des structures acier protégées
    Public TempRef As Decimal                               ' Température de référence (à t = 0)
    Public Const TempMax As Decimal = 1200                  ' Température max (°)

    Public EmissivityFire As Decimal                        ' Emissivité du feu
    'Public EmissivitySteel As Decimal                        ' Emissivité de l'acier

    Public ConvectionCoef As Decimal                        ' Coefficient de convection sur les faces exposées au feu
    Public ConvectionCoefDalle As Decimal                   ' Coefficient de convection sur la face supérieure de la dalle, non exposée au feu (uniquement si calcul EF Dalle)

    Public PhiViewFactor As Decimal                         ' Facteur de vue

    'Public lHeatingSlabEF As Boolean                        ' Indique si on calcul l'échauffement de la dalle par calcul numérique

    Public ksh As Decimal                                   ' coefficient correcteur pour l'effet masque

    Public AlphaSlab As Decimal                             ' Coefficient de pondération pour la résistance plastique en compression de la dalle
    Public lArmaCompression As Boolean                      ' Indique si on prend en compte les armatures comprimées (dans l'enrobage partiel)
    Public lArmaFormeeAFroid As Boolean                     ' Indique si les armatures pour le béton sont formées à froid (True) ou non (False)

    Public lCalcuFeu As Boolean                             ' Indique si on effectue le calcul au feu

    Public lDalleFEM As Boolean                             ' Indique pour les poutres mixtes si on calcule l'échauffement par une analyse EF
    Public tDalleEFmax As Decimal                           ' Epaisseur maximale d'un elt dalle pour le calcul numérique de l'échauffement de la dalle

    Public Const BOLTZMANN As Decimal = 5.67 * 10 ^ (-8)    ' Constante de Boltzmann

    Public lReductionConcreteStrength As Boolean            ' Indique si on réduit la résistance du béton armé pour T<250° (True) ou non (False)

    Public TypeSurface As enu_TypeSurface                   ' Type de surface (protégée ou non, galvanisée ou non)

    Public Enum enu_TypeSurface
        Protege
        AcierNu
        Galvanise
    End Enum

    Public Protection As enu_TypeProtection                 ' Type de protection thermique
    Public EpProtection As Decimal                          ' Epaisseur de protection thermique
    Public CustomLambdaP As Decimal                         ' Conductivité thermique définie par l'utilisateur (dans le cas de la peinture intumescente)

    Public Enum enu_TypeProtection
        LowDensitySpray_Mineral
        LowDensitySpray_Vermiculite
        HighDensitySpray_PerliteCement
        HighDensitySpray_PerlitePlaster
        IntumescentPaint
        BoardsVermiculite
        BoardsSilicate
        BoardsFibroCement
        BoardsPlaster
    End Enum

#End Region

#Region " Constructeur "

    Public Sub New()

        Me.TempRef = 20                     ' [°C]
        Me.DeltaTsimple = 5                 ' [secondes]
        Me.DeltaTprotege = 10               ' [secondes]

        Me.EmissivityFire = 1.0
        'Me.EmissivitySteel = 0.7

        Me.ConvectionCoef = 25              ' [W/m2K]
        Me.ConvectionCoefDalle = 4          ' [W/m2K]

        'Me.lHeatingSlabEF = False

        Me.PhiViewFactor = 1.0

        Me.tDalleEFmax = 0.01               ' 10 mm

        Me.ksh = 1

        Me.AlphaSlab = 1

        Me.lCalcuFeu = True
        Me.lArmaCompression = True
        Me.lArmaFormeeAFroid = True

        Me.lDalleFEM = False
        Me.tDalleEFmax = 1 / 1000 ' 1 mm

        Me.TypeSurface = enu_TypeSurface.AcierNu

        'Me.BOLTZMANN = 5.67 * 10 ^ (-8)

        Me.lReductionConcreteStrength = False

        Me.EpProtection = 0.1

    End Sub

#End Region

#Region " Fonctions "

    Public Property DeltaTCalcul As Decimal
        '------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------
        '   Renvoie le pas de temps du calcul d'échauffement en fct du type de surface
        '------------------------------------------------------------------------------------------------------------
        Get
            Dim myDeltaT As Decimal
            Select Case Me.TypeSurface
                Case enu_TypeSurface.AcierNu, enu_TypeSurface.Galvanise
                    myDeltaT = Me.DeltaTsimple
                Case enu_TypeSurface.Protege
                    myDeltaT = Me.DeltaTprotege
            End Select
            Return myDeltaT
        End Get

        Set(value As Decimal)
            Select Case Me.TypeSurface
                Case enu_TypeSurface.AcierNu, enu_TypeSurface.Galvanise
                    Me.DeltaTsimple = value
                Case enu_TypeSurface.Protege
                    Me.DeltaTprotege = value
            End Select
        End Set

    End Property

    Public Function lProtectionBoard() As Boolean
        '-------------------------------------------------------------------------------------------------------
        '   01/05/24 :  Création - POM
        '-------------------------------------------------------------------------------------------------------
        '   Indique si la protection est de type par panneaux
        '-------------------------------------------------------------------------------------------------------
        '-------------------------------------------------------------------------------------------------------

        Dim lBoard As Boolean

        lBoard = (Me.Protection = enu_TypeProtection.BoardsFibroCement) _
              Or (Me.Protection = enu_TypeProtection.BoardsPlaster) _
              Or (Me.Protection = enu_TypeProtection.BoardsVermiculite) _
              Or (Me.Protection = enu_TypeProtection.BoardsSilicate)



        Return lBoard
    End Function



#End Region

#Region " Propriétés de la protection thermique "

    Public Function Protection_Conductivite() As Decimal
        '------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------
        '   Renvoie la conductivité thermique de la protection thermique (W/m.K)
        '------------------------------------------------------------------------------------------------------------

        Dim myLambdaP As Decimal

        Select Case Me.Protection
            Case enu_TypeProtection.LowDensitySpray_Mineral
                myLambdaP = 0.12
            Case enu_TypeProtection.LowDensitySpray_Vermiculite
                myLambdaP = 0.12
            Case enu_TypeProtection.HighDensitySpray_PerliteCement
                myLambdaP = 0.12
            Case enu_TypeProtection.HighDensitySpray_PerlitePlaster
                myLambdaP = 0.12
            Case enu_TypeProtection.IntumescentPaint
                myLambdaP = CustomLambdaP
            Case enu_TypeProtection.BoardsVermiculite
                myLambdaP = 0.2
            Case enu_TypeProtection.BoardsSilicate
                myLambdaP = 0.15
            Case enu_TypeProtection.BoardsFibroCement
                myLambdaP = 0.15
            Case enu_TypeProtection.BoardsPlaster
                myLambdaP = 0.2
        End Select

        Return myLambdaP

    End Function

    Public Function Protection_MasseVol() As Decimal
        '------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------
        '   Renvoie la masse volumique de la protection thermique (kg/m3)
        '------------------------------------------------------------------------------------------------------------

        Dim myRhoP As Decimal = 500

        Select Case Me.Protection
            Case enu_TypeProtection.LowDensitySpray_Mineral
                myRhoP = 300
            Case enu_TypeProtection.LowDensitySpray_Vermiculite
                myRhoP = 350
            Case enu_TypeProtection.HighDensitySpray_PerliteCement
                myRhoP = 550
            Case enu_TypeProtection.HighDensitySpray_PerlitePlaster
                myRhoP = 650
            Case enu_TypeProtection.IntumescentPaint
                myRhoP = 0
            Case enu_TypeProtection.BoardsVermiculite
                myRhoP = 800
            Case enu_TypeProtection.BoardsSilicate
                myRhoP = 600
            Case enu_TypeProtection.BoardsFibroCement
                myRhoP = 800
            Case enu_TypeProtection.BoardsPlaster
                myRhoP = 800
        End Select

        Return myRhoP

    End Function

    Public Function Protection_ChaleurMassique() As Decimal
        '------------------------------------------------------------------------------------------------------------
        '   22/04/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------
        '   Renvoie la chaleur massique de la protection thermique (J/kg.K)
        '------------------------------------------------------------------------------------------------------------

        Dim mycP As Decimal = 1000

        Select Case Me.Protection
            Case enu_TypeProtection.LowDensitySpray_Mineral
                mycP = 1200
            Case enu_TypeProtection.LowDensitySpray_Vermiculite
                mycP = 1200
            Case enu_TypeProtection.HighDensitySpray_PerliteCement
                mycP = 1100
            Case enu_TypeProtection.HighDensitySpray_PerlitePlaster
                mycP = 1100
            Case enu_TypeProtection.IntumescentPaint
                mycP = 0
            Case enu_TypeProtection.BoardsVermiculite
                mycP = 1200
            Case enu_TypeProtection.BoardsSilicate
                mycP = 1200
            Case enu_TypeProtection.BoardsFibroCement
                mycP = 1200
            Case enu_TypeProtection.BoardsPlaster
                mycP = 1700
        End Select

        Return mycP

    End Function

#End Region

#Region " Fonction de copie "
    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function
#End Region

End Class
