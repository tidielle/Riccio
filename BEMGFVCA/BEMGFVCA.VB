Imports System.Data
Imports System.IO
Imports NTSInformatica.CLN__STD

Public Class CLEMGFVCA
  Inherits CLE__BASE

  Private Moduli_P As Integer = bsModMG + bsModVE + bsModOR
  Private ModuliExt_P As Integer = bsModExtMGE + bsModExtORE
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

#Region "VARIABILI"
  Public oCldFvca As CLDMGFVCA

  Public nCommessaPredef As Integer = 0
  Public bRicostruisci As Boolean = False
  Public bElabInCorso As Boolean = False
  Public bSeleziona As Boolean
  'Public bFileLogAperto As Boolean = False
  'Public nLogCount As Integer = 0

#End Region

  Public Overridable Function LeggiRegistro() As Boolean
    Try

      'Lettura da registro di Business delle OPZIONI disponibili
      nCommessaPredef = NTSCInt(oCldFvca.GetSettingBus("BSDBEMRP", "OPZIONI", ".", "CommessaPredefinita", "0", " ", "0"))

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

  Public Overrides Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, ByVal bRemoting As Boolean, ByVal strRemoteServer As String, ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGFVCA"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldFvca = CType(MyBase.ocldBase, CLDMGFVCA)
    oCldFvca.Init(oApp)
    Return True
  End Function

  Public Overridable Function Elabora(ByVal dttTmp2 As DataTable, ByVal bAr_gescomm As Boolean, ByVal strAr_gescommIni As String, ByVal strAr_gescommFin As String) As Boolean
    Dim bRet As Boolean
    Dim strMsg As String = ""
    Dim strLog As String = ""
    Dim dsArtico As DataSet = Nothing
    Dim evt As NTSEventArgs = Nothing
    Dim nNdec As Integer = 0
    Dim bModTCO As Boolean = False
    Dim dttAnaz As New DataTable
    Dim strDtulap As String = ""
    Dim strDesogglog As String = ""
    Try
      '----------------------------------------------------------------------------------
      'bFileLogAperto = False
      bElabInCorso = False
      '----------------------------------------------------------------------------------
      nNdec = oCldFvca.TrovaNdecSuPrzUn(0)
      If CBool(oApp.ActKey.ModuliAcquistati And bsModExtTCO) Then
        bModTCO = True
      Else
        bModTCO = False
      End If
      If Not oCldFvca.ValCodiceDb(strDittaCorrente, strDittaCorrente, "TABANAZ", "S", "", dttAnaz) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128551470712031250, "Errore: Impossibile trovare la ditta corrente.")))
        Return False
      End If
      If NTSCStr(dttAnaz.Rows(0)!tb_dtulap) = "" Then
        strDtulap = NTSCStr(NTSCDate("01/01/1900"))
      Else
        strDtulap = NTSCStr(NTSCDate(dttAnaz.Rows(0)!tb_dtulap))
      End If
      '----------------------------------------------------------------------------------      
      bElabInCorso = True
      '----------------------------------------------------------------------------------
      'LogStart(oApp.Tr(Me, 130365900515725272, "Forza Variazione Campi Articolo"))
      '----------------------------------------------------------------------------------
      bRet = oCldFvca.Elabora(strDittaCorrente, dttTmp2, nCommessaPredef, bAr_gescomm, strAr_gescommIni, _
                              strAr_gescommFin, strLog, strDtulap, nNdec, bModTCO)
      '----------------------------------------------------------------------------------
      'If strLog.Trim <> "" Then If LogWrite(strLog, True) = False Then Return False
      '----------------------------------------------------------------------------------
      'SCRIVO ACTLOG
      If bRet = True Then
        strDesogglog = "Forza Variazione Campi Articolo" & vbCrLf & vbCrLf
        For Each dtr2 As DataRow In dttTmp2.Rows
          strDesogglog = strDesogglog & " - Articolo.............................: '" & NTSCStr(dtr2!ar_codart) & "' " & vbCrLf
        Next
        strDesogglog = strDesogglog & _
        " - Aggiorna gestione per commessa.......: " & NTSCStr(IIf(bAr_gescomm = True, "'Sì'", "'No'")) & vbCrLf & _
        " - Da valore............................: '" & strAr_gescommIni & "'" & vbCrLf & _
        " - A valore.............................: '" & strAr_gescommFin & "'" & vbCrLf & _
        " - Commessa predefinita.................: '" & nCommessaPredef & "'" & vbCrLf & _
        " - Numero decimali su prezzi unitari....: '" & nNdec & "'" & vbCrLf & _
        " - Aggiorna tabelle taglie e colori.....: " & NTSCStr(IIf(bModTCO = True, "'Sì'", "'No'")) & vbCrLf & _
        " - Data ultimo aggiornamento progressivi: '" & strDtulap & "'" & vbCrLf
        oCldFvca.ScriviActLog(strDittaCorrente, "BSMGFVCA", "BSMGFVCA", "", "", "M", "E", strDesogglog, False)
      End If
      '------------------------
      bElabInCorso = False
      '----------------------------------------------------------------------------------
      Return True
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '----------------------------------------------------------------------------------
    Finally
      '----------------------------------------------------------------------------------
      bElabInCorso = False
      '----------------------------------------------------------------------------------
      'LogStop()
      '----------------------------------------------------------------------------------
    End Try
  End Function

  Public Overridable Function edCodart_Validated(ByVal strCod As String, ByRef strError As String, ByRef dttTmp As DataTable, ByRef dttTmp2 As DataTable) As Boolean
    Try
      Return oCldFvca.ValCodart(strDittaCorrente, strCod, strError, dttTmp, dttTmp2)
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

End Class