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

#Region " Calculs des contraintes "

    Public Sub CalculContraintesCharges(MyPoutre As cls_Poutre, Signe As Decimal, ByRef Sigma(,,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calculs des contraintes normales issues d'un cas de charges
        '-----------------------------------------------------------------------------------
        '   Calcul des contraintes normales à l'issue du calcu d'un cas de charge
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   Signe       [E] :   Cas de charge traité (qui a été calculé par EF)
        '   Sigma       [S] :   Table des contraintes (icas, ipts,inode,0 ou 1)
        '-----------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim NbPts As Integer = Me.zPos.Count
        Dim NbCas As Integer = MyPoutre.ChargesA.Count
        Dim lAcierNonEnrob As Boolean
        Dim iCas As Integer

        '--> Initialisation

        ReDim Sigma(NbCas - 1, NbPts - 1, NbNodes - 1, 1)

        lAcierNonEnrob = (MyPoutre.Section.typeSection = cls_Section.Enum_TypeSection.Acier) '_
        ' Or ((MyPoutre.Section.typeSection = cls_Section.Enum_TypeSection.Mixte) And (MyCas.EtatDalle = cls_CasDeCharge.EnuEtatDalle.Acier))

        '--> Boucle sur tous les cas de charges

        For icas = 0 To NbCas - 1

            If MyPoutre.ChargesA(iCas).lRunCalcul Then

                If lAcierNonEnrob Then
                    Me.CalculContraintesSectionsAcierNonEnrobees(MyPoutre, iCas, Sigma)
                Else
                    Me.CalculContraintesGeneral(MyPoutre, Signe, iCas, Sigma)
                End If

            End If

        Next
    End Sub

    Private Sub CalculContraintesGeneral(MyPoutre As cls_Poutre, Signe As Decimal, iCas As Integer, ByRef Sigma(,,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calculs des contraintes normales issues d'un cas de charges avec section avec du béton
        '   Cas général où il faut prendre en compte le signe de la flexion
        '-----------------------------------------------------------------------------------
        '   Calcul des contraintes normales à l'issue du calcul d'un cas de charge
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   iCas        [E] :   Indice du cas de charge traité (qui a été calculé par EF)
        '   Sigma       [S] :   Table des contraintes pour le cas de charge
        '-----------------------------------------------------------------------------------

        '--> Déclaration

        Dim NbNodes As Integer = MyPoutre.Nodes.nbNodes

        Dim iNode, iPts, k As Integer
        Dim kDeb, kFin, iElt As Integer
        Dim DeltaI() As Integer = {-1, 0}
        Dim DeltaZ, zCdG As Decimal

        Dim Beff() As Decimal = Nothing
        Dim InertieY(,) As Decimal = Nothing
        Dim zANE(,) As Decimal = Nothing
        Dim nEqEnrob As Decimal
        Dim nEqDalle As Decimal
        Dim nEqArma As Decimal
        Dim IndexElts As Integer
        Dim lDalle, lEnrob As Boolean
        Dim MEd As Decimal

        '--> Initialisation

        IndexElts = MyPoutre.ChargesA(iCas).IndElts
        nEqDalle = MyPoutre.Elements(IndexElts).nEqDalle
        nEqEnrob = MyPoutre.Elements(IndexElts).nEqEnrob
        lDalle = (MyPoutre.ChargesA(iCas).EtatDalle = cls_CasDeCharge.EnuEtatDalle.Mixte) And MyPoutre.lMixte
        lEnrob = MyPoutre.lEnrobage
        nEqArma = MyPoutre.Section.Acier.EYoung / MyPoutre.Dalle.AcierArmatures.Es

        '--> Largeur efficace de dalle

        If lDalle Then
            MyPoutre.MaillageBeff(MyPoutre.Param.lLargeurEfficaceSimplifiee, False, Beff)
        End If

        '--> Calcul des propriétés de section

        MyPoutre.MaillagePropElastiquesMixtes(Beff, Signe, True, nEqEnrob, nEqDalle, InertieY, zANE, ldalle)

        '--> Calcul des contraintes 

        For iNode = 0 To NbNodes - 1
            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = NbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kDeb To kFin

                zCdG = zANE(iNode, k)
                MEd = MyPoutre.ChargesA(iCas).MYY(iNode, k)

                If Not IsSmaller(InertieY(iNode, k) * 10 ^ 8, 0) Then

                    '# Contraintes dans la partie acier

                    For iPts = Me.iProfile(0) To Me.iProfile(1)

                        DeltaZ = zCdG - Me.zPos(iPts)

                        Sigma(iCas, iPts, iNode, k) = MEd / InertieY(iNode, k) * DeltaZ / kConvMPaPa

                    Next

                    '# Contraintes dans le béton de la dalle

                    If lDalle And Me.iBetonDalle(0) > -1 Then

                        For iPts = Me.iBetonDalle(0) To Me.iBetonDalle(1)

                            DeltaZ = zCdG - Me.zPos(iPts)

                            Sigma(iCas, iPts, iNode, k) = MEd / InertieY(iNode, k) * DeltaZ * nEqDalle / kConvMPaPa

                        Next

                    End If

                    '# Contraintes dans les armatures de la dalle

                    If lDalle And Me.iArmaDalle(0) > -1 Then

                        For iPts = Me.iArmaDalle(0) To Me.iArmaDalle(1)

                            DeltaZ = zCdG - Me.zPos(iPts)

                            Sigma(iCas, iPts, iNode, k) = MEd / InertieY(iNode, k) * DeltaZ * nEqArma / kConvMPaPa

                        Next

                    End If

                    '# Contraintes dans le béton d'enrobage

                    If lEnrob And Me.iBetonEnrob(0) > -1 Then

                        For iPts = Me.iBetonEnrob(0) To Me.iBetonEnrob(1)

                            DeltaZ = zCdG - Me.zPos(iPts)

                            Sigma(iCas, iPts, iNode, k) = MEd / InertieY(iNode, k) * DeltaZ * nEqEnrob / kConvMPaPa

                        Next

                    End If

                    '# Contraintes dans les armatures d'enrobage

                    If lEnrob And Me.iArmaEnrob(0) > -1 Then

                        For iPts = Me.iArmaEnrob(0) To Me.iArmaEnrob(1)

                            DeltaZ = zCdG - Me.zPos(iPts)

                            Sigma(iCas, iPts, iNode, k) = MEd / InertieY(iNode, k) * DeltaZ * nEqArma / kConvMPaPa

                        Next

                    End If

                End If
            Next

        Next

    End Sub


    Private Sub CalculContraintesSectionsAcierNonEnrobees(MyPoutre As cls_Poutre, iCas As Integer, ByRef Sigma(,,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calculs des contraintes normales issues d'un cas de charges avec section acier seul
        '-----------------------------------------------------------------------------------
        '   Calcul des contraintes normales à l'issue du calcul d'un cas de charge
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   MyCas       [E] :   Cas de charge traité (qui a été calculé par EF)
        '   Sigma       [S] :   Table des contraintes pour le cas de charge
        '-----------------------------------------------------------------------------------

        '--> Déclaration

        Dim NbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim NbPts As Integer = Me.zPos.Count
        Dim iNode, iPts, k As Integer
        Dim kDeb, kFin, iElt As Integer
        Dim DeltaI() As Integer = {-1, 0}
        Dim DeltaZ, zCdG As Decimal
        Dim MEd, InertieY As Decimal

        '--> Traitement

        For iNode = 0 To NbNodes - 1

            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = NbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kDeb To kFin

                iElt = iNode + DeltaI(k)
                zCdG = MyPoutre.Elements(MyPoutre.ChargesA(iCas).IndElts).zANE(iElt)

                If Not IsSmaller(MyPoutre.Elements(MyPoutre.ChargesA(iCas).IndElts).InertieY(iElt) * 10 ^ 8, 0) Then
                    For iPts = Me.iProfile(0) To Me.iProfile(1)

                        DeltaZ = zCdG - Me.zPos(iPts)
                        MEd = MyPoutre.ChargesA(iCas).MYY(iNode, k)
                        InertieY = MyPoutre.Elements(MyPoutre.ChargesA(iCas).IndElts).InertieY(iElt)

                        Sigma(iCas, iPts, iNode, k) = MEd / InertieY * DeltaZ / kConvMPaPa

                    Next
                End If

            Next

        Next

    End Sub

    Public Sub CalculsContraintes(MyPoutre As cls_Poutre, MyCas As cls_CasDeCharge, ByRef Sigma(,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calculs des contraintes normales issues d'un cas de charges
        '-----------------------------------------------------------------------------------
        '   Calcul des contraintes normales à l'issue du calcu d'un cas de charge
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   MyCas       [E] :   Cas de charge traité (qui a été calculé par EF)
        '   Sigma       [S] :   Table des contraintes pour le cas de charge
        '-----------------------------------------------------------------------------------

        '--> Déclaration

        Dim lAcierNonEnrob As Boolean

        '--> Initialisation

        lAcierNonEnrob = (MyPoutre.Section.typeSection = cls_Section.Enum_TypeSection.Acier) _
                      Or ((MyPoutre.Section.typeSection = cls_Section.Enum_TypeSection.Mixte) And (MyCas.EtatDalle = cls_CasDeCharge.EnuEtatDalle.Acier))

        '--> Calcul des contraintes

        If lAcierNonEnrob Then
            CalculContraintesSectionsAcierNonEnrobees(MyPoutre, MyCas, Sigma)
        Else
        End If
    End Sub



    Private Sub CalculContraintesSectionsAcierNonEnrobees(MyPoutre As cls_Poutre, MyCas As cls_CasDeCharge, ByRef Sigma(,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calculs des contraintes normales issues d'un cas de charges avec section acier seul
        '-----------------------------------------------------------------------------------
        '   Calcul des contraintes normales à l'issue du calcul d'un cas de charge
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   MyCas       [E] :   Cas de charge traité (qui a été calculé par EF)
        '   Sigma       [S] :   Table des contraintes pour le cas de charge
        '-----------------------------------------------------------------------------------

        '--> Déclaration

        Dim NbNodes As Integer = MyPoutre.Nodes.nbNodes
        Dim NbPts As Integer = Me.zPos.Count
        Dim iNode, iPts, k As Integer
        Dim kDeb, kFin, iElt As Integer
        Dim DeltaI() As Integer = {-1, 0}
        Dim DeltaZ, zCdG As Decimal

        '--> Initialisation

        ReDim Sigma(NbPts - 1, NbNodes - 1, 1)

        '--> Traitement

        For iNode = 0 To NbNodes - 1

            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = NbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kdeb To kFin

                iElt = iNode + DeltaI(k)
                zCdG = MyPoutre.Elements(MyCas.IndElts).zANE(iElt)

                If Not IsSmaller(MyPoutre.Elements(MyCas.IndElts).InertieY(iElt) * 10 ^ 8, 0) Then
                    For iPts = Me.iProfile(0) To Me.iProfile(1)

                        DeltaZ = zcdg - Me.zPos(iPts)

                        Sigma(iPts, iNode, k) = MyCas.MYY(iNode, k) / MyPoutre.Elements(MyCas.IndElts).InertieY(iElt) * DeltaZ

                    Next
                End If

            Next

        Next

    End Sub

    Private Sub CalculContraintesGeneral(MyPoutre As cls_Poutre, MyCas As cls_CasDeCharge, ByRef Sigma(,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calculs des contraintes normales issues d'un cas de charges avec section avec du béton
        '   Cas général où il faut prendre en compte le signe de la flexion
        '-----------------------------------------------------------------------------------
        '   Calcul des contraintes normales à l'issue du calcul d'un cas de charge
        '-----------------------------------------------------------------------------------
        '   MyPoutre    [E] :   Poutre traitée
        '   MyCas       [E] :   Cas de charge traité (qui a été calculé par EF)
        '   Sigma       [S] :   Table des contraintes pour le cas de charge
        '-----------------------------------------------------------------------------------

        '--> Déclaration



        '--> Calcul des propriétés de section

        '#Flexion positive



        '#Flexion négative

        '--> Calcul des contraintes en fonction du signe du moment



    End Sub

#End Region

End Class
