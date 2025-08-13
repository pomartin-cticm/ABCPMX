Public Class Cls_ConnecteurArmature

    '================================================================================================================
    '= Classe pour traiter la connexion des slims floors par goujons soudés
    '================================================================================================================

#Region " Attributs "

    ''' <summary>
    ''' Diamètre de l'armature
    ''' </summary>
    Public Diametre As Decimal

    '''' <summary>
    '''' Diametre du trou pratiqué dans l'ame de la poutre
    '''' </summary>
    'Public dhs As Decimal

    '''' <summary>
    '''' Distance entre le centre des trous et la semelle supérieure (arase inférieure)
    '''' </summary>
    'Public ahv As Decimal

    '''' <summary>
    '''' Longueur des armatures 
    '''' </summary>
    'Public Ls As Decimal

    Public Acier As cls_AcierArmature

#End Region

#Region " Constructeurs "
    Sub New()
        Me.Diametre = 25 / 1000
        Acier = New cls_AcierArmature
    End Sub
#End Region

#Region " Fonction de copie "
    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Shared Sub DeepClone(ByVal ArmaSource As Cls_ConnecteurArmature, ByRef ArmaCible As Cls_ConnecteurArmature)
        ArmaCible = ArmaSource.Clone()
        ArmaCible.Acier = ArmaSource.Acier.Clone()
    End Sub

#End Region

#Region " Résistance au cisaillement de l'armature utilisée comme conneteur "

    ''' <summary>
    ''' Résistance au cisaillement selon "German Zulassung"
    ''' </summary>
    ''' <param name="MyBeam"></param>
    ''' <returns></returns>
    Public Function PRdGermanZulassung(MyBeam As cls_Poutre) As Decimal
        Dim PRd As Decimal

        Select Case MyBeam.Section.ProfilA.Tw
            Case < 7.5 / 1000
                PRd = 0 'au cas où
            Case <= 15.5 / 1000
                Select Case MyBeam.Dalle.beton.Classe
                    Case "C25/30"
                        PRd = 117 * 10 ^ 3
                    Case "C30/37"
                        PRd = 125 * 10 ^ 3
                    Case "C35/45"
                        PRd = 135 * 10 ^ 3
                    Case "C40/50", "C45/55", "C50/60", "C55/67"
                        PRd = 122 * 10 ^ 3
                    Case Else
                        PRd = 0 'au cas où
                End Select

            Case Else
                Select Case MyBeam.Dalle.beton.Classe
                    Case "C25/30"
                        PRd = 148 * 10 ^ 3
                    Case "C30/37"
                        PRd = 157 * 10 ^ 3
                    Case "C35/45"
                        PRd = 166 * 10 ^ 3
                    Case "C40/50", "C45/55", "C50/60", "C55/67"
                        PRd = 122 * 10 ^ 3
                    Case Else
                        PRd = 0 'au cas où
                End Select

        End Select

        PRd /= MyBeam.Param.Gamma.GammaVc 'GUD: A confirmer

        Return PRd
    End Function

    ''' <summary>
    ''' Résistance au cisaillement selon l'annexe I de la prEN1994-1-1
    ''' </summary>
    ''' <param name="GammaVs">Coefficient partiel</param>
    ''' <returns></returns>
    Public Function PRdAnnexI(GammaVs As Decimal)
        '--------------------------------------------------------------------------------------------
        '   xx/xx/24 :  Création - GuD
        '--------------------------------------------------------------------------------------------
        '   Calcul du PRd d'une armature selon Annexe I de la prEN 1994-1-1:2025
        '--------------------------------------------------------------------------------------------
        '   GammaVs     [E] :   Coefficient partiel
        '--------------------------------------------------------------------------------------------
        Dim PRd As Decimal

        PRd = Math.PI * Me.Diametre ^ 2 * Me.Acier.FsK / (GammaVs * 4 * Math.Sqrt(3)) * kConvMPaPa

        Return PRd

    End Function

    Public Function PRd(GammaVs As Decimal, Nuance As String, Tw As Decimal, Ha As Decimal) As Decimal
        '--------------------------------------------------------------------------------------------
        '   13/08/24 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   Calcul du PRd d'une armature 
        '   Soit selon Annex I
        '   Soit selon TS/EN 1994-1-102
        '--------------------------------------------------------------------------------------------
        '   GammaVs     [E] :   Coefficient partiel
        '   Nuance      [E] :   Nuance d'acier du profilé dans lequel passe l'armature
        '   Tw          [E] :   Epaisseur de l'âme
        '   Ha          [E] :   Hauteur du profilé
        '--------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim lAnnexI As Boolean = False
        Dim pPRd As Decimal
        Dim iNuance As Integer = Me.ExtraitNombreNuance(Nuance)

        If IsSmaller(Tw, 7.5 / 1000) Then lAnnexI = True
        If IsGreater(Ha, 0.65) Then lAnnexI = True
        If iNuance < 355 Then lAnnexI = True

        If lAnnexI Then
            pPRd = Me.PRdAnnexI(GammaVs)
        Else
            pPRd = 122 * 1000 / GammaVs
        End If

        Return pPRd

    End Function

    Private Function ExtraitNombreNuance(Nuance As String) As Integer
        '--------------------------------------------------------------------------------------------
        '   13/08/24 :  Création - POM
        '--------------------------------------------------------------------------------------------
        '   On recherche la valeur numérique incluse dans la nuance
        '--------------------------------------------------------------------------------------------

        '--( Déclaration

        Dim lTrouve As Boolean = False
        Dim pIndN As Integer
        Dim iCar As Integer = -1
        Dim nbCar As Integer = Nuance.Length

        Do While (Not lTrouve) And (iCar < nbCar - 1)
            iCar += 1

            lTrouve = IsNumeric(Nuance.Substring(iCar, 1))

        Loop

        If lTrouve Then
            pIndN = Nuance.Substring(iCar, 3)
        End If

        Return pIndN

    End Function


#End Region

End Class
