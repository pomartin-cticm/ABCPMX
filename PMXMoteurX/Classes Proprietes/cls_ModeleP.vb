Imports System.Runtime.InteropServices.ComTypes

Public Class cls_ModeleP

#Region " Structures et définitions "

    Const BOUCLEMAX As Integer = 1000
    Const Tolerance As Decimal = 0.05
    Const ToleranceR As Decimal = 10
    Const ToleranceS As Decimal = 1

#End Region

#Region " Paramètres de la classe "

    ''' <summary>
    ''' nombre d'éléments dans le modèle de la section
    ''' </summary>
    Public nbElts As Integer

    ''' <summary>
    ''' Liste des mailles du modèle
    ''' </summary>
    Public Mailles As List(Of Cls_Maille)

#End Region

#Region " Constructeur "

    Public Sub New()

        nbElts = 0
        Mailles = New List(Of Cls_Maille)

    End Sub

#End Region

#Region " Outils de création du modèle "

    ''' <summary>
    ''' Ajout d'une maille rectangulaire dans le modèle
    ''' </summary>
    ''' <param name="Aire">     [E] Aire de la maille           </param>
    ''' <param name="Epaisseur">[E] Epaisseur de la maille      </param>
    ''' <param name="zPos">     [E] Position Z                  </param>
    ''' <param name="DeltaC">   [E] Indicateur compression      </param>
    ''' <param name="DeltaT">   [E] Indicateur traction         </param>
    ''' <param name="nEq">      [E] Coefficient d'équivalence   </param>
    ''' <param name="fk">       [E] Valeur caractéristique de la Limite de comportement élastique</param>
    Public Sub AddMaille(Aire As Decimal, Epaisseur As Decimal, zPos As Decimal, DeltaT As Decimal, DeltaC As Decimal, nEq As Decimal,
                         fk As Decimal, kPl As Decimal, Gamma As Decimal,
                         Optional TypeM As Cls_Maille.EnuTypeMaille = Cls_Maille.EnuTypeMaille.Rectangulaire)
        '------------------------------------------------------------------------------------
        '   15/04/2023 : Création - POM
        '------------------------------------------------------------------------------------
        '   Ajout d'une maille rectangle
        '------------------------------------------------------------------------------------
        '   Aire        [E] :   Aire de la maille
        '   Epaisseur   [E] :   Epaisseur de la maille
        '   zPos        [E] :   Position z du centre de la maille
        '   DeltaT      [E] :   Indicateur pour le comportement en traction
        '   DeltaC      [E] :   Indicateur pour le comportement en compression
        '   nEq         [E] :   Coefficient d'équivalence
        '   fk          [E] :   Valeur caractéristique ou nominale de la limite d'élasticité
        '   kPl         [E] :   Coefficient appliqué à fk pour le calcul de la résistance plastique
        '   Gamma       [E] :   Coefficient partiel
        '   TypeM       [E] :   Type de la maille, par défaut rectangle
        '------------------------------------------------------------------------------------

        Mailles.Add(New Cls_Maille(Aire, Epaisseur, 0, 0, zPos, nEq, fk, kPl, Gamma, DeltaC, DeltaT, 1, TypeM))

    End Sub

    ''' <summary>
    ''' Ajout dans le modèle d'une maille circulaire
    ''' </summary>
    ''' <param name="Rayon">    [E] Rayon                       </param>
    ''' <param name="zPos">     [E] Position z du centre        </param>
    ''' <param name="DeltaC">   [E] Indice compression          </param>
    ''' <param name="DeltaT">   [E] Indice traction             </param>
    ''' <param name="nEq">      [E] Coefficient d'équivalence   </param>

    Public Sub AddMailleCirculaire(Rayon As Decimal, zPos As Decimal, DeltaT As Decimal, DeltaC As Decimal, nEq As Decimal,
                                   fk As Decimal, kPl As Decimal, Gamma As Decimal,
                                   Optional Nombre As Integer = 1, Optional TypeM As Cls_Maille.EnuTypeMaille = Cls_Maille.EnuTypeMaille.Circulaire)
        '------------------------------------------------------------------------------------
        '   26/04/2023 : Création - POM
        '------------------------------------------------------------------------------------
        '   Ajout d'une maille cercle
        '------------------------------------------------------------------------------------
        '   Rayon       [E] :   Rayon de la maille
        '   zPos        [E] :   Position z du centre de la maille
        '   DeltaT      [E] :   Indicateur pour le comportement en traction
        '   DeltaC      [E] :   Indicateur pour le comportement en compression
        '   nEq         [E] :   Coefficient d'équivalence
        '   fk          [E] :   Valeur caractéristique ou nominale de la limite d'élasticité
        '   kPl         [E] :   Coefficient appliqué à fk pour le calcul de la résistance plastique
        '   Gamma       [E] :   Coefficient partiel
        '   Nombre      [E] :   Nombre de mailles créées
        '   TypeM       [E] :   Type de la maille, par défaut cercle
        '------------------------------------------------------------------------------------

        Mailles.Add(New Cls_Maille(Math.PI * Rayon ^ 2, 0, Rayon, 0, zPos, nEq, fk, kPl, Gamma, DeltaC, DeltaT, Nombre, TypeM))

    End Sub

    Public Sub AddMailleConges(Rayon As Decimal, zPos As Decimal, DeltaT As Decimal, DeltaC As Decimal, nEq As Decimal,
                               fk As Decimal, kPl As Decimal, Gamma As Decimal,
                               TypeM As Cls_Maille.EnuTypeMaille, Optional Nombre As Integer = 1)
        '------------------------------------------------------------------------------------
        '   26/04/2023 : Création - POM
        '------------------------------------------------------------------------------------
        '   Ajout d'une maille congés de raccordement
        '------------------------------------------------------------------------------------
        '   Rayon       [E] :   Rayon de la maille
        '   zPos        [E] :   Position z du centre de la maille
        '   DeltaT      [E] :   Indicateur pour le comportement en traction
        '   DeltaC      [E] :   Indicateur pour le comportement en compression
        '   nEq         [E] :   Coefficient d'équivalence
        '   fk          [E] :   Valeur caractéristique ou nominale de la limite d'élasticité
        '   kPl         [E] :   Coefficient appliqué à fk pour le calcul de la résistance plastique
        '   Nombre      [E] :   Nombre de mailles créées
        '   TypeM       [E] :   Type de la maille, par défaut cercle
        '------------------------------------------------------------------------------------

        Mailles.Add(New Cls_Maille((4 - Math.PI) / 2 * Rayon ^ 2, 0, Rayon, 0, zPos, nEq, fk, kPl, Gamma, DeltaC, DeltaT, Nombre, TypeM))

    End Sub


#End Region

#Region " Outils "

    ''' <summary>
    ''' Renvoie les valeurs extrêmes des positions z dans le modèle
    ''' </summary>
    ''' <param name="zMin"> [S] valeur mini des z</param>
    ''' <param name="zMax"> [S] valeur maxi des z</param>
    Public Sub ExtremaZ(ByRef zMin As Decimal, ByRef zMax As Decimal)
        '------------------------------------------------------------------------------------
        '   15/04/2023 : Création - POM
        '------------------------------------------------------------------------------------

        '--> Boucle sur les mailles

        If Me.Mailles.Count > 0 Then

            zMin = Mailles(0).ExtremaZMin
            zMax = Mailles(0).ExtremaZMax

            For i As Integer = 1 To Mailles.Count - 1
                zMin = Math.Min(zMin, Mailles(i).ExtremaZMin)
                zMax = Math.Max(zMax, Mailles(i).ExtremaZMax)
            Next
        End If

    End Sub

#End Region

#Region " Outils de calcul pour les propriétés plastiques "

    Public Function CalculMomentPlastique(Signe As Decimal, zAxe As Decimal, lValeurCalcul As Boolean) As Decimal
        '-----------------------------------------------------------------------------------------
        '   29/04/2023 :    Création - POM
        '-----------------------------------------------------------------------------------------
        '   Calcul du moment plastique
        '-----------------------------------------------------------------------------------------
        '   Signe           [E] :   Signe du moment
        '   zAxe            [E] :   Position Axe Neutre Plastique
        '   lValeurCalcul   [E] :   Indique si valeur de calcul ou valeur caractéristique
        '-----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer

        Dim AddR As Decimal
        Dim ResultanteM As Decimal
        Dim IndexD As Integer

        '--> Initialisation

        ResultanteM = 0
        If lValeurCalcul Then IndexD = 0 Else IndexD = 1

        '--> Traitement

        For i = 0 To Me.Mailles.Count - 1

            AddR = Me.Mailles(i).ResultantePlastiqueM(Signe, zAxe, IndexD)
            ResultanteM += AddR

        Next i

        Return ResultanteM


    End Function

    Public Sub RechercheANP(Signe As Decimal, ByRef zANP As Decimal, lValeurCalcul As Boolean)
        '-----------------------------------------------------------------------------------------
        '   28/04/2023 :    Création - POM
        '-----------------------------------------------------------------------------------------
        '   Calcul de la position ANP 
        '-----------------------------------------------------------------------------------------
        '   Signe           [E] :   Signe du moment
        '   zANP            [S] :  Position Axe Neutre Plastique
        '   lValeurCalcul   [E] :   Indique si valeur de calcul ou valeur caractéristique
        '-----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim zMin, zMax As Decimal
        Dim lCont As Boolean = True
        Dim Boucle As Integer = 0
        Dim Result As Decimal

        '--> Initialisation

        Me.ExtremaZ(zMin, zMax)

        '--> Recherche par dichotomie

        Do While lCont
            Boucle += 1
            zANP = (zMin + zMax) / 2
            Result = ResultantePlastiqueN(Signe, zANP, lValeurCalcul)
            If Result > 0 Then
                zMin = zANP
            Else
                zMax = zANP
            End If
            If Math.Abs((zMax - zMin) * 1000) < Tolerance Then lCont = False
            If Boucle > BOUCLEMAX Then lCont = False
            If (Math.Abs(Result) < ToleranceR) Then lCont = False
        Loop

    End Sub

    Private Function ResultantePlastiqueN(Signe As Decimal, zAxe As Decimal, lValeurCalcul As Boolean) As Decimal
        '-----------------------------------------------------------------------------------------
        '   28/04/2023 :    Création - POM
        '-----------------------------------------------------------------------------------------
        '   Calcul de la position ANP 
        '-----------------------------------------------------------------------------------------
        '   Signe           [E] :   Signe du moment pour le duiagramme duquel on recherche la résultante
        '   zAxe            [E] :   Position de l'axe considéré
        '   lValeurCalcul   [E] :   Indique si valeur de calcul ou valeur caractéristique
        '-----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer

        Dim AddR As Decimal
        Dim Resultante As Decimal
        Dim IndexD As Integer

        '--> Initialisation

        Resultante = 0
        If lValeurCalcul Then IndexD = 0 Else IndexD = 1

        '--> Traitement

        For i = 0 To Me.Mailles.Count - 1

            AddR = Me.Mailles(i).ResultantePlastiqueN(Signe, zAxe, IndexD)
            Resultante += AddR

        Next i

        Return Resultante


    End Function
#End Region

#Region " Outils de calcul pour les propriétés élastiques "

    ''' <summary>
    ''' Recherche de la position de l'Axe Neutre Elastique par dichotomie 
    ''' </summary>
    ''' <param name="Signe">    [E] Signe du moment             </param>
    ''' <param name="zANE">     [S] Position ANE                </param>
    Public Sub RechercheANE(Signe As Decimal, ByRef zANE As Decimal)
        '-----------------------------------------------------------------------------------------
        '   15/04/2023 :    Création - POM
        '-----------------------------------------------------------------------------------------
        '   Calcul de la position ANE 
        '-----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim zMin, zMax As Decimal
        Dim lCont As Boolean = True
        Dim Boucle As Integer = 0
        Dim Mst As Decimal

        '--> Initialisation

        Me.ExtremaZ(zMin, zMax)

        '--> Recherche par dichotomie

        Do While lCont
            Boucle += 1
            zANE = (zMin + zMax) / 2
            Mst = MomentStatique(Signe, zANE)
            If Mst > 0 Then
                zMin = zANE
            Else
                zMax = zANE
            End If
            If Math.Abs((zMax - zMin) * 1000) < Tolerance Then lCont = False
            If Boucle > BOUCLEMAX Then lCont = False
            If (Math.Abs(Mst * 1000 ^ 3) < ToleranceS) Then lCont = False
        Loop
    End Sub

    ''' <summary>
    ''' Calcul du moment statique cumulé de tous les éléments du modèle
    ''' </summary>
    ''' <param name="Signe">    [E] Signe du moment             </param>
    ''' <param name="zAxe">     [S] Position ANE                </param>
    ''' <returns></returns>
    Private Function MomentStatique(Signe As Decimal, zAxe As Decimal) As Decimal
        '-------------------------------------------------------------------------------
        '   16/03/2023 :    Création - POM
        '-------------------------------------------------------------------------------
        '   Recherche du moment statique d'un maillage
        '-------------------------------------------------------------------------------
        '   zAxe        [S] :   Position de l'axe de référence
        '-------------------------------------------------------------------------------

        '--> Déclaration

        Dim i As Integer

        Dim AddM As Decimal
        Dim Mstat As Decimal

        '--> Initialisation

        Mstat = 0

        '--> Traitement

        For i = 0 To Me.Mailles.Count - 1

            AddM = Me.Mailles(i).MomentStatique(Signe, zAxe)
            Mstat += AddM

        Next i

        Return Mstat

    End Function

    ''' <summary>
    ''' Calcul de l'inertie des éléments du modèle
    ''' </summary>
    ''' <param name="Signe">    [E] Signe du moment     </param>
    ''' <param name="zAxe">     [E] Position z de l'axe </param>
    ''' <returns></returns>
    Public Function InertieFlexion(Signe As Decimal, zAxe As Decimal) As Decimal
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

        Dim AddI As Double

        '--> Calcul

        Iy = 0

        For i = 0 To Me.Mailles.Count - 1

            AddI = Me.Mailles(i).InertieFlexionY(Signe, zAxe)

            Iy += AddI
        Next i

        Return Iy
    End Function



#End Region


End Class
