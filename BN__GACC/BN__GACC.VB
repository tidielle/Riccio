Imports System.Data
Imports NTSInformatica.CLN__STD

Public Class FRM__GACC
#Region "Moduli"
  Private Moduli_P As Integer = bsModAll
  Private ModuliExt_P As Integer = 0
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

  Public oCleGacc As CLE__GACC
  Public oCallParams As CLE__CLDP
  Public oColoreBloccoOperat As Color = Color.Red
  Public oColoreBloccoRuoli As Color = Color.Blue

#Region "Definizione controlli"
  Private components As System.ComponentModel.IContainer
  Public WithEvents NtsBarManager1 As NTSInformatica.NTSBarManager
  Public WithEvents tlbMain As NTSInformatica.NTSBar
  Public WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
  Public WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
  Public WithEvents tlbGuida As NTSInformatica.NTSBarButtonItem
  Public WithEvents tlbEsci As NTSInformatica.NTSBarButtonItem
  Public WithEvents pnCorpo As NTSInformatica.NTSPanel
  Public WithEvents pnThree As NTSInformatica.NTSPanel
  Public WithEvents trGacc As NTSInformatica.NTSTreeView
  Public WithEvents cbModo As NTSInformatica.NTSComboBox
  Public WithEvents lbModperat As NTSInformatica.NTSLabel
  Public WithEvents edFiltro As NTSInformatica.NTSTextBoxStr
  Public WithEvents lbFiltro As NTSInformatica.NTSLabel
  Public WithEvents cmdProcedi As NTSInformatica.NTSButton
  Public WithEvents cmdOperatore As NTSInformatica.NTSButton
  Public WithEvents cmdGruppoOperatori As NTSInformatica.NTSButton
  Public WithEvents cmdBloccaSbloccaRuolo As NTSInformatica.NTSButton
  Public WithEvents cmdBloccaSbloccaOperat As NTSInformatica.NTSButton
  Public WithEvents lbNotaBlu As NTSInformatica.NTSLabel
  Public WithEvents lbNotaRossa As NTSInformatica.NTSLabel
  Public WithEvents cmdGron As NTSInformatica.NTSButton
  Public WithEvents cmdAccc As NTSInformatica.NTSButton
  Public WithEvents lbRiga As NTSInformatica.NTSLabel
  Public WithEvents cmdBloccaTutto As NTSInformatica.NTSButton
  Public WithEvents cmdSbloccaTutto As NTSInformatica.NTSButton
  Public WithEvents ImageList1 As System.Windows.Forms.ImageList

#End Region

  Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__GACC))
    Me.NtsBarManager1 = New NTSInformatica.NTSBarManager
    Me.tlbMain = New NTSInformatica.NTSBar
    Me.tlbGuida = New NTSInformatica.NTSBarButtonItem
    Me.tlbEsci = New NTSInformatica.NTSBarButtonItem
    Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl
    Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl
    Me.lbFiltro = New NTSInformatica.NTSLabel
    Me.cbModo = New NTSInformatica.NTSComboBox
    Me.lbModperat = New NTSInformatica.NTSLabel
    Me.edFiltro = New NTSInformatica.NTSTextBoxStr
    Me.pnThree = New NTSInformatica.NTSPanel
    Me.trGacc = New NTSInformatica.NTSTreeView
    Me.pnCorpo = New NTSInformatica.NTSPanel
    Me.cmdBloccaTutto = New NTSInformatica.NTSButton
    Me.cmdSbloccaTutto = New NTSInformatica.NTSButton
    Me.lbRiga = New NTSInformatica.NTSLabel
    Me.cmdAccc = New NTSInformatica.NTSButton
    Me.cmdGron = New NTSInformatica.NTSButton
    Me.lbNotaBlu = New NTSInformatica.NTSLabel
    Me.lbNotaRossa = New NTSInformatica.NTSLabel
    Me.cmdBloccaSbloccaOperat = New NTSInformatica.NTSButton
    Me.cmdProcedi = New NTSInformatica.NTSButton
    Me.cmdOperatore = New NTSInformatica.NTSButton
    Me.cmdGruppoOperatori = New NTSInformatica.NTSButton
    Me.cmdBloccaSbloccaRuolo = New NTSInformatica.NTSButton
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbModo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edFiltro.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnThree, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnThree.SuspendLayout()
    CType(Me.pnCorpo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCorpo.SuspendLayout()
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
    Me.NtsBarManager1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.tlbGuida, Me.tlbEsci})
    Me.NtsBarManager1.MaxItemId = 19
    '
    'tlbMain
    '
    Me.tlbMain.BarName = "tlbMain"
    Me.tlbMain.DockCol = 0
    Me.tlbMain.DockRow = 0
    Me.tlbMain.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
    Me.tlbMain.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.tlbGuida, True), New DevExpress.XtraBars.LinkPersistInfo(Me.tlbEsci)})
    Me.tlbMain.OptionsBar.AllowQuickCustomization = False
    Me.tlbMain.OptionsBar.DisableClose = True
    Me.tlbMain.OptionsBar.DrawDragBorder = False
    Me.tlbMain.OptionsBar.UseWholeRow = True
    Me.tlbMain.Text = "tlbMain"
    '
    'tlbGuida
    '
    Me.tlbGuida.Caption = "Guida"
    Me.tlbGuida.Glyph = CType(resources.GetObject("tlbGuida.Glyph"), System.Drawing.Image)
    Me.tlbGuida.GlyphPath = ""
    Me.tlbGuida.Id = 11
    Me.tlbGuida.Name = "tlbGuida"
    Me.tlbGuida.Visible = True
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
    'lbFiltro
    '
    Me.lbFiltro.AutoSize = True
    Me.lbFiltro.BackColor = System.Drawing.Color.Transparent
    Me.lbFiltro.Location = New System.Drawing.Point(9, 41)
    Me.lbFiltro.Name = "lbFiltro"
    Me.lbFiltro.NTSDbField = ""
    Me.lbFiltro.Size = New System.Drawing.Size(31, 13)
    Me.lbFiltro.TabIndex = 3
    Me.lbFiltro.Text = "Filtro"
    Me.lbFiltro.Tooltip = ""
    Me.lbFiltro.UseMnemonic = False
    '
    'cbModo
    '
    Me.cbModo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbModo.DataSource = Nothing
    Me.cbModo.DisplayMember = ""
    Me.cbModo.Location = New System.Drawing.Point(111, 12)
    Me.cbModo.Name = "cbModo"
    Me.cbModo.NTSDbField = ""
    Me.cbModo.Properties.AutoHeight = False
    Me.cbModo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbModo.Properties.DropDownRows = 30
    Me.cbModo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbModo.SelectedValue = ""
    Me.cbModo.Size = New System.Drawing.Size(210, 20)
    Me.cbModo.TabIndex = 2
    Me.cbModo.ValueMember = ""
    '
    'lbModperat
    '
    Me.lbModperat.AutoSize = True
    Me.lbModperat.BackColor = System.Drawing.Color.Transparent
    Me.lbModperat.Location = New System.Drawing.Point(9, 15)
    Me.lbModperat.Name = "lbModperat"
    Me.lbModperat.NTSDbField = ""
    Me.lbModperat.Size = New System.Drawing.Size(96, 13)
    Me.lbModperat.TabIndex = 1
    Me.lbModperat.Text = "Modalità operativa"
    Me.lbModperat.Tooltip = ""
    Me.lbModperat.UseMnemonic = False
    '
    'edFiltro
    '
    Me.edFiltro.Cursor = System.Windows.Forms.Cursors.Default
    Me.edFiltro.Location = New System.Drawing.Point(111, 38)
    Me.edFiltro.Name = "edFiltro"
    Me.edFiltro.NTSDbField = ""
    Me.edFiltro.NTSForzaVisZoom = False
    Me.edFiltro.NTSOldValue = ""
    Me.edFiltro.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edFiltro.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edFiltro.Properties.AutoHeight = False
    Me.edFiltro.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton})
    Me.edFiltro.Properties.MaxLength = 65536
    Me.edFiltro.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edFiltro.Size = New System.Drawing.Size(210, 20)
    Me.edFiltro.TabIndex = 0
    '
    'pnThree
    '
    Me.pnThree.AllowDrop = True
    Me.pnThree.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnThree.Appearance.Options.UseBackColor = True
    Me.pnThree.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnThree.Controls.Add(Me.trGacc)
    Me.pnThree.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnThree.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnThree.Location = New System.Drawing.Point(0, 26)
    Me.pnThree.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnThree.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnThree.Name = "pnThree"
    Me.pnThree.NTSActiveTrasparency = True
    Me.pnThree.Size = New System.Drawing.Size(318, 403)
    Me.pnThree.TabIndex = 5
    Me.pnThree.Text = "NtsPanel1"
    '
    'trGacc
    '
    Me.trGacc.Dock = System.Windows.Forms.DockStyle.Fill
    Me.trGacc.Location = New System.Drawing.Point(0, 0)
    Me.trGacc.Name = "trGacc"
    Me.trGacc.Size = New System.Drawing.Size(318, 403)
    Me.trGacc.TabIndex = 8
    '
    'pnCorpo
    '
    Me.pnCorpo.AllowDrop = True
    Me.pnCorpo.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCorpo.Appearance.Options.UseBackColor = True
    Me.pnCorpo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCorpo.Controls.Add(Me.cmdBloccaTutto)
    Me.pnCorpo.Controls.Add(Me.cmdSbloccaTutto)
    Me.pnCorpo.Controls.Add(Me.lbRiga)
    Me.pnCorpo.Controls.Add(Me.cmdAccc)
    Me.pnCorpo.Controls.Add(Me.cmdGron)
    Me.pnCorpo.Controls.Add(Me.lbNotaBlu)
    Me.pnCorpo.Controls.Add(Me.lbNotaRossa)
    Me.pnCorpo.Controls.Add(Me.cmdBloccaSbloccaOperat)
    Me.pnCorpo.Controls.Add(Me.cmdProcedi)
    Me.pnCorpo.Controls.Add(Me.cmdOperatore)
    Me.pnCorpo.Controls.Add(Me.cmdGruppoOperatori)
    Me.pnCorpo.Controls.Add(Me.lbModperat)
    Me.pnCorpo.Controls.Add(Me.cbModo)
    Me.pnCorpo.Controls.Add(Me.lbFiltro)
    Me.pnCorpo.Controls.Add(Me.edFiltro)
    Me.pnCorpo.Controls.Add(Me.cmdBloccaSbloccaRuolo)
    Me.pnCorpo.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCorpo.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnCorpo.Location = New System.Drawing.Point(318, 26)
    Me.pnCorpo.Name = "pnCorpo"
    Me.pnCorpo.NTSActiveTrasparency = True
    Me.pnCorpo.Size = New System.Drawing.Size(327, 403)
    Me.pnCorpo.TabIndex = 7
    Me.pnCorpo.Text = "NtsPanel1"
    '
    'cmdBloccaTutto
    '
    Me.cmdBloccaTutto.ImagePath = ""
    Me.cmdBloccaTutto.ImageText = ""
    Me.cmdBloccaTutto.Location = New System.Drawing.Point(111, 79)
    Me.cmdBloccaTutto.Name = "cmdBloccaTutto"
    Me.cmdBloccaTutto.NTSContextMenu = Nothing
    Me.cmdBloccaTutto.Size = New System.Drawing.Size(102, 28)
    Me.cmdBloccaTutto.TabIndex = 19
    Me.cmdBloccaTutto.Text = "Blocca Tutto"
    '
    'cmdSbloccaTutto
    '
    Me.cmdSbloccaTutto.ImagePath = ""
    Me.cmdSbloccaTutto.ImageText = ""
    Me.cmdSbloccaTutto.Location = New System.Drawing.Point(219, 79)
    Me.cmdSbloccaTutto.Name = "cmdSbloccaTutto"
    Me.cmdSbloccaTutto.NTSContextMenu = Nothing
    Me.cmdSbloccaTutto.Size = New System.Drawing.Size(102, 28)
    Me.cmdSbloccaTutto.TabIndex = 18
    Me.cmdSbloccaTutto.Text = "Sblocca Tutto"
    '
    'lbRiga
    '
    Me.lbRiga.AutoSize = True
    Me.lbRiga.BackColor = System.Drawing.Color.Transparent
    Me.lbRiga.Location = New System.Drawing.Point(9, 290)
    Me.lbRiga.Name = "lbRiga"
    Me.lbRiga.NTSDbField = ""
    Me.lbRiga.Size = New System.Drawing.Size(313, 13)
    Me.lbRiga.TabIndex = 17
    Me.lbRiga.Text = "___________________________________________________"
    Me.lbRiga.Tooltip = ""
    Me.lbRiga.UseMnemonic = False
    '
    'cmdAccc
    '
    Me.cmdAccc.ImagePath = ""
    Me.cmdAccc.ImageText = ""
    Me.cmdAccc.Location = New System.Drawing.Point(111, 324)
    Me.cmdAccc.Name = "cmdAccc"
    Me.cmdAccc.NTSContextMenu = Nothing
    Me.cmdAccc.Size = New System.Drawing.Size(210, 28)
    Me.cmdAccc.TabIndex = 16
    Me.cmdAccc.Text = "Configuraz. sistema sicurezza avanzata"
    '
    'cmdGron
    '
    Me.cmdGron.ImagePath = ""
    Me.cmdGron.ImageText = ""
    Me.cmdGron.Location = New System.Drawing.Point(111, 358)
    Me.cmdGron.Name = "cmdGron"
    Me.cmdGron.NTSContextMenu = Nothing
    Me.cmdGron.Size = New System.Drawing.Size(210, 28)
    Me.cmdGron.TabIndex = 15
    Me.cmdGron.Text = "Copia configurazione utenti /ruoli"
    '
    'lbNotaBlu
    '
    Me.lbNotaBlu.AutoSize = True
    Me.lbNotaBlu.BackColor = System.Drawing.Color.Transparent
    Me.lbNotaBlu.ForeColor = System.Drawing.Color.Blue
    Me.lbNotaBlu.Location = New System.Drawing.Point(6, 277)
    Me.lbNotaBlu.Name = "lbNotaBlu"
    Me.lbNotaBlu.NTSDbField = ""
    Me.lbNotaBlu.Size = New System.Drawing.Size(307, 13)
    Me.lbNotaBlu.TabIndex = 14
    Me.lbNotaBlu.Text = "Testo blu (o '?'): programma non eseguibile per blocco sul ruolo"
    Me.lbNotaBlu.Tooltip = ""
    Me.lbNotaBlu.UseMnemonic = False
    '
    'lbNotaRossa
    '
    Me.lbNotaRossa.AutoSize = True
    Me.lbNotaRossa.BackColor = System.Drawing.Color.Transparent
    Me.lbNotaRossa.ForeColor = System.Drawing.Color.Red
    Me.lbNotaRossa.Location = New System.Drawing.Point(6, 264)
    Me.lbNotaRossa.Name = "lbNotaRossa"
    Me.lbNotaRossa.NTSDbField = ""
    Me.lbNotaRossa.Size = New System.Drawing.Size(320, 13)
    Me.lbNotaRossa.TabIndex = 13
    Me.lbNotaRossa.Text = "Sfondo rosso (o '!'): progr. non eseguib. per blocco sull'operatore"
    Me.lbNotaRossa.Tooltip = ""
    Me.lbNotaRossa.UseMnemonic = False
    '
    'cmdBloccaSbloccaOperat
    '
    Me.cmdBloccaSbloccaOperat.ImagePath = ""
    Me.cmdBloccaSbloccaOperat.ImageText = ""
    Me.cmdBloccaSbloccaOperat.Location = New System.Drawing.Point(111, 113)
    Me.cmdBloccaSbloccaOperat.Name = "cmdBloccaSbloccaOperat"
    Me.cmdBloccaSbloccaOperat.NTSContextMenu = Nothing
    Me.cmdBloccaSbloccaOperat.Size = New System.Drawing.Size(210, 28)
    Me.cmdBloccaSbloccaOperat.TabIndex = 11
    Me.cmdBloccaSbloccaOperat.Text = "Blocca/Sblocca per Operatore in filtro"
    '
    'cmdProcedi
    '
    Me.cmdProcedi.ImagePath = ""
    Me.cmdProcedi.ImageText = ""
    Me.cmdProcedi.Location = New System.Drawing.Point(3, 79)
    Me.cmdProcedi.Name = "cmdProcedi"
    Me.cmdProcedi.NTSContextMenu = Nothing
    Me.cmdProcedi.Size = New System.Drawing.Size(102, 28)
    Me.cmdProcedi.TabIndex = 10
    Me.cmdProcedi.Text = "Aggiorna menu"
    Me.cmdProcedi.Visible = False
    '
    'cmdOperatore
    '
    Me.cmdOperatore.ImagePath = ""
    Me.cmdOperatore.ImageText = ""
    Me.cmdOperatore.Location = New System.Drawing.Point(111, 172)
    Me.cmdOperatore.Name = "cmdOperatore"
    Me.cmdOperatore.NTSContextMenu = Nothing
    Me.cmdOperatore.Size = New System.Drawing.Size(210, 28)
    Me.cmdOperatore.TabIndex = 8
    Me.cmdOperatore.Text = "Accesso per Operatore"
    '
    'cmdGruppoOperatori
    '
    Me.cmdGruppoOperatori.ImagePath = ""
    Me.cmdGruppoOperatori.ImageText = ""
    Me.cmdGruppoOperatori.Location = New System.Drawing.Point(111, 206)
    Me.cmdGruppoOperatori.Name = "cmdGruppoOperatori"
    Me.cmdGruppoOperatori.NTSContextMenu = Nothing
    Me.cmdGruppoOperatori.Size = New System.Drawing.Size(210, 28)
    Me.cmdGruppoOperatori.TabIndex = 9
    Me.cmdGruppoOperatori.Text = "Accesso per Gruppo Operatori / Ruoli"
    '
    'cmdBloccaSbloccaRuolo
    '
    Me.cmdBloccaSbloccaRuolo.ImagePath = ""
    Me.cmdBloccaSbloccaRuolo.ImageText = ""
    Me.cmdBloccaSbloccaRuolo.Location = New System.Drawing.Point(111, 113)
    Me.cmdBloccaSbloccaRuolo.Name = "cmdBloccaSbloccaRuolo"
    Me.cmdBloccaSbloccaRuolo.NTSContextMenu = Nothing
    Me.cmdBloccaSbloccaRuolo.Size = New System.Drawing.Size(210, 28)
    Me.cmdBloccaSbloccaRuolo.TabIndex = 12
    Me.cmdBloccaSbloccaRuolo.Text = "Blocca/Sblocca per Ruolo in filtro"
    '
    'ImageList1
    '
    Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
    Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    '
    'FRM__GACC
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(645, 429)
    Me.Controls.Add(Me.pnCorpo)
    Me.Controls.Add(Me.pnThree)
    Me.Controls.Add(Me.barDockControlLeft)
    Me.Controls.Add(Me.barDockControlRight)
    Me.Controls.Add(Me.barDockControlBottom)
    Me.Controls.Add(Me.barDockControlTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.HelpButton = True
    Me.Name = "FRM__GACC"
    Me.Text = "GESTIONE ACCESSI"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NtsBarManager1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbModo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edFiltro.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnThree, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnThree.ResumeLayout(False)
    CType(Me.pnCorpo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCorpo.ResumeLayout(False)
    Me.pnCorpo.PerformLayout()
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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__GACC", "BE__GACC", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 128265803653608595, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleGacc = CType(oTmp, CLE__GACC)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__GACC", strRemoteServer, strRemotePort)
    AddHandler oCleGacc.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleGacc.Init(oApp, oScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False

    Return True
  End Function

  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Try
      '-------------------------------------------------
      'carico le immagini della toolbar
      Try
        tlbGuida.GlyphPath = (oApp.ChildImageDir & "\help.gif")
        tlbEsci.GlyphPath = (oApp.ChildImageDir & "\exit.gif")
      Catch ex As Exception
        'non gestisco l'errore: se non c'è una immagine prendo quella standard
      End Try
      tlbMain.NTSSetToolTip()

      Dim dttModo As New DataTable
      dttModo.Columns.Add("cod", GetType(Integer))
      dttModo.Columns.Add("val", GetType(String))
      dttModo.Rows.Add(New Object() {1, "Analizza per operatore"})
      dttModo.Rows.Add(New Object() {2, "Analizza per gruppo operatore/ruolo"})
      dttModo.AcceptChanges()
      cbModo.DataSource = dttModo
      cbModo.ValueMember = "cod"
      cbModo.DisplayMember = "val"

      cbModo.NTSSetParam(oApp.Tr(Me, 129545902561289062, "Modalità operativa"))
      edFiltro.NTSSetParam(oMenu, oApp.Tr(Me, 129546018874150390, "Filtro"), 0, True)
      edFiltro.NTSSetParamZoom("...")

      ImageList1.Images.Add("", Bitmap.FromFile(oApp.ChildImageDir & "\open_treeview.gif"))
      ImageList1.Images.Add("", Bitmap.FromFile(oApp.ChildImageDir & "\materia_prima.gif"))
      trGacc.ImageList = ImageList1
      trGacc.ImageIndex = -1
      trGacc.SelectedImageIndex = -1

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
  Public Overridable Sub FRM__GACC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      InitControls()
      GctlSetRoules()

      cbModo.SelectedValue = "1"

      If Not CreaTreeview() Then
        Me.Close()
        Return
      End If

      If Not CLN__STD.UserIsAdmin(oApp.User.Gruppo) Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130054114967608748, "L'utente non è abilitato all'utilizzo di questro programma non essendo un amministratore"))
        Me.Close()
        Return
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub FRM__GACC_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Dim ctrlTmp As Control = Nothing
    Try
      '--------------------------------------------
      'gestione dello zoom:
      'eseguo la Zoom, tanto se per il controllo non deve venir eseguito uno zoom particolare, la routine non fa nulla e viene processato lo zoom standard del controllo, settato con la NTSSetParamZoom
      If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
        Zoom()
        e.Handled = True    'altrimenti anche il controllo riceve l'F5 e la routine ZOOM viene eseguita 2 volte!!!
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

#End Region

#Region "Eventi Toolbar"
  Public Overridable Sub tlbGuida_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbGuida.ItemClick
    SendKeys.Send("{F1}")
  End Sub

  Public Overridable Sub tlbEsci_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles tlbEsci.ItemClick
    Me.Close()
  End Sub
#End Region

  Public Overridable Sub trGacc_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trGacc.AfterSelect
    Try
      If e.Node.Tag.ToString.ToLower.IndexOf("subm") > -1 Then
        trGacc.SelectedImageIndex = 0 'menu
      Else
        trGacc.SelectedImageIndex = 1  'child
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub edFiltro_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edFiltro.TextChanged
    Try
      'Non più usato
      'visto che il menu è sempre riferito a ciò che è indicato nel textbox, al cambio del testo svuoto il treeview
      ' trGacc.Nodes.Clear()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub edFiltro_ValidatedAndChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edFiltro.ValidatedAndChanged
    Try
      If NTSActiveFirstOccured = False Then Return
      CreaTreeview()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub Zoom()
    Dim ctlLastControl As Control
    Dim ctrlTmp As Control = Nothing
    Dim oParam As New CLE__PATB

    Try
      'entro qui perchè nella FRM__HLAN_KeyDown ho inserito il seguente codice:
      'If e.KeyCode = Keys.F5 And e.Control = False And e.Alt = False And e.Shift = False Then
      '  Zoom()
      '  e.Handled = True
      'End If

      ctlLastControl = NTSFindControlFocused(Me)
      If ctlLastControl Is Nothing Then Return

      If edFiltro.Focused Then
        If NTSCInt(cbModo.SelectedValue) = 1 Then
          'zoom operatori
          edFiltro.NTSSetParamZoom("ZOOMOPERAT")
          NTSCallStandardZoom()
        End If

        If NTSCInt(cbModo.SelectedValue) = 2 Then
          'zoom operatori
          edFiltro.NTSSetParamZoom("ZOOMORUOLIPERAT")
          NTSCallStandardZoom()
        End If
      Else
        '------------------------------------
        'zoom standard di textbox e griglia
        'SendKeys.SendWait("{F5}") 'se faccio questa riga va in un loop infinito....
        NTSCallStandardZoom()
      End If
      '------------------------------------
      'riporto il focus sul controllo che ha chiamato lo zoom
      ctlLastControl.Focus()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbModo_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbModo.SelectedValueChanged
    Try
      edFiltro.Text = "*"
      If NTSCInt(cbModo.SelectedValue) = 1 Then
        GctlSetVisible(cmdBloccaSbloccaOperat)
        cmdBloccaSbloccaRuolo.Visible = False
      Else
        cmdBloccaSbloccaOperat.Visible = False
        GctlSetVisible(cmdBloccaSbloccaRuolo)
      End If

      CreaTreeview()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdProcedi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProcedi.Click
    Try
      CreaTreeview()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Function CreaTreeview() As Boolean
    Dim dttMenu As New DataTable
    Dim nodNew As TreeNode = Nothing
    Dim dtrT As DataRow = Nothing
    Try
      Me.Cursor = Cursors.WaitCursor

      trGacc.Nodes.Clear()

      If edFiltro.Text = "" Then edFiltro.Text = "*"

      'leggo il menu: se xx_operat = 'N' il programma non è eseguibile per blocco su utente, 
      'se xx_gruppo = 'N' il programma non è eseguibile per blocco su gruppo
      Select Case NTSCInt(cbModo.SelectedValue)
        Case 1 : If Not oCleGacc.GetMenupop(edFiltro.Text, False, dttMenu) Then Return False
        Case 2 : If Not oCleGacc.GetMenupop(edFiltro.Text, True, dttMenu) Then Return False
      End Select

      'carico il menu
      For Each dtrT In dttMenu.Select("", "mnlivello, mnliv1, mnliv2, mnliv3")
        nodNew = New TreeNode
        nodNew.Text = NTSCStr(dtrT!mndescr)
        nodNew.Tag = NTSCStr(dtrT!mnprogr)
        If NTSCStr(dtrT!xx_operat) = "N" Then
          nodNew.BackColor = oColoreBloccoOperat 'utente non può eseguire: sfondo rosso
          nodNew.Text = "!" & nodNew.Text
        End If
        If NTSCStr(dtrT!xx_gruppo) = "N" Then
          nodNew.ForeColor = oColoreBloccoRuoli 'gruppo non può esegure: testo blu
          nodNew.Text = "?" & nodNew.Text
        End If
        If nodNew.Tag.ToString.ToLower.IndexOf("subm") > -1 Then
          nodNew.ImageIndex = 0 'menu
        Else
          nodNew.ImageIndex = 1 'child
        End If

        If NTSCInt(dtrT!mnlivello) = 1 Then
          Try
            nodNew.Name = NTSCStr(dtrT!mnliv1)
            trGacc.Nodes.Add(nodNew)
          Catch
            'ignoro l'errore, vuol dire che il livello precedente non esiste / non è abilitato
            nodNew = Nothing
          End Try
        ElseIf NTSCInt(dtrT!mnlivello) = 2 Then
          Try
            nodNew.Name = NTSCStr(dtrT!mnliv1) & "_" & NTSCStr(dtrT!mnliv2)
            trGacc.Nodes(NTSCStr(dtrT!mnliv1)).Nodes.Add(nodNew)
          Catch
            'ignoro l'errore, vuol dire che il livello precedente non esiste / non è abilitato
            nodNew = Nothing
          End Try
        ElseIf NTSCInt(dtrT!mnlivello) = 3 Then
          Try
            nodNew.Name = NTSCStr(dtrT!mnliv1) & "_" & NTSCStr(dtrT!mnliv2) & "_" & NTSCStr(dtrT!mnliv3)
            trGacc.Nodes(NTSCStr(dtrT!mnliv1)).Nodes(NTSCStr(dtrT!mnliv1) & "_" & NTSCStr(dtrT!mnliv2)).Nodes.Add(nodNew)
          Catch
            'ignoro l'errore, vuol dire che il livello precedente non esiste / non è abilitato
            nodNew = Nothing
          End Try
        End If
      Next

      trGacc.Focus()

      Return True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      dttMenu.Clear()
      Me.Cursor = Cursors.Default
    End Try
  End Function


  Public Overridable Sub cmdOperatore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOperatore.Click
    Dim frmGmem As FRM__GMEM = Nothing
    Try
      frmGmem = CType(NTSNewFormModal("FRM__GMEM"), FRM__GMEM)
      If trGacc.SelectedNode Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129546131352607422, "Selezionare prima un menu o un programma"))
        Return
      End If

      frmGmem.Init(oMenu, oCallParams, DittaCorrente)
      frmGmem.InitEntity(oCleGacc, NTSCStr(trGacc.SelectedNode.Tag.ToString), "ACCOPEU")
      frmGmem.ShowDialog()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmGmem Is Nothing Then frmGmem.Dispose()
      frmGmem = Nothing
    End Try
  End Sub
  Public Overridable Sub cmdGruppoOperatori_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGruppoOperatori.Click
    Dim frmGmem As FRM__GMEM = Nothing
    Try
      frmGmem = CType(NTSNewFormModal("FRM__GMEM"), FRM__GMEM)
      If trGacc.SelectedNode Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129546137894658203, "Selezionare prima un menu o un programma"))
        Return
      End If

      frmGmem.Init(oMenu, oCallParams, DittaCorrente)
      frmGmem.InitEntity(oCleGacc, NTSCStr(trGacc.SelectedNode.Tag.ToString), "ACCOPER")
      frmGmem.ShowDialog()

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    Finally
      If Not frmGmem Is Nothing Then frmGmem.Dispose()
      frmGmem = Nothing
    End Try
  End Sub

  Public Overridable Sub cmdBloccaSbloccaOperat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBloccaSbloccaOperat.Click
    Try
      If edFiltro.Text = "" Or edFiltro.Text = "*" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129546145537089843, "Impostare prima un operatore nel filtro"))
        Return
      End If

      If trGacc.SelectedNode Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129546145992285156, "Selezionare prima un menu o un programma"))
        Return
      End If

      SbloccaBloccaOperat(trGacc.SelectedNode, (trGacc.SelectedNode.BackColor <> oColoreBloccoOperat))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdBloccaSbloccaRuolo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBloccaSbloccaRuolo.Click
    Try
      If edFiltro.Text = "" Or edFiltro.Text = "*" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129546145825488281, "Impostare prima un gruppo operatori/ruolo nel filtro"))
        Return
      End If

      If trGacc.SelectedNode Is Nothing Then
        oApp.MsgBoxErr(oApp.Tr(Me, 129546146043486328, "Selezionare prima un menu o un programma"))
        Return
      End If

      SbloccaBloccaRuolo(trGacc.SelectedNode, (trGacc.SelectedNode.ForeColor <> oColoreBloccoRuoli))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Function SbloccaBloccaOperat(ByVal oNode As TreeNode, ByVal bBlocca As Boolean) As Boolean
    Try
      'procedo
      If oNode.BackColor = oColoreBloccoOperat AndAlso Not bBlocca Then
        If oCleGacc.BloccaSblocca(NTSCStr(oNode.Tag), edFiltro.Text, False, False) Then
          oNode.BackColor = Color.White
          oNode.Text = oNode.Text.Replace("!", "")
        End If
      ElseIf oNode.BackColor <> oColoreBloccoOperat AndAlso bBlocca Then
        If oCleGacc.BloccaSblocca(NTSCStr(oNode.Tag), edFiltro.Text, False, True) Then
          oNode.BackColor = oColoreBloccoOperat
          oNode.Text = "!" & oNode.Text
        End If
      End If

      If oNode.Parent IsNot Nothing AndAlso Not bBlocca Then Return SbloccaBloccaOperat(oNode.Parent, bBlocca)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Function SbloccaBloccaRuolo(ByVal oNode As TreeNode, ByVal bBlocca As Boolean) As Boolean
    Try
      'procedo
      If oNode.ForeColor = oColoreBloccoRuoli AndAlso Not bBlocca Then
        If oCleGacc.BloccaSblocca(NTSCStr(oNode.Tag), edFiltro.Text, True, False) Then
          oNode.ForeColor = Color.Black
          oNode.Text = oNode.Text.Replace("?", "")
        End If
      ElseIf oNode.ForeColor <> oColoreBloccoRuoli AndAlso bBlocca Then
        If oCleGacc.BloccaSblocca(NTSCStr(oNode.Tag), edFiltro.Text, True, True) Then
          oNode.ForeColor = oColoreBloccoRuoli
          oNode.Text = "?" & oNode.Text
        End If
      End If

      If oNode.Parent IsNot Nothing AndAlso Not bBlocca Then Return SbloccaBloccaRuolo(oNode.Parent, bBlocca)

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function


  Public Overridable Sub cmdGron_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGron.Click
    Try
      oMenu.RunChild("NTSInformatica", "FRM__GRON", "", DittaCorrente, "", "BN__GRON", Nothing, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdAccc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAccc.Click
    Try
      oMenu.RunChild("NTSInformatica", "FRM__ACCC", "", DittaCorrente, "", "BN__ACCC", Nothing, "", True, True)

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub cmdBloccaTutto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBloccaTutto.Click
    Try
      BloccaSbloccaTutto(True)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdSbloccaTutto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSbloccaTutto.Click
    Try
      BloccaSbloccaTutto(False)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub


  Public Overridable Sub BloccaSbloccaTutto(ByVal bBlocca As Boolean)
    Dim strOperazione As String = "bloccare"
    Try
      If edFiltro.Text = "" OrElse edFiltro.Text = "*" Then
        oApp.MsgBoxErr(oApp.Tr(Me, 130800457973351417, "Impostare prima un operatore nel filtro"))
        Return
      End If

      If Not bBlocca Then strOperazione = "sbloccare"

      If oApp.MsgBoxInfoYesNo_DefNo(oApp.Tr(Me, 130800458767568370, "Sei davvero sicuro di voler |" & strOperazione & _
                                                                    "| TUTTE le voci di menù?")) = Windows.Forms.DialogResult.No Then Return

      Me.Cursor = Cursors.WaitCursor
      For Each oNode As TreeNode In trGacc.Nodes
        If Not BloccaSbloccaTuttoMenu(oNode, bBlocca) Then Return
      Next
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Function BloccaSbloccaTuttoMenu(ByVal oNode As TreeNode, ByVal bBlocca As Boolean) As Boolean
    Try
      'Effettua l'operazione solo sui rami dove è necessario farla
      If NTSCStr(cbModo.SelectedValue) = "2" Then
        SbloccaBloccaRuolo(oNode, bBlocca)
      Else
        SbloccaBloccaOperat(oNode, bBlocca)
      End If

      'Scorre i nodi figlio per completare l'operazioni su tutti
      For Each oChildNode As TreeNode In oNode.Nodes
        If Not BloccaSbloccaTuttoMenu(oChildNode, bBlocca) Then Return False
      Next

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
End Class
