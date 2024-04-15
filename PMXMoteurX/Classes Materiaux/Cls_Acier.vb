

Public Class cls_Acier

#Region " Déclarations "
    Public Const EYACIER As Decimal = 210 * 1000
    Public Const RHOACIER As Decimal = 7850
    Public Const NUANCEDEFAULT As String = "S355"
    Public Const NU As Decimal = 0.3

    Public Structure strucPlage
        Dim Ep As Double
        Dim Fy As Double
        Dim Fu As Double
    End Structure
#End Region

#Region " Attributs "

    ''' <summary>
    ''' nuance de l'acier : S235, S275, S355, S420 ou S460
    ''' </summary>
    Public Nuance As String

    ''' <summary>
    ''' qualité de l'acier : EC3,JR,M,J0W ou MLO
    ''' </summary>
    Public Qualite As String

    Public Reduction As String

    Public NormeProduit As String

    Public EpMax As Double
    Public iBase As Short
    Public iTabStandart, iStandart As Short

    '-------------------------------------------------------------------------
    ' iStandart : no de norme dans la base acier, entre 1 et 20
    ' iTabStandart : no de norme dans la table interne, entre 1 et nStandart
    '-------------------------------------------------------------------------

    Public Plages As List(Of strucPlage)

#End Region

#Region " Propriétés "

    Public Function get_epsilon(fy As Decimal)
        Return Math.Sqrt(235 / fy)
    End Function

    ''' <summary>
    ''' Masse volumique (en kg/m3)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Rho As Decimal
        Get
            Return RHOACIER
        End Get
    End Property

    ''' <summary>
    ''' Module d'Young de l'acier (en MPa)
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property EYoung As Decimal
        Get
            Return EYACIER
        End Get
    End Property

    ''' <summary>
    ''' Coefficient de Poisson
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Poisson As Decimal
        Get
            Return NU
        End Get
    End Property

    ''' <summary>
    ''' Module de cisaillement
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ModuleG As Decimal
        Get
            Return EYACIER / (2 * (1 + NU))
        End Get
    End Property

    Public Function LimiteFy(ByVal Epaisseur As Double) As Decimal
        '--------------------------------------------------------------------------------
        '
        '   04/07/12 :  Création - v3.00 - POM
        '
        '--------------------------------------------------------------------------------
        '
        '   Retourne la limite d'élasticité en fonction de l'épaisseur
        '
        '--------------------------------------------------------------------------------
        '--------------------------------------------------------------------------------

        '--> Déclaration

        Dim MyFy As Decimal = 0

        Dim NbPl As Integer = Me.Plages.Count
        If NbPl = 0 Then Exit Function
        Dim EpMax As Decimal = Me.Plages(NbPl - 1).Ep

        '--> Traitement

        'If Me.lUser Then
        '    '--[ Acier défini directement par l'utilisateur
        '    MyFy = 0
        'Else
        '--[ Acier de la base de donnée : Recherche dans les plages
        If Plages.Count < 1 Then
                MyFy = -1
            Else
                '--> On commence en dehors des plages
                If Epaisseur < Plages(0).Ep Then
                    MyFy = Me.Plages(0).Fy
                ElseIf Epaisseur > EpMax Then
                    MyFy = Me.Plages(Plages.Count - 1).Fy
                    '--> Puis dans les plages
                ElseIf Plages.Count = 1 Then
                    MyFy = Plages(0).Fy
                Else

                    Dim lTrouve As Boolean
                    Dim i As Integer = 0
                    lTrouve = (Me.Plages(i).Ep >= Epaisseur)

                    Do While i < Plages.Count - 1 And Not lTrouve
                        i += 1
                        lTrouve = (Me.Plages(i).Ep >= Epaisseur)
                    Loop

                    If lTrouve Then
                        MyFy = Plages(i - 1).Fy
                    Else
                        lTrouve = (Epaisseur <= EpMax)
                        If lTrouve Then
                            MyFy = Plages(Plages.Count - 1).Fy
                        Else
                            'GestionErreursACB("Cls_SteelNew", "LimiteFy", "Search failure for thickness " & CStr(Epaisseur))
                        End If
                    End If
                End If
            End If
        'End If

        Return MyFy

    End Function


    Public Function LimiteFu(ByVal Epaisseur As Double) As Double
        '--------------------------------------------------------------------------------
        '
        '   04/07/12 :  Création - v3.00 - POM
        '
        '--------------------------------------------------------------------------------
        '
        '   Retourne la limite à la rupture en fonction de l'épaisseur
        '
        '--------------------------------------------------------------------------------
        '--------------------------------------------------------------------------------

        Dim MyFu As Double = 0

        'If Me.lUser Then
        '--[ Acier défini directement par l'utilisateur
        '    MyFu = FuImpose
        'Else
        '--[ Acier de la base de donnée : Recherche dans les plages
        If Plages.Count < 1 Then
                MyFu = -1
            Else
                '--> On commence en dehors des plages
                If Epaisseur < Plages(0).Ep Then
                    MyFu = Me.Plages(0).Fu
                ElseIf Epaisseur > EpMax Then
                    MyFu = Me.Plages(Plages.Count - 1).Fu
                    '--> Puis dans les plages
                ElseIf Plages.Count = 1 Then
                    MyFu = Plages(0).Fu
                Else

                    Dim lTrouve As Boolean
                    Dim i As Integer = 1
                    lTrouve = (Me.Plages(i).Ep >= Epaisseur)

                    Do While i < Plages.Count - 1 And Not lTrouve
                        i += 1
                        lTrouve = (Me.Plages(i).Ep >= Epaisseur)
                    Loop

                    If lTrouve Then
                        MyFu = Plages(i - 1).Fu
                    Else
                        lTrouve = (Epaisseur <= EpMax)
                        If lTrouve Then
                            MyFu = Plages(Plages.Count - 1).Fu
                        Else
                            'GestionErreursACB("Cls_SteelNew", "LimiteFu", "Search failure for thickness " & CStr(Epaisseur))
                        End If
                    End If
                End If
            End If
        'End If

        Return MyFu

    End Function

#End Region

#Region " Constructeur "

    Sub New()

        Me.Nuance = "S235"
        Me.Qualite = "EC3"
        Me.Reduction = "Table 3.1"

        Me.Plages = New List(Of strucPlage)

    End Sub

    Public Sub InitialiseAcierS355MML()
        '------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '------------------------------------------------------------------------------------------
        '   Initialisation d'un acier S355 M/ML pour les tests unitaires (pas d'accès à la base de données)
        '------------------------------------------------------------------------------------------
        '------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyPlage As strucPlage

        '--> Initialisations

        Me.Plages.Clear()

        Me.Nuance = "S355"
        Me.Qualite = "M/ML"
        Me.EpMax = 0.15

        MyPlage.Ep = 0.003
        MyPlage.Fu = 470
        MyPlage.Fy = 355
        Me.Plages.Add(MyPlage)

        MyPlage.Ep = 0.016
        MyPlage.Fu = 470
        MyPlage.Fy = 345
        Me.Plages.Add(MyPlage)

        MyPlage.Ep = 0.04
        MyPlage.Fu = 470
        MyPlage.Fy = 335
        Me.Plages.Add(MyPlage)

        MyPlage.Ep = 0.063
        MyPlage.Fu = 470
        MyPlage.Fy = 325
        Me.Plages.Add(MyPlage)

        MyPlage.Ep = 0.1
        MyPlage.Fu = 470
        MyPlage.Fy = 320
        Me.Plages.Add(MyPlage)

    End Sub


    Public Sub InitialiseAcierS275JR()
        '------------------------------------------------------------------------------------------
        '   11/07/23 :  Création - POM
        '------------------------------------------------------------------------------------------
        '   Initialisation d'un acier S275 M/ML pour les tests unitaires (pas d'accès à la base de données)
        '------------------------------------------------------------------------------------------
        '------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim MyPlage As strucPlage

        '--> Initialisations

        Me.Plages.Clear()

        Me.Nuance = "S275"
        Me.Qualite = "JR/J0/J2"
        Me.EpMax = 0.15

        MyPlage.Ep = 0.003
        MyPlage.Fu = 470
        MyPlage.Fy = 275
        Me.Plages.Add(MyPlage)

        MyPlage.Ep = 0.016
        MyPlage.Fu = 265
        MyPlage.Fy = 265
        Me.Plages.Add(MyPlage)

        MyPlage.Ep = 0.04
        MyPlage.Fu = 455
        MyPlage.Fy = 255
        Me.Plages.Add(MyPlage)

        MyPlage.Ep = 0.063
        MyPlage.Fu = 470
        MyPlage.Fy = 245
        Me.Plages.Add(MyPlage)

        MyPlage.Ep = 0.08
        MyPlage.Fu = 470
        MyPlage.Fy = 235
        Me.Plages.Add(MyPlage)

        MyPlage.Ep = 0.1
        MyPlage.Fu = 470
        MyPlage.Fy = 225
        Me.Plages.Add(MyPlage)

    End Sub

#End Region

#Region " Ecriture Fichier "

    ''' <summary>
    ''' Ecriture des attributs pour enregistrement dans un fichier 
    ''' </summary>
    ''' <param name="Lines">Lignes d'écriture</param>
    'Public Sub EcrireFile(ByRef Lines As List(Of String))

    '    Lines.Add("   Nuance        = " & Nuance)
    '    Lines.Add("   Qualite       = " & Qualite)
    '    Lines.Add("   lUser       = " & lUser)
    '    Lines.Add("   Fyw           = " & f_y.w)
    '    Lines.Add("   Fyfs          = " & f_y.fs)
    '    Lines.Add("   Fyfi          = " & f_y.fi)

    'End Sub

#End Region

#Region " Fonction de copie "

    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Sub Deepclone(ByVal AcierSource As cls_Acier, ByRef AcierCible As cls_Acier)

        'Dim pouet As Double = 0

        AcierCible = AcierSource.Clone()

        'AcierCible.Plages.Clear()

        AcierCible.Plages = New List(Of strucPlage)
        For Each plage As strucPlage In AcierSource.Plages
            Dim plage_loc As strucPlage

            'pouet += 100

            plage_loc.Ep = plage.Ep
            'plage_loc.Ep = pouet
            plage_loc.Fy = plage.Fy
            plage_loc.Fu = plage.Fu

            AcierCible.Plages.Add(plage_loc)
        Next




    End Sub

#End Region

End Class
