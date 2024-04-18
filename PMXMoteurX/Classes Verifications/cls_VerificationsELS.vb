Imports System.Drawing

Public Class cls_VerificationsELS

    '=========================================================================================================
    '=  CLASSE POUR LA VERIFICATION AUX ETATS LIMITES DE SERVICE
    '=========================================================================================================

#Region " Attributs "

    '==( Flèches 

    Public FlechesMaxCombi(,) As Decimal            ' Table des flèches maxi par combinaison et par travée
    Public FlechesMaxCombiETA(,) As Decimal         ' Table des flèches maxi par combinaison et par travée, prenant en compte le degré de connexion

    '==( Armatures transversales anti-fissuration

    Public AsSurSRetrait As Decimal                 ' Armatures longi anti fissuration (§ 7.4.2 de l'EN 1994-1-1:2005)
    Public DiaMaxi As Decimal                       ' Diamètre maximal des armatures, pour la maitrise de la fissuration
    Public EspMaxi As Decimal                       ' Espacement maximal des armatures, idem
    Public SigmaBar As Decimal                      ' Contrainte directe maxi
    Public DeltaSig As Decimal                      ' Effet de rigidité du béton tendu
    Public iCombiArma As Integer                    ' Indice de la combi ELS pour laquelle a on a la contrainte maxi

    '==( Frequences propres 

    Public FrequencesP(,) As Decimal                ' Frequences propres G+iQ

#End Region

#Region " Gestion globale "

    Public Sub Z_VerificationsELS(myBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------------------
        '   16/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELS d'une poutre
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre vérifiée
        '----------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim iCombi As Integer
        Dim UZCombi() As Decimal = Nothing
        Const lCombiRetrait As Boolean = True
        Dim lETA As Boolean
        Dim iTraveeDeb As Integer = myBeam.IndicePremiereTravee
        Dim iTraveeFin As Integer = myBeam.IndiceDerniereTravee

        Dim SigmaP(,,,) As Decimal = Nothing            ' Contraintes normales dans l'hypothèse d'un moment positif
        Dim SigmaM(,,,) As Decimal = Nothing            ' Contraintes normales dans l'hypothèse d'un moment négatif
        Dim SigmaELS(,,) As Decimal = Nothing           ' Contraintes normales sous 1 combinaison ELS
        Dim DeltaSG, DeltaSD As Decimal                 ' Contrainte dans les armatures de la dalle due à l'effet de rigidité du béton tendu
        Dim SigmaAG, SigmaAD As Decimal
        Dim lContinue As Decimal = (myBeam.NbTravees > 1)
        Dim bEff As Decimal
        Dim iNodeD, iNodeF As Integer
        Dim MEd(,) As Decimal = Nothing
        Dim lRetrait As Boolean = True
        Dim lMixte As Boolean = myBeam.lMixte
        Dim lDefQ(2) As Boolean

        '--> Initialisation de la classe

        Me.InitialiseELS(myBeam)

        '--> CALCUL DES FLECHES PAR COMBINAISON - Boucle sur les combinaisons ELS, sans prise en compte du glissement

        lETA = False
        For iCombi = 0 To myBeam.CombiA_ELS.nbCombi - 1
            myBeam.CombiA_ELS.CombineFleches(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, UZCombi, lCombiRetrait, lETA)
            ExtraireFlecheEnveloppes(UZCombi, iTraveeDeb, iTraveeFin, myBeam.Nodes.iNodeExtTrav, iCombi, Me.FlechesMaxCombi)
        Next

        '--> CALCUL DES FLECHES PAR COMBINAISON - Boucle sur les combinaisons ELS, avec prise en compte du glissement

        If (lMixte And myBeam.Param.lFlechesETA) Then
            lETA = True
            For iCombi = 0 To myBeam.CombiA_ELS.nbCombi - 1
                myBeam.CombiA_ELS.CombineFleches(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, UZCombi, lCombiRetrait, lETA)
                ExtraireFlecheEnveloppes(UZCombi, iTraveeDeb, iTraveeFin, myBeam.Nodes.iNodeExtTrav, iCombi, Me.FlechesMaxCombiETA)
            Next
        End If

        '--> Calcul des fréquences propres

        '--> Maitrise de la fissuration sous contraintes directes

        If myBeam.Param.lMaitriseFissuration And lContinue And lMixte Then

            '# Calcul des contraintes normales élastiques pour les cas de charges

            myBeam.PtsSigma.Initialise(myBeam)
            myBeam.PtsSigma.CalculContraintesCharges(myBeam, 1, SigmaP)
            myBeam.PtsSigma.CalculContraintesCharges(myBeam, -1, SigmaM)

            '# Boucle sur les combinaisons

            For iCombi = 0 To myBeam.CombiA_ELS.nbCombi - 1

                '# Cumul des moments

                myBeam.CombiA_ELS.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, lRetrait)

                '# Cumul des contraintes

                myBeam.CombiA_ELS.CombineContraintes(iCombi, myBeam.ChargesA.Count, myBeam.PtsSigma.zPos.Count, myBeam.Nodes.nbNodes,
                                                     myBeam.ChargesA, MEd, SigmaP, SigmaM, lRetrait, SigmaELS)

                '# Calcul des contraintes dans les armatures dues aux effets de rigidité du béton tendu

                If myBeam.lTraveeConsoleGauche Then
                    bEff = myBeam.BeffDalle(0, 1, myBeam.Param.lLargeurEfficaceSimplifiee, False)
                    DeltaSG = myBeam.Section.DeltaSigma(myBeam.Dalle, bEff)
                    iNodeD = myBeam.Nodes.iNodeExtTrav(0, 0)
                    iNodeF = CInt((myBeam.Nodes.iNodeExtTrav(1, 0) + myBeam.Nodes.iNodeExtTrav(1, 1)) / 2)
                    myBeam.PtsSigma.AjusteDeltaS(DeltaSG, SigmaELS, iNodeD, iNodeF)
                    SigmaAG = myBeam.PtsSigma.ContrainteMaxArmature(SigmaELS, myBeam.Nodes.iNodeExtTrav(1, 0))
                End If

                If myBeam.lTraveeConsoleDroite Then
                    bEff = myBeam.BeffDalle(myBeam.LongueurTravee(1), 1, myBeam.Param.lLargeurEfficaceSimplifiee, False)
                    DeltaSD = myBeam.Section.DeltaSigma(myBeam.Dalle, bEff)
                    iNodeF = myBeam.Nodes.iNodeExtTrav(myBeam.IndiceDerniereTravee, 1)
                    iNodeD = CInt((myBeam.Nodes.iNodeExtTrav(1, 0) + myBeam.Nodes.iNodeExtTrav(1, 1)) / 2)
                    myBeam.PtsSigma.AjusteDeltaS(DeltaSD, SigmaELS, iNodeD, iNodeF)
                    SigmaAD = myBeam.PtsSigma.ContrainteMaxArmature(SigmaELS, myBeam.Nodes.iNodeExtTrav(1, 1))
                End If

                '# Diamètre et espacement maxi

                Me.MaitriseFissurationDirecte(myBeam, SigmaAG, DeltaSG, SigmaAD, DeltaSD, iCombi)

            Next

        End If

        '# Calcul des armatures anti fissuration hors contraintes directes

        If myBeam.Param.lMaitriseFissuration And lMixte Then
            Me.ArmaturesAntiFissuration(myBeam)
        End If

        '# Calcul des fréquences propres

        myBeam.Modal.Analyse(myBeam, 0, -1)
        Me.FrequencesP(0, 0) = myBeam.Modal.Frequence
        Me.FrequencesP(0, 1) = myBeam.Modal.Frequence

        lDefQ(1) = myBeam.ChargesU("Q1").EstDefinie
        lDefQ(2) = myBeam.ChargesU("Q2").EstDefinie

        If lDefQ(1) Or lDefQ(2) Then
            For iCombi = 1 To 10
                For iQ As Integer = 1 To 2
                    If lDefQ(iQ) Then
                        myBeam.Modal.Analyse(myBeam, CDbl(iCombi / 10), iQ)
                        Me.FrequencesP(iCombi, iQ - 1) = myBeam.Modal.Frequence
                    End If
                Next
            Next
        End If

    End Sub

    Private Sub InitialiseELS(myBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------------------
        '   16/04/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Initialisation de la classe
        '----------------------------------------------------------------------------------------------------------

        Dim iTraveeFin As Integer = myBeam.IndiceDerniereTravee
        Dim nbCombi = myBeam.CombiA_ELS.nbCombi

        ReDim Me.FlechesMaxCombi(nbCombi - 1, iTraveeFin)
        If (myBeam.lMixte And myBeam.Param.lFlechesETA) Then _
            ReDim Me.FlechesMaxCombiETA(nbCombi - 1, iTraveeFin)

        Me.iCombiArma = -1
        Me.SigmaBar = 0
        Me.EspMaxi = -1
        Me.DiaMaxi = -1

        ReDim Me.FrequencesP(10, 1)

    End Sub
#End Region

#Region " Gestion des flèches "

    Private Sub ExtraireFlecheEnveloppes(UZ() As Decimal, iTravDeb As Integer, iTravFin As Integer, IndiceNoteT(,) As Integer,
                                         iCombi As Integer, ByRef FlechesMaxi(,) As Decimal)
        '-------------------------------------------------------------------------------------------
        '   22/11/23 :  Création - POM
        '-------------------------------------------------------------------------------------------
        '   Recherche des valeurs de flèches maxi par travée
        '-------------------------------------------------------------------------------------------
        '   UZ          [E] :   Tableau des flèches par noeuds
        '   iTravDeb    [E] :   Indice de la première travée
        '   iTravFin    [E] :   Indice de la dernière travée
        '   IndiceNoteT [E] :   Indice des noeuds aux extrémités des travées
        '   iCombi      [E] :   Indice de la combinaison
        '   FlechesMaxi [S] :   Flèches maxi par travée
        '-------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim jTravee As Integer
        Dim iNode As Integer

        '--> Boucle sur les travées

        For jTravee = iTravDeb To iTravFin
            FlechesMaxi(iCombi, jTravee) = UZ(IndiceNoteT(jTravee, 0))
            For iNode = IndiceNoteT(jTravee, 0) + 1 To IndiceNoteT(jTravee, 1)

                If IsGreater(Math.Abs(UZ(iNode)), Math.Abs(FlechesMaxi(iCombi, jTravee))) Then

                    FlechesMaxi(iCombi, jTravee) = UZ(iNode)

                End If

            Next
        Next

    End Sub


#End Region


#Region " Calcul des armatures anti fissuration "

    Private Sub MaitriseFissurationDirecte(myBeam As cls_Poutre, SigmaAG As Decimal, DeltaSG As Decimal, SigmaAD As Decimal, DeltaSD As Decimal, iCombi As Integer)
        '----------------------------------------------------------------------------------------------------------
        '   22/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Gestion du calcul des armatures anti fissuration, selon § 7.4 de l'EN 1994-1:2005
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   SigmaAG     [E] :   Contrainte dans l'armature sur appui gauche
        '   DeltaSG     [E] :   Partie de la contrainte appui gauche due à la rigidité du béton tendu
        '   SigmaAD     [E] :   Contrainte dans l'armature sur appui droite
        '   DeltaSD     [E] :   Partie de la contrainte appui droite due à la rigidité du béton tendu
        '   iCombi      [E] :   Indice de la combinaison
        '----------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim EN1994 As New cls_Eurocodes
        Dim lOK As Boolean
        Dim SigmaArma As Decimal
        Dim lGauche As Boolean

        '--( Sauvegarde des paramètres

        If IsGreater(SigmaAG, SigmaAD) Then
            lGauche = True
            SigmaArma = SigmaAG
        Else
            lGauche = False
            SigmaArma = SigmaAD
        End If

        If IsGreater(SigmaArma, SigmaBar) Then

            Me.SigmaBar = SigmaArma
            If lGauche Then Me.DeltaSig = DeltaSG Else Me.DeltaSig = DeltaSD
            Me.iCombiArma = iCombi

            '--( Diamètre maxi

            Me.DiaMaxi = EN1994.ExDiametreMaxFromTableau71(myBeam.Param.FissureWk, SigmaArma, lOK)

            '--( Espacement maxi

            Me.EspMaxi = EN1994.ExEspacementMaxFromTableau72(myBeam.Param.FissureWk, SigmaArma, lOK)

        End If

    End Sub

    Private Sub ArmaturesAntiFissuration(myBeam As cls_Poutre)
        '----------------------------------------------------------------------------------------------------------
        '   22/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Gestion du calcul des armatures anti fissuration, selon § 7.4 de l'EN 1994-1:2005 hors contraintes directes
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E]
        '   SigmaM      [E] :   Table des contraintes normales sous hypothèse M<0, par cas de charge
        '----------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim myAs As Decimal

        '--( Traitement

        If myBeam.Param.lMaitriseFissuration Then

            '# Anti fissuration en l'absence de contraintes directes

            Me.AsSurSRetrait = 0

            If Not myBeam.lTraveeConsoleGauche Then
                Me.MinimumReinforcement742(myBeam, True, Me.AsSurSRetrait)
            End If

            If Not myBeam.lTraveeConsoleDroite Then
                Me.MinimumReinforcement742(myBeam, False, myAs)
                Me.AsSurSRetrait = Math.Max(Me.AsSurSRetrait, myAs)
            End If

            ''# Anti fissuration sous contraintes directes

            'If myBeam.NbTravees > 1 Then
            '    MaitriseFissurationDirecte(myBeam, SigmaM)
            'End If

        End If

    End Sub

    Private Sub MinimumReinforcement742(myBeam As cls_Poutre, lAppGauche As Boolean, ByRef myAssurS As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   22/03/24 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Calcul de l'armature minimale anti fissuration, selon § 7.4.2 de l'EN 1994-1:2005
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E]
        '   lAppGauche  [E] :   Indique si calcul sur l'appui gauche
        '   myAssurS    [S] :   Section minimale d'armature
        '----------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim ks, kc, k As Decimal
        Dim FctEff As Decimal
        Dim SigmaS As Decimal
        Dim myEN1994 As New cls_Eurocodes
        Dim myDia As Decimal
        Dim lOK As Boolean

        '--( Initialisation

        k = 0.8
        ks = 0.9
        kc = myEN1994.CoefficientKc(myBeam, lAppGauche)

        FctEff = 3

        myDia = myBeam.Dalle.DiametreMaxiArma

        SigmaS = Math.Min(myBeam.Dalle.AcierArmatures.FsK, myEN1994.ExContrainteFromTableau71(myBeam.Param.FissureWk, myDia, lOK))

        '--( Calculs

        If IsGreater(SigmaS, 0) Then
            myAssurS = ks * kc * k * FctEff * myBeam.Dalle.EpaisseurActive / SigmaS
        Else
            myAssurS = -1
        End If

    End Sub

#End Region

End Class
