Public Class cls_VerifFeuSlimMixte

#Region " Declaration "

    'Public Shared TimeSteps() As Decimal = {30, 60, 90, 120, 180}
    Public Shared TimeSteps() As Decimal = {1, 2, 3, 5, 10}
    'Public Shared TimeSteps() As Decimal = {1, 2}

#End Region

#Region " Attributs "

    Private NbStep As Integer                           ' Nombre d'items dans le tableau TimeSteps
    Public RStep As Integer                             ' Indice du dernier pas de calcul de la table TimeStep pour laquelle tous les critères sont OK

    Private Maillage As cls_MaillageSlimFloor           ' Modèle numérique pour le calcul de l'échauffement d'une slim floor

    Public TempMailStep(,,) As Decimal                  ' Tableau des températures du maillage à chaque pas de temps

    Public CritereM() As cls_Critere                    ' Resistance à la flexion
    Public CritereMY() As cls_Critere                   ' Resistance à la flexion transversale 
    Public CritereV() As cls_Critere                    ' Resistance effort tranchant
    Public CritereMV() As cls_Critere                   ' Resistance à l'interaction effort tranchant

#End Region

#Region " Constructeurs et Initialisation "

    Public Sub New()
        Me.NbStep = cls_VerifFeuSlimAcier.TimeSteps.GetUpperBound(0) + 1
        Me.RStep = -1
    End Sub

    Private Sub InitialiseCriteres(NbNodes As Integer, NbCombi As Integer, IndDerniereT As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   30/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialisation des critères pour une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   NbNodes     [E] :   Nombre de noeuds
        '   NbCombi     [E] :   Nombre de combinaisons
        '   IndDerniereT[E] :   Indice de la dernière travée
        '   lElastic    [E] :   Cas d'un dimensionnement élastique VM en flexion
        '   lElasticTau [E] :   Cas d'un dimensionnement élastique VM en cisaillement
        '----------------------------------------------------------------------------------------------------------

        ReDim CritereM(Me.NbStep - 1)
        ReDim CritereMY(Me.NbStep - 1)
        ReDim CritereV(Me.NbStep - 1)
        ReDim CritereMV(Me.NbStep - 1)

        For iStep As Integer = 0 To NbStep - 1
            Me.CritereM(iStep) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereV(iStep) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereMY(iStep) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereMV(iStep) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Next
    End Sub

#End Region

#Region " GENERAL "

    Public Sub Z_VerifFeu(myBeam As cls_Poutre, iBeam As Integer, FileNameP As String,
                     Optional progressEtape As IProgress(Of Struc_MAJEtape) = Nothing,
                     Optional progressDansEtape As IProgress(Of Integer) = Nothing)
        '--------------------------------------------------------------------------------------------------------------------------
        '   28/05/26 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Gestion des calculs au feu pour les poutres slim mixte
        '--------------------------------------------------------------------------------------------------------------------------
        '   myBeam              [E] :   Poutre traitée
        '   iBeam               [E] :   Indice de la poutre traitée dans la liste des poutres du projet
        '   FileNameP           [E] :   Nom du fichier de sauvegarde du projet
        '   progressEtape       [E] :   Permet de d'envoyer l'information de progression sur les étapes de temps à la Frm_CalculEnCours
        '   progressDansEtape   [E] :   Permet de d'envoyer l'information de progression entre les étapes de temps à la Frm_CalculEnCours
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim nbCombiELF As Integer

        Dim TempBeton(NbStep)() As Decimal '= Nothing
        Dim TempArma(NbStep)() As Decimal '= Nothing
        Dim TempSemSup(NbStep)() As Decimal '= Nothing
        Dim TempSemInf(NbStep)() As Decimal '= Nothing
        Dim TempAme(NbStep)() As Decimal '= Nothing
        Dim TempPlat(NbStep)() As Decimal '= Nothing
        Dim TempSoud(NbStep)() As Decimal '= Nothing

        '--( Initialisation

        nbCombiELF = myBeam.CombiA_ELF.nbCombi

        InitialiseCriteres(myBeam.Nodes.nbNodes, nbCombiELF, myBeam.IndiceDerniereTravee)

        For iStep = 0 To NbStep
            TempBeton(iStep) = New Decimal(1) {}
            TempArma(iStep) = New Decimal(1) {}
            TempSemSup(iStep) = New Decimal(1) {}
            TempSemInf(iStep) = New Decimal(1) {}
            TempAme(iStep) = New Decimal(1) {}
            TempPlat(iStep) = New Decimal(1) {}
            TempSoud(iStep) = New Decimal(1) {}
        Next

        '# Calcul échauffement de la section (numérique) #########################################################################
        '--( Calcul de l'échauffement de la section

        If myBeam.lCalculOK Then
            If myBeam.lCalculCharge Then
                '# Cas où les résultats de calcul sont directement disponibles en mémoire
                ' On n'a juste à recréer le maillage

                '  RecupereTemperatureFile(myBeam, iBeam, FileNameP, False)

            Else
                '# Cas où les résultats de calcul sont disponibles dans le fichier de sauvegarde

                ' Il faut regénérer le maillage et 
                ' récupérer les températures du maillage dans le fichier

                '  RecupereTemperatureFile(myBeam, iBeam, FileNameP, True)

            End If
        Else
            '# Cas où les résultats de calcul ne sont pas disponibles

            Echauffement_SlimMixte(myBeam, progressEtape, progressDansEtape)

        End If

        '# Valeurs enveloppes des températures ####################################################################################

        For iStep As Integer = 0 To NbStep - 1
            'myBeam.VerifFeuSlimAcier.TemperatureStepMinMax(iStep, TempBeton(iStep), TempAme(iStep),
            '                                                      TempSemInf(iStep), TempSemSup(iStep),
            '                                                      TempPlat(iStep), TempSoud(iStep), TempArma(iStep))
        Next
    End Sub

    Private Sub InitialiseVariables(NbY As Integer, NbZ As Integer)
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Incrément de temps variable
        '---------------------------------------------------------------------------------------------------------
        '   NbY         [E] :   Nombre de mailles en Y
        '   NbZ         [E] :   Nombre de mailles en Z
        '---------------------------------------------------------------------------------------------------------

        ReDim Me.TempMailStep(Me.NbStep - 1, NbY - 1, NbZ - 1)

    End Sub

#End Region

#Region " Echauffement "

    Private Sub Echauffement_SlimMixte(myBeam As cls_Poutre,
                   Optional progressEtape As IProgress(Of Struc_MAJEtape) = Nothing,
                   Optional progressDansEtape As IProgress(Of Integer) = Nothing)

        '--( Déclaration des variables

        Dim iSTep As Integer
        'Dim Maillage As New cls_MaillageSlimFloor
        Dim bEffG, bEffD, bApp As Decimal

        Dim TimeTarget As Decimal
        Dim TimeT As Decimal = 0
        Dim lCont As Boolean
        Dim DeltaT As Decimal
        Dim lTargetT As Boolean = False

        Dim TempG As Decimal
        Dim EN_Feu As New cls_EurocodesFeu

        Dim SolveurTh As New cls_EchauffementSlimFEM

        Dim val_U As Double = Convert.ToDouble(myBeam.ParamFeu.TeneurU)
        Dim lNormal As Boolean = Not myBeam.Dalle.beton.lLeger
        Dim lANF As Boolean = myBeam.ParamFeu.lANFrance
        Dim lGeneration1 As Boolean = myBeam.Param.lGeneration1
        Dim lRhoCVar As Boolean = myBeam.ParamFeu.lRhoCvar
        Dim RhoC As Double = Convert.ToDouble(myBeam.Dalle.beton.RhoC)

        '--( Initialisation des variables

        Maillage = New cls_MaillageSlimFloor
        'If myBeam.lIntermediaire Then
        '    bEffG = myBeam.EntraxeD1 / 2
        'Else
        '    bEffG = myBeam.EntraxeD1
        'End If
        'bEffD = myBeam.EntraxeD2 / 2
        'bApp = 0.05
        ParamBeffMaillage(myBeam, bEffG, bEffD, bApp)

        '--( Construction du modèle numérique

        Maillage.Creation_maillage_2D_poutre_plancher_mince(myBeam.Section.ProfilA, myBeam.Dalle, myBeam.ParamFeu, bEffG, bEffD, myBeam.lIntermediaire, bApp)

        Me.InitialiseVariables(Maillage.nb_cells_y, Maillage.nb_cells_z)

        '--( Initialisation des températures 

        Maillage.InitialiseTemp(myBeam.ParamFeu.TempRef)

        '# GiB 02/12/2025 : définition de la matrice des températures à l'instant t en dehors de la procédure de calcul à chaque instant 
        Dim local_Temp_0(0 To Maillage.ind_1 - Maillage.ind_2, 0 To Maillage.nb_cells_z - 1) As Double
        Dim local_Expo(0 To Maillage.ind_1 - Maillage.ind_2, 0 To Maillage.nb_cells_z - 1) As Boolean
        Dim local_NoExpo(0 To Maillage.ind_1 - Maillage.ind_2, 0 To Maillage.nb_cells_z - 1) As Boolean

        '# GiB 20/04/2026 : remplissage des matrices des faces extérieures au feu
        Maillage.Faces_exterieures(local_Expo, local_NoExpo, local_Temp_0)

        '--( Boucle sur TimeSteps

        Dim Struct_ValeursProgressionEtapes As Struc_MAJEtape
        For iSTep = 0 To Me.NbStep - 1

            'pour utiliser les timestep de debug (2,3,4) qui sont définis dans cette classe
            TimeTarget = TimeSteps(iSTep) * kConvMinSec
            'pour utiliser la liste globale
            'TimeTarget = cls_VerifFeuMixte.TimeSteps(iSTep) * kConvMinSec

            lCont = IsSmaller(TimeT, TimeTarget)

            'On met à jour la progression sur les étapes 
            Dim decalage As Integer = 0
            If progressEtape IsNot Nothing Then
                Dim percentGlobal As Integer = CInt((iSTep / Me.NbStep) * 100)

                Struct_ValeursProgressionEtapes.ValeurProgression = percentGlobal       'progression en % du calcul global (toutes les étapes)
                Struct_ValeursProgressionEtapes.ValeurEtape = TimeTarget / kConvMinSec  'temps en minutes de l'étape dont le calcul est en cours
                progressEtape.Report(Struct_ValeursProgressionEtapes)
                progressDansEtape.Report(0)

                'Decalage pour calculer la progression dans l'étape
                If iSTep <> 0 Then decalage = TimeSteps(iSTep - 1) * kConvMinSec
            End If

            Do While lCont

                '# GiB 20/04/2026 : actualisation de la matrice local_Temp_0 à chaque pas de temps
                SolveurTh.MAJ_Matrice_calcul(Maillage, local_Temp_0)

                '# Boucle sur le temps jusqu'à obtenir la durée cible

                'DeltaT = IncrementTemps(TimeT, myBeam.lIntermediaire)
                DeltaT = SolveurTh.Increment_Temps(TimeT)
                TimeT += DeltaT

                '# Température des gaz chauds

                TempG = EN_Feu.TemperatureGazISO(TimeT)

                '# Calcul de l'échauffement de la section à l'instant TimeT

                lTargetT = IsSmaller(TimeT, TimeTarget, 10 ^ (-4))
                SolveurTh.Calcul_thermique_Poutre_plancher_mince(Maillage, myBeam.ParamFeu, TimeT, DeltaT, lTargetT, TempG,
                                                                 val_U, lNormal, lANF, lGeneration1, RhoC, lRhoCVar,
                                                                 local_Temp_0, local_Expo, local_NoExpo)

                'On met à jour la progression dans l'étape en cours
                If progressDansEtape IsNot Nothing Then

                    Dim progression As Integer = CInt(((TimeT - decalage) / (TimeTarget - decalage)) * 100)
                    progression = Math.Min(progression, 100)
                    progressDansEtape.Report(progression)

                End If

                lCont = lTargetT

            Loop

            '# Enregistrement des températures du maillage à l'instant TimeTarget

            For iY As Integer = 0 To Maillage.nb_cells_y - 1
                For iZ As Integer = 0 To Maillage.nb_cells_z - 1
                    Me.TempMailStep(iSTep, iY, iZ) = Maillage.Tab_mesh_temp(iY, iZ)
                Next
            Next

        Next

        'MAJ de progression final pour avoir le 100%
        If progressEtape IsNot Nothing Then
            Dim percentGlobal As Integer = CInt((iSTep / Me.NbStep) * 100)
            Struct_ValeursProgressionEtapes.ValeurProgression = percentGlobal
            Struct_ValeursProgressionEtapes.ValeurEtape = TimeTarget / kConvMinSec

            progressEtape.Report(Struct_ValeursProgressionEtapes)
        End If

        myBeam.lCalculOK = True
        myBeam.lCalculCharge = True
        myBeam.lCalculSauve = False

    End Sub

    Private Function IncrementTemps(TimeT As Decimal, lInter As Boolean) As Decimal
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Incrément de temps variable
        '---------------------------------------------------------------------------------------------------------
        '   TimeT       [E] :   Temps actuel
        '   lInter      [E] :   Poutre intérmédiaire ou pas
        '---------------------------------------------------------------------------------------------------------

        Dim DeltaT As Decimal = 0.2

        If IsGreaterOrEqual(TimeT, 600.0) AndAlso IsSmaller(TimeT, 900.0) Then
            DeltaT = 0.25
        ElseIf IsGreaterOrEqual(TimeT, 900.0) Then
            If lInter Then   'poutre intérieure
                If IsSmaller(TimeT, 1200.0) Then
                    DeltaT = 0.3
                ElseIf IsSmaller(TimeT, 1800.0) Then
                    DeltaT = 0.4
                ElseIf IsSmaller(TimeT, 3600.0) Then
                    DeltaT = 0.5
                ElseIf IsSmaller(TimeT, 5400.0) Then
                    DeltaT = 0.6
                ElseIf IsSmaller(TimeT, 7200.0) Then
                    DeltaT = 0.75
                Else
                    DeltaT = 1.0
                End If
            ElseIf IsSmaller(TimeT, 2700.0) Then
                DeltaT = 0.3
            ElseIf IsSmaller(TimeT, 3600.0) Then
                DeltaT = 0.4
            ElseIf IsSmaller(TimeT, 7200.0) Then
                DeltaT = 0.5
            Else
                DeltaT = 0.6
            End If

        End If

        Return DeltaT

    End Function

#End Region

#Region " Outils divers "

    Public Sub ParamBeffMaillage(myBeam As cls_Poutre, ByRef bEffG As Decimal, ByRef bEffD As Decimal, ByRef bApp As Decimal)
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Largeur de béton à droite et à gauhe de la section et pas du maillage
        '---------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   bEffG       [S] :   Largeur à gauche
        '   bEffD       [S] :   Largeur à droite
        '   bApp        [S] :   
        '---------------------------------------------------------------------------------------------------------

        If myBeam.lIntermediaire Then
            bEffG = myBeam.EntraxeD1 / 2
        Else
            bEffG = myBeam.EntraxeD1
        End If
        bEffD = myBeam.EntraxeD2 / 2
        bApp = 0.05

    End Sub

#End Region

End Class
