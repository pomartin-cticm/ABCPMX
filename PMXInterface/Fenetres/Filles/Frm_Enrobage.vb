Imports PMXInterface.Frm_SectionAcierStandard
Imports PMXMoteur2
Imports System.IO

Public Class Frm_Enrobage


#Region " Variables locales "

    Dim lBuild As Boolean = True

    Const iFRMENROBAGE As Integer = 3
    Const kAdjust As Decimal = 0.95
    Dim MyEnrobage As New cls_Enrobage_Partiel

    Dim MyBf As Decimal

    '--> Options d'affichage
    Enum Enu_LitArmaEnCours
        Superieur
        Intermediaire
        Inferieur
    End Enum
    Dim LitArmaEnCours As Enu_LitArmaEnCours = Enu_LitArmaEnCours.Inferieur

    Dim iSelect As Integer = -1
    Const iSelectDIM As Integer = 0             ' Largeur de l'enrobage
    Const iSelectETRIERS As Integer = 1         ' Etriers définition
    Const iSelectETRIERSUY As Integer = 2       ' Etriers enrobage UY
    Const iSelectETRIERSUZ As Integer = 3       ' Etriers enrobage UZ
    Const iSelectETRIERSPHI As Integer = 4      ' Etriers enrobage diametre
    Const iSelectARMAZ As Integer = 5           ' Position z du lit central
    Const iSelectARMAZINF As Integer = 105      ' Position z du lit inférieur
    Const iSelectARMAZSUP As Integer = 115      ' Position z du lit supérieur
    Const iSelectLITINF As Integer = 6          ' Lit inférieur (général)
    Const iSelectLITMID As Integer = 7          ' Lit central (général)
    Const iSelectLITSUP As Integer = 8          ' Lit supérieur (général)
    Const iSelectACIER As Integer = 200         ' Propriétés acier
    Const iSelectBETON As Integer = 201         ' Propriétés béton

    '--> 
    Dim DiametreEtriers() As Decimal = {0.006, 0.008, 0.01}
    Dim DiametreArmaConst() As Decimal = {0.008, 0.01}
    Dim DiametreArmaInf() As Decimal = {0.008, 0.01, 0.012, 0.016, 0.02}
    Dim DiametreArmaNormal() As Decimal = {0.008, 0.01, 0.012}
    Dim RatiosLargeur() As Decimal = {1, 0.9, 0.8}

    Dim ClasseBeton() As String = cls_Beton.TabClasseBeton
    Dim ClasseAcierArma() As String = cls_AcierArmature.tabClasseAcierArma ' {"B450", "B500"}

    Dim TabNb(3) As String
    Dim TabNbAutre(1) As String

    Dim iStart(2), iEnd(2) As Integer

    '--> Textes
    Dim str_Lit(2) As String
    Dim strTypeEtriers(2) As String


#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Enrobage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        lBuild = True
        GestionLangues()
        GestionStyle()
        GestionUnites()

        InitialiseVariable()

        MAJI_AffichageLitArma()
        RemplirComboTypeEtriers()

        RemplirComboAvecValeurs(Me.cmb_DiametreEtriers, Me.DiametreEtriers, Enu_TypeVariable.Dimension)
        RemplirComboAvecValeurs(Me.cmb_RatioBc, Me.RatiosLargeur, Enu_TypeVariable.SansType)
        RemplirComboAvecTableau(Me.cmb_ClasseBetonEnrobage, ClasseBeton)
        RemplirComboAvecTableau(Me.cmb_Acier, ClasseAcierArma)

        AfficherEnrobageEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_ENCASEMENT")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== GENERAL ======================================================================

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== MENU PRINCIPAL ==============================================================='

                '-- Dimensions
                Me.lbl_Dimensions.Text = Bloc("DIMENSIONS")
                Me.lbl_Largeur.Text = Bloc("WIDTH")

                '-- Etriers
                Me.lbl_Etriers.Text = Bloc("STIRRUPS")
                Me.lbl_Type.Text = Bloc("TYPEE")
                strTypeEtriers(0) = "Cadre ZZZ "
                strTypeEtriers(1) = "Etrier soudé ZZZ "
                strTypeEtriers(2) = "Cadre traversant ZZZ "
                Me.lbl_EnrobageEtrier.Text = Bloc("COVERAGE")
                Me.lbl_DiametreE.Text = Bloc("DIAMETER")

                '-- Armatures longi
                Me.lbl_ArmaLongi.Text = Bloc("LONGIREBAR")
                Me.lbl_Nombre.Text = Bloc("NUMBER")
                Me.lbl_PositionZarma.Text = Bloc("POSITION")
                Me.lbl_Active.Text = Bloc("ACTIVEREBARS")

                TabNb(0) = Bloc("NONE")
                TabNb(1) = Bloc("SINGLE")
                TabNb(2) = Bloc("DOUBLE")
                TabNb(3) = Bloc("TRIPLE")
                TabNbAutre(0) = Bloc("NONE")
                TabNbAutre(1) = Bloc("SINGLE")
                str_Lit(2) = Bloc("UPPERLAYER")
                str_Lit(1) = Bloc("MIDDLELAYER")
                str_Lit(0) = Bloc("LOWERLAYER")

                '-- Acier
                Me.lbl_Acier.Text = Bloc("STEEL")
                Me.lbl_ClasseA.Text = Bloc("CLASS")

                '-- Béton
                Me.lbl_Beton.Text = Bloc("CONCRETE")
                Me.lbl_ClasseE.Text = Bloc("CLASS")
                Me.lbl_DiametreA.Text = Bloc("DIAMETER")
                Me.lbl_Exterieur.Text = Bloc("EXTERNAL")
                Me.lbl_Middle.Text = Bloc("MIDDLE")
                Me.lbl_Interieur.Text = Bloc("INTERNAL")

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim1.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim4.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim5.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitDimCarre.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension) & "2"

        Me.etq_UnitSigma1.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)
        Me.etq_UnitSigma2.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)
        Me.etq_UnitModule1.Text = LogicielInfo.Unit_ModulesY(LogicielOptions.IndUnitModulesY)

    End Sub

    Private Sub InitialiseVariable()
        MyBf = MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.Bfs

        MyEnrobage.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage, MyEnrobage)

    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.img_Enrobage.Dock = DockStyle.Fill

        Me.txt_Bc.BackColor = CouleurReadOnly
        Me.txt_Bc.ReadOnly = True
        Me.txt_Fck.BackColor = CouleurReadOnly
        Me.txt_Fck.ReadOnly = True
        Me.txt_Ecm.BackColor = CouleurReadOnly
        Me.txt_Ecm.ReadOnly = True
        Me.txt_Fsk.BackColor = CouleurReadOnly
        Me.txt_Fsk.ReadOnly = True
        Me.txt_As.BackColor = CouleurReadOnly
        Me.txt_As.ReadOnly = True

        Me.lbl_Dimensions.BackColor = CouleurBackBandeaux
        Me.lbl_Dimensions.ForeColor = CouleurForeBandeaux

        Me.lbl_Etriers.BackColor = CouleurBackBandeaux
        Me.lbl_Etriers.ForeColor = CouleurForeBandeaux

        Me.lbl_Acier.BackColor = CouleurBackBandeaux
        Me.lbl_Acier.ForeColor = CouleurForeBandeaux

        Me.lbl_Beton.BackColor = CouleurBackBandeaux
        Me.lbl_Beton.ForeColor = CouleurForeBandeaux

        Me.lbl_ArmaLongi.BackColor = CouleurBackBandeaux
        Me.lbl_ArmaLongi.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub AfficherEnrobageEnCours()
        Dim Chaine As String

        With MyEnrobage

            '--> Largeur

            Me.cmb_RatioBc.SelectedIndex = Array.IndexOf(Me.RatiosLargeur, MyEnrobage.Ratio_bc)
            Me.txt_Bc.Text = GetStringNoUnit(.Ratio_bc * MyBf, Enu_TypeVariable.Dimension)

            '--> Etriers

            'Me.txt_EtrierPhi.Text = GetStringNoUnit(.Etriers_Phi, Enu_TypeVariable.Dimension)
            Me.cmb_DiametreEtriers.Text = GetStringNoUnit(.Etriers_Phi, Enu_TypeVariable.Dimension)
            Me.txt_EtrierUy.Text = GetStringNoUnit(.Etriers_EnrobageY, Enu_TypeVariable.Dimension)
            Me.txt_EtrierUz.Text = GetStringNoUnit(.Etriers_EnrobageZ, Enu_TypeVariable.Dimension)

            Select Case .Etriers_Type
                Case cls_Enrobage_Partiel.EnuTypeEtriers.Cadre : Me.cmb_TypeEtriers.SelectedIndex = 0
                Case cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant : Me.cmb_TypeEtriers.SelectedIndex = 2
                Case cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude : Me.cmb_TypeEtriers.SelectedIndex = 1
            End Select

            '--> Béton

            Chaine = .Beton.Classe
            If Me.ClasseBeton.Contains(Chaine) Then
                Me.cmb_ClasseBetonEnrobage.SelectedIndex = Array.IndexOf(Me.ClasseBeton, Chaine)
            Else
                Me.cmb_ClasseBetonEnrobage.SelectedIndex = 0
            End If
            MAJI_ProprietesBeton()

            '--> Acier

            Chaine = .AcierArmatures.Classe
            If Me.ClasseAcierArma.Contains(Chaine) Then
                Me.cmb_Acier.SelectedIndex = Array.IndexOf(Me.ClasseAcierArma, Chaine)
            Else
                Me.cmb_Acier.SelectedIndex = 0
            End If
            MAJI_ProprietesAcier()

        End With

        AffichageLitArmaturesEnCours()
    End Sub

    Private Sub RemplirComboTypeEtriers()
        Me.cmb_TypeEtriers.Items.Clear()
        Me.cmb_TypeEtriers.Items.AddRange(strTypeEtriers)
        Me.cmb_TypeEtriers.SelectedIndex = 0
    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)
        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)
    End Sub

    Private Sub MAJI_AffichageLitArma()

        DeselectionneChkArma()

        Select Case LitArmaEnCours
            Case Enu_LitArmaEnCours.Inferieur
                Me.chk_LitInf.Checked = True
                Me.lbl_LitSelectionne.Text = str_Lit(0)
                Me.chk_LitInf.Image = MyImgList.Images("LitInf")
                Me.chk_LitInter.Image = MyImgList.Images("LitInterNonS")
                Me.chk_LitSup.Image = MyImgList.Images("LitSupNonS")
            Case Enu_LitArmaEnCours.Intermediaire
                Me.chk_LitInter.Checked = True
                Me.lbl_LitSelectionne.Text = str_Lit(1)
                Me.chk_LitInf.Image = MyImgList.Images("LitInfNonS")
                Me.chk_LitInter.Image = MyImgList.Images("LitInter")
                Me.chk_LitSup.Image = MyImgList.Images("LitSupNonS")
            Case Enu_LitArmaEnCours.Superieur
                Me.chk_LitSup.Checked = True
                Me.lbl_LitSelectionne.Text = str_Lit(2)
                Me.chk_LitInf.Image = MyImgList.Images("LitInfNonS")
                Me.chk_LitInter.Image = MyImgList.Images("LitInterNonS")
                Me.chk_LitSup.Image = MyImgList.Images("LitSup")
        End Select

        MAJI_AireArmaLongi()

        Me.txt_zArma.ReadOnly = Not (LitArmaEnCours = Enu_LitArmaEnCours.Intermediaire)
        If (LitArmaEnCours = Enu_LitArmaEnCours.Intermediaire) Then
            Me.txt_zArma.BackColor = SystemColors.Window
        Else
            Me.txt_zArma.BackColor = CouleurReadOnly
            MAJI_zPosArmaLongi()
        End If
    End Sub

    Private Sub MAJI_zPosArmaLongi()
        Dim iArma As Integer = IndiceLitAffiche()
        Dim zPos As Decimal = MyProjet.Poutres(MyProjet.IndEnCours).Section.zPositionLitArmaEnrobage(iArma)
        Me.txt_zArma.Text = GetStringNoUnit(Math.Abs(zPos), Enu_TypeVariable.Dimension)
    End Sub

    Private Sub MAJI_AireArmaLongi()
        Dim iArma As Integer = IndiceLitAffiche()

        Dim AireAs As Decimal = MyEnrobage.LitArma(iArma).Aire
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension) ^ 2

        Me.txt_As.Text = GetStringNoUnit(AireAs / kUnit, Enu_TypeVariable.SansType)
    End Sub

    Private Sub InitialiseControleArmatures()
        Dim iArma As Integer
        Select Case LitArmaEnCours
            Case Enu_LitArmaEnCours.Inferieur
                iArma = 0
                Select Case MyEnrobage.Etriers_Type
                    Case cls_Enrobage_Partiel.EnuTypeEtriers.Cadre
                        iStart(0) = 1 : iEnd(0) = 3
                        iStart(1) = 0 : iEnd(1) = 3
                        iStart(2) = 1 : iEnd(2) = 3
                    Case cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant, cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude
                        iStart(0) = 1 : iEnd(0) = 3
                        iStart(1) = 0 : iEnd(1) = 3
                        iStart(2) = 0 : iEnd(2) = 3
                End Select
            Case Enu_LitArmaEnCours.Intermediaire
                iArma = 1

                iStart(0) = 0 : iEnd(0) = 1
                iStart(1) = 0 : iEnd(1) = 0
                iStart(2) = 0 : iEnd(2) = 1

            Case Enu_LitArmaEnCours.Superieur
                iArma = 2
                Select Case MyEnrobage.Etriers_Type
                    Case cls_Enrobage_Partiel.EnuTypeEtriers.Cadre
                        iStart(0) = 1 : iEnd(0) = 1
                        iStart(1) = 0 : iEnd(1) = 0
                        iStart(2) = 1 : iEnd(2) = 1
                    Case cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant, cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude
                        iStart(0) = 1 : iEnd(0) = 1
                        iStart(1) = 0 : iEnd(1) = 0
                        iStart(2) = 0 : iEnd(2) = 1
                End Select
        End Select

        Me.pan_Mileu.Visible = (LitArmaEnCours = Enu_LitArmaEnCours.Inferieur)

        '--> Nombre

        RemplirComboNombre(Me.cmb_NombreExt, TabNb, iStart(0), iEnd(0))
        RemplirComboNombre(Me.cmb_NombreMil, TabNb, iStart(1), iEnd(1))
        RemplirComboNombre(Me.cmb_NombreInt, TabNb, iStart(2), iEnd(2))

        '--> Diamètre 

        If iArma = 0 Then
            RemplirComboAvecValeurs(Me.cmb_DiaExt, DiametreArmaInf, Enu_TypeVariable.Dimension)
            RemplirComboAvecValeurs(Me.cmb_DiaMil, DiametreArmaInf, Enu_TypeVariable.Dimension)
            RemplirComboAvecValeurs(Me.cmb_DiaInt, DiametreArmaInf, Enu_TypeVariable.Dimension)
        Else
            RemplirComboAvecValeurs(Me.cmb_DiaExt, DiametreArmaNormal, Enu_TypeVariable.Dimension)
            RemplirComboAvecValeurs(Me.cmb_DiaMil, DiametreArmaNormal, Enu_TypeVariable.Dimension)
            RemplirComboAvecValeurs(Me.cmb_DiaInt, DiametreArmaNormal, Enu_TypeVariable.Dimension)
        End If


    End Sub

    ''' <summary>
    ''' Affichage des données du lit d'armatures à l'affichage, pour la section en cours
    ''' </summary>
    Private Sub AffichageLitArmaturesEnCours()
        Dim iArma As Integer
        Select Case LitArmaEnCours
            Case Enu_LitArmaEnCours.Inferieur : iArma = 0
            Case Enu_LitArmaEnCours.Intermediaire : iArma = 1
            Case Enu_LitArmaEnCours.Superieur : iArma = 2
        End Select

        InitialiseControleArmatures()

        '--> Nombre

        'Select Case iArma
        '    Case 0, 2
        '        RemplirComboNombre(Me.cmb_NombreExt, TabNb)
        '        RemplirComboNombre(Me.cmb_NombreMil, TabNb)
        '        RemplirComboNombre(Me.cmb_NombreInt, TabNb)
        '    Case Else
        '        RemplirComboNombre(Me.cmb_NombreExt, TabNbAutre)
        '        RemplirComboNombre(Me.cmb_NombreMil, TabNbAutre)
        '        RemplirComboNombre(Me.cmb_NombreInt, TabNbAutre)
        'End Select

        Me.cmb_NombreExt.SelectedIndex = MyEnrobage.LitArma(iArma).NbExt - iStart(0)
        Me.cmb_NombreMil.SelectedIndex = MyEnrobage.LitArma(iArma).NbMil - iStart(1)
        Me.cmb_NombreInt.SelectedIndex = MyEnrobage.LitArma(iArma).NbInt - iStart(2)

        '--> Diametre

        AfficheDiametreDansCombo(Me.cmb_DiaExt, MyEnrobage.LitArma(iArma).PhiExt)
        AfficheDiametreDansCombo(Me.cmb_DiaMil, MyEnrobage.LitArma(iArma).PhiMil)
        AfficheDiametreDansCombo(Me.cmb_DiaInt, MyEnrobage.LitArma(iArma).PhiInt)

        '--> Barres Actives

        AfficheBarreActive(Me.chk_ActiveExt, MyEnrobage.LitArma(iArma).NbExt, False, MyEnrobage.LitArma(iArma).lActiveExt)
        AfficheBarreActive(Me.chk_ActiveMil, MyEnrobage.LitArma(iArma).NbMil, True, True)
        AfficheBarreActive(Me.chk_ActiveInt, MyEnrobage.LitArma(iArma).NbInt, False, MyEnrobage.LitArma(iArma).lActiveInt)

        '--> Position z

        If iArma = 1 Then
            Me.txt_zArma.Text = GetStringNoUnit(-MyProjet.Poutres(MyProjet.IndEnCours).Section.zPositionLitArmaEnrobage(iArma), Enu_TypeVariable.Dimension)
        Else
            MAJI_zPosArmaLongi()
        End If

    End Sub

    Private Sub AfficheBarreActive(myChk As CheckBox, NbBarres As Integer, lMilieu As Boolean, lActive As Boolean)

        If lMilieu Then
            myChk.Checked = True
            myChk.Enabled = False
        Else
            'If NbBarres > 1 Then
            '    myChk.Checked = True
            '    myChk.Enabled = False
            'Else
            '    myChk.Checked = lActive
            '    myChk.Enabled = True
            'End If
            MAJI_chkActive(myChk, NbBarres, lActive)
        End If

    End Sub


    ''' <summary>
    ''' Affichage d'une diamètre dans une combo box déjà remplie
    ''' </summary>
    ''' <param name="MyCombo">      [E/S]   Combobox                        </param>
    ''' <param name="MyDiametre">   [E]     Valeur du diamètre à afficher   </param>
    Private Sub AfficheDiametreDansCombo(MyCombo As ComboBox, MyDiametre As Decimal)
        Dim strDia As String
        strDia = GetStringNoUnit(MyDiametre, Enu_TypeVariable.Dimension)
        If MyCombo.Items.Contains(strDia) Then
            MyCombo.Text = strDia
        Else
            MyCombo.SelectedIndex = 0
        End If

    End Sub

    Private Sub RemplirComboNombre(MyCombo As ComboBox, tabNombre() As String, i0 As Integer, iE As Integer)

        MyCombo.Items.Clear()

        For i As Integer = i0 To iE
            MyCombo.Items.Add(tabNombre(i))
        Next

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
                MyProjet.Poutres(MyProjet.IndEnCours).EstModifiee()
            End If

            MyProjet.Poutres(MyProjet.IndEnCours).EstValidee(iFRMENROBAGE)

            Me.Close()
        End If
    End Sub

    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False

        '-- Dimensions ----------------------------------------------------------------------------------------------------

        GereTransfertValeur(MyEnrobage.Ratio_bc, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.Ratio_bc, lModif)

        '-- Etriers -------------------------------------------------------------------------------------------------------

        If (MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.Etriers_Type <> MyEnrobage.Etriers_Type) Then
            MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.Etriers_Type = MyEnrobage.Etriers_Type
            lModif = True
        End If

        GereTransfertValeur(MyEnrobage.Etriers_Phi, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.Etriers_Phi, lModif)
        GereTransfertValeur(MyEnrobage.Etriers_EnrobageY, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.Etriers_EnrobageY, lModif)
        GereTransfertValeur(MyEnrobage.Etriers_EnrobageZ, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.Etriers_EnrobageZ, lModif)

        '-- Beton ----------------------------------------------------------------------------------------------------------------------

        GereTransfertValeur(MyEnrobage.Beton.Classe, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.Beton.Classe, lModif)
        MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.Beton.Calcul_Proprietes()

        '-- Acier ---------------------------------------------------------------------------------------------------------------------

        GereTransfertValeur(MyEnrobage.AcierArmatures.Classe, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.AcierArmatures.Classe, lModif)
        MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.AcierArmatures.MAJProprietes()

        '-- Lits d'armatures ---------------------------------------------------------------------------------------------------------

        For i As Integer = 0 To 2

            GereTransfertValeur(MyEnrobage.LitArma(i).PhiExt, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.LitArma(i).PhiExt, lModif)
            GereTransfertValeur(MyEnrobage.LitArma(i).NbExt, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.LitArma(i).NbExt, lModif)
            GereTransfertValeur(MyEnrobage.LitArma(i).lActiveExt, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.LitArma(i).lActiveExt, lModif)
            GereTransfertValeur(MyEnrobage.LitArma(i).PhiMil, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.LitArma(i).PhiMil, lModif)
            GereTransfertValeur(MyEnrobage.LitArma(i).NbMil, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.LitArma(i).NbMil, lModif)
            GereTransfertValeur(MyEnrobage.LitArma(i).PhiInt, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.LitArma(i).PhiInt, lModif)
            GereTransfertValeur(MyEnrobage.LitArma(i).NbInt, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.LitArma(i).NbInt, lModif)
            GereTransfertValeur(MyEnrobage.LitArma(i).lActiveInt, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.LitArma(i).lActiveInt, lModif)

        Next

        GereTransfertValeur(MyEnrobage.LitArma(1).zPosRatio, MyProjet.Poutres(MyProjet.IndEnCours).Section.Enrobage.LitArma(1).zPosRatio, lModif)

    End Sub


    'Private Sub TransfertSaisie(ByRef lModif As Boolean)

    '    lModif = False

    '    '-- Dimensions ----------------------------------------------------------------------------------------------------

    '    If (MyEnrobage.Ratio_bc <> MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.Ratio_bc) Then
    '        lModif = True
    '        MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.Ratio_bc = MyEnrobage.Ratio_bc
    '    End If

    '    '-- Etriers -------------------------------------------------------------------------------------------------------

    '    If (MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.Etriers_Type <> MyEnrobage.Etriers_Type) Then
    '        MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.Etriers_Type = MyEnrobage.Etriers_Type
    '        lModif = True
    '    End If

    '    If (MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.Etriers_Phi <> MyEnrobage.Etriers_Phi) Then
    '        MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.Etriers_Phi = MyEnrobage.Etriers_Phi
    '        lModif = True
    '    End If

    '    If (MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.Etriers_EnrobageY <> MyEnrobage.Etriers_EnrobageY) Then
    '        MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.Etriers_EnrobageY = MyEnrobage.Etriers_EnrobageY
    '        lModif = True
    '    End If

    '    If (MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.Etriers_EnrobageZ <> MyEnrobage.Etriers_EnrobageZ) Then
    '        MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.Etriers_EnrobageZ = MyEnrobage.Etriers_EnrobageZ
    '        lModif = True
    '    End If

    '    '-- Beton ----------------------------------------------------------------------------------------------------------------------

    '    If (MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.Beton.Classe <> MyEnrobage.Beton.Classe) Then
    '        MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.Beton.Classe = MyEnrobage.Beton.Classe
    '        MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.Beton.Calcul_Proprietes()
    '        lModif = True
    '    End If

    '    '-- Acier ---------------------------------------------------------------------------------------------------------------------

    '    If (MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.AcierArmatures.Classe <> MyEnrobage.AcierArmatures.Classe) Then
    '        MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.AcierArmatures.Classe = MyEnrobage.AcierArmatures.Classe
    '        MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.AcierArmatures.MAJProprietes()
    '        lModif = True
    '    End If

    '    '-- Lits d'armatures ---------------------------------------------------------------------------------------------------------

    '    For i As Integer = 0 To 2

    '        If (MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(i).PhiExt <> MyEnrobage.LitArma(i).PhiExt) Then
    '            MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(i).PhiExt = MyEnrobage.LitArma(i).PhiExt
    '            lModif = True
    '        End If

    '        If (MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(i).NbExt <> MyEnrobage.LitArma(i).NbExt) Then
    '            MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(i).NbExt = MyEnrobage.LitArma(i).NbExt
    '            lModif = True
    '        End If

    '        If (MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(i).PhiMil <> MyEnrobage.LitArma(i).PhiMil) Then
    '            MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(i).PhiMil = MyEnrobage.LitArma(i).PhiMil
    '            lModif = True
    '        End If

    '        If (MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(i).NbMil <> MyEnrobage.LitArma(i).NbMil) Then
    '            MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(i).NbMil = MyEnrobage.LitArma(i).NbMil
    '            lModif = True
    '        End If

    '        If (MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(i).PhiInt <> MyEnrobage.LitArma(i).PhiInt) Then
    '            MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(i).PhiInt = MyEnrobage.LitArma(i).PhiInt
    '            lModif = True
    '        End If

    '        If (MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(i).NbInt <> MyEnrobage.LitArma(i).NbInt) Then
    '            MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(i).NbInt = MyEnrobage.LitArma(i).NbInt
    '            lModif = True
    '        End If

    '    Next

    '    If (MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(1).zPosRatio <> MyEnrobage.LitArma(1).zPosRatio) Then
    '        MyProjet.Poutres(MyProjet.IndEnCours).Section.enrobage_partiel.LitArma(1).zPosRatio = MyEnrobage.LitArma(1).zPosRatio
    '        lModif = True
    '    End If


    'End Sub

#End Region

#Region " Dessins "

    Private Sub img_Enrobage_Paint(sender As Object, e As PaintEventArgs) Handles img_Enrobage.Paint

        DessinFrmEnrobage(e.Graphics, MyProjet.Poutres(MyProjet.IndEnCours).Section, MyEnrobage,
                          Me.img_Enrobage.ClientRectangle.Width, Me.img_Enrobage.ClientRectangle.Height, kAdjust, True, False, iSelect)

    End Sub


#End Region

#Region " Evènements "

    Private Sub DeselectionneChkArma()

        Me.chk_LitInf.Checked = False
        Me.chk_LitSup.Checked = False
        Me.chk_LitInter.Checked = False

    End Sub

    Private Sub checkedchangedLitArma(sender As Object, e As EventArgs) Handles chk_LitSup.CheckedChanged, chk_LitInter.CheckedChanged, chk_LitInf.CheckedChanged
        If lBuild Then Exit Sub

        lBuild = True

        Select Case sender.name
            Case Me.chk_LitInf.Name
                LitArmaEnCours = Enu_LitArmaEnCours.Inferieur
                iSelect = iSelectLITINF
            Case Me.chk_LitInter.Name
                LitArmaEnCours = Enu_LitArmaEnCours.Intermediaire
                iSelect = iSelectLITMID
            Case Me.chk_LitSup.Name
                LitArmaEnCours = Enu_LitArmaEnCours.Superieur
                iSelect = iSelectLITSUP
        End Select

        MAJI_AffichageLitArma()

        AffichageLitArmaturesEnCours()
        Me.img_As.Invalidate()
        Me.img_zArma.Invalidate()
        Me.img_Enrobage.Invalidate()

        lBuild = False
    End Sub

    Private Sub Frm_EnrobagePartielN_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Me.img_Enrobage.Invalidate()
    End Sub

    Private Sub EnterTxtBoxes(sender As Object, e As EventArgs) Handles txt_zArma.Enter, txt_EtrierUz.Enter, txt_EtrierUy.Enter

        If lBuild Then Exit Sub

        Select Case sender.name

            Case Me.txt_EtrierUy.Name : iSelect = iSelectETRIERSUy
            Case Me.txt_EtrierUz.Name : iSelect = iSelectETRIERSUZ
            Case Me.txt_zArma.Name
                Select Case LitArmaEnCours
                    Case Enu_LitArmaEnCours.Inferieur : iSelect = iSelectARMAZINF
                    Case Enu_LitArmaEnCours.Intermediaire : iSelect = iSelectARMAZ
                    Case Enu_LitArmaEnCours.Superieur : iSelect = iSelectARMAZSUP
                End Select

        End Select

        Me.img_Enrobage.Invalidate()

    End Sub

    Private Sub LeaveTxtBoxes(sender As Object, e As EventArgs) Handles txt_zArma.Leave, txt_EtrierUz.Leave, txt_EtrierUy.Leave

        iSelect = -1
        Me.img_Enrobage.Invalidate()

    End Sub

    Private Sub cmb_RatioBc_Leave(sender As Object, e As EventArgs) Handles cmb_RatioBc.Leave
        iSelect = -1
        Me.img_Enrobage.Invalidate()
    End Sub

    Private Sub cmb_RatioBc_Enter(sender As Object, e As EventArgs) Handles cmb_RatioBc.Enter
        If lBuild Then Exit Sub
        iSelect = iSelectDIM
        Me.img_Enrobage.Invalidate()
    End Sub

    Private Sub cmb_TypeEtriers_Leave(sender As Object, e As EventArgs) Handles cmb_TypeEtriers.Leave, cmb_DiametreEtriers.Leave
        iSelect = -1
        Me.img_Enrobage.Invalidate()
    End Sub

    Private Sub cmb_TypeEtriers_Enter(sender As Object, e As EventArgs) Handles cmb_TypeEtriers.Enter, cmb_DiametreEtriers.Enter
        If lBuild Then Exit Sub
        iSelect = iSelectETRIERS
        Me.img_Enrobage.Invalidate()
    End Sub

    Private Sub cmb_DiametreEtriers_Leave(sender As Object, e As EventArgs)
        iSelect = -1
        Me.img_Enrobage.Invalidate()
    End Sub

    Private Sub cmb_DiametreEtriers_Enter(sender As Object, e As EventArgs)
        If lBuild Then Exit Sub
        iSelect = iSelectETRIERSPHI
        Me.img_Enrobage.Invalidate()
    End Sub

    Private Sub txt_zArma_Leave(sender As Object, e As EventArgs)
        iSelect = -1
        Me.img_Enrobage.Invalidate()
    End Sub

    Private Sub txt_zArma_Enter(sender As Object, e As EventArgs)
        If lBuild Then Exit Sub
        iSelect = iSelectARMAZ
        Me.img_Enrobage.Invalidate()
    End Sub


    Private Sub cmbArmaLeave(sender As Object, e As EventArgs) Handles cmb_NombreMil.Leave, cmb_NombreInt.Leave, cmb_NombreExt.Leave, cmb_DiaMil.Leave, cmb_DiaInt.Leave, cmb_DiaExt.Leave
        iSelect = -1
        Me.img_Enrobage.Invalidate()
    End Sub

    Private Sub cmbArmaEnter(sender As Object, e As EventArgs) Handles cmb_NombreMil.Enter, cmb_NombreInt.Enter, cmb_NombreExt.Enter, cmb_DiaMil.Enter, cmb_DiaInt.Enter, cmb_DiaExt.Enter
        If lBuild Then Exit Sub

        Dim iArma As Integer = IndiceLitAffiche()

        Select Case sender.name
            Case Me.cmb_NombreExt.Name
                iSelect = 10 + 3 * (iArma)
            Case Me.cmb_NombreMil.Name
                iSelect = 11 + 3 * (iArma)
            Case Me.cmb_NombreInt.Name
                iSelect = 12 + 3 * (iArma)
            Case Me.cmb_DiaExt.Name
                iSelect = 20 + 3 * (iArma)
            Case Me.cmb_DiaMil.Name
                iSelect = 21 + 3 * (iArma)
            Case Me.cmb_DiaInt.Name
                iSelect = 22 + 3 * (iArma)
        End Select
        Me.img_Enrobage.Invalidate()

    End Sub

    Private Sub cmb_Materiau_Enter(sender As Object, e As EventArgs) Handles cmb_ClasseBetonEnrobage.Enter, cmb_Acier.Enter
        If lBuild Then Exit Sub
        Select Case sender.name
            Case Me.cmb_ClasseBetonEnrobage.Name
                iSelect = iSelectBETON
            Case Me.cmb_Acier.Name
                iSelect = iSelectACIER
        End Select

        Me.img_Enrobage.Invalidate()
    End Sub

    Private Sub cmb_Materiau_Leave(sender As Object, e As EventArgs) Handles cmb_ClasseBetonEnrobage.Leave, cmb_Acier.Leave
        iSelect = -1
        Me.img_Enrobage.Invalidate()
    End Sub

#End Region

#Region " Evènements saisie "

    Private Sub txt_zArma_TextChanged(sender As Object, e As EventArgs) Handles txt_zArma.TextChanged
        If lBuild Then Exit Sub
        If LitArmaEnCours <> Enu_LitArmaEnCours.Intermediaire Then Exit Sub

        Dim Valeur As Decimal
        If VerificationSaisie(sender, Valeur) Then
            MyEnrobage.LitArma(1).zPosRatio = Valeur / MyProjet.Poutres(MyProjet.IndEnCours).Section.ProfilA.ha

            Me.img_Enrobage.Invalidate()
        End If
    End Sub

    Private Sub SaisieLitArma(sender As Object, e As EventArgs) Handles cmb_NombreMil.SelectedIndexChanged, cmb_NombreInt.SelectedIndexChanged, cmb_NombreExt.SelectedIndexChanged, cmb_DiaMil.SelectedIndexChanged, cmb_DiaInt.SelectedIndexChanged, cmb_DiaExt.SelectedIndexChanged
        If lBuild Then Exit Sub

        Dim iArma As Integer = IndiceLitAffiche()
        Dim Diametre() As Decimal

        If iArma = 0 Then
            Diametre = DiametreArmaInf
        Else
            Diametre = DiametreArmaNormal
        End If

        Select Case sender.name
            Case Me.cmb_DiaExt.Name
                MyEnrobage.LitArma(iArma).PhiExt = Diametre(Me.cmb_DiaExt.SelectedIndex)
            Case Me.cmb_DiaMil.Name
                MyEnrobage.LitArma(iArma).PhiMil = Diametre(Me.cmb_DiaMil.SelectedIndex)
            Case Me.cmb_DiaInt.Name
                MyEnrobage.LitArma(iArma).PhiInt = Diametre(Me.cmb_DiaInt.SelectedIndex)
            Case Me.cmb_NombreExt.Name
                MyEnrobage.LitArma(iArma).NbExt = Me.cmb_NombreExt.SelectedIndex + iStart(0)
            Case Me.cmb_NombreMil.Name
                MyEnrobage.LitArma(iArma).NbMil = Me.cmb_NombreMil.SelectedIndex + iStart(1)
            Case Me.cmb_NombreInt.Name
                MyEnrobage.LitArma(iArma).NbInt = Me.cmb_NombreInt.SelectedIndex + iStart(2)
        End Select

        Me.img_Enrobage.Invalidate()
        MAJI_AireArmaLongi()
        MAJI_ActiveArma()
    End Sub

    Private Sub MAJI_ActiveArma()

        Dim iArma As Integer = IndiceLitAffiche()

        If iArma = 0 Or iArma = 2 Then
            MAJI_chkActive(Me.chk_ActiveExt, MyEnrobage.LitArma(iArma).NbExt, MyEnrobage.LitArma(iArma).lActiveExt)
            MAJI_chkActive(Me.chk_ActiveInt, MyEnrobage.LitArma(iArma).NbInt, MyEnrobage.LitArma(iArma).lActiveInt)
        End If

    End Sub

    Private Sub MAJI_chkActive(myChk As CheckBox, nbBarres As Integer, lActive As Boolean)
        If nbBarres > 1 Then
            myChk.Checked = True
            myChk.Enabled = False
        Else
            myChk.Checked = lActive
            myChk.Enabled = True
        End If
    End Sub

    Private Sub cmb_Acier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Acier.SelectedIndexChanged
        If lBuild Then Exit Sub
        MyEnrobage.AcierArmatures.Classe = Me.ClasseAcierArma(Me.cmb_Acier.SelectedIndex)
        MAJI_ProprietesAcier()

        Me.img_Enrobage.Invalidate()
    End Sub

    Private Sub MAJI_ProprietesAcier()
        MyEnrobage.AcierArmatures.MAJProprietes()
        Me.txt_Fsk.Text = GetStringNoUnit(MyEnrobage.AcierArmatures.FsK, Enu_TypeVariable.Contrainte)
    End Sub

    Private Sub cmb_ClasseBetonEnrobage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_ClasseBetonEnrobage.SelectedIndexChanged
        If lBuild Then Exit Sub

        MyEnrobage.Beton.Classe = Me.ClasseBeton(Me.cmb_ClasseBetonEnrobage.SelectedIndex)

        MAJI_ProprietesBeton()
        Me.img_Enrobage.Invalidate()
    End Sub

    Private Sub MAJI_ProprietesBeton()

        MyEnrobage.Beton.Calcul_Proprietes()

        Me.txt_Fck.Text = GetStringNoUnit(MyEnrobage.Beton.Fck, Enu_TypeVariable.Contrainte)
        Me.txt_Ecm.Text = GetStringNoUnit(MyEnrobage.Beton.Ecm, Enu_TypeVariable.ModuleY)

    End Sub

    Private Sub cmb_RatioBc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_RatioBc.SelectedIndexChanged

        If lBuild Then Exit Sub

        MyEnrobage.Ratio_bc = Me.RatiosLargeur(Me.cmb_RatioBc.SelectedIndex)
        Me.txt_Bc.Text = GetStringNoUnit(MyEnrobage.Ratio_bc * MyBf, Enu_TypeVariable.Dimension)

        Me.img_Enrobage.Invalidate()

    End Sub

    Private Sub SaisieTxtEnrobage(sender As Object, e As EventArgs) Handles txt_EtrierUz.TextChanged, txt_EtrierUy.TextChanged

        If lBuild Then Exit Sub
        lBuild = True
        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name

                Case Me.txt_EtrierUy.Name
                    MyEnrobage.Etriers_EnrobageY = Valeur
                Case Me.txt_EtrierUz.Name
                    MyEnrobage.Etriers_EnrobageZ = Valeur

            End Select

            Me.img_Enrobage.Invalidate()
        End If

        lBuild = False
    End Sub

    ''' <summary>
    ''' Vérification de la saisie des paramètres
    ''' </summary>
    Private Function VerificationSaisie(MyTxt As TextBox, ByRef ValeurUI As Decimal) As Boolean

        '-- Déclaration - Initialisation

        Dim lOk As Boolean = True
        ErrorProvider.Clear()

        Dim iErreur As Integer
        Dim ValMin, ValMax As Decimal
        Dim lValMax As Boolean = True
        Dim kUnit As Decimal = LogicielInfo.Transfert_Longueur(LogicielOptions.IndUnitDimension)

        Const ENROBATRIERMIN As Decimal = 0.01
        Const ENROBATRIERMAX As Decimal = 0.05

        Select Case MyTxt.Name
            Case Me.txt_EtrierUy.Name

                ValMin = ENROBATRIERMIN / kUnit
                ValMax = ENROBATRIERMAX / kUnit

            Case Me.txt_EtrierUz.Name

                ValMin = ENROBATRIERMIN / kUnit
                ValMax = ENROBATRIERMAX / kUnit

            Case Me.txt_zArma.Name

                ValMin = -MyProjet.Poutres(MyProjet.IndEnCours).Section.zPositionLitArmaEnrobage(2) _
                       + 2 * Me.DiametreArmaInf(Me.DiametreArmaInf.GetUpperBound(0))
                ValMax = -MyProjet.Poutres(MyProjet.IndEnCours).Section.zPositionLitArmaEnrobage(0) _
                       - 2 * Me.DiametreArmaInf(Me.DiametreArmaInf.GetUpperBound(0))
                ValMin = ValMin / kUnit
                ValMax = ValMax / kUnit

        End Select
        iErreur = ValideSaisieNombre(MyTxt.Text, True, ValMin, lValMax, ValMax)

        If iErreur <> 0 Then
            NotifieErreurSaisie(iErreur, MyTxt, ErrorProvider, ValMin, ValMax)
        Else
            ValeurUI = TraiteReal(MyTxt.Text) * kUnit
            ErrorProvider.Clear()
        End If

        lOk = (iErreur = 0)
        Return lOk

    End Function

    Private Sub comboEtriersChanged(sender As Object, e As EventArgs) Handles cmb_TypeEtriers.SelectedIndexChanged, cmb_DiametreEtriers.SelectedIndexChanged
        If lBuild Then Exit Sub
        Dim Indice As Integer

        Select Case sender.name
            Case Me.cmb_DiametreEtriers.Name
                Indice = Me.cmb_DiametreEtriers.SelectedIndex
                MyEnrobage.Etriers_Phi = DiametreEtriers(Indice)
            Case Me.cmb_TypeEtriers.Name
                Select Case Me.cmb_TypeEtriers.SelectedIndex
                    Case 0 : MyEnrobage.Etriers_Type = cls_Enrobage_Partiel.EnuTypeEtriers.Cadre
                    Case 1 : MyEnrobage.Etriers_Type = cls_Enrobage_Partiel.EnuTypeEtriers.EtrierSoude
                    Case 2 : MyEnrobage.Etriers_Type = cls_Enrobage_Partiel.EnuTypeEtriers.CadreTraversant
                End Select

        End Select

        Me.img_Enrobage.Invalidate()
    End Sub

    Private Function IndiceLitAffiche() As Integer
        Dim iArma As Integer
        Select Case LitArmaEnCours
            Case Enu_LitArmaEnCours.Inferieur : iArma = 0
            Case Enu_LitArmaEnCours.Intermediaire : iArma = 1
            Case Enu_LitArmaEnCours.Superieur : iArma = 2
        End Select
        Return iArma
    End Function

    Private Sub ActivationBarres(sender As Object, e As EventArgs) Handles chk_ActiveInt.CheckedChanged, chk_ActiveExt.CheckedChanged
        If lBuild Then Exit Sub
        Dim iArma As Integer = IndiceLitAffiche()

        Select Case sender.name
            Case Me.chk_ActiveExt.Name
                MyEnrobage.LitArma(iArma).lActiveExt = Me.chk_ActiveExt.Checked
            Case Me.chk_ActiveInt.Name
                MyEnrobage.LitArma(iArma).lActiveInt = Me.chk_ActiveInt.Checked
        End Select

        Me.img_Enrobage.Invalidate()

    End Sub



#End Region

#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles img_Fck.Paint, img_Ecm.Paint, img_Fy.Paint, img_zArma.Paint, img_As.Paint, img_uz.Paint, img_ux.Paint, img_PhiEtrier.Paint, img_Bf.Paint, img_BcX.Paint, img_Bc.Paint

        '--> Déclarations

        Dim sWI As Single = sender.Width
        Dim sHI As Single = sender.Height
        Dim xStart As Single = sWI * 0.95

        Dim strIndice As String = Nothing
        Dim strSymbol As String = Nothing
        Dim lGrec, lIndice, lEgal As Boolean
        Dim xPen As Single = xStart
        Dim hCar As Single = e.Graphics.MeasureString("X", FontSymbolNormal).Height
        Dim hIndice As Single = hCar / 2
        Dim yPen As Single = (sHI / 2 - hCar) / 2 + sHI * 0.15

        '--> Initialisation

        lIndice = False
        lGrec = False
        lEgal = True
        Select Case sender.name
            Case Me.img_zArma.Name
                strSymbol = "z"
                Select Case LitArmaEnCours
                    Case Enu_LitArmaEnCours.Inferieur : strIndice = "s,inf"
                    Case Enu_LitArmaEnCours.Superieur : strIndice = "s,sup"
                    Case Enu_LitArmaEnCours.Intermediaire : strIndice = "s,mid"
                End Select

            Case Me.img_As.Name
                strSymbol = "A"

                Select Case LitArmaEnCours
                    Case Enu_LitArmaEnCours.Inferieur : strIndice = "s,inf"
                    Case Enu_LitArmaEnCours.Superieur : strIndice = "s,sup"
                    Case Enu_LitArmaEnCours.Intermediaire : strIndice = "s,mid"
                End Select

            Case Me.img_Fy.Name
                strSymbol = "f"
                strIndice = "sk"
            Case Me.img_Ecm.Name
                strSymbol = "E"
                strIndice = "cm"
            Case Me.img_Fck.Name
                strSymbol = "f"
                strIndice = "ck"
            Case Me.img_Bc.Name, Me.img_BcX.Name
                strSymbol = "b"
                strIndice = "c"
            Case Me.img_Bf.Name
                strSymbol = "b"
                strIndice = "f"
                lEgal = False
            Case Me.img_PhiEtrier.Name
                strSymbol = "j"
                strIndice = "e"
                lGrec = True
            Case Me.img_ux.Name
                strSymbol = "u"
                strIndice = "y"
            Case Me.img_uz.Name
                strSymbol = "u"
                strIndice = "z"
                'Case Me.img_PhiA.Name
                '    strSymbol = "j"
                '    strIndice = "a"
                '    lGrec = True

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_AlignementH.Droite,
                   FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

End Class