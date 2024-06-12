Public Class Cls_ConnecteurArmature

#Region " Attributs "

    ''' <summary>
    ''' Diamètre de l'armature
    ''' </summary>
    Public ds As Decimal

    ''' <summary>
    ''' Diametre du trou pratiqué dans l'ame de la poutre
    ''' </summary>
    Public dhs As Decimal

    ''' <summary>
    ''' Distance entre le centre des trous et la semelle supérieure (arase inférieure)
    ''' </summary>
    Public ahv As Decimal

    ''' <summary>
    ''' Longueur des armatures 
    ''' </summary>
    Public Ls As Decimal

    Public Acier As New cls_AcierArmature

#End Region

#Region " Constructeurs "

#End Region

#Region " Fonction de copie "
    Private Function Clone() '--> Utilisé pour dupliquer une soudure
        Return Me.MemberwiseClone()
    End Function

    Public Sub DeepClone(ArmaSource As Cls_ConnecteurArmature, ArmaCible As Cls_ConnecteurArmature)
        ArmaCible = ArmaSource.Clone
        ArmaCible.Acier = ArmaSource.Acier.Clone
    End Sub

#End Region

#Region " Résistance au cisaillement du conneteur "

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
    ''' <param name="MyBeam"></param>
    ''' <returns></returns>
    Public Function PRdAnnexI(MyBeam As cls_Poutre)
        Dim PRd As Decimal

        PRd = Math.PI * Me.ds ^ 2 * Me.Acier.FsK / (MyBeam.Param.Gamma.GammaVs * 4 * Math.Sqrt(3)) * kConvMPaPa

        Return PRd
    End Function
#End Region
End Class
