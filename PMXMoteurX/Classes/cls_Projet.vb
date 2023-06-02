Imports System.Collections.Specialized.BitVector32
Imports System.IO
Imports System.Net.Mime.MediaTypeNames

Public Class cls_Projet

#Region " Attributs "

    '--> Paramètres générals

    ''' <summary>
    ''' Nom du projet
    ''' </summary>
    Public Nom As String

    ''' <summary>
    ''' Utilisateur du projet
    ''' </summary>
    Public Utilisateur As String

    ''' <summary>
    ''' Entreprise du projet
    ''' </summary>
    Public Entreprise As String

    ''' <summary>
    ''' Poutres du projet
    ''' </summary>
    Public Poutres As New List(Of cls_Poutre)

    ''' <summary>
    ''' Indice de l'assemblage sélectionné dans la liste
    ''' </summary>
    Public IndEnCours As Integer

    ''' <summary>
    ''' Indique si le projet est modifié après sauvegarde  
    ''' </summary>
    Public lModif As Boolean

    ''' <summary>
    ''' Indique si le projet est déjà enregistré
    ''' </summary>
    Public lSave As Boolean

    ''' <summary>
    ''' Chemin du fichier du projet déjà enregistré
    ''' </summary>
    Public CheminFichier As String

#End Region

#Region " Constructeurs "

    ''' <summary>
    ''' Constructeur vide
    ''' </summary>
    Sub New()

        IndEnCours = -1
        Me.Nom = ""

    End Sub

    ''' <summary>
    ''' Constructeur avec l'utilisateur et l'entreprise
    ''' </summary>
    ''' <param name="utilisateur"></param>
    ''' <param name="entreprise"></param>
    Sub New(ByVal utilisateur As String, ByVal entreprise As String)

        Me.Utilisateur = utilisateur
        Me.Entreprise = entreprise

        IndEnCours = -1

        Me.Nom = ""

    End Sub

#End Region

#Region " Ecriture / Lecture - Fichier "

    Public Sub EcrireFile(ByRef Lines As List(Of String), ByVal version As String)
        '-------------------------------------------------------------------------------------
        '   Ecriture des attributs pour enregistrement dans un fichier 
        '-------------------------------------------------------------------------------------

        '==[ Entete ]=========================================================================
        Lines.Add("'-----------------------------------------------'")
        Lines.Add("'PropMix software - CTICM - Version " & version)
        Lines.Add("'PROJECT USER FILE")
        Lines.Add("'-----------------------------------------------'")
        Lines.Add("'       /!\    Don't edit this file    /!\")
        Lines.Add("'       /!\ Ne pas modifier ce fichier /!\")
        Lines.Add("'-----------------------------------------------'")
        Lines.Add("")

        '==[ Identification ]=================================================================
        Lines.Add("BLOCK IDENTIFICATION")
        Lines.Add("   Utilisateur   = " & Me.Utilisateur)
        Lines.Add("   Entreprise    = " & Me.Entreprise)
        Lines.Add("   Nom           = " & Me.Nom)
        Lines.Add("")

        '==[ Nuances d'acier utilisateur ]=================================================================
        Dim nuancesSave As New List(Of String)

        'For Each s In List_Section

        '    If s.acier.qualite = "USER" And Not nuancesSave.Contains(s.acier.nuance) Then
        '        Lines.Add("BLOCK NUANCEACIER")
        '        Lines.Add("   Nuance        = " & s.acier.nuance)
        '        Lines.Add("   Fy            = " & s.acier.f_y.w)
        '        Lines.Add("")
        '        nuancesSave.Add(s.acier.nuance)
        '    End If

        'Next

        ''==[ Sections ]==================================================================================
        'For Each s In List_Section
        '    s.EcrireFile(Lines)
        'Next

    End Sub

    Public Sub RecuperationFile(ByVal FileName As String, ByVal str_warning_file As String,
                                ByRef nuances As List(Of String), ByRef f_y As List(Of Integer))

        '--> Déclaration
        Dim Lines As Cls_LinesOfFile
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String
        Dim ListeBlocIndex As New List(Of Integer)
        Dim ListeBlocCle As New List(Of String)
        Dim IndexFin As Integer

        '--> Initialisation
        If File.Exists(FileName) Then
            Lines = New Cls_LinesOfFile(FileName)
            Me.lSave = True
            Me.CheminFichier = FileName
        Else
            MsgBox("Fichier n'existe pas | File not exist : " & FileName, MsgBoxStyle.Critical, "Cls_Projet/RecuperationFile")
            Exit Sub
        End If

        '--> Repère des mots clés BLOCK
        Try
            For i = 0 To Lines.LineNumber - 1

                DecomposeLine(Lines.Lines(i), Mots, nbMots)

                If nbMots > 0 Then
                    MotCle = Mots(1).Substring(0, Math.Min(4, Mots(1).Length)).ToUpper

                    If MotCle = "BLOC" Then
                        ListeBlocIndex.Add(i)
                        ListeBlocCle.Add(Mots(2).ToUpper)
                    End If

                End If
            Next
        Catch ex As Exception
            MsgBox("Erreur lecture fichier | Error read file", MsgBoxStyle.Critical, "Cls_Project/RecuperationFile")
        End Try

        Dim oldFichier As Boolean = False

        '--> Traitement des blocks
        For i = 0 To ListeBlocIndex.Count - 1
            If i = ListeBlocIndex.Count - 1 Then IndexFin = Lines.Lines.Count - 1 Else IndexFin = ListeBlocIndex(i + 1)
            Select Case ListeBlocCle(i)

                Case "IDENTIFICATION"
                    Me.ReadBloc_Indentification(Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)

                Case "NUANCEACIER"
                    Me.ReadBloc_SteelGrade(Lines.Lines, ListeBlocIndex(i) + 1, IndexFin, nuances, f_y)

                Case "SECTION"

                    Dim s As New cls_Section
                    s.LectureFile(Lines.Lines, ListeBlocIndex(i) + 1, IndexFin)
                    'List_Section.Add(s)

            End Select

        Next

        If oldFichier Then
            MsgBox(str_warning_file, MsgBoxStyle.Critical, "PropMix")
        End If

    End Sub

    Private Sub ReadBloc_Indentification(ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer)
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(4, Mots(1).Length)).ToUpper

                Select Case MotCle
                    Case "UTIL"
                        Me.Utilisateur = ""
                        For z = 2 To nbMots
                            If z = nbMots Then
                                Me.Utilisateur += Mots(z)
                            Else
                                Me.Utilisateur += Mots(z) + " "
                            End If
                        Next
                    Case "ENTR"
                        Me.Entreprise = ""
                        For z = 2 To nbMots
                            If z = nbMots Then
                                Me.Entreprise += Mots(z)
                            Else
                                Me.Entreprise += Mots(z) + " "
                            End If
                        Next
                    Case "NOM"
                        Me.Nom = ""
                        For z = 2 To nbMots
                            If z = nbMots Then
                                Me.Nom += Mots(z)
                            Else
                                Me.Nom += Mots(z) + " "
                            End If
                        Next
                End Select
            End If
        Next

    End Sub

    Private Sub ReadBloc_SteelGrade(ByVal Lignes As List(Of String), ByVal Index0 As Integer, ByVal IndexFin As Integer,
                                    ByRef nuances As List(Of String), ByRef f_y As List(Of Integer))
        '==> Lecture du fichier pour initialiser les attributs

        '--> Déclaration
        Dim i As Integer
        Dim Mots(0) As String, nbMots As Integer
        Dim MotCle As String

        Dim User_Nuance As String = ""
        Dim User_Fy As Integer

        '--> Traitement
        For i = Index0 To IndexFin
            DecomposeLine(Lignes(i), Mots, nbMots)

            If nbMots > 0 Then
                MotCle = Mots(1).Substring(0, Math.Min(4, Mots(1).Length)).ToUpper

                Select Case MotCle
                    Case "NUAN"
                        For z = 2 To nbMots
                            If z = nbMots Then
                                User_Nuance += Mots(z)
                            Else
                                User_Nuance += Mots(z) + " "
                            End If
                        Next
                    Case "FY" : User_Fy = Mots(nbMots)

                End Select
            End If
        Next

        '--> Ajout de la nuance si elle n'existe pas
        If Not nuances.Contains(User_Nuance) Then
            nuances.Add(User_Nuance)
            f_y.Add(User_Fy)
        End If

    End Sub

#End Region

End Class
