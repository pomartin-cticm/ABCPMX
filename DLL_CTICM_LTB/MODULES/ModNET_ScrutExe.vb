Option Strict Off
Option Explicit On

Module Mod_ScrutEXE
    '===================================================================================
    '   
    '   MODULE POUR LA RECHERCHE DES APPLICATIONS EN COURS
    '
    '===================================================================================

    Const TH32CS_SNAPHEAPLIST As Integer = &H1
    Const TH32CS_SNAPPROCESS As Integer = &H2
    Const TH32CS_SNAPTHREAD As Integer = &H4
    Const TH32CS_SNAPMODULE As Integer = &H8
    Const TH32CS_SNAPALL As Boolean = (TH32CS_SNAPHEAPLIST Or TH32CS_SNAPPROCESS Or TH32CS_SNAPTHREAD Or TH32CS_SNAPMODULE)
    Const TH32CS_INHERIT As Integer = &H80000000
    Const MAX_PATH As Short = 260

    Private Structure PROCESSENTRY32
        Dim dwSize As Integer
        Dim cntUsage As Integer
        Dim th32ProcessID As Integer
        Dim th32DefaultHeapID As Integer
        Dim th32ModuleID As Integer
        Dim cntThreads As Integer
        Dim th32ParentProcessID As Integer
        Dim pcPriClassBase As Integer
        Dim dwFlags As Integer
        'UPGRADE_WARNING: La taille de la chaîne de longueur fixe doit tenir dans la mémoire tampon. Cliquez ici : 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="3C1E4426-0B80-443E-B943-0627CD55D48B"'
        <VBFixedString(MAX_PATH), System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst:=MAX_PATH)> Public szExeFile() As Char
    End Structure

    Private Declare Function CreateToolhelp32Snapshot Lib "Kernel32" (ByVal lFlags As Integer, ByVal lProcessID As Integer) As Integer
    'UPGRADE_WARNING: La structure PROCESSENTRY32 peut nécessiter que des attributs de marshaling soient passés en tant qu'argument dans cette instruction Declare. Cliquez ici : 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
    Private Declare Function Process32First Lib "Kernel32" (ByVal hSnapShot As Integer, ByRef uProcess As PROCESSENTRY32) As Integer
    'UPGRADE_WARNING: La structure PROCESSENTRY32 peut nécessiter que des attributs de marshaling soient passés en tant qu'argument dans cette instruction Declare. Cliquez ici : 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
    Private Declare Function Process32Next Lib "Kernel32" (ByVal hSnapShot As Integer, ByRef uProcess As PROCESSENTRY32) As Integer
    Private Declare Sub CloseHandle Lib "Kernel32" (ByVal hPass As Integer)

    '=========================================================================================
    'fonctions et variables pour récuperer le nom de chacun des programmes en cours
    Private Const PROCESS_QUERY_INFORMATION As Integer = &H400 '1024
    Private Const PROCESS_VM_READ As Integer = &H10 '16
    Private Const OPEN_PROCESS_FLAGS As Boolean = PROCESS_QUERY_INFORMATION Or PROCESS_VM_READ
    Private Declare Function OpenProcess Lib "kernel32.dll" (ByVal dwDesiredAccessas As Integer, ByVal bInheritHandle As Integer, ByVal dwProcId As Integer) As Integer
    Private Declare Function EnumProcessModules Lib "PSAPI.DLL" (ByVal hProcess As Integer, ByRef lphModule As Integer, ByVal cb As Integer, ByRef cbNeeded As Integer) As Integer
    Private Declare Function GetModuleFileNameExA Lib "PSAPI.DLL" (ByVal hProcess As Integer, ByVal hModule As Integer, ByVal ModuleName As String, ByVal nSize As Integer) As Integer


    'Const MAX_FILENAME_LEN = 260
    'Private Declare Function FindExecutable Lib "shell32.dll" Alias "FindExecutableA" (ByVal lpFile As String, ByVal lpDirectory As String, ByVal lpResult As String) As Long

    Function NombreApplication(ByRef NomApp As String, ByRef lComplet As Boolean) As Short
        '
        '   31/01/06 :  v1.14 - Creation
        '
        '-------------------------------------------------------------------------------
        '
        '   Donne le nombre d'application spécifiées en cours
        '
        '-------------------------------------------------------------------------------
        '
        '   NomApp  [E] :   Nom de l'application (seul ou complet)
        '   lComplet[E] :   Indique si recherche sur nom complet de l'application
        '
        '-------------------------------------------------------------------------------

        Dim ListeApp() As String = Nothing
        Dim nApp As Short
        Dim ListeCheminApp() As String = Nothing
        Dim nChem As Short
        Dim nbRech As Integer
        Dim UCNomApp As String
        Dim I As Short

        Call ListeApplications(ListeApp, nApp, ListeCheminApp, nChem)

        nbRech = 0

        If lComplet Then

            'MsgBox "Nombre d'applications à tester : " & CStr(nChem)

            UCNomApp = UCase(NomApp)

            For I = 1 To nChem
                If (UCase(ListeCheminApp(I)) = UCNomApp) Then nbRech = nbRech + 1

                '            MsgBox "Application n°" & CStr(i) & " : " & ListeApp(i) & Chr(13) & ListeCheminApp(i)
                '        End If
            Next I

        Else

            UCNomApp = UCase(NomApp)

            'MsgBox "Nombre d'applications à tester : " & CStr(nApp)

            For I = 1 To nApp
                If UCase(ListeApp(I)) = UCNomApp Then nbRech = nbRech + 1
            Next I

        End If

        NombreApplication = nbRech

        'MsgBox "J'ai trouvé " & CStr(nbRech) & " applications de " & NomApp
    End Function


    Sub ListeApplications(ByRef ListeApp() As String, ByRef nApp As Short, ByRef ListeCheminApp() As String, ByRef nChem As Short)
        '
        '   31/01/06 :  Création - version 1.14
        '
        '------------------------------------------------------------------------------
        '
        '   Liste des applications en cours d'execution
        '
        '------------------------------------------------------------------------------
        '
        '   ListeApp        [S] :   Liste des applications en cours
        '   nApp            [S] :   Nombre d'application en cours
        '   ListeCheminApp  [S] :   Liste des chemins complets des applications en cours
        '   nChem           [S] :   Nombre de chemins complets des applications en cours
        '
        '------------------------------------------------------------------------------

        Dim hSnapShot As Integer
        Dim uProcess As PROCESSENTRY32 = Nothing
        Dim valReturnProcess, r, valReturn As Integer
        'UPGRADE_WARNING: La limite inférieure du tableau lngModules est passée de 1 à 0. Cliquez ici : 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
        Dim lngModules(200) As Integer
        Dim lngSize, lngCBSize2, lngReturn As Integer
        Dim strModuleName As String

        'Takes a snapshot of the processes and the heaps, modules, and threads used by the processes
        hSnapShot = CreateToolhelp32Snapshot(TH32CS_SNAPALL, 0)

        'set the length of our ProcessEntry-type
        uProcess.dwSize = Len(uProcess)

        'Retrieve information about the first process encountered in our system snapshot
        r = Process32First(hSnapShot, uProcess)

        nApp = 0
        nChem = 0

        Dim Chemin As String
        Do While r
            nApp = nApp + 1
            If nApp = 1 Then
                'UPGRADE_WARNING: La limite inférieure du tableau ListeApp est passée de 1 à 0. Cliquez ici : 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
                ReDim ListeApp(nApp)
            Else
                'UPGRADE_WARNING: La limite inférieure du tableau ListeApp est passée de 1 à 0. Cliquez ici : 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
                ReDim Preserve ListeApp(nApp)
            End If
            ListeApp(nApp) = Left(uProcess.szExeFile, IIf(InStr(1, uProcess.szExeFile, Chr(0)) > 0, InStr(1, uProcess.szExeFile, Chr(0)) - 1, 0))
            '    'Retrieve information about the next process recorded in our system snapshot
            '    r = Process32Next(hSnapShot, uProcess)

            '--> recherche chemin complet sur disque


            Chemin = ""

            valReturnProcess = OpenProcess(PROCESS_VM_READ + PROCESS_QUERY_INFORMATION, 0, uProcess.th32ProcessID)

            If valReturnProcess <> 0 Then
                valReturn = EnumProcessModules(valReturnProcess, lngModules(1), 200, lngCBSize2)

                If valReturn <> 0 Then
                    strModuleName = Space(MAX_PATH)
                    lngSize = 500
                    lngReturn = GetModuleFileNameExA(valReturnProcess, lngModules(1), strModuleName, lngSize)
                    Chemin = Left(strModuleName, lngReturn)
                    Chemin = Trim(Chemin)
                End If
            End If

            If Len(Chemin) <> 0 Then
                nChem = nChem + 1
                If nChem = 1 Then
                    'UPGRADE_WARNING: La limite inférieure du tableau ListeCheminApp est passée de 1 à 0. Cliquez ici : 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
                    ReDim ListeCheminApp(nChem)
                Else
                    'UPGRADE_WARNING: La limite inférieure du tableau ListeCheminApp est passée de 1 à 0. Cliquez ici : 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
                    ReDim Preserve ListeCheminApp(nChem)
                End If
                ListeCheminApp(nChem) = Chemin
                'MsgBox "Application n°" & CStr(nChem) & " : " & Right(Chemin, 30)
            End If

            'Retrieve information about the next process recorded in our system snapshot
            r = Process32Next(hSnapShot, uProcess)
        Loop

        'close our snapshot handle
        CloseHandle(hSnapShot)

    End Sub

End Module
