Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRMMGMATR
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

  Public oCleMatr As CLEMGMATR
  Public oCallParams As CLE__CLDP
  Public dsMatr As DataSet
  Public dcMatr As BindingSource = New BindingSource()
  Public bInAbilitaCellaEnter As Boolean = False

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGeneraMatricole As NTSInformatica.NTSBarMenuItem
  Public WithEvents tlbGeneraProgressivo As NTSInformatica.NTSBarMenuItem
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents grMatr As NTSInformatica.NTSGrid
  Public WithEvents grvMatr As NTSInformatica.NTSGridView
  Public WithEvents mma_rigaa As NTSInformatica.NTSGridColumn
  Public WithEvents mma_quant As NTSInformatica.NTSGridColumn
  Public WithEvents mma_matric As NTSInformatica.NTSGridColumn
  Public WithEvents mma_note As NTSInformatica.NTSGridColumn
  Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMMGMATR))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbCancella = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem
    Me.tlbGeneraMatricole = New NTSInformatica.NTSBarMenuItem
    Me.tlbGeneraProgressivo = New NTSInformatica.NTSBarMenuItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.grMatr = New NTSInformatica.NTSGrid
    Me.grvMatr = New NTSInformatica.NTSGridView
    Me.mma_rigaa = New NTSInformatica.NTSGridColumn
    Me.mma_matric = New NTSInformatica.NTSGridColumn
    Me.mma_quant = New NTSInformatica.NTSGridColumn
    Me.mma_note = New NTSInformatica.NTSGridColumn
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grMatr, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.grvMatr, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbGeneraMatricole, Me.tlbGeneraProgressivo, Me.tlbEsci, Me.tlbZoom, Me.tlbStrumenti})
    Me.NtsBarManager1.MaxItemId = 19
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbNuovo), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbCancella), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbStrumenti, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
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
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
    '
    'tlbCancella
    '
    Me.tlbCancella.Caption = "Cancella"
    Me.tlbCancella.Glyph = CType(resources.GetObject("tlbCancella.Glyph"), System.Drawing.Image)
    Me.tlbCancella.GlyphPath = ""
    Me.tlbCancella.Id = 3
    Me.tlbCancella.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F4)
    Me.tlbCancella.Name = "tlbCancella"
    Me.tlbCancella.Visible = True
    '
    'tlbZoom
    '
    Me.tlbZoom.Caption = "Zoom"
    Me.tlbZoom.Glyph = CType(resources.GetObject("tlbZoom.Glyph"), System.Drawing.Image)
    Me.tlbZoom.GlyphPath = ""
    Me.tlbZoom.Id = 13
    Me.tlbZoom.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5)
    Me.tlbZoom.Name = "tlbZoom"
    Me.tlbZoom.Visible = True
    '
    'tlbStrumenti
    '
    Me.tlbStrumenti.Caption = "Strumenti"
    Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
    Me.tlbStrumenti.GlyphPath = ""
    Me.tlbStrumenti.Id = 17
    Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGeneraMatricole), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGeneraProgressivo)})
    Me.tlbStrumenti.Name = "tlbStrumenti"
    Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
    Me.tlbStrumenti.Visible = True
    '
    'tlbGeneraMatricole
    '
    Me.tlbGeneraMatricole.Caption = "Scarica da matricola numero..."
    Me.tlbGeneraMatricole.GlyphPath = ""
    Me.tlbGeneraMatricole.Id = 4
    Me.tlbGeneraMatricole.ItemShortcut = New DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S))
    Me.tlbGeneraMatricole.Name = "tlbGeneraMatricole"
    Me.tlbGeneraMatricole.NTSIsCheckBox = False
    Me.tlbGeneraMatricole.Visible = True
    '
    'tlbGeneraProgressivo
    '
    Me.tlbGeneraProgressivo.Caption = "Genera numero progressivo"
    Me.tlbGeneraProgressivo.GlyphPath = ""
    Me.tlbGeneraProgressivo.Id = 5
    Me.tlbGeneraProgressivo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F7)
    Me.tlbGeneraProgressivo.Name = "tlbGeneraProgressivo"
    Me.tlbGeneraProgressivo.NTSIsCheckBox = False
    Me.tlbGeneraProgressivo.Visible = True
    '
    'tlbEsci
    '
    Me.tlbEsci.Caption = "Esci"
    Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
    Me.tlbEsci.GlyphPath = ""
    Me.tlbEsci.Id = 12
    Me.tlbEsci.Name = "tlbEsci"
    Me.tlbEsci.Visible = True
    '
    'grMatr
    '
    Me.grMatr.Dock = System.Windows.Forms.DockStyle.Fill
    '
    '
    '
    Me.grMatr.EmbeddedNavigator.Name = ""
    Me.grMatr.Location = New System.Drawing.Point(0, 30)
    Me.grMatr.MainView = Me.grvMatr
    Me.grMatr.Name = "grMatr"
    Me.grMatr.Size = New System.Drawing.Size(648, 412)
    Me.grMatr.TabIndex = 5
    Me.grMatr.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvMatr})
    '
    'grvMatr
    '
    Me.grvMatr.ActiveFilterEnabled = False
    Me.grvMatr.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.mma_rigaa, Me.mma_matric, Me.mma_quant, Me.mma_note})
    Me.grvMatr.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
    Me.grvMatr.Enabled = True
    Me.grvMatr.GridControl = Me.grMatr
    Me.grvMatr.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
    Me.grvMatr.MinRowHeight = 14
    Me.grvMatr.Name = "grvMatr"
    Me.grvMatr.NTSAllowDelete = True
    Me.grvMatr.NTSAllowInsert = True
    Me.grvMatr.NTSAllowUpdate = True
    Me.grvMatr.NTSMenuContext = Nothing
    Me.grvMatr.OptionsCustomization.AllowRowSizing = True
    Me.grvMatr.OptionsFilter.AllowFilterEditor = False
    Me.grvMatr.OptionsNavigation.EnterMoveNextColumn = True
    Me.grvMatr.OptionsNavigation.UseTabKey = False
    Me.grvMatr.OptionsSelection.EnableAppearanceFocusedRow = False
    Me.grvMatr.OptionsView.ColumnAutoWidth = False
    Me.grvMatr.OptionsView.EnableAppearanceEvenRow = True
    Me.grvMatr.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
    Me.grvMatr.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    Me.grvMatr.OptionsView.ShowGroupPanel = False
    Me.grvMatr.RowHeight = 16
    '
    'mma_rigaa
    '
    Me.mma_rigaa.AppearanceCell.Options.UseBackColor = True
    Me.mma_rigaa.AppearanceCell.Options.UseTextOptions = True
    Me.mma_rigaa.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mma_rigaa.Caption = "Riga"
    Me.mma_rigaa.Enabled = False
    Me.mma_rigaa.FieldName = "mma_rigaa"
    Me.mma_rigaa.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mma_rigaa.Name = "mma_rigaa"
    Me.mma_rigaa.NTSRepositoryComboBox = Nothing
    Me.mma_rigaa.NTSRepositoryItemCheck = Nothing
    Me.mma_rigaa.NTSRepositoryItemMemo = Nothing
    Me.mma_rigaa.NTSRepositoryItemText = Nothing
    Me.mma_rigaa.OptionsColumn.AllowEdit = False
    Me.mma_rigaa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mma_rigaa.OptionsColumn.ReadOnly = True
    Me.mma_rigaa.OptionsFilter.AllowFilter = False
    Me.mma_rigaa.Width = 70
    '
    'mma_matric
    '
    Me.mma_matric.AppearanceCell.Options.UseBackColor = True
    Me.mma_matric.AppearanceCell.Options.UseTextOptions = True
    Me.mma_matric.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mma_matric.Caption = "Matricola"
    Me.mma_matric.Enabled = True
    Me.mma_matric.FieldName = "mma_matric"
    Me.mma_matric.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mma_matric.Name = "mma_matric"
    Me.mma_matric.NTSRepositoryComboBox = Nothing
    Me.mma_matric.NTSRepositoryItemCheck = Nothing
    Me.mma_matric.NTSRepositoryItemMemo = Nothing
    Me.mma_matric.NTSRepositoryItemText = Nothing
    Me.mma_matric.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mma_matric.OptionsFilter.AllowFilter = False
    Me.mma_matric.Visible = True
    Me.mma_matric.VisibleIndex = 0
    Me.mma_matric.Width = 70
    '
    'mma_quant
    '
    Me.mma_quant.AppearanceCell.Options.UseBackColor = True
    Me.mma_quant.AppearanceCell.Options.UseTextOptions = True
    Me.mma_quant.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mma_quant.Caption = "Quantita'"
    Me.mma_quant.Enabled = True
    Me.mma_quant.FieldName = "mma_quant"
    Me.mma_quant.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mma_quant.Name = "mma_quant"
    Me.mma_quant.NTSRepositoryComboBox = Nothing
    Me.mma_quant.NTSRepositoryItemCheck = Nothing
    Me.mma_quant.NTSRepositoryItemMemo = Nothing
    Me.mma_quant.NTSRepositoryItemText = Nothing
    Me.mma_quant.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mma_quant.OptionsFilter.AllowFilter = False
    Me.mma_quant.Visible = True
    Me.mma_quant.VisibleIndex = 1
    Me.mma_quant.Width = 70
    '
    'mma_note
    '
    Me.mma_note.AppearanceCell.Options.UseBackColor = True
    Me.mma_note.AppearanceCell.Options.UseTextOptions = True
    Me.mma_note.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
    Me.mma_note.Caption = "note"
    Me.mma_note.Enabled = True
    Me.mma_note.FieldName = "mma_note"
    Me.mma_note.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
    Me.mma_note.Name = "mma_note"
    Me.mma_note.NTSRepositoryComboBox = Nothing
    Me.mma_note.NTSRepositoryItemCheck = Nothing
    Me.mma_note.NTSRepositoryItemMemo = Nothing
    Me.mma_note.NTSRepositoryItemText = Nothing
    Me.mma_note.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
    Me.mma_note.OptionsFilter.AllowFilter = False
    Me.mma_note.Visible = True
    Me.mma_note.VisibleIndex = 2
    Me.mma_note.Width = 70
    '
    'FRMMGMATR
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(648, 442)
    Me.Controls.Add(Me.grMatr)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRMMGMATR"
    Me.NTSLastControlFocussed = Me.grMatr
    Me.Text = "DETTAGLIO MATRICOLE"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grMatr, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.grvMatr, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BNMGMATR", "BEMGMATR", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128696805805312500, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleMatr = CType(oTmp, CLEMGMATR)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BNMGMATR", strRemoteServer, strRemotePort)
    AddHandler oCleMatr.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleMatr.Init(oApp, oScript, oMenu.oCleComm, "MOVMATR", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

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
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      grvMatr.NTSSetParam(oMenu, "DETTAGLIO MATRICOLE")
      mma_rigaa.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128696805804687500, "Riga"), "0", 4, 0, 9999)
      mma_quant.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128696805804843750, "Quantita'"), oApp.FormatQta, 9, -999999999, 999999999)
      mma_matric.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128696805805000000, "Matricola"), 30, False)
      mma_note.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128696805805156250, "note"), 0, True)
      mma_matric.NTSSetParamZoom("ZOOMSCHEDEARTMATR")

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

#Region "Eventi di Form"
  Public overridable Sub FRMMGMATR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      oCleMatr.bAutoAggiornaRighe = CBool(Val(oMenu.GetSettingBus("Bsmgmatr", "Opzioni", ".", "AutoAggiornaRighe", "0", " ", "0")))
      oCleMatr.strControllaMatricoleInScarico = oMenu.GetSettingBus("Bsmgmatr", "Opzioni", ".", "ControllaMatricoleInScarico", " ", " ", " ") 'blank=non controllare, A=avvisa, b=blocca
      oCleMatr.strControllaMatricoleInCarico = oMenu.GetSettingBus("Bsmgmatr", "Opzioni", ".", "ControllaMatricoleInCarico", " ", " ", " ") 'blank=non controllare, A=avvisa, b=blocca
      oCleMatr.bCodartDaBarcode = CBool(Val(oMenu.GetSettingBus("Bsveboll", "Opzioni", ".", "CodartDaBarcode", "0", " ", "0"))) 'NON DOCUMENTARE

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBindingNavigator

      'oCallParams.ctlPar1 = ds     'matricole relative alla riga su cui sono
      'oCallParams.ctlPar2 = dsBoll 'il dataset completo senza le matricole della riga su cui sono

      dsMatr = CType(oCallParams.ctlPar1, DataSet)
      oCleMatr.dsDoc = CType(oCallParams.ctlPar2, DataSet)
      oCleMatr.bNewdoc = oCallParams.bAddNew
      oCleMatr.Apri(DittaCorrente, dsMatr)
      oCleMatr.strTipork = oCallParams.strPar1
      oCleMatr.nAnno = NTSCInt(oCallParams.dPar1)
      oCleMatr.strSerie = oCallParams.strPar2
      oCleMatr.lNumdoc = NTSCInt(oCallParams.dPar2)
      oCleMatr.lRiga = NTSCInt(oCallParams.dPar3)
      oCleMatr.dQuant = oCallParams.dPar4
      oCleMatr.nEsist = NTSCInt(oCallParams.dPar5)
      oCleMatr.strCodart = oCallParams.strPar3
      oCleMatr.nfase = NTSCInt(oCallParams.ctlPar3)
      oCleMatr.nMagaz = NTSCInt(oCallParams.ctlPar4)
      oCleMatr.nMagaz2 = NTSCInt(oCallParams.ctlPar5)

      dcMatr.DataSource = dsMatr.Tables("MOVMATR")
      dsMatr.AcceptChanges()

      grMatr.DataSource = dcMatr

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRMMGMATR_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
    If Not oCleMatr.SalvaFinale Then e.Cancel = True
  End Sub
#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
    Try
      grvMatr.NTSNuovo()

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
      If Not grvMatr.NTSDeleteRigaCorrente(dcMatr, True) Then Return
      oCleMatr.Salva(True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Try
      If Not grvMatr.NTSRipristinaRigaCorrenteBefore(dcMatr, True) Then Return
      oCleMatr.Ripristina(dcMatr.Position, dcMatr.Filter)
      grvMatr.NTSRipristinaRigaCorrenteAfter()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Dim oPar As New CLE__PATB
    Dim strParam As String = ""
    Try
      If grvMatr.FocusedColumn.Name = "mma_matric" Then
        strParam = "BNVEBOLL;"
        strParam = strParam & oCleMatr.strCodart & ";"
        If oCleMatr.nMagaz2 <> 0 And oCleMatr.nEsist > 0 Then
          'il magazzino 1 è quello di carico, per cui il magaz 2 (se impostato) è quello di scarico)
          strParam = strParam & oCleMatr.nMagaz2.ToString & ";"
        Else
          strParam = strParam & oCleMatr.nMagaz.ToString & ";"
        End If
        strParam = strParam & "2;"
        strParam = strParam & "TTSTMATR;"
        strParam = strParam & "TTSTMATS;"
        strParam = strParam & ";;;;;;;;;;;;;"
        strParam = strParam & oCleMatr.nFase.ToString & ";"
        strParam = strParam & oCleMatr.nFase.ToString & ";"
        oPar.strTipo = strParam
        NTSZOOM.ZoomStrIn("ZOOMSCHEDEARTMATR", DittaCorrente, oPar)
        If oPar.strOut.Trim <> "" Then
          grvMatr.SetFocusedValue(oPar.strOut.Split(";"c)(0))
          grvMatr.SetFocusedRowCellValue(mma_quant, oPar.strOut.Split(";"c)(1))
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

  Public Overridable Sub tlbGeneraMatricole_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGeneraMatricole.ItemClick
    Try
      oCleMatr.GeneraPartendoDa()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGeneraProgressivo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGeneraProgressivo.ItemClick
    Try
      oCleMatr.GeneraProgressivo()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Sub grvMatr_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvMatr.NTSBeforeRowUpdate
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

  Public Overridable Sub grvMatr_NTSCellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvMatr.NTSCellValueChanged
    Try
      '--------------------------------------------------------------------------------------------------------------
      If oCleMatr.bAutoAggiornaRighe = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If grvMatr.FocusedColumn.Name.ToUpper = "MMA_MATRIC" Then
        If grvMatr.GetFocusedRowCellValue(mma_matric).ToString.Trim <> "" Then
          If Salva() = True Then
            grvMatr.NTSNuovo()
            grvMatr.FocusedColumn = mma_matric
          End If
        End If
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grvMatr_NTSFocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvMatr.NTSFocusedRowChanged
    Try
      AbilitaCellaMatricola()
      '--------------------------------------------------------------------------------------------------------------
      If oCleMatr.bAutoAggiornaRighe = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If dsMatr.Tables("MOVMATR") Is Nothing Then Return
      If dsMatr.Tables("MOVMATR").Rows.Count = 0 Then Return
      If grvMatr.GetFocusedRowCellValue(mma_matric) Is Nothing Then Return
      '--------------------------------------------------------------------------------------------------------------
      If (NTSCStr(grvMatr.GetFocusedRowCellValue(mma_matric)) = "") And (grvMatr.NTSGetCurrentDataRow Is Nothing) Then
        grvMatr.FocusedColumn = mma_matric
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grvMatr_NTSFocusedColumnChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs) Handles grvMatr.NTSFocusedColumnChanged
    Try
      AbilitaCellaMatricola()
      '--------------------------------------------------------------------------------------------------------------
      If oCleMatr.bAutoAggiornaRighe = False Then Return
      '--------------------------------------------------------------------------------------------------------------
      If grvMatr.NTSGetCurrentDataRow Is Nothing Then
        grvMatr.FocusedColumn = mma_matric
      Else
        If NTSCStr(grvMatr.GetFocusedRowCellValue(mma_matric)) = "" Then grvMatr.FocusedColumn = mma_matric
      End If
      '--------------------------------------------------------------------------------------------------------------
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub grMatr_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grMatr.Enter
    Try
      AbilitaCellaMatricola()
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
      dRes = grvMatr.NTSSalvaRigaCorrente(dcMatr, oCleMatr.RecordIsChanged, False)
      Select Case dRes
        Case System.Windows.Forms.DialogResult.Yes
          'salvo
          '-------------------------------------------------
          'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
          If GctlControllaOutNotEqual() = False Then Return False

          If Not oCleMatr.Salva(False) Then
            Return False
          End If
        Case System.Windows.Forms.DialogResult.No
          'ripristino
          oCleMatr.Ripristina(dcMatr.Position, dcMatr.Filter)
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

  Public Overridable Function AbilitaCellaMatricola() As Boolean
    Try
      '--------------------------------------
      'bug di lettura barcode: se sono su una nuova riga ed il cursore non è dentro alla cella 
      '(quindi cella con focus ma fursore non lampeggiante)
      'se sparo sul barcode, a volte inferte il primo con secondo carattere del barcode
      'SU CUBE NON SERVE: IL PROBLEMA NON SI VERIFICA
      If oCleMatr Is Nothing Then Return True
      If bInAbilitaCellaEnter Then Return True
      If oCleMatr.bCodartDaBarcode Then
        If Not grvMatr.FocusedColumn Is Nothing AndAlso grvMatr.FocusedColumn.Name = "mma_matric" _
           AndAlso mma_matric.Enabled AndAlso grvMatr.Enabled Then
          If NTSCStr(grvMatr.GetRowCellValue(grvMatr.FocusedRowHandle, "mma_matric")) = "" Then
            bInAbilitaCellaEnter = True
            grvMatr.ShowEditorByMouse()
            If Not grvMatr.ActiveEditor Is Nothing Then
              'grvRighe.ActiveEditor.SelectAll() 'non va!!!!
              NTSSendKeys.Send(0, "{Down}") 'faccio apparire il cursore del mouse
            End If
          End If
        End If
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      bInAbilitaCellaEnter = False
    End Try
  End Function
End Class
