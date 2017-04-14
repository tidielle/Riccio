Imports System.Data
Imports NTSInformatica
Imports NTSInformatica.CLN__STD

Public Class FRM__HHLC

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

#Region "Variabili"
    Public strNomeTabella As String = "TABHHLC"

    Public oCleHHLC As CLE__HHLC

    Public dsHHVE As DataSet
    Public dcHHLC As BindingSource = New BindingSource()

    Public oCallParams As CLE__CLDP

    Private components As System.ComponentModel.IContainer
    Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
    Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Public WithEvents tlbMain As NTSInformatica.NTSBar
    Public WithEvents tlbNuovo As NTSInformatica.NTSBarButtonItem
    Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
    Public WithEvents tlbCancella As NTSInformatica.NTSBarButtonItem
    Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
    Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
    Public WithEvents tlbStrumenti As NTSInformatica.NTSBarSubItem
    Public WithEvents tlbImpostaStampante As NTSInformatica.NTSBarButtonItem
    Public WithEvents tlbStampa As NTSInformatica.NTSBarButtonItem
    Public WithEvents tlbStampaVideo As NTSInformatica.NTSBarButtonItem
    Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
    Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
    Public WithEvents pnCiva As NTSInformatica.NTSPanel
    Public WithEvents grCLC As NTSInformatica.NTSGrid
    Public WithEvents grvHHLC As NTSInformatica.NTSGridView
    Public WithEvents tb_codhhlc As NTSInformatica.NTSGridColumn
    Friend WithEvents tb_prezzo As NTSGridColumn
    Friend WithEvents GridColumn1_56_4 As NTSGridColumn
    Friend WithEvents tb_deshhlc As NTSGridColumn
    Friend WithEvents GridColumn1_409_5 As NTSGridColumn
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

        InitializeComponent()
        Me.MinimumSize = Me.Size

        '------------------------------------------------
        'Creo e attivo l'entity, in più inizializzo la funzione che dovrà rilevare gli eventi dall'ENTITY stesso
        Dim strErr As String = ""
        Dim objTmp As Object = Nothing
        If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__HHlc", "BE__HHlc", objTmp, strErr, False, "", "") = False Then
            oApp.MsgBoxErr(oApp.Tr(Me, 128182574024687500, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
            Return False
        End If
        oCleHHLC = CType(objTmp, CLE__HHLC)
        '------------------------------------------------
        bRemoting = Menu.Remoting("BN__HHlc", strRemoteServer, strRemotePort)
        AddHandler oCleHHLC.RemoteEvent, AddressOf GestisciEventiEntity
        If oCleHHLC.Init(oApp, NTSScript, oMenu.oCleComm, strNomeTabella, bRemoting, strRemoteServer, strRemotePort) = False Then Return False

        Return True
    End Function
    Public Overridable Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__HHLC))
        Me.NtsBarManager1 = New NTSInformatica.NTSBarManager()
        Me.tlbMain = New NTSInformatica.NTSBar()
        Me.tlbNuovo = New NTSInformatica.NTSBarButtonItem()
        Me.tlbSalva = New NTSInformatica.NTSBarButtonItem()
        Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem()
        Me.tlbCancella = New NTSInformatica.NTSBarButtonItem()
        Me.tlbZoom = New NTSInformatica.NTSBarButtonItem()
        Me.tlbStrumenti = New NTSInformatica.NTSBarSubItem()
        Me.tlbImpostaStampante = New NTSInformatica.NTSBarButtonItem()
        Me.tlbStampa = New NTSInformatica.NTSBarButtonItem()
        Me.tlbStampaVideo = New NTSInformatica.NTSBarButtonItem()
        Me.tlbGuida = New NTSInformatica.NTSBarButtonItem()
        Me.tlbEsci = New NTSInformatica.NTSBarButtonItem()
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.pnCiva = New NTSInformatica.NTSPanel()
        Me.grCLC = New NTSInformatica.NTSGrid()
        Me.grvHHLC = New NTSInformatica.NTSGridView()
        Me.tb_deshhlc = New NTSInformatica.NTSGridColumn()
        Me.tb_codhhlc = New NTSInformatica.NTSGridColumn()
        Me.tb_prezzo = New NTSInformatica.NTSGridColumn()
        CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pnCiva, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnCiva.SuspendLayout()
        CType(Me.grCLC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grvHHLC, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbNuovo, Me.tlbSalva, Me.tlbCancella, Me.tlbRipristina, Me.tlbZoom, Me.tlbStrumenti, Me.tlbImpostaStampante, Me.tlbStampa, Me.tlbStampaVideo, Me.tlbGuida, Me.tlbEsci})
        Me.NtsBarManager1.MaxItemId = 15
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
        'tlbStrumenti
        '
        Me.tlbStrumenti.Caption = "Strumenti"
        Me.tlbStrumenti.Glyph = CType(resources.GetObject("tlbStrumenti.Glyph"), System.Drawing.Image)
        Me.tlbStrumenti.GlyphPath = ""
        Me.tlbStrumenti.Id = 9
        Me.tlbStrumenti.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbImpostaStampante)})
        Me.tlbStrumenti.Name = "tlbStrumenti"
        Me.tlbStrumenti.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionInMenu
        Me.tlbStrumenti.Visible = True
        '
        'tlbImpostaStampante
        '
        Me.tlbImpostaStampante.Caption = "Imposta stampante"
        Me.tlbImpostaStampante.GlyphPath = ""
        Me.tlbImpostaStampante.Id = 10
        Me.tlbImpostaStampante.Name = "tlbImpostaStampante"
        Me.tlbImpostaStampante.Visible = True
        '
        'tlbStampa
        '
        Me.tlbStampa.Caption = "Stampa"
        Me.tlbStampa.Glyph = CType(resources.GetObject("tlbStampa.Glyph"), System.Drawing.Image)
        Me.tlbStampa.GlyphPath = ""
        Me.tlbStampa.Id = 11
        Me.tlbStampa.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F6)
        Me.tlbStampa.Name = "tlbStampa"
        Me.tlbStampa.Visible = True
        '
        'tlbStampaVideo
        '
        Me.tlbStampaVideo.Caption = "Stampa a video"
        Me.tlbStampaVideo.Glyph = CType(resources.GetObject("tlbStampaVideo.Glyph"), System.Drawing.Image)
        Me.tlbStampaVideo.GlyphPath = ""
        Me.tlbStampaVideo.Id = 12
        Me.tlbStampaVideo.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F12)
        Me.tlbStampaVideo.Name = "tlbStampaVideo"
        Me.tlbStampaVideo.Visible = True
        '
        'tlbGuida
        '
        Me.tlbGuida.Caption = "Guida"
        Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
        Me.tlbGuida.GlyphPath = ""
        Me.tlbGuida.Id = 13
        Me.tlbGuida.Name = "tlbGuida"
        Me.tlbGuida.Visible = True
        '
        'tlbEsci
        '
        Me.tlbEsci.Caption = "Esci"
        Me.tlbEsci.Glyph = CType(resources.GetObject("tlbEsci.Glyph"), System.Drawing.Image)
        Me.tlbEsci.GlyphPath = ""
        Me.tlbEsci.Id = 14
        Me.tlbEsci.Name = "tlbEsci"
        Me.tlbEsci.Visible = True
        '
        'barDockControlTop
        '
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(21, 14, 21, 14)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(21, 14, 21, 14)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(21, 14, 21, 14)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(21, 14, 21, 14)
        '
        'pnCiva
        '
        Me.pnCiva.AllowDrop = True
        Me.pnCiva.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.pnCiva.Appearance.Options.UseBackColor = True
        Me.pnCiva.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnCiva.Controls.Add(Me.grCLC)
        Me.pnCiva.Cursor = System.Windows.Forms.Cursors.Default
        Me.pnCiva.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnCiva.Location = New System.Drawing.Point(0, 30)
        Me.pnCiva.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
        Me.pnCiva.LookAndFeel.UseDefaultLookAndFeel = False
        Me.pnCiva.Margin = New System.Windows.Forms.Padding(21, 14, 21, 14)
        Me.pnCiva.Name = "pnCiva"
        Me.pnCiva.NTSActiveTrasparency = True
        Me.pnCiva.Size = New System.Drawing.Size(1914, 1015)
        Me.pnCiva.TabIndex = 0
        Me.pnCiva.Text = "pnCiva"
        '
        'grCLC
        '
        Me.grCLC.Dock = System.Windows.Forms.DockStyle.Fill
        '
        '
        '
        Me.grCLC.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(21, 14, 21, 14)
        Me.grCLC.EmbeddedNavigator.Name = ""
        Me.grCLC.Location = New System.Drawing.Point(0, 0)
        Me.grCLC.MainView = Me.grvHHLC
        Me.grCLC.Margin = New System.Windows.Forms.Padding(21, 14, 21, 14)
        Me.grCLC.Name = "grCLC"
        Me.grCLC.Size = New System.Drawing.Size(1914, 1015)
        Me.grCLC.TabIndex = 1
        Me.grCLC.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.grvHHLC})
        '
        'grvHHLC
        '
        Me.grvHHLC.ActiveFilterEnabled = False
        '
        'tb_deshhlc
        '
        Me.tb_deshhlc.AppearanceCell.Options.UseBackColor = True
        Me.tb_deshhlc.AppearanceCell.Options.UseTextOptions = True
        Me.tb_deshhlc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.tb_deshhlc.Caption = "Descrizione"
        Me.tb_deshhlc.Enabled = True
        Me.tb_deshhlc.Name = "tb_deshhlc"
        Me.tb_deshhlc.NTSRepositoryComboBox = Nothing
        Me.tb_deshhlc.NTSRepositoryItemCheck = Nothing
        Me.tb_deshhlc.NTSRepositoryItemMemo = Nothing
        Me.tb_deshhlc.NTSRepositoryItemText = Nothing
        Me.tb_deshhlc.Visible = True
        Me.tb_deshhlc.VisibleIndex = 1
        Me.grvHHLC.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.tb_codhhlc, Me.tb_deshhlc, Me.tb_prezzo})
        Me.grvHHLC.CustomizationFormBounds = New System.Drawing.Rectangle(680, 326, 208, 170)
        Me.grvHHLC.Enabled = True
        Me.grvHHLC.GridControl = Me.grCLC
        Me.grvHHLC.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden
        Me.grvHHLC.MinRowHeight = 14
        Me.grvHHLC.Name = "grvHHLC"
        Me.grvHHLC.NTSAllowDelete = True
        Me.grvHHLC.NTSAllowInsert = True
        Me.grvHHLC.NTSAllowUpdate = True
        Me.grvHHLC.NTSMenuContext = Nothing
        Me.grvHHLC.OptionsCustomization.AllowRowSizing = True
        Me.grvHHLC.OptionsFilter.AllowFilterEditor = False
        Me.grvHHLC.OptionsNavigation.EnterMoveNextColumn = True
        Me.grvHHLC.OptionsNavigation.UseTabKey = False
        Me.grvHHLC.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.grvHHLC.OptionsView.ColumnAutoWidth = False
        Me.grvHHLC.OptionsView.EnableAppearanceEvenRow = True
        Me.grvHHLC.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom
        Me.grvHHLC.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
        Me.grvHHLC.OptionsView.ShowGroupPanel = False
        Me.grvHHLC.RowHeight = 16
        '
        'tb_codhhlc
        '
        Me.tb_codhhlc.AppearanceCell.Options.UseBackColor = True
        Me.tb_codhhlc.AppearanceCell.Options.UseTextOptions = True
        Me.tb_codhhlc.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.tb_codhhlc.AppearanceHeader.Options.UseTextOptions = True
        Me.tb_codhhlc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.tb_codhhlc.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.tb_codhhlc.Caption = "Codice"
        Me.tb_codhhlc.Enabled = True
        Me.tb_codhhlc.FieldName = "tb_codhhlc"
        Me.tb_codhhlc.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
        Me.tb_codhhlc.Name = "tb_codhhlc"
        Me.tb_codhhlc.NTSRepositoryComboBox = Nothing
        Me.tb_codhhlc.NTSRepositoryItemCheck = Nothing
        Me.tb_codhhlc.NTSRepositoryItemMemo = Nothing
        Me.tb_codhhlc.NTSRepositoryItemText = Nothing
        Me.tb_codhhlc.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.tb_codhhlc.OptionsFilter.AllowFilter = False
        Me.tb_codhhlc.Visible = True
        Me.tb_codhhlc.VisibleIndex = 0
        '
        'tb_prezzo
        '
        Me.tb_prezzo.AppearanceCell.Options.UseBackColor = True
        Me.tb_prezzo.AppearanceCell.Options.UseTextOptions = True
        Me.tb_prezzo.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
        Me.tb_prezzo.Caption = "Prezzo"
        Me.tb_prezzo.Enabled = True
        Me.tb_prezzo.FieldName = "tb_prezzo"
        Me.tb_prezzo.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText
        Me.tb_prezzo.Name = "tb_prezzo"
        Me.tb_prezzo.NTSRepositoryComboBox = Nothing
        Me.tb_prezzo.NTSRepositoryItemCheck = Nothing
        Me.tb_prezzo.NTSRepositoryItemMemo = Nothing
        Me.tb_prezzo.NTSRepositoryItemText = Nothing
        Me.tb_prezzo.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.[False]
        Me.tb_prezzo.OptionsFilter.AllowFilter = False
        Me.tb_prezzo.Visible = True
        Me.tb_prezzo.VisibleIndex = 2
        '
        'FRM__HHLC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.ClientSize = New System.Drawing.Size(1914, 1045)
        Me.Controls.Add(Me.pnCiva)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.HelpButton = True
        Me.Margin = New System.Windows.Forms.Padding(21, 14, 21, 14)
        Me.Name = "FRM__HHLC"
        Me.NTSLastControlFocussed = Me.grCLC
        Me.Text = "GESTIONE VEICOLI"
        CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pnCiva, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnCiva.ResumeLayout(False)
        CType(Me.grCLC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grvHHLC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#Region "Form"
    Public Overridable Sub FRM__CIVA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Predispongo i controlli
            InitControls()

            '-------------------------------------------------
            'Leggo dal database i dati e collego l'NTSBinding
            If oCleHHLC.Apri(DittaCorrente, dsHHVE) Then
                dcHHLC.DataSource = dsHHVE.Tables(strNomeTabella)
                dsHHVE.AcceptChanges()
                grCLC.DataSource = dcHHLC

                GctlSetRoules()

                '--------------------------------------------
                'sono stato chiamato da un altro child: mi posiziono sul record passatomi, se lo trovo
                'If Not oCallParams Is Nothing Then
                '    If Microsoft.VisualBasic.Left(oCallParams.strParam, 5) = "NUOV;" Then
                '        If grvHHVE.NTSAllowInsert Then
                '            grvHHVE.NTSNuovo()
                '        End If
                '    ElseIf Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) <> "" Then
                '        For i As Integer = 0 To dcHHLC.List.Count - 1
                '            If CType(dcHHLC.Item(i), DataRowView)!tb_codciva.ToString = Microsoft.VisualBasic.Mid(oCallParams.strParam, 6) Then
                '                dcHHLC.Position = i
                '                Exit For
                '            End If
                '        Next
                '    End If
                'End If  'If Not oCallParams Is Nothing Then
            Else
                Me.Close()
            End If

        Catch ex As Exception
            '-------------------------------------------------
            Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
            '-------------------------------------------------
        End Try
    End Sub
    Public Overridable Sub InitControls()
        InitControlsBeginEndInit(Me, False)
        Try
            'Carico le immagini della Toolbar
            Try
                tlbNuovo.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\new.gif")
                tlbSalva.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\save.gif")
                tlbCancella.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\delete.gif")
                tlbRipristina.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\restore.gif")
                tlbZoom.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\zoom.gif")
                tlbStrumenti.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\options.gif")
                tlbStampa.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\print.gif")
                tlbStampaVideo.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\prnscreen.gif")
                tlbGuida.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\help.gif")
                tlbEsci.Glyph = Bitmap.FromFile(oApp.ChildImageDir & "\exit.gif")
            Catch ex As Exception
                'Non gestisco l'errore: 
                'se non c'è una immagine prendo quella a design
            End Try
            tlbMain.NTSSetToolTip()

            grvHHLC.NTSSetParam(oMenu, oApp.Tr(Me, 128230023372468841, "Griglia Veicoli"))

            tb_codhhlc.NTSSetParamNUM(oMenu, oApp.Tr(Me, 128230023372625014, "Codice"), "0", 4, 0, 9999)
            tb_deshhlc.NTSSetParamSTR(oMenu, oApp.Tr(Me, 128230023372781187, "Descrizione"), 80, False)
            tb_prezzo.NTSSetParamNUM(oMenu, "Prezzo", "#.##0.00", 9999)
            tb_deshhlc.NTSSetRichiesto()

            'Dim dttComboEuro As New DataTable
            'dttComboEuro.Columns.Add("valDB", GetType(String))
            'dttComboEuro.Columns.Add("valUser", GetType(String))
            'dttComboEuro.Rows.Add(New Object() {"N", "Non definito"})
            'dttComboEuro.Rows.Add(New Object() {"0", "Euro 0"})
            'dttComboEuro.Rows.Add(New Object() {"1", "Euro 1"})
            'dttComboEuro.Rows.Add(New Object() {"2", "Euro 2"})


            'tb_codart.NTSSetParamSTR(oMenu, "Codice Articolo", CLN__STD.CodartMaxLen, True)
            'tb_codart.NTSSetParamZoom("ZOOMARTICO")

            '.NTSSetParamNUMTabe(
            '.NTSSetParamZoom(
            '.NTSSetRichiesto(

            'Chiamo lo script per inizializzare i controlli caricati con Source Extender
            NTSScriptExec("InitControls", Me, Nothing)

        Catch ex As Exception
            '-------------------------------------------------
            Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
            '-------------------------------------------------
        End Try
        InitControlsBeginEndInit(Me, True)
    End Sub

    Public Overridable Sub FRM__CIVA_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If Not Salva() Then e.Cancel = True
        Catch ex As Exception
            '-------------------------------------------------
            Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
            '-------------------------------------------------
        End Try
    End Sub
#End Region

#Region "Griglia"

    Public Overridable Sub grvCiva_NTSFocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvHHLC.NTSFocusedRowChanged
        Try
            'Se il codice è diverso da 0 blocco la colonna, diversamente la rendo editabile
            If NTSCInt(grvHHLC.GetFocusedRowCellValue(tb_codhhlc).ToString.Trim) <> 0 Then
                tb_codhhlc.Enabled = False
            Else
                GctlSetVisEnab(tb_codhhlc, False)
            End If
        Catch ex As Exception
            '-------------------------------------------------
            Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
            '-------------------------------------------------
        End Try
    End Sub

    Public Overridable Sub grvCiva_NTSBeforeRowUpdate(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowAllowEventArgs) Handles grvHHLC.NTSBeforeRowUpdate
        Try
            If Not Salva() Then
                'Rimango sulla riga su cui sono
                e.Allow = False
            End If
        Catch ex As Exception
            '-------------------------------------------------
            Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
            '-------------------------------------------------
        End Try
    End Sub

#End Region

#Region "Toolbar"

    Public Overridable Sub tlbNuovo_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbNuovo.ItemClick
        Try
            grvHHLC.NTSNuovo()
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
            If grvHHLC.NTSGetCurrentDataRow Is Nothing Then Return

            If grvHHLC.NTSDeleteRigaCorrente(dcHHLC, True) Then oCleHHLC.Salva(True)
        Catch ex As Exception
            '-------------------------------------------------
            Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
            '-------------------------------------------------
        End Try
    End Sub

    Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
        Try
            If grvHHLC.NTSRipristinaRigaCorrenteBefore(dcHHLC, CType(IIf(sender Is Nothing, False, True), Boolean)) Then
                oCleHHLC.Ripristina(dcHHLC.Position, dcHHLC.Filter)
                grvHHLC.NTSRipristinaRigaCorrenteAfter()
            End If
        Catch ex As Exception
            '-------------------------------------------------
            Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
            '-------------------------------------------------
        End Try
    End Sub

    Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
        Try
            NTSCallStandardZoom()
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

    Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
        SendKeys.Send("{F1}")
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

    Public Overridable Sub tlbImpostaStampante_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbImpostaStampante.ItemClick
        Try
            oMenu.ReportImposta(Me)
        Catch ex As Exception
            '-------------------------------------------------
            Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
            '-------------------------------------------------
        End Try
    End Sub

#End Region

    Public Overridable Function Salva() As Boolean
        Try

            Me.ValidaLastControl()      'Valido l'ultimo controllo che ha il focus

            Select Case grvHHLC.NTSSalvaRigaCorrente(dcHHLC, oCleHHLC.RecordIsChanged, False)
                Case System.Windows.Forms.DialogResult.Yes
                    'Salvo
                    '-------------------------------------------------
                    'Controllo che i campi abbiano un valore diverso da quello 
                    'impostato in GCTL.OutNotEqual
                    If GctlControllaOutNotEqual() = False Then Return False

                    If Not oCleHHLC.Salva(False) Then
                        Return False
                    End If
                Case System.Windows.Forms.DialogResult.No
                    'Ripristino
                    tlbRipristina_ItemClick(Nothing, Nothing)
                Case System.Windows.Forms.DialogResult.Cancel
                    'Non posso fare nulla
                    Return False
                Case System.Windows.Forms.DialogResult.Abort
                    'La riga non ha subito modifiche
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

            If Not Salva() Then Return

            '--------------------------------------------------
            'Preparo il motore di stampa
            strCrpe = ""
            nPjob = oMenu.ReportPEInit(oApp.Ditta, Me, "BS--CIVA", "Reports1", " ", 0, nDestin, "BS--CIVA.RPT", False, "Codici iva", False)
            If nPjob Is Nothing Then Return

            '--------------------------------------------------
            'Lancio tutti gli eventuali reports (gestisce già il multireport)
            For i = 1 To UBound(CType(nPjob, Array), 2)
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
