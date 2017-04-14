Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__SPAF
  Public oCleSpaf As CLE__SPAF
  Public oCallParams As CLE__CLDP

  Public bElabInCorso As Boolean = False
  Public bOnLoading As Boolean = False

  Private components As System.ComponentModel.IContainer

#Region "Moduli"
  Private Moduli_P As Integer = CLN__STD.bsModAll
  Private ModuliExt_P As Integer = CLN__STD.bsModExtAll
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

#Region "Dichiarazione Controlli"
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
  Public WithEvents tlbCambioDitta As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbStampaGriglia As NTSInformatica.NTSBarButtonItem
  Public WithEvents edData As NTSInformatica.NTSTextBoxData
  Public WithEvents lbData As NTSInformatica.NTSLabel
  Public WithEvents cbParstaf As NTSInformatica.NTSComboBox
  Public WithEvents lbParstaf As NTSInformatica.NTSLabel
  Public WithEvents lbStatus As NTSInformatica.NTSLabel
  Public WithEvents cmdNuovastampa As NTSInformatica.NTSButton
  Public WithEvents cmdRefresh As NTSInformatica.NTSButton
  Public WithEvents cmdFiltri As NTSInformatica.NTSButton
#End Region

#Region "Inizializzazione"
  Public Overloads Function Init(ByRef Menu As CLE__MENU, ByRef Param As CLE__CLDP, Optional ByVal Ditta As String = "", Optional ByRef SharedControls As CLE__EVNT = Nothing) As Boolean
    '----------------------------------------------------------------------------------------------------------------
    oMenu = Menu
    oApp = oMenu.App
    oCallParams = Param
    If Ditta <> "" Then DittaCorrente = Ditta Else DittaCorrente = oApp.Ditta
    '----------------------------------------------------------------------------------------------------------------
    Me.GctlTipoDoc = ""
    '----------------------------------------------------------------------------------------------------------------
    InitializeComponent()
    Me.MinimumSize = Me.Size
    '----------------------------------------------------------------------------------------------------------------
    '--- Creo e attivo l'entity e inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY
    '----------------------------------------------------------------------------------------------------------------
    Dim strErr As String = ""
    Dim oTmp As Object = Nothing
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__SPAF", "BE__SPAF", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222149375000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleSpaf = CType(oTmp, CLE__SPAF)
    '----------------------------------------------------------------------------------------------------------------
    bRemoting = Menu.Remoting("BN__SPAF", strRemoteServer, strRemotePort)
    AddHandler oCleSpaf.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleSpaf.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '----------------------------------------------------------------------------------------------------------------
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__SPAF))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbStampa = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem
    Me.tlbStampaGriglia = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbCambioDitta = New NTSInformatica.NTSBarMenuItem
    Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.edData = New NTSInformatica.NTSTextBoxData
    Me.lbData = New NTSInformatica.NTSLabel
    Me.cbParstaf = New NTSInformatica.NTSComboBox
    Me.lbParstaf = New NTSInformatica.NTSLabel
    Me.lbStatus = New NTSInformatica.NTSLabel
    Me.cmdNuovastampa = New NTSInformatica.NTSButton
    Me.cmdFiltri = New NTSInformatica.NTSButton
    Me.cmdRefresh = New NTSInformatica.NTSButton
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edData.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbParstaf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbCambioDitta, Me.tlbStampaGriglia})
    Me.NtsBarManager1.MaxItemId = 36
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampa), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaVideo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStampaGriglia), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbStampa.Id = 16
    Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
    Me.tlbStampa.Name = "tlbStampa"
    Me.tlbStampa.Visible = True
    '
    'tlbStampaVideo
    '
    Me.tlbStampaVideo.Caption = "Stampa video"
    Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
    Me.tlbStampaVideo.Id = 17
    Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
    Me.tlbStampaVideo.Name = "tlbStampaVideo"
    Me.tlbStampaVideo.Visible = True
    '
    'tlbStampaGriglia
    '
    Me.tlbStampaGriglia.Caption = "Stampa su griglia"
    Me.tlbStampaGriglia.Glyph = CType(resources.GetObject("tlbStampaGriglia.Glyph"), System.Drawing.Image)
    Me.tlbStampaGriglia.Id = 35
    Me.tlbStampaGriglia.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F11)
    Me.tlbStampaGriglia.Name = "tlbStampaGriglia"
    Me.tlbStampaGriglia.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.Id = 22
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCambioDitta, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante, True)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbCambioDitta
    '
    Me.tlbCambioDitta.Caption = "Cambia ditta"
    Me.tlbCambioDitta.Id = 28
    Me.tlbCambioDitta.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D))
    Me.tlbCambioDitta.Name = "tlbCambioDitta"
    Me.tlbCambioDitta.NTSIsCheckBox = False
    Me.tlbCambioDitta.Visible = True
    '
    'tlbImpostaStampante
    '
    Me.tlbImpostaStampante.Caption = "Imposta stampante"
    Me.tlbImpostaStampante.Id = 25
    Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
    Me.tlbImpostaStampante.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Id = 20
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'edData
    '
    Me.edData.Cursor = System.Windows.Forms.Cursors.Default
    Me.edData.EditValue = "01/01/1900"
    Me.edData.Location = New System.Drawing.Point(176, 64)
    Me.edData.Name = "edData"
    Me.edData.NTSDbField = ""
    Me.edData.NTSForzaVisZoom = False
    Me.edData.NTSOldValue = ""
    Me.edData.Properties.Appearance.BackColor = System.Drawing.Color.White
    Me.edData.Properties.Appearance.Options.UseBackColor = True
    Me.edData.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edData.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edData.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edData.Properties.MaxLength = 65536
    Me.edData.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edData.Size = New System.Drawing.Size(97, 20)
    Me.edData.TabIndex = 171
    '
    'lbData
    '
    Me.lbData.AutoSize = True
    Me.lbData.BackColor = System.Drawing.Color.Transparent
    Me.lbData.Location = New System.Drawing.Point(27, 67)
    Me.lbData.Name = "lbData"
    Me.lbData.NTSDbField = ""
    Me.lbData.Size = New System.Drawing.Size(79, 13)
    Me.lbData.TabIndex = 170
    Me.lbData.Text = "Data di stampa"
    Me.lbData.Tooltip = ""
    Me.lbData.UseMnemonic = False
    '
    'cbParstaf
    '
    Me.cbParstaf.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbParstaf.DataSource = Nothing
    Me.cbParstaf.DisplayMember = ""
    Me.cbParstaf.Location = New System.Drawing.Point(176, 86)
    Me.cbParstaf.Name = "cbParstaf"
    Me.cbParstaf.NTSDbField = ""
    Me.cbParstaf.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbParstaf.Properties.DropDownRows = 30
    Me.cbParstaf.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbParstaf.SelectedValue = ""
    Me.cbParstaf.Size = New System.Drawing.Size(410, 20)
    Me.cbParstaf.TabIndex = 173
    Me.cbParstaf.ValueMember = ""
    '
    'lbParstaf
    '
    Me.lbParstaf.AutoSize = True
    Me.lbParstaf.BackColor = System.Drawing.Color.Transparent
    Me.lbParstaf.Location = New System.Drawing.Point(27, 89)
    Me.lbParstaf.Name = "lbParstaf"
    Me.lbParstaf.NTSDbField = ""
    Me.lbParstaf.Size = New System.Drawing.Size(129, 13)
    Me.lbParstaf.TabIndex = 172
    Me.lbParstaf.Text = "Codice formato di stampa"
    Me.lbParstaf.Tooltip = ""
    Me.lbParstaf.UseMnemonic = False
    '
    'lbStatus
    '
    Me.lbStatus.AutoSize = True
    Me.lbStatus.BackColor = System.Drawing.Color.Transparent
    Me.lbStatus.Location = New System.Drawing.Point(12, 164)
    Me.lbStatus.Name = "lbStatus"
    Me.lbStatus.NTSDbField = ""
    Me.lbStatus.Size = New System.Drawing.Size(43, 13)
    Me.lbStatus.TabIndex = 174
    Me.lbStatus.Text = "Pronto."
    Me.lbStatus.Tooltip = ""
    Me.lbStatus.UseMnemonic = False
    '
    'cmdNuovastampa
    '
    Me.cmdNuovastampa.ImageText = ""
    Me.cmdNuovastampa.Location = New System.Drawing.Point(416, 121)
    Me.cmdNuovastampa.Name = "cmdNuovastampa"
    Me.cmdNuovastampa.Size = New System.Drawing.Size(170, 24)
    Me.cmdNuovastampa.TabIndex = 176
    Me.cmdNuovastampa.Text = "Nuova stampa"
    '
    'cmdFiltri
    '
    Me.cmdFiltri.ImageText = ""
    Me.cmdFiltri.Location = New System.Drawing.Point(240, 121)
    Me.cmdFiltri.Name = "cmdFiltri"
    Me.cmdFiltri.Size = New System.Drawing.Size(170, 24)
    Me.cmdFiltri.TabIndex = 175
    Me.cmdFiltri.Text = "Reimposta filtri"
    '
    'cmdRefresh
    '
    Me.cmdRefresh.Image = CType(resources.GetObject("cmdRefresh.Image"), System.Drawing.Image)
    Me.cmdRefresh.ImageText = ""
    Me.cmdRefresh.Location = New System.Drawing.Point(587, 85)
    Me.cmdRefresh.Name = "cmdRefresh"
    Me.cmdRefresh.Size = New System.Drawing.Size(21, 22)
    Me.cmdRefresh.TabIndex = 177
    Me.cmdRefresh.ToolTip = "Ricarica lista"
    '
    'FRM__SPAF
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(642, 186)
    Me.Controls.Add(Me.cmdRefresh)
    Me.Controls.Add(Me.cmdNuovastampa)
    Me.Controls.Add(Me.cmdFiltri)
    Me.Controls.Add(Me.lbStatus)
    Me.Controls.Add(Me.cbParstaf)
    Me.Controls.Add(Me.lbParstaf)
    Me.Controls.Add(Me.edData)
    Me.Controls.Add(Me.lbData)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRM__SPAF"
    Me.Text = "STAMPE PARAMETRICHE PREDEFINITE"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edData.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbParstaf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, False)
    '----------------------------------------------------------------------------------------------------------------
    Dim i As Integer = 0

    Try
      '--------------------------------------------------------------------------------------------------------------
      Try
        tlbStampa.GlyphPath = (oApp.ChildImageDir & "\print.gif")
        tlbStampaVideo.GlyphPath = (oApp.ChildImageDir & "\prnscreen.gif")
        tlbStampaGriglia.GlyphPath = (oApp.ChildImageDir & "\prngrid.gif")
        tlbStrumenti.GlyphPath = (oApp.ChildImageDir & "\options.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
        cmdRefresh.Image = Bitmap.FromFile(oApp.ChildImageDir & "\mnuDockPassaa.gif")
      Catch ex As Exception
      End Try
      '--------------------------------------------------------------------------------------------------------------
      tlbMain.NTSSetToolTip()
      '--------------------------------------------------------------------------------------------------------------
      edData.NTSSetParam(oMenu, oApp.Tr(Me, 129718766666845095, "Data di stampa"), False)
      cbParstaf.NTSSetParam(oApp.Tr(Me, 129718765882869679, "Codice formato di stampa"))
      '--------------------------------------------------------------------------------------------------------------
      NTSScriptExec("InitControls", Me, Nothing)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
    '----------------------------------------------------------------------------------------------------------------
    InitControlsBeginEndInit(Me, True)
    '----------------------------------------------------------------------------------------------------------------
  End Sub
#End Region

#Region "Eventi di Form"
  Public Overridable Sub FRM__SPAF_ActivatedFirst(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.ActivatedFirst
    Try
      '--------------------------------------------------------------------------------------------------------------
      LeggiDatiDitta()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub FRM__SPAF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '--------------------------------------------------------------------------------------------------------------
      bOnLoading = True
      '--------------------------------------------------------------------------------------------------------------
      InitControls()
      '--------------------------------------------------------------------------------------------------------------
      oCleSpaf.bGestAnaext = CBool(oMenu.GetSettingBus("BS--CLIE", "OPZIONI", ".", "GestAnaExt", "0", " ", "0"))
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla se esiste il modulo delle anagrafiche generali
      '--------------------------------------------------------------------------------------------------------------
      If CBool(oMenu.ModuliExtDittaDitt(DittaCorrente) And bsModExtANG) Then
        oCleSpaf.bModuloANG = True
      Else
        oCleSpaf.bModuloANG = False
        tlbCambioDitta.Enabled = False
      End If
      '--------------------------------------------------------------------------------------------------------------
      CaricaCbParstaf()
      '--------------------------------------------------------------------------------------------------------------
      '--- Variabile di registro per creazione tabella ZZDCST
      '--------------------------------------------------------------------------------------------------------------
      oCleSpaf.bAggzzdcst = CBool(oMenu.GetSettingBus("BS--SPAF", "OPZIONI", ".", "Aggzzdcst", "0", " ", "0"))
      '--------------------------------------------------------------------------------------------------------------
      oCleSpaf.nDecSuPrzUn = NTSCInt(oMenu.GetSettingBus("OPZIONI", ".", ".", "nDecSuPrzUn", "2", " ", "2"))
      '--------------------------------------------------------------------------------------------------------------
      oCleSpaf.lIITTstaf = oMenu.GetTblInstId("TTSTAF", False)
      oCleSpaf.lIITTArtpro = oMenu.GetTblInstId("TTARTPRO", False)
      oCleSpaf.lIITTArtprox = oMenu.GetTblInstId("TTARTPROX", False)
      '--------------------------------------------------------------------------------------------------------------
      edData.Text = Now.ToShortDateString
      '--------------------------------------------------------------------------------------------------------------
      GctlSetRoules()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      bOnLoading = True = False
    End Try
  End Sub

  Public Overridable Sub FRM__SPAF_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bElabInCorso = True Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129719528293692705, "Attenzione!" & vbCrLf & _
          "Elaborazione in corso." & vbCrLf & _
          "Confermare, comunque, l'uscita dal programma?")) = Windows.Forms.DialogResult.No Then e.Cancel = True
      End If
      '--------------------------------------------------------------------------------------------------------------
      SvuotaTmpTable()
      '--------------------------------------------------------------------------------------------------------------
      oCleSpaf.dttFiltri.Clear() : oCleSpaf.dttFiltri.Dispose()
      oCleSpaf.dttFiltriX.Clear() : oCleSpaf.dttFiltriX.Dispose()
      If Not oCleSpaf.dttDatiGriglia Is Nothing Then
        oCleSpaf.dttDatiGriglia.Clear()
        oCleSpaf.dttDatiGriglia.Dispose()
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi ToolBar"
  Public Overridable Sub tlbCambioDitta_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCambioDitta.ItemClick
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bElabInCorso = True Then Return
      '--------------------------------------------------------------------------------------------------------------
      LeggiDatiDitta()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbStampa_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampa.ItemClick
    Try
      Stampa(1)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbStampaVideo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaVideo.ItemClick
    Try
      Stampa(0)
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbStampaGriglia_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbStampaGriglia.ItemClick
    Try
      StampaSuGriglia()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
    oMenu.ReportImposta(Me)
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

#Region "Eventi ComboBox"
  Public Overridable Sub cbParstaf_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbParstaf.SelectedIndexChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If bOnLoading = False Then
        oCleSpaf.dttFiltri.Clear()
        oCleSpaf.dttFiltriX.Clear()
      End If
      '--------------------------------------------------------------------------------------------------------------
      '--- Al cambio della stampa selezionata, rilegge i filtri memorizzati
      '--------------------------------------------------------------------------------------------------------------
      oCleSpaf.lCodform = NTSCInt(cbParstaf.SelectedValue)
      oCleSpaf.ApriDataTableFiltri()
      oCleSpaf.ApriDataTableFiltriX()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

#Region "Eventi Pulsanti"
  Public Overridable Sub cmdFiltri_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFiltri.Click
    Dim dttParstaf As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleSpaf.ApriParstaf(dttParstaf) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      Filtri_ANAGRA(dttParstaf)
      Filtri_ARTICO(dttParstaf)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      dttParstaf.Clear()
      dttParstaf.Dispose()
    End Try
  End Sub

  Public Overridable Sub cmdNuovastampa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNuovastampa.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      oMenu.RunChild("BS__ISTF", "CLS__ISTF", oApp.Tr(Me, 129718959652100160, "IMPOSTAZIONE STAMPE PARAMETRICHE PREDEFINITE"), DittaCorrente, "", "", Nothing, "", True, True)
      '--------------------------------------------------------------------------------------------------------------
      CaricaCbParstaf()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      CaricaCbParstaf()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
#End Region

  Public Overridable Sub CaricaCbParstaf()
    Dim bResult As Boolean = False
    Dim dttTmp As New DataTable
    Dim dttParstaf As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------      
      bResult = oCleSpaf.CaricaCbParstaf(dttTmp)
      dttParstaf.Columns.Add("cod", GetType(Integer))
      dttParstaf.Columns.Add("val", GetType(String))
      If bResult = False Then
        dttParstaf.Rows.Add(New Object() {0, ""})
      Else
        For i As Integer = 0 To (dttTmp.Rows.Count - 1)
          dttParstaf.Rows.Add(New Object() {NTSCInt(dttTmp.Rows(i)!pf_codform), NTSCStr(dttTmp.Rows(i)!Titolo)})
        Next
      End If
      dttParstaf.AcceptChanges()
      cbParstaf.DataSource = dttParstaf
      cbParstaf.ValueMember = "cod"
      cbParstaf.DisplayMember = "val"
      '--------------------------------------------------------------------------------------------------------------
      cbParstaf.SelectedIndex = 0
      '--------------------------------------------------------------------------------------------------------------
      oCleSpaf.lCodform = NTSCInt(NTSCInt(cbParstaf.SelectedValue))
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Function CheckSelection(ByVal bStampaSuGriglia As Boolean) As Boolean
    Dim nEscomp As Integer = 0
    Dim dttParstaf As New DataTable
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.ValidaLastControl()
      '--------------------------------------------------------------------------------------------------------------
      If cbParstaf Is Nothing Then Return False
      If cbParstaf.SelectedValue.Trim = "" Then Return False
      If NTSCInt(cbParstaf.SelectedValue) = 0 Then Return False
      '--------------------------------------------------------------------------------------------------------------
      If oCleSpaf.ApriParstaf(dttParstaf) = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129719645399595718, "Attenzione!" & vbCrLf & _
          "Formato di stampa inesistente." & vbCrLf & _
          "Impossibile continuare."))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(dttParstaf.Rows(0)!pf_codquery)
        Case 3
          If bStampaSuGriglia = True Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 129726707268436190, "Attenzione!" & vbCrLf & _
              "Non è possibile visualizzare questo formato di stampa su griglia."))
            Return False
          End If
        Case 14, 15
          If oMenu.ValCodiceDb(DittaCorrente, DittaCorrente, "TABANAZ", "S", "", dttTmp) = False Then
            oApp.MsgBoxInfo(oApp.Tr(Me, 129719647114515253, "Attenzione!" & vbCrLf & _
              "Ditta '|" & DittaCorrente & "|' inesistente." & vbCrLf & _
              "Impossibile continuare."))
            Return False
          Else
            nEscomp = NTSCInt(dttTmp.Rows(0)!tb_escomp)
          End If
          '----------------------------------------------------------------------------------------------------------
          dttTmp.Clear()
          dttTmp.Dispose()
          '----------------------------------------------------------------------------------------------------------
          If oMenu.ValCodiceDb(NTSCStr(nEscomp), DittaCorrente, "TABESCO", "N", "", dttTmp) = False Then
            If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 129719648395136413, "Attenzione!" & vbCrLf & _
              "Esercizio di competenza inesistente tra gli 'Esercizi contabili'" & vbCrLf & _
              "relativi alla ditta '|" & DittaCorrente & "|'." & vbCrLf & _
              "Proseguire ugialmente?")) = Windows.Forms.DialogResult.No Then
              Return False
            Else
              oCleSpaf.strDtineser = IntSetDate("01/01/" & NTSCDate(Now).Year.ToString)
              oCleSpaf.strDtfieser = IntSetDate("31/12/" & NTSCDate(Now).Year.ToString)
            End If
          Else
            oCleSpaf.strDtineser = NTSCDate(dttTmp.Rows(0)!tb_dtineser).ToShortDateString
            oCleSpaf.strDtfieser = NTSCDate(dttTmp.Rows(0)!tb_dtfieser).ToShortDateString
          End If
          '----------------------------------------------------------------------------------------------------------
      End Select
      '--------------------------------------------------------------------------------------------------------------
      If oCleSpaf.LunghezzaCampiSuperioriA255 = True Then
        If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130554122648014260, "Attenzione!" & vbCrLf & _
          "La lunghezza dei campi, per questo formato di stampa, supera i 255 caratteri." & vbCrLf & _
          "Proseguendo, i dati saranno troncati." & vbCrLf & _
          "Confermare?")) = Windows.Forms.DialogResult.No Then
          oApp.MsgBoxInfo(oApp.Tr(Me, 130554124486549405, "Stampa annullata."))
          Return False
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return False
    Finally
      dttTmp.Clear() : dttTmp.Dispose()
      dttParstaf.Clear() : dttParstaf.Dispose()
    End Try
  End Function

  Public Overridable Sub Filtri_ANAGRA(ByRef dttParstaf As DataTable)
    Dim i As Integer = 0
    Dim oParam As New CLE__PATB
    Dim dttTmp As New DataTable
    Dim frmAna2 As FRM__ANA2 = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(dttParstaf.Rows(0)!pf_codquery)
        Case 1, 2, 3
        Case Else : Return
      End Select
      '--------------------------------------------------------------------------------------------------------------
      oParam.bTipoProposto = False
      oParam.bVisGriglia = False
      oParam.strAlfpar = "BN__ISTF"
      oParam.rFiltriAnagra = dttParstaf.Rows(0)
      '--------------------------------------------------------------------------------------------------------------
      oParam.strCin = ""
      If oCleSpaf.ApriParstafx(dttTmp) = True Then
        For i = 2 To (dttTmp.Columns.Count - 1)
          oParam.strCin += NTSCStr(dttTmp.Rows(0)(dttTmp.Columns(i).ColumnName)) & "§"
        Next
      End If
      '--------------------------------------------------------------------------------------------------------------
      NTSZOOM.strIn = ""
      oParam.strTipo = "C"
      NTSZOOM.ZoomStrIn("ZOOMANAGRA", DittaCorrente, oParam)
      '--------------------------------------------------------------------------------------------------------------
      If NTSZOOM.strIn = "" Then GoTo Salta
      '--------------------------------------------------------------------------------------------------------------
      Try
        dttTmp = CType(oParam.oParam, DataTable)
        If Not dttTmp Is Nothing Then
          If dttTmp.Rows.Count <> 0 Then oCleSpaf.dttFiltriX = dttTmp
        End If
      Catch ex As Exception
      End Try
      '--------------------------------------------------------------------------------------------------------------
      If Not oParam.rFiltriAnagra Is Nothing Then
        With oCleSpaf.dttFiltri.Rows(0)
          !pf_1tipo = oParam.rFiltriAnagra!pf_1tipo
          !pf_contoini = oParam.rFiltriAnagra!pf_contoini
          !pf_contofin = oParam.rFiltriAnagra!pf_contofin
          !pf_capini = oParam.rFiltriAnagra!pf_capini
          !pf_capfin = oParam.rFiltriAnagra!pf_capfin
          !pf_zonaini = oParam.rFiltriAnagra!pf_zonaini
          !pf_zonafin = oParam.rFiltriAnagra!pf_zonafin
          !pf_categini = oParam.rFiltriAnagra!pf_categini
          !pf_categfin = oParam.rFiltriAnagra!pf_categfin
          !pf_agenteini = oParam.rFiltriAnagra!pf_agenteini
          !pf_agentefin = oParam.rFiltriAnagra!pf_agentefin
          !pf_codpagini = oParam.rFiltriAnagra!pf_codpagini
          !pf_codpagfin = oParam.rFiltriAnagra!pf_codpagfin
          !pf_dtaperini = oParam.rFiltriAnagra!pf_dtaperini
          !pf_dtaperfin = oParam.rFiltriAnagra!pf_dtaperfin
          !pf_ultaggini = oParam.rFiltriAnagra!pf_ultaggini
          !pf_ultaggfin = oParam.rFiltriAnagra!pf_ultaggfin
          !pf_testiva = oParam.rFiltriAnagra!pf_testiva
          !pf_codcand = oParam.rFiltriAnagra!pf_codcand
          !pf_codcana = oParam.rFiltriAnagra!pf_codcana
          !pf_listd = oParam.rFiltriAnagra!pf_listd
          !pf_lista = oParam.rFiltriAnagra!pf_lista
          !pf_provvd = oParam.rFiltriAnagra!pf_provvd
          !pf_provva = oParam.rFiltriAnagra!pf_provva
          !pf_scontid = oParam.rFiltriAnagra!pf_scontid
          !pf_scontia = oParam.rFiltriAnagra!pf_scontia
          !pf_codesed = oParam.rFiltriAnagra!pf_codesed
          !pf_codesea = oParam.rFiltriAnagra!pf_codesea
          !pf_lingd = oParam.rFiltriAnagra!pf_lingd
          !pf_linga = oParam.rFiltriAnagra!pf_linga
          !pf_vald = oParam.rFiltriAnagra!pf_vald
          !pf_vala = oParam.rFiltriAnagra!pf_vala
          !pf_ragd = oParam.rFiltriAnagra!pf_ragd
          !pf_stato = oParam.rFiltriAnagra!pf_stato
          !pf_blocco = oParam.rFiltriAnagra!pf_blocco
          !pf_privacy = oParam.rFiltriAnagra!pf_privacy
          !pf_status = oParam.rFiltriAnagra!pf_status
          !pf_perfatt = oParam.rFiltriAnagra!pf_perfatt
          !pf_pcons = oParam.rFiltriAnagra!pf_pcons
          !pf_provini = oParam.rFiltriAnagra!pf_provini
          !pf_provfin = oParam.rFiltriAnagra!pf_provfin
        End With
        oCleSpaf.dttFiltri.AcceptChanges()
      End If
      '--------------------------------------------------------------------------------------------------------------
Salta:
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(dttParstaf.Rows(0)!pf_codquery) <> 1 Then
        Dim oParam1 As New CLE__CLDP
        frmAna2 = CType(NTSNewFormModal("FRM__ANA2"), FRM__ANA2)
        frmAna2.Init(oMenu, oParam1, DittaCorrente)
        frmAna2.InitEntity(oCleSpaf)
        frmAna2.nEscomp = NTSCInt(dttParstaf.Rows(0)!pf_escomp)
        frmAna2.nSaldo = NTSCInt(dttParstaf.Rows(0)!pf_saldo)
        frmAna2.nMovimentato = NTSCInt(dttParstaf.Rows(0)!pf_movimentato)
        frmAna2.ShowDialog()
        If frmAna2.bAnnullato = False Then
          With oCleSpaf.dttFiltri.Rows(0)
            !pf_escomp = frmAna2.nEscomp
            !pf_saldo = frmAna2.nSaldo
            !pf_movimentato = frmAna2.nMovimentato
          End With
          oCleSpaf.dttFiltri.AcceptChanges()
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmAna2 Is Nothing Then frmAna2.Dispose()
      frmAna2 = Nothing
    End Try
  End Sub
  Public Overridable Sub Filtri_ARTICO(ByRef dttParstaf As DataTable)
    Dim i As Integer = 0
    Dim oParam As New CLE__PATB
    Dim dttTmp As New DataTable
    Dim frmArt2 As FRM__ART2 = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Select Case NTSCInt(dttParstaf.Rows(0)!pf_codquery)
        Case 1, 2, 3, 4 : Return
      End Select
      '--------------------------------------------------------------------------------------------------------------
      oParam.bVisGriglia = False
      oParam.strAlfpar = "BN__ISTF"
      oParam.strTipoArticolo = "N"
      oParam.rFiltriArtico = dttParstaf.Rows(0)
      '--------------------------------------------------------------------------------------------------------------
      NTSZOOM.strIn = ""
      NTSZOOM.ZoomStrIn("ZOOMARTICO", DittaCorrente, oParam)
      '--------------------------------------------------------------------------------------------------------------
      If NTSZOOM.strIn = "" Then GoTo Salta
      '--------------------------------------------------------------------------------------------------------------
      If Not oParam.rFiltriArtico Is Nothing Then
        With oCleSpaf.dttFiltri.Rows(0)
          !pf_stalist = oParam.rFiltriArtico!pf_stalist
          !pf_2tipo = oParam.rFiltriArtico!pf_2tipo
          !pf_codartini = oParam.rFiltriArtico!pf_codartini
          !pf_codartfin = oParam.rFiltriArtico!pf_codartfin
          !pf_codaltini = oParam.rFiltriArtico!pf_codaltini
          !pf_codaltfin = oParam.rFiltriArtico!pf_codaltfin
          !pf_gruppoini = oParam.rFiltriArtico!pf_gruppoini
          !pf_gruppofin = oParam.rFiltriArtico!pf_gruppofin
          !pf_fornini = oParam.rFiltriArtico!pf_fornini
          !pf_fornfin = oParam.rFiltriArtico!pf_fornfin
          !pf_famprodini = oParam.rFiltriArtico!pf_famprodini
          !pf_famprodfin = oParam.rFiltriArtico!pf_famprodfin

          !pf_codcla1 = oParam.rFiltriArtico!pf_codcla1
          !pf_codcla2 = oParam.rFiltriArtico!pf_codcla2
          !pf_codcla3 = oParam.rFiltriArtico!pf_codcla3
          !pf_codcla4 = oParam.rFiltriArtico!pf_codcla4
          !pf_codcla5 = oParam.rFiltriArtico!pf_codcla5

          !pf_filtro = oParam.rFiltriArtico!pf_filtro
          !pf_sottd = oParam.rFiltriArtico!pf_sottd
          !pf_sotta = oParam.rFiltriArtico!pf_sotta
          !pf_descrd = oParam.rFiltriArtico!pf_descrd
          !pf_descra = oParam.rFiltriArtico!pf_descra
          !pf_marcad = oParam.rFiltriArtico!pf_marcad
          !pf_marcaa = oParam.rFiltriArtico!pf_marcaa
          !pf_claprovd = oParam.rFiltriArtico!pf_claprovd
          !pf_claprova = oParam.rFiltriArtico!pf_claprova
          !pf_clascond = oParam.rFiltriArtico!pf_clascond
          !pf_clascona = oParam.rFiltriArtico!pf_clascona
          !pf_filtrodb = oParam.rFiltriArtico!pf_filtrodb
          !pf_filtroubic = oParam.rFiltriArtico!pf_filtroubic
          !pf_dataultagd = oParam.rFiltriArtico!pf_dataultagd
          !pf_dataultaga = oParam.rFiltriArtico!pf_dataultaga
          !pf_approvda = oParam.rFiltriArtico!pf_approvda
          !pf_approva = oParam.rFiltriArtico!pf_approva
          !pf_codivad = oParam.rFiltriArtico!pf_codivad
          !pf_codivaa = oParam.rFiltriArtico!pf_codivaa
          !pf_lotti = oParam.rFiltriArtico!pf_lotti
          !pf_commessa = oParam.rFiltriArtico!pf_commessa
          !pf_esaurito = oParam.rFiltriArtico!pf_esaurito
          !pf_varianti = oParam.rFiltriArtico!pf_varianti
          !pf_matricole = oParam.rFiltriArtico!pf_matricole
          !pf_codtipad = oParam.rFiltriArtico!pf_codtipad
          !pf_codtipaa = oParam.rFiltriArtico!pf_codtipaa
          !pf_magstockini = oParam.rFiltriArtico!pf_magstockini
          !pf_magstockfin = oParam.rFiltriArtico!pf_magstockfin
          !pf_magprodini = oParam.rFiltriArtico!pf_magprodini
          !pf_magprodfin = oParam.rFiltriArtico!pf_magprodfin
          !pf_gesubic = oParam.rFiltriArtico!pf_gesubic
          !pf_gesfasi = oParam.rFiltriArtico!pf_gesfasi
          !pf_critico = oParam.rFiltriArtico!pf_critico
          !pf_annod = oParam.rFiltriArtico!pf_annod
          !pf_annoa = oParam.rFiltriArtico!pf_annoa
          !pf_codstagd = oParam.rFiltriArtico!pf_codstagd
          !pf_codstaga = oParam.rFiltriArtico!pf_codstaga
          !pf_codtagld = oParam.rFiltriArtico!pf_codtagld
          !pf_codtagla = oParam.rFiltriArtico!pf_codtagla
        End With
        oCleSpaf.dttFiltri.AcceptChanges()
      End If
      '--------------------------------------------------------------------------------------------------------------
Salta:
      '--------------------------------------------------------------------------------------------------------------
      If NTSCInt(dttParstaf.Rows(0)!pf_codquery) > 11 Then
        Dim oParam1 As New CLE__CLDP
        frmArt2 = CType(NTSNewFormModal("FRM__ART2"), FRM__ART2)
        frmArt2.Init(oMenu, oParam1, DittaCorrente)
        frmArt2.InitEntity(oCleSpaf)
        With dttParstaf.Rows(0)
          frmArt2.nScortaminima = NTSCInt(!pf_scorta)
          frmArt2.nEsistenza = NTSCInt(!pf_esistenza)
          frmArt2.nUltimavendita = NTSCInt(!pf_ultvendita)
          frmArt2.strData = NTSCStr(!pf_dtultvendita)
          frmArt2.nCodvalu = NTSCInt(!pf_codvalu)
          frmArt2.nMagazini = NTSCInt(!pf_magazini)
          frmArt2.nMagazfin = NTSCInt(!pf_magazfin)
          frmArt2.nTipolistino = NTSCInt(!pf_tipolistino)
          frmArt2.nListino = NTSCInt(!pf_listino)
          frmArt2.lContoini = NTSCInt(!pf_listconini)
          frmArt2.lContofin = NTSCInt(!pf_listconfin)
          frmArt2.bCodtpro = NTSCInt(!pf_promozione)
          frmArt2.nCodtpro = NTSCInt(!pf_codtpro)
          frmArt2.bPrimoLiv = NTSCInt(!pf_primoliv)
        End With
        frmArt2.nTipoStampa = NTSCInt(dttParstaf.Rows(0)!pf_codquery)
        frmArt2.ShowDialog()
        If frmArt2.bAnnullato = False Then
          With oCleSpaf.dttFiltri.Rows(0)
            !pf_scorta = frmArt2.nScortaminima
            !pf_esistenza = frmArt2.nEsistenza
            !pf_ultvendita = frmArt2.nUltimavendita
            If frmArt2.strData.Trim <> "" Then
              !pf_dtultvendita = frmArt2.strData
            Else
              !pf_dtultvendita = DBNull.Value
            End If
            !pf_codvalu = frmArt2.nCodvalu
            !pf_magazini = frmArt2.nMagazini
            !pf_magazfin = frmArt2.nMagazfin
            !pf_tipolistino = frmArt2.nTipolistino
            !pf_listino = frmArt2.nListino
            !pf_listconini = frmArt2.lContoini
            !pf_listconfin = frmArt2.lContofin
            !pf_promozione = frmArt2.bCodtpro
            !pf_codtpro = frmArt2.nCodtpro
            !pf_primoliv = frmArt2.bPrimoLiv
          End With
          oCleSpaf.dttFiltri.AcceptChanges()
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      If Not frmArt2 Is Nothing Then frmArt2.Dispose()
      frmArt2 = Nothing
    End Try
  End Sub

  Overloads Overrides Sub GestisciEventiEntity(ByVal sender As Object, ByRef e As NTSEventArgs)
    Dim strTmp As String = ""

    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not IsMyThrowRemoteEvent() Then Return
      '--------------------------------------------------------------------------------------------------------------
      MyBase.GestisciEventiEntity(sender, e)
      '--------------------------------------------------------------------------------------------------------------
      If e.TipoEvento.Trim.Length < 10 Then Return
      '--------------------------------------------------------------------------------------------------------------
      Select Case e.TipoEvento.Substring(0, 10)
        Case "RIEMPITMP:" : strTmp = "Riempimento tabelle temporanee in corso..."
        Case "CALCOLOST:" : strTmp = "Calcolo dati statistici/sintetici in corso..."
        Case "RIGATOTAL:" : strTmp = "Aggiunta riga totali in corso..."
      End Select
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = strTmp
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub LeggiDatiDitta()
    Dim bAnagen As Boolean = False
    Dim bDbMultiDitta As Boolean = False

    Try
      '--------------------------------------------------------------------------------------------------------------
      DittaCorrente = oMenu.CambioDitta(oCallParams, DittaCorrente, "BN__SPAR", "", Moduli, ModuliExt, ModuliSup, ModuliSupExt, ModuliPtn, ModuliPtnExt, bAnagen, bDbMultiDitta)
      '--------------------------------------------------------------------------------------------------------------
      If DittaCorrente = "" Then
        Me.Close()
        Return
      End If
      '--------------------------------------------------------------------------------------------------------------
      If (bAnagen = False) Or (bDbMultiDitta = False) Then
        tlbCambioDitta.Enabled = False
        tlbCambioDitta.Visible = False
      Else
        GctlSetVisEnab(tlbCambioDitta, True)
      End If
      '--------------------------------------------------------------------------------------------------------------
      Me.Text = oMenu.SetCaptionDitt(DittaCorrente, Me.Text)
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub Stampa(ByVal nDestin As Integer)
    Dim i As Integer
    Dim nRis As Integer = 0
    Dim strCrpe As String = ""
    Dim strKey As String = ""
    Dim strTitle As String = ""
    Dim strReportHeader As String = ""
    Dim nPjob As Object
    Dim dttTmp As New DataTable

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      lbStatus.Text = oApp.Tr(Me, 129726705210173562, "Estrazione dati in corso...")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      bElabInCorso = True
      '--------------------------------------------------------------------------------------------------------------
      If StampaDati(False) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      bElabInCorso = False
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = "Stampa report " & IIf(nDestin = 0, "a video", "su stampante").ToString & " in corso..."
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      strKey = "Reports" & NTSCStr(cbParstaf.SelectedValue)
      strCrpe = "{TTSTAF.codditt} = " & ConvStrRpt(DittaCorrente) & _
        " And {TTSTAF.instid} = " & oCleSpaf.lIITTstaf
      If oCleSpaf.ApriParstaf(dttTmp) = True Then
        strTitle = NTSCStr(dttTmp.Rows(0)!pf_titstam)
        strReportHeader = "--  " & strTitle & "  --"
      End If
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
      nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--SPAF", strKey, " ", 0, nDestin, "BS--SPAF.RPT", False, strTitle, False)
      If nPjob Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      For i = 1 To UBound(CType(nPjob, Array), 2)
        nRis = oMenu.PESetSelectionFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), CrpeResolveFormula(Me, CStr(CType(nPjob, Array).GetValue(2, i)), strCrpe))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "REPORTHEADER", ConvStrRpt(strReportHeader))
        nRis = oMenu.PESetFormula(NTSCInt(CType(nPjob, Array).GetValue(0, i)), "INTESTAZIONECAMPI", ConvStrRpt(oCleSpaf.strIntestazioneCampi))
        nRis = oMenu.ReportPEVai(NTSCInt(CType(nPjob, Array).GetValue(0, i)))
      Next
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 129721440445239765, "Pronto.")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      '--------------------------------------------------------------------------------------------------------------
      bElabInCorso = False
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 129726705320851640, "Pronto.")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      dttTmp.Clear()
      dttTmp.Dispose()
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function StampaDati(ByVal bStampaSuGriglia As Boolean) As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      '--- Preleva il codice formato di stampa dal ComboBox
      '--------------------------------------------------------------------------------------------------------------
      oCleSpaf.lCodform = NTSCInt(cbParstaf.SelectedValue)
      '--------------------------------------------------------------------------------------------------------------
      '--- Passa la data di elaborazione all'Entity
      '--------------------------------------------------------------------------------------------------------------
      oCleSpaf.strData = edData.Text
      '--------------------------------------------------------------------------------------------------------------
      '--- Controlla la validità dei dati necessari per l'elaborazione/stampa
      '--------------------------------------------------------------------------------------------------------------
      If CheckSelection(bStampaSuGriglia) = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = "Elaborazione dati in corso..."
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      If tlbCambioDitta.Visible = True Then tlbCambioDitta.Enabled = False
      '--------------------------------------------------------------------------------------------------------------
      bElabInCorso = True
      '--------------------------------------------------------------------------------------------------------------
      If SvuotaTmpTable() = False Then Return False
      '--------------------------------------------------------------------------------------------------------------
      If bStampaSuGriglia = True Then
        lbStatus.Text = "Selezione dati da stampare in corso..."
      Else
        lbStatus.Text = "Svuotamento tabelle temporanee in corso..."
      End If
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      If oCleSpaf.StampaDati(bStampaSuGriglia) = False Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129721392059273202, "Attenzione!" & vbCrLf & _
          "Non esistono dati da stampare con queste caratteristiche."))
        Return False
      End If
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return False
    Finally
      bElabInCorso = False
      If tlbCambioDitta.Visible = True Then GctlSetVisEnab(tlbCambioDitta, False)
      lbStatus.Text = "Pronto."
      Me.Refresh()
    End Try
  End Function

  Public Overridable Sub StampaSuGriglia()
    Dim oParam As New CLE__CLDP
    Dim frmGrsf As FRM__GRSF = Nothing

    Try
      '--------------------------------------------------------------------------------------------------------------
      Me.Cursor = Cursors.WaitCursor
      lbStatus.Text = oApp.Tr(Me, 129726705231815018, "Estrazione dati in corso...")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      bElabInCorso = True
      '--------------------------------------------------------------------------------------------------------------
      If StampaDati(True) = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      bElabInCorso = False
      '--------------------------------------------------------------------------------------------------------------
      lbStatus.Text = "Stampa report su griglia in corso..."
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      frmGrsf = CType(NTSNewFormModal("FRM__GRSF"), FRM__GRSF)
      frmGrsf.Init(oMenu, oParam, DittaCorrente)
      frmGrsf.InitEntity(oCleSpaf)
      frmGrsf.ShowDialog()
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      '--------------------------------------------------------------------------------------------------------------
      bElabInCorso = False
      Me.Cursor = Cursors.Default
      lbStatus.Text = oApp.Tr(Me, 129726705305352998, "Pronto.")
      Me.Refresh()
      '--------------------------------------------------------------------------------------------------------------
      If Not frmGrsf Is Nothing Then frmGrsf.Dispose()
      frmGrsf = Nothing
      '--------------------------------------------------------------------------------------------------------------
    End Try
  End Sub

  Public Overridable Function SvuotaTmpTable() As Boolean
    Try
      '--------------------------------------------------------------------------------------------------------------
      oMenu.ResetTblInstId("TTSTAF", False, oCleSpaf.lIITTstaf)
      oMenu.ResetTblInstId("TTARTPRO", False, oCleSpaf.lIITTArtpro)
      oMenu.ResetTblInstId("TTARTPROX", False, oCleSpaf.lIITTArtprox)
      '--------------------------------------------------------------------------------------------------------------
      Return True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      Return False
    End Try
  End Function

End Class