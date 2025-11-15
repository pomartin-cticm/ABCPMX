Public Class cls_VerifFeuSlimAcier


#Region " Declaration "

    Public Shared TimeSteps() As Decimal = {30, 60, 90, 120, 180}

#End Region

#Region " Attributs "

    Private NbStep As Integer                           ' Nombre d'items dans le tableau TimeSteps
    Public RStep As Integer                             ' Indice du dernier pas de calcul de la table TimeStep pour laquelle tous les critères sont OK

    Private ModelNum As cls_EchauffementSlimFEM         ' Modèle numérique pour le calcul de l'échauffement d'une slim floor

#End Region

#Region " Constructeurs et Initialisation "

    Public Sub New()
        Me.NbStep = cls_VerifFeuSlimAcier.TimeSteps.GetUpperBound(0) + 1
        Me.RStep = -1
    End Sub

#End Region

#Region " GENERAL "

    Public Sub Z_VerifFeu(myBeam As cls_Poutre)


        Echauffement_SlimAcier(myBeam)


    End Sub

#End Region



#Region " Echauffement "

    Private Sub Echauffement_SlimAcier(myBeam As cls_Poutre)

        '--( Déclaration des variables

        Dim iSTep As Integer
        Dim Maillage As New cls_MaillageSlimFloor
        Dim bEffG, bEffD, bApp As Decimal

        '--( Initialisation des variables

        If myBeam.lIntermediaire Then
            bEffG = myBeam.EntraxeD1 / 2
        Else
            bEffG = myBeam.EntraxeD1
        End If
        bEffD = myBeam.EntraxeD2 / 2
        bApp = 0.05

        '--( Construction du modèle numérique

        Me.ModelNum = New cls_EchauffementSlimFEM

        Maillage.Creation_maillage_2D_poutre_plancher_mince(myBeam.Section.ProfilA, myBeam.Dalle, myBeam.ParamFeu, bEffG, bEffD, myBeam.lIntermediaire, bApp)

        '--( Initialisation des températures 

        Maillage.InitialiseTemp(myBeam.ParamFeu.TempRef)

        '--( Boucle sur TimeSteps

        For iSTep = 0 To Me.NbStep - 1



        Next

    End Sub


#End Region




End Class
