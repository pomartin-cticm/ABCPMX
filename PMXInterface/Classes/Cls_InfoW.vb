Public Class Cls_InfoW

#Region " Variables "

    Public InfoW_msg As List(Of String)
    Public InfoW_lVisible As Boolean = False

    Public str_InformationW As String = LogicielInfo.Racine

    Public linfo As Boolean = True

    Public BlocF As New Dictionary(Of String, String)

    Enum enu_ModeW
        Information
        Avertissement
        Erreur
    End Enum

    Public Mode As enu_ModeW = enu_ModeW.Information

    Private CouleurInfo As Color
    Private CouleurErreur As Color
    Private CouleurWarning As Color


#End Region

#Region " Gestion Affichage des infos du logiciel "

    Public Sub New()

        Initialise()

    End Sub

    Public Sub InitialiseErreur()
        Me.Initialise()
        Me.Mode = enu_ModeW.Erreur
    End Sub

    Public Sub InitialiseAvertissement()
        Me.Initialise()
        Me.Mode = enu_ModeW.Avertissement
    End Sub

    Public Sub InitialiseInfo()
        Me.Initialise()
        Me.Mode = enu_ModeW.Information
    End Sub

    Private Sub Initialise()

        Me.CouleurErreur = Color.DarkRed
        Me.CouleurWarning = Color.DarkOrange
        Me.CouleurInfo = CouleurBackBandeaux

        InfoW_msg = New List(Of String)
    End Sub

    Public Sub AddInfo(ByVal info As String)
        If InfoW_msg Is Nothing Then
            InfoW_msg = New List(Of String)
        End If
        InfoW_msg.Add(info)
    End Sub

    Public Sub Publie()
        InfoW_lVisible = True
        ' Frm_InfoLogiciel.ShowDialog()
        Frm_InfoLogicielN.ShowDialog()
    End Sub

    Public Sub Fermer()

        'Frm_InfoLogiciel.Close()
        Frm_InfoLogicielN.Close()

    End Sub
#End Region

#Region " Propriétés & fonctions "

    Public ReadOnly Property CouleurBase
        Get
            Dim myColor As Color
            Select Case Me.Mode
                Case enu_ModeW.Avertissement : myColor = Me.CouleurWarning
                Case enu_ModeW.Erreur : myColor = Me.CouleurErreur
                Case enu_ModeW.Information : myColor = Me.CouleurInfo
            End Select
            Return myColor
        End Get
    End Property

#End Region

End Class
