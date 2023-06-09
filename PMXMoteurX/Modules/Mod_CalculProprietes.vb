
Module Mod_CalculProprietes

#Region " Déclarations "

    Const BOUCLEMAX As Integer = 1000
    Const Tolerance As Decimal = 0.05
    Const ToleranceR As Decimal = 1
    Const ToleranceS As Decimal = 1

    Public Const kConvMPaPa As Decimal = 1000 ^ 2

#End Region

#Region " Propriétés élastiques "

    ''' <summary>
    ''' Calcul des propriétés élastiques et plastiques de la section
    ''' </summary>
    ''' <param name="MySection">    [E] Section à calculer              </param>
    ''' <param name="Signe">        [E] Signe du moment                 </param>
    ''' <param name="zANE">         [S] Position Axe Neutre Elastique   </param>
    ''' <param name="InertieY">     [S] Moment d'inertie de flexion     </param>

    'Public Sub CalProprietes(MySection As cls_Section, Signe As Decimal, nEqEc As Decimal, nEqDalle As Decimal, lValeurCalcul As Boolean,
    '                         ByRef zANE As Decimal, ByRef InertieY As Decimal, ByRef zANP As Decimal, ByRef MplRd As Decimal)
    '    '-----------------------------------------------------------------------------------------
    '    '   15/04/2023 :    Création - POM
    '    '-----------------------------------------------------------------------------------------
    '    '   Calcul des propriétés élastiques d'une section
    '    '-----------------------------------------------------------------------------------------
    '    '   Référence z:    fibre supérieure du profilé en acier
    '    '                   z > 0 au dessus du profilé
    '    '-----------------------------------------------------------------------------------------
    '    '   MySection       [E] :   Section calculée
    '    '   Signe           [E] :   Signe du moment de flexion
    '    '   nEqEc           [E] :   Coefficient d'équivalence acier béton pour l'enrobage
    '    '   nEqDalle        [E] :   Coefficient d'équivalence acier béton pour la dalle
    '    '   lValeurCalcul   [E] :   Indique si valeur de calcul ou valeur caractéristique
    '    '
    '    '   zANE            [S] :   Position ANE / fibre sup du profilé
    '    '   InertieY        [S] :   Inertie de flexion élastique / axe YY
    '    '   zANP            [S] :   Position ANP / fibre sup du profilé
    '    '   MplRd           [S] :   Moment plastique
    '    '-----------------------------------------------------------------------------------------

    '    '--> Déclaration

    '    Dim MyModele As New cls_ModeleP
    '    Dim FySup, FyInf, FyW As Decimal
    '    Dim lLamine As Boolean = MySection.lLamine
    '    Dim Aire, Epaisseur, Largeur, Fd, zPos As Decimal
    '    Dim GammaM As Decimal = MySection.Param.Gamma_M0
    '    Dim GammaC As Decimal = MySection.Param.Gamma_C
    '    Dim GammaS As Decimal = MySection.Param.Gamma_S
    '    Dim Hw As Decimal = MySection.ProfilA.HauteurAmeHw

    '    Dim lMixte As Boolean = MySection.lMixte
    '    Dim lModeleRenformis As Boolean = False
    '    Dim DeltaCArma As Decimal = 0
    '    Dim lArmaComp As Boolean

    '    '--> Initialisation

    '    FySup = MySection.FySup
    '    FyW = MySection.FyW
    '    FyInf = MySection.FyInf
    '    MySection.dalle.AcierArmatures.Es = MySection.Param.ArmaYoung
    '    MySection.enrobage_partiel.AcierArmatures.Es = MySection.Param.ArmaYoung
    '    lArmaComp = MySection.Param.lArmaComprimee
    '    If lArmaComp Then DeltaCArma = 1 Else DeltaCArma = 0
    '    Dim RhoV As Decimal

    '    '--> Mise à jour de la section

    '    MySection.InitialisePositionArmaturesEnrobage()

    '    '--> Modélisation du profilé acier

    '    '# Semelle supérieure

    '    MyModele.AddMaille(MySection.ProfilA.AireFs, MySection.ProfilA.t_fs, -MySection.ProfilA.t_fs / 2, 1, 1, 1, FySup, 1, GammaM)

    '    '# Âme

    '    RhoV = MySection.RhoVCalcul
    '    MyModele.AddMaille(Hw * MySection.ProfilA.t_w, Hw, -MySection.ProfilA.t_fs - Hw / 2, 1, 1, 1, FyW, (1 - RhoV), GammaM)

    '    '# Semelle inférieure

    '    MyModele.AddMaille(MySection.ProfilA.AireFi, MySection.ProfilA.t_fi, -MySection.ProfilA.ha + MySection.ProfilA.t_fi / 2, 1, 1, 1, FyInf, 1, GammaM)

    '    '# Congés de raccordement

    '    If lLamine Then

    '        '# Congés supérieurs

    '        MyModele.AddMailleConges(MySection.ProfilA.r_cs, -MySection.ProfilA.t_fs, 1, 1, 1, FyW, (1 - RhoV), GammaM, Cls_Maille.EnuTypeMaille.CongeSup)

    '        '# Congés supérieurs

    '        MyModele.AddMailleConges(MySection.ProfilA.r_ci, -MySection.ProfilA.ha + MySection.ProfilA.t_fs, 1, 1, 1, FyW, (1 - RhoV), GammaM, Cls_Maille.EnuTypeMaille.CongeInf)

    '    End If

    '    '# Béton d'enrobage

    '    If MySection.lEnrobage Then

    '        Largeur = (MySection.LargeurEnrobagePartielBc - MySection.ProfilA.t_w)
    '        Epaisseur = MySection.ProfilA.HauteurAmeHw
    '        Fd = MySection.enrobage_partiel.beton.Fck
    '        MyModele.AddMaille(Largeur * Epaisseur, Epaisseur, -MySection.ProfilA.ha / 2, 0, 1, nEqEc, Fd, 0.85, GammaC, Cls_Maille.EnuTypeMaille.Rectangulaire)

    '        'Pour les profilés laminés, on doit retirer la parties correspondant aux congés

    '        If lLamine Then

    '            '# Congés supérieurs

    '            MyModele.AddMailleConges(MySection.ProfilA.r_cs, -MySection.ProfilA.t_fs, 0, 1, nEqEc, Fd, 0.85, GammaC, Cls_Maille.EnuTypeMaille.CongeSup, -1)

    '            '# Congés supérieurs

    '            MyModele.AddMailleConges(MySection.ProfilA.r_ci, -MySection.ProfilA.ha + MySection.ProfilA.t_fs, 0, 1, nEqEc, Fd, 0.85, GammaC, Cls_Maille.EnuTypeMaille.CongeInf, -1)

    '        End If

    '    End If

    '    '# Armatures de l'enrobage

    '    If MySection.lEnrobage Then

    '        Dim ArmaNb As Integer
    '        Dim ArmaPhi As Decimal
    '        Dim ArmaNeq As Decimal = MySection.acier.EYoung / MySection.enrobage_partiel.AcierArmatures.Es

    '        For i As Integer = 0 To 2

    '            ArmaNb = MySection.enrobage_partiel.LitsArma(i).nbArma * 2
    '            ArmaPhi = MySection.enrobage_partiel.LitsArma(i).Phi

    '            Aire = Math.PI * ArmaPhi ^ 2 / 4

    '            If MySection.lArmaturesConcentrees Then
    '                MyModele.AddMaille(ArmaNb * Aire, 0, MySection.enrobage_partiel.LitsArma(i).zArma, 1, DeltaCArma,
    '                                   ArmaNeq, Fd, 0.85, GammaS, Cls_Maille.EnuTypeMaille.CercleConcentre)
    '            Else
    '                MyModele.AddMailleCirculaire(ArmaPhi / 2, MySection.enrobage_partiel.LitsArma(i).zArma, 1, DeltaCArma,
    '                                             ArmaNeq, Fd, 0.85, GammaS, ArmaNb, Cls_Maille.EnuTypeMaille.Circulaire)
    '            End If

    '        Next

    '    End If

    '    '--> Dalle béton

    '    If lMixte Then

    '        '# Dalle

    '        Largeur = MySection.dalle.Beff
    '        Epaisseur = MySection.dalle.EpaisseurActive
    '        Fd = MySection.dalle.beton.Fck
    '        zPos = MySection.dalle.EpRenformis + MySection.dalle.t_d - Epaisseur / 2

    '        MyModele.AddMaille(Largeur * Epaisseur, Epaisseur, zPos, 0, 1, nEqDalle, Fd, 0.85, GammaC, Cls_Maille.EnuTypeMaille.Rectangulaire)

    '        '# Renformis

    '        If lModeleRenformis And (MySection.dalle.type = Cls_Dalle.Enum_TypeDalle.Pleine) Then

    '            Largeur = MySection.ProfilA.b_fs
    '            Epaisseur = MySection.dalle.t_h

    '            If (Epaisseur > 0) Then

    '                zPos = MySection.dalle.EpRenformis + Epaisseur / 2

    '                MyModele.AddMaille(Largeur * Epaisseur, Epaisseur, zPos, 0, 1, nEqDalle, Fd, 0.85, GammaC, Cls_Maille.EnuTypeMaille.Rectangulaire)

    '            End If

    '        End If

    '        '# Armatures

    '        Dim NEqArmaD As Decimal = MySection.acier.EYoung / MySection.dalle.AcierArmatures.Es
    '        Dim Fsk As Decimal = MySection.dalle.AcierArmatures.FsK
    '        Dim NombreS As Decimal

    '        For iArma As Integer = 0 To 1

    '            Dim zS, PhiS As Decimal

    '            If MySection.dalle.LitArma(iArma).lActive Then

    '                zS = MySection.dalle.zTop - MySection.dalle.LitArma(iArma).z_s
    '                PhiS = MySection.dalle.LitArma(iArma).PhiS
    '                NombreS = MySection.dalle.Beff / MySection.dalle.LitArma(iArma).EspBar

    '                If MySection.lArmaturesConcentrees Then
    '                Else
    '                    MyModele.AddMailleCirculaire(PhiS / 2, zS, 1, DeltaCArma, NEqArmaD, Fsk, 1, GammaS, NombreS)
    '                End If

    '            End If

    '        Next

    '    End If

    '    '--> Recherche de l'axe neutre élastique

    '    MyModele.RechercheANE(Signe, zANE)
    '    'RechercheANE(MyModele, Signe, zANE)

    '    '--> Calcul de l'inertie

    '    'InertieY = InertieFlexionY(MyModele, zANE, Signe)
    '    InertieY = MyModele.InertieFlexionY(Signe, zANE)

    '    '--> Recherche de l'axe neutre plastique

    '    MyModele.RechercheANP(Signe, zANP, lValeurCalcul)

    '    '--> Moment plastique

    '    MplRd = MyModele.CalculMomentPlastique(Signe, zANP, lValeurCalcul)

    'End Sub

    ''' <summary>
    ''' Recherche de la position de l'Axe Neutre Elastique par dichotomie 
    ''' </summary>
    ''' <param name="MyModele"> [E] Modèlisation de la section  </param>
    ''' <param name="Signe">    [E] Signe du moment             </param>
    ''' <param name="zANE">     [S] Position ANE                </param>
    Private Sub RechercheANE(MyModele As cls_ModeleP, Signe As Decimal, ByRef zANE As Decimal)
        '-----------------------------------------------------------------------------------------
        '   15/04/2023 :    Création - POM
        '-----------------------------------------------------------------------------------------
        '   Calcul de la position ANE ==> A mettre dans la classe ?
        '-----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim zMin, zMax As Decimal
        Dim lCont As Boolean = True
        Dim Boucle As Integer = 0
        Dim Mst As Decimal

        '--> Initialisation

        MyModele.ExtremaZ(zMin, zMax)

        '--> Recherche par dichotomie

        Do While lCont
            Boucle += 1
            zANE = (zMin + zMax) / 2
            Mst = MomentStatique(MyModele, Signe, zANE)
            If Mst > 0 Then
                zMin = zANE
            Else
                zMax = zANE
            End If
            If Math.Abs(zMax - zMin) < Tolerance Then lCont = False
            If Boucle > BOUCLEMAX Then lCont = False
            If Math.Abs(Mst) < ToleranceS Then lCont = False
        Loop
    End Sub

    ''' <summary>
    ''' Calcul du moment statique cumulé de tous les éléments du modèle
    ''' </summary>
    ''' <param name="MyModele"> [E] Modèlisation de la section  </param>
    ''' <param name="Signe">    [E] Signe du moment             </param>
    ''' <param name="zAxe">     [S] Position ANE                </param>
    ''' <returns></returns>
    Private Function MomentStatique(MyModele As cls_ModeleP, Signe As Decimal, zAxe As Decimal) As Decimal
        '-------------------------------------------------------------------------------
        '   16/03/2023 :    Création - POM
        '-------------------------------------------------------------------------------
        '   Recherche du moment statique d'un maillage
        '-------------------------------------------------------------------------------
        '   zAxe        [S] :   Position de l'axe de référence
        '-------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer
        Dim DeltaZ As Decimal
        Dim DeltaFunction(0 To 1) As Decimal
        Dim p As Decimal

        Dim DeltaZP, DeltaZM As Decimal
        Dim DeltaAp, DeltaAm As Decimal
        Dim AddM As Decimal
        Dim Mstat As Decimal

        '--> Initialisation

        Mstat = 0

        '--> Traitement

        For i = 0 To MyModele.Mailles.Count - 1
            DeltaFunction(1) = MyModele.Mailles(i).DeltaT
            DeltaFunction(0) = MyModele.Mailles(i).DeltaC

            DeltaZ = MyModele.Mailles(i).zPos - zAxe
            p = Math.Max(Math.Min(1 / 2 + DeltaZ / MyModele.Mailles(i).t, 1), 0)

            DeltaZP = DeltaZ + MyModele.Mailles(i).t * (1 - p) / 2
            DeltaZM = DeltaZ - MyModele.Mailles(i).t * (p) / 2

            DeltaAp = DeltaFunction(1 / 2 * (1 + Signe))
            DeltaAm = DeltaFunction(1 / 2 * (1 - Signe))

            AddM = MyModele.Mailles(i).Aire / MyModele.Mailles(i).n * (DeltaZ * (p * DeltaAp + (1 - p) * DeltaAm) + MyModele.Mailles(i).t / 2 * (1 - p) * p * (DeltaAp - DeltaAm))
            Mstat += AddM

        Next i

        Return Mstat

    End Function

    Private Function InertieFlexionY(MyModele As cls_ModeleP, zAxe As Decimal, Signe As Decimal)
        '-------------------------------------------------------------------------------
        '   16/03/2023 :    Création - POM
        '-------------------------------------------------------------------------------
        '   Calcul de l'Inertie d'un maillage
        '-------------------------------------------------------------------------------
        '   Inertie     [S] :   Aire de la section
        '   zAXE        [E] :   Position axe de référence
        '   Moment      [E] :   Signe du moment
        '-------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer
        Dim Iy As Double
        Dim DeltaZ As Double

        Dim DeltaAp As Double
        Dim DeltaAm As Double
        Dim AddI As Double

        '--> Calcul

        Iy = 0

        Dim DeltaFunction(0 To 1) As Double
        Dim p As Double
        Dim DeltaZP As Double
        Dim DeltaZM As Double

        For i = 0 To MyModele.Mailles.Count - 1
            DeltaZ = MyModele.Mailles(i).zPos - zAxe

            DeltaFunction(1) = MyModele.Mailles(i).DeltaT
            DeltaFunction(0) = MyModele.Mailles(i).DeltaC

            DeltaZ = MyModele.Mailles(i).zPos - zAxe
            p = Math.Max(Math.Min(1 / 2 + DeltaZ / MyModele.Mailles(i).t, 1), 0)

            DeltaZP = DeltaZ + MyModele.Mailles(i).t * (1 - p) / 2
            DeltaZM = DeltaZ - MyModele.Mailles(i).t * (p) / 2

            DeltaAp = DeltaFunction(1 / 2 * (1 + Signe))
            DeltaAm = DeltaFunction(1 / 2 * (1 - Signe))


            AddI = MyModele.Mailles(i).Aire / MyModele.Mailles(i).n * (DeltaAp * p * (DeltaZP ^ 2 + p ^ 2 * MyModele.Mailles(i).t ^ 2 / 12) _
                                                                             + DeltaAm * (1 - p) * (DeltaZM ^ 2 + (1 - p) ^ 2 * MyModele.Mailles(i).t ^ 2 / 12))

            Iy += AddI
        Next i

        Return Iy
    End Function

#End Region


End Module
