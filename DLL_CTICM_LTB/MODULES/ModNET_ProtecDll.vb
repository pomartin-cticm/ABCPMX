Option Strict Off
Option Explicit On

Module Mod_ProtecDll

    Const WRONGAPP As String = "Wrong application"
    Const WRONGDLL As String = "Wrong DLL"

    '==========================================================================
    ' ROUTINE EFFECTUANT LES TEST DE LEGITIMITE D'APPEL DE DLL
    '==========================================================================

    Sub Protec_DLL(ByRef client As Short, ByRef Txt_Recepteur As Object, ByRef VP As Single, ByRef CodeERR As Short)
        Dim TextERR As String
        Dim J As Short
        Dim AppName As String
        Dim I As Short
        Dim found As Boolean
        Dim bbb As String = String.Empty
        Dim b3 As String
        Dim a8 As String
        Dim a7 As String
        Dim dll As String = String.Empty
        Dim a6 As String
        Dim b2 As String
        Dim a5 As String
        Dim b1 As String
        Dim a4 As String
        Dim a3 As String
        Dim a2 As String
        Dim b0 As String
        Dim a1 As String
        Dim test_EXEname As Boolean
        Dim test_TAG As Boolean
        Dim test_DLLname As Boolean

        If client = 1000 Then 'Pas de tests
            CodeERR = 0
            TextERR = ""
            VP = 0
            Exit Sub
        End If

        'Initialisation code erreur
        CodeERR = 999
        TextERR = WRONGAPP

        On Error GoTo out

        Select Case client
            Case 1
                'PMX =========================================================================================
                test_DLLname = True
                test_TAG = True
                test_EXEname = True
            Case 2
                'HERGOS ======================================================================================
                test_DLLname = True
                test_TAG = False
                test_EXEname = False
                '=============================================================================================
            Case 3
                'VB ======================================================================================
                test_DLLname = True
                test_TAG = False
                test_EXEname = False
                '=============================================================================================
        End Select

        'test du nom de la DLL (A ADAPTER EN FONCTION DE LA DESTINATION)
        '---------------------------------------------------------------
        CodeERR = 991
        If test_DLLname = False Then GoTo tag
        Select Case client
            Case 1
                'PMX ============    CTICM_LTBN_PMX   =========================================================
                a1 = "&CT$"
                b0 = "1&é3("
                a2 = "#ICM$"
                a3 = "é_Lù"
                a4 = "%TBé"
                b1 = "-èç&"
                a5 = "àN_P@"
                b2 = "@46h"
                a6 = "éMXy"
                dll = Mid(a1, 2, 2) & Mid(a2, 2, 3) & Mid(a3, 2, 2) & Mid(a4, 2, 2) & Mid(a5, 2, 3) & Mid(a6, 2, 2)
            Case 2
                'HERGOS ============    CTICM_LTBN_HERGOS   ===================================================
                a1 = "&CT$"
                b0 = "1&é3("
                a2 = "#ICM$"
                a3 = "é_Lù"
                a4 = "%TBé"
                b1 = "-èç&"
                a5 = "àN_H@"
                b2 = "@46h"
                a6 = "éERy"
                a7 = ")GOà"
                a8 = "éSRy"
                dll = Mid(a1, 2, 2) & Mid(a2, 2, 3) & Mid(a3, 2, 2) & Mid(a4, 2, 2) & Mid(a5, 2, 3) & Mid(a6, 2, 2) & Mid(a7, 2, 2) & Mid(a8, 2, 1)
            Case 3
                'VB ============    CTICM_LTBN_NET   =========================================================
                a1 = "&CT$"
                b0 = "1&é3("
                a2 = "#ICM$"
                a3 = "é_Lù"
                a4 = "%TBé"
                b1 = "-èç&"
                a5 = "àN_N@"
                b2 = "@46h"
                a6 = "éETy"
                dll = Mid(a1, 2, 2) & Mid(a2, 2, 3) & Mid(a3, 2, 2) & Mid(a4, 2, 2) & Mid(a5, 2, 3) & Mid(a6, 2, 2)
        End Select
        '=============================================================================================
        If UCase(My.Application.Info.AssemblyName) <> UCase(dll) Then
            VP = 0
            CodeERR = 997
            TextERR = WRONGDLL
            Exit Sub
        End If

        'test du TAG de Txt_Recepteur (SI NECESSAIRE SEULEMENT!)
        '-------------------------------------------------------
tag:
        CodeERR = 992
        If test_TAG = False Then GoTo exename
        If TypeName(Txt_Recepteur) = "TextBox" Then
            If Txt_Recepteur.tag <> "   " Then
                VP = 0
                CodeERR = 998
                TextERR = WRONGAPP
                Exit Sub
            End If
        End If

        'test du nom de l'application appelante (SI NECESSAIRE SEULEMENT!)
        '-----------------------------------------------------------------
exename:
        CodeERR = 993
        If test_EXEname = False Then GoTo sauterexe

        Dim ListeApp() As String = Nothing
        Dim ListeCheminApp() As String = Nothing
        Dim nApp As Short
        Dim nChem As Short
        'liste des applications en cours
        Call ListeApplications(ListeApp, nApp, ListeCheminApp, nChem)
        Select Case client
            Case 1
                'PMX ===========================    PMX_140   ========================================
                b1 = "&éPM[-"
                b2 = "('X#(&"
                b3 = "@é40Eà"
                bbb = Mid(b1, 3, 2) & Mid(b2, 3, 1)
            Case 2
                'HERGOS ===========================    -   ===========================================
                ' non testé
                bbb = ""
            Case 3
                'VB ===========================    -   ===========================================
                ' non testé
                bbb = ""
        End Select
        '=====================================================================================
        found = False
        For I = 1 To nApp
            AppName = ListeApp(I)
            J = InStr(AppName, ".")
            If J > 1 Then
                If Left(UCase(AppName), J - 1) = UCase(bbb) Then
                    found = True
                    Exit For
                End If
            End If
        Next I
        If found = False Then
            VP = 0
            CodeERR = 999
            TextERR = WRONGAPP
            Exit Sub
        End If

sauterexe:

        CodeERR = 0

out:

    End Sub

End Module