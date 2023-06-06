Imports System.IO
Imports PMXMoteur2
Module Mod_Database_Section

#Region "   Constantes "

    Private Const NFACCES As Integer = 10 'Ajout BD -> suppresssion MyConst.NFACCES
    Private Const KUNITBD As Decimal = 0.001
    Private Const TOS As Integer = 4
    Private Const TOL As Integer = 4
    Private Const TOI As Integer = 2
    Private Const SIZEVERSION As Integer = 7

#End Region

#Region "   MAIN "

    Public Sub InitDatabase_Section()

        '--> Options de la base de données
        OptionsDatabase_Section.lNewBase = True
        OptionsDatabase_Section.lSoftLimited = True
        OptionsDatabase_Section.FiltreSoft = "ACB+"
        OptionsDatabase_Section.lShowSteelAvailOnly = True
        OptionsDatabase_Section.ChoiceSteel = EnuChoiceAcier.BaseIfNoStandardSteel
        OptionsDatabase_Section.lNoSteelLowThick = True
        OptionsDatabase_Section.lSaveConfig = False
        OptionsDatabase_Section.lShowEC3 = True

        '--> Fichier
        If Not File.Exists(LogicielFichiers.Database_Section) Then
            'File.Copy(InfoLogiciel.RepertoireInstall & "\Database\AM_HRProfiles.dtb", FichierLogiciel.Database_Section)
            File.Copy(LogicielRep.RepertoireInstall & "\" & RepBase & "\" & RacProfile & ExtensionBase, LogicielFichiers.Database_Section)
        End If

        '--> Récupération des données de la database dans le catalogue
        InitialiseCatalogue(LogicielFichiers.Database_Section, MyCatalogue)

    End Sub

    Public Sub InitDatabase_Aciers()
        '--> Fichier
        If Not File.Exists(LogicielFichiers.Database_Aciers) Then
            File.Copy(LogicielRep.RepertoireInstall & "\" & RepBase & "\" & RacAcier & ExtensionBase, LogicielFichiers.Database_Aciers)
        End If

        '--> Récupération de la base

        InitialiseBaseAciers(LogicielFichiers.Database_Aciers, NFACCES, SteelBase)

    End Sub

    Public Sub RecupereAcierDefaut(ByRef Nuance As String, ByRef Qualite As String, ByRef Reduction As String, Plages As List(Of Cls_Acier.strucPlage))
        '-----------------------------------------------------------------------------------------------------------------
        '   29/04/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Récupère dans la base des aciers un acier pour l'initialisation d'un profilé
        '   Quand on appelle la routine, on n'est pas sur de se qui se trouve dans la base
        '   Si la base contient ce qui est demandé, on le récupère
        '   Sinon, on récupère l'acier avec la plus basse ou la plus haute fy
        '-----------------------------------------------------------------------------------------------------------------
        '   Nuance      [E] :   Nuance par défaut recherchée
        '   Qualité     [E] :   Qualité recherchée
        '   Reduction   [E] :   Réduction recherchée
        '   Plages      [S] :   Plages de valeurs fy, fu, ep
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lTrouveN As Boolean = False


        '--> Recherche de l'acier par défaut

        For Each kvpGrade As KeyValuePair(Of String, strucGrade) In SteelBase.Grades

            If kvpGrade.Key = Nuance Then

                lTrouveN = True

            End If

        Next

        If Not lTrouveN Then

            Nuance = SteelBase.Grades.Keys.Last
            'Ecrire une routine pour récupérer le fy le plus petit
        End If

        RecupereAcierDefautNuance(Nuance, Qualite, Reduction, Plages)

    End Sub

    Private Sub RecupereAcierDefautNuance(Nuance As String, ByRef Qualite As String, ByRef Reduction As String, Plages As List(Of Cls_Acier.strucPlage))
        '-----------------------------------------------------------------------------------------------------------------
        '   29/04/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Récupère dans la base des aciers un acier pour l'initialisation d'un profilé
        '   Quand on appelle la routine, on n'est pas sur de se qui se trouve dans la base, mais on sait que la nuance demandée s'y trouve
        '   Si la base contient ce qui est demandé, on le récupère
        '   Sinon, on récupère l'acier de nuance avec la première qualité dans la liste
        '-----------------------------------------------------------------------------------------------------------------
        '   Nuance      [E] :   Nuance par défaut recherchée
        '   Qualité     [E] :   Qualité recherchée
        '   Reduction   [E] :   Réduction recherchée
        '   Plages      [S] :   Plages de valeurs fy, fu, ep
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lTrouveQ As Boolean = False

        '--> Recherche dans la liste des qualités

        For Each kvpQualite As KeyValuePair(Of String, strucQualite) In SteelBase.Grades(Nuance).Qualites

            If kvpQualite.Key = Qualite Then

                lTrouveQ = True

            End If

        Next

        If Not lTrouveQ Then
            Qualite = SteelBase.Grades(Nuance).Qualites.Keys.First
        End If

        RecupereAcierDefautNuanceQualite(Nuance, Qualite, Reduction, Plages)

    End Sub

    Private Sub RecupereAcierDefautNuanceQualite(Nuance As String, Qualite As String, ByRef Reduction As String, Plages As List(Of Cls_Acier.strucPlage))
        '-----------------------------------------------------------------------------------------------------------------
        '   29/04/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Récupère dans la base des aciers un acier pour l'initialisation d'un profilé
        '   Quand on appelle la routine, on n'est pas sur de se qui se trouve dans la base, mais on sait que la nuance demandée s'y trouve
        '   Si la base contient ce qui est demandé, on le récupère
        '   Sinon, on récupère l'acier de nuance avec la première qualité dans la liste
        '-----------------------------------------------------------------------------------------------------------------
        '   Nuance      [E] :   Nuance par défaut recherchée
        '   Qualité     [E] :   Qualité recherchée
        '   Reduction   [E] :   Réduction recherchée
        '   Plages      [S] :   Plages de valeurs fy, fu, ep
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lTrouveR As Boolean = False

        '--> Recherche dans la liste des courbes de réduction

        For Each kvpSteel As KeyValuePair(Of String, strucReduction) In SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv

            If kvpSteel.Key = Reduction Then
                lTrouveR = True

            End If

        Next

        If Not lTrouveR Then
            Reduction = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv.Keys.Last
        End If

        RecupereAcierDefautPlages(Nuance, Qualite, Reduction, Plages)

    End Sub

    Private Sub RecupereAcierDefautPlages(Nuance As String, Qualite As String, Reduction As String, Plages As List(Of Cls_Acier.strucPlage))
        '-----------------------------------------------------------------------------------------------------------------
        '   29/04/23 :  Création - POM
        '-----------------------------------------------------------------------------------------------------------------
        '   Récupère dans la base des aciers un acier pour l'initialisation d'un profilé
        '   On sait quand on appelle cette routine que l'acier demande se trouve dans la base
        '-----------------------------------------------------------------------------------------------------------------
        '   Nuance      [E] :   Nuance par défaut recherchée
        '   Qualité     [E] :   Qualité recherchée
        '   Reduction   [E] :   Réduction recherchée
        '   Plages      [S] :   Plages de valeurs fy, fu, ep
        '-----------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyPlage As Cls_Acier.strucPlage

        '--> Récupération des plages

        Plages.Clear()

        For i As Integer = 0 To SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages.Count - 1
            MyPlage.Fy = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Fy
            MyPlage.Fu = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Fu
            MyPlage.Ep = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Reduction).Plages(i).Ep

            Plages.Add(MyPlage)

        Next

    End Sub

#End Region

#Region "   Options Database "

    Public OptionsDatabase_Section As StrucOptionsDataBase

    Structure StrucOptionsDataBase
        Dim lNewBase As Boolean
        Dim FiltreSoft As String
        Dim lSoftLimited As Boolean
        Dim lShowSteelAvailOnly As Boolean
        Dim ChoiceSteel As EnuChoiceAcier
        Dim lNoSteelLowThick As Boolean
        Dim lSaveConfig As Boolean
        Dim VersionBaseProfiles As StrucVersionDtB
        Dim lShowEC3 As Boolean
    End Structure

    Structure StrucVersionDtB
        Dim Year As Integer
        Dim Indice As Integer
        Dim Format As Short
    End Structure

    Public Enum EnuChoiceAcier
        BaseSteelOnly
        StandardSteelOnly
        BaseAndStandardSteels
        BaseIfNoStandardSteel
        AllSteel
    End Enum

#End Region

#Region "   Déclaration Base des aciers "

    Public SteelBase As strucBaseAciers

    Public Structure strucPlage
        Dim Ep As Double
        Dim Fy As Double
        Dim Fu As Double
    End Structure

    Public Structure strucReduction
        Dim Plages As List(Of strucPlage)
        Dim EpMax As Double
        Dim StIndex As Short
        Dim iBase As Short
    End Structure

    Public Structure strucQualite
        Dim ReductionCurv As Dictionary(Of String, strucReduction)
    End Structure

    Public Structure strucGrade
        Dim Qualites As Dictionary(Of String, strucQualite)
    End Structure

    Public Structure strucBaseAciers
        Dim Grades As Dictionary(Of String, strucGrade)
        Dim nbAciersBase, nbSteels As Integer
        Dim nbAciersStandard As Dictionary(Of Short, Integer)
        Dim nbStds As Integer
        Dim IndexStd As List(Of Integer)
        Dim NormeStd As List(Of String)
    End Structure

#End Region

#Region "   Gestion nouvelle base acier "

    Public Sub InitialiseBaseAciers(ByVal NomFichier As String, ByVal nUnit As Integer, ByRef SteelBase As strucBaseAciers)
        '-----------------------------------------------------------------------------------------
        '
        '   02/07/12 :  Création - Version 3.00 - POM
        '   09/03/20 :  Modification - Version 4.00 - Prise en compte du format 2 des bases (gestion des courbes EN1993)
        '
        '-----------------------------------------------------------------------------------------
        '
        '   NomFichier      [E] :   Nom du fichier de la base de données binaires des aciers
        '   SteelBase       [S] :   Base des aciers
        '
        '-----------------------------------------------------------------------------------------

        '--> Déclarations
        '==== Attention en VBNET la taille des Integer/Long n'est pas la même qu'en VB6/VBA (x2)
        '    VBNET     |    VB6
        '    Integer   =    Long
        '    Short     =    Integer
        '
        '------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim ASG, ASGi, TSTE, ASB As Integer
        Dim nCg, nCq, nCn As Short
        Dim nSteels As Short
        Dim Grade, Qualite, Norm As String
        Dim Plages As New List(Of strucPlage)
        Dim iStandart, iBase As Short
        Dim EpMax As Single
        Dim lOK As Boolean

        '--> Initialisation

        SteelBase.Grades = New Dictionary(Of String, strucGrade)
        SteelBase.Grades.Clear()
        SteelBase.nbAciersBase = 0
        SteelBase.nbSteels = 0
        SteelBase.nbAciersStandard = New Dictionary(Of Short, Integer)

        SteelBase.nbStds = 0
        SteelBase.IndexStd = New List(Of Integer)
        SteelBase.NormeStd = New List(Of String)

        Try
            If File.Exists(NomFichier) Then
                FileOpen(nUnit, NomFichier, OpenMode.Binary)

                FileGet(nUnit, ASG, 10)
                FileGet(nUnit, ASB, 14)
                FileGet(nUnit, nCg, 30)
                FileGet(nUnit, nCq, 32)
                FileGet(nUnit, nCn, 34)

                FileGet(nUnit, nSteels, ASG)
                FileGet(nUnit, TSTE, ASG + 2)

                ExtractStd(nUnit, ASB, SteelBase)

                For iSteel As Integer = 1 To nSteels

                    ASGi = ASG + 6 + (iSteel - 1) * TSTE
                    GetSteelInBase(nUnit, ASGi, nCg, nCq, nCn, Grade, Qualite, Norm, iBase, iStandart, EpMax, Plages)

                    '-- V4.00: les aciers HLE(nuance>460) et les reductions EC3 uniquement en mode expert   
                    AnalyseGrade(Grade, Qualite, Norm, lOK)

                    If lOK Then

                        AddSteelInBaseSteel(Grade, Qualite, Norm, iBase, iStandart, EpMax, Plages, SteelBase)
                        If iBase = 1 Then SteelBase.nbAciersBase += 1
                        SteelBase.nbSteels += 1
                        If SteelBase.nbAciersStandard.ContainsKey(iStandart) Then
                            SteelBase.nbAciersStandard(iStandart) += 1
                        Else
                            SteelBase.nbAciersStandard.Add(iStandart, 1)
                        End If
                    End If

                Next

            End If

        Catch ex As Exception
            MessageBox.Show("Error reading file " & NomFichier & " " & ex.Message)
        Finally
            FileClose(nUnit)
        End Try

    End Sub

    Private Sub AnalyseGrade(ByVal Grade As String, ByVal Qualite As String, ByVal ReducNorm As String, ByRef lAutorise As Boolean)
        '-----------------------------------------------------------------------------------------
        '
        '   15/01/20 :  Création - Version 4.00 - POM
        '
        '-----------------------------------------------------------------------------------------
        '
        '   Recherche si la nuance est HLE et si elle est autorisée pour le logiciel
        '
        '-----------------------------------------------------------------------------------------
        '
        '   Grade       [E] :   Nuance d'acier
        '   Qualite     [E] :   Qualité
        '   ReducNorm   [E] :   Norme pour la courbe de réduction fct de l'épaisseur
        '   lAutorise   [S] :   Indique si la nuance est autorisée dans le logiciel
        '
        '-----------------------------------------------------------------------------------------

        '--> Initialisation

        lAutorise = True

        '--> Traitement

        If IsAcierHLE(Grade) Then
            If Not LogicielOptions.lExpert Then lAutorise = False
        ElseIf IsReductionCurveEC3(Qualite, ReducNorm) Then
            ''On autorise pour le moment toutes les versions EC3
            '''If Not (OptionsDatabase_Section.lShowEC3 And OptionsLogiciel.lExpertMode) Then lAutorise = False

        End If

    End Sub

    Public Function IsReductionCurveEC3(ByVal Qualite As String, ByVal ReducNorm As String) As Boolean
        '-----------------------------------------------------------------------------------------
        '
        '   15/01/20 :  Création - Version 4.00 - POM
        '
        '-----------------------------------------------------------------------------------------
        '
        '   Recherche si la courbe de réduction est EC3 et si elle est autorisée pour le logiciel
        '
        '-----------------------------------------------------------------------------------------
        '
        '   Qualite     [E] :   Qualité de l'acier
        '   ReducNorm   [E] :   Norme pour la courbe de réduction fct de l'épaisseur
        '
        '-----------------------------------------------------------------------------------------

        '--> Déclarations

        Const EC3CURVE As String = "EC3"
        Dim lRep As Boolean = False

        '--> Traitement

        If ReducNorm.ToUpper.Trim.Contains(EC3CURVE) Then lRep = True
        If Qualite.ToUpper.Trim.Contains(EC3CURVE) Then lRep = True

        '--> Fin

        Return lRep

    End Function

    Public Function IsAcierHLE(ByVal Grade As String) As Boolean
        '-----------------------------------------------------------------------------------------
        '
        '   15/01/20 :  Création - Version 4.00 - POM
        '
        '-----------------------------------------------------------------------------------------
        '
        '   Indique si la nuance est HLE et si elle est autorisée pour le logiciel
        '
        '-----------------------------------------------------------------------------------------
        '
        '   Grade       [E] :   Nuance d'acier
        '
        '-----------------------------------------------------------------------------------------

        '--> Déclarations

        Dim IndexGrade As Integer
        Dim iPosNum As Integer
        Const nbCARNUM As Integer = 3
        Const LIMITGRADE As Integer = 460
        Dim lRep As Boolean = False

        '--> Recherche de la position de 3 caractères de suite donnant une valeur numérique dans la chaine de caractères

        iPosNum = PositionValeurDansChaine(Grade, nbCARNUM)

        '--> Traitement

        If iPosNum >= 0 Then
            IndexGrade = CInt(Val(Grade.Substring(iPosNum, nbCARNUM)))

            lRep = (IndexGrade > LIMITGRADE)
        End If

        '--> Fin

        Return lRep

    End Function

    Private Function IsInTable(ByVal MyTable() As String, ByVal MyChaine As String) As Boolean
        '-----------------------------------------------------------------------------------------
        '
        '   15/01/20 :  Création - Version 4.00 - POM
        '
        '-----------------------------------------------------------------------------------------
        '
        '   Teste si une chaine est dans un tableau
        '
        '-----------------------------------------------------------------------------------------
        '
        '   MyTable     [E] :   Tableau de chaines à sonder
        '   MyChaine    [E] :   Chaine recherchée dans le tableau
        '
        '-----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lRep As Boolean = False
        Dim iPos As Integer = -1
        Dim nbVal As Integer

        '--> Initialisation

        nbVal = MyTable.GetUpperBound(0)

        '--> Traitement

        If nbVal >= 0 Then
            Do While (Not lRep) And iPos < nbVal
                iPos += 1
                lRep = (MyTable(iPos) = MyChaine)
            Loop

        End If

        '--> Fin

        Return lRep

    End Function

    Private Function PositionValeurDansChaine(ByVal Chaine As String, ByVal nbCarNum As Integer) As Integer
        '-----------------------------------------------------------------------------------------
        '
        '   15/01/20 :  Création - Version 4.00 - POM
        '
        '-----------------------------------------------------------------------------------------
        '
        '   Indique si la nuance est HLE et si elle est autorisée pour le logiciel
        '
        '-----------------------------------------------------------------------------------------
        '
        '   Chaine      [E] :   Chaine de caractères à analyser
        '   nbCarNum    [E] :   Longueur mini de la sous chaine formant la valeur numérique
        '
        '-----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Index As Integer = -1
        Dim nbCar As Integer = Chaine.Length
        Dim lCont As Boolean
        Dim iPos As Integer
        Dim CarNum() As Boolean
        Dim MyCar As String
        Dim CarNo() As String = {" ", ".", ","}
        Dim lNum3 As Boolean

        '--> Traitement

        If nbCar >= nbCarNum Then

            ReDim CarNum(nbCar - 1)

            For i As Integer = 0 To nbCar - 1
                MyCar = CChar(Chaine.Substring(i, 1))
                If IsNumeric(MyCar) Then
                    CarNum(i) = (Not IsInTable(CarNo, MyCar))
                Else
                    CarNum(i) = False
                End If
            Next

            iPos = -1
            lCont = True
            Do While lCont
                iPos += 1
                lNum3 = CarNum(iPos)
                For i As Integer = 1 To nbCarNum - 1
                    lNum3 = lNum3 And CarNum(i + iPos)
                Next
                If lNum3 Then
                    lCont = False
                    Index = iPos
                ElseIf iPos + nbCarNum >= nbCar Then
                    lCont = False
                End If

            Loop

        End If

        '--> Fin 

        Return Index

    End Function

    Private Sub ExtractStd(ByVal nUnit As Integer, ByVal ASB As Integer, ByRef SteelBase As strucBaseAciers)
        '-----------------------------------------------------------------------------------------
        '
        '   05/07/12 :  Création - Version 3.00 - POM
        '
        '-----------------------------------------------------------------------------------------
        '
        '   Extraction des infos standard dans la base aciers
        '
        '-----------------------------------------------------------------------------------------
        '
        '   nUnit       [E] :   Unité de lecture
        '   ASB         [E] :   Adresse binaire du bloc standard
        '
        '-----------------------------------------------------------------------------------------

        Dim nbStd, iDx, iLng As Short
        Dim RecPos As Integer
        Dim NormStd As String

        FileGet(nUnit, nbStd, ASB)
        SteelBase.nbStds = nbStd

        If nbStd > 0 Then

            For i As Integer = 1 To nbStd
                FileGet(nUnit, iDx, ASB + i * TOI)
                SteelBase.IndexStd.Add(CInt(iDx))
            Next
            RecPos = ASB + (nbStd + 1) * TOI
            For i As Integer = 1 To nbStd
                FileGet(nUnit, iLng, RecPos)
                NormStd = FileGetString(nUnit, RecPos + TOI, iLng).Trim
                SteelBase.NormeStd.Add(NormStd)
                RecPos += TOI + iLng
            Next

        End If

    End Sub

    Public Sub GetSteelInBase(ByVal nUnit As Integer, ByVal ASGi As Integer,
                              ByVal nCg As Short, ByVal nCq As Short, ByVal nCn As Short,
                              ByRef Grade As String, ByRef Qualite As String, ByRef Norm As String,
                              ByRef iBase As Short, ByRef iStandart As Short, ByRef EpMax As Single, ByRef Plages As List(Of strucPlage))
        '----------------------------------------------------------------------------------
        '
        '   02/07/12 :  Création - POM - v3.00
        '
        '----------------------------------------------------------------------------------
        '
        '   Recupération d'un acier dans la base des données
        '
        '----------------------------------------------------------------------------------
        '
        '   nUnit       [E] :   Unité de lecture du fichier binaire
        '   nCg         [E] :   Nombre de caractères pour le Grade
        '   nCq         [E] :   Nombre de caractères pour la Qualité
        '   nCn         [E] :   Nombre de caractères pour la Reduction Curve
        '
        '   Grade       [S] :   Nom de la nuance d'acier
        '   Qualite     [S] :   Nom de la qualité
        '   Norm        [S] :   Nom de la reduction curve
        '   iBase       [S] :   Indique si acier de base
        '   iStandart   [S] :   Indice de standard
        '   EpMax       [S] :   Epaisseur maximale dans la courbe de réduction
        '   Plages      [S] :   Plages définissant la courbe de réduction
        '
        '----------------------------------------------------------------------------------

        Dim RecPos As Integer = ASGi + nCg + nCq + nCn

        Grade = FileGetString(nUnit, ASGi, nCg).Trim
        Qualite = FileGetString(nUnit, ASGi + nCg, nCq).Trim
        Norm = FileGetString(nUnit, ASGi + nCg + nCq, nCn).Trim

        GetSteelInBase(nUnit, RecPos, iBase, iStandart, EpMax, Plages)

    End Sub

    Public Sub GetSteelInBase(ByVal nUnit As Integer, ByVal RecPos As Integer,
                              ByRef iBase As Short, ByRef iStandart As Short, ByRef EpMax As Single, ByRef Plages As List(Of strucPlage))
        '----------------------------------------------------------------------------------
        '
        '   02/07/12 :  Création - POM - v3.00
        '
        '----------------------------------------------------------------------------------
        '
        '   Recupération d'un acier dans la base des données
        '
        '----------------------------------------------------------------------------------
        '
        '   nUnit       [E] :   Unité de lecture du fichier binaire
        '   nCg         [E] :   Nombre de caractères pour le Grade
        '   nCq         [E] :   Nombre de caractères pour la Qualité
        '   nCn         [E] :   Nombre de caractères pour la Reduction Curve
        '
        '   Grade       [S] :   Nom de la nuance d'acier
        '   Qualite     [S] :   Nom de la qualité
        '   Norm        [S] :   Nom de la reduction curve
        '   iBase       [S] :   Indique si acier de base
        '   iStandart   [S] :   Indice de standard
        '   EpMax       [S] :   Epaisseur maximale dans la courbe de réduction
        '   Plages      [S] :   Plages définissant la courbe de réduction
        '
        '----------------------------------------------------------------------------------

        Const TOI As Integer = 2
        Const TOS As Integer = 4
        Dim iExpert As Short
        Dim nbPlages As Short
        Dim MyRange As strucPlage
        Dim ADPP As Integer
        Dim Ep, Fy, Fu As Single
        Const kUNIT As Single = 0.001

        FileGet(nUnit, iExpert, RecPos)
        FileGet(nUnit, iBase, RecPos + TOI)
        FileGet(nUnit, iStandart, RecPos + 2 * TOI)
        FileGet(nUnit, EpMax, RecPos + 3 * TOI)
        FileGet(nUnit, nbPlages, RecPos + 3 * TOI + TOS)

        EpMax = EpMax * kUNIT
        Plages.Clear()
        For i As Integer = 1 To nbPlages
            ADPP = RecPos + 4 * TOI + (3 * (i - 1) + 1) * TOS
            FileGet(nUnit, Ep, ADPP)
            FileGet(nUnit, Fy, ADPP + TOS)
            FileGet(nUnit, Fu, ADPP + 2 * TOS)
            MyRange.Ep = CDbl(Ep * kUNIT)
            MyRange.Fy = CDbl(Fy)
            MyRange.Fu = CDbl(Fu)
            Plages.Add(MyRange)
        Next

    End Sub

    Private Sub AddSteelInBaseSteel(ByVal Grade As String, ByVal Qualite As String, ByVal Norm As String,
                                    ByVal iBase As Short, ByVal iStandart As Short, ByVal EpMax As Single, ByVal Plages As List(Of strucPlage),
                                    ByRef SteelBase As strucBaseAciers)
        '-------------------------------------------------------------------------------------------------------
        '
        '   02/07/12 :  Création - V3.00 - POM
        '
        '-------------------------------------------------------------------------------------------------------
        '
        '   Ajout d'un acier dans le catalogue des aciers
        '
        '-------------------------------------------------------------------------------------------------------
        '
        '   Grade       [E] :   Nuance d'acier
        '   Qualite     [E] :   Qualite d'acier
        '   Norm        [E] :   Label de la courbe de réduction des propriétés
        '   iBase       [E] :   Indique si acier de base    
        '   iStandart   [E] :   Indice de standart (dans la base profilés)
        '   EpMax       [E] :   Epaisseur maxi dans la courbe de réduction'
        '   Plages      [E] :   Liste des plages d'épaisseur dans la courbe de réduction
        '
        '   SteelBase   [S] :   Catalogue des aciers à renseigner
        '
        '-------------------------------------------------------------------------------------------------------

        If SteelBase.Grades.ContainsKey(Grade) Then
            AddQualiteInGrade(Qualite, Norm, iBase, iStandart, EpMax, Plages, SteelBase.Grades(Grade))
        Else
            Dim MyGrade As strucGrade
            MyGrade.Qualites = New Dictionary(Of String, strucQualite)
            SteelBase.Grades.Add(Grade, MyGrade)
            AddQualiteInGrade(Qualite, Norm, iBase, iStandart, EpMax, Plages, SteelBase.Grades(Grade))
        End If

    End Sub

    Private Sub AddQualiteInGrade(ByVal Qualite As String, ByVal Norm As String,
                                  ByVal iBase As Short, ByVal iStandart As Short, ByVal EpMax As Single, ByVal Plages As List(Of strucPlage),
                                  ByRef MyGrade As strucGrade)
        '-------------------------------------------------------------------------------------------------------
        '
        '   02/07/12 :  Création - V3.00 - POM
        '
        '-------------------------------------------------------------------------------------------------------
        '
        '   Ajout d'une qualité dans une nuance du catalogue des aciers
        '
        '-------------------------------------------------------------------------------------------------------
        '
        '   Qualite     [E] :   Qualite d'acier
        '   Norm        [E] :   Label de la courbe de réduction des propriétés
        '   iBase       [E] :   Indique si acier de base    
        '   iStandart   [E] :   Indice de standart (dans la base profilés)
        '   EpMax       [E] :   Epaisseur maxi dans la courbe de réduction'
        '   Plages      [E] :   Liste des plages d'épaisseur dans la courbe de réduction
        '
        '   MyGrade     [S] :   Nuance d'acier à compléter
        '
        '-------------------------------------------------------------------------------------------------------

        If MyGrade.Qualites.ContainsKey(Qualite) Then
            AddNormInQualite(Norm, iBase, iStandart, EpMax, Plages, MyGrade.Qualites(Qualite))
        Else

            Dim MyQualite As strucQualite
            MyQualite.ReductionCurv = New Dictionary(Of String, strucReduction)
            MyGrade.Qualites.Add(Qualite, MyQualite)

            AddNormInQualite(Norm, iBase, iStandart, EpMax, Plages, MyGrade.Qualites(Qualite))

        End If

    End Sub

    Private Sub AddNormInQualite(ByVal Norm As String,
                                 ByVal iBase As Short, ByVal iStandart As Short, ByVal EpMax As Single, ByVal Plages As List(Of strucPlage),
                                 ByRef MyQualite As strucQualite)
        '-------------------------------------------------------------------------------------------------------
        '
        '   02/07/12 :  Création - V3.00 - POM
        '
        '-------------------------------------------------------------------------------------------------------
        '
        '   Ajout d'une courbe de réduction dans une qualité du catalogue des aciers
        '
        '-------------------------------------------------------------------------------------------------------
        '
        '   Norm        [E] :   Label de la courbe de réduction des propriétés
        '   iBase       [E] :   Indique si acier de base    
        '   iStandart   [E] :   Indice de standart (dans la base profilés)
        '   EpMax       [E] :   Epaisseur maxi dans la courbe de réduction'
        '   Plages      [E] :   Liste des plages d'épaisseur dans la courbe de réduction
        '
        '   MyQualite   [S] :   Qualité d'acier à compléter
        '
        '-------------------------------------------------------------------------------------------------------

        If MyQualite.ReductionCurv.ContainsKey(Norm) Then
            '==== BIG PROBLEM
            'GestionErreursACB("Mod_GestionBaseBinaires", "AddNormInQualite", "The reduction curve " & Norm & " already in the base")
        Else
            Dim MyReduc As strucReduction

            MyReduc.EpMax = EpMax
            MyReduc.Plages = New List(Of strucPlage)
            For i As Integer = 0 To Plages.Count - 1
                MyReduc.Plages.Add(Plages(i))
            Next
            MyReduc.StIndex = iStandart
            MyReduc.iBase = iBase

            MyQualite.ReductionCurv.Add(Norm, MyReduc)
        End If

    End Sub

    Public Function SteelIsToCompatibleToProfile(ByVal EpMax As Single, ByVal IndStd() As Integer, ByVal SteelBase As strucBaseAciers, ByVal CorIndStd As Dictionary(Of Short, Short),
                                                 ByVal Nuance As String, ByVal Qualite As String, ByVal Norm As String, ByVal ChoiceAcier As EnuChoiceAcier,
                                                 ByRef lIsNuanceCompatibleProfile As Boolean) As Boolean
        '----------------------------------------------------------------------------------------------------------------------------------------
        '
        '   26/09/12 :  Création - POM - V3.00
        '
        '----------------------------------------------------------------------------------------------------------------------------------------
        '
        '   Indique si un acier est compatible avec un profilé sélectionné
        '
        '----------------------------------------------------------------------------------------------------------------------------------------
        '
        '   EpMax       [E] :   Epaisseur maximale des parois du profilé
        '   IndStd      [E] :   Table des indices Standard du profilé
        '   SteelBase   [E] :   Structure contenant les aciers de la base
        '   CorIndStd
        '   Nuance      [E] :   Nuance d'acier
        '   Qualite     [E] :   Qualité de l'acier
        '   Norm        [E] :   Courbe de réduction
        '   ChoiceAcier [E] :   Politique de choix des aciers
        '   lIsNuance.. [S] :   Indique si un acier considéré compatible en sortie de la fonction l'est à cause du parametre IndStd
        '
        '----------------------------------------------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim lDisplay As Boolean = True
        Dim IndStdSteel As Integer
        Dim lBase, lCompatible As Boolean

        '-->

        lBase = (SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Norm).iBase = 1)
        IndStdSteel = SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Norm).StIndex
        lIsNuanceCompatibleProfile = (IndStd(CorIndStd(CShort(IndStdSteel)) - 1) = 1)

        Select Case ChoiceAcier
            Case EnuChoiceAcier.AllSteel
                lDisplay = True
            Case EnuChoiceAcier.BaseAndStandardSteels
                lDisplay = lBase Or lIsNuanceCompatibleProfile
            Case EnuChoiceAcier.BaseIfNoStandardSteel
                lDisplay = (lBase And Not lIsNuanceCompatibleProfile) Or (lIsNuanceCompatibleProfile)
            Case EnuChoiceAcier.BaseSteelOnly
                lDisplay = lBase
            Case EnuChoiceAcier.StandardSteelOnly
                lDisplay = lIsNuanceCompatibleProfile
        End Select

        If OptionsDatabase_Section.lNoSteelLowThick Then
            lCompatible = EpMax <= SteelBase.Grades(Nuance).Qualites(Qualite).ReductionCurv(Norm).EpMax * (1.00001)
        Else
            lCompatible = True
        End If

        Return (lDisplay And lCompatible)

    End Function

#End Region

#Region "   Déclaration Catalogue de profilés "

    Public MyCatalogue As StrucCatalogue

    Public Structure StrucCatalogue
        Dim nbGammes As Integer
        Dim nbSoft As Integer
        Dim nbDelivery As Integer
        Dim nbStandard As Integer
        Dim IndexStand() As Short
        Dim NormStd() As String
        Dim SoftLab() As String
        Dim IndSoft As Integer
        Dim Series As Dictionary(Of String, StrucGamme)
        Dim CorIndStd As Dictionary(Of Short, Short)
        Dim Langues As List(Of String)
        Dim Delivery() As List(Of String)
    End Structure

    Public Structure StrucGamme
        Dim nbProfiles As Integer
        Dim Profiles As Dictionary(Of String, Cls_SectionNew)
    End Structure

#End Region

#Region "   Classe Section pour la database "

    Public Class Cls_SectionNew

        '== Cette classe est utilisée - ne pas supprimer (en particulier fenetre Frm_SectionNewBase)
        Public Gamme As String
        Public Etiquette As String
        Public Ht As Decimal
        Public Bf, Tf, Tw, Rc As Decimal
        Public IndDeliv() As Short
        Public IndStandart() As Short

        Public lSoft As Boolean     'Indique si le profilé est autorisé pour le logiciel

        Public Sub New(ByVal MyGamme As String, ByVal MyLabel As String)

            Gamme = MyGamme
            Etiquette = MyLabel
            lSoft = True

        End Sub

    End Class

#End Region

#Region "   Gestion des nouvelles bases de données binaires profiles "

    ''' <summary>
    ''' Initialisation du catalogue à partir de la base de données
    ''' 30/06/12 :  Création - Version 3.00 - POM
    ''' </summary>
    ''' <param name="NomFichier">Nom du fichier de la base de données binaires des sections</param>
    ''' <param name="Catalogue">Catalogue de profilés</param>
    Public Sub InitialiseCatalogue(ByVal NomFichier As String, ByRef Catalogue As StrucCatalogue)

        '--> Déclaration
        Dim AGB, ASF, ANB, ADB, RecPos As Integer
        Dim nbSeries As Short
        Dim nCs, nCp, nCSoft As Short
        Dim Label As String
        Dim MyGam As New StrucGamme '--> Dim MyGam As StrucGamme 
        Dim iFBase As Short
        Dim VerBase As String = ""
        '--> Initialisation

        Catalogue.Series = New Dictionary(Of String, StrucGamme)
        Catalogue.Series.Clear()

        Try
            If File.Exists(NomFichier) Then

                FileOpen(NFACCES, NomFichier, OpenMode.Binary)

                '--> Version de la base
                ExtraireFormatIndiceDtB(NFACCES, iFBase, VerBase)
                Dim txt() As String = VerBase.Split("_")
                OptionsDatabase_Section.VersionBaseProfiles.Year = txt(0)
                OptionsDatabase_Section.VersionBaseProfiles.Indice = txt(1)
                OptionsDatabase_Section.VersionBaseProfiles.Format = iFBase

                FileGet(NFACCES, AGB, 30)
                FileGet(NFACCES, ADB, 34)
                FileGet(NFACCES, ANB, 38)
                FileGet(NFACCES, ASF, 42)
                FileGet(NFACCES, nbSeries, AGB)

                FileGet(NFACCES, nCs, 60)
                FileGet(NFACCES, nCp, 62)
                FileGet(NFACCES, nCSoft, 64)

                Catalogue.nbGammes = nbSeries

                GetCatalogBlockStandard(NFACCES, ANB, Catalogue)
                GetCatalogBlockDelivery(NFACCES, ADB, Catalogue)
                GetCatalogBlockSoft(NFACCES, ASF, nCSoft, Catalogue)

                For iGam As Integer = 1 To nbSeries
                    Label = FileGetString(NFACCES, AGB + 2 + CInt((iGam - 1) * (nCs + 4)), nCs).Trim
                    Catalogue.Series.Add(Label, MyGam)
                    FileGet(NFACCES, RecPos, AGB + 2 + CInt((iGam - 1) * (nCs + 4) + nCs))
                    GetCatalogBlockGamme(NFACCES, RecPos, nCp, Catalogue.nbDelivery, Catalogue.nbStandard, Catalogue.nbSoft, Catalogue.Series(Label), Catalogue.IndSoft)
                Next

            End If

        Catch
            MessageBox.Show("Erreur lecture fichier : " & NomFichier & " | InitialiseCatalogue - Mod_Database")
        Finally
            FileClose(NFACCES)
        End Try

    End Sub

    ''' <summary>
    ''' Récupère dans la base de données profilés un block Gamme (y compris tous les profilés)
    ''' 30/06/12 :  Création - POM - v3.00
    ''' </summary>
    ''' <param name="nUnit">Index unité de lecture du fichier</param>
    ''' <param name="ASBi">Adresse du block Gamme dans la base</param>
    ''' <param name="nCp">Nombre de caractères pour le codage d'un lable de profilé</param>
    ''' <param name="nbDelivery">Nombre de conditions de livraison dans le catalogue</param>
    ''' <param name="nbStandard"></param>
    ''' <param name="nbSoft"></param>
    ''' <param name="Gamme">Gamme du catalogue à renseigner</param>
    ''' <param name="ThisSoftIndex"></param>
    Private Sub GetCatalogBlockGamme(ByVal nUnit As Integer, ByVal ASBi As Integer,
                                     ByVal nCp As Short, ByVal nbDelivery As Integer, ByVal nbStandard As Integer, ByVal nbSoft As Integer,
                                     ByRef Gamme As StrucGamme, ByVal ThisSoftIndex As Integer)

        '--> Déclaration 
        Dim nbProf As Short
        Dim ASBPj, TPRO As Integer
        Dim Label As String = ""
        Dim Ht, Tf, Bf, Tw, Rc As Decimal
        Dim G, A, d As Decimal
        Dim Iy, WEly, WPly, RGirY, AvZ As Decimal
        Dim Iz, WElz, WPlz, RGirZ As Decimal
        Dim It, Iw As Decimal
        Dim IndDeliv(), IndStandard(), IndSoft() As Short

        '--> Initialisation
        ReDim IndDeliv(nbDelivery - 1)
        ReDim IndStandard(nbStandard - 1)
        ReDim IndSoft(nbSoft - 1)

        '--> Lecture
        FileGet(nUnit, nbProf, ASBi)
        FileGet(nUnit, TPRO, ASBi + 2)

        Gamme.nbProfiles = CInt(nbProf)
        Gamme.Profiles = New Dictionary(Of String, Cls_SectionNew)
        Gamme.Profiles.Clear()

        For iPro As Integer = 0 To nbProf - 1
            ASBPj = ASBi + 2 + 4 + (iPro) * TPRO

            GetCatalogBlockProfile(nUnit, ASBPj, nCp, nbDelivery, nbStandard, nbSoft, Label, Ht, Bf, Tf, Tw, Rc, G, A, d,
                                   Iy, WEly, WPly, RGirY, AvZ, Iz, WElz, WPlz, RGirZ, It, Iw, IndDeliv, IndStandard, IndSoft)

            Gamme.Profiles.Add(Label, New Cls_SectionNew("", Label))
            Gamme.Profiles(Label).Ht = Ht * KUNITBD
            Gamme.Profiles(Label).Bf = Bf * KUNITBD
            Gamme.Profiles(Label).Tf = Tf * KUNITBD
            Gamme.Profiles(Label).Tw = Tw * KUNITBD
            Gamme.Profiles(Label).Rc = Rc * KUNITBD

            ReDim Gamme.Profiles(Label).IndDeliv(nbDelivery - 1)
            For i As Integer = 0 To nbDelivery - 1
                Gamme.Profiles(Label).IndDeliv(i) = IndDeliv(i)
            Next

            ReDim Gamme.Profiles(Label).IndStandart(nbStandard - 1)
            For i As Integer = 0 To nbStandard - 1
                Gamme.Profiles(Label).IndStandart(i) = IndStandard(i)
            Next

            Gamme.Profiles(Label).lSoft = (IndSoft(ThisSoftIndex) = 1)

        Next

    End Sub

    ''' <summary>
    ''' Récupère dans la base de données profilés le block d'un profilé
    ''' RETOUR DES DONNEES DANS LES UNITES DE LA BASE DE DONNEES
    ''' 30/06/12 :  Création - POM - v3.00
    ''' </summary>
    ''' <param name="nUnit"></param>
    ''' <param name="ASBPj"></param>
    ''' <param name="nCp"></param>
    ''' <param name="nbDelivery"></param>
    ''' <param name="nbStandard"></param>
    ''' <param name="nbSoft"></param>
    ''' <param name="Label"></param>
    ''' <param name="Ht"></param>
    ''' <param name="Bf"></param>
    ''' <param name="Tf"></param>
    ''' <param name="Tw"></param>
    ''' <param name="Rc"></param>
    ''' <param name="G"></param>
    ''' <param name="A"></param>
    ''' <param name="d"></param>
    ''' <param name="Iy"></param>
    ''' <param name="WEly"></param>
    ''' <param name="WPly"></param>
    ''' <param name="RGirY"></param>
    ''' <param name="AvZ"></param>
    ''' <param name="Iz"></param>
    ''' <param name="WElz"></param>
    ''' <param name="WPlz"></param>
    ''' <param name="RGirZ"></param>
    ''' <param name="It"></param>
    ''' <param name="Iw"></param>
    ''' <param name="IndDeliv"></param>
    ''' <param name="IndStandard"></param>
    ''' <param name="IndSoft"></param>
    Private Sub GetCatalogBlockProfile(ByVal nUnit As Integer, ByVal ASBPj As Integer, ByVal nCp As Short,
                                       ByVal nbDelivery As Integer, ByVal nbStandard As Integer, ByVal nbSoft As Integer,
                                       ByRef Label As String, ByRef Ht As Single, ByRef Bf As Single, ByRef Tf As Single,
                                       ByRef Tw As Single, ByRef Rc As Single, ByRef G As Single, ByRef A As Single,
                                       ByRef d As Single, ByRef Iy As Single, ByRef WEly As Single, ByRef WPly As Single,
                                       ByRef RGirY As Single, ByRef AvZ As Single,
                                       ByRef Iz As Single, ByRef WElz As Single, ByRef WPlz As Single,
                                       ByRef RGirZ As Single, ByRef It As Single, ByRef Iw As Single,
                                       ByRef IndDeliv() As Short, ByRef IndStandard() As Short, ByRef IndSoft() As Short)
        '----------------------------------------------------------------------------------------------
        '
        '   30/06/12 :  Création - POM - v3.00
        '
        '----------------------------------------------------------------------------------------------
        '
        '   Récupère dans la base de données profilés le block d'un profilé
        '   RETOUR DES DONNEES DANS LES UNITES DE LA BASE DE DONNEES
        '
        '----------------------------------------------------------------------------------------------
        '
        '   nUnit       [E] :   Index unité de lecture du fichier
        '   ASBPj       [E] :   Adresse du block profilé dans la base
        '   nCp         [E] :   Nombre de caractères pour le codage d'un lable de profilé
        '
        '   Label       [S] :   Nom du profilé
        '   Ht          [S] :   Hauteur du profilé
        '   Bf, Tf      [S] :   Dimensions de la semelle
        '   Tw, Rc      [S] :   Epaisseur ame et rayon du congé
        '
        '----------------------------------------------------------------------------------------------

        Dim Index As Short
        Dim RecPos As Integer = ASBPj + nCp + 19 * TOS

        Label = FileGetString(nUnit, ASBPj, nCp).Trim
        FileGet(nUnit, Ht, ASBPj + nCp)
        FileGet(nUnit, Bf, ASBPj + nCp + TOS)
        FileGet(nUnit, Tf, ASBPj + nCp + 2 * TOS)
        FileGet(nUnit, Tw, ASBPj + nCp + 3 * TOS)
        FileGet(nUnit, Rc, ASBPj + nCp + 4 * TOS)
        FileGet(nUnit, G, ASBPj + nCp + 5 * TOS)
        FileGet(nUnit, A, ASBPj + nCp + 6 * TOS)
        FileGet(nUnit, d, ASBPj + nCp + 7 * TOS)
        FileGet(nUnit, Iy, ASBPj + nCp + 8 * TOS)
        FileGet(nUnit, WEly, ASBPj + nCp + 9 * TOS)
        FileGet(nUnit, WPly, ASBPj + nCp + 10 * TOS)
        FileGet(nUnit, RGirY, ASBPj + nCp + 11 * TOS)
        FileGet(nUnit, AvZ, ASBPj + nCp + 12 * TOS)
        FileGet(nUnit, Iz, ASBPj + nCp + 13 * TOS)
        FileGet(nUnit, WElz, ASBPj + nCp + 14 * TOS)
        FileGet(nUnit, WPlz, ASBPj + nCp + 15 * TOS)
        FileGet(nUnit, RGirZ, ASBPj + nCp + 16 * TOS)
        FileGet(nUnit, It, ASBPj + nCp + 17 * TOS)
        FileGet(nUnit, Iw, ASBPj + nCp + 18 * TOS)

        For k As Integer = 0 To nbDelivery - 1
            FileGet(nUnit, IndDeliv(k), RecPos + k * TOI)
        Next

        RecPos += nbDelivery * TOI

        For k As Integer = 0 To nbStandard - 1
            FileGet(nUnit, IndStandard(k), RecPos + k * TOI)
        Next

        RecPos += nbStandard * TOI

        For k As Integer = 0 To nbSoft - 1
            FileGet(nUnit, Index, CInt(RecPos + k * TOI))
            IndSoft(k) = Index
        Next

    End Sub

    Public Sub GetTabCorrespondanceIndiceStandart(ByVal FileProfiles As String, ByRef CorIndStd As Dictionary(Of Short, Short))
        '----------------------------------------------------------------------------------------------
        '
        '   07/12/12 :  Création - POM - v3.00
        '
        '----------------------------------------------------------------------------------------------
        '
        '   Récupère dans la base de données profilés la table de correspondance
        '   des Indices de standarts
        '
        '----------------------------------------------------------------------------------------------
        '
        '   FileProfiles    [E] :   Nom du fichier base de données des profilés
        '   CorIndStd       [S] :   Table des correspondance des indices
        '
        '----------------------------------------------------------------------------------------------

        Dim ANB As Integer
        Dim nbStand As Short
        Const TOI As Integer = 2
        'Dim RecPos As Integer
        Dim IndexStd() As Short

        Try
            If File.Exists(FileProfiles) Then

                FileOpen(NFACCES, FileProfiles, OpenMode.Binary)

                FileGet(NFACCES, ANB, 38)
                FileGet(NFACCES, nbStand, ANB)

                If nbStand > 0 Then

                    ReDim IndexStd(nbStand - 1)

                    For i As Short = 1 To nbStand
                        FileGet(NFACCES, IndexStd(i - 1), ANB + i * TOI)
                    Next

                    CorIndStd = New Dictionary(Of Short, Short)

                    For i As Short = 1 To nbStand
                        CorIndStd.Add(IndexStd(i - 1), i)
                    Next


                End If
            End If
        Catch
            MessageBox.Show("Erreur lecture fichier : " & FileProfiles & " | GetTabCorrespondanceIndiceStandart - Mod_Database")
        Finally
            FileClose(NFACCES)
        End Try

    End Sub

    ''' <summary>
    ''' Récupère dans la base de données profilés le block standard
    ''' 30/06/12 :  Création - POM - v3.00
    ''' </summary>
    ''' <param name="nUnit">Index unité de lecture du fichier</param>
    ''' <param name="ANB">Adresse du block Standard dans la base</param>
    ''' <param name="Catalogue">Catalogue à renseigner</param>
    Private Sub GetCatalogBlockStandard(ByVal nUnit As Integer, ByVal ANB As Integer, ByRef Catalogue As StrucCatalogue)

        '--> Déclaration
        Dim nbStand As Short
        Const TOI As Integer = 2
        Dim RecPos As Integer

        FileGet(nUnit, nbStand, ANB)
        Catalogue.nbStandard = nbStand

        If nbStand > 0 Then

            ReDim Catalogue.IndexStand(nbStand - 1)
            ReDim Catalogue.NormStd(nbStand - 1)

            For i As Short = 1 To nbStand
                FileGet(nUnit, Catalogue.IndexStand(i - 1), ANB + i * TOI)
            Next

            RecPos = ANB + (nbStand + 1) * TOI
            Dim iLng As Short

            For i As Short = 1 To nbStand
                FileGet(nUnit, iLng, RecPos)
                Catalogue.NormStd(i - 1) = FileGetString(nUnit, RecPos + TOI, CInt(iLng)).Trim
                RecPos += TOI + CInt(iLng)
            Next

            Catalogue.CorIndStd = New Dictionary(Of Short, Short)

            For i As Short = 1 To nbStand
                Catalogue.CorIndStd.Add(Catalogue.IndexStand(i - 1), i)
            Next

        End If

    End Sub

    ''' <summary>
    ''' Récupère dans la base de données profilés le block delivery
    ''' 30/06/12 :  Création - POM - v3.00
    ''' </summary>
    ''' <param name="nUnit">Index unité de lecture du fichier</param>
    ''' <param name="ADB">Adresse du block delivery dans la base</param>
    ''' <param name="Catalogue">Catalogue à renseigner</param>
    Private Sub GetCatalogBlockDelivery(ByVal nUnit As Integer, ByVal ADB As Integer, ByRef Catalogue As StrucCatalogue)

        '--> Déclaration
        Dim nbDelivery, nbLang As Short
        Dim AbbrLng As String
        Dim RecPos, ADBj As Integer
        Dim nSize As Short
        Dim DelCond As String

        FileGet(nUnit, nbDelivery, ADB)
        FileGet(nUnit, nbLang, ADB + 2)

        Catalogue.Langues = New List(Of String)
        Catalogue.Langues.Clear()

        For i As Integer = 1 To nbLang
            AbbrLng = FileGetString(nUnit, ADB + 2 * TOI + 2 * (i - 1), 2).Trim
            Catalogue.Langues.Add(AbbrLng)
        Next

        Catalogue.nbDelivery = nbDelivery

        If nbDelivery > 0 Then
            ReDim Catalogue.Delivery(nbDelivery - 1)

            For j As Integer = 1 To nbDelivery
                Catalogue.Delivery(j - 1) = New List(Of String)
                Catalogue.Delivery(j - 1).Clear()

                RecPos = ADB + 24 + TOL * (j - 1)
                FileGet(nUnit, ADBj, RecPos)

                RecPos = ADBj

                For k As Integer = 1 To nbLang
                    FileGet(nUnit, nSize, RecPos)
                    DelCond = FileGetString(nUnit, RecPos + TOI, nSize).Trim
                    Catalogue.Delivery(j - 1).Add(DelCond)
                    RecPos += TOI + nSize
                Next

            Next

        End If

    End Sub

    ''' <summary>
    ''' Récupère dans la base de données profilés le block software
    ''' 30/06/12 :  Création - POM - v3.00
    ''' </summary>
    ''' <param name="nUnit">Index unité de lecture du fichier</param>
    ''' <param name="ASF">Adresse du block software dans la base</param>
    ''' <param name="nCSoft"> Nombre de caractères sur lequel le nom d'un software est enregistré</param>
    ''' <param name="Catalogue">Catalogue à renseigner</param>
    Private Sub GetCatalogBlockSoft(ByVal nUnit As Integer, ByVal ASF As Integer, ByVal nCSoft As Short, ByRef Catalogue As StrucCatalogue)

        '--> Déclaration
        Dim nbSoft As Short
        Dim RecPos As Integer
        Const TOI As Integer = 2
        Dim lTrouve As Boolean
        Dim jS As Integer

        '--> Lecture du bloc
        FileGet(nUnit, nbSoft, ASF)
        Catalogue.nbSoft = nbSoft
        If nbSoft > 0 Then
            ReDim Catalogue.SoftLab(nbSoft - 1)
            For i As Integer = 0 To nbSoft - 1
                RecPos = ASF + TOI + CInt((i) * nCSoft)
                Catalogue.SoftLab(i) = FileGetString(nUnit, RecPos, CInt(nCSoft)).Trim
            Next

            '--> Recherche de l'indice du logiciel appelant

            lTrouve = False
            jS = -1
            Do While jS < nbSoft - 1 And Not lTrouve
                jS += 1
                lTrouve = (OptionsDatabase_Section.FiltreSoft.Trim = Catalogue.SoftLab(jS).Trim)
            Loop
            If lTrouve Then
                Catalogue.IndSoft = jS
            Else
                Catalogue.IndSoft = -1
            End If

        End If

    End Sub

#End Region

#Region "   Version de la database "

    ''' <summary>
    ''' Lecture dans la new base des sections de l'indice et du format
    ''' 04/07/12 :  Création - v3.00 - POM
    ''' </summary>
    ''' <param name="nUNit">Unité de lecture</param>
    ''' <param name="iFormat">Indice de format</param>
    ''' <param name="strVersion">Version de la base (codage 7 caractères)</param>
    Private Sub ExtraireFormatIndiceDtB(ByVal nUNit As Integer, ByRef iFormat As Short, ByRef strVersion As String)

        strVersion = FileGetString(nUNit, 1, SIZEVERSION).Trim
        FileGet(nUNit, iFormat, SIZEVERSION + 1)

    End Sub

#End Region

#Region "   Lecture d'une chaine de caractères dans un fichier binaire "

    ''' <summary>
    ''' Lecture d'une chaine de caractères dans un fichier binaire
    ''' 20/07/07 :  Création - Version 1.00
    ''' </summary>
    ''' <param name="FileAccess">Numero du fichier à lire</param>
    ''' <param name="Address">Adresse binaire dans le fichier du premier caractère de la chaine</param>
    ''' <param name="Longueur">Longueur de la chaine à lire</param>
    ''' <returns></returns>
    Private Function FileGetString(ByVal FileAccess As Integer, ByVal Address As Long, ByVal Longueur As Integer) As String

        Dim Chaine As String = ""
        Dim MyChar As Char

        For i As Long = 0 To Longueur - 1
            FileGet(FileAccess, MyChar, Address + i)
            Chaine &= MyChar
        Next

        Return Chaine

    End Function

#End Region

End Module
