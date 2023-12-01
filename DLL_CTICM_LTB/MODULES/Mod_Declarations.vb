#Region " INTERNAL VARIABLES - LTB "

Friend Structure LTB_MATERIAU
    Dim E As Double             'module d'élasticité
    Dim G As Double             'module de cisaillement
End Structure

Friend Structure LTB_BARRES
    Dim NBELEM As Short         'nombre d'éléments barres
    Dim LONGPROJ() As Double    'longueur projetée sur OX de chaque élément             LONGPROJ(NBELEM)
    Dim IZ() As Double          'inertie de fexion /zz de chaque élément                IZ(NBELEM)
    Dim IT() As Double          'inertie de torsion de chaque élément                   IT(NBELEM)
    Dim IW() As Double          'inertie de gauchissement de chaque élément             IW(NBELEM)
    Dim BETAZ() As Double       'coef. de Wagner /zz de chaque élément                  BETAZ(NBELEM)
    Dim ZCG() As Double          'ordonnée de C / G de chaque élément (>0 au-dessus)     ZC(NBELEM)
    Dim RYZ() As Double         'rayon de giration polaire                              RYZ(NBELEM)
    Dim KT1() As Double         'rigidité de liaison en torsion (t) à l'origine         KT1(NBELEM) ! -1:continu, 0-libre, >0 ressort
    Dim KVP1() As Double        'rigidité de liaison en flexion/zz (v') à l'origine     KVP1(NBELEM) ! -1:continu, 0-libre, >0 ressort
    Dim KTP1() As Double        'rigidité de liaison en gauchissement (t') à l'origine  KTP1(NBELEM) ! -1:continu, 0-libre, >0 ressort
End Structure

Friend Structure LTB_NOEUDS
    Dim ZN() As Double          'ordonnée du noeud / OX (>0 vers le haut)               ZN(NBELEM+1)            
    Dim RV() As Double          'rigidité support suivant v                             RV(NBELEM+1)
    Dim RT() As Double          'rigidité support suivant t                             RT(NBELEM+1)
    Dim RVP() As Double         'rigidité support suivant v'                            RVP(NBELEM+1)
    Dim RTP() As Double         'rigidité support suivant t'                            RTP(NBELEM+1)
    Dim ZRC() As Double         'ordonnée du point support / C                          ZRC(NBELEM+1)
End Structure

Friend Structure LTB_SOLLICITATIONS
    Dim MY1() As Double         'moment My à l'origine de l'élément (>0 : sens trigo)   MY1(NBELEM)
    Dim MY2() As Double         'moment My à l'extrémité de l'élément                   MY2(NBELEM)
    Dim FN() As Double          'effort normal dans l'élément (>0 : traction)           FN(NBELEM)
    Dim FNBLOCKED As Boolean    'indicateur de blocage des efforts normaux au seuil critique
    Dim MYBLOCKED As Boolean    'indicateur de blocage des moments fléchissants au seuil critique
End Structure

Friend Structure LTB_CHARGESEXCENTREES
    Dim NFZ As Short            'nb de charges concentrées FZ excentrées / C dans la barre physique
    Dim NQZ As Short            'nb de charges réparties QZ excentrées / C dans la barre physique
    Dim XLFZ() As Double        'abscisse sur OX de FZ()                                XLFZ(NFZ)
    Dim FZ() As Double          'charge FZ (composante suivant Z)                       FZ(NFZ)
    Dim ZFZC() As Double        'ordonnée du point d'application de FZ() / C            ZFZC(NFZ)
    Dim XLQZ1() As Double       'abscisse sur OX de l'origine de la charge QZ           XLQZ1(NQZ)
    Dim QZ1() As Double         'valeur à l'origine de la charge répartie QZ            QZ1(NQZ)
    Dim XLQZ2() As Double       'abscisse sur OX de l'extrémité de la charge QZ         XLQZ2(NQZ)
    Dim QZ2() As Double         'valeur à l'extrémité de la charge répartie QZ          QZ2(NQZ)
    Dim ZQZC() As Double        'ordonnée du point d'application de QZ() / C            ZQZC(NFZ)
End Structure

Friend Structure LTB_RESOLUTION
    Dim NBVALP As Integer       'nb de valeurs propres demandées
    Dim TOLERANCE As Double     'tolérance de convergence dans la résolution VP
    Dim NOVECTP As Boolean      'si VRAI : pas de calcul du vecteur propre
    'Dim TXT_RECEPTEUR As Object 'TextBox servant de liaison pour transfert d'évènements entre FORTRAN et VB
    'Dim TXT_PROGRESS As Object  'TextBox dans laquelle est retourné le % d'avancement des calculs
    Dim DUMP As Integer        'indicateur pour sortie de résultats dans RESONVP (--> fichier DUMP.TXT)
    '                            si =0 : pas de dump, si =1 sortie arguments d'entrée + indication passage 1er CALL EXPORTINFO
End Structure

Friend Structure LTB_RESULTATS
    Dim MOMENTMAX As Double   'moment maxi dans la barre physique
    Dim XMOMENTMAX As Double      'position fractionnaire du moment maxi sur OX
    Dim NMAX As Double       'effort normal maxi dans la barre physique
    Dim XNMAX As Double        'position fractionnaire de l'effort normal maxi sur OX  
    Dim VALP() As Double        'valeurs propres                                        VALP(NBVALP)
    Dim VECTP(,) As Double      'vecteurs propres                                       VECTP(NBVALP,4*(NBELEM+1))
    Dim CODEERROR As String     'code erreur
    Dim TEXTERROR As String     'texte erreur
End Structure

#End Region

Module Mod_Declarations

End Module
