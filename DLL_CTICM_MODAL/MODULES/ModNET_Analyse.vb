Option Strict Off
Option Explicit On

Module ModNET_Analyse

    'Code Erreur
    Friend CodeERR As Short
    Friend TextERR As String

    '================================================================================================
    'Déclarations pour le Module Analyse
    '================================================================================================

    Sub ANALYSE(ByRef MAT As MATERIAU, ByRef NOEUDS As STR_NOEUDS, ByRef BARRES As STR_BARRES,
                ByRef MASSES As MAS_BARRES, ByVal RESOLUTION As MOD_RESOLUTION, ByRef RESULTATS As MOD_RESULTATS, lConsole As Boolean)
        '==============================================================================================================================================================
        '
        '   R O U T I N E    D ' A N A L Y S E      M O D A L E    D E    S T R U C T U R E S     2 D
        '
        '==============================================================================================================================================================
        '
        '==============================================================================================================================================================

        'Déclarations
        '------------

        'Géométrie du bâtiment
        '--------------------------------

        'MATERIAU
        Dim E As Double

        'STR_NOEUDS
        Dim NNT As Integer
        Dim X() As Double
        Dim Y() As Double
        Dim RESN(,) As Double

        'STR_BARRES
        Dim NBT As Integer
        Dim JEXB(,) As Integer
        Dim SECT() As Double
        Dim XIN() As Double
        Dim RESB(,) As Double

        'MAS_BARRES
        Dim XMC() As Double
        Dim MC() As Double
        Dim XMR1() As Double
        Dim XMR2() As Double
        Dim MR1() As Double
        Dim MR2() As Double

        Dim XLONG() As Double
        Dim CX() As Double
        Dim CY() As Double

        'Matrices globales
        Dim SK() As Double
        Dim SM() As Double

        Dim NBVP As Integer
        Dim VALP() As Double
        Dim VECTP(,) As Double

        Const RIGID = 1.0E+24

        Dim IDIM, NDIM As Integer
        Dim NMCMAX, NMRMAX As Integer

        Dim NCAS As Integer
        Dim IERREUR As Integer
        Dim DELTA() As Double
        Dim DM, DMD, DMDELTA As Double
        Dim MASSE_TOTALE As Double
        Dim DEPT() As Double
        Dim DEPL() As Double
        Dim MD(3) As Double

        On Error GoTo PbProtec

        Dim I, J As Integer

        '=============================================================================================
        'PROTECTION
        '=============================================================================================
        Dim client As Short ' = 1000 'on saute les test de protection        
        client = 1

        Call Protect_DLL(client, RESOLUTION.TXT_RECEPTEUR, CodeERR, TextERR)

        If CodeERR <> 0 Then GoTo PbProtec

        On Error GoTo PbSolving

        TextERR = String.Empty
        CodeERR = 1


        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        '
        '             TRANSFERT DES DONNEES DES STRUCTURES VERS DES VARIABLES INTERNES
        '
        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        'Infos Matériau
        '--------------
        E = MAT.E

        'Infos Noeuds
        '------------
        NNT = NOEUDS.NNT
        ReDim X(NNT), Y(NNT), RESN(3, NNT)
        For I = 1 To NNT
            X(I) = NOEUDS.X(I)
            Y(I) = NOEUDS.Y(I)
            For J = 1 To 3
                RESN(J, I) = NOEUDS.RESN(J, I)
                If RESN(J, I) = -1 Then RESN(J, I) = RIGID
            Next J
        Next I

        CodeERR = 2

        'Infos Barres
        '------------
        NBT = BARRES.NBT
        ReDim JEXB(2, NBT), SECT(NBT), XIN(NBT), RESB(2, NBT)
        For I = 1 To NBT
            JEXB(1, I) = BARRES.JEXB(1, I)
            JEXB(2, I) = BARRES.JEXB(2, I)
            SECT(I) = BARRES.SECT(I)
            XIN(I) = BARRES.XIN(I)
            RESB(1, I) = BARRES.RESB(1, I)
            If RESB(1, I) = -1 Then RESB(1, I) = RIGID
            RESB(2, I) = BARRES.RESB(2, I)
            If RESB(2, I) = -1 Then RESB(2, I) = RIGID
        Next I

        CodeERR = 3

        'Nombre de cas
        NCAS = RESOLUTION.NCAS

        'Dimensions maxi
        '---------------        
        NMCMAX = 0
        NMRMAX = 0
        For ICAS = 1 To NCAS
            For I = 1 To NBT
                If MASSES.NMC(ICAS, I) > NMCMAX Then NMCMAX = MASSES.NMC(ICAS, I)
                If MASSES.NMR(ICAS, I) > NMRMAX Then NMRMAX = MASSES.NMR(ICAS, I)
            Next I
        Next

        CodeERR = 4

        IDIM = 3 * NNT

        'Dimensionnement de tableaux
        '---------------------------------------------------------        

        'NDDL = 6
        'DIM = 6 * (6 + 1) / 2 = 21

        Dim SKL(21) As Double     'Matrice de rigidité locale
        Dim SKG(21) As Double     'Matrice de rigidité dans le repère global
        Dim SML(21) As Double     'Matrice de masse locale
        Dim SMG(21) As Double     'Matrice de masse dans le repère global

        ReDim XLONG(NBT)            'Longueur de la barre
        ReDim CX(NBT)               'Cos de la barre
        ReDim CY(NBT)               'Sin de la barre

        'Longueurs et cosinus directeurs des barres
        '------------------------------------------
        For I = 1 To NBT
            XLONG(I) = Math.Sqrt((X(JEXB(2, I)) - X(JEXB(1, I))) ^ 2 + (Y(JEXB(2, I)) - Y(JEXB(1, I))) ^ 2)

            CX(I) = (X(JEXB(2, I)) - X(JEXB(1, I))) / XLONG(I)  '= Cos(Alpha)
            CY(I) = (Y(JEXB(2, I)) - Y(JEXB(1, I))) / XLONG(I)  '= Sin(Alpha)
        Next I

        CodeERR = 5

        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        '
        '                          MATRICE DE RIGIDITE DE LA STRUCTURE
        '
        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        'CALCULS RELATIFS AU STOCKAGE DE LA MATRICE DE RIGIDITE
        '-------------------------------------------------------
        'Call STOCKAGE(NNT, NBT, JEXB, NCOL, NDIM, MAXA)

        'NDIM = Dimension des matrices M, K de la structure
        IDIM = 3 * NNT
        NDIM = IDIM * (IDIM + 1) / 2

        ReDim SK(NDIM)

        'limites de NBVP
        NBVP = RESOLUTION.NBVALP
        If NBVP < 1 Then NBVP = 1
        If NBVP > IDIM Then NBVP = IDIM
        ReDim RESULTATS.VALP(NCAS, NBVP)
        ReDim RESULTATS.MAS_TOT(NCAS)
        ReDim RESULTATS.VECTP(NCAS, NBVP, IDIM)
        ReDim RESULTATS.MAS_MOD(NCAS, NBVP)
        ReDim RESULTATS.MAS_GEN(NCAS, NBVP)

        'TRAITEMENT DES SUPPORTS
        '-----------------------
        Call SUPPORTS(NNT, RESN, SK)
        'Call SUPPORTS(NNT, MAXA, RESN, SK)

        CodeERR = 6

        'BOUCLE SUR LES BARRES
        '---------------------
        For IB = 1 To NBT

            'MATRICE DE RIGIDITE DE LA BARRE DANS LE REPERE LOCAL  (ELASTIQUE LINEAIRE)
            '--------------------------------------------------------------            
            Call MatK(E, SECT(IB), XIN(IB), XLONG(IB), RESB(1, IB), RESB(2, IB), SKL, IERREUR)
            If IERREUR <> 0 Then
                CodeERR = 7   'barre instable                
                GoTo PbSolving
            End If

            'MATRICE DE RIGIDITE DE LA BARRE DANS LE REPERE GLOBAL
            '-----------------------------------------
            Call ROTAX(CX(IB), CY(IB), SKL, SKG)

            'ASSEMBLAGE DANS LA MATRICE DE RIGIDITE GLOBALE DE LA STRUCTURE
            '--------------------------------------------------
            Call ASSEMB(JEXB(1, IB), JEXB(2, IB), SKG, SK)

            CodeERR = 8
        Next IB

        'CONTRUCTION DU VECTEUR SOLLICITATION
        '------------------------------------        
        ReDim DELTA(IDIM)
        For I = 1 To NNT
            DELTA((I - 1) * 3 + 2) = 1
        Next


        'BOUCLE SUR LES CAS DE CHARGES
        '---------------------

        For ICAS = 1 To NCAS
            MASSE_TOTALE = 0

            ReDim SM(NDIM)

            For IB = 1 To NBT

                'MATRICE DE MASSE DE LA BARRE DANS LE REPERE LOCAL  (ELASTIQUE LINEAIRE)
                '--------------------------------------------------------------                        
                ReDim XMC(MASSES.NMC(ICAS, IB))
                ReDim MC(MASSES.NMC(ICAS, IB))
                For I = 1 To MASSES.NMC(ICAS, IB)
                    XMC(I) = MASSES.XMC(ICAS, I, IB)
                    MC(I) = MASSES.MC(ICAS, I, IB)

                    MASSE_TOTALE = MASSE_TOTALE + MC(I)
                Next

                ReDim XMR1(MASSES.NMR(ICAS, IB))
                ReDim XMR2(MASSES.NMR(ICAS, IB))
                ReDim MR1(MASSES.NMR(ICAS, IB))
                ReDim MR2(MASSES.NMR(ICAS, IB))
                For I = 1 To MASSES.NMR(ICAS, IB)
                    XMR1(I) = MASSES.XMR1(ICAS, I, IB)
                    MR1(I) = MASSES.MR1(ICAS, I, IB)
                    XMR2(I) = MASSES.XMR2(ICAS, I, IB)
                    MR2(I) = MASSES.MR2(ICAS, I, IB)

                    MASSE_TOTALE = MASSE_TOTALE + (MR1(I) + MR2(I)) * XLONG(IB) / 2
                Next

                Call MatM(XLONG(IB), XMC, MC, XMR1, XMR2, MR1, MR2, SML)

                CodeERR = 9

                'MATRICE DE MASSE DE LA BARRE DANS LE REPERE GLOBAL
                '-----------------------------------------
                Call ROTAX(CX(IB), CY(IB), SML, SMG)

                CodeERR = 10

                'ASSEMBLAGE DANS LA MATRICE DE MASSE GLOBALE DE LA STRUCTURE
                '--------------------------------------------------
                Call ASSEMB(JEXB(1, IB), JEXB(2, IB), SMG, SM)

                CodeERR = 11
            Next IB

            'If lConsole Then
            '    Console.WriteLine("Matrice SM avant ResolVP")
            '    For I = 1 To SM.GetUpperBound(0)
            '        Console.WriteLine("SM(" & I.ToString & ") = " & SM(I).ToString)
            '    Next
            'End If

            CodeERR = 12

            ReDim VALP(NBVP)
            ReDim VECTP(NBVP, IDIM)

            '------------------------------------- L A P A C K -----------------------------------------------
            Call ResolVP(SK, SM, IDIM, RESOLUTION.TOLERANCE, RESOLUTION.NOVECTP,
                         RESOLUTION.TXT_RECEPTEUR, RESOLUTION.TXT_PROGRESS, VALP, VECTP, RESOLUTION.NBVALP, RESOLUTION.DUMP)
            '-------------------------------------------------------------------------------------------------

            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            CodeERR = 13

            'If lConsole Then
            '    Console.WriteLine("Matrice SM après ResolVP")
            '    For I = 1 To SM.GetUpperBound(0)
            '        Console.WriteLine("SM(" & I.ToString & ") = " & SM(I).ToString)
            '    Next
            'End If

            'Récupérer la matrice de masse
            '-----------------------------
            Dim MatriceM(IDIM, IDIM)

            For I = 1 To IDIM
                For J = 1 To I
                    MatriceM(I, J) = SM(JND(I, J))
                Next J
            Next I

            'symétrie
            For I = 1 To IDIM
                For J = I To IDIM
                    MatriceM(I, J) = MatriceM(J, I)
                Next J
            Next I

            CodeERR = 14

            Dim MAX_D As Double

            If lConsole Then
                Console.WriteLine("NBVP = " & NBVP.ToString)
                Console.WriteLine("Nombre noeuds  NNT = " & NNT.ToString)
                Console.WriteLine("Nombre DDLs   IDIM = " & IDIM.ToString)
            End If

            Dim MAX_DN(3) As Double
            Const METHODNORM As Integer = 2
            Dim IDEX As Integer

            For IV = 1 To NBVP

                'Masse totale
                RESULTATS.MAS_TOT(ICAS) = MASSE_TOTALE

                If lConsole Then
                    Console.WriteLine("Mode No = " & IV.ToString)
                    For i = 1 To IDIM
                        Console.WriteLine("Vecteur(" & IV.ToString & " , " & i.ToString & ") = " & VECTP(IV, i).ToString)
                    Next
                    For i = 1 To IDIM
                        Console.WriteLine(VECTP(IV, i).ToString)
                    Next
                End If

                'NORMALISATION DU VECTEUR PROPRE : DMAX=1
                '--------------------------------------------
                If METHODNORM = 0 Then
                    MAX_D = 0
                    For I = 1 To IDIM
                        If Math.Abs(VECTP(IV, I)) > MAX_D Then
                            MAX_D = Math.Abs(VECTP(IV, I))
                        End If
                    Next
                    For I = 1 To IDIM
                        VECTP(IV, I) = VECTP(IV, I) / MAX_D
                    Next
                ElseIf METHODNORM = 1 Then
                    MAX_DN(1) = 0
                    MAX_DN(2) = 0
                    MAX_DN(3) = 0

                    For I = 1 To NNT
                        For J = 1 To 3
                            IDEX = (I - 1) * 3 + J
                            If Math.Abs(VECTP(IV, IDEX)) > MAX_DN(J) Then
                                MAX_DN(J) = Math.Abs(VECTP(IV, IDEX))
                            End If
                        Next
                    Next
                    For I = 1 To NNT
                        For J = 1 To 3
                            If MAX_DN(J) > 0 Then
                                IDEX = (I - 1) * 3 + J
                                VECTP(IV, IDEX) = VECTP(IV, IDEX) / MAX_DN(J)
                            End If
                        Next
                    Next
                ElseIf METHODNORM = 2 Then
                    MAX_D = 0
                    J = 2
                    For I = 1 To NNT

                        IDEX = (I - 1) * 3 + J
                        If Math.Abs(VECTP(IV, IDEX)) > MAX_D Then
                            MAX_D = Math.Abs(VECTP(IV, IDEX))
                        End If

                    Next

                    For I = 1 To IDIM
                        VECTP(IV, I) = VECTP(IV, I) / MAX_D
                    Next
                End If
                If lConsole Then
                    If METHODNORM = 0 Or METHODNORM = 2 Then
                        Console.WriteLine("Normalisation DMAX = " & MAX_D.ToString)
                    ElseIf METHODNORM = 1 Then
                        Console.WriteLine("Normalisation DMAX(1) = " & MAX_DN(1).ToString)
                        Console.WriteLine("Normalisation DMAX(2) = " & MAX_DN(2).ToString)
                        Console.WriteLine("Normalisation DMAX(3) = " & MAX_DN(3).ToString)
                    End If

                    For I = 1 To IDIM
                        Console.WriteLine("Vecteur(" & IV.ToString & " , " & I.ToString & ") = " & VECTP(IV, I).ToString)
                    Next

                    For I = 1 To 3
                        Console.WriteLine("DDL = " & I.ToString)
                        For J = 1 To NNT
                            IDEX = (J - 1) * 3 + I
                            Console.WriteLine(VECTP(IV, IDEX).ToString)
                        Next
                    Next

                End If

                DMD = 0
                DMDELTA = 0

                For I = 1 To IDIM
                    'D^T*M
                    DM = 0
                    For J = 1 To IDIM
                        DM = DM + VECTP(IV, J) * MatriceM(J, I)
                    Next
                    'D^T*M*D
                    DMD += DM * VECTP(IV, I)

                    DMDELTA += DM * DELTA(I)
                Next

                ' Masse généralisée
                RESULTATS.MAS_GEN(ICAS, IV) = DMD

                ' Masse modale                
                RESULTATS.MAS_MOD(ICAS, IV) = DMDELTA ^ 2 / DMD

                'NORMALISATION DU VECTEUR PROPRE : DT.M.D = 1
                '--------------------------------------------
                For I = 1 To IDIM
                    VECTP(IV, I) = VECTP(IV, I) / Math.Sqrt(DMD)
                Next

                ' EFFORTS AUX NOEUDS
                '-------------------

                For I = 1 To NNT

                    'M*D
                    ReDim MD(3)
                    For J = 1 To IDIM
                        For K = 1 To 3
                            MD(K) = MD(K) + MatriceM(3 * (I - 1) + K, J) * VECTP(IV, J)
                        Next
                    Next
                Next I
            Next

            CodeERR = 15

            'Stocker
            For I = 1 To NBVP
                'Valeur propre
                '-------------
                RESULTATS.VALP(ICAS, I) = VALP(I)

                'Récupération du vecteur déplacements
                '------------------------------------
                ReDim DEPT(IDIM)

                For J = 1 To IDIM
                    RESULTATS.VECTP(ICAS, I, J) = VECTP(I, J)
                    DEPT(J) = VECTP(I, J)
                Next

                'Rotation des barres
                '-------------------
                For IB = 1 To NBT
                    '-Déplacements d'extrémités en local
                    ReDim DEPL(6)
                    Call DEPLOC(CX(IB), CY(IB), JEXB(1, IB), JEXB(2, IB), DEPT, DEPL)
                Next
            Next
        Next


        CodeERR = 0
        TextERR = String.Empty

        Exit Sub


        '=================================================================================================================================================================
        'PROBLEME
        '=================================================================================================================================================================
        '-----------------------------------------------------------------------
PbProtec:
        '   CodeERR défini dans le module
        TextERR = "MODAL2D - Problem in authorization check"
        Exit Sub
        '-----------------------------------------------------------------------
PbSolving:
        '   CodeERR défini dans le module
        TextERR = "MODAL2D - Problem in solving eigenproblem"
        TextERR = TextERR + Chr(10) + Err.Description
        Exit Sub

    End Sub


    Sub MatK(ByVal E As Double, ByVal A As Double, ByVal IY As Double, ByVal L As Double, _
               ByVal RESB1 As Double, ByVal RESB2 As Double, _
               ByRef SKL() As Double, ByRef IERREUR As Integer)
        '-----------------------------------------------------------------------
        '     MATRICE DE RIGIDITE TANGENTE EN REPERE LOCAL
        '-----------------------------------------------------------------------
        ' -ENTREES:        
        '     E           : module d'Young
        '     IY          : moment dinertie de flexion
        '     A           : aire de section
        '     RESB1,RESB2 : ressorts de liaison aux extrémités
        '     L           : longueur        
        '
        ' -SORTIES:
        '     SKL(21)     : matrice de rigidit‚ tangente en repère local
        '-----------------------------------------------------------------------

        'C(6) matrice des ressorts  (termes de la diagonale seulement)

        Dim K(6, 6) As Double

        K(1, 1) = A * E / L
        K(1, 4) = -A * E / L

        K(2, 2) = 12 * E * IY / L ^ 3
        K(2, 3) = 6 * E * IY / L ^ 2
        K(2, 5) = -12 * E * IY / L ^ 3
        K(2, 6) = 6 * E * IY / L ^ 2

        K(3, 2) = 6 * E * IY / L ^ 2
        K(3, 3) = 4 * E * IY / L
        K(3, 5) = -6 * E * IY / L ^ 2
        K(3, 6) = 2 * E * IY / L

        K(4, 1) = -A * E / L
        K(4, 4) = A * E / L

        K(5, 2) = -12 * E * IY / L ^ 3
        K(5, 3) = -6 * E * IY / L ^ 2
        K(5, 5) = 12 * E * IY / L ^ 3
        K(5, 6) = -6 * E * IY / L ^ 2

        K(6, 2) = 6 * E * IY / L ^ 2
        K(6, 3) = 2 * E * IY / L
        K(6, 5) = -6 * E * IY / L ^ 2
        K(6, 6) = 4 * E * IY / L

        'Correspondance entre SKL(I) et K(I,J)
        ReDim SKL(21)
        Dim iA As Integer = 0
        For J = 1 To 6
            For I = 1 To J
                iA += 1
                SKL(iA) = K(I, J)
            Next
        Next

        '-TRAITEMENT DES RESSORTS D'EXTREMITES
        '=====================================
        Dim XA As Double = E * A / L
        Dim XI As Double = E * IY / L
        Dim D As Double = 10000000000.0# * XI
        If RESB1 > 0.99 * D And RESB2 > 0.99 * D Then Exit Sub

        Dim C(6) As Double

        C(1) = D * XA
        C(2) = D * XI / L ^ 2
        C(3) = RESB1
        C(4) = C(1)
        C(5) = C(2)
        C(6) = RESB2
        Call MATRES(C, SKL, IERREUR)

    End Sub

    Sub MatM(ByVal L As Double, _
             ByVal xM() As Double, ByVal vM() As Double, _
             ByVal xm1() As Double, ByVal xm2() As Double, _
             ByVal vm1() As Double, ByVal vm2() As Double, _
             ByRef SML() As Double)

        '-----------------------------------------------------------------------
        '     MATRICE DE MASSE EN REPERE LOCAL
        '-----------------------------------------------------------------------
        ' -ENTREES:        
        '     L           : longueur      
        '   xM          : position RELATIVE des masses concentrées
        '   vM           : valeur des masses concentrées
        '   xm1         : position RELATIVE gauche des masses réparties
        '   vm1          : valeur gauche des masses réparties
        '   xm2         : position RELATIVE droite des masses réparties
        '   vm2          : valeur droite des masses réparties
        '
        ' -SORTIES:
        '     SML(21)      : matrice de masse‚ tangente en repère local
        '-----------------------------------------------------------------------

        'C(6) matrice des ressorts  (termes de la diagonale seulement)

        Dim MA(6, 6) As Double

        '====== MASSES CONCENTREES ======
        Dim x, M As Double
        For I = 1 To vM.GetUpperBound(0)

            x = xM(I)
            M = vM(I)

            'u1-u1
            MA(1, 1) += (1 - x) ^ 2 * M

            'u1-u2
            MA(1, 4) += (1 - x) * x * M

            'u2-u2
            MA(4, 4) += x ^ 2 * M

            'v1-v1
            MA(2, 2) += (3 - 2 * (1 - x)) ^ 2 * (1 - x) ^ 4 * M

            'v1-theta1
            MA(2, 3) += (3 - 2 * (1 - x)) * (1 - x) ^ 4 * x * L * M

            'v1-v2
            MA(2, 5) += (3 - 2 * (1 - x)) * (3 - 2 * x) * (1 - x) ^ 2 * x ^ 2 * M

            'v1-theta2
            MA(2, 6) += (3 - 2 * (1 - x)) * (1 - x) ^ 2 * (x - 1) * x ^ 2 * L * M

            'theta1-theta1
            MA(3, 3) += (1 - x) ^ 4 * x ^ 2 * L ^ 2 * M

            'theta1-v2
            MA(3, 5) += (3 - 2 * x) * (1 - x) ^ 2 * x ^ 3 * L * M

            'theta1-theta2
            MA(3, 6) += (1 - x) ^ 2 * (x - 1) * x ^ 3 * L ^ 2 * M

            'v2-v2
            MA(5, 5) += (3 - 2 * x) ^ 2 * x ^ 4 * M

            'v2-theta2
            MA(5, 6) += (3 - 2 * x) * (x - 1) * x ^ 4 * L * M

            'theta2-theta2
            MA(6, 6) += (x - 1) ^ 2 * x ^ 4 * L ^ 2 * M
        Next


        '====== MASSES REPARTIES ======
        Dim x1, x2, m1, m2 As Double

        For I = 1 To vm1.GetUpperBound(0)

            x1 = xm1(I)
            m1 = vm1(I)
            x2 = xm2(I)
            m2 = vm2(I)

            'u1-u1
            MA(1, 1) += ((3 * m2 - 3 * m1) * x2 ^ 4 + _
                         (12 * m1 - 8 * m2) * x2 ^ 3 + _
                         (6 * m2 - 18 * m1) * x2 ^ 2 + _
                         12 * m1 * x2 + _
                         (3 * m1 - 3 * m2) * x1 ^ 4 + _
                         (8 * m2 - 12 * m1) * x1 ^ 3 + _
                         (18 * m1 - 6 * m2) * x1 ^ 2 - _
                         12 * m1 * x1) * L / 12

            'u1-u2
            MA(1, 4) += -((3 * m2 - 3 * m1) * x2 ^ 4 + _
                          (8 * m1 - 4 * m2) * x2 ^ 3 - _
                          6 * m1 * x2 ^ 2 + _
                          (3 * m1 - 3 * m2) * x1 ^ 4 + _
                          (4 * m2 - 8 * m1) * x1 ^ 3 + _
                          6 * m1 * x1 ^ 2) * L / 12

            'u2-u2
            MA(4, 4) += ((3 * m2 - 3 * m1) * x2 ^ 4 + _
                         4 * m1 * x2 ^ 3 + _
                         (3 * m1 - 3 * m2) * x1 ^ 4 - _
                         4 * m1 * x1 ^ 3) * L / 12

            'v1-v1
            MA(2, 2) += ((35 * m2 - 35 * m1) * x2 ^ 8 + _
                         (160 * m1 - 120 * m2) * x2 ^ 7 + _
                         (105 * m2 - 245 * m1) * x2 ^ 6 + _
                         (56 * m2 + 70 * m1) * x2 ^ 5 + _
                         (175 * m1 - 105 * m2) * x2 ^ 4 - _
                         140 * m1 * x2 ^ 3 + _
                         (35 * m2 - 35 * m1) * x2 ^ 2 + _
                         70 * m1 * x2 + _
                         (35 * m1 - 35 * m2) * x1 ^ 8 + _
                         (120 * m2 - 160 * m1) * x1 ^ 7 + _
                         (245 * m1 - 105 * m2) * x1 ^ 6 + _
                         (-56 * m2 - 70 * m1) * x1 ^ 5 + _
                         (105 * m2 - 175 * m1) * x1 ^ 4 + _
                         140 * m1 * x1 ^ 3 + _
                         (35 * m1 - 35 * m2) * x1 ^ 2 - _
                         70 * m1 * x1) * L / 70

            'v1-theta1
            MA(2, 3) += ((105 * m2 - 105 * m1) * x2 ^ 8 + _
                          (540 * m1 - 420 * m2) * x2 ^ 7 + _
                          (560 * m2 - 1050 * m1) * x2 ^ 6 + _
                          (840 * m1 - 168 * m2) * x2 ^ 5 - _
                          210 * m2 * x2 ^ 4 + _
                          (140 * m2 - 420 * m1) * x2 ^ 3 + _
                          210 * m1 * x2 ^ 2 + _
                          (105 * m1 - 105 * m2) * x1 ^ 8 + _
                          (420 * m2 - 540 * m1) * x1 ^ 7 + _
                          (1050 * m1 - 560 * m2) * x1 ^ 6 + _
                          (168 * m2 - 840 * m1) * x1 ^ 5 + _
                          210 * m2 * x1 ^ 4 + _
                          (420 * m1 - 140 * m2) * x1 ^ 3 - _
                          210 * m1 * x1 ^ 2) * L ^ 2 / 420

            'v1-v2
            MA(2, 5) += -((70 * m2 - 70 * m1) * x2 ^ 8 + _
                          (320 * m1 - 240 * m2) * x2 ^ 7 + _
                          (210 * m2 - 490 * m1) * x2 ^ 6 + _
                          (56 * m2 + 196 * m1) * x2 ^ 5 + _
                          (175 * m1 - 105 * m2) * x2 ^ 4 - _
                          140 * m1 * x2 ^ 3 + _
                          (70 * m1 - 70 * m2) * x1 ^ 8 + _
                          (240 * m2 - 320 * m1) * x1 ^ 7 + _
                          (490 * m1 - 210 * m2) * x1 ^ 6 + _
                          (-56 * m2 - 196 * m1) * x1 ^ 5 + _
                          (105 * m2 - 175 * m1) * x1 ^ 4 + _
                          140 * m1 * x1 ^ 3) * L / 140

            'v1-theta2
            MA(2, 6) += ((105 * m2 - 105 * m1) * x2 ^ 8 + _
                          (420 * m1 - 300 * m2) * x2 ^ 7 + _
                          (210 * m2 - 560 * m1) * x2 ^ 6 + _
                          (84 * m2 + 168 * m1) * x2 ^ 5 + _
                          (210 * m1 - 105 * m2) * x2 ^ 4 - _
                          140 * m1 * x2 ^ 3 + _
                          (105 * m1 - 105 * m2) * x1 ^ 8 + _
                          (300 * m2 - 420 * m1) * x1 ^ 7 + _
                          (560 * m1 - 210 * m2) * x1 ^ 6 + _
                          (-84 * m2 - 168 * m1) * x1 ^ 5 + _
                          (105 * m2 - 210 * m1) * x1 ^ 4 + _
                          140 * m1 * x1 ^ 3) * L ^ 2 / 420

            'theta1-theta1
            MA(3, 3) += ((105 * m2 - 105 * m1) * x2 ^ 8 + _
                          (600 * m1 - 480 * m2) * x2 ^ 7 + _
                          (840 * m2 - 1400 * m1) * x2 ^ 6 + _
                          (1680 * m1 - 672 * m2) * x2 ^ 5 + _
                          (210 * m2 - 1050 * m1) * x2 ^ 4 + _
                          280 * m1 * x2 ^ 3 + _
                          (105 * m1 - 105 * m2) * x1 ^ 8 + _
                          (480 * m2 - 600 * m1) * x1 ^ 7 + _
                          (1400 * m1 - 840 * m2) * x1 ^ 6 + _
                          (672 * m2 - 1680 * m1) * x1 ^ 5 + _
                          (1050 * m1 - 210 * m2) * x1 ^ 4 - _
                          280 * m1 * x1 ^ 3) * L ^ 3 / 840

            'theta1-v2
            MA(3, 5) += -((105 * m2 - 105 * m1) * x2 ^ 8 + _
                           (540 * m1 - 420 * m2) * x2 ^ 7 + _
                           (560 * m2 - 1050 * m1) * x2 ^ 6 + _
                           (924 * m1 - 252 * m2) * x2 ^ 5 - _
                           315 * m1 * x2 ^ 4 + _
                           (105 * m1 - 105 * m2) * x1 ^ 8 + _
                           (420 * m2 - 540 * m1) * x1 ^ 7 + _
                           (1050 * m1 - 560 * m2) * x1 ^ 6 + _
                           (252 * m2 - 924 * m1) * x1 ^ 5 + _
                           315 * m1 * x1 ^ 4) * L ^ 2 / 420

            'theta1-theta2
            MA(3, 6) += ((35 * m2 - 35 * m1) * x2 ^ 8 + _
                          (160 * m1 - 120 * m2) * x2 ^ 7 + _
                          (140 * m2 - 280 * m1) * x2 ^ 6 + _
                          (224 * m1 - 56 * m2) * x2 ^ 5 - _
                          70 * m1 * x2 ^ 4 + _
                          (35 * m1 - 35 * m2) * x1 ^ 8 + _
                          (120 * m2 - 160 * m1) * x1 ^ 7 + _
                          (280 * m1 - 140 * m2) * x1 ^ 6 + _
                          (56 * m2 - 224 * m1) * x1 ^ 5 + _
                          70 * m1 * x1 ^ 4) * L ^ 3 / 280

            'v2-v2
            MA(5, 5) += ((35 * m2 - 35 * m1) * x2 ^ 8 + _
                         (160 * m1 - 120 * m2) * x2 ^ 7 + _
                         (105 * m2 - 245 * m1) * x2 ^ 6 + _
                         126 * m1 * x2 ^ 5 + _
                         (35 * m1 - 35 * m2) * x1 ^ 8 + _
                         (120 * m2 - 160 * m1) * x1 ^ 7 + _
                         (245 * m1 - 105 * m2) * x1 ^ 6 - _
                         126 * m1 * x1 ^ 5) * L / 70

            'v2-theta2
            MA(5, 6) += -((105 * m2 - 105 * m1) * x2 ^ 8 + _
                           (420 * m1 - 300 * m2) * x2 ^ 7 + _
                           (210 * m2 - 560 * m1) * x2 ^ 6 + _
                           252 * m1 * x2 ^ 5 + _
                           (105 * m1 - 105 * m2) * x1 ^ 8 + _
                           (300 * m2 - 420 * m1) * x1 ^ 7 + _
                           (560 * m1 - 210 * m2) * x1 ^ 6 - _
                           252 * m1 * x1 ^ 5) * L ^ 2 / 420

            'theta2-theta2
            MA(6, 6) += ((105 * m2 - 105 * m1) * x2 ^ 8 + _
                          (360 * m1 - 240 * m2) * x2 ^ 7 + _
                          (140 * m2 - 420 * m1) * x2 ^ 6 + _
                          168 * m1 * x2 ^ 5 + _
                          (105 * m1 - 105 * m2) * x1 ^ 8 + _
                          (240 * m2 - 360 * m1) * x1 ^ 7 + _
                          (420 * m1 - 140 * m2) * x1 ^ 6 - _
                          168 * m1 * x1 ^ 5) * L ^ 3 / 840
        Next

        '===== Poids propres ====
        ' POUR VERIFIER LA MATRICE M
        'ReDim MA(6, 6)
        'MA(1, 1) = mq * L / 3
        'MA(1, 4) = mq * L / 6
        'MA(4, 4) = mq * L / 3
        'MA(2, 2) = mq * L * 13 / 35
        'MA(2, 3) = mq * L ^ 2 * 11 / 210
        'MA(2, 5) = mq * L * 9 / 70
        'MA(2, 6) = -mq * L ^ 2 * 13 / 420
        'MA(3, 3) = mq * L ^ 3 / 105
        'MA(3, 5) = mq * L ^ 2 * 13 / 420
        'MA(3, 6) = -mq * L ^ 3 / 140
        'MA(5, 5) = mq * L * 13 / 35
        'MA(5, 6) = -mq * L ^ 2 * 11 / 210
        'MA(6, 6) = mq * L ^3 / 105


        'Symétrie
        MA(4, 1) = MA(1, 4)
        MA(3, 2) = MA(2, 3)
        MA(5, 2) = MA(2, 5)
        MA(6, 2) = MA(2, 6)
        MA(4, 3) = MA(3, 4)
        MA(5, 3) = MA(3, 5)
        MA(6, 3) = MA(3, 6)
        MA(5, 4) = MA(4, 5)
        MA(6, 4) = MA(4, 6)
        MA(6, 5) = MA(5, 6)


        'Correspondance entre SML(I) et MA(I,J)
        ReDim SML(21)
        Dim iA As Integer = 0
        For J = 1 To 6
            For I = 1 To J
                iA += 1
                SML(iA) = MA(I, J)
            Next
        Next

    End Sub

    Sub MATRES(ByRef C() As Double, ByRef SBL() As Double, ByRef IERREUR As Integer)
        '-----------------------------------------------------------------------
        '     INTRODUCTION DES RESSORTS DANS MATRICE D'UNE BARRE
        '-----------------------------------------------------------------------
        '
        '     CALCUL DE  C * (C+B)  * B        C= matrice diagonale des ressorts
        '                                      B= matrice sans les ressorts
        '                                      SBL=matrice réduite
        '     On pose  A = C + B
        '
        '              U = A  * B              U calculé en résolvant  A*U=B
        '              A = l * LT
        '                                  on résoud L*Y=X   X=Bj
        '                                  on résoud LT*X=Y  Uj=X
        '                                  puis on calcule C*U
        '
        '     Les matrices A et B sont stockées triangulaires inférieures
        '     par lignes
        '-----------------------------------------------------------------------

        Dim A(21) As Double, B(21) As Double, X(6) As Double, Y(6) As Double
        'Dim C(6), SBL(21)
        Dim N, I, J, K As Integer

        N = 6
        'N*(N+1) = 21

        For I = 1 To 21
            B(I) = SBL(I)
        Next I

        '-CALCUL DE  A = C+B
        '-******************
        For I = 1 To 21
            A(I) = B(I)
        Next I
        A(1) = B(1) + C(1)
        A(3) = B(3) + C(2)
        A(6) = B(6) + C(3)
        A(10) = B(10) + C(4)
        A(15) = B(15) + C(5)
        A(21) = B(21) + C(6)

        '-DECOMPOSITION DE CHOLESKI  A=L.LT  ===> L DANS A
        '-************************************************
        Call CHOL(A, N, 21, IERREUR)
        If IERREUR = 1001 Then Exit Sub

        '-RESOLUTION DE A*U=B
        '-*******************
        For J = N To 1 Step -1

            '-EXTRACTION DE LA COLONNE J DE B  ===> X
            If J > 1 Then
                For I = 1 To J - 1
                    X(I) = B(JND(J, I))
                Next I
            End If
            For I = J To N
                X(I) = B(JND(I, J))
            Next I

            '-RESOLUTION DE  L.Y=X
            Y(1) = X(1) / A(1)
            For I = 2 To N
                Y(I) = X(I)
                For K = 1 To I - 1
                    Y(I) = Y(I) - A(JND(I, K)) * Y(K)
                Next K
                Y(I) = Y(I) / A(JND(I, I))
            Next I

            '-RESOLUTION DE  LT.X=Y   (X=colonne J de U)
            X(N) = Y(N) / A(JND(N, N))
            For I = N - 1 To 1 Step -1
                X(I) = Y(I)
                For K = I + 1 To N
                    X(I) = X(I) - A(JND(K, I)) * X(K)
                Next K
                X(I) = X(I) / A(JND(I, I))
            Next I

            '-INSERTION DE X DANS COLONNE J DE B  (car U est stock‚e dans B)
            '-ET MULTIPLICATION PAR C   ( B=C*U)
            For I = J To N
                B(JND(I, J)) = X(I) * C(I)
                If Math.Abs(B(JND(I, J))) < 0.001 Then B(JND(I, J)) = 0
            Next I

        Next J

        '-MATRICE REDUITE AVEC RESSORTS
        '-*****************************
        For I = 1 To 21
            SBL(I) = B(I)
        Next I

    End Sub

    Sub CHOL(ByRef A() As Double, ByRef N As Integer, ByRef NIM As Integer, ByRef IERREUR As Integer)
        '-----------------------------------------------------------------------
        '    DECOMPOSITION DE CHOLESKI D'UNE MATRICE SYMETRIQUE DEFINIE POSITIVE
        '-----------------------------------------------------------------------
        '    A EST LA PARTIE TRIANGULAIRE INFERIEURE DE LA MATRICE A DECOMPOSER.
        '    EN SORTIE, A EST LA MATRICE TRIANGULAIRE INFERIEURE L DE LA
        '    DECOMPOSITION, STOCKEE PAR LIGNES (EN ENTREE ET EN SORTIE)
        '    N EST LE NOMBRE DE LIGNES DE A
        '-----------------------------------------------------------------------

        '      REAL*8 A(NIM)

        Dim EPS, TOL As Double
        Dim KPIV, IND, LEND, LANF, LIND, IER, NNN As Integer
        Dim DSum, DPIV As Double

        EPS = 0.00001

        If N < 1 Then GoTo ierr

        KPIV = 0
        IER = 0

        For K = 1 To N

            KPIV = KPIV + K
            IND = KPIV
            LEND = K - 1
            TOL = Math.Abs(EPS * A(KPIV))

            For I = K To N

                DSum = 0

                If LEND <> 0 Then
                    For l = 1 To LEND
                        LANF = KPIV - l
                        LIND = IND - l
                        DSum = DSum + A(LANF) * A(LIND)
                    Next l
                End If

                DSum = (A(IND)) - DSum

                If I = K Then
                    If DSum <= TOL Then
                        If DSum <= 0 Then GoTo ierr
                        If IER <= 0 Then IER = K - 1
                    End If
                    DPIV = Math.Sqrt(DSum)
                    A(KPIV) = DPIV
                    DPIV = 1 / DPIV
                Else
                    A(IND) = DSum * DPIV
                End If

                IND = IND + I

            Next I

        Next K

        NNN = N * (N + 1) / 2
        For I = 1 To NNN
            If Math.Abs(A(I)) < 0.0000000001 Then A(I) = 0
        Next I

        If IER <> 0 Then GoTo ierr

        Exit Sub

ierr:
        IERREUR = 1001

    End Sub

    Sub SUPPORTS(ByRef NNT As Integer, ByRef RESN(,) As Double, ByRef S() As Double)
        '----------------------------------------------------------------------
        '     ASSEMBLAGE DES SUPPORTS DANS LA MATRICE DE RIGIDITE
        '----------------------------------------------------------------------
        ' -ENTREES:
        '     NNT        : Nombre total de noeuds
        '     S(e)       : Matrice de rigidité (élément e)
        '     RESN(d,n)  : Ressort suivant le ddl d du noeud n
        '
        ' -SORTIES:
        '     S(e)       : Matrice de rigidité (élément e)
        '----------------------------------------------------------------------

        '      INTEGER*4 NDIM,MAXA(IDIM1)
        '      DOUBLE PRECISION S(NDIM)
        '      Dimension RESN(3, NNT)

        Dim NI As Integer

        For N = 1 To NNT
            For I = 1 To 3
                NI = 3 * (N - 1) + I

                S(JND(NI, NI)) = RESN(I, N)
            Next I
        Next N

    End Sub

    Sub ROTAX(ByRef C As Single, ByVal S As Single, ByRef SBL() As Double, ByRef SBG() As Double)

        Dim K As Integer

        'Matrice de transfert TR (u1,v1,v1',u2,v2,v2') du repère global au repère local (|d|=[TR].|D|)
        'C   S   0   0   0   0
        '-S  C   0   0   0   0
        '0   0   1   0   0   0
        '0   0   0   C   S   0
        '0   0   0   -S  C   0
        '0   0   0   0   0   1

        Dim SBCARRE(6, 6) As Double
        Dim TR(6, 6) As Double
        Dim TRT(6, 6) As Double
        Dim TMP(6, 6) As Double

        'Passage de triangulaire inférieure à matrice carrée
        K = 0
        For I = 1 To 6 : For J = 1 To I : K = K + 1 : SBCARRE(I, J) = SBL(K) : Next J : Next I
        For I = 1 To 6 : For J = 1 To I : SBCARRE(J, I) = SBCARRE(I, J) : Next J : Next I

        'Matrice de rotation TR
        TR(1, 1) = C
        TR(1, 2) = S
        TR(2, 1) = -S
        TR(2, 2) = C
        TR(3, 3) = 1
        TR(4, 4) = C
        TR(4, 5) = S
        TR(5, 4) = -S
        TR(5, 5) = C
        TR(6, 6) = 1

        'TR transposée
        For I = 1 To 6 : For J = 1 To 6 : TRT(I, J) = TR(J, I) : Next J : Next I

        'TMP=TRT*SBCARRE
        For I = 1 To 6
            For J = 1 To 6
                TMP(I, J) = 0
                For K = 1 To 6
                    TMP(I, J) = TMP(I, J) + TRT(I, K) * SBCARRE(K, J)
                Next K
            Next J
        Next I

        'SBCARRE=TMP*TR
        For I = 1 To 6
            For J = 1 To 6
                SBCARRE(I, J) = 0
                For K = 1 To 6
                    SBCARRE(I, J) = SBCARRE(I, J) + TMP(I, K) * TR(K, J)
                Next K
            Next J
        Next I

        'Re-passage en triangulaire inférieure
        K = 0
        For I = 1 To 6 : For J = 1 To I : K = K + 1 : SBG(K) = SBCARRE(I, J) : Next J : Next I

    End Sub

    Sub ASSEMB(ByRef JEXB1 As Integer, ByRef JEXB2 As Integer, ByRef SBG() As Double, ByRef S() As Double)
        '----------------------------------------------------------------------
        'ASSEMBLAGE DANS LA MATRICE GLOBALE DE LA STRUCTURE
        '----------------------------------------------------------------------
        'ENTREES:
        '     JEXB1,JEXB2  : num‚ros des noeuds extr‚mit‚s de la barre
        '     S(ndim)      : matrice de rigidit‚ globale initiale
        '     SBG(10)      : matrice de rigidit‚ de la barre trait‚e
        '     NDIM         : dimension de S
        '     IDIM1        : dimension de MAXA (nombre de ddl +1)
        '
        'SORTIES:
        '     S(ndim)      : matrice de rigidit‚ globale initiale
        '----------------------------------------------------------------------

        'Dim NDIM As Integer
        'Dim MAXA(IDIM1) As Integer
        'Dim S(NDIM) As Double
        'Dim SBG(10) As Double

        'IND(I,J)=MAXA(MAX(I,J))+MAX(I,J)-MIN(I,J)

        Dim J1, K1 As Integer
        Dim inds, indsl As Integer

        Dim indJ, indK As Integer

        J1 = 3 * JEXB1 - 2
        K1 = 3 * JEXB2 - 2
        indJ = 1
        indK = 4

        'Noeud JJ
        For L = J1 To J1 + 2
            For C = J1 To L
                inds = JND(L, C)
                indsl = JND(L - J1 + indJ, C - J1 + indJ)
                S(inds) = S(inds) + SBG(indsl)
            Next C
        Next L

        'Noeud KK
        For L = K1 To K1 + 2
            For C = K1 To L
                inds = JND(L, C)
                indsl = JND(L - K1 + indK, C - K1 + indK)
                S(inds) = S(inds) + SBG(indsl)
            Next C
        Next L

        'Couplage JJ/JK
        For L = K1 To K1 + 2
            For C = J1 To J1 + 2
                inds = JND(L, C)
                indsl = JND(L - K1 + indK, C - J1 + indJ)
                S(inds) = S(inds) + SBG(indsl)
            Next C
        Next L

    End Sub

    Function JND(ByRef I As Integer, ByRef J As Integer) As Integer

        Return (I * (I - 1)) / 2 + J

    End Function

    Sub ASSEMB(ByRef JEXB1 As Integer, ByRef JEXB2 As Integer, ByRef MAXA() As Long, ByRef SBG() As Double, ByRef S() As Double)
        '----------------------------------------------------------------------
        'ASSEMBLAGE DANS LA MATRICE GLOBALE DE LA STRUCTURE
        '----------------------------------------------------------------------
        'ENTREES:
        '     JEXB1,JEXB2  : num‚ros des noeuds extr‚mit‚s de la barre
        '     MAXA(idim1)  : num‚ros des ‚l‚ments diagonaux dans la matrice S
        '     S(ndim)      : matrice de rigidit‚ globale initiale
        '     SBG(10)      : matrice de rigidit‚ de la barre trait‚e
        '     NDIM         : dimension de S
        '     IDIM1        : dimension de MAXA (nombre de ddl +1)
        '
        'SORTIES:
        '     S(ndim)      : matrice de rigidit‚ globale initiale
        '----------------------------------------------------------------------

        'Dim NDIM As Integer
        'Dim MAXA(IDIM1) As Integer
        'Dim S(NDIM) As Double
        'Dim SBG(10) As Double

        'IND(I,J)=MAXA(MAX(I,J))+MAX(I,J)-MIN(I,J)

        Dim J1, J2, J3, K1, K2, K3 As Integer
        Dim J1J1, J1J2, J1J3, J2J2, J2J3, J3J3 As Integer
        Dim K1K1, K1K2, K1K3, K2K2, K2K3, K3K3 As Integer
        Dim J1K1, J1K2, J1K3, J2K1, J2K2, J2K3, J3k1, J3K2, J3K3 As Integer

        J1 = 3 * JEXB1 - 2
        J2 = J1 + 1
        J3 = J2 + 1
        K1 = 3 * JEXB2 - 2
        K2 = K1 + 1
        K3 = K2 + 1

        J1J1 = KND(MAXA, J1, J1)
        S(J1J1) = S(J1J1) + SBG(1)
        J1J2 = KND(MAXA, J1, J2)
        S(J1J2) = S(J1J2) + SBG(2)
        J1J3 = KND(MAXA, J1, J3)
        S(J1J3) = S(J1J3) + SBG(4)
        J2J2 = KND(MAXA, J2, J2)
        S(J2J2) = S(J2J2) + SBG(3)
        J2J3 = KND(MAXA, J2, J3)
        S(J2J3) = S(J2J3) + SBG(5)
        J3J3 = KND(MAXA, J3, J3)
        S(J3J3) = S(J3J3) + SBG(6)

        K1K1 = KND(MAXA, K1, K1)
        S(K1K1) = S(K1K1) + SBG(10)
        K1K2 = KND(MAXA, K1, K2)
        S(K1K2) = S(K1K2) + SBG(14)
        K1K3 = KND(MAXA, K1, K3)
        S(K1K3) = S(K1K3) + SBG(19)
        K2K2 = KND(MAXA, K2, K2)
        S(K2K2) = S(K2K2) + SBG(15)
        K2K3 = KND(MAXA, K2, K3)
        S(K2K3) = S(K2K3) + SBG(20)
        K3K3 = KND(MAXA, K3, K3)
        S(K3K3) = S(K3K3) + SBG(21)

        J1K1 = KND(MAXA, J1, K1)
        S(J1K1) = S(J1K1) + SBG(7)
        J1K2 = KND(MAXA, J1, K2)
        S(J1K2) = S(J1K2) + SBG(11)
        J1K3 = KND(MAXA, J1, K3)
        S(J1K3) = S(J1K3) + SBG(16)
        J2K1 = KND(MAXA, J2, K1)
        S(J2K1) = S(J2K1) + SBG(8)
        J2K2 = KND(MAXA, J2, K2)
        S(J2K2) = S(J2K2) + SBG(12)
        J2K3 = KND(MAXA, J2, K3)
        S(J2K3) = S(J2K3) + SBG(17)
        J3k1 = KND(MAXA, J3, K1)
        S(J3k1) = S(J3k1) + SBG(9)
        J3K2 = KND(MAXA, J3, K2)
        S(J3K2) = S(J3K2) + SBG(13)
        J3K3 = KND(MAXA, J3, K3)
        S(J3K3) = S(J3K3) + SBG(18)

    End Sub

    Function KND(ByRef MAXA, ByRef I, ByRef J) As Integer

        'Dim MAXA(IDIM1) As Integer
        Dim Max, Min

        If J > I Then Max = J Else Max = I
        If J < I Then Min = J Else Min = I
        KND = MAXA(Max) + Max - Min

    End Function

    Function MAXI(ByRef X As Integer, ByRef Y As Integer) As Integer

        If Y > X Then
            Return Y
        Else
            Return X
        End If

    End Function

    Sub STOCKAGE(ByRef NNT As Integer, ByRef NBT As Integer, ByRef JEXB(,) As Integer, ByRef NCOL() As Integer, ByRef NDIM As Integer, ByRef MAXA() As Long)
        '----------------------------------------------------------------------
        '     CALCULS RELATIFS AU STOCKAGE DE LA MATRICE DE RIGIDITE
        '----------------------------------------------------------------------
        ' -ENTREES:
        '     NNT          : Nombre total de noeuds
        '     NBT          : Nombre total de barres
        '     JEXB(e,b)    : Numéro du noeud à l'extrémité e de la barre b
        '     IDIM1        : Dimension du tableau MAXA
        '
        ' -SORTIES:
        '     NCOL(c)      : Hauteur de la colonne c de la matrice
        '     NDIM         : Dimension de la matrice skyline
        '     MAXA(c)      : Indice de l'élément diagonal de la colonne c
        '----------------------------------------------------------------------

        '      Dimension JEXB(2, NBT), NCOL(NNT)
        '      INTEGER*4 NDIM,MAXA(IDIM+1)

        Dim M, NH, NC As Integer

        ' -INITIALISATIONS
        For N = 1 To NNT
            NCOL(N) = 1
        Next N

        '-CALCUL DES HAUTEURS DE COLONNES
        For IB = 1 To NBT
            M = MAXI(JEXB(1, IB), JEXB(2, IB))
            NH = Math.Abs(JEXB(1, IB) - JEXB(2, IB)) + 1
            NCOL(M) = MAXI(NCOL(M), NH)
        Next IB

        '-CALCUL DU TABLEAU D'INDICES DES TERMES DIAGONAUX
        MAXA(1) = 1
        NC = 1
        For N = 1 To NNT
            For IC = 1 To 3
                NC = NC + 1
                MAXA(NC) = MAXA(NC - 1) + 3 * (NCOL(N) - 1) + IC
            Next IC
        Next N
        NDIM = 0

        '-DIMENSION DE LA MATRICE SKYLINE
        NDIM = MAXA(NC) - 1

    End Sub

    Sub SUPPORTS(ByRef NNT As Integer, ByRef MAXA() As Long, ByRef RESN(,) As Double, ByRef S() As Double)
        '----------------------------------------------------------------------
        '     ASSEMBLAGE DES SUPPORTS DANS LA MATRICE DE RIGIDITE
        '----------------------------------------------------------------------
        ' -ENTREES:
        '     NNT        : Nombre total de noeuds
        '     MAXA(c)    : Indice de l'élément diagonal de la colonne c
        '     S(e)       : Matrice de rigidité (élément e)
        '     RESN(d,n)  : Ressort suivant le ddl d du noeud n
        '     NDIM       : Dimension de la matrice S
        '
        ' -SORTIES:
        '     S(e)       : Matrice de rigidité (élément e)
        '----------------------------------------------------------------------

        '      INTEGER*4 NDIM,MAXA(IDIM1)
        '      DOUBLE PRECISION S(NDIM)
        '      Dimension RESN(3, NNT)

        Dim NI, N, I As Integer

        For N = 1 To NNT
            For I = 1 To 3
                NI = 3 * (N - 1) + I
                S(MAXA(NI)) = RESN(I, N)
            Next I
        Next N

    End Sub

    Sub DEPLOC(ByRef CX As Double, ByRef CY As Double, ByRef JO As Integer, ByRef JE As Integer, ByRef DEP() As Double, ByRef DEPL() As Double)
        '----------------------------------------------------------------------
        'CALCUL DES DEPLACEMENTS D'EXTREMITE D'UNE BARRE EN LOCAL
        '----------------------------------------------------------------------
        'ENTREES:
        '     CX,CY        : cosinus directeurs de la barre
        '     JO,JE        : num‚ros d'extr‚mit‚s de la barre
        '     DEP(d)       : vecteur d‚placements dans repŠre global
        '     IDIM         : nb de ddl total
        '
        'SORTIES:
        '     DEPL(6)      : d‚placements d'extr‚mit‚s de la barre en local
        '-----------------------------------------------------------------------

        'Dim DEP(IDIM) As Double
        'Dim DEPL(6) As Double

        Dim U, V As Double

        U = DEP(3 * JO - 2)
        V = DEP(3 * JO - 1)
        DEPL(1) = U * CX + V * CY
        DEPL(2) = V * CX - U * CY
        DEPL(3) = DEP(3 * JO)

        U = DEP(3 * JE - 2)
        V = DEP(3 * JE - 1)
        DEPL(4) = U * CX + V * CY
        DEPL(5) = V * CX - U * CY
        DEPL(6) = DEP(3 * JE)

    End Sub

End Module
