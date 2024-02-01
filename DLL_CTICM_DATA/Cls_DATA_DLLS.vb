Public Class DATA_DLLS

    'Public Structure Struc_Donnees
    '    Dim NbNodes As Integer                      'Nombre de noeuds EF
    '    Dim xNode() As Decimal                      'Position x des nœuds (0 à NbNodes-1)

    '    'Dim NbMaintiensPon As Integer               'Nombre de maintiens ponctuels
    '    'Dim iNodeMaintienPon() As Integer           'Numero du noeud où se trouve le maintien ponctuel j (0 à NbMaintienPon-1)
    '    'Dim MaintienPonV() As Decimal               'Maintien ponctuel en déplacement latéral v (0 à NbMaintienPon-1)   : utile pour calcul LTB, -1: totale, 0-libre, >0 ressort
    '    'Dim MaintienPonVP() As Decimal              'Maintien ponctuel en rotation latéral v' (0 à NbMaintienPon-1)     : utile pour calcul LTB, -1: totale, 0-libre, >0 ressort
    '    'Dim MaintienPonTheta() As Decimal           'Maintien ponctuel en torsion theta (0 à NbMaintienPon-1)           : utile pour calcul LTB, -1: totale, 0-libre, >0 ressort
    '    'Dim MaintienPonThetaP() As Decimal          'Maintien ponctuel en gauchissement theta' (0 à NbMaintienPon-1)    : utile pour calcul LTB, -1: totale, 0-libre, >0 ressort
    '    'Dim zMaintienPonC() As Decimal              'Position verticale du maintien ponctuel j (0 à NbMaintienPon-1)

    '    'Dim NbMaintiensCon As Integer               'Nombre de maintiens continus
    '    'Dim iNodeMaintienCon(,) As Integer          'Numero du noeud où se trouve le maintien continu j (0 à NbMaintienCon-1); à gauche (j,0) et à droite (j,1)        
    '    'Dim MaintienConV() As Decimal               'Maintien continu en déplacement latéral v (0 à NbMaintienCon-1) : utile pour calcul LTB, -1: totale, 0-libre, >0 ressort
    '    'Dim MaintienConVP() As Decimal              'Maintien continu en rotation latéral v' (0 à NbMaintienCon-1) : utile pour calcul LTB, -1: totale, 0-libre, >0 ressort
    '    'Dim MaintienConTheta() As Decimal           'Maintien continu en torsion theta (0 à NbMaintienCon-1) : utile pour calcul LTB, -1: totale, 0-libre, >0 ressort
    '    'Dim zMaintienConC() As Decimal              'Position verticale du maintien continu j (0 à NbMaintienCon-1)

    '    Dim Aire() As Decimal                       'Aire des éléments (0 à NbNodes-2)  : pas utile pour calcul LTB
    '    Dim InertieY() As Decimal                   'Inertie /yy des éléments (0 à NbNodes-2) : pas utile pour calcul LTB
    '    Dim InertieZ() As Decimal                   'Inertie /zz des éléments (0 à NbNodes-2) : utile pour calcul LTB
    '    Dim InertieT() As Decimal                   'Inertie /zz des éléments (0 à NbNodes-2) : utile pour calcul LTB
    '    Dim InertieW() As Decimal                   'Inertie /zz des éléments (0 à NbNodes-2) : utile pour calcul LTB
    '    Dim PositionCG() As Decimal                 'Position verticale du centre C par rapport au centre G (0 à NbNodes-2) : utile pour calcul LTB
    '    Dim RayGirPol() As Decimal                  'Rayon de giration polaire (0 à NbNodes-2) : utile pour calcul LTB
    '    Dim CoefBetaZ() As Decimal                  'Coef de Wagner (0 à NbNodes-2) : utile pour calcul LTB
    '    Dim NbAppuis As Integer                     'Nombre de nœuds appui
    '    Dim iNodeAppui() As Integer                 'Indice des nœuds appui (z bloqué) (0 à NbAppuis-1)
    '    Dim lAppuiArticule() As Boolean             'Indique si continuité (False) ou articulation (True) au droit des nœuds appui (0 à NbAppuis-1)
    '    Dim EYOUNG As Decimal                       'Module d'Young
    '    Dim GSHEAR As Decimal                       'Module de cisaillement : utile pour calcul LTB
    '    Dim NbForcesPon As Integer                  'Nombre de forces ponctuelles verticales
    '    Dim xForcePon() As Decimal                  'Position x de la force j (0 à NbForcesPon-1)
    '    Dim zForcePonC() As Decimal                 'Position z de la force j (0 à NbForcesPon-1) par rapport au centre C
    '    Dim ForcePon() As Decimal                   'Valeur de la force ponctuelle verticale (>0=gravitaire) (0 à NbForcesPon-1)
    '    Dim NbMoments As Integer                    'Nombre de moments de flexion ponctuels
    '    Dim xMoment() As Decimal                    'Position x du moment j (0 à NbMoments-1)
    '    Dim Moment() As Decimal                     'Valeur du moment (0 à NbMoments -1)
    '    Dim NbForcesRep As Integer                  'Nombre de forces réparties
    '    Dim xForceRep(,) As Decimal                 'Position x de la force répartie j (0 à NbForcesRep-1), à gauche (j,0) et à droite (j,1)
    '    Dim zForceRepC() As Decimal                 'Position z de la force répartie j (0 à NbForcesRep-1) par rapport a C
    '    Dim ForceRep(,) As Decimal                  'Valeur de la force répartie j (0 à NbForcesRep-1), à gauche (j,0) et à droite (j,1)        
    '    Dim PESANTEUR As Decimal                    'g
    '    Dim MomentFle(,) As Decimal                 'Valeur du moment flechissant de l'element j (0 à NbNodes -2), à gauche (j,0) et à droite (j,1)
    'End Structure

#Region " Attributs "

    Public NbNodes As Integer                      'Nombre de noeuds EF
    Public xNode() As Decimal                      'Position x des nœuds (0 à NbNodes-1)

    Public Aire() As Decimal                       'Aire des éléments (0 à NbNodes-2)  : pas utile pour calcul LTB
    Public InertieY() As Decimal                   'Inertie /yy des éléments (0 à NbNodes-2) : pas utile pour calcul LTB
    Public InertieZ() As Decimal                   'Inertie /zz des éléments (0 à NbNodes-2) : utile pour calcul LTB
    Public InertieT() As Decimal                   'Inertie /zz des éléments (0 à NbNodes-2) : utile pour calcul LTB
    Public InertieW() As Decimal                   'Inertie /zz des éléments (0 à NbNodes-2) : utile pour calcul LTB
    Public PositionCG() As Decimal                 'Position verticale du centre C par rapport au centre G (0 à NbNodes-2) : utile pour calcul LTB
    Public RayGirPol() As Decimal                  'Rayon de giration polaire (0 à NbNodes-2) : utile pour calcul LTB
    Public CoefBetaZ() As Decimal                  'Coef de Wagner (0 à NbNodes-2) : utile pour calcul LTB
    Public NbAppuis As Integer                     'Nombre de nœuds appui
    Public iNodeAppui() As Integer                 'Indice des nœuds appui (z bloqué) (0 à NbAppuis-1)
    Public lAppuiArticule() As Boolean             'Indique si continuité (False) ou articulation (True) au droit des nœuds appui (0 à NbAppuis-1)
    Public EYOUNG As Decimal                       'Module d'Young
    Public GSHEAR As Decimal                       'Module de cisaillement : utile pour calcul LTB
    Public NbForcesPon As Integer                  'Nombre de forces ponctuelles verticales
    Public xForcePon() As Decimal                  'Position x de la force j (0 à NbForcesPon-1)
    Public zForcePonC() As Decimal                 'Position z de la force j (0 à NbForcesPon-1) par rapport au centre C
    Public ForcePon() As Decimal                   'Valeur de la force ponctuelle verticale (>0=gravitaire) (0 à NbForcesPon-1)
    Public NbMoments As Integer                    'Nombre de moments de flexion ponctuels
    Public xMoment() As Decimal                    'Position x du moment j (0 à NbMoments-1)
    Public Moment() As Decimal                     'Valeur du moment (0 à NbMoments -1)
    Public NbForcesRep As Integer                  'Nombre de forces réparties
    Public xForceRep(,) As Decimal                 'Position x de la force répartie j (0 à NbForcesRep-1), à gauche (j,0) et à droite (j,1)
    Public zForceRepC() As Decimal                 'Position z de la force répartie j (0 à NbForcesRep-1) par rapport a C
    Public ForceRep(,) As Decimal                  'Valeur de la force répartie j (0 à NbForcesRep-1), à gauche (j,0) et à droite (j,1)        
    Public PESANTEUR As Decimal                    'g
    Public MomentFle(,) As Decimal                 'Valeur du moment flechissant de l'element j (0 à NbNodes -2), à gauche (j,0) et à droite (j,1)

#End Region

#Region " Constructeurs "

    Public Sub New()
        Me.NbAppuis = 0
        Me.NbNodes = 0
        Me.NbForcesPon = 0
        Me.NbForcesRep = 0
        Me.NbMoments = 0
    End Sub
#End Region

#Region " Outils "

    Public Sub AjouteForceP(Force As Decimal, xPos As Decimal, Optional zPos As Decimal = 0)
        '----------------------------------------------------------------------------------------------------------
        '   01/02/24 :  Création - POM - V1.00
        '----------------------------------------------------------------------------------------------------------
        '   Ajout d'une force ponctuelle dans le chargement - Paramètres de calcul EF
        '----------------------------------------------------------------------------------------------------------
        '   Force       [E] :   Valeur de la force
        '   xPos        [E] :   Position X de la force (par rapport à extrémité gauche de la poutre)
        '   zPos        [E] :   Position Z de la force (par rapport à centre de cisaillement)
        '-------------------------------------------------------------------------------------

        Me.NbForcesPon += 1
        If Me.NbForcesPon = 1 Then
            ReDim Me.ForcePon(Me.NbForcesPon - 1)
            ReDim Me.xForcePon(Me.NbForcesPon - 1)
            ReDim Me.zForcePonC(Me.NbForcesPon - 1)
        Else
            ReDim Preserve Me.ForcePon(Me.NbForcesPon - 1)
            ReDim Preserve Me.xForcePon(Me.NbForcesPon - 1)
            ReDim Preserve Me.zForcePonC(Me.NbForcesPon - 1)
        End If

        Me.ForcePon(Me.NbForcesPon - 1) = Force
        Me.xForcePon(Me.NbForcesPon - 1) = xPos
        Me.zForcePonC(Me.NbForcesPon - 1) = zPos

    End Sub

    Public Sub AjouteMoment(xMom As Decimal, Moment As Decimal)
        '-------------------------------------------------------------------------------------
        '   01/02/24 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Ajout d'un moment dans les paramètres préparatoires au calcul EF
        '-------------------------------------------------------------------------------------
        '   xMom        [E] :   Position du moment
        '   Moment      [E] :   Valeur du moment
        '-------------------------------------------------------------------------------------

        Me.NbMoments += 1
        If Me.NbMoments = 1 Then
            ReDim Me.Moment(Me.NbMoments - 1)
            ReDim Me.xMoment(Me.NbMoments - 1)
        Else
            ReDim Preserve Me.Moment(Me.NbMoments - 1)
            ReDim Preserve Me.xMoment(Me.NbMoments - 1)
        End If

        Me.Moment(Me.NbMoments - 1) = Moment
        Me.xMoment(Me.NbMoments - 1) = xMom

    End Sub

    Public Sub InitialiseForcesRep(NbRep As Integer)
        '-------------------------------------------------------------------------------------
        '   01/02/24 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Ajout d'un moment dans les paramètres préparatoires au calcul EF
        '-------------------------------------------------------------------------------------
        '   NbRep       [E] :   Nombre de forces réparties
        '-------------------------------------------------------------------------------------

        Me.NbForcesRep = NbRep
        If NbRep > 0 Then
            ReDim Me.ForceRep(NbRep - 1, 1)
            ReDim Me.xForceRep(NbRep - 1, 1)
        End If

    End Sub

    Public Sub AjouteForceRep(xo As Decimal, xe As Decimal, qo As Decimal, qe As Decimal, indice As Integer)
        '-------------------------------------------------------------------------------------
        '   09/09/23 :  Création - Version 1.00 - POM
        '-------------------------------------------------------------------------------------
        '   Ajout d'une force répartie dans les paramètres préparatoires au calcul EF
        '-------------------------------------------------------------------------------------
        '   xo          [E] :   Position gauche de la force répartie
        '   xe          [E] :   Position droite de la force répartie
        '   qo          [E] :   Valeur à gauche de la force répartie
        '   qe          [E] :   Valeur à droite de la force répartie
        '   indice      [E] :   Indice dans le tableau
        '-------------------------------------------------------------------------------------

        Me.xForceRep(indice, 0) = xo
        Me.xForceRep(indice, 1) = xe
        Me.ForceRep(indice, 0) = qo
        Me.ForceRep(indice, 1) = qe

    End Sub

#End Region

End Class
