Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Data
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports NTSInformatica.CLN__STD


Namespace NTSInformatica
    Public Class CLHCGDCST
        Inherits CLDCGDCST
        ' Methods



        Public Overridable Function splticonto(ByVal lconto As Integer) As Integer
            Dim spl_conto As String = ""
            Dim cconto As Integer
            spl_conto = Right(NTSCStr(lconto), 4)
            If Left(NTSCStr(lconto), 3) = "901" Then
                cconto = NTSCInt("904" & spl_conto)
            ElseIf Left(NTSCStr(lconto), 3) = "904" Then
                cconto = NTSCInt("901" & spl_conto)
            End If
            Return cconto
        End Function


        Public Overrides Function BloccaCliente(strDitta As String, lConto As Integer, strTipoBlocco As String) As Boolean
            Return MyBase.BloccaCliente(strDitta, lConto, strTipoBlocco)
        End Function
        Public Overrides Function CalcolaSaldoPrinoteRD(ByVal strDitta As String, ByVal lConto As Integer, ByRef dSaldoRD As Decimal, ByVal nEscomp As Integer, ByVal strData As String, ByRef bInsoluti As Boolean) As Decimal
            Dim lconto2 As Integer
            lconto2 = splticonto(lConto)
            Return MyBase.CalcolaSaldoPrinoteRD(strDitta, lConto, dSaldoRD, nEscomp, strData, bInsoluti) + MyBase.CalcolaSaldoPrinoteRD(strDitta, lconto2, dSaldoRD, nEscomp, strData, bInsoluti)
        End Function
        'Public Overrides Function GetBolleDaFatturare(ByVal strDitta As String, ByVal lConto As Integer, ByVal strTipoConto As String, ByVal strData As String) As Decimal
        '    Dim num2 As Decimal
        '    num2 = MyBase.GetBolleDaFatturare(strDitta, lConto, strTipoConto, strData)
        '    Return num2
        'End Function

        'Public Overrides Function GetEmissioneEffetti(strDitta As String, lConto As Integer, bEseguitaApProvvisoria As Boolean, strData As String, nEscomp As Integer, lControp As Integer) As Decimal
        '    Return MyBase.GetEmissioneEffetti(strDitta, lConto, bEseguitaApProvvisoria, strData, nEscomp, lControp)
        'End Function

        Public Overrides Function GetFatturato(strDitta As String, lConto As Integer, nEscomp As Integer, strDatini As String, strDatfin As String) As Decimal
            Dim lconto2 As Integer
            lconto2 = splticonto(lConto)
            Return MyBase.GetFatturato(strDitta, lConto, nEscomp, strDatini, strDatfin) + MyBase.GetFatturato(strDitta, lconto2, nEscomp, strDatini, strDatfin)
        End Function

        'Public Overrides Function GetFattureNonContab(strDitta As String, lConto As Integer, strTipoConto As String, strData As String) As Decimal
        '    Return MyBase.GetFattureNonContab(strDitta, lConto, strTipoConto, strData)
        'End Function

        'Public Overrides Function GetFattureNonContab(strDitta As String, lConto As Integer, strTipoConto As String, strData As String, bDettaglio As Boolean, ByRef dsOut As DataSet) As Decimal
        '    Return MyBase.GetFattureNonContab(strDitta, lConto, strTipoConto, strData, bDettaglio, dsOut)
        'End Function

        Public Overrides Function GetInsolutiTot(strDitta As String, lConto As Integer, ByRef lNumeroInsoluti As Integer, strData As String) As Decimal
            Dim lconto2 As Integer
            lconto2 = splticonto(lConto)
            Return MyBase.GetInsolutiTot(strDitta, lConto, lNumeroInsoluti, strData) + MyBase.GetInsolutiTot(strDitta, lconto2, lNumeroInsoluti, strData)
        End Function

        Public Overrides Function GetInsolutiTot(strDitta As String, lConto As Integer, ByRef lNumeroInsoluti As Integer, strData As String, bDettaglio As Boolean, ByRef dsOut As DataSet) As Decimal
            Dim lconto2 As Integer
            lconto2 = splticonto(lConto)
            Return MyBase.GetInsolutiTot(strDitta, lConto, lNumeroInsoluti, strData, bDettaglio, dsOut) + MyBase.GetInsolutiTot(strDitta, lconto2, lNumeroInsoluti, strData, bDettaglio, dsOut)
        End Function

        Public Overrides Function GetRBrischio(strDitta As String, lConto As Integer, lConeff As Integer, strData As String, lGiorni As Integer) As Decimal
            Dim lconto2 As Integer
            lconto2 = splticonto(lConto)
            Return MyBase.GetRBrischio(strDitta, lConto, lConeff, strData, lGiorni) + MyBase.GetRBrischio(strDitta, lconto2, lConeff, strData, lGiorni)
        End Function


        'Public Overrides Function GetRDscadute(strDitta As String, lConto As Integer, bCalcolaSaldoRDScadute As Boolean, strData As String) As Decimal
        '    Return MyBase.GetRDscadute(strDitta, lConto, bCalcolaSaldoRDScadute, strData)
        'End Function

        'Public Overrides Function GetRDscadute(strDitta As String, lConto As Integer, bCalcolaSaldoRDScadute As Boolean, strData As String, bDettaglio As Boolean, ByRef dsOut As DataSet) As Decimal
        '    Return MyBase.GetRDscadute(strDitta, lConto, bCalcolaSaldoRDScadute, strData, bDettaglio, dsOut)
        'End Function

        Public Overrides Function GetRimesseDiretteNoDistinta(strDitta As String, lConto As Integer) As Decimal
            Dim lconto2 As Integer
            lconto2 = splticonto(lConto)
            Return MyBase.GetRimesseDiretteNoDistinta(strDitta, lConto) + MyBase.GetRimesseDiretteNoDistinta(strDitta, lconto2)
        End Function
        Public Overrides Function GetRBrischio(strDitta As String, lConto As Integer, lConeff As Integer, strData As String, lGiorni As Integer, bDettaglio As Boolean, ByRef dsOut As DataSet) As Decimal
            Dim lconto2 As Integer
            lconto2 = splticonto(lConto)
            Return MyBase.GetRBrischio(strDitta, lConto, lConeff, strData, lGiorni, bDettaglio, dsOut) + MyBase.GetRBrischio(strDitta, lconto2, lConeff, strData, lGiorni, bDettaglio, dsOut)
        End Function

        Public Overrides Function GetSaldocontabile(strDitta As String, lConto As Integer, bEseguitaApProvvisoria As Boolean, strData As String, nEscomp As Integer) As Decimal
            Dim lconto2 As Integer
            lconto2 = splticonto(lConto)
            Return MyBase.GetSaldocontabile(strDitta, lConto, bEseguitaApProvvisoria, strData, nEscomp) + MyBase.GetSaldocontabile(strDitta, lconto2, bEseguitaApProvvisoria, strData, nEscomp)
        End Function


    End Class
End Namespace

