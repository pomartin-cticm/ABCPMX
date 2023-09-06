Option Strict Off
Option Explicit On

Imports System.Runtime.InteropServices

Module Mod_ComDll

    Const RIGID As Double = 1.0E+30

    '================================================================================================
    'Déclaration de la DLL Fortran Cticm_ResolVP
    '================================================================================================

    'permet l'utilisation de AddressOf dans SetWindowLong
    Delegate Function SubClassProcDelegate(ByVal hwnd As Integer, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    'permet l'import de la DLL Fortran "cticm_resolvp.dll" et l'appel de la routine "RESONVP" (case sensitive!)
    'DLL à mettre dans le    répertoire de l'application ou dans \windows\system32
    '---------------------------------------------------------------------------------------------------------
    'Import pour 64 bits
    '-------------------
    <DllImport("cticm_resolvp_64.dll", CharSet:=CharSet.Unicode)> _
    Sub RESONVP_64(ByRef JJOBVR As Integer, ByRef IRANGE As Integer, _
               ByRef N As Integer, ByVal a(,) As Double, ByVal B(,) As Double, ByRef VL As Double, ByRef VU As Double, _
               ByRef IL As Integer, ByRef IU As Integer, ByRef ABSTOL As Double, ByRef M As Integer, ByVal W() As Double, _
               ByVal Z(,) As Double, ByVal WORK() As Double, ByRef LWORK As Integer, ByVal IWORK() As Integer, _
               ByVal IFAIL() As Integer, ByRef INFO As Integer, ByRef HWNDFRM As Integer, ByRef IDCTRL As Integer, ByRef DUMP As Integer)
    End Sub

    'Import pour 32 bits
    '-------------------
    <DllImport("cticm_resolvp_32.dll", CharSet:=CharSet.Unicode)> _
    Sub RESONVP_32(ByRef JJOBVR As Integer, ByRef IRANGE As Integer, _
               ByRef N As Integer, ByVal a(,) As Double, ByVal B(,) As Double, ByRef VL As Double, ByRef VU As Double, _
               ByRef IL As Integer, ByRef IU As Integer, ByRef ABSTOL As Double, ByRef M As Integer, ByVal W() As Double, _
               ByVal Z(,) As Double, ByVal WORK() As Double, ByRef LWORK As Integer, ByVal IWORK() As Integer, _
               ByVal IFAIL() As Integer, ByRef INFO As Integer, ByRef HWNDFRM As Integer, ByRef IDCTRL As Integer, ByRef DUMP As Integer)
    End Sub

    '================================================================================================
    'Déclaration des fonctions nécessaires à la communication avec la DLL Fortran Cticm_ResolVP
    '================================================================================================

    'Déclarations routines et variables globales 32 bits
    '---------------------------------------------------
    Declare Function GetDlgCtrlID Lib "user32" (ByVal hwnd As Integer) As Integer
    Declare Function SetWindowLongI Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer
    Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer, ByVal dwNewLong As SubClassProcDelegate) As Integer
    Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Integer, ByVal nIndex As Integer) As Integer
    Declare Function CallWindowProc Lib "user32" Alias "CallWindowProcA" (ByVal lpPrevWndFunc As Integer, ByVal hwnd As Integer, ByVal MSG As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    Declare Function SetWindowText Lib "user32" Alias "SetWindowTextA" (ByVal hwnd As Integer, ByVal lpString As String) As Integer
    Public Const GWL_WNDPROC_32 As Short = (-4)
    Public preWinProc_32 As Integer
    Public TXTPROGRESSHWND_32 As Integer 'Variable globale pour transmission % dans Txt_Progress

    'Déclarations routines et variables globales 64 bits
    '---------------------------------------------------
    Declare Function GetDlgCtrlID Lib "user32" (ByVal hwnd As Long) As Long
    Declare Function SetWindowLongI Lib "user32" Alias "SetWindowLongPtrA" (ByVal hwnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long
    Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongPtrA" (ByVal hwnd As Long, ByVal nIndex As Long, ByVal dwNewLong As SubClassProcDelegate) As Long
    Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongPtrA" (ByVal hwnd As Long, ByVal nIndex As Long) As Long
    Declare Function CallWindowProc Lib "user32" Alias "CallWindowProcA" (ByVal lpPrevWndFunc As Long, ByVal hwnd As Long, ByVal MSG As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
    Declare Function SetWindowText Lib "user32" Alias "SetWindowTextA" (ByVal hwnd As Long, ByVal lpString As String) As Long
    Public Const GWL_WNDPROC_64 As Short = (-4)
    Public preWinProc_64 As Long
    Public TXTPROGRESSHWND_64 As Long 'Variable globale pour transmission % dans Txt_Progress

    '================================================================================================

    '================================================================================================
    'Routine de traitement du message transmis par la DLL Fortran Cticm_ResolVP à l'objet "récepteur"
    '================================================================================================

    'Version pour 32 bits
    '--------------------
    Public Function WndProc_32(ByVal hwnd As Integer, ByVal MSG As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
        Dim ValRef As Integer
        Dim txt As String

        ValRef = 9998  'valeur de communication

        'Filtre pour les messages envoyés par la DLL Fortran ResonVP
        If MSG = ValRef Then
            'Affichage du % dans un contrôle TextBox (SAUF L'OBJET "RECEPTEUR!!!)
            'défini par son handle
            txt = CStr(wParam) & " %" 'Le % est dans wParam (0 à 100)
            SetWindowText(TXTPROGRESSHWND_32, txt)
            System.Windows.Forms.Application.DoEvents()
        End If

        'Traitement normal des messages par l'objet "récepteur"
        WndProc_32 = CallWindowProc(preWinProc_32, hwnd, MSG, wParam, lParam)

    End Function

    'Version pour 64 bits
    '--------------------
    Public Function WndProc_64(ByVal hwnd As Integer, ByVal MSG As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
        Dim ValRef As Integer
        Dim txt As String

        ValRef = 9998  'valeur de communication

        'Filtre pour les messages envoyés par la DLL Fortran ResonVP
        If MSG = ValRef Then
            'Affichage du % dans un contrôle TextBox (SAUF L'OBJET "RECEPTEUR!!!)
            'défini par son handle
            txt = CStr(wParam) & " %" 'Le % est dans wParam (0 à 100)
            SetWindowText(TXTPROGRESSHWND_64, txt)
            System.Windows.Forms.Application.DoEvents()
        End If

        'Traitement normal des messages par l'objet "récepteur"
        WndProc_64 = CallWindowProc(preWinProc_64, hwnd, MSG, wParam, lParam)

    End Function

    '================================================================================================
    'Routine de préparation à la résolution VP par la DLL Fortran Cticm_ResolVP
    '================================================================================================
    Sub ResolVP(ByRef RA() As Double, ByRef RB() As Double, ByRef IDIM As Short, ByRef tolerance As Double, _
                ByRef NoVectP As Boolean, ByRef Txt_Recepteur As Object, ByRef Txt_Progress As Object, _
                ByRef VP() As Double, ByRef VectP(,) As Double, ByRef NBVP As Integer, ByRef DUMP As Integer)

        CodeERR = 51

        Dim INFO, JOBVP, N, LWORK As Integer
        Dim IL, M, IU As Integer
        Dim I, J As Short
        Dim IRANGE As Integer

        N = IDIM
        LWORK = 8 * N

        Dim Reso_A(N, N) As Double
        Dim Reso_B(N, N) As Double

        CodeERR = 52

        'Passage des matrices triangulaires inférieures RA et RB aux matrices carrées Reso_A et Reso_B
        '---------------------------------------------------------------------------------------------
        For I = 1 To N
            For J = 1 To I
                Reso_A(I, J) = RA(JND(I, J))
                Reso_B(I, J) = RB(JND(I, J))
            Next J
        Next I

        'symétrie
        For I = 1 To N
            For J = I To N
                Reso_A(I, J) = Reso_A(J, I)
                Reso_B(I, J) = Reso_B(J, I)
            Next J
        Next I

        'Indicateur calcul de vecteurs propres
        '-------------------------------------
        If NoVectP Then
            JOBVP = 0 '1: VP, 0: pas VP
        Else
            JOBVP = 1 '1: VP, 0: pas VP
        End If

        'Nombre de valeurs propres
        '-------------------------
        '        NBVP = 1 '1 vP seulement ici
        IRANGE = 1 '0: toutes, 1:ILème à IUème, 2:de VL à VU
        If NBVP = N Then IRANGE = 0 'toutes
        IL = N - NBVP + 1 'rang de la 1ère
        IU = N 'rang de la dernière

        'Dimensions tableaux de travail
        '------------------------------
        Dim ALPHAR(N) As Double
        Dim Reso_VP(N, N) As Double
        Dim IFAIL(N) As Integer

        'Corrections dues au chgt d'indice mini (1 --> 0) dans les tableaux avec VB2008
        'De ce fait, les matrices sont de dimensions N+1  (0 --> N)
        '------------------------------------------------------------------------------
        'Dim WORK(LWORK) As Double
        'Dim IWORK(5 * N) As Integer
        Dim WORK(LWORK + 8) As Double
        Dim IWORK(5 * N + 5) As Integer
        N = N + 1
        IL = IL + 1
        IU = IU + 1
        LWORK = LWORK + 8
        Reso_A(0, 0) = RIGID  'on fixe une valeur infinie sur la diagonale de A
        Reso_B(0, 0) = 0

        '============ADAPTATION TMN
        'limites de NBVP
        If NBVP < 1 Then NBVP = 1
        If NBVP > IDIM Then NBVP = IDIM
        ReDim VP(NBVP)
        For I = 1 To NBVP
            VP(I) = 0
        Next I
        ReDim VectP(NBVP, IDIM)

        '========== FIN ADAPTATION POM 

        ''''''XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        ''''''STOCKAGE DES MATRICES DANS UN FICHIER POUR TESTS ANNEXES AVEC UNE AUTRE APPLICATION
        ''''''MATRICES : A(linéaire, définie positive) et B(géométrique)
        ''''''Dim A(N-1,N-1), B(N-1,N-1) As Double    1er indice: 0
        ''''''----------------------------------------------------------------------------------------
        '''''FileOpen(1, "MatAB", OpenMode.Binary)
        '''''FilePut(1, N)  'nb de lignes/colonnes des matrices
        '''''FilePut(1, Reso_A)
        '''''FilePut(1, Reso_B)
        '''''FileClose(1)

        ''''''RELECTURE TEST
        ''''''--------------
        '''''FileOpen(1, "MatAB", OpenMode.Binary)
        '''''FileGet(1, N)
        '''''Dim Reso_AA(N - 1, N - 1) As Double    'car indice commence à 0
        '''''Dim Reso_BB(N - 1, N - 1) As Double
        '''''FileGet(1, Reso_AA)
        '''''FileGet(1, Reso_BB)
        '''''FileClose(1)
        ''''''XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX


        'Explications
        '------------
        ' La routine de LAPACK impose d'avoir B définie positive, mais pas A
        ' Donc ici on va résoudre : RB - VPI*RA = 0  puisque RA est définie positive
        ' On obtient donc VPI inverse de VP recherchée
        ' Calculer VP mini positive revient donc à calculer VPI maxi positive (VPI = ALPHAR())
        ' Si on en cherche qu'une, ce sera donc la plus grande, donc celle de rang N
        ' Si on en cherche plusieurs, on décompte à partir de N pour calculer IL
        ' A la sortie, on calcule VP = 1/VPI
        ' Le Vecteur Propre correspondant à VP(I) est dans la colonne I de Reso_VP()
        ' où I est le rang dans le groupe de VP demandé

        CodeERR = 53


        '************************************************************************************************
        'Communication avec la DLL Fortran Cticm_ResolVP
        '===============================================

        Dim HWNDFRM_32, IDCTRL_32, ret_32 As Integer
        Dim HWNDFRM_64, IDCTRL_64, ret_64 As Long

        Dim MYCTRL As System.Windows.Forms.Control = Nothing

        Select Case IntPtr.Size

            Case 8 ' --> 64 bits

                'Faut-il communiquer l'info sur la progression?
                '----------------------------------------------
                Dim TxtRecepteur As String = ""
                Dim TxtProgress As String = ""
                TxtRecepteur = TypeName(Txt_Recepteur)
                TxtProgress = TypeName(Txt_Progress)
                If TxtRecepteur <> "TextBox" Or TxtProgress <> "TextBox" Then 'si TextBoxes, ils contiennent le Caption
                    IDCTRL_64 = 0  'communication supprimée dans la DLL Fortran
                    HWNDFRM_64 = 0 'communication supprimée dans la DLL Fortran
                    GoTo suite1 'on saute la mise en place de la communication
                End If

                'mise en place de la communication
                '---------------------------------
                MYCTRL = Txt_Recepteur 'TextBox réceptrice dans la feuille de l'application appelante
                IDCTRL_64 = GetDlgCtrlID(MYCTRL.Handle.ToInt64)
                'UPGRADE_WARNING: Control propriété MYCTRL.Parent a été mis à niveau vers MYCTRL.FindForm, qui a un nouveau comportement. Cliquez ici : 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
                HWNDFRM_64 = MYCTRL.FindForm.Handle.ToInt64 'hwnd de la fenêtre contenant Txt_Recepteur
                'Identification de la Procédure normale de l'objet "récepteur" Txt_Recepteur
                preWinProc_64 = GetWindowLong(MYCTRL.Handle.ToInt64, GWL_WNDPROC_64) 'donne GWL_WNDPROC
                'Redirection vers la Procédure particulière de l'objet "récepteur"
                'UPGRADE_WARNING: Ajouter un delegate pour AddressOf WndProc Cliquez ici : 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="E9E157F7-EF0C-4016-87B7-7D7FBBC6EE08"'
                ret_64 = SetWindowLong(MYCTRL.Handle.ToInt64, GWL_WNDPROC_64, AddressOf WndProc_64)
                'Handle de Txt_Progress pour passage à WndProc via variable globale TXTPROGRESSHWND
                TXTPROGRESSHWND_64 = Txt_Progress.Handle.ToInt64

            Case Else ' 4  --> 32 bits

                'Faut-il communiquer l'info sur la progression?
                '----------------------------------------------
                Dim TxtRecepteur As String = ""
                Dim TxtProgress As String = ""
                TxtRecepteur = TypeName(Txt_Recepteur)
                TxtProgress = TypeName(Txt_Progress)
                If TxtRecepteur <> "TextBox" Or TxtProgress <> "TextBox" Then 'si TextBoxes, ils contiennent le Caption
                    IDCTRL_32 = 0  'communication supprimée dans la DLL Fortran
                    HWNDFRM_32 = 0 'communication supprimée dans la DLL Fortran
                    GoTo suite1 'on saute la mise en place de la communication
                End If

                'mise en place de la communication
                '---------------------------------
                MYCTRL = Txt_Recepteur 'TextBox réceptrice dans la feuille de l'application appelante
                IDCTRL_32 = GetDlgCtrlID(MYCTRL.Handle.ToInt32)
                'UPGRADE_WARNING: Control propriété MYCTRL.Parent a été mis à niveau vers MYCTRL.FindForm, qui a un nouveau comportement. Cliquez ici : 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="DFCDE711-9694-47D7-9C50-45A99CD8E91E"'
                HWNDFRM_32 = MYCTRL.FindForm.Handle.ToInt32 'hwnd de la fenêtre contenant Txt_Recepteur
                'Identification de la Procédure normale de l'objet "récepteur" Txt_Recepteur
                preWinProc_32 = GetWindowLong(MYCTRL.Handle.ToInt32, GWL_WNDPROC_32) 'donne GWL_WNDPROC
                'Redirection vers la Procédure particulière de l'objet "récepteur"
                'UPGRADE_WARNING: Ajouter un delegate pour AddressOf WndProc Cliquez ici : 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="E9E157F7-EF0C-4016-87B7-7D7FBBC6EE08"'
                ret_32 = SetWindowLong(MYCTRL.Handle.ToInt32, GWL_WNDPROC_32, AddressOf WndProc_32)
                'Handle de Txt_Progress pour passage à WndProc via variable globale TXTPROGRESSHWND
                TXTPROGRESSHWND_32 = Txt_Progress.Handle.ToInt32

        End Select

suite1:

        'Appel de la DLL
        '---------------
        CodeERR = 54

        Select Case IntPtr.Size

            Case 8 ' --> 64 bits

                '================================================================================================
                Call RESONVP_64(JOBVP, IRANGE, N, Reso_B, Reso_A, 0, 0, IL, IU, tolerance, M, ALPHAR, Reso_VP, _
                                WORK, LWORK, IWORK, IFAIL, INFO, HWNDFRM_64, IDCTRL_64, DUMP)
                '================================================================================================

            Case Else '4 --> 32 bits 

                '================================================================================================
                Call RESONVP_32(JOBVP, IRANGE, N, Reso_B, Reso_A, 0, 0, IL, IU, tolerance, M, ALPHAR, Reso_VP, _
                                WORK, LWORK, IWORK, IFAIL, INFO, HWNDFRM_32, IDCTRL_32, DUMP)
                '================================================================================================

        End Select

        'Correction en sens inverse de N (due à chgt indice mini par VB2008)
        N = N - 1

        'Rétablissement de la Procédure normale de l'objet "récepteur"
        '-------------------------------------------------------------
        Select Case IntPtr.Size

            Case 8 ' --> 64 bits

                If HWNDFRM_64 <> 0 And IDCTRL_64 <> 0 Then
                    ret_64 = SetWindowLongI(MYCTRL.Handle.ToInt64, GWL_WNDPROC_64, preWinProc_64)
                    System.Windows.Forms.Application.DoEvents()
                End If

            Case Else '4  --> 32 bits

                If HWNDFRM_32 <> 0 And IDCTRL_32 <> 0 Then
                    ret_32 = SetWindowLongI(MYCTRL.Handle.ToInt32, GWL_WNDPROC_32, preWinProc_32)
                    System.Windows.Forms.Application.DoEvents()
                End If

        End Select

        '************************************************************************************************
        ' M est le nb de VP calculées
        ' INFO = 0   tout est OK
        ' un % d'avancement (0 à 100) est transmis en continu via wParam dans la routine WndProc()

        CodeERR = 55

        'Boucle sur les valeurs propres
        '------------------------------
        For J = 1 To NBVP

            'Valeur propre
            '-------------
            'ALPHAR(J-1) est la Jième VP la plus grande du problème inverse (RB - VP*RA = 0)
            If ALPHAR(NBVP - J) <> 0 Then
                VP(J) = 1 / ALPHAR(NBVP - J) 'car on a resolu RB-VP*RA=0
            Else
                VP(J) = 0 'calcul non abouti ou VP complexe
            End If

            'Vecteur propre
            '--------------
            If VP(J) > 0 Then
                For I = 0 To N
                    VectP(J, I) = Reso_VP(NBVP - J, I)
                Next I
            End If

        Next J

        'on libère la place occupée par les tableaux de travail
        Erase Reso_VP, Reso_A, Reso_B, IFAIL, ALPHAR, WORK, IWORK

        CodeERR = 56


    End Sub

End Module
