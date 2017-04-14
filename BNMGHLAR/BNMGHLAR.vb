Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO

Public Class FRMMGHLAR

  Public oParam As CLE__PATB       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore
  Public oCleHlar As CLEMGHLAR
  Public dsHlar As New DataSet
  Public dcHlar As BindingSource = New BindingSource()
  Public nFocusIniziale As Integer = 0

  Public bSelectIfOneRow As Boolean = False 'se true, dopo aver lanciato la ricerca se viene restituito un solo record viene subito selezionato
  Public bForm_load As Boolean = True
  Public bEseguitaRicerca As Boolean = False
  Public dttCampi As New DataTable          'elenco campi filtrabili di ARTICO

  Public dsFiltri As New DataSet
  Public dcFiltri1 As New BindingSource
  Public strOpz4 As String = ""
  Public dttTipa As New DataTable           'contiene tutte le tipologie articoli
  Public strCodRoot As String = ""          'serve per quando mghlar viene chiamato la seconda volta per visualizzare il dettaglio varianti (solocol zoom_2_livelli = "S")
  Public nFocusInizialeDopoRicerca As Integer = 0

  Public bLiv2 As Boolean                   'Per evitare le l'opzione di registro 'ValoreInInput" con valore diverso da 1 non faccia andare lo zoom a 2 livelli
  Public bNessunRisultato As Boolean = False

  'REIMPOSTAZIONE filtri
  Public bReimpostazioneFiltri As Boolean = False      'E' 'True' quando lo zoom viene utilizzato per reimpostazione filtri, per esempio dalle 'Stampe predefinite'
  Public dcParstaf As BindingSource = New BindingSource()
  Public Const MIN_DATA As String = "01/01/1900"
  Public Const MAX_DATA As String = "31/12/2099"
  Public Const MIN_ANNO As Integer = 1900
  Public Const MAX_ANNO As Integer = 2099

  Public lDimPnClass As Integer = 0

  Public fs As System.IO.FileStream

  Public strProgrChiamante As String = ""
  Public bOnLoading As Boolean = False
  Public bCr As Boolean 'se crystal o query

  Public dttFiltriAnex As New DataTable
  Public strFiltriDaEsterno As String
  Public strEstensioni As String = ""    'stringa contenentei filtri impostati tramite le estensioni anagrafiche
  Public bProponiTipoArticolo As Boolean = True

  Public bCercaInDescr2 As Boolean

#Region "Inizializzazione"
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
    If Not oParam.rFiltriArtico Is Nothing Then
      bReimpostazioneFiltri = True
    End If
    InitializeComponent()

    Me.MinimumSize = Me.Size


    Me.MinimizeBox = False

    '------------------------------------------------
    'creo e attivo l'entity
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGHLAR", "BEMGHLAR", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222142187500, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHlar = CType(oTmp, CLEMGHLAR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGHLAR", strRemoteServer, strRemotePort)
    oCleHlar.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

    '---------------------------------
    'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    AddHandler oCleHlar.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Dim dttTipo As New DataTable() : dttTipo.Columns.Add("cod", GetType(String)) : dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {"T", "Tutti"}) : dttTipo.Rows.Add(New Object() {"S", "Si"}) : dttTipo.Rows.Add(New Object() {"N", "No"})
      dttTipo.AcceptChanges()

      Dim dttTipo1 As New DataTable() : dttTipo1.Columns.Add("cod", GetType(String)) : dttTipo1.Columns.Add("val", GetType(String))
      dttTipo1.Rows.Add(New Object() {"T", "Tutti"}) : dttTipo1.Rows.Add(New Object() {"S", "Si"}) : dttTipo1.Rows.Add(New Object() {"N", "No"})
      dttTipo1.AcceptChanges()

      Dim dttTipo2 As New DataTable() : dttTipo2.Columns.Add("cod", GetType(String)) : dttTipo2.Columns.Add("val", GetType(String))
      dttTipo2.Rows.Add(New Object() {"T", "Tutti"})
      dttTipo2.Rows.Add(New Object() {"O", "Ordini/Impegni"})
      dttTipo2.Rows.Add(New Object() {"S", "Ordini/Imp./Magaz."})
      dttTipo2.Rows.Add(New Object() {"N", "No"})
      dttTipo2.AcceptChanges()

      Dim dttTipo3 As New DataTable() : dttTipo3.Columns.Add("cod", GetType(String)) : dttTipo3.Columns.Add("val", GetType(String))
      dttTipo3.Rows.Add(New Object() {"T", "Tutti"}) : dttTipo3.Rows.Add(New Object() {"S", "Si"}) : dttTipo3.Rows.Add(New Object() {"N", "No"})
      dttTipo3.AcceptChanges()

      Dim dttTipo4 As New DataTable() : dttTipo4.Columns.Add("cod", GetType(String)) : dttTipo4.Columns.Add("val", GetType(String))
      dttTipo4.Rows.Add(New Object() {"T", "Tutti"}) : dttTipo4.Rows.Add(New Object() {"S", "Si"}) : dttTipo4.Rows.Add(New Object() {"N", "No"})
      dttTipo4.AcceptChanges()

      Dim dttTipo5 As New DataTable() : dttTipo5.Columns.Add("cod", GetType(String)) : dttTipo5.Columns.Add("val", GetType(String))
      dttTipo5.Rows.Add(New Object() {"T", "Tutti"}) : dttTipo5.Rows.Add(New Object() {"S", "Si"}) : dttTipo5.Rows.Add(New Object() {"N", "No"})
      dttTipo5.AcceptChanges()

      Dim dttTipo6 As New DataTable() : dttTipo6.Columns.Add("cod", GetType(String)) : dttTipo6.Columns.Add("val", GetType(String))
      dttTipo6.Rows.Add(New Object() {"T", "Tutti"}) : dttTipo6.Rows.Add(New Object() {"S", "Si"}) : dttTipo6.Rows.Add(New Object() {"N", "No"})
      dttTipo6.AcceptChanges()

      Dim dttTipo7 As New DataTable() : dttTipo7.Columns.Add("cod", GetType(String)) : dttTipo7.Columns.Add("val", GetType(String))
      dttTipo7.Rows.Add(New Object() {"T", "Tutti"}) : dttTipo7.Rows.Add(New Object() {"S", "Si"}) : dttTipo7.Rows.Add(New Object() {"N", "No"})
      dttTipo7.AcceptChanges()

      Dim dttTipo8 As New DataTable() : dttTipo8.Columns.Add("cod", GetType(String)) : dttTipo8.Columns.Add("val", GetType(String))
      dttTipo8.Rows.Add(New Object() {"T", "Tutti"}) : dttTipo8.Rows.Add(New Object() {"S", "Si"}) : dttTipo8.Rows.Add(New Object() {"N", "No"})
      dttTipo8.AcceptChanges()

      cbAfasi.DataSource = dttTipo : cbAfasi.DisplayMember = "val" : cbAfasi.ValueMember = "cod" : cbAfasi.SelectedValue = "T"
      cbLotti.DataSource = dttTipo1 : cbLotti.DisplayMember = "val" : cbLotti.ValueMember = "cod" : cbLotti.SelectedValue = "T"
      cbCommessa.DataSource = dttTipo2 : cbCommessa.DisplayMember = "val" : cbCommessa.ValueMember = "cod" : cbCommessa.SelectedValue = "T"
      cbMatricole.DataSource = dttTipo3 : cbMatricole.DisplayMember = "val" : cbMatricole.ValueMember = "cod" : cbMatricole.SelectedValue = "T"
      cbUbicaz.DataSource = dttTipo4 : cbUbicaz.DisplayMember = "val" : cbUbicaz.ValueMember = "cod" : cbUbicaz.SelectedValue = "T"
      cbAvarianti.DataSource = dttTipo5 : cbAvarianti.DisplayMember = "val" : cbAvarianti.ValueMember = "cod" : cbAvarianti.SelectedValue = "T"
      cbInesaur.DataSource = dttTipo6 : cbInesaur.DisplayMember = "val" : cbInesaur.ValueMember = "cod" : cbInesaur.SelectedValue = "T"
      cbAlistino.DataSource = dttTipo7 : cbAlistino.DisplayMember = "val" : cbAlistino.ValueMember = "cod" : cbAlistino.SelectedValue = "T"
      cbCritico.DataSource = dttTipo8 : cbCritico.DisplayMember = "val" : cbCritico.ValueMember = "cod" : cbCritico.SelectedValue = "T"
      ckAbituali.NTSSetParam(oMenu, oApp.Tr(Me, 130314004708970856, "Articoli movimentati ultimi 6 mesi"), "S", "N")

      Dim dttDB As New DataTable()
      dttDB.Columns.Add("cod", GetType(String))
      dttDB.Columns.Add("val", GetType(String))
      dttDB.Rows.Add(New Object() {"T", "Nessun test"})
      dttDB.Rows.Add(New Object() {"S", "Articoli con DB"})
      dttDB.Rows.Add(New Object() {"N", "Cod. DB = Cod. articolo"})
      dttDB.AcceptChanges()
      cbTestdb.DataSource = dttDB : cbTestdb.DisplayMember = "val" : cbTestdb.ValueMember = "cod" : cbTestdb.SelectedValue = "T"

      Dim dttTA As New DataTable()
      dttTA.Columns.Add("cod", GetType(String))
      dttTA.Columns.Add("val", GetType(String))
      dttTA.Rows.Add(New Object() {"T", "Nessun test"})
      dttTA.Rows.Add(New Object() {"N", "Articoli normali e configurati"})
      dttTA.Rows.Add(New Object() {"V", "Articoli a varianti"})
      dttTA.Rows.Add(New Object() {"C", "Articoli a taglie e colori"})
      dttTA.AcceptChanges()
      cbTestArt.DataSource = dttTA : cbTestArt.DisplayMember = "val" : cbTestArt.ValueMember = "cod" : cbTestArt.SelectedValue = "T"

      '-------------------------------------------------
      'completo le informazioni dei i controlli
      grvFiltri1.NTSSetParam(oMenu, oApp.Tr(Me, 128837521708726845, "Griglia filtri 1"))
      grvFiltri1.NTSAllowDelete = False
      grvFiltri1.NTSAllowInsert = False
      xx_nome.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128491820088062000, "Nome filtro"), dttCampi, "xx_nome", "cb_nomcampo")
      xx_valore.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128491820607854000, "Valore filtro"), 0)
      xx_valore.NTSSetParamZoom("__")

      cbTipologia.NTSSetParam(oApp.Tr(Me, 128491971425200500, "Tipologia articoli"))
      edMagaz.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971425512500, "Magazzino"), tabmaga)
      cbTestdb.NTSSetParam(oApp.Tr(Me, 128491971425824500, "Test distinta base"))
      cbTestArt.NTSSetParam(oApp.Tr(Me, 128491971425824501, "Test tipo articolo"))
      edCodarfo.NTSSetParam(oMenu, oApp.Tr(Me, 128491971426448500, "Codice articolo cliente/fornitore"), 0)
      edBarcode.NTSSetParam(oMenu, oApp.Tr(Me, 128491971426604500, "Codice a barre"), 0)
      edMagprodfin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971427384500, "Magazzino prodotti finiti A"), tabmaga)
      edMagprodini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971427540500, "Magazzino prodotti finiti DA"), tabmaga)
      edMagstockfin.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971427852500, "Magazzino stoccaggio finiti A"), tabmaga)
      edMagstockini.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971428008500, "Magazzino stoccaggio finiti DA"), tabmaga)
      edDataUltaga.NTSSetParam(oMenu, oApp.Tr(Me, 128491971428164500, "Data aggiorndamento A"), False)
      edDataUltagd.NTSSetParam(oMenu, oApp.Tr(Me, 128491971428320500, "Data aggiornamento DA"), False)
      edCodIvaa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971428944500, "Codice IVA A"), tabciva)
      edCodIvad.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971429256500, "Codice IVA DA"), tabciva)
      edApprova.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971429100500, "Codice approvvigionatore A"), tabappr)
      edApprovd.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971429412500, "Codice approvvigionatore DA"), tabappr)
      edScontid.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971430192500, "Classe sconti DA"), tabcsar)
      edScontia.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971429880500, "Classe sconti A"), tabcsar)
      edProvvd.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971430348500, "Classe provvigioni DA"), tabcpar)
      edProvva.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971430036500, "Classe provvigioni A"), tabcpar)
      edMarcaa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971430660500, "Marca A"), tabmarc)
      edMarcad.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971430816500, "Marca DA"), tabmarc)
      edCodtipaa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971431284500, "Tipologia articoli A"), tabtipa)
      edCodtipad.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971431440500, "Tipologia articoli DA"), tabtipa)
      edForna.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971431596500, "Cliente/fornitore A"), tabanagraf)
      edFornd.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971431752500, "Cliente/fornitore DA"), tabanagraf)
      edFamproda.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971432064500, "Famiglia DA"), tabcfam, True)
      edFamprodd.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971432220500, "Famiglia A"), tabcfam, True)
      edDescra.NTSSetParam(oMenu, oApp.Tr(Me, 128491971432532500, "Descrizione articolo A"), 40)
      edDescrd.NTSSetParam(oMenu, oApp.Tr(Me, 128491971432844500, "Descrizione articolo DA"), 40)
      edSotta.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971433000500, "Sottogruppo merceologico A"), tabsgme)
      edSottd.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971433312500, "Sottogruppo merceologico DA"), tabsgme)
      edGruppoa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971433624500, "Gruppo merceologico A"), tabgmer)
      edGruppod.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971433780500, "Gruppo merceologico DA"), tabgmer)
      edCodalta.NTSSetParam(oMenu, oApp.Tr(Me, 128491971433936500, "Codice alternativo A"), CLN__STD.CodartMaxLen)
      edCodaltd.NTSSetParam(oMenu, oApp.Tr(Me, 128491971434092500, "Codice alternativo DA"), CLN__STD.CodartMaxLen)
      edCodarta.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971434404500, "Codice articolo A"), tabartico, True)
      edCodartd.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971434560500, "Codice articolo DA"), tabartico, True)
      cbAfasi.NTSSetParam(oApp.Tr(Me, 128491971435028500, "Gestione a Fasi"))
      cbCritico.NTSSetParam(oApp.Tr(Me, 128491971435184500, "Articolo critico"))
      cbAlistino.NTSSetParam(oApp.Tr(Me, 128491971435340500, "Articolo a listino"))
      cbInesaur.NTSSetParam(oApp.Tr(Me, 128491971435496500, "Articolo in esaurimento"))
      cbAvarianti.NTSSetParam(oApp.Tr(Me, 128491971436276500, "Gestione a varianti"))
      cbUbicaz.NTSSetParam(oApp.Tr(Me, 128491971436432500, "Gestoine ad ubicazioni"))
      cbMatricole.NTSSetParam(oApp.Tr(Me, 128491971436588500, "Gestione a matricole"))
      cbCommessa.NTSSetParam(oApp.Tr(Me, 128491971436744500, "Gestione a commessa"))
      cbLotti.NTSSetParam(oApp.Tr(Me, 128491971436900500, "Gestione a lotti"))
      edCodtagla.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971437992500, "Tabella taglie A"), tabtagl)
      edCodtagld.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971438148500, "Tabella taglie DA"), tabtagl)
      edCodstaga.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971438460500, "Tabella stagioni A"), tabstag)
      edCodstagd.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971438616500, "Tabella stagioni DA"), tabstag)
      edAnnoa.NTSSetParam(oMenu, oApp.Tr(Me, 128491971438928500, "Anno A"), "0", 4, 0, 9999)
      edAnnod.NTSSetParam(oMenu, oApp.Tr(Me, 128837521787974845, "Anno DA"), "0", 4, 0, 9999)
      edDtvalid.NTSSetParam(oMenu, oApp.Tr(Me, 128491971439084500, "Data validità listino"), False)
      edListino.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971439396500, "Listino"), tablist)
      ckVisprezzi.NTSSetParam(oMenu, oApp.Tr(Me, 128837521855834845, "Visualizza prezzi"), "S", "N")
      edCodartAcc.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971439708500, "Codice articolo di riferimento"), tabartico, True)
      optSuccedanei.NTSSetParam(oMenu, oApp.Tr(Me, 128491971440020500, "Succedanei"), "S")
      optAccessori.NTSSetParam(oMenu, oApp.Tr(Me, 128491971440176500, "Accessori"), "A")
      ckSuccedanei.NTSSetParam(oMenu, oApp.Tr(Me, 128837521935706845, "Seleziona accessori / succedanei"), "S", "N")
      ckFiltraConto.NTSSetParam(oMenu, oApp.Tr(Me, 128837521947874845, "Filtro Cliente/forn. (per cod. articolo C/F)"), "S", "N")
      ckFiltraMovmag.NTSSetParam(oMenu, oApp.Tr(Me, 128491971440488500, "Test su documenti di mag."), "S", "N")
      edConto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128491971440800500, "Conto cliente/fornitore"), tabanagrac)
      ckOttimistico.NTSSetParam(oMenu, oApp.Tr(Me, 128491971441268500, "Ottimistico"), "S", "N")
      ckBloccati.NTSSetParam(oMenu, oApp.Tr(Me, 128492629240958000, "Visualizza articoli bloccati"), "S", "N")

      grvZoom.NTSSetParam(oMenu, oApp.Tr(Me, 128230023422444201, "Griglia articoli"))
      xx_seleziona.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128601700975468750, "Seleziona"), "S", "N")
      ar_codart.NTSSetParamSTR(oMenu, "--", 0, True)
      xx_codarfo.NTSSetParamSTR(oMenu, "--", 0, True)
      ar_descr.NTSSetParamSTR(oMenu, "--", 0, True)
      ar_unmis.NTSSetParamSTR(oMenu, "--", 0, True)
      xx_esist.NTSSetParamNUM(oMenu, "--", oApp.FormatQta, 15)
      xx_prezzo.NTSSetParamNUM(oMenu, "--", oApp.FormatPrzUn, 15)
      ar_codalt.NTSSetParamSTR(oMenu, "--", 0, True)
      xx_code.NTSSetParamSTR(oMenu, "--", 0, True)
      xx_prenot.NTSSetParamNUM(oMenu, "--", oApp.FormatQta, 15)
      xx_ordin.NTSSetParamNUM(oMenu, "--", oApp.FormatQta, 15)
      xx_impegn.NTSSetParamNUM(oMenu, "--", oApp.FormatQta, 15)
      xx_dispnet.NTSSetParamNUM(oMenu, "--", oApp.FormatQta, 15)
      xx_dispon.NTSSetParamNUM(oMenu, "--", oApp.FormatQta, 15)
      xx_dispo2.NTSSetParamNUM(oMenu, "--", oApp.FormatQta, 15)
      ar_desint.NTSSetParamSTR(oMenu, "--", 0, True)
      ar_sostit.NTSSetParamSTR(oMenu, "--", 0, True)
      ar_sostituito.NTSSetParamSTR(oMenu, "--", 0, True)
      ar_inesaur.NTSSetParamCHK(oMenu, "--", "S", "N")
      ar_blocco.NTSSetParamCHK(oMenu, "--", "S", "N")
      ar_stalist.NTSSetParamCHK(oMenu, "--", "S", "N")
      ar_note.NTSSetParamSTR(oMenu, "--", 0, True)
      xx_fase.NTSSetParamNUM(oMenu, "--", "0", 4)
      xx_descr.NTSSetParamSTR(oMenu, "--", 0, True)

      edTipo.NTSSetParam(oMenu, oApp.Tr(Me, 129434618908212890, "Tipo articolo"), 1, True)
      edDescrLike.NTSSetParam(oMenu, oApp.Tr(Me, 129410430956827923, "Descrizione articolo LIKE"), 40, True)
      edUbicLike.NTSSetParam(oMenu, oApp.Tr(Me, 129410433581443158, "Ubicazione articolo LIKE"), 18, True)
      edCodartLike.NTSSetParam(oMenu, oApp.Tr(Me, 129718025674169422, "Codice articolo LIKE"), 18, True)
      edDBLike.NTSSetParam(oMenu, oApp.Tr(Me, 129718025813506207, "DB articolo LIKE"), 18, True)
      edArtprom.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129933088118102432, "Lista promozioni articoli"), tabtpro)
      edListsar.NTSSetParamTabe(oMenu, oApp.Tr(Me, 129933088300422860, "Lista selezionata articoli"), tablsar)

      grvZoom.NTSAllowDelete = False
      grvZoom.NTSAllowUpdate = True
      grvZoom.NTSAllowInsert = False

      grvZoom.Enabled = False

      edCodaltd.NTSSetParamZoom("")
      edCodalta.NTSSetParamZoom("")
      edCodartLike.NTSSetParamZoom("")
      edDBLike.NTSSetParamZoom("")

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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGHLAR))
    Me.pnDescr = New NTSInformatica.NTSPanel
    Me.tsZoom = New NTSInformatica.NTSTabControl
    Me.TabPage1 = New NTSInformatica.NTSTabPage
    Me.pnTab1Pan2 = New NTSInformatica.NTSPanel
    Me.ckAbituali = New NTSInformatica.NTSCheckBox
    Me.cbTestArt = New NTSInformatica.NTSComboBox
    Me.lbTestArt = New NTSInformatica.NTSLabel
    Me.lbCodartLike = New NTSInformatica.NTSLabel
    Me.edCodartLike = New NTSInformatica.NTSTextBoxStr
    Me.lbUbicLike = New NTSInformatica.NTSLabel
    Me.edUbicLike = New NTSInformatica.NTSTextBoxStr
    Me.lbDescrLike = New NTSInformatica.NTSLabel
    Me.edDescrLike = New NTSInformatica.NTSTextBoxStr
    Me.ckBloccati = New NTSInformatica.NTSCheckBox
    Me.cbTipologia = New NTSInformatica.NTSComboBox
    Me.lbTipologia = New NTSInformatica.NTSLabel
    Me.edMagaz = New NTSInformatica.NTSTextBoxNum
    Me.lbMagaz = New NTSInformatica.NTSLabel
    Me.cbTestdb = New NTSInformatica.NTSComboBox
    Me.lbTestdb = New NTSInformatica.NTSLabel
    Me.cmdLock = New NTSInformatica.NTSButton
    Me.lbCodarfo = New NTSInformatica.NTSLabel
    Me.edCodarfo = New NTSInformatica.NTSTextBoxStr
    Me.edBarcode = New NTSInformatica.NTSTextBoxStr
    Me.lbBarcode = New NTSInformatica.NTSLabel
    Me.pnTab1Pan1 = New NTSInformatica.NTSPanel
    Me.grFiltri1 = New NTSInformatica.NTSGrid
    Me.grvFiltri1 = New NTSInformatica.NTSGridView
    Me.xx_nome = New NTSInformatica.NTSGridColumn
    Me.xx_valore = New NTSInformatica.NTSGridColumn
    Me.TabPage2 = New NTSInformatica.NTSTabPage
    Me.pnTab2Pan2 = New NTSInformatica.NTSPanel
    Me.cmdClassificaDeleteFilter = New NTSInformatica.NTSButton
    Me.cmdClassifica = New NTSInformatica.NTSButton
    Me.lbClassifica = New NTSInformatica.NTSLabel
    Me.lbMagprod = New NTSInformatica.NTSLabel
    Me.edMagprodfin = New NTSInformatica.NTSTextBoxNum
    Me.edMagprodini = New NTSInformatica.NTSTextBoxNum
    Me.lbMagstock = New NTSInformatica.NTSLabel
    Me.edMagstockfin = New NTSInformatica.NTSTextBoxNum
    Me.edMagstockini = New NTSInformatica.NTSTextBoxNum
    Me.lbCodIvada = New NTSInformatica.NTSLabel
    Me.edClassificazioneLivello5 = New NTSInformatica.NTSTextBoxStr
    Me.edClassificazioneLivello4 = New NTSInformatica.NTSTextBoxStr
    Me.edClassificazioneLivello3 = New NTSInformatica.NTSTextBoxStr
    Me.edClassificazioneLivello2 = New NTSInformatica.NTSTextBoxStr
    Me.edClassificazioneLivello1 = New NTSInformatica.NTSTextBoxStr
    Me.lbApprovda = New NTSInformatica.NTSLabel
    Me.edCodIvaa = New NTSInformatica.NTSTextBoxNum
    Me.edApprova = New NTSInformatica.NTSTextBoxNum
    Me.edCodIvad = New NTSInformatica.NTSTextBoxNum
    Me.edApprovd = New NTSInformatica.NTSTextBoxNum
    Me.lbScontida = New NTSInformatica.NTSLabel
    Me.lbProvvda = New NTSInformatica.NTSLabel
    Me.edScontia = New NTSInformatica.NTSTextBoxNum
    Me.edProvva = New NTSInformatica.NTSTextBoxNum
    Me.edScontid = New NTSInformatica.NTSTextBoxNum
    Me.edProvvd = New NTSInformatica.NTSTextBoxNum
    Me.lbMarcada = New NTSInformatica.NTSLabel
    Me.edMarcaa = New NTSInformatica.NTSTextBoxNum
    Me.edMarcad = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel3 = New NTSInformatica.NTSLabel
    Me.NtsLabel4 = New NTSInformatica.NTSLabel
    Me.pnTab2Pan1 = New NTSInformatica.NTSPanel
    Me.lbCodtipada = New NTSInformatica.NTSLabel
    Me.edCodtipaa = New NTSInformatica.NTSTextBoxNum
    Me.edCodtipad = New NTSInformatica.NTSTextBoxNum
    Me.edForna = New NTSInformatica.NTSTextBoxNum
    Me.edFornd = New NTSInformatica.NTSTextBoxNum
    Me.lbFornda = New NTSInformatica.NTSLabel
    Me.edDataUltaga = New NTSInformatica.NTSTextBoxData
    Me.edFamproda = New NTSInformatica.NTSTextBoxStr
    Me.edDataUltagd = New NTSInformatica.NTSTextBoxData
    Me.NtsLabel26 = New NTSInformatica.NTSLabel
    Me.edFamprodd = New NTSInformatica.NTSTextBoxStr
    Me.lbFamigliada = New NTSInformatica.NTSLabel
    Me.edDescra = New NTSInformatica.NTSTextBoxStr
    Me.lbSottogruppoda = New NTSInformatica.NTSLabel
    Me.edDescrd = New NTSInformatica.NTSTextBoxStr
    Me.edSotta = New NTSInformatica.NTSTextBoxNum
    Me.NtsLabel2 = New NTSInformatica.NTSLabel
    Me.edSottd = New NTSInformatica.NTSTextBoxNum
    Me.lbGruppoda = New NTSInformatica.NTSLabel
    Me.edGruppoa = New NTSInformatica.NTSTextBoxNum
    Me.edGruppod = New NTSInformatica.NTSTextBoxNum
    Me.edCodalta = New NTSInformatica.NTSTextBoxStr
    Me.edCodaltd = New NTSInformatica.NTSTextBoxStr
    Me.lbCodaltda = New NTSInformatica.NTSLabel
    Me.edCodarta = New NTSInformatica.NTSTextBoxStr
    Me.edCodartd = New NTSInformatica.NTSTextBoxStr
    Me.lbCodartda = New NTSInformatica.NTSLabel
    Me.NtsLabel11 = New NTSInformatica.NTSLabel
    Me.NtsLabel10 = New NTSInformatica.NTSLabel
    Me.TabPage3 = New NTSInformatica.NTSTabPage
    Me.pnTab3Pan1 = New NTSInformatica.NTSPanel
    Me.NtsLabel5 = New NTSInformatica.NTSLabel
    Me.NtsLabel1 = New NTSInformatica.NTSLabel
    Me.edListsar = New NTSInformatica.NTSTextBoxNum
    Me.edArtprom = New NTSInformatica.NTSTextBoxNum
    Me.lbDBLike = New NTSInformatica.NTSLabel
    Me.edDBLike = New NTSInformatica.NTSTextBoxStr
    Me.edTipo = New NTSInformatica.NTSTextBoxStr
    Me.lbTipo = New NTSInformatica.NTSLabel
    Me.cbAfasi = New NTSInformatica.NTSComboBox
    Me.cbCritico = New NTSInformatica.NTSComboBox
    Me.cbAlistino = New NTSInformatica.NTSComboBox
    Me.cbInesaur = New NTSInformatica.NTSComboBox
    Me.lbAfasi = New NTSInformatica.NTSLabel
    Me.lbCritico = New NTSInformatica.NTSLabel
    Me.lbAlistino = New NTSInformatica.NTSLabel
    Me.lbInesaur = New NTSInformatica.NTSLabel
    Me.cbAvarianti = New NTSInformatica.NTSComboBox
    Me.cbUbicaz = New NTSInformatica.NTSComboBox
    Me.cbMatricole = New NTSInformatica.NTSComboBox
    Me.cbCommessa = New NTSInformatica.NTSComboBox
    Me.cbLotti = New NTSInformatica.NTSComboBox
    Me.lbAvarianti = New NTSInformatica.NTSLabel
    Me.lbUbicaz = New NTSInformatica.NTSLabel
    Me.lbMatricole = New NTSInformatica.NTSLabel
    Me.lbCommessa = New NTSInformatica.NTSLabel
    Me.lbLotti = New NTSInformatica.NTSLabel
    Me.lbCodtaglda = New NTSInformatica.NTSLabel
    Me.edCodtagla = New NTSInformatica.NTSTextBoxNum
    Me.edCodtagld = New NTSInformatica.NTSTextBoxNum
    Me.lbCodstagda = New NTSInformatica.NTSLabel
    Me.edCodstaga = New NTSInformatica.NTSTextBoxNum
    Me.edCodstagd = New NTSInformatica.NTSTextBoxNum
    Me.lbAnnoda = New NTSInformatica.NTSLabel
    Me.edAnnoa = New NTSInformatica.NTSTextBoxNum
    Me.edAnnod = New NTSInformatica.NTSTextBoxNum
    Me.pnTab3Pan2 = New NTSInformatica.NTSPanel
    Me.fmPrezzi = New NTSInformatica.NTSGroupBox
    Me.edDtvalid = New NTSInformatica.NTSTextBoxData
    Me.lbListvalidita = New NTSInformatica.NTSLabel
    Me.edListino = New NTSInformatica.NTSTextBoxNum
    Me.lbListino = New NTSInformatica.NTSLabel
    Me.ckVisprezzi = New NTSInformatica.NTSCheckBox
    Me.fmSuccedanei = New NTSInformatica.NTSGroupBox
    Me.edCodartAcc = New NTSInformatica.NTSTextBoxStr
    Me.lbCodartAcc = New NTSInformatica.NTSLabel
    Me.optSuccedanei = New NTSInformatica.NTSRadioButton
    Me.optAccessori = New NTSInformatica.NTSRadioButton
    Me.ckSuccedanei = New NTSInformatica.NTSCheckBox
    Me.fmCliforn = New NTSInformatica.NTSGroupBox
    Me.ckFiltraConto = New NTSInformatica.NTSCheckBox
    Me.ckFiltraMovmag = New NTSInformatica.NTSCheckBox
    Me.lbConto = New NTSInformatica.NTSLabel
    Me.edConto = New NTSInformatica.NTSTextBoxNum
    Me.TabPage4 = New NTSInformatica.NTSTabPage
    Me.pnTab4Pan1 = New NTSInformatica.NTSPanel
    Me.imArtGif = New NTSInformatica.NTSPictureBox
    Me.pnAction = New NTSInformatica.NTSPanel
    Me.cmdEstensioni = New NTSInformatica.NTSButton
    Me.cmdLastfilter = New NTSInformatica.NTSButton
    Me.cmdProgressivi = New NTSInformatica.NTSButton
    Me.cmdOrdini = New NTSInformatica.NTSButton
    Me.cmdListini = New NTSInformatica.NTSButton
    Me.cmdMovimenti = New NTSInformatica.NTSButton
    Me.ckOttimistico = New NTSInformatica.NTSCheckBox
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdGestione = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.grZoom = New NTSInformatica.NTSGrid
    Me.grvZoom = New NTSInformatica.NTSGridView
    Me.xx_seleziona = New NTSInformatica.NTSGridColumn
    Me.ar_codart = New NTSInformatica.NTSGridColumn
    Me.xx_codarfo = New NTSInformatica.NTSGridColumn
    Me.ar_descr = New NTSInformatica.NTSGridColumn
    Me.ar_unmis = New NTSInformatica.NTSGridColumn
    Me.xx_esist = New NTSInformatica.NTSGridColumn
    Me.xx_prezzo = New NTSInformatica.NTSGridColumn
    Me.ar_codalt = New NTSInformatica.NTSGridColumn
    Me.xx_code = New NTSInformatica.NTSGridColumn
    Me.xx_prenot = New NTSInformatica.NTSGridColumn
    Me.xx_ordin = New NTSInformatica.NTSGridColumn
    Me.xx_impegn = New NTSInformatica.NTSGridColumn
    Me.xx_dispnet = New NTSInformatica.NTSGridColumn
    Me.xx_dispon = New NTSInformatica.NTSGridColumn
    Me.xx_dispo2 = New NTSInformatica.NTSGridColumn
    Me.ar_desint = New NTSInformatica.NTSGridColumn
    Me.ar_sostit = New NTSInformatica.NTSGridColumn
    Me.ar_sostituito = New NTSInformatica.NTSGridColumn
    Me.ar_inesaur = New NTSInformatica.NTSGridColumn
    Me.ar_stalist = New NTSInformatica.NTSGridColumn
    Me.ar_note = New NTSInformatica.NTSGridColumn
    Me.xx_fase = New NTSInformatica.NTSGridColumn
    Me.xx_descr = New NTSInformatica.NTSGridColumn
    Me.ar_blocco = New NTSInformatica.NTSGridColumn
    Me.pnClassificazione = New NTSInformatica.NTSPanel
    Me.trClass = New NTSInformatica.NTSTreeView
    Me.cmdFiltriClassificazione = New NTSInformatica.NTSButton
    Me.ckCodiciRoot = New NTSInformatica.NTSCheckBox
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnDescr.SuspendLayout()
    CType(Me.tsZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsZoom.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    CType(Me.pnTab1Pan2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab1Pan2.SuspendLayout()
    CType(Me.ckAbituali.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTestArt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodartLike.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edUbicLike.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescrLike.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckBloccati.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipologia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTestdb.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodarfo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edBarcode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTab1Pan1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab1Pan1.SuspendLayout()
    CType(Me.grFiltri1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFiltri1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage2.SuspendLayout()
    CType(Me.pnTab2Pan2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab2Pan2.SuspendLayout()
    CType(Me.edMagprodfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMagprodini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMagstockfin.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMagstockini.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello5.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello4.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edClassificazioneLivello1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodIvaa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edApprova.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodIvad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edApprovd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edScontia.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edProvva.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edScontid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edProvvd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMarcaa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edMarcad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTab2Pan1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab2Pan1.SuspendLayout()
    CType(Me.edCodtipaa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodtipad.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edForna.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFornd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataUltaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFamproda.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataUltagd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFamprodd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescra.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDescrd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSotta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSottd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edGruppoa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edGruppod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodalta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodaltd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodarta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodartd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage3.SuspendLayout()
    CType(Me.pnTab3Pan1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab3Pan1.SuspendLayout()
    CType(Me.edListsar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edArtprom.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDBLike.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAfasi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbCritico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAlistino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbInesaur.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAvarianti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbUbicaz.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbMatricole.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbCommessa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbLotti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodtagla.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodtagld.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodstaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCodstagd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnoa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAnnod.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTab3Pan2, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab3Pan2.SuspendLayout()
    CType(Me.fmPrezzi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmPrezzi.SuspendLayout()
    CType(Me.edDtvalid.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckVisprezzi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmSuccedanei, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmSuccedanei.SuspendLayout()
    CType(Me.edCodartAcc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optSuccedanei.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optAccessori.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSuccedanei.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmCliforn, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmCliforn.SuspendLayout()
    CType(Me.ckFiltraConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckFiltraMovmag.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage4.SuspendLayout()
    CType(Me.pnTab4Pan1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTab4Pan1.SuspendLayout()
    CType(Me.imArtGif.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAction.SuspendLayout()
    CType(Me.ckOttimistico.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnClassificazione, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnClassificazione.SuspendLayout()
    CType(Me.ckCodiciRoot.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.pnDescr.Size = New System.Drawing.Size(788, 266)
    Me.pnDescr.TabIndex = 1
    '
    'tsZoom
    '
    Me.tsZoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.tsZoom.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsZoom.Location = New System.Drawing.Point(0, 0)
    Me.tsZoom.Margin = New System.Windows.Forms.Padding(0)
    Me.tsZoom.Name = "tsZoom"
    Me.tsZoom.SelectedTabPage = Me.TabPage3
    Me.tsZoom.Size = New System.Drawing.Size(678, 266)
    Me.tsZoom.TabIndex = 4
    Me.tsZoom.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabPage1, Me.TabPage2, Me.TabPage3, Me.TabPage4})
    '
    'TabPage1
    '
    Me.TabPage1.AllowDrop = True
    Me.TabPage1.Controls.Add(Me.pnTab1Pan2)
    Me.TabPage1.Controls.Add(Me.pnTab1Pan1)
    Me.TabPage1.Enable = True
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Size = New System.Drawing.Size(669, 236)
    Me.TabPage1.Text = "G&enerale"
    '
    'pnTab1Pan2
    '
    Me.pnTab1Pan2.AllowDrop = True
    Me.pnTab1Pan2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab1Pan2.Appearance.Options.UseBackColor = True
    Me.pnTab1Pan2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab1Pan2.Controls.Add(Me.ckAbituali)
    Me.pnTab1Pan2.Controls.Add(Me.cbTestArt)
    Me.pnTab1Pan2.Controls.Add(Me.lbTestArt)
    Me.pnTab1Pan2.Controls.Add(Me.lbCodartLike)
    Me.pnTab1Pan2.Controls.Add(Me.edCodartLike)
    Me.pnTab1Pan2.Controls.Add(Me.lbUbicLike)
    Me.pnTab1Pan2.Controls.Add(Me.edUbicLike)
    Me.pnTab1Pan2.Controls.Add(Me.lbDescrLike)
    Me.pnTab1Pan2.Controls.Add(Me.edDescrLike)
    Me.pnTab1Pan2.Controls.Add(Me.ckBloccati)
    Me.pnTab1Pan2.Controls.Add(Me.cbTipologia)
    Me.pnTab1Pan2.Controls.Add(Me.lbTipologia)
    Me.pnTab1Pan2.Controls.Add(Me.edMagaz)
    Me.pnTab1Pan2.Controls.Add(Me.lbMagaz)
    Me.pnTab1Pan2.Controls.Add(Me.cbTestdb)
    Me.pnTab1Pan2.Controls.Add(Me.lbTestdb)
    Me.pnTab1Pan2.Controls.Add(Me.cmdLock)
    Me.pnTab1Pan2.Controls.Add(Me.lbCodarfo)
    Me.pnTab1Pan2.Controls.Add(Me.edCodarfo)
    Me.pnTab1Pan2.Controls.Add(Me.edBarcode)
    Me.pnTab1Pan2.Controls.Add(Me.lbBarcode)
    Me.pnTab1Pan2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab1Pan2.Location = New System.Drawing.Point(357, 0)
    Me.pnTab1Pan2.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab1Pan2.Name = "pnTab1Pan2"
    Me.pnTab1Pan2.NTSActiveTrasparency = True
    Me.pnTab1Pan2.Size = New System.Drawing.Size(312, 236)
    Me.pnTab1Pan2.TabIndex = 3
    '
    'ckAbituali
    '
    Me.ckAbituali.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAbituali.Location = New System.Drawing.Point(117, 216)
    Me.ckAbituali.Name = "ckAbituali"
    Me.ckAbituali.NTSCheckValue = "S"
    Me.ckAbituali.NTSUnCheckValue = "N"
    Me.ckAbituali.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAbituali.Properties.Appearance.Options.UseBackColor = True
    Me.ckAbituali.Properties.AutoHeight = False
    Me.ckAbituali.Properties.Caption = "Articoli movimentati ultimi 6 mesi"
    Me.ckAbituali.Size = New System.Drawing.Size(195, 19)
    Me.ckAbituali.TabIndex = 25
    '
    'cbTestArt
    '
    Me.cbTestArt.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTestArt.DataSource = Nothing
    Me.cbTestArt.DisplayMember = ""
    Me.cbTestArt.Location = New System.Drawing.Point(113, 75)
    Me.cbTestArt.Name = "cbTestArt"
    Me.cbTestArt.NTSDbField = ""
    Me.cbTestArt.Properties.AutoHeight = False
    Me.cbTestArt.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTestArt.Properties.DropDownRows = 30
    Me.cbTestArt.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTestArt.SelectedValue = ""
    Me.cbTestArt.Size = New System.Drawing.Size(196, 20)
    Me.cbTestArt.TabIndex = 11
    Me.cbTestArt.ValueMember = ""
    '
    'lbTestArt
    '
    Me.lbTestArt.AutoSize = True
    Me.lbTestArt.BackColor = System.Drawing.Color.Transparent
    Me.lbTestArt.Location = New System.Drawing.Point(6, 78)
    Me.lbTestArt.Name = "lbTestArt"
    Me.lbTestArt.NTSDbField = ""
    Me.lbTestArt.Size = New System.Drawing.Size(87, 13)
    Me.lbTestArt.TabIndex = 24
    Me.lbTestArt.Text = "Test tipo articolo"
    Me.lbTestArt.Tooltip = ""
    Me.lbTestArt.UseMnemonic = False
    '
    'lbCodartLike
    '
    Me.lbCodartLike.AutoSize = True
    Me.lbCodartLike.BackColor = System.Drawing.Color.Transparent
    Me.lbCodartLike.Location = New System.Drawing.Point(6, 144)
    Me.lbCodartLike.Name = "lbCodartLike"
    Me.lbCodartLike.NTSDbField = ""
    Me.lbCodartLike.Size = New System.Drawing.Size(95, 13)
    Me.lbCodartLike.TabIndex = 21
    Me.lbCodartLike.Text = "Codice articolo like"
    Me.lbCodartLike.Tooltip = ""
    Me.lbCodartLike.UseMnemonic = False
    '
    'edCodartLike
    '
    Me.edCodartLike.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodartLike.Location = New System.Drawing.Point(113, 141)
    Me.edCodartLike.Name = "edCodartLike"
    Me.edCodartLike.NTSDbField = ""
    Me.edCodartLike.NTSForzaVisZoom = False
    Me.edCodartLike.NTSOldValue = ""
    Me.edCodartLike.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodartLike.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodartLike.Properties.AutoHeight = False
    Me.edCodartLike.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodartLike.Properties.MaxLength = 65536
    Me.edCodartLike.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodartLike.Size = New System.Drawing.Size(196, 20)
    Me.edCodartLike.TabIndex = 20
    '
    'lbUbicLike
    '
    Me.lbUbicLike.AutoSize = True
    Me.lbUbicLike.BackColor = System.Drawing.Color.Transparent
    Me.lbUbicLike.Location = New System.Drawing.Point(6, 188)
    Me.lbUbicLike.Name = "lbUbicLike"
    Me.lbUbicLike.NTSDbField = ""
    Me.lbUbicLike.Size = New System.Drawing.Size(70, 13)
    Me.lbUbicLike.TabIndex = 19
    Me.lbUbicLike.Text = "Ubic. art. like"
    Me.lbUbicLike.Tooltip = ""
    Me.lbUbicLike.UseMnemonic = False
    '
    'edUbicLike
    '
    Me.edUbicLike.Cursor = System.Windows.Forms.Cursors.Default
    Me.edUbicLike.Location = New System.Drawing.Point(113, 185)
    Me.edUbicLike.Name = "edUbicLike"
    Me.edUbicLike.NTSDbField = ""
    Me.edUbicLike.NTSForzaVisZoom = False
    Me.edUbicLike.NTSOldValue = ""
    Me.edUbicLike.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edUbicLike.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edUbicLike.Properties.AutoHeight = False
    Me.edUbicLike.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edUbicLike.Properties.MaxLength = 65536
    Me.edUbicLike.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edUbicLike.Size = New System.Drawing.Size(196, 20)
    Me.edUbicLike.TabIndex = 18
    '
    'lbDescrLike
    '
    Me.lbDescrLike.AutoSize = True
    Me.lbDescrLike.BackColor = System.Drawing.Color.Transparent
    Me.lbDescrLike.Location = New System.Drawing.Point(6, 166)
    Me.lbDescrLike.Name = "lbDescrLike"
    Me.lbDescrLike.NTSDbField = ""
    Me.lbDescrLike.Size = New System.Drawing.Size(80, 13)
    Me.lbDescrLike.TabIndex = 17
    Me.lbDescrLike.Text = "Descr. art.  like"
    Me.lbDescrLike.Tooltip = ""
    Me.lbDescrLike.UseMnemonic = False
    '
    'edDescrLike
    '
    Me.edDescrLike.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDescrLike.Location = New System.Drawing.Point(113, 163)
    Me.edDescrLike.Name = "edDescrLike"
    Me.edDescrLike.NTSDbField = ""
    Me.edDescrLike.NTSForzaVisZoom = False
    Me.edDescrLike.NTSOldValue = ""
    Me.edDescrLike.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescrLike.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescrLike.Properties.AutoHeight = False
    Me.edDescrLike.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescrLike.Properties.MaxLength = 65536
    Me.edDescrLike.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescrLike.Size = New System.Drawing.Size(196, 20)
    Me.edDescrLike.TabIndex = 16
    '
    'ckBloccati
    '
    Me.ckBloccati.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckBloccati.EditValue = True
    Me.ckBloccati.Location = New System.Drawing.Point(169, 98)
    Me.ckBloccati.Name = "ckBloccati"
    Me.ckBloccati.NTSCheckValue = "S"
    Me.ckBloccati.NTSUnCheckValue = "N"
    Me.ckBloccati.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckBloccati.Properties.Appearance.Options.UseBackColor = True
    Me.ckBloccati.Properties.AutoHeight = False
    Me.ckBloccati.Properties.Caption = "Visualizza articoli bloccati"
    Me.ckBloccati.Size = New System.Drawing.Size(139, 19)
    Me.ckBloccati.TabIndex = 15
    '
    'cbTipologia
    '
    Me.cbTipologia.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipologia.DataSource = Nothing
    Me.cbTipologia.DisplayMember = ""
    Me.cbTipologia.Location = New System.Drawing.Point(113, 119)
    Me.cbTipologia.Name = "cbTipologia"
    Me.cbTipologia.NTSDbField = ""
    Me.cbTipologia.Properties.AutoHeight = False
    Me.cbTipologia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipologia.Properties.DropDownRows = 30
    Me.cbTipologia.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipologia.SelectedValue = ""
    Me.cbTipologia.Size = New System.Drawing.Size(196, 20)
    Me.cbTipologia.TabIndex = 14
    Me.cbTipologia.ValueMember = ""
    '
    'lbTipologia
    '
    Me.lbTipologia.AutoSize = True
    Me.lbTipologia.BackColor = System.Drawing.Color.Transparent
    Me.lbTipologia.Location = New System.Drawing.Point(6, 122)
    Me.lbTipologia.Name = "lbTipologia"
    Me.lbTipologia.NTSDbField = ""
    Me.lbTipologia.Size = New System.Drawing.Size(87, 13)
    Me.lbTipologia.TabIndex = 13
    Me.lbTipologia.Text = "Tipologia articolo"
    Me.lbTipologia.Tooltip = ""
    Me.lbTipologia.UseMnemonic = False
    '
    'edMagaz
    '
    Me.edMagaz.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagaz.EditValue = "0"
    Me.edMagaz.Location = New System.Drawing.Point(113, 97)
    Me.edMagaz.Name = "edMagaz"
    Me.edMagaz.NTSDbField = ""
    Me.edMagaz.NTSFormat = "0"
    Me.edMagaz.NTSForzaVisZoom = False
    Me.edMagaz.NTSOldValue = ""
    Me.edMagaz.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagaz.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagaz.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagaz.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagaz.Properties.AutoHeight = False
    Me.edMagaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagaz.Properties.MaxLength = 65536
    Me.edMagaz.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagaz.Size = New System.Drawing.Size(51, 20)
    Me.edMagaz.TabIndex = 12
    '
    'lbMagaz
    '
    Me.lbMagaz.AutoSize = True
    Me.lbMagaz.BackColor = System.Drawing.Color.Transparent
    Me.lbMagaz.Location = New System.Drawing.Point(6, 100)
    Me.lbMagaz.Name = "lbMagaz"
    Me.lbMagaz.NTSDbField = ""
    Me.lbMagaz.Size = New System.Drawing.Size(57, 13)
    Me.lbMagaz.TabIndex = 11
    Me.lbMagaz.Text = "Magazzino"
    Me.lbMagaz.Tooltip = ""
    Me.lbMagaz.UseMnemonic = False
    '
    'cbTestdb
    '
    Me.cbTestdb.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTestdb.DataSource = Nothing
    Me.cbTestdb.DisplayMember = ""
    Me.cbTestdb.Location = New System.Drawing.Point(113, 53)
    Me.cbTestdb.Name = "cbTestdb"
    Me.cbTestdb.NTSDbField = ""
    Me.cbTestdb.Properties.AutoHeight = False
    Me.cbTestdb.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTestdb.Properties.DropDownRows = 30
    Me.cbTestdb.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTestdb.SelectedValue = ""
    Me.cbTestdb.Size = New System.Drawing.Size(196, 20)
    Me.cbTestdb.TabIndex = 10
    Me.cbTestdb.ValueMember = ""
    '
    'lbTestdb
    '
    Me.lbTestdb.AutoSize = True
    Me.lbTestdb.BackColor = System.Drawing.Color.Transparent
    Me.lbTestdb.Location = New System.Drawing.Point(6, 56)
    Me.lbTestdb.Name = "lbTestdb"
    Me.lbTestdb.NTSDbField = ""
    Me.lbTestdb.Size = New System.Drawing.Size(92, 13)
    Me.lbTestdb.TabIndex = 9
    Me.lbTestdb.Text = "Test distinta base"
    Me.lbTestdb.Tooltip = ""
    Me.lbTestdb.UseMnemonic = False
    '
    'cmdLock
    '
    Me.cmdLock.ImagePath = ""
    Me.cmdLock.ImageText = ""
    Me.cmdLock.Location = New System.Drawing.Point(3, 216)
    Me.cmdLock.Name = "cmdLock"
    Me.cmdLock.NTSContextMenu = Nothing
    Me.cmdLock.Size = New System.Drawing.Size(111, 20)
    Me.cmdLock.TabIndex = 8
    Me.cmdLock.Text = "Blocca/sblocca filtri"
    '
    'lbCodarfo
    '
    Me.lbCodarfo.AutoSize = True
    Me.lbCodarfo.BackColor = System.Drawing.Color.Transparent
    Me.lbCodarfo.Location = New System.Drawing.Point(6, 34)
    Me.lbCodarfo.Name = "lbCodarfo"
    Me.lbCodarfo.NTSDbField = ""
    Me.lbCodarfo.Size = New System.Drawing.Size(96, 13)
    Me.lbCodarfo.TabIndex = 3
    Me.lbCodarfo.Text = "Codice art. cli/forn"
    Me.lbCodarfo.Tooltip = ""
    Me.lbCodarfo.UseMnemonic = False
    '
    'edCodarfo
    '
    Me.edCodarfo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodarfo.Location = New System.Drawing.Point(113, 31)
    Me.edCodarfo.Name = "edCodarfo"
    Me.edCodarfo.NTSDbField = ""
    Me.edCodarfo.NTSForzaVisZoom = False
    Me.edCodarfo.NTSOldValue = ""
    Me.edCodarfo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodarfo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodarfo.Properties.AutoHeight = False
    Me.edCodarfo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodarfo.Properties.MaxLength = 65536
    Me.edCodarfo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodarfo.Size = New System.Drawing.Size(196, 20)
    Me.edCodarfo.TabIndex = 2
    '
    'edBarcode
    '
    Me.edBarcode.Cursor = System.Windows.Forms.Cursors.Default
    Me.edBarcode.Location = New System.Drawing.Point(113, 9)
    Me.edBarcode.Name = "edBarcode"
    Me.edBarcode.NTSDbField = ""
    Me.edBarcode.NTSForzaVisZoom = False
    Me.edBarcode.NTSOldValue = ""
    Me.edBarcode.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edBarcode.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edBarcode.Properties.AutoHeight = False
    Me.edBarcode.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edBarcode.Properties.MaxLength = 65536
    Me.edBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edBarcode.Size = New System.Drawing.Size(196, 20)
    Me.edBarcode.TabIndex = 1
    '
    'lbBarcode
    '
    Me.lbBarcode.AutoSize = True
    Me.lbBarcode.BackColor = System.Drawing.Color.Transparent
    Me.lbBarcode.Location = New System.Drawing.Point(6, 12)
    Me.lbBarcode.Name = "lbBarcode"
    Me.lbBarcode.NTSDbField = ""
    Me.lbBarcode.Size = New System.Drawing.Size(77, 13)
    Me.lbBarcode.TabIndex = 0
    Me.lbBarcode.Text = "Codice a barre"
    Me.lbBarcode.Tooltip = ""
    Me.lbBarcode.UseMnemonic = False
    '
    'pnTab1Pan1
    '
    Me.pnTab1Pan1.AllowDrop = True
    Me.pnTab1Pan1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab1Pan1.Appearance.Options.UseBackColor = True
    Me.pnTab1Pan1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab1Pan1.Controls.Add(Me.grFiltri1)
    Me.pnTab1Pan1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab1Pan1.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnTab1Pan1.Location = New System.Drawing.Point(0, 0)
    Me.pnTab1Pan1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTab1Pan1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTab1Pan1.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab1Pan1.Name = "pnTab1Pan1"
    Me.pnTab1Pan1.NTSActiveTrasparency = True
    Me.pnTab1Pan1.Size = New System.Drawing.Size(357, 236)
    Me.pnTab1Pan1.TabIndex = 2
    '
    'grFiltri1
    '
    Me.grFiltri1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grFiltri1.EmbeddedNavigator.Name = ""
    Me.grFiltri1.Location = New System.Drawing.Point(0, 0)
    Me.grFiltri1.MainView = Me.grvFiltri1
    Me.grFiltri1.Name = "grFiltri1"
    Me.grFiltri1.Size = New System.Drawing.Size(357, 236)
    Me.grFiltri1.TabIndex = 5
    Me.grFiltri1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvFiltri1})
    '
    'grvFiltri1
    '
    Me.grvFiltri1.ActiveFilterEnabled = False
    Me.grvFiltri1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_nome, Me.xx_valore})
    Me.grvFiltri1.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvFiltri1.Enabled = True
    Me.grvFiltri1.GridControl = Me.grFiltri1
    Me.grvFiltri1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvFiltri1.MinRowHeight = 14
    Me.grvFiltri1.Name = "grvFiltri1"
    Me.grvFiltri1.NTSAllowDelete = True
    Me.grvFiltri1.NTSAllowInsert = True
    Me.grvFiltri1.NTSAllowUpdate = True
    Me.grvFiltri1.NTSMenuContext = Nothing
    Me.grvFiltri1.OptionsCustomization.AllowRowSizing = True
    Me.grvFiltri1.OptionsFilter.AllowFilterEditor = False
    Me.grvFiltri1.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvFiltri1.OptionsNavigation.UseTabKey = False
    Me.grvFiltri1.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvFiltri1.OptionsView.ColumnAutoWidth = False
    Me.grvFiltri1.OptionsView.EnableAppearanceEvenRow = True
    Me.grvFiltri1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvFiltri1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvFiltri1.OptionsView.ShowGroupPanel = False
    Me.grvFiltri1.RowHeight = 16
    '
    'xx_nome
    '
    Me.xx_nome.AppearanceCell.Options.UseBackColor = True
    Me.xx_nome.AppearanceCell.Options.UseTextOptions = True
    Me.xx_nome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_nome.Caption = "Filtro"
    Me.xx_nome.Enabled = False
    Me.xx_nome.FieldName = "xx_nome"
    Me.xx_nome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_nome.Name = "xx_nome"
    Me.xx_nome.NTSRepositoryComboBox = Nothing
    Me.xx_nome.NTSRepositoryItemCheck = Nothing
    Me.xx_nome.NTSRepositoryItemMemo = Nothing
    Me.xx_nome.NTSRepositoryItemText = Nothing
    Me.xx_nome.OptionsColumn.AllowEdit = False
    Me.xx_nome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_nome.OptionsColumn.ReadOnly = True
    Me.xx_nome.OptionsFilter.AllowFilter = False
    Me.xx_nome.Visible = True
    Me.xx_nome.VisibleIndex = 0
    Me.xx_nome.Width = 178
    '
    'xx_valore
    '
    Me.xx_valore.AppearanceCell.Options.UseBackColor = True
    Me.xx_valore.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valore.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valore.Caption = "Valore"
    Me.xx_valore.Enabled = True
    Me.xx_valore.FieldName = "xx_valore"
    Me.xx_valore.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valore.Name = "xx_valore"
    Me.xx_valore.NTSRepositoryComboBox = Nothing
    Me.xx_valore.NTSRepositoryItemCheck = Nothing
    Me.xx_valore.NTSRepositoryItemMemo = Nothing
    Me.xx_valore.NTSRepositoryItemText = Nothing
    Me.xx_valore.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_valore.OptionsFilter.AllowFilter = False
    Me.xx_valore.Visible = True
    Me.xx_valore.VisibleIndex = 1
    Me.xx_valore.Width = 300
    '
    'TabPage2
    '
    Me.TabPage2.AllowDrop = True
    Me.TabPage2.Controls.Add(Me.pnTab2Pan2)
    Me.TabPage2.Controls.Add(Me.pnTab2Pan1)
    Me.TabPage2.Enable = True
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Size = New System.Drawing.Size(669, 236)
    Me.TabPage2.Text = "Filtri Da / a"
    '
    'pnTab2Pan2
    '
    Me.pnTab2Pan2.AllowDrop = True
    Me.pnTab2Pan2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab2Pan2.Appearance.Options.UseBackColor = True
    Me.pnTab2Pan2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab2Pan2.Controls.Add(Me.cmdClassificaDeleteFilter)
    Me.pnTab2Pan2.Controls.Add(Me.cmdClassifica)
    Me.pnTab2Pan2.Controls.Add(Me.lbClassifica)
    Me.pnTab2Pan2.Controls.Add(Me.lbMagprod)
    Me.pnTab2Pan2.Controls.Add(Me.edMagprodfin)
    Me.pnTab2Pan2.Controls.Add(Me.edMagprodini)
    Me.pnTab2Pan2.Controls.Add(Me.lbMagstock)
    Me.pnTab2Pan2.Controls.Add(Me.edMagstockfin)
    Me.pnTab2Pan2.Controls.Add(Me.edMagstockini)
    Me.pnTab2Pan2.Controls.Add(Me.lbCodIvada)
    Me.pnTab2Pan2.Controls.Add(Me.edClassificazioneLivello5)
    Me.pnTab2Pan2.Controls.Add(Me.edClassificazioneLivello4)
    Me.pnTab2Pan2.Controls.Add(Me.edClassificazioneLivello3)
    Me.pnTab2Pan2.Controls.Add(Me.edClassificazioneLivello2)
    Me.pnTab2Pan2.Controls.Add(Me.edClassificazioneLivello1)
    Me.pnTab2Pan2.Controls.Add(Me.lbApprovda)
    Me.pnTab2Pan2.Controls.Add(Me.edCodIvaa)
    Me.pnTab2Pan2.Controls.Add(Me.edApprova)
    Me.pnTab2Pan2.Controls.Add(Me.edCodIvad)
    Me.pnTab2Pan2.Controls.Add(Me.edApprovd)
    Me.pnTab2Pan2.Controls.Add(Me.lbScontida)
    Me.pnTab2Pan2.Controls.Add(Me.lbProvvda)
    Me.pnTab2Pan2.Controls.Add(Me.edScontia)
    Me.pnTab2Pan2.Controls.Add(Me.edProvva)
    Me.pnTab2Pan2.Controls.Add(Me.edScontid)
    Me.pnTab2Pan2.Controls.Add(Me.edProvvd)
    Me.pnTab2Pan2.Controls.Add(Me.lbMarcada)
    Me.pnTab2Pan2.Controls.Add(Me.edMarcaa)
    Me.pnTab2Pan2.Controls.Add(Me.edMarcad)
    Me.pnTab2Pan2.Controls.Add(Me.NtsLabel3)
    Me.pnTab2Pan2.Controls.Add(Me.NtsLabel4)
    Me.pnTab2Pan2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab2Pan2.Location = New System.Drawing.Point(338, 3)
    Me.pnTab2Pan2.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab2Pan2.Name = "pnTab2Pan2"
    Me.pnTab2Pan2.NTSActiveTrasparency = True
    Me.pnTab2Pan2.Size = New System.Drawing.Size(329, 236)
    Me.pnTab2Pan2.TabIndex = 4
    '
    'cmdClassificaDeleteFilter
    '
    Me.cmdClassificaDeleteFilter.Image = CType(resources.GetObject("cmdClassificaDeleteFilter.Image"), System.Drawing.Image)
    Me.cmdClassificaDeleteFilter.ImagePath = ""
    Me.cmdClassificaDeleteFilter.ImageText = ""
    Me.cmdClassificaDeleteFilter.Location = New System.Drawing.Point(33, 207)
    Me.cmdClassificaDeleteFilter.Name = "cmdClassificaDeleteFilter"
    Me.cmdClassificaDeleteFilter.NTSContextMenu = Nothing
    Me.cmdClassificaDeleteFilter.Size = New System.Drawing.Size(28, 22)
    Me.cmdClassificaDeleteFilter.TabIndex = 679
    Me.cmdClassificaDeleteFilter.ToolTip = "Rimuovi il fitro relativo alla classificazione"
    '
    'cmdClassifica
    '
    Me.cmdClassifica.ImagePath = ""
    Me.cmdClassifica.ImageText = ""
    Me.cmdClassifica.Location = New System.Drawing.Point(3, 184)
    Me.cmdClassifica.Name = "cmdClassifica"
    Me.cmdClassifica.NTSContextMenu = Nothing
    Me.cmdClassifica.Size = New System.Drawing.Size(58, 22)
    Me.cmdClassifica.TabIndex = 679
    Me.cmdClassifica.Text = "Classifica"
    '
    'lbClassifica
    '
    Me.lbClassifica.BackColor = System.Drawing.Color.Transparent
    Me.lbClassifica.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbClassifica.Location = New System.Drawing.Point(63, 184)
    Me.lbClassifica.Name = "lbClassifica"
    Me.lbClassifica.NTSDbField = ""
    Me.lbClassifica.Size = New System.Drawing.Size(246, 44)
    Me.lbClassifica.TabIndex = 678
    Me.lbClassifica.Tooltip = ""
    Me.lbClassifica.UseMnemonic = False
    '
    'lbMagprod
    '
    Me.lbMagprod.AutoSize = True
    Me.lbMagprod.BackColor = System.Drawing.Color.Transparent
    Me.lbMagprod.Location = New System.Drawing.Point(12, 163)
    Me.lbMagprod.Name = "lbMagprod"
    Me.lbMagprod.NTSDbField = ""
    Me.lbMagprod.Size = New System.Drawing.Size(113, 13)
    Me.lbMagprod.TabIndex = 48
    Me.lbMagprod.Text = "Magazzino produzione"
    Me.lbMagprod.Tooltip = ""
    Me.lbMagprod.UseMnemonic = False
    '
    'edMagprodfin
    '
    Me.edMagprodfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagprodfin.EditValue = "9999"
    Me.edMagprodfin.Location = New System.Drawing.Point(249, 160)
    Me.edMagprodfin.Name = "edMagprodfin"
    Me.edMagprodfin.NTSDbField = ""
    Me.edMagprodfin.NTSFormat = "0"
    Me.edMagprodfin.NTSForzaVisZoom = False
    Me.edMagprodfin.NTSOldValue = "9999"
    Me.edMagprodfin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edMagprodfin.Properties.Appearance.Options.UseBackColor = True
    Me.edMagprodfin.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagprodfin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagprodfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagprodfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagprodfin.Properties.AutoHeight = False
    Me.edMagprodfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagprodfin.Properties.MaxLength = 65536
    Me.edMagprodfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagprodfin.Size = New System.Drawing.Size(60, 20)
    Me.edMagprodfin.TabIndex = 47
    '
    'edMagprodini
    '
    Me.edMagprodini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagprodini.EditValue = "0"
    Me.edMagprodini.Location = New System.Drawing.Point(128, 160)
    Me.edMagprodini.Name = "edMagprodini"
    Me.edMagprodini.NTSDbField = ""
    Me.edMagprodini.NTSFormat = "0"
    Me.edMagprodini.NTSForzaVisZoom = False
    Me.edMagprodini.NTSOldValue = ""
    Me.edMagprodini.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edMagprodini.Properties.Appearance.Options.UseBackColor = True
    Me.edMagprodini.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagprodini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagprodini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagprodini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagprodini.Properties.AutoHeight = False
    Me.edMagprodini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagprodini.Properties.MaxLength = 65536
    Me.edMagprodini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagprodini.Size = New System.Drawing.Size(59, 20)
    Me.edMagprodini.TabIndex = 46
    '
    'lbMagstock
    '
    Me.lbMagstock.AutoSize = True
    Me.lbMagstock.BackColor = System.Drawing.Color.Transparent
    Me.lbMagstock.Location = New System.Drawing.Point(12, 139)
    Me.lbMagstock.Name = "lbMagstock"
    Me.lbMagstock.NTSDbField = ""
    Me.lbMagstock.Size = New System.Drawing.Size(111, 13)
    Me.lbMagstock.TabIndex = 45
    Me.lbMagstock.Text = "Magazzino stoccaggio"
    Me.lbMagstock.Tooltip = ""
    Me.lbMagstock.UseMnemonic = False
    '
    'edMagstockfin
    '
    Me.edMagstockfin.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagstockfin.EditValue = "9999"
    Me.edMagstockfin.Location = New System.Drawing.Point(249, 136)
    Me.edMagstockfin.Name = "edMagstockfin"
    Me.edMagstockfin.NTSDbField = ""
    Me.edMagstockfin.NTSFormat = "0"
    Me.edMagstockfin.NTSForzaVisZoom = False
    Me.edMagstockfin.NTSOldValue = "9999"
    Me.edMagstockfin.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edMagstockfin.Properties.Appearance.Options.UseBackColor = True
    Me.edMagstockfin.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagstockfin.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagstockfin.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagstockfin.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagstockfin.Properties.AutoHeight = False
    Me.edMagstockfin.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagstockfin.Properties.MaxLength = 65536
    Me.edMagstockfin.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagstockfin.Size = New System.Drawing.Size(60, 20)
    Me.edMagstockfin.TabIndex = 44
    '
    'edMagstockini
    '
    Me.edMagstockini.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMagstockini.EditValue = "0"
    Me.edMagstockini.Location = New System.Drawing.Point(128, 136)
    Me.edMagstockini.Name = "edMagstockini"
    Me.edMagstockini.NTSDbField = ""
    Me.edMagstockini.NTSFormat = "0"
    Me.edMagstockini.NTSForzaVisZoom = False
    Me.edMagstockini.NTSOldValue = ""
    Me.edMagstockini.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edMagstockini.Properties.Appearance.Options.UseBackColor = True
    Me.edMagstockini.Properties.Appearance.Options.UseTextOptions = True
    Me.edMagstockini.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMagstockini.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMagstockini.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMagstockini.Properties.AutoHeight = False
    Me.edMagstockini.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMagstockini.Properties.MaxLength = 65536
    Me.edMagstockini.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMagstockini.Size = New System.Drawing.Size(59, 20)
    Me.edMagstockini.TabIndex = 43
    '
    'lbCodIvada
    '
    Me.lbCodIvada.AutoSize = True
    Me.lbCodIvada.BackColor = System.Drawing.Color.Transparent
    Me.lbCodIvada.Location = New System.Drawing.Point(12, 115)
    Me.lbCodIvada.Name = "lbCodIvada"
    Me.lbCodIvada.NTSDbField = ""
    Me.lbCodIvada.Size = New System.Drawing.Size(59, 13)
    Me.lbCodIvada.TabIndex = 39
    Me.lbCodIvada.Text = "Codice IVA"
    Me.lbCodIvada.Tooltip = ""
    Me.lbCodIvada.UseMnemonic = False
    '
    'edClassificazioneLivello5
    '
    Me.edClassificazioneLivello5.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edClassificazioneLivello5.Location = New System.Drawing.Point(28, 207)
    Me.edClassificazioneLivello5.Name = "edClassificazioneLivello5"
    Me.edClassificazioneLivello5.NTSDbField = ""
    Me.edClassificazioneLivello5.NTSForzaVisZoom = False
    Me.edClassificazioneLivello5.NTSOldValue = ""
    Me.edClassificazioneLivello5.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edClassificazioneLivello5.Properties.Appearance.Options.UseBackColor = True
    Me.edClassificazioneLivello5.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClassificazioneLivello5.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClassificazioneLivello5.Properties.AutoHeight = False
    Me.edClassificazioneLivello5.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClassificazioneLivello5.Properties.MaxLength = 65536
    Me.edClassificazioneLivello5.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClassificazioneLivello5.Size = New System.Drawing.Size(5, 20)
    Me.edClassificazioneLivello5.TabIndex = 28
    Me.edClassificazioneLivello5.Visible = False
    '
    'edClassificazioneLivello4
    '
    Me.edClassificazioneLivello4.Cursor = System.Windows.Forms.Cursors.Default
    Me.edClassificazioneLivello4.Location = New System.Drawing.Point(22, 207)
    Me.edClassificazioneLivello4.Name = "edClassificazioneLivello4"
    Me.edClassificazioneLivello4.NTSDbField = ""
    Me.edClassificazioneLivello4.NTSForzaVisZoom = False
    Me.edClassificazioneLivello4.NTSOldValue = ""
    Me.edClassificazioneLivello4.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edClassificazioneLivello4.Properties.Appearance.Options.UseBackColor = True
    Me.edClassificazioneLivello4.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClassificazioneLivello4.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClassificazioneLivello4.Properties.AutoHeight = False
    Me.edClassificazioneLivello4.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClassificazioneLivello4.Properties.MaxLength = 65536
    Me.edClassificazioneLivello4.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClassificazioneLivello4.Size = New System.Drawing.Size(5, 20)
    Me.edClassificazioneLivello4.TabIndex = 28
    Me.edClassificazioneLivello4.Visible = False
    '
    'edClassificazioneLivello3
    '
    Me.edClassificazioneLivello3.Cursor = System.Windows.Forms.Cursors.Default
    Me.edClassificazioneLivello3.Location = New System.Drawing.Point(15, 207)
    Me.edClassificazioneLivello3.Name = "edClassificazioneLivello3"
    Me.edClassificazioneLivello3.NTSDbField = ""
    Me.edClassificazioneLivello3.NTSForzaVisZoom = False
    Me.edClassificazioneLivello3.NTSOldValue = ""
    Me.edClassificazioneLivello3.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edClassificazioneLivello3.Properties.Appearance.Options.UseBackColor = True
    Me.edClassificazioneLivello3.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClassificazioneLivello3.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClassificazioneLivello3.Properties.AutoHeight = False
    Me.edClassificazioneLivello3.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClassificazioneLivello3.Properties.MaxLength = 65536
    Me.edClassificazioneLivello3.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClassificazioneLivello3.Size = New System.Drawing.Size(5, 20)
    Me.edClassificazioneLivello3.TabIndex = 28
    Me.edClassificazioneLivello3.Visible = False
    '
    'edClassificazioneLivello2
    '
    Me.edClassificazioneLivello2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edClassificazioneLivello2.Location = New System.Drawing.Point(9, 207)
    Me.edClassificazioneLivello2.Name = "edClassificazioneLivello2"
    Me.edClassificazioneLivello2.NTSDbField = ""
    Me.edClassificazioneLivello2.NTSForzaVisZoom = False
    Me.edClassificazioneLivello2.NTSOldValue = ""
    Me.edClassificazioneLivello2.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edClassificazioneLivello2.Properties.Appearance.Options.UseBackColor = True
    Me.edClassificazioneLivello2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClassificazioneLivello2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClassificazioneLivello2.Properties.AutoHeight = False
    Me.edClassificazioneLivello2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClassificazioneLivello2.Properties.MaxLength = 65536
    Me.edClassificazioneLivello2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClassificazioneLivello2.Size = New System.Drawing.Size(5, 20)
    Me.edClassificazioneLivello2.TabIndex = 28
    Me.edClassificazioneLivello2.Visible = False
    '
    'edClassificazioneLivello1
    '
    Me.edClassificazioneLivello1.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edClassificazioneLivello1.Location = New System.Drawing.Point(3, 207)
    Me.edClassificazioneLivello1.Name = "edClassificazioneLivello1"
    Me.edClassificazioneLivello1.NTSDbField = ""
    Me.edClassificazioneLivello1.NTSForzaVisZoom = False
    Me.edClassificazioneLivello1.NTSOldValue = ""
    Me.edClassificazioneLivello1.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edClassificazioneLivello1.Properties.Appearance.Options.UseBackColor = True
    Me.edClassificazioneLivello1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edClassificazioneLivello1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edClassificazioneLivello1.Properties.AutoHeight = False
    Me.edClassificazioneLivello1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edClassificazioneLivello1.Properties.MaxLength = 65536
    Me.edClassificazioneLivello1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edClassificazioneLivello1.Size = New System.Drawing.Size(5, 20)
    Me.edClassificazioneLivello1.TabIndex = 28
    Me.edClassificazioneLivello1.Visible = False
    '
    'lbApprovda
    '
    Me.lbApprovda.AutoSize = True
    Me.lbApprovda.BackColor = System.Drawing.Color.Transparent
    Me.lbApprovda.Location = New System.Drawing.Point(12, 91)
    Me.lbApprovda.Name = "lbApprovda"
    Me.lbApprovda.NTSDbField = ""
    Me.lbApprovda.Size = New System.Drawing.Size(96, 13)
    Me.lbApprovda.TabIndex = 38
    Me.lbApprovda.Text = "Approvvigionatore"
    Me.lbApprovda.Tooltip = ""
    Me.lbApprovda.UseMnemonic = False
    '
    'edCodIvaa
    '
    Me.edCodIvaa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodIvaa.EditValue = "9999"
    Me.edCodIvaa.Location = New System.Drawing.Point(249, 112)
    Me.edCodIvaa.Name = "edCodIvaa"
    Me.edCodIvaa.NTSDbField = ""
    Me.edCodIvaa.NTSFormat = "0"
    Me.edCodIvaa.NTSForzaVisZoom = False
    Me.edCodIvaa.NTSOldValue = "9999"
    Me.edCodIvaa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodIvaa.Properties.Appearance.Options.UseBackColor = True
    Me.edCodIvaa.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodIvaa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodIvaa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodIvaa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodIvaa.Properties.AutoHeight = False
    Me.edCodIvaa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodIvaa.Properties.MaxLength = 65536
    Me.edCodIvaa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodIvaa.Size = New System.Drawing.Size(60, 20)
    Me.edCodIvaa.TabIndex = 37
    '
    'edApprova
    '
    Me.edApprova.Cursor = System.Windows.Forms.Cursors.Default
    Me.edApprova.EditValue = "999"
    Me.edApprova.Location = New System.Drawing.Point(249, 88)
    Me.edApprova.Name = "edApprova"
    Me.edApprova.NTSDbField = ""
    Me.edApprova.NTSFormat = "0"
    Me.edApprova.NTSForzaVisZoom = False
    Me.edApprova.NTSOldValue = "999"
    Me.edApprova.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edApprova.Properties.Appearance.Options.UseBackColor = True
    Me.edApprova.Properties.Appearance.Options.UseTextOptions = True
    Me.edApprova.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edApprova.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edApprova.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edApprova.Properties.AutoHeight = False
    Me.edApprova.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edApprova.Properties.MaxLength = 65536
    Me.edApprova.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edApprova.Size = New System.Drawing.Size(60, 20)
    Me.edApprova.TabIndex = 36
    '
    'edCodIvad
    '
    Me.edCodIvad.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodIvad.EditValue = "0"
    Me.edCodIvad.Location = New System.Drawing.Point(128, 112)
    Me.edCodIvad.Name = "edCodIvad"
    Me.edCodIvad.NTSDbField = ""
    Me.edCodIvad.NTSFormat = "0"
    Me.edCodIvad.NTSForzaVisZoom = False
    Me.edCodIvad.NTSOldValue = ""
    Me.edCodIvad.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodIvad.Properties.Appearance.Options.UseBackColor = True
    Me.edCodIvad.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodIvad.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodIvad.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodIvad.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodIvad.Properties.AutoHeight = False
    Me.edCodIvad.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodIvad.Properties.MaxLength = 65536
    Me.edCodIvad.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodIvad.Size = New System.Drawing.Size(59, 20)
    Me.edCodIvad.TabIndex = 35
    '
    'edApprovd
    '
    Me.edApprovd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edApprovd.EditValue = "0"
    Me.edApprovd.Location = New System.Drawing.Point(128, 88)
    Me.edApprovd.Name = "edApprovd"
    Me.edApprovd.NTSDbField = ""
    Me.edApprovd.NTSFormat = "0"
    Me.edApprovd.NTSForzaVisZoom = False
    Me.edApprovd.NTSOldValue = ""
    Me.edApprovd.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edApprovd.Properties.Appearance.Options.UseBackColor = True
    Me.edApprovd.Properties.Appearance.Options.UseTextOptions = True
    Me.edApprovd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edApprovd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edApprovd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edApprovd.Properties.AutoHeight = False
    Me.edApprovd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edApprovd.Properties.MaxLength = 65536
    Me.edApprovd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edApprovd.Size = New System.Drawing.Size(59, 20)
    Me.edApprovd.TabIndex = 34
    '
    'lbScontida
    '
    Me.lbScontida.AutoSize = True
    Me.lbScontida.BackColor = System.Drawing.Color.Transparent
    Me.lbScontida.Location = New System.Drawing.Point(12, 67)
    Me.lbScontida.Name = "lbScontida"
    Me.lbScontida.NTSDbField = ""
    Me.lbScontida.Size = New System.Drawing.Size(69, 13)
    Me.lbScontida.TabIndex = 33
    Me.lbScontida.Text = "Classe sconti"
    Me.lbScontida.Tooltip = ""
    Me.lbScontida.UseMnemonic = False
    '
    'lbProvvda
    '
    Me.lbProvvda.AutoSize = True
    Me.lbProvvda.BackColor = System.Drawing.Color.Transparent
    Me.lbProvvda.Location = New System.Drawing.Point(12, 43)
    Me.lbProvvda.Name = "lbProvvda"
    Me.lbProvvda.NTSDbField = ""
    Me.lbProvvda.Size = New System.Drawing.Size(93, 13)
    Me.lbProvvda.TabIndex = 32
    Me.lbProvvda.Text = "Classe provvigioni"
    Me.lbProvvda.Tooltip = ""
    Me.lbProvvda.UseMnemonic = False
    '
    'edScontia
    '
    Me.edScontia.Cursor = System.Windows.Forms.Cursors.Default
    Me.edScontia.EditValue = "9999"
    Me.edScontia.Location = New System.Drawing.Point(249, 64)
    Me.edScontia.Name = "edScontia"
    Me.edScontia.NTSDbField = ""
    Me.edScontia.NTSFormat = "0"
    Me.edScontia.NTSForzaVisZoom = False
    Me.edScontia.NTSOldValue = "9999"
    Me.edScontia.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edScontia.Properties.Appearance.Options.UseBackColor = True
    Me.edScontia.Properties.Appearance.Options.UseTextOptions = True
    Me.edScontia.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edScontia.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edScontia.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edScontia.Properties.AutoHeight = False
    Me.edScontia.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edScontia.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edScontia.Size = New System.Drawing.Size(60, 20)
    Me.edScontia.TabIndex = 31
    '
    'edProvva
    '
    Me.edProvva.Cursor = System.Windows.Forms.Cursors.Default
    Me.edProvva.EditValue = "999"
    Me.edProvva.Location = New System.Drawing.Point(249, 40)
    Me.edProvva.Name = "edProvva"
    Me.edProvva.NTSDbField = ""
    Me.edProvva.NTSFormat = "0"
    Me.edProvva.NTSForzaVisZoom = False
    Me.edProvva.NTSOldValue = "999"
    Me.edProvva.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edProvva.Properties.Appearance.Options.UseBackColor = True
    Me.edProvva.Properties.Appearance.Options.UseTextOptions = True
    Me.edProvva.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edProvva.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edProvva.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edProvva.Properties.AutoHeight = False
    Me.edProvva.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edProvva.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edProvva.Size = New System.Drawing.Size(60, 20)
    Me.edProvva.TabIndex = 30
    '
    'edScontid
    '
    Me.edScontid.Cursor = System.Windows.Forms.Cursors.Default
    Me.edScontid.EditValue = "0"
    Me.edScontid.Location = New System.Drawing.Point(129, 64)
    Me.edScontid.Name = "edScontid"
    Me.edScontid.NTSDbField = ""
    Me.edScontid.NTSFormat = "0"
    Me.edScontid.NTSForzaVisZoom = False
    Me.edScontid.NTSOldValue = ""
    Me.edScontid.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edScontid.Properties.Appearance.Options.UseBackColor = True
    Me.edScontid.Properties.Appearance.Options.UseTextOptions = True
    Me.edScontid.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edScontid.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edScontid.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edScontid.Properties.AutoHeight = False
    Me.edScontid.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edScontid.Properties.MaxLength = 65536
    Me.edScontid.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edScontid.Size = New System.Drawing.Size(59, 20)
    Me.edScontid.TabIndex = 29
    '
    'edProvvd
    '
    Me.edProvvd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edProvvd.EditValue = "0"
    Me.edProvvd.Location = New System.Drawing.Point(129, 40)
    Me.edProvvd.Name = "edProvvd"
    Me.edProvvd.NTSDbField = ""
    Me.edProvvd.NTSFormat = "0"
    Me.edProvvd.NTSForzaVisZoom = False
    Me.edProvvd.NTSOldValue = ""
    Me.edProvvd.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edProvvd.Properties.Appearance.Options.UseBackColor = True
    Me.edProvvd.Properties.Appearance.Options.UseTextOptions = True
    Me.edProvvd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edProvvd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edProvvd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edProvvd.Properties.AutoHeight = False
    Me.edProvvd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edProvvd.Properties.MaxLength = 65536
    Me.edProvvd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edProvvd.Size = New System.Drawing.Size(59, 20)
    Me.edProvvd.TabIndex = 28
    '
    'lbMarcada
    '
    Me.lbMarcada.AutoSize = True
    Me.lbMarcada.BackColor = System.Drawing.Color.Transparent
    Me.lbMarcada.Location = New System.Drawing.Point(12, 19)
    Me.lbMarcada.Name = "lbMarcada"
    Me.lbMarcada.NTSDbField = ""
    Me.lbMarcada.Size = New System.Drawing.Size(36, 13)
    Me.lbMarcada.TabIndex = 27
    Me.lbMarcada.Text = "Marca"
    Me.lbMarcada.Tooltip = ""
    Me.lbMarcada.UseMnemonic = False
    '
    'edMarcaa
    '
    Me.edMarcaa.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edMarcaa.EditValue = "999"
    Me.edMarcaa.Location = New System.Drawing.Point(249, 16)
    Me.edMarcaa.Name = "edMarcaa"
    Me.edMarcaa.NTSDbField = ""
    Me.edMarcaa.NTSFormat = "0"
    Me.edMarcaa.NTSForzaVisZoom = False
    Me.edMarcaa.NTSOldValue = "999"
    Me.edMarcaa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edMarcaa.Properties.Appearance.Options.UseBackColor = True
    Me.edMarcaa.Properties.Appearance.Options.UseTextOptions = True
    Me.edMarcaa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMarcaa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMarcaa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMarcaa.Properties.AutoHeight = False
    Me.edMarcaa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMarcaa.Properties.MaxLength = 65536
    Me.edMarcaa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMarcaa.Size = New System.Drawing.Size(60, 20)
    Me.edMarcaa.TabIndex = 26
    '
    'edMarcad
    '
    Me.edMarcad.Cursor = System.Windows.Forms.Cursors.Default
    Me.edMarcad.EditValue = "0"
    Me.edMarcad.Location = New System.Drawing.Point(129, 16)
    Me.edMarcad.Name = "edMarcad"
    Me.edMarcad.NTSDbField = ""
    Me.edMarcad.NTSFormat = "0"
    Me.edMarcad.NTSForzaVisZoom = False
    Me.edMarcad.NTSOldValue = ""
    Me.edMarcad.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edMarcad.Properties.Appearance.Options.UseBackColor = True
    Me.edMarcad.Properties.Appearance.Options.UseTextOptions = True
    Me.edMarcad.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edMarcad.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edMarcad.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edMarcad.Properties.AutoHeight = False
    Me.edMarcad.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edMarcad.Properties.MaxLength = 65536
    Me.edMarcad.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edMarcad.Size = New System.Drawing.Size(59, 20)
    Me.edMarcad.TabIndex = 25
    '
    'NtsLabel3
    '
    Me.NtsLabel3.AutoSize = True
    Me.NtsLabel3.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel3.Location = New System.Drawing.Point(266, 2)
    Me.NtsLabel3.Name = "NtsLabel3"
    Me.NtsLabel3.NTSDbField = ""
    Me.NtsLabel3.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel3.TabIndex = 21
    Me.NtsLabel3.Text = "A"
    Me.NtsLabel3.Tooltip = ""
    Me.NtsLabel3.UseMnemonic = False
    '
    'NtsLabel4
    '
    Me.NtsLabel4.AutoSize = True
    Me.NtsLabel4.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel4.Location = New System.Drawing.Point(141, 2)
    Me.NtsLabel4.Name = "NtsLabel4"
    Me.NtsLabel4.NTSDbField = ""
    Me.NtsLabel4.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel4.TabIndex = 20
    Me.NtsLabel4.Text = "Da"
    Me.NtsLabel4.Tooltip = ""
    Me.NtsLabel4.UseMnemonic = False
    '
    'pnTab2Pan1
    '
    Me.pnTab2Pan1.AllowDrop = True
    Me.pnTab2Pan1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab2Pan1.Appearance.Options.UseBackColor = True
    Me.pnTab2Pan1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab2Pan1.Controls.Add(Me.lbCodtipada)
    Me.pnTab2Pan1.Controls.Add(Me.edCodtipaa)
    Me.pnTab2Pan1.Controls.Add(Me.edCodtipad)
    Me.pnTab2Pan1.Controls.Add(Me.edForna)
    Me.pnTab2Pan1.Controls.Add(Me.edFornd)
    Me.pnTab2Pan1.Controls.Add(Me.lbFornda)
    Me.pnTab2Pan1.Controls.Add(Me.edDataUltaga)
    Me.pnTab2Pan1.Controls.Add(Me.edFamproda)
    Me.pnTab2Pan1.Controls.Add(Me.edDataUltagd)
    Me.pnTab2Pan1.Controls.Add(Me.NtsLabel26)
    Me.pnTab2Pan1.Controls.Add(Me.edFamprodd)
    Me.pnTab2Pan1.Controls.Add(Me.lbFamigliada)
    Me.pnTab2Pan1.Controls.Add(Me.edDescra)
    Me.pnTab2Pan1.Controls.Add(Me.lbSottogruppoda)
    Me.pnTab2Pan1.Controls.Add(Me.edDescrd)
    Me.pnTab2Pan1.Controls.Add(Me.edSotta)
    Me.pnTab2Pan1.Controls.Add(Me.NtsLabel2)
    Me.pnTab2Pan1.Controls.Add(Me.edSottd)
    Me.pnTab2Pan1.Controls.Add(Me.lbGruppoda)
    Me.pnTab2Pan1.Controls.Add(Me.edGruppoa)
    Me.pnTab2Pan1.Controls.Add(Me.edGruppod)
    Me.pnTab2Pan1.Controls.Add(Me.edCodalta)
    Me.pnTab2Pan1.Controls.Add(Me.edCodaltd)
    Me.pnTab2Pan1.Controls.Add(Me.lbCodaltda)
    Me.pnTab2Pan1.Controls.Add(Me.edCodarta)
    Me.pnTab2Pan1.Controls.Add(Me.edCodartd)
    Me.pnTab2Pan1.Controls.Add(Me.lbCodartda)
    Me.pnTab2Pan1.Controls.Add(Me.NtsLabel11)
    Me.pnTab2Pan1.Controls.Add(Me.NtsLabel10)
    Me.pnTab2Pan1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab2Pan1.Location = New System.Drawing.Point(0, 0)
    Me.pnTab2Pan1.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab2Pan1.Name = "pnTab2Pan1"
    Me.pnTab2Pan1.NTSActiveTrasparency = True
    Me.pnTab2Pan1.Size = New System.Drawing.Size(332, 239)
    Me.pnTab2Pan1.TabIndex = 3
    '
    'lbCodtipada
    '
    Me.lbCodtipada.AutoSize = True
    Me.lbCodtipada.BackColor = System.Drawing.Color.Transparent
    Me.lbCodtipada.Location = New System.Drawing.Point(9, 190)
    Me.lbCodtipada.Name = "lbCodtipada"
    Me.lbCodtipada.NTSDbField = ""
    Me.lbCodtipada.Size = New System.Drawing.Size(49, 13)
    Me.lbCodtipada.TabIndex = 35
    Me.lbCodtipada.Text = "Tipologia"
    Me.lbCodtipada.Tooltip = ""
    Me.lbCodtipada.UseMnemonic = False
    '
    'edCodtipaa
    '
    Me.edCodtipaa.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edCodtipaa.EditValue = "999"
    Me.edCodtipaa.Location = New System.Drawing.Point(222, 187)
    Me.edCodtipaa.Name = "edCodtipaa"
    Me.edCodtipaa.NTSDbField = ""
    Me.edCodtipaa.NTSFormat = "0"
    Me.edCodtipaa.NTSForzaVisZoom = False
    Me.edCodtipaa.NTSOldValue = "999"
    Me.edCodtipaa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodtipaa.Properties.Appearance.Options.UseBackColor = True
    Me.edCodtipaa.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodtipaa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodtipaa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodtipaa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodtipaa.Properties.AutoHeight = False
    Me.edCodtipaa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodtipaa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodtipaa.Size = New System.Drawing.Size(60, 20)
    Me.edCodtipaa.TabIndex = 34
    '
    'edCodtipad
    '
    Me.edCodtipad.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodtipad.EditValue = "0"
    Me.edCodtipad.Location = New System.Drawing.Point(102, 187)
    Me.edCodtipad.Name = "edCodtipad"
    Me.edCodtipad.NTSDbField = ""
    Me.edCodtipad.NTSFormat = "0"
    Me.edCodtipad.NTSForzaVisZoom = False
    Me.edCodtipad.NTSOldValue = ""
    Me.edCodtipad.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodtipad.Properties.Appearance.Options.UseBackColor = True
    Me.edCodtipad.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodtipad.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodtipad.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodtipad.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodtipad.Properties.AutoHeight = False
    Me.edCodtipad.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodtipad.Properties.MaxLength = 65536
    Me.edCodtipad.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodtipad.Size = New System.Drawing.Size(59, 20)
    Me.edCodtipad.TabIndex = 33
    '
    'edForna
    '
    Me.edForna.Cursor = System.Windows.Forms.Cursors.Default
    Me.edForna.EditValue = "999999999"
    Me.edForna.Location = New System.Drawing.Point(222, 163)
    Me.edForna.Name = "edForna"
    Me.edForna.NTSDbField = ""
    Me.edForna.NTSFormat = "0"
    Me.edForna.NTSForzaVisZoom = False
    Me.edForna.NTSOldValue = "999999999"
    Me.edForna.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edForna.Properties.Appearance.Options.UseBackColor = True
    Me.edForna.Properties.Appearance.Options.UseTextOptions = True
    Me.edForna.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edForna.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edForna.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edForna.Properties.AutoHeight = False
    Me.edForna.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edForna.Properties.MaxLength = 65536
    Me.edForna.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edForna.Size = New System.Drawing.Size(105, 20)
    Me.edForna.TabIndex = 32
    '
    'edFornd
    '
    Me.edFornd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFornd.EditValue = "0"
    Me.edFornd.Location = New System.Drawing.Point(102, 163)
    Me.edFornd.Name = "edFornd"
    Me.edFornd.NTSDbField = ""
    Me.edFornd.NTSFormat = "0"
    Me.edFornd.NTSForzaVisZoom = False
    Me.edFornd.NTSOldValue = ""
    Me.edFornd.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edFornd.Properties.Appearance.Options.UseBackColor = True
    Me.edFornd.Properties.Appearance.Options.UseTextOptions = True
    Me.edFornd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edFornd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFornd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFornd.Properties.AutoHeight = False
    Me.edFornd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFornd.Properties.MaxLength = 65536
    Me.edFornd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFornd.Size = New System.Drawing.Size(105, 20)
    Me.edFornd.TabIndex = 31
    '
    'lbFornda
    '
    Me.lbFornda.AutoSize = True
    Me.lbFornda.BackColor = System.Drawing.Color.Transparent
    Me.lbFornda.Location = New System.Drawing.Point(9, 166)
    Me.lbFornda.Name = "lbFornda"
    Me.lbFornda.NTSDbField = ""
    Me.lbFornda.Size = New System.Drawing.Size(51, 13)
    Me.lbFornda.TabIndex = 30
    Me.lbFornda.Text = "Fornitore"
    Me.lbFornda.Tooltip = ""
    Me.lbFornda.UseMnemonic = False
    '
    'edDataUltaga
    '
    Me.edDataUltaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataUltaga.EditValue = "31/12/2099"
    Me.edDataUltaga.Location = New System.Drawing.Point(223, 212)
    Me.edDataUltaga.Name = "edDataUltaga"
    Me.edDataUltaga.NTSDbField = ""
    Me.edDataUltaga.NTSForzaVisZoom = False
    Me.edDataUltaga.NTSOldValue = ""
    Me.edDataUltaga.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDataUltaga.Properties.Appearance.Options.UseBackColor = True
    Me.edDataUltaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataUltaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataUltaga.Properties.AutoHeight = False
    Me.edDataUltaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataUltaga.Properties.MaxLength = 65536
    Me.edDataUltaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataUltaga.Size = New System.Drawing.Size(83, 20)
    Me.edDataUltaga.TabIndex = 42
    '
    'edFamproda
    '
    Me.edFamproda.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFamproda.EditValue = "ZZZZ"
    Me.edFamproda.Location = New System.Drawing.Point(222, 139)
    Me.edFamproda.Name = "edFamproda"
    Me.edFamproda.NTSDbField = ""
    Me.edFamproda.NTSForzaVisZoom = False
    Me.edFamproda.NTSOldValue = "ZZZZ"
    Me.edFamproda.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edFamproda.Properties.Appearance.Options.UseBackColor = True
    Me.edFamproda.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFamproda.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFamproda.Properties.AutoHeight = False
    Me.edFamproda.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFamproda.Properties.MaxLength = 65536
    Me.edFamproda.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFamproda.Size = New System.Drawing.Size(61, 20)
    Me.edFamproda.TabIndex = 29
    '
    'edDataUltagd
    '
    Me.edDataUltagd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataUltagd.EditValue = "01/01/1900"
    Me.edDataUltagd.Location = New System.Drawing.Point(102, 212)
    Me.edDataUltagd.Name = "edDataUltagd"
    Me.edDataUltagd.NTSDbField = ""
    Me.edDataUltagd.NTSForzaVisZoom = False
    Me.edDataUltagd.NTSOldValue = ""
    Me.edDataUltagd.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDataUltagd.Properties.Appearance.Options.UseBackColor = True
    Me.edDataUltagd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataUltagd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataUltagd.Properties.AutoHeight = False
    Me.edDataUltagd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataUltagd.Properties.MaxLength = 65536
    Me.edDataUltagd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataUltagd.Size = New System.Drawing.Size(83, 20)
    Me.edDataUltagd.TabIndex = 41
    '
    'NtsLabel26
    '
    Me.NtsLabel26.AutoSize = True
    Me.NtsLabel26.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel26.Location = New System.Drawing.Point(9, 215)
    Me.NtsLabel26.Name = "NtsLabel26"
    Me.NtsLabel26.NTSDbField = ""
    Me.NtsLabel26.Size = New System.Drawing.Size(73, 13)
    Me.NtsLabel26.TabIndex = 40
    Me.NtsLabel26.Text = "Data aggiorn."
    Me.NtsLabel26.Tooltip = ""
    Me.NtsLabel26.UseMnemonic = False
    '
    'edFamprodd
    '
    Me.edFamprodd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFamprodd.Location = New System.Drawing.Point(102, 139)
    Me.edFamprodd.Name = "edFamprodd"
    Me.edFamprodd.NTSDbField = ""
    Me.edFamprodd.NTSForzaVisZoom = False
    Me.edFamprodd.NTSOldValue = ""
    Me.edFamprodd.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edFamprodd.Properties.Appearance.Options.UseBackColor = True
    Me.edFamprodd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFamprodd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFamprodd.Properties.AutoHeight = False
    Me.edFamprodd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFamprodd.Properties.MaxLength = 65536
    Me.edFamprodd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFamprodd.Size = New System.Drawing.Size(59, 20)
    Me.edFamprodd.TabIndex = 28
    '
    'lbFamigliada
    '
    Me.lbFamigliada.AutoSize = True
    Me.lbFamigliada.BackColor = System.Drawing.Color.Transparent
    Me.lbFamigliada.Location = New System.Drawing.Point(9, 142)
    Me.lbFamigliada.Name = "lbFamigliada"
    Me.lbFamigliada.NTSDbField = ""
    Me.lbFamigliada.Size = New System.Drawing.Size(45, 13)
    Me.lbFamigliada.TabIndex = 27
    Me.lbFamigliada.Text = "Famiglia"
    Me.lbFamigliada.Tooltip = ""
    Me.lbFamigliada.UseMnemonic = False
    '
    'edDescra
    '
    Me.edDescra.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDescra.EditValue = "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ"
    Me.edDescra.Location = New System.Drawing.Point(222, 43)
    Me.edDescra.Name = "edDescra"
    Me.edDescra.NTSDbField = ""
    Me.edDescra.NTSForzaVisZoom = False
    Me.edDescra.NTSOldValue = "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ"
    Me.edDescra.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDescra.Properties.Appearance.Options.UseBackColor = True
    Me.edDescra.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescra.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescra.Properties.AutoHeight = False
    Me.edDescra.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescra.Properties.MaxLength = 65536
    Me.edDescra.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescra.Size = New System.Drawing.Size(105, 20)
    Me.edDescra.TabIndex = 24
    '
    'lbSottogruppoda
    '
    Me.lbSottogruppoda.AutoSize = True
    Me.lbSottogruppoda.BackColor = System.Drawing.Color.Transparent
    Me.lbSottogruppoda.Location = New System.Drawing.Point(9, 118)
    Me.lbSottogruppoda.Name = "lbSottogruppoda"
    Me.lbSottogruppoda.NTSDbField = ""
    Me.lbSottogruppoda.Size = New System.Drawing.Size(67, 13)
    Me.lbSottogruppoda.TabIndex = 26
    Me.lbSottogruppoda.Text = "Sottogruppo"
    Me.lbSottogruppoda.Tooltip = ""
    Me.lbSottogruppoda.UseMnemonic = False
    '
    'edDescrd
    '
    Me.edDescrd.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edDescrd.Location = New System.Drawing.Point(102, 43)
    Me.edDescrd.Name = "edDescrd"
    Me.edDescrd.NTSDbField = ""
    Me.edDescrd.NTSForzaVisZoom = False
    Me.edDescrd.NTSOldValue = ""
    Me.edDescrd.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDescrd.Properties.Appearance.Options.UseBackColor = True
    Me.edDescrd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDescrd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDescrd.Properties.AutoHeight = False
    Me.edDescrd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDescrd.Properties.MaxLength = 65536
    Me.edDescrd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDescrd.Size = New System.Drawing.Size(105, 20)
    Me.edDescrd.TabIndex = 23
    '
    'edSotta
    '
    Me.edSotta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSotta.EditValue = "9999"
    Me.edSotta.Location = New System.Drawing.Point(223, 115)
    Me.edSotta.Name = "edSotta"
    Me.edSotta.NTSDbField = ""
    Me.edSotta.NTSFormat = "0"
    Me.edSotta.NTSForzaVisZoom = False
    Me.edSotta.NTSOldValue = "9999"
    Me.edSotta.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edSotta.Properties.Appearance.Options.UseBackColor = True
    Me.edSotta.Properties.Appearance.Options.UseTextOptions = True
    Me.edSotta.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSotta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSotta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSotta.Properties.AutoHeight = False
    Me.edSotta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSotta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSotta.Size = New System.Drawing.Size(60, 20)
    Me.edSotta.TabIndex = 25
    '
    'NtsLabel2
    '
    Me.NtsLabel2.AutoSize = True
    Me.NtsLabel2.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel2.Location = New System.Drawing.Point(9, 46)
    Me.NtsLabel2.Name = "NtsLabel2"
    Me.NtsLabel2.NTSDbField = ""
    Me.NtsLabel2.Size = New System.Drawing.Size(61, 13)
    Me.NtsLabel2.TabIndex = 22
    Me.NtsLabel2.Text = "Descrizione"
    Me.NtsLabel2.Tooltip = ""
    Me.NtsLabel2.UseMnemonic = False
    '
    'edSottd
    '
    Me.edSottd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSottd.EditValue = "0"
    Me.edSottd.Location = New System.Drawing.Point(102, 115)
    Me.edSottd.Name = "edSottd"
    Me.edSottd.NTSDbField = ""
    Me.edSottd.NTSFormat = "0"
    Me.edSottd.NTSForzaVisZoom = False
    Me.edSottd.NTSOldValue = ""
    Me.edSottd.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edSottd.Properties.Appearance.Options.UseBackColor = True
    Me.edSottd.Properties.Appearance.Options.UseTextOptions = True
    Me.edSottd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edSottd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSottd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSottd.Properties.AutoHeight = False
    Me.edSottd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSottd.Properties.MaxLength = 65536
    Me.edSottd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSottd.Size = New System.Drawing.Size(59, 20)
    Me.edSottd.TabIndex = 24
    '
    'lbGruppoda
    '
    Me.lbGruppoda.AutoSize = True
    Me.lbGruppoda.BackColor = System.Drawing.Color.Transparent
    Me.lbGruppoda.Location = New System.Drawing.Point(9, 94)
    Me.lbGruppoda.Name = "lbGruppoda"
    Me.lbGruppoda.NTSDbField = ""
    Me.lbGruppoda.Size = New System.Drawing.Size(42, 13)
    Me.lbGruppoda.TabIndex = 23
    Me.lbGruppoda.Text = "Gruppo"
    Me.lbGruppoda.Tooltip = ""
    Me.lbGruppoda.UseMnemonic = False
    '
    'edGruppoa
    '
    Me.edGruppoa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edGruppoa.EditValue = "99"
    Me.edGruppoa.Location = New System.Drawing.Point(223, 91)
    Me.edGruppoa.Name = "edGruppoa"
    Me.edGruppoa.NTSDbField = ""
    Me.edGruppoa.NTSFormat = "0"
    Me.edGruppoa.NTSForzaVisZoom = False
    Me.edGruppoa.NTSOldValue = "99"
    Me.edGruppoa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edGruppoa.Properties.Appearance.Options.UseBackColor = True
    Me.edGruppoa.Properties.Appearance.Options.UseTextOptions = True
    Me.edGruppoa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edGruppoa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edGruppoa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edGruppoa.Properties.AutoHeight = False
    Me.edGruppoa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edGruppoa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edGruppoa.Size = New System.Drawing.Size(60, 20)
    Me.edGruppoa.TabIndex = 22
    '
    'edGruppod
    '
    Me.edGruppod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edGruppod.EditValue = "0"
    Me.edGruppod.Location = New System.Drawing.Point(102, 91)
    Me.edGruppod.Name = "edGruppod"
    Me.edGruppod.NTSDbField = ""
    Me.edGruppod.NTSFormat = "0"
    Me.edGruppod.NTSForzaVisZoom = False
    Me.edGruppod.NTSOldValue = ""
    Me.edGruppod.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edGruppod.Properties.Appearance.Options.UseBackColor = True
    Me.edGruppod.Properties.Appearance.Options.UseTextOptions = True
    Me.edGruppod.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edGruppod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edGruppod.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edGruppod.Properties.AutoHeight = False
    Me.edGruppod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edGruppod.Properties.MaxLength = 65536
    Me.edGruppod.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edGruppod.Size = New System.Drawing.Size(59, 20)
    Me.edGruppod.TabIndex = 21
    '
    'edCodalta
    '
    Me.edCodalta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodalta.EditValue = "ZZZZZZZZZZZZZZZZZZ"
    Me.edCodalta.Location = New System.Drawing.Point(222, 67)
    Me.edCodalta.Name = "edCodalta"
    Me.edCodalta.NTSDbField = ""
    Me.edCodalta.NTSForzaVisZoom = False
    Me.edCodalta.NTSOldValue = "ZZZZZZZZZZZZZZZZZZ"
    Me.edCodalta.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodalta.Properties.Appearance.Options.UseBackColor = True
    Me.edCodalta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodalta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodalta.Properties.AutoHeight = False
    Me.edCodalta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodalta.Properties.MaxLength = 65536
    Me.edCodalta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodalta.Size = New System.Drawing.Size(105, 20)
    Me.edCodalta.TabIndex = 19
    '
    'edCodaltd
    '
    Me.edCodaltd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodaltd.Location = New System.Drawing.Point(102, 67)
    Me.edCodaltd.Name = "edCodaltd"
    Me.edCodaltd.NTSDbField = ""
    Me.edCodaltd.NTSForzaVisZoom = False
    Me.edCodaltd.NTSOldValue = ""
    Me.edCodaltd.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodaltd.Properties.Appearance.Options.UseBackColor = True
    Me.edCodaltd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodaltd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodaltd.Properties.AutoHeight = False
    Me.edCodaltd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodaltd.Properties.MaxLength = 65536
    Me.edCodaltd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodaltd.Size = New System.Drawing.Size(105, 20)
    Me.edCodaltd.TabIndex = 18
    '
    'lbCodaltda
    '
    Me.lbCodaltda.AutoSize = True
    Me.lbCodaltda.BackColor = System.Drawing.Color.Transparent
    Me.lbCodaltda.Location = New System.Drawing.Point(9, 70)
    Me.lbCodaltda.Name = "lbCodaltda"
    Me.lbCodaltda.NTSDbField = ""
    Me.lbCodaltda.Size = New System.Drawing.Size(85, 13)
    Me.lbCodaltda.TabIndex = 17
    Me.lbCodaltda.Text = "Cod. alternativo"
    Me.lbCodaltda.Tooltip = ""
    Me.lbCodaltda.UseMnemonic = False
    '
    'edCodarta
    '
    Me.edCodarta.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodarta.EditValue = "ZZZZZZZZZZZZZZZZZZ"
    Me.edCodarta.Location = New System.Drawing.Point(222, 19)
    Me.edCodarta.Name = "edCodarta"
    Me.edCodarta.NTSDbField = ""
    Me.edCodarta.NTSForzaVisZoom = False
    Me.edCodarta.NTSOldValue = "ZZZZZZZZZZZZZZZZZZ"
    Me.edCodarta.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodarta.Properties.Appearance.Options.UseBackColor = True
    Me.edCodarta.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodarta.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodarta.Properties.AutoHeight = False
    Me.edCodarta.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodarta.Properties.MaxLength = 65536
    Me.edCodarta.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodarta.Size = New System.Drawing.Size(105, 20)
    Me.edCodarta.TabIndex = 16
    '
    'edCodartd
    '
    Me.edCodartd.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodartd.Location = New System.Drawing.Point(102, 19)
    Me.edCodartd.Name = "edCodartd"
    Me.edCodartd.NTSDbField = ""
    Me.edCodartd.NTSForzaVisZoom = False
    Me.edCodartd.NTSOldValue = ""
    Me.edCodartd.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodartd.Properties.Appearance.Options.UseBackColor = True
    Me.edCodartd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodartd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodartd.Properties.AutoHeight = False
    Me.edCodartd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodartd.Properties.MaxLength = 65536
    Me.edCodartd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodartd.Size = New System.Drawing.Size(105, 20)
    Me.edCodartd.TabIndex = 15
    '
    'lbCodartda
    '
    Me.lbCodartda.AutoSize = True
    Me.lbCodartda.BackColor = System.Drawing.Color.Transparent
    Me.lbCodartda.Location = New System.Drawing.Point(9, 22)
    Me.lbCodartda.Name = "lbCodartda"
    Me.lbCodartda.NTSDbField = ""
    Me.lbCodartda.Size = New System.Drawing.Size(68, 13)
    Me.lbCodartda.TabIndex = 14
    Me.lbCodartda.Text = "Cod. articolo"
    Me.lbCodartda.Tooltip = ""
    Me.lbCodartda.UseMnemonic = False
    '
    'NtsLabel11
    '
    Me.NtsLabel11.AutoSize = True
    Me.NtsLabel11.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel11.Location = New System.Drawing.Point(262, 5)
    Me.NtsLabel11.Name = "NtsLabel11"
    Me.NtsLabel11.NTSDbField = ""
    Me.NtsLabel11.Size = New System.Drawing.Size(14, 13)
    Me.NtsLabel11.TabIndex = 3
    Me.NtsLabel11.Text = "A"
    Me.NtsLabel11.Tooltip = ""
    Me.NtsLabel11.UseMnemonic = False
    '
    'NtsLabel10
    '
    Me.NtsLabel10.AutoSize = True
    Me.NtsLabel10.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel10.Location = New System.Drawing.Point(137, 5)
    Me.NtsLabel10.Name = "NtsLabel10"
    Me.NtsLabel10.NTSDbField = ""
    Me.NtsLabel10.Size = New System.Drawing.Size(20, 13)
    Me.NtsLabel10.TabIndex = 2
    Me.NtsLabel10.Text = "Da"
    Me.NtsLabel10.Tooltip = ""
    Me.NtsLabel10.UseMnemonic = False
    '
    'TabPage3
    '
    Me.TabPage3.AllowDrop = True
    Me.TabPage3.Appearance.Header.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
    Me.TabPage3.Appearance.Header.Options.UseFont = True
    Me.TabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.TabPage3.Controls.Add(Me.pnTab3Pan1)
    Me.TabPage3.Controls.Add(Me.pnTab3Pan2)
    Me.TabPage3.Enable = True
    Me.TabPage3.Name = "TabPage3"
    Me.TabPage3.Size = New System.Drawing.Size(669, 236)
    Me.TabPage3.Text = "Filtri particolari"
    '
    'pnTab3Pan1
    '
    Me.pnTab3Pan1.AllowDrop = True
    Me.pnTab3Pan1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab3Pan1.Appearance.Options.UseBackColor = True
    Me.pnTab3Pan1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab3Pan1.Controls.Add(Me.NtsLabel5)
    Me.pnTab3Pan1.Controls.Add(Me.NtsLabel1)
    Me.pnTab3Pan1.Controls.Add(Me.edListsar)
    Me.pnTab3Pan1.Controls.Add(Me.edArtprom)
    Me.pnTab3Pan1.Controls.Add(Me.lbDBLike)
    Me.pnTab3Pan1.Controls.Add(Me.edDBLike)
    Me.pnTab3Pan1.Controls.Add(Me.edTipo)
    Me.pnTab3Pan1.Controls.Add(Me.lbTipo)
    Me.pnTab3Pan1.Controls.Add(Me.cbAfasi)
    Me.pnTab3Pan1.Controls.Add(Me.cbCritico)
    Me.pnTab3Pan1.Controls.Add(Me.cbAlistino)
    Me.pnTab3Pan1.Controls.Add(Me.cbInesaur)
    Me.pnTab3Pan1.Controls.Add(Me.lbAfasi)
    Me.pnTab3Pan1.Controls.Add(Me.lbCritico)
    Me.pnTab3Pan1.Controls.Add(Me.lbAlistino)
    Me.pnTab3Pan1.Controls.Add(Me.lbInesaur)
    Me.pnTab3Pan1.Controls.Add(Me.cbAvarianti)
    Me.pnTab3Pan1.Controls.Add(Me.cbUbicaz)
    Me.pnTab3Pan1.Controls.Add(Me.cbMatricole)
    Me.pnTab3Pan1.Controls.Add(Me.cbCommessa)
    Me.pnTab3Pan1.Controls.Add(Me.cbLotti)
    Me.pnTab3Pan1.Controls.Add(Me.lbAvarianti)
    Me.pnTab3Pan1.Controls.Add(Me.lbUbicaz)
    Me.pnTab3Pan1.Controls.Add(Me.lbMatricole)
    Me.pnTab3Pan1.Controls.Add(Me.lbCommessa)
    Me.pnTab3Pan1.Controls.Add(Me.lbLotti)
    Me.pnTab3Pan1.Controls.Add(Me.lbCodtaglda)
    Me.pnTab3Pan1.Controls.Add(Me.edCodtagla)
    Me.pnTab3Pan1.Controls.Add(Me.edCodtagld)
    Me.pnTab3Pan1.Controls.Add(Me.lbCodstagda)
    Me.pnTab3Pan1.Controls.Add(Me.edCodstaga)
    Me.pnTab3Pan1.Controls.Add(Me.edCodstagd)
    Me.pnTab3Pan1.Controls.Add(Me.lbAnnoda)
    Me.pnTab3Pan1.Controls.Add(Me.edAnnoa)
    Me.pnTab3Pan1.Controls.Add(Me.edAnnod)
    Me.pnTab3Pan1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab3Pan1.Location = New System.Drawing.Point(3, 0)
    Me.pnTab3Pan1.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab3Pan1.Name = "pnTab3Pan1"
    Me.pnTab3Pan1.NTSActiveTrasparency = True
    Me.pnTab3Pan1.Size = New System.Drawing.Size(332, 235)
    Me.pnTab3Pan1.TabIndex = 6
    '
    'NtsLabel5
    '
    Me.NtsLabel5.AutoSize = True
    Me.NtsLabel5.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel5.Location = New System.Drawing.Point(186, 209)
    Me.NtsLabel5.Name = "NtsLabel5"
    Me.NtsLabel5.NTSDbField = ""
    Me.NtsLabel5.Size = New System.Drawing.Size(60, 13)
    Me.NtsLabel5.TabIndex = 83
    Me.NtsLabel5.Text = "Lista selez."
    Me.NtsLabel5.Tooltip = ""
    Me.NtsLabel5.UseMnemonic = False
    '
    'NtsLabel1
    '
    Me.NtsLabel1.AutoSize = True
    Me.NtsLabel1.BackColor = System.Drawing.Color.Transparent
    Me.NtsLabel1.Location = New System.Drawing.Point(14, 209)
    Me.NtsLabel1.Name = "NtsLabel1"
    Me.NtsLabel1.NTSDbField = ""
    Me.NtsLabel1.Size = New System.Drawing.Size(83, 13)
    Me.NtsLabel1.TabIndex = 82
    Me.NtsLabel1.Text = "Lista promozioni"
    Me.NtsLabel1.Tooltip = ""
    Me.NtsLabel1.UseMnemonic = False
    '
    'edListsar
    '
    Me.edListsar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListsar.EditValue = "0"
    Me.edListsar.Location = New System.Drawing.Point(257, 206)
    Me.edListsar.Name = "edListsar"
    Me.edListsar.NTSDbField = ""
    Me.edListsar.NTSFormat = "0"
    Me.edListsar.NTSForzaVisZoom = False
    Me.edListsar.NTSOldValue = "0"
    Me.edListsar.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edListsar.Properties.Appearance.Options.UseBackColor = True
    Me.edListsar.Properties.Appearance.Options.UseTextOptions = True
    Me.edListsar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListsar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListsar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListsar.Properties.AutoHeight = False
    Me.edListsar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListsar.Properties.MaxLength = 65536
    Me.edListsar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListsar.Size = New System.Drawing.Size(60, 20)
    Me.edListsar.TabIndex = 81
    '
    'edArtprom
    '
    Me.edArtprom.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edArtprom.EditValue = "0"
    Me.edArtprom.Location = New System.Drawing.Point(113, 206)
    Me.edArtprom.Name = "edArtprom"
    Me.edArtprom.NTSDbField = ""
    Me.edArtprom.NTSFormat = "0"
    Me.edArtprom.NTSForzaVisZoom = False
    Me.edArtprom.NTSOldValue = "0"
    Me.edArtprom.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edArtprom.Properties.Appearance.Options.UseBackColor = True
    Me.edArtprom.Properties.Appearance.Options.UseTextOptions = True
    Me.edArtprom.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edArtprom.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edArtprom.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edArtprom.Properties.AutoHeight = False
    Me.edArtprom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edArtprom.Properties.MaxLength = 65536
    Me.edArtprom.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edArtprom.Size = New System.Drawing.Size(60, 20)
    Me.edArtprom.TabIndex = 80
    '
    'lbDBLike
    '
    Me.lbDBLike.AutoSize = True
    Me.lbDBLike.BackColor = System.Drawing.Color.Transparent
    Me.lbDBLike.Location = New System.Drawing.Point(14, 75)
    Me.lbDBLike.Name = "lbDBLike"
    Me.lbDBLike.NTSDbField = ""
    Me.lbDBLike.Size = New System.Drawing.Size(76, 13)
    Me.lbDBLike.TabIndex = 79
    Me.lbDBLike.Text = "DB articolo like"
    Me.lbDBLike.Tooltip = ""
    Me.lbDBLike.UseMnemonic = False
    '
    'edDBLike
    '
    Me.edDBLike.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDBLike.Location = New System.Drawing.Point(114, 72)
    Me.edDBLike.Name = "edDBLike"
    Me.edDBLike.NTSDbField = ""
    Me.edDBLike.NTSForzaVisZoom = False
    Me.edDBLike.NTSOldValue = ""
    Me.edDBLike.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDBLike.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDBLike.Properties.AutoHeight = False
    Me.edDBLike.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDBLike.Properties.MaxLength = 65536
    Me.edDBLike.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDBLike.Size = New System.Drawing.Size(203, 20)
    Me.edDBLike.TabIndex = 78
    '
    'edTipo
    '
    Me.edTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTipo.Location = New System.Drawing.Point(257, 184)
    Me.edTipo.Name = "edTipo"
    Me.edTipo.NTSDbField = ""
    Me.edTipo.NTSForzaVisZoom = False
    Me.edTipo.NTSOldValue = ""
    Me.edTipo.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTipo.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTipo.Properties.AutoHeight = False
    Me.edTipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTipo.Properties.MaxLength = 65536
    Me.edTipo.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTipo.Size = New System.Drawing.Size(60, 20)
    Me.edTipo.TabIndex = 77
    '
    'lbTipo
    '
    Me.lbTipo.AutoSize = True
    Me.lbTipo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo.Location = New System.Drawing.Point(186, 187)
    Me.lbTipo.Name = "lbTipo"
    Me.lbTipo.NTSDbField = ""
    Me.lbTipo.Size = New System.Drawing.Size(65, 13)
    Me.lbTipo.TabIndex = 76
    Me.lbTipo.Text = "Tipo articolo"
    Me.lbTipo.Tooltip = ""
    Me.lbTipo.UseMnemonic = False
    '
    'cbAfasi
    '
    Me.cbAfasi.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAfasi.DataSource = Nothing
    Me.cbAfasi.DisplayMember = ""
    Me.cbAfasi.Location = New System.Drawing.Point(257, 161)
    Me.cbAfasi.Name = "cbAfasi"
    Me.cbAfasi.NTSDbField = ""
    Me.cbAfasi.Properties.AutoHeight = False
    Me.cbAfasi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAfasi.Properties.DropDownRows = 30
    Me.cbAfasi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAfasi.SelectedValue = ""
    Me.cbAfasi.Size = New System.Drawing.Size(60, 20)
    Me.cbAfasi.TabIndex = 75
    Me.cbAfasi.ValueMember = ""
    '
    'cbCritico
    '
    Me.cbCritico.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbCritico.DataSource = Nothing
    Me.cbCritico.DisplayMember = ""
    Me.cbCritico.Location = New System.Drawing.Point(257, 139)
    Me.cbCritico.Name = "cbCritico"
    Me.cbCritico.NTSDbField = ""
    Me.cbCritico.Properties.AutoHeight = False
    Me.cbCritico.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbCritico.Properties.DropDownRows = 30
    Me.cbCritico.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbCritico.SelectedValue = ""
    Me.cbCritico.Size = New System.Drawing.Size(60, 20)
    Me.cbCritico.TabIndex = 74
    Me.cbCritico.ValueMember = ""
    '
    'cbAlistino
    '
    Me.cbAlistino.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAlistino.DataSource = Nothing
    Me.cbAlistino.DisplayMember = ""
    Me.cbAlistino.Location = New System.Drawing.Point(257, 116)
    Me.cbAlistino.Name = "cbAlistino"
    Me.cbAlistino.NTSDbField = ""
    Me.cbAlistino.Properties.AutoHeight = False
    Me.cbAlistino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAlistino.Properties.DropDownRows = 30
    Me.cbAlistino.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAlistino.SelectedValue = ""
    Me.cbAlistino.Size = New System.Drawing.Size(60, 20)
    Me.cbAlistino.TabIndex = 73
    Me.cbAlistino.ValueMember = ""
    '
    'cbInesaur
    '
    Me.cbInesaur.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbInesaur.DataSource = Nothing
    Me.cbInesaur.DisplayMember = ""
    Me.cbInesaur.Location = New System.Drawing.Point(257, 94)
    Me.cbInesaur.Name = "cbInesaur"
    Me.cbInesaur.NTSDbField = ""
    Me.cbInesaur.Properties.AutoHeight = False
    Me.cbInesaur.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbInesaur.Properties.DropDownRows = 30
    Me.cbInesaur.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbInesaur.SelectedValue = ""
    Me.cbInesaur.Size = New System.Drawing.Size(60, 20)
    Me.cbInesaur.TabIndex = 72
    Me.cbInesaur.ValueMember = ""
    '
    'lbAfasi
    '
    Me.lbAfasi.AutoSize = True
    Me.lbAfasi.BackColor = System.Drawing.Color.Transparent
    Me.lbAfasi.Location = New System.Drawing.Point(186, 164)
    Me.lbAfasi.Name = "lbAfasi"
    Me.lbAfasi.NTSDbField = ""
    Me.lbAfasi.Size = New System.Drawing.Size(55, 13)
    Me.lbAfasi.TabIndex = 71
    Me.lbAfasi.Text = "Art. a fasi"
    Me.lbAfasi.Tooltip = ""
    Me.lbAfasi.UseMnemonic = False
    '
    'lbCritico
    '
    Me.lbCritico.AutoSize = True
    Me.lbCritico.BackColor = System.Drawing.Color.Transparent
    Me.lbCritico.Location = New System.Drawing.Point(186, 142)
    Me.lbCritico.Name = "lbCritico"
    Me.lbCritico.NTSDbField = ""
    Me.lbCritico.Size = New System.Drawing.Size(57, 13)
    Me.lbCritico.TabIndex = 70
    Me.lbCritico.Text = "Art. critico"
    Me.lbCritico.Tooltip = ""
    Me.lbCritico.UseMnemonic = False
    '
    'lbAlistino
    '
    Me.lbAlistino.AutoSize = True
    Me.lbAlistino.BackColor = System.Drawing.Color.Transparent
    Me.lbAlistino.Location = New System.Drawing.Point(186, 119)
    Me.lbAlistino.Name = "lbAlistino"
    Me.lbAlistino.NTSDbField = ""
    Me.lbAlistino.Size = New System.Drawing.Size(44, 13)
    Me.lbAlistino.TabIndex = 69
    Me.lbAlistino.Text = "A listino"
    Me.lbAlistino.Tooltip = ""
    Me.lbAlistino.UseMnemonic = False
    '
    'lbInesaur
    '
    Me.lbInesaur.AutoSize = True
    Me.lbInesaur.BackColor = System.Drawing.Color.Transparent
    Me.lbInesaur.Location = New System.Drawing.Point(186, 97)
    Me.lbInesaur.Name = "lbInesaur"
    Me.lbInesaur.NTSDbField = ""
    Me.lbInesaur.Size = New System.Drawing.Size(61, 13)
    Me.lbInesaur.TabIndex = 68
    Me.lbInesaur.Text = "In esaurim."
    Me.lbInesaur.Tooltip = ""
    Me.lbInesaur.UseMnemonic = False
    '
    'cbAvarianti
    '
    Me.cbAvarianti.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAvarianti.DataSource = Nothing
    Me.cbAvarianti.DisplayMember = ""
    Me.cbAvarianti.Location = New System.Drawing.Point(113, 184)
    Me.cbAvarianti.Name = "cbAvarianti"
    Me.cbAvarianti.NTSDbField = ""
    Me.cbAvarianti.Properties.AutoHeight = False
    Me.cbAvarianti.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAvarianti.Properties.DropDownRows = 30
    Me.cbAvarianti.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAvarianti.SelectedValue = ""
    Me.cbAvarianti.Size = New System.Drawing.Size(60, 20)
    Me.cbAvarianti.TabIndex = 67
    Me.cbAvarianti.ValueMember = ""
    '
    'cbUbicaz
    '
    Me.cbUbicaz.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.cbUbicaz.DataSource = Nothing
    Me.cbUbicaz.DisplayMember = ""
    Me.cbUbicaz.Location = New System.Drawing.Point(113, 161)
    Me.cbUbicaz.Name = "cbUbicaz"
    Me.cbUbicaz.NTSDbField = ""
    Me.cbUbicaz.Properties.AutoHeight = False
    Me.cbUbicaz.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbUbicaz.Properties.DropDownRows = 30
    Me.cbUbicaz.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbUbicaz.SelectedValue = ""
    Me.cbUbicaz.Size = New System.Drawing.Size(60, 20)
    Me.cbUbicaz.TabIndex = 66
    Me.cbUbicaz.ValueMember = ""
    '
    'cbMatricole
    '
    Me.cbMatricole.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbMatricole.DataSource = Nothing
    Me.cbMatricole.DisplayMember = ""
    Me.cbMatricole.Location = New System.Drawing.Point(113, 139)
    Me.cbMatricole.Name = "cbMatricole"
    Me.cbMatricole.NTSDbField = ""
    Me.cbMatricole.Properties.AutoHeight = False
    Me.cbMatricole.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbMatricole.Properties.DropDownRows = 30
    Me.cbMatricole.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbMatricole.SelectedValue = ""
    Me.cbMatricole.Size = New System.Drawing.Size(60, 20)
    Me.cbMatricole.TabIndex = 65
    Me.cbMatricole.ValueMember = ""
    '
    'cbCommessa
    '
    Me.cbCommessa.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbCommessa.DataSource = Nothing
    Me.cbCommessa.DisplayMember = ""
    Me.cbCommessa.Location = New System.Drawing.Point(113, 116)
    Me.cbCommessa.Name = "cbCommessa"
    Me.cbCommessa.NTSDbField = ""
    Me.cbCommessa.Properties.AutoHeight = False
    Me.cbCommessa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbCommessa.Properties.DropDownRows = 30
    Me.cbCommessa.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbCommessa.SelectedValue = ""
    Me.cbCommessa.Size = New System.Drawing.Size(60, 20)
    Me.cbCommessa.TabIndex = 64
    Me.cbCommessa.ValueMember = ""
    '
    'cbLotti
    '
    Me.cbLotti.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbLotti.DataSource = Nothing
    Me.cbLotti.DisplayMember = ""
    Me.cbLotti.Location = New System.Drawing.Point(113, 94)
    Me.cbLotti.Name = "cbLotti"
    Me.cbLotti.NTSDbField = ""
    Me.cbLotti.Properties.AutoHeight = False
    Me.cbLotti.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbLotti.Properties.DropDownRows = 30
    Me.cbLotti.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbLotti.SelectedValue = ""
    Me.cbLotti.Size = New System.Drawing.Size(60, 20)
    Me.cbLotti.TabIndex = 63
    Me.cbLotti.ValueMember = ""
    '
    'lbAvarianti
    '
    Me.lbAvarianti.AutoSize = True
    Me.lbAvarianti.BackColor = System.Drawing.Color.Transparent
    Me.lbAvarianti.Location = New System.Drawing.Point(14, 187)
    Me.lbAvarianti.Name = "lbAvarianti"
    Me.lbAvarianti.NTSDbField = ""
    Me.lbAvarianti.Size = New System.Drawing.Size(74, 13)
    Me.lbAvarianti.TabIndex = 62
    Me.lbAvarianti.Text = "Art. a varianti"
    Me.lbAvarianti.Tooltip = ""
    Me.lbAvarianti.UseMnemonic = False
    '
    'lbUbicaz
    '
    Me.lbUbicaz.AutoSize = True
    Me.lbUbicaz.BackColor = System.Drawing.Color.Transparent
    Me.lbUbicaz.Location = New System.Drawing.Point(14, 164)
    Me.lbUbicaz.Name = "lbUbicaz"
    Me.lbUbicaz.NTSDbField = ""
    Me.lbUbicaz.Size = New System.Drawing.Size(94, 13)
    Me.lbUbicaz.TabIndex = 61
    Me.lbUbicaz.Text = "Gest. ubic. dinam."
    Me.lbUbicaz.Tooltip = ""
    Me.lbUbicaz.UseMnemonic = False
    '
    'lbMatricole
    '
    Me.lbMatricole.AutoSize = True
    Me.lbMatricole.BackColor = System.Drawing.Color.Transparent
    Me.lbMatricole.Location = New System.Drawing.Point(14, 142)
    Me.lbMatricole.Name = "lbMatricole"
    Me.lbMatricole.NTSDbField = ""
    Me.lbMatricole.Size = New System.Drawing.Size(79, 13)
    Me.lbMatricole.TabIndex = 60
    Me.lbMatricole.Text = "Gest. matricole"
    Me.lbMatricole.Tooltip = ""
    Me.lbMatricole.UseMnemonic = False
    '
    'lbCommessa
    '
    Me.lbCommessa.AutoSize = True
    Me.lbCommessa.BackColor = System.Drawing.Color.Transparent
    Me.lbCommessa.Location = New System.Drawing.Point(14, 119)
    Me.lbCommessa.Name = "lbCommessa"
    Me.lbCommessa.NTSDbField = ""
    Me.lbCommessa.Size = New System.Drawing.Size(85, 13)
    Me.lbCommessa.TabIndex = 59
    Me.lbCommessa.Text = "Gest. commessa"
    Me.lbCommessa.Tooltip = ""
    Me.lbCommessa.UseMnemonic = False
    '
    'lbLotti
    '
    Me.lbLotti.AutoSize = True
    Me.lbLotti.BackColor = System.Drawing.Color.Transparent
    Me.lbLotti.Location = New System.Drawing.Point(14, 97)
    Me.lbLotti.Name = "lbLotti"
    Me.lbLotti.NTSDbField = ""
    Me.lbLotti.Size = New System.Drawing.Size(54, 13)
    Me.lbLotti.TabIndex = 58
    Me.lbLotti.Text = "Gest. lotti"
    Me.lbLotti.Tooltip = ""
    Me.lbLotti.UseMnemonic = False
    '
    'lbCodtaglda
    '
    Me.lbCodtaglda.AutoSize = True
    Me.lbCodtaglda.BackColor = System.Drawing.Color.Transparent
    Me.lbCodtaglda.Location = New System.Drawing.Point(14, 53)
    Me.lbCodtaglda.Name = "lbCodtaglda"
    Me.lbCodtaglda.NTSDbField = ""
    Me.lbCodtaglda.Size = New System.Drawing.Size(93, 13)
    Me.lbCodtaglda.TabIndex = 57
    Me.lbCodtaglda.Text = "Da / A scala taglie"
    Me.lbCodtaglda.Tooltip = ""
    Me.lbCodtaglda.UseMnemonic = False
    '
    'edCodtagla
    '
    Me.edCodtagla.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodtagla.EditValue = "9999"
    Me.edCodtagla.Location = New System.Drawing.Point(257, 50)
    Me.edCodtagla.Name = "edCodtagla"
    Me.edCodtagla.NTSDbField = ""
    Me.edCodtagla.NTSFormat = "0"
    Me.edCodtagla.NTSForzaVisZoom = False
    Me.edCodtagla.NTSOldValue = "9999"
    Me.edCodtagla.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodtagla.Properties.Appearance.Options.UseBackColor = True
    Me.edCodtagla.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodtagla.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodtagla.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodtagla.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodtagla.Properties.AutoHeight = False
    Me.edCodtagla.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodtagla.Properties.MaxLength = 65536
    Me.edCodtagla.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodtagla.Size = New System.Drawing.Size(60, 20)
    Me.edCodtagla.TabIndex = 56
    '
    'edCodtagld
    '
    Me.edCodtagld.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edCodtagld.EditValue = "0"
    Me.edCodtagld.Location = New System.Drawing.Point(114, 50)
    Me.edCodtagld.Name = "edCodtagld"
    Me.edCodtagld.NTSDbField = ""
    Me.edCodtagld.NTSFormat = "0"
    Me.edCodtagld.NTSForzaVisZoom = False
    Me.edCodtagld.NTSOldValue = ""
    Me.edCodtagld.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodtagld.Properties.Appearance.Options.UseBackColor = True
    Me.edCodtagld.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodtagld.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodtagld.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodtagld.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodtagld.Properties.AutoHeight = False
    Me.edCodtagld.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodtagld.Properties.MaxLength = 65536
    Me.edCodtagld.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodtagld.Size = New System.Drawing.Size(59, 20)
    Me.edCodtagld.TabIndex = 55
    '
    'lbCodstagda
    '
    Me.lbCodstagda.AutoSize = True
    Me.lbCodstagda.BackColor = System.Drawing.Color.Transparent
    Me.lbCodstagda.Location = New System.Drawing.Point(14, 31)
    Me.lbCodstagda.Name = "lbCodstagda"
    Me.lbCodstagda.NTSDbField = ""
    Me.lbCodstagda.Size = New System.Drawing.Size(81, 13)
    Me.lbCodstagda.TabIndex = 54
    Me.lbCodstagda.Text = "Da / A stagione"
    Me.lbCodstagda.Tooltip = ""
    Me.lbCodstagda.UseMnemonic = False
    '
    'edCodstaga
    '
    Me.edCodstaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodstaga.EditValue = "9999"
    Me.edCodstaga.Location = New System.Drawing.Point(257, 28)
    Me.edCodstaga.Name = "edCodstaga"
    Me.edCodstaga.NTSDbField = ""
    Me.edCodstaga.NTSFormat = "0"
    Me.edCodstaga.NTSForzaVisZoom = False
    Me.edCodstaga.NTSOldValue = "9999"
    Me.edCodstaga.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodstaga.Properties.Appearance.Options.UseBackColor = True
    Me.edCodstaga.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodstaga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodstaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodstaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodstaga.Properties.AutoHeight = False
    Me.edCodstaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodstaga.Properties.MaxLength = 65536
    Me.edCodstaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodstaga.Size = New System.Drawing.Size(60, 20)
    Me.edCodstaga.TabIndex = 53
    '
    'edCodstagd
    '
    Me.edCodstagd.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edCodstagd.EditValue = "0"
    Me.edCodstagd.Location = New System.Drawing.Point(114, 28)
    Me.edCodstagd.Name = "edCodstagd"
    Me.edCodstagd.NTSDbField = ""
    Me.edCodstagd.NTSFormat = "0"
    Me.edCodstagd.NTSForzaVisZoom = False
    Me.edCodstagd.NTSOldValue = ""
    Me.edCodstagd.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edCodstagd.Properties.Appearance.Options.UseBackColor = True
    Me.edCodstagd.Properties.Appearance.Options.UseTextOptions = True
    Me.edCodstagd.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCodstagd.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodstagd.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodstagd.Properties.AutoHeight = False
    Me.edCodstagd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodstagd.Properties.MaxLength = 65536
    Me.edCodstagd.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodstagd.Size = New System.Drawing.Size(59, 20)
    Me.edCodstagd.TabIndex = 52
    '
    'lbAnnoda
    '
    Me.lbAnnoda.AutoSize = True
    Me.lbAnnoda.BackColor = System.Drawing.Color.Transparent
    Me.lbAnnoda.Location = New System.Drawing.Point(14, 11)
    Me.lbAnnoda.Name = "lbAnnoda"
    Me.lbAnnoda.NTSDbField = ""
    Me.lbAnnoda.Size = New System.Drawing.Size(65, 13)
    Me.lbAnnoda.TabIndex = 51
    Me.lbAnnoda.Text = "Da / A Anno"
    Me.lbAnnoda.Tooltip = ""
    Me.lbAnnoda.UseMnemonic = False
    '
    'edAnnoa
    '
    Me.edAnnoa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnnoa.EditValue = "2099"
    Me.edAnnoa.Location = New System.Drawing.Point(257, 6)
    Me.edAnnoa.Name = "edAnnoa"
    Me.edAnnoa.NTSDbField = ""
    Me.edAnnoa.NTSFormat = "0"
    Me.edAnnoa.NTSForzaVisZoom = False
    Me.edAnnoa.NTSOldValue = "2099"
    Me.edAnnoa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnnoa.Properties.Appearance.Options.UseBackColor = True
    Me.edAnnoa.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnoa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnoa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnoa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnoa.Properties.AutoHeight = False
    Me.edAnnoa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnoa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnoa.Size = New System.Drawing.Size(60, 20)
    Me.edAnnoa.TabIndex = 50
    '
    'edAnnod
    '
    Me.edAnnod.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAnnod.EditValue = "1900"
    Me.edAnnod.Location = New System.Drawing.Point(114, 6)
    Me.edAnnod.Name = "edAnnod"
    Me.edAnnod.NTSDbField = ""
    Me.edAnnod.NTSFormat = "0"
    Me.edAnnod.NTSForzaVisZoom = False
    Me.edAnnod.NTSOldValue = "1900"
    Me.edAnnod.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edAnnod.Properties.Appearance.Options.UseBackColor = True
    Me.edAnnod.Properties.Appearance.Options.UseTextOptions = True
    Me.edAnnod.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAnnod.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAnnod.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAnnod.Properties.AutoHeight = False
    Me.edAnnod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAnnod.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAnnod.Size = New System.Drawing.Size(59, 20)
    Me.edAnnod.TabIndex = 49
    '
    'pnTab3Pan2
    '
    Me.pnTab3Pan2.AllowDrop = True
    Me.pnTab3Pan2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab3Pan2.Appearance.Options.UseBackColor = True
    Me.pnTab3Pan2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab3Pan2.Controls.Add(Me.ckCodiciRoot)
    Me.pnTab3Pan2.Controls.Add(Me.fmPrezzi)
    Me.pnTab3Pan2.Controls.Add(Me.fmSuccedanei)
    Me.pnTab3Pan2.Controls.Add(Me.fmCliforn)
    Me.pnTab3Pan2.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab3Pan2.Location = New System.Drawing.Point(341, 0)
    Me.pnTab3Pan2.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab3Pan2.Name = "pnTab3Pan2"
    Me.pnTab3Pan2.NTSActiveTrasparency = True
    Me.pnTab3Pan2.Size = New System.Drawing.Size(329, 234)
    Me.pnTab3Pan2.TabIndex = 5
    '
    'fmPrezzi
    '
    Me.fmPrezzi.AllowDrop = True
    Me.fmPrezzi.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmPrezzi.Appearance.Options.UseBackColor = True
    Me.fmPrezzi.Controls.Add(Me.edDtvalid)
    Me.fmPrezzi.Controls.Add(Me.lbListvalidita)
    Me.fmPrezzi.Controls.Add(Me.edListino)
    Me.fmPrezzi.Controls.Add(Me.lbListino)
    Me.fmPrezzi.Controls.Add(Me.ckVisprezzi)
    Me.fmPrezzi.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmPrezzi.Location = New System.Drawing.Point(3, 152)
    Me.fmPrezzi.Name = "fmPrezzi"
    Me.fmPrezzi.Size = New System.Drawing.Size(322, 61)
    Me.fmPrezzi.TabIndex = 16
    '
    'edDtvalid
    '
    Me.edDtvalid.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDtvalid.Location = New System.Drawing.Point(206, 30)
    Me.edDtvalid.Name = "edDtvalid"
    Me.edDtvalid.NTSDbField = ""
    Me.edDtvalid.NTSForzaVisZoom = False
    Me.edDtvalid.NTSOldValue = ""
    Me.edDtvalid.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDtvalid.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDtvalid.Properties.AutoHeight = False
    Me.edDtvalid.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDtvalid.Properties.MaxLength = 65536
    Me.edDtvalid.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDtvalid.Size = New System.Drawing.Size(100, 20)
    Me.edDtvalid.TabIndex = 10
    '
    'lbListvalidita
    '
    Me.lbListvalidita.AutoSize = True
    Me.lbListvalidita.BackColor = System.Drawing.Color.Transparent
    Me.lbListvalidita.Location = New System.Drawing.Point(154, 33)
    Me.lbListvalidita.Name = "lbListvalidita"
    Me.lbListvalidita.NTSDbField = ""
    Me.lbListvalidita.Size = New System.Drawing.Size(42, 13)
    Me.lbListvalidita.TabIndex = 9
    Me.lbListvalidita.Text = "Valido il"
    Me.lbListvalidita.Tooltip = ""
    Me.lbListvalidita.UseMnemonic = False
    '
    'edListino
    '
    Me.edListino.Cursor = System.Windows.Forms.Cursors.Default
    Me.edListino.EditValue = "0"
    Me.edListino.Location = New System.Drawing.Point(48, 30)
    Me.edListino.Name = "edListino"
    Me.edListino.NTSDbField = ""
    Me.edListino.NTSFormat = "0"
    Me.edListino.NTSForzaVisZoom = False
    Me.edListino.NTSOldValue = ""
    Me.edListino.Properties.Appearance.Options.UseTextOptions = True
    Me.edListino.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edListino.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edListino.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edListino.Properties.AutoHeight = False
    Me.edListino.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edListino.Properties.MaxLength = 65536
    Me.edListino.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edListino.Size = New System.Drawing.Size(48, 20)
    Me.edListino.TabIndex = 8
    '
    'lbListino
    '
    Me.lbListino.AutoSize = True
    Me.lbListino.BackColor = System.Drawing.Color.Transparent
    Me.lbListino.Location = New System.Drawing.Point(5, 33)
    Me.lbListino.Name = "lbListino"
    Me.lbListino.NTSDbField = ""
    Me.lbListino.Size = New System.Drawing.Size(37, 13)
    Me.lbListino.TabIndex = 7
    Me.lbListino.Text = "Listino"
    Me.lbListino.Tooltip = ""
    Me.lbListino.UseMnemonic = False
    '
    'ckVisprezzi
    '
    Me.ckVisprezzi.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckVisprezzi.Location = New System.Drawing.Point(7, 0)
    Me.ckVisprezzi.Name = "ckVisprezzi"
    Me.ckVisprezzi.NTSCheckValue = "S"
    Me.ckVisprezzi.NTSUnCheckValue = "N"
    Me.ckVisprezzi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckVisprezzi.Properties.Appearance.Options.UseBackColor = True
    Me.ckVisprezzi.Properties.AutoHeight = False
    Me.ckVisprezzi.Properties.Caption = "Visualizza prezzi"
    Me.ckVisprezzi.Size = New System.Drawing.Size(106, 19)
    Me.ckVisprezzi.TabIndex = 0
    '
    'fmSuccedanei
    '
    Me.fmSuccedanei.AllowDrop = True
    Me.fmSuccedanei.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmSuccedanei.Appearance.Options.UseBackColor = True
    Me.fmSuccedanei.Controls.Add(Me.edCodartAcc)
    Me.fmSuccedanei.Controls.Add(Me.lbCodartAcc)
    Me.fmSuccedanei.Controls.Add(Me.optSuccedanei)
    Me.fmSuccedanei.Controls.Add(Me.optAccessori)
    Me.fmSuccedanei.Controls.Add(Me.ckSuccedanei)
    Me.fmSuccedanei.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmSuccedanei.Location = New System.Drawing.Point(3, 63)
    Me.fmSuccedanei.Name = "fmSuccedanei"
    Me.fmSuccedanei.Size = New System.Drawing.Size(322, 86)
    Me.fmSuccedanei.TabIndex = 15
    '
    'edCodartAcc
    '
    Me.edCodartAcc.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCodartAcc.Location = New System.Drawing.Point(155, 58)
    Me.edCodartAcc.Name = "edCodartAcc"
    Me.edCodartAcc.NTSDbField = ""
    Me.edCodartAcc.NTSForzaVisZoom = False
    Me.edCodartAcc.NTSOldValue = ""
    Me.edCodartAcc.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCodartAcc.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCodartAcc.Properties.AutoHeight = False
    Me.edCodartAcc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCodartAcc.Properties.MaxLength = 65536
    Me.edCodartAcc.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCodartAcc.Size = New System.Drawing.Size(151, 20)
    Me.edCodartAcc.TabIndex = 7
    '
    'lbCodartAcc
    '
    Me.lbCodartAcc.AutoSize = True
    Me.lbCodartAcc.BackColor = System.Drawing.Color.Transparent
    Me.lbCodartAcc.Location = New System.Drawing.Point(5, 61)
    Me.lbCodartAcc.Name = "lbCodartAcc"
    Me.lbCodartAcc.NTSDbField = ""
    Me.lbCodartAcc.Size = New System.Drawing.Size(109, 13)
    Me.lbCodartAcc.TabIndex = 6
    Me.lbCodartAcc.Text = "Articolo di riferimento"
    Me.lbCodartAcc.Tooltip = ""
    Me.lbCodartAcc.UseMnemonic = False
    '
    'optSuccedanei
    '
    Me.optSuccedanei.Cursor = System.Windows.Forms.Cursors.Hand
    Me.optSuccedanei.Location = New System.Drawing.Point(91, 27)
    Me.optSuccedanei.Name = "optSuccedanei"
    Me.optSuccedanei.NTSCheckValue = "S"
    Me.optSuccedanei.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optSuccedanei.Properties.Appearance.Options.UseBackColor = True
    Me.optSuccedanei.Properties.AutoHeight = False
    Me.optSuccedanei.Properties.Caption = "Succedanei"
    Me.optSuccedanei.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optSuccedanei.Size = New System.Drawing.Size(84, 19)
    Me.optSuccedanei.TabIndex = 2
    '
    'optAccessori
    '
    Me.optAccessori.Cursor = System.Windows.Forms.Cursors.Default
    Me.optAccessori.EditValue = True
    Me.optAccessori.Location = New System.Drawing.Point(7, 27)
    Me.optAccessori.Name = "optAccessori"
    Me.optAccessori.NTSCheckValue = "S"
    Me.optAccessori.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optAccessori.Properties.Appearance.Options.UseBackColor = True
    Me.optAccessori.Properties.AutoHeight = False
    Me.optAccessori.Properties.Caption = "Accessori"
    Me.optAccessori.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optAccessori.Size = New System.Drawing.Size(78, 19)
    Me.optAccessori.TabIndex = 1
    '
    'ckSuccedanei
    '
    Me.ckSuccedanei.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSuccedanei.Location = New System.Drawing.Point(7, 1)
    Me.ckSuccedanei.Name = "ckSuccedanei"
    Me.ckSuccedanei.NTSCheckValue = "S"
    Me.ckSuccedanei.NTSUnCheckValue = "N"
    Me.ckSuccedanei.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSuccedanei.Properties.Appearance.Options.UseBackColor = True
    Me.ckSuccedanei.Properties.AutoHeight = False
    Me.ckSuccedanei.Properties.Caption = "Seleziona accessori / succedanei"
    Me.ckSuccedanei.Size = New System.Drawing.Size(184, 19)
    Me.ckSuccedanei.TabIndex = 0
    '
    'fmCliforn
    '
    Me.fmCliforn.AllowDrop = True
    Me.fmCliforn.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmCliforn.Appearance.Options.UseBackColor = True
    Me.fmCliforn.Controls.Add(Me.ckFiltraConto)
    Me.fmCliforn.Controls.Add(Me.ckFiltraMovmag)
    Me.fmCliforn.Controls.Add(Me.lbConto)
    Me.fmCliforn.Controls.Add(Me.edConto)
    Me.fmCliforn.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmCliforn.Location = New System.Drawing.Point(3, 4)
    Me.fmCliforn.Name = "fmCliforn"
    Me.fmCliforn.Size = New System.Drawing.Size(322, 56)
    Me.fmCliforn.TabIndex = 14
    '
    'ckFiltraConto
    '
    Me.ckFiltraConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFiltraConto.Location = New System.Drawing.Point(5, 0)
    Me.ckFiltraConto.Name = "ckFiltraConto"
    Me.ckFiltraConto.NTSCheckValue = "S"
    Me.ckFiltraConto.NTSUnCheckValue = "N"
    Me.ckFiltraConto.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFiltraConto.Properties.Appearance.Options.UseBackColor = True
    Me.ckFiltraConto.Properties.AutoHeight = False
    Me.ckFiltraConto.Properties.Caption = "Filtro Cliente/forn. (per cod. articolo C/F)"
    Me.ckFiltraConto.Size = New System.Drawing.Size(224, 19)
    Me.ckFiltraConto.TabIndex = 15
    '
    'ckFiltraMovmag
    '
    Me.ckFiltraMovmag.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckFiltraMovmag.Location = New System.Drawing.Point(154, 26)
    Me.ckFiltraMovmag.Name = "ckFiltraMovmag"
    Me.ckFiltraMovmag.NTSCheckValue = "S"
    Me.ckFiltraMovmag.NTSUnCheckValue = "N"
    Me.ckFiltraMovmag.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckFiltraMovmag.Properties.Appearance.Options.UseBackColor = True
    Me.ckFiltraMovmag.Properties.AutoHeight = False
    Me.ckFiltraMovmag.Properties.Caption = "Test su documenti di mag."
    Me.ckFiltraMovmag.Size = New System.Drawing.Size(152, 19)
    Me.ckFiltraMovmag.TabIndex = 7
    '
    'lbConto
    '
    Me.lbConto.AutoSize = True
    Me.lbConto.BackColor = System.Drawing.Color.Transparent
    Me.lbConto.Location = New System.Drawing.Point(3, 28)
    Me.lbConto.Name = "lbConto"
    Me.lbConto.NTSDbField = ""
    Me.lbConto.Size = New System.Drawing.Size(39, 13)
    Me.lbConto.TabIndex = 5
    Me.lbConto.Text = "Codice"
    Me.lbConto.Tooltip = ""
    Me.lbConto.UseMnemonic = False
    '
    'edConto
    '
    Me.edConto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edConto.EditValue = "0"
    Me.edConto.Location = New System.Drawing.Point(48, 25)
    Me.edConto.Name = "edConto"
    Me.edConto.NTSDbField = ""
    Me.edConto.NTSFormat = "0"
    Me.edConto.NTSForzaVisZoom = False
    Me.edConto.NTSOldValue = ""
    Me.edConto.Properties.Appearance.Options.UseTextOptions = True
    Me.edConto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edConto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edConto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edConto.Properties.AutoHeight = False
    Me.edConto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edConto.Properties.MaxLength = 65536
    Me.edConto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edConto.Size = New System.Drawing.Size(100, 20)
    Me.edConto.TabIndex = 6
    '
    'TabPage4
    '
    Me.TabPage4.AllowDrop = True
    Me.TabPage4.Controls.Add(Me.pnTab4Pan1)
    Me.TabPage4.Enable = True
    Me.TabPage4.Name = "TabPage4"
    Me.TabPage4.Size = New System.Drawing.Size(669, 236)
    Me.TabPage4.Text = "Immagine"
    '
    'pnTab4Pan1
    '
    Me.pnTab4Pan1.AllowDrop = True
    Me.pnTab4Pan1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTab4Pan1.Appearance.Options.UseBackColor = True
    Me.pnTab4Pan1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTab4Pan1.Controls.Add(Me.imArtGif)
    Me.pnTab4Pan1.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTab4Pan1.Location = New System.Drawing.Point(0, 0)
    Me.pnTab4Pan1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnTab4Pan1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnTab4Pan1.Margin = New System.Windows.Forms.Padding(0)
    Me.pnTab4Pan1.Name = "pnTab4Pan1"
    Me.pnTab4Pan1.NTSActiveTrasparency = True
    Me.pnTab4Pan1.Size = New System.Drawing.Size(357, 236)
    Me.pnTab4Pan1.TabIndex = 4
    '
    'imArtGif
    '
    Me.imArtGif.AllowDrop = True
    Me.imArtGif.Cursor = System.Windows.Forms.Cursors.Default
    Me.imArtGif.Dock = System.Windows.Forms.DockStyle.Fill
    Me.imArtGif.Location = New System.Drawing.Point(0, 0)
    Me.imArtGif.Name = "imArtGif"
    Me.imArtGif.NTSContextMenu = Nothing
    Me.imArtGif.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.imArtGif.Properties.Appearance.Options.UseBackColor = True
    Me.imArtGif.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.imArtGif.Properties.ShowMenu = False
    Me.imArtGif.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
    Me.imArtGif.Size = New System.Drawing.Size(357, 236)
    Me.imArtGif.TabIndex = 5
    '
    'pnAction
    '
    Me.pnAction.AllowDrop = True
    Me.pnAction.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAction.Appearance.Options.UseBackColor = True
    Me.pnAction.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAction.Controls.Add(Me.cmdEstensioni)
    Me.pnAction.Controls.Add(Me.cmdLastfilter)
    Me.pnAction.Controls.Add(Me.cmdProgressivi)
    Me.pnAction.Controls.Add(Me.cmdOrdini)
    Me.pnAction.Controls.Add(Me.cmdListini)
    Me.pnAction.Controls.Add(Me.cmdMovimenti)
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
    Me.pnAction.Size = New System.Drawing.Size(110, 266)
    Me.pnAction.TabIndex = 3
    '
    'cmdEstensioni
    '
    Me.cmdEstensioni.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdEstensioni.ImagePath = ""
    Me.cmdEstensioni.ImageText = ""
    Me.cmdEstensioni.Location = New System.Drawing.Point(6, 210)
    Me.cmdEstensioni.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdEstensioni.Name = "cmdEstensioni"
    Me.cmdEstensioni.NTSContextMenu = Nothing
    Me.cmdEstensioni.Size = New System.Drawing.Size(97, 22)
    Me.cmdEstensioni.TabIndex = 10
    Me.cmdEstensioni.Text = "&Estensioni"
    '
    'cmdLastfilter
    '
    Me.cmdLastfilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdLastfilter.ImagePath = ""
    Me.cmdLastfilter.ImageText = ""
    Me.cmdLastfilter.Location = New System.Drawing.Point(6, 76)
    Me.cmdLastfilter.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdLastfilter.Name = "cmdLastfilter"
    Me.cmdLastfilter.NTSContextMenu = Nothing
    Me.cmdLastfilter.Size = New System.Drawing.Size(97, 22)
    Me.cmdLastfilter.TabIndex = 11
    Me.cmdLastfilter.Text = "Ultime impostaz."
    '
    'cmdProgressivi
    '
    Me.cmdProgressivi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdProgressivi.ImagePath = ""
    Me.cmdProgressivi.ImageText = ""
    Me.cmdProgressivi.Location = New System.Drawing.Point(6, 188)
    Me.cmdProgressivi.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdProgressivi.Name = "cmdProgressivi"
    Me.cmdProgressivi.NTSContextMenu = Nothing
    Me.cmdProgressivi.Size = New System.Drawing.Size(97, 22)
    Me.cmdProgressivi.TabIndex = 10
    Me.cmdProgressivi.Text = "Progressivi"
    '
    'cmdOrdini
    '
    Me.cmdOrdini.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdOrdini.ImagePath = ""
    Me.cmdOrdini.ImageText = ""
    Me.cmdOrdini.Location = New System.Drawing.Point(6, 122)
    Me.cmdOrdini.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdOrdini.Name = "cmdOrdini"
    Me.cmdOrdini.NTSContextMenu = Nothing
    Me.cmdOrdini.Size = New System.Drawing.Size(97, 22)
    Me.cmdOrdini.TabIndex = 9
    Me.cmdOrdini.Text = "&Dettaglio ordini"
    '
    'cmdListini
    '
    Me.cmdListini.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdListini.ImagePath = ""
    Me.cmdListini.ImageText = ""
    Me.cmdListini.Location = New System.Drawing.Point(6, 166)
    Me.cmdListini.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdListini.Name = "cmdListini"
    Me.cmdListini.NTSContextMenu = Nothing
    Me.cmdListini.Size = New System.Drawing.Size(97, 22)
    Me.cmdListini.TabIndex = 7
    Me.cmdListini.Text = "Tutti i listini"
    '
    'cmdMovimenti
    '
    Me.cmdMovimenti.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdMovimenti.ImagePath = ""
    Me.cmdMovimenti.ImageText = ""
    Me.cmdMovimenti.Location = New System.Drawing.Point(6, 144)
    Me.cmdMovimenti.Margin = New System.Windows.Forms.Padding(3, 1, 3, 1)
    Me.cmdMovimenti.Name = "cmdMovimenti"
    Me.cmdMovimenti.NTSContextMenu = Nothing
    Me.cmdMovimenti.Size = New System.Drawing.Size(97, 22)
    Me.cmdMovimenti.TabIndex = 6
    Me.cmdMovimenti.Text = "&Movimenti"
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
    Me.ckOttimistico.Size = New System.Drawing.Size(88, 19)
    Me.ckOttimistico.TabIndex = 4
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(6, 54)
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
    Me.cmdGestione.Location = New System.Drawing.Point(6, 98)
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
    Me.cmdSeleziona.Location = New System.Drawing.Point(6, 32)
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
    Me.cmdRicerca.Location = New System.Drawing.Point(6, 10)
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
    Me.grZoom.Location = New System.Drawing.Point(271, 266)
    Me.grZoom.MainView = Me.grvZoom
    Me.grZoom.Name = "grZoom"
    Me.grZoom.Size = New System.Drawing.Size(517, 220)
    Me.grZoom.TabIndex = 2
    Me.grZoom.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvZoom})
    '
    'grvZoom
    '
    Me.grvZoom.ActiveFilterEnabled = False
    Me.grvZoom.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_seleziona, Me.ar_codart, Me.xx_codarfo, Me.ar_descr, Me.ar_unmis, Me.xx_esist, Me.xx_prezzo, Me.ar_codalt, Me.xx_code, Me.xx_prenot, Me.xx_ordin, Me.xx_impegn, Me.xx_dispnet, Me.xx_dispon, Me.xx_dispo2, Me.ar_desint, Me.ar_sostit, Me.ar_sostituito, Me.ar_inesaur, Me.ar_stalist, Me.ar_note, Me.xx_fase, Me.xx_descr, Me.ar_blocco})
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
    'xx_seleziona
    '
    Me.xx_seleziona.AppearanceCell.Options.UseBackColor = True
    Me.xx_seleziona.AppearanceCell.Options.UseTextOptions = True
    Me.xx_seleziona.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_seleziona.Caption = "Seleziona"
    Me.xx_seleziona.Enabled = True
    Me.xx_seleziona.FieldName = "xx_seleziona"
    Me.xx_seleziona.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_seleziona.Name = "xx_seleziona"
    Me.xx_seleziona.NTSRepositoryComboBox = Nothing
    Me.xx_seleziona.NTSRepositoryItemCheck = Nothing
    Me.xx_seleziona.NTSRepositoryItemMemo = Nothing
    Me.xx_seleziona.NTSRepositoryItemText = Nothing
    Me.xx_seleziona.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_seleziona.OptionsFilter.AllowFilter = False
    Me.xx_seleziona.Visible = True
    Me.xx_seleziona.VisibleIndex = 0
    '
    'ar_codart
    '
    Me.ar_codart.AppearanceCell.Options.UseBackColor = True
    Me.ar_codart.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codart.Caption = "Codice articolo"
    Me.ar_codart.Enabled = True
    Me.ar_codart.FieldName = "ar_codart"
    Me.ar_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codart.Name = "ar_codart"
    Me.ar_codart.NTSRepositoryComboBox = Nothing
    Me.ar_codart.NTSRepositoryItemCheck = Nothing
    Me.ar_codart.NTSRepositoryItemMemo = Nothing
    Me.ar_codart.NTSRepositoryItemText = Nothing
    Me.ar_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codart.OptionsFilter.AllowFilter = False
    Me.ar_codart.Visible = True
    Me.ar_codart.VisibleIndex = 1
    Me.ar_codart.Width = 146
    '
    'xx_codarfo
    '
    Me.xx_codarfo.AppearanceCell.Options.UseBackColor = True
    Me.xx_codarfo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codarfo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codarfo.Caption = "Cod. art. clinte/forn."
    Me.xx_codarfo.Enabled = True
    Me.xx_codarfo.FieldName = "xx_codarfo"
    Me.xx_codarfo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codarfo.Name = "xx_codarfo"
    Me.xx_codarfo.NTSRepositoryComboBox = Nothing
    Me.xx_codarfo.NTSRepositoryItemCheck = Nothing
    Me.xx_codarfo.NTSRepositoryItemMemo = Nothing
    Me.xx_codarfo.NTSRepositoryItemText = Nothing
    Me.xx_codarfo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codarfo.OptionsFilter.AllowFilter = False
    '
    'ar_descr
    '
    Me.ar_descr.AppearanceCell.Options.UseBackColor = True
    Me.ar_descr.AppearanceCell.Options.UseTextOptions = True
    Me.ar_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_descr.Caption = "Descrizione"
    Me.ar_descr.Enabled = True
    Me.ar_descr.FieldName = "ar_descr"
    Me.ar_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_descr.Name = "ar_descr"
    Me.ar_descr.NTSRepositoryComboBox = Nothing
    Me.ar_descr.NTSRepositoryItemCheck = Nothing
    Me.ar_descr.NTSRepositoryItemMemo = Nothing
    Me.ar_descr.NTSRepositoryItemText = Nothing
    Me.ar_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_descr.OptionsFilter.AllowFilter = False
    Me.ar_descr.Visible = True
    Me.ar_descr.VisibleIndex = 2
    '
    'ar_unmis
    '
    Me.ar_unmis.AppearanceCell.Options.UseBackColor = True
    Me.ar_unmis.AppearanceCell.Options.UseTextOptions = True
    Me.ar_unmis.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_unmis.Caption = "U.M."
    Me.ar_unmis.Enabled = True
    Me.ar_unmis.FieldName = "ar_unmis"
    Me.ar_unmis.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_unmis.Name = "ar_unmis"
    Me.ar_unmis.NTSRepositoryComboBox = Nothing
    Me.ar_unmis.NTSRepositoryItemCheck = Nothing
    Me.ar_unmis.NTSRepositoryItemMemo = Nothing
    Me.ar_unmis.NTSRepositoryItemText = Nothing
    Me.ar_unmis.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_unmis.OptionsFilter.AllowFilter = False
    Me.ar_unmis.Visible = True
    Me.ar_unmis.VisibleIndex = 3
    '
    'xx_esist
    '
    Me.xx_esist.AppearanceCell.Options.UseBackColor = True
    Me.xx_esist.AppearanceCell.Options.UseTextOptions = True
    Me.xx_esist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_esist.Caption = "Esist."
    Me.xx_esist.Enabled = True
    Me.xx_esist.FieldName = "xx_esist"
    Me.xx_esist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_esist.Name = "xx_esist"
    Me.xx_esist.NTSRepositoryComboBox = Nothing
    Me.xx_esist.NTSRepositoryItemCheck = Nothing
    Me.xx_esist.NTSRepositoryItemMemo = Nothing
    Me.xx_esist.NTSRepositoryItemText = Nothing
    Me.xx_esist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_esist.OptionsFilter.AllowFilter = False
    Me.xx_esist.Visible = True
    Me.xx_esist.VisibleIndex = 4
    '
    'xx_prezzo
    '
    Me.xx_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.xx_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_prezzo.Caption = "Prezzo"
    Me.xx_prezzo.Enabled = True
    Me.xx_prezzo.FieldName = "xx_prezzo"
    Me.xx_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_prezzo.Name = "xx_prezzo"
    Me.xx_prezzo.NTSRepositoryComboBox = Nothing
    Me.xx_prezzo.NTSRepositoryItemCheck = Nothing
    Me.xx_prezzo.NTSRepositoryItemMemo = Nothing
    Me.xx_prezzo.NTSRepositoryItemText = Nothing
    Me.xx_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_prezzo.OptionsFilter.AllowFilter = False
    '
    'ar_codalt
    '
    Me.ar_codalt.AppearanceCell.Options.UseBackColor = True
    Me.ar_codalt.AppearanceCell.Options.UseTextOptions = True
    Me.ar_codalt.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_codalt.Caption = "Cod.Altern."
    Me.ar_codalt.Enabled = True
    Me.ar_codalt.FieldName = "ar_codalt"
    Me.ar_codalt.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_codalt.Name = "ar_codalt"
    Me.ar_codalt.NTSRepositoryComboBox = Nothing
    Me.ar_codalt.NTSRepositoryItemCheck = Nothing
    Me.ar_codalt.NTSRepositoryItemMemo = Nothing
    Me.ar_codalt.NTSRepositoryItemText = Nothing
    Me.ar_codalt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_codalt.OptionsFilter.AllowFilter = False
    Me.ar_codalt.Visible = True
    Me.ar_codalt.VisibleIndex = 5
    '
    'xx_code
    '
    Me.xx_code.AppearanceCell.Options.UseBackColor = True
    Me.xx_code.AppearanceCell.Options.UseTextOptions = True
    Me.xx_code.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_code.Caption = "Barcode"
    Me.xx_code.Enabled = True
    Me.xx_code.FieldName = "xx_code"
    Me.xx_code.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_code.Name = "xx_code"
    Me.xx_code.NTSRepositoryComboBox = Nothing
    Me.xx_code.NTSRepositoryItemCheck = Nothing
    Me.xx_code.NTSRepositoryItemMemo = Nothing
    Me.xx_code.NTSRepositoryItemText = Nothing
    Me.xx_code.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_code.OptionsFilter.AllowFilter = False
    '
    'xx_prenot
    '
    Me.xx_prenot.AppearanceCell.Options.UseBackColor = True
    Me.xx_prenot.AppearanceCell.Options.UseTextOptions = True
    Me.xx_prenot.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_prenot.Caption = "Prenotato"
    Me.xx_prenot.Enabled = True
    Me.xx_prenot.FieldName = "xx_prenot"
    Me.xx_prenot.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_prenot.Name = "xx_prenot"
    Me.xx_prenot.NTSRepositoryComboBox = Nothing
    Me.xx_prenot.NTSRepositoryItemCheck = Nothing
    Me.xx_prenot.NTSRepositoryItemMemo = Nothing
    Me.xx_prenot.NTSRepositoryItemText = Nothing
    Me.xx_prenot.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_prenot.OptionsFilter.AllowFilter = False
    Me.xx_prenot.Visible = True
    Me.xx_prenot.VisibleIndex = 6
    '
    'xx_ordin
    '
    Me.xx_ordin.AppearanceCell.Options.UseBackColor = True
    Me.xx_ordin.AppearanceCell.Options.UseTextOptions = True
    Me.xx_ordin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_ordin.Caption = "Ordinato"
    Me.xx_ordin.Enabled = True
    Me.xx_ordin.FieldName = "xx_ordin"
    Me.xx_ordin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_ordin.Name = "xx_ordin"
    Me.xx_ordin.NTSRepositoryComboBox = Nothing
    Me.xx_ordin.NTSRepositoryItemCheck = Nothing
    Me.xx_ordin.NTSRepositoryItemMemo = Nothing
    Me.xx_ordin.NTSRepositoryItemText = Nothing
    Me.xx_ordin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_ordin.OptionsFilter.AllowFilter = False
    Me.xx_ordin.Visible = True
    Me.xx_ordin.VisibleIndex = 7
    '
    'xx_impegn
    '
    Me.xx_impegn.AppearanceCell.Options.UseBackColor = True
    Me.xx_impegn.AppearanceCell.Options.UseTextOptions = True
    Me.xx_impegn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_impegn.Caption = "Impegnato"
    Me.xx_impegn.Enabled = True
    Me.xx_impegn.FieldName = "xx_impegn"
    Me.xx_impegn.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_impegn.Name = "xx_impegn"
    Me.xx_impegn.NTSRepositoryComboBox = Nothing
    Me.xx_impegn.NTSRepositoryItemCheck = Nothing
    Me.xx_impegn.NTSRepositoryItemMemo = Nothing
    Me.xx_impegn.NTSRepositoryItemText = Nothing
    Me.xx_impegn.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_impegn.OptionsFilter.AllowFilter = False
    Me.xx_impegn.Visible = True
    Me.xx_impegn.VisibleIndex = 8
    '
    'xx_dispnet
    '
    Me.xx_dispnet.AppearanceCell.Options.UseBackColor = True
    Me.xx_dispnet.AppearanceCell.Options.UseTextOptions = True
    Me.xx_dispnet.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_dispnet.Caption = "Disp.Netta"
    Me.xx_dispnet.Enabled = True
    Me.xx_dispnet.FieldName = "xx_dispnet"
    Me.xx_dispnet.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_dispnet.Name = "xx_dispnet"
    Me.xx_dispnet.NTSRepositoryComboBox = Nothing
    Me.xx_dispnet.NTSRepositoryItemCheck = Nothing
    Me.xx_dispnet.NTSRepositoryItemMemo = Nothing
    Me.xx_dispnet.NTSRepositoryItemText = Nothing
    Me.xx_dispnet.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_dispnet.OptionsFilter.AllowFilter = False
    Me.xx_dispnet.Visible = True
    Me.xx_dispnet.VisibleIndex = 9
    '
    'xx_dispon
    '
    Me.xx_dispon.AppearanceCell.Options.UseBackColor = True
    Me.xx_dispon.AppearanceCell.Options.UseTextOptions = True
    Me.xx_dispon.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_dispon.Caption = "Disponib."
    Me.xx_dispon.Enabled = True
    Me.xx_dispon.FieldName = "xx_dispon"
    Me.xx_dispon.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_dispon.Name = "xx_dispon"
    Me.xx_dispon.NTSRepositoryComboBox = Nothing
    Me.xx_dispon.NTSRepositoryItemCheck = Nothing
    Me.xx_dispon.NTSRepositoryItemMemo = Nothing
    Me.xx_dispon.NTSRepositoryItemText = Nothing
    Me.xx_dispon.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_dispon.OptionsFilter.AllowFilter = False
    Me.xx_dispon.Visible = True
    Me.xx_dispon.VisibleIndex = 10
    '
    'xx_dispo2
    '
    Me.xx_dispo2.AppearanceCell.Options.UseBackColor = True
    Me.xx_dispo2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_dispo2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_dispo2.Caption = "Es.-Imp."
    Me.xx_dispo2.Enabled = True
    Me.xx_dispo2.FieldName = "xx_dispo2"
    Me.xx_dispo2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_dispo2.Name = "xx_dispo2"
    Me.xx_dispo2.NTSRepositoryComboBox = Nothing
    Me.xx_dispo2.NTSRepositoryItemCheck = Nothing
    Me.xx_dispo2.NTSRepositoryItemMemo = Nothing
    Me.xx_dispo2.NTSRepositoryItemText = Nothing
    Me.xx_dispo2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_dispo2.OptionsFilter.AllowFilter = False
    Me.xx_dispo2.Visible = True
    Me.xx_dispo2.VisibleIndex = 11
    '
    'ar_desint
    '
    Me.ar_desint.AppearanceCell.Options.UseBackColor = True
    Me.ar_desint.AppearanceCell.Options.UseTextOptions = True
    Me.ar_desint.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_desint.Caption = "Descr.2"
    Me.ar_desint.Enabled = True
    Me.ar_desint.FieldName = "ar_desint"
    Me.ar_desint.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_desint.Name = "ar_desint"
    Me.ar_desint.NTSRepositoryComboBox = Nothing
    Me.ar_desint.NTSRepositoryItemCheck = Nothing
    Me.ar_desint.NTSRepositoryItemMemo = Nothing
    Me.ar_desint.NTSRepositoryItemText = Nothing
    Me.ar_desint.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_desint.OptionsFilter.AllowFilter = False
    Me.ar_desint.Visible = True
    Me.ar_desint.VisibleIndex = 12
    '
    'ar_sostit
    '
    Me.ar_sostit.AppearanceCell.Options.UseBackColor = True
    Me.ar_sostit.AppearanceCell.Options.UseTextOptions = True
    Me.ar_sostit.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_sostit.Caption = "Art.sostitutivo"
    Me.ar_sostit.Enabled = True
    Me.ar_sostit.FieldName = "ar_sostit"
    Me.ar_sostit.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_sostit.Name = "ar_sostit"
    Me.ar_sostit.NTSRepositoryComboBox = Nothing
    Me.ar_sostit.NTSRepositoryItemCheck = Nothing
    Me.ar_sostit.NTSRepositoryItemMemo = Nothing
    Me.ar_sostit.NTSRepositoryItemText = Nothing
    Me.ar_sostit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_sostit.OptionsFilter.AllowFilter = False
    Me.ar_sostit.Visible = True
    Me.ar_sostit.VisibleIndex = 13
    '
    'ar_sostituito
    '
    Me.ar_sostituito.AppearanceCell.Options.UseBackColor = True
    Me.ar_sostituito.AppearanceCell.Options.UseTextOptions = True
    Me.ar_sostituito.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_sostituito.Caption = "Art.sostituito"
    Me.ar_sostituito.Enabled = True
    Me.ar_sostituito.FieldName = "ar_sostituito"
    Me.ar_sostituito.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_sostituito.Name = "ar_sostituito"
    Me.ar_sostituito.NTSRepositoryComboBox = Nothing
    Me.ar_sostituito.NTSRepositoryItemCheck = Nothing
    Me.ar_sostituito.NTSRepositoryItemMemo = Nothing
    Me.ar_sostituito.NTSRepositoryItemText = Nothing
    Me.ar_sostituito.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_sostituito.OptionsFilter.AllowFilter = False
    Me.ar_sostituito.Visible = True
    Me.ar_sostituito.VisibleIndex = 14
    '
    'ar_inesaur
    '
    Me.ar_inesaur.AppearanceCell.Options.UseBackColor = True
    Me.ar_inesaur.AppearanceCell.Options.UseTextOptions = True
    Me.ar_inesaur.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_inesaur.Caption = "In esaurim."
    Me.ar_inesaur.Enabled = True
    Me.ar_inesaur.FieldName = "ar_inesaur"
    Me.ar_inesaur.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_inesaur.Name = "ar_inesaur"
    Me.ar_inesaur.NTSRepositoryComboBox = Nothing
    Me.ar_inesaur.NTSRepositoryItemCheck = Nothing
    Me.ar_inesaur.NTSRepositoryItemMemo = Nothing
    Me.ar_inesaur.NTSRepositoryItemText = Nothing
    Me.ar_inesaur.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_inesaur.OptionsFilter.AllowFilter = False
    Me.ar_inesaur.Visible = True
    Me.ar_inesaur.VisibleIndex = 15
    '
    'ar_stalist
    '
    Me.ar_stalist.AppearanceCell.Options.UseBackColor = True
    Me.ar_stalist.AppearanceCell.Options.UseTextOptions = True
    Me.ar_stalist.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_stalist.Caption = "A listino"
    Me.ar_stalist.Enabled = True
    Me.ar_stalist.FieldName = "ar_stalist"
    Me.ar_stalist.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_stalist.Name = "ar_stalist"
    Me.ar_stalist.NTSRepositoryComboBox = Nothing
    Me.ar_stalist.NTSRepositoryItemCheck = Nothing
    Me.ar_stalist.NTSRepositoryItemMemo = Nothing
    Me.ar_stalist.NTSRepositoryItemText = Nothing
    Me.ar_stalist.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_stalist.OptionsFilter.AllowFilter = False
    Me.ar_stalist.Visible = True
    Me.ar_stalist.VisibleIndex = 16
    '
    'ar_note
    '
    Me.ar_note.AppearanceCell.Options.UseBackColor = True
    Me.ar_note.AppearanceCell.Options.UseTextOptions = True
    Me.ar_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_note.Caption = "Note art."
    Me.ar_note.Enabled = True
    Me.ar_note.FieldName = "ar_note"
    Me.ar_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_note.Name = "ar_note"
    Me.ar_note.NTSRepositoryComboBox = Nothing
    Me.ar_note.NTSRepositoryItemCheck = Nothing
    Me.ar_note.NTSRepositoryItemMemo = Nothing
    Me.ar_note.NTSRepositoryItemText = Nothing
    Me.ar_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_note.OptionsFilter.AllowFilter = False
    '
    'xx_fase
    '
    Me.xx_fase.AppearanceCell.Options.UseBackColor = True
    Me.xx_fase.AppearanceCell.Options.UseTextOptions = True
    Me.xx_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_fase.Caption = "Fase"
    Me.xx_fase.Enabled = True
    Me.xx_fase.FieldName = "xx_fase"
    Me.xx_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_fase.Name = "xx_fase"
    Me.xx_fase.NTSRepositoryComboBox = Nothing
    Me.xx_fase.NTSRepositoryItemCheck = Nothing
    Me.xx_fase.NTSRepositoryItemMemo = Nothing
    Me.xx_fase.NTSRepositoryItemText = Nothing
    Me.xx_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_fase.OptionsFilter.AllowFilter = False
    Me.xx_fase.Visible = True
    Me.xx_fase.VisibleIndex = 17
    '
    'xx_descr
    '
    Me.xx_descr.AppearanceCell.Options.UseBackColor = True
    Me.xx_descr.AppearanceCell.Options.UseTextOptions = True
    Me.xx_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_descr.Caption = "Descr.Fase"
    Me.xx_descr.Enabled = True
    Me.xx_descr.FieldName = "xx_descr"
    Me.xx_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_descr.Name = "xx_descr"
    Me.xx_descr.NTSRepositoryComboBox = Nothing
    Me.xx_descr.NTSRepositoryItemCheck = Nothing
    Me.xx_descr.NTSRepositoryItemMemo = Nothing
    Me.xx_descr.NTSRepositoryItemText = Nothing
    Me.xx_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_descr.OptionsFilter.AllowFilter = False
    Me.xx_descr.Visible = True
    Me.xx_descr.VisibleIndex = 18
    '
    'ar_blocco
    '
    Me.ar_blocco.AppearanceCell.Options.UseBackColor = True
    Me.ar_blocco.AppearanceCell.Options.UseTextOptions = True
    Me.ar_blocco.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.ar_blocco.Caption = "Bloccato"
    Me.ar_blocco.Enabled = True
    Me.ar_blocco.FieldName = "ar_blocco"
    Me.ar_blocco.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.ar_blocco.Name = "ar_blocco"
    Me.ar_blocco.NTSRepositoryComboBox = Nothing
    Me.ar_blocco.NTSRepositoryItemCheck = Nothing
    Me.ar_blocco.NTSRepositoryItemMemo = Nothing
    Me.ar_blocco.NTSRepositoryItemText = Nothing
    Me.ar_blocco.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.ar_blocco.OptionsFilter.AllowFilter = False
    Me.ar_blocco.Visible = True
    Me.ar_blocco.VisibleIndex = 19
    '
    'pnClassificazione
    '
    Me.pnClassificazione.AllowDrop = True
    Me.pnClassificazione.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnClassificazione.Appearance.Options.UseBackColor = True
    Me.pnClassificazione.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnClassificazione.Controls.Add(Me.trClass)
    Me.pnClassificazione.Controls.Add(Me.cmdFiltriClassificazione)
    Me.pnClassificazione.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnClassificazione.Location = New System.Drawing.Point(0, 266)
    Me.pnClassificazione.Name = "pnClassificazione"
    Me.pnClassificazione.NTSActiveTrasparency = True
    Me.pnClassificazione.Size = New System.Drawing.Size(271, 220)
    Me.pnClassificazione.TabIndex = 3
    Me.pnClassificazione.Text = "NtsPanel1"
    '
    'trClass
    '
    Me.trClass.Dock = System.Windows.Forms.DockStyle.Fill
    Me.trClass.Location = New System.Drawing.Point(0, 0)
    Me.trClass.Name = "trClass"
    Me.trClass.Size = New System.Drawing.Size(253, 220)
    Me.trClass.TabIndex = 0
    '
    'cmdFiltriClassificazione
    '
    Me.cmdFiltriClassificazione.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
    Me.cmdFiltriClassificazione.Dock = System.Windows.Forms.DockStyle.Right
    Me.cmdFiltriClassificazione.ImagePath = ""
    Me.cmdFiltriClassificazione.ImageText = ""
    Me.cmdFiltriClassificazione.Location = New System.Drawing.Point(253, 0)
    Me.cmdFiltriClassificazione.LookAndFeel.SkinName = "The Asphalt World"
    Me.cmdFiltriClassificazione.Name = "cmdFiltriClassificazione"
    Me.cmdFiltriClassificazione.NTSContextMenu = Nothing
    Me.cmdFiltriClassificazione.Size = New System.Drawing.Size(18, 220)
    Me.cmdFiltriClassificazione.TabIndex = 1
    Me.cmdFiltriClassificazione.Text = "<"
    '
    'ckCodiciRoot
    '
    Me.ckCodiciRoot.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCodiciRoot.Location = New System.Drawing.Point(8, 215)
    Me.ckCodiciRoot.Name = "ckCodiciRoot"
    Me.ckCodiciRoot.NTSCheckValue = "S"
    Me.ckCodiciRoot.NTSUnCheckValue = "N"
    Me.ckCodiciRoot.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCodiciRoot.Properties.Appearance.Options.UseBackColor = True
    Me.ckCodiciRoot.Properties.AutoHeight = False
    Me.ckCodiciRoot.Properties.Caption = "Mostra anche i codici padre degli articoli a varianti e TC"
    Me.ckCodiciRoot.Size = New System.Drawing.Size(317, 19)
    Me.ckCodiciRoot.TabIndex = 17
    '
    'FRMMGHLAR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
    Me.ClientSize = New System.Drawing.Size(788, 486)
    Me.Controls.Add(Me.grZoom)
    Me.Controls.Add(Me.pnClassificazione)
    Me.Controls.Add(Me.pnDescr)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGHLAR"
    Me.NTSLastControlFocussed = Me.grZoom
    Me.Text = "ZOOM ARTICOLI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnDescr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnDescr.ResumeLayout(False)
    CType(Me.tsZoom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsZoom.ResumeLayout(False)
    Me.TabPage1.ResumeLayout(False)
    CType(Me.pnTab1Pan2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab1Pan2.ResumeLayout(False)
    Me.pnTab1Pan2.PerformLayout()
    CType(Me.ckAbituali.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTestArt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodartLike.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edUbicLike.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescrLike.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckBloccati.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipologia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMagaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTestdb.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodarfo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edBarcode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTab1Pan1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab1Pan1.ResumeLayout(False)
    CType(Me.grFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage2.ResumeLayout(False)
    CType(Me.pnTab2Pan2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab2Pan2.ResumeLayout(False)
    Me.pnTab2Pan2.PerformLayout()
    CType(Me.edMagprodfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMagprodini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMagstockfin.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMagstockini.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello5.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello4.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edClassificazioneLivello1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodIvaa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edApprova.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodIvad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edApprovd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edScontia.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edProvva.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edScontid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edProvvd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMarcaa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edMarcad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTab2Pan1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab2Pan1.ResumeLayout(False)
    Me.pnTab2Pan1.PerformLayout()
    CType(Me.edCodtipaa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodtipad.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edForna.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFornd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataUltaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFamproda.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataUltagd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFamprodd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescra.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDescrd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSotta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSottd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edGruppoa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edGruppod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodalta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodaltd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodarta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodartd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage3.ResumeLayout(False)
    CType(Me.pnTab3Pan1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab3Pan1.ResumeLayout(False)
    Me.pnTab3Pan1.PerformLayout()
    CType(Me.edListsar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edArtprom.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDBLike.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAfasi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbCritico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAlistino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbInesaur.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAvarianti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbUbicaz.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbMatricole.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbCommessa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbLotti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodtagla.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodtagld.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodstaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCodstagd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnoa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAnnod.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTab3Pan2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab3Pan2.ResumeLayout(False)
    Me.pnTab3Pan2.PerformLayout()
    CType(Me.fmPrezzi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmPrezzi.ResumeLayout(False)
    Me.fmPrezzi.PerformLayout()
    CType(Me.edDtvalid.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edListino.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckVisprezzi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmSuccedanei, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmSuccedanei.ResumeLayout(False)
    Me.fmSuccedanei.PerformLayout()
    CType(Me.edCodartAcc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optSuccedanei.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optAccessori.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSuccedanei.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmCliforn, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmCliforn.ResumeLayout(False)
    Me.fmCliforn.PerformLayout()
    CType(Me.ckFiltraConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckFiltraMovmag.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edConto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage4.ResumeLayout(False)
    CType(Me.pnTab4Pan1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTab4Pan1.ResumeLayout(False)
    CType(Me.imArtGif.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnAction, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAction.ResumeLayout(False)
    Me.pnAction.PerformLayout()
    CType(Me.ckOttimistico.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvZoom, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnClassificazione, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnClassificazione.ResumeLayout(False)
    CType(Me.ckCodiciRoot.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Function CreaDatatableFiltri() As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim strTmp As String = ""
    Dim strT() As String = Nothing
    Dim i As Integer = 0
    Try
      '---------------------------
      'creo il datatable per contenere i filtri principali
      dsFiltri.Tables.Add("FILTRI1")
      dsFiltri.Tables("FILTRI1").Columns.Add("xx_nome", GetType(String))
      dsFiltri.Tables("FILTRI1").Columns.Add("xx_valore", GetType(String))
      'impostazioni standard
      dtrT = dttCampi.Select("cb_nomcampo = 'artico.ar_descr'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, ""})
      dtrT = dttCampi.Select("cb_nomcampo = 'artico.ar_codart'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, ""})
      dtrT = dttCampi.Select("cb_nomcampo = 'artico.ar_codalt'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, ""})
      dtrT = dttCampi.Select("cb_nomcampo = 'artico.ar_desint'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, ""})
      dtrT = dttCampi.Select("cb_nomcampo = 'artico.ar_forn'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, ""})
      dsFiltri.AcceptChanges()

      'impostazioni come da recent utente
      strTmp = NTSCStr(oMenu.GetSettingBus("BNMGHLAR", "RECENT", ".", "Filtri1", "", " ", ""))
      If strTmp.Trim.Length > 0 Then
        strT = strTmp.Split(";"c)
        For i = 0 To strT.Length - 1
          If strT(i).Trim <> "" Then dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome = strT(i)
          If i > 10 Then Exit For
        Next
      End If
      dsFiltri.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  'Reimpostazione filtri da PARSTAF
  'Qualche valore default di Parstaf è fuori dai range impostati attraverso NTSSetParam
  'Per evitare che il messagio di errore venga mandato durante la validazione
  'bisogna disabilitare la validazione del controllo.Text
  Public Overridable Sub RemoveHandlers(ByVal ctrlIn As Control)
    Try
      For Each contr As Control In ctrlIn.Controls
        Select Case contr.GetType.ToString
          Case "NTSInformatica.NTSTextBoxNum"
            RemoveHandler CType(contr, NTSTextBoxNum).TextChanged, AddressOf CType(contr, NTSTextBoxNum).NTSTextBoxNum_TextChanged
          Case "NTSInformatica.NTSTextBoxStr"
          Case "NTSInformatica.NTSTextBoxData"
          Case Else
            If contr.Controls.Count > 0 Then
              RemoveHandlers(contr)
            End If
        End Select
      Next
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  'Reimpostazione filtri da PARSTAF
  'Ripristino di handler precedentemente disabilitati
  Public Overridable Sub AddHandlers(ByVal ctrlIn As Control)
    Try
      For Each contr As Control In ctrlIn.Controls
        Select Case contr.GetType.ToString
          Case "NTSInformatica.NTSTextBoxNum"
            AddHandler CType(contr, NTSTextBoxNum).TextChanged, AddressOf CType(contr, NTSTextBoxNum).NTSTextBoxNum_TextChanged
          Case "NTSInformatica.NTSTextBoxStr"
          Case "NTSInformatica.NTSTextBoxData"
          Case Else
            If contr.Controls.Count > 0 Then
              AddHandlers(contr)
            End If
        End Select
      Next
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  'Reimpostazione filtri da PARSTAF
  Public Overridable Sub BindControls()
    Try
      '-------------------------------------------------
      'se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)
      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      'tab2
      edCodartd.NTSDbField = "parstaf.pf_codartini"
      edCodarta.NTSDbField = "parstaf.pf_codartfin"

      edDescrd.NTSDbField = "parstaf.pf_descrd"
      edDescra.NTSDbField = "parstaf.pf_descra"

      edCodaltd.NTSDbField = "parstaf.pf_codaltini"
      edCodalta.NTSDbField = "parstaf.pf_codaltfin"

      edGruppod.NTSDbField = "parstaf.pf_gruppoini"
      edGruppoa.NTSDbField = "parstaf.pf_gruppofin"

      edSottd.NTSDbField = "parstaf.pf_sottd"
      edSotta.NTSDbField = "parstaf.pf_sotta"

      edFamprodd.NTSDbField = "parstaf.pf_famprodini"
      edFamproda.NTSDbField = "parstaf.pf_famprodfin"
      edClassificazioneLivello1.NTSDbField = "parstaf.pf_codcla1"
      edClassificazioneLivello2.NTSDbField = "parstaf.pf_codcla2"
      edClassificazioneLivello3.NTSDbField = "parstaf.pf_codcla3"
      edClassificazioneLivello4.NTSDbField = "parstaf.pf_codcla4"
      edClassificazioneLivello5.NTSDbField = "parstaf.pf_codcla5"

      edFornd.NTSDbField = "parstaf.pf_fornini"
      edForna.NTSDbField = "parstaf.pf_fornfin"

      edCodtipad.NTSDbField = "parstaf.pf_codtipad"
      edCodtipaa.NTSDbField = "parstaf.pf_codtipaa"

      edMarcad.NTSDbField = "parstaf.pf_marcad"
      edMarcaa.NTSDbField = "parstaf.pf_marcaa"

      edProvvd.NTSDbField = "parstaf.pf_claprovd"
      edProvva.NTSDbField = "parstaf.pf_claprova"

      edScontid.NTSDbField = "parstaf.pf_clascond"
      edScontia.NTSDbField = "parstaf.pf_clascona"

      edApprovd.NTSDbField = "parstaf.pf_approvda"
      edApprova.NTSDbField = "parstaf.pf_approva"

      edCodIvad.NTSDbField = "parstaf.pf_codivad"
      edCodIvaa.NTSDbField = "parstaf.pf_codivaa"

      edDataUltagd.NTSDbField = "parstaf.pf_dataultagd"
      edDataUltaga.NTSDbField = "parstaf.pf_dataultaga"

      edMagstockini.NTSDbField = "parstaf.pf_magstockini"
      edMagstockfin.NTSDbField = "parstaf.pf_magstockfin"

      edMagprodini.NTSDbField = "parstaf.pf_magprodini"
      edMagprodfin.NTSDbField = "parstaf.pf_magprodfin"

      'tab3
      edAnnod.NTSDbField = "parstaf.pf_annod"
      edAnnoa.NTSDbField = "parstaf.pf_annoa"

      edCodstagd.NTSDbField = "parstaf.pf_codstagd"
      edCodstaga.NTSDbField = "parstaf.pf_codstaga"

      edCodtagld.NTSDbField = "parstaf.pf_codtagld"
      edCodtagla.NTSDbField = "parstaf.pf_codtagla"

      cbLotti.NTSDbField = "parstaf.pf_lotti"
      cbCommessa.NTSDbField = "parstaf.pf_commessa"
      cbMatricole.NTSDbField = "parstaf.pf_matricole"
      cbUbicaz.NTSDbField = "parstaf.pf_gesubic"
      cbAvarianti.NTSDbField = "parstaf.pf_varianti"
      cbInesaur.NTSDbField = "parstaf.pf_esaurito"
      cbAlistino.NTSDbField = "parstaf.pf_stalist"
      cbCritico.NTSDbField = "parstaf.pf_critico"
      cbAfasi.NTSDbField = "parstaf.pf_gesfasi"

      'tab1
      edTipo.NTSDbField = "parstaf.pf_2tipo".Trim
      edDescrLike.NTSDbField = "parstaf.pf_filtro".Trim
      edUbicLike.NTSDbField = "parstaf.pf_filtroubic".Trim
      cbTestdb.NTSDbField = "parstaf.pf_filtrodb"

      RemoveHandlers(Me)
      NTSFormAddDataBinding(dcParstaf, Me)
      AddHandlers(Me)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  'Reimpostazione filtri da PARSTAF
  Public Overridable Sub TranslateDefaultValues()
    Try
      'tab1
      If cbTestdb.SelectedIndex = -1 Then
        cbTestdb.SelectedValue = "T"
      End If

      'tab2
      If edCodarta.Text.Length = 0 Then
        edCodarta.Text = "".PadLeft(CLN__STD.CodartMaxLen, "Z"c)
      End If
      If edDescra.Text.Length = 0 Then
        edDescra.Text = "".PadLeft(40, "Z"c)
      End If
      If edCodalta.Text.Length = 0 Then
        edCodalta.Text = "".PadLeft(CLN__STD.CodartMaxLen, "Z"c)
      End If
      If CInt(edGruppoa.Text) = 0 Then
        edGruppoa.Text = "".PadLeft(2, "9"c)
      End If
      If CInt(edSotta.Text) = 0 Then
        edSotta.Text = "".PadLeft(4, "9"c)
      End If
      If edFamproda.Text.Length = 0 Then
        edFamproda.Text = "".PadLeft(4, "Z"c)
      End If
      If CInt(edForna.Text) = 0 Then
        edForna.Text = "".PadLeft(9, "9"c)
      End If
      If CInt(edMarcaa.Text) = 0 Then
        edMarcaa.Text = "".PadLeft(3, "9"c)
      End If
      If CInt(edProvva.Text) = 0 Then
        edProvva.Text = "".PadLeft(3, "9"c)
      End If
      If CInt(edScontia.Text) = 0 Then
        edScontia.Text = "".PadLeft(3, "9"c)
      End If
      If CInt(edApprova.Text) = 0 Then
        edApprova.Text = "".PadLeft(3, "9"c)
      End If
      If CInt(edCodIvaa.Text) = 0 Then
        edCodIvaa.Text = "".PadLeft(4, "9"c)
      End If

      If edDataUltagd.Text.Length = 0 Then
        edDataUltagd.Text = MIN_DATA
      End If
      If edDataUltaga.Text.Length = 0 Then
        edDataUltaga.Text = MAX_DATA
      End If

      'tab3
      If CInt(edAnnod.Text) = 0 Then
        edAnnod.Text = CStr(MIN_ANNO)
      End If
      If CInt(edAnnoa.Text) = 9999 Then
        edAnnoa.Text = CStr(MAX_ANNO)
      End If
      If cbLotti.SelectedIndex = -1 Then
        cbLotti.SelectedValue = "T"
      End If
      If cbCommessa.SelectedIndex = -1 Then
        cbCommessa.SelectedValue = "T"
      End If
      If cbMatricole.SelectedIndex = -1 Then
        cbMatricole.SelectedValue = "T"
      End If
      If cbUbicaz.SelectedIndex = -1 Then
        cbUbicaz.SelectedValue = "T"
      End If
      If cbAvarianti.SelectedIndex = -1 Then
        cbAvarianti.SelectedValue = "T"
      End If
      If cbInesaur.SelectedIndex = -1 Then
        cbInesaur.SelectedValue = "T"
      End If
      If cbAlistino.SelectedIndex = -1 Then
        cbAlistino.SelectedValue = "T"
      End If
      If cbCritico.SelectedIndex = -1 Then
        cbCritico.SelectedValue = "T"
      End If
      If cbAfasi.SelectedIndex = -1 Then
        cbAfasi.SelectedValue = "T"
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)

    End Try
  End Sub
#End Region


#Region "Eventi di FORM"
  Public Overridable Sub FRMMGHLAR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim nMagaz As Integer = 0
    Dim bNoCodarfo As Boolean = False
    Dim strTmpList As String = ""
    Dim szForm As Size
    Dim i As Integer = 0

    Try
      NTSDisableEnterComeTab()
      '-------------------------------------------------
      'leggo dal database i potenziali filtri su artico
      If Not CType(oMenu.oCleComm, CLELBMENU).LeggiCampiPerHlvl("artico", dttCampi) Then
        Me.Close()
        Return
      End If

      'cb_nomcampo, xx_nome, cb_tipocampo, cb_size, xx_valore
      dttCampi.Rows.Add(New Object() {".", "N.A.", 0, 0, ""})
      dttCampi.AcceptChanges()

      CreaDatatableFiltri()

      dcFiltri1.DataSource = dsFiltri.Tables("FILTRI1")
      dsFiltri.AcceptChanges()
      grFiltri1.DataSource = dcFiltri1
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()
      '-------------------------------------------------
      'setto il recent
      ckOttimistico.Checked = CBool(oMenu.GetSettingBus("BNMGHLAR", "RECENT", ".", "Ottimistico", "0", " ", "0"))

      xx_nome.Width = 170     'per impaginare la griglia fin dal primo avvio
      xx_valore.Width = 163
      tsZoom.SelectedTabPageIndex = 0

      edCodarta.Text = "".PadLeft(CLN__STD.CodartMaxLen, "Z"c)
      edCodalta.Text = "".PadLeft(CLN__STD.CodartMaxLen, "Z"c)
      edDescra.Text = "".PadLeft(40, "Z"c)
      edDtvalid.Text = DateTime.Now.ToShortDateString

      ckVisprezzi.Checked = False
      ckFiltraConto.Checked = False
      ckSuccedanei.Checked = False
      '-------------------------------------------------
      'parametri passati dal chiamante
      'oparam.bVisGriglia
      'oParam.nMagaz
      'oParam.nListino
      'oParam.lContoCF
      'oParam.nTipologia
      'oParam.strDescr
      'oParam.strTipoArticolo
      'oParam.strCodartAcc
      'oparam.bTipoProposto
      oParam.strIn = oParam.strIn.Trim
      edMagaz.Text = oParam.nMagaz.ToString
      edListino.Text = oParam.nListino.ToString
      bProponiTipoArticolo = CBool(oMenu.GetSettingBus("BSMGHLAR", "OPZIONI", ".", "Proponi_Tipo_Articolo", "-1", " ", "-1"))
      If bProponiTipoArticolo Then
        Select Case oParam.strCodPdc
          Case ""
            cbTestArt.SelectedValue = "T"
          Case "CP2"
            'configurazioni di partenza (padri di nuove configurazioni)
            cbTestArt.SelectedValue = "T"
          Case "D"
            'distinta base
            cbTestdb.SelectedValue = "S"
          Case Else 'N = normali, V = a varianti, C = a taglie e colori
            cbTestArt.SelectedValue = oParam.strCodPdc
        End Select
      End If
      If oParam.bLiv2 And oParam.strTipoBil <> "" Then ckVisprezzi.Checked = CBool(oParam.strTipoBil) 'settato da zoom 2 livelli da prima chiamata
      edConto.Text = oParam.lContoCF.ToString
      edCodartAcc.Text = oParam.strCodartAcc
      oParam.strDescr = oParam.strDescr.Trim
      If oParam.strDescr = "" Then oParam.strDescr = oParam.strIn 'ho digitato del testo nel campo cod articolo senza virgola finale, quindi ho fatto F5
      If oParam.strTipo <> "" Then
        If oParam.strTipo = "BNMGSTLI" Then
          cbAlistino.SelectedValue = "S"
        End If
      End If
      'se sono stato chiamato da mghlar per visualizzare il dettaglio varianti sposto l'articolo in codroot
      If oParam.bLiv2 Then
        strCodRoot = oParam.strDescr
        oParam.strDescr = ""
      End If

      bCercaInDescr2 = CBool(oMenu.GetSettingBus("BSMGHLAR", "OPZIONI", ".", "Cerca_anche__in_descr2", "0", " ", "0"))

      '-------------------------------------------------
      nMagaz = NTSCInt(oMenu.GetSettingBus("BSMGHLAR", "OPZIONI", ".", "Magazzino", "0", " ", "0"))
      If oParam.nMagaz > 0 Then
        If nMagaz = -1 Then edMagaz.Text = "0"
      Else
        If nMagaz = -1 Then
          edMagaz.Text = "0"
        Else
          edMagaz.Text = nMagaz.ToString
        End If
      End If

      bNoCodarfo = CBool(oMenu.GetSettingBus("BSMGHLAR", "OPZIONI", ".", "NoCodarfo", "0", " ", "0"))
      If oParam.lContoCF > 0 And (Not bNoCodarfo) Then ckFiltraConto.Checked = True
      '-------------------------------------------------
      Select Case oParam.strTipoArticolo
        Case "N"
          edCodartAcc.Text = ""
        Case "A"
          ckSuccedanei.Checked = True
          ckSuccedanei_CheckedChanged(ckSuccedanei, Nothing)
          optAccessori.Checked = True
        Case "S"
          ckSuccedanei.Checked = True
          ckSuccedanei_CheckedChanged(ckSuccedanei, Nothing)
          optSuccedanei.Checked = True
      End Select

      '--------------------------------------------------
      'leggo tabtipa e se non serve nascondo i campi
      If Not oCleHlar.GetTabtipa(DittaCorrente, dttTipa) Then
        Me.Close()
        Return
      End If

      cbTipologia.DataSource = dttTipa : cbTipologia.DisplayMember = "val" : cbTipologia.ValueMember = "cod"
      If oParam.nTipologia <> 0 Then
        cbTipologia.SelectedValue = oParam.nTipologia.ToString
        If cbTipologia.SelectedIndex = -1 Then cbTipologia.SelectedIndex = 0
        If oParam.bVisGriglia Then
          If NTSCInt(cbTipologia.SelectedValue) = 0 Then
            edCodtipad.Text = "0"
            edCodtipaa.Text = "".PadLeft(3, "9"c)
          Else
            edCodtipad.Text = cbTipologia.SelectedValue
            edCodtipaa.Text = cbTipologia.SelectedValue
          End If
        End If
      Else
        cbTipologia.SelectedIndex = 0
      End If
      If dttTipa.Rows.Count <= 1 Then
        cbTipologia.Visible = False
        lbTipologia.Visible = False
      End If

      '-------------------------------------------------
      If oParam.bVisGriglia Then
        strOpz4 = oMenu.GetSettingBus("BSMGHLAR", "OPZIONI", ".", "Zoom_articoli_2_livelli", " ", " ", " ")
        If strOpz4 = "S" Then
          edCodarfo.Enabled = False
          edBarcode.Enabled = False
          ckCodiciRoot.Checked = False
          ckCodiciRoot.Visible = False
        Else
          ckCodiciRoot.Checked = oMenu.GetSettingBus("BSMGHLAR", "RECENT", ".", "ckCodiciRoot", "0", " ", "0") <> "0"
        End If

        If oMenu.GetSettingBus("BSMGHLAR", "RECENT", ".", "ArticoliBloccati", "*", " ", "*") = "*" Then
          ckBloccati.Checked = True
        Else
          ckBloccati.Checked = False
        End If
      Else
        strOpz4 = " "
        ckCodiciRoot.Checked = False
        ckCodiciRoot.Visible = False
      End If



      '-------------------------------------------------
      If oParam.bLiv2 = False Then
        ckVisprezzi.Checked = CBool(oMenu.GetSettingBus("BSMGHLAR", "OPZIONI", ".", "VisPrezzi", "0", " ", "0"))
        If ckVisprezzi.Checked Then
          ckVisprezzi_CheckedChanged(ckVisprezzi, Nothing)
          strTmpList = oMenu.GetSettingBus("BSMGHLAR", "OPZIONI", ".", "Listino", "?", " ", "?")
          If strTmpList <> "?" Then edListino.Text = NTSCInt(strTmpList).ToString
        End If
      End If

      cbTipologia.SelectedValue = oMenu.GetSettingBus("BNMGHLAR", "RECENT", ".", "Tipologia", "1", " ", "1")

      '-------------------------------------------------
      If oParam.bVisGriglia = False Then
        GctlSetVisEnab(cmdLastfilter, True)
        cmdRicerca.Text = oApp.Tr(Me, 128366686855384000, "Conferma")
        grZoom.Visible = False
        cmdGestione.Visible = False
        cmdSeleziona.Visible = False
        cmdListini.Visible = False
        cmdOrdini.Visible = False
        cmdMovimenti.Visible = False
        szForm.Width = Me.MinimumSize.Width
        szForm.Height = Me.tsZoom.Height - Me.ClientRectangle.Height + Me.Height
        Me.MinimumSize = szForm
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        Me.Width = Me.MinimumSize.Width
        Me.CenterToScreen()
        Me.MaximizeBox = False
        Me.Height = Me.MinimumSize.Height
        ckBloccati.Checked = True
        'ckOttimistico.Checked = False
        'ckOttimistico.Visible = False
        cbTipologia.Visible = False
        lbTipologia.Visible = False

        'disabilito tutti i filtri che non lavorano su artico
        '------------------------------------------------------------------------------------------------------------
        '--- Se chiamato da Desktop Consolle, i filtri su Barcode e Codarfo rimangono visibili
        '------------------------------------------------------------------------------------------------------------
        If oParam.strAlfpar.ToUpper.Trim <> "BNDKKONS" Then
          edBarcode.Visible = False
          lbBarcode.Visible = False
          edCodarfo.Visible = False
          lbCodarfo.Visible = False
        End If
        '------------------------------------------------------------------------------------------------------------
        fmPrezzi.Visible = False
        fmSuccedanei.Visible = False
        fmCliforn.Visible = False
        ckAbituali.Checked = False
        ckAbituali.Enabled = False
        ckAbituali.Visible = False
        pnClassificazione.Visible = False
        tsZoom.SelectedTabPageIndex = 1
        edCodartd.Focus()
      Else
        cmdClassifica.Visible = False
        cmdClassificaDeleteFilter.Visible = False
        lbClassifica.Visible = False
        edCodtipad.Enabled = False
        edCodtipaa.Enabled = False
        cmdLastfilter.Visible = False
        grZoom_Enter(grZoom, Nothing)
        ckAbituali.Checked = CBool(oMenu.GetSettingBus("BNMGHLAR", "RECENT", ".", "ClientiFornitoriAbituali", "0", " ", "0"))
        'If (ckAbituali.Checked = False) And (CLN__STD.IsBis = True) Then ckAbituali.Checked = True
      End If

      If bReimpostazioneFiltri Then
        'dcParstaf.DataSource = oParam.rFiltriArtico.Table
        'BindControls()
        'TranslateDefaultValues()

        grFiltri1.Enabled = False
        cmdLastfilter.Enabled = False
        cmdLock.Enabled = False
      Else
        lbUbicLike.Visible = False
        edUbicLike.Visible = False
        lbDescrLike.Visible = False
        edDescrLike.Visible = False
      End If

      If oParam.bVisGriglia Then CaricaClassificazioni()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipo documento, ecc ...
      '(Ex: CTRL+ALT+F4)
      'GctlTipoDoc = ""
      GctlSetRoules()
      GctlApplicaDefaultValue()

      'se è abilitata la multiselezione, visualizzo la colonna apposita (solo se zoom non su due livelli)
      If oParam.bTipoProposto = False And strOpz4 <> "S" And oParam.bLiv2 = False Then
        xx_seleziona.Visible = True
        grvZoom.Enabled = True
        For i = 0 To grvZoom.Columns.Count - 1
          If grvZoom.Columns(i).Name <> "xx_seleziona" Then CType(grvZoom.Columns(i), NTSGridColumn).Enabled = False
        Next
      Else
        xx_seleziona.Visible = False
      End If

      ckVisprezzi_CheckedChanged(ckVisprezzi, Nothing)
      ckFiltraConto_CheckedChanged(ckFiltraConto, Nothing)
      ckSuccedanei_CheckedChanged(ckSuccedanei, Nothing)

      '-------------------------------------------------
      'la condizione sotto si deve verificare solo quando ho abilitato l'opzione 'Zoom_articoli_2_livelli' 
      'ed in questo momento devo visualizzare il secondo livello
      If oParam.bLiv2 And oParam.bVisGriglia And strCodRoot <> "" Then
        strOpz4 = " "
        tsZoom.Enabled = False
      End If
      bLiv2 = oParam.bLiv2
      oParam.bLiv2 = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGHLAR_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Dim nValoreinInput As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim bRicercasuInput As Boolean = False
    Dim dtrTmp As DataRow

    Try
      '-------------------------------------------------
      'se richiesto dal child se la ricerca trova un solo risultato lo seleziona subito.
      'inoltre se zoom veloce deve essere passata per forza la descrizione articolo, quindi assegno il focus alla descr articolo ...
      bSelectIfOneRow = oParam.bFlag1

      '-------------------------------------------------
      'valore passato in input
      'se non sto utilizzando lo zoom veloce, devo mettere nell'apposita cella il valore passato in input
      If bSelectIfOneRow Then
        nValoreinInput = 3
        strTmp = oParam.strDescr    'valore passato in input senza virgola finale
      Else
        nValoreinInput = NTSCInt(oMenu.GetSettingBus("BSMGHLAR", "OPZIONI", ".", "ValoreinInput", "0", " ", "0"))
        strTmp = oParam.strIn       'valore passato in input
      End If

      If bLiv2 Then nValoreinInput = 1

      If strTmp <> "" Then
        Select Case nValoreinInput
          Case 0
            'non devo proporre nulla
          Case 1
            If oParam.strTipoArticolo <> "S" Then
              dtrT = dsFiltri.Tables("FILTRI1").Select("xx_nome = 'artico.ar_codart'")
              If dtrT.Length > 0 Then dtrT(0)!xx_valore = strTmp
            End If
          Case 2
            dtrT = dsFiltri.Tables("FILTRI1").Select("xx_nome = 'artico.ar_codalt'")
            If dtrT.Length > 0 Then dtrT(0)!xx_valore = strTmp
          Case 3
            dtrT = dsFiltri.Tables("FILTRI1").Select("xx_nome = 'artico.ar_descr'")
            If dtrT.Length > 0 Then dtrT(0)!xx_valore = strTmp
        End Select
      End If

      '---------------------------------------
      'focus iniziale: se zoom veloce sempre la descrizione
      grvFiltri1.FocusedRowHandle = 0
      If bSelectIfOneRow Then
        nFocusIniziale = 2
      Else
        nFocusIniziale = NTSCInt(oMenu.GetSettingBus("BSMGHLAR", "OPZIONI", ".", "FocusIniziale", "2", " ", "2"))
      End If
      Select Case nFocusIniziale
        Case 0 : strTmp = "artico.ar_codart"
        Case 1 : strTmp = "artico.ar_codalt"
        Case 2 : strTmp = "artico.ar_descr"
        Case 3 : strTmp = "artico.ar_desint"
      End Select
      For i = 0 To grvFiltri1.RowCount - 1
        If grvFiltri1.GetRowCellValue(i, "xx_nome").ToString = strTmp Then
          grvFiltri1.FocusedRowHandle = i
          Exit For
        End If
      Next
      grvFiltri1.FocusedColumn = xx_valore
      grFiltri1.Focus()

      nFocusInizialeDopoRicerca = NTSCInt(oMenu.GetSettingBus("BSMGHLAR", "OPZIONI", ".", "FocusInizialeDopoRicerca", "4", " ", "4"))

      If bSelectIfOneRow = True Or strCodRoot <> "" Then
        cmdRicerca_Click(Me, Nothing)
      Else
        If oMenu.GetSettingBus("BSMGHLAR", "OPZIONI", ".", "RicercaSuInput", "N", " ", "N") = "S" And oParam.strDescr <> "" Then
          bRicercasuInput = True
          cmdRicerca_Click(Me, Nothing)
        End If
      End If

      bSelectIfOneRow = False     'dopo la prima ricerca lanciata dal clienti con la ',' finale faccio in modo che il flag 'ottimistico' torni a funzionare

      oCleHlar.SetImagesDir()
      If oParam.bVisGriglia = False Then TabPage4.Visible = False
      '--------------------------------------------------------------------------------------------------------------
      '--- Utilizza il parametro oParam.strAlfpar per leggere il nome dell'eventuale programma chiamante
      '--------------------------------------------------------------------------------------------------------------
      strProgrChiamante = oParam.strAlfpar.ToUpper.Trim
      '--------------------------------------------------------------------------------------------------------------
      Select Case strProgrChiamante
        Case "BN__ISTF"
          grFiltri1.Visible = False
          dtrTmp = oParam.rFiltriArtico
          '----------------------------------------------------------------------------------------------------------
          bOnLoading = True
          '----------------------------------------------------------------------------------------------------------
          With dtrTmp
            cbAlistino.SelectedValue = IIf(NTSCStr(!pf_stalist) = " ", "T", NTSCStr(!pf_stalist)).ToString
            edTipo.Text = NTSCStr(!pf_2tipo)
            edCodartd.Text = NTSCStr(!pf_codartini)
            edCodarta.Text = NTSCStr(!pf_codartfin)
            edCodaltd.Text = NTSCStr(!pf_codaltini)
            edCodalta.Text = NTSCStr(!pf_codaltfin)
            edGruppod.Text = NTSCStr(!pf_gruppoini)
            edGruppoa.Text = NTSCStr(!pf_gruppofin)
            edFornd.Text = NTSCStr(!pf_fornini)
            edForna.Text = NTSCStr(!pf_fornfin)
            edFamprodd.Text = NTSCStr(!pf_famprodini)
            edFamproda.Text = NTSCStr(!pf_famprodfin)
            edClassificazioneLivello1.Text = NTSCStr(!pf_codcla1)
            edClassificazioneLivello2.Text = NTSCStr(!pf_codcla2)
            edClassificazioneLivello3.Text = NTSCStr(!pf_codcla3)
            edClassificazioneLivello4.Text = NTSCStr(!pf_codcla4)
            edClassificazioneLivello5.Text = NTSCStr(!pf_codcla5)
            strTmp = NTSCStr(!pf_filtro)
            If strTmp.Length > 0 Then
              If Microsoft.VisualBasic.Right(strTmp, 1) = "*" Then
                strTmp = Microsoft.VisualBasic.Mid(strTmp, 1, strTmp.Length - 1)
              End If
            End If
            edCodartLike.Text = strTmp
            edSottd.Text = NTSCStr(!pf_sottd)
            edSotta.Text = NTSCStr(!pf_sotta)
            edDescrd.Text = NTSCStr(!pf_descrd)
            edDescra.Text = NTSCStr(!pf_descra)
            edMarcad.Text = NTSCStr(!pf_marcad)
            edMarcaa.Text = NTSCStr(!pf_marcaa)
            edProvvd.Text = NTSCStr(!pf_claprovd)
            edProvva.Text = NTSCStr(!pf_claprova)
            edScontid.Text = NTSCStr(!pf_clascond)
            edScontia.Text = NTSCStr(!pf_clascona)
            strTmp = NTSCStr(!pf_filtrodb)
            If strTmp.Length > 0 Then
              If Microsoft.VisualBasic.Right(strTmp, 1) = "*" Then
                strTmp = Microsoft.VisualBasic.Mid(strTmp, 1, strTmp.Length - 1)
              End If
            End If
            edDBLike.Text = strTmp
            strTmp = NTSCStr(!pf_filtroubic)
            If strTmp.Length > 0 Then
              If Microsoft.VisualBasic.Right(strTmp, 1) = "*" Then
                strTmp = Microsoft.VisualBasic.Mid(strTmp, 1, strTmp.Length - 1)
              End If
            End If
            edUbicLike.Text = strTmp
            edDataUltagd.Text = NTSCStr(!pf_dataultagd)
            edDataUltaga.Text = NTSCStr(!pf_dataultaga)
            edApprovd.Text = NTSCStr(!pf_approvda)
            edApprova.Text = NTSCStr(!pf_approva)
            edCodIvad.Text = NTSCStr(!pf_codivad)
            edCodIvaa.Text = NTSCStr(!pf_codivaa)
            cbLotti.SelectedValue = IIf(NTSCStr(!pf_lotti) = " ", "T", NTSCStr(!pf_lotti)).ToString
            cbCommessa.SelectedValue = IIf(NTSCStr(!pf_commessa) = " ", "T", NTSCStr(!pf_commessa)).ToString
            cbInesaur.SelectedValue = IIf(NTSCStr(!pf_esaurito) = " ", "T", NTSCStr(!pf_esaurito)).ToString
            cbAvarianti.SelectedValue = IIf(NTSCStr(!pf_varianti) = " ", "T", NTSCStr(!pf_varianti)).ToString
            cbMatricole.SelectedValue = IIf(NTSCStr(!pf_matricole) = " ", "T", NTSCStr(!pf_matricole)).ToString
            edCodtipad.Text = NTSCStr(!pf_codtipad)
            edCodtipaa.Text = NTSCStr(!pf_codtipaa)
            edMagstockini.Text = NTSCStr(!pf_magstockini)
            edMagstockfin.Text = NTSCStr(!pf_magstockfin)
            edMagprodini.Text = NTSCStr(!pf_magprodini)
            edMagprodfin.Text = NTSCStr(!pf_magprodfin)
            cbUbicaz.SelectedValue = IIf(NTSCStr(!pf_gesubic) = " ", "T", NTSCStr(!pf_gesubic)).ToString
            cbAfasi.SelectedValue = IIf(NTSCStr(!pf_gesfasi) = " ", "T", NTSCStr(!pf_gesfasi)).ToString
            cbCritico.SelectedValue = IIf(NTSCStr(!pf_critico) = " ", "T", NTSCStr(!pf_critico)).ToString
            edAnnod.Text = NTSCStr(!pf_annod)
            edAnnoa.Text = NTSCStr(!pf_annoa)
            edCodstagd.Text = NTSCStr(!pf_codstagd)
            edCodstaga.Text = NTSCStr(!pf_codstaga)
            edCodtagld.Text = NTSCStr(!pf_codtagld)
            edCodtagla.Text = NTSCStr(!pf_codtagla)
          End With
          If oParam.strCin.Trim <> "" Then strFiltriDaEsterno = oParam.strCin
          '----------------------------------------------------------------------------------------------------------
          bOnLoading = False
          '----------------------------------------------------------------------------------------------------------
          cmdLock.Visible = False
          cmdLastfilter.Visible = False
          cmdProgressivi.Visible = False
          ckOttimistico.Visible = False
          lbBarcode.Visible = False : edBarcode.Visible = False
          lbCodarfo.Visible = False : edCodarfo.Visible = False
          lbTestdb.Visible = False : cbTestdb.Visible = False
          lbTestArt.Visible = False : cbTestArt.Visible = False
          lbMagaz.Visible = False : edMagaz.Visible = False
          ckBloccati.Visible = False
          lbTipologia.Visible = False : cbTipologia.Visible = False
          lbDescrLike.Visible = False : edDescrLike.Visible = False
        Case Else
          '----------------------------------------------------------------------------------------------------------
          lbCodartLike.Visible = False : edCodartLike.Visible = False
          lbDBLike.Visible = False : edDBLike.Visible = False
          '----------------------------------------------------------------------------------------------------------
          SettaFiltri(strProgrChiamante)
          '----------------------------------------------------------------------------------------------------------
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGHLAR_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Dim strTmp As String = ""
    Dim i As Integer = 0
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

      If e.KeyCode = Keys.Enter Then
        If Me.frmAuto.visible = False Then
          If grvZoom.Focused And grvZoom.RowCount > 0 Then
            e.Handled = True
            cmdSeleziona_Click(Me, Nothing)
          Else
            'If grFiltri1.ContainsFocus Then
            'cmdRicerca.Focus()      'mi sposto sul comando ricerca per validare il controllo su cui ero posizionato
            If oParam.bVisGriglia Then
              If Not Me.frmAuto.bSelezionato Then
                e.Handled = True
                cmdRicerca_Click(Me, Nothing)
              Else
                Me.frmAuto.bSelezionato = False
              End If
            End If
          End If
          'End If
        End If
      End If

      If (e.KeyValue > 40 And e.KeyValue < 112) And (e.KeyValue <> 91) And (e.KeyValue <> 93) _
           And e.Alt = False And e.Control = False And e.Shift = False And grvZoom.Focused = True Then
        grvFiltri1.FocusedRowHandle = 0
        Select Case nFocusIniziale
          Case 0 : strTmp = "artico.ar_codart"
          Case 1 : strTmp = "artico.ar_codalt"
          Case 2 : strTmp = "artico.ar_descr"
        End Select
        For i = 0 To grvFiltri1.RowCount - 1
          If grvFiltri1.GetRowCellValue(i, "xx_nome").ToString = strTmp Then
            grvFiltri1.FocusedRowHandle = i
            Exit For
          End If
        Next
        grvFiltri1.FocusedColumn = xx_valore
        grFiltri1.Focus()
        If e.KeyCode.ToString.Length > 1 Then
          Dim regex As New System.Text.RegularExpressions.Regex("\d+")
          Dim match As System.Text.RegularExpressions.Match = regex.Match(e.KeyCode.ToString)
          If match.Success Then NTSSendKeys.Send(0, match.Value)
        Else
          NTSSendKeys.Send(0, e.KeyCode.ToString)
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGHLAR_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    '-------------------------------------------------
    'salvo il recent
    Dim strTmp As String = ""
    Dim i As Integer = 0
    dsFiltri.Tables("FILTRI1").AcceptChanges()
    For i = 0 To 9
      strTmp += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & ";"
    Next
    strTmp = strTmp.Substring(0, strTmp.Length - 1)
    oMenu.SaveSettingBus("BNMGHLAR", "RECENT", ".", "Filtri1", strTmp, " ", "NS.", "NS.", "...")
    If oParam.bVisGriglia Then
      oMenu.SaveSettingBus("BSMGHLAR", "RECENT", ".", "ArticoliBloccati", IIf(ckBloccati.Checked, "*", "N").ToString, " ", "NS.", "NS.", "...")
      If strOpz4 <> "S" Then oMenu.SaveSettingBus("BSMGHLAR", "RECENT", ".", "ckCodiciRoot", IIf(ckCodiciRoot.Checked, "-1", "0").ToString, " ", "NS.", "NS.", "...")
    End If
    oMenu.SaveSettingBus("BNMGHLAR", "RECENT", ".", "Ottimistico", IIf(ckOttimistico.Checked, "-1", "0").ToString, " ", "NS.", "NS.", "...")
    If cbTipologia.SelectedValue IsNot Nothing Then oMenu.SaveSettingBus("BNMGHLAR", "RECENT", ".", "Tipologia", cbTipologia.SelectedValue, " ", "NS.", "NS.", "...")
  End Sub
#End Region

#Region "CommandButton"
  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Dim strQuery As String = ""
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strSelect As String = ""
    Dim strTmp As String = ""
    Dim strQuery2 As String = ""

    Try
      Me.ValidaLastControl()

      '-------------------------------------------
      'filtri si 'filtri 1': se il tipo non è compatibile con il campo (esempio stringa in campo numerico) do errore ed esco
      If Not CheckFiltri1() Then Return

      bEseguitaRicerca = True

      '------------------------------------------
      'ATTENZIONE: nel TAG di ogni controllo deve essere inserito l'operatore che deve essere utilizzato nella query 

      '------------------------------------------
      'campi da selezionare su artico
      For i = 0 To grvZoom.Columns.Count - 1
        If grvZoom.Columns(i).Visible Then strSelect += grvZoom.Columns(i).Name & ", "
      Next
      If strSelect.IndexOf("ar_codart") = -1 Then strSelect += "ar_codart, "
      If strSelect.IndexOf("xx_fase") = -1 Then strSelect += "xx_fase, "
      strSelect = strSelect.Substring(0, strSelect.Length - 2)

      bCr = False
      CreaQuery(strQuery) 'query

      '-----------------------------------------------
      'aggiungo i filtri impostati tramite le estensioni anagrafiche
      If strEstensioni <> "" Then
        strQuery += IIf(strQuery.Trim <> "", "§", "").ToString & strEstensioni
      End If

      If oParam.bVisGriglia = True Then

        Me.Cursor = Cursors.WaitCursor

        If ckSuccedanei.Checked = False Then
          strTmp = "N"
        Else
          If optAccessori.Checked Then
            strTmp = "A"
          Else
            strTmp = "S"
          End If
        End If

        oCleHlar.Ricerca(dsHlar, DittaCorrente, strSelect, strQuery, strOpz4, _
                        CampoTesto(edBarcode.Text.Trim, True), _
                        CampoTesto(edCodarfo.Text.Trim, True), _
                        NTSCInt(edConto.Text), ckFiltraMovmag.Checked, NTSCInt(edMagaz.Text), _
                        strTmp, CampoTesto(edCodartAcc.Text.Trim, False), _
                        ckVisprezzi.Checked, NTSCInt(edListino.Text), edDtvalid.Text, ckAbituali.Checked, ckCodiciRoot.Checked)
        dcHlar.DataSource = dsHlar.Tables("ARTICO")
        dsHlar.AcceptChanges()
        grZoom.DataSource = dcHlar
        '------------------------------------------------------------------------------------------------------------
        oMenu.SaveSettingBus("BNMGHLAR", "RECENT", ".", "ClientiFornitoriAbituali", IIf(ckAbituali.Checked = True, "-1", "0").ToString, " ", "NS.", "...", "...")
        '------------------------------------------------------------------------------------------------------------
        'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valore
        If grvZoom.RowCount > 0 Then
          bNessunRisultato = False
          Application.DoEvents() 'senza questa riga quando premo invio per far partire la ricerca dalla griglia filtri il focus rimane nella griglia filtri, non passa alla griglia articoli ...
          grvZoom.Focus()
          grvZoom.NTSMoveFirstRowColunn()
          If xx_seleziona.Visible Then grvZoom.FocusedColumn = xx_seleziona
          '---------------------------------------------------
          'faccio in modo che la pressione dell'enter non scateni l'emulazione del tasto TAB
          'Me.NTSKeyDownEnterNotEmulateTabNow()
          'SendKeys.Send("+{TAB}")
        Else
          bNessunRisultato = True
          If Not sender Is Nothing Then oApp.MsgBoxInfo(oApp.Tr(Me, 127791222142968750, "La ricerca non ha restituito nessun risultato"))
          If cmdRicerca.Focused Or grvFiltri1.Focused Then
            NTSLastControlFocussed.Focus()
          End If
        End If
        grZoom_Enter(grZoom, Nothing)
        'se è impostato di selezionare subito l'unica riga restituita lo faccio
        If bSelectIfOneRow And grvZoom.RowCount = 1 Then
          oParam.nTipologia = NTSCInt(cbTipologia.SelectedValue)
          oParam.strOut = grvZoom.GetFocusedRowCellDisplayText("ar_codart")
          Me.Cursor = Cursors.Default
          Me.Close()
        Else
          If grvZoom.RowCount > 0 Then
            grZoom.Focus()
            If xx_seleziona.Visible Then grvZoom.FocusedColumn = xx_seleziona
          End If
        End If

        '-------------------------
        'focus iniziale dopo la prima ricerca
        If strCodRoot = "" And nFocusInizialeDopoRicerca > -1 And nFocusInizialeDopoRicerca < 5 Then
          Select Case nFocusInizialeDopoRicerca
            Case 0 : strTmp = "artico.ar_codart"
            Case 1 : strTmp = "artico.ar_codalt"
            Case 2 : strTmp = "artico.ar_descr"
            Case 3 : strTmp = "artico.ar_desint"
            Case 4 : If grvZoom.RowCount = 0 Then strTmp = "artico.ar_descr" Else strTmp = ""
          End Select
          For i = 0 To grvFiltri1.RowCount - 1
            If grvFiltri1.GetRowCellValue(i, "xx_nome").ToString = strTmp Then
              If bNessunRisultato = False Then grvFiltri1.FocusedRowHandle = i
              Exit For
            End If
          Next
          If strTmp <> "" Then
            grvFiltri1.FocusedColumn = xx_valore
            grFiltri1.Focus()
          End If
        End If

        Me.Cursor = Cursors.Default
      Else
        '---------------------------------------------
        'restituisco solo la stringa per la query
        If strQuery = "" Then strQuery = "ar_codart like '%'"
        If Not bReimpostazioneFiltri Then
          '---------------------------------------
          'salvo i filtri in un recent per poterli ricaricare alla prossima chiamata dello zoom
          strTmp = SalvaImpostazioni(Me)
          If strTmp.Length > 0 Then strTmp = strTmp.Substring(0, strTmp.Length - 1)
          oMenu.SaveSettingBus("BNMGHLAR", "RECENT", ".", "LASTFILTER", strTmp, " ", "NS.", "...", "...")
        End If
        oParam.strOut = strQuery

        bCr = True
        CreaQuery(strQuery2) 'crystal
        bCr = False
        oParam.strBanc1 = strQuery2

        Me.Close()
      End If    'If oParam.bVisGriglia = True Then
      '--------------------------------------------------------------------------------------------------------------
      VisualizzaImmagine()
      '--------------------------------------------------------------------------------------------------------------
      '--- Eventuale DataTable contenente i filtri impostati nella modale delle Estensioni
      '--------------------------------------------------------------------------------------------------------------
      Select Case strProgrChiamante
        Case "BN__ISTF"
          With oParam.rFiltriArtico
            !pf_stalist = IIf(cbAlistino.SelectedValue = "T", " ", cbAlistino.SelectedValue).ToString
            !pf_2tipo = edTipo.Text
            !pf_codartini = edCodartd.Text
            !pf_codartfin = edCodarta.Text
            !pf_codaltini = edCodaltd.Text
            !pf_codaltfin = edCodalta.Text()
            !pf_gruppoini = edGruppod.Text
            !pf_gruppofin = edGruppoa.Text
            !pf_fornini = edFornd.Text
            !pf_fornfin = edForna.Text
            !pf_famprodini = edFamprodd.Text
            !pf_famprodfin = edFamproda.Text
            !pf_codcla1 = edClassificazioneLivello1.Text
            !pf_codcla2 = edClassificazioneLivello2.Text
            !pf_codcla3 = edClassificazioneLivello3.Text
            !pf_codcla4 = edClassificazioneLivello4.Text
            !pf_codcla5 = edClassificazioneLivello5.Text
            !pf_filtro = edCodartLike.Text
            !pf_sottd = edSottd.Text
            !pf_sotta = edSotta.Text
            !pf_descrd = edDescrd.Text
            !pf_descra = edDescra.Text
            !pf_marcad = edMarcad.Text
            !pf_marcaa = edMarcaa.Text
            !pf_claprovd = edProvvd.Text
            !pf_claprova = edProvva.Text
            !pf_clascond = edScontid.Text
            !pf_clascona = edScontia.Text
            !pf_filtrodb = edDBLike.Text
            !pf_filtroubic = edUbicLike.Text
            !pf_dataultagd = edDataUltagd.Text
            !pf_dataultaga = edDataUltaga.Text
            !pf_approvda = edApprovd.Text
            !pf_approva = edApprova.Text
            !pf_codivad = edCodIvad.Text
            !pf_codivaa = edCodIvaa.Text
            !pf_lotti = IIf(cbLotti.SelectedValue = "T", " ", cbLotti.SelectedValue).ToString
            !pf_commessa = IIf(cbCommessa.SelectedValue = "T", " ", cbCommessa.SelectedValue).ToString
            !pf_esaurito = IIf(cbInesaur.SelectedValue = "T", " ", cbInesaur.SelectedValue).ToString
            !pf_varianti = IIf(cbAvarianti.SelectedValue = "T", " ", cbAvarianti.SelectedValue).ToString
            !pf_matricole = IIf(cbMatricole.SelectedValue = "T", " ", cbMatricole.SelectedValue).ToString
            !pf_codtipad = edCodtipad.Text
            !pf_codtipaa = edCodtipaa.Text
            !pf_magstockini = edMagstockini.Text
            !pf_magstockfin = edMagstockfin.Text
            !pf_magprodini = edMagprodini.Text
            !pf_magprodfin = edMagprodfin.Text
            !pf_gesubic = IIf(cbUbicaz.SelectedValue = "T", " ", cbUbicaz.SelectedValue).ToString
            !pf_gesfasi = IIf(cbAfasi.SelectedValue = "T", " ", cbAfasi.SelectedValue).ToString
            !pf_critico = IIf(cbCritico.SelectedValue = "T", " ", cbCritico.SelectedValue).ToString
            !pf_annod = edAnnod.Text
            !pf_annoa = edAnnoa.Text
            !pf_codstagd = edCodstagd.Text
            !pf_codstaga = edCodstaga.Text
            !pf_codtagld = edCodtagld.Text
            !pf_codtagla = edCodtagla.Text
          End With
          If Not dttFiltriAnex Is Nothing Then oParam.oParam = dttFiltriAnex
      End Select
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      bCr = False
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oParam.strOut = ""
    oParam.CANCELZOOM = True
    Me.Close()
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case strProgrChiamante
        Case "BNMGARTI", "BNMGARTI_F5"
          strTmp = SalvaImpostazioni(Me)
          If strTmp.Length > 0 Then strTmp = strTmp.Substring(0, strTmp.Length - 1)
          oMenu.SaveSettingBus("BNMGHLAR", "RECENT", ".", "FiltriInApertura", strTmp, " ", ".S.", ".S.", ".N.")
          strTmp = ""
          If dsHlar.Tables.Count <> 0 Then
            If Not grvZoom.NTSGetCurrentDataRow() Is Nothing Then
              strTmp = grvZoom.GetFocusedRowCellDisplayText("ar_codart")
            End If
          End If
          oMenu.SaveSettingBus("BNMGHLAR", "RECENT", ".", "ArticoloInApertura", strTmp, " ", ".S.", ".S.", ".N.")
      End Select
      '--------------------------------------------------------------------------------------------------------------
      If dsHlar.Tables.Count = 0 Then Return

      If Not grvZoom.NTSGetCurrentDataRow() Is Nothing Then
        '----------------------------
        'se ZOOM a 2 livelli, devo far selezionare la variante specifica
        If strOpz4 = "S" And NTSCStr(grvZoom.NTSGetCurrentDataRow()!ar_gesvar) = "S" Then
          Dim oPar As New CLE__PATB
          oPar.bLiv2 = True
          oPar.strDescr = grvZoom.GetFocusedRowCellDisplayText("ar_codart")
          oPar.bVisGriglia = True
          oPar.nMagaz = NTSCInt(edMagaz.Text)
          oPar.nListino = NTSCInt(edListino.Text)
          oPar.lContoCF = NTSCInt(edConto.Text)
          oPar.nTipologia = NTSCInt(cbTipologia.SelectedValue)
          oPar.strTipoArticolo = oParam.strTipoArticolo
          oPar.strTipoBil = IIf(ckVisprezzi.Checked, "-1", "0").ToString
          NTSZOOM.strIn = grvZoom.GetFocusedRowCellDisplayText("ar_codart")
          NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
          If NTSZOOM.strIn <> grvZoom.GetFocusedRowCellDisplayText("ar_codart") Then
            oParam.strOut = NTSZOOM.strIn
          Else
            'non ho selezionato nulla: devo rimanere nello zoom
            Return
          End If
        Else
          If xx_seleziona.Visible And oParam.bTipoProposto = False And strOpz4 <> "S" And oParam.bLiv2 = False Then
            'selezione di più articoli 
            Dim dttOut As New DataTable
            dttOut.Columns.Add("codart", GetType(String))
            grvZoom.NTSMoveNextColunn()

            If dsHlar.Tables("ARTICO").Select("xx_seleziona = 'S'").Length = 0 And dsHlar.Tables("ARTICO").Rows.Count <> 0 Then
              'se dopo aver premuto invio per eseguire la ricerca sono sulla griglia e ripremo invio ...
              grvZoom.NTSGetCurrentDataRow!xx_seleziona = "S"
            End If
            dsHlar.AcceptChanges()
            dtrT = dsHlar.Tables("ARTICO").Select("xx_seleziona = 'S'")
            For i = 0 To dtrT.Length - 1
              dttOut.Rows.Add(New Object() {dtrT(i)!ar_codart.ToString})
            Next
            dttOut.AcceptChanges()
            oParam.strOut = "*"
            oParam.oParam = dttOut
          Else
            'ritorno un articolo solo
            oParam.strOut = grvZoom.GetFocusedRowCellDisplayText("ar_codart")
          End If
        End If
        oParam.nTipologia = NTSCInt(cbTipologia.SelectedValue)
      End If

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdGestione_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGestione.Click
    Dim strParam As String = "APRI;"
    Dim strServer As String = ""
    Dim strNomeDll As String = ""
    Dim strPrgGest As String = ""
    Dim strProgZoom As String = ""
    Dim oParam As New CLE__CLDP
    Dim strGesvar As String = "N"
    Dim strOldCodart As String = ""
    Dim i As Integer = 0
    Dim nTaglia As Integer = 0

    Try
      If grvZoom.RowCount > 0 Then
        strParam += NTSCStr(grvZoom.NTSGetCurrentDataRow!ar_codart)
        strGesvar = NTSCStr(grvZoom.NTSGetCurrentDataRow!ar_gesvar)
        strOldCodart = NTSCStr(grvZoom.NTSGetCurrentDataRow!ar_codart)
        nTaglia = NTSCInt(grvZoom.NTSGetCurrentDataRow!ar_codtagl)
      Else
        Select Case NTSCStr(oMenu.GetSettingBus("BSMGHLAR", "OPZIONI", ".", "Child_Nuovo_Articolo", "N", " ", "N")).Trim
          Case "V"
            'articolo a varianti
            strGesvar = "S"
            nTaglia = 0
          Case "T"
            'articolo taglie e colori
            strGesvar = "S"
            nTaglia = 1
          Case Else
            'articolo normale
            strGesvar = "N"
        End Select
      End If

      '---------------------------------
      'ottengo il nome del programma per la gestione
      If strGesvar = "S" Then
        If nTaglia = 0 Then
          strPrgGest = NTSZOOM.GetNomeProgForGest("ZOOMARTICOV")
        Else
          strPrgGest = NTSZOOM.GetNomeProgForGest("ZOOMARTICOTC")
        End If
      Else
        strPrgGest = NTSZOOM.GetNomeProgForGest("ZOOMARTICO")
      End If

      If strPrgGest = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 127791222145156250, "Funzione GESTIONE non abilitata per il programma '|" & strProgZoom & "|'"))
        Return
      End If

      'verifico se programma child è COM o NET
      If strPrgGest.Substring(0, 3).ToUpper <> "CLS" Then
        'Child NET
        strNomeDll = strPrgGest
        strPrgGest = "FRM" & strPrgGest.Substring(2)
        strServer = "NTSInformatica"

        oMenu.RunChild(strServer, strPrgGest, "", DittaCorrente, "", strNomeDll, Nothing, strParam, True, True)
      Else
        strServer = strPrgGest.Replace("CLS", "BS")
        oMenu.RunChild(strServer, strPrgGest, "", DittaCorrente, "", "", Nothing, strParam, True, True)
      End If
      If bEseguitaRicerca And grZoom.Visible Then
        cmdRicerca_Click(cmdRicerca, Nothing)
        'se posso mi riposiziono sull'articolo su cui ero
        If strOldCodart <> "" Then
          i = dcHlar.Find("ar_codart", strOldCodart)
          If i > -1 Then dcHlar.Position = i
        End If
      End If
      grZoom.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLock.Click
    Try
      xx_nome.Enabled = Not xx_nome.Enabled
      grvFiltri1.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdOrdini_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOrdini.Click
    Dim oPar As New CLE__PATB
    'Dim strParam As String = ""

    Try
      'If grvZoom.RowCount = 0 Then Return

      'strParam = "".PadLeft(9, CChar("0")) & ";" & _
      '           "".PadLeft(9, CChar("9")) & ";" & _
      '           NTSCStr(grvZoom.NTSGetCurrentDataRow!ar_codart).PadLeft(18) & ";" & _
      '           NTSCStr(grvZoom.NTSGetCurrentDataRow!ar_codart).PadLeft(18) & ";" & _
      '           "§;" & _
      '           "1;" & _
      '           "01/01/1900;" & _
      '           "31/12/2099;"

      'oMenu.RunChild("BSORSCHO", "CLSORSCHO", "", DittaCorrente, "", "", Nothing, strParam, True, True)
      'grZoom.Focus()

      oPar.strDescr = oCleHlar.GetWhereHlmo(NTSCInt(edMagaz.Text))
      oPar.strTipo = " "
      oPar.lContoCF = 0
      oPar.strCodart = ""
      oPar.nFase = 0
      If Not grvZoom.NTSGetCurrentDataRow Is Nothing Then
        oPar.strCodart = grvZoom.NTSGetCurrentDataRow!ar_codart.ToString
        oPar.nFase = NTSCInt(grvZoom.NTSGetCurrentDataRow!xx_fase)
      End If
      oPar.dImporto = 0
      oPar.nTipologia = 0                     '0 solo visualizzaz, 2 = possibilità di selez le righe
      oPar.oParam = Nothing                   'se chiamato da veboll qui occorrerà passare il datatable del corpo (oPar.oParam = oCleVeboll.dttEC)
      oPar.nMastro = NTSCInt(IIf(NTSCInt(edMagaz.Text) = 0, 2, 10))    'colonne di bsorhlmo da visualizzare (in vb6 lShowColumn)
      NTSZOOM.ZoomStrIn("ZOOMMOVORD", DittaCorrente, oPar)        'in vb6 la dohlmo
      grZoom.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdMovimenti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMovimenti.Click
    Dim strParam As String = ""
    Try
      If grvZoom.RowCount = 0 Then Return
      strParam = "APRI;" & NTSCStr(grvZoom.NTSGetCurrentDataRow!ar_codart).PadLeft(CLN__STD.CodartMaxLen) & ";0000;" & "".PadLeft(9, CChar("0")) & ";A"
      oMenu.RunChild("BSMGSCHE", "CLSMGSCHE", "", DittaCorrente, "", "", Nothing, strParam, True, True)
      grZoom.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdListini_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdListini.Click
    Dim oPar As CLE__CLDP = Nothing
    Try
      If grvZoom.RowCount = 0 Then Return

      'frmList.Init(oMenu, Nothing, DittaCorrente, Nothing)
      'frmList.InitEntity(oCleHlar)
      'frmList.strCodart = NTSCStr(grvZoom.NTSGetCurrentDataRow!ar_codart)
      'frmList.nFase = NTSCInt(grvZoom.NTSGetCurrentDataRow!xx_fase)
      'frmList.ShowDialog()
      'grZoom.Focus()

      '----------------------
      oPar = New CLE__CLDP
      oPar.Ditta = DittaCorrente
      oPar.strNomProg = "BNORGSOR"
      oPar.strPar1 = NTSCStr(grvZoom.NTSGetCurrentDataRow!ar_codart)
      oPar.strPar2 = DateTime.Now.ToShortDateString
      oPar.dPar1 = NTSCInt(grvZoom.NTSGetCurrentDataRow!xx_fase)
      oPar.dPar3 = NTSCInt(edConto.Text)
      oPar.dPar4 = 0        'ritorna il prezzo
      oPar.dPar5 = 0        'ritorna la valuta

      oMenu.RunChild("NTSInformatica", "FRMMGLIST", "", DittaCorrente, "", "BNMGDOCU", oPar, "", True, True)
      grZoom.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdProgressivi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProgressivi.Click
    Dim oPar As New CLE__CLDP
    Try
      If grvZoom.NTSGetCurrentDataRow Is Nothing Then Return

      oPar.strPar1 = "BNMGHLAR"
      oPar.strPar2 = NTSCStr(grvZoom.NTSGetCurrentDataRow!ar_codart)
      oPar.dPar1 = NTSCInt(grvZoom.NTSGetCurrentDataRow!xx_fase)

      oMenu.RunChild("NTSInformatica", "FRMMGHLAP", "", DittaCorrente, "", "BNMGARMD", oPar, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
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
      strValue = oMenu.GetSettingBus("BNMGHLAR", "RECENT", ".", "LASTFILTER", "", " ", "")
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
              strT1 = strT1(1).Split("§"c)
              For l = 0 To CType(ctrlT, NTSGrid).Views(0).RowCount - 1
                CType(CType(ctrlT, NTSGrid).Views(0), NTSGridView).SetRowCellValue(l, "xx_valore", strT1(l))
              Next
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

  Public Overridable Sub cmdEstensioni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEstensioni.Click
    Dim frmHlex As FRM__HLEX = Nothing
    Try
      frmHlex = CType(NTSNewFormModal("FRM__HLEX"), FRM__HLEX)
      '---------------------------------------------------
      'chiamo ma form per le estensioni anagrafiche:
      'di ritorno, se ho impostato alcuni filtri, mi ritrovo la 
      'oParam.bPar1 = True e la stringa con i filtri in oParam.strPar1

      Dim oParam As New CLE__CLDP
      oParam.strPar1 = "A"
      frmHlex.Init(oMenu, oParam, DittaCorrente)
      frmHlex.InitEntity(oCleHlar)
      frmHlex.bOttimistico = ckOttimistico.Checked
      frmHlex.strProgrChiamante = strProgrChiamante
      frmHlex.strFiltriDaEsterno = strFiltriDaEsterno
      frmHlex.ShowDialog()
      If oParam.bPar1 = True Then
        strEstensioni = oParam.strPar1.Trim
      Else
        strEstensioni = ""
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
#End Region

#Region "Eventi griglia"
  Public Overridable Sub grZoom_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grZoom.Enter
    Try
      If grvZoom.RowCount > 0 Then
        GctlSetVisEnab(cmdOrdini, False)
        GctlSetVisEnab(cmdMovimenti, False)
        GctlSetVisEnab(cmdListini, False)
        GctlSetVisEnab(cmdProgressivi, False)
      Else
        cmdOrdini.Enabled = False
        cmdMovimenti.Enabled = False
        cmdListini.Enabled = False
        cmdProgressivi.Enabled = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grZoom_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grZoom.MouseDoubleClick
    Try
      If Not grvZoom.NTSGetCurrentDataRow() Is Nothing And xx_seleziona.Visible And grvZoom.NTSGetCurrentDataRow.Table.Columns.Contains("xx_seleziona") Then
        grvZoom.NTSGetCurrentDataRow!xx_seleziona = "S"
      End If
      cmdSeleziona_Click(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grvZoom_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvZoom.NTSFocusedRowChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      VisualizzaImmagine()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "TabPage"
  Public Overridable Sub tsZoom_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsZoom.SelectedPageChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      VisualizzaImmagine()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB
    Dim strTmp As String = ""
    Try
      'entro qui perchè nella FRMMGHLAR_KeyDown ho inserito il seguente codice:
      'If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
      '  Zoom()
      '  e.Handled = True
      'End If

      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return

      If grFiltri1.ContainsFocus Then
        '------------------------------------
        'zoom su filtri1 di griglia
        If NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_nome) = "." Then Return 'sono su una colonna N.A.
        strTmp = NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_valore)
        ApriZoomTabella(strTmp, NTSCStr(grvFiltri1.NTSGetCurrentDataRow!xx_nome))
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

  Public Overridable Sub cbTipologia_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipologia.SelectedIndexChanged
    Try
      If grvZoom.RowCount > 0 Then
        dsHlar.Tables("ARTICO").Clear()
      End If
      If oParam.bVisGriglia Then
        If NTSCInt(cbTipologia.SelectedValue) = 0 Then
          edCodtipad.Text = "0"
          edCodtipaa.Text = "".PadLeft(3, "9"c)
        Else
          edCodtipad.Text = cbTipologia.SelectedValue
          edCodtipaa.Text = cbTipologia.SelectedValue
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckSuccedanei_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSuccedanei.CheckedChanged
    Try
      If ckSuccedanei.Checked Then
        GctlSetVisEnab(optAccessori, False)
        GctlSetVisEnab(optSuccedanei, False)
        GctlSetVisEnab(edCodartAcc, False)
      Else
        optAccessori.Enabled = False
        optSuccedanei.Enabled = False
        edCodartAcc.Text = ""
        edCodartAcc.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckFiltraConto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckFiltraConto.CheckedChanged
    Try
      If ckFiltraConto.Checked Then
        GctlSetVisEnab(edConto, False)
        GctlSetVisEnab(ckFiltraMovmag, False)
      Else
        edConto.Text = "0"
        edConto.Enabled = False
        ckFiltraMovmag.Checked = False
        ckFiltraMovmag.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ckVisprezzi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckVisprezzi.CheckedChanged
    Try
      If ckVisprezzi.Checked Then
        GctlSetVisEnab(edListino, False)
        GctlSetVisEnab(edDtvalid, False)
        GctlSetVisEnab(xx_prezzo, True)
      Else
        edListino.Text = "0"
        edListino.Enabled = False
        edDtvalid.Enabled = False
        xx_prezzo.Visible = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub ApriZoomTabella(ByRef strIn As String, ByVal strCampo As String)
    'per eventuali altri controlli caricati al volo
    Dim ctrlTmp As Control = NTSFindControlForZoom()
    If ctrlTmp Is Nothing Then Return
    Dim oParam As New CLE__PATB
    Dim strNomeZoom As String = ""
    Try

      strNomeZoom = CType(oMenu.oCleComm, CLELBMENU).TrovaNomeZoomHlvl(strCampo)
      If strNomeZoom = "" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128586809070468750, "Zoom per campo |'" & strCampo & "'| non trovato (TrovaNomeZoomHlvl)"))
        Return
      End If

      If strNomeZoom = "ZOOMHLVL" Then
        oParam.strTipo = "ARTICO"
        oParam.strDescr = strCampo
        NTSZOOM.strIn = NTSCStr(grvFiltri1.EditingValue)
        NTSZOOM.ZoomStrIn("ZOOMHLVL", DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvFiltri1.EditingValue) Then grvFiltri1.SetFocusedValue(NTSZOOM.strIn)

      Else

        SetFastZoom(NTSCStr(grvFiltri1.EditingValue), oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = NTSCStr(grvFiltri1.EditingValue)
        NTSZOOM.ZoomStrIn(strNomeZoom, DittaCorrente, oParam)
        If NTSZOOM.strIn <> NTSCStr(grvFiltri1.EditingValue) Then grvFiltri1.SetFocusedValue(NTSZOOM.strIn)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub



  Public Overridable Function CheckFiltri1() As Boolean
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
        If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome) = "." Then
          dsFiltri.Tables("FILTRI1").Rows(i)!xx_valore = ""
        Else
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valore) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valore)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128837522006998845, "Nel filtro '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valore)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128492077570882500, "Nel filtro '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If
        End If
      Next
      dsFiltri.Tables("FILTRI1").AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

#Region "Proposta A"
  Public Overridable Sub edCodartd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodartd.Leave
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      If edCodartd.Text.Trim <> "" Then
        edCodartd.Text = edCodartd.Text.ToUpper
        edCodarta.Text = edCodartd.Text
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodarta_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodarta.Leave
    Try
      If edCodarta.Text.Trim <> edCodarta.Text.Trim.ToUpper Then edCodarta.Text = edCodarta.Text.ToUpper
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodaltd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodaltd.Leave
    Try
      If edCodaltd.Text.Trim <> "" Then
        edCodaltd.Text = edCodaltd.Text.ToUpper
        edCodalta.Text = edCodaltd.Text
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edFornd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edFornd.Leave
    Try
      If NTSCInt(edFornd.Text) <> 0 Then edForna.Text = edFornd.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edDescrd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDescrd.Leave
    Try
      If edDescrd.Text.Trim() <> "" Then edDescra.Text = edDescrd.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edGruppod_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edGruppod.Leave
    Try
      If edGruppod.Text.Trim <> "0" Then edGruppoa.Text = edGruppod.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edSottd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSottd.Leave
    Try
      If edSottd.Text.Trim <> "0" Then edSotta.Text = edSottd.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edFamprodd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edFamprodd.Leave
    Try
      If edFamprodd.Text.Trim <> "" Then edFamproda.Text = edFamprodd.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodtipad_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodtipad.Leave
    Try
      If edCodtipad.Text.Trim <> "0" Then edCodtipaa.Text = edCodtipad.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edMarcad_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edMarcad.Leave
    Try
      If edMarcad.Text.Trim <> "0" Then edMarcaa.Text = edMarcad.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edProvvd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edProvvd.Leave
    Try
      If edProvvd.Text.Trim <> "0" Then edProvva.Text = edProvvd.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edScontid_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edScontid.Leave
    Try
      If edScontid.Text.Trim <> "0" Then edScontia.Text = edScontid.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edApprovd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edApprovd.Leave
    Try
      If edApprovd.Text.Trim <> "0" Then edApprova.Text = edApprovd.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edCodIvad_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCodIvad.Leave
    Try
      If edCodIvad.Text.Trim <> "0" Then edCodIvaa.Text = edCodIvad.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edDataUltagd_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDataUltagd.Leave
    Try
      If NTSCDate(edDataUltagd.Text) <> New Date(1900, 1, 1) Then edDataUltaga.Text = edDataUltagd.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edMagstockini_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edMagstockini.Leave
    Try
      If edMagstockini.Text.Trim <> "0" Then edMagstockfin.Text = edMagstockini.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edMagprodini_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edMagprodini.Leave
    Try
      If edMagprodini.Text.Trim <> "0" Then edMagprodfin.Text = edMagprodini.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

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
          If ctrlT.Name <> "grZoom" Then
            strValue += ctrlT.Name & "="
            For i = 0 To CType(ctrlT, NTSGrid).Views(0).RowCount - 1
              strValue += CType(CType(ctrlT, NTSGrid).Views(0), NTSGridView).GetRowCellDisplayText(i, "xx_valore") & "§"
            Next
            strValue = strValue.Substring(0, strValue.Length - 1) & "|"
          End If
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

  Public Overridable Sub VisualizzaImmagine()
    Dim strGif1 As String = ""
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      imArtGif.Visible = False
      '--------------------------------------------------------------------------------------------------------------
      If TabPage4.Visible = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If Not tsZoom.SelectedTabPage.Equals(tsZoom.TabPages(3)) Then Return
      '--------------------------------------------------------------------------------------------------------------
      If dsHlar.Tables("ARTICO") Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If dsHlar.Tables("ARTICO").Rows.Count = 0 Then Return
      '--------------------------------------------------------------------------------------------------------------
      If grvZoom.GetFocusedRowCellValue(ar_codart).ToString.Trim = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.ValCodiceDb(grvZoom.GetFocusedRowCellValue(ar_codart).ToString, DittaCorrente, "ARTICO", "S", "", dttTmp) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      strGif1 = NTSCStr(dttTmp.Rows(0)!ar_gif1)
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      If strGif1.Trim = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non esiste il file indicato in ARTICO.ar_gif1, nella cartella delle immagini, esce
      '--------------------------------------------------------------------------------------------------------------
      If File.Exists(oCleHlar.strImageDir & "\" & strGif1) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      fs = New System.IO.FileStream(oCleHlar.strImageDir & "\" & strGif1, IO.FileMode.Open, IO.FileAccess.Read)
      imArtGif.Image = System.Drawing.Image.FromStream(fs)
      imArtGif.Visible = True
      fs.Close()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttTmp.Clear()
      dttTmp.Dispose()
    End Try
  End Sub

  Public Overridable Sub SettaFiltri(ByVal strProgrChiamante As String)
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim strValue As String = ""
    Dim ctrlT As Object = Nothing
    Dim strT() As String = Nothing
    Dim strT1() As String = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      If strProgrChiamante.ToUpper <> "BNMGARTI" Then Return
      '--------------------------------------------------------------------------------------------------------------
      strValue = oMenu.GetSettingBus("BNMGHLAR", "RECENT", ".", "FiltriInApertura", "", " ", "")
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
              CType(ctrlT, NTSListBox).SelectedIndex = -1 'pulisco gli elementi selezionati
              strT1 = strT1(1).Split(","c)
              For l = 0 To strT1.Length - 1
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
      strValue = oMenu.GetSettingBus("BNMGHLAR", "RECENT", ".", "ArticoloInApertura", "", " ", "")
      If strValue = "" Then Return
      '--------------------------------------------------------------------------------------------------------------
      If dsHlar.Tables("ARTICO").Rows.Count = 0 Then Return
      '--------------------------------------------------------------------------------------------------------------
      grZoom.Focus()
      For i = 0 To (dcHlar.List.Count - 1)
        If NTSCStr(CType(dcHlar.Item(i), DataRowView)!ar_codart) = strValue Then
          dcHlar.Position = i
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

  Public Overridable Function CreaQuery(ByRef strQuery As String) As Boolean
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Dim strTmp As String = ""
    Try
      'bCr True mette le {artico.} poi formatta il valore per Crystal Report per usarla deve essere presente artico in join nel report
      'bCr False come sempre per query
      '------------------------------------------
      'griglia filtri1
      For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
        If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome).Trim <> "." And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valore).Trim <> "" Then
          dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
          '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
          Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
            Case 3, 4, 5, 6, 7
              strQuery += SetPA(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString, "") & " = " & FormatRptSql(NTSCStr(NTSCDec(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valore)), "N") & "§"
            Case 8
              strQuery += SetPA(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString, "") & " = " & FormatRptSql(NTSCDate(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valore).ToShortDateString, "D") & "§"
            Case Else
              If bCr Then
                strTmp = CampoTestoRpt(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valore), True)
              Else
                strTmp = CampoTesto(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valore), True)
              End If
              'SOLO SE OPZIONE ATTIVA E NON PER CRYSTAL REPORTS
              If bCercaInDescr2 And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome) = "artico.ar_descr" And Not bCr Then
                strQuery += SetPA("artico.ar_descr + CASE WHEN artico.ar_desint IS NULL THEN '' ELSE artico.ar_desint END", "") & " like " & strTmp & "§"
              Else
                strQuery += SetPA(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString, "") & " like " & strTmp & "§"
              End If
          End Select
        End If
      Next    'For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1

      '------------------------------------------
      'filtri artico primo tab
      If dttTipa.Rows.Count > 1 Then
        If oParam.bVisGriglia Then
          If NTSCInt(cbTipologia.SelectedValue) > 0 Then
            strQuery += SetPA("ar_codtipa") & " = " & FormatRptSql(cbTipologia.SelectedValue) & "§"
          End If
        End If
      End If

      Select Case cbTestdb.SelectedValue
        Case "S"
          If bCr Then
            strQuery += "not IsNull(" & SetPA("ar_coddb") & ")§"
          Else
            strQuery += "not " & SetPA("ar_coddb") & " is null§"
          End If
        Case "N" : strQuery += SetPA("ar_codart") & " = " & SetPA("ar_coddb") & "§"
      End Select
      If ckBloccati.Checked = False Then strQuery += SetPA("ar_blocco") & " = " & FormatRptSql("N") & "§"
      If edTipo.Text <> "" Then strQuery += SetPA("ar_tipo") & " = " & FormatRptSql(edTipo.Text) & "§"

      If oParam.strCodPdc = "CP2" And bProponiTipoArticolo Then
        'solo articoli padri di configuratore di prodotto 2
        strQuery += SetPA("ar_tipoopz") & " = " & FormatRptSql("Q") & "§"
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiamato da Desktop Consolle, considera, tra i filtri, gli eventuali valori
      '--- inseriti in Barccode e/o Codarfo
      '--------------------------------------------------------------------------------------------------------------
      If strProgrChiamante.ToUpper.Trim = "BNDKKONS" Then
        If (edBarcode.Visible = True) And (edBarcode.Text.Trim <> "") And _
           (edBarcode.Text <> "'%%'") And (edBarcode.Text <> "'%'") Then
          If Microsoft.VisualBasic.Left(edBarcode.Text, 1) <> "%" Then edBarcode.Text = "%" & edBarcode.Text
          If Microsoft.VisualBasic.Right(edBarcode.Text, 1) <> "%" Then edBarcode.Text += "%"
          strQuery += SetPA("ar_codart") & " IN (SELECT bc_codart FROM barcode" & _
                                               " WHERE barcode.codditt = artico.codditt" & _
                                               " AND barcode.bc_codart = artico.ar_codart" & _
                                               " AND bc_code LIKE " & FormatRptSql(edBarcode.Text) & ")§"
        End If
        If (edCodarfo.Visible = True) And (edCodarfo.Text.Trim <> "") And _
           (edCodarfo.Text <> "'%%'") And (edCodarfo.Text <> "'%'") Then
          If Microsoft.VisualBasic.Left(edCodarfo.Text, 1) <> "%" Then edCodarfo.Text = "%" & edCodarfo.Text
          If Microsoft.VisualBasic.Right(edCodarfo.Text, 1) <> "%" Then edCodarfo.Text += "%"
          strQuery += SetPA("ar_codart") & " IN (SELECT caf_codart FROM codarfo" & _
                                               " WHERE codarfo.codditt = artico.codditt" & _
                                               " AND codarfo.caf_codart = artico.ar_codart" & _
                                               " AND caf_codarfo LIKE " & FormatRptSql(edCodarfo.Text) & ")§"
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      'filtri artico secondo tab
      If edDescrd.Text <> "" Then strQuery += SetPA("ar_descr") & " >= " & FormatRptSql(edDescrd.Text) & "§"
      If edDescra.Text <> "".PadLeft(40, "Z"c) Then strQuery += SetPA("ar_descr") & " <= " & FormatRptSql(edDescra.Text) & "§"
      If edCodartd.Text <> "" Then strQuery += SetPA("ar_codart") & " >= " & FormatRptSql(edCodartd.Text) & "§"
      If edCodarta.Text <> "".PadLeft(CLN__STD.CodartMaxLen, "Z"c) Then strQuery += SetPA("ar_codart") & " <= " & FormatRptSql(edCodarta.Text) & "§"

      If edCodaltd.Text <> "" Then strQuery += SetPA("ar_codalt") & " >= " & FormatRptSql(edCodaltd.Text) & "§"
      If edCodalta.Text <> "".PadLeft(CLN__STD.CodartMaxLen, "Z"c) Then strQuery += SetPA("ar_codalt") & " <= " & FormatRptSql(edCodalta.Text) & "§"
      If edGruppod.Text <> "0" Then strQuery += SetPA("ar_gruppo") & " >= " & edGruppod.Text & "§"
      If edGruppoa.Text <> "99" Then strQuery += SetPA("ar_gruppo") & " <= " & edGruppoa.Text & "§"
      If edSottd.Text <> "0" Then strQuery += SetPA("ar_sotgru") & " >= " & edSottd.Text & "§"
      If edSotta.Text <> "9999" Then strQuery += SetPA("ar_sotgru") & " <= " & edSotta.Text & "§"
      If edFamprodd.Text <> "" Then strQuery += SetPA("ar_famprod") & " >= " & FormatRptSql(edFamprodd.Text) & "§"
      If edFamproda.Text <> "".PadLeft(4, "Z"c) Then strQuery += SetPA("ar_famprod") & " <= " & FormatRptSql(edFamproda.Text) & "§"
      If edClassificazioneLivello1.Text.Trim <> "" Then strQuery += SetPA("ar_codcla1") & " = " & FormatRptSql(edClassificazioneLivello1.Text) & "§"
      If edClassificazioneLivello2.Text.Trim <> "" Then strQuery += SetPA("ar_codcla2") & " = " & FormatRptSql(edClassificazioneLivello2.Text) & "§"
      If edClassificazioneLivello3.Text.Trim <> "" Then strQuery += SetPA("ar_codcla3") & " = " & FormatRptSql(edClassificazioneLivello3.Text) & "§"
      If edClassificazioneLivello4.Text.Trim <> "" Then strQuery += SetPA("ar_codcla4") & " = " & FormatRptSql(edClassificazioneLivello4.Text) & "§"
      If edClassificazioneLivello5.Text.Trim <> "" Then strQuery += SetPA("ar_codcla5") & " = " & FormatRptSql(edClassificazioneLivello5.Text) & "§"
      If edFornd.Text <> "0" Then strQuery += SetPA("ar_forn") & " >= " & edFornd.Text & "§"
      If edForna.Text <> "".PadLeft(9, "9"c) Then strQuery += SetPA("ar_forn") & " <= " & edForna.Text & "§"
      If oParam.bVisGriglia = False Then
        If edCodtipad.Text <> "0" Then strQuery += SetPA("ar_codtipa") & " >= " & edCodtipad.Text & "§"
        If edCodtipaa.Text <> "".PadLeft(3, "9"c) Then strQuery += SetPA("ar_codtipa") & " <= " & edCodtipaa.Text & "§"
      End If
      If edMarcad.Text <> "0" Then strQuery += SetPA("ar_codmarc") & " >= " & edMarcad.Text & "§"
      If edMarcaa.Text <> "".PadLeft(3, "9"c) Then strQuery += SetPA("ar_codmarc") & " <= " & edMarcaa.Text & "§"
      If edProvvd.Text <> "0" Then strQuery += SetPA("ar_claprov") & " >= " & edProvvd.Text & "§"
      If edProvva.Text <> "".PadLeft(3, "9"c) Then strQuery += SetPA("ar_claprov") & " <= " & edProvva.Text & "§"
      If edScontid.Text <> "0" Then strQuery += SetPA("ar_clascon") & " >= " & edScontid.Text & "§"
      If edScontia.Text <> "".PadLeft(3, "9"c) Then strQuery += SetPA("ar_clascon") & " <= " & edScontia.Text & "§"
      If edApprovd.Text <> "0" Then strQuery += SetPA("ar_codappr") & " >= " & edApprovd.Text & "§"
      If edApprova.Text <> "".PadLeft(3, "9"c) Then strQuery += SetPA("ar_codappr") & " <= " & edApprova.Text & "§"
      If edCodIvad.Text <> "0" Then strQuery += SetPA("ar_codiva") & " >= " & edCodIvad.Text & "§"
      If edCodIvaa.Text <> "".PadLeft(4, "9"c) Then strQuery += SetPA("ar_codiva") & " <= " & edCodIvaa.Text & "§"
      If NTSCDate(edDataUltagd.Text) <> NTSCDate(IntSetDate("01/01/1900")) Then strQuery += SetPA("ar_ultagg") & " >= " & FormatRptSql(edDataUltagd.Text, "D") & "§"
      If NTSCDate(edDataUltaga.Text) <> NTSCDate(IntSetDate("31/12/2099")) Then strQuery += SetPA("ar_ultagg") & " <= " & FormatRptSql(edDataUltaga.Text, "D") & "§"
      If edMagprodini.Text <> "0" Then strQuery += SetPA("ar_magprod") & " >= " & edMagprodini.Text & "§"
      If edMagprodfin.Text <> "".PadLeft(4, "9"c) Then strQuery += SetPA("ar_magprod") & " <= " & edMagprodfin.Text & "§"
      If edMagstockini.Text <> "0" Then strQuery += SetPA("ar_magstock") & " >= " & edMagstockini.Text & "§"
      If edMagstockfin.Text <> "".PadLeft(4, "9"c) Then strQuery += SetPA("ar_magstock") & " <= " & edMagstockfin.Text & "§"
      '------------------------------------------
      'filtri artico terzo tab
      If edAnnod.Text <> "1900" Then strQuery += SetPA("ar_anno") & " >= " & edAnnod.Text & "§"
      If edAnnoa.Text <> "2099" Then strQuery += SetPA("ar_anno") & " <= " & edAnnoa.Text & "§"
      If edCodstagd.Text <> "0" Then strQuery += SetPA("ar_codstag") & " >= " & edCodstagd.Text & "§"
      If edCodstaga.Text <> "".PadLeft(4, "9"c) Then strQuery += SetPA("ar_codstag") & " <= " & edCodstaga.Text & "§"
      If edCodtagld.Text <> "0" Then strQuery += SetPA("ar_codtagl") & " >= " & edCodtagld.Text & "§"
      If edCodtagla.Text <> "".PadLeft(4, "9"c) Then strQuery += SetPA("ar_codtagl") & " <= " & edCodtagla.Text & "§"

      If cbLotti.SelectedValue <> "T" Then strQuery += SetPA("ar_geslotti") & " = " & FormatRptSql(cbLotti.SelectedValue) & "§"
      If cbInesaur.SelectedValue <> "T" Then strQuery += SetPA("ar_inesaur") & " = " & FormatRptSql(cbInesaur.SelectedValue) & "§"
      If cbCommessa.SelectedValue <> "T" Then strQuery += SetPA("ar_gescomm") & " = " & FormatRptSql(cbCommessa.SelectedValue) & "§"

      If cbAlistino.SelectedValue <> "T" Then strQuery += SetPA("ar_stalist") & " = " & FormatRptSql(cbAlistino.SelectedValue) & "§"
      If cbMatricole.SelectedValue <> "T" Then strQuery += SetPA("ar_gestmatr") & " = " & FormatRptSql(cbMatricole.SelectedValue) & "§"
      If cbCritico.SelectedValue <> "T" Then strQuery += SetPA("ar_critico") & " = " & FormatRptSql(cbCritico.SelectedValue) & "§"
      If cbUbicaz.SelectedValue <> "T" Then strQuery += SetPA("ar_gesubic") & " = " & FormatRptSql(cbUbicaz.SelectedValue) & "§"
      If cbAfasi.SelectedValue <> "T" Then strQuery += SetPA("ar_gesfasi") & " = " & FormatRptSql(cbAfasi.SelectedValue) & "§"
      Select Case cbAvarianti.SelectedValue
        Case "N" : strQuery += SetPA("ar_gesvar") & " IN ('N', 'J')§" 'Sia normali che configurati
        Case "T" 'Nel caso T non deve filtrare nulla 
        Case Else : strQuery += SetPA("ar_gesvar") & " = " & FormatRptSql(cbAvarianti.SelectedValue) & "§"
      End Select


      If NTSCInt(edListsar.Text) <> 0 Then
        If bCr Then 'per crystal non gestito ^N^ è un carattere speciale
          strQuery = "^N^Per la stampa su crystal reports non è possibile inserire filtri per lista selez."
          Return False
        Else
          strQuery += "ar_codart IN " & oCleHlar.GetQueryListe("LISTSAR", NTSCInt(edListsar.Text)) & "§"
        End If
      End If
      If NTSCInt(edArtprom.Text) <> 0 Then
        If bCr Then 'per crystal non gestito ^N^ è un carattere speciale
          strQuery = "^N^Per la stampa su crystal reports non è possibile inserire filtri per lista promozioni."
          Return False
        Else
          strQuery += "ar_codart IN " & oCleHlar.GetQueryListe("ARTPROM", NTSCInt(edArtprom.Text)) & "§"
        End If
      End If

      If strCodRoot.Trim <> "" Then strQuery += SetPA("ar_codroot") & " = " & FormatRptSql(strCodRoot) & "§"

      If strQuery.Length > 0 Then
        strQuery = strQuery.Substring(0, strQuery.Length - 1)
        If strProgrChiamante.ToUpper = "BNMGCLAS" Then strQuery += "§ar_codcla1=' '"
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return False
    End Try
  End Function
  Public Overridable Function SetPA(ByVal strTmp As String) As String
    Try
      If bCr Then
        Return SetPA(strTmp, "artico")
      Else
        Return strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function
  Public Overridable Function SetPA(ByVal strTmp As String, ByVal strTabella As String) As String
    Try
      'CheckInvokeCustomFunction( 'per non fare scattare msg avviso in compilaz funzioni stesso nome create nella stessa archiviazione
      If bCr Then
        Return "{" & strTabella & NTSCStr(IIf(strTabella <> "", ".", "")) & strTmp & "}"
      Else
        Return strTmp
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function
  Public Overridable Function FormatRptSql(ByVal strTmp As String) As String
    Try
      Return FormatRptSql(strTmp, "S")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function
  Public Overridable Function FormatRptSql(ByVal strTmp As String, ByVal strTipo As String) As String
    Try
      'CheckInvokeCustomFunction( 'per non fare scattare msg avviso in compilaz funzioni stesso nome create nella stessa archiviazione
      If strTipo = "S" Then 'stringa
        If bCr Then
          Return ConvStrRpt(strTmp)
        Else
          Return CStrSQL(strTmp)
        End If
      ElseIf strTipo = "N" Then 'numero decimale
        Return CDblSQL(NTSCDec(strTmp))
      ElseIf strTipo = "D" Then 'data
        If bCr Then
          Return ConvDataRpt(strTmp)
        Else
          Return CDataSQL(NTSCDate(strTmp))
        End If
      Else 'altrimenti stringa
        If bCr Then
          Return ConvStrRpt(strTmp)
        Else
          Return CStrSQL(strTmp)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
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

  Public Overridable Sub cbTestArt_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbTestArt.SelectedValueChanged
    Try
      Select Case cbTestArt.SelectedValue
        Case "T"
          cbAvarianti.SelectedValue = "T"
          edCodtagld.Text = "0"
          edCodtagla.Text = "9999"
        Case "N"
          cbAvarianti.SelectedValue = "N"
          edCodtagld.Text = "0"
          edCodtagla.Text = "9999"
        Case "V"
          cbAvarianti.SelectedValue = "S"
          edCodtagld.Text = "0"
          edCodtagla.Text = "0"
        Case "C"
          cbAvarianti.SelectedValue = "S"
          edCodtagld.Text = "1"
          edCodtagla.Text = "9999"
      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Classificazioni"
  Public Overridable Sub cmdClassifica_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClassifica.Click
    Dim oParam As New CLE__PATB
    Dim strClassificaFilter() As String = Nothing
    Try
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTCLAS", DittaCorrente, oParam)
      If NTSZOOM.strIn <> "" Then
        strClassificaFilter = NTSZOOM.strIn.Split("|"c)
        edClassificazioneLivello1.Text = strClassificaFilter(0)
        edClassificazioneLivello2.Text = strClassificaFilter(1)
        edClassificazioneLivello3.Text = strClassificaFilter(2)
        edClassificazioneLivello4.Text = strClassificaFilter(3)
        edClassificazioneLivello5.Text = strClassificaFilter(4)
        lbClassifica.Text = oCleHlar.GetArtclasDescr(strClassificaFilter(0), strClassificaFilter(1), _
                                strClassificaFilter(2), strClassificaFilter(3), strClassificaFilter(4))
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdClassificaDeleteFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClassificaDeleteFilter.Click
    Try
      edClassificazioneLivello1.Text = ""
      edClassificazioneLivello2.Text = ""
      edClassificazioneLivello3.Text = ""
      edClassificazioneLivello4.Text = ""
      edClassificazioneLivello5.Text = ""
      lbClassifica.Text = ""
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdFiltriClassificazione_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFiltriClassificazione.Click
    Try
      If pnClassificazione.Width = cmdFiltriClassificazione.Width Then
        pnClassificazione.Width = lDimPnClass
        cmdFiltriClassificazione.Text = "<"
        oMenu.SaveSettingBus("BSMGHLAR", "RECENT", ".", "FiltriClassificazione", "-1", ".", "NS.", "...", "...")
      Else
        pnClassificazione.Width = cmdFiltriClassificazione.Width
        cmdFiltriClassificazione.Text = ">"
        oMenu.SaveSettingBus("BSMGHLAR", "RECENT", ".", "FiltriClassificazione", "0", ".", "NS.", "...", "...")
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub CaricaClassificazioni()
    Dim dsClass As DataSet = Nothing
    Try
      lDimPnClass = pnClassificazione.Width

      oCleHlar.ListaClassificazioni(dsClass)

      trClass.Nodes.Add("", "(Tutti gli articoli)")

      If dsClass.Tables("ARTCLAS1").Rows.Count = 0 Then
        If pnClassificazione.Width > cmdFiltriClassificazione.Width Then cmdFiltriClassificazione_Click(Nothing, Nothing)
        Return
      End If

      If Not CBool(oMenu.GetSettingBus("BSMGHLAR", "RECENT", ".", "FiltriClassificazione", "-1", ".", "-1")) Then
        If pnClassificazione.Width > cmdFiltriClassificazione.Width Then cmdFiltriClassificazione_Click(Nothing, Nothing)
      End If


      'Carica le immagini della treeview
      Dim imgList As New ImageList
      imgList.Images.Add("FLDC", Bitmap.FromFile(oApp.ChildImageDir & "\open_treeview.gif"))
      imgList.Images.Add("FLDO", Bitmap.FromFile(oApp.ChildImageDir & "\open_treeviewsel.gif"))
      trClass.ImageList = imgList
      trClass.ImageIndex = 0
      trClass.SelectedImageIndex = 1

      For k As Integer = 1 To 5
        CaricaLivelloClassificazione(k, dsClass.Tables("ARTCLAS" & k))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Function CaricaLivelloClassificazione(ByVal lLivello As Integer, ByVal dttLivello As DataTable) As Boolean
    Dim nodeCurrent As TreeNode
    Dim nodeNew As TreeNode
    Try
      For z As Integer = 0 To dttLivello.Rows.Count - 1
        nodeNew = New TreeNode(NTSCStr(dttLivello.Rows(z)("acl_descla" & lLivello)))
        nodeNew.Name = NTSCStr(dttLivello.Rows(z)("acl_codcla" & lLivello))
        nodeNew.Tag = NTSCStr(dttLivello.Rows(z)("acl_codcla" & lLivello))
        nodeNew.ImageKey = "FLDC"

        If lLivello = 1 Then
          trClass.Nodes.Add(nodeNew)
        Else
          nodeCurrent = TrovaNodoDaTag(trClass.Nodes, NTSCStr(dttLivello.Rows(z)("acl_codcla1")))
          For k As Integer = 2 To lLivello - 1
            If nodeCurrent Is Nothing Then Continue For
            nodeCurrent = TrovaNodoDaTag(nodeCurrent.Nodes, NTSCStr(dttLivello.Rows(z)("acl_codcla" & k)))
          Next

          If nodeNew Is Nothing Or nodeCurrent Is Nothing Then Continue For
          nodeCurrent.Nodes.Add(nodeNew)
        End If
      Next

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function TrovaNodoDaTag(ByVal nodeCollection As TreeNodeCollection, ByVal strTag As String) As TreeNode
    Try
      For Each node As TreeNode In nodeCollection
        If NTSCStr(node.Tag) = strTag Then Return node
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
    Return Nothing
  End Function

  Public Overridable Sub trClass_AfterCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trClass.AfterCollapse
    Try

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub trClass_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trClass.AfterExpand
    Try

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub trClass_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trClass.BeforeExpand
    Try

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub trClass_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trClass.NodeMouseClick
    Dim arNodeList As New ArrayList
    Dim node As TreeNode = e.Node
    Dim lLivello As Integer
    Try
      edClassificazioneLivello1.Text = ""
      edClassificazioneLivello2.Text = ""
      edClassificazioneLivello3.Text = ""
      edClassificazioneLivello4.Text = ""
      edClassificazioneLivello5.Text = ""

      If e.Node Is Nothing Then cmdRicerca_Click(Nothing, Nothing) : Return

      While Not node.Parent Is Nothing
        arNodeList.Add(node.Tag)
        node = node.Parent
      End While
      arNodeList.Add(node.Tag)

      lLivello = arNodeList.Count
      While arNodeList.Count > 0
        Select Case lLivello
          Case 1 : edClassificazioneLivello1.Text = NTSCStr(arNodeList(0))
          Case 2 : edClassificazioneLivello2.Text = NTSCStr(arNodeList(0))
          Case 3 : edClassificazioneLivello3.Text = NTSCStr(arNodeList(0))
          Case 4 : edClassificazioneLivello4.Text = NTSCStr(arNodeList(0))
          Case 5 : edClassificazioneLivello5.Text = NTSCStr(arNodeList(0))
        End Select

        arNodeList.RemoveAt(0)
        lLivello -= 1
      End While

      cmdRicerca_Click(Nothing, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region
End Class
