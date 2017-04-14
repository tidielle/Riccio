Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEHLBC
  Public oCleDocu As CLEMGDOCU
  Public oParam As CLE__PATB       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore
  Public dsHlbc As New DataSet
  Public dcHlbc As BindingSource = New BindingSource()

  Public WithEvents pnButton As NTSInformatica.NTSPanel
  Public WithEvents pnGrid As NTSInformatica.NTSPanel
  Public WithEvents grHlbc As NTSInformatica.NTSGrid
  Public WithEvents grvHlbc As NTSInformatica.NTSGridView
  Public WithEvents NtsGridView1 As NTSInformatica.NTSGridView
  Public WithEvents cmdTerminale As NTSInformatica.NTSButton
  Public WithEvents cmdStart As NTSInformatica.NTSButton
  Public WithEvents cmdConferma As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents xx_status As NTSInformatica.NTSGridColumn
  Public WithEvents xx_conto As NTSInformatica.NTSGridColumn
  Public WithEvents mo_codart As NTSInformatica.NTSGridColumn
  Public WithEvents mo_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents mo_fase As NTSInformatica.NTSGridColumn
  Public WithEvents mo_tipork As NTSInformatica.NTSGridColumn
  Public WithEvents mo_anno As NTSInformatica.NTSGridColumn
  Public WithEvents mo_serie As NTSInformatica.NTSGridColumn
  Public WithEvents mo_numord As NTSInformatica.NTSGridColumn
  Public WithEvents mo_riga As NTSInformatica.NTSGridColumn
  Public WithEvents xx_datord As NTSInformatica.NTSGridColumn
  Public WithEvents mo_datcons As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quant As NTSInformatica.NTSGridColumn
  Public WithEvents mo_quaeva As NTSInformatica.NTSGridColumn
  Public WithEvents xx_flevasdaeva As NTSInformatica.NTSGridColumn
  Public WithEvents xx_qtares As NTSInformatica.NTSGridColumn
  Public WithEvents xx_magaz As NTSInformatica.NTSGridColumn
  Public WithEvents xx_riferim As NTSInformatica.NTSGridColumn
  Public WithEvents mo_note As NTSInformatica.NTSGridColumn
  Public WithEvents mo_prezzo As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont1 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont2 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont3 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont4 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont5 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_scont6 As NTSInformatica.NTSGridColumn
  Public WithEvents mo_commeca As NTSInformatica.NTSGridColumn
  Public WithEvents mo_subcommeca As NTSInformatica.NTSGridColumn
  Public WithEvents xx_codpaga As NTSInformatica.NTSGridColumn
  Public WithEvents xx_coddest As NTSInformatica.NTSGridColumn
  Public WithEvents mo_descr As NTSInformatica.NTSGridColumn
  Public WithEvents cmdStartdelete As NTSInformatica.NTSButton
  Public WithEvents pnBottom As NTSInformatica.NTSPanel
  Public WithEvents cmdCancellaRiga As NTSInformatica.NTSButton
  Public WithEvents cmdTermina As NTSInformatica.NTSButton
  Public WithEvents edBarcode As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTipo As NTSInformatica.NTSLabel
  Public WithEvents cmdDaLista As NTSInformatica.NTSButton
  Public WithEvents cmdNote As NTSInformatica.NTSButton
  Public WithEvents xx_quadaeva As NTSInformatica.NTSGridColumn


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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEHLBC", "BEMGDOCU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128581693271641409, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleDocu = CType(oTmp, CLEMGDOCU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEHLBC", strRemoteServer, strRemotePort)
    oCleDocu.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

    '---------------------------------
    'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Dim dttStatus As New DataTable
      dttStatus.Columns.Add("cod", GetType(String))
      dttStatus.Columns.Add("val", GetType(String))
      dttStatus.Rows.Add(New Object() {"N", "Evadibile"})
      dttStatus.Rows.Add(New Object() {"1", "Qta eva = Qta res"})
      dttStatus.Rows.Add(New Object() {"2", "Qta > Qta eva + Qta da eva"})
      dttStatus.Rows.Add(New Object() {"3", "No ordine"})
      dttStatus.Rows.Add(New Object() {"4", "Qta eva < Qta res"})
      dttStatus.Rows.Add(New Object() {"5", "Non evadibile"})
      dttStatus.AcceptChanges()

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
      dttTipoRk.Rows.Add(New Object() {" ", ""})
      dttTipoRk.AcceptChanges()

      '-------------------------------------------------
      'completo le informazioni dei i controlli
      grvHlbc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023157574793, "Griglia righe d'ordine"))
      grvHlbc.NTSAllowUpdate = True
      grvHlbc.NTSAllowInsert = False

      xx_status.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128709844113125000, "Status riga"), dttStatus, "val", "cod")
      xx_conto.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128775259622690000, "Conto"), tabanagra)
      mo_codart.NTSSetParamSTRTabe(oMenu, oApp.Tr(Me, 128775259633454000, "Articolo"), tabartico, True)
      mo_magaz.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128775259643438000, "Magazzino"), tabmaga)
      mo_fase.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128775259653110000, "Fase"), tabartfasi)
      mo_fase.ArtiPerFase = mo_codart
      mo_tipork.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128775259662470000, "tipo ord."), dttTipoRk, "val", "cod")
      mo_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259673078000, "Anno ord."), "0", 4, 0, 9999)
      mo_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128775259683998000, "Serie ord."), CLN__STD.SerieMaxLen, True)
      mo_numord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259692734000, "Num. ord."), "0", 9, 0, 999999999)
      mo_riga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259702250000, "Riga ord."), "0", 9, 0, 999999999)
      xx_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128775259802870000, "Data ord."), True)
      mo_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128775259713482000, "Data cons."), True)
      mo_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128582680133906250, "Qta ordin."), oApp.FormatQta, 15)
      mo_quaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259762310000, "Qta evasa"), oApp.FormatQta, 15)
      xx_quadaeva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259771358000, "Qta da evadere"), oApp.FormatQta, 15, 0, 999999999999999)
      xx_flevasdaeva.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128775259781654000, "Evaso C/S"), "S", "C")
      xx_qtares.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259791482000, "Qta resid."), oApp.FormatQta, 15)
      mo_descr.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128775259378168000, "Descr. Articolo"), 0, True)
      xx_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128775259390180000, "Descr. magazzino"), 0, True)
      xx_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128775259400788000, "Riferimenti"), 0, True)
      mo_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128775259412332000, "Note riga"), 0, True)
      mo_prezzo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259424032000, "Prezzo unitario"), oApp.FormatPrzUn, 15)
      mo_scont1.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259434952000, "Sc. 1"), oApp.FormatSconti, 6)
      mo_scont2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259444000000, "Sc. 2"), oApp.FormatSconti, 6)
      mo_scont3.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259454608000, "Sc. 3"), oApp.FormatSconti, 6)
      mo_scont4.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259464436000, "Sc. 4"), oApp.FormatSconti, 6)
      mo_scont5.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259474888000, "Sc. 5"), oApp.FormatSconti, 6)
      mo_scont6.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259487992000, "Sc. 6"), oApp.FormatSconti, 6)
      mo_commeca.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128775259497976000, "Commessa"), tabcommess)
      mo_subcommeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128775259508584000, "S. comm."), 0, True)
      xx_codpaga.NTSSetParamNUMTabe(oMenu, oApp.Tr(Me, 128775259517476000, "Cod. pagamento"), tabpaga)
      xx_coddest.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128775259527304000, "Cod. destinazione"), "0", 9)
      edBarcode.NTSSetParam(oMenu, oApp.Tr(Me, 128710686095774906, "Barcode"), 0, True)

      xx_status.NTSSetRichiesto()

      grvHlbc.AddColumnBackColor("backcolor_row") 'sempre nella InitControls

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
    Me.pnButton = New NTSInformatica.NTSPanel
    Me.cmdNote = New NTSInformatica.NTSButton
    Me.cmdDaLista = New NTSInformatica.NTSButton
    Me.cmdCancellaRiga = New NTSInformatica.NTSButton
    Me.cmdStartdelete = New NTSInformatica.NTSButton
    Me.cmdStart = New NTSInformatica.NTSButton
    Me.cmdConferma = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdTerminale = New NTSInformatica.NTSButton
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grHlbc = New NTSInformatica.NTSGrid
    Me.grvHlbc = New NTSInformatica.NTSGridView
    Me.xx_status = New NTSInformatica.NTSGridColumn
    Me.mo_codart = New NTSInformatica.NTSGridColumn
    Me.mo_fase = New NTSInformatica.NTSGridColumn
    Me.mo_descr = New NTSInformatica.NTSGridColumn
    Me.mo_quant = New NTSInformatica.NTSGridColumn
    Me.mo_quaeva = New NTSInformatica.NTSGridColumn
    Me.xx_quadaeva = New NTSInformatica.NTSGridColumn
    Me.xx_flevasdaeva = New NTSInformatica.NTSGridColumn
    Me.xx_qtares = New NTSInformatica.NTSGridColumn
    Me.mo_tipork = New NTSInformatica.NTSGridColumn
    Me.mo_anno = New NTSInformatica.NTSGridColumn
    Me.mo_serie = New NTSInformatica.NTSGridColumn
    Me.mo_numord = New NTSInformatica.NTSGridColumn
    Me.mo_riga = New NTSInformatica.NTSGridColumn
    Me.xx_datord = New NTSInformatica.NTSGridColumn
    Me.mo_datcons = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.mo_magaz = New NTSInformatica.NTSGridColumn
    Me.xx_magaz = New NTSInformatica.NTSGridColumn
    Me.xx_riferim = New NTSInformatica.NTSGridColumn
    Me.mo_note = New NTSInformatica.NTSGridColumn
    Me.mo_prezzo = New NTSInformatica.NTSGridColumn
    Me.mo_scont1 = New NTSInformatica.NTSGridColumn
    Me.mo_scont2 = New NTSInformatica.NTSGridColumn
    Me.mo_scont3 = New NTSInformatica.NTSGridColumn
    Me.mo_scont4 = New NTSInformatica.NTSGridColumn
    Me.mo_scont5 = New NTSInformatica.NTSGridColumn
    Me.mo_scont6 = New NTSInformatica.NTSGridColumn
    Me.mo_commeca = New NTSInformatica.NTSGridColumn
    Me.mo_subcommeca = New NTSInformatica.NTSGridColumn
    Me.xx_codpaga = New NTSInformatica.NTSGridColumn
    Me.xx_coddest = New NTSInformatica.NTSGridColumn
    Me.NtsGridView1 = New NTSInformatica.NTSGridView
    Me.pnBottom = New NTSInformatica.NTSPanel
    Me.edBarcode = New NTSInformatica.NTSTextBoxStr
    Me.lbTipo = New NTSInformatica.NTSLabel
    Me.cmdTermina = New NTSInformatica.NTSButton
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnButton.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grHlbc, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvHlbc, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnBottom.SuspendLayout()
    CType(Me.edBarcode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'pnButton
    '
    Me.pnButton.AllowDrop = True
    Me.pnButton.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnButton.Appearance.Options.UseBackColor = True
    Me.pnButton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnButton.Controls.Add(Me.cmdNote)
    Me.pnButton.Controls.Add(Me.cmdDaLista)
    Me.pnButton.Controls.Add(Me.cmdCancellaRiga)
    Me.pnButton.Controls.Add(Me.cmdStartdelete)
    Me.pnButton.Controls.Add(Me.cmdStart)
    Me.pnButton.Controls.Add(Me.cmdConferma)
    Me.pnButton.Controls.Add(Me.cmdAnnulla)
    Me.pnButton.Controls.Add(Me.cmdTerminale)
    Me.pnButton.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnButton.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnButton.Location = New System.Drawing.Point(582, 0)
    Me.pnButton.Name = "pnButton"
    Me.pnButton.NTSActiveTrasparency = True
    Me.pnButton.Size = New System.Drawing.Size(129, 281)
    Me.pnButton.TabIndex = 2
    Me.pnButton.Text = "NtsPanel1"
    '
    'cmdNote
    '
    Me.cmdNote.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.cmdNote.ImageText = ""
    Me.cmdNote.Location = New System.Drawing.Point(7, 255)
    Me.cmdNote.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdNote.Name = "cmdNote"
    Me.cmdNote.NTSContextMenu = Nothing
    Me.cmdNote.Size = New System.Drawing.Size(111, 24)
    Me.cmdNote.TabIndex = 16
    Me.cmdNote.Text = "Note colori"
    '
    'cmdDaLista
    '
    Me.cmdDaLista.ImageText = ""
    Me.cmdDaLista.Location = New System.Drawing.Point(7, 154)
    Me.cmdDaLista.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdDaLista.Name = "cmdDaLista"
    Me.cmdDaLista.NTSContextMenu = Nothing
    Me.cmdDaLista.Size = New System.Drawing.Size(111, 24)
    Me.cmdDaLista.TabIndex = 15
    Me.cmdDaLista.Text = "Da lista selezionata"
    '
    'cmdCancellaRiga
    '
    Me.cmdCancellaRiga.ImageText = ""
    Me.cmdCancellaRiga.Location = New System.Drawing.Point(7, 73)
    Me.cmdCancellaRiga.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdCancellaRiga.Name = "cmdCancellaRiga"
    Me.cmdCancellaRiga.NTSContextMenu = Nothing
    Me.cmdCancellaRiga.Size = New System.Drawing.Size(110, 24)
    Me.cmdCancellaRiga.TabIndex = 14
    Me.cmdCancellaRiga.Text = "Cancella riga"
    '
    'cmdStartdelete
    '
    Me.cmdStartdelete.ImageText = ""
    Me.cmdStartdelete.Location = New System.Drawing.Point(7, 204)
    Me.cmdStartdelete.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdStartdelete.Name = "cmdStartdelete"
    Me.cmdStartdelete.NTSContextMenu = Nothing
    Me.cmdStartdelete.Size = New System.Drawing.Size(111, 24)
    Me.cmdStartdelete.TabIndex = 13
    Me.cmdStartdelete.Text = "Inizia cancellazione"
    '
    'cmdStart
    '
    Me.cmdStart.ImageText = ""
    Me.cmdStart.Location = New System.Drawing.Point(7, 179)
    Me.cmdStart.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdStart.Name = "cmdStart"
    Me.cmdStart.NTSContextMenu = Nothing
    Me.cmdStart.Size = New System.Drawing.Size(111, 24)
    Me.cmdStart.TabIndex = 12
    Me.cmdStart.Text = "Inizia acquisizione"
    '
    'cmdConferma
    '
    Me.cmdConferma.ImageText = ""
    Me.cmdConferma.Location = New System.Drawing.Point(6, 36)
    Me.cmdConferma.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdConferma.Name = "cmdConferma"
    Me.cmdConferma.NTSContextMenu = Nothing
    Me.cmdConferma.Size = New System.Drawing.Size(111, 24)
    Me.cmdConferma.TabIndex = 11
    Me.cmdConferma.Text = "&Conferma"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(6, 10)
    Me.cmdAnnulla.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(111, 24)
    Me.cmdAnnulla.TabIndex = 9
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'cmdTerminale
    '
    Me.cmdTerminale.ImageText = ""
    Me.cmdTerminale.Location = New System.Drawing.Point(7, 129)
    Me.cmdTerminale.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdTerminale.Name = "cmdTerminale"
    Me.cmdTerminale.NTSContextMenu = Nothing
    Me.cmdTerminale.Size = New System.Drawing.Size(111, 24)
    Me.cmdTerminale.TabIndex = 8
    Me.cmdTerminale.Text = "Terminale (Ctrl+K)"
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grHlbc)
    Me.pnGrid.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 0)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(582, 281)
    Me.pnGrid.TabIndex = 3
    Me.pnGrid.Text = "NtsPanel1"
    '
    'grHlbc
    '
    Me.grHlbc.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grHlbc.EmbeddedNavigator.Name = ""
    Me.grHlbc.Location = New System.Drawing.Point(0, 0)
    Me.grHlbc.MainView = Me.grvHlbc
    Me.grHlbc.Name = "grHlbc"
    Me.grHlbc.Size = New System.Drawing.Size(582, 281)
    Me.grHlbc.TabIndex = 0
    Me.grHlbc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHlbc, Me.NtsGridView1})
    '
    'grvHlbc
    '
    Me.grvHlbc.ActiveFilterEnabled = False
    Me.grvHlbc.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_status, Me.mo_codart, Me.mo_fase, Me.mo_descr, Me.mo_quant, Me.mo_quaeva, Me.xx_quadaeva, Me.xx_flevasdaeva, Me.xx_qtares, Me.mo_tipork, Me.mo_anno, Me.mo_serie, Me.mo_numord, Me.mo_riga, Me.xx_datord, Me.mo_datcons, Me.xx_conto, Me.mo_magaz, Me.xx_magaz, Me.xx_riferim, Me.mo_note, Me.mo_prezzo, Me.mo_scont1, Me.mo_scont2, Me.mo_scont3, Me.mo_scont4, Me.mo_scont5, Me.mo_scont6, Me.mo_commeca, Me.mo_subcommeca, Me.xx_codpaga, Me.xx_coddest})
    Me.grvHlbc.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.grvHlbc.Enabled = True
    Me.grvHlbc.GridControl = Me.grHlbc
    Me.grvHlbc.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvHlbc.MinRowHeight = 14
    Me.grvHlbc.Name = "grvHlbc"
    Me.grvHlbc.NTSAllowDelete = True
    Me.grvHlbc.NTSAllowInsert = True
    Me.grvHlbc.NTSAllowUpdate = True
    Me.grvHlbc.NTSMenuContext = Nothing
    Me.grvHlbc.OptionsCustomization.AllowRowSizing = True
    Me.grvHlbc.OptionsFilter.AllowFilterEditor = False
    Me.grvHlbc.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvHlbc.OptionsNavigation.UseTabKey = False
    Me.grvHlbc.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvHlbc.OptionsView.ColumnAutoWidth = False
    Me.grvHlbc.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvHlbc.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvHlbc.OptionsView.ShowGroupPanel = False
    Me.grvHlbc.RowHeight = 14
    '
    'xx_status
    '
    Me.xx_status.AppearanceCell.Options.UseBackColor = True
    Me.xx_status.AppearanceCell.Options.UseTextOptions = True
    Me.xx_status.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_status.Caption = "Status riga"
    Me.xx_status.Enabled = False
    Me.xx_status.FieldName = "xx_status"
    Me.xx_status.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_status.Name = "xx_status"
    Me.xx_status.NTSRepositoryComboBox = Nothing
    Me.xx_status.NTSRepositoryItemCheck = Nothing
    Me.xx_status.NTSRepositoryItemMemo = Nothing
    Me.xx_status.NTSRepositoryItemText = Nothing
    Me.xx_status.OptionsColumn.AllowEdit = False
    Me.xx_status.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_status.OptionsColumn.ReadOnly = True
    Me.xx_status.OptionsFilter.AllowFilter = False
    '
    'mo_codart
    '
    Me.mo_codart.AppearanceCell.Options.UseBackColor = True
    Me.mo_codart.AppearanceCell.Options.UseTextOptions = True
    Me.mo_codart.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_codart.Caption = "Articolo"
    Me.mo_codart.Enabled = False
    Me.mo_codart.FieldName = "mo_codart"
    Me.mo_codart.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_codart.Name = "mo_codart"
    Me.mo_codart.NTSRepositoryComboBox = Nothing
    Me.mo_codart.NTSRepositoryItemCheck = Nothing
    Me.mo_codart.NTSRepositoryItemMemo = Nothing
    Me.mo_codart.NTSRepositoryItemText = Nothing
    Me.mo_codart.OptionsColumn.AllowEdit = False
    Me.mo_codart.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_codart.OptionsColumn.ReadOnly = True
    Me.mo_codart.OptionsFilter.AllowFilter = False
    Me.mo_codart.Visible = True
    Me.mo_codart.VisibleIndex = 0
    '
    'mo_fase
    '
    Me.mo_fase.AppearanceCell.Options.UseBackColor = True
    Me.mo_fase.AppearanceCell.Options.UseTextOptions = True
    Me.mo_fase.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_fase.Caption = "Fase"
    Me.mo_fase.Enabled = False
    Me.mo_fase.FieldName = "mo_fase"
    Me.mo_fase.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_fase.Name = "mo_fase"
    Me.mo_fase.NTSRepositoryComboBox = Nothing
    Me.mo_fase.NTSRepositoryItemCheck = Nothing
    Me.mo_fase.NTSRepositoryItemMemo = Nothing
    Me.mo_fase.NTSRepositoryItemText = Nothing
    Me.mo_fase.OptionsColumn.AllowEdit = False
    Me.mo_fase.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_fase.OptionsColumn.ReadOnly = True
    Me.mo_fase.OptionsFilter.AllowFilter = False
    Me.mo_fase.Visible = True
    Me.mo_fase.VisibleIndex = 1
    '
    'mo_descr
    '
    Me.mo_descr.AppearanceCell.Options.UseBackColor = True
    Me.mo_descr.AppearanceCell.Options.UseTextOptions = True
    Me.mo_descr.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_descr.Caption = "Descr. Articolo"
    Me.mo_descr.Enabled = False
    Me.mo_descr.FieldName = "mo_descr"
    Me.mo_descr.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_descr.Name = "mo_descr"
    Me.mo_descr.NTSRepositoryComboBox = Nothing
    Me.mo_descr.NTSRepositoryItemCheck = Nothing
    Me.mo_descr.NTSRepositoryItemMemo = Nothing
    Me.mo_descr.NTSRepositoryItemText = Nothing
    Me.mo_descr.OptionsColumn.AllowEdit = False
    Me.mo_descr.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_descr.OptionsColumn.ReadOnly = True
    Me.mo_descr.OptionsFilter.AllowFilter = False
    Me.mo_descr.Visible = True
    Me.mo_descr.VisibleIndex = 2
    '
    'mo_quant
    '
    Me.mo_quant.AppearanceCell.Options.UseBackColor = True
    Me.mo_quant.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quant.Caption = "Qta ordin."
    Me.mo_quant.Enabled = False
    Me.mo_quant.FieldName = "mo_quant"
    Me.mo_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quant.Name = "mo_quant"
    Me.mo_quant.NTSRepositoryComboBox = Nothing
    Me.mo_quant.NTSRepositoryItemCheck = Nothing
    Me.mo_quant.NTSRepositoryItemMemo = Nothing
    Me.mo_quant.NTSRepositoryItemText = Nothing
    Me.mo_quant.OptionsColumn.AllowEdit = False
    Me.mo_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quant.OptionsColumn.ReadOnly = True
    Me.mo_quant.OptionsFilter.AllowFilter = False
    Me.mo_quant.Visible = True
    Me.mo_quant.VisibleIndex = 3
    '
    'mo_quaeva
    '
    Me.mo_quaeva.AppearanceCell.Options.UseBackColor = True
    Me.mo_quaeva.AppearanceCell.Options.UseTextOptions = True
    Me.mo_quaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_quaeva.Caption = "Qta evasa"
    Me.mo_quaeva.Enabled = False
    Me.mo_quaeva.FieldName = "mo_quaeva"
    Me.mo_quaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_quaeva.Name = "mo_quaeva"
    Me.mo_quaeva.NTSRepositoryComboBox = Nothing
    Me.mo_quaeva.NTSRepositoryItemCheck = Nothing
    Me.mo_quaeva.NTSRepositoryItemMemo = Nothing
    Me.mo_quaeva.NTSRepositoryItemText = Nothing
    Me.mo_quaeva.OptionsColumn.AllowEdit = False
    Me.mo_quaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_quaeva.OptionsColumn.ReadOnly = True
    Me.mo_quaeva.OptionsFilter.AllowFilter = False
    Me.mo_quaeva.Visible = True
    Me.mo_quaeva.VisibleIndex = 4
    '
    'xx_quadaeva
    '
    Me.xx_quadaeva.AppearanceCell.Options.UseBackColor = True
    Me.xx_quadaeva.AppearanceCell.Options.UseTextOptions = True
    Me.xx_quadaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_quadaeva.Caption = "Qta da evadere"
    Me.xx_quadaeva.Enabled = True
    Me.xx_quadaeva.FieldName = "xx_quadaeva"
    Me.xx_quadaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_quadaeva.Name = "xx_quadaeva"
    Me.xx_quadaeva.NTSRepositoryComboBox = Nothing
    Me.xx_quadaeva.NTSRepositoryItemCheck = Nothing
    Me.xx_quadaeva.NTSRepositoryItemMemo = Nothing
    Me.xx_quadaeva.NTSRepositoryItemText = Nothing
    Me.xx_quadaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_quadaeva.OptionsFilter.AllowFilter = False
    Me.xx_quadaeva.Visible = True
    Me.xx_quadaeva.VisibleIndex = 5
    '
    'xx_flevasdaeva
    '
    Me.xx_flevasdaeva.AppearanceCell.Options.UseBackColor = True
    Me.xx_flevasdaeva.AppearanceCell.Options.UseTextOptions = True
    Me.xx_flevasdaeva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_flevasdaeva.Caption = "Evaso C/S"
    Me.xx_flevasdaeva.Enabled = True
    Me.xx_flevasdaeva.FieldName = "xx_flevasdaeva"
    Me.xx_flevasdaeva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_flevasdaeva.Name = "xx_flevasdaeva"
    Me.xx_flevasdaeva.NTSRepositoryComboBox = Nothing
    Me.xx_flevasdaeva.NTSRepositoryItemCheck = Nothing
    Me.xx_flevasdaeva.NTSRepositoryItemMemo = Nothing
    Me.xx_flevasdaeva.NTSRepositoryItemText = Nothing
    Me.xx_flevasdaeva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_flevasdaeva.OptionsFilter.AllowFilter = False
    Me.xx_flevasdaeva.Visible = True
    Me.xx_flevasdaeva.VisibleIndex = 6
    '
    'xx_qtares
    '
    Me.xx_qtares.AppearanceCell.Options.UseBackColor = True
    Me.xx_qtares.AppearanceCell.Options.UseTextOptions = True
    Me.xx_qtares.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_qtares.Caption = "Qta resid."
    Me.xx_qtares.Enabled = False
    Me.xx_qtares.FieldName = "xx_qtares"
    Me.xx_qtares.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_qtares.Name = "xx_qtares"
    Me.xx_qtares.NTSRepositoryComboBox = Nothing
    Me.xx_qtares.NTSRepositoryItemCheck = Nothing
    Me.xx_qtares.NTSRepositoryItemMemo = Nothing
    Me.xx_qtares.NTSRepositoryItemText = Nothing
    Me.xx_qtares.OptionsColumn.AllowEdit = False
    Me.xx_qtares.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_qtares.OptionsColumn.ReadOnly = True
    Me.xx_qtares.OptionsFilter.AllowFilter = False
    Me.xx_qtares.Visible = True
    Me.xx_qtares.VisibleIndex = 7
    '
    'mo_tipork
    '
    Me.mo_tipork.AppearanceCell.Options.UseBackColor = True
    Me.mo_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.mo_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_tipork.Caption = "tipo ord."
    Me.mo_tipork.Enabled = False
    Me.mo_tipork.FieldName = "mo_tipork"
    Me.mo_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_tipork.Name = "mo_tipork"
    Me.mo_tipork.NTSRepositoryComboBox = Nothing
    Me.mo_tipork.NTSRepositoryItemCheck = Nothing
    Me.mo_tipork.NTSRepositoryItemMemo = Nothing
    Me.mo_tipork.NTSRepositoryItemText = Nothing
    Me.mo_tipork.OptionsColumn.AllowEdit = False
    Me.mo_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_tipork.OptionsColumn.ReadOnly = True
    Me.mo_tipork.OptionsFilter.AllowFilter = False
    Me.mo_tipork.Visible = True
    Me.mo_tipork.VisibleIndex = 8
    '
    'mo_anno
    '
    Me.mo_anno.AppearanceCell.Options.UseBackColor = True
    Me.mo_anno.AppearanceCell.Options.UseTextOptions = True
    Me.mo_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_anno.Caption = "Anno ord."
    Me.mo_anno.Enabled = False
    Me.mo_anno.FieldName = "mo_anno"
    Me.mo_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_anno.Name = "mo_anno"
    Me.mo_anno.NTSRepositoryComboBox = Nothing
    Me.mo_anno.NTSRepositoryItemCheck = Nothing
    Me.mo_anno.NTSRepositoryItemMemo = Nothing
    Me.mo_anno.NTSRepositoryItemText = Nothing
    Me.mo_anno.OptionsColumn.AllowEdit = False
    Me.mo_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_anno.OptionsColumn.ReadOnly = True
    Me.mo_anno.OptionsFilter.AllowFilter = False
    Me.mo_anno.Visible = True
    Me.mo_anno.VisibleIndex = 9
    '
    'mo_serie
    '
    Me.mo_serie.AppearanceCell.Options.UseBackColor = True
    Me.mo_serie.AppearanceCell.Options.UseTextOptions = True
    Me.mo_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_serie.Caption = "Serie ord."
    Me.mo_serie.Enabled = False
    Me.mo_serie.FieldName = "mo_serie"
    Me.mo_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_serie.Name = "mo_serie"
    Me.mo_serie.NTSRepositoryComboBox = Nothing
    Me.mo_serie.NTSRepositoryItemCheck = Nothing
    Me.mo_serie.NTSRepositoryItemMemo = Nothing
    Me.mo_serie.NTSRepositoryItemText = Nothing
    Me.mo_serie.OptionsColumn.AllowEdit = False
    Me.mo_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_serie.OptionsColumn.ReadOnly = True
    Me.mo_serie.OptionsFilter.AllowFilter = False
    Me.mo_serie.Visible = True
    Me.mo_serie.VisibleIndex = 10
    '
    'mo_numord
    '
    Me.mo_numord.AppearanceCell.Options.UseBackColor = True
    Me.mo_numord.AppearanceCell.Options.UseTextOptions = True
    Me.mo_numord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_numord.Caption = "Num. ord."
    Me.mo_numord.Enabled = False
    Me.mo_numord.FieldName = "mo_numord"
    Me.mo_numord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_numord.Name = "mo_numord"
    Me.mo_numord.NTSRepositoryComboBox = Nothing
    Me.mo_numord.NTSRepositoryItemCheck = Nothing
    Me.mo_numord.NTSRepositoryItemMemo = Nothing
    Me.mo_numord.NTSRepositoryItemText = Nothing
    Me.mo_numord.OptionsColumn.AllowEdit = False
    Me.mo_numord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_numord.OptionsColumn.ReadOnly = True
    Me.mo_numord.OptionsFilter.AllowFilter = False
    Me.mo_numord.Visible = True
    Me.mo_numord.VisibleIndex = 11
    '
    'mo_riga
    '
    Me.mo_riga.AppearanceCell.Options.UseBackColor = True
    Me.mo_riga.AppearanceCell.Options.UseTextOptions = True
    Me.mo_riga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_riga.Caption = "Riga ord."
    Me.mo_riga.Enabled = False
    Me.mo_riga.FieldName = "mo_riga"
    Me.mo_riga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_riga.Name = "mo_riga"
    Me.mo_riga.NTSRepositoryComboBox = Nothing
    Me.mo_riga.NTSRepositoryItemCheck = Nothing
    Me.mo_riga.NTSRepositoryItemMemo = Nothing
    Me.mo_riga.NTSRepositoryItemText = Nothing
    Me.mo_riga.OptionsColumn.AllowEdit = False
    Me.mo_riga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_riga.OptionsColumn.ReadOnly = True
    Me.mo_riga.OptionsFilter.AllowFilter = False
    Me.mo_riga.Visible = True
    Me.mo_riga.VisibleIndex = 12
    '
    'xx_datord
    '
    Me.xx_datord.AppearanceCell.Options.UseBackColor = True
    Me.xx_datord.AppearanceCell.Options.UseTextOptions = True
    Me.xx_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_datord.Caption = "Data ord."
    Me.xx_datord.Enabled = False
    Me.xx_datord.FieldName = "xx_datord"
    Me.xx_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_datord.Name = "xx_datord"
    Me.xx_datord.NTSRepositoryComboBox = Nothing
    Me.xx_datord.NTSRepositoryItemCheck = Nothing
    Me.xx_datord.NTSRepositoryItemMemo = Nothing
    Me.xx_datord.NTSRepositoryItemText = Nothing
    Me.xx_datord.OptionsColumn.AllowEdit = False
    Me.xx_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_datord.OptionsColumn.ReadOnly = True
    Me.xx_datord.OptionsFilter.AllowFilter = False
    Me.xx_datord.Visible = True
    Me.xx_datord.VisibleIndex = 13
    '
    'mo_datcons
    '
    Me.mo_datcons.AppearanceCell.Options.UseBackColor = True
    Me.mo_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.mo_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_datcons.Caption = "Data cons."
    Me.mo_datcons.Enabled = False
    Me.mo_datcons.FieldName = "mo_datcons"
    Me.mo_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_datcons.Name = "mo_datcons"
    Me.mo_datcons.NTSRepositoryComboBox = Nothing
    Me.mo_datcons.NTSRepositoryItemCheck = Nothing
    Me.mo_datcons.NTSRepositoryItemMemo = Nothing
    Me.mo_datcons.NTSRepositoryItemText = Nothing
    Me.mo_datcons.OptionsColumn.AllowEdit = False
    Me.mo_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_datcons.OptionsColumn.ReadOnly = True
    Me.mo_datcons.OptionsFilter.AllowFilter = False
    Me.mo_datcons.Visible = True
    Me.mo_datcons.VisibleIndex = 14
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Conto"
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
    '
    'mo_magaz
    '
    Me.mo_magaz.AppearanceCell.Options.UseBackColor = True
    Me.mo_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.mo_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_magaz.Caption = "Magazzino"
    Me.mo_magaz.Enabled = False
    Me.mo_magaz.FieldName = "mo_magaz"
    Me.mo_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_magaz.Name = "mo_magaz"
    Me.mo_magaz.NTSRepositoryComboBox = Nothing
    Me.mo_magaz.NTSRepositoryItemCheck = Nothing
    Me.mo_magaz.NTSRepositoryItemMemo = Nothing
    Me.mo_magaz.NTSRepositoryItemText = Nothing
    Me.mo_magaz.OptionsColumn.AllowEdit = False
    Me.mo_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_magaz.OptionsColumn.ReadOnly = True
    Me.mo_magaz.OptionsFilter.AllowFilter = False
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
    '
    'xx_riferim
    '
    Me.xx_riferim.AppearanceCell.Options.UseBackColor = True
    Me.xx_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.xx_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_riferim.Caption = "Riferimenti"
    Me.xx_riferim.Enabled = False
    Me.xx_riferim.FieldName = "xx_riferim"
    Me.xx_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_riferim.Name = "xx_riferim"
    Me.xx_riferim.NTSRepositoryComboBox = Nothing
    Me.xx_riferim.NTSRepositoryItemCheck = Nothing
    Me.xx_riferim.NTSRepositoryItemMemo = Nothing
    Me.xx_riferim.NTSRepositoryItemText = Nothing
    Me.xx_riferim.OptionsColumn.AllowEdit = False
    Me.xx_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_riferim.OptionsColumn.ReadOnly = True
    Me.xx_riferim.OptionsFilter.AllowFilter = False
    '
    'mo_note
    '
    Me.mo_note.AppearanceCell.Options.UseBackColor = True
    Me.mo_note.AppearanceCell.Options.UseTextOptions = True
    Me.mo_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_note.Caption = "Note riga"
    Me.mo_note.Enabled = False
    Me.mo_note.FieldName = "mo_note"
    Me.mo_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_note.Name = "mo_note"
    Me.mo_note.NTSRepositoryComboBox = Nothing
    Me.mo_note.NTSRepositoryItemCheck = Nothing
    Me.mo_note.NTSRepositoryItemMemo = Nothing
    Me.mo_note.NTSRepositoryItemText = Nothing
    Me.mo_note.OptionsColumn.AllowEdit = False
    Me.mo_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_note.OptionsColumn.ReadOnly = True
    Me.mo_note.OptionsFilter.AllowFilter = False
    '
    'mo_prezzo
    '
    Me.mo_prezzo.AppearanceCell.Options.UseBackColor = True
    Me.mo_prezzo.AppearanceCell.Options.UseTextOptions = True
    Me.mo_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_prezzo.Caption = "Prezzo"
    Me.mo_prezzo.Enabled = False
    Me.mo_prezzo.FieldName = "mo_prezzo"
    Me.mo_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_prezzo.Name = "mo_prezzo"
    Me.mo_prezzo.NTSRepositoryComboBox = Nothing
    Me.mo_prezzo.NTSRepositoryItemCheck = Nothing
    Me.mo_prezzo.NTSRepositoryItemMemo = Nothing
    Me.mo_prezzo.NTSRepositoryItemText = Nothing
    Me.mo_prezzo.OptionsColumn.AllowEdit = False
    Me.mo_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_prezzo.OptionsColumn.ReadOnly = True
    Me.mo_prezzo.OptionsFilter.AllowFilter = False
    '
    'mo_scont1
    '
    Me.mo_scont1.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont1.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont1.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont1.Caption = "Sc. 1"
    Me.mo_scont1.Enabled = False
    Me.mo_scont1.FieldName = "mo_scont1"
    Me.mo_scont1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont1.Name = "mo_scont1"
    Me.mo_scont1.NTSRepositoryComboBox = Nothing
    Me.mo_scont1.NTSRepositoryItemCheck = Nothing
    Me.mo_scont1.NTSRepositoryItemMemo = Nothing
    Me.mo_scont1.NTSRepositoryItemText = Nothing
    Me.mo_scont1.OptionsColumn.AllowEdit = False
    Me.mo_scont1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont1.OptionsColumn.ReadOnly = True
    Me.mo_scont1.OptionsFilter.AllowFilter = False
    '
    'mo_scont2
    '
    Me.mo_scont2.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont2.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont2.Caption = "Sc. 2"
    Me.mo_scont2.Enabled = False
    Me.mo_scont2.FieldName = "mo_scont2"
    Me.mo_scont2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont2.Name = "mo_scont2"
    Me.mo_scont2.NTSRepositoryComboBox = Nothing
    Me.mo_scont2.NTSRepositoryItemCheck = Nothing
    Me.mo_scont2.NTSRepositoryItemMemo = Nothing
    Me.mo_scont2.NTSRepositoryItemText = Nothing
    Me.mo_scont2.OptionsColumn.AllowEdit = False
    Me.mo_scont2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont2.OptionsColumn.ReadOnly = True
    Me.mo_scont2.OptionsFilter.AllowFilter = False
    '
    'mo_scont3
    '
    Me.mo_scont3.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont3.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont3.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont3.Caption = "Sc. 3"
    Me.mo_scont3.Enabled = False
    Me.mo_scont3.FieldName = "mo_scont3"
    Me.mo_scont3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont3.Name = "mo_scont3"
    Me.mo_scont3.NTSRepositoryComboBox = Nothing
    Me.mo_scont3.NTSRepositoryItemCheck = Nothing
    Me.mo_scont3.NTSRepositoryItemMemo = Nothing
    Me.mo_scont3.NTSRepositoryItemText = Nothing
    Me.mo_scont3.OptionsColumn.AllowEdit = False
    Me.mo_scont3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont3.OptionsColumn.ReadOnly = True
    Me.mo_scont3.OptionsFilter.AllowFilter = False
    '
    'mo_scont4
    '
    Me.mo_scont4.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont4.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont4.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont4.Caption = "Sc. 4"
    Me.mo_scont4.Enabled = False
    Me.mo_scont4.FieldName = "mo_scont4"
    Me.mo_scont4.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont4.Name = "mo_scont4"
    Me.mo_scont4.NTSRepositoryComboBox = Nothing
    Me.mo_scont4.NTSRepositoryItemCheck = Nothing
    Me.mo_scont4.NTSRepositoryItemMemo = Nothing
    Me.mo_scont4.NTSRepositoryItemText = Nothing
    Me.mo_scont4.OptionsColumn.AllowEdit = False
    Me.mo_scont4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont4.OptionsColumn.ReadOnly = True
    Me.mo_scont4.OptionsFilter.AllowFilter = False
    '
    'mo_scont5
    '
    Me.mo_scont5.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont5.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont5.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont5.Caption = "Sc. 5"
    Me.mo_scont5.Enabled = False
    Me.mo_scont5.FieldName = "mo_scont5"
    Me.mo_scont5.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont5.Name = "mo_scont5"
    Me.mo_scont5.NTSRepositoryComboBox = Nothing
    Me.mo_scont5.NTSRepositoryItemCheck = Nothing
    Me.mo_scont5.NTSRepositoryItemMemo = Nothing
    Me.mo_scont5.NTSRepositoryItemText = Nothing
    Me.mo_scont5.OptionsColumn.AllowEdit = False
    Me.mo_scont5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont5.OptionsColumn.ReadOnly = True
    Me.mo_scont5.OptionsFilter.AllowFilter = False
    '
    'mo_scont6
    '
    Me.mo_scont6.AppearanceCell.Options.UseBackColor = True
    Me.mo_scont6.AppearanceCell.Options.UseTextOptions = True
    Me.mo_scont6.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_scont6.Caption = "Sc. 6"
    Me.mo_scont6.Enabled = False
    Me.mo_scont6.FieldName = "mo_scont6"
    Me.mo_scont6.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_scont6.Name = "mo_scont6"
    Me.mo_scont6.NTSRepositoryComboBox = Nothing
    Me.mo_scont6.NTSRepositoryItemCheck = Nothing
    Me.mo_scont6.NTSRepositoryItemMemo = Nothing
    Me.mo_scont6.NTSRepositoryItemText = Nothing
    Me.mo_scont6.OptionsColumn.AllowEdit = False
    Me.mo_scont6.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_scont6.OptionsColumn.ReadOnly = True
    Me.mo_scont6.OptionsFilter.AllowFilter = False
    '
    'mo_commeca
    '
    Me.mo_commeca.AppearanceCell.Options.UseBackColor = True
    Me.mo_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.mo_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_commeca.Caption = "Commessa"
    Me.mo_commeca.Enabled = False
    Me.mo_commeca.FieldName = "mo_commeca"
    Me.mo_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_commeca.Name = "mo_commeca"
    Me.mo_commeca.NTSRepositoryComboBox = Nothing
    Me.mo_commeca.NTSRepositoryItemCheck = Nothing
    Me.mo_commeca.NTSRepositoryItemMemo = Nothing
    Me.mo_commeca.NTSRepositoryItemText = Nothing
    Me.mo_commeca.OptionsColumn.AllowEdit = False
    Me.mo_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_commeca.OptionsColumn.ReadOnly = True
    Me.mo_commeca.OptionsFilter.AllowFilter = False
    '
    'mo_subcommeca
    '
    Me.mo_subcommeca.AppearanceCell.Options.UseBackColor = True
    Me.mo_subcommeca.AppearanceCell.Options.UseTextOptions = True
    Me.mo_subcommeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mo_subcommeca.Caption = "S. comm."
    Me.mo_subcommeca.Enabled = False
    Me.mo_subcommeca.FieldName = "mo_subcommeca"
    Me.mo_subcommeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mo_subcommeca.Name = "mo_subcommeca"
    Me.mo_subcommeca.NTSRepositoryComboBox = Nothing
    Me.mo_subcommeca.NTSRepositoryItemCheck = Nothing
    Me.mo_subcommeca.NTSRepositoryItemMemo = Nothing
    Me.mo_subcommeca.NTSRepositoryItemText = Nothing
    Me.mo_subcommeca.OptionsColumn.AllowEdit = False
    Me.mo_subcommeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mo_subcommeca.OptionsColumn.ReadOnly = True
    Me.mo_subcommeca.OptionsFilter.AllowFilter = False
    '
    'xx_codpaga
    '
    Me.xx_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.xx_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codpaga.Caption = "Cod. pagamento"
    Me.xx_codpaga.Enabled = False
    Me.xx_codpaga.FieldName = "xx_codpaga"
    Me.xx_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codpaga.Name = "xx_codpaga"
    Me.xx_codpaga.NTSRepositoryComboBox = Nothing
    Me.xx_codpaga.NTSRepositoryItemCheck = Nothing
    Me.xx_codpaga.NTSRepositoryItemMemo = Nothing
    Me.xx_codpaga.NTSRepositoryItemText = Nothing
    Me.xx_codpaga.OptionsColumn.AllowEdit = False
    Me.xx_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codpaga.OptionsColumn.ReadOnly = True
    Me.xx_codpaga.OptionsFilter.AllowFilter = False
    '
    'xx_coddest
    '
    Me.xx_coddest.AppearanceCell.Options.UseBackColor = True
    Me.xx_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.xx_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_coddest.Caption = "Destinazione 1"
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
    '
    'NtsGridView1
    '
    Me.NtsGridView1.ActiveFilterEnabled = False
    Me.NtsGridView1.CustomizationFormBounds = New System.Drawing.Rectangle(592, 326, 208, 170)
    Me.NtsGridView1.Enabled = True
    Me.NtsGridView1.GridControl = Me.grHlbc
    Me.NtsGridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.NtsGridView1.MinRowHeight = 14
    Me.NtsGridView1.Name = "NtsGridView1"
    Me.NtsGridView1.NTSAllowDelete = True
    Me.NtsGridView1.NTSAllowInsert = True
    Me.NtsGridView1.NTSAllowUpdate = True
    Me.NtsGridView1.NTSMenuContext = Nothing
    Me.NtsGridView1.OptionsCustomization.AllowRowSizing = True
    Me.NtsGridView1.OptionsFilter.AllowFilterEditor = False
    Me.NtsGridView1.OptionsNavigation.EnterMoveNextColumn = True
    Me.NtsGridView1.OptionsNavigation.UseTabKey = False
    Me.NtsGridView1.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.NtsGridView1.OptionsView.ColumnAutoWidth = False
    Me.NtsGridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.NtsGridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.NtsGridView1.OptionsView.ShowGroupPanel = False
    Me.NtsGridView1.RowHeight = 14
    '
    'pnBottom
    '
    Me.pnBottom.AllowDrop = True
    Me.pnBottom.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnBottom.Appearance.Options.UseBackColor = True
    Me.pnBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnBottom.Controls.Add(Me.edBarcode)
    Me.pnBottom.Controls.Add(Me.lbTipo)
    Me.pnBottom.Controls.Add(Me.cmdTermina)
    Me.pnBottom.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom
    Me.pnBottom.Location = New System.Drawing.Point(0, 281)
    Me.pnBottom.Name = "pnBottom"
    Me.pnBottom.NTSActiveTrasparency = True
    Me.pnBottom.Size = New System.Drawing.Size(711, 35)
    Me.pnBottom.TabIndex = 4
    Me.pnBottom.Text = "NtsPanel1"
    '
    'edBarcode
    '
    Me.edBarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edBarcode.Cursor = System.Windows.Forms.Cursors.Default
    Me.edBarcode.Location = New System.Drawing.Point(434, 8)
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
    Me.edBarcode.Size = New System.Drawing.Size(148, 20)
    Me.edBarcode.TabIndex = 16
    '
    'lbTipo
    '
    Me.lbTipo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbTipo.AutoSize = True
    Me.lbTipo.BackColor = System.Drawing.Color.Transparent
    Me.lbTipo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbTipo.ForeColor = System.Drawing.Color.Red
    Me.lbTipo.Location = New System.Drawing.Point(260, 9)
    Me.lbTipo.Name = "lbTipo"
    Me.lbTipo.NTSDbField = ""
    Me.lbTipo.Size = New System.Drawing.Size(146, 16)
    Me.lbTipo.TabIndex = 17
    Me.lbTipo.Text = "Modalita: ACQUISISCI"
    Me.lbTipo.Tooltip = ""
    Me.lbTipo.UseMnemonic = False
    '
    'cmdTermina
    '
    Me.cmdTermina.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdTermina.ImageText = ""
    Me.cmdTermina.Location = New System.Drawing.Point(589, 8)
    Me.cmdTermina.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
    Me.cmdTermina.Name = "cmdTermina"
    Me.cmdTermina.NTSContextMenu = Nothing
    Me.cmdTermina.Size = New System.Drawing.Size(110, 24)
    Me.cmdTermina.TabIndex = 15
    Me.cmdTermina.Text = "Termina"
    '
    'FRMVEHLBC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(711, 316)
    Me.Controls.Add(Me.pnGrid)
    Me.Controls.Add(Me.pnButton)
    Me.Controls.Add(Me.pnBottom)
    Me.MinimizeBox = False
    Me.Name = "FRMVEHLBC"
    Me.Text = "SELEZIONE RIGHE D'ORDINE CON BARCODE"
    CType(Me.pnButton, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnButton.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grHlbc, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvHlbc, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnBottom, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnBottom.ResumeLayout(False)
    Me.pnBottom.PerformLayout()
    CType(Me.edBarcode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRMVEHLBC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------
      'predispongo i controlli
      InitControls()

      oCleDocu.bBarcodeConQta = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "Bsveboll", "Opzioni", ".", "BarcodeConQta", "0", " ", "0")) 'se attiva (-1) rileva la quantità indicata nei barcode
      oCleDocu.bBarcodeDerogaQta = CBool(oMenu.GetSettingBusDitt(DittaCorrente, "Bsveboll", "Opzioni", ".", "BarcodeDerogaQta", "0", " ", "0")) 'se attiva (-1) rileva la quantità indicata nei barcode pero' in deroga a BarcodeConQta (sono alternative)

      GctlSetRoules()

      GctlApplicaDefaultValue()

      '-------------------------
      pnButton.Enabled = True
      pnGrid.Enabled = True
      lbTipo.Visible = False
      edBarcode.Visible = False
      cmdTermina.Visible = False
      oCleDocu.nHlbcTipoOperaz = 0

      grvHlbc.OptionsSelection.EnableAppearanceFocusedRow = False

      Ricerca()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub FRMVEHLBC_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try
      If e.KeyCode = Keys.K And e.Control = True And e.Alt = False And e.Shift = False Then
        cmdTerminale_Click(cmdTerminale, Nothing)
        Return
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub Ricerca()
    Try
      dsHlbc.Clear()

      'parametri passati dal chiamante
      'oPar.strDescr = oCleBoll.GetWhereHlmo(lCodConto)
      'oPar.nAnno = NTSCInt(edEt_codpaga.Text)
      'oPar.nEscomp = NTSCInt(edEt_coddest.Text)
      'oPar.lContoCF = lCodConto
      'oPar.strAlfpar = oCleBoll.dttET.Rows(0)!et_scorpo.ToString
      'oPar.nValuta = NTSCInt(edEt_valuta.Text)
      'oPar.strTipo = cbTipoDoc.SelectedValue
      Me.Cursor = Cursors.WaitCursor
      oCleDocu.nHlbcCodpaga = oParam.nAnno
      oCleDocu.nHlbcCoddest = oParam.nEscomp
      oCleDocu.lHlbcConto = oParam.lContoCF
      oCleDocu.nHlbcValuta = oParam.nValuta
      oCleDocu.strHlbcTipork = oParam.strTipo
      oCleDocu.strHlbcScorpo = oParam.strAlfpar
      If Not oCleDocu.HlbcGetData(oParam.strDescr, dsHlbc) Then
        Me.Close()
        Return
      End If
      dcHlbc.DataSource = dsHlbc.Tables("SELDOC")
      dsHlbc.AcceptChanges()
      grHlbc.DataSource = dcHlbc

      For Each dtrT As DataRow In dsHlbc.Tables("SELDOC").Rows
        SettaColore(dtrT)
      Next
      grHlbc.Refresh()
      grHlbc.Refresh() '2 volte per ricaricare correttamente i colori impostati.

      'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valore
      If grvHlbc.RowCount > 0 Then grHlbc.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

#Region "CommandButton"
  Public Overridable Sub cmdConferma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConferma.Click
    Dim dtrT() As DataRow = Nothing
    Dim i As Integer = 0
    Try
      If Not Salva() Then Return

      Me.Cursor = Cursors.WaitCursor
      dtrT = dsHlbc.Tables("SELDOC").Select("NOT (xx_quadaeva > 0 OR (xx_quadaeva < xx_qtares AND xx_flevasdaeva = 'S'))")
      For i = 0 To dtrT.Length - 1
        dtrT(i).Delete()
      Next
      dsHlbc.Tables("SELDOC").AcceptChanges()
      oParam.oParam = dsHlbc.Tables("SELDOC")      'ritorno le righe selezionate

      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oParam.oParam = Nothing
    Me.Close()
  End Sub


  Public Overridable Sub cmdCancellaRiga_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancellaRiga.Click
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus
      If grvHlbc.RowCount = 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128710606982812500, "Nessuna riga da cancellare"))
        Return
      End If
      If grvHlbc.NTSGetCurrentDataRow Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128775259310308000, "Posizionarsi sulla riga da cancellare"))
        Return
      End If
      If NTSCInt(grvHlbc.NTSGetCurrentDataRow!mo_numord) <> 0 Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128710607461250000, "Possono essere cancellate solo righe senza riferimento all'ordine"))
        Return
      End If

      If Not grvHlbc.NTSDeleteRigaCorrente(dcHlbc, True) Then Return
      oCleDocu.HlbcSalva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdTerminale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTerminale.Click
    Dim i As Integer = 0
    Dim strPath As String = ""
    Dim strFile As String = ""
    Dim oPar As CLE__PATB
    Dim dtrTmp() As DataRow = Nothing
    Try
      Me.Cursor = Cursors.WaitCursor

      If Not oCleDocu.TerminaleLeggiCheck("BSVEBOLL") Then Return

      '---------------------------
      'Se il nome del file in strTermFileName è anteposto da ? lo chiede all'utente
      oCleDocu.SeparatePathAndFileName(oCleDocu.strTermFileName, strPath, strFile)
      If Microsoft.VisualBasic.Left(strFile, 1) = "?" Then
        'Lo chiede all'utente
        'strFile = oApp.InputBoxNew(oApp.Tr(Me, 128571298019531250, "Inserire il nome del file da acquisire (che deve risiedere nella cartella '|" & strPath & "|'):"), Mid(strFile, 2))
        'oCleBoll.strTermFileName = (strPath & "\").Replace("\\", "\") & strFile
        Dim oDlg As New NTSOpenFileDialog
        oDlg.Multiselect = False
        oDlg.ShowHelp = False
        oDlg.Title = oApp.Tr(Me, 128571256152187500, "File da acquisire")
        oDlg.DefaultExt = "TXT"
        oDlg.Filter = "File di testo (*.txt)|*.txt|Tutti i file (*.*)|*.*"
        oDlg.InitialDirectory = strPath
        oDlg.FileName = Mid(strFile, 2)
        oDlg.oMenu = oMenu
        If oDlg.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Return
        If oDlg.FileName <> "" Then
          oCleDocu.strTermFileName = oDlg.FileName
        End If
      End If

      'Lancia la shell
      If oCleDocu.bTermExecute Then
        'Prima di tutto cancella il file se esiste
        Try
          System.IO.File.Delete(oCleDocu.strTermFileName)
        Catch ex As Exception
          oApp.MsgBoxErr(oApp.Tr(Me, 128571303239218750, "Impossibile cancellare il file '|" & oCleDocu.strTermFileName & "|' prima della acquisizione. Errore: ") & vbCrLf & ex.Message)
          Return
        End Try

        If oCleDocu.strTermCommand.Trim <> "" Then
          Try
            Dim proc As New System.Diagnostics.Process
            If oCleDocu.bTermSilent Then proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            proc.StartInfo.FileName = oCleDocu.strTermCommand
            proc.StartInfo.UseShellExecute = True
            proc.Start()
            proc.WaitForExit()
          Catch ex As Exception
            oApp.MsgBoxErr(oApp.Tr(Me, 128571304353281250, "Errore durante l'esecuzione della Shell '|" & oCleDocu.strTermCommand & "|'. Errore: ") & vbCrLf & ex.Message)
            Return
          End Try
        End If
      End If    'If oCleBoll.bTermExecute Then

      If oCleDocu.bHlbcSelezionaOrdiniPreferenziali Then
        '------------------------
        'Chiama lo zoom testate ordini
        oPar = New CLE__PATB
        oPar.strDescr = oCleDocu.HlbcGetWhereHltd()
        oPar.nTipologia = 1       '0 = posso selez una sola riga, 1 posso fare multiselezione
        NTSZOOM.ZoomStrIn("ZOOMTESTORD", DittaCorrente, oPar)

        If Not oPar.oParam Is Nothing Then
          If CType(oPar.oParam, DataTable).Rows.Count > 0 Then
            For Each dtrT As DataRow In CType(oPar.oParam, DataTable).Rows
              dtrTmp = dsHlbc.Tables("SELDOC").Select("mo_tipork = " & CStrSQL(dtrT!td_tipork.ToString) & _
                                                      " AND mo_anno = " & dtrT!td_anno.ToString & _
                                                      " AND mo_serie = " & CStrSQL(dtrT!td_serie.ToString) & _
                                                      " AND mo_numord = " & dtrT!td_numord.ToString)
              For i = 0 To dtrTmp.Length - 1
                dtrTmp(i)!xx_prefer = 1
              Next
            Next
          End If
        End If
        dsHlbc.Tables("SELDOC").AcceptChanges()
      End If

      Me.Cursor = Cursors.WaitCursor
      '---------------------------
      'inizio l'import delle righe del file
      'scollego il datatable, importo le righe e lo ricollego: dalla veneziana, in terminal, senza motivo, 
      'se lasciavo collegato il dattable alla griglia venivano dati una serie di errori inspiegabili e sempre su righe di codice diverso...
      grHlbc.DataSource = Nothing
      If Not oCleDocu.TerminaleLeggiImportaFile(True) Then Return
      grHlbc.DataSource = dcHlbc

      '---------------------------
      'aggiorno i colori di riga
      For Each dtrT As DataRow In dsHlbc.Tables("SELDOC").Rows
        SettaColore(dtrT)
      Next
      grHlbc.Refresh()
      grHlbc.Refresh() '2 volte per ricaricare correttamente i colori impostati.

      '---------------------------
      'Visualizza il log
      If oCleDocu.bTermShowLogErr Then
        NTSProcessStart("notepad", oCleDocu.LogFileName)
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Public Overridable Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
    Try

      pnButton.Enabled = False
      pnGrid.Enabled = False
      lbTipo.Visible = True
      lbTipo.ForeColor = Color.Blue
      lbTipo.Text = oApp.Tr(Me, 128710681492806156, "Modalità ACQUISIZIONE")
      edBarcode.Visible = True
      cmdTermina.Visible = True
      oCleDocu.nHlbcTipoOperaz = 1
      edBarcode.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdStartdelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartdelete.Click
    Try

      pnButton.Enabled = False
      pnGrid.Enabled = False
      lbTipo.Visible = True
      lbTipo.ForeColor = Color.Red
      lbTipo.Text = oApp.Tr(Me, 128710681322493656, "Modalità CANCELLAZIONE")
      edBarcode.Visible = True
      cmdTermina.Visible = True
      oCleDocu.nHlbcTipoOperaz = 2
      edBarcode.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdTermina_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTermina.Click
    Try

      '-------------------------
      pnButton.Enabled = True
      pnGrid.Enabled = True
      lbTipo.Visible = False
      edBarcode.Visible = False
      cmdTermina.Visible = False
      oCleDocu.nHlbcTipoOperaz = 0

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdDaLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDaLista.Click
    Dim oParam As New CLE__CLDP
    Try
      oParam.dPar1 = 0
      oParam.dPar2 = 0

      oMenu.RunChild("NTSInformatica", "FRMMGSELI", "", DittaCorrente, "", "BNMGDOCU", oParam, "", True, True)

      If oParam.dPar2 <> 0 Then
        If oCleDocu.bHlbcSelezionaOrdiniPreferenziali Then
          '------------------------
          'Chiama lo zoom testate ordini
          Dim oPar As New CLE__PATB
          oPar.strDescr = oCleDocu.HlbcGetWhereHltd()
          oPar.nTipologia = 1       '0 = posso selez una sola riga, 1 posso fare multiselezione
          NTSZOOM.ZoomStrIn("ZOOMTESTORD", DittaCorrente, oPar)

          If Not oPar.oParam Is Nothing Then
            If CType(oPar.oParam, DataTable).Rows.Count > 0 Then
              For Each dtrT As DataRow In CType(oPar.oParam, DataTable).Rows
                Dim dtrTmp As datarow() = dsHlbc.Tables("SELDOC").Select("mo_tipork = " & CStrSQL(dtrT!td_tipork.ToString) & _
                                                        " AND mo_anno = " & dtrT!td_anno.ToString & _
                                                        " AND mo_serie = " & CStrSQL(dtrT!td_serie.ToString) & _
                                                        " AND mo_numord = " & dtrT!td_numord.ToString)
                For i As Integer = 0 To dtrTmp.Length - 1
                  dtrTmp(i)!xx_prefer = 1
                Next
              Next
            End If
          End If
          dsHlbc.Tables("SELDOC").AcceptChanges()
        End If


        oCleDocu.ControllaRigheBarcode(NTSCInt(oParam.dPar2), oParam.bPar1)
      End If


      For Each dtrC As DataRow In dsHlbc.Tables("SELDOC").Rows
        SettaColore(dtrC)
      Next
      grHlbc.Refresh()
      grHlbc.Refresh() '2 volte per ricaricare correttamente i colori impostati.
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNote.Click
    Try
      oApp.MsgBoxInfo(oApp.Tr(Me, 129400704463183594, _
                      "VERDE : quantità residua uguale a quantità da evadere" & vbCrLf & _
                      "GIALLO: quantità totale dell'ordine maggiore di quantità già evasa più quantità da evadere" & vbCrLf & _
                      "ROSSO: riga non collegata ad ordini/impegni" & vbCrLf & _
                      "VIOLA  : quantità da evadere minore della quantità residua e flag 'EVASO' spuntato" & vbCrLf & _
                      "GRIGIO: codice pagamento e/o destinazione diversa 1 diversi da quelli del documento che si sta inserendo/modificando"))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region

  Public Overridable Sub grvHlbc_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvHlbc.NTSBeforeRowUpdate
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

  Public Overridable Function Salva() As Boolean
    Try
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If GctlControllaOutNotEqual() = False Then Return False

      If Not oCleDocu.HlbcSalva(False) Then
        Return False
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      SettaColore(grvHlbc.NTSGetCurrentDataRow)
      grHlbc.Refresh()
      grHlbc.Refresh() '2 volte per ricaricare correttamente i colori impostati.
    End Try
  End Function

  Public Overridable Function SettaColore(ByRef dtrIn As DataRow) As Boolean
    'imposta lo staus di riga
    Try
      If dtrIn Is Nothing Then Return True

      'imposto il colore delle celle
      dtrIn!xx_status = "N"

      If NTSCDec(dtrIn!xx_qtares) = NTSCDec(dtrIn!xx_quadaeva) Then
        dtrIn!xx_status = "1"
      End If
      If (NTSCDec(dtrIn!mo_quant) > NTSCDec(dtrIn!xx_quadaeva) + NTSCDec(dtrIn!mo_quaeva)) _
           And (NTSCDec(dtrIn!mo_quaeva) + NTSCDec(dtrIn!xx_quadaeva) > 0) Then
        dtrIn!xx_status = "2"
      End If
      If NTSCInt(dtrIn!mo_numord) = 0 Then
        dtrIn!xx_status = "3"
      End If
      If (NTSCDec(dtrIn!xx_quadaeva) < NTSCDec(dtrIn!xx_qtares)) And (dtrIn!xx_flevasdaeva.ToString = "S") Then
        dtrIn!xx_status = "4"
      End If

      'parametri passati dal chiamante
      'oPar.strDescr = oCleBoll.GetWhereHlmo(lCodConto)
      'oPar.nAnno = NTSCInt(edEt_codpaga.Text)
      'oPar.nEscomp = NTSCInt(edEt_coddest.Text)
      If (NTSCInt(dtrIn!xx_codpaga) <> oParam.nAnno) Or (NTSCInt(dtrIn!xx_coddest) <> oParam.nEscomp) Then
        dtrIn!xx_status = "5"
      End If

      '---------------------------
      'memorizzo lo stato prima della modifica del campo

      'quando viene cambiato il colore della cella backcolor_row o della backcolor_*, 
      'sbs invia al client la segnalazione di 'cella cambiata'
      'su SBC la cellace backcolor_ non viene mai disegnata, ma viene utilizzata per colorare la riga/cella
      'NB: la colonna backcolor_ deve essere inserita anche nella griglia (magari non visibile) altrimenti non viene passata a SBC!!!

      'dtrIn.Table.Columns.Add("backcolor_row", GetType(Integer))        'costante 'backcolor_row' utilizzato da SBC per colorare tutta la riga
      'dtrIn.Table.Columns.Add("backcolor_xx_status", GetType(Integer))  'prefisso 'backcolor_' utilizzato da SBC per colorare la singola cella

      If dtrIn.Table.Columns.Contains("backcolor_row") = False Then
        dtrIn.Table.Columns.Add("backcolor_row", GetType(Integer))
      End If

      Dim oState As DataRowState = dtrIn.RowState
      Dim bOk As Boolean = oCleDocu.bHlbcHasChanges


      dtrIn!backcolor_row = -1  'non colorato
      Select Case NTSCStr(dtrIn!xx_status)
        Case "N" : dtrIn!backcolor_row = Color.White.ToArgb 'bianco
        Case "1" : dtrIn!backcolor_row = Color.FromArgb(136, 255, 136).ToArgb 'verde
        Case "2" : dtrIn!backcolor_row = Color.FromArgb(255, 255, 136).ToArgb 'giallo
        Case "3" : dtrIn!backcolor_row = Color.FromArgb(255, 136, 136).ToArgb 'rosso
        Case "4" : dtrIn!backcolor_row = Color.FromArgb(136, 136, 255).ToArgb 'viola
        Case "5" : dtrIn!backcolor_row = Color.FromArgb(192, 192, 192).ToArgb 'grigio
      End Select

      'reimposto lo stato a prima della modifica del campo
      If oState = DataRowState.Unchanged Then dtrIn.AcceptChanges()
      oCleDocu.bHlbcHasChanges = bOk

      dtrIn.AcceptChanges()

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public Overridable Sub edBarcode_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edBarcode.Validated
    Dim dtrProcess As DataRow = Nothing
    Dim i As Integer = 0
    Dim bOk As Boolean = False
    Try
      '-----------------------
      'processo il codice a barre
      If edBarcode.Text.Trim = "" Then Return
      Select Case edBarcode.Text.ToUpper
        Case "XINSEX"
          cmdStart_Click(cmdStart, Nothing)
        Case "XCANCX"
          cmdStartdelete_Click(cmdStartdelete, Nothing)
        Case Else
          oCleDocu.HlbcProcessBarcode(edBarcode.Text.ToUpper, dtrProcess)
          If dsHlbc.Tables("SELDOC").Rows.Count > 0 Then
            If Not dtrProcess Is Nothing Then
              'mi posiziono sull'ultima riga trattata
              For i = 0 To grvHlbc.RowCount - 1
                If CType(dcHlbc.Item(i), DataRowView).Row!mo_codart.ToString = dtrProcess!mo_codart.ToString And _
                   NTSCInt(CType(dcHlbc.Item(i), DataRowView).Row!mo_fase) = NTSCInt(dtrProcess!mo_fase) And _
                   CType(dcHlbc.Item(i), DataRowView).Row!mo_tipork.ToString = dtrProcess!mo_tipork.ToString And _
                   NTSCInt(CType(dcHlbc.Item(i), DataRowView).Row!mo_anno) = NTSCInt(dtrProcess!mo_anno) And _
                   CType(dcHlbc.Item(i), DataRowView).Row!mo_serie.ToString = dtrProcess!mo_serie.ToString And _
                   NTSCInt(CType(dcHlbc.Item(i), DataRowView).Row!mo_numord) = NTSCInt(dtrProcess!mo_numord) And _
                   NTSCInt(CType(dcHlbc.Item(i), DataRowView).Row!mo_riga) = NTSCInt(dtrProcess!mo_riga) Then
                  dcHlbc.Position = i
                  bOk = True
                  Exit For
                End If
              Next
              If bOk = False Then dcHlbc.Position = 0
            Else
              dcHlbc.Position = 0
            End If
          End If
      End Select
      edBarcode.Focus()

      For Each dtrT As DataRow In dsHlbc.Tables("SELDOC").Rows
        SettaColore(dtrT)
      Next
      grHlbc.Refresh()
      grHlbc.Refresh() '2 volte per ricaricare correttamente i colori impostati.

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      If edBarcode.Text <> "" Then edBarcode.Text = ""
    End Try
  End Sub
End Class
