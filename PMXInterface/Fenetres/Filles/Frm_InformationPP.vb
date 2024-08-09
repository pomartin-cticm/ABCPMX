Imports PMXMoteur2
Imports System.IO

Public Class Frm_InformationPP

#Region " Variables locales "

    Dim lBuild As Boolean = True

    Const formatTxtBox As String = "0.00"
    Const formatTxtBox2 As String = "0."

    ''' <summary>
    ''' Définition d'une poutre_loc afin d'enregistrer les actions de l'utilisateur
    ''' </summary>
    Dim MyPoutreLoc As New cls_Poutre(NomChargements)

    Dim PoidsPropreLoc As cls_Poutre.StructPoidsPropres

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_InformationPP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
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

        MyPoutreLoc = New cls_Poutre(NomChargements)
        cls_Poutre.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours), MyPoutreLoc)
        PoidsPropreLoc = MyPoutreLoc.ChargeRepartiePP()

    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_INFORMATIONPP")
            BlocLine.CreationBloc(Bloc)

            Try

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")

                Me.lbl_ParametresGeneraux.Text = Bloc("MAINPARAMETERS")
                Me.lbl_Projet.Text = Bloc("PROJECT")
                Me.lbl_BeamID.Text = Bloc("BEAMID")
                Me.lbl_Gravite.Text = Bloc("GRAVITY")

                Me.lbl_PP_Profile.Text = Bloc("DEADLOAD_PROFILE")
                Me.lbl_ProfileAcier.Text = Bloc("STEELPROFILE")
                Me.lbl_BetonEnrobage.Text = Bloc("PARTIALENCASEMENT")

                Me.lbl_PP_Dalle.Text = Bloc("DEADLOAD_SLAB")
                Me.lbl_DalleBeton.Text = Bloc("CONCRETESLAB")
                Me.lbl_BacAcier.Text = Bloc("PSHEETING")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()
        Me.etq_UnitGravite.Text = "m/s2"

        Me.etq_UnitRhoa.Text = "kg/m3"
        Me.etq_UnitAa.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & "2"
        Me.etq_UnitQa.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

        Me.etq_UnitRhoec.Text = "kg/m3"
        Me.etq_UnitAec.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & "2"
        Me.etq_UnitQec.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

        Me.etq_UnitRhoc.Text = "kg/m3"
        Me.etq_UnitAc.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur) & "2"
        Me.etq_UnitQc.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

        Me.etq_UnitMup.Text = "kg/m2"
        Me.etq_UnitDc.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
        Me.etq_UnitQp.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)

        Me.etq_UnitQTot.Text = LogicielInfo.Unit_Effort(LogicielOptions.IndUnitEffort) & "/" & LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitLongueur)
    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_ParametresGeneraux.ForeColor = CouleurForeBandeaux
        Me.lbl_ParametresGeneraux.BackColor = CouleurBackBandeaux

        Me.lbl_PP_Profile.ForeColor = CouleurForeBandeaux
        Me.lbl_PP_Profile.BackColor = CouleurBackBandeaux

        Me.lbl_PP_Dalle.ForeColor = CouleurForeBandeaux
        Me.lbl_PP_Dalle.BackColor = CouleurBackBandeaux


    End Sub

    Private Sub AfficherPoutreEnCours()

        Dim dc As Decimal 'largeur de calcul pour le PP
        If MyPoutreLoc.lIntermediaire Then
            dc = MyPoutreLoc.EntraxeD1 / 2 + MyPoutreLoc.EntraxeD2 / 2
        Else
            dc = MyPoutreLoc.EntraxeD1 + MyPoutreLoc.EntraxeD2 / 2
        End If

        Me.txt_Projet.Text = MyProjet.Nom
        Me.txt_BeamID.Text = MyProjet.Poutres(MyProjet.IndEnCours).BeamID
        Me.txt_Gravite.Text = Format(MyPoutreLoc.Param.GraviteG, formatTxtBox)
        Me.txt_PP_tot.Text = GetStringInUnit(PoidsPropreLoc.qPP_Total / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)), Enu_TypeVariable.SansType, 3, 3, False)

        Me.txt_rhoa.Text = Format(MyPoutreLoc.Section.Acier.Rho, formatTxtBox2)
        Me.txt_Aa.Text = GetStringInUnit(MyPoutreLoc.Section.ProfilA.Aire / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur) ^ 2, Enu_TypeVariable.SansType, 3, 3, False)
        Me.txt_qa.Text = GetStringInUnit(PoidsPropreLoc.qPP_ProfilAcier / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)), Enu_TypeVariable.SansType, 3, 3, False)

        If MyPoutreLoc.Section.lEnrobage Then
            Me.txt_rhoec.Text = Format(MyPoutreLoc.Section.Enrobage.Beton.RhoC, formatTxtBox2)
            Me.txt_Aec.Text = GetStringInUnit(MyPoutreLoc.Section.AireEnrobagePartielAec / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur) ^ 2, Enu_TypeVariable.SansType, 3, 3, False)
            Me.txt_qec.Text = GetStringInUnit(PoidsPropreLoc.qPP_BetonEnrobage / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)), Enu_TypeVariable.SansType, 3, 3, False)
        Else
            Me.txt_rhoec.Text = 0
            Me.txt_Aec.Text = 0
            Me.txt_qec.Text = 0
        End If



        Me.txt_rhoc.Text = Format(MyPoutreLoc.Dalle.beton.RhoC, formatTxtBox2)
        Me.txt_Ac.Text = GetStringInUnit(MyPoutreLoc.Dalle.Aire(dc, MyPoutreLoc.Section.ProfilA.Bfs) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur) ^ 2, Enu_TypeVariable.SansType, 3, 3, False)
        Me.txt_qc.Text = GetStringInUnit(PoidsPropreLoc.qPP_DalleBeton / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)), Enu_TypeVariable.SansType, 3, 3, False)

        If MyPoutreLoc.Dalle.type = cls_Dalle.Enum_TypeDalle.Mixte Then
            Me.txt_mup.Text = Format(MyPoutreLoc.Dalle.Bac.msurf, formatTxtBox)
            Me.txt_dc.Text = GetStringInUnit(dc / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur), Enu_TypeVariable.SansType, 3, 3, False)
            Me.txt_qp.Text = GetStringInUnit(PoidsPropreLoc.qPP_BacAcier / (LogicielInfo.Transfert_Effort(LogicielOptions.IndUnitEffort) / LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitLongueur)), Enu_TypeVariable.SansType, 3, 3, False)
        Else
            Me.txt_mup.Text = 0
            Me.txt_dc.Text = 0
            Me.txt_qp.Text = 0
        End If


    End Sub

#End Region

#Region " Dessins "
    Private Sub AffichageSymboles(sender As Object, e As PaintEventArgs) Handles img_rhoa.Paint, img_Aa.Paint, img_qa.Paint, img_rhoec.Paint, img_Aec.Paint, img_qec.Paint, img_rhoc.Paint, img_Ac.Paint, img_qc.Paint, img_mup.Paint, img_dc.Paint, img_qp.Paint, img_qTot.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height
        Dim xStart As Single = sWI * 0.95

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lItalic, lEgal As Boolean
        Dim xPen As Single = xStart
        Dim hCar As Single = e.Graphics.MeasureString("X", FontSymbolNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yPen As Single = (sHI / 2 - hCar) / 2 + sHI * 0.15

        '--> Initialisation

        lItalic = False
        lGrec = False
        lEgal = True

        Select Case sender.name
            Case Me.img_rhoa.Name
                lGrec = True
                strSymbol = "r"
                strIndice = "a"

            Case Me.img_Aa.Name
                strSymbol = "A"
                strIndice = "a"

            Case Me.img_qa.Name
                strSymbol = "q"
                strIndice = "a"

            Case Me.img_rhoec.Name
                lGrec = True
                strSymbol = "r"
                strIndice = "ec"

            Case Me.img_Aec.Name
                strSymbol = "A"
                strIndice = "ec"

            Case Me.img_qec.Name
                strSymbol = "q"
                strIndice = "ec"

            Case Me.img_rhoc.Name
                lGrec = True
                strSymbol = "r"
                strIndice = "c"

            Case Me.img_Ac.Name
                strSymbol = "A"
                strIndice = "c"

            Case Me.img_qc.Name
                strSymbol = "q"
                strIndice = "c"

            Case Me.img_mup.Name
                lGrec = True
                strSymbol = "m"
                strIndice = "p"

            Case Me.img_dc.Name
                strSymbol = "d"
                strIndice = "c"

            Case Me.img_qp.Name
                strSymbol = "q"
                strIndice = "p"

            Case Me.img_qTot.Name
                strSymbol = "q"
                strIndice = "tot"

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lItalic, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub


#End Region

End Class