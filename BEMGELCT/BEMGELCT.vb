#Region "Imports"
Imports System.Data
Imports NTSInformatica.CLN__STD
#End Region

Public Class CLEMGELCT
  Inherits CLE__BASE

#Region "Moduli"
  Private Moduli_P As Integer = bsModMG
  Private ModuliExt_P As Integer = 0
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
  Public oCldElct As CLDMGELCT
  'Variabili per tabella temporanea
  Public nInstid As Integer = 0
  Public strTabellaTemporanea As String = "TTARTICOX"
  'Variabili opzioni di registro
  Public nCodArtDaCatNListPubb As Integer
  Public nCodArtDaCatNListIngr As Integer
  'Variabili di allineamento con UI
  Public strCodartf As String
  Public strDescr As String
#End Region

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGELCT"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldElct = CType(MyBase.ocldBase, CLDMGELCT)
    oCldElct.Init(oApp)
    Return True
  End Function

  Public Overloads Function Apri(ByVal nConto As Integer, ByVal nMarca As Integer, ByVal dateDataAgg As Date) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return Apri(nConto, nMarca, dateDataAgg, True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overloads Function Apri(ByVal nConto As Integer, ByVal nMarca As Integer, ByVal dateDataAgg As Date, _
    ByVal bMarca As Boolean) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {nConto, nMarca, dateDataAgg, bMarca})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        Return CBool(oOut)
      End If
      '--------------------------------------------------------------------------------------------------------------
      If oCldElct.LeggiTabella(strDittaCorrente, strNomeTabella, dsShared, nConto, nMarca, dateDataAgg, bMarca) = True Then
        If nInstid <> 0 Then ResetTblInstId() Else SetDefaultValue(dsShared)
        Return True
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overrides Sub SetDefaultValue(ByRef ds As DataSet)
    Try
      'Creo l'instid per TTARTICOX
      nInstid = oCldElct.GetTblInstId(strTabellaTemporanea, False)

      'leggo impostazioni registro
      nCodArtDaCatNListPubb = NTSCInt(oCldElct.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoNListPubb", "11", " ", "11"))
      nCodArtDaCatNListIngr = NTSCInt(oCldElct.GetSettingBus("BSMGARTI", "OPZIONI", ".", "CodartDaCatalogoNListIngr", "12", " ", "12"))

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function Elabora(ByVal bAutomatica As Boolean, _
                                      ByVal bDescrizione As Boolean, ByVal bClasseSconto As Boolean, _
                                      ByVal bStatus As Boolean, ByVal bGruppo As Boolean, _
                                      ByVal bFamiglia As Boolean, ByVal bNomenclatura As Boolean, _
                                      ByVal bNote1 As Boolean, ByVal bRrfence As Boolean, _
                                      ByVal bPesi As Boolean, ByVal bLotto As Boolean, _
                                      ByVal bPrezzi As Boolean, ByVal bBarcode As Boolean) As Boolean
    Dim cont, i As Integer
    Dim evnt As New NTSEventArgs("", "")

    Try
      cont = 0
      'cicliamo sui record acquisiti dalla apri
      For i = 0 To dsShared.Tables(strNomeTabella).Rows.Count - 1
        'aggiorniamo la status bar della UI con il numero del record in elaborazione
        ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128498074754958728, "Elaborazione record |" & (i + 1) & "| di |" & dsShared.Tables(strNomeTabella).Rows.Count & "| in corso...")))

        With dsShared.Tables(strNomeTabella).Rows(i)

          'controllo se presente ae_codart in artico
          If oCldElct.ValCodiceDb(NTSCStr(!ae_codart), strDittaCorrente, "ARTICO", "S") Then
            If Not bAutomatica Then
              'se l'esecuzione e manuale devo visualizzare la form dell'UI relativa
              strCodartf = NTSCStr(!ae_codartf)
              strDescr = NTSCStr(!ae_descr)
              evnt = New NTSEventArgs("MODALEMAN:", "")
              ThrowRemoteEvent(evnt)
              Select Case evnt.RetValue
                Case "AGGIORNA"
                Case "SALTA"
                Case "TERMINA"
                  ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 128672443178750000, "L'operazione di aggiornamento è stata annullata correttamente. I record rimanenti non verranno elaborati.")))
                  Return True
                Case "AUTOMATICA"
                  bAutomatica = True
                Case Else
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128672442320468750, "Valore non riconosciuto restituito dalla modale, impossibile continuare.")))
                  Return False
              End Select
            End If
            If bAutomatica Or evnt.RetValue = "AGGIORNA" Then
              'se l'esecuzione è automatica oppura abbiamo scelto di aggiornare
              'il record dalla manuale procediamo ad aggiornare
              'artico listini e barcode  con i dati del record (i)
              Dim strErrore As String = ""
              If Not oCldElct.Elaborazione(strDittaCorrente, dsShared.Tables(strNomeTabella).Rows(i), bDescrizione, bClasseSconto, _
                                           bStatus, bGruppo, bFamiglia, bNomenclatura, bNote1, _
                                           bRrfence, bPesi, bLotto, bBarcode, strErrore, nInstid) Then
                ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128672445808593750, "Errore durante l'elaborazione del record |" & (i + 1) & "|. Elaborazione terminata.")))
                Return False
              Else
                'verifico che in strErrore non vi sia alcun errore
                If strErrore <> "" Then
                  ThrowRemoteEvent(New NTSEventArgs("", strErrore))
                Else
                  'se è cosi posso passare all'aggiornamneto dei listini
                  If bPrezzi Then
                    Dim dttTmp As New DataTable
                    Dim nUltFase As Integer = 0
                    If oCldElct.ValCodiceDb(NTSCStr(!ae_codart), strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
                      If NTSCStr(dttTmp.Rows(0)!ar_gesfasi) = "S" Then nUltFase = NTSCInt(dttTmp.Rows(0)!ar_ultfase)

                      If NTSCDec(!ae_prezzopu) <> 0 Then 'prezzo al pubblico
                        CType(oCleComm, CLELBMENU).ScriviPrezzo(strDittaCorrente, NTSCStr(!ae_codart), _
                        nUltFase, 0, _
                        " ", nCodArtDaCatNListPubb, 0, 0, NTSCStr(!ae_unmis), 0, 0, NTSCDate(!ae_dataggpu), _
                        NTSCDec(!ae_prezzopu), New Date(2099, 12, 31), 1, 9999999999, "", "N", True, "", 0)
                      End If

                      If NTSCDec(!ae_prezzogr) <> 0 Then 'prezzo grossisti
                        CType(oCleComm, CLELBMENU).ScriviPrezzo(strDittaCorrente, NTSCStr(!ae_codart), _
                        nUltFase, 0, _
                        " ", nCodArtDaCatNListIngr, 0, 0, NTSCStr(!ae_unmis), 0, 0, NTSCDate(!ae_datagggr), _
                         NTSCDec(!ae_prezzogr), New Date(2099, 12, 31), 1, 9999999999, "", "N", True, "", 0)
                      End If
                    End If
                    If bAutomatica Then cont += 1
                  Else
                    If bAutomatica Then cont += 1
                  End If
                End If
              End If
            End If
          End If
        End With
      Next

      If cont > 0 Then ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 128672639797187500, "Elaborazione automatica terminata, |" & cont & "| articoli aggiornati.")))
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

  Public Overridable Function ValidaConto(ByVal strConto As String, ByRef strDescConto As String) As Boolean
    Try
      Return oCldElct.ValCodiceDb(strConto, strDittaCorrente, "ANAGRAF", "N", strDescConto)
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
  Public Overridable Function ValidaCodmarc(ByVal strCodmarc As String, ByRef strDescCodmarc As String) As Boolean
    Try
      Return oCldElct.ValCodiceDb(strCodmarc, strDittaCorrente, "TABMARC", "N", strDescCodmarc)
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

  Public Overridable Function GetSettingBusRecentCheck(ByVal strRecentDescr As String) As String
    Dim strReturnValue As String = "1"
    Try
      strReturnValue = oCldElct.GetSettingBus("BSMGELCT", "RECENT", ".", strRecentDescr, "1", " ", "1")
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
    Return strReturnValue
  End Function
  Public Overridable Sub SaveSettingBusRecentCheck(ByVal strRecentDescr As String, ByVal bCheckValue As Boolean)
    Try
      oCldElct.SaveSettingBus("BSMGELCT", "RECENT", ".", strRecentDescr, NTSCStr(IIf(bCheckValue, "1", "0")), " ", "NS.", "...", "...")
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ResetTblInstId()
    Try
      oCldElct.ResetTblInstId(strTabellaTemporanea, False, nInstid)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function VerificaPresenzaDatiDaStampare() As Boolean
    Try
      Return oCldElct.VerificaPresenzaDatiDaStampare(strDittaCorrente, strTabellaTemporanea, nInstid)
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
