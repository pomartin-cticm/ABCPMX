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
            LogicielFichiers.LangueNDC = LogicielRep.Install & "\Langues\" & LogicielInfo.NomLogiciel & "_" &
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

    ''' <summary>
    ''' Affichage d'un ErrorProvider en fonction d'une erreur
    ''' </summary>
    ''' <param name="iSaisie"></param>
    ''' <param name="Control"></param>
    ''' <param name="MyErr"></param>
    ''' <param name="ValMin"></param>
    ''' <param name="ValMax"></param>
    ''' <param name="TextError"></param>
    Public Sub NotifieErreurSaisie(ByVal iSaisie As Integer, ByVal Control As Control, ByVal MyErr As ErrorProvider, ByVal ValMin As Double, ByVal ValMax As Double, Optional ByVal TextError As String = "")

        Select Case iSaisie
            Case -1
                'Erreur saisie : Textbox vide------------------------------------
                MyErr.SetError(Control, ErreurNonNul_LNG)
            Case -2
                'Erreur saisie : valeur non numérique----------------------------
                MyErr.SetError(Control, ErreurNonNum_LNG)
            Case -3
                'Erreur saisie : valeur hors limite------------------------------
                Dim Chaine As String = ErreurHorsBornes_LNG + " : " & ValMin & " ≤ x ≤ " & ValMax
                MyErr.SetError(Control, Chaine)
            Case -4
                'Erreur : Divers ------------------------------------------------
                MyErr.SetError(Control, TextError)
        End Select

    End Sub

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

    Public Function GetFormatSignificatifN(ByVal Valeur As Double,
                                           ByVal nbSignificatif As Integer,
                                           ByVal nbDigitMax As Integer) As String
        '---------------------------------------------------------------------------------------------------
        '   11/08/23 :  Création - POM
        '---------------------------------------------------------------------------------------------------
        '   Fournit le format d'affichage pour une valeur avec un nb prédéfini de chiffres signicatifs
        '---------------------------------------------------------------------------------------------------
        '   Valeur          [E] :   Valeur à afficher
        '   nbSignificatif  [E] :   Nombre de chiffres significatifs attendus
        '   nbDigitMax      [E] :   Nombre maxi de chiffres après le séparateur décimal (si -1, pas de limite)
        '---------------------------------------------------------------------------------------------------

        '--> Déclarations

        Dim EpsilonV As Decimal = 0.0000000001#
        Dim MyFormat As String = "0"
        Dim Pui As Decimal
        Dim PuiE As Decimal
        Dim nbUnit As Integer   ' Nb de chiffres dans la partie entière
        Dim nbZero As Integer   ' Nb de chiffres à mettre après de le séparateur décimal

        '--> Traitement

        Try
            If Math.Abs(Valeur) - EpsilonV < 0 Then
                MyFormat = "0"
            Else

                Pui = (Math.Log10(Math.Abs(Valeur)))

                If Pui > 0 Then
                    '# Cas d'une valeur > 1
                    PuiE = Math.Floor(Pui)
                    If (PuiE = Pui) Then
                        nbUnit = PuiE
                    Else
                        nbUnit = PuiE + 1
                    End If
                    nbZero = Math.Max(0, nbSignificatif - nbUnit)
                Else
                    '# Cas d'une valeur < 1
                    PuiE = Math.Floor(-Pui)
                    If (PuiE = -Pui) Then
                        nbZero = PuiE - 1
                    Else
                        nbZero = PuiE
                    End If

                    nbZero += +nbSignificatif

                End If

                '# on écrete le nb de chiffres après la virgule en fct des paramètres d'appel
                If nbDigitMax > -1 Then

                    nbZero = Math.Min(nbZero, nbDigitMax)

                End If

                '# Préparation du format d'affichage
                If nbZero > 0 Then
                    MyFormat = "0."
                    For i As Integer = 1 To nbZero
                        MyFormat &= "0"
                    Next
                End If
            End If

        Catch ex As Exception
            MsgBox("Erreur du traitement", MsgBoxStyle.Critical, "Mod_Outils/GetFormatSignificatifN")
        End Try

        Return MyFormat

    End Function



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
    ''' <param name="lAbsolu">Indique si la valeur doit être affiché comme une valeur absolue</param>
    ''' <returns></returns>
    Public Function GetStringInUnit(ByVal Valeur As Decimal, ByVal Type As Enu_TypeVariable,
                                    ByVal nbSign As Integer, ByVal nbDigitMax As Integer, ByVal lUnite As Boolean, Optional ByVal lAbsolu As Boolean = False) As String

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

                kUnitU = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
                Unite = SEP & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

            Case Enu_TypeVariable.LongueurCM

                kUnitU = LogicielInfo.Transfert_Longueur(1)
                Unite = SEP & LogicielInfo.Unit_Longueur(1)

            Case Enu_TypeVariable.Dimension

                kUnitU = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
                Unite = SEP & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

            Case Enu_TypeVariable.Effort

                kUnitU = LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort)
                Unite = SEP & LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort)

            Case Enu_TypeVariable.Frequence
                kUnitU = 1
                Unite = "Hz"

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

            Case Enu_TypeVariable.InertieWCM6

                kUnitU = LogicielInfo.Transfert_Longueur(1) ^ 6
                Unite = SEP & LogicielInfo.Unit_Longueur(1) & "\+6\="

            Case Enu_TypeVariable.Inertie

                kUnitU = LogicielInfo.Transfert_Inerties(LogicielOptions.IndUnitInerties)
                Unite = SEP & LogicielInfo.Unit_Inerties(LogicielOptions.IndUnitInerties)

            Case Enu_TypeVariable.ModuleCM3

                kUnitU = LogicielInfo.Transfert_Longueur(1) ^ 3
                Unite = SEP & LogicielInfo.Unit_Longueur(1) & "\+3\="

            Case Enu_TypeVariable.AireCM2

                kUnitU = LogicielInfo.Transfert_Longueur(1) ^ 2
                Unite = SEP & LogicielInfo.Unit_Longueur(1) & "\+2\="

            Case Enu_TypeVariable.Millimetre

                kUnitU = LogicielInfo.Transfert_Longueur(0)
                Unite = SEP & LogicielInfo.Unit_Longueur(0)



        End Select

        ValeurU = Valeur / kUnitU
        'MyFormat = GetFormatSignificatif(ValeurU, nbSign, nbDigitMax)
        MyFormat = GetFormatSignificatifN(ValeurU, nbSign, nbDigitMax)

        If ValeurU >= 0 Then lAbsolu = False '--> Affichage VA seulement si valeur négative

        If lUnite Then
            If lAbsolu Then
                Return "|" & Format(ValeurU, MyFormat) & "|" & Unite
            Else
                Return Format(ValeurU, MyFormat) & Unite
            End If
        Else
            If lAbsolu Then
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
        Dim NbDigitMax As Integer = 2

        Select Case Type
            Case Enu_TypeVariable.Longueur
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)
                'Fmt = LogicielInfo.Format_Longueur(LogicielOptions.IndUnitLongueur)
                NbDigitMax = LogicielInfo.NbDigitMax_Longueur(LogicielOptions.IndUnitLongueur)

            Case Enu_TypeVariable.Dimension
                kUnit = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)
                'Fmt = LogicielInfo.Format_Longueur(LogicielOptions.IndUnitDimension)
                NbDigitMax = LogicielInfo.NbDigitMax_Longueur(LogicielOptions.IndUnitDimension)



            'Case Enu_TypeVariable.Millimetres
            '    Chaine = Format(Valeur / kUnitLongueur(indUnitMILLIMETRE), fmtUnitLongueur(indUnitMILLIMETRE))
            Case Enu_TypeVariable.Effort
                kUnit = LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort)
                'Fmt = LogicielInfo.Format_Effort(LogicielOptions.IndUnitEffort)
                NbDigitMax = LogicielInfo.NbDigitMax_Effort(LogicielOptions.IndUnitEffort)

            Case Enu_TypeVariable.Inertie
                kUnit = LogicielInfo.Transfert_Inerties(LogicielOptions.IndUnitInerties)
                'Fmt = LogicielInfo.Format_Inerties(LogicielOptions.IndUnitInerties)
                NbDigitMax = LogicielInfo.NbDigitMax_Inerties(LogicielOptions.IndUnitInerties)

            Case Enu_TypeVariable.SansType
                kUnit = 1
                Fmt = "0.00"
                NbDigitMax = 3

            Case Enu_TypeVariable.Moment
                kUnit = LogicielInfo.Transfert_Moment(LogicielOptions.IndUnitMoment)
                Fmt = LogicielInfo.Format_Moment(LogicielOptions.IndUnitMoment)
                NbDigitMax = LogicielInfo.NbDigitMax_Moment(LogicielOptions.IndUnitMoment)

            Case Enu_TypeVariable.ModuleY
                kUnit = LogicielInfo.Transfert_ModulesY(LogicielOptions.IndUnitModulesY)
                Fmt = LogicielInfo.Format_ModulesY(LogicielOptions.IndUnitModulesY)
                NbDigitMax = LogicielInfo.NbDigitMax_ModulesY(LogicielOptions.IndUnitModulesY)

            Case Enu_TypeVariable.Contrainte
                kUnit = LogicielInfo.Transfert_Contraintes(LogicielOptions.IndUnitContraintes)
                Fmt = LogicielInfo.Format_Contraintes(LogicielOptions.IndUnitContraintes)
                NbDigitMax = LogicielInfo.NbDigitMax_Contraintes(LogicielOptions.IndUnitContraintes)

        End Select

        Fmt = GetFormatSignif(Valeur / kUnit, NbDigitMax)
        Chaine = Format(Valeur / kUnit, Fmt)

        Return Chaine

    End Function

    Private Function GetFormatSignif(Valeur As Decimal, NbDigit As Integer) As String
        '-----------------------------------------------------------------------------------------
        '   05/06/23:   Création - POM
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------
        '-----------------------------------------------------------------------------------------

        '--> Déclaration

        Dim ValAbs As Decimal = Math.Abs(Valeur)
        Dim pDec As Decimal = ValAbs - Math.Floor(ValAbs)
        Dim Compteur As Integer = 0

        Dim Fmt As String = "0."
        Dim lCont As Boolean = (pDec > 0) And (NbDigit > Compteur)

        '--> Traitement

        Do While lCont
            Compteur += 1
            Fmt += "0"
            pDec = pDec * 10 - Math.Floor(pDec * 10)
            lCont = (pDec > 0) And (NbDigit > Compteur)
        Loop

        Return Fmt

    End Function

    ''' <summary>
    ''' Prend en argument un angle donné en radian, renvoi un argument un angle donné en degré 
    ''' </summary>
    ''' <param name="angleRadian">angle donné en radian (donnée d'entrée)</param>
    ''' <returns></returns>
    Function GetAngleInDegree(angleRadian As Decimal) As Decimal
        Dim angleDegre As Decimal
        angleDegre = 180 / Math.PI * angleRadian
        Return angleDegre
    End Function

    ''' <summary>
    ''' Prend en argument un angle donné en degré, renvoi un argument un angle donné en radian 
    ''' </summary>
    ''' <param name="angleDegre">angle donné en degré (donnée d'entrée)</param>
    ''' <returns></returns>
    Function GetAngleInRad(angleDegre As Decimal) As Decimal
        Dim angleRad As Decimal
        angleRad = Math.PI / 180 * angleDegre
        Return angleRad
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
            Dim FichierUpdate As String = LogicielRep.Config & "\Update_PropMix.txt"

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
                          ByVal lGrec As Boolean, lItalic As Boolean, ByVal Alignement As Enu_AlignementH,
                          ByVal FontNormal As Font, ByVal FontSymbol As Font,
                          ByVal FontIndice As Font, ByVal kAdjust As Single, ByVal DrawEgal As Boolean)
        '----------------------------------------------------------------------------------------
        '   21/02/08 :  Création - Version 1.00
        '----------------------------------------------------------------------------------------
        '   Fonction qui retourne la longueur d'un symbole (+indice) d'équation
        '----------------------------------------------------------------------------------------
        '   MyGr        [E] :   Graphics dans lequel on dessine
        '   Symbol      [E] :   Symbole à dessiner
        '   Indice      [E] :   Indice du Symbole
        '   lGrec       [E] :   Indice si symbole de l'alphabet grec
        '   FontNormal  [E] :   Police de caractère normale
        '   FontSymbol  [E] :   Police de caractère pour les symboles grecs
        '   FontIndice  [E] :   Police de caractère pour les indices
        '   kAdjust     [E] :   Coef d'ajustement de la position de l'indice
        '   DrawEgal    [E] :   Indique si affichage du symbole égal
        '----------------------------------------------------------------------------------------

        '--> Déclarations

        Dim LongueurString As Single = LongueurChaine(MyGr, Symbol, Indice, lGrec, FontNormal, FontSymbol, FontIndice, kAdjust, DrawEgal)
        Dim HauteurString As Single = MyGr.MeasureString("X", FontNormal).Height

        Dim xStar As Single

        '--> Positionnement

        Select Case Alignement
            Case Enu_AlignementH.Gauche                 ' Ancrage à gauche : la position correspond à la gauche du texte
                xStar = xPen
            Case Enu_AlignementH.Droite                 ' Ancrage à droite : la position correspond à la droite du texte
                xStar = xPen - LongueurString
            Case Enu_AlignementH.Centre
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

    Public Sub DrawSymbolN(ByVal MyGr As Graphics, ByVal BrushE As Brush,
                           ByVal Symbol As String, ByVal Indice As String,
                           ByVal sWI As Single, ByVal sHI As Single,
                           ByVal lGrec As Boolean, lItalic As Boolean, ByVal Alignement As Enu_AlignementH,
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
        '   BrushE      [E] :   
        '   Symbol      [E] :   Symbole à dessiner
        '   Indice      [E] :   Indice du Symbole
        '   sWI         [E] :   Largeur de l'image
        '   sHI         [E] :   Hauteur de l'image
        '   lGrec       [E] :   Indique si symbole de l'alphabet grec
        '   FontNormal  [E] :   Police de caractère normale
        '   FontSymbol  [E] :   Police de caractère pour les symboles grecs
        '   FontIndice  [E] :   Police de caractère pour les indices
        '
        '----------------------------------------------------------------------------------------

        '--> Déclarations

        Dim LongueurString As Single = LongueurChaine(MyGr, Symbol, Indice, lGrec, FontNormal, FontSymbol, FontIndice, kAdjust, DrawEgal)
        Dim HauteurString As Single = MyGr.MeasureString("X", FontNormal).Height

        Dim xStar As Single

        Dim MARGE As Single = 0.25 * sHI
        Dim yPen As Single

        '--> Positionnement

        Select Case Alignement
            Case Enu_AlignementH.Gauche
                xStar = MARGE
            Case Enu_AlignementH.Droite
                xStar = sWI - MARGE - LongueurString
            Case Enu_AlignementH.Centre
                xStar = sWI / 2 - LongueurString / 2
        End Select
        yPen = sHI / 2 - HauteurString / 2

        '--> Préparation de la police

        Dim MyFontNormal As Font
        If lItalic Then
            MyFontNormal = New Font(FontNormal, FontStyle.Italic)
        Else
            MyFontNormal = FontNormal
        End If

        '--> Ecriture Symbole

        If lGrec Then
            MyGr.DrawString(Symbol, FontSymbol, BrushE, xStar, yPen)
            xStar += MyGr.MeasureString(Symbol, FontSymbol).Width - kAdjust * MyGr.MeasureString(" ", FontSymbol).Width
        Else
            MyGr.DrawString(Symbol, MyFontNormal, BrushE, xStar, yPen)
            xStar += MyGr.MeasureString(Symbol, MyFontNormal).Width - kAdjust * MyGr.MeasureString(" ", MyFontNormal).Width
        End If

        '--> Ecriture Indice

        Dim DecalIndice As Single = HauteurString / 3

        MyGr.DrawString(Indice, FontIndice, BrushE, xStar, yPen + DecalIndice)

        If DrawEgal Then
            xStar += MyGr.MeasureString(Indice, FontIndice).Width ' - kAdjust * MyGr.MeasureString(" ", FontNormal).Width

            MyGr.DrawString("=", FontNormal, BrushE, xStar, yPen)
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

    Public Sub PrepareTextBoxDipo(ByRef MyTxt As TextBox, lDispo As Boolean)
        '------------------------------------------------------------------------------------
        '   10/08/23 :  Création - POM
        '------------------------------------------------------------------------------------
        '   Préparation de l'état d'un textbox en fonction du mode expert (bloqué en mode normal)
        '------------------------------------------------------------------------------------
        '   MyTxt       [E] :   Textbox à préparer
        '   lDispo      [E] :   Indicateur si textbox disponible
        '------------------------------------------------------------------------------------

        If lDispo Then
            MyTxt.ReadOnly = True
            MyTxt.BackColor = SystemColors.Window
        Else
            MyTxt.ReadOnly = False
            MyTxt.BackColor = CouleurReadOnly
        End If

    End Sub

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

#Region "   Manipulation de chaines de caracteres "
    Public Function RemplaceDollar(ByVal Chaine As String, ByVal Argument1 As String) As String
        Dim NewChaine As String = ""

        Dim jStr As Integer
        jStr = InStr(Chaine, "$")

        If jStr > 0 Then
            NewChaine = Chaine.Substring(0, jStr - 1) & Argument1 & Chaine.Substring(jStr)
            Return NewChaine
        Else
            Return Chaine
        End If

    End Function

    Sub CompleteChaine(ByRef Chaine As String, ByVal Motif As String, ByVal Longueur As Integer)
        '
        '   Complete une chaine de caractères par un motif jusqu'à obtenir la longueur désirée
        '
        '---------------------------------------------------------------------------------------

        Dim nbMotif As Integer = Longueur - Chaine.Length

        For i As Integer = 1 To nbMotif
            Chaine = Chaine & Motif.Substring(0, 1)
        Next

    End Sub

    Sub PositionneDansChaine(ByRef Chaine As String, ByVal iPos As Integer, ByVal APlacer As String)
        '
        '   13/09/07 :  Création - Version 1.00
        '
        '------------------------------------------------------------------------------------------
        '
        '   Rajoute la chaine APlacer dans la Chaine à la position indiquée
        '
        '------------------------------------------------------------------------------------------
        '
        '   Chaine      [E/S] : Chaine de caractères à completer
        '   iPos        [E] :   Position où est insérée la nouvelle chaine
        '   APlacer     [E] :   Caractères à placer dans la chaine
        '
        '------------------------------------------------------------------------------------------

        If Chaine.Length < iPos Then
            CompleteChaine(Chaine, " ", iPos)
        End If
        Chaine = Chaine & APlacer
    End Sub

    Function FrmReel(ByVal Valeur As Double, ByVal iDec As Integer) As String
        '----------------------------------------------------------------------------------------
        '
        '   Transforme un entier en chaine / la chaine utilise le point comme separateur decimal
        '
        '----------------------------------------------------------------------------------------
        '
        '   iDec    [E] :   Nombre de decimales
        '
        '----------------------------------------------------------------------------------------

        'Dim sb As New System.Text.StringBuilder
        'Dim Zero As Char = CChar("0")

        'Dim PartieE As Integer, PartieD As Single

        'If iDec = 0 Then
        '    PartieE = CInt(Math.Round(Valeur))
        'Else
        '    PartieE = CInt(Math.Floor(Valeur))
        'End If
        'sb.Append(PartieE.ToString)
        'If iDec > 0 Then
        '    Dim sf As New System.Text.StringBuilder
        '    sb.Append(".")
        '    PartieD = CSng(Math.Round((Valeur - PartieE) * Math.Pow(10, iDec)))
        '    sf.Append(Zero, iDec)
        '    sb.Append(Format(PartieD, sf.ToString))
        'End If

        'Return sb.ToString

        Return Math.Round(Valeur, iDec)
    End Function

#End Region

#Region " Transfert des valeurs avec suivi de modif "

    Public Sub GereTransfertValeur(ByVal ValeurLocale As Boolean, ByRef ValeurGlobale As Boolean, ByRef lModif As Boolean)
        If ValeurGlobale <> ValeurLocale Then lModif = True
        ValeurGlobale = ValeurLocale
    End Sub

    Public Sub GereTransfertValeur(ByVal ValeurLocale As Decimal, ByRef ValeurGlobale As Decimal, ByRef lModif As Boolean)
        If ValeurGlobale <> ValeurLocale Then lModif = True
        ValeurGlobale = ValeurLocale
    End Sub

    Public Sub GereTransfertValeur(ByVal ValeurLocale As Integer, ByRef ValeurGlobale As Integer, ByRef lModif As Boolean)
        If ValeurGlobale <> ValeurLocale Then lModif = True
        ValeurGlobale = ValeurLocale
    End Sub

    Public Sub GereTransfertValeur(ByVal ValeurLocale As String, ByRef ValeurGlobale As String, ByRef lModif As Boolean)
        If ValeurGlobale <> ValeurLocale Then lModif = True
        ValeurGlobale = ValeurLocale
    End Sub
#End Region

#Region "   Recherche des fichiers langues présents "

    Sub RechercheLangue(ByVal RepRec As String, ByVal Racine As String,
                        ByRef tabLangue As List(Of String), ByRef tabAbbrege As List(Of String))
        '------------------------------------------------------------------------------------------------
        '
        '   13/09/07 :  Création - Version 1.00
        '
        '------------------------------------------------------------------------------------------------
        '
        '   Recherche dans le répertoire d'installation des fichiers langue présents
        '
        '------------------------------------------------------------------------------------------------
        '
        '   RepRec      [E] :   Répertoire dans lequel sont recherchés les fichiers langue
        '   Racine      [E] :   Racine du nom de fichiers
        '                       On recherche les fichiers Racine_xx.LNG ou Racine_x.LNG
        '
        '   tabLangue   [S] :   table des langues présentes
        '   tabAbbrege  [S] :   table des langues présentes (abbrviations)
        '
        '------------------------------------------------------------------------------------------------


        tabAbbrege.Clear()
        tabLangue.Clear()

        Dim tabFiles As String()
        Dim RacineComplete As String = RepRec & "\" & Racine
        Dim IndPoint As Integer
        Dim RacFichier, Symb As String

        tabFiles = Directory.GetFiles(RepRec, "*.LNG")

        For i As Integer = 0 To tabFiles.GetUpperBound(0)

            IndPoint = tabFiles(i).IndexOf(".LNG")

            If tabFiles(i).Substring(IndPoint - 3, 1) = "_" Then
                Symb = tabFiles(i).Substring(IndPoint - 2, 2)
                RacFichier = tabFiles(i).Substring(0, IndPoint - 3)
            Else
                Symb = tabFiles(i).Substring(IndPoint - 1, 1)
                RacFichier = tabFiles(i).Substring(0, IndPoint - 2)
            End If

            If RacFichier.ToUpper = RacineComplete.ToUpper Then
                '--[ Si la racine correspond bien :
                tabAbbrege.Add(Symb)
                Select Case Symb
                    Case "EN"
                        tabLangue.Add("English")
                    Case "FR"
                        tabLangue.Add("Français")
                    Case "ES"
                        tabLangue.Add("Espanol")
                    Case "IT"
                        tabLangue.Add("Italiano")
                    Case "DE"
                        tabLangue.Add("Deutsch")
                    Case "PT"
                        tabLangue.Add("Português")
                End Select
            End If

        Next

    End Sub

#End Region

#Region " Outils de COMPARaison "

    Const DeltaVMAx As Decimal = 0.001
    Public Function IsEqual(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        If Math.Abs(b) <= EPS Then
            'AVEC DIMENSION
            Return Math.Abs(a) <= EPS
        Else
            'ATTENTION : Lorsqu'on compare la fraction (PAS DE DIMENSION), il faut utiliser 0.001
            Return Math.Abs(a / b - 1) <= 0.001
        End If
    End Function

    Public Function IsGreater(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        'NE PAS UTILISER POUR CHERCHER LA VALEUR MAX/MIN

        Return (Not IsEqual(a, b, EPS)) AndAlso (a > b)
    End Function

    Private Function IsGreaterOrEqual(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        Return IsEqual(a, b, EPS) OrElse (a > b)

    End Function

    Public Function IsSmaller(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean
        '------------------------------------------
        ' 29/08/2023 : Minh, v 1.00
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------
        Dim lIsSmaller As Boolean = (Not IsEqual(a, b, EPS)) AndAlso (a < b)

        'NE PAS UTILISER POUR CHERCHER LA VALEUR MAX/MIN

        Return lIsSmaller
    End Function

    Public Function IsSmallerOrEqual(ByVal a As Decimal, ByVal b As Decimal, Optional ByVal EPS As Decimal = DeltaVMAx) As Boolean

        '------------------------------------------
        ' 29/11/2013
        '------------------------------------------
        ' Comparer deux valeurs réelles
        '------------------------------------------

        Return IsEqual(a, b, EPS) OrElse (a < b)
    End Function

#End Region

#Region "   Fonctions de confirmation ou d'information "

    Public Function DemandeConfirmation(ByVal Message As String) As Boolean
        '
        '   Demande de confirmation auprès utilisateur
        '
        '---------------------------------------------------------------------------

        If MessageBox.Show(Message, LogicielInfo.NomLogiciel, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
            Return True
        Else
            Return False
        End If


    End Function

    Public Function DemandeConfirmationYesNoCancel(ByVal Message As String) As DialogResult

        Return MessageBox.Show(Message, LogicielInfo.NomLogiciel, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

    End Function

    Public Sub UserInformation(ByVal Message As String)
        MessageBox.Show(Message, LogicielInfo.NomLogiciel, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Public Sub UserWarning(ByVal Message As String)
        MessageBox.Show(Message, LogicielInfo.NomLogiciel, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

#End Region

#Region "   Controle des saisies dans les textbox "

    Function ValideSaisieTextBox(ByRef txtSaisie As TextBox,
                                 ByVal lValMin As Boolean, ByVal ValMin As Double,
                                 ByVal lValMax As Boolean, ByVal ValMax As Double,
                                 ByRef ValNum As Double) As Integer
        '------------------------------------------------------------------
        '
        '   04/03/07 :  Création - Version 1.00
        '   13/05/09 :  Modification - Vercion 1.04
        '
        '------------------------------------------------------------------
        '
        '   Controle de la valeur numérique saisie dans une textbox
        '   Version 1.04 : Prise en compte tolérance dans la limite des bornes
        '
        '------------------------------------------------------------------
        '
        '   txtSaisie   : [E]   TextBox à vérifier
        '   lValMin     : [E]   Indique si controle borne inférieure
        '   ValMin      : [E]   Borne Inférieure Autorisée
        '   lValMax     : [E]   Indique si controle borne supérieure
        '   ValMax      : [E]   Borne Supérieure Autorisée
        '   
        '   ValNum      : [S]   Valeur numérique saisie, quand elle existe
        '
        '   Code retour :       0 saisie correcte
        '                       -1 champ vide
        '                       -2 chaine non numérique
        '                       -3 valeur inférieure borne inférieure
        '                       -4 valeur supérieure borne supérieure
        '
        '------------------------------------------------------------------

        Dim Chaine As String = TraiteReal(txtSaisie.Text)

        '--> Controle de champ vide

        If Chaine = "" Then Return -1

        '--> Controle de champ non numerique

        If Not IsNumeric(Chaine) Then Return -2

        '--> Controle de la valeur saisie

        '===Rajouter un test sur la longueur de la chaine

        ValNum = CDbl(Chaine)

        If lValMin And (ValNum < ValMin * (1 - DELTASAISIE)) Then Return -3

        If lValMax And (ValNum > ValMax * (1 + DELTASAISIE)) Then Return -4

        Return 0

    End Function

#End Region

#Region " Gestion des erreurs"

    Sub PrepareErreurTextBox(ByVal MyErrPo As ErrorProvider, ByVal MyTxtBox As TextBox, ByVal lGauche As Boolean)
        '
        '   03/08/07 :  Création - Version 1.00
        '
        '--------------------------------------------------------------------------------------------
        '
        '   Prépare l'Error Provider d'une Texte Box
        '
        '--------------------------------------------------------------------------------------------
        '
        '   lGauche     [E] :   Indique si on fait apparaitre le ErrorProvider à gauche ou à droite
        '
        '--------------------------------------------------------------------------------------------

        If lGauche Then
            MyErrPo.SetIconAlignment(MyTxtBox, ErrorIconAlignment.MiddleLeft)
        Else
            MyErrPo.SetIconAlignment(MyTxtBox, ErrorIconAlignment.MiddleRight)
        End If
        MyErrPo.SetIconPadding(MyTxtBox, 2)
        MyErrPo.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink

    End Sub

#End Region

End Module
