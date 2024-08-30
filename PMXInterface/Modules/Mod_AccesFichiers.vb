Module Mod_AccesFichiers

#Region " Analyse d'une ligne d'un fichier ASCII "

    Public Sub DecomposeLine(ByVal Line As String, ByRef Separateurs() As String, ByRef Words() As String, ByRef nbWords As Integer)
        '--------------------------------------------------------------------------------------
        '   08/03/06 :  Création - POM
        '--------------------------------------------------------------------------------------
        '   Décomposition d'une ligne en table de mots, en fonction d'une liste de séparateur
        '--------------------------------------------------------------------------------------
        '   Line        [E] :   Ligne de caractères à analyser
        '   Seperateurs [E] :   Table de separateurs pour décomposer la ligne
        '
        '   Words       [S] :   Table des mots contenus dans la ligne
        '   nbWords     [S] :   Nombre de mots
        '--------------------------------------------------------------------------------------

        Dim IsSeparateur As Boolean, Letter As String
        Dim iDeb, iLen As Integer
        Dim i, j As Integer

        iDeb = 0
        iLen = 0
        nbWords = 0

        For i = 0 To Line.Length - 1
            Letter = Line.Substring(i, 1)
            IsSeparateur = (Letter = Separateurs(0))
            j = -1
            Do While j < Separateurs.GetUpperBound(0) And Not IsSeparateur
                j = j + 1
                IsSeparateur = (Letter = Separateurs(j))
            Loop

            '--> Si le caractere est un separateur, on place le mot en cours dans la liste des mots

            If IsSeparateur Then
                If iLen > 0 Then
                    AddMot(Line.Substring(iDeb, iLen), Words, nbWords)
                    iLen = 0
                End If
                iDeb = i + 1
            Else
                iLen = iLen + 1
            End If

        Next

        '--> A la fin de la chaine, si le mot en cours n'est pas vide, on l'ajoute à la liste

        If iLen > 0 Then AddMot(Line.Substring(iDeb, iLen), Words, nbWords)

    End Sub

    Sub AddMot(ByVal NewWord As String, ByRef Words() As String, ByRef nbWords As Integer)
        '--------------------------------------------------------------------------------------
        '   08/03/06 :  Création - Version 1.00
        '--------------------------------------------------------------------------------------
        '   Ajout d'un mot dans une table de mots
        '--------------------------------------------------------------------------------------
        '   NewWord     [E] :   Mot à ajouter
        '   Words()     [E/S] : Tableau de mots à compléter
        '   nbWords     [E/S]:  Nombre de mots dans le tableau
        '--------------------------------------------------------------------------------------

        If nbWords = 0 Then
            nbWords = 1
            ReDim Words(1)
            Words(1) = NewWord
        Else
            nbWords = nbWords + 1
            ReDim Preserve Words(nbWords)
            Words(nbWords) = NewWord
        End If

    End Sub

#End Region

#Region " Sauvegarde des fichiers projets "

    Public Sub EcrireProjetInFile(ByVal FileName As String)
        '------------------------------------------------------------------------------------------------------------
        '   07/08/23 :  Création - POM
        '------------------------------------------------------------------------------------------------------------
        '   Ecriture du fichier de sauvegarde projet
        '------------------------------------------------------------------------------------------------------------
        '   FileName    [E] :   Nom du fichier de sauvegarde
        '------------------------------------------------------------------------------------------------------------

        Try
            '--> Initialisation
            Dim MyFile As System.IO.StreamWriter
            Dim Lines As New List(Of String)

            '--> Ecriture de toutes les données
            MyProjet.EcrireFile(Lines, LogicielInfo.Version.Label)

            '--> Aucune modification par rapport au fichier enregistré
            'MyProject.Modif = False
            'Frm_MAIN.ModifImageSave()

            '--> Création et ouverture du fichier
            MyFile = My.Computer.FileSystem.OpenTextFileWriter(FileName, False)

            '--> Ecriture dans le fichier
            For i As Integer = 0 To Lines.Count - 1
                MyFile.WriteLine(Lines(i))
            Next

            '--> Fermeture du fichier
            MyFile.Close()

        Catch ex As Exception
            MsgBox("Erreur création/écriture du fichier | Error create/write the file", MsgBoxStyle.Critical, "Mod_GestionFichier/EcrireInFile")
        End Try

    End Sub


#End Region


#Region "   Gestion du nom des répertoires "

    Public Function Repertoire(ByVal NomFile As String) As String
        '
        '   07/10/02
        '
        '--------------------------------------------------------------------
        '
        '   Recupere le nom du repertoire dans le nom complet d'un fichier
        '
        '--------------------------------------------------------------------
        '
        '   NomFile [E] :   Nom complet du fichier
        '
        '--------------------------------------------------------------------

        Dim nLen, i As Integer
        Dim lTrouve As Boolean

        nLen = Len(NomFile)
        lTrouve = False
        i = nLen

        Do While (Not lTrouve) And (i > 0)
            If Mid(NomFile, i, 1) = "\" Then lTrouve = True
            i = i - 1
        Loop
        If lTrouve Then
            Repertoire = Mid(NomFile, 1, i + 1)
        Else
            Repertoire = ""
        End If

    End Function

    Public Function NomFichierSeul(ByVal NomComplet As String) As String
        Dim Repertoire As String = ""
        Dim Fichier As String = ""
        DecomposeChemin(NomComplet, Repertoire, Fichier)
        Return Fichier
    End Function

    Public Sub DecomposeChemin(ByVal NomChemin As String,
                               ByRef Repertoire As String, ByRef Fichier As String)
        '
        '   13/02/07 : Création - Version 1.00
        '
        '-------------------------------------------------------------------------------
        '
        '   Recupere le repertoire et le fichier dans le nom complet d'un fichier
        '
        '-------------------------------------------------------------------------------
        '
        '   NomChemin   [E] :   Nom complet du fichier
        '
        '   Repertoire  [S] :   Repertoire
        '   Fichier     [S] :   Fichier
        '
        '-------------------------------------------------------------------------------

        Dim iSlash As Integer = LastSlash(NomChemin)

        If iSlash >= 0 Then
            Repertoire = NomChemin.Substring(0, iSlash)
            Fichier = NomChemin.Substring(iSlash + 1)
        Else
            Repertoire = ""
            Fichier = NomChemin
        End If

    End Sub

    Private _
    Function LastSlash(ByVal NomChemin As String) As Integer
        '
        '   13/02/07 : Création - Version 1.00
        '
        '-------------------------------------------------------------------------------
        '
        '   Donne dans un nom de fichier complet la position du dernier \
        '
        '-------------------------------------------------------------------------------
        '
        '   NomChemin   [E] :   Nom complet du fichier
        '
        '-------------------------------------------------------------------------------

        Dim i As Integer = NomChemin.Length
        Dim lTrouve As Boolean = False

        Do While (Not lTrouve) And i > 0
            i = i - 1
            If NomChemin.Substring(i, 1) = "\" Then lTrouve = True
        Loop

        If lTrouve Then
            Return i
        Else
            Return -1
        End If
    End Function

    Public Sub ReduitNomFichier(ByRef NomFichier As String, ByRef lReduction As Boolean)
        '-------------------------------------------------------------------------------------------------
        '
        '   23/04/09 :  Création - Version 1.03 - POM
        '
        '-------------------------------------------------------------------------------------------------
        '
        '   Réduit le nom d'un fichier (en supprimant des répertoires de l'arborescence
        '   pour réduire la taille de l'affichage
        '   
        '-------------------------------------------------------------------------------------------------
        '
        '   NomFichier  [E/S] : Nom du fichier à traiter
        '   lReduction  [S] :   Indique si on a pu réduire
        '
        '-------------------------------------------------------------------------------------------------

        '--[ Décaration

        Dim NewNom As String
        Dim jFirst, jSecond As Integer

        '--[ Initialisation

        lReduction = False

        NewNom = NomFichier.Substring(0, 3)

        jFirst = NomFichier.Substring(3).IndexOf("\")

        If jFirst >= 0 Then
            lReduction = True
            If NomFichier.Substring(3, 2) = ".." Then
                jSecond = NomFichier.Substring(4 + jFirst).IndexOf("\")
                NewNom = NomFichier.Substring(0, 3 + jFirst) & NomFichier.Substring(jSecond + jFirst + 4)
            Else
                NewNom = NewNom & ".." & NomFichier.Substring(jFirst + 3)
            End If

            NomFichier = NewNom
        End If

    End Sub

#End Region


End Module
