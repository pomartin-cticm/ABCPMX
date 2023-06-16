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



End Module
