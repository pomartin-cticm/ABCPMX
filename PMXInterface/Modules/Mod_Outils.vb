Imports System.IO
Imports PMXMoteur2

Module Mod_Outils

#Region " Gestion Langue "

    '''' <summary>
    '''' Mise à jour du nom du fichier langue Interface
    '''' </summary>
    'Public Sub UpdateLNGFileName()

    '    If (LogicielInfo.ListeLangue.Count > 0) AndAlso (LogicielOptions.IndLangue >= 0) AndAlso (LogicielOptions.IndLangue < LogicielInfo.ListeLangue.Count) Then

    '        'Langue installée
    '        LogicielFichiers.Langue = LogicielRep.RepertoireInstall & "\Langues\" & LogicielInfo.NomLogiciel & "_" &
    '                                  LogicielInfo.ListeLangue(LogicielOptions.IndLangue).Substring(0, 2).ToUpper & ".lng"

    '    Else
    '        LogicielFichiers.Langue = String.Empty
    '    End If

    'End Sub

    ''' <summary>
    ''' Mise à jour du nom du fichier langue Note de calcul
    ''' </summary>
    Public Sub UpdateLNGFileName_NDC()

        If (LogicielInfo.ListeLangueNDC.Count > 0) AndAlso (LogicielOptions.IndLangueNDC >= 0) AndAlso (LogicielOptions.IndLangueNDC < LogicielInfo.ListeLangueNDC.Count) Then

            'Langue installée
            LogicielFichiers.LangueNDC = LogicielRep.RepertoireInstall & "\Langues\" & LogicielInfo.NomLogiciel & "_" &
                                         LogicielInfo.ListeLangueNDC(LogicielOptions.IndLangueNDC).Substring(0, 2).ToUpper & ".lng"

        Else
            LogicielFichiers.Langue = String.Empty
        End If

    End Sub

#End Region

#Region " Vérification des données "

    ''' <summary>
    ''' Transforme les points ou virgule d'une chaine en sepérateur decimal
    ''' </summary>
    ''' <param name="Chaine"></param>
    ''' <returns></returns>
    Public Function TraiteReal(ByVal Chaine As String) As String

        Dim SepDecimal As String = Mid(Format(1.1, "0.0"), 2, 1)

        If SepDecimal = "," Then
            Dim iPoint As Integer = InStr(Chaine, ".")
            If iPoint > 0 Then
                Mid(Chaine, iPoint, 1) = ","
            End If
        ElseIf SepDecimal = "." Then
            Dim iPoint As Integer = InStr(Chaine, ",")
            If iPoint > 0 Then
                Mid(Chaine, iPoint, 1) = "."
            End If
        End If

        Return Chaine

    End Function

    ''' <summary>
    ''' Controle d'une valeur numérique saisie
    ''' </summary>
    ''' <param name="txtSaisie">    Text à vérifier                     </param>
    ''' <param name="lValMin">      Indique si controle borne inférieure</param>
    ''' <param name="ValMin">       Borne Inférieure Autorisée          </param>
    ''' <param name="lValMax">      Indique si controle borne supérieure</param>
    ''' <param name="ValMax">       Borne Supérieure Autorisée          </param>
    ''' <returns>0 saisie correcte | -1 champ vide | -2 chaine non numérique | -3 valeur hors des bornes</returns>
    Public Function ValideSaisieNombre(ByRef txtSaisie As String,
                                     ByVal lValMin As Boolean, ByVal ValMin As Double,
                                     ByVal lValMax As Boolean, ByVal ValMax As Double) As Integer

        '--> Initialisation
        Dim Chaine As String = TraiteReal(txtSaisie)
        Dim ValNum As Double

        '--> Traitement
        If Chaine = "" Then                 '--> Controle de champ vide
            Return -1
        ElseIf Not IsNumeric(Chaine) Then   '--> Controle de champ non numerique
            Return -2
        End If

        ValNum = CDbl(Chaine)

        If (lValMin And (ValNum < ValMin)) Or (lValMax And (ValNum > ValMax)) Then '--> Controle de la valeur saisie
            Return -3
        Else
            Return 0 '--> Pas d'erreur de saisie
        End If

    End Function

    '''' <summary>
    '''' Affichage d'un ErrorProvider en fonction d'une erreur
    '''' </summary>
    '''' <param name="iSaisie"></param>
    '''' <param name="Control"></param>
    '''' <param name="MyErr"></param>
    '''' <param name="ValMin"></param>
    '''' <param name="ValMax"></param>
    '''' <param name="TextError"></param>
    'Public Sub NotifieErreurSaisie(ByVal iSaisie As Integer, ByVal Control As Control, ByVal MyErr As ErrorProvider, ByVal ValMin As Double, ByVal ValMax As Double, Optional ByVal TextError As String = "")

    '    Select Case iSaisie
    '        Case -1
    '            'Erreur saisie : Textbox vide------------------------------------
    '            MyErr.SetError(Control, ErreurNonNul_LNG)
    '        Case -2
    '            'Erreur saisie : valeur non numérique----------------------------
    '            MyErr.SetError(Control, ErreurNonNum_LNG)
    '        Case -3
    '            'Erreur saisie : valeur hors limite------------------------------
    '            Dim Chaine As String = ErreurHorsBornes_LNG + " : " & ValMin & " ≤ x ≤ " & ValMax
    '            MyErr.SetError(Control, Chaine)
    '        Case -4
    '            'Erreur : Divers ------------------------------------------------
    '            MyErr.SetError(Control, TextError)
    '    End Select

    'End Sub

    '''' <summary>
    '''' Affichage d'un ErrorProvider en fonction d'une erreur
    '''' </summary>
    '''' <param name="iSaisie"></param>
    '''' <param name="Control"></param>
    '''' <param name="MyErr"></param>
    '''' <param name="ValMin"></param>
    '''' <param name="TextError"></param>
    'Public Sub NotifieErreurSaisie_BorneInf(ByVal iSaisie As Integer, ByVal Control As Control, ByVal MyErr As ErrorProvider, ByVal ValMin As Double, Optional ByVal TextError As String = "")

    '    Select Case iSaisie
    '        Case -1
    '            'Erreur saisie : Textbox vide------------------------------------
    '            MyErr.SetError(Control, ErreurNonNul_LNG)
    '        Case -2
    '            'Erreur saisie : valeur non numérique----------------------------
    '            MyErr.SetError(Control, ErreurNonNum_LNG)
    '        Case -3
    '            'Erreur saisie : valeur hors limite------------------------------
    '            Dim Chaine As String = ErreurHorsBornes_LNG + " : " & ValMin & " ≤ x"
    '            MyErr.SetError(Control, Chaine)
    '        Case -4
    '            'Erreur : Divers ------------------------------------------------
    '            MyErr.SetError(Control, TextError)
    '    End Select

    'End Sub

    '''' <summary>
    '''' Affichage d'un ErrorProvider en fonction d'une erreur
    '''' </summary>
    '''' <param name="iSaisie"></param>
    '''' <param name="Control"></param>
    '''' <param name="MyErr"></param>
    '''' <param name="ValMax"></param>
    '''' <param name="TextError"></param>
    'Public Sub NotifieErreurSaisie_BorneSup(ByVal iSaisie As Integer, ByVal Control As Control, ByVal MyErr As ErrorProvider, ByVal ValMax As Double, Optional ByVal TextError As String = "")

    '    Select Case iSaisie
    '        Case -1
    '            'Erreur saisie : Textbox vide------------------------------------
    '            MyErr.SetError(Control, ErreurNonNul_LNG)
    '        Case -2
    '            'Erreur saisie : valeur non numérique----------------------------
    '            MyErr.SetError(Control, ErreurNonNum_LNG)
    '        Case -3
    '            'Erreur saisie : valeur hors limite------------------------------
    '            Dim Chaine As String = ErreurHorsBornes_LNG + " : " & " x ≤ " & ValMax
    '            MyErr.SetError(Control, Chaine)
    '        Case -4
    '            'Erreur : Divers ------------------------------------------------
    '            MyErr.SetError(Control, TextError)
    '    End Select

    'End Sub

    '''' <summary>
    '''' Affichage d'un ErrorProvider en fonction d'une erreur dans une dataGridView
    '''' </summary>
    '''' <param name="iSaisie"></param>
    '''' <param name="cell"></param>
    '''' <param name="ValMin"></param>
    '''' <param name="ValMax"></param>
    '''' <param name="TextError"></param>
    'Public Sub NotifieErreurSaisie_DataGridView(ByVal iSaisie As Integer, ByVal cell As DataGridViewCell, ByVal ValMin As Double, ByVal ValMax As Double, Optional ByVal TextError As String = "")

    '    Select Case iSaisie
    '        Case -1
    '            'Erreur saisie : Textbox vide------------------------------------
    '            cell.ErrorText = ErreurNonNul_LNG
    '        Case -2
    '            'Erreur saisie : valeur non numérique----------------------------
    '            cell.ErrorText = ErreurNonNum_LNG
    '        Case -3
    '            'Erreur saisie : valeur hors limite------------------------------
    '            Dim Chaine As String = ErreurHorsBornes_LNG + " : " & ValMin & " ≤ x ≤ " & ValMax
    '            cell.ErrorText = Chaine
    '        Case -4
    '            'Erreur : Divers ------------------------------------------------
    '            cell.ErrorText = TextError
    '    End Select

    'End Sub

    ''' <summary>
    ''' Fonction qui empeche l'utilisateur de renseigné les valeurs au dela du mm
    ''' </summary>
    ''' <param name="sender">Textbox traité</param>
    ''' <param name="e">Evenement traité</param>
    Public Sub CheckChiffreSignificatif(sender As Object, e As KeyPressEventArgs)

        '--> Bloquer les valeurs au mm près
        Dim val() As String = TraiteReal(sender.text).Split(",")

        If sender.name = "TextBox_L" Or sender.name = "TextBox_L_A" Then 'Longueurs du logiciel

            If val.Count > 1 And sender.SelectedText.length = 0 Then
                If val(1).Length >= 1 And LogicielOptions.IndUnitLongueur = 0 Then
                    e.Handled = True
                ElseIf val(1).Length >= 2 And LogicielOptions.IndUnitLongueur = 1 Then
                    e.Handled = True
                ElseIf val(1).Length >= 4 And LogicielOptions.IndUnitLongueur = 2 Then
                    e.Handled = True
                End If
            End If

        Else 'Dimensions

            If val.Count > 1 And sender.SelectedText.length = 0 Then
                If val(1).Length >= 1 And LogicielOptions.IndUnitDimension = 0 Then
                    e.Handled = True
                ElseIf val(1).Length >= 2 And LogicielOptions.IndUnitDimension = 1 Then
                    e.Handled = True
                ElseIf val(1).Length >= 4 And LogicielOptions.IndUnitDimension = 2 Then
                    e.Handled = True
                End If
            End If

        End If

    End Sub

#End Region

#Region " Divers "

    ''' <summary>
    ''' Ouvre une fenêtre de la messagerie par défaut (Outlook)
    ''' avec un mail prérempli pour faire un retour de bug
    ''' </summary>
    Public Sub PrepareMailSupport()

        '--> Déclaration
        Dim destinataire As String = "support.logiciels@cticm.com"
        Dim objet As String '--> sujet du mail
        Dim corps As String '--> Corps du mail

        '--> Remplissage du mail en fonction de la langue (que français ou anglais)
        If LogicielOptions.IndLangue = 1 Then '--> Français
            objet = "Bug(s) détecté(s) dans le logiciel " & LogicielInfo.NomLogiciel & " - Version " & LogicielInfo.Version

            corps = "Bonjour,%0A"
            corps += "%0A"
            corps += "Nous sommes l'entreprise [Insérer le nom de votre entreprise].%0A"
            corps += "Nous avons détecté un ou des bugs dans le logiciel " & LogicielInfo.NomLogiciel & " (Version " & LogicielInfo.Version & ").%0A"
            corps += "Description du problème :%0A"
            corps += "%0A"
            corps += "[Si c'est possible, merci d'attacher au mail une capture d'écran du problème et le fichier '*." & LogicielInfo.Extension & "' de votre projet]%0A"
            corps += "%0A"
            corps += "Cordialement,%0A"
            corps += "%0A"
            corps += "[Votre nom]%0A"
        Else '--> Anglais
            objet = "Bug detected in " & LogicielInfo.NomLogiciel & " software - Version " & LogicielInfo.Version

            corps = "Hello,%0A"
            corps += "%0A"
            corps += "We are the company [Insert your company name].%0A"
            corps += "We detected a bug in " & LogicielInfo.NomLogiciel & " software (Version " & LogicielInfo.Version & ").%0A"
            corps += "Description of the problem :%0A"
            corps += "%0A"
            corps += "[If it's possible, please attach to the email a screenshot of the problem and the file '*." & LogicielInfo.Extension & "' of your project]%0A"
            corps += "%0A"
            corps += "Regards,%0A"
            corps += "%0A"
            corps += "[Your name]%0A"
        End If

        Try
            System.Diagnostics.Process.Start(String.Format("mailto:{0}?subject={1}&body={2}", destinataire, objet, corps))
        Catch ex As Exception
            MsgBox("Error Email Support : " & Chr(13) &
                   " - Ecrire un mail à support.logiciels@cticm.com avec comme objet 'Support " & LogicielInfo.NomLogiciel & "'" & Chr(13) &
                   " - Write an email at support.logiciels@cticm.com with the subject 'Support " & LogicielInfo.NomLogiciel & "'", MsgBoxStyle.Critical, "Mod_Outils/PrepareMailSupport")
        End Try

    End Sub

    ''' <summary>
    ''' Recupère depuis le chemin du fichier complet le chemin du repertoire
    ''' </summary>
    ''' <param name="FileName"></param>
    ''' <returns></returns>
    Public Function RecupRepertoire(ByVal FileName As String)

        Dim newDirectory As String
        Dim nLen, i As Integer
        Dim lTrouve As Boolean

        nLen = Len(FileName) 'Taille du chemin
        lTrouve = False
        i = nLen

        Do While (Not lTrouve) And (i > 0)
            If Mid(FileName, i, 1) = "\" Then lTrouve = True
            i -= 1
        Loop

        If lTrouve Then
            newDirectory = Mid(FileName, 1, i + 1)
        Else
            newDirectory = ""
        End If

        Return newDirectory

    End Function

#End Region

#Region " Affichage donnée "

    ''' <summary>
    ''' Fournit un format d'affichage pour une valeur avec un nombre prédéfini de chiffres significatifs
    ''' </summary>
    ''' <param name="Valeur">Valeur traitée</param>
    ''' <param name="nbSignificatif">Nombre de chiffres significatifs</param>
    ''' <param name="nbDigitMax">Nombre maxi de digits après la virgule</param>
    ''' <returns></returns>
    Public Function GetFormatSignificatif(ByVal Valeur As Double,
                                          ByVal nbSignificatif As Integer,
                                          ByVal nbDigitMax As Integer) As String

        '--[ Déclarations

        Dim Donnee As Double
        Dim nbDigit, nbZero As Long
        Dim MyFormat As String = ""
        Dim EpsilonV As Double = 0.0000000001#

        '--[ Traitement
        Try
            If Math.Abs(Valeur) - EpsilonV < 0 Then
                MyFormat = "0.0"
            Else
                Donnee = Math.Floor(Math.Log10(Math.Abs(Valeur)))

                nbDigit = CLng(Donnee) + 1

                nbZero = Math.Max(0, nbSignificatif - nbDigit)

                If nbDigitMax >= 0 Then
                    nbZero = Math.Min(nbDigitMax, nbZero)

                    If Math.Abs(Valeur) < (1 / 10 ^ nbDigitMax) Then nbZero = 1
                End If

                MyFormat = "0."
                For i As Long = 1 To nbZero
                    MyFormat &= "0"
                Next

            End If

        Catch ex As Exception
            MsgBox("Erreur du traitement", MsgBoxStyle.Critical, "Mod_Outils/GetFormatSignificatif")
        End Try

        Return MyFormat

    End Function

    ''' <summary>
    ''' Renvoie une chaine de caractères pour l'affichage d'une valeur
    ''' </summary>
    ''' <param name="Valeur">Valeur traitée</param>
    ''' <param name="Type">Type de la valeur traitée</param>
    ''' <param name="nbSign">Nombre de chiffres significatifs</param>
    ''' <param name="nbDigitMax">Nombre maxi de digits après la virgule</param>
    ''' <param name="lUnite">Indique si affichage des Unités</param>
    ''' <param name="VA">Indique si la valeur doit être affiché comme une valeur absolue</param>
    ''' <returns></returns>
    Public Function GetStringInUnit(ByVal Valeur As Decimal, ByVal Type As Enu_TypeVariable,
                                    ByVal nbSign As Integer, ByVal nbDigitMax As Integer, ByVal lUnite As Boolean, Optional ByVal VA As Boolean = False) As String

        '--[ Déclarations

        Dim ValeurU As Double
        Dim kUnitU As Double
        'Dim lUniteReconnue As Boolean = true
        Dim MyFormat As String
        Dim Unite As String = ""
        Const SEP As String = " "

        '--[ Traitement

        Select Case Type

            Case Enu_TypeVariable.Longueur

                kUnitU = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
                Unite = SEP & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

            Case Enu_TypeVariable.Effort

                kUnitU = LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort)
                Unite = SEP & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)

            Case Enu_TypeVariable.Moment

                kUnitU = LogicielInfo.Transfert_Moment(LogicielOptions.IndUnitMoment)
                Unite = SEP & LogicielInfo.Unit_Moment(LogicielOptions.IndUnitMoment)

            Case Enu_TypeVariable.ModuleY
                kUnitU = LogicielInfo.Transfert_ModulesY(LogicielOptions.IndUnitModulesY)
                Unite = SEP & LogicielInfo.Unit_ModulesY(LogicielOptions.IndUnitModulesY)

            Case Enu_TypeVariable.Contrainte
                kUnitU = LogicielInfo.Transfert_Contraintes(LogicielOptions.IndUnitContraintes)
                Unite = SEP & LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

            Case Enu_TypeVariable.ContrainteMPa

                kUnitU = 1
                Unite = SEP & "MPa"

            Case Enu_TypeVariable.ContrainteGPa

                kUnitU = 1000
                Unite = SEP & "GPa"

            'Case Enu_TypeVariable.Degre

            '    kUnitU = 1
            '    Unite = "°"

            'Case Enu_TypeVariable.RadianToDegre

            '    kUnitU = Math.PI / 180
            '    Unite = "°"

            Case Enu_TypeVariable.SansType

                kUnitU = 1
                Unite = ""

            Case Enu_TypeVariable.InertieCM4

                kUnitU = LogicielInfo.Transfert_Longueur(1) ^ 4
                Unite = SEP & LogicielInfo.Unit_Longueur(1) & "\+4\="


            Case Enu_TypeVariable.Millimetre

                kUnitU = LogicielInfo.Transfert_Longueur(0)
                Unite = SEP & LogicielInfo.Unit_Longueur(0)



        End Select

        ValeurU = Valeur / kUnitU
        MyFormat = GetFormatSignificatif(ValeurU, nbSign, nbDigitMax)

        If ValeurU >= 0 Then VA = False '--> Affichage VA seulement si valeur négative

        If lUnite Then
            If VA Then
                Return "|" & Format(ValeurU, MyFormat) & "|" & Unite
            Else
                Return Format(ValeurU, MyFormat) & Unite
            End If
        Else
            If VA Then
                Return "|" & Format(ValeurU, MyFormat) & "|"
            Else
                Return Format(ValeurU, MyFormat)
            End If
        End If

    End Function

    ''' <summary>
    ''' Renvoie la chaine à afficher en fonction du type de variable (sans les unités)
    ''' </summary>
    ''' <param name="Valeur">   [E] Valeur à afficher   </param>
    ''' <param name="Type">     [E] Type d'unités       </param>
    ''' <returns></returns>
    Public Function GetStringNoUnit(ByVal Valeur As Double, ByVal Type As Enu_TypeVariable) As String

        Dim Chaine As String
        Dim kUnit As Decimal
        Dim Fmt As String = ""

        Select Case Type
            Case Enu_TypeVariable.Longueur
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
                Fmt = LogicielInfo.Format_Longueur(LogicielOptions.IndUnitLongueur)

            Case Enu_TypeVariable.Dimension
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
                Fmt = LogicielInfo.Format_Longueur(LogicielOptions.IndUnitDimension)

            'Case Enu_TypeVariable.Dimension
            '    kUnit = InfoLogiciel.Transfert_Length(OptionsLogiciel.IndUnitDimension)
            '    Fmt = InfoLogiciel.Format_Length(OptionsLogiciel.IndUnitDimension)


            'Case Enu_TypeVariable.Millimetres
            '    Chaine = Format(Valeur / kUnitLongueur(indUnitMILLIMETRE), fmtUnitLongueur(indUnitMILLIMETRE))
            Case Enu_TypeVariable.Effort
                kUnit = LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort)
                Fmt = LogicielInfo.Format_Effort(LogicielOptions.IndUnitEffort)

            Case Enu_TypeVariable.Inertie
                kUnit = LogicielInfo.Transfert_Inerties(LogicielOptions.IndUnitInerties)
                Fmt = LogicielInfo.Format_Inerties(LogicielOptions.IndUnitInerties)

            Case Enu_TypeVariable.SansType
                kUnit = 1
                Fmt = "0.00"

            Case Enu_TypeVariable.Moment
                kUnit = LogicielInfo.Transfert_Moment(LogicielOptions.IndUnitMoment)
                Fmt = LogicielInfo.Format_Moment(LogicielOptions.IndUnitMoment)

            Case Enu_TypeVariable.ModuleY
                kUnit = LogicielInfo.Transfert_ModulesY(LogicielOptions.IndUnitModulesY)
                Fmt = LogicielInfo.Format_ModulesY(LogicielOptions.IndUnitModulesY)

            Case Enu_TypeVariable.Contrainte
                kUnit = LogicielInfo.Transfert_Contraintes(LogicielOptions.IndUnitContraintes)
                Fmt = LogicielInfo.Format_Contraintes(LogicielOptions.IndUnitContraintes)

        End Select

        Chaine = Format(Valeur / kUnit, Fmt)

        Return Chaine

    End Function

#End Region

#Region " Vérification dernière version du logiciel "

    ''' <summary>
    ''' Télécharge sur le site du CTICM un fichier permettant de 
    ''' vérifier que la version du logiciel est la dernière disponible 
    ''' --> Redirige vers le site si nouvelle version dispo
    ''' --> Si pas de connection à Internet = avertissement
    ''' </summary>
    ''' <param name="MessageRetour">Indique si message si logiciel est à jour</param>
    Public Sub VerifVersionLogiciel(ByVal MessageRetour As Boolean)

        Try

            '--> Textes utilisés dans le msg d'avertissement de version
            Dim NoInternet As String = ""
            Dim NewVersion As String = ""
            Dim VersionLink As String = ""
            Dim LastVersion As String = ""
            Dim Content As String = ""

            If File.Exists(LogicielFichiers.Langue) Then
                Dim Bloc As New Dictionary(Of String, String)
                Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_JURIDIQUE")
                BlocLine.CreationBloc(Bloc)

                Try

                    NoInternet = Bloc("NOINTERNET")
                    NewVersion = Bloc("NEWVERSION")
                    VersionLink = Bloc("VERSIONLINK")
                    LastVersion = Bloc("LASTVERSION")

                Catch ex As Exception
                    MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, "Mod_Outils/VerifVersionLogiciel")
                Finally
                    Bloc.Clear()
                End Try

            End If

            '--> Répertoire du fichier 
            Dim FichierUpdate As String = LogicielRep.RepertoireConfig & "\Update_PropMix.txt"

            '--> Test de connexion à internet
            If My.Computer.Network.IsAvailable Then

                '--> Suppresion du fichier s'il existe pour le mettre à jour
                If My.Computer.FileSystem.FileExists(FichierUpdate) Then
                    My.Computer.FileSystem.DeleteFile(FichierUpdate)
                End If

                Try
                    '--> Téléchargement du fichier sur le site 
                    My.Computer.Network.DownloadFile("http://www.cticm.com/maj/Update_PropMix.txt", FichierUpdate)

                    '--> Déclaration
                    Dim Lines As New Cls_LinesOfFile(FichierUpdate) '--> Fichier convertit en lignes
                    Dim Mots(0) As String, nbMots As Integer
                    Dim ListeBlocIndex As New List(Of Integer)
                    Dim ListeBlocCle As New List(Of String)
                    Dim NewVersionAvailable As Boolean = False
                    Dim Link As String = ""

                    '--> Lecture du fichier
                    For i = 0 To Lines.LineNumber - 1

                        '--> Traitement ligne par ligne du fichier
                        'DecomposeLine(Lines.Lines(i), SEPARATEURS, Mots, nbMots)

                        If nbMots > 0 Then
                            If Mots(1) = "VERSION" Then                 '--> Nouvelle version du logiciel disponible
                                If Mots(2) > LogicielInfo.Version Then
                                    NewVersionAvailable = True
                                End If
                            ElseIf Mots(1) = "LINK" Then                '--> Récupération du lien de la page du logiciel
                                Link = Mots(2)
                            ElseIf Mots(1) = "CONTENT" Then             '--> Récupération du contenu de la nouvelle maj
                                For z As Integer = 2 To Mots.Count - 1
                                    If z = 2 Then
                                        Content &= Mots(z)
                                    Else
                                        Content += " " & Mots(z)
                                    End If
                                Next
                            End If
                        End If
                    Next

                    '--> Affichage
                    If NewVersionAvailable Then

                        '--> Message d'avertissement - Nouvelle version dispo
                        Dim dlg As New DialogResult

                        '--> Texte affiché
                        Dim msg As String = NewVersion & Chr(13)

                        msg += Chr(13) & Content & Chr(13)
                        msg += Chr(13) & VersionLink

                        dlg = MessageBox.Show(msg, LogicielInfo.NomLogiciel, MessageBoxButtons.YesNo, MessageBoxIcon.Information)

                        If dlg = Windows.Forms.DialogResult.Yes Then
                            '--> Ouverture page web - site CTICM - page du logiciel
                            Try
                                Process.Start(Link)
                            Catch ex As Exception
                                MsgBox("Erreur d'ouverture du lien | Error opening link", MsgBoxStyle.Critical, "Mod_Outils/VerifVersionLogiciel")
                            End Try

                        End If
                        '--> Réponse MsgBox = Non : utilisation classique du logiciel

                    ElseIf MessageRetour Then
                        MsgBox(LastVersion, MsgBoxStyle.Information, LogicielInfo.NomLogiciel)
                    End If

                Catch ex As Exception
                    Console.WriteLine("Erreur téléchargement fichier update")
                    If LogicielOptions.lExpert Then
                        MsgBox("Erreur téléchargement fichier update", MsgBoxStyle.Exclamation, LogicielInfo.NomLogiciel)
                    End If
                End Try

            Else
                '--> Message d'avertissement - Pas de connection à internet
                MsgBox(NoInternet, MsgBoxStyle.Exclamation, LogicielInfo.NomLogiciel)
            End If

        Catch ex As Exception
            MsgBox("Erreur vérification version  | Error check version", MsgBoxStyle.Critical, "Mod_Outils/VerifVersionLogiciel")
        End Try

    End Sub

#End Region

#Region " Dessin des symboles "

    ''' <summary>
    ''' Affichage d'un symbole (+indice) d'équation
    ''' </summary>
    ''' <param name="MyGr">Graphics dans lequel on dessine</param>
    ''' <param name="BrushEcrire">Pinceau pour écrire</param>
    ''' <param name="Symbol">Symbole à dessiner</param>
    ''' <param name="Indice">Indice du Symbole</param>
    ''' <param name="xPen">Position du Stylo pour écrire</param>
    ''' <param name="yPen">Position du Stylo pour écrire</param>
    ''' <param name="lGrec">Indique si symbole de l'alphabet grec</param>
    ''' <param name="Alignement">Alignement du symbole</param>
    ''' <param name="FontNormal">Police de caractère normale</param>
    ''' <param name="FontSymbol">Police de caractère pour les symboles grecs</param>
    ''' <param name="FontIndice">Police de caractère pour les indices</param>
    ''' <param name="kAdjust">Ajustement de la position de l'indice</param>
    ''' <param name="DrawEgal">Dessin du signe = à la fin</param>
    Public Sub DrawSymbol(ByVal MyGr As Graphics, ByVal BrushEcrire As Brush,
                          ByVal Symbol As String, ByVal Indice As String,
                          ByVal xPen As Single, ByVal yPen As Single,
                          ByVal lGrec As Boolean, lItalic As Boolean, ByVal Alignement As Enu_Alignement,
                          ByVal FontNormal As Font, ByVal FontSymbol As Font,
                          ByVal FontIndice As Font, ByVal kAdjust As Single, ByVal DrawEgal As Boolean)
        '----------------------------------------------------------------------------------------
        '
        '   21/02/08 :  Création - Version 1.00
        '
        '----------------------------------------------------------------------------------------
        '
        '   Fonction qui retourne la longueur d'un symbole (+indice) d'équation
        '
        '----------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   Symbol      [E] :   Symbole à dessiner
        '   Indice      [E] :   Indice du Symbole
        '   lGrec       [E] :   Indice si symbole de l'alphabet grec
        '   FontNormal  [E] :   Police de caractère normale
        '   FontSymbol  [E] :   Police de caractère pour les symboles grecs
        '   FontIndice  [E] :   Police de caractère pour les indices
        '
        '----------------------------------------------------------------------------------------

        '--> Déclarations

        Dim LongueurString As Single = LongueurChaine(MyGr, Symbol, Indice, lGrec, FontNormal, FontSymbol, FontIndice, kAdjust, DrawEgal)
        Dim HauteurString As Single = MyGr.MeasureString("X", FontNormal).Height

        Dim xStar As Single

        '--> Positionnement

        Select Case Alignement
            Case Enu_Alignement.Gauche
                xStar = xPen - LongueurString
            Case Enu_Alignement.Droite
                xStar = xPen
            Case Enu_Alignement.Centre
                xStar = xPen - LongueurString / 2
        End Select

        '--> Préparation de la police

        Dim MyFontNormal As Font
        If lItalic Then
            MyFontNormal = New Font(FontNormal, FontStyle.Italic)
        Else
            MyFontNormal = FontNormal
        End If

        '--> Ecriture Symbole

        If lGrec Then
            MyGr.DrawString(Symbol, FontSymbol, BrushEcrire, xStar, yPen)
            xStar += MyGr.MeasureString(Symbol, FontSymbol).Width - kAdjust * MyGr.MeasureString(" ", FontSymbol).Width
        Else
            MyGr.DrawString(Symbol, MyFontNormal, BrushEcrire, xStar, yPen)
            xStar += MyGr.MeasureString(Symbol, MyFontNormal).Width - kAdjust * MyGr.MeasureString(" ", MyFontNormal).Width
        End If

        '--> Ecriture Indice

        Dim DecalIndice As Single = HauteurString / 3

        MyGr.DrawString(Indice, FontIndice, BrushEcrire, xStar, yPen + DecalIndice)

        If DrawEgal Then
            xStar += MyGr.MeasureString(Indice, FontIndice).Width ' - kAdjust * MyGr.MeasureString(" ", FontNormal).Width

            MyGr.DrawString("=", FontNormal, BrushEcrire, xStar, yPen)
        End If

    End Sub

    ''' <summary>
    ''' Fonction qui retourne la longueur d'un symbole (+indice) d'équation
    ''' </summary>
    ''' <param name="MyGr">Graphics dans lequel on dessine</param>
    ''' <param name="Symbol">Symbole à dessiner</param>
    ''' <param name="Indice">Indice du Symbole</param>
    ''' <param name="lGrec">Indice si symbole de l'alphabet grec</param>
    ''' <param name="FontNormal">Police de caractère normale</param>
    ''' <param name="FontSymbol">Police de caractère pour les symboles grecs</param>
    ''' <param name="FontIndice">Police de caractère pour les indices</param>
    ''' <param name="kAdjust">Ajustement de la position de l'indice</param>
    ''' <param name="DrawEgal">Dessin du signe = à la fin</param>
    ''' <returns>Longueur du symbole</returns>
    Public Function LongueurChaine(ByVal MyGr As Graphics, ByVal Symbol As String,
                                   ByVal Indice As String, ByVal lGrec As Boolean,
                                   ByVal FontNormal As Font, ByVal FontSymbol As Font,
                                   ByVal FontIndice As Font, ByVal kAdjust As Single, Optional ByVal DrawEgal As Boolean = False) As Single

        If lGrec Then
            If DrawEgal Then
                Return MyGr.MeasureString(Symbol, FontSymbol).Width + MyGr.MeasureString(Indice, FontIndice).Width - kAdjust * MyGr.MeasureString(" ", FontNormal).Width + MyGr.MeasureString("=", FontNormal).Width
            Else
                Return MyGr.MeasureString(Symbol, FontSymbol).Width + MyGr.MeasureString(Indice, FontIndice).Width - kAdjust * MyGr.MeasureString(" ", FontNormal).Width
            End If
        Else
            If DrawEgal Then
                Return MyGr.MeasureString(Symbol, FontNormal).Width + MyGr.MeasureString(Indice, FontIndice).Width - kAdjust * MyGr.MeasureString(" ", FontNormal).Width + MyGr.MeasureString("=", FontNormal).Width
            Else
                Return MyGr.MeasureString(Symbol, FontNormal).Width + MyGr.MeasureString(Indice, FontIndice).Width - kAdjust * MyGr.MeasureString(" ", FontNormal).Width
            End If
        End If

    End Function

#End Region

#Region " Preparation des objets "

    ''' <summary>
    ''' Remplissabge d'un comboBox avec une table de valeurs
    ''' </summary>
    ''' <param name="MyCombo">      [E/S] Combo box à remplir           </param>
    ''' <param name="tabValeurs">   [E]   Table de valeurs              </param>
    ''' <param name="TypeVariable"> [E]   Type de variable des valeurs  </param>
    Public Sub RemplirComboAvecValeurs(ByRef MyCombo As ComboBox, tabValeurs() As Decimal, TypeVariable As Enu_TypeVariable)
        MyCombo.Items.Clear()
        For i As Integer = 0 To tabValeurs.GetUpperBound(0)
            MyCombo.Items.Add(GetStringNoUnit(tabValeurs(i), TypeVariable))
        Next
    End Sub

    Public Sub RemplirComboAvecTableString(ByRef MyCombo As ComboBox, tabValeurs() As String)
        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)
    End Sub

#End Region

End Module
