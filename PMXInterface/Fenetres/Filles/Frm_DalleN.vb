Imports PMXMoteur2
Imports System.IO
Imports System.Drawing.Drawing2D

Public Class Frm_DalleN


#Region " Variables locales "

    Dim lBuild As Boolean = True

    Dim strType(2) As String

    Dim ClasseBeton() As String = Cls_Beton.TabClasseBeton
    Dim ClasseAcierArma() As String = Cls_AcierArmature.tabClasseAcierArma

    Public MyDalleLoc As New Cls_Dalle
    Dim COULEURTXTREADONLY As Color = SystemColors.ControlDark
    Const kADJUST As Decimal = 0.95


#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_Basic_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitialiserFenetre()
    End Sub

    Public Sub InitialiserFenetre()
        lBuild = True
        GestionLangues()
        GestionStyle()
        GestionUnites()
        InitialisationVariablesLocales()
        PreparerFenetre()
        AfficherDalleEnCours()
        lBuild = False
    End Sub

    Private Sub GestionLangues()
        If File.Exists(LogicielFichiers.Langue) Then

            Dim Bloc As New Dictionary(Of String, String)
            Dim BlocLine As New Cls_LinesOfFile(LogicielFichiers.Langue, "#FRM_SLAB")
            BlocLine.CreationBloc(Bloc)

            Try

                '=== FENETRE =====================================================================

                Me.Text = Bloc("TITLE")
                Me.btn_OK.Text = Bloc("OK")
                Me.btn_Annuler.Text = Bloc("CANCEL")

                '=== GENERAL ======================================================================

                Me.lbl_General.Text = Bloc("GENERAL")
                strType(0) = Bloc("SOLIDSLAB")
                strType(1) = Bloc("COMPOSITESLAB")
                strType(2) = Bloc("PRECASTSLAB")

                '=== BETON ========================================================================

                Me.lbl_Beton.Text = Bloc("CONCRETE")

                '=== BAC ==========================================================================

                Me.lbl_Bac.Text = Bloc("SHEETING")
                Me.lbl_BacNom.Text = "Nom"
                Me.btn_ModifierBac.Text = "Modifier le bac"
                Me.lbl_BacOrientation.Text = "Orientation des nervures"
                Me.rdb_BacParallele.Text = "Parallèle"
                Me.rdb_BacPerpendiculaire.Text = "Perpendiculaire"

                Me.lbl_BacConfiguration.Text = "Configuration des nervures sur appui"

                Me.chk_BacPreperce.Text = "Bac prepercé"

                '=== ARMATURES ====================================================================

                Me.lbl_General.Text = Bloc("REBARS")

                '=== ACIER DES ARMATURES ==========================================================

                Me.lbl_Acier.Text = "Reinforcement steel"

            Catch ex As Exception
                MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
            Finally
                Bloc.Clear()
            End Try

        End If

    End Sub

    Private Sub GestionUnites()

        Me.etq_UnitDim2.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)
        Me.etq_UnitDim3.Text = LogicielInfo.Unit_Longueur(LogicielOptions.IndUnitDimension)

        Me.etq_UnitSigma1.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)
        Me.etq_UnitModule1.Text = LogicielInfo.Unit_ModulesY(LogicielOptions.IndUnitModulesY)
        Me.etq_UnitSigma2.Text = LogicielInfo.Unit_Contraintes(LogicielOptions.IndUnitContraintes)

    End Sub

    Private Sub InitialisationVariablesLocales()

        Cls_Dalle.DeepClone(MyProjet.Poutres(MyProjet.IndEnCours).Dalle, MyDalleLoc)

    End Sub

    Private Sub GestionStyle()
        Me.Icon = Frm_PMX.Icon

        Me.lbl_General.BackColor = CouleurBackBandeaux
        Me.lbl_General.ForeColor = CouleurForeBandeaux

        Me.lbl_Bac.BackColor = CouleurBackBandeaux
        Me.lbl_Bac.ForeColor = CouleurForeBandeaux
        Me.lbl_Beton.BackColor = CouleurBackBandeaux
        Me.lbl_Beton.ForeColor = CouleurForeBandeaux
        Me.lbl_Armatures.BackColor = CouleurBackBandeaux
        Me.lbl_Armatures.ForeColor = CouleurForeBandeaux
        Me.lbl_Acier.BackColor = CouleurBackBandeaux
        Me.lbl_Acier.ForeColor = CouleurForeBandeaux

        Me.txt_BacNom.Enabled = False
        Me.txt_BacNom.BackColor = COULEURTXTREADONLY

    End Sub

    Private Sub PreparerFenetre()

        Me.img_Dalle.Dock = DockStyle.Fill

        RemplirComboAvecTableau(Me.cmb_TypeDalle, strType)
        RemplirComboAvecTableau(Me.cmb_ClasseBetonEnrobage, ClasseBeton)
        RemplirComboAvecTableau(Me.cmb_Acier, ClasseAcierArma)

    End Sub

    Private Sub RemplirComboAvecTableau(MyCombo As ComboBox, tabValeurs() As String)
        MyCombo.Items.Clear()
        MyCombo.Items.AddRange(tabValeurs)
    End Sub

    Private Sub AfficherDalleEnCours()

        '--> Type de dalle

        Select Case MyDalleLoc.type
            Case Cls_Dalle.Enum_TypeDalle.Pleine
                Me.cmb_TypeDalle.SelectedIndex = 0
            Case Cls_Dalle.Enum_TypeDalle.Mixte
                Me.cmb_TypeDalle.SelectedIndex = 1
            Case Cls_Dalle.Enum_TypeDalle.Prefabriquee
                Me.cmb_TypeDalle.SelectedIndex = 2
        End Select

        '--> Epaisseur

        Me.txt_Hd.Text = GetStringNoUnit(MyDalleLoc.t_d, Enu_TypeVariable.Dimension)

        '--> Béton

        Dim Chaine As String
        Chaine = MyDalleLoc.beton.Classe
        If Me.ClasseBeton.Contains(Chaine) Then
            Me.cmb_ClasseBetonEnrobage.SelectedIndex = Array.IndexOf(Me.ClasseBeton, Chaine)
        Else
            Me.cmb_ClasseBetonEnrobage.SelectedIndex = 0
        End If

        MAJI_ProprietesBeton()

        '--> Bac

        Me.txt_BacNom.Text = MyDalleLoc.Bac.Etiquette

        '--> Acier

        Chaine = MyDalleLoc.AcierArmatures.Classe
        If Me.ClasseAcierArma.Contains(Chaine) Then
            Me.cmb_Acier.SelectedIndex = Array.IndexOf(Me.ClasseAcierArma, Chaine)
        Else
            Me.cmb_Acier.SelectedIndex = 0
        End If
        MAJI_ProprietesAcier()

    End Sub

    Private Sub MAJI_ProprietesBeton()

        MyDalleLoc.beton.Calcul_Proprietes()

        Me.txt_Fck.Text = GetStringNoUnit(MyDalleLoc.beton.Fck, Enu_TypeVariable.Contrainte)
        Me.txt_Ecm.Text = GetStringNoUnit(MyDalleLoc.beton.Ecm, Enu_TypeVariable.ModuleY)

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

            'MyProjet.Poutres(MyProjet.IndEnCours).EstValidee(iFRMslab)

            Me.Close()
        End If
    End Sub


    Private Function ValideSaisieFenetre() As Boolean
        Return True
    End Function

    Private Sub TransfertSaisie(ByRef lModif As Boolean)

        lModif = False

        '-- Dimensions ----------------------------------------------------------------------------------------------------

        If (MyDalleLoc.type <> MyProjet.Poutres(MyProjet.IndEnCours).Dalle.type) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.type = MyDalleLoc.type
        End If

        If (MyProjet.Poutres(MyProjet.IndEnCours).Dalle.t_d <> MyDalleLoc.t_d) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.t_d = MyDalleLoc.t_d
        End If

        '--> Acier des armatures

        If (MyProjet.Poutres(MyProjet.IndEnCours).Dalle.AcierArmatures.Classe <> MyDalleLoc.AcierArmatures.Classe) Then
            lModif = True
            MyProjet.Poutres(MyProjet.IndEnCours).Dalle.AcierArmatures.Classe = MyDalleLoc.AcierArmatures.Classe
        End If

    End Sub

#End Region

#Region " Dessins "

    Private Sub img_Bac_Paint(sender As Object, e As PaintEventArgs) Handles img_Bac.Paint
        DessineBacTout(e.Graphics, Me.img_Bac.ClientRectangle.Width, Me.ClientRectangle.Height, MyDalleLoc.Bac, True)
    End Sub

    Private Sub img_Dalle_Paint(sender As Object, e As PaintEventArgs) Handles img_Dalle.Paint

    End Sub

    '==== A METTRE DANS LE MODULE DESSIN ================================================================

    Public Sub DessineBacTout(ByRef myGr As Graphics, ByVal pWi As Single, ByVal pHi As Single, MyBac As Cls_Bac,
                              ByVal lTitre As Boolean,
                              ByVal Optional xLeft As Decimal = 0, ByVal Optional yTop As Decimal = 0)
        '-----------------------------------------------------------------------------------------------
        '   26/06/23 :  Version 1.00
        '-----------------------------------------------------------------------------------------------
        '   Dessin du Bac Acier
        '-----------------------------------------------------------------------------------------------
        '   myGr        [E] :   Graphics dans lequel on dessine
        '   Img         [E] :   Image dans laquelle on dessine
        '   sWi, sHi    [E] :   Largeur et hauteur de la zone de dessin
        '   xLeft, yTop [E] :   Position Gauche et Haute de la zone de dessin dans l'objet
        '   EpDalle     [E] :   Epaisseur de la dalle béton
        '   VariableBac [E] :   Parametre du bac sélectionné (pour affichage en rouge)
        '   nbOndes     [E] :   Nombre d'ondes sur lequel on représente le bac
        '   lCotation   [E] :   Indique si on met les cotations sur le dessin
        '   lCotEpTot   [E] :   Indique si cotation epaisseur bac+dalle
        '   lTitre      [E] :   Indique si affichage du titre du bac
        '   ParAff      [S] :   Paramètres d'Affichage
        '   lMemb       [E] :   Indique si on représente la semelle sup de la memb sup
        '   tfSup       [E] :   Epasseur semelle de la membrure superieure
        '   hMax        [E] :   Epaisseur maximale à considérer pour le dessin de la dalle
        '-----------------------------------------------------------------------------------------------

        '--> Declarations

        Dim MyParAff As Struc_Affichage

        Dim ColorPen As Color = Color.Blue
        Dim ColorRedPen As Color = Color.Red

        Dim myBrushBac As New LinearGradientBrush(New PointF(xLeft, yTop), New PointF(xLeft + pWi, yTop + pWi), Color.LightGray, Color.DarkGray)
        Dim MyPenBrush As New SolidBrush(ColorPen)
        Dim MyPenRedBrush As New SolidBrush(ColorRedPen)
        Dim MyPen As New Pen(ColorPen)
        Dim MyPenRed As New Pen(ColorRedPen)
        Dim MyFontNormal As Font = FontBase

        Dim xMin, yMin, xMax, yMax As Double
        Dim dCar As Double

        Dim lRaidSup As Boolean

        Dim xPts() As Single = Nothing
        Dim yPts() As Single = Nothing
        Dim nbPts As Integer
        Dim nbOndes As Integer = 5

        Dim lUn As Boolean = False

        '--> Initialisation

        lRaidSup = MyBac.HasRaidisseurSup
        If lUn Then
            dCar = Math.Sqrt(MyBac.h_p ^ 2 + MyBac.e_p ^ 2) / 16
        Else
            dCar = (MyBac.e_p + MyBac.b_b) / 2
        End If

        '--> Preparation de la zone d'affichage - Calcul de ParAff

        If lUn Then
            xMin = -MyBac.e_p / 2
            xMax = MyBac.e_p / 2
        Else
            xMin = 0
            xMax = MyBac.LargeurModule
        End If
        'nbOndes = Math.Floor(MyBac.LargeurModule / MyBac.e_p)
        yMin = 0
        yMax = MyBac.h_p

        ParametresAffichage(MyParAff, xMin, yMin, xMax - xMin, yMax - yMin, pWi, pHi, xLeft, yTop, kADJUST)

        '--> Calcul des points du pourtour de la dalle

        If lRaidSup Then
            'MyBac.PrepareContourDalleBacRaidi(nbOndes, EpDalle, xPts, yPts, nbPts)
        Else
            MyBac.PrepareContourBacSimpleSeul(xPts, yPts, nbPts)
            'MyBac.PrepareContourBacSimpleSeul2(xPts, yPts, nbPts, lUn)

        End If

        '--> Remplissage contour

        'ContourZone(myGr, New Pen(BlueAM), xPts, yPts, nbPts, MyParAff, True)

        If lUn Then
            RemplirZone(myGr, myBrushBac, xPts, yPts, nbPts, MyParAff, True)
        Else
            ContourZone(myGr, New Pen(BlueAM), xPts, yPts, nbPts, MyParAff, False)
        End If

        ''--> Liberation des Font, Pen et Brush

        'myBrushDalle.Dispose()
        'MyPenBrush.Dispose()
        'MyPen.Dispose()
        'MyPenRedBrush.Dispose()
        'MyPenRed.Dispose()

    End Sub

#End Region

#Region " Evènements "

    Private Sub btn_ModifierBac_Click(sender As Object, e As EventArgs) Handles btn_ModifierBac.Click

        Frm_BacN.ShowDialog()


    End Sub

#End Region

#Region " Evènements saisie "

    Private Sub cmb_TypeDalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_TypeDalle.SelectedIndexChanged
        If lBuild Then Exit Sub

        Select Case Me.cmb_TypeDalle.SelectedIndex
            Case 0 : MyDalleLoc.type = Cls_Dalle.Enum_TypeDalle.Pleine
            Case 1 : MyDalleLoc.type = Cls_Dalle.Enum_TypeDalle.Mixte
            Case 2 : MyDalleLoc.type = Cls_Dalle.Enum_TypeDalle.Prefabriquee
        End Select

        Me.img_Dalle.Invalidate()
    End Sub

    Private Sub SaisieTextChanged(sender As Object, e As EventArgs) Handles txt_Hd.TextChanged

        If lBuild Then Exit Sub
        lBuild = True
        Dim Valeur As Decimal

        If VerificationSaisie(sender, Valeur) Then

            Select Case sender.name

                Case Me.txt_Hd.Name
                    MyDalleLoc.t_d = Valeur

            End Select

            Me.img_Dalle.Invalidate()
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

        Const TDMAXI As Decimal = 0.5
        Const TDMINI As Decimal = 0.05

        Select Case MyTxt.Name
            Case Me.txt_Hd.Name

                ValMin = TDMINI / kUnit
                ValMax = TDMAXI / kUnit

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

    Private Sub cmb_Acier_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Acier.SelectedIndexChanged
        If lBuild Then Exit Sub
        MyDalleLoc.AcierArmatures.Classe = Me.ClasseAcierArma(Me.cmb_Acier.SelectedIndex)
        MAJI_ProprietesAcier()

        Me.img_Dalle.Invalidate()
    End Sub

    Private Sub MAJI_ProprietesAcier()
        MyDalleLoc.AcierArmatures.MAJProprietes()
        Me.txt_Fsk.Text = GetStringNoUnit(MyDalleLoc.AcierArmatures.FsK, Enu_TypeVariable.Contrainte)
    End Sub


#End Region

#Region " Dessins symboles "

    Private Sub PaintSymbol(sender As Object, e As PaintEventArgs) Handles Img_Hh.Paint, Img_Hd.Paint, img_Fy.Paint, img_Fck.Paint, img_Ecm.Paint

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

            Case Me.img_Fy.Name
                strSymbol = "f"
                strIndice = "sk"
            Case Me.img_Ecm.Name
                strSymbol = "E"
                strIndice = "cm"
            Case Me.img_Fck.Name
                strSymbol = "f"
                strIndice = "ck"
            Case Me.Img_Hd.Name
                strSymbol = "t"
                strIndice = "d"

        End Select

        '--> Dessin

        DrawSymbol(e.Graphics, Brushes.Black, strSymbol, strIndice, xPen, yPen, lGrec, lIndice, Enu_Alignement.Gauche,
           FontSymbolNormal, FontSymbolGrec, FontSymbolIndice, 1.0!, lEgal)

    End Sub

#End Region

End Class