Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGSCHC_F

#Region "Moduli"
  Private Moduli_P As Integer = bsModVE + bsModMG
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

#Region "Variabili"
  Public oCleSchc As CLEMGSCHC
  Public oCallParams As CLE__CLDP
  Public dsSchc As New DataSet
  Public dcSchc As New BindingSource
  Public dttCampi As New DataTable
  Public dttCorpocampi As New DataTable
  Public Const bsDbAzienda As Boolean = False
  Public strWhereArtico As String = ""
  Public bModExtTCO As Boolean
  Public bModuloCrm As Boolean
  Public bIsCRMUser As Boolean
  Public bAmm As Boolean
  Public strAccvis As String
  Public strRegvis As String
  Public lCodorgaOperat As Integer
  Public bFiltri As Boolean = True
  Public bDaGest As Boolean = False
  Private components As System.ComponentModel.IContainer

  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaGriglia As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbAlmaga As NTSInformatica.NTSLabel
  Public WithEvents lbAlconto As NTSInformatica.NTSLabel
  Public WithEvents lbDamaga As NTSInformatica.NTSLabel
  Public WithEvents lbAldata As NTSInformatica.NTSLabel
  Public WithEvents lbAgente As NTSInformatica.NTSLabel
  Public WithEvents lbCausa As NTSInformatica.NTSLabel
  Public WithEvents lbDaconto As NTSInformatica.NTSLabel
  Public WithEvents lbDadata As NTSInformatica.NTSLabel
  Public WithEvents edDadata As NTSInformatica.NTSTextBoxData
  Public WithEvents edAldata As NTSInformatica.NTSTextBoxData
  Public WithEvents edSerie As NTSInformatica.NTSTextBoxStr
  Public WithEvents edAgente As NTSInformatica.NTSTextBoxNum
  Public WithEvents edCausa As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAlconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDaconto As NTSInformatica.NTSTextBoxNum
  Public WithEvents edAlmaga As NTSInformatica.NTSTextBoxNum
  Public WithEvents edDamaga As NTSInformatica.NTSTextBoxNum
  Public WithEvents cbTipodoc As NTSInformatica.NTSComboBox
  Public WithEvents ckTipodoc As NTSInformatica.NTSCheckBox
  Public WithEvents fmClifor As NTSInformatica.NTSGroupBox
  Public WithEvents opFornitori As NTSInformatica.NTSRadioButton
  Public WithEvents opClienti As NTSInformatica.NTSRadioButton
  Public WithEvents NtsButton1 As NTSInformatica.NTSButton
  Public WithEvents lbDesagente As NTSInformatica.NTSLabel
  Public WithEvents lbDescausa As NTSInformatica.NTSLabel
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents ckSerie As NTSInformatica.NTSCheckBox
#End Region

#Region "EVENTI DI FORM"
  Public Overridable Sub FRMMGSCHC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim dttRelease As New DataTable
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oMenu.GetBusRelease(dttRelease) = True Then
        If dttRelease.Rows.Count <> 0 Then
          With dttRelease.Rows(0)
            If NTSCInt(!rel_maior) >= 15 Or (NTSCInt(!rel_maior) = 14 And NTSCStr(!rel_pers) = "a") Then oCleSchc.bIs15 = True
          End With
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      bModuloCrm = False
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtCRM) Or _
         CBool(oMenu.ModuliSupDittaDitt(DittaCorrente) And CLN__STD.bsModSupWCR) Then bModuloCrm = True
      '--------------------------------------------------------------------------------------------------------------
      If bModuloCrm Then
        bIsCRMUser = oMenu.IsCrmUser(DittaCorrente, bAmm, strAccvis, "", strRegvis, "")
        If bIsCRMUser Then
          lCodorgaOperat = oMenu.RitornaCodorgaDaOpnome(DittaCorrente)
          If lCodorgaOperat = 0 Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128644940096875000, "Attenzione!" & vbCrLf & _
              "L'operatore '|" & oApp.User.Nome & "|' (CRM) non è associato all'organizzazione della ditta corrente '|" & DittaCorrente & "|'." & vbCrLf & _
              "Impossibile continuare."))
            Me.Close()
            Return
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (bModuloCrm = True) And (bIsCRMUser = True) And (oCleSchc.bIs15 = True) Then
        oCleSchc.lIITtsubqcrm = oMenu.GetTblInstId("TTSUBQCRM", False)
        oCleSchc.nCodcageAccdito = oCleSchc.RitornaCodcageAccdito(bModuloCrm, bIsCRMUser)
      End If
      '--------------------------------------------------------------------------------------------------------------

      oCleSchc.bModExtTCO = CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtTCO)
      oCleSchc.bModuloCrm = bModuloCrm
      oCleSchc.bIsCRMUser = bIsCRMUser
      oCleSchc.bAmm = bAmm
      oCleSchc.strAccvis = strAccvis
      oCleSchc.strRegvis = strRegvis
      oCleSchc.lCodorgaOperat = lCodorgaOperat

      '--- FILTRI TESTATA ---
      'leggo dal database i potenziali filtri su artico
      If Not CType(oMenu.oCleComm, CLELBMENU).LeggiCampiPerHlvl("testmag", dttCampi) Then
        Me.Close()
        Return
      End If
      'cb_nomcampo, xx_nome, cb_tipocampo, cb_size, xx_valoreda, xx_valorea
      dttCampi.Rows.Add(New Object() {".", "N.A.", 0, 0, "", ""})
      dttCampi.AcceptChanges()

      '--- FILTRI CORPO ---
      'leggo dal database i potenziali filtri su artico
      If Not CType(oMenu.oCleComm, CLELBMENU).LeggiCampiPerHlvl("movmag", dttCorpocampi) Then
        Me.Close()
        Return
      End If
      'cb_nomcampo, xx_nome, cb_tipocampo, cb_size, xx_valoreda, xx_valorea
      dttCorpocampi.Rows.Add(New Object() {".", "N.A.", 0, 0, "", ""})
      dttCorpocampi.AcceptChanges()

      'predispongo i controlli
      InitControls()

      edAldata.Text = IntSetDate("31/12/" + NTSCStr(Now.Year))
      edDadata.Text = IntSetDate("01/01/" + NTSCStr(Now.Year))

      oCleSchc.bControlloSuEsistenza = CBool(oMenu.GetSettingBus("Bsmgschc", "Opzioni", ".", "ControlloSuEsistenza", "0", " ", "0"))

      CaricaCombo()

      edSerie.Enabled = False
      ckSerie.Checked = False

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()
      GctlApplicaDefaultValue()

      If Not oCallParams Is Nothing Then
        If oCallParams.strParam <> "" Then
          If Mid(oCallParams.strParam, 1, 1) = "C" Then opClienti.Checked = True Else opFornitori.Checked = True
          If NTSCInt(Mid(oCallParams.strParam, 3, 9)) <> 0 Then
            edDaconto.Text = NTSCInt(Mid(oCallParams.strParam, 3, 9)).ToString
          End If
          If NTSCInt(Mid(oCallParams.strParam, 33, 9)) <> 0 Then
            edAlconto.Text = NTSCInt(Mid(oCallParams.strParam, 13, 9)).ToString
          End If
          cbTipodoc.SelectedValue = Mid(oCallParams.strParam, 23, 1)
          If Trim(Mid(oCallParams.strParam, 25, 10)) <> "" Then
            edDadata.Text = Trim(Mid(oCallParams.strParam, 25, 10))
          End If
          If Trim(Mid(oCallParams.strParam, 36, 10)) <> "" Then
            edAldata.Text = Trim(Mid(oCallParams.strParam, 36, 10))
          End If
          bFiltri = CBool(Mid(oCallParams.strParam, 47, 1))
          bDaGest = True
          ckTipodoc.Checked = False

          If bFiltri = False Then
            StampaGriglia()
            Me.Close()
            Return
          End If
        End If
      End If


    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRMMGSCHC_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcSchc.Dispose()
      dsSchc.Dispose()
    Catch
    End Try
  End Sub

  Public Overridable Sub FRMMGSCHC_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      SvuotaTmpTable()

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
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
    'creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGSCHC", "BEMGSCHC", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128589409211093750, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleSchc = CType(oTmp, CLEMGSCHC)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGSCHC", strRemoteServer, strRemotePort)
    AddHandler oCleSchc.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleSchc.Init(oApp, NTSScript, oMenu.oCleComm, "TABANAZ", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function
  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGSCHC_F))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaGriglia = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.ckSerie = New NTSInformatica.NTSCheckBox
    Me.lbDadata = New NTSInformatica.NTSLabel
    Me.lbDaconto = New NTSInformatica.NTSLabel
    Me.lbCausa = New NTSInformatica.NTSLabel
    Me.lbDesagente = New NTSInformatica.NTSLabel
    Me.lbAgente = New NTSInformatica.NTSLabel
    Me.lbDescausa = New NTSInformatica.NTSLabel
    Me.fmClifor = New NTSInformatica.NTSGroupBox
    Me.opFornitori = New NTSInformatica.NTSRadioButton
    Me.opClienti = New NTSInformatica.NTSRadioButton
    Me.NtsButton1 = New NTSInformatica.NTSButton
    Me.lbAldata = New NTSInformatica.NTSLabel
    Me.cbTipodoc = New NTSInformatica.NTSComboBox
    Me.ckTipodoc = New NTSInformatica.NTSCheckBox
    Me.edSerie = New NTSInformatica.NTSTextBoxStr
    Me.lbDamaga = New NTSInformatica.NTSLabel
    Me.lbAlconto = New NTSInformatica.NTSLabel
    Me.edAgente = New NTSInformatica.NTSTextBoxNum
    Me.lbAlmaga = New NTSInformatica.NTSLabel
    Me.edCausa = New NTSInformatica.NTSTextBoxNum
    Me.edAldata = New NTSInformatica.NTSTextBoxData
    Me.edDadata = New NTSInformatica.NTSTextBoxData
    Me.edDamaga = New NTSInformatica.NTSTextBoxNum
    Me.edAlconto = New NTSInformatica.NTSTextBoxNum
    Me.edAlmaga = New NTSInformatica.NTSTextBoxNum
    Me.edDaconto = New NTSInformatica.NTSTextBoxNum
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.fmClifor, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.fmClifor.SuspendLayout()
    CType(Me.opFornitori.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.opClienti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTipodoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAgente.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edCausa.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAldata.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDadata.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDamaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAlconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAlmaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampaGriglia})
    Me.NtsBarManager1.MaxItemId = 8
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaGriglia), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbStampa
    '
    Me.tlbStampa.Caption = "Stampa"
    Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
    Me.tlbStampa.Id = 0
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "StampaVideo"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 1
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaGriglia
    '
    Me.tlbStampaGriglia.Caption = "Stampa su griglia"
    Me.tlbStampaGriglia.Glyph = CType(resources.GetObject("tlbStampaGriglia.Glyph"), System.Drawing.Image)
    Me.tlbStampaGriglia.Id = 7
    Me.tlbStampaGriglia.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
    Me.tlbStampaGriglia.Name = "tlbStampaGriglia"
    Me.tlbStampaGriglia.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 5
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta Stampante"
    Me.tlbImpostaStampante.Id = 6
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 2
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 3
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'ckSerie
    '
    Me.ckSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckSerie.Location = New System.Drawing.Point(354, 106)
    Me.ckSerie.Name = "ckSerie"
    Me.ckSerie.NTSCheckValue = "S"
    Me.ckSerie.NTSUnCheckValue = "N"
    Me.ckSerie.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckSerie.Properties.Appearance.Options.UseBackColor = True
    Me.ckSerie.Properties.Caption = "Serie"
    Me.ckSerie.Size = New System.Drawing.Size(45, 19)
    Me.ckSerie.TabIndex = 42
    '
    'lbDadata
    '
    Me.lbDadata.AutoSize = True
    Me.lbDadata.BackColor = System.Drawing.Color.Transparent
    Me.lbDadata.Location = New System.Drawing.Point(13, 42)
    Me.lbDadata.Name = "lbDadata"
    Me.lbDadata.NTSDbField = ""
    Me.lbDadata.Size = New System.Drawing.Size(55, 13)
    Me.lbDadata.TabIndex = 0
    Me.lbDadata.Text = "Dalla data"
    Me.lbDadata.Tooltip = ""
    Me.lbDadata.UseMnemonic = False
    '
    'lbDaconto
    '
    Me.lbDaconto.AutoSize = True
    Me.lbDaconto.BackColor = System.Drawing.Color.Transparent
    Me.lbDaconto.Location = New System.Drawing.Point(12, 86)
    Me.lbDaconto.Name = "lbDaconto"
    Me.lbDaconto.NTSDbField = ""
    Me.lbDaconto.Size = New System.Drawing.Size(52, 13)
    Me.lbDaconto.TabIndex = 1
    Me.lbDaconto.Text = "Dal conto"
    Me.lbDaconto.Tooltip = ""
    Me.lbDaconto.UseMnemonic = False
    '
    'lbCausa
    '
    Me.lbCausa.AutoSize = True
    Me.lbCausa.BackColor = System.Drawing.Color.Transparent
    Me.lbCausa.Location = New System.Drawing.Point(13, 130)
    Me.lbCausa.Name = "lbCausa"
    Me.lbCausa.NTSDbField = ""
    Me.lbCausa.Size = New System.Drawing.Size(45, 13)
    Me.lbCausa.TabIndex = 2
    Me.lbCausa.Text = "Causale"
    Me.lbCausa.Tooltip = ""
    Me.lbCausa.UseMnemonic = False
    '
    'lbDesagente
    '
    Me.lbDesagente.BackColor = System.Drawing.Color.Transparent
    Me.lbDesagente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDesagente.Location = New System.Drawing.Point(230, 151)
    Me.lbDesagente.Name = "lbDesagente"
    Me.lbDesagente.NTSDbField = ""
    Me.lbDesagente.Size = New System.Drawing.Size(302, 20)
    Me.lbDesagente.TabIndex = 31
    Me.lbDesagente.Tooltip = ""
    Me.lbDesagente.UseMnemonic = False
    '
    'lbAgente
    '
    Me.lbAgente.AutoSize = True
    Me.lbAgente.BackColor = System.Drawing.Color.Transparent
    Me.lbAgente.Location = New System.Drawing.Point(13, 152)
    Me.lbAgente.Name = "lbAgente"
    Me.lbAgente.NTSDbField = ""
    Me.lbAgente.Size = New System.Drawing.Size(42, 13)
    Me.lbAgente.TabIndex = 3
    Me.lbAgente.Text = "Agente"
    Me.lbAgente.Tooltip = ""
    Me.lbAgente.UseMnemonic = False
    '
    'lbDescausa
    '
    Me.lbDescausa.BackColor = System.Drawing.Color.Transparent
    Me.lbDescausa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbDescausa.Location = New System.Drawing.Point(230, 127)
    Me.lbDescausa.Name = "lbDescausa"
    Me.lbDescausa.NTSDbField = ""
    Me.lbDescausa.Size = New System.Drawing.Size(302, 20)
    Me.lbDescausa.TabIndex = 30
    Me.lbDescausa.Tooltip = ""
    Me.lbDescausa.UseMnemonic = False
    '
    'fmClifor
    '
    Me.fmClifor.AllowDrop = True
    Me.fmClifor.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.fmClifor.Appearance.Options.UseBackColor = True
    Me.fmClifor.Controls.Add(Me.opFornitori)
    Me.fmClifor.Controls.Add(Me.opClienti)
    Me.fmClifor.Cursor = System.Windows.Forms.Cursors.Hand
    Me.fmClifor.Location = New System.Drawing.Point(15, 182)
    Me.fmClifor.Name = "fmClifor"
    Me.fmClifor.ShowCaption = False
    Me.fmClifor.Size = New System.Drawing.Size(135, 49)
    Me.fmClifor.TabIndex = 29
    '
    'opFornitori
    '
    Me.opFornitori.Cursor = System.Windows.Forms.Cursors.Default
    Me.opFornitori.Location = New System.Drawing.Point(14, 25)
    Me.opFornitori.Name = "opFornitori"
    Me.opFornitori.NTSCheckValue = "S"
    Me.opFornitori.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opFornitori.Properties.Appearance.Options.UseBackColor = True
    Me.opFornitori.Properties.Caption = "Fornitori"
    Me.opFornitori.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opFornitori.Size = New System.Drawing.Size(77, 19)
    Me.opFornitori.TabIndex = 1
    '
    'opClienti
    '
    Me.opClienti.Cursor = System.Windows.Forms.Cursors.Default
    Me.opClienti.EditValue = True
    Me.opClienti.Location = New System.Drawing.Point(14, 5)
    Me.opClienti.Name = "opClienti"
    Me.opClienti.NTSCheckValue = "S"
    Me.opClienti.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.opClienti.Properties.Appearance.Options.UseBackColor = True
    Me.opClienti.Properties.Caption = "Clienti"
    Me.opClienti.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
    Me.opClienti.Size = New System.Drawing.Size(77, 19)
    Me.opClienti.TabIndex = 0
    '
    'NtsButton1
    '
    Me.NtsButton1.ImageText = ""
    Me.NtsButton1.Location = New System.Drawing.Point(170, 208)
    Me.NtsButton1.Name = "NtsButton1"
    Me.NtsButton1.Size = New System.Drawing.Size(167, 23)
    Me.NtsButton1.TabIndex = 28
    Me.NtsButton1.Text = "Seleziona articoli"
    '
    'lbAldata
    '
    Me.lbAldata.AutoSize = True
    Me.lbAldata.BackColor = System.Drawing.Color.Transparent
    Me.lbAldata.Location = New System.Drawing.Point(351, 42)
    Me.lbAldata.Name = "lbAldata"
    Me.lbAldata.NTSDbField = ""
    Me.lbAldata.Size = New System.Drawing.Size(48, 13)
    Me.lbAldata.TabIndex = 6
    Me.lbAldata.Text = "alla data"
    Me.lbAldata.Tooltip = ""
    Me.lbAldata.UseMnemonic = False
    '
    'cbTipodoc
    '
    Me.cbTipodoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipodoc.DataSource = Nothing
    Me.cbTipodoc.DisplayMember = ""
    Me.cbTipodoc.Enabled = False
    Me.cbTipodoc.Location = New System.Drawing.Point(170, 105)
    Me.cbTipodoc.Name = "cbTipodoc"
    Me.cbTipodoc.NTSDbField = ""
    Me.cbTipodoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipodoc.Properties.DropDownRows = 30
    Me.cbTipodoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipodoc.SelectedValue = ""
    Me.cbTipodoc.Size = New System.Drawing.Size(167, 20)
    Me.cbTipodoc.TabIndex = 27
    Me.cbTipodoc.ValueMember = ""
    '
    'ckTipodoc
    '
    Me.ckTipodoc.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckTipodoc.Location = New System.Drawing.Point(16, 106)
    Me.ckTipodoc.Name = "ckTipodoc"
    Me.ckTipodoc.NTSCheckValue = "S"
    Me.ckTipodoc.NTSUnCheckValue = "N"
    Me.ckTipodoc.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTipodoc.Properties.Appearance.Options.UseBackColor = True
    Me.ckTipodoc.Properties.Caption = "Tipo documento"
    Me.ckTipodoc.Size = New System.Drawing.Size(107, 19)
    Me.ckTipodoc.TabIndex = 26
    '
    'edSerie
    '
    Me.edSerie.Cursor = System.Windows.Forms.Cursors.Default
    Me.edSerie.Location = New System.Drawing.Point(432, 105)
    Me.edSerie.Name = "edSerie"
    Me.edSerie.NTSDbField = ""
    Me.edSerie.NTSForzaVisZoom = False
    Me.edSerie.NTSOldValue = ""
    Me.edSerie.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edSerie.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edSerie.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edSerie.Properties.MaxLength = 65536
    Me.edSerie.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edSerie.Size = New System.Drawing.Size(45, 20)
    Me.edSerie.TabIndex = 25
    '
    'lbDamaga
    '
    Me.lbDamaga.AutoSize = True
    Me.lbDamaga.BackColor = System.Drawing.Color.Transparent
    Me.lbDamaga.Location = New System.Drawing.Point(13, 64)
    Me.lbDamaga.Name = "lbDamaga"
    Me.lbDamaga.NTSDbField = ""
    Me.lbDamaga.Size = New System.Drawing.Size(75, 13)
    Me.lbDamaga.TabIndex = 9
    Me.lbDamaga.Text = "Dal magazzino"
    Me.lbDamaga.Tooltip = ""
    Me.lbDamaga.UseMnemonic = False
    '
    'lbAlconto
    '
    Me.lbAlconto.AutoSize = True
    Me.lbAlconto.BackColor = System.Drawing.Color.Transparent
    Me.lbAlconto.Location = New System.Drawing.Point(351, 86)
    Me.lbAlconto.Name = "lbAlconto"
    Me.lbAlconto.NTSDbField = ""
    Me.lbAlconto.Size = New System.Drawing.Size(45, 13)
    Me.lbAlconto.TabIndex = 11
    Me.lbAlconto.Text = "al conto"
    Me.lbAlconto.Tooltip = ""
    Me.lbAlconto.UseMnemonic = False
    '
    'edAgente
    '
    Me.edAgente.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAgente.EditValue = "0"
    Me.edAgente.Location = New System.Drawing.Point(170, 149)
    Me.edAgente.Name = "edAgente"
    Me.edAgente.NTSDbField = ""
    Me.edAgente.NTSFormat = "0"
    Me.edAgente.NTSForzaVisZoom = False
    Me.edAgente.NTSOldValue = ""
    Me.edAgente.Properties.Appearance.Options.UseTextOptions = True
    Me.edAgente.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAgente.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAgente.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAgente.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAgente.Properties.MaxLength = 65536
    Me.edAgente.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAgente.Size = New System.Drawing.Size(54, 20)
    Me.edAgente.TabIndex = 22
    '
    'lbAlmaga
    '
    Me.lbAlmaga.AutoSize = True
    Me.lbAlmaga.BackColor = System.Drawing.Color.Transparent
    Me.lbAlmaga.Location = New System.Drawing.Point(351, 64)
    Me.lbAlmaga.Name = "lbAlmaga"
    Me.lbAlmaga.NTSDbField = ""
    Me.lbAlmaga.Size = New System.Drawing.Size(68, 13)
    Me.lbAlmaga.TabIndex = 12
    Me.lbAlmaga.Text = "al magazzino"
    Me.lbAlmaga.Tooltip = ""
    Me.lbAlmaga.UseMnemonic = False
    '
    'edCausa
    '
    Me.edCausa.Cursor = System.Windows.Forms.Cursors.Default
    Me.edCausa.EditValue = "0"
    Me.edCausa.Location = New System.Drawing.Point(170, 127)
    Me.edCausa.Name = "edCausa"
    Me.edCausa.NTSDbField = ""
    Me.edCausa.NTSFormat = "0"
    Me.edCausa.NTSForzaVisZoom = False
    Me.edCausa.NTSOldValue = ""
    Me.edCausa.Properties.Appearance.Options.UseTextOptions = True
    Me.edCausa.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edCausa.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edCausa.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edCausa.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edCausa.Properties.MaxLength = 65536
    Me.edCausa.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edCausa.Size = New System.Drawing.Size(54, 20)
    Me.edCausa.TabIndex = 21
    '
    'edAldata
    '
    Me.edAldata.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAldata.Location = New System.Drawing.Point(432, 39)
    Me.edAldata.Name = "edAldata"
    Me.edAldata.NTSDbField = ""
    Me.edAldata.NTSForzaVisZoom = False
    Me.edAldata.NTSOldValue = ""
    Me.edAldata.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAldata.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAldata.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAldata.Properties.MaxLength = 65536
    Me.edAldata.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAldata.Size = New System.Drawing.Size(100, 20)
    Me.edAldata.TabIndex = 13
    '
    'edDadata
    '
    Me.edDadata.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDadata.Location = New System.Drawing.Point(170, 39)
    Me.edDadata.Name = "edDadata"
    Me.edDadata.NTSDbField = ""
    Me.edDadata.NTSForzaVisZoom = False
    Me.edDadata.NTSOldValue = ""
    Me.edDadata.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDadata.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDadata.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDadata.Properties.MaxLength = 65536
    Me.edDadata.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDadata.Size = New System.Drawing.Size(100, 20)
    Me.edDadata.TabIndex = 14
    '
    'edDamaga
    '
    Me.edDamaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDamaga.EditValue = "0"
    Me.edDamaga.Location = New System.Drawing.Point(170, 61)
    Me.edDamaga.Name = "edDamaga"
    Me.edDamaga.NTSDbField = ""
    Me.edDamaga.NTSFormat = "0"
    Me.edDamaga.NTSForzaVisZoom = False
    Me.edDamaga.NTSOldValue = ""
    Me.edDamaga.Properties.Appearance.Options.UseTextOptions = True
    Me.edDamaga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDamaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDamaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDamaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDamaga.Properties.MaxLength = 65536
    Me.edDamaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDamaga.Size = New System.Drawing.Size(54, 20)
    Me.edDamaga.TabIndex = 15
    '
    'edAlconto
    '
    Me.edAlconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlconto.EditValue = "999999999"
    Me.edAlconto.Location = New System.Drawing.Point(432, 83)
    Me.edAlconto.Name = "edAlconto"
    Me.edAlconto.NTSDbField = ""
    Me.edAlconto.NTSFormat = "0"
    Me.edAlconto.NTSForzaVisZoom = False
    Me.edAlconto.NTSOldValue = "999999999"
    Me.edAlconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edAlconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAlconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlconto.Size = New System.Drawing.Size(100, 20)
    Me.edAlconto.TabIndex = 18
    '
    'edAlmaga
    '
    Me.edAlmaga.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAlmaga.EditValue = "9999"
    Me.edAlmaga.Location = New System.Drawing.Point(432, 61)
    Me.edAlmaga.Name = "edAlmaga"
    Me.edAlmaga.NTSDbField = ""
    Me.edAlmaga.NTSFormat = "0"
    Me.edAlmaga.NTSForzaVisZoom = False
    Me.edAlmaga.NTSOldValue = "9999"
    Me.edAlmaga.Properties.Appearance.Options.UseTextOptions = True
    Me.edAlmaga.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAlmaga.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edAlmaga.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edAlmaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAlmaga.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAlmaga.Size = New System.Drawing.Size(54, 20)
    Me.edAlmaga.TabIndex = 16
    '
    'edDaconto
    '
    Me.edDaconto.Cursor = System.Windows.Forms.Cursors.Default
    Me.edDaconto.EditValue = "0"
    Me.edDaconto.Location = New System.Drawing.Point(170, 83)
    Me.edDaconto.Name = "edDaconto"
    Me.edDaconto.NTSDbField = ""
    Me.edDaconto.NTSFormat = "0"
    Me.edDaconto.NTSForzaVisZoom = False
    Me.edDaconto.NTSOldValue = ""
    Me.edDaconto.Properties.Appearance.Options.UseTextOptions = True
    Me.edDaconto.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edDaconto.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edDaconto.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edDaconto.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edDaconto.Properties.MaxLength = 65536
    Me.edDaconto.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edDaconto.Size = New System.Drawing.Size(100, 20)
    Me.edDaconto.TabIndex = 17
    '
    'FRMMGSCHC_F
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(549, 243)
    Me.Controls.Add(Me.ckSerie)
    Me.Controls.Add(Me.lbDadata)
    Me.Controls.Add(Me.edDadata)
    Me.Controls.Add(Me.edDaconto)
    Me.Controls.Add(Me.lbDaconto)
    Me.Controls.Add(Me.edAlmaga)
    Me.Controls.Add(Me.edAlconto)
    Me.Controls.Add(Me.lbCausa)
    Me.Controls.Add(Me.edDamaga)
    Me.Controls.Add(Me.lbDesagente)
    Me.Controls.Add(Me.lbAgente)
    Me.Controls.Add(Me.lbDescausa)
    Me.Controls.Add(Me.edAldata)
    Me.Controls.Add(Me.edCausa)
    Me.Controls.Add(Me.fmClifor)
    Me.Controls.Add(Me.lbAlmaga)
    Me.Controls.Add(Me.edAgente)
    Me.Controls.Add(Me.NtsButton1)
    Me.Controls.Add(Me.lbAlconto)
    Me.Controls.Add(Me.lbAldata)
    Me.Controls.Add(Me.cbTipodoc)
    Me.Controls.Add(Me.ckTipodoc)
    Me.Controls.Add(Me.lbDamaga)
    Me.Controls.Add(Me.edSerie)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Name = "FRMMGSCHC_F"
    Me.Text = "STAMPA/VISUALIZZAZIONE SCHEDE CLIENTI/FORNITORI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.fmClifor, System.ComponentModel.ISupportInitialize).EndInit()
    Me.fmClifor.ResumeLayout(False)
    Me.fmClifor.PerformLayout()
    CType(Me.opFornitori.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.opClienti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTipodoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTipodoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edSerie.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAgente.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edCausa.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAldata.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDadata.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDamaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAlconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAlmaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edDaconto.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaGriglia.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try

      opFornitori.NTSSetParam(oMenu, oApp.Tr(Me, 128644695983750000, "Fornitori"), "F")
      opClienti.NTSSetParam(oMenu, oApp.Tr(Me, 128644695983906250, "Clienti"), "C")
      cbTipodoc.NTSSetParam(oApp.Tr(Me, 128644695984062500, "Tipo documento"))
      ckTipodoc.NTSSetParam(oMenu, oApp.Tr(Me, 128644695984218750, "Tipo documento"), "S", "N")
      edSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128644695984375000, "Serie"), CLN__STD.SerieMaxLen)
      edAgente.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695984843750, "Codice agente"), tabcage)
      edCausa.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695985000000, "Causale"), tabcaum)
      edAlconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695985468750, "Al conto"), tabanagrac)
      edDaconto.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695985625000, "Dal conto"), tabanagrac)
      edAlmaga.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695985781250, "Al magazzino"), tabmaga)
      edDamaga.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128644695985937500, "Dal magazzino"), tabmaga)
      edDadata.NTSSetParam(oMenu, oApp.Tr(Me, 128644695986093750, "Dalla data"), False)
      edAldata.NTSSetParam(oMenu, oApp.Tr(Me, 128644695986250000, "Alla data"), False)
      ckSerie.NTSSetParam(oMenu, oApp.Tr(Me, 128860056110311263, "Serie"), "S", "N")

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

#Region "Toolbar"
  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Try
      Me.Close()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oParam As New CLE__PATB
    Dim strTipo As String
    Dim strTmp As String = ""
    Try
      If edDaconto.Focused Then
        If opClienti.Checked Then
          strTipo = "C"
        Else
          strTipo = "F"
        End If
        SetFastZoom(edDaconto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edDaconto.Text
        oParam.bVisGriglia = True
        oParam.strTipo = strTipo
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edDaconto.Text Then edDaconto.NTSTextDB = NTSZOOM.strIn
      ElseIf edAlconto.Focused Then
        If opClienti.Checked Then
          strTipo = "C"
        Else
          strTipo = "F"
        End If
        SetFastZoom(edAlconto.Text, oParam)    'abilito la gestione dello zoom veloce
        NTSZOOM.strIn = edAlconto.Text
        oParam.bVisGriglia = True
        oParam.strTipo = strTipo
        oParam.bTipoProposto = False
        NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
        If NTSZOOM.strIn <> edAlconto.Text Then edAlconto.NTSTextDB = NTSZOOM.strIn

      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        NTSCallStandardZoom()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    Try
      oMenu.ReportImposta(Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbStampaGriglia_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaGriglia.ItemClick
    Try
      StampaGriglia()
      If bDaGest = True Then
        Me.Close()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Validate/Changed"
  Public Overridable Sub edDadata_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDadata.Validated
    Try
      If NTSCDate(edDadata.Text) > NTSCDate(edAldata.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128823853829491918, "'Dalla data' deve essere precedente o uguale a 'Alla data'"))
        edDadata.Text = "01/01/" + NTSCStr(Now.Year)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAldata_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAldata.Validated
    Try
      If NTSCDate(edDadata.Text) > NTSCDate(edAldata.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644734760000000, "'Dalla data' deve essere precedente o uguale a 'Alla data'"))
        edAldata.Text = "01/01/" + NTSCStr(Now.Year)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edDamaga_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDamaga.Validated
    Try
      If NTSCInt(edDamaga.Text) > NTSCInt(edAlmaga.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128823853961524858, "'Dal magazzino' deve essere inferiore o uguale a 'Al magazzino'"))
        edDamaga.Text = "0"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAlmaga_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAlmaga.Validated
    Try
      If NTSCInt(edDamaga.Text) > NTSCInt(edAlmaga.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644735708281250, "'Dal magazzino' deve essere inferiore o uguale a 'Al magazzino'"))
        edAlmaga.Text = "9999"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edDaconto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edDaconto.Validated
    Try
      If NTSCInt(edDaconto.Text) > NTSCInt(edAlconto.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128823854044338418, "'Dal conto' deve essere inferiore o uguale a 'Al conto'"))
        edDaconto.Text = "0"
      End If

      If edDaconto.Text <> "0" Then edAlconto.Text = edDaconto.Text
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAlconto_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAlconto.Validated
    Try
      If NTSCInt(edDaconto.Text) > NTSCInt(edAlconto.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644736937968750, "'Dal conto' deve essere inferiore o uguale a 'Al conto'"))
        edAlconto.Text = "999999999"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edCausa_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edCausa.Validated
    Try
      If edCausa.Text = "0" Then
        lbDescausa.Text = ""
        Return
      End If
      If Not oCleSchc.LeggiDescr(edCausa.Text, "TABCAUM", "N", lbDescausa.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644765277812500, "La causale inserita è inesistente"))
        edCausa.Text = "0"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub edAgente_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edAgente.Validated
    Try
      If edAgente.Text = "0" Then
        lbDesagente.Text = ""
        Return
      End If
      If Not oCleSchc.LeggiDescr(edAgente.Text, "TABCAGE", "N", lbDesagente.Text) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 128644765834218750, "Il codice agente inserito è inesistente"))
        edAgente.Text = "0"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckTipodoc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckTipodoc.CheckedChanged
    Try
      If ckTipodoc.Checked Then
        cbTipodoc.Enabled = True
        cbTipodoc.Focus()
      Else
        cbTipodoc.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

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
      strOut = strOut.Replace("?", "_").Replace("*", "%")

      Return strOut

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
      Return "''"
    End Try
  End Function

  Public Overridable Sub CaricaCombo()
    Dim dttTipo As New DataTable()
    Try
      dttTipo.Columns.Add("cod", GetType(String))
      dttTipo.Columns.Add("val", GetType(String))
      dttTipo.Rows.Add(New Object() {"A", "Fatture Imm. emesse"})
      dttTipo.Rows.Add(New Object() {"B", "D.D.T. emessi"})
      dttTipo.Rows.Add(New Object() {"C", "Corrispettivi emessi"})
      dttTipo.Rows.Add(New Object() {"E", "Note di addebito emesse"})
      dttTipo.Rows.Add(New Object() {"F", "Ric. Fiscale emessa"})
      dttTipo.Rows.Add(New Object() {"I", "Riemissione Ric. Fiscali"})
      dttTipo.Rows.Add(New Object() {"J", "Note Accr. ricevute"})
      dttTipo.Rows.Add(New Object() {"L", "Fatture Imm. ricevute"})
      dttTipo.Rows.Add(New Object() {"M", "D.D.T. ricevuti"})
      dttTipo.Rows.Add(New Object() {"N", "Note Accr. emessa"})
      dttTipo.Rows.Add(New Object() {"S", "Fatt. Ric. Fisc. emessa"})
      dttTipo.Rows.Add(New Object() {"Z", "Bolle mov. interna"})
      dttTipo.AcceptChanges()

      cbTipodoc.DataSource = dttTipo
      cbTipodoc.DisplayMember = "val"
      cbTipodoc.ValueMember = "cod"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Function ComponiFormula(ByVal bTipodoc As Boolean, ByVal strTipodoc As String, _
    ByVal bClienti As Boolean, ByVal dtDadata As Date, ByVal dtAldata As Date, ByVal lDamaga As Integer, _
    ByVal lAlmaga As Integer, ByVal lDaconto As Integer, ByVal lAlconto As Integer, ByVal lDacomme As Integer, _
    ByVal lAlcomme As Integer, ByVal strSerie As String, ByVal lCausa As Integer, ByVal lAgente As Integer, _
    ByVal lAgente2 As Integer, ByVal lClifor As Integer, ByRef strWhere As String) As Boolean
    Dim strTipo As String

    Try
      '--------------------------------------------------------------------------------------------------------------
      If bClienti = True Then strTipo = "C" Else strTipo = "F"
      '--------------------------------------------------------------------------------------------------------------
      strWhere = "{KEYMAG.km_aammgg} In Date(" & dtDadata.Year & "," & dtDadata.Month.ToString("00") & "," & dtDadata.Day.ToString("00") & ")" & _
        " To Date(" & dtAldata.Year & "," & dtAldata.Month & "," & dtAldata.Day & ")" & _
        " And {KEYMAG.km_magaz} In " & lDamaga & " To " & lAlmaga & _
        " And {KEYMAG.km_conto} In " & lDaconto & " To " & lAlconto & _
        " And {KEYMAG.km_commeca} In " & lDacomme & " To " & lAlcomme & _
        " And {ANAGRA.an_tipo} = " & CStrSQL(strTipo) & _
        " And {KEYMAG.codditt} = " & CStrSQL(DittaCorrente)
      If bTipodoc = True Then strWhere &= " And {KEYMAG.km_tipork} = '" & strTipodoc & "'"
      If strSerie <> "" Then strWhere &= " And {KEYMAG.km_serie} = '" & strSerie & "'"
      If lCausa <> 0 Then strWhere &= " And {KEYMAG.km_causale} = " & lCausa
      If lAgente <> 0 Then strWhere &= " And {TESTMAG.tm_codagen} = " & lAgente
      If lAgente2 <> 0 Then strWhere &= " And {TESTMAG.tm_codagen2} = " & lAgente2
      If lClifor <> 0 Then strWhere &= " And {ANAGRA.an_categ} = " & lClifor
      '--------------------------------------------------------------------------------------------------------------
      If strWhereArtico <> "" Then oCleSchc.TraduciWhereReport(strWhereArtico, "ARTICO", strWhere)
      '--------------------------------------------------------------------------------------------------------------
      '--- Aggiunge eventuali condizioni CRM
      '--------------------------------------------------------------------------------------------------------------
      If (bModuloCrm = True) And (bIsCRMUser = True) Then
        If oCleSchc.bIs15 = False Then
          strWhere &= " And ("
          If strAccvis <> "T" Then
            strWhere &= "({anagra.an_tipo} = 'C' And {leads.le_coddest} = 0"
            Select Case strAccvis
              Case "P" : strWhere &= " And {leads.le_opinc} = " & lCodorgaOperat
              Case "C" : strWhere &= " And {leads.le_opinc} In [" & strRegvis & "]"
            End Select
            strWhere &= ")"
          Else
            strWhere &= " {anagra.an_tipo} = 'C'"
          End If
          If bAmm = False Then
            strWhere &= " And {anagra.an_tipo} <> 'F'"
          Else
            strWhere &= " Or {anagra.an_tipo} <> 'C'"
          End If
          strWhere &= ")"
        Else
          If oCleSchc.nCodcageAccdito <> 0 Then
            strWhere &= " And ({TESTMAG.tm_codagen} = " & oCleSchc.nCodcageAccdito & " Or {TESTMAG.tm_codagen2} = " & oCleSchc.nCodcageAccdito & ")"
          End If
          RiempiTmpTable()
          strWhere &= " And {TTSUBQCRM.instid} = " & oCleSchc.lIITtsubqcrm
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Sub PassaDatiBE(ByVal bTipodoc As Boolean, ByVal strTipodoc As String, ByVal bClienti As Boolean, _
                                       ByVal dtDadata As Date, ByVal dtAldata As Date, ByVal lDamaga As Integer, ByVal lAlmaga As Integer, _
                                       ByVal lDaconto As Integer, ByVal lAlconto As Integer, ByVal lDacomme As Integer, _
                                       ByVal lAlcomme As Integer, ByVal strSerie As String, ByVal lCausa As Integer, ByVal lAgente As Integer, _
                                       ByVal lAgente2 As Integer, ByVal lClifor As Integer, ByVal strWhereArtico As String)
    Try
      oCleSchc.bTipodoc = bTipodoc
      oCleSchc.strTipodoc = strTipodoc
      oCleSchc.dtDadata = dtDadata
      oCleSchc.dtAldata = dtAldata
      oCleSchc.lDamaga = lDamaga
      oCleSchc.lAlmaga = lAlmaga
      oCleSchc.lDaconto = lDaconto
      oCleSchc.lAlconto = lAlconto
      oCleSchc.lDacomme = lDacomme
      oCleSchc.lAlcomme = lAlcomme
      oCleSchc.strSerie = strSerie
      oCleSchc.lCausa = lCausa
      oCleSchc.lAgente = lAgente
      oCleSchc.lAgente2 = lAgente2
      oCleSchc.lClifor = lClifor
      oCleSchc.strWhereArtico = strWhereArtico
      oCleSchc.strWhereTestata = ""
      oCleSchc.strWhereCorpo = ""
      If bClienti Then
        oCleSchc.strTipo = "C"
      Else
        oCleSchc.strTipo = "F"
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim nPjob As Object
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim i As Integer
    Dim j As Integer
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      RiempiTmpTable()
      '--------------------------------------------------------------------------------------------------------------
      If Not ComponiFormula(ckTipodoc.Checked, cbTipodoc.SelectedValue, opClienti.Checked, NTSCDate(edDadata.Text), _
        NTSCDate(edAldata.Text), NTSCInt(edDamaga.Text), NTSCInt(edAlmaga.Text), NTSCInt(edDaconto.Text), _
        NTSCInt(edAlconto.Text), 0, 999999999, edSerie.Text, NTSCInt(edCausa.Text), NTSCInt(edAgente.Text), 0, _
        0, strCrpe) Then Return
      '--------------------------------------------------------------------------------------------------------------
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BSMGSCHC", "Reports1", " ", 0, nDestin, "BSMGSCHC.RPT", False, "Stampa/Visualizzazione schede clienti/fornitori", False)
      If nPjob Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      'lancio tutti gli eventuali reports (gestisce già il multireport)
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "DATA", "'Dal : " & edDadata.Text & " al : " & edAldata.Text & "'")
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
        For j = 3 To 12
          If Trim(CStr(CType(nPjob, Array).GetValue(j, i))) <> "" Then
            nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CStr(CType(nPjob, Array).GetValue(j, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(j + 10, i))))
          End If
        Next j
      Next
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub StampaGriglia()
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      RiempiTmpTable()
      '--------------------------------------------------------------------------------------------------------------
      PassaDatiBE(ckTipodoc.Checked, cbTipodoc.SelectedValue, opClienti.Checked, NTSCDate(edDadata.Text), _
        NTSCDate(edAldata.Text), NTSCInt(edDamaga.Text), NTSCInt(edAlmaga.Text), NTSCInt(edDaconto.Text), _
        NTSCInt(edAlconto.Text), 0, 999999999, edSerie.Text, NTSCInt(edCausa.Text), NTSCInt(edAgente.Text), 0, _
        0, strWhereArtico)
      '--------------------------------------------------------------------------------------------------------------
      Dim frmGrcf As FRMMGGRCF = Nothing
      frmGrcf = CType(NTSNewFormModal("FRMMGGRCF"), FRMMGGRCF)
      frmGrcf.Init(oMenu, Nothing, DittaCorrente)
      frmGrcf.InitEntity(oCleSchc)
      frmGrcf.ShowDialog(Me)
      frmGrcf.Dispose()
      frmGrcf = Nothing
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdSelartico_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NtsButton1.Click
    Dim oPar As CLE__PATB = New CLE__PATB
    Try
      oPar.bVisGriglia = False
      oPar.strTipoArticolo = "N"
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oPar)
      If oPar.CANCELZOOM = False Then strWhereArtico = oPar.strOut.Trim
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub ckSerie_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckSerie.CheckedChanged
    Try
      If ckSerie.Checked = True Then
        GctlSetVisEnab(edSerie, False)
        edSerie.Text = " "
        edSerie.Focus()
      Else
        edSerie.Enabled = False
        edSerie.Text = ""
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edSerie_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edSerie.Validated
    Dim strTmp As String = ""
    Try
      strTmp = TestSerieMaxLen(edSerie.Text, False)
      If strTmp <> edSerie.Text Then edSerie.Text = strTmp

      If ckSerie.Checked And edSerie.Text = "" Then edSerie.Text = " "
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Function SvuotaTmpTable() As Boolean
    Try
      If (bModuloCrm = False) Or (bIsCRMUser = False) Or (oCleSchc.bIs15 = False) Then Return True
      oMenu.ResetTblInstId("TTSUBQCRM", False, oCleSchc.lIITtsubqcrm)
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function RiempiTmpTable() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      SvuotaTmpTable()
      '--------------------------------------------------------------------------------------------------------------
      oCleSchc.RiempiTmpTable(bModuloCrm, bIsCRMUser)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
End Class