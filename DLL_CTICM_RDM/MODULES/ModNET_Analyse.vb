Option Strict Off
Option Explicit On

Module Mod_Analyse

    '================================================================================================
    'Déclarations pour le Module Analyse
    '================================================================================================

    'Tableaux "images" des structures
    '--------------------------------
    'STR_NOEUDS
    Dim X() As Double
    Dim Y() As Double
    Dim RESN(,) As Double
    'STR_BARRES
    Dim JEXB(,) As Integer
    Dim SECT() As Double
    Dim XIN() As Double
    Dim RESB(,) As Double
    Dim OM0() As Double
    Dim NBTRONC() As Integer
    'CHRG_BARRES
    Dim NCC() As Integer
    Dim XFC(,) As Double
    Dim FCX(,) As Double
    Dim FCY(,) As Double
    Dim FCZ(,) As Double
    Dim NCR() As Integer
    Dim NBTRONCONS() As Integer
    Dim XFR1(,) As Double
    Dim XFR2(,) As Double
    Dim FRX1(,) As Double
    Dim FRX2(,) As Double
    Dim FRY1(,) As Double
    Dim FRY2(,) As Double
    Dim TEMPB() As Double
    'RES_NOEUDS
    Dim DEPT() As Double
    Dim REACT() As Double
    'RES_BARRES
    Dim NSINT() As Integer
    Dim NSINT0(,) As Integer
    Dim XF(,) As Double
    Dim OMT() As Double
    Dim ROTEX() As Double

    'Autres tableaux
    '---------------
    Dim XLONG() As Double
    Dim CX() As Double
    Dim CY() As Double
    Dim FN() As Double
    Dim NCOL() As Integer
    Dim MAXA() As Long
    Dim SBLOC(,) As Double
    Dim AML(,) As Double
    Dim S() As Double
    Dim XF1() As Double
    Dim XF2() As Double
    Dim FX1() As Double
    Dim FX2() As Double
    Dim FY1() As Double
    Dim FY2() As Double
    Dim DX() As Double
    Dim DY() As Double
    Dim DZ() As Double
    Dim SNX(,) As Double
    Dim SMX(,) As Double
    Dim STX(,) As Double
    Dim XMIEQ0() As Double
    Dim XMJEQ0() As Double
    Dim XFF() As Double
    Dim XFI() As Double
    Dim FX() As Double
    Dim FY() As Double
    Dim FZ() As Double
    Dim WIT() As Double
    Dim WJT() As Double
    Dim SNT(,) As Double
    Dim STT(,) As Double
    Dim SMT(,) As Double
    Dim JNMIN() As Integer
    Dim JNMAX() As Integer
    Dim JTMIN() As Integer
    Dim JTMAX() As Integer
    Dim JMMIN() As Integer
    Dim JMMAX() As Integer
    Dim INDSI() As Integer
    Dim OMTSO() As Double
    Dim FNCB() As Double
    Dim FN2ORDRELOC() As Double
    Dim AML2ORDRELOC(,) As Double
    Dim OMTMOY() As Double
    Dim ALPHACR() As Double
    Dim FNINIT() As Double
    Dim SNTMOY() As Double
    Dim OMTIBSNTOMTMAX() As Double

    Const RIGID = 1.0E+24
    Const ARTIC = 0.0001

    '==================================================================================================================================================================
    'Routine d'analyse élastique de structures 2D - Yvan Galéa - Février 2010 - Nov 2011
    '==================================================================================================================================================================

    Sub ANALYSE(ByRef MAT As MATERIAU, ByRef NOEUDS As STR_NOEUDS, ByRef BARRES As STR_BARRES, ByRef CH_NOEUDS As CHRG_NOEUDS,
                 ByRef CH_BARRES As CHRG_BARRES, ByRef OUT_NOEUDS As RES_NOEUDS, ByRef OUT_BARRES As RES_BARRES,
                 ByRef TYPANALYS As TYPE_ANALYSE, ByRef CodeERR As Integer, ByRef TextERR As String)
        '==============================================================================================================================================================
        '
        '   R O U T I N E    D ' A N A L Y S E      E L A S T I Q U E    D E    S T R U C T U R E S     2 D
        '
        '==============================================================================================================================================================
        '
        ' Pour l'analyse au second ordre, la Routine détermine les déplacements de noeuds et les sollicitations aux extrémités des barres
        ' en prenant en compte:
        '     - les effets globaux dus à la rotation globale de chaque barre (forces antagonistes SNT*OMT normales à la barre aux extrémités)
        '     - la conservation de la longueur d'une barre après rotation par introduction d'une compression fictive 0.5*EA*OMT^2 (ex. pour effet de chaînette)
        '     - les effets du second ordre locaux dus à la déformée élastique locale déterminée en discrétisant chaque barre I en BARRES.NBTRONC(I) tronçons;
        '       ces effets sont introduits via les charges équivalentes aux noeuds de la barre générées par les forces SNT*OMT de chaque tronçon de la barre
        '       (AML2ORDRELOC aux extrémités de la barre, FN2ORDRELOC pour chaque noeud en repère global)
        ' Attention : les sollicitations le long de la barre sont déterminées au 1er ordre (pas d'influence de la déformée sur leur distribution), et ce 
        '             afin de faciliter les vérifications de barres ultérieures
        '
        ' Si TYPANALYS.CONSTANTMATRIX = Vrai   --> la matrice n'est calculée qu'au 1er appel de la DLL puis stockée en STATIC
        '
        ' Si structure instable (mécaniquement) -->  CodeERR = 1021 ou 1023 & TextERR & TYPANALYS.ALPHACR(ICAS) = 0
        '
        ' Pour un cas de charge ICAS donné,
        '       Si TYPANALYS.GETALPHACR(ICAS)=Faux        -->   TYPANALYS.ALPHACR(ICAS) = 0
        '       Si TYPANALYS.GETALPHACR(ICAS)=Vrai        -->   calcul de TYPANALYS.ALPHACR(ICAS)  (que l'analyse soit au 1er ou au 2d ordre)
        '                                                       TYPANALYS.ALPHACR(ICAS) peut être inférieur à 1
        '                                                       ALPHACR calculé par méthode itérative sur variations d'un OMT moyen sur barres avec SNT*OMT significatif
        '                                                       ALPHACR = rapport de OMTmoy(iter-1)/OMTmoy(iter) après convergence
        '                                                       Attention : si portique symétrique, symétriquement déformé et chargé --> on peut avoir TYPANALYS.ALPHACR(ICAS)=infini
        '                                                       solution tentée : réinjection de SNTOMTMAX dans les barres avec SNTOMT significatifs
        '                                                   
        '       Si TYPANALYS.SECONDORDRE(ICAS)=Vrai       -->   Analyse au 2d ordre (indépendamment du calcul de ALPHACR)
        '                                                       Analyse réalisée par méthode itérative
        '                                                       Test de convergence sur chacune des OMT significatives  --> si OK  :   TYPANALYS.CONVERGE(ICAS)=Vrai
        '                                                                                                                   sinon  :   TYPANALYS.CONVERGE(ICAS)=Faux
        '                                                                                                                              si Instabilité et si pas de calcul de ALPHACR:
        '                                                                                                                                   ---> TYPANALYS.ALPHACR(ICAS) = -1
        ' Dans tous les cas la matrice de rigidité n'est calculée qu'une fois, puis triangularisée.
        ' On procède ensuite par Backsubstitution pour chaque cas de charge, ou pour chaque itération (ALPHACR ou 2d Ordre)
        '
        '==============================================================================================================================================================

        'Déclarations
        '------------
        Dim NBTRONCMAX As Integer
        Dim NCAS, ICAS As Integer
        Dim IDDL As Integer
        Dim IERREUR As Integer
        Dim IB As Integer
        Dim I, J, K As Integer
        Dim SECONDORDRE, ACONVERGE As Boolean
        Dim NBITERSOMAX, NBITERSO, NBITERSOMIN, NBITERSOCONV As Integer
        Dim EPSO As Double
        Dim SNTMIN, SNTMAX, STTMIN, STTMAX, SMTMIN, SMTMAX As Double
        Dim CodeERRProt As Short
        Dim TextERRProt As String = ""
        Dim EAOM2 As Double
        Dim SUMSNTMOY As Double
        Dim E As Double, ALPHAT As Double
        Dim IDIM As Integer, NDIM As Integer
        Dim NCCMAX As Integer, NCRMAX As Integer, NSIMAX As Integer
        Dim DET As Double, NEXP As Integer
        Dim SNTMOYIB As Double
        Dim NNT As Integer, NBT As Integer
        Dim OMTMAX As Double
        Dim CONVDOMT As Double
        Dim SNTOMT As Double
        Dim ALPHACRMIN As Double = 1.0E+20
        Dim NOSWAY As Boolean
        Dim SUMSNTOMT As Double
        Dim OM0IB As Double
        Dim POOM0 As Boolean

        Dim SUMEIL, SUMEAL, SUMRESB, SUMRESN, SUMSTRUCT, SUMOM0 As Double
        Dim CONSTANTMATRIX_TESTOK As Boolean




        TextERR = "?"
        CodeERR = 101


        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        '
        '             TRANSFERT DES DONNEES DES STRUCTURES VERS DES VARIABLES INTERNES
        '
        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        'Infos Matériau
        '--------------
        E = MAT.E
        ALPHAT = MAT.ALPHAT


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

        CodeERR = 102

        'Infos Barres
        '------------
        NBT = BARRES.NBT
        ReDim JEXB(2, NBT), SECT(NBT), XIN(NBT), RESB(2, NBT), OM0(NBT), NBTRONC(NBT)
        For I = 1 To NBT
            JEXB(1, I) = BARRES.JEXB(1, I)
            JEXB(2, I) = BARRES.JEXB(2, I)
            SECT(I) = BARRES.SECT(I)
            XIN(I) = BARRES.XIN(I)
            RESB(1, I) = BARRES.RESB(1, I)
            If RESB(1, I) = -1 Then RESB(1, I) = RIGID
            RESB(2, I) = BARRES.RESB(2, I)
            If RESB(2, I) = -1 Then RESB(2, I) = RIGID
            OM0(I) = BARRES.OM0(I)
            NBTRONC(I) = BARRES.NBTRONC(I)
        Next I

        CodeERR = 103

        'Nombre de cas de charge
        '-----------------------
        NCAS = MAXI(1, TYPANALYS.NCAS)

        'Dimensions maxi
        '---------------
        CodeERR = 104

        NBTRONCMAX = 0
        NCCMAX = 0
        NCRMAX = 0
        For K = 1 To NCAS
            For I = 1 To NBT
                If CH_BARRES.NBTR(K, I) > NBTRONCMAX Then NBTRONCMAX = CH_BARRES.NBTR(K, I)
                If CH_BARRES.NCC(K, I) > NCCMAX Then NCCMAX = CH_BARRES.NCC(K, I)
                If CH_BARRES.NCR(K, I) > NCRMAX Then NCRMAX = CH_BARRES.NCR(K, I)
            Next I
        Next K
        NCCMAX = NCCMAX + NBTRONCMAX + 1       'Nb maxi de charges concentrées
        NCRMAX = NCRMAX + 1                    'Nb maxi de charges réparties
        NSIMAX = 2 + 2 * NCCMAX + 2 * NCRMAX   'Nb maxi de sections d'intérêt

        If CodeERRProt <> 0 Then CodeERR = CodeERRProt : TextERR = TextERRProt : GoTo ERREUR

        CodeERR = 107

        IDIM = 3 * NNT

        'Dimensionnement de tableaux indépendants du cas de charge
        '---------------------------------------------------------
        ReDim NCOL(NNT)
        ReDim MAXA(IDIM + 1)
        Dim SBL(10) As Double
        Dim SBG(10) As Double
        ReDim SBLOC(10, NBT)
        ReDim XLONG(NBT)
        ReDim CX(NBT)
        ReDim CY(NBT)

        ReDim AML2ORDRELOC(6, NBT)

        'Longueurs et cosinus directeurs des barres
        '------------------------------------------
        For I = 1 To NBT
            XLONG(I) = Math.Sqrt((X(JEXB(2, I)) - X(JEXB(1, I))) ^ 2 + (Y(JEXB(2, I)) - Y(JEXB(1, I))) ^ 2)
            CX(I) = (X(JEXB(2, I)) - X(JEXB(1, I))) / XLONG(I)  '= Cos(Alpha)
            CY(I) = (Y(JEXB(2, I)) - Y(JEXB(1, I))) / XLONG(I)  '= Sin(Alpha)
        Next I

        'Paramètre de reconnaissance de structure (utile pour contrôle quand CONSTANTMATRIX=True)
        '----------------------------------------
        For I = 1 To NBT
            SUMEIL = SUMEIL + I * E * XIN(I) / XLONG(I)
            SUMEAL = SUMEAL + I * E * SECT(I) / XLONG(I)
            SUMRESB = SUMRESB + RESB(1, I) + 0.567 * RESB(2, I)
            SUMOM0 = SUMOM0 + I * OM0(I)
        Next I
        For I = 1 To NNT
            SUMRESN = RESN(1, I) + 0.123 * RESN(2, I) + +0.456 * RESN(3, I)
        Next
        SUMSTRUCT = SUMEAL + 0.789 * SUMEIL + SUMRESB / 1000000000000.0 + SUMRESN / 1000000000000.0 + 100000000.0 * SUMOM0

        CodeERR = 108

        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        '
        '                          MATRICE DE RIGIDITE DE LA STRUCTURE
        '
        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        'CALCULS RELATIFS AU STOCKAGE DE LA MATRICE DE RIGIDITE
        '-------------------------------------------------------
        Call STOCKAGE(NNT, NBT, JEXB, NCOL, NDIM, MAXA)

        CodeERR = 109

        ReDim S(NDIM)

        'sauvegarde pour CONSTANTMATRIX=True
        Static S_save(NDIM) As Double
        Static MAXA_save(IDIM + 1) As Long
        Static IDIM_save As Integer
        Static SBLOC_save(10, NBT) As Double
        Static SUMSTRUCT_save As Double

        CONSTANTMATRIX_TESTOK = TEST_CONSTANTMATRIX(SUMSTRUCT, SUMSTRUCT_save, MAXA, MAXA_save, IDIM, IDIM_save)

        If TYPANALYS.CONSTANTMATRIX = False Or CONSTANTMATRIX_TESTOK = False Then

            'TRAITEMENT DES SUPPORTS
            '-----------------------
            Call SUPPORTS(NNT, MAXA, RESN, S)

            CodeERR = 110

            If CodeERRProt <> 0 Then CodeERR = CodeERRProt : TextERR = TextERRProt : GoTo ERREUR

            'BOUCLE SUR LES BARRES
            '---------------------
            For IB = 1 To NBT

                'MATRICE DE LA BARRE DANS LE REPERE LOCAL  (ELASTIQUE LINEAIRE)
                '--------------------------------------------------------------
                SNTMOYIB = 0
                OM0IB = 0
                Call MATLOC(E, SECT(IB), XIN(IB), XLONG(IB), OM0IB, SNTMOYIB, RESB(1, IB), RESB(2, IB), SBL, IERREUR)
                If IERREUR <> 0 Then
                    CodeERR = 111   'barre instable
                    GoTo ERREUR
                End If

                For IE = 1 To 10
                    SBLOC(IE, IB) = SBL(IE)
                Next IE

                'MATRICE DE LA BARRE DANS LE REPERE GLOBAL
                '-----------------------------------------
                Call ROTAX_ANALYSE(CX(IB), CY(IB), SBL, SBG)

                'ASSEMBLAGE DANS LA MATRICE GLOBALE DE LA STRUCTURE
                '--------------------------------------------------
                Call ASSEMB(JEXB(1, IB), JEXB(2, IB), MAXA, SBG, S)

                CodeERR = 112

            Next IB

        End If

        'Dimensions des tableaux de résultats
        '------------------------------------

        With OUT_NOEUDS
            ReDim .DEPT(NCAS, 3, NNT)
            ReDim .REACT(NCAS, 3, NNT)
        End With

        With OUT_BARRES
            ReDim .OMT(NCAS, NBT)
            ReDim .ROTEX(NCAS, 2, NBT)
            ReDim .NSINT(NCAS, NBT)
            ReDim .NSINTRI(NCAS, NSIMAX, NBT)
            ReDim .XF(NCAS, NSIMAX, NBT)
            ReDim .SOLLIC(NCAS, 3, NSIMAX, NBT)
            ReDim .JSOLM(NCAS, 2, 3, NBT)
        End With

        With TYPANALYS
            ReDim .CONVERGEANA(NCAS)
            ReDim .ALPHACR(NCAS)
            ReDim .CONVERGEACR(NCAS)
        End With

        If CodeERRProt <> 0 Then CodeERR = CodeERRProt : TextERR = TextERRProt : GoTo ERREUR

        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        '
        '                                BOUCLE SUR LES CAS DE CHARGES
        '
        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        For ICAS = 1 To NCAS

            'Dimensionnement de tableaux dépendants du cas de charge
            '---------------------------------------------------------
            ReDim FN(IDIM)
            ReDim FNINIT(IDIM)
            ReDim DEPT(IDIM)
            ReDim REACT(IDIM)
            ReDim OMT(NBT)
            ReDim WIT(NBT)
            ReDim WJT(NBT)
            ReDim NSINT(NBT)
            ReDim AML(6, NBT)
            ReDim JNMIN(NBT)
            ReDim JNMAX(NBT)
            ReDim JTMIN(NBT)
            ReDim JTMAX(NBT)
            ReDim JMMIN(NBT)
            ReDim JMMAX(NBT)

            CodeERR = 113

            'Type d'analyse
            '--------------
            NBITERSOMIN = 6     'nb mini d'itérations pour analyse au 2d ordre
            NBITERSOCONV = 4    'nb d'itérations au delà duquel on commence à tester la convergence

            SECONDORDRE = TYPANALYS.SECONDORDRE(ICAS)
            NBITERSOMAX = MAXI(1, TYPANALYS.NBITERSOMAX(ICAS))
            If SECONDORDRE = True Then NBITERSOMAX = MAXI(NBITERSOMIN, NBITERSOMAX) 'NBITERSOMIN mini en second ordre
            EPSO = MAXI(0.00001, Math.Abs(TYPANALYS.TOLERSO(ICAS) / 100))
            TYPANALYS.CONVERGEANA(ICAS) = False
            TYPANALYS.CONVERGEACR(ICAS) = False
            ALPHACRMIN = 0
            NOSWAY = False

            'Infos Charges aux Noeuds
            '------------------------
            'transfert de charges aux noeuds à charges sur ddl
            IDDL = 0
            For n = 1 To NNT
                For DDL = 1 To 3
                    IDDL = IDDL + 1
                    FN(IDDL) = CH_NOEUDS.FN(ICAS, DDL, n)
                Next DDL
            Next n

            CodeERR = 114

            'Infos Charges sur Barres  (dans leur repère local)
            '--------------------------------------------------

            ReDim NCC(NBT), NCR(NBT), NBTRONCONS(NBT)
            For I = 1 To NBT
                NCC(I) = CH_BARRES.NCC(ICAS, I)
                NCR(I) = CH_BARRES.NCR(ICAS, I)
                NBTRONCONS(I) = CH_BARRES.NBTR(ICAS, I)
            Next I

            CodeERR = 115

            'Charges concentrées
            ReDim XFC(NCCMAX, NBT), FCX(NCCMAX, NBT), FCY(NCCMAX, NBT), FCZ(NCCMAX, NBT)
            For I = 1 To NBT
                For J = 1 To NCC(I)
                    XFC(J, I) = CH_BARRES.XFC(ICAS, J, I)
                    FCX(J, I) = CH_BARRES.FCX(ICAS, J, I)
                    FCY(J, I) = CH_BARRES.FCY(ICAS, J, I)
                    FCZ(J, I) = CH_BARRES.FCZ(ICAS, J, I)
                Next J
            Next I

            'Ajout des forces nulles pour sections d'intérêt supplémentaires
            For I = 1 To NBT
                For J = 1 To NBTRONCONS(I) - 1
                    XFC(NCC(I) + J, I) = J / NBTRONCONS(I)
                Next J
                NCC(I) = NCC(I) + NBTRONCONS(I) - 1
            Next I

            CodeERR = 116

            'Charges réparties
            ReDim XFR1(NCRMAX, NBT), FRX1(NCRMAX, NBT), FRY1(NCRMAX, NBT)
            ReDim XFR2(NCRMAX, NBT), FRX2(NCRMAX, NBT), FRY2(NCRMAX, NBT)
            ReDim TEMPB(NBT)
            For I = 1 To NBT
                For J = 1 To NCR(I)
                    XFR1(J, I) = CH_BARRES.XFR1(ICAS, J, I)
                    FRX1(J, I) = CH_BARRES.FRX1(ICAS, J, I)
                    FRY1(J, I) = CH_BARRES.FRY1(ICAS, J, I)
                    XFR2(J, I) = CH_BARRES.XFR2(ICAS, J, I)
                    FRX2(J, I) = CH_BARRES.FRX2(ICAS, J, I)
                    FRY2(J, I) = CH_BARRES.FRY2(ICAS, J, I)
                Next J
                TEMPB(I) = CH_BARRES.TEMPB(ICAS, I)
            Next I

            CodeERR = 117

            'Dimensionnement des tableaux liés aux sections d'intérêt
            '--------------------------------------------------------
            ReDim SNT(NSIMAX, NBT)
            ReDim SMT(NSIMAX, NBT)
            ReDim STT(NSIMAX, NBT)
            ReDim NSINT0(NSIMAX, NBT)
            ReDim XF(NSIMAX, NBT)
            ReDim SNX(NSIMAX, NBT)
            ReDim STX(NSIMAX, NBT)
            ReDim SMX(NSIMAX, NBT)

            CodeERR = 118

            '******************************************************************************************************************
            '                           TRAITEMENT DES CHARGES APPLIQUEES SUR LA BARRE
            '******************************************************************************************************************

            If CodeERRProt <> 0 Then CodeERR = CodeERRProt : TextERR = TextERRProt : GoTo ERREUR

            'Calcul des sections d'intérêt, charges équivalentes et sollicitations dans barre isostatique
            Call CHABAR(NBT, NCC, NCR, XFC, FCX, FCY, FCZ, XF, XFR1, XFR2, FRX1, FRX2, FRY1, FRY2,
                        XLONG, SECT, XIN, TEMPB, ALPHAT, SNX, STX, SMX, AML, JEXB,
                        FNCB, CX, CY, IDIM, NCCMAX, NCRMAX, NSIMAX, RESB, E, NSINT)

            '******************************************************************************************************************
            '                           CONSTITUTION DU VECTEUR CHARGES AUX NOEUDS
            '******************************************************************************************************************
            'Cumul des charges aux noeuds et des charges équivalentes aux charges sur barres
            For I = 1 To IDIM
                FN(I) = FN(I) + FNCB(I)
            Next I

            'stockage
            FNINIT = FN.Clone

            CodeERR = 119

            '******************************************************************************************************************
            '                                      RESOLUTION DU SYSTEME
            '******************************************************************************************************************

            'Triangularisation   (1er passage seulement (ICAS=1))
            '------------------

            If ICAS = 1 Then

                If TYPANALYS.CONSTANTMATRIX = False Or CONSTANTMATRIX_TESTOK = False Then

                    Call COLSOL(S, FN, MAXA, IDIM, 1, DET, NEXP)

                    S_save = S.Clone
                    MAXA_save = MAXA
                    SBLOC_save = SBLOC.Clone
                    IDIM_save = IDIM
                    SUMSTRUCT_save = SUMSTRUCT

                    CodeERR = 120

                    'Test déterminant
                    If DET <= 0 Then
                        CodeERR = 121  'Structure instable
                        GoTo ERREUR
                    End If

                Else

                    S = S_save.Clone
                    SBLOC = SBLOC_save.Clone

                End If

            End If


            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            '
            '                                   C A L C U L   D E    A L P H A C R 
            '
            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

            ReDim SNTMOY(NBT), OMTMOY(NBITERSOMAX), ALPHACR(NBITERSOMAX), OMTIBSNTOMTMAX(NBITERSOMAX)
            Dim EPSALPHACR As Double = 0.001
            Dim IBSNTOMTMAX As Integer
            Dim SNTMOYOMTMAX As Double

            If TYPANALYS.GETALPHACR(ICAS) Then

                '==============================================================================================================
                'BOUCLE SUR LES ITERATIONS
                '==============================================================================================================
                For ITERSO = 1 To NBITERSOMAX

                    'Back substitution
                    '-----------------
                    Call COLSOL(S, FN, MAXA, IDIM, 2, DET, NEXP)

                    CodeERR = 122

                    'Récupération du vecteur déplacements
                    '------------------------------------
                    For I = 1 To IDIM
                        DEPT(I) = FN(I)
                    Next I

                    'On vérifie qu'aucune rotation ne dépasse pas 10 rad (TMN: 1rd => 10 rd)
                    '--------------------------------------------------
                    For I = 3 To IDIM Step 3
                        If Math.Abs(DEPT(I)) > 10 Then  '> 10 rad!!
                            TYPANALYS.ALPHACR(ICAS) = 0
                            CodeERR = 123   'Structure instable
                            GoTo ERREUR
                        End If
                    Next I

                    CodeERR = 124

                    'Sollicitations dans les barres et rotations globales (avec matrice de rigidité SBLOC linéaire élastique)
                    'Si ITERSO=1, on récupère SNT et OMT
                    'Si ITERSO>1, on ne récupère que OMT
                    '--------------------------------------------------------------------------------------------------------
                    Call SOLLICIT(NBT, JEXB, XLONG, DEPT, CX, CY, SBLOC, SNT, STT, SMT, OMT, AML, NSINT, SNX, STX, SMX, NSIMAX, XF)

                    CodeERR = 125

                    'Calcul de alphacr et Test de convergence
                    '========================================

                    If ITERSO = 1 Then

                        'Calcul de l'effort normal moyen dans chaque barre et test de compression significative
                        '--------------------------------------------------------------------------------------
                        Dim compressionexiste As Boolean = False
                        Dim fy As Double = E / 1000 'ordre de grandeur de fy
                        For I = 1 To NBT
                            SNTMOY(I) = (SNT(1, I) + SNT(2, I)) / 2   '<0 si compression
                            If SNTMOY(I) < -SECT(I) * fy / 500 Then 'N>Npl/500 en compression --> comprssion significative
                                compressionexiste = True
                            End If
                        Next I

                        'Si pas de compression significative --> on sort
                        '-----------------------------------------------
                        If compressionexiste = False Then
                            ALPHACRMIN = 1.0E+20
                            Exit For
                        End If

                        'ajout d'une "perturbation" aux rotations globales des barres comprimées
                        '--> on leur ajoute une rotation globale identique
                        '-----------------------------------------------------------------------
                        OM0IB = 0.0001
                        For I = 1 To NBT
                            If OMT(I) + OM0IB <> 0 Then
                                OMT(I) = OMT(I) + OM0IB
                            Else
                                OMT(I) = OMT(I) + 1.2 * OM0IB
                            End If
                        Next I

                        'calcul de la rotation globale moyenne des barres comprimées pondérées par leur effort normal
                        'pour donner plus de "poids" à leur rotation
                        '--------------------------------------------------------------------------------------------
                        SUMSNTOMT = 0
                        SUMSNTMOY = 0
                        For I = 1 To NBT
                            If SNTMOY(I) < 0 Then  'barre comprimée
                                SUMSNTOMT = SUMSNTOMT + Math.Abs(SNTMOY(I) * OMT(I))
                                SUMSNTMOY = SUMSNTMOY + Math.Abs(SNTMOY(I))
                            End If
                        Next I
                        OMTMOY(ITERSO) = SUMSNTOMT / SUMSNTMOY

                        'alphacr - initialisation
                        ALPHACR(ITERSO) = 1.0E+20

                    Else 'ITERSO>1

                        'calcul de la rotation globale moyenne des barres comprimées pondérées par leur effort normal
                        'pour donner plus de "poids" à leur rotation
                        '--------------------------------------------------------------------------------------------
                        SUMSNTOMT = 0
                        SUMSNTMOY = 0
                        For I = 1 To NBT
                            If SNTMOY(I) < 0 Then  'barre comprimée
                                SUMSNTOMT = SUMSNTOMT + Math.Abs(SNTMOY(I) * OMT(I))
                                SUMSNTMOY = SUMSNTMOY + Math.Abs(SNTMOY(I))
                            End If
                        Next I
                        OMTMOY(ITERSO) = SUMSNTOMT / SUMSNTMOY

                        'alphacr de cette itération
                        '--------------------------
                        If ITERSO > 2 Then
                            ALPHACR(ITERSO) = OMTMOY(ITERSO - 1) / OMTMOY(ITERSO) * ALPHACR(ITERSO - 1)
                        Else
                            ALPHACR(ITERSO) = OMTMOY(ITERSO - 1) / OMTMOY(ITERSO)
                        End If

                        'Repérage de la barre comprimée ayant le SNTMOY*OMT maxi (seulement 1 fois) --> Barre IBSNTOMTMAX
                        '------------------------------------------------------------------------------------------------
                        If ITERSO = 2 Then
                            IBSNTOMTMAX = 1
                            SNTMOYOMTMAX = 0
                            For I = 1 To NBT
                                If SNTMOY(I) < 0 Then
                                    If Math.Abs(SNTMOY(I) * OMT(I)) > SNTMOYOMTMAX Then
                                        IBSNTOMTMAX = I
                                        SNTMOYOMTMAX = Math.Abs(SNTMOY(I) * OMT(I))
                                    End If
                                End If
                            Next I
                        End If


                        'Repérage de la rotation de la barre IBSNTOMTMAX pour détecter un changement éventuel de signe à chaque itération
                        '(pb de détection de convergence vers un ALPHACR négatif)
                        '-------------------------------------------------------------------------------------------
                        OMTIBSNTOMTMAX(ITERSO) = OMT(IBSNTOMTMAX)

                        '=========================================================================================
                        '                           TEST DE CONVERGENCE DE ALPHACR
                        '=========================================================================================
                        If ITERSO > NBITERSOCONV Then

                            If Math.Abs(ALPHACR(ITERSO - 1) / ALPHACR(ITERSO) - 1) < EPSALPHACR Then 'CONVERGENCE ATTEINTE!!!

                                If OMTIBSNTOMTMAX(ITERSO - 1) * OMTIBSNTOMTMAX(ITERSO) < 0 Then
                                    'on tend en réalité vers un ALPHACR négatif
                                    ALPHACRMIN = 1.0E+20
                                Else
                                    'on tend vers un ALPHACR positif
                                    ALPHACRMIN = (1 - EPSALPHACR) * ALPHACR(ITERSO)
                                End If
                                TYPANALYS.CONVERGEACR(ICAS) = True
                                Exit For '--> on sort

                            ElseIf ITERSO = NBITERSOMAX Then 'on atteint le nb d'itérations maxi!!!!

                                If OMTIBSNTOMTMAX(ITERSO - 1) * OMTIBSNTOMTMAX(ITERSO) < 0 Then
                                    'on tend en réalité vers un ALPHACR négatif
                                    ALPHACRMIN = 1.0E+20
                                Else
                                    'on tend vers un ALPHACR positif
                                    ALPHACRMIN = (ALPHACR(ITERSO) + ALPHACR(ITERSO - 1)) / 2
                                End If
                                TYPANALYS.CONVERGEACR(ICAS) = False
                                Exit For '--> on sort

                            End If

                        End If
                        '=========================================================================================

                    End If   'If ITERSO = 1

                    'convergence non atteinte --> itération suivante

                    'Ajout des charges équivalentes aux effets du 2d ordre globaux (SNT*OMT)
                    '-----------------------------------------------------------------------
                    ReDim FN(IDIM) 'on réinitialise FN
                    For I = 1 To NBT
                        If ITERSO = 1 Then
                            SNTOMT = SNTMOY(I) * OMT(I)
                        Else
                            SNTOMT = SNTMOY(I) * OMT(I) * ALPHACR(ITERSO)
                        End If
                        FN(3 * (JEXB(1, I) - 1) + 1) = FN(3 * (JEXB(1, I) - 1) + 1) - SNTOMT * CY(I)
                        FN(3 * (JEXB(1, I) - 1) + 2) = FN(3 * (JEXB(1, I) - 1) + 2) + SNTOMT * CX(I)
                        FN(3 * (JEXB(2, I) - 1) + 1) = FN(3 * (JEXB(2, I) - 1) + 1) + SNTOMT * CY(I)
                        FN(3 * (JEXB(2, I) - 1) + 2) = FN(3 * (JEXB(2, I) - 1) + 2) - SNTOMT * CX(I)
                    Next I

                Next ITERSO
                '==============================================================================================================




            Else  'PAS DE CALCUL DE ALPHACR DEMANDE

                ALPHACRMIN = 0

            End If    'If TYPANALYS.GETALPHACR(ICAS)

            'XXXXXXXXXXXXXXXXXXXXXXXXXXX   FIN CALCUL ALPHACR   XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX


            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            '
            '                                   A N A L Y S E 
            '
            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

            If SECONDORDRE = True Then
                NBITERSO = NBITERSOMAX
                ACONVERGE = False
                POOM0 = False
            Else  '1er ordre
                NBITERSO = 1
                ACONVERGE = True
                POOM0 = False
                '1er ordre + OM0??
                For I = 1 To NBT
                    If OM0(I) <> 0 Then
                        POOM0 = True
                        NBITERSO = 2   'on rajoute une itération pour introduire l'effet de OM0+N
                        Exit For
                    End If
                Next I
            End If


            'Reconstitution du vecteur charges aux noeuds + charges équivalentes à charges sur barres 
            '---------------------------------------------------------------------------------------- 
            FN = FNINIT.Clone

            '==================================================================================================================
            'BOUCLE SUR LES ITERATIONS
            '==================================================================================================================
            For ITERSO = 1 To NBITERSO

                'Back substitution
                '-----------------
                Call COLSOL(S, FN, MAXA, IDIM, 2, DET, NEXP)

                CodeERR = 122

                'Récupération du vecteur déplacements
                '------------------------------------
                For I = 1 To IDIM
                    DEPT(I) = FN(I)
                Next I

                'On vérifie qu'aucune rotation ne dépasse pas 10 rad
                '--------------------------------------------------
                For I = 3 To IDIM Step 3
                    If Math.Abs(DEPT(I)) > 10 Then  '> 10 rad!!                        
                        CodeERR = 123   'Structure instable
                        GoTo ERREUR
                    End If
                Next I

                CodeERR = 124

                'Sollicitations dans les barres et rotations globales (avec matrice rigidité SBLOC élastique linéaire)
                '----------------------------------------------------
                Call SOLLICIT(NBT, JEXB, XLONG, DEPT, CX, CY, SBLOC, SNT, STT, SMT,
                              OMT, AML, NSINT, SNX, STX, SMX, NSIMAX, XF)

                CodeERR = 125

                'TRAITEMENT ANALYSE AU SECOND ORDRE
                '==================================
                If SECONDORDRE = True Or POOM0 = True Then   'soit 2d ordre demandé, soit 1er ordre avec OM0

                    'Test de convergence
                    '===================

                    If ITERSO = 1 Then   '1ère itération

                        'initialisations
                        ReDim OMTSO(NBT)

                        'calcul de OMTMAX
                        '----------------
                        OMTMAX = 0
                        For I = 1 To NBT
                            If Math.Abs(OMT(I)) > OMTMAX Then OMTMAX = Math.Abs(OMT(I))
                        Next I
                        If OMTMAX < 0.000001 Then OMTMAX = 0.000001

                    Else 'ITERSO>1

                        '==================================================================================================================
                        'Correction des sollicitations - Retrait des charges équivalentes ayant généré les effets du second ordre
                        '==================================================================================================================

                        'Correction de l'effort normal si ITERSO>1 due aux effets 0.5*EA*OMT^2 + EA*OMT*OM0  (à faire avant correction STT!!)
                        '----------------------------------------------------------------------------------
                        For I = 1 To NBT
                            If POOM0 = False Then   ' <=> SECONDORDRE=True
                                EAOM2 = 0.5 * E * SECT(I) * OMTSO(I) ^ 2 + E * SECT(I) * OMTSO(I) * OM0(I)
                            Else
                                EAOM2 = 0.5 * E * SECT(I) * OM0(I) ^ 2  'revient à faire OM0=0 et OMTSO=OM0
                            End If
                            For J = 1 To NSINT(I)
                                SNT(J, I) = SNT(J, I) + EAOM2
                            Next J
                        Next I

                        'Correction de l'effort tranchant si ITERSO>1 due aux effets SNT*(OMT+OM0)
                        '-------------------------------------------------------------------------
                        For I = 1 To NBT
                            If POOM0 = False Then   ' <=> SECONDORDRE=True
                                SNTOMT = 0.5 * (SNT(1, I) + SNT(2, I)) * (OMTSO(I) + OM0(I))
                            Else
                                SNTOMT = 0.5 * (SNT(1, I) + SNT(2, I)) * OM0(I)  'revient à faire OM0=0 et OMTSO=OM0
                            End If
                            For J = 1 To NSINT(I)
                                STT(J, I) = STT(J, I) + SNTOMT
                            Next J
                        Next I

                        If POOM0 = False Then  ' <=> SECONDORDRE=True

                            'Correction des efforts si ITERSO>1 due aux effets 2d ordre locaux
                            '-----------------------------------------------------------------
                            For I = 1 To NBT
                                SNT(1, I) = SNT(1, I) - AML2ORDRELOC(1, I)
                                STT(1, I) = STT(1, I) - AML2ORDRELOC(2, I)
                                SMT(1, I) = SMT(1, I) - AML2ORDRELOC(3, I)
                                SNT(2, I) = SNT(2, I) + AML2ORDRELOC(4, I)
                                STT(2, I) = STT(2, I) + AML2ORDRELOC(5, I)
                                SMT(2, I) = SMT(2, I) + AML2ORDRELOC(6, I)
                                For J = 3 To NSINT(I)
                                    SNT(J, I) = SNT(J, I) + AML2ORDRELOC(4, I)
                                    STT(J, I) = STT(J, I) + AML2ORDRELOC(5, I)
                                    SMT(J, I) = SMT(J, I) + (AML2ORDRELOC(6, I) + (-AML2ORDRELOC(3, I) - AML2ORDRELOC(6, I)) * (1 - XF(J, I)))
                                Next J
                            Next I

                        Else   '1er ordre + OM0

                            Exit For '--> Fin des itérations à l'itération 2

                        End If

                        '==================================================================================================================
                        '                 TEST DE CONVERGENCE SUR LES ROTATIONS GLOBALES SIGNIFICATIVES
                        '==================================================================================================================
                        ACONVERGE = True
                        For I = 1 To NBT
                            'Test d'instabilité
                            '------------------
                            If Math.Abs(OMT(I)) > 1 Then  'OMT > 1 rad  ===========> INSTABILITE
                                ACONVERGE = False
                                TYPANALYS.CONVERGEANA(ICAS) = ACONVERGE
                                If TYPANALYS.GETALPHACR(ICAS) = True Then
                                    TYPANALYS.ALPHACR(ICAS) = ALPHACRMIN
                                Else
                                    TYPANALYS.ALPHACR(ICAS) = -1   'on indique une instabilité si ALPHACR n'a pas été calculé avant
                                End If
                                GoTo nextICAS  ' --->  cas suivant   
                            End If
                            'Test de convergence
                            '-------------------
                            If Math.Abs(OMT(I)) > OMTMAX / 20 Then
                                CONVDOMT = Math.Abs((OMT(I) - OMTSO(I)) / OMTSO(I))
                                If CONVDOMT > EPSO Then
                                    ACONVERGE = False
                                End If
                            End If
                        Next I
                        If ACONVERGE = True Then Exit For 'For ITERSO = 1 To NBITERSO     =========> OK
                        '
                        '==================================================================================================================

                    End If   'If ITERSO = 1

                    'Non encore convergé!!!!!!  --> itération suivante

                    'stockage OMT itération courante 
                    '------------------------------- 
                    OMTSO = OMT.Clone

                    'Reconstitution du vecteur charges aux noeuds + charges équivalentes à charges sur barres 
                    '---------------------------------------------------------------------------------------- 
                    FN = FNINIT.Clone

                    '==================================================================================================================
                    ' Correction du vecteur charges aux noeuds - Ajout des charges équivalentes aux effets du second ordre
                    '==================================================================================================================
                    '
                    '                       | EA*OMT^2/2 + EA*OMT*OM0
                    '                       |
                    '                       v
                    '                        <----- SNT*(OMT+OM0)
                    '                       |      
                    '                       |
                    '                       |
                    '                       |
                    '                       |
                    '                       |
                    '                       |
                    '                       |
                    '                       |
                    '                       |
                    '   SNT*(OMT+OM0) -----> 
                    '                       ^
                    '                       |
                    '                       | EA*OMT^2/2 + EA*OMT*OM0
                    '                   
                    '
                    'Ajout des charges équivalentes aux effets du 2d ordre globaux 
                    '------------------------------------------------------------- 
                    'effets dus à N+OMT
                    For I = 1 To NBT
                        If POOM0 = False Then ' <=> SECONDORDRE=True
                            SNTOMT = 0.5 * (SNT(1, I) + SNT(2, I)) * (OMT(I) + OM0(I))
                        Else  ' <=> 1er Ordre + OM0
                            SNTOMT = 0.5 * (SNT(1, I) + SNT(2, I)) * OM0(I)  'revient à faire OM0=0 et OMT=OM0
                        End If
                        FN(3 * (JEXB(1, I) - 1) + 1) = FN(3 * (JEXB(1, I) - 1) + 1) - SNTOMT * CY(I)
                        FN(3 * (JEXB(1, I) - 1) + 2) = FN(3 * (JEXB(1, I) - 1) + 2) + SNTOMT * CX(I)
                        FN(3 * (JEXB(2, I) - 1) + 1) = FN(3 * (JEXB(2, I) - 1) + 1) + SNTOMT * CY(I)
                        FN(3 * (JEXB(2, I) - 1) + 2) = FN(3 * (JEXB(2, I) - 1) + 2) - SNTOMT * CX(I)
                    Next I

                    'effets dus à OMT+OM0 seuls
                    For I = 1 To NBT
                        If POOM0 = False Then  ' <=> SECONDORDRE=True
                            EAOM2 = 0.5 * E * SECT(I) * OMT(I) ^ 2 + E * SECT(I) * OMT(I) * OM0(I)
                        Else
                            EAOM2 = 0.5 * E * SECT(I) * OM0(I) ^ 2   'revient à faire OM0=0 et OMT=OM0
                        End If
                        FN(3 * (JEXB(1, I) - 1) + 1) = FN(3 * (JEXB(1, I) - 1) + 1) + EAOM2 * CX(I)
                        FN(3 * (JEXB(1, I) - 1) + 2) = FN(3 * (JEXB(1, I) - 1) + 2) + EAOM2 * CY(I)
                        FN(3 * (JEXB(2, I) - 1) + 1) = FN(3 * (JEXB(2, I) - 1) + 1) - EAOM2 * CX(I)
                        FN(3 * (JEXB(2, I) - 1) + 2) = FN(3 * (JEXB(2, I) - 1) + 2) - EAOM2 * CY(I)
                    Next I

                    If POOM0 = False Then  ' <=> SECONDORDRE=True

                        'Rotations aux extrémités des barres
                        '-----------------------------------
                        Call ROTEXB(NBT, SMT, CX, CY, JEXB, DEPT, E, SECT, XIN, XLONG, OMT, RESB, WIT, WJT, IERREUR)
                        If IERREUR <> 0 Then
                            CodeERR = 126
                            GoTo ERREUR
                        End If

                        'Ajout des charges équivalentes aux effets du 2d ordre locaux dans les barres "discrétisées" en NBTRONC tronçons
                        '--------------------------------------------------------------------------------------------------------------- 
                        Call CHABAR2ORDRELOC(NBT, NBTRONC, XLONG, JEXB, SECT, XIN, OMT, WIT, WJT, DEPT, FN2ORDRELOC, E, SNT, CX, CY, IDIM, RESB, AML2ORDRELOC)
                        For N = 1 To NNT
                            FN(3 * (N - 1) + 1) = FN(3 * (N - 1) + 1) + FN2ORDRELOC(3 * (N - 1) + 1)
                            FN(3 * (N - 1) + 2) = FN(3 * (N - 1) + 2) + FN2ORDRELOC(3 * (N - 1) + 2)
                            FN(3 * (N - 1) + 3) = FN(3 * (N - 1) + 3) + FN2ORDRELOC(3 * (N - 1) + 3)
                        Next N

                    End If

                End If   'If SECONDORDRE = True

            Next ITERSO

            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX


            '==========================================================================================================================
            '                                            RESULTATS COMPLEMENTAIRES
            '==========================================================================================================================

            'N° Sections des sollicitations extrêmes
            '---------------------------------------
            For I = 1 To NBT
                JNMIN(I) = 0
                JNMAX(I) = 0
                JTMIN(I) = 0
                JTMAX(I) = 0
                JMMIN(I) = 0
                JMMAX(I) = 0
                SNTMIN = 1.0E+30
                SNTMAX = -1.0E+30
                STTMIN = 1.0E+30
                STTMAX = -1.0E+30
                SMTMIN = 1.0E+30
                SMTMAX = -1.0E+30
                For J = 1 To NSINT(I)
                    If SNT(J, I) < 0 And SNT(J, I) < SNTMIN Then SNTMIN = SNT(J, I) : JNMIN(I) = J
                    If SNT(J, I) > 0 And SNT(J, I) > SNTMAX Then SNTMAX = SNT(J, I) : JNMAX(I) = J
                    If STT(J, I) < 0 And STT(J, I) < STTMIN Then STTMIN = STT(J, I) : JTMIN(I) = J
                    If STT(J, I) > 0 And STT(J, I) > STTMAX Then STTMAX = STT(J, I) : JTMAX(I) = J
                    If SMT(J, I) < 0 And SMT(J, I) < SMTMIN Then SMTMIN = SMT(J, I) : JMMIN(I) = J
                    If SMT(J, I) > 0 And SMT(J, I) > SMTMAX Then SMTMAX = SMT(J, I) : JMMAX(I) = J
                Next J
            Next I

            'Rotations aux extrémités des barres
            '-----------------------------------
            Call ROTEXB(NBT, SMT, CX, CY, JEXB, DEPT, E, SECT, XIN, XLONG, OMT, RESB, WIT, WJT, IERREUR)
            If IERREUR <> 0 Then
                CodeERR = 126
                GoTo ERREUR
            End If

            'Réactions d'appuis
            '------------------
            Call REACTS(NNT, DEPT, RESN, REACT)

            CodeERR = 127

            'Classement des sections d'intérêt par ordre croissant d'abscisses
            '-----------------------------------------------------------------
            Call SORSEC(NBT, NSIMAX, SNT, STT, SMT, NSINT, XF, NSINT0)

            CodeERR = 128

            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            '
            '                            TRANSFERT DES RESULTATS DANS LES STRUCTURES
            '
            'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

            If CodeERRProt <> 0 Then CodeERR = CodeERRProt : TextERR = TextERRProt : GoTo ERREUR

            For I = 1 To NNT
                OUT_NOEUDS.DEPT(ICAS, 1, I) = DEPT(3 * (I - 1) + 1)
                OUT_NOEUDS.DEPT(ICAS, 2, I) = DEPT(3 * (I - 1) + 2)
                OUT_NOEUDS.DEPT(ICAS, 3, I) = DEPT(3 * (I - 1) + 3)
                OUT_NOEUDS.REACT(ICAS, 1, I) = REACT(3 * (I - 1) + 1)
                OUT_NOEUDS.REACT(ICAS, 2, I) = REACT(3 * (I - 1) + 2)
                OUT_NOEUDS.REACT(ICAS, 3, I) = REACT(3 * (I - 1) + 3)
            Next I

            CodeERR = 129

            For I = 1 To NBT
                OUT_BARRES.OMT(ICAS, I) = OMT(I)
                OUT_BARRES.ROTEX(ICAS, 1, I) = -WIT(I)
                OUT_BARRES.ROTEX(ICAS, 2, I) = -WJT(I)
                OUT_BARRES.NSINT(ICAS, I) = NSINT(I)
                For J = 1 To NSINT(I)
                    OUT_BARRES.NSINTRI(ICAS, J, I) = NSINT0(J, I)
                    OUT_BARRES.XF(ICAS, J, I) = XF(J, I)
                    OUT_BARRES.SOLLIC(ICAS, 1, J, I) = SNT(J, I)
                    OUT_BARRES.SOLLIC(ICAS, 2, J, I) = STT(J, I)
                    OUT_BARRES.SOLLIC(ICAS, 3, J, I) = SMT(J, I)
                Next J
            Next I

            For I = 1 To NBT
                OUT_BARRES.JSOLM(ICAS, 1, 1, I) = JNMIN(I)
                OUT_BARRES.JSOLM(ICAS, 2, 1, I) = JNMAX(I)
                OUT_BARRES.JSOLM(ICAS, 1, 2, I) = JTMIN(I)
                OUT_BARRES.JSOLM(ICAS, 2, 2, I) = JTMAX(I)
                OUT_BARRES.JSOLM(ICAS, 1, 3, I) = JMMIN(I)
                OUT_BARRES.JSOLM(ICAS, 2, 3, I) = JMMAX(I)
            Next I

            TYPANALYS.CONVERGEANA(ICAS) = ACONVERGE
            TYPANALYS.ALPHACR(ICAS) = ALPHACRMIN

            CodeERR = 130
nextICAS:

        Next ICAS
        'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        CodeERR = 0
        TextERR = "OK"

        Exit Sub


        '=================================================================================================================================================================
        'PROBLEME
        '=================================================================================================================================================================
ERREUR:
        Select Case CodeERR
            Case 111 : TextERR = "Barre " & CStr(IB) & " instable"
            Case 121 : TextERR = "Structure instable - Déterminant non positif"
            Case 123 : TextERR = "Structure instable - Rotation noeud " & CStr(Int((I + 0.00001) / 3)) & " excessive"
            Case 126 : TextERR = "Barre instable trouvée"
            Case Else : TextERR = "Erreur " & CStr(Err.Number) & " : " & Err.Description
        End Select


    End Sub

    Sub SOLLICIT(ByRef NBT As Integer, ByRef JEXB(,) As Integer, ByRef XLONG() As Double, ByRef DEP() As Double, ByRef CX() As Double, ByRef CY() As Double, ByRef SBLOC(,) As Double,
               ByRef SN(,) As Double, ByRef ST(,) As Double, ByRef SM(,) As Double, ByRef OM() As Double, ByRef AML(,) As Double, ByRef NSINT() As Integer, ByRef SNX(,) As Double, ByRef STX(,) As Double,
               ByRef SMX(,) As Double, ByRef NSIMAX As Integer, ByRef XF(,) As Double)
        '----------------------------------------------------------------------
        '     SOLLICITATIONS DANS LES BARRES ET ROTATIONS GLOBALES
        '----------------------------------------------------------------------
        ' -ENTREES:
        '     NBT          : nombre de barres
        '     JEXB(E, b)   : numéros d'extrémités des barres
        '     XLONG(b)     : longueur des barres
        '     CX,CY        : cosinus directeurs des barres
        '     SBLOC(e,b)   : matrices de rigidités locales des barres
        '     DEP(d)       : vecteur déplacements dans repère global
        '     IDIM         : nb de ddl
        '     AML(ddl,b)   : opposées des charges équivalentes aux charges/barre
        '     NSINT(b)     : nombre de sections d'intérêt dans la barre b
        '     NSIMAX       : nombre maxi de sections d'intérêt par barre
        '     SNX(s,b)     : |
        '     STX(s,b)     : |sollicitations dans la section d'intérêt s dans b
        '     SMX(s,b)     : |
        '     XF(s,b)      : position de la section d'intérêt s dans la barre b
        '
        ' -SORTIES:
        '     SN(s,b)      : |
        '     ST(s,b)      : | sollicitations dans sections d'intérêt
        '     SM(s,b)      : | correspondant aux déplacements DEP
        '     OM(b)        : rotations globales correspondant à DEP
        '----------------------------------------------------------------------

        '      DIMENSION JEXB(2, NBT), XLONG(NBT), CX(NBT), CY(NBT), SBLOC(10, NBT)
        '      DIMENSION SN(NSIMAX, NBT), ST(NSIMAX, NBT), SM(NSIMAX, NBT)
        '      DIMENSION DEP(IDIM))
        '      DIMENSION OM(NBT), AML(6, NBT), NSINT(NBT), XF(NSIMAX, NBT)
        '      DIMENSION SNX(NSIMAX, NBT), STX(NSIMAX, NBT), SMX(NSIMAX, NBT)

        Dim SBL(10) As Double, DEPL(6) As Double
        Dim XL, T1, T2, T3, T4, SX1, SY1, SZ1, SZ2 As Double

        '-Boucle sur les barres
        '----------------------
        For I = 1 To NBT

            XL = XLONG(I)

            For I1 = 1 To 10
                SBL(I1) = SBLOC(I1, I)
            Next I1

            '-Déplacements d'extrémités en local
            Call DEPLOC(CX(I), CY(I), JEXB(1, I), JEXB(2, I), DEP, DEPL)

            '-Rotations globales
            OM(I) = (DEPL(5) - DEPL(2)) / XL

            '-Produit : SBLOC * DEPL
            T1 = DEPL(4) - DEPL(1)
            T2 = DEPL(5) - DEPL(2) - DEPL(6) * XL
            T3 = DEPL(6) - DEPL(3)
            T4 = T2 + DEPL(6) * XL
            SX1 = SBL(1) * T1 + SBL(4) * T4 - SBL(5) * DEPL(3) - SBL(7) * DEPL(6)
            SY1 = SBL(4) * T1 + SBL(2) * T4 - SBL(6) * DEPL(3) - SBL(8) * DEPL(6)
            SZ1 = SBL(5) * T1 + SBL(6) * T4 - SBL(3) * DEPL(3) - SBL(9) * DEPL(6)
            SZ2 = -SBL(7) * T1 - SBL(8) * T4 + SBL(9) * DEPL(3) + SBL(10) * DEPL(6)

            '-Sollicitations aux extrémités
            SN(1, I) = SX1 - AML(1, I)
            SN(2, I) = SX1 + AML(4, I)
            ST(1, I) = SY1 - AML(2, I)
            ST(2, I) = SY1 + AML(5, I)
            SM(1, I) = SZ1 - AML(3, I)
            SM(2, I) = SZ2 + AML(6, I)

            '-Sollicitations dans les sections d'intérêt
            If NSINT(I) > 2 Then
                For J = 3 To NSINT(I)
                    SN(J, I) = SN(2, I) + SNX(J, I)
                    ST(J, I) = ST(2, I) + STX(J, I)
                    'SM(J, I) = SM(2, I) + SMX(J, I) + ST(2, I) * XL * (1 - XF(J, I))
                    SM(J, I) = SM(2, I) + SMX(J, I) + (SM(1, I) - SM(2, I) - SMX(1, I)) * (1 - XF(J, I))
                Next J
            End If

        Next I

    End Sub

    Sub REACTS(ByRef NNT As Integer, ByRef DEP() As Double, ByRef RESN(,) As Double, ByRef REAC() As Double)
        '-----------------------------------------------------------------------
        '     CALCUL DES REACTIONS D'APPUIS
        '-----------------------------------------------------------------------
        ' -ENTREES:
        '     DEP(d)       : déplacements dans repère global
        '     RESN(d, n)   : ressorts d'encastrement du noeud n suivant d
        '     NNT          : nb de noeuds
        '
        ' -SORTIES:
        '     REAC(d)      : réactions d'appuis suivant chaque ddl
        '-----------------------------------------------------------------------

        '      DIMENSION DEP(IDIM), RESN(3, NNT), REAC(IDIM)

        For I = 1 To NNT
            For J = 1 To 3
                REAC(3 * (I - 1) + J) = -RESN(J, I) * DEP(3 * (I - 1) + J)
            Next J
        Next I

    End Sub

    Sub CHABAR(ByRef NBT As Integer, ByRef NCC() As Integer, ByRef NCR() As Integer, ByRef XFC(,) As Double, ByRef FCX(,) As Double, ByRef FCY(,) As Double,
                ByRef FCZ(,) As Double, ByRef XF(,) As Double, ByRef XFR1(,) As Double, ByRef XFR2(,) As Double, ByRef FRX1(,) As Double,
                ByRef FRX2(,) As Double, ByRef FRY1(,) As Double, ByRef FRY2(,) As Double,
                ByRef XLONG() As Double, ByRef SECT() As Double, ByRef XIN() As Double, ByRef TEMPB() As Double, ByRef ALPHAT As Double,
                ByRef SNX(,) As Double, ByRef STX(,) As Double, ByRef SMX(,) As Double, ByRef AML(,) As Double, ByRef JEXB(,) As Integer,
                ByRef FNCB() As Double, ByRef CX() As Double, ByRef CY() As Double, ByRef IDIM As Integer, ByRef NCCMAX As Integer, ByRef NCRMAX As Integer,
                ByRef NSIMAX As Integer, ByRef RESB(,) As Double, ByRef E As Double, ByRef NSINT() As Integer)
        '-----------------------------------------------------------------------
        '     CALCUL DES CHARGES EQUIVALENTES POUR LES BARRES CHARGEES
        '-----------------------------------------------------------------------
        ' -ENTREES:
        '     NBT          : nombre total de barres
        '     NCC(b)       : nombre de charges concentrées sur la barre b
        '     NCR(b)       : nombre de charges réparties   sur la barre b
        '     XFC(c,b)     : position fraction. de la charge conc. c dans b
        '     FCX(c,b)     : |
        '     FCY(c,b)     : |composantes de la charge conc. c dans b
        '     FCZ(c,b)     : |
        '     XFR1(r,b)    : position fract. de l'origine de la charge r‚par. r
        '     XFR2(r,b)    : position fract. de l'extrém. de la charge r‚par. r
        '     FRX1(r,b)    : |
        '     FRY1(r,b)    : |composantes de la charge répartie r de la barre b
        '     FRX2(r,b)    : |à son origine et à son extrémité
        '     FRY2(r,b)    : |
        '     XLONG(b)     : longueur de la barre b
        '     SECT(b)      : section de la barre b
        '     XIN(b)       : inertie de la barre b
        '     E            : module d'élasticité
        '     TEMPB(b)     : ‚cart temp‚rature de la barre b (uniforme et en øC)
        '     ALPHAT       : coefficient de dilatation thermique du mat‚riau
        '     JEXB(2, b)   : numéros d'extrémités de la barre b
        '     RESB(2,b)    : ressorts de liaison aux extrémités de b
        '     CX,CY(b)     : cosinus directeurs de la barre b
        '     IDIM         : nombre total de ddl
        '     NCCMAX       : nombre maxi de charges concentrées par barre
        '     NCRMAX       : nombre maxi de charges réparties   par barre
        '     NSIMAX       : nombre maxi de sections d'intérêt  par barre
        '     FN(d)        : vecteur charges/noeuds global initial
        '
        ' -SORTIES:
        '     XFI,FX,FY,FZ : tableaux de travail
        '     XF(s,b)      : position fraction. de la section d'intérêt s dans b
        '     XF1,XF2      : tableaux de travail
        '     FX1,FY1      : tableaux de travail
        '     FX2,FY2      : tableaux de travail
        '     NCC(b)       : nombre de charges concentrées sur la barre b
        '     NSINT(b)     : nombre de sections d'intérêt dans la barre b
        '     DX,DY,DZ(b)  : déplacements à l'extrémité de la barre b supposée
        '                    en poutre console et chargée
        '     SNX(s,b)     : |
        '     STX(s,b)     : |sollicitations dans la section s de la barre b
        '     SMX(s,b)     : |
        '     AML(6,b)     : charges équivalentes aux extrémités de b
        '     FN(d)        : vecteur charges/noeuds global modifié par les AML
        '
        '-----------------------------------------------------------------------
        '
        '      DIMENSION NCC(NBT), NCR(NBT), XFC(NCCMAX, NBT)
        '      DIMENSION FCX(NCCMAX, NBT), FCY(NCCMAX, NBT), FCZ(NCCMAX, NBT)
        '      DIMENSION XFR1(NCRMAX, NBT), FRX1(NCRMAX, NBT), FRY1(NCRMAX, NBT)
        '      DIMENSION XFR2(NCRMAX, NBT), FRX2(NCRMAX, NBT), FRY2(NCRMAX, NBT)
        '      DIMENSION XLONG(NBT), SECT(NBT), XIN(NBT)
        '      DIMENSION TEMPB(NBT)
        '      DIMENSION JEXB(2, NBT), RESB(2, NBT), FN(IDIM)
        '      DIMENSION CX(NBT), CY(NBT), NSINT(NBT)
        '      DIMENSION NBTRONCONS(NBT)
        ReDim XFI(NCCMAX), FX(NCCMAX), FY(NCCMAX), FZ(NCCMAX)
        ReDim XF1(NCRMAX), FX1(NCRMAX), FY1(NCRMAX)
        ReDim XF2(NCRMAX), FX2(NCRMAX), FY2(NCRMAX)
        ReDim DX(NBT), DY(NBT), DZ(NBT)
        ReDim XMIEQ0(NBT), XMJEQ0(NBT)
        ReDim FNCB(IDIM)

        'ReDim SNX(NSIMAX, NBT), STX(NSIMAX, NBT), SMX(NSIMAX, NBT)
        'ReDim AML(6, NBT), XF(NSIMAX, NBT)

        Dim D As Double, R3 As Double, R4 As Double, R5 As Double, R6 As Double,
            R7 As Double, R8 As Double, R9 As Double, R10 As Double
        Dim EPS1, EPS2, X1, X2, XLINT, XL, DXI, DYI, DZI, AX1, AY1, AZ1 As Double
        Dim WX1, WX2, WY1, WY2, EAL, EIL, EIL2, EIL3, WI, WJ, AML3, AML6 As Double
        Dim NCCI, NCRI, J, JE, K, NCRK, JL, IS1, IS2, NI, JJ, JK As Integer
        Dim T1, T2, T3, FXX, FYY, TMP As Double

        EPS1 = 0.00001
        EPS2 = 0.001

        '-Boucle sur les barres
        '======================
        For I = 1 To NBT

            XF(2, I) = 1
            NSINT(I) = 2

            If NCC(I) + NCR(I) > 0 Or TEMPB(I) <> 0 Then

                NCCI = NCC(I)
                NCRI = NCR(I)
                JE = 2 * (NCCI + NCRI + 1)
                NSINT(I) = JE

                If NCCI > 0 Then
                    For J = 1 To NCCI
                        XFI(J) = XFC(J, I)
                        FX(J) = FCX(J, I)
                        FY(J) = FCY(J, I)
                        FZ(J) = FCZ(J, I)
                        XF(2 * J + 1, I) = XFI(J) - EPS1
                        XF(2 * J + 2, I) = XFI(J) + EPS1
                    Next J
                End If

                If NCRI > 0 Then

                    K = 2 * NCCI + 2

                    For J = 1 To NCRI
                        XF1(J) = XFR1(J, I)
                        XF2(J) = XFR2(J, I)
                        FX1(J) = FRX1(J, I)
                        FX2(J) = FRX2(J, I)
                        FY1(J) = FRY1(J, I)
                        FY2(J) = FRY2(J, I)
                        If XF1(J) > EPS1 Then
                            K = K + 1
                            XF(K, I) = XF1(J) + 0.5 * EPS1
                        End If
                        If XF2(J) < 1 - EPS1 Then
                            K = K + 1
                            XF(K, I) = XF2(J) - 0.5 * EPS1
                        End If
                    Next J

                    NSINT(I) = K
                    JE = K

                    '-DETERMINATION DES INTERVALLES DANS LES BARRES SOUMISES A DES CHARGES REPARTIES
                    '-------------------------------------------------------------------------------

                    NCRK = 0
                    K = 0
                    X2 = -EPS1

re:
                    X1 = 1 + EPS1

                    For J = 1 To NCRI
                        If X2 < XF1(J) And XF1(J) < X1 Then X1 = XF1(J)
                    Next J

                    If X1 <= 1 Then

                        For J = 1 To NCRI
                            If Math.Abs(XF1(J) - X1) < EPS2 Then NCRK = NCRK + 1
                            If Math.Abs(XF2(J) - X1) < EPS2 Then NCRK = NCRK - 1
                        Next J

rere:
                        For J = 1 To JE
                            If XF(J, I) >= X1 - 0.1 * EPS1 And XF(J, I) <= X1 + EPS2 Then
                                X1 = XF(J, I)
                                JL = J
                            End If
                        Next J

                        K = K + 1
                        IS1 = JL
                        X2 = 1

                        For J = 1 To JE
                            If XF(J, I) >= X1 + EPS2 And XF(J, I) <= X2 Then
                                X2 = XF(J, I)
                                JL = J
                            End If
                        Next J

                        IS2 = JL

                        '-Création des sections d'intérêt intermédiaires dans l'intervalle K
                        XLINT = XF(IS2, I) - XF(IS1, I)
                        'NI = XLINT * NBTRONCONS(I) + 0.5
                        NI = XLINT * 1 + 0.5
                        If NI >= 2 Then
                            For IT = 1 To NI - 1
                                NSINT(I) = NSINT(I) + 1
                                NCC(I) = NCC(I) + 1
                                NCCI = NCCI + 1
                                XF(NSINT(I), I) = XF(IS1, I) + IT * XLINT / NI
                                FX(NCCI) = 0
                                FY(NCCI) = 0
                                FZ(NCCI) = 0
                                XFI(NCCI) = XF(NSINT(I), I)
                            Next IT
                        End If

                        If 1 - X2 - EPS2 >= 0 Then
                            For J = 1 To NCRI
                                If Math.Abs(XF1(J) - X2) < EPS2 Then NCRK = NCRK + 1
                                If Math.Abs(XF2(J) - X2) < EPS2 Then NCRK = NCRK - 1
                            Next J
                            If NCRK = 0 Then
                                X2 = X2 + EPS2
                                GoTo re
                            Else
                                X1 = X2
                                GoTo rere
                            End If
                        End If

                    End If

                End If

                '-CALCUL DE DX,DY,DZ,ANX,AMX,ATX  AUX EXTREMITES DE LA BARRE
                '-----------------------------------------------------------

                XL = XLONG(I)

                DXI = ALPHAT * TEMPB(I) * E * SECT(I)

                DYI = 0
                DZI = 0
                AX1 = 0
                AY1 = 0
                AZ1 = 0

                If NCCI > 0 Then
                    For J = 1 To NCCI
                        DXI = DXI + FX(J) * XFI(J)
                        DYI = DYI + FY(J) * (3 - XFI(J)) * XFI(J) ^ 2 / 6 + FZ(J) * (1 - 0.5 * XFI(J)) * XFI(J) / XL
                        DZI = DZI + FY(J) * XFI(J) ^ 2 / 2 + FZ(J) * XFI(J) / XL
                        AX1 = AX1 + FX(J)
                        AY1 = AY1 + FY(J)
                        AZ1 = AZ1 + FZ(J) + FY(J) * XL * XFI(J)
                    Next J
                End If

                If NCRI > 0 Then
                    For J = 1 To NCRI
                        X1 = XF1(J)
                        X2 = XF2(J)
                        WX1 = FX1(J) * XL * (X2 - X1)
                        WX2 = (FX2(J) - FX1(J)) * 0.5 * XL * (X2 - X1)
                        WY1 = FY1(J) * XL * (X2 - X1)
                        WY2 = (FY2(J) - FY1(J)) * 0.5 * XL * (X2 - X1)
                        DXI = DXI + WX1 * (X2 + X1) / 2 + WX2 * (2 * X2 + X1) / 3
                        DYI = DYI + WY1 * (4 * X1 ^ 2 + 4 * X1 * X2 + 4 * X2 ^ 2 - X1 ^ 3 - X1 ^ 2 * X2 - X1 * X2 ^ 2 - X2 ^ 3) / 24 _
                        + WY2 * (5 * X1 ^ 2 + 10 * X1 * X2 + 15 * X2 ^ 2 - 4 * X2 ^ 3 - 3 * X1 * X2 ^ 2 - 2 * X1 ^ 2 * X2 - X1 ^ 3) / 60
                        DZI = DZI + WY1 * (X1 ^ 2 + X1 * X2 + X2 ^ 2) / 6 + WY2 * (X1 ^ 2 + 2 * X1 * X2 + 3 * X2 ^ 2) / 12
                        AX1 = AX1 + WX1 + WX2
                        AY1 = AY1 + WY1 + WY2
                        AZ1 = AZ1 + WY1 * 0.5 * XL * (X1 + X2) + WY2 * XL * (X1 + 2 * X2) / 3
                    Next J
                End If

                EAL = E * SECT(I) / XL
                EIL = E * XIN(I) / XL
                EIL2 = E * XIN(I) / XL ^ 2
                EIL3 = E * XIN(I) / XL ^ 3

                DX(I) = DXI / EAL
                DY(I) = DYI / EIL3
                DZ(I) = DZI / EIL2

                SNX(1, I) = AX1
                STX(1, I) = AY1
                SMX(1, I) = AZ1
                SNX(2, I) = 0
                STX(2, I) = 0
                SMX(2, I) = 0

                '-CALCUL DES REACTIONS POUR LA BARRE ENCASTREE SANS RESSORTS
                '-----------------------------------------------------------
                AML(3, I) = 6 * DYI * XL - 2 * DZI * XL - AZ1
                AML(5, I) = -12 * DYI + 6 * DZI
                AML(4, I) = -DXI
                AML(1, I) = -AX1 - AML(4, I)
                AML(2, I) = -AY1 - AML(5, I)
                AML(6, I) = -AZ1 - AML(3, I) - XL * AML(5, I)

                '-Sauvegarde des moments équivalents de la barre sans ressorts
                XMIEQ0(I) = AML(3, I)
                XMJEQ0(I) = AML(6, I)

                '-TERMES DE RIGIDITE (BARRE ELASTIQUE)
                '-------------------------------------
                R3 = 4 * EIL
                R4 = 0
                R5 = 0
                R6 = 6 * EIL2
                R7 = 0
                R8 = 6 * EIL2
                R9 = 2 * EIL
                R10 = 4 * EIL

                D = (R3 + RESB(1, I)) * (R10 + RESB(2, I)) - R9 ^ 2

                '-CALCUL DES ROTATIONS DANS LES RESSORTS
                '---------------------------------------
                WI = ((R10 + RESB(2, I)) * XMIEQ0(I) - R9 * XMJEQ0(I)) / D
                WJ = ((R3 + RESB(1, I)) * XMJEQ0(I) - R9 * XMIEQ0(I)) / D

                '-CALCUL DES REACTIONS POUR LA BARRE ENCASTREE AVEC RESSORTS
                '-----------------------------------------------------------
                AML3 = AML(3, I)
                AML6 = AML(6, I)
                AML(3, I) = ((R10 + RESB(2, I)) * AML3 - R9 * AML6) * RESB(1, I) / D
                AML(6, I) = ((R3 + RESB(1, I)) * AML6 - R9 * AML3) * RESB(2, I) / D
                AML(1, I) = -R5 * WI - R7 * WJ + AML(1, I)
                AML(4, I) = R5 * WI + R7 * WJ + AML(4, I)
                AML(2, I) = -R6 * WI - R8 * WJ + AML(2, I)
                AML(5, I) = R6 * WI + R8 * WJ + AML(5, I)

                '-CALCUL DES CHARGES AUX NOEUDS EQUIVALENTES AUX CHARGES SUR LES BARRES
                '----------------------------------------------------------------------
                JJ = 3 * JEXB(1, I)
                JK = 3 * JEXB(2, I)
                FNCB(JJ - 2) = FNCB(JJ - 2) - AML(1, I) * CX(I) + AML(2, I) * CY(I)
                FNCB(JJ - 1) = FNCB(JJ - 1) - AML(1, I) * CY(I) - AML(2, I) * CX(I)
                FNCB(JJ) = FNCB(JJ) - AML(3, I)
                FNCB(JK - 2) = FNCB(JK - 2) - AML(4, I) * CX(I) + AML(5, I) * CY(I)
                FNCB(JK - 1) = FNCB(JK - 1) - AML(4, I) * CY(I) - AML(5, I) * CX(I)
                FNCB(JK) = FNCB(JK) - AML(6, I)

                '-CALCUL DE SNX,STX,SMX LE LONG DE LA BARRE
                '------------------------------------------

                '-Boucle sur les sections d'intérêt
                For J = 3 To NSINT(I)

                    T1 = 0
                    T2 = 0
                    T3 = 0

                    If NCCI > 0 Then
                        For K = 1 To NCCI
                            If XFI(K) >= XF(J, I) Then
                                T1 = T1 + FX(K)
                                T2 = T2 + FY(K)
                                T3 = T3 + FZ(K) + FY(K) * XL * (XFI(K) - XF(J, I))
                            End If
                        Next K
                    End If

                    If NCRI > 0 Then
                        For K = 1 To NCRI
                            If XF2(K) - XF(J, I) - 2 * EPS1 >= 0 Then
                                If XF1(K) - XF(J, I) + 2 * EPS1 > 0 Then
                                    X1 = XF1(K)
                                    FXX = FX1(K)
                                    FYY = FY1(K)
                                Else
                                    X1 = XF(J, I)
                                    FXX = FX1(K) + (FX2(K) - FX1(K)) * (XF(J, I) - XF1(K)) / (XF2(K) - XF1(K))
                                    FYY = FY1(K) + (FY2(K) - FY1(K)) * (XF(J, I) - XF1(K)) / (XF2(K) - XF1(K))
                                End If
                                T1 = T1 + 0.5 * XL * (FX2(K) + FXX) * (XF2(K) - X1)
                                TMP = 0.5 * (FY2(K) + FYY) * (XF2(K) - X1) * XL
                                T2 = T2 + TMP
                                T3 = T3 + XL ^ 2 * (FYY + 2 * FY2(K)) * (XF2(K) - X1) ^ 2 / 6 + TMP * (X1 - XF(J, I)) * XL
                            End If
                        Next K
                    End If

                    SNX(J, I) = T1
                    STX(J, I) = T2
                    SMX(J, I) = T3

                Next J

            End If

        Next I

    End Sub

    Sub CHABAR2ORDRELOC(ByRef NBT As Integer, ByRef NBTRONC() As Integer, ByRef XLONG() As Double, ByRef JEXB(,) As Integer, ByRef SECT() As Double, ByRef XIN() As Double,
                 ByRef OMT() As Double, ByRef WIT() As Double, ByRef WJT() As Double, ByRef DEPT() As Double, ByRef FN2ORDRELOC() As Double, ByRef E As Double,
                 ByRef SNT(,) As Double, ByRef CX() As Double, ByRef CY() As Double, ByRef IDIM As Integer, ByRef RESB(,) As Double, ByRef AML2ORDRELOC(,) As Double)
        '---------------------------------------------------------------------------------------
        ' CALCUL DES CHARGES AUX NOEUDS EQUIVALENTES AUX EFFETS DU 2ORDRE LOCAUX DANS LES BARRES
        '---------------------------------------------------------------------------------------
        ' -ENTREES:
        '     NBT          : nombre total de barres
        '     XLONG(b)     : longueur de la barre b
        '     JEXB(2, b)   : numéros d'extrémités de la barre b
        '     RESB(2,b)    : ressorts de liaison aux extrémités de b
        '     CX,CY(b)     : cosinus directeurs de la barre b
        '     IDIM         : nombre total de ddl
        '     FN(d)        : vecteur charges/noeuds global initial
        '
        ' -SORTIES:
        '     FN(d)        : vecteur charges/noeuds global modifié par les AML
        '
        '-----------------------------------------------------------------------
        '
        ReDim FN2ORDRELOC(IDIM)
        Dim EAL, EIL, EIL2, EIL3, XL As Double
        Dim RI, RJ As Double
        Dim NT As Integer    'nb de tronçons dans la barre discrétisée
        Dim NN As Integer
        Dim JJ, JK As Integer
        Dim VK, VKM1, VKP1, PHIKM1, PHIK, FK, SNTMOY, RNA, RNB, RMA, RMB, RTA, RTB As Double
        Dim WI, WJ, AML1, AML2, AML3, AML4, AML5, AML6 As Double

        'Dim AL, CXK, CXLK As Double

        Dim D As Double, R3 As Double, R4 As Double, R5 As Double, R6 As Double,
            R7 As Double, R8 As Double, R9 As Double, R10 As Double

        'Exit Sub

        '-Boucle sur les barres 
        '======================
        For I = 1 To NBT

            If NBTRONC(I) > 1 Then

                NT = NBTRONC(I)
                NN = NT + 1

                JJ = 3 * JEXB(1, I)
                JK = 3 * JEXB(2, I)

                XL = XLONG(I)
                SNTMOY = (SNT(1, I) + SNT(2, I)) / 2    '<0 si compression

                'rotations d'extrémité par rapport à la corde orientée par OMT
                RI = DEPT(JJ) - WIT(I) - OMT(I)
                RJ = DEPT(JK) - WJT(I) - OMT(I)

                'réduction sur coefs rigidité XK et XLK
                'AL = Math.Abs(SNTMOY * XL ^ 2 / E / XIN(I)) ^ 0.5
                'Call XKXLK(SNTMOY, AL, CXK, CXLK)


                '-CALCUL DES REACTIONS POUR LA BARRE ENCASTREE SANS RESSORTS
                '-----------------------------------------------------------

                'Initialisations
                RNA = 0
                RNB = 0
                RMA = 0
                RMB = 0
                RTA = 0
                RTB = 0

                'Boucle sur les noeuds de la barres
                '----------------------------------
                For K = 1 To NN

                    'Force transversale du noeud K
                    '-----------------------------
                    'V(K-1)
                    If K > 1 Then
                        VKM1 = RI * XL * ((K - 2) / NT - 2 * ((K - 2) / NT) ^ 2 + ((K - 2) / NT) ^ 3) +
                               RJ * XL * (-((K - 2) / NT) ^ 2 + ((K - 2) / NT) ^ 3)
                    Else
                        VKM1 = 0
                    End If
                    'V(K)
                    VK = RI * XL * ((K - 1) / NT - 2 * ((K - 1) / NT) ^ 2 + ((K - 1) / NT) ^ 3) +
                         RJ * XL * (-((K - 1) / NT) ^ 2 + ((K - 1) / NT) ^ 3)
                    'V(K+1)
                    If K < NN Then
                        VKP1 = RI * XL * ((K) / NT - 2 * ((K) / NT) ^ 2 + ((K) / NT) ^ 3) +
                               RJ * XL * (-((K) / NT) ^ 2 + ((K) / NT) ^ 3)
                    Else
                        VKP1 = 0
                    End If
                    'PHI(K-1)
                    If K >= 1 Then
                        PHIKM1 = NT / XL * (VK - VKM1)
                    Else
                        PHIKM1 = 0
                    End If
                    'PHI(K)
                    If K <= NN Then
                        PHIK = NT / XL * (VKP1 - VK)
                    Else
                        PHIK = 0
                    End If
                    'Force au noeud K
                    FK = -SNTMOY * (PHIKM1 - PHIK)

                    '                  'correction due à la compression
                    '                  FK = FK / CXK

                    'Réactions d'appuis (cumulées pour tous les noeuds)
                    RMA = RMA - FK * XL / NT ^ 3 * (K - 1) * (NT - K + 1) ^ 2
                    RMB = RMB + FK * XL / NT ^ 3 * (K - 1) ^ 2 * (NT - K + 1)
                    RTA = RTA - FK / NT ^ 2 * (NT - K + 1) ^ 2 * (3 - 2 / NT * (NT - K + 1))
                    RTB = RTB - FK / NT ^ 2 * (K - 1) ^ 2 * (3 - 2 / NT * (K - 1))

                Next K


                '-TERMES DE RIGIDITE (BARRE ELASTIQUE)
                '-------------------------------------
                EAL = E * SECT(I) / XL
                EIL = E * XIN(I) / XL
                EIL2 = E * XIN(I) / XL ^ 2
                EIL3 = E * XIN(I) / XL ^ 3

                R3 = 4 * EIL
                R4 = 0
                R5 = 0
                R6 = 6 * EIL2
                R7 = 0
                R8 = 6 * EIL2
                R9 = 2 * EIL
                R10 = 4 * EIL

                D = (R3 + RESB(1, I)) * (R10 + RESB(2, I)) - R9 ^ 2

                '-CALCUL DES ROTATIONS DANS LES RESSORTS
                '---------------------------------------
                WI = ((R10 + RESB(2, I)) * RMA - R9 * RMB) / D
                WJ = ((R3 + RESB(1, I)) * RMB - R9 * RMA) / D

                '-CALCUL DES REACTIONS POUR LA BARRE ENCASTREE AVEC RESSORTS
                '-----------------------------------------------------------
                AML1 = -R5 * WI - R7 * WJ + RNA
                AML2 = -R6 * WI - R8 * WJ + RTA
                AML3 = ((R10 + RESB(2, I)) * RMA - R9 * RMB) * RESB(1, I) / D
                AML4 = R5 * WI + R7 * WJ + RNB
                AML5 = R6 * WI + R8 * WJ + RTB
                AML6 = ((R3 + RESB(1, I)) * RMB - R9 * RMA) * RESB(2, I) / D

                '-CALCUL DES CHARGES AUX NOEUDS EQUIVALENTES AUX EFFETS DU 2ORDRE LOCAUX DANS LES BARRES
                '---------------------------------------------------------------------------------------
                FN2ORDRELOC(JJ - 2) = FN2ORDRELOC(JJ - 2) - AML1 * CX(I) + AML2 * CY(I)
                FN2ORDRELOC(JJ - 1) = FN2ORDRELOC(JJ - 1) - AML1 * CY(I) - AML2 * CX(I)
                FN2ORDRELOC(JJ) = FN2ORDRELOC(JJ) - AML3
                FN2ORDRELOC(JK - 2) = FN2ORDRELOC(JK - 2) - AML4 * CX(I) + AML5 * CY(I)
                FN2ORDRELOC(JK - 1) = FN2ORDRELOC(JK - 1) - AML4 * CY(I) - AML5 * CX(I)
                FN2ORDRELOC(JK) = FN2ORDRELOC(JK) - AML6

                'STOCKAGE REACTIONS POUR LA BARRE ENCASTREE AVEC RESSORTS
                '--------------------------------------------------------
                AML2ORDRELOC(1, I) = AML1
                AML2ORDRELOC(2, I) = AML2
                AML2ORDRELOC(3, I) = AML3
                AML2ORDRELOC(4, I) = AML4
                AML2ORDRELOC(5, I) = AML5
                AML2ORDRELOC(6, I) = AML6

            End If   'If NBTRONC(I) > 1 

        Next I

    End Sub


    Sub SORSEC(ByRef NBT As Integer, ByRef NSIMAX As Integer, ByRef SN(,) As Double, ByRef ST(,) As Double, ByRef SM(,) _
               As Double, ByRef NSINT() As Integer, ByRef XF(,) As Double, ByRef NSINT0(,) As Integer)
        '-----------------------------------------------------------------------
        '     SELECTION ET CLASSEMENT EN FONCTION DE L'ABSCISSE FRACTIONNAIRE
        '     DES SECTIONS D'INTERET POUR LES SORTIES
        '-----------------------------------------------------------------------
        ' -ENTREES:
        '     SN ,ST ,SM   : efforts dans les sections d'intérêt de la barre
        '     NSIMAX       : nombre maxi de sections d'intérêt par barres
        '     NSINT(b)     : nombre de sections d'intérêt dans la barre b
        '     XF(s,b)      : position de la section d'intérêt s dans la barre b
        '     NBT          : nombre total de barres
        '
        ' -SORTIES:
        '     XFF(s)       : tableau de travail
        '     NSINT0(s,b)  : sections à retenir pour écriture
        '                    (classées en abscisses fractionnaires croissantes)
        '-----------------------------------------------------------------------

        '      DIMENSION SN(NSIMAX, NBT), ST(NSIMAX, NBT), SM(NSIMAX, NBT)
        '      DIMENSION NSINT(NBT), XF(NSIMAX, NBT)
        '      DIMENSION NSINT0(NSIMAX, NBT)

        Dim EPS As Double
        Dim I, J, K As Integer

        EPS = 0.005

        For I = 1 To NBT

            ReDim XFF(NSIMAX), INDSI(NSIMAX)

            For J = 1 To NSINT(I)
                XFF(J) = XF(J, I)
                NSINT0(J, I) = 0
            Next J

            Call TriSurIndices(NSINT(I), XFF, INDSI)

            K = 1
            NSINT0(K, I) = 1  'section origine

            For J = 2 To NSINT(I)

                If XFF(INDSI(J)) > XFF(INDSI(J - 1)) And ((XFF(INDSI(J)) - XFF(INDSI(J - 1))) > 0.001 Or
                   (Math.Abs(SN(INDSI(J), I) - SN(INDSI(J - 1), I)) > EPS * MAXI(Math.Abs(SN(INDSI(J), I)), Math.Abs(SN(INDSI(J - 1), I))) Or
                   Math.Abs(ST(INDSI(J), I) - ST(INDSI(J - 1), I)) > EPS * MAXI(Math.Abs(ST(INDSI(J), I)), Math.Abs(ST(INDSI(J - 1), I))) Or
                   Math.Abs(SM(INDSI(J), I) - SM(INDSI(J - 1), I)) > EPS * MAXI(Math.Abs(SM(INDSI(J), I)), Math.Abs(SM(INDSI(J - 1), I))))) Then

                    '                    K = K + 1
                    '                    NSINT0(K, I) = INDSI(J - 1)
                    K = K + 1
                    NSINT0(K, I) = INDSI(J)

                End If

            Next J

            If XFF(NSINT0(K, I)) < 1 Then  'pas de section n°2 en extrémité de barre (possible si N,T,M=Constants )
                K = K + 1
                NSINT0(K, I) = 2  'section extrémité
            End If

        Next I

    End Sub

    Sub ROTEXB(ByRef NBT As Integer, ByRef SM(,) As Double, ByRef CX() As Double, ByRef CY() As Double, ByRef JEXB(,) As Integer, ByRef DEP() As Double, ByRef E As Double, ByRef SECT() As Double,
               ByRef XIN() As Double, ByRef XLONG() As Double, ByRef OMT() As Double, ByRef RESB(,) As Double, ByRef WI() As Double, ByRef WJ() As Double, ByRef IERREUR As Integer)
        '-----------------------------------------------------------------------
        '     CALCUL DES ROTATIONS AUX EXTREMITES DE BARRES
        '-----------------------------------------------------------------------
        ' -ENTREES:
        '     NBT          : nombre total de barres
        '     IDIM         : nombre de degrés de liberté
        '     NSIMAX       : nombre maxi de sections d'intérêt par barre
        '     DEP(ddl)     : vecteur déplacements des noeuds de la structure
        '     SM(s,b)      : variations moment dans section s de b
        '     E            : module d'Young
        '     XIN(b)       : moment d'inertie de flexion
        '     SECT(b)      : aire de section
        '     RESB(e,b)    : ressorts de liaison aux extrémités
        '     XLONG(b)     : longueur
        '     OMT(b)       : rotation globale
        '     CX(b),CY(b)  : cosinus directeurs de b
        '     JEXB(E, b)   : numéros d'extrémités de b
        '     SBLOC(e,b)   : matrice locale de la barre b
        '
        ' -SORTIES:
        '     WI(b),WJ(b)  : rotations des extrémités de la barre b
        '
        '----------------------------------------------------------------------

        '      DIMENSION JEXB(2, NBT), RESB(2, NBT)
        '      DIMENSION SM(NSIMAX, NBT)
        '      DIMENSION CX(NBT), CY(NBT)
        '      DIMENSION SECT(NBT), XIN(NBT), XLONG(NBT), OMT(NBT), WI(NBT), WJ(NBT)
        '      DIMENSION DEP(IDIM) 

        Dim SBL(10) As Double, DEPL(6) As Double, SNTMOY As Double
        Dim RINF, R1, R2, XMIEQ0_1, XMJEQ0_1, U3, U6, D As Double

        '-Boucle sur les barres
        For I = 1 To NBT

            RINF = 10000000000.0# * E * XIN(I) / XLONG(I)
            R1 = RESB(1, I)
            R2 = RESB(2, I)

            If R1 > 0.99 * RINF And R2 > 0.99 * RINF Then

                '-Pas de ressorts, on passe à la barre suivante
                '-------------------------------------------------
                WI(I) = 0
                WJ(I) = 0

            ElseIf R1 >= 0.00001 And R2 >= 0.00001 Then

                '-Les 2 ressorts sont non nuls
                '-----------------------------
                WI(I) = -SM(1, I) / R1
                WJ(I) = SM(2, I) / R2

            Else

                '-Au moins 1 ressort est nul
                '---------------------------
                '-On calcule les déplacements dans le repère local
                Call DEPLOC(CX(I), CY(I), JEXB(1, I), JEXB(2, I), DEP, DEPL)

                '-On calcule la matrice de la barre sans ressorts (SBL = Z)
                SNTMOY = 0  'XXXXXXXXXXXXXXXXXXXX                
                Call MATLOC(E, SECT(I), XIN(I), XLONG(I), OMT(I), SNTMOY, RINF, RINF, SBL, IERREUR)
                If IERREUR > 0 Then Exit Sub

                '-On calcule les moments équivalents de la barre sans ressorts
                '-Ces moments ont été stockés dans WI et WJ dans MODFEQ au pas précédent
                XMIEQ0_1 = WI(I)
                XMJEQ0_1 = WJ(I)

                '-Calcul du déterminant du système 2x2 en WI,WJ
                D = (SBL(3) + R1) * (SBL(10) + R2) - SBL(9) ^ 2

                '-Calcul de U3,U6
                U3 = SBL(5) * (DEPL(1) - DEPL(4)) + SBL(6) * (DEPL(2) - DEPL(5)) + SBL(3) * DEPL(3) + SBL(9) * DEPL(6) + XMIEQ0_1
                U6 = SBL(7) * (DEPL(1) - DEPL(4)) + SBL(8) * (DEPL(2) - DEPL(5)) + SBL(9) * DEPL(3) + SBL(10) * DEPL(6) + XMJEQ0_1

                '-Calcul des rotations WI,WJ
                WI(I) = (U3 * (SBL(10) + R2) - U6 * SBL(9)) / D
                WJ(I) = (U6 * (SBL(3) + R1) - U3 * SBL(9)) / D

            End If

        Next I

    End Sub

    Function MAXI(ByRef X, ByRef Y)

        MAXI = X
        If Y > MAXI Then MAXI = Y

    End Function

    Function MINI(ByRef X, ByRef Y)

        MINI = X
        If Y < MINI Then MINI = Y

    End Function

    Sub ROTAX_ANALYSE(ByRef CX As Double, ByRef CY As Double, ByRef SBL() As Double, ByRef SBG() As Double)

        '----------------------------------------------------------------------
        '     PASSAGE MATRICE D'UNE BARRE DE LOCAL EN GLOBAL
        '----------------------------------------------------------------------
        ' -ENTREES:
        '     CX,CY        : cosinus directeurs de la barre
        '     SBL(10)      : matrice de rigidité condensée de la barre en local
        '
        ' -SORTIES:
        '     SBG(10)      : matrice de rigidité condensée de la barre en global
        '
        '-----------------------------------------------------------------------

        Dim CX2, CY2, CXY As Double

        CX2 = CX * CX
        CY2 = CY * CY
        CXY = CX * CY

        SBG(1) = SBL(1) * CX2 - 2 * SBL(4) * CXY + SBL(2) * CY2
        SBG(2) = SBL(2) * CX2 + 2 * SBL(4) * CXY + SBL(1) * CY2
        SBG(3) = SBL(3)
        SBG(4) = SBL(4) * (CX2 - CY2) + (SBL(1) - SBL(2)) * CXY
        SBG(5) = SBL(5) * CX - SBL(6) * CY
        SBG(6) = SBL(6) * CX + SBL(5) * CY
        SBG(7) = SBL(7) * CX - SBL(8) * CY
        SBG(8) = SBL(7) * CY + SBL(8) * CX
        SBG(9) = SBL(9)
        SBG(10) = SBL(10)

    End Sub

    Sub MATLOC(ByRef E As Double, ByRef SECT As Double, ByRef XIN As Double, ByRef XLONG As Double,
               ByRef OMT As Double, ByVal SNTMOY As Double, ByRef RESB1 As Double, ByRef RESB2 As Double,
               ByRef SBL() As Double, ByRef IERREUR As Integer)
        '-----------------------------------------------------------------------
        '     MATRICE DE RIGIDITE TANGENTE EN REPERE LOCAL
        '-----------------------------------------------------------------------
        ' -ENTREES:
        '     I           : numéro de la barre traitée
        '     E           : module d'Young
        '     XIN         : moment dinertie de flexion
        '     SECT        : aire de section
        '     RESB1,RESB2 : ressorts de liaison aux extrémités
        '     XLONG       : longueur
        '     OMT         : rotation globale
        '
        ' -SORTIES:
        '     SBL(10)     : matrice de rigidit‚ tangente en repŠre local
        '-----------------------------------------------------------------------

        Dim C(6) As Double
        Dim XA, XI, XK, XLK, R1, R2, XKLK, XL, D As Double

        XA = E * SECT / XLONG
        XI = E * XIN / XLONG
        XK = 4 * XI
        XLK = 2 * XI
        R1 = RESB1
        R2 = RESB2

        XKLK = XK + XLK
        XL = XLONG

        SBL(1) = XA
        SBL(2) = 2 * XKLK / XL ^ 2
        SBL(3) = XK
        SBL(4) = 0
        SBL(5) = 0
        SBL(6) = XKLK / XL

        SBL(7) = -SBL(5) + XL * SBL(4)
        SBL(8) = -SBL(6) + XL * SBL(2)
        SBL(9) = -SBL(3) + XL * SBL(6)
        SBL(10) = SBL(3) - 2 * XL * SBL(6) + SBL(2) * XL ^ 2

        '-ON AJOUTE L'INFLUENCE DE LA ROTATION
        '=====================================
        SBL(2) = SBL(2) + 2 * SBL(4) * OMT + SBL(1) * OMT ^ 2 + SNTMOY / XL
        SBL(4) = SBL(4) + SBL(1) * OMT
        SBL(6) = SBL(6) + SBL(5) * OMT
        SBL(8) = SBL(8) + SBL(7) * OMT

        '-TRAITEMENT DES RESSORTS D'EXTREMITES
        '=====================================
        D = 10000000000.0# * XI
        If R1 > 0.99 * D And R2 > 0.99 * D Then Exit Sub
        C(1) = D * XA
        C(2) = D * XI / XL ^ 2
        C(3) = R1
        C(4) = C(1)
        C(5) = C(2)
        C(6) = R2
        Call MATRES(C, SBL, IERREUR)

        '-Instabilité de barre détectée
        '------------------------------
        'If IERERUR = 3 Then "Instabilité de la barre"

    End Sub

    Sub XKXLK(ByRef SNT As Double, ByRef AL As Double, ByRef CXK As Double, ByRef CXLK As Double)
        '-----------------------------------------------------------------------
        '     FONCTIONS DE STABILITE POUR COEF. DE RIGIDITE DANS MATLOC
        '-----------------------------------------------------------------------
        '-ENTREES
        '     SNT        : effort normal moyen dans la barre
        '     AL         : paramètre(SQRT(N * L2 / EI))
        '
        '-SORTIES
        '     CXK        : réduction sur le coef. de rigidité XK
        '     CXLK       : réduction sur le coef. de rigidité XLK
        '-----------------------------------------------------------------------

        Dim C, S, DT As Double

        CXK = 1
        CXLK = 1

        'Return  'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        If AL < 0.2 Or SNT >= 0 Then Exit Sub 'pas de modif si traction ou alphaL faible

        'Compression
        C = Math.Cos(AL)
        S = Math.Sin(AL)
        DT = 2 * (1 - C) - AL * S
        CXK = AL * (S - AL * C) / DT / 4
        CXLK = AL * (AL - S) / DT / 2

    End Sub


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
        S(J1J2) = S(J1J2) + SBG(4)
        J1J3 = KND(MAXA, J1, J3)
        S(J1J3) = S(J1J3) + SBG(5)
        J2J2 = KND(MAXA, J2, J2)
        S(J2J2) = S(J2J2) + SBG(2)
        J2J3 = KND(MAXA, J2, J3)
        S(J2J3) = S(J2J3) + SBG(6)
        J3J3 = KND(MAXA, J3, J3)
        S(J3J3) = S(J3J3) + SBG(3)

        K1K1 = KND(MAXA, K1, K1)
        S(K1K1) = S(K1K1) + SBG(1)
        K1K2 = KND(MAXA, K1, K2)
        S(K1K2) = S(K1K2) + SBG(4)
        K1K3 = KND(MAXA, K1, K3)
        S(K1K3) = S(K1K3) - SBG(7)
        K2K2 = KND(MAXA, K2, K2)
        S(K2K2) = S(K2K2) + SBG(2)
        K2K3 = KND(MAXA, K2, K3)
        S(K2K3) = S(K2K3) - SBG(8)
        K3K3 = KND(MAXA, K3, K3)
        S(K3K3) = S(K3K3) + SBG(10)

        J1K1 = KND(MAXA, J1, K1)
        S(J1K1) = S(J1K1) - SBG(1)
        J1K2 = KND(MAXA, J1, K2)
        S(J1K2) = S(J1K2) - SBG(4)
        J1K3 = KND(MAXA, J1, K3)
        S(J1K3) = S(J1K3) + SBG(7)
        J2K1 = KND(MAXA, J2, K1)
        S(J2K1) = S(J2K1) - SBG(4)
        J2K2 = KND(MAXA, J2, K2)
        S(J2K2) = S(J2K2) - SBG(2)
        J2K3 = KND(MAXA, J2, K3)
        S(J2K3) = S(J2K3) + SBG(8)
        J3k1 = KND(MAXA, J3, K1)
        S(J3k1) = S(J3k1) - SBG(5)
        J3K2 = KND(MAXA, J3, K2)
        S(J3K2) = S(J3K2) - SBG(6)
        J3K3 = KND(MAXA, J3, K3)
        S(J3K3) = S(J3K3) + SBG(9)

    End Sub

    Function KND(ByRef MAXA, ByRef I, ByRef J) As Integer

        'Dim MAXA(IDIM1) As Integer
        Dim Max, Min

        If J > I Then Max = J Else Max = I
        If J < I Then Min = J Else Min = I
        KND = MAXA(Max) + Max - Min

    End Function

    Function JND(ByRef I, ByRef J)

        JND = (I * (I - 1)) / 2 + J

    End Function

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
        'Dim C(6), SBL(10)
        Dim N, I1, I, J, K As Integer

        'Dim INDICE
        'INDICE = Array(0, 1, 4, 2, 5, 6, 3, -1, -4, -5, 1, -4, -2, -6, 4, 2, 7, 8, 9, -7, -8, 10)
        Dim INDICE() As Integer = {0, 1, 4, 2, 5, 6, 3, -1, -4, -5, 1, -4, -2, -6, 4, 2, 7, 8, 9, -7, -8, 10}

        N = 6

        '-CALCUL DE B A PARTIR DE LA MATRICE REDUITE SBL
        '-***********************************************
        For I = 1 To 21
            I1 = Math.Abs(INDICE(I))
            B(I) = SBL(I1) * INDICE(I) / I1
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
        If IERREUR = 3 Then Exit Sub

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
        SBL(1) = B(1)
        SBL(2) = B(3)
        SBL(3) = B(6)
        SBL(4) = B(2)
        SBL(5) = B(4)
        SBL(6) = B(5)
        SBL(7) = B(16)
        SBL(8) = B(17)
        SBL(9) = B(18)
        SBL(10) = B(21)

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
        IERREUR = 3

    End Sub

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

    Sub COLSOL(ByRef A() As Double, ByRef V() As Double, ByRef MAXA() As Long, ByRef NN As Integer, ByRef KKK As Integer, ByRef DET As Double, ByRef NEXP As Integer)
        '----------------------------------------------------------------------
        'RESOLUTION DU SYSTEME
        '----------------------------------------------------------------------
        'ENTREES :
        '     A(NDIM)    : matrice de rigidité en skyline
        '     V(NN)      : vecteur charges
        '     MAXA(NN+1) : vecteur des indices des termes de la diagonale
        '     NN         : nombre de ddl
        '     NDIM       : dimension de A
        '     KKK        : =1-->triangularisation de la matrice de rigidite
        '                  =2-->sans triangularisation
        'SORTIES :
        '     A(NDIM)    : matrice triangularisee (d+lt) en skyline
        '     V(NN)      : vecteur deplacements
        '     DET        : mantisse du déterminant
        '     NEXP       : exposant du déterminant
        '----------------------------------------------------------------------

        'Dim NDIM As Integer
        'Dim MAXA(NN + 1) As Integer
        'Dim A(NDIM) As Double
        'Dim V(NN) As Double

        Dim B As Double
        Dim C As Double
        Dim N, KN, KL, KU, KH, KLT, J, KI, ND, KK, K As Integer

        'FORMATION DE L*D*L(T)    stockée sur diagonale de A
        '---------------------------------------------------
        If KKK = 1 Then
            For N = 1 To NN
                KN = MAXA(N)
                KL = KN + 1
                KU = MAXA(N + 1) - 1
                KH = KU - KL
                If KH < 0 Then GoTo lbl_2
                If KH = 0 Then GoTo lbl_1
                K = N - KH
                KLT = KU
                For J = 1 To KH
                    KLT = KLT - 1
                    KI = MAXA(K)
                    ND = MAXA(K + 1) - KI - 1
                    If ND > 0 Then
                        KK = MINI(J, ND)
                        C = 0
                        For l = 1 To KK
                            C = C + A(KI + l) * A(KLT + l)
                        Next l
                        A(KLT) = A(KLT) - C
                    End If
                    K = K + 1
                Next J
lbl_1:
                K = N
                B = 0
                For KK = KL To KU
                    K = K - 1
                    KI = MAXA(K)
                    C = A(KK) / A(KI)
                    B = B + C * A(KK)
                    A(KK) = C
                Next KK
                A(KN) = A(KN) - B
lbl_2:
                If A(KN) <= 0 Then
                    DET = -1
                    NEXP = 0
                    Exit Sub
                End If
            Next N

            'CALCUL DU DETERMINANT
            '---------------------
            DET = 1
            NEXP = 0
            For N = 1 To NN
                DET = DET * A(MAXA(N))
                While Math.Abs(DET) > 1
                    DET = DET / 10
                    NEXP = NEXP + 1
                End While
                While Math.Abs(DET) < 0.1
                    DET = DET * 10
                    NEXP = NEXP - 1
                End While
            Next N

            Exit Sub

        End If

        'REDUCTION DU VECTEUR FORCE
        '--------------------------
reduc:
        For N = 1 To NN
            KL = MAXA(N) + 1
            KU = MAXA(N + 1) - 1
            If KU >= KL Then
                K = N
                C = 0
                For KK = KL To KU
                    K = K - 1
                    C = C + A(KK) * V(K)
                Next KK
                V(N) = V(N) - C
            End If
        Next N

        'BACK-SUBSTITUTION
        '-----------------
        For N = 1 To NN
            K = MAXA(N)
            V(N) = V(N) / A(K)
        Next N
        If NN = 1 Then Exit Sub
        N = NN
        For l = 2 To NN
            KL = MAXA(N) + 1
            KU = MAXA(N + 1) - 1
            If KU >= KL Then
                K = N
                For KK = KL To KU
                    K = K - 1
                    V(K) = V(K) - A(KK) * V(N)
                Next KK
            End If
            N = N - 1
        Next l

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


    Sub TriSurIndices(ByVal n As Integer, ByVal Tableau() As Double, ByVal Ind() As Integer)

        '==========================================================
        'Routine de tri sur indices par la méthode de Shell-Metzner
        '==========================================================
        Dim ecart, m, stock

        For I = 1 To n
            Ind(I) = I
        Next I

        ecart = n

950:
        ecart = Int(ecart / 2)
        If ecart < 1 Then GoTo fin

        For J = 1 To n - ecart
            For I = J To 1 Step -ecart
                m = I + ecart
                If Tableau(Ind(I)) < Tableau(Ind(m)) Then GoTo 990
                stock = Ind(I)
                Ind(I) = Ind(m)
                Ind(m) = stock
            Next I
990:
        Next J
        GoTo 950

fin:

    End Sub


    Function TEST_CONSTANTMATRIX(ByRef SUMSTRUCT As Double, ByVal SUMSTRUCT_save As Double, ByRef MAXA() As Long, ByRef MAXA_save() As Long,
                                 ByRef IDIM As Integer, ByRef IDIM_save As Integer) As Boolean

        TEST_CONSTANTMATRIX = False

        If IDIM <> IDIM_save Then Exit Function
        If SUMSTRUCT <> SUMSTRUCT_save Then Exit Function
        If MAXA(IDIM + 1) <> MAXA_save(IDIM + 1) Then Exit Function

        TEST_CONSTANTMATRIX = True

    End Function

End Module