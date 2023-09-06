Public Class Cls_CALCUL_MODAL

#Region " VARIABLES INTERNES "
    Dim MAT As MATERIAU
    Dim NOEUDS As STR_NOEUDS
    Dim BARRES As STR_BARRES
    Dim MASSES As MAS_BARRES
    Dim RESOLUTION As MOD_RESOLUTION
    Dim RESULTATS As MOD_RESULTATS
#End Region

#Region " MODELE E.F "

    Private Sub CREATE_MODEL(ByVal Donnees As DATA_MODAL.Struc_Donnees)
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

        With TYPANALYS
            .NCAS = 1                   'On traite combinaison par combinaison
            .CONSTANTMATRIX = False
            ReDim .GETALPHACR(.NCAS)
            ReDim .SECONDORDRE(.NCAS)
            ReDim .NBITERSOMAX(.NCAS)
            ReDim .TOLERSO(.NCAS)

            For ICAS = 1 To .NCAS
                .GETALPHACR(ICAS) = False
                .SECONDORDRE(ICAS) = False
            Next
        End With
    End Sub

    Private Sub LOADING(ByVal Donnees As DATA_RDM.Struc_Donnees)
        '-----------------------------------------------------
        '
        ' 29/08/2023 : TMN, v 1.0.0
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

        '======== CHARGES AUX NOEUDS
        ReDim CH_NOEUDS.FN(1, 3, NOEUDS.NNT)

        For iNode = 1 To NOEUDS.NNT

            'Forces Z
            For iFZ = 1 To Donnees.NbForcesPon
                If IsEqual(Donnees.xForcePon(iFZ - 1), NOEUDS.X(iNode)) Then
                    CH_NOEUDS.FN(1, 2, iNode) -= Donnees.ForcePon(iFZ - 1)
                End If
            Next

            'Moments
            For iMy = 1 To Donnees.NbMoments
                If IsEqual(Donnees.xMoment(iMy - 1), NOEUDS.X(iNode)) Then
                    CH_NOEUDS.FN(1, 3, iNode) += Donnees.Moment(iMy - 1)
                End If
            Next
        Next

        '======== CHARGES SUR BARRES        
        'Nombre de charges        
        NBFC = Donnees.NbForcesPon + Donnees.NbMoments
        NBFR = Donnees.NbForcesRep

        With CH_BARRES
            ReDim .NCC(1, BARRES.NBT)
            ReDim .XFC(1, NBFC, BARRES.NBT)
            ReDim .FCX(1, NBFC, BARRES.NBT)
            ReDim .FCY(1, NBFC, BARRES.NBT)
            ReDim .FCZ(1, NBFC, BARRES.NBT)
            ReDim .NCR(1, BARRES.NBT)
            ReDim .NBTR(1, BARRES.NBT)
            ReDim .XFR1(1, NBFR, BARRES.NBT)
            ReDim .XFR2(1, NBFR, BARRES.NBT)
            ReDim .FRX1(1, NBFR, BARRES.NBT)
            ReDim .FRY1(1, NBFR, BARRES.NBT)
            ReDim .FRX2(1, NBFR, BARRES.NBT)
            ReDim .FRY2(1, NBFR, BARRES.NBT)
            ReDim .TEMPB(1, BARRES.NBT)

            For IBARRE = 1 To BARRES.NBT

                .NBTR(ICAS, IBARRE) = 1         '1 section d'intérêt au milieu                

                'Forces Z
                For iFZ = 1 To Donnees.NbForcesPon
                    If IsGreater(Donnees.xForcePon(iFZ - 1), NOEUDS.X(BARRES.JEXB(1, IBARRE))) AndAlso
                       IsSmaller(Donnees.xForcePon(iFZ - 1), NOEUDS.X(BARRES.JEXB(2, IBARRE))) Then
                        'Fz sur barre

                        'Nombre de FZ
                        .NCC(ICAS, IBARRE) = .NCC(ICAS, IBARRE) + 1

                        'Abcisse fractionnaire
                        x1 = (Donnees.xForcePon(iFZ - 1) - NOEUDS.X(BARRES.JEXB(1, IBARRE))) / (NOEUDS.X(BARRES.JEXB(2, IBARRE)) - NOEUDS.X(BARRES.JEXB(1, IBARRE)))

                        'Position
                        .XFC(ICAS, .NCC(ICAS, IBARRE), IBARRE) = x1

                        .FCY(ICAS, .NCC(ICAS, IBARRE), IBARRE) = -Donnees.ForcePon(iFZ - 1)
                    End If
                Next

                'Moments
                For iMy = 1 To Donnees.NbMoments
                    If IsGreater(Donnees.xMoment(iMy - 1), NOEUDS.X(BARRES.JEXB(1, IBARRE))) AndAlso
                       IsSmaller(Donnees.xMoment(iMy - 1), NOEUDS.X(BARRES.JEXB(2, IBARRE))) Then
                        'Moment sur barre

                        'Nombre de M
                        .NCC(ICAS, IBARRE) = .NCC(ICAS, IBARRE) + 1

                        'Abcisse fractionnaire
                        x1 = (Donnees.xMoment(iMy - 1) - NOEUDS.X(BARRES.JEXB(1, IBARRE))) / (NOEUDS.X(BARRES.JEXB(2, IBARRE)) - NOEUDS.X(BARRES.JEXB(1, IBARRE)))

                        'Position
                        .XFC(ICAS, .NCC(ICAS, IBARRE), IBARRE) = x1

                        .FCZ(ICAS, .NCC(ICAS, IBARRE), IBARRE) = Donnees.Moment(iMy - 1)

                    End If

                Next

                'Forces linéaires Z
                For iQ = 1 To Donnees.NbForcesRep
                    If IsSmaller(Donnees.xForceRep(iQ - 1, 0), NOEUDS.X(BARRES.JEXB(2, IBARRE))) AndAlso
                        IsGreater(Donnees.xForceRep(iQ - 1, 1), NOEUDS.X(BARRES.JEXB(1, IBARRE))) Then
                        'Q sur barre

                        'Nombre de Q
                        .NCR(ICAS, IBARRE) = .NCR(ICAS, IBARRE) + 1

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
                        .XFR1(ICAS, .NCR(ICAS, IBARRE), IBARRE) = x1
                        .XFR2(ICAS, .NCR(ICAS, IBARRE), IBARRE) = x2

                        .FRY1(ICAS, .NCR(ICAS, IBARRE), IBARRE) = -q1
                        .FRY2(ICAS, .NCR(ICAS, IBARRE), IBARRE) = -q2

                    End If

                Next
            Next
        End With
    End Sub
#End Region

End Class
