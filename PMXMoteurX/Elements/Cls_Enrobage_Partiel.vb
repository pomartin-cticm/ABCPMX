Imports System.Security.Cryptography
'Imports PropMix_Engine.Cls_Dalle

Public Class cls_Enrobage_Partiel

#Region " Structures "

    Structure struc_LitArma
        Dim Phi As Decimal
        Dim lArma As Boolean
        Dim nbArma As Integer
        Dim Aire As Decimal
        Dim zArma As Decimal
    End Structure

    Const PhiEtriersDEF As Decimal = 0.006

    Enum EnuTypeEtriers
        Cadre
        CadreTraversant
        EtrierSoude
    End Enum

#End Region

#Region " Attributs "

    Public lArmaConst As Boolean                    ' Indique si armatures de construction dans le lit inf
    Public ConstPhi As Decimal                      ' Diametre des armatures de construction

    Public Ratio_bc As Decimal                      ' Ratio largeur de béton/largeur profilé

    Public Etriers_Type As EnuTypeEtriers


    Public Etriers_Phi As Decimal                   ' Diamètre des étriers
    Public Etriers_EnrobageY As Decimal             ' Enrobage des barres d'étriers // Y (horizontal)
    Public Etriers_EnrobageZ As Decimal             ' Enrobage des barres d'étriers // Z (vertical)

    Private pEtriers_EnrobYinterne As Decimal

#End Region

#Region " Elements de l'enrobage "

    Public LitArma(2) As cls_ArmatureEnrobage       ' Lits d'armature longitudinale (O inférieure / 1 milieu / 2 supérieure)

    Public Beton As New cls_Beton                   ' béton de l'enrobage

    Public AcierArmatures As New cls_AcierArmature  ' Acier des armatures

#End Region

#Region " Fonctions et outils "

    Public Function Get_b_c(bf As Decimal) As Decimal
        Return Ratio_bc * bf
    End Function

    ''' <summary>
    ''' Calcul des propriétés
    ''' </summary>
    Public Sub Calcul_Proprietes()



    End Sub

    ''' <summary>
    ''' Renvoie l'enrobage //yy des étriers par rapport à l'âme du profilé
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Etriers_EnrobageYinterne As Decimal
        Get
            Return pEtriers_EnrobYinterne
        End Get
    End Property


    Public Function Get_Phi_Max() As Decimal
        Dim phi_max As Decimal

        For i As Integer = 0 To LitArma.Length - 1
            If LitArma(i).Get_Phi_Max > phi_max Then phi_max = LitArma(i).Get_Phi_Max
        Next

        Return phi_max

    End Function

#End Region

#Region " Constructeur "

    Sub New()

        'Me.b_c = 0.192

        Me.Ratio_bc = 1
        Me.Etriers_EnrobageY = 0.01
        Me.Etriers_EnrobageZ = 0.01
        Me.pEtriers_EnrobYinterne = 0.01
        Me.Etriers_Phi = PhiEtriersDEF

        Me.Etriers_Type = EnuTypeEtriers.Cadre

        '--> Armatures longitudinales

        For i As Integer = 0 To 2
            Me.LitArma(i) = New cls_ArmatureEnrobage
        Next

        '--> Lit d'armatures inférieur

        'Me.LitsArmaOLD(0).Phi = 0.008
        'Me.LitsArmaOLD(0).lArma = True
        'Me.LitsArmaOLD(0).nbArma = 2
        Me.lArmaConst = False
        ConstPhi = 0.008

        '--> Lit intermédiaire

        'Me.LitsArmaOLD(1).Phi = 0.008
        'Me.LitsArmaOLD(1).lArma = False
        'Me.LitsArmaOLD(1).nbArma = 2

        '--> Lit d'armatures supérieur

        'Me.LitsArmaOLD(2).Phi = 0.008
        'Me.LitsArmaOLD(2).lArma = True
        'Me.LitsArmaOLD(2).nbArma = 2

        Me.Beton = New cls_Beton()

    End Sub

#End Region

#Region " Ecriture Fichier "

    ''' <summary>
    ''' Ecriture des attributs pour enregistrement dans un fichier 
    ''' </summary>
    ''' <param name="Lines">Lignes d'écriture</param>
    Public Sub EcrireFile(ByRef Lines As List(Of String))

        'Lines.Add("   Eb_c          = " & b_c)
        ' Lines.Add("   Ef_y          = " & acier_armature)

        With Beton
            Lines.Add("   EBType        = " & .lLeger)
            Lines.Add("   EBClasse      = " & .Classe)
            Lines.Add("   EBFck         = " & .Fck)
        End With

        'With arma_longi_inf
        '    Lines.Add("   EABc          = " & .c_s)
        '    Lines.Add("   EABd          = " & .PhiS)
        '    Lines.Add("   EABn          = " & .n_s)
        '    Lines.Add("   EABz          = " & .z_s)
        '    Lines.Add("   EABe          = " & .EspBar)
        'End With

        'With arma_longi_sup
        '    Lines.Add("   EAHc          = " & .c_s)
        '    Lines.Add("   EAHd          = " & .PhiS)
        '    Lines.Add("   EAHn          = " & .n_s)
        '    Lines.Add("   EAHz          = " & .z_s)
        '    Lines.Add("   EAHe          = " & .EspBar)
        'End With

    End Sub

#End Region

#Region " Fonction de copie "

    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Sub DeepClone(ByVal EnrobagePartielSource As cls_Enrobage_Partiel, ByRef EnrobagePartielCible As cls_Enrobage_Partiel)
        EnrobagePartielCible = EnrobagePartielSource.Clone

        'ReDim EnrobagePartielCible.LitsArmaOLD(EnrobagePartielSource.LitsArmaOLD.GetUpperBound(0))
        'EnrobagePartielCible.LitsArmaOLD = EnrobagePartielSource.LitsArmaOLD.Clone

        ReDim EnrobagePartielCible.LitArma(EnrobagePartielSource.LitArma.GetUpperBound(0))
        For i As Integer = 0 To EnrobagePartielCible.LitArma.Length - 1
            EnrobagePartielCible.LitArma(i) = EnrobagePartielSource.LitArma(i).Clone
        Next

        EnrobagePartielCible.Beton = EnrobagePartielSource.Beton.Clone
        EnrobagePartielCible.AcierArmatures = EnrobagePartielSource.AcierArmatures.Clone

    End Sub

    Public Shared Sub DeepCopie(EnrobageSource As cls_Enrobage_Partiel, ByRef EnrobageCible As cls_Enrobage_Partiel)

        EnrobageCible = EnrobageSource.Clone
        'EnrobageCible.LitsArmaOLD = EnrobageSource.LitsArmaOLD.Clone
        EnrobageCible.Beton = EnrobageSource.Beton.Clone

    End Sub

#End Region

End Class
