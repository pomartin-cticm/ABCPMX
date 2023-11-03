Option Strict Off
Option Explicit On

Module Mod_ProtectDll

    Const WRONGAPP As String = "Wrong application"
    Const WRONGDLL As String = "Wrong DLL"

    '==========================================================================
    ' ROUTINE EFFECTUANT LES TESTS DE LEGITIMITE D'APPEL DE DLL
    '==========================================================================

    Public Sub Protect_DLL(ByRef client As Short, ByRef Txt_Tag As Object, ByRef CodeERR As Short, ByRef TextERR As String)

        Dim J As Short
        Dim AppName As String
        Dim I As Short
        Dim found As Boolean
        Dim bbb As String
        Dim b3 As String
        Dim a8 As String
        Dim a7 As String
        Dim dll As String = ""
        Dim a6 As String
        Dim b2 As String
        Dim a5 As String
        Dim b1 As String
        Dim a4 As String
        Dim a3 As String
        Dim a2 As String
        Dim b0 As String
        Dim a1 As String
        Dim b4 As String
        Dim b5 As String
        Dim test_EXEname As Boolean
        Dim test_TAG As Boolean
        Dim test_DLLname As Boolean
        Dim aaaa As Short
        Dim versiondll As String = ""
        Dim c1, c2, c3 As Boolean

        aaaa = 1367
        If client = 2367 - aaaa Then  '1000
            CodeERR = 0
            TextERR = ""
            GoTo out
        End If

        'Initialisation code erreur
        CodeERR = 2000
        TextERR = WRONGAPP

        On Error GoTo out

        Select Case client
            Case 1   'VB
                '======================================================================================
                test_DLLname = True
                test_TAG = False
                test_EXEname = False
                '    '=============================================================================================
                'Case 200 + 93   'Arcelor Portal+
                '    '======================================================================================
                '    test_DLLname = True
                '    test_TAG = False
                '    test_EXEname = True
                '    '=============================================================================================            
        End Select

        'test de la présence de la DLL dans les applications lancées par l'application principale
        'et de son n° de version d'assembly
        '----------------------------------------------------------------------------------------

        If test_DLLname = False Then GoTo tag

        CodeERR = 2000 + 1
        TextERR = WRONGDLL

        Select Case client

            Case 1    'VB
                '============    CTICM_MODAL2D_NET   =========================================================
                a1 = "&CT$"
                b0 = "1&é3("
                a2 = "#ICM$"
                a3 = "é_M$"
                a4 = "%ODé"
                b3 = "-YS&"
                b1 = "-è2&"
                a5 = "àAL@"
                b2 = "@46h"
                a6 = "é2Dy"
                b4 = "@_a&"
                a7 = "é_Ny"
                b5 = "@*a&"
                a8 = "éETy"
                dll = Mid$(a1, 2, 2) & Mid$(a2, 2, 3) & Mid$(a3, 2, 2) & Mid$(a4, 2, 2) &
                       Mid$(a5, 2, 2)
                versiondll = "1.0.0.0"    'version de l'Assembly (voir Propriétés projet/Application/Informations de l'assembly)

                'Case 293    'PORTALPLUS
                '    '============    CTICM_ANALYS2D_PTP   =========================================================
                '    a1 = "&CT$"
                '    b0 = "1&é3("
                '    a2 = "#ICM$"
                '    a3 = "é_A$"
                '    a4 = "%NAé"
                '    b3 = "-YS&"
                '    b1 = "-è2&"
                '    a5 = "àLYS@"
                '    b2 = "@46h"
                '    a6 = "é2Dy"
                '    b4 = "@_a&"
                '    a7 = "é_Py"
                '    b5 = "@*a&"
                '    a8 = "éTPy"
                '    dll = Mid$(a1, 2, 2) & Mid$(a2, 2, 3) & Mid$(a3, 2, 2) & Mid$(a4, 2, 2) & _
                '           Mid$(a5, 2, 3) & Mid$(a6, 2, 2) & Mid$(a7, 2, 2) & Mid$(a8, 2, 2)
                '    versiondll = "1.0.0.0"  'version de l'Assembly (voir Propriétés projet/Application/Informations de l'assembly)
        End Select
        '=============================================================================================

        Dim cccc As System.Collections.ObjectModel.ReadOnlyCollection(Of System.Reflection.Assembly) = My.Application.Info.LoadedAssemblies
        Dim ddd As System.Reflection.Assembly
        Dim nnn As String

        For Each ddd In cccc
            nnn = UCase(Mid(ddd.FullName, 1, Math.Max(InStr(ddd.FullName, ",") - 1, 1)))
            If nnn = dll Then
                CodeERR = 2000 + 2
                If InStr(ddd.FullName, "Version=" & versiondll) Then
                    GoTo tag
                End If
            End If
        Next
        GoTo out

        'test du TAG de Txt_Recepteur (SI NECESSAIRE SEULEMENT!)
        '-------------------------------------------------------
tag:
        If test_TAG = False Then GoTo exename

        CodeERR = 2000 + 3
        TextERR = WRONGAPP

        If TypeName(Txt_Tag) = "TextBox" Then
            If Txt_Tag.tag = "   " Then
                GoTo exename
            End If
        End If
        GoTo out

        'test du nom de l'application appelante (SI NECESSAIRE SEULEMENT!)
        '-----------------------------------------------------------------
exename:
        If test_EXEname = False Then GoTo sauterexe

        CodeERR = 2000 + 4
        TextERR = WRONGAPP

        bbb = ""
        Select Case client
            Case 1
                '===========================    VB   ===========================================
                ' non testé
                bbb = ""       'nom de l'application qui doit appeler cette DLL (ex. LTBEAM)
                'Case 200 + 93
                '    '===========================    PORTALPLUS   ===========================================
                '    a1 = "&PO$"
                '    b0 = "1&é3("
                '    a2 = "#RTA$"
                '    a3 = "éLP$"
                '    a4 = "%LUé"
                '    b3 = "-YS&"
                '    b1 = "-è2&"
                '    a5 = "àS@"
                '    b2 = "@46h"
                '    bbb = Mid$(a1, 2, 2) & Mid$(a2, 2, 3) & Mid$(a3, 2, 2) & Mid$(a4, 2, 2) & _
                '           Mid$(a5, 2, 1)            
        End Select
        '=====================================================================================

        'vérification de l'existence du process dans le gestionnaire des tâches
        c1 = (UBound(Diagnostics.Process.GetProcessesByName(bbb)) >= 0)

        'vérification du nom de l'Assembly appelant
        c2 = (UCase(My.Application.Info.AssemblyName) = UCase(bbb))

        If Not c1 And c2 Then CodeERR = 2000 + 5
        If c1 And Not c2 Then CodeERR = 2000 + 6
        If Not c1 And Not c2 Then CodeERR = 2000 + 7
        If CodeERR > 2000 + 4 Then GoTo out

sauterexe:

        CodeERR = 0
        TextERR = ""

        Exit Sub

out:

        Select Case CodeERR
            Case 2000 : TextERR = "MODAL2D - Invalid application"                      'général
            Case 2001 : TextERR = "MODAL2D - Unloaded DLL"                    'Mauvais nom de DLL
            Case 2002 : TextERR = "MODAL2D - Invalid version of the loaded DLL"        'Version Assembly
            Case 2003 : TextERR = "MODAL2D - Invalid calling application (TAG)"      'Txt_Tag.Tag <> "   "
            Case 2004 : TextERR = "MODAL2D - Invalid application"                      'général
            Case 2005 : TextERR = "MODAL2D - Unrecognized calling application (EXE)"    'Nom du fichier .EXE
            Case 2006 : TextERR = "MODAL2D - Unrecognized calling application (ASS)"    'Nom de l'ASSEMBLY
            Case 2007 : TextERR = "MODAL2D - Unrecognized calling application (EXAS)"   'les 2
            Case Else : TextERR = "MODAL2D - Erreur " & CStr(Err.Number) & " : " & Err.Description
        End Select


    End Sub

End Module