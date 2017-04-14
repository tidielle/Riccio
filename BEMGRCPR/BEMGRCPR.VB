Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEMGRCPR
  Inherits CLE__BASE

  Public oCldRcpr As CLDMGRCPR

#Region "VARIABILI"
  '--------------------------------------------------------------------------------------
  '--- Valori in entrata
  '--------------------------------------------------------------------------------------
  Public lContoIn As Integer
  Public nValutaIn As Integer
  Public nFaseIn As Integer
  Public dQuantIn As Decimal
  Public dPrezzoBase As Decimal
  Public strCodartIn As String
  Public strUnmisIn As String
  Public strFlprznetIn As String
  Public dtDatordIn As Date
  '--------------------------------------------------------------------------------------
  Public dPerqta As Decimal
  Public strUnmis As String
  '--------------------------------------------------------------------------------------
  Public dttArtico As New DataTable

#End Region

  Public Overrides Function Init(ByRef App As CLE__APP, ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, ByVal bRemoting As Boolean, ByVal strRemoteServer As String, ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGRCPR"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldRcpr = CType(MyBase.ocldBase, CLDMGRCPR)
    oCldRcpr.Init(oApp)
    Return True
  End Function

  Public Overridable Sub InizializzaCampi(ByVal strDitta As String, _
                                          ByVal strCodart As String, ByVal bOnLoading As Boolean, _
                                          ByRef dttArtico As DataTable, ByRef strDespdon As String, ByRef dUltcos As Decimal, _
                                          ByRef dDaQuant As Decimal, ByRef dAQuant As Decimal, _
                                          ByRef dPrezzoNew() As Decimal, ByRef dPrezzoOld() As Decimal, _
                                          ByRef dtDataOld() As Date)
    Dim i As Integer = 0
    Dim dPrezzo As Decimal = 0
    Dim dTmp As Decimal = 0
    Dim dArro1 As Decimal = 0
    Dim dNuovoprezzo As Decimal = 0
    Dim dttTabpdon As New DataTable
    Dim dRicarico1 As Decimal = 0
    Dim dRicarico2 As Decimal = 0
    Try
      '----------------------------------------------------------------------------------
      '--- Riempie il DataTable da ARTICO
      '----------------------------------------------------------------------------------
      oCldRcpr.ValCodiceDb(strCodart, strDitta, "ARTICO", "S", "", dttArtico)

      With dttArtico.Rows(0)
        If NTSCStr(!ar_gesvar) = "S" Then ' se  a varianti cambia la base di calcolo ..
          Select Case NTSCStr(!ar_prevar)
            Case "N"
              If NTSCStr(!ar_codroot).Trim <> "" Then
                strCodartIn = NTSCStr(!ar_codroot)
                oCldRcpr.ValCodiceDb(strCodartIn, strDitta, "ARTICO", "S", "", dttArtico)
              End If
            Case "S"
              If NTSCStr(!ar_codart).Trim <> "" Then
                strCodartIn = NTSCStr(!ar_codart)
                oCldRcpr.ValCodiceDb(strCodartIn, strDitta, "ARTICO", "S", "", dttArtico)
              End If
            Case "1"
              If (NTSCStr(!ar_codroot) & NTSCStr(!ar_codvar1)).Trim <> "" Then
                oCldRcpr.ValCodiceDb(NTSCStr(!ar_codroot), strDitta, "ARTICO", "S", "", dttArtico)
                strCodartIn = NTSCStr(!ar_codroot) & NTSCStr(!ar_codvar1)
                dttArtico.Rows(0)!ar_codart = strCodartIn
              End If
          End Select
        End If
      End With

      '----------------------------------------------------------------------------------
      '--- Determina, se esiste, la descrizione della relazione listini
      '----------------------------------------------------------------------------------
      strDespdon = ""
      If dttArtico.Rows.Count > 0 Then
        dRicarico1 = NTSCDec(dttArtico.Rows(0)!ar_ricar1)
        dRicarico2 = NTSCDec(dttArtico.Rows(0)!ar_ricar2)

        If NTSCInt(dttArtico.Rows(0)!ar_codpdon) <> 0 Then
          oCldRcpr.ValCodiceDb(dttArtico.Rows(0)!ar_codpdon.ToString, strDitta, "TABPDON", "N", strDespdon)
        End If
      End If
      '----------------------------------------------------------------------------------
      oCldRcpr.RitornaAltriDati(strDitta, strCodart, nValutaIn, dtDatordIn, dQuantIn, strUnmisIn, nFaseIn, dUltcos, dDaQuant, dAQuant)
      '----------------------------------------------------------------------------------
      If bOnLoading = True Then
        EseguiOnLoading(strDitta, strCodart, dttArtico, dRicarico1, dRicarico2, dPrezzoNew, dPrezzoOld, dtDataOld)
      Else
        Esegui(strDitta, strCodart, dttArtico, dRicarico1, dRicarico2, 0.01D, dPrezzoNew, dPrezzoOld, dtDataOld)
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub AggiornaPrezzi(ByVal strDitta As String, _
    ByVal dttArtico As DataTable, ByVal dDaQuant As Decimal, ByVal dAQuant As Decimal, _
    ByVal dtData As Date, ByVal dPrezzoNew() As Decimal)
    Dim i As Integer = 0

    Try
      '----------------------------------------------------------------------------------
      For i = 1 To 8
        '--------------------------------------------------------------------------------
        If (NTSCInt(dttArtico.Rows(0)!ar_codpdon.ToString) = 0) And (i = 2) Then Return
        '--------------------------------------------------------------------------------
        If dPrezzoNew(i) <> 0 Then
          '------------------------------------------------------------------------------
          CType(oCleComm, CLELBMENU).ScriviPrezzo(strDitta, strCodartIn, nFaseIn, _
            0, " ", i, 0, nValutaIn, strUnmisIn, 0, dDaQuant, dtData, dPrezzoNew(i), _
            Nothing, NTSCDec(dttArtico.Rows(0)!ar_perqta.ToString), dAQuant, "", _
            "N", True, "", 0)
          '------------------------------------------------------------------------------
        End If
        '--------------------------------------------------------------------------------
      Next
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub EseguiOnLoading(ByVal strDitta As String, ByVal strCodart As String, _
      ByVal dttArtico As DataTable, ByVal dRicar1 As Decimal, ByVal dRicar2 As Decimal, _
      ByRef dPrezzoNew() As Decimal, ByRef dPrezzoOld() As Decimal, ByRef dtDataOld() As Date)
    Dim i As Integer = 0

    Try
      '----------------------------------------------------------------------------------
      '--- Determina prezzi vecchi e nuovi e date da 1 a 8
      '----------------------------------------------------------------------------------
      For i = 1 To 8
        dPrezzoNew(i) = 0
        dPrezzoOld(i) = 0
        dtDataOld(i) = Nothing
      Next
      '----------------------------------------------------------------------------------
      oCldRcpr.RitornaDatiListino(strDitta, strCodart, 1, nValutaIn, dtDatordIn, _
        dQuantIn, strUnmisIn, nFaseIn, dPrezzoOld(1), dtDataOld(1))
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '----------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Esegui(ByVal strDitta As String, ByVal strCodart As String, _
    ByVal dttArtico As DataTable, ByVal dRicar1 As Decimal, ByVal dRicar2 As Decimal, _
    ByVal dArro As Decimal, _
    ByRef dPrezzoNew() As Decimal, ByRef dPrezzoOld() As Decimal, ByRef dtDataOld() As Date)
    Dim i As Integer = 0
    Dim dPrezzo As Decimal = 0
    Dim dTmp As Decimal = 0
    Dim dArro1 As Decimal = 0
    Dim dNuovoprezzo As Decimal = 0
    Dim dttTabpdon As New DataTable

    Try
      '----------------------------------------------------------------------------------
      If dttArtico.Rows(0)!ar_flricmar.ToString = "R" Then
        dPrezzo = NTSCDec(dPrezzoBase * ((100 + dRicar1) / 100) * ((100 + dRicar2) / 100))
      Else
        dPrezzo = NTSCDec(dPrezzoBase / ((100 - dRicar1 - dRicar2) / 100))
      End If
      '----------------------------------------------------------------------------------
      If NTSCInt(dttArtico.Rows(0)!ar_codpdon.ToString) = 0 Then
        If (dPrezzo / dArro) <> Fix(dPrezzo / dArro) Then
          dTmp = NTSCInt(Fix(dPrezzo / dArro))
          dTmp = (dTmp + 1)
          dPrezzo = NTSCDec(dTmp * dArro)
        End If
        dPrezzoNew(1) = dPrezzo
      Else
        '--------------------------------------------------------------------------------
        oCldRcpr.ValCodiceDb(dttArtico.Rows(0)!ar_codpdon.ToString, strDitta, "TABPDON", "N", "", dttTabpdon)
        '--------------------------------------------------------------------------------
        If dttTabpdon.Rows.Count > 0 Then
          '------------------------------------------------------------------------------
          If NTSCDec(dttTabpdon.Rows(0)!tb_arro_1.ToString) = 0 Then
            dArro1 = dArro
          Else
            dArro1 = NTSCDec(dttTabpdon.Rows(0)!tb_arro_1.ToString)
          End If
          '------------------------------------------------------------------------------
          If (dPrezzo / dArro1) <> Fix(dPrezzo / dArro1) Then
            dTmp = Fix(dPrezzo / dArro1)
            dTmp = (dTmp + 1)
            dPrezzo = (dTmp * dArro1)
          End If
        Else
          '------------------------------------------------------------------------------
          If (dPrezzo / dArro) <> Fix(dPrezzo / dArro) Then
            dTmp = Fix(dPrezzo / dArro)
            dTmp = (dTmp + 1)
            dPrezzo = (dTmp * dArro)
          End If
          '------------------------------------------------------------------------------
        End If
        '--------------------------------------------------------------------------------
        dttTabpdon.Clear()
        '--------------------------------------------------------------------------------
        dPrezzoNew(1) = dPrezzo
        '--------------------------------------------------------------------------------
        oCldRcpr.RitornaDatiListino(strDitta, strCodart, 1, nValutaIn, dtDatordIn, _
          dQuantIn, strUnmisIn, nFaseIn, dPrezzoOld(1), dtDataOld(1))
        '--------------------------------------------------------------------------------
        oCldRcpr.ValCodiceDb(dttArtico.Rows(0)!ar_codpdon.ToString, strDitta, "TABPDON", "N", "", dttTabpdon)
        '--------------------------------------------------------------------------------
        For i = 2 To 8
          '------------------------------------------------------------------------------
          oCldRcpr.RitornaDatiListino(strDitta, strCodart, i, nValutaIn, dtDatordIn, _
            dQuantIn, strUnmisIn, nFaseIn, dPrezzoOld(i), dtDataOld(i))
          '------------------------------------------------------------------------------
          If NTSCDec(dttTabpdon.Rows(0)("tb_rica_" & NTSCStr(i)).ToString) <> 0 Then
            dNuovoprezzo = ArrDbl((dPrezzoNew(1) + (dPrezzoNew(1) * NTSCDec(dttTabpdon.Rows(0)("tb_rica_" & NTSCStr(i)).ToString) / 100)), oCldRcpr.TrovaNdecSuPrzUn(nValutaIn))
            If NTSCDec(dttTabpdon.Rows(0)("tb_arro_" & NTSCStr(i)).ToString) <> 0 Then
              If ((dNuovoprezzo / NTSCDec(dttTabpdon.Rows(0)("tb_arro_" & NTSCStr(i)).ToString)) <> Fix(dNuovoprezzo / NTSCDec(dttTabpdon.Rows(0)("tb_arro_" & NTSCStr(i)).ToString))) Then
                dTmp = Fix(dNuovoprezzo / NTSCDec(dttTabpdon.Rows(0)("tb_arro_" & NTSCStr(i)).ToString))
                dTmp = (dTmp + 1)
                dNuovoprezzo = ArrDbl((dTmp * NTSCDec(dttTabpdon.Rows(0)("tb_arro_" & NTSCStr(i)).ToString)), oCldRcpr.TrovaNdecSuPrzUn(nValutaIn))
              End If
            End If
            dPrezzoNew(i) = dNuovoprezzo
          End If
          '------------------------------------------------------------------------------
        Next
        '--------------------------------------------------------------------------------
        dttTabpdon.Clear()
        '--------------------------------------------------------------------------------
      End If
      '----------------------------------------------------------------------------------
    Catch ex As Exception
      '----------------------------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '----------------------------------------------------------------------------------
    End Try
  End Sub

End Class