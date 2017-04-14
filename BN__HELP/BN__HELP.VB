Imports System.Data
Imports NTSInformatica.CLN__STD
Imports System.IO
Imports mshtml

Public Class FRM__HELP
  Inherits FRM__CHIL

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

#Region "Variabili e oggetti"
  Public oCallParams As CLE__CLDP
  Public oCleBasn As CLE__BASN
  Public dttPage As New DataTable
  Public strMainDir As String = ""
  Public strSep As String = "\"
  Public strWeb As String = "http://www.ntsinformatica.eu/BusHelpWs/BusHelpWs.asmx"

  Public strBusVer As String = "NET2012"
  Public strBusKey As String = "0"
  Public docHTML As IHTMLDocument2

  Public bEditEenable As Boolean = False
  Public bVssEnable As Boolean = False
  Public bWebsiteEnable As Boolean = False

  Public strLastPage As String = ""
  Public strLastHTM As String = ""

  Public strHelpDirectoryLocal As String = ""
  Public bHelpLocalExist As Boolean = False
  Public bHelpOnlineExist As Boolean = True
#End Region

#Region "Controlli e InitializeComponent"
  Public WithEvents pnTop As NTSPanel
  Public WithEvents cmdPrint As NTSButton
  Public WithEvents cmdHome As NTSButton
  Public WithEvents cmdSuc As NTSButton
  Public WithEvents cmdPrec As NTSButton
  Public WithEvents tsHelp As NTSTabControl
  Public WithEvents TabPage0 As NTSTabPage
  Public WithEvents trContenuto As NTSTreeView
  Public WithEvents TabPage2 As NTSTabPage
  Public WithEvents cmdCerca As NTSButton
  Public WithEvents ckTitoli As NTSCheckBox
  Public WithEvents edTrova As NTSTextBoxStr
  Public WithEvents liTrova As NTSListBox
  Public WithEvents pnContenuto As NTSInformatica.NTSPanel
  Public WithEvents pnCerca As NTSInformatica.NTSPanel
  Public WithEvents WebBrows As NTSInformatica.NTSWebBrowser
  Public WithEvents NtsSplitter1 As NTSInformatica.NTSSplitter
  Public WithEvents tmTimer As System.Windows.Forms.Timer
  Public WithEvents cbTipo As NTSInformatica.NTSComboBox
  Public WithEvents ImageList1 As System.Windows.Forms.ImageList
  Private components As System.ComponentModel.IContainer
  Public WithEvents pnEdit As NTSInformatica.NTSPanel
  Public WithEvents cmdSave As NTSInformatica.NTSButton
  Public WithEvents cmdAnnulla As NTSInformatica.NTSButton
  Public WithEvents cmdImage As NTSInformatica.NTSButton
  Public WithEvents cmdEndEdit As NTSInformatica.NTSButton
  Public WithEvents cmdLink As NTSInformatica.NTSButton
  Public WithEvents cbFontDim As NTSInformatica.NTSComboBox
  Public WithEvents cbFont As NTSInformatica.NTSComboBox
  Public WithEvents ckItalic As NTSInformatica.NTSCheckBox
  Public WithEvents ckUnderline As NTSInformatica.NTSCheckBox
  Public WithEvents ckBold As NTSInformatica.NTSCheckBox
  Public WithEvents cmdBookmark As NTSInformatica.NTSButton
  Public WithEvents lbPage As System.Windows.Forms.Label
  Public WithEvents cmdEditor As NTSInformatica.NTSButton
  Public WithEvents lbAvviso As System.Windows.Forms.Label
  Public WithEvents cmdShowHideSummary As NTSInformatica.NTSButton
  Public WithEvents cmdLista As NTSInformatica.NTSButton
  Public WithEvents cmdListaNum As NTSInformatica.NTSButton
  Public WithEvents cmdControlli As NTSInformatica.NTSButton
  Public WithEvents lsbProposta As NTSInformatica.NTSListBox
  Public WithEvents cmdFind As NTSInformatica.NTSButton
  Public WithEvents cmdInsTabe As NTSInformatica.NTSButton
  Public WithEvents cmdConfig As NTSInformatica.NTSButton
  Public WithEvents cmdRecuperaHTML As NTSInformatica.NTSButton
  Public WithEvents cmdAcapo As NTSInformatica.NTSButton
  Public WithEvents TabPage1 As NTSInformatica.NTSTabPage
  Public WithEvents pnCronologia As NTSInformatica.NTSPanel
  Public WithEvents trCronologia As NTSInformatica.NTSTreeView
  Public WithEvents cmdComprimiCronologia As NTSInformatica.NTSButton
  Public WithEvents cmdPrintArgomento As NTSInformatica.NTSButton
  Public WithEvents cmdCancellaCronologia As NTSInformatica.NTSButton
  Public WithEvents tsView As NTSInformatica.NTSTabControl
  Public WithEvents TabPageView0 As NTSInformatica.NTSTabPage
  Public WithEvents pnContenutoUp As NTSInformatica.NTSPanel
  Public WithEvents pnLeft As NTSInformatica.NTSPanel
  Public WithEvents pnContenutoDown As NTSInformatica.NTSPanel
  Public WithEvents cmdRimuoviPDF As NTSInformatica.NTSButton
  Public WithEvents cmdSfogliaPdf As NTSInformatica.NTSButton
  Public WithEvents trPDF As NTSInformatica.NTSTreeView
  Public WithEvents pnRight As NTSInformatica.NTSPanel
  Public WithEvents tsAppunti As NTSInformatica.NTSTabControl
  Public WithEvents TabPageAppunti0 As NTSInformatica.NTSTabPage
  Public WithEvents edAppunti As NTSInformatica.NTSMemoBox
  Public WithEvents NtsSplitter3 As NTSInformatica.NTSSplitter
  Public WithEvents cmdAppunti As NTSInformatica.NTSButton
  Public WithEvents pnWebFill As NTSInformatica.NTSPanel
  Public WithEvents pnAppunti As NTSInformatica.NTSPanel
  Public WithEvents pnFill As NTSInformatica.NTSPanel
  Public WithEvents TabPageView1 As NTSInformatica.NTSTabPage
  Public WithEvents pnAggURL As NTSInformatica.NTSPanel
  Public WithEvents WebBrowsAgg As NTSInformatica.NTSWebBrowser
  Public WithEvents cmdDelURL As NTSInformatica.NTSButton
  Public WithEvents TabPageFile0 As NTSInformatica.NTSTabPage
  Public WithEvents lbFindInPage As System.Windows.Forms.Label
  Public WithEvents cmdEditorNTS As NTSInformatica.NTSButton
  Public WithEvents cmdOpenPdf As NTSInformatica.NTSButton
  Public WithEvents cmdComprimi As NTSInformatica.NTSButton

  Public Overridable Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__HELP))
    Me.pnTop = New NTSInformatica.NTSPanel
    Me.cmdEditorNTS = New NTSInformatica.NTSButton
    Me.lbAvviso = New System.Windows.Forms.Label
    Me.cbTipo = New NTSInformatica.NTSComboBox
    Me.cmdFind = New NTSInformatica.NTSButton
    Me.cmdAppunti = New NTSInformatica.NTSButton
    Me.cmdShowHideSummary = New NTSInformatica.NTSButton
    Me.cmdPrintArgomento = New NTSInformatica.NTSButton
    Me.cmdPrint = New NTSInformatica.NTSButton
    Me.cmdHome = New NTSInformatica.NTSButton
    Me.cmdSuc = New NTSInformatica.NTSButton
    Me.cmdPrec = New NTSInformatica.NTSButton
    Me.tsHelp = New NTSInformatica.NTSTabControl
    Me.TabPageFile0 = New NTSInformatica.NTSTabPage
    Me.pnContenutoDown = New NTSInformatica.NTSPanel
    Me.cmdOpenPdf = New NTSInformatica.NTSButton
    Me.cmdRimuoviPDF = New NTSInformatica.NTSButton
    Me.cmdSfogliaPdf = New NTSInformatica.NTSButton
    Me.trPDF = New NTSInformatica.NTSTreeView
    Me.TabPage0 = New NTSInformatica.NTSTabPage
    Me.pnContenuto = New NTSInformatica.NTSPanel
    Me.pnContenutoUp = New NTSInformatica.NTSPanel
    Me.trContenuto = New NTSInformatica.NTSTreeView
    Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
    Me.cmdComprimi = New NTSInformatica.NTSButton
    Me.TabPage2 = New NTSInformatica.NTSTabPage
    Me.pnCerca = New NTSInformatica.NTSPanel
    Me.lsbProposta = New NTSInformatica.NTSListBox
    Me.cmdCerca = New NTSInformatica.NTSButton
    Me.ckTitoli = New NTSInformatica.NTSCheckBox
    Me.edTrova = New NTSInformatica.NTSTextBoxStr
    Me.liTrova = New NTSInformatica.NTSListBox
    Me.TabPage1 = New NTSInformatica.NTSTabPage
    Me.pnCronologia = New NTSInformatica.NTSPanel
    Me.cmdCancellaCronologia = New NTSInformatica.NTSButton
    Me.trCronologia = New NTSInformatica.NTSTreeView
    Me.cmdComprimiCronologia = New NTSInformatica.NTSButton
    Me.tsView = New NTSInformatica.NTSTabControl
    Me.TabPageView0 = New NTSInformatica.NTSTabPage
    Me.pnWebFill = New NTSInformatica.NTSPanel
    Me.WebBrows = New NTSInformatica.NTSWebBrowser
    Me.TabPageView1 = New NTSInformatica.NTSTabPage
    Me.pnAggURL = New NTSInformatica.NTSPanel
    Me.WebBrowsAgg = New NTSInformatica.NTSWebBrowser
    Me.cmdDelURL = New NTSInformatica.NTSButton
    Me.pnEdit = New NTSInformatica.NTSPanel
    Me.lbPage = New System.Windows.Forms.Label
    Me.cmdBookmark = New NTSInformatica.NTSButton
    Me.ckItalic = New NTSInformatica.NTSCheckBox
    Me.ckUnderline = New NTSInformatica.NTSCheckBox
    Me.ckBold = New NTSInformatica.NTSCheckBox
    Me.cmdConfig = New NTSInformatica.NTSButton
    Me.cmdRecuperaHTML = New NTSInformatica.NTSButton
    Me.cmdEditor = New NTSInformatica.NTSButton
    Me.cbFontDim = New NTSInformatica.NTSComboBox
    Me.cbFont = New NTSInformatica.NTSComboBox
    Me.cmdInsTabe = New NTSInformatica.NTSButton
    Me.cmdListaNum = New NTSInformatica.NTSButton
    Me.cmdAcapo = New NTSInformatica.NTSButton
    Me.cmdLista = New NTSInformatica.NTSButton
    Me.cmdControlli = New NTSInformatica.NTSButton
    Me.cmdImage = New NTSInformatica.NTSButton
    Me.cmdEndEdit = New NTSInformatica.NTSButton
    Me.cmdLink = New NTSInformatica.NTSButton
    Me.cmdAnnulla = New NTSInformatica.NTSButton
    Me.cmdSave = New NTSInformatica.NTSButton
    Me.NtsSplitter1 = New NTSInformatica.NTSSplitter
    Me.tmTimer = New System.Windows.Forms.Timer(Me.components)
    Me.pnLeft = New NTSInformatica.NTSPanel
    Me.pnRight = New NTSInformatica.NTSPanel
    Me.tsAppunti = New NTSInformatica.NTSTabControl
    Me.TabPageAppunti0 = New NTSInformatica.NTSTabPage
    Me.pnAppunti = New NTSInformatica.NTSPanel
    Me.edAppunti = New NTSInformatica.NTSMemoBox
    Me.NtsSplitter3 = New NTSInformatica.NTSSplitter
    Me.pnFill = New NTSInformatica.NTSPanel
    Me.lbFindInPage = New System.Windows.Forms.Label
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnTop.SuspendLayout()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.tsHelp, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsHelp.SuspendLayout()
    Me.TabPageFile0.SuspendLayout()
    CType(Me.pnContenutoDown, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnContenutoDown.SuspendLayout()
    Me.TabPage0.SuspendLayout()
    CType(Me.pnContenuto, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnContenuto.SuspendLayout()
    CType(Me.pnContenutoUp, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnContenutoUp.SuspendLayout()
    Me.TabPage2.SuspendLayout()
    CType(Me.pnCerca, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCerca.SuspendLayout()
    CType(Me.lsbProposta, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckTitoli.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.edTrova.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.liTrova, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage1.SuspendLayout()
    CType(Me.pnCronologia, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnCronologia.SuspendLayout()
    CType(Me.tsView, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsView.SuspendLayout()
    Me.TabPageView0.SuspendLayout()
    CType(Me.pnWebFill, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnWebFill.SuspendLayout()
    Me.TabPageView1.SuspendLayout()
    CType(Me.pnAggURL, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAggURL.SuspendLayout()
    CType(Me.pnEdit, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnEdit.SuspendLayout()
    CType(Me.ckItalic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckUnderline.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.ckBold.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbFontDim.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.cbFont.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnLeft.SuspendLayout()
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnRight.SuspendLayout()
    CType(Me.tsAppunti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tsAppunti.SuspendLayout()
    Me.TabPageAppunti0.SuspendLayout()
    CType(Me.pnAppunti, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnAppunti.SuspendLayout()
    CType(Me.edAppunti.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.pnFill.SuspendLayout()
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
    'pnTop
    '
    Me.pnTop.AllowDrop = True
    Me.pnTop.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnTop.Appearance.Options.UseBackColor = True
    Me.pnTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnTop.Controls.Add(Me.cmdEditorNTS)
    Me.pnTop.Controls.Add(Me.lbAvviso)
    Me.pnTop.Controls.Add(Me.cbTipo)
    Me.pnTop.Controls.Add(Me.cmdFind)
    Me.pnTop.Controls.Add(Me.cmdAppunti)
    Me.pnTop.Controls.Add(Me.cmdShowHideSummary)
    Me.pnTop.Controls.Add(Me.cmdPrintArgomento)
    Me.pnTop.Controls.Add(Me.cmdPrint)
    Me.pnTop.Controls.Add(Me.cmdHome)
    Me.pnTop.Controls.Add(Me.cmdSuc)
    Me.pnTop.Controls.Add(Me.cmdPrec)
    Me.pnTop.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnTop.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnTop.Location = New System.Drawing.Point(0, 0)
    Me.pnTop.Name = "pnTop"
    Me.pnTop.NTSActiveTrasparency = True
    Me.pnTop.Size = New System.Drawing.Size(896, 37)
    Me.pnTop.TabIndex = 2
    '
    'cmdEditorNTS
    '
    Me.cmdEditorNTS.Image = CType(resources.GetObject("cmdEditorNTS.Image"), System.Drawing.Image)
    Me.cmdEditorNTS.ImageText = ""
    Me.cmdEditorNTS.Location = New System.Drawing.Point(330, 1)
    Me.cmdEditorNTS.Name = "cmdEditorNTS"
    Me.cmdEditorNTS.NTSContextMenu = Nothing
    Me.cmdEditorNTS.Size = New System.Drawing.Size(84, 35)
    Me.cmdEditorNTS.TabIndex = 18
    Me.cmdEditorNTS.TabStop = False
    Me.cmdEditorNTS.Text = "Edit"
    Me.cmdEditorNTS.Visible = False
    '
    'lbAvviso
    '
    Me.lbAvviso.AutoSize = True
    Me.lbAvviso.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lbAvviso.ForeColor = System.Drawing.Color.Red
    Me.lbAvviso.Location = New System.Drawing.Point(425, 12)
    Me.lbAvviso.Name = "lbAvviso"
    Me.lbAvviso.Size = New System.Drawing.Size(158, 13)
    Me.lbAvviso.TabIndex = 17
    Me.lbAvviso.Text = "Creazione indice in corso ..."
    '
    'cbTipo
    '
    Me.cbTipo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cbTipo.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbTipo.DataSource = Nothing
    Me.cbTipo.DisplayMember = ""
    Me.cbTipo.Location = New System.Drawing.Point(801, 9)
    Me.cbTipo.Name = "cbTipo"
    Me.cbTipo.NTSDbField = ""
    Me.cbTipo.Properties.AutoHeight = False
    Me.cbTipo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbTipo.Properties.DropDownRows = 30
    Me.cbTipo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbTipo.SelectedValue = ""
    Me.cbTipo.Size = New System.Drawing.Size(92, 20)
    Me.cbTipo.TabIndex = 7
    Me.cbTipo.ValueMember = ""
    '
    'cmdFind
    '
    Me.cmdFind.Image = CType(resources.GetObject("cmdFind.Image"), System.Drawing.Image)
    Me.cmdFind.ImageText = ""
    Me.cmdFind.Location = New System.Drawing.Point(117, 1)
    Me.cmdFind.Name = "cmdFind"
    Me.cmdFind.NTSContextMenu = Nothing
    Me.cmdFind.Size = New System.Drawing.Size(35, 35)
    Me.cmdFind.TabIndex = 4
    Me.cmdFind.TabStop = False
    Me.cmdFind.ToolTip = "Cerca"
    '
    'cmdAppunti
    '
    Me.cmdAppunti.Image = CType(resources.GetObject("cmdAppunti.Image"), System.Drawing.Image)
    Me.cmdAppunti.ImageText = ""
    Me.cmdAppunti.Location = New System.Drawing.Point(269, 1)
    Me.cmdAppunti.Name = "cmdAppunti"
    Me.cmdAppunti.NTSContextMenu = Nothing
    Me.cmdAppunti.Size = New System.Drawing.Size(35, 35)
    Me.cmdAppunti.TabIndex = 4
    Me.cmdAppunti.TabStop = False
    Me.cmdAppunti.ToolTip = "Mostra/nascondi appunti"
    '
    'cmdShowHideSummary
    '
    Me.cmdShowHideSummary.Image = CType(resources.GetObject("cmdShowHideSummary.Image"), System.Drawing.Image)
    Me.cmdShowHideSummary.ImageText = ""
    Me.cmdShowHideSummary.Location = New System.Drawing.Point(231, 1)
    Me.cmdShowHideSummary.Name = "cmdShowHideSummary"
    Me.cmdShowHideSummary.NTSContextMenu = Nothing
    Me.cmdShowHideSummary.Size = New System.Drawing.Size(35, 35)
    Me.cmdShowHideSummary.TabIndex = 4
    Me.cmdShowHideSummary.TabStop = False
    Me.cmdShowHideSummary.ToolTip = "Mostra/nascondi sommario"
    '
    'cmdPrintArgomento
    '
    Me.cmdPrintArgomento.Image = CType(resources.GetObject("cmdPrintArgomento.Image"), System.Drawing.Image)
    Me.cmdPrintArgomento.ImageText = ""
    Me.cmdPrintArgomento.Location = New System.Drawing.Point(193, 1)
    Me.cmdPrintArgomento.Name = "cmdPrintArgomento"
    Me.cmdPrintArgomento.NTSContextMenu = Nothing
    Me.cmdPrintArgomento.Size = New System.Drawing.Size(35, 35)
    Me.cmdPrintArgomento.TabIndex = 4
    Me.cmdPrintArgomento.TabStop = False
    Me.cmdPrintArgomento.ToolTip = "Stampa argomenti - Permette di stampare l'argomento corrente ed i relativi sottoa" & _
        "rgomenti"
    '
    'cmdPrint
    '
    Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
    Me.cmdPrint.ImageText = ""
    Me.cmdPrint.Location = New System.Drawing.Point(155, 1)
    Me.cmdPrint.Name = "cmdPrint"
    Me.cmdPrint.NTSContextMenu = Nothing
    Me.cmdPrint.Size = New System.Drawing.Size(35, 35)
    Me.cmdPrint.TabIndex = 4
    Me.cmdPrint.TabStop = False
    Me.cmdPrint.ToolTip = "Stampa - Lancia la stampa dell'argomento attualmente visualizzato in finestra"
    '
    'cmdHome
    '
    Me.cmdHome.Image = CType(resources.GetObject("cmdHome.Image"), System.Drawing.Image)
    Me.cmdHome.ImageText = ""
    Me.cmdHome.Location = New System.Drawing.Point(3, 1)
    Me.cmdHome.Name = "cmdHome"
    Me.cmdHome.NTSContextMenu = Nothing
    Me.cmdHome.Size = New System.Drawing.Size(35, 35)
    Me.cmdHome.TabIndex = 2
    Me.cmdHome.TabStop = False
    Me.cmdHome.ToolTip = "Homepage"
    '
    'cmdSuc
    '
    Me.cmdSuc.Image = CType(resources.GetObject("cmdSuc.Image"), System.Drawing.Image)
    Me.cmdSuc.ImageText = ""
    Me.cmdSuc.Location = New System.Drawing.Point(79, 1)
    Me.cmdSuc.Name = "cmdSuc"
    Me.cmdSuc.NTSContextMenu = Nothing
    Me.cmdSuc.Size = New System.Drawing.Size(35, 35)
    Me.cmdSuc.TabIndex = 1
    Me.cmdSuc.TabStop = False
    Me.cmdSuc.ToolTip = "Successivo"
    '
    'cmdPrec
    '
    Me.cmdPrec.Image = CType(resources.GetObject("cmdPrec.Image"), System.Drawing.Image)
    Me.cmdPrec.ImageText = ""
    Me.cmdPrec.Location = New System.Drawing.Point(41, 1)
    Me.cmdPrec.Name = "cmdPrec"
    Me.cmdPrec.NTSContextMenu = Nothing
    Me.cmdPrec.Size = New System.Drawing.Size(35, 35)
    Me.cmdPrec.TabIndex = 0
    Me.cmdPrec.TabStop = False
    Me.cmdPrec.ToolTip = "Precedente"
    '
    'tsHelp
    '
    Me.tsHelp.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsHelp.Location = New System.Drawing.Point(0, 0)
    Me.tsHelp.MinimumSize = New System.Drawing.Size(0, 150)
    Me.tsHelp.Name = "tsHelp"
    Me.tsHelp.SelectedTabPage = Me.TabPageFile0
    Me.tsHelp.Size = New System.Drawing.Size(224, 406)
    Me.tsHelp.TabIndex = 8
    Me.tsHelp.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabPage0, Me.TabPage2, Me.TabPage1, Me.TabPageFile0})
    '
    'TabPageFile0
    '
    Me.TabPageFile0.AllowDrop = True
    Me.TabPageFile0.Controls.Add(Me.pnContenutoDown)
    Me.TabPageFile0.Enable = True
    Me.TabPageFile0.Name = "TabPageFile0"
    Me.TabPageFile0.Size = New System.Drawing.Size(215, 376)
    Me.TabPageFile0.Text = "Pdf"
    '
    'pnContenutoDown
    '
    Me.pnContenutoDown.AllowDrop = True
    Me.pnContenutoDown.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnContenutoDown.Appearance.Options.UseBackColor = True
    Me.pnContenutoDown.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnContenutoDown.Controls.Add(Me.cmdOpenPdf)
    Me.pnContenutoDown.Controls.Add(Me.cmdRimuoviPDF)
    Me.pnContenutoDown.Controls.Add(Me.cmdSfogliaPdf)
    Me.pnContenutoDown.Controls.Add(Me.trPDF)
    Me.pnContenutoDown.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnContenutoDown.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnContenutoDown.Location = New System.Drawing.Point(0, 0)
    Me.pnContenutoDown.Name = "pnContenutoDown"
    Me.pnContenutoDown.NTSActiveTrasparency = True
    Me.pnContenutoDown.Size = New System.Drawing.Size(215, 376)
    Me.pnContenutoDown.TabIndex = 3
    Me.pnContenutoDown.Text = "NtsPanel2"
    '
    'cmdOpenPdf
    '
    Me.cmdOpenPdf.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdOpenPdf.Image = CType(resources.GetObject("cmdOpenPdf.Image"), System.Drawing.Image)
    Me.cmdOpenPdf.ImageText = ""
    Me.cmdOpenPdf.Location = New System.Drawing.Point(92, 3)
    Me.cmdOpenPdf.Name = "cmdOpenPdf"
    Me.cmdOpenPdf.NTSContextMenu = Nothing
    Me.cmdOpenPdf.Size = New System.Drawing.Size(36, 24)
    Me.cmdOpenPdf.TabIndex = 3
    Me.cmdOpenPdf.TabStop = False
    Me.cmdOpenPdf.ToolTip = "Apri PDF"
    '
    'cmdRimuoviPDF
    '
    Me.cmdRimuoviPDF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdRimuoviPDF.Image = CType(resources.GetObject("cmdRimuoviPDF.Image"), System.Drawing.Image)
    Me.cmdRimuoviPDF.ImageText = ""
    Me.cmdRimuoviPDF.Location = New System.Drawing.Point(176, 3)
    Me.cmdRimuoviPDF.Name = "cmdRimuoviPDF"
    Me.cmdRimuoviPDF.NTSContextMenu = Nothing
    Me.cmdRimuoviPDF.Size = New System.Drawing.Size(36, 24)
    Me.cmdRimuoviPDF.TabIndex = 2
    Me.cmdRimuoviPDF.TabStop = False
    Me.cmdRimuoviPDF.ToolTip = "Rimuovi PDF"
    '
    'cmdSfogliaPdf
    '
    Me.cmdSfogliaPdf.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdSfogliaPdf.Image = CType(resources.GetObject("cmdSfogliaPdf.Image"), System.Drawing.Image)
    Me.cmdSfogliaPdf.ImageText = ""
    Me.cmdSfogliaPdf.Location = New System.Drawing.Point(134, 3)
    Me.cmdSfogliaPdf.Name = "cmdSfogliaPdf"
    Me.cmdSfogliaPdf.NTSContextMenu = Nothing
    Me.cmdSfogliaPdf.Size = New System.Drawing.Size(36, 24)
    Me.cmdSfogliaPdf.TabIndex = 2
    Me.cmdSfogliaPdf.TabStop = False
    Me.cmdSfogliaPdf.ToolTip = "Aggiungi PDF"
    '
    'trPDF
    '
    Me.trPDF.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.trPDF.HideSelection = False
    Me.trPDF.Location = New System.Drawing.Point(0, 28)
    Me.trPDF.Name = "trPDF"
    Me.trPDF.ShowLines = False
    Me.trPDF.ShowPlusMinus = False
    Me.trPDF.ShowRootLines = False
    Me.trPDF.Size = New System.Drawing.Size(215, 348)
    Me.trPDF.TabIndex = 1
    '
    'TabPage0
    '
    Me.TabPage0.AllowDrop = True
    Me.TabPage0.Controls.Add(Me.pnContenuto)
    Me.TabPage0.Enable = True
    Me.TabPage0.Name = "TabPage0"
    Me.TabPage0.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage0.Size = New System.Drawing.Size(215, 376)
    Me.TabPage0.Text = "Contenuto"
    '
    'pnContenuto
    '
    Me.pnContenuto.AllowDrop = True
    Me.pnContenuto.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnContenuto.Appearance.Options.UseBackColor = True
    Me.pnContenuto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnContenuto.Controls.Add(Me.pnContenutoUp)
    Me.pnContenuto.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnContenuto.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnContenuto.Location = New System.Drawing.Point(3, 3)
    Me.pnContenuto.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnContenuto.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnContenuto.Name = "pnContenuto"
    Me.pnContenuto.NTSActiveTrasparency = True
    Me.pnContenuto.Size = New System.Drawing.Size(209, 370)
    Me.pnContenuto.TabIndex = 0
    Me.pnContenuto.Text = "NtsPanel1"
    '
    'pnContenutoUp
    '
    Me.pnContenutoUp.AllowDrop = True
    Me.pnContenutoUp.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnContenutoUp.Appearance.Options.UseBackColor = True
    Me.pnContenutoUp.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnContenutoUp.Controls.Add(Me.trContenuto)
    Me.pnContenutoUp.Controls.Add(Me.cmdComprimi)
    Me.pnContenutoUp.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnContenutoUp.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnContenutoUp.Location = New System.Drawing.Point(0, 0)
    Me.pnContenutoUp.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnContenutoUp.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnContenutoUp.Name = "pnContenutoUp"
    Me.pnContenutoUp.NTSActiveTrasparency = True
    Me.pnContenutoUp.Size = New System.Drawing.Size(209, 370)
    Me.pnContenutoUp.TabIndex = 3
    Me.pnContenutoUp.Text = "NtsPanel2"
    '
    'trContenuto
    '
    Me.trContenuto.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.trContenuto.HideSelection = False
    Me.trContenuto.ImageIndex = 0
    Me.trContenuto.ImageList = Me.ImageList1
    Me.trContenuto.Location = New System.Drawing.Point(0, 0)
    Me.trContenuto.Name = "trContenuto"
    Me.trContenuto.SelectedImageIndex = 0
    Me.trContenuto.Size = New System.Drawing.Size(209, 333)
    Me.trContenuto.TabIndex = 0
    '
    'ImageList1
    '
    Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
    Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
    Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
    '
    'cmdComprimi
    '
    Me.cmdComprimi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdComprimi.ImageText = ""
    Me.cmdComprimi.Location = New System.Drawing.Point(107, 339)
    Me.cmdComprimi.Name = "cmdComprimi"
    Me.cmdComprimi.NTSContextMenu = Nothing
    Me.cmdComprimi.Size = New System.Drawing.Size(99, 24)
    Me.cmdComprimi.TabIndex = 2
    Me.cmdComprimi.TabStop = False
    Me.cmdComprimi.Text = "Comprimi tutti"
    '
    'TabPage2
    '
    Me.TabPage2.AllowDrop = True
    Me.TabPage2.Controls.Add(Me.pnCerca)
    Me.TabPage2.Enable = True
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage2.Size = New System.Drawing.Size(215, 376)
    Me.TabPage2.Text = "Cerca"
    '
    'pnCerca
    '
    Me.pnCerca.AllowDrop = True
    Me.pnCerca.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCerca.Appearance.Options.UseBackColor = True
    Me.pnCerca.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCerca.Controls.Add(Me.lsbProposta)
    Me.pnCerca.Controls.Add(Me.cmdCerca)
    Me.pnCerca.Controls.Add(Me.ckTitoli)
    Me.pnCerca.Controls.Add(Me.edTrova)
    Me.pnCerca.Controls.Add(Me.liTrova)
    Me.pnCerca.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCerca.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnCerca.Location = New System.Drawing.Point(3, 3)
    Me.pnCerca.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCerca.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCerca.Name = "pnCerca"
    Me.pnCerca.NTSActiveTrasparency = True
    Me.pnCerca.Size = New System.Drawing.Size(209, 370)
    Me.pnCerca.TabIndex = 1
    Me.pnCerca.Text = "NtsPanel1"
    '
    'lsbProposta
    '
    Me.lsbProposta.Cursor = System.Windows.Forms.Cursors.Default
    Me.lsbProposta.ItemHeight = 14
    Me.lsbProposta.Location = New System.Drawing.Point(12, 24)
    Me.lsbProposta.Name = "lsbProposta"
    Me.lsbProposta.NTSDbField = ""
    Me.lsbProposta.ShowToolTips = False
    Me.lsbProposta.Size = New System.Drawing.Size(194, 110)
    Me.lsbProposta.TabIndex = 33
    Me.lsbProposta.Visible = False
    '
    'cmdCerca
    '
    Me.cmdCerca.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdCerca.ImageText = ""
    Me.cmdCerca.Location = New System.Drawing.Point(163, 5)
    Me.cmdCerca.Name = "cmdCerca"
    Me.cmdCerca.NTSContextMenu = Nothing
    Me.cmdCerca.Size = New System.Drawing.Size(43, 20)
    Me.cmdCerca.TabIndex = 6
    Me.cmdCerca.Text = "Cerca"
    '
    'ckTitoli
    '
    Me.ckTitoli.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckTitoli.Location = New System.Drawing.Point(6, 32)
    Me.ckTitoli.Name = "ckTitoli"
    Me.ckTitoli.NTSCheckValue = "S"
    Me.ckTitoli.NTSUnCheckValue = "N"
    Me.ckTitoli.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckTitoli.Properties.Appearance.Options.UseBackColor = True
    Me.ckTitoli.Properties.AutoHeight = False
    Me.ckTitoli.Properties.Caption = "Cerca solo nei titoli"
    Me.ckTitoli.Size = New System.Drawing.Size(116, 19)
    Me.ckTitoli.TabIndex = 7
    '
    'edTrova
    '
    Me.edTrova.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.edTrova.Cursor = System.Windows.Forms.Cursors.Default
    Me.edTrova.EditValue = ""
    Me.edTrova.Location = New System.Drawing.Point(6, 5)
    Me.edTrova.Name = "edTrova"
    Me.edTrova.NTSDbField = ""
    Me.edTrova.NTSForzaVisZoom = False
    Me.edTrova.NTSOldValue = ""
    Me.edTrova.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black
    Me.edTrova.Properties.AppearanceDisabled.Options.UseForeColor = True
    Me.edTrova.Properties.AutoHeight = False
    Me.edTrova.Properties.MaxLength = 65536
    Me.edTrova.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edTrova.Size = New System.Drawing.Size(153, 20)
    Me.edTrova.TabIndex = 5
    Me.edTrova.TextStr = ""
    '
    'liTrova
    '
    Me.liTrova.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.liTrova.Cursor = System.Windows.Forms.Cursors.Default
    Me.liTrova.ItemHeight = 14
    Me.liTrova.Location = New System.Drawing.Point(3, 57)
    Me.liTrova.Name = "liTrova"
    Me.liTrova.NTSDbField = ""
    Me.liTrova.Size = New System.Drawing.Size(203, 310)
    Me.liTrova.TabIndex = 7
    '
    'TabPage1
    '
    Me.TabPage1.AllowDrop = True
    Me.TabPage1.Controls.Add(Me.pnCronologia)
    Me.TabPage1.Enable = True
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Size = New System.Drawing.Size(215, 376)
    Me.TabPage1.Text = "Cronologia"
    '
    'pnCronologia
    '
    Me.pnCronologia.AllowDrop = True
    Me.pnCronologia.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnCronologia.Appearance.Options.UseBackColor = True
    Me.pnCronologia.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnCronologia.Controls.Add(Me.cmdCancellaCronologia)
    Me.pnCronologia.Controls.Add(Me.trCronologia)
    Me.pnCronologia.Controls.Add(Me.cmdComprimiCronologia)
    Me.pnCronologia.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnCronologia.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnCronologia.Location = New System.Drawing.Point(0, 0)
    Me.pnCronologia.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnCronologia.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnCronologia.Name = "pnCronologia"
    Me.pnCronologia.NTSActiveTrasparency = True
    Me.pnCronologia.Size = New System.Drawing.Size(215, 376)
    Me.pnCronologia.TabIndex = 1
    Me.pnCronologia.Text = "NtsPanel1"
    '
    'cmdCancellaCronologia
    '
    Me.cmdCancellaCronologia.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdCancellaCronologia.ImageText = ""
    Me.cmdCancellaCronologia.Location = New System.Drawing.Point(0, 352)
    Me.cmdCancellaCronologia.Name = "cmdCancellaCronologia"
    Me.cmdCancellaCronologia.NTSContextMenu = Nothing
    Me.cmdCancellaCronologia.Size = New System.Drawing.Size(108, 24)
    Me.cmdCancellaCronologia.TabIndex = 3
    Me.cmdCancellaCronologia.TabStop = False
    Me.cmdCancellaCronologia.Text = "Cancella cronologia"
    '
    'trCronologia
    '
    Me.trCronologia.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.trCronologia.HideSelection = False
    Me.trCronologia.ImageIndex = 0
    Me.trCronologia.ImageList = Me.ImageList1
    Me.trCronologia.Location = New System.Drawing.Point(0, 0)
    Me.trCronologia.Name = "trCronologia"
    Me.trCronologia.SelectedImageIndex = 0
    Me.trCronologia.Size = New System.Drawing.Size(215, 346)
    Me.trCronologia.TabIndex = 0
    '
    'cmdComprimiCronologia
    '
    Me.cmdComprimiCronologia.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdComprimiCronologia.ImageText = ""
    Me.cmdComprimiCronologia.Location = New System.Drawing.Point(116, 352)
    Me.cmdComprimiCronologia.Name = "cmdComprimiCronologia"
    Me.cmdComprimiCronologia.NTSContextMenu = Nothing
    Me.cmdComprimiCronologia.Size = New System.Drawing.Size(99, 24)
    Me.cmdComprimiCronologia.TabIndex = 2
    Me.cmdComprimiCronologia.TabStop = False
    Me.cmdComprimiCronologia.Text = "Comprimi tutti"
    '
    'tsView
    '
    Me.tsView.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsView.Location = New System.Drawing.Point(0, 88)
    Me.tsView.Name = "tsView"
    Me.tsView.SelectedTabPage = Me.TabPageView0
    Me.tsView.Size = New System.Drawing.Size(472, 318)
    Me.tsView.TabIndex = 1
    Me.tsView.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabPageView0, Me.TabPageView1})
    Me.tsView.Text = "NtsTabControl1"
    '
    'TabPageView0
    '
    Me.TabPageView0.AllowDrop = True
    Me.TabPageView0.Controls.Add(Me.pnWebFill)
    Me.TabPageView0.Enable = True
    Me.TabPageView0.Name = "TabPageView0"
    Me.TabPageView0.Size = New System.Drawing.Size(463, 286)
    Me.TabPageView0.Text = "Business NET Help"
    '
    'pnWebFill
    '
    Me.pnWebFill.AllowDrop = True
    Me.pnWebFill.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnWebFill.Appearance.Options.UseBackColor = True
    Me.pnWebFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnWebFill.Controls.Add(Me.WebBrows)
    Me.pnWebFill.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnWebFill.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnWebFill.Location = New System.Drawing.Point(0, 0)
    Me.pnWebFill.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnWebFill.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnWebFill.Name = "pnWebFill"
    Me.pnWebFill.NTSActiveTrasparency = True
    Me.pnWebFill.Size = New System.Drawing.Size(463, 286)
    Me.pnWebFill.TabIndex = 1
    Me.pnWebFill.Text = "NtsPanel2"
    '
    'WebBrows
    '
    Me.WebBrows.Dock = System.Windows.Forms.DockStyle.Fill
    Me.WebBrows.IsWebBrowserContextMenuEnabled = False
    Me.WebBrows.Location = New System.Drawing.Point(0, 0)
    Me.WebBrows.MinimumSize = New System.Drawing.Size(20, 20)
    Me.WebBrows.Name = "WebBrows"
    Me.WebBrows.ScriptErrorsSuppressed = True
    Me.WebBrows.Size = New System.Drawing.Size(463, 286)
    Me.WebBrows.TabIndex = 0
    '
    'TabPageView1
    '
    Me.TabPageView1.AllowDrop = True
    Me.TabPageView1.Controls.Add(Me.pnAggURL)
    Me.TabPageView1.Enable = True
    Me.TabPageView1.Image = CType(resources.GetObject("TabPageView1.Image"), System.Drawing.Image)
    Me.TabPageView1.Name = "TabPageView1"
    Me.TabPageView1.Size = New System.Drawing.Size(463, 286)
    Me.TabPageView1.Text = "Aggiungi URL"
    '
    'pnAggURL
    '
    Me.pnAggURL.AllowDrop = True
    Me.pnAggURL.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAggURL.Appearance.Options.UseBackColor = True
    Me.pnAggURL.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAggURL.Controls.Add(Me.WebBrowsAgg)
    Me.pnAggURL.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAggURL.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAggURL.Location = New System.Drawing.Point(0, 0)
    Me.pnAggURL.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnAggURL.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnAggURL.Name = "pnAggURL"
    Me.pnAggURL.NTSActiveTrasparency = True
    Me.pnAggURL.Size = New System.Drawing.Size(463, 286)
    Me.pnAggURL.TabIndex = 0
    '
    'WebBrowsAgg
    '
    Me.WebBrowsAgg.Dock = System.Windows.Forms.DockStyle.Fill
    Me.WebBrowsAgg.IsWebBrowserContextMenuEnabled = False
    Me.WebBrowsAgg.Location = New System.Drawing.Point(0, 0)
    Me.WebBrowsAgg.MinimumSize = New System.Drawing.Size(20, 20)
    Me.WebBrowsAgg.Name = "WebBrowsAgg"
    Me.WebBrowsAgg.ScriptErrorsSuppressed = True
    Me.WebBrowsAgg.Size = New System.Drawing.Size(463, 286)
    Me.WebBrowsAgg.TabIndex = 1
    '
    'cmdDelURL
    '
    Me.cmdDelURL.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdDelURL.Image = CType(resources.GetObject("cmdDelURL.Image"), System.Drawing.Image)
    Me.cmdDelURL.ImageText = ""
    Me.cmdDelURL.Location = New System.Drawing.Point(450, 0)
    Me.cmdDelURL.Name = "cmdDelURL"
    Me.cmdDelURL.NTSContextMenu = Nothing
    Me.cmdDelURL.Size = New System.Drawing.Size(21, 21)
    Me.cmdDelURL.TabIndex = 2
    Me.cmdDelURL.ToolTip = "Premere per rimuovere l'URL aggiunto"
    '
    'pnEdit
    '
    Me.pnEdit.AllowDrop = True
    Me.pnEdit.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnEdit.Appearance.Options.UseBackColor = True
    Me.pnEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnEdit.Controls.Add(Me.lbPage)
    Me.pnEdit.Controls.Add(Me.cmdBookmark)
    Me.pnEdit.Controls.Add(Me.ckItalic)
    Me.pnEdit.Controls.Add(Me.ckUnderline)
    Me.pnEdit.Controls.Add(Me.ckBold)
    Me.pnEdit.Controls.Add(Me.cmdConfig)
    Me.pnEdit.Controls.Add(Me.cmdRecuperaHTML)
    Me.pnEdit.Controls.Add(Me.cmdEditor)
    Me.pnEdit.Controls.Add(Me.cbFontDim)
    Me.pnEdit.Controls.Add(Me.cbFont)
    Me.pnEdit.Controls.Add(Me.cmdInsTabe)
    Me.pnEdit.Controls.Add(Me.cmdListaNum)
    Me.pnEdit.Controls.Add(Me.cmdAcapo)
    Me.pnEdit.Controls.Add(Me.cmdLista)
    Me.pnEdit.Controls.Add(Me.cmdControlli)
    Me.pnEdit.Controls.Add(Me.cmdImage)
    Me.pnEdit.Controls.Add(Me.cmdEndEdit)
    Me.pnEdit.Controls.Add(Me.cmdLink)
    Me.pnEdit.Controls.Add(Me.cmdAnnulla)
    Me.pnEdit.Controls.Add(Me.cmdSave)
    Me.pnEdit.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnEdit.Dock = System.Windows.Forms.DockStyle.Top
    Me.pnEdit.Location = New System.Drawing.Point(0, 0)
    Me.pnEdit.Name = "pnEdit"
    Me.pnEdit.NTSActiveTrasparency = True
    Me.pnEdit.Size = New System.Drawing.Size(472, 88)
    Me.pnEdit.TabIndex = 8
    Me.pnEdit.Text = "NtsPanel1"
    '
    'lbPage
    '
    Me.lbPage.AutoSize = True
    Me.lbPage.Location = New System.Drawing.Point(277, 8)
    Me.lbPage.Name = "lbPage"
    Me.lbPage.Size = New System.Drawing.Size(54, 13)
    Me.lbPage.TabIndex = 16
    Me.lbPage.Text = "pagina ..."
    '
    'cmdBookmark
    '
    Me.cmdBookmark.Image = CType(resources.GetObject("cmdBookmark.Image"), System.Drawing.Image)
    Me.cmdBookmark.ImageText = ""
    Me.cmdBookmark.Location = New System.Drawing.Point(455, 46)
    Me.cmdBookmark.Name = "cmdBookmark"
    Me.cmdBookmark.NTSContextMenu = Nothing
    Me.cmdBookmark.Size = New System.Drawing.Size(35, 35)
    Me.cmdBookmark.TabIndex = 15
    Me.cmdBookmark.TabStop = False
    Me.cmdBookmark.ToolTip = "Mostra/nascondi appunti"
    '
    'ckItalic
    '
    Me.ckItalic.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckItalic.Location = New System.Drawing.Point(347, 34)
    Me.ckItalic.Name = "ckItalic"
    Me.ckItalic.NTSCheckValue = "S"
    Me.ckItalic.NTSUnCheckValue = "N"
    Me.ckItalic.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckItalic.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic)
    Me.ckItalic.Properties.Appearance.Options.UseBackColor = True
    Me.ckItalic.Properties.Appearance.Options.UseFont = True
    Me.ckItalic.Properties.AutoHeight = False
    Me.ckItalic.Properties.Caption = "C"
    Me.ckItalic.Size = New System.Drawing.Size(33, 19)
    Me.ckItalic.TabIndex = 14
    '
    'ckUnderline
    '
    Me.ckUnderline.Cursor = System.Windows.Forms.Cursors.Hand
    Me.ckUnderline.Location = New System.Drawing.Point(386, 34)
    Me.ckUnderline.Name = "ckUnderline"
    Me.ckUnderline.NTSCheckValue = "S"
    Me.ckUnderline.NTSUnCheckValue = "N"
    Me.ckUnderline.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckUnderline.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Underline)
    Me.ckUnderline.Properties.Appearance.Options.UseBackColor = True
    Me.ckUnderline.Properties.Appearance.Options.UseFont = True
    Me.ckUnderline.Properties.AutoHeight = False
    Me.ckUnderline.Properties.Caption = "S"
    Me.ckUnderline.Size = New System.Drawing.Size(33, 19)
    Me.ckUnderline.TabIndex = 13
    '
    'ckBold
    '
    Me.ckBold.Cursor = System.Windows.Forms.Cursors.Default
    Me.ckBold.Location = New System.Drawing.Point(308, 34)
    Me.ckBold.Name = "ckBold"
    Me.ckBold.NTSCheckValue = "S"
    Me.ckBold.NTSUnCheckValue = "N"
    Me.ckBold.Properties.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.ckBold.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
    Me.ckBold.Properties.Appearance.Options.UseBackColor = True
    Me.ckBold.Properties.Appearance.Options.UseFont = True
    Me.ckBold.Properties.AutoHeight = False
    Me.ckBold.Properties.Caption = "G"
    Me.ckBold.Size = New System.Drawing.Size(33, 19)
    Me.ckBold.TabIndex = 12
    '
    'cmdConfig
    '
    Me.cmdConfig.Enabled = False
    Me.cmdConfig.Image = CType(resources.GetObject("cmdConfig.Image"), System.Drawing.Image)
    Me.cmdConfig.ImageText = ""
    Me.cmdConfig.Location = New System.Drawing.Point(327, 57)
    Me.cmdConfig.Name = "cmdConfig"
    Me.cmdConfig.NTSContextMenu = Nothing
    Me.cmdConfig.Size = New System.Drawing.Size(29, 24)
    Me.cmdConfig.TabIndex = 1
    Me.cmdConfig.TabStop = False
    Me.cmdConfig.Visible = False
    '
    'cmdRecuperaHTML
    '
    Me.cmdRecuperaHTML.Image = CType(resources.GetObject("cmdRecuperaHTML.Image"), System.Drawing.Image)
    Me.cmdRecuperaHTML.ImageText = ""
    Me.cmdRecuperaHTML.Location = New System.Drawing.Point(130, 3)
    Me.cmdRecuperaHTML.Name = "cmdRecuperaHTML"
    Me.cmdRecuperaHTML.NTSContextMenu = Nothing
    Me.cmdRecuperaHTML.Size = New System.Drawing.Size(141, 24)
    Me.cmdRecuperaHTML.TabIndex = 1
    Me.cmdRecuperaHTML.TabStop = False
    Me.cmdRecuperaHTML.Text = "Recupera argomento"
    '
    'cmdEditor
    '
    Me.cmdEditor.Enabled = False
    Me.cmdEditor.Image = CType(resources.GetObject("cmdEditor.Image"), System.Drawing.Image)
    Me.cmdEditor.ImageText = ""
    Me.cmdEditor.Location = New System.Drawing.Point(254, 57)
    Me.cmdEditor.Name = "cmdEditor"
    Me.cmdEditor.NTSContextMenu = Nothing
    Me.cmdEditor.Size = New System.Drawing.Size(67, 24)
    Me.cmdEditor.TabIndex = 1
    Me.cmdEditor.TabStop = False
    Me.cmdEditor.Text = "Editor"
    Me.cmdEditor.Visible = False
    '
    'cbFontDim
    '
    Me.cbFontDim.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbFontDim.DataSource = Nothing
    Me.cbFontDim.DisplayMember = ""
    Me.cbFontDim.Enabled = False
    Me.cbFontDim.Location = New System.Drawing.Point(539, 34)
    Me.cbFontDim.Name = "cbFontDim"
    Me.cbFontDim.NTSDbField = ""
    Me.cbFontDim.Properties.AutoHeight = False
    Me.cbFontDim.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbFontDim.Properties.DropDownRows = 30
    Me.cbFontDim.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbFontDim.SelectedValue = ""
    Me.cbFontDim.Size = New System.Drawing.Size(29, 20)
    Me.cbFontDim.TabIndex = 11
    Me.cbFontDim.ValueMember = ""
    Me.cbFontDim.Visible = False
    '
    'cbFont
    '
    Me.cbFont.Cursor = System.Windows.Forms.Cursors.Default
    Me.cbFont.DataSource = Nothing
    Me.cbFont.DisplayMember = ""
    Me.cbFont.Enabled = False
    Me.cbFont.Location = New System.Drawing.Point(539, 58)
    Me.cbFont.Name = "cbFont"
    Me.cbFont.NTSDbField = ""
    Me.cbFont.Properties.AutoHeight = False
    Me.cbFont.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
    Me.cbFont.Properties.DropDownRows = 30
    Me.cbFont.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
    Me.cbFont.SelectedValue = ""
    Me.cbFont.Size = New System.Drawing.Size(29, 20)
    Me.cbFont.TabIndex = 10
    Me.cbFont.ValueMember = ""
    Me.cbFont.Visible = False
    '
    'cmdInsTabe
    '
    Me.cmdInsTabe.Image = CType(resources.GetObject("cmdInsTabe.Image"), System.Drawing.Image)
    Me.cmdInsTabe.ImageText = ""
    Me.cmdInsTabe.Location = New System.Drawing.Point(6, 3)
    Me.cmdInsTabe.Name = "cmdInsTabe"
    Me.cmdInsTabe.NTSContextMenu = Nothing
    Me.cmdInsTabe.Size = New System.Drawing.Size(118, 24)
    Me.cmdInsTabe.TabIndex = 7
    Me.cmdInsTabe.TabStop = False
    Me.cmdInsTabe.Text = "Inserisci Tabella"
    '
    'cmdListaNum
    '
    Me.cmdListaNum.Image = CType(resources.GetObject("cmdListaNum.Image"), System.Drawing.Image)
    Me.cmdListaNum.ImageText = ""
    Me.cmdListaNum.Location = New System.Drawing.Point(6, 57)
    Me.cmdListaNum.Name = "cmdListaNum"
    Me.cmdListaNum.NTSContextMenu = Nothing
    Me.cmdListaNum.Size = New System.Drawing.Size(118, 24)
    Me.cmdListaNum.TabIndex = 7
    Me.cmdListaNum.TabStop = False
    Me.cmdListaNum.Text = "Inserisci Opzione"
    '
    'cmdAcapo
    '
    Me.cmdAcapo.ImageText = ""
    Me.cmdAcapo.Location = New System.Drawing.Point(254, 30)
    Me.cmdAcapo.Name = "cmdAcapo"
    Me.cmdAcapo.NTSContextMenu = Nothing
    Me.cmdAcapo.Size = New System.Drawing.Size(48, 24)
    Me.cmdAcapo.TabIndex = 7
    Me.cmdAcapo.TabStop = False
    Me.cmdAcapo.Text = "A capo"
    '
    'cmdLista
    '
    Me.cmdLista.Image = CType(resources.GetObject("cmdLista.Image"), System.Drawing.Image)
    Me.cmdLista.ImageText = ""
    Me.cmdLista.Location = New System.Drawing.Point(130, 30)
    Me.cmdLista.Name = "cmdLista"
    Me.cmdLista.NTSContextMenu = Nothing
    Me.cmdLista.Size = New System.Drawing.Size(118, 24)
    Me.cmdLista.TabIndex = 7
    Me.cmdLista.TabStop = False
    Me.cmdLista.Text = "Inserisci Elenco"
    '
    'cmdControlli
    '
    Me.cmdControlli.Enabled = False
    Me.cmdControlli.ImageText = ""
    Me.cmdControlli.Location = New System.Drawing.Point(362, 57)
    Me.cmdControlli.Name = "cmdControlli"
    Me.cmdControlli.NTSContextMenu = Nothing
    Me.cmdControlli.Size = New System.Drawing.Size(65, 24)
    Me.cmdControlli.TabIndex = 7
    Me.cmdControlli.TabStop = False
    Me.cmdControlli.Text = "Controlli"
    Me.cmdControlli.Visible = False
    '
    'cmdImage
    '
    Me.cmdImage.Image = CType(resources.GetObject("cmdImage.Image"), System.Drawing.Image)
    Me.cmdImage.ImageText = ""
    Me.cmdImage.Location = New System.Drawing.Point(130, 57)
    Me.cmdImage.Name = "cmdImage"
    Me.cmdImage.NTSContextMenu = Nothing
    Me.cmdImage.Size = New System.Drawing.Size(118, 24)
    Me.cmdImage.TabIndex = 7
    Me.cmdImage.TabStop = False
    Me.cmdImage.Text = "Inserisci IMG"
    '
    'cmdEndEdit
    '
    Me.cmdEndEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdEndEdit.Image = CType(resources.GetObject("cmdEndEdit.Image"), System.Drawing.Image)
    Me.cmdEndEdit.ImageText = ""
    Me.cmdEndEdit.Location = New System.Drawing.Point(377, 57)
    Me.cmdEndEdit.Name = "cmdEndEdit"
    Me.cmdEndEdit.NTSContextMenu = Nothing
    Me.cmdEndEdit.Size = New System.Drawing.Size(91, 24)
    Me.cmdEndEdit.TabIndex = 6
    Me.cmdEndEdit.TabStop = False
    Me.cmdEndEdit.Text = "Chiudi edit"
    '
    'cmdLink
    '
    Me.cmdLink.Image = CType(resources.GetObject("cmdLink.Image"), System.Drawing.Image)
    Me.cmdLink.ImageText = ""
    Me.cmdLink.Location = New System.Drawing.Point(6, 30)
    Me.cmdLink.Name = "cmdLink"
    Me.cmdLink.NTSContextMenu = Nothing
    Me.cmdLink.Size = New System.Drawing.Size(118, 24)
    Me.cmdLink.TabIndex = 5
    Me.cmdLink.TabStop = False
    Me.cmdLink.Text = "Inserisci Link"
    '
    'cmdAnnulla
    '
    Me.cmdAnnulla.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdAnnulla.Image = CType(resources.GetObject("cmdAnnulla.Image"), System.Drawing.Image)
    Me.cmdAnnulla.ImageText = ""
    Me.cmdAnnulla.Location = New System.Drawing.Point(377, 30)
    Me.cmdAnnulla.Name = "cmdAnnulla"
    Me.cmdAnnulla.NTSContextMenu = Nothing
    Me.cmdAnnulla.Size = New System.Drawing.Size(91, 24)
    Me.cmdAnnulla.TabIndex = 2
    Me.cmdAnnulla.TabStop = False
    Me.cmdAnnulla.Text = "Ripristina"
    '
    'cmdSave
    '
    Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
    Me.cmdSave.ImageText = ""
    Me.cmdSave.Location = New System.Drawing.Point(377, 3)
    Me.cmdSave.Name = "cmdSave"
    Me.cmdSave.NTSContextMenu = Nothing
    Me.cmdSave.Size = New System.Drawing.Size(91, 24)
    Me.cmdSave.TabIndex = 1
    Me.cmdSave.TabStop = False
    Me.cmdSave.Text = "Salva"
    '
    'NtsSplitter1
    '
    Me.NtsSplitter1.BackColor = System.Drawing.SystemColors.ControlDark
    Me.NtsSplitter1.Location = New System.Drawing.Point(224, 37)
    Me.NtsSplitter1.Name = "NtsSplitter1"
    Me.NtsSplitter1.Size = New System.Drawing.Size(3, 406)
    Me.NtsSplitter1.TabIndex = 1
    Me.NtsSplitter1.TabStop = False
    '
    'tmTimer
    '
    Me.tmTimer.Interval = 200
    '
    'pnLeft
    '
    Me.pnLeft.AllowDrop = True
    Me.pnLeft.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnLeft.Appearance.Options.UseBackColor = True
    Me.pnLeft.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnLeft.Controls.Add(Me.tsHelp)
    Me.pnLeft.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnLeft.Dock = System.Windows.Forms.DockStyle.Left
    Me.pnLeft.Location = New System.Drawing.Point(0, 37)
    Me.pnLeft.Name = "pnLeft"
    Me.pnLeft.NTSActiveTrasparency = True
    Me.pnLeft.Size = New System.Drawing.Size(224, 406)
    Me.pnLeft.TabIndex = 3
    '
    'pnRight
    '
    Me.pnRight.AllowDrop = True
    Me.pnRight.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnRight.Appearance.Options.UseBackColor = True
    Me.pnRight.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnRight.Controls.Add(Me.tsAppunti)
    Me.pnRight.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnRight.Dock = System.Windows.Forms.DockStyle.Right
    Me.pnRight.Location = New System.Drawing.Point(702, 37)
    Me.pnRight.Name = "pnRight"
    Me.pnRight.NTSActiveTrasparency = True
    Me.pnRight.Size = New System.Drawing.Size(194, 406)
    Me.pnRight.TabIndex = 5
    Me.pnRight.Text = "NtsPanel2"
    '
    'tsAppunti
    '
    Me.tsAppunti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tsAppunti.Location = New System.Drawing.Point(0, 0)
    Me.tsAppunti.Name = "tsAppunti"
    Me.tsAppunti.SelectedTabPage = Me.TabPageAppunti0
    Me.tsAppunti.Size = New System.Drawing.Size(194, 406)
    Me.tsAppunti.TabIndex = 0
    Me.tsAppunti.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.TabPageAppunti0})
    Me.tsAppunti.Text = "NtsTabControl1"
    '
    'TabPageAppunti0
    '
    Me.TabPageAppunti0.AllowDrop = True
    Me.TabPageAppunti0.Controls.Add(Me.pnAppunti)
    Me.TabPageAppunti0.Enable = True
    Me.TabPageAppunti0.Name = "TabPageAppunti0"
    Me.TabPageAppunti0.Size = New System.Drawing.Size(185, 376)
    Me.TabPageAppunti0.Text = "Appunti"
    '
    'pnAppunti
    '
    Me.pnAppunti.AllowDrop = True
    Me.pnAppunti.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnAppunti.Appearance.Options.UseBackColor = True
    Me.pnAppunti.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnAppunti.Controls.Add(Me.edAppunti)
    Me.pnAppunti.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnAppunti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnAppunti.Location = New System.Drawing.Point(0, 0)
    Me.pnAppunti.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat
    Me.pnAppunti.LookAndFeel.UseDefaultLookAndFeel = False
    Me.pnAppunti.Name = "pnAppunti"
    Me.pnAppunti.NTSActiveTrasparency = True
    Me.pnAppunti.Size = New System.Drawing.Size(185, 376)
    Me.pnAppunti.TabIndex = 1
    Me.pnAppunti.Text = "NtsPanel2"
    '
    'edAppunti
    '
    Me.edAppunti.Cursor = System.Windows.Forms.Cursors.Default
    Me.edAppunti.Dock = System.Windows.Forms.DockStyle.Fill
    Me.edAppunti.Location = New System.Drawing.Point(0, 0)
    Me.edAppunti.Name = "edAppunti"
    Me.edAppunti.NTSDbField = ""
    Me.edAppunti.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.edAppunti.Size = New System.Drawing.Size(185, 376)
    Me.edAppunti.TabIndex = 0
    '
    'NtsSplitter3
    '
    Me.NtsSplitter3.BackColor = System.Drawing.SystemColors.ControlDark
    Me.NtsSplitter3.Dock = System.Windows.Forms.DockStyle.Right
    Me.NtsSplitter3.Location = New System.Drawing.Point(699, 37)
    Me.NtsSplitter3.Name = "NtsSplitter3"
    Me.NtsSplitter3.Size = New System.Drawing.Size(3, 406)
    Me.NtsSplitter3.TabIndex = 6
    Me.NtsSplitter3.TabStop = False
    '
    'pnFill
    '
    Me.pnFill.AllowDrop = True
    Me.pnFill.Appearance.BackColor = System.Drawing.Color.Transparent
    Me.pnFill.Appearance.Options.UseBackColor = True
    Me.pnFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
    Me.pnFill.Controls.Add(Me.lbFindInPage)
    Me.pnFill.Controls.Add(Me.cmdDelURL)
    Me.pnFill.Controls.Add(Me.tsView)
    Me.pnFill.Controls.Add(Me.pnEdit)
    Me.pnFill.Cursor = System.Windows.Forms.Cursors.Default
    Me.pnFill.Dock = System.Windows.Forms.DockStyle.Fill
    Me.pnFill.Location = New System.Drawing.Point(227, 37)
    Me.pnFill.Name = "pnFill"
    Me.pnFill.NTSActiveTrasparency = True
    Me.pnFill.Size = New System.Drawing.Size(472, 406)
    Me.pnFill.TabIndex = 7
    Me.pnFill.Text = "NtsPanel2"
    '
    'lbFindInPage
    '
    Me.lbFindInPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lbFindInPage.AutoSize = True
    Me.lbFindInPage.Location = New System.Drawing.Point(192, 4)
    Me.lbFindInPage.Name = "lbFindInPage"
    Me.lbFindInPage.Size = New System.Drawing.Size(254, 13)
    Me.lbFindInPage.TabIndex = 16
    Me.lbFindInPage.Text = "Per ricercare all'interno della pagina premere Ctrl+F"
    '
    'FRM__HELP
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(896, 443)
    Me.Controls.Add(Me.pnFill)
    Me.Controls.Add(Me.NtsSplitter3)
    Me.Controls.Add(Me.pnRight)
    Me.Controls.Add(Me.NtsSplitter1)
    Me.Controls.Add(Me.pnLeft)
    Me.Controls.Add(Me.pnTop)
    Me.Cursor = System.Windows.Forms.Cursors.Default
    Me.Name = "FRM__HELP"
    Me.ShowInTaskbar = True
    Me.Text = "BUSINESS NET HELP"
    CType(Me.dttSmartArt, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnTop, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnTop.ResumeLayout(False)
    Me.pnTop.PerformLayout()
    CType(Me.cbTipo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.tsHelp, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsHelp.ResumeLayout(False)
    Me.TabPageFile0.ResumeLayout(False)
    CType(Me.pnContenutoDown, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnContenutoDown.ResumeLayout(False)
    Me.TabPage0.ResumeLayout(False)
    CType(Me.pnContenuto, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnContenuto.ResumeLayout(False)
    CType(Me.pnContenutoUp, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnContenutoUp.ResumeLayout(False)
    Me.TabPage2.ResumeLayout(False)
    CType(Me.pnCerca, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCerca.ResumeLayout(False)
    Me.pnCerca.PerformLayout()
    CType(Me.lsbProposta, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckTitoli.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.edTrova.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.liTrova, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage1.ResumeLayout(False)
    CType(Me.pnCronologia, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnCronologia.ResumeLayout(False)
    CType(Me.tsView, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsView.ResumeLayout(False)
    Me.TabPageView0.ResumeLayout(False)
    CType(Me.pnWebFill, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnWebFill.ResumeLayout(False)
    Me.TabPageView1.ResumeLayout(False)
    CType(Me.pnAggURL, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAggURL.ResumeLayout(False)
    CType(Me.pnEdit, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnEdit.ResumeLayout(False)
    Me.pnEdit.PerformLayout()
    CType(Me.ckItalic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckUnderline.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.ckBold.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbFontDim.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.cbFont.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnLeft, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnLeft.ResumeLayout(False)
    CType(Me.pnRight, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnRight.ResumeLayout(False)
    CType(Me.tsAppunti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tsAppunti.ResumeLayout(False)
    Me.TabPageAppunti0.ResumeLayout(False)
    CType(Me.pnAppunti, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnAppunti.ResumeLayout(False)
    CType(Me.edAppunti.Properties, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.pnFill, System.ComponentModel.ISupportInitialize).EndInit()
    Me.pnFill.ResumeLayout(False)
    Me.pnFill.PerformLayout()
    Me.ResumeLayout(False)

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
    If CLN__STD.NTSIstanziaDll(oApp.ServerDir, oApp.NetDir, "BN__HELP", "BE__BASN", oTmp, strErr, False, "", "") = False Then
      oApp.MsgBoxErr(oApp.Tr(Me, 127791222149375000, "ERRORE in fase di creazione Entity:" & vbCrLf & "|" & strErr & "|"))
      Return False
    End If
    oCleBasn = CType(oTmp, CLE__BASN)
    '------------------------------------------------
    bRemoting = Menu.Remoting("BN__HELP", strRemoteServer, strRemotePort)
    AddHandler oCleBasn.RemoteEvent, AddressOf GestisciEventiEntity
    If oCleBasn.Init(oApp, NTSScript, oMenu.oCleComm, "", bRemoting, strRemoteServer, strRemotePort) = False Then Return False
    '------------------------------------------------
    'povra di una chamata CallByName: 
    'la funzione Pippo è contenuta nella BG__PAGA che eredita da BE__PAGA
    'CallByName(oClePaga, "Pippo", CallType.Method, "Pippo")
    '------------------------------------------------

    strHelpDirectoryLocal = oApp.CmbDir
    Try
      If File.Exists("c:\NTSHelpEditor\2015\help\sommario.hhc") Then
        strHelpDirectoryLocal = "c:\NTSHelpEditor\2015"
      End If
    Catch ex As Exception
      strHelpDirectoryLocal = oApp.CmbDir
    End Try
    Try
      If File.Exists(strHelpDirectoryLocal & "\help\sommario.hhc") Then bHelpLocalExist = True
    Catch ex As Exception
      bHelpLocalExist = False
    End Try
    Return True
  End Function
  Public Overridable Sub InitControls()
    InitControlsBeginEndInit(Me, False)
    Dim dttTipoRk As New DataTable()
    Dim dttFont As New DataTable()
    Dim dttFontDim As New DataTable()
    'Dim fFonts As New Drawing.Text.InstalledFontCollection ' Carica la lista dei font installati
    Dim i As Integer = 0
    Try
      dttTipoRk.Columns.Add("cod", GetType(String))
      dttTipoRk.Columns.Add("val", GetType(String))
      dttTipoRk.Rows.Add(New Object() {"N", "Help Locale"})
      dttTipoRk.Rows.Add(New Object() {"S", "Help OnLine"})
      cbTipo.DataSource = dttTipoRk
      cbTipo.ValueMember = "cod"
      cbTipo.DisplayMember = "val"

      dttFont.Columns.Add("val")
      dttFont.Columns.Add("cod")

      'Dim arFont() As FontFamily = fFonts.Families()

      'For i = 0 To arFont.Length - 1
      dttFont.Rows.Add()
      dttFont.Rows.Add()
      dttFont.Rows(0)!val = "Times New Roman" 'arFont(i).Name
      dttFont.Rows(0)!cod = "Times New Roman" 'arFont(i).Name
      dttFont.Rows(1)!val = "Courier New"
      dttFont.Rows(1)!cod = "Courier New"
      'Next
      dttFont.AcceptChanges()
      cbFont.DataSource = dttFont
      cbFont.ValueMember = "cod"
      cbFont.DisplayMember = "val"

      dttFontDim.Columns.Add("val")
      dttFontDim.Columns.Add("cod")

      dttFontDim.Rows.Add()
      dttFontDim.Rows.Add()
      dttFontDim.Rows.Add()
      dttFontDim.Rows.Add()
      dttFontDim.Rows.Add()
      dttFontDim.Rows.Add()
      dttFontDim.Rows.Add()
      dttFontDim.Rows(0)!cod = 1
      dttFontDim.Rows(0)!val = "8 pt"
      dttFontDim.Rows(1)!cod = 2
      dttFontDim.Rows(1)!val = "10 pt"
      dttFontDim.Rows(2)!cod = 3
      dttFontDim.Rows(2)!val = "12 pt"
      dttFontDim.Rows(3)!cod = 4
      dttFontDim.Rows(3)!val = "14 pt"
      dttFontDim.Rows(4)!cod = 5
      dttFontDim.Rows(4)!val = "18 pt"
      dttFontDim.Rows(5)!cod = 6
      dttFontDim.Rows(5)!val = "24 pt"
      dttFontDim.Rows(6)!cod = 7
      dttFontDim.Rows(6)!val = "36 pt"
      dttFontDim.AcceptChanges()
      cbFontDim.DataSource = dttFontDim
      cbFontDim.ValueMember = "cod"
      cbFontDim.DisplayMember = "val"

      cbFontDim.SelectedValue = "3"

      Try
        cmdPrec.Image = Bitmap.FromFile(oApp.ChildImageDir & "\moveprevious.gif")
        cmdPrec.Text = ""
        cmdSuc.Image = Bitmap.FromFile(oApp.ChildImageDir & "\movenext.gif")
        cmdSuc.Text = ""
        cmdHome.Image = Bitmap.FromFile(oApp.ChildImageDir & "\magaz.gif")
        cmdPrint.Image = Bitmap.FromFile(oApp.ChildImageDir & "\print.gif")
        cmdPrint.Text = ""

        ImageList1.Images.Add("page", Bitmap.FromFile(oApp.ChildImageDir & "\new_treeview.gif"))
        ImageList1.Images.Add("book", Bitmap.FromFile(oApp.ChildImageDir & "\open_treeview.gif"))
      Catch
      End Try

      cbTipo.NTSSetParam(oApp.Tr(Me, 129641067417675781, "Tipo di help"))
      ckTitoli.NTSSetParam(oMenu, oApp.Tr(Me, 129640269596289063, "Cerca solo nei titoli"), "S", "N")
      edTrova.NTSSetParam(oMenu, oApp.Tr(Me, 129640269596298828, "Trova"), 0)
      liTrova.NTSSetParam(oApp.Tr(Me, 129640269596308594, "Lista risultati"))
      cbFont.NTSSetParam(oApp.Tr(Me, 128961837843474966, "Font"))
      cbFontDim.NTSSetParam(oApp.Tr(Me, 128961837842849950, "Dimensione Font"))
      ckItalic.NTSSetParam(oMenu, oApp.Tr(Me, 128961837842537442, "I"), "S", "N")
      ckUnderline.NTSSetParam(oMenu, oApp.Tr(Me, 128961837842693696, "U"), "S", "N")
      ckBold.NTSSetParam(oMenu, oApp.Tr(Me, 128961837843006204, "B"), "S", "N")

      NTSScriptExec("InitControls", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
    InitControlsBeginEndInit(Me, True)
  End Sub

#Region "Eventi di Form"
  Public Overridable Sub FRM__HELP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Try
      'da fare: da browser non fare fare CTRL+N o SHIFT+click, altrimenti si vede il link
      'su webservice gestire il caso di chiave non abilitata
      'l'help è solo sul server allo stesso livello di CMBDIR + \Help con subdir \html e subdir \images
      'per aggiornare le gid del manuale, come facciamo a distinguere quali vanno in bnimages da quelle per l'help???
      LeggiCronologia()

      InitControls()

      LeggiFilePDF()
      CaricaPDF()

      pnRight.Visible = False

      Dim strIn As String = oMenu.GetSettingBus("BS--HELP", "RECENT", "USER", "URL", "", ".", "")
      If strIn <> "" Then Decodifica(strIn)
      Dim strInTitle As String = oMenu.GetSettingBus("BS--HELP", "RECENT", "USER", "TITLE", "", ".", "")
      If strIn.Trim <> "" AndAlso strInTitle.Trim <> "" Then
        TabPageView1.Text = strInTitle
        TabPageView1.Image = Nothing
      End If
      cmdDelURL.Visible = False
      cmdDelURL.Enabled = False


      GctlSetRoules()
      pnEdit.Visible = False
      lbFindInPage.Visible = True
      Me.Text = oApp.NomeProcedura & " Help "

      strBusVer = "NET" & (1994 + oApp.BUSNET_VERSION).ToString
      strBusKey = oApp.ActKey.Matricola.ToString


      tsHelp.SelectedTabPageIndex = 0

      cmdPrec.Enabled = False
      cmdSuc.Enabled = False
      ckTitoli.Checked = CBool(oMenu.GetSettingBus("BS--HELP", "RECENT", "USER", "Titoli", "0", ".", "0"))

      NTSKeyUpF1Disable()

      Dim strTmp As String = ""
      strTmp = oMenu.GetSettingBus("BS--HELP", "RECENT", "USER", "HelpRemoto", "N", ".", "N")
      If strTmp.ToUpper = "S" Then
        strTmp = "S"
      Else
        strTmp = "N"
      End If

      If bHelpLocalExist = False And strTmp = "N" Then
        If oApp.MsgBoxInfoYesNo_DefYes("Guida non installata in locale! Voi utilizzare la guida online?") = Windows.Forms.DialogResult.Yes Then
          cbTipo.SelectedValue = "S"
          cbTipo.Enabled = False
        Else
          Me.Close()
          Return
        End If
      End If

      If bHelpLocalExist = False Then
        cbTipo.SelectedValue = "S"
        cbTipo.Enabled = False
      Else
        If cbTipo.SelectedValue <> strTmp Then
          cbTipo.SelectedValue = strTmp
        Else
          cbTipo_SelectedIndexChanged(cbTipo, Nothing)
        End If
      End If

      If bHelpLocalExist = False And bHelpOnlineExist = False Then
        Me.Close()
        Return
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub FRM__HELP_ActivatedFirst(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ActivatedFirst
    Try
      Me.Cursor = Cursors.WaitCursor


      If oCallParams Is Nothing Then
        cmdHome_Click(cmdHome, Nothing)
      Else
        If oCallParams.strPar1 = "" And oCallParams.strPar2 = "" And oCallParams.strPar3 = "" Then
          If oCallParams.strParam.Trim = "" Then
            cmdHome_Click(cmdHome, Nothing)
          Else
            GoPage(strMainDir & "html" & strSep & oCallParams.strParam)
          End If
        Else
          If cbTipo.SelectedValue = "N" Then
            'dir local
            Dim fileInfo As New FileInfo(strMainDir & "html" & strSep & oCallParams.strPar1.Replace("*", ""))
            If Not fileInfo.Exists Then
              fileInfo = New FileInfo(strMainDir & "html" & strSep & oCallParams.strPar2)
            End If
            If Not fileInfo.Exists Then
              fileInfo = New FileInfo(strMainDir & "html" & strSep & oCallParams.strPar3)
            End If
            If Not fileInfo.Exists Then
              fileInfo = New FileInfo(strMainDir & "html" & strSep & "business.htm")
            End If
            GoPage(strMainDir & "html" & strSep & fileInfo.Name)
          Else
            'help remoto
            If dttElencoHtml Is Nothing Then
              cmdHome_Click(cmdHome, Nothing)
            Else
              Dim strPage As String = ""
              If dttElencoHtml.Select("xx_html=" & CStrSQL(oCallParams.strPar1.Replace("*", "")), "").Length > 0 Then
                strPage = oCallParams.strPar1.Replace("*", "")
              End If
              If strPage = "" And dttElencoHtml.Select("xx_html=" & CStrSQL(oCallParams.strPar2), "").Length > 0 Then
                strPage = oCallParams.strPar2
              End If
              If strPage = "" And dttElencoHtml.Select("xx_html=" & CStrSQL(oCallParams.strPar3), "").Length > 0 Then
                strPage = oCallParams.strPar3
              End If
              If strPage = "" Then
                strPage = "business.htm"
              End If
              GoPage(strMainDir & "html" & strSep & strPage)
            End If
          End If
        End If
      End If

      LeggiConfigurazioneHelp()

      Try
        If IO.File.Exists("\\s01\Utenti\Filippucci\guida\HelpEditor.exe") Then
          GctlSetVisEnab(cmdEditorNTS, True)
          Try
            If Not CType(NTSFindControlByName(Me, "cmdHHEdit"), NTSButton) Is Nothing Then
              Try
                IO.File.Delete(oApp.RptDir.ToLower.Replace("\rpt", "\script") & "\bn__help.nts")
              Catch ex As Exception
              End Try
              Try
                Dim dbConn As System.Data.SqlClient.SqlConnection = Nothing
                Dim dbCmd As System.Data.SqlClient.SqlCommand = Nothing
                dbConn = New System.Data.SqlClient.SqlConnection(oApp.DbAp.Connect)
                dbCmd = New System.Data.SqlClient.SqlCommand("DELETE FROM uiconf WHERE ui_child = 'BN__HELP' AND ui_form = 'FRM__HELP'", dbConn)
                dbCmd.Connection.Open()
                dbCmd.ExecuteNonQuery()
                dbConn.Close()
              Catch ex As Exception
              End Try
              Try
                Me.Close()
              Catch ex As Exception
              End Try
            End If
          Catch ex As Exception
          End Try
        Else
          cmdEditorNTS.Visible = False
        End If
      Catch ex As Exception
        cmdEditorNTS.Visible = False
      End Try

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub FRM__HELP_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    Try

      If e.Control = False And e.Alt = False And e.Shift = False And e.KeyCode = Keys.Escape Then
        Me.Close()
      End If
      If e.Control = True And e.Alt = True And e.Shift = False And e.KeyCode = Keys.Home And cbTipo.SelectedValue = "N" Then
        If pnEdit.Visible = False Then
          If oApp.InputBoxNew(oApp.Tr(Me, 129641187675439453, "Inserire la password per editare il file")) = "mirto" Then
            StartEdit()
          End If
        End If
      End If
      If e.KeyCode = Keys.Enter And edTrova.Focused Then
        lsbProposta.Visible = False
        cmdCerca_Click(Nothing, Nothing)
        Me.NTSKeyDownEnterNotEmulateTabNow()
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub FRM__HELP_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Try
      oMenu.SaveSettingBus("BS--HELP", "RECENT", "USER", "HelpRemoto", cbTipo.SelectedValue, " ", "NNS", "...", "...")
      SaveCronologia()
      NTSScriptExec("FRM__HELP_FormClosing", Me, Nothing)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub FRM__HELP_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
    Try
      dttPage.Dispose()
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
#End Region
  Public Overridable Sub ckTitoli_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTitoli.CheckedChanged
    Try

      oMenu.SaveSettingBus("BS--HELP", "RECENT", "USER", "Titoli", NTSCStr(IIf(ckTitoli.Checked, "-1", "0")), " ", "NNS", "...", "...")

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub LeggiConfigurazioneHelp()
    bEditEenable = True
    bVssEnable = True
    bWebsiteEnable = True
  End Sub
  Public Overridable Sub WebBrows_Navigated(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles WebBrows.Navigated
    Dim strPageHTML As String = ""
    Try
      cmdPrintArgomento.Enabled = False

      If Not WebBrows.Document Is Nothing Then
        strPageHTML = WebBrows.Document.Url.LocalPath.Substring(WebBrows.Document.Url.LocalPath.LastIndexOf(strSep) + 1)
        If TreeNodeValidate(trContenuto.Nodes, strPageHTML) Then
          trPDF.SelectedNode = Nothing
          strLastHTM = strPageHTML
          If cbTipo.SelectedValue = "N" Then strLastPage = strMainDir & "html\" & strPageHTML
          If cbTipo.SelectedValue = "N" Then
            If trContenuto.SelectedNode.GetNodeCount(False) > 0 Then
              cmdPrintArgomento.Enabled = True
            End If
          End If
        End If
      Else
        TreeNodePDFValidate(WebBrows.Url.LocalPath)
      End If
      cmdSuc.Enabled = True
      cmdPrec.Enabled = True
      If WebBrows.CanGoBack = False Then cmdPrec.Enabled = False
      If WebBrows.CanGoForward = False Then cmdSuc.Enabled = False
      InserisciCronologia()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Function TreeNodeValidate(ByRef nodi As System.Windows.Forms.TreeNodeCollection, ByRef strPageHTML As String) As Boolean
    Dim tmp As System.Windows.Forms.TreeNode
    Try
      For Each tmp In nodi
        If tmp.Name.ToUpper = strPageHTML.ToUpper Then
          Try
            If tmp.TreeView.SelectedNode.Name.ToUpper <> strPageHTML.ToUpper Then
              tmp.TreeView.SelectedNode = tmp
            End If
          Catch exSetNode As Exception
            tmp.TreeView.SelectedNode = tmp
          End Try
          Return True
        End If
        If tmp.Nodes.Count > 0 Then
          If TreeNodeValidate(tmp.Nodes, strPageHTML) Then
            Return True
          End If
        End If
      Next
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub cbTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbTipo.SelectedIndexChanged
    Try
      If cbTipo.SelectedValue = "N" Then
        'dir local
        strMainDir = strHelpDirectoryLocal & "\Help\"
        strSep = "\"
      Else
        'help remoto
        strMainDir = oMenu.GetSettingBus("OPZIONI", ".", ".", "HelpRemotoURL", ".", " ", ".")
        If strMainDir = "." Then strMainDir = "https://www.ntsinformatica.it/BusHelpWs/BusHelpWs.asmx"
        strSep = "/"
      End If


      cmdHome.Visible = False
      cmdPrec.Visible = False
      cmdSuc.Visible = False
      cmdFind.Visible = False
      cmdPrint.Visible = False
      cmdPrintArgomento.Visible = False
      cmdShowHideSummary.Visible = False
      cmdAppunti.Visible = False
      tsAppunti.Visible = False
      tsHelp.Visible = False
      pnFill.Visible = False
      cmdEditorNTS.Visible = False
      NtsSplitter1.Visible = False
      NtsSplitter3.Visible = False

      trContenuto.Visible = False
      trContenuto.Nodes.Clear()
      If cbTipo.SelectedValue = "N" Then
        Load_CaricaLocale()
        If bHelpLocalExist = False Then Return
      Else
        Load_CaricaRemoto()
        If bHelpOnlineExist = False Then Return
      End If


      cmdHome.Visible = True
      cmdPrec.Visible = True
      cmdSuc.Visible = True
      cmdFind.Visible = True
      cmdPrint.Visible = True
      cmdPrintArgomento.Visible = True
      cmdShowHideSummary.Visible = True
      cmdAppunti.Visible = True
      tsAppunti.Visible = True
      tsHelp.Visible = True
      pnFill.Visible = True
      cmdEditorNTS.Visible = True
      NtsSplitter1.Visible = True
      NtsSplitter3.Visible = True

      trContenuto.CollapseAll()
      trContenuto.Visible = True

      If edTrova.Text.Trim <> "" Then
        cmdCerca_Click(Nothing, Nothing)
      End If

      GoPage(strLastHTM)
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub

  Public Overridable Sub Load_CaricaLocale()
    Dim r1 As StreamReader = Nothing
    Dim strFile As String = ""
    Dim strTxt As String = ""
    Dim strTitle As String = ""
    Dim i As Integer = 0
    Dim n As Integer = 0
    Dim oNode As TreeNode = Nothing
    Dim oCurrNode As TreeNode = Nothing
    Dim oLastNodeAdd As TreeNode = Nothing

    Try
      Me.Cursor = Cursors.WaitCursor


      If Not File.Exists(strMainDir & "sommario.hhc") Then
        lbAvviso.Visible = False
        bHelpLocalExist = False
        If oApp.MsgBoxInfoYesNo_DefYes("Guida non installata in locale! Voi utilizzare la guida online?") = Windows.Forms.DialogResult.Yes Then
          cbTipo.SelectedValue = "S"
        Else
          Me.Close()
        End If
        Return
      End If

      lbAvviso.Visible = True

      'carico il menu del tab 'contenuto'
      r1 = New StreamReader(strMainDir & "sommario.hhc", System.Text.Encoding.Default)
      Do While Not r1.EndOfStream
        strTxt = r1.ReadLine

        'aggiungo un nodo finale
        If strTxt.IndexOf("<LI>") > -1 Then
          oNode = New TreeNode
          oNode.ImageIndex = 0
          oNode.SelectedImageIndex = 0

          strTxt = r1.ReadLine 'caption
          strTxt = strTxt.Substring(strTxt.IndexOf("value="""))
          strTxt = strTxt.Substring(7, strTxt.Length - 9)
          oNode.Text = ReplaceText(strTxt)

          strTxt = r1.ReadLine() 'nome del file
          strTxt = strTxt.Substring(strTxt.IndexOf("value="""))
          strTxt = strTxt.Substring(7, strTxt.Length - 9)
          oNode.Tag = strTxt
          oNode.Name = strTxt.Replace("html\", "")

          If oCurrNode Is Nothing Then
            trContenuto.Nodes.Add(oNode)
          Else
            oCurrNode.Nodes.Add(oNode)
            oCurrNode.ImageIndex = 1
            oCurrNode.SelectedImageIndex = 1
          End If
        End If

        'aggiungo un nodo menu
        If strTxt.IndexOf("<UL>") > -1 Then
          oCurrNode = oNode
        End If

        'torno indietro un nodo menu
        If strTxt.IndexOf("</UL>") > -1 Then
          If oCurrNode Is Nothing Then
            oCurrNode = Nothing
          Else
            oCurrNode = oCurrNode.Parent
          End If
        End If
      Loop
      r1.Close()

      lbAvviso.Visible = False

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub
  Public dttElencoHtml As DataTable
  Public Overridable Sub Load_CaricaRemoto()
    'carico il menu dal server remoto
    Dim f As Integer = 0
    Dim strTxt As String = ""
    Dim strTitle As String = ""
    Dim i As Integer = 0
    Dim n As Integer = 0
    Dim oNode As TreeNode = Nothing
    Dim oCurrNode As TreeNode = Nothing
    Dim oLastNodeAdd As TreeNode = Nothing

    Dim params(1) As Object
    params(0) = strBusVer
    params(1) = strBusKey
    Dim strErrWS As String = ""
    Dim oRes As Object
    Try
      Me.Cursor = Cursors.WaitCursor


      oRes = CallWebService(strMainDir & "?WSDL", "BusHelpWs", "BusHelpWs", "GetMenu", params, strErrWS)
      If oRes Is Nothing Then
        lbAvviso.Visible = False
        oApp.MsgBoxErr(oApp.Tr(Me, 129640271083671875, "Guida online non disponibile! Non è stato possibile raggiungere il WebService." & strErrWS))
        bHelpOnlineExist = False
        If bHelpLocalExist Then cbTipo.SelectedValue = "N"
        Return
      End If
      lbAvviso.Visible = True

      Dim strT() As String = oRes.ToString.Split("§"c)
      dttElencoHtml = New DataTable
      dttElencoHtml.Columns.Add("xx_html", GetType(String))
      For f = 0 To strT.Length - 1
        strTxt = strT(f)

        'aggiungo un nodo finale
        If strTxt.IndexOf("<LI>") > -1 Then
          oNode = New TreeNode
          oNode.ImageIndex = 0
          oNode.SelectedImageIndex = 0

          f += 1
          strTxt = strT(f) 'caption
          strTxt = strTxt.Substring(strTxt.IndexOf("value="""))
          strTxt = strTxt.Substring(7, strTxt.Length - 9)
          oNode.Text = ReplaceText(strTxt)

          f += 1
          strTxt = strT(f) 'nome del file
          strTxt = strTxt.Substring(strTxt.IndexOf("value="""))
          strTxt = strTxt.Substring(7, strTxt.Length - 9)
          oNode.Tag = strTxt
          oNode.Name = strTxt.Replace("html\", "")

          dttElencoHtml.Rows.Add(New Object() {oNode.Name})

          If oCurrNode Is Nothing Then
            trContenuto.Nodes.Add(oNode)
          Else
            oCurrNode.Nodes.Add(oNode)
            oCurrNode.ImageIndex = 1
            oCurrNode.SelectedImageIndex = 1
          End If
        End If

        'aggiungo un nodo menu
        If strTxt.IndexOf("<UL>") > -1 Then
          oCurrNode = oNode
        End If

        'torno indietro un nodo menu
        If strTxt.IndexOf("</UL>") > -1 Then
          If oCurrNode Is Nothing Then
            oCurrNode = Nothing
          Else
            oCurrNode = oCurrNode.Parent
          End If
        End If
      Next

      lbAvviso.Visible = False

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub


  Public Overridable Sub liTrova_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles liTrova.DoubleClick
    Try
      If liTrova.SelectedItem Is Nothing Then Return
      GoPage(strCercaUrl(liTrova.SelectedIndex))
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub trContenuto_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trContenuto.AfterSelect
    Try
      If e.Action = TreeViewAction.Unknown Then Return
      GoPage(strMainDir & trContenuto.SelectedNode.Tag.ToString)
      trPDF.SelectedNode = Nothing
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub trCronologia_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trCronologia.AfterSelect
    Try
      If e.Action = TreeViewAction.Unknown Then Return
      If trCronologia.SelectedNode.Tag.ToString = "page" Then WebBrows.Navigate(trCronologia.SelectedNode.Name)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Function ReplaceText(ByVal strIn As String) As String
    Return strIn.Replace("&egrave;", "è").Replace("&agrave;", "à").Replace("&ugrave;", "ù").Replace("&igrave;", "ì").Replace("&ograve;", "ò")
  End Function

  Public Overridable Sub GoPage(ByVal strPage As String)
    Dim i As Integer = 0
    Dim params(2) As Object
    params(0) = strBusVer
    params(1) = strBusKey
    Dim strErrWS As String = ""

    Dim oRes As Object
    Try
      Me.Cursor = Cursors.WaitCursor
      If strPage = "" Then strPage = "business.htm"
      If cbTipo.SelectedValue = "N" Then
        If Not strPage.Trim.ToUpper.Contains(strMainDir.Trim.ToUpper) Then
          strPage = strMainDir & "html\" & strPage
        End If
        NTSScriptExec("GoPage", CType(strPage, Object), Nothing)
        WebBrows.Navigate(strPage)
      Else

        'devo mantenere solo il nome della pagina
        strPage = strPage.Replace("\", "/")
        i = strPage.IndexOf("/")
        While i > -1
          strPage = strPage.Substring(i + 1)
          i = strPage.IndexOf("/")
        End While
        params(2) = strPage

        oRes = CallWebService(strMainDir & "?WSDL", "BusHelpWs", "BusHelpWs", "GetPage", params, strErrWS)
        If Not oRes Is Nothing Then
          Dim strUrl As String = strMainDir.Substring(0, strMainDir.Length - 15) & oRes.ToString
          WebBrows.Navigate(strUrl)
        Else
          If bHelpOnlineExist = True Then oApp.MsgBoxErr(oApp.Tr(Me, 129640272349013672, "Guida online non disponibile! Non è stato possibile raggiungere il WebService." & strErrWS))
          bHelpOnlineExist = False
          If bHelpLocalExist Then cbTipo.SelectedValue = "N"
        End If
      End If
        strLastPage = strPage
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Me.Cursor = Cursors.Default
    End Try
  End Sub

  Private proxyInstance As Object = Nothing
  Private Function CallWebService(ByVal strUrl As String, ByVal strServiceName As String, ByVal strTypeName As String, _
                                  ByVal strNomeFunzione As String, ByVal params() As Object, ByRef strErrWS As String) As Object
    Dim oRes As Object = Nothing
    Try

      If InizializzaWebService(strUrl, strServiceName, strTypeName, strNomeFunzione, params, strErrWS) Then
        Dim targetMethodInfo As System.Reflection.MethodInfo = proxyInstance.GetType().GetMethod(strNomeFunzione)
        'chiamo la funzione
        If (Not params Is Nothing) AndAlso params.Length > 0 Then
          oRes = targetMethodInfo.Invoke(proxyInstance, params)
        Else
          oRes = targetMethodInfo.Invoke(proxyInstance, Nothing)
        End If
        If Not oRes Is Nothing Then
          If oRes.ToString.PadRight(5).Substring(0, 5) = "*ERR*" Then
            strErrWS = vbCrLf & oApp.Tr(Me, 129641102092792969, "Si è verificato un errore in esecuzione procedura nel WebService." & vbCrLf & "Errore: ") & vbCrLf & oRes.ToString
          Else
            Return oRes
          End If
        End If

      End If
      Return Nothing

    Catch ex As System.Reflection.TargetInvocationException
      strErrWS = vbCrLf & ex.Message & vbCrLf & "Unable to invoke the requested method." & vbCrLf & "Ensure the service is available or the service configuration information is correct"
      Return Nothing
    End Try
  End Function
  Private Function InizializzaWebService(ByVal strUrl As String, ByVal strServiceName As String, ByVal strTypeName As String, _
                                         ByVal strNomeFunzione As String, ByVal params() As Object, ByRef strErrWS As String) As Boolean
    Try

      If proxyInstance Is Nothing Then

        'Con questa sembra non dare errore all'invoke
        System.Net.ServicePointManager.Expect100Continue = False

        'chiedo il file WSDL. è una cosa standard di ogni servizio, basta far seguire al nome del servizio il parametro '?WSDL'
        Dim wsdlDocument As System.Xml.XmlReader
        Try
          wsdlDocument = System.Xml.XmlReader.Create(strUrl)
        Catch ex As Exception
          Try
            wsdlDocument = System.Xml.XmlReader.Create(strWeb & "?WSDL")
            strMainDir = strWeb
          Catch ex2 As Exception
            strErrWS = vbCrLf & ex.Message & vbCrLf & "URL non disponibile " & strUrl
            Return False
          End Try
        End Try

        'deserializzo il file WSDL (la definizione del web service)
        Dim oElencoServizi As System.Web.Services.Description.ServiceDescription = System.Web.Services.Description.ServiceDescription.Read(wsdlDocument)
        Dim bOk As Boolean = False
        For Each serviceItem As System.Web.Services.Description.Service In oElencoServizi.Services
          If serviceItem.Name.Equals(strServiceName) Then
            bOk = True
            Exit For
          End If
        Next
        If Not bOk Then
          strErrWS = vbCrLf & "Invalid Service Name."
          Return False
        End If

        'genero la classe proxy per chiamare il servizio
        Dim descriptionImporter As New System.Web.Services.Description.ServiceDescriptionImporter
        descriptionImporter.ProtocolName = "Soap"
        descriptionImporter.AddServiceDescription(oElencoServizi, String.Empty, String.Empty)
        Dim targetCodeNamespace As New System.CodeDom.CodeNamespace
        Dim compileUnit As New System.CodeDom.CodeCompileUnit
        compileUnit.Namespaces.Add(targetCodeNamespace)
        Dim err As System.Web.Services.Description.ServiceDescriptionImportWarnings = descriptionImporter.Import(targetCodeNamespace, compileUnit)
        Dim results As System.CodeDom.Compiler.CompilerResults = Nothing
        Dim serviceAssembly As System.Reflection.Assembly = Nothing
        If err = 0 Then
          'se non si sono verificati errori, compilo la classe
          Dim codeProvider As New VBCodeProvider
          Dim parameters As New System.CodeDom.Compiler.CompilerParameters
          With parameters
            .GenerateExecutable = False
            .GenerateInMemory = True
            .IncludeDebugInformation = False
            .ReferencedAssemblies.Add("System.dll")
            .ReferencedAssemblies.Add("System.Data.dll")
            .ReferencedAssemblies.Add("System.Web.Services.dll")
            .ReferencedAssemblies.Add("System.XML.dll")
          End With
          results = codeProvider.CompileAssemblyFromDom(parameters, compileUnit)
          codeProvider.Dispose()
        End If
        'istanzio la classe
        If Not results Is Nothing Then
          serviceAssembly = results.CompiledAssembly
          If Not serviceAssembly Is Nothing Then
            proxyInstance = serviceAssembly.CreateInstance(strTypeName, True)
          End If
        End If
      End If

      If proxyInstance Is Nothing Then Return False

      Return True
    Catch ex As Exception
      strErrWS = vbCrLf & ex.Message
    End Try
  End Function

  Public Overridable Function CreaTable() As Boolean
    Dim r1 As StreamReader = Nothing
    Dim strFile As String = ""
    Dim strTxt As String = ""
    Dim strTitle As String = ""
    Dim strBody As String = ""
    Dim i As Integer = 0
    Dim n As Integer = 0
    Dim bRicreaIndice As Boolean = False
    Dim dateHelp As New Date

    Try
      'carico tutte le pagine in memoria, per poter eseguire il 'Trova' con help in locale
      dttPage.Clear()
      dttPage.TableName = "PAGINE"
      dttPage.Columns.Clear()
      dttPage.Columns.Add("name", GetType(String))
      dttPage.Columns.Add("title", GetType(String))
      dttPage.Columns.Add("page", GetType(String))

      If System.IO.File.Exists(oApp.Dir & "\help.xml") Then
        For Each strFile In System.IO.Directory.GetFiles(strMainDir & "html", "*.htm")
          If System.IO.File.GetLastWriteTime(strFile) > System.IO.File.GetLastWriteTime(oApp.Dir & "\help.xml") Then
            bRicreaIndice = True
            Exit For
          End If
        Next
        If Not bRicreaIndice Then dttPage.ReadXml(oApp.Dir & "\help.xml")
      Else
        bRicreaIndice = True
      End If

      If bRicreaIndice Or dttPage.Rows.Count = 0 Then
        lbAvviso.Text = "Creazione indice di ricerca in corso ..."
        lbAvviso.Visible = True
        lbAvviso.Refresh()
        For Each strFile In System.IO.Directory.GetFiles(strMainDir & "html", "*.htm")
          If Not strFile.ToLower.Contains("chiavescaduta.htm") Then
            If System.IO.File.GetLastWriteTime(strFile) > dateHelp Then dateHelp = System.IO.File.GetLastWriteTime(strFile)
            r1 = New StreamReader(strFile, System.Text.Encoding.Default)
            strTxt = r1.ReadToEnd
            r1.Close()

            If Not (strTxt.ToLower.Contains("<p>In questa sezione (Argomenti e Moduli):</p>".ToLower) Or strTxt.ToLower.Contains("<p>In questa sezione (Guida Analitica):</p>".ToLower)) Then
              'ricavo il titolo
              'If strTxt.Contains("<title>") AndAlso strTxt.Contains("</title>") Then
              i = strTxt.IndexOf("<title>", StringComparison.OrdinalIgnoreCase) + 7
              n = strTxt.IndexOf("</title>", i, StringComparison.OrdinalIgnoreCase) - i
              strTitle = strTxt.Substring(i, n)
              i = 0
              i = strTxt.IndexOf("<body", StringComparison.OrdinalIgnoreCase)
              strBody = strTxt.Substring(i)
              dttPage.Rows.Add(strFile, strTitle, strBody.ToLower)
              'End If
            End If

          End If
        Next
        dttPage.WriteXml(oApp.Dir & "\help.xml")
        System.IO.File.SetLastWriteTime(oApp.Dir & "\help.xml", dateHelp)

        lbAvviso.Visible = False
        lbAvviso.Text = "Creazione indice in corso ..."
      End If
      dttPage.AcceptChanges()

      Return True

    Catch ex As Exception
      lbAvviso.Visible = False
      lbAvviso.Text = "Creazione indice in corso ..."
      If System.IO.File.Exists(oApp.Dir & "\help.xml") Then System.IO.File.Delete(oApp.Dir & "\help.xml")
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function

  Public strCercaUrl As String()
  Public Overridable Sub cmdCerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCerca.Click
    Dim dtrT() As DataRow = Nothing
    Dim strT() As String = Nothing
    Dim strWhere As String = ""
    Dim i As Integer = 0

    Dim params(3) As Object
    params(0) = strBusVer
    params(1) = strBusKey
    Dim strErrWS As String = ""

    Dim oRes As Object

    Try

      If edTrova.Text.Trim = "" Then Return
      Cursor = Cursors.WaitCursor

      AutoRecents()

      liTrova.Items.Clear()
      strCercaUrl = Nothing

      If cbTipo.SelectedValue = "N" Then
        strT = edTrova.Text.ToLower.Trim.Split(" "c)
        strWhere = "(" & IIf(ckTitoli.Checked, "title", "page").ToString & " like '%" & NTSTraduciLikeValue(strT(0)) & "%')"
        For i = 1 To strT.Length - 1
          strWhere += " AND (" & IIf(ckTitoli.Checked, "title", "page").ToString & " like '%" & NTSTraduciLikeValue(strT(i)) & "%')"
        Next
        If dttPage.Rows.Count = 0 Then CreaTable()
        dtrT = dttPage.Select(strWhere, "title")

        If dtrT.Length > 0 Then
          Dim strTitle(dtrT.Length - 1) As String
          Dim strUrl(dtrT.Length - 1) As String
          strCercaUrl = strUrl
          For i = 0 To dtrT.Length - 1
            strTitle(i) = ReplaceText(dtrT(i)!title.ToString)
            strUrl(i) = dtrT(i)!name.ToString
          Next
          liTrova.Items.AddRange(strTitle)
        End If
      Else
        params(2) = IIf(ckTitoli.Checked, "S", "N").ToString
        params(3) = edTrova.Text.Trim

        oRes = CallWebService(strMainDir & "?WSDL", "BusHelpWs", "BusHelpWs", "Trova", params, strErrWS)
        If oRes Is Nothing Then
          If bHelpOnlineExist = True Then oApp.MsgBoxErr(oApp.Tr(Me, 129640272705576172, "Guida online non disponibile! Non è stato possibile raggiungere il WebService." & strErrWS))
          bHelpOnlineExist = False
          Return
        End If

        If Not oRes.ToString.Trim = "" Then
          strT = oRes.ToString.Split("|"c)
          If strT.Length > 0 Then
            Dim strTitle(strT.Length - 1) As String
            Dim strUrl(strT.Length - 1) As String
            strCercaUrl = strUrl
            For i = 0 To strT.Length - 1
              strTitle(i) = ReplaceText(strT(i)).Split("§"c)(0).TrimEnd
              strUrl(i) = ReplaceText(strT(i)).Split("§"c)(1)
            Next
            liTrova.Items.AddRange(strTitle)
          End If
        End If

        'For i = 0 To strT.Length - 1
        '  If strT(i).Trim <> "" Then liTrova.Items.Add(ReplaceText(strT(i)))
        'Next
      End If

      If liTrova.Items.Count = 0 Then
        oApp.MsgBoxInfo(oApp.Tr(Me, 129640330987480469, "La ricerca non ha prodotto nessun risultato."))
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    Finally
      Cursor = Cursors.Default
    End Try
  End Sub
  Public Overridable Sub cmdHome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHome.Click
    Try
      GoPage(strMainDir & "html" & strSep & "business.htm")
      trContenuto.CollapseAll()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdPrec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrec.Click
    Try
      WebBrows.GoBack()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdSuc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSuc.Click
    Try
      WebBrows.GoForward()
      'cmdSuc.Enabled = True
      'cmdPrec.Enabled = True
      'If WebBrows.CanGoBack = False Then cmdPrec.Enabled = False
      'If WebBrows.CanGoForward = False Then cmdSuc.Enabled = False
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
    Try
      WebBrows.ShowPrintDialog()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdPrintArgomento_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrintArgomento.Click
    Dim frmStampa As FRM__PRIN = Nothing
    Try

      If CreaStampa(trContenuto.SelectedNode) Then
        frmStampa = CType(NTSNewFormModal("FRM__PRIN"), FRM__PRIN)
        frmStampa.Init(oMenu, New CLE__CLDP, DittaCorrente)
        frmStampa.ShowDialog()
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdShowHideSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowHideSummary.Click
    Try
      If pnLeft.Visible = False Then
        GctlSetVisEnab(pnLeft, True)
      Else
        pnLeft.Visible = False
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdAppunti_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAppunti.Click
    Try
      If pnRight.Visible = False Then
        GctlSetVisEnab(pnRight, True)
      Else
        pnRight.Visible = False
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
    Try
      tsHelp.SelectedTabPageIndex = 1
      edTrova.Focus()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdComprimi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComprimi.Click
    Try
      trContenuto.CollapseAll()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdRecuperaHTML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRecuperaHTML.Click
    Dim frmRecHTML As FRM__HREC = Nothing
    Dim oPar As CLE__CLDP

    Try
      frmRecHTML = CType(NTSNewFormModal("FRM__HREC"), FRM__HREC)

      oPar = New CLE__CLDP
      oPar.bPar1 = False
      oPar.strPar1 = strLastPage
      frmRecHTML.Init(oMenu, oPar, DittaCorrente)
      frmRecHTML.ShowDialog()
      If oPar.bPar1 Then
        BackupHTML("")
        Dim fileRec As New FileInfo(oPar.strPar2)
        fileRec.CopyTo(strLastPage, True)
        fileRec.Delete()
        WebBrows.Refresh()
      Else
        If File.Exists(oPar.strPar2) Then File.Delete(oPar.strPar2)
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#Region "Controlli per Editing"
  Public Overridable Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
    SalvaHTML()
  End Sub
  Public Overridable Function SalvaHTML() As Boolean
    Dim strTxt As String = ""
    Dim w1 As StreamWriter = Nothing

    Try
      BackupHTML(strTxt)

      Dim strNewTxt As String = ""
      Dim strTmp As String = ""
      Dim bInScript As Boolean = False
      Dim bInBody As Boolean = False
      Dim bCloseFont As Boolean = False
      Dim bCloseSpan As Boolean = False
      Dim strTmpSpan As String = ""
      Dim strTmpOpzione As String = ""
      For i As Integer = 0 To strTxt.Length - 1
        Select Case strTxt.Substring(i, 1)
          Case "<"
            strTmp &= strTxt.Substring(i, 1)
          Case ">"
            strTmp &= strTxt.Substring(i, 1)
            'check
            If Not bInScript Then
              strTmp = strTmp.Replace(vbCrLf, "")
              Do While strTmp.Contains("  ")
                If strTmp.Contains("  ") Then strTmp = strTmp.Replace("  ", " ")
              Loop
            End If
            If strTmp.ToLower.StartsWith("<strong") Or strTmp.ToLower.StartsWith("<b ") Or strTmp.ToLower.Equals("<b>") Then
              strTmp = "<b>"
            ElseIf strTmp.ToLower.StartsWith("</strong") Or strTmp.ToLower.StartsWith("</b ") Or strTmp.ToLower.Equals("</b>") Then
              strTmp = "</b>"
            ElseIf strTmp.ToLower.StartsWith("<tbody") Then
              strTmp = ""
            ElseIf strTmp.ToLower.StartsWith("</tbody") Then
              strTmp = ""
            ElseIf strTmp.ToLower.StartsWith("<thead") Then
              strTmp = ""
            ElseIf strTmp.ToLower.StartsWith("</thead") Then
              strTmp = ""
            ElseIf strTmp.ToLower.StartsWith("<em") Then
              strTmp = "<i>"
            ElseIf strTmp.ToLower.StartsWith("</em") Then
              strTmp = "</i>"
            ElseIf strTmp.ToLower.StartsWith("<pre") Then
              strTmp = "<pre>"
            ElseIf strTmp.ToLower.StartsWith("</pre") Then
              strTmp = "</pre>"
            ElseIf strTmp.ToLower.StartsWith("<code") Then
              strTmp = "<code>"
            ElseIf strTmp.ToLower.StartsWith("</code") Then
              strTmp = "</code>"
            ElseIf strTmp.ToLower.StartsWith("<i ") Or strTmp.ToLower.Equals("<i>") Then
              strTmp = "<i>"
            ElseIf strTmp.ToLower.StartsWith("</i ") Or strTmp.ToLower.Equals("</i>") Then
              strTmp = "</i>"
            ElseIf strTmp.ToLower.StartsWith("<u ") Or strTmp.ToLower.Equals("<u>") Then
              strTmp = "<u>"
            ElseIf strTmp.ToLower.StartsWith("</u ") Or strTmp.ToLower.Equals("</u>") Then
              strTmp = "</u>"
            ElseIf strTmp.ToLower.StartsWith("<html ") Or strTmp.ToLower.Equals("<html>") Then
              strTmp = "<html>"
            ElseIf strTmp.ToLower.StartsWith("</html ") Or strTmp.ToLower.Equals("</html>") Then
              strTmp = "</html>"
            ElseIf strTmp.ToLower.StartsWith("<head ") Or strTmp.ToLower.Equals("<head>") Then
              strTmp = "<head>"
            ElseIf strTmp.ToLower.StartsWith("</head ") Or strTmp.ToLower.Equals("</head>") Then
              strTmp = "</head>"
            ElseIf strTmp.ToLower.StartsWith("<title ") Or strTmp.ToLower.Equals("<title>") Then
              strTmp = "<title>"
            ElseIf strTmp.ToLower.StartsWith("</title ") Or strTmp.ToLower.Equals("</title>") Then
              strTmp = "</title>"
            ElseIf strTmp.ToLower.StartsWith("<body ") Or strTmp.ToLower.Equals("<body>") Then
              strTmp = "<body>"
            ElseIf strTmp.ToLower.StartsWith("</body ") Or strTmp.ToLower.Equals("</body>") Then
              strTmp = "</body>"
            ElseIf strTmp.ToLower.StartsWith("<img ") AndAlso strTmp.ToLower.Contains("src=") AndAlso strTmp.Replace("\", "/").ToLower.Contains("/images/help_") AndAlso strTmp.ToLower.Contains(".gif") AndAlso strTmp.ToLower.Contains(".gif""") Then
              strTmp = strTmp.Replace("\", "/")
              strTmp = strTmp.Substring(0, strTmp.IndexOf("src=") + 4) & """../images" & strTmp.Substring(strTmp.ToLower.IndexOf("/help_"), strTmp.ToLower.IndexOf(".gif") - strTmp.ToLower.IndexOf("/help_")) & strTmp.Substring(strTmp.ToLower.IndexOf(".gif"""))
            ElseIf strTmp.ToLower.StartsWith("<img ") AndAlso strTmp.ToLower.Contains("src=") AndAlso strTmp.Replace("\", "/").ToLower.Contains("/images/help_") AndAlso strTmp.ToLower.Contains(".jpg") AndAlso strTmp.ToLower.Contains(".jpg""") Then
              strTmp = strTmp.Replace("\", "/")
              strTmp = strTmp.Substring(0, strTmp.IndexOf("src=") + 4) & """../images" & strTmp.Substring(strTmp.ToLower.IndexOf("/help_"), strTmp.ToLower.IndexOf(".jpg") - strTmp.ToLower.IndexOf("/help_")) & strTmp.Substring(strTmp.ToLower.IndexOf(".jpg"""))
            ElseIf strTmp.ToLower.StartsWith("<a href=") And strTmp.ToLower.Replace("\", "/").Contains("/html/") And strTmp.ToLower.Replace("\", "/").Contains("file:/") And strTmp.ToLower.Replace("\", "/").Contains(".htm") Then
              strTmp = strTmp.Replace("\", "/")
              If strTmp.Contains("ampers07.htm#") Then
                strTmp = "<A href=""" & strTmp.Substring(strTmp.ToLower.IndexOf("/html/") + 6)
              Else
                strTmp = "<A href=""" & strTmp.Substring(strTmp.ToLower.IndexOf("/html/") + 6, strTmp.IndexOf(".htm") - (strTmp.ToLower.IndexOf("/html/") + 6)) & ".htm"">"
              End If
            ElseIf strTmp.ToUpper.StartsWith("<V:IMAGEDATA") Then
              strTmp = ""
            ElseIf strTmp.ToUpper.StartsWith("<V:") Or strTmp.ToUpper.StartsWith("</V:") Then
              strTmp = ""
            ElseIf strTmp.ToUpper.StartsWith("<O:") Or strTmp.ToUpper.StartsWith("</O:") Then
              strTmp = ""
            ElseIf strTmp.ToUpper.StartsWith("<H") Or strTmp.ToUpper.StartsWith("</H") Then

              If strTmp.ToUpper.StartsWith("<H1") Or strTmp.ToUpper.StartsWith("<H2") _
               Or strTmp.ToUpper.StartsWith("<H3") Or strTmp.ToUpper.StartsWith("<H4") Then
                strTmp = "<p>"
              End If
              If strTmp.ToUpper.StartsWith("</H1") Or strTmp.ToUpper.StartsWith("</H2") _
               Or strTmp.ToUpper.StartsWith("</H3") Or strTmp.ToUpper.StartsWith("</H4") Then
                strTmp = "</p>"
              End If

            ElseIf strTmp.ToLower.StartsWith("<ul ") Or strTmp.ToLower.Equals("<ul>") Then
              strTmp = "<ul>"
            ElseIf strTmp.ToLower.StartsWith("<ol ") Or strTmp.ToLower.Equals("<ol>") Then
              strTmp = "<ol>"
            ElseIf strTmp.ToLower.StartsWith("<li ") Or strTmp.ToLower.Equals("<li>") Then
              strTmp = "<li>"
            ElseIf strTmp.ToLower.StartsWith("<p ") Or strTmp.ToLower.Equals("<p>") Then
              strTmp = "<p>"
            ElseIf strTmp.ToLower.StartsWith("<div ") Or strTmp.ToLower.Equals("<div>") Then
              strTmp = "<br>"
            ElseIf strTmp.ToLower.Equals("</div>") Then
              strTmp = ""
            ElseIf strTmp.ToLower.StartsWith("<colgroup") Or strTmp.ToLower.Equals("</colgroup>") Then
              strTmp = ""
            ElseIf strTmp.ToLower.Equals("</p>") Then
              strTmp = "</p>"
            ElseIf strTmp.ToLower.Equals("</ul>") Then
              strTmp = "</ul>"
            ElseIf strTmp.ToLower.Equals("</ol>") Then
              strTmp = "</ol>"
            ElseIf strTmp.ToLower.Equals("</li>") Then
              strTmp = "</li>"
            ElseIf strTmp.ToLower.Replace(" ", "").Equals("<br>") Or strTmp.ToLower.Replace(" ", "").Equals("<br/>") Then
              strTmp = "<br>"
            ElseIf strTmp.ToLower.StartsWith("<span") Or strTmp.ToLower.StartsWith("</span") Then
              If strTmp.ToLower.Contains("<span") AndAlso strTmp.ToLower.Contains("opzione=""1""") Then
                strTmpSpan = strTmp
                bCloseSpan = True
              Else
                If strTmp.ToLower.StartsWith("</span") AndAlso bCloseSpan Then
                  strTmpSpan &= strTmp
                  bCloseSpan = False
                End If
              End If
              strTmp = ""
            ElseIf strTmp.ToLower.StartsWith("<font") Or strTmp.ToLower.StartsWith("</font") Then
              If strTmp.ToLower.Contains("<font") AndAlso strTmp.ToLower.Contains("color=white") AndAlso strTmp.ToLower.Contains("size=6") AndAlso strTmp.ToLower.Contains("class=title") Then
                bCloseFont = True
              Else
                If strTmp.ToLower.StartsWith("</font") AndAlso bCloseFont Then
                  bCloseFont = False
                Else
                  strTmp = ""
                End If
              End If
            ElseIf strTmp.ToLower.Equals("<o:p>") Or strTmp.ToLower.Equals("</o:p>") Then
              strTmp = ""
            ElseIf strTmp.StartsWith("<META ") Then
              strTmp = ""
            End If
            If strTmp.ToLower.StartsWith("<script") Then
              strTmp = "<SCRIPT LANGUAGE=""JavaScript"">"
              bInScript = True
            End If
            If strTmp.ToLower.Equals("</script>") Then
              bInScript = False
            End If
            If strTmp.StartsWith("<A ") Then
              strTmp = strTmp.Replace("onmouseover=", "onmouseover=""").Replace(".src;", ".src;""").Replace("onmouseout=", "onmouseout=""")
            End If
            strNewTxt &= strTmp
            strTmp = ""
            If strTmpSpan <> "" Then
              If bCloseSpan = False Then
                If Not dttXmlOpzio Is Nothing Then
                  If dttXmlOpzio.Rows.Count > 0 Then
                    If dttXmlOpzio.Select("xx_opzione=" & CStrSQL(strTmpOpzione)).Length > 0 Then
                      strTmpSpan = "<SPAN title=""" & dttXmlOpzio.Select("xx_opzione=" & CStrSQL(strTmpOpzione))(0)!xx_opzione.ToString & dttXmlOpzio.Select("xx_opzione=" & CStrSQL(strTmpOpzione))(0)!xx_testo.ToString & _
                      """ style=""BACKGROUND: #e9edf4; COLOR: #000000"" opzione=""1"">" & dttXmlOpzio.Select("xx_opzione=" & CStrSQL(strTmpOpzione))(0)!xx_opzione.ToString & _
                      "</SPAN>"
                    End If
                  End If
                End If

                strNewTxt &= strTmpSpan
                strTmpSpan = ""
                strTmpOpzione = ""
              End If
            End If
          Case Else
            If strTmp.Length <> 0 Then
              strTmp &= strTxt.Substring(i, 1)
            Else
              If strTmpSpan <> "" Then
                If bCloseSpan = True Then
                  If strTxt.Substring(i, 1) = "/" Then
                    strTmpSpan &= "\"
                    strTmpOpzione &= "\"
                  Else
                    strTmpSpan &= strTxt.Substring(i, 1)
                    strTmpOpzione &= strTxt.Substring(i, 1)
                  End If
                End If
              Else
                strNewTxt &= strTxt.Substring(i, 1)
              End If
            End If
        End Select
      Next
      strNewTxt = strNewTxt.Replace("<b></b>", "").Replace("<p></p>", "")
      strNewTxt = strNewTxt.Replace("<html><", "<html>" & vbCrLf & "<").Replace("></html>", ">" & vbCrLf & "</html>")
      strNewTxt = strNewTxt.Replace("<title><", "<title>" & vbCrLf & "<").Replace("></title>", ">" & vbCrLf & "</title>")
      strNewTxt = strNewTxt.Replace("<title>GA:", "<title> GA:").Replace("<title>AM:", "<title> AM:")
      strNewTxt = strNewTxt.Replace("<head><", "<head>" & vbCrLf & "<").Replace("></head>", ">" & vbCrLf & "</head>")
      strNewTxt = strNewTxt.Replace("<body><", "<body>" & vbCrLf & "<").Replace("></body>", ">" & vbCrLf & "</body>").Replace("<body>", "<body lang=it topMargin=3>")
      'strNewTxt = strNewTxt.Replace("</li></ul>", "</li>" & vbCrLf & "</ul>").Replace("</li></ol>", "</li>" & vbCrLf & "</ol>")
      strNewTxt = strNewTxt.Replace("><LINK", ">" & vbCrLf & "<LINK")
      strNewTxt = strNewTxt.Replace("><LINK", ">" & vbCrLf & "<LINK")
      strNewTxt = strNewTxt.Replace(vbTab, "")

      strTxt = strNewTxt
      strNewTxt = ""
      strTmp = ""
      For i As Integer = 0 To strTxt.Length - 1
        If bInBody Then
          Select Case strTxt.Substring(i, 1)
            Case "<"
              strTmp &= strTxt.Substring(i, 1)
              If strTmp.Replace(" ", "").Replace(vbCrLf, "").Replace(vbTab, "") = "><" Then
                strTmp = "><"
              Else
                If strNewTxt.ToLower.Contains("<pre") Then
                  If Not strNewTxt.ToLower.Contains("</pre") Then
                    'non fare nulla
                  Else
                    If strNewTxt.ToLower.LastIndexOf("<pre") > strNewTxt.ToLower.LastIndexOf("</pre") Then
                      'non fare nulla 
                    Else
                      strTmp = strTmp.Replace(vbCrLf, "").Replace(vbTab, "")
                      Do While strTmp.Contains("  ")
                        If strTmp.Contains("  ") Then strTmp = strTmp.Replace("  ", " ")
                      Loop
                    End If
                  End If
                Else
                  strTmp = strTmp.Replace(vbCrLf, "").Replace(vbTab, "")
                  Do While strTmp.Contains("  ")
                    If strTmp.Contains("  ") Then strTmp = strTmp.Replace("  ", " ")
                  Loop
                End If
              End If
              strNewTxt &= strTmp
              strTmp = ""
            Case ">"
              strTmp &= strTxt.Substring(i, 1)
            Case Else
              If strTmp.Length <> 0 Then
                strTmp &= strTxt.Substring(i, 1)
              Else
                strNewTxt &= strTxt.Substring(i, 1)
              End If
          End Select
        Else
          Select Case strTxt.Substring(i, 1)
            Case "<"
              strTmp &= strTxt.Substring(i, 1)
            Case ">"
              strTmp &= strTxt.Substring(i, 1)
              'check
              If strTmp.ToLower.StartsWith("<body ") Then
                bInBody = True
              End If
              strNewTxt &= strTmp
              strTmp = ""
            Case Else
              If strTmp.Length <> 0 Then
                strTmp &= strTxt.Substring(i, 1)
              Else
                strNewTxt &= strTxt.Substring(i, 1)
              End If
          End Select
        End If
      Next
      If strTmp.Length <> 0 Then
        strNewTxt &= strTmp.Replace(" ", "").Replace(vbCrLf, "").Replace(vbTab, "")
      End If
      strNewTxt = strNewTxt.Replace(" class=calone ", " ")
      strNewTxt = strNewTxt.Replace(" alt="""" ", " ")
      strNewTxt = strNewTxt.Replace(" </p>", "</p>")
      strNewTxt = strNewTxt.Replace(" </li>", "</li>")
      strNewTxt = strNewTxt.Replace(" </td>", "</td>")
      strNewTxt = strNewTxt.Replace(" </tr>", "</tr>")
      strNewTxt = strNewTxt.Replace(" </ul>", "</ul>")
      '      strNewTxt = strNewTxt.Replace("<pre>", "<p><code>")
      '      strNewTxt = strNewTxt.Replace("</pre>", "</code></p>")

      strNewTxt = strNewTxt.Replace("<TABLE border=0 cellSpacing=0 cellPadding=0><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/software-retail-negozi/""><IMG border=0 src=""../images/help_home_01_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/controllo-di-gestione/""><IMG border=0 src=""../images/help_home_02_Controlla.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/crm/""><IMG border=0 src=""../images/help_home_03_Fidelizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/analisi-dati-datawarehouse/""><IMG border=0 src=""../images/help_home_04_DWH.gif""></A></TD><TD vAlign=center width=169 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/project-management/""><IMG border=0 src=""../images/help_home_05_Gestisci.gif""></A></TD><TD vAlign=center width=146 align=right><IMG border=0 src=""../images/help_home_NTS_logo_up.gif""></TD></TR><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/logistica/""><IMG border=0 src=""../images/help_home_06_Organizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/customer-service/""><IMG border=0 src=""../images/help_home_07_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/produzione/""><IMG border=0 src=""../images/help_home_08_Pianifica.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/archiviazione-documenti/""><IMG border=0 src=""../images/help_home_09_Archivia.gif""></A></TD><TD vAlign=center width=169 align=right><IMG border=0 src=""../images/help_home_NTS_logo_down1.gif""></TD><TD vAlign=center width=146 align=right><A href=""http://www.ntsinformatica.it""><IMG border=0 alt=""Clicca per accedere al sito di NTS Informatica"" src=""../images/help_home_NTS_logo_down2.gif""></A></TD></TR><TR><TD width=987 colSpan=6><IMG border=0 src=""../images/help_home_Line.gif""></TD></TR></TABLE>", "")
      strNewTxt = strNewTxt.Replace("<TABLE border=0 cellSpacing=0 cellPadding=0><TR><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/software-retail-negozi/""><IMG border=0 src=""../images/help_home_01_Gestisci.gif""></A></TD><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/controllo-di-gestione/""><IMG border=0 src=""../images/help_home_02_Controlla.gif""></A></TD><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/crm/""><IMG border=0 src=""../images/help_home_03_Fidelizza.gif""></A></TD><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/analisi-dati-datawarehouse/""><IMG border=0 src=""../images/help_home_04_DWH.gif""></A></TD><TD vAlign=middle width=169 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/project-management/""><IMG border=0 src=""../images/help_home_05_Gestisci.gif""></A></TD><TD vAlign=middle width=146 align=right><IMG border=0 src=""../images/help_home_NTS_logo_up.gif""></TD></TR><TR><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/logistica/""><IMG border=0 src=""../images/help_home_06_Organizza.gif""></A></TD><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/customer-service/""><IMG border=0 src=""../images/help_home_07_Gestisci.gif""></A></TD><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/produzione/""><IMG border=0 src=""../images/help_home_08_Pianifica.gif""></A></TD><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/archiviazione-documenti/""><IMG border=0 src=""../images/help_home_09_Archivia.gif""></A></TD><TD vAlign=middle width=169 align=right><IMG border=0 src=""../images/help_home_NTS_logo_down1.gif""></TD><TD vAlign=middle width=146 align=right><A href=""http://www.ntsinformatica.it""><IMG border=0 alt=""Clicca per accedere al sito di NTS Informatica"" src=""../images/help_home_NTS_logo_down2.gif""></A></TD></TR><TR><TD width=987 colSpan=6><IMG border=0 src=""../images/help_home_Line.gif""></TD></TR></TABLE>", "")
      strNewTxt = strNewTxt.Replace("<TABLE cellSpacing=0 cellPadding=0 border=0><TR><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/software-retail-negozi/""><IMG border=0 src=""../images/help_home_01_Gestisci.gif""></A></TD><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/controllo-di-gestione/""><IMG border=0 src=""../images/help_home_02_Controlla.gif""></A></TD><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/crm/""><IMG border=0 src=""../images/help_home_03_Fidelizza.gif""></A></TD><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/analisi-dati-datawarehouse/""><IMG border=0 src=""../images/help_home_04_DWH.gif""></A></TD><TD vAlign=middle width=169 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/project-management/""><IMG border=0 src=""../images/help_home_05_Gestisci.gif""></A></TD><TD vAlign=middle width=146 align=right><IMG border=0 src=""../images/help_home_NTS_logo_up.gif""></TD></TR><TR><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/logistica/""><IMG border=0 src=""../images/help_home_06_Organizza.gif""></A></TD><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/customer-service/""><IMG border=0 src=""../images/help_home_07_Gestisci.gif""></A></TD><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/produzione/""><IMG border=0 src=""../images/help_home_08_Pianifica.gif""></A></TD><TD vAlign=middle width=168 align=center><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/archiviazione-documenti/""><IMG border=0 src=""../images/help_home_09_Archivia.gif""></A></TD><TD vAlign=middle width=169 align=right><IMG border=0 src=""../images/help_home_NTS_logo_down1.gif""></TD><TD vAlign=middle width=146 align=right><A href=""http://www.ntsinformatica.it""><IMG border=0 alt=""Clicca per accedere al sito di NTS Informatica"" src=""../images/help_home_NTS_logo_down2.gif""></A></TD></TR><TR><TD width=987 colSpan=6><IMG border=0 src=""../images/help_home_Line.gif""></TD></TR></TABLE>", "")

      If Not strNewTxt.Contains("<TABLE border=0 cellSpacing=0 cellPadding=0><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/software-retail-negozi/""><IMG border=0 src=""../images/help_home_01_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/controllo-di-gestione/""><IMG border=0 src=""../images/help_home_02_Controlla.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/crm/""><IMG border=0 src=""../images/help_home_03_Fidelizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/analisi-dati-datawarehouse/""><IMG border=0 src=""../images/help_home_04_DWH.gif""></A></TD><TD vAlign=center width=169 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/project-management/""><IMG border=0 src=""../images/help_home_05_Gestisci.gif""></A></TD><TD vAlign=center width=146 align=right><IMG border=0 src=""../images/help_home_NTS_logo_up.gif""></TD></TR><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/logistica/""><IMG border=0 src=""../images/help_home_06_Organizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/customer-service/""><IMG border=0 src=""../images/help_home_07_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/produzione/""><IMG border=0 src=""../images/help_home_08_Pianifica.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/archiviazione-documenti/""><IMG border=0 src=""../images/help_home_09_Archivia.gif""></A></TD><TD vAlign=center width=169 align=right><IMG border=0 src=""../images/help_home_NTS_logo_down1.gif""></TD><TD vAlign=center width=146 align=right><A href=""http://www.ntsinformatica.it""><IMG border=0 alt=""Clicca per accedere al sito di NTS Informatica"" src=""../images/help_home_NTS_logo_down2.gif""></A></TD></TR><TR><TD width=987 colSpan=6><IMG border=0 src=""../images/help_home_Line.gif""></TD></TR></TABLE>") Then
        strNewTxt = strNewTxt.Replace("</body></html>", "<TABLE border=0 cellSpacing=0 cellPadding=0><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/software-retail-negozi/""><IMG border=0 src=""../images/help_home_01_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/controllo-di-gestione/""><IMG border=0 src=""../images/help_home_02_Controlla.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/crm/""><IMG border=0 src=""../images/help_home_03_Fidelizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/analisi-dati-datawarehouse/""><IMG border=0 src=""../images/help_home_04_DWH.gif""></A></TD><TD vAlign=center width=169 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/project-management/""><IMG border=0 src=""../images/help_home_05_Gestisci.gif""></A></TD><TD vAlign=center width=146 align=right><IMG border=0 src=""../images/help_home_NTS_logo_up.gif""></TD></TR><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/logistica/""><IMG border=0 src=""../images/help_home_06_Organizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/customer-service/""><IMG border=0 src=""../images/help_home_07_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/produzione/""><IMG border=0 src=""../images/help_home_08_Pianifica.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/archiviazione-documenti/""><IMG border=0 src=""../images/help_home_09_Archivia.gif""></A></TD><TD vAlign=center width=169 align=right><IMG border=0 src=""../images/help_home_NTS_logo_down1.gif""></TD><TD vAlign=center width=146 align=right><A href=""http://www.ntsinformatica.it""><IMG border=0 alt=""Clicca per accedere al sito di NTS Informatica"" src=""../images/help_home_NTS_logo_down2.gif""></A></TD></TR><TR><TD width=987 colSpan=6><IMG border=0 src=""../images/help_home_Line.gif""></TD></TR></TABLE></body></html>")
      End If

      If strNewTxt.Contains("<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">") Then
        strNewTxt = strNewTxt.Replace("<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">", "<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01 Transitional//EN"" ""http://www.w3c.org/TR/1999/REC-html401-19991224/loose.dtd"">")
      End If

      If Not strNewTxt.Contains("</TD></TR></TABLE><TABLE border=0 cellSpacing=0 cellPadding=0><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/software-retail-negozi/""><IMG border=0 src=""../images/help_home_01_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/controllo-di-gestione/""><IMG border=0 src=""../images/help_home_02_Controlla.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/crm/""><IMG border=0 src=""../images/help_home_03_Fidelizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/analisi-dati-datawarehouse/""><IMG border=0 src=""../images/help_home_04_DWH.gif""></A></TD><TD vAlign=center width=169 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/project-management/""><IMG border=0 src=""../images/help_home_05_Gestisci.gif""></A></TD><TD vAlign=center width=146 align=right><IMG border=0 src=""../images/help_home_NTS_logo_up.gif""></TD></TR><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/logistica/""><IMG border=0 src=""../images/help_home_06_Organizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/customer-service/""><IMG border=0 src=""../images/help_home_07_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/produzione/""><IMG border=0 src=""../images/help_home_08_Pianifica.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/archiviazione-documenti/""><IMG border=0 src=""../images/help_home_09_Archivia.gif""></A></TD><TD vAlign=center width=169 align=right><IMG border=0 src=""../images/help_home_NTS_logo_down1.gif""></TD><TD vAlign=center width=146 align=right><A href=""http://www.ntsinformatica.it""><IMG border=0 alt=""Clicca per accedere al sito di NTS Informatica"" src=""../images/help_home_NTS_logo_down2.gif""></A></TD></TR><TR><TD width=987 colSpan=6><IMG border=0 src=""../images/help_home_Line.gif""></TD></TR></TABLE>") Then
        strNewTxt = strNewTxt.Replace("<TABLE border=0 cellSpacing=0 cellPadding=0><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/software-retail-negozi/""><IMG border=0 src=""../images/help_home_01_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/controllo-di-gestione/""><IMG border=0 src=""../images/help_home_02_Controlla.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/crm/""><IMG border=0 src=""../images/help_home_03_Fidelizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/analisi-dati-datawarehouse/""><IMG border=0 src=""../images/help_home_04_DWH.gif""></A></TD><TD vAlign=center width=169 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/project-management/""><IMG border=0 src=""../images/help_home_05_Gestisci.gif""></A></TD><TD vAlign=center width=146 align=right><IMG border=0 src=""../images/help_home_NTS_logo_up.gif""></TD></TR><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/logistica/""><IMG border=0 src=""../images/help_home_06_Organizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/customer-service/""><IMG border=0 src=""../images/help_home_07_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/produzione/""><IMG border=0 src=""../images/help_home_08_Pianifica.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/archiviazione-documenti/""><IMG border=0 src=""../images/help_home_09_Archivia.gif""></A></TD><TD vAlign=center width=169 align=right><IMG border=0 src=""../images/help_home_NTS_logo_down1.gif""></TD><TD vAlign=center width=146 align=right><A href=""http://www.ntsinformatica.it""><IMG border=0 alt=""Clicca per accedere al sito di NTS Informatica"" src=""../images/help_home_NTS_logo_down2.gif""></A></TD></TR><TR><TD width=987 colSpan=6><IMG border=0 src=""../images/help_home_Line.gif""></TD></TR></TABLE>", "</TD></TR></TABLE><TABLE border=0 cellSpacing=0 cellPadding=0><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/software-retail-negozi/""><IMG border=0 src=""../images/help_home_01_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/controllo-di-gestione/""><IMG border=0 src=""../images/help_home_02_Controlla.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/crm/""><IMG border=0 src=""../images/help_home_03_Fidelizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/analisi-dati-datawarehouse/""><IMG border=0 src=""../images/help_home_04_DWH.gif""></A></TD><TD vAlign=center width=169 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/project-management/""><IMG border=0 src=""../images/help_home_05_Gestisci.gif""></A></TD><TD vAlign=center width=146 align=right><IMG border=0 src=""../images/help_home_NTS_logo_up.gif""></TD></TR><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/logistica/""><IMG border=0 src=""../images/help_home_06_Organizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/customer-service/""><IMG border=0 src=""../images/help_home_07_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/produzione/""><IMG border=0 src=""../images/help_home_08_Pianifica.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/archiviazione-documenti/""><IMG border=0 src=""../images/help_home_09_Archivia.gif""></A></TD><TD vAlign=center width=169 align=right><IMG border=0 src=""../images/help_home_NTS_logo_down1.gif""></TD><TD vAlign=center width=146 align=right><A href=""http://www.ntsinformatica.it""><IMG border=0 alt=""Clicca per accedere al sito di NTS Informatica"" src=""../images/help_home_NTS_logo_down2.gif""></A></TD></TR><TR><TD width=987 colSpan=6><IMG border=0 src=""../images/help_home_Line.gif""></TD></TR></TABLE></body></html>")
      End If

      Try
        Dim strTryAggiornaData As String = ""
        strTryAggiornaData = strNewTxt.Substring(0, strNewTxt.IndexOf("<A name=hINIeDATlAGGp></A>")).Replace("<CODE>Data ultimo aggiornamento:", "<FONT size=2><CODE>Data ultimo aggiornamento:") & "<A name=hINIeDATlAGGp></A>" & Format(Date.Now, "dd/MM/yyyy") & "</FONT>" & strNewTxt.Substring(strNewTxt.IndexOf("<A name=hFINeDATlAGGp></A>"))
        If strTryAggiornaData <> "" Then strNewTxt = strTryAggiornaData
      Catch ex As Exception
      End Try


      While strNewTxt.Contains("<p></p>")
        strNewTxt = strNewTxt.Replace("<p></p>", "")
      End While
      While strNewTxt.Contains(" <li>")
        strNewTxt = strNewTxt.Replace(" <li>", "<li>")
      End While
      While strNewTxt.Contains("<p><p>")
        strNewTxt = strNewTxt.Replace("<p><p>", "<p>")
      End While
      While strNewTxt.Contains("</p></p>")
        strNewTxt = strNewTxt.Replace("</p></p>", "</p>")
      End While
      While strNewTxt.Contains("<code><CODE>")
        strNewTxt = strNewTxt.Replace("<code><CODE>", "<code>")
      End While
      While strNewTxt.Contains("</CODE></code>")
        strNewTxt = strNewTxt.Replace("</CODE></code>", "</code>")
      End While


      '  strNewTxt = strNewTxt.Substring(0, strNewTxt.IndexOf("<body")) & strNewTxt.Substring(strNewTxt.IndexOf("<body")).Replace(vbCrLf, "<br>")

      strNewTxt = strNewTxt.Substring(0, strNewTxt.IndexOf("</b></FONT></p></TD></TR><TR><TD vAlign=middle colSpan=4 align=left></TD></TR></TABLE><TABLE width=987 border=0><TR><TD>") + "</b></FONT></p></TD></TR><TR><TD vAlign=middle colSpan=4 align=left></TD></TR></TABLE><TABLE width=987 border=0><TR><TD>".Length - 1) & strNewTxt.Substring(strNewTxt.IndexOf("</b></FONT></p></TD></TR><TR><TD vAlign=middle colSpan=4 align=left></TD></TR></TABLE><TABLE width=987 border=0><TR><TD>") + ("</b></FONT></p></TD></TR><TR><TD vAlign=middle colSpan=4 align=left></TD></TR></TABLE><TABLE width=987 border=0><TR><TD>".Length - 1), strNewTxt.IndexOf("</TD></TR></TABLE><TABLE border=0 cellSpacing=0 cellPadding=0><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/software-retail-negozi/"">") + 1 - (strNewTxt.IndexOf("</b></FONT></p></TD></TR><TR><TD vAlign=middle colSpan=4 align=left></TD></TR></TABLE><TABLE width=987 border=0><TR><TD>") + "</b></FONT></p></TD></TR><TR><TD vAlign=middle colSpan=4 align=left></TD></TR></TABLE><TABLE width=987 border=0><TR><TD>".Length - 1)).Replace("><", ">" & vbCrLf & "<") & strNewTxt.Substring(strNewTxt.IndexOf("</TD></TR></TABLE><TABLE border=0 cellSpacing=0 cellPadding=0><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/software-retail-negozi/"">") + 1)

      strNewTxt = strNewTxt.Replace(">" & vbCrLf & "<li>", "><li>")
      strNewTxt = strNewTxt.Replace("<li>", vbCrLf & "<li>")
      strNewTxt = strNewTxt.Replace(">" & vbCrLf & "<br>", "><br>")
      strNewTxt = strNewTxt.Replace("<br>", vbCrLf & "<br>")

      strNewTxt = strNewTxt.Replace("<pre>" & vbCrLf & "<code>", "<pre><code>")
      strNewTxt = strNewTxt.Replace("</code>" & vbCrLf & "</pre>", "</code></pre>")

      While strNewTxt.Contains("<code>" & vbTab) Or strNewTxt.Contains("<code>" & vbCrLf)
        strNewTxt = strNewTxt.Replace("<code>" & vbCrLf, "<code>")
        strNewTxt = strNewTxt.Replace("<code>" & vbTab, "<code>")
      End While

      While strNewTxt.Contains(" </code>") Or strNewTxt.Contains(vbTab & "</code>") Or strNewTxt.Contains(vbCrLf & "</code>")
        strNewTxt = strNewTxt.Replace(vbCrLf & "</code>", "</code>")
        strNewTxt = strNewTxt.Replace(vbTab & "</code>", "</code>")
        strNewTxt = strNewTxt.Replace(" </code>", "</code>")
      End While

      w1 = New StreamWriter(strLastPage, False, System.Text.Encoding.Default)
      w1.Write(strNewTxt)
      w1.Close()

      Return True
    Catch ex As Exception
      Try
        If Not w1 Is Nothing Then w1.Close()
      Catch ex2 As Exception
      End Try
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Sub cmdAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAnnulla.Click
    Try
      WebBrows.Refresh()
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdEndEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEndEdit.Click
    Try
      If oApp.MsgBoxInfoYesNo_DefYes("Salvare le modifiche prima di uscire dall'editor?") = Windows.Forms.DialogResult.Yes Then
        If Not SalvaHTML() Then Return
      End If
      GoPage(strLastPage)
      pnEdit.Visible = False
      lbFindInPage.Visible = False
      WebBrows.IsWebBrowserContextMenuEnabled = False
      WebBrows.AllowNavigation = True
      docHTML.designMode = "Off"
      pnLeft.Enabled = True
      pnTop.Enabled = True
    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Function BackupHTML(ByRef strTxt As String) As Boolean
    Dim wBck As StreamWriter = Nothing
    Dim filePage As FileInfo = Nothing
    Dim dirAscHelp As DirectoryInfo = Nothing
    Try
      strTxt = New StreamReader(WebBrows.DocumentStream, System.Text.Encoding.Default).ReadToEnd
      strTxt = strTxt.Replace("‘", "'").Replace("’", "'").Replace("&nbsp;", " ").Replace("…", "...")
      strTxt = strTxt.Replace(ChrW(8221), """").Replace(ChrW(8220), """")
      Try
        dirAscHelp = New DirectoryInfo(oApp.AscDir & "\help")
        If Not dirAscHelp.Exists Then
          dirAscHelp.Create()
        End If
      Catch ex As Exception
      End Try
      Try
        filePage = New FileInfo(strLastPage)
        wBck = New StreamWriter(oApp.AscDir & "\help\" & Format("0", Now.Year.ToString).PadLeft(4, "0"c) & _
            Format("00", Now.Month.ToString).PadLeft(2, "0"c) & _
            Format("00", Now.Day.ToString).PadLeft(2, "0"c) & _
            Format("00", Now.Hour.ToString).PadLeft(2, "0"c) & _
            Format("00", Now.Minute.ToString).PadLeft(2, "0"c) & _
            Format("00", Now.Second.ToString).PadLeft(2, "0"c) & filePage.Name.Replace(filePage.Extension, _
            filePage.Extension), False, System.Text.Encoding.Default)
        wBck.Write(strTxt)
        wBck.Close()
      Catch ex As Exception
        Try
          If Not wBck Is Nothing Then wBck.Close()
        Catch ex2 As Exception
        End Try
        Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      End Try

      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub cmdImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImage.Click
    Dim frmImg As FRM__HIMG = Nothing
    Dim oParamIMG As CLE__CLDP

    Try
      frmImg = CType(NTSNewFormModal("FRM__HIMG"), FRM__HIMG)

      oParamIMG = New CLE__CLDP
      oParamIMG.bPar1 = False
      oParamIMG.strPar1 = ""
      Dim Range As IHTMLTxtRange
      Range = CType(docHTML.selection.createRange(), IHTMLTxtRange)
      frmImg.Init(oMenu, oParamIMG)
      frmImg.strHelpDirectoryLocal = strHelpDirectoryLocal
      frmImg.WebBrows = WebBrows
      frmImg.ShowDialog()

      If oParamIMG.bPar1 Then
        Try
          Range.pasteHTML(oParamIMG.strPar1)
          Range.collapse(False)
          Range.select()
          cmdSave_Click(Nothing, Nothing)
        Catch ex As Exception
          oApp.MsgBoxErr(oApp.Tr(Me, 129073271974620710, "Impossibile inserire un'immagine nel punto selezionato."))
        End Try
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdLista_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLista.Click
    Dim frmHele As FRM__HELE = Nothing
    Dim oParamHele As CLE__CLDP

    Try
      frmHele = CType(NTSNewFormModal("FRM__HELE"), FRM__HELE)

      oParamHele = New CLE__CLDP
      oParamHele.bPar1 = False
      oParamHele.bPar2 = False
      Dim Range As IHTMLTxtRange
      Range = CType(docHTML.selection.createRange(), IHTMLTxtRange)
      frmHele.Init(oMenu, oParamHele)
      frmHele.ShowDialog()

      If oParamHele.bPar1 Then
        If oParamHele.bPar2 = True Then
          'elenco puntato
          Dim strHtml As String = ""
          strHtml = "<ul><li></li></ul>"
          Range.pasteHTML(strHtml)
          Range.collapse(False)
          Range.select()
        Else
          'elenco numerato
          Dim strHtml As String = ""
          strHtml = "<ol><li></li></ol>"
          Range.pasteHTML(strHtml)
          Range.collapse(False)
          Range.select()
        End If
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdAcapo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAcapo.Click
    Dim strHtml As String = ""
    Dim Range As IHTMLTxtRange
    Try
      strHtml = "<br>"
      Try
        Range = CType(docHTML.selection.createRange(), IHTMLTxtRange)
        Range.pasteHTML(strHtml)
        Range.collapse(False)
        Range.select()
      Catch ex As Exception
        oApp.MsgBoxErr(oApp.Tr(Me, 129073271974620231, "Impossibile andare a capo nel punto selezionato."))
      End Try
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdInsTabe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdInsTabe.Click
    Dim frmInsTabe As FRM__HELT = Nothing
    Dim oPar As CLE__CLDP

    Try
      frmInsTabe = CType(NTSNewFormModal("FRM__HELT"), FRM__HELT)

      oPar = New CLE__CLDP
      oPar.bPar1 = False
      frmInsTabe.Init(oMenu, oPar, DittaCorrente)
      frmInsTabe.ShowDialog()
      If oPar.bPar1 Then
        Dim Range As IHTMLTxtRange
        Range = CType(docHTML.selection.createRange(), IHTMLTxtRange)
        Range.pasteHTML(oPar.strPar1)
        Range.collapse(False)
        Range.select()
      End If

    Catch ex As Exception
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public RangeTMP As IHTMLTxtRange
  Public Overridable Sub FRM__HELP_Deactivate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Deactivate
  End Sub
  Public Overridable Sub FRM__HELP_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop, pnEdit.DragDrop, pnCerca.DragDrop, pnContenuto.DragDrop, pnTop.DragDrop, pnLeft.DragDrop
  End Sub
  Public Overridable Sub cmdLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLink.Click
    Dim frmInsLink As FRM__HREF = Nothing
    Dim oPar As CLE__CLDP

    Try
      frmInsLink = CType(NTSNewFormModal("FRM__HREF"), FRM__HREF)

      oPar = New CLE__CLDP
      oPar.bPar1 = False
      frmInsLink.Init(oMenu, oPar, DittaCorrente)
      frmInsLink.strMainDir = strMainDir
      frmInsLink.strSep = strSep
      frmInsLink.ShowDialog()
      If oPar.bPar1 Then
        Dim Range As IHTMLTxtRange
        Range = CType(docHTML.selection.createRange(), IHTMLTxtRange)
        Range.pasteHTML(oPar.strPar1)
        Range.collapse(False)
        Range.select()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Sub
  Public Overridable Sub cmdBookmark_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBookmark.Click
    Try
      If pnRight.Visible = False Then
        GctlSetVisEnab(pnRight, True)
      Else
        pnRight.Visible = False
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub ckBold_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckBold.CheckedChanged
    Try
      If ckBold.Checked <> docHTML.queryCommandState("Bold") Then
        docHTML.execCommand("Bold", ckBold.Checked, Nothing)
        WebBrows.Document.Body.Focus()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckItalic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckItalic.CheckedChanged
    Try
      If ckItalic.Checked <> docHTML.queryCommandState("Italic") Then
        docHTML.execCommand("Italic", ckBold.Checked, Nothing)
        WebBrows.Document.Body.Focus()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub ckUnderline_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ckUnderline.CheckedChanged
    Try
      If ckUnderline.Checked <> docHTML.queryCommandState("Underline") Then
        docHTML.execCommand("Underline", ckBold.Checked, Nothing)
        WebBrows.Document.Body.Focus()
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cbFont_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFont.SelectedIndexChanged
    Try
      If docHTML.body Is Nothing Then Return
      If NTSCStr(cbFont.SelectedValue).Trim = "" Or cbFont.SelectedValue = NTSCStr(docHTML.queryCommandValue("FontName")) Then Return

      If docHTML.selection.type = "None" Then
        ' Do il focus al controllo, premo spazio, cambio il font e cancello lo spazio
        ' tutto questo perchè altrimenti quando ridò il focus al controllo ricarica l'impostazione precedente
        WebBrows.Document.Body.Focus()
        NTSSendKeys.Send(0, " ")
        docHTML.execCommand("FontName", False, cbFont.SelectedValue)
        NTSSendKeys.Send(0, "{BS}")
      Else
        ' Se è stato selezionato qualcosa applico senza fare modifiche al testo
        WebBrows.Document.Body.Focus()
        docHTML.execCommand("FontName", False, cbFont.SelectedValue)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbFont_DrawItem(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.ListBoxDrawItemEventArgs) Handles cbFont.DrawItem
    Try
      ' Colora il combobox     
      If e.Item.ToString.Trim = "" Then Return

      e.Appearance.DrawBackground(e.Cache, e.Bounds)
      ' La scritta
      Try
        e.Graphics.DrawString(e.Item.ToString, New Font(e.Item.ToString, 9), Brushes.Black, e.Bounds)
      Catch ex As Exception
        Try
          ' Alcuni font non supportano il font regolare, allora in caso gestisco con il grasseto
          e.Graphics.DrawString(e.Item.ToString, New Font(e.Item.ToString, 9, FontStyle.Bold), Brushes.Black, e.Bounds)
        Catch ex1 As Exception
          ' Alcuni font non supportano neanche font grassetto, allora in caso gestisco con il corsivo
          e.Graphics.DrawString(e.Item.ToString, New Font(e.Item.ToString, 9, FontStyle.Italic), Brushes.Black, e.Bounds)
        End Try
      End Try

      e.Handled = True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub cbFontDim_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFontDim.SelectedIndexChanged
    Try
      If docHTML.body Is Nothing Then Return
      If NTSCStr(cbFont.SelectedValue).Trim = "" Or cbFontDim.SelectedValue = NTSCStr(docHTML.queryCommandValue("FontSize")) Then Return

      If docHTML.selection.type = "None" Then
        ' Do il focus al controllo, premo spazio, cambio il font e cancello lo spazio
        ' tutto questo perchè altrimenti quando ridò il focus al controllo ricarica l'impostazione precedente
        WebBrows.Document.Body.Focus()
        NTSSendKeys.Send(0, " ")
        docHTML.execCommand("FontSize", False, cbFontDim.SelectedValue)
        NTSSendKeys.Send(0, "{BS}")
      Else
        ' Se è stato selezionato qualcosa applico senza fare modifiche al testo
        WebBrows.Document.Body.Focus()
        docHTML.execCommand("FontSize", False, cbFontDim.SelectedValue)
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tmTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmTimer.Tick
    'Dim strTmp As String = ""
    'Try
    '  If docHTML Is Nothing Then Return
    '  If Not WebBrows.Document.Focused Then Return

    '  ckBold.Checked = docHTML.queryCommandState("Bold")
    '  ckItalic.Checked = docHTML.queryCommandState("Italic")
    '  ckUnderline.Checked = docHTML.queryCommandState("Underline")

    '  cbFont.SelectedValue = NTSCStr(docHTML.queryCommandValue("FontName"))
    '  cbFontDim.SelectedValue = NTSCStr(docHTML.queryCommandValue("FontSize"))

    '  '' la QueryCommandValue dei colori invece che tornare RGB torna BGR, allora ribalto il colore.
    '  'Try
    '  '  strTmp = Hex(docHTML.queryCommandValue("BackColor")).ToString.PadLeft(6, "0"c)
    '  '  cbColoreSfondo.SelectedValue = strTmp.Substring(4, 2) & strTmp.Substring(2, 2) & strTmp.Substring(0, 2)
    '  'Catch ex As Exception
    '  '  'strTmp = Hex(Color.FromName(NTSCStr(docHTML.queryCommandValue("BackColor"))).ToArgb).ToString()
    '  '  'If strTmp.Length >= 8 Then
    '  '  '  strTmp = strTmp.Substring(2, 6)
    '  '  '  cbColoreTesto.SelectedValue = strTmp.Substring(4, 2) & strTmp.Substring(2, 2) & strTmp.Substring(0, 2)
    '  '  'ElseIf strTmp.Length >= 6 Then
    '  '  '  cbColoreTesto.SelectedValue = strTmp.Substring(4, 2) & strTmp.Substring(2, 2) & strTmp.Substring(0, 2)
    '  '  'End If
    '  '  cbColoreTesto.SelectedValue = NTSCStr(docHTML.queryCommandValue("BackColor"))
    '  'End Try

    '  'Try
    '  '  strTmp = Hex(docHTML.queryCommandValue("ForeColor")).ToString.PadLeft(6, "0"c)
    '  '  cbColoreTesto.SelectedValue = strTmp.Substring(4, 2) & strTmp.Substring(2, 2) & strTmp.Substring(0, 2)
    '  'Catch ex As Exception
    '  '  'strTmp = Hex(Color.FromName(NTSCStr(docHTML.queryCommandValue("ForeColor"))).ToArgb).ToString()
    '  '  'If strTmp.Length >= 8 Then
    '  '  '  strTmp = strTmp.Substring(2, 6)
    '  '  '  cbColoreTesto.SelectedValue = strTmp.Substring(4, 2) & strTmp.Substring(2, 2) & strTmp.Substring(0, 2)
    '  '  'ElseIf strTmp.Length >= 6 Then
    '  '  '  cbColoreTesto.SelectedValue = strTmp.Substring(4, 2) & strTmp.Substring(2, 2) & strTmp.Substring(0, 2)
    '  '  'End If
    '  '  cbColoreTesto.SelectedValue = NTSCStr(docHTML.queryCommandValue("ForeColor"))
    '  'End Try

    '  '' Allineamento destra e sinistra
    '  'If docHTML.queryCommandState("JustifyLeft") Then
    '  '  cbGiustificato.SelectedValue = "JustifyLeft"
    '  'ElseIf docHTML.queryCommandState("JustifyCenter") Then
    '  '  cbGiustificato.SelectedValue = "JustifyCenter"
    '  'ElseIf docHTML.queryCommandState("JustifyRight") Then
    '  '  cbGiustificato.SelectedValue = "JustifyRight"
    '  'End If
    'Catch ex As Exception
    '  ' non gestisco l'errore se capita qui
    'End Try
  End Sub
#End Region

  'Public oimageHTML() As mshtml.HTMLImgClass
  Public Overridable Sub StartEdit()
    Try
      If pnEdit.Visible = False Then
        pnEdit.Visible = True
        lbFindInPage.Visible = False
        WebBrows.IsWebBrowserContextMenuEnabled = True
        WebBrows.AllowNavigation = False
        docHTML = CType(WebBrows.Document.DomDocument, IHTMLDocument2)
        lbPage.Text = WebBrows.Url.ToString.Substring(8).Replace("/", "\")
        'If docHTML.images.length > 0 Then
        '  Dim oimageHTMLTmp(docHTML.images.length - 1) As mshtml.HTMLImgClass
        '  For i As Integer = 0 To docHTML.images.length - 1
        '    oimageHTMLTmp(i) = CType(docHTML.images.item(i, 0), mshtml.HTMLImgClass)
        '  Next
        '  oimageHTML = oimageHTMLTmp
        'Else
        '  oimageHTML = Nothing
        'End If
        docHTML.designMode = "On"
        pnLeft.Enabled = False
        pnTop.Enabled = False
        Try
          CaricaOpzioni()
        Catch exOpz As Exception
        End Try
        'tmTimer.Start()
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public dttXmlOpzio As DataTable
  Public Overridable Sub CaricaOpzioni()
    Dim r1 As StreamReader = Nothing
    Dim strFile As String = ""
    Dim strTxt As String = ""
    Dim strTitle As String = ""
    Dim i As Integer = 0
    Dim n As Integer = 0
    Dim oNode As TreeNode = Nothing
    Dim oCurrNode As TreeNode = Nothing
    Dim oLastNodeAdd As TreeNode = Nothing

    Try

      dttXmlOpzio = New DataTable
      dttXmlOpzio.Columns.Add("xx_name", GetType(System.String))
      dttXmlOpzio.Columns.Add("xx_opzione", GetType(System.String))
      dttXmlOpzio.Columns.Add("xx_testo", GetType(System.String))
      dttXmlOpzio.TableName = "OPZIONI"

      If System.IO.File.Exists("\\s01\Utenti\Filippucci\guida\Script\opzioni.xml") Then
        dttXmlOpzio.ReadXml("\\s01\Utenti\Filippucci\guida\Script\opzioni.xml")
      End If

      dttXmlOpzio.AcceptChanges()

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public strEditor As String = "notepad"
  Public Overridable Sub cmdEditor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditor.Click
    Try
      Dim oFileInfo As New IO.FileInfo(lbPage.Text)
      If oFileInfo.Exists Then
        strEditor = oMenu.GetSettingBus("BS--HELP", "RECENT", "USER", "Editor", "notepad", " ", "notepad")
        NTSProcessStart(strEditor, lbPage.Text)
      Else
        If oApp.MsgBoxInfoYesNo_DefYes("Il file attualmente in editing non esiste, vuoi provare a creare il file htm template da sorgente?") = Windows.Forms.DialogResult.Yes Then
          If LeggiSorgente(oCallParams.strParam) Then

          End If
        End If
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfig.Click
    Try
      Dim ofdImg As New NTSOpenFileDialog
      ofdImg.Filter = "Applicazione|*.exe"
      ofdImg.InitialDirectory = oApp.Dir
      ofdImg.oMenu = oMenu
      If ofdImg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        oMenu.SaveSettingBus("BS--HELP", "RECENT", "USER", "Editor", ofdImg.FileName, " ", "NNS", "...", "...")
      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Function LeggiSorgente(ByVal strPage As String) As Boolean
    Dim srIn As IO.StreamReader
    Dim srOut As IO.StreamWriter
    Dim line As String
    Dim strModuli As String = ""
    Dim strToolbarCollection As String() = Nothing
    Dim strGridCollection As String() = Nothing
    Dim strToolbar As String = ""
    Dim strGrid As String = ""
    '---------
    Dim strNTSBar As String = ""
    Dim strNTSBarButtonItem As String = ""
    Dim strNTSBarManagerItem As String = ""
    Dim strNTSBarMenuItem As String = ""
    Dim strNTSBarSubItem As String = ""
    Dim strNTSGridView As String = ""
    Dim strNTSGridColumn As String = ""
    Dim strNTSButton As String = ""
    Dim strNTSCheckBox As String = ""
    Dim strNTSGroupBox As String = ""
    Dim strNTSLabel As String = ""
    Dim strNTSRadioButton As String = ""
    Dim strNTSTabPage As String = ""

    Dim strNTSControls As String = ""
    Dim strNTSControlsCollection() As String
    Dim strBe As String = "", strBd As String = "", strBn As String = ""
    Try
      Try
        'designer
        srIn = New IO.StreamReader("c:\biz2012\bn" & strPage.Substring(2, 6).Replace("--", "__") & "\bn" & strPage.Substring(2, 6).Replace("--", "__") & ".Designer.vb", System.Text.Encoding.Default)
        line = srIn.ReadToEnd
        srIn.Close()
        srIn.Dispose()

        'vb
        srIn = New IO.StreamReader("c:\biz2012\bn" & strPage.Substring(2, 6).Replace("--", "__") & "\bn" & strPage.Substring(2, 6).Replace("--", "__") & ".vb", System.Text.Encoding.Default)
        line = line & vbCrLf & srIn.ReadToEnd
        srIn.Close()
        srIn.Dispose()

        Try
          srIn = New IO.StreamReader("c:\biz2012\be" & strPage.Substring(2, 6).Replace("--", "__") & "\be" & strPage.Substring(2, 6).Replace("--", "__") & ".vb", System.Text.Encoding.Default)
          strBe = srIn.ReadToEnd
          srIn.Close()
          srIn.Dispose()
        Catch ex As Exception
        End Try

        Try
          srIn = New IO.StreamReader("c:\biz2012\bd" & strPage.Substring(2, 6).Replace("--", "__") & "\bd" & strPage.Substring(2, 6).Replace("--", "__") & ".vb", System.Text.Encoding.Default)
          strBd = srIn.ReadToEnd
          srIn.Close()
          srIn.Dispose()
        Catch ex As Exception
        End Try

        srOut = New StreamWriter("c:\biz2012\bn.txt", False, System.Text.Encoding.Default)
        srOut.Write(line)
        srOut.Close()
        srOut.Dispose()
        strBn = line

        srIn = New IO.StreamReader("c:\biz2012\bn.txt", System.Text.Encoding.Default)
      Catch ex As Exception
        Return False
      End Try

      'Cerco moduli e controlli
      Do Until srIn.EndOfStream
        line = srIn.ReadLine()

        If line.ToLower.Contains("Moduli_P As Integer = ".ToLower) Or _
           line.ToLower.Contains("ModuliExt_P As Integer = ".ToLower) Or _
           line.ToLower.Contains("ModuliSup_P As Integer = ".ToLower) Or _
           line.ToLower.Contains("ModuliSupExt_P As Integer = ".ToLower) Or _
           line.ToLower.Contains("ModuliPtn_P As Integer = ".ToLower) Or _
           line.ToLower.Contains("ModuliPtnExt_P As Integer = ".ToLower) Then
          strModuli &= "+" & line.Split("="c)(1).Replace(" ", "")
        End If

        If line.Contains("Me.") Then
          If line.Contains(" = New NTSInformatica.NTSBarManager") Then
            strNTSBarManagerItem &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSBarManager")).Replace("Me.", "").Trim) & " "
          End If
          If line.Contains(" = New NTSInformatica.NTSBarButtonItem") Then
            strNTSBarButtonItem &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSBarButtonItem")).Replace("Me.", "").Trim) & " "
          End If
          If line.Contains(" = New NTSInformatica.NTSBarMenuItem") Then
            strNTSBarMenuItem &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSBarMenuItem")).Replace("Me.", "").Trim) & " "
          End If
          If line.Contains(" = New NTSInformatica.NTSBarSubItem") Then
            strNTSBarSubItem &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSBarSubItem")).Replace("Me.", "").Trim) & " "
          End If
          If line.Contains(" = New NTSInformatica.NTSGridColumn") Then
            strNTSGridColumn &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSGridColumn")).Replace("Me.", "").Trim) & " "
          End If
          If line.Contains(" = New NTSInformatica.NTSGridView") Then
            strNTSGridView &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSGridView")).Replace("Me.", "").Trim) & " "
            strNTSControls &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSGridView")).Replace("Me.", "").Trim)
          End If
          If line.Contains(" = New NTSInformatica.NTSButton") Then
            strNTSButton &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSButton")).Replace("Me.", "").Trim) & " "
            strNTSControls &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSButton")).Replace("Me.", "").Trim)
          End If
          If line.Contains(" = New NTSInformatica.NTSCheckBox") Then
            strNTSCheckBox &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSCheckBox")).Replace("Me.", "").Trim) & " "
            strNTSControls &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSCheckBox")).Replace("Me.", "").Trim)
          End If
          If line.Contains(" = New NTSInformatica.NTSGroupBox") Then
            strNTSGroupBox &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSGroupBox")).Replace("Me.", "").Trim) & " "
            strNTSControls &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSGroupBox")).Replace("Me.", "").Trim)
          End If
          If line.Contains(" = New NTSInformatica.NTSLabel") Then
            strNTSLabel &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSLabel")).Replace("Me.", "").Trim) & " "
            strNTSControls &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSLabel")).Replace("Me.", "").Trim)
          End If
          If line.Contains(" = New NTSInformatica.NTSRadioButton") Then
            strNTSRadioButton &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSRadioButton")).Replace("Me.", "").Trim) & " "
            strNTSControls &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSRadioButton")).Replace("Me.", "").Trim)
          End If
          If line.Contains(" = New NTSInformatica.NTSTabPage") Then
            strNTSTabPage &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSTabPage")).Replace("Me.", "").Trim) & " "
            strNTSControls &= " " & (line.Substring(0, line.IndexOf(" = New NTSInformatica.NTSTabPage")).Replace("Me.", "").Trim)
          End If
        End If

        If line.Contains("New DevExpress.XtraBars.Bar() {Me.") Then
          strNTSBar = " " & line.Substring(line.IndexOf("New DevExpress.XtraBars.Bar() {Me.") + "New DevExpress.XtraBars.Bar() {Me.".Length).Replace("})", "").Replace(", ", ",").Replace("Me.", "").Trim.Replace(",", " ").Trim
        End If

        If line.Contains("New DevExpress.XtraBars.BarItem() {Me.") Then
          strToolbar &= " " & line.Substring(line.IndexOf("New DevExpress.XtraBars.BarItem() {Me.") + "New DevExpress.XtraBars.BarItem() {Me.".Length).Replace("})", "").Replace(", ", ",").Replace("Me.", "").Trim.Replace(",", " ").Trim
        End If

        If line.Contains(".Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.") Then
          strGrid &= " " & line.Replace(".Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.", ",").Replace("})", "").Replace(", ", ",").Replace("Me.", "").Trim.Replace(",", " ").Trim
        End If
      Loop
      srIn.Close()
      srIn.Dispose()
      strToolbarCollection = strToolbar.Trim.Split(" "c)
      strGridCollection = strGrid.Trim.Split(" "c)
      strNTSControlsCollection = strNTSControls.Trim.Split(" "c)
      Try
        srIn = New IO.StreamReader("c:\biz2012\bn.txt", System.Text.Encoding.Default)
      Catch ex As Exception
        Return False
      End Try

      Dim dttNTSBarItem As New DataTable
      dttNTSBarItem.Columns.Add("cod", GetType(String))
      dttNTSBarItem.Columns.Add("ID", GetType(String))
      dttNTSBarItem.Columns.Add("type", GetType(String))
      dttNTSBarItem.Columns.Add("image", GetType(String))
      dttNTSBarItem.Columns.Add("shortcut", GetType(String))
      dttNTSBarItem.Columns.Add("name", GetType(String))
      For Each strTMPBAR As String In strToolbar.Trim.Split(" "c)
        Dim strType As String = "NTSBarButtonItem"
        If strNTSBarMenuItem.Contains(" " & strTMPBAR & " ") Then strType = "NTSBarMenuItem"
        If strNTSBarSubItem.Contains(" " & strTMPBAR & " ") Then strType = "NTSBarSubItem"
        dttNTSBarItem.Rows.Add(New Object() {strTMPBAR, "0", strType, "", "", ""})
      Next
      dttNTSBarItem.AcceptChanges()
      Dim id As Integer = 1
      Do Until srIn.EndOfStream
        line = srIn.ReadLine()
        If line.Contains(".LinksPersistInfo.AddRange") Then
          Dim strTempBar As String = line.Split("{"c)(1).Split("}"c)(0).Replace(", True", "").Replace(", False", "").Replace("New DevExpress.XtraBars.LinkPersistInfo(Me.", "").Replace(" ", "").Replace(")", "")
          Dim controllo As String = line.Substring(0, line.IndexOf(".LinksPersistInfo.AddRange")).Replace("Me.", "").Trim
          Dim nRottura As Integer = 0
          If dttNTSBarItem.Select("cod=" & CStrSQL(controllo)).Length > 0 Then
            nRottura = NTSCInt(dttNTSBarItem.Select("cod=" & CStrSQL(controllo))(0)!ID)
          End If
          Dim nIDNext As Integer = 0
          For Each dtrTMP As DataRow In dttNTSBarItem.Rows
            If nRottura > 0 AndAlso NTSCInt(dtrTMP!ID) > nRottura AndAlso NTSCInt(dtrTMP!ID) < id Then
              dtrTMP!ID = NTSCInt(dtrTMP!ID) + strTempBar.Split(","c).Length + 5
              nIDNext = NTSCInt(dtrTMP!ID)
            End If
          Next
          For Each strEach As String In strTempBar.Split(","c)
            For Each dtrTMP As DataRow In dttNTSBarItem.Rows
              If dtrTMP!cod.ToString = strEach Then
                dtrTMP!ID = id
                id += 1
                Exit For
              End If
            Next
          Next
          If nIDNext > 0 Then
            id = nIDNext + 1
          End If
        End If
        If line.Contains(".ItemShortcut =") Then
          For Each dtrTMP As DataRow In dttNTSBarItem.Rows
            If line.Contains(dtrTMP!cod.ToString) Then
              dtrTMP!shortcut = line.Split("="c)(1).Replace(" Or ", "+").Replace("New DevExpress.XtraBars.BarShortcut", "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace("System.Windows.Forms.Keys.", "").Replace("Control", "Ctrl")
              Exit For
            End If
          Next
        End If
        If line.Contains(".GlyphPath = (") Then
          For Each dtrTMP As DataRow In dttNTSBarItem.Rows
            If line.Contains(dtrTMP!cod.ToString) Then
              dtrTMP!image = line.Split("\"c)(1).Replace("""", "").Replace(")", "")
              Exit For
            End If
          Next
        End If
      Loop
      dttNTSBarItem.AcceptChanges()

      srIn.Close()
      srIn.Dispose()


      Dim bInizComponent As Boolean = False
      Dim strCtrl As String = ""
      Dim strText As String = ""
      Dim strBody As String = ""
      Dim strTitolo As String = ""
      Try
        srIn = New IO.StreamReader("c:\biz2012\bn.txt", System.Text.Encoding.Default)
      Catch ex As Exception
        Return False
      End Try
      Do Until srIn.EndOfStream
        line = srIn.ReadLine()
        strCtrl = ""
        strText = ""

        If line.Contains(".NTSSetParam(") Then
          If strNTSGridView.Trim.Length > 0 Then
            For Each strGrv As String In strNTSGridView.Trim.Split(" "c)
              If line.Trim.ToLower.StartsWith(strGrv.ToLower) Then

              End If
            Next

          End If
        End If

        If line.Contains("Sub InitializeComponent(") Then
          bInizComponent = True
        End If
        If bInizComponent Then
          If line.Contains("End Sub") Then
            bInizComponent = False
          End If

          If line.Contains("Me.") Then
            If line.Contains(".Caption = ") Then
              If line.Contains(".Properties.Caption = ") Then
                strCtrl = line.Substring(0, line.IndexOf(".Properties.Caption = ")).Replace("Me.", "").Trim
                strText = line.Substring(line.IndexOf("=") + 1).Trim.Replace("""", "")
              Else
                strCtrl = line.Substring(0, line.IndexOf(".Caption = ")).Replace("Me.", "").Trim
                strText = line.Substring(line.IndexOf("=") + 1).Trim.Replace("""", "")
              End If
            End If
            If line.Contains(".Text = ") Then
              strCtrl = line.Substring(0, line.IndexOf(".Text = ")).Replace("Me.", "").Trim
              strText = line.Substring(line.IndexOf("=") + 1).Trim.Replace("""", "")
            End If
            If line.Contains("Me.Text = ") Then
              strTitolo = line.Substring(line.IndexOf("=") + 1).Trim.Replace("""", "")
            End If
            If strCtrl <> "" Then
              For Each dtrTMP As DataRow In dttNTSBarItem.Rows
                If dtrTMP!cod.ToString = strCtrl Then
                  dtrTMP!name = strText
                End If
              Next
              dttNTSBarItem.AcceptChanges()
              If strNTSBarMenuItem.Contains(" " & strCtrl & " ") Then
                For i As Integer = 0 To strToolbarCollection.Length - 1
                  If strToolbarCollection(i) = strCtrl Then
                    strToolbarCollection(i) = "<ul><li><b class=""nomecampo""><a name=""" & strCtrl & """>" & strText & "</a></b></li></ul>"
                  End If
                Next
              End If
              If strNTSBarButtonItem.Contains(" " & strCtrl & " ") Then
                For i As Integer = 0 To strToolbarCollection.Length - 1
                  If strToolbarCollection(i) = strCtrl Then
                    strToolbarCollection(i) = "<p><b class=""nomecampo""><a name=""" & strCtrl & """>" & strText & "</a></b></p>"
                  End If
                Next
              End If
              If strNTSBarSubItem.Contains(" " & strCtrl & " ") Then
                For i As Integer = 0 To strToolbarCollection.Length - 1
                  If strToolbarCollection(i) = strCtrl Then
                    strToolbarCollection(i) = "<p><b class=""nomecampo""><u><a name=""" & strCtrl & """>" & strText & "</a></u></b></p>"
                  End If
                Next
              End If
              If strNTSGridView.Contains(" " & strCtrl & " ") Then
                For i As Integer = 0 To strGridCollection.Length - 1
                  If strGridCollection(i) = strCtrl Then
                    strGridCollection(i) = "<p><b class=""nomecampo""><u><i><a name=""" & strCtrl & """>" & strText & "</a></i></u></b></p>"
                  End If
                Next
              End If
              If strNTSGridColumn.Contains(" " & strCtrl & " ") Then
                For i As Integer = 0 To strGridCollection.Length - 1
                  If strGridCollection(i) = strCtrl Then
                    strGridCollection(i) = "<p><b class=""nomecampo""><a name=""" & strCtrl & """>" & strText & "</a></b></p>"
                  End If
                Next
              End If
              If strNTSButton.Contains(" " & strCtrl & " ") Then
                strBody &= "<p><b class=""nomecampo""><a name=""" & strCtrl & """>" & strText.Replace("&", "") & "</a></b></p>"
              End If
              If strNTSCheckBox.Contains(" " & strCtrl & " ") Then
                strBody &= "<p><b class=""nomecampo""><a name=""" & strCtrl & """>" & strText.Replace("&", "") & "</a></b></p>"
              End If
              If strNTSGroupBox.Contains(" " & strCtrl & " ") Then
                strBody &= "<p><b class=""nomecampo""><u><a name=""" & strCtrl & """>" & strText & "</a></u></b></p>"
              End If
              If strNTSLabel.Contains(" " & strCtrl & " ") Then
                strBody &= "<p><b class=""nomecampo""><a name=""" & strCtrl & """>" & strText & "</a></b></p>"
              End If
              If strNTSRadioButton.Contains(" " & strCtrl & " ") Then
                strBody &= "<p><b class=""nomecampo""><a name=""" & strCtrl & """>" & strText.Replace("&", "") & "</a></b></p>"
              End If
              If strNTSTabPage.Contains(" " & strCtrl & " ") Then
                strBody &= "<p><b class=""nomecampo""><u><i><a name=""" & strCtrl & """>" & strText.Replace("&", "") & "</a></i></u></b></p>"
              End If
            End If
          End If


        End If


      Loop
      srIn.Close()
      strTitolo = strTitolo.Substring(0, 1).ToUpper & strTitolo.Substring(1).ToLower
      Dim strBodyTesta As String = ""
      strBodyTesta = "      <!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01 Transitional//EN"" ""http://www.w3.org/TR/html4/loose.dtd"">" & vbCrLf
      strBodyTesta &= "<html>" & vbCrLf
      strBodyTesta &= "<head>" & vbCrLf
      strBodyTesta &= "  <meta http-equiv=""Content-Type"" content=""text/html; charset=iso-8859-1"">" & vbCrLf
      strBodyTesta &= "  <title>" & strTitolo & "</title>" & vbCrLf
      strBodyTesta &= "  <link rel=""stylesheet"" type=""text/css"" href=""style_topic.css"">" & vbCrLf
      strBodyTesta &= "</head>" & vbCrLf

      strBodyTesta &= "<BODY lang=it topMargin=3>" & vbCrLf
      strBodyTesta &= "<TABLE border=0 width=""100%"">" & vbCrLf
      strBodyTesta &= "  <TBODY>" & vbCrLf
      strBodyTesta &= "  <TR>" & vbCrLf
      strBodyTesta &= "    <TD vAlign=top align=right>" & vbCrLf
      strBodyTesta &= "      <P align=right><FONT size=2><CODE>Data ultimo aggiornamento: <A " & vbCrLf
      strBodyTesta &= "      name=hINIeDATlAGGp></A>" & Format(Date.Now, "dd/MM/yyyy") & "<A name=hFINeDATlAGGp></A> " & vbCrLf
      strBodyTesta &= "      </CODE></FONT></P></TD></TR>" & vbCrLf
      strBodyTesta &= "  <TR>" & vbCrLf
      strBodyTesta &= "    <TD vAlign=top align=right><A href=""http://www.ntsinformatica.it""><IMG " & vbCrLf
      strBodyTesta &= "      class=calone border=0 alt="""" src=""../images/help_ntslogo.gif""></A> " & vbCrLf
      strBodyTesta &= "  </TD></TR></TBODY></TABLE>" & vbCrLf

      strBodyTesta &= "  <h4>" & strTitolo & "</h4>" & vbCrLf

      strBodyTesta &= "  <p><b class=""nomecampo"">Nome programma: " & ("bn" & strPage.Substring(2, 6).Replace("--", "__")).ToUpper & ".DLL</b><br>" & vbCrLf
      strBodyTesta &= "<b class=""nomecampo"">Moduli richiesti: " & DecodStrModuli(strModuli) & "</b>" & vbCrLf

      If (strBn & strBe & strBd).ToUpper.Contains("""OPZIONI") Then
        strBodyTesta &= "<div class=""par"">Opzioni programma: <a href=""ampers07.htm#" & strPage.Substring(0, 8).ToUpper & """>vai alle opzioni</a></div>" & vbCrLf
      End If
      If (strBn & strBe & strBd).ToUpper.Contains("""REPORTS") Then
        strBodyTesta &= "<div class=""rep"">Report programma: <a href=""ampers08.htm#" & strPage.Substring(0, 8).ToUpper & """>vai ai report</a></div>" & vbCrLf
      End If

      strBodyTesta &= "  </p>" & vbCrLf

      strBodyTesta &= "  <p>...</p>" & vbCrLf

      strBodyTesta &= "  <p><img src=""../images/help_" & ("bn" & strPage.Substring(2, 6).Replace("--", "__")).ToLower & "_01.gif"" alt="""" class=""calone""></p>" & vbCrLf

      strBodyTesta &= strBody.Replace("</p>", "</p>" & vbCrLf)

      If Not strGridCollection Is Nothing Then
        For i As Integer = 0 To strGridCollection.Length - 1
          strBodyTesta &= strGridCollection(i) & vbCrLf
        Next
      End If

      If Not strToolbarCollection Is Nothing Then
        strBodyTesta &= "<p><b class=""nomecampo""><u><i><a name=""toolbar"">ToolBar</a></i></u></b></p>" & vbCrLf
        For i As Integer = 0 To strToolbarCollection.Length - 1
          strBodyTesta &= strToolbarCollection(i) & vbCrLf
        Next
      End If
      ''strBodyTesta &= "<p><b class=""nomecampo""><u><i><a name=""toolbar"">ToolBar</a></i></u></b></p>" & vbCrLf
      ''strBodyTesta &= "<ul>" & vbCrLf
      ''Dim nIDJump As Integer = 0
      ''Dim dtrBarOrd() As DataRow
      ''dtrBarOrd = dttNTSBarItem.Select("ID>0", "ID asc")
      ''For Each dtr As DataRow In dtrBarOrd
      ''  If dtr!type.ToString = "NTSBarSubItem" Then
      ''    strBodyTesta &= "<li><b class=""nomecampo""><u><a name=""" & dtr!cod.ToString & """>" & dtr!name.ToString & "</a></u></b> </li>" & vbCrLf
      ''    strBodyTesta &= "<ul>" & vbCrLf
      ''  Else
      ''    If nIDJump + 1 <> NTSCInt(dtr!ID) Then
      ''      strBodyTesta &= "</ul>" & vbCrLf
      ''    End If
      ''    strBodyTesta &= "<li><b class=""nomecampo""><a name=""" & dtr!cod.ToString & """>" & dtr!name.ToString & "</a></b> </li>"
      ''  End If
      ''  nIDJump = NTSCInt(dtr!ID)
      ''Next
      ''strBodyTesta &= "</ul>" & vbCrLf


      strBodyTesta &= "</body>" & vbCrLf
      strBodyTesta &= "</html>" & vbCrLf

      Dim w1 As StreamWriter = Nothing

      w1 = New StreamWriter(lbPage.Text, False, System.Text.Encoding.Default)
      w1.Write(strBodyTesta)
      w1.Close()
      WebBrows.Refresh()

      Return True
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Function DecodStrModuli(ByVal strIn As String) As String
    DecodStrModuli = strIn
    Dim strOut As String = ""
    strIn = strIn.Replace("CLN__STD.", "").Replace("+0", "").Replace("+", " ").Trim
    If strIn = "" Then strIn = "bsModAll"
    Try
      For Each strTmp As String In strIn.Split(" "c)
        Select Case strTmp.Trim
          Case "bsModCG" ' 1 ' contab. generale"
            strOut &= "Contab. generale e sempl., "
          Case "bsModOR" ' 2 ' ordini"
            strOut &= "Ordini, "
          Case "bsModVE" ' 4 ' vendite/fatturazione"
            strOut &= "Vendite/fatturazione, "
          Case "bsModPR" ' 8 ' provvigioni"
            strOut &= "Provvigioni, "
          Case "bsModST" ' 16 ' statistiche"
            strOut &= "Statistiche, "
          Case "bsModMG" ' 32 ' magazzino"
            strOut &= "Magazzino, "
          Case "bsModDB" ' 64 ' distinta base"
            strOut &= "Distinta base, "
          Case "bsModCI" ' 128 ' Contab. analitica"
            strOut &= "Contab. analitica, "
          Case "bsModCE" ' 256 ' cespiti"
            strOut &= "Cespiti, "
            'Case "bsModORCL" ' 512 ' Verticale 1   (ex Oracle Database)"
          Case "bsModGT" ' 1024 ' Source Extender"
            strOut &= "Source Extender, "
          Case "bsModCN" ' 2048 ' Conai"
            strOut &= "Conai, "
          Case "bsModAS" ' 4096 'Customer Service (ex assistenza software (ex ass tecnica))"
            strOut &= "Customer Service, "
          Case "bsModPI" ' 8192 ' Produzione"
            strOut &= "Produzione, "
          Case "bsModIE" ' 16384 ' Import/Export"
            strOut &= "Import/Export, "
          Case "bsModRA" ' 32768 ' richieste d'acquisto RDA-RDO"
            strOut &= "RDA-RDO, "
          Case "bsModTV" ' 65536 ' Tentata vendita"
            strOut &= "Tentata vendita, "
          Case "bsModPM" ' 131072 ' Project Management"
            strOut &= ",Project Management "
          Case "bsModIC" ' 262144 ' Intrastat"
            strOut &= "Intrastat, "
          Case "bsModCT" ' 524288 ' Genera PDF da documenti (ex Catalogo webness)"
            strOut &= "Generazione File pdf da Documenti, "
          Case "bsModPE" ' 1048576 ' Percipienti/Rit. acconto"
            strOut &= "Percipienti/Rit. acconto, "
            'Case "bsMod22" ' 2097152 ' a disposizione developers***"
            'Case "bsMod23" ' 4194304 ' a disposizione developers***"
          Case "bsModTE" ' 8388608 ' tesoreria"
            strOut &= "Tesoreria, "
            'Case "bsMod25" ' 16777216 ' a disposizione developers***"
          Case "bsModSM" ' 33554432 ' San Marino"
            strOut &= "San Marino, "
          Case "bsModCP" ' 67108864 ' Configuratore del prodotto"
            strOut &= "Configuratore del prodotto, "
          Case "bsModSQ" ' 134217728 ' sistema qualit ISO 9000"
            strOut &= "Sistema qualità, "
            'Case "bsMod29" ' 268435456 ' a disposizione developers***"
            'Case "bsMod30" ' 536870912 ' a disposizione developers***"
          Case "bsModAll" ' 1073741823 'tutti      (tutti 1 in binario 2^60)"
            strOut &= "Qualsiasi, "
            'Costanti dei moduli estesi
          Case "bsModExtCP2" ' 1 ' Config. su distinta neutra (CP2) (Ex B2B di webness)"
            strOut &= "Config. su distinta neutra, "
          Case "bsModExtRET" ' 2  ' Retail  (Ex E-SFA webness)"
            strOut &= "Retail, "
          Case "bsModExtTCO" ' 4  ' Taglie e colori (Ex E-Procurement (webness))"
            strOut &= "Taglie e colori, "
          Case "bsModExtNOT" ' 8 ' Bilancio UE e nota integrativa (notagen)"
            strOut &= "Bilancio UE e nota integrativa, "
          Case "bsModExtANB" ' 16 ' Analisi di bilancio (notagen)"
            strOut &= "Black list, "
          Case "bsModExtCRM" ' 32 ' CRM (ex Variaz. AA7/AA9)"
            strOut &= "CRM, "
          Case "bsModExtPIK" ' 64 ' Picking su Palmare(ex CCIAA)"
            strOut &= "Picking su Palmare, "
          Case "bsModExtPAR" ' 128 ' parcellazione"
            strOut &= "Parcellazione, "
          Case "bsModExtCON" ' 256 ' funzioni comuni Consultant (ed F24 GIC)"
            strOut &= "Telematico Oper. Rilevanti Iva, "
          Case "bsModExtANG" ' 512 ' anagrafiche generali"
            strOut &= "Anagrafiche generali, "
          Case "bsModExtCGE" ' 1024 ' Contab. gen. Easy"
            strOut &= "Contab. gen. Easy, "
          Case "bsModExtORE" ' 2048 ' ordini Easy"
            strOut &= "Ordini Easy, "
          Case "bsModExtMGE" ' 4096 ' magazzino Easy"
            strOut &= "Magazzino Easy, "
          Case "bsModExtCGS" ' 8192 ' contabilità professionisti"
            strOut &= "Contabilità professionisti, "
          Case "bsModExtF24" ' 16384 ' modulo F24 di Business"
            strOut &= "Modulo F24, "
          Case "bsModExtLEX" ' 32768 ' logistica extended"
            strOut &= "Logistica extended, "
            'Case "bsModExtVE2" ' 65536 ' Verticale 2 (ex comunicazione annuale Iva)"
          Case "bsModExtINT" ' 131072 ' International"
            strOut &= "Internazionale, "
            'Case "bsModExtNSS" ' 262144 ' NonSolo-Shop"
          Case "bsModExtSSE" ' 524288 ' commesse light"
            strOut &= "Commesse light, "
          Case "bsModExtEIV" ' 1048576 ' Elenchi Iva Cli-for"
            strOut &= "Elenchi Iva Cli-for, "
          Case "bsModExtTCP" ' 2097152 ' Produzione Taglie colori"
            strOut &= "Produzione Taglie e colori, "
            'Case "bsModExtCAS" ' 4194304 '       (ex 730)"
            'Case "bsModExt740" ' 8388608 '        ( ex 740,ICI +IRAP)"
          Case "bsModExtBNF" ' 16777216 ' BUSNET Frame       ( ex 750,ICI +IRAP)"
            strOut &= "BUSNET Frame, "
            'Case "bsModExtXNT" ' 33554432 ' Moduli Supplementari NTS        ( ex 760,ICI +IRAP)"
          Case "bsModExtDWH" ' 67108864 ' Data-Warehouse (ex Mod. RED e ISEE (era a a disposizione...prima))"
            strOut &= "Datawarehouse, "
            'Case "bsModExt28" ' 134217728 ' a disposizione"
            'Case "bsModExt29" ' 268435456 ' a disposizione"
            'Case "bsModExt30" ' 536870912 ' Moduli Supplementari Partner  (ex a disposizione)"
          Case "bsModExtAll" ' 1073741823 'tutti      (tutti 1 in binario 2^60)"
            'Costanti dei moduli suplementari (da 101 a 130)
          Case "bsModSupIPL" ' 1 ' inventari su palmare (101)"
            strOut &= "Inventari su palmare, "
          Case "bsModSupRPL" ' 2  ' Controllo ric. merce su palmare (102)"
            strOut &= "Controllo ric. merce su palmare, "
          Case "bsModSupRME" ' 4  ' Soluzione palmare in terminal server"
            strOut &= "Logistica di Magazzino su Palmare, "
          Case "bsModSupWCR" ' 8 ' Web CRM"
            strOut &= "Web CRM, "
            'Case "bsModSupWSS" ' 16 ' Web Soft Service"
          Case "bsModSupEMA" ' 32 ' E-mail integrata  "
            strOut &= "E-mail integrata, "
          Case "bsModSupDWE" ' 64 ' Data-Warehouse"
            strOut &= "Datawarehouse extended, "
          Case "bsModSupCAE" ' 128 ' Contabilità Analitica Estesa"
            strOut &= "Contab. analitica DC, "
          Case "bsModSupBUD" ' 256 ' Budgeting e Controllo di Gestione"
            strOut &= "Budgeting e Controllo di Gestione, "
          Case "bsModSupDKO" ' 512 ' Desktop Consolle"
            strOut &= "Desktop Consolle, "
          Case "bsModSupTEN" ' 1024 'Tesoreria e flussi finanziari"
            strOut &= "Tesoreria e flussi finanziari, "
          Case "bsModSupTEX" ' 2048 'Tesoreria Extended"
            strOut &= "Tesoreria Extended, "
          Case "bsModSupGPV" ' 4096 'Gestione punti vendita (Nuovo retail)"
            strOut &= "Gestione punti vendita, "
          Case "bsModSupGPE"
            strOut &= "Gestione punti vendita extended, "
          Case Else
            strOut &= strTmp.Trim & ", "
            'Case "bsModSupAll" ' 1073741823 '"
            'Case "bsModSupExtAll" ' 1073741823 '"
        End Select

      Next

      Return strOut.Substring(0, strOut.Length - 2)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function

  Public Overridable Sub cmdControlli_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdControlli.Click
    Dim dttHtml As New DataTable
    Try
      dttHtml.Clear()
      dttHtml.TableName = "PAGINE"
      dttHtml.Columns.Clear()
      dttHtml.Columns.Add("name", GetType(String))
      dttHtml.Columns.Add("isInHhc", GetType(String))
      dttHtml.Columns("isInHhc").DefaultValue = "S"
      dttHtml.Columns.Add("tipoGAoAM", GetType(String))
      dttHtml.Columns.Add("title", GetType(String))
      dttHtml.Columns.Add("page", GetType(String))

      Dim strHhc As String = New StreamReader(strMainDir & "sommario.hhc", System.Text.Encoding.Default).ReadToEnd

      For Each strFile As String In System.IO.Directory.GetFiles(strMainDir & "html", "*.htm")
        Dim fileInfo As New FileInfo(strFile)
        Dim r1 As StreamReader = New StreamReader(strFile, System.Text.Encoding.Default)
        Dim strTxt As String = r1.ReadToEnd
        r1.Close()

        '  If Not (strTxt.ToLower.Contains("<p>In questa sezione (Argomenti e Moduli):</p>".ToLower) Or strTxt.ToLower.Contains("<p>In questa sezione (Guida Analitica):</p>".ToLower)) Then
        'ricavo il titolo
        Dim i As Integer = strTxt.IndexOf("<title>", StringComparison.OrdinalIgnoreCase) + 7
        Dim n As Integer = strTxt.IndexOf("</title>", i, StringComparison.OrdinalIgnoreCase) - i
        Dim strTitle As String = strTxt.Substring(i, n)
        If strHhc.ToLower.Contains("html\" & fileInfo.Name.ToLower) Then
          If strHhc.ToLower.IndexOf("html\" & fileInfo.Name.ToLower) > strHhc.ToLower.IndexOf("value=""Argomenti e Moduli"">".ToLower) And _
             strHhc.ToLower.IndexOf("html\" & fileInfo.Name.ToLower) < strHhc.ToLower.IndexOf("value=""Guida Analitica"">".ToLower) Then
            dttHtml.Rows.Add(strFile, "S", "AM:", strTitle, strTxt)

          Else
            If strHhc.ToLower.IndexOf("html\" & fileInfo.Name.ToLower) > strHhc.ToLower.IndexOf("value=""Guida Analitica"">".ToLower) And _
               strHhc.ToLower.IndexOf("html\" & fileInfo.Name.ToLower) < strHhc.ToLower.IndexOf("value=""Note di versione"">".ToLower) Then
              dttHtml.Rows.Add(strFile, "S", "GA:", strTitle, strTxt)

            Else
              dttHtml.Rows.Add(strFile, "S", "", strTitle, strTxt)

            End If
          End If
        Else
          dttHtml.Rows.Add(strFile, "N", "", strTitle, strTxt)

        End If


      Next
      dttHtml.AcceptChanges()
      If oApp.MsgBoxInfoYesNo_DefNo("Controllare se ci sono pagine senza?") = Windows.Forms.DialogResult.Yes Then
        Dim strPagine As String = ""
        Dim strSenza As String = oApp.InputBoxNew("Digitare cosa ricercare:", "")
        If strSenza <> "" Then
          For cont As Integer = 0 To dttHtml.Rows.Count - 1
            With dttHtml.Rows(cont)
              If !page.ToString.ToLower.Contains(strSenza.ToLower) = False Then
                strPagine &= !name.ToString & vbCrLf
              End If
            End With
          Next
          If strPagine <> "" Then
            Dim w1 As StreamWriter = Nothing
            w1 = New StreamWriter(oApp.AscDir & "\h4.log", False, System.Text.Encoding.Default)
            w1.Write("Pagine senza " & strSenza & ":" & vbCrLf & strPagine)
            w1.Close()
            w1.Dispose()
            NTSProcessStart(oApp.AscDir & "\h4.log", "")
          End If
        End If
      End If
      If oApp.MsgBoxInfoYesNo_DefNo("Controllare se ci sono pagine senza <A name=hINIeDATlAGGp></A>?") = Windows.Forms.DialogResult.Yes Then
        Dim strPagine As String = ""
        For cont As Integer = 0 To dttHtml.Rows.Count - 1
          With dttHtml.Rows(cont)
            If !page.ToString.ToLower.Contains("<A name=hINIeDATlAGGp></A>".ToLower) = False Then
              strPagine &= !name.ToString & vbCrLf
              'NTSProcessStart("C:\Program Files\Notepad++\notepad++.exe", !name.ToString)
            End If
          End With
        Next
        If strPagine <> "" Then
          Dim w1 As StreamWriter = Nothing
          w1 = New StreamWriter(oApp.AscDir & "\hINIeDATlAGGp.log", False, System.Text.Encoding.Default)
          w1.Write("<A name=hINIeDATlAGGp></A>:" & vbCrLf & strPagine)
          w1.Close()
          w1.Dispose()
          NTSProcessStart(oApp.AscDir & "\hINIeDATlAGGp.log", "")
        End If
      End If
      If oApp.MsgBoxInfoYesNo_DefNo("Controllare se ci sono pagine senza <A name=hFINeDATlAGGp></A>?") = Windows.Forms.DialogResult.Yes Then
        Dim strPagine As String = ""
        For cont As Integer = 0 To dttHtml.Rows.Count - 1
          With dttHtml.Rows(cont)
            If !page.ToString.ToLower.Contains("<A name=hFINeDATlAGGp></A>".ToLower) = False Then
              strPagine &= !name.ToString & vbCrLf
            End If
          End With
        Next
        If strPagine <> "" Then
          Dim w1 As StreamWriter = Nothing
          w1 = New StreamWriter(oApp.AscDir & "\hFINeDATlAGGp.log", False, System.Text.Encoding.Default)
          w1.Write("Pagine senza <A name=hFINeDATlAGGp></A>:" & vbCrLf & strPagine)
          w1.Close()
          w1.Dispose()
          NTSProcessStart(oApp.AscDir & "\hFINeDATlAGGp.log", "")
        End If
      End If

      If oApp.MsgBoxInfoYesNo_DefNo("Controllare se html non inseriti nel sommario.hhc?") = Windows.Forms.DialogResult.Yes Then
        Dim strPagine As String = ""
        For cont As Integer = 0 To dttHtml.Rows.Count - 1
          With dttHtml.Rows(cont)
            Dim fileInfo As New FileInfo(!name.ToString)
            If Not strHhc.ToLower.Contains("html\" & fileInfo.Name.ToLower) Then
              strPagine &= !name.ToString & vbCrLf
            End If
          End With
        Next
        If strPagine <> "" Then
          Dim w1 As StreamWriter = Nothing
          w1 = New StreamWriter(oApp.AscDir & "\h4.log", False, System.Text.Encoding.Default)
          w1.Write("Pagine html non inserite nel sommario.hhc:" & vbCrLf & strPagine)
          w1.Close()
          w1.Dispose()
          NTSProcessStart(oApp.AscDir & "\h4.log", "")
        End If
      End If

      If oApp.MsgBoxInfoYesNo_DefNo("Trasferire i <h4> in <title>?") = Windows.Forms.DialogResult.Yes Then
        For cont As Integer = 0 To dttHtml.Rows.Count - 1
          With dttHtml.Rows(cont)
            If !tipoGAoAM.ToString <> "" Then
              Dim fileInfo As New FileInfo(!name.ToString)
              If Not fileInfo.IsReadOnly Then

                Dim strR1 As New StreamReader(!name.ToString, System.Text.Encoding.Default)
                Dim strTxt As String = strR1.ReadToEnd
                strR1.Close()
                Dim i As Integer = strTxt.IndexOf("<title>", StringComparison.OrdinalIgnoreCase)
                Dim n As Integer = strTxt.IndexOf("</title>", i, StringComparison.OrdinalIgnoreCase) - i
                Dim strTitle As String = strTxt.Substring(i, n)

                Dim a As Integer = strTxt.IndexOf("<h4>", StringComparison.OrdinalIgnoreCase) + 4
                Dim b As Integer = strTxt.IndexOf("</h4>", a, StringComparison.OrdinalIgnoreCase) - a
                Dim strh4 As String = strTxt.Substring(a, b).Trim

                Dim w1 As StreamWriter = Nothing
                w1 = New StreamWriter(!name.ToString, False, System.Text.Encoding.Default)
                strTxt = strTxt.Replace(strTitle, ("<title> " & !tipoGAoAM.ToString & " " & strh4).Replace(vbCrLf, ""))
                w1.Write(strTxt)
                w1.Close()
                w1.Dispose()

              End If
            End If
          End With
        Next
      End If
      If oApp.MsgBoxInfoYesNo_DefNo("Verificare se ci sono immagini non contenute in HTM?") = Windows.Forms.DialogResult.Yes Then
        Dim strIMG As String = ""
        dttHtml.WriteXml(oApp.AscDir & "\gifnotused.xml")
        Dim strHTML As String = New StreamReader(oApp.AscDir & "\gifnotused.xml", System.Text.Encoding.Default).ReadToEnd
        strHTML = strHTML.ToLower
        For Each strFile As String In System.IO.Directory.GetFiles(strMainDir & "images", "*.gif")
          Dim fileInfo As New FileInfo(strFile)
          If Not strHTML.Contains(fileInfo.Name.ToLower) Then
            strIMG &= strFile & vbCrLf
          End If
        Next
        For Each strFile As String In System.IO.Directory.GetFiles(strMainDir & "images", "*.jpg")
          Dim fileInfo As New FileInfo(strFile)
          If Not strHTML.Contains(fileInfo.Name.ToLower) Then
            strIMG &= strFile & vbCrLf
          End If
        Next
        If strIMG <> "" Then
          Dim w1 As StreamWriter = Nothing
          w1 = New StreamWriter(oApp.AscDir & "\gifnotused.log", False, System.Text.Encoding.Default)
          w1.Write("immagini non contenute in HTM:" & vbCrLf & strIMG)
          w1.Close()
          w1.Dispose()
          NTSProcessStart(oApp.AscDir & "\gifnotused.log", "")
        End If

      End If

      oApp.MsgBoxInfo("Controlli completati!")
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

#Region "Eventi autocomposizione"
  Public Overridable Sub edTrova_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles edTrova.KeyUp
    Try

      If e.KeyCode = Keys.Enter Then
        '  lsbProposta.Visible = False
        '  Me.NTSKeyDownEnterNotEmulateTabNow()
      Else
        MostraAutoComposizione(edTrova.Text)

        If e.KeyCode = Keys.Up Then
          e.Handled = True
          lsbProposta.Focus()
          lsbProposta.SelectionMode = SelectionMode.None
          lsbProposta.SelectionMode = SelectionMode.One
          lsbProposta.SelectedIndex = lsbProposta.ItemCount - 1
          lsbProposta.SelectedIndex = lsbProposta.ItemCount - 2
          lsbProposta.SelectedIndex = lsbProposta.ItemCount - 1
          lsbProposta.Focus()
          Return
        End If
        If e.KeyCode = Keys.Down Then
          e.Handled = True
          lsbProposta.Focus()
          'lsbProposta.SelectionMode = SelectionMode.None
          'lsbProposta.SelectionMode = SelectionMode.One
          'lsbProposta.SelectedIndex = 0
          'lsbProposta.SelectedIndex = 1
          'lsbProposta.SelectedIndex = 0
          lsbProposta.SetSelected(0, False)
          lsbProposta.SetSelected(0, True)

          lsbProposta.Focus()
          Return
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub edTrova_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles edTrova.Leave
    Try
      If Not lsbProposta.Focused Then lsbProposta.Visible = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub lsbProposta_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lsbProposta.Leave
    Try
      lsbProposta.Visible = False
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub lsbProposta_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lsbProposta.KeyDown
    Try
      Select Case e.KeyCode
        Case Keys.Enter

          edTrova.Text = lsbProposta.SelectedValue.ToString
          edTrova.Focus()
          FRM__HELP_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

          'edTrova_KeyUp(Me, New KeyEventArgs(Keys.Enter))
        Case Keys.Back
          edTrova.Focus()

      End Select
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub lsbProposta_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lsbProposta.DoubleClick
    Try
      edTrova.Text = lsbProposta.SelectedValue.ToString
      edTrova.Focus()
      FRM__HELP_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))

      'edTrova_KeyUp(Me, New KeyEventArgs(Keys.Enter))
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
#End Region

  Public Overridable Sub MostraAutoComposizione(ByVal strTesto As String)

    Try

      lsbProposta.Visible = False

      If Not edTrova.Text.Length > 0 Then Return

      'Inserisco gli elementi alla lista.
      lsbProposta.Items.Clear()
      Dim dttOut As New DataTable
      CType(CType(oMenu.oCleComm, CLELBMENU).oCldComm.oLbBase, CLDLBBASE).GetSettingBusDitt(oApp.Ditta, "BS--HELP", "RECENT", oApp.User.Nome.ToLower, " ", dttOut)

      For z As Integer = 0 To dttOut.Rows.Count - 1
        If NTSCStr(dttOut.Rows(z)!rp_nomprop).StartsWith(edTrova.Text.ToLower) Or edTrova.Text.ToLower = "" Then
          lsbProposta.Items.Add(NTSCStr(dttOut.Rows(z)!rp_nomprop))
        End If
      Next

      'Lo devo posizionare dinamicamente in base alla posizione nel testo e al campo usato
      lsbProposta.Left = edTrova.Left + 2
      lsbProposta.Top = edTrova.Top + edTrova.Height - 1
      lsbProposta.Width = pnCerca.Width - (lsbProposta.Left)
      If lsbProposta.Items.Count > 5 Then
        lsbProposta.Height = 5 * 15 + 1
      Else
        lsbProposta.Height = lsbProposta.Items.Count * 15 + 1
      End If

      If lsbProposta.ItemCount > 0 Then lsbProposta.Visible = True

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub AutoRecents()
    Dim strCerca As String = ""
    Dim strTime As String = ""
    Dim dateNow As Date = Now
    Try
      strCerca = edTrova.Text.Trim
      strTime = dateNow.Year.ToString & dateNow.Month.ToString & dateNow.Day.ToString & dateNow.Hour.ToString & dateNow.Minute.ToString & dateNow.Second.ToString & Format(dateNow.Millisecond, "000")
      strCerca = CStrSQL(strCerca)
      strCerca = strCerca.Substring(1, strCerca.Length - 2)
      oMenu.SaveSettingBus("BS--HELP", "RECENT", oApp.User.Nome.ToLower, strCerca, strTime, " ", "...", "...", "...")

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub



  Public dttCronologia As DataTable
  Public Overridable Function LeggiCronologia() As Boolean
    Try
      dttCronologia = New DataTable
      dttCronologia.TableName = "CRONOLOGIA"
      dttCronologia.Columns.Add("time", GetType(String))
      dttCronologia.Columns.Add("title", GetType(String))
      dttCronologia.Columns.Add("url", GetType(String))
      Dim strCrono As String = oMenu.GetSettingBus("BS--HELP", "RECENT", "CRONOLOGIA", oApp.User.Nome, "", " ", "")
      If strCrono.Trim <> "" Then
        If strCrono.Contains("§"c) Then
          For Each strTmpCro As String In strCrono.Split("§"c)
            dttCronologia.Rows.Add(New Object() {strTmpCro.Substring(0, 17), strTmpCro.Substring(17, 50), strTmpCro.Substring(67, strTmpCro.Length - 67)})
          Next
        Else
          dttCronologia.Rows.Add(New Object() {strCrono.Substring(0, 17), strCrono.Substring(17, 50), strCrono.Substring(67, strCrono.Length - 67)})
        End If
      End If
      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Sub InserisciCronologia()
    Dim strCronologia As String = ""
    Dim strTitle As String = ""
    Dim strTime As String = ""
    Dim dateNow As Date = Now
    Try
      If WebBrows.Document Is Nothing Then Return
      strCronologia = WebBrows.Document.Url.AbsoluteUri
      strTitle = WebBrows.Document.Title.PadRight(50, " "c)
      If strTitle.Trim = "" Then
        If WebBrows.Document.Url.AbsoluteUri.Length > 50 Then
          strTitle = WebBrows.Document.Url.AbsoluteUri.Substring(0, 50)
        Else
          strTitle = WebBrows.Document.Url.AbsoluteUri.PadRight(50, " "c)
        End If
      End If
      strTime = Format(dateNow.Year, "0000") & Format(dateNow.Month, "00") & _
                Format(dateNow.Day, "00") & Format(dateNow.Hour, "00") & _
                Format(dateNow.Minute, "00") & Format(dateNow.Second, "00") & _
                Format(dateNow.Millisecond, "000")
      If dttCronologia.Select("url=" & CStrSQL(strCronologia)).Length > 0 Then
        dttCronologia.Select("url=" & CStrSQL(strCronologia))(0)!time = strTime
        dttCronologia.Select("url=" & CStrSQL(strCronologia))(0)!title = strTitle
      Else
        dttCronologia.Rows.Add(New Object() {strTime, strTitle, strCronologia})
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub SaveCronologia()
    Dim strCronologia As String = ""
    Try

      If dttCronologia.Rows.Count > 0 Then

        For Each dtrCrono As DataRow In dttCronologia.Select("", "time desc")
          With dtrCrono
            If strCronologia = "" Then
              strCronologia = !time.ToString & !title.ToString & !url.ToString
            Else
              strCronologia &= "§" & !time.ToString & !title.ToString & !url.ToString
            End If
          End With
        Next


      End If
      oMenu.SaveSettingBus("BS--HELP", "RECENT", "CRONOLOGIA", oApp.User.Nome, strCronologia, " ", "...", "...", "...")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub tsHelp_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsHelp.SelectedPageChanged
    Dim oNode As TreeNode = Nothing
    Dim oCurrNode As TreeNode = Nothing
    Dim oLastNodeAdd As TreeNode = Nothing
    Dim dateNow As Date = Now
    Dim dateElab As Date = Now
    Dim strTimeOggi As String = ""
    Dim strTimeIeri As String = ""
    Dim strTime7Giorni As String = ""
    Dim strTime30Giorni As String = ""
    Dim strTimeMenoRecente As String = ""
    Dim bTimeOggi As Boolean = False
    Dim bTimeIeri As Boolean = False
    Dim bTime7Giorni As Boolean = False
    Dim bTime30Giorni As Boolean = False
    Dim bTimeMenoRecente As Boolean = False
    Dim strChange As String = ""
    Try
      trCronologia.Nodes.Clear()
      If e Is Nothing Then Return
      If e.Page.Name = TabPage1.Name Then

        'cronologia
        strTimeOggi = Format(dateNow.Year, "0000") & Format(dateNow.Month, "00") & Format(dateNow.Day, "00")
        dateElab = dateNow.AddDays(-1)
        strTimeIeri = Format(dateElab.Year, "0000") & Format(dateElab.Month, "00") & Format(dateElab.Day, "00")
        dateElab = dateNow.AddDays(-2)
        strTime7Giorni = Format(dateElab.Year, "0000") & Format(dateElab.Month, "00") & Format(dateElab.Day, "00")
        dateElab = dateNow.AddDays(-8)
        strTime30Giorni = Format(dateElab.Year, "0000") & Format(dateElab.Month, "00") & Format(dateElab.Day, "00")
        dateElab = dateNow.AddMonths(-30)
        strTimeMenoRecente = Format(dateElab.Year, "0000") & Format(dateElab.Month, "00") & Format(dateElab.Day, "00")


        If dttCronologia.Rows.Count > 0 Then

          For Each dtrCrono As DataRow In dttCronologia.Select("", "time desc")

            If strTimeOggi <= dtrCrono!time.ToString.Substring(0, 8) Then
              If bTimeOggi = False Then
                oCurrNode = New TreeNode
                oCurrNode.ImageIndex = 1
                oCurrNode.SelectedImageIndex = 1

                oCurrNode.Text = "Oggi"
                oCurrNode.Tag = "dir"
                oCurrNode.Name = "Oggi"

                trCronologia.Nodes.Add(oCurrNode)
                bTimeOggi = True
              End If
              oNode = New TreeNode
              oNode.ImageIndex = 0
              oNode.SelectedImageIndex = 0

              oNode.Text = dtrCrono!time.ToString.Substring(8, 2) & ":" & dtrCrono!time.ToString.Substring(10, 2) & " " & dtrCrono!title.ToString.Trim
              oNode.Tag = "page"
              oNode.Name = dtrCrono!url.ToString

              oCurrNode.Nodes.Add(oNode)
            Else
              If strTimeIeri = dtrCrono!time.ToString.Substring(0, 8) Then
                If bTimeIeri = False Then
                  oCurrNode = New TreeNode
                  oCurrNode.ImageIndex = 1
                  oCurrNode.SelectedImageIndex = 1

                  oCurrNode.Text = "Ieri"
                  oCurrNode.Tag = "dir"
                  oCurrNode.Name = "Ieri"

                  trCronologia.Nodes.Add(oCurrNode)
                  bTimeIeri = True
                End If
                oNode = New TreeNode
                oNode.ImageIndex = 0
                oNode.SelectedImageIndex = 0

                If strChange <> dtrCrono!time.ToString.Substring(0, 8) Then
                  oNode.Text = dtrCrono!time.ToString.Substring(6, 2) & "/" & dtrCrono!time.ToString.Substring(4, 2) & "/" & dtrCrono!time.ToString.Substring(0, 4) & " " & _
                               dtrCrono!time.ToString.Substring(8, 2) & ":" & dtrCrono!time.ToString.Substring(10, 2) & " " & dtrCrono!title.ToString.Trim
                  strChange = dtrCrono!time.ToString.Substring(0, 8)
                Else
                  oNode.Text = dtrCrono!time.ToString.Substring(8, 2) & ":" & dtrCrono!time.ToString.Substring(10, 2) & " " & dtrCrono!title.ToString.Trim
                End If
                oNode.Tag = "page"
                oNode.Name = dtrCrono!url.ToString

                oCurrNode.Nodes.Add(oNode)
              Else
                If NTSCLng(strTime7Giorni) >= NTSCLng(dtrCrono!time.ToString.Substring(0, 8)) And NTSCLng(strTime30Giorni) < NTSCLng(dtrCrono!time.ToString.Substring(0, 8)) Then

                  If bTime7Giorni = False Then
                    oCurrNode = New TreeNode
                    oCurrNode.ImageIndex = 1
                    oCurrNode.SelectedImageIndex = 1

                    oCurrNode.Text = "Ultimi 7 giorni"
                    oCurrNode.Tag = "dir"
                    oCurrNode.Name = "Ultimi 7 giorni"

                    trCronologia.Nodes.Add(oCurrNode)
                    bTime7Giorni = True
                  End If
                  oNode = New TreeNode
                  oNode.ImageIndex = 0
                  oNode.SelectedImageIndex = 0

                  If strChange <> dtrCrono!time.ToString.Substring(0, 8) Then
                    oNode.Text = dtrCrono!time.ToString.Substring(6, 2) & "/" & dtrCrono!time.ToString.Substring(4, 2) & "/" & dtrCrono!time.ToString.Substring(0, 4) & " " & _
                                 dtrCrono!time.ToString.Substring(8, 2) & ":" & dtrCrono!time.ToString.Substring(10, 2) & " " & dtrCrono!title.ToString.Trim
                    strChange = dtrCrono!time.ToString.Substring(0, 8)
                  Else
                    oNode.Text = dtrCrono!time.ToString.Substring(8, 2) & ":" & dtrCrono!time.ToString.Substring(10, 2) & " " & dtrCrono!title.ToString.Trim
                  End If
                  oNode.Tag = "page"
                  oNode.Name = dtrCrono!url.ToString

                  oCurrNode.Nodes.Add(oNode)

                Else
                  If NTSCLng(strTime30Giorni) >= NTSCLng(dtrCrono!time.ToString.Substring(0, 8)) And NTSCLng(strTimeMenoRecente) < NTSCLng(dtrCrono!time.ToString.Substring(0, 8)) Then


                    If bTime30Giorni = False Then
                      oCurrNode = New TreeNode
                      oCurrNode.ImageIndex = 1
                      oCurrNode.SelectedImageIndex = 1

                      oCurrNode.Text = "Ultimi 30 giorni"
                      oCurrNode.Tag = "dir"
                      oCurrNode.Name = "Ultimi 30 giorni"

                      trCronologia.Nodes.Add(oCurrNode)
                      bTime30Giorni = True
                    End If
                    oNode = New TreeNode
                    oNode.ImageIndex = 0
                    oNode.SelectedImageIndex = 0

                    If strChange <> dtrCrono!time.ToString.Substring(0, 8) Then
                      oNode.Text = dtrCrono!time.ToString.Substring(6, 2) & "/" & dtrCrono!time.ToString.Substring(4, 2) & "/" & dtrCrono!time.ToString.Substring(0, 4) & " " & _
                                   dtrCrono!time.ToString.Substring(8, 2) & ":" & dtrCrono!time.ToString.Substring(10, 2) & " " & dtrCrono!title.ToString.Trim
                      strChange = dtrCrono!time.ToString.Substring(0, 8)
                    Else
                      oNode.Text = dtrCrono!time.ToString.Substring(8, 2) & ":" & dtrCrono!time.ToString.Substring(10, 2) & " " & dtrCrono!title.ToString.Trim
                    End If
                    oNode.Tag = "page"
                    oNode.Name = dtrCrono!url.ToString

                    oCurrNode.Nodes.Add(oNode)

                  Else


                    If bTimeMenoRecente = False Then
                      oCurrNode = New TreeNode
                      oCurrNode.ImageIndex = 1
                      oCurrNode.SelectedImageIndex = 1

                      oCurrNode.Text = "Meno recente"
                      oCurrNode.Tag = "dir"
                      oCurrNode.Name = "Meno recente"

                      trCronologia.Nodes.Add(oCurrNode)
                      bTimeMenoRecente = True
                    End If
                    oNode = New TreeNode
                    oNode.ImageIndex = 0
                    oNode.SelectedImageIndex = 0

                    If strChange <> dtrCrono!time.ToString.Substring(0, 8) Then
                      oNode.Text = dtrCrono!time.ToString.Substring(6, 2) & "/" & dtrCrono!time.ToString.Substring(4, 2) & "/" & dtrCrono!time.ToString.Substring(0, 4) & " " & _
                                   dtrCrono!time.ToString.Substring(8, 2) & ":" & dtrCrono!time.ToString.Substring(10, 2) & " " & dtrCrono!title.ToString.Trim
                      strChange = dtrCrono!time.ToString.Substring(0, 8)
                    Else
                      oNode.Text = dtrCrono!time.ToString.Substring(8, 2) & ":" & dtrCrono!time.ToString.Substring(10, 2) & " " & dtrCrono!title.ToString.Trim
                    End If
                    oNode.Tag = "page"
                    oNode.Name = dtrCrono!url.ToString

                    oCurrNode.Nodes.Add(oNode)

                  End If
                End If


              End If

            End If



          Next
        End If


      End If
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub cmdComprimiCronologia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdComprimiCronologia.Click
    Try
      trCronologia.CollapseAll()
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public Overridable Sub cmdCancellaCronologia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancellaCronologia.Click
    Try
      dttCronologia.Clear()
      dttCronologia.AcceptChanges()
      tsHelp_SelectedPageChanged(Nothing, Nothing)
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public strStampa As System.Text.StringBuilder
  Public Overridable Function CreaStampa(ByRef nodeTree As TreeNode) As Boolean
    Dim r1 As StreamReader = Nothing
    Dim w1 As StreamWriter = Nothing
    Try
      strStampa = New System.Text.StringBuilder
      r1 = New StreamReader(strMainDir & nodeTree.Tag.ToString, System.Text.Encoding.Default)
      strStampa.Append(r1.ReadToEnd.Replace("<TABLE border=0 cellSpacing=0 cellPadding=0><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/software-retail-negozi/""><IMG border=0 src=""../images/help_home_01_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/controllo-di-gestione/""><IMG border=0 src=""../images/help_home_02_Controlla.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/crm/""><IMG border=0 src=""../images/help_home_03_Fidelizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/analisi-dati-datawarehouse/""><IMG border=0 src=""../images/help_home_04_DWH.gif""></A></TD><TD vAlign=center width=169 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/project-management/""><IMG border=0 src=""../images/help_home_05_Gestisci.gif""></A></TD><TD vAlign=center width=146 align=right><IMG border=0 src=""../images/help_home_NTS_logo_up.gif""></TD></TR><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/logistica/""><IMG border=0 src=""../images/help_home_06_Organizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/customer-service/""><IMG border=0 src=""../images/help_home_07_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/produzione/""><IMG border=0 src=""../images/help_home_08_Pianifica.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/archiviazione-documenti/""><IMG border=0 src=""../images/help_home_09_Archivia.gif""></A></TD><TD vAlign=center width=169 align=right><IMG border=0 src=""../images/help_home_NTS_logo_down1.gif""></TD><TD vAlign=center width=146 align=right><A href=""http://www.ntsinformatica.it""><IMG border=0 alt=""Clicca per accedere al sito di NTS Informatica"" src=""../images/help_home_NTS_logo_down2.gif""></A></TD></TR><TR><TD width=987 colSpan=6><IMG border=0 src=""../images/help_home_Line.gif""></TD></TR></TABLE>", "").Replace("</body></html>", ""))
      r1.Close()

      For Each nodeTmp As TreeNode In nodeTree.Nodes
        If Not CreaStampaNode(nodeTmp) Then Return False
      Next

      strStampa.Append("<TABLE border=0 cellSpacing=0 cellPadding=0><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/software-retail-negozi/""><IMG border=0 src=""../images/help_home_01_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/controllo-di-gestione/""><IMG border=0 src=""../images/help_home_02_Controlla.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/crm/""><IMG border=0 src=""../images/help_home_03_Fidelizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/analisi-dati-datawarehouse/""><IMG border=0 src=""../images/help_home_04_DWH.gif""></A></TD><TD vAlign=center width=169 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/project-management/""><IMG border=0 src=""../images/help_home_05_Gestisci.gif""></A></TD><TD vAlign=center width=146 align=right><IMG border=0 src=""../images/help_home_NTS_logo_up.gif""></TD></TR><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/logistica/""><IMG border=0 src=""../images/help_home_06_Organizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/customer-service/""><IMG border=0 src=""../images/help_home_07_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/produzione/""><IMG border=0 src=""../images/help_home_08_Pianifica.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/archiviazione-documenti/""><IMG border=0 src=""../images/help_home_09_Archivia.gif""></A></TD><TD vAlign=center width=169 align=right><IMG border=0 src=""../images/help_home_NTS_logo_down1.gif""></TD><TD vAlign=center width=146 align=right><A href=""http://www.ntsinformatica.it""><IMG border=0 alt=""Clicca per accedere al sito di NTS Informatica"" src=""../images/help_home_NTS_logo_down2.gif""></A></TD></TR><TR><TD width=987 colSpan=6><IMG border=0 src=""../images/help_home_Line.gif""></TD></TR></TABLE></body></html>")

      Dim fileHTM As New FileInfo(oApp.Dir & "\stampa_" & oApp.User.Nome & ".htm")
      If fileHTM.Exists Then
        fileHTM.IsReadOnly = False
        fileHTM.Delete()
      End If
      Dim fileCSS As New FileInfo(oApp.Dir & "\style_topic.css")
      If fileCSS.Exists Then
        fileCSS.IsReadOnly = False
      End If
      fileCSS = New FileInfo(strHelpDirectoryLocal & "\Help\html\style_topic.css")
      If fileCSS.Exists Then
        fileCSS.CopyTo(oApp.Dir & "\style_topic.css", True)
      End If

      w1 = New StreamWriter(oApp.Dir & "\stampa_" & oApp.User.Nome & ".htm", False, System.Text.Encoding.Default)

      w1.Write(strStampa.Replace("../images/help_", "file:///" & strHelpDirectoryLocal.Replace("\", "/") & "/Help/" & "images/help_"))
      w1.Close()
      Return True
    Catch ex As Exception
      Try
        r1.Close()
      Catch ex2 As Exception
      End Try
      Try
        w1.Close()
      Catch ex2 As Exception
      End Try
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Function CreaStampaNode(ByRef nodeTree As TreeNode) As Boolean
    Dim r1 As StreamReader = Nothing
    Dim strStampaTmp As String = ""
    Try
      r1 = New StreamReader(strMainDir & nodeTree.Tag.ToString, System.Text.Encoding.Default)
      strStampaTmp = r1.ReadToEnd.Replace("<TABLE border=0 cellSpacing=0 cellPadding=0><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/software-retail-negozi/""><IMG border=0 src=""../images/help_home_01_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/controllo-di-gestione/""><IMG border=0 src=""../images/help_home_02_Controlla.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/crm/""><IMG border=0 src=""../images/help_home_03_Fidelizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/analisi-dati-datawarehouse/""><IMG border=0 src=""../images/help_home_04_DWH.gif""></A></TD><TD vAlign=center width=169 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/project-management/""><IMG border=0 src=""../images/help_home_05_Gestisci.gif""></A></TD><TD vAlign=center width=146 align=right><IMG border=0 src=""../images/help_home_NTS_logo_up.gif""></TD></TR><TR><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/logistica/""><IMG border=0 src=""../images/help_home_06_Organizza.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/customer-service/""><IMG border=0 src=""../images/help_home_07_Gestisci.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/produzione/""><IMG border=0 src=""../images/help_home_08_Pianifica.gif""></A></TD><TD vAlign=center width=168 align=middle><A href=""http://www.ntsinformatica.it/software-erp-medie-imprese/software-erp/archiviazione-documenti/""><IMG border=0 src=""../images/help_home_09_Archivia.gif""></A></TD><TD vAlign=center width=169 align=right><IMG border=0 src=""../images/help_home_NTS_logo_down1.gif""></TD><TD vAlign=center width=146 align=right><A href=""http://www.ntsinformatica.it""><IMG border=0 alt=""Clicca per accedere al sito di NTS Informatica"" src=""../images/help_home_NTS_logo_down2.gif""></A></TD></TR><TR><TD width=987 colSpan=6><IMG border=0 src=""../images/help_home_Line.gif""></TD></TR></TABLE>", "").Replace("</body></html>", "")
      r1.Close()

      If strStampaTmp.Contains("<p>In questa sezione (Argomenti e Moduli)") Or _
         strStampaTmp.Contains("<p>In questa sezione (Guida Analitica)") Then
        'non stampare
      Else
        If strStampaTmp.IndexOf("<FONT class=title color=white size=6>") > -1 Then
          strStampa.Append("<TABLE border=0 cellSpacing=0 cellPadding=0><TR><TD bgColor=#009ee2 height=53 vAlign=center width=987 colSpan=4 align=left><p>" & strStampaTmp.Substring(strStampaTmp.IndexOf("<FONT class=title color=white size=6>")))
        Else
          strStampa.Append(strStampaTmp.Substring(strStampaTmp.IndexOf(">", strStampaTmp.ToUpper.IndexOf("<BODY")) + 1))
        End If
      End If

      If nodeTree.GetNodeCount(False) > 0 Then
        For Each nodeTmp As TreeNode In nodeTree.Nodes
          CreaStampaNode(nodeTmp)
        Next
      End If
      Return True
    Catch ex As Exception
      Try
        r1.Close()
      Catch ex2 As Exception
      End Try
      Dim strErr As String = GestError(ex, Me, strMainDir & nodeTree.Tag.ToString, oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function


  Public Overridable Sub cmdOpenPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpenPdf.Click
    Try
      If File.Exists(trPDF.SelectedNode.Name) Then
        NTSProcessStart(trPDF.SelectedNode.Name, "")
      End If
    Catch ex As Exception


    End Try
  End Sub

  Public Overridable Sub cmdSfogliaPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSfogliaPdf.Click
    Dim ofdImg As New NTSOpenFileDialog
    Dim strPDFDir As String = ""
    Try
      ofdImg.Filter = "PDF|*.pdf"
      strPDFDir = oMenu.GetSettingBus("BS--HELP", "RECENT", "USER", "PDFDir", oApp.NetDir, ".", oApp.NetDir)
      If Directory.Exists(strPDFDir) Then
        ofdImg.InitialDirectory = strPDFDir
      Else
        ofdImg.InitialDirectory = oApp.NetDir
      End If
      ofdImg.oMenu = oMenu
      If ofdImg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
        If File.Exists(ofdImg.FileName) = False Then Return
        Dim fileInfoPdf As New IO.FileInfo(ofdImg.FileName)
        If fileInfoPdf.Extension.ToUpper = ".PDF" Then
          oMenu.SaveSettingBus("BS--HELP", "RECENT", "USER", "PDFDir", fileInfoPdf.DirectoryName, " ", "NNS", "...", "...")
          InserisciFilePDF(fileInfoPdf.FullName)
          SaveFilePDF()
          CaricaPDF()
          TreeNodePDFValidate(fileInfoPdf.FullName)
        End If
      End If

    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub
  Public dttFilePDF As DataTable
  Public Overridable Function LeggiFilePDF() As Boolean
    Dim strPDF As String = ""
    Try
      dttFilePDF = New DataTable
      dttFilePDF.TableName = "FilePDF"
      dttFilePDF.Columns.Add("percorso", GetType(String))
      dttFilePDF.Columns.Add("NomePDF", GetType(String))
      strPDF = oMenu.GetSettingBus("BS--HELP", "OPZIONI", ".", "ElencoFilePDFCondivisi", "", " ", "")
      If strPDF.Trim <> "" Then
        If strPDF.Contains("§"c) Then
          For Each strTmpCro As String In strPDF.Split("§"c)
            If File.Exists(strTmpCro) Then
              If Not dttFilePDF.Select("percorso=" & CStrSQL(strTmpCro)).Length > 0 Then
                Dim fileInfoPdf As New IO.FileInfo(strTmpCro)
                dttFilePDF.Rows.Add(New Object() {strTmpCro, fileInfoPdf.Name})
              End If
            End If
          Next
        Else
          If File.Exists(strPDF) Then
            If Not dttFilePDF.Select("percorso=" & CStrSQL(strPDF)).Length > 0 Then
              Dim fileInfoPdf As New IO.FileInfo(strPDF)
              dttFilePDF.Rows.Add(New Object() {strPDF, fileInfoPdf.Name})
            End If
          End If
        End If
      End If
      strPDF = oMenu.GetSettingBus("BS--HELP", "RECENT", "FilePDF", oApp.User.Nome, "", " ", "")
      If strPDF.Trim <> "" Then
        If strPDF.Contains("§"c) Then
          For Each strTmpCro As String In strPDF.Split("§"c)
            If File.Exists(strTmpCro) Then
              Dim fileInfoPdf As New IO.FileInfo(strTmpCro)
              dttFilePDF.Rows.Add(New Object() {strTmpCro, fileInfoPdf.Name})
            End If
          Next
        Else
          If File.Exists(strPDF) Then
            Dim fileInfoPdf As New IO.FileInfo(strPDF)
            dttFilePDF.Rows.Add(New Object() {strPDF, fileInfoPdf.Name})
          End If
        End If
      End If

      Return True
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = CLN__STD.GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------	
    End Try
  End Function
  Public Overridable Sub InserisciFilePDF(ByVal strPDF As String)
    Try
      If Not dttFilePDF.Select("percorso=" & CStrSQL(strPDF)).Length > 0 Then
        If File.Exists(strPDF) Then
          Dim fileInfoPdf As New IO.FileInfo(strPDF)
          If dttFilePDF.Select("percorso=" & CStrSQL(strPDF)).Length = 0 Then _
            dttFilePDF.Rows.Add(New Object() {strPDF, fileInfoPdf.Name})
        End If
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub
  Public Overridable Sub SaveFilePDF()
    Dim strFilePDF As String = ""
    Dim strPDF As String = ""
    Try
      Dim dttTmpCond As New DataTable
      dttTmpCond.Columns.Add("percorso", GetType(String))
      strPDF = oMenu.GetSettingBus("BS--HELP", "OPZIONI", ".", "ElencoFilePDFCondivisi", "", " ", "")
      If strPDF.Trim <> "" Then
        If strPDF.Contains("§"c) Then
          For Each strTmpCro As String In strPDF.Split("§"c)
            dttTmpCond.Rows.Add(New Object() {strTmpCro})
          Next
        Else
          dttTmpCond.Rows.Add(New Object() {strPDF})
        End If
      End If

      If dttFilePDF.Rows.Count > 0 Then

        For Each dtrPDF As DataRow In dttFilePDF.Rows
          With dtrPDF
            If strFilePDF = "" Then
              If dttTmpCond.Select("percorso=" & CStrSQL(!percorso.ToString)).Length = 0 Then _
              strFilePDF = !percorso.ToString
            Else
              If dttTmpCond.Select("percorso=" & CStrSQL(!percorso.ToString)).Length = 0 Then _
              strFilePDF &= "§" & !percorso.ToString
            End If
          End With
        Next


      End If
      oMenu.SaveSettingBus("BS--HELP", "RECENT", "FilePDF", oApp.User.Nome, strFilePDF, " ", "...", "...", "...")
    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Function TreeNodePDFValidate(ByRef strPDF As String) As Boolean
    Dim tmp As System.Windows.Forms.TreeNode
    Try
      If trPDF.Nodes.Count > 0 Then
        If trPDF.SelectedNode Is Nothing Then
          For Each tmp In trPDF.Nodes
            If tmp.Name.ToUpper = strPDF.ToUpper Then
              trPDF.SelectedNode = tmp
              WebBrows.Navigate(trPDF.SelectedNode.Name)
              trContenuto.SelectedNode = Nothing
              Return True
            End If
          Next
        Else
          If trPDF.SelectedNode.Name.ToUpper = strPDF.ToUpper Then Return True
          For Each tmp In trPDF.Nodes
            If tmp.Name.ToUpper = strPDF.ToUpper Then
              trPDF.SelectedNode = tmp
              WebBrows.Navigate(trPDF.SelectedNode.Name)
              trContenuto.SelectedNode = Nothing
              Return True
            End If
          Next
        End If
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Function
  Public Overridable Sub CaricaPDF()
    Dim oNode As TreeNode = Nothing
    Dim oLastNodeAdd As TreeNode = Nothing
    Try
      trPDF.Nodes.Clear()


      If dttFilePDF.Rows.Count > 0 Then

        For Each dtrPDF As DataRow In dttFilePDF.Select("", "NomePDF asc")

          oNode = New TreeNode

          oNode.Text = dtrPDF!NomePDF.ToString.Trim.Substring(0, dtrPDF!NomePDF.ToString.Trim.Length - 4)
          oNode.Tag = "pdf"
          oNode.Name = dtrPDF!percorso.ToString

          trPDF.Nodes.Add(oNode)

        Next
      End If

    Catch ex As Exception
      '-------------------------------------------------
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
      '-------------------------------------------------
    End Try
  End Sub

  Public Overridable Sub trPDF_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trPDF.AfterSelect
    Try
      If e.Action = TreeViewAction.Unknown Then Return
      WebBrows.Navigate(trPDF.SelectedNode.Name)
      trContenuto.SelectedNode = Nothing
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub


  Public Overridable Sub cmdRimuoviPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRimuoviPDF.Click
    Dim tmp As System.Windows.Forms.TreeNode
    Dim nodeBefore As System.Windows.Forms.TreeNode = Nothing
    Try

      If Not trPDF.SelectedNode Is Nothing Then
        Dim strPDF As String = trPDF.SelectedNode.Name
        For Each tmp In trPDF.Nodes
          If tmp.Name.ToUpper = strPDF.ToUpper Then
            Exit For
          End If
          nodeBefore = tmp
        Next

        For Each dtrPDF As DataRow In dttFilePDF.Select("percorso=" & CStrSQL(trPDF.SelectedNode.Name))
          dtrPDF.Delete()
        Next
        dttFilePDF.AcceptChanges()
        SaveFilePDF()
        CaricaPDF()

        If Not nodeBefore Is Nothing Then
          For Each tmp In trPDF.Nodes
            If tmp.Name.ToUpper = nodeBefore.Name.ToUpper Then
              trPDF.SelectedNode = tmp
              WebBrows.Navigate(nodeBefore.Name.ToUpper)
              trContenuto.SelectedNode = Nothing
              Exit For
            End If
          Next
        Else
          If trPDF.Nodes.Count > 0 Then
            trPDF.SelectedNode = trPDF.Nodes(0)
            WebBrows.Navigate(trPDF.SelectedNode.Name.ToUpper)
            trContenuto.SelectedNode = Nothing
          Else
            cmdHome_Click(cmdHome, Nothing)
          End If
        End If
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub



  Public Overridable Sub pnContenutoDown_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles pnContenutoDown.DragDrop
    Try
      Dim strfile As String = ""
      Dim strFiles() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
      For i As Integer = 0 To strFiles.Length - 1

        If File.Exists(strFiles(i)) Then
          Dim fileInfoPdf As New IO.FileInfo(strFiles(i))
          If fileInfoPdf.Extension.ToUpper = ".PDF" Then
            InserisciFilePDF(fileInfoPdf.FullName)
            strfile = fileInfoPdf.FullName
          End If
        End If
      Next
      If strfile <> "" Then
        SaveFilePDF()
        CaricaPDF()
        TreeNodePDFValidate(strfile)
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub



  Public bNavigateAdd As Boolean = False
  Public Overridable Sub tsView_SelectedPageChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles tsView.SelectedPageChanged
    Try
      cmdDelURL.Visible = False
      cmdDelURL.Enabled = False
      If e.Page.Name = TabPageView1.Name Then
        Dim strIn As String = oMenu.GetSettingBus("BS--HELP", "RECENT", "USER", "URL", "", ".", "")
        If strIn <> "" Then Decodifica(strIn)
        Dim strInTitle As String = oMenu.GetSettingBus("BS--HELP", "RECENT", "USER", "TITLE", "", ".", "")
        If strIn.Trim <> "" AndAlso strInTitle.Trim <> "" Then
          GctlSetVisEnab(cmdDelURL, False)
          GctlSetVisEnab(cmdDelURL, True)
          If bNavigateAdd = False Then
            Try
              WebBrowsAgg.Navigate(strIn)
            Catch ex As Exception
              WebBrowsAgg.Refresh()
            End Try
            bNavigateAdd = True
          End If
        Else
          strIn = oApp.InputBoxNew("Inserire l'URL:", "")
          strInTitle = oApp.InputBoxNew("Inserire il titolo:", "")
          If strIn.Trim <> "" AndAlso strInTitle.Trim <> "" Then
            GctlSetVisEnab(cmdDelURL, False)
            GctlSetVisEnab(cmdDelURL, True)
            Codifica(strIn)
            oMenu.SaveSettingBus("BS--HELP", "RECENT", "USER", "URL", strIn, " ", "NNS", "...", "...")
            oMenu.SaveSettingBus("BS--HELP", "RECENT", "USER", "TITLE", strInTitle, " ", "NNS", "...", "...")
            TabPageView1.Text = strInTitle
            TabPageView1.Image = Nothing
            Try
              WebBrowsAgg.Navigate(strIn)
            Catch ex As Exception
              WebBrowsAgg.Refresh()
            End Try
            bNavigateAdd = True
          End If
        End If
      Else

      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdDelURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDelURL.Click
    Try
      If oApp.MsgBoxInfoYesNo_DefNo("Rimuovere ll'URL aggiunto?") = Windows.Forms.DialogResult.Yes Then
        TabPageView1.Text = "Aggiungi URL"
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM__HELP))
        Me.TabPageView1.Image = CType(resources.GetObject("TabPageView1.Image"), System.Drawing.Image)
        oMenu.SaveSettingBus("BS--HELP", "RECENT", "USER", "URL", "", " ", "NNS", "...", "...")
        oMenu.SaveSettingBus("BS--HELP", "RECENT", "USER", "TITLE", "", " ", "NNS", "...", "...")
        tsView.SelectedTabPage = TabPageView0
        bNavigateAdd = False
        cmdDelURL.Visible = False
        cmdDelURL.Enabled = False
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub


  Public Overridable Sub cmdListaNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdListaNum.Click
    Dim frmInsOpzione As FRM__HELO = Nothing
    Dim oPar As CLE__CLDP

    Try
      frmInsOpzione = CType(NTSNewFormModal("FRM__HELO"), FRM__HELO)

      oPar = New CLE__CLDP
      oPar.bPar1 = False
      frmInsOpzione.Init(oMenu, oPar, DittaCorrente)
      frmInsOpzione.ShowDialog()
      If oPar.bPar1 Then
        Dim Range As IHTMLTxtRange
        Range = CType(docHTML.selection.createRange(), IHTMLTxtRange)
        Range.pasteHTML(oPar.strPar1 & " ")
        Range.collapse(False)
        Range.select()
      End If
    Catch ex As Exception
      Dim strErr As String = GestError(ex, Me, "", oApp.InfoError, oApp.ErrorLogFile, True)
    End Try
  End Sub

  Public Overridable Sub cmdEditorNTS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEditorNTS.Click
    Try
      If IO.File.Exists("\\s01\Utenti\Filippucci\guida\HelpEditor.exe") Then
        Dim strCommand As String = """\\s01\Utenti\Filippucci\guida\HelpEditor.exe"""
        NTSProcessStart(strCommand, """/vers=" & strBusVer & """ ""/page=" & strLastHTM & """")
      End If
    Catch ex As Exception
    End Try
  End Sub
End Class
