Imports System.IO
Imports System.Runtime.CompilerServices
Imports PMXMoteur2

Public Module Mod_BasesDonneesASCII

#Region " Déclarations des bases "

    Public BaseBacs As New Dictionary(Of String, cls_Bac)

    'Public BaseGoujons As (String, Decimal, Decimal, Decimal, Decimal, Decimal, Decimal, Boolean)() = Nothing
    Public BaseGoujons As New List(Of cls_ConnecteurGoujonSoude)

#End Region

#Region " Base de données des bacs "

    Public Sub LireBaseBacs(ByRef DicoBac As Dictionary(Of String, cls_Bac))
        '---------------------------------------------------------------------------------
        '   15/06/23 :  Création - POM
        '---------------------------------------------------------------------------------
        '   Recupération des bacs dans la base de données
        '---------------------------------------------------------------------------------
        '   DicoBac     [S] :   Dictionnaire des bacs de la base  
        '---------------------------------------------------------------------------------

        '--[ Déclarations

        Dim ListBacs As New List(Of cls_Bac)

        '--[ Récupération de la liste des bacs

        GetDataBaseBacs(ListBacs)

        '--[ Transfert dans le disctionnaire

        DicoBac.Clear()

        For i As Integer = 0 To ListBacs.Count - 1

            DicoBac.Add(ListBacs(i).Etiquette, ListBacs(i))

        Next

    End Sub

    Sub GetDataBaseBacs(ByRef ListeBac As List(Of cls_Bac))
        '---------------------------------------------------------------------------------
        '   15/06/23 :  Création - POM
        '---------------------------------------------------------------------------------
        '   Recupération des bacs des bases fixe et custom
        '---------------------------------------------------------------------------------
        '   ListeBac    [S] :   Liste des bacs contenus dans les base fixe et custom
        '---------------------------------------------------------------------------------

        '--[ Initialisation

        ListeBac.Clear()

        '--[ Récupération des bacs dans la base fixe

        RecupereBacsFromFile(LogicielFichiers.Base_Bacs, False, ListeBac)

        ''--[ Récupération des bacs dans la base perso

        'If My.Computer.FileSystem.FileExists(FileACB.BacsPerso) Then
        '    RecupereBacsFromFile(FileACB.BacsPerso, True, ListeBac)
        'End If

    End Sub

    Private Sub RecupereBacsFromFile(ByVal FileBac As String, ByVal lCustom As Boolean,
                                     ByRef ListeBac As List(Of cls_Bac))
        '---------------------------------------------------------------------------------
        '
        '   03/07/08 :  Création - Version 1.00 B2
        '   26/10/21 :  Modif - V 4.07 B7 POM
        '
        '---------------------------------------------------------------------------------
        '
        '   Recupération des bacs dans un fichier
        '   Introduction des raidisseurs supérieurs
        '
        '---------------------------------------------------------------------------------
        '
        '   FileBac     [E] :   Nom du fichier base de données à lire
        '   lCustom     [E] :   Indique si base peronnelle de l'utilisateur
        '   ListeBac    [S] :   Liste des bacs contenus dans la base lue
        '
        '---------------------------------------------------------------------------------

        '--[ Déclarations

        Dim LinesSheets As New Cls_LinesOfFile(FileBac, True)
        Dim Etiquette, Parametres As String
        Dim iVirg, iDiez As Integer
        Dim Mots() As String = Nothing
        Dim nbMots As Integer
        Dim b1, b2, e, h, t, hrs As Double
        Dim M, fy, wModule, Ieff As Double
        Dim iField As Integer
        Dim Fabricant As String = ""
        Dim kConvCm4ToM4 = 10 ^ (-8) 'l'inertie du bac est donnée en cm4/m dans la BDD. La convention dans le logiciel est le mètre 

        '--[ Initialisation

        Dim kUnit As Double = 0.001

        '--[ Lecture de la base

        For i As Integer = 1 To LinesSheets.LineNumber - 1

            iDiez = LinesSheets.Lines(i).IndexOf("#")

            If iDiez >= 0 Then
                'Cas d'une ligne définissant un bloc associé à un fabricant
                Fabricant = LinesSheets.Lines(i).Substring(iDiez + 1).Trim
            Else
                'Cas d'une ligne définissant un bac
                iVirg = LinesSheets.Lines(i).IndexOf(",")
                Etiquette = LinesSheets.Lines(i).Substring(0, iVirg).Trim
                Parametres = LinesSheets.Lines(i).Substring(iVirg + 1)

                DecomposeLine(Parametres, SEPARATEURS, Mots, nbMots)

                If nbMots > 0 Then
                    b1 = CDbl(TraiteReal(Mots(1))) * kUnit
                    b2 = CDbl(TraiteReal(Mots(2))) * kUnit
                    e = CDbl(TraiteReal(Mots(3))) * kUnit
                    h = CDbl(TraiteReal(Mots(4))) * kUnit
                    iField = 5

                    hrs = CDbl(TraiteReal(Mots(iField))) * kUnit
                    iField += 1

                    t = CDbl(TraiteReal(Mots(iField))) * kUnit
                    M = CDbl(TraiteReal(Mots(iField + 1)))
                    fy = CDbl(TraiteReal(Mots(iField + 2)))

                    If (nbMots > iField + 2) Then
                        wModule = CDbl(TraiteReal(Mots(iField + 3))) * kUnit
                        Ieff = CDbl(TraiteReal(Mots(iField + 4))) * kConvCm4ToM4
                    End If

                    'ListeBac.Add(New Cls_Bac(Etiquette, b1, b2, h, e, M, fy, lCustom))
                    ListeBac.Add(New cls_Bac(Fabricant, Etiquette, b1, b2, h, hrs, e, t, M, fy, wModule, Ieff))

                    ListeBac(ListeBac.Count - 1).Tp = t
                    ListeBac(ListeBac.Count - 1).h_rs = hrs
                End If
            End If


        Next

    End Sub

#End Region

#Region " Base de données des connecteurs "

    Public Sub GetDataBaseStuds(ByRef ListeStud As List(Of cls_ConnecteurGoujonSoude))
        '---------------------------------------------------------------------------------
        '
        '   27/11/23 :  Ajout GUD (issu d'ACB+)
        '
        '---------------------------------------------------------------------------------
        '
        '   Recupération des connecteurs des bases fixe et custom
        '
        '---------------------------------------------------------------------------------
        '
        '   MyBaseG    [S] :   Liste des bacs contenus dans les base fixe et custom
        '
        '---------------------------------------------------------------------------------

        '--[ Initialisation

        ListeStud.Clear()

        '==== Chargements connecteurs NELSON ====

        '--[ Récupération des connecteurs dans la base fixe

        LireBaseGoujons(LogicielFichiers.Base_Goujons, False, ListeStud)

        '--[ Récupération des connecteurs dans la base perso

        If My.Computer.FileSystem.FileExists(LogicielFichiers.Base_Goujons_Perso) Then
            LireBaseGoujons(LogicielFichiers.Base_Goujons_Perso, True, ListeStud)
        End If

    End Sub

    Private Sub LireBaseGoujons(FileG As String, ByVal lCustom As Boolean,
                                ByRef ListeStud As List(Of cls_ConnecteurGoujonSoude))
        '---------------------------------------------------------------------------------
        '   09/08/23 :  Création - POM
        '---------------------------------------------------------------------------------
        '   Recupération des bacs dans la base de données
        '---------------------------------------------------------------------------------
        '   FileG       [E] :   Base des connecteurs
        '   MyBaseG     [S] :   Base des goujons
        '---------------------------------------------------------------------------------

        '--> Déclarations 

        Dim iStud As Integer
        Dim Etiquette, Parametres As String
        Dim Mots() As String = Nothing
        Dim nMots As Integer
        Dim lFmtPB As Boolean = False

        Dim LinesG As New Cls_LinesOfFile(FileG, True)

        '--[ Analyse du fichier lu

        For iStud = 1 To LinesG.Lines.Count - 1

            Dim Htot, PhiTige, HTete, PhiTete As Single
            Dim fy, fu As Single

            '--> Decoupe de la ligne en Label et en paramètres
            '--> L'étiquette est repérée par la première virgule

            Dim iVirg As Integer = LinesG.Lines(iStud).IndexOf(",")
            Dim kUnit As Single = 0.001

            If iVirg > 0 Then
                'Etiquette = LinesG.Lines(iStud).Substring(0, iVirg)
                Etiquette = LinesG.Lines(iStud).Substring(0, iVirg)
                DecomposeLine(Etiquette, SEPARATEURS, Mots, nMots)

                Parametres = LinesG.Lines(iStud).Substring(iVirg + 1)
                DecomposeLine(Parametres, SEPARATEURS, Mots, nMots)

                Htot = CSng(TraiteReal(Mots(1))) * kUnit
                PhiTige = CSng(TraiteReal(Mots(2))) * kUnit
                PhiTete = CSng(TraiteReal(Mots(3))) * kUnit
                HTete = CSng(TraiteReal(Mots(4))) * kUnit
                fy = CSng(TraiteReal(Mots(5)))
                fu = CSng(TraiteReal(Mots(6)))

                ListeStud.Add(New cls_ConnecteurGoujonSoude(Etiquette, Htot, PhiTige, PhiTete, HTete, fy, fu))

                'AjouteGoujonsBase(Etiquette, Htot, PhiTige, PhiTete, HTete, fy, fu, lCustom, MyBaseG)

                ListeStud(ListeStud.Count - 1).lCustom = lCustom

            Else
                lFmtPB = True
            End If

        Next
        If lFmtPB Then
            'UserWarning(RemplaceDollar(BlocMessage("PBFMT"), FileStud))
        End If
    End Sub

    'Private Sub AjouteGoujonsBase(Label As String, Hsc As Decimal, PhiTige As Decimal, PhiTete As Decimal, HTete As Decimal, Fy As Decimal, Fu As Decimal, lCustom As Boolean, ByRef MyBaseG As (String, Decimal, Decimal, Decimal, Decimal, Decimal, Decimal, Boolean)())
    '    '---------------------------------------------------------------------------------
    '    '   09/08/23 :  Création - POM
    '    '---------------------------------------------------------------------------------
    '    '   Ajout d'un bac dans la base de données des connecteurs 
    '    '---------------------------------------------------------------------------------
    '    '   MyBaseG     [S] :   Base des goujons
    '    '   Label       [E] :   
    '    '   Hsc         [E] :   Hauteur totale du connecteur
    '    '   Phi         [E] :   Diametre du connecteur
    '    '   Fy, Fu      [E] :   Limite d'élasticite et limite ultime à la traction
    '    '---------------------------------------------------------------------------------

    '    '--> Déclaration

    '    Dim nbG As Integer

    '    '--> Initialisation

    '    If MyBaseG Is Nothing Then
    '        nbG = 0
    '    Else
    '        nbG = MyBaseG.GetUpperBound(0) + 1
    '    End If

    '    nbG += 1

    '    If nbG > 0 Then
    '        ReDim Preserve MyBaseG(nbG - 1)
    '    Else
    '        ReDim MyBaseG(nbG)
    '    End If

    '    '--> Infos

    '    MyBaseG(nbG - 1).Item1 = Label
    '    MyBaseG(nbG - 1).Item2 = Hsc
    '    MyBaseG(nbG - 1).Item3 = PhiTige
    '    MyBaseG(nbG - 1).Item4 = Fy
    '    MyBaseG(nbG - 1).Item5 = Fu
    '    MyBaseG(nbG - 1).Item6 = PhiTete
    '    MyBaseG(nbG - 1).Item7 = HTete
    '    MyBaseG(nbG - 1).Item8 = lCustom

    'End Sub

#End Region


End Module
