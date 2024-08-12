Public Class Frm_OptionsLogicielDirectories


#Region " Variables et constantes "

    Const BALISE As String = "OPTSOFTDIRECTORIES"
    Dim MyPenBord As Pen = New Pen(Color.Gray)

    Dim lBuild As Boolean

#End Region

#Region "===OUVERTURE==="

    Public Sub InitialiseFrm()
        lBuild = True
        GestionLangue(Frm_OptionsLogiciel.BlocLangues(BALISE))
        GestionStyle()
        'GestionUnites()
        'IntialiseObjets()
        AfficherOptionsEnCours()

        lBuild = False
    End Sub

    Private Sub GestionLangue(ByVal MyBloc As Dictionary(Of String, String))
        Try

            Me.lbl_Repertoires.Text = MyBloc("DIRECTORIES")

            Me.lbl_Install.Text = MyBloc("INSTALL")
            Me.lbl_Configuration.Text = MyBloc("CONFIG")
            Me.lbl_Travail.Text = MyBloc("DEFAULTWORK")
            Me.lbl_TravailEnCours.Text = MyBloc("WORK")

            Me.lbl_OptionsRep.Text = MyBloc("DIROPTIONS")
            Me.opt_Default.Text = MyBloc("DEFAULT")
            Me.opt_Last.Text = MyBloc("LAST")

        Catch ex As Exception
            GestionErreurAffichageLangue(Me.Name, "GestionLangues")
            'MsgBox("Erreur affichage langue | Error display language", MsgBoxStyle.Critical, Me.Name & "/GestionLangue")
        Finally
        End Try
    End Sub

    Private Sub GestionStyle()

        Me.pan_General.Dock = DockStyle.Fill

        Me.lbl_Repertoires.BackColor = CouleurBackBandeaux
        Me.lbl_Repertoires.ForeColor = CouleurForeBandeaux

        Me.lbl_OptionsRep.BackColor = CouleurBackBandeaux
        Me.lbl_OptionsRep.ForeColor = CouleurForeBandeaux

    End Sub

    Private Sub AfficherOptionsEnCours()

        If Frm_OptionsLogiciel.pLocallDefaultRepW Then
            Me.opt_Default.Checked = True
        Else
            Me.opt_Last.Checked = True
        End If

    End Sub

#End Region

#Region "   Affichage des répertoires "

    Private Sub img_Configuration_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_Configuration.Paint
        AfficheRepertoire(LogicielRep.Config, e.Graphics, Me.img_Configuration.ClientRectangle.Width, Me.img_Configuration.ClientRectangle.Height, False)
    End Sub

    Private Sub AfficheRepertoire(ByVal Repertoire As String, ByVal MyGr As Graphics,
                                  ByVal sWI As Single, ByVal sHI As Single,
                                  ByVal lModifiable As Boolean)
        '-------------------------------------------------------------------------------------------
        '
        '   27/05/08 :  Création - Version 1.00
        '
        '-------------------------------------------------------------------------------------------
        '
        '   Affichage du répertoire de travail dans une picture box (pour controle de la longueur)
        '
        '-------------------------------------------------------------------------------------------
        '
        '   MyGr        [E] :   Graphics dans lequel on affiche
        '   sHI, sWI    [E] :   Dimensions du la zone d'affichage (picture box)
        '   lModifiable [E] :   Indique si champ modifiable par Utilisateur
        '
        '-------------------------------------------------------------------------------------------

        Dim Chaine As String
        Dim xChaine, yChaine As Single
        Const BORDURE As Single = 2
        Dim xDispo, Longueur As Single
        Dim MyFont As New Font(Me.lbl_Travail.Font, FontStyle.Regular)
        'Dim MyFont As New Font("Arial", 9)
        Dim Sep As String = "\"
        Dim iSep0, iSep As Integer

        '--[ Changement de couleur de fond pour les répertoires non modifiables

        If Not lModifiable Then
            Dim MyBrushFond As New SolidBrush(ColorFixe)
            MyGr.FillRectangle(MyBrushFond, 0, 0, sWI, sHI)
            MyBrushFond.Dispose()
        End If

        '--[ Tracé du bord

        MyGr.DrawLine(MyPenBord, 0, 0, 0, sHI)
        MyGr.DrawLine(MyPenBord, 0, sHI - 1, sWI, sHI - 1)
        MyGr.DrawLine(MyPenBord, 0, 0, sWI, 0)
        MyGr.DrawLine(MyPenBord, sWI - 1, 0, sWI - 1, sHI - 1)

        '--[ Affichage du nom du répertoire

        Chaine = Repertoire
        xChaine = BORDURE
        xDispo = sWI - 2 * BORDURE
        yChaine = (sHI - MyGr.MeasureString("\", MyFont).Height) / 2
        iSep0 = Chaine.IndexOf(Sep)
        iSep = iSep0

        Longueur = MyGr.MeasureString(Chaine, MyFont).Width

        Do While Longueur > xDispo

            iSep = Repertoire.IndexOf(Sep, iSep + 1)
            Chaine = Repertoire.Substring(0, iSep0) _
                   & Sep & "..." & Sep _
                   & Repertoire.Substring(iSep + 1)
            Longueur = MyGr.MeasureString(Chaine, MyFont).Width

        Loop

        MyGr.DrawString(Chaine, MyFont, Brushes.Black, xChaine, yChaine)

        MyFont.Dispose()
    End Sub

    Private Sub img_Travail_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_Travail.Paint
        AfficheRepertoire(Frm_OptionsLogiciel.pLocalRepWDefaut, e.Graphics, Me.img_Configuration.ClientRectangle.Width, Me.img_Configuration.ClientRectangle.Height, True)
    End Sub

    Private Sub img_Install_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles img_Install.Paint
        AfficheRepertoire(LogicielRep.Install, e.Graphics, Me.img_Configuration.ClientRectangle.Width, Me.img_Configuration.ClientRectangle.Height, False)
    End Sub

    Private Sub img_TravailEnCours_Paint(sender As Object, e As PaintEventArgs) Handles img_TravailEnCours.Paint
        AfficheRepertoire(LogicielRep.Travail, e.Graphics, Me.img_TravailEnCours.ClientRectangle.Width, Me.img_TravailEnCours.ClientRectangle.Height, False)
    End Sub


#End Region

#Region " Evènements saisie "

    Private Sub opt_Default_CheckedChanged(sender As Object, e As EventArgs) Handles opt_Last.CheckedChanged, opt_Default.CheckedChanged
        If lBuild Then Exit Sub

        Frm_OptionsLogiciel.pLocalRepWDefaut = Me.opt_Default.Checked

    End Sub

    Private Sub pan_Rep_Resize(sender As Object, e As EventArgs) Handles pan_Rep.Resize
        Me.img_Configuration.Invalidate()
        Me.img_Install.Invalidate()
        Me.img_Travail.Invalidate()
        Me.img_TravailEnCours.Invalidate()
    End Sub

#End Region

End Class