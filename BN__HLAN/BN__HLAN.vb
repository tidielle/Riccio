Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__HLAN

  Public oParam As CLE__PATB       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore
  Public oCleHlan As CLE__HLAN
  Public dsHlan As New DataSet
  Public dtcHlan As BindingSource = New BindingSource()
  Public bSemplificata As Boolean = False
  Public strCodPcon As String = ""
  Public bModAnagen As Boolean = False
  Public nFocusIniziale As Integer

  Public strFiltriDaEsterno As String
  Public dttFiltriAnex As New DataTable

  'per accessi CRM
  Public bModuloCRM As Boolean = False
  Public bIsCRMUser As Boolean = False
  Public bAmm As Boolean = False
  Public strAccvis As String = ""
  Public strAccmod As String = ""
  Public strRegvis As String = ""
  Public strRegmod As String = ""
  Public lCodorgaOperat as integer = 0
  Public nCodcageoperat As Integer = 0
  Public bGestAnaExt As Boolean = False
  Public strEstensioni As String = ""    'stringa contenentei filtri impostati tramite le estensioni anagrafiche
  Public strEstensioniRpt As String = ""    'stringa contenentei filtri impostati tramite le estensioni anagrafiche
  Public nTrattaSoloCliFornDellAgente As Integer = 0

  Public bSelectIfOneRow As Boolean = False 'se true, dopo aver lanciato la ricerca se viene restituito un solo record viene subito selezionato
  Public bForm_load As Boolean = True
  Public bEseguitaRicerca As Boolean = False

  Public strProgrChiamante As String = ""

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByVal NomeZoom As String, _
                                 ByRef Param As CLE__PATB, Optional ByVal Ditta As String = "", _
                                 Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean

    oMenu = Menu
    oApp = oMenu.App
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    If Ditta <> "" Then DittaCorrente = Ditta
    oParam = Param
    InitializeComponent()

    Me.MinimumSize = Me.Size


    Me.MinimizeBox = False

    '------------------------------------------------
    'creo e attivo l'entity
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__HLAN", "BE__HLAN", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222142187500, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHlan = CType(oTmp, CLE__HLAN)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__HLTB", strRemoteServer, strRemotePort)
    oCleHlan.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

    oCleHlan.InitHlan(DittaCorrente, bSemplificata, strCodPcon)

    '---------------------------------
    'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    AddHandler oCleHlan.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub CaricaListBox()
    Dim dttPrivacy As New DataTable()
    Dim dttUsaem As New DataTable()
    Dim dttBlocco As New DataTable()
    Dim dttStatus As New DataTable()
    Dim dttFatt As New DataTable()
    Dim dttCons As New DataTable()
    Dim dttPariva As New DataTable()
    Try
      '----------------------------------
      'listbox privacy
      dttPrivacy.Columns.Add("cod", GetType(String))
      dttPrivacy.Columns.Add("val", GetType(String))
      dttPrivacy.Rows.Add(New Object() {"Q", "Qualsiasi"})
      dttPrivacy.Rows.Add(New Object() {" ", "Non definita"})
      dttPrivacy.Rows.Add(New Object() {"S", "Concessa"})
      dttPrivacy.Rows.Add(New Object() {"N", "Non concessa"})
      dttPrivacy.AcceptChanges()

      liPrivacy.DataSource = dttPrivacy
      liPrivacy.DisplayMember = "val"
      liPrivacy.ValueMember = "cod"
      liPrivacy.SelectedValue = "Q"

      '----------------------------------
      'listbox Modalità di spedizione corrispondenza
      dttUsaem.Columns.Add("cod", GetType(String))
      dttUsaem.Columns.Add("val", GetType(String))
      dttUsaem.Rows.Add(New Object() {" ", "Qualsiasi"})
      dttUsaem.Rows.Add(New Object() {"H", "HylaFAX"})
      dttUsaem.Rows.Add(New Object() {"N", "Microsoft Fax"})
      dttUsaem.Rows.Add(New Object() {"S", "E-mail Internet"})
      dttUsaem.Rows.Add(New Object() {"W", "WinFax PRO Symantec"})
      dttUsaem.Rows.Add(New Object() {"X", "Fax service Win XP/2003"})
      dttUsaem.Rows.Add(New Object() {"Y", "Fax service Win 2000 (locale)"})
      dttUsaem.Rows.Add(New Object() {"Z", "Zetafax MAPI"})
      dttUsaem.AcceptChanges()

      liUsaem.DataSource = dttUsaem
      liUsaem.DisplayMember = "val"
      liUsaem.ValueMember = "cod"
      liUsaem.SelectedValue = " "

      '----------------------------------
      'listbox blocco
      dttBlocco.Columns.Add("cod", GetType(String))
      dttBlocco.Columns.Add("val", GetType(String))
      dttBlocco.Rows.Add(New Object() {" ", "Qualsiasi"})
      dttBlocco.Rows.Add(New Object() {"N", "Nessuno"})
      dttBlocco.Rows.Add(New Object() {"B", "Blocco fisso"})
      dttBlocco.Rows.Add(New Object() {"F", "Fuori fido"})
      dttBlocco.Rows.Add(New Object() {"I", "Insoluti"})
      dttBlocco.Rows.Add(New Object() {"R", "RD scadute"})
      dttBlocco.AcceptChanges()

      liBlocco.DataSource = dttBlocco
      liBlocco.DisplayMember = "val"
      liBlocco.ValueMember = "cod"
      liBlocco.SelectedValue = " "

      '----------------------------------
      'listbox status
      dttStatus.Columns.Add("cod", GetType(String))
      dttStatus.Columns.Add("val", GetType(String))
      dttStatus.Rows.Add(New Object() {" ", "Qualsiasi"})
      dttStatus.Rows.Add(New Object() {"A", "Attivo"})
      dttStatus.Rows.Add(New Object() {"I", "Inattivo"})
      dttStatus.Rows.Add(New Object() {"P", "Potenziale"})
      dttStatus.AcceptChanges()

      liStatus.DataSource = dttStatus
      liStatus.DisplayMember = "val"
      liStatus.ValueMember = "cod"
      liStatus.SelectedValue = " "

      '----------------------------------
      'listbox periodo fatturazione
      dttFatt.Columns.Add("cod", GetType(String))
      dttFatt.Columns.Add("val", GetType(String))
      dttFatt.Rows.Add(New Object() {" ", "Qualsiasi"})
      dttFatt.Rows.Add(New Object() {"G", "Giornaliera"})
      dttFatt.Rows.Add(New Object() {"M", "Mensile"})
      dttFatt.Rows.Add(New Object() {"Q", "Quindicinale"})
      dttFatt.Rows.Add(New Object() {"S", "Settimanale"})
      dttFatt.AcceptChanges()

      liFatturaz.DataSource = dttFatt
      liFatturaz.DisplayMember = "val"
      liFatturaz.ValueMember = "cod"
      liFatturaz.SelectedValue = " "

      '----------------------------------
      'listbox giorno di consegna
      dttCons.Columns.Add("cod", GetType(Integer))
      dttCons.Columns.Add("val", GetType(String))
      dttCons.Rows.Add(New Object() {8, "Tutti"})
      dttCons.Rows.Add(New Object() {1, "Lunedì"})
      dttCons.Rows.Add(New Object() {2, "Martedì"})
      dttCons.Rows.Add(New Object() {3, "Mercoledì"})
      dttCons.Rows.Add(New Object() {4, "Giovedì"})
      dttCons.Rows.Add(New Object() {5, "Venerdì"})
      dttCons.Rows.Add(New Object() {6, "Sabato"})
      dttCons.Rows.Add(New Object() {7, "Domenica"})
      dttCons.AcceptChanges()

      liGgcons.DataSource = dttCons
      liGgcons.DisplayMember = "val"
      liGgcons.ValueMember = "cod"
      liGgcons.SelectedValue = "8"

      '----------------------------------
      'listbox partita IVA 
      dttPariva.Columns.Add("cod", GetType(String))
      dttPariva.Columns.Add("val", GetType(String))
      dttPariva.Rows.Add(New Object() {"2", "Tutte"})
      dttPariva.Rows.Add(New Object() {"0", "Si"})
      dttPariva.Rows.Add(New Object() {"1", "No"})
      dttPariva.AcceptChanges()

      liPariva.DataSource = dttPariva
      liPariva.DisplayMember = "val"
      liPariva.ValueMember = "cod"
      liPariva.SelectedValue = "2"

    Catch ex As Exception
      '--------------------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------------------------
    End Try

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttAccperi As New DataTable
    Try
      dttAccperi.Columns.Add("cod", GetType(String))
      dttAccperi.Columns.Add("val", GetType(String))
      dttAccperi.Rows.Add(New Object() {"N", "(Nessuna)"})
      dttAccperi.Rows.Add(New Object() {"S", "Periodo di compet. econ."})
      dttAccperi.Rows.Add(New Object() {"D", "Data compet. Econ."})
      dttAccperi.Rows.Add(New Object() {"I", "Data compet. Iva"})
      dttAccperi.Rows.Add(New Object() {"V", "Data Valuta"})
      dttAccperi.AcceptChanges()


      '-------------------------------------------------
      'completo le informazioni dei i controlli
      edFax.NTSSetParam(oMenu, oApp.Tr(Me, 128230023413386167, "Fax"), 18, True)
      ckSoloSemplificata.NTSSetParam(oMenu, oApp.Tr(Me, 128230023413542340, "Flag 'solo contabilità semplificata'"), "S", "N")
      cbSottc.NTSSetParam(oApp.Tr(Me, 128230023413698513, "Seleziona tipo sottoconto"))
      optSottoconti.NTSSetParam(oMenu, oApp.Tr(Me, 128230023413854686, "Opzione 'sottoconti'"), "S")
      optFornitori.NTSSetParam(oMenu, oApp.Tr(Me, 128230023414010859, "Opzione 'fornitori'"), "S")
      optClienti.NTSSetParam(oMenu, oApp.Tr(Me, 128230023414167032, "Opzione 'clienti'"), "S")
      edTelef.NTSSetParam(oMenu, oApp.Tr(Me, 128230023414323205, "Telefono"), 18, True)
      edEmail.NTSSetParam(oMenu, oApp.Tr(Me, 128230023414479378, "E-mail"), 100, True)
      edComune.NTSSetParam(oMenu, oApp.Tr(Me, 128230023414635551, "Comune"), 28, True)
      edSiglaric.NTSSetParam(oMenu, oApp.Tr(Me, 128230023414791724, "Sigla di ricerca"), 30, True)
      edCodfisc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023414947897, "Codice fiscale"), 16, True)
      edPariva.NTSSetParam(oMenu, oApp.Tr(Me, 128230023415104070, "Partita IVA"), 11, True)
      edDescr2.NTSSetParam(oMenu, oApp.Tr(Me, 128230023415260243, "Ragione sociale 2"), 30, True)
      edDescr.NTSSetParam(oMenu, oApp.Tr(Me, 128230023415416416, "Ragione sociale / Descrizione"), 30, True)
      ckOttimistico.NTSSetParam(oMenu, oApp.Tr(Me, 128230023415572589, "Ottimistico"), "S", "N")
      ckAnagen.NTSSetParam(oMenu, oApp.Tr(Me, 128230023415728762, "Visualizza anche anagrafiche generali"), "S", "N")
      edMastro.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023415884935, "Mastro contabile"), tabmast)
      ckAbituali.NTSSetParam(oMenu, oApp.Tr(Me, 130314004708970856, "Clienti/Fornitori movimenti di magazzino ultimi 6 mesi"), "S", "N")

      edValutaA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023416041108, "Codice valuda A"), tabvalu)
      edValutaDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023416197281, "Codice valuda DA"), tabvalu)
      edDtAggA.NTSSetParam(oMenu, oApp.Tr(Me, 128230023416353454, "Data aggiornamento A"), False)
      edDtAggDa.NTSSetParam(oMenu, oApp.Tr(Me, 128230023416509627, "Data aggiornamento DA"), False)
      edDtAperturaA.NTSSetParam(oMenu, oApp.Tr(Me, 128230023416665800, "Data apertura A"), False)
      edDtAperturaDa.NTSSetParam(oMenu, oApp.Tr(Me, 128230023416821973, "Data apertura DA"), False)
      edListinoA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023416978146, "Listino A"), tablist)
      edListinoDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023417134319, "Listino DA"), tablist)
      edCanaleA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023417290492, "Codice canale A"), tabcana)
      edCanaleDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023417446665, "Codice canale DA"), tabcana)
      edStato.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023417602838, "Codice stato estero"), tabstat, True)
      edRagsocA.NTSSetParam(oMenu, oApp.Tr(Me, 128230023417759011, "Ragione sociale A"), 40, False)
      edRagsocDa.NTSSetParam(oMenu, oApp.Tr(Me, 128230023417915184, "Ragione sociale DA"), 40, True)
      edProvinciaA.NTSSetParam(oMenu, oApp.Tr(Me, 128230023418071357, "Provincia A"), 2, False)
      edProvinciaDa.NTSSetParam(oMenu, oApp.Tr(Me, 128230023418227530, "Provincia DA"), 2, True)
      edPagamDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023418383703, "Codice pagamento DA"), tabpaga)
      edPagamA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023418539876, "Codice pagamento A"), tabpaga)
      edAgenteDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023418696049, "Codice agente DA"), tabcage)
      edAgenteA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023418852222, "Codice agente A"), tabcage)
      edCategDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023419008395, "Codice categoria DA"), tabcate)
      edCategA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023419164568, "Codice categoria A"), tabcate)
      edZonaDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023419320741, "Codice zona DA"), tabzone)
      edZonaA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023419476914, "Codice zona A"), tabzone)
      edCapDa.NTSSetParam(oMenu, oApp.Tr(Me, 128230023419633087, "Codice CAP DA"), 9, True)
      edCapA.NTSSetParam(oMenu, oApp.Tr(Me, 128230023419789260, "codice CAP A"), 9, False)
      edContoA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023419945433, "Codice conto DA"), tabanagrac)
      edContoDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023420101606, "Codice conto A"), tabanagrac)
      edEsenA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023420257779, "Codice esenzione IVA DA"), tabciva)
      edEsenDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023420413952, "Codice esenzione IVA A"), tabciva)
      edLinguaA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023420570125, "Codice lingua DA"), tabling)
      edLinguaDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023420726298, "Codice lingua A"), tabling)
      edSconA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023420882471, "Classe sconto DA"), tabcscl)
      edSconDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023421038644, "Classe sconto A"), tabcscl)
      edProvA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023421194817, "Classe provvigione DA"), tabcpcl)
      edProvDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023421350990, "Classe provvigione A"), tabcpcl)

      liGgcons.NTSSetParam(oApp.Tr(Me, 128230023421507163, "Giorno di consegna"))
      liFatturaz.NTSSetParam(oApp.Tr(Me, 128230023421663336, "Periodicità di fatturazione"))
      liStatus.NTSSetParam(oApp.Tr(Me, 128230023421819509, "Status"))
      liPrivacy.NTSSetParam(oApp.Tr(Me, 128230023421975682, "Privacy"))
      liUsaem.NTSSetParam(oApp.Tr(Me, 129007582278554399, "Modalità spedizione corrispondenza"))
      liBlocco.NTSSetParam(oApp.Tr(Me, 128230023422131855, "Cliente bloccato"))
      liPariva.NTSSetParam(oApp.Tr(Me, 128230023422288028, "Test presenza partita IVA"))

      grvZoom.NTSSetParam(oMenu, oApp.Tr(Me, 128230023422444201, "Griglia Clienti/fornitori/sottoconti"))
      grvZoom.NTSAllowDelete = False
      grvZoom.NTSAllowUpdate = False
      grvZoom.NTSAllowInsert = False
      an_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023422600374, "Codice"), "0", 9)
      an_descr1.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023422756547, "Descrizione"), 50, True)
      an_descr2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023422912720, "Descrizione 2"), 50, True)
      an_citta.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023423068893, "Città"), 50, True)
      an_telef.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023423225066, "Telefono"), 50, True)
      an_faxtlx.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023423381239, "Fax"), 50, True)
      an_pariva.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023423537412, "Partita IVA"), 20, True)
      an_codfis.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023423693585, "Codice Fiscale"), 20, True)
      an_contatt.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023423849758, "Contatto"), 50, True)
      an_flci.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129248615509326172, "Gestione contabilità analitica"), "1", " ")
      an_accperi.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129248615974765625, "Richiedi date"), dttAccperi, "val", "cod")
      grvZoom.Enabled = False

      edMastro.NTSForzaVisZoom = True
      '-------------------------------------------------
      'setto il recent
      ckOttimistico.Checked = CBool(oMenu.GetSettingBus("BN__HLAN", "RECENT", ".", "Ottimistico", "0", " ", "0"))
      ckAnagen.Checked = CBool(oMenu.GetSettingBus("BN__HLAN", "RECENT", ".", "Anagen", "0", " ", "0"))

      'edContoDa.NTSSetParamZoom("")
      'edContoA.NTSSetParamZoom("")

      '-------------------------------------------------
      'gestione delle estensioni anagrafiche
      bGestAnaExt = CBool(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "GestAnaext", "0", " ", "0"))
      If bGestAnaExt = False Then
        cmdEstensioni.Enabled = False
        cmdEstensioni.Visible = False
      End If

      liPariva.SelectionMode = SelectionMode.One

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Sub InitializeComponent()
    Me.pnDescr = New NTSInformatica.NTSPanel
    Me.tsZoom = New NTSInformatica.NTSTabControl
    Me.TabPage1 = New NTSInformatica.NTSTabPage
    Me.pnTab1Pan2 = New NTSInformatica.NTSPanel
    Me.edFax = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel9 = New NTSInformatica.NTSLabel
    Me.edMastro = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel7 = New NTSInformatica.NTSLabel
    Me.NtsLabel6 = New NTSInformatica.NTSLabel
    Me.fmTipo = New NTSInformatica.NTSGroupBox
    Me.ckAbituali = New NTSInformatica.NTSCheckBox
    Me.cbPrivato = New NTSInformatica.NTSComboBox
    Me.cmdReset = New NTSInformatica.NTSButton
    Me.ckSoloSemplificata = New NTSInformatica.NTSCheckBox
    Me.cbSottc = New NTSInformatica.NTSComboBox
    Me.optSottoconti = New NTSInformatica.NTSRadioButton
    Me.optFornitori = New NTSInformatica.NTSRadioButton
    Me.optClienti = New NTSInformatica.NTSRadioButton
    Me.edTelef = New NTSInformatica.NTSTextBoxStr
    Me.pnTab1Pan1 = New NTSInformatica.NTSPanel
    Me.edDescr = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel8 = New NTSInformatica.NTSLabel
    Me.edEmail = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel5 = New NTSInformatica.NTSLabel
    Me.edComune = New NTSInformatica.NTSTextBoxStr
    Me.edSiglaric = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel4 = New NTSInformatica.NTSLabel
    Me.NtsLabel3 = New NTSInformatica.NTSLabel
    Me.edCodfisc = New NTSInformatica.NTSTextBoxStr
    Me.edPariva = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel2 = New NTSInformatica.NTSLabel
    Me.edDescr2 = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.lbDescr = New NTSInformatica.NTSLabel
    Me.TabPage2 = New NTSInformatica.NTSTabPage
    Me.pnTab2Pan2 = New NTSInformatica.NTSPanel
    Me.NtsLabel33 = New NTSInformatica.NTSLabel
    Me.edEsenA = New NTSInformatica.NTSTextBoxNum
    Me.edEsenDa = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel27 = New NTSInformatica.NTSLabel
    Me.edValutaA = New NTSInformatica.NTSTextBoxNum
    Me.edValutaDa = New NTSInformatica.NTSTextBoxNum
    Me.edDtAggA = New NTSInformatica.NTSTextBoxData
    Me.edDtAperturaA = New NTSInformatica.NTSTextBoxData
    Me.edDtAggDa = New NTSInformatica.NTSTextBoxData
    Me.edDtAperturaDa = New NTSInformatica.NTSTextBoxData
    Me.NtsLabel26 = New NTSInformatica.NTSLabel
    Me.NtsLabel25 = New NTSInformatica.NTSLabel
    Me.NtsLabel24 = New NTSInformatica.NTSLabel
    Me.edListinoA = New NTSInformatica.NTSTextBoxNum
    Me.edListinoDa = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel23 = New NTSInformatica.NTSLabel
    Me.edCanaleA = New NTSInformatica.NTSTextBoxNum
    Me.edCanaleDa = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel20 = New NTSInformatica.NTSLabel
    Me.NtsLabel21 = New NTSInformatica.NTSLabel
    Me.edRagsocA = New NTSInformatica.NTSTextBoxStr
    Me.edRagsocDa = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel19 = New NTSInformatica.NTSLabel
    Me.pnTab2Pan1 = New NTSInformatica.NTSPanel
    Me.edProvinciaA = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel18 = New NTSInformatica.NTSLabel
    Me.NtsLabel17 = New NTSInformatica.NTSLabel
    Me.NtsLabel16 = New NTSInformatica.NTSLabel
    Me.edPagamA = New NTSInformatica.NTSTextBoxNum
    Me.edAgenteA = New NTSInformatica.NTSTextBoxNum
    Me.edCategA = New NTSInformatica.NTSTextBoxNum
    Me.edPagamDa = New NTSInformatica.NTSTextBoxNum
    Me.edAgenteDa = New NTSInformatica.NTSTextBoxNum
    Me.edCategDa = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel15 = New NTSInformatica.NTSLabel
    Me.edZonaA = New NTSInformatica.NTSTextBoxNum
    Me.edZonaDa = New NTSInformatica.NTSTextBoxNum
    Me.edCapA = New NTSInformatica.NTSTextBoxStr
    Me.edCapDa = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel14 = New NTSInformatica.NTSLabel
    Me.edProvinciaDa = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel13 = New NTSInformatica.NTSLabel
    Me.edContoA = New NTSInformatica.NTSTextBoxNum
    Me.edContoDa = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel12 = New NTSInformatica.NTSLabel
    Me.NtsLabel11 = New NTSInformatica.NTSLabel
    Me.NtsLabel10 = New NTSInformatica.NTSLabel
    Me.TabPage3 = New NTSInformatica.NTSTabPage
    Me.pnTab3Pan1 = New NTSInformatica.NTSPanel
    Me.lbUsaem = New NTSInformatica.NTSLabel
    Me.liUsaem = New NTSInformatica.NTSListBox
    Me.edStato = New NTSInformatica.NTSTextBoxStr
    Me.NtsLabel22 = New NTSInformatica.NTSLabel
    Me.NtsLabel38 = New NTSInformatica.NTSLabel
    Me.liBlocco = New NTSInformatica.NTSListBox
    Me.NtsLabel32 = New NTSInformatica.NTSLabel
    Me.edLinguaA = New NTSInformatica.NTSTextBoxNum
    Me.edLinguaDa = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel31 = New NTSInformatica.NTSLabel
    Me.edSconA = New NTSInformatica.NTSTextBoxNum
    Me.edSconDa = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel29 = New NTSInformatica.NTSLabel
    Me.NtsLabel30 = New NTSInformatica.NTSLabel
    Me.NtsLabel28 = New NTSInformatica.NTSLabel
    Me.edProvA = New NTSInformatica.NTSTextBoxNum
    Me.edProvDa = New NTSInformatica.NTSTextBoxNum
    Me.pnTab3Pan2 = New NTSInformatica.NTSPanel
    Me.NtsLabel39 = New NTSInformatica.NTSLabel
    Me.NtsLabel37 = New NTSInformatica.NTSLabel
    Me.liPariva = New NTSInformatica.NTSListBox
    Me.NtsLabel36 = New NTSInformatica.NTSLabel
    Me.liGgcons = New NTSInformatica.NTSListBox
    Me.NtsLabel35 = New NTSInformatica.NTSLabel
    Me.liFatturaz = New NTSInformatica.NTSListBox
    Me.liStatus = New NTSInformatica.NTSListBox
    Me.NtsLabel34 = New NTSInformatica.NTSLabel
    Me.liPrivacy = New NTSInformatica.NTSListBox
    Me.pnAction = New NTSInformatica.NTSPanel
    Me.cmdLastfilter = New NTSInformatica.NTSButton
    Me.cmdEstensioni = New NTSInformatica.NTSButton
    Me.ckAnagen = New NTSInformatica.NTSCheckBox
    Me.cmdEscludi = New NTSInformatica.NTSButton
    Me.cmdNuovoPdc = New NTSInformatica.NTSButton
    Me.ckOttimistico = New NTSInformatica.NTSCheckBox
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdGestione = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.grZoom = New NTSInformatica.NTSGrid
    Me.grvZoom = New NTSInformatica.NTSGridView
    Me.an_conto = New NTSInformatica.NTSGridColumn
    Me.an_descr1 = New NTSInformatica.NTSGridColumn
    Me.an_descr2 = New NTSInformatica.NTSGridColumn
    Me.an_citta = New NTSInformatica.NTSGridColumn
    Me.an_telef = New NTSInformatica.NTSGridColumn
    Me.an_faxtlx = New NTSInformatica.NTSGridColumn
    Me.an_pariva = New NTSInformatica.NTSGridColumn
    Me.an_codfis = New NTSInformatica.NTSGridColumn
    Me.an_contatt = New NTSInformatica.NTSGridColumn
    Me.an_flci = New NTSInformatica.NTSGridColumn
    Me.an_accperi = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDescr.SuspendLayout()
    CType(Me.tsZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsZoom.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    CType(Me.pnTab1Pan2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab1Pan2.SuspendLayout()
    CType(Me.edFax.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMastro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmTipo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmTipo.SuspendLayout()
    CType(Me.ckAbituali.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbPrivato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSoloSemplificata.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbSottc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optSottoconti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optFornitori.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optClienti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTelef.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTab1Pan1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab1Pan1.SuspendLayout()
    CType(Me.edDescr.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEmail.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edComune.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSiglaric.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodfisc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPariva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescr2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage2.SuspendLayout()
    CType(Me.pnTab2Pan2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab2Pan2.SuspendLayout()
    CType(Me.edEsenA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edEsenDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edValutaA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edValutaDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDtAggA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDtAperturaA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDtAggDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDtAperturaDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListinoA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListinoDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCanaleA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCanaleDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRagsocA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRagsocDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTab2Pan1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab2Pan1.SuspendLayout()
    CType(Me.edProvinciaA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPagamA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAgenteA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCategA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edPagamDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAgenteDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCategDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edZonaA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edZonaDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCapA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCapDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edProvinciaDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage3.SuspendLayout()
    CType(Me.pnTab3Pan1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab3Pan1.SuspendLayout()
    CType(Me.liUsaem, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edStato.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liBlocco, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLinguaA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edLinguaDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSconA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSconDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edProvA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edProvDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTab3Pan2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab3Pan2.SuspendLayout()
    CType(Me.liPariva, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liGgcons, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liFatturaz, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liStatus, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liPrivacy, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAction.SuspendLayout()
    CType(Me.ckAnagen.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckOttimistico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'frmAuto
    '
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'pnDescr
    '
    Me.pnDescr.AllowDrop = True
    Me.pnDescr.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnDescr.Appearance.Options.UseBackColor = True
    Me.pnDescr.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnDescr.Controls.Add(Me.tsZoom)
    Me.pnDescr.Controls.Add(Me.pnAction)
    Me.pnDescr.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnDescr.Location = New System.Drawing.Point(0, 0)
    Me.pnDescr.Name = "pnDescr"
    Me.pnDescr.NTSActiveTrasparency = True
    Me.pnDescr.Size = New System.Drawing.Size(788, 260)
    Me.pnDescr.TabIndex = 1
    '
    'tsZoom
    '
    Me.tsZoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.tsZoom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsZoom.Location = New System.Drawing.Point(0, 0)
    Me.tsZoom.Margin = New System.Windows.Forms.Padding(0)
    Me.tsZoom.Name = "tsZoom"
    Me.tsZoom.SelectedTabPage = Me.TabPage1
    Me.tsZoom.Size = New System.Drawing.Size(678, 260)
    Me.tsZoom.TabIndex = 4
    Me.tsZoom.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabPage1, Me.TabPage2, Me.TabPage3})
    '
    'TabPage1
    '
    Me.TabPage1.AllowDrop = True
    Me.TabPage1.Controls.Add(Me.pnTab1Pan2)
    Me.TabPage1.Controls.Add(Me.pnTab1Pan1)
    Me.TabPage1.Enable = True
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Size = New System.Drawing.Size(669, 230)
    Me.TabPage1.Text = "&Generale"
    '
    'pnTab1Pan2
    '
    Me.pnTab1Pan2.AllowDrop = True
    Me.pnTab1Pan2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab1Pan2.Appearance.Options.UseBackColor = True
    Me.pnTab1Pan2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab1Pan2.Controls.Add(Me.edFax)
    Me.pnTab1Pan2.Controls.Add(Me.NtsLabel9)
    Me.pnTab1Pan2.Controls.Add(Me.edMastro)
    Me.pnTab1Pan2.Controls.Add(Me.NtsLabel7)
    Me.pnTab1Pan2.Controls.Add(Me.NtsLabel6)
    Me.pnTab1Pan2.Controls.Add(Me.fmTipo)
    Me.pnTab1Pan2.Controls.Add(Me.edTelef)
    Me.pnTab1Pan2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab1Pan2.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnTab1Pan2.Location = New System.Drawing.Point(385, 0)
    Me.pnTab1Pan2.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab1Pan2.Name = "pnTab1Pan2"
    Me.pnTab1Pan2.NTSActiveTrasparency = True
    Me.pnTab1Pan2.Size = New System.Drawing.Size(284, 230)
    Me.pnTab1Pan2.TabIndex = 3
    '
    'edFax
    '
    Me.edFax.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edFax.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFax.Location = New System.Drawing.Point(100, 201)
    Me.edFax.Name = "edFax"
    Me.edFax.NTSDbField = ""
    Me.edFax.NTSForzaVisZoom = False
    Me.edFax.NTSOldValue = ""
    Me.edFax.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edFax.Properties.Appearance.Options.UseBackColor = True
    Me.edFax.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFax.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFax.Properties.AutoHeight = False
    Me.edFax.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFax.Properties.MaxLength = 65536
    Me.edFax.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFax.Size = New System.Drawing.Size(171, 20)
    Me.edFax.TabIndex = 15
    '
    'NtsLabel9
    '
    Me.NtsLabel9.AutoSize = True
    Me.NtsLabel9.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel9.Location = New System.Drawing.Point(17, 152)
    Me.NtsLabel9.Name = "NtsLabel9"
    Me.NtsLabel9.NTSDbField = ""
    Me.NtsLabel9.Size = New System.Drawing.Size(75, 13)
    Me.NtsLabel9.TabIndex = 17
    Me.NtsLabel9.Text = "Codice mastro"
    Me.NtsLabel9.Tooltip = ""
    Me.NtsLabel9.UseMnemonic = False
    '
    'edMastro
    '
    Me.edMastro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMastro.EditValue = "0"
    Me.edMastro.Location = New System.Drawing.Point(203, 149)
    Me.edMastro.Name = "edMastro"
    Me.edMastro.NTSDbField = ""
    Me.edMastro.NTSFormat = "0"
    Me.edMastro.NTSForzaVisZoom = False
    Me.edMastro.NTSOldValue = ""
    Me.edMastro.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edMastro.Properties.Appearance.Options.UseBackColor = True
    Me.edMastro.Properties.Appearance.Options.UseTextOptions = True
    Me.edMastro.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMastro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMastro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMastro.Properties.AutoHeight = False
    Me.edMastro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMastro.Properties.MaxLength = 65536
    Me.edMastro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMastro.Size = New System.Drawing.Size(68, 20)
    Me.edMastro.TabIndex = 16
    '
    'NtsLabel7
    '
    Me.NtsLabel7.AutoSize = True
    Me.NtsLabel7.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel7.Location = New System.Drawing.Point(17, 204)
    Me.NtsLabel7.Name = "NtsLabel7"
    Me.NtsLabel7.NTSDbField = ""
    Me.NtsLabel7.Size = New System.Drawing.Size(25, 13)
    Me.NtsLabel7.TabIndex = 14
    Me.NtsLabel7.Text = "Fax"
    Me.NtsLabel7.Tooltip = ""
    Me.NtsLabel7.UseMnemonic = False
    '
    'NtsLabel6
    '
    Me.NtsLabel6.AutoSize = True
    Me.NtsLabel6.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel6.Location = New System.Drawing.Point(17, 178)
    Me.NtsLabel6.Name = "NtsLabel6"
    Me.NtsLabel6.NTSDbField = ""
    Me.NtsLabel6.Size = New System.Drawing.Size(49, 13)
    Me.NtsLabel6.TabIndex = 13
    Me.NtsLabel6.Text = "Telefono"
    Me.NtsLabel6.Tooltip = ""
    Me.NtsLabel6.UseMnemonic = False
    '
    'fmTipo
    '
    Me.fmTipo.AllowDrop = True
    Me.fmTipo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmTipo.Appearance.Options.UseBackColor = True
    Me.fmTipo.Controls.Add(Me.ckAbituali)
    Me.fmTipo.Controls.Add(Me.cbPrivato)
    Me.fmTipo.Controls.Add(Me.cmdReset)
    Me.fmTipo.Controls.Add(Me.ckSoloSemplificata)
    Me.fmTipo.Controls.Add(Me.cbSottc)
    Me.fmTipo.Controls.Add(Me.optSottoconti)
    Me.fmTipo.Controls.Add(Me.optFornitori)
    Me.fmTipo.Controls.Add(Me.optClienti)
    Me.fmTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmTipo.Location = New System.Drawing.Point(3, 3)
    Me.fmTipo.Name = "fmTipo"
    Me.fmTipo.Size = New System.Drawing.Size(278, 112)
    Me.fmTipo.TabIndex = 0
    Me.fmTipo.Text = "Tipo"
    '
    'ckAbituali
    '
    Me.ckAbituali.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAbituali.Location = New System.Drawing.Point(99, 89)
    Me.ckAbituali.Name = "ckAbituali"
    Me.ckAbituali.NTSCheckValue = "S"
    Me.ckAbituali.NTSUnCheckValue = "N"
    Me.ckAbituali.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAbituali.Properties.Appearance.Options.UseBackColor = True
    Me.ckAbituali.Properties.AutoHeight = False
    Me.ckAbituali.Properties.Caption = "C/F movim.magaz.ultimi 6 mesi"
    Me.ckAbituali.Size = New System.Drawing.Size(174, 19)
    Me.ckAbituali.TabIndex = 8
    '
    'cbPrivato
    '
    Me.cbPrivato.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbPrivato.DataSource = Nothing
    Me.cbPrivato.DisplayMember = ""
    Me.cbPrivato.Location = New System.Drawing.Point(97, 23)
    Me.cbPrivato.Name = "cbPrivato"
    Me.cbPrivato.NTSDbField = ""
    Me.cbPrivato.Properties.AutoHeight = False
    Me.cbPrivato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbPrivato.Properties.DropDownRows = 30
    Me.cbPrivato.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbPrivato.SelectedValue = ""
    Me.cbPrivato.Size = New System.Drawing.Size(171, 20)
    Me.cbPrivato.TabIndex = 7
    Me.cbPrivato.ValueMember = ""
    '
    'cmdReset
    '
    Me.cmdReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdReset.ImagePath = ""
    Me.cmdReset.ImageText = ""
    Me.cmdReset.Location = New System.Drawing.Point(14, 88)
    Me.cmdReset.Name = "cmdReset"
    Me.cmdReset.NTSContextMenu = Nothing
    Me.cmdReset.Size = New System.Drawing.Size(79, 20)
    Me.cmdReset.TabIndex = 6
    Me.cmdReset.Text = "Togli check"
    '
    'ckSoloSemplificata
    '
    Me.ckSoloSemplificata.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSoloSemplificata.Location = New System.Drawing.Point(101, 110)
    Me.ckSoloSemplificata.Name = "ckSoloSemplificata"
    Me.ckSoloSemplificata.NTSCheckValue = "S"
    Me.ckSoloSemplificata.NTSUnCheckValue = "N"
    Me.ckSoloSemplificata.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSoloSemplificata.Properties.Appearance.Options.UseBackColor = True
    Me.ckSoloSemplificata.Properties.AutoHeight = False
    Me.ckSoloSemplificata.Properties.Caption = "Solo sottoconti semplificata"
    Me.ckSoloSemplificata.Size = New System.Drawing.Size(154, 19)
    Me.ckSoloSemplificata.TabIndex = 4
    '
    'cbSottc
    '
    Me.cbSottc.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbSottc.DataSource = Nothing
    Me.cbSottc.DisplayMember = ""
    Me.cbSottc.Location = New System.Drawing.Point(97, 68)
    Me.cbSottc.Name = "cbSottc"
    Me.cbSottc.NTSDbField = ""
    Me.cbSottc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbSottc.Properties.Appearance.Options.UseBackColor = True
    Me.cbSottc.Properties.AutoHeight = False
    Me.cbSottc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbSottc.Properties.DropDownRows = 30
    Me.cbSottc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbSottc.SelectedValue = ""
    Me.cbSottc.Size = New System.Drawing.Size(171, 20)
    Me.cbSottc.TabIndex = 3
    Me.cbSottc.ValueMember = ""
    '
    'optSottoconti
    '
    Me.optSottoconti.Cursor = System.Windows.Forms.Cursors.Default
    Me.optSottoconti.Location = New System.Drawing.Point(17, 68)
    Me.optSottoconti.Name = "optSottoconti"
    Me.optSottoconti.NTSCheckValue = "S"
    Me.optSottoconti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optSottoconti.Properties.Appearance.Options.UseBackColor = True
    Me.optSottoconti.Properties.AutoHeight = False
    Me.optSottoconti.Properties.Caption = "&Sottoconti"
    Me.optSottoconti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optSottoconti.Size = New System.Drawing.Size(74, 19)
    Me.optSottoconti.TabIndex = 2
    '
    'optFornitori
    '
    Me.optFornitori.Cursor = System.Windows.Forms.Cursors.Default
    Me.optFornitori.Location = New System.Drawing.Point(17, 45)
    Me.optFornitori.Name = "optFornitori"
    Me.optFornitori.NTSCheckValue = "S"
    Me.optFornitori.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optFornitori.Properties.Appearance.Options.UseBackColor = True
    Me.optFornitori.Properties.AutoHeight = False
    Me.optFornitori.Properties.Caption = "&Fornitori"
    Me.optFornitori.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optFornitori.Size = New System.Drawing.Size(65, 19)
    Me.optFornitori.TabIndex = 1
    '
    'optClienti
    '
    Me.optClienti.Cursor = System.Windows.Forms.Cursors.Default
    Me.optClienti.EditValue = True
    Me.optClienti.Location = New System.Drawing.Point(17, 22)
    Me.optClienti.Name = "optClienti"
    Me.optClienti.NTSCheckValue = "S"
    Me.optClienti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optClienti.Properties.Appearance.Options.UseBackColor = True
    Me.optClienti.Properties.AutoHeight = False
    Me.optClienti.Properties.Caption = "&Clienti"
    Me.optClienti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optClienti.Size = New System.Drawing.Size(54, 19)
    Me.optClienti.TabIndex = 0
    '
    'edTelef
    '
    Me.edTelef.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTelef.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTelef.Location = New System.Drawing.Point(100, 175)
    Me.edTelef.Name = "edTelef"
    Me.edTelef.NTSDbField = ""
    Me.edTelef.NTSForzaVisZoom = False
    Me.edTelef.NTSOldValue = ""
    Me.edTelef.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edTelef.Properties.Appearance.Options.UseBackColor = True
    Me.edTelef.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTelef.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTelef.Properties.AutoHeight = False
    Me.edTelef.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTelef.Properties.MaxLength = 65536
    Me.edTelef.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTelef.Size = New System.Drawing.Size(171, 20)
    Me.edTelef.TabIndex = 12
    '
    'pnTab1Pan1
    '
    Me.pnTab1Pan1.AllowDrop = True
    Me.pnTab1Pan1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.pnTab1Pan1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab1Pan1.Appearance.Options.UseBackColor = True
    Me.pnTab1Pan1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab1Pan1.Controls.Add(Me.edDescr)
    Me.pnTab1Pan1.Controls.Add(Me.NtsLabel8)
    Me.pnTab1Pan1.Controls.Add(Me.edEmail)
    Me.pnTab1Pan1.Controls.Add(Me.NtsLabel5)
    Me.pnTab1Pan1.Controls.Add(Me.edComune)
    Me.pnTab1Pan1.Controls.Add(Me.edSiglaric)
    Me.pnTab1Pan1.Controls.Add(Me.NtsLabel4)
    Me.pnTab1Pan1.Controls.Add(Me.NtsLabel3)
    Me.pnTab1Pan1.Controls.Add(Me.edCodfisc)
    Me.pnTab1Pan1.Controls.Add(Me.edPariva)
    Me.pnTab1Pan1.Controls.Add(Me.NtsLabel2)
    Me.pnTab1Pan1.Controls.Add(Me.edDescr2)
    Me.pnTab1Pan1.Controls.Add(Me.NtsLabel1)
    Me.pnTab1Pan1.Controls.Add(Me.lbDescr)
    Me.pnTab1Pan1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab1Pan1.Location = New System.Drawing.Point(4, 0)
    Me.pnTab1Pan1.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab1Pan1.Name = "pnTab1Pan1"
    Me.pnTab1Pan1.NTSActiveTrasparency = True
    Me.pnTab1Pan1.Size = New System.Drawing.Size(372, 231)
    Me.pnTab1Pan1.TabIndex = 2
    '
    'edDescr
    '
    Me.edDescr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edDescr.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDescr.Location = New System.Drawing.Point(89, 8)
    Me.edDescr.Name = "edDescr"
    Me.edDescr.NTSDbField = ""
    Me.edDescr.NTSForzaVisZoom = False
    Me.edDescr.NTSOldValue = ""
    Me.edDescr.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDescr.Properties.Appearance.Options.UseBackColor = True
    Me.edDescr.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr.Properties.AutoHeight = False
    Me.edDescr.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescr.Properties.MaxLength = 65536
    Me.edDescr.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr.Size = New System.Drawing.Size(273, 20)
    Me.edDescr.TabIndex = 14
    '
    'NtsLabel8
    '
    Me.NtsLabel8.AutoSize = True
    Me.NtsLabel8.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel8.Location = New System.Drawing.Point(4, 155)
    Me.NtsLabel8.Name = "NtsLabel8"
    Me.NtsLabel8.NTSDbField = ""
    Me.NtsLabel8.Size = New System.Drawing.Size(35, 13)
    Me.NtsLabel8.TabIndex = 13
    Me.NtsLabel8.Text = "E-mail"
    Me.NtsLabel8.Tooltip = ""
    Me.NtsLabel8.UseMnemonic = False
    '
    'edEmail
    '
    Me.edEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edEmail.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEmail.Location = New System.Drawing.Point(89, 152)
    Me.edEmail.Name = "edEmail"
    Me.edEmail.NTSDbField = ""
    Me.edEmail.NTSForzaVisZoom = False
    Me.edEmail.NTSOldValue = ""
    Me.edEmail.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEmail.Properties.Appearance.Options.UseBackColor = True
    Me.edEmail.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEmail.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEmail.Properties.AutoHeight = False
    Me.edEmail.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEmail.Properties.MaxLength = 65536
    Me.edEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEmail.Size = New System.Drawing.Size(273, 20)
    Me.edEmail.TabIndex = 12
    '
    'NtsLabel5
    '
    Me.NtsLabel5.AutoSize = True
    Me.NtsLabel5.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel5.Location = New System.Drawing.Point(2, 207)
    Me.NtsLabel5.Name = "NtsLabel5"
    Me.NtsLabel5.NTSDbField = ""
    Me.NtsLabel5.Size = New System.Drawing.Size(79, 13)
    Me.NtsLabel5.TabIndex = 11
    Me.NtsLabel5.Text = "Città / Comune"
    Me.NtsLabel5.Tooltip = ""
    Me.NtsLabel5.UseMnemonic = False
    '
    'edComune
    '
    Me.edComune.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edComune.Cursor = System.Windows.Forms.Cursors.Default
    Me.edComune.Location = New System.Drawing.Point(89, 204)
    Me.edComune.Name = "edComune"
    Me.edComune.NTSDbField = ""
    Me.edComune.NTSForzaVisZoom = False
    Me.edComune.NTSOldValue = ""
    Me.edComune.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edComune.Properties.Appearance.Options.UseBackColor = True
    Me.edComune.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edComune.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edComune.Properties.AutoHeight = False
    Me.edComune.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edComune.Properties.MaxLength = 65536
    Me.edComune.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edComune.Size = New System.Drawing.Size(138, 20)
    Me.edComune.TabIndex = 10
    '
    'edSiglaric
    '
    Me.edSiglaric.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edSiglaric.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSiglaric.Location = New System.Drawing.Point(89, 34)
    Me.edSiglaric.Name = "edSiglaric"
    Me.edSiglaric.NTSDbField = ""
    Me.edSiglaric.NTSForzaVisZoom = False
    Me.edSiglaric.NTSOldValue = ""
    Me.edSiglaric.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edSiglaric.Properties.Appearance.Options.UseBackColor = True
    Me.edSiglaric.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSiglaric.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSiglaric.Properties.AutoHeight = False
    Me.edSiglaric.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSiglaric.Properties.MaxLength = 65536
    Me.edSiglaric.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSiglaric.Size = New System.Drawing.Size(273, 20)
    Me.edSiglaric.TabIndex = 9
    '
    'NtsLabel4
    '
    Me.NtsLabel4.AutoSize = True
    Me.NtsLabel4.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel4.Location = New System.Drawing.Point(2, 181)
    Me.NtsLabel4.Name = "NtsLabel4"
    Me.NtsLabel4.NTSDbField = ""
    Me.NtsLabel4.Size = New System.Drawing.Size(74, 13)
    Me.NtsLabel4.TabIndex = 8
    Me.NtsLabel4.Text = "Rag. sociale 2"
    Me.NtsLabel4.Tooltip = ""
    Me.NtsLabel4.UseMnemonic = False
    '
    'NtsLabel3
    '
    Me.NtsLabel3.AutoSize = True
    Me.NtsLabel3.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel3.Location = New System.Drawing.Point(4, 88)
    Me.NtsLabel3.Name = "NtsLabel3"
    Me.NtsLabel3.NTSDbField = ""
    Me.NtsLabel3.Size = New System.Drawing.Size(72, 13)
    Me.NtsLabel3.TabIndex = 7
    Me.NtsLabel3.Text = "Codice fiscale"
    Me.NtsLabel3.Tooltip = ""
    Me.NtsLabel3.UseMnemonic = False
    '
    'edCodfisc
    '
    Me.edCodfisc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edCodfisc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodfisc.Location = New System.Drawing.Point(89, 86)
    Me.edCodfisc.Name = "edCodfisc"
    Me.edCodfisc.NTSDbField = ""
    Me.edCodfisc.NTSForzaVisZoom = False
    Me.edCodfisc.NTSOldValue = ""
    Me.edCodfisc.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodfisc.Properties.Appearance.Options.UseBackColor = True
    Me.edCodfisc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodfisc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodfisc.Properties.AutoHeight = False
    Me.edCodfisc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodfisc.Properties.MaxLength = 65536
    Me.edCodfisc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodfisc.Size = New System.Drawing.Size(138, 20)
    Me.edCodfisc.TabIndex = 6
    '
    'edPariva
    '
    Me.edPariva.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edPariva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPariva.Location = New System.Drawing.Point(89, 60)
    Me.edPariva.Name = "edPariva"
    Me.edPariva.NTSDbField = ""
    Me.edPariva.NTSForzaVisZoom = False
    Me.edPariva.NTSOldValue = ""
    Me.edPariva.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edPariva.Properties.Appearance.Options.UseBackColor = True
    Me.edPariva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPariva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPariva.Properties.AutoHeight = False
    Me.edPariva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPariva.Properties.MaxLength = 65536
    Me.edPariva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPariva.Size = New System.Drawing.Size(138, 20)
    Me.edPariva.TabIndex = 5
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(4, 63)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(59, 13)
    Me.NtsLabel2.TabIndex = 4
    Me.NtsLabel2.Text = "Partita IVA"
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'edDescr2
    '
    Me.edDescr2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edDescr2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDescr2.Location = New System.Drawing.Point(89, 178)
    Me.edDescr2.Name = "edDescr2"
    Me.edDescr2.NTSDbField = ""
    Me.edDescr2.NTSForzaVisZoom = False
    Me.edDescr2.NTSOldValue = ""
    Me.edDescr2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDescr2.Properties.Appearance.Options.UseBackColor = True
    Me.edDescr2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescr2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescr2.Properties.AutoHeight = False
    Me.edDescr2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescr2.Properties.MaxLength = 65536
    Me.edDescr2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescr2.Size = New System.Drawing.Size(273, 20)
    Me.edDescr2.TabIndex = 3
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(4, 37)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(75, 13)
    Me.NtsLabel1.TabIndex = 2
    Me.NtsLabel1.Text = "Sigla di ricerca"
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'lbDescr
    '
    Me.lbDescr.AutoSize = True
    Me.lbDescr.BackColor = System.Drawing.Color.Transparent
    Me.lbDescr.Location = New System.Drawing.Point(5, 11)
    Me.lbDescr.Name = "lbDescr"
    Me.lbDescr.NTSDbField = ""
    Me.lbDescr.Size = New System.Drawing.Size(88, 13)
    Me.lbDescr.TabIndex = 1
    Me.lbDescr.Text = "Rag. soc./Descr."
    Me.lbDescr.Tooltip = ""
    Me.lbDescr.UseMnemonic = False
    '
    'TabPage2
    '
    Me.TabPage2.AllowDrop = True
    Me.TabPage2.Controls.Add(Me.pnTab2Pan2)
    Me.TabPage2.Controls.Add(Me.pnTab2Pan1)
    Me.TabPage2.Enable = True
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Size = New System.Drawing.Size(669, 230)
    Me.TabPage2.Text = "Filtri &1"
    '
    'pnTab2Pan2
    '
    Me.pnTab2Pan2.AllowDrop = True
    Me.pnTab2Pan2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab2Pan2.Appearance.Options.UseBackColor = True
    Me.pnTab2Pan2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab2Pan2.Controls.Add(Me.NtsLabel33)
    Me.pnTab2Pan2.Controls.Add(Me.edEsenA)
    Me.pnTab2Pan2.Controls.Add(Me.edEsenDa)
    Me.pnTab2Pan2.Controls.Add(Me.NtsLabel27)
    Me.pnTab2Pan2.Controls.Add(Me.edValutaA)
    Me.pnTab2Pan2.Controls.Add(Me.edValutaDa)
    Me.pnTab2Pan2.Controls.Add(Me.edDtAggA)
    Me.pnTab2Pan2.Controls.Add(Me.edDtAperturaA)
    Me.pnTab2Pan2.Controls.Add(Me.edDtAggDa)
    Me.pnTab2Pan2.Controls.Add(Me.edDtAperturaDa)
    Me.pnTab2Pan2.Controls.Add(Me.NtsLabel26)
    Me.pnTab2Pan2.Controls.Add(Me.NtsLabel25)
    Me.pnTab2Pan2.Controls.Add(Me.NtsLabel24)
    Me.pnTab2Pan2.Controls.Add(Me.edListinoA)
    Me.pnTab2Pan2.Controls.Add(Me.edListinoDa)
    Me.pnTab2Pan2.Controls.Add(Me.NtsLabel23)
    Me.pnTab2Pan2.Controls.Add(Me.edCanaleA)
    Me.pnTab2Pan2.Controls.Add(Me.edCanaleDa)
    Me.pnTab2Pan2.Controls.Add(Me.NtsLabel20)
    Me.pnTab2Pan2.Controls.Add(Me.NtsLabel21)
    Me.pnTab2Pan2.Controls.Add(Me.edRagsocA)
    Me.pnTab2Pan2.Controls.Add(Me.edRagsocDa)
    Me.pnTab2Pan2.Controls.Add(Me.NtsLabel19)
    Me.pnTab2Pan2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab2Pan2.Location = New System.Drawing.Point(338, 3)
    Me.pnTab2Pan2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTab2Pan2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTab2Pan2.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab2Pan2.Name = "pnTab2Pan2"
    Me.pnTab2Pan2.NTSActiveTrasparency = True
    Me.pnTab2Pan2.Size = New System.Drawing.Size(329, 228)
    Me.pnTab2Pan2.TabIndex = 4
    '
    'NtsLabel33
    '
    Me.NtsLabel33.AutoSize = True
    Me.NtsLabel33.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel33.Location = New System.Drawing.Point(14, 62)
    Me.NtsLabel33.Name = "NtsLabel33"
    Me.NtsLabel33.NTSDbField = ""
    Me.NtsLabel33.Size = New System.Drawing.Size(55, 13)
    Me.NtsLabel33.TabIndex = 50
    Me.NtsLabel33.Text = "Esenzione"
    Me.NtsLabel33.Tooltip = ""
    Me.NtsLabel33.UseMnemonic = False
    '
    'edEsenA
    '
    Me.edEsenA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEsenA.EditValue = "9999"
    Me.edEsenA.Location = New System.Drawing.Point(213, 59)
    Me.edEsenA.Name = "edEsenA"
    Me.edEsenA.NTSDbField = ""
    Me.edEsenA.NTSFormat = "0"
    Me.edEsenA.NTSForzaVisZoom = False
    Me.edEsenA.NTSOldValue = "9999"
    Me.edEsenA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEsenA.Properties.Appearance.Options.UseBackColor = True
    Me.edEsenA.Properties.Appearance.Options.UseTextOptions = True
    Me.edEsenA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEsenA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEsenA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEsenA.Properties.AutoHeight = False
    Me.edEsenA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEsenA.Properties.MaxLength = 65536
    Me.edEsenA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEsenA.Size = New System.Drawing.Size(61, 20)
    Me.edEsenA.TabIndex = 49
    '
    'edEsenDa
    '
    Me.edEsenDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edEsenDa.EditValue = "0"
    Me.edEsenDa.Location = New System.Drawing.Point(93, 59)
    Me.edEsenDa.Name = "edEsenDa"
    Me.edEsenDa.NTSDbField = ""
    Me.edEsenDa.NTSFormat = "0"
    Me.edEsenDa.NTSForzaVisZoom = False
    Me.edEsenDa.NTSOldValue = ""
    Me.edEsenDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edEsenDa.Properties.Appearance.Options.UseBackColor = True
    Me.edEsenDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edEsenDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edEsenDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edEsenDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edEsenDa.Properties.AutoHeight = False
    Me.edEsenDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edEsenDa.Properties.MaxLength = 65536
    Me.edEsenDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edEsenDa.Size = New System.Drawing.Size(60, 20)
    Me.edEsenDa.TabIndex = 48
    '
    'NtsLabel27
    '
    Me.NtsLabel27.AutoSize = True
    Me.NtsLabel27.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel27.Location = New System.Drawing.Point(14, 138)
    Me.NtsLabel27.Name = "NtsLabel27"
    Me.NtsLabel27.NTSDbField = ""
    Me.NtsLabel27.Size = New System.Drawing.Size(37, 13)
    Me.NtsLabel27.TabIndex = 33
    Me.NtsLabel27.Text = "Valuta"
    Me.NtsLabel27.Tooltip = ""
    Me.NtsLabel27.UseMnemonic = False
    '
    'edValutaA
    '
    Me.edValutaA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edValutaA.EditValue = "999"
    Me.edValutaA.Location = New System.Drawing.Point(214, 135)
    Me.edValutaA.Name = "edValutaA"
    Me.edValutaA.NTSDbField = ""
    Me.edValutaA.NTSFormat = "0"
    Me.edValutaA.NTSForzaVisZoom = False
    Me.edValutaA.NTSOldValue = "999"
    Me.edValutaA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edValutaA.Properties.Appearance.Options.UseBackColor = True
    Me.edValutaA.Properties.Appearance.Options.UseTextOptions = True
    Me.edValutaA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edValutaA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edValutaA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edValutaA.Properties.AutoHeight = False
    Me.edValutaA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edValutaA.Properties.MaxLength = 65536
    Me.edValutaA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edValutaA.Size = New System.Drawing.Size(60, 20)
    Me.edValutaA.TabIndex = 32
    '
    'edValutaDa
    '
    Me.edValutaDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edValutaDa.EditValue = "0"
    Me.edValutaDa.Location = New System.Drawing.Point(93, 135)
    Me.edValutaDa.Name = "edValutaDa"
    Me.edValutaDa.NTSDbField = ""
    Me.edValutaDa.NTSFormat = "0"
    Me.edValutaDa.NTSForzaVisZoom = False
    Me.edValutaDa.NTSOldValue = ""
    Me.edValutaDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edValutaDa.Properties.Appearance.Options.UseBackColor = True
    Me.edValutaDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edValutaDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edValutaDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edValutaDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edValutaDa.Properties.AutoHeight = False
    Me.edValutaDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edValutaDa.Properties.MaxLength = 65536
    Me.edValutaDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edValutaDa.Size = New System.Drawing.Size(60, 20)
    Me.edValutaDa.TabIndex = 31
    '
    'edDtAggA
    '
    Me.edDtAggA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtAggA.EditValue = "31/12/2099"
    Me.edDtAggA.Location = New System.Drawing.Point(214, 186)
    Me.edDtAggA.Name = "edDtAggA"
    Me.edDtAggA.NTSDbField = ""
    Me.edDtAggA.NTSForzaVisZoom = False
    Me.edDtAggA.NTSOldValue = ""
    Me.edDtAggA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDtAggA.Properties.Appearance.Options.UseBackColor = True
    Me.edDtAggA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtAggA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDtAggA.Properties.AutoHeight = False
    Me.edDtAggA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtAggA.Properties.MaxLength = 65536
    Me.edDtAggA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtAggA.Size = New System.Drawing.Size(97, 20)
    Me.edDtAggA.TabIndex = 30
    '
    'edDtAperturaA
    '
    Me.edDtAperturaA.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edDtAperturaA.EditValue = "31/12/2099"
    Me.edDtAperturaA.Location = New System.Drawing.Point(214, 160)
    Me.edDtAperturaA.Name = "edDtAperturaA"
    Me.edDtAperturaA.NTSDbField = ""
    Me.edDtAperturaA.NTSForzaVisZoom = False
    Me.edDtAperturaA.NTSOldValue = ""
    Me.edDtAperturaA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDtAperturaA.Properties.Appearance.Options.UseBackColor = True
    Me.edDtAperturaA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtAperturaA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDtAperturaA.Properties.AutoHeight = False
    Me.edDtAperturaA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtAperturaA.Properties.MaxLength = 65536
    Me.edDtAperturaA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtAperturaA.Size = New System.Drawing.Size(97, 20)
    Me.edDtAperturaA.TabIndex = 29
    '
    'edDtAggDa
    '
    Me.edDtAggDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtAggDa.EditValue = "01/01/1900"
    Me.edDtAggDa.Location = New System.Drawing.Point(93, 186)
    Me.edDtAggDa.Name = "edDtAggDa"
    Me.edDtAggDa.NTSDbField = ""
    Me.edDtAggDa.NTSForzaVisZoom = False
    Me.edDtAggDa.NTSOldValue = ""
    Me.edDtAggDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDtAggDa.Properties.Appearance.Options.UseBackColor = True
    Me.edDtAggDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtAggDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDtAggDa.Properties.AutoHeight = False
    Me.edDtAggDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtAggDa.Properties.MaxLength = 65536
    Me.edDtAggDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtAggDa.Size = New System.Drawing.Size(97, 20)
    Me.edDtAggDa.TabIndex = 28
    '
    'edDtAperturaDa
    '
    Me.edDtAperturaDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtAperturaDa.EditValue = "01/01/1900"
    Me.edDtAperturaDa.Location = New System.Drawing.Point(93, 160)
    Me.edDtAperturaDa.Name = "edDtAperturaDa"
    Me.edDtAperturaDa.NTSDbField = ""
    Me.edDtAperturaDa.NTSForzaVisZoom = False
    Me.edDtAperturaDa.NTSOldValue = ""
    Me.edDtAperturaDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDtAperturaDa.Properties.Appearance.Options.UseBackColor = True
    Me.edDtAperturaDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtAperturaDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDtAperturaDa.Properties.AutoHeight = False
    Me.edDtAperturaDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtAperturaDa.Properties.MaxLength = 65536
    Me.edDtAperturaDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtAperturaDa.Size = New System.Drawing.Size(97, 20)
    Me.edDtAperturaDa.TabIndex = 27
    '
    'NtsLabel26
    '
    Me.NtsLabel26.AutoSize = True
    Me.NtsLabel26.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel26.Location = New System.Drawing.Point(14, 189)
    Me.NtsLabel26.Name = "NtsLabel26"
    Me.NtsLabel26.NTSDbField = ""
    Me.NtsLabel26.Size = New System.Drawing.Size(73, 13)
    Me.NtsLabel26.TabIndex = 26
    Me.NtsLabel26.Text = "Data aggiorn."
    Me.NtsLabel26.Tooltip = ""
    Me.NtsLabel26.UseMnemonic = False
    '
    'NtsLabel25
    '
    Me.NtsLabel25.AutoSize = True
    Me.NtsLabel25.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel25.Location = New System.Drawing.Point(14, 163)
    Me.NtsLabel25.Name = "NtsLabel25"
    Me.NtsLabel25.NTSDbField = ""
    Me.NtsLabel25.Size = New System.Drawing.Size(75, 13)
    Me.NtsLabel25.TabIndex = 25
    Me.NtsLabel25.Text = "Data apertura"
    Me.NtsLabel25.Tooltip = ""
    Me.NtsLabel25.UseMnemonic = False
    '
    'NtsLabel24
    '
    Me.NtsLabel24.AutoSize = True
    Me.NtsLabel24.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel24.Location = New System.Drawing.Point(14, 112)
    Me.NtsLabel24.Name = "NtsLabel24"
    Me.NtsLabel24.NTSDbField = ""
    Me.NtsLabel24.Size = New System.Drawing.Size(37, 13)
    Me.NtsLabel24.TabIndex = 24
    Me.NtsLabel24.Text = "Listino"
    Me.NtsLabel24.Tooltip = ""
    Me.NtsLabel24.UseMnemonic = False
    '
    'edListinoA
    '
    Me.edListinoA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListinoA.EditValue = "9999"
    Me.edListinoA.Location = New System.Drawing.Point(214, 109)
    Me.edListinoA.Name = "edListinoA"
    Me.edListinoA.NTSDbField = ""
    Me.edListinoA.NTSFormat = "0"
    Me.edListinoA.NTSForzaVisZoom = False
    Me.edListinoA.NTSOldValue = "9999"
    Me.edListinoA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edListinoA.Properties.Appearance.Options.UseBackColor = True
    Me.edListinoA.Properties.Appearance.Options.UseTextOptions = True
    Me.edListinoA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListinoA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListinoA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListinoA.Properties.AutoHeight = False
    Me.edListinoA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListinoA.Properties.MaxLength = 65536
    Me.edListinoA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListinoA.Size = New System.Drawing.Size(60, 20)
    Me.edListinoA.TabIndex = 23
    '
    'edListinoDa
    '
    Me.edListinoDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListinoDa.EditValue = "-2"
    Me.edListinoDa.Location = New System.Drawing.Point(93, 109)
    Me.edListinoDa.Name = "edListinoDa"
    Me.edListinoDa.NTSDbField = ""
    Me.edListinoDa.NTSFormat = "0"
    Me.edListinoDa.NTSForzaVisZoom = False
    Me.edListinoDa.NTSOldValue = "-2"
    Me.edListinoDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edListinoDa.Properties.Appearance.Options.UseBackColor = True
    Me.edListinoDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edListinoDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListinoDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListinoDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListinoDa.Properties.AutoHeight = False
    Me.edListinoDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListinoDa.Properties.MaxLength = 65536
    Me.edListinoDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListinoDa.Size = New System.Drawing.Size(60, 20)
    Me.edListinoDa.TabIndex = 22
    '
    'NtsLabel23
    '
    Me.NtsLabel23.AutoSize = True
    Me.NtsLabel23.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel23.Location = New System.Drawing.Point(14, 86)
    Me.NtsLabel23.Name = "NtsLabel23"
    Me.NtsLabel23.NTSDbField = ""
    Me.NtsLabel23.Size = New System.Drawing.Size(40, 13)
    Me.NtsLabel23.TabIndex = 21
    Me.NtsLabel23.Text = "Canale"
    Me.NtsLabel23.Tooltip = ""
    Me.NtsLabel23.UseMnemonic = False
    '
    'edCanaleA
    '
    Me.edCanaleA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCanaleA.EditValue = "999"
    Me.edCanaleA.Location = New System.Drawing.Point(214, 83)
    Me.edCanaleA.Name = "edCanaleA"
    Me.edCanaleA.NTSDbField = ""
    Me.edCanaleA.NTSFormat = "0"
    Me.edCanaleA.NTSForzaVisZoom = False
    Me.edCanaleA.NTSOldValue = "999"
    Me.edCanaleA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCanaleA.Properties.Appearance.Options.UseBackColor = True
    Me.edCanaleA.Properties.Appearance.Options.UseTextOptions = True
    Me.edCanaleA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCanaleA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCanaleA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCanaleA.Properties.AutoHeight = False
    Me.edCanaleA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCanaleA.Properties.MaxLength = 65536
    Me.edCanaleA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCanaleA.Size = New System.Drawing.Size(60, 20)
    Me.edCanaleA.TabIndex = 20
    '
    'edCanaleDa
    '
    Me.edCanaleDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCanaleDa.EditValue = "0"
    Me.edCanaleDa.Location = New System.Drawing.Point(93, 83)
    Me.edCanaleDa.Name = "edCanaleDa"
    Me.edCanaleDa.NTSDbField = ""
    Me.edCanaleDa.NTSFormat = "0"
    Me.edCanaleDa.NTSForzaVisZoom = False
    Me.edCanaleDa.NTSOldValue = ""
    Me.edCanaleDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCanaleDa.Properties.Appearance.Options.UseBackColor = True
    Me.edCanaleDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edCanaleDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCanaleDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCanaleDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCanaleDa.Properties.AutoHeight = False
    Me.edCanaleDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCanaleDa.Properties.MaxLength = 65536
    Me.edCanaleDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCanaleDa.Size = New System.Drawing.Size(60, 20)
    Me.edCanaleDa.TabIndex = 19
    '
    'NtsLabel20
    '
    Me.NtsLabel20.AutoSize = True
    Me.NtsLabel20.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel20.Location = New System.Drawing.Point(225, 13)
    Me.NtsLabel20.Name = "NtsLabel20"
    Me.NtsLabel20.NTSDbField = ""
    Me.NtsLabel20.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel20.TabIndex = 15
    Me.NtsLabel20.Text = "A"
    Me.NtsLabel20.Tooltip = ""
    Me.NtsLabel20.UseMnemonic = False
    '
    'NtsLabel21
    '
    Me.NtsLabel21.AutoSize = True
    Me.NtsLabel21.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel21.Location = New System.Drawing.Point(100, 13)
    Me.NtsLabel21.Name = "NtsLabel21"
    Me.NtsLabel21.NTSDbField = ""
    Me.NtsLabel21.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel21.TabIndex = 14
    Me.NtsLabel21.Text = "Da"
    Me.NtsLabel21.Tooltip = ""
    Me.NtsLabel21.UseMnemonic = False
    '
    'edRagsocA
    '
    Me.edRagsocA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRagsocA.EditValue = "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ"
    Me.edRagsocA.Location = New System.Drawing.Point(214, 34)
    Me.edRagsocA.Name = "edRagsocA"
    Me.edRagsocA.NTSDbField = ""
    Me.edRagsocA.NTSForzaVisZoom = False
    Me.edRagsocA.NTSOldValue = "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ"
    Me.edRagsocA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edRagsocA.Properties.Appearance.Options.UseBackColor = True
    Me.edRagsocA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRagsocA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRagsocA.Properties.AutoHeight = False
    Me.edRagsocA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRagsocA.Properties.MaxLength = 65536
    Me.edRagsocA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRagsocA.Size = New System.Drawing.Size(97, 20)
    Me.edRagsocA.TabIndex = 13
    '
    'edRagsocDa
    '
    Me.edRagsocDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRagsocDa.Location = New System.Drawing.Point(93, 34)
    Me.edRagsocDa.Name = "edRagsocDa"
    Me.edRagsocDa.NTSDbField = ""
    Me.edRagsocDa.NTSForzaVisZoom = False
    Me.edRagsocDa.NTSOldValue = ""
    Me.edRagsocDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edRagsocDa.Properties.Appearance.Options.UseBackColor = True
    Me.edRagsocDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRagsocDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRagsocDa.Properties.AutoHeight = False
    Me.edRagsocDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRagsocDa.Properties.MaxLength = 65536
    Me.edRagsocDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRagsocDa.Size = New System.Drawing.Size(97, 20)
    Me.edRagsocDa.TabIndex = 12
    '
    'NtsLabel19
    '
    Me.NtsLabel19.AutoSize = True
    Me.NtsLabel19.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel19.Location = New System.Drawing.Point(14, 37)
    Me.NtsLabel19.Name = "NtsLabel19"
    Me.NtsLabel19.NTSDbField = ""
    Me.NtsLabel19.Size = New System.Drawing.Size(65, 13)
    Me.NtsLabel19.TabIndex = 11
    Me.NtsLabel19.Text = "Rag. sociale"
    Me.NtsLabel19.Tooltip = ""
    Me.NtsLabel19.UseMnemonic = False
    '
    'pnTab2Pan1
    '
    Me.pnTab2Pan1.AllowDrop = True
    Me.pnTab2Pan1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab2Pan1.Appearance.Options.UseBackColor = True
    Me.pnTab2Pan1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab2Pan1.Controls.Add(Me.edProvinciaA)
    Me.pnTab2Pan1.Controls.Add(Me.NtsLabel18)
    Me.pnTab2Pan1.Controls.Add(Me.NtsLabel17)
    Me.pnTab2Pan1.Controls.Add(Me.NtsLabel16)
    Me.pnTab2Pan1.Controls.Add(Me.edPagamA)
    Me.pnTab2Pan1.Controls.Add(Me.edAgenteA)
    Me.pnTab2Pan1.Controls.Add(Me.edCategA)
    Me.pnTab2Pan1.Controls.Add(Me.edPagamDa)
    Me.pnTab2Pan1.Controls.Add(Me.edAgenteDa)
    Me.pnTab2Pan1.Controls.Add(Me.edCategDa)
    Me.pnTab2Pan1.Controls.Add(Me.NtsLabel15)
    Me.pnTab2Pan1.Controls.Add(Me.edZonaA)
    Me.pnTab2Pan1.Controls.Add(Me.edZonaDa)
    Me.pnTab2Pan1.Controls.Add(Me.edCapA)
    Me.pnTab2Pan1.Controls.Add(Me.edCapDa)
    Me.pnTab2Pan1.Controls.Add(Me.NtsLabel14)
    Me.pnTab2Pan1.Controls.Add(Me.edProvinciaDa)
    Me.pnTab2Pan1.Controls.Add(Me.NtsLabel13)
    Me.pnTab2Pan1.Controls.Add(Me.edContoA)
    Me.pnTab2Pan1.Controls.Add(Me.edContoDa)
    Me.pnTab2Pan1.Controls.Add(Me.NtsLabel12)
    Me.pnTab2Pan1.Controls.Add(Me.NtsLabel11)
    Me.pnTab2Pan1.Controls.Add(Me.NtsLabel10)
    Me.pnTab2Pan1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab2Pan1.Location = New System.Drawing.Point(0, 0)
    Me.pnTab2Pan1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTab2Pan1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTab2Pan1.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab2Pan1.Name = "pnTab2Pan1"
    Me.pnTab2Pan1.NTSActiveTrasparency = True
    Me.pnTab2Pan1.Size = New System.Drawing.Size(332, 235)
    Me.pnTab2Pan1.TabIndex = 3
    '
    'edProvinciaA
    '
    Me.edProvinciaA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edProvinciaA.EditValue = "ZZ"
    Me.edProvinciaA.Location = New System.Drawing.Point(207, 62)
    Me.edProvinciaA.Name = "edProvinciaA"
    Me.edProvinciaA.NTSDbField = ""
    Me.edProvinciaA.NTSForzaVisZoom = False
    Me.edProvinciaA.NTSOldValue = "ZZ"
    Me.edProvinciaA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edProvinciaA.Properties.Appearance.Options.UseBackColor = True
    Me.edProvinciaA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edProvinciaA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edProvinciaA.Properties.AutoHeight = False
    Me.edProvinciaA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edProvinciaA.Properties.MaxLength = 65536
    Me.edProvinciaA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edProvinciaA.Size = New System.Drawing.Size(61, 20)
    Me.edProvinciaA.TabIndex = 23
    '
    'NtsLabel18
    '
    Me.NtsLabel18.AutoSize = True
    Me.NtsLabel18.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel18.Location = New System.Drawing.Point(8, 192)
    Me.NtsLabel18.Name = "NtsLabel18"
    Me.NtsLabel18.NTSDbField = ""
    Me.NtsLabel18.Size = New System.Drawing.Size(61, 13)
    Me.NtsLabel18.TabIndex = 22
    Me.NtsLabel18.Text = "Pagamento"
    Me.NtsLabel18.Tooltip = ""
    Me.NtsLabel18.UseMnemonic = False
    '
    'NtsLabel17
    '
    Me.NtsLabel17.AutoSize = True
    Me.NtsLabel17.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel17.Location = New System.Drawing.Point(8, 167)
    Me.NtsLabel17.Name = "NtsLabel17"
    Me.NtsLabel17.NTSDbField = ""
    Me.NtsLabel17.Size = New System.Drawing.Size(42, 13)
    Me.NtsLabel17.TabIndex = 21
    Me.NtsLabel17.Text = "Agente"
    Me.NtsLabel17.Tooltip = ""
    Me.NtsLabel17.UseMnemonic = False
    '
    'NtsLabel16
    '
    Me.NtsLabel16.AutoSize = True
    Me.NtsLabel16.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel16.Location = New System.Drawing.Point(8, 141)
    Me.NtsLabel16.Name = "NtsLabel16"
    Me.NtsLabel16.NTSDbField = ""
    Me.NtsLabel16.Size = New System.Drawing.Size(54, 13)
    Me.NtsLabel16.TabIndex = 20
    Me.NtsLabel16.Text = "Categoria"
    Me.NtsLabel16.Tooltip = ""
    Me.NtsLabel16.UseMnemonic = False
    '
    'edPagamA
    '
    Me.edPagamA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edPagamA.EditValue = "999"
    Me.edPagamA.Location = New System.Drawing.Point(207, 189)
    Me.edPagamA.Name = "edPagamA"
    Me.edPagamA.NTSDbField = ""
    Me.edPagamA.NTSFormat = "0"
    Me.edPagamA.NTSForzaVisZoom = False
    Me.edPagamA.NTSOldValue = "999"
    Me.edPagamA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edPagamA.Properties.Appearance.Options.UseBackColor = True
    Me.edPagamA.Properties.Appearance.Options.UseTextOptions = True
    Me.edPagamA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPagamA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPagamA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPagamA.Properties.AutoHeight = False
    Me.edPagamA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPagamA.Properties.MaxLength = 65536
    Me.edPagamA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPagamA.Size = New System.Drawing.Size(61, 20)
    Me.edPagamA.TabIndex = 19
    '
    'edAgenteA
    '
    Me.edAgenteA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAgenteA.EditValue = "9999"
    Me.edAgenteA.Location = New System.Drawing.Point(208, 164)
    Me.edAgenteA.Name = "edAgenteA"
    Me.edAgenteA.NTSDbField = ""
    Me.edAgenteA.NTSFormat = "0"
    Me.edAgenteA.NTSForzaVisZoom = False
    Me.edAgenteA.NTSOldValue = "9999"
    Me.edAgenteA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAgenteA.Properties.Appearance.Options.UseBackColor = True
    Me.edAgenteA.Properties.Appearance.Options.UseTextOptions = True
    Me.edAgenteA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAgenteA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAgenteA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAgenteA.Properties.AutoHeight = False
    Me.edAgenteA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAgenteA.Properties.MaxLength = 65536
    Me.edAgenteA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAgenteA.Size = New System.Drawing.Size(60, 20)
    Me.edAgenteA.TabIndex = 18
    '
    'edCategA
    '
    Me.edCategA.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edCategA.EditValue = "999"
    Me.edCategA.Location = New System.Drawing.Point(208, 138)
    Me.edCategA.Name = "edCategA"
    Me.edCategA.NTSDbField = ""
    Me.edCategA.NTSFormat = "0"
    Me.edCategA.NTSForzaVisZoom = False
    Me.edCategA.NTSOldValue = "999"
    Me.edCategA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCategA.Properties.Appearance.Options.UseBackColor = True
    Me.edCategA.Properties.Appearance.Options.UseTextOptions = True
    Me.edCategA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCategA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCategA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCategA.Properties.AutoHeight = False
    Me.edCategA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCategA.Properties.MaxLength = 65536
    Me.edCategA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCategA.Size = New System.Drawing.Size(60, 20)
    Me.edCategA.TabIndex = 17
    '
    'edPagamDa
    '
    Me.edPagamDa.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edPagamDa.EditValue = "0"
    Me.edPagamDa.Location = New System.Drawing.Point(87, 189)
    Me.edPagamDa.Name = "edPagamDa"
    Me.edPagamDa.NTSDbField = ""
    Me.edPagamDa.NTSFormat = "0"
    Me.edPagamDa.NTSForzaVisZoom = False
    Me.edPagamDa.NTSOldValue = ""
    Me.edPagamDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edPagamDa.Properties.Appearance.Options.UseBackColor = True
    Me.edPagamDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edPagamDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edPagamDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edPagamDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edPagamDa.Properties.AutoHeight = False
    Me.edPagamDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edPagamDa.Properties.MaxLength = 65536
    Me.edPagamDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edPagamDa.Size = New System.Drawing.Size(59, 20)
    Me.edPagamDa.TabIndex = 16
    '
    'edAgenteDa
    '
    Me.edAgenteDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAgenteDa.EditValue = "0"
    Me.edAgenteDa.Location = New System.Drawing.Point(87, 164)
    Me.edAgenteDa.Name = "edAgenteDa"
    Me.edAgenteDa.NTSDbField = ""
    Me.edAgenteDa.NTSFormat = "0"
    Me.edAgenteDa.NTSForzaVisZoom = False
    Me.edAgenteDa.NTSOldValue = ""
    Me.edAgenteDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAgenteDa.Properties.Appearance.Options.UseBackColor = True
    Me.edAgenteDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edAgenteDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAgenteDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAgenteDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAgenteDa.Properties.AutoHeight = False
    Me.edAgenteDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAgenteDa.Properties.MaxLength = 65536
    Me.edAgenteDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAgenteDa.Size = New System.Drawing.Size(59, 20)
    Me.edAgenteDa.TabIndex = 15
    '
    'edCategDa
    '
    Me.edCategDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCategDa.EditValue = "0"
    Me.edCategDa.Location = New System.Drawing.Point(87, 138)
    Me.edCategDa.Name = "edCategDa"
    Me.edCategDa.NTSDbField = ""
    Me.edCategDa.NTSFormat = "0"
    Me.edCategDa.NTSForzaVisZoom = False
    Me.edCategDa.NTSOldValue = ""
    Me.edCategDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCategDa.Properties.Appearance.Options.UseBackColor = True
    Me.edCategDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edCategDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCategDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCategDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCategDa.Properties.AutoHeight = False
    Me.edCategDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCategDa.Properties.MaxLength = 65536
    Me.edCategDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCategDa.Size = New System.Drawing.Size(59, 20)
    Me.edCategDa.TabIndex = 14
    '
    'NtsLabel15
    '
    Me.NtsLabel15.AutoSize = True
    Me.NtsLabel15.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel15.Location = New System.Drawing.Point(8, 115)
    Me.NtsLabel15.Name = "NtsLabel15"
    Me.NtsLabel15.NTSDbField = ""
    Me.NtsLabel15.Size = New System.Drawing.Size(31, 13)
    Me.NtsLabel15.TabIndex = 13
    Me.NtsLabel15.Text = "Zona"
    Me.NtsLabel15.Tooltip = ""
    Me.NtsLabel15.UseMnemonic = False
    '
    'edZonaA
    '
    Me.edZonaA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edZonaA.EditValue = "999"
    Me.edZonaA.Location = New System.Drawing.Point(208, 112)
    Me.edZonaA.Name = "edZonaA"
    Me.edZonaA.NTSDbField = ""
    Me.edZonaA.NTSFormat = "0"
    Me.edZonaA.NTSForzaVisZoom = False
    Me.edZonaA.NTSOldValue = "999"
    Me.edZonaA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edZonaA.Properties.Appearance.Options.UseBackColor = True
    Me.edZonaA.Properties.Appearance.Options.UseTextOptions = True
    Me.edZonaA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edZonaA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edZonaA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edZonaA.Properties.AutoHeight = False
    Me.edZonaA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edZonaA.Properties.MaxLength = 65536
    Me.edZonaA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edZonaA.Size = New System.Drawing.Size(60, 20)
    Me.edZonaA.TabIndex = 12
    '
    'edZonaDa
    '
    Me.edZonaDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edZonaDa.EditValue = "0"
    Me.edZonaDa.Location = New System.Drawing.Point(87, 112)
    Me.edZonaDa.Name = "edZonaDa"
    Me.edZonaDa.NTSDbField = ""
    Me.edZonaDa.NTSFormat = "0"
    Me.edZonaDa.NTSForzaVisZoom = False
    Me.edZonaDa.NTSOldValue = ""
    Me.edZonaDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edZonaDa.Properties.Appearance.Options.UseBackColor = True
    Me.edZonaDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edZonaDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edZonaDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edZonaDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edZonaDa.Properties.AutoHeight = False
    Me.edZonaDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edZonaDa.Properties.MaxLength = 65536
    Me.edZonaDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edZonaDa.Size = New System.Drawing.Size(59, 20)
    Me.edZonaDa.TabIndex = 11
    '
    'edCapA
    '
    Me.edCapA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCapA.EditValue = "ZZZZZ"
    Me.edCapA.Location = New System.Drawing.Point(208, 86)
    Me.edCapA.Name = "edCapA"
    Me.edCapA.NTSDbField = ""
    Me.edCapA.NTSForzaVisZoom = False
    Me.edCapA.NTSOldValue = "ZZZZZ"
    Me.edCapA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCapA.Properties.Appearance.Options.UseBackColor = True
    Me.edCapA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCapA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCapA.Properties.AutoHeight = False
    Me.edCapA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCapA.Properties.MaxLength = 65536
    Me.edCapA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCapA.Size = New System.Drawing.Size(97, 20)
    Me.edCapA.TabIndex = 10
    '
    'edCapDa
    '
    Me.edCapDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCapDa.Location = New System.Drawing.Point(87, 86)
    Me.edCapDa.Name = "edCapDa"
    Me.edCapDa.NTSDbField = ""
    Me.edCapDa.NTSForzaVisZoom = False
    Me.edCapDa.NTSOldValue = ""
    Me.edCapDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCapDa.Properties.Appearance.Options.UseBackColor = True
    Me.edCapDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCapDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCapDa.Properties.AutoHeight = False
    Me.edCapDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCapDa.Properties.MaxLength = 65536
    Me.edCapDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCapDa.Size = New System.Drawing.Size(97, 20)
    Me.edCapDa.TabIndex = 9
    '
    'NtsLabel14
    '
    Me.NtsLabel14.AutoSize = True
    Me.NtsLabel14.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel14.Location = New System.Drawing.Point(8, 89)
    Me.NtsLabel14.Name = "NtsLabel14"
    Me.NtsLabel14.NTSDbField = ""
    Me.NtsLabel14.Size = New System.Drawing.Size(26, 13)
    Me.NtsLabel14.TabIndex = 8
    Me.NtsLabel14.Text = "Cap"
    Me.NtsLabel14.Tooltip = ""
    Me.NtsLabel14.UseMnemonic = False
    '
    'edProvinciaDa
    '
    Me.edProvinciaDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edProvinciaDa.Location = New System.Drawing.Point(87, 62)
    Me.edProvinciaDa.Name = "edProvinciaDa"
    Me.edProvinciaDa.NTSDbField = ""
    Me.edProvinciaDa.NTSForzaVisZoom = False
    Me.edProvinciaDa.NTSOldValue = ""
    Me.edProvinciaDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edProvinciaDa.Properties.Appearance.Options.UseBackColor = True
    Me.edProvinciaDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edProvinciaDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edProvinciaDa.Properties.AutoHeight = False
    Me.edProvinciaDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edProvinciaDa.Properties.MaxLength = 65536
    Me.edProvinciaDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edProvinciaDa.Size = New System.Drawing.Size(59, 20)
    Me.edProvinciaDa.TabIndex = 6
    '
    'NtsLabel13
    '
    Me.NtsLabel13.AutoSize = True
    Me.NtsLabel13.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel13.Location = New System.Drawing.Point(8, 65)
    Me.NtsLabel13.Name = "NtsLabel13"
    Me.NtsLabel13.NTSDbField = ""
    Me.NtsLabel13.Size = New System.Drawing.Size(50, 13)
    Me.NtsLabel13.TabIndex = 5
    Me.NtsLabel13.Text = "Provincia"
    Me.NtsLabel13.Tooltip = ""
    Me.NtsLabel13.UseMnemonic = False
    '
    'edContoA
    '
    Me.edContoA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContoA.EditValue = "999999999"
    Me.edContoA.Location = New System.Drawing.Point(207, 37)
    Me.edContoA.Name = "edContoA"
    Me.edContoA.NTSDbField = ""
    Me.edContoA.NTSFormat = "0"
    Me.edContoA.NTSForzaVisZoom = False
    Me.edContoA.NTSOldValue = "999999999"
    Me.edContoA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edContoA.Properties.Appearance.Options.UseBackColor = True
    Me.edContoA.Properties.Appearance.Options.UseTextOptions = True
    Me.edContoA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edContoA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edContoA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edContoA.Properties.AutoHeight = False
    Me.edContoA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edContoA.Properties.MaxLength = 65536
    Me.edContoA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edContoA.Size = New System.Drawing.Size(97, 20)
    Me.edContoA.TabIndex = 4
    '
    'edContoDa
    '
    Me.edContoDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContoDa.EditValue = "0"
    Me.edContoDa.Location = New System.Drawing.Point(87, 37)
    Me.edContoDa.Name = "edContoDa"
    Me.edContoDa.NTSDbField = ""
    Me.edContoDa.NTSFormat = "0"
    Me.edContoDa.NTSForzaVisZoom = False
    Me.edContoDa.NTSOldValue = ""
    Me.edContoDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edContoDa.Properties.Appearance.Options.UseBackColor = True
    Me.edContoDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edContoDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edContoDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edContoDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edContoDa.Properties.AutoHeight = False
    Me.edContoDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edContoDa.Properties.MaxLength = 65536
    Me.edContoDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edContoDa.Size = New System.Drawing.Size(97, 20)
    Me.edContoDa.TabIndex = 3
    '
    'NtsLabel12
    '
    Me.NtsLabel12.AutoSize = True
    Me.NtsLabel12.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel12.Location = New System.Drawing.Point(8, 40)
    Me.NtsLabel12.Name = "NtsLabel12"
    Me.NtsLabel12.NTSDbField = ""
    Me.NtsLabel12.Size = New System.Drawing.Size(36, 13)
    Me.NtsLabel12.TabIndex = 2
    Me.NtsLabel12.Text = "Conto"
    Me.NtsLabel12.Tooltip = ""
    Me.NtsLabel12.UseMnemonic = False
    '
    'NtsLabel11
    '
    Me.NtsLabel11.AutoSize = True
    Me.NtsLabel11.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel11.Location = New System.Drawing.Point(225, 16)
    Me.NtsLabel11.Name = "NtsLabel11"
    Me.NtsLabel11.NTSDbField = ""
    Me.NtsLabel11.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel11.TabIndex = 1
    Me.NtsLabel11.Text = "A"
    Me.NtsLabel11.Tooltip = ""
    Me.NtsLabel11.UseMnemonic = False
    '
    'NtsLabel10
    '
    Me.NtsLabel10.AutoSize = True
    Me.NtsLabel10.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel10.Location = New System.Drawing.Point(100, 16)
    Me.NtsLabel10.Name = "NtsLabel10"
    Me.NtsLabel10.NTSDbField = ""
    Me.NtsLabel10.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel10.TabIndex = 0
    Me.NtsLabel10.Text = "Da"
    Me.NtsLabel10.Tooltip = ""
    Me.NtsLabel10.UseMnemonic = False
    '
    'TabPage3
    '
    Me.TabPage3.AllowDrop = True
    Me.TabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.TabPage3.Controls.Add(Me.pnTab3Pan1)
    Me.TabPage3.Controls.Add(Me.pnTab3Pan2)
    Me.TabPage3.Enable = True
    Me.TabPage3.Name = "TabPage3"
    Me.TabPage3.Size = New System.Drawing.Size(669, 230)
    Me.TabPage3.Text = "Filtri &2"
    '
    'pnTab3Pan1
    '
    Me.pnTab3Pan1.AllowDrop = True
    Me.pnTab3Pan1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab3Pan1.Appearance.Options.UseBackColor = True
    Me.pnTab3Pan1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab3Pan1.Controls.Add(Me.lbUsaem)
    Me.pnTab3Pan1.Controls.Add(Me.liUsaem)
    Me.pnTab3Pan1.Controls.Add(Me.edStato)
    Me.pnTab3Pan1.Controls.Add(Me.NtsLabel22)
    Me.pnTab3Pan1.Controls.Add(Me.NtsLabel38)
    Me.pnTab3Pan1.Controls.Add(Me.liBlocco)
    Me.pnTab3Pan1.Controls.Add(Me.NtsLabel32)
    Me.pnTab3Pan1.Controls.Add(Me.edLinguaA)
    Me.pnTab3Pan1.Controls.Add(Me.edLinguaDa)
    Me.pnTab3Pan1.Controls.Add(Me.NtsLabel31)
    Me.pnTab3Pan1.Controls.Add(Me.edSconA)
    Me.pnTab3Pan1.Controls.Add(Me.edSconDa)
    Me.pnTab3Pan1.Controls.Add(Me.NtsLabel29)
    Me.pnTab3Pan1.Controls.Add(Me.NtsLabel30)
    Me.pnTab3Pan1.Controls.Add(Me.NtsLabel28)
    Me.pnTab3Pan1.Controls.Add(Me.edProvA)
    Me.pnTab3Pan1.Controls.Add(Me.edProvDa)
    Me.pnTab3Pan1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab3Pan1.Location = New System.Drawing.Point(3, 0)
    Me.pnTab3Pan1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTab3Pan1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTab3Pan1.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab3Pan1.Name = "pnTab3Pan1"
    Me.pnTab3Pan1.NTSActiveTrasparency = True
    Me.pnTab3Pan1.Size = New System.Drawing.Size(332, 235)
    Me.pnTab3Pan1.TabIndex = 6
    '
    'lbUsaem
    '
    Me.lbUsaem.AutoSize = True
    Me.lbUsaem.BackColor = System.Drawing.Color.Transparent
    Me.lbUsaem.Location = New System.Drawing.Point(8, 139)
    Me.lbUsaem.Name = "lbUsaem"
    Me.lbUsaem.NTSDbField = ""
    Me.lbUsaem.Size = New System.Drawing.Size(75, 13)
    Me.lbUsaem.TabIndex = 62
    Me.lbUsaem.Text = "Mod. sp. corr."
    Me.lbUsaem.Tooltip = ""
    Me.lbUsaem.UseMnemonic = False
    '
    'liUsaem
    '
    Me.liUsaem.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.liUsaem.Appearance.Options.UseBackColor = True
    Me.liUsaem.Cursor = System.Windows.Forms.Cursors.Default
    Me.liUsaem.ItemHeight = 14
    Me.liUsaem.Location = New System.Drawing.Point(87, 139)
    Me.liUsaem.Name = "liUsaem"
    Me.liUsaem.NTSDbField = ""
    Me.liUsaem.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liUsaem.Size = New System.Drawing.Size(180, 43)
    Me.liUsaem.TabIndex = 61
    '
    'edStato
    '
    Me.edStato.Cursor = System.Windows.Forms.Cursors.Default
    Me.edStato.Location = New System.Drawing.Point(87, 110)
    Me.edStato.Name = "edStato"
    Me.edStato.NTSDbField = ""
    Me.edStato.NTSForzaVisZoom = False
    Me.edStato.NTSOldValue = ""
    Me.edStato.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edStato.Properties.Appearance.Options.UseBackColor = True
    Me.edStato.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edStato.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edStato.Properties.AutoHeight = False
    Me.edStato.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edStato.Properties.MaxLength = 65536
    Me.edStato.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edStato.Size = New System.Drawing.Size(62, 20)
    Me.edStato.TabIndex = 60
    '
    'NtsLabel22
    '
    Me.NtsLabel22.AutoSize = True
    Me.NtsLabel22.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel22.Location = New System.Drawing.Point(8, 113)
    Me.NtsLabel22.Name = "NtsLabel22"
    Me.NtsLabel22.NTSDbField = ""
    Me.NtsLabel22.Size = New System.Drawing.Size(67, 13)
    Me.NtsLabel22.TabIndex = 59
    Me.NtsLabel22.Text = "Stato estero"
    Me.NtsLabel22.Tooltip = ""
    Me.NtsLabel22.UseMnemonic = False
    '
    'NtsLabel38
    '
    Me.NtsLabel38.AutoSize = True
    Me.NtsLabel38.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel38.Location = New System.Drawing.Point(8, 188)
    Me.NtsLabel38.Name = "NtsLabel38"
    Me.NtsLabel38.NTSDbField = ""
    Me.NtsLabel38.Size = New System.Drawing.Size(37, 13)
    Me.NtsLabel38.TabIndex = 58
    Me.NtsLabel38.Text = "Blocco"
    Me.NtsLabel38.Tooltip = ""
    Me.NtsLabel38.UseMnemonic = False
    '
    'liBlocco
    '
    Me.liBlocco.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.liBlocco.Appearance.Options.UseBackColor = True
    Me.liBlocco.Cursor = System.Windows.Forms.Cursors.Default
    Me.liBlocco.ItemHeight = 14
    Me.liBlocco.Location = New System.Drawing.Point(87, 188)
    Me.liBlocco.Name = "liBlocco"
    Me.liBlocco.NTSDbField = ""
    Me.liBlocco.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liBlocco.Size = New System.Drawing.Size(180, 43)
    Me.liBlocco.TabIndex = 57
    '
    'NtsLabel32
    '
    Me.NtsLabel32.AutoSize = True
    Me.NtsLabel32.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel32.Location = New System.Drawing.Point(8, 88)
    Me.NtsLabel32.Name = "NtsLabel32"
    Me.NtsLabel32.NTSDbField = ""
    Me.NtsLabel32.Size = New System.Drawing.Size(38, 13)
    Me.NtsLabel32.TabIndex = 44
    Me.NtsLabel32.Text = "Lingua"
    Me.NtsLabel32.Tooltip = ""
    Me.NtsLabel32.UseMnemonic = False
    '
    'edLinguaA
    '
    Me.edLinguaA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLinguaA.EditValue = "999"
    Me.edLinguaA.Location = New System.Drawing.Point(207, 85)
    Me.edLinguaA.Name = "edLinguaA"
    Me.edLinguaA.NTSDbField = ""
    Me.edLinguaA.NTSFormat = "0"
    Me.edLinguaA.NTSForzaVisZoom = False
    Me.edLinguaA.NTSOldValue = "999"
    Me.edLinguaA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edLinguaA.Properties.Appearance.Options.UseBackColor = True
    Me.edLinguaA.Properties.Appearance.Options.UseTextOptions = True
    Me.edLinguaA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLinguaA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLinguaA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLinguaA.Properties.AutoHeight = False
    Me.edLinguaA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLinguaA.Properties.MaxLength = 65536
    Me.edLinguaA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLinguaA.Size = New System.Drawing.Size(62, 20)
    Me.edLinguaA.TabIndex = 43
    '
    'edLinguaDa
    '
    Me.edLinguaDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edLinguaDa.EditValue = "0"
    Me.edLinguaDa.Location = New System.Drawing.Point(87, 85)
    Me.edLinguaDa.Name = "edLinguaDa"
    Me.edLinguaDa.NTSDbField = ""
    Me.edLinguaDa.NTSFormat = "0"
    Me.edLinguaDa.NTSForzaVisZoom = False
    Me.edLinguaDa.NTSOldValue = ""
    Me.edLinguaDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edLinguaDa.Properties.Appearance.Options.UseBackColor = True
    Me.edLinguaDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edLinguaDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edLinguaDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edLinguaDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edLinguaDa.Properties.AutoHeight = False
    Me.edLinguaDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edLinguaDa.Properties.MaxLength = 65536
    Me.edLinguaDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edLinguaDa.Size = New System.Drawing.Size(62, 20)
    Me.edLinguaDa.TabIndex = 42
    '
    'NtsLabel31
    '
    Me.NtsLabel31.AutoSize = True
    Me.NtsLabel31.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel31.Location = New System.Drawing.Point(8, 64)
    Me.NtsLabel31.Name = "NtsLabel31"
    Me.NtsLabel31.NTSDbField = ""
    Me.NtsLabel31.Size = New System.Drawing.Size(69, 13)
    Me.NtsLabel31.TabIndex = 41
    Me.NtsLabel31.Text = "Classe sconti"
    Me.NtsLabel31.Tooltip = ""
    Me.NtsLabel31.UseMnemonic = False
    '
    'edSconA
    '
    Me.edSconA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSconA.EditValue = "9999"
    Me.edSconA.Location = New System.Drawing.Point(207, 61)
    Me.edSconA.Name = "edSconA"
    Me.edSconA.NTSDbField = ""
    Me.edSconA.NTSFormat = "0"
    Me.edSconA.NTSForzaVisZoom = False
    Me.edSconA.NTSOldValue = "9999"
    Me.edSconA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edSconA.Properties.Appearance.Options.UseBackColor = True
    Me.edSconA.Properties.Appearance.Options.UseTextOptions = True
    Me.edSconA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSconA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSconA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSconA.Properties.AutoHeight = False
    Me.edSconA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSconA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSconA.Size = New System.Drawing.Size(62, 20)
    Me.edSconA.TabIndex = 40
    '
    'edSconDa
    '
    Me.edSconDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSconDa.EditValue = "0"
    Me.edSconDa.Location = New System.Drawing.Point(87, 61)
    Me.edSconDa.Name = "edSconDa"
    Me.edSconDa.NTSDbField = ""
    Me.edSconDa.NTSFormat = "0"
    Me.edSconDa.NTSForzaVisZoom = False
    Me.edSconDa.NTSOldValue = ""
    Me.edSconDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edSconDa.Properties.Appearance.Options.UseBackColor = True
    Me.edSconDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edSconDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSconDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSconDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSconDa.Properties.AutoHeight = False
    Me.edSconDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSconDa.Properties.MaxLength = 65536
    Me.edSconDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSconDa.Size = New System.Drawing.Size(62, 20)
    Me.edSconDa.TabIndex = 39
    '
    'NtsLabel29
    '
    Me.NtsLabel29.AutoSize = True
    Me.NtsLabel29.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel29.Location = New System.Drawing.Point(225, 16)
    Me.NtsLabel29.Name = "NtsLabel29"
    Me.NtsLabel29.NTSDbField = ""
    Me.NtsLabel29.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel29.TabIndex = 38
    Me.NtsLabel29.Text = "A"
    Me.NtsLabel29.Tooltip = ""
    Me.NtsLabel29.UseMnemonic = False
    '
    'NtsLabel30
    '
    Me.NtsLabel30.AutoSize = True
    Me.NtsLabel30.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel30.Location = New System.Drawing.Point(100, 16)
    Me.NtsLabel30.Name = "NtsLabel30"
    Me.NtsLabel30.NTSDbField = ""
    Me.NtsLabel30.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel30.TabIndex = 37
    Me.NtsLabel30.Text = "Da"
    Me.NtsLabel30.Tooltip = ""
    Me.NtsLabel30.UseMnemonic = False
    '
    'NtsLabel28
    '
    Me.NtsLabel28.AutoSize = True
    Me.NtsLabel28.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel28.Location = New System.Drawing.Point(8, 40)
    Me.NtsLabel28.Name = "NtsLabel28"
    Me.NtsLabel28.NTSDbField = ""
    Me.NtsLabel28.Size = New System.Drawing.Size(73, 13)
    Me.NtsLabel28.TabIndex = 36
    Me.NtsLabel28.Text = "Classe provv."
    Me.NtsLabel28.Tooltip = ""
    Me.NtsLabel28.UseMnemonic = False
    '
    'edProvA
    '
    Me.edProvA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edProvA.EditValue = "999"
    Me.edProvA.Location = New System.Drawing.Point(207, 37)
    Me.edProvA.Name = "edProvA"
    Me.edProvA.NTSDbField = ""
    Me.edProvA.NTSFormat = "0"
    Me.edProvA.NTSForzaVisZoom = False
    Me.edProvA.NTSOldValue = "999"
    Me.edProvA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edProvA.Properties.Appearance.Options.UseBackColor = True
    Me.edProvA.Properties.Appearance.Options.UseTextOptions = True
    Me.edProvA.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edProvA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edProvA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edProvA.Properties.AutoHeight = False
    Me.edProvA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edProvA.Properties.MaxLength = 65536
    Me.edProvA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edProvA.Size = New System.Drawing.Size(62, 20)
    Me.edProvA.TabIndex = 35
    '
    'edProvDa
    '
    Me.edProvDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edProvDa.EditValue = "0"
    Me.edProvDa.Location = New System.Drawing.Point(87, 37)
    Me.edProvDa.Name = "edProvDa"
    Me.edProvDa.NTSDbField = ""
    Me.edProvDa.NTSFormat = "0"
    Me.edProvDa.NTSForzaVisZoom = False
    Me.edProvDa.NTSOldValue = ""
    Me.edProvDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edProvDa.Properties.Appearance.Options.UseBackColor = True
    Me.edProvDa.Properties.Appearance.Options.UseTextOptions = True
    Me.edProvDa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edProvDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edProvDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edProvDa.Properties.AutoHeight = False
    Me.edProvDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edProvDa.Properties.MaxLength = 65536
    Me.edProvDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edProvDa.Size = New System.Drawing.Size(62, 20)
    Me.edProvDa.TabIndex = 34
    '
    'pnTab3Pan2
    '
    Me.pnTab3Pan2.AllowDrop = True
    Me.pnTab3Pan2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab3Pan2.Appearance.Options.UseBackColor = True
    Me.pnTab3Pan2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab3Pan2.Controls.Add(Me.NtsLabel39)
    Me.pnTab3Pan2.Controls.Add(Me.NtsLabel37)
    Me.pnTab3Pan2.Controls.Add(Me.liPariva)
    Me.pnTab3Pan2.Controls.Add(Me.NtsLabel36)
    Me.pnTab3Pan2.Controls.Add(Me.liGgcons)
    Me.pnTab3Pan2.Controls.Add(Me.NtsLabel35)
    Me.pnTab3Pan2.Controls.Add(Me.liFatturaz)
    Me.pnTab3Pan2.Controls.Add(Me.liStatus)
    Me.pnTab3Pan2.Controls.Add(Me.NtsLabel34)
    Me.pnTab3Pan2.Controls.Add(Me.liPrivacy)
    Me.pnTab3Pan2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab3Pan2.Location = New System.Drawing.Point(341, 0)
    Me.pnTab3Pan2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTab3Pan2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTab3Pan2.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab3Pan2.Name = "pnTab3Pan2"
    Me.pnTab3Pan2.NTSActiveTrasparency = True
    Me.pnTab3Pan2.Size = New System.Drawing.Size(329, 234)
    Me.pnTab3Pan2.TabIndex = 5
    '
    'NtsLabel39
    '
    Me.NtsLabel39.AutoSize = True
    Me.NtsLabel39.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel39.Location = New System.Drawing.Point(15, 192)
    Me.NtsLabel39.Name = "NtsLabel39"
    Me.NtsLabel39.NTSDbField = ""
    Me.NtsLabel39.Size = New System.Drawing.Size(83, 13)
    Me.NtsLabel39.TabIndex = 62
    Me.NtsLabel39.Text = "Test partita IVA"
    Me.NtsLabel39.Tooltip = ""
    Me.NtsLabel39.UseMnemonic = False
    '
    'NtsLabel37
    '
    Me.NtsLabel37.AutoSize = True
    Me.NtsLabel37.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel37.Location = New System.Drawing.Point(15, 143)
    Me.NtsLabel37.Name = "NtsLabel37"
    Me.NtsLabel37.NTSDbField = ""
    Me.NtsLabel37.Size = New System.Drawing.Size(87, 13)
    Me.NtsLabel37.TabIndex = 54
    Me.NtsLabel37.Text = "Giorno consegna"
    Me.NtsLabel37.Tooltip = ""
    Me.NtsLabel37.UseMnemonic = False
    '
    'liPariva
    '
    Me.liPariva.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.liPariva.Appearance.Options.UseBackColor = True
    Me.liPariva.Cursor = System.Windows.Forms.Cursors.Default
    Me.liPariva.ItemHeight = 14
    Me.liPariva.Location = New System.Drawing.Point(132, 188)
    Me.liPariva.Name = "liPariva"
    Me.liPariva.NTSDbField = ""
    Me.liPariva.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liPariva.Size = New System.Drawing.Size(180, 43)
    Me.liPariva.TabIndex = 61
    '
    'NtsLabel36
    '
    Me.NtsLabel36.AutoSize = True
    Me.NtsLabel36.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel36.Location = New System.Drawing.Point(15, 97)
    Me.NtsLabel36.Name = "NtsLabel36"
    Me.NtsLabel36.NTSDbField = ""
    Me.NtsLabel36.Size = New System.Drawing.Size(105, 13)
    Me.NtsLabel36.TabIndex = 53
    Me.NtsLabel36.Text = "Periodo fatturazione"
    Me.NtsLabel36.Tooltip = ""
    Me.NtsLabel36.UseMnemonic = False
    '
    'liGgcons
    '
    Me.liGgcons.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.liGgcons.Appearance.Options.UseBackColor = True
    Me.liGgcons.Cursor = System.Windows.Forms.Cursors.Default
    Me.liGgcons.ItemHeight = 14
    Me.liGgcons.Location = New System.Drawing.Point(132, 143)
    Me.liGgcons.Name = "liGgcons"
    Me.liGgcons.NTSDbField = ""
    Me.liGgcons.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liGgcons.Size = New System.Drawing.Size(180, 43)
    Me.liGgcons.TabIndex = 52
    '
    'NtsLabel35
    '
    Me.NtsLabel35.AutoSize = True
    Me.NtsLabel35.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel35.Location = New System.Drawing.Point(15, 50)
    Me.NtsLabel35.Name = "NtsLabel35"
    Me.NtsLabel35.NTSDbField = ""
    Me.NtsLabel35.Size = New System.Drawing.Size(38, 13)
    Me.NtsLabel35.TabIndex = 52
    Me.NtsLabel35.Text = "Status"
    Me.NtsLabel35.Tooltip = ""
    Me.NtsLabel35.UseMnemonic = False
    '
    'liFatturaz
    '
    Me.liFatturaz.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.liFatturaz.Appearance.Options.UseBackColor = True
    Me.liFatturaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.liFatturaz.ItemHeight = 14
    Me.liFatturaz.Location = New System.Drawing.Point(132, 97)
    Me.liFatturaz.Name = "liFatturaz"
    Me.liFatturaz.NTSDbField = ""
    Me.liFatturaz.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liFatturaz.Size = New System.Drawing.Size(180, 43)
    Me.liFatturaz.TabIndex = 51
    '
    'liStatus
    '
    Me.liStatus.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.liStatus.Appearance.Options.UseBackColor = True
    Me.liStatus.Cursor = System.Windows.Forms.Cursors.Default
    Me.liStatus.ItemHeight = 14
    Me.liStatus.Location = New System.Drawing.Point(132, 50)
    Me.liStatus.Name = "liStatus"
    Me.liStatus.NTSDbField = ""
    Me.liStatus.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liStatus.Size = New System.Drawing.Size(180, 43)
    Me.liStatus.TabIndex = 50
    '
    'NtsLabel34
    '
    Me.NtsLabel34.AutoSize = True
    Me.NtsLabel34.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel34.Location = New System.Drawing.Point(15, 3)
    Me.NtsLabel34.Name = "NtsLabel34"
    Me.NtsLabel34.NTSDbField = ""
    Me.NtsLabel34.Size = New System.Drawing.Size(42, 13)
    Me.NtsLabel34.TabIndex = 49
    Me.NtsLabel34.Text = "Privacy"
    Me.NtsLabel34.Tooltip = ""
    Me.NtsLabel34.UseMnemonic = False
    '
    'liPrivacy
    '
    Me.liPrivacy.Appearance.BackColor = System.Drawing.SystemColors.Window
    Me.liPrivacy.Appearance.Options.UseBackColor = True
    Me.liPrivacy.Cursor = System.Windows.Forms.Cursors.Default
    Me.liPrivacy.ItemHeight = 14
    Me.liPrivacy.Location = New System.Drawing.Point(132, 3)
    Me.liPrivacy.Name = "liPrivacy"
    Me.liPrivacy.NTSDbField = ""
    Me.liPrivacy.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
    Me.liPrivacy.Size = New System.Drawing.Size(180, 43)
    Me.liPrivacy.TabIndex = 48
    '
    'pnAction
    '
    Me.pnAction.AllowDrop = True
    Me.pnAction.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAction.Appearance.Options.UseBackColor = True
    Me.pnAction.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAction.Controls.Add(Me.cmdLastfilter)
    Me.pnAction.Controls.Add(Me.cmdEstensioni)
    Me.pnAction.Controls.Add(Me.ckAnagen)
    Me.pnAction.Controls.Add(Me.cmdEscludi)
    Me.pnAction.Controls.Add(Me.cmdNuovoPdc)
    Me.pnAction.Controls.Add(Me.ckOttimistico)
    Me.pnAction.Controls.Add(Me.cmdAnnulla)
    Me.pnAction.Controls.Add(Me.cmdGestione)
    Me.pnAction.Controls.Add(Me.cmdSeleziona)
    Me.pnAction.Controls.Add(Me.cmdRicerca)
    Me.pnAction.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAction.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnAction.Location = New System.Drawing.Point(678, 0)
    Me.pnAction.Name = "pnAction"
    Me.pnAction.NTSActiveTrasparency = True
    Me.pnAction.Size = New System.Drawing.Size(110, 260)
    Me.pnAction.TabIndex = 3
    '
    'cmdLastfilter
    '
    Me.cmdLastfilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdLastfilter.ImagePath = ""
    Me.cmdLastfilter.ImageText = ""
    Me.cmdLastfilter.Location = New System.Drawing.Point(6, 84)
    Me.cmdLastfilter.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdLastfilter.Name = "cmdLastfilter"
    Me.cmdLastfilter.NTSContextMenu = Nothing
    Me.cmdLastfilter.Size = New System.Drawing.Size(97, 24)
    Me.cmdLastfilter.TabIndex = 10
    Me.cmdLastfilter.Text = "Ultime impostaz."
    '
    'cmdEstensioni
    '
    Me.cmdEstensioni.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdEstensioni.ImagePath = ""
    Me.cmdEstensioni.ImageText = ""
    Me.cmdEstensioni.Location = New System.Drawing.Point(6, 140)
    Me.cmdEstensioni.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdEstensioni.Name = "cmdEstensioni"
    Me.cmdEstensioni.NTSContextMenu = Nothing
    Me.cmdEstensioni.Size = New System.Drawing.Size(97, 22)
    Me.cmdEstensioni.TabIndex = 9
    Me.cmdEstensioni.Text = "&Estensioni"
    '
    'ckAnagen
    '
    Me.ckAnagen.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAnagen.Location = New System.Drawing.Point(10, 214)
    Me.ckAnagen.Name = "ckAnagen"
    Me.ckAnagen.NTSCheckValue = "S"
    Me.ckAnagen.NTSUnCheckValue = "N"
    Me.ckAnagen.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAnagen.Properties.Appearance.Options.UseBackColor = True
    Me.ckAnagen.Properties.AutoHeight = False
    Me.ckAnagen.Properties.Caption = "&Vis Anag.gen."
    Me.ckAnagen.Size = New System.Drawing.Size(92, 19)
    Me.ckAnagen.TabIndex = 8
    '
    'cmdEscludi
    '
    Me.cmdEscludi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdEscludi.ImagePath = ""
    Me.cmdEscludi.ImageText = ""
    Me.cmdEscludi.Location = New System.Drawing.Point(6, 191)
    Me.cmdEscludi.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdEscludi.Name = "cmdEscludi"
    Me.cmdEscludi.NTSContextMenu = Nothing
    Me.cmdEscludi.Size = New System.Drawing.Size(97, 22)
    Me.cmdEscludi.TabIndex = 7
    Me.cmdEscludi.Text = "&Escludi conti"
    '
    'cmdNuovoPdc
    '
    Me.cmdNuovoPdc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdNuovoPdc.ImagePath = ""
    Me.cmdNuovoPdc.ImageText = ""
    Me.cmdNuovoPdc.Location = New System.Drawing.Point(6, 165)
    Me.cmdNuovoPdc.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdNuovoPdc.Name = "cmdNuovoPdc"
    Me.cmdNuovoPdc.NTSContextMenu = Nothing
    Me.cmdNuovoPdc.Size = New System.Drawing.Size(97, 22)
    Me.cmdNuovoPdc.TabIndex = 6
    Me.cmdNuovoPdc.Text = "&Nuovo"
    '
    'ckOttimistico
    '
    Me.ckOttimistico.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckOttimistico.Location = New System.Drawing.Point(10, 237)
    Me.ckOttimistico.Name = "ckOttimistico"
    Me.ckOttimistico.NTSCheckValue = "S"
    Me.ckOttimistico.NTSUnCheckValue = "N"
    Me.ckOttimistico.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckOttimistico.Properties.Appearance.Options.UseBackColor = True
    Me.ckOttimistico.Properties.AutoHeight = False
    Me.ckOttimistico.Properties.Caption = "&Ottimistico"
    Me.ckOttimistico.Size = New System.Drawing.Size(74, 19)
    Me.ckOttimistico.TabIndex = 4
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(6, 58)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(97, 22)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "Ann&ulla"
    '
    'cmdGestione
    '
    Me.cmdGestione.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdGestione.ImagePath = ""
    Me.cmdGestione.ImageText = ""
    Me.cmdGestione.Location = New System.Drawing.Point(6, 115)
    Me.cmdGestione.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdGestione.Name = "cmdGestione"
    Me.cmdGestione.NTSContextMenu = Nothing
    Me.cmdGestione.Size = New System.Drawing.Size(97, 22)
    Me.cmdGestione.TabIndex = 2
    Me.cmdGestione.Text = "&Gestione"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdSeleziona.ImagePath = ""
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(6, 33)
    Me.cmdSeleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(97, 22)
    Me.cmdSeleziona.TabIndex = 1
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'cmdRicerca
    '
    Me.cmdRicerca.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdRicerca.ImagePath = ""
    Me.cmdRicerca.ImageText = ""
    Me.cmdRicerca.Location = New System.Drawing.Point(6, 8)
    Me.cmdRicerca.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.NTSContextMenu = Nothing
    Me.cmdRicerca.Size = New System.Drawing.Size(97, 22)
    Me.cmdRicerca.TabIndex = 0
    Me.cmdRicerca.Text = "&Ricerca"
    '
    'grZoom
    '
    Me.grZoom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grZoom.EmbeddedNavigator.Name = ""
    Me.grZoom.Location = New System.Drawing.Point(0, 260)
    Me.grZoom.MainView = Me.grvZoom
    Me.grZoom.Name = "grZoom"
    Me.grZoom.Size = New System.Drawing.Size(788, 226)
    Me.grZoom.TabIndex = 2
    Me.grZoom.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvZoom})
    '
    'grvZoom
    '
    Me.grvZoom.ActiveFilterEnabled = False
    Me.grvZoom.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.an_conto, Me.an_descr1, Me.an_descr2, Me.an_citta, Me.an_telef, Me.an_faxtlx, Me.an_pariva, Me.an_codfis, Me.an_contatt, Me.an_flci, Me.an_accperi})
    Me.grvZoom.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvZoom.Enabled = True
    Me.grvZoom.GridControl = Me.grZoom
    Me.grvZoom.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvZoom.MinRowHeight = 14
    Me.grvZoom.Name = "grvZoom"
    Me.grvZoom.NTSAllowDelete = True
    Me.grvZoom.NTSAllowInsert = True
    Me.grvZoom.NTSAllowUpdate = True
    Me.grvZoom.NTSMenuContext = Nothing
    Me.grvZoom.OptionsCustomization.AllowRowSizing = True
    Me.grvZoom.OptionsFilter.AllowFilterEditor = False
    Me.grvZoom.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvZoom.OptionsNavigation.UseTabKey = False
    Me.grvZoom.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvZoom.OptionsView.ColumnAutoWidth = False
    Me.grvZoom.OptionsView.EnableAppearanceEvenRow = True
    Me.grvZoom.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvZoom.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvZoom.OptionsView.ShowGroupPanel = False
    Me.grvZoom.RowHeight = 16
    '
    'an_conto
    '
    Me.an_conto.AppearanceCell.Options.UseBackColor = True
    Me.an_conto.AppearanceCell.Options.UseTextOptions = True
    Me.an_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_conto.Caption = "Codice"
    Me.an_conto.Enabled = True
    Me.an_conto.FieldName = "an_conto"
    Me.an_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_conto.Name = "an_conto"
    Me.an_conto.NTSRepositoryComboBox = Nothing
    Me.an_conto.NTSRepositoryItemCheck = Nothing
    Me.an_conto.NTSRepositoryItemMemo = Nothing
    Me.an_conto.NTSRepositoryItemText = Nothing
    Me.an_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.an_conto.OptionsFilter.AllowFilter = False
    Me.an_conto.Visible = True
    Me.an_conto.VisibleIndex = 0
    Me.an_conto.Width = 67
    '
    'an_descr1
    '
    Me.an_descr1.AppearanceCell.Options.UseBackColor = True
    Me.an_descr1.AppearanceCell.Options.UseTextOptions = True
    Me.an_descr1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_descr1.Caption = "Rag. sociale/Descrizione"
    Me.an_descr1.Enabled = True
    Me.an_descr1.FieldName = "an_descr1"
    Me.an_descr1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_descr1.Name = "an_descr1"
    Me.an_descr1.NTSRepositoryComboBox = Nothing
    Me.an_descr1.NTSRepositoryItemCheck = Nothing
    Me.an_descr1.NTSRepositoryItemMemo = Nothing
    Me.an_descr1.NTSRepositoryItemText = Nothing
    Me.an_descr1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.an_descr1.OptionsFilter.AllowFilter = False
    Me.an_descr1.Visible = True
    Me.an_descr1.VisibleIndex = 1
    Me.an_descr1.Width = 220
    '
    'an_descr2
    '
    Me.an_descr2.AppearanceCell.Options.UseBackColor = True
    Me.an_descr2.AppearanceCell.Options.UseTextOptions = True
    Me.an_descr2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_descr2.Caption = "Rag. sociale 2/Classe"
    Me.an_descr2.Enabled = True
    Me.an_descr2.FieldName = "an_descr2"
    Me.an_descr2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_descr2.Name = "an_descr2"
    Me.an_descr2.NTSRepositoryComboBox = Nothing
    Me.an_descr2.NTSRepositoryItemCheck = Nothing
    Me.an_descr2.NTSRepositoryItemMemo = Nothing
    Me.an_descr2.NTSRepositoryItemText = Nothing
    Me.an_descr2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.an_descr2.OptionsFilter.AllowFilter = False
    Me.an_descr2.Visible = True
    Me.an_descr2.VisibleIndex = 2
    Me.an_descr2.Width = 177
    '
    'an_citta
    '
    Me.an_citta.AppearanceCell.Options.UseBackColor = True
    Me.an_citta.AppearanceCell.Options.UseTextOptions = True
    Me.an_citta.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_citta.Caption = "Città/Mastro"
    Me.an_citta.Enabled = True
    Me.an_citta.FieldName = "an_citta"
    Me.an_citta.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_citta.Name = "an_citta"
    Me.an_citta.NTSRepositoryComboBox = Nothing
    Me.an_citta.NTSRepositoryItemCheck = Nothing
    Me.an_citta.NTSRepositoryItemMemo = Nothing
    Me.an_citta.NTSRepositoryItemText = Nothing
    Me.an_citta.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.an_citta.OptionsFilter.AllowFilter = False
    Me.an_citta.Visible = True
    Me.an_citta.VisibleIndex = 3
    Me.an_citta.Width = 126
    '
    'an_telef
    '
    Me.an_telef.AppearanceCell.Options.UseBackColor = True
    Me.an_telef.AppearanceCell.Options.UseTextOptions = True
    Me.an_telef.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_telef.Caption = "Telefono"
    Me.an_telef.Enabled = True
    Me.an_telef.FieldName = "an_telef"
    Me.an_telef.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_telef.Name = "an_telef"
    Me.an_telef.NTSRepositoryComboBox = Nothing
    Me.an_telef.NTSRepositoryItemCheck = Nothing
    Me.an_telef.NTSRepositoryItemMemo = Nothing
    Me.an_telef.NTSRepositoryItemText = Nothing
    Me.an_telef.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.an_telef.OptionsFilter.AllowFilter = False
    Me.an_telef.Visible = True
    Me.an_telef.VisibleIndex = 4
    Me.an_telef.Width = 119
    '
    'an_faxtlx
    '
    Me.an_faxtlx.AppearanceCell.Options.UseBackColor = True
    Me.an_faxtlx.AppearanceCell.Options.UseTextOptions = True
    Me.an_faxtlx.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_faxtlx.Caption = "Fax"
    Me.an_faxtlx.Enabled = True
    Me.an_faxtlx.FieldName = "an_faxtlx"
    Me.an_faxtlx.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_faxtlx.Name = "an_faxtlx"
    Me.an_faxtlx.NTSRepositoryComboBox = Nothing
    Me.an_faxtlx.NTSRepositoryItemCheck = Nothing
    Me.an_faxtlx.NTSRepositoryItemMemo = Nothing
    Me.an_faxtlx.NTSRepositoryItemText = Nothing
    Me.an_faxtlx.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.an_faxtlx.OptionsFilter.AllowFilter = False
    Me.an_faxtlx.Visible = True
    Me.an_faxtlx.VisibleIndex = 5
    Me.an_faxtlx.Width = 134
    '
    'an_pariva
    '
    Me.an_pariva.AppearanceCell.Options.UseBackColor = True
    Me.an_pariva.AppearanceCell.Options.UseTextOptions = True
    Me.an_pariva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_pariva.Caption = "Partita IVA"
    Me.an_pariva.Enabled = True
    Me.an_pariva.FieldName = "an_pariva"
    Me.an_pariva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_pariva.Name = "an_pariva"
    Me.an_pariva.NTSRepositoryComboBox = Nothing
    Me.an_pariva.NTSRepositoryItemCheck = Nothing
    Me.an_pariva.NTSRepositoryItemMemo = Nothing
    Me.an_pariva.NTSRepositoryItemText = Nothing
    Me.an_pariva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.an_pariva.OptionsFilter.AllowFilter = False
    Me.an_pariva.Visible = True
    Me.an_pariva.VisibleIndex = 6
    Me.an_pariva.Width = 120
    '
    'an_codfis
    '
    Me.an_codfis.AppearanceCell.Options.UseBackColor = True
    Me.an_codfis.AppearanceCell.Options.UseTextOptions = True
    Me.an_codfis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_codfis.Caption = "Codice fiscale"
    Me.an_codfis.Enabled = True
    Me.an_codfis.FieldName = "an_codfis"
    Me.an_codfis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_codfis.Name = "an_codfis"
    Me.an_codfis.NTSRepositoryComboBox = Nothing
    Me.an_codfis.NTSRepositoryItemCheck = Nothing
    Me.an_codfis.NTSRepositoryItemMemo = Nothing
    Me.an_codfis.NTSRepositoryItemText = Nothing
    Me.an_codfis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.an_codfis.OptionsFilter.AllowFilter = False
    Me.an_codfis.Visible = True
    Me.an_codfis.VisibleIndex = 7
    Me.an_codfis.Width = 115
    '
    'an_contatt
    '
    Me.an_contatt.AppearanceCell.Options.UseBackColor = True
    Me.an_contatt.AppearanceCell.Options.UseTextOptions = True
    Me.an_contatt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_contatt.Caption = "Contatto"
    Me.an_contatt.Enabled = True
    Me.an_contatt.FieldName = "an_contatt"
    Me.an_contatt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_contatt.Name = "an_contatt"
    Me.an_contatt.NTSRepositoryComboBox = Nothing
    Me.an_contatt.NTSRepositoryItemCheck = Nothing
    Me.an_contatt.NTSRepositoryItemMemo = Nothing
    Me.an_contatt.NTSRepositoryItemText = Nothing
    Me.an_contatt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.an_contatt.OptionsFilter.AllowFilter = False
    Me.an_contatt.Visible = True
    Me.an_contatt.VisibleIndex = 8
    Me.an_contatt.Width = 146
    '
    'an_flci
    '
    Me.an_flci.AppearanceCell.Options.UseBackColor = True
    Me.an_flci.AppearanceCell.Options.UseTextOptions = True
    Me.an_flci.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_flci.Caption = "Gestione CA"
    Me.an_flci.Enabled = True
    Me.an_flci.FieldName = "an_flci"
    Me.an_flci.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_flci.Name = "an_flci"
    Me.an_flci.NTSRepositoryComboBox = Nothing
    Me.an_flci.NTSRepositoryItemCheck = Nothing
    Me.an_flci.NTSRepositoryItemMemo = Nothing
    Me.an_flci.NTSRepositoryItemText = Nothing
    Me.an_flci.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.an_flci.OptionsFilter.AllowFilter = False
    Me.an_flci.Visible = True
    Me.an_flci.VisibleIndex = 9
    '
    'an_accperi
    '
    Me.an_accperi.AppearanceCell.Options.UseBackColor = True
    Me.an_accperi.AppearanceCell.Options.UseTextOptions = True
    Me.an_accperi.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.an_accperi.Caption = "Richiedi date"
    Me.an_accperi.Enabled = True
    Me.an_accperi.FieldName = "an_accperi"
    Me.an_accperi.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.an_accperi.Name = "an_accperi"
    Me.an_accperi.NTSRepositoryComboBox = Nothing
    Me.an_accperi.NTSRepositoryItemCheck = Nothing
    Me.an_accperi.NTSRepositoryItemMemo = Nothing
    Me.an_accperi.NTSRepositoryItemText = Nothing
    Me.an_accperi.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.an_accperi.OptionsFilter.AllowFilter = False
    Me.an_accperi.Visible = True
    Me.an_accperi.VisibleIndex = 10
    '
    'FRM__HLAN
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.ClientSize = New System.Drawing.Size(788, 486)
    Me.Controls.Add(Me.grZoom)
    Me.Controls.Add(Me.pnDescr)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRM__HLAN"
    Me.NTSLastControlFocussed = Me.grZoom
    Me.Text = "ZOOM CLIENTI / FORNITORI / SOTTOCONTI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDescr.ResumeLayout(False)
    CType(Me.tsZoom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsZoom.ResumeLayout(False)
    Me.TabPage1.ResumeLayout(False)
    CType(Me.pnTab1Pan2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab1Pan2.ResumeLayout(False)
    Me.pnTab1Pan2.PerformLayout()
    CType(Me.edFax.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMastro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmTipo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmTipo.ResumeLayout(False)
    Me.fmTipo.PerformLayout()
    CType(Me.ckAbituali.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbPrivato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSoloSemplificata.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbSottc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optSottoconti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optFornitori.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optClienti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTelef.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTab1Pan1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab1Pan1.ResumeLayout(False)
    Me.pnTab1Pan1.PerformLayout()
    CType(Me.edDescr.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEmail.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edComune.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSiglaric.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodfisc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPariva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescr2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage2.ResumeLayout(False)
    CType(Me.pnTab2Pan2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab2Pan2.ResumeLayout(False)
    Me.pnTab2Pan2.PerformLayout()
    CType(Me.edEsenA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edEsenDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edValutaA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edValutaDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDtAggA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDtAperturaA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDtAggDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDtAperturaDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListinoA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListinoDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCanaleA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCanaleDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRagsocA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRagsocDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTab2Pan1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab2Pan1.ResumeLayout(False)
    Me.pnTab2Pan1.PerformLayout()
    CType(Me.edProvinciaA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPagamA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAgenteA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCategA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edPagamDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAgenteDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCategDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edZonaA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edZonaDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCapA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCapDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edProvinciaDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage3.ResumeLayout(False)
    CType(Me.pnTab3Pan1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab3Pan1.ResumeLayout(False)
    Me.pnTab3Pan1.PerformLayout()
    CType(Me.liUsaem, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edStato.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liBlocco, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLinguaA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edLinguaDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSconA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSconDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edProvA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edProvDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTab3Pan2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab3Pan2.ResumeLayout(False)
    Me.pnTab3Pan2.PerformLayout()
    CType(Me.liPariva, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liGgcons, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liFatturaz, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liStatus, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liPrivacy, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAction.ResumeLayout(False)
    Me.pnAction.PerformLayout()
    CType(Me.ckAnagen.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckOttimistico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Eventi di form"
  Public Overridable Sub FRM__HLAN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Dim bDisableCRM As Boolean = False       'se true anche se c'è il modulo CRM non viene testato (usato, ad esempio, da BNDKKONS - programma non utilizzabile da utenti crm)

    Try
      '-------------------------------------------------
      'carico i listbox 
      CaricaListBox()

      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-----------------------------------------------------------------------------------------
      'carico il combo dei tipi conto
      Dim dttTipo As New DataTable()
      dttTipo.Columns.Add("cod", GetType(String))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {" ", "Tutti"})
      dttTipo.Rows.Add(New Object() {"P", "Patrimoniali"})
      dttTipo.Rows.Add(New Object() {"E", "Economici"})
      dttTipo.Rows.Add(New Object() {"A", "Attività"})
      dttTipo.Rows.Add(New Object() {"Q", "Passività"})
      dttTipo.Rows.Add(New Object() {"K", "Costi"})
      dttTipo.Rows.Add(New Object() {"R", "Ricavi"})
      dttTipo.Rows.Add(New Object() {"O", "Conti d'ordine"})
      dttTipo.Rows.Add(New Object() {"Z", "Conti riepilogativi"})
      dttTipo.AcceptChanges()
      cbSottc.DataSource = dttTipo
      cbSottc.ValueMember = "cod"
      cbSottc.DisplayMember = "val"
      cbSottc.SelectAll()

      '-----------------------------------------------------------------------------------------
      'carico il combo dei tipi clienti
      Dim dttPrivato As New DataTable()
      dttPrivato.Columns.Add("cod", GetType(String))
      dttPrivato.Columns.Add("val", GetType(String))
      dttPrivato.Rows.Add(New Object() {" ", "Tutti"})
      dttPrivato.Rows.Add(New Object() {"N", "Aziende"})
      dttPrivato.Rows.Add(New Object() {"S", "Privati"})
      dttPrivato.AcceptChanges()
      cbPrivato.DataSource = dttPrivato
      cbPrivato.ValueMember = "cod"
      cbPrivato.DisplayMember = "val"

      'disabilito il fatto che premendo ENTER venga emulata la pressione del tasto TAB
      'per facilitare la ricerca: premendo enter parte la ricerca
      'Me.NTSDisableEnterComeTab()

      '-------------------------------------------------
      'tratto i parametri ricevuti in input dal child
      bDisableCRM = oParam.bLiv2
      If oParam.strTipo.Trim = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127985496979375000, "Zoom Clienti/fornitori/sottoconti chiamato in modo non corretto: non è stato passato il tipo di conto che deve essere cercato!"))
        oParam.strTipo = "S"
      End If
      If oParam.strTipo = "C" Then
        optClienti.Checked = True
      ElseIf oParam.strTipo = "F" Then
        optFornitori.Checked = True
      ElseIf oParam.strTipo = "CF" Then
        optClienti.Checked = True
        optSottoconti.Enabled = False
      ElseIf oParam.strTipo = "FC" Then
        optFornitori.Checked = True
        optSottoconti.Enabled = False
      Else
        optSottoconti.Checked = True
        If oParam.strTipo <> "S" Then cbSottc.SelectedValue = oParam.strTipo
      End If

      If oParam.bTipoProposto = False Then
        optClienti.Enabled = False
        optFornitori.Enabled = False
        optSottoconti.Enabled = False
        cbSottc.Enabled = False
        cmdReset.Enabled = False
      End If
      If oParam.nMastro <> 0 Then edMastro.NTSTextDB = oParam.nMastro.ToString

      For i = 0 To liGgcons.ItemCount - 1
        liGgcons.SetSelected(i, True)
      Next
      For i = 0 To liFatturaz.ItemCount - 1
        liFatturaz.SetSelected(i, True)
      Next

      GctlSetRoules()
      GctlApplicaDefaultValue()
      
      If oParam.bVisGriglia = False Then
        GctlSetVisEnab(cmdLastfilter, True)
        cmdRicerca.Text = oApp.Tr(Me, 128366686855384000, "Conferma")
        grZoom.Visible = False
        cmdEscludi.Visible = False
        cmdNuovoPdc.Visible = False
        cmdGestione.Visible = False
        cmdSeleziona.Visible = False
        Me.Size = Me.MinimumSize
        Me.MinimumSize = New Size(0, 0)
        Me.Height -= grZoom.Height
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.ClientSize = New Size(Me.Width, tsZoom.Height)
        'ckOttimistico.Checked = False
        'ckOttimistico.Enabled = False
        ckAnagen.Checked = False
        ckAnagen.Enabled = False
        ckSoloSemplificata.Checked = False
        ckSoloSemplificata.Enabled = False
        cbSottc.Enabled = False
        cbSottc.SelectedValue = " "
        ckAbituali.Checked = False
        ckAbituali.Enabled = False
        ckAbituali.Visible = False
        Me.MinimumSize = Me.Size
      Else
        If bSemplificata Then ckSoloSemplificata.Checked = True
        cmdReset.Visible = False
        cmdLastfilter.Visible = False
        ckAbituali.Checked = CBool(oMenu.GetSettingBus("BN__HLAN", "RECENT", ".", "ClientiFornitoriAbituali", "0", " ", "0"))
        'If (ckAbituali.Checked = False) And (CLN__STD.IsBis = True) Then ckAbituali.Checked = True
      End If

      If optSottoconti.Checked = False Then
        cbSottc.Enabled = False
        ckSoloSemplificata.Enabled = False
      End If

      bModAnagen = False
      If CBool(oApp.ActKey.ModuliExtAzienda And CLN__STD.bsModExtANG) = True Then bModAnagen = True

      '---------------------------------------------------
      'disabilito le anagrafiche generali se non ho il modulo
      If bModAnagen = False Then
        ckAnagen.Checked = False
        ckAnagen.Enabled = False
      End If

      '------------------------------------------------
      'CRM: se l'operatore non è stato codificato e non ha un ruolo non può operare
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtCRM) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then bModuloCRM = True

      If bDisableCRM Then bModuloCRM = False

      If bModuloCRM Then
        bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, bAmm, strAccvis, strAccmod, strRegvis, strRegmod)
        If bIsCRMUser Then
          lCodorgaOperat = oMenu.RitornaCodorgaDaOpnome(DittaCorrente, nCodcageoperat)
          If lCodorgaOperat = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 127791222142500000, "Attenzione!" & vbCrLf & "L'operatore '|" & oApp.User.Nome & _
                 "|' (CRM) non è associato all'organizzazione della ditta corrente '|" & DittaCorrente & "|'." & vbCrLf & _
                 "Impossibile continuare."))
            Me.Close()
            Return
          End If

          '------------------------------------------------
          ' VEDE SE L'OPZIONE CHE DEROGA SUGLI ZOOM FORNITORI è ATTIVATO
          If bAmm = False Then
            If CBool(oMenu.GetSettingBus("BS--HLAN", "OPZIONIUT", ".", "ConsentiZoomFornitori", "0", " ", "0")) = True Then bAmm = True
          End If
        End If
      End If    ' If bModuloCRM Then

      'Legge l'opzione di registro per vedere se ci sono filtri agente da applicare
      'impostare l'opzione con cod. agente per far si che vengano visualizzati solo i clienti/forn. associati all'agente impostato (la cartella \OpzioniUt deve essere dipendente da operatore)
      nTrattaSoloCliFornDellAgente = NTSCInt(oMenu.GetSettingBus("BS--HLAN", "OpzioniUt", ".", "TrattaSoloCliFornDellAgente", "0", " ", "0"))

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__HLAN_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim i As Integer = 0
    Dim dtrT As DataRow

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se richiesto dal child se la ricerca trova un solo risultato lo seleziona subito.
      '--------------------------------------------------------------------------------------------------------------
      bSelectIfOneRow = oParam.bFlag1
      '--------------------------------------------------------------------------------------------------------------
      If bSelectIfOneRow = True Then
        edDescr.Text = oParam.strDescr
        nFocusIniziale = 4
        cmdRicerca_Click(Me, Nothing)
      Else
        '------------------------------------------------------------------------------------------------------------
        '--- Se ho ricevuto un valore in ingresso, lo tratto
        '------------------------------------------------------------------------------------------------------------
        TrattaValoreInInput(oParam.strIn)
        If oParam.strIn.Trim <> "" Then
          If oMenu.GetSettingBus("BS--HLAN", "OPZIONI", ".", "RicercaSuInput", "N", " ", "N").ToUpper = "S" Then
            cmdRicerca_Click(Me, Nothing)
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Dopo la prima ricerca lanciata dal clienti con la ',' finale
      '--- faccio in modo che il flag 'ottimistico' torni a funzionare
      '--------------------------------------------------------------------------------------------------------------
      bSelectIfOneRow = False
      '--------------------------------------------------------------------------------------------------------------
      '--- Utilizza il parametro oParam.strAlfpar per leggere il nome dell'eventuale programma chiamante
      '--------------------------------------------------------------------------------------------------------------
      strProgrChiamante = oParam.strAlfpar.ToUpper.Trim
      '--------------------------------------------------------------------------------------------------------------
      Select Case strProgrChiamante
        Case "BN__ISTF"
          cmdLastfilter.Enabled = False
          GctlSetVisEnab(cmdReset, False)
          ckOttimistico.Checked = False
          ckOttimistico.Enabled = False
          GctlSetVisEnab(optClienti, False)
          GctlSetVisEnab(optFornitori, False)
          GctlSetVisEnab(optSottoconti, False)
          GctlSetVisEnab(cbPrivato, False)
          GctlSetVisEnab(cbSottc, False)
          dtrT = oParam.rFiltriAnagra
          Select Case NTSCStr(dtrT!pf_1tipo)
            Case "C" : optClienti.Checked = True
            Case "F" : optFornitori.Checked = True
            Case "S" : optSottoconti.Checked = True
            Case Else : cmdReset_Click(Me, Nothing)
          End Select
          With dtrT
            edContoDa.Text = NTSCStr(!pf_contoini)
            edContoA.Text = IIf(NTSCInt(!pf_contofin) = 0, 999999999, !pf_contofin).ToString
            edProvinciaDa.Text = NTSCStr(!pf_provini)
            edProvinciaA.Text = IIf(NTSCStr(!pf_provfin) = "", "".PadLeft(2, "Z"c), NTSCStr(!pf_provfin)).ToString
            edCapDa.Text = NTSCStr(!pf_capini)
            edCapA.Text = IIf(NTSCStr(!pf_capfin) = "", "".PadLeft(5, "Z"c), NTSCStr(!pf_capfin)).ToString
            edZonaDa.Text = NTSCStr(!pf_zonaini)
            edZonaA.Text = IIf(NTSCInt(!pf_zonafin) = 0, "".PadLeft(3, "9"c), !pf_zonafin).ToString
            edCategDa.Text = NTSCStr(!pf_categini)
            edCategA.Text = IIf(NTSCInt(!pf_categfin) = 0, "".PadLeft(3, "9"c), !pf_categfin).ToString
            edAgenteDa.Text = NTSCStr(!pf_agenteini)
            edAgenteA.Text = IIf(NTSCInt(!pf_agentefin) = 0, "".PadLeft(4, "9"c), !pf_agentefin).ToString
            edPagamDa.Text = NTSCStr(!pf_codpagini)
            edPagamA.Text = IIf(NTSCInt(!pf_codpagfin) = 0, "".PadLeft(3, "9"c), !pf_codpagfin).ToString
            edRagsocDa.Text = NTSCStr(!pf_ragd)
            edRagsocDa.Text = IIf(NTSCStr(!pf_ragd) = "", "".PadLeft(30, "Z"c), NTSCStr(!pf_ragd)).ToString
            edDtAperturaDa.Text = IIf(NTSCStr(!pf_dtaperini) = "", IntSetDate("01/01/1900"), NTSCStr(!pf_dtaperini)).ToString
            edDtAperturaA.Text = IIf(NTSCStr(!pf_dtaperfin) = "", IntSetDate("31/12/2099"), NTSCStr(!pf_dtaperfin)).ToString
            edDtAggDa.Text = IIf(NTSCStr(!pf_ultaggini) = "", IntSetDate("01/01/1900"), NTSCStr(!pf_ultaggini)).ToString
            edDtAggA.Text = IIf(NTSCStr(!pf_ultaggfin) = "", IntSetDate("31/12/2099"), NTSCStr(!pf_ultaggfin)).ToString
            edStato.Text = NTSCStr(!pf_stato)
            liPariva.SelectedValue = NTSCStr(!pf_testiva)
            edCanaleDa.Text = NTSCStr(!pf_codcand)
            edCanaleA.Text = IIf(NTSCInt(!pf_codcana) = 0, "".PadLeft(3, "9"c), !pf_codcana).ToString
            edListinoDa.Text = NTSCStr(!pf_listd)
            edListinoA.Text = NTSCStr(!pf_lista)
            tsZoom.SelectedTabPageIndex = 2
            liBlocco.SelectionMode = SelectionMode.One
            liPrivacy.SelectionMode = SelectionMode.One
            liStatus.SelectionMode = SelectionMode.One
            liFatturaz.SelectionMode = SelectionMode.One
            liGgcons.SelectionMode = SelectionMode.One
            liBlocco.SelectedValue = NTSCStr(!pf_blocco)
            liPrivacy.SelectedValue = NTSCStr(!pf_privacy)
            liStatus.SelectedValue = NTSCStr(!pf_status)
            liFatturaz.SelectedValue = NTSCStr(!pf_perfatt)
            liGgcons.SelectedValue = NTSCStr(!pf_pcons)
            tsZoom.SelectedTabPageIndex = 1
          End With
          If oParam.strCin.Trim <> "" Then strFiltriDaEsterno = oParam.strCin
        Case Else
          SettaFiltri(strProgrChiamante)
      End Select
      '--------------------------------------------------------------------------------------------------------------      
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__HLAN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

      If e.KeyCode = Keys.Enter Then
        If grvZoom.Focused And grvZoom.RowCount > 0 Then
          e.Handled = True
          cmdSeleziona_Click(Me, Nothing)
        Else
          'If edDescr.Focused Or edDescr2.Focused Or edSiglaric.Focused Or edPariva.Focused Or edCodfisc.Focused Then
          'cmdRicerca.Focus()      'mi sposto sul comando ricerca per validare il controllo su cui ero posizionato
          If Not Me.frmAuto.bSelezionato Then
            cmdRicerca_Click(Me, Nothing)
          Else
            Me.frmAuto.bSelezionato = False
          End If

          'End If
        End If
      End If

      If (e.KeyValue > 40 And e.KeyValue < 112) And (e.KeyValue <> 91) And (e.KeyValue <> 93) _
        And e.Alt = False And e.Control = False And e.Shift = False And grvZoom.Focused = True Then
        Select Case nFocusIniziale
          Case 1, 4 : If edDescr.Enabled And edDescr.Visible Then ctrlTmp = edDescr
          Case 2, 5 : If edSiglaric.Enabled And edSiglaric.Visible Then ctrlTmp = edSiglaric
          Case 3 : If edPariva.Enabled And edPariva.Visible Then ctrlTmp = edPariva
          Case 6 : If edCodfisc.Enabled And edCodfisc.Visible Then ctrlTmp = edCodfisc
        End Select
        If Not ctrlTmp Is Nothing Then
          ctrlTmp.Focus()
          CType(ctrlTmp, NTSTextBox).SelectAll()
          If e.KeyValue >= 96 And e.KeyValue <= 105 Then
            ctrlTmp.Text = e.KeyCode.ToString.Substring(6)
          Else
            ctrlTmp.Text = e.KeyCode.ToString
          End If
          CType(ctrlTmp, NTSTextBox).SelectionStart = CType(ctrlTmp, NTSTextBox).Text.Length
          e.Handled = True        'se non metto questa riga dopo aver compilato edDescr intercetta nuovamente la KeyDown e fa partire subito la ricerca ...
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__HLAN_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    '-------------------------------------------------
    'salvo il recent
    If ckOttimistico.Checked Then
      oMenu.SaveSettingBus("BN__HLAN", "RECENT", ".", "Ottimistico", "-1", " ", "NS.", "...", "...")
    Else
      oMenu.SaveSettingBus("BN__HLAN", "RECENT", ".", "Ottimistico", "0", " ", "NS.", "...", "...")
    End If
    If oParam.bVisGriglia = False Then Return
    If bModAnagen = True Then
      If ckAnagen.Checked Then
        oMenu.SaveSettingBus("BN__HLAN", "RECENT", ".", "Anagen", "-1", " ", "NS.", "...", "...")
      Else
        oMenu.SaveSettingBus("BN__HLAN", "RECENT", ".", "Anagen", "0", " ", "NS.", "...", "...")
      End If
    End If
  End Sub
#End Region

#Region "CommandButton"
  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Dim strQuery As String = ""
    Dim strRpt As String = ""
    Dim strQueryAnagen As String = ""
    Dim strRptAnagen As String = ""
    Dim strTipoConto As String = "S"
    Dim strTmp As String = ""
    Dim i As Integer

    Try
      Me.ValidaLastControl()

      bEseguitaRicerca = True
      If optClienti.Checked Then strTipoConto = "C"
      If optFornitori.Checked Then strTipoConto = "F"
      If optSottoconti.Checked Then strTipoConto = "S"

      'Applica il filtro
      If nTrattaSoloCliFornDellAgente > 0 And strTipoConto <> "S" Then
        strQuery += "an_agente = " & nTrattaSoloCliFornDellAgente.ToString & "§"
        strRpt += "{anagra.an_agente} = " & nTrattaSoloCliFornDellAgente.ToString & "§"
      End If

      '------------------------------------------------
      'ATTENZIONE: nel TAG di ogni controllo deve essere inserito l'operatore che deve essere utilizzato nella query 
      If bModuloCRM = True And bIsCRMUser = True And optClienti.Checked = True Then
        strTmp = oCleHlan.RitornaSubQueryFiltroLeadVis.Trim
        If strTmp <> "" Then
          strQuery += "le_codlead IN " & strTmp & "§"
          strRpt += "{leads.le_codlead} IN " & strTmp.Replace("(", "[").Replace(")", "]") & "§"
        End If
      End If
      If edDescr.Text <> "" Then
        If edDescr.Text.Contains(".") = False And edDescr.Text.Contains(" ") = False Then
          strQuery += "REPLACE(REPLACE(an_descr1, ' ', ''), '.', '') like " & CampoTesto(edDescr.Text, True) & "§"
          strRpt += "REPLACE(REPLACE({anagra.an_descr1}, ' ', ''), '.', '') like " & CampoTestoRpt(edDescr.Text, True) & "§"
        Else
          strQuery += "an_descr1 like " & CampoTesto(edDescr.Text, True) & "§"
          strRpt += "{anagra.an_descr1} like " & CampoTestoRpt(edDescr.Text, True) & "§"
        End If
      End If
      If edSiglaric.Text <> "" Then
        If edSiglaric.Text.Contains(".") = False And edSiglaric.Text.Contains(" ") = False Then
          strQuery += "REPLACE(REPLACE(an_siglaric, ' ', ''), '.', '') like " & CampoTesto(edSiglaric.Text, True) & "§"
          strRpt += "REPLACE(REPLACE({anagra.an_siglaric}, ' ', ''), '.', '') like " & CampoTestoRpt(edSiglaric.Text, True) & "§"
        Else
          strQuery += "an_siglaric like " & CampoTesto(edSiglaric.Text, True) & "§"
          strRpt += "{anagra.an_siglaric} like " & CampoTestoRpt(edSiglaric.Text, True) & "§"
        End If
      End If
      If edPariva.Text <> "" Then
        strQuery += "an_pariva like " & CampoTesto(edPariva.Text, True) & "§"
        strRpt += "{anagra.an_pariva} like " & CampoTestoRpt(edPariva.Text, True) & "§"
      End If
      If edCodfisc.Text <> "" Then
        strQuery += "an_codfis like " & CampoTesto(edCodfisc.Text, True) & "§"
        strRpt += "{anagra.an_codfis} like " & CampoTestoRpt(edCodfisc.Text, True) & "§"
      End If
      If edEmail.Text <> "" Then
        strQuery += "an_email like " & CampoTesto(edEmail.Text, True) & "§"
        strRpt += "{anagra.an_email} like " & CampoTestoRpt(edEmail.Text, True) & "§"
      End If
      If edDescr2.Text <> "" Then
        strQuery += "an_descr2 like " & CampoTesto(edDescr2.Text, True) & "§"
        strRpt += "{anagra.an_descr2} like " & CampoTestoRpt(edDescr2.Text, True) & "§"
      End If
      If edComune.Text <> "" Then
        strQuery += "an_citta like " & CampoTesto(edComune.Text, True) & "§"
        strRpt += "{anagra.an_citta} like " & CampoTestoRpt(edComune.Text, True) & "§"
      End If
      If edTelef.Text <> "" Then
        strQuery += "an_telef like " & CampoTesto(edTelef.Text, True) & "§"
        strRpt += "{anagra.an_telef} like " & CampoTestoRpt(edTelef.Text, True) & "§"
      End If
      If edFax.Text <> "" Then
        strQuery += "an_faxtlx like " & CampoTesto(edFax.Text, True) & "§"
        strRpt += "{anagra.an_faxtlx} like " & CampoTestoRpt(edFax.Text, True) & "§"
      End If
      If edMastro.Text <> "0" Then
        strQuery += "an_codmast = " & edMastro.Text & "§"
        strRpt += "{anagra.an_codmast} = " & edMastro.Text & "§"
      End If
      If optClienti.Checked Then
        strQuery += "an_tipo = 'C'§"
        strRpt += "{anagra.an_tipo} = 'C'§"
      End If
      If optFornitori.Checked Then
        strQuery += "an_tipo = 'F'§"
        strRpt += "{anagra.an_tipo} = 'F'§"
      End If
      If optSottoconti.Checked Then
        strQuery += "an_tipo = 'S'§"
        strRpt += "{anagra.an_tipo} = 'S'§"
      End If
      If cbPrivato.SelectedValue <> " " Then
        strQuery += "an_privato = " & CStrSQL(cbPrivato.SelectedValue) & "§"
        strRpt += "{anagra.an_privato} = " & ConvStrRpt(cbPrivato.SelectedValue) & "§"
      End If
      'campi tab 1
      If edContoDa.Text <> "0" Then
        strQuery += "an_conto >= " & edContoDa.Text & "§"
        strRpt += "{anagra.an_conto} >= " & edContoDa.Text & "§"
      End If
      If edContoA.Text <> "999999999" Then
        strQuery += "an_conto <= " & edContoA.Text & "§"
        strRpt += "{anagra.an_conto} <= " & edContoA.Text & "§"
      End If
      If edProvinciaDa.Text <> "" Then
        strQuery += "an_prov >= " & CampoTesto(edProvinciaDa.Text) & "§"
        strRpt += "{anagra.an_prov} >= " & CampoTestoRpt(edProvinciaDa.Text) & "§"
      End If
      If edProvinciaA.Text <> "ZZ" Then
        strQuery += "an_prov <= " & CampoTesto(edProvinciaA.Text) & "§"
        strRpt += "{anagra.an_prov} <= " & CampoTestoRpt(edProvinciaA.Text) & "§"
      End If
      If edCapDa.Text <> "" Then
        strQuery += "an_cap >= " & CampoTesto(edCapDa.Text) & "§"
        strRpt += "{anagra.an_cap} >= " & CampoTestoRpt(edCapDa.Text) & "§"
      End If
      If edCapA.Text <> "ZZZZZ" Then
        strQuery += "an_cap <= " & CampoTesto(edCapA.Text) & "§"
        strRpt += "{anagra.an_cap} <= " & CampoTestoRpt(edCapA.Text) & "§"
      End If
      If edZonaDa.Text <> "0" Then
        strQuery += "an_zona >= " & edZonaDa.Text & "§"
        strRpt += "{anagra.an_zona} >= " & edZonaDa.Text & "§"
      End If
      If edZonaA.Text <> "999" Then
        strQuery += "an_zona <= " & edZonaA.Text & "§"
        strRpt += "{anagra.an_zona} <= " & edZonaA.Text & "§"
      End If
      If edCategDa.Text <> "0" Then
        strQuery += "an_categ >= " & edCategDa.Text & "§"
        strRpt += "{anagra.an_categ} >= " & edCategDa.Text & "§"
      End If
      If edCategA.Text <> "999" Then
        strQuery += "an_categ <= " & edCategA.Text & "§"
        strRpt += "{anagra.an_categ} <= " & edCategA.Text & "§"
      End If
      If edAgenteDa.Text <> "0" Then
        strQuery += "an_agente >= " & edAgenteDa.Text & "§"
        strRpt += "{anagra.an_agente} >= " & edAgenteDa.Text & "§"
      End If
      If edAgenteA.Text <> "9999" Then
        strQuery += "an_agente <= " & edAgenteA.Text & "§"
        strRpt += "{anagra.an_agente} <= " & edAgenteA.Text & "§"
      End If
      If edPagamDa.Text <> "0" Then
        strQuery += "an_codpag >= " & edPagamDa.Text & "§"
        strRpt += "{anagra.an_codpag} >= " & edPagamDa.Text & "§"
      End If
      If edPagamA.Text <> "999" Then
        strQuery += "an_codpag <= " & edPagamA.Text & "§"
        strRpt += "{anagra.an_codpag} <= " & edPagamA.Text & "§"
      End If
      If edRagsocDa.Text <> "" Then
        strQuery += "an_descr1 >= " & CampoTesto(edRagsocDa.Text) & "§"
        strRpt += "{anagra.an_descr1} >= " & CampoTestoRpt(edRagsocDa.Text) & "§"
      End If
      If edRagsocA.Text <> "".PadLeft(30, "Z"c) Then
        strQuery += "an_descr1 <= " & CampoTesto(edRagsocA.Text) & "§"
        strRpt += "{anagra.an_descr1} <= " & CampoTestoRpt(edRagsocA.Text) & "§"
      End If
      If edEsenDa.Text <> "0" Then
        strQuery += "an_codese >= " & edEsenDa.Text & "§"
        strRpt += "{anagra.an_codese} >= " & edEsenDa.Text & "§"
      End If
      If edEsenA.Text <> "9999" Then
        strQuery += "an_codese <= " & edEsenA.Text & "§"
        strRpt += "{anagra.an_codese} <= " & edEsenA.Text & "§"
      End If
      If edCanaleDa.Text <> "0" Then
        strQuery += "an_codcana >= " & edCanaleDa.Text & "§"
        strRpt += "{anagra.an_codcana} >= " & edCanaleDa.Text & "§"
      End If
      If edCanaleA.Text <> "999" Then
        strQuery += "an_codcana <= " & edCanaleA.Text & "§"
        strRpt += "{anagra.an_codcana} <= " & edCanaleA.Text & "§"
      End If
      If edListinoDa.Text <> "-2" Then
        strQuery += "an_listino >= " & edListinoDa.Text & "§"
        strRpt += "{anagra.an_listino} >= " & edListinoDa.Text & "§"
      End If
      If edListinoA.Text <> "9999" Then
        strQuery += "an_listino <= " & edListinoA.Text & "§"
        strRpt += "{anagra.an_listino} <= " & edListinoA.Text & "§"
      End If
      If edValutaDa.Text <> "0" Then
        strQuery += "an_valuta >= " & edValutaDa.Text & "§"
        strRpt += "{anagra.an_valuta} >= " & edValutaDa.Text & "§"
      End If
      If edValutaA.Text <> "999" Then
        strQuery += "an_valuta <= " & edValutaA.Text & "§"
        strRpt += "{anagra.an_valuta} <= " & edValutaA.Text & "§"
      End If
      If NTSCDate(edDtAperturaDa.Text) <> New Date(1900, 1, 1) Then
        strQuery += "an_dtaper >= " & CDataSQL(edDtAperturaDa.Text) & "§"
        strRpt += "{anagra.an_dtaper} >= " & ConvDataRpt(edDtAperturaDa.Text) & "§"
      End If
      If NTSCDate(edDtAperturaA.Text) <> New Date(2099, 12, 31) Then
        strQuery += "an_dtaper <= " & CDataSQL(edDtAperturaA.Text) & "§"
        strRpt += "{anagra.an_dtaper} <= " & ConvDataRpt(edDtAperturaA.Text) & "§"
      End If
      If NTSCDate(edDtAggDa.Text) <> New Date(1900, 1, 1) Then
        strQuery += "an_ultagg >= " & CDataSQL(edDtAggDa.Text) & "§"
        strRpt += "{anagra.an_ultagg} >= " & ConvDataRpt(edDtAggDa.Text) & "§"
      End If
      If NTSCDate(edDtAggA.Text) <> New Date(2099, 12, 31) Then
        strQuery += "an_ultagg <= " & CDataSQL(edDtAggA.Text) & "§"
        strRpt += "{anagra.an_ultagg} <= " & ConvDataRpt(edDtAggA.Text) & "§"
      End If

      'campi tab 2
      If edProvDa.Text <> "0" Then
        strQuery += "an_claprov >= " & edProvDa.Text & "§"
        strRpt += "{anagra.an_claprov} >= " & edProvDa.Text & "§"
      End If
      If edProvA.Text <> "999" Then
        strQuery += "an_claprov <= " & edProvA.Text & "§"
        strRpt += "{anagra.an_claprov} <= " & edProvA.Text & "§"
      End If
      If edSconDa.Text <> "0" Then
        strQuery += "an_clascon >= " & edSconDa.Text & "§"
        strRpt += "{anagra.an_clascon} >= " & edSconDa.Text & "§"
      End If
      If edSconA.Text <> "999" Then
        strQuery += "an_clascon <= " & edSconA.Text & "§"
        strRpt += "{anagra.an_clascon} <= " & edSconA.Text & "§"
      End If
      If edLinguaDa.Text <> "0" Then
        strQuery += "an_codling >= " & edLinguaDa.Text & "§"
        strRpt += "{anagra.an_codling} >= " & edLinguaDa.Text & "§"
      End If
      If edLinguaA.Text <> "999" Then
        strQuery += "an_codling <= " & edLinguaA.Text & "§"
        strRpt += "{anagra.an_codling} <= " & edLinguaA.Text & "§"
      End If
      If edStato.Text <> "" Then
        strQuery += "an_stato = " & CampoTesto(edStato.Text) & "§"
        strRpt += "{anagra.an_stato} = " & CampoTestoRpt(edStato.Text) & "§"
      End If

      If liUsaem.SelectedIndices.Count > 0 And liUsaem.SelectedIndices.Count <> liUsaem.ItemCount Then
        If liUsaem.GetItemValue(liUsaem.SelectedIndices(0)).ToString <> " " Then
          strQuery += "an_usaem|"
          strRpt += "{anagra.an_usaem}|"
          For i = 0 To liUsaem.SelectedIndices.Count - 1
            strQuery += "=" & CampoTesto(liUsaem.GetItemValue(liUsaem.SelectedIndices(i)).ToString) & "|"
            strRpt += "=" & CampoTestoRpt(liUsaem.GetItemValue(liUsaem.SelectedIndices(i)).ToString) & "|"
          Next
          strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
          strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
        End If
      End If

      If liBlocco.SelectedIndices.Count > 0 And liBlocco.SelectedIndices.Count <> liBlocco.ItemCount Then
        If liBlocco.GetItemValue(liBlocco.SelectedIndices(0)).ToString <> " " Then
          strQuery += "an_blocco|"
          strRpt += "{anagra.an_blocco}|"
          For i = 0 To liBlocco.SelectedIndices.Count - 1
            strQuery += "=" & CampoTesto(liBlocco.GetItemValue(liBlocco.SelectedIndices(i)).ToString) & "|"
            strRpt += "=" & CampoTestoRpt(liBlocco.GetItemValue(liBlocco.SelectedIndices(i)).ToString) & "|"
          Next
          strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
          strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
        End If
      End If

      If liPrivacy.SelectedIndices.Count > 0 And liPrivacy.SelectedIndices.Count <> liPrivacy.ItemCount Then
        If liPrivacy.GetItemValue(liPrivacy.SelectedIndices(0)).ToString <> "Q" Then
          strQuery += "an_privacy|"
          strRpt += "{anagra.an_privacy}|"
          For i = 0 To liPrivacy.SelectedIndices.Count - 1
            strQuery += "=" & CampoTesto(liPrivacy.GetItemValue(liPrivacy.SelectedIndices(i)).ToString) & "|"
            strRpt += "=" & CampoTestoRpt(liPrivacy.GetItemValue(liPrivacy.SelectedIndices(i)).ToString) & "|"
          Next
          strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
          strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
        End If
      End If

      If liStatus.SelectedIndices.Count > 0 And liStatus.SelectedIndices.Count <> liStatus.ItemCount Then
        If liStatus.GetItemValue(liStatus.SelectedIndices(0)).ToString <> " " Then
          strQuery += "an_status|"
          strRpt += "{anagra.an_status}|"
          For i = 0 To liStatus.SelectedIndices.Count - 1
            strQuery += "=" & CampoTesto(liStatus.GetItemValue(liStatus.SelectedIndices(i)).ToString) & "|"
            strRpt += "=" & CampoTestoRpt(liStatus.GetItemValue(liStatus.SelectedIndices(i)).ToString) & "|"
          Next
          strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
          strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
        End If
      End If

      If liFatturaz.SelectedIndices.Count > 0 And liFatturaz.SelectedIndices.Count <> liFatturaz.ItemCount Then
        strQuery += "an_perfatt|"
        strRpt += "{anagra.an_perfatt}|"
        For i = 0 To liFatturaz.SelectedIndices.Count - 1
          strQuery += "=" & CampoTesto(liFatturaz.GetItemValue(liFatturaz.SelectedIndices(i)).ToString) & "|"
          strRpt += "=" & CampoTestoRpt(liFatturaz.GetItemValue(liFatturaz.SelectedIndices(i)).ToString) & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      If liGgcons.SelectedIndices.Count > 0 And liGgcons.SelectedIndices.Count <> liGgcons.ItemCount Then
        strQuery += "an_gcons|"
        strRpt += "{anagra.an_gcons}|"
        For i = 0 To liGgcons.SelectedIndices.Count - 1
          strQuery += "=" & liGgcons.GetItemValue(liGgcons.SelectedIndices(i)).ToString & "|"
          strRpt += "=" & liGgcons.GetItemValue(liGgcons.SelectedIndices(i)).ToString & "|"
        Next
        strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If

      If liPariva.SelectedIndices.Count > 0 And liPariva.SelectedIndices.Count <> liPariva.ItemCount Then
        For i = 0 To liPariva.SelectedIndices.Count - 1
          Select Case liPariva.GetItemValue(liPariva.SelectedIndices(i)).ToString
            Case "0"
              strQuery += "an_pariva| <> ''" & "|"
              strRpt += "{anagra.an_pariva}| <> ''" & "|"
            Case "1"
              strQuery += "an_pariva| = ''" & "|"
              strRpt += "{anagra.an_pariva}| = ''" & "|"
          End Select
        Next
        If strQuery.Length <> 0 Then strQuery = strQuery.Substring(0, strQuery.Length - 1) & "§"
        If strRpt.Length <> 0 Then strRpt = strRpt.Substring(0, strRpt.Length - 1) & "§"
      End If
      '-----------------------------------------------
      'aggiungo i filtri impostati tramite le estensioni anagrafiche
      If strEstensioni <> "" Then strQuery += strEstensioni
      If strEstensioniRpt <> "" Then strRpt += strEstensioniRpt

      '------------------------------------------------
      'CRM: se non sono amministratore non posso vedere i fornitori
      If bModuloCRM And bIsCRMUser And optFornitori.Visible And optFornitori.Checked And bAmm = False Then
        strQuery += "an_tipo <> 'F'§"
        strRpt += "{anagra.an_tipo} <> 'F'§"
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Eventuale DataTable contenente i filtri impostati nella modale delle Estensioni
      '--------------------------------------------------------------------------------------------------------------
      Select Case strProgrChiamante
        Case "BN__ISTF"
          With oParam.rFiltriAnagra
            strTmp = " "
            If optClienti.Checked = True Then strTmp = "C"
            If optFornitori.Checked = True Then strTmp = "F"
            If optSottoconti.Checked = True Then strTmp = "S"
            !pf_1tipo = strTmp
            !pf_contoini = edContoDa.Text
            !pf_contofin = edContoA.Text
            !pf_provini = edProvinciaDa.Text
            !pf_provfin = edProvinciaA.Text
            !pf_capini = edCapDa.Text
            !pf_capfin = edCapA.Text
            !pf_zonaini = edZonaDa.Text
            !pf_zonafin = edZonaA.Text
            !pf_categini = edCategDa.Text
            !pf_categfin = edCategA.Text
            !pf_agenteini = edAgenteDa.Text
            !pf_agentefin = edAgenteA.Text
            !pf_codpagini = edPagamDa.Text
            !pf_codpagfin = edPagamA.Text
            !pf_dtaperini = edDtAperturaDa.Text
            !pf_dtaperfin = edDtAperturaA.Text
            !pf_ultaggini = edDtAggDa.Text
            !pf_ultaggfin = edDtAggA.Text
            !pf_testiva = liPariva.SelectedValue
            !pf_codcand = edCanaleDa.Text
            !pf_codcana = edCanaleA.Text
            !pf_listd = edListinoDa.Text
            !pf_lista = edListinoA.Text
            !pf_provvd = edProvDa.Text
            !pf_provva = edProvA.Text
            !pf_scontid = edSconDa.Text
            !pf_scontia = edSconA.Text
            !pf_codesed = edEsenDa.Text
            !pf_codesea = edEsenA.Text
            !pf_lingd = edLinguaDa.Text
            !pf_linga = edLinguaA.Text
            !pf_vald = edValutaDa.Text
            !pf_vala = edValutaA.Text
            !pf_ragd = edRagsocDa.Text
            !pf_raga = edRagsocA.Text
            !pf_stato = edStato.Text
            !pf_blocco = liBlocco.SelectedValue
            !pf_privacy = liPrivacy.SelectedValue
            !pf_status = liStatus.SelectedValue
            !pf_perfatt = liFatturaz.SelectedValue
            !pf_pcons = liGgcons.SelectedValue
          End With
          If Not dttFiltriAnex Is Nothing Then oParam.oParam = dttFiltriAnex
      End Select
      '--------------------------------------------------------------------------------------------------------------
      '--- Se:
      '--- il modulo CRM è attivo
      '--- è un CRM user
      '--- si selezionano i Clienti
      '--- seleziona solo i conti collegati ai Leads con codice destinazione a zero, per evitare eventuali
      '--- duplicazioni di righe
      '--------------------------------------------------------------------------------------------------------------
      If bModuloCRM = True And bIsCRMUser = True And optClienti.Checked = True Then
        strQuery += "le_coddest = 0§"
        strRpt += "{leads.le_coddest} = 0§"
      End If
      '--------------------------------------------------------------------------------------------------------------
      If strQuery.Length > 0 Then strQuery = strQuery.Substring(0, strQuery.Length - 1)
      If strRpt.Length > 0 Then strRpt = strRpt.Substring(0, strRpt.Length - 1)

      If oParam.bVisGriglia = True Then
        If strTipoConto = " " Then
          oApp.MsgBoxErr(oApp.Tr(Me, 127791222142812500, "Selezionare quale tipo di conto deve essere trattato (clienti/fornitori/sottoconti)"))
          Return
        End If
        '------------------------------------------------------------------------------------------------------------
        If (ckAnagen.Visible = True) And (ckAnagen.Checked = True) And _
           (ckAbituali.Visible = True) And (ckAbituali.Checked = True) Then
          oApp.MsgBoxErr(oApp.Tr(Me, 130314009331234219, "Attenzione!" & vbCrLf & _
            "Non è possibile, contemporaneamente:" & vbCrLf & _
            " . Visualizzazione delle Anagrafiche Generali" & vbCrLf & _
            " . Clienti/Fornitori presenti in movimenti di magazzino degli ultimi 6 mesi" & vbCrLf & _
            "Deselezionare almeno una delle due scelte."))
          ckAnagen.Focus()
          Return
        End If
        '------------------------------------------------------------------------------------------------------------
        Me.Cursor = Cursors.WaitCursor
        '---------------------------------------
        'filtri specifici per query su anagen
        If ckAnagen.Checked Then
          If edDescr.Text <> "" Then
            strQueryAnagen += "ag_descr1 like " & CampoTesto(edDescr.Text, True) & "§"
            strRptAnagen += "{anagen.ag_descr1} like " & CampoTestoRpt(edDescr.Text, True) & "§"
          End If
          If edSiglaric.Text <> "" Then
            strQueryAnagen += "ag_siglaric like " & CampoTesto(edSiglaric.Text, True) & "§"
            strRptAnagen += "{anagen.ag_siglaric} like " & CampoTestoRpt(edSiglaric.Text, True) & "§"
          End If
          If edPariva.Text <> "" Then
            strQueryAnagen += "ag_pariva like " & CampoTesto(edPariva.Text, True) & "§"
            strRptAnagen += "{anagen.ag_pariva} like " & CampoTestoRpt(edPariva.Text, True) & "§"
          End If
          If edCodfisc.Text <> "" Then
            strQueryAnagen += "ag_codfis like " & CampoTesto(edCodfisc.Text, True) & "§"
            strRptAnagen += "{anagen.ag_codfis} like " & CampoTestoRpt(edCodfisc.Text, True) & "§"
          End If
          If edEmail.Text <> "" Then
            strQueryAnagen += "ag_email like " & CampoTesto(edEmail.Text, True) & "§"
            strRptAnagen += "{anagen.ag_email} like " & CampoTestoRpt(edEmail.Text, True) & "§"
          End If
          If edDescr2.Text <> "" Then
            strQueryAnagen += "ag_descr2 like " & CampoTesto(edDescr2.Text, True) & "§"
            strRptAnagen += "{anagen.ag_descr2} like " & CampoTestoRpt(edDescr2.Text, True) & "§"
          End If
          If edComune.Text <> "" Then
            strQueryAnagen += "ag_citta like " & CampoTesto(edComune.Text, True) & "§"
            strRptAnagen += "{anagen.ag_citta} like " & CampoTestoRpt(edComune.Text, True) & "§"
          End If
          If edTelef.Text <> "" Then
            strQueryAnagen += "ag_telef like " & CampoTesto(edTelef.Text, True) & "§"
            strRptAnagen += "{anagen.ag_telef} like " & CampoTestoRpt(edTelef.Text, True) & "§"
          End If
          If edFax.Text <> "" Then
            strQueryAnagen += "ag_faxtlx like " & CampoTesto(edFax.Text, True) & "§"
            strRptAnagen += "{anagen.ag_faxtlx} like " & CampoTestoRpt(edFax.Text, True) & "§"
          End If

          'campi tab 1
          If edContoDa.Text <> "0" Then
            strQueryAnagen += "ag_codanag >= " & edContoDa.Text & "§"
            strRptAnagen += "{anagen.ag_codanag} >= " & edContoDa.Text & "§"
          End If
          If edContoA.Text <> "999999999" Then
            strQueryAnagen += "ag_codanag <= " & edContoA.Text & "§"
            strRptAnagen += "{anagen.ag_codanag} <= " & edContoA.Text & "§"
          End If
          If edProvinciaDa.Text <> "" Then
            strQueryAnagen += "ag_prov >= " & CampoTesto(edProvinciaDa.Text) & "§"
            strRptAnagen += "{anagen.ag_prov} >= " & CampoTestoRpt(edProvinciaDa.Text) & "§"
          End If
          If edProvinciaA.Text <> "ZZ" Then
            strQueryAnagen += "ag_prov <= " & CampoTesto(edProvinciaA.Text) & "§"
            strRptAnagen += "{anagen.ag_prov} <= " & CampoTestoRpt(edProvinciaA.Text) & "§"
          End If
          If edCapDa.Text <> "" Then
            strQueryAnagen += "ag_cap >= " & CampoTesto(edCapDa.Text) & "§"
            strRptAnagen += "{anagen.ag_cap} >= " & CampoTestoRpt(edCapDa.Text) & "§"
          End If
          If edCapA.Text <> "ZZZZZ" Then
            strQueryAnagen += "ag_cap <= " & CampoTesto(edCapA.Text) & "§"
            strRptAnagen += "{anagen.ag_cap} <= " & CampoTestoRpt(edCapA.Text) & "§"
          End If
          If edRagsocDa.Text <> "" Then
            strQueryAnagen += "ag_descr1 >= " & CampoTesto(edRagsocDa.Text) & "§"
            strRptAnagen += "{anagen.ag_descr1} >= " & CampoTestoRpt(edRagsocDa.Text) & "§"
          End If
          If edRagsocA.Text <> "".PadLeft(30, "Z"c) Then
            strQueryAnagen += "ag_descr1 <= " & CampoTesto(edRagsocA.Text) & "§"
            strRptAnagen += "{anagen.ag_descr1} <= " & CampoTestoRpt(edRagsocA.Text) & "§"
          End If
          If edValutaDa.Text <> "0" Then
            strQueryAnagen += "ag_valuta >= " & edValutaDa.Text & "§"
            strRptAnagen += "{anagen.ag_valuta} >= " & edValutaDa.Text & "§"
          End If
          If edValutaA.Text <> "999" Then
            strQueryAnagen += "ag_valuta <= " & edValutaA.Text & "§"
            strRptAnagen += "{anagen.ag_valuta} <= " & edValutaA.Text & "§"
          End If

          'campi tab 2
          If edLinguaDa.Text <> "0" Then
            strQueryAnagen += "ag_codling >= " & edLinguaDa.Text & "§"
            strRptAnagen += "{anagen.ag_codling} >= " & edLinguaDa.Text & "§"
          End If
          If edLinguaA.Text <> "999" Then
            strQueryAnagen += "ag_codling <= " & edLinguaA.Text & "§"
            strRptAnagen += "{anagen.ag_codling} <= " & edLinguaA.Text & "§"
          End If
          If edStato.Text <> "" Then
            strQueryAnagen += "ag_stato = " & CampoTesto(edStato.Text) & "§"
            strRptAnagen += "{anagen.ag_stato} = " & CampoTestoRpt(edStato.Text) & "§"
          End If

          If strQueryAnagen.Length > 0 Then strQueryAnagen = strQueryAnagen.Substring(0, strQueryAnagen.Length - 1)
          If strRptAnagen.Length > 0 Then strRptAnagen = strRptAnagen.Substring(0, strRptAnagen.Length - 1)
        End If

        oCleHlan.ApriAnagra(dsHlan, DittaCorrente, strQuery, strQueryAnagen, ckAnagen.Checked, ckSoloSemplificata.Checked, _
                                strTipoConto, cbSottc.SelectedValue.ToString, strCodPcon, bModuloCRM, strAccvis, _
                                lCodorgaOperat, strRegvis, bIsCRMUser, oParam.strTipoSezione, (ckAbituali.Checked AndAlso ckAbituali.Enabled))
        dtcHlan.DataSource = dsHlan.Tables("ANAGRA")
        dsHlan.AcceptChanges()
        grZoom.DataSource = dtcHlan

        '------------------------------------------------
        'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valoreù
        If grvZoom.RowCount > 0 Then
          grvZoom.Focus()
          '---------------------------------------------------
          'faccio in modo che la pressione dell'enter non scateni l'emulazione del tasto TAB
          'Me.NTSKeyDownEnterNotEmulateTabNow()
          'SendKeys.Send("+{TAB}")
        Else
          oApp.MsgBoxInfo(oApp.Tr(Me, 127791222142968750, "La ricerca non ha restituito nessun risultato"))
          edDescr.Focus()
        End If
        '------------------------------------------------------------------------------------------------------------
        oMenu.SaveSettingBus("BN__HLAN", "RECENT", ".", "ClientiFornitoriAbituali", IIf(ckAbituali.Checked = True, "-1", "0").ToString, " ", "NS.", "...", "...")
        '------------------------------------------------------------------------------------------------------------
        'se è impostato di selezionare subito l'unica riga restituita lo faccio
        If bSelectIfOneRow And grvZoom.RowCount = 1 Then
          oParam.strOut = grvZoom.GetFocusedRowCellDisplayText("an_conto")
          Me.Cursor = Cursors.Default
          Me.Close()
        End If

        Me.Cursor = Cursors.Default
      Else
        '---------------------------------------
        'salvo i filtri in un recent per poterli ricaricare alla prossima chiamata dello zoom
        strTmp = SalvaImpostazioni(Me)
        If strTmp.Length > 0 Then strTmp = strTmp.Substring(0, strTmp.Length - 1)
        oMenu.SaveSettingBus("BN__HLAN", "RECENT", ".", "LASTFILTER", strTmp, " ", "NS.", "...", "...")

        '---------------------------------------------
        'restituisco solo la stringa per la query
        oParam.strOut = strQuery
        oParam.strBanc1 = strRpt

        Me.Close()
      End If    'If oParam.bVisGriglia = True Then

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Me.Close()
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim lConto As Integer = 0
    Dim lContoTmp As Integer = 0
    Dim strTipo As String = ""
    Dim strErrorMessage As String = ""
    Dim bAggTabnuma As Boolean = True
    Dim frmHlna As FRM__HLNA = Nothing
    Dim strParam As String = ""
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case strProgrChiamante
        Case "BN__CLIE", "BN__CLIE_F5"
          strTmp = SalvaImpostazioni(Me)
          If strTmp.Length > 0 Then strTmp = strTmp.Substring(0, strTmp.Length - 1)
          oMenu.SaveSettingBus("BN__HLAN", "RECENT", ".", "FiltriInApertura", strTmp, " ", ".S.", ".S.", ".N.")
          strTmp = ""
          If dsHlan.Tables.Count <> 0 Then
            If Not grvZoom.NTSGetCurrentDataRow() Is Nothing Then
              strTmp = NTSCStr(grvZoom.GetFocusedRowCellDisplayText("an_conto"))
            End If
          End If
          oMenu.SaveSettingBus("BN__HLAN", "RECENT", ".", "ContoInApertura", strTmp, " ", ".S.", ".S.", ".N.")
      End Select
      '--------------------------------------------------------------------------------------------------------------
      If dsHlan.Tables.Count = 0 Then Return
      If Not grvZoom.NTSGetCurrentDataRow() Is Nothing Then
        oParam.strOut = grvZoom.GetFocusedRowCellDisplayText("an_conto")
        '-------------------------------------------------
        'se necessario creo anagra da anagen
        If NTSCInt(oParam.strOut) <= 0 Then
          If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791951123593750, _
                            "Procedere con la creazione del conto prelevando i dati dall'Anagrafica " & _
                             "Generale selezionata?")) = Windows.Forms.DialogResult.Yes Then
            If optClienti.Checked Then strTipo = "C"
            If optFornitori.Checked Then strTipo = "F"

            '---------------------------------------------
            'devo ottenere il codice del nuovo conto: chiamo la form specifica
            frmHlna = CType(NTSNewFormModal("FRM__HLNA"), FRM__HLNA)
            Dim oParam1 As New CLE__CLDP
            oParam1.strPar1 = strTipo
            oParam1.strPar2 = strCodPcon
            oParam1.dPar2 = NTSCDec(oParam.strOut) * -1
            oParam1.bPar1 = False           'valore di ritorno dalla form frmHlna
            oParam1.dPar1 = 0               'codice del nuovo cliente/fornitore
            oParam1.dPar3 = 0               'mastro del nuovo cliente/fornitore
            frmHlna.Init(oMenu, oParam1, DittaCorrente)
            frmHlna.InitEntity(oCleHlan)
            frmHlna.ShowDialog()
            If oParam1.bPar1 = False Then Return 'ho abortito la creazione del nuovo conto
            lConto = NTSCInt(oParam1.dPar1)
            bAggTabnuma = oParam1.bPar2     'se true con la creazione del cliente/fornitore deve venir aggiornata anche tabnuma

            '---------------------------------------------
            'controllo se PIVA o codFisc già esistenti
            lContoTmp = oCleHlan.CreaAnagraDaAnagen_TestPivaCodfisc(strTipo, _
                                 grvZoom.GetFocusedRowCellDisplayText("an_pariva"), _
                                 grvZoom.GetFocusedRowCellDisplayText("an_codfis"), _
                                 NTSCInt(oParam.strOut) * -1)
            If lContoTmp <> 0 Then
              If oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791951847500000, _
                                 "Esiste già in anagrafica un cliente/fornitore come quello che si desidera inserire " & _
                                 "(|" & lContoTmp.ToString & "|): proseguo ugualmente?")) = Windows.Forms.DialogResult.No Then
                Return
              End If
            End If    'If lContoTmp <> 0 Then

            '---------------------------------------------
            'creo il nuovo conto
            If CreaAnagraDaAnagen(lConto, NTSCInt(oParam1.dPar3), NTSCInt(oParam.strOut) * -1, strTipo, bAggTabnuma, strTipo) = False Then
              Return
            End If

            '---------------------------------------------
            'se c'è modulo Magazzino/Magazzino EASY, Vendite, Ordini/Ordini EASY in tabinsg apro il programma di gestione della nuova anagrafica
            If lConto <> 0 And oCleHlan.CreaAnagraDaAnagen_VisualizzaClie() Then
              strParam = "APRI;" & strTipo & ";" & lConto.ToString.PadLeft(9, "0"c)
              oMenu.RunChild("BS__CLIE", "CLS__CLIE", "ANAGRAFICA CLIENTI/FORNITORI", DittaCorrente, "", "", Nothing, strParam, True, True)
            End If

            '---------------------------------------------
            'se tutto ok esco con il nuovo codice conto
            If lConto <> 0 Then oParam.strOut = lConto.ToString

          End If    'If MessageBox.Show("Procedere con la creazione del conto prel
        End If    'If NTSCInt(oParam.strOut) <= 0 Then
      End If    'If dsHlan.Tables("ANAGRA").Rows.Count > 0 Then

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmHlna Is Nothing Then frmHlna.Dispose()
      frmHlna = Nothing
    End Try
  End Sub

  Public Overridable Sub cmdReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReset.Click
    optClienti.Checked = False
    optFornitori.Checked = False
    optSottoconti.Checked = False
  End Sub

  Public Overridable Sub cmdGestione_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGestione.Click
    Dim bAnagen As Boolean = False
    Dim lContoTmp As Integer = 0
    Dim strParam As String = ""
    Dim strServer As String = ""
    Dim strNomeDll As String = ""
    Dim strPrgGest As String = ""
    Dim strProgZoom As String = ""
    Dim oParam As New CLE__CLDP

    Try
      '--------------------------------------------------------------------------------------------------------------
      If optClienti.Checked = False And optFornitori.Checked = False And optSottoconti.Checked = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222143125000, "Selezionare prima il tipo da trattare (cliente/fornitore/sottoconto)"))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If grvZoom.RowCount > 0 And grvZoom.FocusedRowHandle > -1 Then
        strParam = grvZoom.GetFocusedRowCellDisplayText("an_conto").PadLeft(9, CType("0", Char))
        If strParam <> "000000000" Then
          If optClienti.Checked Then strParam = "APRI;C;" & strParam
          If optFornitori.Checked Then strParam = "APRI;F;" & strParam
          If optSottoconti.Checked Then strParam = "APRI;S;" & strParam
        Else
          strParam = ""
        End If
        If (strParam <> "000000000") And (NTSCInt(grvZoom.GetFocusedRowCellDisplayText("an_conto")) < 0) Then
          bAnagen = True
          lContoTmp = NTSCInt(grvZoom.GetFocusedRowCellDisplayText("an_conto")) * -1
          strParam = lContoTmp.ToString.PadLeft(9, CType("0", Char))
        End If
      Else
        If optClienti.Checked Then strParam = "NUOV;C;0"
        If optFornitori.Checked Then strParam = "NUOV;F;0"
        If optSottoconti.Checked Then strParam = "NUOV;S;0"
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Ottengo il nome del programma per la gestione
      '--------------------------------------------------------------------------------------------------------------
      If bAnagen = True Then
        strProgZoom = "ZOOMANAGEN"
      Else
        If optClienti.Checked Or optFornitori.Checked Then
          strProgZoom = "ZOOMANAGRA"
        Else
          strProgZoom = "ZOOMANAGRAS"
        End If
      End If
      strPrgGest = NTSZOOM.GetNomeProgForGest(strProgZoom)
      If strPrgGest = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222145156250, "Funzione GESTIONE non abilitata per il programma '|" & strProgZoom & "|'"))
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Child COM
      '--------------------------------------------------------------------------------------------------------------
      strServer = strPrgGest.Replace("CLS", "BS")
      oMenu.RunChild(strServer, strPrgGest, "", DittaCorrente, "", "", Nothing, strParam, True, True)
      If bEseguitaRicerca And grZoom.Visible Then cmdRicerca_Click(cmdRicerca, Nothing)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdNuovoPdc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNuovoPdc.Click
    Try
      Select Case True
        Case optClienti.Checked
          oMenu.RunChild("NTSInformatica", "FRM__CLIE", "", DittaCorrente, "", "BN__CLIE", Nothing, "NUOV;C", False, False)
        Case optFornitori.Checked
          oMenu.RunChild("NTSInformatica", "FRM__CLIE", "", DittaCorrente, "", "BN__CLIE", Nothing, "NUOV;F", False, False)
        Case optSottoconti.Checked
          oMenu.RunChild("NTSInformatica", "FRM__SOTC", "", DittaCorrente, "", "BN__SOTC", Nothing, "NUOV;", False, False)
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdEscludi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEscludi.Click
    Try
      Dim oParam As New CLE__CLDP
      oParam.strPar1 = strCodPcon
      Dim frmHlec As FRM__HLEC = Nothing
      frmHlec = CType(NTSNewFormModal("FRM__HLEC"), FRM__HLEC)
      frmHlec.Init(oMenu, oParam, DittaCorrente)
      frmHlec.InitEntity(oCleHlan)
      frmHlec.strTipoConto = "C"
      If optFornitori.Checked = True Then frmHlec.strTipoConto = "F"
      If optSottoconti.Checked = True Then frmHlec.strTipoConto = "S"
      frmHlec.ShowDialog()
      frmHlec.Dispose()
      frmHlec = Nothing

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdEstensioni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEstensioni.Click
    Dim frmHlex As FRM__HLEX = Nothing
    Try
      frmHlex = CType(NTSNewFormModal("FRM__HLEX"), FRM__HLEX)
      '---------------------------------------------------
      'chiamo ma form per le estensioni anagrafiche:
      'di ritorno, se ho impostato alcuni filtri, mi ritrovo la 
      'oParam.bPar1 = True e la stringa con i filtri in oParam.strPar1

      Dim oParam As New CLE__CLDP
      If optClienti.Checked Then
        oParam.strPar1 = "C"
      Else
        oParam.strPar1 = "F"
      End If
      frmHlex.Init(oMenu, oParam, DittaCorrente)
      frmHlex.InitEntity(oCleHlan)
      frmHlex.bOttimistico = ckOttimistico.Checked
      frmHlex.strProgrChiamante = strProgrChiamante
      frmHlex.strFiltriDaEsterno = strFiltriDaEsterno
      frmHlex.ShowDialog()
      If oParam.bPar1 = True Then
        strEstensioni = oParam.strPar1.Trim
        strEstensioniRpt = oParam.strPar2.Trim
      Else
        strEstensioni = ""
        strEstensioniRpt = ""
      End If

      dttFiltriAnex = frmHlex.dttAnex

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmHlex Is Nothing Then frmHlex.Dispose()
      frmHlex = Nothing
    End Try
  End Sub

  Public Overridable Sub cmdLastfilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLastfilter.Click
    Dim strValue As String = ""
    Dim ctrlT As Object = Nothing
    Dim strT() As String = Nothing
    Dim strT1() As String = Nothing
    Dim i As Integer = 0
    Dim l As Integer = 0
    Try
      Me.Cursor = Cursors.WaitCursor
      strValue = oMenu.GetSettingBus("BN__HLAN", "RECENT", ".", "LASTFILTER", "", " ", "")
      If strValue = "" Then Return
      strT = strValue.Split("|"c)
      For i = 0 To strT.Length - 1
        strT1 = strT(i).Split("="c)
        ctrlT = Me.NTSFindControlByName(Me, strT1(0))
        If Not ctrlT Is Nothing Then
          If CType(ctrlT, Control).Enabled Then
            If ctrlT.GetType.ToString.ToUpper.IndexOf("NTSTEXTBOX") > -1 Then
              CType(ctrlT, Control).Text = strT1(1)
            ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSCHECKBOX") > -1 Then
              CType(ctrlT, NTSCheckBox).Checked = CBool(strT1(1))
            ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSRADIOBUTTON") > -1 Then
              CType(ctrlT, NTSRadioButton).Checked = CBool(strT1(1))
            ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSCOMBOBOX") > -1 Then
              CType(ctrlT, NTSComboBox).SelectedValue = strT1(1)
            ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSLISTBOX") > -1 Then
              CType(ctrlT, NTSListBox).SelectedIndex = -1 'pulisco gli elementi selezionati
              strT1 = strT1(1).Split(","c)
              For l = 0 To strT1.Length - 1
                CType(ctrlT, NTSListBox).SetSelected(NTSCInt(strT1(l)), True)
              Next
            ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSGRID") > -1 Then
              'non gestita: senza la visualizzaz. della griglia il primo TAB non è visibile
            End If
          End If    'If CType(ctrlT, Control).Enabled And CType(ctrlT, Control).Visible Then
        End If    'If Not ctrlT Is Nothing Then
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
#End Region


  Public Overridable Function CreaAnagraDaAnagen(ByVal lConto As Integer, ByVal lMastro As Integer, _
                                    ByVal lCodAnag As Integer, ByVal strTipoCF As String, _
                                    ByVal bAggTabNuma As Boolean, ByVal strTipo As String) As Boolean
    'chiamo BE__CLIE, lo istanzio e creo il nuovo cliente da anagrafica generale
    Dim oCleClie As CLE__CLIE
    Dim dsClie As New DataSet
    Dim bContoMovimentato As Boolean = False
    Try
      '------------------------------------------------
      'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
      Dim strErr As String = ""
      Dim oTmp As Object = Nothing
      If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__CLIE", "BE__CLIE", oTmp, strErr, False, "", "") = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128271029889882656, "ERRORE in fase di creazione Entity:" & vbCrLf & strErr))
        Return False
      End If
      oCleClie = CType(oTmp, CLE__CLIE)
      bRemoting = oMenu.Remoting("BN__CLIE", strRemoteServer, strRemotePort)
      AddHandler oCleClie.RemoteEvent, AddressOf GestisciEventiEntity
      If oCleClie.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
      '------------------------------------------------
      'predispongo l'ambiente
      oCleClie.bNonProporreSiglaRic = CBool(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "NonProporreSiglaRic", "0", " ", "0"))
      oCleClie.strGeneraIdPswClienti = oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "GeneraIdPswClienti", "0", " ", "0").ToString
      bGestAnaExt = CBool(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "GestAnaExt", "0", " ", "0"))
      oCleClie.bGesttabcont = CBool(oMenu.GetSettingBus("OPZIONI", ".", ".", "GestTabcont", "0", " ", "0"))
      oCleClie.strBloccaavvertifido = oMenu.GetSettingBus("BSCGDCST", "OPZIONI", ".", "Bl_cliente_sup_fido", " ", " ", " ")
      oCleClie.strBloccainsolu = oMenu.GetSettingBus("BSCGDCST", "OPZIONI", ".", "Bl_cliente_per_insol", " ", " ", " ")
      oCleClie.bScriviActlog = CBool(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "ScriviActlog", "0", " ", "0"))
      oCleClie.nCodpagaInAddNew = CInt(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "Pagamento_Nuovo_Cliente", "0", " ", "0"))
      oCleClie.nListinoInAddNew = CInt(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "Listino_Nuovo_Cliente", "1", " ", "1"))

      oCleClie.bNuovoContoProposto = bAggTabNuma
      oCleClie.strTipoConto = strTipo
      oCleClie.lLead = 0

      '------------------------------------------------
      'CRM: se l'operatore non è stato codificato e non ha un ruolo non può operare
      oCleClie.bModuloAS = False
      oCleClie.bModuloCRM = False
      If CBool(oMenu.ModuliDittaDitt(DittaCorrente) And CLN__STD.bsModAS) Then oCleClie.bModuloAS = True
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And CLN__STD.bsModExtCRM) Then oCleClie.bModuloCRM = True
      If oCleClie.bModuloCRM Then
        oCleClie.bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, oCleClie.bAmm, oCleClie.strAccvis, oCleClie.strAccmod, oCleClie.strRegvis, oCleClie.strRegmod)

        If oCleClie.bIsCRMUser Then
          oCleClie.lCodorgaOperat = oMenu.RitornaCodorgaDaOpnome(DittaCorrente, oCleClie.nCodcageoperat)
          If oCleClie.lCodorgaOperat = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 127791222142500001, "Attenzione!" & vbCrLf & "L'operatore '|" & oApp.User.Nome & _
                 "|' (CRM) non è associato all'organizzazione della ditta corrente '|" & DittaCorrente & "|'." & vbCrLf & _
                 "Impossibile continuare."))
            Return False
          End If
        End If
      Else
        If oCleClie.bModuloAS Then oCleClie.lCodorgaOperat = oMenu.RitornaCodorgaDaOpnome(DittaCorrente, oCleClie.nCodcageoperat)
      End If    ' If bModuloCRM Then

      If Not oCleClie.LeggiDatiDitta(DittaCorrente, bGestAnaExt) Then Return False
      If Not oCleClie.Apri(DittaCorrente, True, lConto, "", dsClie) Then Return False
      If Not oCleClie.NuovoAnagra(lConto, lMastro, lCodAnag, oCleClie.lLead, "", dsClie.Tables("ANAGRA").Rows(0)) Then Return False
      dsClie.Tables("ANAGRA").Rows(0).AcceptChanges()
      If Not oCleClie.CaricaColonneUnbound(dsClie.Tables("ANAGRA").Rows(0), bContoMovimentato) Then Return False
      oCleClie.bHasChanges = True
      If Not oCleClie.Salva(False) Then Return False

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      oCleClie = Nothing
    End Try
  End Function


  Public Overridable Sub grZoom_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grZoom.MouseDoubleClick
    cmdSeleziona_Click(Me, Nothing)
  End Sub


  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      'entro qui perchè nella FRM__HLAN_KeyDown ho inserito il seguente codice:
      'If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
      '  Zoom()
      '  e.Handled = True
      'End If

      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return

      If edMastro.Focused Then
        '----------------------------------------------
        'zoom specifico per mastri di contabilità
        oParam.strCodPdc = strCodPcon     'passo il piano dei conti
        SetFastZoom(edMastro.Text, oParam) 'gestione dello zoom veloce
        NTSZOOM.strIn = edMastro.Text
        NTSZOOM.ZoomStrIn("ZOOMTABMAST", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edMastro.Text Then edMastro.Text = NTSZOOM.strIn
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        'SendKeys.SendWait("{F5}") 'se faccio questa riga va in un loop infinito....
        NTSCallStandardZoom()
      End If
      '------------------------------------
      'riporto il focus sul controllo che ha chiamato lo zoom
      ctlLastControl.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub



  Public Overridable Function CampoTesto(ByVal strTesto As String, Optional ByVal bApplicaPerc As Boolean = False) As String
    Dim strOut As String = ""
    Dim bFil As String = ""
    Try
      If bApplicaPerc Then bFil = "%"

      If strTesto.Length > 1 Then
        If strTesto.Substring(strTesto.Length - 1, 1) = "*" Then
          strTesto = strTesto.Substring(0, strTesto.Length - 1)
        End If
      End If
      If ckOttimistico.Checked And bSelectIfOneRow = False Then
        strOut = CStrSQL(bFil & strTesto & bFil)
      Else
        strOut = CStrSQL(strTesto & bFil)
      End If
      strOut = strOut.Replace("?", "_").Replace("*", "%")

      Return strOut

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return "''"
    End Try
  End Function
  Public Overridable Function CampoTestoRpt(ByVal strTesto As String, Optional ByVal bApplicaPerc As Boolean = False) As String
    Dim strOut As String = ""
    Dim bFil As String = ""
    Try
      If bApplicaPerc Then bFil = "*"

      If strTesto.Length > 1 Then
        If strTesto.Substring(strTesto.Length - 1, 1) = "*" Then
          strTesto = strTesto.Substring(0, strTesto.Length - 1)
        End If
      End If
      If ckOttimistico.Checked And bSelectIfOneRow = False Then
        strOut = ConvStrRpt(bFil & strTesto & bFil)
      Else
        strOut = ConvStrRpt(strTesto & bFil)
      End If
      strOut = strOut.Replace("_", "?").Replace("%", "*")

      Return strOut
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return "''"
    End Try
  End Function



  Public Overridable Sub optSottoconti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSottoconti.CheckedChanged
    If optSottoconti.Checked Then
      GctlSetVisEnab(ckSoloSemplificata, False)
      GctlSetVisEnab(cbSottc, False)
      ckAnagen.Checked = False
      ckAnagen.Enabled = False
      If oParam.bVisGriglia = True Then
        ckAbituali.Checked = False
        ckAbituali.Enabled = False
      End If
    Else
      ckSoloSemplificata.Enabled = False
      cbSottc.Enabled = False
      If bModAnagen = True Then
        GctlSetVisEnab(ckAnagen, False)
      End If
    End If
    GestisceEstensioni()
  End Sub


  Public Overridable Sub TrattaValoreInInput(ByVal strIn As String)
    Dim nValoreInInput As Integer

    Try
      nValoreInInput = NTSCInt(oMenu.GetSettingBus("BS--HLAN", "OPZIONI", ".", "ValoreInInput", "4", " ", "4"))
      nFocusIniziale = NTSCInt(oMenu.GetSettingBus("BS--HLAN", "OPZIONI", ".", "FocusIniziale", "4", " ", "4"))
      If oParam.strTipo <> "C" And oParam.strTipo <> "F" Then
        'sui sottoconti il valore proposto è sempre nella descrizione, così come il focus iniziale
        nValoreInInput = 1
        nFocusIniziale = 4
      End If

      If strIn = "0" Or strIn = "999999999" Then strIn = ""
      If oCleHlan.IsContoInAnagra(strIn) Then strIn = ""
      If nValoreInInput <> 0 Then
        If Trim(strIn) = "" Then strIn = ""
        Select Case nValoreInInput
          Case 1 : edDescr.Text = strIn
          Case 2 : edSiglaric.Text = strIn
          Case 3 : If Len(strIn) = 11 Then edPariva.Text = strIn
          Case 4, 5
            If IsNumeric(strIn) Then
              If Len(strIn) = 11 Then edPariva.Text = strIn
            Else
              If Prime3Cons(strIn) = True Then
                edCodfisc.Text = strIn
              Else
                Select Case nValoreInInput
                  Case 4 : edDescr.Text = strIn
                  Case 5 : edSiglaric.Text = strIn
                End Select
              End If
            End If
        End Select
      End If
      Select Case nFocusIniziale
        Case 1, 4 : If edDescr.Enabled And edDescr.Visible Then edDescr.Focus()
        Case 2, 5 : If edSiglaric.Enabled And edSiglaric.Visible Then edSiglaric.Focus()
        Case 3 : If edPariva.Enabled And edPariva.Visible Then edPariva.Focus()
        Case 6 : If edCodfisc.Enabled And edCodfisc.Visible Then edCodfisc.Focus()
      End Select
    Catch ex As Exception

    End Try
  End Sub

  Public Overridable Function Prime3Cons(ByVal strParam As String) As Boolean
    Dim b1 As Boolean
    Dim b2 As Boolean
    Dim b3 As Boolean

    '-----------------------------------------------------------------------------------------
    '--- Funzione per determinare se le prime 3 lettere del parametro in ingresso
    '--- sono consonanti (per il Codice Fiscale)
    '-----------------------------------------------------------------------------------------
    Prime3Cons = False
    '-----------------------------------------------------------------------------------------
    If Len(Mid(strParam, 1, 3)) = 0 Then Exit Function
    '-----------------------------------------------------------------------------------------
    Select Case UCase(Mid(strParam, 1, 1))
      Case "A", "E", "I", "O", "U", "Y", "" : b1 = False
      Case Else : b1 = True
    End Select
    '-----------------------------------------------------------------------------------------
    Select Case UCase(Mid(strParam, 2, 1))
      Case "A", "E", "I", "O", "U", "Y", "" : b2 = False
      Case Else : b2 = True
    End Select
    '-----------------------------------------------------------------------------------------
    Select Case UCase(Mid(strParam, 3, 1))
      Case "A", "E", "I", "O", "U", "Y", "" : b3 = False
      Case Else : b3 = True
    End Select
    '-----------------------------------------------------------------------------------------
    If (b1 = True) And (b2 = True) And (b3 = True) Then
      Prime3Cons = True
    Else
      Prime3Cons = False
    End If
    '-----------------------------------------------------------------------------------------
  End Function



  Public Overridable Sub optFornitori_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFornitori.CheckedChanged
    Try
      GestisceEstensioni()
      '--------------------------------------------------------------------------------------------------------------
      If (optFornitori.Checked = True) And (oParam.bVisGriglia = True) Then GctlSetVisEnab(ckAbituali, False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub optClienti_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optClienti.CheckedChanged
    Try
      cbPrivato.Enabled = optClienti.Checked

      GestisceEstensioni()
      '--------------------------------------------------------------------------------------------------------------
      If (optClienti.Checked = True) And (oParam.bVisGriglia = True) Then GctlSetVisEnab(ckAbituali, False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub GestisceEstensioni()
    Dim strTipo As String
    Try
      strEstensioni = ""
      If optClienti.Checked Or optFornitori.Checked Then
        If bGestAnaExt Then
          strTipo = "F"
          If optClienti.Checked Then strTipo = "C"
          If oCleHlan.GetGestAnaext(DittaCorrente, strTipo) Then
            GctlSetVisEnab(cmdEstensioni, False)
            GctlSetVisEnab(cmdEstensioni, True)
          Else
            cmdEstensioni.Enabled = False
            cmdEstensioni.Visible = False
          End If
        End If
      Else
        cmdEstensioni.Enabled = False
        cmdEstensioni.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edMastro_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edMastro.Leave
    Try
      If oCleHlan.TestMastro(NTSCInt(edMastro.Text), "", strCodPcon, "") = False Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222143281250, "Codice Mastro contabile non corretto"))
        edMastro.Text = "0"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edDescr_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDescr.Enter
    Try
      FocusGrid()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edSiglaric_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSiglaric.Enter
    Try
      FocusGrid()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edPariva_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edPariva.Enter
    Try
      FocusGrid()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCodfisc_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodfisc.Enter
    Try
      FocusGrid()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FocusGrid()
    Try
      If bForm_load Then
        bForm_load = False
        If grvZoom.RowCount > 0 Then grvZoom.Focus()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edMastro_NTSZoomGest(ByVal sender As System.Object, ByRef e As NTSInformatica.NTSEventArgs) Handles edMastro.NTSZoomGest
    Try
      e.ZoomHandled = True        'per non far lanciare la NTSZoomGest standard del controllo
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Function SalvaImpostazioni(ByRef ctrlStart As Control) As String
    Dim strValue As String = ""
    Dim i As Integer = 0
    SalvaImpostazioni = ""
    Try
      For Each ctrlT As Control In ctrlStart.Controls
        If ctrlT.GetType.ToString.ToUpper.IndexOf("NTSTEXTBOX") > -1 Then
          If ctrlT.Name <> "NTSText" Then strValue += ctrlT.Name & "=" & ctrlT.Text & "|"
        ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSCHECKBOX") > -1 Then
          strValue += ctrlT.Name & "=" & NTSCInt(CType(ctrlT, NTSCheckBox).Checked).ToString & "|"
        ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSRADIOBUTTON") > -1 Then
          strValue += ctrlT.Name & "=" & NTSCInt(CType(ctrlT, NTSRadioButton).Checked).ToString & "|"
        ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSCOMBOBOX") > -1 Then
          strValue += ctrlT.Name & "=" & CType(ctrlT, NTSComboBox).SelectedValue & "|"
        ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSLISTBOX") > -1 Then
          If CType(ctrlT, NTSListBox).SelectedIndices.Count > 0 Then
            strValue += ctrlT.Name & "="
            For i = 0 To CType(ctrlT, NTSListBox).SelectedIndices.Count - 1
              strValue += CType(ctrlT, NTSListBox).SelectedIndices(i).ToString & ","
            Next
            strValue = strValue.Substring(0, strValue.Length - 1) & "|"
          End If
        ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSGRID") > -1 Then
          'non gestita: senza la visualizzaz. della griglia il primo TAB non è visibile
        End If

        If ctrlT.Controls.Count > 0 Then strValue += SalvaImpostazioni(ctrlT)
      Next

      Return strValue

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub SettaFiltri(ByVal strProgrChiamante As String)
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim strValue As String = ""
    Dim ctrlT As Object = Nothing
    Dim strT() As String = Nothing
    Dim strT1() As String = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      If strProgrChiamante.ToUpper <> "BN__CLIE" Then Return
      '--------------------------------------------------------------------------------------------------------------
      strValue = oMenu.GetSettingBus("BN__HLAN", "RECENT", ".", "FiltriInApertura", "", " ", "")
      If strValue = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      strT = strValue.Split("|"c)
      '--------------------------------------------------------------------------------------------------------------
      For i = 0 To (strT.Length - 1)
        strT1 = strT(i).Split("="c)
        ctrlT = Me.NTSFindControlByName(Me, strT1(0))
        If Not ctrlT Is Nothing Then
          If CType(ctrlT, Control).Enabled Then
            If ctrlT.GetType.ToString.ToUpper.IndexOf("NTSTEXTBOX") > -1 Then
              CType(ctrlT, Control).Text = strT1(1)
            ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSCHECKBOX") > -1 Then
              CType(ctrlT, NTSCheckBox).Checked = CBool(strT1(1))
            ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSRADIOBUTTON") > -1 Then
              CType(ctrlT, NTSRadioButton).Checked = CBool(strT1(1))
            ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSCOMBOBOX") > -1 Then
              CType(ctrlT, NTSComboBox).SelectedValue = strT1(1)
            ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSLISTBOX") > -1 Then
              For l = 0 To (CType(ctrlT, NTSListBox).ItemCount - 1)
                CType(ctrlT, NTSListBox).SetSelected(l, CBool(IIf(l = 0, True, False)))
              Next
              strT1 = strT1(1).Split(","c)
              For l = 0 To (strT1.Length - 1)
                CType(ctrlT, NTSListBox).SetSelected(NTSCInt(strT1(l)), True)
              Next
            ElseIf ctrlT.GetType.ToString.ToUpper.IndexOf("NTSGRID") > -1 Then
              strT1 = strT1(1).Split("§"c)
              For l = 0 To CType(ctrlT, NTSGrid).Views(0).RowCount - 1
                CType(CType(ctrlT, NTSGrid).Views(0), NTSGridView).SetRowCellValue(l, "xx_valore", strT1(l))
              Next
            End If
          End If    'If CType(ctrlT, Control).Enabled And CType(ctrlT, Control).Visible Then
        End If    'If Not ctrlT Is Nothing Then
      Next
      '--------------------------------------------------------------------------------------------------------------
      cmdRicerca_Click(Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
      strValue = oMenu.GetSettingBus("BN__HLAN", "RECENT", ".", "ContoInApertura", "", " ", "")
      If strValue = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If dsHlan.Tables("ANAGRA").Rows.Count = 0 Then Return
      '--------------------------------------------------------------------------------------------------------------
      grZoom.Focus()
      For i = 0 To (dtcHlan.List.Count - 1)
        If NTSCInt(CType(dtcHlan.Item(i), DataRowView)!an_conto) = NTSCInt(strValue) Then
          dtcHlan.Position = i
          Exit For
        End If
      Next
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

End Class
