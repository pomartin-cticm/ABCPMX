Imports PMXMoteur2

Public Class Frm_PPHivoss

#Region " Variables "

    Dim lBuild As Boolean

    Dim fDamp, fFreq, fMass As Decimal

#End Region

#Region "===OUVERTURE==="

    Private Sub Frm_PPHivoss_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lBuild = True

        GestionStyle()
        GestionLangue()

        AfficherResultatsHivoss(MyProjet.Poutres(MyProjet.IndEnCours))

        lBuild = False

        Me.img_Hivoss.Invalidate()
    End Sub

    Private Sub GestionStyle()

        Me.Icon = Frm_PMX.Icon

        Me.lbl_Hivoss.BackColor = CouleurBackBandeaux
        Me.lbl_Hivoss.ForeColor = CouleurForeBandeaux

        Me.img_Hivoss.Dock = DockStyle.Fill

        Me.TLPan_PartieBasse.ColumnStyles(1).Width = 0
        Me.TLPan_PartieBasse.ColumnStyles(2).Width = 0

        Me.TLpan_AffichageCentral.ColumnStyles(0).Width = 0

    End Sub

    Private Sub GestionLangue()

        Me.Text = "Analyse Hivoss"
        Me.lbl_Hivoss.Text = "Résultats"

        Me.lbl_Masses.Text = "Masses"
        Me.lbl_Frequence.Text = "Frequency"
        Me.lbl_Periode.Text = "Period"
        Me.lbl_Resultats.Text = "Results"

        Me.lbl_MassTotale.Text = "Masse totale"
        Me.lbl_MassModal.Text = "Masse modale"

        Me.btn_OK.Text = "Close"

    End Sub

    Private Sub AfficherResultatsHivoss(MyBeam As cls_Poutre)

        '--> Déclarations 

        Dim AllFloorVibration As New Dictionary(Of Integer, cls_MethodHivoss.strHivossTable)
        Dim Frequency, ModalMass As Decimal
        Dim MasseProfil, FreqBeam, FreqDalle, PorteeDalle As Decimal
        Dim HResult As String = ""
        Dim HVal As Decimal
        Dim IndConfort As Integer
        Const kPC As Decimal = 100

        '--> Initialisations

        MyBeam.Hivoss.CalculAmortissement()

        MyBeam.Hivoss.ChargerValeursHivoss(AllFloorVibration)

        '--> Analyse modale

        MyBeam.Modal.Analyse(MyBeam, MyBeam.Hivoss.ratioQ, MyBeam.Hivoss.IndexQ)
        Frequency = MyBeam.Modal.Frequence

        ModalMass = MyBeam.Modal.MassTotal / 2              ' A MODIFIER ? pour les multispan

        If MyBeam.Hivoss.lFreqDalle And LogicielOptions.lExpert Then
            MasseProfil = MyBeam.Section.ProfilA.Aire * cls_Acier.RHOACIER
            PorteeDalle = MyBeam.PorteeDalle

            MyBeam.Dalle.FrequenceDalle(MyBeam.LongueurTravee(1), PorteeDalle, MyBeam.LargeurInfluence, MasseProfil, MyBeam.Param.GraviteG)
            FreqBeam = Frequency
            Frequency = CDec(1 / Math.Sqrt(1 / FreqBeam ^ 2 + 1 / FreqDalle ^ 2))
        End If

        Me.txt_Frequence.Text = GetStringInUnit(Frequency, Enu_TypeVariable.Frequence, 3, 1, False)

        Me.txt_MassModal.Text = GetStringInUnit(ModalMass, Enu_TypeVariable.SansType, 3, 0, False)

        ''--[ Calcul Hivoss

        MyBeam.Hivoss.CalculMethodHivoss(CInt(MyBeam.Hivoss.AmortiTotal_Dtot * kPC), Frequency, ModalMass, HResult, HVal)
        IndConfort = MyBeam.Hivoss.ConfortAssessment(HResult)

        fDamp = CInt(MyBeam.Hivoss.AmortiTotal_Dtot * kPC)
        fFreq = Frequency
        fMass = ModalMass

    End Sub

#End Region

#Region " Fermeture "


#End Region

#Region " Dessin "

    Private Sub img_Hivoss_Paint(sender As Object, e As PaintEventArgs) Handles img_Hivoss.Paint

        If lBuild Or (fDamp = 0) Then Exit Sub

        Dim sWiImg As Single = Me.img_Hivoss.ClientRectangle.Width
        Dim sHiImg As Single = Me.img_Hivoss.ClientRectangle.Height

        Const strDamp As String = "Amortissement"
        DessinCourbeHivoss(MyProjet.Poutres(MyProjet.IndEnCours).Hivoss, e.Graphics, 0, 0, sWiImg, sHiImg, fDamp, fFreq, fMass, strDamp)

    End Sub

#End Region

#Region " Evènements "

    Private Sub Frm_PPHivoss_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Me.img_Hivoss.Invalidate()
    End Sub

#End Region


End Class