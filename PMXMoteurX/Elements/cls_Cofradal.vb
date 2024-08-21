Public Class cls_Cofradal

#Region "Attributs"

    ''' <summary>
    ''' nom du cofradal
    ''' </summary>
    Private m_nom As String

    ''' <summary>
    ''' Hauteur du cofradal
    ''' </summary>
    Private m_dp As Decimal

    ''' <summary>
    ''' masse surfacique du cofradal
    ''' </summary>
    Private m_msurf As Decimal

    ''' <summary>
    ''' indique si les données sont renseignées par l'utilisateur (True) ou si elles sont issues de la BDD (False)
    ''' </summary>
    Private m_lCustom As Boolean

#End Region

#Region "Propriétés"

    ''' <summary>
    ''' Renvoi le nom du cofradal
    ''' En cas de modification, lCustom = True 
    ''' </summary>
    ''' <returns></returns>
    Public Property nom As String
        Get
            Return Me.m_nom
        End Get
        Set(value As String)
            Me.m_nom = value
            Me.m_lCustom = True
        End Set
    End Property

    ''' <summary>
    ''' Renvoi la hauteur du cofradal
    ''' En cas de modification, lCustom = True 
    ''' </summary>
    ''' <returns></returns>
    Public Property dp As Decimal
        Get
            Return Me.m_dp
        End Get
        Set(value As Decimal)
            Me.m_dp = value
            Me.m_lCustom = True
        End Set
    End Property

    ''' <summary>
    ''' Renvoi la masse surfacique du cofradal
    ''' En cas de modification, lCustom = True 
    ''' </summary>
    ''' <returns></returns>
    Public Property msurf As Decimal
        Get
            Return Me.m_msurf
        End Get
        Set(value As Decimal)
            Me.m_msurf = value
            Me.m_lCustom = True
        End Set
    End Property

    Public Property lCustom As Boolean
        Get
            Return Me.m_lCustom
        End Get
        Set(value As Boolean)
            Me.m_lCustom = value
        End Set
    End Property


#End Region

#Region "Constructeur"
    ''' <summary>
    ''' Constructeur qui renvoi un cofradal 200 PAC par défaut
    ''' </summary>
    Public Sub New()
        Me.m_nom = "Cofradal 200 PAC"
        AjouteCofradalBDD(Me.m_nom)
    End Sub

#End Region

#Region "Setters"

    ''' <summary>
    ''' Permet de définir un cofradal par rapport au nom renseigné (la hauteur et la masse surfacique sont lues dans la BDD)
    ''' L'appel de cette fonction implique lCustom = False 
    ''' Retour un booléen qui indique si le cofradal a été trouvé (True) ou non (False)
    ''' </summary>
    ''' <param name="nomloc"></param>
    Public Function AjouteCofradalBDD(nomloc As String) As Boolean
        Dim cofradal As (Decimal, Decimal, Boolean) = Mod_DeclarationsX.Get_Cofradal(nomloc)

        If cofradal.Item3 Then
            m_nom = nomloc
            m_dp = cofradal.Item1
            m_msurf = cofradal.Item2

            m_lCustom = False
        End If

        Return cofradal.Item3
    End Function

#End Region

#Region "Fonctions de copie"
    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

#End Region

End Class
