Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__DITS
  Public oCleAnaz As CLE__ANAZ
  Public dsDits As DataSet
  Public oCallParams As CLE__CLDP
  Public dcDits As BindingSource = New BindingSource

  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbSalva As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbRipristina As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbZoom As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents lbAcs_gruspedi As NTSInformatica.NTSLabel
  Public WithEvents lbAcs_tipoamcont As NTSInformatica.NTSLabel
  Public WithEvents cbAcs_tipoamcont As NTSInformatica.NTSComboBox
  Public WithEvents ckAcs_cespint As NTSInformatica.NTSCheckBox
  Public WithEvents lbAcs_codpuman As NTSInformatica.NTSLabel
  Public WithEvents lbAcs_percman1 As NTSInformatica.NTSLabel
  Public WithEvents edAcs_percman1 As NTSInformatica.NTSTextBoxNum
  Public WithEvents lbAcs_percman2 As NTSInformatica.NTSLabel
  Public WithEvents edAcs_percman2 As NTSInformatica.NTSTextBoxNum

  Public Overridable Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__DITS))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbSalva = New NTSInformatica.NTSBarButtonItem
    Me.tlbRipristina = New NTSInformatica.NTSBarButtonItem
    Me.tlbZoom = New NTSInformatica.NTSBarButtonItem
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbAcs_gruspedi = New NTSInformatica.NTSLabel
    Me.lbAcs_tipoamcont = New NTSInformatica.NTSLabel
    Me.cbAcs_tipoamcont = New NTSInformatica.NTSComboBox
    Me.ckAcs_cespint = New NTSInformatica.NTSCheckBox
    Me.lbAcs_codpuman = New NTSInformatica.NTSLabel
    Me.lbAcs_percman1 = New NTSInformatica.NTSLabel
    Me.edAcs_percman1 = New NTSInformatica.NTSTextBoxNum
    Me.lbAcs_percman2 = New NTSInformatica.NTSLabel
    Me.edAcs_percman2 = New NTSInformatica.NTSTextBoxNum
    Me.lbXx_codpuman = New NTSInformatica.NTSLabel
    Me.lbXx_gruspedi = New NTSInformatica.NTSLabel
    Me.edAcs_codpuman = New NTSInformatica.NTSTextBoxStr
    Me.edAcs_gruspedi = New NTSInformatica.NTSTextBoxStr
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbAcs_tipoamcont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckAcs_cespint.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcs_percman1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcs_percman2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcs_codpuman.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edAcs_gruspedi.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'frmPopup
    '
    Me.frmPopup.Appearance.BackColor = System.Drawing.SystemColors.Info
    Me.frmPopup.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
    Me.frmPopup.Appearance.Options.UseBackColor = True
    Me.frmPopup.Appearance.Options.UseImage = True
    '
    'DevXDefaultLookAndFeel
    '
    
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbSalva, Me.tlbRipristina, Me.tlbZoom, Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 26
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbSalva), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbRipristina), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbZoom), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbSalva
    '
    Me.tlbSalva.Caption = "Salva"
    Me.tlbSalva.Glyph = CType(resources.GetObject("tlbSalva.Glyph"), System.Drawing.Image)
    Me.tlbSalva.Id = 1
    Me.tlbSalva.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F9)
    Me.tlbSalva.Name = "tlbSalva"
    Me.tlbSalva.Visible = True
    '
    'tlbRipristina
    '
    Me.tlbRipristina.Caption = "Ripristina"
    Me.tlbRipristina.Glyph = CType(resources.GetObject("tlbRipristina.Glyph"), System.Drawing.Image)
    Me.tlbRipristina.Id = 2
    Me.tlbRipristina.ItemShortcut = New DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F8)
    Me.tlbRipristina.Name = "tlbRipristina"
    Me.tlbRipristina.Visible = True
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
    'lbAcs_gruspedi
    '
    Me.lbAcs_gruspedi.AutoSize = True
    Me.lbAcs_gruspedi.BackColor = System.Drawing.Color.Transparent
    Me.lbAcs_gruspedi.Location = New System.Drawing.Point(10, 97)
    Me.lbAcs_gruspedi.Name = "lbAcs_gruspedi"
    Me.lbAcs_gruspedi.NTSDbField = ""
    Me.lbAcs_gruspedi.Size = New System.Drawing.Size(109, 13)
    Me.lbAcs_gruspedi.TabIndex = 18
    Me.lbAcs_gruspedi.Text = "Gruppo/specie cespiti"
    '
    'lbAcs_tipoamcont
    '
    Me.lbAcs_tipoamcont.AutoSize = True
    Me.lbAcs_tipoamcont.BackColor = System.Drawing.Color.Transparent
    Me.lbAcs_tipoamcont.Location = New System.Drawing.Point(9, 126)
    Me.lbAcs_tipoamcont.Name = "lbAcs_tipoamcont"
    Me.lbAcs_tipoamcont.NTSDbField = ""
    Me.lbAcs_tipoamcont.Size = New System.Drawing.Size(171, 13)
    Me.lbAcs_tipoamcont.TabIndex = 22
    Me.lbAcs_tipoamcont.Text = "Tipo ammortam. da contabilizzare:"
    '
    'cbAcs_tipoamcont
    '
    Me.cbAcs_tipoamcont.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbAcs_tipoamcont.DataSource = Nothing
    Me.cbAcs_tipoamcont.DisplayMember = ""
    Me.cbAcs_tipoamcont.Location = New System.Drawing.Point(277, 123)
    Me.cbAcs_tipoamcont.Name = "cbAcs_tipoamcont"
    Me.cbAcs_tipoamcont.NTSDbField = ""
    Me.cbAcs_tipoamcont.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbAcs_tipoamcont.Properties.DropDownRows = 30
    Me.cbAcs_tipoamcont.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbAcs_tipoamcont.SelectedValue = ""
    Me.cbAcs_tipoamcont.Size = New System.Drawing.Size(215, 20)
    Me.cbAcs_tipoamcont.TabIndex = 512
    Me.cbAcs_tipoamcont.ValueMember = ""
    '
    'ckAcs_cespint
    '
    Me.ckAcs_cespint.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckAcs_cespint.Location = New System.Drawing.Point(12, 41)
    Me.ckAcs_cespint.Name = "ckAcs_cespint"
    Me.ckAcs_cespint.NTSCheckValue = "S"
    Me.ckAcs_cespint.NTSUnCheckValue = "N"
    Me.ckAcs_cespint.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckAcs_cespint.Properties.Appearance.Options.UseBackColor = True
    Me.ckAcs_cespint.Properties.Caption = "Gestione cespiti integrata con Contabilità Generale"
    Me.ckAcs_cespint.Size = New System.Drawing.Size(270, 18)
    Me.ckAcs_cespint.TabIndex = 513
    '
    'lbAcs_codpuman
    '
    Me.lbAcs_codpuman.AutoSize = True
    Me.lbAcs_codpuman.BackColor = System.Drawing.Color.Transparent
    Me.lbAcs_codpuman.Location = New System.Drawing.Point(9, 74)
    Me.lbAcs_codpuman.Name = "lbAcs_codpuman"
    Me.lbAcs_codpuman.NTSDbField = ""
    Me.lbAcs_codpuman.Size = New System.Drawing.Size(180, 13)
    Me.lbAcs_codpuman.TabIndex = 24
    Me.lbAcs_codpuman.Text = "Gruppo/specie/punto manut.e ripar."
    '
    'lbAcs_percman1
    '
    Me.lbAcs_percman1.AutoSize = True
    Me.lbAcs_percman1.BackColor = System.Drawing.Color.Transparent
    Me.lbAcs_percman1.Location = New System.Drawing.Point(9, 152)
    Me.lbAcs_percman1.Name = "lbAcs_percman1"
    Me.lbAcs_percman1.NTSDbField = ""
    Me.lbAcs_percman1.Size = New System.Drawing.Size(248, 13)
    Me.lbAcs_percman1.TabIndex = 27
    Me.lbAcs_percman1.Text = "Percentuali per manutenzioni/riparazioni:  prima %"
    '
    'edAcs_percman1
    '
    Me.edAcs_percman1.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcs_percman1.EditValue = "0"
    Me.edAcs_percman1.Location = New System.Drawing.Point(277, 149)
    Me.edAcs_percman1.Name = "edAcs_percman1"
    Me.edAcs_percman1.NTSDbField = ""
    Me.edAcs_percman1.NTSFormat = "0"
    Me.edAcs_percman1.NTSForzaVisZoom = False
    Me.edAcs_percman1.NTSOldValue = ""
    Me.edAcs_percman1.Properties.Appearance.Options.UseTextOptions = True
    Me.edAcs_percman1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAcs_percman1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcs_percman1.Properties.MaxLength = 65536
    Me.edAcs_percman1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcs_percman1.Size = New System.Drawing.Size(84, 20)
    Me.edAcs_percman1.TabIndex = 517
    '
    'lbAcs_percman2
    '
    Me.lbAcs_percman2.AutoSize = True
    Me.lbAcs_percman2.BackColor = System.Drawing.Color.Transparent
    Me.lbAcs_percman2.Location = New System.Drawing.Point(196, 178)
    Me.lbAcs_percman2.Name = "lbAcs_percman2"
    Me.lbAcs_percman2.NTSDbField = ""
    Me.lbAcs_percman2.Size = New System.Drawing.Size(61, 13)
    Me.lbAcs_percman2.TabIndex = 28
    Me.lbAcs_percman2.Text = "seconda %"
    '
    'edAcs_percman2
    '
    Me.edAcs_percman2.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcs_percman2.EditValue = "0"
    Me.edAcs_percman2.Location = New System.Drawing.Point(277, 175)
    Me.edAcs_percman2.Name = "edAcs_percman2"
    Me.edAcs_percman2.NTSDbField = ""
    Me.edAcs_percman2.NTSFormat = "0"
    Me.edAcs_percman2.NTSForzaVisZoom = False
    Me.edAcs_percman2.NTSOldValue = ""
    Me.edAcs_percman2.Properties.Appearance.Options.UseTextOptions = True
    Me.edAcs_percman2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
    Me.edAcs_percman2.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcs_percman2.Properties.MaxLength = 65536
    Me.edAcs_percman2.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcs_percman2.Size = New System.Drawing.Size(84, 20)
    Me.edAcs_percman2.TabIndex = 518
    '
    'lbXx_codpuman
    '
    Me.lbXx_codpuman.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_codpuman.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_codpuman.Location = New System.Drawing.Point(277, 71)
    Me.lbXx_codpuman.Name = "lbXx_codpuman"
    Me.lbXx_codpuman.NTSDbField = ""
    Me.lbXx_codpuman.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_codpuman.TabIndex = 581
    Me.lbXx_codpuman.Text = "lbXx_codpuman"
    '
    'lbXx_gruspedi
    '
    Me.lbXx_gruspedi.BackColor = System.Drawing.Color.Transparent
    Me.lbXx_gruspedi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
    Me.lbXx_gruspedi.Location = New System.Drawing.Point(276, 97)
    Me.lbXx_gruspedi.Name = "lbXx_gruspedi"
    Me.lbXx_gruspedi.NTSDbField = ""
    Me.lbXx_gruspedi.Size = New System.Drawing.Size(215, 20)
    Me.lbXx_gruspedi.TabIndex = 582
    Me.lbXx_gruspedi.Text = "lbXx_gruspedi"
    '
    'edAcs_codpuman
    '
    Me.edAcs_codpuman.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcs_codpuman.Location = New System.Drawing.Point(195, 71)
    Me.edAcs_codpuman.Name = "edAcs_codpuman"
    Me.edAcs_codpuman.NTSDbField = ""
    Me.edAcs_codpuman.NTSForzaVisZoom = False
    Me.edAcs_codpuman.NTSOldValue = ""
    Me.edAcs_codpuman.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcs_codpuman.Properties.MaxLength = 65536
    Me.edAcs_codpuman.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcs_codpuman.Size = New System.Drawing.Size(76, 20)
    Me.edAcs_codpuman.TabIndex = 595
    '
    'edAcs_gruspedi
    '
    Me.edAcs_gruspedi.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAcs_gruspedi.Location = New System.Drawing.Point(195, 97)
    Me.edAcs_gruspedi.Name = "edAcs_gruspedi"
    Me.edAcs_gruspedi.NTSDbField = ""
    Me.edAcs_gruspedi.NTSForzaVisZoom = False
    Me.edAcs_gruspedi.NTSOldValue = ""
    Me.edAcs_gruspedi.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edAcs_gruspedi.Properties.MaxLength = 65536
    Me.edAcs_gruspedi.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAcs_gruspedi.Size = New System.Drawing.Size(76, 20)
    Me.edAcs_gruspedi.TabIndex = 596
    '
    'FRM__DITS
    '
    Me.ClientSize = New System.Drawing.Size(503, 202)
    Me.Controls.Add(Me.edAcs_gruspedi)
    Me.Controls.Add(Me.edAcs_codpuman)
    Me.Controls.Add(Me.lbXx_gruspedi)
    Me.Controls.Add(Me.lbXx_codpuman)
    Me.Controls.Add(Me.cbAcs_tipoamcont)
    Me.Controls.Add(Me.lbAcs_gruspedi)
    Me.Controls.Add(Me.lbAcs_percman1)
    Me.Controls.Add(Me.edAcs_percman1)
    Me.Controls.Add(Me.lbAcs_percman2)
    Me.Controls.Add(Me.lbAcs_codpuman)
    Me.Controls.Add(Me.edAcs_percman2)
    Me.Controls.Add(Me.ckAcs_cespint)
    Me.Controls.Add(Me.lbAcs_tipoamcont)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "FRM__DITS"
    Me.Text = "DATI AGGIUNTIVI CESPITI"
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbAcs_tipoamcont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckAcs_cespint.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcs_percman1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcs_percman2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcs_codpuman.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edAcs_gruspedi.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

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

    Return True
  End Function


  Public Overridable Sub InitEntity(ByRef cleAnaz As CLE__ANAZ)
    oCleAnaz = cleAnaz
    AddHandler oCleAnaz.RemoteEvent, AddressOf GestisciEventiEntity
  End Sub

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim i As Integer = 0
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbSalva.GlyphPath = (oApp.ChildImageDir & "\save.gif")
        tlbRipristina.GlyphPath = (oApp.ChildImageDir & "\restore.gif")
        tlbZoom.GlyphPath = (oApp.ChildImageDir & "\zoom.gif")
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'Ã¨ una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      Dim dttTipcont As New DataTable()
      dttTipcont.Columns.Add("cod", GetType(String))
      dttTipcont.Columns.Add("val", GetType(String))
      dttTipcont.Rows.Add(New Object() {"C", "Civilistici"})
      dttTipcont.Rows.Add(New Object() {"F", "Fiscali"})
      dttTipcont.AcceptChanges()
      cbAcs_tipoamcont.DataSource = dttTipcont
      cbAcs_tipoamcont.ValueMember = "cod"
      cbAcs_tipoamcont.DisplayMember = "val"

      '-------------------------------------------------
      'completo le informazioni dei controlli
      edAcs_percman1.NTSSetParam(oMenu, oApp.Tr(Me, 128647315491562500, "Prima % manutenzioni e riparazioni"), oApp.FormatSconti, 6, 0, 100)
      edAcs_percman2.NTSSetParam(oMenu, oApp.Tr(Me, 128647315545937500, "Seconda % manutenzioni e riparazioni"), oApp.FormatSconti, 6, 0, 100)
      cbAcs_tipoamcont.NTSSetParam(oApp.Tr(Me, 128647328760156250, "Tipo contabilizzazione ammortamenti"))
      edAcs_gruspedi.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128647328760312500, "Gruppo/specie cespiti"), tabspce, False)
      ckAcs_cespint.NTSSetParam(oMenu, oApp.Tr(Me, 128647328761406250, "Gestione cespiti integrata con Contabilità Generale"), "S", "N")
      edAcs_codpuman.NTSSetParamTabe(oMenu, oApp.Tr(Me, 128647328761562500, "Gruppo/specie/punto manut.e ripar."), tabpuce, False)

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
      'se i controlli erano giÃ  stati precedentemente collegati, li scollego
      NTSFormClearDataBinding(Me)

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      lbXx_gruspedi.NTSDbField = "ANADITACS.xx_gruspedi"
      lbXx_codpuman.NTSDbField = "ANADITACS.xx_codpuman"
      cbAcs_tipoamcont.NTSDbField = "ANADITACS.acs_tipoamcont"
      edAcs_gruspedi.NTSDbField = "ANADITACS.acs_gruspedi"
      edAcs_percman1.NTSDbField = "ANADITACS.acs_percman1"
      edAcs_percman2.NTSDbField = "ANADITACS.acs_percman2"
      ckAcs_cespint.NTSText.NTSDbField = "ANADITACS.acs_cespint"
      edAcs_codpuman.NTSDbField = "ANADITACS.acs_codpuman"

      '-------------------------------------------------
      'per agganciare al dataset i vari controlli
      NTSFormAddDataBinding(dcDits, Me)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#Region "Eventi Form"
  Public Overridable Sub FRM__DITS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      '-------------------------------------------------
      'predispongo i controlli
      InitControls()

      '-------------------------------------------------
      'leggo dal database i dati e collego il NTSBinding
      dsDits = oCleAnaz.dsShared
      dcDits.DataSource = dsDits.Tables("ANADITACS")
      dsDits.Tables("ANADITACS").AcceptChanges()

      '-------------------------------------------------
      'collego il BindingSource ai vari controlli 
      Bindcontrols()

      '-------------------------------------------------
      'sempre alla fine di questa funzione: applico le regole della gctl
      GctlSetRoules()

      '-------------------------------------------------
      'Disabilita il CheckBox relativo a
      'Gestione cespiti integrata con Contabilità Generale'
      'se per la ditta corrente esiste almeno un record nella tebella MOVCESP
      If oCleAnaz.CespitiMovimentati Then ckAcs_cespint.Enabled = False

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__DITS_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
    If Not Salva() Then e.Cancel = True
  End Sub

#End Region

#Region "Eventi Toolbar"
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

  Public Overridable Sub tlbRipristina_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbRipristina.ItemClick
    Dim bRemovBinding As Boolean = False
    Try
      Me.ValidaLastControl()          'se non valido il controllo su cui sono, quando modifico il controllo e, senza uscire, faccio 'ripristina' il controllo rimane sporco

      '-------------------------------------------------
      'ripristino la forma di pagamento
      Dim dlgRes As DialogResult
      If Not sender Is Nothing Then
        'chiamato facendo pressione sulla funzione 'ripristina'
        dlgRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129006946193842189, "Ripristinare le modifiche apportate?"))
      Else
        'chiamato dalla 'salva
        dlgRes = Windows.Forms.DialogResult.Yes
      End If

      Select Case dlgRes
        Case Windows.Forms.DialogResult.No
          Return
        Case Windows.Forms.DialogResult.Yes
          If dsDits.Tables("ANADITACS").Rows.Count = 1 And dsDits.Tables("ANADITACS").Rows(0).RowState = DataRowState.Added Then
            bRemovBinding = True
            NTSFormClearDataBinding(Me)
          End If

          oCleAnaz.DitsRipristina(dcDits.Position, dcDits.Filter)
      End Select

    Catch ex As Exception
      If bRemovBinding Then NTSFormAddDataBinding(dcDits, Me)
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbZoom_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbZoom.ItemClick
    Try
      'per eventuali altri controlli caricati al volo
      Dim ctrlTmp As Control = NTSFindControlForZoom()
      If ctrlTmp Is Nothing Then Return
      Dim oParam As New CLE__PATB

      '------------------------------------
      'zoom standard di textbox e griglia
      NTSCallStandardZoom()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.SendWait("{F1}")
  End Sub

  Public Overridable Sub tlbDits_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Function Salva() As Boolean
    Dim dRes As DialogResult
    Try
      '-------------------------------------------------
      'chiedo conferma e, se necessario, salvo
      Me.ValidaLastControl()      'valido l'ultimo controllo che ha il focus

      If oCleAnaz.DitsRecordIsChanged Then
        '-------------------------------------------------
        'controllo che i campi abbiano un valore diverso da quello impostato in GCTL.OutNotEqual
        If GctlControllaOutNotEqual() = False Then Return False

        dRes = oApp.MsgBoxInfoYesNo_DefYes(oApp.Tr(Me, 129006946335877075, "Confermi il salvataggio?"))
        If dRes = System.Windows.Forms.DialogResult.Cancel Then Return False
        If dRes = System.Windows.Forms.DialogResult.Yes Then
          If Not oCleAnaz.DitsSalva(False) Then Return False
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

End Class

