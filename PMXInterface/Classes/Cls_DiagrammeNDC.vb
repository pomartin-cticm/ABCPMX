Public Class Cls_DiagrammeNDC

#Region "Attributs"
    Public indCasDeChargeNDC As Integer   'indique le cas de charge à afficher

    Public lDessDeformee As Boolean
    Public lDessMoment As Boolean
    Public lDessEffortT As Boolean
    Public lDessInerties As Boolean
    Public lDessNumeros As Boolean
    Public lDessCharges As Boolean
    Public lDessEchLocal As Boolean
    Public lDessValEnv As Boolean = True

    Public tab_fMin() As Decimal
    Public tab_fMax() As Decimal
    Public fMaxG As Decimal
    Public fMinG As Decimal

    Public tab_Mmin() As Decimal
    Public tab_iNodeMmin() As Decimal
    Public tab_Mmax() As Decimal
    Public tab_iNodeMmax() As Decimal
    Public tab_Vmin() As Decimal
    Public tab_iNodeVmin() As Decimal
    Public tab_Vmax() As Decimal
    Public tab_iNodeVmax() As Decimal
    Public MmaxG As Decimal
    Public MminG As Decimal
    Public VmaxG As Decimal
    Public VminG As Decimal

#End Region

#Region "Constructeur"

    Sub New()
        InitialiseVariablesLocales()
    End Sub
#End Region

#Region "Méthodes"

    Public Sub InitialiseVariablesLocales()

        Dim NbC As Integer = MyProjet.Poutres(MyProjet.IndEnCours).ChargesA.Count

        With Me
            ReDim .tab_fMax(NbC - 1)
            ReDim .tab_fMin(NbC - 1)
            ReDim .tab_Mmax(NbC - 1)
            ReDim .tab_Mmin(NbC - 1)
            ReDim .tab_iNodeMmax(NbC - 1)
            ReDim .tab_iNodeMmin(NbC - 1)
            ReDim .tab_Vmax(NbC - 1)
            ReDim .tab_Vmin(NbC - 1)
            ReDim .tab_iNodeVmax(NbC - 1)
            ReDim .tab_iNodeVmin(NbC - 1)

            For i As Integer = 0 To NbC - 1
                MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i).EnveloppesFleche(.tab_fMax(i), .tab_fMin(i))
                If i = 0 Then
                    .fMaxG = .tab_fMax(i)
                    .fMinG = .tab_fMin(i)
                Else
                    .fMaxG = Math.Max(.fMaxG, .tab_fMax(i))
                    .fMinG = Math.Min(.fMinG, .tab_fMin(i))
                End If

                MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i).EnveloppesMoments(.tab_Mmax(i), .tab_iNodeMmax(i), .tab_Mmin(i), .tab_iNodeMmin(i))
                If i = 0 Then
                    .MmaxG = .tab_Mmax(i)
                    .MminG = .tab_Mmin(i)
                Else
                    .MmaxG = Math.Max(.MmaxG, .tab_Mmax(i))
                    .MminG = Math.Min(.MminG, .tab_Mmin(i))
                End If

                MyProjet.Poutres(MyProjet.IndEnCours).ChargesA(i).EnveloppesTranchants(.tab_Vmax(i), .tab_iNodeVmax(i), .tab_Vmin(i), .tab_iNodeVmin(i))
                If i = 0 Then
                    .VmaxG = .tab_Vmax(i)
                    .VminG = .tab_Vmin(i)
                Else
                    .VmaxG = Math.Max(.VmaxG, .tab_Vmax(i))
                    .VminG = Math.Min(.VminG, .tab_Vmin(i))
                End If

            Next

        End With

    End Sub

#End Region
End Class
