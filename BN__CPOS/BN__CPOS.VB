Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__CPOS
#Region "Moduli"
  Private Moduli_P As Integer = 0
  Private ModuliExt_P As Integer = 0
  Private ModuliSup_P As Integer = bsModSupGPV
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
  Public oCleCpos As CLE__CPOS
  Public dsCpos As DataSet
  Public oCallParams As CLE__CLDP
  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbPrecedente As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSuccessivo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbUltimo As NTSInformatica.NTSBarButtonItem
  Public WithEvents edTb_descpos As NTSInformatica.NTSTextBoxStr
  Public WithEvents edtb_codcpos As NTSInformatica.NTSTextBoxNum
  Public WithEvents edTb_filein As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_descpos As NTSInformatica.NTSLabel
  Public WithEvents lbtb_codcpos As NTSInformatica.NTSLabel
  Public WithEvents lbTb_filein As NTSInformatica.NTSLabel
  Public WithEvents lbTb_fileout As NTSInformatica.NTSLabel
  Public WithEvents edTb_fileout As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbTb_timeout As NTSInformatica.NTSLabel
  Public WithEvents edTb_timeout As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbXx_cfgnum2 As NTSInformatica.NTSLabel
  Public WithEvents lbXx_cfgnum1 As NTSInformatica.NTSLabel
  Public WithEvents lbTb_cfgnum2 As NTSInformatica.NTSLabel
  Public WithEvents edTb_cfgnum2 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_cfgnum1 As NTSInformatica.NTSLabel
  Public WithEvents edTb_cfgnum1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbTb_bloccapaga As NTSInformatica.NTSLabel
  Public WithEvents cbTb_bloccapaga As NTSInformatica.NTSComboBox
  Public dcCpos As BindingSource = New BindingSource

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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__CPOS", "BE__CPOS", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222149375000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleCpos = CType(oTmp, CLE__CPOS)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__CPOS", strRemoteServer, strRemotePort)
    AddHandler oCleCpos.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleCpos.Init(oApp, NTSScript, oMenu.oCleComm, "TABCPOS", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------
    'povra di una chamata CallByName: 
    'la funzione Pippo è contenuta nella BG__CPOS che eredita da BE__CPOS
    'CallByName(oCleReca, "Pippo", CallType.Method, "Pippo")
    '------------------------------------------------
    Return True
  End Function

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__CPOS))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbPrecedente = New NTSInformatica.NTSBarButtonItem
    Me.tlbSuccessivo = New NTSInformatica.NTSBarButtonItem
    Me.tlbUltimo = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.edtb_codcpos = New NTSInformatica.NTSTextBoxNum
    Me.edTb_descpos = New NTSInformatica.NTSTextBoxStr
    Me.edTb_filein = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_descpos = New NTSInformatica.NTSLabel
    Me.lbtb_codcpos = New NTSInformatica.NTSLabel
    Me.lbTb_filein = New NTSInformatica.NTSLabel
    Me.lbTb_fileout = New NTSInformatica.NTSLabel
    Me.edTb_fileout = New NTSInformatica.NTSTextBoxStr
    Me.lbTb_timeout = New NTSInformatica.NTSLabel
    Me.edTb_timeout = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_cfgnum1 = New NTSInformatica.NTSLabel
    Me.edTb_cfgnum1 = New NTSInformatica.NTSTextBoxNum
    Me.lbTb_cfgnum2 = New NTSInformatica.NTSLabel
    Me.edTb_cfgnum2 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_cfgnum1 = New NTSInformatica.NTSLabel
    Me.lbXx_cfgnum2 = New NTSInformatica.NTSLabel
    Me.cbTb_bloccapaga = New NTSInformatica.NTSComboBox
    Me.lbTb_bloccapaga = New NTSInformatica.NTSLabel
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edtb_codcpos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_descpos.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_filein.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_fileout.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_timeout.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_cfgnum1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTb_cfgnum2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbTb_bloccapaga.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.Color.Red
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
    'NtsBarManager1
    '
    Me.NtsBarManager1.AllowCustomization = False
    Me.NtsBarManager1.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.tlbMain})
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlTop)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlBottom)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlLeft)
    Me.NtsBarManager1.DockControls.Add(Me.barDockControlRight)
    Me.NtsBarManager1.Form = Me
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbPrimo, Me.tlbPrecedente, Me.tlbSuccessivo, Me.tlbGuida, Me.tlbEsci, Me.tlbUltimo})
    Me.NtsBarManager1.MaxItemId = 26
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrimo, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbPrecedente), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSuccessivo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbUltimo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbNuovo.GlyphPath = ""
    Me.tlbNuovo.Id = 0
    Me.tlbNuovo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F2)
    Me.tlbNuovo.Name = "tlbNuovo"
    Me.tlbNuovo.Visible = True
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.GlyphPath = ""
    Me.tlbSalva.Id = 1
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.GlyphPath = ""
    Me.tlbRipristina.Id = 3
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 2
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 4
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbPrimo
    '
    Me.tlbPrimo.Caption = "Primo"
    Me.tlbPrimo.Glyph = CType(resources.GetObject("tlbPrimo.Glyph"), System.Drawing.Image)
    Me.tlbPrimo.GlyphPath = ""
    Me.tlbPrimo.Id = 5
    Me.tlbPrimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P))
    Me.tlbPrimo.Name = "tlbPrimo"
    Me.tlbPrimo.Visible = True
    '
    'tlbPrecedente
    '
    Me.tlbPrecedente.Caption = "Precedente"
    Me.tlbPrecedente.Glyph = CType(resources.GetObject("tlbPrecedente.Glyph"), System.Drawing.Image)
    Me.tlbPrecedente.GlyphPath = ""
    Me.tlbPrecedente.Id = 6
    Me.tlbPrecedente.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R))
    Me.tlbPrecedente.Name = "tlbPrecedente"
    Me.tlbPrecedente.Visible = True
    '
    'tlbSuccessivo
    '
    Me.tlbSuccessivo.Caption = "Successivo"
    Me.tlbSuccessivo.Glyph = CType(resources.GetObject("tlbSuccessivo.Glyph"), System.Drawing.Image)
    Me.tlbSuccessivo.GlyphPath = ""
    Me.tlbSuccessivo.Id = 7
    Me.tlbSuccessivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbSuccessivo.Name = "tlbSuccessivo"
    Me.tlbSuccessivo.Visible = True
    '
    'tlbUltimo
    '
    Me.tlbUltimo.Caption = "Ultimo"
    Me.tlbUltimo.Glyph = CType(resources.GetObject("tlbUltimo.Glyph"), System.Drawing.Image)
    Me.tlbUltimo.GlyphPath = ""
    Me.tlbUltimo.Id = 20
    Me.tlbUltimo.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.U))
    Me.tlbUltimo.Name = "tlbUltimo"
    Me.tlbUltimo.Visible = True
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 18
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 19
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'edtb_codcpos
    '
    Me.edtb_codcpos.Cursor = System.Windows.Forms.Cursors.Default
    Me.edtb_codcpos.EditValue = "0"
    Me.edtb_codcpos.Location = New System.Drawing.Point(113, 36)
    Me.edtb_codcpos.Name = "edtb_codcpos"
    Me.edtb_codcpos.NTSDbField = ""
    Me.edtb_codcpos.NTSFormat = "0"
    Me.edtb_codcpos.NTSForzaVisZoom = False
    Me.edtb_codcpos.NTSOldValue = ""
    Me.edtb_codcpos.Properties.Appearance.Options.UseTextOptions = True
    Me.edtb_codcpos.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edtb_codcpos.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edtb_codcpos.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edtb_codcpos.Properties.AutoHeight = False
    Me.edtb_codcpos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edtb_codcpos.Properties.MaxLength = 65536
    Me.edtb_codcpos.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edtb_codcpos.Size = New System.Drawing.Size(79, 20)
    Me.edtb_codcpos.TabIndex = 4
    '
    'edTb_descpos
    '
    Me.edTb_descpos.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_descpos.EditValue = ""
    Me.edTb_descpos.Location = New System.Drawing.Point(279, 36)
    Me.edTb_descpos.Name = "edTb_descpos"
    Me.edTb_descpos.NTSDbField = ""
    Me.edTb_descpos.NTSForzaVisZoom = False
    Me.edTb_descpos.NTSOldValue = ""
    Me.edTb_descpos.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_descpos.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_descpos.Properties.AutoHeight = False
    Me.edTb_descpos.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_descpos.Properties.MaxLength = 65536
    Me.edTb_descpos.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_descpos.Size = New System.Drawing.Size(235, 20)
    Me.edTb_descpos.TabIndex = 5
    '
    'edTb_filein
    '
    Me.edTb_filein.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_filein.EditValue = ""
    Me.edTb_filein.Location = New System.Drawing.Point(113, 59)
    Me.edTb_filein.Name = "edTb_filein"
    Me.edTb_filein.NTSDbField = ""
    Me.edTb_filein.NTSForzaVisZoom = False
    Me.edTb_filein.NTSOldValue = ""
    Me.edTb_filein.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_filein.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_filein.Properties.AutoHeight = False
    Me.edTb_filein.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_filein.Properties.MaxLength = 65536
    Me.edTb_filein.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_filein.Size = New System.Drawing.Size(401, 20)
    Me.edTb_filein.TabIndex = 6
    '
    'lbTb_descpos
    '
    Me.lbTb_descpos.AutoSize = True
    Me.lbTb_descpos.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_descpos.Location = New System.Drawing.Point(212, 39)
    Me.lbTb_descpos.Name = "lbTb_descpos"
    Me.lbTb_descpos.NTSDbField = ""
    Me.lbTb_descpos.Size = New System.Drawing.Size(61, 13)
    Me.lbTb_descpos.TabIndex = 17
    Me.lbTb_descpos.Text = "Descrizione"
    Me.lbTb_descpos.Tooltip = ""
    Me.lbTb_descpos.UseMnemonic = False
    '
    'lbtb_codcpos
    '
    Me.lbtb_codcpos.AutoSize = True
    Me.lbtb_codcpos.BackColor = System.Drawing.Color.Transparent
    Me.lbtb_codcpos.Location = New System.Drawing.Point(6, 39)
    Me.lbtb_codcpos.Name = "lbtb_codcpos"
    Me.lbtb_codcpos.NTSDbField = ""
    Me.lbtb_codcpos.Size = New System.Drawing.Size(39, 13)
    Me.lbtb_codcpos.TabIndex = 18
    Me.lbtb_codcpos.Text = "Codice"
    Me.lbtb_codcpos.Tooltip = ""
    Me.lbtb_codcpos.UseMnemonic = False
    '
    'lbTb_filein
    '
    Me.lbTb_filein.AutoSize = True
    Me.lbTb_filein.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_filein.Location = New System.Drawing.Point(6, 62)
    Me.lbTb_filein.Name = "lbTb_filein"
    Me.lbTb_filein.NTSDbField = ""
    Me.lbTb_filein.Size = New System.Drawing.Size(69, 13)
    Me.lbTb_filein.TabIndex = 19
    Me.lbTb_filein.Text = "File invio dati"
    Me.lbTb_filein.Tooltip = ""
    Me.lbTb_filein.UseMnemonic = False
    '
    'lbTb_fileout
    '
    Me.lbTb_fileout.AutoSize = True
    Me.lbTb_fileout.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_fileout.Location = New System.Drawing.Point(6, 85)
    Me.lbTb_fileout.Name = "lbTb_fileout"
    Me.lbTb_fileout.NTSDbField = ""
    Me.lbTb_fileout.Size = New System.Drawing.Size(89, 13)
    Me.lbTb_fileout.TabIndex = 27
    Me.lbTb_fileout.Text = "File ricezione dati"
    Me.lbTb_fileout.Tooltip = ""
    Me.lbTb_fileout.UseMnemonic = False
    '
    'edTb_fileout
    '
    Me.edTb_fileout.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_fileout.EditValue = ""
    Me.edTb_fileout.Location = New System.Drawing.Point(113, 82)
    Me.edTb_fileout.Name = "edTb_fileout"
    Me.edTb_fileout.NTSDbField = ""
    Me.edTb_fileout.NTSForzaVisZoom = False
    Me.edTb_fileout.NTSOldValue = ""
    Me.edTb_fileout.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_fileout.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_fileout.Properties.AutoHeight = False
    Me.edTb_fileout.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_fileout.Properties.MaxLength = 65536
    Me.edTb_fileout.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_fileout.Size = New System.Drawing.Size(401, 20)
    Me.edTb_fileout.TabIndex = 6
    '
    'lbTb_timeout
    '
    Me.lbTb_timeout.AutoSize = True
    Me.lbTb_timeout.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_timeout.Location = New System.Drawing.Point(6, 108)
    Me.lbTb_timeout.Name = "lbTb_timeout"
    Me.lbTb_timeout.NTSDbField = ""
    Me.lbTb_timeout.Size = New System.Drawing.Size(92, 13)
    Me.lbTb_timeout.TabIndex = 30
    Me.lbTb_timeout.Text = "Timeout (secondi)"
    Me.lbTb_timeout.Tooltip = ""
    Me.lbTb_timeout.UseMnemonic = False
    '
    'edTb_timeout
    '
    Me.edTb_timeout.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_timeout.EditValue = "0"
    Me.edTb_timeout.Location = New System.Drawing.Point(113, 105)
    Me.edTb_timeout.Name = "edTb_timeout"
    Me.edTb_timeout.NTSDbField = ""
    Me.edTb_timeout.NTSFormat = "0"
    Me.edTb_timeout.NTSForzaVisZoom = False
    Me.edTb_timeout.NTSOldValue = ""
    Me.edTb_timeout.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_timeout.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_timeout.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_timeout.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_timeout.Properties.AutoHeight = False
    Me.edTb_timeout.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_timeout.Properties.MaxLength = 65536
    Me.edTb_timeout.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_timeout.Size = New System.Drawing.Size(79, 20)
    Me.edTb_timeout.TabIndex = 29
    '
    'lbTb_cfgnum1
    '
    Me.lbTb_cfgnum1.AutoSize = True
    Me.lbTb_cfgnum1.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_cfgnum1.Location = New System.Drawing.Point(6, 154)
    Me.lbTb_cfgnum1.Name = "lbTb_cfgnum1"
    Me.lbTb_cfgnum1.NTSDbField = ""
    Me.lbTb_cfgnum1.Size = New System.Drawing.Size(79, 13)
    Me.lbTb_cfgnum1.TabIndex = 32
    Me.lbTb_cfgnum1.Text = "Pag. bancomat"
    Me.lbTb_cfgnum1.Tooltip = ""
    Me.lbTb_cfgnum1.UseMnemonic = False
    '
    'edTb_cfgnum1
    '
    Me.edTb_cfgnum1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_cfgnum1.EditValue = "0"
    Me.edTb_cfgnum1.Location = New System.Drawing.Point(113, 151)
    Me.edTb_cfgnum1.Name = "edTb_cfgnum1"
    Me.edTb_cfgnum1.NTSDbField = ""
    Me.edTb_cfgnum1.NTSFormat = "0"
    Me.edTb_cfgnum1.NTSForzaVisZoom = False
    Me.edTb_cfgnum1.NTSOldValue = ""
    Me.edTb_cfgnum1.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_cfgnum1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_cfgnum1.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_cfgnum1.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_cfgnum1.Properties.AutoHeight = False
    Me.edTb_cfgnum1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_cfgnum1.Properties.MaxLength = 65536
    Me.edTb_cfgnum1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_cfgnum1.Size = New System.Drawing.Size(79, 20)
    Me.edTb_cfgnum1.TabIndex = 31
    '
    'lbTb_cfgnum2
    '
    Me.lbTb_cfgnum2.AutoSize = True
    Me.lbTb_cfgnum2.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_cfgnum2.Location = New System.Drawing.Point(6, 177)
    Me.lbTb_cfgnum2.Name = "lbTb_cfgnum2"
    Me.lbTb_cfgnum2.NTSDbField = ""
    Me.lbTb_cfgnum2.Size = New System.Drawing.Size(104, 13)
    Me.lbTb_cfgnum2.TabIndex = 34
    Me.lbTb_cfgnum2.Text = "Pag. carta di credito"
    Me.lbTb_cfgnum2.Tooltip = ""
    Me.lbTb_cfgnum2.UseMnemonic = False
    '
    'edTb_cfgnum2
    '
    Me.edTb_cfgnum2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTb_cfgnum2.EditValue = "0"
    Me.edTb_cfgnum2.Location = New System.Drawing.Point(113, 174)
    Me.edTb_cfgnum2.Name = "edTb_cfgnum2"
    Me.edTb_cfgnum2.NTSDbField = ""
    Me.edTb_cfgnum2.NTSFormat = "0"
    Me.edTb_cfgnum2.NTSForzaVisZoom = False
    Me.edTb_cfgnum2.NTSOldValue = ""
    Me.edTb_cfgnum2.Properties.Appearance.Options.UseTextOptions = True
    Me.edTb_cfgnum2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edTb_cfgnum2.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTb_cfgnum2.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTb_cfgnum2.Properties.AutoHeight = False
    Me.edTb_cfgnum2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edTb_cfgnum2.Properties.MaxLength = 65536
    Me.edTb_cfgnum2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTb_cfgnum2.Size = New System.Drawing.Size(79, 20)
    Me.edTb_cfgnum2.TabIndex = 33
    '
    'lbXx_cfgnum1
    '
    Me.lbXx_cfgnum1.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_cfgnum1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_cfgnum1.Location = New System.Drawing.Point(198, 151)
    Me.lbXx_cfgnum1.Name = "lbXx_cfgnum1"
    Me.lbXx_cfgnum1.NTSDbField = ""
    Me.lbXx_cfgnum1.Size = New System.Drawing.Size(316, 20)
    Me.lbXx_cfgnum1.TabIndex = 35
    Me.lbXx_cfgnum1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_cfgnum1.Tooltip = ""
    Me.lbXx_cfgnum1.UseMnemonic = False
    '
    'lbXx_cfgnum2
    '
    Me.lbXx_cfgnum2.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_cfgnum2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_cfgnum2.Location = New System.Drawing.Point(198, 174)
    Me.lbXx_cfgnum2.Name = "lbXx_cfgnum2"
    Me.lbXx_cfgnum2.NTSDbField = ""
    Me.lbXx_cfgnum2.Size = New System.Drawing.Size(316, 20)
    Me.lbXx_cfgnum2.TabIndex = 36
    Me.lbXx_cfgnum2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lbXx_cfgnum2.Tooltip = ""
    Me.lbXx_cfgnum2.UseMnemonic = False
    '
    'cbTb_bloccapaga
    '
    Me.cbTb_bloccapaga.DataSource = Nothing
    Me.cbTb_bloccapaga.DisplayMember = ""
    Me.cbTb_bloccapaga.Location = New System.Drawing.Point(113, 128)
    Me.cbTb_bloccapaga.Name = "cbTb_bloccapaga"
    Me.cbTb_bloccapaga.NTSDbField = ""
    Me.cbTb_bloccapaga.Properties.AutoHeight = False
    Me.cbTb_bloccapaga.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTb_bloccapaga.Properties.DropDownRows = 30
    Me.cbTb_bloccapaga.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTb_bloccapaga.SelectedValue = ""
    Me.cbTb_bloccapaga.Size = New System.Drawing.Size(401, 20)
    Me.cbTb_bloccapaga.TabIndex = 37
    Me.cbTb_bloccapaga.ValueMember = ""
    '
    'lbTb_bloccapaga
    '
    Me.lbTb_bloccapaga.AutoSize = True
    Me.lbTb_bloccapaga.BackColor = System.Drawing.Color.Transparent
    Me.lbTb_bloccapaga.Location = New System.Drawing.Point(6, 131)
    Me.lbTb_bloccapaga.Name = "lbTb_bloccapaga"
    Me.lbTb_bloccapaga.NTSDbField = ""
    Me.lbTb_bloccapaga.Size = New System.Drawing.Size(106, 13)
    Me.lbTb_bloccapaga.TabIndex = 38
    Me.lbTb_bloccapaga.Text = "Tipologia pagamento"
    Me.lbTb_bloccapaga.Tooltip = ""
    Me.lbTb_bloccapaga.UseMnemonic = False
    '
    'FRM__CPOS
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(522, 199)
    Me.Controls.Add(Me.lbTb_bloccapaga)
    Me.Controls.Add(Me.cbTb_bloccapaga)
    Me.Controls.Add(Me.lbXx_cfgnum2)
    Me.Controls.Add(Me.lbXx_cfgnum1)
    Me.Controls.Add(Me.lbTb_cfgnum2)
    Me.Controls.Add(Me.edTb_cfgnum2)
    Me.Controls.Add(Me.lbTb_cfgnum1)
    Me.Controls.Add(Me.edTb_cfgnum1)
    Me.Controls.Add(Me.lbTb_timeout)
    Me.Controls.Add(Me.edTb_timeout)
    Me.Controls.Add(Me.lbTb_fileout)
    Me.Controls.Add(Me.lbTb_filein)
    Me.Controls.Add(Me.lbtb_codcpos)
    Me.Controls.Add(Me.lbTb_descpos)
    Me.Controls.Add(Me.edTb_fileout)
    Me.Controls.Add(Me.edTb_filein)
    Me.Controls.Add(Me.edTb_descpos)
    Me.Controls.Add(Me.edtb_codcpos)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "FRM__CPOS"
    Me.Text = "CONFIGURAZIONE POS"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edtb_codcpos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_descpos.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_filein.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_fileout.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_timeout.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_cfgnum1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTb_cfgnum2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbTb_bloccapaga.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbNuovo.GlyphPath = (oApp.ChildImageDir & "\new.gif")
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbCancella.GlyphPath = (oApp.ChildImageDir & "\delete.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbPrimo.GlyphPath = (oApp.ChildImageDir & "\movefirst.gif")
        tlbPrecedente.GlyphPath = (oApp.ChildImageDir & "\moveprevious.gif")
        tlbSuccessivo.GlyphPath = (oApp.ChildImageDir & "\movenext.gif")
        tlbUltimo.GlyphPath = (oApp.ChildImageDir & "\movelast.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      '-------------------------------------------------
      'completo le informazioni dei i controlli
      edTb_timeout.NTSSetParam(oMenu, oApp.Tr(Me, 129381996913125000, "Time out"), "0", 9, 0, 999999999)
      edTb_filein.NTSSetParam(oMenu, oApp.Tr(Me, 130909309769588264, "Percorso generazione file WPOS1"), 255)
      edTb_fileout.NTSSetParam(oMenu, oApp.Tr(Me, 129381996915781250, "Percorso generazione file WPOS1"), 255)
      edTb_descpos.NTSSetParam(oMenu, oApp.Tr(Me, 129381996915937500, "Descrizione"), 50)
      edtb_codcpos.NTSSetParam(oMenu, oApp.Tr(Me, 129381996916093750, "Codice"), "0", 5, 0, 99999)
      cbTb_bloccapaga.NTSSetParam(oApp.Tr(Me, 130906711124550988, "Blocca pagamento"))
      edTb_cfgnum1.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130918823690655228, "Pagamento Bancomat"), tabpaga)
      edTb_cfgnum2.NTSSetParamTabe(oMenu, oApp.Tr(Me, 130918823957146325, "Pagamento Carta di Credito"), tabpaga)


      edTb_descpos.NTSForzaVisZoom = True
      edTb_filein.NTSForzaVisZoom = True
      edTb_fileout.NTSForzaVisZoom = True

      edtb_codcpos.NTSSetRichiesto()
      edTb_descpos.NTSSetRichiesto()
      edTb_filein.NTSSetRichiesto()
      edTb_fileout.NTSSetRichiesto()
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

  Public Overridable Sub Bindcontrols()
    Try
      '-------------------------------------------------
      'se i controlli erano già stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      edTb_timeout.NTSDbField = "TABCPOS.tb_timeout"
      edTb_filein.NTSDbField = "TABCPOS.tb_filein"
      edTb_fileout.NTSDbField = "TABCPOS.tb_fileout"
      edTb_descpos.NTSDbField = "TABCPOS.tb_descpos"
      edtb_codcpos.NTSDbField = "TABCPOS.tb_codcpos"
      cbTb_bloccapaga.NTSDbField = "TABCPOS.tb_bloccapaga"
      edTb_cfgnum1.NTSDbField = "TABCPOS.tb_cfgnum1"
      edTb_cfgnum2.NTSDbField = "TABCPOS.tb_cfgnum2"
      lbXx_cfgnum1.NTSDbField = "TABCPOS.xx_cfgnum1"
      lbXx_cfgnum2.NTSDbField = "TABCPOS.xx_cfgnum2"
      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcCpos, Me)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub CaricaCombo()
    Dim dttCombo As New DataTable
    Try
      dttCombo.Columns.Add("cod", GetType(String))
      dttCombo.Columns.Add("val", GetType(String))
      dttCombo.Rows.Add("N", "Forza su GPV la forma di pagamento scelta dal POS (se pagamenti configurati)")
      dttCombo.Rows.Add("S", "Forza nel POS la forma di pagamento scelta da GPV")
      dttCombo.AcceptChanges()

      cbTb_bloccapaga.DataSource = dttCombo
      cbTb_bloccapaga.DisplayMember = "val"
      cbTb_bloccapaga.ValueMember = "cod"
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

#Region "EVENTI DI FORM"
  Public Overridable Sub FRM__CPOS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      CaricaCombo()
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      If Not oCleCpos.Apri(DittaCorrente, dsCpos) Then Me.Close()
      dcCpos.DataSource = dsCpos.Tables("TABCPOS")
      dsCpos.AcceptChanges()
      oCleCpos.bHasChanges = False
      If dsCpos.Tables("TABCPOS").Rows.Count > 0 Then edtb_codcpos.Enabled = False

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      'e comunque dopo si può lanciare dopo aver impostato la ditta, cambiato il tipodocumento, ecc ...
      'GctlTipoDoc = ""
      GctlSetRoules()

      'solo per prova: annulla le impostazioni della GCTL
      'GctlRestoreStandardValue()

      If dsCpos.Tables("TABCPOS").Rows.Count = 0 Then tlbNuovo_ItemClick(tlbNuovo, Nothing)
      dcCpos.ResetBindings(False)
      dcCpos.MoveFirst()

      '--------------------------------------------
      'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
      If Not oCallParams Is Nothing Then
        If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
          If dsCpos.Tables("TABCPOS").Rows.Count = 1 And dsCpos.Tables("TABCPOS").Rows(0).RowState = DataRowState.Added Then
            'sono già in record nuovo perchè la tabella è vuota: non devo fare nulla
          Else
            tlbNuovo_ItemClick(Me, Nothing)
          End If
        ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
          For i As Integer = 0 To dsCpos.Tables("TABCPOS").Rows.Count - 1
            If dsCpos.Tables("TABCPOS").Rows(i)!tb_codcpos.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
              dcCpos.Position = i
              Exit For
            End If
          Next
        End If
      End If    'If Not oCallParams Is Nothing Then

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__CPOS_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    Try
      If Not Salva() Then e.Cancel = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__CPOS_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    Try
      dcCpos.Dispose()
      dsCpos.Dispose()
    Catch
    End Try
  End Sub
#End Region

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo

      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleCpos.RecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791952559218750, "Salvo il POS?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleCpos.Salva(False) Then Return False
          If dsCpos.Tables("TABCPOS").Rows.Count > 0 Then
            edtb_codcpos.Enabled = False
          End If
        End If
        If dRes = System.Windows.Forms.DialogResult.No Then
          tlbRipristina_ItemClick(Nothing, Nothing)
        End If
      End If
      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Function

#Region "Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      '-------------------------------------------------
      'creo uno nuovo POS
      If Not Salva() Then Return
      oCleCpos.Nuovo()
      dcCpos.MoveLast()
      edtb_codcpos.Enabled = True
      edtb_codcpos.Focus()
      dsCpos.AcceptChanges()
      dsCpos.Tables("TABCPOS").Rows(dsCpos.Tables("TABCPOS").Rows.Count - 1).SetAdded()

      '-------------------------------------------------
      'imposto i valori di default come impostato nella GCTL
      Me.GctlApplicaDefaultValue()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSalva_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSalva.ItemClick
    Try
      '-------------------------------------------------
      'prima di salvare simulo una lostfocus del campo su cui mi trovo, altrimenti potrei salvare un dato non corretto
      Salva()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbCancella_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbCancella.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      '--------------------------------------------------------------------------------------------------------------
      If Not oCleCpos.IsPOSDeletable(NTSCInt(edtb_codcpos.Text)) Then Return
      '--------------------------------------------------------------------------------------------------------------
      'cancello il pos
      Dim dlgRes As DialogResult
      dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791952085312500, "Cancellare il POS?"))
      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsCpos.Tables("TABCPOS").Rows.Count = 1 Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          dcCpos.RemoveAt(dcCpos.Position)
          oCleCpos.Salva(True)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcCpos, Me)
            bRemovBinding = False
            edtb_codcpos.Enabled = True
          Else
            edtb_codcpos.Enabled = False
          End If

          Return
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcCpos, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco

      '-------------------------------------------------
      'ripristino il POS
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 127791952835468750, "Ripristinare le modifiche apportate al POS?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes

          If dsCpos.Tables("TABCPOS").Rows.Count = 1 And dsCpos.Tables("TABCPOS").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleCpos.Ripristina(dcCpos.Position, dcCpos.Filter)

          If bRemovBinding Then
            tlbNuovo_ItemClick(tlbNuovo, Nothing)
            NTSFormAddDataBinding(dcCpos, Me)
            bRemovBinding = False
            edtb_codcpos.Enabled = True
          Else
            edtb_codcpos.Enabled = False
          End If
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcCpos, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim i As Integer
    Dim oParam As New CLE__PATB
    Try
      '------------------------------------
      'zoom standard di textbox e griglia
      If edTb_filein.Focused Then
        Dim fdFile As New NTSOpenFileDialog
        If fdFile.ShowDialog() = Windows.Forms.DialogResult.OK Then edTb_filein.NTSTextDB = fdFile.FileName
        fdFile.Dispose()
        fdFile = Nothing
      ElseIf edTb_fileout.Focused Then
        Dim fdFile As New NTSOpenFileDialog
        If fdFile.ShowDialog() = Windows.Forms.DialogResult.OK Then edTb_fileout.NTSTextDB = fdFile.FileName
        fdFile.Dispose()
        fdFile = Nothing
      ElseIf edTb_descpos.Focused Then
        Me.ValidaLastControl()            'altrimeti se cambio la descrizione e poi faccio zoom per spostarmi non chiede di salvare !!!
        If oCleCpos.RecordIsChanged Then
          oApp.MsgBoxErr(oApp.Tr(Me, 129382556937656250, "Salvare o ripristinare le modifiche prima di selezionare un nuovo POS"))
          Return
        End If

        NTSZOOM.strIn = edtb_codcpos.Text
        NTSZOOM.ZoomStrIn("ZOOMTABCPOS", DittaCorrente, oParam)

        If NTSZOOM.strIn <> edtb_codcpos.Text Then
          'mi sposto sul record selezionato
          For i = 0 To dcCpos.List.Count - 1
            If CType(dcCpos.Item(i), DataRowView)!tb_codcpos.ToString = NTSZOOM.strIn Then
              dcCpos.Position = i
              Exit For
            End If
          Next
        End If
      Else
        NTSCallStandardZoom()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

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

  Public Overridable Sub tlbPrimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul primo record
      If Not Salva() Then Return
      dcCpos.MoveFirst()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbPrecedente_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbPrecedente.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record precedente
      If Not Salva() Then Return
      dcCpos.MovePrevious()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbSuccessivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbSuccessivo.ItemClick
    Try
      '-------------------------------------------------
      'vado sul record successivo
      If Not Salva() Then Return
      dcCpos.MoveNext()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbUltimo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbUltimo.ItemClick
    Try
      '-------------------------------------------------
      'vado sull'ultimo record
      If Not Salva() Then Return
      dcCpos.MoveLast()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

#Region "Eventi"
  Public Overridable Sub cbTb_bloccapaga_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTb_bloccapaga.SelectedValueChanged
    Try
      If cbTb_bloccapaga.SelectedValue = "N" Then
        edTb_cfgnum1.Enabled = True
        edTb_cfgnum2.Enabled = True
      Else
        edTb_cfgnum1.Enabled = False
        edTb_cfgnum2.Enabled = False
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region
End Class
