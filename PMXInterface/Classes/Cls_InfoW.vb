Public Class Cls_InfoW

#Region " Variables "

    Public InfoW_msg As List(Of String)
    Public InfoW_lVisible As Boolean = False

    Public str_InformationW As String = LogicielInfo.Racine

    Public linfo As Boolean = True

    Public BlocF As New Dictionary(Of String, String)
#End Region

#Region " Gestion Affichage des infos du logiciel "

    Public Sub New()
        Initialise()

    End Sub

    Public Sub Initialise()
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
        Frm_InfoLogiciel.ShowDialog()
    End Sub

    Public Sub Fermer()

        Frm_InfoLogiciel.Close()

    End Sub
#End Region


End Class
