#Region " INTERNAL VARIABLES - MODAL "

Friend Structure MATERIAU
    Dim E As Double             'module d'élasticité    
End Structure

Friend Structure STR_NOEUDS
    Dim NNT As Integer          'nb de noeuds
    Dim X() As Double           'abscisses des noeuds                           X(NNT)
    Dim Y() As Double           'ordonnées des noeuds                           Y(NNT)
    Dim RESN(,) As Double       'ressorts aux noeuds suivant les 3 ddl         RESN(3,NNT)
End Structure

Friend Structure STR_BARRES
    Dim NBT As Integer          'nb de barres
    Dim JEXB(,) As Integer      'n° des noeuds extrémités des barres           JEXB(2,NBT)        
    Dim SECT() As Double        'aires de section des barres                    SECT(NBT)
    Dim XIN() As Double         'inerties de flexion des barres                 XIN(NBT)
    Dim RESB(,) As Double       'ressorts en flexion aux extrémités            RESB(2,NBT)                
End Structure

Friend Structure MAS_BARRES    'DANS LEUR REPERE LOCAL
    Dim NMC(,) As Integer        'nb de masse concentrées par barre              NMC(NCAS,NBT)
    Dim XMC(,,) As Double        'abscisses fractionnaires des masses conc.      XFC(NCAS,NMC,NBT)
    Dim MC(,,) As Double         'valeurs des masses concentrées                 MC(NCAS,NMC,NBT)        
    Dim NMR(,) As Integer        'nb de masses réparties par barre               NMR(NCAS,NBT)
    Dim XMR1(,,) As Double       'abscisse fraction. de l'orig. de la masse rep. XMR1(NCAS,NMR,NBT)
    Dim XMR2(,,) As Double       'abscisse fraction. de l'extr. de la masse rep. XMR2(NCAS,NMR,NBT)
    Dim MR1(,,) As Double        'masse à l'origine de la masse rep.             MR1(NCAS,NMR,NBT)        
    Dim MR2(,,) As Double        'masse à l'extrem. de la masse rep.             MR2(NCAS,NMR,NBT)        
End Structure

Friend Structure MOD_RESOLUTION
    Dim NCAS As Integer           'nb de cas de charges
    Dim NBVALP As Integer       'nb de valeurs propres demandées
    Dim TOLERANCE As Double     'tolérance de convergence dans la résolution VP
    Dim NOVECTP As Boolean      'si VRAI : pas de calcul du vecteur propre
    Dim TXT_RECEPTEUR As Object 'TextBox servant de liaison pour transfert d'évènements entre FORTRAN et VB
    Dim TXT_PROGRESS As Object  'TextBox dans laquelle est retourné le % d'avancement des calculs
    Dim DUMP As Integer         'indicateur pour sortie de résultats dans RESONVP (--> fichier DUMP.TXT)
    '                            si =0 : pas de dump, si =1 sortie arguments d'entrée + indication passage 1er CALL EXPORTINFO        
End Structure

Friend Structure MOD_RESULTATS
    Dim VALP(,) As Double       'valeurs propres                                          VALP(NCAS,NBVALP)
    Dim VECTP(,,) As Double      'vecteurs propres                                          VECTP(NCAS,NBVALP,3*NNT)        
    Dim MAS_TOT() As Double      'Masse totale                                             MAS_TOT(NCAS)        
    Dim MAS_MOD(,) As Double     'Masse modale                                            MAS_MOD(NCAS,NBVALP)            
    Dim MAS_GEN(,) As Double     'Masse généralisée                                       MAS_GEN(NCAS,NBVALP)            
End Structure

#End Region

Module Mod_Declarations

End Module
