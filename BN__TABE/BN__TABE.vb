Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__TABE
  'questo programma serve come base per gestire tabelle del tipo TABxxxx, 
  'con prima colonna tb_codxxxx e seconda colonnatb_desxxxx
  'Se da bus lancio il progamma bs--cate, bscrperv, prendo gli ultimi 4 caratteri del nome programma
  'e se esiste una tabella "TAB" + ultimi 4 caratteri creo al volo il progr ereditando da BN__TABE e lo lancio.
  'per ora lavora solo su db AZIENDA, no ARCPROC

  'per le personalizzazioni sono gestite TABHHxx e TABQQxx
  'con colonne del tipo tb_codHHxx/tb_desHHxx e tb_codQQxx/tb_desQQxx
  'i nomi delle colonne, la lunghezza del testo delle stringhe e quanto altro vengono rilevati da database
  'per tabelle ti tipo TABHHxxxx e TABQQxxxx posso avere anche più di 2 colonne e non c'è bisogno 
  'di chiamare le prime colonne tb_cod e tb_des

  'per le colonne numeriche per impostare la maxlen leggo le costanti tabella da BN__STD
  'la caption della form vieme presa da ORDERTBL
  'per stampa / help prendo il nome del programma ereditato (BN__CATE) e sostituisco BS con BN e "_" con "-"
  'controllo sulla chiave di attivazione            non viene eseguito
  'per le colonne con vincoli check e check constraint (tipo combobox, checkbox e tm_conto -> an_conto) 
  'query per ritornare i vincoli:
  'SELECT sys.check_constraints.* 
  'FROM sysobjects INNER JOIN sys.check_constraints ON sysobjects.Id = sys.check_constraints.parent_object_id
  'WHERE sysobjects.name = 'tabcate'
  'AND sys.check_constraints.type = 'C'             ok

  'BeforeColUpdate e AfterColUpdate                 non faccio nulla: i test di rito in TestPreSalva
  'TestPreSalva                                     controllo che chiave primaria non duplicata e che colonne non nullabili siano OK


  'con GCTL è possibile cambiare la caption delle colonne non mappate correttamente (ma con nome colonna = nome campo di colonna DB)

  'le tabelle che dovrebbero essere gestite da questo programma sono quelle che risultano dalla seguente query:
  'SELECT sysobjects.name
  'FROM sysobjects INNER JOIN syscolumns ON sysobjects.id = syscolumns.id 
  'WHERE syscolumns.name <> 'ts'
  'and substring(sysobjects.name, 1, 3) = 'tab'
  'AND syscolumns.name <> 'codditt'
  'GROUP BY sysobjects.name having count(*) = 2

  'ROBY: sono solo tabelle codice / descrizione: non facciamo nessun controllo
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = bsModExtAll
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

  Public oCleTabe As CLE__TABE
  Public oCallParams As CLE__CLDP
  Public dsTabe As DataSet
  Public dcTabe As BindingSource = New BindingSource()

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents grTabe As NTSInformatica.NTSGrid
  Public WithEvents grvTabe As NTSInformatica.NTSGridView

  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__TABE))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grTabe = New NTSInformatica.NTSGrid
    Me.grvTabe = New NTSInformatica.NTSGridView

    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grTabe, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvTabe, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante})
    Me.NtsBarManager1.MaxItemId = 17
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbNuovo
    '
    Me.tlbNuovo.Caption = "Nuovo"
    Me.tlbNuovo.Glyph = CType(resources.GetObject("tlbNuovo.Glyph"), System.Drawing.Image)
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 1
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.Id = 3
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 15
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 16
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 4
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 5
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    '
    'grTabe
    '
    Me.grTabe.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grTabe.EmbeddedNavigator.Name = ""
    Me.grTabe.Location = New System.Drawing.Point(0, 26)
    Me.grTabe.MainView = Me.grvTabe
    Me.grTabe.Name = "grTabe"
    Me.grTabe.Size = New System.Drawing.Size(648, 416)
    Me.grTabe.TabIndex = 5
    Me.grTabe.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvTabe})
    '
    'grvTabe
    '
    Me.grvTabe.ActiveFilterEnabled = False
    Me.grvTabe.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvTabe.Enabled = True
    Me.grvTabe.GridControl = Me.grTabe
    Me.grvTabe.Name = "grvTabe"
    Me.grvTabe.NTSAllowDelete = True
    Me.grvTabe.NTSAllowInsert = True
    Me.grvTabe.NTSAllowUpdate = True
    Me.grvTabe.NTSMenuContext = Nothing
    Me.grvTabe.OptionsCustomization.AllowRowSizing = True
    Me.grvTabe.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvTabe.OptionsNavigation.UseTabKey = False
    Me.grvTabe.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvTabe.OptionsView.ColumnAutoWidth = False
    Me.grvTabe.OptionsView.EnableAppearanceEvenRow = True
    Me.grvTabe.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvTabe.OptionsView.ShowGroupPanel = False
    Me.grvTabe.RowHeight = 16
    '
    'FRM__TABE
    '
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grTabe)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRM__TABE"
    Me.NTSLastControlFocussed = Me.grTabe
    Me.Text = ""
    Me.HelpContext = "*"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grTabe, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvTabe, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    Dim strTableName As String = ""
    Dim strDescr As String = ""

    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then
      DittaCorrente = Ditta
    Else
      DittaCorrente = oApp.Ditta
    End If
    Me.GctlTipoDoc = ""

    InitializeComponent()
    Me.MinimumSize = Me.Size

    '------------------------------------------------
    'ottengo il nome della tabella da trattare (è e deve continuare ad essere la stessa tecnica di BE__MENU.EsisteChildNet)
    Dim assem As System.Reflection.Assembly = System.Reflection.Assembly.GetAssembly(Me.GetType())
    strTableName = "TAB" & assem.Location.Substring(assem.Location.Length - 8, 4).ToUpper

    '------------------------------------------------
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__TABE", "BE__TABE", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.TransMsg("BN__TABE", 128390972530710000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleTabe = CType(oTmp, CLE__TABE)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__TABE", strRemoteServer, strRemotePort)
    AddHandler oCleTabe.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleTabe.Init(oApp, oScript, oMenu.oCleComm, strTableName, bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    oCleTabe.strChildName = assem.Location.Substring(assem.Location.Length - 12, 8).ToUpper

    '-------------------------------------------------
    'assegno il nome al child
    oMenu.ValCodiceDb(oCleTabe.strNomeTabella, DittaCorrente, "ORDERTBL", "S", strDescr)
    If strDescr = "" Then strDescr = oApp.TransMsg("BN__TABE", 129001517444781525, "Gestione tabella '|" & oCleTabe.strNomeTabella & "|'")
    Me.Text = strDescr.ToUpper & Me.Text
    If oCleTabe.strNomeTabella = "TABSEZD" Then Me.Text = "SEZIONI DOGANALI"

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)

    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      'esempio per colonna combo
      'Dim dttTipo As New DataTable()
      'dttTipo.Columns.Add("cod", GetType(String))
      'dttTipo.Columns.Add("val", GetType(String))
      'dttTipo.Rows.Add(New Object() {" ", "Propria"})
      'dttTipo.Rows.Add(New Object() {"C", "Altrui"})
      'dttTipo.AcceptChanges()

      grvTabe.NTSSetParam(oMenu, oApp.TransMsg("BN__TABE", 128392402631064000, "Gestione tabella '|" & oCleTabe.strNomeTabella & "|'"))

      '-------------------------------------------------
      'chiamo lo script per inizializzare i controlli caricati con source ext
      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

  Public Overridable Function CreaColonneGriglia() As Boolean
    '----------------------------
    'data la tabella provvede a creare le colonne della griglia
    Dim i As Integer = 0
    Dim oColonna As NTSGridColumn = Nothing
    Dim strColCaption As String = ""
    Dim dtrT() As DataRow = Nothing
    Try

      dtrT = oCleTabe.dsTableDataDict.Tables("TABLE_STRUCT").Select("ColumnName <> 'ts' and ColumnName <> 'codditt'", "ColumnOrdinal")
      For i = 0 To dtrT.Length - 1

        'nome della colonna
        strColCaption = dtrT(i)!ColumnName.ToString.ToLower
        If dtrT(i)!ColumnName.ToString.ToLower.IndexOf("tb_cod") > -1 Then strColCaption = oApp.TransMsg("BN__TABE", 128390972530398000, "Codice")
        If dtrT(i)!ColumnName.ToString.ToLower.IndexOf("tb_des") > -1 Then strColCaption = oApp.TransMsg("BN__TABE", 129001494789111328, "Descrizione")

        'impostazioni default colonna
        oColonna = New NTSGridColumn
        With oColonna
          .AppearanceCell.Options.UseBackColor = True
          .AppearanceCell.Options.UseTextOptions = True
          .Caption = strColCaption
          .Name = dtrT(i)!ColumnName.ToString.ToLower
          .FieldName = dtrT(i)!ColumnName.ToString.ToLower
          .NTSRepositoryComboBox = Nothing
          .NTSRepositoryItemCheck = Nothing
          .NTSRepositoryItemText = Nothing
          .OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
          .OptionsFilter.AllowFilter = False
          .FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
          .Width = 70
          .Visible = True
          .VisibleIndex = i
          .Enabled = True
        End With

        Me.grvTabe.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {oColonna})

        'imposto NTSSetParam
        Select Case dtrT(i)!DataType.ToString
          Case "System.String"
            CType(oColonna, NTSGridColumn).NTSSetParamSTR(oMenu, _
                                        strColCaption, _
                                        NTSCInt(IIf(NTSCInt(dtrT(i)!ColumnSize.ToString) > 1000000, 0, NTSCInt(dtrT(i)!ColumnSize.ToString))), _
                                        CBool(dtrT(i)!AllowDBNull))
          Case "System.DateTime"
            CType(oColonna, NTSGridColumn).NTSSetParamDATA(oMenu, _
                                        strColCaption, _
                                        CBool(dtrT(i)!AllowDBNull), "D", _
                                        NTSCDate(IntSetDate("01/01/1900")), _
                                        NTSCDate(IntSetDate("31/12/2099")))
          Case "System.Decimal"
            'potrebbe essere un campo  importo (2 decimali) o sconto (2 decimali)
            CType(oColonna, NTSGridColumn).NTSSetParamNUM(oMenu, _
                                         strColCaption, _
                                        oApp.FormatImporti, _
                                        20)
          Case "System.Double"
            'potrebbe essere un campo quantità (3 decimali) o cambio (9 decimali)
            'lo tratto come una quantità
            CType(oColonna, NTSGridColumn).NTSSetParamNUM(oMenu, _
                                        strColCaption, _
                                        oApp.FormatQta, _
                                        20)
          Case "System.Int32"
            'o è il codice cli/forn (e non dovrebbe accettare negativi) o ....
            'lo lascio comunque libero
            CType(oColonna, NTSGridColumn).NTSSetParamNUM(oMenu, _
                                        strColCaption, _
                                        "0", 9, -999999999, 999999999)
          Case "System.Int16"
            'puo essere il campo codice della tabella (da 0 a 999 o 9999), oppure una anno da 1900 a 2099, oppure ...
            'da finire
            If dtrT(i)!ColumnName.ToString.ToLower.Substring(0, 6) = "tb_cod" Then
              'come faccio a sapere se il cod è di 3 o 4 caratteri?
              'devo risalire alla costante in BN__STD (avrò anche lo zoom - da disabilitare)
              Dim oFieldIno As System.Reflection.FieldInfo = GetType(CLN__STD).GetField(oCleTabe.strNomeTabella.ToLower)
              If Not oFieldIno Is Nothing Then
                CType(oColonna, NTSGridColumn).NTSSetParamNUMTabe(oMenu, _
                                            strColCaption, _
                                            NTSCInt(oFieldIno.GetValue(New Object).ToString))
                CType(oColonna, NTSGridColumn).NTSSetParamZoom("")      'tolgo lo zoom, se c'è
              Else
                'colonna generica (dovrebbe passare di qui solo per le tabelle dei rivenditori...)
                CType(oColonna, NTSGridColumn).NTSSetParamNUM(oMenu, _
                                            strColCaption, _
                                            "0", 4, 0, 9999)
              End If
            Else
              'per me è un anno o qualche cosa di libero
              CType(oColonna, NTSGridColumn).NTSSetParamNUM(oMenu, _
                                          strColCaption, _
                                          "0", 4, -9999, 9999)
            End If
          Case Else
            '... lo tratto come una stringa
            CType(oColonna, NTSGridColumn).NTSSetParamSTR(oMenu, _
                                        strColCaption, _
                                        NTSCInt(IIf(NTSCInt(dtrT(i)!ColumnSize.ToString) > 1000000, 0, NTSCInt(dtrT(i)!ColumnSize.ToString))), _
                                        CBool(dtrT(i)!AllowDBNull))
        End Select
      Next

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


#Region "Eventi di Form"
  Public Overridable Sub FRM__TABE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Dim l As Integer = 0
    Dim strDescr As String = ""
    Dim strT() As String = Nothing
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator
      If Not oCleTabe.Apri(DittaCorrente, strDescr, dsTabe) Then
        Me.Close()
        Return
      End If
      dcTabe.DataSource = dsTabe.Tables(oCleTabe.strNomeTabella)
      dsTabe.AcceptChanges()

      '-------------------------------------------------
      If Not CreaColonneGriglia() Then Me.Close()

      grTabe.DataSource = dcTabe


      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      '--------------------------------------------
      'imposto nel datast la proprietà 'caption' = alla caption della colonna, in modo che 
      'nei messaggi di errore dell'entity appaia il nome corretto della colonna incriminata
      For i = 0 To grvTabe.Columns.Count - 1
        dsTabe.Tables(oCleTabe.strNomeTabella).Columns(grvTabe.Columns(i).Name).Caption = grvTabe.Columns(i).Caption
      Next

      '--------------------------------------------
      'se manca il report nascondod le icone dalla toolbar
      If Not System.IO.File.Exists("BS" & oCleTabe.strChildName.Substring(2).Replace("_", "-") & ".RPT") Then
        tlbStampa.Visible = False
        tlbStampaVideo.Visible = False
      End If

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          grvTabe.NTSNuovo()
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          strT = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6).Split(CChar("|"))
          For i = 0 To dcTabe.List.Count - 1
            If oCleTabe.strChiavePrimaria(0) = "codditt" Then l = 1 Else l = 0 'devo scartare la colonna chiave 'codditt'
            Select Case strT.Length
              Case 1
                If CType(dcTabe.Item(i), DataRowView).Item(oCleTabe.strChiavePrimaria(l + 0)).ToString = strT(0) Then
                  dcTabe.Position = i
                  Exit For
                End If
              Case 2
                If CType(dcTabe.Item(i), DataRowView).Item(oCleTabe.strChiavePrimaria(l + 0)).ToString = strT(0) And _
                   CType(dcTabe.Item(i), DataRowView).Item(oCleTabe.strChiavePrimaria(l + 1)).ToString = strT(1) Then
                  dcTabe.Position = i
                  Exit For
                End If
              Case 3
                If CType(dcTabe.Item(i), DataRowView).Item(oCleTabe.strChiavePrimaria(l + 0)).ToString = strT(0) And _
                   CType(dcTabe.Item(i), DataRowView).Item(oCleTabe.strChiavePrimaria(l + 1)).ToString = strT(1) And _
                   CType(dcTabe.Item(i), DataRowView).Item(oCleTabe.strChiavePrimaria(l + 2)).ToString = strT(2) Then
                  dcTabe.Position = i
                  Exit For
                End If
            End Select

          Next
        End If
      End If  'If Not oCallParams Is Nothing Then

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__TABE_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

  Public Overridable Sub FRM__TABE_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcTabe.Dispose()
      dsTabe.Dispose()
    Catch
    End Try
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      If grvTabe.NTSAllowInsert Then
        grvTabe.NTSNuovo()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      Salva()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Try
      If Not grvTabe.NTSDeleteRigaCorrente(dcTabe, True) Then Return
      oCleTabe.Salva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvTabe.NTSRipristinaRigaCorrenteBefore(dcTabe, True) Then Return
      oCleTabe.Ripristina(grvTabe.NTSGetCurrentDataRow)
      grvTabe.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'zoom standard
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      If Not Salva() Then Return
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub
#End Region

  Public Overridable Sub grvTabe_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvTabe.NTSBeforeRowUpdate
    Try
      If Not Salva() Then
        'rimango sulla riga su cui sono
        e.Allow = False
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub grvTabe_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvTabe.NTSFocusedRowChanged
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim bChiaveImpostata As Boolean = False
    Try
      '-------------------------------------------------
      'se ho già impostato la chiave primaria, non la posso più cambiare ...
      If Not oCleTabe Is Nothing Then
        If grvTabe.Columns.Count > 0 And oCleTabe.strChiavePrimaria.Length > 0 Then
          'per default la chiave primaria è stata settata: se almeno un campo non lo sarà, abiliterò le colonne
          bChiaveImpostata = True
          For i = 0 To oCleTabe.strChiavePrimaria.Length - 1
            dtrT = oCleTabe.dsTableDataDict.Tables("TABLE_STRUCT").Select("ColumnName = " & CStrSQL(oCleTabe.strChiavePrimaria(i)))
            If Not dtrT Is Nothing Then
              Try
                If NTSCStr(dtrT(0)!DataType.ToString).ToLower.IndexOf("string") > -1 Then
                  'stringa
                  If NTSCStr(grvTabe.GetFocusedRowCellValue(oCleTabe.strChiavePrimaria(i))).Trim = "" Then
                    bChiaveImpostata = False
                    Exit For
                  End If
                ElseIf NTSCStr(dtrT(0)!DataType.ToString).ToLower.IndexOf("date") > -1 Then
                  'data
                  If NTSCStr(grvTabe.GetFocusedRowCellValue(oCleTabe.strChiavePrimaria(i))).Trim = "" Then
                    bChiaveImpostata = False
                    Exit For
                  End If
                Else
                  'numero
                  If NTSCInt(NTSCStr(grvTabe.GetFocusedRowCellValue(oCleTabe.strChiavePrimaria(i))).Trim) = 0 Then
                    bChiaveImpostata = False
                    Exit For
                  End If
                End If
              Catch
                'ignoro l'errore: se la colonna non è in griglia deve essere per forza impostata da codice ...
              End Try
            End If    'If Not dtrT Is Nothing Then
          Next

          If bChiaveImpostata Then
            'blocco le colonne
            For i = 0 To oCleTabe.strChiavePrimaria.Length - 1
              Try
                CType(grvTabe.Columns(oCleTabe.strChiavePrimaria(i)), NTSGridColumn).Enabled = False
              Catch
                'ignoro l'errore: la colonna del datatable potrebbe non essere in griglia
              End Try
            Next
          Else
            'abilito le colonne
            For i = 0 To oCleTabe.strChiavePrimaria.Length - 1
              Try
                GctlSetVisEnab(CType(grvTabe.Columns(oCleTabe.strChiavePrimaria(i)), NTSGridColumn), False)
              Catch
                'ignoro l'errore: la colonna del datatable potrebbe non essere in griglia
              End Try
            Next
          End If    'If bChiaveImpostata Then

        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub


  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      Dim dRes As DialogResult
      dRes = grvTabe.NTSSalvaRigaCorrente(dcTabe, oCleTabe.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleTabe.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleTabe.Ripristina(grvTabe.NTSGetCurrentDataRow)
        Case System.Windows.Forms.DialogResult.Cancel
          'non posso fare nulla
          Return False
        Case System.Windows.Forms.DialogResult.Abort
          'la riga non ha subito modifiche
      End Select
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer

    Try
      '--------------------------------------------------
      'preparo il motore di stampa: se serve passo la ditta
      strCrpe = ""
      If dsTabe.Tables(oCleTabe.strNomeTabella).Columns.Contains("codditt") Then
        strCrpe = "{" & oCleTabe.strNomeTabella & ".codditt} = '" & oApp.Ditta & "'"
      End If

      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, _
                                "BS" & oCleTabe.strChildName.Substring(2).Replace("_", "-"), _
                                "Reports1", " ", 0, nDestin, _
                                "BS" & oCleTabe.strChildName.Substring(2).Replace("_", "-") & ".RPT", _
                                False, Me.Text, False)
      If nPjob Is Nothing Then Return

      '--------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = LBound(CType(nPjob, Array), 2) To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

End Class
