#Region "Imports"
Imports System.Data
Imports System.IO
Imports NTSInformatica.CLN__STD
#End Region

Public Class CLEPRELMP
  Inherits CLE__BASE

#Region "Moduli"
  Private Moduli_P As Integer = bsModPR
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

  Public oCldElmp As CLDPRELMP
  'Variabili opzioni registro
  Public bMaturaSoloRDTotInc As Boolean
  Public bSbloccaSoloFinoaData As Boolean
  Public bProvvig2 As Boolean = False     'se true il calcolo delle provvigioni su incassato avverrà con il nuovo sistema di collegamento a scaden (da net 2012)
  Public bRbNoChiucli As Boolean = False  'se true il chiente viene chiuso non con la generaz. effetti ma all'effettivo incasso della RB/tratta    (da net 2012)
  Public bEseguiTestPreElab As Boolean = False
  Public bVisualizzaFilePreElab As Boolean = False

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    MyBase.strNomeDal = "BDPRELMP"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldElmp = CType(MyBase.ocldBase, CLDPRELMP)
    oCldElmp.Init(oApp)
    Return True
  End Function

  Public Overridable Function Elabora(ByVal strData As String, ByVal bSblocca As Boolean) As Boolean
    Try
      Dim nAgentiElaborati As Integer = 0

      If bSblocca And bProvvig2 = False Then
        Dim strbSblocca As New System.Text.StringBuilder
        If oCldElmp.Sblocca(strDittaCorrente, strNomeTabella, dsShared, strbSblocca, strData, bSbloccaSoloFinoaData) Then

          If strbSblocca.Length > 0 Then
            'Messaggistica UI
            ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128733689142968750, "Agente == Cliente == Documento (Tipo/anno/serie/numero/rata):") & strbSblocca.ToString))
          Else
            ThrowRemoteEvent(New NTSEventArgs("STATUSBAR:", oApp.Tr(Me, 128733122898185542, "Nessuna provvigione sbloccata.")))
          End If
        Else
          Return False
        End If
      End If

      'se settato di utilizzare il nuovo sistema di maturazione su incassato, eseguo un giro diverso
      If bProvvig2 Then
        If Not Elabora_IncassatoNew(strData) Then Return False
      End If    'If bProvvig2 Then

      'ora il giro classico (verranno escluse quelle maturate nella 
      If oCldElmp.Elabora(strDittaCorrente, strNomeTabella, strData, bSblocca, bMaturaSoloRDTotInc, nAgentiElaborati) Then
        If bProvvig2 Then
          ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 129673082226640625, "Elaborazione maturato completata")))
        Else
          ThrowRemoteEvent(New NTSEventArgs(CLN__STD.ThMsg.MSG_INFO, oApp.Tr(Me, 128733064437873042, "Elaborazione maturato completata: agenti elaborati: |" & nAgentiElaborati & "|")))
        End If

        Return True
      End If
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

  Public Overridable Function Elabora_IncassatoNew(ByVal strData As String) As Boolean
    'In caso di incassato con rimessa diretta, si guarderà in scaden se effettivamente 
    'la rata risulta essere incassata 

    'se il cliente non si chiude fino all’effettivo incasso (nuovo sistema – c’è flag in anagrafica ditta) 
    'tratto le RB / TRATTE come se fossero rimesse dirette. 
    'Se invece i cliente viene chiuso con l’emissione effetti, 
    'vengono prese le scadenze saldate con data di scadenza della rata collegata a provvig + gg di esito inferiore 
    'a data elaborazione. Se a fronte di più scadenze viene fatto un raggruppamento effetti, 
    'la data di scadenza deve essere quella della nuova scadenza: questa nuova data viene memorizzata in provvig 
    'nel nuovo campo ‘data effettiva per maturazione’ (da utilizzare successivamente per tutte le elaborazioni); 
    'se viene cancellato il raggruppamento effetti deve essere considerata la scadenza originaria. 
    'Per gestire questa problematica nella prima fase di elaborazione maturato viene ricalcolata la NUOVA data di 
    'scadenza: prima viene impostata quella della rata originaria, successivamente si verifica se la scadenza 
    'è stata raggruppata (reg. di CG di incasso con causale ‘raggruppamento effetti’) e nel caso si prende 
    'la data di scadenza della nuova rata (ovvero l’unica nuova scadenza generata dalla reg. di raggruppamento 
    'effetti a parità di conto). 
    'Non è gestito il caso di raggruppamento effetti che chiudono scadenze raggruppate 
    '(ovvero nidificazione di raggruppamento). Se la nuova scadenza raggruppata risulta non saldata, 
    'non viene maturato nulla (perché ancora non presentata in banca …). 
    'Non gestito il caso di raggruppamento effetti che salda scadenze di tipo RB con nuova scadenza non di tipo RB 
    '(stessa cosa per tratte, ovvero il tipo scadenza deve essere lo stesso tra scadenze salsate e nuova 
    'scadenza generata). 
    'Con generazione RB senza chiusura cliente, quindi, maturo quando la scadenza è saldata. 
    'Ma se faccio il raggruppam. Effetti, la scadenza viene subito saldata, mentre quella nuova potrebbe 
    'non esserlo, quindi non va maturata fino a quando non è saldata!!!!

    '- Problema degli insoluti:

    Dim dttTmp As New DataTable
    Dim strMsg As String = ""
    Dim nCausRagg As Integer = 0   'causale di raggruppamento effetti letto da TABPECG

    Try
      '------------------------
      'ottengo anche un datatable contenente le scadenze di provvig che non sono più allineate con scaden 
      'per tipo scadenza e/o importo
      If Not oCldElmp.IncassatoNewCheckScaden(strDittaCorrente, dttTmp) Then Return False
      If dttTmp.Rows.Count > 0 Then
        strMsg = oApp.Tr(Me, 129484753785371094, "ATTENZIONE: sono state trovate provvigioni su INCASSATO disallineate con le relative scadenze " & vbCrLf & _
                         "per importo e/o tipo pagamento: non verrano maturate." & vbCrLf & _
                         "Si consiglia di rieseguire la 'Generazione provvigioni'" & vbCrLf & vbCrLf & _
                         "Agente - cliente - anno documento - serie documento - rata scadenza" & vbCrLf & _
                         "data scadenza - data reg. CG - num. reg. CG" & vbCrLf & _
                         "tipo pag. provvig. - tipo pagam scadenza" & vbCrLf & _
                         "importo scadenza provvig. - importo scadenza scadenziario")
        For Each dtrT As DataRow In dttTmp.Rows
          strMsg += vbCrLf & dtrT!pv_codage.ToString & " - " & _
                             dtrT!pv_conto.ToString & " - " & _
                             dtrT!pv_anno.ToString & " - " & _
                             dtrT!pv_serie.ToString & " - " & _
                             dtrT!pv_numdoc.ToString & " - " & _
                             dtrT!pv_numrat.ToString & "    :    " & _
                             NTSCDate(dtrT!pv_datscad).ToShortDateString & " - " & _
                             NTSCDate(dtrT!sc_datreg).ToShortDateString & " - " & _
                             dtrT!sc_numreg.ToString & "    :    " & _
                             dtrT!pv_tippaga.ToString & " - " & _
                             dtrT!sc_tippaga.ToString & "    :    " & _
                             NTSCDec(dtrT!pv_impscad).ToString(oApp.FormatImporti) & " - " & _
                             NTSCDec(dtrT!sc_importo).ToString(oApp.FormatImporti)
        Next
        ThrowRemoteEvent(New NTSEventArgs("", strMsg))
      End If    'If dttTmp.Rows.Count > 0 Then
      dttTmp.Clear()

      '------------------------
      'prima le rimesse dirette (pv_tippaga >= 3): posso fare tutto con una unica query
      'si guarderà in scaden se effettivamente la rata risulta essere incassata e la data di riferimento 
      'è scaden.sc_dtsaldato
      If Not oCldElmp.IncassatoNewAggMaturatoSuRD(strDittaCorrente, strData) Then Return False

      '------------------------
      'ora le tratte/riba
      'calcolo la nuova data scadenza effettiva nel caso di raggruppamento effetti
      oCldElmp.ValCodiceDb("1", strDittaCorrente, "TABPECG", "N", "", dttTmp)
      nCausRagg = NTSCInt(dttTmp.Rows(0)!tb_cauraggef)
      dttTmp.Clear()

      'ricalcolo la data scadenza effettiva prendendo la data scadenza originaria, poi
      'se c'è un raggruppamento effetti, correggo la data effettiva prendendo 
      'provvig inner join scaden per partita, da scaden sc_rgsaldato prendo prinot con causale raggruppamento effetti
      'con data/num reg. di prinot mi collego a scaden con sc_datreg, sc_numreg (ovvero scadenza generata da raggruppam effetti)
      'e ottengo nuova data scadenza
      If Not oCldElmp.IncassatoNewAggDatScadEffSuRB(strDittaCorrente, strData, nCausRagg) Then Return False

      'ora calcolo il maturato:
      'nella IncassatoNewAggDatScadEffSuRB ho memorizzato in provvig.pv_scflsaldato e pv_dtsaldato
      'le RB/tratte effettivamente chiuse, escludendo le riemissioni la cui nuova scadenza non è saldata.

      'il cli viene chiuso all'effettivo incasso (bRbNoChiucli = true), maturo i record con provvig inner join scaden saldati
      'e scaden.sc_dtsaldato <= data fine elaborazione
      'se c'è stato raggruppamento effetti devo scartare gli effetti raggruppati non saldati e sc_dtsaldato deve essere 
      'quella dell'effetto raggruppato
      'il cliente viene chiuso con l'emissione effetti, maturo i record con provvig inner join scaden saldati
      'e pv_datscadeff + gg esito <= data fine elaborazione
      'se c'è stato raggruppamento effetti devo scartare gli effetti raggruppati non saldati

      If Not oCldElmp.IncassatoNewAggMaturatoSuRB(strDittaCorrente, strData, bRbNoChiucli) Then Return False

      Return True

    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function GetSettingBus() As Boolean
    Dim dttTmp As New DataTable
    Try
      bMaturaSoloRDTotInc = CBool(oCldElmp.GetSettingBus("BSPRELMP", "OPZIONI", ".", "MaturaSoloRDTotInc", "0", " ", "0"))
      bSbloccaSoloFinoaData = CBool(oCldElmp.GetSettingBus("BSPRELMP", "OPZIONI", ".", "SbloccaSoloFinoaData", "0", " ", "0"))

      '-------------------------
      'determino il sistema di maturazione in caso di 'incassato'
      oCldElmp.ValCodiceDb(strDittaCorrente, strDittaCorrente, "ANADITAC", "S", "", dttTmp)
      bProvvig2 = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_provvig2) = "S", True, False))
      bRbNoChiucli = CBool(IIf(NTSCStr(dttTmp.Rows(0)!ac_gestefcc) = "N", True, False))
      '--------------------------------------------------------------------------------------------------------------
      bEseguiTestPreElab = CBool(oCldElmp.GetSettingBus("BSPRELMP", "OPZIONI", ".", "EseguiTestPreElab", "0", " ", "0"))
      bVisualizzaFilePreElab = CBool(oCldElmp.GetSettingBus("BSPRELMP", "OPZIONI", ".", "VisualizzaFilePreElab", "-1", " ", "-1"))
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
    Finally
      dttTmp.Clear()
    End Try
  End Function

  Public Overridable Function TestPreElabora(ByVal strData As String) As Boolean
    Dim nCodage As Integer = 0
    Dim strRiga As String = ""
    Dim dttProvvig As New DataTable
    Dim dtrT() As DataRow
    Dim lw1 As StreamWriter = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se l'opzione di registro relativa al controllo pre-elaborazione non è attiva (default), 
      '--- esce restiruendo True
      '--------------------------------------------------------------------------------------------------------------
      If bEseguiTestPreElab = False Then Return True
      '--------------------------------------------------------------------------------------------------------------
      If bProvvig2 = True Then
        If oCldElmp.TestPreElabora_2(strDittaCorrente, strData, bRbNoChiucli, dttProvvig) = False Then
          GoTo ScritturaFileDiLOG
        End If
      End If
      If oCldElmp.TestPreElabora_1(strDittaCorrente, strData, bMaturaSoloRDTotInc, bProvvig2, dttProvvig) = True Then
        Return True
      End If
      '--------------------------------------------------------------------------------------------------------------
ScritturaFileDiLOG:
      '--------------------------------------------------------------------------------------------------------------
      dtrT = dttProvvig.Select("", "pv_codage")
      '--------------------------------------------------------------------------------------------------------------
      '--- Se arrivato qui, vuol dire che il DataSet contiene dati con Maturato negativo
      '--------------------------------------------------------------------------------------------------------------
      lw1 = New StreamWriter(oApp.Dir & "\" & "BSPRELMP.log", False)
      lw1.WriteLine("--------------------------------------------------------------------")
      lw1.WriteLine("ELABORAZIONE DELLE PROVVIGIONI MATURATE (Controllo pre-elaborazione)")
      lw1.WriteLine("Operazione avviata il " + NTSCStr(Now()))
      lw1.WriteLine("Dall'operatore: " & oApp.User.Nome)
      For i As Integer = 0 To (dtrT.Length - 1)
        With dtrT(i)
          '----------------------------------------------------------------------------------------------------------
          If NTSCInt(!pv_codage) <> nCodage Then
            lw1.WriteLine("--------------------------------------------------------------------")
            lw1.WriteLine("--> Agente: " & NTSCStr(!pv_codage) & _
              IIf(NTSCStr(!tb_descage).Trim <> "", " - ", "").ToString & NTSCStr(!tb_descage).Trim)
            lw1.WriteLine("--------------------------------------------------------------------")
          End If
          '----------------------------------------------------------------------------------------------------------
          strRiga = "".PadLeft(4) & "Origine: "
          Select Case NTSCStr(!pv_origine)
            Case "M" : strRiga += "Da generazione provvigioni"
            Case "T" : strRiga += "Da generazione provvigioni nuovo incassato"
            Case "C" : strRiga += "Integrazione o storno (gestione manuale)"
            Case "A" : strRiga += "Acconto provvigioni"
            Case "S" : strRiga += "Storno di provvigioni già corrisposte"
            Case "B" : strRiga += "Storno acconto provvigioni"
            Case "X" : strRiga += "Simulazione"
            Case "Z" : strRiga += "Provvigioni nuovo incassato non collegate a scadenze"
          End Select
          strRiga += ", "
          Select Case NTSCStr(!pv_tipdoc)
            Case "A" : strRiga += "Fattura Immediata Emessa"
            Case "B" : strRiga += "D.D.T. Emesso"
            Case "C" : strRiga += "Corrispettivo Emesso"
            Case "D" : strRiga += "Fattura Differita Emessa"
            Case "E" : strRiga += "Nota di Addebito Emessa"
            Case "N" : strRiga += "Nota di Accredito Emessa"
            Case "£" : strRiga += "Nota di Accredito Differita Emessa"
            Case "F" : strRiga += "Ricevuta Fiscale Emessa"
            Case "S" : strRiga += "Fattura/Ricevuta Fiscale Emessa"
            Case "P" : strRiga += "Fattura/Ricevuta Fiscale Differita"
            Case "R" : strRiga += "Impegno Cliente"
            Case "M" : strRiga += "D.D.T. Ricevuto"
            Case " " : strRiga += "Altro"
            Case "@" : strRiga += "Premio da Promozione Differita"
          End Select
          strRiga += " N°" & NTSCStr(!pv_numdoc) & _
            IIf(NTSCStr(!pv_serie).Trim <> "", "/", "").ToString & NTSCStr(!pv_serie).Trim & _
            IIf(NTSCInt(!pv_anno) > 1900, " del " & NTSCStr(!pv_anno), "").ToString & _
            IIf(NTSCInt(!pv_numrat) <> 0, " N°rata: " & NTSCStr(!pv_numrat), "").ToString & _
            ", Maturato: " & Format(NTSCDec(!pv_impvmat), oApp.FormatImporti)
          '----------------------------------------------------------------------------------------------------------
          lw1.WriteLine(strRiga)
          '----------------------------------------------------------------------------------------------------------
          nCodage = NTSCInt(!pv_codage)
          '----------------------------------------------------------------------------------------------------------
        End With
      Next i
      lw1.WriteLine("--------------------------------------------------------------------")
      '--------------------------------------------------------------------------------------------------------------
      lw1.Flush()
      lw1.Close()
      '--------------------------------------------------------------------------------------------------------------
      dttProvvig.Clear()
      dttProvvig.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      Return False
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    Finally
      dttProvvig.Clear()
      dttProvvig.Dispose()
    End Try
  End Function

End Class
