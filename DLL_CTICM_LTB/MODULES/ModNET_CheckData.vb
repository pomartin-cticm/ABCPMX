Option Strict Off
Option Explicit On

Module Mod_CheckData

    'CONSTANTES
    'Limites pour les données transmises par arguments
    Const EPSLIM As Double = 0.0001
    Const NQMAX As Short = 1000 'on enlève la limite en mettant un nombre important!
    Const NBELEMMAX As Short = 300
    Const NBELEMMIN As Short = 20
    Const E_LIMINF As Double = 100 * 1000000.0 'N/m2
    Const E_LIMSUP As Double = 1000000 * 1000000.0 'N/m2
    Const NU_LIMINF As Double = 0.1 '=G/E
    Const NU_LIMSUP As Double = 0.5
    Const LELEM_LIMINF As Short = 0.01 'm
    Const LELEM_LIMSUP As Short = 3 'm
    Const IZ_LIMINF As Double = 0.0001 * 0.00000001 'm4
    Const IZ_LIMSUP As Double = 1000.0 'm4
    Const IT_LIMINF As Double = 0.001 * 0.00000001 'm4
    Const IT_LIMSUP As Double = 1000.0 'm4
    Const IW_LIMINF As Short = 0 'm4
    Const IW_LIMSUP As Double = 1000 'm4
    Const BETAZ_LIM As Short = 1.5 'm
    Const GAMMA_LIMSUP As Double = 0.3

    '=====================================================================================================
    ' ROUTINE DE VERIFICATION DES DONNEES
    '=====================================================================================================
    Sub Check_Data(ByRef NB As Integer, ByRef E As Double, ByRef G As Double, ByRef XLONG() As Double,
                   ByRef XIZ() As Double, ByRef XIT() As Double, ByRef XIW() As Double, ByRef XBETAZ() As Double,
                   ByRef XZC() As Double, ByRef XRYZ() As Double, ByRef KT() As Double, ByRef KVP() As Double,
                   ByRef KTP() As Double, ByRef ZN() As Double, ByRef RV() As Double, ByRef RTX() As Double,
                   ByRef RTZ() As Double, ByRef RTXX() As Double, ByRef ZR() As Double, ByRef XM1() As Double,
                   ByRef XM2() As Double, ByRef XFN() As Double, ByRef NF As Short, ByRef NQ As Short,
                   ByRef FF() As Double, ByRef XLF() As Double, ByRef ZFF() As Double, ByRef QQ1() As Double,
                   ByRef XLq1() As Double, ByRef QQ2() As Double, ByRef XLq2() As Double, ByRef ZQQ() As Double,
                   ByRef FNBLOCKED As Boolean, ByRef MYBLOCKED As Boolean,
                   ByRef MomMaxdaNcm As Double, ByRef xfMomMax As Double, ByRef NMaxdaN As Double,
                   ByRef xfNMax As Double, ByRef CodeERR As Short)

        Dim NFMAX As Short
        Dim ixfmax As Short
        Dim gam As Double
        Dim tnGamma As Double
        Dim I As Short
        Dim Ltot As Double
        Dim nbgamout As Integer

        Dim eps10 As Double = 0.005

        'Paramètres généraux
        '-------------------
        If NB < NBELEMMIN Or NB > NBELEMMAX Then CodeERR = 102 : GoTo out1
        If E < (1 - EPSLIM) * E_LIMINF Or E > (1 + EPSLIM) * E_LIMSUP Then CodeERR = 103 : GoTo out1
        If G / E > (1 + EPSLIM) * NU_LIMSUP Or G / E < (1 - EPSLIM) * NU_LIMINF Then CodeERR = 104 : GoTo out1

        'Caractéristiques des barres
        '---------------------------
        Ltot = 0
        nbgamout = 0  'nb d'éléments avec un angle gamma > GAMMA_LIMSUP

        For I = 1 To NB

            'KT, KVP et KTP : ressorts à l'origine de l'élément  (supposés infinis à l'extrémité)
            If XLONG(I) < (1 - EPSLIM) * LELEM_LIMINF Or XLONG(I) > (1 + EPSLIM) * LELEM_LIMSUP Then CodeERR = 105 : GoTo out1 '>1 cm
            If XIZ(I) < (1 - EPSLIM) * IZ_LIMINF Then CodeERR = 106 : GoTo out1 '>1 cm4
            If XIZ(I) > (1 + EPSLIM) * IZ_LIMSUP Then CodeERR = 106 : GoTo out1
            If XIT(I) < (1 - EPSLIM) * IT_LIMINF Then CodeERR = 107 : GoTo out1 '>0.1 cm4
            If XIT(I) > (1 + EPSLIM) * IT_LIMSUP Then CodeERR = 107 : GoTo out1 '
            If XIW(I) < (1 - EPSLIM) * IW_LIMINF Then CodeERR = 108 : GoTo out1 '>0 cm4
            If XIW(I) > (1 + EPSLIM) * IW_LIMSUP Then CodeERR = 108 : GoTo out1 '
            If System.Math.Abs(XBETAZ(I)) > (1 + EPSLIM) * BETAZ_LIM Then CodeERR = 109 : GoTo out1 '>0 cm4
            If KT(I) <> -1 And KT(I) < 0 Then CodeERR = 110 : GoTo out1
            If KVP(I) <> -1 And KVP(I) < 0 Then CodeERR = 110 : GoTo out1
            If KTP(I) <> -1 And KTP(I) < 0 Then CodeERR = 110 : GoTo out1

            '      XZC(I) = 0  'car dans données LTBeam, zF / centre de cisaillement ( et non zg)   ??????????

            'inclinaison des barres
            tnGamma = (ZN(I + 1) - ZN(I)) / XLONG(I) 'tangente(Gamma)
            gam = System.Math.Atan(tnGamma)
            If System.Math.Abs(gam) > (1 + EPSLIM) * GAMMA_LIMSUP Then
                nbgamout = nbgamout + 1
            End If

            'longueur totale projetée
            Ltot = Ltot + XLONG(I)

        Next I
        'si nb d'éléments avec angle gamma > GAMMA_LIMSUP  --> sortie  (on tolère jusqu'à 6 éléments pour traiter les extrémités de renforts avec semelle filante)
        If nbgamout > 6 Then
            CodeERR = 118 : GoTo out1
        End If

        'Maintiens latéraux aux noeuds
        '-----------------------------
        For I = 1 To NB + 1
            If RV(I) <> -1 And RV(I) < 0 Then CodeERR = 111 : GoTo out1
            If RTX(I) <> -1 And RTX(I) < 0 Then CodeERR = 111 : GoTo out1
            If RTZ(I) <> -1 And RTZ(I) < 0 Then CodeERR = 111 : GoTo out1
            If RTXX(I) <> -1 And RTXX(I) < 0 Then CodeERR = 111 : GoTo out1
        Next I

        'Présence d'efforts
        '------------------
        'Moment maxi
        MomMaxdaNcm = 0
        For I = 1 To NB
            If System.Math.Abs(XM1(I)) > System.Math.Abs(MomMaxdaNcm) Then MomMaxdaNcm = XM1(I) : ixfmax = I
            If System.Math.Abs(XM2(I)) > System.Math.Abs(MomMaxdaNcm) Then MomMaxdaNcm = XM2(I) : ixfmax = I + 1
        Next I
        xfMomMax = 0
        For I = 1 To ixfmax - 1
            xfMomMax = xfMomMax + XLONG(I)
        Next I
        xfMomMax = xfMomMax / Ltot
        'Effort normal maxi
        NMaxdaN = 0
        For I = 1 To NB
            If System.Math.Abs(XFN(I)) > System.Math.Abs(NMaxdaN) Then
                NMaxdaN = XFN(I) : ixfmax = I
            End If
        Next I
        xfNMax = 0
        For I = 1 To ixfmax - 1
            xfNMax = xfNMax + XLONG(I)
        Next I
        xfNMax = xfNMax / Ltot
        'contrôle
        If NMaxdaN = 0 And MomMaxdaNcm = 0 Then CodeERR = 112 : GoTo out1

        'Charges sur poutre
        '------------------
        NFMAX = NB + 1
        If NF < 0 Or NF > NFMAX Then CodeERR = 113 : GoTo out1
        If NQ < 0 Or NQ > NQMAX Then CodeERR = 114 : GoTo out1

        'Charges ponctuelles
        If NF > 0 Then
            For I = 1 To NF
                If XLF(I) >= (1 - eps10) * Ltot And XLF(I) <= (1 + eps10) * Ltot Then XLF(I) = Ltot
                If XLF(I) >= -eps10 * Ltot And XLF(I) <= eps10 * Ltot Then XLF(I) = 0
                If XLF(I) < -eps10 * Ltot Or XLF(I) > (1 + eps10) * Ltot Then CodeERR = 115 : GoTo out1
            Next I
        End If

        'Charges réparties
        If NQ > 0 Then
            For I = 1 To NQ
                If XLq1(I) >= -eps10 * Ltot And XLq1(I) <= eps10 * Ltot Then XLq1(I) = 0
                If XLq2(I) >= (1 - eps10) * Ltot And XLq2(I) <= (1 + eps10) * Ltot Then XLq2(I) = Ltot
                If XLq1(I) < -eps10 * Ltot Or XLq2(I) > (1 + eps10) * Ltot Then CodeERR = 116 : GoTo out1
                If (XLq2(I) - XLq1(I)) < Ltot / 100 Then CodeERR = 117 : GoTo out1
            Next I
        End If

        'Blocage des efforts
        '-------------------
        If FNBLOCKED And MYBLOCKED Then CodeERR = 119 : GoTo out1


        'Tout est OK
        '-----------
        CodeERR = 0

out1:

    End Sub


    '=====================================================================================================
    ' ROUTINE DE RE-ECRITURE DES DONNEES DANS UN FICHIER POUR CONTROLE (utilisé si XLONG(0)=-1)
    '=====================================================================================================
    Sub Save_FileLTB(ByRef FileData As String, ByRef NB As Short, ByRef E As Double, ByRef G As Double, ByRef XLONG() As Double, ByRef XIZ() As Double,
                     ByRef XIT() As Double, ByRef XIW() As Double, ByRef XBETAZ() As Double, ByRef XZC() As Double, ByRef XRYZ() As Double,
                     ByRef KT() As Double, ByRef KVP() As Double, ByRef KTP() As Double, ByRef TAPEREFFECT() As Boolean,
                     ByRef XIPSI() As Double, ByRef XIWPSI() As Double, ByRef XIPSIY() As Double,
                     ByRef XIZZX2() As Double, ByRef XIZ2ZX() As Double, ByRef XIY() As Double,
                     ByRef XIZX() As Double, ByRef XIZX2() As Double, ByRef XIZZX() As Double,
                     ByRef XAIRE() As Double, ByRef XAIREV() As Double,
                     ByRef ZN() As Double, ByRef RV() As Double, ByRef RTX() As Double,
                     ByRef RTZ() As Double, ByRef RTXX() As Double, ByRef ZR() As Double, ByRef XM1() As Double, ByRef XM2() As Double,
                     ByRef XFN() As Double, ByRef NF As Short, ByRef NQ As Short, ByRef FF() As Double, ByRef XLF() As Double, ByRef ZFF() As Double,
                     ByRef QQ1() As Double, ByRef XLq1() As Double, ByRef QQ2() As Double, ByRef XLq2() As Double, ByRef ZQQ() As Double)

        Dim I As Short
        Dim NN As Short
        Dim a As String
        Dim NFOUT As Short

        NFOUT = FreeFile()
        FileOpen(NFOUT, FileData, OpenMode.Output)

        'Commentaires
        '------------
        'UPGRADE_WARNING: propriété App.exename de App a un nouveau comportement. Cliquez ici : 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
        a = "Créé par '" & My.Application.Info.AssemblyName & "' pour contrôle des données"
        PrintLine(NFOUT, a)
        PrintLine(NFOUT, "Units : daN cm")

        'Paramètres généraux
        '-------------------
        PrintLine(NFOUT, "Nb Elements, E, G")
        PrintLine(NFOUT, NB, PFormat(CStr(E), 15), PFormat(CStr(G), 15))

        NN = NB + 1

        'Caractéristiques éléments
        '-------------------------
        Dim TAPER As String
        PrintLine(NFOUT, "Elements : n, L, Iz, It, Iw, Betaz, Kt, Kvp, Ktp, zc, ryz, Taper, Ipsi, Iwpsi, Iwz, Ipsiy, Izzx2, Iz2zx, Iy, Izx, Izx2, Izzx, Aire, AireV")
        For I = 1 To NB
            If TAPEREFFECT(I) Then TAPER = "    X    " Else TAPER = "    -    "
            PrintLine(NFOUT, FrmtPlage(I, 3, 0) & FrmtPlage(XLONG(I), 9, 3) & FrmtPlage(XIZ(I), 12, 3) & FrmtPlage(XIT(I), 12, 3) & FrmtPlage(XIW(I), 15, 1) & FrmtPlage(XBETAZ(I), 9, 3) _
            & FrmtPlage(KT(I), 15, 2) & FrmtPlage(KVP(I), 15, 2) & FrmtPlage(KTP(I), 15, 2) & FrmtPlage(XZC(I), 9, 3) & FrmtPlage(XRYZ(I), 9, 3) _
            & TAPER & FrmtPlage(XIPSI(I), 15, 3) & FrmtPlage(XIWPSI(I), 15, 3) & FrmtPlage(XIPSIY(I), 15, 3) & FrmtPlage(XIZZX2(I), 15, 3) & FrmtPlage(XIZ2ZX(I), 15, 3) _
            & FrmtPlage(XIY(I), 15, 3) & FrmtPlage(XIZX(I), 15, 3) & FrmtPlage(XIZX2(I), 15, 3) & FrmtPlage(XIZZX(I), 15, 3) _
            & FrmtPlage(XAIRE(I), 15, 3) & FrmtPlage(XAIREV(I), 15, 3))
        Next I

        'Maintiens latéraux
        '------------------
        PrintLine(NFOUT, "Nodes : n, Rv, Rt, Rvp, Rtp, zr, zn")
        For I = 1 To NN
            PrintLine(NFOUT, FrmtPlage(I, 3, 0) & FrmtPlage(RV(I), 15, 2) & FrmtPlage(RTX(I), 15, 2) & FrmtPlage(RTZ(I), 15, 2) & FrmtPlage(RTXX(I), 15, 2) & FrmtPlage(ZR(I), 11, 3) & FrmtPlage(ZN(I), 11, 3))
        Next I

        'Efforts dans éléments
        '---------------------
        PrintLine(NFOUT, "Elements : n, M1, M2, N(-:compression)") ', V1, V2"
        For I = 1 To NB
            PrintLine(NFOUT, FrmtPlage(I, 3, 0) & FrmtPlage(XM1(I), 14, 1) & FrmtPlage(XM2(I), 14, 1) & FrmtPlage(XFN(I), 14, 2))
        Next I

        'Charges sur poutre
        '------------------
        PrintLine(NFOUT, "Member Loads")
        PrintLine(NFOUT, "NF NQ")
        PrintLine(NFOUT, NF, NQ)

        'Charges ponctuelles
        If NF > 0 Then
            PrintLine(NFOUT, "F, x, z")
            For I = 1 To NF
                PrintLine(NFOUT, FrmtPlage(FF(I), 12, 1) & FrmtPlage(XLF(I), 10, 2) & FrmtPlage(ZFF(I), 10, 2))
            Next I
        End If

        'Charges réparties
        If NQ > 0 Then
            PrintLine(NFOUT, "q1, x1, q2, x2, z")
            For I = 1 To NQ
                PrintLine(NFOUT, FrmtPlage(QQ1(I), 12, 3) & FrmtPlage(XLq1(I), 10, 2) & FrmtPlage(QQ2(I), 12, 3) & FrmtPlage(XLq2(I), 10, 2) & FrmtPlage(ZQQ(I), 10, 2))
            Next I
        End If

        FileClose((NFOUT))

    End Sub

    '=====================================================================================================
    ' ROUTINE DE FORMATAGE D'UN NOMBRE A N DECIMALES CADRE A DROITE
    '=====================================================================================================

    Function FrmtPlage(ByRef x As Object, ByRef plage As Short, ByRef decimales As Short) As String

        'Formatage d'un nombre avec "decimales" décimales, cadré à droite d'une plage de "plage" espaces

        Dim a As String
        Dim G As String
        Dim P As Short
        Dim lenH As Short
        Dim H As String
        Dim F As String

        If decimales > 0 Then
            F = "0." & New String("0", decimales)
        Else
            F = "0"
        End If
        H = Microsoft.VisualBasic.Format(x, F)
        lenH = Len(H)
        P = plage
        If lenH > P Then
            H = Microsoft.VisualBasic.Format(x, "0.000#E+00")
            If Len(H) > P Then P = Len(H)
        End If
        G = New String("@", P)
        a = Microsoft.VisualBasic.Format(H, G)
        FrmtPlage = PFormat(a, Len(a))

    End Function

    '=====================================================================================================
    ' ROUTINE DEFINISSANT UN FORMAT DE NOMBRE
    '=====================================================================================================

    Function PFormat(ByRef x As String, ByRef N As Short) As String

        Dim J As Short
        Dim SeparateurDecimal As String

        'x = valeur numérique sous forme de texte
        'transforme le séparateur décimal courant éventuelle en "."
        'chaîne finale calée à droite sur n blancs

        'Séparateur décimal actuel
        SeparateurDecimal = Mid(Microsoft.VisualBasic.Format(2.2, "0.0"), 2, 1)

        J = InStr(x, SeparateurDecimal)
        If J > 0 Then
            PFormat = Left(x, J - 1) & "." & Mid(x, J + 1)
        Else
            PFormat = x
        End If

        PFormat = Microsoft.VisualBasic.Format(PFormat, New String("@", N))

    End Function

End Module
