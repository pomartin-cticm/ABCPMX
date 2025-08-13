Public Class cls_PointsSigma

    '=========================================================================================================
    '   CLASSE POUR LA DEFINITION DES POINTS OU SONT CALCULEES LES CONTRAINTES NORMALES
    '   ET POUR LE  CALCUL DE CES CONTRAINTES NORMALES
    '=========================================================================================================

#Region " Attributs "

    Public zPos As List(Of Decimal)                 ' Position z des points où sont calculées les contraintes normales

    Public iProfile(1) As Integer                   ' Indice début et fin des points pour le profilé acier
    Public iBetonDalle(1) As Integer                ' Indice début et fin des points pour le béton de la dalle
    Public iBetonEnrob(1) As Integer                ' Indice début et fin des points pour le béton d'enrobage
    Public iArmaDalle(1) As Integer                 ' Indice début et fin des points pour les armatures de la dalle
    Public iArmaEnrob(1) As Integer                 ' Indice début et fin des points pour les armatures d'enrobage

    Public Const CONVSIGNETRACTION As Decimal = 1   ' Convention de signe pour les contraintes de traction

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

        Me.zPos.Clear()

        InitialisePourProfile(MyPoutre)
        InitialisePourBetonEnrob(MyPoutre)
        InitialisePourArmaEnrob(MyPoutre)
        InitialisePourBetonDalle(MyPoutre)
        InitialisePourArmaDalle(MyPoutre)

    End Sub

    Private Sub InitialisePourArmaDalle(MyPoutre As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation des points de calculs des contraintes normales dans les armatures d'enrobage
        '-----------------------------------------------------------------------------------

        '--( Déclaration

        Dim zTop As Decimal = MyPoutre.Dalle.zTop

        '--( Initialisation

        Me.iArmaDalle(0) = -1
        Me.iArmaDalle(1) = -1

        '--( Traitement

        Select Case MyPoutre.Section.typeSection
            Case cls_Section.Enum_TypeSection.Mixte, cls_Section.Enum_TypeSection.MixteEnrobage
                Me.iArmaDalle(0) = Me.zPos.Count

                For iArma As Integer = 0 To 1
                    If MyPoutre.Dalle.LitArma(iArma).lActive Then
                        Me.zPos.Add(zTop - MyPoutre.Dalle.LitArma(iArma).z_s)
                    End If
                Next

                Me.iArmaDalle(1) = Me.zPos.Count - 1

        End Select

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
            Case cls_Section.Enum_TypeSection.AcierSeulEnrobage, cls_Section.Enum_TypeSection.MixteEnrobage
                Me.iArmaEnrob(0) = Me.zPos.Count

                '# Lit inférieur
                Me.zPos.Add(MyPoutre.Section.zPositionLitArmaEnrobage(0))

                '# Lit intermédiaire
                Me.zPos.Add(MyPoutre.Section.zPositionLitArmaEnrobage(1))

                '# Lit supérieur
                Me.zPos.Add(MyPoutre.Section.zPositionLitArmaEnrobage(2))

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
            Case cls_Section.Enum_TypeSection.AcierSeulEnrobage, cls_Section.Enum_TypeSection.MixteEnrobage
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

    Private Sub InitialisePourProfile(myBeam As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation des points de calculs des contraintes normales dans le profilé
        '-----------------------------------------------------------------------------------

        '--( Initialisations

        Me.iProfile(0) = -1
        Me.iProfile(1) = -1

        '--( Traitement en fct du type de section

        Select Case myBeam.Section.ProfilA.typeProfileAcier
            Case cls_ProfilA.Enum_TypeSectionAcier.Lamine, cls_ProfilA.Enum_TypeSectionAcier.PRS_Bi_Sym, cls_ProfilA.Enum_TypeSectionAcier.PRS_Mono_Sym

                InitialisePourProfileStandard(myBeam)

            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA

                InitialisePourProfileSlimIFBA(myBeam)

            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB

                InitialisePourProfileSlimIFBb(myBeam)

            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB

                InitialisePourProfileSlimSAB(myBeam)

            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

                InitialisePourProfileSlimSFB(myBeam)

        End Select

        Me.iProfile(0) = 0
        Me.iProfile(1) = Me.zPos.Count - 1

    End Sub

    Private Sub InitialisePourProfileSlimIFBA(myBeam As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   08/08/25 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation des points de calculs des contraintes normales dans le profilé
        '   Cas profilé slim IFB-A
        '-----------------------------------------------------------------------------------

        '--( Déclarations

        Dim zSup As Decimal

        zSup = myBeam.Section.zSemSup

        '# Fibre supérieure de la semelle supérieure

        Me.zPos.Add(zSup)

        '# Interface semelle sup / âme

        Me.zPos.Add(zSup - myBeam.Section.ProfilA.Tfs)

        '# CdG du profilé acier

        Me.zPos.Add(myBeam.Section.ProfilA.zCdG)

        '# Interface plat / âme

        Me.zPos.Add(0)

        '# fibre inférieure plat

        Me.zPos.Add(-myBeam.Section.ProfilA.Plat_t)

    End Sub

    Private Sub InitialisePourProfileSlimIFBB(myBeam As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   08/08/25 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation des points de calculs des contraintes normales dans le profilé
        '   Cas profilé slim IFB-B
        '-----------------------------------------------------------------------------------

        '--( Déclarations

        Dim zSup As Decimal

        zSup = myBeam.Section.zSemSup

        '# Fibre supérieure du plat supérieur

        Me.zPos.Add(zSup)

        '# Interface plat sup / âme

        Me.zPos.Add(zSup - myBeam.Section.ProfilA.Plat_t)

        '# CdG du profilé acier

        Me.zPos.Add(myBeam.Section.ProfilA.zCdG)

        '# Interface plat / âme

        Me.zPos.Add(0)

        '# fibre inférieure plat

        Me.zPos.Add(-myBeam.Section.ProfilA.Tfi)

    End Sub
    Private Sub InitialisePourProfileSlimSAB(myBeam As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   08/08/25 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation des points de calculs des contraintes normales dans le profilé
        '   Cas profilé slim SAB
        '-----------------------------------------------------------------------------------

        '--( Déclarations

        Dim zSup As Decimal

        zSup = myBeam.Section.zSemSup

        '# Fibre supérieure de la semelle supérieure

        Me.zPos.Add(zSup)

        '# Interface semelle sup / âme

        Me.zPos.Add(zSup - myBeam.Section.ProfilA.Tfs)

        '# CdG du profilé acier

        Me.zPos.Add(myBeam.Section.ProfilA.zCdG)

        '# Interface semelle inf / âme

        Me.zPos.Add(0)

        '# fibre inférieure semelle inf 

        Me.zPos.Add(-myBeam.Section.ProfilA.Tfi)

    End Sub

    Private Sub InitialisePourProfileSlimSFB(myBeam As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   08/08/25 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation des points de calculs des contraintes normales dans le profilé
        '   Cas profilé slim SFB
        '-----------------------------------------------------------------------------------

        '--( Déclarations

        Dim zSup As Decimal

        zSup = myBeam.Section.zSemSup

        '# Fibre supérieure de la semelle supérieure

        Me.zPos.Add(zSup)

        '# Interface semelle sup / âme

        Me.zPos.Add(zSup - myBeam.Section.ProfilA.Tfs)

        '# CdG du profilé acier

        Me.zPos.Add(myBeam.Section.ProfilA.zCdG)

        '# Interface semelle inf / âme

        Me.zPos.Add(zSup - myBeam.Section.ProfilA.ha + myBeam.Section.ProfilA.Tfi + myBeam.Section.ProfilA.Plat_t)

        '# fibre inférieure semelle inf 

        Me.zPos.Add(zSup - myBeam.Section.ProfilA.hb)

        '# fibre inférieure du plat 

        Me.zPos.Add(zSup - myBeam.Section.ProfilA.ha)

    End Sub

    Private Sub InitialisePourProfileStandard(myBeam As cls_Poutre)
        '-----------------------------------------------------------------------------------
        '   08/08/25 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Initialisation des points de calculs des contraintes normales dans le profilé
        '   Cas profilé standard
        '-----------------------------------------------------------------------------------

        '# Fibre supérieure de la semelle supérieure

        Me.zPos.Add(0)

        '# Interface semelle sup / âme

        Me.zPos.Add(-myBeam.Section.ProfilA.Tfs)

        '# CdG du profilé acier

        Me.zPos.Add(myBeam.Section.ProfilA.zCdG)

        '# Interface semelle inf / âme

        Me.zPos.Add(-myBeam.Section.ProfilA.ha + myBeam.Section.ProfilA.Tfi)

        '# Fibre inférieure de la semelle inférieure

        Me.zPos.Add(-myBeam.Section.ProfilA.ha)

    End Sub

#End Region

#Region " Calculs des contraintes "

    Public Sub CalculContraintesCharges(myBeam As cls_Poutre, Signe As Decimal, ByRef Sigma(,,,) As Decimal)
        '-----------------------------------------------------------------------------------
        '   20/10/23 :  Création - POM
        '-----------------------------------------------------------------------------------
        '   Calculs des contraintes normales issues de tous les cas de charges
        '-----------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   Signe       [E] :   Cas de charge traité (qui a été calculé par EF)
        '   Sigma       [S] :   Table des contraintes (icas, ipts,inode,0 ou 1)
        '-----------------------------------------------------------------------------------

        '--> Déclarations

        Dim NbNodes As Integer = myBeam.Nodes.nbNodes
        Dim NbPts As Integer = Me.zPos.Count
        Dim NbCas As Integer = myBeam.ChargesA.Count
        Dim lAcierNonEnrob As Boolean
        Dim iCas As Integer
        Dim lSlimAcier As Boolean = myBeam.Section.lSlimFloor And Not myBeam.Section.lMixte

        '--> Initialisation

        ReDim Sigma(NbCas - 1, NbPts - 1, NbNodes - 1, 1)

        lAcierNonEnrob = (myBeam.Section.TypeSection = cls_Section.Enum_TypeSection.AcierSeul) Or lSlimAcier

        '--> Boucle sur tous les cas de charges

        For iCas = 0 To NbCas - 1

            If myBeam.ChargesA(iCas).lRunCalcul Then

                If lAcierNonEnrob Then
                    Me.CalculContraintesSectionsAcierNonEnrobees(myBeam, iCas, Sigma)
                Else
                    Me.CalculContraintesGeneral(myBeam, Signe, iCas, Sigma)
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
        Dim kDeb, kFin As Integer
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

        IndexElts = MyPoutre.ChargesA(iCas).IndElts(0)
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

                            Sigma(iCas, iPts, iNode, k) = MEd / InertieY(iNode, k) * DeltaZ / nEqDalle / kConvMPaPa

                        Next

                    End If

                    '# Contraintes dans les armatures de la dalle

                    If lDalle And Me.iArmaDalle(0) > -1 Then

                        For iPts = Me.iArmaDalle(0) To Me.iArmaDalle(1)

                            DeltaZ = zCdG - Me.zPos(iPts)

                            Sigma(iCas, iPts, iNode, k) = MEd / InertieY(iNode, k) * DeltaZ / nEqArma / kConvMPaPa

                        Next

                    End If

                    '# Contraintes dans le béton d'enrobage

                    If lEnrob And Me.iBetonEnrob(0) > -1 Then

                        For iPts = Me.iBetonEnrob(0) To Me.iBetonEnrob(1)

                            DeltaZ = zCdG - Me.zPos(iPts)

                            Sigma(iCas, iPts, iNode, k) = MEd / InertieY(iNode, k) * DeltaZ / nEqEnrob / kConvMPaPa

                        Next

                    End If

                    '# Contraintes dans les armatures d'enrobage

                    If lEnrob And Me.iArmaEnrob(0) > -1 Then

                        For iPts = Me.iArmaEnrob(0) To Me.iArmaEnrob(1)

                            DeltaZ = zCdG - Me.zPos(iPts)

                            Sigma(iCas, iPts, iNode, k) = MEd / InertieY(iNode, k) * DeltaZ / nEqArma / kConvMPaPa

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
        Dim indTabElt As Integer = MyPoutre.ChargesA(iCas).IndElts(0)

        '--> Traitement

        For iNode = 0 To NbNodes - 1

            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = NbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kDeb To kFin

                iElt = iNode + DeltaI(k)
                zCdG = MyPoutre.Elements(indTabElt).zANE(iElt)

                If Not IsSmaller(MyPoutre.Elements(indTabElt).InertieY(iElt) * 10 ^ 8, 0) Then
                    For iPts = Me.iProfile(0) To Me.iProfile(1)

                        DeltaZ = zCdG - Me.zPos(iPts)
                        MEd = MyPoutre.ChargesA(iCas).MYY(iNode, k)
                        InertieY = MyPoutre.Elements(indTabElt).InertieY(iElt)

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

        lAcierNonEnrob = (MyPoutre.Section.typeSection = cls_Section.Enum_TypeSection.AcierSeul) _
                      Or ((MyPoutre.Section.typeSection = cls_Section.Enum_TypeSection.Mixte) And (MyCas.EtatDalle = cls_CasDeCharge.EnuEtatDalle.Acier))

        '--> Calcul des contraintes

        If lAcierNonEnrob Then
            CalculContraintesSectionsAcierNonEnrobees(MyPoutre, MyCas, Sigma)
        Else
        End If
    End Sub

    Private Sub CalculContraintesSectionsAcierNonEnrobees(myPoutre As cls_Poutre, myCas As cls_CasDeCharge, ByRef Sigma(,,) As Decimal)
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

        Dim NbNodes As Integer = myPoutre.Nodes.nbNodes
        Dim NbPts As Integer = Me.zPos.Count
        Dim iNode, iPts, k As Integer
        Dim kDeb, kFin, iElt As Integer
        Dim DeltaI() As Integer = {-1, 0}
        Dim DeltaZ, zCdG As Decimal
        Dim indTabElt As Integer = myCas.IndElts(0)

        '--> Initialisation

        ReDim Sigma(NbPts - 1, NbNodes - 1, 1)

        '--> Traitement

        For iNode = 0 To NbNodes - 1

            If iNode = 0 Then kDeb = 1 Else kDeb = 0
            If iNode = NbNodes - 1 Then kFin = 0 Else kFin = 1

            For k = kDeb To kFin

                iElt = iNode + DeltaI(k)
                zCdG = myPoutre.Elements(indTabElt).zANE(iElt)

                If Not IsSmaller(myPoutre.Elements(indTabElt).InertieY(iElt) * 10 ^ 8, 0) Then
                    For iPts = Me.iProfile(0) To Me.iProfile(1)

                        DeltaZ = zCdG - Me.zPos(iPts)

                        Sigma(iPts, iNode, k) = myCas.MYY(iNode, k) / myPoutre.Elements(indTabElt).InertieY(iElt) * DeltaZ

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

#Region " Ajustement des contraintes combinées "

    Public Sub AjusteContraintes(myBeam As cls_Poutre, ByRef SigmaELU(,,) As Decimal)
        '----------------------------------------------------------------------------------------------------------------
        '   29/02/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------
        '   Pour les contraintes après combinaison ELU, on ajuste les contraintes normales 
        '   dans le béton et dans les armatures, en fonction de leur signe
        '----------------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   SigmaELU    [E/S] : Tableau des contraintes aux ELU
        '----------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim lMixte As Boolean = myBeam.lMixte
        Dim lEnrob As Boolean = myBeam.lEnrobage

        Dim nbNodes As Integer = myBeam.Nodes.nbNodes


        '--( Traitement des contraintes dans le béton de la dalle

        If lMixte Then
            TraitementContraintes(SigmaELU, Me.iBetonDalle(0), Me.iBetonDalle(1), nbNodes, -CONVSIGNETRACTION)
        End If

        '--( Traitement des contraintes dans le béton d'enrobage

        If lEnrob Then
            TraitementContraintes(SigmaELU, Me.iBetonEnrob(0), Me.iBetonEnrob(1), nbNodes, -CONVSIGNETRACTION)
        End If

        '--( Traitement des contraintes dans les armatures de la dalle

        If lMixte And Not (myBeam.Param.lCompressionArma) Then
            TraitementContraintes(SigmaELU, Me.iArmaDalle(0), Me.iArmaDalle(1), nbNodes, CONVSIGNETRACTION)
        End If

        '--( Traitement des contraintes dans les armatures de l'enrobage

        If lEnrob And Not (myBeam.Param.lCompressionArma) Then
            TraitementContraintes(SigmaELU, Me.iArmaEnrob(0), Me.iArmaEnrob(1), nbNodes, CONVSIGNETRACTION)
        End If

    End Sub

    Private Sub TraitementContraintes(ByRef SigmaELU(,,) As Decimal, iPt0 As Integer, iPt1 As Integer, nbNodes As Integer, SigneS As Decimal)
        '----------------------------------------------------------------------------------------------------------------
        '   29/02/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------
        '   Pour les contraintes après combinaison ELU, on ajuste les contraintes normales 
        '   dans le béton ou les armatures, en fonction de leur signe
        '----------------------------------------------------------------------------------------------------------------
        '   SigmaELU    [E/S] : Tableau des contraintes aux ELU
        '   iPt0,iPt1   [E] :   Indice des points entre lesquels on ajuste la contrainte dans le béton
        '   nbNodes     [E] :   Nombre de noeuds dans le modèle
        '   SigneS      [S] :   Signe de sélection des contraintes (si 1, on ne retient que les contraintes >0)
        '----------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim iNode, iPoint, k As Integer

        '--( Traitement

        If (iPt0 > -1 And (iPt1 > -1)) Then
            For iPoint = iPt0 To iPt1
                For iNode = 0 To nbNodes - 1
                    For k = 0 To 1
                        SigmaELU(iPoint, iNode, k) = SigneS * Math.Max(0, SigneS * SigmaELU(iPoint, iNode, k))
                    Next
                Next
            Next
        End If

    End Sub

#End Region

#Region " Effet du béton tendu dans les armatures "

    Public Sub AjusteDeltaS(DeltaS As Decimal, ByRef SigmaC(,,) As Decimal, iNodeD As Integer, iNodeF As Integer)
        '--------------------------------------------------------------------------------------------
        '   16/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Ajoute l'effet du béton tendu dans le calcul des contraintes dans les armatures tendues
        '--------------------------------------------------------------------------------------------
        '   DeltaS      [E] :   Effet de rigidité du béton tendu
        '   SigmaC      [E/S] : Contraintes dans la section, après combinaison
        '   iNodeD et F [E] :   Indices des noeuds sur lesquels on réalise le traitement
        '--------------------------------------------------------------------------------------------

        Const lDalle As Boolean = True
        Dim iNode, k, kDeb, kFin As Integer

        If lDalle And Me.iArmaDalle(0) > -1 Then

            For iPts = Me.iArmaDalle(0) To Me.iArmaDalle(1)

                For inode = iNodeD To iNodeF

                    If iNode = iNodeD Then kDeb = 1 Else kDeb = 0
                    If iNode = iNodeF Then kFin = 0 Else kFin = 1

                    For k = kDeb To kFin
                        If IsGreater(CONVSIGNETRACTION * SigmaC(iPts, iNode, k), 0) Then

                            SigmaC(iPts, iNode, k) += DeltaS

                        End If

                    Next

                Next

            Next

        End If

    End Sub

    Public Function ContrainteMaxArmature(ByRef SigmaC(,,) As Decimal, iNodeApp As Integer) As Decimal
        '--------------------------------------------------------------------------------------------
        '   16/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Ajoute l'effet du béton tendu dans le calcul des contraintes dans les armatures tendues
        '--------------------------------------------------------------------------------------------
        '   SigmaC      [E] :   Contraintes dans la section, après combinaison
        '   iNodeApp    [E] :   Indice du noeud sur appui
        '--------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim SigmaArma As Decimal

        '--( Traitement

        If Me.iArmaDalle(0) > -1 Then

            SigmaArma = Math.Max(SigmaC(Me.iArmaDalle(0), iNodeApp, 0), SigmaC(Me.iArmaDalle(0), iNodeApp, 1))

            For iPts As Integer = Me.iArmaDalle(0) + 1 To Me.iArmaDalle(1)

                For k = 0 To 1
                    SigmaArma = Math.Max(SigmaArma, SigmaC(Me.iArmaDalle(0), iNodeApp, k))
                Next

            Next

        End If

        Return SigmaArma

    End Function

#End Region

End Class
