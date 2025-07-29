Imports System.IO
Imports PMXMoteur2

Public Class Frm_ConnectionSlimN

#Region " Variables "

    Dim lBuild As Boolean

    Enum Enu_AffParamSlim
        Connecteur
        Connexion
    End Enum

    Dim AffParam As Enu_AffParamSlim = Enu_AffParamSlim.Connecteur

    Dim Bloc As New Dictionary(Of String, String)

    ''' <summary>
    ''' Définition d'une poutre_loc afin d'enregistrer les actions de l'utilisateur
    ''' </summary>
    Public localBeam As New cls_Poutre(NomChargements)

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_ConnectionSlimN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True
        GestionStyle()
        GestionLangues()
        InitialiseVariables()
        PrepareFenetre()
        lBuild = False
    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

    End Sub

    Private Sub GestionLangues()

        If File.Exists(LogicielFichiers.Langue) Then
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_CONNECTIONSLIM")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== GENERAL ======================================================================

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                Me.rdb_Connecteurs.Text = Bloc("CONNECTORS")
                Me.rdb_Connexion.Text = Bloc("CONNECTION")

            Catch ex As Exception
                GestionErreurAffichageLangue(Me.Name, "GestionLangues")
                'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                'Bloc.Clear()
            End Try

        Else

            GestionFichierLangueAbsent(Me.Name, "GestionLangues")

        End If
    End Sub

    Private Sub InitialiseVariables()
        AffParam = Enu_AffParamSlim.Connecteur

        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), localBeam)

    End Sub

    Private Sub PrepareFenetre()

        AffichageFille()

        Select Case AffParam
            Case Enu_AffParamSlim.Connecteur
                Me.rdb_Connecteurs.Checked = True
            Case Enu_AffParamSlim.Connexion
                Me.rdb_Connexion.Checked = True
        End Select

    End Sub

#End Region

#Region " Events gestion des boutons "

    Private Sub rdb_Connecteurs_CheckedChanged(sender As Object, e As EventArgs) Handles rdb_Connecteurs.CheckedChanged

        If lBuild Then Exit Sub

        Select Case True
            Case Me.rdb_Connecteurs.Checked
                AffParam = Enu_AffParamSlim.Connecteur
            Case Me.rdb_Connexion.Checked
                AffParam = Enu_AffParamSlim.Connexion
        End Select

        AffichageFille()

    End Sub

    Private Sub AffichageFille()

        Me.pan_ContenuFille.Controls.Clear()

        Select Case AffParam
            Case Enu_AffParamSlim.Connecteur
                Me.pan_ContenuFille.Controls.Add(Frm_ConnectionSlimConnecteur.pan_Main)
                Frm_ConnectionSlimConnecteur.InitialiseFenetre(Bloc)
            Case Enu_AffParamSlim.Connexion
                Me.pan_ContenuFille.Controls.Add(Frm_ConnectionSlimConnexion.pan_Main)
                Frm_ConnectionSlimConnexion.InitialiseFenetre(Bloc)
        End Select

    End Sub

#End Region

#Region "===FERMETURE==="

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Dim lModif As Boolean = False

        If ValideSaisie Then

            TransfertSaisie(lModif)

            If lModif Then
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If
            Me.Close()

        End If

    End Sub

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        With MyProjet.Poutres(MyProjet.IndEnCours)

            If .Dalle.typeConnecteur <> localBeam.Dalle.typeConnecteur Then lModif = True
            .Dalle.typeConnecteur = localBeam.Dalle.typeConnecteur

            GereTransfertValeur(localBeam.Dalle.Goujons.nom, .Dalle.Goujons.nom, lModif)
            GereTransfertValeur(localBeam.Dalle.Goujons.hsc, .Dalle.Goujons.hsc, lModif)
            GereTransfertValeur(localBeam.Dalle.Goujons.d, .Dalle.Goujons.d, lModif)
            GereTransfertValeur(localBeam.Dalle.Goujons.Fy, .Dalle.Goujons.Fy, lModif)
            GereTransfertValeur(localBeam.Dalle.Goujons.Fu, .Dalle.Goujons.Fu, lModif)

        End With


    End Sub


    Private Function ValideSaisie() As Boolean

        Return True

    End Function

#End Region

End Class