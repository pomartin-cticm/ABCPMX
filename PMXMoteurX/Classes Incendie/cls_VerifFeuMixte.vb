Public Class cls_VerifFeuMixte

#Region " Declaration "

    Public Shared TimeSteps() As Decimal = {30, 60, 90, 120, 180, 240}              ' En minutes

#End Region

#Region " Attributs "

    Public CritereM() As cls_Critere                    ' Resistance à la flexion
    Public CritereV() As cls_Critere                    ' Resistance effort tranchant
    Public CritereMV() As cls_Critere                   ' Résistance à l'interacion MV

    Private NbStep As Integer                           ' Nombre d'items dans le tableau TimeSteps

    Public RStep As Integer                             ' Indice du dernier pas de calcul de la table TimeStep pour laquelle tous les critères sont OK

    Dim TempFsStep() As Decimal                         ' Température de la semelle supérieure pour les Steps
    Dim TempFiStep() As Decimal                         ' Température de la semelle inférieure pour les Steps
    Dim TempWStep() As Decimal                          ' Température de l'âme pour les Steps

    Dim TempDalleStep(,) As Decimal                     ' Température des deux faces de la dalle pour les Steps

#End Region


#Region " Constructeurs "

    Public Sub New()
        Me.NbStep = cls_VerifFeuEnrobe.TimeSteps.GetUpperBound(0) + 1
        Me.RStep = -1
    End Sub

    Private Sub InitialiseClassePourCalcul(NbNodes As Integer, NbCombi As Integer, IndDerniereT As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   30/10/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialisation des critères pour une poutre acier sans enrobage
        '----------------------------------------------------------------------------------------------------------
        '   NbNodes     [E] :   Nombre de noeuds
        '   NbCombi     [E] :   Nombre de combinaisons
        '   IndDerniereT[E] :   Indice de la dernière travée
        '----------------------------------------------------------------------------------------------------------

        ReDim CritereM(Me.NbStep - 1)
        ReDim CritereV(Me.NbStep - 1)
        ReDim CritereMV(Me.NbStep - 1)

        For i As Integer = 0 To Me.NbStep - 1
            Me.CritereM(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereV(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereMV(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Next

        ReDim TempFsStep(Me.NbStep - 1)
        ReDim TempFiStep(Me.NbStep - 1)
        ReDim TempWStep(Me.NbStep - 1)

        ReDim TempDalleStep(Me.NbStep - 1, 1)

    End Sub

#End Region


#Region "===Gestion de la classe==="

    Public Sub Z_VerifFeu(myBeam As cls_Poutre)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Gestion des calculs au feu pour les poutres mixtes non enrobées de béton
        '--------------------------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim nbCombiELU As Integer

        Dim TimeT As Decimal = 0
        Dim TimeTarget As Decimal
        Dim iSTep As Integer
        Dim DeltaT As Decimal
        Dim lCont As Boolean

        Dim TempG As Decimal                    ' Température des gaz chauds
        Dim TempFs As Decimal                   ' Température de la semelle supérieure
        Dim TempFi As Decimal                   ' Température de la semelle inférieure
        Dim TempW As Decimal                    ' Température de l'âme

        Dim kReducYFs As Decimal                ' Coefficient réduction limite d'élasticité en fct température de la semelle sup
        Dim kReducYFi As Decimal                ' Coefficient réduction limite d'élasticité en fct température de la semelle inf
        Dim kReducYW As Decimal                 ' Coefficient réduction limite d'élasticité en fct température de l'âme

        Dim MassivFs As Decimal                 ' Massiveté de la semelle sup
        Dim MassivFi As Decimal                 ' Massiveté de la semelle inf
        Dim MassivW As Decimal                  ' Massiveté de l'âme
        Dim kSh As Decimal                      ' Facteur d'ombre de la section

        Dim iCombi As Integer

        Dim EN_Feu As New cls_EurocodesFeu
        Dim lProtege As Boolean
        Dim lSsExposee As Boolean               ' Indique si la semelle supérieure est exposée au feu

        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing

        Dim MplRdFeu() As Decimal               ' Moments résistants plastiques aux Time Steps
        Dim MelRdFeu() As Decimal               ' Moments résistants élastiques aux Time Steps
        Dim VplRdFeu() As Decimal               ' Efforts tranchant résistants plastiques aux Time Steps

        '--( Paramètres pour la discrétisation de la dalle

        Dim NbTranches As Integer               ' Nombre de tranches discrétisant la dalle
        Dim EpTranche() As Decimal = Nothing    ' Epaisseur de chaque tranche (indice 0 pour la tranche inférieure)
        Dim zTranche() As Decimal = Nothing     ' Position z de la fibre moyenne de chaque tranche
        Dim TempCTranche() As Decimal = Nothing ' Température de chaque tranche
        Dim EpDalle As Decimal                  ' Epaisseur de dalle constante utilisée pour le calcul

        '--( Initialisation

        TempG = myBeam.ParamFeu.TempRef
        TempFs = myBeam.ParamFeu.TempRef
        TempFi = myBeam.ParamFeu.TempRef
        TempW = myBeam.ParamFeu.TempRef

        DeltaT = myBeam.ParamFeu.DeltaTCalcul
        nbCombiELU = myBeam.CombiA_ELF.nbCombi
        lProtege = (myBeam.ParamFeu.TypeSurface = cls_OptionsFeu.enu_TypeSurface.Protege)
        MassivFs = EN_Feu.MassiveteSemelleSup(myBeam.Section.ProfilA, lSsExposee)
        MassivFi = EN_Feu.MassiveteSemelleInf(myBeam.Section.ProfilA)
        MassivW = EN_Feu.MassiveteAme(myBeam.Section.ProfilA)

        If Not lProtege Then
            kSh = EN_Feu.kShMixte(myBeam.Section.ProfilA)
        End If

        ReDim MplRdFeu(Me.NbStep - 1)
        ReDim MelRdFeu(Me.NbStep - 1)
        ReDim VplRdFeu(Me.NbStep - 1)

        '--( Préparation du maillage de la dalle

        '--( Boucle sur TimeSteps

        For iSTep = 0 To Me.NbStep - 1

            TimeTarget = cls_VerifFeuAcier.TimeSteps(iSTep) * kConvMinSec
            lCont = IsSmaller(TimeT, TimeTarget)

            Do While lCont

                '# Boucle sur le temps jusqu'à obtenir la durée cible

                TimeT += DeltaT

                '# Température des gaz chauds

                TempG = EN_Feu.TemperatureGazISO(TimeT)

                '# Calcul de l'échauffement de la section acier sur le pas de temps

                If lProtege Then

                Else
                    TempFs += EN_Feu.DeltaTempAcierNonProtege(TempFs, TempG, MassivFs, kSh, DeltaT, myBeam.ParamFeu)
                    TempFi += EN_Feu.DeltaTempAcierNonProtege(TempFi, TempG, MassivFi, kSh, DeltaT, myBeam.ParamFeu)
                    TempW += EN_Feu.DeltaTempAcierNonProtege(TempW, TempG, MassivW, kSh, DeltaT, myBeam.ParamFeu)
                End If

                '# Calcul de l'échauffement de la dalle sur le pas de temps (cas de la méthode numérique)

                If myBeam.ParamFeu.lDalleFEM Then

                End If

                '# 

                lCont = IsSmaller(TimeT, TimeTarget)

            Loop

            '# Récupération des températures dans les parties en acier

            TempFsStep(iSTep) = TempFs
            TempFiStep(iSTep) = TempFi
            TempWStep(iSTep) = TempW

            '# Calcul des températures dans le béton dans le cas de la méthode tabulée

            If Not myBeam.ParamFeu.lDalleFEM Then
                EN_Feu.TemperatureDalleTabulee(TimeSteps(iSTep), myBeam.Param.lGeneration1, NbTranches, TempCTranche)
            End If

            '# Réduction des propriétés de l'acier en fct de la température

            kReducYFs = EN_Feu.ReducFyAcier(TempFs)
            kReducYFi = EN_Feu.ReducFyAcier(TempFi)
            kReducYW = EN_Feu.ReducFyAcier(TempW)

            '# Résistance de la section 



        Next

        '# Boucle sur les combinaisons de calcul pour vérifications

        For iCombi = 0 To nbCombiELU - 1

            '## Combinaisons des moments

            myBeam.CombiA_ELF.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, False)

            '## Combinaison des efforts tranchants

            myBeam.CombiA_ELF.CombineEffortsT(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd, False)

            '## Classification



            '## Vérification en flexion

            'RunCriteresMomentsPlastiques(myBeam, iCombi, iSTep, MEd, MRdPos, MRdNeg)

            '## Vérification à l'effort tranchant

            'RunCriteresEffortTranchant(myBeam, iCombi, iSTep, VEd, VRd)

        Next

    End Sub


#End Region


#Region " Propriétés des sections mixtes en fonction de la température "

    Private Sub MomentPlastiquePlus(mySection As cls_Section, myDalle As cls_Dalle, myOptions As cls_OptionsFeu, Gammas As cls_Gamma,
                                    Beff As Decimal, reducKyFs As Decimal, reducKyFi As Decimal, reducKyW As Decimal, ByRef MplRd As Decimal, ByRef zANP As Decimal)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Calcul du moment plastique positif sous incendie d'une section avec enrobage partiel
        '--------------------------------------------------------------------------------------------------------------------------
        '   mySection   [E] :   Section
        '   myDalle     [E] :   Dalle
        '   myOptions   [E] :   Options de calcul à l'incendie
        '   Gammas      [E] :   Coefficients partiels
        '   lMixte      [E] :   Indique si mixité avec la dalle
        '   Beff        [E] :   Largeur efficace de la dalle dans la cas d'une poutre mixte
        '   reducKyFs   [E] :   Réduction de la limite d'élasticité de la semelle sup
        '   reducKyFi   [E] :   Réduction de la limite d'élasticité de la semelle inf
        '   reducKyW    [E] :   Réduction de la limite d'élasticité de l'âme
        '   MplRd       [S] :   Moment plastique de calcul
        '   zANP        [S] :   Position de l'ANP
        '--------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim myModele As New cls_ModeleP
        Dim Hw As Decimal
        Dim lLamine As Boolean = mySection.lLamine

        Const RhoV As Decimal = 0
        Dim FySup, FyInf, FyW As Decimal
        Const Signe As Decimal = 1
        Const lValeurRd As Boolean = True
        Const lMixte As Boolean = True

        '--> Initialisation

        Hw = mySection.ProfilA.HauteurAmeHw
        FySup = mySection.FySup
        FyInf = mySection.FyInf
        FyW = mySection.FyW

        '--> Modélisation du profilé acier

        MaillageProfileAMPlus(mySection.ProfilA, Gammas.GammaM_fi, RhoV, mySection.Enrobage.Ratio_bc, reducKyFs * FySup, FyInf, FyW, myModele)

        ''--> Dalle béton

        If lMixte And (Beff > 0) Then

            '# Dalle 

            '    MaillageDalleMPlus(myDalle, myOptions, Gammas, mySection.ProfilA.Bfs, Beff, Time, myModele)

        End If

        '--> Recherche de l'axe neutre plastique

        myModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = myModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub

    Private Sub MaillageProfileAMPlus(myProfile As cls_ProfilA, GammaM_fi As Decimal, RhoV As Decimal, Ratio_Bc As Decimal,
                                      FySup As Decimal, FyInf As Decimal, FyW As Decimal, ByRef myModele As cls_ModeleP)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Maillage du profilé acier en vue 
        '   du calcul du moment plastique positif sous incendie d'une section mixte sans enrobage partiel
        '--------------------------------------------------------------------------------------------------------------------------
        '   myProfile   [E] :   Profilé
        '   GammaM_fi   [E] :   Coefficient partiel pour l'acier en situation d'incendie
        '   Time        [E] :   Temps du calcul
        '   RatioBc     [E] :   Ratio largeur Bc / largeur Bf
        '   FySup       [E] :   Limite d'élasticité de la semelle sup   (tenant compte de la réduction due à la température)
        '   FyInf       [E] :   Limite d'élasticité de la semelle inf   (tenant compte de la réduction due à la température)
        '   FyW         [E] :   Limite d'élasticité de l'âme            (tenant compte de la réduction due à la température)
        '   myModele    [S] :   Modelisation du profilé
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Hw As Decimal
        Dim zRef As Decimal = myProfile.zRefAraseSup        'Cote de l'arase supérieure de la semelle supérieure du profilé 

        '--> Initialisation

        Hw = myProfile.HauteurAmeHw

        '--> Modélisation du profilé acier

        '# Semelle supérieure

        myModele.AddMaille(myProfile.AireFs, myProfile.Tfs, zRef - myProfile.Tfs / 2, 1, 1, 1, FySup, 1, GammaM_fi)

        '# Âme

        myModele.AddMaille(Hw * myProfile.Tw, Hw, zRef - myProfile.Tfs - Hw / 2, 1, 1, 1, FyW, (1 - RhoV), GammaM_fi)

        '# Semelle inférieure

        myModele.AddMaille(myProfile.AireFi, myProfile.Tfi, zRef - myProfile.ha + myProfile.Tfi / 2, 1, 1, 1, FyInf, 1, GammaM_fi)

        If myProfile.Rcs > 0 Then

            '# Congés supérieurs

            myModele.AddMailleConges(myProfile.Rcs, zRef - myProfile.Tfs, 1, 1, 1, FyW, (1 - RhoV), GammaM_fi, cls_Maille.EnuTypeMaille.CongeSup)

        End If

        If myProfile.Rci > 0 Then

            '# Congés inférieurs

            myModele.AddMailleConges(myProfile.Rci, zRef - myProfile.ha + myProfile.Tfi, 1, 1, 1, FyW, (1 - RhoV), GammaM_fi, cls_Maille.EnuTypeMaille.CongeInf)

        End If

    End Sub



#End Region



End Class
