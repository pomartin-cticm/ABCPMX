Public Class cls_Cofradal

#Region " Déclarations "

    ''' <summary>
    ''' Tableau des cofradals
    ''' item1: nom du cofradal
    ''' item2: hauteur (en m)
    ''' item3: masse surfacique (N/m2) 
    ''' </summary>
    Public Shared TAB_CofraDal As (String, Decimal, Decimal)() =
        {
        ("Cofradal 200 PAC", 0.125, 525),
        ("Cofradal 230 PAC", 0.125, 475),
        ("Cofradal 260 PAC", 0.185, 925),
        ("Cofradal 200 Prefab", 0.2, 2400),
        ("Cofradal 230 Prefab", 0.23, 3100),
        ("Cofradal 260 Prefab", 0.26, 2800)
        }

#End Region

#Region " Attributs privés "

    ''' <summary>
    ''' nom du cofradal
    ''' </summary>
    Private p_nom As String

    ''' <summary>
    ''' Hauteur du cofradal
    ''' </summary>
    Private p_dp As Decimal

    ''' <summary>
    ''' masse surfacique du cofradal
    ''' </summary>
    Private p_msurf As Decimal                  ' en N/m2

    ''' <summary>
    ''' indique si les données sont renseignées par l'utilisateur (True) ou si elles sont issues de la BDD (False)
    ''' </summary>
    Private p_lCustom As Boolean

#End Region

#Region " Propriétés "

    ''' <summary>
    ''' Renvoi le nom du cofradal
    ''' En cas de modification, lCustom = True 
    ''' </summary>
    ''' <returns></returns>
    Public Property Nom As String
        Get
            Return Me.p_nom
        End Get
        Set(value As String)
            Me.p_nom = value
            Me.p_lCustom = True
        End Set
    End Property

    ''' <summary>
    ''' Renvoi la hauteur du cofradal
    ''' En cas de modification, lCustom = True 
    ''' </summary>
    ''' <returns></returns>
    Public Property dp As Decimal
        Get
            Return Me.p_dp
        End Get
        Set(value As Decimal)
            Me.p_dp = value
            Me.p_lCustom = True
        End Set
    End Property

    ''' <summary>
    ''' Renvoi la masse surfacique du cofradal
    ''' En cas de modification, lCustom = True 
    ''' </summary>
    ''' <returns></returns>
    Public Property mSurf As Decimal
        Get
            Return Me.p_msurf
        End Get
        Set(value As Decimal)
            Me.p_msurf = value
            Me.p_lCustom = True
        End Set
    End Property

    Public Property lCustom As Boolean
        Get
            Return Me.p_lCustom
        End Get
        Set(value As Boolean)
            Me.p_lCustom = value
        End Set
    End Property


#End Region

#Region " Constructeur "
    ''' <summary>
    ''' Constructeur qui renvoi un cofradal 200 PAC par défaut
    ''' </summary>
    Public Sub New()
        Me.p_nom = cls_Cofradal.TAB_CofraDal(0).Item1 '"Cofradal 200 PAC"
        Dim lOK As Boolean
        SetCofradalBDD(Me.p_nom, lok)
    End Sub

#End Region

#Region " Outils "

    ''' <summary>
    ''' Permet de définir un cofradal par rapport au nom renseigné (la hauteur et la masse surfacique sont lues dans la BDD)
    ''' L'appel de cette fonction implique lCustom = False 
    ''' Retour un booléen qui indique si le cofradal a été trouvé (True) ou non (False)
    ''' </summary>
    ''' <param name="nomloc"></param>
    Public Sub SetCofradalBDD(nomloc As String, ByRef lOK As Boolean)
        Dim cofradal As (Decimal, Decimal, Boolean) = Me.Get_Cofradal(nomloc)

        If cofradal.Item3 Then
            p_nom = nomloc
            p_dp = cofradal.Item1
            p_msurf = cofradal.Item2

            p_lCustom = False
        End If

        lOK = cofradal.Item3
    End Sub

    ''' <summary>
    ''' Fonction qui renvoi la hauteur (item1) et la masse surfacique (item2) du cofradal dont le nom est passé en argument
    ''' Item1: Hauteur du cofradal
    ''' Item2: Masse surfacique du cofradal
    ''' Item3 indique si le nom a été trouvé dans la BDD (True) ou non (False)
    ''' </summary>
    ''' <param name="nom"></param>
    ''' <returns></returns>
    Private Function Get_Cofradal(ByVal nom As String) As (Decimal, Decimal, Boolean)

        '--> Déclaration 

        Dim retour As (Decimal, Decimal, Boolean) = (0, 0, False)
        Dim i As Integer = 0

        '--> Recherche du nom dans la BDD

        While i < cls_Cofradal.TAB_CofraDal.Length And Not retour.Item3

            retour.Item3 = nom = cls_Cofradal.TAB_CofraDal(i).Item1 'indique si le nom correspond

            If retour.Item3 Then 'récupère les données le cas échéant
                retour.Item1 = cls_Cofradal.TAB_CofraDal(i).Item2 'on récupère la hauteur du cofradal
                retour.Item2 = cls_Cofradal.TAB_CofraDal(i).Item3 'on récupère la masse surfacique du cofradal
            End If

            i += 1

        End While

        '--> Retour

        Return retour
    End Function


    Public Shared Function Get_ListName_Cofradal() As String()
        '--> Déclaration
        Dim strRetour As String()
        ReDim strRetour(cls_Cofradal.TAB_CofraDal.Length - 1)

        '--> Copie des noms des cofradals
        For i As Integer = 0 To cls_Cofradal.TAB_CofraDal.Length - 1
            strRetour(i) = cls_Cofradal.TAB_CofraDal(i).Item1
        Next

        Return strRetour
    End Function


#End Region

#Region " Fonctions de copie "
    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Sub CopieFrom(ByRef CofradalSource As cls_Cofradal)
        Me.p_nom = CofradalSource.Nom
        Me.p_dp = CofradalSource.dp
        Me.p_msurf = CofradalSource.mSurf
        Me.p_lCustom = CofradalSource.lCustom
    End Sub

#End Region

End Class
