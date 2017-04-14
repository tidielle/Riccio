Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMORSEDO
  Private components As System.ComponentModel.IContainer

  Public strTipork As String
  Public strAnno As String
  Public oPar As CLE__CLDP
  Public oCleGsor As CLEORGSOR
  Public dsSeor As DataSet
  Public dcSeor As BindingSource = New BindingSource()
  Public dttOut As New DataTable

  'Non più usati
  Public dttCampi As New DataTable          'elenco campi filtrabili di TESTORD
  Public dsFiltri As New DataSet
  Public dcFiltri1 As New BindingSource
  Public dttCampi2 As New DataTable          'elenco campi filtrabili di MOVORD
  Public dsFiltri2 As New DataSet
  Public dcFiltri2 As New BindingSource


  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    Try
      oMenu = Menu
      oApp = oMenu.App
      If Ditta <> "" Then
        DittaCorrente = Ditta
      Else
        DittaCorrente = oApp.Ditta
      End If

      InitializeComponent()
      Me.MinimumSize = Me.Size

      oPar = Param
      strTipork = Param.strPar1
      strAnno = Param.strPar3
      'edSerie.Text = Param.strPar2

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function


  Public Overridable Sub InitEntity(ByRef cleGsor As CLEORGSOR)
    oCleGsor = cleGsor
    AddHandler oCleGsor.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      edDatConsA.NTSSetParam(oMenu, oApp.Tr(Me, 128230023303440375, "A data consegna"), False)
      edDatConsDa.NTSSetParam(oMenu, oApp.Tr(Me, 128230023303596548, "Da data consegna"), False)
      edContoA.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023303752721, "A codice cliente/fornitore"), tabanagrac)
      edContoDa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128230023303908894, "Da codice cliente/fornitore"), tabanagrac)
      edDataA.NTSSetParam(oMenu, oApp.Tr(Me, 128230023304065067, "A data documento"), False)
      edDataDa.NTSSetParam(oMenu, oApp.Tr(Me, 128230023304221240, "Da data documento"), False)
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128230023304377413, "Serie documento"), CLN__STD.SerieMaxLen, True)
      optTutti.NTSSetParam(oMenu, oApp.Tr(Me, 128230023304533586, ""), "S")
      optNo.NTSSetParam(oMenu, oApp.Tr(Me, 128230023304689759, ""), "S")
      optSi.NTSSetParam(oMenu, oApp.Tr(Me, 128230023304845932, ""), "S")
      edRiferim.NTSSetParam(oMenu, oApp.Tr(Me, 128534192493170000, "Riferimenti"), 255, True)

      grvSeor.NTSSetParam(oMenu, oApp.Tr(Me, 128230023305002105, "Seleziona ordini"))
      xx_sel.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129496814860183202, "Seleziona"), "S", "N")
      td_tipork.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023305158278, "Tipo documento"), 0, True)
      td_anno.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023305314451, "Anno"), "0", 4, 0, 9999)
      td_serie.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023305470624, "Serie"), CLN__STD.SerieMaxLen, True)
      td_numord.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023305626797, "Numero"), "0", 9, 0, 999999999)
      td_datord.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128230023305782970, "Data ordine"), True)
      td_conto.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023305939143, "Codice conto"), "0", 9, 0, 999999999)
      xx_conto.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023306095316, "Descr. conto"), 0, True)
      td_datcons.NTSSetParamDATA(oMenu, oApp.Tr(Me, 128230023306251489, "Data consegna"), True)
      td_totdoc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023306407662, "Tot. documento"), oApp.FormatImporti, 13)
      td_flevas.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023306563835, "Evaso"), "S", "N")
      td_confermato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 128230023306720008, "Confermato"), "S", "N")
      td_coddest.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023306876181, "Cod. destin."), "0", 9, 0, 999999999)
      xx_destin.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023307032354, "Descr. destin."), 0, True)
      td_commeca.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023307188527, "Commessa"), "0", 9, 0, 999999999)
      xx_commeca.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023307344700, "Descr. commessa"), 0, True)
      td_riferim.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128825534390022000, "Riferimenti"), 0, True)
      td_annpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129145010926458157, "Anno p."), "0", 4, 0, 9999)
      td_alfpar.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129145010926614407, "Serie p."), CLN__STD.SerieMaxLen, True)
      td_numpar.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129145010926770657, "Num. par."), "0", 9, 0, 999999999)
      td_datpar.NTSSetParamDATA(oMenu, oApp.Tr(Me, 129145010926926907, "Data par."), True)
      td_tipobf.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129145010927083157, "Tipo b/f"), "0", 4, 0, 9999)
      xx_tipobf.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129145010927239407, "Descr. Tipo b/f"), 0, True)
      td_flstam.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129145010927395657, "Stampato"), "S", "N")
      td_rilasciato.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129145010927551907, "Rilasciato"), "S", "N")
      td_aperto.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129145010927708157, "Aperto"), "S", "N")
      td_sospeso.NTSSetParamCHK(oMenu, oApp.Tr(Me, 129145010927864407, "Sospeso"), "S", "N")
      td_magaz.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129145010928020657, "Mag. 1"), "0", 4, 0, 9999)
      xx_magaz.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129145010928176907, "Descr. Mag. 1"), 0, True)
      td_magaz2.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129145010928333157, "Mag. 2"), "0", 4, 0, 9999)
      xx_magaz2.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129145010928489407, "Descr. Mag. 2"), 0, True)
      td_magimp.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129145010928645657, "Mag. Imp."), "0", 4, 0, 9999)
      xx_magimp.NTSSetParamSTR(oMenu, oApp.Tr(Me, 129145010928801907, "Descr. Mag. Imp."), 0, True)
      optTutti.NTSSetParam(oMenu, oApp.Tr(Me, 129145010935676907, "Tutti"), "T")
      optNo.NTSSetParam(oMenu, oApp.Tr(Me, 129145010935833157, "No"), "N")
      optSi.NTSSetParam(oMenu, oApp.Tr(Me, 129145010935989407, "Si"), "S")
      td_totmerce.NTSSetParamNUM(oMenu, oApp.Tr(Me, 129527776558043991, "Totale merce"), oApp.FormatImporti, 0)

      Select Case strTipork
        Case "Q", "R", "V"
          edContoDa.NTSSetParamZoom("ZOOMANAGRAC")
          edContoA.NTSSetParamZoom("ZOOMANAGRAC")
        Case Else
          edContoDa.NTSSetParamZoom("ZOOMANAGRAF")
          edContoA.NTSSetParamZoom("ZOOMANAGRAF")
      End Select

      ceFiltriExt.NTSSetParam(oMenu, oApp.Tr(Me, 129527776558043992, "Filtri Estesi"), "BSORGSOR", New CLE__CLDP)
      ceFiltriExt.AggiungiTabella("TESTORD")
      ceFiltriExt.AggiungiTabella("MOVORD")
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
    Me.cbEt_blocco = New NTSInformatica.NTSComboBox
    Me.pnCommandbutton = New NTSInformatica.NTSPanel
    Me.cmdRicerca = New NTSInformatica.NTSButton
    Me.cbEt_sospeso = New NTSInformatica.NTSComboBox
    Me.lbBlocco = New NTSInformatica.NTSLabel
    Me.lbStato = New NTSInformatica.NTSLabel
    Me.lbAnno = New System.Windows.Forms.Label
    Me.edRiferim = New NTSInformatica.NTSTextBoxStr
    Me.lbRiferim = New NTSInformatica.NTSLabel
    Me.edDatConsA = New NTSInformatica.NTSTextBoxData
    Me.edDatConsDa = New NTSInformatica.NTSTextBoxData
    Me.edContoA = New NTSInformatica.NTSTextBoxNum
    Me.edContoDa = New NTSInformatica.NTSTextBoxNum
    Me.edDataA = New NTSInformatica.NTSTextBoxData
    Me.edDataDa = New NTSInformatica.NTSTextBoxData
    Me.lbAdata = New NTSInformatica.NTSLabel
    Me.lbAdatacons = New NTSInformatica.NTSLabel
    Me.lbDadatacons = New NTSInformatica.NTSLabel
    Me.lbDadata = New NTSInformatica.NTSLabel
    Me.lbAconto = New NTSInformatica.NTSLabel
    Me.lbDaconto = New NTSInformatica.NTSLabel
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.lbSerie = New NTSInformatica.NTSLabel
    Me.fmEvaso = New NTSInformatica.NTSGroupBox
    Me.optTutti = New NTSInformatica.NTSRadioButton
    Me.optNo = New NTSInformatica.NTSRadioButton
    Me.optSi = New NTSInformatica.NTSRadioButton
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.tsSel = New NTSInformatica.NTSTabControl
    Me.NtsTabPage1 = New NTSInformatica.NTSTabPage
    Me.pnGrid = New NTSInformatica.NTSPanel
    Me.grSeor = New NTSInformatica.NTSGrid
    Me.grvSeor = New NTSInformatica.NTSGridView
    Me.xx_sel = New NTSInformatica.NTSGridColumn
    Me.td_tipork = New NTSInformatica.NTSGridColumn
    Me.td_anno = New NTSInformatica.NTSGridColumn
    Me.td_serie = New NTSInformatica.NTSGridColumn
    Me.td_numord = New NTSInformatica.NTSGridColumn
    Me.td_datord = New NTSInformatica.NTSGridColumn
    Me.td_conto = New NTSInformatica.NTSGridColumn
    Me.xx_conto = New NTSInformatica.NTSGridColumn
    Me.td_datcons = New NTSInformatica.NTSGridColumn
    Me.td_totdoc = New NTSInformatica.NTSGridColumn
    Me.td_flevas = New NTSInformatica.NTSGridColumn
    Me.td_confermato = New NTSInformatica.NTSGridColumn
    Me.td_coddest = New NTSInformatica.NTSGridColumn
    Me.xx_destin = New NTSInformatica.NTSGridColumn
    Me.td_commeca = New NTSInformatica.NTSGridColumn
    Me.xx_commeca = New NTSInformatica.NTSGridColumn
    Me.td_riferim = New NTSInformatica.NTSGridColumn
    Me.td_annpar = New NTSInformatica.NTSGridColumn
    Me.td_alfpar = New NTSInformatica.NTSGridColumn
    Me.td_numpar = New NTSInformatica.NTSGridColumn
    Me.td_datpar = New NTSInformatica.NTSGridColumn
    Me.td_tipobf = New NTSInformatica.NTSGridColumn
    Me.xx_tipobf = New NTSInformatica.NTSGridColumn
    Me.td_flstam = New NTSInformatica.NTSGridColumn
    Me.td_rilasciato = New NTSInformatica.NTSGridColumn
    Me.td_aperto = New NTSInformatica.NTSGridColumn
    Me.td_sospeso = New NTSInformatica.NTSGridColumn
    Me.td_magaz = New NTSInformatica.NTSGridColumn
    Me.xx_magaz = New NTSInformatica.NTSGridColumn
    Me.td_magaz2 = New NTSInformatica.NTSGridColumn
    Me.xx_magaz2 = New NTSInformatica.NTSGridColumn
    Me.td_magimp = New NTSInformatica.NTSGridColumn
    Me.xx_magimp = New NTSInformatica.NTSGridColumn
    Me.td_totmerce = New NTSInformatica.NTSGridColumn
    Me.NtsTabPage2 = New NTSInformatica.NTSTabPage
    Me.pnSel1 = New NTSInformatica.NTSPanel
    Me.ceFiltriExt = New NTSInformatica.NTSXXFILT
    Me.cmdLock = New NTSInformatica.NTSButton
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
    Me.cmdDesAll = New NTSInformatica.NTSButton
    Me.cmdSelAll = New NTSInformatica.NTSButton
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnfiltri, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnfiltri.SuspendLayout()
    CType(Me.cbEt_blocco.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnCommandbutton, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCommandbutton.SuspendLayout()
    CType(Me.cbEt_sospeso.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edRiferim.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatConsA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDatConsDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edContoDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataA.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDataDa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmEvaso, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmEvaso.SuspendLayout()
    CType(Me.optTutti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optNo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.optSi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.tsSel, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsSel.SuspendLayout()
    Me.NtsTabPage1.SuspendLayout()
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnGrid.SuspendLayout()
    CType(Me.grSeor, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvSeor, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.NtsTabPage2.SuspendLayout()
    CType(Me.pnSel1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnSel1.SuspendLayout()
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
    Me.frmAuto.Appearance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
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
    Me.cmdSeleziona.Size = New System.Drawing.Size(106, 23)
    Me.cmdSeleziona.TabIndex = 2
    Me.cmdSeleziona.Text = "&Seleziona"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.cmdAnnulla.ImagePath = ""
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(3, 95)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(106, 23)
    Me.cmdAnnulla.TabIndex = 3
    Me.cmdAnnulla.Text = "&Annulla"
    '
    'pnfiltri
    '
    Me.pnfiltri.AllowDrop = True
    Me.pnfiltri.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnfiltri.Appearance.Options.UseBackColor = True
    Me.pnfiltri.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnfiltri.Controls.Add(Me.cbEt_blocco)
    Me.pnfiltri.Controls.Add(Me.pnCommandbutton)
    Me.pnfiltri.Controls.Add(Me.cbEt_sospeso)
    Me.pnfiltri.Controls.Add(Me.lbBlocco)
    Me.pnfiltri.Controls.Add(Me.lbStato)
    Me.pnfiltri.Controls.Add(Me.lbAnno)
    Me.pnfiltri.Controls.Add(Me.edRiferim)
    Me.pnfiltri.Controls.Add(Me.lbRiferim)
    Me.pnfiltri.Controls.Add(Me.edDatConsA)
    Me.pnfiltri.Controls.Add(Me.edDatConsDa)
    Me.pnfiltri.Controls.Add(Me.edContoA)
    Me.pnfiltri.Controls.Add(Me.edContoDa)
    Me.pnfiltri.Controls.Add(Me.edDataA)
    Me.pnfiltri.Controls.Add(Me.edDataDa)
    Me.pnfiltri.Controls.Add(Me.lbAdata)
    Me.pnfiltri.Controls.Add(Me.lbAdatacons)
    Me.pnfiltri.Controls.Add(Me.lbDadatacons)
    Me.pnfiltri.Controls.Add(Me.lbDadata)
    Me.pnfiltri.Controls.Add(Me.lbAconto)
    Me.pnfiltri.Controls.Add(Me.lbDaconto)
    Me.pnfiltri.Controls.Add(Me.edSerie)
    Me.pnfiltri.Controls.Add(Me.lbSerie)
    Me.pnfiltri.Controls.Add(Me.fmEvaso)
    Me.pnfiltri.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnfiltri.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnfiltri.Location = New System.Drawing.Point(0, 0)
    Me.pnfiltri.Name = "pnfiltri"
    Me.pnfiltri.NTSActiveTrasparency = True
    Me.pnfiltri.Size = New System.Drawing.Size(627, 132)
    Me.pnfiltri.TabIndex = 4
    '
    'cbEt_blocco
    '
    Me.cbEt_blocco.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_blocco.DataSource = Nothing
    Me.cbEt_blocco.DisplayMember = ""
    Me.cbEt_blocco.Location = New System.Drawing.Point(342, 105)
    Me.cbEt_blocco.Name = "cbEt_blocco"
    Me.cbEt_blocco.NTSDbField = ""
    Me.cbEt_blocco.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.cbEt_blocco.Properties.Appearance.Options.UseBackColor = True
    Me.cbEt_blocco.Properties.AutoHeight = False
    Me.cbEt_blocco.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_blocco.Properties.DropDownRows = 30
    Me.cbEt_blocco.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_blocco.SelectedValue = ""
    Me.cbEt_blocco.Size = New System.Drawing.Size(159, 20)
    Me.cbEt_blocco.TabIndex = 73
    Me.cbEt_blocco.ValueMember = ""
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
    Me.pnCommandbutton.Controls.Add(Me.cmdSeleziona)
    Me.pnCommandbutton.Controls.Add(Me.cmdAnnulla)
    Me.pnCommandbutton.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCommandbutton.Location = New System.Drawing.Point(510, 2)
    Me.pnCommandbutton.Name = "pnCommandbutton"
    Me.pnCommandbutton.NTSActiveTrasparency = True
    Me.pnCommandbutton.Size = New System.Drawing.Size(118, 130)
    Me.pnCommandbutton.TabIndex = 49
    '
    'cmdRicerca
    '
    Me.cmdRicerca.ImagePath = ""
    Me.cmdRicerca.ImageText = ""
    Me.cmdRicerca.Location = New System.Drawing.Point(3, 3)
    Me.cmdRicerca.Name = "cmdRicerca"
    Me.cmdRicerca.NTSContextMenu = Nothing
    Me.cmdRicerca.Size = New System.Drawing.Size(106, 23)
    Me.cmdRicerca.TabIndex = 50
    Me.cmdRicerca.Text = "&Ricerca"
    '
    'cbEt_sospeso
    '
    Me.cbEt_sospeso.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbEt_sospeso.DataSource = Nothing
    Me.cbEt_sospeso.DisplayMember = ""
    Me.cbEt_sospeso.Location = New System.Drawing.Point(94, 105)
    Me.cbEt_sospeso.Name = "cbEt_sospeso"
    Me.cbEt_sospeso.NTSDbField = ""
    Me.cbEt_sospeso.Properties.AutoHeight = False
    Me.cbEt_sospeso.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbEt_sospeso.Properties.DropDownRows = 30
    Me.cbEt_sospeso.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbEt_sospeso.SelectedValue = ""
    Me.cbEt_sospeso.Size = New System.Drawing.Size(157, 20)
    Me.cbEt_sospeso.TabIndex = 72
    Me.cbEt_sospeso.ValueMember = ""
    '
    'lbBlocco
    '
    Me.lbBlocco.AutoSize = True
    Me.lbBlocco.BackColor = System.Drawing.Color.Transparent
    Me.lbBlocco.Location = New System.Drawing.Point(269, 108)
    Me.lbBlocco.Name = "lbBlocco"
    Me.lbBlocco.NTSDbField = ""
    Me.lbBlocco.Size = New System.Drawing.Size(66, 13)
    Me.lbBlocco.TabIndex = 71
    Me.lbBlocco.Text = "Blocco ordini"
    Me.lbBlocco.Tooltip = ""
    Me.lbBlocco.UseMnemonic = False
    '
    'lbStato
    '
    Me.lbStato.AutoSize = True
    Me.lbStato.BackColor = System.Drawing.Color.Transparent
    Me.lbStato.Location = New System.Drawing.Point(12, 108)
    Me.lbStato.Name = "lbStato"
    Me.lbStato.NTSDbField = ""
    Me.lbStato.Size = New System.Drawing.Size(73, 13)
    Me.lbStato.TabIndex = 70
    Me.lbStato.Text = "Ordini sospesi"
    Me.lbStato.Tooltip = ""
    Me.lbStato.UseMnemonic = False
    '
    'lbAnno
    '
    Me.lbAnno.AutoSize = True
    Me.lbAnno.Location = New System.Drawing.Point(344, 12)
    Me.lbAnno.Name = "lbAnno"
    Me.lbAnno.Size = New System.Drawing.Size(36, 13)
    Me.lbAnno.TabIndex = 54
    Me.lbAnno.Text = "Anno:"
    '
    'edRiferim
    '
    Me.edRiferim.Cursor = System.Windows.Forms.Cursors.Default
    Me.edRiferim.Location = New System.Drawing.Point(94, 82)
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
    'lbRiferim
    '
    Me.lbRiferim.AutoSize = True
    Me.lbRiferim.BackColor = System.Drawing.Color.Transparent
    Me.lbRiferim.Location = New System.Drawing.Point(12, 85)
    Me.lbRiferim.Name = "lbRiferim"
    Me.lbRiferim.NTSDbField = ""
    Me.lbRiferim.Size = New System.Drawing.Size(84, 13)
    Me.lbRiferim.TabIndex = 49
    Me.lbRiferim.Text = "Riferimenti (like)"
    Me.lbRiferim.Tooltip = ""
    Me.lbRiferim.UseMnemonic = False
    '
    'edDatConsA
    '
    Me.edDatConsA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatConsA.Location = New System.Drawing.Point(251, 59)
    Me.edDatConsA.Name = "edDatConsA"
    Me.edDatConsA.NTSDbField = ""
    Me.edDatConsA.NTSForzaVisZoom = False
    Me.edDatConsA.NTSOldValue = ""
    Me.edDatConsA.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDatConsA.Properties.Appearance.Options.UseBackColor = True
    Me.edDatConsA.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatConsA.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatConsA.Properties.AutoHeight = False
    Me.edDatConsA.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatConsA.Properties.MaxLength = 65536
    Me.edDatConsA.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatConsA.Size = New System.Drawing.Size(84, 20)
    Me.edDatConsA.TabIndex = 48
    '
    'edDatConsDa
    '
    Me.edDatConsDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDatConsDa.Location = New System.Drawing.Point(94, 59)
    Me.edDatConsDa.Name = "edDatConsDa"
    Me.edDatConsDa.NTSDbField = ""
    Me.edDatConsDa.NTSForzaVisZoom = False
    Me.edDatConsDa.NTSOldValue = ""
    Me.edDatConsDa.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edDatConsDa.Properties.Appearance.Options.UseBackColor = True
    Me.edDatConsDa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDatConsDa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDatConsDa.Properties.AutoHeight = False
    Me.edDatConsDa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDatConsDa.Properties.MaxLength = 65536
    Me.edDatConsDa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDatConsDa.Size = New System.Drawing.Size(84, 20)
    Me.edDatConsDa.TabIndex = 47
    '
    'edContoA
    '
    Me.edContoA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContoA.EditValue = "999999999"
    Me.edContoA.Location = New System.Drawing.Point(251, 9)
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
    'edContoDa
    '
    Me.edContoDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edContoDa.EditValue = "0"
    Me.edContoDa.Location = New System.Drawing.Point(94, 9)
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
    Me.edContoDa.Size = New System.Drawing.Size(83, 20)
    Me.edContoDa.TabIndex = 45
    '
    'edDataA
    '
    Me.edDataA.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataA.Location = New System.Drawing.Point(251, 34)
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
    'edDataDa
    '
    Me.edDataDa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDataDa.Location = New System.Drawing.Point(94, 34)
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
    Me.edDataDa.Size = New System.Drawing.Size(83, 20)
    Me.edDataDa.TabIndex = 13
    '
    'lbAdata
    '
    Me.lbAdata.AutoSize = True
    Me.lbAdata.BackColor = System.Drawing.Color.Transparent
    Me.lbAdata.Location = New System.Drawing.Point(183, 37)
    Me.lbAdata.Name = "lbAdata"
    Me.lbAdata.NTSDbField = ""
    Me.lbAdata.Size = New System.Drawing.Size(39, 13)
    Me.lbAdata.TabIndex = 12
    Me.lbAdata.Text = "A data"
    Me.lbAdata.Tooltip = ""
    Me.lbAdata.UseMnemonic = False
    '
    'lbAdatacons
    '
    Me.lbAdatacons.AutoSize = True
    Me.lbAdatacons.BackColor = System.Drawing.Color.Transparent
    Me.lbAdatacons.Location = New System.Drawing.Point(183, 62)
    Me.lbAdatacons.Name = "lbAdatacons"
    Me.lbAdatacons.NTSDbField = ""
    Me.lbAdatacons.Size = New System.Drawing.Size(68, 13)
    Me.lbAdatacons.TabIndex = 11
    Me.lbAdatacons.Text = "A data cons."
    Me.lbAdatacons.Tooltip = ""
    Me.lbAdatacons.UseMnemonic = False
    '
    'lbDadatacons
    '
    Me.lbDadatacons.AutoSize = True
    Me.lbDadatacons.BackColor = System.Drawing.Color.Transparent
    Me.lbDadatacons.Location = New System.Drawing.Point(12, 62)
    Me.lbDadatacons.Name = "lbDadatacons"
    Me.lbDadatacons.NTSDbField = ""
    Me.lbDadatacons.Size = New System.Drawing.Size(74, 13)
    Me.lbDadatacons.TabIndex = 10
    Me.lbDadatacons.Text = "Da data cons."
    Me.lbDadatacons.Tooltip = ""
    Me.lbDadatacons.UseMnemonic = False
    '
    'lbDadata
    '
    Me.lbDadata.AutoSize = True
    Me.lbDadata.BackColor = System.Drawing.Color.Transparent
    Me.lbDadata.Location = New System.Drawing.Point(12, 37)
    Me.lbDadata.Name = "lbDadata"
    Me.lbDadata.NTSDbField = ""
    Me.lbDadata.Size = New System.Drawing.Size(45, 13)
    Me.lbDadata.TabIndex = 9
    Me.lbDadata.Text = "Da data"
    Me.lbDadata.Tooltip = ""
    Me.lbDadata.UseMnemonic = False
    '
    'lbAconto
    '
    Me.lbAconto.AutoSize = True
    Me.lbAconto.BackColor = System.Drawing.Color.Transparent
    Me.lbAconto.Location = New System.Drawing.Point(183, 12)
    Me.lbAconto.Name = "lbAconto"
    Me.lbAconto.NTSDbField = ""
    Me.lbAconto.Size = New System.Drawing.Size(44, 13)
    Me.lbAconto.TabIndex = 8
    Me.lbAconto.Text = "A conto"
    Me.lbAconto.Tooltip = ""
    Me.lbAconto.UseMnemonic = False
    '
    'lbDaconto
    '
    Me.lbDaconto.AutoSize = True
    Me.lbDaconto.BackColor = System.Drawing.Color.Transparent
    Me.lbDaconto.Location = New System.Drawing.Point(12, 12)
    Me.lbDaconto.Name = "lbDaconto"
    Me.lbDaconto.NTSDbField = ""
    Me.lbDaconto.Size = New System.Drawing.Size(50, 13)
    Me.lbDaconto.TabIndex = 7
    Me.lbDaconto.Text = "Da conto"
    Me.lbDaconto.Tooltip = ""
    Me.lbDaconto.UseMnemonic = False
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.Location = New System.Drawing.Point(456, 9)
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
    'lbSerie
    '
    Me.lbSerie.AutoSize = True
    Me.lbSerie.BackColor = System.Drawing.Color.Transparent
    Me.lbSerie.Location = New System.Drawing.Point(422, 12)
    Me.lbSerie.Name = "lbSerie"
    Me.lbSerie.NTSDbField = ""
    Me.lbSerie.Size = New System.Drawing.Size(31, 13)
    Me.lbSerie.TabIndex = 5
    Me.lbSerie.Text = "Serie"
    Me.lbSerie.Tooltip = ""
    Me.lbSerie.UseMnemonic = False
    '
    'fmEvaso
    '
    Me.fmEvaso.AllowDrop = True
    Me.fmEvaso.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmEvaso.Appearance.Options.UseBackColor = True
    Me.fmEvaso.Controls.Add(Me.optTutti)
    Me.fmEvaso.Controls.Add(Me.optNo)
    Me.fmEvaso.Controls.Add(Me.optSi)
    Me.fmEvaso.Cursor = System.Windows.Forms.Cursors.Default
    Me.fmEvaso.Location = New System.Drawing.Point(342, 34)
    Me.fmEvaso.Name = "fmEvaso"
    Me.fmEvaso.Size = New System.Drawing.Size(159, 68)
    Me.fmEvaso.TabIndex = 4
    Me.fmEvaso.Text = "Evaso"
    '
    'optTutti
    '
    Me.optTutti.Cursor = System.Windows.Forms.Cursors.Default
    Me.optTutti.EditValue = True
    Me.optTutti.Location = New System.Drawing.Point(101, 31)
    Me.optTutti.Name = "optTutti"
    Me.optTutti.NTSCheckValue = "S"
    Me.optTutti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optTutti.Properties.Appearance.Options.UseBackColor = True
    Me.optTutti.Properties.AutoHeight = False
    Me.optTutti.Properties.Caption = "&Tutti"
    Me.optTutti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optTutti.Size = New System.Drawing.Size(46, 19)
    Me.optTutti.TabIndex = 2
    '
    'optNo
    '
    Me.optNo.Cursor = System.Windows.Forms.Cursors.Default
    Me.optNo.Location = New System.Drawing.Point(50, 31)
    Me.optNo.Name = "optNo"
    Me.optNo.NTSCheckValue = "S"
    Me.optNo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optNo.Properties.Appearance.Options.UseBackColor = True
    Me.optNo.Properties.AutoHeight = False
    Me.optNo.Properties.Caption = "&No"
    Me.optNo.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optNo.Size = New System.Drawing.Size(39, 19)
    Me.optNo.TabIndex = 1
    '
    'optSi
    '
    Me.optSi.Cursor = System.Windows.Forms.Cursors.Default
    Me.optSi.Location = New System.Drawing.Point(5, 31)
    Me.optSi.Name = "optSi"
    Me.optSi.NTSCheckValue = "S"
    Me.optSi.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.optSi.Properties.Appearance.Options.UseBackColor = True
    Me.optSi.Properties.AutoHeight = False
    Me.optSi.Properties.Caption = "&Si"
    Me.optSi.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.optSi.Size = New System.Drawing.Size(34, 19)
    Me.optSi.TabIndex = 0
    '
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.tsSel)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(636, 440)
    Me.pnTop.TabIndex = 50
    '
    'tsSel
    '
    Me.tsSel.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsSel.Location = New System.Drawing.Point(0, 0)
    Me.tsSel.Name = "tsSel"
    Me.tsSel.SelectedTabPage = Me.NtsTabPage1
    Me.tsSel.Size = New System.Drawing.Size(636, 440)
    Me.tsSel.TabIndex = 50
    Me.tsSel.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.NtsTabPage1, Me.NtsTabPage2})
    Me.tsSel.Text = "NtsTabControl1"
    '
    'NtsTabPage1
    '
    Me.NtsTabPage1.AllowDrop = True
    Me.NtsTabPage1.Controls.Add(Me.pnGrid)
    Me.NtsTabPage1.Controls.Add(Me.pnfiltri)
    Me.NtsTabPage1.Enable = True
    Me.NtsTabPage1.Name = "NtsTabPage1"
    Me.NtsTabPage1.Size = New System.Drawing.Size(627, 410)
    Me.NtsTabPage1.Text = "Generale"
    '
    'pnGrid
    '
    Me.pnGrid.AllowDrop = True
    Me.pnGrid.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnGrid.Appearance.Options.UseBackColor = True
    Me.pnGrid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnGrid.Controls.Add(Me.grSeor)
    Me.pnGrid.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnGrid.Location = New System.Drawing.Point(0, 132)
    Me.pnGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnGrid.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnGrid.Name = "pnGrid"
    Me.pnGrid.NTSActiveTrasparency = True
    Me.pnGrid.Size = New System.Drawing.Size(627, 278)
    Me.pnGrid.TabIndex = 51
    '
    'grSeor
    '
    Me.grSeor.Dock = System.Windows.Forms.DockStyle.Fill
    Me.grSeor.EmbeddedNavigator.Name = ""
    Me.grSeor.Location = New System.Drawing.Point(0, 0)
    Me.grSeor.MainView = Me.grvSeor
    Me.grSeor.Name = "grSeor"
    Me.grSeor.Size = New System.Drawing.Size(627, 278)
    Me.grSeor.TabIndex = 6
    Me.grSeor.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvSeor})
    '
    'grvSeor
    '
    Me.grvSeor.ActiveFilterEnabled = False
    Me.grvSeor.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.xx_sel, Me.td_tipork, Me.td_anno, Me.td_serie, Me.td_numord, Me.td_datord, Me.td_conto, Me.xx_conto, Me.td_datcons, Me.td_totdoc, Me.td_flevas, Me.td_confermato, Me.td_coddest, Me.xx_destin, Me.td_commeca, Me.xx_commeca, Me.td_riferim, Me.td_annpar, Me.td_alfpar, Me.td_numpar, Me.td_datpar, Me.td_tipobf, Me.xx_tipobf, Me.td_flstam, Me.td_rilasciato, Me.td_aperto, Me.td_sospeso, Me.td_magaz, Me.xx_magaz, Me.td_magaz2, Me.xx_magaz2, Me.td_magimp, Me.xx_magimp, Me.td_totmerce})
    Me.grvSeor.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvSeor.Enabled = False
    Me.grvSeor.GridControl = Me.grSeor
    Me.grvSeor.GroupPanelText = "Per raggruppare: Tasto DX sulla colonna -> Group by this column *** Per totali pa" & _
        "rziali/generali: sul piede di gruppo della colonna tasto DX -> SUM "
    Me.grvSeor.MinRowHeight = 14
    Me.grvSeor.Name = "grvSeor"
    Me.grvSeor.NTSAllowDelete = False
    Me.grvSeor.NTSAllowInsert = False
    Me.grvSeor.NTSAllowUpdate = False
    Me.grvSeor.NTSMenuContext = Nothing
    Me.grvSeor.OptionsBehavior.Editable = False
    Me.grvSeor.OptionsCustomization.AllowRowSizing = True
    Me.grvSeor.OptionsFilter.AllowFilterEditor = False
    Me.grvSeor.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvSeor.OptionsNavigation.UseTabKey = False
    Me.grvSeor.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvSeor.OptionsView.ColumnAutoWidth = False
    Me.grvSeor.OptionsView.EnableAppearanceEvenRow = True
    Me.grvSeor.OptionsView.ShowGroupPanel = False
    Me.grvSeor.RowHeight = 16
    '
    'xx_sel
    '
    Me.xx_sel.AppearanceCell.Options.UseBackColor = True
    Me.xx_sel.AppearanceCell.Options.UseTextOptions = True
    Me.xx_sel.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_sel.Caption = "Seleziona"
    Me.xx_sel.Enabled = True
    Me.xx_sel.FieldName = "xx_sel"
    Me.xx_sel.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_sel.Name = "xx_sel"
    Me.xx_sel.NTSRepositoryComboBox = Nothing
    Me.xx_sel.NTSRepositoryItemCheck = Nothing
    Me.xx_sel.NTSRepositoryItemMemo = Nothing
    Me.xx_sel.NTSRepositoryItemText = Nothing
    Me.xx_sel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_sel.Visible = True
    Me.xx_sel.VisibleIndex = 0
    '
    'td_tipork
    '
    Me.td_tipork.AppearanceCell.Options.UseBackColor = True
    Me.td_tipork.AppearanceCell.Options.UseTextOptions = True
    Me.td_tipork.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_tipork.Caption = "Tipo documento"
    Me.td_tipork.Enabled = True
    Me.td_tipork.FieldName = "td_tipork"
    Me.td_tipork.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_tipork.Name = "td_tipork"
    Me.td_tipork.NTSRepositoryComboBox = Nothing
    Me.td_tipork.NTSRepositoryItemCheck = Nothing
    Me.td_tipork.NTSRepositoryItemMemo = Nothing
    Me.td_tipork.NTSRepositoryItemText = Nothing
    Me.td_tipork.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_tipork.Width = 105
    '
    'td_anno
    '
    Me.td_anno.AppearanceCell.Options.UseBackColor = True
    Me.td_anno.AppearanceCell.Options.UseTextOptions = True
    Me.td_anno.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_anno.Caption = "Anno"
    Me.td_anno.Enabled = True
    Me.td_anno.FieldName = "td_anno"
    Me.td_anno.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_anno.Name = "td_anno"
    Me.td_anno.NTSRepositoryComboBox = Nothing
    Me.td_anno.NTSRepositoryItemCheck = Nothing
    Me.td_anno.NTSRepositoryItemMemo = Nothing
    Me.td_anno.NTSRepositoryItemText = Nothing
    Me.td_anno.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_anno.Visible = True
    Me.td_anno.VisibleIndex = 1
    Me.td_anno.Width = 58
    '
    'td_serie
    '
    Me.td_serie.AppearanceCell.Options.UseBackColor = True
    Me.td_serie.AppearanceCell.Options.UseTextOptions = True
    Me.td_serie.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_serie.Caption = "Serie"
    Me.td_serie.Enabled = True
    Me.td_serie.FieldName = "td_serie"
    Me.td_serie.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_serie.Name = "td_serie"
    Me.td_serie.NTSRepositoryComboBox = Nothing
    Me.td_serie.NTSRepositoryItemCheck = Nothing
    Me.td_serie.NTSRepositoryItemMemo = Nothing
    Me.td_serie.NTSRepositoryItemText = Nothing
    Me.td_serie.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_serie.Visible = True
    Me.td_serie.VisibleIndex = 2
    Me.td_serie.Width = 46
    '
    'td_numord
    '
    Me.td_numord.AppearanceCell.Options.UseBackColor = True
    Me.td_numord.AppearanceCell.Options.UseTextOptions = True
    Me.td_numord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_numord.Caption = "Numero"
    Me.td_numord.Enabled = True
    Me.td_numord.FieldName = "td_numord"
    Me.td_numord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_numord.Name = "td_numord"
    Me.td_numord.NTSRepositoryComboBox = Nothing
    Me.td_numord.NTSRepositoryItemCheck = Nothing
    Me.td_numord.NTSRepositoryItemMemo = Nothing
    Me.td_numord.NTSRepositoryItemText = Nothing
    Me.td_numord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_numord.Visible = True
    Me.td_numord.VisibleIndex = 3
    Me.td_numord.Width = 91
    '
    'td_datord
    '
    Me.td_datord.AppearanceCell.Options.UseBackColor = True
    Me.td_datord.AppearanceCell.Options.UseTextOptions = True
    Me.td_datord.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datord.Caption = "Data ordine"
    Me.td_datord.Enabled = True
    Me.td_datord.FieldName = "td_datord"
    Me.td_datord.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datord.Name = "td_datord"
    Me.td_datord.NTSRepositoryComboBox = Nothing
    Me.td_datord.NTSRepositoryItemCheck = Nothing
    Me.td_datord.NTSRepositoryItemMemo = Nothing
    Me.td_datord.NTSRepositoryItemText = Nothing
    Me.td_datord.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_datord.Visible = True
    Me.td_datord.VisibleIndex = 4
    Me.td_datord.Width = 82
    '
    'td_conto
    '
    Me.td_conto.AppearanceCell.Options.UseBackColor = True
    Me.td_conto.AppearanceCell.Options.UseTextOptions = True
    Me.td_conto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_conto.Caption = "Codice conto"
    Me.td_conto.Enabled = True
    Me.td_conto.FieldName = "td_conto"
    Me.td_conto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_conto.Name = "td_conto"
    Me.td_conto.NTSRepositoryComboBox = Nothing
    Me.td_conto.NTSRepositoryItemCheck = Nothing
    Me.td_conto.NTSRepositoryItemMemo = Nothing
    Me.td_conto.NTSRepositoryItemText = Nothing
    Me.td_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_conto.Visible = True
    Me.td_conto.VisibleIndex = 5
    Me.td_conto.Width = 90
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
    Me.xx_conto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_conto.Visible = True
    Me.xx_conto.VisibleIndex = 6
    Me.xx_conto.Width = 112
    '
    'td_datcons
    '
    Me.td_datcons.AppearanceCell.Options.UseBackColor = True
    Me.td_datcons.AppearanceCell.Options.UseTextOptions = True
    Me.td_datcons.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datcons.Caption = "Data consegna"
    Me.td_datcons.Enabled = True
    Me.td_datcons.FieldName = "td_datcons"
    Me.td_datcons.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datcons.Name = "td_datcons"
    Me.td_datcons.NTSRepositoryComboBox = Nothing
    Me.td_datcons.NTSRepositoryItemCheck = Nothing
    Me.td_datcons.NTSRepositoryItemMemo = Nothing
    Me.td_datcons.NTSRepositoryItemText = Nothing
    Me.td_datcons.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_datcons.Visible = True
    Me.td_datcons.VisibleIndex = 7
    '
    'td_totdoc
    '
    Me.td_totdoc.AppearanceCell.Options.UseBackColor = True
    Me.td_totdoc.AppearanceCell.Options.UseTextOptions = True
    Me.td_totdoc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_totdoc.Caption = "Tot. documento"
    Me.td_totdoc.Enabled = True
    Me.td_totdoc.FieldName = "td_totdoc"
    Me.td_totdoc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_totdoc.Name = "td_totdoc"
    Me.td_totdoc.NTSRepositoryComboBox = Nothing
    Me.td_totdoc.NTSRepositoryItemCheck = Nothing
    Me.td_totdoc.NTSRepositoryItemMemo = Nothing
    Me.td_totdoc.NTSRepositoryItemText = Nothing
    Me.td_totdoc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_totdoc.Visible = True
    Me.td_totdoc.VisibleIndex = 8
    '
    'td_flevas
    '
    Me.td_flevas.AppearanceCell.Options.UseBackColor = True
    Me.td_flevas.AppearanceCell.Options.UseTextOptions = True
    Me.td_flevas.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_flevas.Caption = "Evaso"
    Me.td_flevas.Enabled = True
    Me.td_flevas.FieldName = "td_flevas"
    Me.td_flevas.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_flevas.Name = "td_flevas"
    Me.td_flevas.NTSRepositoryComboBox = Nothing
    Me.td_flevas.NTSRepositoryItemCheck = Nothing
    Me.td_flevas.NTSRepositoryItemMemo = Nothing
    Me.td_flevas.NTSRepositoryItemText = Nothing
    Me.td_flevas.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_flevas.Visible = True
    Me.td_flevas.VisibleIndex = 9
    '
    'td_confermato
    '
    Me.td_confermato.AppearanceCell.Options.UseBackColor = True
    Me.td_confermato.AppearanceCell.Options.UseTextOptions = True
    Me.td_confermato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_confermato.Caption = "Confermato"
    Me.td_confermato.Enabled = True
    Me.td_confermato.FieldName = "td_confermato"
    Me.td_confermato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_confermato.Name = "td_confermato"
    Me.td_confermato.NTSRepositoryComboBox = Nothing
    Me.td_confermato.NTSRepositoryItemCheck = Nothing
    Me.td_confermato.NTSRepositoryItemMemo = Nothing
    Me.td_confermato.NTSRepositoryItemText = Nothing
    Me.td_confermato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_confermato.Visible = True
    Me.td_confermato.VisibleIndex = 10
    '
    'td_coddest
    '
    Me.td_coddest.AppearanceCell.Options.UseBackColor = True
    Me.td_coddest.AppearanceCell.Options.UseTextOptions = True
    Me.td_coddest.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_coddest.Caption = "Cod. destin."
    Me.td_coddest.Enabled = True
    Me.td_coddest.FieldName = "td_coddest"
    Me.td_coddest.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_coddest.Name = "td_coddest"
    Me.td_coddest.NTSRepositoryComboBox = Nothing
    Me.td_coddest.NTSRepositoryItemCheck = Nothing
    Me.td_coddest.NTSRepositoryItemMemo = Nothing
    Me.td_coddest.NTSRepositoryItemText = Nothing
    Me.td_coddest.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_coddest.Visible = True
    Me.td_coddest.VisibleIndex = 11
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
    Me.xx_destin.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_destin.Visible = True
    Me.xx_destin.VisibleIndex = 12
    '
    'td_commeca
    '
    Me.td_commeca.AppearanceCell.Options.UseBackColor = True
    Me.td_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.td_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_commeca.Caption = "Commessa"
    Me.td_commeca.Enabled = True
    Me.td_commeca.FieldName = "td_commeca"
    Me.td_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_commeca.Name = "td_commeca"
    Me.td_commeca.NTSRepositoryComboBox = Nothing
    Me.td_commeca.NTSRepositoryItemCheck = Nothing
    Me.td_commeca.NTSRepositoryItemMemo = Nothing
    Me.td_commeca.NTSRepositoryItemText = Nothing
    Me.td_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_commeca.Visible = True
    Me.td_commeca.VisibleIndex = 13
    '
    'xx_commeca
    '
    Me.xx_commeca.AppearanceCell.Options.UseBackColor = True
    Me.xx_commeca.AppearanceCell.Options.UseTextOptions = True
    Me.xx_commeca.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_commeca.Caption = "Descr. commessa"
    Me.xx_commeca.Enabled = True
    Me.xx_commeca.FieldName = "xx_commeca"
    Me.xx_commeca.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_commeca.Name = "xx_commeca"
    Me.xx_commeca.NTSRepositoryComboBox = Nothing
    Me.xx_commeca.NTSRepositoryItemCheck = Nothing
    Me.xx_commeca.NTSRepositoryItemMemo = Nothing
    Me.xx_commeca.NTSRepositoryItemText = Nothing
    Me.xx_commeca.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_commeca.Visible = True
    Me.xx_commeca.VisibleIndex = 14
    '
    'td_riferim
    '
    Me.td_riferim.AppearanceCell.Options.UseBackColor = True
    Me.td_riferim.AppearanceCell.Options.UseTextOptions = True
    Me.td_riferim.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_riferim.Caption = "Riferimenti"
    Me.td_riferim.Enabled = True
    Me.td_riferim.FieldName = "td_riferim"
    Me.td_riferim.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_riferim.Name = "td_riferim"
    Me.td_riferim.NTSRepositoryComboBox = Nothing
    Me.td_riferim.NTSRepositoryItemCheck = Nothing
    Me.td_riferim.NTSRepositoryItemMemo = Nothing
    Me.td_riferim.NTSRepositoryItemText = Nothing
    Me.td_riferim.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_riferim.Visible = True
    Me.td_riferim.VisibleIndex = 15
    '
    'td_annpar
    '
    Me.td_annpar.AppearanceCell.Options.UseBackColor = True
    Me.td_annpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_annpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_annpar.Caption = "Anno p."
    Me.td_annpar.Enabled = True
    Me.td_annpar.FieldName = "td_annpar"
    Me.td_annpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_annpar.Name = "td_annpar"
    Me.td_annpar.NTSRepositoryComboBox = Nothing
    Me.td_annpar.NTSRepositoryItemCheck = Nothing
    Me.td_annpar.NTSRepositoryItemMemo = Nothing
    Me.td_annpar.NTSRepositoryItemText = Nothing
    Me.td_annpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_annpar.Visible = True
    Me.td_annpar.VisibleIndex = 16
    '
    'td_alfpar
    '
    Me.td_alfpar.AppearanceCell.Options.UseBackColor = True
    Me.td_alfpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_alfpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_alfpar.Caption = "Serie p."
    Me.td_alfpar.Enabled = True
    Me.td_alfpar.FieldName = "td_alfpar"
    Me.td_alfpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_alfpar.Name = "td_alfpar"
    Me.td_alfpar.NTSRepositoryComboBox = Nothing
    Me.td_alfpar.NTSRepositoryItemCheck = Nothing
    Me.td_alfpar.NTSRepositoryItemMemo = Nothing
    Me.td_alfpar.NTSRepositoryItemText = Nothing
    Me.td_alfpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_alfpar.Visible = True
    Me.td_alfpar.VisibleIndex = 17
    '
    'td_numpar
    '
    Me.td_numpar.AppearanceCell.Options.UseBackColor = True
    Me.td_numpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_numpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_numpar.Caption = "Num. par."
    Me.td_numpar.Enabled = True
    Me.td_numpar.FieldName = "td_numpar"
    Me.td_numpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_numpar.Name = "td_numpar"
    Me.td_numpar.NTSRepositoryComboBox = Nothing
    Me.td_numpar.NTSRepositoryItemCheck = Nothing
    Me.td_numpar.NTSRepositoryItemMemo = Nothing
    Me.td_numpar.NTSRepositoryItemText = Nothing
    Me.td_numpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_numpar.Visible = True
    Me.td_numpar.VisibleIndex = 18
    '
    'td_datpar
    '
    Me.td_datpar.AppearanceCell.Options.UseBackColor = True
    Me.td_datpar.AppearanceCell.Options.UseTextOptions = True
    Me.td_datpar.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_datpar.Caption = "Data par."
    Me.td_datpar.Enabled = True
    Me.td_datpar.FieldName = "td_datpar"
    Me.td_datpar.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_datpar.Name = "td_datpar"
    Me.td_datpar.NTSRepositoryComboBox = Nothing
    Me.td_datpar.NTSRepositoryItemCheck = Nothing
    Me.td_datpar.NTSRepositoryItemMemo = Nothing
    Me.td_datpar.NTSRepositoryItemText = Nothing
    Me.td_datpar.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_datpar.Visible = True
    Me.td_datpar.VisibleIndex = 19
    '
    'td_tipobf
    '
    Me.td_tipobf.AppearanceCell.Options.UseBackColor = True
    Me.td_tipobf.AppearanceCell.Options.UseTextOptions = True
    Me.td_tipobf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_tipobf.Caption = "Tipo b/f"
    Me.td_tipobf.Enabled = True
    Me.td_tipobf.FieldName = "td_tipobf"
    Me.td_tipobf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_tipobf.Name = "td_tipobf"
    Me.td_tipobf.NTSRepositoryComboBox = Nothing
    Me.td_tipobf.NTSRepositoryItemCheck = Nothing
    Me.td_tipobf.NTSRepositoryItemMemo = Nothing
    Me.td_tipobf.NTSRepositoryItemText = Nothing
    Me.td_tipobf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_tipobf.Visible = True
    Me.td_tipobf.VisibleIndex = 20
    '
    'xx_tipobf
    '
    Me.xx_tipobf.AppearanceCell.Options.UseBackColor = True
    Me.xx_tipobf.AppearanceCell.Options.UseTextOptions = True
    Me.xx_tipobf.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_tipobf.Caption = "Descr. Tipo b/f"
    Me.xx_tipobf.Enabled = True
    Me.xx_tipobf.FieldName = "xx_tipobf"
    Me.xx_tipobf.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_tipobf.Name = "xx_tipobf"
    Me.xx_tipobf.NTSRepositoryComboBox = Nothing
    Me.xx_tipobf.NTSRepositoryItemCheck = Nothing
    Me.xx_tipobf.NTSRepositoryItemMemo = Nothing
    Me.xx_tipobf.NTSRepositoryItemText = Nothing
    Me.xx_tipobf.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_tipobf.Visible = True
    Me.xx_tipobf.VisibleIndex = 21
    '
    'td_flstam
    '
    Me.td_flstam.AppearanceCell.Options.UseBackColor = True
    Me.td_flstam.AppearanceCell.Options.UseTextOptions = True
    Me.td_flstam.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_flstam.Caption = "Stampato"
    Me.td_flstam.Enabled = True
    Me.td_flstam.FieldName = "td_flstam"
    Me.td_flstam.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_flstam.Name = "td_flstam"
    Me.td_flstam.NTSRepositoryComboBox = Nothing
    Me.td_flstam.NTSRepositoryItemCheck = Nothing
    Me.td_flstam.NTSRepositoryItemMemo = Nothing
    Me.td_flstam.NTSRepositoryItemText = Nothing
    Me.td_flstam.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_flstam.Visible = True
    Me.td_flstam.VisibleIndex = 22
    '
    'td_rilasciato
    '
    Me.td_rilasciato.AppearanceCell.Options.UseBackColor = True
    Me.td_rilasciato.AppearanceCell.Options.UseTextOptions = True
    Me.td_rilasciato.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_rilasciato.Caption = "Rilasciato"
    Me.td_rilasciato.Enabled = True
    Me.td_rilasciato.FieldName = "td_rilasciato"
    Me.td_rilasciato.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_rilasciato.Name = "td_rilasciato"
    Me.td_rilasciato.NTSRepositoryComboBox = Nothing
    Me.td_rilasciato.NTSRepositoryItemCheck = Nothing
    Me.td_rilasciato.NTSRepositoryItemMemo = Nothing
    Me.td_rilasciato.NTSRepositoryItemText = Nothing
    Me.td_rilasciato.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_rilasciato.Visible = True
    Me.td_rilasciato.VisibleIndex = 23
    '
    'td_aperto
    '
    Me.td_aperto.AppearanceCell.Options.UseBackColor = True
    Me.td_aperto.AppearanceCell.Options.UseTextOptions = True
    Me.td_aperto.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_aperto.Caption = "Aperto"
    Me.td_aperto.Enabled = True
    Me.td_aperto.FieldName = "td_aperto"
    Me.td_aperto.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_aperto.Name = "td_aperto"
    Me.td_aperto.NTSRepositoryComboBox = Nothing
    Me.td_aperto.NTSRepositoryItemCheck = Nothing
    Me.td_aperto.NTSRepositoryItemMemo = Nothing
    Me.td_aperto.NTSRepositoryItemText = Nothing
    Me.td_aperto.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_aperto.Visible = True
    Me.td_aperto.VisibleIndex = 24
    '
    'td_sospeso
    '
    Me.td_sospeso.AppearanceCell.Options.UseBackColor = True
    Me.td_sospeso.AppearanceCell.Options.UseTextOptions = True
    Me.td_sospeso.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_sospeso.Caption = "Sospeso"
    Me.td_sospeso.Enabled = True
    Me.td_sospeso.FieldName = "td_sospeso"
    Me.td_sospeso.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_sospeso.Name = "td_sospeso"
    Me.td_sospeso.NTSRepositoryComboBox = Nothing
    Me.td_sospeso.NTSRepositoryItemCheck = Nothing
    Me.td_sospeso.NTSRepositoryItemMemo = Nothing
    Me.td_sospeso.NTSRepositoryItemText = Nothing
    Me.td_sospeso.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_sospeso.Visible = True
    Me.td_sospeso.VisibleIndex = 25
    '
    'td_magaz
    '
    Me.td_magaz.AppearanceCell.Options.UseBackColor = True
    Me.td_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.td_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_magaz.Caption = "Mag. 1"
    Me.td_magaz.Enabled = True
    Me.td_magaz.FieldName = "td_magaz"
    Me.td_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_magaz.Name = "td_magaz"
    Me.td_magaz.NTSRepositoryComboBox = Nothing
    Me.td_magaz.NTSRepositoryItemCheck = Nothing
    Me.td_magaz.NTSRepositoryItemMemo = Nothing
    Me.td_magaz.NTSRepositoryItemText = Nothing
    Me.td_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_magaz.Visible = True
    Me.td_magaz.VisibleIndex = 26
    '
    'xx_magaz
    '
    Me.xx_magaz.AppearanceCell.Options.UseBackColor = True
    Me.xx_magaz.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magaz.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magaz.Caption = "Descr. Mag. 1"
    Me.xx_magaz.Enabled = True
    Me.xx_magaz.FieldName = "xx_magaz"
    Me.xx_magaz.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magaz.Name = "xx_magaz"
    Me.xx_magaz.NTSRepositoryComboBox = Nothing
    Me.xx_magaz.NTSRepositoryItemCheck = Nothing
    Me.xx_magaz.NTSRepositoryItemMemo = Nothing
    Me.xx_magaz.NTSRepositoryItemText = Nothing
    Me.xx_magaz.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_magaz.Visible = True
    Me.xx_magaz.VisibleIndex = 27
    '
    'td_magaz2
    '
    Me.td_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.td_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.td_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_magaz2.Caption = "Mag. 2"
    Me.td_magaz2.Enabled = True
    Me.td_magaz2.FieldName = "td_magaz2"
    Me.td_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_magaz2.Name = "td_magaz2"
    Me.td_magaz2.NTSRepositoryComboBox = Nothing
    Me.td_magaz2.NTSRepositoryItemCheck = Nothing
    Me.td_magaz2.NTSRepositoryItemMemo = Nothing
    Me.td_magaz2.NTSRepositoryItemText = Nothing
    Me.td_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_magaz2.Visible = True
    Me.td_magaz2.VisibleIndex = 28
    '
    'xx_magaz2
    '
    Me.xx_magaz2.AppearanceCell.Options.UseBackColor = True
    Me.xx_magaz2.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magaz2.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magaz2.Caption = "Descr. Mag. 2"
    Me.xx_magaz2.Enabled = True
    Me.xx_magaz2.FieldName = "xx_magaz2"
    Me.xx_magaz2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magaz2.Name = "xx_magaz2"
    Me.xx_magaz2.NTSRepositoryComboBox = Nothing
    Me.xx_magaz2.NTSRepositoryItemCheck = Nothing
    Me.xx_magaz2.NTSRepositoryItemMemo = Nothing
    Me.xx_magaz2.NTSRepositoryItemText = Nothing
    Me.xx_magaz2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_magaz2.Visible = True
    Me.xx_magaz2.VisibleIndex = 29
    '
    'td_magimp
    '
    Me.td_magimp.AppearanceCell.Options.UseBackColor = True
    Me.td_magimp.AppearanceCell.Options.UseTextOptions = True
    Me.td_magimp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_magimp.Caption = "Mag. Imp."
    Me.td_magimp.Enabled = True
    Me.td_magimp.FieldName = "td_magimp"
    Me.td_magimp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_magimp.Name = "td_magimp"
    Me.td_magimp.NTSRepositoryComboBox = Nothing
    Me.td_magimp.NTSRepositoryItemCheck = Nothing
    Me.td_magimp.NTSRepositoryItemMemo = Nothing
    Me.td_magimp.NTSRepositoryItemText = Nothing
    Me.td_magimp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_magimp.Visible = True
    Me.td_magimp.VisibleIndex = 30
    '
    'xx_magimp
    '
    Me.xx_magimp.AppearanceCell.Options.UseBackColor = True
    Me.xx_magimp.AppearanceCell.Options.UseTextOptions = True
    Me.xx_magimp.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.xx_magimp.Caption = "Descr. Mag. Imp."
    Me.xx_magimp.Enabled = True
    Me.xx_magimp.FieldName = "xx_magimp"
    Me.xx_magimp.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.xx_magimp.Name = "xx_magimp"
    Me.xx_magimp.NTSRepositoryComboBox = Nothing
    Me.xx_magimp.NTSRepositoryItemCheck = Nothing
    Me.xx_magimp.NTSRepositoryItemMemo = Nothing
    Me.xx_magimp.NTSRepositoryItemText = Nothing
    Me.xx_magimp.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.xx_magimp.Visible = True
    Me.xx_magimp.VisibleIndex = 31
    '
    'td_totmerce
    '
    Me.td_totmerce.AppearanceCell.Options.UseBackColor = True
    Me.td_totmerce.AppearanceCell.Options.UseTextOptions = True
    Me.td_totmerce.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.td_totmerce.Caption = "Totale merce"
    Me.td_totmerce.Enabled = True
    Me.td_totmerce.FieldName = "td_totmerce"
    Me.td_totmerce.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.td_totmerce.Name = "td_totmerce"
    Me.td_totmerce.NTSRepositoryComboBox = Nothing
    Me.td_totmerce.NTSRepositoryItemCheck = Nothing
    Me.td_totmerce.NTSRepositoryItemMemo = Nothing
    Me.td_totmerce.NTSRepositoryItemText = Nothing
    Me.td_totmerce.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[True]
    Me.td_totmerce.Visible = True
    Me.td_totmerce.VisibleIndex = 32
    '
    'NtsTabPage2
    '
    Me.NtsTabPage2.AllowDrop = True
    Me.NtsTabPage2.Controls.Add(Me.pnSel1)
    Me.NtsTabPage2.Enable = True
    Me.NtsTabPage2.Name = "NtsTabPage2"
    Me.NtsTabPage2.Size = New System.Drawing.Size(627, 410)
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
    Me.pnSel1.Size = New System.Drawing.Size(627, 410)
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
    Me.ceFiltriExt.Size = New System.Drawing.Size(627, 410)
    Me.ceFiltriExt.strNomeCampo = ""
    Me.ceFiltriExt.TabIndex = 0
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
    'cmdDesAll
    '
    Me.cmdDesAll.ImagePath = ""
    Me.cmdDesAll.ImageText = ""
    Me.cmdDesAll.Location = New System.Drawing.Point(3, 72)
    Me.cmdDesAll.Name = "cmdDesAll"
    Me.cmdDesAll.NTSContextMenu = Nothing
    Me.cmdDesAll.Size = New System.Drawing.Size(106, 23)
    Me.cmdDesAll.TabIndex = 58
    Me.cmdDesAll.Text = "Deseleziona tutto"
    '
    'cmdSelAll
    '
    Me.cmdSelAll.ImagePath = ""
    Me.cmdSelAll.ImageText = ""
    Me.cmdSelAll.Location = New System.Drawing.Point(3, 49)
    Me.cmdSelAll.Name = "cmdSelAll"
    Me.cmdSelAll.NTSContextMenu = Nothing
    Me.cmdSelAll.Size = New System.Drawing.Size(106, 23)
    Me.cmdSelAll.TabIndex = 57
    Me.cmdSelAll.Text = "Seleziona tutto"
    '
    'FRMORSEDO
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.CancelButton = Me.cmdAnnulla
    Me.ClientSize = New System.Drawing.Size(636, 440)
    Me.Controls.Add(Me.pnTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.MinimizeBox = False
    Me.Name = "FRMORSEDO"
    Me.NTSLastControlFocussed = Me.grSeor
    Me.Text = "SELEZIONE ORDINI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnfiltri, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnfiltri.ResumeLayout(False)
    Me.pnfiltri.PerformLayout()
    CType(Me.cbEt_blocco.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnCommandbutton, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCommandbutton.ResumeLayout(False)
    CType(Me.cbEt_sospeso.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edRiferim.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatConsA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDatConsDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edContoDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataA.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDataDa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmEvaso, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmEvaso.ResumeLayout(False)
    Me.fmEvaso.PerformLayout()
    CType(Me.optTutti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optNo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.optSi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    CType(Me.tsSel, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsSel.ResumeLayout(False)
    Me.NtsTabPage1.ResumeLayout(False)
    CType(Me.pnGrid, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnGrid.ResumeLayout(False)
    CType(Me.grSeor, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvSeor, System.ComponentModel.ISupportInitialize).EndInit()
    Me.NtsTabPage2.ResumeLayout(False)
    CType(Me.pnSel1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnSel1.ResumeLayout(False)
    CType(Me.grFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFiltri1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnSel2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grFiltri2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsGridView2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvFiltri2, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Public Overridable Sub FRMORSEDO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim strSoloSerie As String = ""
    Try
      InitControls()

      lbAnno.Text = oApp.Tr(Me, 128674147271246250, "Anno: ") & strAnno
      edContoDa.Text = "0"
      edContoA.Text = "999999999"
      edDataDa.Text = IntSetDate("01/01/" & strAnno)
      edDataA.Text = IntSetDate("31/12/2099")
      edDatConsDa.Text = IntSetDate("01/01/" & strAnno)
      edDatConsA.Text = IntSetDate("31/12/2099")
      edSerie.Text = ""

      tsSel.SelectedTabPageIndex = 0
      tsSel.Enabled = True

      CaricaCombo()

      '-------------------------------
      'applico le impostazioni da GCTL
      GctlTipoDoc = " "
      GctlSetRoules()
      GctlApplicaDefaultValue()

      'posso modificare solo la colonna 'seleziona
      If xx_sel.Visible And xx_sel.Enabled Then
        grvSeor.Enabled = True
        For Each dtrCol As NTSGridColumn In grvSeor.Columns
          If dtrCol.Name <> "xx_sel" Then dtrCol.Enabled = False
        Next
      Else
        cmdSelAll.Enabled = False
        cmdDesAll.Enabled = False
        cmdSelAll.Visible = False
        cmdDesAll.Visible = False
      End If

      strSoloSerie = oMenu.GetSettingBusDitt(DittaCorrente, "Bsorgsor", "OpzioniDocUt", ".", "SoloSerie", "?", strTipork, "?")
      If strSoloSerie = "" Then strSoloSerie = " "
      If strSoloSerie <> "?" Then
        edSerie.Text = strSoloSerie
        edSerie.Enabled = False
      End If

      'limitazioni per CRM
      If CBool((oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCRM)) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then
        oCleGsor.bModuloCRM = True
        oCleGsor.bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, oCleGsor.bAmm, oCleGsor.strAccvis, oCleGsor.strAccmod, oCleGsor.strRegvis, oCleGsor.strRegmod)
        If oCleGsor.bIsCRMUser = True Then
          oCleGsor.nSedoCodageAccdito = oCleGsor.RitornaCodcageAccdito
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Se chiave di attivazione Friendly nasconde, sempre, alcuni controlli
      '--------------------------------------------------------------------------------------------------------------
      If CLN__STD.FRIENDLY = True Then
        lbStato.Visible = False
        cbEt_sospeso.Visible = False
        td_confermato.Visible = False
        td_commeca.Visible = False
        xx_commeca.Visible = False
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub FRMORSEDO_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      edContoDa.Focus()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub FRMORSEDO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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


  Public Overridable Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeleziona.Click
    If dsSeor Is Nothing Then Return
    If dsSeor.Tables("SEOR").Rows.Count > 0 Then
      oPar.bPar1 = True
      oPar.strPar2 = grvSeor.GetFocusedRowCellValue(td_serie).ToString()
      oPar.strPar4 = grvSeor.GetFocusedRowCellValue(td_numord).ToString()

      grvSeor.NTSGetCurrentDataRow!xx_sel = "S"
      dsSeor.Tables("SEOR").AcceptChanges()

      'memorizzo l'elenco dei documenti da aprire e lo restituisco a bsveboll
      dttOut = New DataTable
      dttOut.Columns.Add("td_tipork", GetType(String))
      dttOut.Columns.Add("td_anno", GetType(Integer))
      dttOut.Columns.Add("td_serie", GetType(String))
      dttOut.Columns.Add("td_numord", GetType(Integer))
      For Each dtrT As DataRow In dsSeor.Tables("SEOR").Select("xx_sel = 'S'", "td_tipork, td_anno, td_serie, td_numord")
        dttOut.Rows.Add(New Object() {dtrT!td_tipork, dtrT!td_anno, dtrT!td_serie, dtrT!td_numord})
      Next
      dttOut.AcceptChanges()
    Else
      oPar.bPar1 = False
      dttOut = Nothing
    End If
    Me.Close()
  End Sub

  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    oPar.bPar1 = False
    dttOut = Nothing
    Me.Close()
  End Sub

  Public Overridable Sub cmdRicerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRicerca.Click
    Dim strEvaso As String = ""
    Try
      If optSi.Checked Then strEvaso = "S"
      If optNo.Checked Then strEvaso = "N"
      Me.Cursor = Cursors.WaitCursor

      oCleGsor.strSospeso = cbEt_sospeso.SelectedValue
      oCleGsor.strBlocco = cbEt_blocco.SelectedValue
      dsSeor = oCleGsor.ZoomSeor(strTipork, CType(strAnno, Integer), edSerie.Text, CType(edContoDa.Text, Integer), _
                                 CType(edContoA.Text, Integer), edDataDa.Text, edDataA.Text, edDatConsDa.Text, _
                                 edDatConsA.Text, strEvaso, edRiferim.Text.Trim, DittaCorrente, _
                                 ceFiltriExt.GeneraQuerySQL(), "")
      dsSeor.Tables("SEOR").Columns.Add("xx_sel", GetType(String))
      dcSeor.DataSource = dsSeor.Tables("SEOR")
      dsSeor.AcceptChanges()
      grSeor.DataSource = dcSeor

      'mi sposto sulla griglia e faccio in modo che con enter venga selezionato il valore
      If grvSeor.RowCount > 0 Then
        grvSeor.MoveFirst()
        grvSeor.Focus()
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



  Public Overridable Sub grvSeor_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grvSeor.KeyDown
    Try
      If e.KeyCode = Keys.Enter Then If grvSeor.Focused And grvSeor.RowCount > 0 Then cmdOk_Click(Me, Nothing)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub grSeor_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grSeor.MouseDoubleClick
    Try
      cmdOk_Click(Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub edSerie_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.ValidatedAndChanged
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, True)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
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
      If dsSeor Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      For Each dtrRow As DataRow In dsSeor.Tables("SEOR").Rows
        dtrRow!xx_sel = IIf(bSeleziona = True, "S", "N")
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
        Select Case strTipork
          Case "Q", "R", "V"
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



#Region "Filtri Estesi - Non più usati"
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
          oParam.strTipo = "TESTORD"
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
          oParam.strTipo = "MOVORD"
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
                  oApp.MsgBoxErr(oApp.Tr(Me, 130398041097597714, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 130398041160482364, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

          If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea) <> "" Then
            dtrT = dttCampi.Select("cb_nomcampo = " & CStrSQL(dsFiltri.Tables("FILTRI1").Rows(i)!xx_nome))
            If NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valoreda) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 128584506055937500, "Se impostato un valore nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' deve essere impostato un valore anche nel filtro DA"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128584503331406250, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri.Tables("FILTRI1").Rows(i)!xx_valorea)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128584503236718750, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome) & "|' sono ammesse solo date"))
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
              oApp.MsgBoxErr(oApp.Tr(Me, 130398041032014968, "Se impostato un valore nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome2) & "|' deve essere impostato un valore anche nel filtro A"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo2.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128735523533622000, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome2) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 128492077570882500, "Nel filtro DA '|" & NTSCStr(dtrT(0)!xx_nome2) & "|' sono ammesse solo date"))
                  Return False
                End If
            End Select
          End If

          If NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2) <> "" Then
            dtrT = dttCampi2.Select("cb_nomcampo2 = " & CStrSQL(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_nome2))
            If NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valoreda2) = "" Then
              oApp.MsgBoxErr(oApp.Tr(Me, 130398040755547812, "Se impostato un valore nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome2) & "|' deve essere impostato un valore anche nel filtro DA"))
              Return False
            End If
            '3 short - 4 long - 5, 6, 7 double - 8 data - 10 stringa - 11 ole - 12 memo (ultimi 2 mai estratti)
            Select Case NTSCInt(dtrT(0)!cb_tipocampo2.ToString)
              Case 3, 4, 5, 6, 7
                If Not IsNumeric(NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 130398041221429405, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome2) & "|' sono ammessi solo numeri"))
                  Return False
                End If
              Case 8
                If Not IsDate(NTSCStr(dsFiltri2.Tables("FILTRI2").Rows(i)!xx_valorea2)) Then
                  oApp.MsgBoxErr(oApp.Tr(Me, 130398040714271043, "Nel filtro A '|" & NTSCStr(dtrT(0)!xx_nome2) & "|' sono ammesse solo date"))
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
      strTmp = NTSCStr(oMenu.GetSettingBus("BNORGSOR", "RECENT", ".", "Filtri1", "", " ", ""))
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
      strTmp = NTSCStr(oMenu.GetSettingBus("BNORGSOR", "RECENT", ".", "Filtri2", "", " ", ""))
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
              oApp.MsgBoxInfo(oApp.Tr(Me, 130398040607729361, "Attenzione!" & vbCrLf & _
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
