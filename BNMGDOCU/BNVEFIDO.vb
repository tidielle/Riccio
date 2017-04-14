Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMVEFIDO
  Private components As System.ComponentModel.IContainer

  Public oParam As CLE__PATB       'parametri passati dal child che mi ha chiamato: sempre tramite questa classe gli restituisceo il valore

  Public oCleDocu As CLEMGDOCU
  Public dsSedo As DataSet
  Public dcSedo As BindingSource = New BindingSource()

  'Non più utilizzati
  Public dttCampi As New DataTable          'elenco campi filtrabili di TESTMAG
  Public dsFiltri As New DataSet
  Public dcFiltri1 As New BindingSource
  Public dttCampi2 As New DataTable          'elenco campi filtrabili di MOVMAG
  Public dsFiltri2 As New DataSet
  Public dcFiltri2 As New BindingSource

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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNVEFIDO", "BEMGDOCU", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 130086828914273175, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleDocu = CType(oTmp, CLEMGDOCU)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNVEFIDO", strRemoteServer, strRemotePort)
    oCleDocu.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort)

    '---------------------------------
    'inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    AddHandler oCleDocu.RemoteEvent, AddressOf GestisciEventiEntity

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      Dim dttVistato As New DataTable
      dttVistato.Columns.Add("cod", GetType(String))
      dttVistato.Columns.Add("val", GetType(String))
      dttVistato.Rows.Add(New Object() {"N", "Non vistato"})
      dttVistato.Rows.Add(New Object() {"T", "Trasferito per picking"})
      dttVistato.Rows.Add(New Object() {"E", "Etichette stampate"})
      dttVistato.Rows.Add(New Object() {"P", "Sospeso su palmare"})
      dttVistato.Rows.Add(New Object() {"S", "Vistato"})
      dttVistato.AcceptChanges()

      edDaTipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128660311571875000, "Da tipo bolla/fattura"), tabtpbf)
      edATipobf.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128660311759062500, "A tipo bolla/fattura"), tabtpbf)
      edContoA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023303752721, "A codice cliente/fornitore"), tabanagrac)
      edContoDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023303908894, "Da codice cliente/fornitore"), tabanagrac)
      edDataA.NTSSetParam(oMenu, oApp.Tr(Me, 128230023304065067, "A data documento"), False)
      edDataDa.NTSSetParam(oMenu, oApp.Tr(Me, 128230023304221240, "Da data documento"), False)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128230023304377413, "Serie documento"), CLN__STD.SerieMaxLen, True)
      edRiferim.NTSSetParam(oMenu, oApp.Tr(Me, 128534192493170000, "Riferimenti"), 255, True)
      edNumpar.NTSSetParam(oMenu, oApp.Tr(Me, 128782946799724000, "Numero partita"), "0", 9, 0, 999999999)

      grvSedoc.NTSSetParam(oMenu, oApp.Tr(Me, 128230023305002105, "Selezione Documenti"))
      xx_sel.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129496814860183202, "Seleziona"), "S", "N")
      tm_tipork.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023305158278, "Tipo documento"), 0, True)
      tm_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023305314451, "Anno"), "0", 4, 0, 9999)
      tm_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023305470624, "Serie"), CLN__STD.SerieMaxLen, True)
      tm_numdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023305626797, "Numero"), "0", 9, 0, 999999999)
      tm_datdoc.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128230023305782970, "Data documento"), True)
      tm_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023305939143, "Codice conto"), "0", 9, 0, 999999999)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023306095316, "Descr. conto"), 0, True)
      tm_vistato.NTSSetParamCMB(oMenu, oApp.Tr(Me, 128230023306251489, "Vistato"), dttVistato, "val", "cod")
      tm_totdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023306407662, "Tot. documento"), oApp.FormatImporti, 13)
      tm_totacceva.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023306563835, "Acconto evaso"), oApp.FormatImporti, 13)
      xx_residuo.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023306720008, "Tot. merce / Acc. residuo"), oApp.FormatImporti, 13)
      tm_coddest.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023306876181, "Cod. destin."), "0", 9, 0, 999999999)
      xx_destin.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023307032354, "Descr. destin."), 0, True)
      tm_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023307188527, "Commessa"), "0", 9, 0, 999999999)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023307344700, "Descr. commeca"), 0, True)
      tm_codpaga.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128660330980468750, "Cod. pagam."), "0", 4, 0, 9999)
      xx_codpaga.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128660330980625000, "Descr. pagam."), 0, True)
      tm_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128660330980781250, "Riferimenti"), 0, True)
      tm_annpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128660330980937500, "Annp P."), "0", 4, 0, 9999)
      tm_alfpar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128660330981093750, "Serie P."), CLN__STD.SerieMaxLen, True)
      tm_numpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128660330981250000, "Numero P."), "0", 9, 0, 999999999)
      tm_datpar.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128660330981406250, "Data P."), True)
      tm_tipobf.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128674151216402500, "Tipo bolla/fattura"), "0", 4, 0, 9999)
      xx_tipobf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128674151227027500, "Descr. tipo bolla/fattura"), 0, True)

      Select Case oParam.strTipo
        Case "A", "B", "C", "D", "N", "E", "W", "Z", "F", "I", "S"
          edContoDa.NTSSetParamZoom("ZOOMANAGRAC")
          edContoA.NTSSetParamZoom("ZOOMANAGRAC")
        Case Else
          edContoDa.NTSSetParamZoom("ZOOMANAGRAF")
          edContoA.NTSSetParamZoom("ZOOMANAGRAF")
      End Select

      Me.grvSedoc.NTSAllowDelete = False
      Me.grvSedoc.NTSAllowInsert = False
      Me.grvSedoc.NTSAllowUpdate = True

      Dim strProgr As String = ""
      If oParam.strTipo <> "D" AndAlso oParam.strTipo <> "K" AndAlso oParam.strTipo <> "(" AndAlso oParam.strTipo <> "£" Then
        strProgr = "BSVEBOLL"
      Else
        strProgr = "BSVEFDIN"
      End If

      ceFiltriExt.NTSSetParam(oMenu, oApp.Tr(Me, 128674151216402501, "Filtri Estesi"), strProgr, New CLE__CLDP)
      ceFiltriExt.AggiungiTabella("TESTMAG")
      'Da FDIN non gestisco MOVMAG
      If oParam.strTipo <> "D" AndAlso oParam.strTipo <> "K" AndAlso oParam.strTipo <> "(" AndAlso oParam.strTipo <> "£" Then
        ceFiltriExt.AggiungiTabella("MOVMAG")
      End If
      ceFiltriExt.AggiungiTabella("ANAGRA")
      ceFiltriExt.AggiungiTabella("DESTDIV")
      ceFiltriExt.AggiungiTabella("TABTPBF")
      ceFiltriExt.InizializzaFiltri()
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
    Me.cmdSeleziona = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.pnfiltri = New NTSInformatica.NTSPanel
    Me.tsSel = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grSedoc = New NTSInformatica.NTSGrid
    Me.grvSedoc = New NTSInformatica.NTSGridView
    Me.xx_sel = New NTSInformatica.NTSGridColumn
    Me.tm_tipork = New NTSInformatica.NTSGridColumn
    Me.tm_anno = New NTSInformatica.NTSGridColumn
    Me.tm_serie = New NTSInformatica.NTSGridColumn
    Me.tm_numdoc = New NTSInformatica.NTSGridColumn
    Me.tm_datdoc = New NTSInformatica.NTSGridColumn
    Me.tm_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.tm_vistato = New NTSInformatica.NTSGridColumn
    Me.tm_totdoc = New NTSInformatica.NTSGridColumn
    Me.tm_totacceva = New NTSInformatica.NTSGridColumn
    Me.xx_residuo = New NTSInformatica.NTSGridColumn
    Me.tm_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_destin = New NTSInformatica.NTSGridColumn
    Me.tm_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.tm_codpaga = New NTSInformatica.NTSGridColumn
    Me.xx_codpaga = New NTSInformatica.NTSGridColumn
    Me.tm_riferim = New NTSInformatica.NTSGridColumn
    Me.tm_annpar = New NTSInformatica.NTSGridColumn
    Me.tm_alfpar = New NTSInformatica.NTSGridColumn
    Me.tm_numpar = New NTSInformatica.NTSGridColumn
    Me.tm_datpar = New NTSInformatica.NTSGridColumn
    Me.tm_tipobf = New NTSInformatica.NTSGridColumn
    Me.xx_tipobf = New NTSInformatica.NTSGridColumn
    Me.pnSel0 = New NTSInformatica.NTSPanel
    Me.pnCommandbutton = New NTSInformatica.NTSPanel
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.edNumpar = New NTSInformatica.NTSTextBoxNum
    Me.lbDaconto = New NTSInformatica.NTSLabel
    Me.edDataDa = New NTSInformatica.NTSTextBoxData
    Me.lbNumpar = New NTSInformatica.NTSLabel
    Me.lbAdata = New NTSInformatica.NTSLabel
    Me.edDataA = New NTSInformatica.NTSTextBoxData
    Me.lbAnno = New System.Windows.Forms.Label
    Me.lbATipobf = New NTSInformatica.NTSLabel
    Me.edContoDa = New NTSInformatica.NTSTextBoxNum
    Me.edDaTipobf = New NTSInformatica.NTSTextBoxNum
    Me.lbDaTipobf = New NTSInformatica.NTSLabel
    Me.lbSerie = New NTSInformatica.NTSLabel
    Me.edContoA = New NTSInformatica.NTSTextBoxNum
    Me.edATipobf = New NTSInformatica.NTSTextBoxNum
    Me.lbDadata = New NTSInformatica.NTSLabel
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.lbRiferim = New NTSInformatica.NTSLabel
    Me.edRiferim = New NTSInformatica.NTSTextBoxStr
    Me.lbAconto = New NTSInformatica.NTSLabel
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnSel1 = New NTSInformatica.NTSPanel
    Me.ceFiltriExt = New NTSInformatica.NTSXXFILT
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.grFiltri1 = New NTSInformatica.NTSGrid
    Me.NtsGridView1 = New NTSInformatica.NTSGridView
    Me.grvFiltri1 = New NTSInformatica.NTSGridView
    Me.xx_nome = New NTSInformatica.NTSGridColumn
    Me.xx_valoreda = New NTSInformatica.NTSGridColumn
    Me.xx_valorea = New NTSInformatica.NTSGridColumn
    Me.NtsTabPage3 = New NTSInformatica.NTSTabPage
    Me.pnSel2 = New NTSInformatica.NTSPanel
    Me.grFiltri2 = New NTSInformatica.NTSGrid
    Me.NtsGridView2 = New NTSInformatica.NTSGridView
    Me.grvFiltri2 = New NTSInformatica.NTSGridView
    Me.xx_nome2 = New NTSInformatica.NTSGridColumn
    Me.xx_valoreda2 = New NTSInformatica.NTSGridColumn
    Me.xx_valorea2 = New NTSInformatica.NTSGridColumn
    Me.cmdLock = New NTSInformatica.NTSButton
    Me.cmdDesAll = New NTSInformatica.NTSButton
    Me.cmdSelAll = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnfiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnfiltri.SuspendLayout()
    CType(Me.tsSel, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSel.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grSedoc, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvSedoc, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSel0, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSel0.SuspendLayout()
    CType(Me.pnCommandbutton, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCommandbutton.SuspendLayout()
    CType(Me.edNumpar.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaTipobf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edATipobf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRiferim.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnSel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSel1.SuspendLayout()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.grFiltri1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFiltri1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnSel2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grFiltri2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvFiltri2, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.Color.Black
    Me.frmAuto.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmAuto.Appearance.Options.UseBackColor = True
    Me.frmAuto.Appearance.Options.UseImage = True
    '
    'cmdSeleziona
    '
    Me.cmdSeleziona.ImagePath = ""
    Me.cmdSeleziona.ImageText = ""
    Me.cmdSeleziona.Location = New System.Drawing.Point(3, 26)
    Me.cmdSeleziona.Name = "cmdSeleziona"
    Me.cmdSeleziona.NTSContextMenu = Nothing
    Me.cmdSeleziona.Size = New System.Drawing.Size(110, 23)
    Me.cmdSeleziona.TabIndex = 2
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 96)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(110, 23)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'pnfiltri
    '
    Me.pnfiltri.AllowDrop = True
    Me.pnfiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnfiltri.Appearance.Options.UseBackColor = True
    Me.pnfiltri.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnfiltri.Controls.Add(Me.tsSel)
    Me.pnfiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnfiltri.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnfiltri.Location = New System.Drawing.Point(0, 0)
    Me.pnfiltri.Name = "pnfiltri"
    Me.pnfiltri.NTSActiveTrasparency = True
    Me.pnfiltri.Size = New System.Drawing.Size(624, 492)
    Me.pnfiltri.TabIndex = 4
    '
    'tsSel
    '
    Me.tsSel.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsSel.Location = New System.Drawing.Point(0, 0)
    Me.tsSel.Name = "tsSel"
    Me.tsSel.SelectedTabPage = Me.NtsTabPage1
    Me.tsSel.Size = New System.Drawing.Size(624, 492)
    Me.tsSel.TabIndex = 0
    Me.tsSel.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2})
    Me.tsSel.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnGrid)
    Me.NtsTabPage1.Controls.Add(Me.pnSel0)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(615, 462)
    Me.NtsTabPage1.Text = "Generale"
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grSedoc)
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 122)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(615, 340)
    Me.pnGrid.TabIndex = 51
    '
    'grSedoc
    '
    Me.grSedoc.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grSedoc.EmbeddedNavigator.Name = ""
    Me.grSedoc.Location = New System.Drawing.Point(0, 0)
    Me.grSedoc.MainView = Me.grvSedoc
    Me.grSedoc.Name = "grSedoc"
    Me.grSedoc.Size = New System.Drawing.Size(615, 340)
    Me.grSedoc.TabIndex = 6
    Me.grSedoc.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvSedoc})
    '
    'grvSedoc
    '
    Me.grvSedoc.ActiveFilterEnabled = False
    Me.grvSedoc.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_sel, Me.tm_tipork, Me.tm_anno, Me.tm_serie, Me.tm_numdoc, Me.tm_datdoc, Me.tm_conto, Me.xx_conto, Me.tm_vistato, Me.tm_totdoc, Me.tm_totacceva, Me.xx_residuo, Me.tm_coddest, Me.xx_destin, Me.tm_commeca, Me.xx_commeca, Me.tm_codpaga, Me.xx_codpaga, Me.tm_riferim, Me.tm_annpar, Me.tm_alfpar, Me.tm_numpar, Me.tm_datpar, Me.tm_tipobf, Me.xx_tipobf})
    Me.grvSedoc.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvSedoc.Enabled = False
    Me.grvSedoc.GridControl = Me.grSedoc
    Me.grvSedoc.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvSedoc.GroupPanelText = "Per raggruppare: Tasto DX sulla colonna -> Group by this column *** Per totali pa" & _
        "rziali/generali: sul piede di gruppo della colonna tasto DX -> SUM "
    Me.grvSedoc.MinRowHeight = 14
    Me.grvSedoc.Name = "grvSedoc"
    Me.grvSedoc.NTSAllowDelete = True
    Me.grvSedoc.NTSAllowInsert = True
    Me.grvSedoc.NTSAllowUpdate = True
    Me.grvSedoc.NTSMenuContext = Nothing
    Me.grvSedoc.OptionsBehavior.Editable = False
    Me.grvSedoc.OptionsCustomization.AllowRowSizing = True
    Me.grvSedoc.OptionsFilter.AllowFilterEditor = False
    Me.grvSedoc.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvSedoc.OptionsNavigation.UseTabKey = False
    Me.grvSedoc.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvSedoc.OptionsView.ColumnAutoWidth = False
    Me.grvSedoc.OptionsView.EnableAppearanceEvenRow = True
    Me.grvSedoc.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvSedoc.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvSedoc.OptionsView.ShowGroupPanel = False
    Me.grvSedoc.RowHeight = 16
    '
    'xx_sel
    '
    Me.xx_sel.AppearanceCell.Options.UseBackColor = True
    Me.xx_sel.AppearanceCell.Options.UseTextOptions = True
    Me.xx_sel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_sel.Caption = "Selez."
    Me.xx_sel.Enabled = True
    Me.xx_sel.FieldName = "xx_sel"
    Me.xx_sel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_sel.Name = "xx_sel"
    Me.xx_sel.NTSRepositoryComboBox = Nothing
    Me.xx_sel.NTSRepositoryItemCheck = Nothing
    Me.xx_sel.NTSRepositoryItemMemo = Nothing
    Me.xx_sel.NTSRepositoryItemText = Nothing
    Me.xx_sel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_sel.OptionsFilter.AllowFilter = False
    Me.xx_sel.Visible = True
    Me.xx_sel.VisibleIndex = 0
    '
    'tm_tipork
    '
    Me.tm_tipork.AppearanceCell.Options.UseBackColor = True
    Me.tm_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.tm_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_tipork.Caption = "Tipo documento"
    Me.tm_tipork.Enabled = True
    Me.tm_tipork.FieldName = "tm_tipork"
    Me.tm_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_tipork.Name = "tm_tipork"
    Me.tm_tipork.NTSRepositoryComboBox = Nothing
    Me.tm_tipork.NTSRepositoryItemCheck = Nothing
    Me.tm_tipork.NTSRepositoryItemMemo = Nothing
    Me.tm_tipork.NTSRepositoryItemText = Nothing
    Me.tm_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_tipork.OptionsFilter.AllowFilter = False
    Me.tm_tipork.Width = 105
    '
    'tm_anno
    '
    Me.tm_anno.AppearanceCell.Options.UseBackColor = True
    Me.tm_anno.AppearanceCell.Options.UseTextOptions = True
    Me.tm_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_anno.Caption = "Anno"
    Me.tm_anno.Enabled = True
    Me.tm_anno.FieldName = "tm_anno"
    Me.tm_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_anno.Name = "tm_anno"
    Me.tm_anno.NTSRepositoryComboBox = Nothing
    Me.tm_anno.NTSRepositoryItemCheck = Nothing
    Me.tm_anno.NTSRepositoryItemMemo = Nothing
    Me.tm_anno.NTSRepositoryItemText = Nothing
    Me.tm_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_anno.OptionsFilter.AllowFilter = False
    Me.tm_anno.Visible = True
    Me.tm_anno.VisibleIndex = 1
    Me.tm_anno.Width = 58
    '
    'tm_serie
    '
    Me.tm_serie.AppearanceCell.Options.UseBackColor = True
    Me.tm_serie.AppearanceCell.Options.UseTextOptions = True
    Me.tm_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_serie.Caption = "Serie"
    Me.tm_serie.Enabled = True
    Me.tm_serie.FieldName = "tm_serie"
    Me.tm_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_serie.Name = "tm_serie"
    Me.tm_serie.NTSRepositoryComboBox = Nothing
    Me.tm_serie.NTSRepositoryItemCheck = Nothing
    Me.tm_serie.NTSRepositoryItemMemo = Nothing
    Me.tm_serie.NTSRepositoryItemText = Nothing
    Me.tm_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_serie.OptionsFilter.AllowFilter = False
    Me.tm_serie.Visible = True
    Me.tm_serie.VisibleIndex = 2
    Me.tm_serie.Width = 46
    '
    'tm_numdoc
    '
    Me.tm_numdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_numdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_numdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_numdoc.Caption = "Numero"
    Me.tm_numdoc.Enabled = True
    Me.tm_numdoc.FieldName = "tm_numdoc"
    Me.tm_numdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_numdoc.Name = "tm_numdoc"
    Me.tm_numdoc.NTSRepositoryComboBox = Nothing
    Me.tm_numdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_numdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_numdoc.NTSRepositoryItemText = Nothing
    Me.tm_numdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_numdoc.OptionsFilter.AllowFilter = False
    Me.tm_numdoc.Visible = True
    Me.tm_numdoc.VisibleIndex = 3
    Me.tm_numdoc.Width = 91
    '
    'tm_datdoc
    '
    Me.tm_datdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_datdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_datdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_datdoc.Caption = "Data doc."
    Me.tm_datdoc.Enabled = True
    Me.tm_datdoc.FieldName = "tm_datdoc"
    Me.tm_datdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_datdoc.Name = "tm_datdoc"
    Me.tm_datdoc.NTSRepositoryComboBox = Nothing
    Me.tm_datdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_datdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_datdoc.NTSRepositoryItemText = Nothing
    Me.tm_datdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_datdoc.OptionsFilter.AllowFilter = False
    Me.tm_datdoc.Visible = True
    Me.tm_datdoc.VisibleIndex = 4
    Me.tm_datdoc.Width = 82
    '
    'tm_conto
    '
    Me.tm_conto.AppearanceCell.Options.UseBackColor = True
    Me.tm_conto.AppearanceCell.Options.UseTextOptions = True
    Me.tm_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_conto.Caption = "Codice conto"
    Me.tm_conto.Enabled = True
    Me.tm_conto.FieldName = "tm_conto"
    Me.tm_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_conto.Name = "tm_conto"
    Me.tm_conto.NTSRepositoryComboBox = Nothing
    Me.tm_conto.NTSRepositoryItemCheck = Nothing
    Me.tm_conto.NTSRepositoryItemMemo = Nothing
    Me.tm_conto.NTSRepositoryItemText = Nothing
    Me.tm_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_conto.OptionsFilter.AllowFilter = False
    Me.tm_conto.Visible = True
    Me.tm_conto.VisibleIndex = 5
    Me.tm_conto.Width = 90
    '
    'xx_conto
    '
    Me.xx_conto.AppearanceCell.Options.UseBackColor = True
    Me.xx_conto.AppearanceCell.Options.UseTextOptions = True
    Me.xx_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_conto.Caption = "Descr. conto"
    Me.xx_conto.Enabled = True
    Me.xx_conto.FieldName = "xx_conto"
    Me.xx_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_conto.Name = "xx_conto"
    Me.xx_conto.NTSRepositoryComboBox = Nothing
    Me.xx_conto.NTSRepositoryItemCheck = Nothing
    Me.xx_conto.NTSRepositoryItemMemo = Nothing
    Me.xx_conto.NTSRepositoryItemText = Nothing
    Me.xx_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_conto.OptionsFilter.AllowFilter = False
    Me.xx_conto.Visible = True
    Me.xx_conto.VisibleIndex = 6
    Me.xx_conto.Width = 112
    '
    'tm_vistato
    '
    Me.tm_vistato.AppearanceCell.Options.UseBackColor = True
    Me.tm_vistato.AppearanceCell.Options.UseTextOptions = True
    Me.tm_vistato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_vistato.Caption = "Vistato"
    Me.tm_vistato.Enabled = True
    Me.tm_vistato.FieldName = "tm_vistato"
    Me.tm_vistato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_vistato.Name = "tm_vistato"
    Me.tm_vistato.NTSRepositoryComboBox = Nothing
    Me.tm_vistato.NTSRepositoryItemCheck = Nothing
    Me.tm_vistato.NTSRepositoryItemMemo = Nothing
    Me.tm_vistato.NTSRepositoryItemText = Nothing
    Me.tm_vistato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_vistato.OptionsFilter.AllowFilter = False
    Me.tm_vistato.Visible = True
    Me.tm_vistato.VisibleIndex = 7
    '
    'tm_totdoc
    '
    Me.tm_totdoc.AppearanceCell.Options.UseBackColor = True
    Me.tm_totdoc.AppearanceCell.Options.UseTextOptions = True
    Me.tm_totdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_totdoc.Caption = "Tot. documento"
    Me.tm_totdoc.Enabled = True
    Me.tm_totdoc.FieldName = "tm_totdoc"
    Me.tm_totdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_totdoc.Name = "tm_totdoc"
    Me.tm_totdoc.NTSRepositoryComboBox = Nothing
    Me.tm_totdoc.NTSRepositoryItemCheck = Nothing
    Me.tm_totdoc.NTSRepositoryItemMemo = Nothing
    Me.tm_totdoc.NTSRepositoryItemText = Nothing
    Me.tm_totdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_totdoc.OptionsFilter.AllowFilter = False
    Me.tm_totdoc.Visible = True
    Me.tm_totdoc.VisibleIndex = 8
    '
    'tm_totacceva
    '
    Me.tm_totacceva.AppearanceCell.Options.UseBackColor = True
    Me.tm_totacceva.AppearanceCell.Options.UseTextOptions = True
    Me.tm_totacceva.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_totacceva.Caption = "Tot. acconto"
    Me.tm_totacceva.Enabled = True
    Me.tm_totacceva.FieldName = "tm_totacceva"
    Me.tm_totacceva.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_totacceva.Name = "tm_totacceva"
    Me.tm_totacceva.NTSRepositoryComboBox = Nothing
    Me.tm_totacceva.NTSRepositoryItemCheck = Nothing
    Me.tm_totacceva.NTSRepositoryItemMemo = Nothing
    Me.tm_totacceva.NTSRepositoryItemText = Nothing
    Me.tm_totacceva.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_totacceva.OptionsFilter.AllowFilter = False
    Me.tm_totacceva.Visible = True
    Me.tm_totacceva.VisibleIndex = 9
    '
    'xx_residuo
    '
    Me.xx_residuo.AppearanceCell.Options.UseBackColor = True
    Me.xx_residuo.AppearanceCell.Options.UseTextOptions = True
    Me.xx_residuo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_residuo.Caption = "Tot. merce / Acc. residuo"
    Me.xx_residuo.Enabled = True
    Me.xx_residuo.FieldName = "xx_residuo"
    Me.xx_residuo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_residuo.Name = "xx_residuo"
    Me.xx_residuo.NTSRepositoryComboBox = Nothing
    Me.xx_residuo.NTSRepositoryItemCheck = Nothing
    Me.xx_residuo.NTSRepositoryItemMemo = Nothing
    Me.xx_residuo.NTSRepositoryItemText = Nothing
    Me.xx_residuo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_residuo.OptionsFilter.AllowFilter = False
    Me.xx_residuo.Visible = True
    Me.xx_residuo.VisibleIndex = 10
    '
    'tm_coddest
    '
    Me.tm_coddest.AppearanceCell.Options.UseBackColor = True
    Me.tm_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.tm_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_coddest.Caption = "Cod. destin."
    Me.tm_coddest.Enabled = True
    Me.tm_coddest.FieldName = "tm_coddest"
    Me.tm_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_coddest.Name = "tm_coddest"
    Me.tm_coddest.NTSRepositoryComboBox = Nothing
    Me.tm_coddest.NTSRepositoryItemCheck = Nothing
    Me.tm_coddest.NTSRepositoryItemMemo = Nothing
    Me.tm_coddest.NTSRepositoryItemText = Nothing
    Me.tm_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_coddest.OptionsFilter.AllowFilter = False
    Me.tm_coddest.Visible = True
    Me.tm_coddest.VisibleIndex = 11
    '
    'xx_destin
    '
    Me.xx_destin.AppearanceCell.Options.UseBackColor = True
    Me.xx_destin.AppearanceCell.Options.UseTextOptions = True
    Me.xx_destin.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_destin.Caption = "Descr. destin."
    Me.xx_destin.Enabled = True
    Me.xx_destin.FieldName = "xx_destin"
    Me.xx_destin.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_destin.Name = "xx_destin"
    Me.xx_destin.NTSRepositoryComboBox = Nothing
    Me.xx_destin.NTSRepositoryItemCheck = Nothing
    Me.xx_destin.NTSRepositoryItemMemo = Nothing
    Me.xx_destin.NTSRepositoryItemText = Nothing
    Me.xx_destin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_destin.OptionsFilter.AllowFilter = False
    Me.xx_destin.Visible = True
    Me.xx_destin.VisibleIndex = 12
    '
    'tm_commeca
    '
    Me.tm_commeca.AppearanceCell.Options.UseBackColor = True
    Me.tm_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.tm_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_commeca.Caption = "Commessa"
    Me.tm_commeca.Enabled = True
    Me.tm_commeca.FieldName = "tm_commeca"
    Me.tm_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_commeca.Name = "tm_commeca"
    Me.tm_commeca.NTSRepositoryComboBox = Nothing
    Me.tm_commeca.NTSRepositoryItemCheck = Nothing
    Me.tm_commeca.NTSRepositoryItemMemo = Nothing
    Me.tm_commeca.NTSRepositoryItemText = Nothing
    Me.tm_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_commeca.OptionsFilter.AllowFilter = False
    Me.tm_commeca.Visible = True
    Me.tm_commeca.VisibleIndex = 13
    '
    'xx_commeca
    '
    Me.xx_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xx_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xx_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_commeca.Caption = "Descr. commeca"
    Me.xx_commeca.Enabled = True
    Me.xx_commeca.FieldName = "xx_commeca"
    Me.xx_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_commeca.Name = "xx_commeca"
    Me.xx_commeca.NTSRepositoryComboBox = Nothing
    Me.xx_commeca.NTSRepositoryItemCheck = Nothing
    Me.xx_commeca.NTSRepositoryItemMemo = Nothing
    Me.xx_commeca.NTSRepositoryItemText = Nothing
    Me.xx_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_commeca.OptionsFilter.AllowFilter = False
    Me.xx_commeca.Visible = True
    Me.xx_commeca.VisibleIndex = 14
    '
    'tm_codpaga
    '
    Me.tm_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.tm_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.tm_codpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_codpaga.Caption = "Cod. pagam."
    Me.tm_codpaga.Enabled = True
    Me.tm_codpaga.FieldName = "tm_codpaga"
    Me.tm_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_codpaga.Name = "tm_codpaga"
    Me.tm_codpaga.NTSRepositoryComboBox = Nothing
    Me.tm_codpaga.NTSRepositoryItemCheck = Nothing
    Me.tm_codpaga.NTSRepositoryItemMemo = Nothing
    Me.tm_codpaga.NTSRepositoryItemText = Nothing
    Me.tm_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_codpaga.OptionsFilter.AllowFilter = False
    Me.tm_codpaga.Visible = True
    Me.tm_codpaga.VisibleIndex = 15
    '
    'xx_codpaga
    '
    Me.xx_codpaga.AppearanceCell.Options.UseBackColor = True
    Me.xx_codpaga.AppearanceCell.Options.UseTextOptions = True
    Me.xx_codpaga.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_codpaga.Caption = "Descr. pagam."
    Me.xx_codpaga.Enabled = True
    Me.xx_codpaga.FieldName = "xx_codpaga"
    Me.xx_codpaga.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_codpaga.Name = "xx_codpaga"
    Me.xx_codpaga.NTSRepositoryComboBox = Nothing
    Me.xx_codpaga.NTSRepositoryItemCheck = Nothing
    Me.xx_codpaga.NTSRepositoryItemMemo = Nothing
    Me.xx_codpaga.NTSRepositoryItemText = Nothing
    Me.xx_codpaga.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_codpaga.OptionsFilter.AllowFilter = False
    Me.xx_codpaga.Visible = True
    Me.xx_codpaga.VisibleIndex = 16
    '
    'tm_riferim
    '
    Me.tm_riferim.AppearanceCell.Options.UseBackColor = True
    Me.tm_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.tm_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_riferim.Caption = "Riferimenti"
    Me.tm_riferim.Enabled = True
    Me.tm_riferim.FieldName = "tm_riferim"
    Me.tm_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_riferim.Name = "tm_riferim"
    Me.tm_riferim.NTSRepositoryComboBox = Nothing
    Me.tm_riferim.NTSRepositoryItemCheck = Nothing
    Me.tm_riferim.NTSRepositoryItemMemo = Nothing
    Me.tm_riferim.NTSRepositoryItemText = Nothing
    Me.tm_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_riferim.OptionsFilter.AllowFilter = False
    Me.tm_riferim.Visible = True
    Me.tm_riferim.VisibleIndex = 17
    '
    'tm_annpar
    '
    Me.tm_annpar.AppearanceCell.Options.UseBackColor = True
    Me.tm_annpar.AppearanceCell.Options.UseTextOptions = True
    Me.tm_annpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_annpar.Caption = "Annp P."
    Me.tm_annpar.Enabled = True
    Me.tm_annpar.FieldName = "tm_annpar"
    Me.tm_annpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_annpar.Name = "tm_annpar"
    Me.tm_annpar.NTSRepositoryComboBox = Nothing
    Me.tm_annpar.NTSRepositoryItemCheck = Nothing
    Me.tm_annpar.NTSRepositoryItemMemo = Nothing
    Me.tm_annpar.NTSRepositoryItemText = Nothing
    Me.tm_annpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_annpar.OptionsFilter.AllowFilter = False
    Me.tm_annpar.Visible = True
    Me.tm_annpar.VisibleIndex = 18
    '
    'tm_alfpar
    '
    Me.tm_alfpar.AppearanceCell.Options.UseBackColor = True
    Me.tm_alfpar.AppearanceCell.Options.UseTextOptions = True
    Me.tm_alfpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_alfpar.Caption = "Serie P."
    Me.tm_alfpar.Enabled = True
    Me.tm_alfpar.FieldName = "tm_alfpar"
    Me.tm_alfpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_alfpar.Name = "tm_alfpar"
    Me.tm_alfpar.NTSRepositoryComboBox = Nothing
    Me.tm_alfpar.NTSRepositoryItemCheck = Nothing
    Me.tm_alfpar.NTSRepositoryItemMemo = Nothing
    Me.tm_alfpar.NTSRepositoryItemText = Nothing
    Me.tm_alfpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_alfpar.OptionsFilter.AllowFilter = False
    Me.tm_alfpar.Visible = True
    Me.tm_alfpar.VisibleIndex = 19
    '
    'tm_numpar
    '
    Me.tm_numpar.AppearanceCell.Options.UseBackColor = True
    Me.tm_numpar.AppearanceCell.Options.UseTextOptions = True
    Me.tm_numpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_numpar.Caption = "Numero P."
    Me.tm_numpar.Enabled = True
    Me.tm_numpar.FieldName = "tm_numpar"
    Me.tm_numpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_numpar.Name = "tm_numpar"
    Me.tm_numpar.NTSRepositoryComboBox = Nothing
    Me.tm_numpar.NTSRepositoryItemCheck = Nothing
    Me.tm_numpar.NTSRepositoryItemMemo = Nothing
    Me.tm_numpar.NTSRepositoryItemText = Nothing
    Me.tm_numpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_numpar.OptionsFilter.AllowFilter = False
    Me.tm_numpar.Visible = True
    Me.tm_numpar.VisibleIndex = 20
    '
    'tm_datpar
    '
    Me.tm_datpar.AppearanceCell.Options.UseBackColor = True
    Me.tm_datpar.AppearanceCell.Options.UseTextOptions = True
    Me.tm_datpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_datpar.Caption = "Data P."
    Me.tm_datpar.Enabled = True
    Me.tm_datpar.FieldName = "tm_datpar"
    Me.tm_datpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_datpar.Name = "tm_datpar"
    Me.tm_datpar.NTSRepositoryComboBox = Nothing
    Me.tm_datpar.NTSRepositoryItemCheck = Nothing
    Me.tm_datpar.NTSRepositoryItemMemo = Nothing
    Me.tm_datpar.NTSRepositoryItemText = Nothing
    Me.tm_datpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_datpar.OptionsFilter.AllowFilter = False
    Me.tm_datpar.Visible = True
    Me.tm_datpar.VisibleIndex = 21
    '
    'tm_tipobf
    '
    Me.tm_tipobf.AppearanceCell.Options.UseBackColor = True
    Me.tm_tipobf.AppearanceCell.Options.UseTextOptions = True
    Me.tm_tipobf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.tm_tipobf.Caption = "Tipo B/f"
    Me.tm_tipobf.Enabled = True
    Me.tm_tipobf.FieldName = "tm_tipobf"
    Me.tm_tipobf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.tm_tipobf.Name = "tm_tipobf"
    Me.tm_tipobf.NTSRepositoryComboBox = Nothing
    Me.tm_tipobf.NTSRepositoryItemCheck = Nothing
    Me.tm_tipobf.NTSRepositoryItemMemo = Nothing
    Me.tm_tipobf.NTSRepositoryItemText = Nothing
    Me.tm_tipobf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.tm_tipobf.OptionsFilter.AllowFilter = False
    Me.tm_tipobf.Visible = True
    Me.tm_tipobf.VisibleIndex = 22
    '
    'xx_tipobf
    '
    Me.xx_tipobf.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipobf.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipobf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipobf.Caption = "Descr. tipo B/f"
    Me.xx_tipobf.Enabled = True
    Me.xx_tipobf.FieldName = "xx_tipobf"
    Me.xx_tipobf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tipobf.Name = "xx_tipobf"
    Me.xx_tipobf.NTSRepositoryComboBox = Nothing
    Me.xx_tipobf.NTSRepositoryItemCheck = Nothing
    Me.xx_tipobf.NTSRepositoryItemMemo = Nothing
    Me.xx_tipobf.NTSRepositoryItemText = Nothing
    Me.xx_tipobf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.xx_tipobf.OptionsFilter.AllowFilter = False
    Me.xx_tipobf.Visible = True
    Me.xx_tipobf.VisibleIndex = 23
    '
    'pnSel0
    '
    Me.pnSel0.AllowDrop = True
    Me.pnSel0.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSel0.Appearance.Options.UseBackColor = True
    Me.pnSel0.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSel0.Controls.Add(Me.pnCommandbutton)
    Me.pnSel0.Controls.Add(Me.edNumpar)
    Me.pnSel0.Controls.Add(Me.lbDaconto)
    Me.pnSel0.Controls.Add(Me.edDataDa)
    Me.pnSel0.Controls.Add(Me.lbNumpar)
    Me.pnSel0.Controls.Add(Me.lbAdata)
    Me.pnSel0.Controls.Add(Me.edDataA)
    Me.pnSel0.Controls.Add(Me.lbAnno)
    Me.pnSel0.Controls.Add(Me.lbATipobf)
    Me.pnSel0.Controls.Add(Me.edContoDa)
    Me.pnSel0.Controls.Add(Me.edDaTipobf)
    Me.pnSel0.Controls.Add(Me.lbDaTipobf)
    Me.pnSel0.Controls.Add(Me.lbSerie)
    Me.pnSel0.Controls.Add(Me.edContoA)
    Me.pnSel0.Controls.Add(Me.edATipobf)
    Me.pnSel0.Controls.Add(Me.lbDadata)
    Me.pnSel0.Controls.Add(Me.edSerie)
    Me.pnSel0.Controls.Add(Me.lbRiferim)
    Me.pnSel0.Controls.Add(Me.edRiferim)
    Me.pnSel0.Controls.Add(Me.lbAconto)
    Me.pnSel0.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnSel0.Location = New System.Drawing.Point(0, 0)
    Me.pnSel0.Name = "pnSel0"
    Me.pnSel0.NTSActiveTrasparency = True
    Me.pnSel0.Size = New System.Drawing.Size(615, 122)
    Me.pnSel0.TabIndex = 0
    Me.pnSel0.Text = "NtsPanel1"
    '
    'pnCommandbutton
    '
    Me.pnCommandbutton.AllowDrop = True
    Me.pnCommandbutton.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCommandbutton.Appearance.Options.UseBackColor = True
    Me.pnCommandbutton.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCommandbutton.Controls.Add(Me.cmdDesAll)
    Me.pnCommandbutton.Controls.Add(Me.cmdSelAll)
    Me.pnCommandbutton.Controls.Add(Me.cmdRicerca)
    Me.pnCommandbutton.Controls.Add(Me.cmdAnnulla)
    Me.pnCommandbutton.Controls.Add(Me.cmdSeleziona)
    Me.pnCommandbutton.Location = New System.Drawing.Point(500, 0)
    Me.pnCommandbutton.Name = "pnCommandbutton"
    Me.pnCommandbutton.NTSActiveTrasparency = True
    Me.pnCommandbutton.Size = New System.Drawing.Size(123, 122)
    Me.pnCommandbutton.TabIndex = 56
    Me.pnCommandbutton.Text = "NtsPanel1"
    '
    'cmdRicerca
    '
    Me.cmdRicerca.ImagePath = ""
    Me.cmdRicerca.ImageText = ""
    Me.cmdRicerca.Location = New System.Drawing.Point(3, 3)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.NTSContextMenu = Nothing
    Me.cmdRicerca.Size = New System.Drawing.Size(110, 23)
    Me.cmdRicerca.TabIndex = 50
    Me.cmdRicerca.Text = "&Ricerca"
    '
    'edNumpar
    '
    Me.edNumpar.Cursor = System.Windows.Forms.Cursors.Default
    Me.edNumpar.EditValue = "0"
    Me.edNumpar.Location = New System.Drawing.Point(410, 77)
    Me.edNumpar.Name = "edNumpar"
    Me.edNumpar.NTSDbField = ""
    Me.edNumpar.NTSFormat = "0"
    Me.edNumpar.NTSForzaVisZoom = False
    Me.edNumpar.NTSOldValue = ""
    Me.edNumpar.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edNumpar.Properties.Appearance.Options.UseBackColor = True
    Me.edNumpar.Properties.Appearance.Options.UseTextOptions = True
    Me.edNumpar.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edNumpar.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edNumpar.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edNumpar.Properties.AutoHeight = False
    Me.edNumpar.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edNumpar.Properties.MaxLength = 65536
    Me.edNumpar.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edNumpar.Size = New System.Drawing.Size(84, 20)
    Me.edNumpar.TabIndex = 55
    '
    'lbDaconto
    '
    Me.lbDaconto.AutoSize = True
    Me.lbDaconto.BackColor = System.Drawing.Color.Transparent
    Me.lbDaconto.Location = New System.Drawing.Point(3, 7)
    Me.lbDaconto.Name = "lbDaconto"
    Me.lbDaconto.NTSDbField = ""
    Me.lbDaconto.Size = New System.Drawing.Size(50, 13)
    Me.lbDaconto.TabIndex = 7
    Me.lbDaconto.Text = "Da conto"
    Me.lbDaconto.Tooltip = ""
    Me.lbDaconto.UseMnemonic = False
    '
    'edDataDa
    '
    Me.edDataDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataDa.Location = New System.Drawing.Point(86, 29)
    Me.edDataDa.Name = "edDataDa"
    Me.edDataDa.NTSDbField = ""
    Me.edDataDa.NTSForzaVisZoom = False
    Me.edDataDa.NTSOldValue = ""
    Me.edDataDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDataDa.Properties.Appearance.Options.UseBackColor = True
    Me.edDataDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataDa.Properties.AutoHeight = False
    Me.edDataDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataDa.Properties.MaxLength = 65536
    Me.edDataDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataDa.Size = New System.Drawing.Size(84, 20)
    Me.edDataDa.TabIndex = 13
    '
    'lbNumpar
    '
    Me.lbNumpar.AutoSize = True
    Me.lbNumpar.BackColor = System.Drawing.Color.Transparent
    Me.lbNumpar.Location = New System.Drawing.Point(334, 80)
    Me.lbNumpar.Name = "lbNumpar"
    Me.lbNumpar.NTSDbField = ""
    Me.lbNumpar.Size = New System.Drawing.Size(67, 13)
    Me.lbNumpar.TabIndex = 54
    Me.lbNumpar.Text = "Num. partita"
    Me.lbNumpar.Tooltip = ""
    Me.lbNumpar.UseMnemonic = False
    '
    'lbAdata
    '
    Me.lbAdata.AutoSize = True
    Me.lbAdata.BackColor = System.Drawing.Color.Transparent
    Me.lbAdata.Location = New System.Drawing.Point(176, 32)
    Me.lbAdata.Name = "lbAdata"
    Me.lbAdata.NTSDbField = ""
    Me.lbAdata.Size = New System.Drawing.Size(39, 13)
    Me.lbAdata.TabIndex = 12
    Me.lbAdata.Text = "A data"
    Me.lbAdata.Tooltip = ""
    Me.lbAdata.UseMnemonic = False
    '
    'edDataA
    '
    Me.edDataA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataA.Location = New System.Drawing.Point(244, 29)
    Me.edDataA.Name = "edDataA"
    Me.edDataA.NTSDbField = ""
    Me.edDataA.NTSForzaVisZoom = False
    Me.edDataA.NTSOldValue = ""
    Me.edDataA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDataA.Properties.Appearance.Options.UseBackColor = True
    Me.edDataA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDataA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDataA.Properties.AutoHeight = False
    Me.edDataA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDataA.Properties.MaxLength = 65536
    Me.edDataA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDataA.Size = New System.Drawing.Size(84, 20)
    Me.edDataA.TabIndex = 14
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.Location = New System.Drawing.Point(334, 7)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.Size = New System.Drawing.Size(36, 13)
    Me.lbAnno.TabIndex = 53
    Me.lbAnno.Text = "Anno:"
    '
    'lbATipobf
    '
    Me.lbATipobf.AutoSize = True
    Me.lbATipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbATipobf.Location = New System.Drawing.Point(176, 57)
    Me.lbATipobf.Name = "lbATipobf"
    Me.lbATipobf.NTSDbField = ""
    Me.lbATipobf.Size = New System.Drawing.Size(48, 13)
    Me.lbATipobf.TabIndex = 11
    Me.lbATipobf.Text = "A tipo Bf"
    Me.lbATipobf.Tooltip = ""
    Me.lbATipobf.UseMnemonic = False
    '
    'edContoDa
    '
    Me.edContoDa.Cursor = System.Windows.Forms.Cursors.Hand
    Me.edContoDa.EditValue = "0"
    Me.edContoDa.Location = New System.Drawing.Point(86, 4)
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
    Me.edContoDa.Size = New System.Drawing.Size(84, 20)
    Me.edContoDa.TabIndex = 45
    '
    'edDaTipobf
    '
    Me.edDaTipobf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaTipobf.EditValue = "0"
    Me.edDaTipobf.Location = New System.Drawing.Point(86, 54)
    Me.edDaTipobf.Name = "edDaTipobf"
    Me.edDaTipobf.NTSDbField = ""
    Me.edDaTipobf.NTSFormat = "0"
    Me.edDaTipobf.NTSForzaVisZoom = False
    Me.edDaTipobf.NTSOldValue = ""
    Me.edDaTipobf.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDaTipobf.Properties.Appearance.Options.UseBackColor = True
    Me.edDaTipobf.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaTipobf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaTipobf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaTipobf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaTipobf.Properties.AutoHeight = False
    Me.edDaTipobf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaTipobf.Properties.MaxLength = 65536
    Me.edDaTipobf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaTipobf.Size = New System.Drawing.Size(84, 20)
    Me.edDaTipobf.TabIndex = 52
    '
    'lbDaTipobf
    '
    Me.lbDaTipobf.AutoSize = True
    Me.lbDaTipobf.BackColor = System.Drawing.Color.Transparent
    Me.lbDaTipobf.Location = New System.Drawing.Point(3, 57)
    Me.lbDaTipobf.Name = "lbDaTipobf"
    Me.lbDaTipobf.NTSDbField = ""
    Me.lbDaTipobf.Size = New System.Drawing.Size(54, 13)
    Me.lbDaTipobf.TabIndex = 10
    Me.lbDaTipobf.Text = "Da tipo Bf"
    Me.lbDaTipobf.Tooltip = ""
    Me.lbDaTipobf.UseMnemonic = False
    '
    'lbSerie
    '
    Me.lbSerie.AutoSize = True
    Me.lbSerie.BackColor = System.Drawing.Color.Transparent
    Me.lbSerie.Location = New System.Drawing.Point(415, 7)
    Me.lbSerie.Name = "lbSerie"
    Me.lbSerie.NTSDbField = ""
    Me.lbSerie.Size = New System.Drawing.Size(31, 13)
    Me.lbSerie.TabIndex = 5
    Me.lbSerie.Text = "Serie"
    Me.lbSerie.Tooltip = ""
    Me.lbSerie.UseMnemonic = False
    '
    'edContoA
    '
    Me.edContoA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContoA.EditValue = "999999999"
    Me.edContoA.Location = New System.Drawing.Point(244, 4)
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
    Me.edContoA.Size = New System.Drawing.Size(84, 20)
    Me.edContoA.TabIndex = 46
    '
    'edATipobf
    '
    Me.edATipobf.Cursor = System.Windows.Forms.Cursors.Default
    Me.edATipobf.EditValue = "9999"
    Me.edATipobf.Location = New System.Drawing.Point(244, 54)
    Me.edATipobf.Name = "edATipobf"
    Me.edATipobf.NTSDbField = ""
    Me.edATipobf.NTSFormat = "0"
    Me.edATipobf.NTSForzaVisZoom = False
    Me.edATipobf.NTSOldValue = "9999"
    Me.edATipobf.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edATipobf.Properties.Appearance.Options.UseBackColor = True
    Me.edATipobf.Properties.Appearance.Options.UseTextOptions = True
    Me.edATipobf.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edATipobf.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edATipobf.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edATipobf.Properties.AutoHeight = False
    Me.edATipobf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edATipobf.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edATipobf.Size = New System.Drawing.Size(84, 20)
    Me.edATipobf.TabIndex = 51
    '
    'lbDadata
    '
    Me.lbDadata.AutoSize = True
    Me.lbDadata.BackColor = System.Drawing.Color.Transparent
    Me.lbDadata.Location = New System.Drawing.Point(3, 32)
    Me.lbDadata.Name = "lbDadata"
    Me.lbDadata.NTSDbField = ""
    Me.lbDadata.Size = New System.Drawing.Size(45, 13)
    Me.lbDadata.TabIndex = 9
    Me.lbDadata.Text = "Da data"
    Me.lbDadata.Tooltip = ""
    Me.lbDadata.UseMnemonic = False
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.SizeNWSE
    Me.edSerie.Location = New System.Drawing.Point(449, 4)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edSerie.Properties.Appearance.Options.UseBackColor = True
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.AutoHeight = False
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(45, 20)
    Me.edSerie.TabIndex = 6
    '
    'lbRiferim
    '
    Me.lbRiferim.AutoSize = True
    Me.lbRiferim.BackColor = System.Drawing.Color.Transparent
    Me.lbRiferim.Location = New System.Drawing.Point(3, 80)
    Me.lbRiferim.Name = "lbRiferim"
    Me.lbRiferim.NTSDbField = ""
    Me.lbRiferim.Size = New System.Drawing.Size(84, 13)
    Me.lbRiferim.TabIndex = 49
    Me.lbRiferim.Text = "Riferimenti (like)"
    Me.lbRiferim.Tooltip = ""
    Me.lbRiferim.UseMnemonic = False
    '
    'edRiferim
    '
    Me.edRiferim.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRiferim.Location = New System.Drawing.Point(87, 77)
    Me.edRiferim.Name = "edRiferim"
    Me.edRiferim.NTSDbField = ""
    Me.edRiferim.NTSForzaVisZoom = False
    Me.edRiferim.NTSOldValue = ""
    Me.edRiferim.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edRiferim.Properties.Appearance.Options.UseBackColor = True
    Me.edRiferim.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edRiferim.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edRiferim.Properties.AutoHeight = False
    Me.edRiferim.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edRiferim.Properties.MaxLength = 65536
    Me.edRiferim.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edRiferim.Size = New System.Drawing.Size(241, 20)
    Me.edRiferim.TabIndex = 50
    '
    'lbAconto
    '
    Me.lbAconto.AutoSize = True
    Me.lbAconto.BackColor = System.Drawing.Color.Transparent
    Me.lbAconto.Location = New System.Drawing.Point(176, 7)
    Me.lbAconto.Name = "lbAconto"
    Me.lbAconto.NTSDbField = ""
    Me.lbAconto.Size = New System.Drawing.Size(44, 13)
    Me.lbAconto.TabIndex = 8
    Me.lbAconto.Text = "A conto"
    Me.lbAconto.Tooltip = ""
    Me.lbAconto.UseMnemonic = False
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnSel1)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(615, 462)
    Me.NtsTabPage2.Text = "Filtri Estesi"
    '
    'pnSel1
    '
    Me.pnSel1.AllowDrop = True
    Me.pnSel1.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSel1.Appearance.Options.UseBackColor = True
    Me.pnSel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSel1.Controls.Add(Me.ceFiltriExt)
    Me.pnSel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnSel1.Location = New System.Drawing.Point(0, 0)
    Me.pnSel1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnSel1.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnSel1.Name = "pnSel1"
    Me.pnSel1.NTSActiveTrasparency = True
    Me.pnSel1.Size = New System.Drawing.Size(615, 462)
    Me.pnSel1.TabIndex = 0
    Me.pnSel1.Text = "NtsPanel1"
    '
    'ceFiltriExt
    '
    Me.ceFiltriExt.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ceFiltriExt.Location = New System.Drawing.Point(0, 0)
    Me.ceFiltriExt.MinimumSize = New System.Drawing.Size(399, 193)
    Me.ceFiltriExt.Name = "ceFiltriExt"
    Me.ceFiltriExt.oParent = Nothing
    Me.ceFiltriExt.Size = New System.Drawing.Size(615, 462)
    Me.ceFiltriExt.strNomeCampo = ""
    Me.ceFiltriExt.TabIndex = 0
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.pnfiltri)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(624, 492)
    Me.pnTop.TabIndex = 50
    '
    'grFiltri1
    '
    Me.grFiltri1.EmbeddedNavigator.Name = ""
    Me.grFiltri1.Location = New System.Drawing.Point(0, 0)
    Me.grFiltri1.MainView = Me.NtsGridView1
    Me.grFiltri1.Name = "grFiltri1"
    Me.grFiltri1.Size = New System.Drawing.Size(400, 200)
    Me.grFiltri1.TabIndex = 0
    Me.grFiltri1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.NtsGridView1})
    '
    'NtsGridView1
    '
    Me.NtsGridView1.Enabled = True
    Me.NtsGridView1.GridControl = Me.grFiltri1
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
    Me.NtsGridView1.OptionsView.ShowGroupPanel = False
    Me.NtsGridView1.RowHeight = 14
    '
    'grvFiltri1
    '
    Me.grvFiltri1.ActiveFilterEnabled = False
    Me.grvFiltri1.Enabled = True
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
    Me.grvFiltri1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvFiltri1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvFiltri1.OptionsView.ShowGroupPanel = False
    Me.grvFiltri1.RowHeight = 14
    '
    'xx_nome
    '
    Me.xx_nome.AppearanceCell.Options.UseBackColor = True
    Me.xx_nome.AppearanceCell.Options.UseTextOptions = True
    Me.xx_nome.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_nome.Enabled = True
    Me.xx_nome.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_nome.Name = "xx_nome"
    Me.xx_nome.NTSRepositoryComboBox = Nothing
    Me.xx_nome.NTSRepositoryItemCheck = Nothing
    Me.xx_nome.NTSRepositoryItemMemo = Nothing
    Me.xx_nome.NTSRepositoryItemText = Nothing
    '
    'xx_valoreda
    '
    Me.xx_valoreda.AppearanceCell.Options.UseBackColor = True
    Me.xx_valoreda.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valoreda.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valoreda.Enabled = True
    Me.xx_valoreda.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valoreda.Name = "xx_valoreda"
    Me.xx_valoreda.NTSRepositoryComboBox = Nothing
    Me.xx_valoreda.NTSRepositoryItemCheck = Nothing
    Me.xx_valoreda.NTSRepositoryItemMemo = Nothing
    Me.xx_valoreda.NTSRepositoryItemText = Nothing
    '
    'xx_valorea
    '
    Me.xx_valorea.AppearanceCell.Options.UseBackColor = True
    Me.xx_valorea.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valorea.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valorea.Enabled = True
    Me.xx_valorea.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valorea.Name = "xx_valorea"
    Me.xx_valorea.NTSRepositoryComboBox = Nothing
    Me.xx_valorea.NTSRepositoryItemCheck = Nothing
    Me.xx_valorea.NTSRepositoryItemMemo = Nothing
    Me.xx_valorea.NTSRepositoryItemText = Nothing
    '
    'NtsTabPage3
    '
    Me.NtsTabPage3.AllowDrop = True
    Me.NtsTabPage3.Enable = True
    Me.NtsTabPage3.Name = "NtsTabPage3"
    Me.NtsTabPage3.Size = New System.Drawing.Size(0, 0)
    '
    'pnSel2
    '
    Me.pnSel2.AllowDrop = True
    Me.pnSel2.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnSel2.Appearance.Options.UseBackColor = True
    Me.pnSel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnSel2.Location = New System.Drawing.Point(0, 0)
    Me.pnSel2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnSel2.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnSel2.Name = "pnSel2"
    Me.pnSel2.NTSActiveTrasparency = True
    Me.pnSel2.Size = New System.Drawing.Size(200, 100)
    Me.pnSel2.TabIndex = 0
    '
    'grFiltri2
    '
    Me.grFiltri2.EmbeddedNavigator.Name = ""
    Me.grFiltri2.Location = New System.Drawing.Point(0, 0)
    Me.grFiltri2.MainView = Me.NtsGridView2
    Me.grFiltri2.Name = "grFiltri2"
    Me.grFiltri2.Size = New System.Drawing.Size(400, 200)
    Me.grFiltri2.TabIndex = 0
    Me.grFiltri2.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.NtsGridView2})
    '
    'NtsGridView2
    '
    Me.NtsGridView2.Enabled = True
    Me.NtsGridView2.GridControl = Me.grFiltri2
    Me.NtsGridView2.MinRowHeight = 14
    Me.NtsGridView2.Name = "NtsGridView2"
    Me.NtsGridView2.NTSAllowDelete = True
    Me.NtsGridView2.NTSAllowInsert = True
    Me.NtsGridView2.NTSAllowUpdate = True
    Me.NtsGridView2.NTSMenuContext = Nothing
    Me.NtsGridView2.OptionsCustomization.AllowRowSizing = True
    Me.NtsGridView2.OptionsFilter.AllowFilterEditor = False
    Me.NtsGridView2.OptionsNavigation.EnterMoveNextColumn = True
    Me.NtsGridView2.OptionsNavigation.UseTabKey = False
    Me.NtsGridView2.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.NtsGridView2.OptionsView.ColumnAutoWidth = False
    Me.NtsGridView2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.NtsGridView2.OptionsView.ShowGroupPanel = False
    Me.NtsGridView2.RowHeight = 14
    '
    'grvFiltri2
    '
    Me.grvFiltri2.ActiveFilterEnabled = False
    Me.grvFiltri2.Enabled = True
    Me.grvFiltri2.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvFiltri2.MinRowHeight = 14
    Me.grvFiltri2.Name = "grvFiltri2"
    Me.grvFiltri2.NTSAllowDelete = True
    Me.grvFiltri2.NTSAllowInsert = True
    Me.grvFiltri2.NTSAllowUpdate = True
    Me.grvFiltri2.NTSMenuContext = Nothing
    Me.grvFiltri2.OptionsCustomization.AllowRowSizing = True
    Me.grvFiltri2.OptionsFilter.AllowFilterEditor = False
    Me.grvFiltri2.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvFiltri2.OptionsNavigation.UseTabKey = False
    Me.grvFiltri2.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvFiltri2.OptionsView.ColumnAutoWidth = False
    Me.grvFiltri2.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvFiltri2.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvFiltri2.OptionsView.ShowGroupPanel = False
    Me.grvFiltri2.RowHeight = 14
    '
    'xx_nome2
    '
    Me.xx_nome2.AppearanceCell.Options.UseBackColor = True
    Me.xx_nome2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_nome2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_nome2.Enabled = True
    Me.xx_nome2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_nome2.Name = "xx_nome2"
    Me.xx_nome2.NTSRepositoryComboBox = Nothing
    Me.xx_nome2.NTSRepositoryItemCheck = Nothing
    Me.xx_nome2.NTSRepositoryItemMemo = Nothing
    Me.xx_nome2.NTSRepositoryItemText = Nothing
    '
    'xx_valoreda2
    '
    Me.xx_valoreda2.AppearanceCell.Options.UseBackColor = True
    Me.xx_valoreda2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valoreda2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valoreda2.Enabled = True
    Me.xx_valoreda2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valoreda2.Name = "xx_valoreda2"
    Me.xx_valoreda2.NTSRepositoryComboBox = Nothing
    Me.xx_valoreda2.NTSRepositoryItemCheck = Nothing
    Me.xx_valoreda2.NTSRepositoryItemMemo = Nothing
    Me.xx_valoreda2.NTSRepositoryItemText = Nothing
    '
    'xx_valorea2
    '
    Me.xx_valorea2.AppearanceCell.Options.UseBackColor = True
    Me.xx_valorea2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_valorea2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_valorea2.Enabled = True
    Me.xx_valorea2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_valorea2.Name = "xx_valorea2"
    Me.xx_valorea2.NTSRepositoryComboBox = Nothing
    Me.xx_valorea2.NTSRepositoryItemCheck = Nothing
    Me.xx_valorea2.NTSRepositoryItemMemo = Nothing
    Me.xx_valorea2.NTSRepositoryItemText = Nothing
    '
    'cmdLock
    '
    Me.cmdLock.ImagePath = ""
    Me.cmdLock.ImageText = ""
    Me.cmdLock.Location = New System.Drawing.Point(0, 0)
    Me.cmdLock.Name = "cmdLock"
    Me.cmdLock.NTSContextMenu = Nothing
    Me.cmdLock.Size = New System.Drawing.Size(75, 23)
    Me.cmdLock.TabIndex = 0
    '
    'cmdDesAll
    '
    Me.cmdDesAll.ImagePath = ""
    Me.cmdDesAll.ImageText = ""
    Me.cmdDesAll.Location = New System.Drawing.Point(3, 73)
    Me.cmdDesAll.Name = "cmdDesAll"
    Me.cmdDesAll.NTSContextMenu = Nothing
    Me.cmdDesAll.Size = New System.Drawing.Size(110, 23)
    Me.cmdDesAll.TabIndex = 56
    Me.cmdDesAll.Text = "Deseleziona tutto"
    '
    'cmdSelAll
    '
    Me.cmdSelAll.ImagePath = ""
    Me.cmdSelAll.ImageText = ""
    Me.cmdSelAll.Location = New System.Drawing.Point(3, 49)
    Me.cmdSelAll.Name = "cmdSelAll"
    Me.cmdSelAll.NTSContextMenu = Nothing
    Me.cmdSelAll.Size = New System.Drawing.Size(110, 23)
    Me.cmdSelAll.TabIndex = 55
    Me.cmdSelAll.Text = "Seleziona tutto"
    '
    'FRMVEFIDO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdAnnulla
    Me.ClientSize = New System.Drawing.Size(624, 492)
    Me.Controls.Add(Me.pnTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.MinimizeBox = False
    Me.Name = "FRMVEFIDO"
    Me.NTSLastControlFocussed = Me.grSedoc
    Me.Text = "SELEZIONE DOCUMENTI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnfiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnfiltri.ResumeLayout(False)
    CType(Me.tsSel, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSel.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grSedoc, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvSedoc, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSel0, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSel0.ResumeLayout(False)
    Me.pnSel0.PerformLayout()
    CType(Me.pnCommandbutton, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCommandbutton.ResumeLayout(False)
    CType(Me.edNumpar.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaTipobf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edATipobf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRiferim.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnSel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSel1.ResumeLayout(False)
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    CType(Me.grFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSel2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grFiltri2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGridView2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFiltri2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub FRMVEFIDO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strSoloSerie As String = ""
    Try
      '-------------------------
      'predispongo i controlli
      InitControls()

      lbAnno.Text = oApp.Tr(Me, 128674147271246250, "Anno: ") & oParam.nAnno.ToString

      edContoDa.Text = "0"
      edContoA.Text = "999999999"
      edDataDa.Text = IntSetDate("01/01/" & oParam.nAnno.ToString)
      edDataA.Text = IntSetDate("31/12/2099")
      edDaTipobf.Text = "0"
      edATipobf.Text = "9999"
      edSerie.Text = ""

      tsSel.SelectedTabPageIndex = 0
      tsSel.Enabled = True

      '-------------------------------
      'applico le impostazioni da GCTL
      GctlTipoDoc = " "
      GctlSetRoules()
      GctlApplicaDefaultValue()

      If oParam.nTipologia = 1 Then
        'se chiamato da bsveboll o da fdin
        strSoloSerie = oMenu.GetSettingBusDitt(DittaCorrente, "BSVEBOLL", "OpzioniDocUt", ".", "SoloSerie", "?", oParam.strTipo, "?")
        If strSoloSerie = "" Then strSoloSerie = " "
        If strSoloSerie <> "?" Then
          edSerie.Text = strSoloSerie
          edSerie.Enabled = False
        End If
        'posso modificare solo la colonna 'seleziona
        If oParam.strTipo <> "D" And oParam.strTipo <> "K" And _
           oParam.strTipo <> "(" And oParam.strTipo <> "£" Then
          If xx_sel.Visible And xx_sel.Enabled Then
            grvSedoc.Enabled = True
            For Each dtrCol As NTSGridColumn In grvSedoc.Columns
              If dtrCol.Name <> "xx_sel" Then dtrCol.Enabled = False
            Next
          End If
        Else
          'non è possibile selezionare più di un documento da fdin
          xx_sel.Visible = False
        End If
      Else
        'non è possibile selezionare più di un documento
        xx_sel.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      If xx_sel.Visible = False Then
        cmdSelAll.Enabled = False
        cmdDesAll.Enabled = False
        cmdSelAll.Visible = False
        cmdDesAll.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcuni controlli
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        tm_vistato.Visible = False
        tm_commeca.Visible = False
        xx_commeca.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub FRMVEFIDO_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      edContoDa.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub FRMVEFIDO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    Try
      If e.KeyCode = Keys.F5 Then
        If ceFiltriExt.ContainsFocus Then ceFiltriExt.Zoom()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    If dsSedo Is Nothing Then Return
    If dsSedo.Tables("SEDOC").Rows.Count > 0 Then
      oParam.strOut = "S"
      oParam.strAlfpar = grvSedoc.GetFocusedRowCellValue(tm_serie).ToString()
      oParam.lNumpar = NTSCInt(grvSedoc.GetFocusedRowCellValue(tm_numdoc))

      If oParam.nTipologia = 1 Then
        'se chiamato da bsveboll o da fdin
        grvSedoc.NTSGetCurrentDataRow!xx_sel = "S"
        dsSedo.Tables("SEDOC").AcceptChanges()

        'memorizzo l'elenco dei documenti da aprire e lo restituisco a bsveboll
        oParam.oParam = Nothing
        Dim dttTmp As New DataTable
        dttTmp.Columns.Add("tm_tipork", GetType(String))
        dttTmp.Columns.Add("tm_anno", GetType(Integer))
        dttTmp.Columns.Add("tm_serie", GetType(String))
        dttTmp.Columns.Add("tm_numdoc", GetType(Integer))
        For Each dtrT As DataRow In dsSedo.Tables("SEDOC").Select("xx_sel = 'S'", "tm_tipork, tm_anno, tm_serie, tm_numdoc")
          dttTmp.Rows.Add(New Object() {dtrT!tm_tipork, dtrT!tm_anno, dtrT!tm_serie, dtrT!tm_numdoc})
        Next
        dttTmp.AcceptChanges()
        oParam.oParam = dttTmp
      End If    'If oParam.nTipologia = 1 Then
    Else
      oParam.strOut = ""
      oParam.oParam = Nothing
    End If
    Me.Close()
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oParam.strOut = ""
    oParam.oParam = Nothing
    Me.Close()
  End Sub

  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Try
      Me.Cursor = Cursors.WaitCursor
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edContoDa.Text) > NTSCInt(edContoA.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130692651544812076, "Attenzione!" & vbCrLf & _
          "Il Conto iniziale non può essere superiore a quello finale."))
        edContoDa.Text = "0"
        edContoA.Text = "".PadLeft(9, "9"c)
        edContoDa.Focus()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCDate(edDataDa.Text) > NTSCDate(edDataA.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130692652516757947, "Attenzione!" & vbCrLf & _
          "La data iniziale non può essere superiore a quella finale."))
        edDataDa.Text = IntSetDate("01/01/" & oParam.nAnno.ToString)
        edDataA.Text = IntSetDate("31/12/2099")
        edDataDa.Focus()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(edDaTipobf.Text) > NTSCInt(edATipobf.Text) Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 130692653084908116, "Attenzione!" & vbCrLf & _
          "Il Tipo bolla/fattura iniziale non può essere superiore a quello finale."))
        edDaTipobf.Text = "0"
        edATipobf.Text = "".PadLeft(4, "9"c)
        edDaTipobf.Focus()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      dsSedo = oCleDocu.ZoomSedoc(oParam.strTipo, oParam.nAnno, edSerie.Text, NTSCInt(edContoDa.Text), _
                                 NTSCInt(edContoA.Text), edDataDa.Text, edDataA.Text, NTSCInt(edDaTipobf.Text), _
                                 NTSCInt(edATipobf.Text), edRiferim.Text.Trim, NTSCInt(edNumpar.Text), DittaCorrente, _
                                 ceFiltriExt.GeneraQuerySQL, "")

      If oParam.nTipologia = 1 Then
        'se chiamato da bsveboll, aggiungo la colonna xx_sel per poter far selezionare i documenti da aprire
        dsSedo.Tables("SEDOC").Columns.Add("xx_sel", GetType(String))
      End If
      dcSedo.DataSource = dsSedo.Tables("SEDOC")
      dsSedo.AcceptChanges()
      grSedoc.DataSource = dcSedo

      'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valore
      If grvSedoc.RowCount > 0 Then
        grvSedoc.NTSMoveFirstRowColunn()
        grvSedoc.Focus()
      End If

      Me.Cursor = Cursors.Default

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Me.Cursor = Cursors.Default
    End Try
  End Sub



  Public Overridable Sub edContoDa_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edContoDa.Validated
    Try
      If edContoDa.Text <> "0" Then edContoA.Text = edContoDa.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edDaTipobfa_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDaTipobf.Validated
    Try
      If edDaTipobf.Text <> "0" Then edATipobf.Text = edDaTipobf.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub



  Public Overridable Sub grvSedoc_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grvSedoc.KeyDown
    Try
      If e.KeyCode = Keys.Enter Then If grvSedoc.Focused And grvSedoc.RowCount > 0 Then cmdOk_Click(Me, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grSedoc_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grSedoc.MouseDoubleClick
    Try
      cmdOk_Click(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edSerie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If (strTmp <> edSerie.Text) And (strTmp.Trim <> "") Then edSerie.Text = strTmp

    Catch ex As Exception
      '--------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '--------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdSelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelAll.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      SelezionaDeselezionaRighe(True)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdDesAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDesAll.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      SelezionaDeselezionaRighe(False)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub SelezionaDeselezionaRighe(ByVal bSeleziona As Boolean)
    Try
      If dsSedo Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrRow As DataRow In dsSedo.Tables("SEDOC").Rows
        dtrRow!xx_sel = IIf(bSeleziona, "S", "N")
      Next
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overrides Function NTSGetDataAutocompletamento(ByVal strTabName As String, ByVal strDescr As String, _
                                                      ByVal IsCrmUser As Boolean, ByRef dsOut As DataSet) As Boolean
    'modifico la funzione standard dell'autocompletamento per fare in modo che da edEt_conto 
    'vengano visti solo clienti o fornitori a seconda che il documento sia emesso o ricevuto
    Try
      If edContoDa.ContainsFocus Or edContoA.ContainsFocus Then
        Select Case oParam.strTipo
          Case "A", "B", "C", "D", "N", "E", "W", "Z", "F", "I", "S"
            strTabName = "ANAGRA_CLI"
          Case Else
            strTabName = "ANAGRA_FOR"
        End Select
        If oApp.oGvar.bAutoCompleteIgnoraCF Then strTabName = "ANAGRACF"
      End If

      Return MyBase.NTSGetDataAutocompletamento(strTabName, strDescr, IsCrmUser, dsOut)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


#Region "Filtri Estesi - Non più utilizzati"
  Public Overridable Sub cmdLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLock.Click
    Try
      If tsSel.SelectedTabPageIndex = 1 Then
        xx_nome.Enabled = Not xx_nome.Enabled
        grvFiltri1.Focus()
      ElseIf tsSel.SelectedTabPageIndex = 2 Then
        xx_nome2.Enabled = Not xx_nome2.Enabled
        grvFiltri2.Focus()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
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
      strOut = CStrSQL(strTesto & bFil)

      Return strOut

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return "''"
    End Try
  End Function

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

      If tsSel.SelectedTabPageIndex = 1 Then
        If strNomeZoom = "ZOOMHLVL" Then
          oParam.strTipo = "TESTMAG"
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
      ElseIf tsSel.SelectedTabPageIndex = 2 Then
        If strNomeZoom = "ZOOMHLVL" Then
          oParam.strTipo = "MOVMAG"
          oParam.strDescr = strCampo
          NTSZOOM.strIn = NTSCStr(grvFiltri2.EditingValue)
          NTSZOOM.ZoomStrIn("ZOOMHLVL", DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvFiltri2.EditingValue) Then grvFiltri2.SetFocusedValue(NTSZOOM.strIn)

        Else

          SetFastZoom(NTSCStr(grvFiltri2.EditingValue), oParam)    'abilito la gestione dello zoom veloce
          NTSZOOM.strIn = NTSCStr(grvFiltri2.EditingValue)
          NTSZOOM.ZoomStrIn(strNomeZoom, DittaCorrente, oParam)
          If NTSZOOM.strIn <> NTSCStr(grvFiltri2.EditingValue) Then grvFiltri2.SetFocusedValue(NTSZOOM.strIn)
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Function CheckFiltri() As Boolean
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Try
      For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
        If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome) = "." Then
          dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda = ""
          dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea = ""
        Else
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128584505571093750, "Se impostato un valore nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' deve essere impostato un valore anche nel filtro A"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128735523533622000, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128492077570882500, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 130409187924808782, "Se impostato un valore nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' deve essere impostato un valore anche nel filtro DA"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 130409187948964057, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 130409187977126002, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

        End If
      Next
      dsFiltri.Tables("FILTRI1").AcceptChanges()

      For i = 0 To dsFiltri2.Tables("FILTRI2").Rows.Count - 1
        If NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2) = "." Then
          dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2 = ""
          dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2 = ""
        Else
          If NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2) <> "" Then
            dtrT = dttCampi2.Select("cb_nomcampo2 = " & CStrSQL(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2))
            If NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 130409187711201848, "Se impostato un valore nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome2) & "|' deve essere impostato un valore anche nel filtro A"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo2.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 130409187746714866, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome2) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 130409187771360275, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome2) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

          If NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2) <> "" Then
            dtrT = dttCampi2.Select("cb_nomcampo2 = " & CStrSQL(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2))
            If NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128584506055937500, "Se impostato un valore nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome2) & "|' deve essere impostato un valore anche nel filtro DA"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo2.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128584503331406250, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome2) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128584503236718750, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome2) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

        End If
      Next
      dsFiltri2.Tables("FILTRI2").AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
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
      dsFiltri.Tables("FILTRI1").Columns.Add("xx_valoreda", GetType(String))
      dsFiltri.Tables("FILTRI1").Columns.Add("xx_valorea", GetType(String))
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dtrT = dttCampi.Select("cb_nomcampo = '.'") : dsFiltri.Tables("FILTRI1").Rows.Add(New Object() {dtrT(0)!cb_nomcampo.ToString, "", ""})
      dsFiltri.AcceptChanges()

      'impostazioni come da recent utente
      strTmp = NTSCStr(oMenu.GetSettingBus("BNVEBOLL", "RECENT", ".", "Filtri1", "", " ", ""))
      If strTmp.Trim.Length > 0 Then
        strT = strTmp.Split(";"c)
        For i = 0 To strT.Length - 1
          If strT(i).Trim <> "" Then dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome = strT(i)
          If i > 10 Then Exit For
        Next
      End If
      dsFiltri.AcceptChanges()

      '---------------------------
      'creo il datatable per contenere i filtri principali
      dsFiltri2.Tables.Add("FILTRI2")
      dsFiltri2.Tables("FILTRI2").Columns.Add("xx_nome2", GetType(String))
      dsFiltri2.Tables("FILTRI2").Columns.Add("xx_valoreda2", GetType(String))
      dsFiltri2.Tables("FILTRI2").Columns.Add("xx_valorea2", GetType(String))
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dtrT = dttCampi2.Select("cb_nomcampo2 = '.'") : dsFiltri2.Tables("FILTRI2").Rows.Add(New Object() {dtrT(0)!cb_nomcampo2.ToString, "", ""})
      dsFiltri2.AcceptChanges()

      'impostazioni come da recent utente
      strTmp = NTSCStr(oMenu.GetSettingBus("BNVEBOLL", "RECENT", ".", "Filtri2", "", " ", ""))
      If strTmp.Trim.Length > 0 Then
        strT = strTmp.Split(";"c)
        For i = 0 To strT.Length - 1
          If strT(i).Trim <> "" Then dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2 = strT(i)
          If i > 10 Then Exit For
        Next
      End If
      dsFiltri2.AcceptChanges()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function ComponiWhereFiltriEstesi(ByVal bCrystal As Boolean) As String
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim strQuery As String = ""
    Try
      If bCrystal Then
        For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome).Trim <> "." And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda).Trim <> "" And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea).Trim <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} >= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda))
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} <= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea))
              Case 8
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} >= " & ConvDataRpt(NTSCDate(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda).ToShortDateString)
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} <= " & ConvDataRpt(NTSCDate(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea).ToShortDateString)
              Case Else
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} >= " & CampoTesto(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda), False)
                strQuery += " AND {" & dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & "} <= " & CampoTesto(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea), False)
            End Select
          End If
        Next    'For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
      Else
        '------------------------------------------
        'griglia filtri1
        For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome).Trim <> "." And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda).Trim <> "" And NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea).Trim <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " >= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) & "§"
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " <= " & CDblSQL(NTSCDec(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) & "§"
              Case 8
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " >= " & CDataSQL(NTSCDate(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) & "§"
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " <= " & CDataSQL(NTSCDate(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) & "§"
              Case Else
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " >= " & CampoTesto(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda), False) & "§"
                strQuery += dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome.ToString & " <= " & CampoTesto(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea), False) & "§"
            End Select
          End If
        Next    'For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
        If strQuery.Length > 0 Then strQuery = strQuery.Substring(0, strQuery.Length - 1)
      End If

      Return strQuery

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function
  Public Overridable Function ComponiWhereFiltriEstesiMovmag(ByVal bCrystal As Boolean) As String
    Dim i As Integer = 0
    Dim dtrT() As DataRow = Nothing
    Dim strQuery As String = ""
    Try
      If bCrystal Then
        For i = 0 To dsFiltri2.Tables("FILTRI2").Rows.Count - 1
          If NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2).Trim <> "." And NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2).Trim <> "" And NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2).Trim <> "" Then
            dtrT = dttCampi2.Select("cb_nomcampo2 = " & CStrSQL(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nom2e))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo2.ToString)
              Case 3, 4, 5, 6, 7
                strQuery += " AND {" & dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2.ToString & "} >= " & CDblSQL(NTSCDec(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2))
                strQuery += " AND {" & dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2.ToString & "} <= " & CDblSQL(NTSCDec(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2))
              Case 8
                strQuery += " AND {" & dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2.ToString & "} >= " & ConvDataRpt(NTSCDate(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2).ToShortDateString)
                strQuery += " AND {" & dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2.ToString & "} <= " & ConvDataRpt(NTSCDate(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2).ToShortDateString)
              Case Else
                strQuery += " AND {" & dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2.ToString & "} >= " & CampoTesto(NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2), False)
                strQuery += " AND {" & dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2.ToString & "} <= " & CampoTesto(NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2), False)
            End Select
          End If
        Next    'For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
      Else
        '------------------------------------------
        'griglia filtri1
        For i = 0 To dsFiltri2.Tables("FILTRI2").Rows.Count - 1
          If NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2).Trim <> "." And NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2).Trim <> "" And NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2).Trim <> "" Then
            dtrT = dttCampi2.Select("cb_nomcampo2 = " & CStrSQL(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2))
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo2.ToString)
              Case 3, 4, 5, 6, 7
                strQuery += dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2.ToString & " >= " & CDblSQL(NTSCDec(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2)) & "§"
                strQuery += dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2.ToString & " <= " & CDblSQL(NTSCDec(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2)) & "§"
              Case 8
                strQuery += dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2.ToString & " >= " & CDataSQL(NTSCDate(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2)) & "§"
                strQuery += dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2.ToString & " <= " & CDataSQL(NTSCDate(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2)) & "§"
              Case Else
                strQuery += dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2.ToString & " >= " & CampoTesto(NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2), False) & "§"
                strQuery += dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2.ToString & " <= " & CampoTesto(NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2), False) & "§"
            End Select
          End If
        Next    'For i = 0 To dsFiltri.Tables("FILTRI1").Rows.Count - 1
        If strQuery.Length > 0 Then strQuery = strQuery.Substring(0, strQuery.Length - 1)
      End If

      Return strQuery

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return ""
    End Try
  End Function
  Public Overridable Function CampiMemoInSelezione() As Boolean
    Dim dtrT() As DataRow = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dsFiltri.Tables("FILTRI1").Rows.Count - 1)
        With dsFiltri.Tables("FILTRI1").Rows(i)
          If (NTSCStr(!xx_nome).Trim <> ".") And (NTSCStr(!xx_valoreda) <> "") And (NTSCStr(!xx_valorea) <> "") Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(!xx_nome))
            If NTSCInt(dtrT(0)!cb_tipocampo.ToString) = 12 Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 130056573971850323, "Attenzione!" & vbCrLf & _
                "Non è possibile indicare un campo Note (memo), in 'Altri filtri corpo'." & vbCrLf & _
                "Modificare la selezione dei dati."))
              Return False
            End If
          End If
        End With
      Next
      '--------------------------------------------------------------------------------------------------------------
      For i As Integer = 0 To (dsFiltri2.Tables("FILTRI2").Rows.Count - 1)
        With dsFiltri2.Tables("FILTRI2").Rows(i)
          If (NTSCStr(!xx_nome2).Trim <> ".") And (NTSCStr(!xx_valoreda2) <> "") And (NTSCStr(!xx_valorea2) <> "") Then
            dtrT = dttCampi2.Select("cb_nomcampo2 = " & CStrSQL(!xx_nome2))
            If NTSCInt(dtrT(0)!cb_tipocampo2.ToString) = 12 Then
              oApp.MsgBoxInfo(oApp.Tr(Me, 130409188097608093, "Attenzione!" & vbCrLf & _
                "Non è possibile indicare un campo Note (memo), in 'Altri filtri corpo'." & vbCrLf & _
                "Modificare la selezione dei dati."))
              Return False
            End If
          End If
        End With
      Next

      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
#End Region
End Class
