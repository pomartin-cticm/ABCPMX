Public Class CALCUL_MODAL

#Region " VARIABLES INTERNES "
    Dim MAT As MATERIAU
    Dim NOEUDS As STR_NOEUDS
    Dim BARRES As STR_BARRES
    Dim MASSES As MAS_BARRES
    Dim RESOLUTION As MOD_RESOLUTION
    Dim RESULTATS As MOD_RESULTATS
#End Region

#Region " MODELE E.F "

    Private Sub CREATE_MODEL(ByVal Donnees As CTICM_DATA_DLLS.DATA_DLLS)
        '-----------------------------------------------------
        '
        ' 06/09/2023 : TMN, v 1.0.0
        '
        '-----------------------------------------------------
        '
        '   CREATE F.E MODEL
        '
        '-----------------------------------------------------
        '
        ' DONNEES   [E] : Données d'entrée
        '
        '-----------------------------------------------------
        'Variables locales
        Dim iNodeAppui, iNode, iBarre As Integer

        '=== Matériaux
        MAT.E = Donnees.EYOUNG

        '=== Noeuds
        With NOEUDS
            .NNT = Donnees.NbNodes
            ReDim .X(.NNT)
            ReDim .Y(.NNT)
            ReDim .RESN(3, .NNT)        'Ressorts aux noeuds suivant les 3 ddl

            For iNode = 1 To Donnees.NbNodes
                .X(iNode) = Donnees.xNode(iNode - 1)
            Next

            '=== Appuis
            'UX (AXE X) : BLOQUER UX pour que le systeme est stable
            .RESN(1, 1) = -1
            'UZ (AXE Z)
            For iAppui = 1 To Donnees.NbAppuis
                .RESN(2, Donnees.iNodeAppui(iAppui - 1) + 1) = -1
            Next
        End With

        '=== BARRES
        With BARRES
            .NBT = Donnees.NbNodes - 1

            ReDim .JEXB(2, .NBT)
            ReDim .SECT(.NBT)
            ReDim .XIN(.NBT)
            ReDim .RESB(2, .NBT)

            For iBarre = 1 To .NBT
                .JEXB(1, iBarre) = iBarre
                .JEXB(2, iBarre) = iBarre + 1

                'Par défaut: assemblage continu
                .RESB(1, iBarre) = -1
                .RESB(2, iBarre) = -1

                .SECT(iBarre) = Donnees.Aire(iBarre - 1)
                .XIN(iBarre) = Donnees.InertieY(iBarre - 1)
            Next

            For iAppui = 1 To Donnees.NbAppuis
                iNodeAppui = Donnees.iNodeAppui(iAppui - 1)
                If Donnees.lAppuiArticule(iAppui - 1) Then
                    'Articulation

                    If iNodeAppui = 0 Then
                        'Extremite gauche
                        .RESB(1, 1) = 0
                    Else
                        .RESB(2, iNodeAppui) = 0            'iBarre = iNode - 1
                    End If
                End If
            Next

        End With
    End Sub

    Private Sub LOADING(ByVal Donnees As CTICM_DATA_DLLS.DATA_DLLS)
        '-----------------------------------------------------
        '
        ' 06/09/2023 : TMN, v 1.0.0
        '
        '-----------------------------------------------------
        '
        '   LOADING
        '
        '-----------------------------------------------------
        '
        ' DONNEES       [E] : Données d'entrée                
        '
        '-----------------------------------------------------        
        Dim NBFC, NBFR As Integer
        Dim ICAS As Integer = 1
        Dim x1, x2, q1, q2 As Decimal

        '======== MASSES SUR BARRES        
        'Nombre de charges        
        NBFC = Donnees.NbForcesPon
        NBFR = Donnees.NbForcesRep

        With MASSES
            ReDim .NMC(1, BARRES.NBT)           'nb de masse concentrées par barre              NMC(NCAS,NBT)
            ReDim .XMC(1, NBFC, BARRES.NBT)     'abscisses fractionnaires des masses conc.      XFC(NCAS,NMC,NBT)
            ReDim .MC(1, NBFC, BARRES.NBT)      'valeurs des masses concentrées                 MC(NCAS,NMC,NBT)                                
            ReDim .NMR(1, BARRES.NBT)           'nb de masses réparties par barre               NMR(NCAS,NBT)
            ReDim .XMR1(1, NBFR, BARRES.NBT)    'abscisse fraction. de l'orig. de la masse rep. XMR1(NCAS,NMR,NBT)
            ReDim .XMR2(1, NBFR, BARRES.NBT)    'abscisse fraction. de l'extr. de la masse rep. XMR2(NCAS,NMR,NBT)
            ReDim .MR1(1, NBFR, BARRES.NBT)     'masse à l'origine de la masse rep.             MR1(NCAS,NMR,NBT)        
            ReDim .MR2(1, NBFR, BARRES.NBT)     'masse à l'extrem. de la masse rep.             MR2(NCAS,NMR,NBT)        

            For IBARRE = 1 To BARRES.NBT

                'Forces Z (applique sur barre ou au noeud)
                For iFZ = 1 To Donnees.NbForcesPon
                    If ((IBARRE = BARRES.NBT) AndAlso IsEqual(Donnees.xForcePon(iFZ - 1), NOEUDS.X(BARRES.JEXB(2, IBARRE)))) OrElse
                       (IsGreaterOrEqual(Donnees.xForcePon(iFZ - 1), NOEUDS.X(BARRES.JEXB(1, IBARRE))) AndAlso
                        IsSmaller(Donnees.xForcePon(iFZ - 1), NOEUDS.X(BARRES.JEXB(2, IBARRE)))) Then
                        'Fz sur barre

                        'Nombre de Masses Concentrees
                        .NMC(ICAS, IBARRE) = .NMC(ICAS, IBARRE) + 1

                        'Abcisse fractionnaire
                        x1 = (Donnees.xForcePon(iFZ - 1) - NOEUDS.X(BARRES.JEXB(1, IBARRE))) / (NOEUDS.X(BARRES.JEXB(2, IBARRE)) - NOEUDS.X(BARRES.JEXB(1, IBARRE)))

                        'Position
                        .XMC(ICAS, .NMC(ICAS, IBARRE), IBARRE) = x1

                        .MC(ICAS, .NMC(ICAS, IBARRE), IBARRE) = Math.Abs(Donnees.ForcePon(iFZ - 1)) / Donnees.PESANTEUR
                    End If
                Next

                'Masses linéaires Z
                For iQ = 1 To Donnees.NbForcesRep
                    If IsSmaller(Donnees.xForceRep(iQ - 1, 0), NOEUDS.X(BARRES.JEXB(2, IBARRE))) AndAlso
                        IsGreater(Donnees.xForceRep(iQ - 1, 1), NOEUDS.X(BARRES.JEXB(1, IBARRE))) Then
                        'Q sur barre

                        'Nombre de Masses lin
                        .NMR(ICAS, IBARRE) = .NMR(ICAS, IBARRE) + 1

                        'Abcisse fractionnaire
                        If IsSmallerOrEqual(Donnees.xForceRep(iQ - 1, 0), NOEUDS.X(BARRES.JEXB(1, IBARRE))) Then
                            x1 = 0
                            q1 = Donnees.ForceRep(iQ - 1, 0) +
                                (Donnees.ForceRep(iQ - 1, 1) - Donnees.ForceRep(iQ - 1, 0)) /
                                (Donnees.xForceRep(iQ - 1, 1) - Donnees.xForceRep(iQ - 1, 0)) *
                                (NOEUDS.X(BARRES.JEXB(1, IBARRE)) - Donnees.xForceRep(iQ - 1, 0))
                        Else
                            x1 = (Donnees.xForceRep(iQ - 1, 0) - NOEUDS.X(BARRES.JEXB(1, IBARRE))) / (NOEUDS.X(BARRES.JEXB(2, IBARRE)) - NOEUDS.X(BARRES.JEXB(1, IBARRE)))
                            q1 = Donnees.ForceRep(iQ - 1, 0)
                        End If
                        If IsGreaterOrEqual(Donnees.xForceRep(iQ - 1, 1), NOEUDS.X(BARRES.JEXB(2, IBARRE))) Then
                            x2 = 1
                            q2 = Donnees.ForceRep(iQ - 1, 0) +
                                (Donnees.ForceRep(iQ - 1, 1) - Donnees.ForceRep(iQ - 1, 0)) /
                                (Donnees.xForceRep(iQ - 1, 1) - Donnees.xForceRep(iQ - 1, 0)) *
                                (NOEUDS.X(BARRES.JEXB(2, IBARRE)) - Donnees.xForceRep(iQ - 1, 0))
                        Else
                            x2 = (Donnees.xForceRep(iQ - 1, 1) - NOEUDS.X(BARRES.JEXB(1, IBARRE))) / (NOEUDS.X(BARRES.JEXB(2, IBARRE)) - NOEUDS.X(BARRES.JEXB(1, IBARRE)))
                            q2 = Donnees.ForceRep(iQ - 1, 1)
                        End If

                        'Position
                        .XMR1(ICAS, .NMR(ICAS, IBARRE), IBARRE) = x1
                        .XMR2(ICAS, .NMR(ICAS, IBARRE), IBARRE) = x2

                        .MR1(ICAS, .NMR(ICAS, IBARRE), IBARRE) = Math.Abs(q1) / Donnees.PESANTEUR
                        .MR2(ICAS, .NMR(ICAS, IBARRE), IBARRE) = Math.Abs(q2) / Donnees.PESANTEUR

                    End If

                Next
            Next
        End With
    End Sub

#End Region

#Region " LANCER LE CALCUL "

    Public Sub CALCULER(ByVal Donnees As CTICM_DATA_DLLS.DATA_DLLS,
                        ByVal nbModes As Integer,
                        ByRef Output_MODAL As DATA_MODAL.Struc_Output,
                        ByRef ErrorCode As Integer,
                        ByRef ErrorText As String, Optional lConsole As Boolean = False)
        '-----------------------------------------------------
        '
        ' 06/09/2023 : TMN, v 1.0.0
        '
        '-----------------------------------------------------
        '
        '   LANCER LE CALCUL
        '
        '-----------------------------------------------------
        '
        '   DONNEES         [E] : Données d'entrée
        '   nbModes         [E] : nombre de modes
        '
        '-----------------------------------------------------
        '
        '   Output_RDM      [S] : Resultats d'analyse
        '   ErrorCode       [S] : code d'erreur (=0) s'il n'y a pas d'erreur
        '   ErrorText       [S] : texte d'erreur
        '
        '-----------------------------------------------------                

        'Créer le modèle E.F
        CREATE_MODEL(Donnees)

        'Chargement
        LOADING(Donnees)

        With RESOLUTION
            .NCAS = 1

            'nombre de modes
            .NBVALP = nbModes

            'tolérance de convergence dans la résolution VP
            '.TOLERANCE = 0.000000001
            .TOLERANCE = 10 ^ -24
            'si VRAI : pas de calcul du vecteur propre
            .NOVECTP = False

            '.TXT_RECEPTEUR = txt_Tag
            '.TXT_PROGRESS = txt_Fortran_VB
        End With

        'Lancer l'analyse
        ANALYSE(MAT, NOEUDS, BARRES, MASSES, RESOLUTION, RESULTATS, lConsole)
        ErrorCode = CodeERR
        ErrorText = TextERR

        'Mise à jour des résultats
        UPDATE_RESULTS(Donnees.PESANTEUR, Output_MODAL)

    End Sub

#End Region

#Region " RESULTATS D'ANALYSE "

    'Private Sub UPDATE_RESULTS(PESANTEUR As Decimal, ByRef OUTPUT As DATA_MODAL.Struc_Output)
    '    '=============================================
    '    '
    '    ' 26/09/2023 : TMN, v 1.00
    '    '
    '    '=============================================
    '    '
    '    ' MISE A JOUR DES RESULTATS DU CALCUL MODAL
    '    '
    '    '=============================================
    '    '        
    '    ' Donnees       [E] : donnees d'entree
    '    '
    '    '=============================================
    '    '        
    '    ' OUTPUT        [S] : résultats
    '    '
    '    '=============================================

    '    'Initialiser
    '    ReDim OUTPUT.FreqProp(RESOLUTION.NBVALP - 1)
    '    ReDim OUTPUT.VectProp(RESOLUTION.NBVALP - 1, NOEUDS.NNT - 1)
    '    ReDim OUTPUT.MasseMod(RESOLUTION.NBVALP - 1)

    '    For iMode = 1 To RESOLUTION.NBVALP
    '        OUTPUT.FreqProp(iMode - 1) = RESULTATS.VALP(1, iMode) ^ 0.5 / 2 / Math.PI
    '        For iNode = 1 To NOEUDS.NNT
    '            OUTPUT.VectProp(iMode - 1, iNode - 1) = RESULTATS.VECTP(1, iMode, 3 * (iNode - 1) + 2)
    '        Next
    '        OUTPUT.MasseMod(iMode - 1) = RESULTATS.MAS_MOD(1, iMode) * PESANTEUR          'Meme unite que la force
    '    Next
    '    OUTPUT.MasseTot = RESULTATS.MAS_TOT(1) * PESANTEUR          'Meme unite que la force
    'End Sub

    Private Sub UPDATE_RESULTS(PESANTEUR As Decimal, ByRef OUTPUT As DATA_MODAL.Struc_Output)
        '=============================================
        '
        ' 26/09/2023 : TMN, v 1.00
        ' 17/12/2024 : TMN, ajouter la masse généralisée
        '
        '=============================================
        '
        ' MISE A JOUR DES RESULTATS DU CALCUL MODAL
        '
        '=============================================
        '        
        ' Donnees       [E] : donnees d'entree
        '
        '=============================================
        '        
        ' OUTPUT        [S] : résultats
        '
        '=============================================

        'Initialiser
        ReDim OUTPUT.FreqProp(RESOLUTION.NBVALP - 1)
        ReDim OUTPUT.VectProp(RESOLUTION.NBVALP - 1, NOEUDS.NNT - 1)
        ReDim OUTPUT.MasseMod(RESOLUTION.NBVALP - 1)
        ReDim OUTPUT.MasseGen(RESOLUTION.NBVALP - 1)

        For iMode = 1 To RESOLUTION.NBVALP
            OUTPUT.FreqProp(iMode - 1) = RESULTATS.VALP(1, iMode) ^ 0.5 / 2 / Math.PI
            For iNode = 1 To NOEUDS.NNT
                OUTPUT.VectProp(iMode - 1, iNode - 1) = RESULTATS.VECTP(1, iMode, 3 * (iNode - 1) + 2)
            Next
            OUTPUT.MasseMod(iMode - 1) = RESULTATS.MAS_MOD(1, iMode)
            OUTPUT.MasseGen(iMode - 1) = RESULTATS.MAS_GEN(1, iMode)
        Next
        OUTPUT.MasseTot = RESULTATS.MAS_TOT(1)
    End Sub


#End Region

End Class
