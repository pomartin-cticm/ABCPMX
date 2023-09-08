Imports PMXMoteur2
Imports System.IO

Public Class Frm_Chargement

#Region " Variables locales "

    Dim lBuild As Boolean = True

    ''' <summary>
    ''' Définition d'une poutre_loc afin d'enregistrer les actions de l'utilisateur
    ''' </summary>
    Dim MyPoutreLoc As New cls_Poutre

    Dim NbTravees As Integer
    Dim NbChargeLineique, NbChargePonctuelle As Integer

    Dim traveeEnCours As Integer
    Dim chargeEnCours As String

    Dim tableau_txtbox_ChargesLineiques(,) As TextBox
    Dim tableau_txtbox_ChargesPonctuelles(,) As TextBox

#End Region

#Region "===OUVERTURE==="


    Private Sub Frm_Chargement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub InitialiserFenetre()
        InitialiserVariables()
        GestionLangues()
        GestionStyle()
        GestionUnites()
        AfficherPoutreEnCours()
        lBuild = False
    End Sub

    Private Sub InitialiserVariables()
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)

        NbTravees = MyPoutreLoc.NbTravees

        'Par défaut on affiche la première travée sur deux appuis
        traveeEnCours = 1
        chargeEnCours = "G1"

        '-----

        ReDim tableau_txtbox_ChargesLineiques(3, 5)

        tableau_txtbox_ChargesLineiques(0, 0) = Me.txt_Indice_Lineique_1
        tableau_txtbox_ChargesLineiques(0, 1) = Me.txt_F1_Lineique_1
        tableau_txtbox_ChargesLineiques(0, 2) = Me.txt_x1_Lineique_1
        tableau_txtbox_ChargesLineiques(0, 3) = Me.txt_F2_Lineique_1
        tableau_txtbox_ChargesLineiques(0, 4) = Me.txt_x2_Lineique_1

        tableau_txtbox_ChargesLineiques(1, 0) = Me.txt_Indice_Lineique_2
        tableau_txtbox_ChargesLineiques(1, 1) = Me.txt_F1_Lineique_2
        tableau_txtbox_ChargesLineiques(1, 2) = Me.txt_x1_Lineique_2
        tableau_txtbox_ChargesLineiques(1, 3) = Me.txt_F2_Lineique_2
        tableau_txtbox_ChargesLineiques(1, 4) = Me.txt_x2_Lineique_2

        tableau_txtbox_ChargesLineiques(2, 0) = Me.txt_Indice_Lineique_3
        tableau_txtbox_ChargesLineiques(2, 1) = Me.txt_F1_Lineique_3
        tableau_txtbox_ChargesLineiques(2, 2) = Me.txt_x1_Lineique_3
        tableau_txtbox_ChargesLineiques(2, 3) = Me.txt_F2_Lineique_3
        tableau_txtbox_ChargesLineiques(2, 4) = Me.txt_x2_Lineique_3

        tableau_txtbox_ChargesLineiques(3, 0) = Me.txt_Indice_Lineique_4
        tableau_txtbox_ChargesLineiques(3, 1) = Me.txt_F1_Lineique_4
        tableau_txtbox_ChargesLineiques(3, 2) = Me.txt_x1_Lineique_4
        tableau_txtbox_ChargesLineiques(3, 3) = Me.txt_F2_Lineique_4
        tableau_txtbox_ChargesLineiques(3, 4) = Me.txt_x2_Lineique_4

        '-----

        ReDim tableau_txtbox_ChargesPonctuelles(7, 2)

        tableau_txtbox_ChargesPonctuelles(0, 0) = Me.txt_Indice_Ponctuelle_1
        tableau_txtbox_ChargesPonctuelles(0, 1) = Me.txt_x_Ponctuelle_1
        tableau_txtbox_ChargesPonctuelles(0, 2) = Me.txt_F_Ponctuelle_1

        tableau_txtbox_ChargesPonctuelles(1, 0) = Me.txt_Indice_Ponctuelle_2
        tableau_txtbox_ChargesPonctuelles(1, 1) = Me.txt_x_Ponctuelle_2
        tableau_txtbox_ChargesPonctuelles(1, 2) = Me.txt_F_Ponctuelle_2

        tableau_txtbox_ChargesPonctuelles(2, 0) = Me.txt_Indice_Ponctuelle_3
        tableau_txtbox_ChargesPonctuelles(2, 1) = Me.txt_x_Ponctuelle_3
        tableau_txtbox_ChargesPonctuelles(2, 2) = Me.txt_F_Ponctuelle_3

        tableau_txtbox_ChargesPonctuelles(3, 0) = Me.txt_Indice_Ponctuelle_4
        tableau_txtbox_ChargesPonctuelles(3, 1) = Me.txt_x_Ponctuelle_4
        tableau_txtbox_ChargesPonctuelles(3, 2) = Me.txt_F_Ponctuelle_4

        tableau_txtbox_ChargesPonctuelles(4, 0) = Me.txt_Indice_Ponctuelle_5
        tableau_txtbox_ChargesPonctuelles(4, 1) = Me.txt_x_Ponctuelle_5
        tableau_txtbox_ChargesPonctuelles(4, 2) = Me.txt_F_Ponctuelle_5

        tableau_txtbox_ChargesPonctuelles(5, 0) = Me.txt_Indice_Ponctuelle_6
        tableau_txtbox_ChargesPonctuelles(5, 1) = Me.txt_x_Ponctuelle_6
        tableau_txtbox_ChargesPonctuelles(5, 2) = Me.txt_F_Ponctuelle_6

        tableau_txtbox_ChargesPonctuelles(6, 0) = Me.txt_Indice_Ponctuelle_7
        tableau_txtbox_ChargesPonctuelles(6, 1) = Me.txt_x_Ponctuelle_7
        tableau_txtbox_ChargesPonctuelles(6, 2) = Me.txt_F_Ponctuelle_7

        tableau_txtbox_ChargesPonctuelles(7, 0) = Me.txt_Indice_Ponctuelle_8
        tableau_txtbox_ChargesPonctuelles(7, 1) = Me.txt_x_Ponctuelle_8
        tableau_txtbox_ChargesPonctuelles(7, 2) = Me.txt_F_Ponctuelle_8

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_CHARGEMENT")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== MENU PRINCIPAL ==============================================================='



            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon
    End Sub

    Private Sub AfficherPoutreEnCours()

    End Sub

#End Region

#Region "===FERMETURE==="

    Public Sub TraitementSaisie()

    End Sub

    Private Sub btn_OK_Click(sender As Object, e As EventArgs) Handles btn_OK.Click
        Dim lModif As Boolean = False
        If ValideSaisieFenetre() Then

            TransfertSaisie(lModif)

            If lModif Then

            End If
            Me.Close()
        End If
    End Sub


    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

    End Sub






#End Region

#Region " Dessins "



#End Region

#Region " Evènements "


#End Region

#Region " Evènements saisie "

    ''' <summary>
    ''' On enregistre le nom de la charge 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SelectionChargement(sender As Object, e As EventArgs) Handles rad_G1.CheckedChanged, rad_G2.CheckedChanged, rad_Q1.CheckedChanged, rad_Q2.CheckedChanged, rad_Qc.CheckedChanged
        Select Case sender.name
            Case rad_G1.Name
                chargeEnCours = "G1"
            Case rad_G2.Name
                chargeEnCours = "G2"
            Case rad_Q1.Name
                chargeEnCours = "Q1"
            Case rad_Q2.Name
                chargeEnCours = "Q2"
            Case rad_Qc.Name
                chargeEnCours = "QC"
        End Select

    End Sub

    Private Sub AffichageChargementSelectionne()
        'Rempli la fenetre avec les valeurs 

        NbChargeLineique = MyPoutreLoc.ChargesU(chargeEnCours).FReparties.Length
        NbChargePonctuelle = MyPoutreLoc.ChargesU(chargeEnCours).Forces.Length

        'MAJ affichage des tableau 

        For i As Integer = 0 To NbChargeLineique - 1
            For j As Integer = 0 To 4
                tableau_txtbox_ChargesLineiques(i, j).Enabled = True
                tableau_txtbox_ChargesLineiques(i, 0).Text = i
                tableau_txtbox_ChargesLineiques(i, 1).Text = MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).xPosG(0)
                tableau_txtbox_ChargesLineiques(i, 2).Text = MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).Force(0)
                tableau_txtbox_ChargesLineiques(i, 3).Text = MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).xPosG(1)
                tableau_txtbox_ChargesLineiques(i, 4).Text = MyPoutreLoc.ChargesU(chargeEnCours).FReparties(traveeEnCours)(i).Force(1)
            Next
        Next

        For i As Integer = NbChargeLineique To 3
            For j As Integer = 0 To 4
                tableau_txtbox_ChargesLineiques(i, j).Enabled = False
                tableau_txtbox_ChargesLineiques(i, j).Text = ""
            Next
        Next

    End Sub

#End Region

End Class