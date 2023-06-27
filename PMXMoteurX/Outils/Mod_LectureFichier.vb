Module Mod_LectureFichier

    ''' <summary>
    ''' Caractères de sépération des mots dans les fichiers
    ''' </summary>
    Public SEPARATEURS() As String = {" ", "=", ";"}

    Public Sub DecomposeLine(ByVal Line As String, ByRef Words() As String, ByRef nbWords As Integer)
        '
        '   08/03/06 :  Création - Version 1.00
        '
        '--------------------------------------------------------------------------------------
        '
        '   Décomposition d'une ligne en table de mots, en fonction d'une liste de séparateur
        '
        '--------------------------------------------------------------------------------------
        '
        '   Line        [E] :   Ligne de caractères
        '   Seperateurs [E] :   Table de separateurs
        '
        '   Lines       [S] :   Lignes du fichiers
        '
        '--------------------------------------------------------------------------------------

        Dim IsSeparateur As Boolean, Letter As String
        Dim iDeb, iLen As Integer
        Dim i, j As Integer

        iDeb = 0
        iLen = 0
        nbWords = 0

        For i = 0 To Line.Length - 1
            Letter = Line.Substring(i, 1)
            IsSeparateur = (Letter = SEPARATEURS(0))
            j = -1
            Do While j < SEPARATEURS.GetUpperBound(0) And Not IsSeparateur
                j += 1
                IsSeparateur = (Letter = SEPARATEURS(j))
            Loop

            '--> Si le caractere est un separateur, on place le mot en cours dans la liste des mots

            If IsSeparateur Then
                If iLen > 0 Then
                    AddWord(Line.Substring(iDeb, iLen), Words, nbWords)
                    iLen = 0
                End If
                iDeb = i + 1
            Else
                iLen += 1
            End If

        Next

        '--> A la fin de la chaine, si le mot en cours n'est pas vide, on l'ajoute à la liste

        If iLen > 0 Then AddWord(Line.Substring(iDeb, iLen), Words, nbWords)

    End Sub

    Private Sub AddWord(ByVal NewWord As String, ByRef Words() As String, ByRef nbWords As Integer)
        '
        '   08/03/06 :  Création - Version 1.00
        '
        '--------------------------------------------------------------------------------------
        '
        '   Ajout d'un mot dans une table de mots
        '
        '--------------------------------------------------------------------------------------

        If nbWords = 0 Then
            nbWords = 1
            ReDim Words(1)
            Words(1) = NewWord
        Else
            nbWords += 1
            ReDim Preserve Words(nbWords)
            Words(nbWords) = NewWord
        End If

    End Sub

    '--> Gestion des listes de decimal pour l'ecriture/lecture du fichier

    ''' <summary>
    ''' Transforme une liste de decimal en string
    ''' </summary>
    ''' <param name="list">tableau de decimal</param>
    ''' <returns></returns>
    Public Function ConvertListToString(ByVal list() As Decimal) As String
        'R 21-012 - Bed - 15/07/21

        Dim text As String = "{"

        For i As Integer = 0 To list.Count - 1
            text += list(i).ToString
            If i <> list.Count - 1 Then
                text += "/"
            Else
                text += "}"
            End If
        Next

        Return text

    End Function

    ''' <summary>
    ''' Transforme une liste d'integer en string
    ''' </summary>
    ''' <param name="list">tableau de decimal</param>
    ''' <returns></returns>
    Public Function ConvertListIntegerToString(ByVal list() As Integer) As String
        'GuD - 26/06/23

        Dim text As String = "{"

        For i As Integer = 0 To list.Count - 1
            text += list(i).ToString
            If i <> list.Count - 1 Then
                text += "/"
            Else
                text += "}"
            End If
        Next

        Return text

    End Function


    ''' <summary>
    ''' Transforme une liste de string en string
    ''' </summary>
    ''' <param name="list">tableau de decimal</param>
    ''' <returns></returns>
    Public Function ConvertListStringToString(ByVal list() As String) As String
        'GuD - 26/06/23

        Dim text As String = "{"

        For i As Integer = 0 To list.Count - 1
            text += list(i).ToString
            If i <> list.Count - 1 Then
                text += "/"
            Else
                text += "}"
            End If
        Next

        Return text

    End Function

    ''' <summary>
    ''' Transforme un string en liste de decimal
    ''' </summary>
    ''' <param name="text">string</param>
    ''' <returns></returns>
    Public Function ConvertStringToList(ByVal text As String) As Decimal()
        'R 21-012 - Bed - 15/07/21

        Dim list_deci(0) As Decimal

        If text.Contains("{") Then 'nouveau fichier

            text = text.Replace("{", "")
            text = text.Replace("}", "")

            If Not text = "" Then
                Dim tab_text() As String = text.Split("/")
                ReDim list_deci(tab_text.Length - 1)

                For i As Integer = 0 To tab_text.Length - 1
                    list_deci(i) = TraiteReal(tab_text(i))
                Next
            End If

        Else 'ancien fichier
            list_deci(0) = text
        End If

        Return list_deci

    End Function

    ''' <summary>
    ''' Transforme un string en liste de integer
    ''' </summary>
    ''' <param name="text">string</param>
    ''' <returns></returns>
    Public Function ConvertStringToListInteger(ByVal text As String) As Integer()
        'GuD - 26/06/23

        Dim list_integer(0) As Integer

        If text.Contains("{") Then 'nouveau fichier

            text = text.Replace("{", "")
            text = text.Replace("}", "")

            If Not text = "" Then
                Dim tab_text() As String = text.Split("/")
                ReDim list_integer(tab_text.Length - 1)

                For i As Integer = 0 To tab_text.Length - 1
                    list_integer(i) = TraiteReal(tab_text(i))
                Next
            End If

        Else 'ancien fichier
            list_integer(0) = text
        End If

        Return list_integer

    End Function

    ''' <summary>
    ''' Transforme un string en liste de string
    ''' </summary>
    ''' <param name="text">string</param>
    ''' <returns></returns>
    Public Function ConvertStringToListString(ByVal text As String) As String()
        'GuD - 26/06/23

        Dim list_string(0) As String

        If text.Contains("{") Then 'nouveau fichier

            text = text.Replace("{", "")
            text = text.Replace("}", "")

            If Not text = "" Then
                Dim tab_text() As String = text.Split("/")
                ReDim list_string(tab_text.Length - 1)

                For i As Integer = 0 To tab_text.Length - 1
                    list_string(i) = TraiteReal(tab_text(i))
                Next
            End If

        Else 'ancien fichier
            list_string(0) = text
        End If

        Return list_string

    End Function

    ''' <summary>
    ''' Transforme les points ou virgule d'une chaine en sepérateur decimal
    ''' </summary>
    ''' <param name="Chaine"></param>
    ''' <returns></returns>
    Public Function TraiteReal(ByVal Chaine As String) As String

        Dim SepDecimal As String = Mid(Format(1.1, "0.0"), 2, 1)

        If SepDecimal = "," Then
            Dim iPoint As Integer = InStr(Chaine, ".")
            If iPoint > 0 Then
                Mid(Chaine, iPoint, 1) = ","
            End If
        ElseIf SepDecimal = "." Then
            Dim iPoint As Integer = InStr(Chaine, ",")
            If iPoint > 0 Then
                Mid(Chaine, iPoint, 1) = "."
            End If
        End If

        Return Chaine

    End Function

End Module
