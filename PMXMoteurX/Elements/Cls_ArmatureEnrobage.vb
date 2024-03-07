Public Class cls_ArmatureEnrobage

#Region " Constantes "

    Const PhiDEF As Decimal = 0.008

#End Region

#Region " Attributs "

    Public PhiExt As Decimal            ' Diamètre des barres placées à l'extérieur
    Public NbExt As Decimal             ' Nombre de barres placées à l'extérieur / chambre
    Public lActiveExt As Boolean        ' Indique si barre active longitudinalement (construction sinon)

    Public PhiMil As Decimal            ' Diamètre des barres placées au centre
    Public NbMil As Decimal             ' Nombre de barres placées au centre / chambre

    Public PhiInt As Decimal            ' Diamètre des barres placées à l'extérieur
    Public NbInt As Decimal             ' Nombre de barres placées à l'extérieur / chambre
    Public lActiveInt As Boolean        ' Indique si barre active longitudinalement (construction sinon)

    Public zPosRatio As Decimal         ' Position z du lit (utilisé pour le lit central uniquement)

#End Region

#Region " Constructeurs "

    Public Sub New()
        PhiExt = PhiDEF
        NbExt = 1
        lActiveExt = True

        PhiMil = PhiDEF
        NbMil = 0

        PhiInt = PhiDEF
        NbInt = 1
        lActiveInt = True

        Me.zPosRatio = 0.5
    End Sub

#End Region

#Region " Fonctions "

    ''' <summary>
    ''' Aire des armatures pour un lit donné et pour UNE seule chambre
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Aire
        Get
            Dim pAire As Decimal

            pAire = NbExt * Math.PI * PhiExt ^ 2 / 4
            pAire += NbMil * Math.PI * PhiMil ^ 2 / 4
            pAire += NbInt * Math.PI * PhiInt ^ 2 / 4

            Return pAire
        End Get
    End Property

    Public Function Get_Phi_Max() As Decimal
        Dim phi_max As Decimal
        If NbExt <> 0 And PhiExt > phi_max Then phi_max = PhiExt
        If NbMil <> 0 And PhiMil > phi_max Then phi_max = PhiMil
        If NbInt <> 0 And PhiInt > phi_max Then phi_max = PhiInt

        Return phi_max

    End Function

    Public Function NbBarres(iPos As Integer) As Integer
        '--------------------------------------------------------------------------------
        '   12/07/23 :  Création - POM
        '--------------------------------------------------------------------------------
        ' Retourne le nombre de barres dans une grappe
        '--------------------------------------------------------------------------------
        '   iPos    [E] :   0 pour la grappe exterieure
        '                   1 pour la grappe intermédiaire
        '                   2 pour la grappe interieure
        '--------------------------------------------------------------------------------

        Dim NbB As Integer

        Select Case iPos
            Case 0 : NbB = Me.NbExt
            Case 1 : NbB = Me.NbMil
            Case 2 : NbB = Me.NbInt
        End Select

        Return NbB
    End Function

    Public Function lBarreActive(iArma As Integer, iPos As Integer) As Boolean
        '--------------------------------------------------------------------------------
        '   12/07/23 :  Création - POM
        '--------------------------------------------------------------------------------
        ' Retourne si la barre est active
        '--------------------------------------------------------------------------------
        '   iArma   [E] :   0 pour le lit inférieur
        '                   1 pour le lit intermédiaire
        '                   2 pour le lit supérieur
        '   iPos    [E] :   0 pour la grappe exterieure
        '                   1 pour la grappe intermédiaire
        '                   2 pour la grappe interieure
        '--------------------------------------------------------------------------------

        Dim NbB As Integer = Me.NbBarres(iPos)
        Dim lActive As Boolean

        Select Case iPos
            Case 0 : lActive = (NbB > 1) Or (NbB <= 1 And lActiveExt)
            Case 1 : lActive = iArma = 0
            Case 2 : lActive = (NbB > 1) Or (NbB <= 1 And lActiveInt)
        End Select

        Return lActive
    End Function

    Public Function PhiBarre(iPos As Integer) As Decimal
        '--------------------------------------------------------------------------------
        '   12/07/23 :  Création - POM
        '--------------------------------------------------------------------------------
        ' Retourne le diamètre de barres dans une grappe
        '--------------------------------------------------------------------------------
        '   iPos    [E] :   0 pour la grappe exterieure
        '                   1 pour la grappe intermédiaire
        '                   2 pour la grappe interieure
        '--------------------------------------------------------------------------------

        Dim Phi As Decimal

        Select Case iPos
            Case 0 : Phi = Me.PhiExt
            Case 1 : Phi = Me.PhiMil
            Case 2 : Phi = Me.PhiInt
        End Select

        Return Phi
    End Function

    Public ReadOnly Property NbTotalBarresActives() As Integer
        '------------------------------------------------------------------------------------------------------------
        '   22/02/24 :  Création - POM
        '------------------------------------------------------------------------------------------------------------
        '   Calcul du nombre total de barres actives dans le lit
        '------------------------------------------------------------------------------------------------------------

        Get
            '--( Initialisation
            Dim Nb As Integer = 0

            '--( Calcul

            If Me.lActiveExt Then Nb += Me.NbExt
            Nb += Me.NbMil
            If Me.lActiveInt Then Nb += Me.NbInt

            Return Nb

        End Get
    End Property

#End Region

#Region " Fonctions de copie "
    Public Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function
#End Region

End Class
