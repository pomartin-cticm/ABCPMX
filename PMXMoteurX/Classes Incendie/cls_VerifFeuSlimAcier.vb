Imports System.Net
Imports System.Reflection
Imports System.Windows.Forms.LinkLabel
Imports Microsoft.VisualBasic.Devices

Public Class cls_VerifFeuSlimAcier

#Region " Declaration "

    Public Shared TimeSteps() As Decimal = {30, 60, 90, 120, 180}
    'Public Shared TimeSteps() As Decimal = {10, 15, 17, 20}
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

#Region " Gestion du maillage en accès - GETTERS "

    Public Function GetMaillage() As cls_MaillageSlimFloor
        Return Maillage
    End Function

    Public ReadOnly Property Maillage_zPos(iY As Integer, iZ As Integer) As Decimal
        Get
            Return Me.Maillage.Tab_mesh_cent_z(iY, iZ)
        End Get
    End Property

    Public ReadOnly Property MaillageEpaisseur(iZ As Integer) As Decimal
        Get
            Return Me.Maillage.Tab_mesh_z(iZ)
        End Get
    End Property

    Public ReadOnly Property MaillageLargeur(iY As Integer) As Decimal
        Get
            Return Me.Maillage.Tab_mesh_y(iY)
        End Get
    End Property

    Public ReadOnly Property MaillageIndMat(iY As Integer, iZ As Integer) As Integer
        Get
            Return Me.Maillage.Tab_mesh_mat(iY, iZ)
        End Get
    End Property

    Public Property Maillage_NbY As Integer
        Get
            Return Me.Maillage.nb_cells_y
        End Get
        Set(value As Integer)
            'Ne rien faire, le maillage est créé dans la méthode d'échauffement
        End Set
    End Property

    Public Property Maillage_NbZ As Integer
        Get
            Return Me.Maillage.nb_cells_z
        End Get
        Set(value As Integer)
            'Ne rien faire, le maillage est créé dans la méthode d'échauffement
        End Set
    End Property

    Public Property TemperatureMaille(iStep As Integer, iY As Integer, iZ As Integer) As Decimal
        Get
            Return Me.TempMailStep(iStep, iY, iZ)
        End Get
        Set(value As Decimal)
            Me.TempMailStep(iStep, iY, iZ) = value
        End Set
    End Property

#End Region

#Region " GENERAL "

    Public Sub Z_VerifFeu(myBeam As cls_Poutre, iBeam As Integer, FileNameP As String,
                     Optional progressEtape As IProgress(Of Struc_MAJEtape) = Nothing,
                     Optional progressDansEtape As IProgress(Of Integer) = Nothing)
        '--------------------------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Gestion des calculs au feu pour les poutres slim acier
        '--------------------------------------------------------------------------------------------------------------------------
        '   myBeam              [E] :   Poutre traitée
        '   iBeam               [E] :   Indice de la poutre traitée dans la liste des poutres du projet
        '   FileNameP           [E] :   Nom du fichier de sauvegarde du projet
        '   progressEtape       [E] :   Permet de d'envoyer l'information de progression sur les étapes de temps à la Frm_CalculEnCours
        '   progressDansEtape   [E] :   Permet de d'envoyer l'information de progression entre les étapes de temps à la Frm_CalculEnCours
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim nbCombiELF As Integer

        Dim MEd(,) As Decimal = Nothing
        Dim VEd(,) As Decimal = Nothing
        Dim QEd() As Decimal = Nothing                  ' tableau des forces nodales
        Dim QSupEd() As Decimal = Nothing               ' tableau des forces nodales / unité longueur

        Dim PsiY_spd(,) As Decimal = Nothing
        Dim PsiY_fi(,) As Decimal = Nothing

        Dim VbRdFi() As Decimal = Nothing
        Dim EtaMax As Decimal

        Dim TempBeton(NbStep)() As Decimal '= Nothing
        Dim TempArma(NbStep)() As Decimal '= Nothing
        Dim TempSemSup(NbStep)() As Decimal '= Nothing
        Dim TempSemInf(NbStep)() As Decimal '= Nothing
        Dim TempAme(NbStep)() As Decimal '= Nothing
        Dim TempPlat(NbStep)() As Decimal '= Nothing
        Dim TempSoud(NbStep)() As Decimal '= Nothing
        'Dim TempArma()() As Decimal = Nothing
        'Dim TempSemSup()() As Decimal = Nothing
        'Dim TempSemInf()() As Decimal = Nothing
        'Dim TempAme()() As Decimal = Nothing
        'Dim TempPlat()() As Decimal = Nothing
        'Dim TempSoud()() As Decimal = Nothing

        Dim MplRd(,) As Decimal = Nothing
        Dim zANP(,) As Decimal = Nothing

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
        'ReDim TempBeton(NbStep)(1)
        'ReDim TempArma(NbStep)(1)
        'ReDim TempSemSup(NbStep)(1)
        'ReDim TempSemInf(NbStep)(1)
        'ReDim TempAme(NbStep)(1)
        'ReDim TempPlat(NbStep)(1)
        'ReDim TempSoud(NbStep)(1)

        '# Calcul échauffement de la section (numérique)
        '--( Calcul de l'échauffement de la section

        If myBeam.lCalculOK Then
            If myBeam.lCalculCharge Then
                '# Cas où les résultats de calcul sont directement disponibles en mémoire
                ' On n'a juste à recréer le maillage

                RecupereTemperatureFile(myBeam, iBeam, FileNameP, False)

            Else
                '# Cas où les résultats de calcul sont disponibles dans le fichier de sauvegarde

                ' Il faut regénérer le maillage et 
                ' récupérer les températures du maillage dans le fichier

                RecupereTemperatureFile(myBeam, iBeam, FileNameP, True)

            End If
        Else
            '# Cas où les résultats de calcul ne sont pas disponibles

            Echauffement_SlimAcier(myBeam, progressEtape, progressDansEtape)

        End If

        '# Valeurs enveloppes des températures

        For iStep As Integer = 0 To NbStep - 1
            myBeam.VerifFeuSlimAcier.TemperatureStepMinMax(iStep, TempBeton(iStep), TempAme(iStep),
                                                                  TempSemInf(iStep), TempSemSup(iStep),
                                                                  TempPlat(iStep), TempSoud(iStep), TempArma(iStep))
        Next

        '# Résistance à l'effort tranchant

        CalculVbRdFeuSlimAcier(myBeam, VbRdFi)

        '# Boucle sur les combinaisons de calcul pour vérifications

        For iCombi = 0 To nbCombiELF - 1

            '## Combinaisons des moments

            myBeam.CombiA_ELF.CombineMoments(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, MEd, False)

            '## Combinaison des efforts tranchants

            myBeam.CombiA_ELF.CombineEffortsT(iCombi, myBeam.Nodes.nbNodes, myBeam.ChargesA, VEd, False)

            '# Recupération des efforts nodaux à partir des tranchants combinés

            myBeam.CombiA_ELF.RecupererEffortsNodauxPonderees(myBeam.Nodes, VEd, QEd, QSupEd)

            '# Calcul des coefficients de réduction 

            CalculCoefficientsReduction(iCombi, myBeam, QEd, TempPlat, TempSemInf, PsiY_spd, PsiY_fi)

            '# Moments resistants

            ProprietesFeuSlimAcier(iCombi, myBeam, True, MplRd, zANP, PsiY_fi, PsiY_spd)

            For iSTep = 0 To Me.NbStep - 1
                '# Vérification de la flexion transversale

                Me.RunCritereResistancePlastiquePlatY_N(myBeam, iCombi, QSupEd, iSTep, TempPlat(iSTep)(1), TempSemInf(iSTep)(1))

                Me.RunCritereResistanceFlexion(myBeam, iCombi, iSTep, MEd, MplRd)

                Me.RunCritereResistanceTranchant(myBeam, iCombi, iSTep, VEd, VbRdFi, EtaMax)

                If IsGreater(EtaMax, 0.5) Then
                    Me.RunCritereInteractionMV(myBeam, iCombi, iSTep, MEd, VEd, VbRdFi, PsiY_fi, PsiY_spd)
                End If
            Next

        Next

        '--( Recherche de la durée de résistance au feu

        DureeResistanceAuFeu()

    End Sub

    Private Sub DureeResistanceAuFeu()
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Recherche du pas de calcul pour lequel tous les critères sont OK
        '--------------------------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim lResist As Boolean

        '--( Traitement

        RStep = NbStep - 1
        lResist = IsResistanceAuFeuOK(RStep)

        Do While (Not lResist) And (Me.RStep >= 0)
            Me.RStep -= 1
            If Me.RStep >= 0 Then lResist = IsResistanceAuFeuOK(RStep)
        Loop

    End Sub

    Private Function IsResistanceAuFeuOK(iStep As Integer) As Boolean
        '--------------------------------------------------------------------------------------------------------------------------
        '   18/04/24 :  Création - POM
        '--------------------------------------------------------------------------------------------------------------------------
        '   Recherche du pas de calcul pour lequel tous les critères sont OK
        '--------------------------------------------------------------------------------------------------------------------------
        '   iStep      [E] :   Indice du pas de temps de calcul
        '--------------------------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim lOK As Boolean = True

        '--( Vérifications

        If IsGreater(Me.CritereMY(iStep).CritereMax, 1) Then lOK = False
        If IsGreater(Me.CritereM(iStep).CritereMax, 1) Then lOK = False
        If IsGreater(Me.CritereV(iStep).CritereMax, 1) Then lOK = False
        If IsGreater(Me.CritereMV(iStep).CritereMax, 1) Then lOK = False

        Return lOK

    End Function

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

#Region " Coefficients de réduction pour le plat inférieur "

    Public Sub CalculCoefficientsReduction(iCombi As Integer, myBeam As cls_Poutre, QEd() As Decimal,
                                           TempPl()() As Decimal, TempFi()() As Decimal,
                                           ByRef PsiY_spd(,) As Decimal, ByRef PsiY_fi(,) As Decimal)
        '-----------------------------------------------------------------------------------------------------------------------------
        '   31/07/25 :  Reprise
        '-----------------------------------------------------------------------------------------------------------------------------
        '   Calcul des coefficients de réduction liés à la flexion transversale des semelles inférieures de slim
        '   UNIQUEMENT AVEC LA METHODE EN1994
        '-----------------------------------------------------------------------------------------------------------------------------
        '   iCombi      [E] :   Indice de la combinaison
        '   myPoutre    [E] :   Poutre étudiée
        '   QEd         [E] :   Efforts nodaux de la poutre
        '-----------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iStep As Integer
        Dim iNode As Integer
        Dim iTravee, iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim deltaX As Decimal = myBeam.LongueurTotale / myBeam.Nodes.nbNodes

        Dim q, dApp, GammaM0 As Decimal
        Dim dbtFi, dbtPlat As Decimal

        Dim FyPlat, fyInf As Decimal

        Dim kChargeQ As Decimal

        Dim EN1994 As New cls_Eurocodes
        Dim ENFeu As New cls_EurocodesFeu

        Dim kReducPl(NbStep - 1) As Decimal
        Dim kReducFi(NbStep - 1) As Decimal

        '--> Initialisation

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        GammaM0 = myBeam.Param.Gamma.GammaM0

        dApp = myBeam.SlimLargeurAppui
        myBeam.Section.SlimBrasLevier(dApp, dbtFi, dbtPlat)

        FyPlat = myBeam.Section.FySpd
        fyInf = myBeam.Section.FyInf

        kChargeQ = CoefficientCharge(myBeam)

        ReDim PsiY_fi(NbStep - 1, myBeam.Nodes.nbNodes - 1)
        ReDim PsiY_spd(NbStep - 1, myBeam.Nodes.nbNodes - 1)

        For iStep = 0 To NbStep - 1
            kReducPl(iStep) = ENFeu.ReducFyAcier(TempPl(iStep)(1))
            kReducFi(iStep) = ENFeu.ReducFyAcier(TempFi(iStep)(1))
        Next

        '--> Traitement

        For iTravee = iDebT To iFinT

            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                With myBeam.Section.ProfilA

                    '== Suggestion pour DeltaX (POM)
                    If iNode = 0 Then
                        deltaX = (myBeam.Nodes.xGlobal(iNode + 1) - myBeam.Nodes.xGlobal(iNode)) / 2
                    ElseIf iNode = myBeam.Nodes.nbNodes - 1 Then
                        deltaX = (myBeam.Nodes.xGlobal(iNode) - myBeam.Nodes.xGlobal(iNode - 1)) / 2
                    Else
                        deltaX = (myBeam.Nodes.xGlobal(iNode + 1) - myBeam.Nodes.xGlobal(iNode - 1)) / 2
                    End If
                    '====

                    q = kChargeQ * QEd(iNode) / deltaX

                    For iStep = 0 To NbStep - 1
                        Select Case .typeProfileAcier
                            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

                                PsiY_spd(iStep, iNode) = EN1994.SlimCalculPsiY(q, dbtPlat, .Plat_t, kReducPl(iStep) * FyPlat, GammaM0)
                                PsiY_fi(iStep, iNode) = EN1994.SlimCalculPsiY(q, dbtFi, .Tfi, kReducFi(iStep) * fyInf, GammaM0)

                            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA

                                PsiY_spd(iStep, iNode) = EN1994.SlimCalculPsiY(q, dbtPlat, .Plat_t, kReducPl(iStep) * FyPlat, GammaM0)
                                PsiY_fi(iStep, iNode) = 1

                            Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB

                                PsiY_spd(iStep, iNode) = 1
                                PsiY_fi(iStep, iNode) = EN1994.SlimCalculPsiY(q, dbtFi, .Tfi, kReducFi(iStep) * fyInf, GammaM0)

                        End Select
                    Next

                End With

            Next
        Next

    End Sub

    Private Function CoefficientCharge(myBeam As cls_Poutre) As Decimal
        '----------------------------------------------------------------------------------------------------------
        '   10/10/25 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Renvoie la proportion de charge reprise de part et d'autre
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '----------------------------------------------------------------------------------------------------------

        Dim dGauche, dDroite As Decimal
        Dim kCote As Decimal

        dGauche = myBeam.EntraxeD1
        dDroite = myBeam.EntraxeD2
        If myBeam.lIntermediaire Then
            kCote = Math.Max(dGauche, dDroite) / (dGauche + dDroite)
        Else
            kCote = Math.Max(2 * dGauche, dDroite) / (2 * dGauche + dDroite)
        End If

        Return kCote

    End Function

#End Region

#Region " Résistance à l'effort tranchant "

    Public Sub CalculVbRdFeuSlimAcier(myBeam As cls_Poutre, ByRef VbRdFi() As Decimal)
        '------------------------------------------------------------------------------
        '   14/04/26 :  Création - POM
        '------------------------------------------------------------------------------
        '   Calcul de la résistance plastique VbRd en fct de l'échauffement
        '------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   VbRdFi      [S] :   Résistances plastiques VbRd en fct de la durée d'exposit
        '------------------------------------------------------------------------------

        '--( Déclarations

        Dim iStep As Integer
        Dim MailAme As New List(Of Integer())
        Dim AireAme As Decimal() = Nothing
        Dim NbW As Integer
        Dim iW As Integer
        Dim kReducY As Decimal
        Dim myTempW As Decimal
        Dim ENFeu As New cls_EurocodesFeu
        Dim RAC3 As Decimal = Math.Sqrt(3)
        Dim FyW As Decimal
        Dim GammaMFi As Decimal

        '--( Initialisations

        ReDim VbRdFi(NbStep - 1)
        ReperageMaillesAme(MailAme)
        NbW = MailAme.Count
        CalAireMaillesAme(MailAme, AireAme)

        GammaMFi = myBeam.Param.Gamma.GammaM_fi
        FyW = myBeam.Section.FyW

        '-- Récupération des mailles de l'âme

        '--( Calculs

        For iStep = 0 To NbStep - 1

            For iW = 0 To NbW - 1

                myTempW = Me.TemperatureMaille(iStep, MailAme(iW)(0), MailAme(iW)(1))
                kReducY = ENFeu.ReducFyAcier(myTempW)

                VbRdFi(iStep) += kReducY * AireAme(iW) * FyW / RAC3 / GammaMFi * kConvMPaPa

            Next

        Next

    End Sub

    Private Sub CalAireMaillesAme(mailWeb As List(Of Integer()), ByRef AireWeb() As Decimal)
        '------------------------------------------------------------------------------
        '   14/04/26 :  Création - POM
        '------------------------------------------------------------------------------
        '   Calcul des aires des mailles de l'âme
        '------------------------------------------------------------------------------
        '   MailWeb     [E] :   Liste des indices Y,Z des mailles de l'âme
        '   AireWeb     [S] :   Tables des aires de chacune des mailles de l'âme
        '------------------------------------------------------------------------------

        '--( Déclaration

        Dim nbMailW As Integer = mailWeb.Count
        Dim iW As Integer

        '--( Initialisation

        ReDim AireWeb(nbMailW - 1)

        '--( Traitement

        For iW = 0 To nbMailW - 1

            AireWeb(iW) = Me.Maillage.Tab_mesh_y(mailWeb(iW)(0)) * Me.Maillage.Tab_mesh_y(mailWeb(iW)(1))

        Next

    End Sub

    Private Sub ReperageMaillesAme(ByRef MailWeb As List(Of Integer()))
        '------------------------------------------------------------------------------
        '   14/04/26 :  Création - POM
        '------------------------------------------------------------------------------
        '   Récupération des mailles de l'âme
        '------------------------------------------------------------------------------
        '   MailWeb     [S] :   Liste des indices Y,Z des mailles de l'âme
        '------------------------------------------------------------------------------

        '--( Déclarations

        Dim iY, iZ As Integer
        'Dim myMail(1) As Decimal

        '--( Boucles sur les mailles

        For iY = 0 To Me.Maillage_NbY - 1
            For iZ = 0 To Maillage_NbZ - 1

                If Me.Maillage.Tab_mesh_mat(iY, iZ) = cls_MaillageSlimFloor.MATACIERAME Then
                    'myMail = {iY, iZ}
                    'MailWeb.Add(myMail)
                    MailWeb.Add({iY, iZ})
                End If

            Next
        Next

    End Sub

#End Region

#Region " Propriétés en flexion "

    Public Sub ProprietesFeuSlimAcier(iCombi As Integer, myBeam As cls_Poutre, lValRd As Boolean,
                                      ByRef MplRd(,) As Decimal, ByRef zANP(,) As Decimal,
                                      PsiY_fi(,) As Decimal, PsiY_spd(,) As Decimal)
        '------------------------------------------------------------------------------
        '   13/04/26 :  Création - POM
        '------------------------------------------------------------------------------
        '   Calcul des propriétés plastiques  le long de la barre en fonction de 
        '   du chargement et de la température
        '------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   indice de la combi en cours 
        '   lValRd      [E] :   indique si valeur de calcul (True) ou non (False)
        '   MplRd       [E] :   Moment plastique résistant
        '   zANP        [E] :   Axe neutre plastique
        '   Psi_fi      [E] :   Tables de réduction de fy pour la semelle inf (tenant compte de la flexion transversale)
        '   Psi_spd     [E] :   Tables de réduction de fy pour le plat (tenant compte de la flexion transversale)
        '------------------------------------------------------------------------------

        '--> Déclaration

        'Dim myModele As cls_ModeleP

        Dim NbNodes As Integer = myBeam.Nodes.nbNodes
        Dim iTravee As Integer
        Dim iTravDeb, iTravFin As Integer       ' Par principe, en fait toutes les poutres sont sans consoles
        Dim iNode As Integer
        Dim iNodeDeb, iNodeFin As Integer
        Dim kDeb, kfin As Integer
        Dim lEdge As Boolean = Not myBeam.lIntermediaire
        Dim lCalcul As Boolean
        'Const SIGNE As Decimal = 1

        '--> Initialisation

        iTravDeb = myBeam.IndicePremiereTravee
        iTravFin = myBeam.IndiceDerniereTravee

        ReDim zANP(NbStep - 1, NbNodes - 1)
        ReDim MplRd(NbStep - 1, NbNodes - 1)

        '--> Traitement

        For iStep As Integer = 0 To NbStep - 1

            For iTravee = iTravDeb To iTravFin

                iNodeDeb = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
                iNodeFin = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

                For iNode = iNodeDeb To iNodeFin
                    If iNode = iNodeDeb Then kDeb = 1 Else kDeb = 0
                    If iNode = iNodeFin Then kfin = 0 Else kfin = 1

                    If iNode = iNodeDeb Then
                        lCalcul = True
                    Else

                        lCalcul = (Not IsEqual(PsiY_fi(iStep, iNode), PsiY_fi(iStep, iNode - 1))) _
                              Or ((Not IsEqual(PsiY_spd(iStep, iNode), PsiY_spd(iStep, iNode - 1))))

                    End If

                    If lCalcul Then

                        '--> Construction du modèle de la section à partir des champs thermiques

                        'myModele = New cls_ModeleP
                        'myModele.MaillageSlimThermique(myBeam, iStep, PsiY_fi(iStep, iNode), PsiY_spd(iStep, iNode))

                        ''--> Recherche de l'axe neutre plastique

                        'myModele.RechercheANP(SIGNE, zANP(iStep, iNode), lValRd)

                        ''--> Moment plastique

                        'MplRd(iStep, iNode) = myModele.CalculMomentPlastique(SIGNE, zANP(iStep, iNode), lValRd)

                        ProprietePlastiqueMSlimFeu(myBeam, iStep, PsiY_fi(iStep, iNode), PsiY_spd(iStep, iNode),
                                                   zANP(iStep, iNode), MplRd(iStep, iNode))

                    Else

                        zANP(iStep, iNode) = zANP(iStep, iNode - 1)
                        MplRd(iStep, iNode) = MplRd(iStep, iNode - 1)

                    End If

                Next

            Next

        Next
    End Sub

    Private Sub ProprietePlastiqueMSlimFeu(myBeam As cls_Poutre, iStep As Integer, myPsiY_fi As Decimal, myPsiY_spd As Decimal,
                                           ByRef zANP As Decimal, ByRef MplRd As Decimal, Optional RhoV As Decimal = 0)
        '------------------------------------------------------------------------------
        '   13/04/26 :  Création - POM
        '------------------------------------------------------------------------------
        '   Calcul des propriétés plastiques  le long de la barre en fonction de 
        '   du chargement et de la température
        '------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iStep       [E] :   indice du champ de température à prendre en compte
        '   MplRd       [S] :   Moment plastique résistant
        '   zANP        [S] :   Axe neutre plastique
        '   myPsi_fi    [E] :   Coef de réduction de fy pour la semelle inf (tenant compte de la flexion transversale)
        '   myPsi_spd   [E] :   Coef de réduction de fy pour le plat (tenant compte de la flexion transversale)
        '   RhoV        [E] :   Coefficient d'interaction V
        '------------------------------------------------------------------------------

        '--> Déclaration

        Dim myModele As New cls_ModeleP
        Const SIGNE As Decimal = 1
        Const lValRd As Boolean = True

        '--( Initialisation

        myModele.MaillageSlimThermique(myBeam, iStep, RhoV, myPsiY_fi, myPsiY_spd)

        '--> Recherche de l'axe neutre plastique

        myModele.RechercheANP(SIGNE, zANP, lValRd)

        '--> Moment plastique

        MplRd = myModele.CalculMomentPlastique(SIGNE, zANP, lValRd)

    End Sub

#End Region

#Region " Lecture Récupération des champs thermiques dans le fichier de données "

    Private Sub RecupereTemperatureFile(myBeam As cls_Poutre, iBeam As Integer, FileNameP As String, lTemp As Boolean)
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Récupération des températures du maillage dans le fichier de sauvegarde
        '---------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iBeam       [E] :   Indice de la poutre traitée dans la liste des poutres du projet
        '   FileNameP   [E] :   Nom du fichier de sauvegarde du projet
        '   lTemp       [E] :   Indique si les températures doivent être récupérées ou non (utile pour la méthode d'échauffement qui n'a pas besoin de récupérer les températures)
        '---------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim bEffG, bEffD, bApp As Decimal

        '--( Initialisation des variables

        Maillage = New cls_MaillageSlimFloor

        ParamBeffMaillage(myBeam, bEffG, bEffD, bApp)

        '--( On commence par regénérer le maillage

        Maillage.Creation_maillage_2D_poutre_plancher_mince(myBeam.Section.ProfilA, myBeam.Dalle, myBeam.ParamFeu, bEffG, bEffD, myBeam.lIntermediaire, bApp)

        '--( Récupération des températures du maillage dans le fichier de sauvegarde

        If lTemp Then
            Me.InitialiseVariables(Maillage.nb_cells_y, Maillage.nb_cells_z)
            ChargerTemperatures(myBeam, iBeam, FileNameP)
        End If

    End Sub

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

    Private Sub ChargerTemperatures(myBeam As cls_Poutre, iBeam As Integer, FileNameP As String)
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Récupération des lignes dans le fichier de sauvegarde
        '---------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iBeam       [E] :   Indice de la poutre traitée dans la liste des poutres du projet
        '   FileNameP   [E] :   Nom du fichier de sauvegarde du projet
        '---------------------------------------------------------------------------------------------------------

        Dim indPosPoutres As List(Of Integer) = Nothing
        Dim Lines As List(Of String) = Nothing

        RecupereLinesFiles(FileNameP, Lines, indPosPoutres)

        LireChargerTemperatures(Lines, indPosPoutres, iBeam)

    End Sub

    Private Sub LireChargerTemperatures(Lines As List(Of String), iLPoutres As List(Of Integer), iBeam As Integer)
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Chargement des températures du maillage à partir des lignes du fichier de sauvegarde
        '---------------------------------------------------------------------------------------------------------
        '   Lines       [E] :   Lignes du fichier de sauvegarde du projet
        '   iLPoutres   [E] :   Indice des lignes ou demarrent chaque poutre du projet
        '   iBeam       [E] :   Indice de la poutre traitée dans la liste des poutres du projet
        '---------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim iStart As Integer
        Dim iStop As Integer
        Dim Blocs As New List(Of String)
        Dim indBlocs As New List(Of Integer)
        Dim pprj As New cls_Projet

        '--( Initialisation des variables
        If iBeam = iLPoutres.Count - 1 Then
            iStart = iLPoutres(iBeam)
            iStop = Lines.Count - 1
        Else
            iStart = iLPoutres(iBeam)
            iStop = iLPoutres(iBeam + 1) - 1
        End If

        pprj.RepereLignesBlocPoutre(Lines, Blocs, indBlocs, iStart)

        '--( Déclarations

        Dim iBloc, nbBlocs As Integer
        Dim iFin As Integer
        Dim MotCle As String
        Dim NbCar As Integer = pprj.NomBlocThermiqueR.Length
        Dim lOK As Boolean = False
        Dim StepR As String

        Dim BkCALCULTH As String = pprj.NomBlocCalculThermique.ToUpper
        Dim BkTHR As String = pprj.NomBlocThermiqueR.ToUpper

        '--( Initialisation

        nbBlocs = Blocs.Count

        '--( Boucle sur les blocs

        For iBloc = 0 To nbBlocs - 1

            If iBloc = nbBlocs - 1 Then iFin = iStop Else iFin = indBlocs(iBloc + 1) - 1

            If Blocs(iBloc) = BkCALCULTH Then
                '# Recupération et contrôle du nombre de mailles en Y et Z du maillage thermique

                ReadBlocCalculTh(Lines, indBlocs(iBloc), iFin, lOK)

            Else

                MotCle = Blocs(iBloc).Substring(0, Math.Min(NbCar, Blocs(iBloc).Length)).ToUpper

                If (MotCle = BkTHR.ToUpper) And lOK Then

                    StepR = Blocs(iBloc).Substring(BkTHR.Length).Trim

                    ReadBlocThermiqueR(Lines, indBlocs(iBloc), iFin, StepR.Substring(1))

                End If

            End If

        Next

    End Sub

    Private Sub ReadBlocThermiqueR(Lines As List(Of String), Index0 As Integer, IndexFin As Integer, StepR As String)
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Lecture du bloc de températures du maillage à l'instant de temps StepR
        '---------------------------------------------------------------------------------------------------------
        '   Lines       [E] :   Lignes du fichier de sauvegarde du projet
        '   Index0      [E] :   Indice de la première ligne du bloc
        '   IndexFin    [E] :   Indice la dernière ligne du bloc
        '   StepR       [E] :   Durée cible du pas de temps traité
        '---------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim i, j As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String
        Const NBCAR As Integer = 6

        Dim iColumnY As Integer
        Dim iMailleZ As Integer
        Dim iStep As Integer

        '--( Initialisation

        iStep = Array.IndexOf(cls_VerifFeuSlimAcier.TimeSteps, CDec(Val(StepR)))

        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lines(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(NBCAR, Mots(1).Length)).ToUpper

                Select Case MotCle
                    Case "COLUMN"

                        iColumnY = CInt(Val(Mots(2)))

                        For j = 3 To nbMots

                            iMailleZ = j - 3

                            If iMailleZ < Me.Maillage_NbZ Then
                                Me.TempMailStep(iStep, iColumnY, iMailleZ) = CDec((Mots(j)))
                            End If

                        Next

                End Select

            End If
        Next

    End Sub

    Private Sub ReadBlocCalculTh(ByVal Lignes As List(Of String),
                                 ByVal Index0 As Integer, ByVal IndexFin As Integer, ByRef lOk As Boolean)
        '-------------------------------------------------------------------------------------
        '   05/09/24 :  Création - POM
        '-------------------------------------------------------------------------------------
        '   Lecture du bloc BETON
        '-------------------------------------------------------------------------------------
        '   myBeton     [S] :   Béton à definir
        '   Lignes      [E] :   lignes extraites du fichier de données
        '   Index0      [E] :   Indice de la première ligne du bloc
        '   IndexFin    [E] :   Indice la dernière ligne du bloc
        '-------------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String
        Const NBCAR As Integer = 4

        Dim pNbY As Integer = -1
        Dim pNbz As Integer = -1

        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(NBCAR, Mots(1).Length)).ToUpper

                Select Case MotCle
                    Case "NB_Y" : pNbY = CInt(Val(Mots(nbMots)))
                    Case "NB_Z" : pNbz = CInt(Val(Mots(nbMots)))
                End Select

            End If
        Next

        '--( Contrôle des données

        lOk = True

        If pNbY <> Me.Maillage_NbY Then lOk = False
        If pNbz <> Me.Maillage_NbZ Then lOk = False

    End Sub

    Private Sub RecupereLinesFiles(FileNameP As String, ByRef Lines As List(Of String), ByRef iPoutres As List(Of Integer))
        '---------------------------------------------------------------------------------------------------------
        '   10/04/26 :  Création
        '---------------------------------------------------------------------------------------------------------
        '   Récupération des lignes dans le fichier de sauvegarde
        '---------------------------------------------------------------------------------------------------------
        '   FileNameP   [E] :   Nom du fichier de sauvegarde du projet
        '   Lines       [S] :   
        '   iPoutres    [S] :
        '---------------------------------------------------------------------------------------------------------

        '--( Déclaration des variables

        Dim Poutres As New List(Of String)          ' Nom des poutres contenues dans le fichier

        '--( Initialisation des variables

        iPoutres = New List(Of Integer)        ' Indice des poutres contenures dans le fichier
        Lines = New List(Of String)

        '--( Déclaration

        Dim lOK As Boolean

        '--( Récupération des lignes du projet

        cls_Projet.ReadLineFile(FileNameP, Lines, lOK)

        '--( Traitement

        cls_Projet.AnalyseFichier(Lines, Poutres, iPoutres)

    End Sub

#End Region

#Region " Echauffement "

    Private Sub Echauffement_SlimAcier(myBeam As cls_Poutre,
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

        Dim val_U As Double = myBeam.ParamFeu.TeneurU
        Dim lNormal As Boolean = Not myBeam.Dalle.beton.lLeger
        Dim lANF As Boolean = myBeam.ParamFeu.lANFrance
        Dim lGeneration1 As Boolean = myBeam.Param.lGeneration1
        Dim lRhoCVar As Boolean = myBeam.ParamFeu.lRhoCvar
        Dim RhoC As Double = myBeam.Dalle.beton.RhoC

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

                '# Boucle sur le temps jusqu'à obtenir la durée cible

                DeltaT = IncrementTemps(TimeT, myBeam.lIntermediaire)
                TimeT += DeltaT

                '# Température des gaz chauds

                TempG = EN_Feu.TemperatureGazISO(TimeT)

                '# Calcul de l'échauffement de la section à l'instant TimeT

                lTargetT = IsSmaller(TimeT, TimeTarget, 10 ^ (-4))
                SolveurTh.Calcul_thermique_Poutre_plancher_mince(Maillage, myBeam.ParamFeu, TimeT, DeltaT, lTargetT, TempG,
                                                                 val_U, lNormal, lANF, lGeneration1, RhoC, lRhoCVar)

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

#Region " Outils "

    Public Sub TemperatureStepMinMax(iStep As Integer, ByRef TempBeton() As Decimal, ByRef TempAme() As Decimal,
                                     ByRef TempSemInf() As Decimal, ByRef TempSemSup() As Decimal,
                                     ByRef TempPlat() As Decimal, ByRef TempSoud() As Decimal, ByRef TempArma() As Decimal)
        '---------------------------------------------------------------------------------------------------------
        '   13/04/26 :  Création - POM
        '---------------------------------------------------------------------------------------------------------
        '   Récupération des températures min et max du maillage au temps iStep
        '---------------------------------------------------------------------------------------------------------
        '   iStep       [E] :   Indice du pas de temps traité
        '   TempBeton   [S] :   Tableau des températures min et max des éléments béton
        '---------------------------------------------------------------------------------------------------------

        '--( Déclarations

        Dim iMailY As Integer
        Dim iMailZ As Integer
        Dim nbY As Integer = Maillage.nb_cells_y
        Dim nbZ As Integer = Maillage.nb_cells_z

        '--( Initialisation

        ReDim TempBeton(1)
        ReDim TempAme(1)
        ReDim TempArma(1)
        ReDim TempPlat(1)
        ReDim TempSemInf(1)
        ReDim TempSemSup(1)
        ReDim TempSoud(1)

        '--( Traitement

        For iMailY = 0 To nbY - 1
            For iMailZ = 0 To nbZ - 1

                Select Case Maillage.Tab_mesh_mat(iMailY, iMailZ)
                    Case cls_MaillageSlimFloor.MATBETON

                        TraitementTempMaille(iStep, iMailY, iMailZ, TempBeton)

                    Case cls_MaillageSlimFloor.MATACIERAME

                        TraitementTempMaille(iStep, iMailY, iMailZ, TempAme)

                    Case cls_MaillageSlimFloor.MATACIERSEMI

                        TraitementTempMaille(iStep, iMailY, iMailZ, TempSemInf)

                    Case cls_MaillageSlimFloor.MATACIERSEMS

                        TraitementTempMaille(iStep, iMailY, iMailZ, TempSemSup)

                    Case cls_MaillageSlimFloor.MATACIERPLAT

                        TraitementTempMaille(iStep, iMailY, iMailZ, TempPlat)

                    Case cls_MaillageSlimFloor.MATACIERSOUD

                        TraitementTempMaille(iStep, iMailY, iMailZ, TempSoud)

                    Case cls_MaillageSlimFloor.MATARMA

                        TraitementTempMaille(iStep, iMailY, iMailZ, TempArma)

                End Select

            Next
        Next

    End Sub

    Private Sub TraitementTempMaille(iStep As Integer, iMailY As Integer, iMailZ As Integer, ByRef TempPart() As Decimal)

        If TempPart(0) = 0 And TempPart(1) = 0 Then
            TempPart(0) = Me.TempMailStep(iStep, iMailY, iMailZ)
            TempPart(1) = Me.TempMailStep(iStep, iMailY, iMailZ)
        Else
            If Me.TempMailStep(iStep, iMailY, iMailZ) < TempPart(0) Then
                TempPart(0) = Me.TempMailStep(iStep, iMailY, iMailZ)
            End If
            If Me.TempMailStep(iStep, iMailY, iMailZ) > TempPart(1) Then
                TempPart(1) = Me.TempMailStep(iStep, iMailY, iMailZ)
            End If
        End If

    End Sub

#End Region

#Region " Vérifications "

    Private Sub RunCritereInteractionMV(myBeam As cls_Poutre, iCombi As Integer, iStep As Integer,
                                        MEd(,) As Decimal, VEd(,) As Decimal, VRdFi() As Decimal,
                                        PsiY_fi(,) As Decimal, PsiY_spd(,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   14/04/26 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion des sections sous températures
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iStep       [E] :   Indice du pas de temps de calcul
        '   MEd         [E] :   Moments aux noeuds
        '   VEd         [E] :   Efforts tranchants aux noeuds
        '   VRdFi       [E] :   Table des résistances à l'effort tranchant (0 à NbStep-1)
        '   Psi_fi      [E] :   Tables de réduction de fy pour la semelle inf (tenant compte de la flexion transversale)
        '   Psi_spd     [E] :   Tables de réduction de fy pour le plat (tenant compte de la flexion transversale)
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Dim iTravee, iNode As Integer
        Dim Eta As Decimal
        Dim zANP As Decimal
        Dim MplVRd As Decimal
        Dim RhoV As Decimal

        '--( Initialisation

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT

            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                Eta = Math.Abs(VEd(iNode, iDebK) / VRdFi(iStep))
                If iFinK > iDebK Then Eta = Math.Max(Eta, Math.Abs(VEd(iNode, iFinK) / VRdFi(iStep)))

                If IsGreater(Eta, 0.5) Then

                    RhoV = (2 * Eta - 1) ^ 2

                    ProprietePlastiqueMSlimFeu(myBeam, iStep, PsiY_fi(iStep, iNode), PsiY_spd(iStep, iNode),
                                               zANP, MplVRd, RhoV)

                    Me.CritereMV(iStep).EnregistreCritere(iNode, iCombi, iTravee, MEd(iNode, iDebK), MplVRd)

                End If
            Next
        Next

    End Sub

    Private Sub RunCritereResistanceTranchant(myBeam As cls_Poutre, iCombi As Integer, iStep As Integer,
                                              VEd(,) As Decimal, VRdFi() As Decimal, ByRef EtaMax As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   13/04/26 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion des sections sous températures
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iStep       [E] :   Indice du pas de temps de calcul
        '   VEd         [E] :   Efforts tranchants aux noeuds
        '   VRdFi       [E] :   Table des résistances à l'effort tranchant (0 à NbStep-1)
        '   EtaMax      [S] :   Ratio VEd/VRd max pour la combinaison
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Dim iTravee, iNode As Integer

        '--( Initialisation

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee
        EtaMax = 0

        '--> Traitement

        For iTravee = iDebT To iFinT

            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                For k = iDebK To iFinK

                    Me.CritereV(iStep).EnregistreCritere(iNode, iCombi, iTravee, VEd(iNode, k), VRdFi(iStep))

                    EtaMax = Math.Max(EtaMax, Math.Abs(VEd(iNode, k) / VRdFi(iStep)))

                Next
            Next
        Next

    End Sub

    Private Sub RunCritereResistanceFlexion(myBeam As cls_Poutre, iCombi As Integer, iStep As Integer,
                                            MEd(,) As Decimal, MRd(,) As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   13/04/26 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance en flexion des sections sous températures
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   iStep       [E] :   Indice du pas de temps de calcul
        '   MEd         [E] :   Moments aux noeuds
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        Dim iDebK, iFinK As Integer
        Dim iTravee, iNode As Integer

        '--( Initialisation

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        '--> Traitement

        For iTravee = iDebT To iFinT

            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                If (iNode = iDebN) Then iDebK = 1 Else iDebK = 0
                If (iNode = iFinN) Then iFinK = 0 Else iFinK = 1

                For k = iDebK To iFinK

                    Me.CritereM(iStep).EnregistreCritere(iNode, iCombi, iTravee, MEd(iNode, k), MRd(iStep, iNode))

                Next
            Next
        Next

    End Sub

    Private Sub RunCritereResistancePlastiquePlatY_N(myBeam As cls_Poutre, iCombi As Integer, qsupEd() As Decimal,
                                                     iStep As Integer, TempPl As Decimal, TempFi As Decimal)
        '----------------------------------------------------------------------------------------------------------
        '   13/04/26 :  Création - POM
        '----------------------------------------------------------------------------------------------------------
        '   Vérification aux ELU de la résistance élasto-plastique des plats supports
        '----------------------------------------------------------------------------------------------------------
        '   myBeam      [E] :   Poutre traitée
        '   iCombi      [E] :   Indice de la combinaison
        '   qsupEd      [E] :   Charge répartie linéique agissant DES 2 COTES
        '   iStep       [E] :   Indice du pas de temps de calcul
        '   TempPl      [E] :   Température maxi dans le plat
        '   TempFi      [E] :   Température maxi dans la semelle inférieure
        '----------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim iTravee, iNode As Integer
        Dim iDebT, iFinT As Integer
        Dim iDebN, iFinN As Integer
        'Dim DeltaX As Decimal
        Dim qLin As Decimal
        Dim myFiEd, myFiRd As Decimal
        Dim myPlEd, myPlRd As Decimal
        Dim GammaM0 As Decimal
        Dim FyPlat, FyInf As Decimal
        Dim dApp, dbtFi, dbtPlat As Decimal
        Const kPlast As Decimal = 1.2
        Dim tPl, tFi As Decimal
        '  Dim dGauche, dDroite As Decimal
        Dim kCote As Decimal

        Dim ENFeu As New cls_EurocodesFeu
        Dim kReducFi As Decimal
        Dim kReducPl As Decimal

        '--( Initialisation

        GammaM0 = myBeam.Param.Gamma.GammaM0

        dApp = myBeam.SlimLargeurAppui
        myBeam.Section.SlimBrasLevier(dApp, dbtFi, dbtPlat)

        FyPlat = myBeam.Section.FySpd
        FyInf = myBeam.Section.FyInf

        tFi = myBeam.Section.ProfilA.Tfi
        tPl = myBeam.Section.ProfilA.Plat_t

        iDebT = myBeam.IndicePremiereTravee
        iFinT = myBeam.IndiceDerniereTravee

        kCote = CoefficientCharge(myBeam)

        '--> Traitement

        For iTravee = iDebT To iFinT
            iDebN = myBeam.Nodes.iNodeExtTrav(iTravee, 0)
            iFinN = myBeam.Nodes.iNodeExtTrav(iTravee, 1)

            For iNode = iDebN To iFinN

                With myBeam.Section.ProfilA

                    qLin = qsupEd(iNode) * kCote

                    Select Case .typeProfileAcier
                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSFB

                            kReducFi = ENFeu.ReducFyAcier(TempFi)
                            myFiEd = qLin * dbtFi
                            myFiRd = kReducFi * kPlast * tFi ^ 2 * FyInf * kConvMPaPa / (6 * GammaM0)

                            Me.CritereMY(iStep).EnregistreCritere(iNode, iCombi, iTravee, myFiEd, myFiRd)

                            kReducPl = ENFeu.ReducFyAcier(TempPl)

                            myPlEd = qLin * dbtPlat
                            myPlRd = kReducPl * kPlast * tPl ^ 2 * FyPlat * kConvMPaPa / (6 * GammaM0)

                            Me.CritereMY(iStep).EnregistreCritere(iNode, iCombi, iTravee, myPlEd, myPlRd)

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBA

                            kReducPl = ENFeu.ReducFyAcier(TempPl)

                            myPlEd = qLin * dbtPlat
                            myPlRd = kReducPl * kPlast * tPl ^ 2 * FyPlat * kConvMPaPa / (6 * GammaM0)

                            Me.CritereMY(iStep).EnregistreCritere(iNode, iCombi, iTravee, myPlEd, myPlRd)

                        Case cls_ProfilA.Enum_TypeSectionAcier.LamineSlimIFBB, cls_ProfilA.Enum_TypeSectionAcier.LamineSlimSAB

                            kReducFi = ENFeu.ReducFyAcier(TempFi)

                            myFiEd = qLin * dbtFi
                            myFiRd = kReducFi * kPlast * tFi ^ 2 * FyInf * kConvMPaPa / (6 * GammaM0)

                            Me.CritereMY(iStep).EnregistreCritere(iNode, iCombi, iTravee, myFiEd, myFiRd)

                    End Select

                End With

            Next
        Next

    End Sub

#End Region

End Class
