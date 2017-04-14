Imports System
#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class CLEMGSTBO
  Inherits CLE__BASE

#Region "Moduli"
  Private Moduli_P As Integer = bsModMG + bsModVE
  Private ModuliExt_P As Integer = bsModExtMGE
  Private ModuliSup_P As Integer = 0
  Private ModuliSupExt_P As Integer = 0
  Private ModuliPtn_P As Integer = 0
  Private ModuliPtnExt_P As Integer = 0

  Public ReadOnly Property Moduli() As Integer
    Get
      Return Moduli_P
    End Get
  End Property
  Public ReadOnly Property ModuliExt() As Integer
    Get
      Return ModuliExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliSup() As Integer
    Get
      Return ModuliSup_P
    End Get
  End Property
  Public ReadOnly Property ModuliSupExt() As Integer
    Get
      Return ModuliSupExt_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtn() As Integer
    Get
      Return ModuliPtn_P
    End Get
  End Property
  Public ReadOnly Property ModuliPtnExt() As Integer
    Get
      Return ModuliPtnExt_P
    End Get
  End Property
#End Region

#Region "Variabili"
  Public oCldStbo As CLDMGSTBO

  Public bModTCO As Boolean
  Public bUsaKeymag As Boolean = False
  'Modulo CRM
  Public bModuloCRM As Boolean
  Public bAmm As Boolean = True
  Public strAccvis, strAccmod, strRegvis, strRegmod As String
  Public bIsCRMUser As Boolean
  Public lCodorgaOperat As Integer
  Public bIs15 As Boolean = False
  Public nCodcageAccdito As Integer = 0
  Public lIITtsubqcrm As Integer = 0
  
#End Region

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGSTBO"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldStbo = CType(MyBase.ocldBase, CLDMGSTBO)
    oCldStbo.Init(oApp)
    Return True
  End Function

  Public Overridable Function edTipobf_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As DataTable = New DataTable
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldStbo.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABTPBF", "N", strTmp, dttTmp) Then
          strErr = oApp.Tr(Me, 128641511137031250, "Tipo bolla/fattura inesistente.")
          Return False
        Else
          strDescr = strTmp
        End If
      Else
        strDescr = ""
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function edCoddest_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String, ByRef strConto As String) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As DataTable = New DataTable
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldStbo.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "DESTDIV", "N", strTmp, dttTmp, strConto) Then
          strErr = oApp.Tr(Me, 128641511414843750, "Destinazione inesistente.")
          Return False
        Else
          strDescr = strTmp
        End If
      Else
        strDescr = ""
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function edCodagen_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As DataTable = New DataTable
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldStbo.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABCAGE", "N", strTmp, dttTmp) Then
          strErr = oApp.Tr(Me, 128535226253309856, "Agente inesistente.")
          Return False
        Else
          strDescr = strTmp
        End If
      Else
        strDescr = ""
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function edCodcfam_Validated(ByVal strCod As String, ByRef strDescr As String, ByRef strErr As String) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As DataTable = New DataTable
    Try
      If Not NTSCStr(strCod) = "" Then
        If Not oCldStbo.ValCodiceDb(NTSCStr(strCod), strDittaCorrente, "TABCFAM", "S", strTmp, dttTmp) Then
          strErr = oApp.Tr(Me, 128535229293391530, "Codice linea/famiglia inesistente.")
          Return False
        Else
          strDescr = strTmp
        End If
      Else
        strDescr = ""
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function edCodstag_Validated(ByVal nCod As Integer, ByRef strDescr As String, ByRef strErr As String) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As DataTable = New DataTable
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldStbo.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABSTAG", "N", strTmp, dttTmp) Then
          strErr = oApp.Tr(Me, 128535232323476532, "Codice stagione inesistente.")
          Return False
        Else
          strDescr = strTmp
        End If
      Else
        strDescr = ""
      End If

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function
  Public Overridable Function edCodlsel_Validated(ByVal nCod As Integer, ByRef strDescr As String, _
    ByRef strErr As String) As Boolean
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(nCod) = 0 Then
        strDescr = ""
      Else
        If oCldStbo.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABLSEL", "N", "", dttTmp) = False Then
          strErr = oApp.Tr(Me, 130374684158774630, "Lista selezionata inesistente.")
          Return False
        Else
          If NTSCStr(dttTmp.Rows(0)!tb_tipocl).ToUpper <> "C" Then
            strErr = oApp.Tr(Me, 130374699844215751, "Atenzione!" & _
              "La lista selezionata indicata deve essere di tipo 'Clienti/Fornitori'.")
            Return False
          Else
            strDescr = NTSCStr(dttTmp.Rows(0)!tb_deslsel)
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Function

  Public Overridable Function ApriDettaglio(ByVal nPosition As Integer, ByVal strDettaglioTesta As String, ByVal strDettaglioRighe As String) As Boolean
    Try
      If Not dsShared Is Nothing Then
        If Not dsShared.Tables(strDettaglioTesta) Is Nothing Then
          dsShared.Tables.Remove(strDettaglioTesta)
          dsShared.AcceptChanges()
        End If
      End If
      If Not dsShared Is Nothing Then
        If Not dsShared.Tables(strDettaglioRighe) Is Nothing Then
          dsShared.Tables.Remove(strDettaglioRighe)
          dsShared.AcceptChanges()
        End If
      End If
      Return oCldStbo.ApriDettaglio(strDittaCorrente, strNomeTabella, dsShared, nPosition, strDettaglioTesta, strDettaglioRighe)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CheckReports(ByVal nDaconto As Integer, ByVal nAconto As Integer, _
    ByVal nAnno As Integer, ByVal strSerie As String, _
    ByVal dateDatini As Date, ByVal dateDatfin As Date, _
    ByVal nDanumdoc As Integer, ByVal nAnumdoc As Integer, _
    ByVal nCommecaini As Integer, ByVal nCommecafin As Integer, _
    ByVal bSeldocumenti As Boolean, ByVal bNoteprel As Boolean, _
    ByVal strTipork As String, _
    ByVal nTipobf As Integer, ByVal nCoddest As Integer, _
    ByVal nCodagen As Integer, ByVal nCodagen2 As Integer, _
    ByVal bBolleFatturateEntrambe As Boolean, ByVal opBolleFatturate As Boolean, _
    ByVal bVistatiEntrambi As Boolean, ByVal bVistati As Boolean, _
    ByVal bFlcontEntrambe As Boolean, ByVal bFlcont As Boolean, _
    ByVal strCodcfam As String, ByVal bSelAnnoStag As Boolean, _
    ByVal nAnnotco As Integer, ByVal nCodstag As Integer, _
    ByVal strQuery As String, ByVal bFlevas As Boolean, _
    ByVal nTippaga As Integer, _
    ByVal nTiporeport As Integer, ByVal bCaricaDatiPerGriglia As Boolean) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return CheckReports(nDaconto, nAconto, nAnno, strSerie, dateDatini, dateDatfin, nDanumdoc, nAnumdoc, _
        nCommecaini, nCommecafin, bSeldocumenti, bNoteprel, strTipork, nTipobf, nCoddest, nCodagen, nCodagen2, _
        bBolleFatturateEntrambe, opBolleFatturate, bVistatiEntrambi, bVistati, bFlcontEntrambe, bFlcont, _
        strCodcfam, bSelAnnoStag, nAnnotco, nCodstag, strQuery, bFlevas, nTippaga, nTiporeport, bCaricaDatiPerGriglia, _
        0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function CheckReports(ByVal nDaconto As Integer, ByVal nAconto As Integer, _
    ByVal nAnno As Integer, ByVal strSerie As String, _
    ByVal dateDatini As Date, ByVal dateDatfin As Date, _
    ByVal nDanumdoc As Integer, ByVal nAnumdoc As Integer, _
    ByVal nCommecaini As Integer, ByVal nCommecafin As Integer, _
    ByVal bSeldocumenti As Boolean, ByVal bNoteprel As Boolean, _
    ByVal strTipork As String, _
    ByVal nTipobf As Integer, ByVal nCoddest As Integer, _
    ByVal nCodagen As Integer, ByVal nCodagen2 As Integer, _
    ByVal bBolleFatturateEntrambe As Boolean, ByVal opBolleFatturate As Boolean, _
    ByVal bVistatiEntrambi As Boolean, ByVal bVistati As Boolean, _
    ByVal bFlcontEntrambe As Boolean, ByVal bFlcont As Boolean, _
    ByVal strCodcfam As String, ByVal bSelAnnoStag As Boolean, _
    ByVal nAnnotco As Integer, ByVal nCodstag As Integer, _
    ByVal strQuery As String, ByVal bFlevas As Boolean, _
    ByVal nTippaga As Integer, _
    ByVal nTiporeport As Integer, ByVal bCaricaDatiPerGriglia As Boolean, ByVal nCodlsel As Integer) As Boolean
    Dim bRet As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nDaconto, nAconto, nAnno, strSerie, dateDatini, dateDatfin, _
        nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, bSeldocumenti, bNoteprel, strTipork, nTipobf, nCoddest, nCodagen, _
        nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, bVistatiEntrambi, bVistati, bFlcontEntrambe, bFlcont, _
        strCodcfam, bSelAnnoStag, nAnnotco, nCodstag, strQuery, bFlevas, nTippaga, nTiporeport, bCaricaDatiPerGriglia, _
        nCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (bSeldocumenti = True) And (bNoteprel = True) Then
        strTipork = "W"
        strNomeTabella = "testprb"
      Else
        strNomeTabella = "testmag"
      End If
      '--------------------------------------------------------------------------------------------------------------
      If Not (bSeldocumenti = True And bNoteprel = True And bFlevas = True) Then bFlevas = False
      '--------------------------------------------------------------------------------------------------------------
      If Not dsShared Is Nothing Then
        If Not dsShared.Tables(strNomeTabella) Is Nothing Then
          dsShared.Tables.Remove(strNomeTabella)
          dsShared.AcceptChanges()
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      bRet = oCldStbo.CheckReports(strDittaCorrente, dsShared, strNomeTabella, nDaconto, nAconto, nAnno, strSerie, _
        dateDatini, dateDatfin, nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, bSeldocumenti, strTipork, _
        nTipobf, nCoddest, nCodagen, nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, _
        bVistatiEntrambi, bVistati, bFlcontEntrambe, bFlcont, strCodcfam, bModTCO, bSelAnnoStag, nAnnotco, _
        nCodstag, strQuery, bFlevas, nTippaga, bModuloCRM, bAmm, strAccvis, strAccmod, strRegvis, strRegmod, _
        bIsCRMUser, lCodorgaOperat, nTiporeport, bCaricaDatiPerGriglia, _
        bIs15, nCodcageAccdito, lIITtsubqcrm, nCodlsel)
      If (bRet = True) And (dsShared.Tables(strNomeTabella).Rows.Count > 0) Then Return bRet
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function GetQueryStampaPdf(ByVal nDaconto As Integer, ByVal nAconto As Integer, _
          ByVal nAnno As Integer, ByVal strSerie As String, ByVal dateDatini As Date, ByVal dateDatfin As Date, _
          ByVal nDanumdoc As Integer, ByVal nAnumdoc As Integer, ByVal nCommecaini As Integer, ByVal nCommecafin As Integer, _
          ByVal bSeldocumenti As Boolean, ByVal bNoteprel As Boolean, ByVal strTipork As String, _
          ByVal nTipobf As Integer, ByVal nCoddest As Integer, ByVal nCodagen As Integer, ByVal nCodagen2 As Integer, _
          ByVal bBolleFatturateEntrambe As Boolean, ByVal opBolleFatturate As Boolean, _
          ByVal bVistatiEntrambi As Boolean, ByVal bVistati As Boolean, _
          ByVal bFlcontEntrambe As Boolean, ByVal bFlcont As Boolean, _
          ByVal strCodcfam As String, ByVal bSelAnnoStag As Boolean, _
          ByVal nAnnotco As Integer, ByVal nCodstag As Integer, _
          ByVal strQuery As String, ByVal bFlevas As Boolean, _
          ByVal nTippaga As Integer, ByRef strQueryGetDocUnico As String) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return GetQueryStampaPdf(nDaconto, nAconto, nAnno, strSerie, dateDatini, dateDatfin, nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, _
           bSeldocumenti, bNoteprel, strTipork, nTipobf, nCoddest, nCodagen, nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, _
           bVistatiEntrambi, bVistati, bFlcontEntrambe, bFlcont, strCodcfam, bSelAnnoStag, nAnnotco, nCodstag, strQuery, bFlevas, _
           nTippaga, strQueryGetDocUnico, "", "")
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return ""
    End Try
  End Function
  Public Overridable Function GetQueryStampaPdf(ByVal nDaconto As Integer, ByVal nAconto As Integer, _
                  ByVal nAnno As Integer, ByVal strSerie As String, ByVal dateDatini As Date, ByVal dateDatfin As Date, _
                  ByVal nDanumdoc As Integer, ByVal nAnumdoc As Integer, ByVal nCommecaini As Integer, ByVal nCommecafin As Integer, _
                  ByVal bSeldocumenti As Boolean, ByVal bNoteprel As Boolean, ByVal strTipork As String, _
                  ByVal nTipobf As Integer, ByVal nCoddest As Integer, ByVal nCodagen As Integer, ByVal nCodagen2 As Integer, _
                  ByVal bBolleFatturateEntrambe As Boolean, ByVal opBolleFatturate As Boolean, _
                  ByVal bVistatiEntrambi As Boolean, ByVal bVistati As Boolean, _
                  ByVal bFlcontEntrambe As Boolean, ByVal bFlcont As Boolean, _
                  ByVal strCodcfam As String, ByVal bSelAnnoStag As Boolean, _
                  ByVal nAnnotco As Integer, ByVal nCodstag As Integer, _
                  ByVal strQuery As String, ByVal bFlevas As Boolean, _
                  ByVal nTippaga As Integer, ByRef strQueryGetDocUnico As String, _
                  ByRef strQueryGetDocClienti As String, ByRef strQueryGetDocAgenti As String) As String
    Try
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nDaconto, nAconto, nAnno, strSerie, dateDatini, dateDatfin, nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, _
           bSeldocumenti, bNoteprel, strTipork, nTipobf, nCoddest, nCodagen, nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, _
           bVistatiEntrambi, bVistati, bFlcontEntrambe, bFlcont, strCodcfam, bSelAnnoStag, nAnnotco, nCodstag, strQuery, bFlevas, _
           nTippaga, strQueryGetDocUnico, strQueryGetDocClienti, strQueryGetDocAgenti})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strQueryGetDocUnico = NTSCStr(oIn(30))
        strQueryGetDocClienti = NTSCStr(oIn(31))
        strQueryGetDocAgenti = NTSCStr(oIn(32))
        Return NTSCStr(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return GetQueryStampaPdf(nDaconto, nAconto, nAnno, strSerie, dateDatini, dateDatfin, nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, _
           bSeldocumenti, bNoteprel, strTipork, nTipobf, nCoddest, nCodagen, nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, _
           bVistatiEntrambi, bVistati, bFlcontEntrambe, bFlcont, strCodcfam, bSelAnnoStag, nAnnotco, nCodstag, strQuery, bFlevas, _
           nTippaga, strQueryGetDocUnico, strQueryGetDocClienti, strQueryGetDocAgenti, False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return ""
    End Try
  End Function
  Public Overridable Function GetQueryStampaPdf(ByVal nDaconto As Integer, ByVal nAconto As Integer, _
                  ByVal nAnno As Integer, ByVal strSerie As String, ByVal dateDatini As Date, ByVal dateDatfin As Date, _
                  ByVal nDanumdoc As Integer, ByVal nAnumdoc As Integer, ByVal nCommecaini As Integer, ByVal nCommecafin As Integer, _
                  ByVal bSeldocumenti As Boolean, ByVal bNoteprel As Boolean, ByVal strTipork As String, _
                  ByVal nTipobf As Integer, ByVal nCoddest As Integer, ByVal nCodagen As Integer, ByVal nCodagen2 As Integer, _
                  ByVal bBolleFatturateEntrambe As Boolean, ByVal opBolleFatturate As Boolean, _
                  ByVal bVistatiEntrambi As Boolean, ByVal bVistati As Boolean, _
                  ByVal bFlcontEntrambe As Boolean, ByVal bFlcont As Boolean, _
                  ByVal strCodcfam As String, ByVal bSelAnnoStag As Boolean, _
                  ByVal nAnnotco As Integer, ByVal nCodstag As Integer, _
                  ByVal strQuery As String, ByVal bFlevas As Boolean, _
                  ByVal nTippaga As Integer, ByRef strQueryGetDocUnico As String, _
                  ByRef strQueryGetDocClienti As String, ByRef strQueryGetDocAgenti As String, _
                  ByVal bUsaReportStandard As Boolean) As String
    Try
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nDaconto, nAconto, nAnno, strSerie, dateDatini, dateDatfin, nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, _
           bSeldocumenti, bNoteprel, strTipork, nTipobf, nCoddest, nCodagen, nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, _
           bVistatiEntrambi, bVistati, bFlcontEntrambe, bFlcont, strCodcfam, bSelAnnoStag, nAnnotco, nCodstag, strQuery, bFlevas, _
           nTippaga, strQueryGetDocUnico, strQueryGetDocClienti, strQueryGetDocAgenti, bUsaReportStandard})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strQueryGetDocUnico = NTSCStr(oIn(30))
        strQueryGetDocClienti = NTSCStr(oIn(31))
        strQueryGetDocAgenti = NTSCStr(oIn(32))
        Return NTSCStr(oOut)
      End If

      Return GetQueryStampaPdf(nDaconto, nAconto, nAnno, strSerie, dateDatini, dateDatfin, nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, _
     bSeldocumenti, bNoteprel, strTipork, nTipobf, nCoddest, nCodagen, nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, _
     bVistatiEntrambi, bVistati, bFlcontEntrambe, bFlcont, strCodcfam, bSelAnnoStag, nAnnotco, nCodstag, strQuery, bFlevas, _
     nTippaga, strQueryGetDocUnico, strQueryGetDocClienti, strQueryGetDocAgenti, bUsaReportStandard, 0)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function
  Public Overridable Function GetQueryStampaPdf(ByVal nDaconto As Integer, ByVal nAconto As Integer, _
              ByVal nAnno As Integer, ByVal strSerie As String, ByVal dateDatini As Date, ByVal dateDatfin As Date, _
              ByVal nDanumdoc As Integer, ByVal nAnumdoc As Integer, ByVal nCommecaini As Integer, ByVal nCommecafin As Integer, _
              ByVal bSeldocumenti As Boolean, ByVal bNoteprel As Boolean, ByVal strTipork As String, _
              ByVal nTipobf As Integer, ByVal nCoddest As Integer, ByVal nCodagen As Integer, ByVal nCodagen2 As Integer, _
              ByVal bBolleFatturateEntrambe As Boolean, ByVal opBolleFatturate As Boolean, _
              ByVal bVistatiEntrambi As Boolean, ByVal bVistati As Boolean, _
              ByVal bFlcontEntrambe As Boolean, ByVal bFlcont As Boolean, _
              ByVal strCodcfam As String, ByVal bSelAnnoStag As Boolean, _
              ByVal nAnnotco As Integer, ByVal nCodstag As Integer, _
              ByVal strQuery As String, ByVal bFlevas As Boolean, _
              ByVal nTippaga As Integer, ByRef strQueryGetDocUnico As String, _
              ByRef strQueryGetDocClienti As String, ByRef strQueryGetDocAgenti As String, _
              ByVal bUsaReportStandard As Boolean, ByVal lCodlsel As Integer) As String
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nDaconto, nAconto, nAnno, strSerie, dateDatini, dateDatfin, nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, _
           bSeldocumenti, bNoteprel, strTipork, nTipobf, nCoddest, nCodagen, nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, _
           bVistatiEntrambi, bVistati, bFlcontEntrambe, bFlcont, strCodcfam, bSelAnnoStag, nAnnotco, nCodstag, strQuery, bFlevas, _
           nTippaga, strQueryGetDocUnico, strQueryGetDocClienti, strQueryGetDocAgenti, bUsaReportStandard, lCodlsel})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        strQueryGetDocUnico = NTSCStr(oIn(30))
        strQueryGetDocClienti = NTSCStr(oIn(31))
        strQueryGetDocAgenti = NTSCStr(oIn(32))
        Return NTSCStr(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return oCldStbo.GetQueryStampaPDF(strDittaCorrente, dsShared, strNomeTabella, nDaconto, nAconto, _
        nAnno, strSerie, dateDatini, dateDatfin, nDanumdoc, nAnumdoc, nCommecaini, nCommecafin, _
        bSeldocumenti, strTipork, nTipobf, nCoddest, nCodagen, nCodagen2, bBolleFatturateEntrambe, opBolleFatturate, _
        bVistatiEntrambi, bVistati, bFlcontEntrambe, bFlcont, strCodcfam, bModTCO, bSelAnnoStag, nAnnotco, _
        nCodstag, strQuery, bFlevas, nTippaga, bModuloCRM, bAmm, strAccvis, strAccmod, strRegvis, strRegmod, _
        bIsCRMUser, lCodorgaOperat, strQueryGetDocUnico, bIs15, nCodcageAccdito, lIITtsubqcrm, strQueryGetDocClienti, _
        strQueryGetDocAgenti, bUsaReportStandard, lCodlsel)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return ""
    End Try
  End Function

  Public Overridable Function RiempiTmpTable(ByVal bModuloCrm As Boolean, ByVal bIsCRMUser As Boolean) As Boolean
    Try
      If (bModuloCrm = False) Or (bIsCRMUser = False) Or (bIs15 = False) Then Return True
      oCldStbo.RiempiTTSUBQCRM(strDittaCorrente, lIITtsubqcrm)
      Return True
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      Return False
    End Try
  End Function

  Public Overridable Function RitornaCodcageAccdito(ByVal bModuloCrm As Boolean, ByVal bIsCRMUser As Boolean) As Integer
    Try
      If (bModuloCrm = False) Or (bIsCRMUser = False) Then Return 0
      Return oCldStbo.RitornaCodcageAccidto(strDittaCorrente)
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function ValCodiceConto(ByVal strCodice As String, ByRef dttTable As DataTable) As Boolean
    Try
      Return oCldStbo.ValCodiceDb(strCodice, strDittaCorrente, "ANAGRA", "N", "", dttTable)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
  End Function

  Public Overridable Function RitornaLISTSEL(ByVal nCodlsel As Integer, ByRef dttOut As DataTable) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return oCldStbo.RitornaLISTSEL(strDittaCorrente, nCodlsel, dttOut)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

#Region "Filtri"
  Public Overridable Function GetTableStructMovIfil(ByRef dttTable As DataTable) As Boolean
    Try
      Return oCldStbo.GetTableStructure("MOVIFIL", False, dttTable)
    Catch ex As Exception
      '----------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '----------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function CaricaFiltri(ByRef dttOut As DataTable) As Boolean
    Try
      If Not oCldStbo.CaricaFiltri(strDittaCorrente, "BNMGSTBO", dttOut) Then Return False

      dttOut.Rows.InsertAt(dttOut.NewRow, 0)
      dttOut.Rows(0)!cod = 0
      dttOut.Rows(0)!val = " -- Nessuno -- "

      Return True
    Catch ex As Exception
      '----------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '----------------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function LeggiFiltro(ByVal lCod As Integer, ByVal strChild As String, ByVal strForm As String, ByRef dttOut As DataTable) As Boolean
    Try
      If Not oCldstbo.LeggiFiltro(lCod, strChild, strForm, dttOut) Then Return False

      Return True
    Catch ex As Exception
      '----------------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '----------------------------------------------------------------------------------------
    End Try
  End Function
#End Region
End Class
