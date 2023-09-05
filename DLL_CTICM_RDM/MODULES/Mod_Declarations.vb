#Region " INTERNAL VARIABLES - RDM "

Friend Structure MATERIAU
    Dim E As Double             'module d'élasticité
    Dim ALPHAT As Double        'coef. de dilatation thermique    
End Structure

Friend Structure STR_NOEUDS
    Dim NNT As Integer          'nb de noeuds
    Dim X() As Double           'abscisses des noeuds                           X(NNT)
    Dim Y() As Double           'ordonnées des noeuds                           Y(NNT)
    Dim RESN(,) As Double        'ressorts aux noeuds suivant les 3 ddl         RESN(3,NNT)
End Structure

Structure STR_BARRES
    Dim NBT As Integer          'nb de barres
    Dim JEXB(,) As Integer       'n° des noeuds extrémités des barres           JEXB(2,NBT)
    Dim SECT() As Double        'aires de section des barres                    SECT(NBT)
    Dim XIN() As Double         'inerties de flexion des barres                 XIN(NBT)
    Dim RESB(,) As Double        'ressorts en flexion aux extrémités            RESB(2,NBT)
    Dim OM0() As Double         'rotations globales initiales des barres        OM0(NBT)
    Dim NBTRONC() As Integer    'nb de tronçons pour calcul des effets du 2d ordre locaux  NBTRONC(NBT)    si 1 ou 0 pas de calcul    
End Structure

Structure CHRG_NOEUDS            'DANS LE REPERE GLOBAL
    Dim FN(,,) As Double         'charges aux noeuds suivant les 3 ddl           FN(NCAS,3,NNT)
End Structure

Structure CHRG_BARRES    'DANS LEUR REPERE LOCAL
    Dim NCC(,) As Integer        'nb de charges concentrées par barre            NCC(NCAS,NBT)
    Dim XFC(,,) As Double        'abscisses fractionnaires des charg. conc.      XFC(NCAS,NCCMAX,NBT)
    Dim FCX(,,) As Double        'composante Fx de charges concentrées           FCX(NCAS,NCCMAX,NBT)
    Dim FCY(,,) As Double        'composante Fy de charges concentrées           FCY(NCAS,NCCMAX,NBT)
    Dim FCZ(,,) As Double        'composante Fz (Mz) de charges concentrées      FCZ(NCAS,NCCMAX,NBT)
    Dim NCR(,) As Integer        'nb de charges réparties par barre              NCR(NCAS,NBT)
    Dim NBTR(,) As Integer       'nb de tronçons pour sections d'intérêt         NBTR(NCAS,NBT)
    Dim XFR1(,,) As Double       'abscisse fraction. de l'orig. de la ch. rep.   XFR1(NCAS,NCRMAX,NBT)
    Dim XFR2(,,) As Double       'abscisse fraction. de l'extr. de la ch. rep.   XFR2(NCAS,NCRMAX,NBT)
    Dim FRX1(,,) As Double       'composante Fx à l'origine de la ch. rep.       FRX1(NCAS,NCRMAX,NBT)
    Dim FRY1(,,) As Double       'composante Fy à l'origine de la ch. rep.       FRY1(NCAS,NCRMAX,NBT)
    Dim FRX2(,,) As Double       'composante Fx à l'extrem. de la ch. rep.       FRX2(NCAS,NCRMAX,NBT)
    Dim FRY2(,,) As Double       'composante Fy à l'extrem. de la ch. rep.       FRY2(NCAS,NCRMAX,NBT)
    Dim TEMPB(,) As Double       'écart température dans la barre (°C/20°C)      TEMPB(NCAS,NBT)
End Structure

Structure TYPE_ANALYSE
    Dim NCAS As Integer           'nb de cas de charges
    Dim SECONDORDRE() As Boolean  '=Vrai si 2d ordre, =Faux pour 1er ordre              SECONDORDRE(NCAS)
    Dim NBITERSOMAX() As Integer  'nb d'iterations maxi pour second ordre et alphacr    NBITERSOMAX(NCAS)
    Dim GETALPHACR() As Boolean   '=Vrai pour calcul de alphacr                         GETALPHACR(NCAS)
    Dim CONSTANTMATRIX As Boolean     'vrai si la matrice de rigidité est la même que lors de l'appel précédent
    Dim TOLERSO() As Double       'tolérance en % sur converg. rot. globales (>= 0.001) TOLERSO(NCAS)
    Dim CONVERGEANA() As Boolean  'vrai si convergence atteinte dans analyse            CONVERGEANA(NCAS)
    Dim ALPHACR() As Double       'alphacr                                              ALPHACR(NCAS)
    Dim CONVERGEACR() As Boolean  'vrai si convergence atteinte dans alphacr            CONVERGEACR(NCAS)
End Structure

Structure RES_NOEUDS
    Dim DEPT(,,) As Double       'déplacements des noeuds suivant les 3 ddl      DEPT(NCAS,3,NNT)
    Dim REACT(,,) As Double      'réactions aux noeuds suivant les 3 ddl         REACT(NCAS,3,NNT)
End Structure

Structure RES_BARRES
    Dim NSINT(,) As Integer      'nb de sections d'intérêt par barre             NSINT(NCAS,NBT)
    Dim NSINTRI(,,) As Integer   'n° sect. d'intérêt triées par x croissants     NSINTRI(NCAS,NSINTMAX,NBT)
    Dim XF(,,) As Double         'absisses fract. des sections d'intérêt         XF(NCAS,NSINTMAX,NBT)
    Dim SOLLIC(,,,) As Double    'sollicitations (N,T,M) dans les barres         SOLLIC(NCAS,3,NSINTMAX,NBT)
    Dim JSOLM(,,,) As Integer    'n° des sections de sollic. extrêmes -/+        JSOLM(NCAS, 2, 3, NBT)
    Dim OMT(,) As Double         'rotations globales des barres (avec OM0)       OMT(NCAS,NBT)
    Dim ROTEX(,,) As Double      'rotations dans extrém. de barres (si ressorts) ROTEX(NCAS,2,NBT)
End Structure

#End Region

Module Mod_Declarations

End Module
