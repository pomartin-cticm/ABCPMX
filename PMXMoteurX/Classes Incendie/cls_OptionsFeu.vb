Public Class cls_OptionsFeu

    '### CLASSE POUR GERER LES OPTIONS DU CALCUL à L'INCENDIE ###################################

#Region " Attributs "

    Public DeltaTsimple As Decimal                          ' Incrément de temps utilisé pour le calcul d'échauffement des structures acier non protégées
    Public DeltaTprotege As Decimal                         ' Incrément de temps utilisé pour le calcul d'échauffement des structures acier protégées
    Public TempRef As Decimal                               ' Température de référence (à t = 0)

    Public EmissivityFire As Decimal                        ' Emissivité du feu

    Public ConvectionCoef As Decimal                        ' Coefficient de convection sur les faces exposées au feu
    Public ConvectionCoefDalle As Decimal                   ' Coefficient de convection sur la face supérieure de la dalle, non exposée au feu (uniquement si calcul EF Dalle)

    Public PhiViewFactor As Decimal                         ' Facteur de vue

    Public lHeatingSlabEF As Boolean                        ' Indique si on calcul l'échauffement de la dalle par calcul numérique
    Public tDalleEFmax As Decimal                           ' Epaisseur maximale d'un elt dalle pour le calcul numérique de l'échauffement de la dalle

    Public AlphaSlab As Decimal                             ' Coefficient de pondération pour la résistance plastique en compression de la dalle
    Public lArmaCompression As Boolean                      ' Indique si on prend en compte les armatures comprimées (dans l'enrobage partiel)

    Public lCalcuFeu As Boolean                             ' Indique si on effectue le calcul au feu

    Public BOLTZMANN As Decimal                             ' Constante de Boltzmann

    Public TypeSurface As enu_TypeSurface                   ' Type de surface (protégée ou non, galvanisée ou non)

    Public Enum enu_TypeSurface
        Protege
        AcierNu
        Galvanise
    End Enum

#End Region

#Region " Constructeur "

    Public Sub New()

        Me.TempRef = 20                     ' [°C]
        Me.DeltaTsimple = 5                 ' [secondes]
        Me.DeltaTprotege = 10               ' [secondes]

        Me.EmissivityFire = 1.0

        Me.ConvectionCoef = 25              ' [W/m2K]
        Me.ConvectionCoefDalle = 4          ' [W/m2K]

        Me.lHeatingSlabEF = False

        Me.PhiViewFactor = 1.0

        Me.tDalleEFmax = 0.01               ' 10 mm

        Me.AlphaSlab = 1

        Me.lCalcuFeu = True
        Me.lArmaCompression = True

        Me.TypeSurface = enu_TypeSurface.AcierNu

        Me.BOLTZMANN = 5.67 * 10 ^ (-8)
    End Sub

#End Region

#Region " Fonctions "

    Public ReadOnly Property DeltaTCalcul As Decimal
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
    End Property

#End Region


End Class
