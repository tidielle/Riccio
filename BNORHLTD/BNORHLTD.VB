Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORHLTD
  Public oCleHltd As CLEORHLTD
  Public oParam As CLE__PATB       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore
  Public dsHltd As New DataSet
  Public dcHltd As BindingSource = New BindingSource()
  Public bAbilitaStart As Boolean = False

  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents pnButton As NTSInformatica.NTSPanel
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents grHltd As NTSInformatica.NTSGrid
  Public WithEvents grvHltd As NTSInformatica.NTSGridView
  Public WithEvents cmdRicerca As NTSInformatica.NTSButton
  Public WithEvents cmdSeleziona As NTSInformatica.NTSButton
  Public WithEvents cmdDeseleziona As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents edCommeca As NTSInformatica.NTSTextBoxNum
  Public WithEvents xx_seleziona As NTSInformatica.NTSGridColumn
  Public WithEvents td_conto As NTSInformatica.NTSGridColumn
  Public WithEvents td_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents td_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents td_anno As NTSInformatica.NTSGridColumn
  Public WithEvents td_serie As NTSInformatica.NTSGridColumn
  Public WithEvents td_numord As NTSInformatica.NTSGridColumn
  Public WithEvents td_datord As NTSInformatica.NTSGridColumn
  Public WithEvents td_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents td_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents td_confermato As NTSInformatica.NTSGridColumn
  Public WithEvents td_rilasciato As NTSInformatica.NTSGridColumn
  Public WithEvents td_aperto As NTSInformatica.NTSGridColumn
  Public WithEvents td_sospeso As NTSInformatica.NTSGridColumn
  Public WithEvents td_annotco As NTSInformatica.NTSGridColumn
  Public WithEvents td_codstag As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codstag As NTSInformatica.NTSGridColumn
  Public WithEvents lbCommeca As NTSInformatica.NTSLabel
  Public WithEvents ckCommeca As NTSInformatica.NTSCheckBox
  Public WithEvents xx_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents td_alfpar As NTSInformatica.NTSGridColumn
  Public WithEvents td_numpar As NTSInformatica.NTSGridColumn
  Public WithEvents td_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents td_annpar As NTSInformatica.NTSGridColumn
  Public WithEvents td_datpar As NTSInformatica.NTSGridColumn
  Public WithEvents td_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents td_tipobf As NTSInformatica.NTSGridColumn
  Public WithEvents xx_tipobf As NTSInformatica.NTSGridColumn
  Public WithEvents td_magaz2 As NTSInformatica.NTSGridColumn
  Public WithEvents xx_magaz2 As NTSInformatica.NTSGridColumn
  Public WithEvents td_totdoc As NTSInformatica.NTSGridColumn
  Public WithEvents xx_magimp As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents cbEt_sospeso As NTSInformatica.NTSComboBox
  Public WithEvents lbBlocco As NTSInformatica.NTSLabel
  Public WithEvents lbStato As NTSInformatica.NTSLabel
  Public WithEvents cbEt_blocco As NTSInformatica.NTSComboBox
  Public WithEvents td_totmerce As NTSInformatica.NTSGridColumn
  Public WithEvents td_flevas As NTSInformatica.NTSGridColumn
  Public WithEvents td_magimp As NTSInformatica.NTSGridColumn
  Public WithEvents cmdApriOrdine As NTSInformatica.NTSButton


  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByVal NomeZoom As String, ByRef Param As CLE__PATB, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNORHLTD", "BEORHLTD", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128581693271641409, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleHltd = CType(oTmp, CLEORHLTD)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNORHLTD", strRemoteServer, strRemotePort)
    oCleHltd.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

    '---------------------------------
    'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    AddHandler oCleHltd.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      Dim dttTipoRk As New DataTable
      dttTipoRk.Columns.Add("cod", GetType(String))
      dttTipoRk.Columns.Add("val", GetType(String))
      dttTipoRk.Rows.Add(New Object() {"R", "Impegno cliente"})
      dttTipoRk.Rows.Add(New Object() {"O", "Ordine fornitore"})
      dttTipoRk.Rows.Add(New Object() {"H", "Ordine di produzione"})
      dttTipoRk.Rows.Add(New Object() {"X", "Impegno Trasferimento"})
      dttTipoRk.Rows.Add(New Object() {"Q", "Preventivo"})
      dttTipoRk.Rows.Add(New Object() {"#", "Impegno di commessa"})
      dttTipoRk.Rows.Add(New Object() {"V", "Impegno cliente aperto"})
      dttTipoRk.Rows.Add(New Object() {"$", "Ordine fornitore aperto"})
      dttTipoRk.Rows.Add(New Object() {"Y", "Impegno di produzione"})

      '-------------------------------------------------
      'completo le informazioni dei i controlli
      ckCommeca.NTSSetParam(oMenu, oApp.Tr(Me, 128583591414375000, "Filtro Commessa"), "S", "N")
      edCommeca.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128582590327500000, "Commessa"), tabcommess)
      cbEt_blocco.NTSSetParam(oApp.Tr(Me, 129425157077500000, "Blocco"))
      cbEt_sospeso.NTSSetParam(oApp.Tr(Me, 129425157077656250, "Sospeso"))
      grvHltd.NTSSetParam(oMenu, oApp.Tr(Me, 128230023157574793, "Griglia testata ordini"))
      grvHltd.NTSAllowDelete = False
      grvHltd.NTSAllowUpdate = True
      grvHltd.NTSAllowInsert = False
      xx_seleziona.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128582558793906250, "Seleziona"), "S", "N")
      td_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129527776966682729, "Conto"), tabanagra)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129527777312594449, "Descr. Conto"), 0, True)
      td_magaz.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129527776991136793, "Magazzino"), tabmaga)
      td_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 129527777007455779, "tipo ord."), dttTipoRk, "val", "cod")
      td_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129527777022358695, "Anno ord."), "0", 4, 0, 9999)
      td_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129527777194640701, "Serie ord."), CLN__STD.SerieMaxLen, True)
      td_numord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129527777097410405, "Num. ord."), "0", 9, 0, 999999999)
      td_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129527777122518791, "Data ord."), True)
      td_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129527777149726867, "Data cons."), True)
      xx_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129527777327370407, "Descr. magazzino"), 0, True)
      td_annpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129527777369110291, "Anno P."), "0", 4, 0, 9999)
      td_alfpar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129527777383925313, "Serie P."), CLN__STD.SerieMaxLen, True)
      td_numpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129527777400400555, "Num. P."), "0", 9, 0, 999999999)
      td_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129527777414072955, "Riferimenti"), 0, True)
      td_datpar.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129527777427598865, "Data partita"), True)
      td_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129527777442492015, "Commessa"), tabcommess)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129527777456047223, "Descr. commessa"), 0, True)
      td_tipobf.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129527777468791853, "Tipo bolla/fattura"), tabtpbf)
      xx_tipobf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129527777481575547, "Descr. tipo bolla/fattura"), 0, True)
      td_magaz2.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129527777494242049, "Magazzino 2"), tabmaga)
      xx_magaz2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129527777508441813, "Descr. magazzino 2"), 0, True)
      td_magimp.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129527777521752871, "Magazzino impegni"), tabmaga)
      xx_magimp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129527777536772979, "Descr. magazzino impegni"), 0, True)
      td_totdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129527777621649285, "Totale documento"), oApp.FormatImporti, 0)

      td_coddest.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129527777714885287, "Cod. destinaz."), "0", 9, 0, 999999999)
      xx_coddest.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129527777728274473, "Descr. destinaz."), 0, True)
      td_confermato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129527777741673425, "Confermato"), "S", "N")
      td_rilasciato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129527777755023547, "Rilasciato"), "S", "N")
      td_aperto.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129527777768051391, "Aperto"), "S", "N")
      td_sospeso.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129527777781001107, "Sospeso"), "S", "N")
      td_annotco.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129527777794019185, "Anno TC"), "0", 4, 0, 9999)
      td_codstag.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 129527777806724751, "Stagione"), tabstag)
      xx_codstag.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129527777819264295, "Descr. stagione"), 0, True)
      td_totmerce.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129527776558043991, "Totale merce"), oApp.FormatImporti, 0)
      td_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129605779789770494, "Evaso"), "S", "N")
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

  Private Sub InitializeComponent()
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.cbEt_blocco = New NTSInformatica.NTSComboBox
    Me.cbEt_sospeso = New NTSInformatica.NTSComboBox
    Me.lbBlocco = New NTSInformatica.NTSLabel
    Me.lbStato = New NTSInformatica.NTSLabel
    Me.lbCommeca = New NTSInformatica.NTSLabel
    Me.ckCommeca = New NTSInformatica.NTSCheckBox
    Me.edCommeca = New NTSInformatica.NTSTextBoxNum
    Me.pnButton = New NTSInformatica.NTSPanel
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.cmdDeseleziona = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grHltd = New NTSInformatica.NTSGrid
    Me.grvHltd = New NTSInformatica.NTSGridView
    Me.xx_seleziona = New NTSInformatica.NTSGridColumn
    Me.td_tipork = New NTSInformatica.NTSGridColumn
    Me.td_anno = New NTSInformatica.NTSGridColumn
    Me.td_serie = New NTSInformatica.NTSGridColumn
    Me.td_numord = New NTSInformatica.NTSGridColumn
    Me.td_datord = New NTSInformatica.NTSGridColumn
    Me.td_datcons = New NTSInformatica.NTSGridColumn
    Me.td_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.td_totdoc = New NTSInformatica.NTSGridColumn
    Me.td_annpar = New NTSInformatica.NTSGridColumn
    Me.td_alfpar = New NTSInformatica.NTSGridColumn
    Me.td_numpar = New NTSInformatica.NTSGridColumn
    Me.td_datpar = New NTSInformatica.NTSGridColumn
    Me.td_riferim = New NTSInformatica.NTSGridColumn
    Me.td_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.td_tipobf = New NTSInformatica.NTSGridColumn
    Me.xx_tipobf = New NTSInformatica.NTSGridColumn
    Me.td_magaz = New NTSInformatica.NTSGridColumn
    Me.xx_magaz = New NTSInformatica.NTSGridColumn
    Me.td_magaz2 = New NTSInformatica.NTSGridColumn
    Me.xx_magaz2 = New NTSInformatica.NTSGridColumn
    Me.td_magimp = New NTSInformatica.NTSGridColumn
    Me.xx_magimp = New NTSInformatica.NTSGridColumn
    Me.td_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.td_confermato = New NTSInformatica.NTSGridColumn
    Me.td_rilasciato = New NTSInformatica.NTSGridColumn
    Me.td_aperto = New NTSInformatica.NTSGridColumn
    Me.td_sospeso = New NTSInformatica.NTSGridColumn
    Me.td_annotco = New NTSInformatica.NTSGridColumn
    Me.td_codstag = New NTSInformatica.NTSGridColumn
    Me.xx_codstag = New NTSInformatica.NTSGridColumn
    Me.td_totmerce = New NTSInformatica.NTSGridColumn
    Me.td_flevas = New NTSInformatica.NTSGridColumn
    Me.cmdApriOrdine = New NTSInformatica.NTSButton
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.cbEt_blocco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbEt_sospeso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckCommeca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnButton.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grHltd, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvHltd, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.cbEt_blocco)
    Me.pnBottom.Controls.Add(Me.cbEt_sospeso)
    Me.pnBottom.Controls.Add(Me.lbBlocco)
    Me.pnBottom.Controls.Add(Me.lbStato)
    Me.pnBottom.Controls.Add(Me.lbCommeca)
    Me.pnBottom.Controls.Add(Me.ckCommeca)
    Me.pnBottom.Controls.Add(Me.edCommeca)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 267)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.Size = New System.Drawing.Size(711, 53)
    Me.pnBottom.TabIndex = 1
    Me.pnBottom.Text = "NtsPanel1"
    '
    'cbEt_blocco
    '
    Me.cbEt_blocco.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_blocco.DataSource = Nothing
    Me.cbEt_blocco.DisplayMember = ""
    Me.cbEt_blocco.Location = New System.Drawing.Point(462, 28)
    Me.cbEt_blocco.Name = "cbEt_blocco"
    Me.cbEt_blocco.NTSDbField = ""
    Me.cbEt_blocco.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbEt_blocco.Properties.Appearance.Options.UseBackColor = True
    Me.cbEt_blocco.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_blocco.Properties.DropDownRows = 30
    Me.cbEt_blocco.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_blocco.SelectedValue = ""
    Me.cbEt_blocco.Size = New System.Drawing.Size(136, 20)
    Me.cbEt_blocco.TabIndex = 69
    Me.cbEt_blocco.ValueMember = ""
    '
    'cbEt_sospeso
    '
    Me.cbEt_sospeso.Cursor = System.Windows.Forms.Cursors.Hand
    Me.cbEt_sospeso.DataSource = Nothing
    Me.cbEt_sospeso.DisplayMember = ""
    Me.cbEt_sospeso.Location = New System.Drawing.Point(136, 28)
    Me.cbEt_sospeso.Name = "cbEt_sospeso"
    Me.cbEt_sospeso.NTSDbField = ""
    Me.cbEt_sospeso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_sospeso.Properties.DropDownRows = 30
    Me.cbEt_sospeso.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_sospeso.SelectedValue = ""
    Me.cbEt_sospeso.Size = New System.Drawing.Size(127, 20)
    Me.cbEt_sospeso.TabIndex = 20
    Me.cbEt_sospeso.ValueMember = ""
    '
    'lbBlocco
    '
    Me.lbBlocco.AutoSize = True
    Me.lbBlocco.BackColor = System.Drawing.Color.Transparent
    Me.lbBlocco.Location = New System.Drawing.Point(362, 31)
    Me.lbBlocco.Name = "lbBlocco"
    Me.lbBlocco.NTSDbField = ""
    Me.lbBlocco.Size = New System.Drawing.Size(66, 13)
    Me.lbBlocco.TabIndex = 19
    Me.lbBlocco.Text = "Blocco ordini"
    Me.lbBlocco.Tooltip = ""
    Me.lbBlocco.UseMnemonic = False
    '
    'lbStato
    '
    Me.lbStato.AutoSize = True
    Me.lbStato.BackColor = System.Drawing.Color.Transparent
    Me.lbStato.Location = New System.Drawing.Point(12, 31)
    Me.lbStato.Name = "lbStato"
    Me.lbStato.NTSDbField = ""
    Me.lbStato.Size = New System.Drawing.Size(73, 13)
    Me.lbStato.TabIndex = 18
    Me.lbStato.Text = "Ordini sospesi"
    Me.lbStato.Tooltip = ""
    Me.lbStato.UseMnemonic = False
    '
    'lbCommeca
    '
    Me.lbCommeca.BackColor = System.Drawing.Color.Transparent
    Me.lbCommeca.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbCommeca.Location = New System.Drawing.Point(269, 6)
    Me.lbCommeca.Name = "lbCommeca"
    Me.lbCommeca.NTSDbField = ""
    Me.lbCommeca.Size = New System.Drawing.Size(329, 20)
    Me.lbCommeca.TabIndex = 17
    Me.lbCommeca.Tooltip = ""
    Me.lbCommeca.UseMnemonic = False
    '
    'ckCommeca
    '
    Me.ckCommeca.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckCommeca.Location = New System.Drawing.Point(12, 7)
    Me.ckCommeca.Name = "ckCommeca"
    Me.ckCommeca.NTSCheckValue = "S"
    Me.ckCommeca.NTSUnCheckValue = "N"
    Me.ckCommeca.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckCommeca.Properties.Appearance.Options.UseBackColor = True
    Me.ckCommeca.Properties.Caption = "Filtro &Commessa"
    Me.ckCommeca.Size = New System.Drawing.Size(105, 19)
    Me.ckCommeca.TabIndex = 15
    '
    'edCommeca
    '
    Me.edCommeca.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCommeca.EditValue = "0"
    Me.edCommeca.Location = New System.Drawing.Point(136, 6)
    Me.edCommeca.Name = "edCommeca"
    Me.edCommeca.NTSDbField = ""
    Me.edCommeca.NTSFormat = "0"
    Me.edCommeca.NTSForzaVisZoom = False
    Me.edCommeca.NTSOldValue = ""
    Me.edCommeca.Properties.Appearance.Options.UseTextOptions = True
    Me.edCommeca.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCommeca.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCommeca.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCommeca.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCommeca.Properties.MaxLength = 65536
    Me.edCommeca.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCommeca.Size = New System.Drawing.Size(127, 20)
    Me.edCommeca.TabIndex = 1
    '
    'pnButton
    '
    Me.pnButton.AllowDrop = True
    Me.pnButton.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnButton.Appearance.Options.UseBackColor = True
    Me.pnButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnButton.Controls.Add(Me.cmdApriOrdine)
    Me.pnButton.Controls.Add(Me.cmdRicerca)
    Me.pnButton.Controls.Add(Me.cmdDeseleziona)
    Me.pnButton.Controls.Add(Me.cmdConferma)
    Me.pnButton.Controls.Add(Me.cmdAnnulla)
    Me.pnButton.Controls.Add(Me.cmdSeleziona)
    Me.pnButton.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnButton.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnButton.Location = New System.Drawing.Point(598, 0)
    Me.pnButton.Name = "pnButton"
    Me.pnButton.Size = New System.Drawing.Size(113, 267)
    Me.pnButton.TabIndex = 2
    Me.pnButton.Text = "NtsPanel1"
    '
    'cmdRicerca
    '
    Me.cmdRicerca.ImageText = ""
    Me.cmdRicerca.Location = New System.Drawing.Point(6, 10)
    Me.cmdRicerca.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.Size = New System.Drawing.Size(98, 22)
    Me.cmdRicerca.TabIndex = 13
    Me.cmdRicerca.Text = "&Ricerca"
    '
    'cmdDeseleziona
    '
    Me.cmdDeseleziona.ImageText = ""
    Me.cmdDeseleziona.Location = New System.Drawing.Point(6, 123)
    Me.cmdDeseleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdDeseleziona.Name = "cmdDeseleziona"
    Me.cmdDeseleziona.Size = New System.Drawing.Size(98, 22)
    Me.cmdDeseleziona.TabIndex = 12
    Me.cmdDeseleziona.Text = "&Deseleziona tutto"
    '
    'cmdConferma
    '
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(6, 62)
    Me.cmdConferma.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.Size = New System.Drawing.Size(98, 22)
    Me.cmdConferma.TabIndex = 11
    Me.cmdConferma.Text = "&Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(6, 36)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.Size = New System.Drawing.Size(98, 22)
    Me.cmdAnnulla.TabIndex = 9
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(6, 98)
    Me.cmdSeleziona.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.Size = New System.Drawing.Size(98, 22)
    Me.cmdSeleziona.TabIndex = 8
    Me.cmdSeleziona.Text = "&Seleziona tutto"
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grHltd)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 0)
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.Size = New System.Drawing.Size(598, 267)
    Me.pnGrid.TabIndex = 3
    Me.pnGrid.Text = "NtsPanel1"
    '
    'grHltd
    '
    Me.grHltd.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grHltd.EmbeddedNavigator.Name = ""
    Me.grHltd.Location = New System.Drawing.Point(0, 0)
    Me.grHltd.MainView = Me.grvHltd
    Me.grHltd.Name = "grHltd"
    Me.grHltd.Size = New System.Drawing.Size(598, 267)
    Me.grHltd.TabIndex = 0
    Me.grHltd.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHltd})
    '
    'grvHltd
    '
    Me.grvHltd.ActiveFilterEnabled = False
    Me.grvHltd.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_seleziona, Me.td_tipork, Me.td_anno, Me.td_serie, Me.td_numord, Me.td_datord, Me.td_datcons, Me.td_conto, Me.xx_conto, Me.td_totdoc, Me.td_annpar, Me.td_alfpar, Me.td_numpar, Me.td_datpar, Me.td_riferim, Me.td_commeca, Me.xx_commeca, Me.td_tipobf, Me.xx_tipobf, Me.td_magaz, Me.xx_magaz, Me.td_magaz2, Me.xx_magaz2, Me.td_magimp, Me.xx_magimp, Me.td_coddest, Me.xx_coddest, Me.td_confermato, Me.td_rilasciato, Me.td_aperto, Me.td_sospeso, Me.td_annotco, Me.td_codstag, Me.xx_codstag, Me.td_totmerce, Me.td_flevas})
    Me.grvHltd.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.grvHltd.Enabled = True
    Me.grvHltd.GridControl = Me.grHltd
    Me.grvHltd.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvHltd.Name = "grvHltd"
    Me.grvHltd.NTSAllowDelete = True
    Me.grvHltd.NTSAllowInsert = True
    Me.grvHltd.NTSAllowUpdate = True
    Me.grvHltd.NTSMenuContext = Nothing
    Me.grvHltd.OptionsCustomization.AllowRowSizing = True
    Me.grvHltd.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvHltd.OptionsNavigation.UseTabKey = False
    Me.grvHltd.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvHltd.OptionsView.ColumnAutoWidth = False
    Me.grvHltd.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvHltd.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvHltd.OptionsView.ShowGroupPanel = False
    Me.grvHltd.RowHeight = 14
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
    'td_tipork
    '
    Me.td_tipork.AppearanceCell.Options.UseBackColor = True
    Me.td_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.td_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_tipork.Caption = "tipo ord."
    Me.td_tipork.Enabled = False
    Me.td_tipork.FieldName = "td_tipork"
    Me.td_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_tipork.Name = "td_tipork"
    Me.td_tipork.NTSRepositoryComboBox = Nothing
    Me.td_tipork.NTSRepositoryItemCheck = Nothing
    Me.td_tipork.NTSRepositoryItemMemo = Nothing
    Me.td_tipork.NTSRepositoryItemText = Nothing
    Me.td_tipork.OptionsColumn.AllowEdit = False
    Me.td_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_tipork.OptionsColumn.ReadOnly = True
    Me.td_tipork.OptionsFilter.AllowFilter = False
    Me.td_tipork.Visible = True
    Me.td_tipork.VisibleIndex = 1
    '
    'td_anno
    '
    Me.td_anno.AppearanceCell.Options.UseBackColor = True
    Me.td_anno.AppearanceCell.Options.UseTextOptions = True
    Me.td_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_anno.Caption = "Anno ord."
    Me.td_anno.Enabled = False
    Me.td_anno.FieldName = "td_anno"
    Me.td_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_anno.Name = "td_anno"
    Me.td_anno.NTSRepositoryComboBox = Nothing
    Me.td_anno.NTSRepositoryItemCheck = Nothing
    Me.td_anno.NTSRepositoryItemMemo = Nothing
    Me.td_anno.NTSRepositoryItemText = Nothing
    Me.td_anno.OptionsColumn.AllowEdit = False
    Me.td_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_anno.OptionsColumn.ReadOnly = True
    Me.td_anno.OptionsFilter.AllowFilter = False
    Me.td_anno.Visible = True
    Me.td_anno.VisibleIndex = 2
    '
    'td_serie
    '
    Me.td_serie.AppearanceCell.Options.UseBackColor = True
    Me.td_serie.AppearanceCell.Options.UseTextOptions = True
    Me.td_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_serie.Caption = "Serie ord."
    Me.td_serie.Enabled = False
    Me.td_serie.FieldName = "td_serie"
    Me.td_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_serie.Name = "td_serie"
    Me.td_serie.NTSRepositoryComboBox = Nothing
    Me.td_serie.NTSRepositoryItemCheck = Nothing
    Me.td_serie.NTSRepositoryItemMemo = Nothing
    Me.td_serie.NTSRepositoryItemText = Nothing
    Me.td_serie.OptionsColumn.AllowEdit = False
    Me.td_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_serie.OptionsColumn.ReadOnly = True
    Me.td_serie.OptionsFilter.AllowFilter = False
    Me.td_serie.Visible = True
    Me.td_serie.VisibleIndex = 3
    '
    'td_numord
    '
    Me.td_numord.AppearanceCell.Options.UseBackColor = True
    Me.td_numord.AppearanceCell.Options.UseTextOptions = True
    Me.td_numord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_numord.Caption = "Num. ord."
    Me.td_numord.Enabled = False
    Me.td_numord.FieldName = "td_numord"
    Me.td_numord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_numord.Name = "td_numord"
    Me.td_numord.NTSRepositoryComboBox = Nothing
    Me.td_numord.NTSRepositoryItemCheck = Nothing
    Me.td_numord.NTSRepositoryItemMemo = Nothing
    Me.td_numord.NTSRepositoryItemText = Nothing
    Me.td_numord.OptionsColumn.AllowEdit = False
    Me.td_numord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_numord.OptionsColumn.ReadOnly = True
    Me.td_numord.OptionsFilter.AllowFilter = False
    Me.td_numord.Visible = True
    Me.td_numord.VisibleIndex = 4
    '
    'td_datord
    '
    Me.td_datord.AppearanceCell.Options.UseBackColor = True
    Me.td_datord.AppearanceCell.Options.UseTextOptions = True
    Me.td_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datord.Caption = "Data ord."
    Me.td_datord.Enabled = False
    Me.td_datord.FieldName = "td_datord"
    Me.td_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datord.Name = "td_datord"
    Me.td_datord.NTSRepositoryComboBox = Nothing
    Me.td_datord.NTSRepositoryItemCheck = Nothing
    Me.td_datord.NTSRepositoryItemMemo = Nothing
    Me.td_datord.NTSRepositoryItemText = Nothing
    Me.td_datord.OptionsColumn.AllowEdit = False
    Me.td_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_datord.OptionsColumn.ReadOnly = True
    Me.td_datord.OptionsFilter.AllowFilter = False
    Me.td_datord.Visible = True
    Me.td_datord.VisibleIndex = 5
    '
    'td_datcons
    '
    Me.td_datcons.AppearanceCell.Options.UseBackColor = True
    Me.td_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.td_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datcons.Caption = "Data cons."
    Me.td_datcons.Enabled = False
    Me.td_datcons.FieldName = "td_datcons"
    Me.td_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datcons.Name = "td_datcons"
    Me.td_datcons.NTSRepositoryComboBox = Nothing
    Me.td_datcons.NTSRepositoryItemCheck = Nothing
    Me.td_datcons.NTSRepositoryItemMemo = Nothing
    Me.td_datcons.NTSRepositoryItemText = Nothing
    Me.td_datcons.OptionsColumn.AllowEdit = False
    Me.td_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_datcons.OptionsColumn.ReadOnly = True
    Me.td_datcons.OptionsFilter.AllowFilter = False
    Me.td_datcons.Visible = True
    Me.td_datcons.VisibleIndex = 6
    '
    'td_conto
    '
    Me.td_conto.AppearanceCell.Options.UseBackColor = True
    Me.td_conto.AppearanceCell.Options.UseTextOptions = True
    Me.td_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_conto.Caption = "Conto"
    Me.td_conto.Enabled = False
    Me.td_conto.FieldName = "td_conto"
    Me.td_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_conto.Name = "td_conto"
    Me.td_conto.NTSRepositoryComboBox = Nothing
    Me.td_conto.NTSRepositoryItemCheck = Nothing
    Me.td_conto.NTSRepositoryItemMemo = Nothing
    Me.td_conto.NTSRepositoryItemText = Nothing
    Me.td_conto.OptionsColumn.AllowEdit = False
    Me.td_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_conto.OptionsColumn.ReadOnly = True
    Me.td_conto.OptionsFilter.AllowFilter = False
    Me.td_conto.Visible = True
    Me.td_conto.VisibleIndex = 7
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Descr. Conto"
    Me.xx_conto.Enabled = False
    Me.xx_conto.FieldName = "xx_conto"
    Me.xx_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_conto.Name = "xx_conto"
    Me.xx_conto.NTSRepositoryComboBox = Nothing
    Me.xx_conto.NTSRepositoryItemCheck = Nothing
    Me.xx_conto.NTSRepositoryItemMemo = Nothing
    Me.xx_conto.NTSRepositoryItemText = Nothing
    Me.xx_conto.OptionsColumn.AllowEdit = False
    Me.xx_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_conto.OptionsColumn.ReadOnly = True
    Me.xx_conto.OptionsFilter.AllowFilter = False
    Me.xx_conto.Visible = True
    Me.xx_conto.VisibleIndex = 8
    '
    'td_totdoc
    '
    Me.td_totdoc.AppearanceCell.Options.UseBackColor = True
    Me.td_totdoc.AppearanceCell.Options.UseTextOptions = True
    Me.td_totdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_totdoc.Caption = "Totale ordine"
    Me.td_totdoc.Enabled = False
    Me.td_totdoc.FieldName = "td_totdoc"
    Me.td_totdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_totdoc.Name = "td_totdoc"
    Me.td_totdoc.NTSRepositoryComboBox = Nothing
    Me.td_totdoc.NTSRepositoryItemCheck = Nothing
    Me.td_totdoc.NTSRepositoryItemMemo = Nothing
    Me.td_totdoc.NTSRepositoryItemText = Nothing
    Me.td_totdoc.OptionsColumn.AllowEdit = False
    Me.td_totdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_totdoc.OptionsColumn.ReadOnly = True
    Me.td_totdoc.OptionsFilter.AllowFilter = False
    Me.td_totdoc.Visible = True
    Me.td_totdoc.VisibleIndex = 9
    '
    'td_annpar
    '
    Me.td_annpar.AppearanceCell.Options.UseBackColor = True
    Me.td_annpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_annpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_annpar.Caption = "Anno P."
    Me.td_annpar.Enabled = False
    Me.td_annpar.FieldName = "td_annpar"
    Me.td_annpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_annpar.Name = "td_annpar"
    Me.td_annpar.NTSRepositoryComboBox = Nothing
    Me.td_annpar.NTSRepositoryItemCheck = Nothing
    Me.td_annpar.NTSRepositoryItemMemo = Nothing
    Me.td_annpar.NTSRepositoryItemText = Nothing
    Me.td_annpar.OptionsColumn.AllowEdit = False
    Me.td_annpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_annpar.OptionsColumn.ReadOnly = True
    Me.td_annpar.OptionsFilter.AllowFilter = False
    Me.td_annpar.Visible = True
    Me.td_annpar.VisibleIndex = 10
    '
    'td_alfpar
    '
    Me.td_alfpar.AppearanceCell.Options.UseBackColor = True
    Me.td_alfpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_alfpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_alfpar.Caption = "Serie P."
    Me.td_alfpar.Enabled = False
    Me.td_alfpar.FieldName = "td_alfpar"
    Me.td_alfpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_alfpar.Name = "td_alfpar"
    Me.td_alfpar.NTSRepositoryComboBox = Nothing
    Me.td_alfpar.NTSRepositoryItemCheck = Nothing
    Me.td_alfpar.NTSRepositoryItemMemo = Nothing
    Me.td_alfpar.NTSRepositoryItemText = Nothing
    Me.td_alfpar.OptionsColumn.AllowEdit = False
    Me.td_alfpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_alfpar.OptionsColumn.ReadOnly = True
    Me.td_alfpar.OptionsFilter.AllowFilter = False
    Me.td_alfpar.Visible = True
    Me.td_alfpar.VisibleIndex = 11
    '
    'td_numpar
    '
    Me.td_numpar.AppearanceCell.Options.UseBackColor = True
    Me.td_numpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_numpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_numpar.Caption = "Num. P."
    Me.td_numpar.Enabled = False
    Me.td_numpar.FieldName = "td_numpar"
    Me.td_numpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_numpar.Name = "td_numpar"
    Me.td_numpar.NTSRepositoryComboBox = Nothing
    Me.td_numpar.NTSRepositoryItemCheck = Nothing
    Me.td_numpar.NTSRepositoryItemMemo = Nothing
    Me.td_numpar.NTSRepositoryItemText = Nothing
    Me.td_numpar.OptionsColumn.AllowEdit = False
    Me.td_numpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_numpar.OptionsColumn.ReadOnly = True
    Me.td_numpar.OptionsFilter.AllowFilter = False
    Me.td_numpar.Visible = True
    Me.td_numpar.VisibleIndex = 12
    '
    'td_datpar
    '
    Me.td_datpar.AppearanceCell.Options.UseBackColor = True
    Me.td_datpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_datpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datpar.Caption = "Data P."
    Me.td_datpar.Enabled = False
    Me.td_datpar.FieldName = "td_datpar"
    Me.td_datpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datpar.Name = "td_datpar"
    Me.td_datpar.NTSRepositoryComboBox = Nothing
    Me.td_datpar.NTSRepositoryItemCheck = Nothing
    Me.td_datpar.NTSRepositoryItemMemo = Nothing
    Me.td_datpar.NTSRepositoryItemText = Nothing
    Me.td_datpar.OptionsColumn.AllowEdit = False
    Me.td_datpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_datpar.OptionsColumn.ReadOnly = True
    Me.td_datpar.OptionsFilter.AllowFilter = False
    Me.td_datpar.Visible = True
    Me.td_datpar.VisibleIndex = 13
    '
    'td_riferim
    '
    Me.td_riferim.AppearanceCell.Options.UseBackColor = True
    Me.td_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.td_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_riferim.Caption = "Riferimenti"
    Me.td_riferim.Enabled = False
    Me.td_riferim.FieldName = "td_riferim"
    Me.td_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_riferim.Name = "td_riferim"
    Me.td_riferim.NTSRepositoryComboBox = Nothing
    Me.td_riferim.NTSRepositoryItemCheck = Nothing
    Me.td_riferim.NTSRepositoryItemMemo = Nothing
    Me.td_riferim.NTSRepositoryItemText = Nothing
    Me.td_riferim.OptionsColumn.AllowEdit = False
    Me.td_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_riferim.OptionsColumn.ReadOnly = True
    Me.td_riferim.OptionsFilter.AllowFilter = False
    Me.td_riferim.Visible = True
    Me.td_riferim.VisibleIndex = 14
    '
    'td_commeca
    '
    Me.td_commeca.AppearanceCell.Options.UseBackColor = True
    Me.td_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.td_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_commeca.Caption = "Commessa"
    Me.td_commeca.Enabled = False
    Me.td_commeca.FieldName = "td_commeca"
    Me.td_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_commeca.Name = "td_commeca"
    Me.td_commeca.NTSRepositoryComboBox = Nothing
    Me.td_commeca.NTSRepositoryItemCheck = Nothing
    Me.td_commeca.NTSRepositoryItemMemo = Nothing
    Me.td_commeca.NTSRepositoryItemText = Nothing
    Me.td_commeca.OptionsColumn.AllowEdit = False
    Me.td_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_commeca.OptionsColumn.ReadOnly = True
    Me.td_commeca.OptionsFilter.AllowFilter = False
    Me.td_commeca.Visible = True
    Me.td_commeca.VisibleIndex = 15
    '
    'xx_commeca
    '
    Me.xx_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xx_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xx_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_commeca.Caption = "Descr. commessa"
    Me.xx_commeca.Enabled = False
    Me.xx_commeca.FieldName = "xx_commeca"
    Me.xx_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_commeca.Name = "xx_commeca"
    Me.xx_commeca.NTSRepositoryComboBox = Nothing
    Me.xx_commeca.NTSRepositoryItemCheck = Nothing
    Me.xx_commeca.NTSRepositoryItemMemo = Nothing
    Me.xx_commeca.NTSRepositoryItemText = Nothing
    Me.xx_commeca.OptionsColumn.AllowEdit = False
    Me.xx_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_commeca.OptionsColumn.ReadOnly = True
    Me.xx_commeca.OptionsFilter.AllowFilter = False
    Me.xx_commeca.Visible = True
    Me.xx_commeca.VisibleIndex = 16
    '
    'td_tipobf
    '
    Me.td_tipobf.AppearanceCell.Options.UseBackColor = True
    Me.td_tipobf.AppearanceCell.Options.UseTextOptions = True
    Me.td_tipobf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_tipobf.Caption = "Tipo BF"
    Me.td_tipobf.Enabled = False
    Me.td_tipobf.FieldName = "td_tipobf"
    Me.td_tipobf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_tipobf.Name = "td_tipobf"
    Me.td_tipobf.NTSRepositoryComboBox = Nothing
    Me.td_tipobf.NTSRepositoryItemCheck = Nothing
    Me.td_tipobf.NTSRepositoryItemMemo = Nothing
    Me.td_tipobf.NTSRepositoryItemText = Nothing
    Me.td_tipobf.OptionsColumn.AllowEdit = False
    Me.td_tipobf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_tipobf.OptionsColumn.ReadOnly = True
    Me.td_tipobf.OptionsFilter.AllowFilter = False
    Me.td_tipobf.Visible = True
    Me.td_tipobf.VisibleIndex = 17
    '
    'xx_tipobf
    '
    Me.xx_tipobf.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipobf.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipobf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipobf.Caption = "Descr. tipo BF"
    Me.xx_tipobf.Enabled = False
    Me.xx_tipobf.FieldName = "xx_tipobf"
    Me.xx_tipobf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tipobf.Name = "xx_tipobf"
    Me.xx_tipobf.NTSRepositoryComboBox = Nothing
    Me.xx_tipobf.NTSRepositoryItemCheck = Nothing
    Me.xx_tipobf.NTSRepositoryItemMemo = Nothing
    Me.xx_tipobf.NTSRepositoryItemText = Nothing
    Me.xx_tipobf.OptionsColumn.AllowEdit = False
    Me.xx_tipobf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tipobf.OptionsColumn.ReadOnly = True
    Me.xx_tipobf.OptionsFilter.AllowFilter = False
    Me.xx_tipobf.Visible = True
    Me.xx_tipobf.VisibleIndex = 18
    '
    'td_magaz
    '
    Me.td_magaz.AppearanceCell.Options.UseBackColor = True
    Me.td_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.td_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_magaz.Caption = "Magazzino"
    Me.td_magaz.Enabled = False
    Me.td_magaz.FieldName = "td_magaz"
    Me.td_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_magaz.Name = "td_magaz"
    Me.td_magaz.NTSRepositoryComboBox = Nothing
    Me.td_magaz.NTSRepositoryItemCheck = Nothing
    Me.td_magaz.NTSRepositoryItemMemo = Nothing
    Me.td_magaz.NTSRepositoryItemText = Nothing
    Me.td_magaz.OptionsColumn.AllowEdit = False
    Me.td_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_magaz.OptionsColumn.ReadOnly = True
    Me.td_magaz.OptionsFilter.AllowFilter = False
    Me.td_magaz.Visible = True
    Me.td_magaz.VisibleIndex = 19
    '
    'xx_magaz
    '
    Me.xx_magaz.AppearanceCell.Options.UseBackColor = True
    Me.xx_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magaz.Caption = "Descr. magazzino"
    Me.xx_magaz.Enabled = False
    Me.xx_magaz.FieldName = "xx_magaz"
    Me.xx_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magaz.Name = "xx_magaz"
    Me.xx_magaz.NTSRepositoryComboBox = Nothing
    Me.xx_magaz.NTSRepositoryItemCheck = Nothing
    Me.xx_magaz.NTSRepositoryItemMemo = Nothing
    Me.xx_magaz.NTSRepositoryItemText = Nothing
    Me.xx_magaz.OptionsColumn.AllowEdit = False
    Me.xx_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_magaz.OptionsColumn.ReadOnly = True
    Me.xx_magaz.OptionsFilter.AllowFilter = False
    Me.xx_magaz.Visible = True
    Me.xx_magaz.VisibleIndex = 20
    '
    'td_magaz2
    '
    Me.td_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.td_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.td_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_magaz2.Caption = "Magaz. 2"
    Me.td_magaz2.Enabled = False
    Me.td_magaz2.FieldName = "td_magaz2"
    Me.td_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_magaz2.Name = "td_magaz2"
    Me.td_magaz2.NTSRepositoryComboBox = Nothing
    Me.td_magaz2.NTSRepositoryItemCheck = Nothing
    Me.td_magaz2.NTSRepositoryItemMemo = Nothing
    Me.td_magaz2.NTSRepositoryItemText = Nothing
    Me.td_magaz2.OptionsColumn.AllowEdit = False
    Me.td_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_magaz2.OptionsColumn.ReadOnly = True
    Me.td_magaz2.OptionsFilter.AllowFilter = False
    Me.td_magaz2.Visible = True
    Me.td_magaz2.VisibleIndex = 21
    '
    'xx_magaz2
    '
    Me.xx_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.xx_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magaz2.Caption = "Descr. magaz. 2"
    Me.xx_magaz2.Enabled = False
    Me.xx_magaz2.FieldName = "xx_magaz2"
    Me.xx_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magaz2.Name = "xx_magaz2"
    Me.xx_magaz2.NTSRepositoryComboBox = Nothing
    Me.xx_magaz2.NTSRepositoryItemCheck = Nothing
    Me.xx_magaz2.NTSRepositoryItemMemo = Nothing
    Me.xx_magaz2.NTSRepositoryItemText = Nothing
    Me.xx_magaz2.OptionsColumn.AllowEdit = False
    Me.xx_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_magaz2.OptionsColumn.ReadOnly = True
    Me.xx_magaz2.OptionsFilter.AllowFilter = False
    Me.xx_magaz2.Visible = True
    Me.xx_magaz2.VisibleIndex = 22
    '
    'td_magimp
    '
    Me.td_magimp.AppearanceCell.Options.UseBackColor = True
    Me.td_magimp.AppearanceCell.Options.UseTextOptions = True
    Me.td_magimp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_magimp.Caption = "Magaz. impegni"
    Me.td_magimp.Enabled = False
    Me.td_magimp.FieldName = "td_magimp"
    Me.td_magimp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_magimp.Name = "td_magimp"
    Me.td_magimp.NTSRepositoryComboBox = Nothing
    Me.td_magimp.NTSRepositoryItemCheck = Nothing
    Me.td_magimp.NTSRepositoryItemMemo = Nothing
    Me.td_magimp.NTSRepositoryItemText = Nothing
    Me.td_magimp.OptionsColumn.AllowEdit = False
    Me.td_magimp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_magimp.OptionsColumn.ReadOnly = True
    Me.td_magimp.OptionsFilter.AllowFilter = False
    Me.td_magimp.Visible = True
    Me.td_magimp.VisibleIndex = 23
    '
    'xx_magimp
    '
    Me.xx_magimp.AppearanceCell.Options.UseBackColor = True
    Me.xx_magimp.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magimp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magimp.Caption = "Descr. magaz. imp."
    Me.xx_magimp.Enabled = False
    Me.xx_magimp.FieldName = "xx_magimp"
    Me.xx_magimp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magimp.Name = "xx_magimp"
    Me.xx_magimp.NTSRepositoryComboBox = Nothing
    Me.xx_magimp.NTSRepositoryItemCheck = Nothing
    Me.xx_magimp.NTSRepositoryItemMemo = Nothing
    Me.xx_magimp.NTSRepositoryItemText = Nothing
    Me.xx_magimp.OptionsColumn.AllowEdit = False
    Me.xx_magimp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_magimp.OptionsColumn.ReadOnly = True
    Me.xx_magimp.OptionsFilter.AllowFilter = False
    Me.xx_magimp.Visible = True
    Me.xx_magimp.VisibleIndex = 24
    '
    'td_coddest
    '
    Me.td_coddest.AppearanceCell.Options.UseBackColor = True
    Me.td_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.td_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_coddest.Caption = "Cod. destinaz."
    Me.td_coddest.Enabled = False
    Me.td_coddest.FieldName = "td_coddest"
    Me.td_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_coddest.Name = "td_coddest"
    Me.td_coddest.NTSRepositoryComboBox = Nothing
    Me.td_coddest.NTSRepositoryItemCheck = Nothing
    Me.td_coddest.NTSRepositoryItemMemo = Nothing
    Me.td_coddest.NTSRepositoryItemText = Nothing
    Me.td_coddest.OptionsColumn.AllowEdit = False
    Me.td_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_coddest.OptionsColumn.ReadOnly = True
    Me.td_coddest.OptionsFilter.AllowFilter = False
    Me.td_coddest.Visible = True
    Me.td_coddest.VisibleIndex = 25
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Descr. destinaz."
    Me.xx_coddest.Enabled = False
    Me.xx_coddest.FieldName = "xx_coddest"
    Me.xx_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_coddest.Name = "xx_coddest"
    Me.xx_coddest.NTSRepositoryComboBox = Nothing
    Me.xx_coddest.NTSRepositoryItemCheck = Nothing
    Me.xx_coddest.NTSRepositoryItemMemo = Nothing
    Me.xx_coddest.NTSRepositoryItemText = Nothing
    Me.xx_coddest.OptionsColumn.AllowEdit = False
    Me.xx_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_coddest.OptionsColumn.ReadOnly = True
    Me.xx_coddest.OptionsFilter.AllowFilter = False
    Me.xx_coddest.Visible = True
    Me.xx_coddest.VisibleIndex = 26
    '
    'td_confermato
    '
    Me.td_confermato.AppearanceCell.Options.UseBackColor = True
    Me.td_confermato.AppearanceCell.Options.UseTextOptions = True
    Me.td_confermato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_confermato.Caption = "Confermato"
    Me.td_confermato.Enabled = False
    Me.td_confermato.FieldName = "td_confermato"
    Me.td_confermato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_confermato.Name = "td_confermato"
    Me.td_confermato.NTSRepositoryComboBox = Nothing
    Me.td_confermato.NTSRepositoryItemCheck = Nothing
    Me.td_confermato.NTSRepositoryItemMemo = Nothing
    Me.td_confermato.NTSRepositoryItemText = Nothing
    Me.td_confermato.OptionsColumn.AllowEdit = False
    Me.td_confermato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_confermato.OptionsColumn.ReadOnly = True
    Me.td_confermato.OptionsFilter.AllowFilter = False
    Me.td_confermato.Visible = True
    Me.td_confermato.VisibleIndex = 27
    '
    'td_rilasciato
    '
    Me.td_rilasciato.AppearanceCell.Options.UseBackColor = True
    Me.td_rilasciato.AppearanceCell.Options.UseTextOptions = True
    Me.td_rilasciato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_rilasciato.Caption = "Rilasciato"
    Me.td_rilasciato.Enabled = False
    Me.td_rilasciato.FieldName = "td_rilasciato"
    Me.td_rilasciato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_rilasciato.Name = "td_rilasciato"
    Me.td_rilasciato.NTSRepositoryComboBox = Nothing
    Me.td_rilasciato.NTSRepositoryItemCheck = Nothing
    Me.td_rilasciato.NTSRepositoryItemMemo = Nothing
    Me.td_rilasciato.NTSRepositoryItemText = Nothing
    Me.td_rilasciato.OptionsColumn.AllowEdit = False
    Me.td_rilasciato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_rilasciato.OptionsColumn.ReadOnly = True
    Me.td_rilasciato.OptionsFilter.AllowFilter = False
    Me.td_rilasciato.Visible = True
    Me.td_rilasciato.VisibleIndex = 28
    '
    'td_aperto
    '
    Me.td_aperto.AppearanceCell.Options.UseBackColor = True
    Me.td_aperto.AppearanceCell.Options.UseTextOptions = True
    Me.td_aperto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_aperto.Caption = "Aperto"
    Me.td_aperto.Enabled = False
    Me.td_aperto.FieldName = "td_aperto"
    Me.td_aperto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_aperto.Name = "td_aperto"
    Me.td_aperto.NTSRepositoryComboBox = Nothing
    Me.td_aperto.NTSRepositoryItemCheck = Nothing
    Me.td_aperto.NTSRepositoryItemMemo = Nothing
    Me.td_aperto.NTSRepositoryItemText = Nothing
    Me.td_aperto.OptionsColumn.AllowEdit = False
    Me.td_aperto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_aperto.OptionsColumn.ReadOnly = True
    Me.td_aperto.OptionsFilter.AllowFilter = False
    Me.td_aperto.Visible = True
    Me.td_aperto.VisibleIndex = 29
    '
    'td_sospeso
    '
    Me.td_sospeso.AppearanceCell.Options.UseBackColor = True
    Me.td_sospeso.AppearanceCell.Options.UseTextOptions = True
    Me.td_sospeso.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_sospeso.Caption = "Sospeso"
    Me.td_sospeso.Enabled = False
    Me.td_sospeso.FieldName = "td_sospeso"
    Me.td_sospeso.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_sospeso.Name = "td_sospeso"
    Me.td_sospeso.NTSRepositoryComboBox = Nothing
    Me.td_sospeso.NTSRepositoryItemCheck = Nothing
    Me.td_sospeso.NTSRepositoryItemMemo = Nothing
    Me.td_sospeso.NTSRepositoryItemText = Nothing
    Me.td_sospeso.OptionsColumn.AllowEdit = False
    Me.td_sospeso.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_sospeso.OptionsColumn.ReadOnly = True
    Me.td_sospeso.OptionsFilter.AllowFilter = False
    Me.td_sospeso.Visible = True
    Me.td_sospeso.VisibleIndex = 30
    '
    'td_annotco
    '
    Me.td_annotco.AppearanceCell.Options.UseBackColor = True
    Me.td_annotco.AppearanceCell.Options.UseTextOptions = True
    Me.td_annotco.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_annotco.Caption = "Anno TC"
    Me.td_annotco.Enabled = False
    Me.td_annotco.FieldName = "td_annotco"
    Me.td_annotco.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_annotco.Name = "td_annotco"
    Me.td_annotco.NTSRepositoryComboBox = Nothing
    Me.td_annotco.NTSRepositoryItemCheck = Nothing
    Me.td_annotco.NTSRepositoryItemMemo = Nothing
    Me.td_annotco.NTSRepositoryItemText = Nothing
    Me.td_annotco.OptionsColumn.AllowEdit = False
    Me.td_annotco.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_annotco.OptionsColumn.ReadOnly = True
    Me.td_annotco.OptionsFilter.AllowFilter = False
    Me.td_annotco.Visible = True
    Me.td_annotco.VisibleIndex = 31
    '
    'td_codstag
    '
    Me.td_codstag.AppearanceCell.Options.UseBackColor = True
    Me.td_codstag.AppearanceCell.Options.UseTextOptions = True
    Me.td_codstag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_codstag.Caption = "Stagione"
    Me.td_codstag.Enabled = False
    Me.td_codstag.FieldName = "td_codstag"
    Me.td_codstag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_codstag.Name = "td_codstag"
    Me.td_codstag.NTSRepositoryComboBox = Nothing
    Me.td_codstag.NTSRepositoryItemCheck = Nothing
    Me.td_codstag.NTSRepositoryItemMemo = Nothing
    Me.td_codstag.NTSRepositoryItemText = Nothing
    Me.td_codstag.OptionsColumn.AllowEdit = False
    Me.td_codstag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_codstag.OptionsColumn.ReadOnly = True
    Me.td_codstag.OptionsFilter.AllowFilter = False
    Me.td_codstag.Visible = True
    Me.td_codstag.VisibleIndex = 32
    '
    'xx_codstag
    '
    Me.xx_codstag.AppearanceCell.Options.UseBackColor = True
    Me.xx_codstag.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codstag.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codstag.Caption = "Descr. stagione"
    Me.xx_codstag.Enabled = False
    Me.xx_codstag.FieldName = "xx_codstag"
    Me.xx_codstag.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codstag.Name = "xx_codstag"
    Me.xx_codstag.NTSRepositoryComboBox = Nothing
    Me.xx_codstag.NTSRepositoryItemCheck = Nothing
    Me.xx_codstag.NTSRepositoryItemMemo = Nothing
    Me.xx_codstag.NTSRepositoryItemText = Nothing
    Me.xx_codstag.OptionsColumn.AllowEdit = False
    Me.xx_codstag.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codstag.OptionsColumn.ReadOnly = True
    Me.xx_codstag.OptionsFilter.AllowFilter = False
    Me.xx_codstag.Visible = True
    Me.xx_codstag.VisibleIndex = 33
    '
    'td_totmerce
    '
    Me.td_totmerce.AppearanceCell.Options.UseBackColor = True
    Me.td_totmerce.AppearanceCell.Options.UseTextOptions = True
    Me.td_totmerce.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_totmerce.Caption = "Totale merce"
    Me.td_totmerce.Enabled = False
    Me.td_totmerce.FieldName = "td_totmerce"
    Me.td_totmerce.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_totmerce.Name = "td_totmerce"
    Me.td_totmerce.NTSRepositoryComboBox = Nothing
    Me.td_totmerce.NTSRepositoryItemCheck = Nothing
    Me.td_totmerce.NTSRepositoryItemMemo = Nothing
    Me.td_totmerce.NTSRepositoryItemText = Nothing
    Me.td_totmerce.OptionsColumn.AllowEdit = False
    Me.td_totmerce.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_totmerce.OptionsColumn.ReadOnly = True
    Me.td_totmerce.OptionsFilter.AllowFilter = False
    Me.td_totmerce.Visible = True
    Me.td_totmerce.VisibleIndex = 34
    '
    'td_flevas
    '
    Me.td_flevas.AppearanceCell.Options.UseBackColor = True
    Me.td_flevas.AppearanceCell.Options.UseTextOptions = True
    Me.td_flevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_flevas.Caption = "Evaso"
    Me.td_flevas.Enabled = False
    Me.td_flevas.FieldName = "td_flevas"
    Me.td_flevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_flevas.Name = "td_flevas"
    Me.td_flevas.NTSRepositoryComboBox = Nothing
    Me.td_flevas.NTSRepositoryItemCheck = Nothing
    Me.td_flevas.NTSRepositoryItemMemo = Nothing
    Me.td_flevas.NTSRepositoryItemText = Nothing
    Me.td_flevas.OptionsColumn.AllowEdit = False
    Me.td_flevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.td_flevas.OptionsColumn.ReadOnly = True
    Me.td_flevas.OptionsFilter.AllowFilter = False
    Me.td_flevas.Visible = True
    Me.td_flevas.VisibleIndex = 35
    '
    'cmdApriOrdine
    '
    Me.cmdApriOrdine.ImageText = ""
    Me.cmdApriOrdine.Location = New System.Drawing.Point(6, 239)
    Me.cmdApriOrdine.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdApriOrdine.Name = "cmdApriOrdine"
    Me.cmdApriOrdine.Size = New System.Drawing.Size(98, 22)
    Me.cmdApriOrdine.TabIndex = 19
    Me.cmdApriOrdine.Text = "Apri &Ordine"
    '
    'FRMORHLTD
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(711, 320)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnButton)
    Me.Controls.Add(Me.pnBottom)
    Me.MinimizeBox = False
    Me.Name = "FRMORHLTD"
    Me.Text = "SELEZIONE TESTATE ORDINI/IMPEGNI"
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    Me.pnBottom.PerformLayout()
    CType(Me.cbEt_blocco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbEt_sospeso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckCommeca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCommeca.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnButton.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grHltd, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvHltd, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub


  Public Overridable Sub FRMORHLTD_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      CaricaCombo()

      GctlSetRoules()
      GctlApplicaDefaultValue()

      oCleHltd.strDittaCorrente = DittaCorrente

      bAbilitaStart = True
      oCleHltd.nTipologia = oParam.nTipologia
      cmdRicerca_Click(Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcuni controlli
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        td_confermato.Visible = False
        td_commeca.Visible = False
        xx_commeca.Visible = False
        td_annotco.Visible = False
        td_codstag.Visible = False
        xx_codstag.Visible = False
        ckCommeca.Visible = False
        edCommeca.Visible = False
        lbCommeca.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub


  Public Overridable Function CaricaCombo() As Boolean
    Dim dttBlocco As New DataTable
    Dim dttSospeso As New DataTable
    Try
      dttBlocco.Columns.Add("cod", GetType(String))
      dttBlocco.Columns.Add("val", GetType(String))
      dttBlocco.Rows.Add(New Object() {"*", "Tutti"})
      dttBlocco.Rows.Add(New Object() {"N", "Nessun blocco"})
      dttBlocco.Rows.Add(New Object() {"B", "Blocco fisso"})
      dttBlocco.Rows.Add(New Object() {"F", "Blocco fuori Fido"})
      dttBlocco.Rows.Add(New Object() {"I", "Blocco insoluti"})
      dttBlocco.Rows.Add(New Object() {"R", "Blocco RD Scadute"})
      dttBlocco.AcceptChanges()
      cbEt_blocco.DataSource = dttBlocco
      cbEt_blocco.ValueMember = "cod"
      cbEt_blocco.DisplayMember = "val"

      dttSospeso.Columns.Add("cod", GetType(String))
      dttSospeso.Columns.Add("val", GetType(String))
      dttSospeso.Rows.Add(New Object() {"*", "Tutti"})
      dttSospeso.Rows.Add(New Object() {"S", "Ordini sospesi"})
      dttSospeso.Rows.Add(New Object() {"N", "Ordini non sospesi"})
      dttSospeso.AcceptChanges()
      cbEt_sospeso.DataSource = dttSospeso
      cbEt_sospeso.ValueMember = "cod"
      cbEt_sospeso.DisplayMember = "val"

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


#Region "CommandButton"
  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Dim strQuery As String
    Try
      If bAbilitaStart = False Then Return

      dsHltd.Clear()

      'parametri passati dal chiamante
      'oPar.strDescr = oCle__Cons.GetWhereHltd()
      'oPar.nTipologia = 0       '0 = posso selez una sola riga, 1 posso fare multiselezione
      'oPar.nMastro = NTSCInt(IIf(strCodart <> "", 3, 1))   'colonne di bsORHLTD da visualizzare (in vb6 oPar.nMastro)

      If oParam.nTipologia = 0 Then
        cmdSeleziona.Visible = False
        cmdDeseleziona.Visible = False
      Else
        GctlSetVisEnab(cmdSeleziona, True)
        GctlSetVisEnab(cmdDeseleziona, True)
      End If

      Me.Cursor = Cursors.WaitCursor
      strQuery = oParam.strDescr.Replace("?", "_").Replace("*", "%")
      If cbEt_blocco.SelectedValue <> "*" Then strQuery &= " AND td_blocco = " & CStrSQL(cbEt_blocco.SelectedValue)
      If cbEt_sospeso.SelectedValue <> "*" Then strQuery &= " AND td_sospeso = " & CStrSQL(cbEt_sospeso.SelectedValue)

      If Not oCleHltd.GetData(NTSCInt(IIf(ckCommeca.Checked, edCommeca.Text, 0)), strQuery, dsHltd) Then
        Me.Close()
        Return
      End If
      dcHltd.DataSource = dsHltd.Tables("TESTORD")
      dsHltd.AcceptChanges()
      grHltd.DataSource = dcHltd

      'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valore
      If grvHltd.RowCount > 0 Then
        grHltd.Focus()
        grvHltd.MoveFirst()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      If Not Salva() Then Return

      dsHltd.Tables("TESTORD").AcceptChanges()

      dtrT = dsHltd.Tables("TESTORD").Select("xx_seleziona = 'N'")
      If dtrT.Length = dsHltd.Tables("TESTORD").Rows.Count Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129137222630555063, "Selezionare almeno una riga prima di confermare."))
        Return
      End If
      For i = 0 To dtrT.Length - 1
        dtrT(i).Delete()
      Next
      dsHltd.Tables("TESTORD").AcceptChanges()
      oParam.oParam = dsHltd.Tables("TESTORD")      'ritorno le righe selezionate

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oParam.oParam = Nothing
    Me.Close()
  End Sub

  Public Overridable Sub cmdSeleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    Dim strFilter As String = ""
    Dim dtraFilteredRows As DataRow() = Nothing
    Try
      If grvHltd.ActiveFilterEnabled Then
        strFilter = grvHltd.ActiveFilterString
      End If

      dtraFilteredRows = dsHltd.Tables("TESTORD").Select(strFilter)
      For Each dtrRow As DataRow In dtraFilteredRows
        dtrRow!xx_seleziona = "S"
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdDeseleziona_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeseleziona.Click
    Try
      For Each dtrRow As DataRow In dsHltd.Tables("TESTORD").Rows
        dtrRow!xx_seleziona = "N"
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdApriOrdine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdApriOrdine.Click
    Dim strTipork As String = ""
    Dim nAnno As Integer = 0
    Dim strSerie As String = ""
    Dim lNumord As Integer = 0
    Dim lRigaOut As Integer = 0
    Dim strParam As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non esistono righe, esce
      '--------------------------------------------------------------------------------------------------------------
      If grvHltd.NTSGetCurrentDataRow() Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      strTipork = NTSCStr(grvHltd.NTSGetCurrentDataRow()!td_tipork)
      If strTipork = "Y" Then strTipork = "H"
      nAnno = NTSCInt(grvHltd.NTSGetCurrentDataRow()!td_anno)
      strSerie = NTSCStr(grvHltd.NTSGetCurrentDataRow()!td_serie)
      lNumord = NTSCInt(grvHltd.NTSGetCurrentDataRow()!td_numord)
      '--------------------------------------------------------------------------------------------------------------
      '--- Chiama 'Gesitone Ordini/Impegni' in modale
      '--------------------------------------------------------------------------------------------------------------
      strParam = "APRI;" & strTipork & ";" & nAnno & ";" & strSerie & ";" & lNumord.ToString("000000000") & ";"
      oMenu.RunChild("BSORGSOR", "CLSORGSOR", oApp.Tr(Me, 129665133528349610, "Gestione ordini/impegno"), DittaCorrente, "", "", Nothing, strParam, True, True)
      '--------------------------------------------------------------------------------------------------------------
      '--- Al ritorno di 'Gestione Ordini/Impegni', fa il refresh dei dati visualizzati in griglia
      '--------------------------------------------------------------------------------------------------------------
      cmdRicerca_Click(Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
      '--- Se non esistono righe, esce
      '--------------------------------------------------------------------------------------------------------------
      If dcHltd.List.Count = 0 Then Return
      '--------------------------------------------------------------------------------------------------------------
      '--- Se, dopo il refresh di griglia, la riga esiste ancora, ci si posiziona
      '--- altrimenti si posiziona sulla prima riga
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To dcHltd.List.Count - 1
        With CType(dcHltd.Item(i), DataRowView)
          If NTSCStr(!td_tipork) = strTipork And NTSCInt(!td_anno) = nAnno And _
             NTSCStr(!td_serie) = strSerie And NTSCInt(!td_numord) = lNumord Then
            dcHltd.Position = i
            Exit For
          End If
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub grvHltd_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvHltd.NTSBeforeRowUpdate
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

  Public Overridable Sub edCommeca_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCommeca.Validated
    Dim strTmp As String = ""
    Try

      If Not oCleHltd.edCommeca_Validated(NTSCInt(edCommeca.Text), strTmp) Then
        edCommeca.Text = NTSCStr(edCommeca.OldEditValue)
      Else
        lbCommeca.Text = strTmp
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

      If GctlControllaOutNotEqual() = False Then Return False

      If Not oCleHltd.Salva() Then
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function
End Class
