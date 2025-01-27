Imports PMXMoteur2
Imports System.Net
Imports System.IO
Imports System.Runtime.CompilerServices

Module Mod_Internet

    '==================================================================================================
    '
    '   Gestion des controles et telechargements sur internet
    '
    '==================================================================================================


#Region " Variables "

    'V2.06 --> On retourne sur .com, a priori plus stable dans le temps
    '    Dim AdresseInternet As String = "http://www.cticm.eu/maj"
    Dim AdresseInternetFirst As String = "https://www.cticm.com/maj"
    Dim AdresseInternetSecours As String = "http://www.cticm.com/maj"
    'Dim FichierUpDate As String = "Update_ACBPMX.txt"

    Public AdresseInternet As String
#End Region

#Region " Initialisation "

    Private Function FichierUpDate() As String
        '---------------------------------------------------------------------------------------------
        '   16/01/25 :  Création - POM
        '---------------------------------------------------------------------------------------------
        '   Donne le nom du fichier UpDate à récupérer sur MAJ
        '   en fonction du maitre
        '---------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------

        Dim Fichier As String = ""
        Select Case LogicielInfo.Maitre
            Case EnuMaitre.ArcelorMittal
                Fichier = "Update_ACB_New.txt"
            Case EnuMaitre.CTICM
                Fichier = "Update_PMX.txt"
        End Select

        Return Fichier
    End Function

    Sub InitialiseAccesInternet(ByRef lNewBasePro As Boolean, ByRef VBasePro As StrucVersionDtB, ByRef lNewBaseSteel As Boolean, ByVal VBaseSteel As StrucVersionDtB,
                                ByRef lNewVersion As Boolean, ByVal lBatch As Boolean, ByVal lDebug As Boolean)
        '---------------------------------------------------------------------------------------------
        '
        '   01/10/12 :  Création - Version 3.00
        '
        '---------------------------------------------------------------------------------------------
        '
        '   Si les acces internet de la machine sont disponibles
        '   et si les controles ont été requis par l'utilisateur
        '   on telecharge le fichier Update_EBplate 
        '   et(enventuellement) la nouvelle base de données des sections et des profilés
        '
        '---------------------------------------------------------------------------------------------
        '
        '   lNewBasePro     [S] :   Indique qu'une base des sections plus récente est disponible
        '   VBasePro        [S] :   Informations sur la version plus récente de base des profilés
        '   lNewBaseSteel   [S] :   Indique qu'une base des aciers plus récente est disponible
        '   VBaseSteel      [S] :   Informations sur la version plus récente de base des aciers
        '   lNewVersion     [S] :   Indique si une nouvelle version est disponible
        '   lBatch          [E] :   Indique si on fonctionne en batch
        '
        '---------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lFirstAttempt As Boolean = True
        Dim lCont As Boolean = True
        Dim KeyPro As String = "BASEPRO"
        Dim KeySte As String = "BASESTE"
        Const iMeth As Integer = 0
        Dim MyWbC As WebClient = New WebClient
        Dim lControlWebVersion As Boolean = LogicielOptions.lControlWebVersion
        Dim lControlWebFichier As Boolean = LogicielOptions.lControlWebFichier

        '--[ Initialisations

        AdresseInternet = AdresseInternetFirst

        lNewBasePro = False
        lNewBaseSteel = False
        lNewVersion = False
        If lDebug Then
            KeyPro = "DEBGPRO"
            KeySte = "DEBGSTE"
        End If

        '--[ Contrôle sur Internet

        If (lControlWebVersion Or lControlWebFichier) And (Not lBatch) Then     'V4.00 : on ne fait pas le controle en batch

            '--[ Si les controles sont demandés par l'utilisateur 

            If My.Computer.Network.IsAvailable Then

                '--[ Si le réseau est disponible, on récupère le fichier

                Dim FichierSource, FichierCible As String
                Dim lOK As Boolean = True

                FichierCible = LogicielRep.Config & "\" & FichierUpDate()

                Do While lCont        '======================================================================================================================

                    FichierSource = AdresseInternet & "/" & FichierUpDate()

                    Try
                        If My.Computer.FileSystem.FileExists(FichierCible) Then
                            My.Computer.FileSystem.DeleteFile(FichierCible)
                        End If
                        If lDebug Then
                            '# en mode débug, lecture sur l'ordinateur
                            FichierSource = My.Application.Info.DirectoryPath & "\..\..\..\UPDATES\" & FichierUpDate()
                            If Not My.Computer.FileSystem.FileExists(FichierSource) Then
                                MsgBox("Impossible de trouver " & FichierSource)
                            End If
                            File.Copy(FichierSource, FichierCible)
                        Else
                            '# Lecture et acces par internet

                            Select Case iMeth
                                Case 0
                                    '--> Téléchargement en bloquant le cache
                                    MyWbC.CachePolicy = New System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore)
                                    MyWbC.DownloadFile(FichierSource, FichierCible)

                                Case 1
                                    '--> Téléchargement
                                    My.Computer.Network.DownloadFile(FichierSource, FichierCible)

                            End Select
                        End If


                    Catch ex As Exception
                        lOK = False
                    End Try
                    If (Not My.Computer.FileSystem.FileExists(FichierCible)) And lFirstAttempt Then lOK = False

                    '--[ Si la récuperation a reussi

                    If lOK Then

                        Dim BlocLine As New Cls_LinesOfFile(FichierCible, True)

                        Dim Bloc As New Dictionary(Of String, String)

                        BlocLine.CreationBloc(Bloc)

                        If Not Bloc.ContainsKey("VERSION") Then
                            lOK = False
                        Else
                            Dim lTrouve As Boolean = Bloc.ContainsKey(KeyPro)

                            '--[ On analyse la version du fichier profilés disponible sur internet

                            If lTrouve And lControlWebFichier Then

                                Dim VersionPro As String = "", iFPro As Short
                                RecupereInfosAJourDataBase(Bloc, KeyPro, VersionPro, iFPro, lOK)

                                If lOK Then
                                    AnalyseInfoVersion(VersionPro, iFPro, VBasePro)
                                    lNewBasePro = ExisteVersionPlusRecenteMemeFormat(OptionsDatabase.VersionBaseProfiles, VBasePro)
                                End If

                            End If

                            '--[ On analyse la version du fichier aciers disponible sur internet

                            lTrouve = Bloc.ContainsKey(KeySte)
                            If lTrouve And lControlWebFichier Then

                                Dim VersionSte As String = "", iFSte As Short
                                RecupereInfosAJourDataBase(Bloc, KeySte, VersionSte, iFSte, lOK)

                                If lOK Then
                                    AnalyseInfoVersion(VersionSte, iFSte, VBaseSteel)
                                    lNewBaseSteel = ExisteVersionPlusRecenteMemeFormat(OptionsDatabase.VersionBaseAciers, VBaseSteel)
                                End If

                            End If

                            '--[ On analyse la version du logiciel disponible sur internet

                            lTrouve = Bloc.ContainsKey("VERSION")
                            If lTrouve And lControlWebVersion Then

                                Dim MyVersion As Single = CSng(TraiteReal(Bloc("VERSION")))
                                Dim VersionEnCours As Single '= VersionACB.Principal + VersionACB.Secondaire / 10.0! + VersionACB.Revision / 100.0!

                                '== V2.01 =========================
                                VersionEnCours = ABCPMXIndiceVersion()
                                '==================================

                                '==R17-010
                                lNewVersion = (MyVersion - VersionEnCours > 0.001)

                            End If
                        End If
                    End If
                    lCont = (Not lOK) And lFirstAttempt
                    If lCont Then
                        AdresseInternet = AdresseInternetSecours
                        lFirstAttempt = False
                        lOK = True
                    End If

                Loop        '======================================================================================================================

                If (Not lOK) And (Not lFirstAttempt) Then
                    If lDebug Then
                        MsgBox("Echec récupération fichiers web")
                    End If
                End If

            Else

                If lDebug Then
                    MsgBox("Accès internet non disponible")
                End If

            End If

        End If

    End Sub

#End Region

#Region " Analyse des informations "

    Public Sub RecupereInfosAJourDataBase(ByVal BlocMAJ As Dictionary(Of String, String), ByVal Balise As String,
                                          ByRef VersionAJour As String, ByRef iFormat As Short,
                                          ByRef lOK As Boolean)
        '---------------------------------------------------------------------------------
        '
        '   28/09/12 : Création - Version 3.00 - POM
        '
        '---------------------------------------------------------------------------------
        '
        '   Récupère dans le fichier internet les infos de mise à jour
        '   d'une base de données binaire (nouveau format)
        '
        '---------------------------------------------------------------------------------
        '
        '   BlocMAJ     [E] :   Liste des instructions de mise à jour
        '   Balise      [E] :   Balise où chercher l'information dans BlocMAJ
        '   VersionAJour[S] :   Version du fichier binaire
        '   iFormat     [S] :   Format du fichier binaire
        '   lOK         [S] :   Indique que la récupération s'est bien déroulée
        '
        '---------------------------------------------------------------------------------

        Dim Mots() As String = Nothing
        Dim nMots As Integer

        lOK = True
        If BlocMAJ.ContainsKey(Balise) Then

            DecomposeLine(BlocMAJ(Balise), SEPARATEURS, Mots, nMots)

            If nMots >= 2 Then
                VersionAJour = Mots(1)
                iFormat = CShort(Mots(2))
            Else
                lOK = False
            End If
        Else
            lOK = False
        End If

    End Sub

#End Region

#Region " Gestion recuperation info sur internet "


    Public Sub RecupereDonneesMAJDebug(ByRef BlocMAJ As Dictionary(Of String, String),
                                       ByRef lAccesOK As Boolean)
        '---------------------------------------------------------------------------------------------
        '   14/06/08 :  Création - Version 1.00
        '---------------------------------------------------------------------------------------------
        '   Simulation en local de récupération des données MAJ
        '---------------------------------------------------------------------------------------------
        '   BlocMAJ     [S] :   Messages récupérés dans le fichier des mises à jour
        '   lAccesOK    [S] :   Indique si on a eu acces à internet ou pas
        '---------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim FichierCible As String

        '--( Lecture en local

        FichierCible = LogicielRep.Config & "\" & FichierUpDate()

        FichierCible = LogicielRep.Install & "\..\..\..\UPDATES\" & FichierUpDate()

        If My.Computer.FileSystem.FileExists(FichierCible) Then
            RecupereBlocUpdate(FichierCible, BlocMAJ)
            lAccesOK = True
        Else
            lAccesOK = False
        End If
    End Sub

    Public _
        Sub RecupereDonneesMAJInternet(ByRef BlocMAJ As Dictionary(Of String, String),
                                       ByRef lAccesOK As Boolean)
        '---------------------------------------------------------------------------------------------
        '
        '   14/06/08 :  Création - Version 1.00
        '
        '---------------------------------------------------------------------------------------------
        '
        '   Si les acces internet de la machine sont disponibles
        '   on telecharge le fichier Update_ACBPlus 
        '   et on transmet le contenu des informations
        '
        '---------------------------------------------------------------------------------------------
        '
        '   BlocMAJ     [S] :   Messages récupérés dans le fichier des mises à jour
        '   lAccesOK    [S] :   Indique si on a eu acces à internet ou pas
        '
        '---------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim lFirstAttempt As Boolean = True
        Dim lCont As Boolean = True
        Const iMeth As Integer = 0
        Dim MyWbC As WebClient = New WebClient

        '--> Initialisation

        AdresseInternet = AdresseInternetFirst

        '--> Récupération internet

        If My.Computer.Network.IsAvailable Then

            '--[ Si le réseau est disponible, on récupère le fichier

            Dim FichierSource, FichierCible As String
            Dim lOK As Boolean = True

            FichierCible = LogicielRep.Config & "\" & FichierUpDate()

            Do While lCont
                FichierSource = AdresseInternet & "/" & FichierUpDate()

                Try
                    If My.Computer.FileSystem.FileExists(FichierCible) Then
                        My.Computer.FileSystem.DeleteFile(FichierCible)
                    End If
                    Select Case iMeth
                        Case 0
                            '--> Téléchargement en bloquant le cache
                            MyWbC.CachePolicy = New System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore)
                            MyWbC.DownloadFile(FichierSource, FichierCible)

                        Case 1
                            '--> Téléchargement

                            My.Computer.Network.DownloadFile(FichierSource, FichierCible)

                    End Select
                Catch ex As Exception
                    lOK = False
                    lAccesOK = False
                End Try
                If (Not My.Computer.FileSystem.FileExists(FichierCible)) And lFirstAttempt Then lOK = False

                '--[ Si la récuperation a reussi

                If lOK Then

                    lAccesOK = True
                    LireFichierUpDate(BlocMAJ)

                End If

                '--> A t on récupéré le bon fichier ?

                If Not BlocMAJ.ContainsKey("VERSION") Then lOK = False
                lCont = (Not lOK) And lFirstAttempt
                If lCont Then
                    AdresseInternet = AdresseInternetSecours
                    lFirstAttempt = False
                    lOK = True
                End If
            Loop
        Else

            lAccesOK = False

        End If

    End Sub

    Public Sub LireFichierUpDate(ByRef BlocMAJ As Dictionary(Of String, String))
        '------------------------------------------------------------------------------------
        '
        '   16/06/08 :  Création - Version 1.00
        '
        '------------------------------------------------------------------------------------
        '
        '   Lecture du fichier UpDate récupéré sur internet
        '
        '------------------------------------------------------------------------------------
        '
        '   BlocMAJ [S] :   Liste des instructions de mises à jour
        '
        '------------------------------------------------------------------------------------

        Dim FichierCible As String = LogicielRep.Config & "\" & FichierUpDate()

        RecupereBlocUpdate(FichierCible, BlocMAJ)

    End Sub

    Private Sub RecupereBlocUpdate(FichierCible As String, ByRef BlocMAJ As Dictionary(Of String, String))
        '------------------------------------------------------------------------------------
        '   02/08/24 :  Création - POM
        '------------------------------------------------------------------------------------
        '------------------------------------------------------------------------------------

        Dim BlocLine As New Cls_LinesOfFile(FichierCible, True)

        BlocLine.CreationBloc(BlocMAJ)

    End Sub

#End Region

End Module
