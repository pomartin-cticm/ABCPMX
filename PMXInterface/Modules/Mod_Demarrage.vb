Module Mod_Demarrage

#Region "===DEMARRAGE==="

    Public Sub Main()

        'MsgBox("Hello PMX")

        Application.EnableVisualStyles()

        InitialiseLogiciel()

        Frm_PMX.ShowDialog()


    End Sub

    Public Sub InitialiseLogiciel()
        '---------------------------------------------------------------------------------------------------------------
        '   25/05/2023 :    POM - Création
        '---------------------------------------------------------------------------------------------------------------
        '   Initialisation générale des paramètres du logiciel ABCPMX-II
        '---------------------------------------------------------------------------------------------------------------
        '---------------------------------------------------------------------------------------------------------------

        '--> Récupération des informations générales du logociel - Non modifiable par l'utilisateur

        LogicielInfo.NomLogiciel = "ABCPMX-II"
        LogicielInfo.Version = "1.0"
        LogicielInfo.Extension = "pmx"
        LogicielInfo.Racine = "ABCPMX"

        LogicielInfo.Maitre = EnuMaitre.CTICM

        LogicielRep.RepertoireInstall = Application.StartupPath
        LogicielRep.RepertoireConfig = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\CTICM\" & LogicielInfo.NomLogiciel & "\ConfigV" & LogicielInfo.Version

        '--> Répertoires

        If Not IO.Directory.Exists(LogicielRep.RepertoireConfig) Then 'R22-001
            IO.Directory.CreateDirectory(LogicielRep.RepertoireConfig)
        End If

        '--> Langues

        LogicielInfo.ListeLangue = {"English", "Français"}
        LogicielInfo.ListeLangueNDC = {"English", "Français"}

        '--> Normes

        LogicielInfo.ListeNorme = {"EN 1994-1-1", "prEN 1994-1-1"} 'Normes

        '--> Unités

        LogicielInfo.Unit_Longueur = {"mm", "cm", "m"}              'Unités des longueurs/dimensions - mm pour les dimensions et m pour les longueurs par défaut (interne m)
        LogicielInfo.Transfert_Longueur = {0.001, 0.01, 1}
        LogicielInfo.Format_Longueur = {"0.0", "0.00", "0.000"}
        LogicielInfo.NbDigitMax_Longueur = {1, 2, 3}

        LogicielInfo.Unit_Effort = {"N", "daN", "kN"}               'Unités des efforts - kN par défaut     (interne N)
        LogicielInfo.Transfert_Effort = {1, 10, 1000}
        LogicielInfo.Format_Effort = {"0.0", "0.00", "0.0"}
        LogicielInfo.NbDigitMax_Effort = {0, 1, 3}

        LogicielInfo.Unit_Moment = {"N.m", "daN.m", "kN.m"}         'Unités des moments - kN.m par défaut   (interne Nm)
        LogicielInfo.Transfert_Moment = {1, 10, 1000}
        LogicielInfo.Format_Moment = {"0.0", "0.00", "0.0"}
        LogicielInfo.NbDigitMax_Longueur = {0, 1, 3}

        LogicielInfo.Unit_Inerties = {"mm4", "cm4", "m4"}           'Unités des inerties - cm4 par défaut    (interne m4)
        LogicielInfo.Transfert_Inerties = {0.001 ^ 4, 0.01 ^ 4, 1 ^ 4}
        LogicielInfo.Format_Inerties = {"0.0", "0.00", "0.0000"}
        LogicielInfo.NbDigitMax_Inerties = {0, 0, 6}

        LogicielInfo.Unit_Contraintes = {"P", "kPa", "MPa"}         'Unités des contraintes - MPa par défaut (interne MPa)
        LogicielInfo.Transfert_Contraintes = {0.000001, 0.0001, 1}
        LogicielInfo.Format_Contraintes = {"0", "0", "0.0"}
        LogicielInfo.NbDigitMax_Contraintes = {1, 2, 3}

        LogicielInfo.Unit_ModulesY = {"MPa", "GPa"}                 'Unités des modules d'élasticité - GPa par défaut (interne MPa)
        LogicielInfo.Transfert_ModulesY = {1, 1000}
        LogicielInfo.Format_ModulesY = {"0", "0.0"}
        LogicielInfo.NbDigitMax_ModulesY = {0, 3}

        '--> Récûpération des options du logiciel - modifiable par l'utilisateur
        Try
            '===> Options générales <============================================================================================

            '--> Mode Expert
            LogicielOptions.lExpert = My.Settings.lExpertMode

            '--> Langues
            LogicielOptions.IndLangue = Array.IndexOf(LogicielInfo.ListeLangue, LogicielInfo.ListeLangue(My.Settings.IndLangue))
            LogicielOptions.IndLangueNDC = Array.IndexOf(LogicielInfo.ListeLangueNDC, LogicielInfo.ListeLangueNDC(My.Settings.IndLangueNDC))

            '--> Unités
            LogicielOptions.IndUnitDimension = Array.IndexOf(LogicielInfo.Unit_Longueur, LogicielInfo.Unit_Longueur(My.Settings.indUnitDimension))
            LogicielOptions.IndUnitLongueur = Array.IndexOf(LogicielInfo.Unit_Longueur, LogicielInfo.Unit_Longueur(My.Settings.indUnitLongueur))

            '--> Fichiers récents
            LogicielFichiers.RecentFiles = New List(Of String)
            If My.Settings.RecentFiles IsNot Nothing Then
                For i = 0 To My.Settings.RecentFiles.Count - 1
                    LogicielFichiers.RecentFiles.Add(My.Settings.RecentFiles(i))
                Next
            End If

        Catch ex As Exception

        End Try

        LogicielOptions.lFenetres = True

        '--> MAJ des noms de fichiers langue

        UpdateLNGFileName()
        UpdateLNGFileName_NDC()

    End Sub

#End Region


#Region " Gestion Langue "

    ''' <summary>
    ''' Mise à jour du nom du fichier langue Interface
    ''' </summary>
    Public Sub UpdateLNGFileName()

        If (LogicielInfo.ListeLangue.Count > 0) AndAlso (LogicielOptions.IndLangue >= 0) AndAlso (LogicielOptions.IndLangue < LogicielInfo.ListeLangue.Count) Then

            'Langue installée
            LogicielFichiers.Langue = LogicielRep.RepertoireInstall & "\Langues\" & LogicielInfo.Racine & "_" &
                                      LogicielInfo.ListeLangue(LogicielOptions.IndLangue).Substring(0, 2).ToUpper & ".lng"

        Else
            LogicielFichiers.Langue = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' Mise à jour du nom du fichier langue Note de calcul
    ''' </summary>
    Public Sub UpdateLNGFileName_NDC()

        If (LogicielInfo.ListeLangueNDC.Count > 0) AndAlso (LogicielOptions.IndLangueNDC >= 0) AndAlso (LogicielOptions.IndLangueNDC < LogicielInfo.ListeLangueNDC.Count) Then

            'Langue installée
            LogicielFichiers.LangueNDC = LogicielRep.RepertoireInstall & "\Langues\" & LogicielInfo.Racine & "_" _
                                       & LogicielInfo.ListeLangueNDC(LogicielOptions.IndLangueNDC).Substring(0, 2).ToUpper & ".lng"

        Else
            LogicielFichiers.Langue = String.Empty
        End If

    End Sub

#End Region


End Module
