Imports PMXMoteur2
Imports System.IO

Imports System.Globalization
Imports System.Windows.Input
Imports System.Net

Public Class Frm_Update

#Region " Variables "
    Dim lBuild As Boolean
    Dim lDebug As Boolean = False

    Dim strDownLoadCompleted As String
    Dim strNoInfo As String

    Dim Bloc As New Dictionary(Of String, String)
    Dim AddressWeb As String

    Dim lNewPro, lNewSteel As Boolean
    Dim lTestDebug As Boolean = False

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Update_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        InitialiseDebug()
        GestionLangue()
        GestionStyle()

        InitialisationFenetre()

        lBuild = False
    End Sub

    <Conditional("DEBUG")> Private Sub InitialiseDebug()
        lDebug = True
    End Sub

    Private Sub GestionLangue()
        '
        '   Gestion de la langue pour la fenetre
        '
        '------------------------------------------------------------------------------------------------

        If File.Exists(LogicielFichiers.Langue) Then

            Dim strLoadedKey As String = ""
            Const CLE As String = ""

            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_UPDATE")
            BlocLine.CreationBloc(Bloc, strLoadedKey)

            Try

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("CLOSE")

                Me.lbl_Version.Text = RemplaceDollar(Bloc("GRPSOFTUPDATE"), LogicielInfo.Racine)
                Me.lbl_Fichier.Text = Bloc("GRPSECTIONS")

                Me.cmd_Telecharger.Text = Bloc("DOWNLOAD")

                Me.etq_Avertissement.Text = Bloc("RESTART")
                strDownLoadCompleted = Bloc("RESTART")
                Me.lkl_Update.Text = Bloc("OPENLNK")

                ' Me.chk_Debug.Text = "Test Import Bases DBG"

                strNoInfo = Bloc("NOINFO")


            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues", CLE, strLoadedKey)

            End Try

        End If

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_Version.BackColor = CouleurBackBandeaux
        Me.lbl_Version.ForeColor = CouleurForeBandeaux
        Me.lbl_Fichier.BackColor = CouleurBackBandeaux
        Me.lbl_Fichier.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub InitialisationFenetre()

        '--[ Général

        Me.etq_Avertissement.Visible = False

        CacheProgressBar()

        'Me.chk_Debug.Visible = lDebug

        '--[ Affichage

        Select Case iComWnd
            Case EnuFenetres.CheckUpDate
                '--> Quand on ouvre la fenetre en cliquant sur controle des mises à jour dans la fenetre configuration
                AfficherMisesAJourDisponibles()
            Case EnuFenetres.NouvelleVersion
                '--> Quand la fenetre est ouverte par le logiciel au démarrage, s'il trouve une version plus récente
                TraiterFichierMAJ()
        End Select

    End Sub


    Private Sub GestionUnites()

    End Sub

#End Region

#Region "===FERMETURE==="

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Me.Close()
    End Sub

#End Region

#Region " Gestion des recherches de mises à jour "

    Private Sub TraiterFichierMAJ()
        '---------------------------------------------------------------------------------
        '
        '   16/06/08 : Création - Version 1.00
        '
        '---------------------------------------------------------------------------------

        Dim BlocMAJ As New Dictionary(Of String, String)

        LireFichierUpDate(BlocMAJ)
        AfficherMAJ(BlocMAJ, True)

    End Sub

    Private Sub CacheProgressBar()
        Me.MyProgressB.Visible = False
        Application.DoEvents()
    End Sub

    Private Sub AfficherMisesAJourDisponibles()
        '---------------------------------------------------------------------------------
        '
        '   16/06/08 : Création - Version 1.00
        '
        '---------------------------------------------------------------------------------

        '--[ Déclaration

        Dim BlocMAJ As New Dictionary(Of String, String)
        Dim lAccesOK As Boolean

        '--[ Récupération du Fichier UpDate

        If lDebug Then
            RecupereDonneesMAJDebug(BlocMAJ, lAccesOK)
        Else
            RecupereDonneesMAJInternet(BlocMAJ, lAccesOK)
        End If

        '--[ Affichage

        AfficherMAJ(BlocMAJ, lAccesOK)

    End Sub

#End Region

#Region " Affichage des mises à jour "

    Private Sub AfficherMAJ(ByVal BlocMAJ As Dictionary(Of String, String), ByVal lAccesOK As Boolean)
        '---------------------------------------------------------------------------------
        '
        '   28/09/12 : Création - Version 3.00 - POM - Refonte
        '
        '---------------------------------------------------------------------------------
        '
        '   Affichage des informations de mise à jour
        '
        '---------------------------------------------------------------------------------
        '
        '   BlocMAJ     [E] :   Liste des instructions de mise à jour
        '   lAccesOK    [E] :   Indique si la lecture du fichier de mise à jour s'est bien déroulé
        '
        '---------------------------------------------------------------------------------

        Dim lFailed As Boolean = False

        If lAccesOK Then

            AfficherMAJVersionLogiciel(BlocMAJ)

            AfficherMAJDataBases(BlocMAJ)

        Else

            AfficherFail()

        End If

    End Sub

    Private Sub AfficherFail()
        '---------------------------------------------------------------------------------
        '   02/08/24 : Création - POM
        '---------------------------------------------------------------------------------
        '   Gère le problème d'accès aux infos de mise à jour
        '---------------------------------------------------------------------------------

        Me.etq_Message01.Text = strNoInfo
        Me.etq_Message02.Text = ""

        Me.etq_MessageSections.Text = strNoInfo
        Me.etq_MessageSteels.Text = ""

    End Sub

    Private Sub AfficherMAJDataBases(ByVal BlocMAJ As Dictionary(Of String, String))
        '---------------------------------------------------------------------------------
        '
        '   28/09/12 : Création - Version 3.00 - POM
        '
        '---------------------------------------------------------------------------------
        '
        '   Affichage des informations de mise à jour
        '   Concernant la version du logiciel
        '
        '---------------------------------------------------------------------------------
        '
        '   BlocMAJ     [E] :   Liste des instructions de mise à jour
        '
        '---------------------------------------------------------------------------------

        '--[ Déclaration 

        Dim VersionPro As String = "", iFPro As Short
        Dim InfoPro As StrucVersionDtB
        Dim VersionSteel As String = "", iFSteel As Short
        Dim InfoSteel As StrucVersionDtB

        Dim lOK As Boolean, lFailed As Boolean

        Dim KeyPro As String = "BASEPRO"
        Dim KeySte As String = "BASESTE"

        '--[ Initialisation

        lFailed = False
        If lTestDebug Then
            KeyPro = "DEBGPRO"
            KeySte = "DEBGSTE"
        End If

        '--[ Récupération des informations pour la base de profilés

        'RecupereInfosAJourDataBase(BlocMAJ, "BASEPRO", VersionPro, iFPro, lOK)
        RecupereInfosAJourDataBase(BlocMAJ, KeyPro, VersionPro, iFPro, lOK)
        If Not lOK Then
            lFailed = True
        Else
            AnalyseInfoVersion(VersionPro, iFPro, InfoPro)
            lNewPro = ExisteVersionPlusRecenteMemeFormat(OptionsDatabase.VersionBaseProfiles, InfoPro)
        End If

        '--[ Récupération des informations pour la base des aciers

        'RecupereInfosAJourDataBase(BlocMAJ, "BASESTE", VersionSteel, iFSteel, lOK)
        RecupereInfosAJourDataBase(BlocMAJ, KeySte, VersionSteel, iFSteel, lOK)
        If Not lOK Then
            lFailed = True
        Else
            AnalyseInfoVersion(VersionSteel, iFSteel, InfoSteel)
            lNewSteel = ExisteVersionPlusRecenteMemeFormat(OptionsDatabase.VersionBaseAciers, InfoSteel)
        End If

        If Not lFailed Then

            Dim lAJour As Boolean
            lAJour = (Not lNewPro) And (Not lNewSteel)

            Me.etq_MessageSteels.Visible = True

            Me.cmd_Telecharger.Enabled = Not lAJour
            'Me.cmd_Telecharger.Enabled = True
            'MsgBox("A CORRIGER")

            If lNewPro Then
                Me.etq_MessageSections.Text = Bloc("NEWBASE") & " (V " & InfoPro.Year.ToString & "_" & InfoPro.Indice.ToString & ")"
            Else
                Me.etq_MessageSections.Text = Bloc("BASEUPTODATE") & " (V " & OptionsDatabase.VersionBaseProfiles.Year.ToString & "_" & OptionsDatabase.VersionBaseProfiles.Indice.ToString & ")"
            End If
            If lNewSteel Then
                Me.etq_MessageSteels.Text = Bloc("NEWBASESTEEL") & " (V " & InfoSteel.Year.ToString & "_" & InfoSteel.Indice.ToString & ")"
            Else
                Me.etq_MessageSteels.Text = Bloc("BASESTEELUPTOD") & " (V " & OptionsDatabase.VersionBaseAciers.Year.ToString & "_" & OptionsDatabase.VersionBaseAciers.Indice.ToString & ")"
            End If

        Else
            Me.cmd_Telecharger.Enabled = False
            Me.etq_MessageSections.Text = Bloc("SECTIONFAILED")
            Me.etq_MessageSteels.Visible = False
        End If

    End Sub

    Private Sub AfficherMAJVersionLogiciel(ByVal BlocMAJ As Dictionary(Of String, String))
        '---------------------------------------------------------------------------------
        '
        '   28/09/12 : Création - Version 3.00 - POM - Refonte
        '
        '---------------------------------------------------------------------------------
        '
        '   Affichage des informations de mise à jour
        '   Concernant la version du logiciel
        '
        '---------------------------------------------------------------------------------
        '
        '   BlocMAJ     [E] :   Liste des instructions de mise à jour
        '
        '---------------------------------------------------------------------------------

        Dim lFailed As Boolean = False

        Dim lOK As Boolean
        Dim lAJour As Boolean

        If BlocMAJ.ContainsKey("VERSION") Then

            Dim NewVersion As Single = CSng(TraiteReal(BlocMAJ("VERSION")))
            'Dim VersionEnCours As Single = VersionACB.Principal + VersionACB.Secondaire / 10.0! + VersionACB.Revision / 100.0!
            Dim VersionEnCours As Single

            '== V2.01 ========
            VersionEnCours = ABCPMXIndiceVersion()
            '=================

            ' '' ''NewVersion = 4.0
            ' '' ''MsgBox("Corriger - AfficherMAJVersionLogiciel")

            '==R17-010
            lAJour = (VersionEnCours >= NewVersion - 0.0001)

            If lAJour Then

                Me.etq_Message01.Text = RemplaceDollar(Bloc("UPTODATE"), LogicielInfo.Version.Label)
                Me.etq_Message02.Text = ""
                Me.lkl_Update.Visible = False

            Else
                Dim Balise01, Balise02, BaliseLNK As String

                Dim ExtLangue As String = LogicielInfo.ListeLangue(LogicielOptions.IndLangue).Substring(0, 2).ToUpper

                '--| Message no 1

                Balise01 = "MSG01" & ExtLangue

                If BlocMAJ.ContainsKey(Balise01) Then
                    lOK = True
                Else
                    Balise01 = "MSG01EN"
                    lOK = BlocMAJ.ContainsKey(Balise01)
                End If

                '--| Message no 2

                Balise02 = "MSG02" & ExtLangue

                If BlocMAJ.ContainsKey(Balise02) Then
                    lOK = True
                Else
                    Balise02 = "MSG02EN"
                    lOK = BlocMAJ.ContainsKey(Balise02)
                End If

                '--| Lien internet

                BaliseLNK = "ADDRESS"

                lOK = BlocMAJ.ContainsKey(BaliseLNK)

                '--| Affichage

                If lOK Then
                    Me.etq_Message01.Text = BlocMAJ(Balise01)
                    Me.etq_Message02.Text = BlocMAJ(Balise02)
                    'Me.lkl_Update.Text = BlocMAJ(BaliseLNK)
                    AddressWeb = BlocMAJ(BaliseLNK)
                    Me.lkl_Update.Visible = True
                Else
                    lFailed = True
                End If
            End If
        Else
            lFailed = True
        End If

        If lFailed Then
            Me.etq_Message01.Text = Bloc("WEBACCESFAILED1")
            Me.etq_Message02.Text = Bloc("WEBACCESFAILED2")
            Me.lkl_Update.Visible = False
        End If

    End Sub

#End Region

End Class