Imports System.IO
Imports System.Runtime.CompilerServices

Public Class Cls_LinesOfFile

    Private FileName As String
    Public BlocName As String
    Public Lines As New List(Of String)

    Public Const NFACCES As Integer = 10

#Region " CONSTRUCTEURS "

    Public Sub New(ByVal FileName As String)
        ' Lire tous le fichier
        LoadFile(FileName, Me.Lines)

    End Sub

    Public Sub New(ByVal FileName As String, ByVal BlocName As String)
        '------------------------------------------------------------------
        ' BlocName [E] : nom du bloc à lire
        '------------------------------------------------------------------

        Me.BlocName = BlocName
        LoadBlocFile(FileName, BlocName, Me.Lines)

    End Sub

    Public Sub New(ByVal FileName As String, ByVal lStream As Boolean)

        Me.FileName = FileName
        If lStream Then
            RecupereFileStream(FileName, Me.Lines)
        Else
            RecupereFile(FileName, Me.Lines)
        End If

    End Sub

    Public ReadOnly Property LineNumber() As Integer
        Get
            Return Me.Lines.Count
        End Get
    End Property

#End Region

#Region " Outils "

    Public Sub CreationBloc(ByRef Bloc As Dictionary(Of String, String))
        '
        '   Transfert des lignes d'un bloc vers un dictionnaire
        '
        '-----------------------------------------------------------------------------

        Dim indEgal As Integer

        Bloc.Clear()

        For i As Integer = 0 To Me.Lines.Count - 1
            indEgal = Me.Lines(i).IndexOf("=")
            If indEgal > 0 Then
                Bloc.Add(Lines(i).Substring(0, indEgal - 1).Trim.ToUpper,
                         Lines(i).Substring(indEgal + 1).Trim)
            End If
        Next

    End Sub

    Private Sub LoadFile(ByVal FileName As String, ByRef Lines As List(Of String))
        '
        '   08/03/06 :  Création - Version 1.00
        '
        '--------------------------------------------------------------------------------------
        '
        '   Lecture d'un fichier - Transfert des lignes dans la liste de lignes
        '
        '--------------------------------------------------------------------------------------
        '
        '   FileName    [E] :   Nom du fichier
        '   lDecriptage [E] :   Indique si le fichier est cripté        
        '
        '   Lines       [S] :   Lignes du fichiers
        '
        '--------------------------------------------------------------------------------------

        Dim Line As String
        Lines.Clear()

        Try 'Gestion des erreurs à l'ouverture et la lecture du fichier            
            Using sr As New StreamReader(FileName)
                Do
                    Line = sr.ReadLine()

                    Lines.Add(Line)
                Loop Until Line Is Nothing
                sr.Close()
            End Using

        Catch
            MsgBox("Error in reading " & FileName, MsgBoxStyle.Critical, "EcliX")
        Finally
            Lines.RemoveAt(Lines.Count - 1)
        End Try

    End Sub

    Private Sub LoadBlocFile(ByVal FileName As String, ByVal BlocName As String, ByRef Lines As List(Of String))
        '
        '   12/05/14 :  Minh - Version 1.00
        '
        '--------------------------------------------------------------------------------------
        '
        '   Lecture d'un fichier - Transfert des lignes d'un bloc dans la liste de lignes
        '
        '--------------------------------------------------------------------------------------
        '
        '   FileName    [E] :   Nom du fichier
        '   BlocName    [E] :   Nom du bloc à lire
        '
        '   Lines       [S] :   Lignes du fichiers
        '
        '--------------------------------------------------------------------------------------

        '--> Initialisations et déclarations

        Dim LineEnCours As String
        Dim lInBloc As Boolean = False
        Dim lTrouve As Boolean = False
        Dim UpperBloc As String = BlocName.ToUpper.Trim
        Dim lEndBloc As Boolean = False

        Lines.Clear()
        Try 'Gestion des erreurs à l'ouverture et la lecture du fichier        

            Using sr As New StreamReader(FileName)
                Do
                    LineEnCours = sr.ReadLine()

                    If LineEnCours IsNot Nothing Then

                        If lInBloc Then
                            If ((Not lEndBloc) AndAlso (LineEnCours.Trim = String.Empty)) OrElse
                                (lEndBloc AndAlso (LineEnCours.ToUpper = "#ENDBLOC")) Then
                                lInBloc = False
                                lTrouve = True
                            Else
                                Lines.Add(LineEnCours)
                            End If
                        Else
                            If LineEnCours.Trim.ToUpper = UpperBloc.Trim Then
                                lInBloc = True
                            End If
                        End If
                    End If

                Loop Until (LineEnCours Is Nothing) OrElse lTrouve
                sr.Close()
            End Using
        Catch
            MsgBox("Error in loading file " & FileName, MsgBoxStyle.Critical, "PropMix")
        End Try

    End Sub

#End Region

    Public Sub RecupereFileStream(ByVal FileName As String, ByRef Lines As List(Of String))
        '
        '   08/03/06 :  Création - Version 1.00
        '
        '--------------------------------------------------------------------------------------
        '
        '   Lecture d'un fichier - Transfert des lignes dans la liste de lignes
        '
        '--------------------------------------------------------------------------------------
        '
        '   FileName    [E] :   Nom du fichier
        '
        '   Lines       [S] :   Lignes du fichiers
        '
        '--------------------------------------------------------------------------------------

        Dim Line As String
        Lines.Clear()

        Try 'Gestion des erreurs à l'ouverture et la lecture du fichier            
            Using sr As StreamReader = New StreamReader(FileName)
                Do
                    Line = sr.ReadLine()
                    Lines.Add(Line)
                Loop Until Line Is Nothing
                sr.Close()
            End Using

        Catch
            MsgBox("Error in loading file " & FileName, MsgBoxStyle.Critical, "PropMix")
        Finally
            Lines.RemoveAt(Lines.Count - 1)
        End Try

    End Sub

    Public Sub RecupereFile(ByVal FileName As String, ByRef Lines As List(Of String))
        '
        '   08/03/06 :  Création - Version 1.00
        '
        '--------------------------------------------------------------------------------------
        '
        '   Lecture d'un fichier - Transfert des lignes dans la liste de lignes
        '
        '--------------------------------------------------------------------------------------
        '
        '   FileName    [E] :   Nom du fichier
        '
        '   Lines       [S] :   Lignes du fichiers
        '
        '--------------------------------------------------------------------------------------

        Dim LineEnCours As String
        'Dim Line As String
        Lines.Clear()

        Try 'Gestion des erreurs à l'ouverture et la lecture du fichier            
            FileOpen(NFACCES, FileName, OpenMode.Input)
            Do Until (EOF(NFACCES))
                LineEnCours = LineInput(NFACCES)

                Lines.Add(LineEnCours)
            Loop

        Catch
            MsgBox("Error in loading file " & FileName, MsgBoxStyle.Critical, "PropMix")
        Finally
            FileClose(NFACCES)
        End Try

    End Sub

End Class
