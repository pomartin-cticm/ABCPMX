Public Class cls_PointsSigma

    '=========================================================================================================
    '   CLASSE POUR LA DEFINITION DES POINTS OU SONT CALCULEES LES CONTRAINTES NORMALES
    '=========================================================================================================

#Region " Attributs "

    Public zPos As List(Of Decimal)     ' Position z des points où sont calculées les contraintes normales

    Public iProfile(1) As Integer       ' Indice début et fin des points pour le profilé acier
    Public iBetonDalle(1) As Integer    ' Indice début et fin des points pour le béton de la dalle
    Public iBetonEnrob(1) As Integer    ' Indice début et fin des points pour le béton d'enrobage
    Public iArmaDalle(1) As Integer     ' Indice début et fin des points pour les armatures de la dalle
    Public iArmaEnrob(1) As Integer     ' Indice début et fin des points pour les armatures d'enrobage

#End Region

#Region " Constructeurs "

    Public Sub New()

        zPos = New List(Of Decimal)

    End Sub

#End Region

#Region " Initialisation "

    Public Sub Initialise(MyPoutre As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation des points de calculs des contraintes normales
        '-----------------------------------------------------------------------------------

        InitialisePourProfile(MyPoutre)
        InitialisePourBetonEnrob(MyPoutre)
        InitialisePourArmaEnrob(MyPoutre)
        InitialisePourBetonDalle(MyPoutre)

    End Sub

    Private Sub InitialisePourArmaEnrob(MyPoutre As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation des points de calculs des contraintes normales dans les armatures d'enrobage
        '-----------------------------------------------------------------------------------

        Me.iArmaEnrob(0) = -1
        Me.iArmaEnrob(1) = -1

        Select Case MyPoutre.Section.typeSection
            Case cls_Section.Enum_TypeSection.AcierEnrobage, cls_Section.Enum_TypeSection.MixteEnrobage
                Me.iArmaEnrob(0) = Me.zPos.Count

                '# Lit inférieur

                '# Lit intermédiaire

                '# Lit supérieur

                Me.iArmaEnrob(1) = Me.zPos.Count - 1
        End Select
    End Sub

    Private Sub InitialisePourBetonEnrob(MyPoutre As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation des points de calculs des contraintes normales dans le béton d'enrobage
        '-----------------------------------------------------------------------------------

        Me.iBetonEnrob(0) = -1
        Me.iBetonEnrob(1) = -1

        Select Case MyPoutre.Section.typeSection
            Case cls_Section.Enum_TypeSection.AcierEnrobage, cls_Section.Enum_TypeSection.MixteEnrobage
                Me.iBetonEnrob(0) = Me.zPos.Count

                '# Fibre supérieure
                Me.zPos.Add(-MyPoutre.Section.ProfilA.Tfs)
                '# Fibre inférieure
                Me.zPos.Add(-MyPoutre.Section.ProfilA.ha + MyPoutre.Section.ProfilA.Tfi)

                Me.iBetonEnrob(1) = Me.zPos.Count - 1
        End Select

    End Sub



    Private Sub InitialisePourBetonDalle(MyPoutre As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation des points de calculs des contraintes normales dans la dalle béton
        '-----------------------------------------------------------------------------------

        Me.iBetonDalle(0) = -1
        Me.iBetonDalle(1) = Me.iBetonDalle(0)

        Select Case MyPoutre.Section.typeSection
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                Me.zPos.Add(MyPoutre.Dalle.zTop)
                Me.iBetonDalle(0) = Me.zPos.Count - 1
                Me.iBetonDalle(1) = Me.iBetonDalle(0)
        End Select

    End Sub

    Private Sub InitialisePourProfile(MyPoutre As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation des points de calculs des contraintes normales dans le profilé
        '-----------------------------------------------------------------------------------

        Me.iProfile(0) = -1
        Me.iProfile(1) = -1

        Select Case MyPoutre.Section.typeSection
            Case cls_Section.Enum_TypeSection.Acier, cls_Section.Enum_TypeSection.AcierEnrobage,
                 cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage

                '# Fibre supérieure de la semelle supérieure

                Me.zPos.Add(0)

                '# Interface semelle sup / âme

                Me.zPos.Add(-MyPoutre.Section.ProfilA.Tfs)

                '# CdG du profilé acier

                Me.zPos.Add(MyPoutre.Section.ProfilA.zcdg)

                '# Interface semelle inf / âme

                Me.zPos.Add(-MyPoutre.Section.ProfilA.ha + MyPoutre.Section.ProfilA.Tfi)

                '# Fibre inférieure de la semelle inférieure

                Me.zPos.Add(-MyPoutre.Section.ProfilA.ha)

                Me.iProfile(0) = 0
                Me.iProfile(1) = Me.zPos.Count - 1

        End Select

    End Sub

#End Region

End Class
