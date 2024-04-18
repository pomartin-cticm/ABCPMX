Public Class cls_VerifFeuEnrobe

#Region " Declaration "

    Public Shared TimeSteps() As Decimal = {30, 60, 90, 120, 180}

#End Region

#Region " Attributs "

    Public CritereM() As cls_Critere                  ' Resistance à la flexion
    Public CritereV() As cls_Critere                  ' Resistance effort tranchant
    Public CritereMV() As cls_Critere                 ' Résistance à l'interacion MV

    Private NbStep As Integer

#End Region

#Region " Constructeurs "

    Public Sub New()
        Me.NbStep = cls_VerifFeuEnrobe.TimeSteps.GetUpperBound(0) + 1
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
        '----------------------------------------------------------------------------------------------------------

        ReDim CritereM(Me.NbStep - 1)
        ReDim CritereV(Me.NbStep - 1)
        ReDim CritereMV(Me.NbStep - 1)

        For i As Integer = 0 To Me.NbStep - 1
            Me.CritereM(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereV(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
            Me.CritereMV(i) = New cls_Critere(NbNodes, NbCombi, IndDerniereT)
        Next

    End Sub

#End Region

#Region "===Gestion de la classe==="

    Private Sub Z_VerifFeu(myBeam As cls_Poutre)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '--------------------------------------------------------------------------------------------------------------------------
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim nbCombiELU As Integer
        Dim iStep, iCombi As Integer
        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing

        '--( Initialisation

        nbCombiELU = myBeam.CombiA_ELF.nbCombi
        Me.InitialiseCriteres(myBeam.Nodes.nbNodes, nbCombiELU, myBeam.IndiceDerniereTravee)

        '--( Boucle et calcul sur chaque time step

        For iStep = 0 To Me.NbStep - 1

            '# Calcul des résistances plastiques selon Annex F de EN 1994-1-2



            '# Boucle sur les combinaisons de calcul

            For iCombi = 0 To nbCombiELU - 1

                '## Combinaisons des moments

                myBeam.CombiA_ELF.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, False)

                '## Combinaison des efforts tranchants

                myBeam.CombiA_ELF.CombineEffortsT(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd, False)

            Next
        Next

    End Sub

#End Region

#Region " Calcul des propriétés "

    Private Sub MomentPlastiquePlus(mySection As cls_Section, myDalle As cls_Dalle, myOptions As cls_OptionsFeu, Gammas As cls_Gamma,
                                    lMixte As Boolean, Beff As Decimal, Time As Decimal, ByRef MplRd As Decimal, ByRef zANP As Decimal)
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
        '   Time        [E] :   Temps du calcul
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

        '--> Initialisation

        Hw = mySection.ProfilA.HauteurAmeHw
        FySup = mySection.FySup
        FyInf = mySection.FyInf
        FyW = mySection.FyW

        '--> Modélisation du profilé acier

        MaillageProfileAMPlus(mySection.ProfilA, Gammas, Time, RhoV, mySection.Enrobage.Ratio_bc, FySup, FyInf, FyW, myModele)

        '# Béton d'enrobage

        ' Dans l'annexe F, le béton d'enrobage est négligé dans le calcul à l'incendie sous moment positif

        '# Armatures de l'enrobage

        '    MaillageArmaturesEnrobage_YY(Gammas, MyModele)

        ''--> Dalle béton

        If lMixte And (Beff > 0) Then

            '# Dalle 

            MaillageDalleMPlus(myDalle, myOptions, Gammas, mySection.ProfilA.Bfs, Beff, Time, myModele)

        End If

        '--> Recherche de l'axe neutre plastique

        myModele.RechercheANP(Signe, zANP, lValeurRd)

        '--> Moment plastique

        MplRd = myModele.CalculMomentPlastique(Signe, zANP, lValeurRd)

    End Sub

    Private Sub MaillageDalleMPlus(myDalle As cls_Dalle, myOptions As cls_OptionsFeu, Gammas As cls_Gamma, Bfs As Decimal, Beff As Decimal, Time As Decimal, ByRef myModele As cls_ModeleP)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Maillage de la dalle béton en vue 
        '   du calcul du moment plastique positif sous incendie d'une section avec enrobage partiel
        '   suivant Annexe F de l'EN 1994-1-2
        '--------------------------------------------------------------------------------------------------------------------------
        '   myDalle     [E] :   Dalle
        '   myOptions   [E] :   Options de calcul à l'incendie
        '   Gammas      [E] :   Coefficients partiels
        '   Bfs         [E] :   Largeur de la semelle supérieure
        '   Beff        [E] :   Largeur efficace de la dalle dans la cas d'une poutre mixte
        '   Time        [E] :   Temps du calcul
        '   myModele    [S] :   Modelisation du profilé
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim Tc As Decimal = myDalle.EpaisseurActive
        Dim Aire As Decimal
        Dim HcFi, TcR, HcEff As Decimal
        Dim EN1994_1_2 As New cls_EurocodesFeu
        Const nEqDalle As Decimal = 1       '(Calcul plastique)

        '--( Initialisation

        HcFi = EN1994_1_2.AnnexF_ReductionDalle(Time)
        Select Case myDalle.type
            Case cls_Dalle.Enum_TypeDalle.Pleine, cls_Dalle.Enum_TypeDalle.PartiellementPrefabriquee
                HcFi = Math.Min(Tc, HcFi)
                TcR = Tc - HcFi
            Case cls_Dalle.Enum_TypeDalle.Mixte
                Select Case myDalle.Bac.Orientation
                    Case cls_Bac.Enum_Orientation.Perpendiculaire
                        Select Case myDalle.Bac.AppuiT
                            Case cls_Bac.EnuConfigTAppui.NervureEtBacContinus, cls_Bac.EnuConfigTAppui.BetonSeulContinu
                                If myDalle.Bac.lNervuresOuvertes Then
                                    HcFi = Math.Min(Tc, HcFi)
                                    TcR = Tc - HcFi
                                Else
                                    HcFi = Math.Min(myDalle.Bac.Hp, HcFi)
                                    TcR = myDalle.Ep_td - HcFi
                                End If
                            Case cls_Bac.EnuConfigTAppui.Discontinu
                                HcFi = Math.Min(Tc, HcFi)
                                TcR = Tc - HcFi
                        End Select
                    Case cls_Bac.Enum_Orientation.Parallele
                        If myDalle.Bac.lNervuresOuvertes Then
                            Tc = HcEff - myDalle.Bac.Hp
                            HcFi = Math.Min(Tc, HcFi)
                            TcR = Tc - HcFi
                        Else
                            HcFi = Math.Min(Tc, HcFi)
                            TcR = Tc - HcFi
                        End If
                End Select
        End Select

        '--> Maillage

        '# Partie supérieure
        Aire = Beff * TcR
        myModele.AddMaille(Aire, TcR, myDalle.zTop - TcR / 2, 0, 1, nEqDalle, myDalle.beton.Fck, myOptions.AlphaSlab, Gammas.GammaC_fi)

        '# Partie inférieure

        Aire = Bfs * HcFi
        myModele.AddMaille(Aire, HcFi, myDalle.zTop - TcR - HcFi / 2, 0, 1, nEqDalle, myDalle.beton.Fck, myOptions.AlphaSlab, Gammas.GammaC_fi)

    End Sub

    Private Sub MaillageProfileAMPlus(myProfile As cls_ProfilA, Gammas As cls_Gamma, Time As Decimal, RhoV As Decimal, Ratio_Bc As Decimal,
                                      FySup As Decimal, FyInf As Decimal, FyW As Decimal, ByRef myModele As cls_ModeleP)
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Maillage du profilé acier en vue 
        '   du calcul du moment plastique positif sous incendie d'une section avec enrobage partiel
        '   suivant Annexe F de l'EN 1994-1-2
        '--------------------------------------------------------------------------------------------------------------------------
        '   myProfile   [E] :   Profilé
        '   Gammas      [E] :   Coefficients partiels
        '   Time        [E] :   Temps du calcul
        '   RatioBc     [E] :   Ratio largeur Bc / largeur Bf
        '   FySup       [E] :   Limite d'élasticité de la semelle sup
        '   FyInf       [E] :   Limite d'élasticité de la semelle inf
        '   FyW         [E] :   Limite d'élasticité de l'âme
        '   myModele    [S] :   Modelisation du profilé
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim Hw As Decimal
        Dim zRef As Decimal = myProfile.zRefAraseSup 'Cote de l'arase supérieure de la semelle supérieure du profilé 
        Dim LargBfs As Decimal
        Dim EN1994_1_2 As New cls_EurocodesFeu
        Dim myBc As Decimal
        Dim ReducKa As Decimal
        Dim Hwl, Hwh As Decimal
        Dim Hwi, zHwi, Fywi As Decimal
        Const RatioHwi As Decimal = 1
        Dim NbHwi, iHwi As Integer
        Dim kcG, zcG As Decimal

        '--> Initialisation

        Hw = myProfile.HauteurAmeHw
        myBc = Ratio_Bc * myProfile.Bfs
        ReducKa = EN1994_1_2.AnnexF_ReducFyInf(Time, myProfile.Tfs, myProfile.ha, myBc)
        kcG = (10 - 3 * Math.PI) / (12 - 3 * Math.PI)

        '--> Modélisation du profilé acier

        '# Semelle supérieure

        LargBfs = myProfile.Bfs - 2 * EN1994_1_2.AnnexF_ReductionLargeurBfs(Time, myProfile.Tfs, myProfile.Bfs, myBc)
        myModele.AddMaille(LargBfs * myProfile.Tfs, myProfile.Tfs, zRef - myProfile.Tfs / 2, 1, 1, 1, FySup, 1, Gammas.GammaM_fi)

        '# Âme

        Hwl = EN1994_1_2.AnnexF_HauteurHwInf(Time, myProfile.Tw, myProfile.ha, myProfile.Bfs * Ratio_Bc, Hw)
        Hwh = Hw - Hwl

        '### Partie supérieure
        If IsGreater(Hwh, 0) Then
            myModele.AddMaille(Hwh * myProfile.Tw, Hwh, zRef - myProfile.Tfs - Hwh / 2, 1, 1, 1, FyW, (1 - RhoV), Gammas.GammaM_fi)
        End If

        '### Partie inférieure ' On discretise car la limite d'élasticité varie avec la position
        Hwi = myProfile.Tw * RatioHwi
        NbHwi = (Math.Ceiling(Hwl / Hwi))
        Hwi = Hwl / NbHwi
        For iHwi = 1 To NbHwi
            zHwi = (iHwi - 0.5) * Hwi
            Fywi = FyW * (1 - zHwi / Hwl * (1 - ReducKa))
            myModele.AddMaille(Hwi * myProfile.Tw, Hwi, zRef - myProfile.Tfs - Hwh - Hwi / 2, 1, 1, 1, Fywi, (1 - RhoV), Gammas.GammaM_fi)
        Next

        '# Semelle inférieure

        myModele.AddMaille(myProfile.AireFi, myProfile.Tfi, zRef - myProfile.ha + myProfile.Tfi / 2, 1, 1, 1, ReducKa * FyInf, 1, Gammas.GammaM_fi)

        If myProfile.Rcs > 0 Then

            '# Congés supérieurs

            If IsGreater(Hwh, kcG * myProfile.Rcs) Then
                '--| Cas où les congés sup sont dans la partie Hwh
                Fywi = FyW
            Else
                '--| Cas où les congés sup sont dans la partie Hwl
                zcG = kcG * myProfile.Rcs - Hwh
                Fywi = FyW * (1 - zcG * (1 - ReducKa))
            End If

            myModele.AddMailleConges(myProfile.Rcs, zRef - myProfile.Tfs, 1, 1, 1, Fywi, (1 - RhoV), Gammas.GammaM_fi, cls_Maille.EnuTypeMaille.CongeSup)

        End If

        If myProfile.Rci > 0 Then

            '# Congés inférieurs

            If IsGreater(Hwl, kcG * myProfile.Rci) Then
                '--| Cas où les congés sup sont dans la partie Hwl
                zcG = Hwl - kcG * myProfile.Rci
                Fywi = FyW * (1 - zcG * (1 - ReducKa))
            Else
                '--| Cas où les congés sup sont dans la partie Hwh
                Fywi = FyW
            End If

            myModele.AddMailleConges(myProfile.Rci, zRef - myProfile.ha + myProfile.Tfi, 1, 1, 1, Fywi, (1 - RhoV), Gammas.GammaM_fi, cls_Maille.EnuTypeMaille.CongeInf)

        End If

    End Sub


#End Region


End Class
