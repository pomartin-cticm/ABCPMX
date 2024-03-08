Option Strict Off
Option Explicit On

Module ModNET_LTBN

    Sub LTBNSolve(ByVal MATERIAU As LTB_MATERIAU, ByVal BARRES As LTB_BARRES, ByVal NOEUDS As LTB_NOEUDS,
                  ByVal SOLLICITATIONS As LTB_SOLLICITATIONS, ByVal CHARGESEXCENTREES As LTB_CHARGESEXCENTREES,
                  ByVal RESOLUTION As LTB_RESOLUTION, ByRef RESULTATS As LTB_RESULTATS)

        Dim CodeERR As Integer
        Dim NB As Integer
        Dim E, G As Double
        Dim XLONG(), XIZ(), XIT(), XIW(), XBETAZ(), XZCP(), XZCS(), XZC(), XRYZ(), KT(), KVP(), KTP() As Double
        Dim TAPEREFFECT() As Boolean
        Dim XIPSI(), XIWPSI(), XIPSIY(), XIZZX2(), XIZ2ZX(), XIY(), XIZX(), XIZX2(), XIZZX(), XAIRE(), XAIREV() As Double
        Dim ZN(), RV(), RTX(), RTZ(), RTXX(), ZR() As Double
        Dim XM1(), XM2(), XFN() As Double
        Dim NF, NQ As Integer
        Dim FF(), XLF(), ZFF(), QQ1(), XLq1(), QQ2(), XLq2(), ZQQ() As Double
        Dim FNBLOCKED, MYBLOCKED As Boolean
        Dim tolerance As Double, NoVectP As Boolean, DUMP As Integer
        Dim Txt_Recepteur As Object = Nothing
        Dim Txt_Progress As Object = Nothing
        Dim MomentMax, xMomentMax, NMax, xNMax As Double
        Dim VP() As Double, Vector(,) As Double, NBVP As Integer

        Dim SA() As Double
        Dim SB() As Double
        Dim STEMP() As Double
        Dim RA() As Double
        Dim RB() As Double
        Dim VectP(,) As Double

        Dim KRES(,) As Double

        Dim XV1() As Double
        Dim XV2() As Double
        Dim F(,) As Double
        Dim XF(,) As Double
        Dim ZF(,) As Double
        Dim q1(,) As Double
        Dim Xq1(,) As Double
        Dim q2(,) As Double
        Dim Xq2(,) As Double
        Dim Zq(,) As Double

        Dim Fe() As Double
        Dim XFe() As Double
        Dim ZFe() As Double
        Dim q1e() As Double
        Dim Xq1e() As Double
        Dim q2e() As Double
        Dim Xq2e() As Double
        Dim Zqe() As Double
        Dim GAMMA() As Double

        'dimensions tableaux internes
        '----------------------------
        NB = BARRES.NBELEM
        NF = CHARGESEXCENTREES.NFZ
        NQ = CHARGESEXCENTREES.NQZ
        NBVP = RESOLUTION.NBVALP
        ReDim XLONG(NB), XIZ(NB), XIT(NB), XIW(NB), XBETAZ(NB), XZC(NB), XRYZ(NB), KT(NB), KVP(NB), KTP(NB)
        ReDim TAPEREFFECT(NB)
        ReDim XIPSI(NB), XIWPSI(NB), XIPSIY(NB), XIZZX2(NB), XIZ2ZX(NB), XIY(NB), XIZX(NB), XIZX2(NB), XIZZX(NB), XAIRE(NB), XAIREV(NB)
        ReDim ZN(NB + 1), RV(NB + 1), RTX(NB + 1), RTZ(NB + 1), RTXX(NB + 1), ZR(NB + 1)
        ReDim XM1(NB), XM2(NB), XFN(NB)
        ReDim FF(NF), XLF(NF), ZFF(NF), QQ1(NQ), XLq1(NQ), QQ2(NQ), XLq2(NQ), ZQQ(NQ)
        ReDim VP(NBVP), Vector(NBVP, 4 * NB + 4)

        'Transfert vers variables internes
        '---------------------------------
        E = MATERIAU.E
        G = MATERIAU.G

        XLONG = BARRES.LONGPROJ.Clone
        XIZ = BARRES.IZ.Clone
        XIT = BARRES.IT.Clone
        XIW = BARRES.IW.Clone
        XBETAZ = BARRES.BETAZ.Clone
        XZC = BARRES.ZCG.Clone
        XRYZ = BARRES.RYZ.Clone
        KT = BARRES.KT1.Clone
        KVP = BARRES.KVP1.Clone
        KTP = BARRES.KTP1.Clone
        ZN = NOEUDS.ZN.Clone
        RV = NOEUDS.RV.Clone
        RTX = NOEUDS.RT.Clone
        RTZ = NOEUDS.RVP.Clone
        RTXX = NOEUDS.RTP.Clone
        ZR = NOEUDS.ZRC.Clone
        XM1 = SOLLICITATIONS.MY1.Clone
        XM2 = SOLLICITATIONS.MY2.Clone
        XFN = SOLLICITATIONS.FN.Clone
        FNBLOCKED = SOLLICITATIONS.FNBLOCKED
        MYBLOCKED = SOLLICITATIONS.MYBLOCKED
        XLF = CHARGESEXCENTREES.XLFZ.Clone
        FF = CHARGESEXCENTREES.FZ.Clone
        ZFF = CHARGESEXCENTREES.ZFZC.Clone
        QQ1 = CHARGESEXCENTREES.QZ1.Clone
        QQ2 = CHARGESEXCENTREES.QZ2.Clone
        XLq1 = CHARGESEXCENTREES.XLQZ1.Clone
        XLq2 = CHARGESEXCENTREES.XLQZ2.Clone
        ZQQ = CHARGESEXCENTREES.ZQZC.Clone
        tolerance = RESOLUTION.TOLERANCE
        NoVectP = RESOLUTION.NOVECTP
        'Txt_Recepteur = RESOLUTION.TXT_RECEPTEUR
        'Txt_Progress = RESOLUTION.TXT_PROGRESS
        DUMP = RESOLUTION.DUMP



        Dim BackFileData As String

        Dim IDIM As Integer
        Dim NDIM As Integer
        Dim NN As Integer
        Dim client As Integer
        Dim test_TAG, test_DLLname, test_EXEname As Boolean

        Dim J, I, K As Integer
        Dim found, found1 As Boolean
        Dim Ltot, dqJ As Double
        Dim ixfmax As Integer

        ' NO TAPER EFFECT
        '-----------------
        For J = 1 To NB
            TAPEREFFECT(J) = False
        Next J

        'Paramètres généraux
        '-------------------
        NN = NB + 1
        IDIM = 4 * NN
        NDIM = CInt(IDIM) * (IDIM + 1) / 2

        'limites de NBVP
        If NBVP < 1 Then NBVP = 1
        If NBVP > IDIM Then NBVP = IDIM
        ReDim VP(NBVP)
        For I = 1 To NBVP
            VP(I) = 0
        Next I

        On Error GoTo PbProtec

        '=============================================================================================
        'PROTECTION
        '=============================================================================================
        client = 1000 'on saute les test de protection
        'client = Int(25 ^ 0.5 - 16 ^ 0.5 + 0.01) 'PMX
        'client = Int(36 ^ 0.5 - 16 ^ 0.5 + 0.01) 'HERGOS
        'client = Int(49 ^ 0.5 - 16 ^ 0.5 + 0.01) 'VB

        Call Protec_DLL(client, Txt_Recepteur, VP(1), CodeERR)

        If CodeERR <> 0 Then GoTo PbProtec

        '=========================================================================================================

        On Error GoTo PbData
        RESULTATS.TEXTERROR = String.Empty

        '=============================================================================================
        'CONTROLE DES DONNEES
        '=============================================================================================

        'Ré-écriture du fichier de données pour contrôle
        '-----------------------------------------------
        CodeERR = 1

        'XLONG(0) = -1 'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        If XLONG(0) = -1 Then
            BackFileData = My.Application.Info.DirectoryPath & "\DataBackup.LTB"
            Call Save_FileLTB(BackFileData, NB, E, G, XLONG, XIZ, XIT, XIW, XBETAZ, XZC, XRYZ, KT, KVP, KTP,
                              TAPEREFFECT, XIPSI, XIWPSI, XIPSIY, XIZZX2, XIZ2ZX, XIY, XIZX, XIZX2, XIZZX,
                              XAIRE, XAIREV, ZN, RV, RTX, RTZ, RTXX, ZR,
                              XM1, XM2, XFN, NF, NQ, FF, XLF, ZFF, QQ1, XLq1, QQ2, XLq2, ZQQ)
        End If

        CodeERR = 2

        'Vérification des données
        '------------------------
        Call Check_Data(NB, E, G, XLONG, XIZ, XIT, XIW, XBETAZ, XZC, XRYZ, KT, KVP, KTP, ZN, RV, RTX,
                        RTZ, RTXX, ZR, XM1, XM2, XFN, NF, NQ, FF, XLF, ZFF, QQ1, XLq1, QQ2, XLq2, ZQQ,
                        FNBLOCKED, MYBLOCKED, MomentMax, xMomentMax, NMax, xNMax, CodeERR)

        If CodeERR <> 0 Then GoTo PbData

        RESULTATS.MOMENTMAX = MomentMax
        RESULTATS.XMOMENTMAX = xMomentMax
        RESULTATS.NMAX = NMax
        RESULTATS.XNMAX = xNMax
        '---------------------------------------------------------------------------------------------

        CodeERR = 3

        'Dimensionnement des tableaux
        '----------------------------
        ReDim SA(36)
        ReDim SB(36)
        ReDim STEMP(36)
        ReDim RA(NDIM)
        ReDim RB(NDIM)
        ReDim XV1(NB)
        ReDim XV2(NB)
        ReDim VP(NBVP)
        ReDim VectP(NBVP, IDIM)
        ReDim KRES(NB, 8)
        Dim KRESI(8) As Double
        ReDim GAMMA(NB) 'angle d'un élément dans repère global
        ReDim XZCP(NB) ' dérivée première de zC de l'élément dans son repère local
        ReDim XZCS(NB) ' dérivée seconde de zC de l'élément dans son repère local

        'initialisation des valeurs et vecteurs propres
        '----------------------------------------------
        For J = 1 To NBVP
            VP(J) = 0
            For I = 1 To IDIM
                Vector(J, I) = 0
            Next I
        Next J

        CodeERR = 4

        'Caractéristiques éléments
        '-------------------------
        Ltot = 0
        For I = 1 To NB

            'Traitement ressorts des éléments
            For K = 1 To 8 : KRES(I, K) = Stiffness(-1) : Next K
            KRES(I, 2) = Stiffness(KT(I))
            KRES(I, 3) = Stiffness(KVP(I))
            KRES(I, 4) = Stiffness(KTP(I))

            'Angle d'inclinaison des éléments
            GAMMA(I) = System.Math.Atan((ZN(I + 1) - ZN(I)) / XLONG(I))

            'Dérivée première de zC des éléments (décalage à droite)
            If I < NB Then
                XZCP(I) = (XZC(I + 1) - XZC(I)) / (XLONG(I) / Math.Cos(GAMMA(I)))
            Else
                XZCP(I) = XZCP(I - 1)
            End If


            'longueur totale projetée de la barre
            Ltot = Ltot + XLONG(I)

        Next I

        'Dérivée seconde de zC des éléments (décalage à gauche)
        For I = 1 To NB
            If I > 1 Then
                XZCS(I) = (XZCP(I) - XZCP(I - 1)) / (XLONG(I) / Math.Cos(GAMMA(I)))
            Else
                XZCS(I) = (XZCP(I + 1) - XZCP(I)) / (XLONG(I) / Math.Cos(GAMMA(I)))
            End If
        Next I


        On Error GoTo 0

        On Error GoTo PbSolving

        CodeERR = 21

        'TRAITEMENT DES CHARGES SUR POUTRE (inclinaison éventuelle des éléments non considérée)
        '=================================
        ReDim F(NB, NF)
        ReDim XF(NB, NF)
        ReDim ZF(NB, NF)
        ReDim q1(NB, NQ)
        ReDim Xq1(NB, NQ)
        ReDim q2(NB, NQ)
        ReDim Xq2(NB, NQ)
        ReDim Zq(NB, NQ)
        Dim XL As Double


        'Charges ponctuelles
        '-------------------
        If NF > 0 Then
            For I = 1 To NF
                XL = 0
                For J = 1 To NB
                    If XL + XLONG(J) >= 0.9999 * XLF(I) Then
                        XF(J, I) = (XLF(I) - XL) / XLONG(J)
                        F(J, I) = FF(I)  'x Cos(GAMMA(J)) ?????
                        ZF(J, I) = ZFF(I)
                        Exit For
                    Else
                        XL = XL + XLONG(J)
                    End If
                Next J
            Next I
        End If

        CodeERR = 22

        'Charges réparties
        '-----------------
        If NQ > 0 Then
            For I = 1 To NQ 'boucle sur les NQ charges réparties
                XL = 0
                found1 = False
                For J = 1 To NB 'boucle sur les NB éléments
                    'Xq1   début de q
                    If XL + XLONG(J) > XLq1(I) Then 'l'extrémité de l'élément à une abscisse > XLq1 --> q commence sur cet élément
                        If found1 = True Then 'le début de q a été établit dans un élément précédent
                            'on assure la continuité - on établit la valeur de q à l'origine de l'élément (= fin de l'élément précédent)
                            q1(J, I) = q2(J - 1, I)
                            Xq1(J, I) = 0
                            Zq(J, I) = ZQQ(I)
                        Else 'on fixe le début de q dans l'élément
                            Xq1(J, I) = (XLq1(I) - XL) / XLONG(J)
                            q1(J, I) = QQ1(I)
                            Zq(J, I) = ZQQ(I)
                            found1 = True 'le début de q a été établit (sert si q s'étend sur plusieurs éléments)
                        End If
                        'Xq2    extrémité de q
                        If XL + XLONG(J) > XLq2(I) Then 'l'extrémité de l'élément à une abscisse > XLq2 --> q finit sur cet élément
                            'on fixe la fin de q dans cet élément
                            Xq2(J, I) = (XLq2(I) - XL) / XLONG(J)
                            q2(J, I) = QQ2(I)
                            Zq(J, I) = ZQQ(I)
                            Exit For 'fini pour cette q
                        Else 'q va au-delà de l'extrémité de cet élément
                            'on assure la continuité - on établit la valeur de q à l'extrémité de l'élément
                            Xq2(J, I) = 1
                            dqJ = (QQ2(I) - QQ1(I)) / (XLq2(I) - XLq1(I)) * XLONG(J)
                            q2(J, I) = q1(J, I) + dqJ * (1 - Xq1(J, I))
                            Zq(J, I) = ZQQ(I)
                        End If
                    End If
                    'on passe à l'abscisse d'extrémité de l'élément suivant
                    XL = XL + XLONG(J)
                Next J 'élément suivant
            Next I 'charge q suivante
        End If

        'CALCUL DES MATRICES RA(LINEAIRE) ET RB(GEOMETRIQUE) (sans supports)
        '===================================================================

        'Initialisation
        '--------------
        Dim II As Integer
        For II = 1 To NDIM
            RA(II) = 0
            RB(II) = 0
        Next II

        'Construction des matrices
        '-------------------------
        Dim nfe, nqe As Short
        Dim XLI As Double
        For I = 1 To NB

            CodeERR = 30

            'longueur réelle de l'élément
            XLI = XLONG(I) / System.Math.Cos(GAMMA(I))

            'Paramètres dus aux charges transversales
            '----------------------------------------
            'XV1 et XV2 plus utilisés car on prend V=-dM/dx
            'les signes '-' devant les efforts sont dus au fait qu'on résoud ici (RA-VP*RB)et non (RA+VP*RB)
            'on traite les charges multiples sur un élément
            ReDim Fe(NF)
            ReDim XFe(NF)
            ReDim ZFe(NF)
            ReDim q1e(NQ)
            ReDim Xq1e(NQ)
            ReDim q2e(NQ)
            ReDim Xq2e(NQ)
            ReDim Zqe(NQ)
            Dim XXFN As Double

            For nfe = 1 To NF
                Fe(nfe) = F(I, nfe)
                XFe(nfe) = XF(I, nfe)
                ZFe(nfe) = ZF(I, nfe)
            Next nfe
            For nqe = 1 To NQ
                q1e(nqe) = q1(I, nqe)
                Xq1e(nqe) = Xq1(I, nqe)
                q2e(nqe) = q2(I, nqe)
                Xq2e(nqe) = Xq2(I, nqe)
                Zqe(nqe) = Zq(I, nqe)
            Next nqe

            CodeERR = 31

            'Matrice Linéaire + Assemblage
            '-----------------------------
            For K = 1 To 8
                KRESI(K) = KRES(I, K)
            Next K

            'Construction de la matrice linéaire
            Call MatSA(E, G, XLI, XZC(I), XZCP(I), XZCS(I), XIZ(I), XIT(I), XIW(I), TAPEREFFECT(I), XIPSI(I), XIWPSI(I),
                      XIPSIY(I), XIZZX2(I), XIZ2ZX(I), XIY(I), XIZX(I), XIZX2(I), XIZZX(I), KRESI, SA)

            'Traitement de l'effort axial bloqué
            If FNBLOCKED Then
                Call MatSB(XLI, XBETAZ(I), XZC(I), XZCP(I), 0, 0, 0, 0, 0, 0, Fe, XFe, ZFe, q1e, Xq1e, q2e, Xq2e, Zqe,
                           -XFN(I), XRYZ(I), XIZX(I), XIZX2(I), XIZZX(I), XIZ2ZX(I), XIZZX2(I), XIY(I), XAIRE(I), XAIREV(I), TAPEREFFECT(I), STEMP)
                For J = 1 To 36
                    SA(J) = SA(J) - STEMP(J)
                Next J
            End If

            'Traitement du moment bloqué
            If MYBLOCKED Then
                Call MatSB(XLI, XBETAZ(I), XZC(I), XZCP(I), -XM1(I), -XM2(I), -XV1(I), -XV2(I), NF, NQ, Fe, XFe, ZFe, q1e, Xq1e, q2e, Xq2e, Zqe,
                           0, XRYZ(I), XIZX(I), XIZX2(I), XIZZX(I), XIZ2ZX(I), XIZZX2(I), XIY(I), XAIRE(I), XAIREV(I), TAPEREFFECT(I), STEMP)
                For J = 1 To 36
                    SA(J) = SA(J) - STEMP(J)
                Next J
            End If

            'Passage en repère global
            Call ROTAX(GAMMA(I), XLI, SA)

            'Assemblage dans la matrice globale
            Call AsBar(I, SA, RA)

            CodeERR = 32

            'Matrice Géométrique + Assemblage
            '--------------------------------
            If FNBLOCKED Then
                Call MatSB(XLI, XBETAZ(I), XZC(I), XZCP(I), -XM1(I), -XM2(I), -XV1(I), -XV2(I), NF, NQ, Fe, XFe, ZFe, q1e, Xq1e, q2e, Xq2e, Zqe,
                           0, XRYZ(I), XIZX(I), XIZX2(I), XIZZX(I), XIZ2ZX(I), XIZZX2(I), XIY(I), XAIRE(I), XAIREV(I), TAPEREFFECT(I), SB)
            ElseIf MYBLOCKED Then
                Call MatSB(XLI, XBETAZ(I), XZC(I), XZCP(I), 0, 0, 0, 0, 0, 0, Fe, XFe, ZFe, q1e, Xq1e, q2e, Xq2e, Zqe,
                           -XFN(I), XRYZ(I), XIZX(I), XIZX2(I), XIZZX(I), XIZ2ZX(I), XIZZX2(I), XIY(I), XAIRE(I), XAIREV(I), TAPEREFFECT(I), SB)
            Else

                Call MatSB(XLI, XBETAZ(I), XZC(I), XZCP(I), -XM1(I), -XM2(I), -XV1(I), -XV2(I), NF, NQ, Fe, XFe, ZFe, q1e, Xq1e, q2e, Xq2e, Zqe,
                           -XFN(I), XRYZ(I), XIZX(I), XIZX2(I), XIZZX(I), XIZ2ZX(I), XIZZX2(I), XIY(I), XAIRE(I), XAIREV(I), TAPEREFFECT(I), SB)
            End If

            'Passage en repère global
            Call ROTAX(GAMMA(I), XLI, SB)

            'Assemblage dans la matrice globale
            Call AsBar(I, SB, RB)

        Next I

        'TRAITEMENT DES MAINTIENS LATERAUX
        '=================================
        'Shifts z des maintiens excentrés
        '--------------------------------
        CodeERR = 41
        For I = 1 To NN
            If ZR(I) <> 0 Then
                Call ShiftZAtSupport(NN, I, RA, RB, ZR(I))
            End If
        Next I

        'Introduction des Ressorts des maintiens dans les matrices
        '---------------------------------------------------------
        CodeERR = 42
        Dim I3, I1, I2, I4 As Integer
        For I = 1 To NN
            I1 = 4 * I - 3
            I2 = 4 * I - 2
            I3 = 4 * I - 1
            I4 = 4 * I
            RA(I1 * (I1 + 1) / 2) = RA(I1 * (I1 + 1) / 2) + Stiffness(RV(I))
            RA(I2 * (I2 + 1) / 2) = RA(I2 * (I2 + 1) / 2) + Stiffness(RTX(I))
            RA(I3 * (I3 + 1) / 2) = RA(I3 * (I3 + 1) / 2) + Stiffness(RTZ(I))
            RA(I4 * (I4 + 1) / 2) = RA(I4 * (I4 + 1) / 2) + Stiffness(RTXX(I))
        Next I

        'CALCUL DE MCR
        '=============
        CodeERR = 5
        '------------------------------------- L A P A C K -----------------------------------------------
        Call ResolVP(RA, RB, IDIM, tolerance, NoVectP, Txt_Recepteur, Txt_Progress, VP, VectP, NBVP, DUMP, CodeERR)
        '-------------------------------------------------------------------------------------------------

        If VP(1) = 0 Then  '1ère VP nulle --> Problème --> on sort avec le CodeERR
            Exit Sub
        Else
            CodeERR = 0 'OK
            RESULTATS.TEXTERROR = String.Empty
        End If

        If NoVectP Then Exit Sub 'Pas de vecteurs propres calculés

        CodeERR = 61

        'Traitement inverse des Shifts z aux supports latéraux
        '-----------------------------------------------------
        Dim TetaPSupport, VShear, VSupport, TetaSupport, VPSupport, VPShear As Double

        For J = 1 To NBVP
            For I = 1 To NN
                If ZR(I) <> 0 Then
                    VSupport = VectP(J, 4 * (I - 1) + 1) 'v
                    TetaSupport = VectP(J, 4 * (I - 1) + 2) 't
                    VShear = VSupport + TetaSupport * ZR(I)
                    VectP(J, 4 * (I - 1) + 1) = VShear
                    VPSupport = VectP(J, 4 * (I - 1) + 3) 'v'
                    TetaPSupport = VectP(J, 4 * (I - 1) + 4) 't'
                    VPShear = VPSupport + TetaPSupport * ZR(I)
                    VectP(J, 4 * (I - 1) + 3) = VPShear
                End If
            Next I
        Next J

        CodeERR = 62


        'NORMALISATION DU VECTEUR PROPRE
        '-------------------------------
        Dim VECMAX, ABSVEC As Double
        For J = 1 To NBVP
            VECMAX = 0
            For I = 1 To IDIM
                ABSVEC = System.Math.Abs(VectP(J, I))
                If ABSVEC >= VECMAX Then
                    VECMAX = ABSVEC
                End If
            Next I
            If VECMAX <> 0 Then
                For I = 1 To IDIM
                    Vector(J, I) = VectP(J, I) / VECMAX
                Next I
            End If
        Next J

        ReDim RESULTATS.VALP(NBVP)
        ReDim RESULTATS.VECTP(NBVP, IDIM)
        For J = 1 To NBVP
            RESULTATS.VALP(J) = VP(J)
            For I = 1 To IDIM
                RESULTATS.VECTP(J, I) = Vector(J, I)
            Next I
        Next J

        CodeERR = 0
        RESULTATS.CODEERROR = CodeERR

        Exit Sub
        '-----------------------------------------------------------------------
PbProtec:
        '   CodeERR défini dans le module
        RESULTATS.TEXTERROR = "LTBNSolver - Problem in authorization check"
        RESULTATS.CODEERROR = CodeERR
        Exit Sub
        '-----------------------------------------------------------------------
PbData:
        '   CodeERR défini dans le module
        RESULTATS.TEXTERROR = "LTBNSolver - Problem in reading data" + Chr(10) + Err.Description
        RESULTATS.CODEERROR = CodeERR
        Exit Sub
        '-----------------------------------------------------------------------
PbSolving:
        '   CodeERR défini dans le module
        RESULTATS.TEXTERROR = "LTBNSolver - Problem in solving Mu_cr" + Chr(10) + Err.Description
        RESULTATS.CODEERROR = CodeERR
        Exit Sub

    End Sub

End Module
