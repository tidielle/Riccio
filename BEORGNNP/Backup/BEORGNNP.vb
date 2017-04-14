Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class CLEORGNNP
  Inherits CLE__BASN

#Region "Variabili"
  Public oCldGnnp As CLDORGNNP
  Public oCleBoll As CLEVEBOLL
  Public dsShared As DataSet
  Public bHasChanges As Boolean

  Public bModExtTCO As Boolean
  Public bModPM As Boolean
  Public lIITTGeGnnp As Integer
  Public lIITTMoPernp As Integer
  Public lIITTDispNet As Integer
  Public lIItttasks As Integer
  Public lIIttproesebappo As Integer
  Public bRistampato As Boolean
  Public bVal As Boolean
  Public bScorp As Boolean
  Public lANumero As Integer
  Public nSvalOpzione As Integer = 3            'di ritorno da FRM__SVAL 3 = usa cambio alla data di elabor e, in mancanza, il cambio più vicino
  Public bRigaFiglio As Boolean
  Public lRigaPadre As Integer
  Public bInCorso As Boolean = False

  Public strFDarave As String
  Public nCausale As Integer
  Public nCausale2 As Integer
  Public nNumFat As Integer

  Public bUltimaNotaGenerataPick As Boolean
  Public lIdpick As Integer
  Public bAggiungiPick As Boolean

  'opzioni di registro
  Public bCancellaNonASaldoNew As Boolean
  Public strCalcPesi As String
  Public nINTRACodrsta As Integer
  Public strINTRACodPort As String
  Public bRicalcPrez As Boolean
  Public bRicalcScon As Boolean
  Public bRicalcProv As Boolean
  Public bPMControllaBaseCostMG As Boolean
  Public bPMSoloTaskRilasciatiMG As Boolean
  Public bRiportaNoteDaImpegno As Boolean
  Public bDeterminaBolliSuOperazEsenti As Boolean
  Public bReprintDoc As Boolean
  Public bMostraListaImpegniAncheSeTuttiNonEvadibili As Boolean

  'seic
  Public bSeicConssoloasa As Boolean
  Public strSeicQuery As String
  Public nSeicEscomp As Integer
  Public bDaSelezioneArticoli As Boolean
  Public lIITTOltomo As Integer
  Public nSeicPriorita As Integer
  Public nSeicMagaz As Integer
  Public strTipoConferma As String
  Public strWhereFiar As String
  Public nSeicClienti As Integer
  Public lSeicClienteini As Integer
  Public lSeicClientefin As Integer
  Public nSeicDate As Integer
  Public strSeicDataini As String
  Public strSeicDatafin As String
  Public nSeicTipo As Integer
  Public strSeicADatacons As String
  Public nSeicBolle As Integer
  Public nSeicTipobf As Integer
  Public nSeicAgenti As Integer
  Public nSeicCodcage As Integer
  Public bSeicTutti As Boolean
  Public bSeicConf As Boolean
  Public nSeicMaxnumord As Integer
  Public nSeicZone As Integer
  Public nSeicCodzona As Integer

  'dtac
  Public nDtacEscomp As Integer
  Public strDtacTipork As String
  Public lDtacNumdoc As Integer
  Public strDtacSerie As String
  Public nDtacAnno As Integer
  Public dDtacDatdoc As Date
  Public strDtacDtiniz As String
  Public dDtacOriniz As Decimal
  Public nDtacCodvett As Integer
  Public strDtacAcuradi As String
  Public bDtacVariaAcc As Boolean
  Public bDtacUnaNotaPerImpegno As Boolean
  Public bDtacCreaPicking As Boolean
  Public bDtacPickingDistinti As Boolean

  'gndt
  Public strGndtTipork As String
  Public nGndtAnno As Integer
  Public strGndtSerie As String
  Public lGndtNumdoc As Integer
  Public strGndtCodart As String
  Public dGndtDispon As Decimal
  Public dGndtQtaass As Decimal
  Public lGndtCommeca As Integer
  Public lGndtLotto As Integer
  Public nGndtFase As Integer
  Public nGndtTaglia As Integer
  Public dsGndtShared As DataSet
  Public bGndtHasChanges As Boolean
  Public dRap As Decimal
  Public dLastQuant As Decimal
  Public dLastColli As Decimal

  'senu
  Public nSenuAnno As Integer
  Public strSenuSerie As String
  Public lSenuInstid As Integer
  Public dsSenuShared As DataSet
  Public bSenuHasChanges As Boolean = False
  Public lProg As Integer

  'gnda
  Public dsGndaShared As DataSet
  Public bConsentiEvasSuperioreTC As Boolean = False
  'Log attività
  Public bEliminaOrdiniOltremax, bEliminaOrdiniAvalorezero, bFileRielabora As Boolean
  Public strActLog As String
  Public bLogSelezioneDati As Boolean = False
#End Region

#Region "Moduli"
  Private Moduli_P As Integer = bsModMG + bsModVE
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

  Public Overrides Function Init(ByRef App As CLE__APP, _
                              ByRef oScriptEngine As INT__SCRIPT, ByRef oCleLbmenu As Object, ByVal strTabella As String, _
                              ByVal bRemoting As Boolean, ByVal strRemoteServer As String, _
                              ByVal strRemotePort As String) As Boolean
    If MyBase.strNomeDal = "BD__BASE" Then MyBase.strNomeDal = "BDORGNNP"
    MyBase.Init(App, oScriptEngine, oCleLbmenu, strTabella, bRemoting, strRemoteServer, strRemotePort)
    oCldGnnp = CType(MyBase.ocldBase, CLDORGNNP)
    oCldGnnp.Init(oApp)

    '-------------------------------------------------
    'gestione di actlog
    'settando queste variabili al salvataggio la scrivitabellasemplice scrive anche actlog
    strActLog = ocldBase.GetSettingBus("BSORGNNP", "OPZIONI", ".", "ScriviActlog", "0", " ", " ")
    If strActLog = " " Then If oApp.ScriviActlogD Then strActLog = "-1"

    Return True
  End Function

  Public Overridable Function InitExt() As Boolean
    Dim dsBoll As New DataSet
    Try
      If Not oCldGnnp.ValCodiceDb("1", strDittaCorrente, "TABPEVE", "N", "") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101093750, "Tabella 'Personalizzazione Vendite' non compilata. Impostarla prima di proseguire")))
        Return False
      End If

      If Not oCldGnnp.ValCodiceDb("1", strDittaCorrente, "TABPEAC", "N", "") Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 127791222101250000, "Tabella 'Personalizzazione Acquisti' non compilata. Impostarla prima di proseguire")))
        Return False
      End If

      '------------------------
      'inizializzo BEVEBOLL
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BEORGNNP", "BEVEBOLL", oTmp, strErr, False, "", "") = False Then
        Throw New NTSException(oApp.Tr(Me, 128607611686875000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
        Return False
      End If
      oCleBoll = CType(oTmp, CLEVEBOLL)
      '------------------------------------------------
      AddHandler oCleBoll.RemoteEvent, AddressOf GestisciEventiEntityBoll
      If oCleBoll.Init(oApp, oScript, oCleComm, "", False, "", "") = False Then Return False
      If Not oCleBoll.InitExt() Then Return False
      oCleBoll.ApriDoc(strDittaCorrente, True, "B", 0, " ", 0, dsBoll)
      oCleBoll.ResetVar()
      oCleBoll.bModuloCRM = False
      oCleBoll.bIsCRMUser = False
      oCleBoll.strProgChiamante = "BNORGNNP"

      bConsentiEvasSuperioreTC = CBool(Val(oCldGnnp.GetSettingBus("BSVEBOLL", "OPZIONI", ".", "ConsentiEvasSuperioreTC", "0", " ", "0"))) 'NON DOCUMENTARE: se abilitata consente di evadere un articolo T&C con quantità per taglia superiore a quelle previste in ordine/nota

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

  Public Overridable Sub GestisciEventiEntityBoll(ByVal sender As Object, ByRef e As NTSEventArgs)
    Try
      'gli eventuali messaggi dati da BEVEBOLL tramite la ThrowRemoteEvent li passo a BNORGNNP
      'solo se non sono messaggi dove viene chiesta una conferma ...
      If e.TipoEvento = "" Then
        If e.Message <> "" Then LogWrite(oApp.Tr(Me, 128843572533598000, "ERROR: ") & e.Message, True)
      ElseIf e.TipoEvento = CLN__STD.ThMsg.MSG_INFO Then
        If e.Message <> "" Then LogWrite(oApp.Tr(Me, 128843573247444000, "INFO: ") & e.Message, True)
      Else
        ThrowRemoteEvent(e)
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

  Public Overridable Function LegNuma(ByVal strTipo As String, ByVal strSerie As String, ByVal nAnno As Integer) As Integer
    Try
      Return oCldGnnp.LegNuma(strDittaCorrente, strTipo, strSerie, nAnno, False)
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

  Public Overridable Function Apri(ByRef ds As DataSet) As Boolean
    Dim bRes As Boolean = False
    Dim i As Integer
    Dim dsTmp As DataSet = Nothing
    Try
      bRes = oCldGnnp.GetData(strDittaCorrente, lIITTGeGnnp, ds)

      For i = 0 To ds.Tables("TTGEGNNP").Rows.Count - 1
        ds.Tables("TTGEGNNP").Rows(i)!xx_seleziona = "S"
      Next

      For i = 0 To ds.Tables("TTGEGNNP").Rows.Count - 1
        With ds.Tables("TTGEGNNP").Rows(i)
          'commessa
          oCldGnnp.GetCommess(strDittaCorrente, strGndtTipork, NTSCStr(!fd_anno), NTSCStr(!fd_serie), _
                              NTSCStr(!fd_numdoc), dsTmp)
          ds.Tables("TTGEGNNP").Rows(i)!xx_commess = dsTmp.Tables("TESTORD").Rows(0)!td_commeca
          ds.Tables("TTGEGNNP").Rows(i)!xx_descommess = dsTmp.Tables("TESTORD").Rows(0)!co_descr1
          'righe
          oCldGnnp.GetRighe(strDittaCorrente, strGndtTipork, NTSCStr(!fd_anno), NTSCStr(!fd_serie), _
                 NTSCStr(!fd_numdoc), lIITTMoPernp, dsTmp)
          ds.Tables("TTGEGNNP").Rows(i)!xx_righe = dsTmp.Tables("TTMOPERNP").Rows(0)!Records
          'righe mancanti
          oCldGnnp.GetRighemancanti(strDittaCorrente, strGndtTipork, NTSCStr(!fd_anno), NTSCStr(!fd_serie), _
                           NTSCStr(!fd_numdoc), lIITTMoPernp, dsTmp)
          ds.Tables("TTGEGNNP").Rows(i)!xx_righemanc = dsTmp.Tables("TTMOPERNP").Rows(0)!Records
          'destinazioni
          oCldGnnp.GetDest(strDittaCorrente, strGndtTipork, NTSCStr(!fd_anno), NTSCStr(!fd_serie), _
                           NTSCStr(!fd_numdoc), dsTmp)
          ds.Tables("TTGEGNNP").Rows(i)!xx_dest = dsTmp.Tables("TESTORD").Rows(0)!td_coddest
          ds.Tables("TTGEGNNP").Rows(i)!xx_desdest = dsTmp.Tables("TESTORD").Rows(0)!dd_nomdest
        End With
      Next

      dsShared = ds

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsShared.Tables("TTGEGNNP").ColumnChanging, AddressOf BeforeColUpdate
      AddHandler dsShared.Tables("TTGEGNNP").ColumnChanged, AddressOf AfterColUpdate

      bHasChanges = False

      Return bRes
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

  Public Overridable Function GetTestateTemp(ByRef dttTmp As DataTable) As Boolean
    Try
      oCldGnnp.GetTestateTemp(strDittaCorrente, lIITTMoPernp, bDtacUnaNotaPerImpegno, dttTmp)

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

  Public Overridable Function CreaDocDaImpCli(ByVal dtrImpCli As DataRow) As Boolean
    Dim i As Integer
    Dim dttMovMag, dttTmp As New DataTable
    Dim lNumDoc As Integer
    Dim strTmp As String = ""
    Dim bSalvato As Boolean = True
    Try
      oCldGnnp.GetMovmag(strDittaCorrente, dtrImpCli, lIITTMoPernp, bModExtTCO, _
                         bDtacUnaNotaPerImpegno, dttMovMag)

      If dttMovMag.Rows.Count = 0 Then Return True

      strTmp = ""
      '  If bDtacUnaNotaPerImpegno = True Then
      strTmp += ": " & dtrImpCli!mn_tipork.ToString & "-" & dtrImpCli!mn_anno.ToString & "-" & dtrImpCli!mn_serie.ToString & "-" & dtrImpCli!mn_numord.ToString & " Riferim: " & dtrImpCli!Riferim.ToString
      'Else
      'strTmp += "Riferim: " & dtrImpCli!Riferim.ToString
      'End If

      LogWrite(oApp.Tr(Me, 128843579324644000, "Impegni coinvolti") & strTmp, False)

      lNumDoc = lDtacNumdoc
      Do While oCldGnnp.IsInTestmag(New NTSDoc(strDittaCorrente, strDtacTipork, nDtacAnno, strDtacSerie, lNumDoc))
        lNumDoc = lNumDoc + 1
      Loop
      'lNumDoc = LegNuma(strDtacTipork, strDtacSerie, nDtacAnno)
      'If lNumDoc = 0 Then
      '  ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128699231787692325, "Prima di creare un nuovo documento è necessario attivare la numerazione del documento")))
      '  Return False
      'End If

      If Not oCleBoll.NuovoDocumento(strDittaCorrente, strDtacTipork, nDtacAnno, strDtacSerie, lNumDoc) Then Return False
      oCleBoll.bInNuovoDocSilent = True
      SettaTestata(dtrImpCli)
      For i = 0 To dttMovMag.Rows.Count - 1
        If ScriviRigaDocDaImpCli(i, dttMovMag.Rows(i), dtrImpCli, lNumDoc) = False Then Return False
      Next
      SettaPiede(dtrImpCli)

      oCleBoll.bCreaFilePick = False 'non faccio generare il piking dal salvataggio del documento

      oCleBoll.bAccontiVerificati = True  'anche se uso tipobf che evadono acconti faccio continuare, visto che gli acconti possono essere agganciati solo da gestione manuale
      oCleBoll.bDisabilitaCheckUbicaz = True  'come in VB6, per gli articoli gestiti ad ubicazione non controllo se l'ubicazione è stata inserita
      If Not oCleBoll.SalvaDocumento("N") Then
        LogWrite(oApp.Tr(Me, 128843594045134000, "Documento non salvato"), True)
        bSalvato = False
      End If

      If bSalvato And bDtacCreaPicking = True Then 'genero 1 unico file di picking else uno per ogni nota
        If bDtacPickingDistinti = False Then
          CType(oCleComm, CLELBMENU).CreaPicking("BSORGNNP", strDittaCorrente, strDtacTipork, nDtacAnno, strDtacSerie, lNumDoc, lIdpick, bAggiungiPick, Not bUltimaNotaGenerataPick)
        Else
          CType(oCleComm, CLELBMENU).CreaPicking("BSORGNNP", strDittaCorrente, strDtacTipork, nDtacAnno, strDtacSerie, lNumDoc, 0)
        End If
      End If

      If Not oCldGnnp.CancellaRigheElaborate(dttMovMag) Then Return False

      lANumero = lNumDoc
      If bSalvato Then nNumFat = nNumFat + 1

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

  Public Overridable Function SettaTestata(ByVal dtrImpCli As DataRow) As Boolean
    Dim dCambio As Decimal
    Dim nCodntra As Integer
    Dim dttTmp As New DataTable
    Try
      '-----------------------------------------------------------------------
      'La causale non è presente sugli ordini quindi
      'la rileva dal tipo bolla/fattura
      nCausale = oCleBoll.nPeveCaumag
      nCausale2 = 0
      If oCldGnnp.ValCodiceDb(NTSCStr(dtrImpCli!mn_tipobf), strDittaCorrente, "TABTPBF", "N", , dttTmp) Then
        If NTSCStr(dttTmp.Rows(0)!tb_new506) = "S" Then
          nCausale = NTSCInt(dttTmp.Rows(0)!tb_tcaumag)
        End If
      End If
      If oCldGnnp.ValCodiceDb(NTSCStr(nCausale), strDittaCorrente, "TABCAUM", "N", , dttTmp) Then
        nCausale2 = NTSCInt(dttTmp.Rows(0)!tb_causec)
        Select Case NTSCStr(dttTmp.Rows(0)!tb_testci)
          Case " " : strFDarave = " "
          Case "A", "B" : strFDarave = "A"
          Case "D", "E" : strFDarave = "D"
        End Select
      Else
        'Per sicurezza
        strFDarave = " "
      End If
      '-----------------------------------------------------------------------
      'Legge la natura transazione
      nCodntra = 0
      If oCldGnnp.ValCodiceDb(NTSCStr(dtrImpCli!mn_conto), strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
        If dttTmp.Rows.Count > 0 Then nCodntra = NTSCInt(dttTmp.Rows(0)!an_codntra)
      End If

      oCleBoll.dttET.Rows(0)!et_conto = dtrImpCli!mn_conto
      oCleBoll.dttET.Rows(0)!et_tipobf = dtrImpCli!mn_tipobf
      oCleBoll.dttET.Rows(0)!et_magaz = nSeicMagaz
      oCleBoll.dttET.Rows(0)!et_magaz2 = dtrImpCli!mn_magaz2
      oCleBoll.dttET.Rows(0)!et_codese = dtrImpCli!mn_codese
      oCleBoll.dttET.Rows(0)!et_flspinc = dtrImpCli!mn_flspinc
      oCleBoll.dttET.Rows(0)!et_codpaga = dtrImpCli!mn_codpaga
      If NTSCStr(dtrImpCli!mn_datapag) = "" Then
        oCleBoll.dttET.Rows(0)!et_datapag = NTSCStr(dDtacDatdoc)
      Else
        oCleBoll.dttET.Rows(0)!et_datapag = dtrImpCli!mn_datapag
      End If
      oCleBoll.dttET.Rows(0)!et_listino = dtrImpCli!LISTINO
      oCleBoll.dttET.Rows(0)!et_scont1 = dtrImpCli!mn_tmscont1
      oCleBoll.dttET.Rows(0)!et_scont2 = dtrImpCli!mn_tmscont2
      oCleBoll.dttET.Rows(0)!et_scopag = dtrImpCli!mn_scopag

      If Trim(NTSCStr(dtrImpCli!Porto)) <> "" Then
        oCleBoll.dttET.Rows(0)!et_porto = dtrImpCli!Porto
      End If
      oCleBoll.dttET.Rows(0)!et_coddest = dtrImpCli!mn_coddest
      oCleBoll.dttET.Rows(0)!et_coddest2 = dtrImpCli!mn_coddest2
      oCleBoll.dttET.Rows(0)!et_codagen = dtrImpCli!mn_codagen
      oCleBoll.dttET.Rows(0)!et_codagen2 = dtrImpCli!mn_codagen2

      oCleBoll.dttET.Rows(0)!et_abi = dtrImpCli!Abi
      oCleBoll.dttET.Rows(0)!et_cab = dtrImpCli!Cab
      oCleBoll.dttET.Rows(0)!et_banc1 = dtrImpCli!banc1
      oCleBoll.dttET.Rows(0)!et_banc2 = dtrImpCli!banc2
      oCleBoll.dttET.Rows(0)!et_datdoc = dDtacDatdoc
      oCleBoll.dttET.Rows(0)!et_riferim = dtrImpCli!Riferim
      oCleBoll.dttET.Rows(0)!et_intra = NTSCStr(IIf(nCodntra = 0, "N", "C"))
      oCleBoll.dttET.Rows(0)!et_codrsta = NTSCStr(IIf(nCodntra = 0, 0, nINTRACodrsta))
      oCleBoll.dttET.Rows(0)!et_codport = NTSCStr(IIf(nCodntra = 0, " ", strINTRACodPort))
      oCleBoll.dttET.Rows(0)!et_codntra = nCodntra
      oCleBoll.dttET.Rows(0)!et_valuta = dtrImpCli!mn_valuta
      oCleBoll.dttET.Rows(0)!et_scorpo = dtrImpCli!mn_scorpo
      oCleBoll.dttET.Rows(0)!et_vettor = dtrImpCli!Vettor
      oCleBoll.dttET.Rows(0)!et_acuradi = dtrImpCli!Acuradi
      oCleBoll.dttET.Rows(0)!et_dtiniz = NTSCDate(strDtacDtiniz)
      oCleBoll.dttET.Rows(0)!et_oriniz = dDtacOriniz
      oCleBoll.dttET.Rows(0)!et_aspetto = dtrImpCli!Aspetto
      oCleBoll.dttET.Rows(0)!et_caustra = dtrImpCli!Caustra
      oCleBoll.dttET.Rows(0)!et_flboll = dtrImpCli!mn_flbolli

      dttTmp = Nothing
      oCldGnnp.GetNote(strDittaCorrente, bDtacUnaNotaPerImpegno, nSeicTipo, lIITTMoPernp, _
                       bRiportaNoteDaImpegno, dtrImpCli, dttTmp)

      If dttTmp Is Nothing Then
        oCleBoll.dttET.Rows(0)!et_note = " "
      Else
        If dttTmp.Rows.Count > 0 Then
          oCleBoll.dttET.Rows(0)!et_note = dttTmp.Rows(0)!xx_note
        Else
          oCleBoll.dttET.Rows(0)!et_note = " "
        End If
      End If

      oCleBoll.dttET.Rows(0)!et_causale = nCausale
      oCleBoll.dttET.Rows(0)!et_codcena = dtrImpCli!Codcena
      oCleBoll.dttET.Rows(0)!et_codaspe = dtrImpCli!Codaspe
      oCleBoll.dttET.Rows(0)!et_commeca = dtrImpCli!COMMECA
      oCleBoll.dttET.Rows(0)!et_subcommeca = dtrImpCli!Subcommeca
      oCleBoll.dttET.Rows(0)!et_coddivi = dtrImpCli!coddivi
      oCleBoll.dttET.Rows(0)!et_codcli = dtrImpCli!codcli
      oCleBoll.dttET.Rows(0)!et_ultagg = Now
      oCleBoll.dttET.Rows(0)!et_vettor2 = dtrImpCli!Vettor2
      oCleBoll.dttET.Rows(0)!et_codbanc = dtrImpCli!Codbanc
      oCleBoll.dttET.Rows(0)!et_opnome = oApp.User.Nome
      oCleBoll.dttET.Rows(0)!et_annotco = dtrImpCli!Annotco
      oCleBoll.dttET.Rows(0)!et_codstag = dtrImpCli!Codstag
      oCleBoll.dttET.Rows(0)!et_codcfam = dtrImpCli!Codcfam
      oCleBoll.dttET.Rows(0)!et_contfatt = dtrImpCli!Contfatt

      '----------------------------
      'Operazione sul cambio
      dCambio = NTSCDec(dtrImpCli!Cambio)
      If NTSCInt(dtrImpCli!mn_valuta) <> 0 Then
        If (nSvalOpzione = 2) Or (nSvalOpzione = 3) Then
          dCambio = 1 'default (in futuro usare ORDLIST.ol_cambio)
          dCambio = RitornaCambio(NTSCInt(dtrImpCli!mn_valuta), nSvalOpzione, NTSCStr(dDtacDatdoc))
        End If
      Else
        dCambio = 0
      End If
      oCleBoll.dttET.Rows(0)!et_cambio = dCambio

      ' se necessario aggiorna i dati accompagnatori
      If bDtacVariaAcc Then
        oCleBoll.dttET.Rows(0)!et_acuradi = strDtacAcuradi
        oCleBoll.dttET.Rows(0)!et_vettor = nDtacCodvett
      End If

      'setto le var per la stampa
      If NTSCInt(oCleBoll.dttET.Rows(0)!et_valuta) > 0 Then bVal = True Else bVal = False
      If NTSCStr(oCleBoll.dttET.Rows(0)!et_scorpo) = "S" Then bScorp = True Else bScorp = False

      If NTSCStr(oCleBoll.dttET.Rows(0)!et_tipork) <> "Z" And NTSCStr(oCleBoll.dttET.Rows(0)!et_tipork) <> "T" And _
         NTSCStr(oCleBoll.dttET.Rows(0)!et_tipork) <> "I" Then
        oCleBoll.dttET.Rows(0)!et_cup = dtrImpCli!td_cup
        oCleBoll.dttET.Rows(0)!et_cig = dtrImpCli!td_cig
        oCleBoll.dttET.Rows(0)!et_riferimpa = dtrImpCli!td_riferimpa
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
      Return False
    End Try
  End Function

  Public Overridable Function SettaPiede(ByVal dtrImpCli As DataRow) As Boolean
    Dim dttTmp As New DataTable
    Try
      dttTmp = Nothing
      oCldGnnp.SettaPiede(strDittaCorrente, bDtacUnaNotaPerImpegno, nSeicTipo, lIITTMoPernp, _
                          dtrImpCli, dttTmp)

      If Not dttTmp Is Nothing Then
        If dttTmp.Rows.Count > 0 Then
          If bDtacUnaNotaPerImpegno = True Then
            oCleBoll.dttET.Rows(0)!et_speacc = dttTmp.Rows(0)!td_speacc
            oCleBoll.dttET.Rows(0)!et_speimb = dttTmp.Rows(0)!td_speimb
            oCleBoll.dttET.Rows(0)!et_speaccv = dttTmp.Rows(0)!td_speaccv
            oCleBoll.dttET.Rows(0)!et_speimbv = dttTmp.Rows(0)!td_speimbv
          Else
            oCleBoll.dttET.Rows(0)!et_speacc = dttTmp.Rows(0)!mn_speacc
            oCleBoll.dttET.Rows(0)!et_speimb = dttTmp.Rows(0)!mn_speimb
            oCleBoll.dttET.Rows(0)!et_speaccv = dttTmp.Rows(0)!mn_speaccv
            oCleBoll.dttET.Rows(0)!et_speimbv = dttTmp.Rows(0)!mn_speimbv
          End If
        End If
      End If

      oCleBoll.CalcolaTotali()

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

  Public Overridable Function ScriviRigaDocDaImpCli(ByVal i As Integer, ByVal dtrMm As DataRow, _
                                                    ByVal dtrTm As DataRow, ByVal lNumDoc As Integer) As Boolean
    Dim dQuant As Decimal = 0
    Dim dColli As Decimal = 0
    Dim strErr As String = ""
    Dim nRiga As Integer
    Dim dttTmp As New DataTable
    Dim dPrezzo As Decimal
    Dim dPrezvalc As Decimal
    Dim dtrT() As DataRow = Nothing
    Dim j As Integer = 0
    Dim dttTmp2 As New DataTable
    Dim dsTmp As DataSet = Nothing
    Try
      nRiga = i + 1

      oCleBoll.bInImportRigheOrd = True

      Select Case NTSCStr(dtrMm!mn_flkit)
        Case " "
          'lRigaPadre = 0
          bRigaFiglio = False
        Case "A", "S"
          lRigaPadre = nRiga
          bRigaFiglio = False
        Case "B", "T"
          bRigaFiglio = True
      End Select

      'Creo una nuova riga di corpo setto i principali campi poi setto tutti gli altri
      If Not oCleBoll.AggiungiRigaCorpo(False, NTSCStr(dtrMm!mn_codart), NTSCInt(dtrMm!mn_fase), nRiga, _
                                  nCausale, NTSCInt(dtrMm!mn_magaz)) Then Return False

      With oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1)
        !ec_magaz2 = dtrMm!mn_magaz2
        !ec_causale2 = nCausale2
        !ec_descr = dtrMm!mn_descr
        !ec_desint = dtrMm!mn_desint
        !ec_controp = dtrMm!mn_controp
        !ec_contocontr = dtrMm!mn_contocontr
        !ec_unmis = dtrMm!mn_unmis
        !ec_colli = dtrMm!mn_mmcolli
        !ec_quant = dtrMm!mn_mmquant
        !ec_prezzo = dtrMm!mn_prezzo
        !ec_scont1 = dtrMm!mn_scont1
        !ec_scont2 = dtrMm!mn_scont2
        !ec_scont3 = dtrMm!mn_scont3
        !ec_codiva = dtrMm!mn_codiva
        !ec_preziva = dtrMm!mn_preziva
        !ec_prezvalc = dtrMm!mn_prezvalc
        !ec_provv = dtrMm!mn_provv
        !ec_commen = dtrMm!mn_commen
        !ec_flelab = "N"
        !ec_flcom = "N"
        !ec_ricimp = dtrMm!mn_flstat
        !ec_ortipo = strGndtTipork
        !ec_oranno = dtrMm!mn_anno
        !ec_orserie = dtrMm!mn_serie
        !ec_ornum = dtrMm!mn_numord
        !ec_orriga = dtrMm!mn_riga
        !ec_salcon = dtrMm!mn_mmflevas
        !ec_stasino = dtrMm!mn_stasino
        !ec_prelist = dtrMm!mn_prelist
        !ec_provv2 = dtrMm!mn_provv2
        !ec_codcfam = dtrMm!mn_codcfam
        !ec_commeca = dtrMm!mn_commeca
        !ec_subcommeca = dtrMm!mn_subcommeca
        !ec_coddivi = dtrMm!mo_coddivi
        '!ec_valore = dtrMm!mn_mmvalore
        !ec_qtadisimp = dtrMm!mn_mmqtadisimp
        !ec_coldisimp = dtrMm!mn_mmqtadisimp
        !ec_valdisimp = dtrMm!mn_mmvaldisimp
        !ec_lotto = dtrMm!mn_lotto
        !ec_codcena = dtrMm!mn_codcena
        !ec_codvuo = dtrMm!mn_codvuo
        !ec_vprovv = dtrMm!mn_mmvprovv
        !ec_vprovv2 = dtrMm!mn_mmvprovv2
        !ec_ump = dtrMm!mn_ump
        !ec_note = dtrMm!mn_note
        !ec_misura1 = dtrMm!mn_misura1
        !ec_misura2 = dtrMm!mn_misura2
        !ec_misura3 = dtrMm!mn_misura3
        !ec_ultagg = Now
        !xxo_orultagg = dtrMm!mn_ultagg
        !ec_perqta = dtrMm!mn_perqta
        !ec_scont4 = dtrMm!mn_scont4
        !ec_scont5 = dtrMm!mn_scont5
        !ec_scont6 = dtrMm!mn_scont6
        !ec_scontp = dtrMm!mn_scontp
        !ec_scontv = 0 'FISSO 0: se nell'impegno ho fatto un abbuono a valore e la quantità di riga è dviersa da 1, quando evado parzialmente la quantità andrebbe riproporzionalizzato. questo sconto è compilato solo da veboll/retail su documenti no IC o note di prel
        !ec_pmtaskid = dtrMm!mn_pmtaskid
        !ec_ubicaz = dtrMm!mn_ubicaz
        !ec_flkit = dtrMm!mn_flkit
        !ec_ktriga = IIf(bRigaFiglio, lRigaPadre, 0)
        '!ec_valorev = dtrMm!mn_mmvalorev
        !ec_codtpro = dtrMm!mn_codtpro
        !ec_flprznet = dtrMm!mn_flprznet

        oCldGnnp.GetDatiniDatfin(strDittaCorrente, lIITTMoPernp, dtrMm, dsTmp)

        If dsTmp.Tables("TTMOPERNP").Rows.Count > 0 Then
          If oCleBoll.bNonEreditareDateCompDaOrd = True Then
            !ec_datini = oCleBoll.dttET.Rows(0)!et_datdoc
            !ec_datfin = oCleBoll.dttET.Rows(0)!et_datdoc
          Else
            !ec_datini = dsTmp.Tables("TTMOPERNP").Rows(0)!mo_datini
            !ec_datfin = dsTmp.Tables("TTMOPERNP").Rows(0)!mo_datfin
          End If
        End If

        If bRicalcPrez Then
          dPrezzo = NTSCDec(dtrMm!mn_prezzo)
          dPrezvalc = NTSCDec(dtrMm!mn_prezvalc)
          If NTSCDec(oCleBoll.dttET.Rows(0)!et_valuta) <> 0 Then
            'Questo serve se il cambio del documento è diverso da quello dell'ordine
            dPrezzo = oCldGnnp.ConvImpValuta(strDittaCorrente, True, NTSCDec(!mn_prezvalc), NTSCInt(oCleBoll.dttET.Rows(0)!ec_valuta), NTSCDate(oCleBoll.dttET.Rows(0)!ec_datdoc), NTSCDec(oCleBoll.dttET.Rows(0)!ec_cambio))
          End If
          ' !ec_prezivav = NTSCDec(dtrMm!mn_prezivav)
          !ec_preziva = NTSCDec(dtrMm!mn_preziva)
          !ec_prezvalc = dPrezvalc
          !ec_prezzo = dPrezzo
          !ec_prelist = NTSCDec(dtrMm!mn_prelist)
        End If
        If bRicalcScon Then
          !ec_scont1 = NTSCDec(dtrMm!mn_scont1)
          !ec_scont2 = NTSCDec(dtrMm!mn_scont2)
          !ec_scont3 = NTSCDec(dtrMm!mn_scont3)
          !ec_scont4 = NTSCDec(dtrMm!mn_scont4)
          !ec_scont5 = NTSCDec(dtrMm!mn_scont5)
          !ec_scont6 = NTSCDec(dtrMm!mn_scont6)
          !ec_scontp = NTSCDec(dtrMm!mn_scontp)
          !ec_scontv = 0  'FISSO 0: se nell'impegno ho fatto un abbuono a valore e la quantità di riga è dviersa da 1, quando evado parzialmente la quantità andrebbe riproporzionalizzato. questo sconto è compilato solo da veboll/retail su documenti no IC o note di prel
        End If
        If bRicalcProv Then
          !ec_ricimp = dtrMm!mn_flstat
          !ec_provv = dtrMm!mn_provv
          !ec_provv2 = dtrMm!mn_provv2
        End If

        '---------------------------
        'se articolo TCO, devo alimentare anche la quantità in CORPOTC
        'la riga è già stata create da beorgsor, per cui devo solo inserire le quantità
        If NTSCInt(!xxo_codtagl) <> 0 And CBool((ModuliExtDittaDitt(strDittaCorrente) And bsModExtTCO)) Then
          For j = 1 To 24
            If Not oCldGnnp.GetMovmagtc(strDittaCorrente, lIITTMoPernp, NTSCStr(dtrMm!mn_anno), NTSCStr(dtrMm!mn_serie), _
                                        NTSCStr(dtrMm!mn_numord), NTSCStr(dtrMm!mn_riga), j, dttTmp2) Then
              Return False
            End If
            If dttTmp2.Rows.Count = 0 Then Continue For
            dtrT = oCleBoll.dttECTC.Select(" ec_tipork = " & CStrSQL(strDtacTipork) & " AND ec_anno = " & nDtacAnno & _
                               " AND ec_serie = " & CStrSQL(strDtacSerie) & " AND ec_numdoc = " & lNumDoc & _
                               " AND ec_riga = " & nRiga)
            dtrT(0)("ec_quant" & j.ToString.PadLeft(2, "0"c)) = NTSCDec(dttTmp2.Rows(0)("mn_mmquant" & j.ToString.PadLeft(2, "0"c)))
            dtrT(0)("ec_qtadis" & j.ToString.PadLeft(2, "0"c)) = NTSCDec(dttTmp2.Rows(0)("mn_mmquant" & j.ToString.PadLeft(2, "0"c)))
          Next
        End If    'If NTSCInt(!xxo_codtagl) <> 0 Then
      End With

      If Not ScriviRigaDocDaImpCli_Pers(oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1), dtrMm) Then
        oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1).Delete()
        Return False
      End If

      If Not oCleBoll.RecordSalva(oCleBoll.dttEC.Rows.Count - 1, False, Nothing) Then
        oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1).Delete()
        Return False
      End If

      Return True

    Catch ex As Exception
      oCleBoll.dttEC.Rows(oCleBoll.dttEC.Rows.Count - 1).Delete()
      '--------------------------------------------------------------
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
      '--------------------------------------------------------------	
    Finally
      oCleBoll.bInImportRigheOrd = False
    End Try
  End Function

  Public Overridable Function ScriviRigaDocDaImpCli_Pers(ByRef dtrNew As DataRow, ByRef dtrOld As DataRow) As Boolean
    Try
      'a disposizione per rive per caricare campi personalizzati di offerta in fase di import righe da offerta
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

  Public Overridable Function VerificaOrdineInUso(ByVal strTipork As String, ByVal lAnno As Integer, ByVal strSerie As String, ByVal lNumord As Integer) As Boolean
    Try
      '---------------------------------------------------------------------------
      'verifico se l'ordine è in modifica tra quelli da importare
      If oCleBoll.nControllaConcorrenzaOggetti = 1 Then
        Dim strMsg As String = ""
        oCleBoll.DocumentLockCheck(strTipork, lAnno, strSerie, lNumord, 0, strMsg)
        If strMsg.Trim <> "" Then
          'blocco
          oCldGnnp.EscludiOrdineDaElaborazione(strDittaCorrente, strTipork, lAnno, strSerie, lNumord, lIITTMoPernp, lIITTGeGnnp)
          LogWrite(strMsg & vbCrLf & oApp.Tr(Me, 129179691578652344, "Importazione ordine annullata."), True)
          Return False
        End If
      End If    'If nControllaConcorrenzaOggetti <> 0 Then

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

  Public Overridable Function Salva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      If bDelete Then
        '----------------------------------------------------------------------------------------
        '--- Per ogni riga selezionata, la cancella
        '----------------------------------------------------------------------------------------
        For Each dtrT As DataRow In dsShared.Tables("TTGEGNNP").Select("", "", DataViewRowState.Deleted)
          oCldGnnp.DeleteRigheInSalva(strDittaCorrente, lIITTMoPernp, lIITTGeGnnp, dtrT)
        Next
      End If

      '----------------------------------------
      'chiamo il dal per salvare
      bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, "TTGEGNNP", dsShared.Tables("TTGEGNNP"), "", "", "")

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

  Public ReadOnly Property RecordIsChanged() As Boolean
    Get
      Return bHasChanges
    End Get
  End Property

  Public Overridable Sub BeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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
      Dim strFunction As String = "BeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub AfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
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

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "AfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Function RitornaCambio(ByVal nValuta As Integer, ByVal nSvalOpzione As Integer, ByVal strDataElab As String) As Decimal
    Dim dsTmp As New DataSet
    Dim dtrT() As DataRow = Nothing
    Dim strDesvalu As String = ""
    Dim evnt As NTSEventArgs = Nothing
    Dim bChiediCambio As Boolean = False
    Try
      oCldGnnp.ValCodiceDb(nValuta.ToString, strDittaCorrente, "TABVALU", strDesvalu)
      RitornaCambio = oCldGnnp.CercaCambioDiOggi(nValuta, strDataElab)

      '----------------------
      If RitornaCambio = 0 And nSvalOpzione = 3 Then
        oCldGnnp.LeggiTabellaSemplice(strDittaCorrente, "CAMBI", dsTmp)
        dtrT = dsTmp.Tables("CAMBI").Select("wx_codvalu = " & nValuta & " AND wx_dtvalid <= " & CDataSQL(strDataElab), "wx_dtvalid DESC")
        If dtrT.Length > 0 Then
          RitornaCambio = NTSCDec(dtrT(0)!wx_cambio)
        Else
          bChiediCambio = True
        End If
        dsTmp.Clear()
      End If

      '----------------------
      If RitornaCambio = 0 And nSvalOpzione = 2 Then
        bChiediCambio = True
      End If

      If bChiediCambio Then
Continua:
        evnt = New NTSEventArgs(CLN__STD.ThMsg.INPUTBOX, oApp.Tr(Me, 128608418924375000, ""))
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 100000000000000000, "Inserire il cambio per la valuta '|" & strDesvalu & "|':")))
        If evnt.RetValue = "" Then
          GoTo Continua
        End If
        If Not IsNumeric(evnt.RetValue) Then
          GoTo Continua
        End If
        If NTSCDec(evnt.RetValue) <= 0 Or NTSCDec(evnt.RetValue) > 999999999 Then
          GoTo Continua
        End If
        RitornaCambio = NTSCDec(evnt.RetValue)
        oCldGnnp.AggiornaCambio(nValuta, strDataElab, NTSCDec(evnt.RetValue), True)
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

  Public Overridable Function SelezionaMovord() As Boolean
    Dim lRecordAf As Integer
    Try
      oCldGnnp.SelezionaMovord(strDittaCorrente, lRecordAf, lIITTMoPernp, bModExtTCO, _
                               strSeicQuery)

      If lRecordAf = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128723341753001419, "Non ci sono righe di impegni da evadere." & vbCrLf & vbCrLf & _
                                              "(Ordini NON bloccati e NON sospesi, NON prenotati a saldo, con codice pagamento diverso da 0, intestati a clienti/fornitori NON bloccati con i seguenti filtri: '|" & strSeicQuery & "|')")))
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

  Public Overridable Function AssegnaMovord(ByVal bRielab As Boolean) As Boolean
    Dim dsTTDispnet As DataSet = Nothing
    Dim dsTTMopernp As DataSet = Nothing
    Dim i As Integer
    Dim j As Integer
    Dim dResiduo As Decimal
    Dim dResordine As Decimal
    Dim dResordinecolli As Decimal
    Dim dValore As Decimal
    Dim dColli As Decimal
    Try
      oCldGnnp.AssegnaMovord(strDittaCorrente, bRielab, lIITTDispNet, lIITTMoPernp, _
                             nSeicMagaz, bModExtTCO)

      If Not oCldGnnp.AssegnaMovordGetTTDispnet(strDittaCorrente, lIITTDispNet, bMostraListaImpegniAncheSeTuttiNonEvadibili, dsTTDispnet) Then Return False

      If dsTTDispnet.Tables("TTDISPNET").Rows.Count = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128723359827353538, "Non ci sono disponibilità nette da assegnare.")))
        Return False
      End If

      '-----------------------------------------------------------------------------------------
      '--- Ha trovato delle disponibilità nette
      '-----------------------------------------------------------------------------------------
      For i = 0 To dsTTDispnet.Tables("TTDISPNET").Rows.Count - 1

        If Not oCldGnnp.AssegnaMovordGetTTMopernp(strDittaCorrente, dsTTDispnet.Tables("TTDISPNET").Rows(i), lIITTMoPernp, nSeicPriorita, _
                                                  dsTTMopernp) Then Return False

        For j = 0 To dsTTMopernp.Tables("TTMOPERNP").Rows.Count - 1
          With dsTTMopernp.Tables("TTMOPERNP").Rows(j)
            If NTSCInt(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_lotto) = 0 Then
              dResiduo = ArrDbl((NTSCDec(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_dispnet) - NTSCDec(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_qtaass)), 3)
              If dResiduo > 0 Then
                dResordine = ArrDbl((NTSCDec(!mn_quant) - NTSCDec(!mn_quaeva) - NTSCDec(!mn_quapre)), 3)
                dResordinecolli = ArrDbl((NTSCDec(!mn_colli) - NTSCDec(!mn_coleva) - NTSCDec(!mn_colpre)), 3)
                If dResordinecolli < 0 Then dResordinecolli = 0
                If (dResiduo < dResordine) And (dResordine > 0) Then
                  '--- Evadi IN CONTO
                  '--- Quantità/colli
                  !mn_mmquant = dResiduo
                  !mn_mmqtadisimp = dResiduo
                  !mn_mmflevas = "C"
                  If dResordinecolli > 0 Then
                    '--- Si gestisciono anche i colli
                    CType(oCleComm, CLELBMENU).ConvQuantUM(strDittaCorrente, NTSCStr(!mn_codart), NTSCStr(!mn_ump), dResiduo, NTSCDec(!mn_misura1), NTSCDec(!mn_misura2), NTSCDec(!mn_misura3), NTSCStr(!mn_unmis), dColli, "", 3)
                    !mn_mmcolli = dColli
                    !mn_mmcoldisimp = dColli
                  Else
                    !mn_mmcolli = 0
                    !mn_mmcoldisimp = 0
                  End If
                  '--- Valori
                  If !mn_umprz.ToString <> "S" Or _
                     !mn_tipork.ToString = "Y" Or _
                     (!mn_unmis.ToString.ToUpper = !mn_ump.ToString.ToUpper) Then
                    dValore = ArrDbl(ArrDbl(NTSCDec(!mn_prezzo) * dResiduo / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(0)) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(0))
                  Else
                    dValore = ArrDbl(ArrDbl(NTSCDec(!mn_prezzo) * dColli / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(0)) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(0))
                  End If
                  !mn_mmvalore = dValore
                  !mn_mmvaldisimp = dValore
                  If NTSCInt(!mn_valuta) > 0 Then
                    !mn_mmvalorev = ArrDbl(ArrDbl(NTSCDec(!mn_prezvalc) * dResiduo / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(NTSCInt(NTSCDec(!mn_valuta)))) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(NTSCInt(NTSCDec(!mn_valuta))))
                  Else
                    !mn_mmvalorev = 0
                  End If
                  '--- Ora i valori provvigione
                  If NTSCStr(!mn_flstat) = "S" Then
                    !mn_mmvprovv = ArrDbl(dResiduo * NTSCDec(!mn_provv), oCldGnnp.TrovaNdec(0))
                    !mn_mmvprovv2 = ArrDbl(dResiduo * NTSCDec(!mn_provv2), oCldGnnp.TrovaNdec(0))
                  Else
                    !mn_mmvprovv = ArrDbl(dValore * 100 / (100 - NTSCDec(!mn_scopag)) * NTSCDec(!mn_provv) / 100, oCldGnnp.TrovaNdec(0))
                    !mn_mmvprovv2 = ArrDbl(dValore * 100 / (100 - NTSCDec(!mn_scopag)) * NTSCDec(!mn_provv2) / 100, oCldGnnp.TrovaNdec(0))
                  End If
                  ocldBase.ScriviTabellaSemplice(strDittaCorrente, "TTMOPERNP", dsTTMopernp.Tables("TTMOPERNP"), "", "", "")
                  dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_qtaass = (NTSCDec(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_qtaass) + dResiduo)
                  ocldBase.ScriviTabellaSemplice(strDittaCorrente, "TTDISPNET", dsTTDispnet.Tables("TTDISPNET"), "", "", "")
                End If
                If (dResiduo >= dResordine) And (dResordine > 0) Then
                  '--- Evadi a SALDO
                  !mn_mmquant = dResordine
                  !mn_mmqtadisimp = dResordine
                  !mn_mmflevas = "S"
                  !mn_mmcolli = dResordinecolli
                  !mn_mmcoldisimp = dResordinecolli
                  If !mn_umprz.ToString <> "S" Or _
                     !mn_tipork.ToString = "Y" Or _
                     (!mn_unmis.ToString.ToUpper = !mn_ump.ToString.ToUpper) Then
                    dValore = ArrDbl(ArrDbl(NTSCDec(!mn_prezzo) * dResordine / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(0)) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(0))
                  Else
                    dValore = ArrDbl(ArrDbl(NTSCDec(!mn_prezzo) * dResordinecolli / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(0)) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(0))
                  End If
                  !mn_mmvalore = dValore
                  !mn_mmvaldisimp = dValore
                  If NTSCInt(!mn_valuta) > 0 Then
                    !mn_mmvalorev = ArrDbl(ArrDbl(NTSCDec(!mn_prezvalc) * dResordine / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(NTSCInt(NTSCDec(!mn_valuta)))) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(NTSCInt(NTSCDec(!mn_valuta))))
                  Else
                    !mn_mmvalorev = 0
                  End If
                  '--- Qui per vprovv
                  If NTSCStr(!mn_flstat) = "S" Then
                    !mn_mmvprovv = ArrDbl(dResordine * NTSCDec(!mn_provv), oCldGnnp.TrovaNdec(0))
                    !mn_mmvprovv2 = ArrDbl(dResordine * NTSCDec(!mn_provv2), oCldGnnp.TrovaNdec(0))
                  Else
                    !mn_mmvprovv = ArrDbl(dValore * 100 / (100 - NTSCDec(!mn_scopag)) * NTSCDec(!mn_provv) / 100, oCldGnnp.TrovaNdec(0))
                    !mn_mmvprovv2 = ArrDbl(dValore * 100 / (100 - NTSCDec(!mn_scopag)) * NTSCDec(!mn_provv2) / 100, oCldGnnp.TrovaNdec(0))
                  End If
                  ocldBase.ScriviTabellaSemplice(strDittaCorrente, "TTMOPERNP", dsTTMopernp.Tables("TTMOPERNP"), "", "", "")
                  dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_qtaass = (NTSCDec(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_qtaass) + dResordine)
                  ocldBase.ScriviTabellaSemplice(strDittaCorrente, "TTDISPNET", dsTTDispnet.Tables("TTDISPNET"), "", "", "")
                End If
              End If
              dResiduo = (NTSCDec(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_dispnet) - NTSCDec(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_qtaass))
              If dResiduo = 0 Then Continue For
            Else
              '---------------------------------------------------------------------------------
              '--- Articoli gestiti a lotto
              '---------------------------------------------------------------------------------
              dResiduo = ArrDbl((NTSCDec(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_dispnet) - NTSCDec(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_qtaass)), 3)
              If dResiduo > 0 Then
                dResordine = ArrDbl((NTSCDec(!mn_quant) - NTSCDec(!mn_quaeva) - NTSCDec(!mn_quapre)), 3)
                dResordinecolli = ArrDbl((NTSCDec(!mn_colli) - NTSCDec(!mn_coleva) - NTSCDec(!mn_colpre)), 3)
                If dResordinecolli < 0 Then dResordinecolli = 0
                If (dResiduo < dResordine) And (dResordine > 0) Then
                  '--- Evadi IN CONTO
                  '--- Quantità/colli
                  !mn_mmquant = dResiduo
                  !mn_mmqtadisimp = dResiduo
                  !mn_mmflevas = "C"
                  !mn_lotto = NTSCInt(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_lotto)
                  !mn_quant = dResiduo '(dynTtMoPernp!mn_quant - dResiduo)
                  If NTSCDec(!mn_colli) <> 0 Then
                    CType(oCleComm, CLELBMENU).ConvQuantUM(strDittaCorrente, NTSCStr(!mn_codart), NTSCStr(!mn_ump), NTSCDec(!mn_quant), NTSCDec(!mn_misura1), NTSCDec(!mn_misura2), NTSCDec(!mn_misura3), NTSCStr(!mn_unmis), dColli, "", 3)
                    !mn_colli = dColli
                  End If
                  If dResordinecolli > 0 Then
                    '--- Si gestisciono anche i colli
                    CType(oCleComm, CLELBMENU).ConvQuantUM(strDittaCorrente, NTSCStr(!mn_codart), NTSCStr(!mn_ump), dResiduo, NTSCDec(!mn_misura1), NTSCDec(!mn_misura2), NTSCDec(!mn_misura3), NTSCStr(!mn_unmis), dColli, "", 3)
                    !mn_mmcolli = dColli
                    !mn_mmcoldisimp = dColli
                  Else
                    !mn_mmcolli = 0
                    !mn_mmcoldisimp = 0
                  End If
                  '--- Valori
                  If !mn_umprz.ToString <> "S" Or _
                     !mn_tipork.ToString = "Y" Or _
                     (!mn_unmis.ToString.ToUpper = !mn_ump.ToString.ToUpper) Then
                    dValore = ArrDbl(ArrDbl(NTSCDec(!mn_prezzo) * dResiduo / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(0)) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(0))
                  Else
                    dValore = ArrDbl(ArrDbl(NTSCDec(!mn_prezzo) * dColli / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(0)) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(0))
                  End If
                  'E' lo stesso valore (dValore) che devono mettere anche sulla 'parte' ordine
                  !mn_valore = dValore
                  !mn_mmvalore = dValore
                  !mn_mmvaldisimp = dValore
                  If NTSCInt(!mn_valuta) > 0 Then
                    !mn_mmvalorev = ArrDbl(ArrDbl(NTSCDec(!mn_prezvalc) * dResiduo / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(NTSCInt(NTSCDec(!mn_valuta)))) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(NTSCInt(NTSCDec(!mn_valuta))))
                  Else
                    !mn_mmvalorev = 0
                  End If
                  '--- Ora i valori provvigione
                  If NTSCStr(!mn_flstat) = "S" Then
                    !mn_mmvprovv = ArrDbl(dResiduo * NTSCDec(!mn_provv), oCldGnnp.TrovaNdec(0))
                    !mn_mmvprovv2 = ArrDbl(dResiduo * NTSCDec(!mn_provv2), oCldGnnp.TrovaNdec(0))
                  Else
                    !mn_mmvprovv = ArrDbl(dValore * 100 / (100 - NTSCDec(!mn_scopag)) * NTSCDec(!mn_provv) / 100, oCldGnnp.TrovaNdec(0))
                    !mn_mmvprovv2 = ArrDbl(dValore * 100 / (100 - NTSCDec(!mn_scopag)) * NTSCDec(!mn_provv2) / 100, oCldGnnp.TrovaNdec(0))
                  End If
                  ocldBase.ScriviTabellaSemplice(strDittaCorrente, "TTMOPERNP", dsTTMopernp.Tables("TTMOPERNP"), "", "", "")
                  '-----------------------------------------------------------------------------
                  '--- Inserisce una nuova riga in TTMOPERNP
                  '-----------------------------------------------------------------------------
                  CType(oCleComm, CLELBMENU).ConvQuantUM(strDittaCorrente, NTSCStr(!mn_codart), NTSCStr(!mn_ump), (dResordine - dResiduo), NTSCDec(!mn_misura1), NTSCDec(!mn_misura2), NTSCDec(!mn_misura3), NTSCStr(!mn_unmis), dColli, "", 3)
                  If !mn_umprz.ToString <> "S" Or _
                     !mn_tipork.ToString = "Y" Or _
                     (!mn_unmis.ToString.ToUpper = !mn_ump.ToString.ToUpper) Then
                    dValore = ArrDbl(ArrDbl(NTSCDec(!mn_prezzo) * (dResordine - dResiduo) / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(0)) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(0))
                  Else
                    dValore = ArrDbl(ArrDbl(NTSCDec(!mn_prezzo) * (dResordinecolli - dColli) / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(0)) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(0))
                  End If
                  oCldGnnp.AssegnaMovordInsertTTMopernp(strDittaCorrente, lIITTMoPernp, dColli, dResordine, _
                                                            dResiduo, dValore, dsTTMopernp.Tables("TTMOPERNP").Rows(j))

                  '-----------------------------------------------------------------------------
                  dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_qtaass = (NTSCDec(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_qtaass) + dResiduo)
                  ocldBase.ScriviTabellaSemplice(strDittaCorrente, "TTDISPNET", dsTTDispnet.Tables("TTDISPNET"), "", "", "")
                End If
                If (dResiduo >= dResordine) And (dResordine > 0) Then
                  '--- Evadi a SALDO
                  !mn_lotto = NTSCInt(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_lotto)
                  !mn_mmquant = dResordine
                  !mn_mmqtadisimp = dResordine
                  !mn_mmflevas = "S"
                  !mn_mmcolli = dResordinecolli
                  !mn_mmcoldisimp = dResordinecolli
                  If !mn_umprz.ToString <> "S" Or _
                     !mn_tipork.ToString = "Y" Or _
                     (!mn_unmis.ToString.ToUpper = !mn_ump.ToString.ToUpper) Then
                    dValore = ArrDbl(ArrDbl(NTSCDec(!mn_prezzo) * dResordine / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(0)) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(0))
                  Else
                    dValore = ArrDbl(ArrDbl(NTSCDec(!mn_prezzo) * dResordinecolli / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(0)) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(0))
                  End If
                  !mn_mmvalore = dValore
                  !mn_mmvaldisimp = dValore
                  If NTSCInt(!mn_valuta) > 0 Then
                    !mn_mmvalorev = ArrDbl(ArrDbl(NTSCDec(!mn_prezvalc) * dResordine / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(NTSCInt(NTSCDec(!mn_valuta)))) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(NTSCInt(NTSCDec(!mn_valuta))))
                  Else
                    !mn_mmvalorev = 0
                  End If
                  '--- Qui per vprovv
                  If NTSCStr(!mn_flstat) = "S" Then
                    !mn_mmvprovv = ArrDbl(dResordine * NTSCDec(!mn_provv), oCldGnnp.TrovaNdec(0))
                    !mn_mmvprovv2 = ArrDbl(dResordine * NTSCDec(!mn_provv2), oCldGnnp.TrovaNdec(0))
                  Else
                    !mn_mmvprovv = ArrDbl(dValore * 100 / (100 - NTSCDec(!mn_scopag)) * NTSCDec(!mn_provv) / 100, oCldGnnp.TrovaNdec(0))
                    !mn_mmvprovv2 = ArrDbl(dValore * 100 / (100 - NTSCDec(!mn_scopag)) * NTSCDec(!mn_provv2) / 100, oCldGnnp.TrovaNdec(0))
                  End If
                  ocldBase.ScriviTabellaSemplice(strDittaCorrente, "TTMOPERNP", dsTTMopernp.Tables("TTMOPERNP"), "", "", "")
                  dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_qtaass = (NTSCDec(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_qtaass) + dResordine)
                  ocldBase.ScriviTabellaSemplice(strDittaCorrente, "TTDISPNET", dsTTDispnet.Tables("TTDISPNET"), "", "", "")
                End If
              End If
              dResiduo = (NTSCDec(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_dispnet) - NTSCDec(dsTTDispnet.Tables("TTDISPNET").Rows(i)!dn_qtaass))
              If dResiduo = 0 Then Continue For
            End If
          End With
        Next
        '--- Passa all'articolo successivo
      Next ' fine ciclo su ttdispnet

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

  Public Overridable Function NuovoInsertTTGegnnp() As Boolean
    Try
      Return oCldGnnp.NuovoInsertTTGegnnp(strDittaCorrente, lIITTGeGnnp, lIITTMoPernp)

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

  Public Overridable Function SettaOrdiniNonASaldo() As Boolean
    Try
      Return oCldGnnp.SettaOrdiniNonASaldo(strDittaCorrente, lIITTGeGnnp, strGndtTipork, lIITTMoPernp)

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

  Public Overridable Function CalcolaValoreMerce(ByRef ds As DataSet) As Boolean
    Try
      Return oCldGnnp.CalcolaValoreMerce(strDittaCorrente, lIITTGeGnnp, ds)

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

  Public Overridable Function EliminaOrdiniNonasaldo(ByVal bChiediconf As Boolean) As Boolean
    Dim evt As NTSEventArgs = Nothing
    Try
      If bChiediconf = True Then
        evt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128723435626002460, "Procedo con la eliminazione degli ordini non consegnabili (solo a saldo)?"))
        ThrowRemoteEvent(evt)
        If Not evt.RetValue = "YES" Then
          Return False
        End If
      End If

      Return oCldGnnp.EliminaOrdiniNonasaldo(strDittaCorrente, lIITTMoPernp, lIITTGeGnnp, bCancellaNonASaldoNew)

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

  Public Overridable Function EliminaOrdiniOltremax(ByVal bChiediconf As Boolean) As Boolean
    Dim evt As NTSEventArgs = Nothing
    Try
      bEliminaOrdiniOltremax = False
      If bChiediconf = True Then
        evt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128723438930213468, "Procedo con la eliminazione degli ordini in eccesso per numero ? "))
        ThrowRemoteEvent(evt)
        If Not evt.RetValue = ThMsg.RETVALUE_YES Then Return False
      End If

      bEliminaOrdiniOltremax = True
      Return oCldGnnp.EliminaOrdiniOltremax(strDittaCorrente, nSeicMaxnumord, lIITTMoPernp, lIITTGeGnnp, dsShared)
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

  Public Overridable Function EliminaOrdiniAvalorezero(ByVal bChiediconf As Boolean) As Boolean
    Dim evt As NTSEventArgs = Nothing
    Try
      bEliminaOrdiniAvalorezero = False
      If bChiediconf = True Then
        evt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128723445934684468, "Vuoi eliminare gli ordini con valore da evadere pari a zero?"))
        ThrowRemoteEvent(evt)
        If Not evt.RetValue = ThMsg.RETVALUE_YES Then Return False
      End If

      bEliminaOrdiniAvalorezero = True
      Return oCldGnnp.EliminaOrdiniAvalorezero(strDittaCorrente, lIITTMoPernp, lIITTGeGnnp)
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

  Public Overridable Function FileRielabora(ByVal bChiamato As Boolean) As Boolean
    Try
      Return oCldGnnp.FileRielabora(strDittaCorrente, lIITTMoPernp, lIITTGeGnnp)

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

  Public Overridable Function CancellaRigheSelezionate() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer
    Dim evt As NTSEventArgs = Nothing
    Try
      dtrTmp = dsShared.Tables("TTGEGNNP").Select("xx_seleziona = " & CStrSQL("S"))
      If dtrTmp.Length = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128723594543323898, "Attenzione!" & vbCrLf & _
          "Non sosno state selezionate righe da eliminare." & vbCrLf & _
          "Cancellazione delle righe selezionate annullata.")))
        Return False
      End If

      '----------------------------------------------------------------------------------------
      '--- Se sono state selezionate tutte le righe chiede conferma
      '----------------------------------------------------------------------------------------
      If dtrTmp.Length = dsShared.Tables("TTGEGNNP").Rows.Count Then
        evt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128733968468660241, "Attenzione!" & vbCrLf & _
          "Sono state selezionate tutte le righe per la cancellazione." & vbCrLf & _
          "Procedere con la cancellazione totale delle righe?"))
        ThrowRemoteEvent(evt)
        If evt.RetValue = "YES" Then
          oCldGnnp.DeleteAllRighe(strDittaCorrente, lIITTMoPernp, lIITTGeGnnp)
          Return True
        Else
          Return False
        End If
      End If
      '----------------------------------------------------------------------------------------
      '--- Chiede conferma dell'operazione
      '----------------------------------------------------------------------------------------
      evt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128723597585824364, "Confermare la cancellazione delle righe selezionate?"))
      ThrowRemoteEvent(evt)
      If Not evt.RetValue = "YES" Then
        Return False
      End If
      '----------------------------------------------------------------------------------------
      '--- Per ogni riga selezionata, la cancella
      '----------------------------------------------------------------------------------------
      For i = 0 To dtrTmp.Length - 1
        oCldGnnp.DeleteRighe(strDittaCorrente, lIITTMoPernp, lIITTGeGnnp, dtrTmp(i))
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
    End Try
  End Function

  Public Overridable Function CancellaRigheNonSelezionate() As Boolean
    Dim dtrTmp() As DataRow
    Dim i As Integer
    Dim evt As NTSEventArgs = Nothing
    Try
      dtrTmp = dsShared.Tables("TTGEGNNP").Select("xx_seleziona = " & CStrSQL("N"))
      If dtrTmp.Length = 0 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128723644331243376, "Attenzione!" & vbCrLf & _
          "Non sono state selezionate righe da escludere dalla cancellazione." & vbCrLf & _
          "Procedere con la cancellazione di tutte le righe?")))
        Return False
      End If

      '----------------------------------------------------------------------------------------
      '--- Se non sosno state selezionate righe, chiede se eliminarle tutte
      '----------------------------------------------------------------------------------------
      If dtrTmp.Length = 0 Then
        evt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128723644353585982, "Attenzione!" & vbCrLf & _
          "Non sono state selezionate righe da escludere dalla cancellazione." & vbCrLf & _
          "Procedere con la cancellazione di tutte le righe?"))
        ThrowRemoteEvent(evt)
        If evt.RetValue = "YES" Then
          oCldGnnp.DeleteAllRighe(strDittaCorrente, lIITTMoPernp, lIITTGeGnnp)
          Return True
        Else
          Return False
        End If
      End If

      '----------------------------------------------------------------------------------------
      '--- Se sono state selezionate tutte le righe, avvisa ed esce
      '----------------------------------------------------------------------------------------
      If dtrTmp.Length = dsShared.Tables("TTGEGNNP").Rows.Count Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128723647359682062, "Attenzione!" & vbCrLf & _
          "Sono state selezionate tutte le righe." & vbCrLf & _
          "Pertanto non sarà possibile eliminare righe NON selezionate." & vbCrLf & _
          "Operazione annullata.")))
        Return False
      End If
      '----------------------------------------------------------------------------------------
      '--- Chiede conferma dell'operazione
      '----------------------------------------------------------------------------------------
      evt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128723644388427948, "Confermare la cancellazione delle righe NON selezionate?"))
      ThrowRemoteEvent(evt)
      If Not evt.RetValue = "YES" Then
        Return False
      End If
      '----------------------------------------------------------------------------------------
      '--- Per ogni riga non selezionata, la cancella
      '----------------------------------------------------------------------------------------
      For i = 0 To dtrTmp.Length - 1
        oCldGnnp.DeleteRighe(strDittaCorrente, lIITTMoPernp, lIITTGeGnnp, dtrTmp(i))
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
    End Try
  End Function

  Public Overridable Function FileModifica(ByVal ds As DataSet) As Boolean
    Dim i As Integer
    Dim j As Integer
    Dim k As Integer
    Dim dTotdoc As Decimal
    Dim dsTmp As DataSet = Nothing
    Try
      ' adesso aggiorna il fd_totdoc su gliglia, supponendo di aver modificato qualsosa a livello di assegnazione
      For i = 0 To ds.Tables("TTGEGNNP").Rows.Count - 1
        ' lo elimina solo se c'e qualcosa (totdoc <> 0
        dTotdoc = 0

        'non a taglia e colori somma il totale documento
        oCldGnnp.FileModificaSelect(strDittaCorrente, ds.Tables("TTGEGNNP").Rows(i), lIITTMoPernp, dsTmp)

        If dsTmp.Tables("TTMOPERNP").Rows.Count > 0 Then
          dTotdoc = dTotdoc + NTSCDec(dsTmp.Tables("TTMOPERNP").Rows(0)!expr1)
        End If

        'a taglia e colori somma il totale documento
        For k = 1 To 24
          oCldGnnp.FileModificaSelectTC(strDittaCorrente, ds.Tables("TTGEGNNP").Rows(i), lIITTMoPernp, k, _
                                        dsTmp)

          For j = 0 To dsTmp.Tables("TTMOPERNP").Rows.Count - 1
            dTotdoc = dTotdoc + NTSCDec(dsTmp.Tables("TTMOPERNP").Rows(j)!expr1)
          Next
        Next

        ' aggiorna il totale documento
        oCldGnnp.FileModificaUpdate(strDittaCorrente, ds.Tables("TTGEGNNP").Rows(i), lIITTGeGnnp, dTotdoc)
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
    End Try
  End Function

  Public Overridable Function SetOpzioniReg() As Boolean
    Try
      oCleBoll.strCalcPesi = oCldGnnp.GetSettingBus("Bsveboll", "Opzioni", ".", "Calc_pesi_in_doc", " ", " ", " ")
      '-----------------------------------------------------------------------
      'Intrastat
      nINTRACodrsta = NTSCInt(oCldGnnp.GetSettingBus("Bsorgnnp", "Opzioni", ".", "IntraRegimeStatistico", "0", " ", "0"))
      'Controllo validità regime statistico
      If nINTRACodrsta <> 0 Then
        If Not oCldGnnp.ValCodiceDb(nINTRACodrsta.ToString, strDittaCorrente, "TABRSTA", "") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128727085565490992, "Opzione di registro \Bsorgnnp\Opzioni\IntraRegimeStatistico contiene un codice di registe statistico Intrastat inesistente. Controllare la proprietà.")))
          nINTRACodrsta = 0
        End If
      End If
      strINTRACodPort = Microsoft.VisualBasic.Left(oCldGnnp.GetSettingBus("Bsorgnnp", "Opzioni", ".", "IntraCodicePorto", " ", " ", " "), 1)
      If strINTRACodPort = "" Then strINTRACodPort = " "
      'Controllo validità codice porto
      If strINTRACodPort <> " " Then
        If Not oCldGnnp.ValCodiceDb(strINTRACodPort, strDittaCorrente, "TABMPOR", "") Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128727085823619296, "Opzione di registro \Bsorgnnp\Opzioni\IntraCodicePorto contiene un codice di porto Intrastat inesistente. Controllare la proprietà.")))
          strINTRACodPort = " "
        End If
      End If

      lIItttasks = oCldGnnp.GetTblInstId("TTTASKS", False)
      lIIttproesebappo = oCldGnnp.GetTblInstId("TTPROESEC", False)
      bPMControllaBaseCostMG = CBool(oCldGnnp.GetSettingBus("OPZIONI", ".", ".", "PMControllaBaseCostMG", "0", " ", "0"))
      bPMSoloTaskRilasciatiMG = CBool(oCldGnnp.GetSettingBus("OPZIONI", ".", ".", "PMSoloTaskRilasciatiMG", "0", " ", "0"))
      '-----------------------------------------------------------------------------------------
      bRiportaNoteDaImpegno = CBool(NTSCInt(oCldGnnp.GetSettingBus("BSORGNNP", "OPZIONI", ".", "RiportaNoteDaImpegno", "0", " ", "0")))
      bCancellaNonASaldoNew = CBool(NTSCInt(oCldGnnp.GetSettingBus("BSORGNNP", "OPZIONI", ".", "CancellaNonASaldoNew", "0", " ", "0")))
      '-----------------------------------------------------------------------------------------
      bDeterminaBolliSuOperazEsenti = CBool(NTSCInt(oCldGnnp.GetSettingBus("Opzioni", ".", ".", "DeterminaBolliSuOperazEsenti", "0", " ", "0"))) 'Se attiva il bollo non viene determinato solo se in testata vi è il codice di esenzione, ma se la somma delle operazioni esenti del documenti (righe e spese di piede) supera la soglia minima in TABBOTR ' NON DOCUMENTARE
      bMostraListaImpegniAncheSeTuttiNonEvadibili = CBool(NTSCInt(oCldGnnp.GetSettingBus("Bsorgnnp", "OPZIONI", ".", "MostraListaImpegniAncheSeTuttiNonEvadibili", "0", " ", "0"))) 'Se abilitata quando si lancia un'elaborazione e tutti le righe d'impegno non sono evadibili per mancanza di disponibilità, mostra comunque la lista degli impegni (come nella versione 11.0.0.198 o inferiore) senza far apparire il messaggio "Non ci sono disponibilità nette da assegnare."
      bLogSelezioneDati = CBool(oCldGnnp.GetSettingBus("BSORGNNP", "OPZIONI", ".", "LogSelezioneDati", "0", ".", "0"))

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

  Public Overridable Function DeleteTTMopernp(ByVal dtrTmp As DataRow) As Boolean
    Try
      oCldGnnp.DeleteTTMopernp(strDittaCorrente, lIITTMoPernp, dtrTmp)

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

  Public Overridable Function CancellaRecordTTMOPERNP(ByVal nAnno As Integer, ByVal strSerie As String, _
      ByVal lNumord As Integer) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      oCldGnnp.CancellaRecordTTMOPERNP(strDittaCorrente, lIITTMoPernp, nAnno, strSerie, lNumord)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function

  Public Overridable Function ScriviLogSelezioneDati(ByVal dttDati As DataTable) As Boolean
    Dim strDesogglog As String = ""
    Try
      If strActLog = "-1" AndAlso bLogSelezioneDati Then
        strDesogglog = oApp.Tr(Me, 130277032066230361, "I filtri selezionati hanno generato la where:") & vbCrLf & strSeicQuery & vbCrLf & vbCrLf
        strDesogglog &= oApp.Tr(Me, 130277034070026418, "Vuoi eliminare gli ordini con valore da evadere pari a zero? ") & NTSCStr(IIf(bEliminaOrdiniAvalorezero, oApp.Tr(Me, 130277033954522820, "'Si'"), oApp.Tr(Me, 130277034027633691, "'No'"))) & vbCrLf
        strDesogglog &= oApp.Tr(Me, 130277034090835189, "Vuoi riassegnare ora eventuali disponibilità liberate? ") & NTSCStr(IIf(bFileRielabora, oApp.Tr(Me, 130277033978036114, "'Si'"), oApp.Tr(Me, 130277034009735021, "'No'"))) & vbCrLf
        strDesogglog &= oApp.Tr(Me, 130277033110698476, "Procedo con la eliminazione degli ordini in eccesso per numero? ") & NTSCStr(IIf(bEliminaOrdiniOltremax, oApp.Tr(Me, 130277033737882258, "'Si'"), oApp.Tr(Me, 130277033405723861, "'No'"))) & vbCrLf & vbCrLf

        strDesogglog &= oApp.Tr(Me, 130277041793363069, "Sono state selezionate |" & dttDati.Rows.Count & "| righe:") & vbCrLf

        For z As Integer = 0 To dttDati.Rows.Count - 1
          With dttDati.Rows(z)
            strDesogglog &= " - " & NTSCInt(!fd_anno) & " '" & NTSCStr(!fd_serie) & "' " & NTSCStr(!fd_numdoc).PadLeft(6) & " - " & _
                            NTSCDate(!fd_datdoc).ToShortDateString & " - " & NTSCInt(!fd_conto) & vbCrLf
          End With
        Next

        oCldGnnp.ScriviActLog(strDittaCorrente, "BSORGNNP", "BSORGNNP", "", "", "M", "D", strDesogglog, False)
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
  Public Overridable Function ScriviLogGenerazione() As Boolean
    Dim strDesogglog As String = ""
    Try
      If strActLog = "-1" Then
        strDesogglog = strDesogglog & vbCrLf & oApp.Tr(Me, 130277021619201087, " - Selezionando i dati da.......................: ")
        Select Case nSeicTipo
          Case 1 : strDesogglog = strDesogglog & oApp.Tr(Me, 130277021695522266, "'Impegni clienti'") & vbCrLf
          Case 2 : strDesogglog = strDesogglog & oApp.Tr(Me, 130277021665125139, "'Impegni di trasferimento'") & vbCrLf
          Case 3 : strDesogglog = strDesogglog & oApp.Tr(Me, 130277021720633713, "'Impegni di produzione'") & vbCrLf
        End Select
        strDesogglog = strDesogglog & oApp.Tr(Me, 130277021739038191, " - Priorità di assegnamento.....................: ")
        Select Case nSeicPriorita
          Case 1 : strDesogglog = strDesogglog & oApp.Tr(Me, 130277021787990936, "'Data di consegna'") & vbCrLf
          Case 2 : strDesogglog = strDesogglog & oApp.Tr(Me, 130277021806721596, "'Codice cliente'") & vbCrLf
          Case 3 : strDesogglog = strDesogglog & oApp.Tr(Me, 130277021828470866, "'Data ordine'") & vbCrLf
          Case 4 : strDesogglog = strDesogglog & oApp.Tr(Me, 130277021849280544, "'Affidabilità cliente'") & vbCrLf
        End Select

        strDesogglog = strDesogglog & _
          oApp.Tr(Me, 130277021875356255, " - Clienti/Fornitori............................: ") & NTSCStr(IIf(nSeicClienti = 0, oApp.Tr(Me, 130277022320141015, "'Tutti'"), oApp.Tr(Me, 130277022408301439, "Dal conto '|" & lSeicClienteini & "|' al conto '|" & lSeicClientefin & "|'"))) & vbCrLf & _
          oApp.Tr(Me, 130277021895189283, " - Data impegno.................................: ") & NTSCStr(IIf(nSeicDate = 0, oApp.Tr(Me, 130277022293258886, "'Tutte'"), oApp.Tr(Me, 130277022451984254, "Dalla data '|" & strSeicDataini & "|' alla data '|" & strSeicDatafin & "|'"))) & vbCrLf & _
          oApp.Tr(Me, 130277021915195289, " - Magazzino....................................: '") & nSeicMagaz & "'" & vbCrLf & _
          oApp.Tr(Me, 130277021939835541, " - Fino a data consegna.........................: '") & strSeicADatacons & "'" & vbCrLf & _
          oApp.Tr(Me, 130277021961592299, " - Tipo bolla/fattura...........................: ") & NTSCStr(IIf(nSeicBolle = 0, oApp.Tr(Me, 130277022270230485, "'Tutte'"), "'" & nSeicTipobf & "'")) & vbCrLf & _
          oApp.Tr(Me, 130277021986066693, " - Agente.......................................: ") & NTSCStr(IIf(nSeicAgenti = 0, oApp.Tr(Me, 130277022255822526, "'Tutti'"), "'" & nSeicCodcage & "'")) & vbCrLf & _
          oApp.Tr(Me, 130277022004797455, " - Tutti gli impegni inevasi....................: ") & NTSCStr(IIf(bSeicTutti = True, oApp.Tr(Me, 130277022235018715, "'Sì'"), oApp.Tr(Me, 130277022470702036, "'No'"))) & vbCrLf & _
          oApp.Tr(Me, 130277022028951641, " - Considera solo a saldo.......................: ") & NTSCStr(IIf(bSeicConssoloasa = True, oApp.Tr(Me, 130277022205911051, "'Sì'"), oApp.Tr(Me, 130277022185584546, "'No'"))) & vbCrLf & _
          oApp.Tr(Me, 130277022046552066, " - Solo impegno confermati......................: ") & NTSCStr(IIf(bSeicConf = True, oApp.Tr(Me, 130277022163823720, "'Sì'"), oApp.Tr(Me, 130277022489893795, "'No'"))) & vbCrLf & _
          oApp.Tr(Me, 130277022096311052, " - Considera max num. ord.......................: '") & nSeicMaxnumord & "'" & vbCrLf & _
          oApp.Tr(Me, 130277022121115711, " - Zona.........................................: ") & NTSCStr(IIf(nSeicCodzona = 0, oApp.Tr(Me, 130277022145909278, "'Tutte'"), "'" & nSeicCodzona & "'")) & vbCrLf

        oCldGnnp.ScriviActLog(strDittaCorrente, "BSORGNNP", "BSORGNNP", "", "", "M", "D", strDesogglog, False)
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

  Public Overridable Function ControllaModuliPickingAnazmod() As Boolean
    Try
      Return oCldGnnp.ControllaModuliPickingAnazmod(strDittaCorrente)
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

#Region "FORM BNORSEIC"
  Public Overridable Function edTipoBf_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldGnnp.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABTPBF", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128607750870156250, "Codice bolla/fattura |'" & nCod.ToString & "'| inesistente")))
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
  Public Overridable Function edCodcage_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldGnnp.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABCAGE", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711257209836831, "Codice agente |'" & nCod.ToString & "'| inesistente")))
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
  Public Overridable Function edCodzona_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldGnnp.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABZONE", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711257411394171, "Codice zona |'" & nCod.ToString & "'| inesistente")))
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

  Public Overridable Function LeggiDatiDitta(ByVal strDitta As String) As Boolean
    Dim dttTmp As New DataTable
    Try
      oCldGnnp.ValCodiceDb(strDitta, strDitta, "TABANAZ", "S", "", dttTmp)
      strDittaCorrente = strDitta
      nSeicEscomp = NTSCInt(dttTmp.Rows(0)!tb_escomp)

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

  Public Overridable Function ComponiQuerySeic(ByVal stredADataCons As String, ByVal stredCodmaga As String, _
                                               ByVal stredSerie As String, ByVal bckTutto As Boolean, _
                                               ByVal bopTuttiClienti As Boolean, ByVal stredDalcliente As String, _
                                               ByVal stredAlcliente As String, ByVal bopTutteDate As Boolean, _
                                               ByVal stredDalladata As String, ByVal stredAlladata As String, _
                                               ByVal bopTutteBolle As Boolean, ByVal stredTipobf As String, _
                                               ByVal bopTuttiAgenti As Boolean, ByVal stredCodcage As String, _
                                               ByVal bopTutteZone As Boolean, ByVal stredCodzona As String, _
                                               ByVal bckConf As Boolean, ByVal stredCommecaini As String, _
                                               ByVal stredCommecafin As String, ByVal bopImp0 As Boolean, _
                                               ByVal bopImp1 As Boolean, ByVal bopImp2 As Boolean, _
                                               ByVal bckConssoloasa As Boolean, ByVal stredMaxnumord As String, _
                                               ByVal bopPriorData As Boolean, ByVal bopPriorCliente As Boolean, _
                                               ByVal bopPriorNumero As Boolean, ByVal bopPriorAffidab As Boolean) As Boolean

    Dim strTipo As String = ""
    Dim bRetailExt As Boolean = False
    Try
      'con tipo R seleziona solo impegni clienti ...
      'ma ora ci sono anche gli altri:
      'X impegno trasferimento
      'Y impegno produzione
      Select Case True
        Case bopImp0
          strTipo = "R"
        Case bopImp1
          strTipo = "X"
        Case bopImp2
          strTipo = "Y"
      End Select
      strGndtTipork = strTipo

      If CBool(ModuliSupDittaDitt(strDittaCorrente) And bsModSupGPE) Then bRetailExt = True

      oCldGnnp.ComponiQuerySeic(strTipo, stredADataCons, stredCodmaga, stredSerie, _
                                bckTutto, bopTuttiClienti, stredDalcliente, _
                                stredAlcliente, bopTutteDate, stredDalladata, stredAlladata, _
                                bopTutteBolle, stredTipobf, bopTuttiAgenti, stredCodcage, _
                                bopTutteZone, stredCodzona, bckConf, strTipoConferma, _
                                stredCommecaini, stredCommecafin, nSenuAnno, strSenuSerie, _
                                strDittaCorrente, lIITTOltomo, bDaSelezioneArticoli, strWhereFiar, _
                                strSeicQuery, bRetailExt)

      '----------------------------------------------------------------------------------------
      bSeicConssoloasa = bckConssoloasa
      nSeicMaxnumord = NTSCInt(stredMaxnumord)
      'Priorità di assegnamento
      '----------------------------------------------------------------------------------------
      '--- Servono per la scrittura in ACTLOG
      '----------------------------------------------------------------------------------------
      If bopPriorData = True Then nSeicPriorita = 1
      If bopPriorCliente = True Then nSeicPriorita = 2
      If bopPriorNumero = True Then nSeicPriorita = 3
      If bopPriorAffidab = True Then nSeicPriorita = 4
      If bopTuttiClienti = True Then
        nSeicClienti = 0
        lSeicClienteini = 0
        lSeicClientefin = 0
      Else
        nSeicClienti = 1
        lSeicClienteini = NTSCInt(stredDalcliente)
        lSeicClientefin = NTSCInt(stredAlcliente)
      End If
      If bopTutteDate = True Then
        nSeicDate = 0
        strSeicDataini = ""
        strSeicDatafin = ""
      Else
        nSeicDate = 1
        strSeicDataini = stredDalladata
        strSeicDatafin = stredAlladata
      End If
      If bopImp0 = True Then nSeicTipo = 1
      If bopImp1 = True Then nSeicTipo = 2
      If bopImp2 = True Then nSeicTipo = 3
      nSeicMagaz = NTSCInt(stredCodmaga)
      strSeicADatacons = stredADataCons
      If bopTutteBolle = True Then
        nSeicBolle = 0
        nSeicTipobf = 0
      Else
        nSeicBolle = 1
        nSeicTipobf = NTSCInt(stredTipobf)
      End If
      If bopTuttiAgenti = True Then
        nSeicAgenti = 0
        nSeicCodcage = 0
      Else
        nSeicAgenti = 1
        nSeicCodcage = NTSCInt(stredCodcage)
      End If
      bSeicTutti = bckTutto
      bSeicConssoloasa = bckConssoloasa
      bSeicConf = bckConf
      nSeicMaxnumord = NTSCInt(stredMaxnumord)
      If bopTutteZone = True Then
        nSeicZone = 0
        nSeicCodzona = 0
      Else
        nSeicZone = 1
        nSeicCodzona = NTSCInt(stredCodzona)
      End If
      '----------------------------------------------------------------------------------------

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

  Public Overridable Function CheckSelection(ByVal stredDalcliente As String, ByVal stredAlcliente As String, _
                                             ByVal stredDalladata As String, ByVal stredAlladata As String, _
                                             ByVal stredCommecaini As String, ByVal stredCommecafin As String) As Boolean
    Dim lGiorni As Integer
    Try
      If NTSCInt(stredDalcliente) > NTSCInt(stredAlcliente) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711317823753041, "Il codice cliente iniziale non può essere superiore a quello finale")))
        Return False
      End If

      lGiorni = NTSCInt(DateDiff("d", NTSCDate(stredDalladata), NTSCDate(stredAlladata)))
      If lGiorni <> 0 Then
        If lGiorni < 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711317512657300, "La data impegno di iniziale non può essere superiore a quella finale")))
          Return False
        End If
      End If

      If NTSCInt(stredCommecaini) > NTSCInt(stredCommecafin) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128720743032343750, "Il codice commessa iniziale non può essere superiore a quello finale")))
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
#End Region

#Region "FORM BNVEDTAC"
  Public Overridable Function edVettor_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim bOut As Boolean = False
    Try
      If nCod = 0 Then
        strDescr = ""
        Return True
      End If

      bOut = oCldGnnp.ValCodiceDb(nCod.ToString, strDittaCorrente, "TABVETT", "N", strDescr)
      If bOut = False Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711505311389814, "Codice vettore |'" & nCod.ToString & "'| inesistente")))
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

  Public Overridable Function DtacCheckSelection(ByVal stredAnnofat As String, ByVal stredDatfat As String) As Boolean
    Try
      If NTSCInt(stredAnnofat) <> Year(NTSCDate(stredDatfat)) Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128711506082478629, "La data attribuita ai nuovi documenti non è inclusa nell'anno indicato.")))
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
#End Region

#Region "FORM BNORSENU"
  Public Overridable Sub SenuOnAddNew(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      lProg = lProg + 1
      e.Row!tt_olprogr = lProg

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
  Public Overridable Function SenuApri(ByVal strDitta As String, ByRef dsSenu As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldGnnp.GetDataSenu(strDitta, lSenuInstid, dsSenu)
      If dReturn = False Then Return False

      oCldGnnp.SetTableDefaultValueFromDB("TTOLTOMO", dsSenu)

      SenuSetDefaultValue(dsSenu)

      dsSenuShared = dsSenu

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsSenuShared.Tables("TTOLTOMO").ColumnChanging, AddressOf SenuBeforeColUpdate
      AddHandler dsSenuShared.Tables("TTOLTOMO").ColumnChanged, AddressOf SenuAfterColUpdate

      '--------------------------------------
      'imposto i valori di default per i nuovi record
      dsSenuShared.Tables("TTOLTOMO").Columns("codditt").DefaultValue = strDittaCorrente
      'dsSenuShared.Tables("TTOLTOMO").Columns("ak_codart").DefaultValue = lSenuInstid

      dsSenuShared.Tables("TTOLTOMO").AcceptChanges()

      bSenuHasChanges = False

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
  Public Overridable Function SenuSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not SenuTestPreSalva() Then Return False
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldGnnp.ScriviTabellaSemplice(strDittaCorrente, "TTOLTOMO", dsSenuShared.Tables("TTOLTOMO"), "", "", "")

      If bResult Then
        bSenuHasChanges = False
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
  Public ReadOnly Property SenuRecordIsChanged() As Boolean
    Get
      Return bSenuHasChanges
    End Get
  End Property
  Public Overridable Function SenuTestPreSalva() As Boolean
    Dim dtrCurrRow() As DataRow
    Dim dtrTmp() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0

    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsSenuShared.Tables("TTOLTOMO").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        If NTSCInt(dtrCurrRow(i)!tt_monumord) = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128563510511861225, "Numero ordine obbligatorio.")))
          Return False
        End If
      Next

      '-------------------------------------------------
      'controlla che rk sia gia presente nella composizione corrente
      dtrTmp = dsSenuShared.Tables("TTOLTOMO").Select("codditt = " & CStrSQL(dtrCurrRow(0)!codditt.ToString) & _
      " AND instid = " & dtrCurrRow(0)!instid.ToString & " AND tt_olprogr = " & dtrCurrRow(0)!tt_olprogr.ToString & _
      " AND tt_moserie = " & CStrSQL(dtrCurrRow(0)!tt_moserie.ToString) & " AND tt_monumord = " & dtrCurrRow(0)!tt_monumord.ToString)
      If dtrTmp.Length > 1 Then
        ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128563555295890783, "Documento già presente nella selezione")))
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
      Return False
    End Try
  End Function
  Public Overridable Sub SenuNuovo()
    Try
      '----------------------------------------
      'inserisco una nuova riga
      dsSenuShared.Tables("TTOLTOMO").Rows.Add(dsSenuShared.Tables("TTOLTOMO").NewRow)
      bSenuHasChanges = True

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
  Public Overridable Function SenuRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsSenuShared.Tables("TTOLTOMO").Select(strFilter)(nRow).RejectChanges()
      bSenuHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function
  Public Overridable Sub SenuSetDefaultValue(ByRef ds As DataSet)
    Try
      'ora imposto i valori di default diversi da quelli impostati nel database
      ds.Tables("TTOLTOMO").Columns("codditt").DefaultValue = strDittaCorrente
      ds.Tables("TTOLTOMO").Columns("instid").DefaultValue = lSenuInstid
      ds.Tables("TTOLTOMO").Columns("tt_moserie").DefaultValue = strSenuSerie
      ds.Tables("TTOLTOMO").Columns("tt_monumord").DefaultValue = "1"

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

  Public Overridable Sub SenuBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      '-------------------------------------------------------------
      'se non  compilato il numero di riga compilo i campi di default
      If e.Column.ColumnName <> "tt_olprogr" Then
        If e.Row!tt_olprogr.ToString = "0" Then SenuOnAddNew(sender, e)
      End If

      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "SenuBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub SenuAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bSenuHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "SenuAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub SenuBeforeColUpdate_tt_moserie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(NTSCStr(e.ProposedValue), False)
      If strTmp <> NTSCStr(e.ProposedValue) Then e.ProposedValue = strTmp

      e.ProposedValue = UCase(e.ProposedValue.ToString)

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

#Region "FORM BNORGNDA"
  Public Overridable Function GndaApri(ByVal strDitta As String, ByRef dsGnda As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldGnnp.GetDataGnda(strDitta, lIITTDispNet, dsGnda)
      If dReturn = False Then Return False

      oCldGnnp.SetTableDefaultValueFromDB("TTDISPNET", dsGnda)

      dsGndaShared = dsGnda

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

  Public Overridable Function GndaModifica(ByVal strCodart As String, ByVal strCommeca As String, _
                                            ByVal strLotto As String, ByVal strFase As String, _
                                            ByRef ds As DataSet) As Boolean
    Try
      Return GndaModifica(strCodart, strCommeca, strLotto, strFase, 0, ds)

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

  Public Overridable Function GndaModifica(ByVal strCodart As String, ByVal strCommeca As String, _
                                           ByVal strLotto As String, ByVal strFase As String, _
                                           ByVal nIndiceTaglia As Integer, ByRef ds As DataSet) As Boolean
    Try

      '----------------
      'per compatibilita' con funzioni ereditate da rive in versioni precedenti
      Dim oOut As Object = Nothing
      Dim oIn As New ArrayList(New Object() {strCodart, strCommeca, strLotto, strFase, nIndiceTaglia, ds, "RISPETTARE l'ORDINE DI COME SONO INDICATI I PARAMETRI NELLA FIRMA DELLA FUNZIONE"})
      If CLN__STD.CheckInvokeCustomFunction(CustomClass, Me, System.Reflection.MethodInfo.GetCurrentMethod, oIn, oOut) Then
        ds = CType(oIn(5), DataSet)        'esempio: da impostare per tutti i parametri funzione passati ByRef !!!!
        Return CBool(oOut)
      End If
      '----------------

      Return oCldGnnp.GndaModifica(strDittaCorrente, lIITTMoPernp, strCodart, strCommeca, _
                                   strLotto, strFase, nIndiceTaglia, ds)

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


  Public Overridable Function GndaSalva(ByVal bDelete As Boolean) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = ocldBase.ScriviTabellaSemplice(strDittaCorrente, "TTDISPNET", dsGndaShared.Tables("TTDISPNET"), "", "", "")

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
#End Region

#Region "FORM BNORGNDT"
  Public Overridable Function GndtApri(ByVal strDitta As String, ByRef dsGndt As DataSet) As Boolean
    Dim dReturn As Boolean = False
    Try
      '--------------------------------------
      'tengo un puntatore al datatable che verr usato nell'UI, quindi chiedo i dati al DAL
      strDittaCorrente = strDitta

      dReturn = oCldGnnp.GetDataGndt(strDitta, strGndtCodart, strGndtTipork, nGndtAnno, _
                                     strGndtSerie, lGndtNumdoc, lIITTMoPernp, lGndtCommeca, _
                                     lGndtLotto, nGndtFase, nGndtTaglia, dsGndt)
      If dReturn = False Then Return False

      oCldGnnp.SetTableDefaultValueFromDB("TTMOPERNP", dsGndt)

      dsGndtShared = dsGndt

      '--------------------------------------
      'creo gli eventi per la gestione del datatable dentro l'entity
      AddHandler dsGndtShared.Tables("TTMOPERNP").ColumnChanging, AddressOf GndtBeforeColUpdate
      AddHandler dsGndtShared.Tables("TTMOPERNP").ColumnChanged, AddressOf GndtAfterColUpdate

      bGndtHasChanges = False

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
  Public Overridable Function GndtSalva(ByVal bDelete As Boolean, _
                                        ByVal stredCommeca As String, ByVal stredLotto As String, _
                                        ByVal stredFase As String, ByVal stredTaglia As String) As Boolean
    Dim bResult As Boolean = False
    Try
      '----------------------------------------
      'controlli pre-salvataggio (solo se non  una delete)
      If Not bDelete Then
        If Not GndtTestPreSalva(stredCommeca, stredLotto, stredFase, stredTaglia) Then Return False
      End If
      '----------------------------------------
      'chiamo il dal per salvare
      bResult = oCldGnnp.ScriviTabellaSemplice(strDittaCorrente, "TTMOPERNP", dsGndtShared.Tables("TTMOPERNP"), "", "", "")

      If bResult Then
        bGndtHasChanges = False
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
  Public ReadOnly Property GndtRecordIsChanged() As Boolean
    Get
      Return bGndtHasChanges
    End Get
  End Property
  Public Overridable Function GndtTestPreSalva(ByVal stredCommeca As String, ByVal stredLotto As String, _
                                               ByVal stredFase As String, ByVal stredTaglia As String) As Boolean
    Dim dtrCurrRow() As DataRow
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Dim dResordine As Decimal
    Dim dResordinecolli As Decimal
    Dim dValore As Decimal
    Dim dVAloreV As Decimal
    Dim dsTmp As DataSet = Nothing
    Dim evt As NTSEventArgs = Nothing
    Try
      '-------------------------------------------------
      'verifico le righe aggiunte o modificate: dovrebbe sempre essere una sola riga
      dtrCurrRow = dsGndtShared.Tables("TTMOPERNP").Select(Nothing, Nothing, DataViewRowState.Added Or DataViewRowState.ModifiedCurrent)
      If dtrCurrRow.Length = 0 Then Return True

      For i = 0 To dtrCurrRow.Length - 1
        'se articolo taglia e colori
        'controlla che la qta evadibile non sia superiore della qta assegnata
        If NTSCInt(stredTaglia) <> 0 Then
          If Not bConsentiEvasSuperioreTC Then
            If (NTSCDec(dtrCurrRow(i)!mn_quant) - NTSCDec(dtrCurrRow(i)!mn_quaeva) - NTSCDec(dtrCurrRow(i)!mn_quapre)) < NTSCDec(dtrCurrRow(i)!mn_mmquant) Then
              ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128726892672548224, "Impossibile evadere una quantità superiore a quella ordinata per articoli a taglia e colori.")))
              Return False
            End If
          End If
        End If
        'Impossibile evadere una quantità superiore di quella ordinata IN CONTO (parzialmente)
        dResordine = NTSCDec(dtrCurrRow(i)!mn_quant) - NTSCDec(dtrCurrRow(i)!mn_quaeva) - NTSCDec(dtrCurrRow(i)!mn_quapre)
        dResordinecolli = NTSCDec(dtrCurrRow(i)!mn_colli) - NTSCDec(dtrCurrRow(i)!mn_coleva) - NTSCDec(dtrCurrRow(i)!mn_colpre)
        If NTSCDec(dtrCurrRow(i)!mn_mmquant) >= dResordine And NTSCStr(dtrCurrRow(i)!mn_mmflevas) = "C" Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128726892906977406, "Impossibile evadere una quantità superiore o uguale a quella ordinata, in conto. Abilitate evasione totale.")))
          Return False
        End If

        oCldGnnp.GetSumTTMopernp(strDittaCorrente, NTSCStr(dtrCurrRow(i)!mn_codart), lIITTMoPernp, stredCommeca, _
                                 stredLotto, stredFase, stredTaglia, dsTmp)

        If dsTmp.Tables("TTMOPERNP").Rows.Count = 0 Then
          ThrowRemoteEvent(New NTSEventArgs("", oApp.Tr(Me, 128726898105963822, "Nessun record in TTMOPERNP.")))
          Return False
        End If
        'Cotrolla la disponibilità
        If ArrDbl(NTSCDec(dsTmp.Tables("TTMOPERNP").Rows(0)!expr1) - dLastQuant + NTSCDec(dtrCurrRow(i)!mn_mmquant), 3) > ArrDbl(dGndtDispon, 3) Then
          evt = New NTSEventArgs(CLN__STD.ThMsg.MSG_YESNO, oApp.Tr(Me, 128726998473080348, "La quantità che si intente evadere supera la disponibilità netta del magazzino. Procedere comunque?"))
          ThrowRemoteEvent(evt)
          If Not evt.RetValue = "YES" Then
            Return False
          End If
        End If
        If NTSCStr(dtrCurrRow(i)!mn_mmflevas) = "S" Then
          dtrCurrRow(i)!mn_mmqtadisimp = dResordine
          dtrCurrRow(i)!mn_mmcoldisimp = dResordinecolli
          dtrCurrRow(i)!mn_mmvaldisimp = NTSCDec(dtrCurrRow(i)!mn_valore)
        Else
          dtrCurrRow(i)!mn_mmqtadisimp = NTSCDec(dtrCurrRow(i)!mn_mmquant)
          dtrCurrRow(i)!mn_mmcoldisimp = NTSCDec(dtrCurrRow(i)!mn_colli)
          dtrCurrRow(i)!mn_mmvaldisimp = NTSCDec(dtrCurrRow(i)!mn_mmquant) * ArrDbl((NTSCDec(dtrCurrRow(i)!mn_valore) / dResordine), oCldGnnp.TrovaNdecSuPrzUn(0))
        End If
        'sistema VALORE/VALOREV/VPROVV/VPROVV1
        With dtrCurrRow(i)
          If !mn_umprz.ToString <> "S" Or _
             !mn_tipork.ToString = "Y" Or _
             (!mn_unmis.ToString.ToUpper = !mn_ump.ToString.ToUpper) Then
            dValore = ArrDbl(ArrDbl(NTSCDec(!mn_prezzo) * NTSCDec(!mn_mmquant) / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(0)) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(0))
          Else
            dValore = ArrDbl(ArrDbl(NTSCDec(!mn_prezzo) * NTSCDec(!mn_mmcolli) / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(0)) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(0))
          End If
          If NTSCInt(!mn_valuta) > 0 Then
            dVAloreV = ArrDbl(ArrDbl(NTSCDec(!mn_prezvalc) * NTSCDec(!mn_mmquant) / NTSCDec(!mn_perqta) * (100 - NTSCDec(!mn_scont1)) / 100 * (100 - NTSCDec(!mn_scont2)) / 100 * (100 - NTSCDec(!mn_scont3)) / 100 * (100 - NTSCDec(!mn_scont4)) / 100 * (100 - NTSCDec(!mn_scont5)) / 100 * (100 - NTSCDec(!mn_scont6)) / 100 * (100 - NTSCDec(!mn_scontp)) / 100 - NTSCDec(!mn_scontv), oCldGnnp.TrovaNdec(NTSCInt(!mn_valuta))) * (100 - NTSCDec(!mn_tmscont1)) / 100 * (100 - NTSCDec(!mn_tmscont2)) / 100 * (100 - NTSCDec(!mn_scopag)) / 100, oCldGnnp.TrovaNdec(NTSCInt(!mn_valuta)))
          Else
            dVAloreV = 0
          End If
          !mn_mmvalore = dValore
          !mn_mmvalorev = dVAloreV
          If NTSCStr(!mn_flstat) = "S" Then
            !mn_mmvprovv = ArrDbl(NTSCDec(!mn_mmquant) * NTSCDec(!mn_provv), oCldGnnp.TrovaNdec(0))
            !mn_mmvprovv2 = ArrDbl(NTSCDec(!mn_mmquant) * NTSCDec(!mn_provv2), oCldGnnp.TrovaNdec(0))
          Else
            !mn_mmvprovv = ArrDbl(dValore * 100 / (100 - NTSCDec(!mn_scopag)) * NTSCDec(!mn_provv) / 100, oCldGnnp.TrovaNdec(0))
            !mn_mmvprovv2 = ArrDbl(dValore * 100 / (100 - NTSCDec(!mn_scopag)) * NTSCDec(!mn_provv2) / 100, oCldGnnp.TrovaNdec(0))
          End If
        End With
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
  Public Overridable Function GndtRipristina(ByVal nRow As Integer, ByVal strFilter As String) As Boolean
    'non gestire l'eccezione in ripristino di una griglia: darebbe errore per un problema del framework
    Try
      dsGndtShared.Tables("TTMOPERNP").Select(strFilter)(nRow).RejectChanges()
      bGndtHasChanges = False
      Return True
    Catch ex As Exception
    End Try
  End Function

  Public Overridable Sub GndtBeforeColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      If ValoriUguali(e.ProposedValue.ToString, e.Row(e.Column.ColumnName).ToString) Then
        strPrevCelValue = e.Column.ColumnName.ToUpper & ";"
        Return
      End If

      dLastQuant = NTSCDec(e.Row!mn_mmquant)
      dLastColli = NTSCDec(e.Row!mn_mmcolli)
      If dLastColli > 0 Then
        dRap = dLastQuant / dLastColli
      Else
        dRap = 0
      End If

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "GndtBeforeColUpdate_" & e.Column.ColumnName.ToLower
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
  Public Overridable Sub GndtAfterColUpdate(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      'non valido la colonna se il dato non è cambiato
      If strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";") > -1 Then
        strPrevCelValue = strPrevCelValue.Remove(strPrevCelValue.IndexOf(e.Column.ColumnName.ToUpper + ";"), e.Column.ColumnName.ToUpper.Length + 1)
        Return
      End If

      bGndtHasChanges = True

      'comunico che una cella è cambiata, per fare in modo che se il dato è contenuto in una griglia 
      'vengano fatte le routine di validazione del caso
      ThrowRemoteEvent(New NTSEventArgs("GRIAGG", e.Column.Table.TableName & "§" & e.Column.ColumnName))

      e.Row.EndEdit()
      e.Row.EndEdit()

      '-------------------------------------------------------------
      'cerco e, se la trovo, eseguo la funzione specifica per la colonna modificata
      Dim strFunction As String = "GndtAfterColUpdate_" & e.Column.ColumnName.ToLower
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

  Public Overridable Sub GndtBeforeColUpdate_mn_mmquant(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      ' Il controllo è necessario per evitare loop infinito con una chiamata ricorsiva tra GndtBeforeColUpdate_mn_mmquant e GndtBeforeColUpdate_mn_mmcolli
      If Not bInCorso Then
        '----------------------------------------------------------------------------------
        '--- Se la quantità evadibile indicata è inferiore alla quantità ordinata
        '--- deseleziona il CheckBox realtivo all'evasione totale
        '----------------------------------------------------------------------------------
        If NTSCDec(e.ProposedValue) < NTSCDec(e.Row!mn_quant) Then
          e.Row!mn_mmflevas = "C"
        End If
        '----------------------------------------------------------------------------------
        'ricalcolo i colli dalla quantità
        bInCorso = True
        If dRap = 0 Then
          e.Row!mn_mmcolli = "0"
        Else
          e.Row!mn_mmcolli = NTSCDec(e.Row!mn_quant) / dRap
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
    Finally
      bInCorso = False
    End Try
  End Sub
  Public Overridable Sub GndtBeforeColUpdate_mn_mmcolli(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Try
      e.Row!mn_mmquant = dRap * NTSCDec(e.ProposedValue)

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
  Public Overridable Sub BeforeColUpdate_mn_serie(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(NTSCStr(e.ProposedValue), False)
      If strTmp <> NTSCStr(e.ProposedValue) Then e.ProposedValue = strTmp

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

  Public Overridable Function edCommeca_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim strTmp As String = ""
    Try
      If Not NTSCInt(nCod) = 0 Then
        If Not oCldGnnp.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "TABCOMMESS", "N", strTmp) Then
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
  Public Overridable Function edLotto_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim strTmp As String = ""
    Try
      If Not NTSCInt(nCod) = 0 And Trim(strGndtCodart) <> "" Then
        If Not oCldGnnp.ValCodiceDb(strGndtCodart, strDittaCorrente, "ANALOTTI", "N", strTmp, , NTSCStr(nCod)) Then
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
  Public Overridable Function edFase_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim strTmp As String = ""
    Try
      If Not NTSCInt(nCod) = 0 And Trim(strGndtCodart) <> "" Then
        If Not oCldGnnp.ValCodiceDb(NTSCStr(nCod), strDittaCorrente, "ARTFASI", "N", strTmp, , strGndtCodart) Then
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
  Public Overridable Function edTaglia_Validated(ByVal nCod As Integer, ByRef strDescr As String) As Boolean
    Dim strTmp As String = ""
    Dim dttTmp As New DataTable
    Dim dttTmp2 As New DataTable
    Dim strCodTagl As String
    Try
      If Trim(strGndtCodart) <> "" Then
        If Not oCldGnnp.ValCodiceDb(NTSCStr(strGndtCodart), strDittaCorrente, "ARTICO", "S", "", dttTmp) Then
          Return False
        Else
          strCodTagl = NTSCStr(dttTmp.Rows(0)!ar_codtagl)
          If Not NTSCInt(strCodTagl) = 0 Then
            If Not oCldGnnp.ValCodiceDb(NTSCStr(strCodTagl), strDittaCorrente, "TABTAGL", "N", "", dttTmp2) Then
              Return False
            Else
              strDescr = NTSCStr(dttTmp2.Rows(0)("tb_dest" & Right("00" & NTSCStr(nGndtTaglia), 2)))
            End If
          Else
            strDescr = ""
          End If
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

  Public Overridable Function GetSumTTMopernp(ByVal strCodart As String, ByVal stredCommeca As String, _
                                              ByVal stredLotto As String, ByVal stredFase As String, _
                                              ByVal stredTaglia As String, ByRef ds As DataSet) As Boolean
    Try
      oCldGnnp.GetSumTTMopernp(strDittaCorrente, strCodart, lIITTMoPernp, stredCommeca, _
                               stredLotto, stredFase, stredTaglia, ds)

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

  Public Overridable Function GetDestDiv(ByVal strMn_conto As String, ByVal strMn_coddest As String, _
                                         ByRef dtrTmp As DataRow) As Boolean
    Dim dttTmp As New DataTable
    Try
      dtrTmp = Nothing
      If Not NTSCInt(strMn_coddest) = 0 Then
        If Not oCldGnnp.ValCodiceDb(strMn_coddest, strDittaCorrente, "DESTDIV", "N", , dttTmp, strMn_conto) Then
          Return False
        Else
          dtrTmp = dttTmp.Rows(0)
        End If
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
  Public Overridable Function GetAnagra(ByVal strMn_conto As String, ByRef dtrTmp As DataRow) As Boolean
    Dim dttTmp As New DataTable
    Try
      dtrTmp = Nothing
      If Not NTSCInt(strMn_conto) = 0 Then
        If Not oCldGnnp.ValCodiceDb(strMn_conto, strDittaCorrente, "ANAGRA", "N", , dttTmp) Then
          Return False
        Else
          dtrTmp = dttTmp.Rows(0)
        End If
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

  Public Overridable Function AggiornaQtaLottoSuTTMOPERNP(ByVal dtrT As DataRow, ByRef ds As DataSet) As Boolean
    Dim bResult As Boolean = False
    Dim dColli As Decimal = 0
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      bResult = oCldGnnp.AggiornaQtaLottoSuTTMOPERNP(strDittaCorrente, lIITTMoPernp, lGndtLotto, dtrT, ds)
      '--------------------------------------------------------------------------------------------------------------
      If bResult = True Then
        If oCldGnnp.RitornaColliNegativiTTMOPERNP(strDittaCorrente, lIITTMoPernp, dttTmp) = True Then
          For i As Integer = 0 To (dttTmp.Rows.Count - 1)
            With dttTmp.Rows(i)
              CType(oCleComm, CLELBMENU).ConvQuantUM(strDittaCorrente, NTSCStr(!mn_codart), NTSCStr(!mn_ump), _
                NTSCDec(!mn_mmquant), NTSCDec(!mn_misura1), NTSCDec(!mn_misura2), NTSCDec(!mn_misura3), _
                NTSCStr(!mn_unmis), dColli, "", 3)
              !mn_colli = dColli
              !mn_mmcolli = dColli
              !mn_mmcoldisimp = dColli
            End With
            dttTmp.AcceptChanges()
          Next
          '----------------------------------------------------------------------------------------------------------
          oCldGnnp.AggiornaColliNegativiTTMOPERNP(dttTmp)
          '----------------------------------------------------------------------------------------------------------
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return bResult
    Catch ex As Exception
      If GestErrorCallThrow() Then
        Throw New NTSException(GestError(ex, Me, "", oApp.InfoError, "", False))
      Else
        ThrowRemoteEvent(New NTSEventArgs("", GestError(ex, Me, "", oApp.InfoError, "", False)))
      End If
    End Try
  End Function
#End Region

End Class
