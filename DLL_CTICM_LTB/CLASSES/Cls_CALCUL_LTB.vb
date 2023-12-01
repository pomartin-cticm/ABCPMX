Public Class CALCUL_LTB

#Region " VARIABLES INTERNES "
    Dim MATERIAU As LTB_MATERIAU
    Dim BARRES As LTB_BARRES
    Dim NOEUDS As LTB_NOEUDS
    Dim SOLLICITATIONS As LTB_SOLLICITATIONS
    Dim CHARGESEXCENTREES As LTB_CHARGESEXCENTREES
    Dim RESOLUTION As LTB_RESOLUTION
    Dim RESULTATS As LTB_RESULTATS
#End Region

#Region " MODELE E.F "

    Private Sub CREATE_MODEL_LTB(ByVal Donnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees)
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
        Dim iNode, iNode1, iNode2, iBarre As Integer
        Dim lTrouve As Boolean
        Dim xLeft, xRight As Double

        '=== Matériaux
        MATERIAU.E = Donnees.EYOUNG
        MATERIAU.G = Donnees.GSHEAR

        '=== BARRES
        With BARRES
            .NBELEM = Donnees.NbNodes - 1
            ReDim .LONGPROJ(.NBELEM)
            ReDim .IZ(.NBELEM)
            ReDim .IT(.NBELEM)
            ReDim .IW(.NBELEM)
            ReDim .BETAZ(.NBELEM)
            ReDim .ZCG(.NBELEM)
            ReDim .RYZ(.NBELEM)
            ReDim .KT1(.NBELEM)
            ReDim .KVP1(.NBELEM)
            ReDim .KTP1(.NBELEM)

            For iBarre = 1 To Donnees.NbNodes - 1
                .LONGPROJ(iBarre) = Donnees.xNode(iBarre) - Donnees.xNode(iBarre - 1)
                .IZ(iBarre) = Donnees.InertieZ(iBarre - 1)
                .IT(iBarre) = Donnees.InertieT(iBarre - 1)
                .IW(iBarre) = Donnees.InertieW(iBarre - 1)
                .BETAZ(iBarre) = Donnees.CoefBetaZ(iBarre - 1)
                .ZCG(iBarre) = Donnees.PositionCG(iBarre - 1)
                .RYZ(iBarre) = Donnees.RayGirPol(iBarre - 1)

                'Relaxation des rigidites
                .KT1(iBarre) = -1       'Continu
                .KVP1(iBarre) = -1      'Continu
                .KTP1(iBarre) = -1      'Continu
            Next

        End With

        '=== Noeuds        
        With NOEUDS
            ReDim .ZN(Donnees.NbNodes)      '=0 : pas de variation de hauteur de section
            ReDim .RV(Donnees.NbNodes)
            ReDim .RVP(Donnees.NbNodes)
            ReDim .RT(Donnees.NbNodes)
            ReDim .RTP(Donnees.NbNodes)
            ReDim .ZRC(Donnees.NbNodes)

            For I = 0 To Donnees.NbMaintiensPon - 1
                iNode = Donnees.iNodeMaintienPon(I)
                .RV(iNode + 1) = Donnees.MaintienPonV(I)
                .RVP(iNode + 1) = Donnees.MaintienPonVP(I)
                .RT(iNode + 1) = Donnees.MaintienPonTheta(I)
                .RTP(iNode + 1) = Donnees.MaintienPonThetaP(I)
                .ZRC(iNode + 1) = Donnees.zMaintienPonC(I)
            Next

            For I = 0 To Donnees.NbMaintiensCon - 1
                iNode1 = Donnees.iNodeMaintienCon(I, 0)
                iNode2 = Donnees.iNodeMaintienCon(I, 1)

                For J = iNode1 To iNode2

                    lTrouve = False
                    For K = 0 To Donnees.NbMaintiensPon - 1
                        iNode = Donnees.iNodeMaintienPon(K)
                        If J = iNode Then
                            lTrouve = True
                            Exit For
                        End If
                    Next

                    'Longueur d'élément
                    If J = iNode1 Then
                        xLeft = Donnees.xNode(J)
                    Else
                        xLeft = Donnees.xNode(J - 1)
                    End If
                    If J = iNode2 Then
                        xRight = Donnees.xNode(J)
                    Else
                        xRight = Donnees.xNode(J + 1)
                    End If

                    If Not lTrouve Then
                        'Le maintien ponctuel domine le maintien continu
                        'Le maintien continu s'applique s'il n'y a pas de maintien ponctuel
                        .RV(J + 1) = Donnees.MaintienConV(I) * (xRight - xLeft) / 2
                        .RVP(J + 1) = Donnees.MaintienConVP(I) * (xRight - xLeft) / 2
                        .RT(J + 1) = Donnees.MaintienConTheta(I) * (xRight - xLeft) / 2
                        .ZRC(J + 1) = Donnees.zMaintienConC(I)
                    End If
                Next
            Next
        End With


    End Sub

    Private Sub LOADING_LTB(ByVal Donnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees)
        '-----------------------------------------------------
        '
        ' 02/11/2023 : TMN, v 1.0.0
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
        Dim iElem As Integer

        With SOLLICITATIONS
            ReDim .FN(Donnees.NbNodes - 1)
            ReDim .MY1(Donnees.NbNodes - 1)
            ReDim .MY2(Donnees.NbNodes - 1)
            .FNBLOCKED = True
            .MYBLOCKED = False

            For iElem = 1 To Donnees.NbNodes - 1
                .MY1(iElem) = Donnees.MomentFle(iElem - 1, 0)
                .MY2(iElem) = Donnees.MomentFle(iElem - 1, 1)
            Next
        End With

        '======== CHARGES EXCENTREES

        With CHARGESEXCENTREES
            'nb de charges concentrées FZ excentrées / C dans la barre physique
            .NFZ = Donnees.NbForcesPon
            'abscisse sur OX de FZ()
            ReDim .XLFZ(.NFZ)
            'charge FZ (composante suivant Z)
            ReDim .FZ(.NFZ)
            'ordonnée du point d'application de FZ() / C
            ReDim .ZFZC(.NFZ)
            For i = 1 To Donnees.NbForcesPon
                .XLFZ(i) = Donnees.xForcePon(i - 1)
                .FZ(i) = Donnees.ForcePon(i - 1)
                .ZFZC(i) = Donnees.zForcePonC(i - 1)
            Next

            'nb de charges réparties QZ excentrées / C dans la barre physique
            .NQZ = Donnees.NbForcesRep

            'abscisse sur OX de l'origine de la charge QZ
            ReDim .XLQZ1(.NQZ)
            'valeur à l'origine de la charge répartie QZ
            ReDim .QZ1(.NQZ)
            'abscisse sur OX de l'extrémité de la charge QZ            
            ReDim .XLQZ2(.NQZ)
            'valeur à l'extrémité de la charge répartie QZ
            ReDim .QZ2(.NQZ)
            'ordonnée du point d'application de QZ() / C
            ReDim .ZQZC(.NQZ)
            For i = 1 To Donnees.NbForcesRep
                .XLQZ1(i) = Donnees.xForceRep(i - 1, 0)
                .QZ1(i) = Donnees.ForceRep(i - 1, 0)
                .XLQZ2(i) = Donnees.xForceRep(i - 1, 1)
                .QZ2(i) = Donnees.ForceRep(i - 1, 1)
                .ZQZC(i) = Donnees.zForceRepC(i - 1)
            Next

        End With
    End Sub

#End Region

#Region " LANCER LE CALCUL "

    Public Sub CALCULER(ByVal Donnees As CTICM_DATA_DLLS.DATA_DLLS.Struc_Donnees,
                        ByRef Output_LTB As DATA_LTB.Struc_Output,
                        ByRef ErrorCode As Integer,
                        ByRef ErrorText As String)
        '-----------------------------------------------------
        '
        ' 02/11/2023 : TMN, v 1.0.0
        '
        '-----------------------------------------------------
        '
        '   LANCER LE CALCUL
        '
        '-----------------------------------------------------
        '
        '   DONNEES         [E] : Données d'entrée        
        '
        '-----------------------------------------------------
        '
        '   Output_LTB      [S] : Resultats d'analyse
        '   ErrorCode       [S] : code d'erreur (=0) s'il n'y a pas d'erreur
        '   ErrorText       [S] : texte d'erreur
        '
        '-----------------------------------------------------                

        'Créer le modèle E.F
        CREATE_MODEL_LTB(Donnees)

        'Chargement
        LOADING_LTB(Donnees)

        'Options de calcul
        With RESOLUTION
            .NBVALP = 1
            .NOVECTP = False
            .TOLERANCE = 0.0001
            '.DUMP = 0
        End With

        LTBNSolve(MATERIAU, BARRES, NOEUDS, SOLLICITATIONS, CHARGESEXCENTREES, RESOLUTION, RESULTATS)

        ErrorCode = RESULTATS.CODEERROR
        ErrorText = RESULTATS.TEXTERROR

        UPDATE_RESULTS(Output_LTB)
    End Sub

#End Region

#Region " RESULTATS D'ANALYSE "

    Private Sub UPDATE_RESULTS(ByRef OUTPUT As DATA_LTB.Struc_Output)
        '=============================================
        '
        ' 07/11/2023 : TMN, v 1.00
        '
        '=============================================
        '
        ' MISE A JOUR DES RESULTATS DU CALCUL LTB
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
        Dim NbNodes As Integer = NOEUDS.ZN.GetUpperBound(0)

        OUTPUT.CoefCr = RESULTATS.VALP(1)
        OUTPUT.MomentCr = RESULTATS.MOMENTMAX * RESULTATS.VALP(1)

        ReDim OUTPUT.V(NbNodes)
        ReDim OUTPUT.VP(NbNodes)
        ReDim OUTPUT.Theta(NbNodes)
        ReDim OUTPUT.ThetaP(NbNodes)

        For i = 1 To NbNodes
            OUTPUT.V(i - 1) = RESULTATS.VECTP(1, 4 * (i - 1) + 1)
            OUTPUT.Theta(i - 1) = RESULTATS.VECTP(1, 4 * (i - 1) + 2)
            OUTPUT.VP(i - 1) = RESULTATS.VECTP(1, 4 * (i - 1) + 3)
            OUTPUT.ThetaP(i - 1) = RESULTATS.VECTP(1, 4 * (i - 1) + 4)
        Next
    End Sub

#End Region

End Class
