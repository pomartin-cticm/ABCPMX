Public Class DATA_MODAL

#Region " Attributs "
    Public Structure Struc_Donnees
        Dim NbNodes As Integer                  'Nombre de noeuds EF
        Dim xNode() As Decimal                  'Position x des nœuds (0 à NbNodes-1)
        Dim Aire() As Decimal                   'Aire des éléments (0 à NbNodes-2)
        Dim InertieY() As Decimal               'Inertie des éléments (0 à NbNodes-2)
        Dim NbAppuis As Integer                 'Nombre de nœuds appui
        Dim iNodeAppui() As Integer             'Indice des nœuds appui (z bloqué) (0 à NbAppuis-1)
        Dim lAppuiArticule() As Boolean         'Indique si continuité (False) ou articulation (True) au droit des nœuds appui (0 à NbAppuis-1)
        Dim EYOUNG As Decimal                   'Module d'Young
        Dim NbForcesPon As Integer              'Nombre de forces ponctuelles verticales
        Dim xForcePon() As Decimal              'Position x de la force j (0 à NbForcesPon-1)
        Dim ForcePon() As Decimal               'Valeur de la force ponctuelle verticale (>0=gravitaire) (0 à NbForcesPon-1)
        Dim NbMoments As Integer                'Nombre de moments de flexion ponctuels
        Dim xMoment() As Decimal                'Position x du moment j (0 à NbMoments-1)
        Dim Moment() As Decimal                 'Valeur du moment (0 à NbMoments -1)
        Dim NbForcesRep As Integer              'Nombre de forces réparties
        Dim xForceRep(,) As Decimal             'Position x de la force répartie j (0 à NbForcesRep-1), à gauche (j,0) et à droite (j,1)
        Dim ForceRep(,) As Decimal              'Valeur de la force répartie j (0 à NbForcesRep-1), à gauche (j,0) et à droite (j,1)
    End Structure

    Public Structure Struc_Output
        Dim VZ(,) As Decimal                    'Effort tranchant dans l’élément i, aux deux extrémités (0 à NbNodes-2, 0 à 1)
        Dim MYY(,) As Decimal                   'Moment fléchissant dans l’élément i, aux deux extrémités (0 à NbNodes-2, 0 à 1)

        Dim UZ() As Decimal                     'Déplacement vertical du nœud i (0 à NbNodes-1)
        Dim ROTY() As Decimal                   'Rotation du nœud i (0 à NbNodes-1)

        Dim RZ() As Decimal                     'Réactions verticales aux nœuds support (0 à NbAppuis-1)
    End Structure

#End Region

End Class
