Imports PMXMoteur2

Module Mod_Outils

#Region " Outils de COMPARaison "

    Public Const DeltaVMAx As Decimal = 1 / 1000

    Public Function IsEqual(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        If Math.Abs(b) <= EPS Then
            'AVEC DIMENSION
            Return Math.Abs(a) <= EPS
        Else
            'ATTENTION : Lorsqu'on compare la fraction (PAS DE DIMENSION), il faut utiliser 0.001
            'Return Math.Abs(a / b - 1) <= 0.001
            Return Math.Abs(a / b - 1) <= EPS
        End If
    End Function

    Private Function IsGreater(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        'NE PAS UTILISER POUR CHERCHER LA VALEUR MAX/MIN

        Return (Not IsEqual(a, b, EPS)) AndAlso (a > b)
    End Function

    Private Function IsGreaterOrEqual(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        Return IsEqual(a, b, EPS) OrElse (a > b)

    End Function

    Private Function IsSmaller(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------
        Dim lIsSmaller As Boolean = (Not IsEqual(a, b, EPS)) AndAlso (a < b)

        'NE PAS UTILISER POUR CHERCHER LA VALEUR MAX/MIN

        Return lIsSmaller
    End Function

    Private Function IsSmallerOrEqual(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean

        '------------------------------------------
        ' 29/11/2013
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        Return IsEqual(a, b, EPS) OrElse (a < b)
    End Function

#End Region

#Region " Outils de génération des configurations pour les tests "

    Public Sub GenereProfileIPE300(ByRef MyProfil As cls_ProfilA)
        '----------------------------------------------------------------------------------------------------------------------------------
        '   04/11/23 :  Création POM
        '----------------------------------------------------------------------------------------------------------------------------------
        '   Génération d'un profilé laminé IPE 300
        '----------------------------------------------------------------------------------------------------------------------------------

        MyProfil.ha = 0.3
        MyProfil.Bfi = 0.15
        MyProfil.Bfs = 0.15
        MyProfil.Tfi = 0.0107
        MyProfil.Tfs = 0.0107
        MyProfil.Tw = 0.0071
        MyProfil.Rci = 0.015
        MyProfil.Rcs = 0.015
        MyProfil.typeProfileAcier = cls_ProfilA.Enum_TypeSectionAcier.Lamine

    End Sub


    Public Sub GenereChargeConcentree(iTravee As Integer, xAppui As Decimal, xPosT As Decimal, Force As Decimal,
                                      ByRef MyChargeU As cls_ChargementUtilisateur)
        '----------------------------------------------------------------------------------------------------------------------------------
        '   04/11/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------------------
        '   Génére une charge utilisateur avec une force concentrée
        '----------------------------------------------------------------------------------------------------------------------------------
        '   iTravee     [E] :   Indice de la travée
        '   xAppui      [E] :   Position de l'appui gauche
        '   xPosT       [E] :   Position dans la travée
        '   Force       [E] :   Force
        '   MyChargeU   [S] :   Charge utilisateur générée
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        MyChargeU.Forces(iTravee).Add(New cls_Force(xPosT, Force, xAppui))

    End Sub

    Public Sub GenereChargeSurfacique(iTravP As Integer, iTravD As Integer, QSurf As Decimal,
                                      ByRef MyChargeU As cls_ChargementUtilisateur)
        '----------------------------------------------------------------------------------------------------------------------------------
        '   04/11/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------------------
        '   Génére une charge utilisateur avec une charge uniformément répartie sur toutes les travées
        '----------------------------------------------------------------------------------------------------------------------------------
        '   iTravP      [E] :   Indice première travée
        '   iTravD      [E] :   Indice dernière travée
        '   QSurf       [E] :   Valeur de la charge surfacique
        '   MyChargeU   [S] :   Charge utilisateur générée
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        '--> Boucle sur les travées

        For i As Integer = iTravD To iTravP

            MyChargeU.QSurf(i) = QSurf

        Next


    End Sub

    Public Sub GenereChargeUniformementRepartie(iTravP As Integer, iTravD As Integer, LongueurT() As Decimal, ChargeRep As Decimal,
                                                ByRef MyChargeU As cls_ChargementUtilisateur)
        '----------------------------------------------------------------------------------------------------------------------------------
        '   04/11/23 :  Création - POM
        '----------------------------------------------------------------------------------------------------------------------------------
        '   Génére une charge utilisateur avec une charge uniformément répartie sur toutes les travées
        '----------------------------------------------------------------------------------------------------------------------------------
        '   iTravP      [E] :   Indice première travée
        '   iTravD      [E] :   Indice dernière travée
        '   LongueurT   [E] :   Longueur de travée
        '   ChargeRep   [E] :   Valeur de la charge répartie
        '   MyChargeU   [S] :   Charge utilisateur générée
        '----------------------------------------------------------------------------------------------------------------------------------

        '--> Déclaration

        Dim Cumul As Decimal = 0

        '--> Initialisation

        ' MyChargeU = New cls_ChargementUtilisateur("TU", iTravD)
        MyChargeU = New cls_ChargementUtilisateur(iTravD)

        '--> Boucle sur les travées

        For i As Integer = iTravD To iTravP

            MyChargeU.FReparties(i).Add(New cls_ForceRepartie(0, ChargeRep, LongueurT(i), ChargeRep, Cumul))

            Cumul += LongueurT(i)
        Next

    End Sub


#End Region

End Module
