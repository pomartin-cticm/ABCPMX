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
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property nom As String
        Get
            Return Me.m_nom
        End Get
    End Property

    ''' <summary>
    ''' Renvoi la hauteur du cofradal
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property dp As String
        Get
            Return Me.m_dp
        End Get
    End Property

    ''' <summary>
    ''' Renvoi la masse surfacique du cofradal
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property msurf As String
        Get
            Return Me.m_msurf
        End Get
    End Property

    Public ReadOnly Property lCustom As Boolean
        Get
            Return Me.m_lCustom
        End Get
    End Property


#End Region

#Region "Constructeur et Setters"
    ''' <summary>
    ''' Constructeur qui renvoi un cofradal 200 PAC par défaut
    ''' </summary>
    Public Sub New()
        Me.m_nom = "Cofradal 200 PAC"
        CofradalBDD(Me.m_nom)
    End Sub

    Public Sub CofradalBDD(nomloc As String)
        Dim cofradal As (Decimal, Decimal, Boolean) = Mod_Declarations.Get_Cofradal(nomloc)

        If cofradal.Item3 Then
            m_nom = nomloc
            m_dp = cofradal.Item1
            m_msurf = cofradal.Item2

            m_lCustom = False
        End If
    End Sub

    Public Sub CofradalUtilisateur(nom_loc As String, dp_loc As Decimal, msurf_loc As Decimal)
        Me.m_nom = nom_loc
        Me.m_dp = dp_loc
        Me.m_msurf = msurf_loc

        Me.m_lCustom = True
    End Sub

#End Region
End Class
