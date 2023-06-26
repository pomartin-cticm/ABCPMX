Imports System.IO
Imports System.Runtime.CompilerServices
Imports PMXMoteur2

Module Mod_BasesDonneesASCII

#Region " Déclarations des bases "

    Public BaseBacs As New Dictionary(Of String, Cls_Bac)

#End Region

#Region " Base de données des bacs "

    Public Sub LireBaseBacs(ByRef DicoBac As Dictionary(Of String, Cls_Bac))
        '---------------------------------------------------------------------------------
        '   15/06/23 :  Création - POM
        '---------------------------------------------------------------------------------
        '   Recupération des bacs dans la base de données
        '---------------------------------------------------------------------------------
        '   DicoBac     [S] :   Dictionnaire des bacs de la base  
        '---------------------------------------------------------------------------------

        '--[ Déclarations

        Dim ListBacs As New List(Of Cls_Bac)

        '--[ Récupération de la liste des bacs

        GetDataBaseBacs(ListBacs)

        '--[ Transfert dans le disctionnaire

        DicoBac.Clear()

        For i As Integer = 0 To ListBacs.Count - 1

            DicoBac.Add(ListBacs(i).Etiquette, ListBacs(i))

        Next

    End Sub

    Sub GetDataBaseBacs(ByRef ListeBac As List(Of Cls_Bac))
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
                                     ByRef ListeBac As List(Of Cls_Bac))
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
        Dim Fabricant As String

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
                        Ieff = CDbl(TraiteReal(Mots(iField + 4)))
                    End If

                    'ListeBac.Add(New Cls_Bac(Etiquette, b1, b2, h, e, M, fy, lCustom))
                    ListeBac.Add(New Cls_Bac(Fabricant, Etiquette, b1, b2, h, hrs, e, t, M, fy, wModule, Ieff))

                    ListeBac(ListeBac.Count - 1).tp = t
                    ListeBac(ListeBac.Count - 1).h_rs = hrs
                End If
            End If


        Next

    End Sub

#End Region


End Module
