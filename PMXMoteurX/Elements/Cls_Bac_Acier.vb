Imports PropMix_Engine.Cls_Dalle

Public Class Cls_Bac_Acier

#Region " Attributs pour l'interface "

    ''' <summary>
    ''' Nom du bac
    ''' </summary>
    Public Etiquettte As String

    ''' <summary>
    ''' Indique si il est récupéré de la base de données ou non
    ''' </summary>
    Public lDatabase As Decimal

#End Region

#Region " Attributs "

    ''' <summary>
    ''' hauteur du bac, y compris le raidisseur supérieur
    ''' </summary>
    Public h_pg As Decimal

    ''' <summary>
    ''' hauteur du bac, non compris le raidisseur supérieur
    ''' </summary>
    Public h_p As Decimal

    ''' <summary>
    ''' Largeur de nervure en creux d'onde (en bas du bac) b1
    ''' </summary>
    Public b_b As Decimal

    ''' <summary>
    ''' Largeur de nervure au somment (en haut du bac) b2
    ''' </summary>
    Public b_t As Decimal

    ''' <summary>
    ''' Entraxe des nervures
    ''' </summary>
    Public e_p As Decimal

    ''' <summary>
    ''' Epaisseur de tôle
    ''' </summary>
    Public t As Decimal

    ''' <summary>
    ''' Orientation du bac
    ''' </summary>
    Public orientation As Enum_Orientation

#End Region

#Region " Enumérations "

    Public Enum Enum_Orientation
        Parallele
        Perpendiculaire
    End Enum

#End Region

#Region " Constructeur "

    Sub New()

        Me.lDatabase = False
        Me.orientation = Enum_Orientation.Parallele

        Me.b_b = 0.062
        Me.b_t = 0.101
        Me.h_pg = 0.058
        Me.h_p = 0.058
        Me.e_p = 0.207

    End Sub

    Sub New(ByVal etiquette As String, ByVal b_b As Decimal, ByVal b_t As Decimal, ByVal h_p As Decimal, ByVal h_rs As Decimal, ByVal e_p As Decimal, ByVal t As Decimal)

        Me.lDatabase = True
        Me.Etiquettte = etiquette
        Me.b_b = b_b
        Me.b_t = b_t
        Me.h_pg = h_rs + h_p
        Me.h_p = h_p
        Me.e_p = e_p
        Me.t = t

    End Sub

#End Region

#Region " Ecriture Fichier "

    ''' <summary>
    ''' Ecriture des attributs pour enregistrement dans un fichier 
    ''' </summary>
    ''' <param name="Lines">Lignes d'écriture</param>
    Public Sub EcrireFile(ByRef Lines As List(Of String))

        Lines.Add("   BOrient       = " & orientation)
        Lines.Add("   BDatabase     = " & lDatabase)
        Lines.Add("   BEtiquette    = " & Etiquettte)
        Lines.Add("   Bbb           = " & b_b)
        Lines.Add("   Bbt           = " & b_t)
        Lines.Add("   Bhpg          = " & h_pg)
        Lines.Add("   Bhp           = " & h_p)
        Lines.Add("   Bep           = " & e_p)

    End Sub

#End Region

#Region " Fonction de copie "

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

#End Region

#Region " Outils "

    Public ReadOnly Property LargeurBmoyenne As Decimal
        Get
            Return (Me.b_b + Me.b_t) / 2
        End Get

    End Property

#End Region

End Class
