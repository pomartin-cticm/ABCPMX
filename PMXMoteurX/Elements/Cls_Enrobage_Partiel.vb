Imports System.Security.Cryptography
Imports PropMix_Engine.Cls_Dalle

Public Class Cls_Enrobage_Partiel

#Region " Structures "

    Structure struc_LitArma
        Dim Phi As Decimal
        Dim lArma As Boolean
        Dim nbArma As Integer
        Dim Aire As Decimal
        Dim zArma As Decimal
    End Structure

    Enum EnuTypeEtriers
        Cadre
        CadreTraversant
        EtrierSoude
    End Enum

#End Region

#Region " Attributs "

    ''' <summary>
    ''' Définiton des lits d'armatures longitudinales
    ''' </summary>
    Public LitsArma(2) As struc_LitArma

    ''' <summary>
    ''' Indique si armatures de construction dans le lit inf
    ''' </summary>
    Public lArmaConst As Boolean

    ''' <summary>
    ''' Diametre des armatures de construction
    ''' </summary>
    Public ConstPhi As Decimal

    ''' <summary>
    ''' largeur de béton
    ''' </summary>
    Public b_c As Decimal

    ''' <summary>
    ''' Ratio largeur de béton/largeur profilé
    ''' </summary>
    Public Ratio_bc As Decimal

    Public Etriers_Type As EnuTypeEtriers

    ''' <summary>
    ''' Diamètre des étriers
    ''' </summary>
    Public Etriers_Phi As Decimal

    ''' <summary>
    ''' Enrobage des barres d'étriers // Y (horizontal)
    ''' </summary>
    Public Etriers_EnrobageY As Decimal

    ''' <summary>
    ''' Enrobage des barres d'étriers // Z (vertical)
    ''' </summary>
    Public Etriers_EnrobageZ As Decimal

#End Region

#Region " Elements de l'enrobage "

    ''' <summary>
    ''' Armatures longitudinales supérieur
    ''' </summary>
    Public arma_longi_sup As New Cls_Armatures_Longi

    ''' <summary>
    ''' Armatures longitudinales inférieur
    ''' </summary>
    Public arma_longi_inf As New Cls_Armatures_Longi

    ''' <summary>
    ''' béton de l'enrobage
    ''' </summary>
    Public beton As New Cls_Beton

    ''' <summary>
    ''' Acier des armatures
    ''' </summary>
    Public AcierArmatures As New Cls_AcierArmature

#End Region

#Region " Fonction de calcul "

    ''' <summary>
    ''' Calcul des propriétés
    ''' </summary>
    Public Sub Calcul_Proprietes()

        With arma_longi_sup
            .A_s = .n_s * Math.PI * .PhiS ^ 2 / 2
        End With

        With arma_longi_inf
            .A_s = .n_s * Math.PI * .PhiS ^ 2 / 2
        End With

    End Sub

#End Region

#Region " Constructeur "

    Sub New()


        'Me.b_c = 0.192

        Me.Ratio_bc = 1
        Me.Etriers_EnrobageY = 0.01
        Me.Etriers_EnrobageZ = 0.01
        Me.Etriers_Phi = 0.006

        Me.arma_longi_inf.n_s = 1
        Me.Etriers_Phi = 0.006

        Me.Etriers_Type = EnuTypeEtriers.Cadre

        '--> Lit d'armatures inférieur

        Me.LitsArma(0).Phi = 0.008
        Me.LitsArma(0).lArma = True
        Me.LitsArma(0).nbArma = 2
        Me.lArmaConst = False
        ConstPhi = 0.008

        '--> Lit intermédiaire

        Me.LitsArma(1).Phi = 0.008
        Me.LitsArma(1).lArma = False
        Me.LitsArma(1).nbArma = 2

        '--> Lit d'armatures supérieur

        Me.LitsArma(2).Phi = 0.008
        Me.LitsArma(2).lArma = True
        Me.LitsArma(2).nbArma = 2

        Me.Beton = New Cls_Beton()

    End Sub

#End Region

#Region " Ecriture Fichier "

    ''' <summary>
    ''' Ecriture des attributs pour enregistrement dans un fichier 
    ''' </summary>
    ''' <param name="Lines">Lignes d'écriture</param>
    Public Sub EcrireFile(ByRef Lines As List(Of String))

        Lines.Add("   Eb_c          = " & b_c)
        ' Lines.Add("   Ef_y          = " & acier_armature)

        With beton
            Lines.Add("   EBType        = " & .type)
            Lines.Add("   EBClasse      = " & .classe)
            Lines.Add("   EBFck         = " & .Fck)
        End With

        With arma_longi_inf
            Lines.Add("   EABc          = " & .c_s)
            Lines.Add("   EABd          = " & .PhiS)
            Lines.Add("   EABn          = " & .n_s)
            Lines.Add("   EABz          = " & .z_s)
            Lines.Add("   EABe          = " & .EspBar)
        End With

        With arma_longi_sup
            Lines.Add("   EAHc          = " & .c_s)
            Lines.Add("   EAHd          = " & .PhiS)
            Lines.Add("   EAHn          = " & .n_s)
            Lines.Add("   EAHz          = " & .z_s)
            Lines.Add("   EAHe          = " & .EspBar)
        End With

    End Sub

#End Region

#Region " Fonction de copie "

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

#End Region

End Class
