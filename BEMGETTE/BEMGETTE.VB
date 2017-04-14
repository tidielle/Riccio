Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Tcp

Public Class CLEMGETTE
  Inherits CLE__BASE

  Public oCldEtte As CLDMGETTE
  Public dsEtbaShared As DataSet
  Public bDaTc As Boolean = False
  Public dttTc As DataTable

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

  Public Overrides Function Init(ByRef App As CLE__APP, _
                                    ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                                    ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                                    ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDMGETTE"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldEtte = CType(MyBase.ocldBase, CLDMGETTE)
    oCldEtte.Init(oApp)

    Return True
  End Function

  Public Overridable Function IstanziaNTSCondCommerciali() As NTSCondCommerciali
    Try
      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovr rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEMGETTE", "BN__STD.NTSCondCommerciali", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 127791222114531250, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return Nothing
      End If
      Return CType(oTmp, NTSCondCommerciali)
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    End Try
    Return Nothing
  End Function

  Public Overridable Function InserDatiTT() As Boolean

    Dim dsTmp As DataSet = Nothing
    Dim strDatini As String = ""
    Dim strDatfin As String = ""
    Try

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

  Public Overridable Function ValidaCodice(ByVal strCod As String, ByVal strTipo As String, ByVal strTabella As String, _
                                           ByRef strDesc As String) As Boolean
    Try
      Return oCldEtte.ValCodiceDb(strCod, strDittaCorrente, strTabella, strTipo, strDesc)
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

  Public Overridable Function DaMagazzino(ByVal strDoc As String, ByVal lAnno As Integer, ByVal strSerie As String, _
                                          ByVal lNumDoc As Integer, ByRef dttTmp As DataTable) As Boolean
    Try
      oCldEtte.LeggiDatiMagaz(strDittaCorrente, strDoc, lAnno, strSerie, lNumDoc, dttTmp)

      If dttTmp.Rows.Count = 0 Then
        dttTmp.Dispose()
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128662863951718750, "Documento di magazzino non trovato.")))
        Return False
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
  Public Overridable Function DaOrdini(ByVal strDoc As String, ByVal lAnno As Integer, ByVal strSerie As String, _
                                          ByVal lNumDoc As Integer, ByRef dttTmp As DataTable) As Boolean
    Try
      oCldEtte.LeggiDatiOrdini(strDittaCorrente, strDoc, lAnno, strSerie, lNumDoc, dttTmp)

      If dttTmp.Rows.Count = 0 Then
        dttTmp.Dispose()
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128662863917031250, "Ordine non trovato")))
        Return False
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

  Public Overridable Function LeggiTable(ByVal strOrigine As String, ByVal bBD_DaCodarfo As Boolean, ByVal strArtico As String, ByVal strArticoli As String, _
                                         ByVal strWhereArtico As String, ByVal lLista As Integer, ByVal strTipodoc As String, ByVal bCreaBarcode As Boolean, _
                                         ByVal lDaclie As Integer, ByVal lAclie As Integer, ByVal dtDadata As Date, ByVal dtAdata As Date, ByVal lDaanno As Integer, _
                                         ByVal lAanno As Integer, ByVal strDaserie As String, ByVal strAserie As String, ByVal lDanumero As Integer, _
                                         ByVal lAnumero As Integer, ByVal lDariga As Integer, ByVal lAriga As Integer, ByVal bColli As Boolean, ByRef dttOut As DataTable, _
                                         ByRef dttTmp As DataTable, ByRef dttTmp2 As DataTable, ByRef bDocMagazz As Boolean, ByRef bDaDoc As Boolean, _
                                         ByRef strCode As String, ByRef strCodartList As String, ByRef strCodart As String, ByRef bAvvisare As Boolean) As Boolean

    Dim dQtaCon2 As Decimal
    Dim i As Integer
    Dim j As Integer
    Try
      ThrowRemoteEvent(New NTSEventArgs("LABEL", oApp.Tr(Me, 129030099551738143, "Preparazione dati in corso...")))

      'Riempie la tabella temporanea
      If strOrigine = "A" And bDaTc = False Then
        'Selezione articoli
        oCldEtte.SelezionaArtico(strDittaCorrente, bBD_DaCodarfo, strArtico, strArticoli, strWhereArtico, dttOut)

        If dttOut.Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129030099020151829, "Nessun articolo selezionato.")))
          Return False
        End If
      ElseIf strOrigine = "L" Then
        'Lista selezionata di articoli
        oCldEtte.SelezionaLista(strDittaCorrente, lLista, dttOut)

        If dttOut.Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129030099223129672, "Nessun articolo selezionato.")))
          Return False
        End If
      ElseIf strOrigine = "M" Then
        'Documenti/Ordini
        bDaDoc = True
        Select Case strTipodoc
          Case "A", "B", "C", "D", "E", "F", "I", "J", "K", "L", "M", "N", "P", "S", "T", "U", "Z", "W"
            bDocMagazz = True
          Case "$", "H", "O", "Q", "R", "V", "X", "Y"
            bDocMagazz = False
        End Select
        '------------------------------------------------------------------------------------
        'Se richiesto crea il barcode
        If bCreaBarcode Then
          ThrowRemoteEvent(New NTSEventArgs("LABEL", oApp.Tr(Me, 128665635980657051, "Creazione barcode mancanti in corso...")))
          If Not oCldEtte.SelezionaMagazzino(strDittaCorrente, strTipodoc, lDaclie, lAclie, dtDadata, dtAdata, lDaanno, lAanno, _
                                             strDaserie, strAserie, lDanumero, lAnumero, lDariga, lAriga, bDocMagazz, dttOut) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)

          For i = 0 To dttOut.Rows.Count - 1
            'Determina il codice articolo:
            'CODART-QUANTITACONFEZIONE
            If NTSCDec(dttOut.Rows(i)!Qtacon2) = 0 Then
              dQtaCon2 = 1
            Else
              dQtaCon2 = NTSCDec(dttOut.Rows(i)!QtaCon2)
            End If
            strCode = NTSCStr(dttOut.Rows(i)!Codart)
            If dQtaCon2 > 1 Then strCode &= "-" & NTSCInt(dQtaCon2)
            strCode = Left(strCode, 18)
            'Controlla se già presente
            If Not oCldEtte.IsInBarcode(NTSCStr(dttOut.Rows(i)!Codart), strCode, dttTmp) Then
              If dttTmp.Rows.Count <> 0 Then
                If UCase(NTSCStr(dttOut.Rows(i)!Codart)) = UCase(NTSCStr(dttTmp.Rows(0)!bc_codart)) Then
                  If Not oCldEtte.MakeBarcode(strDittaCorrente, NTSCStr(dttOut.Rows(i)!Codart), NTSCDate(dttOut.Rows(i)!DtDoc), strCode, NTSCStr(dttOut.Rows(i)!UMP), dQtaCon2) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)
                Else
                  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128665731569807988, "Attenzione! il codice a barre '|" & UCase(strCode) & "|' che si stà inserendo sull'articolo '|" & UCase(strCodart) & _
                         "|', risulta già associato all'articolo '|" & UCase(NTSCStr(dttTmp.Rows(0)!bc_codart)) & "|'. Codice a barre non creato.")))
                End If
              End If
            End If
          Next
          ThrowRemoteEvent(New NTSEventArgs("LABEL", oApp.Tr(Me, 129030099342197506, "Preparazione dati in corso...")))
        End If
        '------------------------------------------------------------------------------------
        dttOut = Nothing
        If Not oCldEtte.SelezionaDaDocMaga(strDittaCorrente, strTipodoc, lDaclie, lAclie, dtDadata, dtAdata, lDaanno, lAanno, _
                                           strDaserie, strAserie, lDanumero, lAnumero, lDariga, lAriga, bDocMagazz, bColli, dttOut) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)

        If dttOut.Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 129030099382980583, "Nessun articolo selezionato.")))
          Return False
        End If
        'Determina il totale record
      ElseIf strOrigine = "A" And bDaTc = True Then
        oCldEtte.SelezionaArtico(strDittaCorrente, False, strArtico, strArticoli, strWhereArtico, dttOut)
        dttOut.Columns.Add("xx_tagl")
        dttOut.Columns.Add("xx_qta")
        dttOut.AcceptChanges()
        For j = 0 To dttTc.Rows.Count - 1
          If NTSCInt(dttTc.Rows(j)!xx_qta) <> 0 Then
            dttOut.ImportRow(dttOut.Rows(0))
            dttOut.Rows(dttOut.Rows.Count - 1)!xx_tagl = NTSCStr(dttTc.Rows(j)!xx_tagl)
            dttOut.Rows(dttOut.Rows.Count - 1)!xx_qta = NTSCInt(dttTc.Rows(j)!xx_qta)
          End If
        Next
        dttOut.Rows(0).Delete()
        dttOut.AcceptChanges()
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
  Public Overridable Function RiempiTmpTable(ByVal bPrezzi As Boolean, ByVal bDatamod As Boolean, ByVal dtDatamod As Date, _
                                             ByVal bSolobar As Boolean, ByVal strSolobar As String, ByVal strUmBC As String, ByVal bArtbar As Boolean, _
                                             ByVal strOrigine As String, ByVal bBD_DaCodarfo As Boolean, ByVal lLista As Integer, _
                                             ByVal bColli As Boolean, ByVal bNEtichetteBarCode As Boolean, ByVal bPrezzival As Boolean, _
                                             ByVal lListino As Integer, ByVal dtValidita As Date, ByVal lValuta As Integer, _
                                             ByVal bMoltdiv As Boolean, ByVal strQualeOpt As String, ByVal bMisura1 As Boolean, _
                                             ByVal lNumMolt As Integer, ByVal bDividiQuantConfez As Boolean, ByVal lIIstMatr As Integer, _
                                             ByVal lIIstMats As Integer, ByVal bUsaTtstMatr As Boolean, ByVal lNumEtiche As Integer, _
                                             ByVal nBD_Listino As Integer, ByVal bModTCO As Boolean, ByVal strTaglia As String, _
                                             ByVal lIIstMatrTmp As Integer, ByVal bStampaNegativi As Boolean, ByRef dttOut As DataTable, _
                                             ByRef dttTmp As DataTable, ByRef dttTmp2 As DataTable, ByRef bDocMagazz As Boolean, ByRef bDaDoc As Boolean, _
                                             ByRef strCode As String, ByRef strCodartList As String, ByRef strCodart As String, ByRef bAvvisare As Boolean) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      Return RiempiTmpTable(bPrezzi, bDatamod, dtDatamod, bSolobar, strSolobar, strUmBC, bArtbar, strOrigine, _
        bBD_DaCodarfo, lLista, bColli, bNEtichetteBarCode, bPrezzival, lListino, dtValidita, lValuta, bMoltdiv, _
        strQualeOpt, bMisura1, lNumMolt, bDividiQuantConfez, lIIstMatr, lIIstMats, bUsaTtstMatr, lNumEtiche, _
        nBD_Listino, bModTCO, strTaglia, lIIstMatrTmp, bStampaNegativi, dttOut, dttTmp, dttTmp2, bDocMagazz, bDaDoc, _
        strCode, strCodartList, strCodart, bAvvisare, 0)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
  Public Overridable Function RiempiTmpTable(ByVal bPrezzi As Boolean, ByVal bDatamod As Boolean, ByVal dtDatamod As Date, _
                                             ByVal bSolobar As Boolean, ByVal strSolobar As String, ByVal strUmBC As String, ByVal bArtbar As Boolean, _
                                             ByVal strOrigine As String, ByVal bBD_DaCodarfo As Boolean, ByVal lLista As Integer, _
                                             ByVal bColli As Boolean, ByVal bNEtichetteBarCode As Boolean, ByVal bPrezzival As Boolean, _
                                             ByVal lListino As Integer, ByVal dtValidita As Date, ByVal lValuta As Integer, _
                                             ByVal bMoltdiv As Boolean, ByVal strQualeOpt As String, ByVal bMisura1 As Boolean, _
                                             ByVal lNumMolt As Integer, ByVal bDividiQuantConfez As Boolean, ByVal lIIstMatr As Integer, _
                                             ByVal lIIstMats As Integer, ByVal bUsaTtstMatr As Boolean, ByVal lNumEtiche As Integer, _
                                             ByVal nBD_Listino As Integer, ByVal bModTCO As Boolean, ByVal strTaglia As String, _
                                             ByVal lIIstMatrTmp As Integer, ByVal bStampaNegativi As Boolean, ByRef dttOut As DataTable, _
                                             ByRef dttTmp As DataTable, ByRef dttTmp2 As DataTable, ByRef bDocMagazz As Boolean, ByRef bDaDoc As Boolean, _
                                             ByRef strCode As String, ByRef strCodartList As String, _
                                             ByRef strCodart As String, ByRef bAvvisare As Boolean, _
                                             ByVal nEtichetteAggPerTC As Integer) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bPrezzi, bDatamod, dtDatamod, bSolobar, strSolobar, strUmBC, _
                                             bArtbar, strOrigine, bBD_DaCodarfo, lLista, bColli, bNEtichetteBarCode, _
                                             bPrezzival, lListino, dtValidita, lValuta, bMoltdiv, strQualeOpt, _
                                             bMisura1, lNumMolt, bDividiQuantConfez, lIIstMatr, lIIstMats, _
                                             bUsaTtstMatr, lNumEtiche, nBD_Listino, bModTCO, strTaglia, _
                                             lIIstMatrTmp, bStampaNegativi, dttOut, dttTmp, dttTmp2, bDocMagazz, _
                                             bDaDoc, strCode, strCodartList, strCodart, bAvvisare, nEtichetteAggPerTC})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(30), DataTable)
        dttTmp = CType(oIn(31), DataTable)
        dttTmp2 = CType(oIn(32), DataTable)
        bDocMagazz = CBool(oIn(33))
        bDaDoc = CBool(oIn(34))
        strCode = NTSCStr(oIn(35))
        strCodartList = NTSCStr(oIn(36))
        strCodart = NTSCStr(oIn(37))
        bAvvisare = CBool(oIn(38))
        Return CBool(oOut)
      End If

      Return RiempiTmpTable(bPrezzi, bDatamod, dtDatamod, bSolobar, strSolobar, strUmBC, bArtbar, strOrigine, bBD_DaCodarfo, lLista, _
                            bColli, bNEtichetteBarCode, bPrezzival, lListino, dtValidita, lValuta, bMoltdiv, strQualeOpt, bMisura1, _
                            lNumMolt, bDividiQuantConfez, lIIstMatr, lIIstMats, bUsaTtstMatr, lNumEtiche, nBD_Listino, bModTCO, strTaglia, _
                            lIIstMatrTmp, bStampaNegativi, dttOut, dttTmp, dttTmp2, bDocMagazz, bDaDoc, strCode, strCodartList, _
                            strCodart, bAvvisare, nEtichetteAggPerTC, True)
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
  Public Overridable Function RiempiTmpTable(ByVal bPrezzi As Boolean, ByVal bDatamod As Boolean, ByVal dtDatamod As Date, _
                                           ByVal bSolobar As Boolean, ByVal strSolobar As String, ByVal strUmBC As String, ByVal bArtbar As Boolean, _
                                           ByVal strOrigine As String, ByVal bBD_DaCodarfo As Boolean, ByVal lLista As Integer, _
                                           ByVal bColli As Boolean, ByVal bNEtichetteBarCode As Boolean, ByVal bPrezzival As Boolean, _
                                           ByVal lListino As Integer, ByVal dtValidita As Date, ByVal lValuta As Integer, _
                                           ByVal bMoltdiv As Boolean, ByVal strQualeOpt As String, ByVal bMisura1 As Boolean, _
                                           ByVal lNumMolt As Integer, ByVal bDividiQuantConfez As Boolean, ByVal lIIstMatr As Integer, _
                                           ByVal lIIstMats As Integer, ByVal bUsaTtstMatr As Boolean, ByVal lNumEtiche As Integer, _
                                           ByVal nBD_Listino As Integer, ByVal bModTCO As Boolean, ByVal strTaglia As String, _
                                           ByVal lIIstMatrTmp As Integer, ByVal bStampaNegativi As Boolean, ByRef dttOut As DataTable, _
                                           ByRef dttTmp As DataTable, ByRef dttTmp2 As DataTable, ByRef bDocMagazz As Boolean, ByRef bDaDoc As Boolean, _
                                           ByRef strCode As String, ByRef strCodartList As String, _
                                           ByRef strCodart As String, ByRef bAvvisare As Boolean, _
                                           ByVal nEtichetteAggPerTC As Integer, ByVal bUsaNoteDaLista As Boolean) As Boolean
    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bPrezzi, bDatamod, dtDatamod, bSolobar, strSolobar, strUmBC, _
                                             bArtbar, strOrigine, bBD_DaCodarfo, lLista, bColli, bNEtichetteBarCode, _
                                             bPrezzival, lListino, dtValidita, lValuta, bMoltdiv, strQualeOpt, _
                                             bMisura1, lNumMolt, bDividiQuantConfez, lIIstMatr, lIIstMats, _
                                             bUsaTtstMatr, lNumEtiche, nBD_Listino, bModTCO, strTaglia, _
                                             lIIstMatrTmp, bStampaNegativi, dttOut, dttTmp, dttTmp2, bDocMagazz, _
                                             bDaDoc, strCode, strCodartList, strCodart, bAvvisare, nEtichetteAggPerTC, bUsaNoteDaLista})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(30), DataTable)
        dttTmp = CType(oIn(31), DataTable)
        dttTmp2 = CType(oIn(32), DataTable)
        bDocMagazz = CBool(oIn(33))
        bDaDoc = CBool(oIn(34))
        strCode = NTSCStr(oIn(35))
        strCodartList = NTSCStr(oIn(36))
        strCodart = NTSCStr(oIn(37))
        bAvvisare = CBool(oIn(38))
        Return CBool(oOut)
      End If

      Return RiempiTmpTable(bPrezzi, bDatamod, dtDatamod, bSolobar, strSolobar, strUmBC, _
                            bArtbar, strOrigine, bBD_DaCodarfo, lLista, bColli, bNEtichetteBarCode, _
                            bPrezzival, lListino, dtValidita, lValuta, bMoltdiv, strQualeOpt, _
                            bMisura1, lNumMolt, bDividiQuantConfez, lIIstMatr, lIIstMats, _
                            bUsaTtstMatr, lNumEtiche, nBD_Listino, bModTCO, strTaglia, _
                            lIIstMatrTmp, bStampaNegativi, dttOut, dttTmp, dttTmp2, bDocMagazz, _
                            bDaDoc, strCode, strCodartList, strCodart, bAvvisare, nEtichetteAggPerTC, bUsaNoteDaLista, False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function RiempiTmpTable(ByVal bPrezzi As Boolean, ByVal bDatamod As Boolean, ByVal dtDatamod As Date, _
                                           ByVal bSolobar As Boolean, ByVal strSolobar As String, ByVal strUmBC As String, ByVal bArtbar As Boolean, _
                                           ByVal strOrigine As String, ByVal bBD_DaCodarfo As Boolean, ByVal lLista As Integer, _
                                           ByVal bColli As Boolean, ByVal bNEtichetteBarCode As Boolean, ByVal bPrezzival As Boolean, _
                                           ByVal lListino As Integer, ByVal dtValidita As Date, ByVal lValuta As Integer, _
                                           ByVal bMoltdiv As Boolean, ByVal strQualeOpt As String, ByVal bMisura1 As Boolean, _
                                           ByVal lNumMolt As Integer, ByVal bDividiQuantConfez As Boolean, ByVal lIIstMatr As Integer, _
                                           ByVal lIIstMats As Integer, ByVal bUsaTtstMatr As Boolean, ByVal lNumEtiche As Integer, _
                                           ByVal nBD_Listino As Integer, ByVal bModTCO As Boolean, ByVal strTaglia As String, _
                                           ByVal lIIstMatrTmp As Integer, ByVal bStampaNegativi As Boolean, ByRef dttOut As DataTable, _
                                           ByRef dttTmp As DataTable, ByRef dttTmp2 As DataTable, ByRef bDocMagazz As Boolean, ByRef bDaDoc As Boolean, _
                                           ByRef strCode As String, ByRef strCodartList As String, _
                                           ByRef strCodart As String, ByRef bAvvisare As Boolean, _
                                           ByVal nEtichetteAggPerTC As Integer, ByVal bUsaNoteDaLista As Boolean, ByVal bPrzNetClasseSconto As Boolean) As Boolean
    Dim oCondCommerciali As NTSCondCommerciali = Nothing
    Dim bContinua As Boolean = True
    Dim lFase As Integer
    Dim bCode As Boolean
    Dim lMaxCounter As Integer
    Dim dDivMolMisura1 As Decimal
    Dim lCounter As Integer
    Dim bArticoloTeC As Boolean
    Dim dtDataAggList As Date
    Dim dPrezzo As Decimal
    Dim dPrezval As Decimal
    Dim dQuant As Decimal = 0
    Dim i As Integer = 0
    Dim x As Integer = 0
    Dim y As Integer = 0
    Dim j As Integer = 0
    Dim k As Integer = 0

    Try
      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {bPrezzi, bDatamod, dtDatamod, bSolobar, strSolobar, strUmBC, _
                                             bArtbar, strOrigine, bBD_DaCodarfo, lLista, bColli, bNEtichetteBarCode, _
                                             bPrezzival, lListino, dtValidita, lValuta, bMoltdiv, strQualeOpt, _
                                             bMisura1, lNumMolt, bDividiQuantConfez, lIIstMatr, lIIstMats, _
                                             bUsaTtstMatr, lNumEtiche, nBD_Listino, bModTCO, strTaglia, _
                                             lIIstMatrTmp, bStampaNegativi, dttOut, dttTmp, dttTmp2, bDocMagazz, _
                                             bDaDoc, strCode, strCodartList, strCodart, bAvvisare, nEtichetteAggPerTC, bUsaNoteDaLista, bPrzNetClasseSconto})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        dttOut = CType(oIn(30), DataTable)
        dttTmp = CType(oIn(31), DataTable)
        dttTmp2 = CType(oIn(32), DataTable)
        bDocMagazz = CBool(oIn(33))
        bDaDoc = CBool(oIn(34))
        strCode = NTSCStr(oIn(35))
        strCodartList = NTSCStr(oIn(36))
        strCodart = NTSCStr(oIn(37))
        bAvvisare = CBool(oIn(38))
        Return CBool(oOut)
      End If
      '----------------
      'Riempie il temporaneo
      lCounter = 0
      If bPrezzi Then
        'Prepara la struttura per elaborare i prezzi
        oCondCommerciali = IstanziaNTSCondCommerciali()
        oCondCommerciali.bCalcolaPrezzo = True
        oCondCommerciali.Input.strDitta = strDittaCorrente
        oCondCommerciali.Input.strTipoval = "P"
        oCondCommerciali.Input.bConspromo = True
        oCondCommerciali.Input.dtDatdoc = dtValidita
      End If
      For i = 0 To dttOut.Rows.Count - 1
        bContinua = True
        '---------------------------------------------------------------------------------
        If bNEtichetteBarCode Then
          ' con opzione che visualizza la finestra su quante etichette stampare per ogni BARCODE
          If bBD_DaCodarfo And strOrigine = "A" Then
            strCodartList = NTSCStr(dttOut.Rows(i)!caf_codart)
          Else
            strCodartList = NTSCStr(dttOut.Rows(i)!Codart)
          End If
          strCodart = NTSCStr(dttOut.Rows(i)!Codart)

          'Determina i prezzi
          If bPrezzi Then
            'determina la fase
            lFase = 0
            If Not oCldEtte.SelectFasi(strCodartList, dttTmp) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)
            If dttTmp.Rows.Count <> 0 AndAlso NTSCStr(dttTmp.Rows(0)!ar_gesfasi) = "S" Then lFase = NTSCInt(dttTmp.Rows(0)!ar_ultfase)
            dttTmp = Nothing

            'Legge il prezzo (non applico un quetso caso il filtro data agg listino
            '-----------------------------------------------
            oCondCommerciali.SvuotaOutput()
            With oCondCommerciali.Input
              .strCodart = strCodart
              .nListino = lListino
              .nFase = lFase
            End With
            '-----------------------------------------------
            CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
            dPrezzo = oCondCommerciali.OutputPrezzo.dPrezzo
            '-----------------------------------------------
            If bPrezzival Then
              oCondCommerciali.SvuotaOutput()
              oCondCommerciali.Input.nCodvalu = lValuta
              '-----------------------------------------------
              CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
              '-----------------------------------------------
              dPrezval = oCondCommerciali.OutputPrezzo.dPrezzo
            Else
              dPrezval = 0
            End If
          Else
            dPrezzo = 0
            dPrezval = 0
          End If

          PrezzoNetto(bPrzNetClasseSconto, strCodartList, lListino, dtValidita, dPrezzo, dPrezval)

          'Quante etichette stampare?
          bCode = False
          If Not oCldEtte.SelectEtichetteStp(strDittaCorrente, NTSCStr(dttOut.Rows(i)!Codart), lIIstMatrTmp, dttTmp2) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)

          If dttTmp2.Rows.Count <> 0 Then
            For x = 0 To dttTmp2.Rows.Count - 1
              bCode = True
              lMaxCounter = NTSCInt(dttTmp2.Rows(x)!tt_quant)

              If strOrigine = "M" Then
                If bMoltdiv Then
                  dDivMolMisura1 = ArrDbl(NTSCDec(dttOut.Rows(i)!Misura1), 0)
                  If dDivMolMisura1 < 1 Then dDivMolMisura1 = 1
                  If strQualeOpt = "1" Then
                    'moltiplicatore
                    lMaxCounter = NTSCInt(ArrDblEcc(lMaxCounter * NTSCDec(IIf(bMisura1, dDivMolMisura1, lNumMolt))))
                  Else
                    'divisore
                    If lNumMolt = 0 Then
                      lMaxCounter = 0
                    Else
                      lMaxCounter = NTSCInt(ArrDblEcc(lMaxCounter / NTSCDec(IIf(bMisura1, dDivMolMisura1, lNumMolt)), 0))
                    End If
                  End If
                End If

                'Ora se abilitata opzione DividiQuantConfez e se vengono lette le quantità (no i colli) e nn ci sono moltiplicatori/divisori
                If bDividiQuantConfez And Not bMoltdiv And Not bColli And NTSCDec(dttOut.Rows(i)!Qtacon2) <> 0 Then
                  lMaxCounter = NTSCInt(ArrDblEcc(lMaxCounter / NTSCDec(dttTmp.Rows(i)!Qtacon2), 0))
                  If lMaxCounter = 0 Then lMaxCounter = 1
                End If
              End If

              strCode = Left(NTSCStr(dttTmp2.Rows(x)!tt_matric), 30)
              If strCode = "" Then strCode = " "

              'Looppa tante volte ...
              For y = 1 To lMaxCounter
                If y Mod 50 = 0 Or y = 1 Or y = lMaxCounter Then
                  ThrowRemoteEvent(New NTSEventArgs("LABEL", oApp.Tr(Me, 128667428842616443, "Preparazione etichetta n° |" & lCounter + 1 & "| in corso...")))
                End If
                'utilizzando la tabella TTSTMATR è possibile fare  l'aggancio con la tabella movmag ....
                If Not oCldEtte.InsertAggancioMovmag(strDittaCorrente, lIIstMatr, lIIstMats, lCounter, i, strCodart, dPrezzo, dPrezval, _
                                              strCode, strOrigine, bUsaTtstMatr, dttOut) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)

                lCounter += 1

                If Not MaxStampe(lCounter, bAvvisare) Then Exit For
              Next ' For y = 1 To lMaxCounter
            Next ' For x = 0 To dttTmp2.Rows.Count - 1

            If Not oCldEtte.DeleteTTstmatr(strDittaCorrente, lIIstMatrTmp, NTSCStr(dttOut.Rows(i)!Codart), i, dttOut) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)

            If Not MaxStampe(lCounter, bAvvisare) Then Exit For

            GoTo Salta_1
          End If
          dttTmp2 = Nothing

          If Not bCode Then
            If Not oCldEtte.SelectBarcodeStp(strDittaCorrente, NTSCStr(dttOut.Rows(i)!Codart), strSolobar, bSolobar, dttTmp2) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)

            If dttTmp2.Rows.Count = 0 Then
              dttTmp2 = Nothing
            ElseIf dttTmp2.Rows.Count > 1 Then
              dttTmp2 = Nothing
              If Not MaxStampe(lCounter, bAvvisare) Then Exit For
            End If
          End If
        End If

        If bBD_DaCodarfo And strOrigine = "A" Then
          'CASO PARTICOLARE: personalizzazione BEDETTI
          strCodartList = NTSCStr(dttOut.Rows(i)!caf_codart)
          strCodart = NTSCStr(dttOut.Rows(i)!Codart) '(strCodart contiene il cod. articolo cli/forn caf_codarfo)
          bArticoloTeC = False
        Else
          strCodartList = NTSCStr(dttOut.Rows(i)!Codart)
          strCodart = NTSCStr(dttOut.Rows(i)!Codart)
          If NTSCInt(dttOut.Rows(i)!Codtagl) <> 0 Then bArticoloTeC = True Else bArticoloTeC = False
        End If

        'Svuoto il barcode (lo valorizza dopo con una lettura diretta su BARCODE)
        strCode = " "

        'Quante etichette stampare?
        If strOrigine = "A" Then
          lMaxCounter = lNumEtiche
        Else
          If strOrigine = "L" Then
            lMaxCounter = lNumEtiche

            If bUsaNoteDaLista Then
              If Not oCldEtte.NumeroEtichette(strDittaCorrente, strCodart, lLista, dttTmp) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)

              If dttTmp.Rows.Count <> 0 Then
                If (NTSCInt(dttTmp.Rows(0)!lsa_note) >= 1) And (NTSCInt(dttTmp.Rows(0)!lsa_note) <= 10000) Then
                  lMaxCounter = NTSCInt(dttTmp.Rows(0)!lsa_note)
                End If
              End If
            End If
          Else
            'Documenti
            '--------------------------------------------------------------------------------------------------------
            '--- Se attiva l'opzione di registro "BSMGETTE/OPZIONI/ArrotEccessoQuantColli", arrotonda, per eccesso
            '--- la quantità letta da Documenti/Ordini/Impegni
            '--------------------------------------------------------------------------------------------------------
            dQuant = NTSCDec(dttOut.Rows(i)!QUANT)
            If CBool(oCldEtte.GetSettingBus("BSMGETTE", "OPZIONI", ".", "ArrotEccessoQuantColli", "0", " ", "0")) = True Then
              dQuant = ArrDblEcc(dQuant)
            End If
            '--------------------------------------------------------------------------------------------------------
            lMaxCounter = NTSCInt(IIf(bStampaNegativi, Math.Abs(NTSCInt(dQuant)), NTSCInt(dQuant)))
            If bMoltdiv Then
              dDivMolMisura1 = ArrDbl(NTSCDec(dttOut.Rows(i)!Misura1), 0)
              If dDivMolMisura1 < 1 Then dDivMolMisura1 = 1
              If strQualeOpt = "1" Then
                'moltiplicatore
                lMaxCounter = NTSCInt(ArrDblEcc(lMaxCounter * NTSCDec(IIf(bMisura1, dDivMolMisura1, lNumMolt))))
              Else
                'divisore
                If lNumMolt = 0 Then
                  lMaxCounter = 0
                Else
                  lMaxCounter = NTSCInt(ArrDblEcc(lMaxCounter / NTSCDec(IIf(bMisura1, dDivMolMisura1, lNumMolt))))
                End If
              End If
            End If
            'Ora se abilitata opzione DividiQuantConfez e se vengono lette le quantità (no i colli) e nn ci sono moltiplicatori/divisori
            If bDividiQuantConfez And (Not bMoltdiv) And (strQualeOpt = "1") And (NTSCDec(dttOut.Rows(i)!Qtacon2) <> 0) Then
              lMaxCounter = NTSCInt(ArrDblEcc(lMaxCounter / NTSCDec(dttOut.Rows(i)!Qtacon2)))
              If lMaxCounter = 0 Then lMaxCounter = 1
            End If
          End If
        End If

        'Determina i prezzi
        If bPrezzi Then
          'determina la fase
          lFase = 0
          If Not oCldEtte.DeterminaFasi(strCodartList, dttTmp) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)

          If dttTmp.Rows.Count <> 0 Then
            If NTSCStr(dttTmp.Rows(0)!ar_gesfasi) = "S" Then lFase = NTSCInt(dttTmp.Rows(0)!ar_ultfase)
          End If

          'Legge il prezzo
          dtDataAggList = New Date(1900, 1, 1)
          oCondCommerciali.SvuotaOutput()
          With oCondCommerciali.Input
            .strCodart = strCodart
            .nListino = lListino
            .nCodvalu = 0
            .nFase = lFase
          End With
          CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
          dPrezzo = oCondCommerciali.OutputPrezzo.dPrezzo
          dtDataAggList = oCondCommerciali.OutputPrezzo.dtDatAgg
          '--------------------------------
          If bPrezzival Then
            oCondCommerciali.SvuotaOutput()
            oCondCommerciali.Input.nCodvalu = lValuta
            CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
            dPrezval = oCondCommerciali.OutputPrezzo.dPrezzo
            dtDataAggList = oCondCommerciali.OutputPrezzo.dtDatAgg
          Else
            dPrezval = 0
            If nBD_Listino <> 0 Then
              '------------------------------------
              With oCondCommerciali.Input
                .strCodart = strCodart
                .nListino = nBD_Listino
                .nCodvalu = 0
                .nFase = lFase
              End With
              CType(oCleComm, CLELBMENU).CercaCondCommerciali(oCondCommerciali)
              dPrezval = oCondCommerciali.OutputPrezzo.dPrezzo
              dtDataAggList = oCondCommerciali.OutputPrezzo.dtDatAgg
              '------------------------------------
            End If
          End If
          'Se applico il filtro sulla data inizio validità lo controlla ora
          If bDatamod Then
            If dtDataAggList < dtDatamod Then
              bContinua = False
            End If
          End If
        Else
          dPrezzo = 0
          dPrezval = 0
        End If

        PrezzoNetto(bPrzNetClasseSconto, strCodartList, lListino, dtValidita, dPrezzo, dPrezval)

        If strOrigine = "A" And bDaTc Then
          lMaxCounter = NTSCInt(dttOut.Rows(i)!xx_qta)
        End If

        If bContinua Then
          'Looppa tante volte ...
          For y = 1 To lMaxCounter
            If y Mod 50 = 0 Or y = 1 Or y = lMaxCounter Then
              ThrowRemoteEvent(New NTSEventArgs("LABEL", oApp.Tr(Me, 129030099509236239, "Preparazione etichetta n° |" & lCounter + 1 & "| in corso...")))
            End If
            'utilizzando la tabella TTSTMATR è possibile fare  l'aggancio con la tabella movmag ....
            '-------------------------------------------------------------------------------------
            '--- Se è attivo il modulo Taglie e colori
            '--- se il tipo documento e ordine/impegno (TESTORD/MOVORD)
            '--- se tipo stampa è ordini/documenti di magazzino
            '--- se ci sono delle quantità per taglia in MOVORDTC
            '--- scrive tante righe quante sono le quantità per taglia con le descrizioni taglie
            '--- diversificate.
            '--- Altrimenti lavora come prima
            '-------------------------------------------------------------------------------------
            If bModTCO And strOrigine = "M" Then
              oCldEtte.SelectMovJoin(strDittaCorrente, bDocMagazz, dttOut, i, dttTmp)

              If dttTmp.Rows.Count <> 0 Then
                For j = 1 To 24
                  If NTSCInt(dttTmp.Rows(0).Item("Quant" & Right("0" & CStr(j), 2))) > 0 Then
                    'Controllo se esiste (e legge) un barcode specifico per taglia
                    strCode = " "
                    If Not oCldEtte.BarcodeTaglia(strDittaCorrente, strCodart, strSolobar, NTSCStr(dttOut.Rows(i)!UMQuant), _
                                                  NTSCStr(dttTmp.Rows(0).Item("tb_dest" & Right("0" & CStr(j), 2))), bSolobar, dttTmp2) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)
                    If dttTmp2.Rows.Count <> 0 Then strCode = NTSCStr(dttTmp2.Rows(0)!bc_code)

                    'Se non si devono considerare gli articoli senza barcode, passa al prossimo
                    If bSolobar And Trim(strCode) = "" Then Exit For
                    If strCode = "" Then strCode = " "
                    'Ora scrive tante etichette quante sono le quantità
                    For x = 1 To NTSCInt(dttTmp.Rows(0).Item("Quant" & Right("0" & CStr(j), 2))) + nEtichetteAggPerTC
                      If Not oCldEtte.ScriviEtichette(strDittaCorrente, dttOut, dttTmp, bUsaTtstMatr, strCodart, lCounter, strCode, dPrezzo, _
                                                      dPrezval, lIIstMatr, lIIstMats, i, j) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)
                      lCounter += 1
                    Next
                  End If
                Next
                MaxStampe(lCounter, bAvvisare)
                Exit For
              End If
            End If
            '-------------------------------------------------------------------------------------
            'Leggo qui il barcode, sia quello per taglie e colori che per gli articoli normali
            strCode = " "

            If Not (bBD_DaCodarfo And strOrigine = "A") Then
              If Not oCldEtte.SelezionaBarcodeTCO(strDittaCorrente, dttOut, i, strCodart, bSolobar, strOrigine, bArticoloTeC, _
                                                  strSolobar, bColli, bModTCO, strTaglia, strUmBC, dttTmp, bDaTc) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)

              If dttTmp.Rows.Count <> 0 Then
                strCode = NTSCStr(dttTmp.Rows(0)!bc_code)
                If dttOut.Rows.Count > 0 AndAlso dttOut.Columns.Contains("xx_tagl") Then strTaglia = NTSCStr(dttOut.Rows(i)!xx_tagl)
              End If
            Else
              'CASO PARTICOLARE (personalizzazione BEDETTI) il barcode è sempre caf_desnote
              strCode = Left(NTSCStr(dttOut.Rows(i)!BARCODE), 30)
            End If
            'Se non si devono considerare gli articoli senza barcode, passa al prossimo
            If bSolobar And Trim(strCode) = "" Then
              If Not MaxStampe(lCounter, bAvvisare) Then Exit For
            End If
            If strCode = "" Then strCode = " "
            If (bArtbar = True) And (strCode.Trim = "") Then GoTo salta
            '-------------------------------------------------------------------------------------
            'Scrive a riga di etichetta
            If Not oCldEtte.InsertTlbTT(strDittaCorrente, bUsaTtstMatr, strOrigine, lIIstMatr, lIIstMats, strCodart, strCode, _
                                        dPrezzo, dPrezval, dttOut, i, bModTCO, strTaglia, lCounter) Then Return SvuotaDtt(dttOut, dttTmp, dttTmp2, False)

            lCounter += 1
            If Not MaxStampe(lCounter, bAvvisare) Then Exit For
Salta:
            'Ora controlla moltiplicatori/divisori
          Next 'For y = 1 To lMaxCounter
        End If
Salta_1:
        If Not MaxStampe(lCounter, bAvvisare) Then Exit For
      Next ' For i = 0 To dttOut.Rows.Count - 1

      ThrowRemoteEvent(New NTSEventArgs("LABEL", oApp.Tr(Me, 128667428957149159, "Stampa di |" & lCounter & "| etichette in corso...")))

      Return SvuotaDtt(dttOut, dttTmp, dttTmp2, True)
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

  Public Overridable Sub PrezzoNetto(ByVal bPrzNetClasseSconto As Boolean, ByVal strCodart As String, ByVal nClasAn As Integer, _
                                     ByVal dtValidita As Date, ByRef dPrezzo As Decimal, ByRef dPrezval As Decimal)
    Try
      'Calcolo del prezzo al netto della classe di sconto con lo stesso numero di listino associato.
      If bPrzNetClasseSconto Then
        If dPrezzo <> 0 OrElse dPrezval <> 0 Then
          Dim dScont1, dScont2, dScont3, dScont4, dScont5, dScont6 As Decimal
          CType(oCleComm, CLELBMENU).CercaSconti(strDittaCorrente, strCodart, 0, 0, nClasAn, "P", True, 0, dtValidita, 1, dScont1, dScont2, dScont3, dScont4, dScont5, dScont6, 0, "N")
          dPrezzo *= ((100 - dScont1) / 100) * ((100 - dScont2) / 100) * ((100 - dScont3) / 100) * ((100 - dScont4) / 100) * ((100 - dScont5) / 100) * ((100 - dScont6) / 100)
        End If
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
  End Sub


  Public Overridable Function MaxStampe(ByVal lCounter As Integer, ByRef bAvvisare As Boolean) As Boolean
    Try
      If lCounter >= 30000 Then
        If bAvvisare Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128667995564836316, "E' possibile stampare al massimo 30.000 etichette alla volta.")))
          bAvvisare = False
        End If
        Return False
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

  Public Overridable Function SvuotaDtt(ByRef dtt1 As DataTable, ByRef dtt2 As DataTable, ByRef dtt3 As DataTable, _
                                        ByVal bReturn As Boolean) As Boolean
    Try
      If Not dtt1 Is Nothing Then dtt1.Dispose()
      If Not dtt2 Is Nothing Then dtt2.Dispose()
      If Not dtt3 Is Nothing Then dtt3.Dispose()

      Return bReturn
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return bReturn
    End Try
  End Function

  Public Overridable Function SelectBarcode(ByVal bSoloBar As Boolean, ByVal strSolobar As String, ByVal strCodart As String, ByRef dttTmp As DataTable) As Boolean
    Try
      Return oCldEtte.SelectBarcode(strDittaCorrente, bSoloBar, strSolobar, strCodart, dttTmp)
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
  Public Overridable Function SelectTTstmatr(ByVal lIIstMatrTmp As Integer, ByVal strCodart As String, ByRef dttTmp2 As DataTable) As Boolean
    Try
      Return oCldEtte.SelectTTstmatr(strDittaCorrente, lIIstMatrTmp, strCodart, dttTmp2)
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
  Public Overridable Function InsertBarcodeTTstmatr(ByVal lIIstMatrTmp As Integer, ByVal strCodart As String, ByVal strBcCode As String, ByVal dQta As Decimal, _
                                                    ByVal strUM As String) As Boolean
    Try
      Return oCldEtte.InsertBarcodeTTstmatr(strDittaCorrente, lIIstMatrTmp, strCodart, strBcCode, dQta, strUM)
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

  Public Overridable Function DatiTermica(ByVal bUsattstmatr As Boolean, ByVal strOrigine As String, ByVal lIIstMatr As Integer, ByVal lIIstMats As Integer, _
                                          ByRef dttOut As DataTable) As Boolean
    Try
      Return oCldEtte.DatiTermica(strDittaCorrente, bUsattstmatr, strOrigine, lIIstMatr, lIIstMats, dttOut)
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
  Public Overridable Function DatiTermicaDometic(ByVal strOrigine As String, ByVal bUsaTtstMatr As Boolean, ByVal lIIstMatr As Integer, ByVal lIIstMats As Integer, _
                                                 ByRef dttOut As DataTable) As Boolean
    Try
      If Not oCldEtte.DatiTermicaDometic(strDittaCorrente, strOrigine, bUsaTtstMatr, lIIstMatr, lIIstMats, dttOut) Then Return False
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

  Public Overridable Function LeggiArtico(ByVal strCodart As String, ByRef dttArtico As DataTable) As Boolean
    Try
      oCldEtte.ValCodiceDb(strCodart, strDittaCorrente, "ARTICO", "S", "", dttArtico)
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
#Region "ETBA"
  Public Overridable Function ApriEtba(ByVal lEtbaIITTBarcode As Integer, ByVal strCodart As String, ByRef ds As DataSet) As Boolean
    Try
      If Not oCldEtte.ApriEtba(strDittaCorrente, lEtbaIITTBarcode, strCodart, ds) Then Return False

      '--------------------------------------------------------------
      'imposto i valori di default della tabella: con la riga che segue prendo prima i valori dal database
      ocldBase.SetTableDefaultValueFromDB("TTSTMATR", ds)

      SetDefaultValue(ds)

      dsEtbaShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsEtbaShared.Tables("TTSTMATR").ColumnChanging, AddressOf EtbaBeforeColUpdate
      AddHandler dsEtbaShared.Tables("TTSTMATR").ColumnChanged, AddressOf EtbaAfterColUpdate

      bHasChanges = False

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

  Public Overridable Function EtbaTestPreSalva() As Boolean
    '-------------------------------------------------
    'eseguo i controlli necessari prima di fare il salvataggio della riga
    Dim dtrCurrRow As DataRow() = dsEtbaShared.Tables("TTSTMATR").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
    Dim i As Integer = 0
    Try
      For i = 0 To dtrCurrRow.Length - 1
        If NTSCInt(dtrCurrRow(i)!tt_quant) < 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128674272849285840, "La quantità deve essere maggiore o uguale a 0")))
          Return False
        End If
      Next

      Return True
    Catch ex As Exception
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------
      Return False
    End Try
  End Function
  Public Overridable Function EtbaSalva(ByVal bDelete As Boolean, ByVal lIIstMatrTmp As Integer) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non è una delete)
      If Not bDelete Then
        If Not EtbaTestPreSalva() Then Return False
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      If strActLog <> "-1" Then
        bResult = oCldEtte.ScriviTabellaTTSTMATR(strDittaCorrente, dsEtbaShared, lIIstMatrTmp)
      End If

      If bResult Then
        bHasChanges = False
      End If

      Return bResult
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

  Public Overridable Sub EtbaAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()


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
  Public Overridable Sub EtbaBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strErr As String = ""
    Try

      'memorizzo il valore corrente di cella per testarlo nella AfterColUpdate
      'solo se il dato è uguale a quello precedentemente contenuto nella cella
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue += e.Column.ColumnName.ToUpper + ";"
        Return
      End If

      '-------------------------------------------------------------
      'controllo che in una cella short non venga inserito un numero troppo grande
      If Not CheckCellaShort(e, strErr) Then Throw New NTSException(strErr)
      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "EtbaBeforeColUpdate_" & e.Column.ColumnName.ToLower
      Dim fun As System.Reflection.MethodInfo = Me.GetType.GetMethod(strFunction)  'occhio: è case_sensitive!!!!
      If Not fun Is Nothing Then fun.Invoke(Me, New Object() {sender, e})

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
#End Region
End Class